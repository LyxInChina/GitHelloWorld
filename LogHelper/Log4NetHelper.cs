using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Core;

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

        public Log4NetHelper()
        {
            logger = log4net.LogManager.GetLogger(typeof(Log4NetHelper));
        }

        public void Info(string msg, Exception ex=null)
        {
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
