using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackupL
{
    interface ICloud
    {
        String PrepareUri();

        String ParseUriForToken(Uri uri);
    }
}
