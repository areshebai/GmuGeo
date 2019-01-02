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
    public class GeoBlockPosition
    {
        public int Index { get; set; }
        public double MinLng { get; set; }
        public double MaxLng { get; set; }
        public double MinLat { get; set; }
        public double MaxLat { get; set; }
        public int LatIndex { get; set; }
        public int LngIndex { get; set; }

        public GeoBlockPosition(int index, double minLng, double maxLng, double minLat, double maxLat, int latIndex, int lngIndex)
        {
            Index = index;
            MinLng = minLng;
            MaxLng = maxLng;
            MinLat = minLat;
            MaxLat = minLat;
            LatIndex = latIndex;
            LngIndex = lngIndex;
        }
    }

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

        public List<GeoBlockPosition> InitGeoBlockPositionTable()
        {
            List<GeoBlockPosition> blockTable = new List<GeoBlockPosition>();

            blockTable.Add(new GeoBlockPosition(001, -180.0, -165.0, 60.0, 75.0, 01, 00));
            blockTable.Add(new GeoBlockPosition(002, -165.0, -150.0, 60.0, 75.0, 01, 01));
            blockTable.Add(new GeoBlockPosition(003, -150.0, -135.0, 60.0, 75.0, 01, 02));
            blockTable.Add(new GeoBlockPosition(004, -135.0, -120.0, 60.0, 75.0, 01, 03));
            blockTable.Add(new GeoBlockPosition(005, -120.0, -105.0, 60.0, 75.0, 01, 04));
            blockTable.Add(new GeoBlockPosition(006, -105.0, -90.0, 60.0, 75.0, 01, 05));
            blockTable.Add(new GeoBlockPosition(007, -90.0, -75.0, 60.0, 75.0, 01, 06));
            blockTable.Add(new GeoBlockPosition(008, -75.0, -60.0, 60.0, 75.0, 01, 07));
            blockTable.Add(new GeoBlockPosition(009, -180.0, -165.0, 45.0, 60.0, 02, 00));
            blockTable.Add(new GeoBlockPosition(010, -165.0, -150.0, 45.0, 60.0, 02, 01));
            blockTable.Add(new GeoBlockPosition(011, -135.0, -120.0, 45.0, 60.0, 02, 03));
            blockTable.Add(new GeoBlockPosition(012, -120.0, -105.0, 45.0, 60.0, 02, 04));
            blockTable.Add(new GeoBlockPosition(013, -105.0, -90.0, 45.0, 60.0, 02, 05));
            blockTable.Add(new GeoBlockPosition(014, -90.0, -75.0, 45.0, 60.0, 02, 06));
            blockTable.Add(new GeoBlockPosition(015, -75.0, -60.0, 45.0, 60.0, 02, 07));
            blockTable.Add(new GeoBlockPosition(016, -60.0, -45.0, 45.0, 60.0, 02, 08));
            blockTable.Add(new GeoBlockPosition(017, -135.0, -120.0, 30.0, 45.0, 03, 03));
            blockTable.Add(new GeoBlockPosition(018, -120.0, -105.0, 30.0, 45.0, 03, 04));
            blockTable.Add(new GeoBlockPosition(019, -105.0, -90.0, 30.0, 45.0, 03, 05));
            blockTable.Add(new GeoBlockPosition(020, -90.0, -75.0, 30.0, 45.0, 03, 06));
            blockTable.Add(new GeoBlockPosition(021, -75.0, -60.0, 30.0, 45.0, 03, 07));
            blockTable.Add(new GeoBlockPosition(022, -120.0, -105.0, 15.0, 30.0, 04, 04));
            blockTable.Add(new GeoBlockPosition(023, -105.0, -90.0, 15.0, 30.0, 04, 05));
            blockTable.Add(new GeoBlockPosition(024, -90.0, -75.0, 15.0, 30.0, 04, 06));
            blockTable.Add(new GeoBlockPosition(025, -75.0, -60.0, 15.0, 30.0, 04, 07));
            blockTable.Add(new GeoBlockPosition(026, -105.0, -90.0, 0.0, 15.0, 05, 05));
            blockTable.Add(new GeoBlockPosition(027, -90.0, -75.0, 0.0, 15.0, 05, 06));
            blockTable.Add(new GeoBlockPosition(028, -75.0, -60.0, 0.0, 15.0, 05, 07));
            blockTable.Add(new GeoBlockPosition(029, -60.0, -45.0, 0.0, 15.0, 05, 08));
            blockTable.Add(new GeoBlockPosition(030, -90.0, -75.0, -15.0, 0.0, 06, 06));
            blockTable.Add(new GeoBlockPosition(031, -75.0, -60.0, -15.0, 0.0, 06, 07));
            blockTable.Add(new GeoBlockPosition(032, -60.0, -45.0, -15.0, 0.0, 06, 08));
            blockTable.Add(new GeoBlockPosition(033, -45.0, -30.0, -15.0, 0.0, 06, 09));
            blockTable.Add(new GeoBlockPosition(034, -75.0, -60.0, -30.0, -15.0, 07, 07));
            blockTable.Add(new GeoBlockPosition(035, -60.0, -45.0, -30.0, -15.0, 07, 08));
            blockTable.Add(new GeoBlockPosition(036, -45.0, -30.0, -30.0, -15.0, 07, 09));
            blockTable.Add(new GeoBlockPosition(037, -75.0, -60.0, -45.0, -30.0, 08, 07));
            blockTable.Add(new GeoBlockPosition(038, -60.0, -45.0, -45.0, -30.0, 08, 08));
            blockTable.Add(new GeoBlockPosition(039, -90.0, -75.0, -60.0, -45.0, 09, 06));
            blockTable.Add(new GeoBlockPosition(040, -75.0, -60.0, -60.0, -45.0, 09, 07));
            blockTable.Add(new GeoBlockPosition(041, -60.0, -45.0, -60.0, -45.0, 09, 08));
            blockTable.Add(new GeoBlockPosition(042, -15.0, 0.0, 60.0, 75.0, 01, 11));
            blockTable.Add(new GeoBlockPosition(043, 0.0, 15.0, 60.0, 75.0, 01, 12));
            blockTable.Add(new GeoBlockPosition(044, 15.0, 30.0, 60.0, 75.0, 01, 13));
            blockTable.Add(new GeoBlockPosition(045, 30.0, 45.0, 60.0, 75.0, 01, 14));
            blockTable.Add(new GeoBlockPosition(046, 45.0, 60.0, 60.0, 75.0, 01, 15));
            blockTable.Add(new GeoBlockPosition(047, 60.0, 75.0, 60.0, 75.0, 01, 16));
            blockTable.Add(new GeoBlockPosition(048, 75.0, 90.0, 60.0, 75.0, 01, 17));
            blockTable.Add(new GeoBlockPosition(049, 90.0, 105.0, 60.0, 75.0, 01, 18));
            blockTable.Add(new GeoBlockPosition(050, 90.0, 105.0, 75.0, 90.0, 00, 18));
            blockTable.Add(new GeoBlockPosition(051, 105.0, 120.0, 75.0, 90.0, 00, 19));
            blockTable.Add(new GeoBlockPosition(052, 105.0, 120.0, 60.0, 75.0, 01, 19));
            blockTable.Add(new GeoBlockPosition(053, 120.0, 135.0, 60.0, 75.0, 01, 20));
            blockTable.Add(new GeoBlockPosition(054, 135.0, 150.0, 60.0, 75.0, 01, 21));
            blockTable.Add(new GeoBlockPosition(055, 150.0, 165.0, 60.0, 75.0, 01, 22));
            blockTable.Add(new GeoBlockPosition(056, 165.0, 180.0, 60.0, 75.0, 01, 23));
            blockTable.Add(new GeoBlockPosition(057, -15.0, 0.0, 45.0, 60.0, 02, 11));
            blockTable.Add(new GeoBlockPosition(058, 0.0, 15.0, 45.0, 60.0, 02, 12));
            blockTable.Add(new GeoBlockPosition(059, 15.0, 30.0, 45.0, 60.0, 02, 13));
            blockTable.Add(new GeoBlockPosition(060, 30.0, 45.0, 45.0, 60.0, 02, 14));
            blockTable.Add(new GeoBlockPosition(061, 45.0, 60.0, 45.0, 60.0, 02, 15));
            blockTable.Add(new GeoBlockPosition(062, 60.0, 75.0, 45.0, 60.0, 02, 16));
            blockTable.Add(new GeoBlockPosition(063, 75.0, 90.0, 45.0, 60.0, 02, 17));
            blockTable.Add(new GeoBlockPosition(064, 90.0, 105.0, 45.0, 60.0, 02, 18));
            blockTable.Add(new GeoBlockPosition(065, 105.0, 120.0, 45.0, 60.0, 02, 19));
            blockTable.Add(new GeoBlockPosition(066, 120.0, 135.0, 45.0, 60.0, 02, 20));
            blockTable.Add(new GeoBlockPosition(067, 135.0, 150.0, 45.0, 60.0, 02, 21));
            blockTable.Add(new GeoBlockPosition(068, 150.0, 165.0, 45.0, 60.0, 02, 22));
            blockTable.Add(new GeoBlockPosition(069, 165.0, 180.0, 45.0, 60.0, 02, 23));
            blockTable.Add(new GeoBlockPosition(070, -15.0, 0.0, 30.0, 45.0, 03, 11));
            blockTable.Add(new GeoBlockPosition(071, 0.0, 15.0, 30.0, 45.0, 03, 12));
            blockTable.Add(new GeoBlockPosition(072, 15.0, 30.0, 30.0, 45.0, 03, 13));
            blockTable.Add(new GeoBlockPosition(073, 30.0, 45.0, 30.0, 45.0, 03, 14));
            blockTable.Add(new GeoBlockPosition(074, 45.0, 60.0, 30.0, 45.0, 03, 15));
            blockTable.Add(new GeoBlockPosition(075, 60.0, 75.0, 30.0, 45.0, 03, 16));
            blockTable.Add(new GeoBlockPosition(076, 75.0, 90.0, 30.0, 45.0, 03, 17));
            blockTable.Add(new GeoBlockPosition(077, 90.0, 105.0, 30.0, 45.0, 03, 18));
            blockTable.Add(new GeoBlockPosition(078, 105.0, 120.0, 30.0, 45.0, 03, 19));
            blockTable.Add(new GeoBlockPosition(079, 120.0, 135.0, 30.0, 45.0, 03, 20));
            blockTable.Add(new GeoBlockPosition(080, 135.0, 150.0, 30.0, 45.0, 03, 21));
            blockTable.Add(new GeoBlockPosition(081, 150.0, 165.0, 30.0, 45.0, 03, 22));
            blockTable.Add(new GeoBlockPosition(082, -30.0, -15.0, 15.0, 30.0, 04, 10));
            blockTable.Add(new GeoBlockPosition(083, -15.0, 0.0, 15.0, 30.0, 04, 11));
            blockTable.Add(new GeoBlockPosition(084, 0.0, 15.0, 15.0, 30.0, 04, 12));
            blockTable.Add(new GeoBlockPosition(085, 15.0, 30.0, 15.0, 30.0, 04, 13));
            blockTable.Add(new GeoBlockPosition(086, 30.0, 45.0, 15.0, 30.0, 04, 14));
            blockTable.Add(new GeoBlockPosition(087, 45.0, 60.0, 15.0, 30.0, 04, 15));
            blockTable.Add(new GeoBlockPosition(088, 60.0, 75.0, 15.0, 30.0, 04, 16));
            blockTable.Add(new GeoBlockPosition(089, 75.0, 90.0, 15.0, 30.0, 04, 17));
            blockTable.Add(new GeoBlockPosition(090, 90.0, 105.0, 15.0, 30.0, 04, 18));
            blockTable.Add(new GeoBlockPosition(091, 105.0, 120.0, 15.0, 30.0, 04, 19));
            blockTable.Add(new GeoBlockPosition(092, 120.0, 135.0, 15.0, 30.0, 04, 20));
            blockTable.Add(new GeoBlockPosition(093, -30.0, -15.0, 0.0, 15.0, 05, 10));
            blockTable.Add(new GeoBlockPosition(094, -15.0, 0.0, 0.0, 15.0, 05, 11));
            blockTable.Add(new GeoBlockPosition(095, 0.0, 15.0, 0.0, 15.0, 05, 12));
            blockTable.Add(new GeoBlockPosition(096, 15.0, 30.0, 0.0, 15.0, 05, 13));
            blockTable.Add(new GeoBlockPosition(097, 30.0, 45.0, 0.0, 15.0, 05, 14));
            blockTable.Add(new GeoBlockPosition(098, 45.0, 60.0, 0.0, 15.0, 05, 15));
            blockTable.Add(new GeoBlockPosition(099, 60.0, 75.0, 0.0, 15.0, 05, 16));
            blockTable.Add(new GeoBlockPosition(100, 75.0, 90.0, 0.0, 15.0, 05, 17));
            blockTable.Add(new GeoBlockPosition(101, 90.0, 105.0, 0.0, 15.0, 05, 18));
            blockTable.Add(new GeoBlockPosition(102, 105.0, 120.0, 0.0, 15.0, 05, 19));
            blockTable.Add(new GeoBlockPosition(103, 120.0, 135.0, 0.0, 15.0, 05, 20));
            blockTable.Add(new GeoBlockPosition(104, 0.0, 15.0, -15.0, 0.0, 06, 12));
            blockTable.Add(new GeoBlockPosition(105, 15.0, 30.0, -15.0, 0.0, 06, 13));
            blockTable.Add(new GeoBlockPosition(106, 30.0, 45.0, -15.0, 0.0, 06, 14));
            blockTable.Add(new GeoBlockPosition(107, 45.0, 60.0, -15.0, 0.0, 06, 15));
            blockTable.Add(new GeoBlockPosition(108, 75.0, 90.0, -15.0, 0.0, 06, 17));
            blockTable.Add(new GeoBlockPosition(109, 90.0, 105.0, -15.0, 0.0, 06, 18));
            blockTable.Add(new GeoBlockPosition(110, 105.0, 120.0, -15.0, 0.0, 06, 19));
            blockTable.Add(new GeoBlockPosition(111, 120.0, 135.0, -15.0, 0.0, 06, 20));
            blockTable.Add(new GeoBlockPosition(112, 135.0, 150.0, -15.0, 0.0, 06, 21));
            blockTable.Add(new GeoBlockPosition(113, 0.0, 15.0, -30.0, -15.0, 07, 12));
            blockTable.Add(new GeoBlockPosition(114, 15.0, 30.0, -30.0, -15.0, 07, 13));
            blockTable.Add(new GeoBlockPosition(115, 30.0, 45.0, -30.0, -15.0, 07, 14));
            blockTable.Add(new GeoBlockPosition(116, 45.0, 60.0, -30.0, -15.0, 07, 15));
            blockTable.Add(new GeoBlockPosition(117, 105.0, 120.0, -30.0, -15.0, 07, 19));
            blockTable.Add(new GeoBlockPosition(118, 120.0, 135.0, -30.0, -15.0, 07, 20));
            blockTable.Add(new GeoBlockPosition(119, 135.0, 150.0, -30.0, -15.0, 07, 21));
            blockTable.Add(new GeoBlockPosition(120, 150.0, 165.0, -30.0, -15.0, 07, 22));
            blockTable.Add(new GeoBlockPosition(121, 15.0, 30.0, -45.0, -30.0, 08, 13));
            blockTable.Add(new GeoBlockPosition(122, 30.0, 45.0, -45.0, -30.0, 08, 14));
            blockTable.Add(new GeoBlockPosition(123, 105.0, 120.0, -45.0, -30.0, 08, 19));
            blockTable.Add(new GeoBlockPosition(124, 120.0, 135.0, -45.0, -30.0, 08, 20));
            blockTable.Add(new GeoBlockPosition(125, 135.0, 150.0, -45.0, -30.0, 08, 21));
            blockTable.Add(new GeoBlockPosition(126, 150.0, 165.0, -45.0, -30.0, 08, 22));
            blockTable.Add(new GeoBlockPosition(127, 165.0, 180.0, -45.0, -30.0, 08, 23));
            blockTable.Add(new GeoBlockPosition(128, 165.0, 180.0, -60.0, -45.0, 09, 23));

            return blockTable;
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

        public Image JoinImages(string imageFolder, int hour = -1, int minute = -1, int second = -1)
        {
            int[,] earthBlockIndexTable = new int[12, 24]
            {
                { 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 050, 052, 000, 000, 000, 000}, // 75-90
                { 001, 002, 003, 004, 005, 006, 007, 008, 000, 000, 000, 042, 043, 044, 045, 046, 047, 048, 049, 052, 053, 054, 055, 056}, // 60-75
                { 009, 010, 000, 011, 012, 013, 014, 015, 016, 000, 000, 057, 058, 059, 060, 061, 062, 063, 064, 065, 066, 067, 068, 069}, // 45-60
                { 000, 000, 000, 017, 018, 019, 020, 021, 000, 000, 000, 070, 071, 072, 073, 074, 075, 076, 077, 078, 079, 080, 081, 000}, // 30-45
                { 000, 000, 000, 000, 022, 023, 024, 025, 000, 000, 082, 083, 084, 085, 086, 087, 088, 089, 090, 091, 092, 000, 000, 000}, // 15-30
                { 000, 000, 000, 000, 000, 026, 027, 028, 029, 000, 093, 094, 095, 096, 097, 098, 099, 100, 101, 102, 103, 000, 000, 000}, // 00-15
            //-180---165--150--135--120--105--90---75---60---45---30---15----0---15---30---45---60---75---90---105--120--135--150--165--180--------
                { 000, 000, 000, 000, 000, 000, 030, 031, 032, 033, 000, 000, 104, 105, 106, 107, 000, 108, 109, 110, 111, 112, 000, 000}, // 00-15
                { 000, 000, 000, 000, 000, 000, 000, 034, 035, 036, 000, 000, 113, 114, 115, 116, 000, 000, 000, 117, 118, 119, 120, 000}, // 15-30
                { 000, 000, 000, 000, 000, 000, 000, 037, 038, 000, 000, 000, 000, 121, 122, 000, 000, 000, 000, 123, 124, 125, 126, 127}, // 30-45
                { 000, 000, 000, 000, 000, 000, 039, 040, 041, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 128}, // 45-60
                { 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000}, // 60-75
                { 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000}, // 75-90
            };

            Bitmap joinedBitmap = new Bitmap(200, 400);
            Graphics graph = Graphics.FromImage(joinedBitmap);

            if (hour != -1)
            {
                List<GeoBlockPosition> positionList = InitGeoBlockPositionTable();

                string fileFilterString = string.Format("*_{0:00}0000_*.png", hour);
                string[] files = Directory.GetFiles(imageFolder, fileFilterString);

                for (int i = 0; i < files.Length; i++)
                {
                    int index = FileServerUtil.GetBlockIndexFromFileName(files[i]);

                    Image image = Image.FromFile(files[i]);

                    GeoBlockPosition position = positionList[index];

                    graph.DrawImage(image, 4448 * position.LngIndex, 4448 * position.LatIndex, 4448, 4448);
                }

                joinedBitmap.Save(@"D:\Test\Images\cspp-viirs-flood-globally_20180815_010000_66_69.png");
            }

            joinedBitmap.Save(@"C:\Temp\cspp-viirs-flood-globally_20180815_010000.png");
            return joinedBitmap;
        }

        public void ProcessImages(string sourcePath, int rate = 50)
        {
            string[] pngFiles = Directory.GetFiles(sourcePath, "*.png");

            string targetPath = Path.Combine(sourcePath, rate.ToString());
            if (Directory.Exists(targetPath))
            {
                Directory.Delete(targetPath, true);
            }

            Directory.CreateDirectory(targetPath);

            for (int i = 0; i < pngFiles.Length; i++)
            {
                string fileName = pngFiles[i];
                using (Image image = Image.FromFile(fileName))
                {
                    int newWidth = image.Width * rate / 100;
                    int newHeight = image.Height * rate / 100;

                    using (Bitmap newBitmap = new Bitmap(image, newWidth, newHeight))
                    {
                        Graphics graphic = Graphics.FromImage(newBitmap);
                        graphic.CompositingQuality = CompositingQuality.HighQuality;
                        graphic.SmoothingMode = SmoothingMode.HighQuality;
                        graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Rectangle imageRectangle = new Rectangle(0, 0, newWidth, newHeight);

                        string newFileName = Path.Combine(targetPath, Path.GetFileName(fileName));
                        Console.WriteLine(string.Format("Saving {0} to {1}", fileName, newFileName));

                        graphic.DrawImage(image, imageRectangle);
                        newBitmap.Save(newFileName);
                    }
                }
            }
        }
    }
}
