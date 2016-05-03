using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using SQLite;
//using System.Data.SQLite;

namespace CloudBackupL
{
    class DatabaseService
    {
        String connString = ConfigurationManager.AppSettings["DbSQLite"];

        public DatabaseService()
        {
            //using (SQLiteConnection conn = new SQLiteConnection(connString, true))
            //{
            //    conn.CreateTable<Cloud>();
            //    conn.CreateTable<BackupPlan>();
            //}
        }

        public int InsertBackupPlan(BackupPlan backupPlan)
        {
            int key;
            using(SQLiteConnection conn = new SQLiteConnection(connString, true))
            {
                key = conn.Insert(backupPlan);
            }

            return key;
        }

        public int InsertCloud(Cloud cloud)
        {
            int key;
            using (SQLiteConnection conn = new SQLiteConnection(connString, true))
            {
                key = conn.Insert(cloud);
            }

            return key;
        }

        public List<Cloud> GetAllClouds()
        {
            List<Cloud> result;
            using (SQLiteConnection conn = new SQLiteConnection(connString, true))
            {
                result = conn.Query<Cloud>("select * from cloud");
            }
            return result;
        }

        public List<BackupPlan> GetAllPlans()
        {
            List<BackupPlan> result;
            using (SQLiteConnection conn = new SQLiteConnection(connString, true))
            {
                result = conn.Query<BackupPlan>("select * from BackupPlan");
            }
            return result;
        }

        public bool CheckPlanName(String name)
        {
            int count = 1;
            using (SQLiteConnection conn = new SQLiteConnection(connString, true))
            {
                count = conn.Query<Cloud>("select * from cloud where name = ?", name).Count;
            }
            return count == 0 ? true : false;
        }

        public void DeletePlan(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connString, true))
            {
                conn.Delete<BackupPlan>(id);
            }
        }

        public BackupPlan GetBackupPlan(int id)
        {
            BackupPlan plan = null;
            using (SQLiteConnection conn = new SQLiteConnection(connString, true))
            {
                plan = conn.Get<BackupPlan>(id);
            }
            return plan;
        }

        public Cloud GetCloudByName(String name)
        {
            List<Cloud> cloud;
            using (SQLiteConnection conn = new SQLiteConnection(connString, true))
            {
                cloud = conn.Query<Cloud>("select * from cloud where name = ?", name);
            }
            return cloud[0];
        }
    }
}
