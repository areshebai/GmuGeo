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
            bool generateSqlInsertCommands = true;
            bool generateJ01Files = true;

            if (downloadAndSaveFiles)
            {
                instance.ProcessRawPackagesFromFtpServer();
            }

            if (joinImages)
            {
                // instance.JoinImages("");
            }

            if (generateSqlInsertCommands)
            {
                instance.GenerateSqlInsertCommands(@"D:\GMUTrans\Products\J01\2019\06\11\Trans");
            }

            if (generateJ01Files)
            {
                string sourceDir = @"D:\GMUTrans\Products\J01\2019\06\11";
                string targetDir = @"D:\GMUTrans\Products\J01\2019\06\11\Trans";
                instance.GenerateDailyJ01DisplayFiles(sourceDir, targetDir);
            }
        }
    }
}
