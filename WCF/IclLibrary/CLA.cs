using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Icl;
using System.ServiceModel.Web;

namespace IclLibrary
{
    [ServiceBehavior()]
    public class CLA : IServiceA
    {
        private string m = AppDomain.CurrentDomain.FriendlyName;
        public string GetDataA(int value)
        {
            return m + "_A_" + value;
        }

        public CompositeType GetDataUsingDataContractA(CompositeType composite)
        {
            return new CompositeType() { BoolValue = !composite.BoolValue, StringValue = m + "_A_" + composite.StringValue };
        }
    }

    [ServiceBehavior(ConcurrencyMode= ConcurrencyMode.Multiple)]
    public class CLB : IServiceB
    {
        private string m = AppDomain.CurrentDomain.FriendlyName;
        
        public string GetDataB(int value)
        {
            if (value>50)
            {
                Console.WriteLine("GetDataB call value:" + value);
                //服务端回调 获取回调信道
                var call = OperationContext.Current.GetCallbackChannel<Icl.ICallBackA>();
                if (call!=null)
                {
                    var res = call.Call(value);
                    Console.WriteLine("value call back:" + value + ",rsult is :" + res);
                }
            }
            return m + "_B_" + value;
        }

        public CompositeType GetDataUsingDataContractB(CompositeType composite)
        {
            return new CompositeType() { BoolValue = !composite.BoolValue, StringValue = m + "_B_" + composite.StringValue };
        }
    }

    [ServiceBehavior]
    public class CLC : IServiceC
    {
        private string m = AppDomain.CurrentDomain.FriendlyName;
        
        public void SetDataC(int value)
        {
            Console.WriteLine("receive value:" + value);
        }

        [WebGet(UriTemplate ="string?name={name}")]
        public string GetStr(string name)
        {
            return "";
        }

        [WebGet(UriTemplate = "Tuple<string,int>?name={name}&a={a}&b={b}")]
        public Tuple<string,int> GetStr(string name,int a,int b)
        {
            return Tuple.Create(name,a*b);
        }

    }

}
