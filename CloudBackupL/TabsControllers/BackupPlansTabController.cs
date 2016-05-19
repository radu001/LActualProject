using CloudBackupL.CustomControllers;
using CloudBackupL.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public void ReportProgress(int value)
        {
            
            if (value == 110)
            {
                currentRunningPlan.BeginInvoke(new Action(() => currentRunningPlan.ProgressBarArchiving.Value = 100));
                currentRunningPlan.BeginInvoke(new Action(() => currentRunningPlan.LabelStatus.Text = "Status: Completed"));
            }
            else if (value == 200)
            {
                currentRunningPlan.BeginInvoke(new Action(() => currentRunningPlan.ProgressBarArchiving.Value = 0));
                currentRunningPlan.BeginInvoke(new Action(() => currentRunningPlan.LabelStatus.Text = "Status: Uploading..."));
            }
            else if (value != 200)
            {
                currentRunningPlan.BeginInvoke(new Action(() => currentRunningPlan.ProgressBarArchiving.Value = value));
            }
            
        }

        private void backgroundWorkerBackup_DoWork(object sender, DoWorkEventArgs e)
        {
            ArchiveUtils archiveUtils = new ArchiveUtils();
            archiveUtils.RunArchiving(backgroundWorkerBackup, currentRunningPlan, instance);
        }

        private void backgroundWorkerBackup_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            currentRunningPlan.ProgressBarArchiving.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerBackup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mainWindowinstance.homeTabController.LoadClouds();
            LoadPlans();
        }
    }
}
