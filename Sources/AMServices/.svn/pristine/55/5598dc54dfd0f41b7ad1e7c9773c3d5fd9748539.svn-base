using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ETradeCommon;
using ETradeCommon.Enums;

namespace AccountManagerGeneralService
{
    ///<summary>
    /// Authentication service for RSA
    ///</summary>
    public class RSAAuthenticationServices : IAuthenticationService
    {
        #region Acclnt.h
        // AceXxxx calls error codes
        public const int ACE_SUCCESS = 1;

        public const int ACE_ERR_START = 100;
        public const int ACE_ERR_INVALID_HANDLE = ACE_ERR_START + 1;
        public const int ACE_ERR_NOT_INITIALIZED = ACE_ERR_START + 2;

        /* these two used to be separate values, they are now obsolete but this */
        /* will let legacy code compile without modifications */
        public const int ACE_CHECK_INVALID_HANDLE = ACE_ERR_INVALID_HANDLE;
        public const int ACE_CLOSE_INVALID_HANDLE = ACE_ERR_INVALID_HANDLE;

        public const int ACE_PROCESSING = 150;

        public const int ACE_START = 200;
        public const int ACE_CFGFILE_NOT_FOUND = ACE_START + 1;
        public const int ACE_CFGFILE_READ_FAIL = ACE_START + 2;
        public const int ACE_EVENT_CREATE_FAIL = ACE_START + 3;
        public const int ACE_SEMAPHORE_CREATE_FAIL = ACE_START + 4;
        public const int ACE_THREAD_CREATE_FAIL = ACE_START + 5;
        public const int ACE_SOCKET_LIB_NOT_FOUND = ACE_START + 6;
        public const int ACE_PTHREAD_FAIL = ACE_START + 7;
        public const int ACE_PTHREAD_CREATE_FAIL = ACE_START + 8;
        public const int ACE_PTHREADATTR_FAIL = ACE_START + 9;
        public const int ACE_PTHREADATTR_CREATE_FAIL = ACE_START + 10;
        public const int ACE_PTHREADCONDVAR_CREATE_FAIL = ACE_START + 11;
        public const int ACE_PTHREADMUTEX_CREATE_FAIL = ACE_START + 12;

        public const int ACE_NET_START = 300;
        public const int ACE_NET_SEND_PACKET_FAIL = ACE_NET_START + 1;
        public const int ACE_NET_WAITING_TIMEOUT = ACE_NET_START + 2;

        public const int ACE_INIT_START = 400;
        public const int ACE_INIT_NO_RESOURCE = ACE_INIT_START + 2;
        public const int ACE_INIT_SOCKET_FAIL = ACE_INIT_START + 3;
        public const int ACE_INIT_SYNCRONIZE_FAIL = ACE_INIT_START + 4;

        public const int ACE_CHECK_START = 500;
        public const int ACE_CHECK_PIN_REQ_NOT_KNOWN = ACE_CHECK_START + 2;

        public const int ACE_TOO_MANY_CALLERS = 700;
        public const int ACE_NOT_ENOUGH_STORAGE = 750;
        public const int ACE_INVALID_ARG = 800;

        public const int ACE_UNDEFINED = 900;
        public const int ACE_UNDEFINED_USERNAME = ACE_UNDEFINED + 1;
        public const int ACE_UNDEFINED_PASSCODE = ACE_UNDEFINED + 2;
        public const int ACE_UNDEFINED_NEXT_PASSCODE = ACE_UNDEFINED + 3;
        public const int ACE_UNDEFINED_PIN = ACE_UNDEFINED + 4;
        public const int ACE_UNDEFINED_CLIENTADDR = ACE_UNDEFINED + 5;

        #endregion

        public const int ACE_DA_START = 1000;
        public const int ACE_DA_INVALID_PASSWORD = ACE_DA_START + 1;


        /* 
         * Additional RSA_DA_ATTR used in AceGetDAAuthData (really to get
         * something out of the sUSER structure).
         */
        public const int RSA_DA_ATTR_DA_HANDLE = -1;
        public const int RSA_DA_ATTR_IS_DISCONNECTED = -2;
        public const int RSA_DA_ATTR_DA_ADDRESS = -3;
        public const int RSA_DA_ATTR_DA_PORT = -4;
        public const int RSA_DA_ATTR_DA_STATE = -5; /* Same as AceGetDAuthenticationStatus */
        public const int RSA_DA_ATTR_DA_STATUS = -6; /* Status of the last call to AceDACheck */

        // SDI Size
        public const int LENID = 6;
        public const int LENPIN = 12;
        public const int LENSER = 12;
        public const int LENLOGID = 32;
        public const int LENPRNST = 16;
        public const int LENPATH = 64;
        public const int LENTITLE = 40;
        public const int LENACMFILE = 64;
        public const int LENHOSTNAME = 64;
        public const int LENACMNAME = 32;
        public const int LENUSERNAME = 64;
        public const int LENUSERNAME_61 = 128;
        public const int LENSECRET = 16;
        public const int LENMAXPIN = 16;
        public const int LENMAXPIN_61 = 32;
        public const int LENSEQNUM = 8;

        public const int LENMSGSTRING = 256;
        public const int LENFULLPATH = 1024;
        public const int LENAUCHBUFF = 1024;
        public const int LENSYSCMD = 2048;
        public const int SOCKET_BUFFER_LEN = 4096;
        public const int MAX_RESPONSE_DATA = 100;
        public const int LENREALMID = 24;

        public const int MAX_LENPRN = 8;	/*Max size of PRN */

        public const int BATCH_NUMPRNS = 9;    /*  for batch mode  */
        public const int SPECIAL_NUMPRNS = 7;    /*  for special mode  */

        public const int LENPASCD = 20;            /* parsed passcode 8 + 1 + 8 + slop     */
        public const int LENSHELL = LENACMNAME;    /* better name for api users            */
        public const int LENCERTIFICATE = 35;
        public const int BUFFER_LEN = 512;

        /* Action codes for user selectable pins. */

        public const int SELECT_PIN = 0;
        public const int DEFER_PIN = 1;

        /* options codes for user selectable pins */

        public const int CANNOT_CHOOSE_PIN = 0;
        public const int USER_SELECTABLE = 1;
        public const int MUST_CHOOSE_PIN = 2;

        /* sdtermio flags */
        public const int SDI_ECHO = 0;
        public const int SDI_NO_ECHO = 1;

        /* Length that user data gets hashed into */
        public const int CERT_USER_DATA_HASH_LEN = 36;

        public static uint INFINITE = 0xFFFFFFFF;
        #region sdacmvls.h
        /* message status codes for ACM_RESP_MSG.status */

        public const int ACM_OK = 0;
        public const int ACM_ACCESS_DENIED = 1;
        public const int ACM_NEXT_CODE_REQUIRED = 2;
        public const int ACM_NEXT_CODE_BAD = 4;
        public const int ACM_NEW_PIN_REQUIRED = 5;
        public const int ACM_NEW_PIN_ACCEPTED = 6;
        public const int ACM_NEW_PIN_REJECTED = 7;
        public const int ACM_SHELL_OK = 8;
        public const int ACM_SHELL_BAD = 9;
        public const int ACM_TIME_OK = 10;
        public const int ACM_SUSPECT_ACK = 13;

        public const int ACM_LOG_ACK = 16;

        /* client only values */
        public const int ACM_NO_SERVER = 23;
        public const int ACM_INVALID_SERVER = 24;

        /* A/S V5.0 - new status values */
        public const int ACM_OK_2 = 25;
        public const int ACM_DOWNGRADE = 26;
        public const int ACM_ACK_NAMELOCK = 27;

        /* A/S V6.1 - new status values */
        public const int ACM_OK_5 = 28;
        public const int ACM_EAP_INVALID_PEPPER = 29;

        /* sd_auth result; no chars entered, ignore entry */
        public const int ACM_ENTRY_ERR = -1;
        #endregion

        public const string AUTH_CHALLENGE_USERNAME_STR = "Enter USERNAME: ";
        public const string AUTH_CHALLENGE_PASSCODE_STR = "Enter PASSCODE: ";
        public const string AUTH_CHALLENGE_NEXT_CODE_STR = "Wait for the tokencode to change,\nthen enter the new tokencode: ";
        public const string AUTH_CHALLENGE_NEW_PIN_STR = "You must select a new PIN.\nDo you want the system to generate\nyour new PIN? (y/n) [n] ";
        public const string AUTH_CHALLENGE_NEW_PIN_ALPHA_SAME_STR = "Enter a new PIN of %d alphanumeric\ncharacters: ";
        public const string AUTH_CHALLENGE_NEW_PIN_ALPHA_STR = "Enter a new PIN between %d and %d alphanumeric\ncharacters: ";
        public const string AUTH_CHALLENGE_NEW_PIN_DIGITS_SAME_STR = "Enter a new PIN of %d digits: ";
        public const string AUTH_CHALLENGE_NEW_PIN_DIGITS_STR = "Enter a new PIN between %d and %d digits: ";
        public const string AUTH_CHALLENGE_NEW_PIN_SYS_STR = "To continue, you must accept a new PIN generated\nby the system. Are you ready to have the\nsystem generate your PIN? (y/n) [n] ";
        public const string AUTH_CHALLENGE_CONFIRM_PIN_STR = "Re-enter new PIN to confirm: ";
        public const string AUTH_CHALLENGE_NEW_SYS_PIN_PASSCODE_STR = "Wait for the tokencode to change,\nthen enter a new PASSCODE: ";
        public const string AUTH_CHALLENGE_NEW_USER_PIN_PASSCODE_STR = "PIN accepted. Wait for the tokencode to\nchange, then enter a new PASSCODE: ";
        public const string AUTH_CHALLENGE_SUCCESS_STR = "PASSCODE accepted.\n";
        public const string AUTH_CHALLENGE_ACCESS_DENIED_STR = "Access denied.\n";
        public const string AUTH_ERROR_MISC_STR = "Unexpected authentication error.\n";
        public const string AUTH_ERROR_BAD_PASSCODE_STR = "Access denied.\n";
        public const string AUTH_ERROR_BAD_TOKENCODE_STR = "Access denied.\n";
        public const string AUTH_ERROR_INVALID_PIN_STR = "Invalid PIN.\n";
        public const string AUTH_ERROR_CONFIRM_PIN_STR = "PIN did not match confirmation. Press Enter to continue.\n";
        public const string AUTH_ERROR_INVALID_ARG_STR = "Invalid argument.\n";
        public const string AUTH_CHALLENGE_NEW_PIN_USER_STR = "To continue you must enter a new PIN.\nAre you ready to enter a new PIN? (y/n) [n]";
        public const string AUTH_CHALLENGE_NEW_SYS_PIN_DISPLAY_STR = "\n\nYour screen will automatically clear in 10 seconds.\nYour new PIN is: %s\n";

        public delegate void ACECallback(int hdl);

        // ACE API
        [DllImport("aceclnt.dll", EntryPoint = "AceInitialize",
        ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceInitialize(); // return bool

        [DllImport("aceclnt.dll", EntryPoint = "AceInitializeEx",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceInitializeEx(string configFile, string reserved1, int reserved2); // return bool

        //bool WINAPI AceShutdown(void (WINAPI*appCallback)(SDI_HANDLE))
        [DllImport("aceclnt.dll", EntryPoint = "AceShutdown",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceShutdown(ACECallback appCallback); // return bool

        //bool WINAPI AceCleanup(void (WINAPI*appCallback)(SDI_HANDLE))
        [DllImport("aceclnt.dll", EntryPoint = "AceCleanup",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceCleanup(ACECallback appCallback); // return bool

        //int  WINAPI AceLock ( SDI_HANDLE, ACECALLBACK )
        [DllImport("aceclnt.dll", EntryPoint = "AceLock",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceLock(int sdiHandle, ACECallback appCallback); // return int

        //int  WINAPI AceCheck ( SDI_HANDLE, ACECALLBACK )
        [DllImport("aceclnt.dll", EntryPoint = "AceCheck",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceCheck(int sdiHandle, ACECallback appCallback); // return int

        //int  WINAPI AceClientCheck ( SDI_HANDLE, ACECALLBACK )
        [DllImport("aceclnt.dll", EntryPoint = "AceClientCheck",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceClientCheck(int sdiHandle, ACECallback appCallback); // return int

        //int  WINAPI AceSendNextPasscode ( SDI_HANDLE, ACECALLBACK )
        [DllImport("aceclnt.dll", EntryPoint = "AceSendNextPasscode",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSendNextPasscode(int sdiHandle, ACECallback appCallback); // return int

        //int  WINAPI AceSendPin ( SDI_HANDLE, ACECALLBACK )
        [DllImport("aceclnt.dll", EntryPoint = "AceSendPin",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSendPin(int sdiHandle, ACECallback appCallback); // return int

        //int  WINAPI AceCancelPin ( SDI_HANDLE,  ACECALLBACK )
        [DllImport("aceclnt.dll", EntryPoint = "AceCancelPin",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceCancelPin(int sdiHandle, ACECallback appCallback); // return int

        //int WINAPI AceClose(SDI_HANDLE SdiHandle, void (WINAPI*appCallback)(SDI_HANDLE SdiHandle))
        [DllImport("aceclnt.dll", EntryPoint = "AceClose",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceClose(int sdiHandle, ACECallback appCallback); // return int

        //int  WINAPI AceGetAuthenticationStatus ( SDI_HANDLE, SD_I32 * )
        [DllImport("aceclnt.dll", EntryPoint = "AceGetAuthenticationStatus",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetAuthenticationStatus(int sdiHandle, ref int val); // return int

        //int  WINAPI AceGetMinPinLen ( SDI_HANDLE, SD_CHAR * )
        [DllImport("aceclnt.dll", EntryPoint = "AceGetMinPinLen",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetMinPinLen(int sdiHandle, string val); // return int

        //int  WINAPI AceGetMaxPinLen ( SDI_HANDLE, SD_CHAR * )
        [DllImport("aceclnt.dll", EntryPoint = "AceGetMaxPinLen",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetMaxPinLen(int sdiHandle, string val); // return int

        //int  WINAPI AceGetUserSelectable( SDI_HANDLE, SD_CHAR * )
        [DllImport("aceclnt.dll", EntryPoint = "AceGetUserSelectable",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetUserSelectable(int sdiHandle, string val); // return int

        //int WINAPI AceGetAlphanumeric ( SDI_HANDLE, SD_CHAR * )
        [DllImport("aceclnt.dll", EntryPoint = "AceGetAlphanumeric",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetAlphanumeric(int sdiHandle, string val); // return int

        //int  WINAPI AceGetSystemPin ( SDI_HANDLE, SD_CHAR * )
        [DllImport("aceclnt.dll", EntryPoint = "AceGetSystemPin",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetSystemPin(int sdiHandle, ref string val); // return int

        //int  WINAPI AceGetLoginPW ( SDI_HANDLE, SD_CHAR *, SD_U32 * )
        [DllImport("aceclnt.dll", EntryPoint = "AceGetLoginPW",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetLoginPW(int sdiHandle, ref string pchPWBuffer, ref int pu32PWLen); // return int

        //int  WINAPI AceSetLoginPW ( SDI_HANDLE, SD_CHAR * , SD_U32 )
        [DllImport("aceclnt.dll", EntryPoint = "AceSetLoginPW",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetLoginPW(int sdiHandle, string pchPWBuffer, int pu32PWLen); // return int

        //int WINAPI AceGetDAuthenticationStatus(SDI_HANDLE, INT32BIT *)
        [DllImport("aceclnt.dll", EntryPoint = "AceGetDAuthenticationStatus",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetDAuthenticationStatus(int sdiHandle, ref int piStatus); // return int

        //int WINAPI AceGetDAAuthData ( SDI_HANDLE, RSA_DA_ATTR, void *, SD_U32*)

        //int WINAPI AceGetAuthAttr	(SDI_HANDLE, RSA_AUTH_GET_ATTR, void *, SD_U32 *)

        //int  WINAPI AceGetTime ( SDI_HANDLE, SD_I32 * )
        [DllImport("aceclnt.dll", EntryPoint = "AceGetTime",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetTime(int sdiHandle, ref int val); // return int

        //int  WINAPI AceGetShell ( SDI_HANDLE, SD_CHAR * )
        [DllImport("aceclnt.dll", EntryPoint = "AceGetShell",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetShell(int sdiHandle, ref string shell); // return int

        //int  WINAPI AceGetPepperPolicy (SDI_HANDLE, char *, char *)
        [DllImport("aceclnt.dll", EntryPoint = "AceGetPepperPolicy",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetPepperPolicy(int sdiHandle, ref string min, ref string max); // return int

        //int  WINAPI AceGetIterCountPolicy(SDI_HANDLE, INT32BIT *, INT32BIT *)
        [DllImport("aceclnt.dll", EntryPoint = "AceGetIterCountPolicy",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetIterCountPolicy(int sdiHandle, ref int min, ref int max); // return int

        //int  WINAPI AceGetRealmID ( SDI_HANDLE, char* )
        [DllImport("aceclnt.dll", EntryPoint = "AceGetRealmID",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetRealmID(int sdiHandle, ref string pRealmID); // return int

        //int  WINAPI AceSetPasscode ( SDI_HANDLE, SD_CHAR * )
        [DllImport("aceclnt.dll", EntryPoint = "AceSetPasscode",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetPasscode(int sdiHandle, string passcode); // return int

        //int  WINAPI AceSetCredential ( SDI_HANDLE, RSA_AUTH_CRED_TYPE, void *, SD_U32)
        [DllImport("aceclnt.dll", EntryPoint = "AceSetCredential",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetCredential(int sdiHandle, int credType, int credBuf, int credLen); // return int

        //int  WINAPI AceSetUsername ( SDI_HANDLE, SD_CHAR * )
        [DllImport("aceclnt.dll", EntryPoint = "AceSetUsername",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetUsername(int sdiHandle, string username); // return int

        //int  WINAPI AceSetClientAddr ( SDI_HANDLE, SD_U32 )
        [DllImport("aceclnt.dll", EntryPoint = "AceSetClientAddr",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetClientAddr(int sdiHandle, int addr); // return int

        //int  WINAPI AceSetNextPasscode ( SDI_HANDLE, SD_CHAR * )
        [DllImport("aceclnt.dll", EntryPoint = "AceSetNextPasscode",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetNextPasscode(int sdiHandle, string passcode); // return int

        //int  WINAPI AceSetNextCredential( SDI_HANDLE, void *, SD_U32 );
        [DllImport("aceclnt.dll", EntryPoint = "AceSetNextCredential",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetNextCredential(int sdiHandle, int nextBuf, int nextLen); // return int

        //int  WINAPI AceSetPin ( SDI_HANDLE, SD_CHAR * )
        [DllImport("aceclnt.dll", EntryPoint = "AceSetPin",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetPin(int sdiHandle, string pin); // return int

        //int  WINAPI AceSetPinCredential ( SDI_HANDLE, void *, SD_U32 )
        [DllImport("aceclnt.dll", EntryPoint = "AceSetPinCredential",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetPinCredential(int sdiHandle, int pinBuf, int bufLen); // return int

        //int  WINAPI AceSetUserClientAddress ( SDI_HANDLE, SD_UCHAR * );
        [DllImport("aceclnt.dll", EntryPoint = "AceSetUserClientAddress",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetUserClientAddress(int sdiHandle, string val); // return int

        //int  WINAPI AceSetUserData ( SDI_HANDLE, void * )
        [DllImport("aceclnt.dll", EntryPoint = "AceSetUserData",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetUserData(int sdiHandle, int userData); // return int

        //int  WINAPI AceSetTimeout ( SDI_HANDLE, time_t, ACECALLBACK )
        [DllImport("aceclnt.dll", EntryPoint = "AceSetTimeout",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetTimeout(int sdiHandle, int lifetime, ACECallback appCallback); // return int

        //int	 WINAPI AceSetAuthAttr ( SDI_HANDLE, RSA_AUTH_SET_ATTR, void *, SD_U32)
        [DllImport("aceclnt.dll", EntryPoint = "AceSetAuthAttr",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceSetAuthAttr(int sdiHandle, int authAttribute, int attrValue, int attrSize); // return int

        //SD_ERROR WINAPI AceStartAuth(LP_SDI_HANDLE, SD_CHAR *userID, SD_I32 userIDLen, SD_BOOL *moreData, SD_BOOL *noEcho, SD_I32 *respTimeout,  
        //                             SD_I32 *nextRespLen, SD_CHAR *promptStr, SD_I32 *promptStrLen);
        [DllImport("aceclnt.dll", EntryPoint = "AceStartAuth",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceStartAuth(ref int sdiHandle, string userID, int userIDLen, ref int moreData, ref int noEcho, ref int respTimeout,
                                              ref int nextRespLen, [MarshalAs(UnmanagedType.LPArray)] byte[] promptStr, ref int promptStrLen); // return int

        //SD_ERROR WINAPI AceContinueAuth(LP_SDI_HANDLE, SD_CHAR *userID, SD_I32 userIDLen, SD_BOOL *moreData, SD_BOOL *noEcho, SD_I32 *respTimeout,  
        //                                SD_I32 *nextRespLen, SD_CHAR *promptStr, SD_I32 *promptStrLen);

        [DllImport("aceclnt.dll", EntryPoint = "AceContinueAuth",
        ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceContinueAuth(int sdiHandle, string userID, int userIDLen, ref int moreData, ref int noEcho, ref int respTimeout,
                                                 ref  int nextRespLen, [MarshalAs(UnmanagedType.LPArray)] byte[] promptStr, ref int promptStrLen); // return int

        //SD_ERROR WINAPI AceCloseAuth(SDI_HANDLE);
        [DllImport("aceclnt.dll", EntryPoint = "AceCloseAuth",
        ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceCloseAuth(int sdiHandle); // return int

        ///<summary>
        /// Authenticate using RSA
        /// this function is a demonstration of the use of the new synchronous API
        /// defined by the calls AceStartAuth(), AceContinueAuth(), and AceCloseAuth().
        /// Please notice the use of the AceGetAuthenticationStatus() call before the
        /// AceCloseAuth() call to determine the final authentication result.
        ///</summary>
        ///<param name="username"></param>
        ///<param name="password"></param>
        ///<returns></returns>
        public int Authenticate(string username, string password)
        {
            int aceHdl = 0;

            int promptLen = BUFFER_LEN;
            int nextRespLen = 0;
            int respTimeout = 0;
            int moreFlag = 0;
            int noechoFlag = 0;
            int authStatus = ACM_ACCESS_DENIED;

            if (AceInitialize() == 0)
            {
                LogHandler.Log("AceInitialize failed", GetType() + ".Authenticate()", TraceEventType.Error);

                return (int) CommonEnums.RET_CODE.SYSTEM_ERROR;
            }

            var promptArr = new byte[BUFFER_LEN];

            /*The AceStartAuth function provides the first step in authenticating with the 
                RSA SecurID protocol using the synchronous API. 
              If the function returns successfully, then your code calls AceContinueAuth with the 
                required passcode. Your code continues to call AceContinueAuth until no more data 
                is required by the authentication context, or an error occurs. 
              Whenever AceStartAuth returns successfully, a call to AceCloseAuth must 
                eventually be made for the specific authentication context.*/
            int retVal = AceStartAuth(ref aceHdl, username, username.Length,
                                      ref moreFlag, ref noechoFlag, ref respTimeout, ref nextRespLen,
                                      promptArr, ref promptLen);
            string promptStr = Utils.GetString(promptArr).Substring(0, promptLen - 1);

            if (retVal != ACM_OK)
            {
                LogHandler.Log("AceStartAuth failed: " + promptStr, GetType() + ".Authenticate()", TraceEventType.Error);

                return (int)CommonEnums.RET_CODE.INCORRECT_USER_PASSWORD;
            }

            try
            {
                // loop until no more data is requested
                /*Your code must continue to supply data to the authentication context through this 
                    function as long as the moreData argument is set to True. The promptStr argument 
                    contains the string used to display the next response message to the user who is 
                    attempting authentication. This string, which is set by the API, contains either a 
                    prompt for the user (for example, “Enter next tokencode:”) or a statement intended to 
                    provide status information to the user (for example, “Access denied.”).*/
                while (moreFlag == 1)
                {
                    // a system PIN is about to be displayed
                    LogHandler.Log("Message: " + promptStr, GetType() + ".Authenticate()", TraceEventType.Information);
                    if (respTimeout == 10)
                    {
                        LogHandler.Log("Wait 10s: " + promptStr, GetType() + ".Authenticate()", TraceEventType.Information);
                        // wait for 10 seconds
                        System.Threading.Thread.Sleep(respTimeout * 1000);
                    }

                    promptLen = BUFFER_LEN;

                    retVal = AceContinueAuth(aceHdl, password, password.Length,
                                             ref moreFlag, ref noechoFlag, ref respTimeout, ref nextRespLen,
                                             promptArr, ref promptLen);
                    LogHandler.Log("RetVal: " + retVal, GetType() + ".Authenticate()", TraceEventType.Information);
                    promptStr = Utils.GetString(promptArr).Substring(0, promptLen - 1);
                }

                // we need to call AceGetAuthenticationStatus() to retrieve
                // the final result. the return code from AceContinueAuth()
                // indicates the success/failure of the call and not of the
                // authentication request.

                AceGetAuthenticationStatus(aceHdl, ref authStatus);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception, Constants.EXCEPTION_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
            finally
            {
                // close authentication context
                AceCloseAuth(aceHdl);
            }

            if (authStatus == ACM_OK)
            {
                return (int)CommonEnums.RET_CODE.SUCCESS;
            }
            LogHandler.Log("Authentication failed: " + promptStr + ", Status " + authStatus, GetType() + ".Authenticate()", TraceEventType.Error);
            return (int)CommonEnums.RET_CODE.INCORRECT_USER_PASSWORD;
            
        }
    }
}
