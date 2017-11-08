using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HelloWorld
{
    public class RegexTest
    {
        public static void HasStr()
        {
            var str = "htt00";
            var pattern = "^[a-zA-Z0-9_.]{4,8}$";
            //^[a-zA-Z][a-zA-Z0-9_]{4,15}$ 
            /// (\d+)\.(\d+)\.(\d+)\.(\d+)/g //
            var regex = new Regex(pattern, RegexOptions.None);
            var res = regex.IsMatch(str);
            Console.WriteLine("{0} has {1} {2}", str, pattern, res);
        }

        public static void ReplaceStr()
        {

        }

        public static void PickStr()
        {

        }

        public static void IsHtml(string var=null)
        {
            var str = var ?? "fsafhoi.Htm.hht";
            var pattern = @"^*\.(html|htm)$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var res = regex.IsMatch("sfafaf.htm");
            Console.WriteLine("{0} is html {1}", str, res);
        }
    }
}
