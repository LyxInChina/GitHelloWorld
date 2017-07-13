using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEFContract;

namespace MEFSimple
{
    public class CalculatorShow : ICalShow
    {
        public string Show(int a)
        {
            return "ok int :: " + a;
        }

        public string Show(int a, int b)
        {
            return "ok int :: " + a + ",int :: " + b;
        }
    }
}
