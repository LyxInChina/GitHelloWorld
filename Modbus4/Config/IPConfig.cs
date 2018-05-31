using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModBus4
{
    /// <summary>
    /// IP配置类
    /// </summary>
    [Serializable]
    public class IPConfig : MConfig
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// IP模式：UPD/TCP
        /// </summary>
        public IPMode Mode { get; set; } = IPMode.Tcp;
        public IPConfig()
        {
        }
        public IPEndPoint CreateIPEndPoint()
        {
            return new IPEndPoint(IPAddress.Parse(Address), Port);
        }

    }
    public enum IPMode
    {
        Tcp=0,
        Udp=1,
    }
}
