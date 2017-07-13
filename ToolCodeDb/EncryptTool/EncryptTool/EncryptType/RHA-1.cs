using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncryptTool.EncryptType
{
    /// <summary>
    /// RHA 摘要算法
    /// </summary>
    public class RHA_1
    {
        public static  string Encrypt(string entryStr)
        {
            byte[] bytes = Encoding.Default.GetBytes(entryStr);
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] shaByte = sha1.ComputeHash(bytes);

            return BitConverter.ToString(shaByte).Replace("-","");

            //用此方法也可行
            //StringBuilder sb = new StringBuilder();
            //foreach (byte iByte in shaByte)
            //{
            //    sb.AppendFormat("{0:X2}", iByte);
            //}
            //return sb.ToString();
        }
    }
}
