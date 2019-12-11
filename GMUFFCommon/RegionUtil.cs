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

        public static int[,] DistrictBounds = new int[137, 4]
        {
            {-180, 180, -90, 90},
            {-180, -165, 60, 75},
            {-165, -150, 60, 75},
            {-150, -135, 60, 75},
            {-135, -120, 60, 75},
            {-120, -105, 60, 75},
            {-105, -90, 60, 75},
            {-90, -75, 60, 75},
		    {-75, -60, 60, 75},
		    {-180, -165, 45, 60},
		    {-165, -150, 45, 60},
		    {-135, -120, 45, 60},
		    {-120, -105, 45, 60},
		    {-105, -90, 45, 60},
		    {-90, -75, 45, 60},
		    {-75, -60, 45, 60},
		    {-60, -45, 45, 60},
		    {-135, -120, 30, 45},
		    {-120, -105, 30, 45},
		    {-105, -90, 30, 45},
		    {-90, -75, 30, 45},
		    {-75, -60, 30, 45},
		    {-120, -105, 15, 30},
		    {-105, -90, 15, 30},
		    {-90, -75, 15, 30},
		    {-75, -60, 15, 30},
		    {-105, -90, 0, 15},
		    {-90, -75, 0, 15},
		    {-75, -60, 0, 15},
		    {-60, -45, 0, 15},
		    {-90, -75, -15, 0},
		    {-75, -60, -15, 0},
		    {-60, -45, -15, 0},
		    {-45, -30, -15, 0},
		    {-75, -60, -30, -15},
		    {-60, -45, -30, -15},
		    {-45, -30, -30, -15},
		    {-75, -60, -45, -30},
		    {-60, -45, -45, -30},
		    {-90, -75, -60, -45},
		    {-75, -60, -60, -45},
		    {-60, -45, -60, -45},
		    {-15, 0, 60, 75},
		    {0, 15, 60, 75},
		    {15, 30, 60, 75},
		    {30, 45, 60, 75},
		    {45, 60, 60, 75},
		    {60, 75, 60, 75},
		    {75, 90, 60, 75},
		    {90, 105, 60, 75},
		    {90, 105, 75, 90},
		    {105, 120, 75, 90},
		    {105, 120, 60, 75},
		    {120, 135, 60, 75},
		    {135, 150, 60, 75},
		    {150, 165, 60, 75},
		    {165, 180, 60, 75},
		    {-15, 0, 45, 60},
		    {0, 15, 45, 60},
		    {15, 30, 45, 60},
		    {30, 45, 45, 60},
		    {45, 60, 45, 60},
		    {60, 75, 45, 60},
		    {75, 90, 45, 60},
		    {90, 105, 45, 60},
		    {105, 120, 45, 60},
		    {120, 135, 45, 60},
		    {135, 150, 45, 60},
		    {150, 165, 45, 60},
		    {165, 180, 45, 60},
		    {-15, 0, 30, 45},
		    {0, 15, 30, 45},
		    {15, 30, 30, 45},
		    {30, 45, 30, 45},
		    {45, 60, 30, 45},
		    {60, 75, 30, 45},
		    {75, 90, 30, 45},
		    {90, 105, 30, 45},
		    {105, 120, 30, 45},
		    {120, 135, 30, 45},
		    {135, 150, 30, 45},
		    {150, 165, 30, 45},
		    {-30, -15, 15, 30},
		    {-15, 0, 15, 30},
		    {0, 15, 15, 30},
		    {15, 30, 15, 30},
		    {30, 45, 15, 30},
		    {45, 60, 15, 30},
		    {60, 75, 15, 30},
		    {75, 90, 15, 30},
		    {90, 105, 15, 30},
		    {105, 120, 15, 30},
		    {120, 135, 15, 30},
		    {-30, -15, 0, 15},
		    {-15, 0, 0, 15},
		    {0, 15, 0, 15},
		    {15, 30, 0, 15},
		    {30, 45, 0, 15},
		    {45, 60, 0, 15},
		    {60, 75, 0, 15},
		    {75, 90, 0, 15},
		    {90, 105, 0, 15},
		    {105, 120, 0, 15},
		    {120, 135, 0, 15},
		    {0, 15, -15, 0},
		    {15, 30, -15, 0},
		    {30, 45, -15, 0},
		    {45, 60, -15, 0},
		    {75, 90, -15, 0},
		    {90, 105, -15, 0},
		    {105, 120, -15, 0},
		    {120, 135, -15, 0},
		    {135, 150, -15, 0},
		    {0, 15, -30, -15},
		    {15, 30, -30, -15},
		    {30, 45, -30, -15},
		    {45, 60, -30, -15},
		    {105, 120, -30, -15},
		    {120, 135, -30, -15},
		    {135, 150, -30, -15},
		    {150, 165, -30, -15},
		    {15, 30, -45, -30},
		    {30, 45, -45, -30},
		    {105, 120, -45, -30},
		    {120, 135, -45, -30},
		    {135, 150, -45, -30},
		    {150, 165, -45, -30},
		    {165, 180, -45, -30},
		    {165, 180, -60, -45},
		    {-150, -135, 45, 60},
		    {-60, -45, 60, 75},
		    {-45, -30, 60, 75},
		    {-30, -15, 60, 75},
		    {-160, -150, 15, 25},
		    {150, 165, -15, 0},
		    {165, 180, -30, -15},
		    {-180, -165, -20, -5},
        };

        public static int[] NorthAmerica = new int[30] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 129, 130, 131, 132, 133 };
        public static int[] SourceAmerica = new int[6] { 36, 37, 38, 39, 40, 41};
        public static int[] Europe = new int[19] { 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 };
        public static int[] Asia = new int[29] { 61, 62, 63, 64, 65, 66, 67, 68, 69, 73, 74, 75, 76, 77, 78, 79, 80, 81, 87, 88, 89, 90, 91, 92, 99, 100, 101, 102, 103 };
        public static int[] Africa = new int[27] {70, 71, 72, 82, 83, 84, 85, 86, 87, 88, 89, 93, 94, 95, 96, 97, 98, 104, 105, 106, 107, 113, 114, 115, 116, 121, 122 };
        public static int[] Australia = new int[21] { 108, 109, 110, 111, 112, 117, 118, 119, 120, 123, 124, 125, 126, 127, 128, 129, 130, 133, 134, 135, 136 };

        public static readonly Dictionary<string, int[]> CountryDistricts = new Dictionary<string, int[]>
        {
            {"afghanistan", new int[]{75,88}},
            {"albania", new int[]{72}},
            {"algeria", new int[]{70,71,83,84}},
            {"andorra", new int[]{71}},
            {"angola", new int[]{104,105,113,114}},
            {"argentina", new int[]{34,35,37,38,40}},
            {"armenia", new int[]{73,74}},
            {"australia", new int[]{111,112,117,118,119,120,123,124,125,126}},
            {"austria", new int[]{58,59}},
            {"azerbaijan", new int[]{73,74}},
            {"azores", new int[]{70}},
            {"bahrain", new int[]{87}},
            {"bangladesh", new int[]{89,90}},
            {"belarus", new int[]{59,60}},
            {"belgium", new int[]{58}},
            {"benin", new int[]{95}},
            {"bermuda", new int[]{21}},
            {"bhutan", new int[]{89,90}},
            {"bolivia", new int[]{31,34,35}},
            {"bosnia and herzegovina", new int[]{59,72}},
            {"botswana", new int[]{114}},
            {"brazil", new int[]{28,29,31,32,33,34,35,36,38}},
            {"brunei darussalam", new int[]{102}},
            {"bulgaria", new int[]{72}},
            {"burkina faso", new int[]{83,94,95}},
            {"burundi", new int[]{105,106}},
            {"cabo verde", new int[]{82,93}},
            {"cambodia", new int[]{101,102}},
            {"cameroon", new int[]{95,96}},
            {"canada", new int[]{3,4,5,6,7,8,11,12,13,14,15,16,20,21,129}},
            {"central african republic", new int[]{95,96}},
            {"central america", new int[]{23,24,25,26,27,28,29}},
            {"chad", new int[]{84,85,95,96}},
            {"chile", new int[]{34,37,39,40}},
            {"china", new int[]{63,64,65,66,75,76,77,78,79,89,90,91,92}},
            {"christmas island", new int[]{110}},
            {"clipperton", new int[]{57,58,70,71}},
            {"cocos islands", new int[]{109}},
            {"colombia", new int[]{27,28,30,31}},
            {"comoros", new int[]{106}},
            {"congo", new int[]{95,96,104,105}},
            {"congo drc", new int[]{96,97,104,105,106}},
            {"croatia", new int[]{58,59,71,72}},
            {"cyprus", new int[]{73}},
            {"czech republic", new int[]{58,59}},
            {"denmark", new int[]{58,59}},
            {"djibouti", new int[]{97}},
            {"ecuador", new int[]{26,27,30}},
            {"egypt", new int[]{72,73,85,86}},
            {"equatorial guinea", new int[]{95}},
            {"eritrea", new int[]{86,97}},
            {"estonia", new int[]{59}},
            {"ethiopia", new int[]{97,98}},
            {"falkland islands", new int[]{40,41}},
            {"faroe islands", new int[]{42}},
            {"fiji", new int[]{135,136}},
            {"finland", new int[]{44,45,59}},
            {"gabon", new int[]{95,104}},
            {"gambia", new int[]{93,94}},
            {"georgia", new int[]{73,74}},
            {"germany", new int[]{58,59}},
            {"ghana", new int[]{94,95}},
            {"gibraltar", new int[]{70}},
            {"glorioso islands", new int[]{107}},
            {"greece", new int[]{72}},
            {"greenland", new int[]{130,131,132}},
            {"guernsey", new int[]{57}},
            {"guinea", new int[]{93,94}},
            {"guinea-bissau", new int[]{93,94}},
            {"hungary", new int[]{59}},
            {"iceland", new int[]{132,42}},
            {"india", new int[]{75,76,88,89,90,99,100,101}},
            {"indonesia", new int[]{101,102,103,109,110,111,112}},
            {"iran", new int[]{73,74,75,87,88}},
            {"iraq", new int[]{73,74,86,87}},
            {"ireland", new int[]{57}},
            {"isle of man", new int[]{57}},
            {"israel", new int[]{73,86}},
            {"italy", new int[]{58,59,71,72}},
            {"ivory coast", new int[]{94}},
            {"jan mayen", new int[]{42,44}},
            {"japan", new int[]{67,79,80,92}},
            {"jersey", new int[]{57}},
            {"jordan", new int[]{73,86}},
            {"juan de nova island", new int[]{115}},
            {"kazakhstan", new int[]{61,62,63,74,75,76}},
            {"kenya", new int[]{97,106}},
            {"kuwait", new int[]{74,87}},
            {"kyrgyzstan", new int[]{75,76}},
            {"laos", new int[]{90,91,102}},
            {"latvia", new int[]{59}},
            {"lebanon", new int[]{73}},
            {"lesotho", new int[]{114,121}},
            {"liberia", new int[]{94}},
            {"libya", new int[]{71,72,84,85}},
            {"liechtenstein", new int[]{58}},
            {"lithuania", new int[]{59}},
            {"luxembourg", new int[]{58}},
            {"madagascar", new int[]{107,115,116}},
            {"malawi", new int[]{106,115}},
            {"malaysia", new int[]{101,102}},
            {"maldives", new int[]{99}},
            {"mali", new int[]{83,84,94,95}},
            {"malta", new int[]{71}},
            {"mauritania", new int[]{82,83,94}},
            {"mauritius", new int[]{116}},
            {"mayotte", new int[]{107}},
            {"mexico", new int[]{18,19,22,23,24,26}},
            {"moldova", new int[]{59,60}},
            {"monaco", new int[]{71}},
            {"mongolia", new int[]{63,64,65,77,78}},
            {"montenegro", new int[]{72}},
            {"morocco", new int[]{70,82,83}},
            {"mozambique", new int[]{106,115}},
            {"myanmar", new int[]{90,101}},
            {"namibia", new int[]{113,114}},
            {"nepal", new int[]{76,89}},
            {"netherlands", new int[]{58}},
            {"new caledonia", new int[]{120,135}},
            {"new zealand", new int[]{127,128}},
            {"niger", new int[]{84,85,95}},
            {"nigeria", new int[]{95}},
            {"norfolk island", new int[]{135}},
            {"north korea", new int[]{79}},
            {"norway", new int[]{43,44,45,58}},
            {"oman", new int[]{87}},
            {"pacific islands", new int[]{136}},
            {"pakistan", new int[]{75,76,88}},
            {"palau", new int[]{103}},
            {"palestinian territory", new int[]{73}},
            {"papua new guinea", new int[]{112,134}},
            {"paraguay", new int[]{34,35}},
            {"peru", new int[]{30,31,34}},
            {"philippines", new int[]{91,92,102,103}},
            {"poland", new int[]{58,59}},
            {"qatar", new int[]{87}},
            {"reunion", new int[]{116}},
            {"romania", new int[]{59,72}},
            {"russian federation", new int[]{1,44,45,46,47,48,49,50,51,52,53,54,55,56,59,60,61,62,63,64,65,66,67,68,69,73,74,79,80}},
            {"rwanda", new int[]{105,106}},
            {"saint pierre and miquelon", new int[]{16}},
            {"san marino", new int[]{71}},
            {"sao tome and principe", new int[]{95}},
            {"saudi arabia", new int[]{73,86,87}},
            {"senegal", new int[]{82,83,93,94}},
            {"serbia", new int[]{59,72}},
            {"seychelles", new int[]{107}},
            {"sierra leone", new int[]{94}},
            {"singapore", new int[]{101}},
            {"slovakia", new int[]{59}},
            {"slovenia", new int[]{58,59}},
            {"solomon islands", new int[]{134}},
            {"somalia", new int[]{97,98,106}},
            {"south africa", new int[]{114,115,121,122}},
            {"south korea", new int[]{79}},
            {"south sudan", new int[]{96,97}},
            {"spain", new int[]{70,71,82,83}},
            {"sri lanka", new int[]{100}},
            {"sudan", new int[]{85,86,96,97}},
            {"swaziland", new int[]{115}},
            {"sweden", new int[]{43,44,58,59}},
            {"switzerland", new int[]{58}},
            {"syria", new int[]{73}},
            {"tajikistan", new int[]{75,76}},
            {"tanzania", new int[]{105,106}},
            {"thailand", new int[]{90,91,101,102}},
            {"the former yugoslav republic of macedonia", new int[]{72}},
            {"timor-leste", new int[]{111}},
            {"togo", new int[]{94,95}},
            {"tunisia", new int[]{71}},
            {"turkey", new int[]{72,73}},
            {"turkmenistan", new int[]{74,75}},
            {"uganda", new int[]{96,97,105,106}},
            {"ukraine", new int[]{59,60,73}},
            {"united arab emirates", new int[]{87}},
            {"united kingdom", new int[]{42,57,58}},
            {"uruguay", new int[]{38}},
            {"usa-alaska", new int[]{1,2,3,9,10,11,69,129,}},
            {"usa-conus", new int[]{11,12,13,14,15,17,18,19,20,21,23,24}},
            {"usa-hawaii", new int[]{133}},
            {"uzbekistan", new int[]{61,74,75}},
            {"vanuatu", new int[]{135}},
            {"vatican city", new int[]{71}},
            {"vietnam", new int[]{90,91,101,102}},
            {"yemen", new int[]{86,87,97,98}},
            {"zambia", new int[]{105,106,114,115}},
            {"zimbabwe", new int[]{114,115}},
        };

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

            if (Name.ToLower() == "all")
                return Region.All;

            return Region.Unknown;
        }

        public static bool IsDistrictInCountry(int districtIndex, string countryName)
        {
            bool isInCountry = false;

            int[] districts = CountryDistricts[countryName.ToLower()];
            if (districts != null)
            {
                isInCountry = Array.IndexOf(districts, districtIndex) != -1;
            }

            return isInCountry;
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
                    if ((districtIndex >= 1 && districtIndex <= 4) ||
                        (districtIndex >= 9 && districtIndex <= 11))
                        isInRegion = true;
                    break;
                case Region.USNorthEast:
                    if ((districtIndex >= 13 && districtIndex <= 16) ||
                        (districtIndex >= 19 && districtIndex <= 21))
                        isInRegion = true;
                    break;
                case Region.USNorthCentral:
                    if ((districtIndex >= 12 && districtIndex <= 14) ||
                        (districtIndex >= 18 && districtIndex <= 20))
                        isInRegion = true;
                    break;
                case Region.USSouthEast:
                    if ((districtIndex >= 20 && districtIndex <= 21) ||
                        (districtIndex >= 24 && districtIndex <= 25))
                        isInRegion = true;
                    break;
                case Region.USMissouriBasin:
                    if ((districtIndex >= 13 && districtIndex <= 13) ||
                        (districtIndex >= 19 && districtIndex <= 19))
                        isInRegion = true;
                    break;
                case Region.USWestGulf:
                    if ((districtIndex >= 18 && districtIndex <= 19) ||
                        (districtIndex >= 22 && districtIndex <= 23))
                        isInRegion = true;
                    break;
                case Region.USNorthWest:
                    if ((districtIndex >= 11 && districtIndex <= 12) ||
                        (districtIndex >= 17 && districtIndex <= 18))
                        isInRegion = true;
                    break;
                case Region.USSouthWest:
                    if ((districtIndex >= 17 && districtIndex <= 18) ||
                        (districtIndex >= 22 && districtIndex <= 22))
                        isInRegion = true;
                    break;
                case Region.USACONUS:
                    if ((districtIndex >= 11 && districtIndex <= 25))
                        isInRegion = true;
                    break;
                case Region.USAAlaska:
                    if ((districtIndex >= 1 && districtIndex <= 3) ||
                        (districtIndex >= 10 && districtIndex <= 10))
                        isInRegion = true;
                    break;
                case Region.USAHawaii:
                    if ((districtIndex >= 133 && districtIndex <= 133))
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
