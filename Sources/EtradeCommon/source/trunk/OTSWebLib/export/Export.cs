using System;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Threading;


namespace OTS.AppCode.ExportData
{
	public class Export
	{
        System.Web.HttpResponse response;
        public Export()
        {
            response = System.Web.HttpContext.Current.Response;
        }
        	
        public void WriteToString(DataTable table, bool header, bool quoteall,string filename)
        {
            response.Clear();
            response.Buffer = true;

         
            response.ContentType = "text/csv";
            response.AppendHeader("content-disposition", "attachment; filename=" + filename);
            
            
            StringWriter writer = new StringWriter();
            WriteToStream(writer, table, header, quoteall);
            //StreamWriter sw = new StreamWriter("c:\text.csv");
            //return writer.ToString();
            response.Write(writer.ToString());
            //sw.Close();
            writer.Close();
            //stream.Close();
            response.End();
           // writer.Close();
        }

        public void WriteToStream(TextWriter stream, DataTable table, bool header, bool quoteall)
        {
            if (header)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, table.Columns[i].Caption, quoteall);
                    if (i < table.Columns.Count - 1)
                        stream.Write(',');
                    else
                        stream.Write('\n');
                }
            }
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, row[i], quoteall);
                    if (i < table.Columns.Count - 1)
                        stream.Write(',');
                    else
                        stream.Write('\n');
                }
            }
        }

        private void WriteItem(TextWriter stream, object item, bool quoteall)
        {
            if (item == null)
                return;
            string s = item.ToString();
            if (quoteall || s.IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
            {
               // s.Replace("\"", "\"\"");  
                stream.Write( s.Replace(",", "."));
            }
            else
                stream.Write(s);
        }
	}
}
