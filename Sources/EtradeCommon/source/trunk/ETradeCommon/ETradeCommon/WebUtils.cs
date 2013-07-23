using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace ETradeCommon
{
    public class WebUtils
    {
        /// <summary>
        /// Create a web request
        /// </summary>
        /// <param name="address"></param>
        /// <param name="cookies"></param>
        /// <param name="methodType"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static HttpWebRequest CreateWebRequest(string address, string cookies, int methodType, string postData)
        {
            var webrequest = (HttpWebRequest)WebRequest.Create(address);
            int timeout = int.Parse(ConfigurationManager.AppSettings["Timeout"]);
            webrequest.Timeout = timeout;// set time out 500 ms

            // Decompression content
            webrequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            if (!string.IsNullOrEmpty(cookies))
            {
                webrequest.Headers.Add("Cookie", cookies);
            }
            webrequest.Headers.Add("Accept-Language", "en-us,en;q=0.5");
            webrequest.Headers.Add("Accept-Encoding", "gzip,deflate");
            webrequest.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
            webrequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.3) Gecko/20100403 Firefox/3.6.3 (Palemoon/3.6.3)";

            webrequest.AllowWriteStreamBuffering = false;
            if (methodType == 0)
            {
                webrequest.Method = WebRequestMethods.Http.Get;
            }
            else
            {
                webrequest.Method = WebRequestMethods.Http.Post;
                webrequest.ContentType = "application/x-www-form-urlencoded";
                var encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(postData);

                webrequest.ContentLength = postData.Length;
                Stream stream = webrequest.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
            return webrequest;
        }
    }
}
