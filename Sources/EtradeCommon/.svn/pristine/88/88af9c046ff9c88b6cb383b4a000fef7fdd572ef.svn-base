// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResultObject.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ResultObject type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCommon
{
    using Enums;

    /// <summary>
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ResultObject<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultObject&lt;T&gt;"/> class.
        /// </summary>
        public ResultObject()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultObject&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public ResultObject(T data)
        {
            Result = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultObject&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="returnCode">The return code.</param>
        public ResultObject(CommonEnums.RET_CODE returnCode)
        {
            RetCode = returnCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultObject&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="returnCode">The return code.</param>
        public ResultObject(T data, CommonEnums.RET_CODE returnCode)
        {
            Result = data;

            RetCode = returnCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultObject&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="returnCode">The return code.</param>
        /// <param name="errorMsg">The error MSG.</param>
        public ResultObject(T data, CommonEnums.RET_CODE returnCode, string errorMsg)
        {
            Result = data;

            RetCode = returnCode;

            ErrorMessage = errorMsg;
        }

        /// <summary>
        /// Gets or sets the ret code.
        /// </summary>
        /// <value>The ret code.</value>
        public CommonEnums.RET_CODE RetCode { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public T Result { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }
    }
}