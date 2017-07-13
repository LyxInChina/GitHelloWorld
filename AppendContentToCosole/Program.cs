using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Web;
using Test;

namespace AppendContentToCosole
{
    public class QContent{
        public string FileName { get; set; }
        public string Content { get; set; }
    }
    /// <summary>
    /// 追加内容监控程序
    /// </summary>
    class Program
    {
        private static Dictionary<string, long> fileDic = new Dictionary<string, long>();
        private static ConcurrentQueue<QContent> logQueue = new ConcurrentQueue<QContent>();
        private static string paramPath = "param.txt";
        
        static void Main(string[] args)
        {
            //ThreadPool.QueueUserWorkItem((obj) => {
            //    while (true) {
            //        IOHelper.WriteLogToFile("测试写时间:" + (new Random().Next(20000)), @"E:\workspace\AppendContentToCosole\AppendContentToCosole\bin\Release\Log\default");
            //        IOHelper.WriteLogToFile("测试写时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), @"E:\workspace\AppendContentToCosole\AppendContentToCosole\bin\Release\Log\default");
            //        Thread.Sleep(1000);
            //    }
            //});
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine("开始初始化...");
            Console.WriteLine("当前目录工作目录[" + AppDomain.CurrentDomain.BaseDirectory + "]");
            string param = IOHelper.AppReadTxt(paramPath, Encoding.UTF8);
            Console.WriteLine("参数：" + param);
            string Filter = "";
            string Path = "";
            long initOffset = 0;//初始化偏移
            int initInterval = 100;//初始化间隔
            if (string.IsNullOrEmpty(param))
            {
                throw new Exception();
            }
            else
            {
                try
                {
                    string[] _a = param.Split(';');
                    if (_a.Length == 2)
                    {
                        Path = _a[0];
                        Filter = _a[1];
                    }
                    else if (_a.Length == 3)
                    {
                        Path = _a[0];
                        Filter = _a[1];
                        initOffset = long.Parse(_a[2]);
                    }
                    else if (_a.Length == 4)
                    {
                        Path = _a[0];
                        Filter = _a[1];
                        initOffset = long.Parse(_a[2]);
                        initInterval = int.Parse(_a[3]);
                    }
                    else
                    {
                        throw new Exception("参数配置错误，请修改param.txt文件");
                    }
                }
                catch(Exception e){
                    Console.WriteLine("error:"+e.Message);
                    Console.ReadKey();
                }
            }
            if (Directory.Exists(Path)==false) {
                Console.WriteLine("Error 目录[" + Path + "]不存在");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("目录文件记录初始化...");
            //先遍历一遍该目录下的文件，做一下初始化记录
            FileInfo[] files = IOHelper.GetFiles(Path, Filter);
            for (int i = 0;i<files.Length ; i++) {
                string filePath = files[i].FullName;
                if (initOffset == 0)
                {
                    fileDic.Add(filePath, files[i].Length);
                    Console.WriteLine("目录[" + filePath + "],偏移：" + files[i].Length);
                }
                else {
                    if (initOffset > files[i].Length)//防止超出
                    {
                        fileDic.Add(filePath, files[i].Length);
                    }
                    else {
                        fileDic.Add(filePath, initOffset);
                    }
                }
            }
            Console.WriteLine("目录[" + Path + "]下，有[" + Filter + "]文件" + files.Length + "个");
            Console.WriteLine("初始化成功！");
            //只监控该目录下的
            Console.WriteLine("开始监控[" + Path + "]下的所有[" + Filter + "]文件的追加内容");
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Path;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
            watcher.Filter = Filter;
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            watcher.Created += new FileSystemEventHandler(watcher_Created);
            watcher.Deleted += new FileSystemEventHandler(watcher_Deleted);
            watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
            watcher.EnableRaisingEvents = true;
            while (true)
            {
                Thread.Sleep(initInterval);
                while (logQueue.Count > 0)
                {
                    QContent result = new QContent();
                    logQueue.TryDequeue(out result);
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"文件："+ result.FileName + " 追加:" + result.Content);//输出追加的内容
                }
            }
        }
        /// <summary>
        /// 创建内存映射文件
        /// </summary>
        private static void AppendContentToCosole(long offset, string filePath,string name)
        {
            string line = string.Empty;
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (fs.CanSeek)
                    {
                        fs.Seek(offset, SeekOrigin.Begin);
                        fileDic[filePath] = fs.Length;
                        Debug.WriteLine("fs.Length:" + fs.Length);
                        if (offset < fs.Length)//防止期间文件删除后创建导致偏移变化
                        {
                            using (StreamReader sr = new StreamReader(fs))
                            {
                                string tmp = "";
                                while (string.IsNullOrEmpty(tmp = sr.ReadLine()) != true)
                                {
                                    Debug.WriteLine("追加:" + tmp);//输出追加的内容
                                    QContent _q = new QContent() { FileName=name,Content=tmp };
                                    logQueue.Enqueue(_q);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine("当前流不支持查找");
                    }
                }
            }
        }

        private static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Debug.WriteLine("Changed 文件路径：" + e.FullPath);
            long temp = 0;
            fileDic.TryGetValue(e.FullPath, out temp);
            if (temp == 0)
            {
                fileDic[e.FullPath] = 0;
            }
            AppendContentToCosole(fileDic[e.FullPath], e.FullPath, e.Name);
        }

        private static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            Debug.WriteLine("Create 文件路径：" + e.FullPath);
            if (File.Exists(e.FullPath))
            {
                FileInfo _info = new FileInfo(e.FullPath);
                fileDic.Add(e.FullPath, _info.Length);
                QContent _q = new QContent() { FileName = e.Name, Content = "新增加对文件：" + e.FullPath + "的监控(新创建了该文件)" };
                logQueue.Enqueue(_q);
            }
            else {
                Debug.WriteLine("Create :文件 " + e.FullPath + " 已不存在");
            }
        }

        private static void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Debug.WriteLine("Deleted 文件路径：" + e.FullPath);
            if (File.Exists(e.FullPath) == false)
            {
                fileDic.Remove(e.FullPath);
                QContent _q = new QContent() { FileName = e.Name, Content = "删除了加对文件：" + e.FullPath + "的监控(文件已经被删除)" };
                logQueue.Enqueue(_q);
            }
            else {
                Debug.WriteLine("Deleted :文件 " + e.FullPath + " 还存在");
            }
        }

        private static void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Debug.WriteLine("Renamed 从" + e.OldFullPath + "改成" + e.FullPath);
            if (File.Exists(e.FullPath))
            {
                fileDic.Remove(e.OldFullPath);
                QContent _q0 = new QContent() { FileName = e.OldName, Content = "删除了加对文件：" + e.FullPath + "的监控(文件已经被重命名)" };
                logQueue.Enqueue(_q0);
                FileInfo _info = new FileInfo(e.FullPath);
                fileDic.Add(e.FullPath, _info.Length);
                QContent _q1 = new QContent() { FileName = e.Name, Content = "新增加对文件：" + e.FullPath + "的监控(重命名了该文件)" };
                logQueue.Enqueue(_q1);
            }
            else
            {
                Debug.WriteLine("Renamed :文件 " + e.FullPath + " 不存在");
            }
        }
    }
}


