﻿using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using Entity.UnderWriting.Entities;
using Entity.UnderWriting.IllusData;
using RESOURCE.UnderWriting.NewBussiness;
using Statetrust.Framework.Security.Bll;
using Statetrust.Framework.Security.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;
using WEB.NewBusiness.Common.Illustration;
using Statetrust.Framework.Security.Bll.Item;
using WEB.NewBusiness.Common.Illustration.IllustrationVehicle.Models;
using System.Globalization;


namespace WEB.NewBusiness.NewBusiness.UserControls.Illustration
{
    public partial class WUCIllustrationsList : UC, IUC
    {
        private string[] ExcludeColumns { get; set; }
        public WUCIllustrationsList()
        {
            ExcludeColumns = new[] { "CheckSelect", "View", "InspectionLabel", "RequiredLabel", "FinancialClearance", "Notes" };
        }

        public enum ColumnType { GridViewDataTextColumn, GridViewDataDateColumn };

        public string[] TabsGroupIds
        {
            get
            {
                return new string[] { "lnkPreSuscripcion", "lnkSuscripcion", "lnkHistorico" };
            }

        }

        public bool ColumnsTabConfigure
        {
            get
            {
                return ViewState["ColumnsTabConfigure"].ToBoolean();
            }
            set
            {
                ViewState["ColumnsTabConfigure"] = value;
            }
        }

        public class ItemTabs
        {
            public Utility.TabsGroup TabGroup { get; set; }
            public Utility.Tabs Tab { get; set; }
        }

        #region  TabsListConfig
        public List<ItemTabs> TabsListConfig = new List<ItemTabs>
        {
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkPreSuscripcion,
               Tab = Utility.Tabs.lnkIllustrationsToWork
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkPreSuscripcion,
               Tab = Utility.Tabs.lnkCompleteIllustrations
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkPreSuscripcion,
               Tab = Utility.Tabs.lnkDeclinedByClient
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkPreSuscripcion,
               Tab = Utility.Tabs.lnkExpired
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkPreSuscripcion,
               Tab = Utility.Tabs.lnkExpiring
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkSuscripcion,
               Tab = Utility.Tabs.lnkSubscriptions
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkSuscripcion,
               Tab = Utility.Tabs.lnkDiscounts
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkSuscripcion,
               Tab = Utility.Tabs.lnkConfirmationCall
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkSuscripcion,
               Tab = Utility.Tabs.lnkMissingDocuments
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkSuscripcion,
               Tab = Utility.Tabs.lnkMissingInspections
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkSuscripcion,
               Tab = Utility.Tabs.lnkFacultative
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkHistorico,
               Tab = Utility.Tabs.lnkDeclinedBySubscription
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkHistorico,
               Tab = Utility.Tabs.lnkApprovedBySubscription
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkHistorico,
               Tab = Utility.Tabs.lnkHistoricalIllustrations
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkHistorico,
               Tab = Utility.Tabs.lnkPuntoVentaTab
          },
          new ItemTabs {
               TabGroup = Utility.TabsGroup.lnkHistorico,
               Tab = Utility.Tabs.lnkStatistics
          }
        };
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string TabRedirect = string.Empty;
            EnableOrDisabledGroupTab();
            UCEmisiones.ReportType = Utility.StatisticsReportType.StatisticsReportEmission.ToInt();
            if (!IsPostBack)
            {
                //Recoger el tab via QueryString   
                if (!string.IsNullOrEmpty(Request.QueryString["Tab"]))
                {
                    TabRedirect = Request.QueryString["Tab"];
                    ObjServices.InboxTabRedirect = (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), TabRedirect);
                }

                if (string.IsNullOrEmpty(ObjServices.Bandeja))
                {
                    if (ObjServices.UsuarioPropiedades.Any(o => o.Contains("PropiedadCot")))
                    {
                        ObjServices.Bandeja = "Propiedad";
                    }
                    else if (ObjServices.UsuarioPropiedades.Any(o => o.Contains("AutoCot")))
                    {
                        ObjServices.Bandeja = "Auto";
                    }
                    else
                        Response.Redirect(string.Format("~/NewBusiness/Pages/Error.aspx?msg={0}", Resources.WithoutInformationAccess));
                }


                if (!string.IsNullOrEmpty(Request.QueryString["vBandeja"]))
                {
                    if (Request.QueryString["vBandeja"] == "Auto")
                    {
                        ObjServices.Bandeja = Request.QueryString["vBandeja"];
                    }
                    else if (Request.QueryString["vBandeja"] == "Propiedad")
                    {
                        ObjServices.Bandeja = Request.QueryString["vBandeja"];
                    }
                }

                //Si no tiene tab para redireccionar
                if (string.IsNullOrEmpty(TabRedirect))
                {
                    var ctrl = (ObjServices.hdnTabGroup != null) ? ObjServices.hdnTabGroup : hdnTabGroup.Value;
                    var lnk = FindControl(ctrl);
                    if (lnk != null)
                    {
                        ManageGroupTabs(lnk, null);
                        ObjServices.InboxTabRedirect = Utility.Tabs.None;
                    }
                }
                else
                {
                    var DataTab = TabsListConfig.FirstOrDefault(p => p.Tab == ObjServices.InboxTabRedirect);
                    if (DataTab != null)
                    {
                        ObjServices.hdnTabGroup = DataTab.TabGroup.ToString();
                        ObjServices.hdnQuotationTabs = DataTab.Tab.ToString();
                        this.TabSelected = DataTab.Tab;
                        Response.Redirect("~/NewBusiness/Pages/Illustrations.aspx");
                    }
                }

                //Llamar la funcion de los contadores
                this.ExcecuteJScript("setCounterQuotationInbox();");
            }

            ucPopupChangeStatusSaveNotes.ChangeStatusSaveNotes += ChangeStatus;

            #region eliminar AutoPostBack a drpStatusQuotation en Popup de Cambio de Status / asignar funcion OnChange via JS
            DropDownList drpStatusQuotation = ucPopupChangeStatusSaveNotes.FindControl("UpdatePanel2")
                                                                          .FindControl("ppcChangeStatusIllustrations")
                                                                          .FindControl("pccStatus")
                                                                          .FindControl("UpdatePanel100")
                                                                          .FindControl("drpStatusQuotation") as DropDownList;
            if (drpStatusQuotation != null)
            {
                drpStatusQuotation.AutoPostBack = false;
                this.ExcecuteJScript("StatusQuotationOnChange();");
            }
            #endregion


            if (hdnShowpopLocateQuotFlat.Value == "true")
            {
                popLocateQuotFlat.Show();
            }

            if (hdnShowpoppnPrintingInvoice.Value == "true")
            {
                ModalPrintingInvoice.Show();

            }
        }

        public class TabColumnConfig
        {
            public Utility.Tabs tabName { get; set; }
            public List<GridsColumn> Columns { get; set; }
        }

        public class GridsColumn
        {
            public ColumnType columnType { get; set; }
            public string FieldName { get; set; }
            public string CaptionInfoText { get; set; }
            public string Name { get; set; }
            public int VisibleIndex { get; set; }
            public HorizontalAlign CellStyleHorizontalAlign { get; set; }
            public Unit Width { get; set; }
        }

        /// <summary>
        /// Configuracion de las columnas del grid de cotizaciones
        /// </summary>
        public List<TabColumnConfig> TabsColumns = new List<TabColumnConfig>
        {
            #region Cotizaciones para trabajar
            new TabColumnConfig {
                    tabName = Utility.Tabs.lnkIllustrationsToWork,
                    Columns = new List<GridsColumn>
                    {   new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel" ,
                            VisibleIndex = 8,
                            Width = 121
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNo",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 10,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 11,
                            Width = 121
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 12,
                            Width = 121
                        },
                        new GridsColumn {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 13,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 14,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 15,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 19
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 20,
                            Width = 145
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 21
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex =22
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex =23
                        }, 
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 24
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Horas",
                            FieldName = "WorkMinute",
                            Name = "HorasLabel",
                            VisibleIndex= 25
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 26
                        }
                    }
                },
        #endregion 
            #region Cotizaciones Completadas
            new TabColumnConfig {
                    tabName = Utility.Tabs.lnkCompleteIllustrations,
                    Columns = new List<GridsColumn>
                    {
                         new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel" ,
                            VisibleIndex = 8
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNo",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 10,
                            Width = 118
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex =11,
                            Width = 121
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 12,
                            Width = 121
                        },
                         new GridsColumn {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 13,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 14,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 15,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                                  
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 19
                        },new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 20,
                            Width = 145
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 21
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 22
                        },
                        new GridsColumn                        
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex =23
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 24
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 25
                        }
                    }
                },
        #endregion
            #region Declinado por el cliente
                new TabColumnConfig {
                    tabName = Utility.Tabs.lnkDeclinedByClient,
                    Columns = new List<GridsColumn>
                    {
                         new GridsColumn
                         {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel" ,
                            VisibleIndex = 8
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNo",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 10,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 11,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 12
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha en que fue declinada",
                            FieldName = "DeclinedQuoDate",
                            Name = "DeclinedQuoDate",
                            VisibleIndex = 13,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Razón Declinación",
                            FieldName = "DeclinedQuoReason",
                            Name = "DeclinedQuoReason",
                            VisibleIndex = 14,
                            Width = 121
                        },
                        new GridsColumn 
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 15,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 19,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 20,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                        
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 21
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 22,
                            Width = 145
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 23
                        },                      
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 24
                        },                       
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex = 25
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 26
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 27
                        }
                    }
                },
            #endregion         
            #region Expiradas
                new TabColumnConfig {
                    tabName = Utility.Tabs.lnkExpired,
                    Columns = new List<GridsColumn>
                    {
                         new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel" ,
                            VisibleIndex = 8
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNo",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 10,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 11,
                            Width = 121
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 12,
                            Width = 121
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha en que expiró",
                            FieldName = "ExpirationDate",
                            Name = "ExpirationDate",
                            VisibleIndex = 13,
                            Width = 121
                        },
                        new GridsColumn {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 14,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 15,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 19,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                                               
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 20
                        }
                        ,new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 21,
                            Width = 145
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 22
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 23
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex =24
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 25
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 26
                        }
                    }
                },
            #endregion
            #region Por expirar
                new TabColumnConfig {
                    tabName = Utility.Tabs.lnkExpiring,
                    Columns = new List<GridsColumn>
                    {  new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel" ,
                            VisibleIndex = 8
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNo",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 10,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 11,
                            Width = 121
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 12,
                            Width = 121
                        },
                        new GridsColumn {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 13,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 14,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 15,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                        
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 19
                        }
                        ,new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 20,
                            Width = 145
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 21
                        },                        
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 22
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex =23
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 24
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Horas",
                            FieldName = "",
                            Name = "HorasLabel",
                            VisibleIndex= 25
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 26
                        }
                    }
                },
            #endregion
            #region Subscripciones
            new TabColumnConfig {
                    tabName = Utility.Tabs.lnkSubscriptions,
                    Columns = new List<GridsColumn>
                    {    new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel",
                            VisibleIndex = 8
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNo",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 10,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 11,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha en que fue puesta en subscripción",
                            FieldName = "QuoDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 12,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 13,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 14,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 15,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 19,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Siniestralidad Modelo",
                            FieldName = "ModelAccidentRate",
                            Name = "ModelAccidentRate",
                            VisibleIndex = 20,                          
                            Width=145,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },                        
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "% Deduc. DP",
                            FieldName = "PorcDeducDP",
                            Name = "PorcMinDeducDP",
                            VisibleIndex = 21,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. DP",
                            FieldName = "MinDeducDP",
                            Name = "MinDeducDP",
                            VisibleIndex = 22,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. Cristales",
                            FieldName = "MinDeducCristales",
                            Name = "MinDeducCristales",
                            VisibleIndex = 23,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Descuento",
                            FieldName = "TieneDescuento",
                            Name = "HasDiscount",
                            VisibleIndex = 24,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Recargo",
                            FieldName = "TieneRecargo",
                            Name = "HasSurcharge",
                            VisibleIndex = 25,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 26
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 27,
                            Width = 145
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 28
                        },
                        new GridsColumn{
			                columnType = ColumnType.GridViewDataTextColumn,
			                CaptionInfoText = "Siniestralidad Agente",
			                FieldName = "AgentAccidentRate",
			                Name = "AgentAccidentRate",
			                VisibleIndex = 29,
			                Width=145,
			                CellStyleHorizontalAlign = HorizontalAlign.Center
		                },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 30
                        },
                        new GridsColumn{
					        columnType = ColumnType.GridViewDataTextColumn,
					        CaptionInfoText = "Siniestralidad Vendedor",
					        FieldName = "VendorAccidentRate",
					        Name = "VendorAccidentRate",
					        VisibleIndex = 31,
					        Width=145,
					        CellStyleHorizontalAlign = HorizontalAlign.Center
				        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex = 32
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 33
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Suscriptor asignado",
                            FieldName = "AssignedSubscriber",
                            Name = "Subscriber",
                            VisibleIndex = 34
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Horas",
                            FieldName = "SubscriptionMinute",
                            Name = "HorasLabel",
                            VisibleIndex= 35
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 36
                        }
                    }
                },
            #endregion  
            #region Casos Facultativos
            new TabColumnConfig {
                    tabName = Utility.Tabs.lnkFacultative,
                    Columns = new List<GridsColumn>
                    {   new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel",
                            VisibleIndex = 8
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNo",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 10,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 11,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha en que fue puesta en subscripción",
                            FieldName = "QuoPosDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 12,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 13,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 14,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 15,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 19,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "% Deduc. DP",
                            FieldName = "PorcDeducDP",
                            Name = "PorcMinDeducDP",
                            VisibleIndex = 20,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },   
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. DP",
                            FieldName = "MinDeducDP",
                            Name = "MinDeducDP",
                            VisibleIndex = 21,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                        
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. Cristales",
                            FieldName = "MinDeducCristales",
                            Name = "MinDeducCristales",
                            VisibleIndex = 22,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Descuento",
                            FieldName = "TieneDescuento",
                            Name = "HasDiscount",
                            VisibleIndex = 23,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Recargo",
                            FieldName = "TieneRecargo",
                            Name = "HasSurcharge",
                            VisibleIndex = 24,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 25
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 26,
                            Width = 145
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 27
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 28
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex = 29
                        },new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 30
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Suscriptor asignado",
                            FieldName = "AssignedSubscriber",
                            Name = "Subscriber",
                            VisibleIndex = 31
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Horas",
                            FieldName = "SubscriptionMinute",
                            Name = "HorasLabel",
                            VisibleIndex= 32
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 33
                        }
                    }
                },
            #endregion 
            #region Descuentos
            new TabColumnConfig {
                    tabName = Utility.Tabs.lnkDiscounts,
                    Columns = new List<GridsColumn>
                    {   new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Descuento",
                            FieldName = "TieneDescuento",
                            Name = "HasDiscount",
                            VisibleIndex = 7,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de Negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 8
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel",
                            VisibleIndex = 9
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNo",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 10,
                            Width = 118
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 11,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 12,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha en que fue puesta en subscripción",
                            FieldName = "QuoPosDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 13,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 14,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 15,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 19,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 20,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "% Deduc. DP",
                            FieldName = "PorcDeducDP",
                            Name = "PorcMinDeducDP",
                            VisibleIndex = 21,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. DP",
                            FieldName = "MinDeducDP",
                            Name = "MinDeducDP",
                            VisibleIndex = 22,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                        
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. Cristales",
                            FieldName = "MinDeducCristales",
                            Name = "MinDeducCristales",
                            VisibleIndex = 23,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Recargo",
                            FieldName = "TieneRecargo",
                            Name = "HasSurcharge",
                            VisibleIndex = 24,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 25
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 26,
                            Width = 145
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 27
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 28
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex = 29
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 30
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Suscriptor asignado",
                            FieldName = "AssignedSubscriber",
                            Name = "Subscriber",
                            VisibleIndex = 31
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Horas",
                            FieldName = "SubscriptionMinute",
                            Name = "HorasLabel",
                            VisibleIndex = 32
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 33
                        }
                    }
                },
            #endregion 
            #region Llamada de confirmación
            new TabColumnConfig {
                    tabName = Utility.Tabs.lnkConfirmationCall,
                    Columns = new List<GridsColumn>
                    {   new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel",
                            VisibleIndex = 8
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNo",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 10,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 11,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha en que fue puesta en subscripción",
                            FieldName = "QuoPosDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 12,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 13
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 14,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 15,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 19,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "% Deduc. DP",
                            FieldName = "PorcDeducDP",
                            Name = "PorcMinDeducDP",
                            VisibleIndex = 20,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. DP",
                            FieldName = "MinDeducDP",
                            Name = "MinDeducDP",
                            VisibleIndex = 21,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                        
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. Cristales",
                            FieldName = "MinDeducCristales",
                            Name = "MinDeducCristales",
                            VisibleIndex = 22,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Descuento",
                            FieldName = "TieneDescuento",
                            Name = "HasDiscount",
                            VisibleIndex = 23,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Recargo",
                            FieldName = "TieneRecargo",
                            Name = "HasSurcharge",
                            VisibleIndex = 24,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 25
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 26,
                            Width = 145
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 27
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 28
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex = 29
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 30
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Suscriptor asignado",
                            FieldName = "AssignedSubscriber",
                            Name = "Subscriber",
                            VisibleIndex = 31
                        },
                         new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Persona asignada para hacer la confirmación de llamada",
                            FieldName = "ConfirmationCallerName",
                            Name = "ConfirmBy",
                            VisibleIndex = 32
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Horas",
                            FieldName = "SubscriptionMinute",
                            Name = "HorasLabel",
                            VisibleIndex= 33
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 34
                        }
                    }
                },
            #endregion 
            #region Declinado por Subscripcion
            new TabColumnConfig {
                    tabName = Utility.Tabs.lnkDeclinedBySubscription,
                    Columns = new List<GridsColumn>
                    {   new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel" ,
                            VisibleIndex = 8
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNo",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 10,
                            Width = 121
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 11,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex =12,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha Declinación",
                            FieldName = "DeclinedQuoDate",
                            Name = "DeclinedQuoDate",
                            VisibleIndex = 13,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Razón Declinación",
                            FieldName = "DeclinedQuoReason",
                            Name = "DeclinedQuoReason",
                            VisibleIndex = 14
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 15,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 19,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "% Deduc. DP",
                            FieldName = "PorcDeducDP",
                            Name = "PorcMinDeducDP",
                            VisibleIndex = 20,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                       
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. DP",
                            FieldName = "MinDeducDP",
                            Name = "MinDeducDP",
                            VisibleIndex = 21,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                        
                         new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. Cristales",
                            FieldName = "MinDeducCristales",
                            Name = "MinDeducCristales",
                            VisibleIndex = 22,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 23,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Descuento",
                            FieldName = "TieneDescuento",
                            Name = "HasDiscount",
                            VisibleIndex = 24,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Recargo",
                            FieldName = "TieneRecargo",
                            Name = "HasSurcharge",
                            VisibleIndex = 25,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 26
                        }
                        ,new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 27,
                            Width = 145
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 28
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 29
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex = 30
                        }, new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 31
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 32
                        }
                    }
                },
            #endregion 
            #region Documentos faltantes
            new TabColumnConfig {
                    tabName = Utility.Tabs.lnkMissingDocuments,
                    Columns = new List<GridsColumn>
                    {   
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel",
                            VisibleIndex = 8
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNo",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 10,
                            Width = 121
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 11,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 12,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Razón de devolución de la cotización",
                            FieldName = "MissingDocumentQuoReason",
                            Name = "MissingDocumentQuoReason",
                            VisibleIndex = 13
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 14,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 15,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 19,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "% Deduc. DP",
                            FieldName = "PorcDeducDP",
                            Name = "PorcMinDeducDP",
                            VisibleIndex = 20,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. DP",
                            FieldName = "MinDeducDP",
                            Name = "MinDeducDP",
                            VisibleIndex = 21,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                        
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. Cristales",
                            FieldName = "MinDeducCristales",
                            Name = "MinDeducCristales",
                            VisibleIndex = 22,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Descuento",
                            FieldName = "TieneDescuento",
                            Name = "HasDiscount",
                            VisibleIndex = 23,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Recargo",
                            FieldName = "TieneRecargo",
                            Name = "HasSurcharge",
                            VisibleIndex = 24,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 25
                        }
                        ,new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 26,
                            Width = 145
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 27
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 28
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex = 29
                        }, 
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 30
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Minutes",
                            FieldName = "WorkMinute",
                            Name = "HorasLabel",
                            VisibleIndex = 31
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 32
                        }
                    }
                },
        #endregion 
            #region Inspecciones faltantes
            new TabColumnConfig {
                    tabName = Utility.Tabs.lnkMissingInspections,
                    Columns = new List<GridsColumn>
                    {   new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel",
                            VisibleIndex = 8
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNo",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 10,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 11,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 12,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha envio inspección",
                            FieldName = "InspectionQuoDate",
                            Name = "InspectionQuoDateLabel",
                            VisibleIndex = 13,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 14,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 15,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 19,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "% Deduc. DP",
                            FieldName = "PorcDeducDP",
                            Name = "PorcMinDeducDP",
                            VisibleIndex = 20,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. DP",
                            FieldName = "MinDeducDP",
                            Name = "MinDeducDP",
                            VisibleIndex = 21,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                        
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. Cristales",
                            FieldName = "MinDeducCristales",
                            Name = "MinDeducCristales",
                            VisibleIndex = 22,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Descuento",
                            FieldName = "TieneDescuento",
                            Name = "HasDiscount",
                            VisibleIndex = 23,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Recargo",
                            FieldName = "TieneRecargo",
                            Name = "HasSurcharge",
                            VisibleIndex = 24,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Subscriptor asignado",
                            FieldName = "AssignedSubscriber",
                            Name = "Subscriber",
                            VisibleIndex = 25
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Inspector asignado",
                            FieldName = "InspectorAgent",
                            Name = "Inspector",
                            VisibleIndex = 26
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex =27
                        }
                        ,new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 28,
                            Width = 145
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 29
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 30
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex = 31
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 32
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Minutes",
                            FieldName = "InspectionMinute",
                            Name = "HorasLabel",
                            VisibleIndex= 33
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 34
                        }
                    }
                },
            #endregion 
            #region Aprobadas por Subscripcion
            new TabColumnConfig {
                    tabName = Utility.Tabs.lnkApprovedBySubscription,
                    Columns = new List<GridsColumn>
                    {   new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel" ,
                            VisibleIndex = 8
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNoTemp",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 10,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Póliza",
                            FieldName = "IllustrationNo",
                            Name = "PolicyNoLabel",
                            VisibleIndex = 11,
                            Width = 118
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Poliza inclusion/Exclusion/Renovación",
                            FieldName = "PolicyNoMain",
                            Name = "MovementPolicyLabel",
                            VisibleIndex = 12,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 13,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de inspección",
                            FieldName = "InspectionQuoDate",
                            Name = "FechaInspeccionLabel",
                            VisibleIndex = 14,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aprobación",
                            FieldName = "EffectiveDate",
                            Name = "FechaAprobacionLabel",
                            VisibleIndex = 15,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 19,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 20,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 21,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "% Deduc. DP",
                            FieldName = "PorcDeducDP",
                            Name = "PorcMinDeducDP",
                            VisibleIndex = 22,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. DP",
                            FieldName = "MinDeducDP",
                            Name = "MinDeducDP",
                            VisibleIndex = 23,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                        
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. Cristales",
                            FieldName = "MinDeducCristales",
                            Name = "MinDeducCristales",
                            VisibleIndex = 24,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Descuento",
                            FieldName = "DiscountF",
                            Name = "Discount",
                            VisibleIndex = 25,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Recargo",
                            FieldName = "TieneRecargo",
                            Name = "HasSurcharge",
                            VisibleIndex = 26,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Suscriptor asignado",
                            FieldName = "AssignedSubscriber",
                            Name = "Subscriber",
                            VisibleIndex = 27
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Inspector asignado",
                            FieldName = "InspectorAgent",
                            Name = "Inspector",
                            VisibleIndex = 28
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 29
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 30,
                            Width = 145
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 31
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 32
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex = 33
                        },
                        new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 34
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 35
                        }
                    }
                },
            #endregion 
            #region Historico de cotizaciones
            new TabColumnConfig {
                    tabName = Utility.Tabs.lnkHistoricalIllustrations,
                    Columns = new List<GridsColumn>
                    {    new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo Solicitud",
                            FieldName = "RequestTypeDesc",
                            Name = "RequestTypeLabel",
                            VisibleIndex= 5
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Financiada",
                            FieldName = "Financed",
                            Name = "Financed",
                            VisibleIndex= 6
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Linea de negocio",
                            FieldName = "FamilyProduct",
                            Name = "LineofBusinessLabel",
                            VisibleIndex = 7
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tipo de plan",
                            FieldName = "PlanType",
                            Name = "PlanTypeLabel" ,
                            VisibleIndex = 8
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Cotización Temporal",
                            FieldName = "IllustrationNoTemp",
                            Name = "ILLUSTRATIONLABEL",
                            VisibleIndex = 9,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de la cotización",
                            FieldName = "IllustrationDate",
                            Name = "Illustration_Date",
                            VisibleIndex = 10,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Número de Póliza",
                            FieldName = "IllustrationNo",
                            Name = "PolicyNoLabel",
                            VisibleIndex = 11,
                            Width = 118
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Estatus de Cotizacón/Póliza",
                            FieldName = "Status",
                            Name = "LiteralStatus",
                            VisibleIndex = 12,
                            Width = 165
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aceptacion de la cotizacion por el cliente",
                            FieldName = "QuoDate",
                            Name = "FechaAceptacionCliente",
                            VisibleIndex = 13,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de inspección",
                            FieldName = "InspectionQuoDate",
                            Name = "FechaInspeccionLabel",
                            VisibleIndex = 14,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataDateColumn,
                            CaptionInfoText = "Fecha de aprobacion",
                            FieldName = "EffectiveDate",
                            Name = "FechaAprobacionLabel",
                            VisibleIndex = 15,
                            Width = 121
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Monto asegurado",
                            FieldName = "InsuredAmountF",
                            Name = "InsuredAmount",
                            VisibleIndex = 16,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Prima sin impuestos",
                            FieldName = "TotalPremiumF",
                            Name = "PremiumWithoutTax",
                            VisibleIndex = 17,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tasa",
                            FieldName = "Rate",
                            Name = "Rate",
                            VisibleIndex = 18,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Marca",
                            FieldName = "Make",
                            Name = "Make",
                            VisibleIndex = 19,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Modelo",
                            FieldName = "Model",
                            Name = "Model",
                            VisibleIndex = 20,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Año",
                            FieldName = "Year",
                            Name = "Year",
                            VisibleIndex = 21,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "% Deduc. DP",
                            FieldName = "PorcDeducDP",
                            Name = "PorcMinDeducDP",
                            VisibleIndex = 22,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. DP",
                            FieldName = "MinDeducDP",
                            Name = "MinDeducDP",
                            VisibleIndex = 23,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },                        
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Min Deduc. Cristales",
                            FieldName = "MinDeducCristales",
                            Name = "MinDeducCristales",
                            VisibleIndex = 24,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Descuento",
                            FieldName = "DiscountF",
                            Name = "Discount",
                            VisibleIndex = 25,
                            CellStyleHorizontalAlign = HorizontalAlign.Right
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Tiene Recargo",
                            FieldName = "TieneRecargo",
                            Name = "HasSurcharge",
                            VisibleIndex = 26,
                            CellStyleHorizontalAlign = HorizontalAlign.Center
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Oficina",
                            FieldName = "Office",
                            Name = "Office",
                            VisibleIndex = 27
                        }
                        ,new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Canal",
                            FieldName = "Channel",
                            Name = "Channel",
                            VisibleIndex = 28,
                            Width = 145
                        },
                            new GridsColumn{
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Agente Comercial",
                            FieldName = "SupervisorAgentName",
                            Name = "SupervisorAgentNameLabel",
                            VisibleIndex = 29
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Vendedor",
                            FieldName = "AgentName",
                            Name = "Vendor",
                            VisibleIndex = 30
                        },
                        new GridsColumn
                        {
                            columnType = ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Asegurado",
                            FieldName = "InsuredName",
                            Name = "INSURED",
                            VisibleIndex = 31
                        }, new GridsColumn
                        {
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Identificacion del asegurado",
                            FieldName = "Identification",
                            Name = "ID",
                            VisibleIndex = 32
                        },
                        new GridsColumn{
                            columnType =ColumnType.GridViewDataTextColumn,
                            CaptionInfoText = "Usuario",
                            FieldName = "QuoCreateUserName",
                            Name = "USER",
                            VisibleIndex= 33
                        }
                    }
                },
        #endregion
        };

        //private void CreateFixedColumns(ASPxGridView GridView)
        //{
        //    var myCheckBoxSelect = new Utility.MyColumn
        //    {
        //        Caption = "",
        //        FieldName = "",
        //        Name = "CheckSelect"
        //    };

        //    var CheckAttr = new Dictionary<string, string>();
        //    CheckAttr.Add("Width", "50px");

        //    var chkSelect = Utility.CreateTemplate(myCheckBoxSelect,
        //                                           CheckAttr,
        //                                           Utility.WebControlType.CheckBox,
        //                                           Utility.TemplateType.GridViewDataCheckColumn,
        //                                           "chkSelect",
        //                                           ""
        //                                           );


        //    chkSelect.VisibleIndex = 0;
        //    GridView.Columns.Add(chkSelect);

        //    var myViewButton = new Utility.MyColumn
        //    {
        //        Caption = "",
        //        FieldName = "",
        //        Name = "View"
        //    };

        //    var ButtonAttr = new Dictionary<string, string>();
        //    ButtonAttr.Add("Width", "50px");
        //    ButtonAttr.Add("title", "Ver detalle de la cotización");

        //    var ViewButton = Utility.CreateTemplate(myViewButton,
        //                                            ButtonAttr,
        //                                            Utility.WebControlType.Button,
        //                                            Utility.TemplateType.GridViewDataColumn,
        //                                            "btnVerCotPol",
        //                                            "view_file",
        //                                            CommandName: "VerCotPol"
        //                                            );

        //    var ColumnViewSettings = (ViewButton as GridViewDataColumn);
        //    ColumnViewSettings.Settings.AllowSort = DevExpress.Utils.DefaultBoolean.False;
        //    ColumnViewSettings.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False;
        //    ColumnViewSettings.VisibleIndex = 1;
        //    GridView.Columns.Add(ColumnViewSettings);


        //    var myInspectionViewButton = new Utility.MyColumn
        //    {
        //        Caption = "",
        //        FieldName = "",
        //        Name = "InspectionLabel"
        //    };

        //    var InspectionViewAttr = new Dictionary<string, string>();
        //    InspectionViewAttr.Add("Width", "50px");
        //    InspectionViewAttr.Add("title", "Ir a la inspección");

        //    var InspectionViewButton = Utility.CreateTemplate(myInspectionViewButton,
        //                                            InspectionViewAttr,
        //                                            Utility.WebControlType.Button,
        //                                            Utility.TemplateType.GridViewDataColumn,
        //                                            "btnInspection",
        //                                            "inspection_doc",
        //                                            CommandName: "Inspection"
        //                                            );

        //    var InspectionViewButtonSettings = (InspectionViewButton as GridViewDataColumn);
        //    InspectionViewButtonSettings.Settings.AllowSort = DevExpress.Utils.DefaultBoolean.False;
        //    InspectionViewButtonSettings.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False;
        //    InspectionViewButtonSettings.VisibleIndex = 2;
        //    GridView.Columns.Add(InspectionViewButtonSettings);
        //}

        private void SettingColumns(ASPxGridView GridView)
        {
            #region Configuracion de columnas
            GridViewColumn Column = null;
            var dataConfigTab = TabsColumns.Where(p => p.tabName == TabSelected);

            if (dataConfigTab.Any())
            {
                var dataColumns = dataConfigTab.FirstOrDefault().Columns.OrderBy(o => o.VisibleIndex);
                foreach (var item in dataColumns)
                {
                    switch (item.columnType)
                    {
                        //En caso de que la columna sea de tipo GridViewDataTextColumn
                        case ColumnType.GridViewDataTextColumn:
                            Column = new GridViewDataTextColumn
                            {
                                FieldName = item.FieldName,
                                Name = item.Name,
                                Width = item.Width
                            };

                            var DataTextColumn = (Column as GridViewDataTextColumn);
                            DataTextColumn.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                            DataTextColumn.CellStyle.HorizontalAlign = item.CellStyleHorizontalAlign;
                            break;
                        //En caso de que la columna sea de tipo GridViewDataDateColumn 
                        case ColumnType.GridViewDataDateColumn:
                            Column = new GridViewDataDateColumn
                            {
                                FieldName = item.FieldName,
                                Name = item.Name,
                                Width = item.Width
                            };

                            var DateColumn = (Column as GridViewDataDateColumn);
                            DateColumn.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                            DateColumn.PropertiesDateEdit.DisplayFormatString = "g";
                            break;
                    }

                    //Verificar si la columna ya esta agregada al grid
                    var ColumnExistInGrid = ThisColumnExist(Column.Name);

                    if (!ColumnExistInGrid)
                        GridView.Columns.Add(Column);
                }

                ReorderColumnsInGrid(GridView, dataColumns);

                foreach (var t in ExcludeColumns)
                    if (t != "CheckSelect")
                        GridView.HideOrShowColumnGrid(TabSelected != Utility.Tabs.lnkPuntoVentaTab, t);
            }
            #endregion
        }

        private void ReorderColumnsInGrid(ASPxGridView GridView, IEnumerable<GridsColumn> dataColumns)
        {
            //Verificar configuracion del grid y las columnas
            foreach (var c in GridView.Columns)
            {
                var co = c as GridViewColumn;
                //Si la columna no es una de las que se excluyeron entonces hacer verificacion
                if (!ExcludeColumns.Contains(co.Name))
                {
                    //Verifico si la columna existe en el tab seleccionado
                    var ExistInTab = dataColumns.Any(l => l.Name == co.Name);
                    //Obtengo la columna
                    var DColumn = dataColumns.FirstOrDefault(l => l.Name == co.Name);
                    //Verificacion de visibilidad de la columna
                    if (!ExistInTab)
                        co.Visible = false;
                    else
                    {
                        co.Visible = true;
                        co.VisibleIndex = DColumn.VisibleIndex;

                        if (DColumn.columnType == ColumnType.GridViewDataTextColumn)
                        {
                            var col = (co as GridViewDataTextColumn);
                            col.FieldName = DColumn.FieldName;
                        }
                    }
                }
            }
        }

        protected void ManageTabs(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            TabSelected = Utility.ParseEnum<Utility.Tabs>(lnk.ID);

            if (ObjServices.hdnQuotationTabs != TabSelected.ToString())
                ColumnsTabConfigure = false;

            ObjServices.hdnQuotationTabs = lnk.ID;
            hdnTabSelected.Value = lnk.ID;
            hideButtonFrontByRoles(lnk.ID);
            gvIllustration.FilterExpression = string.Empty;

            pnlIllustrationGridView.Visible = true;
            pnlIllustrationsFilters.Visible = true;
            pnlStatisticsGridView.Visible = false;
            pnlStatisticsFilters.Visible = false;

            FillData();

            pnlExportExcel.Visible = true;

            mvBandejaPOS.SetActiveView(TabSelected != Utility.Tabs.lnkPuntoVentaTab ? vwBandeja : vwPOS);

            if (TabSelected == Utility.Tabs.lnkStatistics)
            {
                pnlIllustrationGridView.Visible = false;
                pnlIllustrationsFilters.Visible = false;
                pnlStatisticsGridView.Visible = true;
                pnlStatisticsFilters.Visible = true;
                pnlExportExcel.Visible = false;
                lnkStatistics_Click();
            }


        }
        private bool IsGerencialRole
        {
            get
            {
                return ((WEB.NewBusiness.NewBusiness.Pages.Illustrations)Page).Usuario.Propiedades.Any(o => o.Contains("SubscriptionManagerAutoAdmin"));
            }
        }

        private bool ISFiltering
        {
            get { return ViewState["ISFiltering"].ToBoolean(); }
            set { ViewState["ISFiltering"] = value; }
        }

        private bool IsHeaderFilter
        {
            get { return ViewState["IsHeaderFilter"].ToBoolean(); }
            set { ViewState["IsHeaderFilter"] = value; }
        }

        private Utility.Tabs TabSelected
        {
            get
            {
                if (Session["TabSelected"] == null)
                    Session["TabSelected"] = Utility.Tabs.lnkIllustrationsToWork;

                Utility.Tabs result = (Utility.Tabs)Utility.getEnumTypeFromValue(typeof(Utility.Tabs), Session["TabSelected"].ToString());

                return result;
            }
            set
            {
                Session["TabSelected"] = value;
            }
        }

        private int ExpirationDays
        {
            get
            {
                var illustrationExpirationDays = ConfigurationManager.AppSettings["IllustrationExpirationDays"];
                return illustrationExpirationDays == null ? 60 : illustrationExpirationDays.ToInt();
            }
        }

        private int AvailableDays
        {
            get
            {
                var illustrationAvailableDays = ConfigurationManager.AppSettings["AvailableDays"];
                return illustrationAvailableDays == null ? 5 : illustrationAvailableDays.ToInt();
            }
        }

        private string AgentNameId
        {
            get
            {
                return ((WEB.NewBusiness.NewBusiness.Pages.Illustrations)Page).Usuario.AgentNameId;
            }
        }

        private List<Policy.VehicleInsured> getVehiclesOfQuotation(int vCorpId, int vRegionId, int vCountryId, int vDomesticregId, int vStateProvId, int vCityId, int vOfficeId, int vCaseSeqNo, int vHistSeqNo)
        {
            var param = new Policy.Parameter
            {
                CorpId = vCorpId,
                RegionId = vRegionId,
                CountryId = vCountryId,
                DomesticregId = vDomesticregId,
                StateProvId = vStateProvId,
                CityId = vCityId,
                OfficeId = vOfficeId,
                CaseSeqNo = vCaseSeqNo,
                HistSeqNo = vHistSeqNo
            };

            var result = ObjServices.oPolicyManager.GetVehicleInsured(param).ToList();
            return result.Count > 0 ? result : new List<Policy.VehicleInsured>() { };
        }

        private List<Property> getPropertiesOfQuotation(int vCorpId, int vRegionId, int vCountryId, int vDomesticregId, int vStateProvId, int vCityId, int vOfficeId, int vCaseSeqNo, int vHistSeqNo)
        {
            var param = new Property.Key
            {
                CorpId = vCorpId,
                RegionId = vRegionId,
                CountryId = vCountryId,
                DomesticregId = vDomesticregId,
                StateProvId = vStateProvId,
                CityId = vCityId,
                OfficeId = vOfficeId,
                CaseSeqNo = vCaseSeqNo,
                HistSeqNo = vHistSeqNo
            };

            var result = ObjServices.oPropertyManager.GetProperty(param).ToList();
            return result.Count > 0 ? result : new List<Property>() { };
        }

        private List<Transport.Insured> getTransportsOfQuotation(int vCorpId, int vRegionId, int vCountryId, int vDomesticregId, int vStateProvId, int vCityId, int vOfficeId, int vCaseSeqNo, int vHistSeqNo)
        {
            var param = new Transport.Insured.Key
            {
                CorpId = vCorpId,
                RegionId = vRegionId,
                CountryId = vCountryId,
                DomesticregId = vDomesticregId,
                StateProvId = vStateProvId,
                CityId = vCityId,
                OfficeId = vOfficeId,
                CaseSeqNo = vCaseSeqNo,
                HistSeqNo = vHistSeqNo
            };

            var result = ObjServices.oTransportManager.GetTransportInsured(param).ToList();
            return result.Count > 0 ? result : new List<Transport.Insured>() { };
        }

        private List<Navy.Insured> getNaviesOfQuotation(int vCorpId, int vRegionId, int vCountryId, int vDomesticregId, int vStateProvId, int vCityId, int vOfficeId, int vCaseSeqNo, int vHistSeqNo)
        {
            var param = new Navy.Insured.Key
            {
                CorpId = vCorpId,
                RegionId = vRegionId,
                CountryId = vCountryId,
                DomesticRegId = vDomesticregId,
                StateProvId = vStateProvId,
                CityId = vCityId,
                OfficeId = vOfficeId,
                CaseSeqNo = vCaseSeqNo,
                HistSeqNo = vHistSeqNo
            };

            var result = ObjServices.oNavyManager.GetNavyInsured(param).ToList();
            return result.Count > 0 ? result : new List<Navy.Insured>() { };
        }

        private List<Bail.Insured> getBailsOfQuotation(int vCorpId, int vRegionId, int vCountryId, int vDomesticregId, int vStateProvId, int vCityId, int vOfficeId, int vCaseSeqNo, int vHistSeqNo)
        {
            var param = new Bail.Insured.Key
            {
                CorpId = vCorpId,
                RegionId = vRegionId,
                CountryId = vCountryId,
                DomesticRegId = vDomesticregId,
                StateProvId = vStateProvId,
                CityId = vCityId,
                OfficeId = vOfficeId,
                CaseSeqNo = vCaseSeqNo,
                HistSeqNo = vHistSeqNo
            };

            var result = ObjServices.oBailManager.GetBailInsured(param).ToList();
            return result.Count > 0 ? result : new List<Bail.Insured>() { };
        }

        private List<Airplane.Insured> getAirPlanesOfQuotation(int vCorpId, int vRegionId, int vCountryId, int vDomesticregId, int vStateProvId, int vCityId, int vOfficeId, int vCaseSeqNo, int vHistSeqNo)
        {
            var param = new Airplane.Insured.Key
            {
                CorpId = vCorpId,
                RegionId = vRegionId,
                CountryId = vCountryId,
                DomesticRegId = vDomesticregId,
                StateProvId = vStateProvId,
                CityId = vCityId,
                OfficeId = vOfficeId,
                CaseSeqNo = vCaseSeqNo,
                HistSeqNo = vHistSeqNo
            };

            var result = ObjServices.oAirPlaneManager.GetAirplaneInsured(param).ToList();
            return result.Count > 0 ? result : new List<Airplane.Insured>() { };
        }

        public void ClearData() { }
        public void ReadOnlyControls(bool isReadOnly) { }
        public void save() { }
        public void edit() { }

        private void SetCounters()
        {
            var FormatoNumerico = "{0:#,0}";

            //Grupos de Tabs
            PreSuscripcion.InnerText = string.Format(FormatoNumerico, hdnPreSuscripcionCount.Value);
            Suscripcion.InnerText = string.Format(FormatoNumerico, hdnSuscripcionCount.Value);
            Historico.InnerText = string.Format(FormatoNumerico, hdnHistoricoCount.Value);

            //Tabs
            IllustrationsToWorkCount.InnerText = string.Format(FormatoNumerico, hdnIllustrationsToWorkCount.Value);
            CompleteIllustrationsCount.InnerText = string.Format(FormatoNumerico, hdnCompleteIllustrationsCount.Value);
            DeclinedByClientCount.InnerText = string.Format(FormatoNumerico, hdnDeclinedByClientCount.Value);
            ExpiredCount.InnerText = string.Format(FormatoNumerico, hdnExpiredCount.Value);
            ExpiringCount.InnerText = string.Format(FormatoNumerico, hdnExpiringCount.Value);
            SubscriptionsCount.InnerText = string.Format(FormatoNumerico, hdnSubscriptionsCount.Value);
            DiscountsCount.InnerText = string.Format(FormatoNumerico, hdnDiscountsCount.Value);
            ConfirmationCallCount.InnerText = string.Format(FormatoNumerico, hdnConfirmationCallCount.Value);
            MissingDocumentsCount.InnerText = string.Format(FormatoNumerico, hdnMissingDocumentsCount.Value);
            MissingInspectionsCount.InnerText = string.Format(FormatoNumerico, hdnMissingInspectionsCount.Value);
            FacultativeCount.InnerText = string.Format(FormatoNumerico, hdnFacultativeCount.Value);
            DeclinedBySubscriptionCount.InnerText = string.Format(FormatoNumerico, hdnDeclinedBySubscriptionCount.Value);
            ApprovedBySubscriptionCount.InnerText = string.Format(FormatoNumerico, hdnApprovedBySubscriptionCount.Value);
            HistoricalIllustrationsCount.InnerText = string.Format(FormatoNumerico, hdnHistoricalIllustrationsCount.Value);
            PuntoVentaTabCount.InnerText = string.Format(FormatoNumerico, hdnPuntoVentaTabCount.Value);
            IncompleteIllustrationCount.InnerText = string.Format(FormatoNumerico, hdnIncompleteIllustrationCount.Value);
            ApprovedByClientCount.InnerText = string.Format(FormatoNumerico, hdnApprovedByClientCount.Value);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Translator(string.Empty);
            pnFlatTableRefresh.Visible = ObjServices.isUserCot;
            SetCounters();
            hdnCounterFreQ.Value = ObjServices.CounterFreQRefresh.ToString();
        }

        public void Translator(string Lang)
        {
            btnPrintInvoice.Text = Resources.PrintInvoice.ToUpper();
            btnSearch.Text = Resources.Search;
            btnStatisticsSearch.Text = Resources.Search;
            btnAssignIllustrations.Text =
            btnOpenAssignIllustrations.Text = Resources.AssignIllustrations.ToUpper();
            lblAssignIllustrationMessage.Text = Resources.Sure_the_following_illustrations_are_be_assigned;
            btnPrintList.Text = Resources.Export.ToUpper();
            btnSubscription.Text = Resources.SendToSubscription.ToUpper();
            btnDeclinedByClient.Text = Resources.btnChangeStatusCotizacion.ToUpper();
            btnDeclinedByClient.Attributes.Add("data-StatusMessage", Resources.Are_you_sure_you_want_to_change_the_status_to_the_following_quote);
            btnDeclinedByClient.Attributes.Add("data-camefromstartpage", TabSelected.ToString());
            btnSubscription.Attributes.Add("data-Status", Utility.IllustrationStatus.Submitted.ToString());
            btnSubscription.Attributes.Add("data-StatusMessage", Resources.Sure_the_following_illustrations_has_been_accepted_by_client);
            btnApproveBySubscription.Text = Resources.ApproveBySubscription.ToUpper();
            btnFlatTableRefresh.Text = Resources.FlatTableRefresh.ToUpper();
            btnRefreshCounters.Text = Resources.RefreshCounters;

            //btnLocaleQuotFlat.Text = Resources.LocateQuotationLabel.ToUpper();


            var tb = Utility.tabsQoutationsPopUp.QoutationsToWork.ToString();

            if (!string.IsNullOrEmpty(ObjServices.hdnQuotationTabs))
            {
                var v = ObjServices.hdnQuotationTabs;

                switch (v)
                {
                    case "lnkIllustrationsToWork": tb = Utility.tabsQoutationsPopUp.QoutationsToWork.ToString(); break;
                    case "lnkCompleteIllustrations": tb = Utility.tabsQoutationsPopUp.QoutationsCompleted.ToString(); break;
                    case "lnkSubscriptions": tb = Utility.tabsQoutationsPopUp.QoutationsSubscription.ToString(); break;
                    case "lnkMissingDocuments": tb = Utility.tabsQoutationsPopUp.QoutationsMissingDocuments.ToString(); break;
                    case "lnkMissingInspections": tb = Utility.tabsQoutationsPopUp.QoutationsMissingInspections.ToString(); break;
                    case "lnkExpiring": tb = Utility.tabsQoutationsPopUp.QoutationsExpiring.ToString(); break;
                    case "lnkConfirmationCall": tb = Utility.tabsQoutationsPopUp.QoutationsConfirmationCall.ToString(); break;
                    case "lnkDiscounts": tb = Utility.tabsQoutationsPopUp.QoutationsDiscount.ToString(); break;
                }
            }

            btnDeclinedByClient.Attributes.Add("data-Tab", tb);

            ucPopupChangeStatusSaveNotes.StatusQuotation(tb);

            changeVisibilityButtonsByRole();

            ProccessSelectTab();

            if (ObjServices.isChangingLang)
            {
                gvIllustration.DataBind();
                FillDropDown();
                FillData();
            }

            lnkIllustrationsToWork.Text =
                RESOURCE.UnderWriting.NewBussiness.Resources.IllustrationsToWork;

            lblAgent.Text =
                ObjServices.IsSuscripcionQuotRole &&
                ObjServices.UserType == Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.Assistant
                ? Resources.Subscriptors : Resources.Agents;

            if (ObjServices.isChangingLang)
            {
                ObjServices.isChangingLang = false;
                FillStatisticsDrops();
            }
        }

        private void changeVisibilityButtonsByRole()
        {
            var ButtonVisible = false;

            if (ObjServices.IsAgentQuotRole || ObjServices.IsAngetInspectorQuotRole || ObjServices.isUserCot)
                ButtonVisible = (
                                  ObjServices.hdnQuotationTabs == Utility.Tabs.lnkIllustrationsToWork.ToString() ||
                                  ObjServices.hdnQuotationTabs == Utility.Tabs.lnkCompleteIllustrations.ToString() ||
                                  ObjServices.hdnQuotationTabs == Utility.Tabs.lnkExpiring.ToString() ||
                                  ObjServices.hdnQuotationTabs == Utility.Tabs.lnkMissingDocuments.ToString() ||
                                  ObjServices.hdnQuotationTabs == Utility.Tabs.lnkMissingInspections.ToString()
                                );

            if ((ObjServices.IsSuscripcionManagerQuotRole || ObjServices.IsSucripcionDirectorQuotRole || ObjServices.isUserCot) &&
                ObjServices.hdnQuotationTabs == Utility.Tabs.lnkSubscriptions.ToString())
                ButtonVisible = true;

            if ((ObjServices.IsSuscripcionManagerQuotRole || ObjServices.IsSucripcionDirectorQuotRole || ObjServices.IsSuscripcionQuotRole || ObjServices.isUserCot) &&
                ObjServices.hdnQuotationTabs == Utility.Tabs.lnkMissingInspections.ToString())
                ButtonVisible = true;


            if (ObjServices.IsConfirmationCallManagerCot && ObjServices.hdnQuotationTabs == Utility.Tabs.lnkConfirmationCall.ToString())
                ButtonVisible = true;


            pnlAssignIllustrations.Visible = ButtonVisible;
            pnlSubscription.Visible = false;
        }

        private void ProccessSelectTab()
        {
            RemoveClassAllLi(Controls);
            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)FindControl("li" + TabSelected.ToString());
            li.Attributes.Add("class", "active");
        }

        private void RemoveClassAllLi(ControlCollection controls)
        {
            foreach (var control in controls)
            {
                var castControl = control as Control;
                if (castControl is HtmlGenericControl && castControl.ID.Contains("lilnk"))
                    ((HtmlGenericControl)castControl).Attributes.Remove("class");
                else if (castControl.Controls.Count > 0)
                    RemoveClassAllLi(castControl.Controls);
            }
        }

        public void FillDropDown()
        {
            ObjServices.GettingAllDrops(ref drpCompanyProfile,
                                        Utility.DropDownType.Company,
                                        "CompanyDesc",
                                        "CompanyId",
                                        GenerateItemSelect: true,
                                        companyId: -1,
                                        GenerateItemSelectText: Resources.All);


            ObjServices.GettingAllDropsJSON(ref drpOffice,
                                            Utility.DropDownType.OfficeCot,
                                            "OfficeDesc",
                                            agentId: ObjServices.Agent_Id,
                                            GenerateItemSelect: true,
                                            GenerateItemSelectText: Resources.All);


            ObjServices.GettingAllDrops(ref drpPeriod,
                                        Utility.DropDownType.TimeDimension,
                                        "ElementDesc",
                                        "ElementId",
                                        GenerateItemSelect: true,
                                        companyId: -1,
                                        GenerateItemSelectText: Resources.All);

            //Cargar por defecto los ultimos 30 dias
            drpPeriod.SelectIndexByValue("3");

            var dataBl = ObjServices.GettingDropData(Utility.DropDownType.BusinessLineQuo).Select(k => new { Value = k.BlId, Text = k.BlDesc });

            drpBusinessLine.DataSource = dataBl;
            drpBusinessLine.DataValueField = "Value";
            drpBusinessLine.DataTextField = "Text";
            drpBusinessLine.DataBind();
            drpBusinessLine.Items.Insert(0, new ListItem { Text = Resources.All, Value = "-1" });

            Utility.itemOfficceWithoutAgent Office = new Utility.itemOfficceWithoutAgent();

            if (ObjServices.UserType != Usuarios.UserTypeEnum.User)
            {
                Office = ObjServices.GetCurrentOfficeWithoutAgent();
                var jsonOffice = Utility.serializeToJSON(Office);

                if (!ObjServices.IsSucripcionDirectorQuotRole)
                {
                    var li = new List<ListItem>();
                    foreach (ListItem item in drpOffice.Items)
                    {
                        var officeddl = Utility.deserializeJSON<Utility.itemOfficceWithoutAgent>(item.Value);
                        if (officeddl != null)
                            if (officeddl.OfficeId != Office.OfficeId)
                                li.Add(item);
                    }

                    foreach (var item in li)
                        drpOffice.Items.Remove(item);
                }

                drpOffice.SelectedIndex = 0;
                drpOffice_SelectedIndexChanged(drpOffice, null);
            }

            ucPopupChangeStatusSaveNotes.FillReasonDenied("auto", Utility.ReasonPredefinieds.DeniedIllustrationReason);

            //#region Llenar dropdown drpAssignIllustrationsSubscribers en popup para Asignar Cotizaciones

            //drpAssignIllustrationsSubscribers.Visible = ObjServices.IsSuscripcionManagerQuotRole ||
            //                                                         ObjServices.IsAgentQuotRole ||
            //                                                ObjServices.IsAngetInspectorQuotRole ||
            //                                            ObjServices.IsSucripcionDirectorQuotRole;

            //var isAgent = (ObjServices.IsAgentQuotRole || ObjServices.IsAngetInspectorQuotRole);

            //divdrpAsignIllustrations.Attributes.Remove("Style");
            //divdrpAsignIllustrations.Attributes.Add("Style", (isAgent) ? "display:none" : "display:block");

            //if (drpAssignIllustrationsSubscribers.Visible)
            //{
            //    var data = new List<Entity.UnderWriting.Entities.DropDown>(0);

            //    if (!isAgent)
            //    {
            //        txtAgentsOrSubscriptor.Visible = false;
            //        data = ObjServices.GettingDropData(Utility.DropDownType.AgentQuotation,
            //                                           corpId: ObjServices.Corp_Id,
            //                                           regionId: ObjServices.Region_Id,
            //                                           countryId: ObjServices.Country_Id,
            //                                           domesticregId: ObjServices.Domesticreg_Id,
            //                                           stateProvId: ObjServices.State_Prov_Id,
            //                                           cityId: ObjServices.City_Id,
            //                                           officeId: ObjServices.Office_Id,
            //                                           agentId: null).ToList();
            //        if (data != null)
            //            data = data.Where(d => d.RoleDesc == "SuscripcionCot").ToList();
            //    }
            //    else
            //    {
            //        txtAgentsOrSubscriptor.Visible = true;
            //        //Es un agente
            //        data = ObjServices.GettingDropData(Utility.DropDownType.AgentCot,
            //                                          corpId: ObjServices.Corp_Id,
            //                                          regionId: ObjServices.Region_Id,
            //                                          countryId: ObjServices.Country_Id,
            //                                          domesticregId: ObjServices.Domesticreg_Id,
            //                                          stateProvId: ObjServices.State_Prov_Id,
            //                                          cityId: ObjServices.City_Id,
            //                                          officeId: ObjServices.Office_Id,
            //                                          agentId: ObjServices.Agent_LoginId
            //                                          ).ToList();
            //    }

            //    drpAssignIllustrationsSubscribers.DataSource = data;
            //    drpAssignIllustrationsSubscribers.DataTextField = isAgent ? "RejectReasonDesc" : "AgentName";
            //    drpAssignIllustrationsSubscribers.DataValueField = "AgentId";
            //    drpAssignIllustrationsSubscribers.DataBind();

            //    drpAssignIllustrationsSubscribers.Items.Insert(0, new ListItem() { Value = "-1", Text = Resources.Select });
            //    drpAssignIllustrationsSubscribers.SelectedIndex = 0;
            //}
            //#endregion
        }

        private void EnableOrDisabledGroupTab()
        {
            lnkPreSuscripcion.Enabled = hdnPreSuscripcion.Value == "true";
            lnkSuscripcion.Enabled = hdnSuscripcion.Value == "true";
            lnkHistorico.Enabled = hdnHistorico.Value == "true";
        }

        private bool setFirstMenu(bool SelectFirst, Utility.Tabs TabSelected = Utility.Tabs.None)
        {
            ColumnsTabConfigure = false;
            bool result = true;

            Control lnkGroup = null,
                    lnk = null;

            if (!SelectFirst)
            {
                var idLnkGroup = ObjServices.hdnTabGroup;
                var group = this.FindControl(idLnkGroup);
                lnkGroup = group as LinkButton;

                var IdLnk = ObjServices.hdnQuotationTabs;
                var lnkObject = this.FindControl(TabSelected == Utility.Tabs.None ? IdLnk : TabSelected.ToString());
                lnk = lnkObject as LinkButton;

                if (lnk.ID == "lnkStatistics")
                {
                    pnlIllustrationGridView.Visible = false;
                    pnlIllustrationsFilters.Visible = false;
                    pnlStatisticsGridView.Visible = true;
                    pnlStatisticsFilters.Visible = true;
                    pnlExportExcel.Visible = false;
                    lnkStatistics_Click();
                }
            }
            else
            {
                System.Web.UI.WebControls.View SelectedView = mvGroupTabs.Views[mvGroupTabs.ActiveViewIndex];
                IEnumerable<Control> controls = SelectedView.Controls.Cast<Control>();
                Control ctrl = controls.FirstOrDefault(c => c is HtmlGenericControl && c.ID.Contains("lilnk") && ((HtmlGenericControl)c).Visible);

                if (ctrl == null)
                    result = false;
                else
                    lnk = FindControl(TabSelected == Utility.Tabs.None ? ctrl.ID.Replace("lilnk", "lnk") : TabSelected.ToString());
            }

            if (lnkGroup != null)
            {
                ObjServices.hdnTabGroup = hdnTabGroup.Value = lnkGroup.ID;
                SetActiveView(ObjServices.hdnTabGroup);
            }

            if (lnk == null)
                result = false;

            if (result)
            {
                TabSelected = Utility.ParseEnum<Utility.Tabs>(lnk.ID);
                ObjServices.hdnQuotationTabs = lnk.ID;
                hdnTabSelected.Value = lnk.ID;
                hideButtonFrontByRoles(lnk.ID);

                ManageTabs(lnk, null);

                if (TabSelected != Utility.Tabs.lnkPuntoVentaTab)
                    mvBandejaPOS.SetActiveView(vwBandeja);
            }

            return
                result;
        }

        private List<IllustrationInformation> SetPolicyStatusExpired(int? UserId, int? CompanyId, DateTime? DateFrom, DateTime? DateTo, int? CorpId, int? RegionId,
                                                                     int? CountryId, int? DomesticRegId, int? StateProvId, int? CityId, int? OfficeId, int? LanguageId,
                                                                     bool? GetHistorical, string IllustrationStatusCode, string TabFilter, string AgentNameId,
                                                                     string AgentType, string AssignedSubscriberNameId, List<IllustrationInformation> lst)
        {
            var AllTimeExpired = lst.Where(o => o.isExpired == true).ToList();
            if (AllTimeExpired.Any())
            {
                foreach (var item in AllTimeExpired)
                {
                    //Cambiando el Status a Expiradas
                    ObjServices.oPolicyManager.SetPolicyStatus(new Policy.Parameter
                    {
                        CorpId = item.CorpId,
                        RegionId = item.RegionId,
                        CountryId = item.CountryId,
                        DomesticregId = item.DomesticregId,
                        StateProvId = item.StateProvId,
                        CityId = item.CityId,
                        OfficeId = item.OfficeId,
                        CaseSeqNo = item.CaseSeqNo,
                        HistSeqNo = item.HistSeqNo,
                        StatusChangeTypeId = Utility.PolicyStatusChangeType.IllustrationExpired.ToInt(),
                        StatusId = Utility.IllustrationStatus.TimeExpired.ID() != null ? Utility.IllustrationStatus.TimeExpired.ID().ToInt() : -1,
                        UserId = ObjServices.UserID
                    });

                    //Actualizar la tabla temp
                    ObjServices.UpdateTempTable(item.IllustrationNo, ObjServices.UserID.GetValueOrDefault());
                }

                var BlSelected = drpBusinessLine.SelectedValue.ToInt();
                int? blId = BlSelected > 0 ? BlSelected : (int?)null;
                bool filterAgent = ObjServices.IsSucripcionDirectorQuotRole || ObjServices.isUserCot || ObjServices.IsAgentServiceQuoRole;

                var parameters = new Policy.QuoGrid.Key
                {
                    CorpId = ObjServices.Corp_Id,
                    Tab = TabSelected.ToString().Replace("lnk", ""),
                    CompanyId = ObjServices.CompanyId,
                    DateTo = DateTo,
                    DateFrom = DateFrom,
                    OfficeId = OfficeId,
                    AgentId = filterAgent ? (int?)null : ObjServices.Agent_LoginId,
                    BlId = blId,
                    UserId = ObjServices.UserID,
                    Bandeja = ObjServices.Bandeja,
                    AgentChain = filterAgent ? null : ObjServices.AgentChain
                };

                var DataResult = ObjServices.oPolicyManager.GetAllCustomerPlanDetailQuo(parameters).ToList();

                return DataResult.Select(o => new IllustrationInformation
                {
                    InsuredId = o.ContactId,
                    CorpId = o.CorpId,
                    RegionId = o.RegionId,
                    CountryId = o.CountryId,
                    DomesticregId = o.DomesticregId,
                    StateProvId = o.StateProvId,
                    CityId = o.CityId,
                    OfficeId = o.OfficeId,
                    CaseSeqNo = o.CaseSeqNo,
                    HistSeqNo = o.HistSeqNo,
                    Company = o.CompanyDesc,
                    Identification = o.IdContact,
                    FamilyProduct = o.BlDesc.Translate(),
                    PlanGroupCode = o.BlDesc.Substring(0, 1).ToUpper(),
                    PlanType = o.ProductTypeDesc,
                    IllustrationNo = o.PolicyNo,
                    IllustrationDate = o.QuoDate,
                    ExpirationDate = o.QuoDate.AddDays(ExpirationDays),
                    IllustrationStatusCode = o.PolicyStatusNameKey,
                    AvailableDays = o.Days,
                    InsuredName = o.FullName,
                    InsuredAmount = o.InsuredAmount,
                    InsuredAmountF = o.InsuredAmount.ToFormatNumeric(),
                    TotalPremium = o.AnnualPremium,
                    TotalPremiumF = o.AnnualPremium.ToFormatNumeric(),
                    InitialPremium = o.InitialContribution,
                    InitialPremiumF = o.InitialContribution.ToFormatNumeric(),
                    Status = ("Illustration_" + o.PolicyStatusNameKey).Translate(),
                    Office = o.OfficeDesc,
                    AgentName = o.AgentName,
                    Channel = o.DistributionDesc,
                    NewStatusDate = ((o.InspectionQuoDate != null) ? (o.InspectionQuoDate) : null),
                    MissingDocuments = o.DocumentMissing,
                    AgentId = o.AgentId,
                    AssignedSubscriber = o.SubscriberName,
                    AssignedSubscriberId = o.SubscriberAgentId,
                    isExpired = o.Days < 0,
                    FinancialClearance = string.Empty
                }).ToList();
            }

            return new List<IllustrationInformation>();
        }

        private int? GetAvailableDays(Illustrator.CustomerPlanDetail customerPlan)
        {
            int? availableDays = null;

            if (customerPlan.IllustrationStatusCode == Utility.IllustrationStatus.Subscription.Code())
                return customerPlan.NewStatusDate.HasValue ? (DateTime.Now - customerPlan.NewStatusDate.Value).Days : 0;

            var daysDiff = (customerPlan.DateCreated.AddDays(ExpirationDays) - DateTime.Now).Days;

            if ((
                customerPlan.IllustrationStatusCode == Utility.IllustrationStatus.Illustration.Code() ||
                customerPlan.IllustrationStatusCode == Utility.IllustrationStatus.NewPlan.Code() ||
                customerPlan.IllustrationStatusCode == Utility.IllustrationStatus.PendingByClient.Code() ||
                customerPlan.IllustrationStatusCode == Utility.IllustrationStatus.ApprovedByClient.Code() ||
                customerPlan.IllustrationStatusCode == Utility.IllustrationStatus.MissingInspection.Code() ||
                TabSelected == Utility.Tabs.lnkHistoricalIllustrations) &&
               daysDiff > 0)
                availableDays = daysDiff;

            return availableDays;
        }

        private bool isExpiredQoutation(Illustrator.CustomerPlanDetail customerPlan)
        {
            var daysDiff = (customerPlan.DateCreated.AddDays(ExpirationDays) - DateTime.Now).Days;

            return daysDiff < 0;
        }

        private decimal? GetInsuredAmount(Illustrator.CustomerPlanDetail customerPlan)
        {
            decimal? insuredAmount = 0m;

            if (customerPlan.PlanGroupCode == Utility.EFamilyProductType.Education.Code() ||
                customerPlan.PlanGroupCode == Utility.EFamilyProductType.Retirement.Code())
                insuredAmount = customerPlan.AnnuityAmount;
            else if (customerPlan.PlanGroupCode == Utility.EFamilyProductType.Auto.Code())
                insuredAmount = customerPlan.InsuredAmount;
            else
                insuredAmount = customerPlan.BenefitAmount;

            return insuredAmount;
        }

        private decimal? GetTotalPremium(Illustrator.CustomerPlanDetail customerPlan)
        {
            decimal totalPremium = 0m;
            if (customerPlan.PlanGroupCode == Utility.EFamilyProductType.Education.Code() ||
                customerPlan.PlanGroupCode == Utility.EFamilyProductType.Retirement.Code())
                totalPremium = (customerPlan.AnnuityAmount * customerPlan.RetirementPeriod);
            else if (customerPlan.PlanGroupCode == Utility.EFamilyProductType.Auto.Code())
                totalPremium = customerPlan.PremiumAmount;
            else
                totalPremium = (customerPlan.BenefitAmount.GetValueOrDefault() + customerPlan.RiderTermAmount);

            return totalPremium;
        }

        private string GetCurrentIllustrationStatusCode()
        {
            string illustrationStatusCode = null;
            switch (TabSelected)
            {
                case Utility.Tabs.lnkIncompleteIllustration:
                    illustrationStatusCode = Utility.IllustrationStatus.Incomplete.Code();
                    break;
                case Utility.Tabs.lnkApprovedBySubscription:
                    illustrationStatusCode = Utility.IllustrationStatus.ApprovedBySubscription.Code();
                    break;
                case Utility.Tabs.lnkCompleteIllustrations:
                    illustrationStatusCode = Utility.IllustrationStatus.ApprovedByClient.Code();
                    break;
                case Utility.Tabs.lnkDeclinedByClient:
                    illustrationStatusCode = Utility.IllustrationStatus.DeclinedByClient.Code();
                    break;
                case Utility.Tabs.lnkExpired:
                    illustrationStatusCode = Utility.IllustrationStatus.TimeExpired.Code();
                    break;
                case Utility.Tabs.lnkApprovedByClient:
                    illustrationStatusCode = Utility.IllustrationStatus.ApprovedByClient.Code();
                    break;
                case Utility.Tabs.lnkSubscriptions:
                    illustrationStatusCode = Utility.IllustrationStatus.Subscription.Code();
                    break;
                case Utility.Tabs.lnkDeclinedBySubscription:
                    illustrationStatusCode = Utility.IllustrationStatus.DeclinedBySubscription.Code();
                    break;

                case Utility.Tabs.lnkMissingDocuments:
                    illustrationStatusCode = Utility.IllustrationStatus.MissingDocuments.Code();
                    break;

                case Utility.Tabs.lnkMissingInspections:
                    illustrationStatusCode = Utility.IllustrationStatus.MissingInspection.Code();
                    break;
            }

            return illustrationStatusCode;
        }

        private string GetCurrentTabFilter()
        {
            return TabSelected.ToString().Replace("lnk", "");
        }

        private void ConfigureColumnsGridByTab(ASPxGridView Gridview = null)
        {
            SettingColumns(Gridview == null ? gvIllustration : Gridview);

            if (ObjServices.Bandeja == "Propiedad")
            {
                gvIllustration.HideOrShowColumnGrid(false, "Rate");
                gvIllustration.HideOrShowColumnGrid(false, "Model");
                gvIllustration.HideOrShowColumnGrid(false, "Make");
                gvIllustration.HideOrShowColumnGrid(false, "Year");
                gvIllustration.HideOrShowColumnGrid(false, "AgentAccidentRate");
                gvIllustration.HideOrShowColumnGrid(false, "VendorAccidentRate");
                gvIllustration.HideOrShowColumnGrid(false, "ModelAccidentRate");
                gvIllustration.HideOrShowColumnGrid(false, "MinDeducCristales");
                gvIllustration.HideOrShowColumnGrid(false, "MinDeducDP");
                gvIllustration.HideOrShowColumnGrid(false, "PorcMinDeducDP");
            }
        }

        public List<IllustrationInformation> FillData(ASPxGridView Gridview = null)
        {
            ConfigureColumnsGridByTab(Gridview);

            var lstIllustrationInformation = new List<IllustrationInformation>();
            var listAgentIDByChain = new List<int>(0);
            DateTime? dateFrom = null;
            DateTime? dateTo = null;
            int? companyId = null;
            int? _corpId = null;
            int? _regionId = null;
            int? _countryId = null;
            int? _domesticregId = null;
            int? _stateProvId = null;
            int? _cityId = null;
            int? _officeId = null;
            int? _userId = null;
            string _agentNameId = null;
            string _agentType = null;
            string _assignedSubscriberNameId = null;


            var period = (Utility.PeriodsDate)drpPeriod.SelectedValue.ToInt();

            if (drpCompanyProfile.SelectedIndex > 0)
                companyId = drpCompanyProfile.SelectedValue.ToInt();

            switch (period)
            {
                case Utility.PeriodsDate.YTD:
                    dateFrom = new DateTime(DateTime.Now.Year, 1, 1).Date;
                    dateTo = DateTime.Now.Date;
                    break;
                case Utility.PeriodsDate.MTD:
                    dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;
                    dateTo = DateTime.Now.Date;
                    break;
                case Utility.PeriodsDate.ThirtyDays:
                    dateFrom = DateTime.Now.Date.AddDays(-30);
                    dateTo = DateTime.Now.Date;
                    break;
                case Utility.PeriodsDate.Quarter1:
                    //Enero - Febrero - Marzo
                    dateFrom = new DateTime(DateTime.Now.Year, 1, 1).Date;
                    dateTo = dateFrom.GetValueOrDefault().AddMonths(2).Date;
                    break;
                case Utility.PeriodsDate.Quarter2:
                    //De Abril - Mayo - Junio
                    dateFrom = new DateTime(DateTime.Now.Year, 4, 1).Date;
                    dateTo = dateFrom.GetValueOrDefault().AddMonths(2).Date;
                    break;
                case Utility.PeriodsDate.Quarter3:
                    //Julio - Agosto - Septiembre
                    dateFrom = new DateTime(DateTime.Now.Year, 7, 1).Date;
                    dateTo = dateFrom.GetValueOrDefault().AddMonths(2).Date;
                    break;
                case Utility.PeriodsDate.Quarter4:
                    //Octubre - Noviembre - Diciembre
                     dateFrom = new DateTime(DateTime.Now.Year, 11, 1).Date;
                    dateTo = dateFrom.GetValueOrDefault().AddMonths(2).Date;
                    break;
                case Utility.PeriodsDate.CustomDate:
                    dateFrom = txtFrom.Text.IsDateReturnNull();
                    dateTo = txtTo.Text.IsDateReturnNull();
                    if (dateTo.HasValue)
                        dateTo = dateTo.Value.AddDays(1).AddMilliseconds(-1);
                    break;
            }

            var isUserLogin = drpOffice.SelectedIndex > 0;

            if (isUserLogin)
            {
                var dataOffice = Utility.deserializeJSON<Utility.itemOfficce>(drpOffice.SelectedValue);
                _corpId = dataOffice.CorpId;
                _regionId = dataOffice.RegionId;
                _countryId = dataOffice.CountryId;
                _domesticregId = dataOffice.DomesticregId;
                _stateProvId = dataOffice.StateProvId;
                _cityId = dataOffice.CityId;
                _officeId = dataOffice.OfficeId;
            }

            ViewState["dateTo"] = dateTo;
            ViewState["dateFrom"] = dateFrom;
            ViewState["_officeId"] = _officeId;

            if (ObjServices.UserType != Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.User)
            {
                _userId = ObjIllustrationServices.IllusUserID;
                _agentNameId = AgentNameId;
                _agentType = ObjServices.UserType.ToString();

                if (ObjServices.IsSuscripcionQuotRole)
                {
                    _assignedSubscriberNameId = _agentNameId;
                    _agentNameId = null;
                }
            }

            if (ObjServices.IsSuscripcionManagerQuotRole || ObjServices.IsDirectorQuotRole || ObjServices.IsSucripcionDirectorQuotRole)
            {
                _agentNameId = null;
                _assignedSubscriberNameId = null;
            }

            if (ObjServices.IsInspectorQuotRole)
            {
                _agentNameId = null;
                _assignedSubscriberNameId = null;
            }

            string illustrationStatusCode = GetCurrentIllustrationStatusCode();
            string tabFilter = GetCurrentTabFilter();

            var DataResult = new List<Policy.QuoGrid>();

            var BlSelected = drpBusinessLine.SelectedValue.ToInt();
            int? blId = BlSelected > 0 ? BlSelected : (int?)null;
            bool filterAgent = ObjServices.IsSucripcionDirectorQuotRole || ObjServices.isUserCot || ObjServices.IsAgentServiceQuoRole;

            var parameters = new Policy.QuoGrid.Key
            {
                CorpId = ObjServices.Corp_Id,
                Tab = TabSelected.ToString().Replace("lnk", ""),
                CompanyId = companyId,
                DateTo = dateTo,
                DateFrom = dateFrom,
                OfficeId = _officeId,
                AgentId = filterAgent ? (int?)null : ObjServices.Agent_LoginId,
                BlId = blId,
                UserId = ObjServices.UserID,
                Bandeja = ObjServices.Bandeja,
                AgentChain = filterAgent ? null : ObjServices.AgentChain
            };

            DataResult = (TabSelected == Utility.Tabs.lnkPuntoVentaTab) ? ObjServices.oPolicyManager.GetAllPuntoVentaQuo(parameters).ToList()
                                                                        : ObjServices.oPolicyManager.GetAllCustomerPlanDetailQuo(parameters).ToList();

            /*
             Bl_Type_Id	Bl_Id	Bl_Desc
                 1	      1     Life
                 1        2 	Vehicle
                 1   	  3	    Health
                 1    	  4  	Incendio y Lineas Aliadas
             */

            var requiredDisabled = new[]
            {
                 Utility.IllustrationStatus.ApprovedBySubscription.Code(),
                 Utility.IllustrationStatus.DeclinedByClient.Code(),
                 Utility.IllustrationStatus.DeclinedBySubscription.Code(),
                 Utility.IllustrationStatus.Effective.Code(),
                 Utility.IllustrationStatus.TimeExpired.Code(),
                 Utility.IllustrationStatus.Cancelled.Code()
            };

            var RequestTypeList = new[]
            {
                 Utility.RequestType.Inclusion,
                 Utility.RequestType.Exclusion,
                 Utility.RequestType.Cambios,
                 Utility.RequestType.Renovacion,
                 Utility.RequestType.InclusionDeclarativa
            };

            int? Agent_AssignedSubscriber_Id = ObjServices.Agent_LoginId;
            /*
              Agregado por Carlos Lebron
              el 31/07/2018
            */
            if (TabSelected == Utility.Tabs.lnkConfirmationCall && DataResult.Any())
            {
                //Sacar de la llamada de confirmacion los casos de cambios y las inclusion declarativa
                DataResult = DataResult.Where(c => !ObjServices.ExConfirmationCall.Contains(c.RequestTypeId.GetValueOrDefault())).ToList();
            }

            if (TabSelected != Utility.Tabs.lnkPuntoVentaTab)
            {
                #region Tabs: para Trabajar/Completas/Declinadas por Cliente/Expiradas/por Expirar/Suscripciones/Descuentos/Llamada de Confirmacion/Documentos Faltantes/Inspecciones/Facultativos/Declinado por Suscripcion/Aprobadas por Subscripcion/Historico
                lstIllustrationInformation = DataResult.Select(o => new IllustrationInformation
                {
                    InsuredId = o.ContactId,
                    CorpId = o.CorpId,
                    RegionId = o.RegionId,
                    CountryId = o.CountryId,
                    DomesticregId = o.DomesticregId,
                    StateProvId = o.StateProvId,
                    CityId = o.CityId,
                    OfficeId = o.OfficeId,
                    CaseSeqNo = o.CaseSeqNo,
                    HistSeqNo = o.HistSeqNo,
                    Company = o.CompanyDesc,
                    Identification = o.IdContact,
                    FamilyProduct = o.BlDesc.Translate(),
                    PlanType = o.ProductTypeDesc,
                    IllustrationNoTemp = o.PolicyNoTemp,
                    ProductSubTypeDesc = o.ProductSubTypeDesc,
                    IllustrationNo = o.PolicyNo,
                    IllustrationDate = o.QuoDate,
                    ExpirationDate = o.QuoDate.AddDays(ExpirationDays),
                    PlanGroupCode = o.BlDesc.Substring(0, 1).ToUpper(),
                    QuoPosDate = o.QuoPosDate,
                    EffectiveDate = o.EffectiveDate,
                    PolicyExpirationDate = o.PolicyExpirationDate,
                    WorkMinute = o.WorkMinute,
                    SubscriptionMinute = o.SubscriptionMinute,
                    InspectionMinute = o.InspectionMinute,
                    HasDiscount = o.HasDiscount,
                    HasSurcharge = o.HasSurcharge,
                    MakeDiscount = o.MakeDiscount,
                    TieneDescuento = o.HasDiscount ? Resources.YesLabel : Resources.NoLabel,
                    TieneRecargo = o.HasSurcharge ? Resources.YesLabel : Resources.NoLabel,
                    IllustrationStatusCode = o.PolicyStatusNameKey,
                    AvailableDays = o.Days,
                    InsuredName = o.FullName,
                    SupervisorAgentName = o.SupervisorAgentName,
                    QuoDate = o.QuoDate,
                    DeclinedQuoReason = o.DeclinedQuoReason,
                    MissingDocumentQuoReason = o.MissingDocumentQuoReason,
                    ConfirmationCallerName = o.ConfirmationCallerName,
                    InsuredAmount = o.InsuredAmount,
                    InsuredAmountF = o.InsuredAmount.ToFormatNumeric(),
                    TotalPremium = o.AnnualPremium,
                    TotalPremiumF = o.AnnualPremium.ToFormatNumeric(),
                    InitialPremium = o.InitialContribution,
                    InitialPremiumF = o.InitialContribution.ToFormatNumeric(),
                    Status = ("Illustration_" + o.PolicyStatusNameKey).Translate(),
                    Discount = o.DiscountAmount,
                    DiscountF = o.DiscountAmount.ToFormatNumeric(),
                    Office = o.OfficeDesc,
                    AgentName = o.AgentName,
                    InspectionQuoDate = o.InspectionQuoDate,
                    Channel = o.DistributionDesc,
                    NewStatusDate = ((o.InspectionQuoDate != null) ? (o.InspectionQuoDate) : null),
                    MissingDocuments = o.DocumentMissing,
                    AgentId = o.AgentId,
                    InspectorAgentId = o.InspectorAgentId,
                    InspectorAgent = o.InspectorName,
                    AssignedSubscriber = o.SubscriberName,
                    AssignedSubscriberId = o.SubscriberAgentId,
                    isExpired = o.IsExpired,
                    IsExpiring = o.IsExpiring,
                    TipoRiesgoNameKey = o.TipoRiesgoNameKey == "N/A" ? "NONE" : o.TipoRiesgoNameKey,
                    DeclinedQuoDate = ((o.DeclinedQuoDate != null) ? (o.DeclinedQuoDate) : null),
                    FinancialClearance = !string.IsNullOrEmpty(o.TipoRiesgoNameKey) ? Utility.GetImgRiesgo((Utility.TipoRiesgo)Enum.Parse(typeof(Utility.TipoRiesgo), o.TipoRiesgoNameKey == "N/A" ? "NONE" : o.TipoRiesgoNameKey))
                                                                                    : string.Empty,
                    DocumentRequiredEnabled = !requiredDisabled.Contains(o.PolicyStatusNameKey),
                    DocumentRequiredCssClass = requiredDisabled.Contains(o.PolicyStatusNameKey) ? "upload_file_des" : "required_doc",
                    QuoCreateUserName = o.QuoCreateUserName,
                    PolicyStatusNameKey = o.PolicyStatusNameKey,
                    PolicyNoMain = !string.IsNullOrEmpty(o.RequestTypeDesc) ? RequestTypeList.Contains(((Utility.RequestType)Enum.Parse(typeof(Utility.RequestType), o.RequestTypeDesc.Replace(" ", "").MyRemoveInvalidCharactersFilName()))) ? o.PolicyNoMain : "N/A" : string.Empty,
                    InspectionFormEnabled = o.NeedInspection,
                    InspectionFormCssClass = o.NeedInspection ? "inspection_doc" : "inspection_doc_ocultar_boton",
                    HasFacultative = o.HasFacultative,
                    ProratedPremium = o.ProratedPremium.GetValueOrDefault(),
                    RequestTypeId = o.RequestTypeId.GetValueOrDefault(),
                    RequestTypeDesc = o.RequestTypeDesc,
                    Financed = o.Financed.GetValueOrDefault() ? "Si" : "No",
                    Rate = o.Rate > 0 ? o.Rate.ToFormatNumeric() + "%" : "-",
                    Make = o.Make,
                    Model = o.Model,
                    Year = o.Year,
                    ModelAccidentRate = o.ModelAccidentRate > 0 ? o.ModelAccidentRate.ToFormatNumeric() + "%" : "-",
                    VendorAccidentRate = o.VendorAccidentRate > 0 ? o.VendorAccidentRate.ToFormatNumeric() + "%" : "-",
                    AgentAccidentRate = o.AgentAccidentRate > 0 ? o.AgentAccidentRate.ToFormatNumeric() + "%" : "-",
                    MinDeducCristales = o.MinDeducCristales != "FLOTILLA" ? string.Format(CultureInfo.InvariantCulture, "{0:#,0.00}", Convert.ToDecimal(o.MinDeducCristales.Replace(",", ""), CultureInfo.InvariantCulture)) : o.MinDeducCristales,
                    MinDeducDP = o.MinDeducDP != "FLOTILLA" ? string.Format(CultureInfo.InvariantCulture, "{0:#,0.00}", Convert.ToDecimal(o.MinDeducDP.Replace(",", ""), CultureInfo.InvariantCulture)) : o.MinDeducDP,
                    PorcDeducDP = o.PorcDeducDP != "FLOTILLA" ? string.Format(CultureInfo.InvariantCulture, "{0:#,0}", Convert.ToDecimal(o.PorcDeducDP.Replace(",", ""), CultureInfo.InvariantCulture)) + "%" : o.PorcDeducDP
                }).ToList();

                #region llustrations To Work
                if (TabSelected == Utility.Tabs.lnkIllustrationsToWork)
                {
                    var notIllustrationsToWork = new List<string>();

                    notIllustrationsToWork.AddRange(new string[] {
                    Utility.IllustrationStatus.Subscription.Code(),
                     Utility.IllustrationStatus.ApprovedBySubscription.Code(),
                     Utility.IllustrationStatus.DeclinedByClient.Code(),
                     Utility.IllustrationStatus.DeclinedBySubscription.Code(),
                     Utility.IllustrationStatus.MissingDocuments.Code(),
                     Utility.IllustrationStatus.MissingInspection.Code(),
                     Utility.IllustrationStatus.TimeExpired.Code()
                    });

                    lstIllustrationInformation = lstIllustrationInformation.Where(o => !notIllustrationsToWork.Contains(o.IllustrationStatusCode)).ToList();
                }
                #endregion

                #region Si el tab es Confirmation Call Excluir los casos de Exclusion y Cambios
                /*
                 //Comentado porque el senor leonel rosario dice que si deben caer en ese tab
                if (TabSelected == Utility.Tabs.lnkConfirmationCall)
                {
                    var RequestTypeListToExclude = new[]
                    {
                        Utility.RequestType.Exclusion,
                        Utility.RequestType.Cambios
                    };

                    lstIllustrationInformation = lstIllustrationInformation.Where(o => !RequestTypeListToExclude.Contains(((Utility.RequestType)Enum.Parse(typeof(Utility.RequestType), o.RequestTypeDesc.Replace(" ", "").MyRemoveInvalidCharactersFilName())))).ToList();
                }
                */
                #endregion

                #region Enviando a Expiradas
                //--Tabs en donde se recojeran las expiradas: Cotizaciones para Trabajar, Cotizaciones Completas, Por Expirar, Subscripciones, Documentos Faltantes, Inspecciones Faltantes
                List<Utility.Tabs> TabsToGetExpired = new List<Utility.Tabs>();
                TabsToGetExpired.AddRange(new Utility.Tabs[] {  Utility.Tabs.lnkIllustrationsToWork,
                                                                Utility.Tabs.lnkCompleteIllustrations,
                                                                Utility.Tabs.lnkExpiring,
                                                                Utility.Tabs.lnkSubscriptions,
                                                                Utility.Tabs.lnkMissingDocuments,
                                                                Utility.Tabs.lnkMissingInspections});

                if (TabsToGetExpired.Contains(TabSelected))
                {
                    /*Poniendo cotizaciones en expiradas*/
                    var containsExpired = SetPolicyStatusExpired(_userId,
                                                                 companyId,
                                                                 dateFrom,
                                                                 dateTo,
                                                                 ObjServices.Corp_Id,
                                                                 ObjServices.Region_Id,
                                                                 ObjServices.Country_Id,
                                                                 ObjServices.Domesticreg_Id,
                                                                 ObjServices.State_Prov_Id,
                                                                 ObjServices.City_Id,
                                                                 ObjServices.Office_Id,
                                                                 ObjServices.Language.ToInt(),
                                                                 TabSelected == Utility.Tabs.lnkHistoricalIllustrations,
                                                                 Utility.IllustrationStatus.TimeExpired.Code(),
                                                                 tabFilter,
                                                                 _agentNameId,
                                                                 _agentType,
                                                                 _assignedSubscriberNameId,
                                                                 lstIllustrationInformation
                                                                );
                    if (containsExpired.Any())
                        lstIllustrationInformation = containsExpired;
                }
                else if (TabSelected == Utility.Tabs.lnkExpired)
                    lstIllustrationInformation = lstIllustrationInformation.ToList();
                else
                    if (TabSelected != Utility.Tabs.lnkHistoricalIllustrations && TabSelected != Utility.Tabs.lnkConfirmationCall)
                        lstIllustrationInformation = lstIllustrationInformation.Where(o => o.isExpired == false).ToList();
                #endregion

                if (TabSelected == Utility.Tabs.lnkExpiring)
                    lstIllustrationInformation = lstIllustrationInformation.Where(x => x.IsExpiring == true).ToList();

                if (TabSelected == Utility.Tabs.lnkDiscounts)
                    lstIllustrationInformation = lstIllustrationInformation.Where(x => x.MakeDiscount).ToList();
                #endregion
            }
            else
            {
                #region Tab: Cotizaciones en Punto de Ventas
                lstIllustrationInformation = DataResult.Select(o => new IllustrationInformation
                {
                    PlanType = o.ProductTypeDesc,
                    IllustrationNo = o.PolicyNo,
                    IllustrationDate = o.QuoDate,
                    InsuredName = o.FullName,
                    InsuredAmountF = o.InsuredAmount.ToFormatNumeric(),
                    TotalPremiumF = o.AnnualPremium.ToFormatNumeric(),
                    Office = o.OfficeDesc,
                    AgentName = o.AgentName,
                    AgentPhones = o.AgentPhones
                }).ToList();
                #endregion
            }

            #region List Agents By Chain
            if (ObjServices.IsDirectorQuotRole && drpAgent.SelectedValue == "-1")
            {
                foreach (ListItem item in drpAgent.Items)
                {
                    if (item.Value != "-1")
                    {
                        Agent_AssignedSubscriber_Id = item.Value.ToInt();
                        listAgentIDByChain.Add(Agent_AssignedSubscriber_Id.Value);
                    }
                }

                lstIllustrationInformation = lstIllustrationInformation.Where(o => listAgentIDByChain.Contains(o.AgentId.GetValueOrDefault())).ToList();
            }
            #endregion

            #region Assigned Subscriber
            if (drpAgent.SelectedIndex > 0)
            {
                Agent_AssignedSubscriber_Id = drpAgent.SelectedValue.ToInt();
                lstIllustrationInformation = lstIllustrationInformation.Where(o => o.AgentId == Agent_AssignedSubscriber_Id).ToList();
            }
            #endregion

            #region Excluir los Tabs: Historico,Aprobado por Subscripcion, Declinado por Subscripcion, Declinado por Cliente, Expiradas de las validaciones de inpeccion
            var excludedTabs = new Utility.Tabs[]
            {
                Utility.Tabs.lnkHistoricalIllustrations,
                Utility.Tabs.lnkApprovedBySubscription,
                Utility.Tabs.lnkDeclinedBySubscription,
                Utility.Tabs.lnkDeclinedByClient,
                Utility.Tabs.lnkExpired,
                Utility.Tabs.lnkPuntoVentaTab
            };

            #endregion

            var GridSender = (TabSelected != Utility.Tabs.lnkPuntoVentaTab) ? gvIllustration : gvPOSCotizaciones;
            //Esto lo comento para cuando tenga que buscar la ejecucion de una poliza en especifico
            lstIllustrationInformation = lstIllustrationInformation.Where(p => p.PolicyNoMain == "13-05-535855").ToList();
            //lstIllustrationInformation = lstIllustrationInformation.ToList();
            GridSender.DataSource = lstIllustrationInformation;

            #region Sortear registros segun el tab
            GridViewDataColumn Column = null;
            switch (TabSelected)
            {
                case Utility.Tabs.lnkIllustrationsToWork:
                    Column = gvIllustration.getThisColumnEx("Illustration_Date");
                    break;
                case Utility.Tabs.lnkDeclinedByClient:
                    Column = gvIllustration.getThisColumnEx("DeclinedQuoDate");
                    break;
                case Utility.Tabs.lnkExpired:
                    Column = gvIllustration.getThisColumnEx("ExpirationDate");
                    break;
                case Utility.Tabs.lnkSubscriptions:
                    Column = gvIllustration.getThisColumnEx("Illustration_Date");
                    break;
                case Utility.Tabs.lnkDeclinedBySubscription:
                    Column = gvIllustration.getThisColumnEx("DeclinedQuoDate");
                    break;
                case Utility.Tabs.lnkMissingInspections:
                    Column = gvIllustration.getThisColumnEx("InspectionQuoDateLabel");
                    break;
                case Utility.Tabs.lnkApprovedBySubscription:
                    Column = gvIllustration.getThisColumnEx("FechaAprobacionLabel");
                    break;
                case Utility.Tabs.lnkConfirmationCall:
                    Column = gvIllustration.getThisColumnEx("Illustration_Date");
                    break;
                case Utility.Tabs.lnkCompleteIllustrations:
                    Column = gvIllustration.getThisColumnEx("");
                    break;
                case Utility.Tabs.lnkExpiring:
                    Column = gvIllustration.getThisColumnEx("");
                    break;
                case Utility.Tabs.lnkMissingDocuments:
                    Column = gvIllustration.getThisColumnEx("");
                    break;
                case Utility.Tabs.lnkHistoricalIllustrations:
                    Column = gvIllustration.getThisColumnEx("");
                    break;
                case Utility.Tabs.lnkDiscounts:
                    Column = gvIllustration.getThisColumnEx("");
                    break;
                case Utility.Tabs.lnkFacultative:
                    Column = gvIllustration.getThisColumnEx("");
                    break;
            }

            if (Column != null)
                Column.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

            #endregion

            GridSender.DataBind();
            GridSender.FocusedRowIndex = -1;
            gvIllustration.SetFilterSettings();
            setCounterCurrentTab();

            if (!string.IsNullOrEmpty(ObjServices.Bandeja))
            {
                hdnTabBandeja.Value = ObjServices.Bandeja == "Propiedad" ? "#Comerciales" : "#liPuntoVenta";
                this.ExcecuteJScript("setActiveClassTabsNewbusiness('" + hdnTabBandeja.Value + "');");
            }
            else
                hdnTabBandeja.Value = string.Empty;

            return
                 lstIllustrationInformation;
        }

        private void setCounterCurrentTab()
        {
            var hdn = string.Concat("hdn", GetCurrentTabFilter(), "Count");
            var Hidden = this.FindControl(hdn);
            if (Hidden != null)
                (Hidden as HiddenField).Value = gvIllustration.VisibleRowCount.ToString();

            this.ExcecuteJScript("SetCounters();");
        }

        public void Initialize()
        {
            Cache["ChangeStatusDependency"] = DateTime.Now;
            FillDropDown();
            ChangePeriod();
            gvIllustration.FocusedRowIndex = -1;
            hideTabsByRol();
            ColumnsTabConfigure = false;
        }

        protected void gvIllustration_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            var CallbackName = e.CallbackName;

            if (Utility.CallBackList.Contains(CallbackName))
            {
                gvIllustration.FocusedRowIndex = -1;
                gvIllustration.SetFilterSettings();
                FillData();
            }
        }

        private void Search()
        {
            FillData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void drpPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangePeriod();
        }

        public void ChangePeriod()
        {
            //var period = (Utility.PeriodsDate)drpPeriod.SelectedValue.ToInt();
            //txtFrom.Enabled = txtTo.Enabled = period == Utility.PeriodsDate.CustomDate;
            //txtFrom.Text = txtTo.Text = "";
        }

        protected void btnPrintList_Click(object sender, EventArgs e)
        {
            SettingColumns(gvFakeGridView);
            var DataExport = FillData(gvFakeGridView);

            if (hdnTabSelected.Value == "lnkPuntoVentaTab")
                OcultarColumnasFakeGrid();

            gvFakeGridView.DataSource = DataExport;
            gvFakeGridView.DataBind();

            ASPxGridViewExporter1.WriteXlsxToResponse("IllustrationList", true, new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }

        private void OcultarColumnasFakeGrid()
        {
            var _invisible = new List<string>()
            {
                "IllustrationNo",
                "Status",
                "Identification",
                "InitialPremiumF",
                "ExpirationDate",
                "NewStatusDate",
                "AssignedSubscriber",
                "MissingDocuments",
                "AvailableDays"
            };

            foreach (GridViewColumn item in gvFakeGridView.Columns)
            {
                item.Visible = !_invisible.Contains(item.Name);
                if (item.Name == "Channel")
                    item.Caption = "Identification";
            }
        }

        private void ChangeStatus(Utility.IllustrationStatus illustrationStatus, string note, List<Policy.VehiclesCoverage> lst = null, string Comment = null)
        {
            var policyNo = "";

            try
            {
                if (illustrationStatus == Utility.IllustrationStatus.DeclinedByClient && !CanDeclinedIllustrations())
                    return;

                for (int i = gvIllustration.VisibleStartIndex; i < gvIllustration.VisibleRowCount; i++)
                {
                    var chk = gvIllustration.FindRowCellTemplateControl(i, null, "chkSelect") as CheckBox;

                    if (chk != null && chk.Checked)
                    {
                        var vCorpId = gvIllustration.GetKeyFromAspxGridView("CorpId", i).ToInt();
                        var vRegionId = gvIllustration.GetKeyFromAspxGridView("RegionId", i).ToInt();
                        var vCountryId = gvIllustration.GetKeyFromAspxGridView("CountryId", i).ToInt();
                        var vDomesticregId = gvIllustration.GetKeyFromAspxGridView("DomesticregId", i).ToInt();
                        var vStateProvId = gvIllustration.GetKeyFromAspxGridView("StateProvId", i).ToInt();
                        var vCityId = gvIllustration.GetKeyFromAspxGridView("CityId", i).ToInt();
                        var vOfficeId = gvIllustration.GetKeyFromAspxGridView("OfficeId", i).ToInt();
                        var vCaseSeqNo = gvIllustration.GetKeyFromAspxGridView("CaseSeqNo", i).ToInt();
                        var vHistSeqNo = gvIllustration.GetKeyFromAspxGridView("HistSeqNo", i).ToInt();
                        policyNo = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", i).ToString();
                        var familyProduct = gvIllustration.GetKeyFromAspxGridView("FamilyProduct", i).ToString();
                        var illustrationStatusCode = gvIllustration.GetKeyFromAspxGridView("IllustrationStatusCode", i).ToString();

                        ObjServices.Corp_Id = vCorpId;
                        ObjServices.Region_Id = vRegionId;
                        ObjServices.Country_Id = vCountryId;
                        ObjServices.Domesticreg_Id = vDomesticregId;
                        ObjServices.State_Prov_Id = vStateProvId;
                        ObjServices.City_Id = vCityId;
                        ObjServices.Office_Id = vOfficeId;
                        ObjServices.Case_Seq_No = vCaseSeqNo;
                        ObjServices.Hist_Seq_No = vHistSeqNo;

                        ObjServices.ChangeIllustrationStatus
                        (
                            -1,
                            vCorpId,
                            vRegionId,
                            vCountryId,
                            vDomesticregId,
                            vStateProvId,
                            vCityId,
                            vOfficeId,
                            vCaseSeqNo,
                            vHistSeqNo,
                            ObjServices.UserID.GetValueOrDefault(),
                            illustrationStatus,
                            note
                        );
                    }
                }

                this.MessageBox(Resources.StatusChangedSuccessfully, Title: Resources.Success);
                FillData();

            }
            catch (Exception ex)
            {
                var msg = ex.GetLastInnerException().Message;
                this.MessageBox(Resources.AErrorOccurredWithIllustrationNo.SFormat(policyNo) + (!msg.SIsNullOrEmpty() ? " - " + msg.RemoveInvalidCharacters().Replace('\'', '\"') : ""), Title: "Error");
            }
        }

        private bool CanDeclinedIllustrations()
        {
            var policyNo = "";
            try
            {
                var familyProduct = "";
                for (int i = gvIllustration.VisibleStartIndex; i < gvIllustration.VisibleRowCount; i++)
                {
                    var chk = gvIllustration.FindRowCellTemplateControl(i, null, "chkSelect") as CheckBox;

                    if (chk != null && chk.Checked)
                    {
                        var newFamilyProduct = gvIllustration.GetKeyFromAspxGridView("FamilyProduct", i).ToString();
                        if (!familyProduct.SIsNullOrEmpty() && familyProduct != newFamilyProduct)
                            throw new Exception(Resources.CantDeclinedIllustrationsWithDifferentBusinessLine);

                        policyNo = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", i).ToString();
                        familyProduct = newFamilyProduct;
                        var illustrationStatusCode = gvIllustration.GetKeyFromAspxGridView("IllustrationStatusCode", i).ToString();

                        if (!ObjServices.CanChangeStatus(illustrationStatusCode))
                            throw new Exception(Resources.IllustrationCantChangeHisStatus);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.GetLastInnerException().Message;
                this.MessageBox(Resources.AErrorOccurredWithIllustrationNo.SFormat(policyNo) + (!msg.SIsNullOrEmpty() ? " - " + msg.RemoveInvalidCharacters().Replace('\'', '\"') : ""), Title: "Error");
            }
            return false;
        }

        private bool CanSentToSubscription(int corpId,
                                           int regionId,
                                           int countryId,
                                           int domesticregId,
                                           int stateProvId,
                                           int cityId,
                                           int officeId,
                                           int caseSeqNo,
                                           int histSeqNo,
                                           string illustrationStatusCode)
        {
            var lstVehicles = ObjServices.oPolicyManager.GetVehiclesCoverage(new Policy.Parameter
            {
                CorpId = corpId,
                RegionId = regionId,
                CountryId = countryId,
                DomesticregId = domesticregId,
                StateProvId = stateProvId,
                CityId = cityId,
                OfficeId = officeId,
                CaseSeqNo = caseSeqNo,
                HistSeqNo = histSeqNo
            }).ToList();

            var hasJustOneVehicleAndBasic =
                lstVehicles.Count == 1 &&
                lstVehicles.Single().ProductTypeNameKey == Utility.ProductBehavior.basico.ToString();

            return !hasJustOneVehicleAndBasic && new[] {
                    Utility.IllustrationStatus.NewPlan.Code(),
                    Utility.IllustrationStatus.PendingByClient.Code(),
                    Utility.IllustrationStatus.ApprovedByClient.Code(),
                    Utility.IllustrationStatus.Illustration.Code()
            }.Contains(illustrationStatusCode);
        }

        protected void gvIllustration_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            var command = e.CommandArgs.CommandName;
            var index = e.VisibleIndex;

            var vCorpId = gvIllustration.GetKeyFromAspxGridView("CorpId", index).ToInt();
            var vRegionId = gvIllustration.GetKeyFromAspxGridView("RegionId", index).ToInt();
            var vCountryId = gvIllustration.GetKeyFromAspxGridView("CountryId", index).ToInt();
            var vDomesticregId = gvIllustration.GetKeyFromAspxGridView("DomesticregId", index).ToInt();
            var vStateProvId = gvIllustration.GetKeyFromAspxGridView("StateProvId", index).ToInt();
            var vCityId = gvIllustration.GetKeyFromAspxGridView("CityId", index).ToInt();
            var vOfficeId = gvIllustration.GetKeyFromAspxGridView("OfficeId", index).ToInt();
            var vCaseSeqNo = gvIllustration.GetKeyFromAspxGridView("CaseSeqNo", index).ToInt();
            var vHistSeqNo = gvIllustration.GetKeyFromAspxGridView("HistSeqNo", index).ToInt();
            var priority = gvIllustration.GetKeyFromAspxGridView("Priority", index).ToBoolean();
            var PolicyNoMain = gvIllustration.GetKeyFromAspxGridView("PolicyNoMain", index).ToString();

            ObjServices.PolicyNoMain = PolicyNoMain;
            ObjServices.Policy_Id = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", index).ToString();
            ObjServices.QuotationAgentId = gvIllustration.GetKeyFromAspxGridView("AgentId", index).ToInt();
            ObjServices.CustomerName = gvIllustration.GetKeyFromAspxGridView("InsuredName", index).ToString();


            //var PolicyKey = Utility.serializeToJSON(new
            //{
            //    vCorpId = vCorpId,
            //    vRegionId = vRegionId,
            //    vCountryId = vCountryId,
            //    vDomesticregId = vDomesticregId,
            //    vStateProvId = vStateProvId,
            //    vCityId = vCityId,
            //    vOfficeId = vOfficeId,
            //    vCaseSeqNo = vCaseSeqNo,
            //    vHistSeqNo = vHistSeqNo,
            //    PolicyNoMain = PolicyNoMain
            //});


            switch (command)
            {
                case "Note":
                    ucPopupChangeStatusSaveNotes.FillNotes(vCorpId,
                                                           vRegionId,
                                                           vCountryId,
                                                           vDomesticregId,
                                                           vStateProvId,
                                                           vCityId,
                                                           vOfficeId,
                                                           vCaseSeqNo,
                                                           vHistSeqNo);

                    this.ExcecuteJScript("ShowNotes()");
                    break;

                case "Required":
                    Session["RequiredTab"] = false;
                    RequiredDocs(index);
                    break;

                case "Inspection":
                    //Session["RequiredTab"] = false;
                    InspectionForm(index);
                    break;

                case "VerCotPol":
                    VerCotizacion(index);
                    break;
            }
        }

        protected void gvIllustration_BeforeHeaderFilterFillItems(object sender, ASPxGridViewBeforeHeaderFilterFillItemsEventArgs e)
        {
            IsHeaderFilter = true;
            FillData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private bool ThisColumnExist(string Name)
        {
            var result = false;
            foreach (var item in gvIllustration.Columns)
            {
                var Columna = item as GridViewColumn;
                if (Columna.Name == Name)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private void hideButtonFrontByRoles(string button)
        {
            if (ObjServices.IsAgentQuotRole)
            {
                switch (button)
                {
                    case "lnkIllustrationsToWork":
                    case "lnkCompleteIllustrations":
                    case "lnkSubscriptions":
                    case "lnkDiscounts":
                    case "lnkMissingDocuments":
                    case "lnkMissingInspections":
                    case "lnkExpiring":
                        pnlApprovedBySubscription.Visible = false;
                        pnlAssignIllustrations.Visible = true;
                        pnlDeclinedByClient.Visible = true;
                        break;
                    default:
                        pnlAssignIllustrations.Visible = false;
                        pnlDeclinedByClient.Visible = false;
                        pnlApprovedBySubscription.Visible = false;
                        break;
                }
            }
            else if (ObjServices.IsSucripcionDirectorQuotRole || ObjServices.IsSuscripcionManagerQuotRole || ObjServices.IsSuscripcionQuotRole)
            {
                switch (button)
                {
                    case "lnkMissingInspections":
                        pnlAssignIllustrations.Visible = true;
                        break;
                    case "lnkSubscriptions":
                    case "lnkDiscounts":
                        pnlApprovedBySubscription.Visible = true;
                        break;
                    default:
                        pnlAssignIllustrations.Visible = false;
                        pnlApprovedBySubscription.Visible = false;
                        break;
                }
            }
            else if (ObjServices.IsSuscripcionQuotRole)
            {
                switch (button)
                {
                    case "lnkSubscriptions":
                    case "lnkDiscounts":
                        pnlApprovedBySubscription.Visible = true;
                        break;
                    default:
                        pnlApprovedBySubscription.Visible = false;
                        break;
                }
            }
            else if (ObjServices.isUserCot)
            {
                switch (button)
                {
                    case "lnkIllustrationsToWork":
                    case "lnkCompleteIllustrations":
                    case "lnkMissingDocuments":
                    case "lnkMissingInspections":
                    case "lnkExpiring":
                        pnlApprovedBySubscription.Visible = false;
                        pnlDeclinedByClient.Visible = true;
                        break;

                    case "lnkSubscriptions":
                    case "lnkDiscounts":
                        pnlApprovedBySubscription.Visible = true;
                        pnlDeclinedByClient.Visible = true;
                        break;

                    default:
                        pnlDeclinedByClient.Visible = false;
                        pnlApprovedBySubscription.Visible = false;
                        break;
                }
            }
            else if (ObjServices.isReclamacionesQuotRole)
            {
                pnlApprovedBySubscription.Visible = false;
                pnlDeclinedByClient.Visible = false;
                pnlAssignIllustrations.Visible = false;
            }

            if (TabSelected == Utility.Tabs.lnkConfirmationCall)
            {
                pnlApprovedBySubscription.Visible = false;
                pnlDeclinedByClient.Visible = false;
                pnlAssignIllustrations.Visible = false;
            }
        }

        /// <summary>
        /// Enviar al core (Sysflex, Plexis)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApproveBySubscription_Click(object sender, EventArgs e)
        {
            var PolicyList = new List<string>(0);
            var result = new Tuple<string, string, bool>(string.Empty, string.Empty, false);

            try
            {

                for (int i = gvIllustration.VisibleStartIndex; i < gvIllustration.VisibleRowCount; i++)
                {
                    var chk = gvIllustration.FindRowCellTemplateControl(i, null, "chkSelect") as CheckBox;

                    if (chk != null && chk.Checked)
                    {
                        var vPolicyNumber = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", i).ToString();
                        var vCorpId = gvIllustration.GetKeyFromAspxGridView("CorpId", i).ToInt();
                        var vRegionId = gvIllustration.GetKeyFromAspxGridView("RegionId", i).ToInt();
                        var vCountryId = gvIllustration.GetKeyFromAspxGridView("CountryId", i).ToInt();
                        var vDomesticregId = gvIllustration.GetKeyFromAspxGridView("DomesticregId", i).ToInt();
                        var vStateProvId = gvIllustration.GetKeyFromAspxGridView("StateProvId", i).ToInt();
                        var vCityId = gvIllustration.GetKeyFromAspxGridView("CityId", i).ToInt();
                        var vOfficeId = gvIllustration.GetKeyFromAspxGridView("OfficeId", i).ToInt();
                        var vCaseSeqNo = gvIllustration.GetKeyFromAspxGridView("CaseSeqNo", i).ToInt();
                        var vHistSeqNo = gvIllustration.GetKeyFromAspxGridView("HistSeqNo", i).ToInt();
                        var familyProduct = gvIllustration.GetKeyFromAspxGridView("FamilyProduct", i).ToString();
                        var illustrationStatusCode = gvIllustration.GetKeyFromAspxGridView("IllustrationStatusCode", i).ToString();

                        ObjServices.Corp_Id = vCorpId;
                        ObjServices.Region_Id = vRegionId;
                        ObjServices.Country_Id = vCountryId;
                        ObjServices.Domesticreg_Id = vDomesticregId;
                        ObjServices.State_Prov_Id = vStateProvId;
                        ObjServices.City_Id = vCityId;
                        ObjServices.Office_Id = vOfficeId;
                        ObjServices.Case_Seq_No = vCaseSeqNo;
                        ObjServices.Hist_Seq_No = vHistSeqNo;

                        //Enviar la cotizacion al core SysFlex
                        if (familyProduct == Utility.EFamilyProductType.Auto.ToString() && illustrationStatusCode == Utility.IllustrationStatus.Subscription.Code())
                        {
                            ObjServices.VerifyCanSendQuotesToSysFlex(familyProduct, illustrationStatusCode, vPolicyNumber);
                            result = ObjServices.SendQuotToSysFlex(vCorpId, vRegionId, vCountryId, vDomesticregId, vStateProvId, vCityId, vOfficeId, vCaseSeqNo, vHistSeqNo, false, vPolicyNumber, Server.MapPath("~/NewBusiness/XML/"));
                        }

                        var message = string.Format(Resources.PolicySysflexMessage, result.Item1);

                        if (result.Item3)
                            message = string.Format(Resources.ErrorGeneratingSysflexInvoicing, result.Item1, string.Concat("<br/><br/>", " Se produjo un error generando factura(s)"));

                        PolicyList.Add(message);
                        var msj = string.Join("<br/>", PolicyList.ToArray());
                        this.MessageBox(msj, Width: 445);
                    }
                }

                Search();
            }
            catch (Exception ex)
            {
                var msg = ex.GetLastInnerException().Message;
                this.MessageBox(msg.RemoveInvalidCharacters().Replace('\'', '\"'), Title: "Error", Width: 800);
            }
        }

        private void RequiredDocs(int index)
        {
            ObjServices.Policy_Id = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", index).ToString();
            var planGroupCode = gvIllustration.GetKeyFromAspxGridView("PlanGroupCode", index).ToString();
            var vCorpId = gvIllustration.GetKeyFromAspxGridView("CorpId", index).ToInt();
            var vRegionId = gvIllustration.GetKeyFromAspxGridView("RegionId", index).ToInt();
            var vCountryId = gvIllustration.GetKeyFromAspxGridView("CountryId", index).ToInt();
            var vDomesticregId = gvIllustration.GetKeyFromAspxGridView("DomesticregId", index).ToInt();
            var vStateProvId = gvIllustration.GetKeyFromAspxGridView("StateProvId", index).ToInt();
            var vCityId = gvIllustration.GetKeyFromAspxGridView("CityId", index).ToInt();
            var vOfficeId = gvIllustration.GetKeyFromAspxGridView("OfficeId", index).ToInt();
            var vCaseSeqNo = gvIllustration.GetKeyFromAspxGridView("CaseSeqNo", index).ToInt();
            var vHistSeqNo = gvIllustration.GetKeyFromAspxGridView("HistSeqNo", index).ToInt();
            var vIllustrationStatusCode = gvIllustration.GetKeyFromAspxGridView("IllustrationStatusCode", index).ToString();
            var vProductSubTypeDesc = gvIllustration.GetKeyFromAspxGridView("ProductSubTypeDesc", index).ToString();
            var priority = gvIllustration.GetKeyFromAspxGridView("Priority", index).ToBoolean();
            var HasFacultative = gvIllustration.GetKeyFromAspxGridView("HasFacultative", index, string.Empty).ToBoolean();
            ObjServices.HasFacultative = HasFacultative;
            var PolicyNoMain = gvIllustration.GetKeyFromAspxGridView("PolicyNoMain", index, string.Empty).ToString();
            ObjServices.PolicyNoMain = PolicyNoMain;
            ObjServices.QuotationAgentId = gvIllustration.GetKeyFromAspxGridView("AgentId", index).ToInt();
            ObjServices.StatusNameKey = vIllustrationStatusCode;
            ObjServices.CustomerName = gvIllustration.GetKeyFromAspxGridView("InsuredName", index).ToString();

            ObjServices.AlliedLinesProductBehavior = (planGroupCode == "A" ||
                                                      planGroupCode == "I" ||
                                                      planGroupCode == "L") ? (Utility.AlliedLinesType)Enum.Parse(typeof(Utility.AlliedLinesType), vProductSubTypeDesc)
                                                                            : Utility.AlliedLinesType.None;

            if (planGroupCode == Utility.EFamilyProductType.Auto.Code() ||
                 planGroupCode == Utility.EFamilyProductType.Health.Code() ||
                 planGroupCode == Utility.EFamilyProductType.IncendioLineasAliadas.Code() ||
                 planGroupCode == Utility.EFamilyProductType.LineasComerciales.Code()
                 )
            {
                ObjServices.Corp_Id = vCorpId;
                ObjServices.Region_Id = vRegionId;
                ObjServices.Country_Id = vCountryId;
                ObjServices.Domesticreg_Id = vDomesticregId;
                ObjServices.State_Prov_Id = vStateProvId;
                ObjServices.City_Id = vCityId;
                ObjServices.Office_Id = vOfficeId;
                ObjServices.Case_Seq_No = vCaseSeqNo;
                ObjServices.Hist_Seq_No = vHistSeqNo;
                ObjServices.ContactEntityID = gvIllustration.GetKeyFromAspxGridView("InsuredId", index).ToInt();

                switch (planGroupCode)
                {
                    case "A": ObjServices.ProductLine = Utility.ProductLine.Auto; break;
                    case "H":
                        ObjServices.ProductLine = Utility.ProductLine.HealthInsurance;
                        //Crear el boton
                        LinkButton bntDrop = new LinkButton();
                        bntDrop.Attributes["path"] = ConfigurationManager.AppSettings["pathHealth"];
                        bntDrop.Attributes["appname"] = ConfigurationManager.AppSettings["appnameHealth"];

                        /*Enviar Poliza Como parametro*/
                        bntDrop.Attributes.Add("Action", ObjServices.Policy_Id);

                        var addInfo = new Statetrust.Framework.Security.Bll.Item.AdditionalInfo
                        {
                            CompanyId = ObjServices.CompanyId,
                            Language = (ObjServices.Language == Utility.Language.en ? "en" : "es")
                        };

                        var data = SecurityPage.GenerateToken(ObjServices.UserID.Value, bntDrop, addInfo);

                        if (data.Status)
                            Response.Redirect(data.UrlPath, true);
                        else
                        {
                            this.MessageBox(data.errormessage);
                            return;
                        }
                        break;
                    case "I":
                    case "L":
                        ObjServices.ProductLine = Utility.ProductLine.AlliedLines;
                        ObjServices.AlliedLinesProductBehavior = (Utility.AlliedLinesType)Enum.Parse(typeof(Utility.AlliedLinesType), vProductSubTypeDesc);
                        break;
                }

                ObjServices.AssignedSubscriberId = gvIllustration.GetKeyFromAspxGridView("AssignedSubscriberId", index).ToInt();
                ObjServices.hdnQuotationTabs = hdnTabSelected.Value;
                ObjServices.hdnTabGroup = hdnTabGroup.Value;
                Session["RequiredTab"] = true;
                Session["redirected"] = true;
                Response.Redirect("IllustrationsVehicle.aspx");
            }
            else
            {
                ObjIllustrationServices.CustomerPlanNo = gvIllustration.GetKeyFromAspxGridView("CustomerPlanNo", index).ToLong();
                ObjServices.TabRedirect = "lnkIllustrations";
                Response.Redirect("Contact.aspx");
            }
        }

        private void InspectionForm(int index)
        {
            ObjServices.Policy_Id = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", index).ToString();
            var planGroupCode = gvIllustration.GetKeyFromAspxGridView("PlanGroupCode", index).ToString();
            var vCorpId = gvIllustration.GetKeyFromAspxGridView("CorpId", index).ToInt();
            var vRegionId = gvIllustration.GetKeyFromAspxGridView("RegionId", index).ToInt();
            var vCountryId = gvIllustration.GetKeyFromAspxGridView("CountryId", index).ToInt();
            var vDomesticregId = gvIllustration.GetKeyFromAspxGridView("DomesticregId", index).ToInt();
            var vStateProvId = gvIllustration.GetKeyFromAspxGridView("StateProvId", index).ToInt();
            var vCityId = gvIllustration.GetKeyFromAspxGridView("CityId", index).ToInt();
            var vOfficeId = gvIllustration.GetKeyFromAspxGridView("OfficeId", index).ToInt();
            var vCaseSeqNo = gvIllustration.GetKeyFromAspxGridView("CaseSeqNo", index).ToInt();
            var vHistSeqNo = gvIllustration.GetKeyFromAspxGridView("HistSeqNo", index).ToInt();
            var vIllustrationStatusCode = gvIllustration.GetKeyFromAspxGridView("IllustrationStatusCode", index).ToString();
            var vProductSubTypeDesc = gvIllustration.GetKeyFromAspxGridView("ProductSubTypeDesc", index).ToString();
            var priority = gvIllustration.GetKeyFromAspxGridView("Priority", index).ToBoolean();
            var PolicyNoMain = gvIllustration.GetKeyFromAspxGridView("PolicyNoMain", index, string.Empty).ToString();
            var HasFacultative = gvIllustration.GetKeyFromAspxGridView("HasFacultative", index, string.Empty).ToBoolean();
            ObjServices.HasFacultative = HasFacultative;
            ObjServices.PolicyNoMain = PolicyNoMain;
            ObjServices.QuotationAgentId = gvIllustration.GetKeyFromAspxGridView("AgentId", index).ToInt();
            ObjServices.StatusNameKey = vIllustrationStatusCode;
            ObjServices.CustomerName = gvIllustration.GetKeyFromAspxGridView("InsuredName", index).ToString();

            ObjServices.AlliedLinesProductBehavior = (planGroupCode == "A" ||
                                                      planGroupCode == "I" ||
                                                      planGroupCode == "L") ? (Utility.AlliedLinesType)Enum.Parse(typeof(Utility.AlliedLinesType), vProductSubTypeDesc)
                                                                            : Utility.AlliedLinesType.None;
            #region Auto
            if (planGroupCode == Utility.EFamilyProductType.Auto.Code() ||
                planGroupCode == Utility.EFamilyProductType.Health.Code() ||
                planGroupCode == Utility.EFamilyProductType.IncendioLineasAliadas.Code() ||
                planGroupCode == Utility.EFamilyProductType.LineasComerciales.Code()
                )
            {
                ObjServices.Corp_Id = vCorpId;
                ObjServices.Region_Id = vRegionId;
                ObjServices.Country_Id = vCountryId;
                ObjServices.Domesticreg_Id = vDomesticregId;
                ObjServices.State_Prov_Id = vStateProvId;
                ObjServices.City_Id = vCityId;
                ObjServices.Office_Id = vOfficeId;
                ObjServices.Case_Seq_No = vCaseSeqNo;
                ObjServices.Hist_Seq_No = vHistSeqNo;
                ObjServices.ContactEntityID = gvIllustration.GetKeyFromAspxGridView("InsuredId", index).ToInt();

                switch (planGroupCode)
                {
                    case "A":
                        ObjServices.ProductLine = Utility.ProductLine.Auto;
                        break;

                    case "L":
                        ObjServices.ProductLine = Utility.ProductLine.AlliedLines;

                        break;
                }

                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                {
                    var ListVehicles = getVehiclesOfQuotation(vCorpId, vRegionId, vCountryId, vDomesticregId, vStateProvId, vCityId, vOfficeId, vCaseSeqNo, vHistSeqNo);

                    int notInspected = ListVehicles.Count(v => v.InspectionRequired.GetValueOrDefault());

                    Session["IllustrationStatusCode"] = vIllustrationStatusCode;

                    ObjServices.hdnQuotationTabs = hdnTabSelected.Value;
                    ObjServices.hdnTabGroup = hdnTabGroup.Value;

                    //Verificar si existe inspeccion previa
                    var vehiclesReview = ObjServices.oVehicleManager.GetVehicleReview(new Vehicle
                    {
                        CorpId = vCorpId,
                        RegionId = vRegionId,
                        CountryId = vCountryId,
                        DomesticRegId = vDomesticregId,
                        StateProvId = vStateProvId,
                        CityId = vCityId,
                        OfficeId = vOfficeId,
                        CaseSeqNo = vCaseSeqNo,
                        HistSeqNo = vHistSeqNo
                    }).Where(v => v.Inspection.GetValueOrDefault()).ToList();


                    if (ObjServices.IsInspectorQuotRole)
                    {
                        if (notInspected > 0)
                        {
                            Session["Initialize"] = true;
                            Response.Redirect("VehicleInspectionForm.aspx");
                        }
                        else
                        {
                            var inspeccionPrevia = vehiclesReview.Any(v => v.ReviewId > 0);
                            if (inspeccionPrevia)
                            {
                                Session["Initialize"] = true;
                                Response.Redirect("VehicleInspectionForm.aspx");
                            }
                            else
                            {
                                this.MessageBox(Resources.VehicleDoesNotRequireInspection, Width: 500, Title: Resources.InformationLabel);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (vehiclesReview.Count > 0)
                        {
                            Session["Initialize"] = true;
                            Response.Redirect("VehicleInspectionForm.aspx");
                        }
                        else
                        {
                            if (notInspected > 0)
                            {
                                Session["Initialize"] = true;
                                Response.Redirect("VehicleInspectionForm.aspx");
                            }
                            else
                            {
                                this.MessageBox(Resources.VehicleDoesNotRequireInspection, Width: 500, Title: Resources.InformationLabel);
                                return;
                            }
                        }
                    }
                }

                #region Lineas Aliadas
                else if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                {
                    Response.Redirect("AlliedLinesRiskInspectionForm.aspx");
                }
                #endregion
            }
            #endregion
        }

        private void VerCotizacion(int index)
        {
            ObjServices.Policy_Id = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", index).ToString();
            var planGroupCode = gvIllustration.GetKeyFromAspxGridView("PlanGroupCode", index).ToString();
            var vCorpId = gvIllustration.GetKeyFromAspxGridView("CorpId", index).ToInt();
            var vRegionId = gvIllustration.GetKeyFromAspxGridView("RegionId", index).ToInt();
            var vCountryId = gvIllustration.GetKeyFromAspxGridView("CountryId", index).ToInt();
            var vDomesticregId = gvIllustration.GetKeyFromAspxGridView("DomesticregId", index).ToInt();
            var vStateProvId = gvIllustration.GetKeyFromAspxGridView("StateProvId", index).ToInt();
            var vCityId = gvIllustration.GetKeyFromAspxGridView("CityId", index).ToInt();
            var vOfficeId = gvIllustration.GetKeyFromAspxGridView("OfficeId", index).ToInt();
            var vCaseSeqNo = gvIllustration.GetKeyFromAspxGridView("CaseSeqNo", index).ToInt();
            var vHistSeqNo = gvIllustration.GetKeyFromAspxGridView("HistSeqNo", index).ToInt();
            var vIllustrationStatusCode = gvIllustration.GetKeyFromAspxGridView("IllustrationStatusCode", index).ToString();
            var vProductSubTypeDesc = gvIllustration.GetKeyFromAspxGridView("ProductSubTypeDesc", index).ToString();
            var priority = gvIllustration.GetKeyFromAspxGridView("Priority", index).ToBoolean();
            var PolicyNoMain = gvIllustration.GetKeyFromAspxGridView("PolicyNoMain", index, string.Empty).ToString();
            var HasFacultative = gvIllustration.GetKeyFromAspxGridView("HasFacultative", index, string.Empty).ToBoolean();
            ObjServices.HasFacultative = HasFacultative;
            ObjServices.PolicyNoMain = PolicyNoMain;
            ObjServices.QuotationAgentId = gvIllustration.GetKeyFromAspxGridView("AgentId", index).ToInt();
            ObjServices.StatusNameKey = vIllustrationStatusCode;
            ObjServices.CustomerName = gvIllustration.GetKeyFromAspxGridView("InsuredName", index).ToString();

            ObjServices.AlliedLinesProductBehavior = (planGroupCode == "A" ||
                                                      planGroupCode == "I" ||
                                                      planGroupCode == "L") ? (Utility.AlliedLinesType)Enum.Parse(typeof(Utility.AlliedLinesType), vProductSubTypeDesc)
                                                                            : Utility.AlliedLinesType.None;

            if (planGroupCode == Utility.EFamilyProductType.Auto.Code() ||
                 planGroupCode == Utility.EFamilyProductType.Health.Code() ||
                 planGroupCode == Utility.EFamilyProductType.IncendioLineasAliadas.Code() ||
                 planGroupCode == Utility.EFamilyProductType.LineasComerciales.Code()
                 )
            {
                ObjServices.Corp_Id = vCorpId;
                ObjServices.Region_Id = vRegionId;
                ObjServices.Country_Id = vCountryId;
                ObjServices.Domesticreg_Id = vDomesticregId;
                ObjServices.State_Prov_Id = vStateProvId;
                ObjServices.City_Id = vCityId;
                ObjServices.Office_Id = vOfficeId;
                ObjServices.Case_Seq_No = vCaseSeqNo;
                ObjServices.Hist_Seq_No = vHistSeqNo;
                ObjServices.ContactEntityID = gvIllustration.GetKeyFromAspxGridView("InsuredId", index).ToInt();

                switch (planGroupCode)
                {
                    case "A": ObjServices.ProductLine = Utility.ProductLine.Auto; break;
                    case "H":
                        ObjServices.ProductLine = Utility.ProductLine.HealthInsurance;
                        //Crear el boton
                        LinkButton bntDrop = new LinkButton();
                        bntDrop.Attributes["path"] = ConfigurationManager.AppSettings["pathHealth"];
                        bntDrop.Attributes["appname"] = ConfigurationManager.AppSettings["appnameHealth"];

                        /*Enviar Poliza Como parametro*/
                        bntDrop.Attributes.Add("Action", ObjServices.Policy_Id);

                        var addInfo = new Statetrust.Framework.Security.Bll.Item.AdditionalInfo
                        {
                            CompanyId = ObjServices.CompanyId,
                            Language = (ObjServices.Language == Utility.Language.en ? "en" : "es")
                        };

                        var data = SecurityPage.GenerateToken(ObjServices.UserID.Value, bntDrop, addInfo);

                        if (data.Status)
                            Response.Redirect(data.UrlPath, true);
                        else
                        {
                            this.MessageBox(data.errormessage);
                            return;
                        }
                        break;
                    case "I":
                    case "L":
                        ObjServices.ProductLine = Utility.ProductLine.AlliedLines;
                        ObjServices.AlliedLinesProductBehavior = (Utility.AlliedLinesType)Enum.Parse(typeof(Utility.AlliedLinesType), vProductSubTypeDesc);
                        break;
                }

                ObjServices.AssignedSubscriberId = gvIllustration.GetKeyFromAspxGridView("AssignedSubscriberId", index).ToInt();
                ObjServices.hdnQuotationTabs = hdnTabSelected.Value;
                ObjServices.hdnTabGroup = hdnTabGroup.Value;
                Session["RequiredTab"] = false;
                Session["redirected"] = true;
                Response.Redirect("IllustrationsVehicle.aspx");
            }
            else
            {
                ObjIllustrationServices.CustomerPlanNo = gvIllustration.GetKeyFromAspxGridView("CustomerPlanNo", index).ToLong();
                ObjServices.TabRedirect = "lnkIllustrations";
                Response.Redirect("Contact.aspx");
            }
        }

        /// <summary>
        /// Seleccion de registro en el grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectedRow_Click(object sender, EventArgs e)
        {
            var index = hdnSelectedRowVisibleIndex.Value.ToInt();
            ObjServices.Policy_Id = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", index).ToString();
            var planGroupCode = gvIllustration.GetKeyFromAspxGridView("PlanGroupCode", index).ToString();
            var vCorpId = gvIllustration.GetKeyFromAspxGridView("CorpId", index).ToInt();
            var vRegionId = gvIllustration.GetKeyFromAspxGridView("RegionId", index).ToInt();
            var vCountryId = gvIllustration.GetKeyFromAspxGridView("CountryId", index).ToInt();
            var vDomesticregId = gvIllustration.GetKeyFromAspxGridView("DomesticregId", index).ToInt();
            var vStateProvId = gvIllustration.GetKeyFromAspxGridView("StateProvId", index).ToInt();
            var vCityId = gvIllustration.GetKeyFromAspxGridView("CityId", index).ToInt();
            var vOfficeId = gvIllustration.GetKeyFromAspxGridView("OfficeId", index).ToInt();
            var vCaseSeqNo = gvIllustration.GetKeyFromAspxGridView("CaseSeqNo", index).ToInt();
            var vHistSeqNo = gvIllustration.GetKeyFromAspxGridView("HistSeqNo", index).ToInt();
            var vIllustrationStatusCode = gvIllustration.GetKeyFromAspxGridView("IllustrationStatusCode", index).ToString();
            var vProductSubTypeDesc = gvIllustration.GetKeyFromAspxGridView("ProductSubTypeDesc", index).ToString();
            var priority = gvIllustration.GetKeyFromAspxGridView("Priority", index).ToBoolean();
            var PolicyNoMain = gvIllustration.GetKeyFromAspxGridView("PolicyNoMain", index, string.Empty).ToString();
            var HasFacultative = gvIllustration.GetKeyFromAspxGridView("HasFacultative", index, string.Empty).ToBoolean();
            ObjServices.HasFacultative = HasFacultative;

            ObjServices.PolicyNoMain = PolicyNoMain;
            ObjServices.QuotationAgentId = gvIllustration.GetKeyFromAspxGridView("AgentId", index).ToInt();
            ObjServices.StatusNameKey = vIllustrationStatusCode;
            ObjServices.CustomerName = gvIllustration.GetKeyFromAspxGridView("InsuredName", index).ToString();

            ObjServices.AlliedLinesProductBehavior = (planGroupCode == "A" ||
                                                      planGroupCode == "I" ||
                                                      planGroupCode == "L") ? (Utility.AlliedLinesType)Enum.Parse(typeof(Utility.AlliedLinesType), vProductSubTypeDesc)
                                                                            : Utility.AlliedLinesType.None;

            if (planGroupCode == Utility.EFamilyProductType.Auto.Code() ||
                planGroupCode == Utility.EFamilyProductType.Health.Code() ||
                planGroupCode == Utility.EFamilyProductType.IncendioLineasAliadas.Code() ||
                planGroupCode == Utility.EFamilyProductType.LineasComerciales.Code()
                )
            {
                ObjServices.Corp_Id = vCorpId;
                ObjServices.Region_Id = vRegionId;
                ObjServices.Country_Id = vCountryId;
                ObjServices.Domesticreg_Id = vDomesticregId;
                ObjServices.State_Prov_Id = vStateProvId;
                ObjServices.City_Id = vCityId;
                ObjServices.Office_Id = vOfficeId;
                ObjServices.Case_Seq_No = vCaseSeqNo;
                ObjServices.Hist_Seq_No = vHistSeqNo;
                ObjServices.ContactEntityID = gvIllustration.GetKeyFromAspxGridView("InsuredId", index).ToInt();

                switch (planGroupCode)
                {
                    case "A": ObjServices.ProductLine = Utility.ProductLine.Auto; break;
                    case "H":
                        ObjServices.ProductLine = Utility.ProductLine.HealthInsurance;
                        ObjServices.ProductLine = Utility.ProductLine.HealthInsurance;
                        //Crear el boton
                        LinkButton bntDrop = new LinkButton();
                        bntDrop.Attributes["path"] = ConfigurationManager.AppSettings["pathHealth"];
                        bntDrop.Attributes["appname"] = ConfigurationManager.AppSettings["appnameHealth"];

                        /*Enviar Poliza Como parametro*/
                        bntDrop.Attributes.Add("Action", ObjServices.Policy_Id);

                        var addInfo = new Statetrust.Framework.Security.Bll.Item.AdditionalInfo
                        {
                            CompanyId = ObjServices.CompanyId,
                            Language = (ObjServices.Language == Utility.Language.en ? "en" : "es")
                        };

                        var data = SecurityPage.GenerateToken(ObjServices.UserID.Value, bntDrop, addInfo);

                        if (data.Status)
                            Response.Redirect(data.UrlPath, true);
                        else
                        {
                            this.MessageBox(data.errormessage);
                            return;
                        }
                        break;
                    case "I":
                    case "L":
                        ObjServices.ProductLine = Utility.ProductLine.AlliedLines;
                        ObjServices.AlliedLinesProductBehavior = (Utility.AlliedLinesType)Enum.Parse(typeof(Utility.AlliedLinesType), vProductSubTypeDesc);
                        break;
                }

                ObjServices.AssignedSubscriberId = gvIllustration.GetKeyFromAspxGridView("AssignedSubscriberId", index).ToInt();
                ObjServices.hdnQuotationTabs = hdnTabSelected.Value;
                ObjServices.hdnTabGroup = hdnTabGroup.Value;
                Session["redirected"] = true;
                Response.Redirect("IllustrationsVehicle.aspx");
            }
            else
            {
                ObjIllustrationServices.CustomerPlanNo = gvIllustration.GetKeyFromAspxGridView("CustomerPlanNo", index).ToLong();
                ObjServices.TabRedirect = "lnkIllustrations";
                Response.Redirect("Contact.aspx");
            }
        }

        protected void btnUrgentIllustration_Click(object sender, EventArgs e)
        {
            for (int i = gvIllustration.VisibleStartIndex; i < gvIllustration.VisibleRowCount; i++)
            {
                var chk = gvIllustration.FindRowCellTemplateControl(i, null, "chkSelect") as CheckBox;

                if (chk != null && chk.Checked)
                {
                    var vCustomerPlanNo = gvIllustration.GetKeyFromAspxGridView("CustomerPlanNo", i).ToLong();
                    var vCorpId = gvIllustration.GetKeyFromAspxGridView("CorpId", i).ToInt();
                    var vRegionId = gvIllustration.GetKeyFromAspxGridView("RegionId", i).ToInt();
                    var vCountryId = gvIllustration.GetKeyFromAspxGridView("CountryId", i).ToInt();
                    var vDomesticregId = gvIllustration.GetKeyFromAspxGridView("DomesticregId", i).ToInt();
                    var vStateProvId = gvIllustration.GetKeyFromAspxGridView("StateProvId", i).ToInt();
                    var vCityId = gvIllustration.GetKeyFromAspxGridView("CityId", i).ToInt();
                    var vOfficeId = gvIllustration.GetKeyFromAspxGridView("OfficeId", i).ToInt();
                    var vCaseSeqNo = gvIllustration.GetKeyFromAspxGridView("CaseSeqNo", i).ToInt();
                    var vHistSeqNo = gvIllustration.GetKeyFromAspxGridView("HistSeqNo", i).ToInt();
                    var priority = gvIllustration.GetKeyFromAspxGridView("Priority", i).ToBoolean();

                    if (!priority)
                    {
                        var policy = ObjServices.oPolicyManager.GetPolicy(
                            vCorpId,
                            vRegionId,
                            vCountryId,
                            vDomesticregId,
                            vStateProvId,
                            vCityId,
                            vOfficeId,
                            vCaseSeqNo,
                            vHistSeqNo
                            );
                        policy.Priority = !priority;
                        ObjServices.oPolicyManager.UpdatePolicy(policy);
                    }
                }
            }

            Search();
            this.MessageBox(Resources.PriorityChangedSuccessfully, Title: Resources.Success);
        }

        protected void drpOffice_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dataOffice = new Utility.itemOfficce();

            if (drpOffice.SelectedValue != "-1")
            {
                var dOffice = Utility.deserializeJSON<Utility.itemOfficce>(drpOffice.SelectedValue);

                if (dOffice.CityId > 0)
                {
                    dataOffice = Utility.deserializeJSON<Utility.itemOfficce>(drpOffice.SelectedValue);
                    SelectOffice(dataOffice);
                }
            }
        }

        private void SelectOffice(Utility.itemOfficce dataOffice)
        {
            ObjServices.GettingAllDrops(ref drpAgent,
                                        Utility.DropDownType.AgentQuotation,
                                        "AgentName",
                                        "AgentId",
                                        corpId: dataOffice.CorpId,
                                        regionId: dataOffice.RegionId,
                                        countryId: dataOffice.CountryId,
                                        domesticregId: dataOffice.DomesticregId,
                                        stateProvId: dataOffice.StateProvId,
                                        cityId: dataOffice.CityId,
                                        officeId: dataOffice.OfficeId,
                                        agentId: ObjServices.Agent_Id,
                                        BlId: Utility.BusinessLine.Vehicle.ToInt(),
                                        GenerateItemSelect: true,
                                        GenerateItemSelectText: Resources.All);
        }

        protected void btnAssignIllustrations_Click(object sender, EventArgs e)
        {
            var policyNo = "";

            int subscriberIdOrAgentId = drpAssignIllustrationsSubscribers.SelectedValue.ToInt();

            try
            {
                if (!CanAssignIllustrations())
                    return;

                var vCorpId = -1;
                var vRegionId = -1;
                var vCountryId = -1;
                var vDomesticregId = -1;
                var vStateProvId = -1;
                var vCityId = -1;
                var vOfficeId = -1;
                var vCaseSeqNo = -1;
                var vHistSeqNo = -1;
                var roles = Session["Roles"] != null ? Session["Roles"].ToString() : string.Empty;

                policy = policyNo = ltSelectedPolicy.Text;

                var key = Utility.deserializeJSON<Entity.UnderWriting.Entities.Case>(PolicyKey.Value);

                if (key != null)
                {
                    vCorpId = key.CorpId;
                    vRegionId = key.RegionId;
                    vCountryId = key.CountryId;
                    vDomesticregId = key.DomesticregId;
                    vStateProvId = key.StateProvId;
                    vCityId = key.CityId;
                    vOfficeId = key.OfficeId;
                    vCaseSeqNo = key.CaseSeqNo;
                    vHistSeqNo = key.HistSeqNo;
                }

                if (subscriberIdOrAgentId > 0)
                {
                    var isAgent = (ObjServices.IsAgentQuotRole || ObjServices.IsAngetInspectorQuotRole);

                    if (!isAgent)
                    {
                        ObjServices.AssignIllustrationToSubscriber(vCorpId,
                                                                   vRegionId,
                                                                   vCountryId,
                                                                   vDomesticregId,
                                                                   vStateProvId,
                                                                   vCityId,
                                                                   vOfficeId,
                                                                   vCaseSeqNo,
                                                                   vHistSeqNo,
                                                                   subscriberIdOrAgentId,
                                                                   roles
                                                                  );

                        this.MessageBox(Resources.IllustrationAssignedSuccessfully, Title: Resources.Success);
                        //Actualizar la tabla temp
                        ObjServices.UpdateTempTable(policyNo, ObjServices.UserID.GetValueOrDefault());

                    }
                    else
                    {
                        ObjServices.oPolicyManager.ChangePolicyChain(new Policy.Parameter
                        {
                            CorpId = vCorpId,
                            RegionId = vRegionId,
                            CountryId = vCountryId,
                            DomesticregId = vDomesticregId,
                            StateProvId = vStateProvId,
                            CityId = vCityId,
                            OfficeId = vOfficeId,
                            CaseSeqNo = vCaseSeqNo,
                            HistSeqNo = vHistSeqNo,
                            AgentId = subscriberIdOrAgentId,
                            UserId = ObjServices.UserID.GetValueOrDefault()
                        });

                        this.MessageBox(Resources.IllustrationAssignedSuccessfully, Title: Resources.Success);
                        //Actualizar la tabla temp
                        ObjServices.UpdateTempTable(policyNo, ObjServices.UserID.GetValueOrDefault());
                    }
                }

                FillData();
            }
            catch (Exception ex)
            {
                var msg = ex.GetLastInnerException().Message;
            }
        }

        private bool CanAssignIllustrations()
        {
            try
            {
                for (int i = gvIllustration.VisibleStartIndex; i < gvIllustration.VisibleRowCount; i++)
                {
                    var chk = gvIllustration.FindRowCellTemplateControl(i, null, "chkSelect") as CheckBox;

                    if (chk != null && chk.Checked)
                    {
                        var illustrationStatusCode = gvIllustration.GetKeyFromAspxGridView("IllustrationStatusCode", i).ToString();
                        var policyNo = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", i).ToString();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.GetLastInnerException().Message;
                this.MessageBox(!msg.SIsNullOrEmpty() ? " - " + msg.RemoveInvalidCharacters().Replace('\'', '\"') : "", Title: "Error");
            }
            return false;
        }

        private void hideTabsByRol()
        {
            #region TabSettings
            if (ObjServices.TabsByRol.Count() > 0)
            {
                //Verificar los group tabs
                var isPreSuscripcion = ObjServices.TabsByRol.Any(x => x.TabGroupDesc == "lnkPreSuscripcion" && x.Visible);

                if (!isPreSuscripcion)
                    setTabGroup("lnkPreSuscripcion");

                var isSuscripcion = ObjServices.TabsByRol.Any(x => x.TabGroupDesc == "lnkSuscripcion" && x.Visible);
                if (!isSuscripcion)
                    setTabGroup("lnkSuscripcion");

                var isHistorico = ObjServices.TabsByRol.Any(x => x.TabGroupDesc == "lnkHistorico" && x.Visible);
                if (!isHistorico)
                    setTabGroup("lnkHistorico");

                foreach (var item in ObjServices.TabsByRol)
                {
                    var oLi = this.FindControl(item.TabName);
                    if (oLi != null)
                        oLi.Visible = item.Visible;
                }
            }
            else
            {
                setTabGroup("lnkPreSuscripcion");
                setTabGroup("lnkSuscripcion");
                setTabGroup("lnkHistorico");
            }
            #endregion

            lilnkStatistics.Visible = ObjServices.CanViewStatistics;
        }

        protected void gvIllustration_PreRender(object sender, EventArgs e)
        {
            var Grid = (sender as ASPxGridView);
            Grid.TranslateColumnsAspxGrid(ExcludeColumns);
        }

        protected void drpAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvIllustration.DataBind();
        }

        protected void btnDeclinedByClient_Click(object sender, EventArgs e)
        {
            string policyNo = "";

            for (int i = gvIllustration.VisibleStartIndex; i < gvIllustration.VisibleRowCount; i++)
            {
                var chk = gvIllustration.FindRowCellTemplateControl(i, null, "chkSelect") as CheckBox;

                if (chk != null && chk.Checked)
                {
                    policyNo = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", i).ToString();
                    break;
                }
            }

            try
            {
                ucPopupChangeStatusSaveNotes.Initialize();

                ucPopupChangeStatusSaveNotes.FillReasonDenied(Utility.EFamilyProductType.Auto.ToString(),
                                                              Utility.ReasonPredefinieds.DeniedIllustrationReason);

                this.ExcecuteJScript("setTimeout(\"ChangeIllustrationStatus(document.getElementById('btnDeclinedByClient'))\",10);");
                this.ExcecuteJScript("setTabQoutation(document.getElementById('btnDeclinedByClient'));");
            }
            catch (Exception ex)
            {
                var msg = ex.GetLastInnerException().Message;
                this.MessageBox(Resources.AErrorOccurredWithIllustrationNo.SFormat(policyNo) + (!msg.SIsNullOrEmpty() ? " - " + msg.RemoveInvalidCharacters().Replace('\'', '\"') : ""), Title: "Error");
            }
        }

        protected void gvIllustration_PageIndexChanged(object sender, EventArgs e)
        {
            FillData();
        }

        private void ContadorCotizaciones(DateTime? DateTo, DateTime? DateFrom, int? OfficeId)
        {
            var BlSelected = drpBusinessLine.SelectedValue.ToInt();
            int? blId = BlSelected > 0 ? BlSelected : (int?)null;
            bool filterAgent = ObjServices.IsSucripcionDirectorQuotRole || ObjServices.isUserCot || ObjServices.IsAgentServiceQuoRole;

            var result = ObjServices.oPolicyManager.GetAllCustomerPlanDetailCountQuo(new Policy.QuoGrid.Key
            {
                CorpId = ObjServices.Corp_Id,
                AgentId = filterAgent ? (int?)null : ObjServices.Agent_LoginId,
                CompanyId = ObjServices.CompanyId,
                DateTo = DateTo,
                DateFrom = DateFrom,
                OfficeId = OfficeId,
                BlId = blId,
                UserId = ObjServices.UserID,
                Bandeja = ObjServices.Bandeja,
                AgentChain = filterAgent ? null : ObjServices.AgentChain
            }).ToList();

            string zero = "0";

            var sIllustrationsToWork = result.FirstOrDefault(c => c.Tab == "IllustrationsToWork");
            IllustrationsToWorkCount.InnerText = sIllustrationsToWork != null ? Convert.ToString(sIllustrationsToWork.Count) : zero;

            var sCompleteIllustrations = result.FirstOrDefault(c => c.Tab == "CompleteIllustrations");
            CompleteIllustrationsCount.InnerText = sCompleteIllustrations != null ? Convert.ToString(sCompleteIllustrations.Count) : zero;

            var sDeclinedByClient = result.FirstOrDefault(c => c.Tab == "DeclinedByClient");
            DeclinedByClientCount.InnerText = sDeclinedByClient != null ? Convert.ToString(sDeclinedByClient.Count) : zero;

            var sSubscriptions = result.FirstOrDefault(c => c.Tab == "Subscriptions");
            SubscriptionsCount.InnerText = sSubscriptions != null ? Convert.ToString(sSubscriptions.Count) : zero;

            var sDiscounts = result.FirstOrDefault(c => c.Tab == "Discounts");
            DiscountsCount.InnerText = sDiscounts != null ? Convert.ToString(sDiscounts.Count) : zero;

            var sDeclinedBySubscription = result.FirstOrDefault(c => c.Tab == "DeclinedBySubscription");
            DeclinedBySubscriptionCount.InnerText = sDeclinedBySubscription != null ? Convert.ToString(sDeclinedBySubscription.Count) : zero;

            var sMissingInspections = result.FirstOrDefault(c => c.Tab == "MissingInspections");
            MissingInspectionsCount.InnerText = sMissingInspections != null ? Convert.ToString(sMissingInspections.Count) : zero;

            var sApprovedBySubscription = result.FirstOrDefault(c => c.Tab == "ApprovedBySubscription");
            ApprovedBySubscriptionCount.InnerText = sApprovedBySubscription != null ? Convert.ToString(sApprovedBySubscription.Count) : zero;

            var sHistorical = result.FirstOrDefault(c => c.Tab == "Historical");
            HistoricalIllustrationsCount.InnerText = sHistorical != null ? Convert.ToString(sHistorical.Count) : zero;

            var sIncompleteIllustration = result.FirstOrDefault(c => c.Tab == "IncompleteIllustration");
            IncompleteIllustrationCount.InnerText = sIncompleteIllustration != null ? Convert.ToString(sIncompleteIllustration.Count) : zero;

            var sExpired = result.FirstOrDefault(c => c.Tab == "Expired");
            ExpiredCount.InnerText = sExpired != null ? Convert.ToString(sExpired.Count) : zero;

            var sExpiring = result.FirstOrDefault(c => c.Tab == "Expiring");
            ExpiringCount.InnerText = sExpiring != null ? Convert.ToString(sExpiring.Count) : zero;

            var sApprovedByClient = result.FirstOrDefault(c => c.Tab == "ApprovedByClient");
            ApprovedByClientCount.InnerText = sApprovedByClient != null ? Convert.ToString(sApprovedByClient.Count) : zero;

            var sMissingDocuments = result.FirstOrDefault(c => c.Tab == "MissingDocuments");
            MissingDocumentsCount.InnerText = sMissingDocuments != null ? Convert.ToString(sMissingDocuments.Count) : zero;

            var sPuntoVentaTabCount = result.FirstOrDefault(c => c.Tab == "PointOfSale");
            PuntoVentaTabCount.InnerText = sPuntoVentaTabCount != null ? Convert.ToString(sPuntoVentaTabCount.Count) : zero;

            var sConfirmationCall = result.FirstOrDefault(c => c.Tab == "ConfirmationCall");
            ConfirmationCallCount.InnerText = sConfirmationCall != null ? Convert.ToString(sConfirmationCall.Count) : zero;

            var sFacultativo = result.FirstOrDefault(c => c.Tab == "Facultativo");
            FacultativeCount.InnerText = sFacultativo != null ? Convert.ToString(sFacultativo.Count) : zero;
        }

        protected void gvIllustration_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            var Grid = (sender as ASPxGridView);

            if (e.RowType == GridViewRowType.Data)
            {
                var urlImg = Grid.GetKeyFromAspxGridView("FinancialClearance", e.VisibleIndex, defaultValue: string.Empty).ToString();

                if (!string.IsNullOrEmpty(urlImg))
                {
                    var TipoRiesgo = (Utility.TipoRiesgo)Enum.Parse(typeof(Utility.TipoRiesgo), Grid.GetKeyFromAspxGridView("TipoRiesgoNameKey", e.VisibleIndex, defaultValue: string.Empty).ToString());
                    var RiesgoDesc = Utility.GetDescRiesgo(TipoRiesgo);

                    var imgRiesgo = Grid.FindRowCellTemplateControl(e.VisibleIndex, null, "imgRiesgo") as Image;

                    if (imgRiesgo != null)
                    {
                        imgRiesgo.ImageUrl = urlImg;
                        imgRiesgo.Attributes.Add("title", RiesgoDesc);
                    }
                }
            }
        }

        protected void btnOpenAssignIllustrations_Click(object sender, EventArgs e)
        {
            var vPolicyNumber = string.Empty;
            var CorpId = -1;
            var RegionId = -1;
            var CountryId = -1;
            var DomesticregId = -1;
            var StateProvId = -1;
            var CityId = -1;
            var OfficeId = -1;
            var CaseSeqNo = -1;
            var HistSeqNo = -1;
            var planGroupCode = string.Empty;

            int BusinessLineId = -1;

            for (int i = gvIllustration.VisibleStartIndex; i < gvIllustration.VisibleRowCount; i++)
            {
                var chk = gvIllustration.FindRowCellTemplateControl(i, null, "chkSelect") as CheckBox;

                if (chk != null && chk.Checked)
                {
                    vPolicyNumber = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", i).ToString();
                    CorpId = gvIllustration.GetKeyFromAspxGridView("CorpId", i).ToInt();
                    RegionId = gvIllustration.GetKeyFromAspxGridView("RegionId", i).ToInt();
                    CountryId = gvIllustration.GetKeyFromAspxGridView("CountryId", i).ToInt();
                    DomesticregId = gvIllustration.GetKeyFromAspxGridView("DomesticregId", i).ToInt();
                    StateProvId = gvIllustration.GetKeyFromAspxGridView("StateProvId", i).ToInt();
                    CityId = gvIllustration.GetKeyFromAspxGridView("CityId", i).ToInt();
                    OfficeId = gvIllustration.GetKeyFromAspxGridView("OfficeId", i).ToInt();
                    CaseSeqNo = gvIllustration.GetKeyFromAspxGridView("CaseSeqNo", i).ToInt();
                    HistSeqNo = gvIllustration.GetKeyFromAspxGridView("HistSeqNo", i).ToInt();
                    planGroupCode = gvIllustration.GetKeyFromAspxGridView("PlanGroupCode", i).ToString();

                    ltSelectedPolicy.Text = vPolicyNumber;

                    if (planGroupCode == Utility.EFamilyProductType.Auto.Code() ||
                        planGroupCode == Utility.EFamilyProductType.Health.Code() ||
                        planGroupCode == Utility.EFamilyProductType.IncendioLineasAliadas.Code() ||
                        planGroupCode == Utility.EFamilyProductType.LineasComerciales.Code())
                    {
                        switch (planGroupCode)
                        {
                            case "A": BusinessLineId = Utility.BusinessLine.Vehicle.ToInt(); break;
                            case "H": BusinessLineId = Utility.BusinessLine.Health.ToInt(); break;
                            case "I":
                            case "L": BusinessLineId = Utility.BusinessLine.IncendioLineasAliadas.ToInt(); break;
                        }
                    }
                    else
                        BusinessLineId = Utility.BusinessLine.Life.ToInt();

                    var policyKey = Utility.serializeToJSON(new { CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo });

                    PolicyKey.Value = policyKey;

                    hdnBusinessLineId.Value = BusinessLineId.ToString();
                }
            }

            #region Llenar dropdown drpAssignIllustrationsSubscribers en popup para Asignar Cotizaciones

            drpAssignIllustrationsSubscribers.Visible = ObjServices.IsSuscripcionManagerQuotRole ||
                                                        ObjServices.IsSucripcionDirectorQuotRole ||
                                                        ObjServices.IsSuscripcionQuotRole ||
                                                        ObjServices.IsAgentQuotRole ||
                                                        ObjServices.IsAngetInspectorQuotRole ||
                                                        ObjServices.isUserCot ||
                                                        ObjServices.IsConfirmationCallManagerCot;


            var isAgent = (ObjServices.IsAgentQuotRole || ObjServices.IsAngetInspectorQuotRole);

            divdrpAsignIllustrations.Attributes.Remove("Style");
            divdrpAsignIllustrations.Attributes.Add("Style", (isAgent) ? "display:none" : "display:block");

            var TbSelected = (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), ObjServices.hdnQuotationTabs);

            var Role = string.Empty;

            switch (TbSelected)
            {
                case Utility.Tabs.lnkSubscriptions:
                    Role = "SuscripcionCot";
                    Session["Roles"] = "Subscriber";//aGREGADO 
                    break;
                case Utility.Tabs.lnkMissingInspections:
                    Role = "InspectorCot";
                    Session["Roles"] = "Inspector";
                    break;
                case Utility.Tabs.lnkConfirmationCall:
                    Role = "ConfirmationCallCot";
                    Session["Roles"] = "ConfirmationCaller";
                    break;
                default:
                    break;
            }

            if (drpAssignIllustrationsSubscribers.Visible)
            {
                var data = new List<Entity.UnderWriting.Entities.DropDown>(0);

                if (ObjServices.IsSuscripcionManagerQuotRole ||
                    ObjServices.IsSucripcionDirectorQuotRole ||
                    ObjServices.IsSuscripcionQuotRole ||
                    (ObjServices.isUserCot && (TbSelected == Utility.Tabs.lnkMissingInspections || TabSelected == Utility.Tabs.lnkSubscriptions)))
                {
                    //Es un suscriptor
                    txtAgentsOrSubscriptor.Visible = false;

                    data = ObjServices.GettingDropData(Utility.DropDownType.AgentQuotation,
                                                       corpId: CorpId,
                                                       regionId: RegionId,
                                                       countryId: CountryId,
                                                       domesticregId: DomesticregId,
                                                       stateProvId: StateProvId,
                                                       cityId: CityId,
                                                       officeId: OfficeId,
                                                       caseSeqNo: CaseSeqNo,
                                                       histSeqNo: HistSeqNo,
                                                       BlId: hdnBusinessLineId.Value.ToInt(),
                                                       agentId: null,
                                                       NameKey: Role).ToList();

                    if (data != null)
                        data = data.Where(d => d.RoleDesc == Role).ToList();
                }
                else if (ObjServices.IsAgentQuotRole || ObjServices.IsAngetInspectorQuotRole)
                {
                    txtAgentsOrSubscriptor.Visible = true;
                    //Es un agente
                    data = ObjServices.GettingDropData(Utility.DropDownType.AgentCot,
                                                      corpId: CorpId,
                                                      regionId: RegionId,
                                                      countryId: CountryId,
                                                      domesticregId: DomesticregId,
                                                      stateProvId: StateProvId,
                                                      cityId: CityId,
                                                      officeId: OfficeId,
                                                      caseSeqNo: CaseSeqNo,
                                                      histSeqNo: HistSeqNo,
                                                      BlId: hdnBusinessLineId.Value.ToInt(),
                                                      agentId: ObjServices.Agent_LoginId,
                                                      NameKey: Role
                                                      ).ToList();
                }
                else if (ObjServices.IsConfirmationCallManagerCot)
                {
                    //Es un Confirmation call Manager
                    txtAgentsOrSubscriptor.Visible = false;

                    data = ObjServices.GettingDropData(Utility.DropDownType.AgentQuotation,
                                                       corpId: CorpId,
                                                       regionId: RegionId,
                                                       countryId: CountryId,
                                                       domesticregId: DomesticregId,
                                                       stateProvId: StateProvId,
                                                       cityId: CityId,
                                                       officeId: OfficeId,
                                                       caseSeqNo: CaseSeqNo,
                                                       histSeqNo: HistSeqNo,
                                                       BlId: hdnBusinessLineId.Value.ToInt(),
                                                       agentId: null,
                                                       NameKey: Role
                                                       ).ToList();

                    if (data != null)
                        data = data.Where(d => d.RoleDesc == Role).ToList();
                }
                else
                {
                    this.MessageBox(Resources.OnlyAgentChange);
                    return;
                }

                if (data != null)
                {
                    drpAssignIllustrationsSubscribers.DataSource = data;
                    drpAssignIllustrationsSubscribers.DataTextField = isAgent ? "RejectReasonDesc" : "AgentName";
                    drpAssignIllustrationsSubscribers.DataValueField = "AgentId";
                    drpAssignIllustrationsSubscribers.DataBind();
                    drpAssignIllustrationsSubscribers.Items.Insert(0, new ListItem() { Value = "-1", Text = Resources.Select });
                    drpAssignIllustrationsSubscribers.SelectedIndex = 0;
                }
            }
            #endregion

            hdnShowPopAssignIllustration.Value = "true";
            popAssignIllustrations.Show();
        }

        #region Statistics
        private void lnkStatistics_Click()
        {
            FillStatisticsDrops();

            ddlStatisticsReportType.SelectedIndex = 0;
            ddlEmisionesViewBy.SelectedIndex = Utility.StatisticsEmisionesViewBy.Count.ToInt();
            ddlPerformanceViewBy.SelectedIndex = Utility.StatisticsPerformanceViewBy.Suscriptor.ToInt();
            ddlStatisticsTimeDimension.SelectedIndex = 0;
            ddlStatisticsTimeDimension_SelectedIndexChanged(ddlStatisticsTimeDimension, null);
        }

        private void FillStatisticsDrops()
        {
            #region ReportType
            var StatisticsReportType = ObjServices.GettingDropData(Utility.DropDownType.StatisticsReportType);
            if (StatisticsReportType != null)
            {
                var dicReporType = new Dictionary<int, string> { };
                foreach (var reporType in StatisticsReportType)
                    dicReporType.Add(
                                        reporType.ElementId.GetValueOrDefault(),
                                        Resources.ResourceManager.GetString(string.Format("Statistics{0}", reporType.ElementDesc))
                                    );


                ddlStatisticsReportType.Items.Clear();
                ddlStatisticsReportType.DataSource = dicReporType;
                ddlStatisticsReportType.DataValueField = "Key";
                ddlStatisticsReportType.DataTextField = "Value";
                ddlStatisticsReportType.DataBind();
            }
            #endregion

            #region Emisiones ViewBy
            var dicEmisionesViewBy = new Dictionary<int, string>
            {
                {0, Resources.Count },
                {1, Resources.Premium },
                {2, Resources.InsuredAmount },
                {3, Resources.Rate },
                {4, Resources.VehicleCount }
            };

            ddlEmisionesViewBy.Items.Clear();
            ddlEmisionesViewBy.DataSource = dicEmisionesViewBy;
            ddlEmisionesViewBy.DataValueField = "Key";
            ddlEmisionesViewBy.DataTextField = "Value";
            ddlEmisionesViewBy.DataBind();
            #endregion

            #region Performance ViewBy
            var dicPerformanceViewBy = new Dictionary<int, string>
            {
                {0, Resources.StatisticsSubscriber },
                {1, "Inspector" }
            };

            ddlPerformanceViewBy.Items.Clear();
            ddlPerformanceViewBy.DataSource = dicPerformanceViewBy;
            ddlPerformanceViewBy.DataValueField = "Key";
            ddlPerformanceViewBy.DataTextField = "Value";
            ddlPerformanceViewBy.DataBind();
            #endregion

            #region TimeDimension
            var lstPeriods = new Dictionary<int, string>
            {
                { (int)Period.Periods.Last3Month, Resources.LastNMonth.SFormat(3) },
                { (int)Period.Periods.Last6Month, Resources.LastNMonth.SFormat(6) }
            };

            foreach (var period in Utility.GetStatisticsPeriodDateList())
                lstPeriods.Add(period.Key, period.Value);

            ddlStatisticsTimeDimension.Items.Clear();
            ddlStatisticsTimeDimension.DataSource = lstPeriods;
            ddlStatisticsTimeDimension.DataValueField = "Key";
            ddlStatisticsTimeDimension.DataTextField = "Value";
            ddlStatisticsTimeDimension.DataBind();
            #endregion

            #region Years
            ddlYears.Items.Clear();
            ddlYears.DataSource = Utility.GetListYears();
            ddlYears.DataValueField = "Key";
            ddlYears.DataTextField = "Value";
            ddlYears.DataBind();
            #endregion


        }

        protected void ddlStatisticsReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlEmisionesViewBy.Visible =
            pnlPerformanceViewBy.Visible = false;

            var ddlStatisticType = ddlStatisticsReportType.SelectedValue.ToInt();

            if (ddlStatisticType == Utility.StatisticsReportType.StatisticsReportEmission.ToInt())
            {
                UCEmisiones.ReportType = 1;
                pnlEmisionesViewBy.Visible = true;
                ddlEmisionesViewBy.SelectedIndex = Utility.StatisticsEmisionesViewBy.Count.ToInt();
                EmisionesSearch();
                mvStatistics.SetActiveView(vwEmisiones);
            }
            else if (ddlStatisticType == Utility.StatisticsReportType.StatisticsReportInclusions.ToInt())
            {
                UCEmisiones.ReportType = 2;
                pnlEmisionesViewBy.Visible = true;
                ddlEmisionesViewBy.SelectedIndex = Utility.StatisticsEmisionesViewBy.Count.ToInt();
                EmisionesSearch();
                mvStatistics.SetActiveView(vwEmisiones);
            }
            else if (ddlStatisticType == Utility.StatisticsReportType.StatisticsReportExclusions.ToInt())
            {
                UCEmisiones.ReportType = 3;
                pnlEmisionesViewBy.Visible = true;
                ddlEmisionesViewBy.SelectedIndex = Utility.StatisticsEmisionesViewBy.Count.ToInt();
                EmisionesSearch();
                mvStatistics.SetActiveView(vwEmisiones);
            }
            else if (ddlStatisticsReportType.SelectedValue.ToInt() == Utility.StatisticsReportType.StatisticsReportPerformance.ToInt())
            {
                pnlPerformanceViewBy.Visible = true;
                ddlPerformanceViewBy.SelectedIndex = Utility.StatisticsPerformanceViewBy.Suscriptor.ToInt();
                PerformanceSearch();
                mvStatistics.SetActiveView(vwPerformance);
            }
        }

        protected void ddlEmisionesViewBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlStatisticType = ddlStatisticsReportType.SelectedValue.ToInt();

            if (ddlStatisticType == Utility.StatisticsReportType.StatisticsReportEmission.ToInt())
            {
                UCEmisiones.ReportType = 1;
            }
            else if (ddlStatisticType == Utility.StatisticsReportType.StatisticsReportInclusions.ToInt())
            {
                UCEmisiones.ReportType = 2;
            }
            else if (ddlStatisticType == Utility.StatisticsReportType.StatisticsReportExclusions.ToInt())
            {
                UCEmisiones.ReportType = 3;
            }

            EmisionesSearch();
        }

        protected void ddlPerformanceViewBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformanceSearch();
        }

        protected void ddlStatisticsTimeDimension_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangePeriodData();
            var ddlStatisticType = ddlStatisticsReportType.SelectedValue.ToInt();

            if (ddlStatisticType == Utility.StatisticsReportType.StatisticsReportEmission.ToInt())
            {
                UCEmisiones.ReportType = 1;
            }
            else if (ddlStatisticType == Utility.StatisticsReportType.StatisticsReportInclusions.ToInt())
            {
                UCEmisiones.ReportType = 2;
            }
            else if (ddlStatisticType == Utility.StatisticsReportType.StatisticsReportExclusions.ToInt())
            {
                UCEmisiones.ReportType = 3;
            }

            if (pnlEmisionesViewBy.Visible)
                EmisionesSearch();
            else
                PerformanceSearch();
        }

        protected void btnStatisticsSearch_Click(object sender, EventArgs e)
        {
            var ddlStatisticType = ddlStatisticsReportType.SelectedValue.ToInt();

            if (ddlStatisticType == Utility.StatisticsReportType.StatisticsReportEmission.ToInt())
            {
                UCEmisiones.ReportType = 1;
            }
            else if (ddlStatisticType == Utility.StatisticsReportType.StatisticsReportInclusions.ToInt())
            {
                UCEmisiones.ReportType = 2;
            }
            else if (ddlStatisticType == Utility.StatisticsReportType.StatisticsReportExclusions.ToInt())
            {
                UCEmisiones.ReportType = 3;
            }

            if (pnlEmisionesViewBy.Visible)
                EmisionesSearch();
            else
                PerformanceSearch();
        }

        protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pnlEmisionesViewBy.Visible)
                EmisionesSearch();
            else
                PerformanceSearch();
        }

        protected void ddlPeriodData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pnlEmisionesViewBy.Visible)
                EmisionesSearch();
            else
                PerformanceSearch();
        }

        private void ConfigurePeriodData(string periodData, Dictionary<int, string> data, string selectValue)
        {
            lblPeriodData.Text = periodData;
            ddlPeriodData.Items.Clear();
            ddlPeriodData.DataSource = data;
            ddlPeriodData.DataValueField = "Key";
            ddlPeriodData.DataTextField = "Value";
            ddlPeriodData.DataBind();
            ddlPeriodData.SelectedValue = selectValue;
            pnlPeriodData.Visible = true;
        }

        private void ChangePeriodData()
        {
            var currentPeriod = (Period.Periods)int.Parse(ddlStatisticsTimeDimension.SelectedValue);
            if (currentPeriod == Period.Periods.SeasonalMonth)
                ConfigurePeriodData(Resources.Month, Utility.GetListMonths(), DateTime.Now.Month.ToString());
            else if (currentPeriod == Period.Periods.SeasonalQuarter)
                ConfigurePeriodData(Resources.Quarter, Utility.GetListQuarters(), DateTime.Now.StartQuarterDate().Month.ToString());
            else if (currentPeriod == Period.Periods.SeasonalSemestral)
                ConfigurePeriodData(Resources.Semester, Utility.GetListSemestres(), DateTime.Now.StartSemestreDate().Month.ToString());

            if (currentPeriod == Period.Periods.Yearly ||
                currentPeriod == Period.Periods.SeasonalMonth ||
                currentPeriod == Period.Periods.SeasonalQuarter ||
                currentPeriod == Period.Periods.SeasonalSemestral)
            {
                ddlYears.SelectedValue = currentPeriod != Period.Periods.Quarterly ? "3" : "2";
                pnlYears.Visible = true;

                if (currentPeriod == Period.Periods.Semestral || currentPeriod == Period.Periods.Quarterly || currentPeriod == Period.Periods.Yearly)
                {
                    pnlPeriodData.Visible = false;
                    ddlPeriodData.DataSource = null;
                    ddlPeriodData.DataBind();
                }
            }
            else
            {
                pnlYears.Visible =
                pnlPeriodData.Visible = false;

                ddlYears.DataSource =
                ddlPeriodData.DataSource = null;

                ddlYears.DataBind();
                ddlPeriodData.DataBind();
            }
        }

        private void EmisionesSearch()
        {
            UCEmisiones.Search(
                                (Utility.StatisticsEmisionesViewBy)ddlEmisionesViewBy.SelectedValue.ToInt(),
                                (Period.Periods)ddlStatisticsTimeDimension.SelectedValue.ToInt(),
                                ddlPeriodData.SelectedValue.ToInt(),
                                ddlYears.SelectedValue.ToInt()
                              );
        }

        private void PerformanceSearch()
        {
            UCPerformance.Search(
                                    (Utility.StatisticsPerformanceViewBy)ddlPerformanceViewBy.SelectedValue.ToInt(),
                                    (Period.Periods)ddlStatisticsTimeDimension.SelectedValue.ToInt(),
                                    ddlPeriodData.SelectedValue.ToInt(),
                                    ddlYears.SelectedValue.ToInt()
                                );
        }
        #endregion

        protected void btnFlatTableRefresh_Click(object sender, EventArgs e)
        {
            var SelectedPoliciesArray = new List<string>();
            try
            {
                for (int i = gvIllustration.VisibleStartIndex; i < gvIllustration.VisibleRowCount; i++)
                {
                    var chk = gvIllustration.FindRowCellTemplateControl(i, null, "chkSelect") as CheckBox;

                    if (chk != null && chk.Checked)
                    {
                        var vPolicyNumber = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", i).ToString();
                        SelectedPoliciesArray.Add(vPolicyNumber);
                    }
                }

                if (SelectedPoliciesArray.Count > 0)
                {
                    var SelectedPolicies = string.Join("|", SelectedPoliciesArray.ToArray());
                    ObjServices.UpdateTempTable(SelectedPolicies, ObjServices.UserID.GetValueOrDefault());
                    FillData();
                    this.MessageBox("Proceso completado con exito.");
                }
            }
            catch (Exception ex)
            {
                var msg = ex.GetLastInnerException().Message;
                this.MessageBox(msg.RemoveInvalidCharacters().Replace('\'', '\"'), Title: "Error", Width: 800);
            }
        }

        protected void tBandejas_Tick(object sender, EventArgs e)
        {
            FillData();
        }

        protected void btnRefreshCounters_Click(object sender, EventArgs e)
        {
            DateTime? dateFrom = (DateTime?)ViewState["dateFrom"]; ;
            DateTime? dateTo = (DateTime?)ViewState["dateTo"];
            int? _officeId = (int?)ViewState["_officeId"];

            ContadorCotizaciones(dateTo, dateFrom, _officeId);
        }

        protected void SetActiveView(string lnk)
        {
            switch (lnk)
            {
                case "lnkPreSuscripcion":
                    mvGroupTabs.SetActiveView(vwPreSuscripcion);
                    break;
                case "lnkSuscripcion":
                    mvGroupTabs.SetActiveView(vwSuscripcion);
                    break;
                case "lnkHistorico":
                    mvGroupTabs.SetActiveView(vwHistorico);
                    break;
            }
        }

        private void setTabGroup(string TabId)
        {
            //Guarda Tab que esta deshabilitado 
            switch (TabId)
            {
                case "lnkPreSuscripcion":
                    hdnPreSuscripcion.Value = "false";
                    break;
                case "lnkSuscripcion":
                    hdnSuscripcion.Value = "false";
                    break;
                case "lnkHistorico":
                    hdnHistorico.Value = "false";
                    break;
            }

            EnableOrDisabledGroupTab();
        }

        protected void ManageGroupTabs(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            string TabAnterior = hdnTabGroup.Value;
            hdnTabGroup.Value = ObjServices.hdnTabGroup = lnk.ID;

            SetActiveView(lnk.ID);

            pnlIllustrationGridView.Visible = (lnk.ID != "lnkHistorico" || TabSelected != Utility.Tabs.lnkStatistics);
            pnlStatisticsGridView.Visible = !pnlIllustrationGridView.Visible;

            Translator(string.Empty);

            bool firstOption = true;

            if (Session["firstOption"] != null)
            {
                firstOption = Convert.ToBoolean(Session["firstOption"]);
                Session["firstOption"] = null;
            }

            var TabSel = ObjServices.InboxTabRedirect;

            if (!setFirstMenu(firstOption, TabSel))
            {
                var Index = hdnGroupTabIndex.Value.ToInt() + 1;
                hdnGroupTabIndex.Value = Index.ToString();

                hdnTabGroup.Value = ObjServices.hdnTabGroup = TabsGroupIds[Index];

                SetActiveView(ObjServices.hdnTabGroup);

                var SelectFirst = string.IsNullOrEmpty(ObjServices.hdnQuotationTabs);
                var result = setFirstMenu(SelectFirst);

                if (!result)
                {
                    setTabGroup(hdnTabGroup.Value);
                    var lnkAnterior = FindControl(TabAnterior);
                    ManageGroupTabs(lnkAnterior, null);
                }

                setTabGroup(TabAnterior);
            }

            SetCounterParameters();
        }

        private void SetCounterParameters()
        {
            bool filterAgent = ObjServices.IsSucripcionDirectorQuotRole || ObjServices.isUserCot || ObjServices.IsAgentServiceQuoRole;

            var objParams = new Utility.itemCounter
            {
                CorpId = ObjServices.Corp_Id,
                AgentId = ObjServices.Agent_LoginId,
                CompanyId = ObjServices.CompanyId,
                DateTo = null,
                DateFrom = null,
                OfficeId = null,
                BlId = null,
                Bandeja = ObjServices.Bandeja,
                UserId = ObjServices.UserID,
                FilterAgent = filterAgent
            };

            hdnParameters.Value = Utility.serializeToJSON(objParams);
        }

        void IUC.FillData()
        {
            FillData();
        }

        protected void gvPOSCotizaciones_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            var CallbackName = e.CallbackName;

            if (Utility.CallBackList.Contains(CallbackName))
            {
                gvPOSCotizaciones.FocusedRowIndex = -1;
                gvPOSCotizaciones.SetFilterSettings();
                FillData();
            }
        }

        protected void gvPOSCotizaciones_BeforeHeaderFilterFillItems(object sender, ASPxGridViewBeforeHeaderFilterFillItemsEventArgs e)
        {
            IsHeaderFilter = true;
            FillData();
        }

        protected void gvPOSCotizaciones_PreRender(object sender, EventArgs e)
        {
            var Grid = (sender as ASPxGridView);
            Grid.TranslateColumnsAspxGrid();
        }

        protected void gvPOSCotizaciones_PageIndexChanged(object sender, EventArgs e)
        {
            FillData();
        }
        protected void lnkAuto_Click(object sender, EventArgs e)
        {
            if (!ObjServices.UsuarioPropiedades.Any(o => o.Contains("AutoCot")))
            {
                this.ExcecuteJScript("setActiveClassTabsNewbusiness('" + hdnTabBandeja.Value + "');");
                this.MessageBox(Resources.WithoutInformationAccess);
                return;
            }

            ObjServices.Bandeja = "Auto";
            ColumnsTabConfigure = false;
            FillData();
            SetCounterParameters();
            this.ExcecuteJScript("setCounterQuotationInbox();");
        }

        protected void lnkLineaAleada_Click(object sender, EventArgs e)
        {
            if (!ObjServices.UsuarioPropiedades.Any(o => o.Contains("PropiedadCot")))
            {
                this.ExcecuteJScript("setActiveClassTabsNewbusiness('" + hdnTabBandeja.Value + "');");
                this.MessageBox(Resources.WithoutInformationAccess);
                return;
            }
            ColumnsTabConfigure = false;
            ObjServices.Bandeja = "Propiedad";
            FillData();
            SetCounterParameters();
            this.ExcecuteJScript("setCounterQuotationInbox();");
        }

        protected void lnkHealth_Click(object sender, EventArgs e)
        {
            string PvSaludPath = System.Configuration.ConfigurationManager.AppSettings["PvSaludBandeja"].ToString();
            string PvSaludApp_Name = System.Configuration.ConfigurationManager.AppSettings["appnameHealth"].ToString();

            var addInfo = new AdditionalInfo
            {
                CompanyId = ObjServices.CompanyId,
                Language = (ObjServices.Language == Utility.Language.en ? "en" : "es")
            };

            System.Web.UI.WebControls.LinkButton bntDrop = new System.Web.UI.WebControls.LinkButton();
            bntDrop.Attributes["path"] = PvSaludPath;
            bntDrop.Attributes["appname"] = PvSaludApp_Name;

            var data = SecurityPage.GenerateToken(ObjServices.UserID.Value, bntDrop, addInfo);

            if (data.Status)
            {
                Response.Redirect(data.UrlPath, true);
            }
            else
            {
                string msjerrr = data.errormessage;

                if (msjerrr == "This user does not have access to this page or App")
                {
                    msjerrr = Resources.UserNoAccess;
                }

                this.MessageBox(msjerrr);
                return;
            }
        }

        protected void lkLife_Click(object sender, EventArgs e)
        {
            string PvLifePath = System.Configuration.ConfigurationManager.AppSettings["PvLifeHistory"].ToString();
            string PvLifeApp_Name = System.Configuration.ConfigurationManager.AppSettings["PvLifeApp_Name"].ToString();

            var addInfo = new AdditionalInfo
            {
                CompanyId = ObjServices.CompanyId,
                Language = (ObjServices.Language == Utility.Language.en ? "en" : "es")
            };

            System.Web.UI.WebControls.LinkButton bntDrop = new System.Web.UI.WebControls.LinkButton();
            bntDrop.Attributes["path"] = PvLifePath;
            bntDrop.Attributes["appname"] = PvLifeApp_Name;

            var data = SecurityPage.GenerateToken(ObjServices.UserID.Value, bntDrop, addInfo);

            if (data.Status)
            {
                Response.Redirect(data.UrlPath, true);
            }
            else
            {
                string msjerrr = data.errormessage;
                if (msjerrr == "This user does not have access to this page or App")
                {
                    msjerrr = Resources.UserNoAccess;
                }

                this.MessageBox(msjerrr);
                return;
            }
        }

        protected void btnLocaleQuotFlat_Click(object sender, EventArgs e)
        {
            lblTab.Visible = false;
            lblTab.Text = "";
            txtQuotNumberFlat.Text = "";
            hdnShowpopLocateQuotFlat.Value = "true";
            popLocateQuotFlat.Show();
        }

        protected void btnLocateQFlat_Click(object sender, EventArgs e)
        {
            try
            {
                lblTab.Text = "";

                if (!string.IsNullOrEmpty(txtQuotNumberFlat.Text))
                {
                    var result = ObjServices.oPolicyManager.UpdateOneQuotationInfoTemp(new Policy.Quo.Temp()
                    {
                        PolicyNo = txtQuotNumberFlat.Text,
                        UserId = ObjServices.UserID.GetValueOrDefault()
                    });

                    string TabName = "";
                    lblTab.Visible = true;

                    if (result != null)
                    {
                        string realTab = result.Tab;

                        string[] sp = realTab.Split(new string[] { "And" }, StringSplitOptions.RemoveEmptyEntries);
                        if (sp.Count() > 1)
                        {
                            realTab = sp[0].Trim();
                        }

                        TabName = Utility.GetTabName("lnk" + realTab);
                        //lblTab.Text = string.Format(Resources.QuotationLocateLabel, TabName);
                    }
                    else
                    {
                        //lblTab.Text = Resources.QuotationLocateErrorLabel;
                    }
                }
            }
            catch (Exception ex)
            {
                lblTab.Visible = true;
                //lblTab.Text = Resources.errorSearchQuotFlat + "\n " + ex.Message;
            }
        }

        protected void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            string policyNo = "";

            try
            {
                var TabSelected = (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), ObjServices.hdnQuotationTabs);

                if (TabSelected == Utility.Tabs.lnkApprovedBySubscription)
                {
                    for (int i = gvIllustration.VisibleStartIndex; i < gvIllustration.VisibleRowCount; i++)
                    {
                        var chk = gvIllustration.FindRowCellTemplateControl(i, null, "chkSelect") as CheckBox;

                        if (chk != null && chk.Checked)
                        {
                            policyNo = gvIllustration.GetKeyFromAspxGridView("IllustrationNo", i).ToString();
                            break;
                        }
                    }
                }
                UCPrintingInvoice.SelectedPolicy = policyNo;
                UCPrintingInvoice.Initialize();
                ModalPrintingInvoice.Show();
                hdnShowpoppnPrintingInvoice.Value = "true";
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void pnlPrintInvoice_PreRender(object sender, EventArgs e)
        {
            (sender as Panel).Visible = ObjServices.CanPrintInvoice;
        }

        protected void btnReorderColumnsGrid_Click(object sender, EventArgs e)
        {
            var dataConfigTab = TabsColumns.Where(p => p.tabName == TabSelected);

            if (dataConfigTab.Any())
            {
                var dataColumns = dataConfigTab.FirstOrDefault().Columns.OrderBy(o => o.VisibleIndex);
                ReorderColumnsInGrid(gvIllustration, dataColumns);
            }
        }
    }
}