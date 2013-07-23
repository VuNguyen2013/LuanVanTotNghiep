// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppConfig.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the AppConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCommon
{
    using System;
    using System.Configuration;
    using System.Diagnostics;

    /// <summary>
    /// Load all application configurations for first use, 
    /// theys are updated if restarting IIS
    /// </summary>
    public static class AppConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether [_ SMT p_ SEN d_ ASYNCHRONOUS].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [_ SMT p_ SEN d_ ASYNCHRONOUS]; otherwise, <c>false</c>.
        /// </value>
        public static bool SmtpSendAsynchronous;

        /// <summary>
        /// Gets or sets a value indicating whether [_ SMT p_ SS l_ ENABLE].
        /// </summary>
        /// <value><c>true</c> if [_ SMT p_ SS l_ ENABLE]; otherwise, <c>false</c>.</value>
        public static bool SmtpSslEnable;

        /// <summary>
        /// Gets or sets a value indicating whether [_ MAI l_ FROM].
        /// </summary>
        /// <value><c>true</c> if [_ MAI l_ FROM]; otherwise, <c>false</c>.</value>
        public static string MailFrom;

        /// <summary>
        /// Gets or sets the _ SMT p_ SERVER.
        /// </summary>
        /// <value>The _ SMT p_ SERVER.</value>
        public static string SmtpServer;

        /// <summary>
        /// Gets or sets the _ SMT p_ USER.
        /// </summary>
        /// <value>The _ SMT p_ USER.</value>
        public static string SmtpUser;

        /// <summary>
        /// Gets or sets the _ SMT p_ PASSWORD.
        /// </summary>
        /// <value>The _ SMT p_ PASSWORD.</value>
        public static string SmtpPassword;

        /// <summary>
        /// Gets or sets the _ SMT p_ PORT.
        /// </summary>
        /// <value>The _ SMT p_ PORT.</value>
        public static int SmtpPort;

        /// <summary>
        /// Get or set MsDbConnectionString
        /// </summary>
        public static string MsDbConnectionString;

        /// <summary>
        /// Get or set FisDbConnectionString
        /// </summary>
        public static string FisDbConnectionString;

        /// <summary>
        /// Get or set SbaConnectionString
        /// </summary>
        public static string SbaConnectionString;

        /// <summary>
        /// Get or set AliasInformix
        /// </summary>
        public static string AliasInformix;

        /// <summary>
        /// Get or set TraderID of LinkOPS
        /// </summary>
        public static string LinkOPSTraderID;

        /// <summary>
        /// Get or set the IP address of LinkOPS server
        /// </summary>
        public static string LinkOPSAddress;

        /// <summary>
        /// get or set the port of LinkOPS from web.config
        /// </summary>
        public static string LinkOPSPort;

        /// <summary>
        /// Get or set the duration for sending the heartbeat to LinkOPS
        /// </summary>
        public static string LinkOPSHeartBeat;

        /// <summary>
        /// Get or set the user name for login to LinkOPS
        /// </summary>
        public static string LinkOPSUserName;

        /// <summary>
        /// Get or set the password for login to LinkOPS
        /// </summary>
        public static string LinkOPSPass;

        /// <summary>
        /// Get or set the service name,
        /// </summary>
        public static string ServiceName;

        /// <summary>
        /// Check GW connection
        /// </summary>
        public static bool CheckGWConnection;

        public static int PreOpenDuration { get; set; }

        public static int MorningOpenDuration { get; set; }

        public static int AfternoonOpenDuration { get; set; }

        public static int PreCloseDuration { get; set; }

        public static bool EnablePutATCbefore { get; set; }

        public static int PreOpenDurationHose { get; set; }

        public static int MorningOpenDurationHose { get; set; }

        public static int AfternoonOpenDurationHose { get; set; }

        public static int PreCloseDurationHose { get; set; }

        public static bool EnablePutATCbeforeHose { get; set; }

        public static int PreOpenDurationHnx { get; set; }

        public static int MorningOpenDurationHnx { get; set; }

        public static int AfternoonOpenDurationHnx { get; set; }

        public static int PreCloseDurationHnx { get; set; }

        public static bool EnablePutATCbeforeHnx { get; set; }

        public static int TradingDurationHNX;

        public static int TradingDurationUpcomSession1;
        public static int TradingDurationUpcomSession2;

        public static char OrderSource;

        public static int OrderHistSource;

        public static int DealHistSource;

        public static int LimitQuantityAdvanceOrder;

        public static decimal VAT;
        public static string AllowIPRestartEtradeService;

        public static int TradingDurationHnxSS2;

        /// <summary>
        /// Initializes static members of the <see cref="AppConfig"/> class.
        /// </summary>
        static AppConfig()
        {
            try
            {
                System.Collections.Specialized.NameValueCollection collection = ConfigurationManager.AppSettings;

                if (collection.Count > 0)
                {
                    FisDbConnectionString = collection["FisDbConnectionString"] ?? string.Empty;
                    MsDbConnectionString = collection["MsDbConnectionString"] ?? string.Empty;
                    SbaConnectionString = collection["SbaConnectionString"] ?? string.Empty;
                    AliasInformix = collection["AliasInformix"] ?? string.Empty;

                    LinkOPSTraderID = collection["LinkOPSTraderID"] ?? string.Empty;
                    LinkOPSAddress = collection["LinkOPSAddress"] ?? string.Empty;
                    LinkOPSPort = collection["LinkOPSPort"] ?? string.Empty;
                    LinkOPSHeartBeat = collection["LinkOPSHeartBeat"] ?? string.Empty;
                    LinkOPSUserName = collection["LinkOPSUserName"] ?? string.Empty;
                    LinkOPSPass = collection["LinkOPSPass"] ?? string.Empty;
                    ServiceName = collection["ServiceName"] ?? string.Empty;

                    if (collection["LimitQuantityAdvanceOrder"] != null)
                    {
                        int limit = 0;
                        int.TryParse(collection["LimitQuantityAdvanceOrder"], out limit);
                        LimitQuantityAdvanceOrder = limit;
                    }
                    else
                    {
                        LimitQuantityAdvanceOrder = 0;
                    }

                    if (collection["VAT"] != null)
                    {
                        decimal vat = 0;
                        decimal.TryParse(collection["VAT"], out vat);
                        VAT = vat;
                    }
                    else
                    {
                        VAT = 0;
                    }


                    bool booleanVal;
                    CheckGWConnection = bool.TryParse(collection["CheckGWConnection"], out booleanVal)
                                            ? booleanVal
                                            : false;
                    //hose
                    if (!string.IsNullOrEmpty(collection["Preopen_DurationHose"]))
                    {
                        PreOpenDurationHose = int.Parse(collection["Preopen_DurationHose"]);
                    }

                    if (!string.IsNullOrEmpty(collection["Morning_Open_DurationHose"]))
                    {
                        MorningOpenDurationHose = int.Parse(collection["Morning_Open_DurationHose"]);
                    }

                    if (!string.IsNullOrEmpty(collection["Afternoon_Open_DurationHose"]))
                    {
                        AfternoonOpenDurationHose = int.Parse(collection["Afternoon_Open_DurationHose"]);
                    }

                    if (!string.IsNullOrEmpty(collection["Preclose_Duration"]))
                    {
                        PreCloseDurationHose = int.Parse(collection["Preclose_DurationHose"]);
                    }

                    EnablePutATCbeforeHose = bool.TryParse(collection["EnablePutATCbeforeHose"], out booleanVal)
                                            ? booleanVal
                                            : false;
                    //hnx
                    if (!string.IsNullOrEmpty(collection["Preopen_DurationHnx"]))
                    {
                        PreOpenDurationHnx = int.Parse(collection["Preopen_DurationHnx"]);
                    }

                    if (!string.IsNullOrEmpty(collection["Morning_Open_DurationHnx"]))
                    {
                        MorningOpenDurationHnx = int.Parse(collection["Morning_Open_DurationHnx"]);
                    }

                    if (!string.IsNullOrEmpty(collection["Afternoon_Open_DurationHnx"]))
                    {
                        AfternoonOpenDurationHnx = int.Parse(collection["Afternoon_Open_DurationHnx"]);
                    }

                    if (!string.IsNullOrEmpty(collection["Preclose_DurationHnx"]))
                    {
                        PreCloseDurationHnx = int.Parse(collection["Preclose_DurationHnx"]);
                    }

                    EnablePutATCbeforeHnx = bool.TryParse(collection["EnablePutATCbeforeHnx"], out booleanVal)
                                            ? booleanVal
                                            : false;
                    //
                    if (!string.IsNullOrEmpty(collection["Trading_Duration_HNX"]))
                    {
                        TradingDurationHNX = int.Parse(collection["Trading_Duration_HNX"]);
                    }
                    if (!string.IsNullOrEmpty(collection["Trading_Duration_UPCOM_SS1"]))
                    {
                        TradingDurationUpcomSession1 = int.Parse(collection["Trading_Duration_UPCOM_SS1"]);
                    }
                    if (!string.IsNullOrEmpty(collection["Trading_Duration_UPCOM_SS2"]))
                    {
                        TradingDurationUpcomSession2 = int.Parse(collection["Trading_Duration_UPCOM_SS2"]);
                    }


                    if (collection["OrderSource"] != null)
                    {
                        OrderSource = collection["OrderSource"][0];
                    }

                    if (!string.IsNullOrEmpty(collection["OrderHistSource"]))
                    {
                        OrderHistSource = int.Parse(collection["OrderHistSource"]);
                    }
                    if (!string.IsNullOrEmpty(collection["DealHistSource"]))
                    {
                        DealHistSource = int.Parse(collection["DealHistSource"]);
                    }
                    if (!string.IsNullOrEmpty(collection["AllowIPRestartEtradeService"]))
                    {
                        AllowIPRestartEtradeService = collection["AllowIPRestartEtradeService"];
                    }
                    if (!string.IsNullOrEmpty(collection["Trading_Duration_HNX_SS2"]))
                    {
                        TradingDurationHnxSS2 = int.Parse(collection["Trading_Duration_HNX_SS2"]);
                    }
                }
            }
            catch (Exception exception)
            {
                LogHandler.Log("AppConfig: EXCEPTION, ex = " + exception, "AppConfig.AppConfig()", TraceEventType.Error);
            }
        }
    }
}