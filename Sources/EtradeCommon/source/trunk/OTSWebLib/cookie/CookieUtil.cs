using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using OTS.WebLib.encryption;

namespace OTS.WebLib.cookie
{
    /// <summary>
    /// Cookie tool
    /// Innotech
    /// </summary>
    public class CookieUtil
    {
        /// <summary>
        /// write cookie with encryption
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        /// <param name="expiredDays"></param>
        /// <returns></returns>
        public static void writeCookieWithEncrypt(string cookieName, string cookieValue, int expiredMins)
        {
            MEncryption encryptTool = new MEncryption();
            HttpContext.Current.Response.Cookies[cookieName].Value = encryptTool.encrypt(cookieValue, "/^_&qn0Q");
            HttpContext.Current.Response.Cookies[cookieName].Expires = DateTime.Now.AddMinutes(expiredMins);
        }

        /// <summary>
        /// read encrypted cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string readEncryptedCookie(string cookieName)
        {
            MEncryption encryptTool = new MEncryption();
    
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
                   return encryptTool.decrypt(HttpContext.Current.Request.Cookies[cookieName].Value, "/^_&qn0Q"); 

            return string.Empty;
        }

        /// <summary>
        /// Doc cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string readCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
                return HttpContext.Current.Request.Cookies[cookieName].Value;

            return string.Empty;
        }

        /// <summary>
        /// ghi cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static void writeCookie(string cookieName, string cookieValue, int expiredMins)
        {
            HttpContext.Current.Response.Cookies[cookieName].Value = cookieValue;
            HttpContext.Current.Response.Cookies[cookieName].Expires = DateTime.Now.AddMinutes(expiredMins);
        }

        /// <summary>
        /// xoa cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static void deleteCookie(string cookieName)
        {
            HttpContext.Current.Response.Cookies[cookieName].Expires = DateTime.Now.AddYears(-30);
        }

        
    }
}
