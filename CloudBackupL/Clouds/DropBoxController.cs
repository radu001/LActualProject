using ByteSizeLib;
using Dropbox.Api;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public DropBoxController()
        {
        }

        public String PrepareUri()
        {
            var appKey = ConfigurationManager.AppSettings["dropboxAppKey"];
            var uri = string.Format(
                @"https://www.dropbox.com/1/oauth2/authorize?response_type=token&redirect_uri={0}&client_id={1}",
                redirectUri, appKey);
            return uri;
        }

        static async Task Connect()
        {
            using (var dbx = new DropboxClient("GS22QMqeLQ0AAAAAAAABgckLNV9clypyh2QwOcodvfo_r_om8xpG6GKcuHqrYBUi"))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
                Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
            }
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
    }
}
