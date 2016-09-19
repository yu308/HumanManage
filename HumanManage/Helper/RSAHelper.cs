using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HumanManage.Helper
{
    public class RSAHelper
    {
        private static string private_key = string.Empty;
        public static void RSAGen()
        {
            string keyFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.xml");
            const int keySize = 1024;
            // 这个类在System.Security.Cryptography命名空间中
            RSACryptoServiceProvider sp = new RSACryptoServiceProvider(keySize);
            // 参数true表示XML中包含私钥。如果给false表示只生成公钥的XML
            string str = sp.ToXmlString(true);
            using (TextWriter writer = new StreamWriter(keyFileName))
            {
                writer.Write(str);
            }
        }

        private static string BytesToHexString(byte[] input)
        {
            StringBuilder hexString = new StringBuilder(64);
            for (int i = 0; i < input.Length; i++)
            {
                hexString.Append(String.Format("{0:X2}", input[i]));
            }
            return hexString.ToString();
        }
        private static byte[] HexStringToBytes(string hex)
        {
            if (hex.Length == 0)
            {
                return new byte[] { 0 };
            }
            if (hex.Length % 2 == 1)
            {
                hex = "0" + hex;
            }
            byte[] result = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length / 2; i++)
            {
                result[i] = byte.Parse(hex.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            return result;
        }
    
    public static List<string> RSAGet()
        {
            string keyFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.xml");
            RSACryptoServiceProvider sp = new RSACryptoServiceProvider();
            string str = string.Empty;
            using (TextReader reader = new StreamReader(keyFileName))
            {
                str=reader.ReadToEnd();
            }

            sp.FromXmlString(str);

            RSAParameters rsap = sp.ExportParameters(false);
            private_key = sp.ToXmlString(true);
            return new List<string> { BytesToHexString(rsap.Exponent), BytesToHexString(rsap.Modulus) };
        }

        public static string RSADecrpt(string encrypt_string)
        {
            string plain_str = string.Empty;
            RSACryptoServiceProvider sp = new RSACryptoServiceProvider();
            sp.FromXmlString(private_key);
            byte[] res = sp.Decrypt(HexStringToBytes(encrypt_string), false);
            ASCIIEncoding enc = new ASCIIEncoding();
            plain_str = enc.GetString(res);
            return plain_str;
        }
    }
}