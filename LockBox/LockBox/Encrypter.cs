using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LockBox
{
    /// <summary>
    /// Responsible for the encoding and decoding of data
    /// </summary>
    static class Enccrypter
    {
        static public string Encode(string data, string pass)
        {
            byte[] textbytes = ASCIIEncoding.ASCII.GetBytes(data);

            AesCryptoServiceProvider endec = new AesCryptoServiceProvider();
            endec.BlockSize = 128;
            endec.KeySize = 256;
            endec.IV = Encoding.UTF8.GetBytes("1a1a1a1a1a1a1a1a");
            endec.Key = sha256_hash(pass);
            endec.Padding = PaddingMode.PKCS7;
            endec.Mode = CipherMode.CBC;
            ICryptoTransform icrypt = endec.CreateEncryptor(endec.Key, endec.IV);
            byte[] enc = icrypt.TransformFinalBlock(textbytes, 0, textbytes.Length);
            icrypt.Dispose();

            return Convert.ToBase64String(enc);
        }

        static public string Decode(string data, string pass)
        {
            byte[] textbytes = Convert.FromBase64String(data);

            AesCryptoServiceProvider endec = new AesCryptoServiceProvider();
            endec.BlockSize = 128;
            endec.KeySize = 256;
            endec.IV = Encoding.UTF8.GetBytes("1a1a1a1a1a1a1a1a");
            endec.Key = sha256_hash(pass);
            endec.Padding = PaddingMode.PKCS7;
            endec.Mode = CipherMode.CBC;
            ICryptoTransform icrypt = endec.CreateDecryptor(endec.Key, endec.IV);
            byte[] enc = icrypt.TransformFinalBlock(textbytes, 0, textbytes.Length);
            icrypt.Dispose();

            return System.Text.ASCIIEncoding.ASCII.GetString(enc);
        }

        /// <summary>
        /// Converts the given string into a hash value
        /// </summary>
        /// <param name="value">string to be converted into hash</param>
        /// <returns></returns>
        static private Byte[] sha256_hash(string value)//https://stackoverflow.com/questions/16999361/obtain-sha-256-string-of-a-string
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }

    }
}
