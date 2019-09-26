using System;
using System.IO;

namespace GMUFFCommon
{
    public enum DataFileType
    {
        ABI = 1,
        AHI,
        VIIRSABI,
        VIIRSAHI,
        VIIRS1Day,
        VIIRS5Day
    }

    public static class FileNameUtil
    {
        private static readonly char[] SplitCharacters = {'_', '.' };

        public static int GetBlockIndexFromFileName(string fileName, DataFileType type = DataFileType.ABI)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(SplitCharacters, StringSplitOptions.RemoveEmptyEntries);

            int index = -1;
            switch (type)
            {
                case DataFileType.ABI:
                case DataFileType.AHI:
                    if (elements.Length != 14)
                    {
                        throw new Exception(string.Format("Unsupported ABI or AHI file name format: {0}", fileName));
                    }
                    index = Convert.ToInt32(elements[3]);
                    break;
                case DataFileType.VIIRSABI:
                case DataFileType.VIIRSAHI:
                    if (elements.Length != 11)
                    {
                        throw new Exception(string.Format("Unsupported VIIRSABI or VIIRSAHI file name format: {0}", fileName));
                    }
                    index = Convert.ToInt32(elements[10]);
                    break;
                case DataFileType.VIIRS1Day:
                case DataFileType.VIIRS5Day:
                    if (elements.Length != 12)
                    {
                        throw new Exception(string.Format("Unsupported VIIRS1Day or VIIRS5Day file name format: {0}", fileName));
                    }

                    string dayStep = elements[10].ToLower();
                    if (dayStep != "001day" && dayStep != "005day")
                    {
                        throw new Exception(string.Format("Bad VIIRS1Day or VIIRS5Day file name format: {0}", fileName));
                    }

                    index = Convert.ToInt32(elements[11]);
                    break;
            }

            return index;
        }

        public static DateTime GetDateFromFileName(string fileName, DataFileType type = DataFileType.ABI)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(SplitCharacters, StringSplitOptions.RemoveEmptyEntries);

            DateTime date = new DateTime();
            switch (type)
            {
                case DataFileType.ABI:
                case DataFileType.AHI:
                    if (elements.Length != 14)
                    {
                        throw new Exception(string.Format("Unsupported ABI or AHI file name format: {0}", fileName));
                    }
                    date = new DateTime(Convert.ToInt32(elements[4].Substring(0, 4)), Convert.ToInt32(elements[4].Substring(4, 2)), Convert.ToInt32(elements[4].Substring(6, 2)));
                    break;
                case DataFileType.VIIRSABI:
                case DataFileType.VIIRSAHI:
                    if (elements.Length != 11)
                    {
                        throw new Exception(string.Format("Unsupported VIIRSABI or VIIRSAHI file name format: {0}", fileName));
                    }
                    date = new DateTime(Convert.ToInt32(elements[6].Substring(1, 4)), Convert.ToInt32(elements[6].Substring(5, 2)), Convert.ToInt32(elements[6].Substring(7, 2)));
                    break;
                case DataFileType.VIIRS1Day:
                case DataFileType.VIIRS5Day:
                    if (elements.Length != 12)
                    {
                        throw new Exception(string.Format("Unsupported VIIRS1Day or VIIRS5Day file name format: {0}", fileName));
                    }
                    date = new DateTime(Convert.ToInt32(elements[5].Substring(1, 4)), Convert.ToInt32(elements[5].Substring(5, 2)), Convert.ToInt32(elements[5].Substring(7, 2)));
                    break;
            }

            return date;
        }

        public static int GetHourFromFileName(string fileName, DataFileType type = DataFileType.ABI)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(SplitCharacters, StringSplitOptions.RemoveEmptyEntries);

            int hour = 0;
            switch (type)
            {
                case DataFileType.ABI:
                case DataFileType.AHI:
                    throw new Exception(string.Format("Hour value is not supported by ABI or AHI file: {0}", fileName));
                case DataFileType.VIIRSABI:
                case DataFileType.VIIRSAHI:
                    if (elements.Length != 11)
                    {
                        throw new Exception(string.Format("Unsupported VIIRSABI or VIIRSAHI file name format: {0}", fileName));
                    }
                    hour = Convert.ToInt32(elements[7]);
                    break;
                case DataFileType.VIIRS1Day:
                case DataFileType.VIIRS5Day:
                    if (elements.Length != 12)
                    {
                        throw new Exception(string.Format("Unsupported VIIRS1Day or VIIRS5Day file name format: {0}", fileName));
                    }
                    hour = Convert.ToInt32(elements[9]);
                    break;
            }

            return hour;
        }
    }
}
