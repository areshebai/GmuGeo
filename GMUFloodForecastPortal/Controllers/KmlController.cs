﻿using System;
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
        // GET: api/Kml
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> kmlFiles = new List<string>();

            string kmlFullFilePathFormat = @"http://13.78.136.56/{0}";

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

            return kmlFiles;
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
        public string Get(int id)
        {
            return "value";
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
