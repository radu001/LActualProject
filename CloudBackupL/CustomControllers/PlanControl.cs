using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudBackupL.CustomControllers
{
    public partial class PlanControl : UserControl
    {
        public event EventHandler OnUserControlDeletePlanButtonClicked;
        public event EventHandler OnUserControlRunNowButtonClicked;

        public PlanControl()
        {
            InitializeComponent();
            linkLabelDeletePlan.LinkClicked += (s, e) =>
        {
            if (OnUserControlDeletePlanButtonClicked != null)
                OnUserControlDeletePlanButtonClicked(this, e);
        };

            buttonRunNow.Click += (s, e) =>
            {
                if (OnUserControlRunNowButtonClicked != null)
                    OnUserControlRunNowButtonClicked(this, e);
            };
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
    }
}
