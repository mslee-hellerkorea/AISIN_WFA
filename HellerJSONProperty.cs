using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AISIN_WFA
{
    //class HellerJSONProperty
    //{
    //}

    public class barcodeRecipe
    {
        public string barcode;
        public string recipe;
        public string beltWidth;
        public string beltSpeed;
    }

    public class globalFunctions
    {
        public static void SerialToFile(string jsonString, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(jsonString);
                }
            }
        }

        public static string DeserialDataFromFile(string filePath)
        {
            string json = string.Empty;
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }

        public static void initializeBarcodeRecipeTable()
        {
            if (globalParameter.barcodeRecipeList == null)
                globalParameter.barcodeRecipeList = new List<barcodeRecipe>();
            else
                globalParameter.barcodeRecipeList.Clear();

            if (File.Exists(globalParameter.barcodeRecipePath))
            {
                string JsonData = globalFunctions.DeserialDataFromFile(globalParameter.barcodeRecipePath);
                List<barcodeRecipe> list_bar_recipe = JsonConvert.DeserializeObject<List<barcodeRecipe>>(JsonData);
                foreach (barcodeRecipe bar_rec in list_bar_recipe)
                {
                    globalParameter.barcodeRecipeList.Add(bar_rec);
                }
            }
        }

        public static void changeXMLFile(Dictionary<string, string> modifedDict)
        {
            XmlDocument doc = new XmlDocument();
            // get file path
            string strFileName = Application.StartupPath + "\\AISIN_WFA.exe.config";
            doc.Load(strFileName);
            // find all the settings
            XmlNodeList nodes = doc.GetElementsByTagName("setting");
            XmlAttribute att;
            for (int i = 0; i < nodes.Count; i++)
            {
                // get current key
                att = nodes[i].Attributes["name"];
                // check if it is the attr we want
                if (att != null && modifedDict.Keys.Contains(att.Value))
                {
                    // modified the value
                    nodes[i].ChildNodes[0].InnerText = modifedDict[att.Value];
                    modifedDict.Remove(att.Value);
                    if (modifedDict.Count == 0)
                        break;
                }
            }
            // save configuration
            doc.Save(strFileName);
        }
    }

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

        public static bool UpstreamEnableLane1 { get; set; } = true;
        public static bool UpstreamEnableLane2 { get; set; } = true;

        public static eRails Lane1Rail { get; set; } = eRails.Rail1;
        public static eRails Lane2Rail { get; set; } = eRails.Rail2;

        public enum ePLCType
        {
            None,
            OMRON,
            Mitsubishi
        }
        public enum eRails
        {
            Rail1,
            Rail2,
            Rail3,
            Rail4,
            Disable
        }

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
