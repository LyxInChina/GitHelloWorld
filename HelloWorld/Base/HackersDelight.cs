using System;
using System.Diagnostics.Contracts;

namespace HelloWorld
{
    class HackersDelight
    {
        static void Out(dynamic value,int intbase=2)
        {
            Contract.Requires(intbase == 2 || intbase == 8 || intbase == 10 || intbase == 16);
            if(value is string)
            {
                Console.WriteLine(value);
            }
            else if(value is byte|| value is short
                || value is UInt16||value is Int16
                || value is UInt32 || value is Int32
                || value is UInt64 || value is Int64)
            {
                Console.WriteLine(Convert.ToString(value, intbase));
            }
            else
            {
                Console.WriteLine(value.ToSting());
            }
        }

        static void Main()
        {
            ushort x = ushort.Parse("6086");
            Out(x.ToString());
            Out("Original Value:");
            Out(x);
            Out("~x");
            Out(~x);
            Out("-x");
            Out(-x);
            Out(~(-x)+1);
            Out("Turn Off Rightmost 1-bit to 0");
            Out(x & (x - 1));
            Out("Turn Off Rightmost 0-bit to 1");
            Out(x | (x + 1));
            Out("Turn Off Rightmost Trailing 1-bits to 0");
            Out(x & (x + 1));
            Out("Turn Off Rightmost Trailing 0-bits to 1");
            Out(x | (x - 1));
            Out("Signal the Rightmost 0-bit with 1");
            Out(~x & (x + 1));
            Out("Signal the Rightmost 1-bit with 0");
            Out(~x | (x - 1));
            Out("Signal the Rightmost Trailing 0-bit with 1");
            Out(~x & (x - 1));
            Out(~(x | -x));
            Out(~x & ~(-x));
            Out((x & -x)-1);
            Out("Signal the Rightmost Trailing 1-bit with 0");
            Out(~x | (x + 1));
            Out("Isolate the Rightmost 1-bit ");
            Out(x & (-x));
            Out("Singal the Rightmost 1-bit and Trailing 0-bits with 1");
            Out(x ^ (x - 1));
            Out("Singal the Rightmost 0-bit and Trailing 1-bits with 1");
            Out(x ^ (x + 1));
            Out("Turn Off the Rightmost Contiguous 1‘s");//check x form 2^j - 2^k by take 0-test
            Out(((x | (x - 1)) + 1) & x);
            Out(((x & -x) + x) & x);
            Out("De Morgan Laws Extended");
            Out("~(x & y) = ~x | ~y");
            Out("~(x | y) = ~x & ~y");
            Out("~(x + y) = ~x - y");
            Out("~(x - y) = ~x + y");
            Out("~(x + 1) = ~x - 1");
            Out("~(x - 1) = ~x + 1");
            Out("~-x  = x - 1");
            Out("~(x ^ y) = ~x ^ y = x E y");
            Out("~(x E y) = ~x E y = x ^ y");

            //a= --a ~~a = ~(-a)+1          
            //a-1 = ~(-a)-> -a = ~(a-1)
            Out(x - 1);
            Out(~(-x));

            int aaa = 255;
            Out(aaa);            
        }

    }
    //common language specification
    public static class CLS
    {
        public static ushort MAXIMUM_CAPACITY_Ushort = 1 << (sizeof(ushort) - 1);
        public static short MAXIMUM_CAPACITY_Short = 1 << (sizeof(short) - 1);
        public static int MAXIMUM_CAPACITY_Int = 1 << (sizeof(int) - 1);
        public static uint MAXIMUM_CAPACITY_Uint = 1 << (sizeof(uint) - 1);
        public static long MAXIMUM_CAPACITY_Long = 1 << (sizeof(long) - 1);
        public static ulong MAXIMUM_CAPACITY_Ulong = 1 << (sizeof(ulong) - 1);

        public static int tableSizeFor_or(int c)
        {
            int n = c - 1;
            n |= n >> 1;
            n |= n >> 2;
            n |= n >> 4;
            n |= n >> 8;
            n |= n >> 16;
            n |= n >> 32;
            return (n < 0) ? 1 : (n >= MAXIMUM_CAPACITY_Int) ? MAXIMUM_CAPACITY_Int : n + 1;
        }
        public static int tableSizeFor(int c)
        {
            int n = c - 1;
            int s = sizeof(int);
            for (int i = 0; i < s; i++)
            {
                n |= n >> (1 << i);
            }
            return (n < 0) ? 1 : (n >= MAXIMUM_CAPACITY_Int) ? MAXIMUM_CAPACITY_Int : n + 1;
        }
        public static ulong tableSizeFor(ulong c)
        {
            var n = c - 1;
            int s = sizeof(ulong);
            for (int i = 0; i < s; i++)
            {
                n |= n >> (1 << i);
            }
            return (n < 0) ? 1 : (n >= MAXIMUM_CAPACITY_Ulong) ? MAXIMUM_CAPACITY_Ulong : n + 1;
        }
    }

}
