using ByteSizeLib;
using CloudBackupL.CustomControllers;
using CloudBackupL.Utils;
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
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace CloudBackupL
{
    public partial class MainWindow : Form
    {
        DatabaseService databaseService;
        DropBoxController dropBoxController;
        PlanControl currentRunningPlan = null;
        static MainWindow instance;

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
            LoadCloudList();
        }

        //Button Add Cloud Clicked
        private void buttonAddCloud_Click(object sender, EventArgs e)
        {
            AddCloudWindow addCloudWindow = new AddCloudWindow();
            addCloudWindow.ShowDialog(); 
            LoadClouds();
            LoadCloudList();
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

        //Load Plans in the 2nd tab
        private void LoadCloudList()
        {
            listBoxClouds.Items.Clear();
            List<Cloud> clouds = databaseService.GetAllClouds();
            foreach (var c in clouds)
            {
                listBoxClouds.Items.Add(new ListItem(c.name, c.id));
            }
        }

        private void listBoxClouds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((System.Windows.Forms.ListBox)sender) == null) return;
            listViewBackupsInfo.Items.Clear();
            ListItem selectedItem = (ListItem)((System.Windows.Forms.ListBox)sender).SelectedItem;
            List<Backup> backups = databaseService.GetBackCloudBackups(selectedItem.Value);
            foreach(var b in backups)
            {
                Double size = Math.Round(ByteSize.FromBytes(b.size).MegaBytes, 3);
                Double compressedSize = Math.Round(ByteSize.FromBytes(b.compressedSize).MegaBytes, 3);
                TimeSpan runTimeTimeSpan = new TimeSpan(0, 0, 0, 0, (int)b.runTime);
                String runTime = (runTimeTimeSpan.Days > 0 ? runTimeTimeSpan.Days + " d - " : "") +
                    runTimeTimeSpan.Hours + " h : " + runTimeTimeSpan.Minutes + " m : " + runTimeTimeSpan.Seconds + " s";

                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(
                    new string [] {b.backupPlanName, b.date.ToString("yyyy/MM/dd h-m-s"),
                    size + " MB" , compressedSize + " MB", runTime});
                item.Tag = b.id;
                listViewBackupsInfo.Items.Add(item);
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
            ArchiveUtils archiveUtils = new ArchiveUtils();
            archiveUtils.RunArchiving(backgroundWorkerBackup, currentRunningPlan, instance);
        }

        private void backgroundWorkerBackup_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ReportProgress(e.ProgressPercentage);
      
        }

        private void backgroundWorkerBackup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadClouds();
            LoadPlans();
        }

        public BackgroundWorker BackgroundWorkerBackup
        {
            get { return this.backgroundWorkerBackup; }
            set { this.backgroundWorkerBackup = value; }
        }


    }
}
