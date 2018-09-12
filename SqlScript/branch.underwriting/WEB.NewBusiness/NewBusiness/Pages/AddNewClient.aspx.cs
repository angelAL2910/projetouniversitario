﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using WEB.NewBusiness.Common;
using System.Web.UI.HtmlControls;
using RESOURCE.UnderWriting.NewBussiness;
 
using Statetrust.Framework.Security.Bll.Item;
using Statetrust.Framework.Security.Core;
using System.Configuration;

namespace WEB.NewBusiness.NewBusiness.Pages
{
    public partial class AddNewClient : BasePage
    {

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //Obtengo la configuracion de los tabs
            string attClass, tabEnd, tabStart, act;
            var TabConfig = ObjServices.getTabConfig();
            LinkButton[] controls;

            attClass = "class";
            act = "active";
            tabEnd = "tab_terminado";
            tabStart = "tab_encurso";

            controls = new LinkButton[]
            {
                lnkClientInfo,
                lnkOwnerInfo,
                lnkPlanPolicy,
                lnkBeneficiaries,
                lnkHealthDeclaration,
                lnkRequirements,
                lnkPayment
            };

            foreach (LinkButton item in controls)
            {
                var tabStatus = ((HtmlGenericControl)item.FindControl(item.Attributes["tabStatus"]));
                var TabId = item.Attributes["TabID"].ToInt();
                bool status = TabConfig.FirstOrDefault(x => x.TabId == TabId).IsValid;

                tabStatus.Attributes.Add(attClass, status ? tabEnd : tabStart);
                tabStatus.InnerHtml = status ? string.Empty : item.CommandArgument;

                ((HtmlGenericControl)item.Parent).Attributes.Add(attClass, status ? act : string.Empty);
            }

            if (ObjServices.IsReadyToReview)
            {
                liPayment.Visible = true;
            }
            else
            {
                liPayment.Visible = false;
            }

            //Traduccion
            Translator();
        }

        public void Translator()
        {
            var vCurrentTab = hdnCurrentTabAddNewClient.Value.Split('|')[0];
            policyInformation.Text = Resources.PolicyInformationLabel;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var ClientInfo = bool.Parse(ConfigurationManager.AppSettings["ClientInfo"]);
            var OwnerInfo = bool.Parse(ConfigurationManager.AppSettings["OwnerInfo"]);
            var PlanPolicy = bool.Parse(ConfigurationManager.AppSettings["PlanPolicy"]);
            var Beneficiaries = bool.Parse(ConfigurationManager.AppSettings["Beneficiaries"]);
            var Requirements = bool.Parse(ConfigurationManager.AppSettings["Requirements"]);
            var Payments = bool.Parse(ConfigurationManager.AppSettings["Payments"]);
            var HealthDeclaration = bool.Parse(ConfigurationManager.AppSettings["HealthDeclaration"]);

            //Deshabilitar tabs segun sea el caso
            if (!ClientInfo)
            {
                lnkClientInfo.Click -= ManageTabs;
                lnkClientInfo.Attributes.Add("alt", "Disabled");
            }

            if (!OwnerInfo)
            {
                lnkOwnerInfo.Click -= ManageTabs;
                lnkOwnerInfo.Attributes.Add("alt", "Disabled");
            }

            if (!PlanPolicy)
            {
                lnkPlanPolicy.Click -= ManageTabs;
                lnkPlanPolicy.Attributes.Add("alt", "Disabled");
            }

            if (!Beneficiaries)
            {
                lnkBeneficiaries.Click -= ManageTabs;
                lnkBeneficiaries.Attributes.Add("alt", "Disabled");
            }

            if (!Payments)
            {
                lnkPayment.Click -= ManageTabs;
                lnkPayment.Attributes.Add("alt", "Disabled");
            }

            if (!Requirements)
            {
                lnkRequirements.Click -= ManageTabs;
                lnkRequirements.Attributes.Add("alt", "Disabled");
            }

            if (!HealthDeclaration)
            {
                lnkHealthDeclaration.Click -= ManageTabs;
                lnkHealthDeclaration.Attributes.Add("alt", "Disabled");
            }

            //End deshabilitar tabs 

            var hdnCurrentMenuSelectedMenuLeft = this.Master.FindControl("hdnCurrentMenuSelectedMenuLeft");

            if (hdnCurrentMenuSelectedMenuLeft != null)
                ((HiddenField)hdnCurrentMenuSelectedMenuLeft).Value = "MenulnkClientInfo";

            if (!IsPostBack)
            {
                if (this.ObjServices.isNewCase)
                    this.ObjServices.CleanSessionCase();

                ClearData();

                hdnCurrentTabAddNewClient.Value = string.IsNullOrEmpty(this.ObjServices.TabRedirect) ?
                                                  hdnCurrentTabAddNewClient.Value : this.ObjServices.TabRedirect;

                LinkButton BotonTab = null;

                switch (hdnCurrentTabAddNewClient.Value)
                {
                    case "lnkClientInfo":
                        BotonTab = lnkClientInfo;
                        break;
                    case "lnkOwnerInfo":
                        BotonTab = lnkOwnerInfo;
                        break;
                    case "lnkPlanPolicy":
                        BotonTab = lnkPlanPolicy;
                        break;
                    case "lnkBeneficiaries":
                        BotonTab = lnkBeneficiaries;
                        break;
                    case "lnkRequirements":
                        BotonTab = lnkRequirements;
                        break;
                    case "lnkHealthDeclaration":
                        BotonTab = lnkHealthDeclaration;
                        break;
                    case "lnkPayment":
                        BotonTab = lnkPayment;
                        break;
                }

                ManageTabs(BotonTab, null);
            }

        }

        /// <summary>        
        /// Metodo para salvar los datos de un tab
        /// </summary>
        /// <param name="TabActual"></param>
        /// <returns></returns>
        public bool Save(String TabActual)
        {
            var result = true;

            switch (TabActual)
            {
                case "lnkClientInfo":
                case "lnkOwnerInfo":
                    ContactsInfoContainer.save();
                    WUCSearch.save();
                    break;
                case "lnkPlanPolicy":
                    result = PlanPolicyContainer.saveValidator();
                    break;
                case "lnkBeneficiaries":

                    result = BeneficiariesContainer.saveBeneficiaries();                   
                    break;
                case "lnkHealthDeclaration":
                    //Poner aqui el metodo save de HealthDeclaration
                    HealthDeclarationContainer.save();
                    result = Session["isValidQuestionaries"].ToBoolean();
                    if (!result)
                    {
                        var Title = Resources.Warning;
                        this.ExcecuteJScript("CustomDialogMessageEx(null, null, null, true,'" + Title + "', 'QuestionariesValidationMessage');");
                    }

                    break;
                case "lnkRequirements":
                    RequirementsContainer.save();
                    break;
                case "lnkPayment":
                    PaymentContainer.save();
                    break;
            }

            return result;
        }

        protected void ManageTabs(object sender, EventArgs e)
        {
            
            if (sender == null)
                sender = lnkClientInfo;

            setIsFuneral();
            var hdnIsReadOnly = this.Master.FindControl("hdnIsReadOnly");

            ((HiddenField)hdnIsReadOnly).Value = (!hdnIsReadOnly.isNullReferenceControl() && ObjServices.IsReadOnly) ? "true" : "false";

            var vButton = ((LinkButton)sender);
            var TabActual = hdnCurrentTabAddNewClient.Value.Split('|')[0];
            var Tab = vButton.ClientID;

            if (IsPostBack && Tab == TabActual && TabActual != "lnkPayment")
                return;

            if (!base.IsRefreshPage())
            {
                if (Tab != TabActual)
                {
                    if (!ObjServices.IsReadOnly)
                    {
                        var NoOrdenCurrent = -1;
                        var NoOrdenNext = vButton.CommandArgument.ToInt();
                        var PieExist = hdnCurrentTabAddNewClient.Value.IndexOf("|") != -1;

                        NoOrdenCurrent = (PieExist) ? hdnCurrentTabAddNewClient.Value.Split('|')[1].ToInt()
                                                    : NoOrdenNext;

                        if (NoOrdenNext >= NoOrdenCurrent)
                            if (!Save(TabActual))
                                return;
                    }
                }
            }

            //Ocultar todos los panels
            Utility.HideShowAllControls(pnContainer.Controls, false);

            hdnCurrentTabAddNewClient.Value = Tab + "|" + vButton.CommandArgument;

            udpAddNewClient.Update();

            ClearData();

            this.ObjServices.isOwnerContact = (Tab == "lnkOwnerInfo");

            var TabConfig = ObjServices.getTabConfig();

            switch (Tab)
            {
                case "lnkClientInfo":
                case "lnkOwnerInfo":
                    this.ObjServices.ContactEntityID = (Tab == "lnkClientInfo") ? this.ObjServices.Contact_Id :
                                                                                  this.ObjServices.Owner_Id;
                    pnContactsInfo.Visible = true;
                    WUCSearchContacts.Initialize();
                    ContactsInfoContainer.Initialize();
                    break;
                case "lnkPlanPolicy":
                    //Validar si tiene acceso al tab si el tab ClientInfo y OwnerInfo estan Listos   
                    var TabidOwnerInfoId = Utility.Tab.OwnerInfo.ToInt();
                    var TabidClientInfoId = Utility.Tab.ClientInfo.ToInt();

                    var TabValidOwnerInfo = TabConfig.FirstOrDefault(x => x.TabId == TabidOwnerInfoId);
                    var TabValidClientInfo = TabConfig.FirstOrDefault(x => x.TabId == TabidClientInfoId);

                    if (!TabValidOwnerInfo.IsValid || !TabValidClientInfo.IsValid)
                    {
                        var msj = string.Empty;

                        if (!TabValidOwnerInfo.IsValid && !TabValidClientInfo.IsValid)
                            msj = ObjServices.Language == Utility.Language.en ? "Client Info and Owner Info"
                                                                              : "Información del Cliente e Información del Propietario";
                        else
                            if (TabValidOwnerInfo.IsValid && !TabValidClientInfo.IsValid)
                                msj = ObjServices.Language == Utility.Language.en ? "Client Info"
                                                                                  : "Información del Cliente";
                            else
                                if (!TabValidOwnerInfo.IsValid && TabValidClientInfo.IsValid)
                                    msj = ObjServices.Language == Utility.Language.en ? "Owner Info"
                                                                                      : "Información del Propietario";

                        this.MessageBox(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.PlanPolicyTabDisabled, msj));
                        RedirectTab(TabActual, vButton);
                        return;
                    }

                    this.ObjServices.ContactEntityID = -1;

                    this.ObjServices.ContactEntityID = (this.ObjServices.DesignatedPensionerContactId > 0) ?
                                                        this.ObjServices.DesignatedPensionerContactId :
                                                        this.ObjServices.InsuredAddContactId;
                    pnPlanPolicy.Visible = true;
                    PlanPolicyContainer.Initialize();
                    WUCSearch.Initialize();
                    break;
                case "lnkBeneficiaries":
                    //Validar que el tab de plan policy este completo
                    var TabidPlanPolicyId = Utility.Tab.PlanPolicy.ToInt();

                    var TabValidPlanPolicy = TabConfig.FirstOrDefault(x => x.TabId == TabidPlanPolicyId);

                    if (!TabValidPlanPolicy.IsValid)
                    {
                        this.MessageBox(RESOURCE.UnderWriting.NewBussiness.Resources.BeneficiariesTabDisabled);
                        RedirectTab(TabActual, vButton);
                        return;
                    }

                    if ((ObjServices.ProductLine == Utility.ProductLine.HealthInsurance))
                    {
                        lnkBeneficiaries.Attributes.Remove("onclick");
                        lnkBeneficiaries.Attributes.Add("onclick", "return false");
                        HealthDeclarationContainer.Initialize();
                        RedirectTab("lnkHealthDeclaration", lnkHealthDeclaration);
                        return;
                    }

                    pnBeneficiaries.Visible = true;
                    BeneficiariesContainer.Initialize();
                    WUCSearch3.Initialize();
                    break;
                case "lnkHealthDeclaration":
                    pnHealthDeclaration.Visible = true;
                    HealthDeclarationContainer.Initialize();
                    break;
                case "lnkRequirements":
                    //Validar que el tab de plan policy este completo
                    var TabidPlanPolicyId2 = Utility.Tab.PlanPolicy.ToInt();
                    var TabidHealthDeclaration = Utility.Tab.HealthDeclaration.ToInt();

                    var TabValidPlanPolicy2 = TabConfig.FirstOrDefault(x => x.TabId == TabidPlanPolicyId2);
                    var TabValidHealthDeclaration = TabConfig.FirstOrDefault(x => x.TabId == TabidHealthDeclaration);

                    if (!TabValidPlanPolicy2.IsValid || !TabValidHealthDeclaration.IsValid)
                    {
                        var msj = string.Empty;

                        if (!TabValidPlanPolicy2.IsValid && !TabValidHealthDeclaration.IsValid)
                            msj = ObjServices.Language == Utility.Language.en ? "Plan Policy and HealthDeclaration"
                                                                              : "Plan / Póliza y la Declaración de Salud";
                        else
                            if (TabValidPlanPolicy2.IsValid && !TabValidHealthDeclaration.IsValid)
                                msj = ObjServices.Language == Utility.Language.en ? "HealthDeclaration"
                                                                                  : "Declaración de Salud";
                            else
                                if (!TabValidPlanPolicy2.IsValid && TabValidHealthDeclaration.IsValid)
                                    msj = ObjServices.Language == Utility.Language.en ? "Plan Policy"
                                                                                      : "Plan / Póliza";

                        this.MessageBox(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.RequirementsTabsDisabled, msj));

                        RedirectTab(TabActual, vButton);

                        return;
                    }

                    pnRequirements.Visible = true;
                    RequirementsContainer.Initialize();
                    WUCSearch4.Initialize();
                    break;
                case "lnkPayment":

                    if (!ObjServices.IsReadyToReview)
                    {
                        this.MessageBox(RESOURCE.UnderWriting.NewBussiness.Resources.PaymentsTabDisabled);
                        RedirectTab(TabActual, vButton);
                        return;
                    }

                    pnPayment.Visible = true;
                    WUCSearch5.Initialize();
                    PaymentContainer.Initialize();
                    break;
            }
        }

        private void RedirectTab(string TabActual, LinkButton vButton)
        {
            hdnCurrentTabAddNewClient.Value = TabActual + "|" + vButton.CommandArgument;

            switch (TabActual)
            {
                case "lnkClientInfo":
                    WUCSearchContacts.Initialize();
                    ContactsInfoContainer.Initialize();
                    pnContactsInfo.Visible = true;
                    break;
                case "lnkOwnerInfo":
                    WUCSearchContacts.Initialize();

                    ContactsInfoContainer.Initialize();

                    if (ObjServices.isCompanyOwner)
                    {
                        var bodyContent = this.Master.FindControl("bodyContent");
                        var oCompanyForm = bodyContent.FindControl("ContactsInfoContainer") as NewBusiness.UserControls.ContactsInfo.ContactsInfoContainer;
                        var oWUCSearch = bodyContent.FindControl("WUCSearchContacts");

                        if (!oCompanyForm.isNullReferenceControl())
                        {
                            oCompanyForm.ChangeView(1);

                            if (!oWUCSearch.isNullReferenceControl())
                                ((NewBusiness.UserControls.ContactsInfo.WUCSearch)oWUCSearch).CheckedIsCompany();
                        }
                    }

                    pnContactsInfo.Visible = true;
                    break;
                case "lnkPlanPolicy":
                    PlanPolicyContainer.Initialize();
                    WUCSearch.Initialize();
                    pnPlanPolicy.Visible = true;
                    break;
                case "lnkBeneficiaries":
                    BeneficiariesContainer.FillData();
                    WUCSearch3.Initialize();
                    pnBeneficiaries.Visible = true;
                    break;
                case "lnkHealthDeclaration":
                    pnHealthDeclaration.Visible = true;
                    break;
                case "lnkRequirements":
                    RequirementsContainer.Initialize();
                    WUCSearch4.Initialize();
                    pnRequirements.Visible = true;
                    break;
                case "lnkPayment":
                    WUCSearch5.Initialize();
                    PaymentContainer.Initialize();
                    pnPayment.Visible = true;
                    break;
            }
        }

        private void ClearData()
        {           
            ContactsInfoContainer.ClearData();
            WUCSearchContacts.ClearData();
            PlanPolicyContainer.ClearData();
            BeneficiariesContainer.ClearData();
        }

        protected void lnkAuto_Click(object sender, EventArgs e)
        {
            string PvAutoPath = ConfigurationManager.AppSettings["PvAutoPath"].ToString();
            string PvAutoApp_Name = ConfigurationManager.AppSettings["PvAutoApp_Name"].ToString();

            var addInfo = new AdditionalInfo
            {
                CompanyId = ObjServices.CompanyId,
                Language = (ObjServices.Language == Utility.Language.en ? "en" : "es")
            };

            LinkButton bntDrop = new LinkButton();
            bntDrop.Attributes["path"] = PvAutoPath;
            bntDrop.Attributes["appname"] = PvAutoApp_Name;

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

        protected void lnkHealth_Click(object sender, EventArgs e)
        {
            string PvSaludPath = ConfigurationManager.AppSettings["PvSaludPath"].ToString();
            string PvSaludApp_Name = ConfigurationManager.AppSettings["PvSaludApp_Name"].ToString();

            var addInfo = new AdditionalInfo
            {
                CompanyId = ObjServices.CompanyId,
                Language = (ObjServices.Language == Utility.Language.en ? "en" : "es")
            };

            LinkButton bntDrop = new LinkButton();
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

        protected void lnkLineaAleada_Click(object sender, EventArgs e)
        {
            string PVLineasAliadasApp_Name = ConfigurationManager.AppSettings["PVLineasAliadasApp_Name"].ToString();
           
            //PropiedaCotizacion.aspx?IDQuotationBusiness=0&GlobalRamo=3&Pais=129
            string urlpath = ConfigurationManager.AppSettings["PvLineasPath"].ToString();
            
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PvLineasGlobalRamoPath"].ToString()))
            {
                urlpath += "&" + ConfigurationManager.AppSettings["PvLineasGlobalRamoPath"].ToString();
            }

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PvLineasPaisPath"].ToString()))
            {
                urlpath += "&" + ConfigurationManager.AppSettings["PvLineasPaisPath"].ToString();
            } 

            var addInfo = new AdditionalInfo
            {
                CompanyId = ObjServices.CompanyId,
                Language = (ObjServices.Language == Utility.Language.en ? "en" : "es")
            };

            LinkButton bntDrop = new LinkButton();

            bntDrop.Attributes["path"] = urlpath;
            bntDrop.Attributes["appname"] = PVLineasAliadasApp_Name;

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

        protected void lnkVivienda_Click(object sender, EventArgs e)
        {
            string PVLineasAliadasApp_Name = ConfigurationManager.AppSettings["PVLineasAliadasApp_Name"].ToString();

            //PropiedaCotizacion.aspx?IDQuotationBusiness=0&GlobalRamo=2&Pais=129
            string urlpath = ConfigurationManager.AppSettings["PvViviendaPath"].ToString();

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PvViviendaGlobalRamoPath"].ToString()))
            {
                urlpath += "&" + ConfigurationManager.AppSettings["PvViviendaGlobalRamoPath"].ToString();
            }

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PvViviendaPaisPath"].ToString()))
            {
                urlpath += "&" + ConfigurationManager.AppSettings["PvViviendaPaisPath"].ToString();
            } 

            var addInfo = new AdditionalInfo
            {
                CompanyId = ObjServices.CompanyId,
                Language = (ObjServices.Language == Utility.Language.en ? "en" : "es")
            };

            LinkButton bntDrop = new LinkButton();

            bntDrop.Attributes["path"] = urlpath;
            bntDrop.Attributes["appname"] = PVLineasAliadasApp_Name;

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
            this.MessageBox(Resources.CurrentlyPointSaleLifeFunerary);
            return;
        }



    }
}