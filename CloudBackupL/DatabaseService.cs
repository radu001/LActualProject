using CloudBackupL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace CloudBackupL
{
    class DatabaseService
    {
        String connString = ConfigurationManager.AppSettings["DbSQLite"];
        private static bool checkDBExists = true;

        public DatabaseService()
        {
            if (checkDBExists && !File.Exists(connString))
            {
                checkDBExists = false;

                Settings s = new Settings();
                s.nrAttempts = 3;
                s.delay = 500;
                s.preventShutDown = true;
                s.trayType = "never";
                s.chunkSize = 99;
                s.askPassword = true;
                s.logFileLocation = AppDomain.CurrentDomain.BaseDirectory + "\\log.txt";

                lock (DatabaseConnection.connection)
                {
                    DatabaseConnection.connection.CreateTable<Cloud>();
                    DatabaseConnection.connection.CreateTable<BackupPlan>();
                    DatabaseConnection.connection.CreateTable<Backup>();
                    DatabaseConnection.connection.CreateTable<Settings>();
                    DatabaseConnection.connection.Insert(s);
                }
            }
        }

        public int InsertBackupPlan(BackupPlan backupPlan)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Insert(backupPlan);
            }
        }

        public int UpdateBackupPlan(BackupPlan backupPlan)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Update(backupPlan);
            }
        }

        public int UpdateCloud(Cloud cloud)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Update(cloud);
            }
        }

        public int InsertCloud(Cloud cloud)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Insert(cloud);
            }
        }

        public void InsertBackup(Backup backup)
        {
            lock (DatabaseConnection.connection)
            {
                DatabaseConnection.connection.Insert(backup);
                BackupPlan backupPlan = GetBackupPlan(backup.backupPlanId);
                backupPlan.lastExecution = backup.date;
                backupPlan.currentStatus = "Updated";
                backupPlan.lastResult = true;
                backupPlan.lastBackupDuration = backup.runTime;
                DatabaseConnection.connection.Update(backupPlan);
            }
        }

        public List<Cloud> GetAllClouds()
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Query<Cloud>("select * from cloud");
            }
        }

        public List<BackupPlan> GetAllPlans()
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Query<BackupPlan>("select * from BackupPlan");
            }
        }

        public bool CheckPlanName(String name)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Query<Cloud>("select * from cloud where name = ?", name).Count == 0 ? true : false;
            }
        }

        public void DeletePlan(int id)
        {
            lock (DatabaseConnection.connection)
            {
                DatabaseConnection.connection.Delete<BackupPlan>(id);
                DatabaseConnection.connection.Query<Backup>("delete from Backup where backupPlanId = ?", id);
            }
        }

        public BackupPlan GetBackupPlan(int id)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Get<BackupPlan>(id);
            }
        }

        public Cloud GetCloudByName(String name)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Query<Cloud>("select * from cloud where name = ?", name)[0];
            }
        }

        public bool IsCloudAlreadyInsered(string id)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Query<Cloud>("select * from cloud where id = ?", id).Count > 0 ? true : false;
            }
        }

        public bool CanDeleteCloud(string id)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Query<BackupPlan>("select * from BackupPlan where cloudId = ?", id).Count > 0 ? false : true;
            }
        }

        public void DeleteCloud(string id)
        {
            lock (DatabaseConnection.connection)
            {
                DatabaseConnection.connection.Delete<Cloud>(id);
            }
        }

        public void DeleteBackup(int id)
        {
            lock (DatabaseConnection.connection)
            {
                DatabaseConnection.connection.Delete<Backup>(id);
            }
        }

        public List<Backup> GetBackCloudBackups(string cloudId)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Query<Backup>("select * from Backup where cloudId = ?", cloudId);
            }
        }

        public Backup GetBackup(int backupId)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Get<Backup>(backupId);
            }
        }

        public Backup GetLastBackup(int planId)
        {
            lock (DatabaseConnection.connection)
            {
                List<Backup> result;
                result = DatabaseConnection.connection.Query<Backup>("select * from Backup where backupPlanId = ? order by id desc limit 1", planId);
                return result.Count > 0 ? result[0] : null;
            }
        }

        public Cloud GetCloud(string cloudId)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Get<Cloud>(cloudId);
            }
        }

        public List<Backup> GetBackupsByPlanId(int planId)
        {
            lock (DatabaseConnection.connection)
            {
                return DatabaseConnection.connection.Query<Backup>("select * from Backup where backupPlanId = ?", planId);
            }
        }

        public Cloud GetCloudByToken(string accessToken)
        {
            lock (DatabaseConnection.connection)
            {
                List<Cloud> clouds;
                clouds = DatabaseConnection.connection.Query<Cloud>("select * from Cloud where token = ?", accessToken);
                return clouds.Count == 1 ? clouds[0] : null;
            }
        }

        public Settings GetSettings()
        {
            List<Settings> settings;
            lock (DatabaseConnection.connection)
            {
                settings = DatabaseConnection.connection.Query<Settings>("select * from Settings");
                return settings.Count > 0 ? settings[0] : null;
            }
        }

        public void SetSettings(Settings settings)
        {
            lock (DatabaseConnection.connection)
            {
                DatabaseConnection.connection.Update(settings);
            }
        }
    }
}
