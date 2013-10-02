using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using AccountManager.Services;
using AccountManagerWebServices.SMSService;
using ETrade;
using ETradeCommon;
using ETradeCommon.Enums;

namespace AccountManagerWebServices.Utils
{
    ///<summary>
    /// Util to send sms
    ///</summary>
    public class SMSSender
    {
        private static readonly string ServicesID = ConfigurationManager.AppSettings["SMSServicesID"];
        private static readonly string UserName = ConfigurationManager.AppSettings["SMSServicesUserName"];
        private static readonly string Password = ConfigurationManager.AppSettings["SMSServicesPassword"];
        private static readonly SMSSendService ServicesCaller = new SMSSendService();
        private const string WEB_SERVICE_POLICY = "WebServiceExceptionPolicy";
        private static readonly int ResendSMSSleepTime = int.Parse(ConfigurationManager.AppSettings["ResendSMSSleepTime"]);
        private static readonly SmsCountService SMSCountService = new SmsCountService();

        private static string UserNameSiteVietGuy = ConfigurationManager.AppSettings["UserNameSiteVietGuy"];
        private static string PasswordSiteVietGuy = ConfigurationManager.AppSettings["PasswordSiteVietGuy"];
        private static string FromSiteVietGuy = ConfigurationManager.AppSettings["FromSiteVietGuy"];
        private static string[] DauSoCMS = ConfigurationManager.AppSettings["DauSoCMS"].Split(',');

        ///<summary>
        /// Connect to the webservices provided by the CMC.
        /// Send the SMS message to the SMS GW.
        /// And get the return code.
        /// RequestID: blank if it it MT message.
        ///</summary>
        ///<param name="fullMessage"></param>
        ///<returns></returns>
        public static List<string> BreakDownMessage(string fullMessage)
        {
            const string DELIMITER = " ";
            string token;

            // Processing for case when length of Message > 160 characters.
            var messages = new List<string>();
            var Token = new Tokens(fullMessage, DELIMITER);
            string message = "";

            while ((token = Token.nextElement()) != null)
            {

                if (message.Length + token.Length < Constants.SMS_MESSAGE_LENGTH)
                {
                    if (message == "")
                    {
                        message = token;
                    }
                    else
                    {
                        message += (DELIMITER + token);
                    }

                }
                else//full message
                {
                    messages.Add(message);
                    message = token;
                }
            }


            if (message != "")
            {
                messages.Add(message);
            }

            return messages;

        }

        /// <summary>
        /// Break messesage incase it is Unicoded
        /// </summary>
        /// <param name="fullMessage"></param>
        /// <returns></returns>
        public static List<string> BreakDownUnicodeMessage(string fullMessage)
        {
            const string DELIMITER = " ";
            string token;

            // Processing for case when length of Message > 160 characters.
            var messages = new List<string>();
            var Token = new Tokens(fullMessage, DELIMITER);
            string message = "";

            while ((token = Token.nextElement()) != null)
            {

                if (message.Length + token.Length < Constants.SMS_UNICODE_MESSAGE_LENGTH)
                {
                    if (message == "")
                    {
                        message = token;
                    }
                    else
                    {
                        message += (DELIMITER + token);
                    }

                }
                else//full message
                {
                    messages.Add(message);
                    message = token;
                }
            }


            if (message != "")
            {
                messages.Add(message);
            }

            return messages;

        }

        private static  CommonEnums.RET_CODE SendMessage(String phone, string message, String requestID)
        {
            int ret = ServicesCaller.SendSms(phone, message, 0, requestID, ServicesID, UserName, Password);

            switch (ret)
            {
                case 1:
                    return CommonEnums.RET_CODE.SUCCESS;
                case 0:
                    return CommonEnums.RET_CODE.ERROR_ACCOUNT;
                case -1:
                    return CommonEnums.RET_CODE.SYSTEM_ERROR;
                default:
                    return CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Send message with isUnicode param
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <param name="requestID"></param>
        /// <param name="isUnicode"></param>
        /// <returns></returns>
        private static CommonEnums.RET_CODE SendMessage(String phone, string message, String requestID, int isUnicode)
        {
            int ret = ServicesCaller.SendSms(phone, message, isUnicode, requestID, ServicesID, UserName, Password);

            switch (ret)
            {
                case 1:
                    return CommonEnums.RET_CODE.SUCCESS;
                case 0:
                    return CommonEnums.RET_CODE.ERROR_ACCOUNT;
                case -1:
                    return CommonEnums.RET_CODE.SYSTEM_ERROR;
                default:
                    return CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        ///<summary>
        /// Send non unicode message
        ///</summary>
        ///<param name="phone"></param>
        ///<param name="message"></param>
        ///<param name="requestID">Blank if this is auto send message; otherwise 1</param>
        ///<returns></returns>
        public static CommonEnums.RET_CODE SendSMS(String phone, string message, String requestID)
        {
            try
            {
                CommonEnums.RET_CODE ret;
                List<string> messages = BreakDownMessage(message);

                if (messages.Count <= 0)
                {
                    return CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                int count = 0;
                bool isSendCMS = IsCMS(phone);
                foreach (string item in messages)
                {
                    LogHandler.Log("[SendSMS]: Phone " + phone + " Message " + item, "SMSSender.SendSMS",
                                   TraceEventType.Information);
                    ret = !isSendCMS ? SendMessage(phone, item) : SendMessage(phone, item, requestID);

                    if (ret != CommonEnums.RET_CODE.SUCCESS)
                    {
                        LogHandler.Log("[SendSMS] Error: Return Code: " + ret + " Phone " + phone + " Message " + item, 
                                   "SMSSender.SendSMS", TraceEventType.Error);
                        int retryTimes = 0;
                        while ((ret == CommonEnums.RET_CODE.SYSTEM_ERROR) && (retryTimes <= 3))
                        {
                            Thread.Sleep(ResendSMSSleepTime);// sleep 10s and resend
                            LogHandler.Log("[SendSMS]: Resend Phone " + phone + " Message " + item, "SMSSender.SendSMS",
                                   TraceEventType.Information);
                            ret = !isSendCMS ? SendMessage(phone, item) : SendMessage(phone, item, requestID);
                            retryTimes++;
                        }
                    }
                    count++;
                }

                if (count > 0)
                {
                    // Save message total to database
                    SMSCountService.UpdateCount(count);
                }

                return CommonEnums.RET_CODE.SUCCESS;
            }

            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, WEB_SERVICE_POLICY);
                return CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Send Unicode SMS
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <param name="requestID"></param>
        /// <param name="isUnicode">
        /// 1: Unicode
        /// 0: Not unicode
        /// </param>
        /// <returns></returns>
        public static CommonEnums.RET_CODE SendSMS(String phone, string message, String requestID, int isUnicode)
        {
            try
            {
                List<string> messages;
                CommonEnums.RET_CODE ret;
                if (isUnicode == Constants.ISUNICODE)
                {
                    messages = BreakDownUnicodeMessage(message);
                }
                else
                {
                    messages = BreakDownMessage(message);
                }

                if (messages.Count <= 0)
                {
                    return CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                int count = 0;
                bool isSendCMS = IsCMS(phone);
                foreach (string item in messages)
                {
                    LogHandler.Log("[SendSMS]: Phone " + phone + " Message " + item, "SMSSender.SendSMS",
                                   TraceEventType.Information);

                    ret = !isSendCMS ? SendMessage(phone, item) : SendMessage(phone, item, requestID, isUnicode);

                    if (ret != CommonEnums.RET_CODE.SUCCESS)
                    {
                        LogHandler.Log("[SendSMS] Error: Return Code: " + ret + " Phone " + phone + " Message " + item,
                                   "SMSSender.SendSMS", TraceEventType.Error);
                        while (ret == CommonEnums.RET_CODE.SYSTEM_ERROR)
                        {
                            Thread.Sleep(ResendSMSSleepTime);// sleep 10s and resend
                            LogHandler.Log("[SendSMS]: Resend Phone " + phone + " Message " + item, "SMSSender.SendSMS",
                                   TraceEventType.Information);
                            ret = !isSendCMS ? SendMessage(phone, item) : SendMessage(phone, item, requestID, isUnicode);
                        }
                    }
                    count++;
                }

                if (count > 0)
                {
                    // Save message total to database
                    SMSCountService.UpdateCount(count);
                }

                return CommonEnums.RET_CODE.SUCCESS;
            }

            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, WEB_SERVICE_POLICY);
                return CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        public static CommonEnums.RET_CODE SendMessage(String Phone, string Message)
        {
            try
            {
                string address = string.Format(
                    "http://sms.vietguys.biz/api/?u={0}&pwd={1}&from={2}&phone={3}&sms={4}", UserNameSiteVietGuy,
                    PasswordSiteVietGuy, FromSiteVietGuy, Phone, Message);
                var webRequest = (HttpWebRequest)System.Net.WebRequest.Create(address);
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string str = reader.ReadToEnd();
                if (str.Trim().Length>3)
                {
                    return CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    int i = int.Parse(str.Trim());
                    switch (i)
                    {
                        case (int)CommonEnums.RET_CODE_SMS.ERROR_ACCOUNT:
                        case (int)CommonEnums.RET_CODE_SMS.ERROR_ACCOUNT2:
                            LogHandler.Log("Thong tin tai khoan chua chinh xac", "SendMessage", TraceEventType.Warning);
                            break;
                        case (int)CommonEnums.RET_CODE_SMS.ERROR_ACCOUNT_EMPTY:
                        case (int)CommonEnums.RET_CODE_SMS.ERROR_ACCOUNT_EMPTY2:
                            LogHandler.Log("Tai khoan het Credit gui tin", "SendMessage",TraceEventType.Warning);
                            break;
                        case (int)CommonEnums.RET_CODE_SMS.ERROR_ACCOUNT_LOCK:
                            LogHandler.Log("Tai khoan bi khoa", "SendMessage", TraceEventType.Warning);
                            break;
                        case (int)CommonEnums.RET_CODE_SMS.ERROR_API:
                            LogHandler.Log("API Chua actived", "SendMessage", TraceEventType.Warning);
                            break;
                        case (int)CommonEnums.RET_CODE_SMS.ERROR_IP:
                            LogHandler.Log("IP bi gioi han truy cap, khong duoc phep gui tu IP nay", "SendMessage", TraceEventType.Warning);
                            break;
                        case (int)CommonEnums.RET_CODE_SMS.ERROR_NETWORK:
                            LogHandler.Log("Khong the ket noi den may chu VietGuy", "SendMessage", TraceEventType.Warning);
                            break;
                        case (int)CommonEnums.RET_CODE_SMS.ERROR_PHONE:
                            LogHandler.Log("So dien thoai nguoi nhan chua chinh xac: "+Phone, "SendMessage", TraceEventType.Warning);
                            break;
                        case (int)CommonEnums.RET_CODE_SMS.ERROR_PHONE_BACLIST:
                            LogHandler.Log("So dien thoai nam trong danh sach backlist", "SendMessage", TraceEventType.Warning);
                            break;
                        case (int)CommonEnums.RET_CODE_SMS.MISSING_PARAMETER:
                            LogHandler.Log("Chua nhap day du tham so", "SendMessage", TraceEventType.Warning);
                            break;
                        case (int)CommonEnums.RET_CODE_SMS.ERROR_SYSTEM:
                            LogHandler.Log("Dau so chua dang ky: "+FromSiteVietGuy, "SendMessage", TraceEventType.Warning);
                            break;
                        default:
                            LogHandler.Log(str, "SendMessage", TraceEventType.Warning);
                            break;
                    }
                    return CommonEnums.RET_CODE.SYSTEM_ERROR;
                }

            }
            catch (Exception)
            {
                return CommonEnums.RET_CODE.SYSTEM_ERROR;
            }

        }
        
        public static bool IsCMS(string phone)
        {
            foreach (var s in DauSoCMS)
            {
                if (phone.IndexOf("0")==0)
                {
                    if (phone.IndexOf(s)==0)
                    {
                        return true;
                    }
                }
                else
                {
                    string substring = "0" + phone.Substring(2);
                    if (substring.IndexOf(s)==0)
                        return true;
                }
            }
            return false;
        }
    }
}
