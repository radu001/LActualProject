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
        Label labelStatusMain = MainWindow.instance.LabelMainStatus;
        ProgressBar progressBarMain = MainWindow.instance.ProgressBarMain;
        ICloud cloudController;
        Cloud cloud;
        EventHandler<Boolean> backupCompleteEvent;
        string password;
        bool isFromSchedule;


        public UploadBackupAction(BackupPlan backupPlan, Label labelStatus, ProgressBar progressBar, EventHandler<Boolean> backupCompleteEvent, string password, bool isFromSchedule)
        {
            this.isFromSchedule = isFromSchedule;
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
                Logger.Log("Starting backup " + backupPlan.name + ", please don't interrupt!", ToolTipIcon.Info);
                backgroundWorker.RunWorkerAsync();
            }
        }


        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //start counting time
            Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            //clear temp directory
            MyUtils.DeleteDirectory(tempUploadZipFolder);
            long size = MyUtils.GetDirectorySize(backupPlan.path);
            MyUtils.ZipFiles(backupPlan.path, tempUploadZipFolder, Zip_SaveProgress, password);
            var compressedSize = MyUtils.GetDirectorySize(tempUploadZipFolder);
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
                progressBarMain.Invoke(new Action(() => progressBarMain.Value = 0));
                labelStatus.Invoke(new Action(() => labelStatus.Text = "Uploading file " + i + " of " + total));
                labelStatusMain.Invoke(new Action(() => labelStatusMain.Text = "Uploading file " + i + " of " + total));
                var tcs = new TaskCompletionSource<bool>();
                String targetPath = currentBackup.targetPath + file.Name;
                cloudController.Upload(targetPath, cloud.token, file.FullName, Web_UploadProgressChanged, tcs);
                tcs.Task.Wait();
                i++;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            MyUtils.DeleteDirectory(tempUploadZipFolder);

            watch.Stop();
            if(backupPlan.overrideBackup)
            {
                Backup lastBackup = databaseService.GetLastBackup(backupPlan.id);
                if (lastBackup != null)
                {
                    cloudController.DeleteFolder(cloud.token, DeleteFolderCompelte, lastBackup.targetPath);
                    databaseService.DeleteBackup(lastBackup.id);
                }
            }
            if (!backupPlan.scheduleType.Equals("Manual"))
                backupPlan.nextExecution = MyUtils.GetNextExecution(backupPlan);
            databaseService.UpdateBackupPlan(backupPlan);
            currentBackup.runTime = watch.ElapsedMilliseconds;
            databaseService.InsertBackup(currentBackup);
        }

        private void DeleteFolderCompelte(object sender, object args)
        {

        }


        private void Zip_SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead)
            {
                int progress = (int)((e.BytesTransferred * 100) / e.TotalBytesToTransfer);
                progressBar.Invoke(new Action(() => progressBar.Value = progress));
                progressBarMain.Invoke(new Action(() => progressBarMain.Value = progress));
            }
            else if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry)
            {
                labelStatus.Invoke(new Action(() => labelStatus.Text = "Status: Archiving file " + e.EntriesSaved + " of " + e.EntriesTotal));
                labelStatusMain.Invoke(new Action(() => labelStatusMain.Text = "Status: Archiving file " + e.EntriesSaved + " of " + e.EntriesTotal));
            }
        }

        private void Web_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            int progress = (int)(e.BytesSent * 100 / e.TotalBytesToSend);
            progressBar.Invoke(new Action(() => progressBar.Value = progress));
            progressBarMain.Invoke(new Action(() => progressBarMain.Value = progress));
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error == null)
            {
                Logger.Log("Backup " + backupPlan.name + " successful!", ToolTipIcon.Info);
                backupCompleteEvent(this, true);
            } else
            {
                if(isFromSchedule)
                {
                    Logger.Log("Something went wrong on backup " + backupPlan.name+ "! Backup from schedule postponed to 5 minutes.", ToolTipIcon.Error);
                    backupPlan.nextExecution = backupPlan.nextExecution.AddMinutes(new DatabaseService().GetSettings().postpone);
                    databaseService.UpdateBackupPlan(backupPlan);
                }
                else
                {
                    Logger.Log("Something went wrong on backup " + backupPlan.name + "!", ToolTipIcon.Error);
                }      
                backupCompleteEvent(this, false);
            }
        }
    
    }
}
