// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CashServices.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the CashServices type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace ETradeCore.Services
{
    using ETradeCommon.Enums;

    using DataAccess;
    using DataAccess.SqlClient;
    using Entities;
    using ETradeFinance.Services;

    public class CashServices
    {
        readonly IFisCoreProvider _db2Provider = new SqlDb2Provider();
        readonly MarginServices marginSerive=new MarginServices();

        /// <summary>
        /// Gets the available cash.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="isConditionOrder">if set to <c>true</c> [is condition order].</param>
        /// <returns></returns>
        public CashAvailable GetAvailableCash(string accountNo, int accountType,bool isConditionOrder)
        {
            CashAvailable cashAvailable=new CashAvailable();
            if (accountType == (int)CommonEnums.ACCOUNT_TYPE.NORMAL)
            {
                cashAvailable = _db2Provider.GetCashAvailable4NormalAccount(accountNo);                               
            }

            if (accountType == (int)CommonEnums.ACCOUNT_TYPE.MARGIN)
            {                
                cashAvailable=_db2Provider.GetCashAvailable4MarginAccount(accountNo);                                     
            }

            if (isConditionOrder)
            {
                CashDueInfo cashDueInfo = _db2Provider.GetCashDue(accountNo);

                if (cashAvailable == null)
                {
                    return null;
                }

                if (cashDueInfo != null)
                {
                    cashAvailable.WTR_T1 = cashDueInfo.AMT_T1;
                    cashAvailable.WTR_T2 = cashDueInfo.AMT_T2;
                    cashAvailable.WTR_T3 = cashDueInfo.AMT_T3;
                }
            }

            //Not support other accountType
            return cashAvailable;
        }

        /// <summary>
        /// Gets the available cash.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="tradeDate">The trade date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="isConditionOrder">if set to <c>true</c> [is condition order].</param>
        /// <returns></returns>
        public CashAvailable GetAvailableCash(string accountNo, int accountType,string tradeDate,string symbol,bool isConditionOrder)
        {
            CashAvailable cashAvailable=new CashAvailable();
            if (accountType == (int)CommonEnums.ACCOUNT_TYPE.NORMAL)
            {
                cashAvailable= _db2Provider.GetCashAvailable4NormalAccount(accountNo);
            }

            if (accountType == (int)CommonEnums.ACCOUNT_TYPE.MARGIN)
            {
                cashAvailable = _db2Provider.GetCashAvailable4MarginAccount(accountNo);
                if(cashAvailable!=null)
                {
                    MaginSecInfo maginSecInfo = marginSerive.GetMaginSecInfo(tradeDate, symbol);
                    if (maginSecInfo != null)
                    {
                        cashAvailable.IMStock = maginSecInfo.IM;
                        //Caculate the StockPP = EE/IMStock. IM Stock get form maginsec view of BA_VIEW
                        if (cashAvailable != null && maginSecInfo != null)
                        {
                            if (maginSecInfo.IM != 0)
                                cashAvailable.PPStock = cashAvailable.EE / maginSecInfo.IM;
                            else
                            {
                                cashAvailable.PPStock = 0;
                            }
                        }
                    }
                }                                                                                 
            }
            if (isConditionOrder)
            {
                CashDueInfo cashDueInfo = _db2Provider.GetCashDue(accountNo);

                if (cashAvailable == null)
                {
                    return null;
                }

                if (cashDueInfo != null)
                {
                    cashAvailable.WTR_T1 = cashDueInfo.AMT_T1;
                    cashAvailable.WTR_T2 = cashDueInfo.AMT_T2;
                    cashAvailable.WTR_T3 = cashDueInfo.AMT_T3;
                }
            }
            return cashAvailable;
        }

        /// <summary>
        /// Gets the cash balance.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns></returns>
        public CashBalance GetCashBalance(string accountNo, int accountType)
        {
            var cashBalance = new CashBalance();

            if (accountType == (int)CommonEnums.ACCOUNT_TYPE.NORMAL)
            {
                cashBalance = _db2Provider.GetCashBalance4NormalAccount(accountNo);
                if(cashBalance!=null)
                {
                    FeeService feeService = new FeeService();
                    cashBalance.TotalBuy = feeService.BuyValueAfterFee(cashBalance.TotalBuy);
                    cashBalance.TotalSell = feeService.SelValueAfterFee(cashBalance.TotalSell);                                 
                }
            }
            else if (accountType == (int)CommonEnums.ACCOUNT_TYPE.MARGIN)
            {
                cashBalance = _db2Provider.GetCashBalance4MarginAccount(accountNo);                
            }

            CashDueInfo cashDueInfo = _db2Provider.GetCashDue(accountNo);

            if (cashBalance == null)
            {
                return null;
            }

            if (cashDueInfo != null)
            {                
                cashBalance.AMT_T1 = cashDueInfo.AMT_T1;
                cashBalance.AMT_T2 = cashDueInfo.AMT_T2;
                cashBalance.AMT_T3 = cashDueInfo.AMT_T3;                             
                cashBalance.OverDue = cashDueInfo.OverDue;
                cashBalance.Payment = cashDueInfo.Payment;
            }
            if (accountType == (int)CommonEnums.ACCOUNT_TYPE.NORMAL)
                cashBalance.WithDraw = cashBalance.BuyCredit - cashBalance.AMT_T1;

            if (accountType == (int)CommonEnums.ACCOUNT_TYPE.MARGIN)
                cashBalance.WithDraw = cashBalance.WithDraw < 0 ? 0 : cashBalance.WithDraw;

            return cashBalance;
        }
        
    }
}