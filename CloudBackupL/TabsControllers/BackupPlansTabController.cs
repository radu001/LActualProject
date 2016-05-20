using CloudBackupL.CustomControllers;
using CloudBackupL.Utils;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudBackupL.TabsControllers
{
    public class BackupPlansTabController
    {
        MainWindow mainWindowinstance;
        BackgroundWorker backgroundWorkerBackup;
        DatabaseService databaseService;
        PlanControl currentRunningPlan;
        BackupPlansTabController instance;
        DropBoxController dropboxController;

        public BackupPlansTabController()
        {
            instance = this;
            mainWindowinstance = MainWindow.instance;
            backgroundWorkerBackup = new BackgroundWorker();
            backgroundWorkerBackup.WorkerReportsProgress = true;
            backgroundWorkerBackup.DoWork += backgroundWorkerBackup_DoWork;
            backgroundWorkerBackup.ProgressChanged += backgroundWorkerBackup_ProgressChanged;
            backgroundWorkerBackup.RunWorkerCompleted += backgroundWorkerBackup_RunWorkerCompleted;
            databaseService = new DatabaseService();
            dropboxController = new DropBoxController();
        }

        //Load Plans in the 2nd tab
        public void LoadPlans()
        {
            mainWindowinstance.FlowLayoutPanelPlans.Controls.Clear();

            List<BackupPlan> plans = databaseService.GetAllPlans();
            mainWindowinstance.LabelTotalPlans.Text = "Total plans: " + plans.Count;
            foreach (var c in plans)
            {
                PlanControl control = new PlanControl();
                control.LabelBackupName.Text = c.name;
                control.LabelCloudName.Text = c.cloudName;
                control.LabelCreated.Text = c.creationDate.ToShortDateString();
                control.LabelCurrentStatus.Text = c.currentStatus;
                control.LabelFolderPath.Text = c.path;
                control.LabelLastDuration.Text = c.lastBackupDuration.ToString();
                control.LabelLastResult.Text = (c.lastResult ? "Succes" : "Failed");
                control.LabelScheduleTime.Text = c.scheduleTime.ToLongTimeString();
                control.LabelScheduleType.Text = c.scheduleType;
                control.LabelLastRun.Text = c.lastRun.ToLongTimeString();
                control.LabelPlanId.Text = c.id + "";
                control.OnUserControlDeletePlanButtonClicked += (s, e) => DeletePlanButtonClicked(s, e);
                control.OnUserControlRunNowButtonClicked += (s, e) => RunNowButtonClicked(s, e);
                mainWindowinstance.FlowLayoutPanelPlans.Controls.Add(control);
            }
        }

        //Button Delecte Plan Clicked
        private void DeletePlanButtonClicked(object sender, EventArgs e)
        {
            string id = ((PlanControl)sender).LabelPlanId.Text;
            databaseService.DeletePlan(Int32.Parse(id));
            MessageBox.Show("Plan deleted!");
            LoadPlans();
        }


        //Button Run Backup Clicked
        private void RunNowButtonClicked(object sender, EventArgs e)
        {
            currentRunningPlan = (PlanControl)sender;
            currentRunningPlan.LabelStatus.Text = "Status: Archiving...";
            
            if (backgroundWorkerBackup.IsBusy == false)
            {
                backgroundWorkerBackup.RunWorkerAsync();
            }
        }


        Stopwatch watch;
        Backup currentBackup;
        Cloud currentCloud;
        BackupPlan currentBackupPlan;
        long size;

        private void backgroundWorkerBackup_DoWork(object sender, DoWorkEventArgs e)
        {
            //start counting time
            watch = System.Diagnostics.Stopwatch.StartNew();
            string id = currentRunningPlan.LabelPlanId.Text;
            currentBackupPlan = databaseService.GetBackupPlan(Int32.Parse(id));
            currentCloud = databaseService.GetCloudByName(currentBackupPlan.cloudName);
            size = ArchiveUtils.GetDirectorySize(currentBackupPlan.path);
            ArchiveUtils archiveUtils = new ArchiveUtils();
            archiveUtils.RunArchiving(currentBackupPlan.path, Zip_SaveProgress);
            //
            Console.WriteLine("saving complete2");

            var compressedSize = ArchiveUtils.GetDirectorySize(AppDomain.CurrentDomain.BaseDirectory + "tmpFolder\\");
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            currentBackup = new Backup();
            currentBackup.date = DateTime.Now;
            currentBackup.targetPath = "/" + currentBackupPlan.name + "/" + date + "/";
            currentBackup.cloudId = currentCloud.id;
            currentBackup.backupPlanId = currentBackupPlan.id;
            currentBackup.size = size;
            currentBackup.compressedSize = compressedSize;
            currentBackup.backupPlanName = currentBackupPlan.name;


            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "tmpFolder\\");
            int i = 1;
            int total = di.GetFiles().Length;
            foreach (FileInfo file in di.GetFiles())
            {
                currentRunningPlan.ProgressBarArchiving.Invoke(new Action(() => currentRunningPlan.LabelStatus.Text = "Uploading file " + i + " of " + total));
                var tcs = new TaskCompletionSource<bool>();
                String targetPath = currentBackup.targetPath + file.Name;
                dropboxController.Upload(targetPath, currentCloud.token, file.FullName, Web_UploadProgressChanged, tcs);
                tcs.Task.Wait();
                i++;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            DirectoryInfo did = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "tmpFolder\\");
            foreach (FileInfo file in did.GetFiles())
            {
                file.Delete();
            }

            Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "tmpFolder\\");

            watch.Stop();
            currentBackup.runTime = watch.ElapsedMilliseconds;
            databaseService.InsertBackup(currentBackup);
        }

        private void Web_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            int p = (int)(e.BytesSent * 100 / e.TotalBytesToSend);
            if(p >= 0 && p <=100)
            {
                currentRunningPlan.ProgressBarArchiving.Invoke(new Action(() => currentRunningPlan.ProgressBarArchiving.Value = p));
            }   
        }

        private void Zip_SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead)
            {
                int progress = (int)((e.BytesTransferred * 100) / e.TotalBytesToTransfer);
                backgroundWorkerBackup.ReportProgress(progress);
            } 
            if(e.EventType == ZipProgressEventType.Saving_Completed)
            {
                currentRunningPlan.ProgressBarArchiving.Invoke(new Action(() => currentRunningPlan.ProgressBarArchiving.Value = 0));
                currentRunningPlan.ProgressBarArchiving.Invoke(new Action(() => currentRunningPlan.LabelStatus.Text = "Status: Uploading..."));
            }
            if(e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry)
            {
                currentRunningPlan.ProgressBarArchiving.Invoke(new Action(() => currentRunningPlan.LabelStatus.Text = "Status: Archiving file " + e.EntriesSaved + " of " + e.EntriesTotal));
            }
        }

        private void backgroundWorkerBackup_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            currentRunningPlan.ProgressBarArchiving.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerBackup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            currentRunningPlan.ProgressBarArchiving.Invoke(new Action(() => currentRunningPlan.LabelStatus.Text = "Status: Completed"));
            MainWindow.instance.homeTabController.LoadClouds();
            LoadPlans();
        }
    }
}
