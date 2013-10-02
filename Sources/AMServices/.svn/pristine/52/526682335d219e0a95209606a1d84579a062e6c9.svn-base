	

#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AccountManager.DataAccess.Bases;
using AccountManager.Entities;

using AccountManager.DataAccess;


#endregion

namespace AccountManager.Services
{		
	/// <summary>
	/// An component type implementation of the 'Language' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class LanguageService : LanguageServiceBase
	{		
        private static readonly bool noTranByDefault = false;
        private static readonly string layerExceptionPolicy = "ServiceLayerExceptionPolicy";

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <param name="languageId">The language id.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public Language GetLanguage(string languageId)
        {
            #region Security check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("AuthenticateCustLogon");
            #endregion Security check

            #region Initialisation
            Language language = null;
            TransactionManager transactionManager = null;
            NetTiersProvider dataProvider;
            #endregion Initialisation

            try
            {
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;
                // Get MainCustAccount information from database.
                language = dataProvider.LanguageProvider.GetByLanguageId(transactionManager, languageId);

            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }

            return language;
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="languageId">The language id.</param>
        /// <param name="languageName">Name of the language.</param>
        /// <returns></returns>
        public List<Language> GetList(string languageId, string languageName)
        {
            // Create dynamic query
            var whereClause = new StringBuilder();
            if (!string.IsNullOrEmpty(languageId))
            {
                whereClause.AppendFormat("AND LanguageId LIKE N'%{0}%' ", languageId);
            }

            if (!string.IsNullOrEmpty(languageName))
            {
                whereClause.AppendFormat("AND LanguageName LIKE N'%{0}%' ", languageName);
            }

            string finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
            {
                finalWhereClause = finalWhereClause.Substring(4);
            }

            int totalRecord;
            var list = GetPaged(finalWhereClause, "LanguageName DESC", 0, int.MaxValue, out totalRecord);
            return list.ToList();
        }
		
	}//End Class

} // end namespace
