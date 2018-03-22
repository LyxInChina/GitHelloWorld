using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.CLR
{
    public abstract class BaseClass
    {
        public virtual void BFunc1()
        {

        }
        public void NFunc1()
        {

        }

    }
    public sealed class CSharpPerformancePromote: BaseClass
    {
        public CSharpPerformancePromote()
        {

        }
        public static void Func1()
        {

        }
        public void Func2()
        {

        }
        public void Func3()
        {

        }
        public override void BFunc1()
        {
            //base.BFunc1();
        }

        public new void NFunc1()
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static void Test()
        {
            var obj = new CSharpPerformancePromote();
            CSharpPerformancePromote.Func1();//call
            obj.Func2();
            obj?.Func3();
            obj.BFunc1();
            obj.NFunc1();
            obj.ToString();
            obj.GetHashCode();
            obj.GetType();            
        }
    }
}

/*
 C#性能优化

    函数调用：
    call与callvirt
    call：可调用静态方法（必须指定方法定义的类型），实例方法和虚方法（必须指定引用对象的变量，假定变量不为空）
    call：可调用实例方法和虚方法（会调查发出调用的对象的类型即验证变量是否为null，然后以多态方式调用）


     */
