using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Icl
{
    //服务契约

    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract(Name ="ServiceA",Namespace ="http://127.0.0.1:11223")]
    public interface IServiceA
    {

        [OperationContract]
        string GetDataA(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContractA(CompositeType composite);

        // TODO: 在此添加您的服务操作
    }

    [ServiceContract(Name ="ServiceB",Namespace ="http://127.0.0.1:11224",CallbackContract =typeof(ICallBackA))]    
    public interface IServiceB
    {
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        string GetDataB(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContractB(CompositeType composite);

    }

    [ServiceContract(Name = "ServiceC", Namespace = "http://127.0.0.1:11225")]
    public interface IServiceC
    {
        [OperationContract(IsOneWay =true)]
        void SetDataC(int value);

        [OperationContract]
        Tuple<string, int> GetStr(string name, int a, int b);
    }


    [ServiceContract(Name = "ICallBackA")]
    public interface ICallBackA
    {
        [OperationContract]
        int Call(int t);
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

    //[FaultContract(detailType:typeof(Msg))]
    [DataContract]
    public class FaultException
    {
        [DataMember]
        public string Msg { get; set; }
    }
    
    //消息契约

    [MessageContract]
    public class Msg
    {
        [MessageBodyMember(Name ="msgb",Namespace ="msgb ms")]
        public string MsgB { get; set; }

        [MessageHeader(Name ="msgh", 
            Namespace ="msgh ms",
            MustUnderstand =true)]
        public string MsgH { get; set; }
    }

}
