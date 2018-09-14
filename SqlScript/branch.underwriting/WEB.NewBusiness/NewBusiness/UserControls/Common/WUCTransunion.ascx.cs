﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;
using WEB.NewBusiness.TransunionServiceReference;

namespace WEB.NewBusiness.NewBusiness.UserControls.Common
{
    /// <summary>
    /// Author: Lic. Carlos Ml. Lebron
    /// </summary>
    public partial class WUCTransunion : UC, IUC
    {
        public void Translator(string Lang) { }
        public void ReadOnlyControls(bool isReadOnly) { }
        public void save()
        {


        }
        public void edit() { }
        public void ClearData() { }

        public string KeyRiesgo
        {
            get { return ViewState["KeyRiesgo"].ToString(); }
            set { ViewState["KeyRiesgo"] = value; }
        }

        const string NumberFormat = "#0,0.00";
        const string defaultValue = "0";
        public string pCedulaOrDriverLicense
        {
            get
            {
                return ViewState["CedulaOrDriverLicense"].ToString();
            }

            set
            {
                ViewState["CedulaOrDriverLicense"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (
                ObjServices.IsSuscripcionQuotRole ||
                ObjServices.IsSucripcionDirectorQuotRole ||
                ObjServices.IsSuscripcionManagerQuotRole
               )
            {
                trDirecciones.Visible = false;
                trTelefonos.Visible = false;
            }
        }

        public void FillData()
        {
            StringBuilder encabezado;
            StringBuilder bodyContent;
            var data = new ReportHCIDatosGenerales();

            try 
            {
                #region Transunion
                var DefaulltPassword = ConfigurationManager.AppSettings["TransunionDefaultPassword"];
                var user = ConfigurationManager.AppSettings["TransunionUserName"];
                var pass = ConfigurationManager.AppSettings["TransunionPass"];
                #endregion

                var TransunionClient = ObjServices.TransunionServiceLogIn(user, pass, DefaulltPassword);

                var param = new Identification
                {
                    Cedula = pCedulaOrDriverLicense,
                    UserId = string.IsNullOrEmpty(ObjServices.UserName) ? "defaultuser" : ObjServices.UserName
                };


                data = TransunionClient.GetReportHCI(param);

                if (data == null)
                    return;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("cedula no.") > 0)
                    this.MessageBox(RESOURCE.UnderWriting.NewBussiness.Resources.IDnoValid);
                else
                    this.MessageBox(string.Format("ExceptionMessage {0} TraceStack {1}", ex.Message, ex.StackTrace).MyRemoveInvalidCharacters(), Title: "Error", Width: 800);

                return;
            }

            var Photo = Utility.GetBase64FromImage(data.Photo);
            imgPhoto.ImageUrl = Photo;
            ltScore.Text = data.Score.ToString();
            ltcedula.Text = data.Cedula;
            ltNombres.Text = HttpUtility.HtmlEncode(string.Format("{0} {1}", data.PrimerNombre, data.SegundoNombre));
            ltApellidos.Text = HttpUtility.HtmlEncode(string.Format("{0} {1}", data.PrimerApellido, data.SegundoApellido));
            ltFechaNac.Text = HttpUtility.HtmlEncode(string.Format("{0:dd/MMM/yyyy}", data.FechaNacimiento));
            ltNacionalidad.Text = HttpUtility.HtmlEncode(data.LugarNacimiento);
            ltPassaporte.Text = HttpUtility.HtmlEncode(data.Pasaporte);
            var EstatusPerfil = string.Empty;

            var TRiesgo = (Utility.TipoRiesgo)Enum.Parse(typeof(Utility.TipoRiesgo), KeyRiesgo);

            switch (TRiesgo)
            {
                case Utility.TipoRiesgo.RA:
                    EstatusPerfil = "Declinado";
                    break;
                case Utility.TipoRiesgo.RM:
                case Utility.TipoRiesgo.RB:
                    EstatusPerfil = "Aprobado";
                    break;
                default:
                    EstatusPerfil = "N/A";
                    break;
            }

            ltStatusPerfil.Text = string.Format("Perfil {0}", EstatusPerfil);

            var Telefonos = data.Telefonos;

            var dCasa = data.Telefonos.FirstOrDefault(g => g.Tipo == "Casa");
            if (dCasa != null && !string.IsNullOrEmpty(dCasa.Numero))
                ltTelCasa.Text = string.Format("{0:(###)-###-####}", Convert.ToInt64(dCasa.Numero));

            var dTrabajo = data.Telefonos.FirstOrDefault(g => g.Tipo == "Trabajo");
            if (dTrabajo != null && !string.IsNullOrEmpty(dTrabajo.Numero))
                ltTrabajo.Text = string.Format("{0:(###)-###-####}", Convert.ToInt64(dTrabajo.Numero));

            var dCelular = data.Telefonos.FirstOrDefault(g => g.Tipo == "Celular");
            if (dCelular != null && !string.IsNullOrEmpty(dCelular.Numero))
                ltCelular.Text = string.Format("{0:(###)-###-####}", Convert.ToInt64(dCelular.Numero));

            ltEdad.Text = data.Edad.ToString() + HttpUtility.HtmlEncode(" Años");
            ltocupacion.Text = HttpUtility.HtmlEncode(data.Ocupacion);
            ltEstadoCivil.Text = HttpUtility.HtmlEncode(data.EstadoCivil);

            var dDirecciones = data.Direcciones;
            var Direcciones = new List<string>(0);

            if (dDirecciones.Any())
                foreach (var item in dDirecciones)
                    Direcciones.Add(HttpUtility.HtmlEncode(item.Descricion));

            ltDireccion.Text = Direcciones.Any() ? "<br/><br/>" + string.Join("<br/><br/>", Direcciones.ToArray())
                                                 : string.Empty;

            encabezado = new StringBuilder();
            bodyContent = new StringBuilder();

            encabezado.AppendLine("<tr class=\"bold flColor\">");
            encabezado.AppendLine("<td rowspan=\"2\" align=\"center\">Suscriptor</td>");
            encabezado.AppendLine("<td rowspan=\"2\" align=\"center\">cant. ctas</td>");
            encabezado.AppendLine("<td rowspan=\"2\" align=\"center\">Tipo cta</td>");
            encabezado.AppendLine("<td colspan=\"2\" align=\"center\">Monto de credito</td>");
            encabezado.AppendLine("<td colspan=\"2\" align=\"center\">balance actual</td>");
            encabezado.AppendLine("<td colspan=\"2\" align=\"center\">atraso actual</td>");
            encabezado.AppendLine("<td colspan=\"2\" align=\"center\">% utilizacion</td>");
            encabezado.AppendLine("</tr>");
            encabezado.AppendLine("<tr class=\"bold flColor\">");
            encabezado.AppendLine("<td align=\"center\">RD$</td>");
            encabezado.AppendLine("<td align=\"center\">US$</td>");
            encabezado.AppendLine("<td align=\"center\">RD$</td>");
            encabezado.AppendLine("<td align=\"center\">US$</td>");
            encabezado.AppendLine("<td align=\"center\">RD$</td>");
            encabezado.AppendLine("<td align=\"center\">US$</td>");
            encabezado.AppendLine("<td align=\"center\">RD$</td>");
            encabezado.AppendLine("<td align=\"center\">US$</td>");
            encabezado.AppendLine("</tr>");
            bodyContent.AppendLine(encabezado.ToString());
            /*           
             TC  = Tarjeta de credito
             TR  = Prestamo
             TEl = Telefono              
            */

            #region DESPLEGAR EL RESUMEN DE TRANSACCIONES
            var Resumen = data.ResumenTransacciones;

            if (Resumen != null && Resumen.Any())
            {
                var TCData = Resumen.Where(p => p.Categoria.ToUpper() == "TC");
                var PRCData = Resumen.Where(p => p.Categoria.ToUpper() == "PR");
                var TELData = Resumen.Where(p => p.Categoria.ToUpper() == "TEL");
                if (TCData.Any())
                    bodyContent.AppendLine(SetDataResumen(TCData.ToArray(), "TC").ToString());

                if (PRCData.Any())
                    bodyContent.AppendLine(SetDataResumen(PRCData.ToArray(), "PR").ToString());

                if (TELData.Any())
                    bodyContent.AppendLine(SetDataResumen(TELData.ToArray(), "TEL").ToString());

                bodyContent.AppendLine(SetDataResumen(Resumen.ToArray(), string.Empty, true).ToString());

                TblResumen.InnerHtml = bodyContent.ToString();
            }

            #endregion

            #region DESPLEGAR EL DETALLE DE CUENTAS ACTIVAS / VIGENTES
            var DetalleActivas = data.Activas;
            /*
                TARJETA CR
                PRESTAMO
                CELULAR
                TELCO
            */
            bodyContent = new StringBuilder();

            if (DetalleActivas != null && DetalleActivas.Any())
            {
                var TCDataActivas = DetalleActivas.Where(p => p.Categoria.ToUpper() == "TARJETA CR");
                var PRCDataActivas = DetalleActivas.Where(p => p.Categoria.ToUpper() == "PRESTAMO");
                var PRCONSUMODataActivas = DetalleActivas.Where(p => p.Categoria.ToUpper() == "PRESTAMO (CONSUMO)");
                var PRHIPOTECARIODataActivas = DetalleActivas.Where(p => p.Categoria.ToUpper() == "PRESTAMO (HIPOTECARIO)");

                var CELDataActivas = DetalleActivas.Where(p => p.Categoria.ToUpper() == "CELULAR");
                var TELDataActivas = DetalleActivas.Where(p => p.Categoria.ToUpper() == "TELCO");

                if (TCDataActivas.Any())
                    bodyContent.AppendLine(SetDataActivas(TCDataActivas.ToArray(), "TARJETA CR").ToString());

                if (PRCDataActivas.Any())
                    bodyContent.AppendLine(SetDataActivas(PRCDataActivas.ToArray(), "PRESTAMO").ToString());

                if (PRCONSUMODataActivas.Any())
                    bodyContent.AppendLine(SetDataActivas(PRCONSUMODataActivas.ToArray(), "PRESTAMO (CONSUMO)").ToString());

                if (PRHIPOTECARIODataActivas.Any())
                    bodyContent.AppendLine(SetDataActivas(PRCONSUMODataActivas.ToArray(), "PRESTAMO (HIPOTECARIO)").ToString());

                if (CELDataActivas.Any())
                    bodyContent.AppendLine(SetDataActivas(CELDataActivas.ToArray(), "CELULAR").ToString());

                if (TELDataActivas.Any())
                    bodyContent.AppendLine(SetDataActivas(TELDataActivas.ToArray(), "TELCO").ToString());

                //Generar el Grand total
                bodyContent.AppendLine(SetDataActivas(DetalleActivas.ToArray(), string.Empty, "DOP", true).ToString());
                bodyContent.AppendLine(SetDataActivas(DetalleActivas.ToArray(), string.Empty, "US", true).ToString());

                TblCuentasActivas.InnerHtml = bodyContent.ToString();
            }

            #endregion

            #region DESPLEGAR EL DETALLE DE LAS CUENTAS INACTIVAS / CERRADAS
            bodyContent = new StringBuilder();
            var DetalleInActivas = data.Inactivas;

            if (DetalleInActivas != null && DetalleInActivas.Any())
            {
                var TCDatainActivas = DetalleInActivas.Where(p => p.Categoria.ToUpper() == "TARJETA CR");
                var PRCDatainActivas = DetalleInActivas.Where(p => p.Categoria.ToUpper() == "PRESTAMO");
                var PRCONSUMODatainActivas = DetalleInActivas.Where(p => p.Categoria.ToUpper() == "PRESTAMO (CONSUMO)");
                var PRHIPOTECARIODatainActivas = DetalleInActivas.Where(p => p.Categoria.ToUpper() == "PRESTAMO (HIPOTECARIO)");

                var CELDatainActivas = DetalleInActivas.Where(p => p.Categoria.ToUpper() == "CELULAR");
                var TELDatainActivas = DetalleInActivas.Where(p => p.Categoria.ToUpper() == "TELCO");

                if (TCDatainActivas.Any())
                    bodyContent.AppendLine(SetDataInactivas(TCDatainActivas.ToArray(), "TARJETA CR").ToString());

                if (PRCDatainActivas.Any())
                    bodyContent.AppendLine(SetDataInactivas(PRCDatainActivas.ToArray(), "PRESTAMO").ToString());

                if (PRCONSUMODatainActivas.Any())
                    bodyContent.AppendLine(SetDataInactivas(PRCONSUMODatainActivas.ToArray(), "PRESTAMO (CONSUMO)").ToString());

                if (PRHIPOTECARIODatainActivas.Any())
                    bodyContent.AppendLine(SetDataInactivas(PRHIPOTECARIODatainActivas.ToArray(), "PRESTAMO (HIPOTECARIO)").ToString());

                if (CELDatainActivas.Any())
                    bodyContent.AppendLine(SetDataInactivas(CELDatainActivas.ToArray(), "CELULAR").ToString());

                if (TELDatainActivas.Any())
                    bodyContent.AppendLine(SetDataInactivas(TELDatainActivas.ToArray(), "TELCO").ToString());

                //Generar el Grand total
                bodyContent.AppendLine(SetDataInactivas(DetalleInActivas.ToArray(), string.Empty, "DOP", true).ToString());

                tblCuentasInactivas.InnerHtml = bodyContent.ToString();
            }


            #endregion

            #region DESPLEGAR LAS SENTENCIAS

            bodyContent = new StringBuilder();
            var Sentencias = data.InformacionPublicaJudicial;

            if (Sentencias != null && Sentencias.Any())
            {
                pnSentencias.Visible = true;
                lblNotaSentencia.Visible = false;

                bodyContent.AppendLine(SetSentencias(Sentencias.ToArray()).ToString());
                TblSentencias.InnerHtml = bodyContent.ToString();
            }
            else
            {
                lblNotaSentencia.Visible = true;
                pnSentencias.Visible = false;
            }

            #endregion
            udpTransunion.Update();
        }

        public void Initialize() { }
        public void Initialize(string KeyRiesgo)
        {
            this.KeyRiesgo = KeyRiesgo;
            ClearData();
            FillData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cadena"></param>
        /// <param name="groupNumber"></param>
        /// <returns></returns>
        private List<string> strToArrayGrouping(string cadena, int groupNumber)
        {
            var result = new List<string>(0);
            var dataTemp = new List<char>(0);
            var counter = 0;

            for (int i = 0; i < cadena.Length; i++)
            {
                var caracter = cadena[i];
                dataTemp.Add(caracter);
                counter++;

                if (counter == groupNumber)
                {
                    var x = string.Join("", dataTemp.ToArray());
                    result.Add(x);
                    counter = 0;
                    dataTemp = new List<Char>(0);
                }
            }

            return result;
        }

        /// <summary>
        /// Generar el grid del detalle de las cuentas activas
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Categoria"></param>
        /// <param name="Currency"></param>
        /// <param name="generateGranTotal"></param>
        /// <returns></returns>
        private StringBuilder SetDataActivas(ReportHCITransaccion[] data, string Categoria, string Currency = "", bool generateGranTotal = false)
        {
            var filas = new StringBuilder();
            var trEncabezado = "<tr class=\"head_sm\">{0}</tr>";
            var tdEncabezado = "<td colspan=\"11\"><em>{0}>></em> {1}</td>";
            var tr = "<tr class=\"body\">{0}</tr>";
            var td = "<td {0}>{1}</td>";
            var tdEx = "<table><tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr></table>";
            var trTotalGral = "<tr class=\"bold no_border_td\">{0}</tr>";
            var NewRow = string.Empty;
            var celda = string.Empty;
            List<string> Comportamiento;
            var TableEx = string.Empty;

            var GDataSuscriptor = data.Select(j => j.Suscriptor).Distinct();

            if (!generateGranTotal)
            {
                foreach (var itemSuscriptor in GDataSuscriptor)
                {
                    celda = string.Format(tdEncabezado, Categoria, itemSuscriptor);
                    //Generar la cabecera del grupo
                    NewRow = string.Format(trEncabezado, celda);
                    filas.AppendLine(NewRow);

                    var GData = data.Where(k => k.Suscriptor == itemSuscriptor);

                    foreach (var item in GData)
                    {
                        if (Categoria.ToUpper() == "TARJETA CR")
                        {
                            Comportamiento = strToArrayGrouping(item.Comportamiento, 3);
                            TableEx = string.Format(tdEx, Comportamiento[0], Comportamiento[1], Comportamiento[2], Comportamiento[3]);

                            //Pesos
                            celda = string.Concat(
                                                 string.Format(td, "", item.Estatus),
                                                 string.Format(td, "align=\"center\"", item.FechaUltimaAct),
                                                 string.Format(td, "align=\"center\"", item.FechaApertura),
                                                 string.Format(td, "align=\"center\"", item.FechaVencimiento),
                                                 string.Format(td, "align=\"right\" class=\"bold\"", "RD$"),
                                                 string.Format(td, "align=\"center\"", item.LimiteCredito > 0 ? item.LimiteCredito.ToString(NumberFormat, CultureInfo.InvariantCulture)
                                                                                                              : defaultValue),
                                                 string.Format(td, "align=\"center\"", item.Balance > 0 ? item.Balance.ToString(NumberFormat, CultureInfo.InvariantCulture)
                                                                                                      : defaultValue),

                                                 string.Format(td, "align=\"center\"", item.MontoAtraso > 0 ? item.MontoAtraso.ToString(NumberFormat, CultureInfo.InvariantCulture)
                                                                                                            : defaultValue),
                                                 string.Format(td, "align=\"center\"", item.MontoCuotas > 0 ? item.MontoCuotas.ToString(NumberFormat, CultureInfo.InvariantCulture)
                                                                                                            : defaultValue),
                                                 string.Format(td, "align=\"center\"", item.NoCuotas > 0 ? item.NoCuotas.ToString() : "-"),
                                                 string.Format(td, "align=\"center\"", TableEx)
                                                 );

                            NewRow = string.Format(tr, celda);
                            filas.AppendLine(NewRow);

                            if (!string.IsNullOrEmpty(item.U_LimiteCredito))
                            {
                                var LimiteCredito = decimal.Parse(item.U_LimiteCredito);
                                var Balance = decimal.Parse(item.U_Balance);
                                var MontoAtraso = decimal.Parse(item.U_MontoAtraso);
                                var MontoCuotas = decimal.Parse(item.U_MontoCuotas);

                                Comportamiento = strToArrayGrouping(item.Comportamiento, 3);
                                TableEx = string.Format(tdEx, Comportamiento[0], Comportamiento[1], Comportamiento[2], Comportamiento[3]);

                                //Dollares
                                celda = string.Concat(
                                                     string.Format(td, "", item.U_Estatus),
                                                     string.Format(td, "align=\"center\"", item.U_FechaUltimaAct),
                                                     string.Format(td, "align=\"center\"", item.U_FechaApertura),
                                                     string.Format(td, "align=\"center\"", item.U_FechaVencimiento),
                                                     string.Format(td, "align=\"right\" class=\"bold\"", "US$"),
                                                     string.Format(td, "align=\"center\"", LimiteCredito > 0 ? LimiteCredito.ToString(NumberFormat, CultureInfo.InvariantCulture)
                                                                                                           : defaultValue),

                                                     string.Format(td, "align=\"center\"", Balance > 0 ? Balance.ToString(NumberFormat, CultureInfo.InvariantCulture)
                                                                                                       : defaultValue),

                                                     string.Format(td, "align=\"center\"", MontoAtraso > 0 ? MontoAtraso.ToString(NumberFormat, CultureInfo.InvariantCulture)
                                                                                                           : defaultValue),

                                                     string.Format(td, "align=\"center\"", MontoCuotas > 0 ? MontoCuotas.ToString(NumberFormat, CultureInfo.InvariantCulture)
                                                                                                           : defaultValue),

                                                     string.Format(td, "align=\"center\"", item.U_NoCuotas.Trim() == "0" || string.IsNullOrEmpty(item.U_NoCuotas.Trim()) ? item.U_NoCuotas
                                                                                                                                                                         : "-"),
                                                     string.Format(td, "align=\"center\"", TableEx)
                                                     );

                                NewRow = string.Format(tr, celda);
                                filas.AppendLine(NewRow);
                            }
                        }
                        else
                        {
                            Comportamiento = strToArrayGrouping(item.Comportamiento, 3);
                            TableEx = string.Format(tdEx, Comportamiento[0], Comportamiento[1], Comportamiento[2], Comportamiento[3]);

                            //Pesos
                            celda = string.Concat(
                                                 string.Format(td, "", item.Estatus),
                                                 string.Format(td, "align=\"center\"", item.FechaUltimaAct),
                                                 string.Format(td, "align=\"center\"", item.FechaApertura),
                                                 string.Format(td, "align=\"center\"", item.FechaVencimiento),
                                                 string.Format(td, "align=\"right\" class=\"bold\"", "RD$"),
                                                 string.Format(td, "align=\"center\"", item.LimiteCredito > 0 ? item.LimiteCredito.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                                 string.Format(td, "align=\"center\"", item.Balance > 0 ? item.Balance.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                                 string.Format(td, "align=\"center\"", item.MontoAtraso > 0 ? item.MontoAtraso.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                                 string.Format(td, "align=\"center\"", item.MontoCuotas > 0 ? item.MontoCuotas.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                                 string.Format(td, "align=\"center\"", item.NoCuotas > 0 ? item.NoCuotas.ToString() : "-"),
                                                 string.Format(td, "align=\"center\"", TableEx)
                                                 );

                            NewRow = string.Format(tr, celda);
                            filas.AppendLine(NewRow);
                        }
                    }
                }
            }
            else
            {
                //Totalizar Pesos
                if (Currency.ToUpper() == "DOP")
                {
                    var dataBalanceActual = data.Where(h => h.Balance > 0);
                    var TBalanceActual = dataBalanceActual.Sum(o => o.Balance);
                    var TBalanceMora = data.Where(h => h.MontoAtraso > 0).Sum(o => o.MontoAtraso);
                    var TBalanceMontoCuota = data.Where(h => h.MontoCuotas > 0).Sum(o => o.MontoCuotas);

                    var TotalCelda = string.Concat(
                                                    string.Format(td, "align='right' colspan='6'", "TOTALES GENERALES RD$:"),
                                                    string.Format(td, "align='right'", TBalanceActual > 0 ? TBalanceActual.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                                    string.Format(td, "align='center'", TBalanceMora > 0 ? TBalanceMora.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                                    string.Format(td, "align='right'", TBalanceMontoCuota > 0 ? TBalanceMontoCuota.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                                    string.Format(td, "align='center'", "&nbsp;"),
                                                    string.Format(td, "", "-")
                                                  );

                    NewRow = string.Format(trTotalGral, TotalCelda);
                    filas.AppendLine(NewRow);
                }
                else
                    //Totalizar Dollar
                    if (Currency.ToUpper() == "US")
                    {
                        var dataBalanceActual = data.Where(h => h.U_Balance != string.Empty);
                        var TBalanceActual = dataBalanceActual.Sum(o => decimal.Parse(o.U_Balance));

                        var dataBalanceMora = data.Where(h => h.U_MontoAtraso != string.Empty);
                        var TBalanceMora = dataBalanceMora.Sum(o => decimal.Parse(o.U_MontoAtraso));

                        var dataMontoCuotas = data.Where(h => h.U_MontoCuotas != string.Empty);
                        var TBalanceMontoCuota = dataMontoCuotas.Sum(o => decimal.Parse(o.U_MontoCuotas));

                        var TotalCelda = string.Concat(
                                                        string.Format(td, "align='right' colspan='6'", "TOTALES GENERALES US$:"),
                                                        string.Format(td, "align='right'", TBalanceActual > 0 ? TBalanceActual.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                                        string.Format(td, "align='center'", TBalanceMora > 0 ? TBalanceMora.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                                        string.Format(td, "align='right'", TBalanceMontoCuota > 0 ? TBalanceMontoCuota.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                                        string.Format(td, "align='center'", "&nbsp;"),
                                                        string.Format(td, "", "-")
                                                        );

                        NewRow = string.Format(trTotalGral, TotalCelda);
                        filas.AppendLine(NewRow);
                    }

            }

            return filas;
        }

        /// <summary>
        /// Generar el grid del detalle de las cuentas inactivas
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Categoria"></param>
        /// <param name="Currency"></param>
        /// <param name="generateGranTotal"></param>
        /// <returns></returns>
        private StringBuilder SetDataInactivas(ReportHCITransaccion[] data, string Categoria, string Currency = "", bool generateGranTotal = false)
        {
            var filas = new StringBuilder();
            var tr = "<tr class=\"body\">{0}</tr>";
            var td = "<td {0}>{1}</td>";
            var tdEx = "<table><tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr></table>";
            var NewRow = string.Empty;
            var celda = string.Empty;
            List<string> Comportamiento;
            var TableEx = string.Empty;

            if (!generateGranTotal)
            {
                foreach (var item in data)
                {
                    if (Categoria.ToUpper() == "TARJETA CR")
                    {
                        Comportamiento = strToArrayGrouping(item.Comportamiento, 3);
                        TableEx = string.Format(tdEx, Comportamiento[0], Comportamiento[1], Comportamiento[2], Comportamiento[3]);

                        //Pesos
                        celda = string.Concat(
                                               string.Format(td, "class=\"head_sm\"", string.Format("<em>{0} >> {1} >></em> {2}", Categoria, item.Estatus, item.Suscriptor)),
                                               string.Format(td, "align=\"center\"", item.FechaApertura),
                                               string.Format(td, "align=\"center\"", item.FechaUltimaTrx),
                                               string.Format(td, "align=\"center\"", "RD$"),
                                               string.Format(td, "align=\"center\"", item.LimiteCredito > 0 ? item.LimiteCredito.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                               string.Format(td, "align=\"center\"", TableEx)
                                             );

                        NewRow = string.Format(tr, celda);
                        filas.AppendLine(NewRow);

                        if (!string.IsNullOrEmpty(item.U_LimiteCredito))
                        {
                            decimal LimiteCredito = 0;
                            decimal.TryParse(item.U_LimiteCredito, out LimiteCredito);

                            Comportamiento = strToArrayGrouping(item.Comportamiento, 3);
                            TableEx = string.Format(tdEx, Comportamiento[0], Comportamiento[1], Comportamiento[2], Comportamiento[3]);

                            //Dollar
                            celda = string.Concat(
                                                   string.Format(td, "class=\"head_sm\"", string.Format("<em>{0} >> {1} >></em> {2}", Categoria, item.U_Estatus, item.U_Suscriptor)),
                                                   string.Format(td, "align=\"center\"", item.U_FechaApertura),
                                                   string.Format(td, "align=\"center\"", item.U_FechaUltimaTrx),
                                                   string.Format(td, "align=\"center\"", "RD$"),
                                                   string.Format(td, "align=\"center\"", LimiteCredito > 0 ? LimiteCredito.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                                   string.Format(td, "align=\"center\"", TableEx)
                                                 );

                            NewRow = string.Format(tr, celda);
                            filas.AppendLine(NewRow);
                        }
                    }
                    else
                    {
                        Comportamiento = strToArrayGrouping(item.Comportamiento, 3);
                        TableEx = string.Format(tdEx, Comportamiento[0], Comportamiento[1], Comportamiento[2], Comportamiento[3]);

                        //Pesos
                        celda = string.Concat(
                                               string.Format(td, "class=\"head_sm\"", string.Format("<em>{0} >> {1} >></em> {2}", Categoria, item.Estatus, item.Suscriptor)),
                                               string.Format(td, "align=\"center\"", item.FechaApertura),
                                               string.Format(td, "align=\"center\"", item.FechaUltimaTrx),
                                               string.Format(td, "align=\"center\"", "RD$"),
                                               string.Format(td, "align=\"center\"", item.LimiteCredito > 0 ? item.LimiteCredito.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue),
                                               string.Format(td, "align=\"center\"", TableEx)
                                             );
                        NewRow = string.Format(tr, celda);
                        filas.AppendLine(NewRow);
                    }
                }
            }

            return filas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private StringBuilder SetSentencias(ReportHCISentencia[] data)
        {
            var filas = new StringBuilder();
            var tr = "<tr>{0}</tr>";
            var td = "<td {0}>{1}</td>";
            var NewRow = string.Empty;
            var celda = string.Empty;

            foreach (var item in data)
            {
                celda = string.Concat(
                                      string.Format(td, "align=\"center\"", HttpUtility.HtmlEncode(item.Numero)),
                                      string.Format(td, "align=\"center\"", HttpUtility.HtmlEncode(item.Tipo)),
                                      string.Format(td, "align=\"center\"", HttpUtility.HtmlEncode(item.Demandantes)),
                                      string.Format(td, "align=\"center\"", HttpUtility.HtmlEncode(item.AbogadoDemandante)),
                                      string.Format(td, "align=\"center\"", HttpUtility.HtmlEncode(item.Monto))
                                     );

                NewRow = string.Format(tr, celda);
                filas.AppendLine(NewRow);
            }

            return filas;
        }

        /// <summary>
        /// Generar el grid de el resumen
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Categoria"></param>
        /// <param name="generateGranTotal"></param>
        /// <returns></returns>
        private StringBuilder SetDataResumen(ReportHCIResumenTransaccion[] data, string Categoria, bool generateGranTotal = false)
        {
            var filas = new StringBuilder();
            var tr = "<tr>{0}</tr>";
            var td = "<td {2} align=\"{0}\">{1}</td>";
            var trSubTotal = "<tr class=\"bold flColorGR\">{0}</tr>";
            var trTotalGral = "<tr class=\"bold no_border_td\">{0}</tr>";
            var NewRow = string.Empty;

            if (!generateGranTotal)
            {
                foreach (var item in data)
                {
                    var celda = string.Empty;

                    celda = string.Concat(
                                         string.Format(td, "left", item.Suscriptor, ""),
                                         string.Format(td, "center", item.CantidadCuentas, ""),
                                         string.Format(td, "center", item.Categoria, ""),
                                         string.Format(td, "right", item.LimiteCredito > 0 ? item.LimiteCredito.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                         string.Format(td, "right", item.LimiteCreditoUS > 0 ? item.LimiteCreditoUS.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                         string.Format(td, "right", item.Balance > 0 ? item.Balance.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                         string.Format(td, "right", item.BalanceUS > 0 ? item.BalanceUS.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                         string.Format(td, "right", item.MontoAtraso > 0 ? item.MontoAtraso.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                         string.Format(td, "right", item.MontoAtrasoUS > 0 ? item.MontoAtrasoUS.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                         string.Format(td, "right", (item.PctUtilizacion > 0 ? String.Format("{0:0,0}", item.PctUtilizacion) : "0") + "%", ""),
                                         string.Format(td, "right", (item.PctUtilizacionUS > 0 ? String.Format("{0:0,0}", item.PctUtilizacionUS) : "0") + "%", "")
                                        );

                    NewRow = string.Format(tr, celda);
                    filas.AppendLine(NewRow);
                }

                //SubTotalizar
                var CantidadCuentas = data.Sum(k => k.CantidadCuentas);
                var LimiteCredito = data.Sum(k => k.LimiteCredito);
                var LimiteCreditoUS = data.Sum(k => k.LimiteCreditoUS);
                var Balance = data.Sum(k => k.Balance);
                var BalanceUS = data.Sum(k => k.BalanceUS);
                var MontoAtraso = data.Sum(k => k.MontoAtraso);
                var MontoAtrasoUS = data.Sum(k => k.MontoAtrasoUS);
                var PctUtilizacion = data.Sum(k => k.PctUtilizacion);
                var PctUtilizacionUS = data.Sum(k => k.PctUtilizacionUS);

                var SubTotalCelda = string.Concat(
                                                     string.Format(td, "center", "SUB-TOTAL (" + Categoria.ToUpper() + ")", ""),
                                                     string.Format(td, "center", CantidadCuentas, ""),
                                                     string.Format(td, "center", Categoria.ToUpper(), ""),
                                                     string.Format(td, "right", LimiteCredito > 0 ? LimiteCredito.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                                     string.Format(td, "right", LimiteCreditoUS > 0 ? LimiteCreditoUS.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                                     string.Format(td, "right", Balance > 0 ? Balance.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                                     string.Format(td, "right", BalanceUS > 0 ? BalanceUS.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                                     string.Format(td, "right", MontoAtraso > 0 ? MontoAtraso.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                                     string.Format(td, "right", MontoAtrasoUS > 0 ? MontoAtrasoUS.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                                     string.Format(td, "right", (PctUtilizacion > 0 ? String.Format("{0:0,0}", PctUtilizacion) : "0") + "%", ""),
                                                     string.Format(td, "right", (PctUtilizacionUS > 0 ? String.Format("{0:0,0}", PctUtilizacionUS) : "0") + "%", "")
                                                 );

                NewRow = string.Format(trSubTotal, SubTotalCelda);
                filas.AppendLine(NewRow);
            }
            else
            {
                //Totalizar
                var CantidadCuentas = data.Sum(k => k.CantidadCuentas);
                var LimiteCredito = data.Sum(k => k.LimiteCredito);
                var LimiteCreditoUS = data.Sum(k => k.LimiteCreditoUS);
                var Balance = data.Sum(k => k.Balance);
                var BalanceUS = data.Sum(k => k.BalanceUS);
                var MontoAtraso = data.Sum(k => k.MontoAtraso);
                var MontoAtrasoUS = data.Sum(k => k.MontoAtrasoUS);
                var PctUtilizacion = data.Sum(k => k.PctUtilizacion);
                var PctUtilizacionUS = data.Sum(k => k.PctUtilizacionUS);

                var TotalCelda = string.Concat(
                                                string.Format(td, "", "", ""),
                                                string.Format(td, "", "", ""),
                                                string.Format(td, "", "", ""),
                                                string.Format(td, "right", "TOTAL GENERAL >>", "colspan=\"2\""),
                                                string.Format(td, "right", Balance > 0 ? Balance.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                                string.Format(td, "right", BalanceUS > 0 ? BalanceUS.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                                string.Format(td, "right", MontoAtraso > 0 ? MontoAtraso.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                                string.Format(td, "right", MontoAtrasoUS > 0 ? MontoAtrasoUS.ToString(NumberFormat, CultureInfo.InvariantCulture) : defaultValue, ""),
                                                string.Format(td, "right", "&nbsp;", ""),
                                                string.Format(td, "right", "&nbsp;", "")
                                              );

                NewRow = string.Format(trTotalGral, TotalCelda);
                filas.AppendLine(NewRow);
            }

            return filas;
        }
    }
}