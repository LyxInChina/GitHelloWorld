using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VSX.Common
{
    public class SerializationHelper
    {
        public static string ExternFileName = ".xml";
        public static void Serializable<T>(T abs,string externName=".xml")
        {
            var ser = new XmlSerializer(typeof(T));
            var f = typeof(T).FullName + externName;
            if (File.Exists(f))
            {
                File.Delete(f);
            }
            var stream = new FileStream(f, FileMode.OpenOrCreate);
            ser.Serialize(stream, abs);
            stream.Flush();
            stream.Close();
        }

        public static void Deserializable<T>(out T abs, string externName = ".xml")
        {
            abs = default(T);
            var ser = new XmlSerializer(typeof(T));
            var f = typeof(T).FullName + externName;
            if (File.Exists(f))
            {
                var stream = new FileStream(f, FileMode.Open);
                var obj = ser.Deserialize(stream);
                if (obj is T)
                {
                    abs = (T)obj;
                }
            }
        }
    }
}
