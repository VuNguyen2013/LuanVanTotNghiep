using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace OTS.WebLib.cache
{
    /// <summary>
    /// Cache helper
    /// Innotech
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// cache object
        /// </summary>
        /// <param name="index"></param>
        /// <param name="content"></param>
        public static void cacheObject(System.Web.Caching.Cache Cache, string cacheKey, object content)
        {
            Cache[cacheKey] = content;
        }

        /// <summary>
        /// get cached object
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static object getCachedObject(System.Web.Caching.Cache Cache, string cacheKey)
        {
            return Cache[cacheKey];
        }


        /// <summary>
        /// enable cache
        /// To store the output cache for a specified duration
        /// </summary>
        public static void enableCache(System.Web.HttpResponse Response)
        {
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(60));
            Response.Cache.SetCacheability(HttpCacheability.Public);
        }

        /// <summary>
        /// Enable cache
        /// To store the output cache on the browser client where the request originated
        /// </summary>
        public static void enableCacheOnClientBrowser(System.Web.HttpResponse Response)
        {
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(60));
            Response.Cache.SetCacheability(HttpCacheability.Private);
        }

        /// <summary>
        ///To store the output cache on any HTTP 1.1 cache-capable devices including the proxy servers and the client that made request 
        /// </summary>
        public static void enableCacheOnHttp11(System.Web.HttpResponse Response)
        {
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(60));
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.Cache.SetNoServerCaching();
        }

        /// <summary>
        ///  To store the output cache on the Web server
        /// </summary>
        public static void enableCacheOnWebServer(System.Web.HttpResponse Response)
        {
            TimeSpan freshness = new TimeSpan(0, 0, 0, 60);
            DateTime now = DateTime.Now;
            Response.Cache.SetExpires(now.Add(freshness));
            Response.Cache.SetMaxAge(freshness);
            Response.Cache.SetCacheability(HttpCacheability.Server);
            Response.Cache.SetValidUntilExpires(true);
        }


        /// <summary>
        ///          To cache the output for each HTTP request that arrives with a different City:
        /// </summary>
        public static void enableCacheBaseOnVariables(System.Web.HttpResponse Response, string[] variables)
        {
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(60));
            Response.Cache.SetCacheability(HttpCacheability.Public);

            foreach (string variableName in variables)
            {
                Response.Cache.VaryByParams[variableName] = true;
            }
        }

        /// <summary>
        /// disable cache
        /// </summary>
        /// <param name="Response"></param>
        public static void disableCache(System.Web.HttpResponse Response)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            Response.Expires = 0;
            Response.CacheControl = "no-cache";

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetNoStore();
            Response.Cache.SetNoServerCaching();
            Response.Cache.SetExpires(DateTime.Now);  
        }

      }
}
