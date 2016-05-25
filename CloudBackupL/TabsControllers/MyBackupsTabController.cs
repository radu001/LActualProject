using ByteSizeLib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using CloudBackupL.Clouds;
using CloudBackupL.BackupActions;
using CloudBackupL.Utils;

namespace CloudBackupL.TabsControllers
{
    public class MyBackupsTabController
    {
        ListBox listBoxBackupPlans;
        ListView listViewBackupsInfo;
        DatabaseService databaseService;
        ContextMenuStrip contextMenuStrip;
        ListViewItem selectedItem;
        MainWindow mainInstance;
        Label labelStatus;
        ProgressBar progressBar;

        public MyBackupsTabController()
        {
            listBoxBackupPlans = MainWindow.instance.ListBoxBackupPlans;
            listViewBackupsInfo = MainWindow.instance.ListViewBackupsInfo;
            databaseService = new DatabaseService();
            listBoxBackupPlans.SelectedIndexChanged += listBoxClouds_SelectedIndexChanged;
            listViewBackupsInfo.MouseClick += ListViewBackupsInfo_MouseClick;
            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Restore", null, EventRestore);
            contextMenuStrip.Items.Add("Download", null, EventDownload);
            contextMenuStrip.Items.Add("Delete", null, EventDelete);
            labelStatus = MainWindow.instance.LabelMainStatus;
            progressBar = MainWindow.instance.ProgressBarMain;
            mainInstance = MainWindow.instance;
        }

        public void EventRestore(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to restore this backup? Your current folder will be replaced by this one.", "Restore backup", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (DialogResult.Yes == result)
            {
                if (selectedItem != null && !MainWindow.isActiveDownloadOperation)
                {
                    string password = ArchiveUtils.Encript("mypassword");
                    Backup backup = databaseService.GetBackup((int)selectedItem.Tag);
                    BackupPlan backupPlan = databaseService.GetBackupPlan(backup.backupPlanId);
                    Cloud cloud = databaseService.GetCloud(backup.cloudId);
                    mainInstance.ResetDownloadAction();
                    DownloadBackupAction downloadBackupAction = new DownloadBackupAction(cloud, backup.targetPath, labelStatus, progressBar, backupPlan.path, DownloadCompleteEvent, password, true);
                    downloadBackupAction.StartDownloadBackupAction();
                }
            }
        }

        public void EventDownload(object sender, EventArgs e)
        {
            if (selectedItem != null && !MainWindow.isActiveDownloadOperation)
            {
                string password = ArchiveUtils.Encript("mypassword");
                Backup backup = databaseService.GetBackup((int)selectedItem.Tag);
                Cloud cloud = databaseService.GetCloud(backup.cloudId);
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                DialogResult dialogResult = folderBrowser.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    mainInstance.RestrictDownloadAction();
                    DownloadBackupAction downloadBackupAction = new DownloadBackupAction(cloud, backup.targetPath, labelStatus, progressBar, folderBrowser.SelectedPath + "//" + Path.GetFileName(backup.targetPath), DownloadCompleteEvent, password, false);
                    downloadBackupAction.StartDownloadBackupAction();
                } else
                {
                    mainInstance.ResetDownloadAction();
                }
            }
        }

        private void DownloadCompleteEvent(object sender, bool e)
        {
            mainInstance.ResetDownloadAction();
            Console.WriteLine("Download complete");
        }







        public void EventDelete(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this backup ? ", "Delete backup", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (DialogResult.Yes == result)
            {
                if (selectedItem != null)
                {
                    Backup backup = databaseService.GetBackup((int)selectedItem.Tag);
                    Cloud cloud = databaseService.GetCloud(backup.cloudId);
                    ICloud cloudController;
                    if (cloud.cloudType.Equals("dropbox"))
                        cloudController = new DropBoxController();
                    else
                        cloudController = new OneDriveController();
                    cloudController.DeleteFolder(cloud.token, DeleteFolderCompelte, backup.targetPath);
                    databaseService.DeleteBackup(backup.id);
                }
            }
        }


        private void DeleteFolderCompelte(object sender, object args)
        {
            
        }



        private void ListViewBackupsInfo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listViewBackupsInfo.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    selectedItem = listViewBackupsInfo.FocusedItem;
                    contextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        public void LoadBackupPlansList()
        {
            listBoxBackupPlans.Items.Clear();
            List<BackupPlan> plans = databaseService.GetAllPlans();
            foreach (var p in plans)
            {
                System.Web.UI.WebControls.ListItem listItem = new System.Web.UI.WebControls.ListItem(p.name, p.id.ToString());
                listItem.Attributes.Add("cloudId", p.cloudId);
                listBoxBackupPlans.Items.Add(listItem);
            }
        }

        private void listBoxClouds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((System.Windows.Forms.ListBox)sender).Text.Equals("")) return;
            listViewBackupsInfo.Items.Clear();
            System.Web.UI.WebControls.ListItem selectedItem = (System.Web.UI.WebControls.ListItem)((System.Windows.Forms.ListBox)sender).SelectedItem;
            List<Backup> backups = databaseService.GetBackupsByPlanId(Int32.Parse(selectedItem.Value));
            string cloudId = selectedItem.Attributes["cloudId"];

            foreach (var b in backups)
            {
                Double size = Math.Round(ByteSize.FromBytes(b.size).MegaBytes, 3);
                Double compressedSize = Math.Round(ByteSize.FromBytes(b.compressedSize).MegaBytes, 3);
                TimeSpan runTimeTimeSpan = new TimeSpan(0, 0, 0, 0, (int)b.runTime);
                String runTime = (runTimeTimeSpan.Days > 0 ? runTimeTimeSpan.Days + " d - " : "") +
                    runTimeTimeSpan.Hours + " h : " + runTimeTimeSpan.Minutes + " m : " + runTimeTimeSpan.Seconds + " s";

                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(
                    new string[] {b.targetPath, b.date.ToString(),
                    size + " MB" , compressedSize + " MB", runTime});
                item.Tag = b.id;
                listViewBackupsInfo.Items.Add(item);
            }

        }
    }
}