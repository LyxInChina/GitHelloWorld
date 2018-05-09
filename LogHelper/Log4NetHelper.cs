using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

/* Using log4net
 * step 1: using log4net.dll；
 * step 2: 添加程序集特性,指定配置文件；
 * step 3: 添加配置文件
 * step 4: 
 */

namespace LogHelper
{
    public class Log4NetHelper
    {

    }
}
