﻿using System;
using System.Diagnostics;
using ETradeCommon;

namespace ETradeAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if ((args != null) && (args.Length > 0))
                {
                    string argument = args[0];
                    int taskId = int.Parse(argument);
                    if (taskId == (int) Constants.TaskId.ACTIVATION)
                    {
                        LogHandler.Log("Activate service", "ETradeAutomation", TraceEventType.Information);
                        var etradeWebService = new ETradeWebService.ETradeServicesWebServices();
                        etradeWebService.Login("1", "1");//fake login
                        var rtWebService = new RTWebService.Service();
                        rtWebService.MarketStatus(1);
                    }
                    /*if (taskId == (int) Constants.TaskId.CONDITION_ORDER)
                    {
                        LogHandler.Log("Start putting condition order", "ETradeAutomation", TraceEventType.Information);
                        var etradeWebService = new ETradeWebService.ETradeServicesWebServices();
                        etradeWebService.PutConditionOrder();
                    }*/
                    if (taskId == (int) Constants.TaskId.CURRENCIES)
                    {
                        LogHandler.Log("Start getting currencies", "ETradeAutomation", TraceEventType.Information);
                        var currenciesParser = new CurrenciesParser();
                        currenciesParser.Parse();
                    }
                    if (taskId == (int)Constants.TaskId.CASH_ADVANCE_CLEAN_UP)
                    {
                        LogHandler.Log("Start cleaning up expired cash advance", "ETradeAutomation", TraceEventType.Information);
                        var amService = new AMServices.AccountManagerServices();
                        amService.RejectCashAdvanceExpired("Hết hạn(expired)");
                    }
                    if (taskId == (int) Constants.TaskId.RESET_CONDITION_ORDER)
                    {
                        LogHandler.Log("Start reseting condition order", "ETradeAutomation", TraceEventType.Information);
                        var etradeWebService = new ETradeWebService.ETradeServicesWebServices();
                        etradeWebService.ResetConditionOrder();
                    }
                    if(taskId == (int)Constants.TaskId.UPDATE_COMPANYINFO)
                    {
                        LogHandler.Log("Start update company info", "EtradeAutomation", TraceEventType.Information);
                        var rtService = new RTWebService.Service();
                        rtService.UpdateCompanyInfo();
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, ETradeCommon.Constants.EXCEPTION_POLICY);
            }
            
        }
    }
}
