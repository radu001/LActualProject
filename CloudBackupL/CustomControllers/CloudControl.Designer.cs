namespace CloudBackupL.CustomControllers
{
    partial class CloudControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxCloudImage = new System.Windows.Forms.PictureBox();
            this.labelCloudName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTotalSpace = new System.Windows.Forms.Label();
            this.labelFreeSpace = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCloudImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCloudImage
            // 
            this.pictureBoxCloudImage.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxCloudImage.Name = "pictureBoxCloudImage";
            this.pictureBoxCloudImage.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxCloudImage.TabIndex = 0;
            this.pictureBoxCloudImage.TabStop = false;
            // 
            // labelCloudName
            // 
            this.labelCloudName.AutoSize = true;
            this.labelCloudName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCloudName.Location = new System.Drawing.Point(109, 14);
            this.labelCloudName.Name = "labelCloudName";
            this.labelCloudName.Size = new System.Drawing.Size(0, 16);
            this.labelCloudName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Total space:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Free space:";
            // 
            // labelTotalSpace
            // 
            this.labelTotalSpace.AutoSize = true;
            this.labelTotalSpace.Location = new System.Drawing.Point(182, 40);
            this.labelTotalSpace.Name = "labelTotalSpace";
            this.labelTotalSpace.Size = new System.Drawing.Size(0, 13);
            this.labelTotalSpace.TabIndex = 4;
            // 
            // labelFreeSpace
            // 
            this.labelFreeSpace.AutoSize = true;
            this.labelFreeSpace.Location = new System.Drawing.Point(182, 63);
            this.labelFreeSpace.Name = "labelFreeSpace";
            this.labelFreeSpace.Size = new System.Drawing.Size(0, 13);
            this.labelFreeSpace.TabIndex = 5;
            // 
            // CloudControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelFreeSpace);
            this.Controls.Add(this.labelTotalSpace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelCloudName);
            this.Controls.Add(this.pictureBoxCloudImage);
            this.Name = "CloudControl";
            this.Size = new System.Drawing.Size(250, 106);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCloudImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCloudImage;
        private System.Windows.Forms.Label labelCloudName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTotalSpace;
        private System.Windows.Forms.Label labelFreeSpace;
    }
}
