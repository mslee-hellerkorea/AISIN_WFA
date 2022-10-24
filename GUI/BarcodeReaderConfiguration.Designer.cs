
namespace AISIN_WFA.GUI
{
    partial class BarcodeReaderConfiguration
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbBarcodeReaderPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBarcodeReaderSave = new System.Windows.Forms.Button();
            this.btnBarcodeReaderCancel = new System.Windows.Forms.Button();
            this.cbBarcodeReaderDataBits = new System.Windows.Forms.ComboBox();
            this.cbBarcodeReaderParity = new System.Windows.Forms.ComboBox();
            this.cbBarcodeReaderBitRate = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbBarcodeReaderStopBits = new System.Windows.Forms.ComboBox();
            this.cbBarcodeReaderFlowControl = new System.Windows.Forms.ComboBox();
            this.cbBarcodeReaderType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port";
            // 
            // cbBarcodeReaderPort
            // 
            this.cbBarcodeReaderPort.FormattingEnabled = true;
            this.cbBarcodeReaderPort.Location = new System.Drawing.Point(146, 25);
            this.cbBarcodeReaderPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbBarcodeReaderPort.Name = "cbBarcodeReaderPort";
            this.cbBarcodeReaderPort.Size = new System.Drawing.Size(147, 28);
            this.cbBarcodeReaderPort.Sorted = true;
            this.cbBarcodeReaderPort.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data Bits";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Parity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Bit Rate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 275);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Flow Control";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 325);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Type";
            // 
            // btnBarcodeReaderSave
            // 
            this.btnBarcodeReaderSave.Location = new System.Drawing.Point(22, 388);
            this.btnBarcodeReaderSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBarcodeReaderSave.Name = "btnBarcodeReaderSave";
            this.btnBarcodeReaderSave.Size = new System.Drawing.Size(84, 45);
            this.btnBarcodeReaderSave.TabIndex = 12;
            this.btnBarcodeReaderSave.Text = "Save";
            this.btnBarcodeReaderSave.UseVisualStyleBackColor = true;
            this.btnBarcodeReaderSave.Click += new System.EventHandler(this.btnBarcodeReaderSave_Click);
            // 
            // btnBarcodeReaderCancel
            // 
            this.btnBarcodeReaderCancel.Location = new System.Drawing.Point(146, 388);
            this.btnBarcodeReaderCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBarcodeReaderCancel.Name = "btnBarcodeReaderCancel";
            this.btnBarcodeReaderCancel.Size = new System.Drawing.Size(84, 46);
            this.btnBarcodeReaderCancel.TabIndex = 13;
            this.btnBarcodeReaderCancel.Text = "Cancel";
            this.btnBarcodeReaderCancel.UseVisualStyleBackColor = true;
            this.btnBarcodeReaderCancel.Click += new System.EventHandler(this.btnBarcodeReaderCancel_Click);
            // 
            // cbBarcodeReaderDataBits
            // 
            this.cbBarcodeReaderDataBits.FormattingEnabled = true;
            this.cbBarcodeReaderDataBits.Location = new System.Drawing.Point(146, 75);
            this.cbBarcodeReaderDataBits.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbBarcodeReaderDataBits.Name = "cbBarcodeReaderDataBits";
            this.cbBarcodeReaderDataBits.Size = new System.Drawing.Size(157, 28);
            this.cbBarcodeReaderDataBits.TabIndex = 14;
            // 
            // cbBarcodeReaderParity
            // 
            this.cbBarcodeReaderParity.FormattingEnabled = true;
            this.cbBarcodeReaderParity.Location = new System.Drawing.Point(146, 125);
            this.cbBarcodeReaderParity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbBarcodeReaderParity.Name = "cbBarcodeReaderParity";
            this.cbBarcodeReaderParity.Size = new System.Drawing.Size(157, 28);
            this.cbBarcodeReaderParity.TabIndex = 15;
            // 
            // cbBarcodeReaderBitRate
            // 
            this.cbBarcodeReaderBitRate.FormattingEnabled = true;
            this.cbBarcodeReaderBitRate.Location = new System.Drawing.Point(146, 225);
            this.cbBarcodeReaderBitRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbBarcodeReaderBitRate.Name = "cbBarcodeReaderBitRate";
            this.cbBarcodeReaderBitRate.Size = new System.Drawing.Size(157, 28);
            this.cbBarcodeReaderBitRate.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "Stop Bits";
            // 
            // cbBarcodeReaderStopBits
            // 
            this.cbBarcodeReaderStopBits.FormattingEnabled = true;
            this.cbBarcodeReaderStopBits.Location = new System.Drawing.Point(146, 175);
            this.cbBarcodeReaderStopBits.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbBarcodeReaderStopBits.Name = "cbBarcodeReaderStopBits";
            this.cbBarcodeReaderStopBits.Size = new System.Drawing.Size(157, 28);
            this.cbBarcodeReaderStopBits.TabIndex = 18;
            // 
            // cbBarcodeReaderFlowControl
            // 
            this.cbBarcodeReaderFlowControl.FormattingEnabled = true;
            this.cbBarcodeReaderFlowControl.Location = new System.Drawing.Point(146, 275);
            this.cbBarcodeReaderFlowControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbBarcodeReaderFlowControl.Name = "cbBarcodeReaderFlowControl";
            this.cbBarcodeReaderFlowControl.Size = new System.Drawing.Size(157, 28);
            this.cbBarcodeReaderFlowControl.TabIndex = 19;
            // 
            // cbBarcodeReaderType
            // 
            this.cbBarcodeReaderType.FormattingEnabled = true;
            this.cbBarcodeReaderType.Location = new System.Drawing.Point(146, 325);
            this.cbBarcodeReaderType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbBarcodeReaderType.Name = "cbBarcodeReaderType";
            this.cbBarcodeReaderType.Size = new System.Drawing.Size(157, 28);
            this.cbBarcodeReaderType.TabIndex = 20;
            // 
            // BarcodeReaderConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(423, 471);
            this.Controls.Add(this.cbBarcodeReaderType);
            this.Controls.Add(this.cbBarcodeReaderFlowControl);
            this.Controls.Add(this.cbBarcodeReaderStopBits);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbBarcodeReaderBitRate);
            this.Controls.Add(this.cbBarcodeReaderParity);
            this.Controls.Add(this.cbBarcodeReaderDataBits);
            this.Controls.Add(this.btnBarcodeReaderCancel);
            this.Controls.Add(this.btnBarcodeReaderSave);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbBarcodeReaderPort);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BarcodeReaderConfiguration";
            this.Text = "Barcode Reader Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBarcodeReaderSave;
        private System.Windows.Forms.Button btnBarcodeReaderCancel;
        private System.Windows.Forms.ComboBox cbBarcodeReaderPort;
        private System.Windows.Forms.ComboBox cbBarcodeReaderDataBits;
        private System.Windows.Forms.ComboBox cbBarcodeReaderParity;
        private System.Windows.Forms.ComboBox cbBarcodeReaderBitRate;
        private System.Windows.Forms.ComboBox cbBarcodeReaderStopBits;
        private System.Windows.Forms.ComboBox cbBarcodeReaderFlowControl;
        private System.Windows.Forms.ComboBox cbBarcodeReaderType;

        public int result;
        public int dataBits;
        public System.IO.Ports.StopBits stopBits;
        public System.IO.Ports.Parity parity;
        public System.IO.Ports.Handshake flowControl;
        public int bitRate;
        public int readerType;
    }
}