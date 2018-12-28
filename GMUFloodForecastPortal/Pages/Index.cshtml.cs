using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GMUFloodForecastPortal.Pages
{
    public class IndexModel : PageModel
    {
        public List<string> KmlFiles = new List<string>();

        public void OnGet()
        {
        /*
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
                KmlFiles = directoryOrFileNames.ToList();
            }
            */
        }
    }
}
