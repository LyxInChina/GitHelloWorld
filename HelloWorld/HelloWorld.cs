using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace HelloWorld
{
    public class HelloWorld
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello world");
            Console.ReadLine();
            IsDebugMode();

            Console.ReadKey();
        }

        static void IsDebugMode()
        {
            Console.WriteLine("Is Debug Mode?");
            if (Debugger.IsAttached)
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");
            }
        }

        static void ImmStatus()
        {
            var h = Process.GetCurrentProcess().Handle;
            if (ImmGetOpenStatus(h))
            {
                Console.WriteLine("Imm status:{0}",true);
            }
        }

        static void PrintCurrentAsssemblyInfo()
        {
            var a = Assembly.GetCallingAssembly().GetCustomAttribute(typeof(AssemblyVersionAttribute));
            if(a!=null && a is AssemblyVersionAttribute)
            {
                Console.WriteLine((a as AssemblyVersionAttribute).Version);
            }
        }

        //https://docs.microsoft.com/en-us/windows/desktop/api/Imm/
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd); //获取当前正在输入的窗口的输入法句柄

        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr hIMC);//判断当前输入法是否是打开的

        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr hIMC, ref int conversion, ref int sentence); //获取当前输入法的状态
        [DllImport("imm32.dll")]
        public static extern bool ImmSetConversionStatus(IntPtr hIMC, int conversion, int sentence);//设置输入法的状态

        [DllImport("imm32.dll")]
        public static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);//释放上下文关联
    }
}
