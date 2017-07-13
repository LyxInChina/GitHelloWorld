using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncryptTool.EncryptType
{
    public class DES
    {
        /// <summary>
        /// 密钥偏移向量 默认 8位
        /// </summary>
        private static byte[] IV = { 0xEF, 0xAB, 0x56, 0x78, 0x90, 0x34, 0xCD, 0x12 };
        /// <summary>
        /// 密钥 必须为8位
        /// </summary>
       // private static string key = "DesEncry";


        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="entryStr"></param>
        /// <returns></returns>
        public static byte[] Encrypt(string entryStr, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.CBC;//默认
            des.Padding = PaddingMode.PKCS7;//默认
            des.IV = IV;
            des.Key = Encoding.Default.GetBytes(key);

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] bytes = Encoding.Default.GetBytes(entryStr);
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(des.Key, des.IV), CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Count());
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        /// <summary>
        /// DES 解密 
        /// </summary>
        /// <param name="entryStr"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] bytes, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            des.IV = IV;
            des.Key = Encoding.Default.GetBytes(key);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(des.Key, des.IV), CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Count());
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }
    }
}
