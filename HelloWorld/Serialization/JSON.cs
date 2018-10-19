using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace HelloWorld.Serializable
{

    [DataContract]
    public class JsonData
    {

        [DataMember]
        public bool BoolValueIgnored { get; set; }

        [DataMember]
        public bool BoolValue { get; set; }

        [DataMember]
        public int IntValue { get; set; }

        [DataMember(EmitDefaultValue =false,IsRequired =true,Name ="string",Order =0)]
        public string StrValue { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = true, Name = "list<string>", Order = 1)]
        public List<string> StrValues { get; set; }
    }

    public class JSonSerializationHelper
    {
        /// <summary>
        /// JSON序列化类到字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static bool Serialize<T>(T t, out string jsonStr)
        {
            var result = false;
            jsonStr = null;
            try
            {
                System.Diagnostics.Contracts.Contract.Requires(t != null);
                var js = new DataContractJsonSerializer(typeof(T));
                using (var ms = new MemoryStream())
                {
                    js.WriteObject(ms, t);
                    ms.Position = 0;
                    var reader = new StreamReader(ms);
                    jsonStr = reader.ReadToEnd();
                    reader.Close();
                    ms.Close();
                    if (!string.IsNullOrEmpty(jsonStr))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Fail(ex.Message, ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// JSON反序列化字符串到类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool Deserialize<T>(string jsonStr, out T t)
        {
            var result = false;
            t = default(T);
            if (string.IsNullOrEmpty(jsonStr))
            {
                return false;
            }
            try
            {
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonStr)))
                {
                    var js = new DataContractJsonSerializer(typeof(T));
                    t = (T)js.ReadObject(ms);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Fail(ex.Message, ex.StackTrace);
            }
            return result;
        }
    }
}

