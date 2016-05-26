using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace CloudBackupL.TabsControllers
{
    public class SettingsTabController
    {
        TextBox textBoxChunkSize;
        RadioButton radioButtonTrayAlways;
        RadioButton radioButtonTrayMinimized;
        RadioButton radioButtonTrayNever;
        NotifyIcon notifyIcon;

        public SettingsTabController()
        {
            textBoxChunkSize = MainWindow.instance.TextBoxChunkSize;
            radioButtonTrayAlways = MainWindow.instance.RadioButtonTrayAlways;
            radioButtonTrayMinimized = MainWindow.instance.RadioButtonTrayMinimized;
            radioButtonTrayNever = MainWindow.instance.RadioButtonTrayNever;
            notifyIcon = MainWindow.instance.NotifyIconApp;
            MainWindow.instance.ButtonSave.Click += ButtonSave_Click;



            LoadSettings();
        }

        private void ButtonSave_Click(object sender, System.EventArgs e)
        {
            if (radioButtonTrayMinimized.Checked)
                ConfigurationManager.AppSettings["trayType"] = "minimized";
            else if (radioButtonTrayNever.Checked)
                ConfigurationManager.AppSettings["trayType"] = "never";
            else if (radioButtonTrayAlways.Checked)
                ConfigurationManager.AppSettings["trayType"] = "always";

            ConfigurationManager.AppSettings["chunkSize"] = textBoxChunkSize.Text;


        }

        private void LoadSettings()
        {
            textBoxChunkSize.Text = ConfigurationManager.AppSettings["chunkSize"];
            switch(ConfigurationManager.AppSettings["trayType"])
            {
                case "minimized":
                    radioButtonTrayMinimized.Checked = true;
                    break;
                case "never":
                    radioButtonTrayNever.Checked = true;
                    break;
                case "always":
                    radioButtonTrayAlways.Checked = true;
                    break;
            }
        }

    }
}
