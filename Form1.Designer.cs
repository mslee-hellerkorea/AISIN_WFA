using System;
using System.Text;
using OMRON.Compolet.CIP;
using Microsoft.Win32;
using AxHELLERCOMMLib;

namespace AISIN_WFA
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.compolet = new OMRON.Compolet.CIP.CJ2Compolet(this.components);
            this.axHellerComm1 = new AxHELLERCOMMLib.AxHellerComm();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_EHC2 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.cb_EHC1 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbPlcStation = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tbPlcStation = new System.Windows.Forms.TextBox();
            this.combo_PLCType = new System.Windows.Forms.ComboBox();
            this.btn_PlcSettings = new System.Windows.Forms.Button();
            this.radioButtonRail1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbBASignalLane1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tbRail1WidthSP = new System.Windows.Forms.TextBox();
            this.tbBarcodeLane2 = new System.Windows.Forms.TextBox();
            this.tbRail2WidthSP = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbRail3WidthSP = new System.Windows.Forms.TextBox();
            this.btn_map_barcode = new System.Windows.Forms.Button();
            this.tbRail4WidthSP = new System.Windows.Forms.TextBox();
            this.radioButtonRail4 = new System.Windows.Forms.RadioButton();
            this.tbRail1WidthPV = new System.Windows.Forms.TextBox();
            this.radioButtonRail3 = new System.Windows.Forms.RadioButton();
            this.tbRail2WidthPV = new System.Windows.Forms.TextBox();
            this.radioButtonRail2 = new System.Windows.Forms.RadioButton();
            this.tbRail3WidthPV = new System.Windows.Forms.TextBox();
            this.tbRail4WidthPV = new System.Windows.Forms.TextBox();
            this.tbTagIP = new System.Windows.Forms.TextBox();
            this.tbBelt1Processed = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbBelt2Processed = new System.Windows.Forms.TextBox();
            this.tbBelt4InOven = new System.Windows.Forms.TextBox();
            this.tbBelt3Processed = new System.Windows.Forms.TextBox();
            this.tbBelt3InOven = new System.Windows.Forms.TextBox();
            this.tbLightTowerColor = new System.Windows.Forms.TextBox();
            this.tbBelt2InOven = new System.Windows.Forms.TextBox();
            this.btnSelectPort = new System.Windows.Forms.Button();
            this.tbBelt1InOven = new System.Windows.Forms.TextBox();
            this.btnQuit = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbBelt4Processed = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbLogFilesFolder = new System.Windows.Forms.TextBox();
            this.tbBarcodeLane1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbBarcodeLane1string = new System.Windows.Forms.TextBox();
            this.tbBarcodeLane2string = new System.Windows.Forms.TextBox();
            this.btnStartComm = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tbDownstreamPLCTag = new System.Windows.Forms.TextBox();
            this.tbBASignalLane2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbBarcodeSelect = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbUpstreamPLCTag = new System.Windows.Forms.TextBox();
            this.btnSelectBarcode = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axHellerComm1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // compolet
            // 
            this.compolet.Active = true;
            this.compolet.ConnectionType = OMRON.Compolet.CIP.ConnectionType.UCMM;
            this.compolet.HeartBeatTimer = 0;
            this.compolet.LocalPort = 2;
            this.compolet.PeerAddress = null;
            this.compolet.ReceiveTimeLimit = ((long)(750));
            this.compolet.RoutePath = "";
            this.compolet.UseRoutePath = false;
            this.compolet.OnHeartBeatTimer += new System.EventHandler(this.compolet_OnHeartBeatTimer);
            // 
            // axHellerComm1
            // 
            this.axHellerComm1.Enabled = true;
            this.axHellerComm1.Location = new System.Drawing.Point(600, 550);
            this.axHellerComm1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.axHellerComm1.Name = "axHellerComm1";
            this.axHellerComm1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axHellerComm1.OcxState")));
            this.axHellerComm1.Size = new System.Drawing.Size(144, 31);
            this.axHellerComm1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_EHC2);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.cb_EHC1);
            this.groupBox1.Location = new System.Drawing.Point(413, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 85);
            this.groupBox1.TabIndex = 120;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rail Configuration";
            // 
            // cb_EHC2
            // 
            this.cb_EHC2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_EHC2.FormattingEnabled = true;
            this.cb_EHC2.Location = new System.Drawing.Point(59, 46);
            this.cb_EHC2.Name = "cb_EHC2";
            this.cb_EHC2.Size = new System.Drawing.Size(95, 21);
            this.cb_EHC2.TabIndex = 75;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 49);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(47, 13);
            this.label21.TabIndex = 74;
            this.label21.Text = "Lane#2:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 13);
            this.label20.TabIndex = 73;
            this.label20.Text = "Lane#1:";
            // 
            // cb_EHC1
            // 
            this.cb_EHC1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_EHC1.FormattingEnabled = true;
            this.cb_EHC1.Location = new System.Drawing.Point(59, 22);
            this.cb_EHC1.Name = "cb_EHC1";
            this.cb_EHC1.Size = new System.Drawing.Size(95, 21);
            this.cb_EHC1.TabIndex = 72;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbPlcStation);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.tbPlcStation);
            this.groupBox2.Controls.Add(this.combo_PLCType);
            this.groupBox2.Location = new System.Drawing.Point(413, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 99);
            this.groupBox2.TabIndex = 119;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PLC Type Configuration";
            // 
            // lbPlcStation
            // 
            this.lbPlcStation.AutoSize = true;
            this.lbPlcStation.Location = new System.Drawing.Point(6, 59);
            this.lbPlcStation.Name = "lbPlcStation";
            this.lbPlcStation.Size = new System.Drawing.Size(66, 13);
            this.lbPlcStation.TabIndex = 69;
            this.lbPlcStation.Text = "PLC Station:";
            this.lbPlcStation.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 25);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 13);
            this.label19.TabIndex = 70;
            this.label19.Text = "PLC Type:";
            // 
            // tbPlcStation
            // 
            this.tbPlcStation.Location = new System.Drawing.Point(78, 52);
            this.tbPlcStation.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbPlcStation.Name = "tbPlcStation";
            this.tbPlcStation.Size = new System.Drawing.Size(94, 20);
            this.tbPlcStation.TabIndex = 68;
            this.tbPlcStation.Visible = false;
            // 
            // combo_PLCType
            // 
            this.combo_PLCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_PLCType.FormattingEnabled = true;
            this.combo_PLCType.Items.AddRange(new object[] {
            "None",
            "OMRON",
            "Mitsubishi"});
            this.combo_PLCType.Location = new System.Drawing.Point(77, 25);
            this.combo_PLCType.Name = "combo_PLCType";
            this.combo_PLCType.Size = new System.Drawing.Size(95, 21);
            this.combo_PLCType.TabIndex = 71;
            // 
            // btn_PlcSettings
            // 
            this.btn_PlcSettings.Location = new System.Drawing.Point(413, 389);
            this.btn_PlcSettings.Name = "btn_PlcSettings";
            this.btn_PlcSettings.Size = new System.Drawing.Size(116, 25);
            this.btn_PlcSettings.TabIndex = 118;
            this.btn_PlcSettings.Tag = "Setup";
            this.btn_PlcSettings.Text = "PLC Settings";
            this.btn_PlcSettings.UseVisualStyleBackColor = true;
            // 
            // radioButtonRail1
            // 
            this.radioButtonRail1.AutoSize = true;
            this.radioButtonRail1.Location = new System.Drawing.Point(123, 4);
            this.radioButtonRail1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButtonRail1.Name = "radioButtonRail1";
            this.radioButtonRail1.Size = new System.Drawing.Size(31, 17);
            this.radioButtonRail1.TabIndex = 109;
            this.radioButtonRail1.TabStop = true;
            this.radioButtonRail1.Text = "1";
            this.radioButtonRail1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "Rail Width SP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 66;
            this.label2.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 65);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 67;
            this.label6.Text = "Rail Width PV";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 97);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 68;
            this.label7.Text = "Boards Processed";
            // 
            // tbBASignalLane1
            // 
            this.tbBASignalLane1.Location = new System.Drawing.Point(111, 392);
            this.tbBASignalLane1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBASignalLane1.Name = "tbBASignalLane1";
            this.tbBASignalLane1.ReadOnly = true;
            this.tbBASignalLane1.Size = new System.Drawing.Size(286, 20);
            this.tbBASignalLane1.TabIndex = 117;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 162);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 69;
            this.label8.Text = "Light Tower Color";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 389);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 13);
            this.label18.TabIndex = 116;
            this.label18.Text = "BA Signal Lane1";
            // 
            // tbRail1WidthSP
            // 
            this.tbRail1WidthSP.Location = new System.Drawing.Point(112, 35);
            this.tbRail1WidthSP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail1WidthSP.Name = "tbRail1WidthSP";
            this.tbRail1WidthSP.ReadOnly = true;
            this.tbRail1WidthSP.Size = new System.Drawing.Size(61, 20);
            this.tbRail1WidthSP.TabIndex = 70;
            // 
            // tbBarcodeLane2
            // 
            this.tbBarcodeLane2.Location = new System.Drawing.Point(112, 360);
            this.tbBarcodeLane2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeLane2.Name = "tbBarcodeLane2";
            this.tbBarcodeLane2.ReadOnly = true;
            this.tbBarcodeLane2.Size = new System.Drawing.Size(286, 20);
            this.tbBarcodeLane2.TabIndex = 115;
            // 
            // tbRail2WidthSP
            // 
            this.tbRail2WidthSP.Location = new System.Drawing.Point(186, 35);
            this.tbRail2WidthSP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail2WidthSP.Name = "tbRail2WidthSP";
            this.tbRail2WidthSP.ReadOnly = true;
            this.tbRail2WidthSP.Size = new System.Drawing.Size(61, 20);
            this.tbRail2WidthSP.TabIndex = 71;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(11, 357);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 13);
            this.label17.TabIndex = 114;
            this.label17.Text = "Barcode Lane2";
            // 
            // tbRail3WidthSP
            // 
            this.tbRail3WidthSP.Location = new System.Drawing.Point(262, 35);
            this.tbRail3WidthSP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail3WidthSP.Name = "tbRail3WidthSP";
            this.tbRail3WidthSP.ReadOnly = true;
            this.tbRail3WidthSP.Size = new System.Drawing.Size(61, 20);
            this.tbRail3WidthSP.TabIndex = 72;
            // 
            // btn_map_barcode
            // 
            this.btn_map_barcode.AutoSize = true;
            this.btn_map_barcode.Location = new System.Drawing.Point(413, 448);
            this.btn_map_barcode.Margin = new System.Windows.Forms.Padding(2);
            this.btn_map_barcode.Name = "btn_map_barcode";
            this.btn_map_barcode.Size = new System.Drawing.Size(116, 25);
            this.btn_map_barcode.TabIndex = 113;
            this.btn_map_barcode.Text = "Map barcode";
            this.btn_map_barcode.UseVisualStyleBackColor = true;
            // 
            // tbRail4WidthSP
            // 
            this.tbRail4WidthSP.Location = new System.Drawing.Point(336, 35);
            this.tbRail4WidthSP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail4WidthSP.Name = "tbRail4WidthSP";
            this.tbRail4WidthSP.ReadOnly = true;
            this.tbRail4WidthSP.Size = new System.Drawing.Size(61, 20);
            this.tbRail4WidthSP.TabIndex = 73;
            // 
            // radioButtonRail4
            // 
            this.radioButtonRail4.AutoSize = true;
            this.radioButtonRail4.Location = new System.Drawing.Point(349, 4);
            this.radioButtonRail4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButtonRail4.Name = "radioButtonRail4";
            this.radioButtonRail4.Size = new System.Drawing.Size(31, 17);
            this.radioButtonRail4.TabIndex = 112;
            this.radioButtonRail4.TabStop = true;
            this.radioButtonRail4.Text = "4";
            this.radioButtonRail4.UseVisualStyleBackColor = true;
            // 
            // tbRail1WidthPV
            // 
            this.tbRail1WidthPV.Location = new System.Drawing.Point(112, 68);
            this.tbRail1WidthPV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail1WidthPV.Name = "tbRail1WidthPV";
            this.tbRail1WidthPV.ReadOnly = true;
            this.tbRail1WidthPV.Size = new System.Drawing.Size(61, 20);
            this.tbRail1WidthPV.TabIndex = 74;
            // 
            // radioButtonRail3
            // 
            this.radioButtonRail3.AutoSize = true;
            this.radioButtonRail3.Location = new System.Drawing.Point(273, 4);
            this.radioButtonRail3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButtonRail3.Name = "radioButtonRail3";
            this.radioButtonRail3.Size = new System.Drawing.Size(31, 17);
            this.radioButtonRail3.TabIndex = 111;
            this.radioButtonRail3.TabStop = true;
            this.radioButtonRail3.Text = "3";
            this.radioButtonRail3.UseVisualStyleBackColor = true;
            // 
            // tbRail2WidthPV
            // 
            this.tbRail2WidthPV.Location = new System.Drawing.Point(186, 67);
            this.tbRail2WidthPV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail2WidthPV.Name = "tbRail2WidthPV";
            this.tbRail2WidthPV.ReadOnly = true;
            this.tbRail2WidthPV.Size = new System.Drawing.Size(61, 20);
            this.tbRail2WidthPV.TabIndex = 75;
            // 
            // radioButtonRail2
            // 
            this.radioButtonRail2.AutoSize = true;
            this.radioButtonRail2.Location = new System.Drawing.Point(199, 4);
            this.radioButtonRail2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButtonRail2.Name = "radioButtonRail2";
            this.radioButtonRail2.Size = new System.Drawing.Size(31, 17);
            this.radioButtonRail2.TabIndex = 110;
            this.radioButtonRail2.TabStop = true;
            this.radioButtonRail2.Text = "2";
            this.radioButtonRail2.UseVisualStyleBackColor = true;
            // 
            // tbRail3WidthPV
            // 
            this.tbRail3WidthPV.Location = new System.Drawing.Point(262, 67);
            this.tbRail3WidthPV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail3WidthPV.Name = "tbRail3WidthPV";
            this.tbRail3WidthPV.ReadOnly = true;
            this.tbRail3WidthPV.Size = new System.Drawing.Size(61, 20);
            this.tbRail3WidthPV.TabIndex = 76;
            // 
            // tbRail4WidthPV
            // 
            this.tbRail4WidthPV.Location = new System.Drawing.Point(336, 67);
            this.tbRail4WidthPV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail4WidthPV.Name = "tbRail4WidthPV";
            this.tbRail4WidthPV.ReadOnly = true;
            this.tbRail4WidthPV.Size = new System.Drawing.Size(61, 20);
            this.tbRail4WidthPV.TabIndex = 77;
            // 
            // tbTagIP
            // 
            this.tbTagIP.Location = new System.Drawing.Point(134, 263);
            this.tbTagIP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbTagIP.Name = "tbTagIP";
            this.tbTagIP.Size = new System.Drawing.Size(263, 20);
            this.tbTagIP.TabIndex = 108;
            // 
            // tbBelt1Processed
            // 
            this.tbBelt1Processed.Location = new System.Drawing.Point(112, 100);
            this.tbBelt1Processed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt1Processed.Name = "tbBelt1Processed";
            this.tbBelt1Processed.ReadOnly = true;
            this.tbBelt1Processed.Size = new System.Drawing.Size(61, 20);
            this.tbBelt1Processed.TabIndex = 78;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 260);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 13);
            this.label16.TabIndex = 107;
            this.label16.Text = "Oven Tag IP";
            // 
            // tbBelt2Processed
            // 
            this.tbBelt2Processed.Location = new System.Drawing.Point(186, 100);
            this.tbBelt2Processed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt2Processed.Name = "tbBelt2Processed";
            this.tbBelt2Processed.ReadOnly = true;
            this.tbBelt2Processed.Size = new System.Drawing.Size(61, 20);
            this.tbBelt2Processed.TabIndex = 79;
            // 
            // tbBelt4InOven
            // 
            this.tbBelt4InOven.Location = new System.Drawing.Point(336, 133);
            this.tbBelt4InOven.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt4InOven.Name = "tbBelt4InOven";
            this.tbBelt4InOven.ReadOnly = true;
            this.tbBelt4InOven.Size = new System.Drawing.Size(61, 20);
            this.tbBelt4InOven.TabIndex = 106;
            // 
            // tbBelt3Processed
            // 
            this.tbBelt3Processed.Location = new System.Drawing.Point(262, 100);
            this.tbBelt3Processed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt3Processed.Name = "tbBelt3Processed";
            this.tbBelt3Processed.ReadOnly = true;
            this.tbBelt3Processed.Size = new System.Drawing.Size(61, 20);
            this.tbBelt3Processed.TabIndex = 80;
            // 
            // tbBelt3InOven
            // 
            this.tbBelt3InOven.Location = new System.Drawing.Point(262, 132);
            this.tbBelt3InOven.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt3InOven.Name = "tbBelt3InOven";
            this.tbBelt3InOven.ReadOnly = true;
            this.tbBelt3InOven.Size = new System.Drawing.Size(61, 20);
            this.tbBelt3InOven.TabIndex = 105;
            // 
            // tbLightTowerColor
            // 
            this.tbLightTowerColor.Location = new System.Drawing.Point(112, 165);
            this.tbLightTowerColor.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbLightTowerColor.Name = "tbLightTowerColor";
            this.tbLightTowerColor.ReadOnly = true;
            this.tbLightTowerColor.Size = new System.Drawing.Size(61, 20);
            this.tbLightTowerColor.TabIndex = 81;
            // 
            // tbBelt2InOven
            // 
            this.tbBelt2InOven.Location = new System.Drawing.Point(186, 133);
            this.tbBelt2InOven.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt2InOven.Name = "tbBelt2InOven";
            this.tbBelt2InOven.ReadOnly = true;
            this.tbBelt2InOven.Size = new System.Drawing.Size(61, 20);
            this.tbBelt2InOven.TabIndex = 104;
            // 
            // btnSelectPort
            // 
            this.btnSelectPort.Enabled = false;
            this.btnSelectPort.Location = new System.Drawing.Point(14, 482);
            this.btnSelectPort.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSelectPort.Name = "btnSelectPort";
            this.btnSelectPort.Size = new System.Drawing.Size(77, 25);
            this.btnSelectPort.TabIndex = 82;
            this.btnSelectPort.Text = "Select Port";
            this.btnSelectPort.UseVisualStyleBackColor = true;
            // 
            // tbBelt1InOven
            // 
            this.tbBelt1InOven.Location = new System.Drawing.Point(112, 133);
            this.tbBelt1InOven.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt1InOven.Name = "tbBelt1InOven";
            this.tbBelt1InOven.ReadOnly = true;
            this.tbBelt1InOven.Size = new System.Drawing.Size(61, 20);
            this.tbBelt1InOven.TabIndex = 103;
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(112, 482);
            this.btnQuit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(56, 25);
            this.btnQuit.TabIndex = 83;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 130);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 13);
            this.label15.TabIndex = 102;
            this.label15.Text = "Boards In Oven";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(413, 414);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(116, 25);
            this.btnConnect.TabIndex = 84;
            this.btnConnect.Text = "Barcode Settings";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // tbBelt4Processed
            // 
            this.tbBelt4Processed.Location = new System.Drawing.Point(336, 100);
            this.tbBelt4Processed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt4Processed.Name = "tbBelt4Processed";
            this.tbBelt4Processed.ReadOnly = true;
            this.tbBelt4Processed.Size = new System.Drawing.Size(61, 20);
            this.tbBelt4Processed.TabIndex = 101;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 325);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 85;
            this.label9.Text = "Barcode Lane1";
            // 
            // tbLogFilesFolder
            // 
            this.tbLogFilesFolder.Location = new System.Drawing.Point(134, 295);
            this.tbLogFilesFolder.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbLogFilesFolder.Name = "tbLogFilesFolder";
            this.tbLogFilesFolder.Size = new System.Drawing.Size(263, 20);
            this.tbLogFilesFolder.TabIndex = 100;
            // 
            // tbBarcodeLane1
            // 
            this.tbBarcodeLane1.Location = new System.Drawing.Point(112, 329);
            this.tbBarcodeLane1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeLane1.Name = "tbBarcodeLane1";
            this.tbBarcodeLane1.ReadOnly = true;
            this.tbBarcodeLane1.Size = new System.Drawing.Size(285, 20);
            this.tbBarcodeLane1.TabIndex = 86;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 292);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 13);
            this.label14.TabIndex = 99;
            this.label14.Text = "Log Files Folder";
            // 
            // tbBarcodeLane1string
            // 
            this.tbBarcodeLane1string.Location = new System.Drawing.Point(413, 329);
            this.tbBarcodeLane1string.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeLane1string.Name = "tbBarcodeLane1string";
            this.tbBarcodeLane1string.ReadOnly = true;
            this.tbBarcodeLane1string.Size = new System.Drawing.Size(90, 20);
            this.tbBarcodeLane1string.TabIndex = 87;
            // 
            // tbBarcodeLane2string
            // 
            this.tbBarcodeLane2string.Location = new System.Drawing.Point(413, 360);
            this.tbBarcodeLane2string.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeLane2string.Name = "tbBarcodeLane2string";
            this.tbBarcodeLane2string.ReadOnly = true;
            this.tbBarcodeLane2string.Size = new System.Drawing.Size(90, 20);
            this.tbBarcodeLane2string.TabIndex = 88;
            // 
            // btnStartComm
            // 
            this.btnStartComm.Location = new System.Drawing.Point(273, 482);
            this.btnStartComm.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnStartComm.Name = "btnStartComm";
            this.btnStartComm.Size = new System.Drawing.Size(116, 25);
            this.btnStartComm.TabIndex = 98;
            this.btnStartComm.Text = "Stop Comm";
            this.btnStartComm.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 420);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 89;
            this.label10.Text = "BA Signal Lane2";
            // 
            // tbDownstreamPLCTag
            // 
            this.tbDownstreamPLCTag.Location = new System.Drawing.Point(134, 230);
            this.tbDownstreamPLCTag.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbDownstreamPLCTag.Name = "tbDownstreamPLCTag";
            this.tbDownstreamPLCTag.Size = new System.Drawing.Size(263, 20);
            this.tbDownstreamPLCTag.TabIndex = 97;
            // 
            // tbBASignalLane2
            // 
            this.tbBASignalLane2.Location = new System.Drawing.Point(111, 419);
            this.tbBASignalLane2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBASignalLane2.Name = "tbBASignalLane2";
            this.tbBASignalLane2.ReadOnly = true;
            this.tbBASignalLane2.Size = new System.Drawing.Size(286, 20);
            this.tbBASignalLane2.TabIndex = 90;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 227);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(111, 13);
            this.label13.TabIndex = 96;
            this.label13.Text = "Downstream PLC Tag";
            // 
            // tbBarcodeSelect
            // 
            this.tbBarcodeSelect.Location = new System.Drawing.Point(111, 451);
            this.tbBarcodeSelect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeSelect.Name = "tbBarcodeSelect";
            this.tbBarcodeSelect.ReadOnly = true;
            this.tbBarcodeSelect.Size = new System.Drawing.Size(286, 20);
            this.tbBarcodeSelect.TabIndex = 91;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Enabled = false;
            this.label11.Location = new System.Drawing.Point(9, 450);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.TabIndex = 92;
            this.label11.Text = "Barcode Select";
            // 
            // tbUpstreamPLCTag
            // 
            this.tbUpstreamPLCTag.Location = new System.Drawing.Point(134, 198);
            this.tbUpstreamPLCTag.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbUpstreamPLCTag.Name = "tbUpstreamPLCTag";
            this.tbUpstreamPLCTag.Size = new System.Drawing.Size(263, 20);
            this.tbUpstreamPLCTag.TabIndex = 95;
            // 
            // btnSelectBarcode
            // 
            this.btnSelectBarcode.Enabled = false;
            this.btnSelectBarcode.Location = new System.Drawing.Point(172, 482);
            this.btnSelectBarcode.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSelectBarcode.Name = "btnSelectBarcode";
            this.btnSelectBarcode.Size = new System.Drawing.Size(90, 25);
            this.btnSelectBarcode.TabIndex = 93;
            this.btnSelectBarcode.Text = "Select Barcode";
            this.btnSelectBarcode.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 195);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 13);
            this.label12.TabIndex = 94;
            this.label12.Text = "Upstream PLC Tag";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(615, 538);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_PlcSettings);
            this.Controls.Add(this.radioButtonRail1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbBASignalLane1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.tbRail1WidthSP);
            this.Controls.Add(this.tbBarcodeLane2);
            this.Controls.Add(this.tbRail2WidthSP);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.tbRail3WidthSP);
            this.Controls.Add(this.btn_map_barcode);
            this.Controls.Add(this.tbRail4WidthSP);
            this.Controls.Add(this.radioButtonRail4);
            this.Controls.Add(this.tbRail1WidthPV);
            this.Controls.Add(this.radioButtonRail3);
            this.Controls.Add(this.tbRail2WidthPV);
            this.Controls.Add(this.radioButtonRail2);
            this.Controls.Add(this.tbRail3WidthPV);
            this.Controls.Add(this.tbRail4WidthPV);
            this.Controls.Add(this.tbTagIP);
            this.Controls.Add(this.tbBelt1Processed);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.tbBelt2Processed);
            this.Controls.Add(this.tbBelt4InOven);
            this.Controls.Add(this.tbBelt3Processed);
            this.Controls.Add(this.tbBelt3InOven);
            this.Controls.Add(this.tbLightTowerColor);
            this.Controls.Add(this.tbBelt2InOven);
            this.Controls.Add(this.btnSelectPort);
            this.Controls.Add(this.tbBelt1InOven);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbBelt4Processed);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbLogFilesFolder);
            this.Controls.Add(this.tbBarcodeLane1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tbBarcodeLane1string);
            this.Controls.Add(this.tbBarcodeLane2string);
            this.Controls.Add(this.btnStartComm);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbDownstreamPLCTag);
            this.Controls.Add(this.tbBASignalLane2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbBarcodeSelect);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbUpstreamPLCTag);
            this.Controls.Add(this.btnSelectBarcode);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.axHellerComm1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "AISIN Line Communication PLC Interface (v1.19)";
            ((System.ComponentModel.ISupportInitialize)(this.axHellerComm1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxHELLERCOMMLib.AxHellerComm axHellerComm1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cb_EHC2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cb_EHC1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbPlcStation;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbPlcStation;
        private System.Windows.Forms.ComboBox combo_PLCType;
        private System.Windows.Forms.Button btn_PlcSettings;
        private System.Windows.Forms.RadioButton radioButtonRail1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbBASignalLane1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbRail1WidthSP;
        private System.Windows.Forms.TextBox tbBarcodeLane2;
        private System.Windows.Forms.TextBox tbRail2WidthSP;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbRail3WidthSP;
        private System.Windows.Forms.Button btn_map_barcode;
        private System.Windows.Forms.TextBox tbRail4WidthSP;
        private System.Windows.Forms.RadioButton radioButtonRail4;
        private System.Windows.Forms.TextBox tbRail1WidthPV;
        private System.Windows.Forms.RadioButton radioButtonRail3;
        private System.Windows.Forms.TextBox tbRail2WidthPV;
        private System.Windows.Forms.RadioButton radioButtonRail2;
        private System.Windows.Forms.TextBox tbRail3WidthPV;
        private System.Windows.Forms.TextBox tbRail4WidthPV;
        private System.Windows.Forms.TextBox tbTagIP;
        private System.Windows.Forms.TextBox tbBelt1Processed;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbBelt2Processed;
        private System.Windows.Forms.TextBox tbBelt4InOven;
        private System.Windows.Forms.TextBox tbBelt3Processed;
        private System.Windows.Forms.TextBox tbBelt3InOven;
        private System.Windows.Forms.TextBox tbLightTowerColor;
        private System.Windows.Forms.TextBox tbBelt2InOven;
        private System.Windows.Forms.Button btnSelectPort;
        private System.Windows.Forms.TextBox tbBelt1InOven;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbBelt4Processed;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbLogFilesFolder;
        private System.Windows.Forms.TextBox tbBarcodeLane1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbBarcodeLane1string;
        private System.Windows.Forms.TextBox tbBarcodeLane2string;
        private System.Windows.Forms.Button btnStartComm;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbDownstreamPLCTag;
        private System.Windows.Forms.TextBox tbBASignalLane2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbBarcodeSelect;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbUpstreamPLCTag;
        private System.Windows.Forms.Button btnSelectBarcode;
        private System.Windows.Forms.Label label12;
    }
}

