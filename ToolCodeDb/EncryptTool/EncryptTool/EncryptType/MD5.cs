using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncryptTool.EncryptType
{
    /// <summary>
    /// MD5摘要算法
    /// </summary>
    public class MD5
    {
        public static string Encrypt(string entryStr)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(entryStr);
            byte[] hash = md5.ComputeHash(bytes);

            return BitConverter.ToString(hash).Replace("-", "");

            //用此方法也可行
            //StringBuilder sb = new StringBuilder();
            //foreach (byte iByte in hash)
            //{
            //    sb.AppendFormat("{0:X2}", iByte);
            //}
            //return sb.ToString();
        }

    }
}
