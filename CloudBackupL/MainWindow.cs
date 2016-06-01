using CloudBackupL.Properties;
using CloudBackupL.TabsControllers;
using CloudBackupL.Utils;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CloudBackupL
{
    public partial class MainWindow : Form
    {
        public static MainWindow instance;
        public HomeTabController homeTabController;
        public BackupPlansTabController backupPlansTabController;
        public MyBackupsTabController myBackupsTabController;
        public ManualWorkTabController manualWorkTabController;
        public SettingsTabController settingsTabController;
        public static bool isActiveDownloadOperation = false;
        public static bool isActiveUploadOperation = false;
        private Button precedentButton;
        Color selectedColor;
        Color normalColor;
        DatabaseService databaseService;
        
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect, // x-coordinate of upper-left corner
        int nTopRect, // y-coordinate of upper-left corner
        int nRightRect, // x-coordinate of lower-right corner
        int nBottomRect, // y-coordinate of lower-right corner
        int nWidthEllipse, // height of ellipse
        int nHeightEllipse // width of ellipse
        );
        
        //Main Window Constructor
        public MainWindow()
        {
            instance = this;
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            backupPlansTabController = new BackupPlansTabController();
            myBackupsTabController = new MyBackupsTabController();
            manualWorkTabController = new ManualWorkTabController();
            settingsTabController = new SettingsTabController();
            homeTabController = new HomeTabController();
            selectedColor = buttonTabHome.BackColor;
            normalColor = buttonTabBackupPlans.BackColor;
            precedentButton = buttonTabHome;
            databaseService = new DatabaseService();

            notifyIconApp.Icon = Resources.app_icon;
            notifyIconApp.Text = "Secure Backup";
            notifyIconApp.DoubleClick += NotifyIconApp_DoubleClick;
            LoadTrayType();
            this.FormClosing += MainWindow_FormClosing;

        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {  
            if(new DatabaseService().GetSettings().preventShutDown && (isActiveDownloadOperation || isActiveUploadOperation))
            {
                MessageBox.Show("Please wait until current opperation is finished");
                e.Cancel = true;
            }        
        }

        private void NotifyIconApp_DoubleClick(object sender, EventArgs e)
        {
            string traySetting = databaseService.GetSettings().trayType;
            if (traySetting.Equals("minimized"))
            {
                notifyIconApp.Visible = false;
                this.Show();
            }

            if (traySetting.Equals("always"))
            {
                this.Show();
            }
        }

        public void LoadTrayType()
        {
            switch (databaseService.GetSettings().trayType)
            {
                case "minimized":
                    notifyIconApp.Visible = true;
                    ShowInTaskbar = true;
                    break;
                case "never":
                    notifyIconApp.Visible = false;
                    ShowInTaskbar = true;
                    break;
                case "always":
                    notifyIconApp.Visible = true;
                    ShowInTaskbar = false;
                    break;
            }
        }

        //Main Window Load
        private void MainWindow_Load(object sender, EventArgs e)
        {
            LoadAllControlls();
        }

        public void LoadAllControlls()
        {
            homeTabController.LoadClouds();
            backupPlansTabController.LoadPlans();
            myBackupsTabController.LoadBackupPlansList();
            manualWorkTabController.LoadCloudList();
            homeTabController.LoadQueueList();
            LabelMainPlanName.Text = "-";
            progressBarMain.Value = 0;
            Logger.Log("Application started!");
        }

        public void RestrictDownloadAction()
        {
            isActiveDownloadOperation = true;
            backupPlansTabController.DisableActions();
            buttonManualDownload.Enabled = false;
        }

        public void ResetDownloadAction()
        {
            isActiveDownloadOperation = false;
            backupPlansTabController.LoadPlans();
            buttonManualDownload.Enabled = true;
        }

        public Button ButtonAddCloud
        {
            get { return this.buttonAddCloud; }
            set { this.buttonAddCloud = value; }
        }

        public FlowLayoutPanel FlowLayoutPanelClouds
        {
            get { return this.flowLayoutPanelClouds; }
            set { this.flowLayoutPanelClouds = value; }
        }

        public ImageList ImageListClouds
        {
            get { return this.imageListClouds; }
            set { this.imageListClouds = value; }
        }

        public FlowLayoutPanel FlowLayoutPanelPlans
        {
            get { return this.flowLayoutPanelPlans; }
            set { this.flowLayoutPanelPlans = value; }
        }

        public Label LabelTotalPlans
        {
            get { return this.labelTotalPlans; }
            set { this.labelTotalPlans = value; }
        }

        public Button ButtonAddBackupPlan
        {
            get { return this.buttonAddBackupPlan; }
            set { this.buttonAddBackupPlan = value; }
        }

        public Button ButtonTabMyBackups
        {
            get { return this.buttonTabMyBackups; }
            set { this.buttonTabMyBackups = value; }
        }

        public ListBox ListBoxBackupPlans
        {
            get { return this.listBoxBackupPlans; }
            set { this.listBoxBackupPlans = value; }
        }

        public ListView ListViewBackupsInfo
        {
            get { return this.listViewBackupsInfo; }
            set { this.listViewBackupsInfo = value; }
        }

        public ListBox ListBoxCloudsManual
        {
            get { return this.listBoxCloudsManual; }
            set { this.listBoxCloudsManual = value; }
        }

        public ListView ListViewCloudFiles
        {
            get { return this.listViewCloudFiles; }
            set { this.listViewCloudFiles = value; }
        }

        public Button ButtonManualDownload
        {
            get { return this.buttonManualDownload; }
            set { this.buttonManualDownload = value; }
        }

        public ProgressBar ProgressBarManual
        {
            get { return this.progressBarManual; }
            set { this.progressBarManual = value; }
        }

        public Label LabelManualStatus
        {
            get { return this.labelManualStatus; }
            set { this.labelManualStatus = value; }
        }

        public TextBox TextBoxManualPassword
        {
            get { return this.textBoxManualPassword; }
            set { this.textBoxManualPassword = value; }
        }


        public TextBox TextBoxChunkSize
        {
            get { return this.textBoxChunkSize; }
            set { this.textBoxChunkSize = value; }
        }

        public TabControl TabControl
        {
            get { return this.tabControl1; }
            set { this.tabControl1 = value; }
        }

        public Label LabelMainStatus
        {
            get { return this.labelMainStatus; }
            set { this.labelMainStatus = value; }
        }

        public ProgressBar ProgressBarMain
        {
            get { return this.progressBarMain; }
            set { this.progressBarMain = value; }
        }

        public ListView ListViewBackupQueue
        {
            get { return this.listViewBackupQueue; }
            set { this.listViewBackupQueue = value; }
        }

        public Label LabelMainPlanName
        {
            get { return this.labelMainPlanName; }
            set { this.labelMainPlanName = value; }
        }

        public NotifyIcon NotifyIconApp
        {
            get { return notifyIconApp; }
            set { notifyIconApp = value; }
        }

        public RadioButton RadioButtonTrayAlways
        {
            get { return radioButtonTrayAlways; }
            set { radioButtonTrayAlways = value; }
        }

        public RadioButton RadioButtonTrayMinimized
        {
            get { return radioButtonTrayMinimized; }
            set { radioButtonTrayMinimized = value; }
        }

        public RadioButton RadioButtonTrayNever
        {
            get { return radioButtonTrayNever; }
            set { radioButtonTrayNever = value; }
        }

        public Button ButtonSave
        {
            get { return buttonSave; }
            set { buttonSave = value; }
        }

        public Button ButtonCancel
        {
            get { return buttonCancel; }
            set { buttonCancel = value; }
        }

        public CheckBox CheckBoxPassword
        {
            get { return checkBoxPassword; }
            set { checkBoxPassword = value; }
        }

        public TextBox TextBoxPassword
        {
            get { return textBoxPassword; }
            set { textBoxPassword = value; }
        }

        public TextBox TextBoxRepeatPassword
        {
            get { return textBoxRepeatPassword; }
            set { textBoxRepeatPassword = value; }
        }

        public TextBox TextBoxLogs
        {
            get { return textBoxLogs; }
            set { textBoxLogs = value; }
        }

        public CheckBox CheckBoxShowNotifications
        {
            get { return checkBoxShowNotifications; }
            set { checkBoxShowNotifications = value; }
        }

        public TextBox TextBoxPostpone
        {
            get { return textBoxPostpone; }
            set { textBoxPostpone = value; }
        }

        public TextBox TextBoxDatabaseLocation
        {
            get { return textBoxDatabaseLocation; }
            set { textBoxDatabaseLocation = value; }
        }

        public TextBox TextBoxLogToFile
        {
            get { return textBoxLogToFile; }
            set { textBoxLogToFile = value; }
        }

        public Button ButtonSelectDatabase
        {
            get { return buttonSelectDatabase; }
            set { buttonSelectDatabase = value; }
        }

        public Button ButtonSelectLogFile
        {
            get { return buttonSelectLogFile; }
            set { buttonSelectLogFile = value; }
        }

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void panelTopBar_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panelTopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panelTopBar_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }


        public void buttonTab_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (precedentButton == button) return;
            tabControl1.SelectedIndex = Int32.Parse((string)(button.Tag));
            button.BackColor = selectedColor;
            precedentButton.BackColor = normalColor;
            precedentButton = button;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonExit_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = Color.White;
        }

        private void buttonExit_MouseMove(object sender, MouseEventArgs e)
        {
            ((Button)sender).ForeColor = Color.Orange;
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            string traySetting = databaseService.GetSettings().trayType;
            if (traySetting.Equals("minimized") || traySetting.Equals("always"))
            {
                notifyIconApp.Visible = true;
                this.Hide();
            } else
            {
                WindowState = FormWindowState.Minimized;
            }
        }       
            
    }
}
