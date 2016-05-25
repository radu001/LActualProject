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
        public string id { get; set; }

        [MaxLength(50), NotNull, Unique]
        public String name { get; set; }

        [MaxLength(50), NotNull]
        public String cloudType { get; set; }

        [MaxLength(1000), NotNull]
        public String token { get; set; }

        [MaxLength(1000)]
        public String refreshToken { get; set; }

        [NotNull]
        public DateTime date { get; set; }

        public Cloud(string id, String name, String cloudType, String token, DateTime date)
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
