using CloudBackupL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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
            dateTimePickerScheduleTime.Value = DateTime.Now;
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
            foreach (var c in clouds)
            {
                comboBoxClouds.Items.Add(new ListItem(c.name, c.id.ToString()));
            }

            if (!isNewBackupPlan)
            {
                textBoxPlanName.Text = backupPlanToEdit.name;
                textBoxFolderPath.Text = backupPlanToEdit.path;
                comboBoxClouds.SelectedIndex = comboBoxClouds.FindStringExact(backupPlanToEdit.cloudName);
                comboBoxScheduleType.SelectedIndex = comboBoxScheduleType.FindStringExact(backupPlanToEdit.scheduleType);
                dateTimePickerScheduleTime.Value = backupPlanToEdit.scheduleTime;
                if (backupPlanToEdit.scheduleType.Equals("Weekly") || backupPlanToEdit.scheduleType.Equals("Monthly"))
                {
                    PopulateSchedule(backupPlanToEdit.scheduleType);
                    comboBoxMonthOrWeekDay.SelectedIndex = backupPlanToEdit.scheduleDay-1;
                }

                if (backupPlanToEdit.overrideBackup)
                    radioButtonYes.Checked = true;
                else radioButtonNo.Checked = true;
                this.Text = "Edit Backup Plan";
            }
        }

        private void comboBoxScheduleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var scheduleType = comboBoxScheduleType.Text;
            PopulateSchedule(scheduleType);
        }

        private void PopulateSchedule(string scheduleType)
        {
            comboBoxMonthOrWeekDay.Text = String.Empty;
            comboBoxMonthOrWeekDay.Items.Clear();
            dateTimePickerScheduleTime.Enabled = true;
            comboBoxMonthOrWeekDay.Enabled = true;
            switch (scheduleType)
            {
                case "Manual":
                    dateTimePickerScheduleTime.Enabled = false;
                    comboBoxMonthOrWeekDay.Enabled = false;
                    break;
                case "Daily":
                    comboBoxMonthOrWeekDay.Enabled = false;
                    break;
                case "Weekly":
                    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)).OfType<DayOfWeek>().ToList().Skip(1))
                        comboBoxMonthOrWeekDay.Items.Add(day.ToString());
                    comboBoxMonthOrWeekDay.Items.Add(DayOfWeek.Sunday.ToString());
                    comboBoxMonthOrWeekDay.Enabled = true;
                    break;
                case "Monthly":
                    for (int i = 1; i <= 31; i++)
                        comboBoxMonthOrWeekDay.Items.Add(i);
                    comboBoxMonthOrWeekDay.Enabled = true;
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
            bool val1;
            bool val2 = true;
            val1 = textBoxPlanName.Text.Length > 0 && textBoxFolderPath.Text.Length > 0
                && comboBoxClouds.Text.Length > 0 && databaseService.CheckPlanName(textBoxPlanName.Text)
                && comboBoxScheduleType.Text.Length > 0;
            if (comboBoxScheduleType.Text.Equals("Weekly") || comboBoxScheduleType.Text.Equals("Monthly"))
                if (comboBoxMonthOrWeekDay.Text.Length == 0)
                    val2 = false;

            return val1 && val2; 
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
                    if (comboBoxScheduleType.Text.Equals("Weekly") || comboBoxScheduleType.Text.Equals("Monthly"))
                    {
                        plan.scheduleDay = comboBoxMonthOrWeekDay.SelectedIndex + 1;
                    }

                    plan.currentStatus = "No backup";
                    plan.cloudName = comboBoxClouds.Text;
                    plan.cloudId = ((ListItem)comboBoxClouds.SelectedItem).Value;
                    plan.overrideBackup = radioButtonYes.Checked ? true : false;
                    if(!plan.scheduleType.Equals("Manual"))
                        plan.nextExecution = MyUtils.GetNextExecution(plan);
                    databaseService.InsertBackupPlan(plan);
                    dialogMessage = "Plan created succesfully!";
                    Logger.Log(string.Format("Plan {0} created succesfully!", plan.name));
                }
                else
                {
                    backupPlanToEdit.name = textBoxPlanName.Text;
                    backupPlanToEdit.path = textBoxFolderPath.Text;
                    backupPlanToEdit.creationDate = DateTime.Now;
                    backupPlanToEdit.scheduleType = comboBoxScheduleType.Text;
                    backupPlanToEdit.scheduleTime = dateTimePickerScheduleTime.Value;
                    if (comboBoxScheduleType.Text.Equals("Weekly") || comboBoxScheduleType.Text.Equals("Monthly"))
                    {
                        backupPlanToEdit.scheduleDay = comboBoxMonthOrWeekDay.SelectedIndex + 1;
                    }
                    backupPlanToEdit.cloudName = comboBoxClouds.Text;
                    backupPlanToEdit.cloudId = ((ListItem)comboBoxClouds.SelectedItem).Value;
                    backupPlanToEdit.overrideBackup = radioButtonYes.Checked ? true : false;
                    if (!backupPlanToEdit.scheduleType.Equals("Manual"))
                        backupPlanToEdit.nextExecution = MyUtils.GetNextExecution(backupPlanToEdit);
                    databaseService.UpdateBackupPlan(backupPlanToEdit);
                    dialogMessage = "Plan updated succesfully";
                }
                this.DialogResult = DialogResult.OK;
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
            this.DialogResult = DialogResult.None;
            this.Close();
        }
    }
}
