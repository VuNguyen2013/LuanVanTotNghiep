
USE [ETradeOrders]
GO
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrder_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrder_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrder_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the ConditionOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrder_Get_List

AS


				
				SELECT
					[ConditionOrderID],
					[SecSymbol],
					[Side],
					[Price],
					[Volume],
					[MatchedVolume],
					[SubCustAccountID],
					[MainCustAccountID],
					[Market],
					[EffDate],
					[ExpDate],
					[TypeOfCond],
					[MaxValue],
					[MinValue],
					[Status],
					[TradeTime],
					[DoneTime],
					[RejectReason]
				FROM
					[dbo].[ConditionOrder]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrder_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrder_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrder_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the ConditionOrder table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrder_GetPaged
(

	@WhereClause nvarchar (2000)  ,

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
					SET @OrderBy = '[ConditionOrderID]'
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
				SET @SQL = @SQL + ', [ConditionOrderID]'
				SET @SQL = @SQL + ', [SecSymbol]'
				SET @SQL = @SQL + ', [Side]'
				SET @SQL = @SQL + ', [Price]'
				SET @SQL = @SQL + ', [Volume]'
				SET @SQL = @SQL + ', [MatchedVolume]'
				SET @SQL = @SQL + ', [SubCustAccountID]'
				SET @SQL = @SQL + ', [MainCustAccountID]'
				SET @SQL = @SQL + ', [Market]'
				SET @SQL = @SQL + ', [EffDate]'
				SET @SQL = @SQL + ', [ExpDate]'
				SET @SQL = @SQL + ', [TypeOfCond]'
				SET @SQL = @SQL + ', [MaxValue]'
				SET @SQL = @SQL + ', [MinValue]'
				SET @SQL = @SQL + ', [Status]'
				SET @SQL = @SQL + ', [TradeTime]'
				SET @SQL = @SQL + ', [DoneTime]'
				SET @SQL = @SQL + ', [RejectReason]'
				SET @SQL = @SQL + ' FROM [dbo].[ConditionOrder]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [ConditionOrderID],'
				SET @SQL = @SQL + ' [SecSymbol],'
				SET @SQL = @SQL + ' [Side],'
				SET @SQL = @SQL + ' [Price],'
				SET @SQL = @SQL + ' [Volume],'
				SET @SQL = @SQL + ' [MatchedVolume],'
				SET @SQL = @SQL + ' [SubCustAccountID],'
				SET @SQL = @SQL + ' [MainCustAccountID],'
				SET @SQL = @SQL + ' [Market],'
				SET @SQL = @SQL + ' [EffDate],'
				SET @SQL = @SQL + ' [ExpDate],'
				SET @SQL = @SQL + ' [TypeOfCond],'
				SET @SQL = @SQL + ' [MaxValue],'
				SET @SQL = @SQL + ' [MinValue],'
				SET @SQL = @SQL + ' [Status],'
				SET @SQL = @SQL + ' [TradeTime],'
				SET @SQL = @SQL + ' [DoneTime],'
				SET @SQL = @SQL + ' [RejectReason]'
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
				SET @SQL = @SQL + ' FROM [dbo].[ConditionOrder]'
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

	

-- Drop the dbo.ConditionOrder_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrder_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrder_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the ConditionOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrder_Insert
(

	@ConditionOrderId bigint    OUTPUT,

	@SecSymbol varchar (8)  ,

	@Side varchar (1)  ,

	@Price decimal (10, 3)  ,

	@Volume int   ,

	@MatchedVolume int   ,

	@SubCustAccountId varchar (20)  ,

	@MainCustAccountId varchar (20)  ,

	@Market varchar (1)  ,

	@EffDate datetime   ,

	@ExpDate datetime   ,

	@TypeOfCond smallint   ,

	@MaxValue decimal (10, 3)  ,

	@MinValue decimal (10, 3)  ,

	@Status varchar (1)  ,

	@TradeTime datetime   ,

	@DoneTime datetime   ,

	@RejectReason int   
)
AS


				
				INSERT INTO [dbo].[ConditionOrder]
					(
					[SecSymbol]
					,[Side]
					,[Price]
					,[Volume]
					,[MatchedVolume]
					,[SubCustAccountID]
					,[MainCustAccountID]
					,[Market]
					,[EffDate]
					,[ExpDate]
					,[TypeOfCond]
					,[MaxValue]
					,[MinValue]
					,[Status]
					,[TradeTime]
					,[DoneTime]
					,[RejectReason]
					)
				VALUES
					(
					@SecSymbol
					,@Side
					,@Price
					,@Volume
					,@MatchedVolume
					,@SubCustAccountId
					,@MainCustAccountId
					,@Market
					,@EffDate
					,@ExpDate
					,@TypeOfCond
					,@MaxValue
					,@MinValue
					,@Status
					,@TradeTime
					,@DoneTime
					,@RejectReason
					)
				
				-- Get the identity value
				SET @ConditionOrderId = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrder_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrder_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrder_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the ConditionOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrder_Update
(

	@ConditionOrderId bigint   ,

	@SecSymbol varchar (8)  ,

	@Side varchar (1)  ,

	@Price decimal (10, 3)  ,

	@Volume int   ,

	@MatchedVolume int   ,

	@SubCustAccountId varchar (20)  ,

	@MainCustAccountId varchar (20)  ,

	@Market varchar (1)  ,

	@EffDate datetime   ,

	@ExpDate datetime   ,

	@TypeOfCond smallint   ,

	@MaxValue decimal (10, 3)  ,

	@MinValue decimal (10, 3)  ,

	@Status varchar (1)  ,

	@TradeTime datetime   ,

	@DoneTime datetime   ,

	@RejectReason int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[ConditionOrder]
				SET
					[SecSymbol] = @SecSymbol
					,[Side] = @Side
					,[Price] = @Price
					,[Volume] = @Volume
					,[MatchedVolume] = @MatchedVolume
					,[SubCustAccountID] = @SubCustAccountId
					,[MainCustAccountID] = @MainCustAccountId
					,[Market] = @Market
					,[EffDate] = @EffDate
					,[ExpDate] = @ExpDate
					,[TypeOfCond] = @TypeOfCond
					,[MaxValue] = @MaxValue
					,[MinValue] = @MinValue
					,[Status] = @Status
					,[TradeTime] = @TradeTime
					,[DoneTime] = @DoneTime
					,[RejectReason] = @RejectReason
				WHERE
[ConditionOrderID] = @ConditionOrderId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrder_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrder_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrder_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the ConditionOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrder_Delete
(

	@ConditionOrderId bigint   
)
AS


				DELETE FROM [dbo].[ConditionOrder] WITH (ROWLOCK) 
				WHERE
					[ConditionOrderID] = @ConditionOrderId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrder_GetByConditionOrderId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrder_GetByConditionOrderId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrder_GetByConditionOrderId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the ConditionOrder table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrder_GetByConditionOrderId
(

	@ConditionOrderId bigint   
)
AS


				SELECT
					[ConditionOrderID],
					[SecSymbol],
					[Side],
					[Price],
					[Volume],
					[MatchedVolume],
					[SubCustAccountID],
					[MainCustAccountID],
					[Market],
					[EffDate],
					[ExpDate],
					[TypeOfCond],
					[MaxValue],
					[MinValue],
					[Status],
					[TradeTime],
					[DoneTime],
					[RejectReason]
				FROM
					[dbo].[ConditionOrder]
				WHERE
					[ConditionOrderID] = @ConditionOrderId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrder_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrder_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrder_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the ConditionOrder table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrder_Find
(

	@SearchUsingOR bit   = null ,

	@ConditionOrderId bigint   = null ,

	@SecSymbol varchar (8)  = null ,

	@Side varchar (1)  = null ,

	@Price decimal (10, 3)  = null ,

	@Volume int   = null ,

	@MatchedVolume int   = null ,

	@SubCustAccountId varchar (20)  = null ,

	@MainCustAccountId varchar (20)  = null ,

	@Market varchar (1)  = null ,

	@EffDate datetime   = null ,

	@ExpDate datetime   = null ,

	@TypeOfCond smallint   = null ,

	@MaxValue decimal (10, 3)  = null ,

	@MinValue decimal (10, 3)  = null ,

	@Status varchar (1)  = null ,

	@TradeTime datetime   = null ,

	@DoneTime datetime   = null ,

	@RejectReason int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ConditionOrderID]
	, [SecSymbol]
	, [Side]
	, [Price]
	, [Volume]
	, [MatchedVolume]
	, [SubCustAccountID]
	, [MainCustAccountID]
	, [Market]
	, [EffDate]
	, [ExpDate]
	, [TypeOfCond]
	, [MaxValue]
	, [MinValue]
	, [Status]
	, [TradeTime]
	, [DoneTime]
	, [RejectReason]
    FROM
	[dbo].[ConditionOrder]
    WHERE 
	 ([ConditionOrderID] = @ConditionOrderId OR @ConditionOrderId IS NULL)
	AND ([SecSymbol] = @SecSymbol OR @SecSymbol IS NULL)
	AND ([Side] = @Side OR @Side IS NULL)
	AND ([Price] = @Price OR @Price IS NULL)
	AND ([Volume] = @Volume OR @Volume IS NULL)
	AND ([MatchedVolume] = @MatchedVolume OR @MatchedVolume IS NULL)
	AND ([SubCustAccountID] = @SubCustAccountId OR @SubCustAccountId IS NULL)
	AND ([MainCustAccountID] = @MainCustAccountId OR @MainCustAccountId IS NULL)
	AND ([Market] = @Market OR @Market IS NULL)
	AND ([EffDate] = @EffDate OR @EffDate IS NULL)
	AND ([ExpDate] = @ExpDate OR @ExpDate IS NULL)
	AND ([TypeOfCond] = @TypeOfCond OR @TypeOfCond IS NULL)
	AND ([MaxValue] = @MaxValue OR @MaxValue IS NULL)
	AND ([MinValue] = @MinValue OR @MinValue IS NULL)
	AND ([Status] = @Status OR @Status IS NULL)
	AND ([TradeTime] = @TradeTime OR @TradeTime IS NULL)
	AND ([DoneTime] = @DoneTime OR @DoneTime IS NULL)
	AND ([RejectReason] = @RejectReason OR @RejectReason IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ConditionOrderID]
	, [SecSymbol]
	, [Side]
	, [Price]
	, [Volume]
	, [MatchedVolume]
	, [SubCustAccountID]
	, [MainCustAccountID]
	, [Market]
	, [EffDate]
	, [ExpDate]
	, [TypeOfCond]
	, [MaxValue]
	, [MinValue]
	, [Status]
	, [TradeTime]
	, [DoneTime]
	, [RejectReason]
    FROM
	[dbo].[ConditionOrder]
    WHERE 
	 ([ConditionOrderID] = @ConditionOrderId AND @ConditionOrderId is not null)
	OR ([SecSymbol] = @SecSymbol AND @SecSymbol is not null)
	OR ([Side] = @Side AND @Side is not null)
	OR ([Price] = @Price AND @Price is not null)
	OR ([Volume] = @Volume AND @Volume is not null)
	OR ([MatchedVolume] = @MatchedVolume AND @MatchedVolume is not null)
	OR ([SubCustAccountID] = @SubCustAccountId AND @SubCustAccountId is not null)
	OR ([MainCustAccountID] = @MainCustAccountId AND @MainCustAccountId is not null)
	OR ([Market] = @Market AND @Market is not null)
	OR ([EffDate] = @EffDate AND @EffDate is not null)
	OR ([ExpDate] = @ExpDate AND @ExpDate is not null)
	OR ([TypeOfCond] = @TypeOfCond AND @TypeOfCond is not null)
	OR ([MaxValue] = @MaxValue AND @MaxValue is not null)
	OR ([MinValue] = @MinValue AND @MinValue is not null)
	OR ([Status] = @Status AND @Status is not null)
	OR ([TradeTime] = @TradeTime AND @TradeTime is not null)
	OR ([DoneTime] = @DoneTime AND @DoneTime is not null)
	OR ([RejectReason] = @RejectReason AND @RejectReason is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.QuickOrder_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.QuickOrder_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.QuickOrder_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the QuickOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.QuickOrder_Get_List

AS


				
				SELECT
					[QuickOrderID],
					[SecSymbol],
					[Side],
					[Volume],
					[SubCustAccountID],
					[Market],
					[TradeTime],
					[TypeOfQuick],
					[Status]
				FROM
					[dbo].[QuickOrder]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.QuickOrder_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.QuickOrder_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.QuickOrder_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the QuickOrder table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.QuickOrder_GetPaged
(

	@WhereClause nvarchar (2000)  ,

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
					SET @OrderBy = '[QuickOrderID]'
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
				SET @SQL = @SQL + ', [QuickOrderID]'
				SET @SQL = @SQL + ', [SecSymbol]'
				SET @SQL = @SQL + ', [Side]'
				SET @SQL = @SQL + ', [Volume]'
				SET @SQL = @SQL + ', [SubCustAccountID]'
				SET @SQL = @SQL + ', [Market]'
				SET @SQL = @SQL + ', [TradeTime]'
				SET @SQL = @SQL + ', [TypeOfQuick]'
				SET @SQL = @SQL + ', [Status]'
				SET @SQL = @SQL + ' FROM [dbo].[QuickOrder]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [QuickOrderID],'
				SET @SQL = @SQL + ' [SecSymbol],'
				SET @SQL = @SQL + ' [Side],'
				SET @SQL = @SQL + ' [Volume],'
				SET @SQL = @SQL + ' [SubCustAccountID],'
				SET @SQL = @SQL + ' [Market],'
				SET @SQL = @SQL + ' [TradeTime],'
				SET @SQL = @SQL + ' [TypeOfQuick],'
				SET @SQL = @SQL + ' [Status]'
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
				SET @SQL = @SQL + ' FROM [dbo].[QuickOrder]'
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

	

-- Drop the dbo.QuickOrder_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.QuickOrder_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.QuickOrder_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the QuickOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.QuickOrder_Insert
(

	@QuickOrderId int    OUTPUT,

	@SecSymbol varchar (8)  ,

	@Side varchar (1)  ,

	@Volume int   ,

	@SubCustAccountId varchar (20)  ,

	@Market varchar (1)  ,

	@TradeTime datetime   ,

	@TypeOfQuick smallint   ,

	@Status varchar (1)  
)
AS


				
				INSERT INTO [dbo].[QuickOrder]
					(
					[SecSymbol]
					,[Side]
					,[Volume]
					,[SubCustAccountID]
					,[Market]
					,[TradeTime]
					,[TypeOfQuick]
					,[Status]
					)
				VALUES
					(
					@SecSymbol
					,@Side
					,@Volume
					,@SubCustAccountId
					,@Market
					,@TradeTime
					,@TypeOfQuick
					,@Status
					)
				
				-- Get the identity value
				SET @QuickOrderId = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.QuickOrder_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.QuickOrder_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.QuickOrder_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the QuickOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.QuickOrder_Update
(

	@QuickOrderId int   ,

	@SecSymbol varchar (8)  ,

	@Side varchar (1)  ,

	@Volume int   ,

	@SubCustAccountId varchar (20)  ,

	@Market varchar (1)  ,

	@TradeTime datetime   ,

	@TypeOfQuick smallint   ,

	@Status varchar (1)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[QuickOrder]
				SET
					[SecSymbol] = @SecSymbol
					,[Side] = @Side
					,[Volume] = @Volume
					,[SubCustAccountID] = @SubCustAccountId
					,[Market] = @Market
					,[TradeTime] = @TradeTime
					,[TypeOfQuick] = @TypeOfQuick
					,[Status] = @Status
				WHERE
[QuickOrderID] = @QuickOrderId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.QuickOrder_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.QuickOrder_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.QuickOrder_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the QuickOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.QuickOrder_Delete
(

	@QuickOrderId int   
)
AS


				DELETE FROM [dbo].[QuickOrder] WITH (ROWLOCK) 
				WHERE
					[QuickOrderID] = @QuickOrderId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.QuickOrder_GetByQuickOrderId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.QuickOrder_GetByQuickOrderId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.QuickOrder_GetByQuickOrderId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the QuickOrder table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.QuickOrder_GetByQuickOrderId
(

	@QuickOrderId int   
)
AS


				SELECT
					[QuickOrderID],
					[SecSymbol],
					[Side],
					[Volume],
					[SubCustAccountID],
					[Market],
					[TradeTime],
					[TypeOfQuick],
					[Status]
				FROM
					[dbo].[QuickOrder]
				WHERE
					[QuickOrderID] = @QuickOrderId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.QuickOrder_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.QuickOrder_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.QuickOrder_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the QuickOrder table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.QuickOrder_Find
(

	@SearchUsingOR bit   = null ,

	@QuickOrderId int   = null ,

	@SecSymbol varchar (8)  = null ,

	@Side varchar (1)  = null ,

	@Volume int   = null ,

	@SubCustAccountId varchar (20)  = null ,

	@Market varchar (1)  = null ,

	@TradeTime datetime   = null ,

	@TypeOfQuick smallint   = null ,

	@Status varchar (1)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [QuickOrderID]
	, [SecSymbol]
	, [Side]
	, [Volume]
	, [SubCustAccountID]
	, [Market]
	, [TradeTime]
	, [TypeOfQuick]
	, [Status]
    FROM
	[dbo].[QuickOrder]
    WHERE 
	 ([QuickOrderID] = @QuickOrderId OR @QuickOrderId IS NULL)
	AND ([SecSymbol] = @SecSymbol OR @SecSymbol IS NULL)
	AND ([Side] = @Side OR @Side IS NULL)
	AND ([Volume] = @Volume OR @Volume IS NULL)
	AND ([SubCustAccountID] = @SubCustAccountId OR @SubCustAccountId IS NULL)
	AND ([Market] = @Market OR @Market IS NULL)
	AND ([TradeTime] = @TradeTime OR @TradeTime IS NULL)
	AND ([TypeOfQuick] = @TypeOfQuick OR @TypeOfQuick IS NULL)
	AND ([Status] = @Status OR @Status IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [QuickOrderID]
	, [SecSymbol]
	, [Side]
	, [Volume]
	, [SubCustAccountID]
	, [Market]
	, [TradeTime]
	, [TypeOfQuick]
	, [Status]
    FROM
	[dbo].[QuickOrder]
    WHERE 
	 ([QuickOrderID] = @QuickOrderId AND @QuickOrderId is not null)
	OR ([SecSymbol] = @SecSymbol AND @SecSymbol is not null)
	OR ([Side] = @Side AND @Side is not null)
	OR ([Volume] = @Volume AND @Volume is not null)
	OR ([SubCustAccountID] = @SubCustAccountId AND @SubCustAccountId is not null)
	OR ([Market] = @Market AND @Market is not null)
	OR ([TradeTime] = @TradeTime AND @TradeTime is not null)
	OR ([TypeOfQuick] = @TypeOfQuick AND @TypeOfQuick is not null)
	OR ([Status] = @Status AND @Status is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ExecOrder_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ExecOrder_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ExecOrder_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the ExecOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ExecOrder_Get_List

AS


				
				SELECT
					[OrderID],
					[RefOrderID],
					[MessageType],
					[FISOrderID],
					[SecSymbol],
					[Side],
					[Price],
					[AvgPrice],
					[ConPrice],
					[Volume],
					[ExecutedVol],
					[ExecutedPrice],
					[CancelVolume],
					[CancelledVolume],
					[SubCustAccountID],
					[ExecTransType],
					[TradeTime],
					[MatchedTime],
					[CancelledTime],
					[OrderStatus],
					[OrdRejReason],
					[ConfirmNo],
					[CancelledConfirmNo],
					[SourceID],
					[ExecType],
					[CancelledExecType],
					[PortOrClient],
					[Market],
					[MarketStatus],
					[OrderSource],
					[IsNewOrder],
					[Sequence],
					[NumOfMatch],
					[QuickOrderID],
					[ConditionOrderID]
				FROM
					[dbo].[ExecOrder]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ExecOrder_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ExecOrder_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ExecOrder_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the ExecOrder table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ExecOrder_GetPaged
(

	@WhereClause nvarchar (2000)  ,

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
					SET @OrderBy = '[OrderID]'
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
				SET @SQL = @SQL + ', [OrderID]'
				SET @SQL = @SQL + ', [RefOrderID]'
				SET @SQL = @SQL + ', [MessageType]'
				SET @SQL = @SQL + ', [FISOrderID]'
				SET @SQL = @SQL + ', [SecSymbol]'
				SET @SQL = @SQL + ', [Side]'
				SET @SQL = @SQL + ', [Price]'
				SET @SQL = @SQL + ', [AvgPrice]'
				SET @SQL = @SQL + ', [ConPrice]'
				SET @SQL = @SQL + ', [Volume]'
				SET @SQL = @SQL + ', [ExecutedVol]'
				SET @SQL = @SQL + ', [ExecutedPrice]'
				SET @SQL = @SQL + ', [CancelVolume]'
				SET @SQL = @SQL + ', [CancelledVolume]'
				SET @SQL = @SQL + ', [SubCustAccountID]'
				SET @SQL = @SQL + ', [ExecTransType]'
				SET @SQL = @SQL + ', [TradeTime]'
				SET @SQL = @SQL + ', [MatchedTime]'
				SET @SQL = @SQL + ', [CancelledTime]'
				SET @SQL = @SQL + ', [OrderStatus]'
				SET @SQL = @SQL + ', [OrdRejReason]'
				SET @SQL = @SQL + ', [ConfirmNo]'
				SET @SQL = @SQL + ', [CancelledConfirmNo]'
				SET @SQL = @SQL + ', [SourceID]'
				SET @SQL = @SQL + ', [ExecType]'
				SET @SQL = @SQL + ', [CancelledExecType]'
				SET @SQL = @SQL + ', [PortOrClient]'
				SET @SQL = @SQL + ', [Market]'
				SET @SQL = @SQL + ', [MarketStatus]'
				SET @SQL = @SQL + ', [OrderSource]'
				SET @SQL = @SQL + ', [IsNewOrder]'
				SET @SQL = @SQL + ', [Sequence]'
				SET @SQL = @SQL + ', [NumOfMatch]'
				SET @SQL = @SQL + ', [QuickOrderID]'
				SET @SQL = @SQL + ', [ConditionOrderID]'
				SET @SQL = @SQL + ' FROM [dbo].[ExecOrder]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [OrderID],'
				SET @SQL = @SQL + ' [RefOrderID],'
				SET @SQL = @SQL + ' [MessageType],'
				SET @SQL = @SQL + ' [FISOrderID],'
				SET @SQL = @SQL + ' [SecSymbol],'
				SET @SQL = @SQL + ' [Side],'
				SET @SQL = @SQL + ' [Price],'
				SET @SQL = @SQL + ' [AvgPrice],'
				SET @SQL = @SQL + ' [ConPrice],'
				SET @SQL = @SQL + ' [Volume],'
				SET @SQL = @SQL + ' [ExecutedVol],'
				SET @SQL = @SQL + ' [ExecutedPrice],'
				SET @SQL = @SQL + ' [CancelVolume],'
				SET @SQL = @SQL + ' [CancelledVolume],'
				SET @SQL = @SQL + ' [SubCustAccountID],'
				SET @SQL = @SQL + ' [ExecTransType],'
				SET @SQL = @SQL + ' [TradeTime],'
				SET @SQL = @SQL + ' [MatchedTime],'
				SET @SQL = @SQL + ' [CancelledTime],'
				SET @SQL = @SQL + ' [OrderStatus],'
				SET @SQL = @SQL + ' [OrdRejReason],'
				SET @SQL = @SQL + ' [ConfirmNo],'
				SET @SQL = @SQL + ' [CancelledConfirmNo],'
				SET @SQL = @SQL + ' [SourceID],'
				SET @SQL = @SQL + ' [ExecType],'
				SET @SQL = @SQL + ' [CancelledExecType],'
				SET @SQL = @SQL + ' [PortOrClient],'
				SET @SQL = @SQL + ' [Market],'
				SET @SQL = @SQL + ' [MarketStatus],'
				SET @SQL = @SQL + ' [OrderSource],'
				SET @SQL = @SQL + ' [IsNewOrder],'
				SET @SQL = @SQL + ' [Sequence],'
				SET @SQL = @SQL + ' [NumOfMatch],'
				SET @SQL = @SQL + ' [QuickOrderID],'
				SET @SQL = @SQL + ' [ConditionOrderID]'
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
				SET @SQL = @SQL + ' FROM [dbo].[ExecOrder]'
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

	

-- Drop the dbo.ExecOrder_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ExecOrder_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ExecOrder_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the ExecOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ExecOrder_Insert
(

	@OrderId int    OUTPUT,

	@RefOrderId varchar (64)  ,

	@MessageType varchar (2)  ,

	@FisOrderId int   ,

	@SecSymbol varchar (8)  ,

	@Side varchar (1)  ,

	@Price decimal (10, 3)  ,

	@AvgPrice decimal (10, 3)  ,

	@ConPrice varchar (1)  ,

	@Volume int   ,

	@ExecutedVol int   ,

	@ExecutedPrice decimal (10, 3)  ,

	@CancelVolume int   ,

	@CancelledVolume int   ,

	@SubCustAccountId varchar (20)  ,

	@ExecTransType int   ,

	@TradeTime datetime   ,

	@MatchedTime datetime   ,

	@CancelledTime datetime   ,

	@OrderStatus smallint   ,

	@OrdRejReason int   ,

	@ConfirmNo varchar (6)  ,

	@CancelledConfirmNo varchar (6)  ,

	@SourceId smallint   ,

	@ExecType varchar (1)  ,

	@CancelledExecType varchar (1)  ,

	@PortOrClient varchar (1)  ,

	@Market varchar (1)  ,

	@MarketStatus varchar (1)  ,

	@OrderSource varchar (1)  ,

	@IsNewOrder bit   ,

	@Sequence int   ,

	@NumOfMatch int   ,

	@QuickOrderId int   ,

	@ConditionOrderId bigint   
)
AS


				
				INSERT INTO [dbo].[ExecOrder]
					(
					[RefOrderID]
					,[MessageType]
					,[FISOrderID]
					,[SecSymbol]
					,[Side]
					,[Price]
					,[AvgPrice]
					,[ConPrice]
					,[Volume]
					,[ExecutedVol]
					,[ExecutedPrice]
					,[CancelVolume]
					,[CancelledVolume]
					,[SubCustAccountID]
					,[ExecTransType]
					,[TradeTime]
					,[MatchedTime]
					,[CancelledTime]
					,[OrderStatus]
					,[OrdRejReason]
					,[ConfirmNo]
					,[CancelledConfirmNo]
					,[SourceID]
					,[ExecType]
					,[CancelledExecType]
					,[PortOrClient]
					,[Market]
					,[MarketStatus]
					,[OrderSource]
					,[IsNewOrder]
					,[Sequence]
					,[NumOfMatch]
					,[QuickOrderID]
					,[ConditionOrderID]
					)
				VALUES
					(
					@RefOrderId
					,@MessageType
					,@FisOrderId
					,@SecSymbol
					,@Side
					,@Price
					,@AvgPrice
					,@ConPrice
					,@Volume
					,@ExecutedVol
					,@ExecutedPrice
					,@CancelVolume
					,@CancelledVolume
					,@SubCustAccountId
					,@ExecTransType
					,@TradeTime
					,@MatchedTime
					,@CancelledTime
					,@OrderStatus
					,@OrdRejReason
					,@ConfirmNo
					,@CancelledConfirmNo
					,@SourceId
					,@ExecType
					,@CancelledExecType
					,@PortOrClient
					,@Market
					,@MarketStatus
					,@OrderSource
					,@IsNewOrder
					,@Sequence
					,@NumOfMatch
					,@QuickOrderId
					,@ConditionOrderId
					)
				
				-- Get the identity value
				SET @OrderId = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ExecOrder_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ExecOrder_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ExecOrder_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the ExecOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ExecOrder_Update
(

	@OrderId int   ,

	@RefOrderId varchar (64)  ,

	@MessageType varchar (2)  ,

	@FisOrderId int   ,

	@SecSymbol varchar (8)  ,

	@Side varchar (1)  ,

	@Price decimal (10, 3)  ,

	@AvgPrice decimal (10, 3)  ,

	@ConPrice varchar (1)  ,

	@Volume int   ,

	@ExecutedVol int   ,

	@ExecutedPrice decimal (10, 3)  ,

	@CancelVolume int   ,

	@CancelledVolume int   ,

	@SubCustAccountId varchar (20)  ,

	@ExecTransType int   ,

	@TradeTime datetime   ,

	@MatchedTime datetime   ,

	@CancelledTime datetime   ,

	@OrderStatus smallint   ,

	@OrdRejReason int   ,

	@ConfirmNo varchar (6)  ,

	@CancelledConfirmNo varchar (6)  ,

	@SourceId smallint   ,

	@ExecType varchar (1)  ,

	@CancelledExecType varchar (1)  ,

	@PortOrClient varchar (1)  ,

	@Market varchar (1)  ,

	@MarketStatus varchar (1)  ,

	@OrderSource varchar (1)  ,

	@IsNewOrder bit   ,

	@Sequence int   ,

	@NumOfMatch int   ,

	@QuickOrderId int   ,

	@ConditionOrderId bigint   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[ExecOrder]
				SET
					[RefOrderID] = @RefOrderId
					,[MessageType] = @MessageType
					,[FISOrderID] = @FisOrderId
					,[SecSymbol] = @SecSymbol
					,[Side] = @Side
					,[Price] = @Price
					,[AvgPrice] = @AvgPrice
					,[ConPrice] = @ConPrice
					,[Volume] = @Volume
					,[ExecutedVol] = @ExecutedVol
					,[ExecutedPrice] = @ExecutedPrice
					,[CancelVolume] = @CancelVolume
					,[CancelledVolume] = @CancelledVolume
					,[SubCustAccountID] = @SubCustAccountId
					,[ExecTransType] = @ExecTransType
					,[TradeTime] = @TradeTime
					,[MatchedTime] = @MatchedTime
					,[CancelledTime] = @CancelledTime
					,[OrderStatus] = @OrderStatus
					,[OrdRejReason] = @OrdRejReason
					,[ConfirmNo] = @ConfirmNo
					,[CancelledConfirmNo] = @CancelledConfirmNo
					,[SourceID] = @SourceId
					,[ExecType] = @ExecType
					,[CancelledExecType] = @CancelledExecType
					,[PortOrClient] = @PortOrClient
					,[Market] = @Market
					,[MarketStatus] = @MarketStatus
					,[OrderSource] = @OrderSource
					,[IsNewOrder] = @IsNewOrder
					,[Sequence] = @Sequence
					,[NumOfMatch] = @NumOfMatch
					,[QuickOrderID] = @QuickOrderId
					,[ConditionOrderID] = @ConditionOrderId
				WHERE
[OrderID] = @OrderId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ExecOrder_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ExecOrder_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ExecOrder_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the ExecOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ExecOrder_Delete
(

	@OrderId int   
)
AS


				DELETE FROM [dbo].[ExecOrder] WITH (ROWLOCK) 
				WHERE
					[OrderID] = @OrderId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ExecOrder_GetByConditionOrderId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ExecOrder_GetByConditionOrderId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ExecOrder_GetByConditionOrderId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the ExecOrder table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ExecOrder_GetByConditionOrderId
(

	@ConditionOrderId bigint   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[OrderID],
					[RefOrderID],
					[MessageType],
					[FISOrderID],
					[SecSymbol],
					[Side],
					[Price],
					[AvgPrice],
					[ConPrice],
					[Volume],
					[ExecutedVol],
					[ExecutedPrice],
					[CancelVolume],
					[CancelledVolume],
					[SubCustAccountID],
					[ExecTransType],
					[TradeTime],
					[MatchedTime],
					[CancelledTime],
					[OrderStatus],
					[OrdRejReason],
					[ConfirmNo],
					[CancelledConfirmNo],
					[SourceID],
					[ExecType],
					[CancelledExecType],
					[PortOrClient],
					[Market],
					[MarketStatus],
					[OrderSource],
					[IsNewOrder],
					[Sequence],
					[NumOfMatch],
					[QuickOrderID],
					[ConditionOrderID]
				FROM
					[dbo].[ExecOrder]
				WHERE
					[ConditionOrderID] = @ConditionOrderId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ExecOrder_GetByQuickOrderId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ExecOrder_GetByQuickOrderId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ExecOrder_GetByQuickOrderId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the ExecOrder table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ExecOrder_GetByQuickOrderId
(

	@QuickOrderId int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[OrderID],
					[RefOrderID],
					[MessageType],
					[FISOrderID],
					[SecSymbol],
					[Side],
					[Price],
					[AvgPrice],
					[ConPrice],
					[Volume],
					[ExecutedVol],
					[ExecutedPrice],
					[CancelVolume],
					[CancelledVolume],
					[SubCustAccountID],
					[ExecTransType],
					[TradeTime],
					[MatchedTime],
					[CancelledTime],
					[OrderStatus],
					[OrdRejReason],
					[ConfirmNo],
					[CancelledConfirmNo],
					[SourceID],
					[ExecType],
					[CancelledExecType],
					[PortOrClient],
					[Market],
					[MarketStatus],
					[OrderSource],
					[IsNewOrder],
					[Sequence],
					[NumOfMatch],
					[QuickOrderID],
					[ConditionOrderID]
				FROM
					[dbo].[ExecOrder]
				WHERE
					[QuickOrderID] = @QuickOrderId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ExecOrder_GetByOrderId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ExecOrder_GetByOrderId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ExecOrder_GetByOrderId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the ExecOrder table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ExecOrder_GetByOrderId
(

	@OrderId int   
)
AS


				SELECT
					[OrderID],
					[RefOrderID],
					[MessageType],
					[FISOrderID],
					[SecSymbol],
					[Side],
					[Price],
					[AvgPrice],
					[ConPrice],
					[Volume],
					[ExecutedVol],
					[ExecutedPrice],
					[CancelVolume],
					[CancelledVolume],
					[SubCustAccountID],
					[ExecTransType],
					[TradeTime],
					[MatchedTime],
					[CancelledTime],
					[OrderStatus],
					[OrdRejReason],
					[ConfirmNo],
					[CancelledConfirmNo],
					[SourceID],
					[ExecType],
					[CancelledExecType],
					[PortOrClient],
					[Market],
					[MarketStatus],
					[OrderSource],
					[IsNewOrder],
					[Sequence],
					[NumOfMatch],
					[QuickOrderID],
					[ConditionOrderID]
				FROM
					[dbo].[ExecOrder]
				WHERE
					[OrderID] = @OrderId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ExecOrder_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ExecOrder_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ExecOrder_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the ExecOrder table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ExecOrder_Find
(

	@SearchUsingOR bit   = null ,

	@OrderId int   = null ,

	@RefOrderId varchar (64)  = null ,

	@MessageType varchar (2)  = null ,

	@FisOrderId int   = null ,

	@SecSymbol varchar (8)  = null ,

	@Side varchar (1)  = null ,

	@Price decimal (10, 3)  = null ,

	@AvgPrice decimal (10, 3)  = null ,

	@ConPrice varchar (1)  = null ,

	@Volume int   = null ,

	@ExecutedVol int   = null ,

	@ExecutedPrice decimal (10, 3)  = null ,

	@CancelVolume int   = null ,

	@CancelledVolume int   = null ,

	@SubCustAccountId varchar (20)  = null ,

	@ExecTransType int   = null ,

	@TradeTime datetime   = null ,

	@MatchedTime datetime   = null ,

	@CancelledTime datetime   = null ,

	@OrderStatus smallint   = null ,

	@OrdRejReason int   = null ,

	@ConfirmNo varchar (6)  = null ,

	@CancelledConfirmNo varchar (6)  = null ,

	@SourceId smallint   = null ,

	@ExecType varchar (1)  = null ,

	@CancelledExecType varchar (1)  = null ,

	@PortOrClient varchar (1)  = null ,

	@Market varchar (1)  = null ,

	@MarketStatus varchar (1)  = null ,

	@OrderSource varchar (1)  = null ,

	@IsNewOrder bit   = null ,

	@Sequence int   = null ,

	@NumOfMatch int   = null ,

	@QuickOrderId int   = null ,

	@ConditionOrderId bigint   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [OrderID]
	, [RefOrderID]
	, [MessageType]
	, [FISOrderID]
	, [SecSymbol]
	, [Side]
	, [Price]
	, [AvgPrice]
	, [ConPrice]
	, [Volume]
	, [ExecutedVol]
	, [ExecutedPrice]
	, [CancelVolume]
	, [CancelledVolume]
	, [SubCustAccountID]
	, [ExecTransType]
	, [TradeTime]
	, [MatchedTime]
	, [CancelledTime]
	, [OrderStatus]
	, [OrdRejReason]
	, [ConfirmNo]
	, [CancelledConfirmNo]
	, [SourceID]
	, [ExecType]
	, [CancelledExecType]
	, [PortOrClient]
	, [Market]
	, [MarketStatus]
	, [OrderSource]
	, [IsNewOrder]
	, [Sequence]
	, [NumOfMatch]
	, [QuickOrderID]
	, [ConditionOrderID]
    FROM
	[dbo].[ExecOrder]
    WHERE 
	 ([OrderID] = @OrderId OR @OrderId IS NULL)
	AND ([RefOrderID] = @RefOrderId OR @RefOrderId IS NULL)
	AND ([MessageType] = @MessageType OR @MessageType IS NULL)
	AND ([FISOrderID] = @FisOrderId OR @FisOrderId IS NULL)
	AND ([SecSymbol] = @SecSymbol OR @SecSymbol IS NULL)
	AND ([Side] = @Side OR @Side IS NULL)
	AND ([Price] = @Price OR @Price IS NULL)
	AND ([AvgPrice] = @AvgPrice OR @AvgPrice IS NULL)
	AND ([ConPrice] = @ConPrice OR @ConPrice IS NULL)
	AND ([Volume] = @Volume OR @Volume IS NULL)
	AND ([ExecutedVol] = @ExecutedVol OR @ExecutedVol IS NULL)
	AND ([ExecutedPrice] = @ExecutedPrice OR @ExecutedPrice IS NULL)
	AND ([CancelVolume] = @CancelVolume OR @CancelVolume IS NULL)
	AND ([CancelledVolume] = @CancelledVolume OR @CancelledVolume IS NULL)
	AND ([SubCustAccountID] = @SubCustAccountId OR @SubCustAccountId IS NULL)
	AND ([ExecTransType] = @ExecTransType OR @ExecTransType IS NULL)
	AND ([TradeTime] = @TradeTime OR @TradeTime IS NULL)
	AND ([MatchedTime] = @MatchedTime OR @MatchedTime IS NULL)
	AND ([CancelledTime] = @CancelledTime OR @CancelledTime IS NULL)
	AND ([OrderStatus] = @OrderStatus OR @OrderStatus IS NULL)
	AND ([OrdRejReason] = @OrdRejReason OR @OrdRejReason IS NULL)
	AND ([ConfirmNo] = @ConfirmNo OR @ConfirmNo IS NULL)
	AND ([CancelledConfirmNo] = @CancelledConfirmNo OR @CancelledConfirmNo IS NULL)
	AND ([SourceID] = @SourceId OR @SourceId IS NULL)
	AND ([ExecType] = @ExecType OR @ExecType IS NULL)
	AND ([CancelledExecType] = @CancelledExecType OR @CancelledExecType IS NULL)
	AND ([PortOrClient] = @PortOrClient OR @PortOrClient IS NULL)
	AND ([Market] = @Market OR @Market IS NULL)
	AND ([MarketStatus] = @MarketStatus OR @MarketStatus IS NULL)
	AND ([OrderSource] = @OrderSource OR @OrderSource IS NULL)
	AND ([IsNewOrder] = @IsNewOrder OR @IsNewOrder IS NULL)
	AND ([Sequence] = @Sequence OR @Sequence IS NULL)
	AND ([NumOfMatch] = @NumOfMatch OR @NumOfMatch IS NULL)
	AND ([QuickOrderID] = @QuickOrderId OR @QuickOrderId IS NULL)
	AND ([ConditionOrderID] = @ConditionOrderId OR @ConditionOrderId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [OrderID]
	, [RefOrderID]
	, [MessageType]
	, [FISOrderID]
	, [SecSymbol]
	, [Side]
	, [Price]
	, [AvgPrice]
	, [ConPrice]
	, [Volume]
	, [ExecutedVol]
	, [ExecutedPrice]
	, [CancelVolume]
	, [CancelledVolume]
	, [SubCustAccountID]
	, [ExecTransType]
	, [TradeTime]
	, [MatchedTime]
	, [CancelledTime]
	, [OrderStatus]
	, [OrdRejReason]
	, [ConfirmNo]
	, [CancelledConfirmNo]
	, [SourceID]
	, [ExecType]
	, [CancelledExecType]
	, [PortOrClient]
	, [Market]
	, [MarketStatus]
	, [OrderSource]
	, [IsNewOrder]
	, [Sequence]
	, [NumOfMatch]
	, [QuickOrderID]
	, [ConditionOrderID]
    FROM
	[dbo].[ExecOrder]
    WHERE 
	 ([OrderID] = @OrderId AND @OrderId is not null)
	OR ([RefOrderID] = @RefOrderId AND @RefOrderId is not null)
	OR ([MessageType] = @MessageType AND @MessageType is not null)
	OR ([FISOrderID] = @FisOrderId AND @FisOrderId is not null)
	OR ([SecSymbol] = @SecSymbol AND @SecSymbol is not null)
	OR ([Side] = @Side AND @Side is not null)
	OR ([Price] = @Price AND @Price is not null)
	OR ([AvgPrice] = @AvgPrice AND @AvgPrice is not null)
	OR ([ConPrice] = @ConPrice AND @ConPrice is not null)
	OR ([Volume] = @Volume AND @Volume is not null)
	OR ([ExecutedVol] = @ExecutedVol AND @ExecutedVol is not null)
	OR ([ExecutedPrice] = @ExecutedPrice AND @ExecutedPrice is not null)
	OR ([CancelVolume] = @CancelVolume AND @CancelVolume is not null)
	OR ([CancelledVolume] = @CancelledVolume AND @CancelledVolume is not null)
	OR ([SubCustAccountID] = @SubCustAccountId AND @SubCustAccountId is not null)
	OR ([ExecTransType] = @ExecTransType AND @ExecTransType is not null)
	OR ([TradeTime] = @TradeTime AND @TradeTime is not null)
	OR ([MatchedTime] = @MatchedTime AND @MatchedTime is not null)
	OR ([CancelledTime] = @CancelledTime AND @CancelledTime is not null)
	OR ([OrderStatus] = @OrderStatus AND @OrderStatus is not null)
	OR ([OrdRejReason] = @OrdRejReason AND @OrdRejReason is not null)
	OR ([ConfirmNo] = @ConfirmNo AND @ConfirmNo is not null)
	OR ([CancelledConfirmNo] = @CancelledConfirmNo AND @CancelledConfirmNo is not null)
	OR ([SourceID] = @SourceId AND @SourceId is not null)
	OR ([ExecType] = @ExecType AND @ExecType is not null)
	OR ([CancelledExecType] = @CancelledExecType AND @CancelledExecType is not null)
	OR ([PortOrClient] = @PortOrClient AND @PortOrClient is not null)
	OR ([Market] = @Market AND @Market is not null)
	OR ([MarketStatus] = @MarketStatus AND @MarketStatus is not null)
	OR ([OrderSource] = @OrderSource AND @OrderSource is not null)
	OR ([IsNewOrder] = @IsNewOrder AND @IsNewOrder is not null)
	OR ([Sequence] = @Sequence AND @Sequence is not null)
	OR ([NumOfMatch] = @NumOfMatch AND @NumOfMatch is not null)
	OR ([QuickOrderID] = @QuickOrderId AND @QuickOrderId is not null)
	OR ([ConditionOrderID] = @ConditionOrderId AND @ConditionOrderId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrderDetail_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrderDetail_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrderDetail_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the ConditionOrderDetail table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrderDetail_Get_List

AS


				
				SELECT
					[DetailId],
					[Volume],
					[MatchedVolume],
					[AvgPrice],
					[OrderStatus],
					[ConditionOrderID],
					[FISOrderID],
					[OrdRejReason],
					[NumOfMatch],
					[CancelledVol],
					[CreatedDateTime],
					[UpdatedDateTime]
				FROM
					[dbo].[ConditionOrderDetail]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrderDetail_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrderDetail_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrderDetail_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the ConditionOrderDetail table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrderDetail_GetPaged
(

	@WhereClause nvarchar (2000)  ,

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
					SET @OrderBy = '[DetailId]'
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
				SET @SQL = @SQL + ', [DetailId]'
				SET @SQL = @SQL + ', [Volume]'
				SET @SQL = @SQL + ', [MatchedVolume]'
				SET @SQL = @SQL + ', [AvgPrice]'
				SET @SQL = @SQL + ', [OrderStatus]'
				SET @SQL = @SQL + ', [ConditionOrderID]'
				SET @SQL = @SQL + ', [FISOrderID]'
				SET @SQL = @SQL + ', [OrdRejReason]'
				SET @SQL = @SQL + ', [NumOfMatch]'
				SET @SQL = @SQL + ', [CancelledVol]'
				SET @SQL = @SQL + ', [CreatedDateTime]'
				SET @SQL = @SQL + ', [UpdatedDateTime]'
				SET @SQL = @SQL + ' FROM [dbo].[ConditionOrderDetail]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [DetailId],'
				SET @SQL = @SQL + ' [Volume],'
				SET @SQL = @SQL + ' [MatchedVolume],'
				SET @SQL = @SQL + ' [AvgPrice],'
				SET @SQL = @SQL + ' [OrderStatus],'
				SET @SQL = @SQL + ' [ConditionOrderID],'
				SET @SQL = @SQL + ' [FISOrderID],'
				SET @SQL = @SQL + ' [OrdRejReason],'
				SET @SQL = @SQL + ' [NumOfMatch],'
				SET @SQL = @SQL + ' [CancelledVol],'
				SET @SQL = @SQL + ' [CreatedDateTime],'
				SET @SQL = @SQL + ' [UpdatedDateTime]'
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
				SET @SQL = @SQL + ' FROM [dbo].[ConditionOrderDetail]'
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

	

-- Drop the dbo.ConditionOrderDetail_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrderDetail_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrderDetail_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the ConditionOrderDetail table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrderDetail_Insert
(

	@DetailId bigint    OUTPUT,

	@Volume int   ,

	@MatchedVolume int   ,

	@AvgPrice decimal (10, 3)  ,

	@OrderStatus smallint   ,

	@ConditionOrderId bigint   ,

	@FisOrderId int   ,

	@OrdRejReason int   ,

	@NumOfMatch int   ,

	@CancelledVol int   ,

	@CreatedDateTime datetime   ,

	@UpdatedDateTime datetime   
)
AS


				
				INSERT INTO [dbo].[ConditionOrderDetail]
					(
					[Volume]
					,[MatchedVolume]
					,[AvgPrice]
					,[OrderStatus]
					,[ConditionOrderID]
					,[FISOrderID]
					,[OrdRejReason]
					,[NumOfMatch]
					,[CancelledVol]
					,[CreatedDateTime]
					,[UpdatedDateTime]
					)
				VALUES
					(
					@Volume
					,@MatchedVolume
					,@AvgPrice
					,@OrderStatus
					,@ConditionOrderId
					,@FisOrderId
					,@OrdRejReason
					,@NumOfMatch
					,@CancelledVol
					,@CreatedDateTime
					,@UpdatedDateTime
					)
				
				-- Get the identity value
				SET @DetailId = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrderDetail_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrderDetail_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrderDetail_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the ConditionOrderDetail table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrderDetail_Update
(

	@DetailId bigint   ,

	@Volume int   ,

	@MatchedVolume int   ,

	@AvgPrice decimal (10, 3)  ,

	@OrderStatus smallint   ,

	@ConditionOrderId bigint   ,

	@FisOrderId int   ,

	@OrdRejReason int   ,

	@NumOfMatch int   ,

	@CancelledVol int   ,

	@CreatedDateTime datetime   ,

	@UpdatedDateTime datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[ConditionOrderDetail]
				SET
					[Volume] = @Volume
					,[MatchedVolume] = @MatchedVolume
					,[AvgPrice] = @AvgPrice
					,[OrderStatus] = @OrderStatus
					,[ConditionOrderID] = @ConditionOrderId
					,[FISOrderID] = @FisOrderId
					,[OrdRejReason] = @OrdRejReason
					,[NumOfMatch] = @NumOfMatch
					,[CancelledVol] = @CancelledVol
					,[CreatedDateTime] = @CreatedDateTime
					,[UpdatedDateTime] = @UpdatedDateTime
				WHERE
[DetailId] = @DetailId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrderDetail_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrderDetail_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrderDetail_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the ConditionOrderDetail table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrderDetail_Delete
(

	@DetailId bigint   
)
AS


				DELETE FROM [dbo].[ConditionOrderDetail] WITH (ROWLOCK) 
				WHERE
					[DetailId] = @DetailId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrderDetail_GetByConditionOrderId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrderDetail_GetByConditionOrderId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrderDetail_GetByConditionOrderId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the ConditionOrderDetail table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrderDetail_GetByConditionOrderId
(

	@ConditionOrderId bigint   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[DetailId],
					[Volume],
					[MatchedVolume],
					[AvgPrice],
					[OrderStatus],
					[ConditionOrderID],
					[FISOrderID],
					[OrdRejReason],
					[NumOfMatch],
					[CancelledVol],
					[CreatedDateTime],
					[UpdatedDateTime]
				FROM
					[dbo].[ConditionOrderDetail]
				WHERE
					[ConditionOrderID] = @ConditionOrderId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrderDetail_GetByDetailId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrderDetail_GetByDetailId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrderDetail_GetByDetailId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the ConditionOrderDetail table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrderDetail_GetByDetailId
(

	@DetailId bigint   
)
AS


				SELECT
					[DetailId],
					[Volume],
					[MatchedVolume],
					[AvgPrice],
					[OrderStatus],
					[ConditionOrderID],
					[FISOrderID],
					[OrdRejReason],
					[NumOfMatch],
					[CancelledVol],
					[CreatedDateTime],
					[UpdatedDateTime]
				FROM
					[dbo].[ConditionOrderDetail]
				WHERE
					[DetailId] = @DetailId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ConditionOrderDetail_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ConditionOrderDetail_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ConditionOrderDetail_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Tuesday, January 31, 2012

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the ConditionOrderDetail table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ConditionOrderDetail_Find
(

	@SearchUsingOR bit   = null ,

	@DetailId bigint   = null ,

	@Volume int   = null ,

	@MatchedVolume int   = null ,

	@AvgPrice decimal (10, 3)  = null ,

	@OrderStatus smallint   = null ,

	@ConditionOrderId bigint   = null ,

	@FisOrderId int   = null ,

	@OrdRejReason int   = null ,

	@NumOfMatch int   = null ,

	@CancelledVol int   = null ,

	@CreatedDateTime datetime   = null ,

	@UpdatedDateTime datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [DetailId]
	, [Volume]
	, [MatchedVolume]
	, [AvgPrice]
	, [OrderStatus]
	, [ConditionOrderID]
	, [FISOrderID]
	, [OrdRejReason]
	, [NumOfMatch]
	, [CancelledVol]
	, [CreatedDateTime]
	, [UpdatedDateTime]
    FROM
	[dbo].[ConditionOrderDetail]
    WHERE 
	 ([DetailId] = @DetailId OR @DetailId IS NULL)
	AND ([Volume] = @Volume OR @Volume IS NULL)
	AND ([MatchedVolume] = @MatchedVolume OR @MatchedVolume IS NULL)
	AND ([AvgPrice] = @AvgPrice OR @AvgPrice IS NULL)
	AND ([OrderStatus] = @OrderStatus OR @OrderStatus IS NULL)
	AND ([ConditionOrderID] = @ConditionOrderId OR @ConditionOrderId IS NULL)
	AND ([FISOrderID] = @FisOrderId OR @FisOrderId IS NULL)
	AND ([OrdRejReason] = @OrdRejReason OR @OrdRejReason IS NULL)
	AND ([NumOfMatch] = @NumOfMatch OR @NumOfMatch IS NULL)
	AND ([CancelledVol] = @CancelledVol OR @CancelledVol IS NULL)
	AND ([CreatedDateTime] = @CreatedDateTime OR @CreatedDateTime IS NULL)
	AND ([UpdatedDateTime] = @UpdatedDateTime OR @UpdatedDateTime IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [DetailId]
	, [Volume]
	, [MatchedVolume]
	, [AvgPrice]
	, [OrderStatus]
	, [ConditionOrderID]
	, [FISOrderID]
	, [OrdRejReason]
	, [NumOfMatch]
	, [CancelledVol]
	, [CreatedDateTime]
	, [UpdatedDateTime]
    FROM
	[dbo].[ConditionOrderDetail]
    WHERE 
	 ([DetailId] = @DetailId AND @DetailId is not null)
	OR ([Volume] = @Volume AND @Volume is not null)
	OR ([MatchedVolume] = @MatchedVolume AND @MatchedVolume is not null)
	OR ([AvgPrice] = @AvgPrice AND @AvgPrice is not null)
	OR ([OrderStatus] = @OrderStatus AND @OrderStatus is not null)
	OR ([ConditionOrderID] = @ConditionOrderId AND @ConditionOrderId is not null)
	OR ([FISOrderID] = @FisOrderId AND @FisOrderId is not null)
	OR ([OrdRejReason] = @OrdRejReason AND @OrdRejReason is not null)
	OR ([NumOfMatch] = @NumOfMatch AND @NumOfMatch is not null)
	OR ([CancelledVol] = @CancelledVol AND @CancelledVol is not null)
	OR ([CreatedDateTime] = @CreatedDateTime AND @CreatedDateTime is not null)
	OR ([UpdatedDateTime] = @UpdatedDateTime AND @UpdatedDateTime is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

