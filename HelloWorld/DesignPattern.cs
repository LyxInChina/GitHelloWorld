using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    #region Create Pattern
    
    /// <summary>
    /// 单例模式
    /// </summary>
    public class SingletonPattern
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
                        _mutex.ReleaseMutex();
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

    /// <summary>
    /// 原型模式
    /// </summary>
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
                    //System.Xml.Serialization.XmlSerializer formatter1 = new System.Xml.Serialization.XmlSerializer(this.GetType());
                    //using (var mems = new System.IO.MemoryStream())
                    //{
                    //    formatter1.Serialize(mems, this);
                    //    mems.Seek(0, System.IO.SeekOrigin.Begin);
                    //    return (Prototype)formatter1.Deserialize(mems);
                    //}
                }
                else
                {
                    //使用反射实现
                    //效率不高
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
                return null;
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

        public class ConcreteFlyweight0:Flyweight
        {
            public ConcreteFlyweight0():base()
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
                    }else
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

        public static void Main()
        {
            var list = new List<Flyweight>();
            var flyWeightFactory = new FlyweightFactory();

            list.Add(flyWeightFactory["0"]);
            list.Add(flyWeightFactory["0"]);
            list.Add(flyWeightFactory["0"]);
            list.Add(flyWeightFactory["1"]);
            list.Add(flyWeightFactory["1"]);

            list.ForEach(s => s.Func());

            Console.ReadLine();
        }

    }

    public class ProxyPattern
    {
        public abstract class Subject
        {
            public abstract void Func();
        }

        public class RealSubject:Subject
        {
            public override void Func()
            {
                Console.WriteLine("RealSubject::Func()");
            }
        }

        public class SubjectProxy:Subject
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

        public static void Main()
        {
            var proxy = new SubjectProxy();
            proxy.Func();

            Console.ReadLine();
        }

    }

    #endregion

    #region Action Pattern

    public class ChainOfResponsibilityPattern
    {

        public abstract class Handler
        {
            public Handler NextHandler { get; set; }
            public abstract void Process(Request request);

            protected void CallBack(Request request)
            {
                if (NextHandler != null)
                {
                    NextHandler.Process(request);
                }
            }
        }

        public class Request
        {
            public int Level { get; set; }
        }

        public class ConcreteHandler1 : Handler
        {
            public override void Process(Request request)
            {
                if (request.Level <= 1)
                {
                    Console.WriteLine("Porcess Request::ConcreteHandler1");
                }
                else
                {
                    CallBack(request);
                }                
            }
        }

        public class ConcreteHandler2 : Handler
        {
            public override void Process(Request request)
            {
                if (request.Level == 2)
                {
                    Console.WriteLine("Porcess Request::ConcreteHandler2");
                }
                else
                {
                    CallBack(request);
                }
            }
        }
        public class ConcreteHandler3 : Handler
        {
            public override void Process(Request request)
            {
                if (request.Level == 3)
                {
                    Console.WriteLine("Porcess Request::ConcreteHandler3");
                }
                else
                {
                    CallBack(request);
                }
            }
        }

        public static void Main()
        {
            var c1 = new ConcreteHandler1();
            var c2 = new ConcreteHandler2();
            var c3 = new ConcreteHandler3();
            //设置责任链
            c1.NextHandler = c2;
            c2.NextHandler = c3;
            c1.Process(new Request() { Level = 0 });
            c1.Process(new Request() { Level = 2 });
            c1.Process(new Request() { Level = 3 });

            Console.ReadLine();
        }

    }

    public class CommandPattern
    {
        public abstract class Command
        {
            public string Cmd { get; set; }
            public Invoker Invoke { get; set; }
            protected Command(string str)
            {
                Cmd = str;
            }
            public abstract void Process();
            public override string ToString()
            {
                return Cmd;
            }
        }

        public class ConcreteCmd1:Command
        {
            public ConcreteCmd1(string str):base(str)
            {

            }
            public override void Process()
            {
                Invoke?.Execute(this);
            }
        }
        public class ConcreteCmd2 : Command
        {
            public ConcreteCmd2(string str):base(str)
            {

            }
            public override void Process()
            {
                Invoke?.Execute(this);
            }
        }

        public abstract class Invoker
        {
            public abstract void Execute(Command cmd);
        }

        public class ConcreteInvoker1:Invoker
        {
            public override void Execute(Command cmd)
            {
                Console.WriteLine(this.GetType().ToString() + "::" + cmd.ToString());
            }
        }

        public class ConcreteInvoker2 : Invoker
        {
            public override void Execute(Command cmd)
            {
                Console.WriteLine(this.GetType().ToString() + "::" + cmd.ToString());
            }
        }

        public class Receiver
        {
            private Invoker _invoker;
            public Receiver(Invoker invoker)
            {
                _invoker = invoker;
            }

            public void ReceiveCmd(Command cmd)
            {
                //命令绑定执行者 命令执行
                cmd.Invoke = _invoker;
                cmd.Process();
                //或者 执行者执行命令
                _invoker.Execute(cmd);
            }
        }

        public static void Main()
        {
            //指定命令执行者
            var invoker1 = new ConcreteInvoker1();
            //指定命令接受者
            var receiver1 = new Receiver(invoker1);
            //创建命令
            var cmd1 = new ConcreteCmd1("cmd1");
            var cmd2 = new ConcreteCmd2("cmd2");
            //发给接受者执行
            receiver1.ReceiveCmd(cmd1);
            receiver1.ReceiveCmd(cmd2);

            Console.ReadLine();
        }
    }

    public class InterpreterPattern
    {
        public abstract class Expression
        {
            public abstract void Interpret(Context context);
        }

        public class TerminalExpression : Expression
        {
            public override void Interpret(Context context)
            {
                var t = context.Operators.ToList().Find(s => s.Key == context.Input);
                int result=0;
                if (t.Value != null)
                {
                    result = t.Value.Invoke(context.A, context.B);
                }
                Console.WriteLine("Terminal Expression:"+result);
            }
        }

        public class NoneterminalExpression : Expression
        {
            public override void Interpret(Context context)
            {
                var t = context.Operators.ToList().Find(s => s.Key == context.Input);
                int result = 0;
                if (t.Value != null)
                {
                    result = t.Value.Invoke(context.A, context.B);
                }
                Console.WriteLine("None Terminal Expression:"+result);
            }
        }

        public class Context
        {
            public Dictionary<char, Func<int,int,int>> Operators = new Dictionary<char, Func<int, int, int>>
            {
                {'+',(int a,int b)=> {return a+b; } },
                {'-',(int a,int b)=> {return a-b; } },
                {'*',(int a,int b)=> {return a*b; } },
                {'/',(int a,int b)=> {if(b!=0)return a/b;else return 0; } },
                {'%',(int a,int b)=> {if(b!=0)return a%b;else return 0; } },
                {'|',(int a,int b)=> {return a|b; } },
                {'^',(int a,int b)=> {return a^b; } },
            };

            public int A { get; set; }
            public int B { get; set; }

            public char Input { get; set; }

        }


        public static void Main()
        {
            var context = new Context() {A=98,B=13, Input = '%' };
            var exp1 = new NoneterminalExpression();
            var exp2 = new NoneterminalExpression();
            var exp3 = new TerminalExpression();
            exp1.Interpret(context);
            exp2.Interpret(context);
            exp3.Interpret(context);

            Console.ReadLine();
        }
    }

    public class IteratorPattern
    {
        public interface Iterator: System.Collections.IEnumerator
        {

        }

        public class ConcreteIterator1 : Iterator
        {
            private IList<object> _list;
            private int _index = 0;

            public object Current
            {
                get
                {
                    return _list[_index];
                }
            }

            public bool MoveNext()
            {
                if(_list.Count >= _index+1)
                {
                    ++_index;
                    return true;
                }                
                return false;
            }

            public void Reset()
            {
                _index = 0;
            }
        }

        public class ConcreteIterator2 : Iterator
        {
            private object[] _list;
            private int _index = 0;

            public object Current
            {
                get
                {
                    return _list[_index];
                }
            }

            public bool MoveNext()
            {
                if (_list.Length >= _index + 1)
                {
                    ++_index;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                _index = 0;
            }
        }

        public abstract class Aggregate
        {
            public abstract Iterator CreateIterator();
        }

        public class ConcreteAggregate1: Aggregate
        {
            public override Iterator CreateIterator()
            {
                return new ConcreteIterator1();
            }
        }
        public class ConcreteAggregate2 : Aggregate
        {
            public override Iterator CreateIterator()
            {
                return new ConcreteIterator2();
            }
        }

        public static void Main()
        {
            var agg1 = new ConcreteAggregate1();
            var agg2 = new ConcreteAggregate2();
            var it1 = agg1.CreateIterator();
            var it2 = agg2.CreateIterator();
            //TOOD:统一方法遍历it1、it2

            Console.ReadLine();
        }


    }

    public class MediatorPattern
    {
        public abstract class Mediator
        {
            public IList<Colleage> colls = new List<Colleage>();
            public abstract void Contact(Message msg, Colleage coll);
        }

        public class Message
        {
            public string Msg { get; set; }
            public override string ToString()
            {
                return Msg;
            }
        }

        public class ConcreteMediator1 : Mediator
        {
            public override void Contact(Message msg, Colleage coll)
            {
                //TODO:get target colleage and call receive function
                colls.ToList().ForEach(c => { if (c != coll) { c.ReceiveMsg(msg); } });
            }
        }

        public abstract class Colleage
        {
            private Mediator _med;
            public Mediator Med { get { return _med; } set { value.colls.Add(this); _med = value; } }
            public string ID { get; set; }
            public abstract void ReceiveMsg(Message msg);
            public virtual void SendMsg(Message msg)
            {
                Med.Contact(msg, this);
            }
        }

        public class ConcreteColleage1 : Colleage
        {
            public override void ReceiveMsg(Message msg)
            {
                Console.WriteLine(this.GetType().ToString()+"::"+msg);
            }

            public override void SendMsg(Message msg)
            {
                base.SendMsg(msg);
            }
        }

        public class ConcreteColleage2 : Colleage
        {
            public override void ReceiveMsg(Message msg)
            {
                Console.WriteLine(this.GetType().ToString() + "::" + msg);
            }
            public override void SendMsg(Message msg)
            {
                base.SendMsg(msg);
            }
        }

        public static void Main()
        {
            var mediator = new ConcreteMediator1();
            var coll1 = new ConcreteColleage1();
            coll1.Med = mediator;
            var coll2 = new ConcreteColleage2();
            coll2.Med = mediator;
            
            coll1.SendMsg(new Message() { Msg="coll01"});
            coll2.SendMsg(new Message() { Msg="coll02"});

            Console.ReadLine();
        }

    }

    public class MementoPattern
    {
        public class Memento
        {
            private string _state { get; set; }
            public void SetState(string state)
            {
                _state = state;
            }

            public string GetState()
            {
                return _state;
            }
        }
    
        public class CareTaker
        {
            private Memento _memen { get; set; }

            public Memento GetMemento()
            {
                return _memen;
            }

            public void SetMemento(Memento memento)
            {
                _memen = memento;
            }

        }
        public class Originator
        {
            public Memento SaveMemento()
            {
                return new Memento();
            }
            public void RestoreMemento(Memento memento)
            {

            }
        }

        public static void Main()
        {
            var originator = new Originator();
            var taker = new CareTaker();

            taker.SetMemento(originator.SaveMemento());

            originator.RestoreMemento(taker.GetMemento());

            Console.ReadLine();
        }

    }

    public class ObserverPattern
    {
        public interface IObserver
        {
            void Update(string msg);
        }

        public class ConcreteObserver1 : IObserver
        {
            public void Update(string msg)
            {
                throw new NotImplementedException();
            }
        }

        public class ConcreteObserver2 : IObserver
        {
            public void Update(string msg)
            {
                throw new NotImplementedException();
            }
        }

        public interface ISubject
        {
            IList<IObserver> Observers { get; set; }
            void Add(IObserver ser);
            void Renmove(IObserver ser);
            void Notify(string msg);
        }

        public class ConcreteSubject : ISubject
        {
            public IList<IObserver> Observers { get; set; } = new List<IObserver>();

            public void Add(IObserver ser)
            {
                Observers.Add(ser);
            }

            public void Notify(string msg)
            {
                Observers.ToList().ForEach(s => s.Update(msg));
            }

            public void Renmove(IObserver ser)
            {
                Observers.Remove(ser);
            }
        }


        public static void Main()
        {
            var sub1 = new ConcreteSubject();
            var ob1 = new ConcreteObserver1();
            var ob2 = new ConcreteObserver1();

            //添加观察者
            sub1.Add(ob1);
            sub1.Add(ob2);

            sub1.Notify("New Msg");
            sub1.Renmove(ob2);

            Console.ReadLine();
        }

    }

    public class StatePattern
    {
        public class Context
        {
            private State _state;
            public void SetState(State state)
            {
                //set state change and set action
                state.Handle();
            }
        }

        public abstract class State
        {
            public abstract void Handle();
        }

        public class ConcreteState1 : State
        {
            public override void Handle()
            {
                throw new NotImplementedException();
            }
        }

        public class ConcreteState2:State
        {
            public override void Handle()
            {
                throw new NotImplementedException();
            }
        }

        public static void Main()
        {

        }
    }

    /// <summary>
    /// 策略模式
    /// 
    /// </summary>
    public class StrategyPattern
    {
        public class Context
        {
            private Strategy _strategy;

            public void SetStrategy(Strategy st)
            {
                _strategy = st;
            }

            public void RunAlgorithm()
            {
                _strategy?.Algorithm();
            }
        }

        public abstract class Strategy
        {
            protected IList<IComparable> _array;
            public int CalculateDegree()
            {
                return 0;
            }
            public abstract void Algorithm();
        }

        public class ConcreteStrategy1 : Strategy
        {
            /// <summary>
            /// bubble sort
            /// </summary>
            public override void Algorithm()
            {
                for (int i = 0; i < _array.Count; i++)
                {
                    for (int j = i + 1; j < _array.Count; j++)
                    {
                        if (_array[i].CompareTo(_array[j]) > 0)
                        {
                            var temp = _array[j];
                            _array[j] = _array[i];
                            _array[i] = temp;
                        }
                    }
                }
            }
        }

        public class ConcreteStrategy2 : Strategy
        {
            /// <summary>
            /// quick sort
            /// </summary>
            public override void Algorithm()
            {
                Sort_q(_array, 0, _array.Count - 1);
            }

            private void Sort_q(IList<IComparable> rest, int left, int right)
            {
                if (left < right)
                {
                    //找到中间元素为中间值
                    var middle = rest[(left + right) / 2];
                    int i = left - 1,
                        j = right + 1;
                    while (true)
                    {
                        //找到比middle小的元素
                        while (rest[++i].CompareTo(middle) < 0 && i < right) ;
                        //找到比middle大的元素
                        while (rest[--j].CompareTo(middle) > 0 && j > 0) ;
                        //若越界则退出循环
                        if (i >= j)
                            break;
                        //交互元素
                        var num = rest[i];
                        rest[j] = rest[j];
                        rest[j] = num;
                    }
                    //迭代排序
                    Sort_q(rest, left, i - 1);
                    Sort_q(rest, j + 1, right);
                }
            }
        }

        public class ConcreteStrategy3 : Strategy
        {
            /// <summary>
            /// insert sort
            /// </summary>
            public override void Algorithm()
            {
                for (int i = 1; i < _array.Count; i++)
                {
                    int j;
                    var temp = _array[i];//index i
                    for (j = i;  j > 0; j--)//遍历i之前元素
                    {
                        if (_array[j - 1].CompareTo(temp) > 0)//比较当前元素与之前元素
                        {
                            _array[j] = _array[j - 1];
                        }
                        else
                        {
                            break;
                        }
                    }
                    _array[j] = temp;
                }
            }
        }

        public class ConcreteStrategy4 : Strategy
        {
            /// <summary>
            /// select sort
            /// </summary>
            public override void Algorithm()
            {
                IComparable temp;
                for (int i = 0; i < _array.Count; i++)
                {
                    temp = _array[i];//index i
                    int j;
                    int select = i;
                    for (j = i+1; j < _array.Count; j++)
                    {
                        if (_array[j].CompareTo(temp) < 0)
                        {
                            temp = _array[j];
                            select = j;
                        }
                    }
                    _array[select] = _array[i];
                    _array[i] = temp;
                }
            }
        }
        
        public static void Main()
        {
            var context = new Context();
            //定义算法
            var cs1 = new ConcreteStrategy1();
            var cs2 = new ConcreteStrategy2();
            //
            context.SetStrategy(cs1);
            context.RunAlgorithm();
            context.SetStrategy(cs2);
            context.RunAlgorithm();

            Console.ReadLine();
        }

    }

    /// <summary>
    /// 模板模式
    /// 定义一个模板类，预留使用的每个抽象实现
    /// 子类实现每个抽象实现
    /// </summary>
    public class TemplateMethodPattern
    {
        public abstract class Abstraction
        {
            public void TemplateMethod()
            {
                Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);
                Func1();
                Func2();
                Func3();
            }
            protected abstract void Func1();
            protected abstract void Func2();
            protected abstract void Func3();
        }

        public class ConcreteClass1 : Abstraction
        {
            protected override void Func1()
            {
                Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            protected override void Func2()
            {
                Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            protected override void Func3()
            {
                Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        public class ConcreteClass2 : Abstraction
        {
            protected override void Func1()
            {
                Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }

            protected override void Func2()
            {
                Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }

            protected override void Func3()
            {
                Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
        }

        public static void Main()
        {
            Abstraction cc1 = new ConcreteClass1();
            Abstraction cc2 = new ConcreteClass2();
            cc1.TemplateMethod();
            cc2.TemplateMethod();

            Console.ReadLine();
        }

    }

    /// <summary>
    /// 访问者模式
    /// </summary>
    public class VisitorPattern
    {
        /// <summary>
        /// 抽象访问者
        /// </summary>
        public abstract class Visitor
        {
            public abstract void Visit(Element element);
        }

        public class ConcreteVisotor1 : Visitor
        {
            public override void Visit(Element element)
            {
                element.Func();
            }
        }
        public class ConcreteVisotor2 : Visitor
        {
            public override void Visit(Element element)
            {
                element.Func();
            }
        }

        /// <summary>
        /// 抽象元素
        /// </summary>
        public abstract class Element
        {
            public abstract void Accept(Visitor vistor);
            public abstract void Func();
        }

        public class ConcreteElement1 : Element
        {
            public override void Accept(Visitor vistor)
            {
                vistor.Visit(this);
            }

            public override void Func()
            {
                Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        public class ConcreteElement2 : Element
        {
            public override void Accept(Visitor vistor)
            {
                vistor.Visit(this);
            }
            public override void Func()
            {
                Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// 具体元素集合
        /// </summary>
        public class ObjectStruct
        {
            public IList<Element> Elements = new List<Element>();
            public void AddElement(Element ele)
            {
                Elements.Add(ele);
            }

            public void RemoveElement(Element ele)
            {
                Elements.Remove(ele);
            }

            public void Accept(Visitor vistor)
            {
                Elements.ToList().ForEach(e => e.Accept(vistor));
            }
        }

        public static void Main()
        {
            var os = new ObjectStruct();
            os.AddElement(new ConcreteElement1());
            os.AddElement(new ConcreteElement2());
            os.AddElement(new ConcreteElement1());
            os.AddElement(new ConcreteElement2());

            var vis1 = new ConcreteVisotor1();
            var vis2 = new ConcreteVisotor2();
            os.Accept(vis1);
            os.Accept(vis2);

            Console.ReadLine();
        }

    }

    #endregion
}
