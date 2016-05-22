using ByteSizeLib;
using Ionic.Zip;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CloudBackupL.Utils
{
    class ArchiveUtils
    {
        public static void ZipFiles(string filesSource, string saveDirectory, EventHandler<SaveProgressEventArgs> Zip_SaveProgress, string password)
        {
            int size = (int) ByteSize.FromMegaBytes(Double.Parse(ConfigurationManager.AppSettings["chunkSize"])).Bytes;

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

        public static void ExtractZip(string directoryName, string extractPath, EventHandler<ExtractProgressEventArgs> Zip_ExtractProgress, string password)
        {
            using (ZipFile zip = ZipFile.Read(directoryName + "temp.zip"))
            {
                zip.ExtractProgress += Zip_ExtractProgress;
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.Password = password;
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

        public static string Encript(string password)
        {
            byte[] hash;
            using (SHA512 shaM = new SHA512Managed())
            {
                var data = Encoding.UTF8.GetBytes("text");
                hash = shaM.ComputeHash(data);
            }

            string encodedPassword = Encoding.UTF8.GetString(hash);

            return encodedPassword + encodedPassword;
        }
    }
}
