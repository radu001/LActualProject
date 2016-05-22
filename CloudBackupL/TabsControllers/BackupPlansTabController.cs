using CloudBackupL.BackupActions;
using CloudBackupL.CustomControllers;
using CloudBackupL.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CloudBackupL.TabsControllers
{
    public class BackupPlansTabController
    {
        DatabaseService databaseService;
        FlowLayoutPanel flowLayoutPanelPlans;
        Label labelTotalPlans;

        public BackupPlansTabController()
        {
            this.flowLayoutPanelPlans = MainWindow.instance.FlowLayoutPanelPlans;
            this.labelTotalPlans = MainWindow.instance.LabelTotalPlans;
            databaseService = new DatabaseService();
        }

        //Load Plans in the 2nd tab
        public void LoadPlans()
        {
            flowLayoutPanelPlans.Controls.Clear();

            List<BackupPlan> plans = databaseService.GetAllPlans();
            labelTotalPlans.Text = "Total plans: " + plans.Count;
            foreach (var c in plans)
            {
                PlanControl control = new PlanControl();
                control.LabelBackupName.Text = c.name;
                control.LabelCloudName.Text = c.cloudName;
                control.LabelCreated.Text = c.creationDate.ToShortDateString();
                control.LabelCurrentStatus.Text = c.currentStatus;
                Backup backup = databaseService.GetLastBackup(c.id);
                if(backup != null)
                {
                    TimeSpan runTimeTimeSpan = new TimeSpan(0, 0, 0, 0, (int)backup.runTime);
                    String runTime = (runTimeTimeSpan.Days > 0 ? runTimeTimeSpan.Days + " d - " : "") +
                        runTimeTimeSpan.Hours + " h : " + runTimeTimeSpan.Minutes + " m : " + runTimeTimeSpan.Seconds + " s";
                    control.LabelLastDuration.Text = runTime;
                }
                control.LabelFolderPath.Text = c.path;
                control.LabelLastResult.Text = (c.lastResult ? "Succes" : "");
                control.LabelScheduleTime.Text = c.scheduleTime.ToLongTimeString();
                control.LabelScheduleType.Text = c.scheduleType;
                control.LabelLastRun.Text = c.lastRun.ToLongTimeString();
                control.LabelPlanId.Text = c.id + "";
                control.OnUserControlDeletePlanButtonClicked += (s, e) => DeletePlanButtonClicked(s, e);
                control.OnUserControlRunNowButtonClicked += (s, e) => RunNowButtonClicked(s, e);
                control.OnUserControlDownloadButtonClicked += (s, e) => DownloadButtonClicked(s, e);
                flowLayoutPanelPlans.Controls.Add(control);
            }
        }

        private void DownloadButtonClicked(object sender, EventArgs e)
        {
            if(!MainWindow.isActiveDownloadOperation)
            {
                MainWindow.isActiveDownloadOperation = true;
                //to do
                string password = ArchiveUtils.Encript("mypassword");
                PlanControl planControl = (PlanControl)sender;
                Backup lastBackup = databaseService.GetLastBackup(Int32.Parse(planControl.LabelPlanId.Text));
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                DialogResult dialogResult = folderBrowser.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    DownloadBackupAction downloadBackupAction = new DownloadBackupAction(lastBackup, planControl.LabelStatus, planControl.ProgressBarArchiving, folderBrowser.SelectedPath, DownloadCompleteEvent, password);
                    downloadBackupAction.StartDownloadBackupAction();
                }
            }
        }

        private void DownloadCompleteEvent(object sender, bool e)
        {
            Console.WriteLine("Download complete");
            MainWindow.isActiveDownloadOperation = false;
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
            if (!MainWindow.isActiveUploadOperation)
            {
                MainWindow.isActiveUploadOperation = true;
                PlanControl planControl = (PlanControl)sender;
                //to do
                string password = ArchiveUtils.Encript("mypassword");
                BackupPlan backupPlan = databaseService.GetBackupPlan(Int32.Parse(planControl.LabelPlanId.Text));
                UploadBackupAction uploadBackupAction = new UploadBackupAction(backupPlan, planControl.LabelStatus, planControl.ProgressBarArchiving, BackupCompleteEvent, password);
                uploadBackupAction.StartBackupAction();
            }
        }

        private void BackupCompleteEvent(object sender, bool e)
        {
            MainWindow.isActiveUploadOperation = false;
            MainWindow.instance.homeTabController.LoadClouds();
            MainWindow.instance.myBackupsTabController.LoadBackupPlansList();
            LoadPlans();
        }

    }
}
