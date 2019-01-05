using System;
using System.IO;

namespace GMUFFCommon
{
    public static class FileNameUtil
    {
        public static int GetBlockIndexFromFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != 4)
            {
                throw new Exception();
            }

            return Convert.ToInt32(elements[3]);
        }

        public static DateTime GetDateFromFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != 4)
            {
                throw new Exception();
            }

            return new DateTime(Convert.ToInt32(elements[1].Substring(0, 4)), Convert.ToInt32(elements[1].Substring(4, 2)), Convert.ToInt32(elements[1].Substring(6, 2)));
        }

        public static int GetHourFromFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            string[] elements = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != 4)
            {
                throw new Exception();
            }

            return Convert.ToInt32(elements[2].Substring(0, 2));
        }
    }
}
