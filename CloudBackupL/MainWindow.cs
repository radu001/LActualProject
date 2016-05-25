using CloudBackupL.CustomControllers;
using CloudBackupL.TabsControllers;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows;

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
            homeTabController = new HomeTabController();
            backupPlansTabController = new BackupPlansTabController();
            myBackupsTabController = new MyBackupsTabController();
            manualWorkTabController = new ManualWorkTabController();
            settingsTabController = new SettingsTabController();
            selectedColor = buttonTabHome.BackColor;
            normalColor = buttonTabBackupPlans.BackColor;
            precedentButton = buttonTabHome;
            this.Paint += MainWindow_Paint;
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black, 3),
                            this.DisplayRectangle);
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
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
