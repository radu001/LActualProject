﻿using CloudBackupL.Clouds;
using CloudBackupL.CustomControllers;
using CloudBackupL.Models;
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
    public class HomeTabController
    {
        MainWindow mainWindowinstance;
        BackgroundWorker backgroundWorkerLoadClouds;
        DatabaseService databaseService;
        ICloud dropBoxController;
        ICloud boxController;
        ListView listViewPlansQueue;

        public HomeTabController()
        {
            mainWindowinstance = MainWindow.instance;
            boxController = new OneDriveController();
            databaseService = new DatabaseService();
            dropBoxController = new DropBoxController();
            this.listViewPlansQueue = mainWindowinstance.ListViewBackupQueue;
            mainWindowinstance.ButtonAddCloud.Click += buttonAddCloud_Click;
            mainWindowinstance.ButtonAddBackupPlan.Click += buttonAddBackupPlan_Click;
            backgroundWorkerLoadClouds = new BackgroundWorker();
            backgroundWorkerLoadClouds.WorkerReportsProgress = true;
            backgroundWorkerLoadClouds.ProgressChanged += backgroundWorkerLoadClouds_ProgressChanged;
            backgroundWorkerLoadClouds.DoWork += backgroundWorkerLoadClouds_DoWork;
        }

        //Button Add Cloud Clicked
        private void buttonAddCloud_Click(object sender, EventArgs e)
        {
            AddCloudWindow addCloudWindow = new AddCloudWindow();
            addCloudWindow.ShowDialog();
            MainWindow.instance.LoadAllControlls();
        }

        //Load Clouds, need internet
        public void LoadClouds()
        {
            if (backgroundWorkerLoadClouds.IsBusy == false)
            {
                mainWindowinstance.FlowLayoutPanelClouds.Controls.Clear();
                backgroundWorkerLoadClouds.RunWorkerAsync();
            }
        }

        //Load Clouds in another thread
        private void backgroundWorkerLoadClouds_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Cloud> clouds = databaseService.GetAllClouds();
            foreach (var c in clouds)
            {

                CloudControl control = new CloudControl();
                switch (c.cloudType)
                {
                    case "dropbox":
                        try
                        {
                            CloudUserInfo cloudUserInfoDropBox = dropBoxController.GetAccountInfo(c.token);
                            control.LabelTotalSpace.Text = MyUtils.GetFormatedSpaceInGB(cloudUserInfoDropBox.total_space) + " GB";
                            control.LabelFreeSpace.Text = MyUtils.GetFormatedSpaceInGB(cloudUserInfoDropBox.free_space) + " GB";
                            int progress = (int)((cloudUserInfoDropBox.used_space * 100) / cloudUserInfoDropBox.total_space);
                            control.SetAvaible(progress);
                        }
                        catch (Exception)
                        {
                            Logger.Log("Error loading clouds, please check internet connection!");
                        }
                        control.LabelCloudName.Text = c.name;
                        control.PictureBoxCloudImage.Image = mainWindowinstance.ImageListClouds.Images[0];
                        control.LabelId.Text = c.id.ToString();
                        break;
                    case "box":
                        try
                        {
                            CloudUserInfo cloudUserInfoBox = boxController.GetAccountInfo(c.token);
                            control.LabelTotalSpace.Text = MyUtils.GetFormatedSpaceInGB(cloudUserInfoBox.total_space) + " GB";
                            control.LabelFreeSpace.Text = MyUtils.GetFormatedSpaceInGB(cloudUserInfoBox.free_space) + " GB";
                            int progress = (int)((cloudUserInfoBox.used_space * 100) / cloudUserInfoBox.total_space);
                            control.SetAvaible(progress);
                        }
                        catch (Exception)
                        {
                            Logger.Log("Error loading clouds, please check internet connection!");
                        }
                        control.LabelCloudName.Text = c.name;
                        control.PictureBoxCloudImage.Image = mainWindowinstance.ImageListClouds.Images[1];
                        control.LabelId.Text = c.id.ToString();
                        break;
                }
                control.OnUserControlDeleteCloudButtonClicked += (s, eve) => DeleteCloudButtonClicked(s, eve);
                backgroundWorkerLoadClouds.ReportProgress(1, control);
            }
        }

        private void backgroundWorkerLoadClouds_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            mainWindowinstance.FlowLayoutPanelClouds.Controls.Add((CloudControl)e.UserState);
        }

        //Button delete cloud clicked
        private void DeleteCloudButtonClicked(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove this cloud?", "Remove Cloud", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string id = ((CloudControl)sender).LabelId.Text;

                if (databaseService.CanDeleteCloud(id))
                {
                    databaseService.DeleteCloud(id);
                    Logger.Log("The cloud " + ((CloudControl)sender).LabelCloudName.Text + " was deleted!");
                    LoadClouds();
                }
                else
                {
                    MessageBox.Show("Can't delete this cloud, there are plans who use this cloud.", "Error", MessageBoxButtons.OK);
                }
                MainWindow.instance.LoadAllControlls();
            }
        }


        //Button Add Plan Clicked
        private void buttonAddBackupPlan_Click(object sender, EventArgs e)
        {
            ManageBackupPlanWindow addBackupPlanWindow = new ManageBackupPlanWindow();
            addBackupPlanWindow.ShowDialog();
            mainWindowinstance.backupPlansTabController.LoadPlans();
            mainWindowinstance.myBackupsTabController.LoadBackupPlansList();
            MainWindow.instance.LoadAllControlls();
        }

        Timer executeBackupTimer = new Timer();
        public void LoadQueueList()
        {
            listViewPlansQueue.Items.Clear();
            List<BackupPlan> plans = databaseService.GetAllPlans();
            plans.Sort(new Comparison<BackupPlan>((x, y) => DateTime.Compare(x.nextExecution, y.nextExecution)));
            BackupPlan planToQueue = null;
            foreach(var plan in plans)
            {
                if (!plan.scheduleType.Equals("Manual"))
                {
                    if (planToQueue == null)
                        planToQueue = plan;
                    listViewPlansQueue.Items.Add(new ListViewItem(new string[]
                    {
                        plan.name,
                        plan.nextExecution.ToString()
                    }));    
                }
            }

            if (planToQueue != null)
            {
                if(DateTime.Compare(planToQueue.nextExecution, DateTime.Now) < 0)
                {
                    //execute now
                    ExecuteBackup(planToQueue.id);
                } else
                {      
                    //set timer
                    int timerTime = (int)(planToQueue.nextExecution - DateTime.Now).TotalMilliseconds;
                    if (timerTime < 0) timerTime = 2000;
                    executeBackupTimer.Stop();
                    executeBackupTimer.Interval = timerTime;
                    executeBackupTimer.Tick += (sender, e) => ExecuteBackupTimer_Tick(sender, e, planToQueue.id);
                    executeBackupTimer.Enabled = true;
                }
            } 
        }

        private void ExecuteBackupTimer_Tick(object sender, EventArgs e, int planId)
        {
            ExecuteBackup(planId);
        }

        private void ExecuteBackup(int planId)
        {
            bool isExecuted = false;
            foreach(var p in mainWindowinstance.FlowLayoutPanelPlans.Controls)
            {
                PlanControl planControl = (PlanControl)p;
                if(Int32.Parse(planControl.LabelPlanId.Text) == planId)
                {
                    Console.WriteLine("start backup from schedule");
                    mainWindowinstance.backupPlansTabController.PerformBackup(planControl,true);
                    isExecuted = true;
                }
            }
            //in case a plan was deleted
            if (!isExecuted) LoadQueueList();
        }
    }
}
