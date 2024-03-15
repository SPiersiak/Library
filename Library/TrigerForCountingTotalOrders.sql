USE [Library]
GO

/****** Object:  Trigger [dbo].[UpdateTotalQuantityInOrderAgr]    Script Date: 15.03.2024 11:32:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[UpdateTotalQuantityInOrderAgr] ON [dbo].[Orders]
AFTER INSERT
NOT FOR REPLICATION 
AS
 
BEGIN
  update OrderAgr
	set TotalOrderQuantity = TotalOrderQuantity + 1;

END
GO

ALTER TABLE [dbo].[Orders] ENABLE TRIGGER [UpdateTotalQuantityInOrderAgr]
GO


