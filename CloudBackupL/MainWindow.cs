using CloudBackupL.CustomControllers;
using CloudBackupL.TabsControllers;
using System;
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

        //Main Window Constructor
        public MainWindow()
        {
            instance = this;
            InitializeComponent();
            homeTabController = new HomeTabController();
            backupPlansTabController = new BackupPlansTabController();
            myBackupsTabController = new MyBackupsTabController();
            manualWorkTabController = new ManualWorkTabController();
            settingsTabController = new SettingsTabController();
        }

        //Main Window Load
        private void MainWindow_Load(object sender, EventArgs e)
        {
            homeTabController.LoadClouds();
            backupPlansTabController.LoadPlans();
            myBackupsTabController.LoadBackupPlansList();
            manualWorkTabController.LoadCloudList();
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
    }
}
