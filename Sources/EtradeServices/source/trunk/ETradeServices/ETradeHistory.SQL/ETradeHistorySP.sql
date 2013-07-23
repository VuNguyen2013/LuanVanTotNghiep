
USE [ETradeHistory]
GO
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.TradedHistory_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.TradedHistory_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.TradedHistory_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the TradedHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.TradedHistory_Get_List

AS


				
				SELECT
					[ID],
					[TradeTime],
					[SubCustAccountID],
					[Type],
					[FISOrderID],
					[SecSymbol],
					[Side],
					[Price],
					[ConPrice],
					[Volume],
					[ExecutedVol],
					[ExecutedPrice],
					[CancelledVolume],
					[MatchedTime],
					[CancelledTime],
					[OrdRejReason],
					[CancelledRejReason],
					[SourceID],
					[Market],
					[RefOrderID],
					[EffDate],
					[ExpDate],
					[MinValue],
					[MaxValue]
				FROM
					[dbo].[TradedHistory]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.TradedHistory_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.TradedHistory_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.TradedHistory_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the TradedHistory table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.TradedHistory_GetPaged
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				IF (@OrderBy IS NULL OR LEN(@OrderBy) < 1)
				BEGIN
					-- default order by to first column
					SET @OrderBy = '[ID]'
				END

				-- SQL Server 2005 Paging
				DECLARE @SQL AS nvarchar(MAX)
				SET @SQL = 'WITH PageIndex AS ('
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' ROW_NUMBER() OVER (ORDER BY ' + @OrderBy + ') as RowIndex'
				SET @SQL = @SQL + ', [ID]'
				SET @SQL = @SQL + ', [TradeTime]'
				SET @SQL = @SQL + ', [SubCustAccountID]'
				SET @SQL = @SQL + ', [Type]'
				SET @SQL = @SQL + ', [FISOrderID]'
				SET @SQL = @SQL + ', [SecSymbol]'
				SET @SQL = @SQL + ', [Side]'
				SET @SQL = @SQL + ', [Price]'
				SET @SQL = @SQL + ', [ConPrice]'
				SET @SQL = @SQL + ', [Volume]'
				SET @SQL = @SQL + ', [ExecutedVol]'
				SET @SQL = @SQL + ', [ExecutedPrice]'
				SET @SQL = @SQL + ', [CancelledVolume]'
				SET @SQL = @SQL + ', [MatchedTime]'
				SET @SQL = @SQL + ', [CancelledTime]'
				SET @SQL = @SQL + ', [OrdRejReason]'
				SET @SQL = @SQL + ', [CancelledRejReason]'
				SET @SQL = @SQL + ', [SourceID]'
				SET @SQL = @SQL + ', [Market]'
				SET @SQL = @SQL + ', [RefOrderID]'
				SET @SQL = @SQL + ', [EffDate]'
				SET @SQL = @SQL + ', [ExpDate]'
				SET @SQL = @SQL + ', [MinValue]'
				SET @SQL = @SQL + ', [MaxValue]'
				SET @SQL = @SQL + ' FROM [dbo].[TradedHistory]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [ID],'
				SET @SQL = @SQL + ' [TradeTime],'
				SET @SQL = @SQL + ' [SubCustAccountID],'
				SET @SQL = @SQL + ' [Type],'
				SET @SQL = @SQL + ' [FISOrderID],'
				SET @SQL = @SQL + ' [SecSymbol],'
				SET @SQL = @SQL + ' [Side],'
				SET @SQL = @SQL + ' [Price],'
				SET @SQL = @SQL + ' [ConPrice],'
				SET @SQL = @SQL + ' [Volume],'
				SET @SQL = @SQL + ' [ExecutedVol],'
				SET @SQL = @SQL + ' [ExecutedPrice],'
				SET @SQL = @SQL + ' [CancelledVolume],'
				SET @SQL = @SQL + ' [MatchedTime],'
				SET @SQL = @SQL + ' [CancelledTime],'
				SET @SQL = @SQL + ' [OrdRejReason],'
				SET @SQL = @SQL + ' [CancelledRejReason],'
				SET @SQL = @SQL + ' [SourceID],'
				SET @SQL = @SQL + ' [Market],'
				SET @SQL = @SQL + ' [RefOrderID],'
				SET @SQL = @SQL + ' [EffDate],'
				SET @SQL = @SQL + ' [ExpDate],'
				SET @SQL = @SQL + ' [MinValue],'
				SET @SQL = @SQL + ' [MaxValue]'
				SET @SQL = @SQL + ' FROM PageIndex'
				SET @SQL = @SQL + ' WHERE RowIndex > ' + CONVERT(nvarchar, @PageLowerBound)
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' AND RowIndex <= ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				EXEC sp_executesql @SQL
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[TradedHistory]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.TradedHistory_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.TradedHistory_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.TradedHistory_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the TradedHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.TradedHistory_Insert
(

	@Id bigint    OUTPUT,

	@TradeTime datetime   ,

	@SubCustAccountId varchar (20)  ,

	@Type varchar (10)  ,

	@FisOrderId int   ,

	@SecSymbol varchar (8)  ,

	@Side varchar (1)  ,

	@Price decimal (10, 3)  ,

	@ConPrice varchar (1)  ,

	@Volume bigint   ,

	@ExecutedVol bigint   ,

	@ExecutedPrice decimal (10, 3)  ,

	@CancelledVolume bigint   ,

	@MatchedTime datetime   ,

	@CancelledTime datetime   ,

	@OrdRejReason int   ,

	@CancelledRejReason int   ,

	@SourceId smallint   ,

	@Market varchar (1)  ,

	@RefOrderId varchar (64)  ,

	@EffDate datetime   ,

	@ExpDate datetime   ,

	@MinValue decimal (10, 3)  ,

	@MaxValue decimal (10, 3)  
)
AS


				
				INSERT INTO [dbo].[TradedHistory]
					(
					[TradeTime]
					,[SubCustAccountID]
					,[Type]
					,[FISOrderID]
					,[SecSymbol]
					,[Side]
					,[Price]
					,[ConPrice]
					,[Volume]
					,[ExecutedVol]
					,[ExecutedPrice]
					,[CancelledVolume]
					,[MatchedTime]
					,[CancelledTime]
					,[OrdRejReason]
					,[CancelledRejReason]
					,[SourceID]
					,[Market]
					,[RefOrderID]
					,[EffDate]
					,[ExpDate]
					,[MinValue]
					,[MaxValue]
					)
				VALUES
					(
					@TradeTime
					,@SubCustAccountId
					,@Type
					,@FisOrderId
					,@SecSymbol
					,@Side
					,@Price
					,@ConPrice
					,@Volume
					,@ExecutedVol
					,@ExecutedPrice
					,@CancelledVolume
					,@MatchedTime
					,@CancelledTime
					,@OrdRejReason
					,@CancelledRejReason
					,@SourceId
					,@Market
					,@RefOrderId
					,@EffDate
					,@ExpDate
					,@MinValue
					,@MaxValue
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.TradedHistory_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.TradedHistory_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.TradedHistory_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the TradedHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.TradedHistory_Update
(

	@Id bigint   ,

	@TradeTime datetime   ,

	@SubCustAccountId varchar (20)  ,

	@Type varchar (10)  ,

	@FisOrderId int   ,

	@SecSymbol varchar (8)  ,

	@Side varchar (1)  ,

	@Price decimal (10, 3)  ,

	@ConPrice varchar (1)  ,

	@Volume bigint   ,

	@ExecutedVol bigint   ,

	@ExecutedPrice decimal (10, 3)  ,

	@CancelledVolume bigint   ,

	@MatchedTime datetime   ,

	@CancelledTime datetime   ,

	@OrdRejReason int   ,

	@CancelledRejReason int   ,

	@SourceId smallint   ,

	@Market varchar (1)  ,

	@RefOrderId varchar (64)  ,

	@EffDate datetime   ,

	@ExpDate datetime   ,

	@MinValue decimal (10, 3)  ,

	@MaxValue decimal (10, 3)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[TradedHistory]
				SET
					[TradeTime] = @TradeTime
					,[SubCustAccountID] = @SubCustAccountId
					,[Type] = @Type
					,[FISOrderID] = @FisOrderId
					,[SecSymbol] = @SecSymbol
					,[Side] = @Side
					,[Price] = @Price
					,[ConPrice] = @ConPrice
					,[Volume] = @Volume
					,[ExecutedVol] = @ExecutedVol
					,[ExecutedPrice] = @ExecutedPrice
					,[CancelledVolume] = @CancelledVolume
					,[MatchedTime] = @MatchedTime
					,[CancelledTime] = @CancelledTime
					,[OrdRejReason] = @OrdRejReason
					,[CancelledRejReason] = @CancelledRejReason
					,[SourceID] = @SourceId
					,[Market] = @Market
					,[RefOrderID] = @RefOrderId
					,[EffDate] = @EffDate
					,[ExpDate] = @ExpDate
					,[MinValue] = @MinValue
					,[MaxValue] = @MaxValue
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.TradedHistory_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.TradedHistory_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.TradedHistory_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the TradedHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.TradedHistory_Delete
(

	@Id bigint   
)
AS


				DELETE FROM [dbo].[TradedHistory] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.TradedHistory_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.TradedHistory_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.TradedHistory_GetById
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the TradedHistory table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.TradedHistory_GetById
(

	@Id bigint   
)
AS


				SELECT
					[ID],
					[TradeTime],
					[SubCustAccountID],
					[Type],
					[FISOrderID],
					[SecSymbol],
					[Side],
					[Price],
					[ConPrice],
					[Volume],
					[ExecutedVol],
					[ExecutedPrice],
					[CancelledVolume],
					[MatchedTime],
					[CancelledTime],
					[OrdRejReason],
					[CancelledRejReason],
					[SourceID],
					[Market],
					[RefOrderID],
					[EffDate],
					[ExpDate],
					[MinValue],
					[MaxValue]
				FROM
					[dbo].[TradedHistory]
				WHERE
					[ID] = @Id
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.TradedHistory_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.TradedHistory_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.TradedHistory_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the TradedHistory table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.TradedHistory_Find
(

	@SearchUsingOR bit   = null ,

	@Id bigint   = null ,

	@TradeTime datetime   = null ,

	@SubCustAccountId varchar (20)  = null ,

	@Type varchar (10)  = null ,

	@FisOrderId int   = null ,

	@SecSymbol varchar (8)  = null ,

	@Side varchar (1)  = null ,

	@Price decimal (10, 3)  = null ,

	@ConPrice varchar (1)  = null ,

	@Volume bigint   = null ,

	@ExecutedVol bigint   = null ,

	@ExecutedPrice decimal (10, 3)  = null ,

	@CancelledVolume bigint   = null ,

	@MatchedTime datetime   = null ,

	@CancelledTime datetime   = null ,

	@OrdRejReason int   = null ,

	@CancelledRejReason int   = null ,

	@SourceId smallint   = null ,

	@Market varchar (1)  = null ,

	@RefOrderId varchar (64)  = null ,

	@EffDate datetime   = null ,

	@ExpDate datetime   = null ,

	@MinValue decimal (10, 3)  = null ,

	@MaxValue decimal (10, 3)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [TradeTime]
	, [SubCustAccountID]
	, [Type]
	, [FISOrderID]
	, [SecSymbol]
	, [Side]
	, [Price]
	, [ConPrice]
	, [Volume]
	, [ExecutedVol]
	, [ExecutedPrice]
	, [CancelledVolume]
	, [MatchedTime]
	, [CancelledTime]
	, [OrdRejReason]
	, [CancelledRejReason]
	, [SourceID]
	, [Market]
	, [RefOrderID]
	, [EffDate]
	, [ExpDate]
	, [MinValue]
	, [MaxValue]
    FROM
	[dbo].[TradedHistory]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([TradeTime] = @TradeTime OR @TradeTime IS NULL)
	AND ([SubCustAccountID] = @SubCustAccountId OR @SubCustAccountId IS NULL)
	AND ([Type] = @Type OR @Type IS NULL)
	AND ([FISOrderID] = @FisOrderId OR @FisOrderId IS NULL)
	AND ([SecSymbol] = @SecSymbol OR @SecSymbol IS NULL)
	AND ([Side] = @Side OR @Side IS NULL)
	AND ([Price] = @Price OR @Price IS NULL)
	AND ([ConPrice] = @ConPrice OR @ConPrice IS NULL)
	AND ([Volume] = @Volume OR @Volume IS NULL)
	AND ([ExecutedVol] = @ExecutedVol OR @ExecutedVol IS NULL)
	AND ([ExecutedPrice] = @ExecutedPrice OR @ExecutedPrice IS NULL)
	AND ([CancelledVolume] = @CancelledVolume OR @CancelledVolume IS NULL)
	AND ([MatchedTime] = @MatchedTime OR @MatchedTime IS NULL)
	AND ([CancelledTime] = @CancelledTime OR @CancelledTime IS NULL)
	AND ([OrdRejReason] = @OrdRejReason OR @OrdRejReason IS NULL)
	AND ([CancelledRejReason] = @CancelledRejReason OR @CancelledRejReason IS NULL)
	AND ([SourceID] = @SourceId OR @SourceId IS NULL)
	AND ([Market] = @Market OR @Market IS NULL)
	AND ([RefOrderID] = @RefOrderId OR @RefOrderId IS NULL)
	AND ([EffDate] = @EffDate OR @EffDate IS NULL)
	AND ([ExpDate] = @ExpDate OR @ExpDate IS NULL)
	AND ([MinValue] = @MinValue OR @MinValue IS NULL)
	AND ([MaxValue] = @MaxValue OR @MaxValue IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [TradeTime]
	, [SubCustAccountID]
	, [Type]
	, [FISOrderID]
	, [SecSymbol]
	, [Side]
	, [Price]
	, [ConPrice]
	, [Volume]
	, [ExecutedVol]
	, [ExecutedPrice]
	, [CancelledVolume]
	, [MatchedTime]
	, [CancelledTime]
	, [OrdRejReason]
	, [CancelledRejReason]
	, [SourceID]
	, [Market]
	, [RefOrderID]
	, [EffDate]
	, [ExpDate]
	, [MinValue]
	, [MaxValue]
    FROM
	[dbo].[TradedHistory]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([TradeTime] = @TradeTime AND @TradeTime is not null)
	OR ([SubCustAccountID] = @SubCustAccountId AND @SubCustAccountId is not null)
	OR ([Type] = @Type AND @Type is not null)
	OR ([FISOrderID] = @FisOrderId AND @FisOrderId is not null)
	OR ([SecSymbol] = @SecSymbol AND @SecSymbol is not null)
	OR ([Side] = @Side AND @Side is not null)
	OR ([Price] = @Price AND @Price is not null)
	OR ([ConPrice] = @ConPrice AND @ConPrice is not null)
	OR ([Volume] = @Volume AND @Volume is not null)
	OR ([ExecutedVol] = @ExecutedVol AND @ExecutedVol is not null)
	OR ([ExecutedPrice] = @ExecutedPrice AND @ExecutedPrice is not null)
	OR ([CancelledVolume] = @CancelledVolume AND @CancelledVolume is not null)
	OR ([MatchedTime] = @MatchedTime AND @MatchedTime is not null)
	OR ([CancelledTime] = @CancelledTime AND @CancelledTime is not null)
	OR ([OrdRejReason] = @OrdRejReason AND @OrdRejReason is not null)
	OR ([CancelledRejReason] = @CancelledRejReason AND @CancelledRejReason is not null)
	OR ([SourceID] = @SourceId AND @SourceId is not null)
	OR ([Market] = @Market AND @Market is not null)
	OR ([RefOrderID] = @RefOrderId AND @RefOrderId is not null)
	OR ([EffDate] = @EffDate AND @EffDate is not null)
	OR ([ExpDate] = @ExpDate AND @ExpDate is not null)
	OR ([MinValue] = @MinValue AND @MinValue is not null)
	OR ([MaxValue] = @MaxValue AND @MaxValue is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PnLHistory_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PnLHistory_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PnLHistory_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the PnLHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PnLHistory_Get_List

AS


				
				SELECT
					[ID],
					[TradeTime],
					[RefOrderID],
					[FISOrderID],
					[SecSymbol],
					[Price],
					[AvgPrice],
					[Volume],
					[Profit],
					[ProfitabilityRatio],
					[SubCustAccountID],
					[Market]
				FROM
					[dbo].[PnLHistory]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PnLHistory_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PnLHistory_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PnLHistory_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the PnLHistory table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PnLHistory_GetPaged
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				IF (@OrderBy IS NULL OR LEN(@OrderBy) < 1)
				BEGIN
					-- default order by to first column
					SET @OrderBy = '[ID]'
				END

				-- SQL Server 2005 Paging
				DECLARE @SQL AS nvarchar(MAX)
				SET @SQL = 'WITH PageIndex AS ('
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' ROW_NUMBER() OVER (ORDER BY ' + @OrderBy + ') as RowIndex'
				SET @SQL = @SQL + ', [ID]'
				SET @SQL = @SQL + ', [TradeTime]'
				SET @SQL = @SQL + ', [RefOrderID]'
				SET @SQL = @SQL + ', [FISOrderID]'
				SET @SQL = @SQL + ', [SecSymbol]'
				SET @SQL = @SQL + ', [Price]'
				SET @SQL = @SQL + ', [AvgPrice]'
				SET @SQL = @SQL + ', [Volume]'
				SET @SQL = @SQL + ', [Profit]'
				SET @SQL = @SQL + ', [ProfitabilityRatio]'
				SET @SQL = @SQL + ', [SubCustAccountID]'
				SET @SQL = @SQL + ', [Market]'
				SET @SQL = @SQL + ' FROM [dbo].[PnLHistory]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [ID],'
				SET @SQL = @SQL + ' [TradeTime],'
				SET @SQL = @SQL + ' [RefOrderID],'
				SET @SQL = @SQL + ' [FISOrderID],'
				SET @SQL = @SQL + ' [SecSymbol],'
				SET @SQL = @SQL + ' [Price],'
				SET @SQL = @SQL + ' [AvgPrice],'
				SET @SQL = @SQL + ' [Volume],'
				SET @SQL = @SQL + ' [Profit],'
				SET @SQL = @SQL + ' [ProfitabilityRatio],'
				SET @SQL = @SQL + ' [SubCustAccountID],'
				SET @SQL = @SQL + ' [Market]'
				SET @SQL = @SQL + ' FROM PageIndex'
				SET @SQL = @SQL + ' WHERE RowIndex > ' + CONVERT(nvarchar, @PageLowerBound)
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' AND RowIndex <= ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				EXEC sp_executesql @SQL
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[PnLHistory]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PnLHistory_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PnLHistory_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PnLHistory_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the PnLHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PnLHistory_Insert
(

	@Id bigint    OUTPUT,

	@TradeTime datetime   ,

	@RefOrderId varchar (64)  ,

	@FisOrderId int   ,

	@SecSymbol varchar (8)  ,

	@Price decimal (10, 3)  ,

	@AvgPrice decimal (10, 3)  ,

	@Volume int   ,

	@Profit decimal (18, 3)  ,

	@ProfitabilityRatio decimal (8, 3)  ,

	@SubCustAccountId varchar (20)  ,

	@Market varchar (1)  
)
AS


				
				INSERT INTO [dbo].[PnLHistory]
					(
					[TradeTime]
					,[RefOrderID]
					,[FISOrderID]
					,[SecSymbol]
					,[Price]
					,[AvgPrice]
					,[Volume]
					,[Profit]
					,[ProfitabilityRatio]
					,[SubCustAccountID]
					,[Market]
					)
				VALUES
					(
					@TradeTime
					,@RefOrderId
					,@FisOrderId
					,@SecSymbol
					,@Price
					,@AvgPrice
					,@Volume
					,@Profit
					,@ProfitabilityRatio
					,@SubCustAccountId
					,@Market
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PnLHistory_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PnLHistory_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PnLHistory_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the PnLHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PnLHistory_Update
(

	@Id bigint   ,

	@TradeTime datetime   ,

	@RefOrderId varchar (64)  ,

	@FisOrderId int   ,

	@SecSymbol varchar (8)  ,

	@Price decimal (10, 3)  ,

	@AvgPrice decimal (10, 3)  ,

	@Volume int   ,

	@Profit decimal (18, 3)  ,

	@ProfitabilityRatio decimal (8, 3)  ,

	@SubCustAccountId varchar (20)  ,

	@Market varchar (1)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PnLHistory]
				SET
					[TradeTime] = @TradeTime
					,[RefOrderID] = @RefOrderId
					,[FISOrderID] = @FisOrderId
					,[SecSymbol] = @SecSymbol
					,[Price] = @Price
					,[AvgPrice] = @AvgPrice
					,[Volume] = @Volume
					,[Profit] = @Profit
					,[ProfitabilityRatio] = @ProfitabilityRatio
					,[SubCustAccountID] = @SubCustAccountId
					,[Market] = @Market
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PnLHistory_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PnLHistory_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PnLHistory_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the PnLHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PnLHistory_Delete
(

	@Id bigint   
)
AS


				DELETE FROM [dbo].[PnLHistory] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PnLHistory_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PnLHistory_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PnLHistory_GetById
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the PnLHistory table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PnLHistory_GetById
(

	@Id bigint   
)
AS


				SELECT
					[ID],
					[TradeTime],
					[RefOrderID],
					[FISOrderID],
					[SecSymbol],
					[Price],
					[AvgPrice],
					[Volume],
					[Profit],
					[ProfitabilityRatio],
					[SubCustAccountID],
					[Market]
				FROM
					[dbo].[PnLHistory]
				WHERE
					[ID] = @Id
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PnLHistory_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PnLHistory_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PnLHistory_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Friday, November 12, 2010

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the PnLHistory table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PnLHistory_Find
(

	@SearchUsingOR bit   = null ,

	@Id bigint   = null ,

	@TradeTime datetime   = null ,

	@RefOrderId varchar (64)  = null ,

	@FisOrderId int   = null ,

	@SecSymbol varchar (8)  = null ,

	@Price decimal (10, 3)  = null ,

	@AvgPrice decimal (10, 3)  = null ,

	@Volume int   = null ,

	@Profit decimal (18, 3)  = null ,

	@ProfitabilityRatio decimal (8, 3)  = null ,

	@SubCustAccountId varchar (20)  = null ,

	@Market varchar (1)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [TradeTime]
	, [RefOrderID]
	, [FISOrderID]
	, [SecSymbol]
	, [Price]
	, [AvgPrice]
	, [Volume]
	, [Profit]
	, [ProfitabilityRatio]
	, [SubCustAccountID]
	, [Market]
    FROM
	[dbo].[PnLHistory]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([TradeTime] = @TradeTime OR @TradeTime IS NULL)
	AND ([RefOrderID] = @RefOrderId OR @RefOrderId IS NULL)
	AND ([FISOrderID] = @FisOrderId OR @FisOrderId IS NULL)
	AND ([SecSymbol] = @SecSymbol OR @SecSymbol IS NULL)
	AND ([Price] = @Price OR @Price IS NULL)
	AND ([AvgPrice] = @AvgPrice OR @AvgPrice IS NULL)
	AND ([Volume] = @Volume OR @Volume IS NULL)
	AND ([Profit] = @Profit OR @Profit IS NULL)
	AND ([ProfitabilityRatio] = @ProfitabilityRatio OR @ProfitabilityRatio IS NULL)
	AND ([SubCustAccountID] = @SubCustAccountId OR @SubCustAccountId IS NULL)
	AND ([Market] = @Market OR @Market IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [TradeTime]
	, [RefOrderID]
	, [FISOrderID]
	, [SecSymbol]
	, [Price]
	, [AvgPrice]
	, [Volume]
	, [Profit]
	, [ProfitabilityRatio]
	, [SubCustAccountID]
	, [Market]
    FROM
	[dbo].[PnLHistory]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([TradeTime] = @TradeTime AND @TradeTime is not null)
	OR ([RefOrderID] = @RefOrderId AND @RefOrderId is not null)
	OR ([FISOrderID] = @FisOrderId AND @FisOrderId is not null)
	OR ([SecSymbol] = @SecSymbol AND @SecSymbol is not null)
	OR ([Price] = @Price AND @Price is not null)
	OR ([AvgPrice] = @AvgPrice AND @AvgPrice is not null)
	OR ([Volume] = @Volume AND @Volume is not null)
	OR ([Profit] = @Profit AND @Profit is not null)
	OR ([ProfitabilityRatio] = @ProfitabilityRatio AND @ProfitabilityRatio is not null)
	OR ([SubCustAccountID] = @SubCustAccountId AND @SubCustAccountId is not null)
	OR ([Market] = @Market AND @Market is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

