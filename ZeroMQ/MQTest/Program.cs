using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTest;
using System.Threading;
using static MQTest.ZeroMQTest;

namespace MQTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ZeroMQ VersionInfo:" + ZeroMQTest.GetZeroMQVersion());
            Console.WriteLine("选择类型:server?client?publisher?subscriber?");
            string u = "tcp://127.0.0.1:8989";
            string us = "tcp://127.0.0.1:9000";
            var res = Console.ReadLine();
            switch(res.ToLower())
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
                default:
                    break;
            }
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
    }
}
