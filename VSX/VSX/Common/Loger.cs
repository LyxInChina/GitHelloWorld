
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSX.Common
{
    public class Loger
    {
        /// <summary>
        /// 输出窗口
        /// </summary>
        private static EnvDTE.OutputWindowPane OutputPane { get; set; }

        public Loger(EnvDTE.OutputWindowPane pane)
        {
            OutputPane = pane;
        }

        public static void Init(EnvDTE.OutputWindowPane pane)
        {
            OutputPane = pane;
        }

        public static void WriteInfo(string format, params string[] msg)
        {
            OutputPane.OutputString(string.Format(format, msg) + "\n");
        }
    }

}
