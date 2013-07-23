using System;
using System.Threading;
using ETradeCommon.Enums;

namespace RTWebService.Updater
{
    public static class RealTimeStockUpdater
    {
        private static bool _isUpdaterRunning;
        private static Thread _updaterThread;

        private static bool _enableUpdatingHose = true;
        private static bool _enableUpdatingHnx = true;
        private static bool _enableUpdatingUpcom = true;

        //private static bool _enableUpdatingIntraD = true;


        private static bool _enableIntraDhnx = true;
        private static bool _enableIntraDhose = true;
        private static bool _enableIntraUpcom = true;


        private static readonly Boolean EnableIntraD = Boolean.Parse(Utils.Common.ReadFromWebConfig("EnableIntraD"));

        public static bool EnableUpdatingHose
        {
            get { return _enableUpdatingHose; }
            set { _enableUpdatingHose = value; }
        }

        public static bool EnableUpdatingHnx
        {
            get { return _enableUpdatingHnx; }
            set { _enableUpdatingHnx = value; }
        }

        public static bool EnableUpdatingUpcom
        {
            get { return _enableUpdatingUpcom; }
            set { _enableUpdatingUpcom = value; }
        }

        /*public static bool EnableUpdatingIntraD
        {
            get
            {
                return _enableUpdatingIntraD;
            }
            set
            {
                _enableUpdatingIntraD = value;
            }
        }*/


        public static bool EnableIntraDhnx
        {
            get { return _enableIntraDhnx; }
            set { _enableIntraDhnx = value; }
        }

        public static bool EnableIntraDhose
        {
            get { return _enableIntraDhose; }
            set { _enableIntraDhose = value; }
        }

        public static bool EnableIntraDupcom
        {
            get { return _enableIntraUpcom; }
            set { _enableIntraUpcom = value; }
        }

        public static void StartUpdater()
        {
            if (!_isUpdaterRunning)
            {
                _updaterThread = new Thread(UpdaterHandler) {IsBackground = true};
                _isUpdaterRunning = true;
                _updaterThread.Start();
                Utils.Common.Log("Start Updater");
            }
        }

        public static void StopUpdater()
        {
            _isUpdaterRunning = false;
            Utils.Common.Log("Stop Updater");
        }

        public static void StartUpdater(int marketId)
        {
            switch (marketId)
            {
                case (short) CommonEnums.MARKET_ID.HOSE:
                    EnableUpdatingHose = true;

                    break;
                case (short) CommonEnums.MARKET_ID.HNX:
                    EnableUpdatingHnx = true;

                    break;
                case (short) CommonEnums.MARKET_ID.UPCoM:
                    EnableUpdatingUpcom = true;

                    break;
                default:
                    return;
            }
        }

        public static void StopUpdater(int marketId)
        {
            switch (marketId)
            {
                case (short) CommonEnums.MARKET_ID.HOSE:
                    EnableUpdatingHose = false;

                    break;
                case (short) CommonEnums.MARKET_ID.HNX:
                    EnableUpdatingHnx = false;

                    break;
                case (short) CommonEnums.MARKET_ID.UPCoM:
                    EnableUpdatingUpcom = false;

                    break;
                default:
                    return;
            }
        }

        public static bool UpdatingStatus(int marketId)
        {
            switch (marketId)
            {
                case (short) CommonEnums.MARKET_ID.HOSE:
                    return EnableUpdatingHose;
                case (short) CommonEnums.MARKET_ID.HNX:
                    return EnableUpdatingHnx;
                case (short) CommonEnums.MARKET_ID.UPCoM:
                    return EnableUpdatingUpcom;
                default:
                    return false;
            }
        }

        private static void UpdaterHandler()
        {
            while (_isUpdaterRunning)
            {
                try
                {
                    DbServices.ResetAllMarketData();

                    DbServices.UpdateAllMarketInfo(_enableUpdatingHose, _enableUpdatingHnx, _enableUpdatingUpcom);
                    DbServices.UpdateAllStockInfo(_enableUpdatingHose, _enableUpdatingHnx, _enableUpdatingUpcom);
                    DbServices.UpdateIndexVn30();
                    DbServices.UpdateIndexVn30History();
                    DbServices.UpdateBasketInfo();
                    DbServices.UpdateHnxIndex30();
                    DbServices.UpdateHnxIndex30History();
                    DbServices.UpdateIndexs();
                    if (EnableIntraD)
                    {
                        DbServices.UpdateTransactionInfo(_enableIntraDhose, _enableIntraDhnx, _enableIntraUpcom);
                    }
                }
                catch (Exception ex)
                {
                    Utils.Common.Log(ex.ToString());
                }

                Thread.Sleep(100);
            }

            return;
        }

        public static bool IsIntradayRunning(int marketId)
        {
            bool intraday = _isUpdaterRunning && EnableIntraD;
            switch (marketId)
            {
                case (short) CommonEnums.MARKET_ID.HOSE:
                    return (intraday && EnableIntraDhose);
                case (short) CommonEnums.MARKET_ID.HNX:
                    return (intraday && EnableIntraDhnx);
                case (short) CommonEnums.MARKET_ID.UPCoM:
                    return (intraday && EnableIntraDupcom);
                default:
                    return false;
            }
        }

        public static bool RestartRtService()
        {
            try
            {
                StopUpdater();
                DbServices.ResetRtData();
                DbServices.CreateInMemDb();
                StartUpdater();
                return true;
            }
            catch (Exception exception)
            {
                Utils.Common.Log(exception.ToString());
                return false;
            }
        }
    }
}