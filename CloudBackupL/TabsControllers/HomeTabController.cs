using CloudBackupL.Clouds;
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

        public HomeTabController()
        {
            mainWindowinstance = MainWindow.instance;
            boxController = new OneDriveController();
            databaseService = new DatabaseService();
            dropBoxController = new DropBoxController();
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
            LoadClouds();
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
                        CloudUserInfo cloudUserInfoDropBox = dropBoxController.GetAccountInfo(c.token);
                        control.LabelTotalSpace.Text = ArchiveUtils.GetFormatedSpaceInGB(cloudUserInfoDropBox.total_space) + " GB";
                        control.LabelFreeSpace.Text = ArchiveUtils.GetFormatedSpaceInGB(cloudUserInfoDropBox.free_space) + " GB";
                        control.LabelCloudName.Text = c.name;
                        control.PictureBoxCloudImage.Image = mainWindowinstance.ImageListClouds.Images[0];
                        control.LabelId.Text = c.id.ToString();
                        break;
                    case "box":
                        CloudUserInfo cloudUserInfoBox = boxController.GetAccountInfo(c.token);
                        control.LabelTotalSpace.Text = ArchiveUtils.GetFormatedSpaceInGB(cloudUserInfoBox.total_space) + " GB";
                        control.LabelFreeSpace.Text = ArchiveUtils.GetFormatedSpaceInGB(cloudUserInfoBox.free_space) + " GB";
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
                    LoadClouds();
                }
                else
                {
                    MessageBox.Show("Can't delete this cloud, there are plans who use this cloud.", "Error", MessageBoxButtons.OK);
                }
            }
        }


        //Button Add Plan Clicked
        private void buttonAddBackupPlan_Click(object sender, EventArgs e)
        {
            ManageBackupPlanWindow addBackupPlanWindow = new ManageBackupPlanWindow();
            addBackupPlanWindow.ShowDialog();
            mainWindowinstance.backupPlansTabController.LoadPlans();
            mainWindowinstance.myBackupsTabController.LoadBackupPlansList();
        }
    }
}
