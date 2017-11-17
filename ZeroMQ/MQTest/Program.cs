using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTest;
using System.Threading;
using static MQTest.ZeroMQTest;
using static HelloWorld.CircuitBreakerPattern;

namespace MQTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string arg="";
            if(args.Length>0)
            {
                arg = args[0];
            }
            Console.WriteLine("ZeroMQ VersionInfo:" + GetZeroMQVersion());
            Console.WriteLine("选择类型:server?client?publisher?subscriber?");
            string u = "tcp://127.0.0.1:8989";
            string us = "tcp://127.0.0.1:9000";
            bool b = true;
            bool t = true;
            do
            {
                string res;
                if (t)
                {
                    res = arg.ToLower();
                    t = false;
                    Console.WriteLine(res);
                }
                else
                {
                    res = Console.ReadLine().ToLower();
                }
                if (res.Contains(" "))
                {
                    var tt = res.Split(' ');
                    if (tt[0] == "c" && tt.Length > 1)
                    {
                        StartNewProcess(tt[1]);
                    }
                    continue;
                }
                switch (res)
                {
                    case "server":
                        DoServer(u);
                        break;
                    case "client":
                        DoClient(u);
                        break;
                    case "publisher":
                        DoPublisher(us);
                        break;
                    case "subscriber":
                        DoSubscriber(us);
                        break;
                    case "servercb":
                        DoServer_CircuitBreaker(u);
                        break;
                    case "clientcb":
                        DoClient_CircuitBreaker(u);
                        break;
                    case "v":
                        DoVentilator();
                        break;
                    case "t":
                        DoTaskWorker();
                        break;
                    case "s":
                        DoSink();
                        break;
                    case "exit":
                        b = false;
                        break;
                    case "pipe":
                        StartNewProcess("v");
                        StartNewProcess("t");
                        StartNewProcess("s");
                        break;
                    default:
                        break;
                }
            } while (b);
        }
        
        static void DoPublisher(string u)
        {
            var socket = Publish_Subscribe.CreatePublisher(u);
            var ran = new Random();
            do
            {
                var rad = ran.Next(100, 500);
                Thread.Sleep(rad / 2);
                Publish_Subscribe.PublishMsg(socket, rad.ToString());
            } while (true);
        }

        static void DoSubscriber(string u)
        {
            var socket = Publish_Subscribe.CreateSubscribe(u);
            string msg;
            var ran = new Random();
            do
            {
                var rad = ran.Next(100, 500);
                Thread.Sleep(rad / 2);
                Publish_Subscribe.UpdateMsg(socket, out msg);
            } while (true);
        }

        static void  DoServer(string u)
        {
            var socket = Request_Reply.CreateServer(u);
            Request_Reply.Listen(socket);
        }

        static void DoClient(string u)
        {
            var socket = Request_Reply.CreateClient(u);
            Request_Reply.SendAndWaitReply(socket);
        }

        static void DoServer_CircuitBreaker(string u)
        {
            var socket = Request_Reply.CreateServer(u);
            var breaker = new CircuitBreaker(()=> {
                do
                {
                    Request_Reply.ReceiveOnece(socket);
                } while (true);
            },null);

            breaker.Process();
        }


        static void DoClient_CircuitBreaker(string u)
        {
            var socket = Request_Reply.CreateClient(u);
            var breaker = new CircuitBreaker(() => {
                do
                {
                    Request_Reply.SendMsg(socket,"ok");
                } while (true);
            }, null);
            breaker.Process();
        }

        static string u1 = "tcp://127.0.0.1:10001";
        static string u2 = "tcp://127.0.0.1:10002";

        static void DoVentilator()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            var v = new Parallel_Pipeline.Ventilator(u1, u2);
            do
            {
                Console.WriteLine("Input Number:");
                uint t = 1;
                uint.TryParse(Console.ReadLine(),out t);
                t = t > 100 ? 100 : t;
                v.ProduceTask(t);
            } while (true);
        }

        static void DoTaskWorker()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            var t = new Parallel_Pipeline.TaskWorker(u1, u2);
            t.DoWork();
        }

        static void DoSink()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            var s = new Parallel_Pipeline.Sinker(u1);
            s.WaitResult();
        }
        
        static void StartNewProcess(string arg)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var name = AppDomain.CurrentDomain.FriendlyName;
            var p = new System.Diagnostics.ProcessStartInfo();
            p.FileName = System.IO.Path.Combine(path,name);
            p.Verb = "runas";
            p.Arguments = arg;
            p.CreateNoWindow = false;
            //p.RedirectStandardInput = true;
            p.UseShellExecute = false;
            p.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            p.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Process.Start(p);
            //s.StandardInput.Write("ssss");
        }
    }
}
