using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.CLR
{
    public class AppDomainHelper
    {
        public static bool CreateDomain(string name,string dir)
        {
            var applicationIdentity = new ApplicationIdentity("AppFullName");
            var activationContext = ActivationContext.CreatePartialActivationContext(applicationIdentity, null);
            var setup = new AppDomainSetup(activationContext);
            //
            setup.ApplicationName = "AppName";
            setup.ApplicationBase = dir;
            setup.DynamicBase = dir;
            setup.PrivateBinPath = dir;
            setup.DisallowApplicationBaseProbing = true;
            var domain = AppDomain.CreateDomain(name, null, setup, null);

            return false;
        }

        public static bool LoadAssembly(AppDomain domain,string dll)
        {
            var name = AssemblyName.GetAssemblyName(dll);
            domain.Load(name);

            return false;
        }

        public static void Calling()
        {
            var domain = AppDomain.CreateDomain("A1 #1", null);
            //load a assembly
            MarshalByRefType mbrt = null;

            //得到代理引用 按引用封送
            mbrt = (MarshalByRefType)domain.CreateInstanceAndUnwrap("assembly.FullName", "MarshalByRefType");
            Console.WriteLine("Is TransparentProxy:{0}", RemotingServices.IsTransparentProxy(mbrt));

            //得到按引用封送的结构对象
            var mbvst = mbrt.GetMarshalByValStructType();
            //得到按值封送的类对象
            var mbvt = mbrt.GetMarshalByValType();

            //无法得到不可封送类型
            var nmt = mbrt.GetNonMarshalType();


            AppDomainManager mgr = new AppDomainManager();

        }

    }

    [Serializable]
    public class MarshalByValType
    {
        public string GetDateTimeStr()
        {
            return DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        }
    }

    [Serializable]
    public class MarshalByValStructType
    {
        public string GetDateTimeStr()
        {
            return DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        }
    }

    public class NonMarshalType
    {
        public string GetDateTimeStr()
        {
            return DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        }
    }

    public class MarshalByRefType : MarshalByRefObject
    {
        public MarshalByValType GetMarshalByValType()
        {
            return new MarshalByValType();
        }

        public MarshalByValStructType GetMarshalByValStructType()
        {
            return new MarshalByValStructType();
        }

        public NonMarshalType GetNonMarshalType()
        {
            return new NonMarshalType();
        }
    }

}
