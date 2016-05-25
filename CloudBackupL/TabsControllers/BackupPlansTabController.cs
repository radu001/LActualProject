using CloudBackupL.BackupActions;
using CloudBackupL.Clouds;
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
                    control.LabelLastRun.Text = c.lastRun.ToLongTimeString();
                }
                else
                {
                    control.DisableActionsWhenNoBackup();
                }
                control.LabelFolderPath.Text = c.path;
                control.LabelLastResult.Text = (c.lastResult ? "Succes" : "");
                control.LabelScheduleTime.Text = c.scheduleTime.ToLongTimeString();
                control.LabelScheduleType.Text = c.scheduleType;
                control.LabelPlanId.Text = c.id + "";
                control.OnUserControlDeletePlanButtonClicked += (s, e) => DeletePlanButtonClicked(s, e);
                control.OnUserControlRunNowButtonClicked += (s, e) => RunNowButtonClicked(s, e);
                control.OnUserControlDownloadButtonClicked += (s, e) => DownloadButtonClicked(s, e);
                control.OnUserControlEditButtonClicked += (s, e) => EditButtonClicked(s, e);
                control.OnUserControlViewHystoryButtonClicked += (s, e) => ViewHistoryButtonClicked(s, e);
                control.OnUserControlRestoreButtonClicked += (s, e) => RestoreButtonClicked(s, e);
                flowLayoutPanelPlans.Controls.Add(control);
            }
        }


        public void DisableActions()
        {
            foreach(PlanControl planControl in flowLayoutPanelPlans.Controls)
            {
                planControl.DisableActions();
            }
        }


        private void RestoreButtonClicked(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to restore files from last backup?You will lose your current work.", "Restore Backup Plan", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (!MainWindow.isActiveDownloadOperation)
                {
                    MainWindow.isActiveDownloadOperation = true;
                    DisableActions();
                    //to do
                    string password = ArchiveUtils.Encript("mypassword");
                    PlanControl planControl = (PlanControl)sender;
                    Cloud cloud = databaseService.GetCloudByName(planControl.LabelCloudName.Text);    
                    Backup lastBackup = databaseService.GetLastBackup(Int32.Parse(planControl.LabelPlanId.Text));

                    DownloadBackupAction downloadBackupAction = new DownloadBackupAction(cloud, lastBackup.targetPath, planControl.LabelStatus, planControl.ProgressBarArchiving, planControl.LabelFolderPath.Text, DownloadCompleteEvent, password, true);
                    downloadBackupAction.StartDownloadBackupAction();
                }
            }
        }

        private void ViewHistoryButtonClicked(object sender, EventArgs e)
        {
            string backupPlanName = ((PlanControl)sender).LabelBackupName.Text;
            MainWindow.instance.ListBoxBackupPlans.SelectedIndex = MainWindow.instance.ListBoxBackupPlans.FindStringExact(backupPlanName);
            MainWindow.instance.TabControl.SelectedIndex = 2;
        }

        private void EditButtonClicked(object sender, EventArgs e)
        {
            int planId = Int32.Parse(((PlanControl)sender).LabelPlanId.Text);
            ManageBackupPlanWindow manageBackupPlanWindow = new ManageBackupPlanWindow(planId);
            manageBackupPlanWindow.ShowDialog();
        }

        private void DownloadButtonClicked(object sender, EventArgs e)
        {
            if(!MainWindow.isActiveDownloadOperation)
            {
                MainWindow.isActiveDownloadOperation = true;
                //to do
                DisableActions();
                string password = ArchiveUtils.Encript("mypassword");
                PlanControl planControl = (PlanControl)sender;
                Backup lastBackup = databaseService.GetLastBackup(Int32.Parse(planControl.LabelPlanId.Text));
                Cloud cloud = databaseService.GetCloudByName(planControl.LabelCloudName.Text);
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                DialogResult dialogResult = folderBrowser.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    DownloadBackupAction downloadBackupAction = new DownloadBackupAction(cloud ,lastBackup.targetPath, planControl.LabelStatus, planControl.ProgressBarArchiving, folderBrowser.SelectedPath, DownloadCompleteEvent, password, false);
                    downloadBackupAction.StartDownloadBackupAction();
                } else
                {
                    MainWindow.isActiveDownloadOperation = false;
                }
            }
        }

        private void DownloadCompleteEvent(object sender, bool e)
        {
            LoadPlans();
            Console.WriteLine("Download complete");
            MainWindow.isActiveDownloadOperation = false;
        }

        //Button Delecte Plan Clicked
        private void DeletePlanButtonClicked(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to delete this plan?", "Delete Backup Plan", MessageBoxButtons.YesNo);
            if(dialog == DialogResult.Yes)
            {
                string id = ((PlanControl)sender).LabelPlanId.Text;
                DialogResult dialog2 = MessageBox.Show("Do you want to delete cloud files too?", "Delete Backup Plan", MessageBoxButtons.YesNo);
                if (dialog2 == DialogResult.Yes)
                {
                    BackupPlan backupPlan = databaseService.GetBackupPlan(Int32.Parse(id));
                    Cloud cloud = databaseService.GetCloud(backupPlan.cloudId);
                    if(cloud.cloudType.Equals("dropbox"))
                    {
                        new DropBoxController().DeleteFolder(cloud.token, DeleteFolderCompelte, backupPlan.name);
                    } else
                    {
                        new OneDriveController().DeleteFolder(cloud.token, DeleteFolderCompelte, backupPlan.name);
                    }

                }
                databaseService.DeletePlan(Int32.Parse(id));
                LoadPlans();
                MainWindow.instance.myBackupsTabController.LoadBackupPlansList();
                MessageBox.Show("Plan deleted!");
            } 
        }

        private void DeleteFolderCompelte(object sender, object args)
        {

        }


        //Button Run Backup Clicked
        private void RunNowButtonClicked(object sender, EventArgs e)
        {
            if (!MainWindow.isActiveUploadOperation)
            {
                MainWindow.isActiveUploadOperation = true;
                DisableActions();
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
            MainWindow.instance.manualWorkTabController.LoadCloudList();
            LoadPlans();
        }

    }
}
