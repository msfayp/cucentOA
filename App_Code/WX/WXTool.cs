using System;
using System.Xml;

using System.IO;
using System.Web;

namespace WX.Tool
{
    /*=========================== 微信开发工具类 ===========================*/
    /*========================= 开发时间2013-01-21 =========================*/
    /*=========================== 开发人员Human ===========================*/
    /*=========================== QQ：332345375 ===========================*/

    /// <summary>
    /// 微信请求类
    /// </summary>
    [Serializable]
    public class WXTool
    {
        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// 校验是否为数字
        /// </summary>
        /// <param name="str">要校验的字符串</param>
        /// <returns>是否为数字</returns>
        public static bool IsNum(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"(\d)$");
            //^[-]?\d+[.]?\d*$
            return reg1.IsMatch(str);
        }
        /// <summary>
        /// 校验是否为数字[支持小数]
        /// </summary>
        /// <param name="str">要校验的字符串</param>
        /// <returns>是否为数字</returns>
        public static bool IsNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[-]?(\d+\.?\d*|\.\d+)$");
            //^[-]?\d+[.]?\d*$
            return reg1.IsMatch(str);
        }

        /// <summary>
        /// 写日志(用于跟踪)
        /// </summary>
        public static void WriteLog(string strMemo)
        {
            string filename = System.Web.HttpContext.Current.Request.MapPath("logs/log.txt");
            if (!Directory.Exists(System.Web.HttpContext.Current.Request.MapPath("logs")))
                Directory.CreateDirectory("logs");
            StreamWriter sr = null;
            try
            {
                if (!File.Exists(filename))
                {
                    sr = File.CreateText(filename);
                }
                else
                {
                    sr = File.AppendText(filename);
                }
                sr.WriteLine(strMemo);
            }
            catch
            {
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
    }
}
