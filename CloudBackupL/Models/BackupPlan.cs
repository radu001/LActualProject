using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackupL
{
    class BackupPlan
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int id { get; set; }

        [MaxLength(100), NotNull]
        public String name { get; set; }

        [NotNull]
        public DateTime creationDate { get; set; }

        [MaxLength(300), NotNull]
        public String path { get; set; }

        [MaxLength(50), NotNull]
        public String cloudName { get; set; }

        [MaxLength(50), NotNull]
        public string cloudId { get; set; }

        public DateTime lastExecution { get; set; }

        public Boolean lastResult { get; set; }

        [NotNull]
        public DateTime nextExecution { get; set; }

        [MaxLength(50), NotNull]
        public String currentStatus { get; set; }

        public float lastBackupDuration { get; set; }

        [MaxLength(50), NotNull]
        public String scheduleType { get; set; }

        public DateTime scheduleTime { get; set; }

        public int scheduleDay { get; set; }

        public Boolean overrideBackup { get; set; }

    }
}
