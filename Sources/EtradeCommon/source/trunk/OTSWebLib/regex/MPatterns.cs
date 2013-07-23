using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OTS.WebLib.regex
{
	/// <summary>
    /// A utility class containing frequently used Regular Expression Patterns
    /// and two utility methods to see if a passed string conforms to the designated 
    /// pattern.
    /// </summary>
	public class MPatterns
	{

		public const string EMAIL = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";

		public const string US_ZIPCODE = @"^\d{5}$";
		public const string US_ZIPCODE_PLUS_FOUR = @"^\d{5}((-|\s)?\d{4})$";
		public const string US_ZIPCODE_PLUS_FOUR_OPTIONAL = @"^\d{5}((-|\s)?\d{4})?$";

		/// <summary>
		/// Permissive US Telephone Regex. Does not allow extensions.
		/// </summary>
		/// <example>
		/// Allows: 324-234-3433, 3242343434, (234)234-234, (234) 234-2343
		/// </example>
		public const string US_TELEPHONE = @"^([\(]{1}[0-9]{3}[\)]{1}[\.| |\-]{0,1}|^[0-9]{3}[\.|\-| ]?)?[0-9]{3}(\.|\-| )?[0-9]{4}$";

		/// <summary>
		/// This matches a url in the generic format 
		/// scheme://authority/path?query#fragment
		/// </summary>
		/// <example>
		/// Allows: http://www.yahoo.com, https://www.yahoo.com, ftp://www.yahoo.com
		/// </example>
		public const string URL = @"^(?<Protocol>\w+):\/\/(?<Domain>[\w.]+\/?)\S*$";

		/// <summary>
		/// This matches an ip address in the format xxx-xxx-xxx-xxx
		/// each group of xxx must be less than or equal to 255
		/// </summary>
		/// <example>
		/// Allows: 123.123.123.123, 192.168.1.1
		/// </example>
		public const string IP_ADDRESS = @"^(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)$";

		/// <summary>
		/// This matches a date in the format mm/dd/yy
		/// </summary>
		/// <example>
		/// Allows: 01/05/05, 12/30/99, 04/11/05
		/// Does not allow: 01/05/2000, 2/2/02
		/// </example> 
		public const string DATE_MM_DD_YY = @"^(1[0-2]|0[1-9])/(([1-2][0-9]|3[0-1]|0[1-9])/\d\d)$";


		/// <summary>
		/// This matches a date in the format mm/yy
		/// </summary>
		/// <example>
		/// Allows: 01/05, 11/05, 04/99
		/// Does not allow: 1/05, 13/05, 00/05
		/// </example>
		public const string DATE_MM_YY = @"^((0[1-9])|(1[0-2]))\/(\d{2})$";


		/// <summary>
		///     This matches only numbers(no decimals)
		/// </summary>
		/// <example>
		/// Allows: 0, 1, 123, 4232323, 1212322
		/// </example>
		public const string IS_NUMBER_ONLY = @"^([1-9]\d*)$|^0$";

		/// <summary>
		/// This matches any string with only alpha characters upper or lower case(A-Z)
		/// </summary>
		/// <example>
		/// Allows: abc, ABC, abCd, AbCd
		/// Does not allow: A C, abc!, (a,b)
		/// </example>
		public const string IS_ALPHA_ONLY = @"^[a-zA-Z]+$";

		/// <summary>
		/// This matches any string with only upper case alpha character(A-Z)
		/// </summary>
		public const string IS_UPPER_CASE = @"^[A-Z]+$";

		/// <summary>
		/// This matches any string with only lower case alpha character(A-Z)
		/// </summary>
		public const string IS_LOWER_CASE = @"^[a-z]+$";

		/// <summary>
		/// Ensures the string only contains alpha-numeric characters, and
		/// not punctuation, spaces, line breaks, etc.
		/// </summary>
		/// <example>
		/// Allows: ab2c, 112ABC, ab23Cd
		/// Does not allow: A C, abc!, a.a
		/// </example>
		public const string IS_ALPHA_NUMBER_ONLY = @"^[a-zA-Z0-9]+$";

		/// <summary>
		/// Validates US Currency.  Requires $ sign
		/// Allows for optional commas and decimal. 
		/// No leading zeros. 
		/// </summary>
		/// <example>Allows: $100,000 or $10000.00 or $10.00 or $.10 or $0 or $0.00
		/// Does not allow: $0.10 or 10.00 or 10,000</example>
		public const string IS_US_CURRENCY = @"^\$(([1-9]\d*|([1-9]\d{0,2}(\,\d{3})*))(\.\d{1,2})?|(\.\d{1,2}))$|^\$[0](.00)?$";

		/// <summary>
		/// Matches major credit cards including: Visa (length 16, prefix 4); 
		/// Mastercard (length 16, prefix 51-55);
		/// Diners Club/Carte Blanche (length 14, prefix 36, 38, or 300-305); 
		/// Discover (length 16, prefix 6011); 
		/// American Express (length 15, prefix 34 or 37). 
		/// Saves the card type as a named group to facilitate further validation 
		/// against a "card type" checkbox in a program. 
		/// All 16 digit formats are grouped 4-4-4-4 with an optional hyphen or space 
		/// between each group of 4 digits. 
		/// The American Express format is grouped 4-6-5 with an optional hyphen or space 
		/// between each group of digits. 
		/// Formatting characters must be consistant, i.e. if two groups are separated by a hyphen, 
		/// all groups must be separated by a hyphen for a match to occur.
		/// </summary>
		public const string CREDIT_CARD = @"^(?:(?<Visa>4\d{3})|(?<Mastercard>5[1-5]\d{2})|(?<Discover>6011)|(?<DinersClub>(?:3[68]\d{2})|(?:30[0-5]\d))|(?<AmericanExpress>3[47]\d{2}))([ -]?)(?(DinersClub)(?:\d{6}\1\d{4})|(?(AmericanExpress)(?:\d{6}\1\d{5})|(?:\d{4}\1\d{4}\1\d{4})))$";

		/// <summary>
		/// Matches social security in the following format xxx-xx-xxxx
		/// where x is a number
		/// </summary>
		/// <example>
		/// Allows: 123-45-6789, 232-432-1212
		/// </example>
		public const string SOCIAL_SECURITY = @"^\d{3}-\d{2}-\d{4}$";

		/// <summary>
		/// Matches x,x where x is a name, spaces are only allowed between comma and name
		/// </summary>
		/// <example>
		/// Allows: christophersen,eric; christophersen, eric
		/// Not allowed: christophersen ,eric;
		/// </example>
		public const string NAME_COMMA_NAME = @"^[a-zA-Z]+,\s?[a-zA-Z]+$";

		private MPatterns() { }

		/// <summary>
		/// Checks to see if the passed input has the passed pattern
		/// </summary>
		/// <param name="pattern">The pattern to use</param>
		/// <param name="input">The input to check</param>
		/// <returns>True if the input has the pattern, false otherwise</returns>
		public static bool HasPattern(string pattern, string input)
		{
			Regex regEx = new Regex(pattern);
			return regEx.IsMatch(input);
		}

		/// <summary>
		/// Checks the passed input to make sure it has all the patterns in the 
		/// passed patterns array
		/// </summary>
		/// <param name="patterns">Array of patterns</param>
		/// <param name="input">String value to check</param>
		/// <returns>True if the input has all of the patterns, false otherwise.</returns>
		public static bool HasPatterns(string[] patterns, string input)
		{
			for (int i = 0; i < patterns.Length; i++)
			{
				if (MPatterns.HasPattern(patterns[i], input) == false)
					return false;
			}

			return true;
		}
	}
}
