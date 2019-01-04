using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.RegexExpression
{
    [TestClass]
    public class RegexHelper_Test
    {
        [TestMethod]
        public void Test_HasChinese()
        {
            List<string> ss =new List<string>(){ "12345", "abcde", "!@#$%^&*()", "；’、。？，。", "absced锕adfafsa324" };
            bool[] res = new bool[ss.Count];
            for (int i = 0; i < ss.Count; i++)
            {
                res[i] = RegexHelper.HasChinese(ss[i]);
            }
            for (int i = 0; i < res.Length; i++)
            {
                if (i != res.Length - 1)
                {
                    Assert.AreEqual(res[i], false);
                }
                else
                {
                    Assert.AreEqual(res[i], true);
                }
            }
        }
    }
}
