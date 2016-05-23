using System;
using System.Windows.Forms;

namespace CloudBackupL.CustomControllers
{
    public partial class PlanControl : UserControl
    {
        public event EventHandler OnUserControlDeletePlanButtonClicked;
        public event EventHandler OnUserControlRunNowButtonClicked;
        public event EventHandler OnUserControlDownloadButtonClicked;
        public event EventHandler OnUserControlEditButtonClicked;
        public event EventHandler OnUserControlRestoreButtonClicked;
        public event EventHandler OnUserControlViewHystoryButtonClicked;

        public PlanControl()
        {
            InitializeComponent();
            linkLabelDeletePlan.LinkClicked += (s, e) =>
            {
                    OnUserControlDeletePlanButtonClicked(this, e);
            };

            buttonRunNow.Click += (s, e) =>
            {
                    OnUserControlRunNowButtonClicked(this, e);
            };

            linkLabelDownload.LinkClicked += (s, e) =>
            {
                    OnUserControlDownloadButtonClicked(this, e);
            };

            linkLabelEdit.LinkClicked += (s, e) =>
            {
                    OnUserControlEditButtonClicked(this, e);
            };

            linkLabelViewHistory.LinkClicked += (s, e) =>
            {
                OnUserControlViewHystoryButtonClicked(this, e);
            };


            linkLabelRestoreFiles.LinkClicked += (s, e) =>
            {
                OnUserControlRestoreButtonClicked(this, e);
            };
        }

        public void DisableActions()
        {
            linkLabelDeletePlan.Enabled = false;
            linkLabelDownload.Enabled = false;
            linkLabelEdit.Enabled = false;
            linkLabelRestoreFiles.Enabled = false;
            buttonRunNow.Enabled = false;
        }

        public void DisableActionsWhenNoBackup()
        {
            linkLabelDownload.Enabled = false;
            linkLabelRestoreFiles.Enabled = false;
        }

        public Label LabelBackupName 
        {
            get { return this.labelBackupName; }
            set { this.labelBackupName = value; }
        }
        public Label LabelCloudName
        {
            get { return this.labelCloudName; }
            set { this.labelCloudName = value; }
        }
        public Label LabelCreated
        {
            get { return this.labelCreated; }
            set { this.labelCreated = value; }
        }
        public Label LabelCurrentStatus
        {
            get { return this.labelCurrentStatus; }
            set { this.labelCurrentStatus = value; }
        }
        public Label LabelFolderPath
        {
            get { return this.labelFolderPath; }
            set { this.labelFolderPath = value; }
        }
        public Label LabelLastDuration
        {
            get { return this.labelLastDuration; }
            set { this.labelLastDuration = value; }
        }
        public Label LabelLastResult
        {
            get { return this.labelLastResult; }
            set { this.labelLastResult = value; }
        }
        public Label LabelScheduleTime
        {
            get { return this.labelScheduleTime; }
            set { this.labelScheduleTime = value; }
        }
        public Label LabelScheduleType
        {
            get { return this.labelScheduleType; }
            set { this.labelScheduleType = value; }
        }
        public Label LabelLastRun
        {
            get { return this.labelLastRun; }
            set { this.labelLastRun = value; }
        }
        public Label LabelPlanId
        {
            get { return this.labelPlanId; }
            set { this.labelPlanId = value; }
        }

        public ProgressBar ProgressBarArchiving
        {
            get { return this.progressBarArchiving; }
            set { this.progressBarArchiving = value; }
        }

        public Label LabelStatus
        {
            get { return this.labelStatus ; }
            set { this.labelStatus = value; }
        }
    }
}
