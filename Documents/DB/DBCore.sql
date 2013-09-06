create database DataStockCore

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
	[IsPublished] [bit] not null,
	[MarketId] [int] foreign key references Market(Id)
)

CREATE TABLE [dbo].[HoseMarketInfoHist](
	[id] [bigint] IDENTITY(1,1) primary key,
	[TradeDate] [datetime] not null default getdate(),
	[SetIndex] [bigint]  not null default 0,
	[TotalTrade] [bigint]  not null default 0,
	[Totalshare] [bigint]  not null default 0,
	[TotalValue] [bigint]  not null default 0,
	[UpVolume] [bigint]  not null default 0,
	[DownVolume] [bigint]  not null default 0,
	[NoChangeVolume] [bigint]  not null default 0,
	[Advances] [bigint]  not null default 0,
	[Declines] [bigint]  not null default 0,
	[Nochange] [bigint]  not null default 0,
	[MarketId] int foreign key references Market(Id),
	[Status] [char](1) NULL
)

CREATE TABLE [dbo].[HNXMarketInfoHist](
	[id] [bigint] IDENTITY(1,1) primary key,
	[TradeDate] [datetime] not null default getdate(),
	[SetIndex] [float] not null default 0,
	[TotalTrade] [bigint] not null default 0,
	[Totalshare] [bigint] not null default 0,
	[TotalValue] [bigint] not null default 0,
	[Advances] [smallint] not null default 0,
	[Nochange] [smallint] not null default 0,
	[Declines] [smallint] not null default 0,
	[OpenIndex] [float] not null default 0,
	[MarketId] int foreign key references Market(Id),
	[Status] [char](1) NULL
)
CREATE TABLE [dbo].[UpComMarketInfoHist](
	[id] [bigint] IDENTITY(1,1) primary key,
	[TradeDate] [datetime] not null default getdate(),
	[SetIndex] [float]  not null default 0,
	[TotalTrade] [bigint]  not null default 0,
	[Totalshare] [bigint]  not null default 0,
	[TotalValue] [bigint]  not null default 0,
	[Advances] [smallint]  not null default 0,
	[Nochange] [smallint]  not null default 0,
	[Declines] [smallint]  not null default 0,
	[OpenIndex] [float]  not null default 0,
	[Marketid] int foreign key references Market(Id),
	[Status] [char](1) NULL
)

CREATE TABLE [dbo].[MemberStockCompany](
	[Id] [varchar](10) primary key,
	[ShortNameVi] [nvarchar](500) NULL,
	[ShortNameEn] [nvarchar](500) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Website] [nvarchar](50) NULL,
	[IsPublished] [bit] NOT NULL
)
create table HoseStockInfoHist
(
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[TradeDate] [datetime] not null default getdate(),
	[StockSymbol] [varchar](10) foreign key references [CompanyInfo](Code),
	[Ceiling] [bigint] not null default 0,
	[Floor] [bigint] not null default 0,	
	[AvrPrice] [bigint] not null default 0,
	[Last] [bigint] not null default 0,
	[LastVol] [bigint] not null default 0,
	[LastVal] [bigint] not null default 0,
	[Highest] [bigint] not null default 0,
	[Lowest] [bigint] not null default 0,
	[Totalshares] [bigint] not null default 0,
	[TotalValue] [bigint] not null default 0,
	[Best1Bid] [bigint] not null default 0,
	[Best1BidVolume] [bigint] not null default 0,
	[Best2Bid] [bigint] not null default 0,
	[Best2BidVolume] [bigint] not null default 0,
	[Best3Bid] [bigint] not null default 0,
	[Best3BidVolume] [bigint] not null default 0,
	[Best1Offer] [bigint] not null default 0,
	[Best1OfferVolume] [bigint] not null default 0,
	[Best2Offer] [bigint] not null default 0,
	[Best2OfferVolume] [bigint] not null default 0,
	[Best3Offer] [bigint] not null default 0,
	[Best3OfferVolume] [bigint] not null default 0,
	[CurrentRoom] [bigint] not null default 0,
	[StartRoom] [bigint] not null default 0,
)

create table HNXStockInfoHist
(
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[TradeDate] [datetime] not null default getdate(),
	[StockSymbol] [varchar](10) foreign key references [CompanyInfo](Code),
	[Ceiling] [bigint] not null default 0,
	[Floor] [bigint] not null default 0,	
	[AvrPrice] [bigint] not null default 0,
	[Last] [bigint] not null default 0,
	[LastVol] [bigint] not null default 0,
	[LastVal] [bigint] not null default 0,
	[Highest] [bigint] not null default 0,
	[Lowest] [bigint] not null default 0,
	[Totalshares] [bigint] not null default 0,
	[TotalValue] [bigint] not null default 0,
	[Best1Bid] [bigint] not null default 0,
	[Best1BidVolume] [bigint] not null default 0,
	[Best2Bid] [bigint] not null default 0,
	[Best2BidVolume] [bigint] not null default 0,
	[Best3Bid] [bigint] not null default 0,
	[Best3BidVolume] [bigint] not null default 0,
	[Best1Offer] [bigint] not null default 0,
	[Best1OfferVolume] [bigint] not null default 0,
	[Best2Offer] [bigint] not null default 0,
	[Best2OfferVolume] [bigint] not null default 0,
	[Best3Offer] [bigint] not null default 0,
	[Best3OfferVolume] [bigint] not null default 0,
	[SELL_FOREIGN_QTTY] [bigint] not null default 0,
	[BUY_FOREIGN_QTTY] [bigint] not null default 0,
)

create table UpComStockInfoHist
(
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[TradeDate] [datetime] not null default getdate(),
	[StockSymbol] [varchar](10) foreign key references [CompanyInfo](Code),
	[Ceiling] [bigint] not null default 0,
	[Floor] [bigint] not null default 0,	
	[AvrPrice] [bigint] not null default 0,
	[Last] [bigint] not null default 0,
	[LastVol] [bigint] not null default 0,
	[LastVal] [bigint] not null default 0,
	[Highest] [bigint] not null default 0,
	[Lowest] [bigint] not null default 0,
	[Totalshares] [bigint] not null default 0,
	[TotalValue] [bigint] not null default 0,
	[Best1Bid] [bigint] not null default 0,
	[Best1BidVolume] [bigint] not null default 0,
	[Best2Bid] [bigint] not null default 0,
	[Best2BidVolume] [bigint] not null default 0,
	[Best3Bid] [bigint] not null default 0,
	[Best3BidVolume] [bigint] not null default 0,
	[Best1Offer] [bigint] not null default 0,
	[Best1OfferVolume] [bigint] not null default 0,
	[Best2Offer] [bigint] not null default 0,
	[Best2OfferVolume] [bigint] not null default 0,
	[Best3Offer] [bigint] not null default 0,
	[Best3OfferVolume] [bigint] not null default 0,
	[SELL_FOREIGN_QTTY] [bigint] not null default 0,
	[BUY_FOREIGN_QTTY] [bigint] not null default 0,
)
CREATE TABLE [dbo].[MainCustAccount](
	[MainCustAccountID] [varchar](20) NOT NULL primary key,
	[FullName] [nvarchar](50) NULL,
	[Email] [varchar](30) NULL,
	[Phone] [varchar](20) NULL,
	[Actived] [bit] NOT NULL,
	[Password] [varchar](50) NULL,
	[PIN] [varchar](50) NULL,
	[PassLockReason] [int] NULL,
	[PINLockReason] [int] NULL,
	[LockReason] [int] NULL,
	[TokenID] [varchar](20) NULL,
	[TokenName] [varchar](20) NULL,
	[TokenActived] [varchar](10) NULL,
	[BrokerID] [varchar](20) NULL,
	[PassIsNew] [bit] NULL,
	[PINIsNew] [bit] NULL,
	[PassExpDate] [datetime] NULL,
	[PINExpDate] [datetime] NULL,
	[CustomerType] [int] NOT NULL,
	[AuthType] [smallint] NOT NULL,
	[PinType] [smallint] NOT NULL,
	[LanguageId] [varchar](10) NOT NULL,
	[FailedLoginCount] [int] NULL,
	[FailedLoginTime] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUser] [varchar](20) NOT NULL,
	[UpdatedUser] [varchar](20) NULL,
	[UpdatedDate] [datetime] NULL,
)
CREATE TABLE [dbo].[SubCustAccount](
	[SubCustAccountID] [varchar](20) primary key,
	[Name] [nvarchar](50) NULL,
	[Actived] [bit] NULL,
	[LockAccountReason] [smallint] NULL,
	[MainCustAccountID] [varchar](20) foreign key references [MainCustAccount]([MainCustAccountID]),
	[CreatedDate] [datetime] NULL,
	[CreatedUser] [varchar](20) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedUser] [varchar](20) NULL,
	WithDraw bigint not null default 0,
	BuyCredit bigint not null default 0,
	TotalBuy bigint not null default 0,
	TotalSell bigint not null default 0,
	)
create table StockBalance
(
	[SubCustAccountID] varchar(20) foreign key references [SubCustAccount]([SubCustAccountID]),
	[StockSymbol] varchar(10)foreign key references CompanyInfo(Code),
	Available bigint not null default 0,
	Total bigint not null default 0,
	WTR_T1 bigint not null default 0,
	WTR_T2 bigint not null default 0,
	WTS_T1 bigint not null default 0,
	WTS_T2 bigint not null default 0,
	primary key([SubCustAccountID],[StockSymbol])
)
create table [Matched]
(
	OrderSellID BIGINT NOT NULL FOREIGN KEY REFERENCES  [Order](Id),
	OrderBuyID BIGINT NOT NULL FOREIGN KEY REFERENCES  [Order](Id),
	MatchedPrice bigint not null,
	MatchedVol smallint not null,
	DateMatched datetime not null default getdate()
)

CREATE TABLE [Order]
(
	Id BIGINT IDENTITY(1,1) PRIMARY KEY,
	AccountNo VARCHAR(20) FOREIGN KEY REFERENCES dbo.SubCustAccount(SubCustAccountID),
	StockSymbol VARCHAR(10) FOREIGN KEY REFERENCES dbo.CompanyInfo(Code),
	Price BIGINT NOT NULL,
	Volume SMALLINT NOT NULL,
	MatchedVol SMALLINT NOT NULL DEFAULT 0,
	Side CHAR(1) NOT NULL,
	TradeDate DATETIME NOT NULL DEFAULT GETDATE(),
	[Status] SMALLINT NOT NULL,
)

CREATE TABLE CashTempDeduction
(
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	AccountNo VARCHAR(20) FOREIGN KEY REFERENCES dbo.SubCustAccount(SubCustAccountID),
	Amount BIGINT NOT NULL DEFAULT 0,
	DeductedDate DATETIME NOT NULL DEFAULT GETDATE(),
	[Status] SMALLINT NOT NULL,
	IsAdd BIT NOT NULL
)
CREATE TABLE StockTempDeduction
(
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	AccountNo VARCHAR(20) FOREIGN KEY REFERENCES dbo.SubCustAccount(SubCustAccountID),
	StockSymbol VARCHAR(10) FOREIGN KEY REFERENCES dbo.CompanyInfo(Code),
	Volume BIGINT NOT NULL DEFAULT 0,
	DeductedDate DATETIME NOT NULL DEFAULT GETDATE(),
	[Status] SMALLINT NOT NULL,
	IsAdd BIT NOT NULL
)
