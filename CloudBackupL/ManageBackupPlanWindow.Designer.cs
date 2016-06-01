namespace CloudBackupL
{
    partial class ManageBackupPlanWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageBackupPlanWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxMonthOrWeekDay = new System.Windows.Forms.ComboBox();
            this.radioButtonNo = new System.Windows.Forms.RadioButton();
            this.radioButtonYes = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonComplete = new System.Windows.Forms.Button();
            this.buttonBrowseFolder = new System.Windows.Forms.Button();
            this.textBoxFolderPath = new System.Windows.Forms.TextBox();
            this.dateTimePickerScheduleTime = new System.Windows.Forms.DateTimePicker();
            this.comboBoxScheduleType = new System.Windows.Forms.ComboBox();
            this.comboBoxClouds = new System.Windows.Forms.ComboBox();
            this.textBoxPlanName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.labelScheduleTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxMonthOrWeekDay);
            this.panel1.Controls.Add(this.radioButtonNo);
            this.panel1.Controls.Add(this.radioButtonYes);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonComplete);
            this.panel1.Controls.Add(this.buttonBrowseFolder);
            this.panel1.Controls.Add(this.textBoxFolderPath);
            this.panel1.Controls.Add(this.dateTimePickerScheduleTime);
            this.panel1.Controls.Add(this.comboBoxScheduleType);
            this.panel1.Controls.Add(this.comboBoxClouds);
            this.panel1.Controls.Add(this.textBoxPlanName);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.labelScheduleTime);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 307);
            this.panel1.TabIndex = 0;
            // 
            // comboBoxMonthOrWeekDay
            // 
            this.comboBoxMonthOrWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMonthOrWeekDay.Enabled = false;
            this.comboBoxMonthOrWeekDay.FormattingEnabled = true;
            this.comboBoxMonthOrWeekDay.Location = new System.Drawing.Point(113, 195);
            this.comboBoxMonthOrWeekDay.Name = "comboBoxMonthOrWeekDay";
            this.comboBoxMonthOrWeekDay.Size = new System.Drawing.Size(87, 21);
            this.comboBoxMonthOrWeekDay.TabIndex = 19;
            // 
            // radioButtonNo
            // 
            this.radioButtonNo.AutoSize = true;
            this.radioButtonNo.Checked = true;
            this.radioButtonNo.Location = new System.Drawing.Point(309, 167);
            this.radioButtonNo.Name = "radioButtonNo";
            this.radioButtonNo.Size = new System.Drawing.Size(39, 17);
            this.radioButtonNo.TabIndex = 18;
            this.radioButtonNo.TabStop = true;
            this.radioButtonNo.Text = "No";
            this.radioButtonNo.UseVisualStyleBackColor = true;
            // 
            // radioButtonYes
            // 
            this.radioButtonYes.AutoSize = true;
            this.radioButtonYes.Location = new System.Drawing.Point(309, 144);
            this.radioButtonYes.Name = "radioButtonYes";
            this.radioButtonYes.Size = new System.Drawing.Size(43, 17);
            this.radioButtonYes.TabIndex = 17;
            this.radioButtonYes.Text = "Yes";
            this.radioButtonYes.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(306, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Override Backup:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(351, 272);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonComplete
            // 
            this.buttonComplete.Location = new System.Drawing.Point(469, 272);
            this.buttonComplete.Name = "buttonComplete";
            this.buttonComplete.Size = new System.Drawing.Size(75, 23);
            this.buttonComplete.TabIndex = 12;
            this.buttonComplete.Text = "Complete";
            this.buttonComplete.UseVisualStyleBackColor = true;
            this.buttonComplete.Click += new System.EventHandler(this.buttonComplete_Click);
            // 
            // buttonBrowseFolder
            // 
            this.buttonBrowseFolder.Location = new System.Drawing.Point(503, 73);
            this.buttonBrowseFolder.Name = "buttonBrowseFolder";
            this.buttonBrowseFolder.Size = new System.Drawing.Size(24, 24);
            this.buttonBrowseFolder.TabIndex = 11;
            this.buttonBrowseFolder.Text = "...";
            this.buttonBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // textBoxFolderPath
            // 
            this.textBoxFolderPath.Location = new System.Drawing.Point(351, 75);
            this.textBoxFolderPath.Name = "textBoxFolderPath";
            this.textBoxFolderPath.ReadOnly = true;
            this.textBoxFolderPath.Size = new System.Drawing.Size(146, 20);
            this.textBoxFolderPath.TabIndex = 10;
            // 
            // dateTimePickerScheduleTime
            // 
            this.dateTimePickerScheduleTime.Checked = false;
            this.dateTimePickerScheduleTime.CustomFormat = "";
            this.dateTimePickerScheduleTime.Enabled = false;
            this.dateTimePickerScheduleTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerScheduleTime.Location = new System.Drawing.Point(204, 195);
            this.dateTimePickerScheduleTime.Name = "dateTimePickerScheduleTime";
            this.dateTimePickerScheduleTime.ShowUpDown = true;
            this.dateTimePickerScheduleTime.Size = new System.Drawing.Size(83, 20);
            this.dateTimePickerScheduleTime.TabIndex = 9;
            this.dateTimePickerScheduleTime.Value = new System.DateTime(2016, 5, 26, 13, 6, 0, 0);
            // 
            // comboBoxScheduleType
            // 
            this.comboBoxScheduleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleType.FormattingEnabled = true;
            this.comboBoxScheduleType.Items.AddRange(new object[] {
            "Manual",
            "Daily",
            "Weekly",
            "Monthly"});
            this.comboBoxScheduleType.Location = new System.Drawing.Point(113, 155);
            this.comboBoxScheduleType.Name = "comboBoxScheduleType";
            this.comboBoxScheduleType.Size = new System.Drawing.Size(174, 21);
            this.comboBoxScheduleType.TabIndex = 8;
            this.comboBoxScheduleType.SelectedIndexChanged += new System.EventHandler(this.comboBoxScheduleType_SelectedIndexChanged);
            // 
            // comboBoxClouds
            // 
            this.comboBoxClouds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClouds.FormattingEnabled = true;
            this.comboBoxClouds.Location = new System.Drawing.Point(113, 115);
            this.comboBoxClouds.Name = "comboBoxClouds";
            this.comboBoxClouds.Size = new System.Drawing.Size(174, 21);
            this.comboBoxClouds.TabIndex = 7;
            // 
            // textBoxPlanName
            // 
            this.textBoxPlanName.Location = new System.Drawing.Point(113, 75);
            this.textBoxPlanName.Name = "textBoxPlanName";
            this.textBoxPlanName.Size = new System.Drawing.Size(174, 20);
            this.textBoxPlanName.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(306, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Folder:";
            // 
            // labelScheduleTime
            // 
            this.labelScheduleTime.AutoSize = true;
            this.labelScheduleTime.Location = new System.Drawing.Point(22, 198);
            this.labelScheduleTime.Name = "labelScheduleTime";
            this.labelScheduleTime.Size = new System.Drawing.Size(78, 13);
            this.labelScheduleTime.TabIndex = 4;
            this.labelScheduleTime.Text = "Schedule Time";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Schedule Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cloud:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Plan Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(181, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fill all data to create a plan";
            // 
            // ManageBackupPlanWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 307);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManageBackupPlanWindow";
            this.Text = "Add Backup Plan";
            this.Load += new System.EventHandler(this.AddBackupPlanWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelScheduleTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerScheduleTime;
        private System.Windows.Forms.ComboBox comboBoxScheduleType;
        private System.Windows.Forms.ComboBox comboBoxClouds;
        private System.Windows.Forms.TextBox textBoxPlanName;
        private System.Windows.Forms.Button buttonBrowseFolder;
        private System.Windows.Forms.TextBox textBoxFolderPath;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonComplete;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.RadioButton radioButtonNo;
        private System.Windows.Forms.RadioButton radioButtonYes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxMonthOrWeekDay;
    }
}