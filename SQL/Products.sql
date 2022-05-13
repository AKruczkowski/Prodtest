USE [TestingProducts]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 11.05.2022 09:50:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products](
	[Product_ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [decimal](10, 2) NULL,
	[Width] [decimal](10, 2) NULL,
	[Length] [decimal](10, 2) NULL,
	[Height] [decimal](10, 2) NULL,
	[Date] [datetime] NULL,
	[Description] [nvarchar](50) NULL,
	[Category] [nvarchar](50) NULL,
	[ShippingPrice] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Product_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


