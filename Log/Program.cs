using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Log
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestLog4Net();
            TestWindowsLog();
            var i = Console.ReadLine();
        }

        static void TestLog4Net()
        {
            while (true)
            {
                try
                {
                    Convert.ToInt32("sss");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Log4NetHelper.Log.Error(ex, "");
                }
                Thread.Sleep(200);
            }
        }

        static void TestNLog()
        {
            while (true)
            {
                try
                {
                    Convert.ToInt32("sss");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    NLogHelper.Log.Error(ex, "");
                }
                Thread.Sleep(200);
            }
        }

        static void TestWindowsLog()
        {
            while (true)
            {
                try
                {
                    Convert.ToInt32("sss");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    WindowsLog.GetLogger().Debug(ex, "this is debug msg.");
                    WindowsLog.GetLogger().Error(ex, "this is error msg.");
                    WindowsLog.GetLogger().Info(ex, "this is info msg.");
                    WindowsLog.GetLogger().Warn(ex, "this is warn msg.");
                }
                Thread.Sleep(200);
            }
        }
    }
}
