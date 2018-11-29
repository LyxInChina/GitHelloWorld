using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HelloWorld
{
    public class Nofity : IDisposable
    {
        public NotifyIcon MNotifyIcon = new NotifyIcon();
        public ContextMenu Menu { get; set; }

        public int MBalloonTipTimeOut { get; set; } = 50;
        public string MBalloonTipTitle { get; set; } = "OK";
        public string MBalloonTipText { get; set; } = "NO";


        public Nofity()
        {
            Icon icon;
            Helper.GetSysIcon(out icon);
            MNotifyIcon.Icon = icon;
            MNotifyIcon.MouseClick += MNotifyIcon_MouseClick;
            Menu = new ContextMenu();
            MenuItem windowMenu = new MenuItem("Window");
            MenuItem shake = new MenuItem("Shake");
            shake.Click += WindowMenu_Click;
            MNotifyIcon.Visible = true;
            Menu.MenuItems.Add(windowMenu);
            Menu.MenuItems.Add(shake);
            MNotifyIcon.ContextMenu = Menu;
        }

        private void WindowMenu_Click(object sender, EventArgs e)
        {
            Shake();
        }

        private void Shake()
        {
            var f = Environment.CurrentDirectory + @"\msg.wav";
            PlaySoundUtility.PlaySound(f);
            int t = 4;
            while (t > 0)
            {
                t--;
                var s = t % 2 == 0;
                MNotifyIcon.Icon = s ? SystemIcons.Question : SystemIcons.Error;
                System.Threading.Thread.Sleep(300);
            }
        }

        private void MNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MNotifyIcon.ShowBalloonTip(MBalloonTipTimeOut, MBalloonTipTitle, MBalloonTipText, ToolTipIcon.None);
            }
            else if (e.Button == MouseButtons.Right)
            {
                //Menu?.Show();
            }
        }

        public void ShowNotifyIcon()
        {
            MNotifyIcon.ShowBalloonTip(MBalloonTipTimeOut, MBalloonTipTitle, MBalloonTipText, ToolTipIcon.None);
        }


        public void Dispose()
        {
            MNotifyIcon.Dispose();
        }

    }

    public static class Helper
    {
        #region 禁用关闭按钮

        /// <summary>
        /// 查找窗体句柄
        /// 根据指定的类名和窗口名仅查找顶级的窗体，不区分大小写
        /// </summary>
        /// <param name="lpClassName">类名或者为空</param>
        /// <param name="lpWindowName">窗体标题，若标题为null，则匹配所有窗体</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 获取系统菜单
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="bRevert"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        /// <summary>
        /// 删除菜单项
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="uPosition"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        static extern IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="hwind"></param>
        /// <param name="cmdShow"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "ShowWindow")]
        public static extern bool ShowWindow(IntPtr hwind, int cmdShow);

        /// <summary>
        /// 设置窗口置前
        /// </summary>
        /// <param name="hwind"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hwind);


        public static void GetSysIcon(out Icon icon)
        {
            icon = SystemIcons.Question;
        }
        

        ///<summary>
        /// 禁用关闭按钮
        ///</summary>
        ///<param name="consoleName">控制台名字</param>
        public static void DisableCloseButton(string title)
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

        private static void OpWindow(string title, int cmdshow)
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

    /// <summary>
    /// 播放声音单元
    /// </summary>
    public static class PlaySoundUtility
    {
        /// <summary>
        /// 播放标识
        /// </summary>
        [Flags]
        private enum PlaySoundFlags : int
        {
            /// <summary>
            /// 同步播放
            /// </summary>
            SND_SYNC = 0x0000,    /* play synchronously (default) */ //同步

            /// <summary>
            /// 异步播放
            /// </summary>
            SND_ASYNC = 0x0001,    /* play asynchronously */ //异步
            SND_NODEFAULT = 0x0002,    /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004,    /* pszSound points to a memory file */
            SND_LOOP = 0x0008,    /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010,    /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004    /* name is resource name or atom */
        }

        /// <summary>
        /// 播放声音WAV文件
        /// </summary>
        /// <param name="szSound"></param>
        /// <param name="hMod"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        private static extern bool PlaySound(string szSound, IntPtr hMod, PlaySoundFlags flags);

        /// <summary>
        /// 播放WAV文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool PlaySound(string fileName)
        {
            if (System.IO.File.Exists(fileName) && System.IO.Path.GetExtension(fileName).ToLower().Equals(".wav"))
            {
                return PlaySound(fileName, IntPtr.Zero, PlaySoundFlags.SND_ASYNC);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 播放WAV文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool PlayWavSound(string fileName)
        {
            if (System.IO.File.Exists(fileName) && System.IO.Path.GetExtension(fileName).ToLower().Equals(".wav"))
            {
                try
                {
                    System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(fileName);
                    sndPlayer.Play();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
