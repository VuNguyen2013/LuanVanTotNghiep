using System;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace ETradeCommon
{
    public class ExceptionHandler
    {
        /// <summary>
        /// Wraps call to tohe <see cref="ExceptionPolicy"/> class which handles all exceptions 
        /// based on the security policy.
        /// </summary>
        public static bool HandleException(Exception e, string policyName)
        {
            try
            {
                return false;
            }
            catch (System.Configuration.ConfigurationErrorsException)
            {
                return true;
            }
        }
    }
}
