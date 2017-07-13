using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using MEFContract;

namespace MEFConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var pa = System.AppDomain.CurrentDomain.BaseDirectory;
            FileSystemWatcher watcher=null;
            FileWatch(watcher, pa);
            var a = new MyClass();
            if (Find(pa, a))
            {
                var t = a.cal.GetOperations();
            }
            var b=  new MyCal();
            FindExt<MyCal,ICalShow>(pa, b);
            var s = b.Show?.Show(5);
            var c = new MCal2();
            if (Find(pa, c))
            {
                var res3 = c.MDefinC.GetStr();
                var res4 = c.Subtract[0].Value(0.3, 0.4);
                //var resi3 = c.Add[0](90, 90);
                var s1 = "1";
                var s2="2";
                c.RefeObj(s1, s2);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 入口部件类
        /// </summary>
        class MyClass
        {
            //入口标识
            [Import]
            public ICal cal { get; set; }

        }

        /// <summary>
        /// 入口部件类
        /// </summary>
        public class MyCal
        {
            public ICalShow Show { get; set; }
        }

        public class MCal2
        {   
            [Import("MEFSimple.DefinC")]
            public dynamic MDefinC { get; set; }

            [ImportMany("Add", typeof(Func<int, int, int>))]
            public Func<int, int, int>[] Add { get; set; }

            [ImportMany("Add", typeof(Func<int, int, int>))]
            public Lazy<Func<int,int,int>,IDictionary<string ,object>>[] AddEx { get; set; }

            [Import("RefeObj",typeof(Action<string,string>))]
            public Action<string ,string> RefeObj { get; set; }

            [ImportMany("Subtract",typeof(Func<double,double,double>))]
            public Lazy<Func<double,double,double>,IMetaDataEx>[] Subtract { get; set; } 

        }

        public class MImC : IPartImportsSatisfiedNotification
        {
            [Import("MEFSimple.DefinC")]
            public dynamic MDefinC { get; set; }

            [Import("MEFSimple.DefinC")]
            public Lazy<dynamic> MLDefinC { get; set; } //惰性加载部件

            [ImportMany(AllowRecomposition = true)]
            public IEnumerable<Lazy<ICalShow, IMetaDataEx>> CalShow { get; set; }

            [ImportMany("Add", typeof(Func<int, int, int>))]
            public Func<int, int, int>[] Add { get; set; }

            [ImportMany("Add", typeof(Func<int, int, int>))]
            public Lazy<Func<int, int, int>, IDictionary<string, object>>[] AddEx { get; set; }

            [Import("RefeObj", typeof(Action<string, string>))]
            public Action<string, string> RefeObj { get; set; }

            [ImportMany("Subtract", typeof(Func<double, double, double>))]
            public Lazy<Func<double, double, double>, IMetaDataEx>[] Subtract { get; set; } //自定义导出特性


            public event EventHandler OmImportsStatisied;
            /// <summary>
            /// 加载完成事件
            /// </summary>
            public void OnImportsSatisfied()
            {
                throw new NotImplementedException();
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
            var minc = new MImC();
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

        public static void FileWatch(FileSystemWatcher w,string path)
        {
            w = new FileSystemWatcher(path,"*.txt");
            //设置监视类型
            //w.NotifyFilter = NotifyFilters.FileName |
            //                 NotifyFilters.LastWrite ;
            Console.WriteLine("WathcPath:"+path+";FileType:"+ "*.txt");
            w.Changed += (sender, e) =>
            {
                Console.WriteLine("Changed   Name:"+e.Name+";ChangeType:"+e.ChangeType+";FullPath:"+e.FullPath);
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
                Console.WriteLine("Rename   OldName:"+e.OldName+";OldFullPath:"+e.OldFullPath);
            };
            w.EnableRaisingEvents = true;//开始监视
           
        }

        /// <summary>
        /// 基于约定的部件注册
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool FindEx(string path, MyCal obj)
        {
            var res = false;
            var rb = new RegistrationBuilder();//用于定义出口和入口的约定
            rb.ForTypesDerivedFrom<ICalShow>().Export<ICalShow>();//指定出口约定  给所有实现接口的类型应用[Export(Type)]特性
            rb.ForType<MyCal>().ImportProperty<ICalShow>(t =>t.Show);//指定入口约定 将入口映射到指定的属性上
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
}
