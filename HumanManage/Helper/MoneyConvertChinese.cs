using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanManage.Helpers
{
    public static class MoneyConvertChinese
    {
        /// <summary>金额转大写    
        ///     
        /// </summary>    
        /// <param name="LowerMoney"></param>    
        /// <returns></returns>    
        public static string MoneyToChinese(string LowerMoney)
        {
            string functionReturnValue = null;
            bool IsNegative = false; // 是否是负数  
            if (LowerMoney.Trim().Substring(0, 1) == "-")
            {
                // 是负数则先转为正数    
                LowerMoney = LowerMoney.Trim().Remove(0, 1);
                IsNegative = true;
            }
            if (LowerMoney.Count() > 10)
            {
                functionReturnValue = "报账金额超过了指定的范围";
            }
            else
            {
                string strLower = null;
                string strUpart = null;
                string strUpper = null;
                int iTemp = 0;
                // 保留两位小数 123.489→123.49　　123.4→123.4    
                LowerMoney = Math.Round(double.Parse(LowerMoney), 2).ToString().PadLeft(5, '0');
                if (LowerMoney.IndexOf(".") > 0)
                {
                    if (LowerMoney.IndexOf(".") == LowerMoney.Length - 2)
                    {
                        LowerMoney = LowerMoney + "0";
                    }
                }
                else
                {
                    LowerMoney = LowerMoney + ".00";
                }
                strLower = LowerMoney;
                iTemp = 1;
                strUpper = "";
                while (iTemp <= strLower.Length)
                {
                    switch (strLower.Substring(strLower.Length - iTemp, 1))
                    {
                        case ".":
                            strUpart = "圆";
                            break;
                        case "0":
                            strUpart = "<span>零</span>";
                            break;
                        case "1":
                            strUpart = "<span>壹</span>";
                            break;
                        case "2":
                            strUpart = "<span>贰</span>";
                            break;
                        case "3":
                            strUpart = "<span>叁</span>";
                            break;
                        case "4":
                            strUpart = "<span>肆</span>";
                            break;
                        case "5":
                            strUpart = "<span>伍</span>";
                            break;
                        case "6":
                            strUpart = "<span>陆</span>";
                            break;
                        case "7":
                            strUpart = "<span>柒</span>";
                            break;
                        case "8":
                            strUpart = "<span>捌</span>";
                            break;
                        case "9":
                            strUpart = "<span>玖</span>";
                            break;
                    }

                    switch (iTemp)
                    {
                        case 1:
                            strUpart = strUpart + "分";
                            break;
                        case 2:
                            strUpart = strUpart + "角";
                            break;
                        case 3:
                            strUpart = strUpart + "";
                            break;
                        case 4:
                            strUpart = strUpart + "";
                            break;
                        case 5:
                            strUpart = strUpart + "拾";
                            break;
                        case 6:
                            strUpart = strUpart + "佰";
                            break;
                        case 7:
                            strUpart = strUpart + "仟";
                            break;
                        case 8:
                            strUpart = strUpart + "万";
                            break;
                        case 9:
                            strUpart = strUpart + "拾";
                            break;
                        case 10:
                            strUpart = strUpart + "佰";
                            break;
                        case 11:
                            strUpart = strUpart + "仟";
                            break;
                        case 12:
                            strUpart = strUpart + "亿";
                            break;
                        case 13:
                            strUpart = strUpart + "拾";
                            break;
                        case 14:
                            strUpart = strUpart + "佰";
                            break;
                        case 15:
                            strUpart = strUpart + "仟";
                            break;
                        case 16:
                            strUpart = strUpart + "万";
                            break;
                        default:
                            strUpart = strUpart + "";
                            break;
                    }

                    strUpper = strUpart + strUpper;
                    iTemp = iTemp + 1;
                }                
                
                functionReturnValue = strUpper;
            }

            if (IsNegative == true)
            {
                return "负" + functionReturnValue;
            }
            else
            {
                return functionReturnValue;
            }
        }


        public static string MoneyToChinese(string LowerMoney, int index)
        {
            string functionReturnValue = null;
            string strUpart = null;
            string strUpper = null;

            switch (LowerMoney.Substring(LowerMoney.Length - index, 1))
            {
                case ".":
                    strUpper = "圆";
                    break;
                case "0":
                    strUpper = "零";
                    break;
                case "1":
                    strUpper = "壹";
                    break;
                case "2":
                    strUpper = "贰";
                    break;
                case "3":
                    strUpper = "叁";
                    break;
                case "4":
                    strUpper = "肆";
                    break;
                case "5":
                    strUpper = "伍";
                    break;
                case "6":
                    strUpart = "陆";
                    break;
                case "7":
                    strUpper = "柒";
                    break;
                case "8":
                    strUpper = "捌";
                    break;
                case "9":
                    strUpper = "玖";
                    break;
            }

            switch (index)
            {
                case -2:
                    strUpart = strUpart + "分";
                    break;
                case -1:
                    strUpart = strUpart + "角";
                    break;
                case 1:
                    strUpart = strUpart + "元";
                    break;
                case 2:
                    strUpart = strUpart + "拾";
                    break;
                case 3:
                    strUpart = strUpart + "佰";
                    break;
                case 4:
                    strUpart = strUpart + "仟";
                    break;
                case 5:
                    strUpart = strUpart + "万";
                    break;
            }
            functionReturnValue = strUpper + strUpart;
            return functionReturnValue;
        }
    }

}