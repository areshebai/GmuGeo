using System;
using System.Collections.Generic;
using System.Text;

namespace GMUFFCommon
{
    public class RegionBoundary
    {
        public int North;
        public int South;
        public int West;
        public int East;
    }

    public static class RegionUtil
    {
        public static int[,] earthBlockIndexTable = new int[12, 24]
        {
            { 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 050, 052, 000, 000, 000, 000}, // 75-90
            { 001, 002, 003, 004, 005, 006, 007, 008, 000, 000, 000, 042, 043, 044, 045, 046, 047, 048, 049, 052, 053, 054, 055, 056}, // 60-75
            { 009, 010, 000, 011, 012, 013, 014, 015, 016, 000, 000, 057, 058, 059, 060, 061, 062, 063, 064, 065, 066, 067, 068, 069}, // 45-60
            { 000, 000, 000, 017, 018, 019, 020, 021, 000, 000, 000, 070, 071, 072, 073, 074, 075, 076, 077, 078, 079, 080, 081, 000}, // 30-45
            { 000, 000, 000, 000, 022, 023, 024, 025, 000, 000, 082, 083, 084, 085, 086, 087, 088, 089, 090, 091, 092, 000, 000, 000}, // 15-30
            { 000, 000, 000, 000, 000, 026, 027, 028, 029, 000, 093, 094, 095, 096, 097, 098, 099, 100, 101, 102, 103, 000, 000, 000}, // 00-15
            //180--165--150--135--120--105--90---75---60---45---30---15----0---15---30---45---60---75---90---105--120--135--150--165--180--------
            { 000, 000, 000, 000, 000, 000, 030, 031, 032, 033, 000, 000, 104, 105, 106, 107, 000, 108, 109, 110, 111, 112, 000, 000}, // 00-15
            { 000, 000, 000, 000, 000, 000, 000, 034, 035, 036, 000, 000, 113, 114, 115, 116, 000, 000, 000, 117, 118, 119, 120, 000}, // 15-30
            { 000, 000, 000, 000, 000, 000, 000, 037, 038, 000, 000, 000, 000, 121, 122, 000, 000, 000, 000, 123, 124, 125, 126, 127}, // 30-45
            { 000, 000, 000, 000, 000, 000, 039, 040, 041, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 128}, // 45-60
            { 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000}, // 60-75
            { 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000}, // 75-90
        };

        public static int[] NorthAmerica = new int[30] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 129, 130, 131, 132, 133 };
        public static int[] SourceAmerica = new int[6] { 36, 37, 38, 39, 40, 41};
        public static int[] Europe = new int[19] { 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 };
        public static int[] Asia = new int[29] { 61, 62, 63, 64, 65, 66, 67, 68, 69, 73, 74, 75, 76, 77, 78, 79, 80, 81, 87, 88, 89, 90, 91, 92, 99, 100, 101, 102, 103 };
        public static int[] Africa = new int[27] {70, 71, 72, 82, 83, 84, 85, 86, 87, 88, 89, 93, 94, 95, 96, 97, 98, 104, 105, 106, 107, 113, 114, 115, 116, 121, 122 };
        public static int[] Australia = new int[21] { 108, 109, 110, 111, 112, 117, 118, 119, 120, 123, 124, 125, 126, 127, 128, 129, 130, 133, 134, 135, 136 };

        public static Region GetRegion (int districtIndex)
        {
            if ((districtIndex >= 1 && districtIndex <= 25) ||
                (districtIndex >= 129 && districtIndex <= 133))
                return Region.NorthAmerica; // North America

            if (districtIndex >= 26 && districtIndex <= 41)
                return Region.SouthAmerica; // South America

            if ((districtIndex >= 42 && districtIndex <= 45) ||
                (districtIndex >= 57 && districtIndex <= 60) ||
                (districtIndex >= 70 && districtIndex <= 73))
                return Region.Europe; // Europe

            if ((districtIndex >= 62 && districtIndex <= 69) ||
                (districtIndex >= 73 && districtIndex <= 81) ||
                (districtIndex >= 86 && districtIndex <= 92) ||
                (districtIndex >= 99 && districtIndex <= 103) ||
                (districtIndex >= 108 && districtIndex <= 111))
                return Region.Asia; // Asia

            if ((districtIndex >= 70 && districtIndex <= 73) || (districtIndex >= 82 && districtIndex <= 86) || (districtIndex >= 93 && districtIndex <= 98) || (districtIndex >= 104 && districtIndex <= 107) || (districtIndex >= 113 && districtIndex <= 116) || (districtIndex >= 121 && districtIndex <= 122))
                return Region.Africa; // Africa

            if ((districtIndex >= 108 && districtIndex <= 110) || (districtIndex >= 117 && districtIndex <= 120) || (districtIndex >= 123 && districtIndex <= 136))
                return Region.Australia; // Australia

            return Region.All;
        }

        public static Region GetRegionFromDisplayName(string Name)
        {
            if (Name.ToLower() == "north america")
                return Region.NorthAmerica; // North America

            if (Name.ToLower() == "south america")
                return Region.SouthAmerica; // South America

            if (Name.ToLower() == "europe")
                return Region.Europe; // Europe

            if (Name.ToLower() == "asia")
                return Region.Asia; // Asia

            if (Name.ToLower() == "africa")
                return Region.Africa; // Africa

            if (Name.ToLower() == "australia")
                return Region.Australia; // Australia

            if (Name.ToLower() == "nws alaska")
                return Region.USAlaska;

            if (Name.ToLower() == "nws north east")
                return Region.USNorthEast;

            if (Name.ToLower() == "nws north central")
                return Region.USNorthCentral;

            if (Name.ToLower() == "nws south east")
                return Region.USSouthEast;

            if (Name.ToLower() == "nws missouri basin")
                return Region.USMissouriBasin;

            if (Name.ToLower() == "nws west gulf")
                return Region.USWestGulf;;

            if (Name.ToLower() == "nws north west")
                return Region.USNorthWest;

            if (Name.ToLower() == "nws south west")
                return Region.USSouthWest;

            return Region.All;
        }

        public static bool IsDistrictInRegion(int districtIndex, string targetRegionName)
        {
            bool isInRegion = false;
            Region targetRegion = GetRegionFromDisplayName(targetRegionName);
            switch (targetRegion)
            {
                case Region.NorthAmerica:
                    if ((districtIndex >= 1 && districtIndex <= 25) ||
                        (districtIndex >= 129 && districtIndex <= 133))
                        isInRegion = true;
                    break;
                case Region.SouthAmerica:
                    if (districtIndex >= 26 && districtIndex <= 41)
                        isInRegion = true;
                    break;
                case Region.Europe:
                    if ((districtIndex >= 42 && districtIndex <= 45) ||
                        (districtIndex >= 57 && districtIndex <= 60) ||
                        (districtIndex >= 70 && districtIndex <= 73))
                        isInRegion = true;
                    break;
                case Region.Asia:
                    if ((districtIndex >= 62 && districtIndex <= 69) ||
                        (districtIndex >= 73 && districtIndex <= 81) ||
                        (districtIndex >= 86 && districtIndex <= 92) ||
                        (districtIndex >= 99 && districtIndex <= 103) ||
                        (districtIndex >= 108 && districtIndex <= 111))
                        isInRegion = true;
                    break;
                case Region.Africa:
                    if ((districtIndex >= 70 && districtIndex <= 73) || 
                        (districtIndex >= 82 && districtIndex <= 86) || 
                        (districtIndex >= 93 && districtIndex <= 98) || 
                        (districtIndex >= 104 && districtIndex <= 107) || 
                        (districtIndex >= 113 && districtIndex <= 116) || 
                        (districtIndex >= 121 && districtIndex <= 122))
                        isInRegion = true;
                    break;
                case Region.Australia:
                    if ((districtIndex >= 110 && districtIndex <= 112) || 
                        (districtIndex >= 117 && districtIndex <= 120) || 
                        (districtIndex >= 123 && districtIndex <= 136))
                        isInRegion = true;
                    break;
                case Region.USAlaska:
                    if ((districtIndex >= 1 && districtIndex <= 2) ||
                        (districtIndex >= 9 && districtIndex <= 10))
                        isInRegion = true;
                    break;
                case Region.USNorthEast:
                    if ((districtIndex >= 13 && districtIndex <= 16) ||
                        (districtIndex >= 19 && districtIndex <= 21))
                        isInRegion = true;
                    break;
                case Region.USNorthCentral:
                    if ((districtIndex >= 12 && districtIndex <= 15) ||
                        (districtIndex >= 18 && districtIndex <= 21))
                        isInRegion = true;
                    break;
                case Region.USSouthEast:
                    if ((districtIndex >= 20 && districtIndex <= 21) ||
                        (districtIndex >= 24 && districtIndex <= 25))
                        isInRegion = true;
                    break;
                case Region.USMissouriBasin:
                    if ((districtIndex >= 13 && districtIndex <= 14) ||
                        (districtIndex >= 19 && districtIndex <= 20))
                        isInRegion = true;
                    break;
                case Region.USWestGulf:
                    if ((districtIndex >= 19 && districtIndex <= 20) ||
                        (districtIndex >= 23 && districtIndex <= 24))
                        isInRegion = true;
                    break;
                case Region.USNorthWest:
                    if ((districtIndex >= 12 && districtIndex <= 13) ||
                        (districtIndex >= 18 && districtIndex <= 19))
                        isInRegion = true;
                    break;
                case Region.USSouthWest:
                    if ((districtIndex >= 18 && districtIndex <= 19) ||
                        (districtIndex >= 22 && districtIndex <= 23))
                        isInRegion = true;
                    break;
                default:
                    isInRegion = false;
                    break;
            }

            return isInRegion;
        }
    }
}
