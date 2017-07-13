using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FolderWatcher
{
    class Program
    {
        static System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher();
        static void Main(string[] args)
        {
            Ana(args);
            string path = Console.ReadLine();


            watcher.Changed += Watcher_Changed;
            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Renamed += Watcher_Renamed;
            watcher.Error += Watcher_Error;

            //do
            //{
            //    string cmd = Console.ReadLine();

            //} while (true);

        }

        private static string ExcuteCmd(string cmd)
        {
            if (!string.IsNullOrEmpty(cmd))
            {
                string[] s = cmd.Split(' ');

            }
            return "error";
        }


        private static void Watcher_Error(object sender, ErrorEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(e.GetException());
        }

        private static void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(e.ChangeType);
        }

        private static void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(e.ChangeType);
        }

        private static void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(e.ChangeType);
        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(e.ChangeType);
        }

        static void Ana(string[] args)
        {
            if (args.Length > 0)
            {
                if (Directory.Exists(args[0]))
                {
                    watcher.Path = args[0];
                }
            }
        }
    }
       

}
