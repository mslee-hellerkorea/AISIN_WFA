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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_downstream = new System.Windows.Forms.TableLayoutPanel();
            this.tb_DnPlcConnState = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_dnRetryCount = new System.Windows.Forms.TextBox();
            this.tlp_Upstream = new System.Windows.Forms.TableLayoutPanel();
            this.tb_UpPlcConnState = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_upRetryCount = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tb_Hc2state = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tlp_downstream.SuspendLayout();
            this.tlp_Upstream.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_PlcSettings
            // 
            this.btn_PlcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PlcSettings.Location = new System.Drawing.Point(138, 35);
            this.btn_PlcSettings.Name = "btn_PlcSettings";
            this.btn_PlcSettings.Size = new System.Drawing.Size(129, 27);
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
            this.label1.Size = new System.Drawing.Size(136, 30);
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
            this.label6.Size = new System.Drawing.Size(136, 30);
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
            this.label7.Size = new System.Drawing.Size(136, 30);
            this.label7.TabIndex = 68;
            this.label7.Text = "Boards Processed";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBASignalLane1
            // 
            this.tbBASignalLane1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBASignalLane1.Location = new System.Drawing.Point(142, 215);
            this.tbBASignalLane1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBASignalLane1.Name = "tbBASignalLane1";
            this.tbBASignalLane1.ReadOnly = true;
            this.tbBASignalLane1.Size = new System.Drawing.Size(89, 20);
            this.tbBASignalLane1.TabIndex = 117;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(2, 120);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 30);
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
            this.label18.Size = new System.Drawing.Size(136, 30);
            this.label18.TabIndex = 116;
            this.label18.Text = "BA Signal Lane1";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbRail1WidthSP
            // 
            this.tbRail1WidthSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail1WidthSP.Location = new System.Drawing.Point(142, 5);
            this.tbRail1WidthSP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail1WidthSP.Name = "tbRail1WidthSP";
            this.tbRail1WidthSP.ReadOnly = true;
            this.tbRail1WidthSP.Size = new System.Drawing.Size(89, 20);
            this.tbRail1WidthSP.TabIndex = 70;
            // 
            // tbBarcodeLane2
            // 
            this.tbBarcodeLane2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.tbBarcodeLane2, 2);
            this.tbBarcodeLane2.Location = new System.Drawing.Point(142, 185);
            this.tbBarcodeLane2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeLane2.Name = "tbBarcodeLane2";
            this.tbBarcodeLane2.ReadOnly = true;
            this.tbBarcodeLane2.Size = new System.Drawing.Size(182, 20);
            this.tbBarcodeLane2.TabIndex = 115;
            // 
            // tbRail2WidthSP
            // 
            this.tbRail2WidthSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail2WidthSP.Location = new System.Drawing.Point(235, 5);
            this.tbRail2WidthSP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail2WidthSP.Name = "tbRail2WidthSP";
            this.tbRail2WidthSP.ReadOnly = true;
            this.tbRail2WidthSP.Size = new System.Drawing.Size(89, 20);
            this.tbRail2WidthSP.TabIndex = 71;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(2, 180);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(136, 30);
            this.label17.TabIndex = 114;
            this.label17.Text = "Barcode Lane2";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbRail3WidthSP
            // 
            this.tbRail3WidthSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail3WidthSP.Location = new System.Drawing.Point(328, 5);
            this.tbRail3WidthSP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail3WidthSP.Name = "tbRail3WidthSP";
            this.tbRail3WidthSP.ReadOnly = true;
            this.tbRail3WidthSP.Size = new System.Drawing.Size(89, 20);
            this.tbRail3WidthSP.TabIndex = 72;
            // 
            // btn_map_barcode
            // 
            this.btn_map_barcode.AutoSize = true;
            this.btn_map_barcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_map_barcode.Location = new System.Drawing.Point(2, 34);
            this.btn_map_barcode.Margin = new System.Windows.Forms.Padding(2);
            this.btn_map_barcode.Name = "btn_map_barcode";
            this.btn_map_barcode.Size = new System.Drawing.Size(131, 29);
            this.btn_map_barcode.TabIndex = 113;
            this.btn_map_barcode.Text = "Map barcode";
            this.btn_map_barcode.UseVisualStyleBackColor = true;
            this.btn_map_barcode.Click += new System.EventHandler(this.btn_map_barcode_Click);
            // 
            // tbRail4WidthSP
            // 
            this.tbRail4WidthSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail4WidthSP.Location = new System.Drawing.Point(421, 5);
            this.tbRail4WidthSP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail4WidthSP.Name = "tbRail4WidthSP";
            this.tbRail4WidthSP.ReadOnly = true;
            this.tbRail4WidthSP.Size = new System.Drawing.Size(92, 20);
            this.tbRail4WidthSP.TabIndex = 73;
            // 
            // tbRail1WidthPV
            // 
            this.tbRail1WidthPV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail1WidthPV.Location = new System.Drawing.Point(142, 35);
            this.tbRail1WidthPV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail1WidthPV.Name = "tbRail1WidthPV";
            this.tbRail1WidthPV.ReadOnly = true;
            this.tbRail1WidthPV.Size = new System.Drawing.Size(89, 20);
            this.tbRail1WidthPV.TabIndex = 74;
            // 
            // tbRail2WidthPV
            // 
            this.tbRail2WidthPV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail2WidthPV.Location = new System.Drawing.Point(235, 35);
            this.tbRail2WidthPV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail2WidthPV.Name = "tbRail2WidthPV";
            this.tbRail2WidthPV.ReadOnly = true;
            this.tbRail2WidthPV.Size = new System.Drawing.Size(89, 20);
            this.tbRail2WidthPV.TabIndex = 75;
            // 
            // tbRail3WidthPV
            // 
            this.tbRail3WidthPV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail3WidthPV.Location = new System.Drawing.Point(328, 35);
            this.tbRail3WidthPV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail3WidthPV.Name = "tbRail3WidthPV";
            this.tbRail3WidthPV.ReadOnly = true;
            this.tbRail3WidthPV.Size = new System.Drawing.Size(89, 20);
            this.tbRail3WidthPV.TabIndex = 76;
            // 
            // tbRail4WidthPV
            // 
            this.tbRail4WidthPV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRail4WidthPV.Location = new System.Drawing.Point(421, 35);
            this.tbRail4WidthPV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRail4WidthPV.Name = "tbRail4WidthPV";
            this.tbRail4WidthPV.ReadOnly = true;
            this.tbRail4WidthPV.Size = new System.Drawing.Size(92, 20);
            this.tbRail4WidthPV.TabIndex = 77;
            // 
            // tbBelt1Processed
            // 
            this.tbBelt1Processed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt1Processed.Location = new System.Drawing.Point(142, 65);
            this.tbBelt1Processed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt1Processed.Name = "tbBelt1Processed";
            this.tbBelt1Processed.ReadOnly = true;
            this.tbBelt1Processed.Size = new System.Drawing.Size(89, 20);
            this.tbBelt1Processed.TabIndex = 78;
            // 
            // tbBelt2Processed
            // 
            this.tbBelt2Processed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt2Processed.Location = new System.Drawing.Point(235, 65);
            this.tbBelt2Processed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt2Processed.Name = "tbBelt2Processed";
            this.tbBelt2Processed.ReadOnly = true;
            this.tbBelt2Processed.Size = new System.Drawing.Size(89, 20);
            this.tbBelt2Processed.TabIndex = 79;
            // 
            // tbBelt4InOven
            // 
            this.tbBelt4InOven.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt4InOven.Location = new System.Drawing.Point(421, 95);
            this.tbBelt4InOven.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt4InOven.Name = "tbBelt4InOven";
            this.tbBelt4InOven.ReadOnly = true;
            this.tbBelt4InOven.Size = new System.Drawing.Size(92, 20);
            this.tbBelt4InOven.TabIndex = 106;
            // 
            // tbBelt3Processed
            // 
            this.tbBelt3Processed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt3Processed.Location = new System.Drawing.Point(328, 65);
            this.tbBelt3Processed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt3Processed.Name = "tbBelt3Processed";
            this.tbBelt3Processed.ReadOnly = true;
            this.tbBelt3Processed.Size = new System.Drawing.Size(89, 20);
            this.tbBelt3Processed.TabIndex = 80;
            // 
            // tbBelt3InOven
            // 
            this.tbBelt3InOven.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt3InOven.Location = new System.Drawing.Point(328, 95);
            this.tbBelt3InOven.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt3InOven.Name = "tbBelt3InOven";
            this.tbBelt3InOven.ReadOnly = true;
            this.tbBelt3InOven.Size = new System.Drawing.Size(89, 20);
            this.tbBelt3InOven.TabIndex = 105;
            // 
            // tbLightTowerColor
            // 
            this.tbLightTowerColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLightTowerColor.Location = new System.Drawing.Point(142, 125);
            this.tbLightTowerColor.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbLightTowerColor.Name = "tbLightTowerColor";
            this.tbLightTowerColor.ReadOnly = true;
            this.tbLightTowerColor.Size = new System.Drawing.Size(89, 20);
            this.tbLightTowerColor.TabIndex = 81;
            // 
            // tbBelt2InOven
            // 
            this.tbBelt2InOven.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt2InOven.Location = new System.Drawing.Point(235, 95);
            this.tbBelt2InOven.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt2InOven.Name = "tbBelt2InOven";
            this.tbBelt2InOven.ReadOnly = true;
            this.tbBelt2InOven.Size = new System.Drawing.Size(89, 20);
            this.tbBelt2InOven.TabIndex = 104;
            // 
            // tbBelt1InOven
            // 
            this.tbBelt1InOven.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt1InOven.Location = new System.Drawing.Point(142, 95);
            this.tbBelt1InOven.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt1InOven.Name = "tbBelt1InOven";
            this.tbBelt1InOven.ReadOnly = true;
            this.tbBelt1InOven.Size = new System.Drawing.Size(89, 20);
            this.tbBelt1InOven.TabIndex = 103;
            // 
            // btnQuit
            // 
            this.btnQuit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQuit.Location = new System.Drawing.Point(2, 35);
            this.btnQuit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(106, 27);
            this.btnQuit.TabIndex = 83;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(2, 90);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(136, 30);
            this.label15.TabIndex = 102;
            this.label15.Text = "Boards In Oven";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConnect
            // 
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Location = new System.Drawing.Point(2, 3);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(131, 26);
            this.btnConnect.TabIndex = 84;
            this.btnConnect.Text = "Barcode Settings";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnBarcodeSetting_Click);
            // 
            // tbBelt4Processed
            // 
            this.tbBelt4Processed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBelt4Processed.Location = new System.Drawing.Point(421, 65);
            this.tbBelt4Processed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBelt4Processed.Name = "tbBelt4Processed";
            this.tbBelt4Processed.ReadOnly = true;
            this.tbBelt4Processed.Size = new System.Drawing.Size(92, 20);
            this.tbBelt4Processed.TabIndex = 101;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(2, 150);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 30);
            this.label9.TabIndex = 85;
            this.label9.Text = "Barcode Lane1";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBarcodeLane1
            // 
            this.tbBarcodeLane1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.tbBarcodeLane1, 2);
            this.tbBarcodeLane1.Location = new System.Drawing.Point(142, 155);
            this.tbBarcodeLane1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeLane1.Name = "tbBarcodeLane1";
            this.tbBarcodeLane1.ReadOnly = true;
            this.tbBarcodeLane1.Size = new System.Drawing.Size(182, 20);
            this.tbBarcodeLane1.TabIndex = 86;
            // 
            // tbBarcodeLane1string
            // 
            this.tbBarcodeLane1string.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.tbBarcodeLane1string, 2);
            this.tbBarcodeLane1string.Location = new System.Drawing.Point(328, 155);
            this.tbBarcodeLane1string.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeLane1string.Name = "tbBarcodeLane1string";
            this.tbBarcodeLane1string.ReadOnly = true;
            this.tbBarcodeLane1string.Size = new System.Drawing.Size(185, 20);
            this.tbBarcodeLane1string.TabIndex = 87;
            // 
            // tbBarcodeLane2string
            // 
            this.tbBarcodeLane2string.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.tbBarcodeLane2string, 2);
            this.tbBarcodeLane2string.Location = new System.Drawing.Point(328, 185);
            this.tbBarcodeLane2string.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBarcodeLane2string.Name = "tbBarcodeLane2string";
            this.tbBarcodeLane2string.ReadOnly = true;
            this.tbBarcodeLane2string.Size = new System.Drawing.Size(185, 20);
            this.tbBarcodeLane2string.TabIndex = 88;
            // 
            // btnStartComm
            // 
            this.btnStartComm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStartComm.Location = new System.Drawing.Point(112, 3);
            this.btnStartComm.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnStartComm.Name = "btnStartComm";
            this.btnStartComm.Size = new System.Drawing.Size(107, 26);
            this.btnStartComm.TabIndex = 98;
            this.btnStartComm.Text = "Stop Comm";
            this.btnStartComm.UseVisualStyleBackColor = true;
            this.btnStartComm.Click += new System.EventHandler(this.btnStartComm_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(2, 240);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 30);
            this.label10.TabIndex = 89;
            this.label10.Text = "BA Signal Lane2";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBASignalLane2
            // 
            this.tbBASignalLane2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBASignalLane2.Location = new System.Drawing.Point(142, 245);
            this.tbBASignalLane2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBASignalLane2.Name = "tbBASignalLane2";
            this.tbBASignalLane2.ReadOnly = true;
            this.tbBASignalLane2.Size = new System.Drawing.Size(89, 20);
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
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 2, 10);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 11);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(515, 400);
            this.tableLayoutPanel1.TabIndex = 119;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 3);
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(236, 283);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 84);
            this.groupBox1.TabIndex = 119;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setup";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnConnect, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_PlcSettings, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_map_barcode, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(270, 65);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 2);
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 283);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(227, 84);
            this.groupBox2.TabIndex = 120;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btnStartComm, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnQuit, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(221, 65);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel4, 5);
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.tableLayoutPanel4.Controls.Add(this.tlp_downstream, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.tlp_Upstream, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 370);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(515, 30);
            this.tableLayoutPanel4.TabIndex = 121;
            // 
            // tlp_downstream
            // 
            this.tlp_downstream.ColumnCount = 3;
            this.tlp_downstream.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.85714F));
            this.tlp_downstream.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.85714F));
            this.tlp_downstream.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlp_downstream.Controls.Add(this.tb_DnPlcConnState, 1, 0);
            this.tlp_downstream.Controls.Add(this.label4, 0, 0);
            this.tlp_downstream.Controls.Add(this.tb_dnRetryCount, 2, 0);
            this.tlp_downstream.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_downstream.Location = new System.Drawing.Point(327, 0);
            this.tlp_downstream.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_downstream.Name = "tlp_downstream";
            this.tlp_downstream.RowCount = 1;
            this.tlp_downstream.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_downstream.Size = new System.Drawing.Size(188, 30);
            this.tlp_downstream.TabIndex = 2;
            // 
            // tb_DnPlcConnState
            // 
            this.tb_DnPlcConnState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_DnPlcConnState.Location = new System.Drawing.Point(83, 5);
            this.tb_DnPlcConnState.Name = "tb_DnPlcConnState";
            this.tb_DnPlcConnState.ReadOnly = true;
            this.tb_DnPlcConnState.Size = new System.Drawing.Size(74, 20);
            this.tb_DnPlcConnState.TabIndex = 0;
            this.tb_DnPlcConnState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 30);
            this.label4.TabIndex = 1;
            this.label4.Text = "Downstream : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_dnRetryCount
            // 
            this.tb_dnRetryCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_dnRetryCount.Location = new System.Drawing.Point(163, 5);
            this.tb_dnRetryCount.Name = "tb_dnRetryCount";
            this.tb_dnRetryCount.ReadOnly = true;
            this.tb_dnRetryCount.Size = new System.Drawing.Size(22, 20);
            this.tb_dnRetryCount.TabIndex = 2;
            this.tb_dnRetryCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tlp_Upstream
            // 
            this.tlp_Upstream.ColumnCount = 3;
            this.tlp_Upstream.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.85714F));
            this.tlp_Upstream.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.85714F));
            this.tlp_Upstream.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlp_Upstream.Controls.Add(this.tb_UpPlcConnState, 1, 0);
            this.tlp_Upstream.Controls.Add(this.label3, 0, 0);
            this.tlp_Upstream.Controls.Add(this.tb_upRetryCount, 2, 0);
            this.tlp_Upstream.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Upstream.Location = new System.Drawing.Point(140, 0);
            this.tlp_Upstream.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Upstream.Name = "tlp_Upstream";
            this.tlp_Upstream.RowCount = 1;
            this.tlp_Upstream.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Upstream.Size = new System.Drawing.Size(187, 30);
            this.tlp_Upstream.TabIndex = 1;
            // 
            // tb_UpPlcConnState
            // 
            this.tb_UpPlcConnState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_UpPlcConnState.Location = new System.Drawing.Point(83, 5);
            this.tb_UpPlcConnState.Name = "tb_UpPlcConnState";
            this.tb_UpPlcConnState.ReadOnly = true;
            this.tb_UpPlcConnState.Size = new System.Drawing.Size(74, 20);
            this.tb_UpPlcConnState.TabIndex = 0;
            this.tb_UpPlcConnState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 30);
            this.label3.TabIndex = 1;
            this.label3.Text = "Upstream : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_upRetryCount
            // 
            this.tb_upRetryCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_upRetryCount.Location = new System.Drawing.Point(163, 5);
            this.tb_upRetryCount.Name = "tb_upRetryCount";
            this.tb_upRetryCount.ReadOnly = true;
            this.tb_upRetryCount.Size = new System.Drawing.Size(21, 20);
            this.tb_upRetryCount.TabIndex = 2;
            this.tb_upRetryCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.tb_Hc2state, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(140, 30);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // tb_Hc2state
            // 
            this.tb_Hc2state.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Hc2state.Location = new System.Drawing.Point(73, 5);
            this.tb_Hc2state.Name = "tb_Hc2state";
            this.tb_Hc2state.ReadOnly = true;
            this.tb_Hc2state.Size = new System.Drawing.Size(64, 20);
            this.tb_Hc2state.TabIndex = 0;
            this.tb_Hc2state.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "HC2 State : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(515, 400);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "AISIN Line Communication PLC Interface (v1.19)";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tlp_downstream.ResumeLayout(false);
            this.tlp_downstream.PerformLayout();
            this.tlp_Upstream.ResumeLayout(false);
            this.tlp_Upstream.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TextBox tb_Hc2state;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tlp_downstream;
        private System.Windows.Forms.TextBox tb_DnPlcConnState;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tlp_Upstream;
        private System.Windows.Forms.TextBox tb_UpPlcConnState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_upRetryCount;
        private System.Windows.Forms.TextBox tb_dnRetryCount;
    }
}

