using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModBus4
{
    [Serializable]
    public class IPConfig : MConfig
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public IPAddress Address;
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port;
        public IPMode Mode = IPMode.Tcp;
        public IPConfig()
        {
        }
        public IPEndPoint CreateIPEndPoint()
        {
            return new IPEndPoint(Address, Port);
        }

    }
    public enum IPMode
    {
        Tcp=0,
        Udp=1,
    }
}
