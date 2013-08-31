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

