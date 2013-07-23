#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using RTStockData.Entities;
using RTStockData.Data;

#endregion

namespace RTStockData.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="HastcStocksProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class HastcStocksProviderBaseCore : EntityProviderBase<RTStockData.Entities.HastcStocks, RTStockData.Entities.HastcStocksKey>
	{		
		#region Get from Many To Many Relationship Functions
		#endregion	
		
		#region Delete Methods

		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to delete.</param>
		/// <returns>Returns true if operation suceeded.</returns>
		public override bool Delete(TransactionManager transactionManager, RTStockData.Entities.HastcStocksKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int64 _id)
		{
			return Delete(null, _id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int64 _id);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
		#endregion

		#region Get By Index Functions
		
		/// <summary>
		/// 	Gets a row from the DataSource based on its primary key.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to retrieve.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <returns>Returns an instance of the Entity class.</returns>
		public override RTStockData.Entities.HastcStocks Get(TransactionManager transactionManager, RTStockData.Entities.HastcStocksKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_hastc_stocks index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.HastcStocks"/> class.</returns>
		public RTStockData.Entities.HastcStocks GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_hastc_stocks index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.HastcStocks"/> class.</returns>
		public RTStockData.Entities.HastcStocks GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_hastc_stocks index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.HastcStocks"/> class.</returns>
		public RTStockData.Entities.HastcStocks GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_hastc_stocks index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.HastcStocks"/> class.</returns>
		public RTStockData.Entities.HastcStocks GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_hastc_stocks index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.HastcStocks"/> class.</returns>
		public RTStockData.Entities.HastcStocks GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_hastc_stocks index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.HastcStocks"/> class.</returns>
		public abstract RTStockData.Entities.HastcStocks GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;HastcStocks&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;HastcStocks&gt;"/></returns>
		public static TList<HastcStocks> Fill(IDataReader reader, TList<HastcStocks> rows, int start, int pageLength)
		{
			NetTiersProvider currentProvider = DataRepository.Provider;
            bool useEntityFactory = currentProvider.UseEntityFactory;
            bool enableEntityTracking = currentProvider.EnableEntityTracking;
            LoadPolicy currentLoadPolicy = currentProvider.CurrentLoadPolicy;
			Type entityCreationFactoryType = currentProvider.EntityCreationalFactoryType;
			
			// advance to the starting row
			for (int i = 0; i < start; i++)
			{
				if (!reader.Read())
				return rows; // not enough rows, just return
			}
			for (int i = 0; i < pageLength; i++)
			{
				if (!reader.Read())
					break; // we are done
					
				string key = null;
				
				RTStockData.Entities.HastcStocks c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("HastcStocks")
					.Append("|").Append((System.Int64)reader[((int)HastcStocksColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<HastcStocks>(
					key.ToString(), // EntityTrackingKey
					"HastcStocks",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new RTStockData.Entities.HastcStocks();
				}
				
				if (!enableEntityTracking ||
					c.EntityState == EntityState.Added ||
					(enableEntityTracking &&
					
						(
							(currentLoadPolicy == LoadPolicy.PreserveChanges && c.EntityState == EntityState.Unchanged) ||
							(currentLoadPolicy == LoadPolicy.DiscardChanges && c.EntityState != EntityState.Unchanged)
						)
					))
				{
					c.SuppressEntityEvents = true;
					c.Id = (System.Int64)reader[((int)HastcStocksColumn.Id - 1)];
					c.TradeDate = (reader.IsDBNull(((int)HastcStocksColumn.TradeDate - 1)))?null:(System.DateTime?)reader[((int)HastcStocksColumn.TradeDate - 1)];
					c.Stockno = (reader.IsDBNull(((int)HastcStocksColumn.Stockno - 1)))?null:(System.Int16?)reader[((int)HastcStocksColumn.Stockno - 1)];
					c.StockSymbol = (reader.IsDBNull(((int)HastcStocksColumn.StockSymbol - 1)))?null:(System.String)reader[((int)HastcStocksColumn.StockSymbol - 1)];
					c.StockType = (reader.IsDBNull(((int)HastcStocksColumn.StockType - 1)))?null:(System.String)reader[((int)HastcStocksColumn.StockType - 1)];
					c.Ceiling = (reader.IsDBNull(((int)HastcStocksColumn.Ceiling - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Ceiling - 1)];
					c.Floor = (reader.IsDBNull(((int)HastcStocksColumn.Floor - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Floor - 1)];
					c.SecurityName = (reader.IsDBNull(((int)HastcStocksColumn.SecurityName - 1)))?null:(System.String)reader[((int)HastcStocksColumn.SecurityName - 1)];
					c.PriorClosePrice = (reader.IsDBNull(((int)HastcStocksColumn.PriorClosePrice - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.PriorClosePrice - 1)];
					c.Last = (reader.IsDBNull(((int)HastcStocksColumn.Last - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Last - 1)];
					c.LastVol = (reader.IsDBNull(((int)HastcStocksColumn.LastVol - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.LastVol - 1)];
					c.LastVal = (reader.IsDBNull(((int)HastcStocksColumn.LastVal - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.LastVal - 1)];
					c.Highest = (reader.IsDBNull(((int)HastcStocksColumn.Highest - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Highest - 1)];
					c.Average = (reader.IsDBNull(((int)HastcStocksColumn.Average - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Average - 1)];
					c.Lowest = (reader.IsDBNull(((int)HastcStocksColumn.Lowest - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Lowest - 1)];
					c.Totalshares = (reader.IsDBNull(((int)HastcStocksColumn.Totalshares - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Totalshares - 1)];
					c.TotalValue = (reader.IsDBNull(((int)HastcStocksColumn.TotalValue - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.TotalValue - 1)];
					c.Best1Bid = (reader.IsDBNull(((int)HastcStocksColumn.Best1Bid - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best1Bid - 1)];
					c.Best1BidVolume = (reader.IsDBNull(((int)HastcStocksColumn.Best1BidVolume - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best1BidVolume - 1)];
					c.Best2Bid = (reader.IsDBNull(((int)HastcStocksColumn.Best2Bid - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best2Bid - 1)];
					c.Best2BidVolume = (reader.IsDBNull(((int)HastcStocksColumn.Best2BidVolume - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best2BidVolume - 1)];
					c.Best3Bid = (reader.IsDBNull(((int)HastcStocksColumn.Best3Bid - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best3Bid - 1)];
					c.Best3BidVolume = (reader.IsDBNull(((int)HastcStocksColumn.Best3BidVolume - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best3BidVolume - 1)];
					c.Best1Offer = (reader.IsDBNull(((int)HastcStocksColumn.Best1Offer - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best1Offer - 1)];
					c.Best1OfferVolume = (reader.IsDBNull(((int)HastcStocksColumn.Best1OfferVolume - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best1OfferVolume - 1)];
					c.Best2Offer = (reader.IsDBNull(((int)HastcStocksColumn.Best2Offer - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best2Offer - 1)];
					c.Best2OfferVolume = (reader.IsDBNull(((int)HastcStocksColumn.Best2OfferVolume - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best2OfferVolume - 1)];
					c.Best3Offer = (reader.IsDBNull(((int)HastcStocksColumn.Best3Offer - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best3Offer - 1)];
					c.Best3OfferVolume = (reader.IsDBNull(((int)HastcStocksColumn.Best3OfferVolume - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best3OfferVolume - 1)];
					c.NmTotalTradedQtty = (reader.IsDBNull(((int)HastcStocksColumn.NmTotalTradedQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.NmTotalTradedQtty - 1)];
					c.PrevPriorPrice = (reader.IsDBNull(((int)HastcStocksColumn.PrevPriorPrice - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.PrevPriorPrice - 1)];
					c.SellCount = (reader.IsDBNull(((int)HastcStocksColumn.SellCount - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.SellCount - 1)];
					c.BuyCount = (reader.IsDBNull(((int)HastcStocksColumn.BuyCount - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.BuyCount - 1)];
					c.NmTotalTradedValue = (reader.IsDBNull(((int)HastcStocksColumn.NmTotalTradedValue - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.NmTotalTradedValue - 1)];
					c.TotalBidQtty = (reader.IsDBNull(((int)HastcStocksColumn.TotalBidQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.TotalBidQtty - 1)];
					c.TotalSellTradingQtty = (reader.IsDBNull(((int)HastcStocksColumn.TotalSellTradingQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.TotalSellTradingQtty - 1)];
					c.TotalOfferQtty = (reader.IsDBNull(((int)HastcStocksColumn.TotalOfferQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.TotalOfferQtty - 1)];
					c.TotalBuyTradingQtty = (reader.IsDBNull(((int)HastcStocksColumn.TotalBuyTradingQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.TotalBuyTradingQtty - 1)];
					c.BidCount = (reader.IsDBNull(((int)HastcStocksColumn.BidCount - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.BidCount - 1)];
					c.OfferCount = (reader.IsDBNull(((int)HastcStocksColumn.OfferCount - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.OfferCount - 1)];
					c.BuyForeignQtty = (reader.IsDBNull(((int)HastcStocksColumn.BuyForeignQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.BuyForeignQtty - 1)];
					c.BuyForeignValue = (reader.IsDBNull(((int)HastcStocksColumn.BuyForeignValue - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.BuyForeignValue - 1)];
					c.SellForeignQtty = (reader.IsDBNull(((int)HastcStocksColumn.SellForeignQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.SellForeignQtty - 1)];
					c.SellForeignValue = (reader.IsDBNull(((int)HastcStocksColumn.SellForeignValue - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.SellForeignValue - 1)];
					c.RemainForeignQtty = (reader.IsDBNull(((int)HastcStocksColumn.RemainForeignQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.RemainForeignQtty - 1)];
					c.PtMatchPrice = (reader.IsDBNull(((int)HastcStocksColumn.PtMatchPrice - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.PtMatchPrice - 1)];
					c.PtMatchQtty = (reader.IsDBNull(((int)HastcStocksColumn.PtMatchQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.PtMatchQtty - 1)];
					c.PtTotalTradedQtty = (reader.IsDBNull(((int)HastcStocksColumn.PtTotalTradedQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.PtTotalTradedQtty - 1)];
					c.PtTotalTradedValue = (reader.IsDBNull(((int)HastcStocksColumn.PtTotalTradedValue - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.PtTotalTradedValue - 1)];
					c.TotalListingQtty = (reader.IsDBNull(((int)HastcStocksColumn.TotalListingQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.TotalListingQtty - 1)];
					c.OpenPrice = (reader.IsDBNull(((int)HastcStocksColumn.OpenPrice - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.OpenPrice - 1)];
					c.ClosePrice = (reader.IsDBNull(((int)HastcStocksColumn.ClosePrice - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.ClosePrice - 1)];
					c.AveragePrice = (reader.IsDBNull(((int)HastcStocksColumn.AveragePrice - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.AveragePrice - 1)];
					c.Status = (reader.IsDBNull(((int)HastcStocksColumn.Status - 1)))?null:(System.String)reader[((int)HastcStocksColumn.Status - 1)];
					c.Sequence = (reader.IsDBNull(((int)HastcStocksColumn.Sequence - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Sequence - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.HastcStocks"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.HastcStocks"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, RTStockData.Entities.HastcStocks entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)HastcStocksColumn.Id - 1)];
			entity.TradeDate = (reader.IsDBNull(((int)HastcStocksColumn.TradeDate - 1)))?null:(System.DateTime?)reader[((int)HastcStocksColumn.TradeDate - 1)];
			entity.Stockno = (reader.IsDBNull(((int)HastcStocksColumn.Stockno - 1)))?null:(System.Int16?)reader[((int)HastcStocksColumn.Stockno - 1)];
			entity.StockSymbol = (reader.IsDBNull(((int)HastcStocksColumn.StockSymbol - 1)))?null:(System.String)reader[((int)HastcStocksColumn.StockSymbol - 1)];
			entity.StockType = (reader.IsDBNull(((int)HastcStocksColumn.StockType - 1)))?null:(System.String)reader[((int)HastcStocksColumn.StockType - 1)];
			entity.Ceiling = (reader.IsDBNull(((int)HastcStocksColumn.Ceiling - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Ceiling - 1)];
			entity.Floor = (reader.IsDBNull(((int)HastcStocksColumn.Floor - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Floor - 1)];
			entity.SecurityName = (reader.IsDBNull(((int)HastcStocksColumn.SecurityName - 1)))?null:(System.String)reader[((int)HastcStocksColumn.SecurityName - 1)];
			entity.PriorClosePrice = (reader.IsDBNull(((int)HastcStocksColumn.PriorClosePrice - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.PriorClosePrice - 1)];
			entity.Last = (reader.IsDBNull(((int)HastcStocksColumn.Last - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Last - 1)];
			entity.LastVol = (reader.IsDBNull(((int)HastcStocksColumn.LastVol - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.LastVol - 1)];
			entity.LastVal = (reader.IsDBNull(((int)HastcStocksColumn.LastVal - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.LastVal - 1)];
			entity.Highest = (reader.IsDBNull(((int)HastcStocksColumn.Highest - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Highest - 1)];
			entity.Average = (reader.IsDBNull(((int)HastcStocksColumn.Average - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Average - 1)];
			entity.Lowest = (reader.IsDBNull(((int)HastcStocksColumn.Lowest - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Lowest - 1)];
			entity.Totalshares = (reader.IsDBNull(((int)HastcStocksColumn.Totalshares - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Totalshares - 1)];
			entity.TotalValue = (reader.IsDBNull(((int)HastcStocksColumn.TotalValue - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.TotalValue - 1)];
			entity.Best1Bid = (reader.IsDBNull(((int)HastcStocksColumn.Best1Bid - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best1Bid - 1)];
			entity.Best1BidVolume = (reader.IsDBNull(((int)HastcStocksColumn.Best1BidVolume - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best1BidVolume - 1)];
			entity.Best2Bid = (reader.IsDBNull(((int)HastcStocksColumn.Best2Bid - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best2Bid - 1)];
			entity.Best2BidVolume = (reader.IsDBNull(((int)HastcStocksColumn.Best2BidVolume - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best2BidVolume - 1)];
			entity.Best3Bid = (reader.IsDBNull(((int)HastcStocksColumn.Best3Bid - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best3Bid - 1)];
			entity.Best3BidVolume = (reader.IsDBNull(((int)HastcStocksColumn.Best3BidVolume - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best3BidVolume - 1)];
			entity.Best1Offer = (reader.IsDBNull(((int)HastcStocksColumn.Best1Offer - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best1Offer - 1)];
			entity.Best1OfferVolume = (reader.IsDBNull(((int)HastcStocksColumn.Best1OfferVolume - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best1OfferVolume - 1)];
			entity.Best2Offer = (reader.IsDBNull(((int)HastcStocksColumn.Best2Offer - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best2Offer - 1)];
			entity.Best2OfferVolume = (reader.IsDBNull(((int)HastcStocksColumn.Best2OfferVolume - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best2OfferVolume - 1)];
			entity.Best3Offer = (reader.IsDBNull(((int)HastcStocksColumn.Best3Offer - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best3Offer - 1)];
			entity.Best3OfferVolume = (reader.IsDBNull(((int)HastcStocksColumn.Best3OfferVolume - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Best3OfferVolume - 1)];
			entity.NmTotalTradedQtty = (reader.IsDBNull(((int)HastcStocksColumn.NmTotalTradedQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.NmTotalTradedQtty - 1)];
			entity.PrevPriorPrice = (reader.IsDBNull(((int)HastcStocksColumn.PrevPriorPrice - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.PrevPriorPrice - 1)];
			entity.SellCount = (reader.IsDBNull(((int)HastcStocksColumn.SellCount - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.SellCount - 1)];
			entity.BuyCount = (reader.IsDBNull(((int)HastcStocksColumn.BuyCount - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.BuyCount - 1)];
			entity.NmTotalTradedValue = (reader.IsDBNull(((int)HastcStocksColumn.NmTotalTradedValue - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.NmTotalTradedValue - 1)];
			entity.TotalBidQtty = (reader.IsDBNull(((int)HastcStocksColumn.TotalBidQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.TotalBidQtty - 1)];
			entity.TotalSellTradingQtty = (reader.IsDBNull(((int)HastcStocksColumn.TotalSellTradingQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.TotalSellTradingQtty - 1)];
			entity.TotalOfferQtty = (reader.IsDBNull(((int)HastcStocksColumn.TotalOfferQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.TotalOfferQtty - 1)];
			entity.TotalBuyTradingQtty = (reader.IsDBNull(((int)HastcStocksColumn.TotalBuyTradingQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.TotalBuyTradingQtty - 1)];
			entity.BidCount = (reader.IsDBNull(((int)HastcStocksColumn.BidCount - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.BidCount - 1)];
			entity.OfferCount = (reader.IsDBNull(((int)HastcStocksColumn.OfferCount - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.OfferCount - 1)];
			entity.BuyForeignQtty = (reader.IsDBNull(((int)HastcStocksColumn.BuyForeignQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.BuyForeignQtty - 1)];
			entity.BuyForeignValue = (reader.IsDBNull(((int)HastcStocksColumn.BuyForeignValue - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.BuyForeignValue - 1)];
			entity.SellForeignQtty = (reader.IsDBNull(((int)HastcStocksColumn.SellForeignQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.SellForeignQtty - 1)];
			entity.SellForeignValue = (reader.IsDBNull(((int)HastcStocksColumn.SellForeignValue - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.SellForeignValue - 1)];
			entity.RemainForeignQtty = (reader.IsDBNull(((int)HastcStocksColumn.RemainForeignQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.RemainForeignQtty - 1)];
			entity.PtMatchPrice = (reader.IsDBNull(((int)HastcStocksColumn.PtMatchPrice - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.PtMatchPrice - 1)];
			entity.PtMatchQtty = (reader.IsDBNull(((int)HastcStocksColumn.PtMatchQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.PtMatchQtty - 1)];
			entity.PtTotalTradedQtty = (reader.IsDBNull(((int)HastcStocksColumn.PtTotalTradedQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.PtTotalTradedQtty - 1)];
			entity.PtTotalTradedValue = (reader.IsDBNull(((int)HastcStocksColumn.PtTotalTradedValue - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.PtTotalTradedValue - 1)];
			entity.TotalListingQtty = (reader.IsDBNull(((int)HastcStocksColumn.TotalListingQtty - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.TotalListingQtty - 1)];
			entity.OpenPrice = (reader.IsDBNull(((int)HastcStocksColumn.OpenPrice - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.OpenPrice - 1)];
			entity.ClosePrice = (reader.IsDBNull(((int)HastcStocksColumn.ClosePrice - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.ClosePrice - 1)];
			entity.AveragePrice = (reader.IsDBNull(((int)HastcStocksColumn.AveragePrice - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.AveragePrice - 1)];
			entity.Status = (reader.IsDBNull(((int)HastcStocksColumn.Status - 1)))?null:(System.String)reader[((int)HastcStocksColumn.Status - 1)];
			entity.Sequence = (reader.IsDBNull(((int)HastcStocksColumn.Sequence - 1)))?null:(System.Int64?)reader[((int)HastcStocksColumn.Sequence - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.HastcStocks"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.HastcStocks"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, RTStockData.Entities.HastcStocks entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["ID"];
			entity.TradeDate = Convert.IsDBNull(dataRow["TradeDate"]) ? null : (System.DateTime?)dataRow["TradeDate"];
			entity.Stockno = Convert.IsDBNull(dataRow["Stockno"]) ? null : (System.Int16?)dataRow["Stockno"];
			entity.StockSymbol = Convert.IsDBNull(dataRow["StockSymbol"]) ? null : (System.String)dataRow["StockSymbol"];
			entity.StockType = Convert.IsDBNull(dataRow["StockType"]) ? null : (System.String)dataRow["StockType"];
			entity.Ceiling = Convert.IsDBNull(dataRow["Ceiling"]) ? null : (System.Int64?)dataRow["Ceiling"];
			entity.Floor = Convert.IsDBNull(dataRow["Floor"]) ? null : (System.Int64?)dataRow["Floor"];
			entity.SecurityName = Convert.IsDBNull(dataRow["SecurityName"]) ? null : (System.String)dataRow["SecurityName"];
			entity.PriorClosePrice = Convert.IsDBNull(dataRow["PriorClosePrice"]) ? null : (System.Int64?)dataRow["PriorClosePrice"];
			entity.Last = Convert.IsDBNull(dataRow["Last"]) ? null : (System.Int64?)dataRow["Last"];
			entity.LastVol = Convert.IsDBNull(dataRow["LastVol"]) ? null : (System.Int64?)dataRow["LastVol"];
			entity.LastVal = Convert.IsDBNull(dataRow["LastVal"]) ? null : (System.Int64?)dataRow["LastVal"];
			entity.Highest = Convert.IsDBNull(dataRow["Highest"]) ? null : (System.Int64?)dataRow["Highest"];
			entity.Average = Convert.IsDBNull(dataRow["Average"]) ? null : (System.Int64?)dataRow["Average"];
			entity.Lowest = Convert.IsDBNull(dataRow["Lowest"]) ? null : (System.Int64?)dataRow["Lowest"];
			entity.Totalshares = Convert.IsDBNull(dataRow["Totalshares"]) ? null : (System.Int64?)dataRow["Totalshares"];
			entity.TotalValue = Convert.IsDBNull(dataRow["TotalValue"]) ? null : (System.Int64?)dataRow["TotalValue"];
			entity.Best1Bid = Convert.IsDBNull(dataRow["Best1Bid"]) ? null : (System.Int64?)dataRow["Best1Bid"];
			entity.Best1BidVolume = Convert.IsDBNull(dataRow["Best1BidVolume"]) ? null : (System.Int64?)dataRow["Best1BidVolume"];
			entity.Best2Bid = Convert.IsDBNull(dataRow["Best2Bid"]) ? null : (System.Int64?)dataRow["Best2Bid"];
			entity.Best2BidVolume = Convert.IsDBNull(dataRow["Best2BidVolume"]) ? null : (System.Int64?)dataRow["Best2BidVolume"];
			entity.Best3Bid = Convert.IsDBNull(dataRow["Best3Bid"]) ? null : (System.Int64?)dataRow["Best3Bid"];
			entity.Best3BidVolume = Convert.IsDBNull(dataRow["Best3BidVolume"]) ? null : (System.Int64?)dataRow["Best3BidVolume"];
			entity.Best1Offer = Convert.IsDBNull(dataRow["Best1Offer"]) ? null : (System.Int64?)dataRow["Best1Offer"];
			entity.Best1OfferVolume = Convert.IsDBNull(dataRow["Best1OfferVolume"]) ? null : (System.Int64?)dataRow["Best1OfferVolume"];
			entity.Best2Offer = Convert.IsDBNull(dataRow["Best2Offer"]) ? null : (System.Int64?)dataRow["Best2Offer"];
			entity.Best2OfferVolume = Convert.IsDBNull(dataRow["Best2OfferVolume"]) ? null : (System.Int64?)dataRow["Best2OfferVolume"];
			entity.Best3Offer = Convert.IsDBNull(dataRow["Best3Offer"]) ? null : (System.Int64?)dataRow["Best3Offer"];
			entity.Best3OfferVolume = Convert.IsDBNull(dataRow["Best3OfferVolume"]) ? null : (System.Int64?)dataRow["Best3OfferVolume"];
			entity.NmTotalTradedQtty = Convert.IsDBNull(dataRow["NM_TOTAL_TRADED_QTTY"]) ? null : (System.Int64?)dataRow["NM_TOTAL_TRADED_QTTY"];
			entity.PrevPriorPrice = Convert.IsDBNull(dataRow["PREV_PRIOR_PRICE"]) ? null : (System.Int64?)dataRow["PREV_PRIOR_PRICE"];
			entity.SellCount = Convert.IsDBNull(dataRow["SELL_COUNT"]) ? null : (System.Int64?)dataRow["SELL_COUNT"];
			entity.BuyCount = Convert.IsDBNull(dataRow["BUY_COUNT"]) ? null : (System.Int64?)dataRow["BUY_COUNT"];
			entity.NmTotalTradedValue = Convert.IsDBNull(dataRow["NM_TOTAL_TRADED_VALUE"]) ? null : (System.Int64?)dataRow["NM_TOTAL_TRADED_VALUE"];
			entity.TotalBidQtty = Convert.IsDBNull(dataRow["TOTAL_BID_QTTY"]) ? null : (System.Int64?)dataRow["TOTAL_BID_QTTY"];
			entity.TotalSellTradingQtty = Convert.IsDBNull(dataRow["TOTAL_SELL_TRADING_QTTY"]) ? null : (System.Int64?)dataRow["TOTAL_SELL_TRADING_QTTY"];
			entity.TotalOfferQtty = Convert.IsDBNull(dataRow["TOTAL_OFFER_QTTY"]) ? null : (System.Int64?)dataRow["TOTAL_OFFER_QTTY"];
			entity.TotalBuyTradingQtty = Convert.IsDBNull(dataRow["TOTAL_BUY_TRADING_QTTY"]) ? null : (System.Int64?)dataRow["TOTAL_BUY_TRADING_QTTY"];
			entity.BidCount = Convert.IsDBNull(dataRow["BID_COUNT"]) ? null : (System.Int64?)dataRow["BID_COUNT"];
			entity.OfferCount = Convert.IsDBNull(dataRow["OFFER_COUNT"]) ? null : (System.Int64?)dataRow["OFFER_COUNT"];
			entity.BuyForeignQtty = Convert.IsDBNull(dataRow["BUY_FOREIGN_QTTY"]) ? null : (System.Int64?)dataRow["BUY_FOREIGN_QTTY"];
			entity.BuyForeignValue = Convert.IsDBNull(dataRow["BUY_FOREIGN_VALUE"]) ? null : (System.Int64?)dataRow["BUY_FOREIGN_VALUE"];
			entity.SellForeignQtty = Convert.IsDBNull(dataRow["SELL_FOREIGN_QTTY"]) ? null : (System.Int64?)dataRow["SELL_FOREIGN_QTTY"];
			entity.SellForeignValue = Convert.IsDBNull(dataRow["SELL_FOREIGN_VALUE"]) ? null : (System.Int64?)dataRow["SELL_FOREIGN_VALUE"];
			entity.RemainForeignQtty = Convert.IsDBNull(dataRow["REMAIN_FOREIGN_QTTY"]) ? null : (System.Int64?)dataRow["REMAIN_FOREIGN_QTTY"];
			entity.PtMatchPrice = Convert.IsDBNull(dataRow["PT_MATCH_PRICE"]) ? null : (System.Int64?)dataRow["PT_MATCH_PRICE"];
			entity.PtMatchQtty = Convert.IsDBNull(dataRow["PT_MATCH_QTTY"]) ? null : (System.Int64?)dataRow["PT_MATCH_QTTY"];
			entity.PtTotalTradedQtty = Convert.IsDBNull(dataRow["PT_TOTAL_TRADED_QTTY"]) ? null : (System.Int64?)dataRow["PT_TOTAL_TRADED_QTTY"];
			entity.PtTotalTradedValue = Convert.IsDBNull(dataRow["PT_TOTAL_TRADED_VALUE"]) ? null : (System.Int64?)dataRow["PT_TOTAL_TRADED_VALUE"];
			entity.TotalListingQtty = Convert.IsDBNull(dataRow["TOTAL_LISTING_QTTY"]) ? null : (System.Int64?)dataRow["TOTAL_LISTING_QTTY"];
			entity.OpenPrice = Convert.IsDBNull(dataRow["OPEN_PRICE"]) ? null : (System.Int64?)dataRow["OPEN_PRICE"];
			entity.ClosePrice = Convert.IsDBNull(dataRow["CLOSE_PRICE"]) ? null : (System.Int64?)dataRow["CLOSE_PRICE"];
			entity.AveragePrice = Convert.IsDBNull(dataRow["AVERAGE_PRICE"]) ? null : (System.Int64?)dataRow["AVERAGE_PRICE"];
			entity.Status = Convert.IsDBNull(dataRow["STATUS"]) ? null : (System.String)dataRow["STATUS"];
			entity.Sequence = Convert.IsDBNull(dataRow["Sequence"]) ? null : (System.Int64?)dataRow["Sequence"];
			entity.AcceptChanges();
		}
		#endregion 
		
		#region DeepLoad Methods
		/// <summary>
		/// Deep Loads the <see cref="IEntity"/> object with criteria based of the child 
		/// property collections only N Levels Deep based on the <see cref="DeepLoadType"/>.
		/// </summary>
		/// <remarks>
		/// Use this method with caution as it is possible to DeepLoad with Recursion and traverse an entire object graph.
		/// </remarks>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.HastcStocks"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">RTStockData.Entities.HastcStocks Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, RTStockData.Entities.HastcStocks entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			
			//Fire all DeepLoad Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			deepHandles = null;
		}
		
		#endregion 
		
		#region DeepSave Methods

		/// <summary>
		/// Deep Save the entire object graph of the RTStockData.Entities.HastcStocks object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">RTStockData.Entities.HastcStocks instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">RTStockData.Entities.HastcStocks Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, RTStockData.Entities.HastcStocks entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			//Fire all DeepSave Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			
			// Save Root Entity through Provider, if not already saved in delete mode
			if (entity.IsDeleted)
				this.Save(transactionManager, entity);
				

			deepHandles = null;
						
			return true;
		}
		#endregion
	} // end class
	
	#region HastcStocksChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>RTStockData.Entities.HastcStocks</c>
	///</summary>
	public enum HastcStocksChildEntityTypes
	{
	}
	
	#endregion HastcStocksChildEntityTypes
	
	#region HastcStocksFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;HastcStocksColumn&gt;"/> class
	/// that is used exclusively with a <see cref="HastcStocks"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HastcStocksFilterBuilder : SqlFilterBuilder<HastcStocksColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HastcStocksFilterBuilder class.
		/// </summary>
		public HastcStocksFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the HastcStocksFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HastcStocksFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HastcStocksFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HastcStocksFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HastcStocksFilterBuilder
	
	#region HastcStocksParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;HastcStocksColumn&gt;"/> class
	/// that is used exclusively with a <see cref="HastcStocks"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HastcStocksParameterBuilder : ParameterizedSqlFilterBuilder<HastcStocksColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HastcStocksParameterBuilder class.
		/// </summary>
		public HastcStocksParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the HastcStocksParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HastcStocksParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HastcStocksParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HastcStocksParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HastcStocksParameterBuilder
	
	#region HastcStocksSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;HastcStocksColumn&gt;"/> class
	/// that is used exclusively with a <see cref="HastcStocks"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class HastcStocksSortBuilder : SqlSortBuilder<HastcStocksColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HastcStocksSqlSortBuilder class.
		/// </summary>
		public HastcStocksSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion HastcStocksSortBuilder
	
} // end namespace
