using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nemiro.OAuth;
using System.IO;
using System.Net;
using CloudBackupL.Utils;
using Ionic.Zip;

namespace CloudBackupL.BackupActions
{

    class DownloadBackupAction
    {
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        string tempDownloadZipFolder = AppDomain.CurrentDomain.BaseDirectory + "tmpZipDownloadFolder\\";
        DatabaseService databaseService = new DatabaseService();
        BackupPlan backupPlan;
        Backup backup;
        Label labelStatus;
        ProgressBar progressBar;
        ICloud cloudController;
        Cloud cloud;
        EventHandler<Boolean> downloadCompleteEvent;
        string downloadPath;
        RequestResult requestFilesListResult;
        TaskCompletionSource<bool> tastWaitDownload;

        public DownloadBackupAction(Backup backup, Label labelStatus, ProgressBar progressBar, string downloadPath, EventHandler<Boolean> downloadCompleteEvent)
        {
            this.backup = backup;
            this.labelStatus = labelStatus;
            this.progressBar = progressBar;
            this.downloadPath = downloadPath;
            this.downloadCompleteEvent = downloadCompleteEvent;
            backupPlan = databaseService.GetBackupPlan(backup.backupPlanId);
            cloud = databaseService.GetCloud(backupPlan.cloudId);
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            if(cloud.cloudType.Equals("dropbox"))
            {
                cloudController = new DropBoxController();
            }
        }

        public void StartDownloadBackupAction()
        {
            if (backgroundWorker.IsBusy == false)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }
        
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            cloudController.GetFilesList(cloud.token, LoadFilesCallback, backup.targetPath);
            tastWaitDownload = new TaskCompletionSource<bool>();
            tastWaitDownload.Task.Wait();

            Directory.CreateDirectory(tempDownloadZipFolder);
            int i = 1;
            int totalFiles = requestFilesListResult["contents"].Count;
            foreach (UniValue file in requestFilesListResult["contents"])
            {
                var tcs = new TaskCompletionSource<bool>();
                labelStatus.Invoke(new Action(() => labelStatus.Text = "Downloading file " + i + " of " + totalFiles));
                string fileName = Path.GetFileName(file["path"].ToString());

                cloudController.Download(file["path"].ToString(), cloud.token, tempDownloadZipFolder + fileName, Web_DownloadProgressChanged, tcs);
                tcs.Task.Wait();
                i++;
            }
            labelStatus.Invoke(new Action(() => labelStatus.Text = ""));
            progressBar.Invoke(new Action(() => progressBar.Value = 0));

            //now save file to directory
            ArchiveUtils.ExtractZip(tempDownloadZipFolder, downloadPath, Zip_ExtractProgress);

            labelStatus.Invoke(new Action(() => labelStatus.Text = ""));
            progressBar.Invoke(new Action(() => progressBar.Value = 0));
        }

        private void Web_DownloadProgressChanged(object sender, object e)
        {
            DownloadProgressChangedEventArgs args = (DownloadProgressChangedEventArgs)e;
            progressBar.Invoke(new Action(() => progressBar.Value = args.ProgressPercentage));
        }

        private void LoadFilesCallback(Object result)
        {
            RequestResult requestResult = (RequestResult)result;
            if (requestResult.StatusCode == 200)
            {
                requestFilesListResult = requestResult;
                tastWaitDownload.TrySetResult(true);
            } else
            {
                tastWaitDownload.TrySetException(new Exception("Error getting file list"));
            }
        }
        static int currentEntryNr = 1;
        private void Zip_ExtractProgress(object sender, object e)
        {
            ZipProgressEventArgs args = (ZipProgressEventArgs)e;
            if (args.EventType == ZipProgressEventType.Extracting_EntryBytesWritten)
            {
                int progress = (int)((args.BytesTransferred * 100) / args.TotalBytesToTransfer);
                progressBar.Invoke(new Action(() => progressBar.Value = progress));
            }
            if (args.EventType == ZipProgressEventType.Extracting_BeforeExtractEntry)
            {
                labelStatus.Invoke(new Action(() => labelStatus.Text = "Status: Extracting file " + currentEntryNr + " of " + args.EntriesTotal));
                currentEntryNr++;
            }
            if(args.EventType == ZipProgressEventType.Extracting_AfterExtractAll)
            {
                currentEntryNr = 1;
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error == null)
            {
                downloadCompleteEvent(this, true);
            } else
            {
                downloadCompleteEvent(this, false);
            }    
        }

    }
}
