using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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
            var assembles = AppDomain.CurrentDomain.GetAssemblies();
            var types = new List<Type>();
            var typesh = new List<RuntimeTypeHandle>();
            var methods = new List<MethodInfo>();
            var methodsh = new List<RuntimeMethodHandle>();
            var fields = new List<FieldInfo>();
            var fieldsh = new List<RuntimeFieldHandle>();
            for (int i = 0; i < assembles.Length; i++)
            {
                types = assembles[i].GetTypes().ToList();                
            }
            GC.KeepAlive(types);
            PrintMemory("After build type");
            typesh = types.ConvertAll(e => e.TypeHandle);
            GC.KeepAlive(types);
            PrintMemory("After build typehandle");
            types = null;
            GC.Collect(0);
            PrintMemory("After Collect type");
        }
        
    }
}
