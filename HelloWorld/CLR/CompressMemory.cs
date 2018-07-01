using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;

namespace HelloWorld.CLR
{
    public class CompressMemory
    {
        /* 压缩内存
         * 
         * 
         * 1.使用RuntimeTypeHandle,代替Type
         *  
         * 
         */


        public static void PrintMemory(string msg)
        {
            Console.WriteLine(string.Format("MemorySize:{0},Msg:{1}", GC.GetTotalMemory(true), msg));
        }

        public static void TestRuntimeHandle()
        {
            PrintMemory("Before");
            var refass = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            for (int i = 0; i < refass.Length; i++)
            {
                Assembly.Load(refass[i]);
            }
            var assembles = AppDomain.CurrentDomain.GetAssemblies();
            var types = new List<Type>();
            var typesh = new List<RuntimeTypeHandle>();
            var methods = new List<MethodInfo>();
            var methodsh = new List<RuntimeMethodHandle>();
            var fields = new List<FieldInfo>();
            var fieldsh = new List<RuntimeFieldHandle>();
            PrintMemory("Before get types");
            for (int i = 0; i < assembles.Length; i++)
            {
                types.AddRange(assembles[i].GetExportedTypes().ToList());                
            }

            //for (int i = 0; i < types.Count; i++)
            //{
            //    methods.AddRange(types[i].GetRuntimeMethods());
            //}
            Console.WriteLine("total Object:{0}", types.Count);
            //GC.KeepAlive(types);
            PrintMemory("After build type");
            typesh = types.ConvertAll(e => e.TypeHandle);
            //methodsh = methods?.ConvertAll(e => e.MethodHandle);
            //GC.KeepAlive(types);
            PrintMemory("After build typehandle");
            types = null;
            //methods = null;
            GC.Collect();
            Thread.Sleep(200);
            PrintMemory("After Collect object type");
            typesh = null;
            //methodsh = null;
            GC.Collect();
            PrintMemory("After Collect object type and handle");
        }
        
    }
}
