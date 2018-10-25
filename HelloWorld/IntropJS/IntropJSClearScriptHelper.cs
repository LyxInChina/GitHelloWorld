using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JScript;
using System.IO;
using System.CodeDom.Compiler;
using System.Reflection;

namespace HelloWorld.IntropJS
{
    public class IntropJSClearScriptHelper
    {
        public static JScriptCodeProvider MJScriptCodeProvider;
        public static CompilerParameters parameters = new CompilerParameters();
        static IntropJSClearScriptHelper()
        {
            MJScriptCodeProvider = new JScriptCodeProvider();
            parameters.GenerateInMemory = true;
        }

        public static bool ImportJSFile(string file)
        {
            if (File.Exists(file))
            {
                try
                {
                    var str = file;
                    var pa = new CompilerParameters
                    {
                        GenerateInMemory = true
                    };
                    var result = MJScriptCodeProvider.CompileAssemblyFromSource(parameters, str);
                    var ass = result.CompiledAssembly;
                    var s = ass.GetType();
                    var obj = s.InvokeMember("Test", BindingFlags.InvokeMethod, null, null, new object[] { "this ok" });
                    Console.WriteLine("OBJ::" + obj);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public static object RunJS_MS(string code)
        {
            //var msc = new MSScriptControl.ScriptControlClass();
            //msc.Language = "javascript";
            //msc.AddCode("test.js");
            //var res = msc.Eval(@"test(""sfsafafas"")");
            //Console.WriteLine(res);
            return null;
        }
    }
}
