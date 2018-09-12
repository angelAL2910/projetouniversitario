﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Threading;
using System.Globalization;
using WEB.NewBusiness.Common.Illustration;
using System.Web.UI.WebControls;

namespace WEB.NewBusiness.Common
{
    public class BasePage : Statetrust.Framework.Web.WebParts.Pages.STFMainPage
    {
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

        public bool isChangingLang
        {
            get
            {
                var hdnLangChange = this.Master.FindControl("hdnLangChange") as System.Web.UI.WebControls.HiddenField;
                return hdnLangChange.Value == "true";
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
                {   //Este caso es unico y exclusivo del tab de requirements
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

        public void SetCompany(int companyId)
        {
            ObjServices.CompanyId = companyId;
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

                Session["SessionID"] = System.Guid.NewGuid().ToString();
                ViewState["ViewID"] = Session["SessionID"].ToString();
            }

            return result;

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            var hdnLang = this.Master.FindControl("STFCUserProfile1").FindControl("hdnLang") as System.Web.UI.WebControls.HiddenField;
            var hdnLangChange = this.Master.FindControl("hdnLangChange") as System.Web.UI.WebControls.HiddenField;

            var idioma = hdnLang.Value;

            if (ObjServices.Language.ToString() != idioma && IsPostBack)
            {
                ObjServices.Language = idioma == "en" ? Utility.Language.en : Utility.Language.es;
                ObjServices.isChangingLang = true;
                hdnLangChange.Value = "true";
            }
            else
            {
                ObjServices.isChangingLang = false;
                hdnLangChange.Value = "false";
                ObjServices.SetHiddenFieldLanguage(hdnLang);
            }

            Usuario.CurrentLanguageId = ObjServices.Language.ToInt();

            //Configuracion del idioma del sistema
            var culture = new System.Globalization.CultureInfo(idioma == "es" ? "es" : idioma);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            Usuario.CurrentLanguageKey = ObjServices.Language.ToString();

            var oScriptManager = this.Page.Master.FindControl("ScriptManager");
            if (oScriptManager != null)
            {
                var ApiKey = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "ApiKeyGoogleMaps").ConfigurationValue;
                var urlApiGoogleMaps = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "urlApiGoogleMaps").ConfigurationValue;
                var SManager = (oScriptManager as ScriptManager);
                var urlScripGoogleMaps = string.Format(urlApiGoogleMaps, ApiKey);
                SManager.Scripts.Add(new ScriptReference { Path = urlScripGoogleMaps });
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //Configuracion para todos los gridview de devExpress
            DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;            
        }

        public void setIsFuneral()
        {
            var hfProdutLine = this.Master.FindControl("hfProdutLine");
            var hdnisFuneral = this.Master.FindControl("hfisFuneral");

            if (!hdnisFuneral.isNullReferenceControl())
                (hdnisFuneral as HiddenField).Value = ObjServices.KeyNameProduct;

            if (!hfProdutLine.isNullReferenceControl())
                (hfProdutLine as HiddenField).Value = ObjServices.ProductLine.ToString();
        }
    }
}

