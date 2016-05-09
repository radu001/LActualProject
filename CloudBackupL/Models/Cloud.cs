using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackupL
{
    class Cloud
    {
        [MaxLength(50), NotNull, PrimaryKey]
        public String id { get; set; }

        [MaxLength(50), NotNull, Unique]
        public String name { get; set; }

        [MaxLength(50), NotNull]
        public String cloudType { get; set; }

        [MaxLength(250), NotNull]
        public String token { get; set; }

        [NotNull]
        public DateTime date { get; set; }

        public Cloud(String id, String name, String cloudType, String token, DateTime date)
        {
            this.id = id;
            this.name = name;
            this.cloudType = cloudType;
            this.token = token;
            this.date = date;
        }

        public Cloud()
        {
        }
    }
}
