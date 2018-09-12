﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common.Illustration;

namespace WEB.NewBusiness.Common
{
    public class UC : System.Web.UI.UserControl
    {
        public string policy { get; set; }
        private Services _services;
        private IllustrationService _illustrationServices;
        private string ProjectSessionName;

        protected void ExportToTxt(byte[] txt)
        {
            const string name = "Error.txt";
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + name);
            Response.AddHeader("Content-Type", "application/text");
            Response.ContentType = "application/vnd.txt";
            Response.AddHeader("Content-Length", txt.Length.ToString(CultureInfo.InvariantCulture));
            Response.BinaryWrite(txt);
            Response.End();
        }

        public void DownloadErrorDescripcion(Exception ex)
        {
            var MessageEx = ex.Message.Replace('\'', '\"').MyRemoveInvalidCharacters();
            (this.Page as BasePage).ErrorDescription = ex.InnerException != null || !string.IsNullOrEmpty(ex.StackTrace) ? ex.InnerException != null ? ex.InnerException.ToString() : string.Empty + ex.StackTrace : string.Empty;
            var msg = string.Format("{0}  <br> <br> Presione Ok para descargar un archivo con el detalle del error", MessageEx);
            this.CustomDialogMessageWithCallBack(msg, "function(){$('#btnGenerateFileError').click();}", "Error", "", "");
        }

        public string ErrorDescription
        {
            get { return Session["ErrorDescription"].ToString(); }
            set { Session["ErrorDescription"] = value; }
        }

        protected string ServerMaptPath
        {
            get
            {
                return Server.MapPath("~/NewBusiness/XML/");
            }
        }

        public bool getisView
        {
            get
            {
                return (this.Page.Master.FindControl("bodyContent").FindControl("hdnIsView") as HiddenField).Value == "true";
            }
        }

        protected bool IsRefreshPage()
        {
            bool result = false;

            if (!IsPostBack)
            {
                ViewState["ViewID"] = Guid.NewGuid();
                Session["SessionID"] = ViewState["ViewID"];
            }
            else
            {
                if (ViewState["ViewID"] != null && Session["SessionID"] != null)
                    result = (ViewState["ViewID"].ToString() != Session["SessionID"].ToString());
                else
                    result = false;

                Session["SessionID"] = System.Guid.NewGuid().ToString();
                ViewState["ViewID"] = Session["SessionID"].ToString();
            }

            return result;

        }

        public bool isChangingLang
        {
            get
            {
                var hdnLangChange = this.Page.Master.FindControl("hdnLangChange") as System.Web.UI.WebControls.HiddenField;
                return hdnLangChange.Value == "true";
            }
        }

        public string currentTab
        {
            get
            {
                string result = string.Empty;

                bool isView = false;

                if (ObjServices.IsDataReviewMode)
                    isView = getisView;

                if (!ObjServices.IsDataReviewMode)
                    result = Utility.GetCurrentTabAddNewClient(this).Replace("lnk", "");
                if (ObjServices.IsDataReviewMode && isView)
                    result = Utility.getCurrentTabView(this).Replace("lnk", "");
                else if (ObjServices.IsDataReviewMode)
                    result = Utility.GetCurrentTabDataReviewCompareData(this).Replace("btn", "");
                return result;
            }
        }

        public Services ObjServices
        {
            get
            {
                Control hdnSessionName = null;

                try
                {
                    hdnSessionName = this.Page.Master.FindControl("hdnSessionName");
                }
                catch (Exception)
                {
                    //Este caso es unico y exclusivo del tab de requirements
                    ProjectSessionName = "NewBusiness";
                }

                if (!hdnSessionName.isNullReferenceControl())
                    ProjectSessionName = (hdnSessionName as System.Web.UI.WebControls.HiddenField).Value;

                return _services ?? new Services(ProjectSessionName);
            }
        }

        public IllustrationService ObjIllustrationServices
        {
            get
            {
                Control hdnSessionName = null;

                try
                {
                    hdnSessionName = this.Page.Master.FindControl("hdnSessionName");
                }
                catch (Exception)
                {
                    //Este caso es unico y exclusivo del tab de requirements
                    ProjectSessionName = "NewBusiness";
                }

                if (!hdnSessionName.isNullReferenceControl())
                    ProjectSessionName = (hdnSessionName as System.Web.UI.WebControls.HiddenField).Value;

                return _illustrationServices ?? new IllustrationService(ProjectSessionName);
            }
        }

        public void ViewStateModeControl(bool Mode)
        {
            this.EnableViewState = Mode;
            if (Mode)
                this.ViewStateMode = System.Web.UI.ViewStateMode.Enabled;
            else
                this.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
        }

        /// <summary>
        /// Author       : Lic. Carlos Ml. Lebron
        /// Created Date : 11-25-2014
        /// Limpiar todos los componetes del usercontrol
        /// </summary>
        protected void ClearControls(Control ExcludeControl)
        {
            Utility.ClearAll(this.Controls, ExcludeControl);
        }

        protected void CleanControls(Control ListControls)
        {
            Utility.ClearAll(ListControls.Controls);
        }

        /// <summary>
        /// Author       : Lic. Carlos Ml. Lebron
        /// Created Date : 11-25-2014
        /// Limpiar todos los componetes del usercontrol
        /// </summary>
        protected void ClearControls()
        {
            Utility.ClearAll(this.Controls);
        }

        /// <summary>
        /// Author       : Lic. Carlos Ml. Lebron
        /// Created Date : 12-01-2014
        /// </summary>
        /// <param name="readOnly"></param>
        protected void EnabledControls(ControlCollection controls, bool readOnly)
        {
            Utility.EnableControls(controls, readOnly);
        }

        private HiddenField getHiddendFieldObject(string value)
        {
            HiddenField hdn = null;

            var objhdnUploadFile = this.Parent.Page.Master.FindControl("hdnUploadFile");

            if (!objhdnUploadFile.isNullReferenceControl())
                hdn = objhdnUploadFile as HiddenField;

            return hdn;
        }

        public void ShowPopUploadFile(bool Show)
        {
            getHiddendFieldObject(Show ? "true" : "false");
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            var PdfViewerList = this.Controls.OfType<PdfViewer4AspNet.PdfViewer>();
            foreach (var item in PdfViewerList)
            {
                var ControlPDF = (item as PdfViewer4AspNet.PdfViewer);
                ControlPDF.LicenseKey = System.Configuration.ConfigurationManager.AppSettings["PDFViewer"];
            }                
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
        }
    }
}