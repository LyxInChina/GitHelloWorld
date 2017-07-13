using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Icl;
using IclLibrary;

namespace WcfServiceHost
{
    class Program
    {
        static List<ServiceHost> hosts = new List<ServiceHost>();
        static void Main(string[] args)
        {
            var a = BuldHost(typeof(CLA));
            hosts.Add(a);

            //var b = BuldHost(typeof(CLB));
            //hosts.Add(b);

            //var c = BuldHost(typeof(CLC));
            //hosts.Add(c);

            //var d = BuldHostEx<IServiceB>(typeof(CLB), @"http://127.0.0.1:10232");
            //hosts.Add(d);

            Console.ReadKey();
            hosts.ForEach(h => h?.Close());            
        }

        static ServiceHost BuldHost(Type type)
        {
            try
            {
                if (type != null)
                {
                    var host = new ServiceHost(type);
                    host.Opened += delegate
                    {
                        Console.WriteLine("Service" + type.Name + "已经启动，按任意键终止服务！");
                    };
                    host.Open();
                    return host;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        static ServiceHost BuldHostEx<T>(Type type,string url)
        {
            try
            {
                var  _host = new ServiceHost(type);

                // a b c

                var address = new Uri(url);
                var binding = new WSDualHttpBinding();
                var t = typeof(T);
                //_host.AddServiceEndpoint(typeof(T),
                //    new WSDualHttpBinding(),
                //    new Uri(url)
                //    );
                _host.AddServiceEndpoint(t, binding, address);
                //数据发布
                if (_host.Description.Behaviors.Find<ServiceMetadataBehavior>()==null)
                {
                    //服务元数据
                    var behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    //元数据Url
                    behavior.HttpGetUrl = new Uri(url + "/metadata");
                    //将元数据信息添加到服务
                    _host.Description.Behaviors.Add(behavior);
                }
                _host.Faulted += (object sender, EventArgs e) => { };
                _host.Opened += (sender, e) =>
                  {
                      var h = sender as ServiceHost;
                      Console.WriteLine("service has opened :"+h.BaseAddresses);
                  };
                if (_host.State != CommunicationState.Opening)
                {
                    try
                    {
                        _host.Open();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


    }
}
