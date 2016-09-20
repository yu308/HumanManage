using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HumanManage.Helper
{
    public static class RSAHelper
    {
        private static string private_key = string.Empty;

        //private static RSAParameters rsap = new RSAParameters
        //{
        //    Modulus = Convert.FromBase64String(@"uQlRZvfH6MMdhNRgiAlKMY88dqsU2suKNIWbHY/FiTsvDgH5DLmNmGMp85qtQwSPhBQ+/E7DQkvk1OxIN7EBL+21NRPJIaDKuJciWC940ZFVU0d5oUujKy5uCrF/rfZce8MXjoiErtc+QRjCKI8wfGdIKuclooEPiJwb1rydMuE="),
        //    Exponent = Convert.FromBase64String(@"AQAB"),
        //    P = Convert.FromBase64String(@"wSwI9i+aM6h7hayvFD01iINAeZ9JK5qExBJAWDzjOQwWRE9x1dCX52jb+HrutwblfqQuOk6hazOmGTluxITXQw=="),
        //    Q = Convert.FromBase64String(@"9TfkbPTexGpQ9ZHNjYnmRJLcG8wG6yzzJ/RrWIjq1IKQYMhYDq08bNbUVuXlntKW9GgmEYnuhP8smrH5y+mRCw=="),
        //    DP = Convert.FromBase64String(@"s2Xx7LDIxLD0BnEZJ/KwhNdgSZNkoNof8vgASfJCE/jltQsS7T+L053OrDV+/PuqprJTPFNKFgUhfMuZ02iLgQ=="),
        //    DQ = Convert.FromBase64String(@"5IfLXXO0LI78lm/khlUPAbdwZIN3qzMABat3Y1Jur9BiZ6Au2LbASprH15h3r9WJE4wAdnX6kX4SfrUBHPW20w=="),
        //    InverseQ = Convert.FromBase64String(@"FhlNb2WkipUaXvuwDxEWPeE754+qM2F5otEUP9clG91yaerdsBpBmU0G6S2AqUNjr/qgfpQyl1EW2dl10rmTpw=="),
        //    D = Convert.FromBase64String(@"WjhPXv/Qks7T7UiqGppA+UIoToojPH1C0VIVrEfGHp/jVRakKs6sWhF7yoHwGf22xkUi4t26efBMTn84xSLCexjQwj5AQtYk+3Qr2QjRDdn2ooIV1gWKW/C0O0+80Y6PEeszItuBVfjKC6mNEcZ1g44/wOdvIG7Olsl0F7vmQrM=")
        //};
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

        private static byte[] ToBytes(this string me, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.ASCII;
            return encoding.GetBytes(me);
        }

        private static string GetString(this byte[] me, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.ASCII;
            return encoding.GetString(me);
        }

        private static byte[] HexDecode(this string me)
        {
            string s = me.Length % 2 == 1 ? "0" + me : me;
            byte[] data = new byte[s.Length / 2];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = byte.Parse(s.Substring(i + i, 2), NumberStyles.HexNumber);
            }
            return data;
        }

        private static string HexEncode(this byte[] me)
        {
            return BitConverter.ToString(me).Replace("-", string.Empty);
        }

        public static List<string> RSAGet()
        {
            string keyFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.xml");
            RSACryptoServiceProvider sp = new RSACryptoServiceProvider();
            string str = string.Empty;
            using (TextReader reader = new StreamReader(keyFileName))
            {
                str = reader.ReadToEnd();
            }

            sp.FromXmlString(str);

            RSAParameters rsap = sp.ExportParameters(false);
            private_key = sp.ToXmlString(true);
            return new List<string> { rsap.Exponent.HexEncode(), rsap.Modulus.HexEncode() };
        }

        public static string RSADecrpt(string encrypt_string)
        {
            string plain_str = string.Empty;
            RSACryptoServiceProvider sp = new RSACryptoServiceProvider();
            sp.FromXmlString(private_key);
            byte[] res = sp.Decrypt(encrypt_string.HexDecode(), false);
            plain_str = res.GetString();
            return plain_str;
        }
    }
}