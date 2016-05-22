using Nemiro.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace CloudBackupL.TabsControllers
{
    public class ManualWorkTabController
    {
        ListBox listBoxClouds;
        ListView listViewCloudFiles;
        DatabaseService databaseService;
        DropBoxController dropboxController;
        MainWindow mainWindowInstance;
        string currentPath = "/";
        string selectedPath;
        Cloud currentCLoud;

        public ManualWorkTabController()
        {
            listBoxClouds = MainWindow.instance.ListBoxCloudsManual;
            listBoxClouds.SelectedIndexChanged += ListBoxClouds_SelectedIndexChanged;
            databaseService = new DatabaseService();
            dropboxController = new DropBoxController();
            mainWindowInstance = MainWindow.instance;
            listViewCloudFiles = mainWindowInstance.ListViewCloudFiles;
            listViewCloudFiles.DoubleClick += ListViewCloudFiles_DoubleClick;
            mainWindowInstance.ButtonManualDownload.Click += ButtonManualDownload_Click;
            mainWindowInstance.ButtonManualUpload.Click += ButtonManualUpload_Click;
            listViewCloudFiles.ItemSelectionChanged += ListViewCloudFiles_ItemSelectionChanged;
        }

        private void ButtonManualUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult dialogResult = ofd.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                dropboxController.Upload("/", currentCLoud.token, ofd.FileName, Web_UploadProgressChanged, null);
            }
        }


        private void Web_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            int p = (int)(e.BytesSent * 100 / e.TotalBytesToSend);
            if(p >= 0 && p <= 100)
            mainWindowInstance.ProgressBarManualDownload.Value = p;
        }

        private void ListViewCloudFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            selectedPath = e.Item.Text;
        }


        private void Web_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            mainWindowInstance.ProgressBarManualDownload.Value = e.ProgressPercentage;
        }

        private void ButtonManualDownload_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult dialogResult = folderBrowser.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                dropboxController.Download(selectedPath, currentCLoud.token, folderBrowser.SelectedPath + "//" + Path.GetFileName(selectedPath), Web_DownloadProgressChanged, null);
            }
        }

        private void ListViewCloudFiles_DoubleClick(object sender, EventArgs e)
        {
            
            if(listViewCloudFiles.FocusedItem.Text == "..")
            {
                if(this.currentPath != "/")
                {
                    currentPath = Path.GetDirectoryName(this.currentPath).Replace("\\", "/");
                    dropboxController.GetFilesList(currentCLoud.token, LoadFilesCallback, currentPath);
                }
            } else
            {
                currentPath += listViewCloudFiles.FocusedItem.Text.Remove(0,1);
                dropboxController.GetFilesList(currentCLoud.token, LoadFilesCallback, currentPath);
            }
            listViewCloudFiles.FocusedItem.Focused = false;
        }

        private void ListBoxClouds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((System.Windows.Forms.ListBox)sender).Text.Equals("")) return;
            listViewCloudFiles.Items.Clear();
            System.Web.UI.WebControls.ListItem selectedItem = (System.Web.UI.WebControls.ListItem)((System.Windows.Forms.ListBox)sender).SelectedItem;
            Cloud cloud = databaseService.GetCloud(Int32.Parse(selectedItem.Value));
            currentCLoud = cloud;
            listViewCloudFiles.Items.Add("..");
            dropboxController.GetFilesList(cloud.token, LoadFilesCallback, currentPath);
        }

        private void LoadFilesCallback(RequestResult result)
        {
            if(result.StatusCode == 200)
            {
                listViewCloudFiles.Invoke(new Action(() => listViewCloudFiles.Items.Clear()));
                listViewCloudFiles.Invoke(new Action(() => listViewCloudFiles.Items.Add("..")));
                foreach (UniValue file in result["contents"])
                {
                    listViewCloudFiles.Invoke(new Action(() => listViewCloudFiles.Items.Add(file["path"].ToString())));
                }
            }
        }

        public void LoadCloudList()
        {
            listBoxClouds.Items.Clear();
            List<Cloud> clouds = databaseService.GetAllClouds();
            foreach (var c in clouds)
            {
                listBoxClouds.Items.Add(new System.Web.UI.WebControls.ListItem(c.name, c.id.ToString()));
            }
        }
    }
}
