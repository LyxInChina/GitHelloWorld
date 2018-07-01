using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.DesignPattern
{
    #region Action Pattern

    /// <summary>
    /// 责任链模式
    /// </summary>
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



    }

    /// <summary>
    /// 命令模式
    /// </summary>
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

        public class ConcreteCmd1 : Command
        {
            public ConcreteCmd1(string str) : base(str)
            {

            }
            public override void Process()
            {
                Invoke?.Execute(this);
            }
        }
        public class ConcreteCmd2 : Command
        {
            public ConcreteCmd2(string str) : base(str)
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

        public class ConcreteInvoker1 : Invoker
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


    }

    /// <summary>
    /// 解释器模式
    /// </summary>
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
                int result = 0;
                if (t.Value != null)
                {
                    result = t.Value.Invoke(context.A, context.B);
                }
                Console.WriteLine("Terminal Expression:" + result);
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
                Console.WriteLine("None Terminal Expression:" + result);
            }
        }

        public class Context
        {
            public Dictionary<char, Func<int, int, int>> Operators = new Dictionary<char, Func<int, int, int>>
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



    }

    /// <summary>
    /// 迭代器模式
    /// </summary>
    public class IteratorPattern
    {
        public interface Iterator
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
                if (_list.Count >= _index + 1)
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

        public class ConcreteAggregate1 : Aggregate
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
    }

    /// <summary>
    /// 中介者模式
    /// </summary>
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
                Console.WriteLine(this.GetType().ToString() + "::" + msg);
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


    }

    /// <summary>
    /// 
    /// </summary>
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



    }

    /// <summary>
    /// 观察者模式
    /// </summary>
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



    }

    /// <summary>
    /// 状态模式
    /// </summary>
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

        public class ConcreteState2 : State
        {
            public override void Handle()
            {
                throw new NotImplementedException();
            }
        }


    }

    /// <summary>
    /// 策略模式
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
                    for (j = i; j > 0; j--)//遍历i之前元素
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
                    for (j = i + 1; j < _array.Count; j++)
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
                //Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                //Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            protected override void Func2()
            {
                //Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            protected override void Func3()
            {
                //Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        public class ConcreteClass2 : Abstraction
        {
            protected override void Func1()
            {
                //Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }

            protected override void Func2()
            {
                //Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }

            protected override void Func3()
            {
                //Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
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
                //Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                //Console.WriteLine(this.GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);
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



    }

    #endregion
}
