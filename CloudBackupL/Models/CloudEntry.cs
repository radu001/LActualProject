using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackupL.Models
{
    class CloudEntry
    {
        public string path;
        public DateTime date;

        public CloudEntry(string path, DateTime date)
        {
            this.path = path;
            this.date = date;
        }
    }
}
