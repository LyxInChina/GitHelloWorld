using System;
using System.Diagnostics;



namespace HelloWorld
{
    class ProgramIL
    {
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            var v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var v1 = System.Reflection.Assembly.GetExecutingAssembly().GetName().VersionCompatibility;
            var v2 = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributesData();
            //Run();
            //RunRegex();
            IntropJS.RunJS_V8();
            IntropJS.RunJS_MS("");
            RunJS();
            Console.ReadLine();
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
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
            if(IntropJS.ImportJSFile(file))
            {
                
            }    
        }

        public static void RunRegex()
        {
            RegexTest.HasStr();
            RegexTest.IsHtml();
        }

        public static void Run()
        {
            var methodName = new StackTrace().GetFrame(1).GetMethod().Name;
            Console.WriteLine(methodName);
            int a = 10;
            int b = 20;
            var c = 30;
            var d = Fun(a, b);
            string s1 = "10";
            string s2 = "20";
            var s3 = "30";
            var s4 = s3;
            Console.WriteLine("a is " + a);
            Console.WriteLine("c is " + c);
            Console.WriteLine("a+b=" + (a + b));
            Console.WriteLine("Fun(a,b) is " + d);
            Console.WriteLine("s1 is " + s1);
            Console.WriteLine("s1 + s2 is " + s1 + s2);
            Console.WriteLine("s3 + s4 is " + s3 + s4);
            Fun1();
            Console.WriteLine("a is " + a);
            ClSA cl = new ClSA() { Name = "012345", Address = "Ads0123", Number = 123 };
            var t = ((IFormattable)cl).ToString("", new CustomFormatter());
            Console.Write("___");
            Console.Write(t);
            Console.Write("___");
            Console.ReadLine();
        }
        
        /// <summary>
        /// 编译器条件编译
        /// </summary>
        [Conditional(conditionString:"Release")]
        static void Fun1()
        {
            
            //return a + b;
        }
        static int Fun(int a, int b)
        {
            return a + b;
        }
    }

    class ClSA : IFormattable
    {
        public string Name { get; set; }
        public int Number { get; set; }
        
        public string Address { get; set; }

        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            if(formatProvider!=null)
            {
                ICustomFormatter fmt = formatProvider.GetFormat(GetType()) as ICustomFormatter;
                if (fmt != null)
                    return fmt.Format(format, this, formatProvider);
            }
            switch(format)
            {
                case "Na":
                    return Name;
                case "Nu":
                    return Number.ToString();
                case "Ad":
                    return Address;
                default:
                    return "Name=" + Name + ",Number=" + Number.ToString() + ",Address=" + Address;
            }
        }
    }

    class CustomFormatter : IFormatProvider
    {
        object IFormatProvider.GetFormat(Type formatType)
        {
            //if (formatType == typeof(IFormattable))
            if(formatType.GetInterface(typeof(IFormattable).Name)!=null)
            {
                return new CustomFormatProvider();
            }
            return null;
        }
    }

    class CustomFormatProvider : ICustomFormatter
    {
        string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider)
        {
            ClSA c = arg as ClSA;
            if(c!=null)
            {
                return string.Format("{0,-10:D9},{1,10},{2,10:x}", c.Name, c.Address, c.Number);
            }
            return arg.ToString();

        }
    }

    public static class DeepClone
    {
        //public static T DeepClone<T>(this T obj) where T : class
        //{
        //    return obj != null ? obj.ToJson().FromJson<T>() : null;
        //}
    }

}

