using System;
using System.Windows;
using Microsoft.Win32;
using System.IO;

namespace FileAssociation
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 设置文件关联 
        /// </summary> 
        /// <param name="p_Filename">程序的名称</param> 
        /// <param name="p_FileTypeName">扩展名</param> 
        public static void SaveReg(string Filename, string FileTypeName)
        {
            RegistryKey RegKey = Registry.ClassesRoot.OpenSubKey("", true);//打开注册表 

            RegistryKey VKey = RegKey.OpenSubKey(FileTypeName, true);
            if (VKey != null)  //判断注册表项是否存在
                VKey.SetValue("", "Exec");
            else
            {
                RegKey.CreateSubKey(FileTypeName);
                VKey = RegKey.OpenSubKey(FileTypeName, true);
                VKey.SetValue("", "Exec");
            }

            VKey = RegKey.OpenSubKey("Exec", true);
            if (VKey != null)
                RegKey.DeleteSubKeyTree("Exec");  //如果不等于空 则删除注册表

            RegKey.CreateSubKey("Exec");
            VKey = RegKey.OpenSubKey("Exec", true);
            VKey.CreateSubKey("shell");
            VKey = VKey.OpenSubKey("shell", true);//写入必须路径 
            VKey.CreateSubKey("open");
            VKey = VKey.OpenSubKey("open", true);
            VKey.CreateSubKey("command");
            VKey = VKey.OpenSubKey("command", true);
            string _PathString = "\"" + Filename + "\" \"%1\"";
            VKey.SetValue("", _PathString); //写入数据 


        }
        /// <summary> 
        /// 删除恢复文件关联 
        /// </summary> 
        /// <param name="p_FileTypeName">扩展名</param> 
        public static void ResReg(string FileTypeName)
        {
            RegistryKey Regkey = Registry.ClassesRoot.OpenSubKey("", true);
            RegistryKey Vkey = Regkey.OpenSubKey(FileTypeName, true);
            if (Vkey != null)
                Vkey.SetValue("", "txtfile"); //恢复到默认的值
            Regkey.DeleteSubKeyTree("Exec");

        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string p_Filename = this.GetType().Assembly.Location;//获取该程序的完整路径
            SaveReg(p_Filename, ".txt");
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            ResReg(".txt");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string PathFileName = Environment.CommandLine; //获取启动行命令
            string[] para = PathFileName.Split('\"');  //进行分割并判断是否通过关联的文件打开程序
            if (para.Length > 3)
            {
                ReadText(para[3]);
            } 
        }
        private void ReadText(string PathFileName)
        {
            StreamReader sr = new StreamReader(PathFileName);
            string readtxt = sr.ReadToEnd();
            textbox.Text = readtxt;
            sr.Close();
        }

    }
}
