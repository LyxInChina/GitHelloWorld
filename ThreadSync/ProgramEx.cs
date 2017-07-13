using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSync
{
    class ProgramEx
    {
        static ProcessCommunication.IPCommunication IPC;
        static void Main(string[] args)
        {
            var s = Console.ReadLine();
            switch (s)
            {
                case "c":
                    IPC = new ProcessCommunication.PipePC("111", "222");
                    do
                    {
                        var t = Console.ReadLine();
                        IPC.SendMsg(t);
                        Console.WriteLine("send msg :"+t);
                        if (t=="exit")
                        {
                            break;
                        }
                    } while (true);
                    break;
                case "s":
                    {
                        IPC = new ProcessCommunication.PipePC("222", "111");
                        do
                        {
                            var ss = IPC.ReceiveMsg();
                            Console.WriteLine("receive msg:"+ss);
                            if (ss=="exits")
                            {
                                break;
                            }
                            Thread.Sleep(500);
                        } while (true);
                    }break;
                default:
                    IPC = new ProcessCommunication.PipePC("222", "111");
                    break;
            }

        }
    }
}
