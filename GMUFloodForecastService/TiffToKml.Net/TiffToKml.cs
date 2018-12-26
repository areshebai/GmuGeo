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
            ftpRequest.Credentials = GetFileServerCredential();

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
                    FileServerUtil.ProcessSourceFileFromFTPServer(serverFileFullPath, @"C:\Temp");
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
                    int index = GetBlockIndexFromFileName(files[i]);

                    Image image = Image.FromFile(files[i]);

                    GeoBlockPosition position = positionList[index];

                    graph.DrawImage(image, 4448 * position.LngIndex, 4448 * position.LatIndex, 4448, 4448);
                }

                joinedBitmap.Save(@"D:\Test\Images\cspp-viirs-flood-globally_20180815_010000_66_69.png");
            }

            joinedBitmap.Save(@"C:\Temp\cspp-viirs-flood-globally_20180815_010000.png");
            return joinedBitmap;
        }

        public int GetBlockIndexFromFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != 4)
            {
                throw new Exception();
            }

            return Convert.ToInt32(elements[3]);
        }

        public DateTime GetDateFromFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != 4)
            {
                throw new Exception();
            }

            return new DateTime(Convert.ToInt32(elements[1].Substring(0, 4)), Convert.ToInt32(elements[1].Substring(4, 2)), Convert.ToInt32(elements[1].Substring(6, 2)));
        }

        public int GetHourFromFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != 4)
            {
                throw new Exception();
            }

            return Convert.ToInt32(elements[2].Substring(0, 2));
        }

        public void Execute()
        {
            GdalConfiguration.ConfigureGdal();

            /* -------------------------------------------------------------------- */
            /*      Register driver(s).                                             */
            /* -------------------------------------------------------------------- */
            Gdal.AllRegister();

            /* -------------------------------------------------------------------- */
            /*      Open dataset.                                                   */
            /* -------------------------------------------------------------------- */


            List<string> tiffFiles = GetGeoTiffFiles();
            foreach (string servertiffFile in tiffFiles)
            {
                string localTiffFile = GetLocalTiffFileName(Path.GetFileNameWithoutExtension(servertiffFile));
                string localPngFile = GetLocalPngFileName(Path.GetFileNameWithoutExtension(servertiffFile));
                string serverPngFile = servertiffFile.Replace(".tif", ".png");

                DownloadGeoTiffFile(servertiffFile, localTiffFile);

                Dataset ds = Gdal.Open(localTiffFile, Access.GA_Update);

                if (ds == null)
                {
                    Console.WriteLine("Can't open " + localTiffFile);
                    throw new Exception();
                }

                Console.WriteLine("Raster dataset parameters:");
                Console.WriteLine("  Projection: " + ds.GetProjectionRef());
                Console.WriteLine("  RasterCount: " + ds.RasterCount);
                Console.WriteLine("  RasterSize (" + ds.RasterXSize + "," + ds.RasterYSize + ")");

                /* -------------------------------------------------------------------- */
                /*      Get driver                                                      */
                /* -------------------------------------------------------------------- */
                Driver drv = ds.GetDriver();

                if (drv == null)
                {
                    Console.WriteLine("Can't get driver.");
                    System.Environment.Exit(-1);
                }

                Console.WriteLine("Using driver " + drv.LongName);

                /* -------------------------------------------------------------------- */
                /*      Get raster band                                                 */
                /* -------------------------------------------------------------------- */
                for (int iBand = 1; iBand <= ds.RasterCount; iBand++)
                {
                    Band band = ds.GetRasterBand(iBand);
                    Console.WriteLine("Band " + iBand + " :");
                    Console.WriteLine("   DataType: " + band.DataType);
                    Console.WriteLine("   Size (" + band.XSize + "," + band.YSize + ")");
                    Console.WriteLine("   PaletteInterp: " + band.GetRasterColorInterpretation().ToString());

                    for (int iOver = 0; iOver < band.GetOverviewCount(); iOver++)
                    {
                        Band over = band.GetOverview(iOver);
                        Console.WriteLine("      OverView " + iOver + " :");
                        Console.WriteLine("         DataType: " + over.DataType);
                        Console.WriteLine("         Size (" + over.XSize + "," + over.YSize + ")");
                        Console.WriteLine("         PaletteInterp: " + over.GetRasterColorInterpretation().ToString());
                    }
                }

                /* -------------------------------------------------------------------- */
                /*      Processing the raster                                           */
                /* -------------------------------------------------------------------- */
                //SaveBitmapBuffered(ds, localPngFile, -1);
                SaveBitmapGrayBuffered(ds, localPngFile, -1);

                UploadKmzFile(localPngFile, serverPngFile);
            }
            //string fileName = "cspp-viirs-flood-globally_20180815_080000.p153435116453720";
            //string geoTiffFileName = string.Format(@"D:\Repos\GmuGeo-bak\Resources\VIIRS_floodmap_Aug15_2018\{0}.tif", fileName);
            //string pngFileName = string.Format(@"D:\Repos\GmuGeo\Examples\png\{0}.png", fileName);

        }

        private void SaveBitmapGrayBuffered(Dataset ds, string filename, int iOverview)
        {
            // Get the GDAL Band objects from the Dataset
            Band band = ds.GetRasterBand(1);
            ColorTable ct = band.GetRasterColorTable();
            if (iOverview >= 0 && band.GetOverviewCount() > iOverview)
                band = band.GetOverview(iOverview);

            // Get the width and height of the Dataset
            int width = band.XSize;
            int height = band.YSize;

            KmlElement element = new KmlElement();
            element.ImageWidth = width;
            element.ImageHeight = height;
            element.UperLeft = GDALInfoGetPosition(ds, 0.00, 0.00);
            element.UperRight = GDALInfoGetPosition(ds, width, 0.00);
            element.LowerLeft = GDALInfoGetPosition(ds, 0.00, height);
            element.LowerRight = GDALInfoGetPosition(ds, width, height);
            element.ImageFileName = Path.GetFileName(filename);
            
            // Create a Bitmap to store the GDAL image in
            // Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);

            // BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            // bitmap.Palette = GenerateColorPalette(bitmap.Palette);

            // int stride = bitmapData.Stride;
            // IntPtr buf = bitmapData.Scan0;

            DateTime start = DateTime.Now;

            byte[] r = new byte[width * height];
            band.ReadRaster(0, 0, width, height, r, width, height, 0, 0);
            //band.ReadRaster(0, 0, width, height, buf, width, height, DataType.GDT_Byte, 1, stride);
            TimeSpan renderTime = DateTime.Now - start;
            Console.WriteLine("SaveBitmapBuffered fetch time: " + renderTime.TotalMilliseconds + " ms");

            for (int i = 0; i<width; i++)
            {
                for (int j=0; j<height; j++)
                {
                    // Color newColor = Color.FromArgb(Convert.ToInt32(r[i + j * width]), Convert.ToInt32(r[i + j * width]), Convert.ToInt32(r[i + j * width]));
                    Color newColor = GetColor(i + j * width, r);
                    bitmap.SetPixel(i, j, newColor);
                }
            }
 
            // bitmap.UnlockBits(bitmapData);
            bitmap.Save(filename);
        }

        private GeoPosition GDALInfoGetPosition(Dataset ds, double x, double y)
        {
            double[] geoTransform = new double[6];
            ds.GetGeoTransform(geoTransform);

            double geoX = geoTransform[0] + geoTransform[1] * x + geoTransform[2] * y;
            double geoY = geoTransform[3] + geoTransform[4] * x + geoTransform[5] * y;

            return new GeoPosition() { X = geoX, Y = geoY };
        }

        private Color GetColor(int position, byte[] data)
        {
            Color result = new Color();

            /*
            Index   Meaning	                                    R	G	B   Index
            14	    Fillvalue	                                0	0	0   14	 
            28	    Floodwater without water fraction	        0	0	100 28	 
            42	    Clear-sky dry land	                        196	162	114 42	 
            56	    Supra-snow/ice water or mixed water&ice	    180	0	230 56	 
            70	    Snow	                                    255	255	255 70	 
            84	    Ice	0	                                    255	255     84	 
            98	    Cloud	                                    200	200	200 98	 
            112	    Cloud shadow	                            100	100	100 112	 
            126	    Normal open water	                        0	0	255 126	 
            140	    Floodwaterfraction:_1-20%	                50	255	100 140	 
            154	    Floodwaterfraction:_21-30%	                0	255	0   154	 
            168	    Floodwaterfraction:_31-40%	                200	255	0   168	 
            182	    Floodwaterfraction:_41-50%	                255	255	150 182	 
            196	    Floodwaterfraction:_51-60%	                255	255	0   196	 
            210	    Floodwaterfraction:_61-70%	                255	200	0   210	 
            224	    Floodwaterfraction:_71-80%	                255	150	50  224	 
            238	    Floodwaterfraction:_81-90%	                255	100	0   238	 
            252	    Floodwaterfraction:_91-100%	                255	0	0   252	 
            */
            if (data[position] == 14)
            {
                result = Color.FromArgb(255, 0, 0, 0);
                Console.WriteLine("Fill value: 0, 0, 0");
            }
            else if (data[position] == 28)
            {
                result = Color.FromArgb(255, 0, 0, 100);
            }
            else if (data[position] == 42)
            {
                result = Color.FromArgb(255, 196, 162, 114);
            }
            else if (data[position] == 56)
            {
                result = Color.FromArgb(255, 180, 0, 230);
            }
            else if (data[position] == 70)
            {
                result = Color.FromArgb(255, 255, 255, 255);
            }
            else if (data[position] == 84)
            {
                result = Color.FromArgb(255, 0, 255, 255);
            }
            else if (data[position] == 98)
            {
                result = Color.FromArgb(255, 200, 200, 200);
            }
            else if (data[position] == 112)
            {
                result = Color.FromArgb(255, 100, 100, 100);
            }
            else if (data[position] == 126)
            {
                result = Color.FromArgb(255, 0, 0, 255);
            }
            else if (data[position] == 140)
            {
                result = Color.FromArgb(255, 50, 255, 100);
            }
            else if (data[position] == 154)
            {
                result = Color.FromArgb(255, 0, 255, 0);
            }
            else if (data[position] == 168)
            {
                result = Color.FromArgb(255, 200, 255, 0);
            }
            else if (data[position] == 182)
            {
                result = Color.FromArgb(255, 255, 255, 150);
            }
            else if (data[position] == 196)
            {
                result = Color.FromArgb(255, 255, 255, 0);
            }
            else if (data[position] == 210)
            {
                result = Color.FromArgb(255, 255, 200, 0);
            }
            else if (data[position] == 224)
            {
                result = Color.FromArgb(255, 255, 150, 50);
            }
            else if (data[position] == 238)
            {
                result = Color.FromArgb(255, 255, 100, 0);
            }
            else if (data[position] == 252)
            {
                result = Color.FromArgb(255, 255, 0, 0);
            }
            else
            {
                result = Color.FromArgb(Convert.ToInt32(data[position]), Convert.ToInt32(data[position]), Convert.ToInt32(data[position]));
            }

            return result;
        }

        private List<string> GetGeoTiffFiles()
        {
            // Example string: ftp://13.78.149.101:21/2018/08/15
            string url = GetFileServerUrl("13.78.149.101", 21, new DateTime(2018, 8, 15));
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(url);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            ftpRequest.Credentials = GetFileServerCredential();

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
                if (fileName.Contains("_230000.p") && fileName.EndsWith(".tif"))
                {
                    filteredResult.Add(string.Format("{0}/{1}", url, fileName));
                }
            }
            
            return filteredResult;
        }

        private NetworkCredential GetFileServerCredential()
        {
            return new NetworkCredential("tiffauthor", "07Apples");
        }

        private string GetFileServerUrl(string host, int port, DateTime date)
        {
            return string.Format("ftp://{0}:{1}/{2}", host, port, date.ToString("yyyy/MM/dd"));
        }

        private void DownloadGeoTiffFile(string fileUrl, string localFilePath)
        {
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
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

        private string GetLocalTiffFileName(string fileNameWithOutExtension)
        {
            return string.Format(@"D:\Repos\GmuGeo\Examples\png\{0}.tif", fileNameWithOutExtension);
        }

        private string GetLocalPngFileName(string fileNameWithOutExtension)
        {
            return string.Format(@"D:\Repos\GmuGeo\Examples\png\{0}.png", fileNameWithOutExtension);
        }

        private void UploadKmzFile(string localFilePath, string fileUrl)
        {
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
            ftpRequest.Credentials = GetFileServerCredential();

            using (FileStream localFileSteam = File.OpenRead(localFilePath))
            {
                ftpRequest.ContentLength = localFileSteam.Length;
                using (Stream requestStream = ftpRequest.GetRequestStream())
                {
                    localFileSteam.CopyTo(requestStream);
                }

                using (FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                }
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////
        // Legacy function
        ///////////////////////////////////////////////////////////////////////////////////////////
        private void SaveBitmapBuffered(Dataset ds, string filename, int iOverview)
        {
            // Get the GDAL Band objects from the Dataset
            Band redBand = ds.GetRasterBand(1);

            if (redBand.GetRasterColorInterpretation() == ColorInterp.GCI_PaletteIndex)
            {
                SaveBitmapPaletteBuffered(ds, filename, iOverview);
                return;
            }

            if (redBand.GetRasterColorInterpretation() == ColorInterp.GCI_GrayIndex)
            {
                SaveBitmapGrayBuffered(ds, filename, iOverview);
                return;
            }

            if (redBand.GetRasterColorInterpretation() != ColorInterp.GCI_RedBand)
            {
                Console.WriteLine("Non RGB images are not supported by this sample! ColorInterp = " +
                    redBand.GetRasterColorInterpretation().ToString());
                return;
            }

            if (ds.RasterCount < 3)
            {
                Console.WriteLine("The number of the raster bands is not enough to run this sample");
                System.Environment.Exit(-1);
            }

            if (iOverview >= 0 && redBand.GetOverviewCount() > iOverview)
                redBand = redBand.GetOverview(iOverview);

            Band greenBand = ds.GetRasterBand(2);

            if (greenBand.GetRasterColorInterpretation() != ColorInterp.GCI_GreenBand)
            {
                Console.WriteLine("Non RGB images are not supported by this sample! ColorInterp = " +
                    greenBand.GetRasterColorInterpretation().ToString());
                return;
            }

            if (iOverview >= 0 && greenBand.GetOverviewCount() > iOverview)
                greenBand = greenBand.GetOverview(iOverview);

            Band blueBand = ds.GetRasterBand(3);

            if (blueBand.GetRasterColorInterpretation() != ColorInterp.GCI_BlueBand)
            {
                Console.WriteLine("Non RGB images are not supported by this sample! ColorInterp = " +
                    blueBand.GetRasterColorInterpretation().ToString());
                return;
            }

            if (iOverview >= 0 && blueBand.GetOverviewCount() > iOverview)
                blueBand = blueBand.GetOverview(iOverview);

            // Get the width and height of the raster
            int width = redBand.XSize;
            int height = redBand.YSize;

            // Create a Bitmap to store the GDAL image in
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);

            DateTime start = DateTime.Now;

            byte[] r = new byte[width * height];
            byte[] g = new byte[width * height];
            byte[] b = new byte[width * height];

            redBand.ReadRaster(0, 0, width, height, r, width, height, 0, 0);
            greenBand.ReadRaster(0, 0, width, height, g, width, height, 0, 0);
            blueBand.ReadRaster(0, 0, width, height, b, width, height, 0, 0);
            TimeSpan renderTime = DateTime.Now - start;
            Console.WriteLine("SaveBitmapBuffered fetch time: " + renderTime.TotalMilliseconds + " ms");

            int i, j;
            for (i = 0; i < width; i++)
            {
                for (j = 0; j < height; j++)
                {
                    Color newColor = Color.FromArgb(Convert.ToInt32(r[i + j * width]), Convert.ToInt32(g[i + j * width]), Convert.ToInt32(b[i + j * width]));
                    bitmap.SetPixel(i, j, newColor);
                }
            }

            bitmap.Save(filename);
        }

        private void SaveBitmapPaletteBuffered(Dataset ds, string filename, int iOverview)
        {
            // Get the GDAL Band objects from the Dataset
            Band band = ds.GetRasterBand(1);
            if (iOverview >= 0 && band.GetOverviewCount() > iOverview)
                band = band.GetOverview(iOverview);

            ColorTable ct = band.GetRasterColorTable();
            if (ct == null)
            {
                Console.WriteLine("Band has no color table!");
                return;
            }

            if (ct.GetPaletteInterpretation() != PaletteInterp.GPI_RGB)
            {
                Console.WriteLine("Only RGB palette interp is supported by this sample!");
                return;
            }

            // Get the width and height of the Dataset
            int width = band.XSize;
            int height = band.YSize;

            // Create a Bitmap to store the GDAL image in
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);

            DateTime start = DateTime.Now;

            byte[] r = new byte[width * height];

            band.ReadRaster(0, 0, width, height, r, width, height, 0, 0);
            TimeSpan renderTime = DateTime.Now - start;
            Console.WriteLine("SaveBitmapBuffered fetch time: " + renderTime.TotalMilliseconds + " ms");

            int i, j;
            for (i = 0; i < width; i++)
            {
                for (j = 0; j < height; j++)
                {
                    ColorEntry entry = ct.GetColorEntry(r[i + j * width]);
                    Color newColor = Color.FromArgb(Convert.ToInt32(entry.c1), Convert.ToInt32(entry.c2), Convert.ToInt32(entry.c3));
                    bitmap.SetPixel(i, j, newColor);
                }
            }

            bitmap.Save(filename);
        }

        private ColorPalette GenerateColorPalette(ColorPalette colorPalette)
        {
            ColorPalette updatedColorPalette = colorPalette;
            /*
            Index   Meaning	                                    R	G	B   Index
            14	    Fillvalue	                                0	0	0   14	 
            28	    Floodwater without water fraction	        0	0	100 28	 
            42	    Clear-sky dry land	                        196	162	114 42	 
            56	    Supra-snow/ice water or mixed water&ice	    180	0	230 56	 
            70	    Snow	                                    255	255	255 70	 
            84	    Ice	0	                                    255	255     84	 
            98	    Cloud	                                    200	200	200 98	 
            112	    Cloud shadow	                            100	100	100 112	 
            126	    Normal open water	                        0	0	255 126	 
            140	    Floodwaterfraction:_1-20%	                50	255	100 140	 
            154	    Floodwaterfraction:_21-30%	                0	255	0   154	 
            168	    Floodwaterfraction:_31-40%	                200	255	0   168	 
            182	    Floodwaterfraction:_41-50%	                255	255	150 182	 
            196	    Floodwaterfraction:_51-60%	                255	255	0   196	 
            210	    Floodwaterfraction:_61-70%	                255	200	0   210	 
            224	    Floodwaterfraction:_71-80%	                255	150	50  224	 
            238	    Floodwaterfraction:_81-90%	                255	100	0   238	 
            252	    Floodwaterfraction:_91-100%	                255	0	0   252	 
            */
            for (int n = 0; n < 256; n++)
            {
                if (n == 14)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 0, 0, 0);
                }
                else if (n == 28)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 0, 0, 100);
                }
                else if (n == 42)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 196, 162, 114);
                }
                else if (n == 56)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 180, 0, 230);
                }
                else if (n == 70)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 255, 255, 255);
                }
                else if (n == 84)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 0, 255, 255);
                }
                else if (n == 98)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 200, 200, 200);
                }
                else if (n == 112)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 100, 100, 100);
                }
                else if (n == 126)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 0, 0, 255);
                }
                else if (n == 140)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 50, 255, 100);
                }
                else if (n == 154)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 0, 255, 0);
                }
                else if (n == 168)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 200, 255, 0);
                }
                else if (n == 182)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 255, 255, 150);
                }
                else if (n == 196)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 255, 255, 0);
                }
                else if (n == 210)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 255, 200, 0);
                }
                else if (n == 224)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 255, 150, 50);
                }
                else if (n == 238)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 255, 100, 0);
                }
                else if (n == 252)
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, 255, 0, 0);
                }
                else
                {
                    updatedColorPalette.Entries[n] = Color.FromArgb(255, n, n, n);
                }
            }

            return updatedColorPalette;
        }
    }
}
