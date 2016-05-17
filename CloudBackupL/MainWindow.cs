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
        public CloudBackupsTabController cloudBackupsTabController;

        //Main Window Constructor
        public MainWindow()
        {
            instance = this;
            InitializeComponent();
            homeTabController = new HomeTabController();
            backupPlansTabController = new BackupPlansTabController();
            cloudBackupsTabController = new CloudBackupsTabController();
        }

        //Main Window Load
        private void MainWindow_Load(object sender, EventArgs e)
        {
            homeTabController.LoadClouds();
            backupPlansTabController.LoadPlans();
            cloudBackupsTabController.LoadCloudList();
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

        public ListBox ListBoxClouds
        {
            get { return this.listBoxClouds; }
            set { this.listBoxClouds = value; }
        }

        public ListView ListViewBackupsInfo
        {
            get { return this.listViewBackupsInfo; }
            set { this.listViewBackupsInfo = value; }
        }
    }
}
