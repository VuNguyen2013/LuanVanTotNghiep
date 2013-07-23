using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Web;

/*
 * 
 *	SAMPLE
	Declare a HttpUrlQuery object, and instantiate it with contents of current page's querystring. Note that this instantiation can also be done from a variety of sources, including a NameValueCollection, or another HttpUrlQuery object.

		HttpUrlQuery qs = new HttpUrlQuery(Page.Request.QueryString);


	Remove an unneed field. No errors are caused if the field doesn't exist. If you need to validate an existance of a field in the querystring, the Contains() method can be used.

		qs.Remove("name");


	Setting the value of a field is quite easy. If the field does not exist in the query string, it is appended to the end. Otherwise, its value is simply modified to the new value. This example also demonstrates encoding abilities of the library. If you do not desire the value to be encoded, an overload of the Set() method exists that allows you to do that.

		qs.Set("name", "George Vorgen-Peterson");


	The setting task can also be accomplished using the following, more programmer-friendly syntax. The same rules of the Set method apply.

		qs["equation"] = "2 + 2 - 3 = 1";


	Call the ToString() method of the object to get a string representation, which is preceeded by the ? sign so it can be readily appended without any clumsy checks.

		Response.Redirect("page.aspx" + qs.ToString();


	Comments, questions, and suggestions are welcome. Enjoy.
 * */
namespace OTS.WebLib.host
{
	/// <summary>
	/// Class for easier manipulation of standard Http querystrings
	/// </summary>
	public class HttpUrlQuery
	{
		private string start = "?";
		private string separator_pairs = "&";
		private string separator_values = "=";

		private string original = "";

		private NameValueCollection qs;

		/// <summary>
		/// Default constructor
		/// </summary>
		public HttpUrlQuery()
		{
			qs = new NameValueCollection(0);
		}

		/// <summary>
		/// Constructor from a string that already contains a querystring. No url encoding is done. Standard separation characters are used.
		/// </summary>
		/// <param name="QueryString">Querystring string source</param>
		public HttpUrlQuery(string QueryString)
			: this(QueryString, false)
		{
		}


		/// <summary>
		/// Constructor from a string that already contains a querystring. Standard separation characters are used.
		/// </summary>
		/// <param name="QueryString">Querystring string source</param>
		/// <param name="UrlEncode">Whether or not standard Http url encoding should be performed on the values</param>
		public HttpUrlQuery(string QueryString, bool UrlEncode)
			: this(QueryString, UrlEncode, "?", "&", ":")
		{
		}

		/// <summary>
		/// Constructor from a string that already contains a querystring. Custom separation characters can be used.
		/// </summary>
		/// <param name="QueryString">Querystring string source</param>
		/// <param name="UrlEncode">Whether or not standard Http url encoding should be performed on the values</param>
		/// <param name="StartString">String that appears in the front of the query string. Used in the ToString() output.</param>
		/// <param name="SeparatorPairsString">String that appears between pairs of the query string. Used in the ToString() output.</param>
		/// <param name="SeparatorNameValueString">String that appears between name and value in each pair of the query string. Used in the ToString() output.</param>
		public HttpUrlQuery(string QueryString, bool UrlEncode, string StartString, string SeparatorPairsString, string SeparatorNameValueString)
		{
			original = QueryString;

			start = StartString;
			separator_pairs = SeparatorPairsString;
			separator_values = SeparatorNameValueString;

			QueryString = QueryString.Replace(start, "");

			string[] parts = Regex.Split(QueryString, separator_pairs);

			qs = new NameValueCollection(parts.Length);

			string[] subparts;

			foreach (string part in parts)
			{
				subparts = Regex.Split(part, separator_values);

				if (subparts.Length == 0)
					Set(subparts[0], "");

				else if (subparts.Length > 0)
					Set(subparts[0], subparts[1], UrlEncode);
			}

		}

		/// <summary>
		/// Constructor from another HttpUrlQuery object. Deep copy is performed to make sure the new object becomes completely independent of its source.
		/// </summary>
		/// <param name="Source">Another object whose state will be used to create this new object.</param>
		public HttpUrlQuery(HttpUrlQuery Source)
		{
			original = Source.ToString();

			start = Source.StartString;
			separator_pairs = Source.SeparatorPairsString;
			separator_values = Source.SeparatorNameValueString;

			string[] keys = Source.AllKeys;

			qs = new NameValueCollection(keys.Length);

			foreach (string key in keys)
				Set(key, Source.Get(key));

		}

		/// <summary>
		/// Constructor from a NameValueCollection, such as one usually found in System.Web.UI.Page.Request.Querystring objects. No url encoding is done.
		/// </summary>
		/// <param name="QueryString">Querystring collection source</param>
		public HttpUrlQuery(NameValueCollection QueryString)
			: this(QueryString, false)
		{
		}


		/// <summary>
		/// Constructor from a NameValueCollection, such as one usually found in System.Web.UI.Page.Request.Querystring objects. 
		/// </summary>
		/// <param name="QueryString">Querystring collection source</param>
		/// <param name="UrlEncode">Whether or not standard Http url encoding should be performed on the values</param>
		public HttpUrlQuery(NameValueCollection QueryString, bool UrlEncode)
		{
			qs = new NameValueCollection(QueryString.Count);

			// copy each pair
			foreach (string q in QueryString)
				Set(q, QueryString.Get(q), UrlEncode);

			original = ToString();

		}

		/// <summary>
		/// Removes pair with given name.
		/// </summary>
		/// <param name="Name">Name of the pair to be removed.</param>
		public void Remove(string Name)
		{
			qs.Remove(Name);
		}

		/// <summary>
		/// Determines whether pair with given name exists in this query string. Case insensitive.
		/// </summary>
		/// <param name="Name">Name of the desired pair.</param>
		/// <returns>Whether or not given pair exists in this query string.</returns>
		public bool Contains(string Name)
		{
			Name = Name.ToLower();

			foreach (string x in qs.Keys)
				if (x.ToLower() == Name)
					return true;

			return false;
		}

		/// <summary>
		/// Sets the value of the pair with given name. Adds new pair, if doesn't already exist. Url encoding is performed on NewValue.
		/// </summary>
		/// <param name="Name">Name of the desired pair.</param>
		/// <param name="NewValue">New value to be assigned to this pair. Will be forced to undergo url encoding.</param>
		public void Set(string Name, string NewValue)
		{
			Set(Name, NewValue, true);
		}


		/// <summary>
		/// Retreives the value of the pair with given name.
		/// </summary>
		/// <param name="Name">Name of the desired pair.</param>
		public string Get(string Name)
		{
			return (Contains(Name) ? qs.Get(Name) : "");

		}

		/// <summary>
		/// Returns all keys in this query string
		/// </summary>
		public string[] AllKeys
		{
			get
			{
				return qs.AllKeys;
			}

		}

		/// <summary>
		/// Allows for square-bracket referencing of elements
		/// </summary>
		public string this[string Name]
		{
			get
			{
				return Get(Name);
			}

			set
			{
				Set(Name, value);
			}

		}

		/// <summary>
		/// Sets the value of the pair with given name. Adds new pair, if doesn't already exist. No url encoding is done.
		/// </summary>
		/// <param name="Name">Name of the desired pair.</param>
		/// <param name="NewValue">New value to be assigned to this pair.</param>
		/// <param name="UrlEncode">Whether or not standard Http url encoding should be performed on the values</param>
		public void Set(string Name, string NewValue, bool UrlEncode)
		{
			if (UrlEncode)
				NewValue = System.Web.HttpContext.Current.Server.UrlEncode(NewValue);

			if (Contains(Name))
				qs.Set(Name, NewValue);
			else
				qs.Add(Name, NewValue);
		}

		/// <summary>
		/// Defines the string that preceeds the actual pairs in the string output of this query string. "?" by default.
		/// </summary>
		public string StartString
		{
			get
			{
				return start;
			}

			set
			{
				start = value;
			}
		}

		/// <summary>
		/// Defines the string that separates pairs. "&amp;" by default.
		/// </summary>
		public string SeparatorPairsString
		{
			get
			{
				return separator_pairs;
			}

			set
			{
				separator_pairs = value;
			}
		}

		/// <summary>
		/// Defines the string that separates name from value within a pair. "=" by default.
		/// </summary>
		public string SeparatorNameValueString
		{
			get
			{
				return separator_values;
			}

			set
			{
				separator_values = value;
			}
		}

		/// <summary>
		/// An instance of the querystring object that is based on the querystring in the current context
		/// </summary>
		public static HttpUrlQuery Current
		{
			get
			{
				return new HttpUrlQuery(HttpContext.Current.Request.QueryString);
			}
		}

		/// <summary>
		/// Represenation of the original string which was used to initialize this object
		/// </summary>
		public string OriginalString
		{
			get
			{
				return original;
			}
		}


		/// <summary>
		/// String representation of contained querystring.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (qs.Count < 1)
				return "";

			StringBuilder b = new StringBuilder();

			b.Append(start);

			string s = "";

			for (int i = 0; i < qs.Count; i++)
			{
				s = qs.Keys[i];

				b.Append(s);
				b.Append(separator_values);
				b.Append(qs.Get(s));

				if (i < (qs.Count - 1))
					b.Append(separator_pairs);

			}

			return b.ToString();
		}
	}
}
