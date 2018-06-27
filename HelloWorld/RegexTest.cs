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
            var regex = new Regex(pattern, RegexOptions.Compiled);
            var res = regex.IsMatch(str);
            Regex.IsMatch("", pattern, RegexOptions.Compiled);
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
            var pattern = @"^*\.(html|htm)$";
            var str = var ?? "fsafhoi.Htm.hht";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var res = regex.IsMatch("sfafaf.htm");
            Console.WriteLine("{0} is html {1}", str, res);
        }

        public static bool HasChinese(string var = null)
        {
            //System.Text.RegularExpressions.Regex.IsMatch("NLine.Str", @"[\u4e00-\u9fa5]+")
            var parttern = @"[\u4e00-\u9fa5]+";
            var str = var ?? "ok汉s字test";
            var regex = new Regex(parttern, RegexOptions.IgnoreCase);
            var res = regex.IsMatch(str);

            return res;
        }
    }
}
