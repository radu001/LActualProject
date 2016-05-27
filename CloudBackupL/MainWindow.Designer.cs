namespace CloudBackupL
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.labelMainPlanName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewBackupQueue = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label6 = new System.Windows.Forms.Label();
            this.flowLayoutPanelClouds = new System.Windows.Forms.FlowLayoutPanel();
            this.labelMainStatus = new System.Windows.Forms.Label();
            this.progressBarMain = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAddBackupPlan = new System.Windows.Forms.Button();
            this.buttonAddCloud = new System.Windows.Forms.Button();
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelTotalPlans = new System.Windows.Forms.Label();
            this.flowLayoutPanelPlans = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxBackupPlans = new System.Windows.Forms.ListBox();
            this.listViewBackupsInfo = new System.Windows.Forms.ListView();
            this.columnBackupName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCompressedSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnRunTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.listBoxCloudsManual = new System.Windows.Forms.ListBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxManualPassword = new System.Windows.Forms.TextBox();
            this.labelManualStatus = new System.Windows.Forms.Label();
            this.buttonManualDownload = new System.Windows.Forms.Button();
            this.progressBarManual = new System.Windows.Forms.ProgressBar();
            this.listViewCloudFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonSelectDatabase = new System.Windows.Forms.Button();
            this.buttonSelectLogFile = new System.Windows.Forms.Button();
            this.textBoxDatabaseLocation = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxLogToFile = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxPreventSleep = new System.Windows.Forms.CheckBox();
            this.textBoxChunkSize = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxRepeatPassword = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.checkBoxPassword = new System.Windows.Forms.CheckBox();
            this.radioButtonTrayNever = new System.Windows.Forms.RadioButton();
            this.radioButtonTrayMinimized = new System.Windows.Forms.RadioButton();
            this.radioButtonTrayAlways = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.imageListClouds = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonTabAbout = new System.Windows.Forms.Button();
            this.buttonTabSettings = new System.Windows.Forms.Button();
            this.buttonTabManualWork = new System.Windows.Forms.Button();
            this.buttonTabMyBackups = new System.Windows.Forms.Button();
            this.buttonTabBackupPlans = new System.Windows.Forms.Button();
            this.buttonTabHome = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelTopBar = new System.Windows.Forms.Panel();
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.notifyIconApp = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panelTopBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(42, 22);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(742, 467);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.labelMainPlanName);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.listViewBackupQueue);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.flowLayoutPanelClouds);
            this.tabPage1.Controls.Add(this.labelMainStatus);
            this.tabPage1.Controls.Add(this.progressBarMain);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.buttonAddBackupPlan);
            this.tabPage1.Controls.Add(this.buttonAddCloud);
            this.tabPage1.Controls.Add(this.textBoxLogs);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(734, 437);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Home";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(64, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Executing:";
            // 
            // labelMainPlanName
            // 
            this.labelMainPlanName.BackColor = System.Drawing.Color.White;
            this.labelMainPlanName.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMainPlanName.Location = new System.Drawing.Point(12, 30);
            this.labelMainPlanName.Name = "labelMainPlanName";
            this.labelMainPlanName.Size = new System.Drawing.Size(178, 23);
            this.labelMainPlanName.TabIndex = 14;
            this.labelMainPlanName.Text = "-";
            this.labelMainPlanName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Backup Queue:";
            // 
            // listViewBackupQueue
            // 
            this.listViewBackupQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listViewBackupQueue.Location = new System.Drawing.Point(130, 85);
            this.listViewBackupQueue.Name = "listViewBackupQueue";
            this.listViewBackupQueue.Size = new System.Drawing.Size(322, 200);
            this.listViewBackupQueue.TabIndex = 12;
            this.listViewBackupQueue.UseCompatibleStateImageBehavior = false;
            this.listViewBackupQueue.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Plan Name";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Backup Time";
            this.columnHeader4.Width = 150;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(453, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Connected clouds:";
            // 
            // flowLayoutPanelClouds
            // 
            this.flowLayoutPanelClouds.AllowDrop = true;
            this.flowLayoutPanelClouds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelClouds.AutoScroll = true;
            this.flowLayoutPanelClouds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelClouds.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelClouds.Location = new System.Drawing.Point(456, 19);
            this.flowLayoutPanelClouds.Name = "flowLayoutPanelClouds";
            this.flowLayoutPanelClouds.Size = new System.Drawing.Size(278, 417);
            this.flowLayoutPanelClouds.TabIndex = 10;
            this.flowLayoutPanelClouds.WrapContents = false;
            // 
            // labelMainStatus
            // 
            this.labelMainStatus.AutoSize = true;
            this.labelMainStatus.Location = new System.Drawing.Point(248, 28);
            this.labelMainStatus.Name = "labelMainStatus";
            this.labelMainStatus.Size = new System.Drawing.Size(40, 13);
            this.labelMainStatus.TabIndex = 9;
            this.labelMainStatus.Text = "Status:";
            // 
            // progressBarMain
            // 
            this.progressBarMain.Location = new System.Drawing.Point(251, 44);
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Size = new System.Drawing.Size(199, 23);
            this.progressBarMain.TabIndex = 7;
            this.progressBarMain.Value = 100;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Logs:";
            // 
            // buttonAddBackupPlan
            // 
            this.buttonAddBackupPlan.Location = new System.Drawing.Point(12, 124);
            this.buttonAddBackupPlan.Name = "buttonAddBackupPlan";
            this.buttonAddBackupPlan.Size = new System.Drawing.Size(100, 23);
            this.buttonAddBackupPlan.TabIndex = 3;
            this.buttonAddBackupPlan.Text = "Add Backup Plan";
            this.buttonAddBackupPlan.UseVisualStyleBackColor = true;
            // 
            // buttonAddCloud
            // 
            this.buttonAddCloud.Location = new System.Drawing.Point(12, 85);
            this.buttonAddCloud.Name = "buttonAddCloud";
            this.buttonAddCloud.Size = new System.Drawing.Size(100, 23);
            this.buttonAddCloud.TabIndex = 2;
            this.buttonAddCloud.Text = "Add Cloud";
            this.buttonAddCloud.UseVisualStyleBackColor = true;
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLogs.Location = new System.Drawing.Point(0, 291);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.Size = new System.Drawing.Size(452, 146);
            this.textBoxLogs.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labelTotalPlans);
            this.tabPage2.Controls.Add(this.flowLayoutPanelPlans);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(734, 437);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Backup Plans";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelTotalPlans
            // 
            this.labelTotalPlans.AutoSize = true;
            this.labelTotalPlans.Location = new System.Drawing.Point(3, 420);
            this.labelTotalPlans.Name = "labelTotalPlans";
            this.labelTotalPlans.Size = new System.Drawing.Size(71, 13);
            this.labelTotalPlans.TabIndex = 1;
            this.labelTotalPlans.Text = "Total plans: 0";
            // 
            // flowLayoutPanelPlans
            // 
            this.flowLayoutPanelPlans.AllowDrop = true;
            this.flowLayoutPanelPlans.AutoScroll = true;
            this.flowLayoutPanelPlans.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelPlans.Name = "flowLayoutPanelPlans";
            this.flowLayoutPanelPlans.Size = new System.Drawing.Size(731, 414);
            this.flowLayoutPanelPlans.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(734, 437);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "My backups";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.listBoxBackupPlans);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewBackupsInfo);
            this.splitContainer1.Size = new System.Drawing.Size(728, 431);
            this.splitContainer1.SplitterDistance = 178;
            this.splitContainer1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(36, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "My Backup Plans";
            // 
            // listBoxBackupPlans
            // 
            this.listBoxBackupPlans.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBoxBackupPlans.FormattingEnabled = true;
            this.listBoxBackupPlans.IntegralHeight = false;
            this.listBoxBackupPlans.Location = new System.Drawing.Point(0, 27);
            this.listBoxBackupPlans.Name = "listBoxBackupPlans";
            this.listBoxBackupPlans.Size = new System.Drawing.Size(178, 404);
            this.listBoxBackupPlans.TabIndex = 1;
            // 
            // listViewBackupsInfo
            // 
            this.listViewBackupsInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnBackupName,
            this.columnDate,
            this.columnSize,
            this.columnCompressedSize,
            this.columnRunTime});
            this.listViewBackupsInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewBackupsInfo.FullRowSelect = true;
            this.listViewBackupsInfo.Location = new System.Drawing.Point(0, 0);
            this.listViewBackupsInfo.Name = "listViewBackupsInfo";
            this.listViewBackupsInfo.Size = new System.Drawing.Size(546, 431);
            this.listViewBackupsInfo.TabIndex = 0;
            this.listViewBackupsInfo.UseCompatibleStateImageBehavior = false;
            this.listViewBackupsInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnBackupName
            // 
            this.columnBackupName.Text = "Backup Path";
            this.columnBackupName.Width = 150;
            // 
            // columnDate
            // 
            this.columnDate.Text = "Date";
            this.columnDate.Width = 115;
            // 
            // columnSize
            // 
            this.columnSize.Text = "Size";
            this.columnSize.Width = 103;
            // 
            // columnCompressedSize
            // 
            this.columnCompressedSize.Text = "Compressed Size";
            this.columnCompressedSize.Width = 121;
            // 
            // columnRunTime
            // 
            this.columnRunTime.Text = "Run Time";
            this.columnRunTime.Width = 100;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.splitContainer2);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(734, 437);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Manual Work";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.listBoxCloudsManual);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label16);
            this.splitContainer2.Panel2.Controls.Add(this.textBoxManualPassword);
            this.splitContainer2.Panel2.Controls.Add(this.labelManualStatus);
            this.splitContainer2.Panel2.Controls.Add(this.buttonManualDownload);
            this.splitContainer2.Panel2.Controls.Add(this.progressBarManual);
            this.splitContainer2.Panel2.Controls.Add(this.listViewCloudFiles);
            this.splitContainer2.Size = new System.Drawing.Size(728, 431);
            this.splitContainer2.SplitterDistance = 149;
            this.splitContainer2.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "My Clouds";
            // 
            // listBoxCloudsManual
            // 
            this.listBoxCloudsManual.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBoxCloudsManual.FormattingEnabled = true;
            this.listBoxCloudsManual.IntegralHeight = false;
            this.listBoxCloudsManual.Location = new System.Drawing.Point(0, 27);
            this.listBoxCloudsManual.Name = "listBoxCloudsManual";
            this.listBoxCloudsManual.Size = new System.Drawing.Size(149, 404);
            this.listBoxCloudsManual.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(402, 70);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Password:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxManualPassword
            // 
            this.textBoxManualPassword.Location = new System.Drawing.Point(402, 86);
            this.textBoxManualPassword.Name = "textBoxManualPassword";
            this.textBoxManualPassword.Size = new System.Drawing.Size(167, 20);
            this.textBoxManualPassword.TabIndex = 4;
            this.textBoxManualPassword.UseSystemPasswordChar = true;
            // 
            // labelManualStatus
            // 
            this.labelManualStatus.AutoSize = true;
            this.labelManualStatus.Location = new System.Drawing.Point(402, 7);
            this.labelManualStatus.Name = "labelManualStatus";
            this.labelManualStatus.Size = new System.Drawing.Size(40, 13);
            this.labelManualStatus.TabIndex = 3;
            this.labelManualStatus.Text = "Status:";
            // 
            // buttonManualDownload
            // 
            this.buttonManualDownload.Location = new System.Drawing.Point(402, 131);
            this.buttonManualDownload.Name = "buttonManualDownload";
            this.buttonManualDownload.Size = new System.Drawing.Size(167, 23);
            this.buttonManualDownload.TabIndex = 2;
            this.buttonManualDownload.Text = "Download";
            this.buttonManualDownload.UseVisualStyleBackColor = true;
            // 
            // progressBarManual
            // 
            this.progressBarManual.Location = new System.Drawing.Point(402, 27);
            this.progressBarManual.Name = "progressBarManual";
            this.progressBarManual.Size = new System.Drawing.Size(167, 23);
            this.progressBarManual.TabIndex = 1;
            // 
            // listViewCloudFiles
            // 
            this.listViewCloudFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewCloudFiles.Dock = System.Windows.Forms.DockStyle.Left;
            this.listViewCloudFiles.FullRowSelect = true;
            this.listViewCloudFiles.HideSelection = false;
            this.listViewCloudFiles.Location = new System.Drawing.Point(0, 0);
            this.listViewCloudFiles.MultiSelect = false;
            this.listViewCloudFiles.Name = "listViewCloudFiles";
            this.listViewCloudFiles.Scrollable = false;
            this.listViewCloudFiles.Size = new System.Drawing.Size(396, 431);
            this.listViewCloudFiles.TabIndex = 0;
            this.listViewCloudFiles.UseCompatibleStateImageBehavior = false;
            this.listViewCloudFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Date";
            this.columnHeader2.Width = 200;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.panel1);
            this.tabPage5.Location = new System.Drawing.Point(4, 26);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(734, 437);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Settings";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(-1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 427);
            this.panel1.TabIndex = 1;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(632, 396);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(538, 396);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonSelectDatabase);
            this.groupBox4.Controls.Add(this.buttonSelectLogFile);
            this.groupBox4.Controls.Add(this.textBoxDatabaseLocation);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.textBoxLogToFile);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(378, 200);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(339, 190);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Other";
            // 
            // buttonSelectDatabase
            // 
            this.buttonSelectDatabase.Location = new System.Drawing.Point(286, 56);
            this.buttonSelectDatabase.Name = "buttonSelectDatabase";
            this.buttonSelectDatabase.Size = new System.Drawing.Size(28, 20);
            this.buttonSelectDatabase.TabIndex = 16;
            this.buttonSelectDatabase.Text = "...";
            this.buttonSelectDatabase.UseVisualStyleBackColor = true;
            // 
            // buttonSelectLogFile
            // 
            this.buttonSelectLogFile.Location = new System.Drawing.Point(286, 16);
            this.buttonSelectLogFile.Name = "buttonSelectLogFile";
            this.buttonSelectLogFile.Size = new System.Drawing.Size(28, 20);
            this.buttonSelectLogFile.TabIndex = 15;
            this.buttonSelectLogFile.Text = "...";
            this.buttonSelectLogFile.UseVisualStyleBackColor = true;
            // 
            // textBoxDatabaseLocation
            // 
            this.textBoxDatabaseLocation.Location = new System.Drawing.Point(105, 57);
            this.textBoxDatabaseLocation.Name = "textBoxDatabaseLocation";
            this.textBoxDatabaseLocation.Size = new System.Drawing.Size(175, 20);
            this.textBoxDatabaseLocation.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Database location:";
            // 
            // textBoxLogToFile
            // 
            this.textBoxLogToFile.Location = new System.Drawing.Point(105, 17);
            this.textBoxLogToFile.Name = "textBoxLogToFile";
            this.textBoxLogToFile.Size = new System.Drawing.Size(175, 20);
            this.textBoxLogToFile.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Log to file:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.checkBoxPreventSleep);
            this.groupBox3.Controls.Add(this.textBoxChunkSize);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(15, 200);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(340, 190);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Advanced";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(175, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "MB";
            // 
            // checkBoxPreventSleep
            // 
            this.checkBoxPreventSleep.AutoSize = true;
            this.checkBoxPreventSleep.Location = new System.Drawing.Point(10, 52);
            this.checkBoxPreventSleep.Name = "checkBoxPreventSleep";
            this.checkBoxPreventSleep.Size = new System.Drawing.Size(273, 17);
            this.checkBoxPreventSleep.TabIndex = 6;
            this.checkBoxPreventSleep.Text = "Prevent computer from sleeping while plan is running";
            this.checkBoxPreventSleep.UseVisualStyleBackColor = true;
            // 
            // textBoxChunkSize
            // 
            this.textBoxChunkSize.Location = new System.Drawing.Point(77, 17);
            this.textBoxChunkSize.Name = "textBoxChunkSize";
            this.textBoxChunkSize.Size = new System.Drawing.Size(92, 20);
            this.textBoxChunkSize.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Chunk size:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(377, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 190);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connection";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(151, 57);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(119, 20);
            this.textBox5.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Time between attempts (ms):";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(151, 17);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(119, 20);
            this.textBox4.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Number of attempts:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxRepeatPassword);
            this.groupBox1.Controls.Add(this.textBoxPassword);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.checkBoxPassword);
            this.groupBox1.Controls.Add(this.radioButtonTrayNever);
            this.groupBox1.Controls.Add(this.radioButtonTrayMinimized);
            this.groupBox1.Controls.Add(this.radioButtonTrayAlways);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Location = new System.Drawing.Point(15, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 190);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // textBoxRepeatPassword
            // 
            this.textBoxRepeatPassword.Location = new System.Drawing.Point(117, 157);
            this.textBoxRepeatPassword.Name = "textBoxRepeatPassword";
            this.textBoxRepeatPassword.Size = new System.Drawing.Size(143, 20);
            this.textBoxRepeatPassword.TabIndex = 9;
            this.textBoxRepeatPassword.Text = "tempPass-852";
            this.textBoxRepeatPassword.UseSystemPasswordChar = true;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(117, 133);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(143, 20);
            this.textBoxPassword.TabIndex = 8;
            this.textBoxPassword.Text = "tempPass-852";
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 160);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Repeat password:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(20, 136);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Password:";
            // 
            // checkBoxPassword
            // 
            this.checkBoxPassword.AutoSize = true;
            this.checkBoxPassword.Location = new System.Drawing.Point(10, 110);
            this.checkBoxPassword.Name = "checkBoxPassword";
            this.checkBoxPassword.Size = new System.Drawing.Size(169, 17);
            this.checkBoxPassword.TabIndex = 5;
            this.checkBoxPassword.Text = "Protect the app with password";
            this.checkBoxPassword.UseVisualStyleBackColor = true;
            // 
            // radioButtonTrayNever
            // 
            this.radioButtonTrayNever.AutoSize = true;
            this.radioButtonTrayNever.Location = new System.Drawing.Point(23, 83);
            this.radioButtonTrayNever.Name = "radioButtonTrayNever";
            this.radioButtonTrayNever.Size = new System.Drawing.Size(54, 17);
            this.radioButtonTrayNever.TabIndex = 3;
            this.radioButtonTrayNever.TabStop = true;
            this.radioButtonTrayNever.Text = "Never";
            this.radioButtonTrayNever.UseVisualStyleBackColor = true;
            // 
            // radioButtonTrayMinimized
            // 
            this.radioButtonTrayMinimized.AutoSize = true;
            this.radioButtonTrayMinimized.Location = new System.Drawing.Point(23, 60);
            this.radioButtonTrayMinimized.Name = "radioButtonTrayMinimized";
            this.radioButtonTrayMinimized.Size = new System.Drawing.Size(123, 17);
            this.radioButtonTrayMinimized.TabIndex = 2;
            this.radioButtonTrayMinimized.TabStop = true;
            this.radioButtonTrayMinimized.Text = "Only when minimized";
            this.radioButtonTrayMinimized.UseVisualStyleBackColor = true;
            // 
            // radioButtonTrayAlways
            // 
            this.radioButtonTrayAlways.AutoSize = true;
            this.radioButtonTrayAlways.Location = new System.Drawing.Point(23, 36);
            this.radioButtonTrayAlways.Name = "radioButtonTrayAlways";
            this.radioButtonTrayAlways.Size = new System.Drawing.Size(58, 17);
            this.radioButtonTrayAlways.TabIndex = 1;
            this.radioButtonTrayAlways.TabStop = true;
            this.radioButtonTrayAlways.Text = "Always";
            this.radioButtonTrayAlways.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(162, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Show the program in system tray:";
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 26);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(734, 437);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "About";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // imageListClouds
            // 
            this.imageListClouds.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListClouds.ImageStream")));
            this.imageListClouds.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListClouds.Images.SetKeyName(0, "dr1.PNG");
            this.imageListClouds.Images.SetKeyName(1, "or1.PNG");
            this.imageListClouds.Images.SetKeyName(2, "dropbox.png");
            this.imageListClouds.Images.SetKeyName(3, "drive.png");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(42)))), ((int)(((byte)(55)))));
            this.panel2.Controls.Add(this.buttonTabAbout);
            this.panel2.Controls.Add(this.buttonTabSettings);
            this.panel2.Controls.Add(this.buttonTabManualWork);
            this.panel2.Controls.Add(this.buttonTabMyBackups);
            this.panel2.Controls.Add(this.buttonTabBackupPlans);
            this.panel2.Controls.Add(this.buttonTabHome);
            this.panel2.Location = new System.Drawing.Point(0, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(156, 445);
            this.panel2.TabIndex = 1;
            // 
            // buttonTabAbout
            // 
            this.buttonTabAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(77)))), ((int)(((byte)(101)))));
            this.buttonTabAbout.FlatAppearance.BorderSize = 0;
            this.buttonTabAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTabAbout.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.buttonTabAbout.ForeColor = System.Drawing.Color.White;
            this.buttonTabAbout.Location = new System.Drawing.Point(-7, 250);
            this.buttonTabAbout.Name = "buttonTabAbout";
            this.buttonTabAbout.Size = new System.Drawing.Size(172, 51);
            this.buttonTabAbout.TabIndex = 5;
            this.buttonTabAbout.Tag = "5";
            this.buttonTabAbout.Text = "        About";
            this.buttonTabAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTabAbout.UseVisualStyleBackColor = false;
            this.buttonTabAbout.Click += new System.EventHandler(this.buttonTab_Click);
            // 
            // buttonTabSettings
            // 
            this.buttonTabSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(77)))), ((int)(((byte)(101)))));
            this.buttonTabSettings.FlatAppearance.BorderSize = 0;
            this.buttonTabSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTabSettings.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.buttonTabSettings.ForeColor = System.Drawing.Color.White;
            this.buttonTabSettings.Location = new System.Drawing.Point(-7, 200);
            this.buttonTabSettings.Name = "buttonTabSettings";
            this.buttonTabSettings.Size = new System.Drawing.Size(172, 51);
            this.buttonTabSettings.TabIndex = 4;
            this.buttonTabSettings.Tag = "4";
            this.buttonTabSettings.Text = "        Settings";
            this.buttonTabSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTabSettings.UseVisualStyleBackColor = false;
            this.buttonTabSettings.Click += new System.EventHandler(this.buttonTab_Click);
            // 
            // buttonTabManualWork
            // 
            this.buttonTabManualWork.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(77)))), ((int)(((byte)(101)))));
            this.buttonTabManualWork.FlatAppearance.BorderSize = 0;
            this.buttonTabManualWork.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTabManualWork.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.buttonTabManualWork.ForeColor = System.Drawing.Color.White;
            this.buttonTabManualWork.Location = new System.Drawing.Point(-7, 150);
            this.buttonTabManualWork.Name = "buttonTabManualWork";
            this.buttonTabManualWork.Size = new System.Drawing.Size(172, 51);
            this.buttonTabManualWork.TabIndex = 3;
            this.buttonTabManualWork.Tag = "3";
            this.buttonTabManualWork.Text = "        Manual Work";
            this.buttonTabManualWork.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTabManualWork.UseVisualStyleBackColor = false;
            this.buttonTabManualWork.Click += new System.EventHandler(this.buttonTab_Click);
            // 
            // buttonTabMyBackups
            // 
            this.buttonTabMyBackups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(77)))), ((int)(((byte)(101)))));
            this.buttonTabMyBackups.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonTabMyBackups.FlatAppearance.BorderSize = 0;
            this.buttonTabMyBackups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTabMyBackups.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.buttonTabMyBackups.ForeColor = System.Drawing.Color.White;
            this.buttonTabMyBackups.Location = new System.Drawing.Point(-7, 100);
            this.buttonTabMyBackups.Name = "buttonTabMyBackups";
            this.buttonTabMyBackups.Size = new System.Drawing.Size(172, 51);
            this.buttonTabMyBackups.TabIndex = 2;
            this.buttonTabMyBackups.Tag = "2";
            this.buttonTabMyBackups.Text = "        My Backups";
            this.buttonTabMyBackups.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTabMyBackups.UseVisualStyleBackColor = false;
            this.buttonTabMyBackups.Click += new System.EventHandler(this.buttonTab_Click);
            // 
            // buttonTabBackupPlans
            // 
            this.buttonTabBackupPlans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(77)))), ((int)(((byte)(101)))));
            this.buttonTabBackupPlans.FlatAppearance.BorderSize = 0;
            this.buttonTabBackupPlans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTabBackupPlans.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTabBackupPlans.ForeColor = System.Drawing.Color.White;
            this.buttonTabBackupPlans.Location = new System.Drawing.Point(-8, 50);
            this.buttonTabBackupPlans.Name = "buttonTabBackupPlans";
            this.buttonTabBackupPlans.Size = new System.Drawing.Size(172, 51);
            this.buttonTabBackupPlans.TabIndex = 1;
            this.buttonTabBackupPlans.Tag = "1";
            this.buttonTabBackupPlans.Text = "        Backup Plans";
            this.buttonTabBackupPlans.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTabBackupPlans.UseVisualStyleBackColor = false;
            this.buttonTabBackupPlans.Click += new System.EventHandler(this.buttonTab_Click);
            // 
            // buttonTabHome
            // 
            this.buttonTabHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(149)))), ((int)(((byte)(172)))));
            this.buttonTabHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonTabHome.FlatAppearance.BorderSize = 0;
            this.buttonTabHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTabHome.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTabHome.ForeColor = System.Drawing.Color.White;
            this.buttonTabHome.Location = new System.Drawing.Point(-7, 0);
            this.buttonTabHome.Name = "buttonTabHome";
            this.buttonTabHome.Size = new System.Drawing.Size(172, 50);
            this.buttonTabHome.TabIndex = 0;
            this.buttonTabHome.Tag = "0";
            this.buttonTabHome.Text = "        Home";
            this.buttonTabHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTabHome.UseVisualStyleBackColor = false;
            this.buttonTabHome.Click += new System.EventHandler(this.buttonTab_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(115)))), ((int)(((byte)(0)))));
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(156, 56);
            this.panel3.TabIndex = 0;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTopBar_MouseDown);
            this.panel3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTopBar_MouseMove);
            this.panel3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTopBar_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CloudBackupL.Properties.Resources.upload_512;
            this.pictureBox1.Location = new System.Drawing.Point(28, -9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTopBar_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTopBar_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTopBar_MouseUp);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tabControl1);
            this.panel4.Location = new System.Drawing.Point(155, 29);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(742, 467);
            this.panel4.TabIndex = 2;
            // 
            // panelTopBar
            // 
            this.panelTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(42)))), ((int)(((byte)(55)))));
            this.panelTopBar.Controls.Add(this.buttonMinimize);
            this.panelTopBar.Controls.Add(this.buttonExit);
            this.panelTopBar.Controls.Add(this.label17);
            this.panelTopBar.Location = new System.Drawing.Point(155, 0);
            this.panelTopBar.Name = "panelTopBar";
            this.panelTopBar.Size = new System.Drawing.Size(742, 23);
            this.panelTopBar.TabIndex = 3;
            this.panelTopBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTopBar_MouseDown);
            this.panelTopBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTopBar_MouseMove);
            this.panelTopBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTopBar_MouseUp);
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.BackColor = System.Drawing.Color.Transparent;
            this.buttonMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMinimize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.buttonMinimize.FlatAppearance.BorderSize = 0;
            this.buttonMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimize.Font = new System.Drawing.Font("Lucida Fax", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMinimize.ForeColor = System.Drawing.Color.White;
            this.buttonMinimize.Location = new System.Drawing.Point(646, 12);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(39, 37);
            this.buttonMinimize.TabIndex = 2;
            this.buttonMinimize.Text = "-";
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            this.buttonMinimize.MouseLeave += new System.EventHandler(this.buttonExit_MouseLeave);
            this.buttonMinimize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonExit_MouseMove);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Transparent;
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Lucida Fax", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.White;
            this.buttonExit.Location = new System.Drawing.Point(691, 12);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(39, 37);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "X";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.button3_Click);
            this.buttonExit.MouseLeave += new System.EventHandler(this.buttonExit_MouseLeave);
            this.buttonExit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonExit_MouseMove);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Century Schoolbook", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(7, 15);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(173, 25);
            this.label17.TabIndex = 0;
            this.label17.Text = "Secure Backup";
            this.label17.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTopBar_MouseDown);
            this.label17.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTopBar_MouseMove);
            this.label17.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTopBar_MouseUp);
            // 
            // notifyIconApp
            // 
            this.notifyIconApp.Text = "notifyIcon1";
            this.notifyIconApp.Visible = true;
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(42)))), ((int)(((byte)(55)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(895, 495);
            this.ControlBox = false;
            this.Controls.Add(this.panelTopBar);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Secure Backup";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panelTopBar.ResumeLayout(false);
            this.panelTopBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TextBox textBoxLogs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAddBackupPlan;
        private System.Windows.Forms.Button buttonAddCloud;
        private System.Windows.Forms.Label labelMainStatus;
        private System.Windows.Forms.ProgressBar progressBarMain;
        private System.Windows.Forms.ListBox listBoxBackupPlans;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listViewBackupsInfo;
        private System.Windows.Forms.ColumnHeader columnBackupName;
        private System.Windows.Forms.ColumnHeader columnDate;
        private System.Windows.Forms.ColumnHeader columnSize;
        private System.Windows.Forms.ColumnHeader columnCompressedSize;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox listBoxCloudsManual;
        private System.Windows.Forms.ListView listViewCloudFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ImageList imageListClouds;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelClouds;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPlans;
        private System.Windows.Forms.Label labelTotalPlans;
        private System.Windows.Forms.ColumnHeader columnRunTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonSelectDatabase;
        private System.Windows.Forms.Button buttonSelectLogFile;
        private System.Windows.Forms.TextBox textBoxDatabaseLocation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxLogToFile;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxPreventSleep;
        private System.Windows.Forms.TextBox textBoxChunkSize;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxRepeatPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBoxPassword;
        private System.Windows.Forms.RadioButton radioButtonTrayNever;
        private System.Windows.Forms.RadioButton radioButtonTrayMinimized;
        private System.Windows.Forms.RadioButton radioButtonTrayAlways;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button buttonManualDownload;
        private System.Windows.Forms.ProgressBar progressBarManual;
        private System.Windows.Forms.Label labelManualStatus;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxManualPassword;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonTabAbout;
        private System.Windows.Forms.Button buttonTabSettings;
        private System.Windows.Forms.Button buttonTabManualWork;
        private System.Windows.Forms.Button buttonTabMyBackups;
        private System.Windows.Forms.Button buttonTabBackupPlans;
        private System.Windows.Forms.Button buttonTabHome;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelTopBar;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewBackupQueue;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label labelMainPlanName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NotifyIcon notifyIconApp;
    }
}

