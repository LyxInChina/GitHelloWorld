﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSync
{
    class Program
    {
        #region 处理键盘命令

//        /// <summary>
//        /// 处理键盘命令
//        /// </summary>
//        /// <param name="msg"></param>
//        /// <param name="keyData"></param>
//        /// <returns></returns>
//        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
//        {
//            int WM_KEYDOWN = 256;
//            int WM_SYSKEYDOWN = 260;
//            if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
//            {
//                switch (keyData)
//                {
//                    case Keys.Escape:
//                        this.Close(); //esc关闭窗体
//                        break;
//                }
//            }
//            return false;
//        }

        #endregion

        static void Main(string[] args)
        {
            //Thread th1 = new Thread(C.EntryMethod);
            //th1.Start(0);
            //Thread th2 = new Thread(C.EntryMethod);
            //th2.Start(1);

            //int a = 3;
            //C.InitEx(a);
            //for (int i = 0; i < a; i++)
            //{
            //    Thread th = new Thread(C.EntryMethodEx);
            //    th.Start(i);
            //}
            //while (true)
            //{
            //    var str = Console.ReadLine();
            //    if (!string.IsNullOrEmpty(str)&&str.Equals("exit"))
            //    {
            //        C.Exit = false;
            //        C.ExitEx = false;
            //        break;
            //    }
            //}

            var process = new ProcessStartInfo();
            process.FileName = "F://Test//FileAssociation.exe";
            process.Verb = "runas";
            Process.Start(process);
            Console.WriteLine("wait exit...");
            do
            {
                if (Console.ReadLine().ToLower()=="exit")
                {
                    break;
                }
                Thread.Sleep(50);
            } while (true);
        }
    }

    public static class C
    {
        public static bool[] Flag = new bool[2];
        public static int Turn;
        public static Random random= new Random(7);
        public static bool Exit = true;

        public static bool[] FlagEx;
        public static int TurnEx;
        public static Random randomEx=new Random();
        public static bool ExitEx = true;
        public static int Max;

        public static AutoResetEvent PausEvent = new AutoResetEvent(false);
        public static AutoResetEvent StopEvent = new AutoResetEvent(false);
        public static AutoResetEvent SleepEvent = new AutoResetEvent(false);
        public static ManualResetEvent MManualResetEvent = new ManualResetEvent(false);

        public static AutoResetEvent PausEventEx = new AutoResetEvent(false);
        public static AutoResetEvent StopEventEx = new AutoResetEvent(false);
        public static AutoResetEvent SleepEventEx = new AutoResetEvent(false);

        public static bool MLock = false;
        public static void EntryMethod(object pas)
        {
            int pa = (int) pas;
            if (pa<0||pa>1)
            {
                return;
            }
            Method(pa, 1 - pa);
        }
        public static void Method(int i,int j)
        {
            do
            {
                //Entry Section
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "entry Entry Section");
                Flag[i] = true;
                Turn = j;
                while (Flag[j] && Turn == j)
                {
                    //Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "Wating the work");
                }
                //Critical Section
                Work();
                //Exit Section
                Flag[i]= false;
                //Remainder Section
                var t = random.Next(50, 500);
                //Thread.Sleep(t);
                SleepEvent.WaitOne(t);
            } while (!StopEvent.WaitOne(1));
        }

        public static void InitEx(int max)
        {
            Max = max;
            FlagEx= new bool[Max];
        }

        public static void EntryMethodEx(object pas)
        {
            int pa = (int) pas;
            if (pa<0||pa>=Max)
            {
                return;
            }
            MethodEx(pa);
        }

        public static void MethodEx(int i)
        {
            do
            {
                //Entry Section 进入区
                FlagEx[i] = true;
                TurnEx = i + 1 >=Max ? 0 : i + 1;//0 到 Max-1
                while (Wait(i))
                {

                }
                //WaitEx(i);
                //Critical Section 临界区
                // 互斥 mutual exclusion 
                // 前进 progress
                // 有限等待 bounded waiting
                Work();
                //Exit Section 退出区
                FlagEx[i] = false;

                //Remainder Section 剩余区
                var t = random.Next(50, 500);
                SleepEventEx.WaitOne(t);
            } while (!StopEventEx.WaitOne(1));
        }

        public static bool Wait(int i)
        {
            int t = i + 1 >= Max ? 0 : i + 1;
            bool temp= FlagEx[t] && TurnEx == t;
            for (int j = 0; j < Max; j++)
            {
                if (j != i && j != t)
                {
                    temp = temp || FlagEx[j] && TurnEx == j;
                }
            }
            return temp;
        }

        public static void WaitEx(int i)
        {
            for (int j = 0; j < Max; j++)
            {
                if (j!=i)
                {
                    while (FlagEx[j] && TurnEx == j) ;
                }
            }
        }

        public static void Work()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "_____Begain do the Work....");
            SleepEvent.WaitOne(3000);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "_____Had done the Work.");
        }

        public static unsafe bool TestAndSet(bool* target)
        {
            bool res = *target;
            *target = true;
            return res;
        }
        
        public static unsafe void Method()
        {
            do
            {
                fixed (bool* temp = &MLock)
                {
                    while (TestAndSet(temp));
                }
                //Critial Section
                MLock = false;
                //Remainder section

            } while (true);
        }

        public static unsafe void Swap(bool* a, bool* b)
        {
            var t = *a;
            *a = *b;
            *b = t;
        }

        public static unsafe void MethodSwap()
        {
            
        }

        public static bool[] WaitBools;
        public static bool Lock;

        [Obfuscation(Feature = "rename")]
        public static void MethodTestAndSet(int i)
        {
            do
            {
                WaitBools[i] = true;
                var key = true;
                while (WaitBools[i] && key)
                {
                    //key = Interlocked.CompareExchange(ref Lock,true, true);
                }
            } while (ExitEx);
        }

        public static Semaphore MSemaphore = new Semaphore(9,9,"Semaphore_01");

        public static SemaphoreSlim MSemaphoreSlim = new SemaphoreSlim(9,9);

        public static Semaphore MMutex= new Semaphore(0,1);

        public static Semaphore MEmtpty=new Semaphore(9,9);

        public static Semaphore MFull = new Semaphore(0,9);

        public static SemaphoreSlim MMutexEx = new SemaphoreSlim(0,1);
        public static SemaphoreSlim MEmptyEx = new SemaphoreSlim(9,9);
        public static SemaphoreSlim MFullEx = new SemaphoreSlim(0,9);
        public static SpinLock MSpinLock = new SpinLock(false);

        public static void Producter()
        {
            do
            {
                MEmtpty.WaitOne();
                MMutex.WaitOne();
                //make a Production
                MMutex.Release();
                MEmtpty.Release();

            } while (Exit);

            do
            {
                MFull.WaitOne();
                MMutex.WaitOne();
                //use a Production
                MMutex.Release();
                MFull.Release();

            } while (Exit);

        }

        public static void ProductEx()
        {
            do
            {
                MEmptyEx.Wait();
                MMutexEx.Wait();
                //make a production
                MMutexEx.Release();
                MFullEx.Release();

            } while (Exit);


            do
            {
                MFullEx.Wait();
                MMutexEx.Wait();
                //use a production
                MMutexEx.Release();
                MEmptyEx.Release();

            } while (Exit);
        }

        public static void WaitSemaphore(int t)
        {
            while (t <= 0) ;
            t--;
        }

        public static void SignalSemaphore(int t)
        {
            t++;
        }

        public static void SemaphoreMethod()
        {
            //Entry Section
            MSemaphore.WaitOne(40, true);

            //Critical Section
            //TODO: Critical Action

            //Exit Section
            MSemaphore.Release();

            //Remainder Section
        }

        public static void SemaphoreMethodEx()
        {
            //Entry Section
            MSemaphoreSlim.Wait();

            //Critical Section
            //TODO: Critical Action

            //Exit Section
            MSemaphoreSlim.Release();

            //Remainder Section

        }

        public static bool MLockToken = false;

        public static void SpinLockMethod()
        {
            
            MSpinLock.Enter(ref MLockToken);
            MSpinLock.TryEnter(40,ref MLockToken);
            MSpinLock.Exit();
        }

        public static SemaphoreSlim MMutexRW,MWrt = new SemaphoreSlim(0,1);
        public static int ReaderCount = 0;

        /// <summary>
        /// 读写问题
        /// </summary>
        public static void RWProblom()
        {
            //Writer
            do
            {
                MWrt.Wait();
                //Writing
                MWrt.Release();

            } while (ExitEx);
            //reader 
            do
            {
                MMutexRW.Wait();
                ReaderCount++;
                if (ReaderCount==1)
                {
                    MWrt.Wait();
                }
                MMutexRW.Release();
                //Reading
                MMutexRW.Wait();
                ReaderCount--;
                if (ReaderCount==0)
                {
                    MWrt.Release();
                }
                MMutexRW.Release();

            } while (ExitEx);

        }

        /// <summary>
        /// Dekker 互斥算法
        /// 
        /// </summary>
        public class Dekker
        {
            /// <summary>
            /// 共享数据项 Flag turn 
            /// </summary>
            private bool[] Flag=new bool[2]{false,false};
            private int turn;
            private void ThreadD(int i,int j)
            {
                do
                {
                    //Try enter cirtical section
                    Flag[i] = true;
                    while (Flag[j])
                    {
                        if (turn==j)
                        {
                            Flag[i] = false;
                            while (turn==j)
                            {
                                
                            }
                            Flag[i] = true;
                        }
                    }
                    //Critical section

                    turn = j;
                    Flag[i] = false;

                    //Remainder section
                    //TOOD: wroking
                    Thread.Sleep(2000);
                } while (true);
            }

        }

    }

    /*
     * 
     同步机制包括临界区（critical section），信号量（simphore），互斥量（mutex），管程（monitor）
    关于AutoResetEvent与ManualResetEvent
    ResetEvent线程通知事件
    可用于多线程同步 线程控制

    AutoResetEvent与ManualResetEvent的区别
    相同点
    1. 都从EventWaitHandle基类派生;
    2.Set方法将信号置为发送状态，Reset方法将信号置为不发送状态,WaitOne等待信号的发送。
    可以通过构造函数的参数值来决定其初始状态，若为true则非阻塞状态，为false为阻塞状态。
    如果某个线程调用WaitOne方法,则当信号处于发送状态时,该线程会得到信号, 继续向下执行
    不同点
    针对WaitOne()方法调用后的状态十分改变
    AutoResetEvent自动改变（置为不发送状态），则此时只有一个线程会继续执行；
    Manual则不会改变状态（保持原样即发送状态），则此时其他同步线程都可以执行；
    AutoResetEvent.WaitOne()每次只允许一个线程进入,当某个线程得到信号后,AutoResetEvent会自动又将信号置为不发送状态,
    则其他调用WaitOne的线程只有继续等待.也就是说,AutoResetEvent一次只唤醒一个线程;
    而ManualResetEvent则可以唤醒多个线程,因为当某个线程调用了ManualResetEvent.Set()方法后,
    其他调用WaitOne的线程获得信号得以继续执行,而ManualResetEvent不会自动将信号置为不发送。
    也就是说,除非手工调用了ManualResetEvent.Reset()方法,
    则ManualResetEvent将一直保持有信号状态,ManualResetEvent也就可以同时唤醒多个线程继续执行。

    关于Simphore与Simphoeresilm
    信号量与轻量级的信号量
    Simphore 控制对资源池的访问
    请求资源 WaitOne() 减少一次资源可用计数 若计数为0 则阻塞当前线程等待其他线程释放该资源
    释放资源Release() 增加一次可用计数


    关于Mutex
    
     */

}
