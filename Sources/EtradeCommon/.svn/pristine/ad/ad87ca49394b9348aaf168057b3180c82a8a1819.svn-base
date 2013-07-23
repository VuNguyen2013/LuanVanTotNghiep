// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailsService.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the EmailsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCommon
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public class EmailsService 
    {
        public static bool EnableSsl;

        public static bool SendAsynchronous;

        public static bool PickupDirectory;

        /// <summary>
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <param name="body">
        /// The body.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="customerID">
        /// The customer id.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// </returns>
        public bool SendMail(string emailAddress, string body, string subject, string customerID, string userID)
        {
            string excep = string.Empty;
            bool ret = true;
            try
            {
                SendAsynchronous = AppConfig.SmtpSendAsynchronous;
                EnableSsl = AppConfig.SmtpSslEnable;

                string mailfrom = AppConfig.MailFrom;
                string smtpSubject = subject;
                string smtpServer = AppConfig.SmtpServer;
                string smtpUid = AppConfig.SmtpUser;
                string smtpPwd = AppConfig.SmtpPassword;
                int smtpPort = AppConfig.SmtpPort;

                SendMail(mailfrom, emailAddress, "", "", smtpSubject,
                    body, null, smtpServer, smtpUid, smtpPwd, smtpPort,
                    MailPriority.Normal, Encoding.UTF8, true);
            }
            catch (Exception ex)
            {
                //TOOD: should log there
            }

            return ret;

        }

        /// <summary>
        /// Send mail with all details
        /// </summary>
        /// <param name="mailFrom"></param>
        /// <param name="mailTo"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="fileAttachmentPath"></param>
        /// <param name="smtpServer">if not specifies, use default in web.config</param>		
        /// <param name="smtpUsername">if not specifies, use default in web.config</param>
        /// <param name="smtpPassword">if not specifies, use default in web.config</param>
        /// <param name="smtpPort"></param>
        /// <param name="priority"></param>
        /// <param name="bodyEncoding">Encoding.GetEncoding("iso-8859-1")</param>
        /// <param name="isHtmlMail"></param>
        public static void SendMail(

            string mailFrom,			// e-mail address from
            string mailTo,				// e-mail address to
            string cc,					// e-mail address CC
            string bcc,					// e-mail address BCC

            string subject,				// Subject
            string body,				// Body
            string fileAttachmentPath,	// attachment

            string smtpServer,			// smtp									
            string smtpUsername,		// smtpusername
            string smtpPassword,		// smtppassword		
            int smtpPort,

            MailPriority priority,
            System.Text.Encoding bodyEncoding,
            bool isHtmlMail)
        {

            MailMessage objMail = new MailMessage(mailFrom, mailTo, subject, body);

            objMail.IsBodyHtml = isHtmlMail;

            if (!String.IsNullOrEmpty(cc))
            {
                objMail.CC.Add(new MailAddress(cc));
            }

            if (!String.IsNullOrEmpty(bcc))
            {
                objMail.Bcc.Add(new MailAddress(bcc));
            }

            // message			
            objMail.Priority = priority;

            if (!String.IsNullOrEmpty(subject))
                objMail.Subject = subject;

            if (bodyEncoding != null)
                objMail.BodyEncoding = bodyEncoding;

            if (!String.IsNullOrEmpty(body))
                objMail.Body = body;

            // attachment
            if (!String.IsNullOrEmpty(fileAttachmentPath))
            {
                objMail.Attachments.Add(new Attachment(fileAttachmentPath));
            }

            SmtpClient client = null;
            if (!String.IsNullOrEmpty(smtpPassword) &&
                !String.IsNullOrEmpty(smtpUsername) &&
                !String.IsNullOrEmpty(smtpServer))
            {
                client = new SmtpClient(smtpServer, smtpPort);
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                //client.UseDefaultCredentials = true;
            }
            else
            {
                client = new SmtpClient();
            }

            client.EnableSsl = EnableSsl;

            if (PickupDirectory)
                client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;

            if (!SendAsynchronous)
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
            MailMessage mail = (MailMessage)e.UserState;

            //write out the subject
            string subject = mail.Subject;

            if (e.Cancelled)
            {
                // TODO: should log there for cancel enail				
            }

            if (e.Error != null)
            {
                // TODO: should log there for send email failed
            }
            else
            {
                // TODO: should log there for send email success
            }
        }
    }
}