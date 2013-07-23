using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OTS.WebLib.text
{
	/// <summary>
	/// Text utilities
	/// Innotech
	/// </summary>
	public class TextUtil
	{
		public TextUtil() { }

		/// <summary>
		/// drop text fix to specific lenghth		
		/// Could use for HTML also
		/// </summary>
		/// <param name="fullText"></param>
		/// <param name="numberOfCharacters"></param>
		/// <returns></returns>
		public static string dropTextFixLength(string fullText, int numberOfCharacters)
		{
			string text;
			if (fullText.Length > numberOfCharacters)
			{
				int spacePos = fullText.IndexOf(" ", numberOfCharacters);
				if (spacePos > -1)
				{
					text = fullText.Substring(0, spacePos).Trim() + "...";
				}
				else
				{
                    text = fullText.Substring(0, numberOfCharacters).Trim() + "...";
				}
			}
			else
			{
				text = fullText;
			}

			Regex regexStripHTML = new Regex("<[^>]+>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			text = regexStripHTML.Replace(text, " ");
			return text;
		}

		/// <summary>
		/// Left of the first occurance of c
		/// </summary>
		/// <param name="src">The source string.</param>
		/// <param name="c">Return everything to the left of this character.</param>
		/// <returns>String to the left of c, or the entire string.</returns>
		public static string LeftOf(string src, char c)
		{
			string ret=src;
			int idx=src.IndexOf(c);
			if (idx != -1)
			{
				ret=src.Substring(0, idx);
			}
			return ret;
		}

		/// <summary>
		/// Left of the n'th occurance of c.
		/// </summary>
		/// <param name="src">The source string.</param>
		/// <param name="c">Return everything to the left n'th occurance of this character.</param>
		/// <param name="n">The occurance.</param>
		/// <returns>String to the left of c, or the entire string if not found or n is 0.</returns>
		public static string LeftOf(string src, char c, int n)
		{
			string ret=src;
			int idx=-1;
			while (n > 0)
			{
				idx=src.IndexOf(c, idx+1);
				if (idx==-1)
				{
					break;
				}
				--n;
			}
			if (idx != -1)
			{
				ret=src.Substring(0, idx);
			}
			return ret;
		}

		/// <summary>
		/// Right of the first occurance of c
		/// </summary>
		/// <param name="src">The source string.</param>
		/// <param name="c">The search char.</param>
		/// <returns>Returns everything to the right of c, or an empty string if c is not found.</returns>
		public static string RightOf(string src, char c)
		{
			string ret=String.Empty;
			int idx=src.IndexOf(c);
			if (idx != -1)
			{
				ret=src.Substring(idx+1);
			}
			return ret;
		}

		/// <summary>
		/// Right of the n'th occurance of c
		/// </summary>
		/// <param name="src">The source string.</param>
		/// <param name="c">The search char.</param>
		/// <param name="n">The occurance.</param>
		/// <returns>Returns everything to the right of c, or an empty string if c is not found.</returns>
		public static string RightOf(string src, char c, int n)
		{
			string ret=String.Empty;
			int idx=-1;
			while (n > 0)
			{
				idx=src.IndexOf(c, idx+1);
				if (idx==-1)
				{
					break;
				}
				--n;
			}

			if (idx != -1)
			{
				ret=src.Substring(idx+1);
			}

			return ret;
		}

		/// <summary>
		/// Returns everything to the left of the righmost char c.
		/// </summary>
		/// <param name="src">The source string.</param>
		/// <param name="c">The search char.</param>
		/// <returns>Everything to the left of the rightmost char c, or the entire string.</returns>
		public static string LeftOfRightmostOf(string src, char c)
		{
			string ret=src;
			int idx=src.LastIndexOf(c);
			if (idx != -1)
			{
				ret=src.Substring(0, idx);
			}
			return ret;
		}

		/// <summary>
		/// Returns everything to the right of the rightmost char c.
		/// </summary>
		/// <param name="src">The source string.</param>
		/// <param name="c">The seach char.</param>
		/// <returns>Returns everything to the right of the rightmost search char, or an empty string.</returns>
		public static string RightOfRightmostOf(string src, char c)
		{
			string ret=String.Empty;
			int idx=src.LastIndexOf(c);
			if (idx != -1)
			{
				ret=src.Substring(idx+1);
			}
			return ret;
		}

		/// <summary>
		/// Returns everything between the start and end chars, exclusive.
		/// </summary>
		/// <param name="src">The source string.</param>
		/// <param name="start">The first char to find.</param>
		/// <param name="end">The end char to find.</param>
		/// <returns>The string between the start and stop chars, or an empty string if not found.</returns>
		public static string Between(string src, char start, char end)
		{
			string ret=String.Empty;
			int idxStart=src.IndexOf(start);
			if (idxStart != -1)
			{
				++idxStart;
				int idxEnd=src.IndexOf(end, idxStart);
				if (idxEnd != -1)
				{
					ret=src.Substring(idxStart, idxEnd-idxStart);
				}
			}
			return ret;
		}

		/// <summary>
		/// Returns the number of occurances of "find".
		/// </summary>
		/// <param name="src">The source string.</param>
		/// <param name="find">The search char.</param>
		/// <returns>The # of times the char occurs in the search string.</returns>
		public static int Count(string src, char find)
		{
			int ret=0;
			foreach(char s in src)
			{
				if (s==find)
				{
					++ret;
				}
			}
			return ret;
		}

		/// <summary>
		/// Returns the rightmost char in src.
		/// </summary>
		/// <param name="src">The source string.</param>
		/// <returns>The rightmost char, or '\0' if the source has zero length.</returns>
		public static char Rightmost(string src)
		{
			char c='\0';
			if (src.Length>0)
			{
				c=src[src.Length-1];
			}
			return c;
		}

	}
}


