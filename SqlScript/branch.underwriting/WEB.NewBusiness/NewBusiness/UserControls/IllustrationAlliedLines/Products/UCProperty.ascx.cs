﻿using Entity.UnderWriting.Entities;
using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.IllustrationAlliedLines.Products
{
    public partial class UCProperty : UC, IUC
    {
        public delegate void ExportToPDFHandler(byte[] pdfFile, string FileName);
        public event ExportToPDFHandler ExportToPdf;

        public void ClearData() { }

        public void Translator(string Lang) { }
        public void ReadOnlyControls(bool isReadOnly) { }
        public void save() { }
        public void edit() { }

        private void HideOrShow(String Product)
        {
            var DataConfig = ObjServices.GettingDropData(
                                                        Utility.DropDownType.ProjectConfigurationValue,
                                                        corpId: ObjServices.Corp_Id,
                                                        pProjectId: int.Parse(System.Configuration.ConfigurationManager.AppSettings["ProjectIdNewBusiness"])
                                                        );

            if (DataConfig != null)
            {
                var DataFields = DataConfig.FirstOrDefault(h => h.Namekey == Product);
                if (DataFields != null)
                {
                    var Fields = DataFields.ConfigurationValue.Split(',');

                    foreach (DevExpress.Web.GridViewColumn item in gvPropertyDetail.Columns)
                        item.Visible = false;

                    foreach (var item in Fields)
                    {
                        var Column = gvPropertyDetail.Columns[item];

                        if (Column != null)
                        {
                            Column.Visible = true;
                        }
                    }
                }
            }
        }

        private int UniquePropertyId
        {
            get { return ViewState["UniquePropertyId"].ToInt(); }
            set { ViewState["UniquePropertyId"] = value; }
        }

        public void ExportPDF(byte[] pdfFile, string FileName)
        {
            ExportToPdf(pdfFile, FileName);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pdfViewerMyPreviewPDF.LicenseKey = System.Configuration.ConfigurationManager.AppSettings["PDFViewer"];
            UCEndosoCesionAlliedLines.ExportToPdf += ExportPDF;
            UCEndosoCesionAlliedLines.BindGrid += BindGrid;
            UCPropertyDetail.BindGrid += BindGrid;

            var showPropertyDetailPop = hdnPopPropertyDetail.Value == "true";
            var showPopCoverages = hdnPopCoverages.Value == "true";

            if (showPropertyDetailPop)
                mpePropertyDetail.Show();

            if (showPopCoverages)
                ModalPopupCoverage.Show();

            if (hdnEndosoPopup.Value == "true")
                ModalPopupEndoso.Show();
        }

        private void FillDataCoverage()
        {
            UCCoverages.Initialize();
        }

        protected void gvAlliedLinesDetail_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            var grid = sender as DevExpress.Web.ASPxGridView;
            var Command = e.CommandArgs.CommandName;
            UniquePropertyId = grid.GetKeyFromAspxGridView("UniquePropertyId", e.VisibleIndex).ToInt();
            var insuredAmount = grid.GetKeyFromAspxGridView("InsuredAmount", e.VisibleIndex).ToInt();

            var ServerMaptPathXML = Server.MapPath("~/NewBusiness/XML/");
            byte[] XMLByteArray;
            byte[] PDFByteArray;

            switch (Command)
            {
                case "BlackList":
                    if (!ObjServices.IsValidateBlackListCot)
                    {
                        this.MessageBox(Resources.BlackListUserValidateAlert);
                        return;
                    }

                    hdnPopBlackList.Value = "true";
                    WUCBlackListValidation.Initialize();
                    ModalPopupBlackList.Show();
                    break;
                case "EditProperty":
                    //Popup para ver la información detallada de la propiedad  
                    UCPropertyDetail.UniquePropertyId = UniquePropertyId;
                    UCPropertyDetail.Initialize();
                    hdnPopPropertyDetail.Value = "true";
                    mpePropertyDetail.Show();

                    this.ExcecuteJScript("addClassDoble();");
                    break;
                case "Coverage":
                    hdnPopCoverages.Value = "true";
                    UCCoverages.UniqueId = UniquePropertyId;
                    FillDataCoverage();
                    ModalPopupCoverage.Show();
                    break;
                case "Endoso":
                    #region Endoso
                    UCEndosoCesionAlliedLines.Initialize();
                    UCEndosoCesionAlliedLines.UniqueId = UniquePropertyId;
                    UCEndosoCesionAlliedLines.InsuredAmount = insuredAmount;
                    UCEndosoCesionAlliedLines.FillData();
                    hdnEndosoPopup.Value = "true";
                    ModalPopupEndoso.Show();
                    #endregion
                    break;
                case "EndosoAclaratorio":
                    //Mostrar el Endoso Aclaratorio generado via thunderhead
                    //Generar el Documento XML con la data de la cotizacion 
                    #region Endoso Aclaratorio
                    //ObjServices.GenerateXMLQuotationThuderheadPropertyEndoso()
                    //XMLByteArray = ObjServices.GenerateXMLQuotationToThunderhead(ObjServices.Corp_Id,
                    //                                                             ObjServices.Region_Id,
                    //                                                             ObjServices.Country_Id,
                    //                                                             ObjServices.Domesticreg_Id,
                    //                                                             ObjServices.State_Prov_Id,
                    //                                                             ObjServices.City_Id,
                    //                                                             ObjServices.Office_Id,
                    //                                                             ObjServices.Case_Seq_No,
                    //                                                             ObjServices.Hist_Seq_No,
                    //                                                             ServerMaptPathXML,
                    //                                                             vehicleUniqueID: vehicle.VehicleUniqueId,
                    //                                                             templateType: ThunderheadWrap.Service.TemplateType.EndosoAclaratorio
                    //                                                            );

                    //PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, ThunderheadWrap.Service.TemplateType.EndosoAclaratorio);
                    //pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                    //pdfViewerMyPreviewPDF.DataBind();
                    //hdnShowPDF.Value = "true";
                    //ModalPopupShowPDF.Show();
                    #endregion
                    break;
                case "Inspeccion":
                    //Mostrar la Inspeccion generado via thunderhead
                    //Generar el Documento XML con la data de la cotizacion
                    var policy = new Property.Key()
                    {
                        CorpId = ObjServices.Corp_Id,
                        RegionId = ObjServices.Region_Id,
                        CountryId = ObjServices.Country_Id,
                        DomesticregId = ObjServices.Domesticreg_Id,
                        StateProvId = ObjServices.State_Prov_Id,
                        CityId = ObjServices.City_Id,
                        OfficeId = ObjServices.Office_Id,
                        CaseSeqNo = ObjServices.Case_Seq_No,
                        HistSeqNo = ObjServices.Hist_Seq_No,
                    };

                    XMLByteArray = ObjServices.GenerateXMLInspeccionRiesgoToThuderhead(policy, ServerMaptPathXML, UniquePropertyId);

                    string msg = ASCIIEncoding.ASCII.GetString(XMLByteArray);

                    if (msg == Resources.QuotationDontHasContact || msg == Resources.ThisQuoteDoesNotHaveProperty || msg == Resources.PropertyDontHasInspection)
                        this.MessageBox(msg, null, null, true, Resources.InformationLabel);
                    else
                    {
                        var countryCode = ObjServices.Country == Utility.Country.RepublicaDominicana ? ThunderheadWrap.Service.ContactCountry.RepublicaDominicana
                                                                                                     : ThunderheadWrap.Service.ContactCountry.ElSalvador;

                        PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray,
                                                                     ThunderheadWrap.Service.TemplateType.InspeccionRiesgo,
                                                                     ThunderheadWrap.Service.BusinessLine.IncendioLineasAliadas,
                                                                     countryCode);

                        pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                        pdfViewerMyPreviewPDF.DataBind();
                        hdnShowPDF.Value = "true";
                        ModalPopupShowPDF.Show();
                    }
                    break;
                default:
                    break;
            }
        }

        public void FillData()
        {
            var dataProperty = ObjServices.GetDataProperty();
            gvPropertyDetail.DatabindAspxGridView(dataProperty);


            var colProductDesc = dataProperty.FirstOrDefault();
            if (colProductDesc != null)
            {
                string productDesc = "Grid_" + colProductDesc.ProductDesc.Replace(" ", "");
                HideOrShow(productDesc);
            }       

            var PolicyNoMainCol = gvPropertyDetail.getThisColumn("PolicyNumberMain");
            if (PolicyNoMainCol != null)
                PolicyNoMainCol.Visible = !string.IsNullOrEmpty(ObjServices.PolicyNoMain);

            var BlackListCol = gvPropertyDetail.getThisColumn("BlackList");
            if (BlackListCol != null)
                BlackListCol.Visible = ObjServices.BlackListHasProblem;
        }

        public void BindGrid() { FillData(); }

        public void Initialize()
        {
            ClearData();
            FillData();
        }

        protected void gvPropertyDetail_PreRender(object sender, EventArgs e)
        {
            var Grid = (sender as DevExpress.Web.ASPxGridView);

            //Traducir las columnas
            Grid.TranslateColumnsAspxGrid();

            //Mostrar columna 
            Grid.VisibleColumnsAspxGrid(ObjServices.Country == Utility.Country.RepublicaDominicana, new string[] 
            { 
                "EndorsementClarifying" 
            });
        }

    }
}