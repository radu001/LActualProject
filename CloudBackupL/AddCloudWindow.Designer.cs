namespace CloudBackupL
{
    partial class AddCloudWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCloudWindow));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("", 1);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelCloudSelect = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCloudName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.listViewCloudsType = new System.Windows.Forms.ListView();
            this.panelCloudLogin = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panelCloudSelect.SuspendLayout();
            this.panelCloudLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "dr1.PNG");
            this.imageList1.Images.SetKeyName(1, "or1.PNG");
            // 
            // panelCloudSelect
            // 
            this.panelCloudSelect.Controls.Add(this.label3);
            this.panelCloudSelect.Controls.Add(this.textBoxCloudName);
            this.panelCloudSelect.Controls.Add(this.label2);
            this.panelCloudSelect.Controls.Add(this.label1);
            this.panelCloudSelect.Controls.Add(this.buttonNext);
            this.panelCloudSelect.Controls.Add(this.listViewCloudsType);
            this.panelCloudSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCloudSelect.Location = new System.Drawing.Point(0, 0);
            this.panelCloudSelect.Name = "panelCloudSelect";
            this.panelCloudSelect.Size = new System.Drawing.Size(449, 316);
            this.panelCloudSelect.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(387, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Please enter a name for your cloud and select the cloud service.";
            // 
            // textBoxCloudName
            // 
            this.textBoxCloudName.Location = new System.Drawing.Point(84, 72);
            this.textBoxCloudName.Name = "textBoxCloudName";
            this.textBoxCloudName.Size = new System.Drawing.Size(177, 20);
            this.textBoxCloudName.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cloud name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select cloud";
            // 
            // buttonNext
            // 
            this.buttonNext.Enabled = false;
            this.buttonNext.Location = new System.Drawing.Point(347, 283);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 4;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // listViewCloudsType
            // 
            this.listViewCloudsType.HideSelection = false;
            this.listViewCloudsType.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewCloudsType.LargeImageList = this.imageList1;
            this.listViewCloudsType.Location = new System.Drawing.Point(12, 139);
            this.listViewCloudsType.MultiSelect = false;
            this.listViewCloudsType.Name = "listViewCloudsType";
            this.listViewCloudsType.Size = new System.Drawing.Size(425, 138);
            this.listViewCloudsType.SmallImageList = this.imageList1;
            this.listViewCloudsType.TabIndex = 3;
            this.listViewCloudsType.UseCompatibleStateImageBehavior = false;
            this.listViewCloudsType.SelectedIndexChanged += new System.EventHandler(this.listViewCloudsType_SelectedIndexChanged);
            // 
            // panelCloudLogin
            // 
            this.panelCloudLogin.Controls.Add(this.webBrowser1);
            this.panelCloudLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCloudLogin.Location = new System.Drawing.Point(0, 0);
            this.panelCloudLogin.Name = "panelCloudLogin";
            this.panelCloudLogin.Size = new System.Drawing.Size(449, 316);
            this.panelCloudLogin.TabIndex = 1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(449, 316);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // AddCloudWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 316);
            this.Controls.Add(this.panelCloudSelect);
            this.Controls.Add(this.panelCloudLogin);
            this.Name = "AddCloudWindow";
            this.Text = "AddCloud";
            this.panelCloudSelect.ResumeLayout(false);
            this.panelCloudSelect.PerformLayout();
            this.panelCloudLogin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panelCloudSelect;
        private System.Windows.Forms.ListView listViewCloudsType;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelCloudLogin;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCloudName;
        private System.Windows.Forms.Label label2;
    }
}