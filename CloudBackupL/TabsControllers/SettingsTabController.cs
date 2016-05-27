using CloudBackupL.Models;
using CloudBackupL.Utils;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace CloudBackupL.TabsControllers
{
    public class SettingsTabController
    {
        Settings settings;
        DatabaseService databaseService;

        TextBox textBoxChunkSize;
        RadioButton radioButtonTrayAlways;
        RadioButton radioButtonTrayMinimized;
        RadioButton radioButtonTrayNever;
        CheckBox checkBoxPassword;
        NotifyIcon notifyIcon;
        TextBox textBoxPassword;
        TextBox textBoxRepeatPassword;
        CheckBox checkBoxShowNotifications;
        TextBox textBoxPostpone;
        TextBox textBoxDatabaseLocation;
        TextBox textBoxLogFileLocation;

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
            checkBoxShowNotifications = MainWindow.instance.CheckBoxShowNotifications;
            textBoxPostpone = MainWindow.instance.TextBoxPostpone;
            textBoxLogFileLocation = MainWindow.instance.TextBoxLogToFile;
            textBoxDatabaseLocation = MainWindow.instance.TextBoxDatabaseLocation;

            MainWindow.instance.ButtonSelectDatabase.Click += ButtonSelectDatabase_Click;
            MainWindow.instance.ButtonSelectLogFile.Click += ButtonSelectLogFile_Click;
            MainWindow.instance.ButtonSave.Click += ButtonSave_Click;
            MainWindow.instance.ButtonCancel.Click += ButtonCancel_Click;

            databaseService = new DatabaseService();
            LoadSettings();        
        }

        private void ButtonSelectLogFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "*.txt|*.txt";
            if (dialog.ShowDialog() != DialogResult.OK) return;
            textBoxLogFileLocation.Text = dialog.FileName;
        }

        private void ButtonSelectDatabase_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "*.s3db|*.s3db";
            if (dialog.ShowDialog() != DialogResult.OK) return;
            textBoxDatabaseLocation.Text = dialog.FileName;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
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

            bool postponeSize = Int32.TryParse(textBoxPostpone.Text, out result);
            if (!postponeSize)
            {
                saveSettings = false;
                MessageBox.Show("Enter valid postpone number!");
            }
            else if (result < 1)
            {
                saveSettings = false;
                MessageBox.Show("Please enter postpone delay greater than one minute");
            }

            if (saveSettings)
            {
                settings.chunkSize = Int32.Parse(textBoxChunkSize.Text);
                settings.postpone = Int32.Parse(textBoxPostpone.Text);
                settings.showNotifications = checkBoxShowNotifications.Checked;
                settings.askPassword = checkBoxPassword.Checked;
                settings.askPassword = checkBoxPassword.Checked;

                if (radioButtonTrayMinimized.Checked)
                    settings.trayType = "minimized";
                else if (radioButtonTrayNever.Checked)
                    settings.trayType = "never";
                else if (radioButtonTrayAlways.Checked)
                    settings.trayType = "always";

                if(!textBoxPassword.Text.Equals("tempPass-852"))
                    settings.setPassword(MyUtils.Encript(textBoxPassword.Text));

                String logFilePath = ConfigurationManager.AppSettings["logFile"];
                String databaseFilePath = ConfigurationManager.AppSettings["DbSQLite"];
                bool filePathChanged = false;
                if (textBoxLogFileLocation.Text != "")
                {
                    if(Path.GetFullPath(logFilePath) != Path.GetFullPath(textBoxLogFileLocation.Text))
                    {
                        MyUtils.UpdateSetting("logFile", Path.GetFullPath(textBoxLogFileLocation.Text));
                        filePathChanged = true;
                    }
                }

                if (textBoxDatabaseLocation.Text != "")
                {
                    if (Path.GetFullPath(databaseFilePath) != Path.GetFullPath(textBoxDatabaseLocation.Text))
                    {
                        MyUtils.UpdateSetting("DbSQLite", Path.GetFullPath(textBoxDatabaseLocation.Text));
                        filePathChanged = true;
                    }
                }

                databaseService.SetSettings(settings);
                if(filePathChanged)
                    MessageBox.Show("Settings saved successfully. Database and Log files will be changed on startup!");
                else 
                    MessageBox.Show("Settings saved successfully");
                Logger.Log("Settings saved!");
            }
        }

        private void LoadSettings()
        {
            settings = databaseService.GetSettings();
            textBoxChunkSize.Text = settings.chunkSize.ToString();
            checkBoxPassword.Checked = settings.askPassword;
            checkBoxShowNotifications.Checked = settings.showNotifications;
            textBoxPostpone.Text = settings.postpone.ToString();

            switch (settings.trayType)
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

            String logFilePath = ConfigurationManager.AppSettings["logFile"];
            String databaseFilePath = ConfigurationManager.AppSettings["DbSQLite"];
            textBoxLogFileLocation.Text = Path.GetFullPath(logFilePath);
            textBoxDatabaseLocation.Text = Path.GetFullPath(databaseFilePath);
        }

    }
}
