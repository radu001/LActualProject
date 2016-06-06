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

        public void SetAvaible(int percentage)
        {
            labelAvaible.Text = percentage + "%";
            if (percentage > 95)
            {
                labelAvaible.BackColor = Color.Red;
                return;
            }
            if (percentage > 75)
            {
                labelAvaible.BackColor = Color.Yellow;
                return;
            }
            if (percentage > 50)
            {
                labelAvaible.BackColor = Color.YellowGreen;
                return;
            }
            labelAvaible.BackColor = Color.Green;
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
