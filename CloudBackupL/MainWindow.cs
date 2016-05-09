using ByteSizeLib;
using CloudBackupL.CustomControllers;
using Dropbox.Api;
using Dropbox.Api.Files;
using Ionic.Zip;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudBackupL
{
    public partial class MainWindow : Form
    {
        DatabaseService databaseService;
        DropBoxController dropBoxController;
        PlanControl currentRunningPlan = null;
        MainWindow instance;

        //Main Window Constructor
        public MainWindow()
        {
            instance = this;
            InitializeComponent();
            databaseService = new DatabaseService();
            dropBoxController = new DropBoxController();
            backgroundWorkerLoadClouds.WorkerReportsProgress = true;
            
        }

        //Main Window Load
        private void MainWindow_Load(object sender, EventArgs e)
        {
            LoadClouds();
            LoadPlans();
        }

        //Button Add Cloud Clicked
        private void buttonAddCloud_Click(object sender, EventArgs e)
        {
            AddCloudWindow addCloudWindow = new AddCloudWindow();
            addCloudWindow.ShowDialog(); 
            LoadClouds();
        }

        //Button Add Cloud Clicked
        private void buttonAddBackupPlan_Click(object sender, EventArgs e)
        {
            AddBackupPlanWindow addBackupPlanWindow = new AddBackupPlanWindow();
            addBackupPlanWindow.ShowDialog();
            LoadPlans();
        }

        //Load Plans in the 2nd tab
        private void LoadPlans()
        {
            flowLayoutPanelPlans.Controls.Clear();

            List<BackupPlan> plans = databaseService.GetAllPlans();
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
                flowLayoutPanelPlans.Controls.Add(control);
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

        //Load Clouds, need internet
        private void LoadClouds()
        {
            if (backgroundWorkerLoadClouds.IsBusy == false)
            {
                flowLayoutPanelClouds.Controls.Clear();
                backgroundWorkerLoadClouds.RunWorkerAsync();
            }
        }

        //Load Clouds in another thread
        private void backgroundWorkerLoadClouds_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Cloud> clouds = databaseService.GetAllClouds();
            int y = 0;
            foreach (var c in clouds)
            {

                CloudControl control = new CloudControl();
                switch (c.cloudType)
                {
                    case "dropbox":
                        double totalSpaceGB = dropBoxController.GetTotalSpaceInGB(c.token);
                        double freeSpaceGB = dropBoxController.GetFreeSpaceInGB(c.token);

                        control.LabelTotalSpace.Text = Math.Round(totalSpaceGB, 3) + " GB";
                        control.LabelFreeSpace.Text = Math.Round(freeSpaceGB, 3) + " GB";

                        control.LabelCloudName.Text = c.name;
                        control.PictureBoxCloudImage.Image = imageListClouds.Images[0];
                        control.LabelId.Text = c.id;
                        break;
                }
                control.OnUserControlDeleteCloudButtonClicked += (s, eve) => DeleteCloudButtonClicked(s, eve);
                backgroundWorkerLoadClouds.ReportProgress(1, control);
            }
        }

        private void backgroundWorkerLoadClouds_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            flowLayoutPanelClouds.Controls.Add((CloudControl)e.UserState);
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
                    LoadClouds();
                } else
                {
                    MessageBox.Show("Can't delete this cloud, there are plans who use this cloud.", "Error", MessageBoxButtons.OK);
                }
            }  
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
            System.Console.WriteLine("progress: " + value);
            if (value == 110)
            {
                currentRunningPlan.ProgressBarArchiving.Value = 100;
                currentRunningPlan.LabelStatus.Text = "Status: Completed";
            }
            else if (value == 200)
            {
                currentRunningPlan.ProgressBarArchiving.Value = 0;
                currentRunningPlan.LabelStatus.Text = "Status: Uploading...";
            }
            else if (value != 200)
            {
                currentRunningPlan.ProgressBarArchiving.Value = value;
            }
        }

        private void backgroundWorkerBackup_DoWork(object sender, DoWorkEventArgs e)
        {
            string id = currentRunningPlan.LabelPlanId.Text;
            BackupPlan plan = databaseService.GetBackupPlan(Int32.Parse(id));
            Cloud cloud = databaseService.GetCloudByName(plan.cloudName);
            using (ZipFile zip = new ZipFile())
            {
                zip.SaveProgress += Zip_SaveProgress;
                zip.AddDirectory(@plan.path, "Backup1");
                zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                zip.Save(AppDomain.CurrentDomain.BaseDirectory + "\\temp.zip");
            }
            Task.Run(async () => dropBoxController.Upload(AppDomain.CurrentDomain.BaseDirectory + "temp.zip", "/temp.zip", new DropboxClient(cloud.token), this)).Wait(); ;
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
                backgroundWorkerBackup.ReportProgress(200);
            }
        }

        private void backgroundWorkerBackup_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ReportProgress(e.ProgressPercentage);
      
        }

        private void backgroundWorkerBackup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        public BackgroundWorker BackgroundWorkerBackup
        {
            get { return this.backgroundWorkerBackup; }
            set { this.backgroundWorkerBackup = value; }
        }
    }
}
