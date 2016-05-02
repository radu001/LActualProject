using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudBackupL
{
    public partial class AddCloudWindow : Form
    {
        private ICloud cloudController;
        DatabaseService dbService;

        public AddCloudWindow()
        {
            InitializeComponent();
            cloudController = new DropBoxController();
            dbService = new DatabaseService();
            panelCloudLogin.Visible = false;
            
        }

        private void AddCloudWindow_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonNext.Enabled = true;
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            var accessToken = cloudController.ParseUriForToken(e.Url);
            if (accessToken != null)
            {
                Cloud cloud = new Cloud();
                cloud.name = textBoxCloudName.Text;
                cloud.cloudType = "dropbox";
                cloud.token = accessToken;
                cloud.date = DateTime.Now;
                dbService.InsertCloud(cloud);
                this.Close();
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            panelCloudSelect.Visible = false;
            panelCloudLogin.Visible = true;
            var uri = cloudController.PrepareUri();
            webBrowser1.Navigate(uri);
        }
    }
}
