using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MEF.MEFContract;

namespace MEF
{
    class Program
    {
        static void Main(string[] args)
        {
            var pa = System.AppDomain.CurrentDomain.BaseDirectory;
            //文件系统监控
            FileSystemWatcher watcher=null;
            FileWatch(watcher, pa);

            MefTest(pa);

            Console.ReadKey();
        }


        public static void FileWatch(FileSystemWatcher w, string path)
        {
            w = new FileSystemWatcher(path, "*.txt");
            //设置监视类型
            //w.NotifyFilter = NotifyFilters.FileName |
            //                 NotifyFilters.LastWrite ;
            Console.WriteLine("WathcPath:" + path + ";FileType:" + "*.txt");
            w.Changed += (sender, e) =>
            {
                Console.WriteLine("Changed   Name:" + e.Name + ";ChangeType:" + e.ChangeType + ";FullPath:" + e.FullPath);
            };
            w.Created += (s, e) =>
            {
                Console.WriteLine("Created   Name:" + e.Name + ";ChangeType:" + e.ChangeType + ";FullPath:" + e.FullPath);
            };
            w.Deleted += (s, e) =>
            {
                Console.WriteLine("Delete   Name:" + e.Name + ";ChangeType:" + e.ChangeType + ";FullPath:" + e.FullPath);
            };
            w.Renamed += (s, e) =>
            {
                Console.WriteLine("Rename   Name:" + e.Name + ";ChangeType:" + e.ChangeType + ";FullPath:" + e.FullPath);
                Console.WriteLine("Rename   OldName:" + e.OldName + ";OldFullPath:" + e.OldFullPath);
            };
            w.EnableRaisingEvents = true;//开始监视

        }

        static void MefTest(string pa)
        {
            var a = new MEFClient.MyClass();
            if (Find(pa, a))
            {
                var t = a.cal.GetOperations();
            }
            var b = new MEFClient.MyCal();
            FindExt<MEFClient.MyCal, MEFContract.ICalShow>(pa, b);
            var s = b.Show?.Show(5);
            var c = new MEFClient.MCal2();
            if (Find(pa, c))
            {
                var res3 = c.MDefinC.GetStr();
                var res4 = c.Subtract[0].Value(0.3, 0.4);
                //var resi3 = c.Add[0](90, 90);
                var s1 = "1";
                var s2 = "2";
                c.RefeObj(s1, s2);
            }
        }

        /// <summary>
        /// 使用特性标识查找部件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool Find(string path,object obj)
        {
            bool res = false;
            DirectoryCatalog dcl = new DirectoryCatalog(path);
            CompositionContainer cc = new CompositionContainer(dcl);
            try
            {
                cc.ComposeParts(obj);
                res = true;
            }
            catch (ChangeRejectedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        public static async void InitlizeContainer(string path,object obj)
        {
            var catalog = new DirectoryCatalog(path);
            var cb = new CompositionBatch();
            cb.AddPart(obj);

            var container = new CompositionContainer(catalog);
            var minc = new MEFClient.MImC();
            minc.OmImportsStatisied += (s, e) =>
            {
                Console.WriteLine(e);
            };
            await Task.Run(() =>
            {
                container.ComposeParts(minc);
                container.Compose(cb);
            });
        }

        public static async void InitlizeContainer<T>(string path, object obj)
            where T: IPartImportsSatisfiedNotification, new()
        {
            var catalog = new DirectoryCatalog(path);
            var cb = new CompositionBatch();
            cb.AddPart(obj);

            var container = new CompositionContainer(catalog);
            var part = new T();
            part.OnImportsSatisfied();            
            await Task.Run(() =>
            {
                container.ComposeParts(part);
                container.Compose(cb);
            });
        }

        /// <summary>
        /// 基于约定的部件注册
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool FindEx(string path, MEFClient.MyCal obj)
        {
            var res = false;
            var rb = new RegistrationBuilder();//用于定义出口和入口的约定
            rb.ForTypesDerivedFrom<MEFContract.ICalShow>().Export<MEFContract.ICalShow>();//指定出口约定  给所有实现接口的类型应用[Export(Type)]特性
            rb.ForType<MEFClient.MyCal>().ImportProperty<MEFContract.ICalShow>(t =>t.Show);//指定入口约定 将入口映射到指定的属性上
            var dc = new DirectoryCatalog(path,rb);
            CompositionService sv = dc.CreateCompositionService();
            try
            {
                sv.SatisfyImportsOnce(obj, rb); //在目录中搜索部件 多个出口匹配项出异常
                res = true;
            }
            catch (ChangeRejectedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (CompositionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sv.Dispose();
            return res;
        }
        
        public static bool FindExt<T,K>(string path, T obj)
        {
            return FindExt<T, K>(path, obj, p => p.PropertyType == typeof (K));
        }

        /// <summary>
        /// 基于约定的部件注册 类T注册接口K
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        /// <param name="propertyFilter"></param>
        /// <returns></returns>
        public static bool FindExt<T, K>(string path, T obj, Predicate<PropertyInfo> propertyFilter)
        {
            var res = false;
            var rb = new RegistrationBuilder();//用于定义出口和入口的约定
            rb.ForTypesDerivedFrom<K>().Export<K>();//指定出口约定  给所有实现接口的类型应用[Export(Type)]特性
            rb.ForType<T>().ImportProperties(propertyFilter);//指定入口约定
            var dc = new DirectoryCatalog(path, rb);
            CompositionService sv = dc.CreateCompositionService();
            try
            {
                sv.SatisfyImportsOnce(obj, rb); //在目录中搜索部件 若存在多个出口匹配项则抛出出异常
                res = true;
            }
            catch (ChangeRejectedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (CompositionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sv.Dispose();
            return res;
        }
        
    }

    /*定义约定*/
    /// <summary>
    /// 公用约定
    /// </summary>
    namespace MEFContract
    {
        /// <summary>
        /// 定义计算器接口
        /// </summary>
        public interface ICal
        {
            /// <summary>
            /// 获取运算符
            /// </summary>
            /// <returns></returns>
            IList<IOperation> GetOperations();

            /// <summary>
            /// 进行运算
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            int Opeartion(IOperation op, int[] paras); 
        }

        public interface IOperation
        {
            string Name { get; }
            int NumberOperands { get; }
        }



        public interface ICalShow
        {
            string Show(int a);
            string Show(int a, int b);
        }

        [MetadataAttribute]
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
        public class MetaDataExAttribute : ExportAttribute
        {
            public string V { get; set; }
            public MetaDataExAttribute(string name, Type type)
                : base(name, type)
            {

            }
        }

        public interface IMetaDataEx
        {
            string V { get; }//只能定义只读属性 才能用作MetaData代替
        }

    }

    /*定义实现*/
    /// <summary>
    /// 具体实现
    /// </summary>
    namespace MEFAchieve
    {
        /// <summary>
        /// 导出计算器A
        /// </summary>
        [Export(contractName: "CalculatorA", contractType: typeof(MEFContract.ICal))]
        public class CalculatorA : MEFContract.ICal
        {

            public int Opeartion(IOperation op, int[] paras)
            {
                if (op == null||paras==null)
                {
                    return -2;
                }
                if(op.NumberOperands!=paras.Length)
                {
                    return -3;
                }
                var result = 0;
                switch (op.Name)
                {
                    case "+":
                        result = paras[0] + paras[1]; 
                        break;
                    case "-":
                        result = paras[0] - paras[1];
                        break;
                    case "*":
                        result = paras[0] * paras[1];
                        break;
                    case "/":
                        {
                            if(paras[1]==0)
                            {
                                result = int.MinValue;
                            }
                            else
                            {
                                result = paras[0] + paras[1];
                            }
                        }
                        break;
                    default:    
                        break;
                }
                return result;
            }

            IList<IOperation> ICal.GetOperations()
            {
                return new List<IOperation>
                {
                    new OperationA(){Name="+",NumberOperands=2},
                    new OperationA(){Name="-",NumberOperands=2},
                    new OperationA(){Name="*",NumberOperands=2},
                    new OperationA(){Name="/",NumberOperands=2}

                };
            }
        }

        /// <summary>
        /// 导出计算器B
        /// </summary>
        [Export(contractName: "CalculatorB", contractType: typeof(MEFContract.ICal))]
        public class CalculatorB : MEFContract.ICal
        {

            public int Opeartion(IOperation op, int[] paras)
            {
                if (op == null || paras == null)
                {
                    return -2;
                }
                if (op.NumberOperands != paras.Length)
                {
                    return -3;
                }
                var result = 0;
                switch (op.Name)
                {
                    case "|":
                        result = paras[0] | paras[1];
                        break;
                    case "&":
                        result = paras[0] & paras[1];
                        break;
                    case "<<":
                        result = paras[0] << paras[1];
                        break;
                    case ">>":
                        result = paras[0] >> paras[1];
                        break;
                    default:
                        break;
                }
                return result;
            }


            IList<IOperation> ICal.GetOperations()
            {
                return new List<IOperation>
                {
                    new OperationA(){Name="|",NumberOperands=2},
                    new OperationA(){Name="&",NumberOperands=2},
                    new OperationA(){Name="<<",NumberOperands=2},
                    new OperationA(){Name=">>",NumberOperands=2}

                };
            }
        }

        public class OperationA : IOperation
        {
            public string Name { get; internal set; }

            public int NumberOperands { get; internal set; }
        }



        public class CalculatorShow : MEFContract.ICalShow
        {
            public string Show(int a)
            {
                return "ok int :: " + a;
            }

            public string Show(int a, int b)
            {
                return "ok int :: " + a + ",int :: " + b;
            }
        }

        [Export("MEFAchieve.DefinC")]
        public class DefineC
        {

            public string GetStr()
            {
                return "Definc ";
            }
        }

        public class DefineD
        {
            [Export("Add", typeof(Func<int, int, int>))]
            [ExportMetadata("n1", "v1")]
            public int Add(int a, int b)
            {
                return a + b;
            }

            [Export("RefeObj", typeof(Action<string, string>))]
            [ExportMetadata("n2", "v2")]
            public void RefeObj(string s1, string s2)
            {
                s1 += s2;
            }

            [MEFContract.MetaDataEx("Subtract", typeof(Func<double, double, double>), V = "VX")]
            public double Subtract(double d1, double d2)
            {
                return d1 - d2;
            }

        }
    }

    /*定义使用*/
    namespace MEFClient
    {
        /// <summary>
        /// 入口部件类
        /// </summary>
        class MyClass
        {
            //入口标识
            [Import]
            public MEFContract.ICal cal { get; set; }

        }

        /// <summary>
        /// 入口部件类
        /// </summary>
        public class MyCal
        {
            public MEFContract.ICalShow Show { get; set; }
        }

        public class MCal2
        {
            [Import("MEFSimple.DefinC")]
            public dynamic MDefinC { get; set; }

            [ImportMany("Add", typeof(Func<int, int, int>))]
            public Func<int, int, int>[] Add { get; set; }

            [ImportMany("Add", typeof(Func<int, int, int>))]
            public Lazy<Func<int, int, int>, IDictionary<string, object>>[] AddEx { get; set; }

            [Import("RefeObj", typeof(Action<string, string>))]
            public Action<string, string> RefeObj { get; set; }

            [ImportMany("Subtract", typeof(Func<double, double, double>))]
            public Lazy<Func<double, double, double>, MEFContract.IMetaDataEx>[] Subtract { get; set; }

        }

        public class MImC : IPartImportsSatisfiedNotification
        {
            [Import("MEFAchieve.DefinC")]
            public dynamic MDefinC { get; set; }

            [Import("MEFAchieve.DefinC")]
            public Lazy<dynamic> MLDefinC { get; set; } //惰性加载部件

            [ImportMany(AllowRecomposition = true)]
            public IEnumerable<Lazy<MEFContract.ICalShow, MEFContract.IMetaDataEx>> CalShow { get; set; }

            [ImportMany("Add", typeof(Func<int, int, int>))]
            public Func<int, int, int>[] Add { get; set; }

            [ImportMany("Add", typeof(Func<int, int, int>))]
            public Lazy<Func<int, int, int>, IDictionary<string, object>>[] AddEx { get; set; }

            [Import("RefeObj", typeof(Action<string, string>))]
            public Action<string, string> RefeObj { get; set; }

            [ImportMany("Subtract", typeof(Func<double, double, double>))]
            public Lazy<Func<double, double, double>, MEFContract.IMetaDataEx>[] Subtract { get; set; } //自定义导出特性


            public event EventHandler OmImportsStatisied;
            /// <summary>
            /// 加载完成事件
            /// </summary>
            public void OnImportsSatisfied()
            {
                //throw new NotImplementedException();
            }
        }
    }

}

/*  MEF Managed Extensibility Framework
 *  所在命名空间 System.ComponentModel.Composition
 *  
 *  宿主->容器->类别->导出部件<--导入部件<-宿主
 *  
 *  宿主类：类别和容器
 *  基元类：基类
 *  特性类：基于特性的类
 *  
 */
