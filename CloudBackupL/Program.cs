using System;
using System.Configuration;
using System.Windows.Forms;

namespace CloudBackupL
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (new DatabaseService().GetSettings().askPassword)
            {
                LoginForm fLogin = new LoginForm();
                if (fLogin.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new MainWindow());
                }
                else
                {
                    Application.Exit();
                }
            } else
            {
                Application.Run(new MainWindow());
            }
        }
    }
}
