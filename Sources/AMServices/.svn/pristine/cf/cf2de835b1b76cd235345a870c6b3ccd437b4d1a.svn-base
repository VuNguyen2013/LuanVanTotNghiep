using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using ETradeCommon;

namespace AccountManager.Services
{
    class AuthenticationService : IAuthenticationService
    {
        private const int BUFFER_LEN = 512;

        // ACM
        /* message status codes for ACM_RESP_MSG.status */

        private const int ACM_OK = 0;
        private const int ACM_ACCESS_DENIED = 1;

        [StructLayout(LayoutKind.Sequential)]
        public class SdEventS
        {
            public IntPtr CondVar;    // an event object in NT, a condition variable in UNIX
        };

        [StructLayout(LayoutKind.Sequential)]
        public class EventData
        {
            public SdEventS Event;
            public int asynchAceRet;
            public int aceHdl;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 65)]
            public string user;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
            public string prn;
        };

        /* Holds pin limits and related values */
        [StructLayout(LayoutKind.Sequential)]
        public struct SdPin
        {
            public byte Min;
            public byte Max;
            public byte Selectable;
            public byte Alphanumeric;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string System;
        };

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

        //int WINAPI AceInit(LP_SDI_HANDLE pSdiHandle, unsigned int userData, void (WINAPI*appCallback)(SDI_HANDLE))
        [DllImport("aceclnt.dll", EntryPoint = "AceInit",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceInit(ref int pSdiHandle, EventData userData, ACECallback appCallback); // return int

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

        //int WINAPI AceClose(SDI_HANDLE sdiHandle, void (WINAPI*appCallback)(SDI_HANDLE sdiHandle))
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

        //int  WINAPI AceGetPinParams ( SDI_HANDLE, SD_PIN* )
        [DllImport("aceclnt.dll", EntryPoint = "AceGetPinParams",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetPinParams(int sdiHandle, ref SdPin pin); // return int

        //int  WINAPI AceGetUserData ( SDI_HANDLE, void ** )
        [DllImport("aceclnt.dll", EntryPoint = "AceGetUserData",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AceGetUserData(int sdiHandle, ref EventData val); // return int

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


        ///////////////////////////////////////////////////////////////////////////////
        // this function is a demonstration of the use of the new synchronous API
        // defined by the calls AceStartAuth(), AceContinueAuth(), and AceCloseAuth().
        // Please notice the use of the AceGetAuthenticationStatus() call before the
        // AceCloseAuth() call to determine the final authentication result.
        //
        public bool Authenticate(string username, string password)
        {
            int aceHdl = 0;

            int promptLen = BUFFER_LEN;
            int nextRespLen = 0;
            int respTimeout = 0;
            int moreFlag = 0;
            int noechoFlag = 0;

            if (AceInitialize() == 0)
            {
                LogHandler.Log("AceInitialize failed", GetType() + ".Authenticate()", TraceEventType.Error);
                return false;
            }

            var promptArr = new byte[BUFFER_LEN];

            int retVal = AceStartAuth(ref aceHdl, username, username.Length,
                                      ref moreFlag, ref noechoFlag, ref respTimeout, ref nextRespLen,
                                      promptArr, ref promptLen);
            string promptStr = Encoding.ASCII.GetString(promptArr);
            promptStr = promptStr.Substring(0, promptLen - 1);

            if (retVal != ACM_OK)
            {
                LogHandler.Log(promptStr, GetType() + ".Authenticate()", TraceEventType.Error);
                return false;
            }

            // loop until no more data is requested
            while (moreFlag == 1)
            {
                // a system PIN is about to be displayed
                if (respTimeout == 10)
                {
                    LogHandler.Log("Wait 10s: " + promptStr, GetType() + ".Authenticate()", TraceEventType.Information);
                    // wait for 10 seconds
                    System.Threading.Thread.Sleep(10 * 1000);
                }

                promptLen = BUFFER_LEN;

                AceContinueAuth(aceHdl, password, password.Length,
                                         ref moreFlag, ref noechoFlag, ref respTimeout, ref nextRespLen,
                                         promptArr, ref promptLen);

                promptStr = Encoding.ASCII.GetString(promptArr);
                promptStr = promptStr.Substring(0, promptLen - 1);
            }

            // we need to call AceGetAuthenticationStatus() to retrieve
            // the final result. the return code from AceContinueAuth()
            // indicates the success/failure of the call and not of the
            // authentication request.
            int authStatus = ACM_ACCESS_DENIED;

            if (moreFlag == 1)       // the loop was broken by I/O error?
            {
                LogHandler.Log("Access denied.", GetType() + ".Authenticate()", TraceEventType.Error);
            }
            else
            {
                AceGetAuthenticationStatus(aceHdl, ref authStatus);
            }

            // close authentication context
            AceCloseAuth(aceHdl);

            // return true if status is ACM_OK
            return authStatus == ACM_OK;
        }
    }
}
