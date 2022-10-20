//-----------------------------------------------------------------------------
// Program.cs -- startup
//
// Author: Frank Natoli
// E-mail: frankjnatoli@embarqmail.com
// Tel:    973-222-8159
//
// Edit History:
//
// 04-Aug-14  00.01  FJN  Created
// 05-Sep-14  00.02  FJN  Disable form close requiring Quit to exit
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AISIN_WFA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Form1 form1 = new Form1();
                form1.FormClosing += form1.formClosing;
                Application.Run(form1);
            }
            catch (Exception e)
            {
                MessageBox.Show("Main Exception " + e.Message);
            }
        }
    }
}
