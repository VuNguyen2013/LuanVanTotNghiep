// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogonDTO.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the LogonDTO type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeServicesMock.DTO
{
    public class LogonDTO
    {
        /// <summary>
        /// Gets or sets the session id.
        /// </summary>
        /// <value>The session id.</value>
        public System.String SessionId { get; set; }

        /// <summary>
        /// Gets or sets the OTP.
        /// </summary>
        /// <value>The OTP.</value>
        public System.String OTP { get; set; }
    }
}