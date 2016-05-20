using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudBackupL.TabsControllers
{
    public class SettingsTabController
    {
        TextBox textBoxChunkSize;

        public SettingsTabController()
        {
            textBoxChunkSize = MainWindow.instance.TextBoxChunkSize;

            LoadSettings();
        }

        private void LoadSettings()
        {
            textBoxChunkSize.Text = ConfigurationManager.AppSettings["chunkSize"];
        }


    }
}
