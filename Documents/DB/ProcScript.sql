create proc HV_InsertHoseMarketInfo
		@TradeDate DateTime,
        @SetIndex bigint,
        @TotalTrade bigint,
        @Totalshare bigint,
        @TotalValue bigint,
        @UpVolume bigint,
        @DownVolume bigint,
        @NoChangeVolume bigint,
        @Advances bigint,
        @Declines bigint,
        @Nochange bigint,
        @Status char(1)
as
begin
	 declare @query varchar(max)
	 set @query = 'insert into totalmarket(TradeDate,SetIndex,TotalTrade,TotalShare,TotalValue,UpVolume,DownVolume,NoChangeVolume,
	 Advances,Declines,NoChange,Status) values('+@TradeDate+','+@SetIndex+','+@TotalTrade+','+@Totalshare+','
	 +@TotalValue+','+@UpVolume+','+@DownVolume+','+@NoChangeVolume+','+@Advances+','+@Declines+','+@Nochange+''','+@Status+''''
	 exec sp_sqlexec @query  
end

create proc HV_InsertHNXMarketInfo
		@TradeDate DateTime,
        @SetIndex float,
        @TotalTrade bigint,
        @Totalshare bigint,
        @TotalValue bigint,
        @Advances smallint,
        @Declines smallint,
        @Nochange smallint,
        @OpenIndex float,
        @Status char(1)
as
begin
	 declare @query varchar(max)
	 set @query = 'insert into hastc_market(TradeDate,SetIndex,TotalTrade,TotalShare,TotalValue,
	 Advances,Declines,NoChange,Status,OpenIndex) values('+@TradeDate+','+@SetIndex+','+@TotalTrade+','+@Totalshare+','
	 +@TotalValue+','+@Advances+','+@Declines+','+@Nochange+''','+@Status+''','+@OpenIndex
	 exec sp_sqlexec @query  
end

create proc HV_InsertUpcomMarketInfo
		@TradeDate DateTime,
        @SetIndex float,
        @TotalTrade bigint,
        @Totalshare bigint,
        @TotalValue bigint,
        @Advances smallint,
        @Declines smallint,
        @Nochange smallint,
        @OpenIndex float,
        @Status char(1)
as
begin
	 declare @query varchar(max)
	 set @query = 'insert into hastc_market(TradeDate,SetIndex,TotalTrade,TotalShare,TotalValue,
	 Advances,Declines,NoChange,Status,OpenIndex) values('+@TradeDate+','+@SetIndex+','+@TotalTrade+','+@Totalshare+','
	 +@TotalValue+','+@Advances+','+@Declines+','+@Nochange+''','+@Status+''','+@OpenIndex
	 exec sp_sqlexec @query  
end

USE [RTStockDataTest]
GO
/****** Object:  StoredProcedure [dbo].[HV_InsertHoseStockInfo]    Script Date: 09/03/2013 22:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[HV_InsertHoseStockInfo]
@StockSymbol VARCHAR(20),
@TradeDate CHAR(25),
@Ceiling CHAR(15),
@Floor CHAR(15),
@AvrPrice CHAR(15),
@Last  CHAR(15),
@LastVol CHAR(15),
@LastVal CHAR(15),
@Highest CHAR(15),
@Lowest CHAR(15),
@Totalshares CHAR(15),
@TotalValue char(15),
@Best1Bid CHAR(15),
@Best1BidVolume CHAR(15),
@Best2Bid CHAR(15),
@Best2BidVolume CHAR(15),
@Best3Bid CHAR(15),
@Best3BidVolume CHAR(15),
@Best1Offer CHAR(15),
@Best1OfferVolume CHAR(15),
@Best2Offer CHAR(15),
@Best2OfferVolume CHAR(15),
@Best3Offer CHAR(15),
@Best3OfferVolume CHAR(15),
@CurrentRoom CHAR(15),
@StartRoom CHAR(15)
 AS 
 BEGIN
 DECLARE @query VARCHAR(MAX)
 IF(EXISTS(SELECT StockSymbol FROM dbo.security_realtime WHERE StockSymbol=@StockSymbol))
begin
SET @query = 'Update security_realtime set TradeDate='''+@TradeDate+''',Ceiling='+@Ceiling+',Floor='+@Floor+',AvrPrice='+@AvrPrice+',Last='+@Last+',LastVol
 ='+@LastVol+',LastVal='+@LastVal+',Highest='+@Highest+',Lowest='+@Lowest+',Totalshares='+@Totalshares+',TotalValue='+@TotalValue+',Best1Bid='+@Best1Bid+',Best1BidVolume='+@Best1BidVolume+',Best2Bid='+@Best2Bid+',Best2BidVolume
='+@Best2BidVolume+' ,Best3Bid='+@Best3Bid+',Best3BidVolume='+@Best3BidVolume+',Best1Offer='+@Best1Offer+',Best1OfferVolume='+@Best1OfferVolume+',Best2Offer='+@Best2Offer+',Best2OfferVolume='+@Best2OfferVolume+',Best3Offer='+@Best3Offer+',Best3OfferVolume
 ='+@Best3OfferVolume+',CurrentRoom='+@CurrentRoom+',StartRoom='+@StartRoom+' where StockSymbol='''+@StockSymbol+'''';
END
ELSE
begin
 
 SET @query = 'Insert into security_realtime(StockSymbol,TradeDate,Ceiling,Floor,AvrPrice,Last,LastVol
 ,LastVal,Highest,Lowest,Totalshares,TotalValue,Best1Bid,Best1BidVolume,Best2Bid,Best2BidVolume
 ,Best3Bid,Best3BidVolume,Best1Offer,Best1OfferVolume,Best2Offer,Best2OfferVolume,Best3Offer,Best3OfferVolume
 ,CurrentRoom,StartRoom) values('''+@StockSymbol+''','+@TradeDate+''','+@Ceiling+','+@Floor+','+@AvrPrice+','+@Last+','+@LastVol
 +','+@LastVal+','+@Highest+','+@Lowest+','+@Totalshares+','+@TotalValue+','+@Best1Bid+','+@Best1BidVolume+','+@Best2Bid+','+@Best2BidVolume
 +','+@Best3Bid+','+@Best3BidVolume+','+@Best1Offer+','+@Best1OfferVolume+','+@Best2Offer+','+@Best2OfferVolume+','+@Best3Offer+','+@Best3OfferVolume
 +','+@CurrentRoom+','+@StartRoom+')'
 
 END
 exec sp_sqlexec @query  
 end
 
 USE [RTStockDataTest]
GO
/****** Object:  StoredProcedure [dbo].[HV_InserHNXStockInfo]    Script Date: 09/03/2013 22:04:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[HV_InserHNXStockInfo]
@StockSymbol VARCHAR(20),
@TradeDate CHAR(25),
@Ceiling CHAR(15),
@Floor CHAR(15),
@AvrPrice CHAR(15),
@Last  CHAR(15),
@LastVol CHAR(15),
@LastVal CHAR(15),
@Highest CHAR(15),
@Lowest CHAR(15),
@Totalshares CHAR(15),
@TotalValue char(15),
@Best1Bid CHAR(15),
@Best1BidVolume CHAR(15),
@Best2Bid CHAR(15),
@Best2BidVolume CHAR(15),
@Best3Bid CHAR(15),
@Best3BidVolume CHAR(15),
@Best1Offer CHAR(15),
@Best1OfferVolume CHAR(15),
@Best2Offer CHAR(15),
@Best2OfferVolume CHAR(15),
@Best3Offer CHAR(15),
@Best3OfferVolume CHAR(15),
@SELL_FOREIGN_QTTY CHAR(15),
@BUY_FOREIGN_QTTY CHAR(15)
 AS 
 BEGIN
 DECLARE @query VARCHAR(MAX)
 IF(EXISTS(SELECT StockSymbol FROM dbo.hastc_stocks WHERE StockSymbol=@StockSymbol))
begin
SET @query = 'Update hastc_stocks set TradeDate='''+@TradeDate+''',Ceiling='+@Ceiling+',Floor='+@Floor+',AVERAGE_PRICE='+@AvrPrice+',Last='+@Last+',LastVol
 ='+@LastVol+',LastVal='+@LastVal+',Highest='+@Highest+',Lowest='+@Lowest+',Totalshares='+@Totalshares+',TotalValue='+@TotalValue+',Best1Bid='+@Best1Bid+',Best1BidVolume='+@Best1BidVolume+',Best2Bid='+@Best2Bid+',Best2BidVolume
='+@Best2BidVolume+' ,Best3Bid='+@Best3Bid+',Best3BidVolume='+@Best3BidVolume+',Best1Offer='+@Best1Offer+',Best1OfferVolume='+@Best1OfferVolume+',Best2Offer='+@Best2Offer+',Best2OfferVolume='+@Best2OfferVolume+',Best3Offer='+@Best3Offer+',Best3OfferVolume
 ='+@Best3OfferVolume+',SELL_FOREIGN_QTTY='+@SELL_FOREIGN_QTTY+',BUY_FOREIGN_QTTY='+@BUY_FOREIGN_QTTY+' where StockSymbol='''+@StockSymbol+'''';
END
ELSE
begin
 
 SET @query = 'Insert into hastc_stocks(StockSymbol,TradeDate,Ceiling,Floor,AVERAGE_PRICE,Last,LastVol
 ,LastVal,Highest,Lowest,Totalshares,TotalValue,Best1Bid,Best1BidVolume,Best2Bid,Best2BidVolume
 ,Best3Bid,Best3BidVolume,Best1Offer,Best1OfferVolume,Best2Offer,Best2OfferVolume,Best3Offer,Best3OfferVolume
 ,SELL_FOREIGN_QTTY,BUY_FOREIGN_QTTY) values('''+@StockSymbol+''','+@TradeDate+''','+@Ceiling+','+@Floor+','+@AvrPrice+','+@Last+','+@LastVol
 +','+@LastVal+','+@Highest+','+@Lowest+','+@Totalshares+','+@TotalValue+','+@Best1Bid+','+@Best1BidVolume+','+@Best2Bid+','+@Best2BidVolume
 +','+@Best3Bid+','+@Best3BidVolume+','+@Best1Offer+','+@Best1OfferVolume+','+@Best2Offer+','+@Best2OfferVolume+','+@Best3Offer+','+@Best3OfferVolume
 +','+@SELL_FOREIGN_QTTY+','+@BUY_FOREIGN_QTTY+')'
 
 END
 exec sp_sqlexec @query  
 end


USE [RTStockDataTest]
GO
/****** Object:  StoredProcedure [dbo].[HV_InsertUpComStockInfo]    Script Date: 09/03/2013 21:40:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[HV_InsertUpComStockInfo]
@StockSymbol VARCHAR(25),
@TradeDate CHAR(25),
@Ceiling CHAR(15),
@Floor CHAR(15),
@AvrPrice CHAR(15),
@Last  CHAR(15),
@LastVol CHAR(15),
@LastVal CHAR(15),
@Highest CHAR(15),
@Lowest CHAR(15),
@Totalshares CHAR(15),
@TotalValue char(15),
@Best1Bid CHAR(15),
@Best1BidVolume CHAR(15),
@Best2Bid CHAR(15),
@Best2BidVolume CHAR(15),
@Best3Bid CHAR(15),
@Best3BidVolume CHAR(15),
@Best1Offer CHAR(15),
@Best1OfferVolume CHAR(15),
@Best2Offer CHAR(15),
@Best2OfferVolume CHAR(15),
@Best3Offer CHAR(15),
@Best3OfferVolume CHAR(15),
@SELL_FOREIGN_QTTY CHAR(15),
@BUY_FOREIGN_QTTY CHAR(15)
 AS 
 BEGIN
 DECLARE @query VARCHAR(MAX)
 IF(EXISTS(SELECT StockSymbol FROM dbo.upcom_stocks WHERE StockSymbol=@StockSymbol))
begin
SET @query = 'Update upcom_stocks set TradeDate='''+@TradeDate+''',Ceiling='+@Ceiling+',Floor='+@Floor+',AVERAGE_PRICE='+@AvrPrice+',Last='+@Last+',LastVol
 ='+@LastVol+',LastVal='+@LastVal+',Highest='+@Highest+',Lowest='+@Lowest+',Totalshares='+@Totalshares+',TotalValue='+@TotalValue+',Best1Bid='+@Best1Bid+',Best1BidVolume='+@Best1BidVolume+',Best2Bid='+@Best2Bid+',Best2BidVolume
='+@Best2BidVolume+' ,Best3Bid='+@Best3Bid+',Best3BidVolume='+@Best3BidVolume+',Best1Offer='+@Best1Offer+',Best1OfferVolume='+@Best1OfferVolume+',Best2Offer='+@Best2Offer+',Best2OfferVolume='+@Best2OfferVolume+',Best3Offer='+@Best3Offer+',Best3OfferVolume
 ='+@Best3OfferVolume+',SELL_FOREIGN_QTTY='+@SELL_FOREIGN_QTTY+',BUY_FOREIGN_QTTY='+@BUY_FOREIGN_QTTY+' where StockSymbol='''+@StockSymbol+'''';
END
ELSE
begin
 
 SET @query = 'Insert into upcom_stocks (StockSymbol,TradeDate,Ceiling,Floor,AVERAGE_PRICE,Last,LastVol
 ,LastVal,Highest,Lowest,Totalshares,TotalValue,Best1Bid,Best1BidVolume,Best2Bid,Best2BidVolume
 ,Best3Bid,Best3BidVolume,Best1Offer,Best1OfferVolume,Best2Offer,Best2OfferVolume,Best3Offer,Best3OfferVolume
 ,SELL_FOREIGN_QTTY,BUY_FOREIGN_QTTY) values('''+@StockSymbol+''','''+@TradeDate+''','+@Ceiling+','+@Floor+','+@AvrPrice+','+@Last+','+@LastVol
 +','+@LastVal+','+@Highest+','+@Lowest+','+@Totalshares+','+@TotalValue+','+@Best1Bid+','+@Best1BidVolume+','+@Best2Bid+','+@Best2BidVolume
 +','+@Best3Bid+','+@Best3BidVolume+','+@Best1Offer+','+@Best1OfferVolume+','+@Best2Offer+','+@Best2OfferVolume+','+@Best3Offer+','+@Best3OfferVolume
 +','+@SELL_FOREIGN_QTTY+','+@BUY_FOREIGN_QTTY+')'
 
 END
 exec sp_sqlexec @query  
 end



