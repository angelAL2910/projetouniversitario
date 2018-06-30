/****** Consulta para saber cual es el Usuario y trabajadaro para hacer el mask ******/
SELECT * FROM [dbventas].[dbo].[USERS] 
select * from [dbo].[trabajador]

  begin tran --commit rollback
  DECLARE @id int = (select max(id)+1 from dbo.UsuarioTrabajador)
  INSERT INTO [dbventas].[dbo].[UsuarioTrabajador]
  SELECT TOP 1
       @id[id]
      ,10[id_trabajador]
      ,2[id_usuario]
      ,GETDATE()[fecha_asignacion]
      ,NULL[fecha_deasinacion]
      ,1[status_assignacion]
      ,'PVENEIKER'[usuarioAdiciona]
      ,NULL[usuarioModifica]
      ,GETDATE()[fechaAdiciona]
      ,NULL[fechaModifica]
  FROM [dbventas].[dbo].[UsuarioTrabajador]