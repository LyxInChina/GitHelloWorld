using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace HelloWorld.Base
{
    /// <summary>
    /// 函数调用信息
    /// </summary>
    public class CallInfo
    {
        public static string RecordMethodInfo()
        {
            var minfo = GetCallMethodInfo(2);
            return string.Format("Tid:{0},Assembly:{1},MethodInfo:{2}"
                , Thread.CurrentThread.ManagedThreadId
                , Assembly.GetCallingAssembly().FullName
                , minfo.ToString()
                );
        }

        public static string[] GetCallMethodInfo(uint udeep =1)
        {
            StackTrace st = new StackTrace(true);
            var frames = st.GetFrames();
            if (udeep>=frames.Length)
            {
                udeep = (uint)frames.Length - 1;
            }
            StackFrame sf = st.GetFrame((int)udeep);
            return new string[]
            {
                sf.GetMethod().Name,
                sf.GetFileName(),
                sf.GetFileLineNumber().ToString(),
            };
        }

        public static void GetCallMemberName([CallerMemberName] string name ="")
        {

        }

        public static void GetLineNumber([CallerLineNumber] int lineNumber =-1)
        {

        }

        public static void GetFilePath([CallerFilePath] string filePath="")
        {

        }

    }
}
