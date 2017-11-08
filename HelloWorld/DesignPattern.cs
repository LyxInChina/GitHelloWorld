using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{

    #region Create Pattern
    
    public class DesignPattern
    {

    }
    
    public class Singleton
    {
        public class SingleClass
        {
            //key-1 类内部 has-a 该类静态未初始化实例 已创建使用引用
            private static SingleClass _instance = null;
            //key-2 私有构造函数 限制使用new创建路径 
            private SingleClass()
            {

            }
            
            public static SingleClass GetInstance()
            {
                if (_instance == null)
                {
                    _instance = new SingleClass();
                }
                return _instance;
            }

            private static object _lock = new object();

            public static SingleClass GetInstanceAsync()
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SingleClass();
                        }
                    }
                }
                return _instance;
            }

            private static string _name = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            private static System.Threading.Mutex _mutex;
            public static SingleClass GetInstanceProcessAsync()
            {
                if (_name.Length > 260)
                {
                    _name = _name.Substring(_name.Length - 261);
                }
                try
                {
                    bool res = true;
                    System.Security.AccessControl.MutexSecurity ms = new System.Security.AccessControl.MutexSecurity(_name, System.Security.AccessControl.AccessControlSections.Access);
                    _mutex = new System.Threading.Mutex(true, _name, out res);
                    if (res)
                    {
                        if (_instance == null)
                        {
                            lock (_lock)
                            {
                                if (_instance == null)
                                {
                                    _instance = new SingleClass();
                                }
                            }
                        }
                    }
                    else
                    {
                        if(System.Threading.Mutex.TryOpenExisting(_name, out _mutex))
                        {
                            //
                        }
                        else
                        {
                            //
                        }
                    }
                }
                catch (Exception e)
                {
                }
                return _instance;
            }
        }
    }

    public class SimpleFactory
    {
        public abstract class Product
        {
            public abstract void Func();
        }
        
        public class Product1:Product
        {
            public override void Func()
            {
                Console.WriteLine("Product1");
            }
        }

        public class Product2:Product
        {
            public override void Func()
            {
                Console.WriteLine("Product2");
            }
        }

        public class Factory
        {
            public Product ProduceProduct(string str)
            {
                switch (str)
                {
                    case "product1":
                        {
                            return new Product1();
                        }

                    case "product2":
                        {
                            return new Product2();
                        }
                    default:
                        throw new Exception();
                }
            }
        }

        public static void Main()
        {
            var factory = new Factory();
            var p1 = factory.ProduceProduct("product1");
            p1.Func();
            var p2 = factory.ProduceProduct("product2");
            p2.Func();
            Console.ReadLine();
        }

    }

    public class AbstructFactory
    {
        public abstract class Product
        {
            public abstract void Func();
        }

        public class ProductA:Product
        {
            public override void Func()
            {
                Console.WriteLine("ProductA");
            }
        }

        public class ProductB : Product
        {
            public override void Func()
            {
                Console.WriteLine("ProductB");
            }
        }

        public abstract class Factory
        {
            public abstract Product PorduceProduct();
        }

        public class ProductAFactory:Factory
        {
            public override Product PorduceProduct()
            {
                return new ProductA();
            }
        }

        public class ProductBFactory : Factory
        {
            public override Product PorduceProduct()
            {
                return new ProductB();
            }
        }

        public static void Main()
        {
            var productAFactory = new ProductAFactory();
            var productA = productAFactory.PorduceProduct();
            productA.Func();

            var productBFactory = new ProductBFactory();
            var productB = productBFactory.PorduceProduct();
            productB.Func();

            Console.ReadLine();
        }
    }

    public class BuilderParttern
    {
        public class Part
        {
            private string _name;
            public Part(string name)
            {
                _name = name;
            }
            public override string ToString()
            {
                return _name;
            }
        }

        public class Product
        {
            private IList<Part> _prats = new List<Part>();
            public void Add(Part p)
            {
                _prats.Add(p);
            }
            public void BuidUp()
            {
                Console.WriteLine("Begain BuidUp Product...");
                foreach (var item in _prats)
                {
                    Console.WriteLine("install part:" + item.ToString());
                }
                Console.WriteLine("BuidUp Complete");
            }
        }

        public abstract class Builder
        {
            public abstract void BuildPart1();
            public abstract void BuildPart2();
            public abstract Product GetProduct();
        }

        public class Product1Bulder: Builder
        {
            private Product p = new Product();
            public override void BuildPart1()
            {
                p.Add(new Part("Product1_part1"));
            }

            public override void BuildPart2()
            {
                p.Add(new Part("Product1_part2"));
            }

            public override Product GetProduct()
            {
                return p;
            }
        }

        public class Product2Bulder: Builder
        {
            private Product p = new Product();
            public override void BuildPart1()
            {
                p.Add(new Part("Product2_part1"));
            }

            public override void BuildPart2()
            {
                p.Add(new Part("Product2_part2"));
            }

            public override Product GetProduct()
            {
                return p;
            }
        }

        public class Director
        {
            public void Construct(Builder builder)
            {
                builder.BuildPart1();
                builder.BuildPart2();
            }
                 
        }

        public static void Main()
        {
            var director = new Director();

            var product1Builder = new Product1Bulder();
            var product2Builder = new Product2Bulder();

            director.Construct(product1Builder);
            var product1 = product1Builder.GetProduct();
            product1.BuidUp();

            director.Construct(product2Builder);
            var product2 = product2Builder.GetProduct();
            product2.BuidUp();

            Console.ReadLine();
        }

    }

    public class PrototypePattern
    {
        public abstract class Prototype:ICloneable
        {
            public virtual void Func()
            {
                Console.WriteLine("Prototype Class");
            }

            public object Clone()
            {
                throw new NotImplementedException();
            }

            public abstract Prototype CloneEx();
        }

        public class ConcretePrototype1 : Prototype
        {
            public override void Func()
            {
                base.Func();
                Console.WriteLine(this.GetType().ToString());
            }
            public override Prototype CloneEx()
            {

                if (this.GetType().IsSerializable)
                {
                    //序列化与反序列化方法 进行对象的深复制   
                    if (Object.ReferenceEquals(this, null))
                    {
                        return null;
                    }
                    //二进制序列化
                    System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    System.IO.Stream stream = new System.IO.MemoryStream();
                    using (stream)
                    {
                        formatter.Serialize(stream, this);
                        stream.Seek(0, System.IO.SeekOrigin.Begin);
                        return (Prototype)formatter.Deserialize(stream);
                    }
                    //XML序列化
                    System.Xml.Serialization.XmlSerializer formatter1 = new System.Xml.Serialization.XmlSerializer(this.GetType());
                    using (var mems = new System.IO.MemoryStream())
                    {
                        formatter1.Serialize(mems, this);
                        mems.Seek(0, System.IO.SeekOrigin.Begin);
                        return (Prototype)formatter1.Deserialize(mems);
                    }
                }
                else
                {
                    //使用反射实现
                    var t = this.GetType();
                    var obj = Activator.CreateInstance(t);
                    var properties = t.GetProperties();
                    for (int i = 0; i < properties.Length; i++)
                    {
                        var p = properties[i];
                        p.SetValue(obj, p.GetValue(obj));//对于引用类型还需要进行再次的递归深复制
                    }
                    return (Prototype)obj;
                }
            }
        }

        public class ConcretePrototype2 : Prototype
        {
            public override void Func()
            {
                base.Func();
                Console.WriteLine(this.GetType().ToString());
            }
            public override Prototype CloneEx()
            {

                if (this.GetType().IsSerializable)
                {
                    //序列化与反序列化方法 进行对象的深复制   
                    if (Object.ReferenceEquals(this, null))
                    {
                        return null;
                    }
                    //二进制序列化
                    System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    System.IO.Stream stream = new System.IO.MemoryStream();
                    using (stream)
                    {
                        formatter.Serialize(stream, this);
                        stream.Seek(0, System.IO.SeekOrigin.Begin);
                        return (Prototype)formatter.Deserialize(stream);
                    }
                    //XML序列化
                    System.Xml.Serialization.XmlSerializer formatter1 = new System.Xml.Serialization.XmlSerializer(this.GetType());
                    using (var mems = new System.IO.MemoryStream())
                    {
                        formatter1.Serialize(mems, this);
                        mems.Seek(0, System.IO.SeekOrigin.Begin);
                        return (Prototype)formatter1.Deserialize(mems);
                    }
                }
                else
                {
                    //使用反射实现
                    var t = this.GetType();
                    var obj = Activator.CreateInstance(t);
                    var properties = t.GetProperties();
                    for (int i = 0; i < properties.Length; i++)
                    {
                        var p = properties[i];
                        p.SetValue(obj, p.GetValue(obj));//对于引用类型还需要进行再次的递归深复制
                    }
                    return (Prototype)obj;
                }
            }
        }

        public static void Main()
        {
            var concreteProto = new ConcretePrototype1();
            var proto1 = concreteProto.CloneEx();
            var proto2 = concreteProto.CloneEx();
            proto1.Func();
            proto2.Func();

            var concreteProto2 = new ConcretePrototype2();
            var proto3 = concreteProto2.CloneEx();
            var proto4 = concreteProto2.CloneEx();
            proto3.Func();
            proto4.Func();

            Console.ReadLine();
        }
    }

    #endregion

    #region Struct Pattern

    public class AdapterPattern
    {
        public class ObjectAdapter
        {
            public class Target
            {
                public virtual void Func()
                {
                    Console.WriteLine(this.GetType() + "::Func()");
                }
            }

            public class Adaptee
            {
                public void SpecificFunc()
                {
                    Console.WriteLine("Adaptee SpecificFunc()");
                }
            }

            public class Adapter:Target
            {
                private Adaptee adaptee = new Adaptee();
                //完成转换 从Adaptee到Target
                public override void Func()
                {
                    //base.Func();
                    adaptee.SpecificFunc();
                    Console.WriteLine("Adapter::Func()");
                }
            }

            public static void Main()
            {
                var target = new Adapter();
                target.Func();

                Console.WriteLine();
            }

        }

        public class ClassAdapter
        {

            public interface ITarget
            {
                void Func();
            }

            public class Adaptee
            {
                public void SpecificFunc()
                {
                    Console.WriteLine("Adaptee SpecificFunc()");
                }
            }

            public class Adapter:Adaptee,ITarget
            {
                public void Func()
                {
                    this.SpecificFunc();
                    Console.WriteLine("Adapter Func()");
                }
            }

            public static void Main()
            {
                var target = new Adapter();
                target.Func();

                Console.ReadLine();
            }

        }

    }

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

        public static void Main()
        {
            Implementor imp1 = new Implementor1();
            Abstraction abs1 = new RedefinedAbstraction1(imp1);
            abs1.Func();

            Implementor imp2 = new Implementor2();
            Abstraction abs2 = new RedefinedAbstraction2(imp2);
            abs2.Func();

            Console.WriteLine();
        }


    }

    /// <summary>
    /// Wrapper
    /// </summary>
    public class DecoratorPattern
    {
        public abstract class Component
        {
            public abstract void Func();
        }

        public class Component1:Component
        {
            public override void Func()
            {
                Console.WriteLine(this.GetType().ToString() + "::Func()");
            }
        }

        public abstract class Decoration:Component
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

        public class Decoration1:Decoration
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

        public static void Main()
        {
            Component com = new Component1();
            Decoration deco1 = new Decoration1(com);
            deco1.Process();
            
            Decoration decor2 = new Decoration2(com);
            decor2.Process();

            Console.WriteLine();
        }

    }

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

        public static void Main()
        {
            var root = new Composite();
            var leaf1 = new Leaf();
            var leaf2 = new Leaf();
            var comp = new Composite();
            root.Add(leaf1);
            root.Add(leaf2);
            root.Add(comp);
            comp.Add(leaf1);
            root.Func();
            leaf1.Func();
            comp.Func();

            Console.ReadLine();
        }
    }

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

        public static void Main()
        {
            var facade = new Facade();
            facade.Func();

            Console.ReadLine();
        }
    }

    public class FlyweightPattern
    {
        //大量重复对象的重用 共享对象必须是细颗粒度

    }

    #endregion
}
