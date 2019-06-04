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
    [Produces("application/json")]
    [Route("api/Kml")]
    public class KmlController : Controller
    {
        private readonly string kmlFullFilePathFormat = @"https://jpssflood.gmu.edu/kmls/{0}.kml";
        private readonly string DatabaseConnectionstring = @"server=127.0.0.1;userid=root;password=07Apples;database=jpssflood;";
        private readonly string DatabaseConnectionstringProd = @"server=localhost;userid=root;database=jpssflood;";

        // GET: api/Kml
        [HttpGet]
        public IEnumerable<string> Get(DateTime from, DateTime to, string step, string region, string product)
        {
            List<string> kmlFiles = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnectionstringProd))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM jpssflood.kmlmetadata";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int kmlId = reader.GetInt32(0);
                    int productId = reader.GetInt32(1);
                    int regionId = reader.GetInt32(2);
                    int districtId = reader.GetInt32(3);
                    MySql.Data.Types.MySqlDateTime mySqldate = reader.GetMySqlDateTime(4);
                    string fileName = reader.GetString(5);

                    DateTime date = new DateTime(mySqldate.Year, mySqldate.Month, mySqldate.Day, mySqldate.Hour, 0, 0);
                    // if (date >= from && date <= to)
                    {
                        kmlFiles.Add(string.Format(kmlFullFilePathFormat, fileName));
                    }
                }
            }

            return kmlFiles;
        }

        private int compareKmlFilesByDistrictIndex(string fileName1, string fileName2)
        {
            int dIndex1 = FileNameUtil.GetBlockIndexFromFileName(fileName1);
            int dIndex2 = FileNameUtil.GetBlockIndexFromFileName(fileName2);

            return dIndex1.CompareTo(dIndex2);
        }

        // GET: api/Kml/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<string> Get(int id)
        {
            List<string> kmlFiles = new List<string>();

            string serverUrl = FtpUtil.GetFileServerUrl("13.78.149.101", 21, new DateTime(2018, 08, 15));
            serverUrl += "/dis";

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
                if (fileName.EndsWith(".kml"))
                {
                    if (fileName.Contains("_1.kml") || fileName.Contains("_9.kml"))
                        continue;

                    string timeSegment = string.Format("_{0}0000_", id.ToString("00"));
                    if (fileName.Contains(timeSegment))
                    {
                        string serverFilePath = string.Format("{0}/{1}", "kmls/2018/08/15/dis", fileName);
                        kmlFiles.Add(string.Format(kmlFullFilePathFormat, serverFilePath));
                    }
                }
            }

            kmlFiles.Sort(compareKmlFilesByDistrictIndex);

            return kmlFiles;
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
        }
    }
}
