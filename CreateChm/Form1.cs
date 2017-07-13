/*
 * 项目名：C#生成简易CHM文件
 * 创建时间：20100928
 * 创建人：Alexis
 * 作者主页：http://www.cnblogs.com/alexis/
 * 联系方式：shuifengxu@gmail.com
 * 说明：转载请保留作者说明
 */ 


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace CreateChm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string startPath = "";
        
        string hhcFile = @"C:\Program Files (x86)\HTML Help Workshop\hhc.exe";//hhc.exe文件位置，windows自带的，一般是这个路径
        public string _defaultTopic = "NewTopic.html";
        StreamWriter streamWriter;

        private bool Compile()
        {
            string _chmFile = startPath + @"\test.chm";//chm文件存储路径
            Process helpCompileProcess = new Process();  //创建新的进程，用Process启动HHC.EXE来Compile一个CHM文件
            try
            {
                //判断文件是否存在并不被占用
                try
                {
                    string path = _chmFile;  //chm生成路径
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch
                {
                    throw new Exception("文件被打开！");
                }

                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.FileName = hhcFile;  //调入HHC.EXE文件 
                processStartInfo.Arguments = "\"" + GetPathToProjectFile() + "\"";//获取空的HHP文件
                processStartInfo.UseShellExecute = false;
                helpCompileProcess.StartInfo = processStartInfo;
                helpCompileProcess.Start();
                helpCompileProcess.WaitForExit(); //组件无限期地等待关联进程退出

                if (helpCompileProcess.ExitCode == 0)
                {
                    MessageBox.Show(new Exception().Message);
                    return false;
                }
            }
            finally
            {
                helpCompileProcess.Close();
            }
            return true;
        }

        public void OpenHhp(string title)
        {
            FileStream fs = new FileStream(GetPathToProjectFile(), FileMode.Create); //创建hhp文件
            streamWriter = new System.IO.StreamWriter(fs, System.Text.Encoding.GetEncoding("GB18030"));
            streamWriter.WriteLine("[OPTIONS]");
            streamWriter.WriteLine("Title=" + title);
            streamWriter.WriteLine("Compatibility=1.1 or later");
            streamWriter.WriteLine("Compiled file=" + GetCompiledHtmlFilename());  //chm文件名
            streamWriter.WriteLine("Contents file=" + GetContentsHtmlFilename());  //hhc文件名
            streamWriter.WriteLine("Index file=test.hhk");
            streamWriter.WriteLine("Default topic=" + _defaultTopic);  //默认页
            streamWriter.WriteLine("Display compile progress=NO"); //是否显示编译过程
            streamWriter.WriteLine("Language=0x804 中文(中国)");  //chm文件语言
            streamWriter.WriteLine("Default Window=Main");
            streamWriter.WriteLine();
            streamWriter.WriteLine("[WINDOWS]");
            streamWriter.WriteLine("Main=test\",\"test.hhc\",\"test.hhk\",,,,,,,0x20,180,0x104E, [80,60,720,540],0x0,0x0,,,,,0");//这里最重要了，一般默认即可
            streamWriter.WriteLine();
            streamWriter.WriteLine("[FILES]");
            streamWriter.WriteLine("NewTopic.html");
            streamWriter.WriteLine();
            streamWriter.Close();
        }

        private void OpenHhc()
        {
            FileStream fs = new FileStream(GetContentsHtmlFilename(), FileMode.Create); //创建hhp文件
            streamWriter = new System.IO.StreamWriter(fs, System.Text.Encoding.GetEncoding("GB18030"));
            streamWriter.WriteLine("<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML//EN\">");
            streamWriter.WriteLine("<HTML>");
            streamWriter.WriteLine("<HEAD>");
            streamWriter.WriteLine("<meta name=\"GENERATOR\" content=\"Microsoft&reg; HTML Help Workshop 4.1\">");
            streamWriter.WriteLine("<!-- Sitemap 1.0 -->");
            streamWriter.WriteLine("</HEAD>");
            streamWriter.WriteLine("<BODY>");
            streamWriter.WriteLine("<OBJECT type=\"text/site properties\">");
            streamWriter.WriteLine("<param name=\"Window Styles\" value=\"0x237\">");
            streamWriter.WriteLine("</OBJECT>");
            streamWriter.WriteLine("<UL>");
            streamWriter.WriteLine("<LI> <OBJECT type=\"text/sitemap\">");
            streamWriter.WriteLine("<param name=\"Name\" value=\"NewTopic\">");
            streamWriter.WriteLine("</OBJECT>");
            streamWriter.WriteLine("<UL>");
            streamWriter.WriteLine("<LI> <OBJECT type=\"text/sitemap\">");
            streamWriter.WriteLine("<param name=\"Name\" value=\"NewTopic\">");
            streamWriter.WriteLine("<param name=\"Local\" value=\"NewTopic.html\">");
            streamWriter.WriteLine("</OBJECT>");
            streamWriter.WriteLine("</UL>");
            streamWriter.WriteLine("</UL>");
            streamWriter.WriteLine("</BODY>");
            streamWriter.WriteLine("</HTML>");
            streamWriter.WriteLine();
            streamWriter.Close();
        }

        private void OpenHhk()
        {
            FileStream fs = new FileStream(startPath + @"\test.hhk", FileMode.Create); //创建hhp文件
            streamWriter = new System.IO.StreamWriter(fs, System.Text.Encoding.GetEncoding("GB18030"));
            streamWriter.WriteLine("<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML//EN\">");
            streamWriter.WriteLine("<HTML>");
            streamWriter.WriteLine("<HEAD>");
            streamWriter.WriteLine("<meta name=\"GENERATOR\" content=\"Microsoft&reg; HTML Help Workshop 4.1\">");
            streamWriter.WriteLine("<!-- Sitemap 1.0 -->");
            streamWriter.WriteLine("</HEAD>");
            streamWriter.WriteLine("<BODY>");
            streamWriter.WriteLine("<UL>");
            streamWriter.WriteLine("	<LI> <OBJECT type=\"text/sitemap\">");
            streamWriter.WriteLine("		<param name=\"Name\" value=\"NewTopic\">");
            streamWriter.WriteLine("<param name=\"Local\" value=\"NewTopic.html\">");
            streamWriter.WriteLine("</OBJECT>");
            streamWriter.WriteLine("</UL>");
            streamWriter.WriteLine("</BODY>");
            streamWriter.WriteLine("</HTML>");
            streamWriter.WriteLine();
            streamWriter.Close();
        }

        /// <summary>
        /// 获取hhp文件路径
        /// </summary>
        /// <returns></returns>
        private string GetPathToProjectFile()
        {
            return startPath+@"\test.hhp";
        }

        /// <summary>
        /// 获取含有列表的hhc文件
        /// </summary>
        /// <returns></returns>
        private string GetContentsHtmlFilename()
        {
            return "test.hhc";
            //return startPath+@"\test.hhc";
        }

        /// <summary>
        /// 设置编译后的文件名
        /// </summary>
        /// <returns></returns>
        private string GetCompiledHtmlFilename()
        {
            //return startPath+@"\test.chm";
            return "test.chm";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startPath = Application.StartupPath;//起始路径

            OpenHhp("test.chm");//打开hhp文件
            OpenHhc();//打开hhc文件
            OpenHhk();//打开hhk文件
            if(Compile())//编译成chm文件
            {
                MessageBox.Show("Success，Pls Check!");
            }
            var list = new List<string>();
            var result = from x in list.AsParallel().WithDegreeOfParallelism(50)
                         select Proc(x);
        }

        private string Proc(string x)
        {
            return x;
        }
    }
}
