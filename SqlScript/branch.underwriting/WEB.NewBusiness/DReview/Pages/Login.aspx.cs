﻿using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.DReview.Pages
{

    public partial class Login : BasePage
    {
        public void setValues(String URLRedirect)
        {
           
                //Configuracion Inicial        
                ObjServices.ContactServicesIsActive = (System.Configuration.ConfigurationManager.AppSettings["ContactServicesIsActive"] == "true");
                ObjServices.ProjectId = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ProjectIdDataReview"]);                            

                ObjServices.Agent_LoginId = Usuario.AgentId;
                ObjServices.Agent_Id = ObjServices.Agent_LoginId;
                ObjServices.UserID = Usuario.UserID;
           
                ObjServices.TabRedirect = string.Empty;
                ObjServices.CompanyId = Usuario.CurrentCompanyId;
                ObjServices.isNewCase = false;
                ObjServices.UserName = Usuario.UserLogin;
            
                ObjServices.Language = Usuario.CurrentLanguageKey.ToLower() == "en" ?
                                                                Utility.Language.en :
                                                                Utility.Language.es;
            
                ObjServices.IsDataReviewMode = true;
                ObjServices.IsReadOnly = false;
                ObjServices.isOwnerContact = false;
                ObjServices.CurrentProject = Utility.Project.DataReview;

            //Bmarroquin 20-04-2017 variable que se utilizara en la pantalla de rechazo de casos By Compliance, esto dado la limitante actual de no poder dar permisos por acciones en botones...
            ObjServices.UserFullName = Usuario.FullName;
            List<string> UsuarioPropiedades = Usuario.Propiedades;
            var lBol_esComplianceOK = UsuarioPropiedades.Any(o => o.Contains("ComplianceSupCot"));
            if (lBol_esComplianceOK)
            {                              
                Session["RolCompliance"] = "OkReenviar";
            }
            //Fin Bmarroquin 20-04-2017

            var jsonAdditionalInfo = Request.QueryString["additionalinfo"];

                if (!String.IsNullOrEmpty(jsonAdditionalInfo))
                {
                    var additionalInfo = GetAdditionalInfo(jsonAdditionalInfo);

                    ObjServices.Language = Usuario.CurrentLanguageKey == "en" ?
                                                            Utility.Language.en :
                                                            Utility.Language.es;

                    ObjServices.CompanyId = Usuario.CurrentCompanyId;

                    Response.Redirect(additionalInfo.RedirectUrl, true);

                }
                else
                    Response.Redirect(URLRedirect, true);                          
        }

        protected override void Page_PreInit(object sender, EventArgs e)
        {
            int UserID = 0;
            isLoginPage = true;
            SecurityMenuBar = false;

            
                if (!IsPostBack)
                {
                    if (ConfigurationManager.AppSettings["ApplySecurity"].ToString(CultureInfo.InvariantCulture) == "false")
                    {
                        var LoginTest = "Clebron";//FARIRENE,clebron,STORJESS;
                        if (Login(LoginTest, "z6NlfE9MMSCtGbEJhJKMxw==", ref UserID))
                            setValues(System.Configuration.ConfigurationManager.AppSettings["LoginRedirectDataReview"]);
                        else
                            this.MessageBox(string.Format(Resources.LoginFailed, LoginTest));
                    }
                    else
                    {
                        if (Request.QueryString.Count > 0)
                        {
                            int AplicationID = 0;
                            if (Login(Request.QueryString[0], ref UserID, ref  AplicationID))
                            {
                                var URL = GetDefaultPageByUserID(UserID, AplicationID);
                                if (URL.Trim() != "")
                                    setValues(URL);
                            }
                        }
                        else
                            Response.Redirect(WebConfigurationManager.AppSettings["SecurityLogin"]);
                    }
                }
            }           

        }
    }

