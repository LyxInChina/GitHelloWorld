using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEFContract;

namespace MEFSimple
{
    [Export("MEFSimple.DefinC")]
    public class DefineC
    {

        public string GetStr()
        {
            return "Definc ";
        }
    }

    public class DefineD
    {
        [Export("Add",typeof(Func<int,int,int>))]
        [ExportMetadata("n1","v1")]
        public int Add(int a, int b)
        {
            return a + b;
        }

        [Export("RefeObj",typeof(Action<string,string>))]
        [ExportMetadata("n2", "v2")]
        public void RefeObj(string s1, string s2)
        {
            s1 += s2;
        }

        [MetaDataEx("Subtract",typeof(Func<double,double,double>), V = "VX")]
        public double Subtract(double d1,double d2)
        {
            return d1 - d2;
        }

    }

}
