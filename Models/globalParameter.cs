using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISIN_WFA.Models
{
    public class globalParameter
    {
        public static List<barcodeRecipe> barcodeRecipeList { get; set; } = null;  // default as null
        public static string barcodeRecipePath { get; set; } = @"C:\Heller Industries\AISIN Line Comm\Bin\barcodeTable.json";
        public static string debugLogFolder { get; set; } = @"C:\Heller Industries\AISIN Line Comm\Logs";
        public static string debugFileName { get; set; } = "DebugLog.txt";
        public static bool holdSmemaUntilBarcode { get; set; } = true;
        public static bool autoChangeRecipeWidthSpeed { get; set; } = true;

#if true // 25-Sep-22 MSL v1.19

        public static string LogFilePath { get; set; } = @"C:\Heller Industries\AISIN Line Comm\Logs";
        public static string BoardTransitLogPath { get; set; } = @"C:\Heller Industries\AISIN Line Comm\BarcodeReader\Logs";
        public static int DefaultRail { get; set; } = 1;
        public static int DownstreamPLCPeriod { get; set; } = 1000;
        public static int UpstreamPLCPeriod { get; set; } = 1000;
        public static int UpdatePeriod { get; set; } = 2000;
        public static string TagIP { get; set; } = "192.168.241.9";
        public static string DownstreamPLCTag { get; set; } = "RE1outSTCV1";
        public static string UpstreamPLCTag { get; set; } = "RE1inAMCV1";
        public static bool RailLogging { get; set; } = false;
        public static ePLCType PLCType { get; set; } = ePLCType.None;
        public static int UpstreamMxPlcStation { get; set; } = 6;
        public static int DownstreamMxPlcStation { get; set; } = 6;

        public static string AddrMxBarcodeLane1 { get; set; } = "D0";
        public static string AddrMxBarcodeLane2 { get; set; } = "D100";

        public static string AddrMxBoardAvailableLane1 { get; set; } = "D21";
        public static string AddrMxBoardAvailableLane2 { get; set; } = "D121";

        public static string AddrMxRailWidthLane1 { get; set; } = "D270";
        public static string AddrMxRailWidthLane2 { get; set; } = "D370";

        public static eUpstreamUse UpstreamEnableLane1 { get; set; } = globalParameter.eUpstreamUse.Enable;
        public static eUpstreamUse UpstreamEnableLane2 { get; set; } = globalParameter.eUpstreamUse.Enable;

        public static string Lane1Rail { get; set; } = "Rail1";
        public static string Lane2Rail { get; set; } = "Rail2";

        public enum ePLCType
        {
            None,
            OMRON,
            Mitsubishi
        }

        public enum eUpstreamUse
        {
            Enable,
            Disable
        }

        public enum eRails
        {
            Rail1,
            Rail2,
            Rail3,
            Rail4,
            Disable
        }

        public static string[] strRails = new string[] { "Rail1", "Rail2", "Rail3", "Rail4", "-" };

        public enum eLane
        {
            Lane1,
            Lane2
        }

        public enum eEndian
        {
            BigEndian,
            LittleEndian
        }

        public enum NavigatorLocation
        {
            Main,
            Setup,
        }
#else
        public static string PLCType { get; set; } = "None";
#endif
    }
}
