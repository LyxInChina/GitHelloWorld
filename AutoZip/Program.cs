using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using ZQ.LogHelper;

namespace AutoZip
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new EventLogHelper();
            var helperEx = new FileLogHelper();
            try
            {
                var directory = Properties.Settings.Default.filePath;
                var time = new Stopwatch();
                if (Directory.Exists(directory))
                {
                    var dir = new DirectoryInfo(directory);
                    var timeFormat = Properties.Settings.Default.fileNameFormat;
                    var fileName = dir.Name+"-" + DateTime.Now.ToString(timeFormat);
                    var name = CheckFileName(fileName);
                    if (dir.Parent != null)
                    {
                        Console.WriteLine("Begin Zip...");
                        time.Start();
                        ZipHelper.ZipDirectory(directory, dir.Parent.FullName, name);
                        time.Stop();
                        Console.WriteLine("Zip done!");
                        var stringb = new StringBuilder();
                        stringb.AppendLine("********" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "********");
                        stringb.AppendLine(dir.FullName + "压缩完成！(用时：" + time.ElapsedMilliseconds/1000 + "s)");
                        stringb.AppendLine("压缩后文件名为：" + name);
                        stringb.AppendLine("文件路径：" + dir.Parent.FullName);
                        Console.WriteLine("Begin Record Event...");
                        helperEx.WriteLog(stringb.ToString(), EventLogEntryType.Information);
                        helper.WriteLog(stringb.ToString(), EventLogEntryType.Information);
                        Console.WriteLine("Record done.");
                        Console.WriteLine("AutoZip Exit.");
                    }
                    else
                    {
                        helperEx.WriteLog(dir.FullName + "父目录为Null", EventLogEntryType.Warning);
                        helper.WriteLog(dir.FullName + "父目录为Null", EventLogEntryType.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                helper.WriteLog(ex, EventLogEntryType.Error);
            }
        }

        public static string CheckFileName(string fileName,int index=1)
        {
            if (File.Exists(fileName+".zip"))
            {
                if (File.Exists(fileName+"("+index+")" + ".zip"))
                {
                    index++;
                    CheckFileName(fileName, index);
                }
                else
                {
                    fileName = fileName + "(" + index + ")";
                }
            }
            return fileName;
        }

        
    }
}
