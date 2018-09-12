﻿using Entity.UnderWriting.Entities;
using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.IllustrationAlliedLines.Products
{
    public partial class UCAirPlane : UC, IUC
    {
        public delegate void ExportToPDFHandler(byte[] pdfFile, string FileName);
        public event ExportToPDFHandler ExportToPdf;

        public void Translator(string Lang) { }
        public void ReadOnlyControls(bool isReadOnly) { }
        public void save() { }
        public void edit() { }

        public void ExportPDF(byte[] pdfFile, string FileName)
        {
            ExportToPdf(pdfFile, FileName);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            pdfViewerMyPreviewPDF.LicenseKey = System.Configuration.ConfigurationManager.AppSettings["PDFViewer"];
            UCEndosoCesionAlliedLines.ExportToPdf += ExportPDF;
            UCEndosoCesionAlliedLines.BindGrid += BindGrid;

            UCAirPlaneDetail.BindGrid += FillData;

            var showPopCoverages = hdnAirCoverages.Value == "true";
            if (showPopCoverages)
                ModalPopupCoverage.Show();

            var showAirPlaneDetailPop = hdnPopAirPlaneDetail.Value == "true";
            if (showAirPlaneDetailPop)
                mpeAirPlaneDetail.Show();

            if (hdnEndosoPopup.Value == "true")
                ModalPopupEndoso.Show();

            if (IsPostBack) return;
        }

        private int UniqueAirplaneId
        {
            get { return ViewState["UniqueAirplaneId"].ToInt(); }
            set { ViewState["UniqueAirplaneId"] = value; }
        }

        private void FillDataCoverage()
        {
            UCCoverages.Initialize();
        }

        protected void gvAlliedLinesDetail_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            var grid = sender as DevExpress.Web.ASPxGridView;
            var Command = e.CommandArgs.CommandName;
            UniqueAirplaneId = grid.GetKeyFromAspxGridView("UniqueAirplaneId", e.VisibleIndex).ToInt();
            var insuredAmount = grid.GetKeyFromAspxGridView("InsuredAmount", e.VisibleIndex).ToInt();

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
                case "EditAirPlane":
                    UCAirPlaneDetail.UniqueAirplaneId = UniqueAirplaneId;
                    UCAirPlaneDetail.Initialize();
                    hdnPopAirPlaneDetail.Value = "true";
                    mpeAirPlaneDetail.Show();

                    this.ExcecuteJScript("addClassDoble();");
                    break;
                case "Coverage":
                    hdnAirCoverages.Value = "true";
                    UCCoverages.UniqueId = UniqueAirplaneId;
                    FillDataCoverage();
                    ModalPopupCoverage.Show();
                    break;
                case "Endoso":
                    #region Endoso
                    UCEndosoCesionAlliedLines.Initialize();
                    UCEndosoCesionAlliedLines.UniqueId = UniqueAirplaneId;
                    UCEndosoCesionAlliedLines.InsuredAmount = insuredAmount;
                    UCEndosoCesionAlliedLines.FillData();
                    hdnEndosoPopup.Value = "true";
                    ModalPopupEndoso.Show();
                    #endregion
                    break;
                default:
                    break;
            }
        }

        public void FillData()
        {
            gvAirPlaneDetail.DatabindAspxGridView(ObjServices.GetDataAirPlane());
            var PolicyNoMainCol = gvAirPlaneDetail.getThisColumn("PolicyNumberMain");
            if (PolicyNoMainCol != null)
                PolicyNoMainCol.Visible = !string.IsNullOrEmpty(ObjServices.PolicyNoMain);

            var BlackListCol = gvAirPlaneDetail.getThisColumn("BlackList");
            if (BlackListCol != null)
                BlackListCol.Visible = ObjServices.BlackListHasProblem;
        }

        public void BindGrid() { FillData(); }

        public void Initialize()
        {
            ClearData();
            FillData();
        }

        public void ClearData()
        {

        }

        protected void gvAirPlaneDetail_PreRender(object sender, EventArgs e)
        {
            var Grid = (sender as DevExpress.Web.ASPxGridView);

            //Traducir las columnas
            Grid.TranslateColumnsAspxGrid();
        }
    }
}