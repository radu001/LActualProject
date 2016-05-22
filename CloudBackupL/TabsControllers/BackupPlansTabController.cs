using CloudBackupL.BackupActions;
using CloudBackupL.CustomControllers;
using CloudBackupL.Utils;
using Ionic.Zip;
using Nemiro.OAuth;
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
                control.LabelFolderPath.Text = c.path;
                control.LabelLastDuration.Text = c.lastBackupDuration.ToString();
                control.LabelLastResult.Text = (c.lastResult ? "Succes" : "Failed");
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
                PlanControl planControl = (PlanControl)sender;
                Backup lastBackup = databaseService.GetLastBackup(Int32.Parse(planControl.LabelPlanId.Text));
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                DialogResult dialogResult = folderBrowser.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    DownloadBackupAction downloadBackupAction = new DownloadBackupAction(lastBackup, planControl.LabelStatus, planControl.ProgressBarArchiving, folderBrowser.SelectedPath, DownloadCompleteEvent);
                    downloadBackupAction.StartDownloadBackupAction();
                }
            }
        }

        private void DownloadCompleteEvent(object sender, bool e)
        {
            Console.WriteLine("Download complete");
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
            PlanControl planControl = (PlanControl)sender;
            BackupPlan backupPlan = databaseService.GetBackupPlan(Int32.Parse(planControl.LabelPlanId.Text));
            UploadBackupAction uploadBackupAction = new UploadBackupAction(backupPlan, planControl.LabelStatus, planControl.ProgressBarArchiving, BackupCompleteEvent);
            uploadBackupAction.StartBackupAction();
        }

        private void BackupCompleteEvent(object sender, bool e)
        {
            MainWindow.instance.homeTabController.LoadClouds();
            LoadPlans();
        }

    }
}
