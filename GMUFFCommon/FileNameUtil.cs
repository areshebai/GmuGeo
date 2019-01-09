using System;
using System.IO;

namespace GMUFFCommon
{
    public enum DataFileType
    {
        GOES16 = 1,
        J01,
        SNNP
    }

    public static class FileNameUtil
    {
        private static readonly char[] SplitCharacters = {'_', '.' };

        public static int GetBlockIndexFromFileName(string fileName, DataFileType type = DataFileType.SNNP)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(SplitCharacters, StringSplitOptions.RemoveEmptyEntries);

            int index = -1;
            switch (type)
            {
                case DataFileType.GOES16:
                    break;
                case DataFileType.J01:
                case DataFileType.SNNP:
                    if (elements.Length != 5)
                    {
                        throw new Exception();
                    }
                    index = Convert.ToInt32(elements[3]);
                    break;
            }

            return index;
        }

        public static DateTime GetDateFromFileName(string fileName, DataFileType type = DataFileType.SNNP)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(SplitCharacters, StringSplitOptions.RemoveEmptyEntries);

            DateTime date = new DateTime();
            switch (type)
            {
                case DataFileType.GOES16:
                    if (elements.Length != 11)
                    {
                        throw new Exception();
                    }
                    date = new DateTime(Convert.ToInt32(elements[3].Substring(0, 4)), Convert.ToInt32(elements[3].Substring(4, 2)), Convert.ToInt32(elements[3].Substring(6, 2)));
                    break;
                case DataFileType.J01:
                case DataFileType.SNNP:
                    if (elements.Length != 5)
                    {
                        throw new Exception();
                    }
                    date = new DateTime(Convert.ToInt32(elements[1].Substring(0, 4)), Convert.ToInt32(elements[1].Substring(4, 2)), Convert.ToInt32(elements[1].Substring(6, 2)));
                    break;
            }

            return date;
        }

        public static int GetHourFromFileName(string fileName, DataFileType type = DataFileType.SNNP)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(SplitCharacters, StringSplitOptions.RemoveEmptyEntries);

            int hour = 0;
            switch (type)
            {
                case DataFileType.GOES16:
                    if (elements.Length != 11)
                    {
                        throw new Exception();
                    }
                    hour = Convert.ToInt32(elements[5].Substring(0, 2));
                    break;
                case DataFileType.J01:
                case DataFileType.SNNP:
                    if (elements.Length != 5)
                    {
                        throw new Exception();
                    }
                    hour = Convert.ToInt32(elements[2].Substring(0, 2));
                    break;
            }

            return hour;
        }
    }
}
