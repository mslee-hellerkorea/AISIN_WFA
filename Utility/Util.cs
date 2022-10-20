using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AISIN_WFA.Utility
{
    public class Util
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lp1, string lp2);
    }
}
