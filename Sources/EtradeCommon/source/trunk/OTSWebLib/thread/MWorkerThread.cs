using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OTS.WebLib.thread
{
	/// <summary>
	/// Threading	
	/// Innotech
	/// </summary>
	public class MWorkerThread
	{
		private Thread _thread = null;
		private ManualResetEvent _eventThreadStopped = null;
		private ManualResetEvent _eventStopThread = null;

		public void startThread()
		{
			_eventThreadStopped = new ManualResetEvent(false);
			_eventStopThread = new ManualResetEvent(false);
			_eventThreadStopped.Reset(); _eventStopThread.Reset();

			_thread = new Thread(new ThreadStart(realRun));
			_thread.Start();
		}

		/// <summary>
		/// raise event here
		/// </summary>
		protected virtual void beforeExecuteRun()
		{			
		}

		/// <summary>
		/// allow override
		/// When override, remebers to recall this function
		/// 
		/// While (doSth)
		/// {
		///		...
		///		base.run();
		/// }
		/// </summary>
		protected virtual void run()
		{
			// SAMPLE TEMPLATE
			// while(something)
			// long task come heres
			//...............


			//if (hasStopNotified())
			//{
			//    return;
			//}

			// end while
		}

		/// <summary>
		/// clean up everything if any
		/// </summary>
		protected virtual void cleanUp()
		{
		}

		/// <summary>
		/// raise event here
		/// </summary>
		protected virtual void afterExecuteRun()
		{
		}

		/// <summary>
		/// notify thread stopped
		/// </summary>
		protected void notifyThreadStopped()
		{
			if (_eventThreadStopped != null)
			{
				_eventThreadStopped.Set();
			}
		}

		/// <summary>
		/// check for stop notified event
		/// </summary>
		protected bool hasNotifyMeToStop()
		{
			// there is a StopThread event
			if (_eventStopThread.WaitOne(0, true))
			{
				// clean up resource
				cleanUp();

				// notify that the thread has stopped.
				notifyThreadStopped();

				// force thread to stop
				return true;
			}

			return false;
		}

		/// <summary>
		/// template method
		/// </summary>
		private void realRun()
		{
			beforeExecuteRun();
			
			// this method allow to be override latter
			run();

			afterExecuteRun();
		}

		
		/// <summary>
		/// stop worker thread
		/// </summary>
		public void stopThread()
		{
			while (_thread != null && _thread.IsAlive)
			{
				// notified to stop thread
				_eventStopThread.Set();

				// wait until RUN finish its working
				if (WaitHandle.WaitAll((new ManualResetEvent[] { _eventThreadStopped }), 100, true))
				{
					break;
				}				

			}

			_thread = null;
			_eventThreadStopped = null;
			_eventStopThread = null;
		}
	}
}

