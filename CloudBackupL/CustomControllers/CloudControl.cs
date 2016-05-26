using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudBackupL.CustomControllers
{
    public partial class CloudControl : UserControl
    {
        public event EventHandler OnUserControlDeleteCloudButtonClicked;
        public CloudControl()
        {
            InitializeComponent();
            buttonDelete.Click += (s, e) =>
            {
                if (OnUserControlDeleteCloudButtonClicked != null)
                    OnUserControlDeleteCloudButtonClicked(this, e);
            };
        }

        public Label LabelFreeSpace
        {
            get { return this.labelFreeSpace; }
            set { this.labelFreeSpace = value; }
        }
        public Label LabelTotalSpace
        {
            get { return this.labelTotalSpace; }
            set { this.labelTotalSpace = value; }
        }
        public Label LabelCloudName
        {
            get { return this.labelCloudName; }
            set { this.labelCloudName = value; }
        }
        public PictureBox PictureBoxCloudImage
        {
            get { return this.pictureBoxCloudImage; }
            set { this.pictureBoxCloudImage = value; }
        }

        public Label LabelId
        {
            get { return this.labelId; }
            set { this.labelId = value; }
        }

    }
}
