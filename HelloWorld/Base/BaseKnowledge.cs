using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Base
{
    public class BaseKnowledge
    {
        /// <summary>
        /// 编译器条件编译
        /// </summary>
        [Conditional(conditionString: "Release")]
        static void Fun1()
        {
        }
        
        public interface ITest
        {
            string S { get; set; }
            Delegate MDelegate { get; set; }
            void Func1();
            
        }

        public abstract class BClass
        {
            public virtual string S { get; set; }
            public abstract string AS { get; set; }
        }

        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            var v = Assembly.GetExecutingAssembly().GetName().Version;
            var v1 = Assembly.GetExecutingAssembly().GetName().VersionCompatibility;
            var v2 = Assembly.GetExecutingAssembly().GetCustomAttributesData();
            //Run();
            //RunRegex();
            IntropJS.RunJS_V8();
            IntropJS.RunJS_MS("");
            RunJS();
            Console.ReadLine();
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var ass = args.Name;
            var sa = args.RequestingAssembly;
            throw new NotImplementedException();
        }

        private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            //throw new NotImplementedException();
            var ass = args.LoadedAssembly;
        }

        public static void RunJS()
        {
            var file = @"test.js";
            if (IntropJS.ImportJSFile(file))
            {

            }
        }

        public static void RunRegex()
        {
            RegexTest.HasStr();
            RegexTest.IsHtml();
        }

    }
}

/*  Base Knowledge
 *  
 *  对象深复制
 *  
 *  序列化
 *  
 *  JSON格式化
 *  
 *  依赖注入和控制反转
 * 
 * 
 */
