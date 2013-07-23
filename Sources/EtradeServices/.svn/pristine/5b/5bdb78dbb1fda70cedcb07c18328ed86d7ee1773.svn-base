// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETradeGW.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ETradeGW type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using ETradeCommon.Enums;

namespace ETradeGWServices
{
    using System;

    using ETradeCommon;

    using ETradeOrders.Entities;
    using ETradeOrders.Services;

    public class ETradeGW
    {
        readonly ExecOrderService _execOrderService = new ExecOrderService();
        readonly static MessageHandler MessageHandler = new MessageHandler();

        /// <summary>
        /// Puts the order.
        /// </summary>
        /// <param name="market">The market.</param>
        /// <param name="orderSession">The order session.</param>
        /// <param name="accountId">The account id.</param>
        /// <param name="secSymbol">The security symbol.</param>
        /// <param name="side">The side.</param>
        /// <param name="volume">The volume.</param>
        /// <param name="price">The price.</param>
        /// <param name="conPrice">The con price.</param>
        /// <param name="avgPrice">The avg price.</param>
        /// <param name="conditionOrderId"></param>
        /// <param name="orderID">Order id.</param>
        /// <param name="outExecOrder">Returned order value.</param>
        /// <param name="orderSource">Order source. From web or mobile, ...</param>
        /// <returns>
        /// <para>Result of putting order.</para>
        /// <para>RET_CODE=ERROR_GW_NOT_CONNECTED: LinkOPS hasn't been connected.</para>
        /// <para>RET_CODE=ERROR_GW_NOT_SEND: Sending message failed.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public CommonEnums.RET_CODE PutOrder(int market, CommonEnums.ORDER_SESSION orderSession, string accountId, string secSymbol, 
            char side, int volume, decimal price, char conPrice, decimal avgPrice, long? conditionOrderId, out int orderID, out ExecOrder outExecOrder, char orderSource,char condition)
        {
            var execOrder = new ExecOrder();
            orderID = 0;

            try
            {
                if (AppConfig.CheckGWConnection && !MessageHandler.IsConnected())
                {
                    LogHandler.Log("GW is not connected", "PutOrder", TraceEventType.Error);

                    orderID = 0;
                    outExecOrder = null;
                    return CommonEnums.RET_CODE.ERROR_GW_NOT_CONNECTED;
                }

                execOrder.ExecutedVol = 0;
                execOrder.NumOfMatch = 0;
                execOrder.CancelledVolume = 0;

                execOrder.Market = market.ToString();
                execOrder.MarketStatus = ((char) orderSession).ToString();
                execOrder.MessageType = ETradeGWCommonConstants.DATA_NEW_ORDER;
                execOrder.SubCustAccountId = accountId;
                execOrder.SecSymbol = secSymbol;
                execOrder.Side = side.ToString();
                execOrder.Volume = volume;
                execOrder.Price = price;
                execOrder.ConPrice = conPrice.ToString();
                execOrder.TradeTime = DateTime.Now;
                execOrder.ExecTransType = (int) TRANS_TYPE.TRANS_NEW;
                execOrder.OrderStatus = (int) CommonEnums.ORDER_STATUS.NEW_ORDER;
                execOrder.OrdRejReason = (int) CommonEnums.REJECT_REASON.NOTHING;
                execOrder.IsNewOrder = true;
                execOrder.ConditionOrderId = conditionOrderId;

                execOrder.AvgPrice = avgPrice;
                execOrder.Condition = condition.ToString();


                execOrder = _execOrderService.Save(execOrder); // first, save to generate the OrderId.

                //string refOrderId = EtradeGWCommonUtils.GetRefOrderID(execOrder.OrderId, AppConfig.ServiceName);
                string refOrderId = EtradeGWCommonUtils.GetRefOrderID(execOrder.OrderId, orderSource);
                //char orderSource = (char) EtradeGWCommonUtils.GetOrderSource(refOrderId);

                execOrder.OrderSource = orderSource.ToString();
                execOrder.RefOrderId = refOrderId;
                _execOrderService.Update(execOrder);

                //use for test without LinkOPS connection.
                if (!AppConfig.CheckGWConnection)
                {
                    orderID = execOrder.OrderId;
                    outExecOrder = execOrder;
                    return CommonEnums.RET_CODE.SUCCESS;
                }
                float stopPrice = 0;

                bool ret = MessageHandler.NewOrder(refOrderId, AppConfig.LinkOPSTraderID, secSymbol, side, (float)price,
                                                    conPrice, volume, accountId, stopPrice, condition);
                if (!ret)
                {
                    _execOrderService.Delete(execOrder.OrderId);

                    LogHandler.Log("GW can not send new order:" + orderID, "PutOrder", TraceEventType.Error);

                    orderID = 0;
                    outExecOrder = null;
                    return CommonEnums.RET_CODE.ERROR_GW_NOT_SEND;
                }

                LogHandler.Log("GW has sent the new order:" + orderID, "PutOrder", TraceEventType.Information);
                LogExecOrder(execOrder, GetType() + ".PutOrder()", TraceEventType.Information);

                orderID = execOrder.OrderId;
                outExecOrder = execOrder;
                return CommonEnums.RET_CODE.SUCCESS;
            }

            catch(Exception ex)
            {
                LogHandler.Log("put order error:" + execOrder.SubCustAccountId + " " + execOrder.Side + " " +
                        execOrder.SecSymbol + " " + execOrder.Price, "PutOrder", TraceEventType.Error);
                LogExecOrder(execOrder, GetType() + ".PutOrder()", TraceEventType.Error);

                orderID = 0;
                ExceptionHandler.HandleException(ex, Constants.EXCEPTION_POLICY);
                outExecOrder = null;
                return CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Cancel order.
        /// </summary>
        /// <param name="orderID">The order id.</param>
        /// <returns>
        /// <para>Result of cancelling order.</para>
        /// <para>RET_CODE=ERROR_GW_NOT_CONNECTED: The LinkOPS is not connected.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public CommonEnums.RET_CODE CancelOrder(int orderID)
        {
            ExecOrder orderInfo = null;

            try
            {
                if (AppConfig.CheckGWConnection && !MessageHandler.IsConnected())
                {
                    LogHandler.Log("GW is not connected", "CancelOrder", TraceEventType.Error);

                    return CommonEnums.RET_CODE.ERROR_GW_NOT_CONNECTED;
                }

                orderInfo = _execOrderService.GetByOrderId(orderID);
                if(orderInfo == null)
                {
                    return CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                //This is a new order that has not confirmed from FIS/SET yet
                if (orderInfo.OrderStatus == (short)CommonEnums.ORDER_STATUS.NEW_ORDER || !AppConfig.CheckGWConnection)
                {
                    orderInfo.MessageType = ETradeGWCommonConstants.DATA_EXEC_REPORT;
                    orderInfo.ExecutedVol = 0;
                    orderInfo.ExecutedPrice = 0;
                    orderInfo.CancelledVolume = orderInfo.Volume;
                    orderInfo.CancelledTime = DateTime.Now;
                    orderInfo.OrderStatus = (short)CommonEnums.ORDER_STATUS.CANCELLED;
                    orderInfo.IsNewOrder = true;

                    _execOrderService.Update(orderInfo);

                    LogHandler.Log("Cancelled the order:" + orderID, "CancelOrder", TraceEventType.Information);
                    LogExecOrder(orderInfo, GetType() + ".CancelOrder()", TraceEventType.Information);

                    return CommonEnums.RET_CODE.SUCCESS;
                }

                bool ret = MessageHandler.CancelOrder(orderInfo.RefOrderId, AppConfig.LinkOPSTraderID,
                                                       (int) orderInfo.FisOrderId);
                if (!ret)
                {
                    LogHandler.Log("GW can not send a cancel order:" + orderID, "CancelOrder", TraceEventType.Error);

                    return CommonEnums.RET_CODE.ERROR_GW_NOT_SEND;
                }
                orderInfo.MessageType = ETradeGWCommonConstants.DATA_CANCEL_ORDER;
                orderInfo.ExecTransType = (int) TRANS_TYPE.TRANS_CANCEL;
                orderInfo.OrderStatus = (short) CommonEnums.ORDER_STATUS.NEW_CANCEL;

                _execOrderService.Update(orderInfo);

                LogHandler.Log("cancel order:" + orderID, "CancelOrder", TraceEventType.Information);
                LogExecOrder(orderInfo, GetType() + ".CancelOrder()", TraceEventType.Information);

                return CommonEnums.RET_CODE.SUCCESS;
            }

            catch(Exception ex)
            {
                LogHandler.Log("cancel order error:" + orderID, "CancelOrder", TraceEventType.Error);
                LogExecOrder(orderInfo, GetType() + ".CancelOrder()", TraceEventType.Error);

                ExceptionHandler.HandleException(ex, Constants.EXCEPTION_POLICY);

                return CommonEnums.RET_CODE.SYSTEM_ERROR;

            }
        }

        /// <summary>
        /// Update the order.
        /// </summary>
        /// <param name="execOrder">Exec order information.</param>
        /// <param name="newPrice">New price.</param>
        /// <returns></returns>
        public CommonEnums.RET_CODE UpdateOrder(ExecOrder execOrder, decimal newPrice)
        {
            try
            {
                if (AppConfig.CheckGWConnection && !MessageHandler.IsConnected())
                {
                    LogHandler.Log("GW is not connected", "PutOrder", TraceEventType.Error);

                    return CommonEnums.RET_CODE.ERROR_GW_NOT_CONNECTED;
                }


                execOrder.MessageType = ETradeGWCommonConstants.DATA_CHANGE_ORDER;
                execOrder.ChangedOrderStatus = (short)CommonEnums.CHANGED_ORDER_STATUS.PROCESSING;

                //use for test without LinkOPS connection.
                if (!AppConfig.CheckGWConnection)
                {
                    execOrder.Price = newPrice;
                    execOrder.NewPrice = newPrice;
                    execOrder.ChangedOrderStatus = (short)CommonEnums.CHANGED_ORDER_STATUS.ACCEPTED;
                    bool result = _execOrderService.Update(execOrder);
                    if (!result)
                    {
                        return CommonEnums.RET_CODE.FAIL;
                    }
                    return CommonEnums.RET_CODE.SUCCESS;
                }
                if (execOrder.FisOrderId != null)
                {
                    //Send message to LinkOPS
                    bool ret = MessageHandler.ChangeOrder(execOrder.RefOrderId, AppConfig.LinkOPSTraderID,
                                                          execOrder.FisOrderId.Value, execOrder.SubCustAccountId, ' ',
                                                          (float)execOrder.Price, (float)newPrice);


                    if (!ret)
                    {
                        LogHandler.Log("GW can not send new order:" + execOrder.OrderId, "PutOrder", TraceEventType.Error);

                        return CommonEnums.RET_CODE.ERROR_GW_NOT_SEND;
                    }
                    //Update database
                    execOrder.NewPrice = newPrice;
                    bool result = _execOrderService.Update(execOrder);
                    if (!result)
                    {
                        return CommonEnums.RET_CODE.FAIL;
                    }
                    LogHandler.Log("GW has sent the new order:" + execOrder.OrderId, "PutOrder", TraceEventType.Information);
                    LogExecOrder(execOrder, GetType() + ".UpdateOrder()", TraceEventType.Information);

                    return CommonEnums.RET_CODE.SUCCESS;
                }
                return CommonEnums.RET_CODE.NOT_ALLOW;
            }

            catch (Exception ex)
            {
                LogHandler.Log("Change order error:" + execOrder.OrderId + " " + newPrice, "PutOrder", TraceEventType.Error);

                ExceptionHandler.HandleException(ex, Constants.EXCEPTION_POLICY);
                return CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Return connected status of LinkOPS
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return MessageHandler.IsConnected();
        }

         public bool Connect(string ipAddress, string port)
         {
             return MessageHandler.Connect(ipAddress, port);
         }

         public bool Disconnect()
         {
             return MessageHandler.Disconnect();
         }

        /// <summary>
        /// Recovery lost package
        /// </summary>
        /// <param name="beginSeq">Begin Sequence.</param>
        /// <param name="endSeq">End Sequence.</param>/// 
        /// <returns>true if recovery successfully; otherwise false.</returns>
        public bool Recovery(int beginSeq, int endSeq)
        {
            if (MessageHandler.IsConnected())
            {
                return MessageHandler.Recovery(beginSeq, beginSeq, endSeq);
            }
            LogHandler.Log("GW is not connected", "Recovery",
                           TraceEventType.Information);
            return false;
        }

        private void LogExecOrder (ExecOrder orderInfo, string methodName, TraceEventType erroType)
        {
            try
            {
                if (orderInfo == null)
                {
                    return;
                }

                LogHandler.Log("Sequence " + orderInfo.Sequence + " " +
                               "FISOrderID " + orderInfo.FisOrderId + " " +
                               "SourceID " + orderInfo.SourceId + " " +
                               "TradeTime " + orderInfo.TradeTime + " " +
                               "Symbol " + orderInfo.SecSymbol + " " +
                               "Side " + orderInfo.Side + " " +
                               "Price " + orderInfo.Price + " " +
                               "ConPrice " + orderInfo.ConPrice + " " +
                               "Volume " + orderInfo.Volume + " " +
                               "Account " + orderInfo.SubCustAccountId + " " +
                               "Status " + orderInfo.OrderStatus + " " +
                               "OrdRejReason " + orderInfo.OrdRejReason + " " +
                               "ExecTransType " + orderInfo.ExecTransType + " " +
                               "RefOrderId " + orderInfo.RefOrderId,
                               methodName,
                               erroType);
            }
            catch(Exception)
            {
                return;
            }
        }
    }
}
