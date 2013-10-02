	

#region Using Directives
using System;
using System.ComponentModel;
using System.Collections;
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
	/// An component type implementation of the 'Research' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class ResearchService : AccountManager.Services.ResearchServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the ResearchService class.
		/// </summary>
		public ResearchService() : base()
		{
		}
		#endregion Constructors
		
	}//End Class

} // end namespace
