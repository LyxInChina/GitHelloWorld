using System;
using System.Collections.Generic;
using System.Linq;

namespace ModbusTest
{
    static class Program
    {
        static void Main(string[] args)
        {
            var serialm = new SerialModbus();
            serialm.WorkControl(true);
            while (true)
            {
                var input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    serialm.Dispose();
                    break;
                }
            }
        }
    }
}
