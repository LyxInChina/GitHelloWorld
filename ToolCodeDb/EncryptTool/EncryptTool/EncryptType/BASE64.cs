using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncryptTool
{
    public class BASE64
    {
        public static string Encrypt(string entryStr)
        {
            string base64 = string.Empty;
            if (!string.IsNullOrWhiteSpace(entryStr))
            {
                byte[] bytes = Encoding.Default.GetBytes(entryStr);
                base64 = Convert.ToBase64String(bytes);
            }
            return base64;
        }

        public static string Decrypt(string entryStr)
        {
            string str = string.Empty;
            if (!string.IsNullOrWhiteSpace(entryStr))
            {
                byte[] bytes = new byte[] { };

                bytes = Convert.FromBase64String(entryStr);
                str = Encoding.Default.GetString(bytes);


            }
            return str;
        }
    }



}
