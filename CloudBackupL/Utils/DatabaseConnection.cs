using SQLite;
using System;
using System.Configuration;

namespace CloudBackupL
{
    class DatabaseConnection
    {
        private static String connString = ConfigurationManager.AppSettings["DbSQLite"];
        private static SQLite.SQLiteConnection _connection;
        public static SQLite.SQLiteConnection connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SQLiteConnection(connString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex, true);
                }
                return _connection;
            }
        }

    }
}
