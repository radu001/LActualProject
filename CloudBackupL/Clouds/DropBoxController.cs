using ByteSizeLib;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using CloudBackupL.Models;
using System.Collections.Generic;

namespace CloudBackupL
{
    class DropBoxController : ICloud
    {
        public object MessageBox { get; private set; }
        string redirectUri = ConfigurationManager.AppSettings["dropboxRedirectUri"];
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

        public CloudUserInfo GetAccountInfo(String accessToken)
        {
            var client = new WebClient();
            client.Headers["Authorization"] = "Bearer " + accessToken;
            string source = client.DownloadString("https://www.dropbox.com/1/account/info");
            dynamic data = JObject.Parse(source);
            CloudUserInfo cloudUserInfo = new CloudUserInfo();
            cloudUserInfo.uid = data.uid;
            cloudUserInfo.total_space = data.quota_info.quota;
            cloudUserInfo.used_space = data.quota_info.normal + data.quota_info.shared;
            cloudUserInfo.free_space = cloudUserInfo.total_space - cloudUserInfo.used_space;
            return cloudUserInfo;
        }

        EventHandler<List<CloudEntry>> eventHandler;
        public void GetFilesList(String accessToken, EventHandler<List<CloudEntry>> eventHandler, string currentPath)
        {
            this.eventHandler = eventHandler;
            var client = new WebClient();
            client.DownloadStringCompleted += Client_DownloadStringCompleted; ;
            client.Headers["Authorization"] = "Bearer " + accessToken;
            client.DownloadStringAsync(new Uri("https://api.dropboxapi.com/1/metadata/auto/" + currentPath));
        }

        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs args)
        {
            if (args.Error == null)
            {
                List<CloudEntry> list = new List<CloudEntry>();
                dynamic result = JObject.Parse(args.Result);
                foreach (var file in result.contents)
                {
                    DateTime date = DateTime.Parse((string)file.modified);
                    string path = file.path.ToString();
                    CloudEntry entry = new CloudEntry(path, date);
                    list.Add(entry);
                }
                eventHandler(this, list);
            }
        }

        public void Download(string cloudPath, string token, string localPath, DownloadProgressChangedEventHandler eh, TaskCompletionSource<bool> tcs)
        {
            var web = new WebClient();
            web.DownloadProgressChanged += eh;
            web.DownloadFileCompleted += (sender, args) => Web_DownloadFileCompleted(sender, tcs, args);
            web.DownloadFileAsync(new Uri(string.Format("https://content.dropboxapi.com/1/files/auto{0}?access_token={1}", cloudPath, token)), localPath);
        }

        private void Web_DownloadFileCompleted(object sender, TaskCompletionSource<bool> tcs, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                tcs.TrySetCanceled();
            }
            else if (e.Error != null)
            {
                tcs.TrySetException(e.Error);
            }
            else
            {
                tcs.TrySetResult(true);
            }
        }

        public void Upload(string cloudPath, string token, string clientPath, UploadProgressChangedEventHandler peh, TaskCompletionSource<bool> tcs)
        {
            using (var web = new WebClient())
            {
                web.Headers["Authorization"] = "Bearer " + token;
                web.UploadProgressChanged += peh;
                web.UploadFileCompleted += (sender, args) => Web_UploadFileCompleted(sender, tcs, args);
                web.UploadFileAsync(new Uri(string.Format("https://content.dropboxapi.com/1/files_put/auto/{0}", cloudPath)), "PUT", clientPath);
            }
        }


        private void Web_UploadFileCompleted(object sender, TaskCompletionSource<bool> tcs, UploadFileCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                tcs.TrySetCanceled();
            }
            else if (e.Error != null)
            {
                tcs.TrySetException(e.Error);
            }
            else
            {
                tcs.TrySetResult(true);
            }
        }

        public void DeleteFolder(String accessToken, DownloadStringCompletedEventHandler eventHandler, string currentPath)
        {
            var client = new WebClient();
            client.DownloadStringCompleted += eventHandler;
            client.DownloadStringAsync(new Uri(string.Format("https://api.dropboxapi.com/1/fileops/delete/?path={0}&root=sandbox&access_token={1}", currentPath, accessToken)));
        }
    }
}
