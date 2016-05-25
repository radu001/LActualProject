using CloudBackupL.Clouds;
using CloudBackupL.Utils;
using Ionic.Zip;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudBackupL.BackupActions
{

    class UploadBackupAction
    {
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        string tempUploadZipFolder = AppDomain.CurrentDomain.BaseDirectory + "tmpZipUploadFolder\\";
        DatabaseService databaseService = new DatabaseService();
        BackupPlan backupPlan;
        Label labelStatus;
        ProgressBar progressBar;
        ICloud cloudController;
        Cloud cloud;
        EventHandler<Boolean> backupCompleteEvent;
        string password;


        public UploadBackupAction(BackupPlan backupPlan, Label labelStatus, ProgressBar progressBar, EventHandler<Boolean> backupCompleteEvent, string password)
        {
            this.backupPlan = backupPlan;
            this.labelStatus = labelStatus;
            this.progressBar = progressBar;
            this.backupCompleteEvent = backupCompleteEvent;
            this.password = password;
            this.cloud = databaseService.GetCloud(backupPlan.cloudId);

            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            if(cloud.cloudType == "dropbox")
            {
                cloudController = new DropBoxController();
            } else
            {
                cloudController = new OneDriveController();
            }
        }

        public void StartBackupAction()
        {
            if (backgroundWorker.IsBusy == false)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }


        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //start counting time
            Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            //clear temp directory
            ArchiveUtils.DeleteDirectory(tempUploadZipFolder);
            long size = ArchiveUtils.GetDirectorySize(backupPlan.path);
            ArchiveUtils.ZipFiles(backupPlan.path, tempUploadZipFolder, Zip_SaveProgress, password);
            var compressedSize = ArchiveUtils.GetDirectorySize(tempUploadZipFolder);
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");

            Backup currentBackup = new Backup();
            currentBackup.date = DateTime.Now;
            currentBackup.targetPath = "/" + backupPlan.name + "/" + new DirectoryInfo(backupPlan.path).Name + "-" + date + "/";
            currentBackup.cloudId = cloud.id;
            currentBackup.backupPlanId = backupPlan.id;
            currentBackup.size = size;
            currentBackup.compressedSize = compressedSize;
            currentBackup.backupPlanName = backupPlan.name;

            DirectoryInfo di = new DirectoryInfo(tempUploadZipFolder);
            int i = 1;
            int total = di.GetFiles().Length;
            foreach (FileInfo file in di.GetFiles())
            {
                progressBar.Invoke(new Action(() => progressBar.Value = 0));
                labelStatus.Invoke(new Action(() => labelStatus.Text = "Uploading file " + i + " of " + total));
                var tcs = new TaskCompletionSource<bool>();
                String targetPath = currentBackup.targetPath + file.Name;
                cloudController.Upload(targetPath, cloud.token, file.FullName, Web_UploadProgressChanged, tcs);
                tcs.Task.Wait();
                i++;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            ArchiveUtils.DeleteDirectory(tempUploadZipFolder);

            watch.Stop();
            currentBackup.runTime = watch.ElapsedMilliseconds;
            databaseService.InsertBackup(currentBackup);
        }


        private void Zip_SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead)
            {
                int progress = (int)((e.BytesTransferred * 100) / e.TotalBytesToTransfer);
                progressBar.Invoke(new Action(() => progressBar.Value = progress));
            }else if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry)
            {
                labelStatus.Invoke(new Action(() => labelStatus.Text = "Status: Archiving file " + e.EntriesSaved + " of " + e.EntriesTotal));
            }
        }

        private void Web_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
                progressBar.Invoke(new Action(() => progressBar.Value = (int)(e.BytesSent * 100 / e.TotalBytesToSend)));
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error == null)
            {
                backupCompleteEvent(this, true);
            } else
            {
                backupCompleteEvent(this, false);
            }
        }
    
    }
}
