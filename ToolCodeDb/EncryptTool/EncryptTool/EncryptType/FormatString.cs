using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EncryptTool.EncryptType
{
    public class FormatString
    {
        /// <summary>
        /// 16进制转2进制
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static  byte[] Hex_16To2(string hexString)
        {
            if ((hexString.Length % 2) != 0)
            {
                hexString += " ";
            }
            Byte[] returnBytes = new Byte[hexString.Length / 2];
            for (Int32 i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return returnBytes;
        }

        /// <summary>
        /// 2进制转16进制
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Hex_2To16(byte[] bytes)
        {
            String hexString = String.Empty;
            Int32 iLength = 65535;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                if (bytes.Length < iLength)
                {
                    iLength = bytes.Length;
                }

                for (int i = 0; i < iLength; i++)
                {
                    strB.AppendFormat("{0:X2}",bytes[i]);
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        /// <summary>
        /// 转Base64
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToBase64String(byte[] bytes)
        {
            if(bytes.Length>0)
            {
                return Convert.ToBase64String(bytes);
            }
            return string.Empty;
        }

        /// <summary>
        /// 解Base64
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static byte[] FromBase64String(string base64String)
        {
            if(!string.IsNullOrWhiteSpace(base64String))
            {
                return Convert.FromBase64String(base64String);
            }

            return new byte[] {};
        }
    }
}
