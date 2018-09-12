﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.DReview.UserControl.HistoricalCases
{
    public partial class HistoricalCasesContainer : UC, IUC
    {
        public delegate void setViewPrincipalHandler(int index, string TitleView);
        public event setViewPrincipalHandler setViewPrincipal;

        protected override void OnPreRender(EventArgs e)
        {
            Translator("");
        }

        public void Translator(string Lang) 
        {
            btnExport.Text = RESOURCE.UnderWriting.NewBussiness.Resources.Export.ToUpper();
        }

        public void save() { }
        public void edit() { }
        public void Initialize() {
            hdnCurrentTabHistoricalCases.Value = "lnkApprovedCases";
            FillData();
            BindExportEvent();
        }
        public void ClearData() { }
        public void ReadOnlyControls(bool isReadOnly) { }

        public void setView(int index, string TitleView)
        {
            setViewPrincipal(index, TitleView);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            WUCRejectedCases.setViewPrincipal += setView;
            WUCApprovedCases.setViewPrincipal += setView;
            WUCRejectedCompliance.setViewPrincipal += setView;//Bmarroquin 19-04-2017 se agrega por rechazados por cumplimiento
        }

        protected void ManageTabs(object sender, EventArgs e)
        {
            var Tab = ((LinkButton)sender).ID;


            switch (Tab)
            {
                case "lnkApprovedCases":
                    WUCApprovedCases.Initialize();
                    mtvHistoricalCases.SetActiveView(vApprovedCases);
                    break;
                case "lnkRejectedCases":
                    WUCRejectedCases.Initialize();
                    mtvHistoricalCases.SetActiveView(vRejectedCases);
                    break;
                //Bmarroquin 19-04-2017 se agrega case de rechazados por cumplimiento
                case "lnkRejectedByCompliance":
                    WUCRejectedCompliance.Initialize();
                    mtvHistoricalCases.SetActiveView(vRejectedCompliance);
                    break;
            }


            hdnCurrentTabHistoricalCases.Value = Tab;

            BindExportEvent();
        }

        public void BindExportEvent()
        {
            var grid = string.Empty;
            var ActualTab = hdnCurrentTabHistoricalCases.Value;
            View view = null;
            if (ActualTab == "lnkRejectedCases")
            {
                view = vRejectedCases;
                grid = "gvRejectedCases";
            }
            else
            {
                if (ActualTab == "lnkApprovedCases")
                {
                    grid = "gvApprovedCases";
                    view = vApprovedCases;
                }

                if (ActualTab == "lnkRejectedByCompliance")  //Bmarroquin 19-04-2017 se agrega if por logica de rechazados por cumplimiento
                {
                    grid = "gvRejectedByCompliance";
                    view = vRejectedCompliance;
                }
            }


            btnExport.OnClientClick = "return ConfirmPrintList('" + grid + "');";
        }

        public void FillData()
        {
            var ActualTab = hdnCurrentTabHistoricalCases.Value;
            View view = null;
            if (ActualTab == "lnkRejectedCases")
            {
                WUCRejectedCases.Initialize();
                view = vRejectedCases;
            }
            else
            {
                if (ActualTab == "lnkApprovedCases")
                {
                    WUCApprovedCases.Initialize();
                    view = vApprovedCases;
                }
                //Bmarroquin 19-04-2017 se agrega if por logica de rechazados por cumplimiento
                if (ActualTab == "lnkRejectedByCompliance")  
                {
                    WUCRejectedCompliance.Initialize();
                    view = vRejectedCompliance;
                }
            }
            mtvHistoricalCases.SetActiveView(view); 
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var view = mtvHistoricalCases.GetActiveView();

            if (view == vApprovedCases)
                WUCApprovedCases.ExportarAExcel();
            else
            {
                if (view == vRejectedCases)
                    WUCRejectedCases.ExportarAExcel();
                else  //Bmarroquin 19-04-2017 se agrega else de rechazados por cumplimiento
                    WUCRejectedCompliance.ExportarAExcel();
            }

        }
    }
}