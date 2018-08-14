using System;
using System.Collections.Generic;
using static HelloWorld.DesignPattern.BuilderParttern;
using static HelloWorld.DesignPattern.PrototypePattern;
using static HelloWorld.DesignPattern.BridgePattern;
using static HelloWorld.DesignPattern.DecoratorPattern;
using static HelloWorld.DesignPattern.CompositePattern;
using static HelloWorld.DesignPattern.FacadePattern;
using static HelloWorld.DesignPattern.FlyweightPattern;
using static HelloWorld.DesignPattern.ProxyPattern;
using static HelloWorld.DesignPattern.ChainOfResponsibilityPattern;
using static HelloWorld.DesignPattern.CommandPattern;
using static HelloWorld.DesignPattern.InterpreterPattern;
using static HelloWorld.DesignPattern.VisitorPattern;
using static HelloWorld.DesignPattern.IteratorPattern;
using static HelloWorld.DesignPattern.MediatorPattern;
using static HelloWorld.DesignPattern.MementoPattern;
using static HelloWorld.DesignPattern.ObserverPattern;
using static HelloWorld.DesignPattern.StrategyPattern;
using static HelloWorld.DesignPattern.TemplateMethodPattern;
using static HelloWorld.DesignPattern.SimpleFactory;
using static HelloWorld.DesignPattern.AbstructFactory;
using SFactory = HelloWorld.DesignPattern.SimpleFactory.Factory;
using static HelloWorld.DesignPattern.AdapterPattern;
using TAbstraction = HelloWorld.DesignPattern.TemplateMethodPattern.Abstraction;
using BAbstraction = HelloWorld.DesignPattern.BridgePattern.Abstraction;
using SContext = HelloWorld.DesignPattern.StrategyPattern.Context;
using HelloWorld.DesignPattern;

namespace HelloWorld
{

    public class PatternTest
    {
        public static void Main_Factory()
        {
            var factory = new SFactory();
            var p1 = factory.ProduceProduct("product1");
            p1.Func();
            var p2 = factory.ProduceProduct("product2");
            p2.Func();
            Console.ReadLine();
        }
        public static void Main_AbsFactory()
        {
            var productAFactory = new ProductAFactory();
            var productA = productAFactory.PorduceProduct();
            productA.Func();

            var productBFactory = new ProductBFactory();
            var productB = productBFactory.PorduceProduct();
            productB.Func();

            Console.ReadLine();
        }
        public static void Main_Builder()
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
        public static void Main_Prototype()
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
        public static void Main_ObjectAdapter()
        {
            var target = new Object_Adapter();
            target.Func();

            var tar = new Class_Adapter();
            tar.Func();

            Console.WriteLine();
        }
        public static void Main_Bridge()
        {
            Implementor imp1 = new Implementor1();
            BAbstraction abs1 = new RedefinedAbstraction1(imp1);
            abs1.Func();

            Implementor imp2 = new Implementor2();
            BAbstraction abs2 = new RedefinedAbstraction2(imp2);
            abs2.Func();

            Console.WriteLine();
        }
        public static void Main_Decoration()
        {
            Component com = new Component1();
            Decoration deco1 = new Decoration1(com);
            deco1.Process();

            Decoration decor2 = new Decoration2(com);
            decor2.Process();

            Console.WriteLine();
        }
        public static void Main_Composite()
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

        public static void Main_Fade()
        {
            var facade = new Facade();
            facade.Func();

            Console.ReadLine();
        }
        /// <summary>
        /// 享元模式
        /// </summary>
        public static void Main_Flyweight()
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
        /// <summary>
        /// 代理模式
        /// </summary>
        public static void Main_Proxy()
        {
            var proxy = new SubjectProxy();
            proxy.Func();

            Console.ReadLine();
        }
        /// <summary>
        /// 责任链模式
        /// </summary>
        public static void Main_ChainOfResponsibility()
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
        /// <summary>
        /// 命令模式
        /// </summary>
        public static void Main_Command()
        {
            //指定命令执行者
            var invoker1 = new ConcreteInvoker1();
            //指定命令接受者
            var receiver1 = new Receiver01(invoker1);
            //创建命令
            var cmd1 = new Select("cmd1");
            var cmd2 = new Update("cmd2");
            //发给接受者执行
            receiver1.ReceiveCmd(cmd1);
            receiver1.ReceiveCmd(cmd2);

            Console.ReadLine();
        }
        public static void Main_Interpreter()
        {
            var context = new InterpreterPattern.Context() { A = 98, B = 13, Input = '%' };
            var exp1 = new NoneterminalExpression();
            var exp2 = new NoneterminalExpression();
            var exp3 = new TerminalExpression();
            exp1.Interpret(context);
            exp2.Interpret(context);
            exp3.Interpret(context);

            Console.ReadLine();
        }
        public static void Main_Iterator()
        {
            var agg1 = new ConcreteAggregate1();
            var agg2 = new ConcreteAggregate2();
            var it1 = agg1.CreateIterator();
            var it2 = agg2.CreateIterator();
            //TOOD:统一方法遍历it1、it2

            Console.ReadLine();
        }
        public static void Main_Mediator()
        {
            var mediator = new ConcreteMediator1();
            var coll1 = new ConcreteColleage1
            {
                Med = mediator
            };
            var coll2 = new ConcreteColleage2
            {
                Med = mediator
            };

            coll1.SendMsg(new Message() { Msg = "coll01" });
            coll2.SendMsg(new Message() { Msg = "coll02" });

            Console.ReadLine();
        }
        public static void Main_Memento()
        {
            var originator = new Originator();
            var taker = new CareTaker();

            taker.SetMemento(originator.SaveMemento());

            originator.RestoreMemento(taker.GetMemento());

            Console.ReadLine();
        }
        public static void Main_Observer()
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

        public static void Main_State()
        {

        }
        public static void Main_Strategy()
        {
            var context = new SContext();
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
        public static void Main_TemplateMethodPattern()
        {
            TAbstraction cc1 = new ConcreteClass1();
            TAbstraction cc2 = new ConcreteClass2();
            cc1.TemplateMethod();
            cc2.TemplateMethod();

            Console.ReadLine();
        }
        public static void Main_Visitor()
        {
            var os = new ObjectStruct();
            os.AddElement(new ConcreteElement1());
            os.AddElement(new ConcreteElement1());

            var vis1 = new ConcreteVisotor1();
            os.Accept(vis1);

            Console.ReadLine();
        }
    }

}
