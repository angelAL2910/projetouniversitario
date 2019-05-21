USE [Facturador]
GO
/****** Object:  Table [dbo].[Comprobante]    Script Date: 5/21/2019 7:26:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comprobante](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Cliente] [varchar](300) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[Creado] [datetime] NOT NULL,
 CONSTRAINT [PK_Comprobante] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComprobanteDetalle]    Script Date: 5/21/2019 7:26:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComprobanteDetalle](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ComprobanteId] [int] NOT NULL,
	[ProductoId] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [decimal](18, 2) NOT NULL,
	[Monto] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ComprobanteDetalle] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 5/21/2019 7:26:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](300) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Comprobante] ON 

INSERT [dbo].[Comprobante] ([id], [Cliente], [Total], [Creado]) VALUES (6, N'PEDRO', CAST(1208.00 AS Decimal(18, 2)), CAST(N'2019-05-20T22:10:40.640' AS DateTime))
SET IDENTITY_INSERT [dbo].[Comprobante] OFF
SET IDENTITY_INSERT [dbo].[ComprobanteDetalle] ON 

INSERT [dbo].[ComprobanteDetalle] ([id], [ComprobanteId], [ProductoId], [Cantidad], [PrecioUnitario], [Monto]) VALUES (1, 6, 3, 1, CAST(1200.00 AS Decimal(18, 2)), CAST(1200.00 AS Decimal(18, 2)))
INSERT [dbo].[ComprobanteDetalle] ([id], [ComprobanteId], [ProductoId], [Cantidad], [PrecioUnitario], [Monto]) VALUES (2, 6, 7, 1, CAST(8.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[ComprobanteDetalle] OFF
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([id], [Nombre], [Precio]) VALUES (1, N'Fender Squier Vibe 60', CAST(350.00 AS Decimal(18, 2)))
INSERT [dbo].[Producto] ([id], [Nombre], [Precio]) VALUES (2, N'Amplificador Marshal', CAST(800.00 AS Decimal(18, 2)))
INSERT [dbo].[Producto] ([id], [Nombre], [Precio]) VALUES (3, N'Amplificador Laney a tubos', CAST(1200.00 AS Decimal(18, 2)))
INSERT [dbo].[Producto] ([id], [Nombre], [Precio]) VALUES (4, N'Messa Boggie Rectifier', CAST(3500.00 AS Decimal(18, 2)))
INSERT [dbo].[Producto] ([id], [Nombre], [Precio]) VALUES (5, N'Cuerdas para guitarra Addario #9', CAST(7.00 AS Decimal(18, 2)))
INSERT [dbo].[Producto] ([id], [Nombre], [Precio]) VALUES (7, N'Cuerdas para guitarra Addario #10', CAST(8.00 AS Decimal(18, 2)))
INSERT [dbo].[Producto] ([id], [Nombre], [Precio]) VALUES (9, N'Cuerdas para guitarra Ernie Ball #10', CAST(8.50 AS Decimal(18, 2)))
INSERT [dbo].[Producto] ([id], [Nombre], [Precio]) VALUES (10, N'Guitarra Ibanez 350 EX', CAST(340.00 AS Decimal(18, 2)))
INSERT [dbo].[Producto] ([id], [Nombre], [Precio]) VALUES (11, N'Guitarra Schecter Omen Extreme 6', CAST(400.00 AS Decimal(18, 2)))
INSERT [dbo].[Producto] ([id], [Nombre], [Precio]) VALUES (12, N'Guitarra Schecter Diamond Series', CAST(800.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Producto] OFF
ALTER TABLE [dbo].[ComprobanteDetalle]  WITH CHECK ADD  CONSTRAINT [FK_ComprobanteDetalle_Comprobante1] FOREIGN KEY([ComprobanteId])
REFERENCES [dbo].[Comprobante] ([id])
GO
ALTER TABLE [dbo].[ComprobanteDetalle] CHECK CONSTRAINT [FK_ComprobanteDetalle_Comprobante1]
GO
ALTER TABLE [dbo].[ComprobanteDetalle]  WITH CHECK ADD  CONSTRAINT [FK_ComprobanteDetalle_Producto] FOREIGN KEY([ProductoId])
REFERENCES [dbo].[Producto] ([id])
GO
ALTER TABLE [dbo].[ComprobanteDetalle] CHECK CONSTRAINT [FK_ComprobanteDetalle_Producto]
GO
