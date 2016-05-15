using CloudBackupL.CustomControllers;
using Dropbox.Api;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackupL.Utils
{
    class ArchiveUtils
    {
        BackgroundWorker backgroundWorkerBackup;
        PlanControl currentRunningPlan;
        DatabaseService databaseService;
        DropBoxController dropBoxController;
        MainWindow mainWindowInstance;
        long size;
        long compressedSize;
        Stopwatch watch;

        public void RunArchiving(BackgroundWorker backgroundWorkerBackup, PlanControl currentRunningPlan, MainWindow mainWindowInstance)
        {
            this.backgroundWorkerBackup = backgroundWorkerBackup;
            this.currentRunningPlan = currentRunningPlan;
            this.mainWindowInstance = mainWindowInstance;
            databaseService = new DatabaseService();
            dropBoxController = new DropBoxController();
            //start counting time
            watch = System.Diagnostics.Stopwatch.StartNew();

            string id = currentRunningPlan.LabelPlanId.Text;
            BackupPlan plan = databaseService.GetBackupPlan(Int32.Parse(id));
            Cloud cloud = databaseService.GetCloudByName(plan.cloudName);
            size = GetDirectorySize(@plan.path);
            using (ZipFile zip = new ZipFile())
            {
                zip.SaveProgress += Zip_SaveProgress;
                zip.AddDirectory(@plan.path, "Backup1");
                zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                zip.Save(AppDomain.CurrentDomain.BaseDirectory + "temp.zip");
            }
        }

        private void Zip_SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead)
            {
                int progress = (int)((e.BytesTransferred * 100) / e.TotalBytesToTransfer);
                backgroundWorkerBackup.ReportProgress(progress);
            }
            else if (e.EventType == ZipProgressEventType.Saving_Completed)
            {
                compressedSize = new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "temp.zip").Length;
                string id = currentRunningPlan.LabelPlanId.Text;
                BackupPlan plan = databaseService.GetBackupPlan(Int32.Parse(id));
                Cloud cloud = databaseService.GetCloudByName(plan.cloudName);
                Console.WriteLine("saving complete");
                String targetPath = "/" + id + "/" + DateTime.Now.ToString("yyyyMMddHms") + ".zip";
                Backup backup = new Backup();
                backup.date = DateTime.Now;
                backup.targetPath = targetPath;
                backup.cloudId = Int32.Parse(cloud.id);
                backup.backupPlanId = Int32.Parse(id);
                backup.size = size;
                backup.compressedSize = compressedSize;
                backup.backupPlanName = plan.name;
                Task<Boolean> task = dropBoxController.Upload(AppDomain.CurrentDomain.BaseDirectory + "temp.zip", targetPath, new DropboxClient(cloud.token), mainWindowInstance, backup, watch);
                Boolean b = task.Result;
            }
        }

        private static long GetDirectorySize(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }
    }
}
