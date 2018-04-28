using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace ModbusTest
{
    public class SerialModbus: IDisposable
    {
        private SerialPort MSerialPort;
        private ModbusMaster Master;
        private List<Thread> threads = new List<Thread>();
        private ManualResetEvent ManualResetLive = new ManualResetEvent(false);
        private ManualResetEvent ManualResetWork = new ManualResetEvent(false);
        public SerialModbus()
        {
            InitSerialPort();
            InitMaster();
            if (!MSerialPort.IsOpen)
            {
                try
                {
                    MSerialPort.Open();
                }
                catch (Exception ex)
                {

                }
            }
            for (int i = 0; i < 20; i++)
            {

                threads.Add(new Thread(work) { IsBackground = true });
            }
            threads.ForEach(t => t.Start());
        }
        private void InitSerialPort()
        {
            MSerialPort = new SerialPort();
            MSerialPort.PortName = "com3";
            MSerialPort.BaudRate = 9600;
            MSerialPort.StopBits = StopBits.One;
            MSerialPort.Parity = Parity.None;
            MSerialPort.DataBits = 8;
        }

        private void InitMaster()
        {
            Master = ModbusSerialMaster.CreateRtu(MSerialPort);
            Master.Transport.Retries = 0;
            Master.Transport.WaitToRetryMilliseconds = 100;
            Master.Transport.ReadTimeout = 100;
            Master.Transport.WriteTimeout = 100;
        }
        private int timec = 0;
        private void work()
        {
            Thread.Sleep(new Random().Next(50));
            while(true)
            {
                if (ManualResetLive.WaitOne(0))
                {
                    break;
                }
                if(ManualResetWork.WaitOne(-1))
                {
                    timec = Environment.TickCount - timec;
                    System.Diagnostics.Debug.WriteLine(String.Format("CurrentThreadID:{0},Time:{1}", Thread.CurrentThread.ManagedThreadId ,timec));
                    timec = Environment.TickCount;
                    try
                    {
                        MainWork();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
                     
            }
        }
        private void MainWork()
        {
            var res = Master.ReadHoldingRegisters(1, 4008, 3);
            for (int i = 0; i < res.Length; i++)
            {
                res[i]++;
            }
            var id = (ushort)Thread.CurrentThread.ManagedThreadId;
            Master.WriteMultipleRegisters(1, 4008, new ushort[] { id,id,id,id});
        }

        public void WorkControl(bool v)
        {
            if (v)
            {
                ManualResetWork.Set();
            }
            else
            {
                ManualResetWork.Reset();
            }            
        }

        public void Dispose()
        {
            ManualResetWork.Set();
            ManualResetLive.Set();
            Master.Dispose();
            MSerialPort.Close();
            MSerialPort.Dispose();
        }
    }
}
