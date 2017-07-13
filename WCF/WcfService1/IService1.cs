using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    //服务契约

    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract(Namespace ="http://127.0.0.1:55566")]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: 在此添加您的服务操作
    }

    //数据契约

    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    [DataContract]
    public class CompositeType:INotifyPropertyChanged
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { SetProperty(ref boolValue, value); }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { SetProperty(ref stringValue, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnNotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void SetProperty<T>(ref T item,T value,[CallerMemberName] string propertyName=null)
        {
            if (!EqualityComparer<T>.Default.Equals(item,value))
            {
                item = value;
                OnNotifyPropertyChanged(propertyName);
            }
        }
    }

    //错误契约

    [FaultContract(detailType:typeof(IService1)]
    public class FaultException
    {

    }
    
    //消息契约

    [MessageContract]
    public class Msg
    {

    }

}
