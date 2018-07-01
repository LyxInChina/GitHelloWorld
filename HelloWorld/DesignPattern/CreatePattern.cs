using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HelloWorld.DesignPattern
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

            private static string _name = Process.GetCurrentProcess().MainModule.FileName;
            private static Mutex _mutex;
            public static SingleClass GetInstanceProcessAsync()
            {
                if (_name.Length > 260)
                {
                    _name = _name.Substring(_name.Length - 261);
                }
                try
                {
                    //MutexSecurity ms = new MutexSecurity(_name, System.Security.AccessControl.AccessControlSections.Access);
                    _mutex = new Mutex(true, _name, out bool res);
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
                        if (Mutex.TryOpenExisting(_name, out _mutex))
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

    /// <summary>
    /// 简单工厂模式
    /// </summary>
    public class SimpleFactory
    {
        public abstract class Product
        {
            public abstract void Func();
        }

        public class Product1 : Product
        {
            public override void Func()
            {
                Console.WriteLine("Product1");
            }
        }

        public class Product2 : Product
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

    }

    /// <summary>
    /// 抽象工厂模式
    /// </summary>
    public class AbstructFactory
    {
        public abstract class Product
        {
            public abstract void Func();
        }

        public class ProductA : Product
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

        public class ProductAFactory : Factory
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

    }

    /// <summary>
    /// 构造者模式
    /// </summary>
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

        public class Product1Bulder : Builder
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

        public class Product2Bulder : Builder
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
    }

    /// <summary>
    /// 原型模式
    /// </summary>
    public class PrototypePattern
    {
        public abstract class Prototype : ICloneable
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
                    if (this is null)
                    {
                        return null;
                    }
                    //二进制序列化
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new MemoryStream();
                    using (stream)
                    {
                        formatter.Serialize(stream, this);
                        stream.Seek(0, SeekOrigin.Begin);
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


    }

    #endregion
}
