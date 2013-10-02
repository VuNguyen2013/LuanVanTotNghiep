	

#region Using Directives
using System;
using AccountManager.Entities;
using ETradeCommon.Enums;

#endregion

namespace AccountManager.Services
{		
	/// <summary>
	/// An component type implementation of the 'Configurations' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class ConfigurationsService : AccountManager.Services.ConfigurationsServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the ConfigurationsService class.
		/// </summary>
		public ConfigurationsService() : base()
		{
		}
		#endregion Constructors

        /// <summary>
        /// Create configuration.
        /// </summary>
        /// <param name="name">Name of the configuration</param>
        /// <param name="value">Value of the configuration</param>
        /// <returns>
        /// <para>Result of creating configuration</para>
        /// <para>RET_CODE=EXISTED_DATA: Data is existing.</para>
        /// <para>RET_CODE=FAIL: Fail to create data.</para>
        /// <para>RET_CODE=SUCCESS: Create data successfully.</para>
        /// </returns>
        public int CreateConfiguration(string name, string value)
        {
            var configuration = GetByName(name);
            if (configuration != null)
            {
                return (int) CommonEnums.RET_CODE.EXISTED_DATA;
            }
            configuration = new Configurations {Name = name, Value = value};
            bool result = Insert(configuration);
            if (result)
            {
                return (int) CommonEnums.RET_CODE.SUCCESS;
            }
            return (int) CommonEnums.RET_CODE.FAIL;
        }

        /// <summary>
        /// Update configuration
        /// </summary>
        /// <param name="name">Name of the configuration</param>
        /// <param name="value">Value of the configuration</param>
        /// <returns>
        /// <para>Result of updating configuration</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=FAIL: Fail to update data.</para>
        /// <para>RET_CODE=SUCCESS: Update data successfully.</para>
        /// </returns>
        public int UpdateConfiguration(string name, string value)
        {
            var configuration = GetByName(name);
            if (configuration == null)
            {
                return (int) CommonEnums.RET_CODE.NO_EXISTED_DATA;
            }
            configuration.Value = value;
            bool result = Update(configuration);
            if (result)
            {
                return (int)CommonEnums.RET_CODE.SUCCESS;
            }
            return (int)CommonEnums.RET_CODE.FAIL;
        }
		
	}//End Class

} // end namespace
