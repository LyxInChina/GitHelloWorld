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
    public class IPConfig:MConfig
    {
        public IPAddress Address;
        public int Port;
        public IPConfig()
        {
            CType = ConfigType.Ip;
        }

    }
}
