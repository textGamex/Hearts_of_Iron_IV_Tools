using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts_of_Iron_IV_Tools
{
    public static class Tools
    {
        /// <summary>
        /// 金额转为大写金额
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
            string strLower = null;
            string strUpart = null;
            string strUpper = null;
            int iTemp = 0;
            // 保留两位小数 123.489→123.49　　123.4→123.4
            LowerMoney = Math.Round(double.Parse(LowerMoney), 2).ToString();
            if (LowerMoney.IndexOf(".") > 0)
            {
                if (LowerMoney.IndexOf(".") == LowerMoney.Length - 2)
                {
                    LowerMoney = LowerMoney + "0";
                }
            }
            else
            {
                LowerMoney += ".00";
            }
            strLower = LowerMoney;
            iTemp = 1;
            strUpper = string.Empty;
            while (iTemp <= strLower.Length)
            {
                switch (strLower.Substring(strLower.Length - iTemp, 1))
                {
                    case ".":
                        strUpart = string.Empty;
                        break;
                    case "0":
                        strUpart = "零";
                        break;
                    case "1":
                        strUpart = "一";
                        break;
                    case "2":
                        strUpart = "二";
                        break;
                    case "3":
                        strUpart = "三";
                        break;
                    case "4":
                        strUpart = "四";
                        break;
                    case "5":
                        strUpart = "五";
                        break;
                    case "6":
                        strUpart = "六";
                        break;
                    case "7":
                        strUpart = "七";
                        break;
                    case "8":
                        strUpart = "八";
                        break;
                    case "9":
                        strUpart = "九";
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
                        strUpart = strUpart + string.Empty;
                        break;
                    case 4:
                        strUpart = strUpart + string.Empty;
                        break;
                    case 5:
                        strUpart = strUpart + "十";
                        break;
                    case 6:
                        strUpart = strUpart + "百";
                        break;
                    case 7:
                        strUpart = strUpart + "千";
                        break;
                    case 8:
                        strUpart = strUpart + "万";
                        break;
                    case 9:
                        strUpart = strUpart + "十";
                        break;
                    case 10:
                        strUpart = strUpart + "百";
                        break;
                    case 11:
                        strUpart = strUpart + "千";
                        break;
                    case 12:
                        strUpart = strUpart + "亿";
                        break;
                    case 13:
                        strUpart = strUpart + "十";
                        break;
                    case 14:
                        strUpart = strUpart + "百";
                        break;
                    case 15:
                        strUpart = strUpart + "千";
                        break;
                    case 16:
                        strUpart = strUpart + "万";
                        break;
                    default:
                        strUpart = strUpart + string.Empty;
                        break;
                }

                strUpper += strUpper;
                iTemp += 1;
            }

            strUpper = strUpper.Replace("零十", "零");
            strUpper = strUpper.Replace("零百", "零");
            strUpper = strUpper.Replace("零千", "零");
            strUpper = strUpper.Replace("零零零", "零");
            strUpper = strUpper.Replace("零零", "零");
            strUpper = strUpper.Replace("零角零分", string.Empty);
            strUpper = strUpper.Replace("零分", string.Empty);
            strUpper = strUpper.Replace("零角", "零");
            strUpper = strUpper.Replace("零亿零万零", "亿");
            strUpper = strUpper.Replace("亿零万零", "亿");
            strUpper = strUpper.Replace("零亿零万", "亿");
            strUpper = strUpper.Replace("零万零", "万");
            strUpper = strUpper.Replace("零亿", "亿");
            strUpper = strUpper.Replace("零万", "万");
            strUpper = strUpper.Replace("零", string.Empty);
            strUpper = strUpper.Replace("零零", "零");

            if (strUpper.Substring(0, 1) == "零")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "角")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "分")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "整")
            {
                strUpper = "零";
            }
            functionReturnValue = strUpper;

            if (IsNegative == true)
            {
                return "负" + functionReturnValue;
            }
            else
            {
                return functionReturnValue;
            }
        }
    }
}
