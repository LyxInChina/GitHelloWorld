using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.RegexExpression
{
    public class RegexHelper
    {
        public static bool HasChinese(string value)
        {
            System.Diagnostics.Contracts.Contract.Requires(!string.IsNullOrEmpty(value));
            var parttern = @"[\u4e00-\u9fa5]+";
            parttern = @"[\S\s]*[\u4E00-\u9FFF]+[\S\s]*+";
            var regex =new System.Text.RegularExpressions.Regex(parttern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            var res = regex.IsMatch(value);
            return res;
        }

        public static void ReplaceStr()
        {
            
        }

        public static void PickStr()
        {

        }
    }
}
