﻿namespace CloudBackupL.CustomControllers
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
            this.buttonDelete = new System.Windows.Forms.Button();
            this.labelId = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelAvaible = new System.Windows.Forms.Label();
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
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(199, 80);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(48, 23);
            this.buttonDelete.TabIndex = 6;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Enabled = false;
            this.labelId.Location = new System.Drawing.Point(110, 89);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(0, 13);
            this.labelId.TabIndex = 7;
            this.labelId.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Used: ";
            // 
            // labelAvaible
            // 
            this.labelAvaible.AutoSize = true;
            this.labelAvaible.BackColor = System.Drawing.Color.Transparent;
            this.labelAvaible.Location = new System.Drawing.Point(158, 85);
            this.labelAvaible.Name = "labelAvaible";
            this.labelAvaible.Size = new System.Drawing.Size(0, 13);
            this.labelAvaible.TabIndex = 9;
            // 
            // CloudControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelAvaible);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelId);
            this.Controls.Add(this.buttonDelete);
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
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelAvaible;
    }
}
