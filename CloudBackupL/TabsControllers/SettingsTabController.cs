using CloudBackupL.Models;
using CloudBackupL.Utils;
using System;
using System.Windows.Forms;

namespace CloudBackupL.TabsControllers
{
    public class SettingsTabController
    {
        TextBox textBoxChunkSize;
        RadioButton radioButtonTrayAlways;
        RadioButton radioButtonTrayMinimized;
        RadioButton radioButtonTrayNever;
        CheckBox checkBoxPassword;
        NotifyIcon notifyIcon;
        TextBox textBoxPassword;
        TextBox textBoxRepeatPassword;
        Settings settings;
        DatabaseService databaseService;

        public SettingsTabController()
        {
            textBoxChunkSize = MainWindow.instance.TextBoxChunkSize;
            radioButtonTrayAlways = MainWindow.instance.RadioButtonTrayAlways;
            radioButtonTrayMinimized = MainWindow.instance.RadioButtonTrayMinimized;
            radioButtonTrayNever = MainWindow.instance.RadioButtonTrayNever;
            checkBoxPassword = MainWindow.instance.CheckBoxPassword;
            notifyIcon = MainWindow.instance.NotifyIconApp;
            textBoxPassword = MainWindow.instance.TextBoxPassword;
            textBoxRepeatPassword = MainWindow.instance.TextBoxRepeatPassword;
            MainWindow.instance.ButtonSave.Click += ButtonSave_Click;
            databaseService = new DatabaseService();
            settings = databaseService.GetSettings();

            LoadSettings();
        }

        private void ButtonSave_Click(object sender, System.EventArgs e)
        {
            var saveSettings = true;

            int result;
            bool validChunkSize = Int32.TryParse(textBoxChunkSize.Text, out result);
            if (!validChunkSize)
            {
                saveSettings = false;
                MessageBox.Show("Enter valid ChunkSize number!");
            }
            else if (result < 1 || result > 99)
            {
                saveSettings = false;
                MessageBox.Show("Please enter chunk size between 1 and 99");
            }

            if (textBoxPassword.Text.Equals(textBoxRepeatPassword.Text))
            {
                if (textBoxRepeatPassword.Text.Length < 4)
                {
                    saveSettings = false;
                    MessageBox.Show("Password too short!");
                }
            }
            else
            {
                saveSettings = false;
                MessageBox.Show("Password not equals!");
            }


            if(saveSettings)
            {
                settings.chunkSize = Int32.Parse(textBoxChunkSize.Text);

                if (radioButtonTrayMinimized.Checked)
                    settings.trayType = "minimized";
                else if (radioButtonTrayNever.Checked)
                    settings.trayType = "never";
                else if (radioButtonTrayAlways.Checked)
                    settings.trayType = "always";

                if(!textBoxPassword.Text.Equals("tempPass-852"))
                    settings.setPassword(MyUtils.Encript(textBoxPassword.Text));

                settings.askPassword = checkBoxPassword.Checked;

                databaseService.SetSettings(settings);
                MessageBox.Show("Saved successfully");
            }

        }

        private void LoadSettings()
        {
            textBoxChunkSize.Text = settings.chunkSize.ToString();
            switch(settings.trayType)
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

            if (settings.askPassword)
                checkBoxPassword.Checked = true;
        }

    }
}
