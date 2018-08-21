USE [dbventas]
GO
/****** Object:  Table [dbo].[articulo]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[articulo](
	[idarticulo] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[idcategoria] [int] NOT NULL,
	[Imag_Url] [varchar](250) NULL,
	[descripcion] [varchar](200) NULL,
	[precioVenta] [decimal](9, 2) NULL,
	[precioCompra] [decimal](9, 2) NULL,
	[cantidad] [decimal](9, 2) NULL,
	[estado] [bit] NULL DEFAULT ((0)),
	[idProveedor] [int] NOT NULL,
	[CodigoBarra]  AS (CONVERT([varchar],(upper(substring([nombre],(1),(4)))+'00000')+CONVERT([varchar],[idarticulo]))),
 CONSTRAINT [PK_articulo] PRIMARY KEY CLUSTERED 
(
	[idarticulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[categoria]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[categoria](
	[idcategoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [nchar](256) NULL,
 CONSTRAINT [PK_categoria] PRIMARY KEY CLUSTERED 
(
	[idcategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cliente](
	[idcliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellidos] [varchar](40) NULL,
	[sexo] [char](10) NULL,
	[fecha_nacimiento] [date] NULL,
	[tipo_documento] [varchar](20) NOT NULL,
	[num_documento] [varchar](15) NOT NULL,
	[direccion] [varchar](100) NULL,
	[telefono] [varchar](15) NULL,
	[email] [varchar](50) NULL,
	[statu] [bit] NULL,
	[CodigoCliente]  AS ((CONVERT([varchar],datepart(day,[fecha_nacimiento]))+'-0')+CONVERT([varchar],[idcliente])),
	[FechaAdiciona] [datetime] NULL,
	[FechaModifica] [datetime] NULL,
	[UsuarioAdiciona] [varchar](50) NULL,
	[UsuarioModifica] [varchar](50) NULL,
	[HostName] [varchar](200) NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[idcliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cotizacion]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cotizacion](
	[idcotizacion] [int] IDENTITY(1,1) NOT NULL,
	[idcliente] [int] NOT NULL,
	[idtrabajador] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[subtotal] [decimal](18, 2) NOT NULL,
	[itbis] [decimal](9, 2) NOT NULL,
	[total] [decimal](18, 2) NOT NULL,
	[fecha] [date] NOT NULL,
	[estatus] [bit] NOT NULL CONSTRAINT [DF_cotizacion_estatus]  DEFAULT ((1)),
 CONSTRAINT [PK_cotizacion] PRIMARY KEY CLUSTERED 
(
	[idcotizacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cuentas_x_cobrar]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cuentas_x_cobrar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[fecha] [date] NOT NULL CONSTRAINT [DF__cuentas_x__fecha__2D27B809]  DEFAULT (getdate()),
	[valor] [decimal](18, 2) NOT NULL,
	[pagado] [bit] NOT NULL CONSTRAINT [DF__cuentas_x__pagad__2E1BDC42]  DEFAULT ((0)),
	[usuario] [varchar](50) NOT NULL,
	[id_venta] [int] NOT NULL,
 CONSTRAINT [PK_cuentas_x_cobrar] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cuentas_x_pagar]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cuentas_x_pagar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_proveedor] [int] NOT NULL,
	[fecha] [datetime] NOT NULL,
	[valor] [decimal](18, 2) NOT NULL,
	[pagado] [bit] NOT NULL,
	[usuario] [varchar](50) NOT NULL,
 CONSTRAINT [PK_cuentas_x_pagar] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[detalle_cotizacion_productos]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[detalle_cotizacion_productos](
	[id_cotizacion_producto] [int] IDENTITY(1,1) NOT NULL,
	[idcotizacion] [int] NOT NULL,
	[producto] [varchar](50) NOT NULL,
	[cantidad] [int] NOT NULL,
	[precioVenta] [decimal](18, 2) NOT NULL,
	[itbis] [decimal](9, 2) NOT NULL,
	[subtotal] [decimal](18, 2) NOT NULL,
	[total] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_detalle_cotizacion_productos] PRIMARY KEY CLUSTERED 
(
	[id_cotizacion_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[detalle_ingreso]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_ingreso](
	[iddetalle_ingreso] [int] IDENTITY(1,1) NOT NULL,
	[idingreso] [int] NOT NULL,
	[idarticulo] [int] NOT NULL,
	[precio_compra] [money] NOT NULL,
	[precio_venta] [money] NOT NULL,
	[stock_inicial] [int] NOT NULL,
	[stock_actual] [int] NOT NULL,
	[fecha_produccion] [date] NULL,
	[fecha_vencimiento] [date] NULL,
 CONSTRAINT [PK_detalle_ingreso] PRIMARY KEY CLUSTERED 
(
	[iddetalle_ingreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[detalle_venta]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[detalle_venta](
	[iddetalle_venta] [int] IDENTITY(1,1) NOT NULL,
	[idventa] [int] NOT NULL,
	[producto] [varchar](50) NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio_venta] [decimal](18, 2) NOT NULL,
	[descuento] [decimal](18, 2) NOT NULL,
	[itbis] [decimal](9, 2) NOT NULL,
	[sub_itbis]  AS ([cantidad]*[itbis]),
	[sub_total]  AS ([cantidad]*[precio_venta]),
 CONSTRAINT [PK_detalle_venta] PRIMARY KEY CLUSTERED 
(
	[iddetalle_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Factura](
	[id_factura] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[nombre_trabajador] [varchar](100) NOT NULL,
	[tipo_pago] [varchar](50) NOT NULL,
	[fecha] [date] NOT NULL CONSTRAINT [DF_Factura_fecha]  DEFAULT (getdate()),
	[medio_pago] [varchar](50) NOT NULL,
	[id_venta] [int] NOT NULL,
	[id_trabajador] [int] NOT NULL,
	[cantidad_articulos] [int] NOT NULL,
	[subtotal] [decimal](18, 2) NOT NULL,
	[itbis] [decimal](9, 2) NOT NULL,
	[total] [decimal](18, 2) NOT NULL,
	[numero_factura] [varchar](100) NULL CONSTRAINT [DF_Factura_numero_factura]  DEFAULT ('(''NFC''+CONVERT(varchar(50),(upper(substring([nombre_trabajador],(1),(4)))+''000000'')+CONVERT(varchar(10),[id_venta])))'),
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ingreso]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ingreso](
	[idingreso] [int] IDENTITY(1,1) NOT NULL,
	[CodigBarra] [int] NULL,
	[idproveedor] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[tipo_comprobante] [varchar](20) NOT NULL,
	[igv] [decimal](9, 2) NOT NULL,
	[FechaAdiciona] [datetime] NULL,
	[FechaModifica] [datetime] NULL,
	[UsuarioAdiciona] [varchar](50) NULL,
	[UsuarioModifica] [varchar](50) NULL,
 CONSTRAINT [PK_ingreso] PRIMARY KEY CLUSTERED 
(
	[idingreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MovimientosPagosYcobranzas]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MovimientosPagosYcobranzas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DetalleMov] [varchar](50) NULL,
	[fechaPago] [datetime] NULL,
	[idFactura] [varchar](60) NULL,
	[cantidadPagada] [decimal](9, 2) NULL,
	[statud] [bit] NULL,
	[usuarioPago] [varchar](50) NULL,
	[id_cxc] [int] NULL,
	[id_cxp] [int] NULL,
 CONSTRAINT [PK__Movimien__3213E83F5BDAEDBE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ncf_Comprovante]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ncf_Comprovante](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Tipo_Comprovante_Fiscal] [varchar](50) NULL,
	[Number] [varchar](50) NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Ncf_Comprovante] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[proveedor]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[proveedor](
	[idproveedor] [int] IDENTITY(1,1) NOT NULL,
	[razon_social] [varchar](150) NOT NULL,
	[NombreProveedor] [varchar](50) NOT NULL,
	[tipo_documento] [varchar](20) NOT NULL,
	[num_documento] [varchar](15) NOT NULL,
	[direccion] [nchar](100) NULL,
	[telefono] [varchar](10) NULL,
	[email] [varchar](50) NULL,
	[url] [varchar](100) NULL,
	[statu] [bit] NULL,
	[FechaAdiciona] [datetime] NULL,
	[FechaModifica] [datetime] NULL,
	[UsuarioAdiciona] [varchar](50) NULL,
	[UsuarioModifica] [varchar](50) NULL,
	[HostName] [varchar](200) NULL,
 CONSTRAINT [PK_proveedor] PRIMARY KEY CLUSTERED 
(
	[idproveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ROLES]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ROLES](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Grupo] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[trabajador]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trabajador](
	[idtrabajador] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](20) NOT NULL,
	[apellidos] [varchar](40) NOT NULL,
	[sexo] [varchar](1) NOT NULL,
	[Fecha_nac] [date] NOT NULL,
	[num_documento] [varchar](15) NOT NULL,
	[direccion] [varchar](100) NULL,
	[telefono] [varchar](10) NULL,
	[email] [varchar](50) NULL,
	[StatusE] [bit] NULL CONSTRAINT [DF_trabajador_StatusE]  DEFAULT ((1)),
	[FechaModifica] [datetime] NULL,
	[UsuarioModifica] [varchar](50) NULL,
	[FechaAdiciona] [datetime] NULL,
	[UsuarioAdiciona] [varchar](50) NULL,
 CONSTRAINT [PK_trabajador] PRIMARY KEY CLUSTERED 
(
	[idtrabajador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[USERS](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Clave] [varchar](50) NOT NULL,
	[RolID] [int] NOT NULL,
	[Statud] [bit] NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsuarioTrabajador]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsuarioTrabajador](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_trabajador] [int] NULL,
	[id_usuario] [int] NULL,
	[fecha_asignacion] [datetime] NULL,
	[fecha_deasinacion] [datetime] NULL,
	[status_assignacion] [bit] NULL,
	[usuarioAdiciona] [varchar](50) NULL,
	[usuarioModifica] [varchar](50) NULL,
	[fechaAdiciona] [datetime] NULL,
	[fechaModifica] [datetime] NULL,
 CONSTRAINT [PK_UsuarioTrabajador] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[venta]    Script Date: 12/7/18 12:32:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[venta](
	[idventa] [int] IDENTITY(1,1) NOT NULL,
	[idcliente] [int] NOT NULL,
	[idtrabajador] [int] NOT NULL,
	[fecha] [date] NOT NULL CONSTRAINT [DF_venta_fecha]  DEFAULT (getdate()),
	[tipo_comprobante] [varchar](20) NOT NULL,
	[tipo_venta] [varchar](20) NOT NULL,
	[tipo_cliente] [varchar](20) NOT NULL,
	[itbis] [decimal](9, 2) NOT NULL,
	[subtotal] [decimal](18, 2) NOT NULL,
	[total] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_venta] PRIMARY KEY CLUSTERED 
(
	[idventa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[cuentas_x_pagar] ADD  DEFAULT (getdate()) FOR [fecha]
GO
ALTER TABLE [dbo].[cuentas_x_pagar] ADD  DEFAULT ((0)) FOR [pagado]
GO
ALTER TABLE [dbo].[articulo]  WITH CHECK ADD  CONSTRAINT [FK_articulo_categoria] FOREIGN KEY([idcategoria])
REFERENCES [dbo].[categoria] ([idcategoria])
GO
ALTER TABLE [dbo].[articulo] CHECK CONSTRAINT [FK_articulo_categoria]
GO
ALTER TABLE [dbo].[cotizacion]  WITH CHECK ADD  CONSTRAINT [FK_cotizacion_cliente] FOREIGN KEY([idcliente])
REFERENCES [dbo].[cliente] ([idcliente])
GO
ALTER TABLE [dbo].[cotizacion] CHECK CONSTRAINT [FK_cotizacion_cliente]
GO
ALTER TABLE [dbo].[cotizacion]  WITH CHECK ADD  CONSTRAINT [FK_cotizacion_trabajador] FOREIGN KEY([idtrabajador])
REFERENCES [dbo].[trabajador] ([idtrabajador])
GO
ALTER TABLE [dbo].[cotizacion] CHECK CONSTRAINT [FK_cotizacion_trabajador]
GO
ALTER TABLE [dbo].[cuentas_x_cobrar]  WITH CHECK ADD  CONSTRAINT [FK_cuentas_x_cobrar_cliente] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[cliente] ([idcliente])
GO
ALTER TABLE [dbo].[cuentas_x_cobrar] CHECK CONSTRAINT [FK_cuentas_x_cobrar_cliente]
GO
ALTER TABLE [dbo].[cuentas_x_cobrar]  WITH CHECK ADD  CONSTRAINT [FK_cuentas_x_cobrar_venta] FOREIGN KEY([id_venta])
REFERENCES [dbo].[venta] ([idventa])
GO
ALTER TABLE [dbo].[cuentas_x_cobrar] CHECK CONSTRAINT [FK_cuentas_x_cobrar_venta]
GO
ALTER TABLE [dbo].[cuentas_x_pagar]  WITH CHECK ADD  CONSTRAINT [FK_cuentas_x_pagar_proveedor] FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[proveedor] ([idproveedor])
GO
ALTER TABLE [dbo].[cuentas_x_pagar] CHECK CONSTRAINT [FK_cuentas_x_pagar_proveedor]
GO
ALTER TABLE [dbo].[detalle_cotizacion_productos]  WITH CHECK ADD  CONSTRAINT [FK_detalle_cotizacion_productos_cotizacion] FOREIGN KEY([idcotizacion])
REFERENCES [dbo].[cotizacion] ([idcotizacion])
GO
ALTER TABLE [dbo].[detalle_cotizacion_productos] CHECK CONSTRAINT [FK_detalle_cotizacion_productos_cotizacion]
GO
ALTER TABLE [dbo].[detalle_ingreso]  WITH CHECK ADD  CONSTRAINT [FK_detalle_ingreso_articulo] FOREIGN KEY([idarticulo])
REFERENCES [dbo].[articulo] ([idarticulo])
GO
ALTER TABLE [dbo].[detalle_ingreso] CHECK CONSTRAINT [FK_detalle_ingreso_articulo]
GO
ALTER TABLE [dbo].[detalle_ingreso]  WITH CHECK ADD  CONSTRAINT [FK_detalle_ingreso_ingreso] FOREIGN KEY([idingreso])
REFERENCES [dbo].[ingreso] ([idingreso])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[detalle_ingreso] CHECK CONSTRAINT [FK_detalle_ingreso_ingreso]
GO
ALTER TABLE [dbo].[detalle_venta]  WITH CHECK ADD  CONSTRAINT [FK_detalle_venta_venta] FOREIGN KEY([idventa])
REFERENCES [dbo].[venta] ([idventa])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[detalle_venta] CHECK CONSTRAINT [FK_detalle_venta_venta]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_cliente] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[cliente] ([idcliente])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_cliente]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_trabajador] FOREIGN KEY([id_trabajador])
REFERENCES [dbo].[trabajador] ([idtrabajador])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_trabajador]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_venta] FOREIGN KEY([id_venta])
REFERENCES [dbo].[venta] ([idventa])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_venta]
GO
ALTER TABLE [dbo].[ingreso]  WITH CHECK ADD  CONSTRAINT [FK_ingreso_proveedor] FOREIGN KEY([CodigBarra])
REFERENCES [dbo].[proveedor] ([idproveedor])
GO
ALTER TABLE [dbo].[ingreso] CHECK CONSTRAINT [FK_ingreso_proveedor]
GO
ALTER TABLE [dbo].[MovimientosPagosYcobranzas]  WITH CHECK ADD  CONSTRAINT [FK_MovimientosPagosYcobranzas_cuentas_x_cobrar] FOREIGN KEY([id_cxc])
REFERENCES [dbo].[cuentas_x_cobrar] ([id])
GO
ALTER TABLE [dbo].[MovimientosPagosYcobranzas] CHECK CONSTRAINT [FK_MovimientosPagosYcobranzas_cuentas_x_cobrar]
GO
ALTER TABLE [dbo].[MovimientosPagosYcobranzas]  WITH CHECK ADD  CONSTRAINT [FK_MovimientosPagosYcobranzas_cuentas_x_pagar] FOREIGN KEY([id_cxp])
REFERENCES [dbo].[cuentas_x_pagar] ([id])
GO
ALTER TABLE [dbo].[MovimientosPagosYcobranzas] CHECK CONSTRAINT [FK_MovimientosPagosYcobranzas_cuentas_x_pagar]
GO
ALTER TABLE [dbo].[USERS]  WITH CHECK ADD  CONSTRAINT [FK_USERS_ROLES] FOREIGN KEY([RolID])
REFERENCES [dbo].[ROLES] ([id])
GO
ALTER TABLE [dbo].[USERS] CHECK CONSTRAINT [FK_USERS_ROLES]
GO
ALTER TABLE [dbo].[venta]  WITH CHECK ADD  CONSTRAINT [FK_venta_cliente] FOREIGN KEY([idcliente])
REFERENCES [dbo].[cliente] ([idcliente])
GO
ALTER TABLE [dbo].[venta] CHECK CONSTRAINT [FK_venta_cliente]
GO
ALTER TABLE [dbo].[venta]  WITH CHECK ADD  CONSTRAINT [FK_venta_trabajador] FOREIGN KEY([idtrabajador])
REFERENCES [dbo].[trabajador] ([idtrabajador])
GO
ALTER TABLE [dbo].[venta] CHECK CONSTRAINT [FK_venta_trabajador]
GO
