using Dropbox.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CloudBackupL.MainWindow;

namespace CloudBackupL
{
    interface ICloud
    {
        String PrepareUri();

        String ParseUriForToken(Uri uri);

        String GetAccountInfo(String accessToken);


        void Upload(string file, string targetPath, DropboxClient client, MainWindow instance);
    }
}
