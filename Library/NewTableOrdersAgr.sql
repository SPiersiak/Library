USE [Library]
GO

/****** Object:  Table [dbo].[OrderAgr]    Script Date: 15.03.2024 11:30:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderAgr](
	[TotalOrderQuantity] [bigint] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderAgr] ADD  CONSTRAINT [DF_OrderAgr_TotalOrderQuantity]  DEFAULT ((0)) FOR [TotalOrderQuantity]
GO


