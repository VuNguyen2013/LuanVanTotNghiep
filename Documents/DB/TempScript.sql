
insert into CompanyInfo values('BCE',N'CTCP XD & GT BINH DUONG ',N'',N'','','','','1',1)

INSERT into [dbo].[HoseStockInfo] ( [TradeDate], [StockSymbol], [Ceiling], [Floor],[AvrPrice], 
[Last], [LastVol], [LastVal], [Highest], [Lowest], [Totalshares], [TotalValue],
 [Best1Bid], [Best1BidVolume], [Best2Bid], [Best2BidVolume], [Best3Bid], [Best3BidVolume],
  [Best1Offer], [Best1OfferVolume], [Best2Offer], [Best2OfferVolume], [Best3Offer], [Best3OfferVolume], 
   [CurrentRoom], [StartRoom]) values(GETDATE(),'BCE',9200,8400,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,14433644,14433644)