﻿#region Using directives

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
	/// This class is the base class for any <see cref="SecurityRealtimeProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class SecurityRealtimeProviderBaseCore : EntityProviderBase<RTStockData.Entities.SecurityRealtime, RTStockData.Entities.SecurityRealtimeKey>
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
		public override bool Delete(TransactionManager transactionManager, RTStockData.Entities.SecurityRealtimeKey key)
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
		public override RTStockData.Entities.SecurityRealtime Get(TransactionManager transactionManager, RTStockData.Entities.SecurityRealtimeKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_security_realtime index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.SecurityRealtime"/> class.</returns>
		public RTStockData.Entities.SecurityRealtime GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_security_realtime index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.SecurityRealtime"/> class.</returns>
		public RTStockData.Entities.SecurityRealtime GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_security_realtime index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.SecurityRealtime"/> class.</returns>
		public RTStockData.Entities.SecurityRealtime GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_security_realtime index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.SecurityRealtime"/> class.</returns>
		public RTStockData.Entities.SecurityRealtime GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_security_realtime index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.SecurityRealtime"/> class.</returns>
		public RTStockData.Entities.SecurityRealtime GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_security_realtime index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.SecurityRealtime"/> class.</returns>
		public abstract RTStockData.Entities.SecurityRealtime GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;SecurityRealtime&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;SecurityRealtime&gt;"/></returns>
		public static TList<SecurityRealtime> Fill(IDataReader reader, TList<SecurityRealtime> rows, int start, int pageLength)
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
				
				RTStockData.Entities.SecurityRealtime c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("SecurityRealtime")
					.Append("|").Append((System.Int64)reader[((int)SecurityRealtimeColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<SecurityRealtime>(
					key.ToString(), // EntityTrackingKey
					"SecurityRealtime",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new RTStockData.Entities.SecurityRealtime();
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
					c.Id = (System.Int64)reader[((int)SecurityRealtimeColumn.Id - 1)];
					c.TradeDate = (reader.IsDBNull(((int)SecurityRealtimeColumn.TradeDate - 1)))?null:(System.DateTime?)reader[((int)SecurityRealtimeColumn.TradeDate - 1)];
					c.Stockno = (reader.IsDBNull(((int)SecurityRealtimeColumn.Stockno - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.Stockno - 1)];
					c.StockSymbol = (reader.IsDBNull(((int)SecurityRealtimeColumn.StockSymbol - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.StockSymbol - 1)];
					c.StockType = (reader.IsDBNull(((int)SecurityRealtimeColumn.StockType - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.StockType - 1)];
					c.Ceiling = (reader.IsDBNull(((int)SecurityRealtimeColumn.Ceiling - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Ceiling - 1)];
					c.Floor = (reader.IsDBNull(((int)SecurityRealtimeColumn.Floor - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Floor - 1)];
					c.BigLotValue = (reader.IsDBNull(((int)SecurityRealtimeColumn.BigLotValue - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.BigLotValue - 1)];
					c.SecurityName = (reader.IsDBNull(((int)SecurityRealtimeColumn.SecurityName - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.SecurityName - 1)];
					c.SectorNo = (reader.IsDBNull(((int)SecurityRealtimeColumn.SectorNo - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.SectorNo - 1)];
					c.Designated = (reader.IsDBNull(((int)SecurityRealtimeColumn.Designated - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Designated - 1)];
					c.Suspension = (reader.IsDBNull(((int)SecurityRealtimeColumn.Suspension - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Suspension - 1)];
					c.Delist = (reader.IsDBNull(((int)SecurityRealtimeColumn.Delist - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Delist - 1)];
					c.HaltResumeFlag = (reader.IsDBNull(((int)SecurityRealtimeColumn.HaltResumeFlag - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.HaltResumeFlag - 1)];
					c.Split = (reader.IsDBNull(((int)SecurityRealtimeColumn.Split - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Split - 1)];
					c.Benefit = (reader.IsDBNull(((int)SecurityRealtimeColumn.Benefit - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Benefit - 1)];
					c.Meeting = (reader.IsDBNull(((int)SecurityRealtimeColumn.Meeting - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Meeting - 1)];
					c.Notice = (reader.IsDBNull(((int)SecurityRealtimeColumn.Notice - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Notice - 1)];
					c.ClientidRequired = (reader.IsDBNull(((int)SecurityRealtimeColumn.ClientidRequired - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.ClientidRequired - 1)];
					c.CouponRate = (reader.IsDBNull(((int)SecurityRealtimeColumn.CouponRate - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.CouponRate - 1)];
					c.IssueDate = (reader.IsDBNull(((int)SecurityRealtimeColumn.IssueDate - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.IssueDate - 1)];
					c.MatureDate = (reader.IsDBNull(((int)SecurityRealtimeColumn.MatureDate - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.MatureDate - 1)];
					c.AvrPrice = (reader.IsDBNull(((int)SecurityRealtimeColumn.AvrPrice - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.AvrPrice - 1)];
					c.ParValue = (reader.IsDBNull(((int)SecurityRealtimeColumn.ParValue - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.ParValue - 1)];
					c.SdcFlag = (reader.IsDBNull(((int)SecurityRealtimeColumn.SdcFlag - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.SdcFlag - 1)];
					c.PriorClosePrice = (reader.IsDBNull(((int)SecurityRealtimeColumn.PriorClosePrice - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.PriorClosePrice - 1)];
					c.PriorCloseDate = (reader.IsDBNull(((int)SecurityRealtimeColumn.PriorCloseDate - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.PriorCloseDate - 1)];
					c.ProjectOpen = (reader.IsDBNull(((int)SecurityRealtimeColumn.ProjectOpen - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.ProjectOpen - 1)];
					c.OpenPrice = (reader.IsDBNull(((int)SecurityRealtimeColumn.OpenPrice - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.OpenPrice - 1)];
					c.Last = (reader.IsDBNull(((int)SecurityRealtimeColumn.Last - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Last - 1)];
					c.LastVol = (reader.IsDBNull(((int)SecurityRealtimeColumn.LastVol - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.LastVol - 1)];
					c.LastVal = (reader.IsDBNull(((int)SecurityRealtimeColumn.LastVal - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.LastVal - 1)];
					c.Highest = (reader.IsDBNull(((int)SecurityRealtimeColumn.Highest - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Highest - 1)];
					c.Lowest = (reader.IsDBNull(((int)SecurityRealtimeColumn.Lowest - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Lowest - 1)];
					c.Totalshares = (reader.IsDBNull(((int)SecurityRealtimeColumn.Totalshares - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Totalshares - 1)];
					c.TotalValue = (reader.IsDBNull(((int)SecurityRealtimeColumn.TotalValue - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.TotalValue - 1)];
					c.AccumulateDeal = (reader.IsDBNull(((int)SecurityRealtimeColumn.AccumulateDeal - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.AccumulateDeal - 1)];
					c.BigDeal = (reader.IsDBNull(((int)SecurityRealtimeColumn.BigDeal - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.BigDeal - 1)];
					c.BigVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.BigVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.BigVolume - 1)];
					c.BigValue = (reader.IsDBNull(((int)SecurityRealtimeColumn.BigValue - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.BigValue - 1)];
					c.OddDeal = (reader.IsDBNull(((int)SecurityRealtimeColumn.OddDeal - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.OddDeal - 1)];
					c.OddVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.OddVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.OddVolume - 1)];
					c.OddValue = (reader.IsDBNull(((int)SecurityRealtimeColumn.OddValue - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.OddValue - 1)];
					c.Best1Bid = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best1Bid - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best1Bid - 1)];
					c.Best1BidVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best1BidVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best1BidVolume - 1)];
					c.Best2Bid = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best2Bid - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best2Bid - 1)];
					c.Best2BidVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best2BidVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best2BidVolume - 1)];
					c.Best3Bid = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best3Bid - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best3Bid - 1)];
					c.Best3BidVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best3BidVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best3BidVolume - 1)];
					c.Best1Offer = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best1Offer - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best1Offer - 1)];
					c.Best1OfferVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best1OfferVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best1OfferVolume - 1)];
					c.Best2Offer = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best2Offer - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best2Offer - 1)];
					c.Best2OfferVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best2OfferVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best2OfferVolume - 1)];
					c.Best3Offer = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best3Offer - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best3Offer - 1)];
					c.Best3OfferVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best3OfferVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best3OfferVolume - 1)];
					c.BoardLost = (reader.IsDBNull(((int)SecurityRealtimeColumn.BoardLost - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.BoardLost - 1)];
					c.TotalRoom = (reader.IsDBNull(((int)SecurityRealtimeColumn.TotalRoom - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.TotalRoom - 1)];
					c.CurrentRoom = (reader.IsDBNull(((int)SecurityRealtimeColumn.CurrentRoom - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.CurrentRoom - 1)];
					c.StartRoom = (reader.IsDBNull(((int)SecurityRealtimeColumn.StartRoom - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.StartRoom - 1)];
					c.Sequence = (reader.IsDBNull(((int)SecurityRealtimeColumn.Sequence - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Sequence - 1)];
                    c.IsVn30 = (reader.IsDBNull(((int)SecurityRealtimeColumn.IsVn30 - 1)))?null:(System.Boolean?)reader[((int)SecurityRealtimeColumn.IsVn30 - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.SecurityRealtime"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.SecurityRealtime"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, RTStockData.Entities.SecurityRealtime entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)SecurityRealtimeColumn.Id - 1)];
			entity.TradeDate = (reader.IsDBNull(((int)SecurityRealtimeColumn.TradeDate - 1)))?null:(System.DateTime?)reader[((int)SecurityRealtimeColumn.TradeDate - 1)];
			entity.Stockno = (reader.IsDBNull(((int)SecurityRealtimeColumn.Stockno - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.Stockno - 1)];
			entity.StockSymbol = (reader.IsDBNull(((int)SecurityRealtimeColumn.StockSymbol - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.StockSymbol - 1)];
			entity.StockType = (reader.IsDBNull(((int)SecurityRealtimeColumn.StockType - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.StockType - 1)];
			entity.Ceiling = (reader.IsDBNull(((int)SecurityRealtimeColumn.Ceiling - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Ceiling - 1)];
			entity.Floor = (reader.IsDBNull(((int)SecurityRealtimeColumn.Floor - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Floor - 1)];
			entity.BigLotValue = (reader.IsDBNull(((int)SecurityRealtimeColumn.BigLotValue - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.BigLotValue - 1)];
			entity.SecurityName = (reader.IsDBNull(((int)SecurityRealtimeColumn.SecurityName - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.SecurityName - 1)];
			entity.SectorNo = (reader.IsDBNull(((int)SecurityRealtimeColumn.SectorNo - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.SectorNo - 1)];
			entity.Designated = (reader.IsDBNull(((int)SecurityRealtimeColumn.Designated - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Designated - 1)];
			entity.Suspension = (reader.IsDBNull(((int)SecurityRealtimeColumn.Suspension - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Suspension - 1)];
			entity.Delist = (reader.IsDBNull(((int)SecurityRealtimeColumn.Delist - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Delist - 1)];
			entity.HaltResumeFlag = (reader.IsDBNull(((int)SecurityRealtimeColumn.HaltResumeFlag - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.HaltResumeFlag - 1)];
			entity.Split = (reader.IsDBNull(((int)SecurityRealtimeColumn.Split - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Split - 1)];
			entity.Benefit = (reader.IsDBNull(((int)SecurityRealtimeColumn.Benefit - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Benefit - 1)];
			entity.Meeting = (reader.IsDBNull(((int)SecurityRealtimeColumn.Meeting - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Meeting - 1)];
			entity.Notice = (reader.IsDBNull(((int)SecurityRealtimeColumn.Notice - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.Notice - 1)];
			entity.ClientidRequired = (reader.IsDBNull(((int)SecurityRealtimeColumn.ClientidRequired - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.ClientidRequired - 1)];
			entity.CouponRate = (reader.IsDBNull(((int)SecurityRealtimeColumn.CouponRate - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.CouponRate - 1)];
			entity.IssueDate = (reader.IsDBNull(((int)SecurityRealtimeColumn.IssueDate - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.IssueDate - 1)];
			entity.MatureDate = (reader.IsDBNull(((int)SecurityRealtimeColumn.MatureDate - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.MatureDate - 1)];
			entity.AvrPrice = (reader.IsDBNull(((int)SecurityRealtimeColumn.AvrPrice - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.AvrPrice - 1)];
			entity.ParValue = (reader.IsDBNull(((int)SecurityRealtimeColumn.ParValue - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.ParValue - 1)];
			entity.SdcFlag = (reader.IsDBNull(((int)SecurityRealtimeColumn.SdcFlag - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.SdcFlag - 1)];
			entity.PriorClosePrice = (reader.IsDBNull(((int)SecurityRealtimeColumn.PriorClosePrice - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.PriorClosePrice - 1)];
			entity.PriorCloseDate = (reader.IsDBNull(((int)SecurityRealtimeColumn.PriorCloseDate - 1)))?null:(System.String)reader[((int)SecurityRealtimeColumn.PriorCloseDate - 1)];
			entity.ProjectOpen = (reader.IsDBNull(((int)SecurityRealtimeColumn.ProjectOpen - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.ProjectOpen - 1)];
			entity.OpenPrice = (reader.IsDBNull(((int)SecurityRealtimeColumn.OpenPrice - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.OpenPrice - 1)];
			entity.Last = (reader.IsDBNull(((int)SecurityRealtimeColumn.Last - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Last - 1)];
			entity.LastVol = (reader.IsDBNull(((int)SecurityRealtimeColumn.LastVol - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.LastVol - 1)];
			entity.LastVal = (reader.IsDBNull(((int)SecurityRealtimeColumn.LastVal - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.LastVal - 1)];
			entity.Highest = (reader.IsDBNull(((int)SecurityRealtimeColumn.Highest - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Highest - 1)];
			entity.Lowest = (reader.IsDBNull(((int)SecurityRealtimeColumn.Lowest - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Lowest - 1)];
			entity.Totalshares = (reader.IsDBNull(((int)SecurityRealtimeColumn.Totalshares - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Totalshares - 1)];
			entity.TotalValue = (reader.IsDBNull(((int)SecurityRealtimeColumn.TotalValue - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.TotalValue - 1)];
			entity.AccumulateDeal = (reader.IsDBNull(((int)SecurityRealtimeColumn.AccumulateDeal - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.AccumulateDeal - 1)];
			entity.BigDeal = (reader.IsDBNull(((int)SecurityRealtimeColumn.BigDeal - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.BigDeal - 1)];
			entity.BigVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.BigVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.BigVolume - 1)];
			entity.BigValue = (reader.IsDBNull(((int)SecurityRealtimeColumn.BigValue - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.BigValue - 1)];
			entity.OddDeal = (reader.IsDBNull(((int)SecurityRealtimeColumn.OddDeal - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.OddDeal - 1)];
			entity.OddVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.OddVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.OddVolume - 1)];
			entity.OddValue = (reader.IsDBNull(((int)SecurityRealtimeColumn.OddValue - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.OddValue - 1)];
			entity.Best1Bid = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best1Bid - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best1Bid - 1)];
			entity.Best1BidVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best1BidVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best1BidVolume - 1)];
			entity.Best2Bid = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best2Bid - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best2Bid - 1)];
			entity.Best2BidVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best2BidVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best2BidVolume - 1)];
			entity.Best3Bid = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best3Bid - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best3Bid - 1)];
			entity.Best3BidVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best3BidVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best3BidVolume - 1)];
			entity.Best1Offer = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best1Offer - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best1Offer - 1)];
			entity.Best1OfferVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best1OfferVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best1OfferVolume - 1)];
			entity.Best2Offer = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best2Offer - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best2Offer - 1)];
			entity.Best2OfferVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best2OfferVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best2OfferVolume - 1)];
			entity.Best3Offer = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best3Offer - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best3Offer - 1)];
			entity.Best3OfferVolume = (reader.IsDBNull(((int)SecurityRealtimeColumn.Best3OfferVolume - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Best3OfferVolume - 1)];
			entity.BoardLost = (reader.IsDBNull(((int)SecurityRealtimeColumn.BoardLost - 1)))?null:(System.Int16?)reader[((int)SecurityRealtimeColumn.BoardLost - 1)];
			entity.TotalRoom = (reader.IsDBNull(((int)SecurityRealtimeColumn.TotalRoom - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.TotalRoom - 1)];
			entity.CurrentRoom = (reader.IsDBNull(((int)SecurityRealtimeColumn.CurrentRoom - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.CurrentRoom - 1)];
			entity.StartRoom = (reader.IsDBNull(((int)SecurityRealtimeColumn.StartRoom - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.StartRoom - 1)];
			entity.Sequence = (reader.IsDBNull(((int)SecurityRealtimeColumn.Sequence - 1)))?null:(System.Int64?)reader[((int)SecurityRealtimeColumn.Sequence - 1)];
            entity.IsVn30 = (reader.IsDBNull(((int)SecurityRealtimeColumn.IsVn30 - 1)))?null:(System.Boolean?)reader[((int)SecurityRealtimeColumn.IsVn30 - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.SecurityRealtime"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.SecurityRealtime"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, RTStockData.Entities.SecurityRealtime entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["id"];
			entity.TradeDate = Convert.IsDBNull(dataRow["TradeDate"]) ? null : (System.DateTime?)dataRow["TradeDate"];
			entity.Stockno = Convert.IsDBNull(dataRow["Stockno"]) ? null : (System.Int16?)dataRow["Stockno"];
			entity.StockSymbol = Convert.IsDBNull(dataRow["StockSymbol"]) ? null : (System.String)dataRow["StockSymbol"];
			entity.StockType = Convert.IsDBNull(dataRow["StockType"]) ? null : (System.String)dataRow["StockType"];
			entity.Ceiling = Convert.IsDBNull(dataRow["Ceiling"]) ? null : (System.Int64?)dataRow["Ceiling"];
			entity.Floor = Convert.IsDBNull(dataRow["Floor"]) ? null : (System.Int64?)dataRow["Floor"];
			entity.BigLotValue = Convert.IsDBNull(dataRow["BigLotValue"]) ? null : (System.Int64?)dataRow["BigLotValue"];
			entity.SecurityName = Convert.IsDBNull(dataRow["SecurityName"]) ? null : (System.String)dataRow["SecurityName"];
			entity.SectorNo = Convert.IsDBNull(dataRow["SectorNo"]) ? null : (System.String)dataRow["SectorNo"];
			entity.Designated = Convert.IsDBNull(dataRow["Designated"]) ? null : (System.String)dataRow["Designated"];
			entity.Suspension = Convert.IsDBNull(dataRow["SUSPENSION"]) ? null : (System.String)dataRow["SUSPENSION"];
			entity.Delist = Convert.IsDBNull(dataRow["Delist"]) ? null : (System.String)dataRow["Delist"];
			entity.HaltResumeFlag = Convert.IsDBNull(dataRow["HaltResumeFlag"]) ? null : (System.String)dataRow["HaltResumeFlag"];
			entity.Split = Convert.IsDBNull(dataRow["SPLIT"]) ? null : (System.String)dataRow["SPLIT"];
			entity.Benefit = Convert.IsDBNull(dataRow["Benefit"]) ? null : (System.String)dataRow["Benefit"];
			entity.Meeting = Convert.IsDBNull(dataRow["Meeting"]) ? null : (System.String)dataRow["Meeting"];
			entity.Notice = Convert.IsDBNull(dataRow["Notice"]) ? null : (System.String)dataRow["Notice"];
			entity.ClientidRequired = Convert.IsDBNull(dataRow["ClientidRequired"]) ? null : (System.String)dataRow["ClientidRequired"];
			entity.CouponRate = Convert.IsDBNull(dataRow["CouponRate"]) ? null : (System.Int16?)dataRow["CouponRate"];
			entity.IssueDate = Convert.IsDBNull(dataRow["IssueDate"]) ? null : (System.String)dataRow["IssueDate"];
			entity.MatureDate = Convert.IsDBNull(dataRow["MatureDate"]) ? null : (System.String)dataRow["MatureDate"];
			entity.AvrPrice = Convert.IsDBNull(dataRow["AvrPrice"]) ? null : (System.Int64?)dataRow["AvrPrice"];
			entity.ParValue = Convert.IsDBNull(dataRow["ParValue"]) ? null : (System.Int16?)dataRow["ParValue"];
			entity.SdcFlag = Convert.IsDBNull(dataRow["SDCFlag"]) ? null : (System.String)dataRow["SDCFlag"];
			entity.PriorClosePrice = Convert.IsDBNull(dataRow["PriorClosePrice"]) ? null : (System.Int64?)dataRow["PriorClosePrice"];
			entity.PriorCloseDate = Convert.IsDBNull(dataRow["PriorCloseDate"]) ? null : (System.String)dataRow["PriorCloseDate"];
			entity.ProjectOpen = Convert.IsDBNull(dataRow["ProjectOpen"]) ? null : (System.Int64?)dataRow["ProjectOpen"];
			entity.OpenPrice = Convert.IsDBNull(dataRow["OpenPrice"]) ? null : (System.Int64?)dataRow["OpenPrice"];
			entity.Last = Convert.IsDBNull(dataRow["Last"]) ? null : (System.Int64?)dataRow["Last"];
			entity.LastVol = Convert.IsDBNull(dataRow["LastVol"]) ? null : (System.Int64?)dataRow["LastVol"];
			entity.LastVal = Convert.IsDBNull(dataRow["LastVal"]) ? null : (System.Int64?)dataRow["LastVal"];
			entity.Highest = Convert.IsDBNull(dataRow["Highest"]) ? null : (System.Int64?)dataRow["Highest"];
			entity.Lowest = Convert.IsDBNull(dataRow["Lowest"]) ? null : (System.Int64?)dataRow["Lowest"];
			entity.Totalshares = Convert.IsDBNull(dataRow["Totalshares"]) ? null : (System.Int64?)dataRow["Totalshares"];
			entity.TotalValue = Convert.IsDBNull(dataRow["TotalValue"]) ? null : (System.Int64?)dataRow["TotalValue"];
			entity.AccumulateDeal = Convert.IsDBNull(dataRow["AccumulateDeal"]) ? null : (System.Int16?)dataRow["AccumulateDeal"];
			entity.BigDeal = Convert.IsDBNull(dataRow["BigDeal"]) ? null : (System.Int16?)dataRow["BigDeal"];
			entity.BigVolume = Convert.IsDBNull(dataRow["BigVolume"]) ? null : (System.Int64?)dataRow["BigVolume"];
			entity.BigValue = Convert.IsDBNull(dataRow["BigValue"]) ? null : (System.Int64?)dataRow["BigValue"];
			entity.OddDeal = Convert.IsDBNull(dataRow["OddDeal"]) ? null : (System.Int16?)dataRow["OddDeal"];
			entity.OddVolume = Convert.IsDBNull(dataRow["OddVolume"]) ? null : (System.Int64?)dataRow["OddVolume"];
			entity.OddValue = Convert.IsDBNull(dataRow["OddValue"]) ? null : (System.Int64?)dataRow["OddValue"];
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
			entity.BoardLost = Convert.IsDBNull(dataRow["BoardLost"]) ? null : (System.Int16?)dataRow["BoardLost"];
			entity.TotalRoom = Convert.IsDBNull(dataRow["TotalRoom"]) ? null : (System.Int64?)dataRow["TotalRoom"];
			entity.CurrentRoom = Convert.IsDBNull(dataRow["CurrentRoom"]) ? null : (System.Int64?)dataRow["CurrentRoom"];
			entity.StartRoom = Convert.IsDBNull(dataRow["StartRoom"]) ? null : (System.Int64?)dataRow["StartRoom"];
			entity.Sequence = Convert.IsDBNull(dataRow["Sequence"]) ? null : (System.Int64?)dataRow["Sequence"];
            entity.IsVn30 = Convert.IsDBNull(dataRow["IsVn30"]) ? null : (System.Boolean?)dataRow["IsVN30"];
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
		/// <param name="entity">The <see cref="RTStockData.Entities.SecurityRealtime"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">RTStockData.Entities.SecurityRealtime Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, RTStockData.Entities.SecurityRealtime entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the RTStockData.Entities.SecurityRealtime object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">RTStockData.Entities.SecurityRealtime instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">RTStockData.Entities.SecurityRealtime Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, RTStockData.Entities.SecurityRealtime entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region SecurityRealtimeChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>RTStockData.Entities.SecurityRealtime</c>
	///</summary>
	public enum SecurityRealtimeChildEntityTypes
	{
	}
	
	#endregion SecurityRealtimeChildEntityTypes
	
	#region SecurityRealtimeFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;SecurityRealtimeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SecurityRealtime"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SecurityRealtimeFilterBuilder : SqlFilterBuilder<SecurityRealtimeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SecurityRealtimeFilterBuilder class.
		/// </summary>
		public SecurityRealtimeFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SecurityRealtimeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SecurityRealtimeFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SecurityRealtimeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SecurityRealtimeFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SecurityRealtimeFilterBuilder
	
	#region SecurityRealtimeParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;SecurityRealtimeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SecurityRealtime"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SecurityRealtimeParameterBuilder : ParameterizedSqlFilterBuilder<SecurityRealtimeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SecurityRealtimeParameterBuilder class.
		/// </summary>
		public SecurityRealtimeParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SecurityRealtimeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SecurityRealtimeParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SecurityRealtimeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SecurityRealtimeParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SecurityRealtimeParameterBuilder
	
	#region SecurityRealtimeSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;SecurityRealtimeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SecurityRealtime"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class SecurityRealtimeSortBuilder : SqlSortBuilder<SecurityRealtimeColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SecurityRealtimeSqlSortBuilder class.
		/// </summary>
		public SecurityRealtimeSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion SecurityRealtimeSortBuilder
	
} // end namespace
