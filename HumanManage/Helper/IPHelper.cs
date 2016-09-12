using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace HumanManage.Helpers
{
    public class IPHelper
    {

        #region 私有成员
        private static string dataPath;
        private static string ip;
        private static string country;
        private static string local;

        private static long firstStartIp = 0;
        private static long lastStartIp = 0;

        private static FileStream objfs = null;

        private static long startIp = 0;
        private static long endIp = 0;
        private static int countryFlag = 0;
        private static long endIpOff = 0;
        private static string errMsg = null;
        #endregion

        //#region 构造函数
        //public IPScanner()
        //{
        //    //
        //    // TODO: 在此处添加构造函数逻辑
        //    //
        //}
        //#endregion

        //#region 公共属性
        public static string DataPath
        {
            set { dataPath = value; }
        }
        public static string IP
        {
            set { ip = value; }
        }
        public static string Country
        {
            get { return country; }
        }
        public static string Local
        {
            get { return local; }
        }
        public static string ErrMsg
        {
            get { return errMsg; }
        }
        //#endregion



        #region 搜索匹配数据
        private static int QQwry()
        {
            string pattern = @"(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))";
            Regex objRe = new Regex(pattern);
            Match objMa = objRe.Match(ip);
            if (!objMa.Success)
            {
                errMsg = "IP格式错误";
                return 4;
            }

            long ip_Int = IpToInt(ip);
            int nRet = 0;
            if (ip_Int >= IpToInt("127.0.0.0") && ip_Int <= IpToInt("127.255.255.255"))
            {
                country = "本机内部环回地址";
                local = "";
                nRet = 1;
            }
            else if ((ip_Int >= IpToInt("0.0.0.0") && ip_Int <= IpToInt("2.255.255.255")) || (ip_Int >= IpToInt("64.0.0.0") && ip_Int <= IpToInt("126.255.255.255")) || (ip_Int >= IpToInt("58.0.0.0") && ip_Int <= IpToInt("60.255.255.255")))
            {
                country = "网络保留地址";
                local = "";
                nRet = 1;
            }
            objfs = new FileStream(dataPath, FileMode.Open, FileAccess.Read);
            try
            {
                //objfs.Seek(0,SeekOrigin.Begin);
                objfs.Position = 0;
                byte[] buff = new Byte[8];
                objfs.Read(buff, 0, 8);
                firstStartIp = buff[0] + buff[1] * 256 + buff[2] * 256 * 256 + buff[3] * 256 * 256 * 256;
                lastStartIp = buff[4] * 1 + buff[5] * 256 + buff[6] * 256 * 256 + buff[7] * 256 * 256 * 256;
                long recordCount = Convert.ToInt64((lastStartIp - firstStartIp) / 7.0);
                if (recordCount <= 1)
                {
                    country = "FileDataError";
                    objfs.Close();
                    return 2;
                }
                long rangE = recordCount;
                long rangB = 0;
                long recNO = 0;
                while (rangB < rangE - 1)
                {
                    recNO = (rangE + rangB) / 2;
                    GetStartIp(recNO);
                    if (ip_Int == startIp)
                    {
                        rangB = recNO;
                        break;
                    }
                    if (ip_Int > startIp)
                        rangB = recNO;
                    else
                        rangE = recNO;
                }
                GetStartIp(rangB);
                GetEndIp();
                if (startIp <= ip_Int && endIp >= ip_Int)
                {
                    GetCountry();
                    local = local.Replace("（我们一定要解放台湾！！！）", "");
                }
                else
                {
                    nRet = 3;
                    country = "未知";
                    local = "";
                }
                objfs.Close();
                return nRet;
            }
            catch
            {
                return 1;
            }

        }
        #endregion

        #region IP地址转换成Int数据
        private static long IpToInt(string ip)
        {
            char[] dot = new char[] { '.' };
            string[] ipArr = ip.Split(dot);
            if (ipArr.Length == 3)
                ip = ip + ".0";
            ipArr = ip.Split(dot);

            long ip_Int = 0;
            long p1 = long.Parse(ipArr[0]) * 256 * 256 * 256;
            long p2 = long.Parse(ipArr[1]) * 256 * 256;
            long p3 = long.Parse(ipArr[2]) * 256;
            long p4 = long.Parse(ipArr[3]);
            ip_Int = p1 + p2 + p3 + p4;
            return ip_Int;
        }
        #endregion

        #region int转换成IP
        private static string IntToIP(long ip_Int)
        {
            long seg1 = (ip_Int & 0xff000000) >> 24;
            if (seg1 < 0)
                seg1 += 0x100;
            long seg2 = (ip_Int & 0x00ff0000) >> 16;
            if (seg2 < 0)
                seg2 += 0x100;
            long seg3 = (ip_Int & 0x0000ff00) >> 8;
            if (seg3 < 0)
                seg3 += 0x100;
            long seg4 = (ip_Int & 0x000000ff);
            if (seg4 < 0)
                seg4 += 0x100;
            string ip = seg1.ToString() + "." + seg2.ToString() + "." + seg3.ToString() + "." + seg4.ToString();

            return ip;
        }
        #endregion

        #region 获取起始IP范围
        private static long GetStartIp(long recNO)
        {
            long offSet = firstStartIp + recNO * 7;
            //objfs.Seek(offSet,SeekOrigin.Begin);
            objfs.Position = offSet;
            byte[] buff = new Byte[7];
            objfs.Read(buff, 0, 7);

            endIpOff = Convert.ToInt64(buff[4].ToString()) + Convert.ToInt64(buff[5].ToString()) * 256 + Convert.ToInt64(buff[6].ToString()) * 256 * 256;
            startIp = Convert.ToInt64(buff[0].ToString()) + Convert.ToInt64(buff[1].ToString()) * 256 + Convert.ToInt64(buff[2].ToString()) * 256 * 256 + Convert.ToInt64(buff[3].ToString()) * 256 * 256 * 256;
            return startIp;
        }
        #endregion

        #region 获取结束IP
        private static long GetEndIp()
        {
            //objfs.Seek(endIpOff,SeekOrigin.Begin);
            objfs.Position = endIpOff;
            byte[] buff = new Byte[5];
            objfs.Read(buff, 0, 5);
            endIp = Convert.ToInt64(buff[0].ToString()) + Convert.ToInt64(buff[1].ToString()) * 256 + Convert.ToInt64(buff[2].ToString()) * 256 * 256 + Convert.ToInt64(buff[3].ToString()) * 256 * 256 * 256;
            countryFlag = buff[4];
            return endIp;
        }
        #endregion

        #region 获取国家/区域偏移量
        private static string GetCountry()
        {
            switch (countryFlag)
            {
                case 1:
                case 2:
                    country = GetFlagStr(endIpOff + 4);
                    local = (1 == countryFlag) ? " " : GetFlagStr(endIpOff + 8);
                    break;
                default:
                    country = GetFlagStr(endIpOff + 4);
                    local = GetFlagStr(objfs.Position);
                    break;
            }
            return " ";
        }
        #endregion

        #region 获取国家/区域字符串
        private static string GetFlagStr(long offSet)
        {
            int flag = 0;
            byte[] buff = new Byte[3];
            while (1 == 1)
            {
                //objfs.Seek(offSet,SeekOrigin.Begin);
                objfs.Position = offSet;
                flag = objfs.ReadByte();
                if (flag == 1 || flag == 2)
                {
                    objfs.Read(buff, 0, 3);
                    if (flag == 2)
                    {
                        countryFlag = 2;
                        endIpOff = offSet - 4;
                    }
                    offSet = Convert.ToInt64(buff[0].ToString()) + Convert.ToInt64(buff[1].ToString()) * 256 + Convert.ToInt64(buff[2].ToString()) * 256 * 256;
                }
                else
                {
                    break;
                }
            }
            if (offSet < 12)
                return " ";
            objfs.Position = offSet;
            return GetStr();
        }
        #endregion

        #region GetStr
        private static string GetStr()
        {
            byte lowC = 0;
            byte upC = 0;
            string str = "";
            byte[] buff = new byte[2];
            while (1 == 1)
            {
                lowC = (Byte)objfs.ReadByte();
                if (lowC == 0)
                    break;
                if (lowC > 127)
                {
                    upC = (byte)objfs.ReadByte();
                    buff[0] = lowC;
                    buff[1] = upC;
                    System.Text.Encoding enc = System.Text.Encoding.GetEncoding("GB2312");
                    str += enc.GetString(buff);
                }
                else
                {
                    str += (char)lowC;
                }
            }
            return str;
        }
        #endregion

        #region 获取IP地址

        public static string IPLocation(string strdataPath, string strip)
        {
            ip = strip;
            dataPath = strdataPath;
            QQwry();
            return country;
        }

        #endregion

        #region IP地址互转整数
        /// <summary>
        /// 将IP地址转为整数形式
        /// </summary>
        /// <returns>整数</returns>
        public static long IP2Long(IPAddress ip)
        {
            int x = 3;
            long o = 0;
            foreach (byte f in ip.GetAddressBytes())
            {
                o += (long)f << 8 * x--;
            }
            return o;
        }
        /// <summary>
        /// 将整数转为IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        public static IPAddress Long2IP(long l)
        {
            byte[] b = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                b[3 - i] = (byte)(l >> 8 * i & 255);
            }
            return new IPAddress(b);
        }
        #endregion

        #region 获得用户IP
        /// <summary>
        /// 获得用户IP
        /// </summary>
        public static string GetUserIp
        {
            get
            {
                string ip;
                string[] temp;
                bool isErr = false;
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                else
                    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
                if (ip.Length > 15)
                    isErr = true;
                else
                {
                    temp = ip.Split('.');
                    if (temp.Length == 4)
                    {
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if (temp[i].Length > 3) isErr = true;
                        }
                    }
                    else
                        isErr = true;
                }

                if (isErr)
                    return "1.1.1.1";
                else
                    return ip;
            }
        }
        #endregion

        #region 格式化IP
        /// <summary>
        /// 格式化IP
        /// </summary>
        public static string FormatIp(string ipStr)
        {
            string[] temp = ipStr.Split('.');
            string format = "";
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].Length < 3) temp[i] = Convert.ToString("000" + temp[i]).Substring(Convert.ToString("000" + temp[i]).Length - 3, 3);
                format += temp[i].ToString();
            }
            return format;
        }
        #endregion
    }
}