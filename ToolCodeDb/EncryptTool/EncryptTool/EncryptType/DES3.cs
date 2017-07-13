using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncryptTool.EncryptType
{
    /// <summary>
    /// DES3
    /// </summary>
    public  class DES3
    {
        private static  byte[] IV = { 0xEF, 0xAB, 0x56, 0x78, 0x90, 0x34, 0xCD, 0x12 };

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="entryStr"></param>
        /// <param name="key">key 长度只能为8位</param>
        /// <returns></returns>
        public static byte[] Encrypt(string entryStr, string key)
        {
            DESCryptoServiceProvider des3 = new DESCryptoServiceProvider();
            des3.Mode = CipherMode.CBC;//默认值
            des3.Padding = PaddingMode.PKCS7;//默认值
            des3.Key = Encoding.Default.GetBytes(key);
            des3.IV = IV;
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] bytes = Encoding.Default.GetBytes(entryStr);
                using (CryptoStream cs = new CryptoStream(ms, des3.CreateEncryptor(des3.Key, des3.IV), CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Count());
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }

        }

        /// <summary>
        ///  解密
        /// </summary>
        /// <param name="entryStr"></param>
        /// <param name="key">key 长度只能为8位</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] bytes, string key)
        {
            DESCryptoServiceProvider des3 = new DESCryptoServiceProvider();
            des3.Mode = CipherMode.CBC;//默认值
            des3.Padding = PaddingMode.PKCS7;//默认值
            des3.Key = Encoding.Default.GetBytes(key);
            des3.IV = IV;

            using (MemoryStream ms = new MemoryStream())
            {
                //byte[] bytes = Convert.FromBase64String(entryStr);
                using (CryptoStream cs = new CryptoStream(ms, des3.CreateDecryptor(des3.Key, des3.IV), CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Count());
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }
    }
}
