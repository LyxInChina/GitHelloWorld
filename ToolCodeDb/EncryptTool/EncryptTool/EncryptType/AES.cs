using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncryptTool
{
    public class AES
    {
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptStr">明文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static byte[] Encrypt(string encryptStr, string key)
        {
            //byte[] keyArray =Encoding.Default.GetBytes(key);
            //byte[] toEncryptArray = Encoding.Default.GetBytes(encryptStr);
            //RijndaelManaged rDel = new RijndaelManaged();
            //rDel.Key = keyArray;
            ////rDel.KeySize = 128;
            //rDel.Mode = CipherMode.ECB;
            //rDel.Padding = PaddingMode.PKCS7;
            //ICryptoTransform cTransform = rDel.CreateEncryptor();
            //byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            //return Encoding.Default.GetString(resultArray);

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = Encoding.Default.GetBytes(key);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] bytes = Encoding.Default.GetBytes(encryptStr);
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Count());
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }



        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptStr">密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] bytes, string key)
        {
            //byte[] keyArray = Encoding.Default.GetBytes(key);
            //byte[] toEncryptArray = Encoding.Default.GetBytes(decryptStr);
            //RijndaelManaged rDel = new RijndaelManaged();
            //rDel.Key = keyArray;
            //// rDel.KeySize = 128;
            //rDel.Mode = CipherMode.ECB;
            //rDel.Padding = PaddingMode.PKCS7;
            //ICryptoTransform cTransform = rDel.CreateDecryptor();
            //byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            //return Encoding.Default.GetString(resultArray);

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = Encoding.Default.GetBytes(key);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            using (MemoryStream ms = new MemoryStream())
            {
                //byte[] bytes = Convert.FromBase64String(decryptStr);
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Count());
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }


        }


        /// <summary>  
        /// 2进制转16进制  
        /// </summary>  
        public static String Hex_2To16(Byte[] bytes)
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
                    strB.Append(bytes[i].ToString("X2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        /// <summary>  
        /// 16进制转2进制  
        /// </summary>  
        public static Byte[] Hex_16To2(String hexString)
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
    }
}
