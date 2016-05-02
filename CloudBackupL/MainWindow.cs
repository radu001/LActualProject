using ByteSizeLib;
using CloudBackupL.CustomControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CloudBackupL
{
    public partial class MainWindow : Form
    {
        DatabaseService databaseService;
        DropBoxController dropBoxController;
        public MainWindow()
        {
            InitializeComponent();
            databaseService = new DatabaseService();
            dropBoxController = new DropBoxController();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //load clouds
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
                        break;
                }
                //control.Location = new System.Drawing.Point(0, y);
                //y += 110;
                flowLayoutPanelClouds.Controls.Add(control);
                //listViewClouds.Controls.Add(control);
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonAddCloud_Click(object sender, EventArgs e)
        {
            AddCloudWindow addCloudWindow = new AddCloudWindow();
            addCloudWindow.ShowDialog();          
        }

        private void buttonAddBackupPlan_Click(object sender, EventArgs e)
        {
            AddBackupPlanWindow addBackupPlanWindow = new AddBackupPlanWindow();
            addBackupPlanWindow.ShowDialog();
        }
    }
}
