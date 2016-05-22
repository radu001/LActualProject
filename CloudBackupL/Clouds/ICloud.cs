using CloudBackupL.TabsControllers;
using Dropbox.Api;
using Nemiro.OAuth;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace CloudBackupL
{
    interface ICloud
    {
        String PrepareUri();

        String ParseUriForToken(Uri uri);

        String GetAccountInfo(String accessToken);

        void Upload(string cloudPath, string token, string clientPath, UploadProgressChangedEventHandler peh, TaskCompletionSource<bool> tcs);

        void GetFilesList(string token, ExecuteRequestAsyncCallback callback, string targetPath);

        void Download(string cloudPath, string token, string localPath, DownloadProgressChangedEventHandler eh, TaskCompletionSource<bool> tcs);
    }
}
