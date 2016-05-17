using ByteSizeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudBackupL.TabsControllers
{
    public class CloudBackupsTabController
    {
        ListBox listBoxClouds;
        ListView listViewBackupsInfo;
        DatabaseService databaseService;

        public CloudBackupsTabController()
        {
            listBoxClouds = MainWindow.instance.ListBoxClouds;
            listViewBackupsInfo = MainWindow.instance.ListViewBackupsInfo;
            databaseService = new DatabaseService();
            listBoxClouds.SelectedIndexChanged += listBoxClouds_SelectedIndexChanged;
        }

        public void LoadCloudList()
        {
            listBoxClouds.Items.Clear();
            List<Cloud> clouds = databaseService.GetAllClouds();
            foreach (var c in clouds)
            {
                listBoxClouds.Items.Add(new System.Web.UI.WebControls.ListItem(c.name, c.id));
            }
        }

        private void listBoxClouds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((System.Windows.Forms.ListBox)sender).Text.Equals("")) return;
            listViewBackupsInfo.Items.Clear();
            System.Web.UI.WebControls.ListItem selectedItem = (System.Web.UI.WebControls.ListItem)((System.Windows.Forms.ListBox)sender).SelectedItem;
            List<Backup> backups = databaseService.GetBackCloudBackups(selectedItem.Value);
            foreach (var b in backups)
            {
                Double size = Math.Round(ByteSize.FromBytes(b.size).MegaBytes, 3);
                Double compressedSize = Math.Round(ByteSize.FromBytes(b.compressedSize).MegaBytes, 3);
                TimeSpan runTimeTimeSpan = new TimeSpan(0, 0, 0, 0, (int)b.runTime);
                String runTime = (runTimeTimeSpan.Days > 0 ? runTimeTimeSpan.Days + " d - " : "") +
                    runTimeTimeSpan.Hours + " h : " + runTimeTimeSpan.Minutes + " m : " + runTimeTimeSpan.Seconds + " s";

                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(
                    new string[] {b.backupPlanName, b.date.ToString("yyyy/MM/dd h-m-s"),
                    size + " MB" , compressedSize + " MB", runTime});
                item.Tag = b.id;
                listViewBackupsInfo.Items.Add(item);
            }

        }
    }
}
