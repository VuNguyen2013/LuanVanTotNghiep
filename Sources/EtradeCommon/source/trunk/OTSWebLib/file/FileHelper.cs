using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OTS.WebLib.file
{
    /// <summary>
    /// file helper 
    /// miguel vu pham
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// get content path in web
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string getContentPathInWeb(System.Web.HttpServerUtility Server, string fileName)
        {
            return Server.MapPath(fileName);
        }


        /// <summary>
        /// load file content
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string loadFileContent(string filepath)
        {
            FileStream fs = null;
            StreamReader r = null;

            try
            {
                fs = new FileStream(filepath, FileMode.Open);
                r = new StreamReader(fs, Encoding.UTF8);
                return r.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (r != null)
                    r.Close();

                if (fs != null)
                    fs.Close();
            }
        }
    }
}
