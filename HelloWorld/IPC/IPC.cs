using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Reflection;

namespace ThreadSync
{   

    /// <summary>
    /// 进程间通信 inter-process communication
    /// </summary>
    public class IPC_C
    {
        public static void Main(string[] args)
        {
            var nps =  IPCC.Build(eIPC.Client, typeof(NamedPipe));

        }
        public interface IPC: IDisposable
        {
            void Init(eIPC eIPC);
            void SendMsg(byte[] msg);
            string ReceiveMsg();
        }

        public enum eIPC
        {
            Client,
            Server,
        }

        public class IPCC
        {
            public readonly static string AssmGuid = "";
            static IPCC()
            {
                var tt = Assembly.GetExecutingAssembly();
                var g = tt.GetCustomAttribute(typeof(GuidAttribute));
                AssmGuid = ((System.Runtime.InteropServices.GuidAttribute)g).Value;
            }

            public static IPC Build(eIPC etype,Type type)
            {
                if (type == null)
                    return null;
                if (type.GetInterface(typeof(IPC).Name) == null)
                    return null;
                IPC obj = (IPC)AppDomain.CurrentDomain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null,
                    null, null, null);
                obj.Init(etype);
                return obj;
            }
        }

        /*IPC
         * 进程间通信分为LPC RPC
         * LPC本地进程通信：管道，共享内存，消息队列，信号量，信号，剪切板，邮槽
         * RPC远程进程通信：TCP/UDP，stream
         * 
         * 
         */

        /*管道 pipe
         分两种：
         匿名管道：
         命名管道:

        C#:System.IO.Pipes
        本质对Windows API的封装
        管道 
        命名管道 ：双工通信，可以跨网络
        匿名管道 ：半双工通信，即只有一端可以写另一端可以读，只能在同一机器上使用，不能跨网络
            
            

             */
        #region MQ-message queue 
        public class ZeroMq
        {

        }

        public class RabbitMq
        {

        }

        public class RocketMq
        {

        }
       
        #endregion
        #region Pipe

        public class AnonymousPipe:IPC
        {
            public volatile AnonymousPipeClientStream MAnClientPipe;
            public volatile AnonymousPipeServerStream MAnServerPipe;
            private PipeStream apipe = null;
            public void Init(eIPC eIPC)
            {
            }

            public void Dispose()
            {
                apipe?.Dispose();
            }

            public string ReceiveMsg()
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
        }

        public class NamedPipe:IPC
        {
            private readonly static string MServerName = "";
            private readonly static string MClientName = "";
            private PipeStream npine = null;
            private string  _mcname = "";
            private string _msname = "";
            private Mutex _mutexc;
            private Mutex _mutexs;
            private bool _ready = false;
            static NamedPipe()
            {
                MServerName = IPCC.AssmGuid + "~sn";
                MClientName = IPCC.AssmGuid + "~cn";
            }

            public NamedPipe()
            {
                _mcname = IPCC.AssmGuid + "~mcn";
                _msname = IPCC.AssmGuid + "~msn";
            }

            public void Init(eIPC eIPC)
            {
                switch (eIPC)
                {
                    case eIPC.Client:
                        {
                            bool _cn = false;
                            _mutexc = new Mutex(true, _mcname, out _cn);
                            if (_cn)
                            {
                                npine = new NamedPipeClientStream(MServerName,
                                    MClientName, PipeDirection.InOut,
                                    PipeOptions.Asynchronous,
                                    TokenImpersonationLevel.None);
                                _ready = true;
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                        break;
                    case eIPC.Server:
                        {
                            bool cn = false;
                            _mutexs = new Mutex(true, _msname, out cn);
                            if (cn)
                            {
                                npine = new NamedPipeServerStream(MServerName,
                                 PipeDirection.InOut, maxNumberOfServerInstances: 1,
                                 PipeTransmissionMode.Message,
                                 PipeOptions.Asynchronous);
                                _ready = true;
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                        break;
                    default:
                        break;
                }

            }

            public void Dispose()
            {
                _ready = false;
                npine?.Dispose();
                _mutexc?.Close();
                _mutexs?.Close();
            }

            public void SendMsg(byte[] msg)
            {
                try
                {
                    if (_ready&&!npine.IsConnected)
                    {
                        ((NamedPipeClientStream)npine).Connect(500);
                    }
                    var p = new StreamWriter(npine);
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
                    if (_ready&&!npine.IsConnected)
                    {
                        ((NamedPipeServerStream)npine).WaitForConnection();
                    }
                    return new StreamReader(npine).ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "error";
                }
            }


        }

        #endregion

        #region windows消息

        /// <summary>
        /// Windows消息
        /// </summary>
        public class Wm_CopyData: IPC
        {
            public void Init(eIPC eIPC)
            {
                throw new NotImplementedException();
            }
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

        #endregion

        #region 内存映射

        /// <summary>
        /// 内存映射
        /// </summary>
        public class MemoryMappedFile:IPC
        {
            public void Init(eIPC eIPC)
            {
                throw new NotImplementedException();
            }
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

        #endregion

        #region 共享内存

        /// <summary>
        /// 共享内存
        /// </summary>
        public class SharedMemory
        {
            
        }
        #endregion


    }
}
