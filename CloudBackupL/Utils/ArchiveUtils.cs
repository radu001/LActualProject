using ByteSizeLib;
using CloudBackupL.CustomControllers;
using CloudBackupL.TabsControllers;
using Dropbox.Api;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackupL.Utils
{
    class ArchiveUtils
    {
        public void RunArchiving(string directoryName, EventHandler<SaveProgressEventArgs> Zip_SaveProgress)
        {
            int size = (int) ByteSize.FromMegaBytes(Double.Parse(ConfigurationManager.AppSettings["chunkSize"])).Bytes;


            using (ZipFile zip = new ZipFile())
            {
                zip.MaxOutputSegmentSize = size;
                zip.SaveProgress += Zip_SaveProgress;
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.Password = "radu";
                zip.AddDirectory(directoryName, "Backup1");
                zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "tmpFolder\\");
                zip.Save(AppDomain.CurrentDomain.BaseDirectory + "tmpFolder\\temp.zip");
            }
        }

        public static long GetDirectorySize(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }
    }
}
