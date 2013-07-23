using System;
using System.Collections.Generic;
using System.Text;

namespace ETradeGW
{
    public class LogTransactionDTO
    {
        protected System.Int64 _ID;
        protected System.DateTime _TradeTime;
        protected System.String _AccountID;
        protected System.String _Type;
        protected System.String _Side;
        protected System.String _SecSymbol;
        protected System.Double _Price;
        protected System.String _ConPrice;
        protected System.Int64 _Volume;
        protected System.Int64 _ExecutedVol;
        protected System.Double _ExecutedPrice;
        protected System.Int64 _CancelledVolume;
        protected System.String _OrdRejReason;
        protected System.Int16 _SourceID;
        protected System.String _Market;
        protected System.String _RefOrderID;
        protected System.Int64 _FISOrderID;

        public LogTransactionDTO()
        {
            _ID              = -1;
            _TradeTime       = DateTime.Now;
            _AccountID       = " ";
            _Type            = " ";
            _Side            = " ";
            _SecSymbol       = " ";
            _Price           = 0;
            _ConPrice        = " ";
            _Volume          = 0;
            _ExecutedVol     = 0;
            _ExecutedPrice   = 0;
            _CancelledVolume = 0;
            _OrdRejReason    = " ";
            _SourceID        = 0;
            _Market          = "O";
            _RefOrderID      = "";
            _FISOrderID      = 0;
        }

        public System.Int64 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public System.DateTime TradeTime
        {
            get { return _TradeTime; }
            set { _TradeTime = value; }
        }

        public System.String AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        public System.String Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public System.String Side
        {
            get { return _Side; }
            set { _Side = value; }
        }

        public System.String SecSymbol
        {
            get { return _SecSymbol; }
            set { _SecSymbol = value; }
        }

        public System.Double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        public System.String ConPrice
        {
            get { return _ConPrice; }
            set { _ConPrice = value; }
        }

        public System.Int64 Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }

        public System.Int64 ExecutedVol
        {
            get { return _ExecutedVol; }
            set { _ExecutedVol = value; }
        }

        public System.Double ExecutedPrice
        {
            get { return _ExecutedPrice; }
            set { _ExecutedPrice = value; }
        }

        public System.Int64 CancelledVolume
        {
            get { return _CancelledVolume; }
            set { _CancelledVolume = value; }
        }

        public System.String OrdRejReason
        {
            get { return _OrdRejReason; }
            set { _OrdRejReason = value; }
        }

        public System.Int16 SourceID
        {
            get { return _SourceID; }
            set { _SourceID = value; }
        }

        public System.String Market
        {
            get { return _Market; }
            set { _Market = value; }
        }

        public System.String RefOrderID
        {
            get { return _RefOrderID; }
            set { _RefOrderID = value; }
        }

        public System.Int64 FISOrderID
        {
            get { return _FISOrderID; }
            set { _FISOrderID = value; }
        }


    } // end DTO class
}
