using CloudBackupL.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CloudBackupL
{
    public enum CloudsEnum
    {
        DropBox,
        OneDrive
    }

    interface ICloud
    {
        String PrepareUri();

        String ParseUriForToken(Uri uri);

        // String GetAccountInfo(String accessToken);
        CloudUserInfo GetAccountInfo(string accessToken);

        void Upload(string cloudPath, string token, string localPath, UploadProgressChangedEventHandler peh, TaskCompletionSource<bool> tcs);

        void Download(string cloudPath, string token, string localPath, DownloadProgressChangedEventHandler eh, TaskCompletionSource<bool> tcs);

        void GetFilesList(String token, EventHandler<List<CloudEntry>> eventHandler, string cloudPath);

        void DeleteFolder(String token, DownloadStringCompletedEventHandler eventHandler, string cloudPath);

    }
}
