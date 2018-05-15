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
        public static ModbusMaster CreateSerialMaster(SerialPort port, SerialModBusMode type)
        {
            ModbusMaster master = null;
            if (port != null)
            {
                if (!port.IsOpen)
                    port.Open();
                switch (type)
                {
                    case SerialModBusMode.Rtu:
                        master = ModbusSerialMaster.CreateRtu(port);
                        break;
                    case SerialModBusMode.Ascii:
                        master = ModbusSerialMaster.CreateRtu(port);
                        break;
                    default:
                        break;
                }
            }
            return master;
        }

        public static ModbusMaster CreateUdpMaster(UdpClient client)
        {
            ModbusMaster master = null;
            if (client != null)
            {
                master = ModbusIpMaster.CreateIp(client);
            }
            return master;
        }

        public static ModbusMaster CreateTcpMaster(TcpClient client)
        {
            ModbusMaster master = null;
            if (client != null)
            {
                master = ModbusIpMaster.CreateIp(client);
            }
            return master;
        }

        public static ModbusSlave CreateSlave(byte id, SerialPortAdapter adapter, SerialModBusMode type)
        {
            ModbusSlave slave = null;
            if (adapter != null)
            {
                switch (type)
                {
                    case SerialModBusMode.Rtu:
                        slave = ModbusSerialSlave.CreateRtu(id, adapter);
                        break;
                    case SerialModBusMode.Ascii:
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

        public static ModbusMaster CreateMaster(MConfig config)
        {
            if (config != null)
            {
                var s = config.GetType();
                if(s.FullName == typeof(SerialConfig).FullName)
                {
                    var conf = config as SerialConfig;
                    if (conf != null)
                    {
                        var port = conf.CreateSerialPort();
                        return CreateSerialMaster(port, conf.Mode);
                    }
                }
                else if (s.FullName == typeof(IPConfig).FullName)
                {
                    var con = config as IPConfig;
                    if (con != null)
                    {
                        if (con.Mode == IPMode.Tcp)
                        {
                            var client = new TcpClient(con.Address.ToString(),con.Port);
                            if (!client.Connected)
                            {
                                client.Connect(con.Address, con.Port);
                            }
                            return ModbusIpMaster.CreateIp(client);
                        }
                        else if (con.Mode == IPMode.Udp)
                        {
                            var client = new UdpClient();
                            client.Connect(con.CreateIPEndPoint());
                            return ModbusIpMaster.CreateIp(client);
                        }
                    }
                }
            }
            return null;
        }
    }

}
