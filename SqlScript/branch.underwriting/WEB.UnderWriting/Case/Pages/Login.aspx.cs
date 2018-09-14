﻿using System;
using System.Configuration;
using System.Globalization;
using System.Web.Configuration;
using WEB.UnderWriting.Common;

namespace WEB.UnderWriting.Case.Pages
{
    public partial class Login : BasePage
    {
        protected override void Page_PreInit(object sender, EventArgs e)
        {
            isLoginPage = true;
            SecurityMenuBar = false;

            if (IsPostBack) return;
            if (ConfigurationManager.AppSettings["ApplySecurity"].ToString(CultureInfo.InvariantCulture) == "false")
            {
                var userId = 0;
                var result = Login("epimentel", "/jSNLSYVQL4=", ref userId);

                if (!result) return;
                SetSessionValues();
                Response.Redirect("Case.aspx", true);
            }
            else
            {
                if (Request.QueryString.Count > 0)
                {
                    var userId = 0;
                    var aplicationId = 0;
                    if (!Login(Request.QueryString[0], ref userId, ref aplicationId)) return;
                    var url = GetDefaultPageByUserID(userId, aplicationId);
                    if (url.Trim() == "") return;
                    SetSessionValues();
                    Response.Redirect(url, true);
                }
                else
                    Response.Redirect(WebConfigurationManager.AppSettings["SecurityLogin"], true);
            }
        }

        private void SetSessionValues()
        {
            Session["ID"] = Guid.NewGuid().ToString().Replace("-", "");
            Usuario.CurrentCompanyId = 2;
            Service.datos.ContactInfo = new SessionContact
            {
                Underwriter_Id = Usuario.UserID,
                PdfViewerKey = ConfigurationManager.AppSettings["PDFViewer"],
                CompanyId = Usuario.CurrentCompanyId,
                LanguageId = Usuario.CurrentLanguageId,
                UnderwriterEmail = Usuario.Email,
                #region Transunion
                TransunionDefaultPassword = ConfigurationManager.AppSettings["TransunionDefaultPassword"],
                TransunionUser = ConfigurationManager.AppSettings["TransunionUserName"],
                TransunionPass = ConfigurationManager.AppSettings["TransunionPass"],
                #endregion
                UserName = Usuario.UserLogin
        };

            //Service.datos.ContactInfo.Underwriter_Id = 54;
            //Service.datos.ContactInfo.UnderwriterEmail = "ideleon@statetrustlife.com";

            Service.datos.Save();
        }
    }
}