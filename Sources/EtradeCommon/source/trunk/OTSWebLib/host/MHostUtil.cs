using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;

namespace OTS.WebLib.host
{
	/// <summary>
	/// Utilities on Host
	/// Innotech
	/// </summary>
	public class MHostUtil
	{
		private MHostUtil() { }

		/// <summary>
		/// get current url
		/// </summary>
		/// <returns></returns>
		public static string getCurrentUrl()
		{
			return HttpContext.Current.Request.Url.LocalPath;
		}

		/// <summary>
		/// duong dan tuong doi --> tuyet doi
		/// ~/xyz/file.tmp --> C:/dfjalkfa/fdafda/xyz/file.tmp
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static string getFullFileSystem(string filePath)
		{
			return HttpContext.Current.Server.MapPath(filePath);
		}

		/// <summary>
		/// Build ra link voi query string
		/// </summary>
		/// <param name="originalString">http://abc.com</param>
		/// <param name="queryStringVars">string[]{x, y}</param>
		/// <param name="queryStringValues">string[] {xVal, yVal}</param>
		/// <returns>http://abc.com?x=xVal&y=yVal</returns>
		public static string buildLinkWithQueryString(string originalString, string[] queryStringVars, string[] queryStringValues)
		{
			StringBuilder result = new StringBuilder("");

			if (string.IsNullOrEmpty(originalString) || queryStringValues == null ||
				queryStringVars == null || queryStringVars.Length != queryStringVars.Length ||
				originalString.IndexOf("?") != -1 || originalString.IndexOf("&") != -1)
			{

				return result.ToString();
			}
						

			for (int i = 0, size = queryStringVars.Length; i < size; i++)
			{
				result.AppendFormat("{0}={1}",
					encodeUrl(queryStringVars[i]),
					encodeUrl(queryStringValues[i]));

				if (i + 1 < size)
					result.Append("&");
			}

			result.Insert(0, originalString + "?");
			return result.ToString();
		}

		/// <summary>
		/// encode url
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string encodeUrl(string url)
		{
			return HttpContext.Current.Server.UrlEncode(url);
		}

		/// <summary>
		/// encode html
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		public static string encodeHtml(string content)
		{
			return HttpContext.Current.Server.HtmlEncode(content);
		}

		/// <summary>
		/// URL hien tai co' nhi`u querystring param
		/// Ta muon lay URL nhung drop di 1 so param
		/// </summary>
		/// <param name="url"></param>
		/// <param name="queryStringParam"></param>
		/// <returns></returns>
		public string getURLWithoutParam(ArrayList queryStringParam)
		{
			string currentURL = HttpContext.Current.Request.Url.LocalPath;
			bool bNeedQuestionMark = true;

			foreach (string param in HttpContext.Current.Request.QueryString.AllKeys)
			{
				if (!queryStringParam.Contains(param))
				{
					if (!bNeedQuestionMark)
					{
						currentURL += "&" + param + "=" + HttpContext.Current.Request.QueryString[param];
					}
					else
					{
						currentURL += "?" + param + "=" + HttpContext.Current.Request.QueryString[param];

						if (bNeedQuestionMark)
							bNeedQuestionMark = false;
					}
				}
			}
			return currentURL;
		}

		/// <summary>
		/// get request ip
		/// </summary>
		/// <returns></returns>
		public string getClientIP()
		{
			return HttpContext.Current.Request.UserHostAddress;
		}

	}
}
