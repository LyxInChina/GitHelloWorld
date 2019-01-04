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
    public class A : AbsClass
    {
        public string Str1;
        public int a;
        public MyEnum My;
        public DateTime Now = DateTime.Now;
    }

    //[XmlRoot(DataType = "B")]
    [XmlType(TypeName = "AbsClass")]
    [Serializable]
    public class B : AbsClass
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

    public static class XmlSerializableHelper
    {
        public static void SerializableTest()
        {
            var a = new A()
            {
                a = 998,
                My = MyEnum.C,
                Str1 = "kokoko",
            };
        }

        public static bool Serialize<T>(T t, out string xml)
        {
            xml = null;
            try
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                var ser = new XmlSerializer(typeof(T));
                using (var ms = new MemoryStream())
                {
                    ser.Serialize(ms, t, ns);
                    ms.Position = 0;
                    var reader = new StreamReader(ms);
                    xml = reader.ReadToEnd();
                    reader.Close();
                }
                return true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.Fail(ex.Message, ex.StackTrace);
            }
            return false;
        }

        public static bool Deserialize<T>(string xml, out T t)
        {
            t = default(T);
            if (string.IsNullOrEmpty(xml))
            {
                return false;
            }
            try
            {
                var ser = new XmlSerializer(typeof(T));
                var bytes = Encoding.Unicode.GetBytes(xml);
                using (var ms = new MemoryStream(bytes))
                {
                    var obj = ser.Deserialize(ms);
                    if (obj is T)
                    {
                        t = (T)obj;
                        return true;
                    }
                }
            }
            catch (System.Exception ex)
            {

                System.Diagnostics.Trace.Fail(ex.Message, ex.StackTrace);
            }
            return false;
        }

        public static void Serializable<T>(T abs, string file)
        {
            try
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                var ser = new XmlSerializer(typeof(T));
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                var stream = new FileStream(file, FileMode.OpenOrCreate);
                ser.Serialize(stream, abs, ns);
                stream.Flush();
                stream.Close();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.Fail(ex.Message, ex.StackTrace);
            }
        }

        public static void Deserializable<T>(string file, out T abs)
        {
            abs = default(T);
            try
            {
                var ser = new XmlSerializer(typeof(T));
                if (File.Exists(file))
                {
                    var stream = new FileStream(file, FileMode.Open);
                    var obj = ser.Deserialize(stream);
                    if (obj is T)
                    {
                        abs = (T)obj;
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.Fail(ex.Message, ex.StackTrace);
            }
        }
    }


}
