﻿using System;
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
            // instance.Execute();
            // instance.DownloadAndSaveFiles();
            instance.JoinImages(@"C:\Temp\2018\08\15\dis", 1);
        }
    }
}