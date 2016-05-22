using System;
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

        void Download(string cloudPath, string token, string localPath, DownloadProgressChangedEventHandler eh, TaskCompletionSource<bool> tcs);

        void GetFilesList(String accessToken, DownloadStringCompletedEventHandler eventHandler, string currentPath);
    }
}
