//-----------------------------------------------------------------------------
// PlcSetupWindow.cs -- PlcSetupWindow
//
// Author: MS Lee
// E-mail: mslee@hellerindustries.co.kr
// Tel:
//
// Edit History:
//
// 08-Nov-22  01.01.02.01   MSL Added setup config for address of PLC Alive check.(Releaes 01.01.03.00)
// 11-Nov-211 01.01.02.02   MSL Added Lane Type configuration for TCO oven. (Releaes 01.01.03.00)
//-----------------------------------------------------------------------------
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
    public partial class PlcSetupWindow : Form
    {
        private bool IsModify = false;
        public PlcSetupWindow()
        {
            InitializeComponent();
            Initalization();
        }

        private void Initalization()
        {
            try
            {
                tableLayoutPanel1.Enabled = false;
                cb_PlcType.DropDownStyle = ComboBoxStyle.DropDownList;
                cb_Lane1Enable.DropDownStyle = ComboBoxStyle.DropDownList;
                cb_Lane2Enable.DropDownStyle = ComboBoxStyle.DropDownList;
                cb_Lane1Rail.DropDownStyle = ComboBoxStyle.DropDownList;
                cb_Lane2Rail.DropDownStyle = ComboBoxStyle.DropDownList;
                // 11-Nov-211 01.01.02.02   MSL Added Lane Type configuration for TCO oven.
                cb_LaneType.DropDownStyle = ComboBoxStyle.DropDownList;

                tabctrl_PlcType.Appearance = TabAppearance.FlatButtons;
                tabctrl_PlcType.ItemSize = new Size(0, 1);
                tabctrl_PlcType.SizeMode = TabSizeMode.Fixed;

                // PLC Type
                foreach (globalParameter.ePLCType plcType in Enum.GetValues(typeof(globalParameter.ePLCType)))
                {
                    cb_PlcType.Items.Add(plcType.ToString());
                }
                cb_PlcType.Text = globalParameter.PLCType.ToString();
                plcSetupVisible(cb_PlcType.Text);

                // Upstream Lane Enable
                foreach (globalParameter.eUpstreamUse upEnable in Enum.GetValues(typeof(globalParameter.eUpstreamUse)))
                {
                    cb_Lane1Enable.Items.Add(upEnable.ToString());
                    cb_Lane2Enable.Items.Add(upEnable.ToString());
                }
                cb_Lane1Enable.Text = globalParameter.UpstreamEnableLane1.ToString();
                cb_Lane2Enable.Text = globalParameter.UpstreamEnableLane2.ToString();

                // Lane Rail Setup
                for (int rail = 0; rail < globalParameter.strRails.Length; rail++)
                {
                    cb_Lane1Rail.Items.Add(globalParameter.strRails[rail].ToString());
                    cb_Lane2Rail.Items.Add(globalParameter.strRails[rail].ToString());
                }
                cb_Lane1Rail.Text = globalParameter.Lane1Rail.ToString();
                cb_Lane2Rail.Text = globalParameter.Lane2Rail.ToString();

                tb_UpstreamStation.Text = globalParameter.UpstreamMxPlcStation.ToString();
                tb_DownStation.Text = globalParameter.DownstreamMxPlcStation.ToString();

                tb_Lane1BcrAddr.Text = globalParameter.AddrMxBarcodeLane1.ToString();
                tb_Lane2BcrAddr.Text = globalParameter.AddrMxBarcodeLane2.ToString();

                tb_Lane1BaAddr.Text = globalParameter.AddrMxBoardAvailableLane1.ToString();
                tb_Lane2BaAddr.Text = globalParameter.AddrMxBoardAvailableLane2.ToString();

                tb_Lane1RailWidthAddr.Text = globalParameter.AddrMxRailWidthLane1.ToString();
                tb_Lane2RailWidthAddr.Text = globalParameter.AddrMxRailWidthLane2.ToString();

                tb_PlcAliveCheckAddr.Text = globalParameter.AddrMitsubishiPlcAliveCheck.ToString();

                tbUpstreamPLCTag.Text = globalParameter.UpstreamPLCTag;
                tbDownstreamPLCTag.Text = globalParameter.DownstreamPLCTag;
                tbTagIP.Text = globalParameter.TagIP;

                cb_Lane1Enable.Text = globalParameter.UpstreamEnableLane1.ToString();
                cb_Lane2Enable.Text = globalParameter.UpstreamEnableLane2.ToString();

                tbLogFilesFolder.Text = globalParameter.LogFilePath;
                tb_EventLogPath.Text = globalParameter.debugLogFolder;
                ckb_RailLog.Checked = globalParameter.RailLogging ? true : false;

                // 11-Nov-211 01.01.02.02   MSL Added Lane Type configuration for TCO oven.
                for (int lane = 0; lane < globalParameter.strLaneType.Length; lane++)
                {
                    cb_LaneType.Items.Add(globalParameter.strLaneType[lane].ToString());
                }
                cb_LaneType.Text = globalParameter.LaneType;

            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, "PlcSetupWindow - Initalization " + ex.Message);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                
                switch (btn.Tag)
                {
                    case "Save":
                        SaveConfig();
                        HLog.log(HLog.eLog.EVENT, $"User Click {btn.Tag} Button");
                        break;
                    case "Modify":
                        if (!IsModify)
                        {
                            IsModify = true;
                            tableLayoutPanel1.Enabled = true;
                            btn_Modify.Text = "Lock";
                            HLog.log(HLog.eLog.EVENT, $"User Click Modify Button");
                        }
                        else
                        {
                            IsModify = false;
                            tableLayoutPanel1.Enabled = false;
                            btn_Modify.Text = "Modify";
                            HLog.log(HLog.eLog.EVENT, $"User Click Lock Button");
                        }
                        break;
                    case "Close":
                        this.Close();
                        HLog.log(HLog.eLog.EVENT, $"User Click PLC Setup Close Button");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, "PlcSetupWindow - button_Click " + ex.Message);
            }
        }

        private void SaveConfig()
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Save", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    // Barcode function
                    UseConfigFile.SetBoolConfigurationSetting("HoldSmemaBarcode", globalParameter.holdSmemaUntilBarcode);
                    UseConfigFile.SetBoolConfigurationSetting("AutoBarcodeRecipe", globalParameter.autoChangeRecipeWidthSpeed);

                    // PLC Type
                    UseConfigFile.SetStringConfigurationSetting("PLCType", cb_PlcType.SelectedItem.ToString());

                    // Mx Upstream Station
                    int upStation = 0;
                    bool bUpstation = Int32.TryParse(tb_UpstreamStation.Text, out upStation);
                    if (bUpstation)
                    {
                        UseConfigFile.SetIntConfigurationSetting("UpstreamMxPlcStation", upStation);
                    }
                    else
                    {
                        tb_UpstreamStation.Text = globalParameter.UpstreamMxPlcStation.ToString();
                        MessageBox.Show("Incorrect Upstation station number, please retry enter station number.");
                    }

                    // Mx Downstream station
                    int downStation = 0;
                    bool bDownstation = Int32.TryParse(tb_DownStation.Text, out downStation);
                    if (bDownstation)
                    {
                        UseConfigFile.SetIntConfigurationSetting("DownstreamMxPlcStation", downStation);
                    }
                    else
                    {
                        tb_DownStation.Text = globalParameter.DownstreamMxPlcStation.ToString();
                        MessageBox.Show("Incorrect Downtream station number, please retry enter station number.");
                    }

                    // Mx Address
                    globalParameter.AddrMxBarcodeLane1 = tb_Lane1BcrAddr.Text;
                    globalParameter.AddrMxBarcodeLane2 = tb_Lane2BcrAddr.Text;
                    globalParameter.AddrMxBoardAvailableLane1 = tb_Lane1BaAddr.Text;
                    globalParameter.AddrMxBoardAvailableLane2 = tb_Lane2BaAddr.Text;
                    globalParameter.AddrMxRailWidthLane1 = tb_Lane1RailWidthAddr.Text;
                    globalParameter.AddrMxRailWidthLane2 = tb_Lane2RailWidthAddr.Text;
                    globalParameter.AddrMitsubishiPlcAliveCheck = tb_PlcAliveCheckAddr.Text; // 08-Nov-22  01.01.02.01   MSL Added setup config for address of PLC Alive check
                    UseConfigFile.SetStringConfigurationSetting("AddrMxBarcodeLane1", globalParameter.AddrMxBarcodeLane1);
                    UseConfigFile.SetStringConfigurationSetting("AddrMxBarcodeLane2", globalParameter.AddrMxBarcodeLane2);
                    UseConfigFile.SetStringConfigurationSetting("AddrMxBoardAvailableLane1", globalParameter.AddrMxBoardAvailableLane1);
                    UseConfigFile.SetStringConfigurationSetting("AddrMxBoardAvailableLane2", globalParameter.AddrMxBoardAvailableLane2);
                    UseConfigFile.SetStringConfigurationSetting("AddrMxRailWidthLane1", globalParameter.AddrMxRailWidthLane1);
                    UseConfigFile.SetStringConfigurationSetting("AddrMxRailWidthLane2", globalParameter.AddrMxRailWidthLane2);
                    UseConfigFile.SetStringConfigurationSetting("AddrMitsubishiPlcAliveCheck", globalParameter.AddrMitsubishiPlcAliveCheck); // 08-Nov-22  01.01.02.01   MSL Added setup config for address of PLC Alive check

                    // Omron Address
                    globalParameter.UpstreamPLCTag = tbUpstreamPLCTag.Text;
                    globalParameter.DownstreamPLCTag = tbDownstreamPLCTag.Text;
                    globalParameter.TagIP = tbTagIP.Text;

                    // Lane Enable
                    globalParameter.UpstreamEnableLane1 = cb_Lane1Enable.Text == "Enable" ? globalParameter.eUpstreamUse.Enable : globalParameter.eUpstreamUse.Disable;
                    globalParameter.UpstreamEnableLane2 = cb_Lane2Enable.Text == "Enable" ? globalParameter.eUpstreamUse.Enable : globalParameter.eUpstreamUse.Disable;
                    UseConfigFile.SetStringConfigurationSetting("UpstreamEnableLane1", globalParameter.UpstreamEnableLane1.ToString());
                    UseConfigFile.SetStringConfigurationSetting("UpstreamEnableLane2", globalParameter.UpstreamEnableLane2.ToString());

                    // Lane Rail Setup
                    globalParameter.Lane1Rail = cb_Lane1Rail.SelectedItem.ToString();
                    UseConfigFile.SetStringConfigurationSetting("Lane1Rail", globalParameter.Lane1Rail);

                    globalParameter.Lane2Rail = cb_Lane2Rail.SelectedItem.ToString();
                    UseConfigFile.SetStringConfigurationSetting("Lane2Rail", globalParameter.Lane2Rail);

                    // Omron PLC Setup
                    UseConfigFile.SetStringConfigurationSetting("UpstreamPLCTag", tbUpstreamPLCTag.Text);
                    UseConfigFile.SetStringConfigurationSetting("DownstreamPLCTag", tbDownstreamPLCTag.Text);
                    UseConfigFile.SetStringConfigurationSetting("TagIP", tbTagIP.Text);

                    // General Setup
                    UseConfigFile.SetStringConfigurationSetting("LogFilePath", tbLogFilesFolder.Text);
                    UseConfigFile.SetStringConfigurationSetting("debugLogFolder", tb_EventLogPath.Text);
                    globalParameter.RailLogging = ckb_RailLog.Checked ? true : false;
                    UseConfigFile.SetBoolConfigurationSetting("RailLogging", globalParameter.RailLogging);

                    // 11-Nov-22  01.01.01.02   MSL MSL Added Lane Type configuration for TCO oven.
                    UseConfigFile.SetStringConfigurationSetting("LaneType", cb_LaneType.Text);

                    MessageBox.Show("Saved config file. Please restart this application to apply changes.");
                }
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, "PlcSetupWindow - SaveConfig " + ex.Message);
            }
        }

        private void cb_PlcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string type = cb_PlcType.SelectedItem.ToString();
                plcSetupVisible(type);
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, "PlcSetupWindow - cb_PlcType_SelectedIndexChanged " + ex.Message);
            }
        }

        private void plcSetupVisible(string type)
        {
            try
            {
                // Panel1 = Mitsubishi;
                // Panel2 = OMRON;
                switch (type)
                {
                    case "Mitsubishi":
                        tabctrl_PlcType.SelectedIndex = 0;
                        break;
                    case "OMRON":
                        tabctrl_PlcType.SelectedIndex = 1;
                        break;
                    case "None":
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, "PlcSetupWindow - cb_PlcType_SelectedIndexChanged " + ex.Message);
            }
        }

        private globalParameter.eRails laneRailStringConverter(string value)
        {
            globalParameter.eRails result = globalParameter.eRails.Disable;
            try
            {
                switch (value)
                {
                    case "Rail1": result = globalParameter.eRails.Rail1; break;
                    case "Rail2": result = globalParameter.eRails.Rail2; break;
                    case "Rail3": result = globalParameter.eRails.Rail3; break;
                    case "Rail4": result = globalParameter.eRails.Rail4; break;
                    case "Disable": result = globalParameter.eRails.Disable; break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, "PlcSetupWindow - laneRailStringConverter " + ex.Message);
            }
            return result;
        }

    }
}
