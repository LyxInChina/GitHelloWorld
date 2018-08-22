﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogHelper
{
    public class StackInfo
    {
        public static Type GetCallingType()
        {
            StackTrace st = new StackTrace(true);
            StackFrame sf = st.GetFrame(2);
            //var tid = Thread.CurrentThread.ManagedThreadId;
            var func = sf.GetMethod();
            var type = func.DeclaringType;
            return type;
        }
    }
}
