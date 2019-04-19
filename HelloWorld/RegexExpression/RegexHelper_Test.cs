using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        [TestMethod]
        public void Test_Rgex_SearchText()
        {
            //病人 病房 吃饭
            //病人or病房or吃饭
            //病人 病房&吃饭
            //病人or病房and吃饭
            var target = "Test Source Host im ok you fine";
            //Assert.IsTrue(Regex.IsMatch(target, "(fine+?)"));
            //A or B or C
            //Assert.IsTrue(Regex.IsMatch(target, "(?=.*es.*|.*rc.*|.*in.*)"));
            Assert.IsTrue(Regex.IsMatch(target, ".*es.*|.*rc.*|.*in.*"));
            //A and B and C (?=(?=A)B)C
            Assert.IsTrue(Regex.IsMatch(target, @"(?=(?=.*es.*).*in.*).*os.*"));
            //A and B or C  (?=A)B|C
            Assert.IsTrue(Regex.IsMatch(target, @"(?=.*es.*).*in.*|.*os.*"));


            //Assert.IsFalse(Regex.IsMatch(target, "(est+?) & (rce1+?)"));
            //est or rce1
            //Assert.IsTrue(Regex.IsMatch(target, "(est+?) | (rce+?)"));
            //Assert.IsTrue(Regex.IsMatch(target, "(est+?) | (rce1+?)"));
            //Assert.IsTrue(Regex.IsMatch(target, "(est1+?) | (rce+?)"));
            //
            //Assert.IsTrue(Regex.IsMatch(target, "(es +?) & (ce +?)"));
            //Assert.IsTrue(Regex.IsMatch(target, "(es +?) & (ce +?)"));

        }

    }
}
