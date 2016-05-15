using ByteSizeLib;
using Dropbox.Api;
using Dropbox.Api.Files;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CloudBackupL
{
    class DropBoxController : ICloud
    {
        public object MessageBox { get; private set; }
        string redirectUri = ConfigurationManager.AppSettings["dropboxRedirectUri"];
        FileStream CurrentFileStream;
        DatabaseService databaseService;

        public DropBoxController()
        {
            databaseService = new DatabaseService();
        }

        public String PrepareUri()
        {
            var appKey = ConfigurationManager.AppSettings["dropboxAppKey"];
            var uri = string.Format(
                @"https://www.dropbox.com/1/oauth2/authorize?response_type=token&redirect_uri={0}&client_id={1}",
                redirectUri, appKey);
            return uri;
        }

        public string ParseUriForToken(Uri uri)
        {
            if (uri.AbsoluteUri.StartsWith(@redirectUri) && !uri.AbsoluteUri.Equals(@redirectUri))
            {
                var accessToken = HttpUtility.ParseQueryString(uri.Fragment.Substring(1))["access_token"];
                return accessToken;
            }
            return null;
        }

        public string GetAccountInfo(String accessToken)
        {
            var client = new WebClient();
            client.Headers["Authorization"] = "Bearer " + accessToken;
            return client.DownloadString("https://www.dropbox.com/1/account/info");
        }

        public double GetFreeSpaceInGB(String accessToken)
        {
            var client = new WebClient();
            client.Headers["Authorization"] = "Bearer " + accessToken;
            string source = client.DownloadString("https://www.dropbox.com/1/account/info");

            dynamic data = JObject.Parse(source);
            double totalSpaceBytes = data.quota_info.quota;
            double normal = data.quota_info.normal;
            double shared = data.quota_info.shared;
            double freeSpaceBytes = totalSpaceBytes - normal - shared;
            return ByteSize.FromBytes(freeSpaceBytes).GigaBytes;
        }

        public double GetTotalSpaceInGB(String accessToken)
        {
            var client = new WebClient();
            client.Headers["Authorization"] = "Bearer " + accessToken;
            string source = client.DownloadString("https://www.dropbox.com/1/account/info");

            dynamic data = JObject.Parse(source);
            double totalSpaceBytes = data.quota_info.quota;
            return ByteSize.FromBytes(totalSpaceBytes).GigaBytes;
        }

        public async Task<Boolean> Upload(string file, string targetPath, DropboxClient client, MainWindow instance, Backup backup, Stopwatch watch)
        {
            instance.BeginInvoke(new Action(() => instance.ReportProgress(200)));
            const int chunkSize = 1024 * 10240;
            FileStream CurrentFileStream = File.Open(file, FileMode.Open, FileAccess.Read);
            
            if (CurrentFileStream.Length <= chunkSize)
            {
                System.Diagnostics.Trace.WriteLine("Start one-shot upload");
                await client.Files.UploadAsync(targetPath, body: CurrentFileStream, mode: WriteMode.Overwrite.Instance);
                instance.BeginInvoke(new Action(() => instance.ReportProgress(110)));
            } else {
                System.Diagnostics.Trace.WriteLine("Start chunk upload");
                int numChunks = (int)Math.Ceiling((double)CurrentFileStream.Length / chunkSize);

                byte[] buffer = new byte[chunkSize];
                string sessionId = null;

                for (var idx = 0; idx < numChunks; idx++)
                {
                    var byteRead = CurrentFileStream.Read(buffer, 0, chunkSize);

                    using (MemoryStream memStream = new MemoryStream(buffer, 0, byteRead))
                    {
                        if (idx == 0)
                        {
                            var result = await client.Files.UploadSessionStartAsync(false, memStream);
                            sessionId = result.SessionId;
                        }
                        else
                        {
                            UploadSessionCursor cursor = new UploadSessionCursor(sessionId, (ulong)(chunkSize * idx));

                            if (idx == numChunks - 1)
                            {
                                System.Diagnostics.Trace.WriteLine("Session finish");
                                await client.Files.UploadSessionFinishAsync(cursor, new CommitInfo(targetPath, mode: WriteMode.Overwrite.Instance), memStream);
                                instance.BeginInvoke(new Action(() => instance.ReportProgress(110)));
                            }

                            else
                            {
                                instance.Invoke(new Action(() => instance.ReportProgress((int)(idx*100 / numChunks))));
                                await client.Files.UploadSessionAppendV2Async(cursor, body: memStream);
                                
                            }
                        }
                    }
                }
            }
            watch.Stop();
            backup.runTime = watch.ElapsedMilliseconds;
            databaseService.InsertBackup(backup);
            return true;
        }
    }
}
