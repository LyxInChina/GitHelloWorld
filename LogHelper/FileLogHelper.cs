using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace ZQ.LogHelper
{
    public class FileLogHelper : ILogHelper
    {
        private static string _basePath = AppDomain.CurrentDomain.BaseDirectory;

        public void WriteLog(string filePath, string fileName, string context, Encoding encoding,
            EventLogEntryType level = EventLogEntryType.Warning)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var mode = File.Exists(Path.Combine(filePath, fileName))
                ? FileMode.Append
                : FileMode.Create;
            var file = new FileStream(Path.Combine(filePath, fileName), mode);
            var bys = encoding.GetBytes(context + "\n" + "ErrorLevel:" + level.ToString());
            file.Write(bys, 0, bys.Length);
            file.Flush();
            file.Close();
        }

        public void WriteLog(string context, Encoding encoding, EventLogEntryType level = EventLogEntryType.Warning)
        {
            WriteLog(_basePath, DateTime.Now.ToString("yyyMMdd") + ".log", context, encoding);
        }
        
        public void WriteLog(string context)
        {
            WriteLog(context, Encoding.UTF8);
        }

        public void WriteLog(string context, EventLogEntryType level)
        {
            WriteLog(context, Encoding.UTF8, level);
        }

        public void WriteLog(Exception exception)
        {
            WriteLog(exception, EventLogEntryType.Warning);
        }
        public void WriteLog(Exception exception, EventLogEntryType level)
        {
            var sf = new StringBuilder();
            sf.AppendLine("********" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "********");

            if (exception != null)
            {
                sf.AppendLine("Messsage:" + exception.Message);
                sf.AppendLine("StackTrace:" + exception.StackTrace);
                sf.AppendLine("Source:" + exception.Source);
                sf.AppendLine("HResult:" + exception.HResult);
            }
            else
            {
                sf.AppendLine("Null Exception");
            }
            WriteLog(sf.ToString(), Encoding.UTF8, level);
        }

    }
}
