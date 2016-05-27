using ByteSizeLib;
using Ionic.Zip;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CloudBackupL.Utils
{
    class MyUtils
    {
        public static void ZipFiles(string filesSource, string saveDirectory, EventHandler<SaveProgressEventArgs> Zip_SaveProgress, string password)
        {
            int size = (int) ByteSize.FromMegaBytes(new DatabaseService().GetSettings().chunkSize).Bytes;

            using (ZipFile zip = new ZipFile())
            {
                zip.MaxOutputSegmentSize = size;
                zip.SaveProgress += Zip_SaveProgress;
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.Password = password;
                zip.AddDirectory(filesSource, new DirectoryInfo(filesSource).Name);
                zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                Directory.CreateDirectory(saveDirectory);
                zip.Save(saveDirectory + "temp.zip");
            }
        }

        public static void ExtractZip(string directoryName, string extractPath, EventHandler<ExtractProgressEventArgs> Zip_ExtractProgress, string password, bool isRestoreAction)
        {
            string tempPath = null;

            try
            {
                if (isRestoreAction)
                {
                    tempPath = extractPath + "-tmp\\";
                    DeleteDirectory(tempPath);
                    Directory.CreateDirectory(tempPath);
                    DirectoryInfo di = Directory.CreateDirectory(tempPath);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
                using (ZipFile zip = ZipFile.Read(directoryName + "temp.zip"))
                {
                    zip.ExtractProgress += Zip_ExtractProgress;
                    zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                    zip.Password = password;
                    zip.ExtractAll(isRestoreAction ? tempPath : extractPath,ExtractExistingFileAction.OverwriteSilently);
                }
                if (isRestoreAction)
                {
                    DeleteDirectory(extractPath);
                    Directory.Move(tempPath + new DirectoryInfo(extractPath).Name, extractPath);
                    DeleteDirectory(tempPath);
                }
            } catch(Ionic.Zip.BadPasswordException)
            {
                MessageBox.Show("Wrong password, please try with another password!");
            }
        }

        static public void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }


        public static long GetDirectorySize(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }

        public static string Encript(string password)
        {
            if(String.IsNullOrEmpty(password))
            {
                return null;
            }

            byte[] hash;
            using (SHA512 shaM = new SHA512Managed())
            {
                var data = Encoding.UTF8.GetBytes(password);
                hash = shaM.ComputeHash(data);
            }

            string encodedPassword = Encoding.UTF8.GetString(hash);
            return encodedPassword;
        }


        public static double GetFormatedSpaceInGB(long space)
        {
            return Math.Round(ByteSize.FromBytes(space).GigaBytes, 3);
        }

        public static DateTime GetNextExecution(BackupPlan plan)
        {
            DateTime nextUpdate = DateTime.Now;
            nextUpdate = nextUpdate.AddHours(-nextUpdate.Hour + plan.scheduleTime.Hour);
            nextUpdate = nextUpdate.AddMinutes(-nextUpdate.Minute + plan.scheduleTime.Minute);
            switch (plan.scheduleType)
            {
                case "Monthly":
                    if (nextUpdate.Day == plan.scheduleDay && DateTime.Compare(DateTime.Now, nextUpdate) < 0) return nextUpdate;

                    nextUpdate = nextUpdate.AddDays(1);
                    while (nextUpdate.Day != plan.scheduleDay)
                    {
                        nextUpdate = nextUpdate.AddDays(1);
                    }
                    return nextUpdate;

                case "Weekly":
                    int dayOfWeek = (plan.scheduleDay == 7 ? 0 : plan.scheduleDay);
                    if ((int)nextUpdate.DayOfWeek == dayOfWeek && DateTime.Compare(DateTime.Now, nextUpdate) < 0) return nextUpdate;
                    nextUpdate = nextUpdate.AddDays(1);
                    while ((int)nextUpdate.DayOfWeek != dayOfWeek)
                    {
                        nextUpdate = nextUpdate.AddDays(1);
                    }
                    return nextUpdate;

                case "Daily":
                    return DateTime.Compare(DateTime.Now, nextUpdate) < 0 ? nextUpdate : nextUpdate.AddDays(1);
            }
            return new DateTime(2500, 1, 1);
        }

        public static void UpdateSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}
