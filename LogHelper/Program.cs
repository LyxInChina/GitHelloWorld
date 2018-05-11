using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LogHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            TestLog4Net();
            var i = Console.ReadLine();
        }
        static void TestLog4Net()
        {
            new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Convert.ToInt32("sss");
                    }
                    catch (Exception ex)
                    {
                        Log4NetHelper.Log.Error("", ex);
                    }
                    Thread.Sleep(200);
                }
            })
            { IsBackground = true }.Start();

        }
    }
}
