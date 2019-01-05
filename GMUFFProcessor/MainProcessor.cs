using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Net;
using GMUFFCommon;

namespace GMUFFProcessor
{
    public class MainProcessor
    {
        public string GetFileServerIpAddress()
        {
            // To-do: read from configuration file.
            return "13.78.149.101";
        }

        public int GetFileServerPort()
        {
            // To-do: read from configuration file.
            return 21;
        }

        public void ProcessRawPackagesFromFtpServer()
        {
            // Can support process local file if web server, ftp server, file download server on the same physical machine
            string fileServerIpAddress = GetFileServerIpAddress();
            int fileServerPort = GetFileServerPort();

            string serverUrl = FtpUtil.GetFileServerUrl(fileServerIpAddress, 21, "subset");

            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(serverUrl);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            ftpRequest.Credentials = FtpUtil.GetFileServerCredential();

            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

            string[] directoryOrFileNames;
            using (Stream responseStream = ftpResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream);
                string result = reader.ReadToEnd();
                directoryOrFileNames = String.IsNullOrEmpty(result) ? null : result.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }

            List<string> filteredResult = new List<string>();
            for (int i = 0; i < directoryOrFileNames.Length; i++)
            {
                string fileName = directoryOrFileNames[i];
                if (fileName.EndsWith(".zip"))
                {
                    string serverFileFullPath = string.Format("{0}/{1}", serverUrl, fileName);
                    FtpUtil.ProcessSourceFileFromFTPServer(serverFileFullPath, @"C:\GmuTemp");
                }
            }

            // Upload processed file back to ftp server
        }

        public void SaveDailyLatestKmlAndPng(string savedDirectory, string kmlFileName)
        {
            long fileLength = new FileInfo(kmlFileName.Replace(".kml", ".png")).Length;
            int bIndex = FileNameUtil.GetBlockIndexFromFileName(kmlFileName);
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

        public void ProcessSourceFileFromFTPServer(string serverFilePath, string savedDirectory)
        {
            string fileName = Path.GetFileName(serverFilePath);
            string tempFilePath = Path.GetTempPath();

            // Download zip file to temp folder
            string tempFileName = Path.Combine(tempFilePath, fileName);
            FtpUtil.DownloadAndSaveFile(serverFilePath, tempFileName);

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
