using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace OTS.WebLib.mail
{
	/// <summary>
	/// Date 27 Sep 2k6
	/// A Utility mail module
	/// Miguel.Vu.Pham
	/// 
	/// ------------------
	/// Miguel's lesson:
	/// ------------------
	/// 
	/// When sending an email from an ASP.NET 2.0 page you will, typically:
	/// 
	/// 1. Create a MailMessage object
	/// 2. Assign its properties
	/// 3. Create an instance of the SmtpClient class
	/// 4. Specify details about the SMTP server to use (if they're not already specified within Web.config)
	/// 5. Send the MailMessage via the SmtpClient object's Send method
	/// 
	/// Only a subset of the SmtpClient properties can be specified through settings in Web.config. 
	/// To customize the other SmtpClient properties - EnableSsl, Timeout, and so on - 
	/// set them programmatically when sending the email (step 4 from the list of five steps examined earlier 
	/// in this article).
	/// 
	/// <configuration>
	//// <!-- Add the email settings to the <system.net> element -->
	//// <system.net>
	//// <mailSettings>
	////  <smtp>
	////    <network 
	////         host="relayServerHostname" 
	////         port="portNumber"
	////         userName="username"
	////         password="password" />
	////  </smtp>
	////</mailSettings>
	////</system.net>
	////
	////<system.web>
	////...
	////</system.web>
	////</configuration>
	/// </summary>
	public class MailUtil
	{
		public static bool ENABLE_SSL = false;
		public static bool SEND_ASYNCHRONOUS = false;
		
		/*
		 * Writing email to the IIS Server's SMTP service pickup directory is 
		 * another new feature of System.Net.Mail. 
		 * The SMTP pickup directory is a special directory used by Microsoft's SMTP service to send email. 
		 * Any email files found in that directory are processed and delivered over SMTP. 
		 * If the delivery process fails, the files are stored in a queue directory for delivery at another time. 
		 * If a fatal error occurs (such as a DNS resolution error), 
		 * the files are moved to the Badmail directory.
		 * By writing to the pickup directory, this speeds up the process because the entire chatting SMTP 
		 * layer used for relaying is by passed. 
		 * Below is an example of how to write directly to the Pickup directory.
		 * */
		public static bool PICKUP_DIRECTORY = false;

		public MailUtil() { }

		/// <summary>
		/// System.Net.Mail does not natively support sending a web page. 
		/// However, using the WebRequest class, 
		/// you can screen scrape web pages,
		/// and pass the resulting Html string to the MailMessage object. 
		/// The following example demonstrates this technique. 
		/// </summary>		
		/// <param name="MailFrom"></param>
		/// <param name="MailTo"></param>
		/// <param name="Cc"></param>
		/// <param name="Bcc"></param>
		/// <param name="Subject"></param>
		/// <param name="Body"></param>
		/// <param name="webPageURL"></param>
		public static void sendWebPageAsMail(			
			string MailFrom,			// e-mail address from
			string MailTo,				// e-mail address to
			string Cc,					// e-mail address CC
			string Bcc,					// e-mail address BCC

			string Subject,				// Subject
			string webPageURL			// send webpage
			)
		{

			WebRequest objRequest = System.Net.HttpWebRequest.Create(webPageURL);
            StreamReader sr = new StreamReader(objRequest.GetResponse().GetResponseStream());
			string Body = sr.ReadToEnd();
            sr.Close();

			MailUtil.sendMail(
				MailFrom, 
				MailTo, 
				Cc, 
				Bcc, 
				Subject,
				Body, 
				null, 
				null, 
				null, 
				null, 
				25, 
				MailPriority.Normal,
				Encoding.UTF8, 
				true);
		}


		/// <summary>
		/// send normal mail
		/// </summary>
		/// <param name="MailFrom"></param>
		/// <param name="MailTo"></param>
		/// <param name="Cc"></param>
		/// <param name="Bcc"></param>
		/// <param name="Subject"></param>
		/// <param name="Body"></param>
		public static void sendPlainTextMail(
			string MailFrom,			// e-mail address from
			string MailTo,				// e-mail address to
			string Cc,					// e-mail address CC
			string Bcc,					// e-mail address BCC

			string Subject,				// Subject
			string Body					// Body
			)
		{
			MailUtil.sendMail(
				MailFrom, 
				MailTo, 
				Cc, 
				Bcc, 
				Subject, 
				Body, 
				null, 
				null, 
				null, 
				null, 
				25, 
				MailPriority.Normal, 
				Encoding.UTF8, 
				false);
		}

		/// <summary>
		/// Send mail with all details
		/// </summary>
		/// <param name="MailFrom"></param>
		/// <param name="MailTo"></param>
		/// <param name="Cc"></param>
		/// <param name="Bcc"></param>
		/// <param name="Subject"></param>
		/// <param name="Body"></param>
		/// <param name="fileAttachmentPath"></param>
		/// <param name="SMTPServer">if not specifies, use default in web.config</param>		
		/// <param name="SMTPUsername">if not specifies, use default in web.config</param>
		/// <param name="SMTPPassword">if not specifies, use default in web.config</param>
		/// <param name="priority"></param>
		/// <param name="BodyEncoding">Encoding.GetEncoding("iso-8859-1")</param>
		public static void sendMail(

			string MailFrom,			// e-mail address from
			string MailTo,				// e-mail address to
			string Cc,					// e-mail address CC
			string Bcc,					// e-mail address BCC

			string Subject,				// Subject
			string Body,				// Body
			string fileAttachmentPath,	// attachment

			string SMTPServer,			// smtp									
			string SMTPUsername,		// smtpusername
			string SMTPPassword,		// smtppassword		
			int	SMTPPort,

			MailPriority priority,
			System.Text.Encoding BodyEncoding,
			bool isHtmlMail)
		{
			
			MailMessage objMail = new MailMessage(MailFrom, MailTo, Subject, Body);
			
			objMail.IsBodyHtml = isHtmlMail;

			if (!String.IsNullOrEmpty(Cc))
			{
				objMail.CC.Add(new MailAddress(Cc));
			}
			
			if (!String.IsNullOrEmpty(Bcc))
			{
				objMail.Bcc.Add(new MailAddress(Bcc));
			}

			// message			
			objMail.Priority = priority;

			if(!String.IsNullOrEmpty(Subject))
				objMail.Subject = Subject;
						
			if(BodyEncoding != null)
				objMail.BodyEncoding = BodyEncoding;
			
			if(!String.IsNullOrEmpty(Body))
				objMail.Body = Body;

			// attachment
			if (!String.IsNullOrEmpty(fileAttachmentPath))
			{
				objMail.Attachments.Add(new Attachment(fileAttachmentPath));
			}

			SmtpClient client = null;
			if (!String.IsNullOrEmpty(SMTPPassword) &&
				!String.IsNullOrEmpty(SMTPUsername) &&
				!String.IsNullOrEmpty(SMTPServer))
			{
				client = new SmtpClient(SMTPServer, SMTPPort);
				client.Credentials = new NetworkCredential(SMTPUsername, SMTPPassword);
				client.UseDefaultCredentials = false;
			}
			else
			{
				client = new SmtpClient();
			}

			client.EnableSsl = ENABLE_SSL;
			
			if (PICKUP_DIRECTORY)
				client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;

			if (!SEND_ASYNCHRONOUS)
			{
				client.Send(objMail);
			}
			else
			{
				//the userstate can be any object. The object can be accessed in the callback method
				//We should use our objMail for futher process !
				object userState = objMail;
				client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);				
				client.SendAsync(objMail, userState);
			}
		}


		/// <summary>
		/// event handler when using asynchorous send mail method
		/// Config log4Net va ghi log
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			//Get the Original MailMessage object
            MailMessage mail= (MailMessage)e.UserState;

            //write out the subject
            string subject = mail.Subject;

            if (e.Cancelled)
            {
                // [LOG] Send canceled for mail with subject [{0}].;				
            }

            if (e.Error != null)
            {
				// [LOG] Error when sending mail
            }
            else
            {
                // [LOG] successfully send
            }
		}
	}
}
