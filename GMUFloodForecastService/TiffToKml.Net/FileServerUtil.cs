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

        public static void ProcessSourceFileFromFTPServer(string serverFilePath, string savedDirectory)
        {
            string fileName = Path.GetFileName(serverFilePath);
            string tempFilePath = Path.GetTempPath();
            string tempFileName = Path.Combine(tempFilePath, fileName);

            // Download zip file to temp folder
            DownloadAndSaveFile(serverFilePath, tempFileName);

            // Unzip downloaded zip file
            DirectoryInfo targetDirectory = Directory.CreateDirectory(Path.Combine(tempFilePath, "uncompress"));
            ZipFile.ExtractToDirectory(tempFileName, targetDirectory.FullName);

            // Delete downloaded zip file
            File.Delete(tempFileName);

            string[] kmlFileNames = Directory.GetFiles(targetDirectory.FullName, "*.kml");
            for (int i = 0; i < kmlFileNames.Length; i++)
            {
                // cspp-viirs-flood-globally_20180815_010000_54.kml
                string name = Path.GetFileNameWithoutExtension(kmlFileNames[i]);
                string directoryName = Path.GetDirectoryName(kmlFileNames[i]);
                string[] elements = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                if (elements.Length != 4)
                {
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

                File.Copy(Path.Combine(directoryName, name) + ".kml", Path.Combine(disfolderPath, name) + ".kml");
                File.Copy(Path.Combine(directoryName, name) + ".png", Path.Combine(disfolderPath, name) + ".png");
                File.Copy(Path.Combine(directoryName, name) + ".tif", Path.Combine(rawfolderPath, name) + ".tif");
            }

            // Delete temp uncompress directory
            Directory.Delete(targetDirectory.FullName, true);
        }
    }
}
