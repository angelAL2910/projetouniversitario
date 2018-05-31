if exists (select * from sys.databases where name='dbventas')
begin 
use[master]
drop database [dbventas]
end

if not exists(select * from sys.databases where name='dbventas')
begin 
create database dbventas

end
CREATE TABLE [dbo].[articulo](
	[idarticulo] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](50) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[idcategoria] [int] NOT NULL,
	[Imag_Url] [varchar](250) NULL,
	[descripcion] [varchar](200) NULL,
	[precioVenta] [decimal](9, 2) NULL,
	[precioCompra] [decimal](9, 2) NULL,
	[cantidad] [decimal](9, 2) NULL,
	[estado] [bit] NULL,
	[idProveedor] [int] NOT NULL,
 CONSTRAINT [PK_articulo] PRIMARY KEY CLUSTERED 
(
	[idarticulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[categoria]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[cliente]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[cuentas_x_cobrar]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuentas_x_cobrar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[fecha] [datetime] NOT NULL,
	[valor] [decimal](18, 2) NOT NULL,
	[pagado] [bit] NOT NULL,
	[usuario] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cuentas_x_pagar]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuentas_x_pagar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_proveedor] [int] NOT NULL,
	[fecha] [datetime] NOT NULL,
	[valor] [decimal](18, 2) NOT NULL,
	[pagado] [bit] NOT NULL,
	[usuario] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalle_ingreso]    Script Date: 31/05/2018 16:32:47 ******/
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
	[fecha_produccion] [date] NOT NULL,
	[fecha_vencimiento] [date] NOT NULL,
 CONSTRAINT [PK_detalle_ingreso] PRIMARY KEY CLUSTERED 
(
	[iddetalle_ingreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalle_venta]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_venta](
	[iddetalle_venta] [int] IDENTITY(1,1) NOT NULL,
	[idventa] [int] NOT NULL,
	[iddetalle_ingreso] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio_venta] [money] NOT NULL,
	[descuento] [money] NOT NULL,
 CONSTRAINT [PK_detalle_venta] PRIMARY KEY CLUSTERED 
(
	[iddetalle_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ingreso]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ingreso](
	[idingreso] [int] IDENTITY(1,1) NOT NULL,
	[idtrabajador] [int] NOT NULL,
	[idproveedor] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[tipo_comprobante] [varchar](20) NOT NULL,
	[serie] [varchar](4) NOT NULL,
	[correlativo] [varchar](7) NOT NULL,
	[igv] [decimal](4, 2) NOT NULL,
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
/****** Object:  Table [dbo].[proveedor]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedor](
	[idproveedor] [int] IDENTITY(1,1) NOT NULL,
	[razon_social] [varchar](150) NOT NULL,
	[sector_comercial] [varchar](50) NOT NULL,
	[tipo_documento] [varchar](20) NOT NULL,
	[num_documento] [varchar](11) NOT NULL,
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
/****** Object:  Table [dbo].[ROLES]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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



INSERT INTO [dbo]. [ROLES] (Nombre, Grupo) VALUES ( ' Administrador ' , ' Administrador ' );
INSERT INTO [dbo]. [ROLES] (Nombre, Grupo) VALUES ( ' Almacenista ' , ' Inventario ' );
INSERT INTO [dbo]. [ROLES] (Nombre, Grupo) VALUES ( ' Vendedor ' , ' Operario ' );
INSERT INTO [dbo]. [ROLES] (Nombre, Grupo) VALUES ( ' Almacenista Sup ' , ' Inventario ' );
INSERT INTO [dbo]. [ROLES] (Nombre, Grupo) VALUES ( ' Vendedor Sup ' , ' Operario ' );
INSERT INTO [dbo]. [ROLES] (Nombre, Grupo) VALUES ( ' Gerente ' , ' Administrativo ' );
INSERT INTO [dbo]. [ROLES] (Nombre, Grupo) VALUES ( ' HHRR ' , ' Administrativo ' );
GO
/****** Object:  Table [dbo].[trabajador]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[trabajador](
	[idtrabajador] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](20) NOT NULL,
	[apellidos] [varchar](40) NOT NULL,
	[sexo] [varchar](1) NOT NULL,
	[Fecha_nac] [date] NOT NULL,
	[num_documento] [varchar](8) NOT NULL,
	[direccion] [varchar](100) NULL,
	[telefono] [varchar](10) NULL,
	[email] [varchar](50) NULL,
	[acceso] [varchar](20) NOT NULL,
	[usuario] [varchar](20) NOT NULL,
	[password] [varchar](20) NOT NULL,
 CONSTRAINT [PK_trabajador] PRIMARY KEY CLUSTERED 
(
	[idtrabajador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Clave] [varchar](50) NOT NULL,
	[RolID] [int] NOT NULL,
	[Statud] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


insert into dbo.USERS (Usuario,Clave,RolID,Statud) values('Edsource','ADMIN',1,1)
insert into dbo.USERS (Usuario,Clave,RolID,Statud) values('PVENEIKER','ADMIN',1,1)
insert into dbo.USERS (Usuario,Clave,RolID,Statud) values('GBLANCO','ADMIN',1,1)

GO
/****** Object:  Table [dbo].[venta]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venta](
	[idventa] [int] IDENTITY(1,1) NOT NULL,
	[idcliente] [int] NOT NULL,
	[idtrabajador] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[tipo_comprobante] [varchar](20) NOT NULL,
	[serie] [varchar](4) NOT NULL,
	[correlativo] [varchar](7) NOT NULL,
	[igv] [decimal](4, 2) NOT NULL,
 CONSTRAINT [PK_venta] PRIMARY KEY CLUSTERED 
(
	[idventa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[get_client_parameter]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create view [dbo].[get_client_parameter]
  as  
  
  select 
  c.id
  ,c.[id_cliente]
  ,cl.CodigoCliente
  ,cl.num_documento 
  ,(cl.nombre +''+ cl.apellidos) NombreCompleto   
  ,[fecha]
  ,[valor]
  ,[pagado] 
    
  from [dbventas].[dbo].[cuentas_x_cobrar]c
  left join dbo.cliente cl

  on c.id_cliente=cl.idcliente

  where --cl.CodigoCliente='6-015'
         cl.statu=1
		and c.pagado=0
GO
/****** Object:  View [dbo].[VW_CLIENTES_LOAD]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[VW_CLIENTES_LOAD]

AS
SELECT [idcliente]
      ,CodigoCliente
      ,upper(apellidos+' '+nombre)[Nombre_Completo_Empleado]
      ,[Sexo]
      ,[fecha_nacimiento][Fecha Nacimiento]
      ,[tipo_documento][Tipo_de_Documento]
      ,[num_documento][Numero_Identificación]
      ,[direccion][Direccion]
      ,[telefono][Telefono]
      ,[email][Correo_electronico]
      --iif([statu]=0,'Inactivo','Activo')[Status]
	  --,statu
  FROM [dbventas].[dbo].[cliente] where statu=1
GO
/****** Object:  View [dbo].[wv_get_cliente_deuda]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE view [dbo].[wv_get_cliente_deuda]

as

SELECT cc.id
      ,cc.id_cliente
	  ,c.num_documento
	  ,c.CodigoCliente
	  ,(c.nombre+' '+c.apellidos) as NombreCompleto
	  ,cc.fecha
	  ,cc.valor
	  ,cc.pagado
	  FROM dbo.[cuentas_x_cobrar]cc 
left join dbo.[cliente]c

on 

cc.id_cliente=c.idcliente

where c.statu=1   and cc.pagado=0
and valor>0

 
GO
ALTER TABLE [dbo].[articulo] ADD  DEFAULT ((0)) FOR [estado]
GO
ALTER TABLE [dbo].[cuentas_x_cobrar] ADD  DEFAULT (getdate()) FOR [fecha]
GO
ALTER TABLE [dbo].[cuentas_x_cobrar] ADD  DEFAULT ((0)) FOR [pagado]
GO
ALTER TABLE [dbo].[cuentas_x_pagar] ADD  DEFAULT (getdate()) FOR [fecha]
GO
ALTER TABLE [dbo].[cuentas_x_pagar] ADD  DEFAULT ((0)) FOR [pagado]
GO
ALTER TABLE [dbo].[USERS] ADD  DEFAULT ((0)) FOR [Statud]
GO
ALTER TABLE [dbo].[articulo]  WITH CHECK ADD  CONSTRAINT [FK_articulo_categoria] FOREIGN KEY([idcategoria])
REFERENCES [dbo].[categoria] ([idcategoria])
GO
ALTER TABLE [dbo].[articulo] CHECK CONSTRAINT [FK_articulo_categoria]
GO
ALTER TABLE [dbo].[cuentas_x_cobrar]  WITH CHECK ADD  CONSTRAINT [FK_cuentas_x_cobrar_cliente] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[cliente] ([idcliente])
GO
ALTER TABLE [dbo].[cuentas_x_cobrar] CHECK CONSTRAINT [FK_cuentas_x_cobrar_cliente]
GO
ALTER TABLE [dbo].[cuentas_x_pagar]  WITH CHECK ADD  CONSTRAINT [FK_cuentas_x_pagar_proveedor] FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[proveedor] ([idproveedor])
GO
ALTER TABLE [dbo].[cuentas_x_pagar] CHECK CONSTRAINT [FK_cuentas_x_pagar_proveedor]
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
ALTER TABLE [dbo].[detalle_venta]  WITH CHECK ADD  CONSTRAINT [FK_detalle_venta_detalle_ingreso] FOREIGN KEY([iddetalle_ingreso])
REFERENCES [dbo].[detalle_ingreso] ([iddetalle_ingreso])
GO
ALTER TABLE [dbo].[detalle_venta] CHECK CONSTRAINT [FK_detalle_venta_detalle_ingreso]
GO
ALTER TABLE [dbo].[detalle_venta]  WITH CHECK ADD  CONSTRAINT [FK_detalle_venta_venta] FOREIGN KEY([idventa])
REFERENCES [dbo].[venta] ([idventa])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[detalle_venta] CHECK CONSTRAINT [FK_detalle_venta_venta]
GO
ALTER TABLE [dbo].[ingreso]  WITH CHECK ADD  CONSTRAINT [FK_ingreso_proveedor] FOREIGN KEY([idproveedor])
REFERENCES [dbo].[proveedor] ([idproveedor])
GO
ALTER TABLE [dbo].[ingreso] CHECK CONSTRAINT [FK_ingreso_proveedor]
GO
ALTER TABLE [dbo].[ingreso]  WITH CHECK ADD  CONSTRAINT [FK_ingreso_trabajador] FOREIGN KEY([idtrabajador])
REFERENCES [dbo].[trabajador] ([idtrabajador])
GO
ALTER TABLE [dbo].[ingreso] CHECK CONSTRAINT [FK_ingreso_trabajador]
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
/****** Object:  StoredProcedure [dbo].[SP_GET_ARTICULO_LOAD]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_GET_ARTICULO_LOAD]
as 
BEGIN
SELECT [idarticulo]
      ,[codigo]
      ,[nombre]
      ,[idcategoria]
      ,[Imag_Url]
      ,[descripcion]
      ,[precioVenta]
      ,[precioCompra]
      ,[cantidad]
      ,[estado]
      ,[idProveedor]
  FROM [dbventas].[dbo].[articulo]

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_ARTICULOS_BUSCAR]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROC [dbo].[SP_GET_ARTICULOS_BUSCAR]
@codigo VARCHAR(50)
,@nombre VARCHAR(50)

as 
BEGIN

select            idarticulo
                 ,codigo
	             ,nombre
	             ,idcategoria
	             ,Imag_Url
	             ,descripcion
	             ,precioVenta
	             ,precioCompra 
from dbo.articulo
where 
codigo=@codigo or nombre like @nombre+'%'

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_articulos_LOAD]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_GET_articulos_LOAD]


as
begin 
select * from dbo.articulo
end
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_CATEGORIA_BUSCAR]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_GET_CATEGORIA_BUSCAR]

@nombre VARCHAR(50)

as 
BEGIN

select          idcategoria
               ,nombre
			   ,descripcion
from dbo.categoria
where 
nombre like '%'+@nombre+'%'

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_Categoria_LOAD]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_GET_Categoria_LOAD]


as
begin 
select * from dbo.categoria
end
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_CLIENTES_BUSCAR]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_GET_CLIENTES_BUSCAR]

@CodigoCliente varchar(60),
@Identificacion varchar(15),
@Nombre_Completo varchar(50),
@Telefono  varchar(15)
as 
BEGIN
select * from VW_CLIENTES_LOAD
where 
     [CodigoCliente]=@CodigoCliente 
or [Numero_Identificación]=@Identificacion
or [Nombre_Completo_Empleado] LIKE @Nombre_Completo+'%'
OR [Telefono]=@Telefono
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_clientes_deudores]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_get_clientes_deudores]

as

begin 

select * from [dbo].[wv_get_cliente_deuda]
end
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_CLIENTES_LOAD]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_GET_CLIENTES_LOAD]
as 
BEGIN
SELECT [idcliente]
      ,[nombre]
      ,[apellidos]
      ,[sexo]
      ,[fecha_nacimiento]
      ,[tipo_documento]
      ,[num_documento]
      ,[direccion]
      ,[telefono]
      ,[email]
      ,[CodigoCliente]
      FROM [dbventas].[dbo].[cliente]
	  where statu=1

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_COMBOBOX_CLIENTE]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[SP_GET_COMBOBOX_CLIENTE]

AS

BEGIN 
SELECT idcliente,upper(nombre +' '+apellidos)as NombreCompleto FROM dbo.cliente  
order by NombreCompleto
END
  
  --select 
  --c.id
  --,c.[id_cliente]
  --,cl.CodigoCliente
  --,cl.num_documento 
  --,(cl.nombre +''+ cl.apellidos) NombreCompleto   
  --,[fecha]
  --,[valor]
  --,[pagado] 
    
  --from [dbventas].[dbo].[cuentas_x_cobrar]c
  --left join dbo.cliente cl

  --on c.id_cliente=cl.idcliente

  --where cl.CodigoCliente='6-015'
  --      and cl.statu=1
		--and c.pagado=0

GO
/****** Object:  StoredProcedure [dbo].[sp_get_searche_client_pagos]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE proc [dbo].[sp_get_searche_client_pagos]
@num_documento varchar(15)
,@codigoCliente varchar(62)
,@NombComp varchar(80)

as

begin 

select * from get_client_parameter
where codigocliente =@codigoCliente or num_documento=@num_documento or 
NombreCompleto like '%'+@NombComp+'%%'
end 
GO
/****** Object:  StoredProcedure [dbo].[SP_LOGIN]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_LOGIN]
@usuario varchar(50),
@contrasena varchar(50),
@rolid int output
AS
BEGIN
 SELECT @rolid = RolID from [dbo].[USERS] WHERE Usuario = @usuario and Clave = @contrasena;
 IF NOT (@rolid > 0)
 BEGIN
   SET @rolid = 0
 END
END

GO
/****** Object:  StoredProcedure [dbo].[SP_SET_ARTICULO_INSERT_UPDATE]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_SET_ARTICULO_INSERT_UPDATE]
    
        @idarticulo	    int
       ,@codigo         varchar(50)
       ,@nombre         varchar(50)
       ,@idcategoria    int
       ,@Imag_Url       varchar(250)
       ,@descripcion    varchar(200)
       ,@precioVenta    decimal(9,2)
       ,@precioCompra   decimal(9,2)
       ,@cantidad       decimal(9,2)
       ,@estado         bit
       ,@idProveedor    int
	       
AS

BEGIN
IF EXISTS(select * from dbo.articulo where idarticulo=@idarticulo)

UPDATE dbo.articulo SET      
	 				     
						 [codigo]		= @codigo       
						,[nombre]		= @nombre       
						,[idcategoria]	= @idcategoria  
						,[Imag_Url]		= @Imag_Url     
						,[descripcion]	= @descripcion  
						,[precioVenta]	= @precioVenta  
						,[precioCompra]	= @precioCompra 
						,[cantidad]		= @cantidad     
						,[estado]		= @estado       
						,[idProveedor]  = @idProveedor  
					   

where idarticulo=@idarticulo
					   
ELSE

INSERT INTO dbo.articulo values(
                                  @codigo       
								 ,@nombre       
								 ,@idcategoria  
								 ,@Imag_Url     
								 ,@descripcion  
								 ,@precioVenta  
								 ,@precioCompra 
								 ,@cantidad     
								 ,@estado       
								 ,@idProveedor  
                                                             
                              )
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SET_CATEGORIA]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[SP_SET_CATEGORIA]
@idCat int
,@nom varchar(50)
,@Desc varchar(256)

as

begin 
if exists(select * from categoria where idcategoria=@idCat)
update categoria set nombre=@nom,descripcion=@Desc where idcategoria=@idCat
else

insert into categoria (nombre,descripcion)values(@nom,@Desc)
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SET_Categoria_UPDATE_INSERT]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_SET_Categoria_UPDATE_INSERT]

@IdCategoria int,
@NomCategiria VARCHAR(50),
@Descripcion varchar(256)

as

begin 
if exists(select * from dbo.categoria where idcategoria=@IdCategoria)
update dbo.categoria set nombre=@NomCategiria,descripcion=@Descripcion
where idcategoria=@IdCategoria
else

insert into dbo.categoria  values(@NomCategiria,@Descripcion)
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SET_CLIENTE_DELETE]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROC [dbo].[SP_SET_CLIENTE_DELETE]
 
     
 @idcliente INT 
--,@status    BIT
,@UsuarioModifica VARCHAR(50)

AS

BEGIN
UPDATE dbo.cliente set  statu=0
                       ,UsuarioAdiciona=@UsuarioModifica
					    where idcliente=@idcliente

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SET_CLIENTE_INSERT_UPDATE]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_SET_CLIENTE_INSERT_UPDATE]
    
	   @idcliente         int
      ,@nombre			  varchar(50)
      ,@apellidos		  varchar(40)
      ,@sexo			  char(10)
      ,@fecha_nacimiento  date
      ,@tipo_documento	  varchar(20)
      ,@num_documento	  varchar(8)
      ,@direccion		  varchar(100)
      ,@telefono		  varchar(15)
      ,@email			  varchar(50)
	  ,@status            bit
	  ,@FechaAdiciona     datetime
      ,@FechaModifica     datetime
      ,@UsuarioAdiciona   varchar(50)
      --,@UsuarioModifica	  varchar(50)
                
AS

BEGIN
IF EXISTS(select * from dbo.cliente where idcliente=@idcliente)

UPDATE dbo.cliente SET      
	 				   nombre		     =	@nombre			
					   ,apellidos	     =	@apellidos		
					   ,sexo			 =	@sexo			
					   ,fecha_nacimiento =	@fecha_nacimiento
					   ,tipo_documento	 =	@tipo_documento	
					   ,num_documento	 =	@num_documento	
					   ,direccion		 =	@direccion		
					   ,telefono		 =	@telefono		
					   ,email            =	@email
					   ,statu            =  @status
			           ,FechaAdiciona    =  @FechaAdiciona
                       ,FechaModifica    =  @FechaModifica
                       ,UsuarioAdiciona  =  @UsuarioAdiciona
                       --,UsuarioModifica  =  @UsuarioModifica
                       ,HostName         = HOST_NAME()
					   

where idcliente=@idcliente
					   
ELSE

INSERT INTO dbo.cliente values(

                                @nombre			
							   ,@apellidos		
							   ,@sexo			
							   ,@fecha_nacimiento
							   ,@tipo_documento	
							   ,@num_documento	
							   ,@direccion		
							   ,@telefono		
							   ,@email
							   ,@status
							   ,@FechaAdiciona
							   ,@FechaModifica
							   ,@UsuarioAdiciona
							   ,null--@UsuarioModifica
							   ,HOST_NAME()
                              )


END
GO
/****** Object:  StoredProcedure [dbo].[SP_SET_CLIENTE_INSERT_UPDATE_20180528]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[SP_SET_CLIENTE_INSERT_UPDATE_20180528]
    
	   @idcliente         int
      ,@nombre			  varchar(50)
      ,@apellidos		  varchar(40)
      ,@sexo			  char(10)
      ,@fecha_nacimiento  date
      ,@tipo_documento	  varchar(20)
      ,@num_documento	  varchar(8)
      ,@direccion		  varchar(100)
      ,@telefono		  varchar(15)
      ,@email			  varchar(50)
	  ,@status            bit
	  ,@FechaAdiciona     datetime
      --,@FechaModifica     datetime
      ,@UsuarioAdiciona   varchar(50)
      --,@UsuarioModifica	  varchar(50)
                
AS
declare @FechaModifica  datetime=getdate() 

BEGIN
IF EXISTS(select * from dbo.cliente where idcliente=@idcliente)

UPDATE dbo.cliente SET      
	 				   nombre		     =	@nombre			
					   ,apellidos	     =	@apellidos		
					   ,sexo			 =	@sexo			
					   ,fecha_nacimiento =	@fecha_nacimiento
					   ,tipo_documento	 =	@tipo_documento	
					   ,num_documento	 =	@num_documento	
					   ,direccion		 =	@direccion		
					   ,telefono		 =	@telefono		
					   ,email            =	@email
					   ,statu            =  @status
			           ,FechaAdiciona    =  @FechaAdiciona
                       ,FechaModifica    =  @FechaModifica
                       ,UsuarioAdiciona  =  @UsuarioAdiciona
                       --,UsuarioModifica  =  @UsuarioModifica
                       ,HostName         = HOST_NAME()
					   

where idcliente=@idcliente
					   
ELSE

INSERT INTO dbo.cliente values(

                                @nombre			
							   ,@apellidos		
							   ,@sexo			
							   ,@fecha_nacimiento
							   ,@tipo_documento	
							   ,@num_documento	
							   ,@direccion		
							   ,@telefono		
							   ,@email
							   ,@status
							   ,getdate()
							   ,@FechaModifica
							   ,@UsuarioAdiciona
							   ,null--@UsuarioModifica
							   ,HOST_NAME()
                              )


END
GO
/****** Object:  StoredProcedure [dbo].[SP_SET_DELETE_ARTICULO]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_SET_DELETE_ARTICULO]
@codigo int 
,@estado bit
AS

BEGIN 
UPDATE dbo.articulo SET estado=@estado where codigo=@codigo

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SET_INSERT_UPDATE_ARTICULO]    Script Date: 31/05/2018 16:32:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_SET_INSERT_UPDATE_ARTICULO]
    @idarticulo int ,
	@codigo varchar(50)  ,
	@nombre varchar(50)  ,
	@idcategoria int  ,
	@Imag_Url varchar(250) ,
	@descripcion varchar(200) ,
	@precioVenta decimal(9, 2) ,
	@precioCompra decimal(9, 2) ,
	@cantidad decimal(9, 2) ,
	@estado bit ,
	@idProveedor int 

AS
BEGIN 
IF EXISTS(SELECT * FROM dbo.articulo where idarticulo=@idarticulo)
UPDATE [dbo].[articulo]
   SET 
       [codigo]       = @codigo
	  ,[nombre]       = @nombre
      ,[idcategoria]  = @idcategoria
      ,[Imag_Url]     = @Imag_Url
      ,[descripcion]  = @descripcion
      ,[precioVenta]  = @precioVenta
      ,[precioCompra] = @precioCompra
      ,[cantidad]     = @cantidad
      ,[estado]       = @estado
      ,[idProveedor]  = @idProveedor

 WHERE idarticulo=@idarticulo
else

INSERT INTO [dbo].[articulo]
           ([codigo]
           ,[nombre]
           ,[idcategoria]
           ,[Imag_Url]
           ,[descripcion]
           ,[precioVenta]
           ,[precioCompra]
           ,[cantidad]
           ,[estado]
           ,[idProveedor])
     VALUES
           (
		    @codigo
		   ,@nombre
		   ,@idcategoria
		   ,@Imag_Url
		   ,@descripcion
		   ,@precioVenta
		   ,@precioCompra
		   ,@cantidad
		   ,@estado
		   ,@idProveedor
		   )

END
GO
