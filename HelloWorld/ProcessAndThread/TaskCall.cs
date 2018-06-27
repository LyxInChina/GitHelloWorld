using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HelloWorld.ProcessAndThread
{
    public class TaskCall
    {
        private static bool _IsLoadStubAssembly = false;
        private static object _loadStubAssemblytLock = new object();

        /// <summary>
        /// 加载其他stub类库
        /// </summary>
        public static void LoadStubAssembly()
        {
            if (!_IsLoadStubAssembly)
            {
                if (Monitor.TryEnter(_loadStubAssemblytLock))
                {
                    if (!_IsLoadStubAssembly)
                    {
                        LoadAllStubAssemblies();
                        _IsLoadStubAssembly = true;
                    }
                    Monitor.Exit(_loadStubAssemblytLock);
                }
            }
        }

        private static Task _loadStubTask;
        private static Thread _loadStubThread = new Thread(Do_Work);
        private static CancellationTokenSource tokenSource = new CancellationTokenSource();

        private static Action _action;

        private static void Do_Work()
        {
            try
            {
                _action?.Invoke();
            }
            catch (Exception ex)
            {
                
            }
        }

        public static void BeginLoadStubAssembly()
        {
            if (_loadStubThread != null)
            {
                tokenSource.Cancel(false);
                _loadStubTask.Dispose();
                GC.ReRegisterForFinalize(_loadStubTask);
                _loadStubTask = null;
            }
            _loadStubTask = new Task(() =>
            {
                LoadStubAssembly();
            }, tokenSource.Token);
            if(_loadStubTask.Status == TaskStatus.WaitingToRun|| _loadStubTask.Status== TaskStatus.Created)
            {
                _loadStubTask.Start();
            }
        }

        public static void EndLoadStubAssembly()
        {
            if (!_loadStubTask.IsCompleted)
            {
                _loadStubTask.Wait();
            }
            if (_loadStubTask.IsFaulted)
            {
                Trace.WriteLine("异步加载stub异常，{0}", _loadStubTask.Exception.InnerException.Message);
            }
        }

        public static void BeginLoad()
        {

        }

        private static void LoadAllStubAssemblies()
        {
            Console.WriteLine("work begin");
            Thread.Sleep(1000);
            Console.WriteLine("work done");
        }

    }
}
