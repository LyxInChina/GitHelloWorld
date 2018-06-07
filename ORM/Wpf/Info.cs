﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wpf
{
    public class Info
    {
        /// <summary>
        /// 打印当前调用函数信息
        /// </summary>
        public static void RecordMethodInfo()
        {
            StackTrace st = new StackTrace(true);
            StackFrame sf = st.GetFrame(1);
            var tid = Thread.CurrentThread.ManagedThreadId;
            var mb = sf.GetMethod();
            int tc = Environment.TickCount;
            string s = string.Format("Time:{0},TID:{1},Method:{2},Line:{3},File:{4}",
                tc, tid, mb.Name, sf.GetFileLineNumber(), sf.GetFileName());
            Debug.WriteLine(s);
        }


    }
}
