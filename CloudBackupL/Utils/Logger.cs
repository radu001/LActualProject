using CloudBackupL.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudBackupL.Utils
{
    class Logger
    {
        private static String logFilePath = ConfigurationManager.AppSettings["logFile"];
        private static readonly object _syncObject = new object();
        static readonly TextWriter textWriter;

        static Logger()
        {
            textWriter = TextWriter.Synchronized(File.AppendText(logFilePath));
        }

        public static void Log(string logMessage)
        {
            lock (_syncObject)
            {
                string logText = string.Format("{0} {1} : {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), logMessage);    
                textWriter.WriteLine(logText);
                textWriter.Flush();
                TextBox textBoxLogs = MainWindow.instance.TextBoxLogs;
                textBoxLogs.Invoke(new Action(() => textBoxLogs.AppendText(textBoxLogs.Text == "" ? logText : Environment.NewLine + logText)));
            }
        }

        public static void Log(string logMessage, ToolTipIcon toolTipIcon)
        {
            lock (_syncObject)
            {
                string logText = string.Format("{0} {1} : {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), logMessage);
                textWriter.WriteLine(logText);
                textWriter.Flush();

                if(new DatabaseService().GetSettings().showNotifications)
                {
                    NotifyIcon notifyIcon = MainWindow.instance.NotifyIconApp;
                    var state = notifyIcon.Visible;
                    notifyIcon.Visible = true;
                    notifyIcon.Icon = Resources.app_icon;
                    notifyIcon.BalloonTipText = logMessage;
                    notifyIcon.BalloonTipTitle = "Secure Backup";
                    notifyIcon.BalloonTipIcon = toolTipIcon;

                    notifyIcon.ShowBalloonTip(3000);
                    notifyIcon.Visible = state;
                }

                TextBox textBoxLogs = MainWindow.instance.TextBoxLogs;
                textBoxLogs.Invoke(new Action(() => textBoxLogs.AppendText(textBoxLogs.Text == "" ? logText : Environment.NewLine + logText)));
            }
        }
    }
}
