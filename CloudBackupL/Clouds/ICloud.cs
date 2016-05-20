using CloudBackupL.TabsControllers;
using Dropbox.Api;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloudBackupL
{
    interface ICloud
    {
        String PrepareUri();

        String ParseUriForToken(Uri uri);

        String GetAccountInfo(String accessToken);

        //Task<Boolean> Upload(string file, string targetPath, DropboxClient client, BackupPlansTabController instance, Backup backup, Stopwatch watch);
    }
}
