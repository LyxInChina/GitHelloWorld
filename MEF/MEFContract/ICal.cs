using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 公用约定
/// </summary>
namespace MEFContract
{

    public interface ICal
    {
        IList<string> GetOperations(); 
        int Add(int a, int b);
    }

    public interface ICalShow
    {
        string Show(int a);
        string Show(int a, int b);
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class)]
    public class MetaDataExAttribute : ExportAttribute
    {
        public string V { get; set; }
        public MetaDataExAttribute(string name, Type type)
            : base(name, type)
        {
            
        }
    }

    public interface IMetaDataEx
    {
        string V { get; }//只能定义只读属性 才能用作MetaData代替
    }

}
