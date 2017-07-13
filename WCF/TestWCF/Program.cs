using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace TestWCF
{
    class Program
    {
        public class CallB : ServiceReference2.ServiceBCallback
        {
            public int Call(int t)
            {
                Console.WriteLine("B call:" + t);
                return t;
            }
        }

        static void Main(string[] args)
        {
            var a = new ServiceReference1.ServiceAClient();
            a.Open();
            if (a.State== System.ServiceModel.CommunicationState.Opened)
            {
                var t1 = a.GetDataA(99);
                Console.WriteLine(t1);
                var t2 = a.GetDataUsingDataContractA(new ServiceReference1.CompositeType() { BoolValue = true, StringValue = "testA" });
                Console.WriteLine(t2.BoolValue + "::" + t2.StringValue);
            }

            var cb = new CallB();
            var b = new ServiceReference2.ServiceBClient(new System.ServiceModel.InstanceContext(cb));
            b.Open();
            if (b.State == System.ServiceModel.CommunicationState.Opened)
            {
                var t1 = b.GetDataB(69);
                Console.WriteLine(t1);
                var t2 = b.GetDataUsingDataContractB(new ServiceReference2.CompositeType() { BoolValue = true, StringValue = "testB" });
                Console.WriteLine(t2.BoolValue + "::" + t2.StringValue);
            }

            var c = new ServiceReference3.ServiceCClient();
            c.Open();
            if (c.State == System.ServiceModel.CommunicationState.Opened)
            {
                c.SetDataC(99);
            }
            Console.ReadLine();
            a.Close();
            b.Close();
            c.Close();


        }

        /// <summary>
        /// 构建一般服务连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="binding"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        static Tuple<T, ChannelFactory<T>> BuildChannel<T>(Binding binding,EndpointAddress address)
        {
            var factory = new ChannelFactory<T>(binding, address);
            return Tuple.Create(factory.CreateChannel(), factory); 
        }

        /// <summary>
        /// 构建双工通信的服务连接 双工通信提供回调URL 在binding中设置ClientAddress
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="binding"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        static Tuple<T, DuplexChannelFactory<T>> BuildDuplexChannel<T>(InstanceContext context,Binding binding, EndpointAddress address)
        {
            var factory = new DuplexChannelFactory<T>(context,binding, address);
            
            return Tuple.Create(factory.CreateChannel(), factory);
        }
        /*
                         var callback = new EncoderCallProxy();
                var cf = new DuplexChannelFactory<MLRealtimeEncoderLib.IMLRealtimeEncoderStatusMonitorService>(new InstanceContext(callback));
                var binding = new WSDualHttpBinding();
                binding.ClientBaseAddress = new Uri("http://127.0.0.1:11766/");
                cf.Endpoint.Binding = binding;
                cf.Endpoint.Address = new EndpointAddress($"http://127.0.0.1:11756/");
                _configService = cf.CreateChannel();
                ((IClientChannel)_configService).OperationTimeout = TimeSpan.FromMilliseconds(5000);
                MLEncodersMgr.Instance.MEncoderService = _configService;
                MLEncodersMgr.Instance.EncoderProxy = callback;

         */
    }
}
