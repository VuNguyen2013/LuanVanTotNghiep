	

#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Data;

using AccountManager.Entities;
using AccountManager.Entities.Validation;

using AccountManager.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace AccountManager.Services
{		
	/// <summary>
	/// An component type implementation of the 'BrokerPermission' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class BrokerPermissionService : AccountManager.Services.BrokerPermissionServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the BrokerPermissionService class.
		/// </summary>
		public BrokerPermissionService() : base()
		{
		}
		#endregion Constructors

        /// <summary>
        /// Gets the broker by permission.
        /// </summary>
        /// <param name="permissionID">The permission ID.</param>
        /// <returns></returns>
        public List<string> GetBrokerByPermission(int permissionID)
        {
            // Create dynamic query
            var whereClause = new StringBuilder();
            if (permissionID > 0)
            {
                whereClause.AppendFormat("PermissionID = {0} ", permissionID);
            }        
            int totalRecord;

            var list = GetPaged(whereClause.ToString(), "", 0, int.MaxValue, out totalRecord);
            var listPermissions = list.ToList();

            var returnObject = new List<string>();
            foreach (var permission in listPermissions)
            {
                returnObject.Add(permission.BrokerId);
            }
            return returnObject;
        }
	}//End Class

} // end namespace
