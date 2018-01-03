using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using HelloWorld;

[assembly:CLSCompliant(true)]
namespace EmitExamples
{
    [StructLayout(LayoutKind.Sequential)]
    class EmitProgram
    {
        /// <summary>
        /// 用来调用动态方法的委托
        /// </summary>
        private delegate void HelloWorldDelegate();


        static void Main(string[] args)
        {
            MakeHelloWorldFunc();
            FCurrentFunction();
            RegexTest.HasChinese("sdfioafia汉asf");
            Console.ReadKey();
        }

        public static void MakeHelloWorldFunc()
        {
            //定义一个名为HelloWorld的动态方法，没有返回值，没有参数
            DynamicMethod helloWorldMethod = new DynamicMethod("HelloWorld", null, null);

            //创建一个MSIL生成器，为动态方法生成代码
            ILGenerator helloWorldIL = helloWorldMethod.GetILGenerator();

            //将要输出的Hello World!字符创加载到堆栈上
            helloWorldIL.Emit(OpCodes.Ldstr, "Hello World!");
            //调用Console.WriteLine(string)方法输出Hello World!
            helloWorldIL.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            //方法结束，返回
            helloWorldIL.Emit(OpCodes.Ret);

            //完成动态方法的创建，并且获取一个可以执行该动态方法的委托
            HelloWorldDelegate HelloWorld = (HelloWorldDelegate)helloWorldMethod.CreateDelegate(typeof(HelloWorldDelegate));

            //执行动态方法，将在屏幕上打印Hello World!
            HelloWorld();

            Console.WriteLine("Hello World!");
        }

        public static void NumberFormat()
        {
            var cc = new Clacc();
            cc.bol = true;
            cc.a = 0x88;
            cc.b = 16;
            cc.c = 16;
            cc.d = 166d;
            cc.str = "abcdefg";
            decimal dd = 89m, dd2 = 89M;
            float ff = 89f, ff2 = 89F;
            long ll = 89l, ll2 = 89L;
            double ddo = 89d, ddo2 = 89D;
            byte bb = 89;
            int ii = 32, ii2 = 0x32;
        }

        public static void StructSize()
        {
            var a = new MemoryAlignA() { b = 98, c = 97, d = 96 };
            var b = new MemoryAlignB() { b = 98, c = 97, d = 96 };
            unsafe
            {
                //Console.WriteLine(Marshal.SizeOf(a));
                //Console.WriteLine(Marshal.SizeOf(b));
                Console.WriteLine(sizeof(MemoryAlignA));
                Console.WriteLine(sizeof(MemoryAlignB));
                Console.WriteLine(sizeof(MemoryAlignC));
                //Console.WriteLine(sizeof(bool));
            }
        }

        public static void FCurrentFunction()
        {
            Console.WriteLine(":@@@@:" + System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        public static void FileInfo()
        {

        }

    }


         

    [StructLayout(LayoutKind.Explicit)]
    public class Clacc
    {
        [FieldOffset(0)]
        public bool bol;

        [FieldOffset(1)]
        public int a;

        [FieldOffset(5)]
        public byte b;

        [FieldOffset(6)]
        public short c;

        [FieldOffset(8)]
        public double d;

        [FieldOffset(16)]
        public string str;
    }

    //[StructLayout(LayoutKind.Sequential)]
    public struct MemoryAlignA
    {//内存成员布局 编译器执行内存对齐优化 从小到大 或者从大到小 
        //8+1+2+4
        MemoryAlignC cc;
        public byte b;
        public short c;
        public int d;
    }

    //[StructLayout(LayoutKind.Sequential)]
    public struct MemoryAlignB
    {
        public short c;
        public int d;
        MemoryAlignC cc;
        public byte b;
    }

    public struct MemoryAlignC
    {   //1+1+1+01+4 = 8
        bool a;//1
        bool a1;//1
        bool a2;//1
        int b;//4
    }

    public class ClacD
    {
        public bool bol;

        public int a;

        public byte b;

        public short c;

        public double d;

        public string str;
    }

    public class A
    {
        public int a { get; set; }
        public string b { get; set; }
        public long c { get; set; }
        public short d { get; set; }
        public uint e { get; set; }
        public ushort f { get; set; }
        public ulong g { get; set; }
        public char h { get; set; }
        public int[] i { get; set; } 
        public List<int> k { get; set; } 
        private ushort j { get; set; }

        //~A()
        //{

        //}

        public void Finalize()
        {

        }
    }
}
