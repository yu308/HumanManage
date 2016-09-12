﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using System.Web.Security;
using System.Net;
using System.Collections;


namespace HumanManage.Helpers
{
    public class StringHelper
    {
        private static string passWord;	//加密字符串

        #region 单个数字转汉字
        /// <summary>
        /// 单个数字转汉字
        /// </summary>
        /// <param name="num">单个数字转汉字</param>
        /// <returns></returns>
        public static string GetNumToChar(string num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 

            return str1.Substring(int.Parse(num), 1);

        }
        #endregion

        #region 判断输入是否数字
        /// <summary>
        /// 判断输入是否数字
        /// </summary>
        /// <param name="num">要判断的字符串</param>
        /// <returns></returns>
        static public bool VldInt(string num)
        {

            int ResultNum;
            return int.TryParse(num, out ResultNum);

        }
        #endregion

        #region 返回文本编辑器替换后的字符串
        /// <summary>
        /// 返回文本编辑器替换后的字符串
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <returns></returns>
        static public string GetHtmlEditReplace(string str)
        {

            return str.Replace("'", "’").Replace("&nbsp;", " ").Replace(",", ",").Replace("%", "％").
                Replace("script", "").Replace(".js", "");

        }
        #endregion

        #region 截取字符串函数
        /// <summary>
        /// 截取字符串函数
        /// </summary>
        /// <param name="str">所要截取的字符串</param>
        /// <param name="num">截取字符串的长度</param>
        /// <returns></returns>
        static public string GetSubString(string str, int num)
        {

            return (str.Length > num) ? str.Substring(0, num) + "..." : str;

        }
        #endregion

        #region 截取字符串优化版
        /// <summary>
        /// 截取字符串优化版
        /// </summary>
        /// <param name="stringToSub">所要截取的字符串</param>
        /// <param name="length">截取字符串的长度</param>
        /// <returns></returns>
        public static string GetFirstString(string stringToSub, int length)
        {
            if (stringToSub != null)
                stringToSub = HtmlToString(stringToSub);
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            if (!String.IsNullOrEmpty(stringToSub))
            {
                char[] stringChar = stringToSub.ToCharArray();
                StringBuilder sb = new StringBuilder();
                int nLength = 0;
                bool isCut = false;
                for (int i = 0; i < stringChar.Length; i++)
                {
                    if (regex.IsMatch((stringChar[i]).ToString()))
                    {
                        sb.Append(stringChar[i]);
                        nLength += 2;
                    }
                    else
                    {
                        sb.Append(stringChar[i]);
                        nLength = nLength + 1;
                    }

                    if (nLength > length)
                    {
                        isCut = true;
                        break;
                    }
                }
                if (isCut)
                    //return sb.ToString() + "..";
                    return sb.ToString();
                else
                    return sb.ToString();
            }
            else
                return "";
        }

        /// <summary>
        /// 截取字符串优化版
        /// </summary>
        /// <param name="stringToSub">所要截取的字符串</param>
        /// <param name="length">截取字符串的长度</param>
        /// <returns></returns>
        public static string GetFirstString(string stringToSub, int length, bool isclernHtml)
        {
            if (stringToSub != null && isclernHtml)
                stringToSub = HtmlToString(stringToSub);
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            if (!String.IsNullOrEmpty(stringToSub))
            {
                char[] stringChar = stringToSub.ToCharArray();
                StringBuilder sb = new StringBuilder();
                int nLength = 0;
                bool isCut = false;
                for (int i = 0; i < stringChar.Length; i++)
                {
                    if (regex.IsMatch((stringChar[i]).ToString()))
                    {
                        sb.Append(stringChar[i]);
                        nLength += 2;
                    }
                    else
                    {
                        sb.Append(stringChar[i]);
                        nLength = nLength + 1;
                    }

                    if (nLength > length)
                    {
                        isCut = true;
                        break;
                    }
                }
                if (isCut)
                    //return sb.ToString() + "..";
                    return sb.ToString();
                else
                    return sb.ToString();
            }
            else
                return "";
        }
        #endregion

        #region  截取过滤Html的字符串
        public static string GetNoHtmlSubString(string stringToSub, int length)
        {
            string str = ClearHtml(stringToSub);
            return GetFirstString(str, length);
        }

        #endregion

        #region 过滤输入信息
        /// <summary>
        /// 过滤输入信息
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>
        public static string InputText(string text, int maxLength)
        {

            text = text.Trim();
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            if (text.Length > maxLength)
                text = text.Substring(0, maxLength);
            text = Regex.Replace(text, "[\\s]{2,}", " ");	//two or more spaces
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags
            text = text.Replace("'", "''");
            return text;

        }
        #endregion

        #region 生成随机数
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        public static string GenerateCheckCode()
        {

            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 20; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }

            HttpContext.Current.Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));

            return checkCode;

        }

        /// <summary>
        /// 生成随机字母数字
        /// </summary>
        /// <param name="length">指定验证码的长度</param>
        /// <returns></returns>
        public static string CreatePassWord(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";
            //生成起始序列值
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 2));
            }
            //生成验证码
            for (int i = 0; i < length; i++)
            {
                string a = "AB0CD1EF2GH3IJ4KL5MN6OP7QR8ST9UVWSYZAB0CD1EF2GH3IJ4KL5MN6OP7QR8ST9UVWSYZAB0CD1EF2GH3IJ4KL5MN6OP7QR8ST9UVWSYZAB0CD1EF2GH3IJ4KL5MN6OP7QR8ST9UVWSYZ";
                validateNumberStr += a.Substring(validateNums[i], 1);
            }
            return validateNumberStr;
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="num">随机次数</param>
        /// <returns></returns>
        public static string GetRandomCode(int num)
        {
            string str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string randomCode = "";
            Random rd = new Random();
            for (int i = 0; i < num; i++)
            {
                randomCode += str.Substring(rd.Next(0, str.Length), 1);
            }
            return randomCode;
        }
        #endregion

        #region 生成验证码图片
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        public void CreateCheckCodeImage()
        {

            string checkCode = GenerateCheckCode();
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(Color.White);

                //画图片的背景噪音线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 150; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Gif";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }

        }
        #endregion

        #region 生成指定位数随机数
        private static char[] constant =   
          {   
            '0','1','2','3','4','5','6','7','8','9',   
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'   
          };

        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }

        public static string GetNumRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(10);
            char[] NumStr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(10)]);
            }
            return newRandom.ToString();
        }

        /// <summary>
        /// 获取订单编号
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNo()
        {
            string time = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now) + GetNumRandom(4);
            return time;
        }
        #endregion

        #region 获取汉字第一个拼音
        /// <summary>
        /// 获取汉字第一个拼音
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string getSpells(string input)
        {
            int len = input.Length;
            string reVal = "";
            for (int i = 0; i < len; i++)
            {
                reVal += getSpell(input.Substring(i, 1));
            }
            return reVal;

        }
        #endregion

        #region 获取汉字的字母
        static public string getSpell(string cn)
        {

            byte[] arrCN = Encoding.Default.GetBytes(cn);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "?";
            }
            else return cn;

        }
        #endregion

        #region 半角转全角
        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="BJstr"></param>
        /// <returns></returns>
        static public string GetQuanJiao(string BJstr)
        {

            char[] c = BJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 0)
                    {
                        b[0] = (byte)(b[0] - 32);
                        b[1] = 255;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }

            string strNew = new string(c);
            return strNew;

        }
        #endregion

        #region 全角转半角
        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="QJstr"></param>
        /// <returns></returns>
        static public string GetBanJiao(string QJstr)
        {

            char[] c = QJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            string strNew = new string(c);
            return strNew;

        }
        #endregion

        #region 加密的类型
        /// <summary>
        /// 加密的类型
        /// </summary>
        public enum PasswordType
        {
            SHA1,
            MD5
        }
        #endregion

        #region 字符串加密
        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="PasswordString">要加密的字符串</param>
        /// <param name="PasswordFormat">要加密的类别</param>
        /// <returns></returns>
        static public string EncryptPassword(string PasswordString, PasswordType PasswordFormat)
        {

            switch (PasswordFormat)
            {
                case PasswordType.SHA1:
                    {
                        passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "SHA1");
                        break;
                    }
                case PasswordType.MD5:
                    {
                        passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "MD5").Substring(8, 16).ToLower();
                        break;
                    }
                default:
                    {
                        passWord = string.Empty;
                        break;
                    }
            }
            return passWord;

        }
        #endregion

        #region 字符串转换为 html
        /// <summary>
        /// 字符串转换为 html
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToHtml(string str)
        {

            str = str.Replace("&", "&amp;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\r\n", "<br>");

            return str;
        }
        #endregion

        #region html转换成字符串
        /// <summary>
        /// html转换成字符串
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToString(string strHtml)
        {

            strHtml = strHtml.Replace("<br>", "\r\n");
            strHtml = strHtml.Replace(@"<br />", "\r\n");
            strHtml = strHtml.Replace(@"<br/>", "\r\n");
            strHtml = strHtml.Replace("&gt;", ">");
            strHtml = strHtml.Replace("&lt;", "<");
            strHtml = strHtml.Replace("&nbsp;", " ");
            strHtml = strHtml.Replace("&quot;", "\"");

            strHtml = Regex.Replace(strHtml, @"<\/?[^>]+>", "", RegexOptions.IgnoreCase);

            return strHtml;
        }
        #endregion

        #region img转换绝对地址
        /// <summary>
        /// html转换成字符串
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string ImgToUrl(string strHtml)
        {

            Regex r = new Regex("<img[^>]*?src=\"[^http]+([^>]+?)(?:\"| )[^>]*?>", RegexOptions.ExplicitCapture);
            string d = "";
            string e = strHtml;
            foreach (var a in r.Matches(strHtml, 0))
            {
                Regex b = new Regex("src=\"", RegexOptions.IgnoreCase);
                d = b.Replace(a.ToString(), "src=\"http://gl.365pzg.com");
                Regex c = new Regex(a.ToString(), RegexOptions.IgnoreCase);
                e = c.Replace(e, d);

            }
            return e;
        }
        #endregion


        #region 获得中文星期表示形式
        /// <summary>
        /// 获得中文星期表示形式
        /// </summary>
        /// <returns></returns>
        public static string GetChineseWeek(DateTime t)
        {

            string week = "";

            switch (t.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "三";
                    break;
                case DayOfWeek.Thursday:
                    week = "四";
                    break;
                case DayOfWeek.Friday:
                    week = "五";
                    break;
                case DayOfWeek.Saturday:
                    week = "六";
                    break;
                case DayOfWeek.Sunday:
                    week = "日";
                    break;
            }

            return "星期" + week;

        }
        #endregion

        #region 生成日期随机码
        /// <summary>
        /// 生成日期随机码
        /// </summary>
        /// <returns></returns>
        public static string GetRamCode()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssff");

        }
        #endregion

        #region 生成指定长度的字符串,即生成strLong个str字符串
        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong, string str)
        {

            string ReturnStr = string.Empty;
            for (int i = 0; i < strLong; i++)
            {
                ReturnStr += str;
            }
            return ReturnStr;

        }
        #endregion

        #region 字符串过滤
        /// <summary>
        /// 字符串过滤
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterStr(string str)
        {
            return str.Replace("'", " ").Replace(".", "").Replace("\r\n", "　");
        }
        #endregion

        #region 生成条形码
        /// <summary>
        /// 生成条形码：如：bar_code("20070520122334", 20, 1, 1);
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ch">度度</param>
        /// <param name="cw">线条宽度</param>
        /// <param name="type_code">是否输出文字1为输出</param>
        /// <returns></returns>
        public static string BarCode(object str, int ch, int cw, int type_code)
        {

            string strTmp = str.ToString();
            string code = strTmp;
            strTmp = strTmp.ToLower();
            int height = ch;
            int width = cw;

            strTmp = strTmp.Replace("0", "_|_|__||_||_|"); ;
            strTmp = strTmp.Replace("1", "_||_|__|_|_||");
            strTmp = strTmp.Replace("2", "_|_||__|_|_||");
            strTmp = strTmp.Replace("3", "_||_||__|_|_|");
            strTmp = strTmp.Replace("4", "_|_|__||_|_||");
            strTmp = strTmp.Replace("5", "_||_|__||_|_|");
            strTmp = strTmp.Replace("7", "_|_|__|_||_||");
            strTmp = strTmp.Replace("6", "_|_||__||_|_|");
            strTmp = strTmp.Replace("8", "_||_|__|_||_|");
            strTmp = strTmp.Replace("9", "_|_||__|_||_|");
            strTmp = strTmp.Replace("a", "_||_|_|__|_||");
            strTmp = strTmp.Replace("b", "_|_||_|__|_||");
            strTmp = strTmp.Replace("c", "_||_||_|__|_|");
            strTmp = strTmp.Replace("d", "_|_|_||__|_||");
            strTmp = strTmp.Replace("e", "_||_|_||__|_|");
            strTmp = strTmp.Replace("f", "_|_||_||__|_|");
            strTmp = strTmp.Replace("g", "_|_|_|__||_||");
            strTmp = strTmp.Replace("h", "_||_|_|__||_|");
            strTmp = strTmp.Replace("i", "_|_||_|__||_|");
            strTmp = strTmp.Replace("j", "_|_|_||__||_|");
            strTmp = strTmp.Replace("k", "_||_|_|_|__||");
            strTmp = strTmp.Replace("l", "_|_||_|_|__||");
            strTmp = strTmp.Replace("m", "_||_||_|_|__|");
            strTmp = strTmp.Replace("n", "_|_|_||_|__||");
            strTmp = strTmp.Replace("o", "_||_|_||_|__|");
            strTmp = strTmp.Replace("p", "_|_||_||_|__|");
            strTmp = strTmp.Replace("r", "_||_|_|_||__|");
            strTmp = strTmp.Replace("q", "_|_|_|_||__||");
            strTmp = strTmp.Replace("s", "_|_||_|_||__|");
            strTmp = strTmp.Replace("t", "_|_|_||_||__|");
            strTmp = strTmp.Replace("u", "_||__|_|_|_||");
            strTmp = strTmp.Replace("v", "_|__||_|_|_||");
            strTmp = strTmp.Replace("w", "_||__||_|_|_|");
            strTmp = strTmp.Replace("x", "_|__|_||_|_||");
            strTmp = strTmp.Replace("y", "_||__|_||_|_|");
            strTmp = strTmp.Replace("z", "_|__||_||_|_|");
            strTmp = strTmp.Replace("-", "_|__|_|_||_||");
            strTmp = strTmp.Replace("*", "_|__|_||_||_|");
            strTmp = strTmp.Replace("/", "_|__|__|_|__|");
            strTmp = strTmp.Replace("%", "_|_|__|__|__|");
            strTmp = strTmp.Replace("+", "_|__|_|__|__|");
            strTmp = strTmp.Replace(".", "_||__|_|_||_|");
            strTmp = strTmp.Replace("_", "<span   style='height:" + height + ";width:" + width + ";background:#FFFFFF;'></span>");
            strTmp = strTmp.Replace("|", "<span   style='height:" + height + ";width:" + width + ";background:#000000;'></span>");

            if (type_code == 1)
            {
                return strTmp + "<BR>" + code;
            }
            else
            {
                return strTmp;
            }

        }
        #endregion

        #region 清理Html
        public static string ClearHtml(string HtmlString)
        {
            string pn = "(</?.*?/?>)";
            HtmlString = Regex.Replace(HtmlString, pn, "");
            return HtmlString;
        }

        public static string ClearFormat(string HtmlString)
        {
            HtmlString = HtmlString.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
            return HtmlString.Trim();
        }
        #endregion

        #region 获取N行数据
        /// <summary>
        /// 取指定行数数据
        /// </summary>
        /// <param name="str">传入的待取字符串</param>
        /// <param name="rowsnum">指定的行数</param>
        /// <param name="strnum">每行的英文字符数或字节数</param>
        /// <returns></returns>
        public static string GetContent(string str, int rowsnum, int strnum)
        {
            //1计算内容块           
            string content = str.Replace("\r\n", "§");
            string[] strContent = content.Split(Convert.ToChar("§"));

            int strCount = rowsnum * strnum;
            int cutrow = rowsnum - strContent.Length;
            cutrow = rowsnum > 10 ? rowsnum : 10;
            int pStrCount;
            string setOkStr = "";


            //2对内容块进行
            for (int i = 0; i < strContent.Length; i++)
            {
                pStrCount = System.Text.Encoding.Default.GetBytes(strContent[i]).Length;
                if (pStrCount < strCount)
                {
                    setOkStr += strContent[i] + "<br>";
                    rowsnum -= Convert.ToInt32(Math.Ceiling((double)pStrCount / (double)strnum));
                    strCount = rowsnum * strnum;
                }
                else
                {
                    if (rowsnum > 0)
                    {
                        setOkStr += CutStr(strContent[i], rowsnum * strnum, cutrow);
                    }
                    else
                    {
                        //减去rowsnum是为了避免有些行长度为90,有的为89的现像
                        setOkStr = setOkStr.Substring(0, setOkStr.Length - cutrow / 2) + "...";
                    }
                    break;
                }
            }

            setOkStr = setOkStr.Replace("  ", "　"); //软(半角)空格转硬(全角)空格
            return setOkStr;

        }

        //字符串截取函数
        private static string CutStr(string str, int length, int rowsnum)
        {
            if (System.Text.Encoding.Default.GetBytes(str).Length < length)
                return str;

            length = length - rowsnum;
            int i = 0, j = 0;
            foreach (char chr in str)
            {
                if ((int)chr > 127)
                    i += 2;
                else
                    i++;
                if (i > length)
                {
                    str = str.Substring(0, j) + "...";
                    break;
                }
                j++;
            }
            return str;

        }
        #endregion

        #region 得到整型
        public static int ConvertToInt(string Str)
        {
            return Str.Trim() == string.Empty ? 0 : int.Parse(Str);
        }

        public static int GetInt(object o)
        {
            #region
            if (o == DBNull.Value || o == null)
                return 0;
            else
                return Convert.ToInt32(o);
            #endregion
        }
        #endregion

        #region 转化成时间类型
        /// <summary>
        /// 转化成时间类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static object ConvertDate(object o)
        {
            DateTime dt;
            if (DateTime.TryParse(o.ToString(), out dt))
                return dt;
            else
                return DBNull.Value;
        }
        #endregion

        #region 闭合HTML代码
        public static string CloseHTML(string str)
        {
            string[] HtmlTag = new string[] { "p", "div", "span", "table", "ul", "font", "b", "u", "i", "a", "h1", "h2", "h3", "h4", "h5", "h6" };

            for (int i = 0; i < HtmlTag.Length; i++)
            {
                int OpenNum = 0, CloseNum = 0;
                Regex re = new Regex("<" + HtmlTag + "[^>]*" + ">", RegexOptions.IgnoreCase);
                MatchCollection m = re.Matches(str);
                OpenNum = m.Count;
                re = new Regex("</" + HtmlTag + ">", RegexOptions.IgnoreCase);
                m = re.Matches(str);
                CloseNum = m.Count;

                for (int j = 0; j < OpenNum - CloseNum; j++)
                {
                    str += "</" + HtmlTag + ">";
                }
            }

            return str;
        }
        #endregion

        #region 得到192.248.23.*的IP
        /// <summary>
        /// 得到192.248.23.*的IP
        /// </summary>
        /// <param name="Str">IP地址</param>
        /// <returns></returns>
        public static string GetSortIp(string Str)
        {
            int x = Str.LastIndexOf('.') - 1;
            return Str.Substring(0, x) + "*.*";
        }
        #endregion

        #region 获取年月
        /// <summary>
        /// 获取年月
        /// </summary>
        /// <returns></returns>
        public static string GetYearMonth()
        {
            return DateTime.Now.ToString("yyyyMM");
        }
        #endregion

        #region 获取远程页面内容
        public static string GetHttpData(string Url)
        {
            //string sException = null;
            string sRslt = null;
            WebResponse oWebRps = null;
            WebRequest oWebRqst = WebRequest.Create(Url);
            oWebRqst.Timeout = 50000;
            try
            {
                oWebRps = oWebRqst.GetResponse();
            }
            catch (WebException e)
            {
                //sException = e.Message.ToString();
                //Response.Write(sException);
            }
            catch (Exception e)
            {
                //sException = e.ToString();
                //Response.Write(sException);
            }
            finally
            {
                if (oWebRps != null)
                {
                    StreamReader oStreamRd = new StreamReader(oWebRps.GetResponseStream(), Encoding.GetEncoding("UTF-8"));//GB2312|UTF-8"
                    sRslt = oStreamRd.ReadToEnd();
                    oStreamRd.Close();
                    oWebRps.Close();
                }
            }
            return sRslt;
        }

        public string[] GetData(string Html)
        {
            String[] rS = new String[2];
            string s = Html;
            s = Regex.Replace(s, "\\s{3,}", "");
            s = s.Replace("\r", "");
            s = s.Replace("\n", "");
            string Pat = "<td align=\"center\" class=\"24p\"><B>(.*)</B></td></tr><tr>.*(<table width=\"95%\" border=\"0\" cellspacing=\"0\" cellpadding=\"10\">.*</table>)<table width=\"98%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">(.*)<td align=center class=l6h>";
            Regex Re = new Regex(Pat);
            Match Ma = Re.Match(s);
            if (Ma.Success)
            {
                rS[0] = Ma.Groups[1].ToString();
                rS[1] = Ma.Groups[2].ToString();
                //pgStr = Ma.Groups[3].ToString();
            }
            return rS;
        }
        #endregion

        #region 判断页面是否存在
        /// <summary>
        /// 判断页面是否存在
        /// </summary>
        /// <param name="sURL"></param>
        /// <returns></returns>
        public static bool UrlExist(string sURL)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
                //WebProxy   proxy   =   new   WebProxy("your   proxy   server",   8080);   
                //request.Proxy   =   proxy;   
                request.Method = "HEAD";
                request.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                bool result = false;
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        result = true;
                        break;
                    case HttpStatusCode.Moved:
                        break;
                    case HttpStatusCode.NotFound:
                        break;
                }
                response.Close();
                return result;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region 获取字串中的链接
        /// <summary>
        /// 获取字串中的链接
        /// </summary>
        /// <param name="HtmlCode"></param>
        /// <returns></returns>
        public static ArrayList GetPageUrl(string HtmlCode)
        {
            ArrayList my_list = new ArrayList();
            string p = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            Regex re = new Regex(p, RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(HtmlCode);

            for (int i = 0; i <= mc.Count - 1; i++)
            {
                string name = mc[i].ToString();
                if (!my_list.Contains(name))//排除重复URL
                {
                    my_list.Add(name);
                }
            }
            return my_list;
        }
        #endregion

        #region 将 Stream 转化成 string
        /// <summary>
        /// 将 Stream 转化成 string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertStreamToString(Stream s)
        {
            string strResult = "";
            StreamReader sr = new StreamReader(s, Encoding.UTF8);

            Char[] read = new Char[256];

            // Read 256 charcters at a time.    
            int count = sr.Read(read, 0, 256);

            while (count > 0)
            {
                // Dump the 256 characters on a string and display the string onto the console.
                string str = new String(read, 0, count);
                strResult += str;
                count = sr.Read(read, 0, 256);
            }


            // 释放资源
            sr.Close();

            return strResult;
        }
        #endregion

        #region 对传递的参数字符串进行处理,防止注入式攻击
        /// <summary>
        /// 对传递的参数字符串进行处理,防止注入式攻击
        /// </summary>
        /// <param name="str">传递的参数字符串</param>
        /// <returns>String</returns>
        public static string ConvertSql(string str)
        {
            str = str.Trim();
            str = str.Replace("'", "''");
            str = str.Replace(";--", "");
            str = str.Replace("=", "");
            str = str.Replace(" or ", "");
            str = str.Replace(" and ", "");

            return str;
        }
        #endregion

        #region 格式化占用空间大小的输出
        /// <summary>
        /// 格式化占用空间大小的输出
        /// </summary>
        /// <param name="size">大小</param>
        /// <returns>返回 String</returns>
        public static string FormatNUM(long size)
        {

            decimal NUM;
            string strResult;

            if (size > 1073741824)
            {
                NUM = (Convert.ToDecimal(size) / Convert.ToDecimal(1073741824));
                strResult = NUM.ToString("N") + " M";
            }
            else if (size > 1048576)
            {
                NUM = (Convert.ToDecimal(size) / Convert.ToDecimal(1048576));
                strResult = NUM.ToString("N") + " M";
            }
            else if (size > 1024)
            {
                NUM = (Convert.ToDecimal(size) / Convert.ToDecimal(1024));
                strResult = NUM.ToString("N") + " KB";
            }
            else
            {
                strResult = size + " 字节";
            }

            return strResult;

        }
        #endregion

        #region 获取数组元素的合并字符串
        /// <summary>
        /// 获取数组元素的合并字符串
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        public static string GetArrayString(string[] stringArray)
        {
            #region
            string totalString = null;
            for (int i = 0; i < stringArray.Length; i++)
            {
                totalString = totalString + stringArray[i];
            }
            return totalString;
            #endregion
        }
        #endregion

        #region 将指定字符串中的汉字转换为拼音首字母的缩写,其中非汉字保留为原字符
        /// <summary>
        /// 将指定字符串中的汉字转换为拼音首字母的缩写,其中非汉字保留为原字符
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertSpellFirst(string text)
        {
            char pinyin;
            byte[] array;
            StringBuilder sb = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                pinyin = c;
                array = Encoding.Default.GetBytes(new char[] { c });

                if (array.Length == 2)
                {
                    int i = array[0] * 0x100 + array[1];

                    #region 条件匹配
                    if (i < 0xB0A1) pinyin = c;
                    else
                        if (i < 0xB0C5) pinyin = 'a';
                        else
                            if (i < 0xB2C1) pinyin = 'b';
                            else
                                if (i < 0xB4EE) pinyin = 'c';
                                else
                                    if (i < 0xB6EA) pinyin = 'd';
                                    else
                                        if (i < 0xB7A2) pinyin = 'e';
                                        else
                                            if (i < 0xB8C1) pinyin = 'f';
                                            else
                                                if (i < 0xB9FE) pinyin = 'g';
                                                else
                                                    if (i < 0xBBF7) pinyin = 'h';
                                                    else
                                                        if (i < 0xBFA6) pinyin = 'g';
                                                        else
                                                            if (i < 0xC0AC) pinyin = 'k';
                                                            else
                                                                if (i < 0xC2E8) pinyin = 'l';
                                                                else
                                                                    if (i < 0xC4C3) pinyin = 'm';
                                                                    else
                                                                        if (i < 0xC5B6) pinyin = 'n';
                                                                        else
                                                                            if (i < 0xC5BE) pinyin = 'o';
                                                                            else
                                                                                if (i < 0xC6DA) pinyin = 'p';
                                                                                else
                                                                                    if (i < 0xC8BB) pinyin = 'q';
                                                                                    else
                                                                                        if (i < 0xC8F6) pinyin = 'r';
                                                                                        else
                                                                                            if (i < 0xCBFA) pinyin = 's';
                                                                                            else
                                                                                                if (i < 0xCDDA) pinyin = 't';
                                                                                                else
                                                                                                    if (i < 0xCEF4) pinyin = 'w';
                                                                                                    else
                                                                                                        if (i < 0xD1B9) pinyin = 'x';
                                                                                                        else
                                                                                                            if (i < 0xD4D1) pinyin = 'y';
                                                                                                            else
                                                                                                                if (i < 0xD7FA) pinyin = 'z';
                    #endregion
                }

                sb.Append(pinyin);
            }

            return sb.ToString();

        }
        #endregion

        #region Request
        /// </summary>
        /// <param name="filedName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int Request(string filedName, int defaultValue)
        {
            return Request(filedName, defaultValue, false);
        }
        #endregion

        #region 获取URL指定参数,如果参数不存在或者为空就返回默认值
        /// <summary>
        /// 获取URL指定参数,如果参数不存在或者为空就返回默认值
        /// </summary>
        /// <param name="filedName">请求KEY</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="isDecrypt">是否需要解密</param>
        /// <returns></returns>
        public static int Request(string filedName, int defaultValue, bool isDecrypt)
        {
            string str = HttpContext.Current.Request[filedName];
            if (str != null && str.Trim() != "")
            {
                return int.Parse(str);
            }

            return defaultValue;
        }
        #endregion

        #region 获取URL指定参数,如果参数不存在或者为空就抛出错误信息
        /// <summary>
        /// 获取URL指定参数,如果参数不存在或者为空就抛出错误信息
        /// </summary>
        /// <param name="filedName"></param>
        /// <returns></returns>
        public static int Request(string filedName)
        {
            return Request(filedName, false);
        }
        #endregion

        #region 获取URL指定参数,如果参数不存在或者为空就抛出错误信息
        /// <summary>
        /// 获取URL指定参数,如果参数不存在或者为空就抛出错误信息
        /// </summary>
        /// <param name="filedName"></param>
        /// <param name="isDecrypt">是否需要解密</param>
        /// <returns></returns>
        public static int Request(string filedName, bool isDecrypt)
        {
            int i = Request(filedName, -1, isDecrypt);
            if (i == -1)
            {
                throw new Exception("找不到指定参数：" + filedName);
            }

            return i;
        }
        #endregion

        #region 通过网址得到域名
        /// <summary>
        /// 通过网址得到域名
        /// </summary>
        /// <param name="url">w网址</param>
        /// <returns></returns>
        public static string GetDomain(string url)
        {
            string host;
            Uri uri;
            try
            {

                uri = new Uri(url);
                host = uri.Host + " ";
            }
            catch
            {
                return "";
            }

            string[] BeReplacedStrs = new string[] { ".com.cn", ".edu.cn", ".net.cn", ".org.cn", ".co.jp", ".gov.cn", ".co.uk", "ac.cn", ".edu", ".tv", ".info", ".com", ".ac", ".ag", ".am", ".at", ".be", ".biz", ".bz", ".cc", ".cn", ".com", ".de", ".es", ".eu", ".fm", ".gs", ".hk", ".in", ".info", ".io", ".it", ".jp", ".la", ".md", ".ms", ".name", ".net", ".nl", ".nu", ".org", ".pl", ".ru", ".sc", ".se", ".sg", ".sh", ".tc", ".tk", ".tv", ".tw", ".us", ".co", ".uk", ".vc", ".vg", ".ws", ".il", ".li", ".nz" };

            foreach (string oneBeReplacedStr in BeReplacedStrs)
            {
                string BeReplacedStr = oneBeReplacedStr + " ";
                if (host.IndexOf(BeReplacedStr) != -1)
                {
                    host = host.Replace(BeReplacedStr, string.Empty);
                    break;
                }
            }

            int dotIndex = host.LastIndexOf(".");
            host = uri.Host.Substring(dotIndex + 1);
            return host;
        }
        #endregion

        #region byte[]转换string

        /// <summary>
        /// byte[]转换string
        /// </summary>
        /// <param name="Byte"></param>
        /// <returns></returns>
        public static string ByteToStr(byte[] inputBytes)
        {
            System.Text.UnicodeEncoding converter = new System.Text.UnicodeEncoding();
            string inputString = converter.GetString(inputBytes);
            return inputString;
        }
        #endregion

        #region String转换byte[]
        public static byte[] StrToByte(string inputString)
        {
            System.Text.UnicodeEncoding converter = new System.Text.UnicodeEncoding();
            byte[] inputBytes = converter.GetBytes(inputString);
            return inputBytes;
        }
        #endregion

        #region 从字符串中去掉重复的字符串
        /// <summary>
        /// 从字符串中去掉重复的字符串
        /// </summary>
        /// <param name="OldArrayList"></param>
        /// <returns></returns>
        public static string GetNewArrayList(string OldArrayList)
        {
            StringBuilder NewArrayList = new StringBuilder();

            List<string> listString = new List<string>();
            foreach (string eachString in OldArrayList.Split(','))
            {
                if (!listString.Contains(eachString))
                    listString.Add(eachString);
            }
            //最后从List里取出各个字符串进行操作 
            foreach (string eachString in listString)
            {
                NewArrayList.Append(eachString);
                NewArrayList.Append(",");
            }
            return NewArrayList.ToString().Substring(0, NewArrayList.Length - 1);
        }
        #endregion

        #region SHA1加密
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string GetMD5(string strSource)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "MD5");
        }
        #endregion

        public static string GetPercentage(string min, string max)
        {
            decimal dMin = Convert.ToDecimal(min);
            decimal dMax = Convert.ToDecimal(max);
            if (dMax == 0)
                dMax = 1;
            decimal p = (dMin / dMax) * 100;
            string strP = String.Format("{0:0.00}", p);
            return strP + "%";
        }

        #region 过滤匹配字符串
        /// <summary>
        /// 过滤匹配字符串
        /// </summary>
        /// <param name="regexstr">正则表达式</param>
        /// <param name="codestr">HTML/CODE/STRING</param>
        /// <returns></returns>
        public static string Reg(string regexstr, string codestr, int index)
        {
            Regex VIEWSTATERegex = new Regex(regexstr);
            MatchCollection VIEWSTATEMatchResult = VIEWSTATERegex.Matches(codestr);
            string regstr = "";
            foreach (Match vmr in VIEWSTATEMatchResult)
            {
                regstr = vmr.Groups[index].Value.ToString();
            }
            return regstr;
        }
        #endregion
        /// <summary>   

        /// 根据url获取网站HTML内容   

        /// </summary>    
        /// <param name="url">网址</param>
        /// <param name="url">编码</param>
        /// <returns>获取网站HTML内容</returns>   

        public static string GetHtmlContentByUrl(string url, string encode)
        {

            var htmlContent = string.Empty;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.Timeout = 10000000;

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            var stream = httpWebResponse.GetResponseStream();

            if (stream != null)
            {

                var streamReader = new StreamReader(stream, Encoding.GetEncoding(encode));

                htmlContent = streamReader.ReadToEnd();

                streamReader.Close();

                streamReader.Dispose();

                stream.Close();

                stream.Dispose();

            }

            httpWebResponse.Close();

            return htmlContent;

        }

        #region 获取城市
        /// <summary>
        /// 获取城市
        /// </summary>
        /// <returns></returns>
        public static string GetCityAddress(string ip)
        {
            //ip = "117.89.35.0";

            //获取IP地址
            string IpString = StringHelper.GetHtmlContentByUrl("http://iplookup.dangdang.com/?ip=" + ip, "utf-8")
              .Replace("\n", "");
            string[] list = IpString.Split('\t');
            return list[5].Trim();

        }
        #endregion
        #region 获取IP地址
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIpAddress()
        {
            //获取IP地址
            string IpString = StringHelper.GetHtmlContentByUrl("http://iframe.ip138.com/ic.asp", "gb2312");
            string regcityString = @"\[(.*)\]";
            string IpAddress = StringHelper.Reg(regcityString, IpString, 1);
            return IpAddress;
        }
        #endregion


        #region 获取当天星期农历
        /// <summary>
        /// 获取当天星期农历
        /// </summary>
        /// <returns></returns>
        public static string GetDataString()
        {
            string dataString = StringHelper.GetHtmlContentByUrl("http://www.nongli114.com/", "gb2312");

            //获取当天星期几
            //string regString = @"</b> ([^)]*?)</td>";
            //string data = StringHelper.Reg(regString, dataString, 1);


            //获取当天农历
            string regAString = @"<div id=""span_select_lunar_month"">([^)]*?)</div>";
            string dataA = StringHelper.Reg(regAString, dataString, 1);

            string regBString = @"<div id=""span_select_lunar_day"">([^)]*?)</div>";
            string dataB = StringHelper.Reg(regBString, dataString, 1);
            string dataForString = " " + "农历" + dataA + "月" + dataB + "日";
            //string dataForString = data+" "+"农历" + dataA + "月" + dataB + "日";
            return dataForString;
        }
        #endregion

        #region 获取地方天气
        /// <summary>
        /// 获取地方天气
        /// </summary>
        /// <returns></returns>
        public static string GetWeatherString()
        {
            //获取地方天气

            //string weatherAString = StringHelper.GetHtmlContentByUrl("http://61.4.185.48:81/g/","gb2312");
            //string weatherReg = @"id=([^)]*?);if";
            //string weatherID = StringHelper.Reg(weatherReg, weatherAString, 1);
            string weatherID = "101200101";//获取武汉天气
            string weatherBString = StringHelper.GetHtmlContentByUrl("http://m.weather.com.cn/data/" + weatherID + ".html", "utf-8");
            string cityReg = @"""city"":""([^)]*?)""";
            string cityString = StringHelper.Reg(cityReg, weatherBString, 1);

            string weather1Reg = @"""weather1"":""([^)]*?)""";
            string weather1String = StringHelper.Reg(weather1Reg, weatherBString, 1);

            string weekReg = @"""week"":""([^)]*?)""";
            string weekString = StringHelper.Reg(weekReg, weatherBString, 1);

            string temp1Reg = @"""temp1"":""([^)]*?)""";
            string temp1String = StringHelper.Reg(temp1Reg, weatherBString, 1);
            string weatherString = weekString + " " + cityString + weather1String + temp1String;
            return weatherString;
        }
        #endregion


    }
}