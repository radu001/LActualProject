using CloudBackupL.Models;
using CloudBackupL.Utils;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CloudBackupL
{
    public partial class LoginForm : Form
    {
        Settings settings;
        string oldPass = null;
        DatabaseService databaseService = new DatabaseService();

        public LoginForm()
        {
            InitializeComponent();
            settings = databaseService.GetSettings();
            oldPass = settings.getPassword();
            if(string.IsNullOrEmpty(oldPass))
            {
                labelTitle.Text = "Register";
                button1.Text = "Register";

            } else
            {
                labelPassword.Visible = false;
                textBoxPassword.Visible = false;
                labelRepeatPassword.Text = "Password:";    
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }

        private void buttonExit_MouseMove(object sender, MouseEventArgs e)
        {
            buttonExit.ForeColor = Color.Orange;
        }

        private void buttonExit_MouseLeave(object sender, EventArgs e)
        {
            buttonExit.ForeColor = Color.White;
        }


        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(oldPass))
            {
                if(textBoxPassword.Text.Equals(textBoxRepeatPassword.Text))
                {
                    if(textBoxRepeatPassword.Text.Length < 4)
                    {
                        MessageBox.Show("Password too short!");
                    } else
                    {
                        settings.setPassword(MyUtils.Encript(textBoxRepeatPassword.Text));
                        databaseService.SetSettings(settings);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                } else
                {
                    MessageBox.Show("Password not equals!");
                }
            } else
            {
                if(MyUtils.Encript(textBoxRepeatPassword.Text).Equals(oldPass))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                } else
                {
                    MessageBox.Show("Wrong password");
                }
            }
        }
    }
}
