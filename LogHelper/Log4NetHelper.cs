using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Core;
using log4net.Repository;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Threading;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

/* Using log4net
 * step 1: using log4net.dll；
 * step 2: 添加程序集特性,指定配置文件；
 * step 3: 添加配置文件
 * step 4: 获取ILog对象
 * step 5: 使用
 */

namespace LogHelper
{
    public class Log4NetHelper:ILogHelper
    {
        public static readonly ILogHelper Log = new Log4NetHelper();

        private readonly ILog logger;
        private readonly ILoggerRepository repository;

        public Log4NetHelper()
        {
            repository = log4net.LogManager.GetRepository(Assembly.GetCallingAssembly());            
        }

        private void InnerLog(Type type,Level level,string msg,Exception ex)
        {

            repository.GetLogger(type.FullName).Log(type, level, msg, ex);
        }

        public void Info(string msg, Exception ex=null)
        {
            StackTrace st = new StackTrace(true);
            StackFrame sf = st.GetFrame(1);
            var tid = Thread.CurrentThread.ManagedThreadId;
            var type = sf.GetType();

            var calling = Assembly.GetCallingAssembly();
            var type2 = calling.GetType();
            InnerLog(type2, Level.Info, msg, ex);
            logger.Info(msg,ex);
        }

        public void Warn(string msg, Exception ex=null)
        {
            logger.Warn(msg, ex);
        }

        public void Error(string msg, Exception ex = null)
        {
            logger.Error(msg, ex);
        }
    }
}
