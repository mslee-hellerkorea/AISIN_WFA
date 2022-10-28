using AISIN_WFA.Models;
using AISIN_WFA.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AISIN_WFA.GUI
{
    public partial class BarcodeSetting : Form
    {
        public String configurationKey = "HKEY_LOCAL_MACHINE\\Software\\Heller Industries\\HC2\\BarcodeReader";
        private static BarcodeSetting _instance = null;
        public BarcodeSetting()
        {
            InitializeComponent();
            HLog.log(HLog.eLog.EVENT, $"Open Barocde Setting Load config by setted values");
            chk_autoChange.Checked = globalParameter.autoChangeRecipeWidthSpeed;
            chk_holdSmema.Checked = globalParameter.holdSmemaUntilBarcode;
        }

        private void chk_holdSmema_CheckedChanged(object sender, EventArgs e)
        {
            globalParameter.holdSmemaUntilBarcode = chk_holdSmema.Checked;

            // v1.20 MSL
            UseConfigFile.SetBoolConfigurationSetting("HoldSmemaBarcode", globalParameter.holdSmemaUntilBarcode);
            //Dictionary<string, string> ModifiedDict = new Dictionary<string, string>();
            //ModifiedDict.Add("HoldSmemaBarcode", chk_holdSmema.Checked.ToString());
            //globalFunctions.changeXMLFile(ModifiedDict);
            HLog.log(HLog.eLog.EVENT, $"Change globalParameter.holdSmemaUntilBarcode to { globalParameter.holdSmemaUntilBarcode}");
        }

        public static BarcodeSetting AddFormInstance()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new BarcodeSetting();
            }
            return _instance;
        }

        private void chk_autoChange_CheckedChanged(object sender, EventArgs e)
        {
            globalParameter.autoChangeRecipeWidthSpeed = chk_autoChange.Checked;

            // v1.20 MSL
            UseConfigFile.SetBoolConfigurationSetting("AutoBarcodeRecipe", globalParameter.autoChangeRecipeWidthSpeed);
            //Dictionary<string, string> ModifiedDict = new Dictionary<string, string>();
            //ModifiedDict.Add("AutoBarcodeRecipe", chk_autoChange.Checked.ToString());
            //globalFunctions.changeXMLFile(ModifiedDict);
            HLog.log(HLog.eLog.EVENT, $"Change globalParameter.autoChangeRecipeWidthSpeed to { globalParameter.autoChangeRecipeWidthSpeed}");
        }
    }
}
