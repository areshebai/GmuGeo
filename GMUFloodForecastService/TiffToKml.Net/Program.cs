using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiffToKml.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            TiffToKml instance = new TiffToKml();

            bool downloadAndSaveFiles = false;
            bool joinImages = false;
            bool processImages = true;

            if (downloadAndSaveFiles)
            {
                instance.DownloadAndSaveFiles();
            }

            if (joinImages)
            {
                instance.JoinImages("");
            }

            if (processImages)
            {
                instance.ProcessImages(@"C:\GmuTemp\2018\08\15\uni", 10);
            }
        }
    }
}
