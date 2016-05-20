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
        public MyBackupsTabController cloudBackupsTabController;
        public ManualWorkTabController manualWorkTabController;
        public SettingsTabController settingsTabController;

        //Main Window Constructor
        public MainWindow()
        {
            instance = this;
            InitializeComponent();
            homeTabController = new HomeTabController();
            backupPlansTabController = new BackupPlansTabController();
            cloudBackupsTabController = new MyBackupsTabController();
            manualWorkTabController = new ManualWorkTabController();
            settingsTabController = new SettingsTabController();
        }

        //Main Window Load
        private void MainWindow_Load(object sender, EventArgs e)
        {
            homeTabController.LoadClouds();
            backupPlansTabController.LoadPlans();
            cloudBackupsTabController.LoadBackupPlansList();
            manualWorkTabController.LoadCloudList();
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

        public ProgressBar ProgressBarManualDownload
        {
            get { return this.progressBarManualDownload; }
            set { this.progressBarManualDownload = value; }
        }

        public Button ButtonManualUpload
        {
            get { return this.buttonManualUpload; }
            set { this.buttonManualUpload = value; }
        }

        public TextBox TextBoxChunkSize
        {
            get { return this.textBoxChunkSize; }
            set { this.textBoxChunkSize = value; }
        }
    }
}
