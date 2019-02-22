﻿using StateTrustGlobal.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
namespace StateTrustGlobal.Data
{
    public class DataAccess
    {
        public string MsgSql { get; set; }
        string result;
        #region Base de Datos Global

        public IEnumerable<Marcas> Mostrar_Marcas(string Marca)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                IEnumerable<Marcas> RetornarValue = context.Database.SqlQuery<Marcas>("select * from Global.VW_Get_all_ST_VEHICLE_MAKE1 where Make_Desc LIKE '%'+@Marca+'%' ",
                    new SqlParameter("Marca", Marca)).ToList();
                return RetornarValue.ToList();
            }
        }
        /// <summary>
        /// Busca todas las marcas que se encuentra registradas
        /// DB:Global.ST_VEHICLE_MAKE 
        /// </summary>
        /// <param name="NombreMarca"></param>
        /// <returns></returns>
        public IEnumerable<Marcas> Mostrar_Marcas()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<Marcas> RetornarValue = context.Database.SqlQuery<Marcas>("select * from Global.VW_Get_all_ST_VEHICLE_MAKE1").ToList();
                return RetornarValue.ToList();
            }
        }
        /// <summary>
        /// Actualiza la marca para activarla o inactivarla.
        /// </summary>
        /// <param name="Make_Id"></param>
        /// <param name="Make_Desc"></param>
        /// <param name="Pos_Flag"></param>
        /// <param name="User"></param>
        /// <param name="Hostname"></param>
        public void Actualizar_Estatus_Marcas(int Make_Id, string Make_Desc, bool Pos_Flag, string User, string Hostname, int Opcion, string Estatus, int Cod)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                using (var cmd = context.Database.Connection.CreateCommand())
                {
                    context.Database.Connection.Open();
                    cmd.CommandText = "GLOBAL.SP_ST_VEHICLE_MAKE";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Make_Id", Make_Id));
                    cmd.Parameters.Add(new SqlParameter("Make_Desc", Make_Desc));
                    cmd.Parameters.Add(new SqlParameter("Pos_Flag", Pos_Flag));
                    cmd.Parameters.Add(new SqlParameter("User", User));
                    cmd.Parameters.Add(new SqlParameter("Hostname", Hostname));
                    cmd.Parameters.Add(new SqlParameter("Opcion", Opcion));
                    cmd.Parameters.Add(new SqlParameter("Estatus", Estatus));
                    cmd.Parameters.Add(new SqlParameter("Cod", Cod));
                    var Exec = (cmd.ExecuteScalar());
                }
            }
        }
        /// <summary>
        /// Registro la nueva marca en las base de datos
        /// [Global] y SysFlexSeguros
        /// </summary>
        /// <param name="Make_Desc"></param>
        /// <param name="Pos_Flag"></param>
        /// <param name="User"></param>
        /// <param name="Hostname"></param>
        /// <param name="opcion"></param>
        /// <param name="Estatus"></param>
        public void Registrar_Marcas(string Make_Desc, int Pos_Flag, string User, string Hostname, int opcion, string Estatus)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                using (var cmd = context.Database.Connection.CreateCommand())
                {
                    context.Database.Connection.Open();
                    cmd.CommandText = "GLOBAL.SP_INSERT_ST_VEHICLE_MAKE";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Make_Desc", Make_Desc));
                    cmd.Parameters.Add(new SqlParameter("Pos_Flag", Pos_Flag));
                    cmd.Parameters.Add(new SqlParameter("User", User));
                    cmd.Parameters.Add(new SqlParameter("Hostname", Hostname));
                    cmd.Parameters.Add(new SqlParameter("opcion", opcion));
                    cmd.Parameters.Add(new SqlParameter("Estatus", Estatus));
                    var Exec = (cmd.ExecuteScalar());
                }
            }

        }
        /// <summary>
        /// Registrar los modelos
        /// </summary>
        /// <param name="Marca"></param>
        /// <param name="Categoria"></param>
        /// <param name="Recargo"></param>
        /// <param name="DeducibleMinimo"></param>
        /// <param name="Opcion"></param>
        /// <param name="Descripcion"></param>
        /// <param name="User"></param>
        /// <param name="Vehicle_Type_Id"></param>
        /// <param name="Model_Desc"></param>
        /// <param name="Pos_Flag"></param>
        //public void Registrar_Modelos(int Marca,/* int Modelo,*/ string Categoria, decimal Recargo, int DeducibleMinimo,/* string HostName,*/ int Opcion/*Opcion 1*/,
        //    string Descripcion, int User, int Vehicle_Type_Id, string Model_Desc, int Pos_Flag, int Core_id)
        //{
        //    using (GlobalDbContext context = new GlobalDbContext())
        //    {
        //        using (var cmd = context.Database.Connection.CreateCommand())
        //        {
        //            context.Database.Connection.Open();
        //            cmd.CommandText = "Global.SP_INTSERT_ST_VEHICLE_MODEL";
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            /*OPCION 1*/
        //            cmd.Parameters.Add(new SqlParameter("Marca", Marca));
        //            //cmd.Parameters.Add(new SqlParameter("Modelo", Modelo));
        //            cmd.Parameters.Add(new SqlParameter("Categoria", Categoria));
        //            cmd.Parameters.Add(new SqlParameter("Recargo", Recargo));
        //            cmd.Parameters.Add(new SqlParameter("DeducibleMinimo", DeducibleMinimo));
        //            //cmd.Parameters.Add(new SqlParameter("HostName", HostName));
        //            cmd.Parameters.Add(new SqlParameter("Opcion", Opcion));
        //            /*OPCION 1*/
        //            //************************************************************************
        //            /*OPCION 2*/
        //            cmd.Parameters.Add(new SqlParameter("Descripcion", Descripcion));
        //            cmd.Parameters.Add(new SqlParameter("User", User));
        //            /*OPCION 2*/
        //            //************************************************************************
        //            /*OPCION 3*/
        //            cmd.Parameters.Add(new SqlParameter("Vehicle_Type_Id", Vehicle_Type_Id));
        //            cmd.Parameters.Add(new SqlParameter("Model_Desc", Model_Desc));
        //            cmd.Parameters.Add(new SqlParameter("Pos_Flag", Pos_Flag));
        //            cmd.Parameters.Add(new SqlParameter("Core_id", Core_id));
        //            /*OPCION 3*/


        //            var Exec = (cmd.ExecuteScalar());
        //        }
        //    }

        //}
        /// <summary>
        /// METODO PARA MODIFICAR LOS DATOS DE LOS MODELOS
        /// </summary>
        /// <param name="Categoria"></param>
        /// <param name="Recargo"></param>
        /// <param name="DeducibleMinimo"></param>
        /// <param name="Modelo"></param>
        /// <param name="Opcion"></param>
        /// <param name="Descripcion"></param>
        /// <param name="Usuario"></param>
        /// <param name="Estatus"></param>
        /// <param name="coreId"></param>
        /// <param name="Vehicle_Type_Id"></param>
        /// <param name="Model_Desc"></param>
        /// <param name="Pos_Flag"></param>
        /// <param name="User"></param>
        /// <param name="Make_Id"></param>
        /// <param name="Model_Id"></param>
        //public void Modificar_Modelos(string Categoria, decimal Recargo, int DeducibleMinimo, int Modelo, int Opcion, string Descripcion, string Usuario, int Estatus,
        //    int coreId, int Vehicle_Type_Id, string Model_Desc, int Pos_Flag, int User, int Make_Id, int Model_Id)
        //{
        //    using (GlobalDbContext context = new GlobalDbContext())
        //    {
        //        using (var cmd = context.Database.Connection.CreateCommand())
        //        {
        //            context.Database.Connection.Open();
        //            cmd.CommandText = "SP_MODIFICAR_MODELOS";
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            /*OPCION 1*/
        //            cmd.Parameters.Add(new SqlParameter("Categoria", Categoria));
        //            cmd.Parameters.Add(new SqlParameter("Recargo", Recargo));
        //            cmd.Parameters.Add(new SqlParameter("DeducibleMinimo", DeducibleMinimo));
        //            cmd.Parameters.Add(new SqlParameter("Modelo", Modelo));
        //            cmd.Parameters.Add(new SqlParameter("Opcion", Opcion));
        //            /*OPCION 1*/
        //            /*OPCION 2*/
        //            cmd.Parameters.Add(new SqlParameter("Descripcion", Descripcion));
        //            cmd.Parameters.Add(new SqlParameter("Usuario", Usuario));
        //            cmd.Parameters.Add(new SqlParameter("Estatus", Estatus));
        //            cmd.Parameters.Add(new SqlParameter("coreId", coreId));
        //            /*OPCION 2*/
        //            /*OPCION 3 y 4*/
        //            cmd.Parameters.Add(new SqlParameter("Vehicle_Type_Id", Vehicle_Type_Id));
        //            cmd.Parameters.Add(new SqlParameter("Model_Desc", Model_Desc    ));
        //            cmd.Parameters.Add(new SqlParameter("Pos_Flag", Pos_Flag));
        //            cmd.Parameters.Add(new SqlParameter("User", User));
        //            cmd.Parameters.Add(new SqlParameter("Make_Id", Make_Id));
        //            cmd.Parameters.Add(new SqlParameter("Model_Id", Model_Id));
        //            /*OPCION 3 y 4*/
        //            var Exec = (cmd.ExecuteScalar());
        //        }
        //    }

        //}

        //public void Registrar_Global(string Make_Desc, string Model_Desc, int Vehicle_Type_Id, int Core_Id)
        //{
        //    using (GlobalDbContext context = new GlobalDbContext())
        //    {
        //        using (var cmd = context.Database.Connection.CreateCommand())
        //        {
        //            context.Database.Connection.Open();
        //            cmd.CommandText = "RegistroMarcaModelo";
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("Make_Desc", Make_Desc));
        //            cmd.Parameters.Add(new SqlParameter("Model_Desc", Model_Desc));
        //            cmd.Parameters.Add(new SqlParameter("Vehicle_Type_Id", Vehicle_Type_Id));
        //            cmd.Parameters.Add(new SqlParameter("Core_Id", Core_Id));
        //            var Exec = (cmd.ExecuteScalar());
        //        }
        //    }

        //}


        public IEnumerable<TipodeVehiculo> Llenar_Tipo_Vehiculos()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                IEnumerable<TipodeVehiculo> RetornarValue = context.Database.SqlQuery<TipodeVehiculo>("select * from Global.VW_ST_VEHICLE_TYPE").ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<TipodeVehiculo> Llenar_Tipo_Vehiculos(string Vehicle_Type_Desc)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                IEnumerable<TipodeVehiculo> RetornarValue = context.Database.SqlQuery<TipodeVehiculo>("select * from Global.VW_ST_VEHICLE_TYPE where  Vehicle_Type_Desc LIKE '%'+@Vehicle_Type_Desc+'%'",
                    new SqlParameter("Vehicle_Type_Desc", Vehicle_Type_Desc)).ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<CategoriaVehiculos> Categoria_Vehiculos()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                IEnumerable<CategoriaVehiculos> RetornarValue = context.Database.SqlQuery<CategoriaVehiculos>("select * from Global.VW_ConfgCategoriaVehiculo").ToList();
                return RetornarValue.ToList();
            }
        }

        /// <summary>
        /// Muestra todos lso intermediarios registrados
        /// </summary>
        /// <returns></returns>

        public IEnumerable<TasaIntermediario> Mostrar_Intermediarios()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                IEnumerable<TasaIntermediario> RetornarValue = context.Database.SqlQuery<TasaIntermediario>("select * from VW_Consultar_intermediario").ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<TasaIntermediario> Mostrar_Intermediarios(int codigo)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                IEnumerable<TasaIntermediario> RetornarValue = context.Database.SqlQuery<TasaIntermediario>("select * from VW_Consultar_intermediario_ALL where codigo=@codigo",
                    new SqlParameter("codigo", codigo)).ToList();
                return RetornarValue.ToList();
            }
        }
        public void connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            // this gets the print statements (maybe the error statements?)
            var outputFromStoredProcedure = e.Message;
            MsgSql = e.Message;

        }

        #endregion
        #region SysFlex base de Datos
        public void Registrar_Marcas_Modelos_All(string Make_Desc, string Model_Desc, int Vehicle_Type_Id, int Categoria, string Estatus)
        {
            using (SysFlexDbContext_NC context = new SysFlexDbContext_NC())
            {

                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    //string Qry = "RegistroMarcaModeloALL";
                    string Qry = "RegistroMarcaModeloALL_New";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Make_Desc", Make_Desc));
                    cmd.Parameters.Add(new SqlParameter("Model_Desc", Model_Desc));
                    cmd.Parameters.Add(new SqlParameter("Vehicle_Type_Id", Vehicle_Type_Id));
                    cmd.Parameters.Add(new SqlParameter("Categoria", Categoria));
                    cmd.Parameters.Add(new SqlParameter("Estatus", Estatus));
                    connection.InfoMessage += connection_InfoMessage;
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public void Agregar_TasaIntermediarios(int Codigo, string Usuario)
        {

            using (SysFlexDbContext context = new SysFlexDbContext())
            {

                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    string Qry = "AsignarTasaIntermediario";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Intermediario", Codigo);
                    cmd.Parameters.AddWithValue("@Usuario ", Usuario);
                    connection.InfoMessage += connection_InfoMessage;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// CONSULTO LA MARCA EN SYSFLEX PARA OBTENER EL CODIGO DE LA MARCA
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public IEnumerable<MarcasSysFlex> Consultar_Marcas_SysFlex(string Descripcion)
        {
            using (SysFlexDbContext context = new SysFlexDbContext())
            {
                IEnumerable<MarcasSysFlex> RetornarValue = context.Database.SqlQuery<MarcasSysFlex>("select * from VW_CONSULTAR_MARCA_SysFlex where Descripcion=@Descripcion",
                    new SqlParameter("Descripcion", Descripcion)).ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<Modelos_SysFlex> Consultar_Modelos_SysFlex(string MODEL_DESC, string MAKE_DESC)
        {
            using (SysFlexDbContext context = new SysFlexDbContext())
            {
                IEnumerable<Modelos_SysFlex> RetornarValue = context.Database.SqlQuery<Modelos_SysFlex>("select * from VW_CONSULTAR_MODELOS_SysFlex where Model_Desc=@MODEL_DESC and Make_Desc=@MAKE_DESC ",
                    new SqlParameter("MODEL_DESC", MODEL_DESC),
                     new SqlParameter("MAKE_DESC", MAKE_DESC)).ToList();
                return RetornarValue.ToList();
            }
        }
        #endregion
        #region BASE DE DATOS POS_SITE
        //###########################################################################################################################################
        //######################################## DESARROLLO #######################################################################################
        //###########################################################################################################################################
        /// <summary>
        /// Consulto los modelos en Pos_Site para ver si Existe.
        /// </summary>
        /// <param name="Make_Id"></param>
        /// <param name="Model_desc"></param>;
        /// <returns></returns>
        //public IEnumerable<Modelos_Pos_Site> Consultar_Modelos_Pos_Site(int Make_Id, string Model_desc)
        //{
        //    using (GlobalDbContext context = new GlobalDbContext())
        //    {
        //        IEnumerable<Modelos_Pos_Site> RetornarValue = context.Database.SqlQuery<Modelos_Pos_Site>("select * from VW_CONSULAR_MODELO_Pos_Site where Make_Id=@Make_Id and Model_desc = @Model_desc ",
        //            new SqlParameter("Make_Id", Make_Id),
        //            new SqlParameter("Model_desc", Model_desc)).ToList();
        //        return RetornarValue.ToList();
        //    }
        //}
        //public IEnumerable<Modelos> Mostrar_Modelos_x_Marcas(int Make_Id)
        //{
        //    using (GlobalDbContext context = new GlobalDbContext())
        //    {
        //        IEnumerable<Modelos> RetornarValue = context.Database.SqlQuery<Modelos>("select * from VW_CONSULTAR_MODELOS where Make_Id=@Make_Id",
        //            new SqlParameter("Make_Id", Make_Id)).ToList();
        //        return RetornarValue.ToList();
        //    }
        //}

        //public IEnumerable<Modelos> Mostrar_Modelos_x_Marcas()
        //{
        //    using (GlobalDbContext context = new GlobalDbContext())
        //    {
        //        IEnumerable<Modelos> RetornarValue = context.Database.SqlQuery<Modelos>("select * from VW_CONSULTAR_MODELOS");
        //        //new SqlParameter("Make_Id", Make_Id)).ToList();
        //        return RetornarValue.ToList();
        //    }
        //}
        //###########################################################################################################################################
        //######################################## DESARROLLO #######################################################################################
        //###########################################################################################################################################
        public IEnumerable<Modelos_Pos_Site> Consultar_Modelos_Pos_Site(int Make_Id, string Model_desc)
        {
            using (Pos_SiteDbContext context = new Pos_SiteDbContext())
            {
                IEnumerable<Modelos_Pos_Site> RetornarValue = context.Database.SqlQuery<Modelos_Pos_Site>("select * from VW_CONSULAR_MODELO_Pos_Site where Make_Id=@Make_Id and Model_desc = @Model_desc ",
                    new SqlParameter("Make_Id", Make_Id),
                    new SqlParameter("Model_desc", Model_desc)).ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<Modelos> Mostrar_Modelos_x_Marcas(int Make_Id)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                IEnumerable<Modelos> RetornarValue = context.Database.SqlQuery<Modelos>("select * from VW_CONSULTAR_MODELOS where Make_Id=@Make_Id",
                    new SqlParameter("Make_Id", Make_Id)).ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<Modelos> Mostrar_Modelos_x_Marcas()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                IEnumerable<Modelos> RetornarValue = context.Database.SqlQuery<Modelos>("select * from VW_CONSULTAR_MODELOS");
                //new SqlParameter("Make_Id", Make_Id)).ToList();
                return RetornarValue.ToList();
            }
        }

        #endregion


        #region Manejo de Cambio de Cartera
        public IEnumerable<Agentes> Mostrar_Agentes(string Agent_Id)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                IEnumerable<Agentes> RetornarValue = context.Database.SqlQuery<Agentes>("select * from VW_GET_NAME_AGENT where Agent_Id=@Agent_Id",
                new SqlParameter("Agent_Id", Agent_Id)).ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<Agentes> Mostrar_Agentes()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                IEnumerable<Agentes> RetornarValue = context.Database.SqlQuery<Agentes>("select * from VW_GET_NAME_AGENT");
                return RetornarValue.ToList();
            }
        }

        #endregion


        public IEnumerable<CapacidadTipoVehiculo> Mostrar_CapacidadTipoVehiculo(int Tipo)
        {
            using (SysFlexDbContext context = new SysFlexDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<CapacidadTipoVehiculo> RetornarValue = context.Database.SqlQuery<CapacidadTipoVehiculo>("SELECT [TipoVehiculo],[IdListaHeader],[Descripcion] FROM [SysFlexSeguros_NC].[dbo].[CapacidadTipoVehiculo]where Idioma = 3 AND TipoVehiculo=@Tipo",
                    new SqlParameter("Tipo", Tipo)).ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<SPCBuscaTipoCapacidadModelo> SPCBuscaTipoCapacidadModelo(string DescTipoListasGeneral)
        {
            using (SysFlexDbContext_NC context = new SysFlexDbContext_NC())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<SPCBuscaTipoCapacidadModelo> RetornarValue = context.Database.SqlQuery<SPCBuscaTipoCapacidadModelo>("EXEC ConfiguracionSeguros.SPCBuscaTipoCapacidadModelo @DescTipoListasGeneral",
                    new SqlParameter("DescTipoListasGeneral", DescTipoListasGeneral)).ToList();
                return RetornarValue.ToList();
            }
        }

        public IEnumerable<CapacidadTipoVehiculo> Capacidad_Vehiculo(/*int IdMarca,*/ string Descripcion)
        {
            using (SysFlexDbContext context = new SysFlexDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<CapacidadTipoVehiculo> RetornarValue = context.Database.SqlQuery<CapacidadTipoVehiculo>("EXEC SP_BUSCA_CAPACIDAD_SYSFLEX_NC @IDDescripcion ",
                    //new SqlParameter("IdMarca", IdMarca),
                    new SqlParameter("IDDescripcion ", Descripcion)).ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<CapacidadTipoVehiculo> ALL_Capacidad_Vehiculo()
        {
            using (SysFlexDbContext context = new SysFlexDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<CapacidadTipoVehiculo> RetornarValue = context.Database.SqlQuery<CapacidadTipoVehiculo>("SELECT * FROM VW_ALL_CAPACIDADES").ToList();
                return RetornarValue.ToList();
            }
        }
        /// <summary>
        /// muestra las provincias de acuerdo al pais
        /// </summary>
        /// <param name="Country_Id"></param>
        /// <returns></returns>
        public IEnumerable<Provincias> Mostrar_Provincias_x_Pais()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION

                IEnumerable<Provincias> RetornarValue = context.Database.SqlQuery<Provincias>("SELECT * FROM VW_GET_ALL_PROVINCIES").ToList();
                return RetornarValue.ToList();
                /*
                IEnumerable<Provincias> RetornarValue = context.Database.SqlQuery<Provincias>("SELECT * FROM VW_GET_ALL_PROVINCIES where [Country_Id]=@Country_Id",
                    new SqlParameter("Country_Id", Country_Id)).ToList();
                return RetornarValue.ToList();*/
            }

        }

        public IEnumerable<Pais> Mostrar_Pais()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<Pais> RetornarValue = context.Database.SqlQuery<Pais>("select * FROM VW_GET_ALL_COUNTRIES").ToList();
                return RetornarValue.ToList();
            }
        }

        public IEnumerable<Ciudades> Mostrar_ciudades_x_Provincias(/*string pais, string Ciudad*/)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<Ciudades> RetornarValue = context.Database.SqlQuery<Ciudades>("select * FROM VW_GET_ALL_CITIES_X_CONTRIES").ToList();
                return RetornarValue.ToList();
            }
        }


        public IEnumerable<Modelos> Mostrar_Detalle_Modelos(int IdMarca)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<Modelos> RetornarValue = context.Database.SqlQuery<Modelos>("SP_SPCBuscaModeloVehiculo @IdMarca",
                    new SqlParameter("IdMarca", IdMarca)).ToList();
                return RetornarValue.ToList();
            }
        }

        public IEnumerable<Modelos> Mostrar_Detalle_Modelos(int IdMarca, int idModelo)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<Modelos> RetornarValue = context.Database.SqlQuery<Modelos>("SP_SPCBuscaModeloVehiculo @IdMarca, @IdModelo",
                    new SqlParameter("IdMarca", IdMarca),
                    new SqlParameter("idModelo", idModelo)).ToList();
                return RetornarValue.ToList();
            }
        }

        public DataTable GET_VERSION_VEHICULO(int MARCA, int MODELOID)
        {

            using (GlobalDbContext context = new GlobalDbContext())
            {

                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    string Qry = "SP_GET_VERSION_VEHICULO";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MARCA", MARCA);
                    cmd.Parameters.AddWithValue("@MODELOID", MODELOID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;

                }
            }

        }
        public DataTable GET_TIPO_VEHICULO(int MARCA, int MODELOID)
        {

            using (GlobalDbContext context = new GlobalDbContext())
            {

                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    string Qry = "SP_GET_TIPO_VEHICULO";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MARCA", MARCA);
                    cmd.Parameters.AddWithValue("@MODELOID", MODELOID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;

                }
            }

        }

        //public List<SPCBuscaTipoCapacidadModelo> SPCBuscaTipoCapacidadModelo(string DescTipoListasGeneral)
        //{
        //    using (SysFlexDbContext_NC context = new SysFlexDbContext_NC())
        //    {

        //        var connection = context.Database.Connection as SqlConnection;

        //        using (connection)
        //        {
        //            connection.Open();
        //            string Qry = "ConfiguracionSeguros.SPCBuscaTipoCapacidadModelo";
        //            SqlCommand cmd = new SqlCommand(Qry, connection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@DescTipoListasGeneral", DescTipoListasGeneral);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            var datos = new List<SPCBuscaTipoCapacidadModelo>(dt.Rows.Count);
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                var values = row.ItemArray;
        //                var dato = new SPCBuscaTipoCapacidadModelo()
        //                {
        //                    IdListaHeader = (int)values[2],
        //                    DescIdioma = (string)values[1]
        //                };
        //                datos.Add(dato);
        //            }
        //            return datos;


        //        }
        //    }
        //}

        public void Registrar_VERSION_CATEGORIA_TIPO(int IdModelo, int idVersion, int IdTipo, int idCategoria, int idCapacidad, string Opcion)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {

                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    string Qry = "SP_INSERT_VERSION_CATEGORIA_TIPO";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdModelo", IdModelo));
                    cmd.Parameters.Add(new SqlParameter("idVersion", idVersion));
                    cmd.Parameters.Add(new SqlParameter("IdTipo", IdTipo));
                    cmd.Parameters.Add(new SqlParameter("idCategoria", idCategoria));
                    cmd.Parameters.Add(new SqlParameter("idCapacidad", idCapacidad));
                 
                    cmd.Parameters.Add(new SqlParameter("Opcion", Opcion));

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void Modificar_VERSION_CATEGORIA_TIPO(int IdModelo, int idVersion, int IdTipo, int idCategoria, int idCapacidad, int idCapacidadViejo, string Opcion)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {

                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    string Qry = "SP_UPDATE_VERSION_CATEGORIA_TIPO";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdModelo", IdModelo));
                    cmd.Parameters.Add(new SqlParameter("idVersion", idVersion));
                    cmd.Parameters.Add(new SqlParameter("IdTipo", IdTipo));
                    cmd.Parameters.Add(new SqlParameter("idCategoria", idCategoria));
                    cmd.Parameters.Add(new SqlParameter("idCapacidad", idCapacidad));
                    cmd.Parameters.Add(new SqlParameter("idCapacidadViejo", idCapacidadViejo));
                    cmd.Parameters.Add(new SqlParameter("Opcion", Opcion));
                    
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public void Eliminar_VERSION_CATEGORIA_TIPO(int IdModelo)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {

                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    string Qry = "SP_DELETE_VERSION_CATEGORIA_TIPO";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdModelo", IdModelo));
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public IEnumerable<IdVehiculo> GET_ID_VEHICULO(string Descripcion)
        {
            using (SysFlexDbContext context = new SysFlexDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<IdVehiculo> RetornarValue = context.Database.SqlQuery<IdVehiculo>("select * from GET_ID_VEHICULO where  Descripcion=@Descripcion",
                    new SqlParameter("Descripcion", Descripcion)).ToList();
                return RetornarValue.ToList();
            }
        }

        public void Registrar_SET_ST_PROVINCE_GLOBAL_POS_SITE_POS_SITE_ATL_SYSFLEX(int Country_Id, int Domesticreg_Id, int State_Prov_Id, string State_Prov_Desc, bool State_Prov_Status,
            int UsrId, int CodPais, int CodZona, int Codigo, string Usuario)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    string Qry = "[Global].[SP_SET_ST_PROVINCE_GLOBAL_POS_SITE_POS_SITE_ATL_SYSFLEX]";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Country_Id", Country_Id));
                    cmd.Parameters.Add(new SqlParameter("Domesticreg_Id", Domesticreg_Id));
                    cmd.Parameters.Add(new SqlParameter("State_Prov_Id", State_Prov_Id));
                    cmd.Parameters.Add(new SqlParameter("State_Prov_Desc", State_Prov_Desc.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("State_Prov_Status", State_Prov_Status));
                    cmd.Parameters.Add(new SqlParameter("UsrId", UsrId));
                    cmd.Parameters.Add(new SqlParameter("CodPais", CodPais));
                    cmd.Parameters.Add(new SqlParameter("CodZona", CodZona));
                    cmd.Parameters.Add(new SqlParameter("Codigo", Codigo));
                    cmd.Parameters.Add(new SqlParameter("Usuario", Usuario));
                    connection.InfoMessage += connection_InfoMessage;
                   
                    SqlDataReader reader = cmd.ExecuteReader();
                    string returnValue;
                    while (reader.Read())
                    {
                        returnValue = reader[0].ToString();
                        MsgSql = returnValue;
                    }
                   
                    //cmd.ExecuteNonQuery();
                }
            } 

        }

        public void Registrar_SET_ST_CITY_GLOBAL_POS_SITE_POS_SITE_ATL_SYSFLEX(int Country_Id, int Domesticreg_Id, int State_Prov_Id, int City_Id, string City_Desc, bool City_Status,
            int UsrId, int CodPais, int CodZona, int CodProvincia, int Codigo, string Usuario, int CodMunicipio, int @CodigoS)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    string Qry = "[Global].[SP_SET_ST_CITY_GLOBAL_POS_SITE_POS_SITE_ATL_SYSFLEX]";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Country_Id", Country_Id));
                    cmd.Parameters.Add(new SqlParameter("Domesticreg_Id", Domesticreg_Id));
                    cmd.Parameters.Add(new SqlParameter("State_Prov_Id", State_Prov_Id));
                    cmd.Parameters.Add(new SqlParameter("City_Id", City_Id));
                    cmd.Parameters.Add(new SqlParameter("City_Desc", City_Desc.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("City_Status", City_Status));
                    cmd.Parameters.Add(new SqlParameter("UsrId", UsrId));
                    cmd.Parameters.Add(new SqlParameter("CodPais", CodPais));
                    cmd.Parameters.Add(new SqlParameter("CodZona", CodZona));
                    cmd.Parameters.Add(new SqlParameter("CodProvincia", CodProvincia));
                    cmd.Parameters.Add(new SqlParameter("Codigo", Codigo));
                    cmd.Parameters.Add(new SqlParameter("Usuario", Usuario));
                    cmd.Parameters.Add(new SqlParameter("CodMunicipio", CodMunicipio));
                    cmd.Parameters.Add(new SqlParameter("CodigoS", CodigoS));
                    connection.InfoMessage += connection_InfoMessage;

                    SqlDataReader reader = cmd.ExecuteReader();
                    string returnValue;
                    while (reader.Read())
                    {
                        returnValue = reader[0].ToString();
                        MsgSql = returnValue;
                    }

                    //cmd.ExecuteNonQuery();
                }
            }

        }

        public void Registrar_SET_SECTORES_SYSFLEX(int CodPais, int CodZona, int CodProvincia, int CodMunicipio, int Codigo, string Descripcion, string Usuario)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    string Qry = "[Global].[SP_SET_SECTORES_SYSFLEX]";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("CodPais", CodPais));
                    cmd.Parameters.Add(new SqlParameter("CodZona", CodZona));
                    cmd.Parameters.Add(new SqlParameter("CodProvincia", CodProvincia));
                    cmd.Parameters.Add(new SqlParameter("CodMunicipio", CodMunicipio));
                    cmd.Parameters.Add(new SqlParameter("Codigo", Codigo));
                    cmd.Parameters.Add(new SqlParameter("Descripcion", Descripcion.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("Usuario", Usuario));
                    connection.InfoMessage += connection_InfoMessage;

                    SqlDataReader reader = cmd.ExecuteReader();
                    string returnValue;
                    while (reader.Read())
                    {
                        returnValue = reader[0].ToString();
                        MsgSql = returnValue;
                    }

                    //cmd.ExecuteNonQuery();
                }
            }

        }

        public void Registrar_SET_UBICACIONES_SYSFLEX(int CodPais, int CodZona, int CodProvincia, int CodMunicipio, int CodSector, int Codigo, string Descripcion, string Usuario)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    string Qry = "[Global].[SP_SET_UBICACIONES_SYSFLEX]";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("CodPais", CodPais));
                    cmd.Parameters.Add(new SqlParameter("CodZona", CodZona));
                    cmd.Parameters.Add(new SqlParameter("CodProvincia", CodProvincia));
                    cmd.Parameters.Add(new SqlParameter("CodMunicipio", CodMunicipio));
                    cmd.Parameters.Add(new SqlParameter("CodSector", CodSector));
                    cmd.Parameters.Add(new SqlParameter("Codigo", Codigo));
                    cmd.Parameters.Add(new SqlParameter("Descripcion", Descripcion.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("Usuario", Usuario));
                    connection.InfoMessage += connection_InfoMessage;

                    SqlDataReader reader = cmd.ExecuteReader();
                    string returnValue;
                    while (reader.Read())
                    {
                        returnValue = reader[0].ToString();
                        MsgSql = returnValue;
                    }

                    //cmd.ExecuteNonQuery();
                }
            }

        }

        public void Registrar_SET_ZONAS_SYSFLEX(int CodPais, int Codigo, string Descripcion, string Usuario)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    string Qry = "[Global].[SP_SET_ZONAS_SYSFLEX]";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("CodPais", CodPais));
                    cmd.Parameters.Add(new SqlParameter("Codigo", Codigo));
                    cmd.Parameters.Add(new SqlParameter("Descripcion", Descripcion.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("Usuario", Usuario));
                    connection.InfoMessage += connection_InfoMessage;

                    SqlDataReader reader = cmd.ExecuteReader();
                    string returnValue;
                    while (reader.Read())
                    {
                        returnValue = reader[0].ToString();
                        MsgSql = returnValue;
                    }

                    //cmd.ExecuteNonQuery();
                }
            }

        }
        public void Registrar_SET_ST_CITY(int CountryId, int State_Prov_Id, int City_Id)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                var connection = context.Database.Connection as SqlConnection;

                using (connection)
                {
                    connection.Open();
                    string Qry = "[Global].[SP_SET_ST_CITY_SYSFLEX]";
                    SqlCommand cmd = new SqlCommand(Qry, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("CountryId", CountryId));
                    cmd.Parameters.Add(new SqlParameter("State_Prov_Id", State_Prov_Id));
                    cmd.Parameters.Add(new SqlParameter("City_Id", City_Id));
                    connection.InfoMessage += connection_InfoMessage;

                    SqlDataReader reader = cmd.ExecuteReader();
                    string returnValue;
                    while (reader.Read())
                    {
                        returnValue = reader[0].ToString();
                        MsgSql = returnValue;
                    }

                    //cmd.ExecuteNonQuery();
                }
            }

        }

        public IEnumerable<Localidades_SysFlex> Mostrar_GET_LOCALIDADES_SYSFLEX(/*string PAIS, string PROVINCIA*/)
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<Localidades_SysFlex> RetornarValue = context.Database.SqlQuery<Localidades_SysFlex>("SP_GET_LOCALIDADES_SYSFLEX").ToList();
               /* IEnumerable<Localidades_SysFlex> RetornarValue = context.Database.SqlQuery<Localidades_SysFlex>("SP_GET_LOCALIDADES_SYSFLEX @PAIS,@PROVINCIA",
                new SqlParameter("PAIS", PAIS),
                new SqlParameter("PROVINCIA", PROVINCIA)
                ).ToList();*/
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<Sector> GET_ALL_SECTOR()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<Sector> RetornarValue = context.Database.SqlQuery<Sector>("select * from VW_GET_ALL_SECTOR").ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<ALL_LOCATION> ALL_LOCATIONS()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<ALL_LOCATION> RetornarValue = context.Database.SqlQuery<ALL_LOCATION>("select * from VW_GET_ALL_LOCATION").ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<Zonas> All_Zonas()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<Zonas> RetornarValue = context.Database.SqlQuery<Zonas>("select * from VW_GET_ALL_ZONE").ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<ProvinciasSysFlex> All_Provincias()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<ProvinciasSysFlex> RetornarValue = context.Database.SqlQuery<ProvinciasSysFlex>("select * from VW_GET_ALL_PROVINCIAS").ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<Municipios> All_Municipios()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<Municipios> RetornarValue = context.Database.SqlQuery<Municipios>("select * from VW_GET_ALL_MUNICIPIOS").ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<ProvinciasSysFlex> All_ProvinciasSysFlex()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<ProvinciasSysFlex> RetornarValue = context.Database.SqlQuery<ProvinciasSysFlex>("select * from VW_GET_ALL_ProvinciasSysFlex").ToList();
                return RetornarValue.ToList();
            }
        }
        public IEnumerable<PaisSysFlex> All_PaisesSysFlex()
        {
            using (GlobalDbContext context = new GlobalDbContext())
            {
                //CREAR METODO EN POS_SITE DE PRODUCCION
                IEnumerable<PaisSysFlex> RetornarValue = context.Database.SqlQuery<PaisSysFlex>("select * from VW_GET_ALL_COUNTRIES_SYSFLEX").ToList();
                return RetornarValue.ToList();
            }
        }

    }
}