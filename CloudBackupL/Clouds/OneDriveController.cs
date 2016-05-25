using CloudBackupL.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace CloudBackupL.Clouds
{
    class OneDriveController : ICloud
    {
        string redirectUri = ConfigurationManager.AppSettings["oneDriveRedirectUri"];
        string appKey = ConfigurationManager.AppSettings["oneDriveAppKey"];
        string appSecret = ConfigurationManager.AppSettings["oneDriveSecretAppKey"];
        DatabaseService databaseService;

        public OneDriveController()
        {
            databaseService = new DatabaseService();
        }

        public String PrepareUri()
        {
            var uri = string.Format(
                "https://login.live.com/oauth20_authorize.srf?client_id={0}&scope=onedrive.readwrite offline_access&response_type=code&redirect_uri={1}",
                appKey, redirectUri);
            return uri;
        }

        public string RefreshToken(string accessToken)
        {
            Cloud cloud = databaseService.GetCloudByToken(accessToken);
            if (cloud == null) return accessToken;
            string myParameters = String.Format("client_id={0}&redirect_uri={1}&client_secret={2}&refresh_token={3}&grant_type=refresh_token", appKey, redirectUri, appSecret, cloud.refreshToken);
            WebClient client = new WebClient();
            client.UseDefaultCredentials = true;
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            string result = client.UploadString("https://login.live.com/oauth20_token.srf", "POST", myParameters);
            dynamic data = JObject.Parse(result);
            cloud.token = data.access_token;
            cloud.refreshToken = data.refresh_token;
            databaseService.UpdateCloud(cloud);
            return cloud.token;
        }

        public string ParseUriForToken(Uri uri)
        {
            if (uri.AbsoluteUri.StartsWith(@redirectUri) && !uri.AbsoluteUri.Equals(@redirectUri))
            {             
                var code = HttpUtility.ParseQueryString(uri.Query).Get("code");
                string myParameters = String.Format("client_id={0}&redirect_uri={1}&client_secret={2}&code={3}&grant_type=authorization_code", appKey, redirectUri, appSecret, code);
                WebClient client = new WebClient();
                client.UseDefaultCredentials = true;
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string result = client.UploadString("https://login.live.com/oauth20_token.srf", "POST", myParameters);
                return result;
            }
            return null;
        }

        public CloudUserInfo GetAccountInfo(string accessToken)
        {
            accessToken = RefreshToken(accessToken);
            CloudUserInfo cloudUserInfo = new CloudUserInfo();
            var client = new WebClient();
            client.Headers["Authorization"] = "Bearer " + accessToken;
            string source = client.DownloadString("https://api.onedrive.com/v1.0/drive");
            dynamic data = JObject.Parse(source);
            cloudUserInfo.uid = data.id;
            cloudUserInfo.total_space = data.quota.total;
            cloudUserInfo.used_space = data.quota.used;
            cloudUserInfo.free_space = data.quota.remaining;
            return cloudUserInfo;
        }

        public void Upload(string cloudPath, string token, string clientPath, UploadProgressChangedEventHandler peh, TaskCompletionSource<bool> tcs)
        {
            using (var web = new WebClient())
            {
                web.Headers["Authorization"] = "Bearer " + token;
                web.UploadProgressChanged += peh;
                web.UploadFileCompleted += (sender, args) => Web_UploadFileCompleted(sender, tcs, args);
                web.UploadFileAsync(new Uri(string.Format("https://api.onedrive.com/v1.0/drive/special/approot:{0}:/content", cloudPath)), "PUT", clientPath);
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

        public void Download(string cloudPath, string token, string localPath, DownloadProgressChangedEventHandler eh, TaskCompletionSource<bool> tcs)
        {
            var web = new WebClient();
            web.DownloadProgressChanged += eh;
            web.Headers["Authorization"] = "Bearer " + token;
            web.DownloadFileCompleted += (sender, args) => Web_DownloadFileCompleted(sender, tcs, args);
            web.DownloadFileAsync(new Uri(string.Format("https://api.onedrive.com/v1.0/drive/special/approot:{0}:/content", cloudPath)), localPath);
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



        EventHandler<List<CloudEntry>> eventHandler;
        string currentPath;
        public void GetFilesList(string accessToken, EventHandler<List<CloudEntry>> eventHandler, string currentPath)
        {
            this.currentPath = currentPath;
            this.eventHandler = eventHandler;
            var client = new WebClient();
            client.DownloadStringCompleted += Client_DownloadStringCompleted; ;
            client.Headers["Authorization"] = "Bearer " + accessToken;
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            if (currentPath.EndsWith("/"))
                currentPath = currentPath.Remove(currentPath.Length - 1);
            client.DownloadStringAsync(new Uri(String.Format("https://api.onedrive.com/v1.0/drive/special/approot:{0}:/children", currentPath)));
        }

        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs args)
        {
            if (args.Error == null)
            {
                List<CloudEntry> list = new List<CloudEntry>();
                dynamic result = JObject.Parse(args.Result);
                foreach (var file in result.value)
                {
                    DateTime date = DateTime.Parse((string)file.lastModifiedDateTime);
                    string path;
                    if (!currentPath.EndsWith("/"))
                        path = currentPath + "/" + file.name;
                    else
                        path = currentPath + file.name;
                    CloudEntry entry = new CloudEntry(path, date);
                    list.Add(entry);
                }
                eventHandler(this, list);
            }
        }

        public void DeleteFolder(string accessToken, DownloadStringCompletedEventHandler eventHandler, string currentPath)
        {
            var request = WebRequest.Create(new Uri(string.Format("https://api.onedrive.com/v1.0/drive/special/approot:/{0}", currentPath)));
            request.Method = "DELETE";
            request.Headers["Authorization"] = "Bearer " + accessToken;
            request.GetResponseAsync();
        }
    }
}
