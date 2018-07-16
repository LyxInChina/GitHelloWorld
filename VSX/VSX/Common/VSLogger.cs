using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSX.Common
{
    /// <summary>
    /// VS日志类
    /// </summary>
    [Export(typeof(ILogger))]
    public class VsLogger : ILogger
    {
        /// <summary>
        /// 输出窗口
        /// </summary>
        private EnvDTE.OutputWindowPane OutputPane { get; set; }

        public void Init(EnvDTE.OutputWindowPane outputPane)
        {
            OutputPane = outputPane;
        }

        public void Clear()
        {
            OutputPane.Clear();
        }

        public void Error(string format, params string[] args)
        {
            Info(format, true, args);
        }

        public void Info(string format, bool trackCode = false, params string[] args)
        {
            OutputPane.OutputString(String.Format(format, args) + "\n");
            if (trackCode)
                GetExInfo();
        }

        public void Info(string format, params string[] args)
        {
            OutputPane.OutputString(String.Format(format, args) + "\n");
        }

        public void GetExInfo()
        {
            StackTrace st = new StackTrace(true);
            System.Diagnostics.StackFrame frame = st.GetFrame(3);
            var info = string.Format("Method:{0},{1},Line:{2} \n", frame.GetMethod().Name, frame.GetFileName(), frame.GetFileLineNumber());
            OutputPane.OutputString("" + info);
        }
    }

}
