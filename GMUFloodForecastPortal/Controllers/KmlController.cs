using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using GMUFFCommon;
using MySql.Data.MySqlClient;

namespace GMUFloodForecastPortal.Controllers
{
    public class KmlFileInfo
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public int DistrictId { get; set; }
    }

    [Produces("application/json")]
    [Route("api/kmls")]
    public class KmlController : Controller
    {
        private readonly string kmlFullFilePathFormat = @"https://jpssflood.gmu.edu/kmls/{0}.kml";
        private readonly static string DatabaseConnectionstringLocal = @"server=127.0.0.1;userid=root;password=07Apples;database=jpssflood;";
        private readonly static string DatabaseConnectionstringProd = @"server=localhost;userid=root;database=jpssflood;";
        private string DatabaseConnectionstring = DatabaseConnectionstringLocal;

        // File name formats
        /*
        private readonly string ABIFileNameFormat = @"COM_G16_ABI_WATER_20190920_2019263_0950_2220_0349_1620_4800_5000_76_004";
        private readonly string AHIFileNameFormat = @"COM_H08_AHI_WATER_20190923_2019266_2300_1040_0700_1839_6000_2000_69_001.kml";
        private readonly string VIIRSABIFileNameFormat = @"Joint_VIIRS_ABI_WATER_Prj_SVI_d20190920_17_4448_4448_015.kml";
        private readonly string VIIRSAHIFileNameFormat = @"Joint_VIIRS_AHI_WATER_Prj_SVI_d20190924_18_4448_4448_135.kml";
        private readonly string VIIRS5DaysFileNameFormat = @"WATER_COM_VIIRS_Prj_SVI_d20190916_d20190920_2966_2966_24_005day_133.kml";
        private readonly string VIIRS1DaysFileNameFormat = @"WATER_COM_VIIRS_Prj_SVI_d20190920_d20190920_4448_4448_6_001day_118.kml";
        */

        // GET: api/Kml
        [HttpGet]
        public JsonResult Get(DateTime from, DateTime to, int step, string region, string product)
        {
            List<KmlFileInfo> kmlFiles = new List<KmlFileInfo>();

            string fromDateFormatString = string.Empty;
            string toDateFormatString = string.Empty;

            to = from;
            if (step == 1)
            {
                fromDateFormatString = @"yyyy-MM-dd hh:00:00";
                toDateFormatString = @"yyyy-MM-dd hh:00:00";
            }
            else
            {
                fromDateFormatString = @"yyyy-MM-dd 00:00:00";
                toDateFormatString = @"yyyy-MM-dd 23:00:00";
            }

            int queryProductId = ConvertProductStringToId(product);

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnectionstring))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT * FROM jpssflood.kmlmetadata WHERE Date >= '{0}' AND Date <= '{1}' AND ProductId = {2} AND RegionId = {3} AND DistrictId > 1 AND DistrictId < 136 ORDER BY DistrictID DESC", from.ToString(fromDateFormatString), to.AddHours(1).ToString(toDateFormatString), queryProductId, 1);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int kmlId = reader.GetInt32(0);
                    int productId = reader.GetInt32(1);
                    int regionId = reader.GetInt32(2);
                    int districtId = reader.GetInt32(3);
                    MySql.Data.Types.MySqlDateTime mySqldate = reader.GetMySqlDateTime(4);
                    string fileName = reader.GetString(5);

                    Region regionFromDistrict = RegionUtil.GetRegion(districtId);
                    if (Region.All != RegionUtil.GetRegionFromDisplayName(region) && queryProductId == 1)
                    {
                        if (regionFromDistrict != RegionUtil.GetRegionFromDisplayName(region))
                        {
                            continue;
                        }
                    }

                    if (step != 3 && fileName.Contains("_005day_"))
                    {
                        continue;
                    }

                    if (step == 3 && !fileName.Contains("_005day_"))
                    {
                        continue;
                    }

                    kmlFiles.Add(new KmlFileInfo { FullName = string.Format(kmlFullFilePathFormat, fileName), ShortName = fileName, DistrictId = districtId });
                }
            }

            return Json(kmlFiles);
        }

        private int ConvertProductStringToId(string product)
        {
            if (product == "VIIRS 375-m")
            {
                return 1;
            }

            if (product == "ABI 1-km")
            {
                return 2;
            }

            if (product == "AHI 1-km")
            {
                return 3;
            }

            if (product == "Joint VIIRS/ABI")
            {
                return 4;
            }

            if (product == "Joint VIIRS/AHI")
            {
                return 5;
            }

            return 0;
        }

        private int compareKmlFilesByDistrictIndex(string fileName1, string fileName2)
        {
            int dIndex1 = FileNameUtil.GetBlockIndexFromFileName(fileName1);
            int dIndex2 = FileNameUtil.GetBlockIndexFromFileName(fileName2);

            return dIndex2.CompareTo(dIndex1);
        }

        private int compareKmlByDistrictIndex(KmlFileInfo file1, KmlFileInfo file2)
        {
            int dIndex1 = FileNameUtil.GetBlockIndexFromFileName(file1.ShortName);
            int dIndex2 = FileNameUtil.GetBlockIndexFromFileName(file2.ShortName);

            return dIndex2.CompareTo(dIndex1);
        }

        [HttpGet]
        [Route("LatestDataDate")]
        public string GetLatestDataDate()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseConnectionstring))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT * FROM jpssflood.kmlmetadata ORDER BY Date DESC LIMIT 1 ");
                MySqlDataReader reader = command.ExecuteReader();

                reader.Read();
                MySql.Data.Types.MySqlDateTime mySqldate = reader.GetMySqlDateTime(4);
                return mySqldate.Value.ToString("MM/dd/yyyy");
            }
        }

        [HttpGet]
        [Route("download")]
        public string GenerateDownloadPackage(DateTime from, DateTime to, int step, string region, int north, int south, int west, int east, string product, string format)
        {
            int result = 0;
            string dateFormatString = @"yyyy-MM-dd";
            string dateTimeFormatString = @"yyyy-MM-dd hh:mm:ss";

            int queryProductId = ConvertProductStringToId(product);

            string downloadTaskName = DateTime.UtcNow.ToString("yyyy_MM_dd") + "_" + DateTime.UtcNow.Ticks.ToString();

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnectionstring))
            {
                string queryCommandTextFormat = "INSERT INTO jpssflood.downloadtasks_v2 (CreatedTime, Name, StartTime, EndTime, Step, Product, Region, North, South, West, East, ProcessStartTime, ProcessEndTime, ImageFormat, Status) ";
                queryCommandTextFormat += "VALUES('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, {7}, {8}, {9}, {10}, '{11}', '{12}', '{13}', {14})";
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = string.Format(queryCommandTextFormat, DateTime.UtcNow.ToString(dateTimeFormatString)/*CreatedTime*/, downloadTaskName/*Name*/, 
                    from.ToString(dateFormatString)/*StartTime*/, to.ToString(dateFormatString)/*EndTime*/, 
                    1, queryProductId, 1, north, south, west, east,
                    DateTime.MinValue.ToString(dateTimeFormatString)/*ProcessStartTime*/, DateTime.MinValue.ToString(dateTimeFormatString)/*ProcessEndTime*/,
                    format/*ImageFormat*/, 1/*Status*/);
                result = command.ExecuteNonQuery();
            } 

            return result == 1 ? downloadTaskName : string.Empty;
        }
        
        // POST: api/Kml
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Kml/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new UnauthorizedAccessException("Delete is not allowed.");
        }
    }
}
