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
            this.btn_PlcSettings = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            this.tbRail1WidthPV = new System.Windows.Forms.TextBox();
            this.tbRail2WidthPV = new System.Windows.Forms.TextBox();
            this.tbRail3WidthPV = new System.Windows.Forms.TextBox();
            this.tbRail4WidthPV = new System.Windows.Forms.TextBox();
            this.tbBelt1Processed = new System.Windows.Forms.TextBox();
            this.tbBelt2Processed = new System.Windows.Forms.TextBox();
            this.tbBelt4InOven = new System.Windows.Forms.TextBox();
            this.tbBelt3Processed = new System.Windows.Forms.TextBox();
            this.tbBelt3InOven = new System.Windows.Forms.TextBox();
            this.tbLightTowerColor = new System.Windows.Forms.TextBox();
            this.tbBelt2InOven = new System.Windows.Forms.TextBox();
            this.tbBelt1InOven = new System.Windows.Forms.TextBox();
            this.btnQuit = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbBelt4Processed = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbBarcodeLane1 = new System.Windows.Forms.TextBox();
            this.tbBarcodeLane1string = new System.Windows.Forms.TextBox();
            this.tbBarcodeLane2string = new System.Windows.Forms.TextBox();
            this.btnStartComm = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tbBASignalLane2 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.axHellerComm1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
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
            // btn_PlcSettings
            // 
            this.btn_PlcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PlcSettings.Location = new System.Drawing.Point(390, 323);
            this.btn_PlcSettings.Name = "btn_PlcSettings";
            this.btn_PlcSettings.Size = new System.Drawing.Size(80, 34);
            this.btn_PlcSettings.TabIndex = 118;
            this.btn_PlcSettings.Tag = "Setup";
            this.btn_PlcSettings.Text = "PLC Settings";
            this.btn_PlcSettings.UseVisualStyleBackColor = true;
            this.btn_PlcSettings.Click += new System.EventHandler(this.btn_PlcSettings_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 30);
            this.label1.TabIndex = 65;
            this.label1.Text = "Rail Width SP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(2, 30);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 30);
            this.label6.TabIndex = 67;
            this.label6.Text = "Rail Width PV";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(2, 60);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 30);
            this.label7.TabIndex = 68;
            this.label7.Text = "Boards Processed";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBASignalLane1
            // 
            this.tbBASignalLane1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBASignalLane1.Location = new System.Drawing.Point(131, 215);
            this.tbBASignalLane1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBASignalLane1.Name = "tbBASignalLane1";
            this.tbBASignalLane1.ReadOnly = true;
            this.tbBASignalLane1.Size = new System.Drawing.Size(82, 20);
            this.tbBASignalLane1.TabIndex = 117;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(2, 120);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 30);
            this.label8.TabIndex = 69;
            this.label8.Text = "Light Tower Color";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(2, 210);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(125, 30);
            this.label18.TabIndex = 116;
            this.label18.Text = "BA Signal Lane1";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbRail1WidthSP
            // 
            this.tbRail1WidthSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail1WidthSP.Location = new System.Drawing.Point(131, 5);
            this.tbRail1WidthSP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail1WidthSP.Name = "tbRail1WidthSP";
            this.tbRail1WidthSP.ReadOnly = true;
            this.tbRail1WidthSP.Size = new System.Drawing.Size(82, 20);
            this.tbRail1WidthSP.TabIndex = 70;
            // 
            // tbBarcodeLane2
            // 
            this.tbBarcodeLane2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.tbBarcodeLane2, 2);
            this.tbBarcodeLane2.Location = new System.Drawing.Point(131, 185);
            this.tbBarcodeLane2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeLane2.Name = "tbBarcodeLane2";
            this.tbBarcodeLane2.ReadOnly = true;
            this.tbBarcodeLane2.Size = new System.Drawing.Size(168, 20);
            this.tbBarcodeLane2.TabIndex = 115;
            // 
            // tbRail2WidthSP
            // 
            this.tbRail2WidthSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail2WidthSP.Location = new System.Drawing.Point(217, 5);
            this.tbRail2WidthSP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail2WidthSP.Name = "tbRail2WidthSP";
            this.tbRail2WidthSP.ReadOnly = true;
            this.tbRail2WidthSP.Size = new System.Drawing.Size(82, 20);
            this.tbRail2WidthSP.TabIndex = 71;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(2, 180);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(125, 30);
            this.label17.TabIndex = 114;
            this.label17.Text = "Barcode Lane2";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbRail3WidthSP
            // 
            this.tbRail3WidthSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail3WidthSP.Location = new System.Drawing.Point(303, 5);
            this.tbRail3WidthSP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail3WidthSP.Name = "tbRail3WidthSP";
            this.tbRail3WidthSP.ReadOnly = true;
            this.tbRail3WidthSP.Size = new System.Drawing.Size(82, 20);
            this.tbRail3WidthSP.TabIndex = 72;
            // 
            // btn_map_barcode
            // 
            this.btn_map_barcode.AutoSize = true;
            this.btn_map_barcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_map_barcode.Location = new System.Drawing.Point(131, 322);
            this.btn_map_barcode.Margin = new System.Windows.Forms.Padding(2);
            this.btn_map_barcode.Name = "btn_map_barcode";
            this.btn_map_barcode.Size = new System.Drawing.Size(82, 36);
            this.btn_map_barcode.TabIndex = 113;
            this.btn_map_barcode.Text = "Map barcode";
            this.btn_map_barcode.UseVisualStyleBackColor = true;
            // 
            // tbRail4WidthSP
            // 
            this.tbRail4WidthSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail4WidthSP.Location = new System.Drawing.Point(389, 5);
            this.tbRail4WidthSP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail4WidthSP.Name = "tbRail4WidthSP";
            this.tbRail4WidthSP.ReadOnly = true;
            this.tbRail4WidthSP.Size = new System.Drawing.Size(82, 20);
            this.tbRail4WidthSP.TabIndex = 73;
            // 
            // tbRail1WidthPV
            // 
            this.tbRail1WidthPV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail1WidthPV.Location = new System.Drawing.Point(131, 35);
            this.tbRail1WidthPV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail1WidthPV.Name = "tbRail1WidthPV";
            this.tbRail1WidthPV.ReadOnly = true;
            this.tbRail1WidthPV.Size = new System.Drawing.Size(82, 20);
            this.tbRail1WidthPV.TabIndex = 74;
            // 
            // tbRail2WidthPV
            // 
            this.tbRail2WidthPV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail2WidthPV.Location = new System.Drawing.Point(217, 35);
            this.tbRail2WidthPV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail2WidthPV.Name = "tbRail2WidthPV";
            this.tbRail2WidthPV.ReadOnly = true;
            this.tbRail2WidthPV.Size = new System.Drawing.Size(82, 20);
            this.tbRail2WidthPV.TabIndex = 75;
            // 
            // tbRail3WidthPV
            // 
            this.tbRail3WidthPV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail3WidthPV.Location = new System.Drawing.Point(303, 35);
            this.tbRail3WidthPV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail3WidthPV.Name = "tbRail3WidthPV";
            this.tbRail3WidthPV.ReadOnly = true;
            this.tbRail3WidthPV.Size = new System.Drawing.Size(82, 20);
            this.tbRail3WidthPV.TabIndex = 76;
            // 
            // tbRail4WidthPV
            // 
            this.tbRail4WidthPV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail4WidthPV.Location = new System.Drawing.Point(389, 35);
            this.tbRail4WidthPV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail4WidthPV.Name = "tbRail4WidthPV";
            this.tbRail4WidthPV.ReadOnly = true;
            this.tbRail4WidthPV.Size = new System.Drawing.Size(82, 20);
            this.tbRail4WidthPV.TabIndex = 77;
            // 
            // tbBelt1Processed
            // 
            this.tbBelt1Processed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt1Processed.Location = new System.Drawing.Point(131, 65);
            this.tbBelt1Processed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt1Processed.Name = "tbBelt1Processed";
            this.tbBelt1Processed.ReadOnly = true;
            this.tbBelt1Processed.Size = new System.Drawing.Size(82, 20);
            this.tbBelt1Processed.TabIndex = 78;
            // 
            // tbBelt2Processed
            // 
            this.tbBelt2Processed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt2Processed.Location = new System.Drawing.Point(217, 65);
            this.tbBelt2Processed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt2Processed.Name = "tbBelt2Processed";
            this.tbBelt2Processed.ReadOnly = true;
            this.tbBelt2Processed.Size = new System.Drawing.Size(82, 20);
            this.tbBelt2Processed.TabIndex = 79;
            // 
            // tbBelt4InOven
            // 
            this.tbBelt4InOven.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt4InOven.Location = new System.Drawing.Point(389, 95);
            this.tbBelt4InOven.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt4InOven.Name = "tbBelt4InOven";
            this.tbBelt4InOven.ReadOnly = true;
            this.tbBelt4InOven.Size = new System.Drawing.Size(82, 20);
            this.tbBelt4InOven.TabIndex = 106;
            // 
            // tbBelt3Processed
            // 
            this.tbBelt3Processed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt3Processed.Location = new System.Drawing.Point(303, 65);
            this.tbBelt3Processed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt3Processed.Name = "tbBelt3Processed";
            this.tbBelt3Processed.ReadOnly = true;
            this.tbBelt3Processed.Size = new System.Drawing.Size(82, 20);
            this.tbBelt3Processed.TabIndex = 80;
            // 
            // tbBelt3InOven
            // 
            this.tbBelt3InOven.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt3InOven.Location = new System.Drawing.Point(303, 95);
            this.tbBelt3InOven.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt3InOven.Name = "tbBelt3InOven";
            this.tbBelt3InOven.ReadOnly = true;
            this.tbBelt3InOven.Size = new System.Drawing.Size(82, 20);
            this.tbBelt3InOven.TabIndex = 105;
            // 
            // tbLightTowerColor
            // 
            this.tbLightTowerColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLightTowerColor.Location = new System.Drawing.Point(131, 125);
            this.tbLightTowerColor.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbLightTowerColor.Name = "tbLightTowerColor";
            this.tbLightTowerColor.ReadOnly = true;
            this.tbLightTowerColor.Size = new System.Drawing.Size(82, 20);
            this.tbLightTowerColor.TabIndex = 81;
            // 
            // tbBelt2InOven
            // 
            this.tbBelt2InOven.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt2InOven.Location = new System.Drawing.Point(217, 95);
            this.tbBelt2InOven.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt2InOven.Name = "tbBelt2InOven";
            this.tbBelt2InOven.ReadOnly = true;
            this.tbBelt2InOven.Size = new System.Drawing.Size(82, 20);
            this.tbBelt2InOven.TabIndex = 104;
            // 
            // tbBelt1InOven
            // 
            this.tbBelt1InOven.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt1InOven.Location = new System.Drawing.Point(131, 95);
            this.tbBelt1InOven.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt1InOven.Name = "tbBelt1InOven";
            this.tbBelt1InOven.ReadOnly = true;
            this.tbBelt1InOven.Size = new System.Drawing.Size(82, 20);
            this.tbBelt1InOven.TabIndex = 103;
            // 
            // btnQuit
            // 
            this.btnQuit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQuit.Location = new System.Drawing.Point(389, 363);
            this.btnQuit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(82, 34);
            this.btnQuit.TabIndex = 83;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(2, 90);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 30);
            this.label15.TabIndex = 102;
            this.label15.Text = "Boards In Oven";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConnect
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btnConnect, 2);
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Location = new System.Drawing.Point(217, 323);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(168, 34);
            this.btnConnect.TabIndex = 84;
            this.btnConnect.Text = "Barcode Settings";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btn_map_barcode_Click);
            // 
            // tbBelt4Processed
            // 
            this.tbBelt4Processed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt4Processed.Location = new System.Drawing.Point(389, 65);
            this.tbBelt4Processed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt4Processed.Name = "tbBelt4Processed";
            this.tbBelt4Processed.ReadOnly = true;
            this.tbBelt4Processed.Size = new System.Drawing.Size(82, 20);
            this.tbBelt4Processed.TabIndex = 101;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(2, 150);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 30);
            this.label9.TabIndex = 85;
            this.label9.Text = "Barcode Lane1";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBarcodeLane1
            // 
            this.tbBarcodeLane1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.tbBarcodeLane1, 2);
            this.tbBarcodeLane1.Location = new System.Drawing.Point(131, 155);
            this.tbBarcodeLane1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeLane1.Name = "tbBarcodeLane1";
            this.tbBarcodeLane1.ReadOnly = true;
            this.tbBarcodeLane1.Size = new System.Drawing.Size(168, 20);
            this.tbBarcodeLane1.TabIndex = 86;
            // 
            // tbBarcodeLane1string
            // 
            this.tbBarcodeLane1string.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.tbBarcodeLane1string, 2);
            this.tbBarcodeLane1string.Location = new System.Drawing.Point(303, 155);
            this.tbBarcodeLane1string.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeLane1string.Name = "tbBarcodeLane1string";
            this.tbBarcodeLane1string.ReadOnly = true;
            this.tbBarcodeLane1string.Size = new System.Drawing.Size(168, 20);
            this.tbBarcodeLane1string.TabIndex = 87;
            // 
            // tbBarcodeLane2string
            // 
            this.tbBarcodeLane2string.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.tbBarcodeLane2string, 2);
            this.tbBarcodeLane2string.Location = new System.Drawing.Point(303, 185);
            this.tbBarcodeLane2string.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeLane2string.Name = "tbBarcodeLane2string";
            this.tbBarcodeLane2string.ReadOnly = true;
            this.tbBarcodeLane2string.Size = new System.Drawing.Size(168, 20);
            this.tbBarcodeLane2string.TabIndex = 88;
            // 
            // btnStartComm
            // 
            this.btnStartComm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStartComm.Location = new System.Drawing.Point(131, 363);
            this.btnStartComm.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnStartComm.Name = "btnStartComm";
            this.btnStartComm.Size = new System.Drawing.Size(82, 34);
            this.btnStartComm.TabIndex = 98;
            this.btnStartComm.Text = "Stop Comm";
            this.btnStartComm.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(2, 240);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 30);
            this.label10.TabIndex = 89;
            this.label10.Text = "BA Signal Lane2";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBASignalLane2
            // 
            this.tbBASignalLane2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBASignalLane2.Location = new System.Drawing.Point(131, 245);
            this.tbBASignalLane2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBASignalLane2.Name = "tbBASignalLane2";
            this.tbBASignalLane2.ReadOnly = true;
            this.tbBASignalLane2.Size = new System.Drawing.Size(82, 20);
            this.tbBASignalLane2.TabIndex = 90;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
            this.tableLayoutPanel1.Controls.Add(this.btnQuit, 4, 11);
            this.tableLayoutPanel1.Controls.Add(this.btn_PlcSettings, 4, 10);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbBASignalLane1, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label18, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbBASignalLane2, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.tbBarcodeLane2, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.tbRail1WidthSP, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tbBarcodeLane2string, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.tbRail2WidthSP, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbRail3WidthSP, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbRail4WidthPV, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbBelt1Processed, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbBelt4InOven, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbBelt2Processed, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbBelt3InOven, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbBelt3Processed, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbBelt2InOven, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbRail3WidthPV, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbBelt1InOven, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbRail2WidthPV, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbRail1WidthPV, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbRail4WidthSP, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbBelt4Processed, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbLightTowerColor, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbBarcodeLane1, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbBarcodeLane1string, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnConnect, 2, 10);
            this.tableLayoutPanel1.Controls.Add(this.btn_map_barcode, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.btnStartComm, 1, 11);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(473, 400);
            this.tableLayoutPanel1.TabIndex = 119;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(473, 400);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.axHellerComm1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "AISIN Line Communication PLC Interface (v1.19)";
            ((System.ComponentModel.ISupportInitialize)(this.axHellerComm1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AxHELLERCOMMLib.AxHellerComm axHellerComm1;
        private System.Windows.Forms.Button btn_PlcSettings;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.TextBox tbRail1WidthPV;
        private System.Windows.Forms.TextBox tbRail2WidthPV;
        private System.Windows.Forms.TextBox tbRail3WidthPV;
        private System.Windows.Forms.TextBox tbRail4WidthPV;
        private System.Windows.Forms.TextBox tbBelt1Processed;
        private System.Windows.Forms.TextBox tbBelt2Processed;
        private System.Windows.Forms.TextBox tbBelt4InOven;
        private System.Windows.Forms.TextBox tbBelt3Processed;
        private System.Windows.Forms.TextBox tbBelt3InOven;
        private System.Windows.Forms.TextBox tbLightTowerColor;
        private System.Windows.Forms.TextBox tbBelt2InOven;
        private System.Windows.Forms.TextBox tbBelt1InOven;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbBelt4Processed;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbBarcodeLane1;
        private System.Windows.Forms.TextBox tbBarcodeLane1string;
        private System.Windows.Forms.TextBox tbBarcodeLane2string;
        private System.Windows.Forms.Button btnStartComm;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbBASignalLane2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

