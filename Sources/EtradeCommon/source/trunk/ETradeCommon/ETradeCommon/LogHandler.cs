using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace ETradeCommon
{
    public class LogHandler
    {
        public static void Log(string message, string methodName, TraceEventType logType)
        {
            var log = new LogEntry {Message = message, Priority = Constants.Priority.NORMAL, Severity = logType};
            log.ExtendedProperties.Add("MethodName", methodName);
            if (logType == TraceEventType.Critical || logType == TraceEventType.Error)
            {
                log.Categories.Add(Constants.Category.ERROR_LOG);
            }
            else
            {
                log.Categories.Add(Constants.Category.GENERAL);
            }
            Logger.Write(log);
        }

        public static void Log4Web(string message, string methodName, TraceEventType logType)
        {
            var log = new LogEntry { Message = message, Priority = Constants.Priority.NORMAL, Severity = logType };
            log.ExtendedProperties.Add("MethodName", methodName);
            log.Categories.Add(Constants.Category.GENERAL);

            Logger.Write(log);
        }

         public static void LogLinkOPS(string message, string methodName, TraceEventType logType)
         {
             var log = new LogEntry { Message = message, Priority = Constants.Priority.NORMAL, Severity = logType };
             log.ExtendedProperties.Add("MethodName", methodName);
             log.Categories.Add(Constants.Category.LINKOPS);
             Logger.Write(log);
         }
    }
}
