using System;
using System.IO;
using System.Text;

namespace Test
{
    /// <summary>
    /// 常用IO操作类
    /// </summary>
    public class IOHelper
    {

        /// <summary>  
        /// 返回指定目录下目录信息  
        /// </summary>  
        /// <param name="strDirectory">路径</param>  
        /// <returns></returns>  
        public static DirectoryInfo[] GetDirectory(string strDirectory)
        {
            if (string.IsNullOrEmpty(strDirectory)==false) {
                return new DirectoryInfo(strDirectory).GetDirectories();
            }
            return new DirectoryInfo[] { };
        }
        /// <summary>  
        /// 返回指定目录下所有文件信息  
        /// </summary>  
        /// <param name="strDirectory">路径</param>  
        /// <returns></returns>  
        public static FileInfo[] GetFiles(string strDirectory)
        {
            if (string.IsNullOrEmpty(strDirectory) == false)
            {
                return new DirectoryInfo(strDirectory).GetFiles();
            }
            return new FileInfo[] { };
        }
        /// <summary>
        ///  返回指定目录下过滤文件信息  
        /// </summary>
        /// <param name="strDirectory">目录地址</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static FileInfo[] GetFiles(string strDirectory, string filter)
        {
            if (string.IsNullOrEmpty(strDirectory) == false)
            {
                return new DirectoryInfo(strDirectory).GetFiles(filter, SearchOption.TopDirectoryOnly);
            }
            return new FileInfo[] { };
        }

        /// <summary>
        /// 控制台数据错误
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteConsole(Exception ex) {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            Console.WriteLine("Data:" + ex.Data + Environment.NewLine
            + " InnerException:" + ex.InnerException + Environment.NewLine
            + " Message:" + ex.Message + Environment.NewLine
            + " Source:" + ex.Source + Environment.NewLine
            + " StackTrace:" + ex.StackTrace + Environment.NewLine
            + " TargetSite:" + ex.TargetSite);
        }
        /// <summary>
        /// 错误记录
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLog(Exception ex)
        {
            //WriteConsole(ex);
            WriteLog("Data:" + ex.Data + Environment.NewLine
                + " InnerException:" + ex.InnerException+ Environment.NewLine
                + " Message:" + ex.Message+ Environment.NewLine
                + " Source:" + ex.Source+ Environment.NewLine
                + " StackTrace:" + ex.StackTrace+ Environment.NewLine
                + " TargetSite:" + ex.TargetSite);
        }
        /// <summary>
        /// 写log
        /// </summary>
        /// <param name="InfoStr"></param>
        public static void WriteLog(string InfoStr)
        {
            WriteLog(InfoStr, Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Log");
        }
        /// <summary>
        /// 写log(自动时间log)
        /// </summary>
        /// <param name="InfoStr">内容</param>
        /// <param name="FilePath">目录地址</param>
        public static void WriteLog(string InfoStr, string DirPath)
        {
            FileStream stream = null;
            System.IO.StreamWriter writer = null;
            try
            {
                string logPath = DirPath;
                if (Directory.Exists(logPath) == false)
                {
                    Directory.CreateDirectory(logPath);
                }
                string filepath = logPath + Path.DirectorySeparatorChar + "log_" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";
                if (File.Exists(logPath) == false)
                {
                    stream = new FileStream(logPath, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
                }
                else
                {
                    stream = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }
                writer = new System.IO.StreamWriter(stream);
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                writer.WriteLine(InfoStr);
                writer.WriteLine("");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        /// <summary>
        /// 写log到指定文件（不存在就创建）
        /// </summary>
        /// <param name="InfoStr">内容</param>
        /// <param name="FilePath">文件地址</param>
        public static void WriteLogToFile(string InfoStr, string FilePath)
        {
            FileStream stream = null;
            System.IO.StreamWriter writer = null;
            try
            {
                string logPath = FilePath;
                if (File.Exists(logPath) == false)
                {
                    stream = new FileStream(logPath, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
                }
                else {
                    stream = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }
                
                writer = new System.IO.StreamWriter(stream);
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                writer.WriteLine(InfoStr);
                writer.WriteLine("");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        public static void AsyncWriteLog(byte[] datagram, string FilePath,AsyncCallback callback,int numBytes)
        {
            FileStream stream = null;
            try
            {
                string logPath = FilePath;
                if (File.Exists(logPath) == false)
                {
                    stream = new FileStream(logPath, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
                }
                else
                {
                    stream = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }

                if (stream.CanWrite)
                {

                    stream.BeginWrite(datagram, 0, numBytes, callback, "AsyncWriteLog_" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                else {
                    throw new Exception("文件无法写入，文件或只读！");
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
        
        /// <summary>
        /// 文本转义（方便讲文本转换成C#变量代码）
        /// 例子 " 转化成 string str="\"";
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToEscape(string str,string m_var) {
            /*
                "           \"
                \           \\
            */
            str = str.Trim();
            str = str.Replace("\\", "\\\\");
            str = str.Replace("\"", "\\\"");
            return "string " + m_var + "=\"" + str + "\";";
        }
        /// <summary>
        /// 读取Appliction目录下的文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string AppReadTxt(string filePath,Encoding enc)
        {
            FileStream stream = null;
            System.IO.StreamReader reader = null;
            try
            {
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                string path = appPath + filePath;
                if (File.Exists(path) == false)
                {
                    stream = new FileStream(path, FileMode.CreateNew, FileAccess.Read, FileShare.ReadWrite);
                }
                else
                {
                    stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                reader = new System.IO.StreamReader(stream,enc);
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                WriteLog(e);
                return "";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
        /// <summary>
        /// 读取Appliction目录下的文件（UTF-8）
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string AppReadTxt(string filePath)
        {
            return AppReadTxt(filePath, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 查看目录信息(或者返回目录信息)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="IfJson"></param>
        /// <returns></returns>
        public static string LookDirectoryInfo(string path,bool IfJson)
        {
            string _path = AppDomain.CurrentDomain.BaseDirectory + path;
            if (IfJson == false)
            {
                Console.WriteLine(_path);
                foreach (var d in new DirectoryInfo(_path).GetDirectories())
                {
                    Console.WriteLine("目录:" + d.Name + " | " + d.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                foreach (var f in new DirectoryInfo(_path).GetFiles())
                {
                    Console.WriteLine("文件:" + f.Name + " | " + f.CreationTime.ToString("yyyy-MM-dd hh:mm:ss") + " | " + (f.Length + 1023) / 1024 + "kb");
                }
                return "";
            }
            else {
                Console.WriteLine(_path);
                string json = "{\"currentPath\":\"" + Uri.EscapeDataString(_path) + "\",\"info\":[";
                string temp01 = "";
                foreach (var d in new DirectoryInfo(_path).GetDirectories())
                {
                    if (string.IsNullOrEmpty(temp01))
                    {

                        temp01 = temp01 + "{\"name\":\"" + d.Name + "\",\"creationTime\":\"" + d.CreationTime.ToString("yyyy-MM-dd hh:mm:ss") + "\",\"isFile\":\"false\",\"size\":\"0kb\"}";
                    }
                    else {
                        temp01 = temp01 + ",{\"name\":\"" + d.Name + "\",\"creationTime\":\"" + d.CreationTime.ToString("yyyy-MM-dd hh:mm:ss") + "\",\"isFile\":\"false\",\"size\":\"0kb\"}";
                    }
                    
                }
                string temp02 = "";
                foreach (var f in new DirectoryInfo(_path).GetFiles())
                {
                    if (string.IsNullOrEmpty(temp02))
                    {
                        temp02 =temp02+ "{\"name\":\"" + f.Name + "\",\"creationTime\":\"" + f.CreationTime.ToString("yyyy-MM-dd hh:mm:ss") + "\",\"isFile\":\"true\",\"size\":\"" + (f.Length + 1023) / 1024 + "kb" + "\"}";
                    }
                    else {
                        temp02 = temp02 + ",{\"name\":\"" + f.Name + "\",\"creationTime\":\"" + f.CreationTime.ToString("yyyy-MM-dd hh:mm:ss") + "\",\"isFile\":\"true\",\"size\":\"" + (f.Length + 1023) / 1024 + "kb" + "\"}";
                    }
                }
                if (string.IsNullOrEmpty(temp01)) {
                    json = json + temp02 + "]}";
                }else if (string.IsNullOrEmpty(temp02)){
                    json = json + temp01 + "]}";
                }else{
                    json = json + temp01 + "," + temp02 + "]}";
                }
                /*
                 格式
                 { "currentPath":"","info":[{"name":"","creationTime":"","isFile":"","size":""},{"name":"","creationTime":"","isFile":"","size":""}]}
                 */
                return json;
            }
        }
    }
}
