// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EtradeGWCommonEnums.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Types of errors of EtradeGW
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeGWServices
{
    /// <summary>
    /// Types of errors of EtradeGW
    /// </summary>
    public enum ERROR_TYPE
    { 
        DATABASE_ERROR,
        SOCKET_ERROR,
        PIPE_ERROR,
        GENERAL_ERROR,
        INFORMATION
    }

 
    /// <summary>
    /// Action types of EtradeGW
    /// </summary>
    public enum ACTION_TYPE
    {
        LOGON,
        LOGOUT,
        NEW_ORDER,
        CANCEL_ORD,
        CHANGE_ACC
    }

    /*public enum ORDER_STATUS
    {
        ORD_NOTHING = -1,
        ORD_PENDING = 0,
        ORD_WAITING = 1,
        ORD_FINISHED = 2,
        ORD_REJECTED = 3,
        ORD_UNKNOWN,
    }*/

    public enum SOURCE_ID
    {
        FROM_FIS = 0,
        FROM_SET = 3
    }

    public enum TRANS_TYPE
    {
        /// <summary>
        /// Value = 0
        /// </summary>
        TRANS_NEW,
        /// <summary>
        /// Value = 1
        /// </summary>
        TRANS_CANCEL,
        /// <summary>
        /// Value = 2
        /// </summary>
        TRANS_CHANGE_ACC,
        /// <summary>
        /// Value = 3
        /// </summary>
        TRANS_CANCEL_WITHOUT_APPRO
    }

    public enum CENTER_TYPE
    {
        HOSE = 'O',
        HASTC = 'N',
        UPCOM = 'U'
    };
}