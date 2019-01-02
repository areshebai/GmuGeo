using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace GMUFloodForecastPortal.Controllers
{
    [Produces("application/json")]
    [Route("api/Kml")]
    public class KmlController : Controller
    {
        private readonly string kmlFullFilePathFormat = @"http://13.77.200.161/{0}";
        // GET: api/Kml
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> kmlFiles = new List<string>();

            string serverUrl = GetFileServerUrl("13.78.149.101", 21, new DateTime(2018, 08, 15));
            serverUrl += "/uni";

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
                if (fileName.EndsWith(".kml"))
                {
                    if (fileName.Contains("_1.kml") || fileName.Contains("_9.kml"))
                        continue;
                    string serverFilePath = string.Format("{0}/{1}", "kmls/2018/08/15/uni", fileName);
                    kmlFiles.Add(string.Format(kmlFullFilePathFormat, serverFilePath));
                }
            }

            kmlFiles.Sort(compareKmlFilesByDistrictIndex);

            return kmlFiles;
        }

        private int compareKmlFilesByDistrictIndex(string fileName1, string fileName2)
        {
            int dIndex1 = GetBlockIndexFromFileName(fileName1);
            int dIndex2 = GetBlockIndexFromFileName(fileName2);

            return dIndex1.CompareTo(dIndex2);
        }

        private int GetBlockIndexFromFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != 4)
            {
                throw new Exception();
            }

            return Convert.ToInt32(elements[3]);
        }

        private NetworkCredential GetFileServerCredential()
        {
            return new NetworkCredential("tiffauthor", "07Apples");
        }

        private string GetFileServerUrl(string host, int port, DateTime date)
        {
            return string.Format("ftp://{0}:{1}/{2}", host, port, date.ToString("yyyy/MM/dd"));
        }

        // GET: api/Kml/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<string> Get(int id)
        {
            List<string> kmlFiles = new List<string>();

            string serverUrl = GetFileServerUrl("13.78.149.101", 21, new DateTime(2018, 08, 15));
            serverUrl += "/dis";

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
