using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace HumanManage.Helper
{
    public class RSAHelper
    {

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

        private static string RSAByte2Str(byte[] b)
        {
            return BitConverter.ToString(b).Replace("-", string.Empty);
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

            return new List<string> { RSAByte2Str(rsap.Exponent), RSAByte2Str(rsap.Modulus) };
        }
    }
}