using Newtonsoft.Json.Linq;
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
                dynamic data = JObject.Parse(cloudController.GetAccountInfo(accessToken));
                String uid = data.uid;
                if(dbService.IsCloudAlreadyInsered(uid))
                {
                    DialogResult dialogResult = MessageBox.Show("This cloud is already inserted.", "Error", MessageBoxButtons.OK);
                    this.Close();
                } else
                {
                    Cloud cloud = new Cloud();
                    cloud.id = data.uid;
                    cloud.name = textBoxCloudName.Text;
                    cloud.cloudType = "dropbox";
                    cloud.token = accessToken;
                    cloud.date = DateTime.Now;
                    dbService.InsertCloud(cloud);
                    DialogResult dialogResult = MessageBox.Show("Cloud inserted successfuly.", "Succes", MessageBoxButtons.OK);
                    this.Close();
                }

            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            this.Height += 220;
            panelCloudSelect.Visible = false;
            panelCloudLogin.Visible = true;
            var uri = cloudController.PrepareUri();
            webBrowser1.Navigate(uri);
        }
    }
}
