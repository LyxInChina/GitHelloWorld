using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ModBus4
{
    /// <summary>
    /// 串口配置
    /// </summary>
    [Serializable]
    public class SerialConfig : MConfig
    {
        /// <summary>
        /// 串口名称
        /// </summary>
        public string PortName;
        /// <summary>
        /// 比特率
        /// </summary>
        public int BaudRate;
        /// <summary>
        /// 奇偶校验检查协议
        /// </summary>
        public Parity Parity;
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits;
        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits;
        /// <summary>
        /// 串口模式
        /// </summary>
        public SerialModBusMode Mode = SerialModBusMode.Rtu;
        public SerialConfig()
        {
        }

        public SerialPort CreateSerialPort()
        {
            var port = new SerialPort();
            port.PortName = PortName ?? "COM1";
            port.BaudRate = BaudRate;
            port.Parity = Parity;
            port.DataBits = DataBits;
            port.StopBits = StopBits;
            return port;
        }
    }
    /// <summary>
    /// 串口模式
    /// </summary>
    public enum SerialModBusMode
    {
        Rtu = 0,
        Ascii = 1,
    }

}
