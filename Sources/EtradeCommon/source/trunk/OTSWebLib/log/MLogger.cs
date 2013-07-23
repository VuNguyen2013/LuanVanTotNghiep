using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using log4net;

namespace OTS.WebLib.log
{
	/// <summary>
	/// log4net helper, 
	/// 1. Init config
	/// 2. get logger
	/// 
	/// ----------
	/// Miguel Vu Pham
	/// </summary>
	/// 
	/* Sample :
		MLogger.Instance.configureInWeb("/config/log4net_config.config");
		MLog log = MLogger.Instance.getLog(this.GetType().FullName);
	 * 
	 *  .....
		log.Error("aa");

	 * */

	public class MLogger
	{
		private static MLogger _current = null;
        private static MLogger _service = null;	
		private MLogger()
		{
			
		}

		/// <summary>
		/// singleton pattern
		/// this is just for make sure logger has been configured
		/// </summary>
		public static MLogger Instance
		{
			get
			{
				lock (typeof(MLogger))
				{
					if (_current == null)
					{
						_current = new MLogger();				
					}
					return _current;
				}

			}
		}
        public static MLogger ServiceInstance
        {
            get
            {
                lock (typeof(MLogger))
                {
                    if (_service == null)
                    {
                        _service = new MLogger();
                    }
                    return _service;
                }

            }
        }
		/// <summary>
		/// config
		/// no' se doc noi dung config trong web.config, hoac app.config
		/// </summary>
		public void configureDefault()
		{
			log4net.Config.DOMConfigurator.Configure();
		}

		/// <summary>
		/// config, input file la duong dan tuong doi tren web
		/// </summary>
		/// <param name="webFilePath"></param>
		public void configureInWeb(string webContextFilePath)
		{
            if (HttpContext.Current != null && HttpContext.Current.Server!=null)
			    configureInApp(HttpContext.Current.Server.MapPath(webContextFilePath));
            else
                configureInApp(System.Web.Hosting.HostingEnvironment.MapPath(webContextFilePath));
		}

		/// <summary>
		/// configure voi full file path : C:/dfafd/fdafda/
		/// </summary>
		/// <param name="fullFilePath"></param>
		public void configureInApp(string fullFilePath)
		{
			log4net.Config.DOMConfigurator.Configure(new FileInfo(fullFilePath));
		}

		/// <summary>
		/// return null if log has not been config
		/// </summary>
		/// <param name="logName"></param>
		/// <returns></returns>
		public MLog getLog(string logName)
		{
			ILog log = LogManager.GetLogger(logName);
			if (log != null && (log.IsDebugEnabled || log.IsErrorEnabled || log.IsFatalEnabled || log.IsInfoEnabled || log.IsWarnEnabled))
			{
				return new MLog(log);
			}

			return null;
		}

	}
}

/**
 * Sample log4net config:
 * 
 * // log4net_sample.config
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="log4net" type="System.Configuration.IgnoreSectionHandler" />
  </configSections>
  
  <log4net>
    <appender 
      name="ERRORLogFileAppender" 
      type="log4net.Appender.RollingFileAppender">
      <param name="File" value="~^_^~/error-log.log" />
      <param name="AppendToFile" value="true" />
      <param name="MaxSizeRollBackups" value="9" />
      <param name="MaximumFileSize" value="10MB" />
      <param name="RollingStyle" value="Date" />
      <datePattern value="yyyyMMdd" />
      <param name="StaticLogFileName" value="true" />
      <layout type="log4net.Layout.PatternLayout">
        <param name="ConversionPattern" value="%d [%t] %-5p - %m%n" />
      </layout>
    </appender>     
   
	<appender name="ConsoleAppender" type="log4net.Appender.ConsoleAppender" >
		<filter type="log4net.Filter.LevelRangeFilter">
			<acceptOnMatch value="true" />
			<levelMin value="DEBUG" />
			<levelMax value="FATAL" />
		</filter>
		<layout type="log4net.Layout.PatternLayout">
			<conversionPattern value="%-5p %5rms [%-17.17t] %-22.22c{1} %-18.18M - %m%n" />
		</layout>
	</appender>
	
	<appender name="SmtpAppender" type="log4net.Appender.SmtpAppender">
		<to value="SomebodyWhoCares@example.com" />
		<from value="GameServerMonitor@skilljam.com" />
		<subject value="Game Server Monitoring Failed!" />
		<smtpHost value="mail2.gameuniverse.com" />
		<bufferSize value="256" />
		<lossy value="true" />
		<evaluator type="log4net.spi.LevelEvaluator">
			<threshold value="ERROR" />
		</evaluator>
		<layout type="log4net.Layout.PatternLayout">
			<conversionPattern value="%-5p %d [ThreadId: %t] Class:%c{1} Method:%M %nMESSAGE:%n%m%n%n" />
		</layout>
	</appender>
	
    <root>
      <level value="ALL" />
      <appender-ref ref="LogFileAppender" />      
    </root>    
  </log4net>
</configuration>

*/