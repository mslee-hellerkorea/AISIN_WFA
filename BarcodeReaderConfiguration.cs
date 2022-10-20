using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AISIN_WFA
{
    public partial class BarcodeReaderConfiguration : Form
    {
        public BarcodeReaderConfiguration()
        {
            InitializeComponent();

            // populate Port ComboBox
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                this.cbBarcodeReaderPort.Items.Add(port);
            this.cbBarcodeReaderPort.SelectedIndex = 0;

            // populate DataBits ComboBox
            this.cbBarcodeReaderDataBits.Items.Add("5");
            this.cbBarcodeReaderDataBits.Items.Add("6");
            this.cbBarcodeReaderDataBits.Items.Add("7");
            this.cbBarcodeReaderDataBits.Items.Add("8");
            this.cbBarcodeReaderDataBits.SelectedIndex = 3;

            // populate Parity ComboBox
            this.cbBarcodeReaderParity.Items.Add("None");
            this.cbBarcodeReaderParity.Items.Add("Even");
            this.cbBarcodeReaderParity.Items.Add("Odd");
            this.cbBarcodeReaderParity.Items.Add("Mark");
            this.cbBarcodeReaderParity.Items.Add("Space");
            this.cbBarcodeReaderParity.SelectedIndex = 0;

            // populate StopBits ComboBox
            this.cbBarcodeReaderStopBits.Items.Add("None");
            this.cbBarcodeReaderStopBits.Items.Add("1");
            this.cbBarcodeReaderStopBits.Items.Add("1.5");
            this.cbBarcodeReaderStopBits.Items.Add("2");
            this.cbBarcodeReaderStopBits.SelectedIndex = 1;

            // populate BitRate ComboBox
            this.cbBarcodeReaderBitRate.Items.Add("9600");
            this.cbBarcodeReaderBitRate.Items.Add("19200");
            this.cbBarcodeReaderBitRate.Items.Add("38400");
            this.cbBarcodeReaderBitRate.Items.Add("57600");
            this.cbBarcodeReaderBitRate.Items.Add("115200");
            this.cbBarcodeReaderBitRate.SelectedIndex = 4;

            // populate FlowControl ComboBox
            this.cbBarcodeReaderFlowControl.Items.Add("None");
            this.cbBarcodeReaderFlowControl.Items.Add("RTS");
            this.cbBarcodeReaderFlowControl.Items.Add("XON+XOFF");
            this.cbBarcodeReaderFlowControl.Items.Add("RTS+XON+XOFF");
            this.cbBarcodeReaderFlowControl.SelectedIndex = 0;

            // populate Type ComboBox
            this.cbBarcodeReaderType.Items.Add("Code Reader 2.0");
            this.cbBarcodeReaderType.Items.Add("Reader with CR-LF");
            this.cbBarcodeReaderType.SelectedIndex = 0;
        }

        private void btnBarcodeReaderSave_Click(object sender, EventArgs e)
        {
            // remember selected data bits
            switch (this.cbBarcodeReaderDataBits.SelectedIndex)
            {
                case 0:
                    this.dataBits = 5;
                    break;
                case 1:
                    this.dataBits = 6;
                    break;
                case 2:
                    this.dataBits = 7;
                    break;
                case 3:
                default:
                    this.dataBits = 8;
                    break;
            }

            // remember selected stop bits
            switch (this.cbBarcodeReaderStopBits.SelectedIndex)
            {
                case 0:
                    this.stopBits = System.IO.Ports.StopBits.None;
                    break;
                case 1:
                default :
                    this.stopBits = System.IO.Ports.StopBits.One;
                    break;
                case 2:
                    this.stopBits = System.IO.Ports.StopBits.OnePointFive;
                    break;
                case 3:
                    this.stopBits = System.IO.Ports.StopBits.Two;
                    break;
            }

            // remember selected parity
            switch (this.cbBarcodeReaderParity.SelectedIndex)
            {
                case 0 :
                default :
                    this.parity = System.IO.Ports.Parity.None;
                    break;
                case 1 :
                    this.parity = System.IO.Ports.Parity.Even;
                    break;
                case 2 :
                    this.parity = System.IO.Ports.Parity.Odd;
                    break;
                case 3 :
                    this.parity = System.IO.Ports.Parity.Mark;
                    break;
                case 4 :
                    this.parity = System.IO.Ports.Parity.Space;
                    break;
            }

            // remember selected flow control
            switch (this.cbBarcodeReaderFlowControl.SelectedIndex)
            {
                case 0:
                default:
                    this.flowControl = System.IO.Ports.Handshake.None;
                    break;
                case 1 :
                    this.flowControl = System.IO.Ports.Handshake.RequestToSend;
                    break;
                case 2 :
                    this.flowControl = System.IO.Ports.Handshake.XOnXOff;
                    break;
                case 3 :
                    this.flowControl = System.IO.Ports.Handshake.RequestToSendXOnXOff;
                    break;
            }

            // remember selected bit rate
            switch (this.cbBarcodeReaderBitRate.SelectedIndex)
            {
                case 0:
                    this.bitRate = 9600;
                    break;
                case 1:
                    this.bitRate = 19200;
                    break;
                case 2:
                    this.bitRate = 38400;
                    break;
                case 3:
                    this.bitRate = 57600;
                    break;
                case 4:
                default :
                    this.bitRate = 115200;
                    break;
            }

            // remember reader type
            this.readerType = this.cbBarcodeReaderType.SelectedIndex;

            // indicate OK
            this.result = 1;
            this.Close();
        }

        private void btnBarcodeReaderCancel_Click(object sender, EventArgs e)
        {
            this.result = 0;
            this.Close();
        }
    }
}
