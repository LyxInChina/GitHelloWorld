using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace ThreadSync
{
    public class ProcessCommunication
    {
        public interface IPCommunication: IDisposable
        {
            void SendMsg(string msg);
            void SendMsg(byte[] msg);
            string ReceiveMsg();
        }

        /// <summary>
        /// 管道
        /// </summary>
        public class PipePC:IPCommunication
        {
            /*
             
             System.IO.Pipes
             本质对Windows API的封装

             管道 
             命名管道 ：双工通信，可以跨网络
             匿名管道 ：半双工通信，即只有一端可以写另一端可以读，只能在同一机器上使用，不能跨网络

            */


            public volatile NamedPipeClientStream MClientPipe;
            public volatile NamedPipeServerStream MServerPipe;

            public volatile AnonymousPipeClientStream MAnClientPipe;
            public volatile AnonymousPipeServerStream MAnServerPipe;

            public int TimeOut { get; set; } = 500;

            public PipePC(string clientPipeName, string serverPipeName)
                :this(clientPipeName,".", serverPipeName)
            {

            }

            public PipePC(string clientPipeName,string serverName,string serverPipeName)
            {
                MClientPipe = new NamedPipeClientStream(".",clientPipeName, PipeDirection.InOut, PipeOptions.Asynchronous, TokenImpersonationLevel.None);
                MServerPipe = new NamedPipeServerStream(serverPipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            }

            public void SendMsg(Stream msg)
            {
                byte[] buff = new byte[msg.Length];
                msg.Read(buff, 0, buff.Length);
                SendMsg(buff);
            }

            public void Dispose()
            {
                MClientPipe?.Close();
                MClientPipe?.Dispose();
                MClientPipe = null;
                MServerPipe?.Close();
                MServerPipe?.Dispose();
                MServerPipe = null;
            }

            public void SendMsg(string msg)
            {
                var buff = System.Text.Encoding.UTF8.GetBytes(msg);
                SendMsg(buff);
            }

            public void SendMsg(byte[] msg)
            {
                try
                {
                    if (!MClientPipe.IsConnected)
                    {
                        MClientPipe.Connect(500);
                    }
                    var p = new StreamWriter(MClientPipe);
                    p.WriteLine(System.Text.Encoding.UTF8.GetString(msg));
                    p.Flush();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            public string ReceiveMsg()
            {
                try
                {
                    if (!MServerPipe.IsConnected)
                    {
                        MServerPipe.WaitForConnection();
                    }
                    return new StreamReader(MServerPipe).ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "error";
                }
            }
        }

        /// <summary>
        /// socket
        /// </summary>
        public class Remoting
        {
            /*
             基于socket

             */
        }

        /// <summary>
        /// Windows消息
        /// </summary>
        public class Wm_CopyData: IPCommunication
        {

            [DllImport("User32.dll", EntryPoint = "SendMessage")]
            private static extern int SendMessage(
                     int hWnd,                    // handle to destination window
                     int Msg,                     // message
                     int wParam,                  // first message parameter
                     ref CopyDataStruct lParam    // second message parameter
                     );
            [DllImport("User32.dll", EntryPoint = "FindWindow")]
            private static extern int FindWindow(string lpClassName, string lpWindowName);

            [StructLayout(LayoutKind.Sequential)]
            public struct CopyDataStruct
            {
                public IntPtr dwData;
                public int cbData;
                public IntPtr lpData;
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public void SendMsg(string msg)
            {
                throw new NotImplementedException();
            }

            public void SendMsg(byte[] msg)
            {
                throw new NotImplementedException();
            }

            public string ReceiveMsg()
            {
                throw new NotImplementedException();
            }

        }

        /// <summary>
        /// 内存映射
        /// </summary>
        public class MemoryMappedFile:IPCommunication
        {
            public void Dispose()
            {
                throw new NotImplementedException();
            }
            public void SendMsg(string msg)
            {

            }
            public void SendMsg(byte[] msg)
            {
                throw new NotImplementedException();
            }

            public string ReceiveMsg()
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 共享内存
        /// </summary>
        public class SharedMemory
        {
            
        }


    }
}
