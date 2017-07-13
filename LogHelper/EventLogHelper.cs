using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZQ.LogHelper
{
    public class EventLogHelper : ILogHelper
    {

        public FileLogHelper LogHelper=  new FileLogHelper();

        public static string SourceName
        {
            get
            {
                return AppDomain.CurrentDomain.FriendlyName;
            }
        }

        public static string LogName
        {
            get { return AppDomain.CurrentDomain.FriendlyName; }
        }

        private static void CreateSource(string sourceName, string logName)
        {
            if (!EventLog.SourceExists(sourceName,Environment.MachineName))
            {

                var name = EventLog.Exists(logName);
                if (name)
                {
                    EventLog.CreateEventSource(new EventSourceCreationData(sourceName, logName));
                }
            }
        }

        /// <summary>
        /// 写入日志信息
        /// </summary>
        /// <param name="sourceName">事件源名</param>
        /// <param name="context">日志内容</param>
        /// <param name="type">事件类型</param>
        /// <param name="eventid">事件ID</param>
        /// <param name="logName">日志文件名</param>
        public void WriteLog(string sourceName, string context, EventLogEntryType type, int eventid = 0,
            string logName = null)
        {
            try
            {
                var names = EventLog.GetEventLogs();

                //if (!EventLog.SourceExists(sourceName, Environment.MachineName))
                {
                    CreateSource(sourceName, logName ?? LogName);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex, EventLogEntryType.Error);
            }
            try
            {
                using (var log = new EventLog())
                {
                    log.Source = sourceName;
                    log.MachineName = Environment.MachineName;
                    log.WriteEntry(context, type, eventid);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex, EventLogEntryType.Information);
            }

        }

        public void WriteLog(string context, EventLogEntryType type)
        {
            WriteLog(SourceName, context, type);
        }

        public void WriteLog(string context)
        {
            WriteLog(SourceName,context, EventLogEntryType.Information);
        }

        public void WriteLog(Exception exception)
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
            WriteLog(exception.Source, sf.ToString(), EventLogEntryType.Warning, 0);
        }

        public void WriteLog(Exception exception, EventLogEntryType type)
        {
            var sf = new StringBuilder();
            sf.AppendLine("********" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "********");
            sf.AppendLine("Messsage:" + exception.Message);
            sf.AppendLine("StackTrace:" + exception.StackTrace);
            sf.AppendLine("Source:" + exception.Source);
            sf.AppendLine("HResult:" + exception.HResult);
            WriteLog(exception.Source, sf.ToString(), type, 0);
        }

    }
}
