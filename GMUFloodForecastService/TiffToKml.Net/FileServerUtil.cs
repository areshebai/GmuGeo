using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO.Compression;

namespace TiffToKml.Net
{
    public class FileServerUtil
    {
        public static NetworkCredential GetFileServerCredential()
        {
            return new NetworkCredential("tiffauthor", "07Apples");
        }

        public static string GetFileServerUrl(string host, int port, DateTime date)
        {
            return string.Format("ftp://{0}:{1}/{2}", host, port, date.ToString("yyyy/MM/dd"));
        }

        public static string GetFileServerUrl(string host, int port, string folder)
        {
            return string.Format("ftp://{0}:{1}/{2}", host, port, folder);
        }

        public static void DownloadAndSaveFile(string serverFilePath, string localFilePath, bool overwrite = true)
        {
            if (File.Exists(localFilePath) && overwrite)
            {
                File.Delete(localFilePath);
            }

            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(serverFilePath);
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            ftpRequest.Credentials = GetFileServerCredential();

            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

            using (Stream responseStream = ftpResponse.GetResponseStream())
            {
                using (FileStream fs = File.OpenWrite(localFilePath))
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    responseStream.CopyTo(fs);
                }
            }
        }

        public static int GetBlockIndexFromFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != 4)
            {
                throw new Exception();
            }

            return Convert.ToInt32(elements[3]);
        }

        public static DateTime GetDateFromFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != 4)
            {
                throw new Exception();
            }

            return new DateTime(Convert.ToInt32(elements[1].Substring(0, 4)), Convert.ToInt32(elements[1].Substring(4, 2)), Convert.ToInt32(elements[1].Substring(6, 2)));
        }

        public static int GetHourFromFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != 4)
            {
                throw new Exception();
            }

            return Convert.ToInt32(elements[2].Substring(0, 2));
        }

        public static void SaveDailyLatestKmlAndPng(string savedDirectory, string kmlFileName)
        {
            long fileLength = new FileInfo(kmlFileName.Replace(".kml", ".png")).Length;
            int bIndex = GetBlockIndexFromFileName(kmlFileName);
            string[] existedFiles = Directory.GetFiles(savedDirectory, string.Format("*_{0}.kml", bIndex));

            if (existedFiles.Length > 1)
            {
                throw new Exception();
            }

            if (existedFiles.Length == 0)
            {
                File.Copy(kmlFileName, Path.Combine(savedDirectory, Path.GetFileNameWithoutExtension(kmlFileName) + ".kml"));
                File.Copy(kmlFileName.Replace(".kml", ".png"), Path.Combine(savedDirectory, Path.GetFileNameWithoutExtension(kmlFileName) + ".png"));
            }

            if (existedFiles.Length == 1)
            {
                long length = new FileInfo(existedFiles[0].Replace(".kml", ".png")).Length;
                if (fileLength > length)
                {
                    File.Delete(existedFiles[0]);
                    File.Delete(existedFiles[0].Replace(".kml", ".png"));
                    File.Copy(kmlFileName, Path.Combine(savedDirectory, Path.GetFileNameWithoutExtension(kmlFileName) + ".kml"));
                    File.Copy(kmlFileName.Replace(".kml", ".png"), Path.Combine(savedDirectory, Path.GetFileNameWithoutExtension(kmlFileName) + ".png"));
                }
            }
        }

        public static void ProcessSourceFileFromFTPServer(string serverFilePath, string savedDirectory)
        {
            string fileName = Path.GetFileName(serverFilePath);
            string tempFilePath = Path.GetTempPath();

            // Download zip file to temp folder
            string tempFileName = Path.Combine(tempFilePath, fileName);
            DownloadAndSaveFile(serverFilePath, tempFileName);

            // Unzip downloaded zip file
            string unCompressFolder = Path.Combine(tempFilePath, "uncompress");
            if (Directory.Exists(unCompressFolder))
            {
                Directory.Delete(unCompressFolder, true);
            }
            Directory.CreateDirectory(unCompressFolder);
            ZipFile.ExtractToDirectory(tempFileName, unCompressFolder);

            // Delete downloaded zip file
            File.Delete(tempFileName);

            string[] kmlFileNames = Directory.GetFiles(unCompressFolder, "*.kml");
            for (int i = 0; i < kmlFileNames.Length; i++)
            {
                // cspp-viirs-flood-globally_20180815_010000_54.kml
                string name = Path.GetFileNameWithoutExtension(kmlFileNames[i]);
                string directoryName = Path.GetDirectoryName(kmlFileNames[i]);
                string[] elements = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                if (elements.Length != 4)
                {
                    // 0: cspp-viirs-flood-globally, 1: 20180815_010000_54, 2: 010000, 3: 54
                    throw new Exception();
                }

                string folderPath = Path.Combine(savedDirectory, elements[1].Substring(0, 4), elements[1].Substring(4, 2), elements[1].Substring(6, 2));
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string disfolderPath = Path.Combine(folderPath, "dis");
                if (!Directory.Exists(disfolderPath))
                {
                    Directory.CreateDirectory(disfolderPath);
                }

                string rawfolderPath = Path.Combine(folderPath, "raw");
                if (!Directory.Exists(rawfolderPath))
                {
                    Directory.CreateDirectory(rawfolderPath);
                }

                string unifolderPath = Path.Combine(folderPath, "uni");
                if (!Directory.Exists(unifolderPath))
                {
                    Directory.CreateDirectory(unifolderPath);
                }

                File.Copy(Path.Combine(directoryName, name) + ".kml", Path.Combine(disfolderPath, name) + ".kml", true);
                File.Copy(Path.Combine(directoryName, name) + ".png", Path.Combine(disfolderPath, name) + ".png", true);
                File.Copy(Path.Combine(directoryName, name) + ".tif", Path.Combine(rawfolderPath, name) + ".tif", true);

                SaveDailyLatestKmlAndPng(unifolderPath, kmlFileNames[i]);
            }

            // Delete temp uncompress directory
            Directory.Delete(unCompressFolder, true);
        }
    }
}
