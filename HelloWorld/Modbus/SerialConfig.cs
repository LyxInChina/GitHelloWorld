using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ModBus4
{
    public abstract class MConfig
    {
       public ConfigType CType { get;protected set; }
    }

    [Serializable]
    public class SerialConfig:MConfig
    {
        public string PortName;
        public int BaudRate;
        public Parity Parity;
        public int DataBits;
        public StopBits StopBits;
        public SerialConfig()
        {
            CType = ConfigType.Serial;

        }
    }

    public enum ConfigType
    {
        Serial =0,
        Ip=1,
    }
}
