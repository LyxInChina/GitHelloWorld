using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagedDll
{
    public class Class1
    {
        public static int Start(string argument)
        {
            Application.Run(new MainForm());
            return 0;
        }
    }
}
