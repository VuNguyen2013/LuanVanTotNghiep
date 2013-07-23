// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountInfoDTO.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the AccountInfoDTO type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeServicesMock.DTO
{
    using System.Collections.Generic;
    using System;

    public class AccountInfoDTO
    {
        /// <summary>
        /// Gets or sets the list sub account.
        /// </summary>
        /// <value>The list sub account.</value>
        public List<String> ListSubAccount { get; set; }

        /// <summary>
        /// Gets or sets the name of the account.
        /// </summary>
        /// <value>The name of the account.</value>
        public String AccountName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is actived.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is actived; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsActived { get; set; }

        /// <summary>
        /// Gets or sets the lock account reason.
        /// </summary>
        /// <value>The lock account reason.</value>
        public Int32 LockAccountReason { get; set; }

        /// <summary>
        /// Gets or sets the type of the auth.
        /// </summary>
        /// <value>The type of the auth.</value>
        public Int32 AuthType { get; set; }

        /// <summary>
        /// Gets or sets the list permission.
        /// </summary>
        /// <value>The list permission.</value>
        public List<CustomerPermissionDTO> ListPermission { get; set; }
    }
}