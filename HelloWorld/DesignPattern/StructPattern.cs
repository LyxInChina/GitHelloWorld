using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.DesignPattern
{
    #region Struct Pattern

    /// <summary>
    /// 适配器模式
    /// 对象适配器：继承父类+组合代理器
    /// 类型适配器：继承父类+实现接口
    /// </summary>
    public class AdapterPattern
    {
        public class Adaptee
        {
            /// <summary>
            /// 要代理的方法
            /// </summary>
            public void SpecificFunc()
            {
                Console.WriteLine("Adaptee SpecificFunc()");
            }
        }

        public class Target_Object
        {
            public virtual void Func()
            {

            }
        }

        public interface ITarget_Class
        {
            void Func();
        }

        public class Object_Adapter : Target_Object
        {
            private Adaptee adaptee = new Adaptee();
            //完成转换从Adapter-》adaptee
            public override void Func()
            {
                //base.Func();
                adaptee.SpecificFunc();
                Console.WriteLine("Adapter::Func()");
            }
        }

        public class Class_Adapter : Adaptee, ITarget_Class
        {
            public void Func()
            {
                base.SpecificFunc();
                Console.WriteLine("Adapter::Func()");
            }
        }
    }

    /// <summary>
    /// 桥接模式
    /// </summary>
    public class BridgePattern
    {
        /// <summary>
        /// 定义实现
        /// </summary>
        public abstract class Implementor
        {
            public abstract void Process();
        }

        /// <summary>
        /// 定义操作
        /// </summary>
        public abstract class Abstraction
        {
            protected Implementor _implementor;
            protected Abstraction(Implementor implementor)
            {
                _implementor = implementor;
            }
            public abstract void Func();
        }

        public class RedefinedAbstraction1 : Abstraction
        {
            public RedefinedAbstraction1(Implementor implementor) : base(implementor)
            {

            }

            public override void Func()
            {
                this._implementor.Process();
                Console.WriteLine(this.GetType().ToString() + "::Func()");
            }
        }

        public class RedefinedAbstraction2 : Abstraction
        {
            public RedefinedAbstraction2(Implementor implementor) : base(implementor)
            {

            }

            public override void Func()
            {
                this._implementor.Process();
                Console.WriteLine(this.GetType().ToString() + "::Func()");
            }
        }

        public class Implementor1 : Implementor
        {
            public override void Process()
            {
                Console.WriteLine(this.GetType().ToString() + "::Process()");
            }
        }

        public class Implementor2 : Implementor
        {
            public override void Process()
            {
                Console.WriteLine(this.GetType().ToString() + "::Process()");
            }
        }




    }

    /// <summary>
    /// 装饰者模式
    /// </summary>
    public class DecoratorPattern
    {
        public abstract class Component
        {
            public abstract void Func();
        }

        public class Component1 : Component
        {
            public override void Func()
            {
                Console.WriteLine(this.GetType().ToString() + "::Func()");
            }
        }

        public abstract class Decoration : Component
        {
            protected Component _component;
            protected Decoration(Component component)
            {
                _component = component;
            }
            public override void Func()
            {

            }
            public abstract void Process();
        }

        public class Decoration1 : Decoration
        {
            public Decoration1(Component component) : base(component)
            {

            }

            public override void Process()
            {
                Console.WriteLine(this.GetType().ToString() + "::Process()");
            }
        }

        public class Decoration2 : Decoration
        {
            public Decoration2(Component component) : base(component)
            {

            }

            public override void Process()
            {
                Console.WriteLine(this.GetType().ToString() + "::Process()");
            }
        }
    }

    /// <summary>
    /// 组合模式
    /// </summary>
    public class CompositePattern
    {
        public interface IComponent
        {
            void Add(IComponent com);
            void Remove(IComponent com);
            IComponent GetChild(int index);
            void Func();
        }

        public class Leaf : IComponent
        {
            public void Add(IComponent com)
            {
                throw new NotImplementedException();
            }
            public IComponent GetChild(int index)
            {
                throw new NotImplementedException();
            }

            public void Remove(IComponent com)
            {
                throw new NotImplementedException();
            }
            public void Func()
            {

            }
        }

        public class Composite : IComponent
        {
            protected IList<IComponent> _component = new List<IComponent>();
            public void Add(IComponent component)
            {
                _component.Add(component);
            }

            public void Func()
            {
                throw new NotImplementedException();
            }

            public IComponent GetChild(int index)
            {
                if (index <= _component.Count)
                    return _component[index];
                else
                    return null;
            }

            public void Remove(IComponent component)
            {
                _component.Remove(component);
            }
        }
    }

    /// <summary>
    /// 外观模式
    /// </summary>
    public class FacadePattern
    {
        //解耦客户端程序与子系统
        public class SubSystem1
        {
            public void Func1()
            {
                Console.WriteLine("SubSystem1::Func1()");
            }
        }

        public class SubSystem2
        {
            public void Func2()
            {
                Console.WriteLine("SunSystem2::Func2()");
            }
        }

        public class Facade
        {
            private SubSystem1 sub1 = new SubSystem1();
            private SubSystem2 sub2 = new SubSystem2();

            public void Func()
            {
                sub1.Func1();
                sub2.Func2();
                Console.WriteLine("Facade::Func()");
            }
        }


    }

    /// <summary>
    /// 享元模式
    /// </summary>
    public class FlyweightPattern
    {
        //使用共享支持大量的细颗粒度对象
        public abstract class Flyweight
        {
            protected string _guid;
            protected Flyweight()
            {
                _guid = Guid.NewGuid().ToString();
            }
            public abstract void Func();
        }

        public class ConcreteFlyweight0 : Flyweight
        {
            public ConcreteFlyweight0() : base()
            {

            }

            public override void Func()
            {
                Console.WriteLine(_guid);
            }
        }

        public class FlyweightFactory
        {
            private IList<Tuple<string, Flyweight>> _flyWeights = new List<Tuple<string, Flyweight>>();

            public Flyweight this[string key]
            {
                get
                {
                    Flyweight res = null;
                    var temp = _flyWeights.ToList().Find(r => r.Item1 == key);
                    if (temp != null)
                    {
                        res = temp.Item2;
                    }
                    else
                    {
                        Flyweight temp1 = new ConcreteFlyweight0();
                        _flyWeights.Add(Tuple.Create(key, temp1));
                        //Build T
                        //Add to list
                    }
                    return res;
                }
            }

        }



    }

    /// <summary>
    /// 代理模式
    /// </summary>
    public class ProxyPattern
    {
        public abstract class Subject
        {
            public abstract void Func();
        }

        public class RealSubject : Subject
        {
            public override void Func()
            {
                Console.WriteLine("RealSubject::Func()");
            }
        }

        public class SubjectProxy : Subject
        {
            private Subject _subject = new RealSubject();

            public SubjectProxy()
            {

            }
            public override void Func()
            {
                _subject.Func();
            }
        }



    }

    #endregion
}
