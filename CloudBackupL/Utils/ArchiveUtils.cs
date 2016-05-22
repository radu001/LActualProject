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
        public static void ZipFiles(string filesSource, string saveDirectory, EventHandler<SaveProgressEventArgs> Zip_SaveProgress)
        {
            int size = (int) ByteSize.FromMegaBytes(Double.Parse(ConfigurationManager.AppSettings["chunkSize"])).Bytes;

            using (ZipFile zip = new ZipFile())
            {
                zip.MaxOutputSegmentSize = size;
                zip.SaveProgress += Zip_SaveProgress;
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.Password = "radu";
                zip.AddDirectory(filesSource, new DirectoryInfo(filesSource).Name);
                zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                Directory.CreateDirectory(saveDirectory);
                zip.Save(saveDirectory + "temp.zip");
            }
        }

        public static void ExtractZip(string directoryName, string extractPath, EventHandler<ExtractProgressEventArgs> Zip_ExtractProgress)
        {
            using (ZipFile zip = ZipFile.Read(directoryName + "temp.ziptemp.zip"))
            {
                zip.ExtractProgress += Zip_ExtractProgress;
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.Password = "radu";
                zip.ExtractAll(extractPath);
            }
        }

        static public void DeleteDirectory(string path)
        {
            if(Directory.Exists(path))
            {
                DirectoryInfo did = new DirectoryInfo(path);
                foreach (FileInfo file in did.GetFiles())
                {
                    file.Delete();
                }

                Directory.Delete(path);
            }
        }


        public static long GetDirectorySize(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }
    }
}
