using AISIN_WFA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AISIN_WFA.Models
{
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
}
