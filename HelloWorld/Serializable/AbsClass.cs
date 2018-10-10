using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HelloWorld.Serializable
{

    [Serializable]
    public abstract class AbsClass
    {
    }

    //[XmlRoot(DataType = "A",ElementName ="OKOK")]
    [XmlType(TypeName = "AbsClass")]
    [Serializable]
    public class A:AbsClass
    {
        public string Str1;
        public int a;
        public MyEnum My;
        public DateTime Now = DateTime.Now;
    }

    //[XmlRoot(DataType = "B")]
    [XmlType(TypeName = "AbsClass")]
    [Serializable]
    public class B:AbsClass
    {
        public string str2;
        public string str3;
        public int a;
        public MyEnum2 My2;
        public DateTime Now = DateTime.Now;
    }
    
    public enum MyEnum
    {
        A,
        B,
        C,
        D,
    }
    public enum MyEnum2
    {
        AA,
        BA,
        CA,
        DA,
    }

    public static class SerializableHelper 
    {
        public static void SerializableTest()
        {
            var a = new A()
            {
                a = 998,
                My = MyEnum.C,
                Str1 = "kokoko",
            };
            Serializable(a);
            AbsClass abss;
            Deserializable(out abss);
            A aa;
            B bb;
            Deserializable(out aa);
            Deserializable(out bb);
        }
        public static void Serializable<T>(T abs)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var ser = new XmlSerializer(typeof(T));
            var f = typeof(AbsClass).FullName + ".xml";
            if (File.Exists(f))
            {
                File.Delete(f);
            }
            var stream = new FileStream(f,FileMode.OpenOrCreate);
            ser.Serialize(stream, abs, ns);
            stream.Flush();
            stream.Close();            
        }

        public static void Deserializable<T>(out T abs)
        {
            abs = default(T);
            var ser = new XmlSerializer(typeof(T));
            var f = typeof(T).FullName + ".xml";
            if (File.Exists(f))
            {
                var stream = new FileStream(f, FileMode.Open);
                var obj = ser.Deserialize(stream);
                if(obj is T)
                {
                    abs = (T)obj;
                }
            }
        }
    }


}
