using CloudBackupL.Clouds;
using CloudBackupL.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;

namespace CloudBackupL
{
    public partial class AddCloudWindow : Form
    {
        private ICloud cloudController;
        DatabaseService dbService;
        int cloudType;

        public AddCloudWindow()
        {
            InitializeComponent();
            dbService = new DatabaseService();
            panelCloudLogin.Visible = false;
        }

        private void listViewCloudsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listViewCloudsType.SelectedItems.Count == 1)
            {
                if(listViewCloudsType.SelectedIndices[0] == 0)
                {
                    cloudController = new DropBoxController();
                    cloudType = (int)CloudsEnum.DropBox;
                } else
                {
                    cloudController = new OneDriveController();
                    cloudType = (int)CloudsEnum.OneDrive;
                }
                buttonNext.Enabled = true;
            }else
            {
                buttonNext.Enabled = false;
            }
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            string accessToken;
            string refresh_token = null;
            var result = cloudController.ParseUriForToken(e.Url);
            if (result != null)
            {
                if(cloudType == (int)CloudsEnum.OneDrive)
                {
                    dynamic data1 = JObject.Parse(result);
                    refresh_token = data1.refresh_token;
                    accessToken = data1.access_token;
                } else
                {
                    accessToken = result;
                }
                CloudUserInfo cloudUserInfo = cloudController.GetAccountInfo(accessToken);

                if (dbService.IsCloudAlreadyInsered(cloudUserInfo.uid))
                {
                    DialogResult dialogResult = MessageBox.Show("This cloud is already inserted.", "Error", MessageBoxButtons.OK);
                    this.Close();
                } else
                {
                    Cloud cloud = new Cloud();
                    cloud.id = cloudUserInfo.uid;
                    cloud.name = textBoxCloudName.Text;
                    cloud.cloudType = (cloudType == (int)CloudsEnum.DropBox) ? "dropbox" : "box";
                    cloud.token = accessToken;
                    cloud.refreshToken = refresh_token;
                    cloud.date = DateTime.Now;
                    dbService.InsertCloud(cloud);
                    DialogResult dialogResult = MessageBox.Show("Cloud inserted successfuly.", "Succes", MessageBoxButtons.OK);
                    this.Close();
                }

            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBoxCloudName.Text))
            {
                MessageBox.Show("Please insert the name for the cloud.");
                return;
            }
            this.Height += 220;
            panelCloudSelect.Visible = false;
            panelCloudLogin.Visible = true;
            var uri = cloudController.PrepareUri();
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
            try
            {
                System.IO.Directory.Delete(Path, true);
            }catch (Exception){}
            webBrowser1.Navigate(uri);
        }


    }
}
