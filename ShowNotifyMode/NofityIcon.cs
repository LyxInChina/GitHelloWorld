using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ShowNotifyMode
{
    public class NofityIcon:IDisposable
    {
        public NotifyIcon MNotifyIcon = new NotifyIcon();
        public MenuStrip Menu { get; set; }
        public System.Drawing.Icon MICon { get; set; }
        public int MBalloonTipTimeOut { get; set; } = 50;
        public string MBalloonTipTitle { get; set; } = "";
        public string MBalloonTipText { get; set; } = "";

        private bool _show = false;

        public NofityIcon(Icon icon)
        {
            MNotifyIcon.Icon = icon;
            MNotifyIcon.MouseClick += MNotifyIcon_MouseClick;
        }

        private void MNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _show = !_show;
                if (_show)
                {
                    ShowNotifyIcon();
                }
                else
                {
                    HideNotifyIcon();
                }
            }
            else if(e.Button== MouseButtons.Right)
            {
                Menu?.Show();
            }
        }

        public void ShowNotifyIcon()
        {
            MNotifyIcon.Visible = true;
            MNotifyIcon.ShowBalloonTip(MBalloonTipTimeOut, MBalloonTipTitle, MBalloonTipText, ToolTipIcon.None);
        }

        public void HideNotifyIcon()
        {
            MNotifyIcon.Visible = false;
        }

        public void Dispose()
        {
            MICon.Dispose();
            MNotifyIcon.Dispose();
        }

    }

    public static class Helper
    {
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
            Thread.Sleep(20);
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

        private static void OpWindow(string title,int cmdshow)
        {
            IntPtr ParenthWnd = new IntPtr(0);
            IntPtr et = new IntPtr(0);
            ParenthWnd = FindWindow(null, title);
            ShowWindow(ParenthWnd, cmdshow);
        }

        public static void Hidden(string title)
        {
            OpWindow(title, 0);

            //IntPtr ParenthWnd = new IntPtr(0);
            //IntPtr et = new IntPtr(0);
            //ParenthWnd = FindWindow(null, title);
            //int normalState = 0;//窗口状态(隐藏)
            //ShowWindow(ParenthWnd, normalState);
        }
        public static void Show(string title)
        {
            OpWindow(title, 0);

            //IntPtr ParenthWnd = new IntPtr(0);
            //IntPtr et = new IntPtr(0);
            //ParenthWnd = FindWindow(null, title);
            //int normalState = 9;//窗口状态(隐藏)
            //ShowWindow(ParenthWnd, normalState);
        }

        #endregion


    }

}
