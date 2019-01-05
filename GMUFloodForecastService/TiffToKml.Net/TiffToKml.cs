using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using OSGeo.GDAL;
using Gdal = OSGeo.GDAL.Gdal;
using Ogr = OSGeo.OGR.Ogr;
using System.Drawing.Drawing2D;

namespace TiffToKml.Net
{

    public class GeoPosition
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class KmlElement
    {
        public GeoPosition UperLeft { get; set; }
        public GeoPosition UperRight { get; set; }
        public GeoPosition LowerLeft { get; set; }
        public GeoPosition LowerRight { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public string ImageFileName { get; set; }
    }

    public class TiffToKml
    {
        public TiffToKml()
        {
        }

        

        public void DownloadAndSaveFiles()
        {
            // Example string: ftp://13.78.149.101:21/subset
            string serverUrl = FileServerUtil.GetFileServerUrl("13.78.149.101", 21, "subset");
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(serverUrl);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            ftpRequest.Credentials = FileServerUtil.GetFileServerCredential();

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
                    FileServerUtil.ProcessSourceFileFromFTPServer(serverFileFullPath, @"C:\GmuTemp");
                }
            }
        }
    }
}
