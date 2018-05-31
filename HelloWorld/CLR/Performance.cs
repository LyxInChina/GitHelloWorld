using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.CLR.Performance
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
    public sealed class ClassA: BaseClass
    {
        public ClassA()
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
            var obj = new ClassA();
            ClassA.Func1();//call
            obj.Func2();
            obj?.Func3();
            obj.BFunc1();
            obj.NFunc1();
            obj.ToString();
            obj.GetHashCode();
            obj.GetType();            
        }
    }

    public struct MyStruct
    {

    }
}

/*
 C#性能优化

    函数调用：
    call与callvirt
    call：可调用静态方法（必须指定方法定义的类型），实例方法和虚方法（必须指定引用对象的变量，假定变量不为空）
    callvirt：可调用实例方法和虚方法（会调查发出调用的对象的类型即验证变量是否为null，然后以多态方式调用）
    共同点：接收一个隐藏的this实参作为第一个参数，this实参引用要操作的对象；

    调用call性能比callvirt高，call不会进行调用变量的非空判断,JIT编译器不能内嵌（inline）虚方法；
    如何尽量让方法编译为IL后，JIT调用时，使用call而不是callvirt：
    1.尽量使用静态类中的静态方法 - 静态方法IL代码为call；
    2.尽量使用非虚方法，某些编译器会使用call调用非虚方法（C#编译器会使用callvirt调用所有的实例方法）；
    3.调用值类型中的方法，一般使用call；
    4.尽量使用sealed 密封类，JIT使用非虚方式（call）调用该类中的虚方法（C#编译器生成callvirt指令，JIT会优化这个调用）；

     */
