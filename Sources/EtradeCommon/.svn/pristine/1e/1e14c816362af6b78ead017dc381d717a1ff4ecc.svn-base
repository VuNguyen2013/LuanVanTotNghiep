using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;

/*
 * 	<configSections>
		<section name="MyApp" type="System.Configuration.NameValueSectionHandler,system,Version=1.0.3300.0,Culture=neutral,PublicKeyToken=b77a5c561934e089,Custom=null" />
	</configSections>

 * <MyApp>
		<add key="abc" value="xyz"/>
	</MyApp>
	
 * 
	string templateDir = MConfig.getValue("MyApp", "abc");
 */
namespace OTS.WebLib.configuration
{
	/// <summary>
	/// Configuration loading helper
	/// Innotech
	/// </summary>
	public class MConfig
	{
		// not allow create object
		private MConfig() { }

		/// <summary>
		/// Get value of key in one region
		/// Use both for app and web config
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		/*
		 * Sample
		 * in web.config
		 * <LiLyConfig>
		 *		<add setting="TemplateDir" value="~/Templates" />
		 *		<add setting="ImageDir" value="~/UserFiles/Image" />
		 *		<add setting="SuperUser" value="admin" />
		 *		<add setting="AdministratorRole" value="Administrator" />
		 *		<add setting="SMTPServer" value="localhost" />
		 *		<add setting="SearchIndexDir" value="~/index" />
		 *		<add setting="InstantIndexing" value="true" /> <!-- true/false -->
		 *		<add setting="FCKeditor:UserFilesPath" value="~/UserFiles" /> 
		 *	</LiLyConfig>
		 * */
		public static string getValue(string region, string key)
		{
			if (!String.IsNullOrEmpty(region) && !String.IsNullOrEmpty(key))
			{				
				NameValueCollection collection = (NameValueCollection)System.Configuration.ConfigurationSettings.GetConfig(region);
				if (collection != null)
				{
					return collection[key];
				}
			}

			return "";
		}
	}
}
