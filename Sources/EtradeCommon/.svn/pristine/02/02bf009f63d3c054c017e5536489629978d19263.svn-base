using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace OTS.WebLib.directory
{
	/// <summary>
	/// This class provides some static methods which are useful
	/// for file system "find" types of operations, similar to the *nix find
	/// command.
	/// SAMPLE:
	/// 
	/// 
	///  setup args
	/// string dirName = ".";
	/// ArrayList regexps = new ArrayList();
	/// regexps.Add("*.cs");
	/// regexps.Add("*.csproj");
	/// SortedList list = new SortedList();

	/// do the find
	/// DirectoryListing.findFiles(dirName, regexps, ref list, false);
	/// DirectoryListing.addParents(list);
	/// 
	/// // display the results
	/// foreach(DictionaryEntry entry in list)
	/// {
	///     string s = (string)entry.Key;
	///     Console.WriteLine(s);
	/// }
	/// </summary>
	public class DirectoryListing
	{
		
		/// <summary>
		/// Find files under a specified directory and matching any of
		/// a set of regular expressions. Return them by putting
		/// (string -> null) entries into the input SortedList.
		/// The regular expressions are Command Prompt dir command 
		/// type (like *.cs), not normal regular expressions.
		/// This optionally puts the directory name itself into the list.
		/// </summary>
		/// <remarks>This uses SortedList to get the insertion sort.</remarks>
		/// <param name="dirName">The directory to find files under. "." works.</param>
		/// <param name="regexps">List of directory regexp strings, for example *.cs and *.bat</param>
		/// <param name="list">The SortedList to fill.</param>
		/// <param name="addDirItself">Whether to add the dir name into the list.</param>
		/// <returns>bool - true for success, false for failure</returns>
		public static bool findFiles(string dirName, ArrayList regexps,	ref SortedList list, bool addDirItself)
		{
			DirectoryInfo dir = new DirectoryInfo(dirName);
			if (!dir.Exists) return(false);
			if (addDirItself) list.Add(dirName, null);

			// list directories: this can fail via exception
			DirectoryInfo[] dirs = null;
			try
			{
				dirs = dir.GetDirectories();
			}
			catch(UnauthorizedAccessException)
			{				
				return(false);
			}

			foreach (DirectoryInfo d in dirs)
			{
				string subDirName = dirName + Path.DirectorySeparatorChar + d.Name;
				if (!findFiles(subDirName, regexps, ref list, false))
				{
					Console.Error.WriteLine("Warn: Unable to find files under dir {0}", d.Name);
				}
			}

			foreach (string regexp in regexps)
			{
				foreach (FileInfo f in dir.GetFiles(regexp)) 
				{
					string s = dirName + Path.DirectorySeparatorChar + f.Name;
					list.Add(s, null);
				}
				foreach (string d in Directory.GetDirectories(dirName, regexp)) 
				{
					list.Add(d, null);
				}
			}
			
			return(true);
		}

		/// <summary>
		/// Overload with single regular expression.
		/// </summary>
		public static bool findFiles(string dirName, string regexp, ref SortedList list, bool addDirItself)
		{
			ArrayList regexps = new ArrayList();
			regexps.Add(regexp);
			return(findFiles(dirName, regexps, ref list, addDirItself));
		}

		/// <summary>
		/// For each entry in the input SortedList (where the keys in the
		/// list entries are strings, the file paths) add all parent 
		/// directories into the list.  So given a list with an entry
		/// with key "a/b/c", put "a/b" and "a" in the list.
		/// </summary>
		/// <remarks>The new entries are string dir name with value null.
		/// </remarks>
		/// <param name="list">The SortedList of DictionaryEntry's, with
		/// string file path keys.</param>
		/// <returns>true</returns>
		public static bool addParents(SortedList list)
		{
			ArrayList parents = new ArrayList();

			foreach (DictionaryEntry entry in list)
			{
				string dir = Path.GetDirectoryName((string)entry.Key);
				while ((dir != null) && (dir.Length > 1))
				{
					parents.Add(dir);
					dir = Path.GetDirectoryName(dir);
				}
			}

			foreach (string p in parents)
			{
				if (!list.ContainsKey(p)) list.Add(p, null);
			}
			return (true);
		}

	}
}


#region TEMP
/*
		/// <summary>
		/// A main which provides a command-line interface.  Run it without
		/// arguments or with -h or -? to see usage.
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
			// arg handling
			string usage = "usage:  nfind [-h] <directory> [<regexp1> ...] [-delete] [-includeParents] [-force]\n"
				+ "  -h : Display this help message.\n"
				+ "  <directory> : The directory for the root of your search.\n"
				+ "  <regexp1> ... : The file/directory style (*.cs) regular expressions.\n"
				+ "                  Files and directories which match any of the expressions\n"
				+ "                  you specify are acted upon (listed or deleted).\n"
				+ "                  If no expressions are specified, *.* is assumed.\n"
				+ "  -delete : Delete the files and directories which are found.\n"
				+ "            This deletes directories recursively, so be careful.\n"
				+ "  -includeParents : Include all parents of all files/dirs found in the output list.\n"
				+ "                    You can't do -delete with this option on, too dangerous.\n"
				+ "  -force : This confirms that you really want to delete all files under the\n"
				+ "           specified directory in the case where you have specified -delete and\n"
				+ "           have *.* in the regexp list or have specified no regexps.\n"
				+ "  For example: nfind . *.cs *.bat\n"
				+ "  For example: nfind . *.cs *.bat -includeParents\n"
				+ "  For example: nfind . *.pdb -delete\n"
				+ "  If no regular expressions are present, *.* is assumed.";

			// help
			if ((args.Length < 1) 
				|| (Array.IndexOf(args, "-h") >= 0)
				|| (Array.IndexOf(args, "-?") >= 0)
				|| (Array.IndexOf(args, "/h") >= 0)
				|| (Array.IndexOf(args, "/?") >= 0)
				)
			{
				string ver = Assembly.GetCallingAssembly().GetName().Version.ToString();
				string rver = Assembly.GetExecutingAssembly().GetName().Version.ToString();
				Console.WriteLine("NFind Version {0}", ver);
				Console.WriteLine("RTools.Util Version " + rver);
				Console.WriteLine(usage);
				return;
			}

			// dir name
			string dirName = args[0];
			if (!Directory.Exists(dirName))
			{
				Console.WriteLine("Directory '{0}' not found.", dirName);
				Console.WriteLine(usage);
				return;
			}

			// behavior switches
			bool deleteFiles = false; // whether or not user specified '-delete'
			bool includeParents = false; // whether or not to include parent dirs in output list
			bool force = false; // whether or not to really delete all files under cwd

			// collect arguments
			ArrayList regexps = new ArrayList();
			for (int i = 1; i < args.Length; i++)
			{
				if (args[i] == "-name") {} // drop unix-style arg
				else if (args[i] == "-delete") deleteFiles = true;
				else if (args[i] == "-includeParents") includeParents = true;
				else if (args[i] == "-force") force = true;
				else regexps.Add(args[i]);
			}
			if (regexps.Count == 0) 
			{
				regexps.Add("*.*"); // no args means *.*
			}

			// safety checks
			if (deleteFiles && !force && 
				(regexps.Contains("*.*") || regexps.Contains("*")))
			{
				Console.Error.WriteLine("Cannot delete all files under current dir without specifying -force.");
				return;
			}

			if (deleteFiles && includeParents)
			{
				Console.Error.WriteLine("Cannot include parents and delete files, too dangerous.");
				Console.Error.WriteLine("That would delete all files in all directories above any files found.");
				return;
			}

			// do the find
			SortedList list = new SortedList();
			Finder.FindFiles(dirName, regexps, ref list, false);
			if (includeParents) Finder.AddParents(list);

			// display the results
			foreach(DictionaryEntry entry in list)
			{
				string s = (string)entry.Key;

				if (deleteFiles)
				{
					try
					{
						if (Directory.Exists(s)) 
						{
							Directory.Delete(s, true);
							Console.WriteLine("Deleted dir: " + s);
						}
						else if (File.Exists(s)) 
						{
							File.Delete(s);
							Console.WriteLine("Deleted " + s);
						}
					}
					catch(Exception e)
					{
						Console.Error.WriteLine("Unable to delete '{0}', exception: '{1}'",
							s, e.Message);
					}
				}
				else Console.WriteLine(s);
			}
		}
		*/
#endregion