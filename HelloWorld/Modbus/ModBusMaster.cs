using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus;
using System.IO.Ports;
using System.Net.Sockets;
using Modbus.Device;
using Modbus.Serial;
using Modbus.IO;

namespace ModBus4
{
    public static class ModBusMasterFactor
    {
        public static ModbusMaster CreateSerialMaster(SerialPort port, SerialModBusType type)
        {
            ModbusMaster master = null;
            if (port != null)
            {
                if (!port.IsOpen)
                    port.Open();
                switch (type)
                {
                    case SerialModBusType.Rtu:
                        master = ModbusSerialMaster.CreateRtu(port);
                        break;
                    case SerialModBusType.Ascii:
                        master = ModbusSerialMaster.CreateRtu(port);
                        break;
                    default:
                        break;
                }
            }
            return master;
        }

        public static ModbusMaster CreateTcpMaster(TcpClient client)
        {
            ModbusMaster master = null;
            if (client != null)
            {
                if (!client.Connected)
                {
                    client.Connect("127.0.0.1", 5250);
                }
                master = ModbusIpMaster.CreateIp(client);
            }
            return master;
        }

        public static ModbusSlave CreateSlave(byte id, SerialPortAdapter adapter, SerialModBusType type)
        {
            ModbusSlave slave = null;
            if (adapter != null)
            {
                switch (type)
                {
                    case SerialModBusType.Rtu:
                        slave = ModbusSerialSlave.CreateRtu(id, adapter);
                        break;
                    case SerialModBusType.Ascii:
                        slave = ModbusSerialSlave.CreateAscii(id, adapter);
                        break;
                    default:
                        break;
                }
            }
            return slave;
        }

        public static ModbusSlave CreateTcpSlave(byte id, TcpListener adapter)
        {
            ModbusSlave slave = null;
            if (adapter != null)
            {
                slave = ModbusTcpSlave.CreateTcp(id, adapter);
            }
            return slave;
        }

        public static ModbusSlave CreateUdpSlave(byte id,UdpClient adapter)
        {
            ModbusSlave slave = null;
            if (adapter != null)
            {
                slave = ModbusUdpSlave.CreateUdp(id, adapter);
            }
            return slave;
        }
        
        public static ModbusTransport Config(ModbusMaster master)
        {
            var ts = master.Transport;
            ts.ReadTimeout = 0;
            ts.Retries = 1;
            ts.WriteTimeout = 0;
            ts.WaitToRetryMilliseconds = 0;
            return null;
        }

        public static ModbusMaster CreateMaster(MConfig config, SerialModBusType type)
        {
            if (config != null)
            {
                switch (config.CType)
                {
                    case ConfigType.Serial:
                        {
                            var conf = config as SerialConfig;
                            if (conf != null)
                            {
                                var port = new SerialPort();
                                port.PortName = conf.PortName;
                                port.BaudRate = conf.BaudRate;
                                port.DataBits = conf.DataBits;
                                port.StopBits = conf.StopBits;
                                port.Parity = conf.Parity;
                                return CreateSerialMaster(port, type);
                            }
                        }
                        break;
                    case ConfigType.Ip:
                        {
                            var con = config as IPConfig;
                            if (con != null)
                            {
                                var client = new TcpClient(new System.Net.IPEndPoint(con.Address, con.Port));
                                return CreateTcpMaster(client);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return null;
        }
    }
    public enum SerialModBusType
    {
        Rtu=0,
        Ascii=1,
    }
}
