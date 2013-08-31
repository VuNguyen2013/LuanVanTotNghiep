
USE [ETradeFinance]
GO
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.OddLotOrder_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.OddLotOrder_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.OddLotOrder_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the OddLotOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.OddLotOrder_Get_List

AS


				
				SELECT
					[ID],
					[SecSymbol],
					[Side],
					[Price],
					[Volume],
					[SubCustAccountID],
					[Market],
					[ExecPrice],
					[ExecVol],
					[CanceledVol],
					[Status],
					[BrokerID],
					[RequestTime],
					[ExecTime],
					[Note]
				FROM
					[dbo].[OddLotOrder]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.OddLotOrder_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.OddLotOrder_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.OddLotOrder_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the OddLotOrder table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.OddLotOrder_GetPaged
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
				SET @SQL = @SQL + ', [SecSymbol]'
				SET @SQL = @SQL + ', [Side]'
				SET @SQL = @SQL + ', [Price]'
				SET @SQL = @SQL + ', [Volume]'
				SET @SQL = @SQL + ', [SubCustAccountID]'
				SET @SQL = @SQL + ', [Market]'
				SET @SQL = @SQL + ', [ExecPrice]'
				SET @SQL = @SQL + ', [ExecVol]'
				SET @SQL = @SQL + ', [CanceledVol]'
				SET @SQL = @SQL + ', [Status]'
				SET @SQL = @SQL + ', [BrokerID]'
				SET @SQL = @SQL + ', [RequestTime]'
				SET @SQL = @SQL + ', [ExecTime]'
				SET @SQL = @SQL + ', [Note]'
				SET @SQL = @SQL + ' FROM [dbo].[OddLotOrder]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [ID],'
				SET @SQL = @SQL + ' [SecSymbol],'
				SET @SQL = @SQL + ' [Side],'
				SET @SQL = @SQL + ' [Price],'
				SET @SQL = @SQL + ' [Volume],'
				SET @SQL = @SQL + ' [SubCustAccountID],'
				SET @SQL = @SQL + ' [Market],'
				SET @SQL = @SQL + ' [ExecPrice],'
				SET @SQL = @SQL + ' [ExecVol],'
				SET @SQL = @SQL + ' [CanceledVol],'
				SET @SQL = @SQL + ' [Status],'
				SET @SQL = @SQL + ' [BrokerID],'
				SET @SQL = @SQL + ' [RequestTime],'
				SET @SQL = @SQL + ' [ExecTime],'
				SET @SQL = @SQL + ' [Note]'
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
				SET @SQL = @SQL + ' FROM [dbo].[OddLotOrder]'
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

	

-- Drop the dbo.OddLotOrder_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.OddLotOrder_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.OddLotOrder_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the OddLotOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.OddLotOrder_Insert
(

	@Id bigint    OUTPUT,

	@SecSymbol varchar (8)  ,

	@Side varchar (1)  ,

	@Price decimal (10, 3)  ,

	@Volume bigint   ,

	@SubCustAccountId varchar (20)  ,

	@Market varchar (1)  ,

	@ExecPrice decimal (10, 3)  ,

	@ExecVol bigint   ,

	@CanceledVol bigint   ,

	@Status int   ,

	@BrokerId varchar (20)  ,

	@RequestTime datetime   ,

	@ExecTime datetime   ,

	@Note ntext   
)
AS


				
				INSERT INTO [dbo].[OddLotOrder]
					(
					[SecSymbol]
					,[Side]
					,[Price]
					,[Volume]
					,[SubCustAccountID]
					,[Market]
					,[ExecPrice]
					,[ExecVol]
					,[CanceledVol]
					,[Status]
					,[BrokerID]
					,[RequestTime]
					,[ExecTime]
					,[Note]
					)
				VALUES
					(
					@SecSymbol
					,@Side
					,@Price
					,@Volume
					,@SubCustAccountId
					,@Market
					,@ExecPrice
					,@ExecVol
					,@CanceledVol
					,@Status
					,@BrokerId
					,@RequestTime
					,@ExecTime
					,@Note
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

	

-- Drop the dbo.OddLotOrder_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.OddLotOrder_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.OddLotOrder_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the OddLotOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.OddLotOrder_Update
(

	@Id bigint   ,

	@SecSymbol varchar (8)  ,

	@Side varchar (1)  ,

	@Price decimal (10, 3)  ,

	@Volume bigint   ,

	@SubCustAccountId varchar (20)  ,

	@Market varchar (1)  ,

	@ExecPrice decimal (10, 3)  ,

	@ExecVol bigint   ,

	@CanceledVol bigint   ,

	@Status int   ,

	@BrokerId varchar (20)  ,

	@RequestTime datetime   ,

	@ExecTime datetime   ,

	@Note ntext   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[OddLotOrder]
				SET
					[SecSymbol] = @SecSymbol
					,[Side] = @Side
					,[Price] = @Price
					,[Volume] = @Volume
					,[SubCustAccountID] = @SubCustAccountId
					,[Market] = @Market
					,[ExecPrice] = @ExecPrice
					,[ExecVol] = @ExecVol
					,[CanceledVol] = @CanceledVol
					,[Status] = @Status
					,[BrokerID] = @BrokerId
					,[RequestTime] = @RequestTime
					,[ExecTime] = @ExecTime
					,[Note] = @Note
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.OddLotOrder_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.OddLotOrder_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.OddLotOrder_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the OddLotOrder table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.OddLotOrder_Delete
(

	@Id bigint   
)
AS


				DELETE FROM [dbo].[OddLotOrder] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.OddLotOrder_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.OddLotOrder_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.OddLotOrder_GetById
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the OddLotOrder table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.OddLotOrder_GetById
(

	@Id bigint   
)
AS


				SELECT
					[ID],
					[SecSymbol],
					[Side],
					[Price],
					[Volume],
					[SubCustAccountID],
					[Market],
					[ExecPrice],
					[ExecVol],
					[CanceledVol],
					[Status],
					[BrokerID],
					[RequestTime],
					[ExecTime],
					[Note]
				FROM
					[dbo].[OddLotOrder]
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

	

-- Drop the dbo.OddLotOrder_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.OddLotOrder_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.OddLotOrder_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the OddLotOrder table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.OddLotOrder_Find
(

	@SearchUsingOR bit   = null ,

	@Id bigint   = null ,

	@SecSymbol varchar (8)  = null ,

	@Side varchar (1)  = null ,

	@Price decimal (10, 3)  = null ,

	@Volume bigint   = null ,

	@SubCustAccountId varchar (20)  = null ,

	@Market varchar (1)  = null ,

	@ExecPrice decimal (10, 3)  = null ,

	@ExecVol bigint   = null ,

	@CanceledVol bigint   = null ,

	@Status int   = null ,

	@BrokerId varchar (20)  = null ,

	@RequestTime datetime   = null ,

	@ExecTime datetime   = null ,

	@Note ntext   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [SecSymbol]
	, [Side]
	, [Price]
	, [Volume]
	, [SubCustAccountID]
	, [Market]
	, [ExecPrice]
	, [ExecVol]
	, [CanceledVol]
	, [Status]
	, [BrokerID]
	, [RequestTime]
	, [ExecTime]
	, [Note]
    FROM
	[dbo].[OddLotOrder]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([SecSymbol] = @SecSymbol OR @SecSymbol IS NULL)
	AND ([Side] = @Side OR @Side IS NULL)
	AND ([Price] = @Price OR @Price IS NULL)
	AND ([Volume] = @Volume OR @Volume IS NULL)
	AND ([SubCustAccountID] = @SubCustAccountId OR @SubCustAccountId IS NULL)
	AND ([Market] = @Market OR @Market IS NULL)
	AND ([ExecPrice] = @ExecPrice OR @ExecPrice IS NULL)
	AND ([ExecVol] = @ExecVol OR @ExecVol IS NULL)
	AND ([CanceledVol] = @CanceledVol OR @CanceledVol IS NULL)
	AND ([Status] = @Status OR @Status IS NULL)
	AND ([BrokerID] = @BrokerId OR @BrokerId IS NULL)
	AND ([RequestTime] = @RequestTime OR @RequestTime IS NULL)
	AND ([ExecTime] = @ExecTime OR @ExecTime IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [SecSymbol]
	, [Side]
	, [Price]
	, [Volume]
	, [SubCustAccountID]
	, [Market]
	, [ExecPrice]
	, [ExecVol]
	, [CanceledVol]
	, [Status]
	, [BrokerID]
	, [RequestTime]
	, [ExecTime]
	, [Note]
    FROM
	[dbo].[OddLotOrder]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([SecSymbol] = @SecSymbol AND @SecSymbol is not null)
	OR ([Side] = @Side AND @Side is not null)
	OR ([Price] = @Price AND @Price is not null)
	OR ([Volume] = @Volume AND @Volume is not null)
	OR ([SubCustAccountID] = @SubCustAccountId AND @SubCustAccountId is not null)
	OR ([Market] = @Market AND @Market is not null)
	OR ([ExecPrice] = @ExecPrice AND @ExecPrice is not null)
	OR ([ExecVol] = @ExecVol AND @ExecVol is not null)
	OR ([CanceledVol] = @CanceledVol AND @CanceledVol is not null)
	OR ([Status] = @Status AND @Status is not null)
	OR ([BrokerID] = @BrokerId AND @BrokerId is not null)
	OR ([RequestTime] = @RequestTime AND @RequestTime is not null)
	OR ([ExecTime] = @ExecTime AND @ExecTime is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.AdvanceTime_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AdvanceTime_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AdvanceTime_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the AdvanceTime table
-- Table Comment: Thoi gian ung truoc
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AdvanceTime_Get_List

AS


				
				SELECT
					[ID],
					[StartTime],
					[EndTime],
					[AdvanceType]
				FROM
					[dbo].[AdvanceTime]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.AdvanceTime_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AdvanceTime_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AdvanceTime_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the AdvanceTime table passing page index and page count parameters
-- Table Comment: Thoi gian ung truoc
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AdvanceTime_GetPaged
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
				SET @SQL = @SQL + ', [StartTime]'
				SET @SQL = @SQL + ', [EndTime]'
				SET @SQL = @SQL + ', [AdvanceType]'
				SET @SQL = @SQL + ' FROM [dbo].[AdvanceTime]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [ID],'
				SET @SQL = @SQL + ' [StartTime],'
				SET @SQL = @SQL + ' [EndTime],'
				SET @SQL = @SQL + ' [AdvanceType]'
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
				SET @SQL = @SQL + ' FROM [dbo].[AdvanceTime]'
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

	

-- Drop the dbo.AdvanceTime_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AdvanceTime_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AdvanceTime_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the AdvanceTime table
-- Table Comment: Thoi gian ung truoc
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AdvanceTime_Insert
(

	@Id int    OUTPUT,

	@StartTime datetime   ,

	@EndTime datetime   ,

	@AdvanceType int   
)
AS


				
				INSERT INTO [dbo].[AdvanceTime]
					(
					[StartTime]
					,[EndTime]
					,[AdvanceType]
					)
				VALUES
					(
					@StartTime
					,@EndTime
					,@AdvanceType
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

	

-- Drop the dbo.AdvanceTime_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AdvanceTime_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AdvanceTime_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the AdvanceTime table
-- Table Comment: Thoi gian ung truoc
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AdvanceTime_Update
(

	@Id int   ,

	@StartTime datetime   ,

	@EndTime datetime   ,

	@AdvanceType int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[AdvanceTime]
				SET
					[StartTime] = @StartTime
					,[EndTime] = @EndTime
					,[AdvanceType] = @AdvanceType
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.AdvanceTime_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AdvanceTime_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AdvanceTime_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the AdvanceTime table
-- Table Comment: Thoi gian ung truoc
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AdvanceTime_Delete
(

	@Id int   
)
AS


				DELETE FROM [dbo].[AdvanceTime] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.AdvanceTime_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AdvanceTime_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AdvanceTime_GetById
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the AdvanceTime table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AdvanceTime_GetById
(

	@Id int   
)
AS


				SELECT
					[ID],
					[StartTime],
					[EndTime],
					[AdvanceType]
				FROM
					[dbo].[AdvanceTime]
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

	

-- Drop the dbo.AdvanceTime_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AdvanceTime_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AdvanceTime_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the AdvanceTime table passing nullable parameters
-- Table Comment: Thoi gian ung truoc
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AdvanceTime_Find
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@StartTime datetime   = null ,

	@EndTime datetime   = null ,

	@AdvanceType int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [StartTime]
	, [EndTime]
	, [AdvanceType]
    FROM
	[dbo].[AdvanceTime]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([StartTime] = @StartTime OR @StartTime IS NULL)
	AND ([EndTime] = @EndTime OR @EndTime IS NULL)
	AND ([AdvanceType] = @AdvanceType OR @AdvanceType IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [StartTime]
	, [EndTime]
	, [AdvanceType]
    FROM
	[dbo].[AdvanceTime]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([StartTime] = @StartTime AND @StartTime is not null)
	OR ([EndTime] = @EndTime AND @EndTime is not null)
	OR ([AdvanceType] = @AdvanceType AND @AdvanceType is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Fee_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Fee_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Fee_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the Fee table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Fee_Get_List

AS


				
				SELECT
					[FeeID],
					[MinValue],
					[MaxValue],
					[MinFee],
					[FeeRatio],
					[FeeType],
					[VAT]
				FROM
					[dbo].[Fee]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Fee_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Fee_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Fee_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the Fee table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Fee_GetPaged
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
					SET @OrderBy = '[FeeID]'
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
				SET @SQL = @SQL + ', [FeeID]'
				SET @SQL = @SQL + ', [MinValue]'
				SET @SQL = @SQL + ', [MaxValue]'
				SET @SQL = @SQL + ', [MinFee]'
				SET @SQL = @SQL + ', [FeeRatio]'
				SET @SQL = @SQL + ', [FeeType]'
				SET @SQL = @SQL + ', [VAT]'
				SET @SQL = @SQL + ' FROM [dbo].[Fee]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [FeeID],'
				SET @SQL = @SQL + ' [MinValue],'
				SET @SQL = @SQL + ' [MaxValue],'
				SET @SQL = @SQL + ' [MinFee],'
				SET @SQL = @SQL + ' [FeeRatio],'
				SET @SQL = @SQL + ' [FeeType],'
				SET @SQL = @SQL + ' [VAT]'
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
				SET @SQL = @SQL + ' FROM [dbo].[Fee]'
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

	

-- Drop the dbo.Fee_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Fee_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Fee_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the Fee table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Fee_Insert
(

	@FeeId bigint    OUTPUT,

	@MinValue decimal (18, 3)  ,

	@MaxValue decimal (18, 3)  ,

	@MinFee decimal (12, 3)  ,

	@FeeRatio decimal (8, 3)  ,

	@FeeType int   ,

	@Vat decimal (8, 3)  
)
AS


				
				INSERT INTO [dbo].[Fee]
					(
					[MinValue]
					,[MaxValue]
					,[MinFee]
					,[FeeRatio]
					,[FeeType]
					,[VAT]
					)
				VALUES
					(
					@MinValue
					,@MaxValue
					,@MinFee
					,@FeeRatio
					,@FeeType
					,@Vat
					)
				
				-- Get the identity value
				SET @FeeId = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Fee_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Fee_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Fee_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the Fee table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Fee_Update
(

	@FeeId bigint   ,

	@MinValue decimal (18, 3)  ,

	@MaxValue decimal (18, 3)  ,

	@MinFee decimal (12, 3)  ,

	@FeeRatio decimal (8, 3)  ,

	@FeeType int   ,

	@Vat decimal (8, 3)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Fee]
				SET
					[MinValue] = @MinValue
					,[MaxValue] = @MaxValue
					,[MinFee] = @MinFee
					,[FeeRatio] = @FeeRatio
					,[FeeType] = @FeeType
					,[VAT] = @Vat
				WHERE
[FeeID] = @FeeId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Fee_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Fee_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Fee_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the Fee table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Fee_Delete
(

	@FeeId bigint   
)
AS


				DELETE FROM [dbo].[Fee] WITH (ROWLOCK) 
				WHERE
					[FeeID] = @FeeId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Fee_GetByFeeId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Fee_GetByFeeId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Fee_GetByFeeId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the Fee table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Fee_GetByFeeId
(

	@FeeId bigint   
)
AS


				SELECT
					[FeeID],
					[MinValue],
					[MaxValue],
					[MinFee],
					[FeeRatio],
					[FeeType],
					[VAT]
				FROM
					[dbo].[Fee]
				WHERE
					[FeeID] = @FeeId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Fee_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Fee_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Fee_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the Fee table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Fee_Find
(

	@SearchUsingOR bit   = null ,

	@FeeId bigint   = null ,

	@MinValue decimal (18, 3)  = null ,

	@MaxValue decimal (18, 3)  = null ,

	@MinFee decimal (12, 3)  = null ,

	@FeeRatio decimal (8, 3)  = null ,

	@FeeType int   = null ,

	@Vat decimal (8, 3)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [FeeID]
	, [MinValue]
	, [MaxValue]
	, [MinFee]
	, [FeeRatio]
	, [FeeType]
	, [VAT]
    FROM
	[dbo].[Fee]
    WHERE 
	 ([FeeID] = @FeeId OR @FeeId IS NULL)
	AND ([MinValue] = @MinValue OR @MinValue IS NULL)
	AND ([MaxValue] = @MaxValue OR @MaxValue IS NULL)
	AND ([MinFee] = @MinFee OR @MinFee IS NULL)
	AND ([FeeRatio] = @FeeRatio OR @FeeRatio IS NULL)
	AND ([FeeType] = @FeeType OR @FeeType IS NULL)
	AND ([VAT] = @Vat OR @Vat IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [FeeID]
	, [MinValue]
	, [MaxValue]
	, [MinFee]
	, [FeeRatio]
	, [FeeType]
	, [VAT]
    FROM
	[dbo].[Fee]
    WHERE 
	 ([FeeID] = @FeeId AND @FeeId is not null)
	OR ([MinValue] = @MinValue AND @MinValue is not null)
	OR ([MaxValue] = @MaxValue AND @MaxValue is not null)
	OR ([MinFee] = @MinFee AND @MinFee is not null)
	OR ([FeeRatio] = @FeeRatio AND @FeeRatio is not null)
	OR ([FeeType] = @FeeType AND @FeeType is not null)
	OR ([VAT] = @Vat AND @Vat is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.StockTransfer_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.StockTransfer_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.StockTransfer_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the StockTransfer table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.StockTransfer_Get_List

AS


				
				SELECT
					[ID],
					[SecSymbol],
					[WithdrawableAmt],
					[TransferedAmt],
					[AdvOrderAmt],
					[AvilableAmt],
					[RequestAmt],
					[RequestTime],
					[SrcAccountID],
					[DestAccountID],
					[TransType],
					[Status],
					[ExecTime],
					[ApprovedAmt],
					[Note],
					[BrokerID]
				FROM
					[dbo].[StockTransfer]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.StockTransfer_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.StockTransfer_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.StockTransfer_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the StockTransfer table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.StockTransfer_GetPaged
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
				SET @SQL = @SQL + ', [SecSymbol]'
				SET @SQL = @SQL + ', [WithdrawableAmt]'
				SET @SQL = @SQL + ', [TransferedAmt]'
				SET @SQL = @SQL + ', [AdvOrderAmt]'
				SET @SQL = @SQL + ', [AvilableAmt]'
				SET @SQL = @SQL + ', [RequestAmt]'
				SET @SQL = @SQL + ', [RequestTime]'
				SET @SQL = @SQL + ', [SrcAccountID]'
				SET @SQL = @SQL + ', [DestAccountID]'
				SET @SQL = @SQL + ', [TransType]'
				SET @SQL = @SQL + ', [Status]'
				SET @SQL = @SQL + ', [ExecTime]'
				SET @SQL = @SQL + ', [ApprovedAmt]'
				SET @SQL = @SQL + ', [Note]'
				SET @SQL = @SQL + ', [BrokerID]'
				SET @SQL = @SQL + ' FROM [dbo].[StockTransfer]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [ID],'
				SET @SQL = @SQL + ' [SecSymbol],'
				SET @SQL = @SQL + ' [WithdrawableAmt],'
				SET @SQL = @SQL + ' [TransferedAmt],'
				SET @SQL = @SQL + ' [AdvOrderAmt],'
				SET @SQL = @SQL + ' [AvilableAmt],'
				SET @SQL = @SQL + ' [RequestAmt],'
				SET @SQL = @SQL + ' [RequestTime],'
				SET @SQL = @SQL + ' [SrcAccountID],'
				SET @SQL = @SQL + ' [DestAccountID],'
				SET @SQL = @SQL + ' [TransType],'
				SET @SQL = @SQL + ' [Status],'
				SET @SQL = @SQL + ' [ExecTime],'
				SET @SQL = @SQL + ' [ApprovedAmt],'
				SET @SQL = @SQL + ' [Note],'
				SET @SQL = @SQL + ' [BrokerID]'
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
				SET @SQL = @SQL + ' FROM [dbo].[StockTransfer]'
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

	

-- Drop the dbo.StockTransfer_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.StockTransfer_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.StockTransfer_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the StockTransfer table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.StockTransfer_Insert
(

	@Id bigint    OUTPUT,

	@SecSymbol varchar (8)  ,

	@WithdrawableAmt bigint   ,

	@TransferedAmt bigint   ,

	@AdvOrderAmt bigint   ,

	@AvilableAmt bigint   ,

	@RequestAmt bigint   ,

	@RequestTime datetime   ,

	@SrcAccountId varchar (20)  ,

	@DestAccountId varchar (20)  ,

	@TransType int   ,

	@Status int   ,

	@ExecTime datetime   ,

	@ApprovedAmt bigint   ,

	@Note ntext   ,

	@BrokerId varchar (20)  
)
AS


				
				INSERT INTO [dbo].[StockTransfer]
					(
					[SecSymbol]
					,[WithdrawableAmt]
					,[TransferedAmt]
					,[AdvOrderAmt]
					,[AvilableAmt]
					,[RequestAmt]
					,[RequestTime]
					,[SrcAccountID]
					,[DestAccountID]
					,[TransType]
					,[Status]
					,[ExecTime]
					,[ApprovedAmt]
					,[Note]
					,[BrokerID]
					)
				VALUES
					(
					@SecSymbol
					,@WithdrawableAmt
					,@TransferedAmt
					,@AdvOrderAmt
					,@AvilableAmt
					,@RequestAmt
					,@RequestTime
					,@SrcAccountId
					,@DestAccountId
					,@TransType
					,@Status
					,@ExecTime
					,@ApprovedAmt
					,@Note
					,@BrokerId
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

	

-- Drop the dbo.StockTransfer_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.StockTransfer_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.StockTransfer_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the StockTransfer table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.StockTransfer_Update
(

	@Id bigint   ,

	@SecSymbol varchar (8)  ,

	@WithdrawableAmt bigint   ,

	@TransferedAmt bigint   ,

	@AdvOrderAmt bigint   ,

	@AvilableAmt bigint   ,

	@RequestAmt bigint   ,

	@RequestTime datetime   ,

	@SrcAccountId varchar (20)  ,

	@DestAccountId varchar (20)  ,

	@TransType int   ,

	@Status int   ,

	@ExecTime datetime   ,

	@ApprovedAmt bigint   ,

	@Note ntext   ,

	@BrokerId varchar (20)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[StockTransfer]
				SET
					[SecSymbol] = @SecSymbol
					,[WithdrawableAmt] = @WithdrawableAmt
					,[TransferedAmt] = @TransferedAmt
					,[AdvOrderAmt] = @AdvOrderAmt
					,[AvilableAmt] = @AvilableAmt
					,[RequestAmt] = @RequestAmt
					,[RequestTime] = @RequestTime
					,[SrcAccountID] = @SrcAccountId
					,[DestAccountID] = @DestAccountId
					,[TransType] = @TransType
					,[Status] = @Status
					,[ExecTime] = @ExecTime
					,[ApprovedAmt] = @ApprovedAmt
					,[Note] = @Note
					,[BrokerID] = @BrokerId
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.StockTransfer_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.StockTransfer_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.StockTransfer_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the StockTransfer table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.StockTransfer_Delete
(

	@Id bigint   
)
AS


				DELETE FROM [dbo].[StockTransfer] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.StockTransfer_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.StockTransfer_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.StockTransfer_GetById
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the StockTransfer table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.StockTransfer_GetById
(

	@Id bigint   
)
AS


				SELECT
					[ID],
					[SecSymbol],
					[WithdrawableAmt],
					[TransferedAmt],
					[AdvOrderAmt],
					[AvilableAmt],
					[RequestAmt],
					[RequestTime],
					[SrcAccountID],
					[DestAccountID],
					[TransType],
					[Status],
					[ExecTime],
					[ApprovedAmt],
					[Note],
					[BrokerID]
				FROM
					[dbo].[StockTransfer]
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

	

-- Drop the dbo.StockTransfer_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.StockTransfer_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.StockTransfer_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the StockTransfer table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.StockTransfer_Find
(

	@SearchUsingOR bit   = null ,

	@Id bigint   = null ,

	@SecSymbol varchar (8)  = null ,

	@WithdrawableAmt bigint   = null ,

	@TransferedAmt bigint   = null ,

	@AdvOrderAmt bigint   = null ,

	@AvilableAmt bigint   = null ,

	@RequestAmt bigint   = null ,

	@RequestTime datetime   = null ,

	@SrcAccountId varchar (20)  = null ,

	@DestAccountId varchar (20)  = null ,

	@TransType int   = null ,

	@Status int   = null ,

	@ExecTime datetime   = null ,

	@ApprovedAmt bigint   = null ,

	@Note ntext   = null ,

	@BrokerId varchar (20)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [SecSymbol]
	, [WithdrawableAmt]
	, [TransferedAmt]
	, [AdvOrderAmt]
	, [AvilableAmt]
	, [RequestAmt]
	, [RequestTime]
	, [SrcAccountID]
	, [DestAccountID]
	, [TransType]
	, [Status]
	, [ExecTime]
	, [ApprovedAmt]
	, [Note]
	, [BrokerID]
    FROM
	[dbo].[StockTransfer]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([SecSymbol] = @SecSymbol OR @SecSymbol IS NULL)
	AND ([WithdrawableAmt] = @WithdrawableAmt OR @WithdrawableAmt IS NULL)
	AND ([TransferedAmt] = @TransferedAmt OR @TransferedAmt IS NULL)
	AND ([AdvOrderAmt] = @AdvOrderAmt OR @AdvOrderAmt IS NULL)
	AND ([AvilableAmt] = @AvilableAmt OR @AvilableAmt IS NULL)
	AND ([RequestAmt] = @RequestAmt OR @RequestAmt IS NULL)
	AND ([RequestTime] = @RequestTime OR @RequestTime IS NULL)
	AND ([SrcAccountID] = @SrcAccountId OR @SrcAccountId IS NULL)
	AND ([DestAccountID] = @DestAccountId OR @DestAccountId IS NULL)
	AND ([TransType] = @TransType OR @TransType IS NULL)
	AND ([Status] = @Status OR @Status IS NULL)
	AND ([ExecTime] = @ExecTime OR @ExecTime IS NULL)
	AND ([ApprovedAmt] = @ApprovedAmt OR @ApprovedAmt IS NULL)
	AND ([BrokerID] = @BrokerId OR @BrokerId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [SecSymbol]
	, [WithdrawableAmt]
	, [TransferedAmt]
	, [AdvOrderAmt]
	, [AvilableAmt]
	, [RequestAmt]
	, [RequestTime]
	, [SrcAccountID]
	, [DestAccountID]
	, [TransType]
	, [Status]
	, [ExecTime]
	, [ApprovedAmt]
	, [Note]
	, [BrokerID]
    FROM
	[dbo].[StockTransfer]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([SecSymbol] = @SecSymbol AND @SecSymbol is not null)
	OR ([WithdrawableAmt] = @WithdrawableAmt AND @WithdrawableAmt is not null)
	OR ([TransferedAmt] = @TransferedAmt AND @TransferedAmt is not null)
	OR ([AdvOrderAmt] = @AdvOrderAmt AND @AdvOrderAmt is not null)
	OR ([AvilableAmt] = @AvilableAmt AND @AvilableAmt is not null)
	OR ([RequestAmt] = @RequestAmt AND @RequestAmt is not null)
	OR ([RequestTime] = @RequestTime AND @RequestTime is not null)
	OR ([SrcAccountID] = @SrcAccountId AND @SrcAccountId is not null)
	OR ([DestAccountID] = @DestAccountId AND @DestAccountId is not null)
	OR ([TransType] = @TransType AND @TransType is not null)
	OR ([Status] = @Status AND @Status is not null)
	OR ([ExecTime] = @ExecTime AND @ExecTime is not null)
	OR ([ApprovedAmt] = @ApprovedAmt AND @ApprovedAmt is not null)
	OR ([BrokerID] = @BrokerId AND @BrokerId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CashAdvance_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvance_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvance_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the CashAdvance table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvance_Get_List

AS


				
				SELECT
					[ID],
					[SubAccountID],
					[AdvanceDate],
					[ContractNo],
					[OrderID],
					[StockSymbol],
					[SellDueDate],
					[CashDueDate],
					[TotalSellValue],
					[CashAvailable],
					[CashRequest],
					[Fee],
					[CashReceived],
					[Status],
					[TradeType],
					[BrokerID],
					[Reason],
					[ExecTime],
					[VAT]
				FROM
					[dbo].[CashAdvance]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CashAdvance_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvance_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvance_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the CashAdvance table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvance_GetPaged
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
				SET @SQL = @SQL + ', [SubAccountID]'
				SET @SQL = @SQL + ', [AdvanceDate]'
				SET @SQL = @SQL + ', [ContractNo]'
				SET @SQL = @SQL + ', [OrderID]'
				SET @SQL = @SQL + ', [StockSymbol]'
				SET @SQL = @SQL + ', [SellDueDate]'
				SET @SQL = @SQL + ', [CashDueDate]'
				SET @SQL = @SQL + ', [TotalSellValue]'
				SET @SQL = @SQL + ', [CashAvailable]'
				SET @SQL = @SQL + ', [CashRequest]'
				SET @SQL = @SQL + ', [Fee]'
				SET @SQL = @SQL + ', [CashReceived]'
				SET @SQL = @SQL + ', [Status]'
				SET @SQL = @SQL + ', [TradeType]'
				SET @SQL = @SQL + ', [BrokerID]'
				SET @SQL = @SQL + ', [Reason]'
				SET @SQL = @SQL + ', [ExecTime]'
				SET @SQL = @SQL + ', [VAT]'
				SET @SQL = @SQL + ' FROM [dbo].[CashAdvance]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [ID],'
				SET @SQL = @SQL + ' [SubAccountID],'
				SET @SQL = @SQL + ' [AdvanceDate],'
				SET @SQL = @SQL + ' [ContractNo],'
				SET @SQL = @SQL + ' [OrderID],'
				SET @SQL = @SQL + ' [StockSymbol],'
				SET @SQL = @SQL + ' [SellDueDate],'
				SET @SQL = @SQL + ' [CashDueDate],'
				SET @SQL = @SQL + ' [TotalSellValue],'
				SET @SQL = @SQL + ' [CashAvailable],'
				SET @SQL = @SQL + ' [CashRequest],'
				SET @SQL = @SQL + ' [Fee],'
				SET @SQL = @SQL + ' [CashReceived],'
				SET @SQL = @SQL + ' [Status],'
				SET @SQL = @SQL + ' [TradeType],'
				SET @SQL = @SQL + ' [BrokerID],'
				SET @SQL = @SQL + ' [Reason],'
				SET @SQL = @SQL + ' [ExecTime],'
				SET @SQL = @SQL + ' [VAT]'
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
				SET @SQL = @SQL + ' FROM [dbo].[CashAdvance]'
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

	

-- Drop the dbo.CashAdvance_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvance_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvance_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the CashAdvance table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvance_Insert
(

	@Id bigint    OUTPUT,

	@SubAccountId varchar (20)  ,

	@AdvanceDate datetime   ,

	@ContractNo varchar (50)  ,

	@OrderId int   ,

	@StockSymbol varchar (8)  ,

	@SellDueDate datetime   ,

	@CashDueDate datetime   ,

	@TotalSellValue decimal (18, 3)  ,

	@CashAvailable decimal (18, 3)  ,

	@CashRequest decimal (18, 3)  ,

	@Fee decimal (18, 3)  ,

	@CashReceived decimal (18, 3)  ,

	@Status int   ,

	@TradeType int   ,

	@BrokerId varchar (20)  ,

	@Reason ntext   ,

	@ExecTime datetime   ,

	@Vat decimal (18, 3)  
)
AS


				
				INSERT INTO [dbo].[CashAdvance]
					(
					[SubAccountID]
					,[AdvanceDate]
					,[ContractNo]
					,[OrderID]
					,[StockSymbol]
					,[SellDueDate]
					,[CashDueDate]
					,[TotalSellValue]
					,[CashAvailable]
					,[CashRequest]
					,[Fee]
					,[CashReceived]
					,[Status]
					,[TradeType]
					,[BrokerID]
					,[Reason]
					,[ExecTime]
					,[VAT]
					)
				VALUES
					(
					@SubAccountId
					,@AdvanceDate
					,@ContractNo
					,@OrderId
					,@StockSymbol
					,@SellDueDate
					,@CashDueDate
					,@TotalSellValue
					,@CashAvailable
					,@CashRequest
					,@Fee
					,@CashReceived
					,@Status
					,@TradeType
					,@BrokerId
					,@Reason
					,@ExecTime
					,@Vat
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

	

-- Drop the dbo.CashAdvance_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvance_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvance_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the CashAdvance table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvance_Update
(

	@Id bigint   ,

	@SubAccountId varchar (20)  ,

	@AdvanceDate datetime   ,

	@ContractNo varchar (50)  ,

	@OrderId int   ,

	@StockSymbol varchar (8)  ,

	@SellDueDate datetime   ,

	@CashDueDate datetime   ,

	@TotalSellValue decimal (18, 3)  ,

	@CashAvailable decimal (18, 3)  ,

	@CashRequest decimal (18, 3)  ,

	@Fee decimal (18, 3)  ,

	@CashReceived decimal (18, 3)  ,

	@Status int   ,

	@TradeType int   ,

	@BrokerId varchar (20)  ,

	@Reason ntext   ,

	@ExecTime datetime   ,

	@Vat decimal (18, 3)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[CashAdvance]
				SET
					[SubAccountID] = @SubAccountId
					,[AdvanceDate] = @AdvanceDate
					,[ContractNo] = @ContractNo
					,[OrderID] = @OrderId
					,[StockSymbol] = @StockSymbol
					,[SellDueDate] = @SellDueDate
					,[CashDueDate] = @CashDueDate
					,[TotalSellValue] = @TotalSellValue
					,[CashAvailable] = @CashAvailable
					,[CashRequest] = @CashRequest
					,[Fee] = @Fee
					,[CashReceived] = @CashReceived
					,[Status] = @Status
					,[TradeType] = @TradeType
					,[BrokerID] = @BrokerId
					,[Reason] = @Reason
					,[ExecTime] = @ExecTime
					,[VAT] = @Vat
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CashAdvance_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvance_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvance_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the CashAdvance table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvance_Delete
(

	@Id bigint   
)
AS


				DELETE FROM [dbo].[CashAdvance] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CashAdvance_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvance_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvance_GetById
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the CashAdvance table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvance_GetById
(

	@Id bigint   
)
AS


				SELECT
					[ID],
					[SubAccountID],
					[AdvanceDate],
					[ContractNo],
					[OrderID],
					[StockSymbol],
					[SellDueDate],
					[CashDueDate],
					[TotalSellValue],
					[CashAvailable],
					[CashRequest],
					[Fee],
					[CashReceived],
					[Status],
					[TradeType],
					[BrokerID],
					[Reason],
					[ExecTime],
					[VAT]
				FROM
					[dbo].[CashAdvance]
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

	

-- Drop the dbo.CashAdvance_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvance_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvance_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the CashAdvance table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvance_Find
(

	@SearchUsingOR bit   = null ,

	@Id bigint   = null ,

	@SubAccountId varchar (20)  = null ,

	@AdvanceDate datetime   = null ,

	@ContractNo varchar (50)  = null ,

	@OrderId int   = null ,

	@StockSymbol varchar (8)  = null ,

	@SellDueDate datetime   = null ,

	@CashDueDate datetime   = null ,

	@TotalSellValue decimal (18, 3)  = null ,

	@CashAvailable decimal (18, 3)  = null ,

	@CashRequest decimal (18, 3)  = null ,

	@Fee decimal (18, 3)  = null ,

	@CashReceived decimal (18, 3)  = null ,

	@Status int   = null ,

	@TradeType int   = null ,

	@BrokerId varchar (20)  = null ,

	@Reason ntext   = null ,

	@ExecTime datetime   = null ,

	@Vat decimal (18, 3)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [SubAccountID]
	, [AdvanceDate]
	, [ContractNo]
	, [OrderID]
	, [StockSymbol]
	, [SellDueDate]
	, [CashDueDate]
	, [TotalSellValue]
	, [CashAvailable]
	, [CashRequest]
	, [Fee]
	, [CashReceived]
	, [Status]
	, [TradeType]
	, [BrokerID]
	, [Reason]
	, [ExecTime]
	, [VAT]
    FROM
	[dbo].[CashAdvance]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([SubAccountID] = @SubAccountId OR @SubAccountId IS NULL)
	AND ([AdvanceDate] = @AdvanceDate OR @AdvanceDate IS NULL)
	AND ([ContractNo] = @ContractNo OR @ContractNo IS NULL)
	AND ([OrderID] = @OrderId OR @OrderId IS NULL)
	AND ([StockSymbol] = @StockSymbol OR @StockSymbol IS NULL)
	AND ([SellDueDate] = @SellDueDate OR @SellDueDate IS NULL)
	AND ([CashDueDate] = @CashDueDate OR @CashDueDate IS NULL)
	AND ([TotalSellValue] = @TotalSellValue OR @TotalSellValue IS NULL)
	AND ([CashAvailable] = @CashAvailable OR @CashAvailable IS NULL)
	AND ([CashRequest] = @CashRequest OR @CashRequest IS NULL)
	AND ([Fee] = @Fee OR @Fee IS NULL)
	AND ([CashReceived] = @CashReceived OR @CashReceived IS NULL)
	AND ([Status] = @Status OR @Status IS NULL)
	AND ([TradeType] = @TradeType OR @TradeType IS NULL)
	AND ([BrokerID] = @BrokerId OR @BrokerId IS NULL)
	AND ([ExecTime] = @ExecTime OR @ExecTime IS NULL)
	AND ([VAT] = @Vat OR @Vat IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [SubAccountID]
	, [AdvanceDate]
	, [ContractNo]
	, [OrderID]
	, [StockSymbol]
	, [SellDueDate]
	, [CashDueDate]
	, [TotalSellValue]
	, [CashAvailable]
	, [CashRequest]
	, [Fee]
	, [CashReceived]
	, [Status]
	, [TradeType]
	, [BrokerID]
	, [Reason]
	, [ExecTime]
	, [VAT]
    FROM
	[dbo].[CashAdvance]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([SubAccountID] = @SubAccountId AND @SubAccountId is not null)
	OR ([AdvanceDate] = @AdvanceDate AND @AdvanceDate is not null)
	OR ([ContractNo] = @ContractNo AND @ContractNo is not null)
	OR ([OrderID] = @OrderId AND @OrderId is not null)
	OR ([StockSymbol] = @StockSymbol AND @StockSymbol is not null)
	OR ([SellDueDate] = @SellDueDate AND @SellDueDate is not null)
	OR ([CashDueDate] = @CashDueDate AND @CashDueDate is not null)
	OR ([TotalSellValue] = @TotalSellValue AND @TotalSellValue is not null)
	OR ([CashAvailable] = @CashAvailable AND @CashAvailable is not null)
	OR ([CashRequest] = @CashRequest AND @CashRequest is not null)
	OR ([Fee] = @Fee AND @Fee is not null)
	OR ([CashReceived] = @CashReceived AND @CashReceived is not null)
	OR ([Status] = @Status AND @Status is not null)
	OR ([TradeType] = @TradeType AND @TradeType is not null)
	OR ([BrokerID] = @BrokerId AND @BrokerId is not null)
	OR ([ExecTime] = @ExecTime AND @ExecTime is not null)
	OR ([VAT] = @Vat AND @Vat is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CashTransfer_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashTransfer_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashTransfer_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the CashTransfer table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashTransfer_Get_List

AS


				
				SELECT
					[ID],
					[WithdrawableAmt],
					[TransferedAmt],
					[AdvOrderAmt],
					[AvilableAmt],
					[RequestAmt],
					[RequestTime],
					[Fee],
					[VAT],
					[AmtAfterFee],
					[SrcAccountID],
					[DestAccountID],
					[TransType],
					[Status],
					[ExecTime],
					[ApprovedAmt],
					[Note],
					[BrokerID],
					[BankName],
					[BranchName]
				FROM
					[dbo].[CashTransfer]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CashTransfer_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashTransfer_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashTransfer_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the CashTransfer table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashTransfer_GetPaged
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
				SET @SQL = @SQL + ', [WithdrawableAmt]'
				SET @SQL = @SQL + ', [TransferedAmt]'
				SET @SQL = @SQL + ', [AdvOrderAmt]'
				SET @SQL = @SQL + ', [AvilableAmt]'
				SET @SQL = @SQL + ', [RequestAmt]'
				SET @SQL = @SQL + ', [RequestTime]'
				SET @SQL = @SQL + ', [Fee]'
				SET @SQL = @SQL + ', [VAT]'
				SET @SQL = @SQL + ', [AmtAfterFee]'
				SET @SQL = @SQL + ', [SrcAccountID]'
				SET @SQL = @SQL + ', [DestAccountID]'
				SET @SQL = @SQL + ', [TransType]'
				SET @SQL = @SQL + ', [Status]'
				SET @SQL = @SQL + ', [ExecTime]'
				SET @SQL = @SQL + ', [ApprovedAmt]'
				SET @SQL = @SQL + ', [Note]'
				SET @SQL = @SQL + ', [BrokerID]'
				SET @SQL = @SQL + ', [BankName]'
				SET @SQL = @SQL + ', [BranchName]'
				SET @SQL = @SQL + ' FROM [dbo].[CashTransfer]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [ID],'
				SET @SQL = @SQL + ' [WithdrawableAmt],'
				SET @SQL = @SQL + ' [TransferedAmt],'
				SET @SQL = @SQL + ' [AdvOrderAmt],'
				SET @SQL = @SQL + ' [AvilableAmt],'
				SET @SQL = @SQL + ' [RequestAmt],'
				SET @SQL = @SQL + ' [RequestTime],'
				SET @SQL = @SQL + ' [Fee],'
				SET @SQL = @SQL + ' [VAT],'
				SET @SQL = @SQL + ' [AmtAfterFee],'
				SET @SQL = @SQL + ' [SrcAccountID],'
				SET @SQL = @SQL + ' [DestAccountID],'
				SET @SQL = @SQL + ' [TransType],'
				SET @SQL = @SQL + ' [Status],'
				SET @SQL = @SQL + ' [ExecTime],'
				SET @SQL = @SQL + ' [ApprovedAmt],'
				SET @SQL = @SQL + ' [Note],'
				SET @SQL = @SQL + ' [BrokerID],'
				SET @SQL = @SQL + ' [BankName],'
				SET @SQL = @SQL + ' [BranchName]'
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
				SET @SQL = @SQL + ' FROM [dbo].[CashTransfer]'
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

	

-- Drop the dbo.CashTransfer_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashTransfer_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashTransfer_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the CashTransfer table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashTransfer_Insert
(

	@Id bigint    OUTPUT,

	@WithdrawableAmt decimal (18, 3)  ,

	@TransferedAmt decimal (18, 3)  ,

	@AdvOrderAmt decimal (18, 3)  ,

	@AvilableAmt decimal (18, 3)  ,

	@RequestAmt decimal (18, 3)  ,

	@RequestTime datetime   ,

	@Fee decimal (18, 3)  ,

	@Vat decimal (18, 3)  ,

	@AmtAfterFee decimal (18, 3)  ,

	@SrcAccountId varchar (20)  ,

	@DestAccountId varchar (64)  ,

	@TransType int   ,

	@Status int   ,

	@ExecTime datetime   ,

	@ApprovedAmt decimal (18, 3)  ,

	@Note ntext   ,

	@BrokerId varchar (20)  ,

	@BankName nvarchar (100)  ,

	@BranchName nvarchar (100)  
)
AS


				
				INSERT INTO [dbo].[CashTransfer]
					(
					[WithdrawableAmt]
					,[TransferedAmt]
					,[AdvOrderAmt]
					,[AvilableAmt]
					,[RequestAmt]
					,[RequestTime]
					,[Fee]
					,[VAT]
					,[AmtAfterFee]
					,[SrcAccountID]
					,[DestAccountID]
					,[TransType]
					,[Status]
					,[ExecTime]
					,[ApprovedAmt]
					,[Note]
					,[BrokerID]
					,[BankName]
					,[BranchName]
					)
				VALUES
					(
					@WithdrawableAmt
					,@TransferedAmt
					,@AdvOrderAmt
					,@AvilableAmt
					,@RequestAmt
					,@RequestTime
					,@Fee
					,@Vat
					,@AmtAfterFee
					,@SrcAccountId
					,@DestAccountId
					,@TransType
					,@Status
					,@ExecTime
					,@ApprovedAmt
					,@Note
					,@BrokerId
					,@BankName
					,@BranchName
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

	

-- Drop the dbo.CashTransfer_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashTransfer_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashTransfer_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the CashTransfer table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashTransfer_Update
(

	@Id bigint   ,

	@WithdrawableAmt decimal (18, 3)  ,

	@TransferedAmt decimal (18, 3)  ,

	@AdvOrderAmt decimal (18, 3)  ,

	@AvilableAmt decimal (18, 3)  ,

	@RequestAmt decimal (18, 3)  ,

	@RequestTime datetime   ,

	@Fee decimal (18, 3)  ,

	@Vat decimal (18, 3)  ,

	@AmtAfterFee decimal (18, 3)  ,

	@SrcAccountId varchar (20)  ,

	@DestAccountId varchar (64)  ,

	@TransType int   ,

	@Status int   ,

	@ExecTime datetime   ,

	@ApprovedAmt decimal (18, 3)  ,

	@Note ntext   ,

	@BrokerId varchar (20)  ,

	@BankName nvarchar (100)  ,

	@BranchName nvarchar (100)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[CashTransfer]
				SET
					[WithdrawableAmt] = @WithdrawableAmt
					,[TransferedAmt] = @TransferedAmt
					,[AdvOrderAmt] = @AdvOrderAmt
					,[AvilableAmt] = @AvilableAmt
					,[RequestAmt] = @RequestAmt
					,[RequestTime] = @RequestTime
					,[Fee] = @Fee
					,[VAT] = @Vat
					,[AmtAfterFee] = @AmtAfterFee
					,[SrcAccountID] = @SrcAccountId
					,[DestAccountID] = @DestAccountId
					,[TransType] = @TransType
					,[Status] = @Status
					,[ExecTime] = @ExecTime
					,[ApprovedAmt] = @ApprovedAmt
					,[Note] = @Note
					,[BrokerID] = @BrokerId
					,[BankName] = @BankName
					,[BranchName] = @BranchName
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CashTransfer_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashTransfer_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashTransfer_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the CashTransfer table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashTransfer_Delete
(

	@Id bigint   
)
AS


				DELETE FROM [dbo].[CashTransfer] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CashTransfer_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashTransfer_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashTransfer_GetById
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the CashTransfer table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashTransfer_GetById
(

	@Id bigint   
)
AS


				SELECT
					[ID],
					[WithdrawableAmt],
					[TransferedAmt],
					[AdvOrderAmt],
					[AvilableAmt],
					[RequestAmt],
					[RequestTime],
					[Fee],
					[VAT],
					[AmtAfterFee],
					[SrcAccountID],
					[DestAccountID],
					[TransType],
					[Status],
					[ExecTime],
					[ApprovedAmt],
					[Note],
					[BrokerID],
					[BankName],
					[BranchName]
				FROM
					[dbo].[CashTransfer]
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

	

-- Drop the dbo.CashTransfer_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashTransfer_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashTransfer_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the CashTransfer table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashTransfer_Find
(

	@SearchUsingOR bit   = null ,

	@Id bigint   = null ,

	@WithdrawableAmt decimal (18, 3)  = null ,

	@TransferedAmt decimal (18, 3)  = null ,

	@AdvOrderAmt decimal (18, 3)  = null ,

	@AvilableAmt decimal (18, 3)  = null ,

	@RequestAmt decimal (18, 3)  = null ,

	@RequestTime datetime   = null ,

	@Fee decimal (18, 3)  = null ,

	@Vat decimal (18, 3)  = null ,

	@AmtAfterFee decimal (18, 3)  = null ,

	@SrcAccountId varchar (20)  = null ,

	@DestAccountId varchar (64)  = null ,

	@TransType int   = null ,

	@Status int   = null ,

	@ExecTime datetime   = null ,

	@ApprovedAmt decimal (18, 3)  = null ,

	@Note ntext   = null ,

	@BrokerId varchar (20)  = null ,

	@BankName nvarchar (100)  = null ,

	@BranchName nvarchar (100)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [WithdrawableAmt]
	, [TransferedAmt]
	, [AdvOrderAmt]
	, [AvilableAmt]
	, [RequestAmt]
	, [RequestTime]
	, [Fee]
	, [VAT]
	, [AmtAfterFee]
	, [SrcAccountID]
	, [DestAccountID]
	, [TransType]
	, [Status]
	, [ExecTime]
	, [ApprovedAmt]
	, [Note]
	, [BrokerID]
	, [BankName]
	, [BranchName]
    FROM
	[dbo].[CashTransfer]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([WithdrawableAmt] = @WithdrawableAmt OR @WithdrawableAmt IS NULL)
	AND ([TransferedAmt] = @TransferedAmt OR @TransferedAmt IS NULL)
	AND ([AdvOrderAmt] = @AdvOrderAmt OR @AdvOrderAmt IS NULL)
	AND ([AvilableAmt] = @AvilableAmt OR @AvilableAmt IS NULL)
	AND ([RequestAmt] = @RequestAmt OR @RequestAmt IS NULL)
	AND ([RequestTime] = @RequestTime OR @RequestTime IS NULL)
	AND ([Fee] = @Fee OR @Fee IS NULL)
	AND ([VAT] = @Vat OR @Vat IS NULL)
	AND ([AmtAfterFee] = @AmtAfterFee OR @AmtAfterFee IS NULL)
	AND ([SrcAccountID] = @SrcAccountId OR @SrcAccountId IS NULL)
	AND ([DestAccountID] = @DestAccountId OR @DestAccountId IS NULL)
	AND ([TransType] = @TransType OR @TransType IS NULL)
	AND ([Status] = @Status OR @Status IS NULL)
	AND ([ExecTime] = @ExecTime OR @ExecTime IS NULL)
	AND ([ApprovedAmt] = @ApprovedAmt OR @ApprovedAmt IS NULL)
	AND ([BrokerID] = @BrokerId OR @BrokerId IS NULL)
	AND ([BankName] = @BankName OR @BankName IS NULL)
	AND ([BranchName] = @BranchName OR @BranchName IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [WithdrawableAmt]
	, [TransferedAmt]
	, [AdvOrderAmt]
	, [AvilableAmt]
	, [RequestAmt]
	, [RequestTime]
	, [Fee]
	, [VAT]
	, [AmtAfterFee]
	, [SrcAccountID]
	, [DestAccountID]
	, [TransType]
	, [Status]
	, [ExecTime]
	, [ApprovedAmt]
	, [Note]
	, [BrokerID]
	, [BankName]
	, [BranchName]
    FROM
	[dbo].[CashTransfer]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([WithdrawableAmt] = @WithdrawableAmt AND @WithdrawableAmt is not null)
	OR ([TransferedAmt] = @TransferedAmt AND @TransferedAmt is not null)
	OR ([AdvOrderAmt] = @AdvOrderAmt AND @AdvOrderAmt is not null)
	OR ([AvilableAmt] = @AvilableAmt AND @AvilableAmt is not null)
	OR ([RequestAmt] = @RequestAmt AND @RequestAmt is not null)
	OR ([RequestTime] = @RequestTime AND @RequestTime is not null)
	OR ([Fee] = @Fee AND @Fee is not null)
	OR ([VAT] = @Vat AND @Vat is not null)
	OR ([AmtAfterFee] = @AmtAfterFee AND @AmtAfterFee is not null)
	OR ([SrcAccountID] = @SrcAccountId AND @SrcAccountId is not null)
	OR ([DestAccountID] = @DestAccountId AND @DestAccountId is not null)
	OR ([TransType] = @TransType AND @TransType is not null)
	OR ([Status] = @Status AND @Status is not null)
	OR ([ExecTime] = @ExecTime AND @ExecTime is not null)
	OR ([ApprovedAmt] = @ApprovedAmt AND @ApprovedAmt is not null)
	OR ([BrokerID] = @BrokerId AND @BrokerId is not null)
	OR ([BankName] = @BankName AND @BankName is not null)
	OR ([BranchName] = @BranchName AND @BranchName is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.XROrders_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.XROrders_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.XROrders_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the XROrders table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.XROrders_Get_List

AS


				
				SELECT
					[ID],
					[SubAccountID],
					[BuyRightID],
					[SecSymbol],
					[Market],
					[Volume],
					[Price],
					[RegisteredVol],
					[AvailableVol],
					[RequestVol],
					[RequestTime],
					[ApprovedVol],
					[Status],
					[BrokerID],
					[ExecTime],
					[Note]
				FROM
					[dbo].[XROrders]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.XROrders_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.XROrders_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.XROrders_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the XROrders table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.XROrders_GetPaged
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
				SET @SQL = @SQL + ', [SubAccountID]'
				SET @SQL = @SQL + ', [BuyRightID]'
				SET @SQL = @SQL + ', [SecSymbol]'
				SET @SQL = @SQL + ', [Market]'
				SET @SQL = @SQL + ', [Volume]'
				SET @SQL = @SQL + ', [Price]'
				SET @SQL = @SQL + ', [RegisteredVol]'
				SET @SQL = @SQL + ', [AvailableVol]'
				SET @SQL = @SQL + ', [RequestVol]'
				SET @SQL = @SQL + ', [RequestTime]'
				SET @SQL = @SQL + ', [ApprovedVol]'
				SET @SQL = @SQL + ', [Status]'
				SET @SQL = @SQL + ', [BrokerID]'
				SET @SQL = @SQL + ', [ExecTime]'
				SET @SQL = @SQL + ', [Note]'
				SET @SQL = @SQL + ' FROM [dbo].[XROrders]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [ID],'
				SET @SQL = @SQL + ' [SubAccountID],'
				SET @SQL = @SQL + ' [BuyRightID],'
				SET @SQL = @SQL + ' [SecSymbol],'
				SET @SQL = @SQL + ' [Market],'
				SET @SQL = @SQL + ' [Volume],'
				SET @SQL = @SQL + ' [Price],'
				SET @SQL = @SQL + ' [RegisteredVol],'
				SET @SQL = @SQL + ' [AvailableVol],'
				SET @SQL = @SQL + ' [RequestVol],'
				SET @SQL = @SQL + ' [RequestTime],'
				SET @SQL = @SQL + ' [ApprovedVol],'
				SET @SQL = @SQL + ' [Status],'
				SET @SQL = @SQL + ' [BrokerID],'
				SET @SQL = @SQL + ' [ExecTime],'
				SET @SQL = @SQL + ' [Note]'
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
				SET @SQL = @SQL + ' FROM [dbo].[XROrders]'
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

	

-- Drop the dbo.XROrders_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.XROrders_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.XROrders_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the XROrders table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.XROrders_Insert
(

	@Id bigint    OUTPUT,

	@SubAccountId varchar (20)  ,

	@BuyRightId bigint   ,

	@SecSymbol varchar (8)  ,

	@Market varchar (1)  ,

	@Volume bigint   ,

	@Price decimal (10, 3)  ,

	@RegisteredVol bigint   ,

	@AvailableVol bigint   ,

	@RequestVol bigint   ,

	@RequestTime datetime   ,

	@ApprovedVol bigint   ,

	@Status int   ,

	@BrokerId varchar (20)  ,

	@ExecTime datetime   ,

	@Note ntext   
)
AS


				
				INSERT INTO [dbo].[XROrders]
					(
					[SubAccountID]
					,[BuyRightID]
					,[SecSymbol]
					,[Market]
					,[Volume]
					,[Price]
					,[RegisteredVol]
					,[AvailableVol]
					,[RequestVol]
					,[RequestTime]
					,[ApprovedVol]
					,[Status]
					,[BrokerID]
					,[ExecTime]
					,[Note]
					)
				VALUES
					(
					@SubAccountId
					,@BuyRightId
					,@SecSymbol
					,@Market
					,@Volume
					,@Price
					,@RegisteredVol
					,@AvailableVol
					,@RequestVol
					,@RequestTime
					,@ApprovedVol
					,@Status
					,@BrokerId
					,@ExecTime
					,@Note
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

	

-- Drop the dbo.XROrders_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.XROrders_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.XROrders_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the XROrders table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.XROrders_Update
(

	@Id bigint   ,

	@SubAccountId varchar (20)  ,

	@BuyRightId bigint   ,

	@SecSymbol varchar (8)  ,

	@Market varchar (1)  ,

	@Volume bigint   ,

	@Price decimal (10, 3)  ,

	@RegisteredVol bigint   ,

	@AvailableVol bigint   ,

	@RequestVol bigint   ,

	@RequestTime datetime   ,

	@ApprovedVol bigint   ,

	@Status int   ,

	@BrokerId varchar (20)  ,

	@ExecTime datetime   ,

	@Note ntext   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[XROrders]
				SET
					[SubAccountID] = @SubAccountId
					,[BuyRightID] = @BuyRightId
					,[SecSymbol] = @SecSymbol
					,[Market] = @Market
					,[Volume] = @Volume
					,[Price] = @Price
					,[RegisteredVol] = @RegisteredVol
					,[AvailableVol] = @AvailableVol
					,[RequestVol] = @RequestVol
					,[RequestTime] = @RequestTime
					,[ApprovedVol] = @ApprovedVol
					,[Status] = @Status
					,[BrokerID] = @BrokerId
					,[ExecTime] = @ExecTime
					,[Note] = @Note
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.XROrders_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.XROrders_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.XROrders_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the XROrders table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.XROrders_Delete
(

	@Id bigint   
)
AS


				DELETE FROM [dbo].[XROrders] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.XROrders_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.XROrders_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.XROrders_GetById
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the XROrders table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.XROrders_GetById
(

	@Id bigint   
)
AS


				SELECT
					[ID],
					[SubAccountID],
					[BuyRightID],
					[SecSymbol],
					[Market],
					[Volume],
					[Price],
					[RegisteredVol],
					[AvailableVol],
					[RequestVol],
					[RequestTime],
					[ApprovedVol],
					[Status],
					[BrokerID],
					[ExecTime],
					[Note]
				FROM
					[dbo].[XROrders]
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

	

-- Drop the dbo.XROrders_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.XROrders_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.XROrders_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the XROrders table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.XROrders_Find
(

	@SearchUsingOR bit   = null ,

	@Id bigint   = null ,

	@SubAccountId varchar (20)  = null ,

	@BuyRightId bigint   = null ,

	@SecSymbol varchar (8)  = null ,

	@Market varchar (1)  = null ,

	@Volume bigint   = null ,

	@Price decimal (10, 3)  = null ,

	@RegisteredVol bigint   = null ,

	@AvailableVol bigint   = null ,

	@RequestVol bigint   = null ,

	@RequestTime datetime   = null ,

	@ApprovedVol bigint   = null ,

	@Status int   = null ,

	@BrokerId varchar (20)  = null ,

	@ExecTime datetime   = null ,

	@Note ntext   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [SubAccountID]
	, [BuyRightID]
	, [SecSymbol]
	, [Market]
	, [Volume]
	, [Price]
	, [RegisteredVol]
	, [AvailableVol]
	, [RequestVol]
	, [RequestTime]
	, [ApprovedVol]
	, [Status]
	, [BrokerID]
	, [ExecTime]
	, [Note]
    FROM
	[dbo].[XROrders]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([SubAccountID] = @SubAccountId OR @SubAccountId IS NULL)
	AND ([BuyRightID] = @BuyRightId OR @BuyRightId IS NULL)
	AND ([SecSymbol] = @SecSymbol OR @SecSymbol IS NULL)
	AND ([Market] = @Market OR @Market IS NULL)
	AND ([Volume] = @Volume OR @Volume IS NULL)
	AND ([Price] = @Price OR @Price IS NULL)
	AND ([RegisteredVol] = @RegisteredVol OR @RegisteredVol IS NULL)
	AND ([AvailableVol] = @AvailableVol OR @AvailableVol IS NULL)
	AND ([RequestVol] = @RequestVol OR @RequestVol IS NULL)
	AND ([RequestTime] = @RequestTime OR @RequestTime IS NULL)
	AND ([ApprovedVol] = @ApprovedVol OR @ApprovedVol IS NULL)
	AND ([Status] = @Status OR @Status IS NULL)
	AND ([BrokerID] = @BrokerId OR @BrokerId IS NULL)
	AND ([ExecTime] = @ExecTime OR @ExecTime IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [SubAccountID]
	, [BuyRightID]
	, [SecSymbol]
	, [Market]
	, [Volume]
	, [Price]
	, [RegisteredVol]
	, [AvailableVol]
	, [RequestVol]
	, [RequestTime]
	, [ApprovedVol]
	, [Status]
	, [BrokerID]
	, [ExecTime]
	, [Note]
    FROM
	[dbo].[XROrders]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([SubAccountID] = @SubAccountId AND @SubAccountId is not null)
	OR ([BuyRightID] = @BuyRightId AND @BuyRightId is not null)
	OR ([SecSymbol] = @SecSymbol AND @SecSymbol is not null)
	OR ([Market] = @Market AND @Market is not null)
	OR ([Volume] = @Volume AND @Volume is not null)
	OR ([Price] = @Price AND @Price is not null)
	OR ([RegisteredVol] = @RegisteredVol AND @RegisteredVol is not null)
	OR ([AvailableVol] = @AvailableVol AND @AvailableVol is not null)
	OR ([RequestVol] = @RequestVol AND @RequestVol is not null)
	OR ([RequestTime] = @RequestTime AND @RequestTime is not null)
	OR ([ApprovedVol] = @ApprovedVol AND @ApprovedVol is not null)
	OR ([Status] = @Status AND @Status is not null)
	OR ([BrokerID] = @BrokerId AND @BrokerId is not null)
	OR ([ExecTime] = @ExecTime AND @ExecTime is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CashAdvanceHistory_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvanceHistory_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvanceHistory_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets all records from the CashAdvanceHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvanceHistory_Get_List

AS


				
				SELECT
					[ID],
					[SubAccountID],
					[AdvanceDate],
					[ContractNo],
					[OrderID],
					[StockSymbol],
					[SellDueDate],
					[CashDueDate],
					[TotalSellValue],
					[CashAvilable],
					[CashRequest],
					[Fee],
					[CashReceived],
					[Status],
					[TradeType],
					[BrokerID],
					[BrokerName],
					[Reason],
					[ExecTime],
					[VAT]
				FROM
					[dbo].[CashAdvanceHistory]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CashAdvanceHistory_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvanceHistory_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvanceHistory_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Gets records from the CashAdvanceHistory table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvanceHistory_GetPaged
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
				SET @SQL = @SQL + ', [SubAccountID]'
				SET @SQL = @SQL + ', [AdvanceDate]'
				SET @SQL = @SQL + ', [ContractNo]'
				SET @SQL = @SQL + ', [OrderID]'
				SET @SQL = @SQL + ', [StockSymbol]'
				SET @SQL = @SQL + ', [SellDueDate]'
				SET @SQL = @SQL + ', [CashDueDate]'
				SET @SQL = @SQL + ', [TotalSellValue]'
				SET @SQL = @SQL + ', [CashAvilable]'
				SET @SQL = @SQL + ', [CashRequest]'
				SET @SQL = @SQL + ', [Fee]'
				SET @SQL = @SQL + ', [CashReceived]'
				SET @SQL = @SQL + ', [Status]'
				SET @SQL = @SQL + ', [TradeType]'
				SET @SQL = @SQL + ', [BrokerID]'
				SET @SQL = @SQL + ', [BrokerName]'
				SET @SQL = @SQL + ', [Reason]'
				SET @SQL = @SQL + ', [ExecTime]'
				SET @SQL = @SQL + ', [VAT]'
				SET @SQL = @SQL + ' FROM [dbo].[CashAdvanceHistory]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				SET @SQL = @SQL + ' ) SELECT'
				SET @SQL = @SQL + ' [ID],'
				SET @SQL = @SQL + ' [SubAccountID],'
				SET @SQL = @SQL + ' [AdvanceDate],'
				SET @SQL = @SQL + ' [ContractNo],'
				SET @SQL = @SQL + ' [OrderID],'
				SET @SQL = @SQL + ' [StockSymbol],'
				SET @SQL = @SQL + ' [SellDueDate],'
				SET @SQL = @SQL + ' [CashDueDate],'
				SET @SQL = @SQL + ' [TotalSellValue],'
				SET @SQL = @SQL + ' [CashAvilable],'
				SET @SQL = @SQL + ' [CashRequest],'
				SET @SQL = @SQL + ' [Fee],'
				SET @SQL = @SQL + ' [CashReceived],'
				SET @SQL = @SQL + ' [Status],'
				SET @SQL = @SQL + ' [TradeType],'
				SET @SQL = @SQL + ' [BrokerID],'
				SET @SQL = @SQL + ' [BrokerName],'
				SET @SQL = @SQL + ' [Reason],'
				SET @SQL = @SQL + ' [ExecTime],'
				SET @SQL = @SQL + ' [VAT]'
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
				SET @SQL = @SQL + ' FROM [dbo].[CashAdvanceHistory]'
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

	

-- Drop the dbo.CashAdvanceHistory_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvanceHistory_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvanceHistory_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Inserts a record into the CashAdvanceHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvanceHistory_Insert
(

	@Id bigint    OUTPUT,

	@SubAccountId varchar (20)  ,

	@AdvanceDate datetime   ,

	@ContractNo varchar (50)  ,

	@OrderId int   ,

	@StockSymbol varchar (8)  ,

	@SellDueDate datetime   ,

	@CashDueDate datetime   ,

	@TotalSellValue decimal (18, 3)  ,

	@CashAvilable decimal (18, 3)  ,

	@CashRequest decimal (18, 3)  ,

	@Fee decimal (18, 3)  ,

	@CashReceived decimal (18, 3)  ,

	@Status int   ,

	@TradeType int   ,

	@BrokerId varchar (20)  ,

	@BrokerName varchar (50)  ,

	@Reason ntext   ,

	@ExecTime datetime   ,

	@Vat decimal (18, 3)  
)
AS


				
				INSERT INTO [dbo].[CashAdvanceHistory]
					(
					[SubAccountID]
					,[AdvanceDate]
					,[ContractNo]
					,[OrderID]
					,[StockSymbol]
					,[SellDueDate]
					,[CashDueDate]
					,[TotalSellValue]
					,[CashAvilable]
					,[CashRequest]
					,[Fee]
					,[CashReceived]
					,[Status]
					,[TradeType]
					,[BrokerID]
					,[BrokerName]
					,[Reason]
					,[ExecTime]
					,[VAT]
					)
				VALUES
					(
					@SubAccountId
					,@AdvanceDate
					,@ContractNo
					,@OrderId
					,@StockSymbol
					,@SellDueDate
					,@CashDueDate
					,@TotalSellValue
					,@CashAvilable
					,@CashRequest
					,@Fee
					,@CashReceived
					,@Status
					,@TradeType
					,@BrokerId
					,@BrokerName
					,@Reason
					,@ExecTime
					,@Vat
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

	

-- Drop the dbo.CashAdvanceHistory_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvanceHistory_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvanceHistory_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Updates a record in the CashAdvanceHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvanceHistory_Update
(

	@Id bigint   ,

	@SubAccountId varchar (20)  ,

	@AdvanceDate datetime   ,

	@ContractNo varchar (50)  ,

	@OrderId int   ,

	@StockSymbol varchar (8)  ,

	@SellDueDate datetime   ,

	@CashDueDate datetime   ,

	@TotalSellValue decimal (18, 3)  ,

	@CashAvilable decimal (18, 3)  ,

	@CashRequest decimal (18, 3)  ,

	@Fee decimal (18, 3)  ,

	@CashReceived decimal (18, 3)  ,

	@Status int   ,

	@TradeType int   ,

	@BrokerId varchar (20)  ,

	@BrokerName varchar (50)  ,

	@Reason ntext   ,

	@ExecTime datetime   ,

	@Vat decimal (18, 3)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[CashAdvanceHistory]
				SET
					[SubAccountID] = @SubAccountId
					,[AdvanceDate] = @AdvanceDate
					,[ContractNo] = @ContractNo
					,[OrderID] = @OrderId
					,[StockSymbol] = @StockSymbol
					,[SellDueDate] = @SellDueDate
					,[CashDueDate] = @CashDueDate
					,[TotalSellValue] = @TotalSellValue
					,[CashAvilable] = @CashAvilable
					,[CashRequest] = @CashRequest
					,[Fee] = @Fee
					,[CashReceived] = @CashReceived
					,[Status] = @Status
					,[TradeType] = @TradeType
					,[BrokerID] = @BrokerId
					,[BrokerName] = @BrokerName
					,[Reason] = @Reason
					,[ExecTime] = @ExecTime
					,[VAT] = @Vat
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CashAdvanceHistory_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvanceHistory_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvanceHistory_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Deletes a record in the CashAdvanceHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvanceHistory_Delete
(

	@Id bigint   
)
AS


				DELETE FROM [dbo].[CashAdvanceHistory] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CashAdvanceHistory_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvanceHistory_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvanceHistory_GetById
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Select records from the CashAdvanceHistory table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvanceHistory_GetById
(

	@Id bigint   
)
AS


				SELECT
					[ID],
					[SubAccountID],
					[AdvanceDate],
					[ContractNo],
					[OrderID],
					[StockSymbol],
					[SellDueDate],
					[CashDueDate],
					[TotalSellValue],
					[CashAvilable],
					[CashRequest],
					[Fee],
					[CashReceived],
					[Status],
					[TradeType],
					[BrokerID],
					[BrokerName],
					[Reason],
					[ExecTime],
					[VAT]
				FROM
					[dbo].[CashAdvanceHistory]
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

	

-- Drop the dbo.CashAdvanceHistory_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CashAdvanceHistory_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CashAdvanceHistory_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: Thursday, May 05, 2011

-- Created By: OTS Co., Ltd. (http://ots.vn)
-- Purpose: Finds records in the CashAdvanceHistory table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CashAdvanceHistory_Find
(

	@SearchUsingOR bit   = null ,

	@Id bigint   = null ,

	@SubAccountId varchar (20)  = null ,

	@AdvanceDate datetime   = null ,

	@ContractNo varchar (50)  = null ,

	@OrderId int   = null ,

	@StockSymbol varchar (8)  = null ,

	@SellDueDate datetime   = null ,

	@CashDueDate datetime   = null ,

	@TotalSellValue decimal (18, 3)  = null ,

	@CashAvilable decimal (18, 3)  = null ,

	@CashRequest decimal (18, 3)  = null ,

	@Fee decimal (18, 3)  = null ,

	@CashReceived decimal (18, 3)  = null ,

	@Status int   = null ,

	@TradeType int   = null ,

	@BrokerId varchar (20)  = null ,

	@BrokerName varchar (50)  = null ,

	@Reason ntext   = null ,

	@ExecTime datetime   = null ,

	@Vat decimal (18, 3)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [SubAccountID]
	, [AdvanceDate]
	, [ContractNo]
	, [OrderID]
	, [StockSymbol]
	, [SellDueDate]
	, [CashDueDate]
	, [TotalSellValue]
	, [CashAvilable]
	, [CashRequest]
	, [Fee]
	, [CashReceived]
	, [Status]
	, [TradeType]
	, [BrokerID]
	, [BrokerName]
	, [Reason]
	, [ExecTime]
	, [VAT]
    FROM
	[dbo].[CashAdvanceHistory]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([SubAccountID] = @SubAccountId OR @SubAccountId IS NULL)
	AND ([AdvanceDate] = @AdvanceDate OR @AdvanceDate IS NULL)
	AND ([ContractNo] = @ContractNo OR @ContractNo IS NULL)
	AND ([OrderID] = @OrderId OR @OrderId IS NULL)
	AND ([StockSymbol] = @StockSymbol OR @StockSymbol IS NULL)
	AND ([SellDueDate] = @SellDueDate OR @SellDueDate IS NULL)
	AND ([CashDueDate] = @CashDueDate OR @CashDueDate IS NULL)
	AND ([TotalSellValue] = @TotalSellValue OR @TotalSellValue IS NULL)
	AND ([CashAvilable] = @CashAvilable OR @CashAvilable IS NULL)
	AND ([CashRequest] = @CashRequest OR @CashRequest IS NULL)
	AND ([Fee] = @Fee OR @Fee IS NULL)
	AND ([CashReceived] = @CashReceived OR @CashReceived IS NULL)
	AND ([Status] = @Status OR @Status IS NULL)
	AND ([TradeType] = @TradeType OR @TradeType IS NULL)
	AND ([BrokerID] = @BrokerId OR @BrokerId IS NULL)
	AND ([BrokerName] = @BrokerName OR @BrokerName IS NULL)
	AND ([ExecTime] = @ExecTime OR @ExecTime IS NULL)
	AND ([VAT] = @Vat OR @Vat IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [SubAccountID]
	, [AdvanceDate]
	, [ContractNo]
	, [OrderID]
	, [StockSymbol]
	, [SellDueDate]
	, [CashDueDate]
	, [TotalSellValue]
	, [CashAvilable]
	, [CashRequest]
	, [Fee]
	, [CashReceived]
	, [Status]
	, [TradeType]
	, [BrokerID]
	, [BrokerName]
	, [Reason]
	, [ExecTime]
	, [VAT]
    FROM
	[dbo].[CashAdvanceHistory]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([SubAccountID] = @SubAccountId AND @SubAccountId is not null)
	OR ([AdvanceDate] = @AdvanceDate AND @AdvanceDate is not null)
	OR ([ContractNo] = @ContractNo AND @ContractNo is not null)
	OR ([OrderID] = @OrderId AND @OrderId is not null)
	OR ([StockSymbol] = @StockSymbol AND @StockSymbol is not null)
	OR ([SellDueDate] = @SellDueDate AND @SellDueDate is not null)
	OR ([CashDueDate] = @CashDueDate AND @CashDueDate is not null)
	OR ([TotalSellValue] = @TotalSellValue AND @TotalSellValue is not null)
	OR ([CashAvilable] = @CashAvilable AND @CashAvilable is not null)
	OR ([CashRequest] = @CashRequest AND @CashRequest is not null)
	OR ([Fee] = @Fee AND @Fee is not null)
	OR ([CashReceived] = @CashReceived AND @CashReceived is not null)
	OR ([Status] = @Status AND @Status is not null)
	OR ([TradeType] = @TradeType AND @TradeType is not null)
	OR ([BrokerID] = @BrokerId AND @BrokerId is not null)
	OR ([BrokerName] = @BrokerName AND @BrokerName is not null)
	OR ([ExecTime] = @ExecTime AND @ExecTime is not null)
	OR ([VAT] = @Vat AND @Vat is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO
