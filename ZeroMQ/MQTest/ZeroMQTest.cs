using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using AsyncIO;
using System.Runtime.Remoting.Contexts;
using ZeroMQ;
using System.Threading;

namespace MQTest
{
    public class ZeroMQTest
    {
        /// <summary>
        /// 获取当前ZeroMQ版本号
        /// </summary>
        /// <returns></returns>
        public static string GetZeroMQVersion()
        {
            int major, minor, patch;
            ZeroMQ.lib.zmq.version(out major,out minor,out patch);
            return string.Format("{0}.{1}.{2}", major, minor, patch);
        }
        

        public class Request_Reply
        {
            public static ZError error;
            public static ZSocket CreateServer(string url)
            {
                var context = new ZContext();
                var zsocker = new ZSocket(context, ZSocketType.REP);
                zsocker.Bind(url);
                return zsocker;
            }

            public static void Listen(ZSocket socket)
            {
                Console.WriteLine("Start Listen Msg ");
                while (true)
                {
                    using (var frame = socket.ReceiveFrame())
                    {
                        Console.WriteLine("receive request:{0}", frame.ReadString());
                        Thread.Sleep(300);
                        socket.SendFrame(new ZFrame("reply:" + System.Diagnostics.Process.GetCurrentProcess().Id));
                    }
                }
            }

            public static ZSocket CreateClient(string url)
            {
                var context = new ZContext();
                var zsocker = new ZSocket(context, ZSocketType.REQ);
                zsocker.Connect(url);
                return zsocker;
            }

            public static void SendAndWaitReply(ZSocket socket)
            {
                Console.WriteLine("Start Send Msg ");
                while (true)
                {
                    socket.SendFrame(new ZFrame("send:" + System.Diagnostics.Process.GetCurrentProcess().Id));
                    using (var reply = socket.ReceiveFrame())
                    {
                        Console.WriteLine("receive reply:" + reply.ReadString());
                        Thread.Sleep(500);
                    }
                }
            }
        }

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
            //public static ZSocket Create()
            //{

            //}
        }

    }
}
