using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace CloudBackupL
{
    public partial class ManageBackupPlanWindow : Form
    {
        DatabaseService databaseService;
        private bool isNewBackupPlan;
        BackupPlan backupPlanToEdit;

        public ManageBackupPlanWindow()
        {
            InitializeComponent();
            isNewBackupPlan = true;
            databaseService = new DatabaseService();
        }

        public ManageBackupPlanWindow(int backupPlanId)
        {
            InitializeComponent();
            isNewBackupPlan = false;
            databaseService = new DatabaseService();
            backupPlanToEdit = databaseService.GetBackupPlan(backupPlanId);
        }



        private void AddBackupPlanWindow_Load(object sender, EventArgs e)
        {
            List<Cloud> clouds = databaseService.GetAllClouds();
            foreach(var c in clouds)
            {
                comboBoxClouds.Items.Add(new ListItem(c.name, c.id.ToString()));   
            }

            if(!isNewBackupPlan)
            {
                textBoxPlanName.Text = backupPlanToEdit.name;
                textBoxFolderPath.Text = backupPlanToEdit.path;
                comboBoxClouds.SelectedIndex = comboBoxClouds.FindStringExact(backupPlanToEdit.cloudName);
                comboBoxScheduleType.SelectedIndex = comboBoxScheduleType.FindStringExact(backupPlanToEdit.scheduleType);
                dateTimePickerScheduleTime.Value = backupPlanToEdit.scheduleTime;
            }
        }

        private void comboBoxScheduleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dateTimePickerScheduleTime.Enabled = true;
            switch (comboBoxScheduleType.Text)
            {
                case "Manual":
                    dateTimePickerScheduleTime.Enabled = false;
                    break;
                case "Daily":
                    dateTimePickerScheduleTime.CustomFormat = "HH:MM";
                    break;
                case "Weekly":
                    dateTimePickerScheduleTime.CustomFormat = "dddd  HH:MM";
                    break;
                case "Monthly":
                    dateTimePickerScheduleTime.CustomFormat = "d  HH:MM";
                    break;

            }
        }

        private void buttonBrowseFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFolderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }


        private bool ValidatePlanCreation()
        {
            if (textBoxPlanName.Text.Length > 0 && textBoxFolderPath.Text.Length > 0 
                && comboBoxClouds.Text.Length > 0 && databaseService.CheckPlanName(textBoxPlanName.Text))
            {
                return true;
            } else
            {
                return false;
            }
        }

        private void buttonComplete_Click(object sender, EventArgs e)
        {
            if (ValidatePlanCreation())
            {
                string dialogMessage;
                if (isNewBackupPlan)
                {
                    BackupPlan plan = new BackupPlan();
                    plan.name = textBoxPlanName.Text;
                    plan.path = textBoxFolderPath.Text;
                    plan.creationDate = DateTime.Now;
                    plan.scheduleType = comboBoxScheduleType.Text;
                    plan.scheduleTime = dateTimePickerScheduleTime.Value;
                    plan.currentStatus = "";
                    plan.cloudName = comboBoxClouds.Text;
                    plan.cloudId = Int32.Parse(((ListItem)comboBoxClouds.SelectedItem).Value);
                    databaseService.InsertBackupPlan(plan);
                    dialogMessage = "Plan created succesfully";
                }
                else
                {
                    backupPlanToEdit.name = textBoxPlanName.Text;
                    backupPlanToEdit.path = textBoxFolderPath.Text;
                    backupPlanToEdit.creationDate = DateTime.Now;
                    backupPlanToEdit.scheduleType = comboBoxScheduleType.Text;
                    backupPlanToEdit.scheduleTime = dateTimePickerScheduleTime.Value;
                    backupPlanToEdit.currentStatus = "";
                    backupPlanToEdit.cloudName = comboBoxClouds.Text;
                    backupPlanToEdit.cloudId = Int32.Parse(((ListItem)comboBoxClouds.SelectedItem).Value);
                    databaseService.UpdateBackupPlan(backupPlanToEdit);
                    dialogMessage = "Plan updated succesfully";
                }

                DialogResult dialog = MessageBox.Show(dialogMessage);
                if (dialog == DialogResult.OK)
                {
                    MainWindow.instance.backupPlansTabController.LoadPlans();
                    this.Close();
                }
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Please complete all fields.");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
