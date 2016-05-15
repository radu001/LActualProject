using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackupL
{
    class Backup
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int id { get; set; }

        [NotNull]
        public DateTime date { get; set; }

        [NotNull]
        public int cloudId { get; set; }

        [NotNull]
        public int backupPlanId { get; set; }

        [MaxLength(100), NotNull]
        public String backupPlanName { get; set; }

        public long runTime { get; set; }

        public long size { get; set; }

        public long compressedSize { get; set; }

        [MaxLength(100), NotNull]
        public String targetPath { get; set; }

    }
}
