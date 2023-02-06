//-----------------------------------------------------------------------------
// OcxWrappercs.cs -- OcxWrappercs
//
// Author: MS Lee
// E-mail: mslee@hellerindustries.co.kr
// Tel:
//
// Edit History:
//
// 07-Nov-22  01.01.01.00   MSL Improvement ocx thread.
// 11-Jan-23  01.01.07.00   MSL Remove take control when change SP.
// 11-Jan-23  01.01.08.00   MSL restore remove take control
// 27-Jan-23  01.01.09.00   MSL Discard decimal point during gets rail setpoint.
//                              Discard decimal point during gets belt setpoint.
// 07-Feb-23  01.01.10.00   MSL Discard more than one decimal place during gets rail setpoint.
//-----------------------------------------------------------------------------
using AISIN_WFA.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AISIN_WFA.Models
{

    public class OcxWrappercs
    {
        #region [Member]

        private AxHELLERCOMMLib.AxHellerComm ocx = null;
        private ReaderWriterLockSlim lockOcx = new ReaderWriterLockSlim();
        internal bool IsTakenControl { get; set; } = false;
        private bool IsIntialSPUpdateFinished = false;
        private object variant = new object();
        public OvenState Oven = new OvenState();
        public event OcxUIupdate UpdateUiEventHandler;
        public event OcxUIupdate UpdateHc2ConnectEventHandler;
        public string Lane1Barcode { get; set; } = string.Empty;
        public string Lane2Barcode { get; set; } = string.Empty;
        #endregion

        #region [Enum]
        private enum eOCXEventID : int
        {
            ALARM_ID = 20,
            ALARM_MESSAGE = 21,
            ALARM_ACK = 22,
            LIGHT_TOWER_CHANGE = 30,
            JOB_CHANGE = 40,
            HEAT_SP_CHANGE = 51,
            BOARD_ENTERED = 60,
            BOARD_EXITED = 61,
            LANE1_SMEMA_ENTRY_BA = 71,  //
            LANE2_SMEMA_ENTRY_BA = 72,  //
            LANE3_SMEMA_ENTRY_BA = 73,  //
            LANE4_SMEMA_ENTRY_BA = 74,  //
            BELT0_OPENLOOP = 81,        //belt speed setpoint changed
            BELT0_CLOSEDLOOP = 82,
            BELT1_OPENLOOP = 83,
            BELT1_CLOSEDLOOP = 84,
            LANE1_BOARD_ENTRY = 100,
            LANE2_BOARD_ENTRY = 101,
            LANE3_BOARD_ENTRY = 102,
            LANE4_BOARD_ENTRY = 103,
            LANE1_BOARD_EXIT = 104,
            LANE2_BOARD_EXIT = 105,
            LANE3_BOARD_EXIT = 106,
            LANE4_BOARD_EXIT = 107,
        }

        private enum eLightTowerState
        {
            LIGHT_TOWER_OFF = 0,
            LIGHT_TOWER_RED = 1,
            LIGHT_TOWER_AMBER = 2,
            LIGHT_TOWER_GREEN = 4
        }
        #endregion

        #region [Connection]
        public bool InitWrapper()
        {
            if (!CheckOvenAlive())
            {
                HLog.log(HLog.eLog.ERROR, "Go To Shutdown. Cannot find HC2 Oven Operating Program!");
                return false;
            }

            //Initialize OCX.
            bool isSuccess = true;

            //bool bTakeLock = false;
            try
            {
                Thread.CurrentThread.SetApartmentState(ApartmentState.STA);

                ocx = new AxHELLERCOMMLib.AxHellerComm();
                ocx.CreateControl();
                isSuccess = ocx.StartCommunicating(1);
                if (isSuccess)
                {
                    isSuccess = ocx.StartOven();
                    //[07 - Nov - 22  01.01.01.00   MSL Improvement ocx thread. - Start]
                    ocx.NotificationEvent += OCXWrapper_NotificationEvent;
                    ocx.UpdateChannelParam += OCXWrapper_UpdateChannelParam;

                    // Start Get from ocx
                    Task.Factory.StartNew(() => { ThreadPolling(); });
                    //[07 - Nov - 22  01.01.01.00   MSL Improvement ocx thread. - End]
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("InitWrapper -- Message: {0}", ex.Message));
            }

            if (!isSuccess) return false;

            return true;
        }

        private bool CheckOvenAlive()
        {
            int handle = 0;
            handle = Util.FindWindow("CHellerMainFrame", null);
            if (handle != 0)
            {
                if (Oven.IsConnectWithHC2 != true)
                {
                    Oven.IsConnectWithHC2 = true;
                    UpdateHc2ConnectEventHandler.BeginInvoke(null, null);
                }
            }
            else
            {
                if (Oven.IsConnectWithHC2 != false)
                {
                    Oven.IsConnectWithHC2 = false;
                    UpdateHc2ConnectEventHandler.BeginInvoke(null, null);
                }
            }
            return handle != 0;
        }
        #endregion

        #region [Thread]
        private void ThreadPolling()
        {
            while(true)
            {
                try
                {
                    if (!CheckOvenAlive())
                    {
                        Oven.IsConnectWithHC2 = false;
                        continue;
                    }
                    Oven.IsConnectWithHC2 = true;

                    // Get Light Tower State
                    Oven.LighTower = GetCurrentLightTowerColor();

                    // Get Recipe Name
                    Oven.RecipeName = GetRecipePath();

                    // Get Rail Width PV
                    for (int i = 1; i < 5; i++)
                    {
                        Oven.RailWidthPV[i - 1] = GetRailWidth(i);
                    }

                    // Get Rail Width SP
                    for (int i = 101; i < 105; i++)
                    {
                        Oven.RailWidthSP[i - 101] = GetRailWidth(i);
                    }

                    // Processed Count
                    for (int i = 49; i < 52; i++)
                    {
                        Oven.ProcessedCount[i - 49] = GetChannel(i);
                    }
                    Oven.ProcessedCount[3] = GetChannel(53);

                    // InOven Count
                    for (int i = 46; i < 49; i++)
                    {
                        Oven.InOvenCount[i - 46] = GetChannel(i);
                    }
                    Oven.InOvenCount[3] = GetChannel(54);

                    // Top zone SP
                    foreach (var TopZone in ChannelInfo.TopZoneChannelList)
                    {
                        int channel = 0;
                        if (TopZone.Value >= 13)
                            channel = TopZone.Value - 2;
                        else
                            channel = TopZone.Value;

                        channel += 1;

                        Oven.TopZoneTemperatureSP[TopZone.Key] = GetFurnaceSetpointsLong(channel, true);
                    }

                    // Bottom zone SP
                    foreach (var BottomZone in ChannelInfo.BottomZoneChannelList)
                    {
                        int channel = 0;
                        if (BottomZone.Value >= 13)
                            channel = BottomZone.Value - 2;
                        else
                            channel = BottomZone.Value;

                        channel += 1;

                        Oven.BottomZoneTemperatureSP[BottomZone.Key] = GetFurnaceSetpointsLong(channel, true);
                    }

                    // Top zone PV
                    foreach (var TopZone in ChannelInfo.TopZoneChannelList)
                    {
                        int channel = 0;
                        if (TopZone.Value >= 13)
                            channel = TopZone.Value - 2;
                        else
                            channel = TopZone.Value;

                        channel += 1;

                        Oven.TopZoneTemperaturePV[TopZone.Key] = GetChannel(channel);
                    }

                    // Bottom zone PV
                    foreach (var BottomZone in ChannelInfo.BottomZoneChannelList)
                    {
                        int channel = 0;
                        if (BottomZone.Value >= 13)
                            channel = BottomZone.Value - 2;
                        else
                            channel = BottomZone.Value;

                        channel += 1;

                        Oven.BottomZoneTemperaturePV[BottomZone.Key] = GetChannel(channel);
                    }

                    // Bottom zone PV
                    foreach (var belt in ChannelInfo.BeltSpeedChannelList)
                    
                    // Belt Speed SP
                    for (int i = 1; i < ChannelInfo.BeltSpeedChannelList.Count + 1; i++)
                    {
                        Oven.BeltSpeedSP[i - 1] = GetFurnaceBeltSetPoint(i) * 0.1F;
                    }

                    // Belt Speed PV
                    for (int i = 1; i < ChannelInfo.BeltSpeedChannelList.Count + 1; i++)
                    {
                        Oven.BeltSpeedPV[i - 1] = (float)GetFurnaceBeltSpeed(i);
                    }

                    // Oxygen
                    Oven.OxgenPPM[0] = GetFurnaceOxygenPPM();

                    UpdateUiEventHandler?.Invoke();
                }
                catch (Exception ex)
                {
                    HLog.log(HLog.eLog.EXCEPTION, $"ThreadPolling - {ex.Message}");
                }
                Thread.Sleep(globalParameter.UpdatePeriod);
            }
        }
        #endregion

        #region [Get Set Function]
        public void TakeControl(bool bTake)
        {
            lockOcx.EnterWriteLock();
            try
            {
                if (bTake) ocx.TakeControl(2);
                else ocx.ReleaseControl(2);

                IsTakenControl = bTake;
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("TakeControl -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
        }

        public int LoadRecipe(string fullRecipeName)
        {
            //this will not check the existance of board.
            int result = -1;
            lockOcx.EnterWriteLock();
            try
            {
                if (fullRecipeName.ToUpper().Contains("COOLDOWN"))
                    ocx.LoadCooldown();
                else
                    result = ocx.LoadRecipe(ref fullRecipeName);
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }

            return result;
        }

        public string GetRecipePath()
        {
            string recipe = string.Empty;
            lockOcx.EnterWriteLock();
            try
            {
                int result = ocx.GetRecipePath(ref recipe);
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("GetRecipeName -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
            return recipe;
        }

        public void SetRailWidth(int railIdx, float width)
        {
            lockOcx.EnterWriteLock();
            try
            {
                //ocx.TakeControl(2);    // 11-Jan-23  01.01.07.00   MSL Remove take control when change SP.
                ocx.SetRailWidth((short)railIdx, width);
                //ocx.ReleaseControl(2); // 11-Jan-23  01.01.07.00   MSL Remove take control when change SP.
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("SetRailWidth -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
        }

        public float GetRailWidth(int railIdx)
        {
            float fRailWidth = 0;
            lockOcx.EnterWriteLock();
            try
            {
                variant = ocx.GetRailWidth((short)railIdx);
                if (variant == null)
                    fRailWidth = -1;
                else
                    fRailWidth = (float)variant;
#if true
                // 07-Feb-23  01.01.10.00   MSL Discard more than one decimal place during gets rail setpoint.
                if (railIdx > 100) // discard decimal point only setpoint
                    fRailWidth = (float)Math.Round(fRailWidth, 1); //fRailWidth = (float)Math.Truncate(fRailWidth);
#endif
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("GetRailWidth -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }

            return fRailWidth;
        }

        public void SetBeltSpeed(int beltIdx, float speed)
        {
            lockOcx.EnterWriteLock();
            try
            {
                // 07-Feb-23  01.01.10.00   MSL Discard more than one decimal place during gets rail setpoint.
                speed = (float)Math.Round(speed, 1);

                // 11-Jan-23  01.01.08.00   MSL restore remove take control
                ocx.TakeControl(2);       // 11-Jan-23  01.01.07.00   MSL Remove take control when change SP.
                ocx.SetFurnaceBeltSpeed(speed, (short)beltIdx);
                ocx.ReleaseControl(2);    // 11-Jan-23  01.01.07.00   MSL Remove take control when change SP.
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("SetBeltSpeed -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
        }

        public float GetBeltSpeed(int channel, bool isSetPoint)
        {
            float speed = 0;
            lockOcx.EnterWriteLock();
            try
            {
                if (isSetPoint)
                {
#if false
                    // Display same value with Oven Software.
                    // 27-Jan-23  01.01.09.00   MSL Discard decimal point during gets rail setpoint.
                    //                              Discard decimal point during gets belt setpoint.
                    speed = ocx.GetFurnaceBeltSetPoint((short)channel) * 0.1F;
                    speed = (float)Math.Truncate(speed);
#else
                    speed = ocx.GetFurnaceBeltSetPoint((short)channel) * 0.1F;
#endif
                }
                else
                    speed = Convert.ToSingle(ocx.GetFurnaceBeltSpeed((short)channel)) / 100F;
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("SetBeltSpeed -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
            return speed;
        }

        public int GetFurnaceBeltSetPoint(int channel)
        {
            int speed = 0;
            lockOcx.EnterWriteLock();
            try
            {
                speed = ocx.GetFurnaceBeltSetPoint((short)channel);
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("GetFurnaceBeltSpeed -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
            return speed;
        }

        public object GetFurnaceBeltSpeed(int channel)
        {
            object speed = 0;
            lockOcx.EnterWriteLock();
            try
            {
                speed = Convert.ToSingle(ocx.GetFurnaceBeltSpeed((short)channel)) / 100F; //TODO : Need to check the Acutal value
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("GetFurnaceBeltSpeed -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
            return speed;
        }

        public int GetFurnaceOxygenPPM()
        {
            int ppm = 0;
            lockOcx.EnterWriteLock();
            try
            {
                ppm = ocx.getFurnaceOxygenPPM();
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("getFurnaceOxygenPPM -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
            return ppm;
        }

        public void SetSmema(int laneID, bool bOn)
        {
            lockOcx.EnterWriteLock();
            try
            {
                //Hold 1 is off, 0 is on                
                if (ocx != null && !ocx.IsDisposed)
                    ocx.SMEMA_SetLaneHold(Convert.ToUInt32(laneID - 1), bOn ? 0 : 1);
                else
                    HLog.log(HLog.eLog.ERROR, "Oven is not connected!");
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("SetBeSetSmemaltSpeed -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
        }

        public void SetSmema(int laneID, int bhold)
        {
            lockOcx.EnterWriteLock();
            try
            {
                //Hold 1 is off, 0 is on                
                if (ocx != null && !ocx.IsDisposed)
                    ocx.SMEMA_SetLaneHold((uint)laneID, bhold);
                else
                    HLog.log(HLog.eLog.ERROR, "Oven is not connected!");
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("SetBeSetSmemaltSpeed -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
        }

        public int GetBoardInCount(int laneNo)
        {
            lockOcx.EnterWriteLock();
            try
            {
                if (laneNo == 0)
                {
                    int ret = 0;
                    for (short i = 0; i < ChannelInfo.LaneChannelList.Count; i++)
                    {
                        ret += ocx.GetBoardsInOvenCount(i);
                    }

                    return ret;
                }
                else
                    return ocx.GetBoardsInOvenCount((short)(laneNo - 1));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
        }

        public void SetTemperatureSP(bool isCoolZone, bool isTop, int zoneID, float temperature)
        {
            lockOcx.EnterWriteLock();
            try
            {
                //ocx.TakeControl(2); // 11-Jan-23  01.01.07.00   MSL Remove take control when change SP.
                if (isCoolZone)
                {
                    //channelparam is used to set the SP according the recipe information's sequence.
                    //the column #2 is setpoint.
                    ocx.SetChannelParam((short)ChannelInfo.CoolZoneChannelList[zoneID], 2, temperature, 0);
                }
                else
                {
                    int heaterIndex = 1;
                    if (isTop) heaterIndex = zoneID * 2 - 1;
                    else heaterIndex = zoneID * 2;
                    ocx.SetFurnaceSetpoints_Float(temperature, (short)heaterIndex);
                }
                //ocx.ReleaseControl(2); // 11-Jan-23  01.01.07.00   MSL Remove take control when change SP.
            }
            catch (Exception ex)
            {
                HLog.log("ERROR", String.Format("SetTemperatureSP -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
        }

        public float GetFurnaceSetpointsLong(int channel, bool isSetPoint)
        {
            float temp = 0;
            lockOcx.EnterWriteLock();
            try
            {
                if (isSetPoint)
                    temp = ocx.GetFurnaceSetpointsLong((short)channel) / 10F; // TODO : 확인 필요
                else
                    temp = Convert.ToSingle(ocx.GetFurnaceTCValueLong((short)channel)) / 10F;
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("SetBeltSpeed -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
            return temp;
        }

        public void SetDigitalOutput(short numDO, bool bOn)
        {
            if (numDO < 8) numDO += 48;
            else if (numDO < 16) numDO += 0;
            else if (numDO < 24) numDO += 8;
            else if (numDO < 32) numDO += 16;

            lockOcx.EnterWriteLock();
            try
            {
                if (ocx != null && !ocx.IsDisposed)
                    ocx.SetDigitalOutput(numDO, bOn ? 1 : 0);
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, String.Format("SetDigitalOutput -- Message: {0}", ex.Message));
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
        }

        public bool GetDigitalOutput(short numDO)
        {
            if (numDO < 8) numDO += 48;
            else if (numDO < 16) numDO += 0;
            else if (numDO < 24) numDO += 8;
            else if (numDO < 32) numDO += 16;

            lockOcx.EnterWriteLock();
            try
            {
                return ocx.GetDigitalInput(numDO) != 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                lockOcx.ExitWriteLock();
            }
        }

        public string GetCurrentLightTowerColor()
        {
            string state = string.Empty;
            try
            {
                lockOcx.EnterWriteLock();
                short lightTowerColor = ocx.GetCurrentLightTowerColor();
                lockOcx.ExitWriteLock();
                switch ((eLightTowerState)lightTowerColor)
                {
                    case eLightTowerState.LIGHT_TOWER_OFF:
                        state = "Off";
                        break;
                    case eLightTowerState.LIGHT_TOWER_RED:
                        state = "Red";
                        break;
                    case eLightTowerState.LIGHT_TOWER_AMBER:
                        state = "Amber";
                        break;
                    case eLightTowerState.LIGHT_TOWER_GREEN:
                        state = "Green";
                        break;
                    default:
                        state = lightTowerColor.ToString("0");
                        break;
                }
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"GetCurrentLightTowerColor - {ex.Message}");
            }

            return state;
        }

        public float GetChannel(int channel)
        {
            float value = 0;
            try
            {
                lockOcx.EnterWriteLock();
                value = ocx.GetChannel((short)channel);
                lockOcx.ExitWriteLock();
            }
            catch (Exception ex)
            {
                HLog.log(HLog.eLog.EXCEPTION, $"GetChannel - {ex.Message}");
            }

            return value;
        }
#endregion

#region [Local Function]
        private string[] ConvertToTraceData()
        {
            string[] valuse = new string[20];
            try
            {
                // Recipe Name
                valuse[0] = Oven.RecipeName;

                // Top Zone SP
                StringBuilder setTopZones = new StringBuilder();
                foreach (float zone in Oven.TopZoneTemperatureSP)
                {
                    if (zone != 0)
                    {
                        setTopZones.Append(zone.ToString());
                        setTopZones.Append(",");
                    }
                }
                setTopZones.Remove(setTopZones.Length - 1, 1);
                valuse[1] = setTopZones.ToString();

                // Bottom Zone SP
                StringBuilder setBottomZones = new StringBuilder();
                foreach (float zone in Oven.BottomZoneTemperatureSP)
                {
                    if (zone != 0)
                    {
                        setBottomZones.Append(zone.ToString());
                        setBottomZones.Append(",");
                    }
                }
                setBottomZones.Remove(setBottomZones.Length - 1, 1);
                valuse[2] = setBottomZones.ToString();

                // Conveyor1 SP
                valuse[3] = Oven.BeltSpeedSP[0].ToString();

                // Top Zone PV
                StringBuilder processTopZones = new StringBuilder();
                foreach (float zone in Oven.TopZoneTemperaturePV)
                {
                    if (zone != 0)
                    {
                        processTopZones.Append(zone.ToString());
                        processTopZones.Append(",");
                    }
                }
                processTopZones.Remove(processTopZones.Length - 1, 1);
                valuse[4] = processTopZones.ToString();

                // Bottom Zone PV
                StringBuilder processBottomZones = new StringBuilder();
                foreach (float zone in Oven.BottomZoneTemperaturePV)
                {
                    if (zone != 0)
                    {
                        processBottomZones.Append(zone.ToString());
                        processBottomZones.Append(",");
                    }
                }
                processBottomZones.Remove(processBottomZones.Length -1, 1);
                valuse[5] = processBottomZones.ToString();

                // Conveyor1 PV
                valuse[6] = Oven.BeltSpeedPV[0].ToString();

                // O2 PPM
                valuse[7] = Oven.OxgenPPM[0].ToString();

            }
            catch (Exception ex)
            {
                HLog.log("ERROR", String.Format("ConvertToTraceData -- Message: {0}", ex.Message));
            }
            return valuse;
        }
#endregion

#region [Event]
        private void OCXWrapper_NotificationEvent(object sender, AxHELLERCOMMLib._DHellerCommEvents_NotificationEventEvent e)
        {
            switch ((eOCXEventID)e.lEventID)
            {
                case eOCXEventID.ALARM_ID:
                    break;
                case eOCXEventID.ALARM_MESSAGE:
                    break;
                case eOCXEventID.ALARM_ACK:
                    break;
                case eOCXEventID.LIGHT_TOWER_CHANGE:
                    {
                        switch (e.lEventData)
                        {
                            case 0:
                                HLog.log(HLog.eLog.EVENT, $"Light Tower Change to [{e.lEventData}] [Off]");
                                break;
                            case 1:
                                HLog.log(HLog.eLog.EVENT, $"Light Tower Change to [{e.lEventData}] [Red]");
                                break;
                            case 2:
                                HLog.log(HLog.eLog.EVENT, $"Light Tower Change to [{e.lEventData}] [Amber]");
                                break;
                            case 4:
                                HLog.log(HLog.eLog.EVENT, $"Light Tower Change to [{e.lEventData}] [Green]");
                                break;
                            default:
                                HLog.log(HLog.eLog.EVENT, $"Light Tower Change to [{e.lEventData}] [Unknown]");
                                break;
                        }
                    }
                    break;
                case eOCXEventID.JOB_CHANGE:
                    {
                        HLog.log(HLog.eLog.EVENT, $"Start Job Change to [{GetRecipePath()}]");
                    }
                    break;
                case eOCXEventID.HEAT_SP_CHANGE:
                    if (e.lEventData == 64)
                    {
                        IsIntialSPUpdateFinished = true;
                        // Ignore..
                        // OnInitialized?.BeginInvoke(ChannelInfo.LaneChannelList.Count, null, null);
                    }
                    break;
            }

            if (IsIntialSPUpdateFinished)
            {
                switch ((eOCXEventID)e.lEventID)
                {
                    case eOCXEventID.BOARD_ENTERED:
                    case eOCXEventID.BOARD_EXITED:
                        //not use
                        break;
                    case eOCXEventID.LANE1_SMEMA_ENTRY_BA:
                        HLog.log(HLog.eLog.EVENT, $"SMEMA1 {e.lEventData}");
                        //not use
                        break;
                    case eOCXEventID.LANE2_SMEMA_ENTRY_BA:
                        HLog.log(HLog.eLog.EVENT, $"SMEMA2 {e.lEventData}");
                        //not use
                        break;
                    case eOCXEventID.LANE3_SMEMA_ENTRY_BA:
                        HLog.log(HLog.eLog.EVENT, $"SMEMA3 {e.lEventData}");
                        //not use
                        break;
                    case eOCXEventID.LANE4_SMEMA_ENTRY_BA:
                        HLog.log(HLog.eLog.EVENT, $"SMEMA4 {e.lEventData}");
                        //not use
                        break;
                    case eOCXEventID.LANE1_BOARD_ENTRY:
                        SetSmema(0, 1);
                        HLog.logTrace(Lane1Barcode, ConvertToTraceData());
                        //UseLog.Log(UseLog.LogCategory.Error, $"TEST Board{e.lEventData} Enter");
                        BoardEntered(1, e.lEventData);  //lEventData is serial number of baord 
                        break;
                    case eOCXEventID.LANE1_BOARD_EXIT:  //include board drop
                        //UseLog.Log(UseLog.LogCategory.Error, $"TEST Board{e.lEventData} Exit");
                        BoardExited(1, e.lEventData);
                        break;
                    case eOCXEventID.LANE2_BOARD_ENTRY:
                        SetSmema(1, 1);
                        HLog.logTrace(Lane2Barcode, ConvertToTraceData());
                        BoardEntered(2, e.lEventData);
                        break;
                    case eOCXEventID.LANE2_BOARD_EXIT:
                        BoardExited(2, e.lEventData);
                        break;
                    case eOCXEventID.LANE3_BOARD_ENTRY:
                        BoardEntered(3, e.lEventData);
                        break;
                    case eOCXEventID.LANE3_BOARD_EXIT:
                        BoardExited(3, e.lEventData);
                        break;
                    case eOCXEventID.LANE4_BOARD_ENTRY:
                        BoardEntered(4, e.lEventData);
                        break;
                    case eOCXEventID.LANE4_BOARD_EXIT:
                        BoardExited(4, e.lEventData);
                        break;
                    default:
                        break;
                }
            }

        }

        private void BoardEntered(int laneID, int serialID)
        {
            HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)laneID}] [Board Entry]");
        }

        private void BoardExited(int laneID, int serialID)
        {
            HLog.log(HLog.eLog.EVENT, $"[{(globalParameter.eLane)laneID}] [Board Exit]");
        }

        private void OCXWrapper_UpdateChannelParam(object sender, AxHELLERCOMMLib._DHellerCommEvents_UpdateChannelParamEvent e)
        {
            HLog.log(HLog.eLog.EVENT, $"EnumChannel : {e.enumChannel}, ChannelParam : {e.enumChannellParam}, Value : {e.fValue}");
            //throw new NotImplementedException();
        }
#endregion
    }
}
