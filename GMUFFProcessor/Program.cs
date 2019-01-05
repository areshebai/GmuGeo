using System;

namespace GMUFFProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            MainProcessor instance = new MainProcessor();

            bool downloadAndSaveFiles = false;
            bool joinImages = false;
            bool processImages = true;

            if (downloadAndSaveFiles)
            {
                instance.ProcessRawPackagesFromFtpServer();
            }

            if (joinImages)
            {
                // instance.JoinImages("");
            }

            if (processImages)
            {
                // instance.ProcessImages(@"C:\GmuTemp\2018\08\15\uni", 10);
            }
        }
    }
}
