using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace CloudBackupL
{
    public partial class AddBackupPlanWindow : Form
    {
        DatabaseService databaseService;
        public AddBackupPlanWindow()
        {
            InitializeComponent();
            databaseService = new DatabaseService();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddBackupPlanWindow_Load(object sender, EventArgs e)
        {
            List<Cloud> clouds = databaseService.GetAllClouds();
            foreach(var c in clouds)
            {
                comboBoxClouds.Items.Add(new ListItem(c.name, c.id.ToString()));   
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
                BackupPlan plan = new BackupPlan();
                plan.name = textBoxPlanName.Text;
                plan.path = textBoxFolderPath.Text;
                plan.creationDate = DateTime.Now;
                plan.scheduleType = comboBoxScheduleType.Text;
                plan.scheduleTime = dateTimePickerScheduleTime.Value;
                plan.currentStatus = "notRun";
                plan.cloudName = comboBoxClouds.Text;
                plan.cloudId = Int32.Parse(((ListItem)comboBoxClouds.SelectedItem).Value);
                databaseService.InsertBackupPlan(plan);
                DialogResult dialog = MessageBox.Show("Plan created succesfully");
                if (dialog == DialogResult.OK)
                {
                    this.Close();
                }
            } else
            {
                DialogResult dialog = MessageBox.Show("Please complete all fields.");
            } 

            
        }
    }
}
