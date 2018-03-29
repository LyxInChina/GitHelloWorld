using System;
using ZeroMQ;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MQTest
{
    public class ZeroMQTest
    {
        /*
         * Hadoop Zookeeper
         * AMQP
         * 
         请求回复模式
         lockstep
         客户端必须zmq_send()然后zmq_recv()
         服务端必须zmq_recv()然后zmq_send()
         服务器重启后客户端不会自动重新连接(一定几率)

        ZeroMQ 字符串结尾不含终止符
        ZContext对象在进程开始和终止时进行创建和释放
        退出清理
        三种对象context socket message

        */

        /// <summary>
        /// 获取当前ZeroMQ版本号
        /// </summary>
        /// <returns></returns>
        public static string GetZeroMQVersion()
        {
            ZeroMQ.lib.zmq.version(out int major, out int minor, out int patch);
            return string.Format("{0}.{1}.{2}", major, minor, patch);
        }

        /// <summary>
        /// 请求回复模式
        /// </summary>
        public class Request_Reply
        {
            private static ZError error;

            public static ZSocket CreateServer(string url)
            {
                var context = new ZContext();
                var zsocker = new ZSocket(context, ZSocketType.REP);
                zsocker.Bind(url);
                return zsocker;
            }

            /// <summary>
            /// 同一个服务端可以帮到多个IP
            /// </summary>
            /// <param name="url"></param>
            /// <returns></returns>
            public static ZSocket CreateServer(string[] url)
            {
                var context = new ZContext();
                var zsocker = new ZSocket(context, ZSocketType.REP);
                for (int i = 0; i < url.Length; i++)
                {
                    zsocker.Bind(url[i]);
                }
                return zsocker;
            }

            public static ZSocket CreateClient(string url)
            {
                var context = new ZContext();
                var zsocker = new ZSocket(context, ZSocketType.REQ);
                zsocker.Connect(url);
                return zsocker;
            }

            public static void ReceiveOnece(ZSocket socket)
            {
                ZError error=null;
                using (var frame = socket.ReceiveFrame(out error))
                {
                    if (error != null)
                    {
                        throw new Exception(error.Text);
                    }
                    Console.WriteLine("Server::Get-request::{0}", frame.ReadString());
                    Thread.Sleep(new Random().Next(100, 300));
                    var msg = new Random().Next(1000, 9999).ToString();
                    socket.SendFrame(new ZFrame("NONO::" +msg));
                }
            }

            public static void Listen(ZSocket socket)
            {
                Console.WriteLine("Start Listen Msg ");
                while (true)
                {
                    using (var frame = socket.ReceiveFrame())
                    {
                        Console.WriteLine("Re:{0}", frame.ReadString());
                        Thread.Sleep(300);
                        var msg = Environment.TickCount;
                        socket.SendFrame(new ZFrame("I'MServer:" + msg));
                    }
                }
            }

            public static void SendMsg(ZSocket socket,string msg)
            {
                ZError error = null;
                socket.SendFrame(new ZFrame("OOOO:" + msg), ZSocketFlags.DontWait,out error);
                using (var reply = socket.ReceiveFrame(ZSocketFlags.DontWait,out error))
                {
                    Console.WriteLine("Client::get reply:" + reply?.ReadString());
                    Thread.Sleep(new Random().Next(500,900));
                }
            }

            public static void SendAndWaitReply(ZSocket socket)
            {
                Console.WriteLine("Start Send Msg ");
                while (true)
                {
                    socket.SendFrame(new ZFrame("ImClient:" + Environment.TickCount));
                    using (var reply = socket.ReceiveFrame())
                    {
                        Console.WriteLine("receive reply:" + reply.ReadString());
                        Thread.Sleep(500);
                    }
                }
            }
        }

        /// <summary>
        /// 发布者订阅者模式
        /// </summary>
        public class Publish_Subscribe
        {
            public static ZSocket CreatePublisher(string url)
            {
                var context = new ZContext();
                var socket = new ZSocket(context, ZSocketType.PUB);
                socket.Bind(url);
                return socket;
            }

            public static ZSocket CreateSubscribe(string url)
            {
                var context = new ZContext();
                var socket = new ZSocket(context, ZSocketType.SUB);
                socket.Connect(url);
                return socket;
            }

            public static void PublishMsg(ZSocket socket,string msg)
            {
                using (var sendframe = new ZFrame(msg))
                {
                    socket.Send(sendframe);
                    Console.WriteLine("Publish Msg:"+msg);
                }
            }

            public static void UpdateMsg(ZSocket socket,out string msg)
            {
                socket.Subscribe("0000");
                using (var frame = socket.ReceiveFrame())
                {
                    msg = frame.ReadString();
                    Console.WriteLine("Update Msg:" + msg);
                }
            }
        }

        public class Parallel_Pipeline
        {
            public class Ventilator:IDisposable
            {
                private ZContext context;
                private ZSocket sender;
                private ZSocket sink;
                private string CUrl;
                private string VUrl;
                public int TaskNum = 10;
                public Ventilator(string s,string c)
                {
                    CUrl = s;
                    VUrl = c;
                    Init();
                }

                public void Init()
                {
                    context = new ZContext();
                    sender = new ZSocket(context, ZSocketType.PUSH);
                    sink = new ZSocket(context, ZSocketType.PUSH);
                    sender.Bind(VUrl);
                    sink.Connect(CUrl);
                }
                public void ProduceTask(uint num=10)
                {
                    num = num > 100 ? 100 : num;
                    num = num <= 0 ? 1 : num;
                    //发出任务分配信号
                    var sig = BitConverter.GetBytes(num);
                    sink.Send(sig, 0, sig.Length);
                    //发出任务信息
                    for (int i = 0; i < num; i++)
                    {
                        var action = BitConverter.GetBytes(i);
                        sender.Send(action, 0, action.Length);
                    }
                }

                public void Dispose()
                {
                    sender.Close();
                    sink.Close();
                    sender.Dispose();
                    sink.Dispose();
                    context.Shutdown();
                    context.Dispose();
                }
            }

            public class TaskWorker:IDisposable
            {
                private bool loop = true;
                private ZContext context;
                private ZSocket receiver;
                private ZSocket sink;
                public string SUrl;
                public string TUrl;
                
                public TaskWorker(string s,string t)
                {
                    SUrl = s;
                    TUrl = t;
                    Init();
                }

                public void Init()
                {
                    context = new ZContext();
                    receiver = new ZSocket(context, ZSocketType.PULL);
                    sink = new ZSocket(context, ZSocketType.PUSH);
                    receiver.Connect(TUrl);
                    sink.Connect(SUrl);
                }

                public void DoWork()
                {
                    while (loop)
                    {
                        var res = new byte[4];
                        //获取任务数字
                        receiver.ReceiveBytes(res, 0, res.Length);
                        System.Threading.Tasks.Task.Run(() =>
                        {
                            var num = BitConverter.ToInt32(res, 0);
                            Console.WriteLine("Get Task::{0}", num);
                            //TODO:do work
                            Thread.Sleep(new Random().Next(100,800));
                            //通知sink该任务完成
                            sink.Send(res, 0,res.Length);
                        });
                    }
                }
                public void Dispose()
                {
                    loop = false;
                    receiver.Close();
                    sink.Close();
                    receiver.Dispose();
                    sink.Dispose();
                    context.Shutdown();
                    context.Dispose();
                }
            }

            public class Sinker:IDisposable
            {
                private ZContext context;
                private ZSocket sink;
                private bool loop=true;
                private string SUrl;
                public Sinker(string c)
                {
                    SUrl = c;
                    Init();
                }

                public void Init()
                {
                    context = new ZContext();
                    sink = new ZSocket(context, ZSocketType.PULL);
                    sink.Bind(SUrl);
                }

                public void WaitResult()
                {
                    while (loop)
                    {
                        var res = new byte[4];
                        sink.ReceiveBytes(res, 0, res.Length);
                        var r = BitConverter.ToInt32(res, 0);
                        if (r < 0)
                        {
                            Console.WriteLine("New Task:{0}", -r);
                        }
                        else
                        {
                            Console.WriteLine("Task:{0} Done", r);
                        }
                    }
                }
                public void Dispose()
                {
                    loop = false;
                    sink.Close();
                    sink.Dispose();
                    context.Shutdown();
                    context.Dispose();
                }
            }

            private Ventilator ventilator;
            private TaskWorker taskWorker;
            private Sinker sinker;
            private IList<TaskWorker> taskWorkers = new List<TaskWorker>();
            public string SinkerUrl="tcp://127.0.0.1:10001";
            public string VentiltorUrl="tcp://127.0.0.1:10002";

            public Parallel_Pipeline(string surl,string vurl)
            {
                if(!string.IsNullOrEmpty(surl))
                {
                    SinkerUrl = surl;
                }
                if (!string.IsNullOrEmpty(vurl))
                {
                    VentiltorUrl = vurl;
                }
            }

            public void Init()
            {
                ventilator = new Ventilator(SinkerUrl, VentiltorUrl);
                taskWorker = new TaskWorker(SinkerUrl, VentiltorUrl);
                sinker = new Sinker(SinkerUrl);
            }
                
        }


    }
}
