using System;
using System.Collections.Generic;
using System.Text;

namespace GMUFFCommon
{
    public static class Constants
    {
        public const string Satellite_SuomiNPP_Name = "Suomi-NPP";
        public const string Satellite_NOAA20_Name = "NOAA-20";
        public const string Satellite_GOES16_Name = "GOES-16";
    }

    public enum Region
    {
        Unknown = -1,
        All = 0,
        NorthAmerica = 1,
        SouthAmerica = 2,
        Europe = 3,
        Asia = 4,
        Africa = 5,
        Australia = 6,
        USAlaska = 7,
        USNorthEast = 8,
        USNorthCentral = 9,
        USSouthEast = 10,
        USMissouriBasin = 11,
        USWestGulf = 12,
        USNorthWest = 13,
        USSouthWest= 14,

        USACONUS = 15,
        USAAlaska = 16,
        USAHawaii = 17,
        Afghanistan = 18,
        Albania = 19,
        Algeria = 20,
        AmericanSamoa = 21,
        Andorra = 22
    }
}
