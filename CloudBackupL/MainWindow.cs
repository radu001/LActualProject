using ByteSizeLib;
using CloudBackupL.CustomControllers;
using Dropbox.Api;
using Dropbox.Api.Files;
using Ionic.Zip;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
            databaseService = new DatabaseService();
            dropBoxController = new DropBoxController();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //load clouds
            LoadClouds();
            //load plans
            LoadPlans();
        }



        private void buttonAddCloud_Click(object sender, EventArgs e)
        {
            AddCloudWindow addCloudWindow = new AddCloudWindow();
            addCloudWindow.ShowDialog(); 
            LoadClouds();
        }

        private void buttonAddBackupPlan_Click(object sender, EventArgs e)
        {
            AddBackupPlanWindow addBackupPlanWindow = new AddBackupPlanWindow();
            addBackupPlanWindow.ShowDialog();
            LoadPlans();
        }

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

        private void DeletePlanButtonClicked(object sender, EventArgs e)
        {
            string id = ((PlanControl)sender).LabelPlanId.Text;
            databaseService.DeletePlan(Int32.Parse(id));
            MessageBox.Show("Plan deleted!");
            LoadPlans();
        }

        private void RunNowButtonClicked(object sender, EventArgs e)
        {
            string id = ((PlanControl)sender).LabelPlanId.Text;
            BackupPlan plan = databaseService.GetBackupPlan(Int32.Parse(id));
            Cloud cloud = databaseService.GetCloudByName(plan.cloudName);
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(@plan.path, "Backup1");
                zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                zip.Save("D:\\backupB1.zip");
            }
            byte[] bytes = File.ReadAllBytes("D:\\backupB1.zip");
            DropboxClient client = new DropboxClient(cloud.token);
            Task tt = new Task(() => Upload(client, "", "backupB1.zip", bytes));
            tt.Start();
            MessageBox.Show("Run now!");
        }

        static async Task Upload(DropboxClient dbx, string folder, string file, byte[] content)
        {
            using (var mem = new MemoryStream(content))
            {
                var updated = await dbx.Files.UploadAsync(
                    folder + "/" + file,
                    WriteMode.Overwrite.Instance,
                    body: mem);
                Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, updated.Rev);
            }
        }

        private void LoadClouds()
        {
            flowLayoutPanelClouds.Controls.Clear();

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
                flowLayoutPanelClouds.Controls.Add(control);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }



    }
}
