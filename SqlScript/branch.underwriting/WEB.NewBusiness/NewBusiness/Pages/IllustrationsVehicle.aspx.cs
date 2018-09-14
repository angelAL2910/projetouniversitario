﻿using Entity.UnderWriting.Entities;
using RESOURCE.UnderWriting.NewBussiness;
using Statetrust.Framework.Security.Bll.Item;
using Statetrust.Framework.Security.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;
using WEB.NewBusiness.Common.Illustration.IllustrationVehicle.Models;
using WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle;

namespace WEB.NewBusiness.NewBusiness.Pages
{
    public partial class IllustrationsVehicle : BasePage
    {
        private List<LinkButton> vLinksButtons
        {
            get
            {
                return (List<LinkButton>)ViewState["vLinksButtons"];
            }
            set
            {
                ViewState["vLinksButtons"] = value;
            }
        }

        public string IllustrationStatusCode
        {
            get
            {
                return ucIllustrationInformation.IllustrationStatusCode;
            }
        }

        public string ActionIllustrationPayment
        {
            get
            {
                var actionIllustrationPayment = ViewState["ActionIllustrationPayment"];
                if (actionIllustrationPayment != null) return actionIllustrationPayment.ToString();

                var val = Session["ActionModel"];
                if (val == null) return null;
                var actionModel = val.ToString().ToObject<ActionsModel>();
                Session.Remove("ActionModel");
                if (actionModel.ActionType == ActionTypes.Inspection)
                {
                    ViewState["ActionIllustrationPayment"] = actionModel.ActionJson;
                    return actionModel.ActionJson;
                }
                return null;
            }
            set
            {
                ViewState["ActionIllustrationPayment"] = value;
            }
        }

        public string Sessions
        {
            get
            {
                var actionIllustrationPayment = ActionIllustrationPayment;
                if (String.IsNullOrEmpty(actionIllustrationPayment)) return string.Empty;

                return Newtonsoft.Json.JsonConvert.DeserializeObject<ActionIllustrationPayment>(actionIllustrationPayment).Sessions;
            }
        }

        public List<Policy.VehicleInsured> ListVehicles
        {
            get
            {
                return ucVehiclesInformation.ListVehicles;
            }
        }

        private List<Policy.VehicleInsured> _ListVehiclesPoPuP;

        public List<Policy.VehicleInsured> ListVehiclesPoPuP
        {
            get
            {
                _ListVehiclesPoPuP = ListVehicles;
                return _ListVehiclesPoPuP == null ? null : _ListVehiclesPoPuP;
            }
            set
            {
                _ListVehiclesPoPuP = value;
            }
        }

        private int? AssignedSubscriberId
        {
            get
            {
                var assignedSubscriberId = Session["AssignedSubscriberId"];
                return assignedSubscriberId == null ? null : (int?)Session["AssignedSubscriberId"].ToInt();
            }
        }

        private bool IsGerencialRole
        {
            get
            {
                return ((WEB.NewBusiness.NewBusiness.Pages.IllustrationsVehicle)Page).Usuario.Propiedades.Any(o => o.Contains("SubscriptionManagerAutoAdmin"));
            }
        }

        /// <summary>
        /// Verificar si los vehiculos aplican para reaseguro
        /// </summary>
        private void VerifyReaseguro()
        {
            var TablaResumen = string.Empty;
            var MessageList = new StringBuilder();

            try
            {
                TablaResumen = "<table class='tblresumen'><tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th><tr/> <tbody><tr><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><tr/></tbody><table/>";

                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                {

                    //Verificar si los vehiculos de esta cotizacion aplican para reaseguro
                    if (ObjServices.StatusNameKey == Utility.IllustrationStatus.Subscription.Code() && ObjServices.IsSuscripcionQuotRole)
                    {
                        //Data de los Vehiculos
                        var dataVehicles = ObjServices.oPolicyManager.GetVehicleInsured(new Policy.Parameter
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
                            UnderwriterId = ObjServices.Agent_LoginId,
                            LanguageId = ObjServices.Language.ToInt(),
                            UserId = ObjServices.UserID
                        });

                        foreach (var item in dataVehicles)
                        {
                            var DataCoverage = ObjServices.oPolicyManager.GetVehicleCoverage(new Policy.VehicleCoverageGet
                            {
                                CorpId = item.CorpId,
                                VehicleUniqueId = item.VehicleUniqueId
                            });

                            var SubRamo = DataCoverage.FirstOrDefault().SubRamo.GetValueOrDefault();
                            var data = ObjServices.oSFPolicyServiceClient.GetMaximoReaseguro(106, SubRamo);

                            if (!string.IsNullOrEmpty(data.JSONResult) && data.JSONResult.ToLower() != "null")
                            {
                                var OldValue = "{}";
                                var resultString = data.JSONResult.Replace(OldValue, "0");
                                var dataResaseguro = Utility.deserializeJSON<Utility.DataReaseguro>(resultString);

                                if (dataResaseguro.MaximoReaseguro.Trim().ToUpper() != "NA")
                                {
                                    var MontoMaximoReaseguro = decimal.Parse(dataResaseguro.MaximoReaseguro);
                                    var VehicleVal = Convert.ToDecimal(item.VehicleValue.GetValueOrDefault());

                                    if (VehicleVal > MontoMaximoReaseguro)
                                    {
                                        if (!item.AppliesToReinsurance.HasValue)
                                        {
                                            //Marcar este vehiculo como que aplica para reaseguro
                                            item.AppliesToReinsurance = true;
                                            ObjServices.oPolicyManager.SetVehicleInsured(item);
                                        }

                                        var notify = true;

                                        var dataDocReaseguro = ObjServices.GetDocumentMandatory(Utility.AgentRoleType.Subscritor.ToString()).FirstOrDefault(c => c.RequimentOnBaseNameKey == "SUS-Documento de autorización de reaseguro");

                                        if (dataDocReaseguro != null)
                                            notify = !dataDocReaseguro.DocumentId.HasValue;

                                        if (notify)
                                        {
                                            var msg = string.Format(Resources.ReaseguroMessage, string.Concat(Resources.Make + ": ", item.MakeDesc + " ", Resources.Model + ": ", item.ModelDesc + " ", Resources.Year + ": ", item.Year.GetValueOrDefault()) + " ");
                                            var TResumen = string.Format(TablaResumen, Resources.Make, Resources.Model, Resources.Year, Resources.Insured_Amount, Resources.MaximumReinsurance, item.MakeDesc, item.ModelDesc, item.Year, item.VehicleValue.ToFormatCurrency(), MontoMaximoReaseguro.ToFormatCurrency());
                                            MessageList.AppendLine(msg + "<br/><br/>");
                                            MessageList.AppendLine(TResumen + "<br/><br/>");
                                        }
                                    }
                                }
                            }
                        }

                        if (MessageList.Length > 0)
                            this.MessageBox(MessageList.ToString().MyRemoveInvalidCharacters(), Title: Resources.Warning, Width: 800);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.GetLastInnerException().Message;
                this.MessageBox(msg.RemoveInvalidCharacters().Replace('\'', '\"'), Title: "Error");
            }
        }

        private void hideButtonsOnExclusion()
        {
            btnInspection.Visible = false;
            lnkDataCredito.Visible = false;
            btnOnBaseKCO.Visible = true;
            btnOnBase.Visible = true;
            btnOpenApplyDiscount.Visible = false;
            btnOpenApplySurcharge.Visible = false;
            btnForeignStatusNotification.Visible = false;
            btnCondicionado.Visible = false;
            btnCondPart.Visible = false;

            btnSeeIllustration.Text = Resources.btnSeeIllustrationExclusion;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var TabName = Utility.GetTabName(ObjServices.hdnQuotationTabs);

            pnTabSelected.InnerText = string.Concat("Tab : ", TabName);
            pdfViewerMyPreviewPDF.LicenseKey = System.Configuration.ConfigurationManager.AppSettings["PDFViewer"];
            ucPopupChangeStatusSaveNotes.ChangeStatusSaveNotes += ChangeStatus;
            ucVehiclesInformation.ExportToPdf += ExportToPdf;
            UCAlliedLinesDetail.ExportToPdf += ExportToPdf;
            ucRequiredInformation.ExportFile += ExportToFile;

            if (hdnApplyDiscount.Value == "true")
                mpeApplyDiscount.Show();

            if (IsPostBack) return;

            VerifyReaseguro();
            Initialize();

            ucIllustrationInformation.FillData();
            ucInsuredInformation.Initialize();

            bool visibleBtn = (ucIllustrationInformation.IllustrationStatusCode == Utility.IllustrationStatus.Effective.Code());

            btnCondicionado.Visible = visibleBtn;
            btnCondPart.Visible = visibleBtn;

            bool inspectionButton = (ObjServices.AlliedLinesProductBehavior == Utility.AlliedLinesType.Vehicle || ObjServices.AlliedLinesProductBehavior == Utility.AlliedLinesType.Property);

            btnInspection.Visible = inspectionButton;

            if (Session["fromInspection"] != null)
            {
                Initialize();
                Session["fromInspection"] = null;
            }

            #region Discounts
            string role = string.Empty;

            if (ObjServices.IsSuscripcionManagerQuotRole)
                role = Utility.DiscountRoles.SuscripcionManagerCot.ToString();

            if (ObjServices.IsDirectorQuotRole)
                role = Utility.DiscountRoles.DirectorCot.ToString();

            if (ObjServices.IsSucripcionDirectorQuotRole)
                role = Utility.DiscountRoles.DirectorSuscricion.ToString();

            if (ObjServices.isUserCot)
                role = Utility.DiscountRoles.UserCot.ToString();

            if (ObjServices.isDescuentocot)
                role = Utility.DiscountRoles.DescuentoCot.ToString();

            if (ObjServices.isDescuentoCot100Porc)
                role = Utility.DiscountRoles.DescuentoCot100Porc.ToString();


            hdnDiscountRole.Value = role;
            hdnCorpId.Value = ObjServices.Corp_Id.ToString();
            #endregion

            if (ObjServices.ProductLine == Utility.ProductLine.Auto)
            {
                mtvMaster.SetActiveView(vVehicleDetail);
                UpdatePanel6.Visible = true;
                ucHealthView.Visible = false;

                var IsRequireTabRedirect = (Session["RequiredTab"] != null && Session["RequiredTab"].ToBoolean());

                if (IsRequireTabRedirect)
                {
                    hdnCurrentTab.Value = "lnkRequired";
                    mtvTabs.SetActiveView(vRequired);
                    Session["RequiredTab"] = false;
                    this.ExcecuteJScript("$(document).ready(function() { BeginRequestHandler(); __doPostBack('ctl00$bodyContent$lnkRequired', ''); });");
                    return;
                }
                else
                    FillVehiclesInformation();
            }
            else if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
            {
                mtvMaster.SetActiveView(vAlliedLinesDetail);

                var IsRequireTabRedirect = (Session["RequiredTab"] != null && Session["RequiredTab"].ToBoolean());

                if (IsRequireTabRedirect)
                {
                    hdnCurrentTab.Value = "lnkRequired";
                    mtvTabs.SetActiveView(vRequired);
                    Session["RequiredTab"] = false;
                    this.ExcecuteJScript("$(document).ready(function() { BeginRequestHandler(); __doPostBack('ctl00$bodyContent$lnkRequired', ''); });");
                    return;
                }
                else
                    UCAlliedLinesDetail.Initialize();
            }

            if (ObjServices.isExclusion || ObjServices.isVehicleChange)
            {
                hideButtonsOnExclusion();
            }
            else
            {
                btnSeeIllustration.Text = Resources.SeeIllustration;
            }

        }

        private bool InpectionCompleted()
        {
            bool allInspected = false;

            var vehicleReview = ObjServices.oVehicleManager.GetVehicleReview(new Vehicle
            {
                CorpId = ObjServices.Corp_Id,
                RegionId = ObjServices.Region_Id,
                CountryId = ObjServices.Country_Id,
                DomesticRegId = ObjServices.Domesticreg_Id,
                StateProvId = ObjServices.State_Prov_Id,
                CityId = ObjServices.City_Id,
                OfficeId = ObjServices.Office_Id,
                CaseSeqNo = ObjServices.Case_Seq_No,
                HistSeqNo = ObjServices.Hist_Seq_No
            });

            allInspected = vehicleReview.Any();
            return allInspected;

        }

        private void ChangeButtonsStatus()
        {
            if (ObjServices.IsSuscripcionQuotRole && !IsGerencialRole)
                btnOpenApplySurcharge.Visible =
                btnOpenApplyDiscount.Visible =
                AssignedSubscriberId.HasValue &&
                AssignedSubscriberId.GetValueOrDefault() == ObjServices.Agent_Id &&
                IllustrationIsSubcription();
            else
                btnOpenApplySurcharge.Visible =
                btnOpenApplyDiscount.Visible = IllustrationIsSubcription();


            btnDeclinedByClient.Visible = EnableDeclinedByClient();

            btnReopenIllustration.Visible = EnableReopenIllustration();

            var tab = ucPopupChangeStatusSaveNotes.getTab(!string.IsNullOrEmpty(ObjServices.hdnQuotationTabs) ? ObjServices.hdnQuotationTabs : "");

            //btnInspection.Visible = (tab == "QoutationsMissingInspections");

            ProcessEnableDataButtonSubscriptionRole();
        }

        private void ProcessEnableDataButtonSubscriptionRole()
        {

        }

        private bool IllustrationIsSubcription()
        {
            return new[] {
                    Utility.IllustrationStatus.Subscription.Code(),
                    Utility.IllustrationStatus.Submitted.Code()}.Contains(IllustrationStatusCode);
        }

        private bool EnablePendingDeclinedByClient()
        {
            return new[] {
                    Utility.IllustrationStatus.NewPlan.Code(),
                    Utility.IllustrationStatus.PendingByClient.Code(),
                    Utility.IllustrationStatus.Illustration.Code()
            }.Contains(IllustrationStatusCode);
        }

        private bool EnableDeclinedByClient()
        {
            var tb = string.Empty;

            if (!string.IsNullOrEmpty(ObjServices.hdnQuotationTabs))
            {
                switch (ObjServices.hdnQuotationTabs)
                {
                    case "lnkIllustrationsToWork":
                        tb = Utility.tabsQoutationsPopUp.QoutationsToWork.ToString();
                        break;
                    case "lnkCompleteIllustrations":
                        tb = Utility.tabsQoutationsPopUp.QoutationsCompleted.ToString();
                        break;
                    case "lnkSubscriptions":
                        tb = Utility.tabsQoutationsPopUp.QoutationsSubscription.ToString();
                        break;
                    case "lnkMissingDocuments": break;
                    case "":
                        tb = Utility.tabsQoutationsPopUp.QoutationsMissingDocuments.ToString();
                        break;
                    case "lnkMissingInspections":
                        tb = Utility.tabsQoutationsPopUp.QoutationsMissingInspections.ToString();
                        break;
                }
            }

            return new[] {
                     Utility.tabsQoutationsPopUp.QoutationsToWork.ToString(),
                     Utility.tabsQoutationsPopUp.QoutationsCompleted.ToString(),
                     Utility.tabsQoutationsPopUp.QoutationsSubscription.ToString(),
                     Utility.tabsQoutationsPopUp.QoutationsMissingDocuments.ToString(),
                     Utility.tabsQoutationsPopUp.QoutationsMissingInspections.ToString()
            }.Contains(tb);
        }

        private bool EnableSubscription()
        {
            var hasJustOneVehicleAndBasic =
                ucVehiclesInformation.ListVehicles.Count == 1 &&
                ucVehiclesInformation.ListVehicles.Single().ProductNameKey == Utility.ProductBehavior.basico.ToString();

            return !hasJustOneVehicleAndBasic && new[] {
                    Utility.IllustrationStatus.NewPlan.Code(),
                    Utility.IllustrationStatus.PendingByClient.Code(),
                    Utility.IllustrationStatus.ApprovedByClient.Code(),
                    Utility.IllustrationStatus.Illustration.Code()
            }.Contains(IllustrationStatusCode);
        }

        private bool EnableReopenIllustration()
        {
            return new[] {
                    Utility.IllustrationStatus.DeclinedBySubscription.Code(),
                    Utility.IllustrationStatus.DeclinedByClient.Code()
            }.Contains(IllustrationStatusCode) &&
            ucIllustrationInformation.IllustrationDate.HasValue &&
            (DateTime.Now - ucIllustrationInformation.IllustrationDate.Value).Days < 15;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        public void Initialize()
        {
            btnDeclinedByClient.Attributes.Add("data-StatusMessage", Resources.Are_you_sure_you_want_to_change_the_status_to_the_following_quote);

            var tb = Utility.tabsQoutationsPopUp.QoutationsToWork.ToString();

            if (!string.IsNullOrEmpty(ObjServices.hdnQuotationTabs))
            {
                switch (ObjServices.hdnQuotationTabs)
                {
                    case "lnkIllustrationsToWork": tb = Utility.tabsQoutationsPopUp.QoutationsToWork.ToString(); break;
                    case "lnkCompleteIllustrations": tb = Utility.tabsQoutationsPopUp.QoutationsCompleted.ToString(); break;
                    case "lnkSubscriptions": tb = Utility.tabsQoutationsPopUp.QoutationsSubscription.ToString(); break;
                    case "lnkMissingDocuments": tb = Utility.tabsQoutationsPopUp.QoutationsMissingDocuments.ToString(); break;
                    case "lnkMissingInspections": tb = Utility.tabsQoutationsPopUp.QoutationsMissingInspections.ToString(); break;
                    case "lnkExpiring": tb = Utility.tabsQoutationsPopUp.QoutationsExpiring.ToString(); break;
                    case "lnkConfirmationCall": tb = Utility.tabsQoutationsPopUp.QoutationsConfirmationCall.ToString(); break;
                }

                processVisibilityButtonsByRole(ObjServices.hdnQuotationTabs);
            }

            btnDeclinedByClient.Attributes.Add("data-Tab", tb);
            btnSubscription.Attributes.Add("data-Status", Utility.IllustrationStatus.Submitted.ToString());
            btnSubscription.Attributes.Add("data-StatusMessage", Resources.Sure_the_following_illustrations_has_been_accepted_by_client);
            liFacultative.Visible = ObjServices.HasFacultative;
            lnkBlackList.Visible = ObjServices.BlackListHasProblem;
            lnkEndorsementClarifyingDepre.Visible = false;
            lnkEndorsementOfDeductibleApplication.Visible = false;
            lnkEndorsementClarifyingEconovida.Visible = false;

            if (ObjServices.Bandeja.ToUpper() == "AUTO")
            {
                //Ocultar botones de endosos aclaratorios si los productos son ley/ultra o semi full
                var datatVehicles = ObjServices.oPolicyManager.GetVehicleInsured(new Policy.Parameter
                {
                    CorpId = ObjServices.Corp_Id,
                    RegionId = ObjServices.Region_Id,
                    CountryId = ObjServices.Country_Id,
                    DomesticregId = ObjServices.Domesticreg_Id,
                    StateProvId = ObjServices.State_Prov_Id,
                    CityId = ObjServices.City_Id,
                    OfficeId = ObjServices.Office_Id,
                    CaseSeqNo = ObjServices.Case_Seq_No,
                    HistSeqNo = ObjServices.Hist_Seq_No
                }).ToList();

                var ExisteFull = datatVehicles.Any(x => x.ProductNameKey.ToUpper() == "FULL" && !x.ProductTypeDesc.ToUpper().Contains("SEMI"));
                lnkEndorsementClarifyingDepre.Visible = ExisteFull;
                lnkEndorsementOfDeductibleApplication.Visible = ExisteFull;

                
                var algunEconovida = datatVehicles.Any(x => (x.ProductTypeDesc.ToLower().Contains("econovida") || x.ProductTypeDesc.ToLower().Contains("econo vida")));
                lnkEndorsementClarifyingEconovida.Visible = algunEconovida;
            }
        }

        private void avalibaleLinks(bool IsVisible = false)
        {
            foreach (var item in pnLinksButtons.Controls)
            {
                var ctrl = item as Control;
                if (ctrl is LinkButton)
                {
                    var Lnk = (ctrl as LinkButton);
                    if (Lnk != btnForeignStatusNotification ||
                        Lnk != btnCondicionado ||
                        Lnk != btnCondPart ||
                        Lnk != btnOnBase ||
                        Lnk != btnOnBaseKCO ||
                        Lnk != lnkEndorsementOfDeductibleApplication ||
                        Lnk != lnkEndorsementClarifyingDepre ||
                        Lnk != lnkEndorsementClarifyingEconovida)
                        Lnk.Visible = IsVisible;
                }
            }
        }

        private void processVisibilityButtonsByRole(string v)
        {
            avalibaleLinks();

            var isEffectivePolicy = ObjServices.StatusNameKey == Utility.IllustrationStatus.Effective.Code();

            var TbSelected = (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), ObjServices.hdnQuotationTabs);

            var OnBaseButtonVisible = ObjServices.isUserCot &&
                                (TbSelected == Utility.Tabs.lnkApprovedBySubscription || TbSelected == Utility.Tabs.lnkDeclinedBySubscription || TbSelected == Utility.Tabs.lnkDeclinedByClient || TbSelected == Utility.Tabs.lnkHistoricalIllustrations);

            btnOnBase.Visible = OnBaseButtonVisible;
            btnOnBaseKCO.Visible = OnBaseButtonVisible;

            btnForeignStatusNotification.Visible = true;

            var Tab = Utility.ParseEnum<Utility.Tabs>(v);

            lnkBlackList.Visible =
                                   ObjServices.isUserCot ||
                                   ObjServices.IsSuscripcionQuotRole ||
                                   ObjServices.IsSucripcionDirectorQuotRole ||
                                   ObjServices.IsSuscripcionManagerQuotRole;


            lnkDataCredito.Visible = (ObjServices.isUserCot ||
                                     ObjServices.IsSuscripcionQuotRole ||
                                     ObjServices.IsSucripcionDirectorQuotRole ||
                                     ObjServices.IsSuscripcionManagerQuotRole) && !isEffectivePolicy;

            switch (Tab)
            {
                case Utility.Tabs.lnkIllustrationsToWork:
                    btnSeeIllustration.Visible = true;
                    btnDeclinedByClient.Visible = ObjServices.IsAgentQuotRole;

                    btnInspection.Visible = true;

                    btnOpenApplyDiscount.Visible = ObjServices.IsPreSuscribcionDescuentoCot || (ObjServices.IsPreSuscribcionDescuentoCot && ObjServices.IsPreSuscribcionRecargoCot);
                    btnOpenApplySurcharge.Visible = ObjServices.IsPreSuscribcionRecargoCot  || (ObjServices.IsPreSuscribcionDescuentoCot && ObjServices.IsPreSuscribcionRecargoCot);

                    if (ObjServices.isUserCot)
                    {
                        avalibaleLinks(true);
                        btnDeclinedByClient.Visible = true;
                        btnSubscription.Visible = false;
                        btnDeclinedBySubscription.Visible = false;
                        btnOpenApplyDiscount.Visible = false;
                        btnOpenApplySurcharge.Visible = false;
                        btnSendToCore.Visible = false;
                        btnReopenIllustration.Visible = false;
                        //btnInspection.Visible = false;//ORIGINAL 23-06-2017                        
                        btnCondicionado.Visible = false;
                        btnCondPart.Visible = false;
                    }

                    lnkEndorsementOfDeductibleApplication.Visible = true;
                    lnkEndorsementClarifyingDepre.Visible = true;
                    
                    break;
                case Utility.Tabs.lnkCompleteIllustrations:
                    btnSeeIllustration.Visible = true;
                    btnDeclinedByClient.Visible = ObjServices.IsAgentQuotRole;
                    btnInspection.Visible = true;

                    if (ObjServices.isUserCot)
                    {
                        avalibaleLinks(true);
                        btnDeclinedByClient.Visible = true;
                        btnSeeIllustration.Visible = false;
                        btnSubscription.Visible = false;
                        btnDeclinedBySubscription.Visible = false;
                        btnOpenApplyDiscount.Visible = false;
                        btnOpenApplySurcharge.Visible = false;
                        btnSendToCore.Visible = false;
                        btnReopenIllustration.Visible = false;
                        //btnInspection.Visible = false;//ORIGINAL 23-06-2017                        
                        btnCondicionado.Visible = false;
                        btnCondPart.Visible = false;
                    }

                    lnkEndorsementOfDeductibleApplication.Visible = true;
                    lnkEndorsementClarifyingDepre.Visible = true;
                    break;
                case Utility.Tabs.lnkDeclinedByClient:
                    btnSeeIllustration.Visible = true;
                    btnDeclinedByClient.Visible = false;
                    btnSubscription.Visible = false;
                    btnDeclinedBySubscription.Visible = false;
                    btnOpenApplyDiscount.Visible = false;
                    btnOpenApplySurcharge.Visible = false;
                    btnSendToCore.Visible = false;
                    btnReopenIllustration.Visible = false;
                    btnInspection.Visible = false;
                    btnCondicionado.Visible = false;
                    btnCondPart.Visible = false;
                    lnkEndorsementOfDeductibleApplication.Visible = false;
                    lnkEndorsementClarifyingDepre.Visible = false;
                    //lnkEndorsementClarifyingEconovida.Visible = false;
                    break;
                case Utility.Tabs.lnkExpired:
                    btnSeeIllustration.Visible = true;
                    lnkEndorsementOfDeductibleApplication.Visible = false;
                    lnkEndorsementClarifyingDepre.Visible = false;
                    //lnkEndorsementClarifyingEconovida.Visible = false;
                    break;
                case Utility.Tabs.lnkExpiring:
                    btnSeeIllustration.Visible = true;
                    btnDeclinedByClient.Visible = ObjServices.IsAgentQuotRole;

                    btnInspection.Visible = true;

                    if (ObjServices.isUserCot)
                    {
                        avalibaleLinks(true);
                        btnDeclinedByClient.Visible = true;
                        btnSeeIllustration.Visible = false;
                        btnSubscription.Visible = false;
                        btnDeclinedBySubscription.Visible = false;
                        btnOpenApplyDiscount.Visible = false;
                        btnOpenApplySurcharge.Visible = false;
                        btnSendToCore.Visible = false;
                        btnReopenIllustration.Visible = false;
                        //btnInspection.Visible = false;//ORIGINAL 23-06-2017                        
                        btnCondicionado.Visible = false;
                        btnCondPart.Visible = false;
                    }

                    lnkEndorsementOfDeductibleApplication.Visible = true;
                    lnkEndorsementClarifyingDepre.Visible = true;
                    break;
                case Utility.Tabs.lnkSubscriptions:
                case Utility.Tabs.lnkDiscounts:
                    btnSeeIllustration.Visible = true;
                    //btnInspection.Visible = InpectionCompleted();//ORIGINAL 23-06-2017
                    btnInspection.Visible = true;

                    btnDeclinedByClient.Visible = ObjServices.IsAgentQuotRole;

                    if ((ObjServices.IsSuscripcionQuotRole && ObjServices.isDescuentocot) || ObjServices.isDescuentocot)
                        btnOpenApplyDiscount.Visible = true;

                    if (ObjServices.IsSuscripcionQuotRole)
                    {
                        btnDeclinedByClient.Visible = true;
                        btnOpenApplySurcharge.Visible = true;
                        btnSendToCore.Visible = true;
                    }

                    if (ObjServices.IsSuscripcionManagerQuotRole || ObjServices.IsDirectorQuotRole)
                    {
                        btnOpenApplyDiscount.Visible = true;
                        btnOpenApplySurcharge.Visible = true;
                    }
                    else if (ObjServices.IsSucripcionDirectorQuotRole)
                    {
                        btnOpenApplyDiscount.Visible = true;
                        btnOpenApplySurcharge.Visible = true;
                    }

                    if ((ObjServices.IsSucripcionDirectorQuotRole && ObjServices.IsSuscripcionQuotRole))
                    {
                        btnOpenApplyDiscount.Visible = true;
                        btnOpenApplySurcharge.Visible = true;
                        btnDeclinedByClient.Visible = true;
                        btnOpenApplySurcharge.Visible = true;
                        btnSendToCore.Visible = true;
                    }

                    else if (ObjServices.isUserCot)
                    {
                        avalibaleLinks(true);

                        //btnInspection.Visible = InpectionCompleted();//ORIGINAL 23-06-2017                        
                        btnDeclinedByClient.Visible = true;
                        btnOpenApplyDiscount.Visible = true;
                        btnDeclinedBySubscription.Visible = false;
                        btnOpenApplySurcharge.Visible = true;
                        btnSendToCore.Visible = true;
                        btnSubscription.Visible = false;
                        btnReopenIllustration.Visible = false;
                        btnCondicionado.Visible = false;
                        btnCondPart.Visible = false;
                    }

                    lnkEndorsementOfDeductibleApplication.Visible = true;
                    lnkEndorsementClarifyingDepre.Visible = true;
                    break;
                case Utility.Tabs.lnkMissingDocuments:
                    btnSeeIllustration.Visible = true;
                    btnDeclinedByClient.Visible = ObjServices.IsAgentQuotRole;
                    //btnInspection.Visible = InpectionCompleted();//ORIGINAL 23-06-2017 
                    btnInspection.Visible = true;

                    if (ObjServices.isUserCot)
                    {
                        avalibaleLinks(true);
                        btnDeclinedByClient.Visible = true;
                        btnOpenApplyDiscount.Visible = false;
                        btnDeclinedBySubscription.Visible = false;
                        btnOpenApplySurcharge.Visible = false;
                        btnSubscription.Visible = false;
                        btnSendToCore.Visible = false;
                        btnReopenIllustration.Visible = false;
                        btnCondicionado.Visible = false;
                        btnCondPart.Visible = false;
                    }

                    lnkEndorsementOfDeductibleApplication.Visible = true;
                    lnkEndorsementClarifyingDepre.Visible = true;
                    break;
                case Utility.Tabs.lnkMissingInspections:
                    btnSeeIllustration.Visible = true;
                    //btnInspection.Visible = ObjServices.IsInspectorQuotRole ? true : InpectionCompleted();// ORIGINAL 23-06-2017
                    btnInspection.Visible = ObjServices.IsInspectorQuotRole;
                    btnDeclinedByClient.Visible = ObjServices.IsInspectorQuotRole;

                    if (ObjServices.isUserCot)
                    {
                        avalibaleLinks(true);
                        btnDeclinedByClient.Visible = true;
                        btnInspection.Visible = true;
                        btnOpenApplyDiscount.Visible = false;
                        btnSubscription.Visible = false;
                        btnDeclinedBySubscription.Visible = false;
                        btnOpenApplySurcharge.Visible = false;
                        btnSendToCore.Visible = false;
                        btnReopenIllustration.Visible = false;
                        btnCondicionado.Visible = false;
                        btnCondPart.Visible = false;
                    }
                    lnkEndorsementOfDeductibleApplication.Visible = false;
                    lnkEndorsementClarifyingDepre.Visible = false;
                    //lnkEndorsementClarifyingEconovida.Visible = false;
                    break;
                case Utility.Tabs.lnkApprovedBySubscription:
                case Utility.Tabs.lnkHistoricalIllustrations:
                    var visible = ObjServices.StatusNameKey == Utility.IllustrationStatus.Effective.Code();
                    btnInspection.Visible = InpectionCompleted();
                    btnSeeIllustration.Visible = true;
                    btnCondicionado.Visible = visible;
                    btnCondPart.Visible = visible;
                    lnkEndorsementOfDeductibleApplication.Visible = false;
                    lnkEndorsementClarifyingDepre.Visible = false;
                    lnkDataCredito.Visible = ObjServices.isUserCot;
                    //lnkEndorsementClarifyingEconovida.Visible = true;
                    break;
                case Utility.Tabs.lnkDeclinedBySubscription:
                    btnSeeIllustration.Visible = true;
                    btnDeclinedByClient.Visible = ObjServices.isUserCot || ObjServices.IsCreditoCot;
                    lnkEndorsementOfDeductibleApplication.Visible = false;
                    lnkEndorsementClarifyingDepre.Visible = false;
                    //lnkEndorsementClarifyingEconovida.Visible = false;
                    break;
                case Utility.Tabs.lnkIncompleteIllustration:
                    break;
                case Utility.Tabs.lnkConfirmationCall:
                    btnDeclinedByClient.Visible = true;
                    lnkEndorsementOfDeductibleApplication.Visible = false;
                    lnkEndorsementClarifyingDepre.Visible = false;
                    //lnkEndorsementClarifyingEconovida.Visible = false;
                    break;
                default:
                    break;
            }
        }

        protected void lnkIllustrations_Click(object sender, EventArgs e)
        {
            Session["firstOption"] = false;
            Response.Redirect("Illustrations.aspx");
        }

        private void ChangeStatus(Utility.IllustrationStatus illustrationStatus, string note = "", List<Policy.VehiclesCoverage> lst = null, string Comment = null)
        {
            var policyNo = "";
            try
            {
                policyNo = ucIllustrationInformation.GetIllustrationNo();

                string tab = ucPopupChangeStatusSaveNotes.getTab(!string.IsNullOrEmpty(ObjServices.hdnQuotationTabs) ? ObjServices.hdnQuotationTabs : "");
                if (tab == "QoutationsMissingInspections")
                {
                    if (illustrationStatus == Utility.IllustrationStatus.Subscription)
                    {
                        //aqui debo consultar si el status de inspection esta o no
                        bool r = ObjServices.hasInspectionAllVehicles(ObjServices.Case_Seq_No, ObjServices.City_Id, ObjServices.Corp_Id, ObjServices.Country_Id, ObjServices.Domesticreg_Id,
                            ObjServices.Hist_Seq_No, ObjServices.Office_Id,
                            ObjServices.Region_Id, ObjServices.State_Prov_Id);
                        if (!r)
                        {
                            this.MessageBox(Resources.QuotationInspectionNoChange, Title: "Error", Width: 500, Height: 150);
                            Session["areInspected"] = "False";
                            return;
                        }
                    }
                }

                Session["areInspected"] = null;

                ObjServices.ChangeIllustrationStatus(-1,
                                                     ObjServices.Corp_Id,
                                                     ObjServices.Region_Id,
                                                     ObjServices.Country_Id,
                                                     ObjServices.Domesticreg_Id,
                                                     ObjServices.State_Prov_Id,
                                                     ObjServices.City_Id,
                                                     ObjServices.Office_Id,
                                                     ObjServices.Case_Seq_No,
                                                     ObjServices.Hist_Seq_No,
                                                     ObjServices.UserID.GetValueOrDefault(),
                                                     illustrationStatus,
                                                     note,
                                                     ObjServices.Agent_Id,
                                                     Comment
                                                     );

                ucIllustrationInformation.IllustrationStatusCode = illustrationStatus.Code();
                ucIllustrationInformation.Translator(null);

                ucIllustrationsInformation.FillData();
                ucVehiclesInformation.FillData();
                ChangeButtonsStatus();
                btnWorkflow_Click(null, null);

                Utility.ExcecuteJScript(this, string.Format("BackToIllustrationList('{0}', '{1}')", Resources.StatusChangedSuccessfully, Resources.Success));
            }
            catch (Exception ex)
            {
                var msg = ex.GetLastInnerException().Message;
                this.MessageBox(Resources.AErrorOccurredWithIllustrationNo.SFormat(policyNo) + (!msg.SIsNullOrEmpty() ? " - " + msg.RemoveInvalidCharacters().Replace('\'', '\"') : ""), Title: "Error");
            }
        }

        public void FillNotes(int corpId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            ucPopupChangeStatusSaveNotes.FillNotes(
                                                     corpId,
                                                     regionId,
                                                     countryId,
                                                     domesticRegId,
                                                     stateProvId,
                                                     cityId,
                                                     officeId,
                                                     caseSeqNo,
                                                     histSeqNo);
            this.ExcecuteJScript("setTimeout('ppcNotes.Show()', 10);");
        }

        protected void btnDeclinedByClient_Click(object sender, EventArgs e)
        {
            var policyNo = ucIllustrationInformation.GetIllustrationNo();

            try
            {
                ucPopupChangeStatusSaveNotes.Initialize();

                ucPopupChangeStatusSaveNotes.FillReasonDenied(ObjServices.ProductLine.ToString(),
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

        protected void btnOpenApplyDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                //Esta logica solo aplica para la linea de negocios de auto
                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                {
                    //Cuando un cliente sea menor de 26 años o mayor de 70 no permitir descuentos
                    var DataPolicy = ObjServices.oPolicyManager.GetPolicy(ObjServices.Corp_Id,
                                                                          ObjServices.Region_Id,
                                                                          ObjServices.Country_Id,
                                                                          ObjServices.Domesticreg_Id,
                                                                          ObjServices.State_Prov_Id,
                                                                          ObjServices.City_Id,
                                                                          ObjServices.Office_Id,
                                                                          ObjServices.Case_Seq_No,
                                                                          ObjServices.Hist_Seq_No);

                    if (DataPolicy != null)
                    {
                        //Buscar el contacto de esta poliza
                        var dataContact = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, DataPolicy.ContactId, ObjServices.Language.ToInt());

                        if (dataContact != null && dataContact.ContactTypeId == Utility.ContactTypeId.Contact.ToInt())
                        {
                            var Age = dataContact.Age.GetValueOrDefault();

                            if (Age < 26 || Age > 70)
                            {
                                this.MessageBox(Resources.Desc2670, Title: "Error", Width: 800);
                                return;
                            }
                        }
                    }
                    //hdnApplyDiscount.Value == "true"
                    this.ExcecuteJScript("setTimeout(\"OpenApplyDiscount()\",10);");
                    ucPopupApplyDiscount.Initialize();
                    hdnApplyDiscount.Value = "true";
                    mpeApplyDiscount.Show();

                }
                else if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                {
                    this.ExcecuteJScript("setTimeout(\"OpenApplyDiscount()\",10);");
                    ucPopupApplyDiscount.Initialize();
                    hdnApplyDiscount.Value = "true";
                    mpeApplyDiscount.Show();
                }
                else
                {
                    this.MessageBox("Esta función aun no ha sido programada!", Title: Resources.Warning);
                }
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }
        }

        public void FillVehiclesInformation()
        {
            var enableTag = EnableTag();
            ucVehiclesInformation.Visible = true;
            var canEnableInspection = ObjServices.IsSuscripcionQuotRole && IllustrationIsSubcription();
            ucVehiclesInformation.FillData(ucInsuredInformation.InsuredName, enableTag, canEnableInspection);
        }

        private bool EnableTag()
        {
            return !new[] {
                            Utility.IllustrationStatus.DeclinedByClient.Code(),
                            Utility.IllustrationStatus.ApprovedBySubscription.Code(),
                            Utility.IllustrationStatus.DeclinedBySubscription.Code(),
                            Utility.IllustrationStatus.NewPlan.Code(),
                            Utility.IllustrationStatus.Illustration.Code(),
                            Utility.IllustrationStatus.PendingByClient.Code(),
                            Utility.IllustrationStatus.TimeExpired.Code(),
                            Utility.IllustrationStatus.Subscription.Code(),
                            Utility.IllustrationStatus.Incomplete.Code(),
                            Utility.IllustrationStatus.ApprovedByClient.Code(),
                            Utility.IllustrationStatus.Complete.Code(),
                            Utility.IllustrationStatus.MissingInspection.Code()
                          }.Contains(IllustrationStatusCode);
        }

        protected void btnOpenApplySurcharge_Click(object sender, EventArgs e)
        {
            ucPopupApplySurcharge.Initialize();
            this.ExcecuteJScript("setTimeout(\"OpenApplySurcharge()\",10);");
        }

        protected void btnSendToCore_Click(object sender, EventArgs e)
        {
            var illustrationData = ObjServices.getillustrationData();
            Utility.RequestType RequestType = (Utility.RequestType)Enum.Parse(typeof(Utility.RequestType), illustrationData.RequestTypeDesc.Replace(" ", "").MyRemoveInvalidCharactersFilName());

            //Validacion de BlackList
            if (ObjServices.BlackListHasProblem)
            {
                if (!ObjServices.BlacklistCheck.HasValue)
                {
                    this.MessageBox(Resources.BlackListValidation);
                    return;
                }
            }

            //Validar si la poliza es financiada que se hayan capturado todos los datos de domiciliación
            var dataContact = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, ObjServices.Contact_Id.GetValueOrDefault(), ObjServices.Language.ToInt());

            if (illustrationData.DirectDebit.HasValue)
            {
                if (illustrationData.DirectDebit.Value)
                {
                    if (!dataContact.CreditCardTypeId.HasValue || string.IsNullOrEmpty(dataContact.CreditCardNumber) || string.IsNullOrEmpty(dataContact.CardHolder))
                    {
                        this.MessageBox("Se marco esta poliza para domiciliar el pago, sin embargo el sistema no tiene los datos de la tarjeta de credito");
                        return;
                    }
                }
            }

            if (illustrationData.Financed.GetValueOrDefault() && RequestType != Utility.RequestType.Exclusion)
            {
                if (!dataContact.CreditCardTypeId.HasValue || string.IsNullOrEmpty(dataContact.CreditCardNumber) || string.IsNullOrEmpty(dataContact.CardHolder))
                {
                    this.MessageBox(Resources.DomiciliationValidation);
                    return;
                }

                var IsPerson = !dataContact.IsCompany;

                //Si es una persona
                if (IsPerson)
                {
                    if (!dataContact.homeOwner.HasValue)
                    {
                        this.MessageBox(Resources.homeOwnerValidation);
                        return;
                    }

                    if (!dataContact.qtyPersonsDepend.HasValue)
                    {
                        this.MessageBox(Resources.DepValidation);
                        return;
                    }

                    if (!dataContact.MaritalStatId.HasValue)
                    {
                        this.MessageBox(Resources.MaritalStatusValidationMessage);
                        return;
                    }
                }
                //Si es una compañia
                else
                {
                    if (!dataContact.qtyEmployees.HasValue)
                    {
                        this.MessageBox(Resources.QtyemployeeValidation);
                        return;
                    }
                }
            }

            var PolicyList = new List<string>(0);
            var result = new Tuple<string, string, bool>(string.Empty, string.Empty, false);
            bool faltaInspeccion = false;
            string mensaje = "";
            string message = string.Empty;

            try
            {
                var policy = new Utility.itemPolicy()
                {
                    CorpId = ObjServices.Corp_Id,
                    RegionId = ObjServices.Region_Id,
                    CountryId = ObjServices.Country_Id,
                    DomesticregId = ObjServices.Domesticreg_Id,
                    StateProvId = ObjServices.State_Prov_Id,
                    CityId = ObjServices.City_Id,
                    OfficeId = ObjServices.Office_Id,
                    CaseSeqNo = ObjServices.Case_Seq_No,
                    HistSeqNo = ObjServices.Hist_Seq_No
                };

                //Este proceso solo aplica para auto
                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                {
                    //Data de los Vehiculos
                    var dataVehicles = ObjServices.oPolicyManager.GetVehicleInsured(new Policy.Parameter
                    {
                        CorpId = ObjServices.Corp_Id,
                        RegionId = ObjServices.Region_Id,
                        CountryId = ObjServices.Country_Id,
                        DomesticregId = ObjServices.Domesticreg_Id,
                        StateProvId = ObjServices.State_Prov_Id,
                        CityId = ObjServices.City_Id,
                        OfficeId = ObjServices.Office_Id,
                        CaseSeqNo = ObjServices.Case_Seq_No,
                        HistSeqNo = ObjServices.Hist_Seq_No
                    }).ToList();

                    if (dataVehicles.Count > 0)
                    {
                        faltaInspeccion = dataVehicles.Any(p => p.InspectionRequired.GetValueOrDefault()); //valido si al menos un vehiculo requiere inspeccion

                        if (faltaInspeccion)
                        {
                            mensaje = ObjServices.InspectionCompleted(policy, ObjServices.ProductLine);
                            if (string.IsNullOrEmpty(mensaje))
                            {
                                if (illustrationData.InspectorAgentId == null || illustrationData.InspectorAgentId <= 0)
                                {
                                    mensaje = Resources.YouMustIndicateInspectorName;
                                }

                                var resValidation = ObjServices.HasCustomerSignInInspection();

                                if (resValidation.Item1 == false)
                                {
                                    this.MessageBox(resValidation.Item2);
                                    return;
                                }
                            }

                            if (!string.IsNullOrEmpty(mensaje)) { throw new Exception(mensaje); }
                        }
                        
                    }

                    ObjServices.VerifyCanSendQuotesToSysFlex(Utility.ProductLine.Auto.ToString(), "ISU", ObjServices.Policy_Id);

                    switch (RequestType)
                    {
                        case Utility.RequestType.Emision:
                            result = ObjServices
                             .SendQuotToSysFlexNewVersion(ObjServices.Corp_Id,
                                                          ObjServices.Region_Id,
                                                          ObjServices.Country_Id,
                                                          ObjServices.Domesticreg_Id,
                                                          ObjServices.State_Prov_Id,
                                                          ObjServices.City_Id,
                                                          ObjServices.Office_Id,
                                                          ObjServices.Case_Seq_No,
                                                          ObjServices.Hist_Seq_No,
                                                          false,
                                                          ObjServices.Policy_Id,
                                                          Server.MapPath("~/NewBusiness/XML/")
                                                          );

                            message = string.Format(Resources.PolicySysflexMessage, result.Item1);

                            break;
                        case Utility.RequestType.Inclusion:
                            string VehiclesIncluded = string.Empty;

                            result = ObjServices.SendQuotInclusionSysflex(
                                                            ObjServices.Corp_Id,
                                                            ObjServices.Region_Id,
                                                            ObjServices.Country_Id,
                                                            ObjServices.Domesticreg_Id,
                                                            ObjServices.State_Prov_Id,
                                                            ObjServices.City_Id,
                                                            ObjServices.Office_Id,
                                                            ObjServices.Case_Seq_No,
                                                            ObjServices.Hist_Seq_No,
                                                            ObjServices.Policy_Id,
                                                            out VehiclesIncluded,
                                                            Server.MapPath("~/NewBusiness/XML/")
                                                            );

                            message = string.Format(Resources.InlcusionMessage, result.Item1, VehiclesIncluded);

                            break;
                        case Utility.RequestType.Exclusion:
                            string VehiclesExcluded = string.Empty;

                            result = ObjServices.SendQuotExclusionSysflex(
                                                            ObjServices.Corp_Id,
                                                            ObjServices.Region_Id,
                                                            ObjServices.Country_Id,
                                                            ObjServices.Domesticreg_Id,
                                                            ObjServices.State_Prov_Id,
                                                            ObjServices.City_Id,
                                                            ObjServices.Office_Id,
                                                            ObjServices.Case_Seq_No,
                                                            ObjServices.Hist_Seq_No,
                                                            ObjServices.Policy_Id,
                                                            out VehiclesExcluded,
                                                            Server.MapPath("~/NewBusiness/XML/")
                                                            );
                            message = string.Format(Resources.ExclusionMessage, result.Item1, VehiclesExcluded);
                            break;
                        case Utility.RequestType.Cambios:
                            VehiclesExcluded = string.Empty;

                            result = ObjServices.SendQuotRequestChanceSysflex(
                                                            ObjServices.Corp_Id,
                                                            ObjServices.Region_Id,
                                                            ObjServices.Country_Id,
                                                            ObjServices.Domesticreg_Id,
                                                            ObjServices.State_Prov_Id,
                                                            ObjServices.City_Id,
                                                            ObjServices.Office_Id,
                                                            ObjServices.Case_Seq_No,
                                                            ObjServices.Hist_Seq_No,
                                                            ObjServices.Policy_Id,
                                                            out VehiclesExcluded,
                                                            Server.MapPath("~/NewBusiness/XML/")
                                                            );
                            message = string.Format(Resources.RequestChangeMessageCompleted, result.Item1, VehiclesExcluded);
                            break;
                        case Utility.RequestType.Renovacion:
                            string VehiclesRenovados = string.Empty;

                            result = ObjServices.SendQuotRenovacionSysflex(
                                                            ObjServices.Corp_Id,
                                                            ObjServices.Region_Id,
                                                            ObjServices.Country_Id,
                                                            ObjServices.Domesticreg_Id,
                                                            ObjServices.State_Prov_Id,
                                                            ObjServices.City_Id,
                                                            ObjServices.Office_Id,
                                                            ObjServices.Case_Seq_No,
                                                            ObjServices.Hist_Seq_No,
                                                            ObjServices.Policy_Id,
                                                            out VehiclesRenovados,
                                                            Server.MapPath("~/NewBusiness/XML/")
                                                            );

                            message = string.Format(Resources.RenovacionMessage, result.Item1, VehiclesRenovados);
                            break;
                        case Utility.RequestType.InclusionDeclarativa:
                            VehiclesIncluded = string.Empty;

                            result = ObjServices.SendQuotInclusionDeclarativaSysflex(
                                                            ObjServices.Corp_Id,
                                                            ObjServices.Region_Id,
                                                            ObjServices.Country_Id,
                                                            ObjServices.Domesticreg_Id,
                                                            ObjServices.State_Prov_Id,
                                                            ObjServices.City_Id,
                                                            ObjServices.Office_Id,
                                                            ObjServices.Case_Seq_No,
                                                            ObjServices.Hist_Seq_No,
                                                            ObjServices.Policy_Id,
                                                            out VehiclesIncluded,
                                                            Server.MapPath("~/NewBusiness/XML/")
                                                            );

                            message = string.Format(Resources.InlcusionMessage, result.Item1, VehiclesIncluded);

                            break;
                    }
                }

                //Este proceso solo aplica para incendio y lineas aliadas
                else if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                {
                    #region Get Property
                    var property = ObjServices.oPropertyManager.GetProperty(new Property.Key
                    {
                        CorpId = ObjServices.Corp_Id,
                        RegionId = ObjServices.Region_Id,
                        CountryId = ObjServices.Country_Id,
                        DomesticregId = ObjServices.Domesticreg_Id,
                        StateProvId = ObjServices.State_Prov_Id,
                        CityId = ObjServices.City_Id,
                        OfficeId = ObjServices.Office_Id,
                        CaseSeqNo = ObjServices.Case_Seq_No,
                        HistSeqNo = ObjServices.Hist_Seq_No
                    }).ToList();

                    if (property.Count > 0)
                    {
                        faltaInspeccion = property.Any(p => p.RequiresInspection); //valido si al menos una propiedad requiere inspeccion

                        if (faltaInspeccion)
                        {
                            mensaje = ObjServices.InspectionCompleted(policy, ObjServices.ProductLine);
                            if (!string.IsNullOrEmpty(mensaje)) { throw new Exception(mensaje); }
                        }
                    }
                    #endregion


                    ObjServices.VerifyCanSendQuotesToSysFlexIL(Utility.ProductLine.AlliedLines.ToString(), "ISU", ObjServices.Policy_Id);

                    result = ObjServices.SendQuotToSysFlex_IL(ObjServices.Corp_Id,
                                                              ObjServices.Region_Id,
                                                              ObjServices.Country_Id,
                                                              ObjServices.Domesticreg_Id,
                                                              ObjServices.State_Prov_Id,
                                                              ObjServices.City_Id,
                                                              ObjServices.Office_Id,
                                                              ObjServices.Case_Seq_No,
                                                              ObjServices.Hist_Seq_No,
                                                              false,
                                                              ObjServices.Policy_Id,
                                                              Server.MapPath("~/NewBusiness/XML/")
                                                              );

                    message = string.Format(Resources.PolicySysflexMessage, result.Item1);
                }


                //Cuando se genera un error generando la factura en Sysflex
                string MessageError = string.Empty;
                string messageErrorEx = message;
                //Numero de poliza
                if (result.Item3)
                {
                    MessageError = string.Concat("Se genero un error generando la factura en SysFlex ", "<br/><br/>");

                    //Example message error {1} The policy number {0} was generated in Sysflex                  
                    if (RequestType == Utility.RequestType.Emision)
                        messageErrorEx = string.Format(Resources.ErrorGeneratingSysflexInvoicing, MessageError, result.Item1);
                    else
                        if (RequestType == Utility.RequestType.Inclusion || RequestType == Utility.RequestType.Exclusion || RequestType == Utility.RequestType.Cambios)
                        messageErrorEx = string.Format(Resources.ErrorGeneratingSysflexInvoicingInclusion, MessageError, message);

                    //Enviar correo de notificacion de error
                    var dataConfig = ObjServices.GettingDropData(Utility.DropDownType.ProjectConfigurationValue, corpId: ObjServices.Corp_Id, pProjectId: int.Parse(System.Configuration.ConfigurationManager.AppSettings["ProjectIdNewBusiness"]));
                    var EmailData = dataConfig.FirstOrDefault(v => v.Namekey == "ErrorInvoiceSysFlexAuto");

                    if (EmailData != null)
                    {
                        var EmailFrom = ConfigurationManager.AppSettings["EmailFrom"];
                        var Cc = string.Empty;
                        var Bcc = string.Empty;

                        var isTest = System.Configuration.ConfigurationManager.AppSettings["isTestingQuotDebug"] == "true";

                        MailManager.SendMessage(
                                                 EmailData.ConfigurationValue,
                                                 Cc,
                                                 Bcc,
                                                 message,
                                                 EmailFrom,
                                                 string.Format("Bandeja Virtual Office {0} - Error en emisión de póliza generando factura en sysflex. Usuario : {1}", isTest ? "Dev" : "Prod", ObjServices.UserFullName),
                                                 "",
                                                 true
                                                );
                    }
                }

                PolicyList.Add(messageErrorEx);
                var msj = string.Join("<br/>", PolicyList.ToArray());
                ucIllustrationInformation.FillData();
                ucIllustrationInformation.Translator(null);
                ChangeButtonsStatus();

                //Actualizar el resumen   
                var InsuredInformationUC = Utility.GetAllChildren(this.Page).FirstOrDefault(uc => uc is UCInsuredInformation);

                if (InsuredInformationUC != null)
                    (InsuredInformationUC as UCInsuredInformation).Initialize();

                Utility.ExcecuteJScript(this, string.Format("BackToIllustrationList('{0}', '{1}')", msj, Resources.Alert));
            }
            catch (Exception ex)
            {
                var MessageEx = ex.Message.Replace('\'', '\"').MyRemoveInvalidCharacters();
                var InnerExceptionEx = ex.InnerException == null ? string.Empty : ex.InnerException.ToString();

                var StackTraceEx = ex.StackTrace.Replace('\'', '\"').MyRemoveInvalidCharacters();

                var isInnerException = !string.IsNullOrEmpty(InnerExceptionEx);

                var msg = isInnerException ? string.Format("{0}  <br> <br> Presione Ok para descargar un archivo con el detalle del error", MessageEx)
                                           : string.Format("{0}", MessageEx);
                var IsErrorOnBase = false;

                var msgSplitErr = msg.Split('|');

                IsErrorOnBase = msgSplitErr.Length > 1;

                if (!IsErrorOnBase)
                {
                    if (!isInnerException)
                        this.MessageBox(msg, Title: "Error", Width: 800);
                    else
                    {
                        base.ErrorDescription = InnerExceptionEx;
                        this.CustomDialogMessageWithCallBack(msg, "function(){$('#btnGenerateFileError').click();}", "Error", "", "");
                    }
                }
                else
                    Utility.ExcecuteJScript(this, string.Format("BackToIllustrationList('{0}', '{1}')", msgSplitErr[0], Resources.Alert));
            }
        }

        public void SetIframeSrcTagConditioned(string src, int? DocumentId = null, int? DocumentCategoryId = null)
        {
            UCPopupVehicleTagConditioned.Initialize();
            UCPopupVehicleTagConditioned.DocumentId = DocumentId.GetValueOrDefault();
            UCPopupVehicleTagConditioned.DocumentCategporyID = DocumentCategoryId.GetValueOrDefault();
            UCPopupVehicleTagConditioned.SetIframeSrcTagConditioned(src);
        }

        protected void btnPolicy_Click(object sender, EventArgs e)
        {
            ucPoliciesInformation.FillData();
        }

        protected void btnIllustrationHistoric_Click(object sender, EventArgs e)
        {
            ucIllustrationsInformation.FillData();
        }

        protected void btnIllustration_Click(Object sender, EventArgs e)
        {
            if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                ucVehiclesInformation.Initialize();
            else if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                UCAlliedLinesDetail.Initialize();
        }

        protected void ManageTab(object sender, EventArgs e)
        {
            var Lnk = (sender as LinkButton).ID;
            hdnCurrentTab.Value = Lnk;

            switch (Lnk)
            {
                case "lnkIllustration":
                    mtvTabs.SetActiveView(vCotizacion);
                    btnIllustration_Click(btnIllustration, null);
                    break;
                case "lnkPolicy":
                    mtvTabs.SetActiveView(vPoliza);
                    btnPolicy_Click(btnPolicy, null);
                    break;
                case "lnkIllustrationHistoric":
                    mtvTabs.SetActiveView(vIllustration);
                    btnIllustrationHistoric_Click(btnIllustrationHistoric, null);
                    break;
                case "lnkRequired":
                    mtvTabs.SetActiveView(vRequired);
                    Session["hdnCurrentTab"] = "lnkRequired";
                    ucRequiredInformation.Initialize();
                    break;
                case "lnkFacultative":
                    mtvTabs.SetActiveView(vFacultative);
                    UCFacultyPosition.Initialize();
                    break;
                case "lnkWorkflow":
                    mtvTabs.SetActiveView(vWorkflow);
                    btnWorkflow_Click(btnWorkflow, null);
                    break;
            }
        }

        protected void btnReopenIllustration_Click(object sender, EventArgs e)
        {
            ChangeStatus(Utility.IllustrationStatus.Subscription);
        }

        protected void btnDeclinedBySubscription_Click(object sender, EventArgs e)
        {
            var policyNo = ucIllustrationInformation.GetIllustrationNo();
            try
            {
                ucPopupChangeStatusSaveNotes.FillReasonDenied(
                    ObjServices.ProductLine.ToString(),
                    Utility.ReasonPredefinieds.DeniedSubscriptionIllustrationReason);
                this.ExcecuteJScript("setTimeout(\"ChangeIllustrationStatus(document.getElementById('btnDeclinedBySubscription'))\",10);");
            }
            catch (Exception ex)
            {
                var msg = ex.GetLastInnerException().Message;
                this.MessageBox(Resources.AErrorOccurredWithIllustrationNo.SFormat(policyNo) + (!msg.SIsNullOrEmpty() ? " - " + msg.RemoveInvalidCharacters().Replace('\'', '\"') : ""), Title: "Error");
            }
        }

        protected void btnApplyPayment_Click(object sender, EventArgs e)
        {
            var addInfo = new AdditionalInfo
            {
                CompanyId = ObjServices.CompanyId,
                Language = (ObjServices.Language == Utility.Language.en ? "en" : "es")
            };

            var btn = (Button)sender;

            var action = new ActionIllustrationPayment
            {
                Corp_Id = ObjServices.Corp_Id,
                Region_Id = ObjServices.Region_Id,
                Country_Id = ObjServices.Country_Id,
                Domesticreg_Id = ObjServices.Domesticreg_Id,
                State_Prov_Id = ObjServices.State_Prov_Id,
                City_Id = ObjServices.City_Id,
                Office_Id = ObjServices.Office_Id,
                Case_Seq_No = ObjServices.Case_Seq_No,
                Hist_Seq_No = ObjServices.Hist_Seq_No,
                ContactEntityID = ObjServices.ContactEntityID,
                ProductLine = ObjServices.ProductLine.ToString()
            };

            btn.Attributes.Add("action", action.ToJSON());

            var data = SecurityPage.GenerateToken(ObjServices.UserID.Value, btn, addInfo);

            if (data.Status)
                Response.Redirect(data.UrlPath, true);
            else
                this.MessageBox(data.errormessage);
        }

        private void InfGen()
        {
            Session["Initialize"] = true;
        }

        protected void btnInspection_Click(object sender, EventArgs e)
        {
            try
            {
                #region Auto
                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                {
                    /*Revisando si ya tiene la direccion de Inspeccion*/

                    //var EmptyInspectionAddress = ListVehicles.Where(x => x.InspectionRequired.GetValueOrDefault()).Any(v => v.InspectionAddress.Trim() == string.Empty);

                    //if (EmptyInspectionAddress)
                    //{
                    //    string qnumber = ucIllustrationInformation.GetIllustrationNo();

                    //    this.ExcecuteJScript("$('#divReason').hide();");
                    //    this.MessageBox(string.Format("Para poder hacer la inspección a la cotización: {0} - {1}", qnumber, Resources.YouMustIndicateInspectionAddressNew), Width: 500, Title: Resources.Warning);
                    //    return;
                    //}

                    /**/

                    int notInspected = ListVehicles.Count(v => v.InspectionRequired.GetValueOrDefault());

                    //Verificar si existe inspeccion previa
                    var vehiclesReview = ObjServices.oVehicleManager.GetVehicleReview(new Vehicle
                    {
                        CorpId = ObjServices.Corp_Id,
                        RegionId = ObjServices.Region_Id,
                        CountryId = ObjServices.Country_Id,
                        DomesticRegId = ObjServices.Domesticreg_Id,
                        StateProvId = ObjServices.State_Prov_Id,
                        CityId = ObjServices.City_Id,
                        OfficeId = ObjServices.Office_Id,
                        CaseSeqNo = ObjServices.Case_Seq_No,
                        HistSeqNo = ObjServices.Hist_Seq_No
                    }).Where(v => v.Inspection.GetValueOrDefault()).ToList();

                    if (ObjServices.IsInspectorQuotRole)
                    {
                        if (notInspected > 0)
                        {
                            InfGen();
                            Response.Redirect("VehicleInspectionForm.aspx");
                        }
                        else
                        {
                            var inspeccionPrevia = vehiclesReview.Any(v => v.ReviewId > 0);
                            if (inspeccionPrevia)
                            {
                                InfGen();
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
                            InfGen();
                            Response.Redirect("VehicleInspectionForm.aspx");
                        }
                        else
                        {
                            if (notInspected > 0)
                            {
                                InfGen();
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
                #endregion

                #region Lineas Aliadas
                else if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                {
                    bool IspectinReq = false;
                    var property = ObjServices.oPropertyManager.GetProperty(new Property.Key
                    {
                        CorpId = ObjServices.Corp_Id,
                        RegionId = ObjServices.Region_Id,
                        CountryId = ObjServices.Country_Id,
                        DomesticregId = ObjServices.Domesticreg_Id,
                        StateProvId = ObjServices.State_Prov_Id,
                        CityId = ObjServices.City_Id,
                        OfficeId = ObjServices.Office_Id,
                        CaseSeqNo = ObjServices.Case_Seq_No,
                        HistSeqNo = ObjServices.Hist_Seq_No
                    }).ToList();

                    if (property != null)
                    {
                        foreach (var item in property) //Recorro el listado de propiedades del key consultado mas arriba para validar si al menos uno de los item requiere inspecion
                        {
                            if (item.RequiresInspection)
                            {
                                IspectinReq = true;
                                break;
                            }
                        }
                    }

                    if (!IspectinReq)
                    {
                        this.MessageBox(Resources.RiskDoesNotRequireInspection, Width: 500, Title: Resources.InformationLabel);
                        return;
                    }
                    else
                    {

                        Response.Redirect("AlliedLinesRiskInspectionForm.aspx");
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                var MessageEx = ex.Message.Replace('\'', '\"').MyRemoveInvalidCharacters();
                (this.Page as BasePage).ErrorDescription = ex.InnerException != null ? ex.InnerException.ToString() : string.Empty;
                var msg = string.Format("{0}  <br> <br> Presione Ok para descargar un archivo con el detalle del error", MessageEx);
                this.CustomDialogMessageWithCallBack(msg, "function(){$('#btnGenerateFileError').click();}", "Error", "", "");
            }
        }

        protected void btnWorkflow_Click(object sender, EventArgs e)
        {
            ucWorkflow.FillData();
        }

        /// <summary>
        /// Visualizacion de cotizacion via thunderhead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeeIllustration_Click(object sender, EventArgs e)
        {
            try
            {
                #region Auto
                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                {
                    //Verificar si todos los vehiculos tienen sus planes
                    //Data de los Vehiculos
                    var dataVehicle = ObjServices.oPolicyManager.GetVehicleInsured(new Policy.Parameter
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
                        UnderwriterId = ObjServices.Agent_LoginId,
                        LanguageId = ObjServices.Language.ToInt(),
                        UserId = ObjServices.UserID
                    });

                    var NoExist = dataVehicle.Where(y => y.ProductTypeDesc == string.Empty).Any();

                    if (NoExist)
                    {
                        this.MessageBox(Resources.VehicleDosntPlan);
                        return;
                    }

                    try
                    {
                        //Mostrar la cotizacion generada via thunderhead
                        //Generar el Documento XML con la data de la cotizazion 
                        var XMLByteArray = ObjServices.GenerateXMLQuotationToThunderhead(ObjServices.Corp_Id,
                                                                                         ObjServices.Region_Id,
                                                                                         ObjServices.Country_Id,
                                                                                         ObjServices.Domesticreg_Id,
                                                                                         ObjServices.State_Prov_Id,
                                                                                         ObjServices.City_Id,
                                                                                         ObjServices.Office_Id,
                                                                                         ObjServices.Case_Seq_No,
                                                                                         ObjServices.Hist_Seq_No,
                                                                                         ServerMaptPath,
                                                                                         templateType: ThunderheadWrap.Service.TemplateType.Cotizacion
                                                                                         );

                        var PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, ThunderheadWrap.Service.TemplateType.Cotizacion);
                        pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                        pdfViewerMyPreviewPDF.DataBind();

                        //Visualizar el pdf
                        hdnShowPreviewPDF.Value = "true";
                        udpQuotationPrev.Update();
                        ModalPopupPDFViewer.Show();
                        this.ExcecuteJScript("$('#popupBhvr_backgroundElement').css('display', 'none');");
                    }
                    catch (Exception ex)
                    {
                        this.MessageBox(string.Format("ExceptionMessage {0} TraceStack {1}", ex.Message, ex.StackTrace).MyRemoveInvalidCharacters());
                    }
                }
                #endregion
                #region Lineas Aliadas
                else if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                {
                    try
                    {
                        //Mostrar la cotizacion generada via thunderhead
                        //Generar el Documento XML con la data de la cotizazion 
                        var XMLByteArray = ObjServices.GenerateXMLQuotationThuderheadProperty(ObjServices.Corp_Id,
                                                                                              ObjServices.Region_Id,
                                                                                              ObjServices.Country_Id,
                                                                                              ObjServices.Domesticreg_Id,
                                                                                              ObjServices.State_Prov_Id,
                                                                                              ObjServices.City_Id,
                                                                                              ObjServices.Office_Id,
                                                                                              ObjServices.Case_Seq_No,
                                                                                              ObjServices.Hist_Seq_No,
                                                                                              ServerMaptPath,
                                                                                              templateType: ThunderheadWrap.Service.TemplateType.CotizacionPropiedad
                                                                                              );

                        var PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray,
                                                                         ThunderheadWrap.Service.TemplateType.CotizacionPropiedad,
                                                                         ThunderheadWrap.Service.BusinessLine.IncendioLineasAliadas,
                                                                          ObjServices.Country == Utility.Country.RepublicaDominicana ?
                                                                             ThunderheadWrap.Service.ContactCountry.RepublicaDominicana
                                                                           : ThunderheadWrap.Service.ContactCountry.ElSalvador
                                                                         );
                        pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                        pdfViewerMyPreviewPDF.DataBind();

                        //Visualizar el pdf
                        hdnShowPreviewPDF.Value = "true";
                        udpQuotationPrev.Update();
                        ModalPopupPDFViewer.Show();
                        this.ExcecuteJScript("$('#popupBhvr_backgroundElement').css('display', 'none');");
                    }
                    catch (Exception ex)
                    {
                        this.MessageBox(string.Format("ExceptionMessage {0} TraceStack {1}", ex.Message, ex.StackTrace).MyRemoveInvalidCharacters());
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }
        }

        /// <summary>
        /// Exportar Arreglo de bytes a pdf
        /// </summary>
        /// <param name="csvFile"></param>
        public void ExportToPdf(byte[] pdfFile, string FileName)
        {
            try
            {
                Response.Clear();
                MemoryStream ms = new MemoryStream(pdfFile);
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".pdf");
                Response.Buffer = true;
                ms.WriteTo(Response.OutputStream);
                Response.End();
            }
            catch (Exception ex)
            {
                this.MessageBox(ex.Message, Title: "Error");
            }
        }

        /// <summary>
        /// Descarga un archivo cualquiera
        /// </summary>
        /// <param name="BytefFile"></param>
        /// <param name="FileName"></param>
        public void ExportToFile(byte[] BytefFile, string FileName)
        {
            try
            {
                Response.Clear();
                MemoryStream ms = new MemoryStream(BytefFile);
                Response.ContentType = "undefined";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
                Response.Buffer = true;
                ms.WriteTo(Response.OutputStream);
                Response.End();
            }
            catch (Exception ex)
            {
                this.MessageBox(ex.Message, Title: "Error");
            }

        }

        public void ShowApplyDiscountPopup()
        {
            mpeApplyDiscount.Show();
        }

        public void HideApplyDiscountPopup()
        {
            this.ExcecuteJScript("setTimeout(\"ClosePopDiscount()\",10);");
            mpeApplyDiscount.Hide();
        }

        protected void btnForeignStatusNotification_Click(object sender, EventArgs e)
        {
            try
            {
                //Visualizar documento de Notificación de Estatus Extranjero
                //Mostrar la cotizacion generada via thunderhead      
                var XMLByteArray = ObjServices.GenerateXMLQuotationToThunderhead(ObjServices.Corp_Id,
                                                                                 ObjServices.Region_Id,
                                                                                 ObjServices.Country_Id,
                                                                                 ObjServices.Domesticreg_Id,
                                                                                 ObjServices.State_Prov_Id,
                                                                                 ObjServices.City_Id,
                                                                                 ObjServices.Office_Id,
                                                                                 ObjServices.Case_Seq_No,
                                                                                 ObjServices.Hist_Seq_No,
                                                                                 ServerMaptPath,
                                                                                 templateType: ThunderheadWrap.Service.TemplateType.NotificacionEstatusExtranjero
                                                                                 );

                var PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, ThunderheadWrap.Service.TemplateType.NotificacionEstatusExtranjero);
                pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                pdfViewerMyPreviewPDF.DataBind();

                //Visualizar el pdf
                hdnShowPreviewPDF.Value = "true";
                udpQuotationPrev.Update();
                ModalPopupPDFViewer.Show();
                this.ExcecuteJScript("$('#popupBhvr_backgroundElement').css('display', 'none');");
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }
        }

        protected void btnForeignStatusNotification_PreRender(object sender, EventArgs e)
        {
            var val1 = ((String.IsNullOrEmpty(ObjServices.Nationality)) ? "" : ObjServices.Nationality.RemoveAccentsWithRegEx());
            var val2 = "República Dominicana".ToLower().RemoveAccentsWithRegEx();

            var isVisible = val1 != val2;
            (sender as LinkButton).Visible = isVisible && ObjServices.ProductLine != Utility.ProductLine.AlliedLines;
        }

        protected void btnCondicionado_Click(object sender, EventArgs e)
        {
            try
            {
                var ServerMaptPathXML = Server.MapPath("~/NewBusiness/XML/");

                byte[] XMLByteArray = null;
                byte[] PDFByteArray = null;
                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                {
                    //Codicionado
                    //Mostrar el Condicionado generado via thunderhead
                    //Generar el Documento XML con la data de la cotizazion 
                    XMLByteArray = ObjServices.GenerateXMLCondicionadoAutoToThunderhead(ObjServices.Corp_Id,
                                                                                           ObjServices.Region_Id,
                                                                                           ObjServices.Country_Id,
                                                                                           ObjServices.Domesticreg_Id,
                                                                                           ObjServices.State_Prov_Id,
                                                                                           ObjServices.City_Id,
                                                                                           ObjServices.Office_Id,
                                                                                           ObjServices.Case_Seq_No,
                                                                                           ObjServices.Hist_Seq_No,
                                                                                           ServerMaptPathXML,
                                                                                           ThunderheadWrap.Service.TemplateType.Condicionado.ToString()
                                                                                           );

                    PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, ThunderheadWrap.Service.TemplateType.Condicionado);
                }
                else if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                {
                    //Mostrar la cotizacion generada via thunderhead
                    //Generar el Documento XML con la data de la cotizazion 
                    XMLByteArray = ObjServices.GenerateXMLQuotationThuderheadProperty(ObjServices.Corp_Id,
                                                                                      ObjServices.Region_Id,
                                                                                      ObjServices.Country_Id,
                                                                                      ObjServices.Domesticreg_Id,
                                                                                      ObjServices.State_Prov_Id,
                                                                                      ObjServices.City_Id,
                                                                                      ObjServices.Office_Id,
                                                                                      ObjServices.Case_Seq_No,
                                                                                      ObjServices.Hist_Seq_No,
                                                                                      ServerMaptPath,
                                                                                      templateType: ThunderheadWrap.Service.TemplateType.Condicionado
                                                                                     );

                    PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray,
                                                                ThunderheadWrap.Service.TemplateType.Condicionado,
                                                                ThunderheadWrap.Service.BusinessLine.IncendioLineasAliadas,
                                                                ObjServices.Country == Utility.Country.RepublicaDominicana ?
                                                                    ThunderheadWrap.Service.ContactCountry.RepublicaDominicana
                                                                    : ThunderheadWrap.Service.ContactCountry.ElSalvador
                                                                );
                }

                pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                pdfViewerMyPreviewPDF.DataBind();

                //Visualizar el pdf
                hdnShowPreviewPDF.Value = "true";
                udpQuotationPrev.Update();
                ModalPopupPDFViewer.Show();
                this.ExcecuteJScript("$('#popupBhvr_backgroundElement').css('display', 'none');");
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }
        }

        protected void btnCondPart_Click(object sender, EventArgs e)
        {
            try
            {
                var ServerMapPathXML = Server.MapPath("~/NewBusiness/XML/");
                byte[] XMLByteArray = null;
                byte[] PDFByteArray;
                ThunderheadWrap.Service.BusinessLine Bl = ThunderheadWrap.Service.BusinessLine.Vehicle;

                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                {
                    //Mostrar el Condiciones particulares generado via thunderhead
                    //Generar el Documento XML con la data de la cotizazion 
                    XMLByteArray = ObjServices.GenerateXMLMarbeteCondicionadoToThunderhead(ObjServices.Corp_Id,
                                                                                           ObjServices.Region_Id,
                                                                                           ObjServices.Country_Id,
                                                                                           ObjServices.Domesticreg_Id,
                                                                                           ObjServices.State_Prov_Id,
                                                                                           ObjServices.City_Id,
                                                                                           ObjServices.Office_Id,
                                                                                           ObjServices.Case_Seq_No,
                                                                                           ObjServices.Hist_Seq_No,
                                                                                           ServerMapPathXML,
                                                                                           ThunderheadWrap.Service.TemplateType.CondicionesParticulares.ToString()
                                                                                           );

                }
                else if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                {
                    XMLByteArray = ObjServices.GenerateXMLParticularConditionPropertyAlliedLinesThuderhead(ObjServices.Corp_Id,
                                                                                              ObjServices.Region_Id,
                                                                                              ObjServices.Country_Id,
                                                                                              ObjServices.Domesticreg_Id,
                                                                                              ObjServices.State_Prov_Id,
                                                                                              ObjServices.City_Id,
                                                                                              ObjServices.Office_Id,
                                                                                              ObjServices.Case_Seq_No,
                                                                                              ObjServices.Hist_Seq_No,
                                                                                              ServerMaptPath,
                                                                                              templateType: ThunderheadWrap.Service.TemplateType.CondicionesParticulares
                                                                                              );

                    //throw new Exception("Este documento no está disponible actualmente. Disculpe los inconvenientes!");
                    Bl = ThunderheadWrap.Service.BusinessLine.IncendioLineasAliadas;
                }

                PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, ThunderheadWrap.Service.TemplateType.CondicionesParticulares, Bl);
                pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                pdfViewerMyPreviewPDF.DataBind();


                //Visualizar el pdf
                hdnShowPreviewPDF.Value = "true";
                udpQuotationPrev.Update();
                ModalPopupPDFViewer.Show();
                this.ExcecuteJScript("$('#popupBhvr_backgroundElement').css('display', 'none');");
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }

        }

        protected void btnEnsosoCesionDerecho_Click(object sender, EventArgs e)
        {
        }

        protected void UpdatePanel_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception ex) { }
        }

        protected void lnkBlackList_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(ObjServices.BlacklistMember))
            {
                this.MessageBox(Resources.BlackListValidationAlert);
                return;
            }

            var PolicyData = ObjServices.oPolicyManager.GetPolicy(ObjServices.Corp_Id,
                                                                         ObjServices.Region_Id,
                                                                         ObjServices.Country_Id,
                                                                         ObjServices.Domesticreg_Id,
                                                                         ObjServices.State_Prov_Id,
                                                                         ObjServices.City_Id,
                                                                         ObjServices.Office_Id,
                                                                         ObjServices.Case_Seq_No,
                                                                         ObjServices.Hist_Seq_No
                                                                        );

            var dataId = ObjServices.oContactManager.GetAllIdDocumentInformation(PolicyData.ContactId, ObjServices.Language.ToInt());
            var RecordId = dataId.FirstOrDefault();

            var ContactData = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, PolicyData.ContactId, ObjServices.Language.ToInt());

            bool hasProblem = false;
            bool _declinarPorBlackList = false;

            var pCedulaOrDriverLicense = RecordId.Id.Replace("-", "").RemoveInvalidCharacters().RemoveAccentsWithRegEx();
            var resultMessage = ObjServices.ValidacionBlackList(PolicyData.PolicyNo, ContactData, pCedulaOrDriverLicense, Utility.BlackListAction.No, ref hasProblem, ref _declinarPorBlackList);
            if (!string.IsNullOrEmpty(resultMessage))
            {
                this.MessageBox(resultMessage);
            }
        }

        protected void lnkDataCredito_Click(object sender, EventArgs e)
        {
            try
            {
                //Objeto de la Data de la Poliza
                var PolicyData = ObjServices.oPolicyManager.GetPolicy(ObjServices.Corp_Id,
                                                                      ObjServices.Region_Id,
                                                                      ObjServices.Country_Id,
                                                                      ObjServices.Domesticreg_Id,
                                                                      ObjServices.State_Prov_Id,
                                                                      ObjServices.City_Id,
                                                                      ObjServices.Office_Id,
                                                                      ObjServices.Case_Seq_No,
                                                                      ObjServices.Hist_Seq_No);
                var KRiesgos = new[]
                {
                    Utility.TipoRiesgo.RA,
                    Utility.TipoRiesgo.RB,
                    Utility.TipoRiesgo.RM
                };

                //Verificar si ya se hizo la verificacion crediticia
                var ContactData = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, PolicyData.ContactId, ObjServices.Language.ToInt());
                var keyRiesgo = ContactData.TipoRiesgoNameKey == "N/A" || string.IsNullOrEmpty(ContactData.TipoRiesgoNameKey) ? "NONE" : ContactData.TipoRiesgoNameKey;

                if ((keyRiesgo == null))
                {
                    this.MessageBox(Resources.DontViewInfoCredit);
                    return;
                }

                if (!KRiesgos.Contains((Utility.TipoRiesgo)Enum.Parse(typeof(Utility.TipoRiesgo), keyRiesgo)))
                {
                    this.MessageBox(Resources.DontViewInfoCredit);
                    return;
                }

                //Id Doc
                var dataId = ObjServices.oContactManager.GetAllIdDocumentInformation(PolicyData.ContactId, ObjServices.Language.ToInt());
                var RecordId = dataId.FirstOrDefault();

                if (ObjServices.Country == Utility.Country.RepublicaDominicana)
                {
                    #region TransUnion
                    if (RecordId.ContactIdType == Utility.IdentificationType.ID.ToInt() ||
                        RecordId.ContactIdType == Utility.IdentificationType.DriverLicense.ToInt())
                    {
                        var pCedulaOrDriverLicense = RecordId.Id.Replace("-", "").RemoveInvalidCharacters().RemoveAccentsWithRegEx();
                        var vProtocol = "";

                        try
                        {
                            vProtocol = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PageProtocol").ConfigurationValue;
                        }
                        catch (Exception)
                        {
                            vProtocol = "'http://";
                        }

                        var url = vProtocol + System.Web.HttpContext.Current.Request.Url.Authority + "/NewBusiness/Pages/Transunion.aspx?data={0}'";
                        var func = string.Format("$('#ifrmTransunion').attr('src'," + url + ")", HttpUtility.UrlEncode(Utility.Encrypt_Query(pCedulaOrDriverLicense + "|" + keyRiesgo + "|N")));
                        this.ExcecuteJScript(func);
                        hdnPopTransunion.Value = "true";
                        mpeTransunion.Show();
                    }
                    #endregion
                }
                else if (ObjServices.Country == Utility.Country.ElSalvador)
                {
                    #region Equifax
                    if (RecordId.ContactIdType == Utility.IdentificationType.ID.ToInt() ||
                    RecordId.ContactIdType == Utility.IdentificationType.DriverLicense.ToInt())
                    {
                        var pCedulaOrDriverLicense = RecordId.Id.Replace("-", "").RemoveInvalidCharacters().RemoveAccentsWithRegEx();

                        var url = "'http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/NewBusiness/Pages/Equifax.aspx?data={0}'";
                        var func = string.Format("$('#ifrmTransunion').attr('src'," + url + ")", HttpUtility.UrlEncode(Utility.Encrypt_Query(pCedulaOrDriverLicense + "|" + keyRiesgo)));

                        this.ExcecuteJScript(func);

                        var func1 = "$('#lblTitle').text('" + "Reporte Crediticio " + ContactData.FullName + "')";
                        this.ExcecuteJScript(func1);

                        hdnPopTransunion.Value = "true";
                        mpeTransunion.Show();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }
        }

        protected void btnOnBase_Click(object sender, EventArgs e)
        {
            try
            {
                var illustrationData = ObjServices.getillustrationData();
                var contactData = ObjServices.oContactManager.GetContact(illustrationData.CorpId, illustrationData.ContactId, ObjServices.Language.ToInt());
                var RepresentanteLegal = contactData != null ? contactData.ManagerName : string.Empty;
                ObjServices.GenerateOnBaseFiles(ObjServices.Corp_Id,
                                                ObjServices.Region_Id,
                                                ObjServices.Country_Id,
                                                ObjServices.Domesticreg_Id,
                                                ObjServices.State_Prov_Id,
                                                ObjServices.City_Id,
                                                ObjServices.Office_Id,
                                                ObjServices.Case_Seq_No,
                                                ObjServices.Hist_Seq_No,
                                                false,
                                                Server.MapPath("~/NewBusiness/XML/"),
                                                Financed: illustrationData.Financed.GetValueOrDefault(),
                                                LoanNumber: ObjServices.LoanPetitionNo,
                                                CopyATLFile: true,
                                                CopyKCOFile: false,
                                                RepresentanteLegal: RepresentanteLegal
                                               );

                this.MessageBox(Resources.SaveSucessfully);
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }
        }

        protected void btnGenerateFileError_Click(object sender, EventArgs e)
        {
            var ErrorDesc = base.ErrorDescription;
            base.ErrorDescription = string.Empty;
            base.ExportToTxt(ASCIIEncoding.Default.GetBytes(ErrorDesc));
        }

        protected void btnOnBaseKCO_Click(object sender, EventArgs e)
        {
            try
            {
                var illustrationData = ObjServices.getillustrationData();
                var contactData = ObjServices.oContactManager.GetContact(illustrationData.CorpId, illustrationData.ContactId, ObjServices.Language.ToInt());
                var RepresentanteLegal = contactData != null ? contactData.ManagerName : string.Empty;
                //Copiar los archivo en Onbase
                ObjServices.GenerateOnBaseFiles(ObjServices.Corp_Id,
                                                ObjServices.Region_Id,
                                                ObjServices.Country_Id,
                                                ObjServices.Domesticreg_Id,
                                                ObjServices.State_Prov_Id,
                                                ObjServices.City_Id,
                                                ObjServices.Office_Id,
                                                ObjServices.Case_Seq_No,
                                                ObjServices.Hist_Seq_No,
                                                false,
                                                Server.MapPath("~/NewBusiness/XML/"),
                                                Financed: illustrationData.Financed.GetValueOrDefault(),
                                                LoanNumber: ObjServices.LoanPetitionNo,
                                                CopyATLFile: false,
                                                CopyKCOFile: true,
                                                RepresentanteLegal: RepresentanteLegal
                                               );

                this.MessageBox(Resources.SaveSucessfully);
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }
        }

        protected void lnkEndorsementClarifyingDepre_Click(object sender, EventArgs e)
        {
            byte[] XMLByteArray = null;
            byte[] PDFByteArray = null;

            try
            {
                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                {
                    var Template = (sender as LinkButton).ID == "lnkEndorsementOfDeductibleApplication" ? ThunderheadWrap.Service.TemplateType.EnsosoAclaratorioAplicacionDeducible
                                                                                                        : ThunderheadWrap.Service.TemplateType.EndosoAclaratorioDepreciacionMinPT;

                    XMLByteArray = ObjServices.GenerateXMLToEndosoAclaratorioDeprecion(ObjServices.Corp_Id,
                                                                                       ObjServices.Region_Id,
                                                                                       ObjServices.Country_Id,
                                                                                       ObjServices.Domesticreg_Id,
                                                                                       ObjServices.State_Prov_Id,
                                                                                       ObjServices.City_Id,
                                                                                       ObjServices.Office_Id,
                                                                                       ObjServices.Case_Seq_No,
                                                                                       ObjServices.Hist_Seq_No,
                                                                                       Template
                                                                                       );

                    PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, Template);
                    pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                    pdfViewerMyPreviewPDF.DataBind();

                    //Visualizar el pdf
                    hdnShowPreviewPDF.Value = "true";
                    udpQuotationPrev.Update();
                    ModalPopupPDFViewer.Show();
                    this.ExcecuteJScript("$('#popupBhvr_backgroundElement').css('display', 'none');");
                }
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }
        }

        protected void lnkEndorsementClarifyingEconovida_Click(object sender, EventArgs e)
        {
            byte[] XMLByteArray = null;
            byte[] PDFByteArray = null;

            try
            {
                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                {
                    var Template = ThunderheadWrap.Service.TemplateType.EnsosoAclaratorioEconoVida;

                    XMLByteArray = ObjServices.GenerateXMLToEndosoAclaratorioEconoVida(ObjServices.Corp_Id,
                                                                                       ObjServices.Region_Id,
                                                                                       ObjServices.Country_Id,
                                                                                       ObjServices.Domesticreg_Id,
                                                                                       ObjServices.State_Prov_Id,
                                                                                       ObjServices.City_Id,
                                                                                       ObjServices.Office_Id,
                                                                                       ObjServices.Case_Seq_No,
                                                                                       ObjServices.Hist_Seq_No,
                                                                                       Template
                                                                                       );

                    PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, Template);
                    pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                    pdfViewerMyPreviewPDF.DataBind();

                    //Visualizar el pdf
                    hdnShowPreviewPDF.Value = "true";
                    udpQuotationPrev.Update();
                    ModalPopupPDFViewer.Show();
                    this.ExcecuteJScript("$('#popupBhvr_backgroundElement').css('display', 'none');");
                }
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }
        }
    }
}