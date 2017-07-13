using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MEFContract;

/// <summary>
/// 具体实现
/// </summary>
namespace MEFSimple
{
    [Export(typeof(ICal))]
    public class Calculator:ICal
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public IList<string> GetOperations()
        {
            return new List<string>()
            {"+","-","*","/"};
        }
    }
}
