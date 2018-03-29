using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace HelloWorld.Zip
{
    public class Zip
    {
        public static readonly string MICOFile =Environment.CurrentDirectory+ @"\Zip\ZIP.cio";

        static bool _IsExit = false;
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "AutoZip";
                ConsoleWin32Helper.Hidden();
                ConsoleWin32Helper.ShowNotifyIcon();
                ConsoleWin32Helper.DisableCloseButton(Console.Title);
                Thread threadMonitorInput = new Thread(MonitorInput);
                threadMonitorInput.Start();
                while (true)
                {
                    Application.DoEvents();
                    if (_IsExit)
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private static void MonitorInput()
        {
            AutoZipClass.MainWork();
            _IsExit = true;
            Thread.CurrentThread.Abort();
        }
    }
    public class ConsoleWin32Helper
    {
        private static bool _mShow = false;
        static ConsoleWin32Helper()
        {
            var icos = new Icon(Zip.MICOFile);
            _NotifyIcon.Icon = icos; 
            _NotifyIcon.Visible = false;
            _NotifyIcon.Text = "AutoZip";
            ContextMenu menu = new ContextMenu();
            MenuItem item = new MenuItem();
            item.Text = "AutoZip";
            item.Index = 0;
            menu.MenuItems.Add(item);
            _NotifyIcon.ContextMenu = menu;
            _NotifyIcon.MouseDoubleClick += new MouseEventHandler(_NotifyIcon_MouseDoubleClick);
        }
        static void _NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _mShow = !_mShow;
            if (_mShow)
            {
                Show();
            }
            else
            {
                Hidden();
            }
        }

        #region 禁用关闭按钮

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        static extern IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);
        [DllImport("User32.dll", EntryPoint = "ShowWindow")]
        public static extern bool ShowWindow(IntPtr hwind, int cmdShow);
        [DllImport("User32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hwind);

        ///<summary>
        /// 禁用关闭按钮
        ///</summary>
        ///<param name="consoleName">控制台名字</param>
        public static void DisableCloseButton(string title)//线程睡眠，确保closebtn中能够正常FindWindow，否则有时会Find失败。。 
        {
            Thread.Sleep(100);
            IntPtr windowHandle = FindWindow(null, title);
            IntPtr closeMenu = GetSystemMenu(windowHandle, IntPtr.Zero);
            uint SC_CLOSE = 0xF060;
            RemoveMenu(closeMenu, SC_CLOSE, 0x0);
        }
        public static bool IsExistsConsole(string title)
        {
            IntPtr windowHandle = FindWindow(null, title);
            if (windowHandle.Equals(IntPtr.Zero))
            {
                return false;
            }
            return true;
        }
        public static void Hidden()
        {
            IntPtr ParenthWnd = new IntPtr(0);
            IntPtr et = new IntPtr(0);
            ParenthWnd = FindWindow(null, "AutoZip");
            int normalState = 0;//窗口状态(隐藏)
            ShowWindow(ParenthWnd, normalState);
        }
        public static void Show()
        {
            IntPtr ParenthWnd = new IntPtr(0);
            IntPtr et = new IntPtr(0);
            ParenthWnd = FindWindow(null, "AutoZip");
            int normalState = 9;//窗口状态(隐藏)
            ShowWindow(ParenthWnd, normalState);
        }

        #endregion

        #region 托盘图标
        static NotifyIcon _NotifyIcon = new NotifyIcon();
        public static void ShowNotifyIcon()
        {
            _NotifyIcon.Visible = true;
            _NotifyIcon.ShowBalloonTip(50, "", "AutoZip Status", ToolTipIcon.None);
        }
        public static void HideNotifyIcon()
        {
            _NotifyIcon.Visible = false;
        }
        #endregion
    }
}