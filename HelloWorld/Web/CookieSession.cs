
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
 
namespace HelloWorld
{
    public class Cookie
    {
        /// <summary>
        /// 新建cookie对象
        /// </summary>
        /// <param name="iCookieName"></param>
        /// <param name="iCookieValue"></param>
        /// <returns></returns>
        public static HttpCookie CreateCookie(string iCookieName,string iCookieValue)
        {

            HttpCookie iCookie = new HttpCookie(iCookieName);
            iCookie.Value = iCookieValue;
            //
            DateTime dtNow = DateTime.Now;
            TimeSpan tsMinute = new TimeSpan(1, 1, 30, 0);
            iCookie.Expires = dtNow + tsMinute;
            //iCookie.Expires = DateTime.Now.AddMinutes(3);
            return iCookie;
        }

        /// <summary>
        /// 在Cookie中添加键值对
        /// </summary>
        /// <param name="iCookie"></param>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HttpCookie AddValue(HttpCookie iCookie, string keyName, string value)
        {
            if (iCookie == null)
            {
                return null;
            }
            else
            {
                iCookie[keyName] = value;
                iCookie.Expires = DateTime.Now.AddMinutes(3);
                return iCookie;
            }
        }

    }

    public class Session
    {
        /// <summary>
        /// 设置回话值
        /// </summary>
        /// <param name="iPage"></param>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        public static void SetSession(Page iPage, string keyName, object value)
        {
            iPage.Session[keyName] = value;
        }

        /// <summary>
        /// 获取回话内string值
        /// </summary>
        /// <param name="iPage"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static string GetSession(Page iPage, string keyName)
        {
            if (iPage.Session[keyName] != null)
            {
                return iPage.Session[keyName].ToString();
            }
            return null;
        }

        /// <summary>
        /// 获取回话objcet值
        /// </summary>
        /// <param name="iPage"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static object GetSessionObject(Page iPage, string keyName)
        {
            return iPage.Session[keyName];
        }

        /// <summary>
        /// 销毁回话
        /// </summary>
        /// <param name="iPage"></param>
        public static void RuinSession(Page iPage)
        {
            iPage.Session.Abandon();
        }

    }

}