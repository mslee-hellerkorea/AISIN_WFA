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
    public partial class PlcSetupWindow : Form
    {
        public PlcSetupWindow()
        {
            InitializeComponent();
            Initalization();
        }

        private void Initalization()
        {
            try
            {
                foreach (globalParameter.ePLCType plcType in Enum.GetValues(typeof(globalParameter.ePLCType)))
                {
                    cb_PlcType.Items.Add(plcType.ToString());
                }

                cb_PlcType.SelectedItem = globalParameter.PLCType;
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, ex.Message);
            }
        }
    }
}
