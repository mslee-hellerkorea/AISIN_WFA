//-----------------------------------------------------------------------------
// Program.cs -- startup
//
// Author: Frank Natoli
// E-mail: frankjnatoli@embarqmail.com
// Tel:    973-222-8159
//
// Edit History:
//
// 04-Aug-14  00.01         FJN  Created
// 05-Sep-14  00.02         FJN  Disable form close requiring Quit to exit
// 21-Nov-22  01.01.04.00   MSL  Added feature of duplicate execution prevention.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using AISIN_WFA.Utility;

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
            // 21-Nov-22  01.01.04.00   MSL  Added feature of duplicate execution prevention.
            if (IsExistProcess(Process.GetCurrentProcess().ProcessName))
            {
                MessageBox.Show("Line Communication Software Already running");
                HLog.log(HLog.eLog.EVENT, "Line Communication Software Already running");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }

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
        
        private static bool IsExistProcess(string processName)
        {
            // 21-Nov-22  01.01.04.00   MSL  Added feature of duplicate execution prevention.
            Process[] process = Process.GetProcesses();
            int cnt = 0;
            foreach (var p in process)
            {
                if (p.ProcessName == processName)
                    cnt++;
                if (cnt > 1)
                    return true;
            }
            return false;
        }
    }
}
