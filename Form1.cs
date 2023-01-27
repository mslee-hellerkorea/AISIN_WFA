//-----------------------------------------------------------------------------
// Form1.cs -- dialog and plc handling
//
// Author: Frank Natoli
// E-mail: franknatoli@ptd.net
// Tel:    973-222-8159
//
// Edit History:
//
// 04-Aug-14  00.01  FJN  Created
// 05-Sep-14  00.02  FJN  Disable form close requiring Quit to exit
// 08-Sep-14  00.02  FJN  Start with PLC communications enabled, disable on PLC communications failure
// 08-Sep-14  00.02  FJN  Attempt to connect to Barcode Reader application on startup
// 08-Sep-14  00.02  FJN  Ask are you sure on quit button
// 21-Nov-14  00.03  FJN  Test for null variant returned by GetRailWidth then return -1
// 21-Mar-16  01.03  FJN  Create radio buttons for each of four rails, send selected rail data to PLC
// 03-May-16  01.04  FJN  Try-catch log file write
// 03-May-16  01.04  FJN  Implement railLogging Boolean
// 03-May-16  01.04  FJN  Save DefaultRail and RailLogging to registy
// 26-Oct-20  01.05  DCH  Move all the features from STD barcode to ASIN communication line software
// 12-Nov-20  01.06  DCH  When lane2 board entry, should also hold smema.
// 17-Nov-20  01.07  DCH  Trim barcode after receiving barcode from upstream PLC program.
// 19-Nov-20  01.08  DCH  Display warning message when barcode is not in the barcode mapping table.
// 20-Nov-20  01.09  DCH  Add new barcode text box for lane2
// 26-Nov-20  01.10  DCH  Will have another retry if first communication failed
// 26-Nov-20  01.11  DCH  Modify PLC read logic for lane2
// 27-Nov-20  01.12  DCH  Fix a problem that when "Hold smema until barcode scan" is unchecked, not all lane smema are released.
// 28-Nov-20  01.13  DCH  Skip barcode recipe check if not scanned on the specific lane.
// 01-Dec-20  01.14  DCH  Fix a bug that all the acquired barcode goes to lane1.
// 08-Dec-20  01.15  DCH  Always send rail widht of lane1 and lane2 to downstream PLC data memory even they are not checked
// 09-Dec-20  01.16  DCH  Correct the mistake that will always send lane1 and lane2 rail width to the downstream.
// 29-Dec-20  01.17  DCH  Add debug information for downstream information print.
// 06-Jun-22  01.18  DCH  Add the capibility to support "Mitsubshi" PLC.
// 23-Sep-22  01-19  MSL  Debug the capibility to support "Mitsubishi" PLC.
// 12-Oct-22  01.01.00.00   MSL  1) Add Trace Log.
//                               2) Remove MX Component Label feature.
// 07-Nov-22  01.01.01.00   MSL Improvement Mitsubishi PLC thread.
// 11-Nov-22  01.01.01.02   MSL MSL Added Lane Type configuration for TCO oven.(Release 01.01.03.00)
// 11-Jan-23  01.01.06.00   MSL Bug fix to software shutdown(crashes) when change rail width.
// 11-Jan-23  01.01.08.00   MSL Bug fix to belt speed index
// 27-Jan-23  01.01.09.00   MSL Discard decimal point during gets rail setpoint.
//                              Discard decimal point during gets belt setpoint.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AISIN_WFA.Utility;
using AISIN_WFA.Models;
using AISIN_WFA.GUI;

namespace AISIN_WFA
{
    public delegate void OcxUIupdate();
    public partial class Form1 : Form
    {
        #region [Members]
        // Revision
        private string revision = "v1.1.9.0";
        private OcxWrappercs ocx;
        private MxWrapper UpstreamMxPlc;
        private MxWrapper DownstreamMxPlc;
        private OMRON.Compolet.CIP.CJ2Compolet compolet;

        private bool RunFlag = true;

        // [Mutex]
        private Mutex plcMutex;
        private Mutex logMutex;

        // [Thread]
        private Thread upstreamThread;
        private Thread downstreamThread;

        // PLC Data and State
        private bool plcCommEnable;
        private int[] plcInputData;
        private int[] plcOutputData;
        private byte[] barcodeData;

        private int[] MxPlcBarcodeDataLane1;
        private int[] MxPlcBarcodeDataLane2;

        private int MxPlcBaSignalLane1 = 0;
        private int MxPlcBaSignalLane2 = 0;

        private bool BASignal;
        private bool BASignalLane1;
        private bool BASignalLane2;
        private bool LastUpstreamConnectedState = false;
        private bool LastDownstreamConnectedState = false;
        private bool closeOK;

        private byte[] lane1BarcodeData;
        private byte[] lane2BarcodeData;

        private byte[] lane1BarcodeByte;
        private byte[] lane2BarcodeByte;

        private string barcodeFromUpLane1;
        private string barcodeFromUpLane2;

        int defaultRail;
        //private bool railLogging;
        bool barcodeRecipeEmptyDisplayed;
        List<float> currentBeltSpeed = null;
        List<float> currentBeltWidth = null;

        const int BARCODE_MAX = 24;
        const int PLC_MEMORY_MAX = 125;
        const int TCP_MSG_MAX = 32;
        const int RAIL_MAX = 4;
        const int BELT_MAX = 3;

        const int RAIL_WIDTH_ARRAY_NDX = 20;
        const int RAIL_WIDTH_ARRAY_NDX_2 = 120;
        const int BA_SIGNAL_ARRAY_NDX_LANE1 = 21;
        const int BA_SIGNAL_ARRAY_NDX_LANE2 = 121;

        private bool bWaitForOvenEmptytoLoadRecipe = false;
        private Thread waitForOvenEmptyToChangeWidthorSpeed = null;
        private string nextRecipeToLoad = null;
        private int beltCount = 0;

        #endregion

        public Form1()
        {
            HLog.StartupPath = Application.StartupPath + @"\Log\";

            InitializeComponent();
            Text = string.Format("AISIN Line Communication PLC Interface ({0})", revision);
            // add form closing
            this.FormClosing += new FormClosingEventHandler(MainForm_Closing);

            HLog.log(HLog.eLog.EVENT, $"Loading Main form... [Version: {revision}]");
            LoadConfigurationSettings();
            InitializeMembers();

            if (InitializeControl()) 
            {
                // if Hc2 ocx is disconnect, not allow operate any actions.
                InitializePLC();
            }
            else
            {
                HLog.log(HLog.eLog.EVENT, $"Not able to connect with HC2, please start after oven software execute.");
                MessageBox.Show("Not able to connect with HC2, please start after oven software execute.", "HC2 Connection fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #region [Initialize]

        private void InitializeMembers()
        {
            try
            {
                // initialize buffers
                plcInputData = new Int32[PLC_MEMORY_MAX];
                Array.Clear(plcInputData, 0, PLC_MEMORY_MAX);

                MxPlcBarcodeDataLane1 = new Int32[BARCODE_MAX];
                Array.Clear(MxPlcBarcodeDataLane1, 0, BARCODE_MAX);

                MxPlcBarcodeDataLane2 = new Int32[BARCODE_MAX];
                Array.Clear(MxPlcBarcodeDataLane2, 0, BARCODE_MAX);

                plcOutputData = new Int32[PLC_MEMORY_MAX];
                Array.Clear(plcOutputData, 0, PLC_MEMORY_MAX);

                barcodeData = new byte[BARCODE_MAX];
                Array.Clear(barcodeData, 0, BARCODE_MAX);

                lane1BarcodeData = new byte[TCP_MSG_MAX];
                Array.Clear(lane1BarcodeData, 0, TCP_MSG_MAX);

                lane2BarcodeData = new byte[TCP_MSG_MAX];
                Array.Clear(lane2BarcodeData, 0, TCP_MSG_MAX);

                lane1BarcodeByte = new byte[BARCODE_MAX];
                Array.Clear(lane1BarcodeByte, 0, BARCODE_MAX);

                lane2BarcodeByte = new byte[BARCODE_MAX];
                Array.Clear(lane2BarcodeByte, 0, BARCODE_MAX);

                logMutex = new Mutex();
                globalFunctions.initializeBarcodeRecipeTable();

                // initialize booleans
                BASignal = false;

                closeOK = false;
                barcodeRecipeEmptyDisplayed = false;

                // initialize mutex
                plcMutex = new Mutex();

                HLog.log(HLog.eLog.EVENT, "InitializeMembers()");
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"InitializeMembers - {ex.Message}");
                MessageBox.Show(ex.Message, "EXCEPTION");
            }
        }

        private bool InitializeControl()
        {
            bool isConnect = false;
            try
            {
                ocx = new OcxWrappercs();
                ChannelInfo.GetChannelInfo();
                ocx.UpdateUiEventHandler += UpdatingValuesOcx;
                ocx.UpdateHc2ConnectEventHandler += UpdatingHc2State;
                isConnect = ocx.InitWrapper();

                HLog.log(HLog.eLog.EVENT, $"InitializeControl()[Create OCX Wrapper]");

                // hold smema for all lanes
                if (globalParameter.holdSmemaUntilBarcode)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        SmemaLaneHold(i, 1);
                        HLog.log(HLog.eLog.EVENT, $"InitializeControl()[SmemaLaneHold: {(globalParameter.eLane)i}: hold]");
                    }
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        SmemaLaneHold(i, 0);
                        HLog.log(HLog.eLog.EVENT, $"InitializeControl()[SmemaLaneHold: {(globalParameter.eLane)i}: Release]");
                    }
                }
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"InitializeControl - {ex.Message}");
                MessageBox.Show(ex.Message, "EXCEPTION");
            }
            return isConnect;
        }

        private void InitializePLC()
        {
            try
            {
                // Create instance
                switch (globalParameter.PLCType)
                {
                    case globalParameter.ePLCType.None:
                        {
                            MessageBox.Show("Please setup to PLC Type");
                            HLog.log(HLog.eLog.EVENT, $"InitializePLC()[PLC Type: None]");
                        }
                        break;
                    case globalParameter.ePLCType.OMRON:
                        {
                            HLog.log(HLog.eLog.EVENT, $"InitializePLC()[PLC Type: OMRON]");
                            this.components = new System.ComponentModel.Container();
                            compolet = new OMRON.Compolet.CIP.CJ2Compolet(this.components);
                            compolet.Active = true;
                            compolet.HeartBeatTimer = 0;
                            compolet.LocalPort = 2;
                            compolet.PeerAddress = null;
                            compolet.ReceiveTimeLimit = ((long)(750));
                            compolet.RoutePath = "";
                            compolet.UseRoutePath = false;
                            compolet.OnHeartBeatTimer += new System.EventHandler(this.compolet_OnHeartBeatTimer);
                            HLog.log(HLog.eLog.EVENT, $"InitializePLC()[Created: OMRON-compolet]");
                            plcCommEnable = true;
                            //start downstream PLC thread
                            
                            UpDownstreamThread();
                        }
                        break;
                    case globalParameter.ePLCType.Mitsubishi:
                        {
                            //[07-Nov-22  01.01.01.00   MSL Improvement Mitsubishi PLC thread.]
                            Task.Factory.StartNew(() =>
                            {
                                HLog.log(HLog.eLog.EVENT, $"InitializePLC()[PLC Type: Mitsubishi]");
                                int UpStation = globalParameter.UpstreamMxPlcStation;
                                int DownStation = globalParameter.DownstreamMxPlcStation;

                                UpstreamMxPlc = new MxWrapper(UpStation);
                                DownstreamMxPlc = new MxWrapper(DownStation);

                                UpstreamMxPlc.UpdateMxConnectionStateEventHandler += MxPlc_UpdateMxConnectionStateEventHandler;
                                DownstreamMxPlc.UpdateMxConnectionStateEventHandler += MxPlc_UpdateMxConnectionStateEventHandler;
                                HLog.log(HLog.eLog.EVENT, $"InitializePLC()[Created: Mitsubishi-Mx Components]");
                                UpDownstreamThread();
                            });
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"InitializePLC - {ex.Message}");
                MessageBox.Show(ex.Message, "EXCEPTION");
            }
        }

        private void MxPlc_UpdateMxConnectionStateEventHandler(int station)
        {
            try
            {
                UpdatingUpDownPlcState();
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"UpstreamMxPlc_UpdateMxConnectionStateEventHandler - {ex.Message}");
            }
        }

        protected void LoadConfigurationSettings()
        {
            try
            {
                HLog.log(HLog.eLog.EVENT, $"LoadConfigurationSettings()[Start read Configuraiton]");
                // --------------------------------------------
                // Get from app.config
                // --------------------------------------------
                // BoardTransitLogPath
                globalParameter.BoardTransitLogPath = UseConfigFile.GetStringConfigurationSetting("BoardTransitLogPath", globalParameter.BoardTransitLogPath);

                // DefaultRail
                globalParameter.DefaultRail = UseConfigFile.GetIntConfigurationSetting("DefaultRail", globalParameter.DefaultRail);

                // DefaultRail
                globalParameter.DownstreamPLCPeriod = UseConfigFile.GetIntConfigurationSetting("DownstreamPLCPeriod", globalParameter.DownstreamPLCPeriod);

                // UpstreamPLCPeriod 
                globalParameter.UpstreamPLCPeriod = UseConfigFile.GetIntConfigurationSetting("UpstreamPLCPeriod", globalParameter.UpstreamPLCPeriod);

                // UpdatePeriod  
                globalParameter.UpdatePeriod = UseConfigFile.GetIntConfigurationSetting("UpdatePeriod", globalParameter.UpdatePeriod);

                // TagIP  
                globalParameter.TagIP = UseConfigFile.GetStringConfigurationSetting("TagIP", globalParameter.TagIP);
                UpdatingValues("tbTagIP", globalParameter.TagIP);

                // DownstreamPLCTag  
                globalParameter.DownstreamPLCTag = UseConfigFile.GetStringConfigurationSetting("DownstreamPLCTag", globalParameter.DownstreamPLCTag);
                UpdatingValues("tbDownstreamPLCTag", globalParameter.DownstreamPLCTag);

                // UpstreamPLCTag   
                globalParameter.UpstreamPLCTag = UseConfigFile.GetStringConfigurationSetting("UpstreamPLCTag", globalParameter.UpstreamPLCTag);
                UpdatingValues("tbUpstreamPLCTag", globalParameter.UpstreamPLCTag);

                // RailLogging    
                globalParameter.RailLogging = UseConfigFile.GetBoolConfigurationSetting("RailLogging", globalParameter.RailLogging);

                // AutoBarcodeRecipe
                globalParameter.autoChangeRecipeWidthSpeed = UseConfigFile.GetBoolConfigurationSetting("AutoBarcodeRecipe", globalParameter.autoChangeRecipeWidthSpeed);

                // HoldSmemaBarcode
                globalParameter.holdSmemaUntilBarcode = UseConfigFile.GetBoolConfigurationSetting("HoldSmemaBarcode", globalParameter.holdSmemaUntilBarcode);

                // LogFilePath
                globalParameter.LogFilePath = UseConfigFile.GetStringConfigurationSetting("LogFilePath", globalParameter.LogFilePath);
                HLog.LogTracePath = globalParameter.LogFilePath;
                globalParameter.debugLogFolder = UseConfigFile.GetStringConfigurationSetting("debugLogFolder", globalParameter.debugLogFolder);

                // PLCType
                string plcType = globalParameter.PLCType.ToString();
                plcType = UseConfigFile.GetStringConfigurationSetting("PLCType", plcType);

                switch (plcType)
                {
                    case "OMRON":
                        globalParameter.PLCType = globalParameter.ePLCType.OMRON;
                        tlp_downstream.Visible = false;
                        tlp_Upstream.Visible = false;
                        break;
                    case "Mitsubishi":
                        globalParameter.PLCType = globalParameter.ePLCType.Mitsubishi;
                        tlp_downstream.Visible = true;
                        tlp_Upstream.Visible = true;
                        btnStartComm.Visible = false;
                        break;
                    default:
                        globalParameter.PLCType = globalParameter.ePLCType.None;
                        tlp_downstream.Visible = false;
                        tlp_Upstream.Visible = false;
                        break;
                }

                // PLC Station number
                globalParameter.UpstreamMxPlcStation = UseConfigFile.GetIntConfigurationSetting("PLCStation", globalParameter.UpstreamMxPlcStation);

                globalParameter.UpstreamMxPlcStation = UseConfigFile.GetIntConfigurationSetting("UpstreamMxPlcStation", globalParameter.UpstreamMxPlcStation);
                globalParameter.DownstreamMxPlcStation = UseConfigFile.GetIntConfigurationSetting("DownstreamMxPlcStation", globalParameter.DownstreamMxPlcStation);

                // Mx Address
                globalParameter.AddrMxBarcodeLane1 = UseConfigFile.GetStringConfigurationSetting("AddrMxBarcodeLane1", globalParameter.AddrMxBarcodeLane1);
                globalParameter.AddrMxBarcodeLane2 = UseConfigFile.GetStringConfigurationSetting("AddrMxBarcodeLane2", globalParameter.AddrMxBarcodeLane2);
                globalParameter.AddrMxBoardAvailableLane1 = UseConfigFile.GetStringConfigurationSetting("AddrMxBoardAvailableLane1", globalParameter.AddrMxBoardAvailableLane1);
                globalParameter.AddrMxBoardAvailableLane2 = UseConfigFile.GetStringConfigurationSetting("AddrMxBoardAvailableLane2", globalParameter.AddrMxBoardAvailableLane2);
                globalParameter.AddrMxRailWidthLane1 = UseConfigFile.GetStringConfigurationSetting("AddrMxRailWidthLane1", globalParameter.AddrMxRailWidthLane1);
                globalParameter.AddrMxRailWidthLane2 = UseConfigFile.GetStringConfigurationSetting("AddrMxRailWidthLane2", globalParameter.AddrMxRailWidthLane2);

                globalParameter.AddrMitsubishiPlcAliveCheck = UseConfigFile.GetStringConfigurationSetting("AddrMitsubishiPlcAliveCheck", globalParameter.AddrMitsubishiPlcAliveCheck);

                string lane1enable = globalParameter.UpstreamEnableLane1.ToString();
                string lane2enable = globalParameter.UpstreamEnableLane2.ToString();
                lane1enable = UseConfigFile.GetStringConfigurationSetting("UpstreamEnableLane1", lane1enable);
                lane2enable = UseConfigFile.GetStringConfigurationSetting("UpstreamEnableLane2", lane2enable);
                globalParameter.UpstreamEnableLane1 = lane1enable == "Enable" ? globalParameter.eUpstreamUse.Enable : globalParameter.eUpstreamUse.Disable;
                globalParameter.UpstreamEnableLane2 = lane2enable == "Enable" ? globalParameter.eUpstreamUse.Enable : globalParameter.eUpstreamUse.Disable;

                // Lane1EHC
                globalParameter.Lane1Rail = UseConfigFile.GetStringConfigurationSetting("Lane1Rail", globalParameter.Lane1Rail);

                // Lane2EHC
                globalParameter.Lane2Rail = UseConfigFile.GetStringConfigurationSetting("Lane2Rail", globalParameter.Lane2Rail);
                HLog.log(HLog.eLog.EVENT, $"LoadConfigurationSettings()[Completed read Configuraiton]");

                // 11-Nov-22  01.01.01.02   MSL MSL Added Lane Type configuration for TCO oven.
                globalParameter.LaneType = UseConfigFile.GetStringConfigurationSetting("LaneType", globalParameter.LaneType);

            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"LoadConfigurationSettings - {ex.Message}");
                MessageBox.Show(ex.Message, "EXCEPTION");
            }
        }

        //---------------------------------------------------------------------
        // Method compolet_OnHeartBeatTimer
        //
        // Copied from Omron CJ2Compolet sample
        //---------------------------------------------------------------------
        private void compolet_OnHeartBeatTimer(object sender, EventArgs e)
        {
            // Do work something
        }

        #endregion

        #region [Threads]

        //---------------------------------------------------------------------
        // Method UpstreamThread
        //---------------------------------------------------------------------
        private void UpstreamThread()
        {
            HLog.log(HLog.eLog.EVENT, $"UpstreamThread()[Start UpstreamThread]");
            //while (true)
            while (RunFlag)
            {
                switch (globalParameter.PLCType)
                {
                    case globalParameter.ePLCType.None:
                        break;
                    case globalParameter.ePLCType.OMRON:
                        {
                            //-----------------------------------------------
                            // if plc communications enabled
                            if (plcCommEnable)
                            {
                                try
                                {
                                    // lock access to PLC
                                    plcMutex.WaitOne();

                                    try
                                    {
                                        plcInputData = (Int32[])this.compolet.ReadVariable(globalParameter.UpstreamPLCTag);
                                    }
                                    catch (Exception e)
                                    {
                                        //this.compolet.PeerAddress = (String)Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\Heller Industries\\HC2\\BarcodeReader", "TagIP", "192.168.241.2");
                                        this.compolet.PeerAddress = globalParameter.TagIP;

                                        // give him another retry
                                        if (plcCommEnable)
                                        {
                                            plcCommEnable = false;
                                            continue;
                                        }
                                        //btnStartComm.Text = "Start Comm";
                                        UpdatingValues("btnStartComm", "Start Comm");

                                        MessageBox.Show("Omron CX-Compolet ReadVariable " + globalParameter.UpstreamPLCTag + " exception " + e.Message);
                                    }

                                    // unlock access to PLC
                                    plcMutex.ReleaseMutex();
                                }
                                catch (Exception ex)
                                {
                                    HLog.log(HLog.eLog.EXCEPTION, $"UpstreamThread exception during plcMutex - {ex.Message}");
                                }

                                try
                                {
                                    // Always update current read barcode value
                                    UpdateBarcodeString();

                                    // test BA signal transition from OFF to ON
                                    if (BASignal == false &&
                                        (plcInputData[BA_SIGNAL_ARRAY_NDX_LANE1] != 0 || plcInputData[BA_SIGNAL_ARRAY_NDX_LANE2] != 0))    // daniel modified,  remove barcode socket check.
                                    {
                                        //// copy barcode to tcp message buffer
                                        //Array.Clear(tcpMsgData, 0, TCP_MSG_MAX);
                                        LogWrite("Clear barcode array");
                                        Array.Clear(lane1BarcodeData, 0, TCP_MSG_MAX);
                                        Array.Clear(lane2BarcodeData, 0, TCP_MSG_MAX);

                                        // Line1 Barcode
                                        for (int ndx = 0; ndx < BARCODE_MAX; ndx++)
                                        {
                                            byte barcode1Digit;

                                            // Big endian
                                            if ((ndx & 1) == 1)
                                                barcode1Digit = (byte)(plcInputData[ndx / 2] & 0xFF);
                                            else
                                                barcode1Digit = (byte)(plcInputData[ndx / 2] >> 8 & 0xFF);
                                            if (barcode1Digit == 0)
                                                break;
                                            lane1BarcodeData[ndx] = barcode1Digit;
                                        }

                                        int startIndex = 100;
                                        byte barcode2Digit;

                                        for (int ndx = startIndex; ndx < startIndex + BARCODE_MAX; ndx++)
                                        {
                                            // Big endian
                                            if ((ndx & 1) == 1)
                                                barcode2Digit = (byte)(plcInputData[startIndex + (ndx - startIndex) / 2] & 0xFF);
                                            else
                                                barcode2Digit = (byte)(plcInputData[startIndex + (ndx - startIndex) / 2] >> 8 & 0xFF);
                                            if (barcode2Digit == 0)
                                                break;
                                            lane2BarcodeData[ndx - startIndex] = barcode2Digit;
                                        }

                                        // deal with this barcode
                                        if (lane1BarcodeData[0] != 0)
                                        {
                                            barcodeFromUpLane1 = System.Text.Encoding.Default.GetString(lane1BarcodeData);
                                            barcodeFromUpLane1 = Encoding.ASCII.GetString(lane1BarcodeData, 0, lane1BarcodeData.Length);
                                            int offset = barcodeFromUpLane1.IndexOf("\r\n");
                                            if (offset < 0)
                                                offset = barcodeFromUpLane1.IndexOf('\r');
                                            if (offset < 0)
                                                offset = barcodeFromUpLane1.IndexOf('\n');
                                            if (offset > 0)
                                                barcodeFromUpLane1 = barcodeFromUpLane1.Substring(0, offset);

                                            // 1.07 revision,  trim barcode before using it for changing recipe or speed or length.
                                            barcodeFromUpLane1 = barcodeFromUpLane1.Trim().Replace(" ", "");
                                            LogWrite("Acquired barcode for lane1: " + barcodeFromUpLane1);
                                            UpdatingValues("tbBarcodeLane1", barcodeFromUpLane1);
                                            ocx.Lane1Barcode = barcodeFromUpLane1.Replace("\0", "");
                                        }

                                        if (lane2BarcodeData[0] != 0)
                                        {
                                            barcodeFromUpLane2 = System.Text.Encoding.Default.GetString(lane2BarcodeData);
                                            barcodeFromUpLane2 = Encoding.ASCII.GetString(lane2BarcodeData, 0, lane2BarcodeData.Length);
                                            int offset = barcodeFromUpLane2.IndexOf("\r\n");
                                            if (offset < 0)
                                                offset = barcodeFromUpLane2.IndexOf('\r');
                                            if (offset < 0)
                                                offset = barcodeFromUpLane2.IndexOf('\n');
                                            if (offset > 0)
                                                barcodeFromUpLane2 = barcodeFromUpLane2.Substring(0, offset);

                                            barcodeFromUpLane2 = barcodeFromUpLane2.Trim().Replace(" ", "");
                                            LogWrite("Acquired barcode for lane2: " + barcodeFromUpLane2);
                                            //tbBarcodeLane2.Text = barcodeFromUpLane2;
                                            UpdatingValues("tbBarcodeLane2", barcodeFromUpLane2);
                                            ocx.Lane2Barcode = barcodeFromUpLane2.Replace("\0", "");
                                        }

                                        // do nothing if hold smema until barcode scan is not enabled.
                                        if (!globalParameter.holdSmemaUntilBarcode)
                                            continue;

                                        if (!globalParameter.autoChangeRecipeWidthSpeed)
                                        {
                                            // change recipe,  width, speed not enabled, release smema when barcode is scanned.
                                            if (lane1BarcodeData[0] != 0)
                                            {
                                                LogWrite("Auto load recipe not enabled, release smema for l.");
                                                SmemaLaneHold(0, 0);
                                            }
                                            if (lane2BarcodeData[0] != 0)
                                            {
                                                LogWrite("Auto load recipe not enabled, release smema for 2.");
                                                SmemaLaneHold(1, 0);
                                            }
                                            continue;
                                        }

                                        if (lane1BarcodeData[0] != 0)
                                        {
                                            LogWrite("Start to check barcode recipe for lane1");
                                            CheckBarcodeRecipe(0, barcodeFromUpLane1);
                                        }

                                        if (lane2BarcodeData[0] != 0)
                                        {
                                            LogWrite("Start to check barcode recipe for lane2");
                                            CheckBarcodeRecipe(1, barcodeFromUpLane2);
                                        }

                                        // flag BA signal true
                                        BASignal = true;
                                    }

                                    // test BA signal transition from ON to OFF
                                    if (BASignal &&
                                        plcInputData[BA_SIGNAL_ARRAY_NDX_LANE1] == 0
                                        && plcInputData[BA_SIGNAL_ARRAY_NDX_LANE2] == 0)
                                    {
                                        BASignal = false;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    LogWrite(ex.Message);
                                }
                            }
                        }
                        break;
                    case globalParameter.ePLCType.Mitsubishi:
                        {
                            //-----------------------------------------------
                            // if plc communications enabled
                            if (UpstreamMxPlc.BConnected)
                            {
                                try
                                {
                                    // lock access to PLC
                                    plcMutex.WaitOne();

                                    try
                                    {
                                        // Read PLC data
                                        // MxPlcBarcodeDataLane1
                                        // MxPlcBarcodeDataLane2
                                        // MxPlcBaSignalLane1
                                        // MxPlcBaSignalLane2

                                        ReadBarcodeDataFromPLC();
                                        ReadBoardAvailableFromPLC();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Mitsubishi MX Component ReadVariable " + globalParameter.UpstreamPLCTag + " exception " + ex.Message);
                                    }

                                    // unlock access to PLC
                                    plcMutex.ReleaseMutex();
                                }
                                catch (Exception ex)
                                {
                                    HLog.log(HLog.eLog.EXCEPTION, $"UpstreamThread exception during plcMutex - {ex.Message}");
                                }

                                try
                                {
                                    // Always update current read barcode value
                                    UpdateBarcodeMxString();

                                    foreach (globalParameter.eLane lane in Enum.GetValues(typeof(globalParameter.eLane)))
                                    {
                                        UpstreamBarcodeOperation(lane);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    LogWrite(ex.Message);
                                    HLog.log(HLog.eLog.EXCEPTION, $"UpstreamThread exception during UpdateBarcodeMxString - {ex.Message}");
                                }
                            }

                            if (!UpstreamMxPlc.IsOnline())
                            {
                                if (UpstreamMxPlc.IsRetryFail)
                                {
                                    HLog.log(HLog.eLog.ERROR, "Mitsubishi Upstream MX Component disconnected");
                                    MessageBox.Show("Mitsubishi Upstream MX Component disconnected, Please check the PLC connection state");
                                    if (UpstreamMxPlc != null)
                                    {
                                        UpstreamMxPlc = null;
                                    }
                                    return;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
                // snooze
                Thread.Sleep(globalParameter.UpstreamPLCPeriod);
            }
            HLog.log(HLog.eLog.EVENT, $"UpstreamThread()[Stop UpstreamThread]");
        }

        //---------------------------------------------------------------------
        // Method DownstreamThread
        //---------------------------------------------------------------------
        private void DownstreamThread()
        {
            HLog.log(HLog.eLog.EVENT, $"DownstreamThread()[Start DownstreamThread]");
            //while (true)
            while (RunFlag)
            {

                switch (globalParameter.PLCType)
                {
                    case globalParameter.ePLCType.None:
                        break;
                    case globalParameter.ePLCType.OMRON:
                        {

                            // if plc communications enabled
                            if (plcCommEnable)
                            {

                                float lane1Width = 0;
                                float lane2Width = 0;

                                switch (globalParameter.Lane1Rail)
                                {
                                    case "Rail1":
                                        {
                                            lane1Width = ocx.Oven.RailWidthSP[0];
                                        }
                                        break;
                                    case "Rail2":
                                        {
                                            lane1Width = ocx.Oven.RailWidthSP[1];
                                        }
                                        break;
                                    case "Rail3":
                                        {
                                            lane1Width = ocx.Oven.RailWidthSP[2];
                                        }
                                        break;
                                    case "Rail4":
                                        {
                                            lane1Width = ocx.Oven.RailWidthSP[3];
                                        }
                                        break;
                                    case "-":
                                        lane1Width = 0;
                                        break;
                                    default:
                                        break;
                                }

                                switch (globalParameter.Lane2Rail)
                                {
                                    case "Rail1":
                                        {
                                            lane2Width = ocx.Oven.RailWidthSP[0];
                                        }
                                        break;
                                    case "Rail2":
                                        {
                                            lane2Width = ocx.Oven.RailWidthSP[1];
                                        }
                                        break;
                                    case "Rail3":
                                        {
                                            lane2Width = ocx.Oven.RailWidthSP[2];
                                        }
                                        break;
                                    case "Rail4":
                                        {
                                            lane2Width = ocx.Oven.RailWidthSP[3];
                                        }
                                        break;
                                    case "-":
                                        lane2Width = 0;
                                        break;
                                    default:
                                        break;
                                }

                                if (globalParameter.RailLogging)
                                {
                                    DateTime now = DateTime.Now;
                                    string path = "C:\\Heller Industries\\AISIN Line Comm\\Logs\\Rail_" +
                                        now.Year.ToString("D4") + now.Month.ToString("D2") + now.Day.ToString("D2") + ".log";

                                    try
                                    {
                                        StreamWriter logFile = new StreamWriter(path, true);

                                        logFile.WriteLine(lane1Width.ToString("F1"));
                                        logFile.WriteLine(lane2Width.ToString("F1"));

                                        logFile.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        HLog.log(HLog.eLog.EXCEPTION, $"DownstreamThread - RailLogging error {ex.Message}");
                                    }
                                }

                                if (lane1Width > 0)
                                {
                                    FillPLCOutPut(lane1Width, RAIL_WIDTH_ARRAY_NDX);
                                    LogWrite("Fill rail1 width: " + lane1Width + " to index: " + RAIL_WIDTH_ARRAY_NDX);
                                }

                                if (lane2Width > 0)
                                {
                                    FillPLCOutPut(lane2Width, RAIL_WIDTH_ARRAY_NDX_2);
                                    LogWrite("Fill rail2 width: " + lane2Width + " to index: " + RAIL_WIDTH_ARRAY_NDX_2);
                                }

                                try
                                {
                                    // lock access to PLC
                                    plcMutex.WaitOne();

                                    // write PLC
                                    try
                                    {
                                        this.compolet.WriteVariable(globalParameter.DownstreamPLCTag, plcOutputData);
                                    }
                                    catch (Exception e)
                                    {
                                        btnStartComm.Text = "Start Comm";
                                        plcCommEnable = false;
                                        MessageBox.Show("Omron CX-Compolet WriteVariable " + globalParameter.DownstreamPLCTag + " exception " + e.Message + " rail1Width=" + lane1Width.ToString());
                                    }

                                    // unlock access to PLC
                                    plcMutex.ReleaseMutex();
                                }
                                catch (Exception ex)
                                {
                                    HLog.log(HLog.eLog.EXCEPTION, $"DownstreamThread - during plcMutex {ex.Message}");
                                }
                            }
                        }
                        break;
                    case globalParameter.ePLCType.Mitsubishi:
                        {
                            if (DownstreamMxPlc.BConnected)
                            {
                                foreach (globalParameter.eLane lane in Enum.GetValues(typeof(globalParameter.eLane)))
                                {
                                    WriteRailWidthToPlc(lane);
                                }
                            }

                            if (!DownstreamMxPlc.IsOnline())
                            {
                                if (DownstreamMxPlc.IsRetryFail)
                                {
                                    HLog.log(HLog.eLog.ERROR, "Mitsubishi Downstream MX Component disconnected");
                                    MessageBox.Show("Mitsubishi Downstream MX Component disconnected, Please check the PLC connection state");
                                    if (DownstreamMxPlc != null)
                                    {
                                        DownstreamMxPlc = null;
                                    }
                                    return;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }

                // snooze
                Thread.Sleep(globalParameter.DownstreamPLCPeriod);
            }

            HLog.log(HLog.eLog.EVENT, $"DownstreamThread()[Stop DownstreamThread]");
        }

        private void UpDownstreamThread()
        {
            HLog.log(HLog.eLog.EVENT, $"InitializePLC()[Start UpDownstreamThread]");
            downstreamThread = new Thread(new ThreadStart(DownstreamThread));
            downstreamThread.Start();

            // start upstream PLC thread
            upstreamThread = new Thread(new ThreadStart(UpstreamThread));
            upstreamThread.Start();
        }

        #endregion

        #region [Local Function]

        private void UpstreamBarcodeOperation(globalParameter.eLane lane)
        {
            try
            {
                // In case of TCO
                // Front PC = Lane1 operation. (D0   ~ D11  / D21 )
                // Rear  PC = Lane1 operation. (D100 ~ D111 / D121)

                // In case of Dual lane
                // Front Lane = Lane1 operation. (D0   ~ D11  / D21 )
                // Rear  Lane = Lane2 operation. (D100 ~ D111 / D121)

                switch (lane)
                {
                    case globalParameter.eLane.Lane1:
                        {
                            if (globalParameter.UpstreamEnableLane1 == globalParameter.eUpstreamUse.Disable) break;

                            // test BA signal transition from OFF to ON
                            if (BASignalLane1 == false && (MxPlcBaSignalLane1 != 0))    // daniel modified,  remove barcode socket check.
                            {
                                HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [BA: {BASignalLane1}] [PLC BA: {MxPlcBaSignalLane1}]");
                                // copy barcode to tcp message buffer
                                // Array.Clear(tcpMsgData, 0, TCP_MSG_MAX);
                                LogWrite("Clear barcode array");
                                Array.Clear(lane1BarcodeData, 0, TCP_MSG_MAX);
                                HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [Array.Clear - lane1BarcodeData]");

                                // Line1 Barcode
                                lane1BarcodeData = BarcodeDataIntToByte(globalParameter.eEndian.LittleEndian, MxPlcBarcodeDataLane1); // Mitsubishi use Litte endian
                                HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [BarcodeDataIntToByte[0]: {lane1BarcodeData[0]}]");

                                // deal with this barcode
                                if (lane1BarcodeData[0] != 0)
                                {
                                    barcodeFromUpLane1 = BarcodeByteToString(lane1BarcodeData);
                                    LogWrite("Acquired barcode for lane1: " + barcodeFromUpLane1);
                                    HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [BarcodeByteToString: {barcodeFromUpLane1}]");

                                    UpdatingValues("tbBarcodeLane1", barcodeFromUpLane1);
                                    ocx.Lane1Barcode = barcodeFromUpLane1.Replace("\0", "");
                                    HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [Trace Log Barcode Number {ocx.Lane1Barcode}]");
                                }

                                // do nothing if hold smema until barcode scan is not enabled.
                                if (!globalParameter.holdSmemaUntilBarcode)
                                {
                                    HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [holdSmemaUntilBarcode disabled]");
                                    return;
                                }

                                if (!globalParameter.autoChangeRecipeWidthSpeed)
                                {
                                    // change recipe,  width, speed not enabled, release smema when barcode is scanned.
                                    if (lane1BarcodeData[0] != 0)
                                    {
                                        LogWrite("Auto load recipe not enabled, release smema for line1.");
                                        SmemaLaneHold(0, 0);
                                        HLog.log(HLog.eLog.EVENT, $"{(globalParameter.eLane)lane} UpstreamBarcodeOperation [autoChangeRecipeWidthSpeed disabled] [SMEMA - Release]");
                                    }
                                    return;
                                }

                                if (lane1BarcodeData[0] != 0)
                                {
                                    LogWrite("Start to check barcode recipe for lane1");
                                    CheckBarcodeRecipe(0, barcodeFromUpLane1);
                                    HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [CheckBarcodeRecipe] [Barcode: {barcodeFromUpLane1}]");
                                }

                                // flag BA signal true
                                BASignalLane1 = true;
                            }

                            // test BA signal transition from ON to OFF
                            if (BASignalLane1 && MxPlcBaSignalLane1 == 0)
                            {
                                BASignalLane1 = false;
                            }
                        }
                        break;
                    case globalParameter.eLane.Lane2:
                        {
                            if (globalParameter.UpstreamEnableLane2 == globalParameter.eUpstreamUse.Disable) break;

                            // test BA signal transition from OFF to ON
                            if (BASignalLane2 == false && (MxPlcBaSignalLane2 != 0))    // daniel modified,  remove barcode socket check.
                            {
                                HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [BA: {BASignalLane2}] [PLC BA: {MxPlcBaSignalLane2}]");
                                // copy barcode to tcp message buffer
                                // Array.Clear(tcpMsgData, 0, TCP_MSG_MAX);
                                LogWrite("Clear barcode array");
                                Array.Clear(lane2BarcodeData, 0, TCP_MSG_MAX);
                                HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [Array.Clear - lane2BarcodeData]");

                                // Line2 Barcode
                                lane2BarcodeData = BarcodeDataIntToByte(globalParameter.eEndian.LittleEndian, MxPlcBarcodeDataLane2); // Mitsubishi use Litte endian
                                HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [BarcodeDataIntToByte[0]: {lane2BarcodeData[0]}]");

                                // deal with this barcode
                                if (lane2BarcodeData[0] != 0)
                                {
                                    barcodeFromUpLane2 = BarcodeByteToString(lane2BarcodeData);
                                    LogWrite("Acquired barcode for lane2: " + barcodeFromUpLane2);
                                    HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [BarcodeByteToString: {barcodeFromUpLane2}]");

                                    UpdatingValues("tbBarcodeLane2", barcodeFromUpLane2);
                                    ocx.Lane2Barcode = barcodeFromUpLane2.Replace("\0", "");
                                    HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [Trace Log Barcode Number: {ocx.Lane2Barcode}]");
                                }

                                // do nothing if hold smema until barcode scan is not enabled.
                                if (!globalParameter.holdSmemaUntilBarcode)
                                {
                                    HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [holdSmemaUntilBarcode disabled]");
                                    return;
                                }

                                if (!globalParameter.autoChangeRecipeWidthSpeed)
                                {
                                    // change recipe,  width, speed not enabled, release smema when barcode is scanned.
                                    if (lane2BarcodeData[0] != 0)
                                    {
                                        LogWrite("Auto load recipe not enabled, release smema for line2.");
                                        SmemaLaneHold(1, 0);
                                        HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [autoChangeRecipeWidthSpeed disabled] [SMEMA - Release]");
                                    }
                                    return;
                                }

                                if (lane2BarcodeData[0] != 0)
                                {
                                    LogWrite("Start to check barcode recipe for lane2");
                                    CheckBarcodeRecipe(1, barcodeFromUpLane2);
                                    HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] UpstreamBarcodeOperation [Barcode: {barcodeFromUpLane2}]");
                                }

                                // flag BA signal true
                                BASignalLane2 = true;
                            }

                            // test BA signal transition from ON to OFF
                            if (BASignalLane2 && MxPlcBaSignalLane2 == 0)
                            {
                                BASignalLane2 = false;
                            }
                        }
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"UpstreamBarcodeOperation - {ex.Message}");
            }
        }

        private byte[] BarcodeDataIntToByte(globalParameter.eEndian endian, int[] data)
        {
            byte[] laneBarcodeData = new byte[TCP_MSG_MAX];
            Array.Clear(laneBarcodeData, 0, TCP_MSG_MAX); ;
            int first = 1;
            int second = 0;
            byte barcodeDigit = 0;
            try
            {
                switch (endian)
                {
                    case globalParameter.eEndian.BigEndian:
                        {
                            first = 1;
                            second = 0;
                        }
                        break;
                    case globalParameter.eEndian.LittleEndian:
                        {
                            first = 0;
                            second = 1;
                        }
                        break;
                    default:
                        break;
                }

                for (int ndx = 0; ndx < BARCODE_MAX; ndx++)
                {
                    if ((ndx & 1) == first)
                        barcodeDigit = (byte)(data[ndx / 2] & 0xFF);
                    else
                        barcodeDigit = (byte)(data[ndx / 2] >> 8 & 0xFF);
                    if (barcodeDigit == second)
                        break;
                    laneBarcodeData[ndx] = barcodeDigit;
                }


            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"BarcodeDataConvert - {ex.Message}");
            }

            return laneBarcodeData;
        }

        private string BarcodeByteToString(byte[] btBcr)
        {
            string strBcr = string.Empty;
            try
            {
                strBcr = System.Text.Encoding.Default.GetString(btBcr);
                strBcr = Encoding.ASCII.GetString(btBcr, 0, btBcr.Length);
                int offset = strBcr.IndexOf("\r\n");
                if (offset < 0)
                    offset = strBcr.IndexOf('\r');
                if (offset < 0)
                    offset = strBcr.IndexOf('\n');
                if (offset > 0)
                    strBcr = strBcr.Substring(0, offset);
                strBcr = strBcr.Trim().Replace(" ", "");
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"BarcodeByteToString - {ex.Message}");
            }

            return strBcr;
        }

        private void ReadBarcodeDataFromPLC()
        {

            try
            {
                // In case of TCO
                // Front PC = Lane1 operation. (D0   ~ D11  / D21 )
                // Rear  PC = Lane1 operation. (D100 ~ D111 / D121)

                // In case of Dual lane
                // Front Lane = Lane1 operation. (D0   ~ D11  / D21 )
                // Rear  Lane = Lane2 operation. (D100 ~ D111 / D121)

                int[] bcrData1 = new int[BARCODE_MAX];
                int[] bcrData2 = new int[BARCODE_MAX];
                string addr = string.Empty;
                int length = BARCODE_MAX;

                foreach (globalParameter.eLane lane in Enum.GetValues(typeof(globalParameter.eLane)))
                {
                    switch (lane)
                    {
                        case globalParameter.eLane.Lane1:
                            {
                                if (globalParameter.UpstreamEnableLane1 == globalParameter.eUpstreamUse.Disable) break;
                                addr = globalParameter.AddrMxBarcodeLane1;
                                int iReturnCode = UpstreamMxPlc.ReadDeviceBlockInt(addr, length, out bcrData1[0]);
                                if (iReturnCode != 0)
                                {
                                    throw new Exception("Return code not 0, code: " + iReturnCode);
                                }
                                MxPlcBarcodeDataLane1 = bcrData1;
                            }
                            break;
                        case globalParameter.eLane.Lane2:
                            {
                                if (globalParameter.UpstreamEnableLane2 == globalParameter.eUpstreamUse.Disable) break;
                                addr = globalParameter.AddrMxBarcodeLane2;
                                int iReturnCode = UpstreamMxPlc.ReadDeviceBlockInt(addr, length, out bcrData2[0]);
                                if (iReturnCode != 0)
                                {
                                    throw new Exception("Return code not 0, code: " + iReturnCode);
                                }
                                MxPlcBarcodeDataLane2 = bcrData2;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"ReadBarcodeDataFromPLC - {ex.Message}");
            }
        }

        private void ReadBoardAvailableFromPLC()
        {
            try
            {
                int baData = 0;
                string addr = string.Empty;
                int length = 1;
                foreach (globalParameter.eLane lane in Enum.GetValues(typeof(globalParameter.eLane)))
                {
                    switch (lane)
                    {
                        case globalParameter.eLane.Lane1:
                            {
                                if (globalParameter.UpstreamEnableLane1 == globalParameter.eUpstreamUse.Disable) break;
                                addr = globalParameter.AddrMxBoardAvailableLane1;
                                int iReturnCode = UpstreamMxPlc.ReadDeviceBlockInt(addr, length, out baData);

                                if (iReturnCode != 0)
                                {
                                    throw new Exception("Return code not 0, code: " + iReturnCode);
                                }

                                MxPlcBaSignalLane1 = baData;
                                UpdatingValues("tbBASignalLane1", MxPlcBaSignalLane1.ToString());
                            }
                            break;
                        case globalParameter.eLane.Lane2:
                            {
                                if (globalParameter.UpstreamEnableLane2 == globalParameter.eUpstreamUse.Disable) break;
                                addr = globalParameter.AddrMxBoardAvailableLane2;
                                int iReturnCode = UpstreamMxPlc.ReadDeviceBlockInt(addr, length, out baData);

                                if (iReturnCode != 0)
                                {
                                    throw new Exception("Return code not 0, code: " + iReturnCode);
                                }

                                MxPlcBaSignalLane2 = baData;
                                UpdatingValues("tbBASignalLane2", MxPlcBaSignalLane2.ToString());
                            }
                            break;
                        default:
                            break;
                    }
                }


            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"ReadBoardAvailableFromPLC - {ex.Message}");
            }
        }

        private void WriteRailWidthToPlc(globalParameter.eLane lane)
        {
            try
            {
                float flane1 = 0;
                float flane2 = 0;
                int ilane1 = 0;
                int ilane2 = 0;
                string addrLane1 = globalParameter.AddrMxRailWidthLane1;
                string addrLane2 = globalParameter.AddrMxRailWidthLane2;

                switch (lane)
                {
                    case globalParameter.eLane.Lane1:
                        {
                            switch (globalParameter.Lane1Rail)
                            {
                                case "Rail1":
                                    flane1 = ocx.Oven.RailWidthSP[0];
                                    break;
                                case "Rail2":
                                    flane1 = ocx.Oven.RailWidthSP[1];
                                    break;
                                case "Rail3":
                                    flane1 = ocx.Oven.RailWidthSP[2];
                                    break;
                                case "Rail4":
                                    flane1 = ocx.Oven.RailWidthSP[3];
                                    break;
                                case "-":
                                    return; // No need to write value;
                                default:
                                    break;
                            }

                            if (flane1 > 0)
                            {
                                ilane1 = BCDConverter(flane1);
                                LogWrite("Fill rail1 width: " + flane1);
                            }

                            int returnCode = DownstreamMxPlc.WriteDeviceBlockInt(addrLane1, 1, ref ilane1);

                            if (returnCode != 0)
                            {
                                throw new Exception("Return code is not 0, return code: " + returnCode);
                            }
                        }
                        break;
                    case globalParameter.eLane.Lane2:
                        {
                            switch (globalParameter.Lane2Rail)
                            {
                                case "Rail1":
                                    flane2 = ocx.Oven.RailWidthSP[0];
                                    break;
                                case "Rail2":
                                    flane2 = ocx.Oven.RailWidthSP[1];
                                    break;
                                case "Rail3":
                                    flane2 = ocx.Oven.RailWidthSP[2];
                                    break;
                                case "Rail4":
                                    flane2 = ocx.Oven.RailWidthSP[3];
                                    break;
                                case "-":
                                    return; // No need to write value;
                                default:
                                    break;
                            }

                            if (flane2 > 0)
                            {
                                ilane2 = BCDConverter(flane2);
                                LogWrite("Fill rail2 width: " + flane2);
                            }

                            int returnCode = DownstreamMxPlc.WriteDeviceBlockInt(addrLane2, 1, ref ilane2);

                            if (returnCode != 0)
                            {
                                throw new Exception("Return code is not 0, return code: " + returnCode);
                            }
                        }
                        break;
                    default:
                        break;
                }

                if (globalParameter.RailLogging)
                {
                    DateTime now = DateTime.Now;
                    string path = "C:\\Heller Industries\\AISIN Line Comm\\Logs\\Rail_" +
                        now.Year.ToString("D4") + now.Month.ToString("D2") + now.Day.ToString("D2") + ".log";

                    try
                    {
                        StreamWriter logFile = new StreamWriter(path, true);

                        logFile.WriteLine(flane1.ToString("F1"));
                        logFile.WriteLine(flane2.ToString("F1"));

                        logFile.Close();
                    }
                    catch (Exception ex)
                    {
                        HLog.log(HLog.eLog.EXCEPTION, $"DownstreamThread - RailLogging error {ex.Message}");
                    }
                }

            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"WriteRailWidthToPlc - {ex.Message}");
            }
        }

        public void UpdatingValues(string item, string value)
        {
            if (InvokeRequired) this.Invoke(new MethodInvoker(() => UpdatingValues(item, value)));
            else
            {
                try
                {
                    switch (item)
                    {
                        case "btnStartComm":
                            {
                                btnStartComm.Text = value;
                            }
                            break;
                        case "tbBarcodeLane1":
                            {
                                tbBarcodeLane1.Text = value;
                            }
                            break;
                        case "tbBarcodeLane2":
                            {
                                tbBarcodeLane2.Text = value;
                            }
                            break;
                        case "tbBASignalLane1":
                            {
                                tbBASignalLane1.Text = value;
                            }
                            break;
                        case "tbBASignalLane2":
                            {
                                tbBASignalLane2.Text = value;
                            }
                            break;
                        case "tbBarcodeLane1string":
                            {
                                tbBarcodeLane1string.Text = value;
                            }
                            break;
                        case "tbBarcodeLane2string":
                            {
                                tbBarcodeLane2string.Text = value;
                            }
                            break;

                        case "radioButtonRail1":
                            break;
                        case "radioButtonRail2":
                            break;
                        case "radioButtonRail3":
                            break;
                        case "radioButtonRail4":
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    HLog.log(HLog.eLog.EXCEPTION, $"UpdatingValues - {ex.Message}");
                }
            }
        }

        public void UpdatingValuesOcx()
        {
            if (InvokeRequired) this.Invoke(new MethodInvoker(() => UpdatingValuesOcx()));
            else
            {
                try
                {
                    tbRail1WidthSP.Text = ocx.Oven.RailWidthSP[0].ToString();
                    tbRail2WidthSP.Text = ocx.Oven.RailWidthSP[1].ToString();
                    tbRail3WidthSP.Text = ocx.Oven.RailWidthSP[2].ToString();
                    tbRail4WidthSP.Text = ocx.Oven.RailWidthSP[3].ToString();

                    tbRail1WidthPV.Text = ocx.Oven.RailWidthPV[0].ToString();
                    tbRail2WidthPV.Text = ocx.Oven.RailWidthPV[1].ToString();
                    tbRail3WidthPV.Text = ocx.Oven.RailWidthPV[2].ToString();
                    tbRail4WidthPV.Text = ocx.Oven.RailWidthPV[3].ToString();

                    tbBelt1Processed.Text = ocx.Oven.ProcessedCount[0].ToString();
                    tbBelt2Processed.Text = ocx.Oven.ProcessedCount[1].ToString();
                    tbBelt3Processed.Text = ocx.Oven.ProcessedCount[2].ToString();
                    tbBelt4Processed.Text = ocx.Oven.ProcessedCount[3].ToString();

                    tbBelt1InOven.Text = ocx.Oven.InOvenCount[0].ToString();
                    tbBelt2InOven.Text = ocx.Oven.InOvenCount[1].ToString();
                    tbBelt3InOven.Text = ocx.Oven.InOvenCount[2].ToString();
                    tbBelt4InOven.Text = ocx.Oven.InOvenCount[3].ToString();

                    tbLightTowerColor.Text = ocx.Oven.LighTower;


                }
                catch (Exception ex)
                {
                    HLog.log(HLog.eLog.EXCEPTION, $"UpdatingValues - {ex.Message}");
                }
            }
        }

        public void UpdatingHc2State()
        {
            try
            {
                if (InvokeRequired) this.Invoke(new MethodInvoker(() => UpdatingHc2State()));
                else
                {
                    tb_Hc2state.Text = ocx.Oven.IsConnectWithHC2 ? "Connect" : "Disconnect";
                    tb_Hc2state.BackColor = ocx.Oven.IsConnectWithHC2 ? System.Drawing.Color.Lime : System.Drawing.Color.Yellow;

                }
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"UpdatingHc2State - {ex.Message}");
            }
        }

        public void UpdatingUpDownPlcState()
        {
            try
            {
                if (InvokeRequired) this.Invoke(new MethodInvoker(() => UpdatingUpDownPlcState()));
                else
                {
                    if (UpstreamMxPlc != null)
                    {
                        tb_UpPlcConnState.Text = UpstreamMxPlc.BConnected ? "Connect" : "Disconnect";
                        tb_UpPlcConnState.BackColor = UpstreamMxPlc.BConnected ? System.Drawing.Color.Lime : System.Drawing.Color.Yellow;
                        tb_upRetryCount.Text = UpstreamMxPlc.RetryConnectCount.ToString();

                        if (LastUpstreamConnectedState != UpstreamMxPlc.BConnected)
                        {
                            HLog.log(HLog.eLog.EVENT, $"Upstream Connection Change from {LastUpstreamConnectedState} to {UpstreamMxPlc.BConnected}");
                        }
                        LastUpstreamConnectedState = UpstreamMxPlc.BConnected;
                    }

                    if (DownstreamMxPlc != null)
                    {
                        tb_DnPlcConnState.Text = DownstreamMxPlc.BConnected ? "Connect" : "Disconnect";
                        tb_DnPlcConnState.BackColor = DownstreamMxPlc.BConnected ? System.Drawing.Color.Lime : System.Drawing.Color.Yellow;
                        tb_dnRetryCount.Text = DownstreamMxPlc.RetryConnectCount.ToString();

                        if (LastDownstreamConnectedState != DownstreamMxPlc.BConnected)
                        {
                            HLog.log(HLog.eLog.EVENT, $"Downstream Connection Change from {LastDownstreamConnectedState} to {DownstreamMxPlc.BConnected}");
                        }
                        LastDownstreamConnectedState = DownstreamMxPlc.BConnected;
                    }
                }
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"UpdatingHc2State - {ex.Message}");
            }
        }

        public void FillPLCOutPut(float railWidth, int railWidthIndex)
        {
            // compute rail width in tenths of millimeter
            ushort temp = (ushort)((railWidth + 0.05) * 10);

            // break down into BCD digits
            ushort digit1 = (ushort)(temp / 1000);
            temp %= 1000;
            ushort digit2 = (ushort)(temp / 100);
            temp %= 100;
            ushort digit3 = (ushort)(temp / 10);
            ushort digit4 = (ushort)(temp % 10);

            switch (globalParameter.PLCType)
            {
                case globalParameter.ePLCType.None:
                    break;
                case globalParameter.ePLCType.OMRON:
                    {
                        // store rail width in BCD
                        // Big endian
                        plcOutputData[railWidthIndex] = digit1 << 12 | digit2 << 8 | digit3 << 4 | digit4;
                    }
                    break;
                case globalParameter.ePLCType.Mitsubishi:
                    {
                        // store rail width in BCD
                        // Little endian
                        plcOutputData[railWidthIndex] = digit1 << 12 | digit2 << 8 | digit3 << 4 | digit4;
                    }
                    break;
                default:
                    break;
            }

        }

        private int BCDConverter(float railWidth)
        {
            int result = 0;
            // compute rail width in tenths of millimeter
            ushort temp = (ushort)((railWidth + 0.05) * 10);

            // break down into BCD digits
            ushort digit1 = (ushort)(temp / 1000);
            temp %= 1000;
            ushort digit2 = (ushort)(temp / 100);
            temp %= 100;
            ushort digit3 = (ushort)(temp / 10);
            ushort digit4 = (ushort)(temp % 10);

            // store rail width in BCD
            result = digit1 << 12 | digit2 << 8 | digit3 << 4 | digit4;
            return result;

        }

        private void UpdateBarcodeString()
        {
            try
            {
                // update barcode data
                string barcodeTextLane1 = "";
                string barcodeTextLane2 = "";

                Array.Clear(lane1BarcodeByte, 0, BARCODE_MAX);
                Array.Clear(lane2BarcodeByte, 0, BARCODE_MAX);

                for (int ndx = 0; ndx < BARCODE_MAX; ndx++)
                {
                    byte barcodeDigit;
                    switch (globalParameter.PLCType)
                    {
                        case globalParameter.ePLCType.None:
                            break;
                        case globalParameter.ePLCType.OMRON:
                            {
                                // Big endian
                                if ((ndx & 1) == 1)
                                    barcodeDigit = (byte)(plcInputData[ndx / 2] & 0xFF);
                                else
                                    barcodeDigit = (byte)(plcInputData[ndx / 2] >> 8 & 0xFF);
                                barcodeTextLane1 += barcodeDigit.ToString("X2") + " ";
                                lane1BarcodeByte[ndx] = barcodeDigit;
                            }
                            break;
                        case globalParameter.ePLCType.Mitsubishi:
                            {
                                // Little endian
                                if ((ndx & 1) == 0)
                                    barcodeDigit = (byte)(plcInputData[ndx / 2] & 0xFF);
                                else
                                    barcodeDigit = (byte)(plcInputData[ndx / 2] >> 8 & 0xFF);
                                barcodeTextLane1 += barcodeDigit.ToString("X2") + " ";
                                lane1BarcodeByte[ndx] = barcodeDigit;
                            }
                            break;
                        default:
                            break;
                    }
                }

                int startIndex = 100;
                for (int ndx = startIndex; ndx < startIndex + BARCODE_MAX; ndx++)
                {
                    byte barcodeDigit;

                    switch (globalParameter.PLCType)
                    {
                        case globalParameter.ePLCType.None:
                            break;
                        case globalParameter.ePLCType.OMRON:
                            {
                                // Big endian
                                if ((ndx & 1) == 1)
                                    barcodeDigit = (byte)(plcInputData[startIndex + (ndx - startIndex) / 2] & 0xFF);
                                else
                                    barcodeDigit = (byte)(plcInputData[startIndex + (ndx - startIndex) / 2] >> 8 & 0xFF);
                                //if (barcodeDigit == 0)
                                //    break;
                                barcodeTextLane2 += barcodeDigit.ToString("X2") + " ";
                                lane2BarcodeByte[ndx - startIndex] = barcodeDigit;
                            }
                            break;
                        case globalParameter.ePLCType.Mitsubishi:
                            {
                                // Little endian
                                if ((ndx & 1) == 0)
                                    barcodeDigit = (byte)(plcInputData[startIndex + (ndx - startIndex) / 2] & 0xFF);
                                else
                                    barcodeDigit = (byte)(plcInputData[startIndex + (ndx - startIndex) / 2] >> 8 & 0xFF);
                                //if (barcodeDigit == 0)
                                //    break;
                                barcodeTextLane2 += barcodeDigit.ToString("X2") + " ";
                                lane2BarcodeByte[ndx - startIndex] = barcodeDigit;
                            }
                            break;
                        default:
                            break;
                    }
                }

                UpdatingValues("tbBarcodeLane1", barcodeTextLane1);
                UpdatingValues("tbBarcodeLane2", barcodeTextLane2);
                UpdatingValues("tbBarcodeLane1string", GetBarcodeString(lane1BarcodeByte));
                UpdatingValues("tbBarcodeLane2string", GetBarcodeString(lane2BarcodeByte));
                UpdatingValues("tbBASignalLane1", plcInputData[BA_SIGNAL_ARRAY_NDX_LANE1].ToString());
                UpdatingValues("tbBASignalLane2", plcInputData[BA_SIGNAL_ARRAY_NDX_LANE2].ToString());

            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"UpdateBarcodeString - {ex.Message}");
            }
        }

        private void UpdateBarcodeMxString()
        {
            try
            {
                // update barcode data
                string barcodeTextLane1 = "";
                string barcodeTextLane2 = "";

                Array.Clear(lane1BarcodeByte, 0, BARCODE_MAX);
                Array.Clear(lane2BarcodeByte, 0, BARCODE_MAX);

                for (int ndx = 0; ndx < BARCODE_MAX; ndx++)
                {
                    byte barcodeDigit;
                    // Little endian
                    if ((ndx & 1) == 0)
                        barcodeDigit = (byte)(MxPlcBarcodeDataLane1[ndx / 2] & 0xFF);
                    else
                        barcodeDigit = (byte)(MxPlcBarcodeDataLane1[ndx / 2] >> 8 & 0xFF);
                    barcodeTextLane1 += barcodeDigit.ToString("X2") + " ";
                    lane1BarcodeByte[ndx] = barcodeDigit;
                }

                for (int ndx = 0; ndx < BARCODE_MAX; ndx++)
                {
                    byte barcodeDigit;
                    // Little endian
                    if ((ndx & 1) == 0)
                        barcodeDigit = (byte)(MxPlcBarcodeDataLane2[ndx / 2] & 0xFF);
                    else
                        barcodeDigit = (byte)(MxPlcBarcodeDataLane2[ndx / 2] >> 8 & 0xFF);
                    //if (barcodeDigit == 0)
                    //    break;
                    barcodeTextLane2 += barcodeDigit.ToString("X2") + " ";
                    lane2BarcodeByte[ndx] = barcodeDigit;
                }

                UpdatingValues("tbBarcodeLane1", barcodeTextLane1);
                UpdatingValues("tbBarcodeLane2", barcodeTextLane2);
                UpdatingValues("tbBarcodeLane1string", GetBarcodeString(lane1BarcodeByte));
                UpdatingValues("tbBarcodeLane2string", GetBarcodeString(lane2BarcodeByte));
                UpdatingValues("tbBASignalLane1", MxPlcBaSignalLane1.ToString());
                UpdatingValues("tbBASignalLane2", MxPlcBaSignalLane2.ToString());

            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"UpdateBarcodeString - {ex.Message}");
            }
        }
        #endregion

        #region [Barcode Function]


        private string GetBarcodeString(byte[] barcodeByte)
        {
            string barcode = string.Empty;

            try
            {
                barcode = System.Text.Encoding.Default.GetString(barcodeByte);
                barcode = Encoding.ASCII.GetString(barcodeByte, 0, barcodeByte.Length);
                int offset = barcode.IndexOf("\r\n");
                if (offset < 0)
                    offset = barcode.IndexOf('\r');
                if (offset < 0)
                    offset = barcode.IndexOf('\n');
                if (offset > 0)
                    barcode = barcode.Substring(0, offset);

                // 1.07 revision,  trim barcode before using it for changing recipe or speed or length.
                barcode = barcode.Trim().Replace(" ", "");
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, ex.Message);
                // ignor - display only.
                return string.Empty;
            }

            return barcode;
        }

        public void CheckBarcodeRecipe(int lane, string barcodeFromUp)
        {
            string currentJob = GetCurrentRecipeName();
            bool recipeBarcodeFound = false;
            float beltSpeedChanged = -1;  //  -1 means needn't change belt speed. other means the target belt speed
            int beltSpeedChangeRemark = 0;  // 0 means needn't change, 1 means change lane1, 2 means change lane2
            float railWidthChanged = -1;  //  -1 means needn't change belt speed. other means the target belt speed
            int railWidthChangeRemark = 0;  // 0 means needn't change, 1 means change lane1, 2 means change lane2
            if (globalParameter.barcodeRecipeList == null || globalParameter.barcodeRecipeList.Count == 0)
            {
                if (!barcodeRecipeEmptyDisplayed)
                {
                    barcodeRecipeEmptyDisplayed = true;
                    LogWrite("Barcode mapping list is empty.");
                    HLog.log(HLog.eLog.ERROR, $"Fail to barcode mappling list [{(globalParameter.eLane)lane}] [Barcode: {barcodeFromUp}]");

                    if (globalParameter.barcodeRecipeList == null)
                        HLog.log(HLog.eLog.ERROR, $"barcodeRecipeList is null");

                    if (globalParameter.barcodeRecipeList.Count == 0)
                        HLog.log(HLog.eLog.ERROR, $"barcodeRecipeList count is 0");

                    if (MessageBox.Show("Barcode Recipe table is empty, please fill it !", "Barcode error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                        barcodeRecipeEmptyDisplayed = false;
                }
            }
            else
            {
                foreach (barcodeRecipe bar_rec in globalParameter.barcodeRecipeList)
                {
                    float bar_rec_speed = Convert.ToSingle(bar_rec.beltSpeed);
                    float bar_rec_width = Convert.ToSingle(bar_rec.beltWidth);

                    string currentRecipe = currentJob.ToLower().Replace(".job", "");
                    string barcodeRecipe = bar_rec.recipe.ToLower().Replace(".job", "");
                    LogWrite("current recipe: " + currentRecipe + ", barcode recipe: " + barcodeRecipe);
                    HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] [Current Recipe:{currentRecipe}] [Barcode Recipe: {barcodeRecipe}]");
                    // if recipe from list matches current job
                    if (currentRecipe.Equals(barcodeRecipe))    //ver1.0.61
                    {
                        LogWrite("Current recipe equals barcode recipe");
                        LogWrite("barcode from mapping table: " + bar_rec.barcode + ", barcode from upstream: " + barcodeFromUp);
#if true
                        bool match = RegexLib.IsValidCurrency(barcodeFromUp, bar_rec.barcode);
                        HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] [Match with Current Recipe:{currentRecipe}] [Barcode Recipe: {barcodeRecipe}] [Mappling Table: {bar_rec.barcode}] [Match: {match}]");
#else
                        bool match = Regex.IsMatch(barcodeFromUp, WildCardToRegular(bar_rec.barcode));
#endif

                        if (match)
                        {
                            recipeBarcodeFound = true;
                            LogWrite("Barcode=" + barcodeFromUp + " matches to current Recipe=" + currentRecipe);
                            refreshSpeedWidth();
                            ////Monitor.Exit(lockObject);

                            // check lane belt speed or belt width
                            if (bar_rec_speed != -1 && bar_rec_speed != currentBeltSpeed[lane])
                            {
                                beltSpeedChanged = bar_rec_speed;
                                beltSpeedChangeRemark = 2;
                                LogWrite(string.Format("Lane{0}, current belt speed setpoint is {1}, belt speed setpoint in the mapping table is {2}. They doesn't match !", lane, currentBeltSpeed[1].ToString(), bar_rec.beltSpeed.ToString()));
                                HLog.log(HLog.eLog.EVENT, $"Not Match Belt Speed[{(globalParameter.eLane)lane}] [Current Speed: {currentBeltSpeed[lane]}] [Barcode Speed: {bar_rec_speed}]");
                            }
                            if (bar_rec_width != -1 && bar_rec_width != currentBeltWidth[lane])
                            {
                                railWidthChanged = bar_rec_width;
                                railWidthChangeRemark = 2;
                                LogWrite(string.Format("Lane{0}, current rail width setpoint is {1}, belt width setpoint in the mapping table is {2}. They doesn't match !", lane, currentBeltWidth[1].ToString(), bar_rec.beltWidth.ToString()));
                                HLog.log(HLog.eLog.EVENT, $"Not Match Rail Width[{(globalParameter.eLane)lane}] [Current Width: {currentBeltWidth[lane]}] [Barcode Width: {bar_rec_width}]");
                            }

                            // All lanes needn't change anything
                            if (beltSpeedChangeRemark == 0 && railWidthChangeRemark == 0)
                            {
                                LogWrite("Barcode is allowed to release smema");
                                SmemaLaneHold(lane, 0);
                                HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] Barcode is allowed [Release]");
                            }
                            // need change belt speed and rail width for lane1 or lane2
                            else if (railWidthChangeRemark != 0 && beltSpeedChangeRemark != 0)
                            {
                                LogWrite("Begin to change belt speed and rail width on lane: " + lane);
                                ChangeBeltSpeedAndWidth(lane, railWidthChanged, beltSpeedChanged);
                                HLog.log(HLog.eLog.EVENT, $"Begin to change belt speed and rail width on {(globalParameter.eLane)lane}");
                            }
                            // need change rail width for lane1 or lane2
                            else if (railWidthChangeRemark != 0)
                            {
                                LogWrite("Begin to change rail width on lane: " + lane);
                                ChangeRailWidth(lane, railWidthChanged);
                                HLog.log(HLog.eLog.EVENT, $"Begin to change rail width on {(globalParameter.eLane)lane}");
                            }
                            // need change belt speed for lane1 or lane2
                            else if (beltSpeedChangeRemark != 0)
                            {
                                LogWrite("Begin to change belt speed on lane: " + lane);
                                ChangeBeltSpeed(lane, beltSpeedChanged);
                                HLog.log(HLog.eLog.EVENT, $"Begin to change belt speed on {(globalParameter.eLane)lane}");
                            }

                            break;
                        }
                    }

                    // may need to change recipe
                    else
                    {
#if true
                        bool match = RegexLib.IsValidCurrency(barcodeFromUp, bar_rec.barcode);
                        HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] [Not Match with Current Recipe:{currentRecipe}] [Barcode Recipe: {barcodeRecipe}] [Mappling Table: {bar_rec.barcode}] [Match: {match}]");
#else
                        bool match = Regex.IsMatch(barcodeFromUp, WildCardToRegular(bar_rec.barcode));
#endif

                        if (match)
                        {
                            recipeBarcodeFound = true;
                            nextRecipeToLoad = bar_rec.recipe;
                            LogWrite("Barcode=" + barcodeFromUp + " matches to another Recipe=" + barcodeRecipe);
                            refreshSpeedWidth();
                            if (bar_rec_speed != -1)
                            {
                                beltSpeedChanged = bar_rec_speed;
                                LogWrite("Also need to change belt speed to be " + beltSpeedChanged + " after recipe change");
                                HLog.log(HLog.eLog.EVENT, $"[Also need to change belt speed to be {beltSpeedChanged} after recipe change");
                            }
                            if (bar_rec_width != -1)
                            {
                                railWidthChanged = bar_rec_width;
                                LogWrite("Also need to change rail width to be " + railWidthChanged + " after recipe change");
                                HLog.log(HLog.eLog.EVENT, $"[Also need to change rail width to be {beltSpeedChanged} after recipe change");
                            }
                            //nextRecipeToLoad = 
                            ChangeRecipe(lane, nextRecipeToLoad, railWidthChanged, beltSpeedChanged);
                            break;
                        }
                    }
                }
                if (!recipeBarcodeFound)
                {
                    MessageBox.Show(string.Format("Barcode: {0} not found in the mapping table !", barcodeFromUp.Substring(0, 12)), "Barcode error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void ChangeBeltSpeed(int lane, float targetSpeed)
        {
            int currentBoardsCount = GetBoardsCountInOven(lane);
            if (currentBoardsCount == 0)
            {
                // just change speed
                SetBeltSpeed(lane, targetSpeed);
                // relesae smema lane hold for this lane
                SmemaLaneHold(lane, 0);
            }
            else
            {
                // start a monitor thread to change belt speed when board count is 0.
                if (waitForOvenEmptyToChangeWidthorSpeed == null)
                {
                    waitForOvenEmptyToChangeWidthorSpeed = new Thread(new ThreadStart(() =>
                    {
                        WaitForOvenEmptyToChangeWidthOrSpeed(lane, -1, targetSpeed);
                    }));
                    waitForOvenEmptyToChangeWidthorSpeed.SetApartmentState(ApartmentState.STA);
                    waitForOvenEmptyToChangeWidthorSpeed.Start();
                }
            }
        }

        private void ChangeRailWidth(int lane, float targetWidth)
        {
            int currentBoardsCount = GetBoardsCountInOven(lane);
            if (currentBoardsCount == 0)
            {
                // just change width
                SetRailWidth(lane, targetWidth);
                // relesae smema lane hold for this lane
                SmemaLaneHold(lane, 0);
            }
            else
            {
                // start a monitor thread to change belt width when board count is 0.
                if (waitForOvenEmptyToChangeWidthorSpeed == null)
                {
                    waitForOvenEmptyToChangeWidthorSpeed = new Thread(new ThreadStart(() =>
                    {
                        WaitForOvenEmptyToChangeWidthOrSpeed(lane, targetWidth, -1);
                    }));
                    waitForOvenEmptyToChangeWidthorSpeed.SetApartmentState(ApartmentState.STA);
                    waitForOvenEmptyToChangeWidthorSpeed.Start();
                }
            }
        }

        private void ChangeBeltSpeedAndWidth(int lane, float targetWidth, float targetSpeed)
        {
            int currentBoardsCount = GetBoardsCountInOven(lane);
            if (currentBoardsCount == 0)
            {
                // just change width
                SetBeltSpeed(lane, targetSpeed);
                SetRailWidth(lane, targetWidth);
                // relesae smema lane hold for this lane
                SmemaLaneHold(lane, 0);
            }
            else
            {
                // start a monitor thread to change belt width when board count is 0.
                if (waitForOvenEmptyToChangeWidthorSpeed == null)
                {
                    waitForOvenEmptyToChangeWidthorSpeed = new Thread(new ThreadStart(() =>
                    {
                        WaitForOvenEmptyToChangeWidthOrSpeed(lane, targetWidth, targetSpeed);
                    }));
                    waitForOvenEmptyToChangeWidthorSpeed.SetApartmentState(ApartmentState.STA);
                    waitForOvenEmptyToChangeWidthorSpeed.Start();
                }
            }
        }

        private void WaitForOvenEmptyToChangeWidthOrSpeed(int lane, float targetWidth, float targetSpeed)
        {
            while (true)
            {
                int boardCount = GetBoardsCountInOven(lane);
                if (boardCount == 0)
                {
                    if (targetWidth != -1)
                        SetRailWidth(lane, targetWidth);
                    if (targetSpeed != -1)
                        SetBeltSpeed(lane, targetSpeed);
                    SmemaLaneHold(lane, 0);
                    break;
                }
                Thread.Sleep(2000);
            }
            waitForOvenEmptyToChangeWidthorSpeed = null;
        }

        private void WaitForOvenEmptyToChangeRecipe(int lane, float targetWidth, float targetSpeed, string recipeName)
        {
            while (true)
            {
                int boardCount = 0;
                for (int i = 0; i < 2; i++)
                {
                    boardCount += GetBoardsCountInOven(i);
                }
                HLog.log(HLog.eLog.EVENT, $"WaitForOvenEmptyToChangeRecipe()[Board In Count: {boardCount}]");
                if (boardCount == 0 && nextRecipeToLoad != null)
                {
                    LoadRecipe(recipeName);
                    if (targetWidth != -1 || targetSpeed != -1)
                    {
                        Thread.Sleep(45 * 1000);

                        if (targetWidth != -1)
                        {
                            SetRailWidth(lane, targetWidth);
                        }
                        if (targetSpeed != -1)
                        {
                            SetBeltSpeed(lane, targetSpeed);
                        }
                    }
                    // release smema for all lanes.
                    SmemaLaneHold(0, 0);
                    SmemaLaneHold(1, 0);
                    break;
                }
                Thread.Sleep(2000);
            }
            // waitForOvenEmptyToLoadRecipe = null; // v1.19
            bWaitForOvenEmptytoLoadRecipe = false;
        }

        private void ChangeRecipe(int lane, string recipe, float targetWidth, float targetSpeed)
        {
            HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] ChangeRecipe() [Recipe: {recipe}] [Width: {targetWidth}] [Speed: {targetSpeed}]");
            int currentBoardsCount = 0;

            // for (int i = 0; i < currentBoardsCount; i++) v1.19 MSL
            // Not allow change recipe when one lane is empty...
            // TCO is Single lane
            // Dual lane need to both lane empty.
            for (int i = 0; i < 2; i++) // Max Lane 4
            {
                currentBoardsCount += GetBoardsCountInOven(i);
            }

            HLog.log(HLog.eLog.EVENT, $"ChangeRecipe() [Board In Count: {currentBoardsCount}]");

            if (currentBoardsCount == 0)
            {
                // just load recipe
                LoadRecipe(recipe);

                // any of them need to be changed after recipe loaded
                if (targetWidth != -1 || targetSpeed != -1)
                {
                    Thread.Sleep(45 * 1000);
                    if (targetSpeed != -1)
                    {
                        SetBeltSpeed(lane, targetSpeed);
                        HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] [SetBeltSpeed] [Speed: {targetSpeed}]");
                    }
                    if (targetWidth != -1)
                    {
                        SetRailWidth(lane, targetWidth);
                        HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] [SetRailWidth] [Width: {targetWidth}]");
                    }
                }

                if (lane == 0)
                {
                    SmemaLaneHold(0, 0);
                    HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] [ChangeRecipe] [SMEMA: Release]");
                }

                if (lane == 1)
                {
                    SmemaLaneHold(1, 0);
                    HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)lane}] [ChangeRecipe] [SMEMA: Release]");
                }
            }
            else
            {
                HLog.log(HLog.eLog.EVENT, $":Starting wait oven emtpy for Load recipe");
                // start a monitor thread to change recipe
#if true        // v1.19
                if (!bWaitForOvenEmptytoLoadRecipe)
                {
                    Task.Factory.StartNew(() =>
                    {
                        bWaitForOvenEmptytoLoadRecipe = true;
                        WaitForOvenEmptyToChangeRecipe(lane, targetWidth, targetSpeed, recipe);
                    });
                }
#else
                if (waitForOvenEmptyToLoadRecipe == null)
                {
                    waitForOvenEmptyToLoadRecipe = new Thread(new ThreadStart(() => {
                        WaitForOvenEmptyToChangeRecipe(lane, targetWidth, targetSpeed, recipe);
                    }));
                }
#endif
            }
        }

        private string WildCardToRegular(string value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }

        #endregion

        #region [Ocx Function]

        private bool CheckCBSExist(int lane)
        {
            //ocxMutex.WaitOne();
            float fResult = 0;
            switch (lane)
            {
                case 0:
                    {
                        fResult = ocx.GetChannel(37);
                        //fResult = obj.GetChannel(37);
                    }
                    break;
                case 1:
                    {
                        fResult = ocx.GetChannel(52);
                        //fResult = obj.GetChannel(52);
                    }
                    break;
                default:
                    fResult = -1;
                    break;
            }
            //ocxMutex.ReleaseMutex();
            bool exist = (fResult == 0 || fResult == -1) ? false : true;
            return exist;
        }

        private void LoadRecipe(string recipeName)
        {
#if true
            recipeName = "c:\\oven\\recipe files\\" + recipeName + ".job";
            int iResult = ocx.LoadRecipe(recipeName);
            LogWrite("Loading recipe: " + recipeName + " Result=" + iResult.ToString());
            HLog.log(HLog.eLog.EVENT, $"[Loading recipe: {recipeName}] [Result: {iResult}]");
            nextRecipeToLoad = null;
#else
            ocxMutex.WaitOne();
            recipeName = "c:\\oven\\recipe files\\" + recipeName + ".job";
            int iResult = obj.LoadRecipe(ref recipeName);
            ocxMutex.ReleaseMutex();
            LogWrite("Loading recipe: " + recipeName + " Result=" + iResult.ToString());
            nextRecipeToLoad = null;
#endif
        }

        private int GetBoardsCountInOven(int lane)
        {
            float fResult = ocx.GetChannel((short)(46 + lane));
            //ocxMutex.WaitOne();
            //float fResult = obj.GetChannel((short)(46 + lane));
            //ocxMutex.ReleaseMutex();
            return (int)fResult;
        }

        private int GetBeltCount()
        {
            int beltCount = 0;
            for (int i = 0; i < 2; i++)
            {
                if (GetBeltSpeedValue(i) != -1)
                    beltCount++;
            }
            return beltCount;
        }

        /// <summary>
        /// Set smema to hold or release state
        /// </summary>
        /// <param name="lane">lane number</param>
        /// <param name="hold">0 means hold, 1 means release</param>
        private void SmemaLaneHold(int lane, int hold)
        {
            // In case of TCO (Front PC Lane#1 / Rear PC Lane#1)
            // > Need to Modify Mx Component address for Rear PC.
            // [Front PC - Lane#1] D0 / D21
            // [Rear PC - Lane#1] D100 / D121

            // In case of Dual Lane(Front Lane#1 / Rear Lane#2)
            // > Should be set Lane1 for front lane,
            // > And should be set Lane2 for rear lane.
            // [Front Lane#1] D0 / D21
            // [Rear Lane#1] D100 / D121

            // 11-Nov-22  01.01.01.02   MSL MSL Added Lane Type configuration for TCO oven.
            switch (globalParameter.LaneType)
            {
                case "SingleLane":
                    {
                        HLog.log(HLog.eLog.EVENT, $"LaneType is Single lane Change to SMEMA Control Lane from [{lane}] to [0]");
                        lane = 0;
                    }
                    break;
                case "DualLane":
                    break;
                default:
                    break;
            }

            ocx.SetSmema(lane, hold);
        }

        public string GetCurrentRecipeName()
        {
#if true
            string sResult = ocx.GetRecipePath();
            int offset = sResult.LastIndexOf('\\');
            if (offset >= 0)
                sResult = sResult.Substring(offset + 1);
            LogWrite("Current recipe name=" + sResult);
            return sResult;
#else
            string outPtr = null;
            ocxMutex.WaitOne();
            int iResult = obj.GetRecipePath(ref outPtr);
            ocxMutex.ReleaseMutex();
            string sResult = outPtr;
            int offset = sResult.LastIndexOf('\\');
            if (offset >= 0)
                sResult = sResult.Substring(offset + 1);
            LogWrite("Current recipe name=" + sResult);
            return sResult;
#endif
        }

        public float GetRailSetPoint(int lane)
        {
#if true
            float fResult = ocx.GetRailWidth((short)(lane + 101));
            return fResult;
#else
            ocxMutex.WaitOne();
            Object objTmp = obj.GetRailWidth((short)(lane + 101));
            ocxMutex.ReleaseMutex();
            if (objTmp == null)
            {
                return -1;
            }
            float fResult = (float)objTmp;
            //LogWrite("Get rail width SP: " + fResult.ToString() + " on lane" + lane.ToString());
            return fResult;
#endif
        }

        public float GetRailValue(int lane)
        {
#if true
            float fResult = ocx.GetRailWidth((short)(lane + 1));
            return fResult;
#else
            ocxMutex.WaitOne();
            Object tempObj = obj.GetRailWidth((short)(lane + 1));
            ocxMutex.ReleaseMutex();
            if (obj == null)
            {
                return -1;
            }
            float fResult = (float)tempObj;
            //LogWrite("Get rail width PV: " + fResult.ToString() + " on lane" + lane.ToString());
            return fResult;
#endif
        }

        public float GetBeltSpeedSetPoint(int lane)
        {
#if true
            int iResult = ocx.GetFurnaceBeltSetPoint((short)(lane + 1));
            float fResult = iResult / 10.0F;
            LogWrite("Get belt speed SP: " + fResult.ToString() + " on lane" + lane.ToString());
            return fResult;
#else
            ocxMutex.WaitOne();
            Object tmpObj = obj.GetFurnaceBeltSetPoint((short)(lane + 1));
            ocxMutex.ReleaseMutex();
            if (tmpObj == null)
            {
                return -1;
            }
            int iResult = (int)tmpObj;
            float fResult = iResult / 10.0F;
            LogWrite("Get belt speed SP: " + fResult.ToString() + " on lane" + lane.ToString());
            return fResult;
#endif
        }

        public float GetBeltSpeedValue(int lane)
        {
#if true

            object tmpObj = ocx.GetFurnaceBeltSpeed(lane + 1);
            if (tmpObj == null)
                return -1;

            int iResult = (int)tmpObj;
            float fResult = iResult / 100.0F;
            LogWrite("Get belt speed PV: " + fResult.ToString() + " on lane" + lane.ToString());
            return fResult;
#else
            ocxMutex.WaitOne();
            Object tmpObj = obj.GetFurnaceBeltSpeed((short)(lane + 1));
            ocxMutex.ReleaseMutex();
            if (tmpObj == null)
            {
                return -1;
            }
            int iResult = (int)tmpObj;
            float fResult = iResult / 100.0F;
            LogWrite("Get belt speed PV: " + fResult.ToString() + " on lane" + lane.ToString());
            return fResult;
#endif
        }

        public void SetBeltSpeed(int lane, float targetSpeed)
        {
#if true
            // 27-Jan-23  01.01.09.00   MSL Discard decimal point during gets rail setpoint.
            //                              Discard decimal point during gets belt setpoint.
            targetSpeed = (float)Math.Truncate(targetSpeed);
            //ocx.SetBeltSpeed((short)(lane + beltCount - 1), targetSpeed); // 11-Jan-23  01.01.08.00   MSL Bug fix to belt speed index
            ocx.SetBeltSpeed((short)(lane + 1), targetSpeed);               // 11-Jan-23  01.01.08.00   MSL Bug fix to belt speed index
            HLog.log(HLog.eLog.EVENT, $"Set Belt Speed on lane [{lane}], target speed [{targetSpeed}]");
            LogWrite("Set Belt Speed on lane: " + lane + ", target Speed: " + targetSpeed.ToString("F1"));
#else
            ocxMutex.WaitOne();
            obj.TakeControl(2);
            int iResult = obj.SetFurnaceBeltSpeed(targetSpeed, (short)(lane + beltCount -1));
            obj.ReleaseControl(2);
            ocxMutex.ReleaseMutex();
            LogWrite("Set Belt Speed on lane: " + lane + ", target Speed: " + targetSpeed.ToString("F1") + ", Result=" + iResult.ToString());
#endif
        }

        public void SetRailWidth(int lane, float targetWidth)
        {
#if true
            short railNum = GetRailConfigNumber(lane);
            //ocx.SetRailWidth((short)(lane + beltCount - 1), targetWidth); // 11-Jan-23  01.01.06.00   MSL Bug fix to software shutdown(crashes) when change rail width.

            // 27-Jan-23  01.01.09.00   MSL Discard decimal point during gets rail setpoint.
            //                              Discard decimal point during gets belt setpoint.
            targetWidth = (float)Math.Truncate(targetWidth);
            ocx.SetRailWidth(railNum, targetWidth);       // 11-Jan-23  01.01.06.00   MSL add Get Rail confignumber for rail control.
            HLog.log(HLog.eLog.EVENT, $"Set Rail Width on lane [{lane}], Rail[{railNum}], target width [{targetWidth}]" );
            LogWrite("Set Rail Width on lane: " + lane.ToString() + ", target width: " + targetWidth.ToString());
#else
            ocxMutex.WaitOne();
            //int cbsCount = CBSExisit[lane] ? lane + 2 : 0;
            int iResult = obj.SetRailWidth((short)(lane + beltCount - 1), targetWidth);
            ocxMutex.ReleaseMutex();
            LogWrite("Set Rail Width on lane: " + lane.ToString() + ", target width: " + targetWidth.ToString() + ", Result=" + iResult.ToString());
#endif
        }

        private short GetRailConfigNumber(int lane)
        {
            short rail = 1;
            string laneRail = "-";
            try
            {
                if (lane == 0)
                    laneRail = globalParameter.Lane1Rail;
                else
                    laneRail = globalParameter.Lane2Rail;

                switch (laneRail)
                {
                    case "Rail1":
                        rail = 1;
                        break;
                    case "Rail2":
                        rail = 2;
                        break;
                    case "Rail3":
                        rail = 3;
                        break;
                    case "Rail4":
                        rail = 4;
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"forceTerminate - {ex.Message}");
            }

            return rail;
        }

        private void refreshSpeedWidth()
        {
            if (currentBeltSpeed == null)
                currentBeltSpeed = new List<float>();
            else
                currentBeltSpeed.Clear();

            if (currentBeltWidth == null)
                currentBeltWidth = new List<float>();
            else
                currentBeltWidth.Clear();

            try
            {
                // get belt speed and width setpoint
                for (int i = 0; i < 2; i++)
                {
#if true
                    // 27-Jan-23  01.01.09.00   MSL Discard decimal point during gets rail setpoint.
                    //                              Discard decimal point during gets belt setpoint.
                    currentBeltWidth.Add((float)Math.Truncate(GetRailSetPoint(i)));
                    currentBeltSpeed.Add((float)Math.Truncate(GetBeltSpeedSetPoint(i)));
#else
                    currentBeltWidth.Add(GetRailSetPoint(i));
                    currentBeltSpeed.Add(GetBeltSpeedSetPoint(i));
#endif
                }
            }
            catch (Exception ex)
            {
                LogWrite("Refresh belt speed and belt width error: " + ex.Message);
            }
        }

#endregion

#region [Button Function]


        //---------------------------------------------------------------------
        // Method btnSelectPort_Click
        //---------------------------------------------------------------------
        private void btnSelectPort_Click(object sender, EventArgs e)
        {
            BarcodeReaderConfiguration brc = new BarcodeReaderConfiguration();
            int baudRate = brc.bitRate; 
            brc.ShowDialog();
            if (brc.result == 1)
                MessageBox.Show("BitRate=" + baudRate.ToString() + " DataBits=" + brc.dataBits);
            else
                MessageBox.Show("Cancel");
        }

        //---------------------------------------------------------------------
        // Method btnQuit_Click
        //---------------------------------------------------------------------
        private void btnQuit_Click(object sender, EventArgs e)
        {
#if true    // 25-Sep-22 MSL v1.19
            HLog.log(HLog.eLog.EVENT, $"Click Quit Button");
            terminate();
#else
            DialogResult result = MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                RunFlag = false;

#if true
                if (updateThread != null)
                {
                    updateThread.Join();
                    updateThread = null;
                }

                if (upstreamThread != null)
                {
                    upstreamThread.Join();
                    upstreamThread = null;
                }

                if (downstreamThread != null)
                {
                    downstreamThread.Join();
                    downstreamThread = null;
                }
#else
                updateThread.Abort();
                upstreamThread.Abort();
                downstreamThread.Abort();
#endif
                closeOK = true;
                this.Close();

                // release smema before quit software
                for (int i = 0; i < 2; i++)
                {
                    SmemaLaneHold(i, 0);
                }
                Environment.Exit(0);
            }
#endif
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
#if true    // 25-Sep-22 MSL v1.19
            terminate();
#else
            DialogResult result = MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {


#if true
                RunFlag = false;
                Thread.Sleep(1000);

                if (updateThread != null)
                    updateThread.Join();

                if (upstreamThread != null)
                    upstreamThread.Join();

                if (upstreamThread != null)
                    upstreamThread.Join();

#else
                updateThread.Abort();
                upstreamThread.Abort();
                downstreamThread.Abort();
#endif

                closeOK = true;

                // 25-Sep-22 MSL v1.19
#if true
                switch (globalParameter.PLCType)
                {
                    case globalParameter.ePLCType.None:
                        break;
                    case globalParameter.ePLCType.OMRON:
                        break;
                    case globalParameter.ePLCType.Mitsubishi:
                        {
                            dotUtlType1.Close();
                        }
                        break;
                    default:
                        break;
                }
#else

                if (globalParameter.PLCType == "Mitsubishi")
                {
                    dotUtlType1.Close();
                }
#endif
                // release smema before quit software
                for (int i = 0; i < 2; i++)
                {
                    SmemaLaneHold(i, 0);
                }
                Environment.Exit(0);
            }
#endif
        }

        private void terminate()
        {
            // 25-Sep-22 MSL v1.19
            DialogResult result = MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {

                HLog.log(HLog.eLog.EVENT, $"Starting terminate process");
#if true
                RunFlag = false;

                Task.Factory.StartNew(() => { forceTerminate(); });

                if (updateThread != null)
                {
                    updateThread.Join();
                    HLog.log(HLog.eLog.EVENT, $"terminate [updateThread - Join]");
                }

                if (upstreamThread != null)
                {
                    upstreamThread.Join();
                    HLog.log(HLog.eLog.EVENT, $"terminate [upstreamThread - Join]");
                }

                if (downstreamThread != null)
                {
                    downstreamThread.Join();
                    HLog.log(HLog.eLog.EVENT, $"terminate [downstreamThread - Join]");
                }

#else
                updateThread.Abort();
                upstreamThread.Abort();
                downstreamThread.Abort();
#endif

                closeOK = true;

                // 25-Sep-22 MSL v1.19
#if true
                switch (globalParameter.PLCType)
                {
                    case globalParameter.ePLCType.None:
                        break;
                    case globalParameter.ePLCType.OMRON:
                        break;
                    case globalParameter.ePLCType.Mitsubishi:
                        {
                            //dotUtlType1.Close();
                            if (UpstreamMxPlc != null)
                            {
                                UpstreamMxPlc.Close();
                                UpstreamMxPlc = null;
                                HLog.log(HLog.eLog.EVENT, $"terminate() [UpstreamMxPlc - Close]");
                            }

                            if (DownstreamMxPlc != null)
                            {
                                DownstreamMxPlc.Close();
                                DownstreamMxPlc = null;
                                HLog.log(HLog.eLog.EVENT, $"terminate() [DownstreamMxPlc - Close]");
                            }
                        }
                        break;
                    default:
                        break;
                }
#else

                if (globalParameter.PLCType == "Mitsubishi")
                {
                    dotUtlType1.Close();
                }
#endif
                // release smema before quit software
                for (int i = 0; i < 2; i++)
                {
                    SmemaLaneHold(i, 0);
                    HLog.log(HLog.eLog.EVENT, $"terminate [SmemaLaneHold] [{(globalParameter.eLane)i}] [Release]");
                }

                HLog.log(HLog.eLog.EVENT, $"Finish terminate process");
                Environment.Exit(0);
            }
        }

        int threadJoinCount = 0;
        private void forceTerminate()
        {
            HLog.log(HLog.eLog.EVENT, $"Starting forceTerminate process - Wait 1500ms");
            while (15 > threadJoinCount)
            {
                threadJoinCount++;
                Thread.Sleep(100);
            }

            HLog.log(HLog.eLog.EVENT, $"Starting forceTerminate process - Done 1500ms");
            try
            {
                // release smema before quit software
                for (int i = 0; i < 2; i++)
                {
                    SmemaLaneHold(i, 0);
                    HLog.log(HLog.eLog.EVENT, $"forceTerminate [SmemaLaneHold] [{(globalParameter.eLane)i}] [Release]");
                }

                if (updateThread != null)
                {
                    updateThread.Abort();
                    HLog.log(HLog.eLog.EVENT, $"forceTerminate [updateThread - Abort]");
                }

                if (upstreamThread != null)
                {
                    upstreamThread.Abort();
                    HLog.log(HLog.eLog.EVENT, $"forceTerminate [upstreamThread - Abort]");
                }

                if (downstreamThread != null)
                {
                    downstreamThread.Abort();
                    HLog.log(HLog.eLog.EVENT, $"forceTerminate [downstreamThread - Abort]");
                }

                closeOK = true;

                switch (globalParameter.PLCType)
                {
                    case globalParameter.ePLCType.None:
                        break;
                    case globalParameter.ePLCType.OMRON:
                        break;
                    case globalParameter.ePLCType.Mitsubishi:
                        {
                            //dotUtlType1.Close();
                            if (UpstreamMxPlc != null)
                            {
                                UpstreamMxPlc.Close();
                                UpstreamMxPlc = null;
                                HLog.log(HLog.eLog.EVENT, $"forceTerminate [UpstreamMxPlc - Close]");
                            }

                            if (DownstreamMxPlc != null)
                            {
                                DownstreamMxPlc.Close();
                                DownstreamMxPlc = null;
                                HLog.log(HLog.eLog.EVENT, $"forceTerminate [DownstreamMxPlc - Close]");
                            }
                        }
                        break;
                    default:
                        break;
                }

                
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"forceTerminate - {ex.Message}");
            }

        }

        //---------------------------------------------------------------------
        // Method btnConnect_Click
        //---------------------------------------------------------------------
        private void btnConnect_Click(object sender, EventArgs e)
        {
            //if (barcodeSocketConnected)
            //{
            //    // disconnect from barcode program
            //    barcodeSocket.Disconnect(true);

            //    // close socket
            //    barcodeSocket.Close();

            //    // flag disconnected
            //    barcodeSocketConnected = false;
            //    btnConnect.Text = "Connect";
            //}
            //else
            //{
            //    // create socket
            //    try
            //    {
            //        barcodeSocket = new Socket(
            //                            System.Net.Sockets.AddressFamily.InterNetwork,
            //                            System.Net.Sockets.SocketType.Stream,
            //                            System.Net.Sockets.ProtocolType.Tcp);
            //    }
            //    catch (Exception e2)
            //    {
            //        MessageBox.Show("Socket create exception " + e2.Message);
            //        return;
            //    }

            //    // connect to barcode program
            //    try
            //    {
            //        barcodeSocket.Connect(barcodeSocketIP, (UInt16)barcodeSocketPort);
            //    }
            //    catch (Exception e2)
            //    {
            //        MessageBox.Show("Socket connect " + barcodeSocketIP + ":" + barcodeSocketPort.ToString() + " exception " + e2.Message);
            //        return;
            //    }

            //    // flag connected
            //    barcodeSocketConnected = true;
            //    btnConnect.Text = "Disconnect";
            //}

            // daniel new added, always disconnect with barcode program.
            //btnConnect.Text = "Disconnect";
            BarcodeSetting barcodeSetting = BarcodeSetting.AddFormInstance();
            if (!barcodeSetting.Visible)
            {
                barcodeSetting.StartPosition = FormStartPosition.CenterParent;
                barcodeSetting.TopMost = true;
                barcodeSetting.Show();
            }

        }

        private void btnBarcodeSetting_Click(object sender, EventArgs e)
        {
            BarcodeSetting barcodeSetting = BarcodeSetting.AddFormInstance();
            if (!barcodeSetting.Visible)
            {
                barcodeSetting.StartPosition = FormStartPosition.CenterParent;
                barcodeSetting.TopMost = true;
                barcodeSetting.Show();
            }

        }

        //---------------------------------------------------------------------
        // Method btnSelectBarcode_Click
        //---------------------------------------------------------------------
        private void btnSelectBarcode_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    plcOutputData[0] = Convert.ToInt32(tbBarcodeSelect.Text);
            //}
            //catch (Exception e2)
            //{
            //    plcOutputData[0] = 0;
            //}
        }

        //---------------------------------------------------------------------
        // Method btnStartComm_Click
        //---------------------------------------------------------------------
        private void btnStartComm_Click(object sender, EventArgs e)
        {
            switch (globalParameter.PLCType)
            {
                case globalParameter.ePLCType.None:
                    break;
                case globalParameter.ePLCType.OMRON:
                    {
                        if (plcCommEnable)
                        {
                            plcCommEnable = false;
                            btnStartComm.Text = "Start Comm";
                        }
                        else
                        {
                            plcCommEnable = true;
                            btnStartComm.Text = "Stop Comm";
                        }
                    }
                    break;
                case globalParameter.ePLCType.Mitsubishi:
                    {

                    }
                    break;
                default:
                    break;
            }

        }

        //---------------------------------------------------------------------
        // Method btnSaveConfig_Click
        //---------------------------------------------------------------------
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            // Move to PlcSetupWindow
            //try
            //{
            //    // 23-Sep-22 MSL v1.19
            //    UseConfigFile.SetBoolConfigurationSetting("HoldSmemaBarcode", globalParameter.holdSmemaUntilBarcode);
            //    UseConfigFile.SetBoolConfigurationSetting("AutoBarcodeRecipe", globalParameter.autoChangeRecipeWidthSpeed);

            //    UseConfigFile.SetStringConfigurationSetting("PLCType", combo_PLCType.SelectedItem.ToString());
            //    int stationNumber = 0;
            //    bool bStationNumber = Int32.TryParse(tbPlcStation.Text, out stationNumber);
            //    if (bStationNumber)
            //    {
            //        UseConfigFile.SetIntConfigurationSetting("PLCStation", stationNumber);
            //    }
            //    else
            //    {
            //        tbPlcStation.Text = globalParameter.UpstreamMxPlcStation.ToString();
            //        MessageBox.Show("Incorrect station number, please retry enter station number.");
            //    }

            //    globalParameter.Lane1Rail = cb_EHC1.SelectedItem.ToString();
            //    UseConfigFile.SetStringConfigurationSetting("Lane1Rail", globalParameter.Lane1Rail);

            //    globalParameter.Lane2Rail = cb_EHC2.SelectedItem.ToString();
            //    UseConfigFile.SetStringConfigurationSetting("Lane2Rail", globalParameter.Lane2Rail);

            //    UseConfigFile.SetStringConfigurationSetting("UpstreamPLCTag", tbUpstreamPLCTag.Text);
            //    UseConfigFile.SetStringConfigurationSetting("DownstreamPLCTag", tbDownstreamPLCTag.Text);
            //    UseConfigFile.SetStringConfigurationSetting("TagIP", tbTagIP.Text);

            //    if (radioButtonRail2.Checked)
            //        UseConfigFile.SetIntConfigurationSetting("DefaultRail", 1);
            //    else if (radioButtonRail3.Checked)
            //        UseConfigFile.SetIntConfigurationSetting("DefaultRail", 2);
            //    else if (radioButtonRail4.Checked)
            //        UseConfigFile.SetIntConfigurationSetting("DefaultRail", 3);
            //    else
            //        UseConfigFile.SetIntConfigurationSetting("DefaultRail", 0);

            //    UseConfigFile.SetStringConfigurationSetting("LogFilePath", tbLogFilesFolder.Text);

            //    UseConfigFile.SetBoolConfigurationSetting("RailLogging", globalParameter.RailLogging);

            //    MessageBox.Show("Saved config file. Please restart this application to apply changes.");
            //}
            //catch (UnauthorizedAccessException e2)
            //{
            //    MessageBox.Show("Save configuration failure. Restart as Administrator");
            //}
            //catch (Exception e2)
            //{
            //    MessageBox.Show("Save configuration exception " + e2.Message);
            //}
        }

        //---------------------------------------------------------------------
        // Method formClosing
        //---------------------------------------------------------------------
        public void formClosing(object sender, FormClosingEventArgs e)
        {
            if (closeOK == false)
                e.Cancel = true;
        }

        private void btn_map_barcode_Click(object sender, EventArgs e)
        {
            BarcodeMappingTable barcodeMap = new BarcodeMappingTable();
            barcodeMap.ShowDialog();
        }

        private void btn_PlcSettings_Click(object sender, EventArgs e)
        {
            PlcSetupWindow setup = new PlcSetupWindow();
            setup.ShowDialog();
        }

#endregion

#region [Log]

        public void LogWrite(string msg)
        {
            if (!Directory.Exists(globalParameter.debugLogFolder))
            {
                Directory.CreateDirectory(globalParameter.debugLogFolder);
            }
            logMutex.WaitOne();
            try
            {
                DateTime now = DateTime.Now;
                msg = now.Year.ToString("D4") + "-" + now.Month.ToString("D2") + "-" + now.Day.ToString("D2") + " " +
                    now.Hour.ToString("D2") + ":" + now.Minute.ToString("D2") + ":" + now.Second.ToString("D2") + "." + now.Millisecond.ToString("D3") + " " +
                    msg;
                StreamWriter writer = new StreamWriter(globalParameter.debugLogFolder + "\\" +
                    now.Year.ToString("D4") + now.Month.ToString("D2") + now.Day.ToString("D2") +
                    "_AsinLineDebug.log", true);
                writer.WriteLine(msg);
                writer.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("HellerInterfaceWebServer::LogWrite failure=" + e.Message);
            }
            logMutex.ReleaseMutex();
        }
#endregion

#region [No more used]
        AxHELLERCOMMLib.AxHellerComm obj;
        // private AxActProgTypeLib.AxActProgType axActProgType1;
        // private DotUtlType dotUtlType1;
        // Thread waitForOvenEmptyToLoadRecipe = null;
        const String configurationKey = "HKEY_LOCAL_MACHINE\\Software\\Heller Industries\\HC2\\BarcodeReader";
        Mutex ocxMutex;
        Thread updateThread;
        int updatePeriod;
        int upstreamPLCPeriod;
        int downstreamPLCPeriod;
        //byte[] tcpMsgData;
        //Socket barcodeSocket;
        //Boolean barcodeSocketConnected;
        //String upstreamPLCTag;
        //String downstreamPLCTag;
        //String logFilesFolder;
        //String barcodeSocketIP;
        //String tagIP;
        int barcodeSocketPort;
        private bool[] CBSExisit = new bool[] { false, false };

        float rail1Width;
        float rail2Width;
        float rail3Width;
        float rail4Width;


        const int LIGHT_TOWER_OFF = 0;
        const int LIGHT_TOWER_RED = 1;
        const int LIGHT_TOWER_AMBER = 2;
        const int LIGHT_TOWER_GREEN = 4;

        const int NOTIFICATION_EVENT_LIGHT_TOWER_CHANGE = 30; //ver1.0.66, compatible HC2 software version 8.0.0.42
        const int NOTIFICATION_EVENT_JOB_CHANGE = 40;
        const int NOTIFICATION_EVENT_BOARD_ENTERED = 60;
        const int NOTIFICATION_EVENT_BOARD_EXITED = 61;
        const int NOTIFICATION_EVENT_LANE1_SMEMA_ENTRY_BA = 71;
        const int NOTIFICATION_EVENT_LANE2_SMEMA_ENTRY_BA = 72;
        const int NOTIFICATION_EVENT_LANE3_SMEMA_ENTRY_BA = 73;
        const int NOTIFICATION_EVENT_LANE4_SMEMA_ENTRY_BA = 74;
        const int NOTIFICATION_EVENT_LANE1_BOARD_ENTRY = 100;
        const int NOTIFICATION_EVENT_LANE2_BOARD_ENTRY = 101;
        const int NOTIFICATION_EVENT_LANE3_BOARD_ENTRY = 102;
        const int NOTIFICATION_EVENT_LANE4_BOARD_ENTRY = 103;
        const int NOTIFICATION_EVENT_LANE1_BOARD_EXIT = 104;
        const int NOTIFICATION_EVENT_LANE2_BOARD_EXIT = 105;
        const int NOTIFICATION_EVENT_LANE3_BOARD_EXIT = 106;
        const int NOTIFICATION_EVENT_LANE4_BOARD_EXIT = 107;

        //---------------------------------------------------------------------
        // Method UpdateThread
        //---------------------------------------------------------------------
        private void UpdateThread()
        {
            // forever
            //while (true)
            while (RunFlag)
            {
                // query light tower color
                short lightTowerColor = obj.GetCurrentLightTowerColor();
                switch (lightTowerColor)
                {
                    case LIGHT_TOWER_OFF:
                        tbLightTowerColor.Text = "Off";
                        break;
                    case LIGHT_TOWER_RED:
                        tbLightTowerColor.Text = "Red";
                        break;
                    case LIGHT_TOWER_AMBER:
                        tbLightTowerColor.Text = "Amber";
                        break;
                    case LIGHT_TOWER_GREEN:
                        tbLightTowerColor.Text = "Green";
                        break;
                    default:
                        tbLightTowerColor.Text = lightTowerColor.ToString("0");
                        break;
                }

                // query rail width process values
                float railWidth;
                Object variant = obj.GetRailWidth(1);
                if (variant == null)
                    railWidth = -1;
                else
                    railWidth = (float)variant;
                tbRail1WidthPV.Text = railWidth.ToString();
                variant = obj.GetRailWidth(2);
                if (variant == null)
                    railWidth = -1;
                else
                    railWidth = (float)variant;
                tbRail2WidthPV.Text = railWidth.ToString();
                variant = obj.GetRailWidth(3);
                if (variant == null)
                    railWidth = -1;
                else
                    railWidth = (float)variant;
                tbRail3WidthPV.Text = railWidth.ToString();
                variant = obj.GetRailWidth(4);
                if (variant == null)
                    railWidth = -1;
                else
                    railWidth = (float)variant;
                tbRail4WidthPV.Text = railWidth.ToString();

                // query rail width setpoints
                variant = obj.GetRailWidth(101);
                railWidth = (float)variant;
                if (railWidth <= 1000.0)
                    rail1Width = railWidth;
                tbRail1WidthSP.Text = railWidth.ToString();
                variant = obj.GetRailWidth(102);
                railWidth = (float)variant;
                if (railWidth <= 1000.0)
                    rail2Width = railWidth;
                tbRail2WidthSP.Text = railWidth.ToString();
                variant = obj.GetRailWidth(103);
                railWidth = (float)variant;
                if (railWidth <= 1000.0)
                    rail3Width = railWidth;
                tbRail3WidthSP.Text = railWidth.ToString();
                variant = obj.GetRailWidth(104);
                railWidth = (float)variant;
                if (railWidth <= 1000.0)
                    rail4Width = railWidth;
                tbRail4WidthSP.Text = railWidth.ToString();

                // query boards processed count (49..51,54)
                float beltProcessed = obj.GetChannel(49);
                tbBelt1Processed.Text = beltProcessed.ToString();
                beltProcessed = obj.GetChannel(50);
                tbBelt2Processed.Text = beltProcessed.ToString();
                beltProcessed = obj.GetChannel(51);
                tbBelt3Processed.Text = beltProcessed.ToString();
                beltProcessed = obj.GetChannel(54);
                tbBelt4Processed.Text = beltProcessed.ToString();

                // query boards in oven count (46..48,53)
                beltProcessed = obj.GetChannel(46);
                tbBelt1InOven.Text = beltProcessed.ToString();
                beltProcessed = obj.GetChannel(47);
                tbBelt2InOven.Text = beltProcessed.ToString();
                beltProcessed = obj.GetChannel(48);
                tbBelt3InOven.Text = beltProcessed.ToString();
                beltProcessed = obj.GetChannel(53);
                tbBelt4InOven.Text = beltProcessed.ToString();

                // update barcode data
                String barcodeTextLane1 = "";
                String barcodeTextLane2 = "";
                Array.Clear(lane1BarcodeByte, 0, BARCODE_MAX);
                Array.Clear(lane2BarcodeByte, 0, BARCODE_MAX);

                for (int ndx = 0; ndx < BARCODE_MAX; ndx++)
                {
                    byte barcodeDigit;
                    switch (globalParameter.PLCType)
                    {
                        case globalParameter.ePLCType.None:
                            break;
                        case globalParameter.ePLCType.OMRON:
                            {
                                // Big endian
                                if ((ndx & 1) == 1)
                                    barcodeDigit = (byte)(plcInputData[ndx / 2] & 0xFF);
                                else
                                    barcodeDigit = (byte)(plcInputData[ndx / 2] >> 8 & 0xFF);
                                barcodeTextLane1 += barcodeDigit.ToString("X2") + " ";
                                lane1BarcodeByte[ndx] = barcodeDigit;
                            }
                            break;
                        case globalParameter.ePLCType.Mitsubishi:
                            {
                                // Little endian
                                if ((ndx & 1) == 0)
                                    barcodeDigit = (byte)(plcInputData[ndx / 2] & 0xFF);
                                else
                                    barcodeDigit = (byte)(plcInputData[ndx / 2] >> 8 & 0xFF);
                                barcodeTextLane1 += barcodeDigit.ToString("X2") + " ";
                                lane1BarcodeByte[ndx] = barcodeDigit;
                            }
                            break;
                        default:
                            break;
                    }
                }

                //for (int ndx = 100; ndx < BARCODE_MAX; ndx++)
                //{
                //    byte barcodeDigit;
                //    if ((ndx & 1) == 1)
                //        barcodeDigit = (byte)(plcInputData[ndx / 2] & 0xFF);
                //    else
                //        barcodeDigit = (byte)(plcInputData[ndx / 2] >> 8 & 0xFF);
                //    barcodeTextLane2 += barcodeDigit.ToString("X2") + " ";
                //}

                int startIndex = 100;
                for (int ndx = startIndex; ndx < startIndex + BARCODE_MAX; ndx++)
                {
                    byte barcodeDigit;

                    switch (globalParameter.PLCType)
                    {
                        case globalParameter.ePLCType.None:
                            break;
                        case globalParameter.ePLCType.OMRON:
                            {
                                // Big endian
                                if ((ndx & 1) == 1)
                                    barcodeDigit = (byte)(plcInputData[startIndex + (ndx - startIndex) / 2] & 0xFF);
                                else
                                    barcodeDigit = (byte)(plcInputData[startIndex + (ndx - startIndex) / 2] >> 8 & 0xFF);
                                //if (barcodeDigit == 0)
                                //    break;
                                barcodeTextLane2 += barcodeDigit.ToString("X2") + " ";
                                lane2BarcodeByte[ndx - startIndex] = barcodeDigit;
                            }
                            break;
                        case globalParameter.ePLCType.Mitsubishi:
                            {
                                // Little endian
                                if ((ndx & 1) == 0)
                                    barcodeDigit = (byte)(plcInputData[startIndex + (ndx - startIndex) / 2] & 0xFF);
                                else
                                    barcodeDigit = (byte)(plcInputData[startIndex + (ndx - startIndex) / 2] >> 8 & 0xFF);
                                //if (barcodeDigit == 0)
                                //    break;
                                barcodeTextLane2 += barcodeDigit.ToString("X2") + " ";
                                lane2BarcodeByte[ndx - startIndex] = barcodeDigit;
                            }
                            break;
                        default:
                            break;
                    }
                }

                tbBarcodeLane1.Text = barcodeTextLane1;
                tbBarcodeLane2.Text = barcodeTextLane2;

                tbBarcodeLane1string.Text = GetBarcodeString(lane1BarcodeByte);
                tbBarcodeLane2string.Text = GetBarcodeString(lane2BarcodeByte);

                // update BA signal data for lane 1
                tbBASignalLane1.Text = plcInputData[BA_SIGNAL_ARRAY_NDX_LANE1].ToString();
                // update BA signal data for lane 2
                tbBASignalLane2.Text = plcInputData[BA_SIGNAL_ARRAY_NDX_LANE2].ToString();

                // snooze
                Thread.Sleep(globalParameter.UpdatePeriod);

            }
        }


        //---------------------------------------------------------------------
        // Method InitializeOCX
        //
        // Instantiate and start iMagic ActiveX object
        //---------------------------------------------------------------------
        private void InitializeOCX()
        {
            // not recommended
            Control.CheckForIllegalCrossThreadCalls = false;
            logMutex = new Mutex();
            LogWrite("ASIN line software: " + revision);

            // access Heller COMM Control ActiveX
            obj = new AxHELLERCOMMLib.AxHellerComm();
            obj.CreateControl();
            bool result = obj.StartCommunicating(1);
            if (!result)
                MessageBox.Show("StartCommunicating failure");
            result = obj.StartOven();
            if (!result)
                MessageBox.Show("StartOven failure");

            // hook notification events
            obj.NotificationEvent += new AxHELLERCOMMLib._DHellerCommEvents_NotificationEventEventHandler(this.NotificationEvent);

            // initialize buffers
            plcInputData = new Int32[PLC_MEMORY_MAX];
            Array.Clear(plcInputData, 0, PLC_MEMORY_MAX);
            plcOutputData = new Int32[PLC_MEMORY_MAX];
            Array.Clear(plcOutputData, 0, PLC_MEMORY_MAX);
            barcodeData = new byte[BARCODE_MAX];
            Array.Clear(barcodeData, 0, BARCODE_MAX);
            //tcpMsgData = new byte[TCP_MSG_MAX];
            //Array.Clear(tcpMsgData, 0, TCP_MSG_MAX);

            lane1BarcodeData = new byte[TCP_MSG_MAX];
            Array.Clear(lane1BarcodeData, 0, TCP_MSG_MAX);
            lane2BarcodeData = new byte[TCP_MSG_MAX];
            Array.Clear(lane2BarcodeData, 0, TCP_MSG_MAX);

            lane1BarcodeByte = new byte[BARCODE_MAX];
            Array.Clear(lane1BarcodeByte, 0, BARCODE_MAX);
            lane2BarcodeByte = new byte[BARCODE_MAX];
            Array.Clear(lane2BarcodeByte, 0, BARCODE_MAX);

            // initialize booleans
            // barcodeSocketConnected = false;
            BASignal = false;
            plcCommEnable = true;
            closeOK = false;
            barcodeRecipeEmptyDisplayed = false;

            // initialize other
            rail1Width = 0;
            rail2Width = 0;
            rail3Width = 0;
            rail4Width = 0;

            ocxMutex = new Mutex();
#if false   // 25-Sep-22 MSL v1.19
            globalParameter.autoChangeRecipeWidthSpeed = Properties.Settings.Default.AutoBarcodeRecipe;
            globalParameter.holdSmemaUntilBarcode = Properties.Settings.Default.HoldSmemaBarcode;
#endif
            // hold smema for all lanes
            if (globalParameter.holdSmemaUntilBarcode)
            {
                for (int i = 0; i < 2; i++)
                {
                    SmemaLaneHold(i, 1);
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    SmemaLaneHold(i, 0);
                }
            }

            // read barcode recipe mapping table.
            globalFunctions.initializeBarcodeRecipeTable();
            initializeBeltSpeedCountAndCBS();

            // initialize mutex
            plcMutex = new Mutex();

            // read configuration
            //tbUpstreamPLCTag.Text = upstreamPLCTag = (String)Registry.GetValue(configurationKey, "UpstreamPLCTag", "RE1inAMCV1");
            //tbDownstreamPLCTag.Text = downstreamPLCTag = (String)Registry.GetValue(configurationKey, "DownstreamPLCTag", "RE1outAMCV1");
            //tbTagIP.Text = tagIP = (String)Registry.GetValue(configurationKey, "TagIP", "192.168.241.9");

            // 25-Sep-22 MSL v1.19 - Move to LoadConfigurationSettings()
            //tbLogFilesFolder.Text = globalParameter.debugLogFolder = Properties.Settings.Default.LogFilePath;


#if false   // 25-Sep-22 MSL v1.19 - Move to LoadConfigurationSettings()
            combo_PLCType.Text = globalParameter.PLCType = Properties.Settings.Default.PLCType; // 25-Sep-22 MSL v1.19
#endif
            //barcodeSocketIP = (String)Registry.GetValue(configurationKey, "IP", "127.0.0.1");



            try
            {
                barcodeSocketPort = (int)Registry.GetValue(configurationKey, "Port", 31000);
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, ex.Message);
                barcodeSocketPort = 31000;
            }
            try
            {
                updatePeriod = (int)Registry.GetValue(configurationKey, "UpdatePeriod", 2000);
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, ex.Message);
                updatePeriod = 2000;
            }
            try
            {
                upstreamPLCPeriod = (int)Registry.GetValue(configurationKey, "UpstreamPLCPeriod", 1000);
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, ex.Message);
                upstreamPLCPeriod = 1000;
            }
            try
            {
                downstreamPLCPeriod = (int)Registry.GetValue(configurationKey, "DownstreamPLCPeriod", 1000);
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, ex.Message);
                downstreamPLCPeriod = 1000;
            }
            try
            {
                defaultRail = (int)Registry.GetValue(configurationKey, "DefaultRail", 0);
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, ex.Message);
                defaultRail = 0;
            }
            try
            {
                //string strLog = (string)Registry.GetValue(configurationKey, "RailLogging", "false");
                //switch (strLog)
                //{
                //    case "true":
                //        railLogging = true;
                //        break;
                //    case "false":
                //        railLogging = false;
                //        break;
                //    default:
                //        railLogging = false;
                //        break;
                //}

                //railLogging = (Boolean)Registry.GetValue(configurationKey, "RailLogging", false);
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, ex.Message);
                //railLogging = false;
            }

            //switch (defaultRail)
            //{
            //    case 0:
            //        radioButtonRail1.Checked = true;
            //        radioButtonRail2.Checked = false;
            //        radioButtonRail3.Checked = false;
            //        radioButtonRail4.Checked = false;
            //        break;
            //    case 1:
            //        radioButtonRail1.Checked = false;
            //        radioButtonRail2.Checked = true;
            //        radioButtonRail3.Checked = false;
            //        radioButtonRail4.Checked = false;
            //        break;
            //    case 2:
            //        radioButtonRail1.Checked = false;
            //        radioButtonRail2.Checked = false;
            //        radioButtonRail3.Checked = true;
            //        radioButtonRail4.Checked = false;
            //        break;
            //    case 3:
            //        radioButtonRail1.Checked = false;
            //        radioButtonRail2.Checked = false;
            //        radioButtonRail3.Checked = false;
            //        radioButtonRail4.Checked = true;
            //        break;
            //}



            // start display update thread
            updateThread = new Thread(new ThreadStart(UpdateThread));
            updateThread.Start();

            // 23-Sep-22 MSL v1.19
#if true
            // Create instance
            switch (globalParameter.PLCType)
            {
                case globalParameter.ePLCType.None:
                    {
                        MessageBox.Show("Please setup to PLC Type");
                    }
                    break;
                case globalParameter.ePLCType.OMRON:
                    {
                        compolet = new OMRON.Compolet.CIP.CJ2Compolet();
                        //start downstream PLC thread
                        UpDownstreamThread();
                    }
                    break;
                case globalParameter.ePLCType.Mitsubishi:
                    {
                        //lbPlcStation.Visible = true;
                        //tbPlcStation.Visible = true;

                        //tbPlcStation.Text = globalParameter.UpstreamMxPlcStation.ToString();
                        int station = globalParameter.UpstreamMxPlcStation;
                        //dotUtlType1 = new DotUtlType() { ActLogicalStationNumber = station };
                        //dotUtlType1.Open();
                        UpDownstreamThread();
                    }
                    break;
                default:
                    break;
            }
#else
            if (globalParameter.PLCType != "None")
            {
                if (globalParameter.PLCType == "Mitsubishi")
                {
                    dotUtlType1.Open();
                }

                // start downstream PLC thread
                downstreamThread = new Thread(new ThreadStart(DownstreamThread));
                downstreamThread.Start();

                // start upstream PLC thread
                upstreamThread = new Thread(new ThreadStart(UpstreamThread));
                upstreamThread.Start();
            }
#endif

        }

        private void initializeBeltSpeedCountAndCBS()
        {
            beltCount = GetBeltCount();
            for (int i = 0; i < 2; i++)
            {
                CBSExisit[i] = CheckCBSExist(i);
            }

            // double check CBS
            if (GetRailSetPoint(2) > 0)
                CBSExisit[0] = true;
            if (GetRailSetPoint(3) > 0)
                CBSExisit[1] = true;
        }

        private void NotificationEvent(object sender, AxHELLERCOMMLib._DHellerCommEvents_NotificationEventEvent e)
        {
            //int iResult = 0;
            //float fResult = 0.0F;
            // string sResult = null;
            int[] iArg = new int[3];        //1.0.82 extend 2 -> 3 
            string[] sArg = new string[1];
            // int lane;

            // switch on type of notification
            switch (e.lEventID)
            {
                case NOTIFICATION_EVENT_BOARD_ENTERED:
                    break;
                case NOTIFICATION_EVENT_BOARD_EXITED:
                    break;
                case NOTIFICATION_EVENT_JOB_CHANGE:
                    break;
                case NOTIFICATION_EVENT_LANE1_BOARD_ENTRY:
                    {
                        // hold smema for lane1 again
                        SmemaLaneHold(0, 1);
                        LogWrite("Board entered on lane1, hold smema again.");
                    }
                    break;
                case NOTIFICATION_EVENT_LANE2_BOARD_ENTRY:
                    {
                        // hold smema for lane2 again
                        SmemaLaneHold(1, 1);
                        LogWrite("Board eentered on lane2, hold smema again");
                    }
                    break;
                case NOTIFICATION_EVENT_LANE3_BOARD_ENTRY:
                    break;
                case NOTIFICATION_EVENT_LANE4_BOARD_ENTRY:
                    break;
                case NOTIFICATION_EVENT_LANE1_BOARD_EXIT:
                    break;
                case NOTIFICATION_EVENT_LANE2_BOARD_EXIT:
                    break;
                case NOTIFICATION_EVENT_LANE3_BOARD_EXIT:
                    break;
                case NOTIFICATION_EVENT_LANE4_BOARD_EXIT:
                    break;
            }
        }



#endregion
    }
}
