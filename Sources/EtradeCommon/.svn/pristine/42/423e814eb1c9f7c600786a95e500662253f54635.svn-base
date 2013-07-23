using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Core;

namespace OTS.WebLib.log
{
	/// <summary>
	/// Miguel : Adapter. Client just need to know our assembly, they dont want to manually ref to log4net.dll
	/// </summary>
	public class MLog 
	{
		private ILog _log = null;

		public MLog(ILog log) { _log = log; }

		public void Debug(object message, Exception t)
		{
			_log.Debug(message, t);
		}

		public void Debug(object message)
		{
			_log.Debug(message);
		}

		public void Error(object message, Exception t)
		{
			_log.Error(message, t);
		}

		public void Error(object message)
		{
			_log.Error(message);
		}

		public void Fatal(object message, Exception t)
		{
			_log.Fatal(message, t);
		}

		public void Fatal(object message)
		{
			_log.Fatal(message);
		}

		public void Info(object message, Exception t)
		{
			_log.Info(message, t);
		}

		public void Info(object message)
		{
			_log.Info(message);
		}

		public bool IsDebugEnabled
		{
			get { return _log.IsDebugEnabled; }
		}

		public bool IsErrorEnabled
		{
			get { return _log.IsErrorEnabled; }
		}

		public bool IsFatalEnabled
		{
			get { return _log.IsFatalEnabled; }
		}

		public bool IsInfoEnabled
		{
			get { return _log.IsInfoEnabled; }
		}

		public bool IsWarnEnabled
		{
			get { return _log.IsWarnEnabled; }
		}

		public void Warn(object message, Exception t)
		{
			_log.Warn(message, t);
		}

		public void Warn(object message)
		{
			_log.Warn(message);
		}


		public ILogger Logger
		{
			get { return _log.Logger; }
		}
	}
}
