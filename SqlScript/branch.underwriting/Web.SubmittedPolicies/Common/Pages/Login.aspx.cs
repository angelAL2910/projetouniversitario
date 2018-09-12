﻿using System;
using System.Configuration;
using System.Globalization;
using System.Web.Configuration;
using Statetrust.Framework.Web.WebParts.Pages;

namespace Web.SubmittedPolicies.Common.Pages
{
    public partial class Login : STFMainPage
    {
        protected override void Page_PreInit(object sender, EventArgs e)
        {
            isLoginPage = true;
            SecurityMenuBar = false;

            if (IsPostBack) return;
            if (ConfigurationManager.AppSettings["ApplySecurity"].ToString(CultureInfo.InvariantCulture) == "false")
            {
                var userId = 0;
                var result = Login("epimentel", "outiiz/77lQ=", ref userId);

                if (!result) return;
                SetSessionValues();
                Response.Redirect("../../Life/Pages/Life.aspx", false);
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

                    /* Bmarroquin 30/01/2017 se pone a true el end Response dado que tronaba al dejarlo False, ademas el codigo del 
                       LoadComplete de la clase padre (STFMainPage) no es util aplicandolo a la pagina de Login, solo el metodo UserloginA
                       pero dicho metodo se invoca en la siguiente pagina que se solicita, la Life.aspx
                    */
                    Response.Redirect(url, true);
                }
                else
                    Response.Redirect(WebConfigurationManager.AppSettings["SecurityLogin"]);
            }
        }

        private void SetSessionValues()
        {
            Usuario.CurrentLanguageId = 2;
            Session["UserID"] = Usuario.UserID;
            Session["CurrentCompanyId"] = Usuario.CurrentCompanyId;
            Session["LanguageId"] = Usuario.CurrentLanguageId;
        }
    }
}