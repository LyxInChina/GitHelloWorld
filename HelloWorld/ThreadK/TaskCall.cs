using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace HelloWorld.ThreadK
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

        private static void LoadAllStubAssemblies()
        {
            Console.WriteLine("work begin");
            Thread.Sleep(1000);
            Console.WriteLine("work done");
        }

        public static void DoWorkAsync<T>(Action<T> action)
        {

        }



    }

    /// <summary>
    /// 处理器信息
    /// </summary>
    public static class ProcessorInfo
    {
        #region 获取硬件信息 CPU

        public static uint GetCPUProcessorNumber()
        {
            System_Info info;
            if (Environment.Is64BitProcess)
            {
                GetNativeSystemInfo(out info);
            }
            else
            {
                GetSystemInfo(out info);
            }
            return info.dwNumberOfProcessors;
        }

        /// <summary>
        /// x86下调用
        /// </summary>
        /// <param name="info"></param>
        [DllImport("Kernel32.dll")]
        private static extern void GetSystemInfo(out System_Info info);

        /// <summary>
        /// wow64下，64位应用程序调用
        /// </summary>
        [DllImport("kernel32.dll")]
        private static extern void GetNativeSystemInfo(out System_Info info);

        [DllImport("Kernel32.dll")]
        public static extern UInt32 GetLastError();

        [StructLayout(LayoutKind.Sequential)]
        public struct System_Info
        {
            /// <summary>
            /// 已过时为保持兼容性留下的结构体
            /// </summary>
            [Obsolete("已过时为保持兼容性留下的结构体")]
            public OemId oemId;
            /// <summary>
            /// 
            /// </summary>
            public UInt32 dwPageSize;
            /// <summary>
            /// 应用程序或者DLL最小地址指针
            /// </summary>
            public IntPtr lpMinimumApplicationAddress;
            /// <summary>
            /// 应用程序或者DLL最大地址指针
            /// </summary>
            public IntPtr lpMaximumApplicationAddress;
            /// <summary>
            /// 系统内处理器配置的标记
            /// </summary>
            public IntPtr dwActiveProcessorMask;
            /// <summary>
            /// 处理器数量
            /// </summary>
            public UInt32 dwNumberOfProcessors;
            /// <summary>
            /// 处理器类型
            /// </summary>
            [Obsolete("已过时，为兼容性保留，")]
            public UInt32 dwProcessorType;
            /// <summary>
            /// 虚拟内存分配的颗粒度
            /// </summary>
            public UInt32 dwAllocationGranularity;
            /// <summary>
            /// 架构相关的处理器等级
            /// </summary>
            public UInt16 wProcessorLevel;
            /// <summary>
            /// 处理器修订号
            /// </summary>
            public UInt16 wProcessorRevision;
        }

        /// <summary>
        /// 已过时为保持兼容性留下的结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct OemId
        {
            /// <summary>
            /// 安装系统处理器架构
            /// </summary>
            UInt16 wProcessArchitecture;
            /// <summary>
            /// 将来预留字段
            /// </summary>
            UInt16 wReserved;
        }

        public enum ProcessArchitecture
        {
            /// <summary>
            /// x64 AMD or intel
            /// </summary>
            PROCESSOR_ARCHITECTURE_AMD64 = 9,
            /// <summary>
            /// ARM
            /// </summary>
            PROCESSOR_ARCHITECTURE_ARM = 5,
            /// <summary>
            /// ARM64
            /// </summary>
            PROCESSOR_ARCHITECTURE_ARM64 = 12,
            PROCESSOR_ARCHITECTURE_IA64 = 6,
            PROCESSOR_ARCHITECTURE_INTEL = 0,
            PROCESSOR_ARCHITECTURE_UNKNOWN = 0xffff,
        }
        #endregion
    }


    public interface AsyncCall
    {
        void RunAsync(Action action, Action callBack);
        void RunAsync(Action action);
        void RunAsync(Action action, object param);
    }

}
