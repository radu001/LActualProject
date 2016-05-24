using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using CloudBackupL.BackupActions;
using CloudBackupL.Utils;

namespace CloudBackupL.TabsControllers
{
    public class ManualWorkTabController
    {
        ListBox listBoxClouds;
        ListView listViewCloudFiles;
        DatabaseService databaseService;
        MainWindow mainWindowInstance;
        ProgressBar progressBar;
        Label labelStatus;
        TextBox textBoxPassword;
        string currentPath = "/";
        string selectedPath;
        Cloud currentCLoud;
        ICloud cloudController;
        bool isInPlanFolder = false;

        public ManualWorkTabController()
        {
            mainWindowInstance = MainWindow.instance;
            this.progressBar = mainWindowInstance.ProgressBarManual;
            this.labelStatus = mainWindowInstance.LabelManualStatus;
            this.textBoxPassword = mainWindowInstance.TextBoxManualPassword;
            listBoxClouds = mainWindowInstance.ListBoxCloudsManual;
            listBoxClouds.SelectedIndexChanged += ListBoxClouds_SelectedIndexChanged;
            databaseService = new DatabaseService();
            listViewCloudFiles = mainWindowInstance.ListViewCloudFiles;
            listViewCloudFiles.DoubleClick += ListViewCloudFiles_DoubleClick;
            mainWindowInstance.ButtonManualDownload.Click += ButtonManualDownload_Click;
            listViewCloudFiles.ItemSelectionChanged += ListViewCloudFiles_ItemSelectionChanged;
        }


        private void ListViewCloudFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            selectedPath = e.Item.Text;
        }


        private void ListViewCloudFiles_DoubleClick(object sender, EventArgs e)
        {
            if(listViewCloudFiles.FocusedItem.Text == "..")
            {
                cloudController.GetFilesList(currentCLoud.token, LoadFilesCallback, "/");
                isInPlanFolder = false;
                listViewCloudFiles.Items.Clear();
            }
             else if(!isInPlanFolder)
            {
                currentPath = listViewCloudFiles.FocusedItem.Text;
                cloudController.GetFilesList(currentCLoud.token, LoadFilesCallback, currentPath);
                listViewCloudFiles.Items.Clear();
                listViewCloudFiles.Items.Add("..");
                isInPlanFolder = true;
            }
        }


        private void ListBoxClouds_SelectedIndexChanged(object sender, EventArgs e)
        {
            isInPlanFolder = false;
            if (((System.Windows.Forms.ListBox)sender).Text.Equals("")) return;
            listViewCloudFiles.Items.Clear();
            System.Web.UI.WebControls.ListItem selectedItem = (System.Web.UI.WebControls.ListItem)((System.Windows.Forms.ListBox)sender).SelectedItem;
            Cloud cloud = databaseService.GetCloud(Int32.Parse(selectedItem.Value));
            currentCLoud = cloud;
            if(currentCLoud.cloudType.Equals("dropbox"))
            {
                cloudController = new DropBoxController();
            }
            cloudController.GetFilesList(cloud.token, LoadFilesCallback, "/");
        }
        
        private void LoadFilesCallback(object s, DownloadStringCompletedEventArgs args)
        {
            if(args.Error == null)
            {
                dynamic result = JObject.Parse(args.Result);
                foreach (var file in result.contents)
                {
                    DateTime date = DateTime.Parse((string)file.modified);
                    ListViewItem item = new ListViewItem(new string[] {file.path.ToString(), date.ToString()});
                    item.Tag = file.path.ToString();
                    listViewCloudFiles.Invoke(new Action(() => listViewCloudFiles.Items.Add(item)));
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

        private void ButtonManualDownload_Click(object sender, EventArgs e)
        {
            if(isInPlanFolder && listViewCloudFiles.SelectedItems.Count == 1 && !listViewCloudFiles.SelectedItems[0].Text.Equals(".."))
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                DialogResult dialogResult = folderBrowser.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    mainWindowInstance.ResetDownloadAction();
                    DownloadBackupAction downloadBackupAction = new DownloadBackupAction(currentCLoud, listViewCloudFiles.SelectedItems[0].Tag.ToString(), labelStatus, progressBar, folderBrowser.SelectedPath, DownloadCompleteEvent, ArchiveUtils.Encript(textBoxPassword.Text), false);
                    downloadBackupAction.StartDownloadBackupAction();
                }
            } else
            {
                MessageBox.Show("Please select backup folder then click Download!");
            }
        }

        private void Web_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void DownloadCompleteEvent(object sender, bool e)
        {
            labelStatus.Text = "Status:";
            mainWindowInstance.ResetDownloadAction();
        }
    }
}
