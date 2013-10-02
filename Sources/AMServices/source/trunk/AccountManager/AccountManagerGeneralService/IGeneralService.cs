﻿using System.Collections.Generic;
using AccountManager.Entities;
using ETradeCommon;
using ETradeFinance.Entities;

namespace AccountManagerGeneralService
{
    public interface IGeneralService
    {
        List<CashAdvanceHistory> GetListCashAdvanceHistory(string subAccountId, string fromDate, string toDate,
                                                           string contractNo, string sellDueDateFrom,
                                                           string sellDueDateTo, int status, int tradeType,
                                                           string brokerId, int pageIndex, int pageSize, out int count);

        List<CashAdvance> GetListCashAdvance(string subAccountId, string contractNo, int status,
                                             int tradeType, int pageIndex, int pageSize, out int count);

        long GetTotalUnfinishedXROrderRegisterAmount(long buyRightId, string subAccountId, string secSymbol,
                                                            string market);

        PagingObject<List<XrOrders>> GetListXROrderHist(long id, string subAccountId, string secSymbol,
                                                               string market, string fromDate, string toDate, int status,
                                                               string brokerID, string note, int pageIndex, int pageSize);

        int AuthenticateCustLogon(MainCustAccount mainCustAccount, string password, string updatedUserId);
    }
}