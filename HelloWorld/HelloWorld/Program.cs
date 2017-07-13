using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;

namespace EmitExamples.HelloWorld
{
    class Program
    {
        /// <summary>
        /// 用来调用动态方法的委托
        /// </summary>
        private delegate void HelloWorldDelegate();

        static void Main(string[] args)
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
        }
    }
}
