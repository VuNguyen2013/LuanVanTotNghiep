using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OTS.WebLib.directory
{
    /// <summary>
    /// DirectoryHelper
    /// Innotech
    /// </summary>
    public class DirectoryHelper
    {
        
        /// <summary>
        /// neu folder chua tao thi tao,
        /// tao roi thi thoi
        /// Tao theo mau 2007-09
        /// uploadBaseDir = "d:/upload/
        /// </summary>
        public static string createFolderForUpload(string uploadBaseDir)
        {
            if (uploadBaseDir.Equals(string.Empty)) return string.Empty;

            string folderPath = string.Format("{0}/{1}-{2}/", uploadBaseDir, DateTime.Now.Year, DateTime.Now.Month);

            if (Directory.Exists(folderPath)) return folderPath;

            Directory.CreateDirectory(folderPath);

            return folderPath;
        }
    }
}
