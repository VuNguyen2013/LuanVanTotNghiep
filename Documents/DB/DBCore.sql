create table Market
(
	Id int primary key,
	Name nvarchar(10) not null
)

CREATE TABLE [dbo].[CompanyInfo](
	[Code] [varchar](10) primary key,
	[ShortNameVi] [nvarchar](500) NULL,
	[ShortNameEn] [nvarchar](500) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Website] [nvarchar](50) NULL,
	[IsPublished] [bit] NULL,
	[MarketId] [int] foreign key references Market(Id)
)

CREATE TABLE [dbo].[HoseMarketInfoHis](
	[id] [bigint] IDENTITY(1,1) primary key,
	[TradeDate] [datetime] NULL,
	[SetIndex] [bigint] NULL,
	[TotalTrade] [bigint] NULL,
	[Totalshare] [bigint] NULL,
	[TotalValue] [bigint] NULL,
	[UpVolume] [bigint] NULL,
	[DownVolume] [bigint] NULL,
	[NoChangeVolume] [bigint] NULL,
	[Advances] [bigint] NULL,
	[Declines] [bigint] NULL,
	[Nochange] [bigint] NULL,
	[Marketid] int foreign key references Market(Id),
	[Status] [char](1) NULL
)

CREATE TABLE [dbo].[HNXMarketInfoHis](
	[id] [bigint] IDENTITY(1,1) primary key,
	[TradeDate] [datetime] NULL,
	[SetIndex] [float] NULL,
	[TotalTrade] [bigint] NULL,
	[Totalshare] [bigint] NULL,
	[TotalValue] [bigint] NULL,
	[Advances] [smallint] NULL,
	[Nochange] [smallint] NULL,
	[Declines] [smallint] NULL,
	[OpenIndex] [float] NULL,
	[Marketid] int foreign key references Market(Id),
)
CREATE TABLE [dbo].[UpComMarketInfoHis](
	[id] [bigint] IDENTITY(1,1) primary key,
	[TradeDate] [datetime] NULL,
	[SetIndex] [float] NULL,
	[TotalTrade] [bigint] NULL,
	[Totalshare] [bigint] NULL,
	[TotalValue] [bigint] NULL,
	[Advances] [smallint] NULL,
	[Nochange] [smallint] NULL,
	[Declines] [smallint] NULL,
	[OpenIndex] [float] NULL,
	[Marketid] int foreign key references Market(Id),
)

CREATE TABLE [dbo].[MemberStockCompany](
	[Id] [varchar](10) primary key,
	[ShortNameVi] [nvarchar](500) NULL,
	[ShortNameEn] [nvarchar](500) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Website] [nvarchar](50) NULL,
	[IsPublished] [bit] NULL
)