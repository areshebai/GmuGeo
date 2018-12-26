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
    public class TiffProcessor
    {
        public void StartToProcess()
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

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
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
            string url = FileServerUtil.GetFileServerUrl("13.78.149.101", 21, new DateTime(2018, 8, 15));
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(url);
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
                if (fileName.Contains("_230000.p") && fileName.EndsWith(".tif"))
                {
                    filteredResult.Add(string.Format("{0}/{1}", url, fileName));
                }
            }

            return filteredResult;
        }

        private void DownloadGeoTiffFile(string fileUrl, string localFilePath)
        {
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            ftpRequest.Credentials = FileServerUtil.GetFileServerCredential();

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
            ftpRequest.Credentials = FileServerUtil.GetFileServerCredential();

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
