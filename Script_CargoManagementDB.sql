CREATE DATABASE CargoManagementDB
GO

USE [CargoManagementDB]
GO
/****** Object:  Table [dbo].[CarrierConfigurations]    Script Date: 6/7/2023 1:35:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarrierConfigurations](
	[CarrierId] [int] NOT NULL,
	[CarrierMaxDesi] [int] NOT NULL,
	[CarrierMinDesi] [int] NOT NULL,
	[CarrierCost] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_CarrierConfigurations] PRIMARY KEY CLUSTERED 
(
	[CarrierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarrierReports]    Script Date: 6/7/2023 1:35:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarrierReports](
	[CarrierReportId] [int] IDENTITY(1,1) NOT NULL,
	[CarrierId] [int] NOT NULL,
	[CarrierCost] [decimal](18, 2) NOT NULL,
	[CarrierReportDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CarrierReports] PRIMARY KEY CLUSTERED 
(
	[CarrierReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carriers]    Script Date: 6/7/2023 1:35:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carriers](
	[CarrierId] [int] IDENTITY(1,1) NOT NULL,
	[CarrierName] [nvarchar](50) NOT NULL,
	[CarrierIsActive] [bit] NOT NULL,
	[CarrierPlusDesiCost] [int] NOT NULL,
 CONSTRAINT [PK_Carriers] PRIMARY KEY CLUSTERED 
(
	[CarrierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 6/7/2023 1:35:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[CarrierId] [int] NOT NULL,
	[OrderDesi] [int] NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[OrderCarrierCost] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CarrierConfigurations] ([CarrierId], [CarrierMaxDesi], [CarrierMinDesi], [CarrierCost]) VALUES (1, 30, 15, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[CarrierConfigurations] ([CarrierId], [CarrierMaxDesi], [CarrierMinDesi], [CarrierCost]) VALUES (2, 45, 10, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[CarrierConfigurations] ([CarrierId], [CarrierMaxDesi], [CarrierMinDesi], [CarrierCost]) VALUES (3, 55, 15, CAST(6.00 AS Decimal(18, 2)))
INSERT [dbo].[CarrierConfigurations] ([CarrierId], [CarrierMaxDesi], [CarrierMinDesi], [CarrierCost]) VALUES (4, 35, 20, CAST(10.00 AS Decimal(18, 2)))
INSERT [dbo].[CarrierConfigurations] ([CarrierId], [CarrierMaxDesi], [CarrierMinDesi], [CarrierCost]) VALUES (5, 100, 0, CAST(1.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[CarrierReports] ON 

INSERT [dbo].[CarrierReports] ([CarrierReportId], [CarrierId], [CarrierCost], [CarrierReportDate]) VALUES (17, 2, CAST(4.00 AS Decimal(18, 2)), CAST(N'2023-06-07T01:13:10.5919418' AS DateTime2))
INSERT [dbo].[CarrierReports] ([CarrierReportId], [CarrierId], [CarrierCost], [CarrierReportDate]) VALUES (18, 3, CAST(212.00 AS Decimal(18, 2)), CAST(N'2023-06-07T01:13:10.8134875' AS DateTime2))
INSERT [dbo].[CarrierReports] ([CarrierReportId], [CarrierId], [CarrierCost], [CarrierReportDate]) VALUES (19, 5, CAST(1.00 AS Decimal(18, 2)), CAST(N'2023-06-07T01:13:10.8190697' AS DateTime2))
INSERT [dbo].[CarrierReports] ([CarrierReportId], [CarrierId], [CarrierCost], [CarrierReportDate]) VALUES (20, 2, CAST(4.00 AS Decimal(18, 2)), CAST(N'2023-06-07T01:16:09.5735773' AS DateTime2))
INSERT [dbo].[CarrierReports] ([CarrierReportId], [CarrierId], [CarrierCost], [CarrierReportDate]) VALUES (21, 3, CAST(212.00 AS Decimal(18, 2)), CAST(N'2023-06-07T01:16:09.5812772' AS DateTime2))
INSERT [dbo].[CarrierReports] ([CarrierReportId], [CarrierId], [CarrierCost], [CarrierReportDate]) VALUES (22, 5, CAST(1.00 AS Decimal(18, 2)), CAST(N'2023-06-07T01:16:09.5856831' AS DateTime2))
SET IDENTITY_INSERT [dbo].[CarrierReports] OFF
GO
SET IDENTITY_INSERT [dbo].[Carriers] ON 

INSERT [dbo].[Carriers] ([CarrierId], [CarrierName], [CarrierIsActive], [CarrierPlusDesiCost]) VALUES (1, N'Aras', 1, 32)
INSERT [dbo].[Carriers] ([CarrierId], [CarrierName], [CarrierIsActive], [CarrierPlusDesiCost]) VALUES (2, N'MNG', 1, 45)
INSERT [dbo].[Carriers] ([CarrierId], [CarrierName], [CarrierIsActive], [CarrierPlusDesiCost]) VALUES (3, N'Yurtİçi', 1, 20)
INSERT [dbo].[Carriers] ([CarrierId], [CarrierName], [CarrierIsActive], [CarrierPlusDesiCost]) VALUES (4, N'PTT', 1, 15)
INSERT [dbo].[Carriers] ([CarrierId], [CarrierName], [CarrierIsActive], [CarrierPlusDesiCost]) VALUES (5, N'UPS', 1, 1)
SET IDENTITY_INSERT [dbo].[Carriers] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderId], [CarrierId], [OrderDesi], [OrderDate], [OrderCarrierCost]) VALUES (1, 2, 30, CAST(N'2023-06-05T20:03:51.8836180' AS DateTime2), CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([OrderId], [CarrierId], [OrderDesi], [OrderDate], [OrderCarrierCost]) VALUES (2, 3, 65, CAST(N'2023-06-05T23:22:42.1786914' AS DateTime2), CAST(206.00 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([OrderId], [CarrierId], [OrderDesi], [OrderDate], [OrderCarrierCost]) VALUES (3, 3, 50, CAST(N'2023-06-06T22:13:31.5051470' AS DateTime2), CAST(6.00 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([OrderId], [CarrierId], [OrderDesi], [OrderDate], [OrderCarrierCost]) VALUES (4, 5, 50, CAST(N'2023-06-06T22:14:06.5292891' AS DateTime2), CAST(1.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
ALTER TABLE [dbo].[CarrierConfigurations]  WITH CHECK ADD  CONSTRAINT [FK_CarrierConfigurations_Carriers_CarrierId] FOREIGN KEY([CarrierId])
REFERENCES [dbo].[Carriers] ([CarrierId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CarrierConfigurations] CHECK CONSTRAINT [FK_CarrierConfigurations_Carriers_CarrierId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Carriers_CarrierId] FOREIGN KEY([CarrierId])
REFERENCES [dbo].[Carriers] ([CarrierId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Carriers_CarrierId]
GO

CREATE DATABASE CargoManagementHangfireDB
GO
