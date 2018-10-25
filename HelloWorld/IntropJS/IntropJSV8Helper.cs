using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntropJS
{
    public class IntropJSV8Helper
    {
        public static object RunJS_V8()
        {
            using (var engine = new V8ScriptEngine())
            {

                engine.AddHostType("Console", typeof(Console));
                engine.Execute("Console.WriteLine('{0}',Math.PI)");

                engine.AddHostObject("random", new Random());
                engine.Execute("Console.WriteLine('Next random Number is {0}',random.Next())");



                var f = "test.js";
                var code = File.ReadAllText(f);
                engine.Compile(code);
                engine.AddHostType("Console", typeof(Console));
                engine.Execute("Console.WriteLine('{0} is an interesting number.', Math.PI)");
                engine.AddHostType("Console", typeof(Console));
                engine.Evaluate("Console.debug('faasfafa')");
                var res = engine.Evaluate("test(fsfs)");
                Console.WriteLine(res);
            }
            return null;
        }
    }
}
