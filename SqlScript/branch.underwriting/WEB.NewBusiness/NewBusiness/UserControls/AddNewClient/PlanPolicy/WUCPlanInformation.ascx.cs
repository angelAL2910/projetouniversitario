﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;
using DI.UnderWriting.Interfaces;
using DI.UnderWriting;
using System.Reflection;
using System.Text;
using System.Web.UI.HtmlControls;
using RESOURCE.UnderWriting.NewBussiness;
using System.Globalization;
using Entity.UnderWriting.Entities;

namespace WEB.NewBusiness.NewBusiness.UserControls.PlanPolicy
{
    public partial class WUCPlanInformation : UC, IUC
    {
        #region Controls
        public Panel opnDesignatedPensioner;
        public UpdatePanel oudpDesignatedPensioner;
        public WUCDesignatedPensionerInformation oWUCDesignatedPensionerInformation;

        #endregion

        #region Variables
        public int CorpId;
        public int RegionId;
        public int CountryId;
        public int DomesticregId;
        public int StateProvId;
        public int CityId;
        public int OfficeId;
        public int CaseSeqNo;
        public int HistSeqNo;
        decimal? ReturnOfPremiumBk = 0;

        #endregion

        #region Controles
        public DropDownList ddlFamilyProduct;
        public DropDownList ddlProductName;
        public DropDownList ddlPlanType;
        public DropDownList ddlCurrency;
        public DropDownList ddlFrequencyofPayment;
        //public TextBox txtContributionPeriod;
        public DropDownList ddlContributionPeriod;
        public DropDownList ddlRetirementPeriod;
        public DropDownList ddlMaternityAndNewBornComplication;
        public DropDownList ddlOrganTransplant;
        public DropDownList ddlDefermentPeriod;
        public DropDownList ddlRisk;
        public DropDownList ddlPerThousand;
        public DropDownList ddlOtherInsured;
        public DropDownList ddlDependents;
        public DropDownList ddlRepatriation;
        public DropDownList ddlInitialContribution;
        public DropDownList ddlInvestmentProfile;
        public DropDownList ddlDeducibleType;
        public DropDownList ddlDeducible;
        public TextBox txtAmount;

        public Button btnPProfile;
        public TextBox txtStudentName = null;
        public TextBox txtAge = null;
        public DropDownList ddlEducationPeriod = null;
        public DropDownList ddlCritialIllness = null;
        public DropDownList ddlFinancialGlobal = null;
        public DropDownList ddlLote = null;
        public DropDownList ddlLoteType = null;
        public DropDownList ddlSpecialPayment;
        /// <summary>
        /// Papyment special depend of ddSpecialPayment
        /// </summary>
        public TextBox txtSpecialPayment;
        /// <summary>
        /// Tasa de interes financing rate
        /// </summary>
        public TextBox txtFinancingRate;
        DropDownList ddlDestinyFund;
        public DropDownList ddlFinancialInstitution;
        public TextBox txtAmount2 = null;
        public TextBox txtEffectiveDate = null;
        public TextBox txtAtAge = null;
        public TextBox txtAdditionalTermInsuranceInsuredAmount = null;
        public TextBox txtCritialIllnessInsuredAmount = null;
        public TextBox txtAccidentalDeathInsuredAmount = null;
        public TextBox txtSpouseOtherInsured = null;
        public TextBox txtUntilAge = null;
        public DropDownList ddlUntilAge = null;
        public TextBox txtYearsSpouseOther = null;
        public Panel pnPlanType = null;
        public Panel pnInvestmentProfile = null;
        public HiddenField hdnDeductibleTypeID = null;
        public TextBox txtAnnualPremium = null;
        #region Controles Traducibles
        Literal planinformationLabel;
        HtmlGenericControl FamilyProductLabel;
        HtmlGenericControl PlanNameLabel;
        HtmlGenericControl PlanTypeLabel;
        HtmlGenericControl CurrencyLabel;
        HtmlGenericControl FrequencyOfPaymentLabel;
        HtmlGenericControl ContributionPeriodLabel;
        HtmlGenericControl RetirementPeriodLabel;
        HtmlGenericControl DefermentPeriodLabel;
        HtmlGenericControl InitialContributionPeriodLabel;
        HtmlGenericControl AmountLabel;
        HtmlGenericControl Amount2Label;
        HtmlGenericControl HaveDesignatedPensionerLabel;
        HtmlGenericControl YesLabel;
        HtmlGenericControl NoLabel;
        HtmlGenericControl InvestmentProfileLabel;
        HtmlGenericControl EducationPeriodLabel;
        Literal ltStudentInformation;
        HtmlGenericControl StudentName;
        HtmlGenericControl DateOfBirthLabel;
        HtmlGenericControl ContributionTypeLabel;
        HtmlGenericControl YearsLabel;
        HtmlGenericControl InsuredAmountLabel;
        HtmlGenericControl SpouseOtherInsuredLabel;
        HtmlGenericControl InsuredSpouseOtherInsuredAmountLabel;
        HtmlGenericControl YearsspouseOtherLabel;
        HtmlGenericControl AdditionalTermInsuranceLabel;
        HtmlGenericControl AdditonalTermInsuredAmount;
        HtmlGenericControl AccidentalDeathBenefitsLabel;
        HtmlGenericControl FinancialGoal;
        HtmlGenericControl AmountGoal;
        HtmlGenericControl AtAgeGoal;
        HtmlGenericControl AdditionalTermInsuranceInsuredAmount;
        HtmlGenericControl CritialIllnessInsuredAmount;
        HtmlGenericControl CriticalIllnessLabel;
        HtmlGenericControl OthersInsuredsLabel;
        HtmlGenericControl Repatriation;
        HtmlGenericControl Lote;
        HtmlGenericControl LoteType;
        HtmlGenericControl lblDeducible;
        HtmlGenericControl lblEffectiveDate;
        HtmlGenericControl DepositAmount;
        HtmlGenericControl lblDependents;
        HtmlGenericControl MaternityAndNewBornComplication;
        HtmlGenericControl OrganTransplant;
        Literal ltRidersLabel;

        #endregion

        DropDownList ddlContributionType;
        DropDownList ddlAccidentalDeathBenefits;
        DropDownList ddlSpouseOtherInsured;
        DropDownList ddlAdditionalTermInsurance;

        public UserControl Controles;
        WUCFieldFooter oWUCFieldFooter;



        //Bmarroquin 19/01/2017 a raiz de tropicalizacion ESA, se agregan los controles para mostrar la cobertura Basica: "Vida Basico" 
        HtmlGenericControl lblVidaBasico;
        HtmlGenericControl lblMontoVidaBasico;
        public DropDownList ddlVidaBasico = null;
        public TextBox txtMontoAseguradoCorBasica = null;
        // Fin cambios 19/01/2017 a raiz de tropicalizacion ESA

        #endregion

        public void edit() { }

        public void LoadDataFromIllustration(Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetail dataPlan)
        {
            var frequency = 0;
            setControls();
            setContainer();
            var PlanGroupCode = dataPlan.PlanGroupCode;
            var FamilyProduct = string.Empty;

            //Seleccionar la familia de productos
            if (dataPlan.PlanGroup == "Term Insurance")
            {
                FamilyProduct = ObjServices.Language == Utility.Language.en ?
                                                                              dataPlan.PlanGroup == "Term Insurance" ? "Life Insurance" : dataPlan.PlanGroup
                                                                            : dataPlan.PlanGroup == "Term Insurance" ? "Vida" : dataPlan.PlanGroup;
            }
            else
                if (dataPlan.PlanGroup == "Funeral")
                {
                    FamilyProduct = ObjServices.Language == Utility.Language.en ?
                                                                                  dataPlan.PlanGroup == "Funeral" ? "Funeral" : dataPlan.PlanGroup
                                                                                : dataPlan.PlanGroup == "Funeral" ? "Funerario" : dataPlan.PlanGroup;
                }

            var FamilyProductValue = ddlFamilyProduct.Items.FindByText(FamilyProduct).Value;
            ddlFamilyProduct.SelectIndexByValueJSON(FamilyProductValue);
            ddlFamilyProduct_SelectedIndexChanged(ddlFamilyProduct, null);

            //Seleccionar el producto
            var ProductValue = ddlProductName.Items.FindByText(dataPlan.Product).Value;
            var NameKeyProduct = Utility.deserializeJSON<Utility.itemProduct>(ProductValue);
            ddlProductName.SelectIndexByValueJSON(ProductValue);
            ddlProductName_SelectedIndexChanged(ddlProductName, null);

            //Seleccionar el periodo de contrinucion
            if (!ddlContributionPeriod.isNullReferenceControl())
                ddlContributionPeriod.SelectIndexByValue(dataPlan.ContributionPeriod.ToString());

            //Seleccionar la frecuencia de pago
            if (!ddlFrequencyofPayment.isNullReferenceControl())
            {
                var Freq = string.Empty;

                switch (dataPlan.FrequencyType)
                {
                    case "Quarterly":
                        Freq = ObjServices.Language == Utility.Language.en ? "Quarterly" : "Trimestral";
                        frequency = 4;
                        break;
                    case "Monthly":
                        Freq = ObjServices.Language == Utility.Language.en ? "Monthly" : "Mensual";
                        frequency = 12;
                        break;
                    case "Annual":
                        Freq = ObjServices.Language == Utility.Language.en ? "Annual" : "Anual";
                        frequency = 1;
                        break;
                    case "Semiannual":
                    case "Semi Annual":
                        Freq = ObjServices.Language == Utility.Language.en ? "Semiannual" : "Semestral";
                        frequency = 2;
                        break;
                    default:
                        break;
                }

                var FrequencyofPaymentValue = ddlFrequencyofPayment.Items.FindByText(Freq).Value;
                ddlFrequencyofPayment.SelectIndexByValue(FrequencyofPaymentValue);
            }

            //Seleccionar el contribution type 
            if (!ddlContributionType.isNullReferenceControl())
            {
                var ContributionType = dataPlan.ContributionTypeDescCode.ToUpper();

                for (int i = 0; i <= ddlContributionType.Items.Count; i++)
                {

                    var ConType = ddlContributionType.Items[i].Text.ToUpper() == "Numero de Años".ToUpper() ? "NUMBER OF YEARS"
                                                                                                            : ddlContributionType.Items[i].Text.ToUpper();

                    if (ConType == ContributionType)
                    {
                        ddlContributionType.SelectedIndex = i;
                        break;
                    }
                }
            }

            //Seleccionar la moneda
            if (!ddlCurrency.isNullReferenceControl())
            {
                var CurrencyValue = ddlCurrency.Items.FindByText(dataPlan.CurrencyCode).Value;
                ddlCurrency.SelectIndexByValue(CurrencyValue);
            }

            if (!ddlInitialContribution.isNullReferenceControl())
            {
                if (dataPlan.InitialContribution > 0)
                {
                    ddlInitialContribution.SelectIndexByValue("1");
                    ddlInitialContribution_SelectedIndexChanged(ddlInitialContribution, null);
                    txtAmount.Text = dataPlan.InitialContribution.ToString();
                }
            }

            if (!ddlRepatriation.isNullReferenceControl())
                ddlRepatriation.SelectIndexByValue((dataPlan.Repatriacion == "Y") ? "1" : "0");

            //Riders
            if (!ddlAccidentalDeathBenefits.isNullReferenceControl())
            {
                ddlAccidentalDeathBenefits.SelectIndexByValue(dataPlan.RiderAdb == "Y" ? "1" : "2");
                ddlManageRidersDropDowns_SelectedIndexChanged(ddlAccidentalDeathBenefits, null);
                txtAccidentalDeathInsuredAmount.Text = dataPlan.RiderAdbAmount.ToFormatNumeric();
            }

            //Bmarroquin 25-01-2017 Cambios a raiz de tropicalizacion se agrega la carga de los riders desde la cotizacion 

            if (!ddlOtherInsured.isNullReferenceControl()) //Esto es Gastos funerarios
            {
                ddlOtherInsured.SelectIndexByValue(dataPlan.RiderTerm == "Y" ? "1" : "2");
                ddlManageRidersDropDowns_SelectedIndexChanged(ddlOtherInsured, null);
                txtAdditionalTermInsuranceInsuredAmount.Text = dataPlan.RiderTermAmount.ToFormatNumeric();
            }

            if (!ddlCritialIllness.isNullReferenceControl())
            {
                ddlCritialIllness.SelectIndexByValue(dataPlan.RiderCi == "Y" ? "1" : "2");
                ddlManageRidersDropDowns_SelectedIndexChanged(ddlCritialIllness, null);
                txtCritialIllnessInsuredAmount.Text = dataPlan.RiderCiAmount.ToFormatNumeric();
            }
            //  **********    Fin Cambios  25-01-2017   ******

            var dataAdditionalInsureds = ObjIllustrationServices.oIllusDataManager.GetCustomerPlanPartnerInsurance(dataPlan.CustomerPlanNo.Value);

            if (dataAdditionalInsureds != null)
            {
                if (!ddlSpouseOtherInsured.isNullReferenceControl())
                {
                    ddlSpouseOtherInsured.SelectIndexByValue("1");
                    ddlManageRidersDropDowns_SelectedIndexChanged(ddlSpouseOtherInsured, null);
                    txtSpouseOtherInsured.Text = dataAdditionalInsureds.InsuredAmount.ToFormatNumeric();
                    txtYearsSpouseOther.Text = dataAdditionalInsureds.UntilAge.ToString();
                    oWUCDesignatedPensionerInformation.LoadDataFromIllustration(dataAdditionalInsureds);
                    oWUCDesignatedPensionerInformation.FindControl("hdnValidateFormDesignatedPensionerOrAddicionalInsured");
                    oWUCDesignatedPensionerInformation.setHiddend("true");

                    int? contactType = null;

                    switch (NameKeyProduct.Product)
                    {
                        case Utility.ProductBehavior.Horizon:
                        case Utility.ProductBehavior.Axys:
                        case Utility.ProductBehavior.EduPlan:
                        case Utility.ProductBehavior.Scholar:
                            contactType = Utility.ContactRoleIDType.DesignatedPensioner.ToInt();
                            break;
                        case Utility.ProductBehavior.CompassIndex:
                        case Utility.ProductBehavior.Legacy:
                        case Utility.ProductBehavior.Sentinel:
                        case Utility.ProductBehavior.Lighthouse:
                        case Utility.ProductBehavior.Guardian:
                        case Utility.ProductBehavior.GuardianPlus:
                        case Utility.ProductBehavior.Orion:
                        case Utility.ProductBehavior.OrionPlus:
                            contactType = Utility.ContactRoleIDType.AddicionalInsured.ToInt();
                            break;
                        case Utility.ProductBehavior.Luminis:
                        case Utility.ProductBehavior.LuminisVIP:
                        case Utility.ProductBehavior.Exequium:
                        case Utility.ProductBehavior.ExequiumVIP:
                            contactType = Utility.ContactRoleIDType.IncludedFamiliar.ToInt();
                            break;
                        default:
                            break;
                    }

                    oWUCDesignatedPensionerInformation.ContactRoleTypeID = contactType.Value;
                }
            }

            if (!ddlAdditionalTermInsurance.isNullReferenceControl())
            {
                ddlAdditionalTermInsurance.SelectIndexByValue(dataPlan.RiderTerm == "Y" ? "1" : "2");
                ddlManageRidersDropDowns_SelectedIndexChanged(ddlAdditionalTermInsurance, null);
                txtAdditionalTermInsuranceInsuredAmount.Text = dataPlan.RiderTermAmount.ToFormatNumeric();
            }

            //Bmarroquin 19-01-2017 cambio a raiz de la tropicalizacion de ESA, se agregan las rutinas siguientes...
            if (!txtMontoAseguradoCorBasica.isNullReferenceControl())
            {
                txtMontoAseguradoCorBasica.Text = dataPlan.InsuredAmount.ToFormatNumeric();
            }
            //Fin cambios Bmarroquin 19-01-2017

                //Binding Field Footer 
                oWUCFieldFooter.setControls();

            if (!oWUCFieldFooter.txtPeriodicPremium.isNullReferenceControl())
                oWUCFieldFooter.txtPeriodicPremium.Text = dataPlan.PremiumAmount.ToFormatNumeric();

            if (!oWUCFieldFooter.txtInsuredAmount.isNullReferenceControl())
                oWUCFieldFooter.txtInsuredAmount.Text = dataPlan.InsuredAmount.ToFormatNumeric();

            if (!oWUCFieldFooter.txtTargetAnnualPremium.isNullReferenceControl())
                oWUCFieldFooter.txtTargetAnnualPremium.Text = dataPlan.TargetPremium.ToFormatNumeric();

            if (!oWUCFieldFooter.txtReturnofPremium.isNullReferenceControl())
            {
                var initialcontributionamount = dataPlan.InitialContribution;
                var contributionperiod = dataPlan.ContributionPeriod;
                var premiumamount = dataPlan.PremiumAmount;
                //var ReturnOfPremium = initialcontributionamount + (frequency * contributionperiod * premiumamount);
                var ReturnOfPremium = initialcontributionamount + (frequency * contributionperiod * (int)Math.Floor(premiumamount));//Se hace con floor y no ceiling para que redondee al entero menor
                oWUCFieldFooter.txtReturnofPremium.Text = ReturnOfPremium.ToFormatNumeric();
            }

            //Bmarroquin 13-01-2017 cambio a raiz de la tropicalizacion de ESA, se manda la cotizacion anual y la cotizacion con tax q es cero
            if (!oWUCFieldFooter.txtAnnualPremium.isNullReferenceControl())
                oWUCFieldFooter.txtAnnualPremium.Text = dataPlan.AnnualizedPremium.ToFormatNumeric();

            if (!oWUCFieldFooter.txtAnnualPremiumWithTax.isNullReferenceControl())
                oWUCFieldFooter.txtAnnualPremiumWithTax.Text = dataPlan.AnnualizedPremium.ToFormatNumeric();

            //Bmarroquin 07-04-2017 Fix a Issue se cambia el numero de cotizacion cuando se adjunta la cotizacion de IllusData...
            hdnNumCotizacionIllusData.Value = dataPlan.DispIllustrationNo;

            //Bmarroquin 28-04-2017 Add Campo Monto Recargo por Fraccionamiento a solicitud de SSF
            if (!oWUCFieldFooter.txtFraccionamiento.isNullReferenceControl())
                oWUCFieldFooter.txtFraccionamiento.Text = dataPlan.FractionSurcharge.ToFormatNumeric();

            //Bmarroquin 05-05-2017 Add Campo Prima Comercial a solicitud de SSF
            if (!oWUCFieldFooter.txtPrimaAnualNeta.isNullReferenceControl())
                oWUCFieldFooter.txtPrimaAnualNeta.Text = dataPlan.NetAnnualPremium.ToFormatNumeric(); 

            udpPlanInformation.Update();

        }

        public void setVariables()
        {
            CorpId = ObjServices.Corp_Id;
            RegionId = ObjServices.Region_Id;
            CountryId = ObjServices.Country_Id;
            DomesticregId = ObjServices.Domesticreg_Id;
            StateProvId = ObjServices.State_Prov_Id;
            CityId = ObjServices.City_Id;
            OfficeId = ObjServices.Office_Id;
            CaseSeqNo = ObjServices.Case_Seq_No;
            HistSeqNo = ObjServices.Hist_Seq_No;
        }

        public void setControls()
        {
            switch (hfSelectControls.Value)
            {
                case "VHorizon":
                    Controles = UCHorizon;
                    break;
                case "VAxy":
                    Controles = UCAxy;
                    break;
                case "VEduplan":
                    Controles = UCEduplan;
                    break;
                case "VScholar":
                    Controles = UCScholar;
                    break;
                case "VCompassIndex":
                    Controles = UCCompassIndex;
                    break;
                case "VLegacy":
                    Controles = UCLegacy;
                    break;
                case "VSentinel":
                    Controles = UCSentinel;
                    break;
                case "VLightHouse":
                    Controles = UCLightHouse;
                    break;
                case "VFunerarios":
                    Controles = UCFunerarios;
                    break;
                case "VSelect":
                    Controles = UCSelect;
                    break;
                case "VElite":
                    Controles = UCElite;
                    break;
                case "VFortis":
                    Controles = UCFortis;
                    break;
                case "VSerenity":
                    Controles = UCSerenity;
                    break;
                case "VAsistencia30dias":
                    Controles = UCAsistenciaalViajeroAnual30diascontinuos;
                    break;
                case "VAsistencia60dias":
                    Controles = UCAsistenciaalViajeroAnual60díascontinuos;
                    break;
                case "VAsistencia90dias":
                    Controles = UCAsistenciaalViajerohasta90dias;
                    break;
                default:
                    Controles = UCBasicPlan;
                    break;
            }

            var bodyContent = this.Page.Master.FindControl("bodyContent");

            if (!ObjServices.IsDataReviewMode)
                oWUCFieldFooter = bodyContent.FindControl("PlanPolicyContainer").FindControl("WUCFieldFooter") as WUCFieldFooter;
            else
                if (ObjServices.IsDataReviewMode && getisView)
                    oWUCFieldFooter = bodyContent.FindControl("WUCView").FindControl("PlanPolicyContainer").FindControl("WUCFieldFooter") as WUCFieldFooter;
                else if (ObjServices.IsDataReviewMode)
                    oWUCFieldFooter = bodyContent.FindControl("DReviewContainer").FindControl("PlanPolicyContainer").FindControl("WUCFieldFooter") as WUCFieldFooter;

            /*BUSCO LOS CONTRLES QUE QUIERO GUARDAR*/
            pnPlanType = ((Panel)Controles.FindControl("pnPlanType"));
            pnInvestmentProfile = ((Panel)Controles.FindControl("pnInvestmentProfile"));
            ddlProductName = ((DropDownList)Controles.FindControl("ddlProductName"));
            ddlFamilyProduct = ((DropDownList)Controles.FindControl("ddlFamilyProduct"));
            ddlDefermentPeriod = ((DropDownList)Controles.FindControl("ddlDefermentPeriod"));
            ddlPlanType = ((DropDownList)Controles.FindControl("ddlPlanType"));
            txtEffectiveDate = ((TextBox)Controles.FindControl("txtEffectiveDate"));
            btnPProfile = ((Button)Controles.FindControl("btnPprofile"));
            ddlCurrency = ((DropDownList)Controles.FindControl("ddlCurrency"));
            ddlSpecialPayment = ((DropDownList)Controles.FindControl("ddlSpecialPayment"));
            txtSpecialPayment = ((TextBox)Controles.FindControl("txtSpecialPayment"));
            txtFinancingRate = ((TextBox)Controles.FindControl("txtFinancingRate"));
            ddlDestinyFund = ((DropDownList)Controles.FindControl("ddlDestinyFund"));
            ddlFinancialInstitution = ((DropDownList)Controles.FindControl("ddlFinancialInstitution"));
            ddlFrequencyofPayment = ((DropDownList)Controles.FindControl("ddlFrequencyofPayment"));
            //txtContributionPeriod = ((TextBox)Controles.FindControl("txtContributionPeriod"));
            ddlContributionPeriod = ((DropDownList)Controles.FindControl("ddlContributionPeriod"));
            ddlRetirementPeriod = ((DropDownList)Controles.FindControl("ddlRetirementPeriod"));
            ddlCritialIllness = ((DropDownList)Controles.FindControl("ddlCritialIllness"));
            txtCritialIllnessInsuredAmount = ((TextBox)Controles.FindControl("txtCritialIllnessInsuredAmount"));
            ddlRisk = ((DropDownList)Controles.FindControl("ddlRisk"));
            ddlPerThousand = ((DropDownList)Controles.FindControl("ddlPerThousand"));
            ddlInitialContribution = ((DropDownList)Controles.FindControl("ddlInitialContribution"));
            txtAmount = ((TextBox)Controles.FindControl("txtAmount"));
            ddlEducationPeriod = ((DropDownList)Controles.FindControl("ddlEducationPeriod"));
            ddlInvestmentProfile = ((DropDownList)Controles.FindControl("ddlInvestmentProfile"));
            ddlLoteType = ((DropDownList)Controles.FindControl("ddlLoteType"));
            ddlOtherInsured = ((DropDownList)Controles.FindControl("ddlOtherInsured"));
            ddlDependents = ((DropDownList)Controles.FindControl("ddlDependents"));
            ddlMaternityAndNewBornComplication = ((DropDownList)Controles.FindControl("ddlMaternityAndNewBornComplication"));
            ddlOrganTransplant = ((DropDownList)Controles.FindControl("ddlOrganTransplant"));
            ddlRepatriation = ((DropDownList)Controles.FindControl("ddlRepatriation"));
            ddlLote = ((DropDownList)Controles.FindControl("ddlLote"));
            ddlAccidentalDeathBenefits = ((DropDownList)Controles.FindControl("ddlAccidentalDeathBenefits"));
            ddlSpouseOtherInsured = ((DropDownList)Controles.FindControl("ddlSpouseOtherInsured"));
            ddlAdditionalTermInsurance = ((DropDownList)Controles.FindControl("ddlAdditionalTermInsurance"));
            ddlInitialContribution = ((DropDownList)Controles.FindControl("ddlInitialContribution"));
            ddlContributionType = ((DropDownList)Controles.FindControl("ddlContributionType"));
            ddlFinancialGlobal = ((DropDownList)Controles.FindControl("ddlFinancialGlobal"));
            txtAtAge = ((TextBox)Controles.FindControl("txtAtAge"));
            txtAmount2 = ((TextBox)Controles.FindControl("txtAmount2"));
            txtUntilAge = ((TextBox)Controles.FindControl("txtUntilAge"));
            ddlUntilAge = ((DropDownList)Controles.FindControl("ddlUntilAge"));
            txtYearsSpouseOther = ((TextBox)Controles.FindControl("txtYearsSpouseOther"));
            txtAccidentalDeathInsuredAmount = ((TextBox)Controles.FindControl("txtAccidentalDeathInsuredAmount"));
            txtSpouseOtherInsured = ((TextBox)Controles.FindControl("txtSpouseOtherInsured"));
            txtCritialIllnessInsuredAmount = ((TextBox)Controles.FindControl("txtCritialIllnessInsuredAmount"));
            txtAdditionalTermInsuranceInsuredAmount = ((TextBox)Controles.FindControl("txtAdditionalTermInsuranceInsuredAmount"));
            planinformationLabel = (Literal)Controles.FindControl("planinformation");
            FamilyProductLabel = (HtmlGenericControl)Controles.FindControl("FamilyProduct");
            PlanNameLabel = (HtmlGenericControl)Controles.FindControl("PlanName");
            PlanTypeLabel = (HtmlGenericControl)Controles.FindControl("PlanType");
            CurrencyLabel = (HtmlGenericControl)Controles.FindControl("Currency");
            FrequencyOfPaymentLabel = (HtmlGenericControl)Controles.FindControl("FrequencyOfPayment");
            ContributionPeriodLabel = (HtmlGenericControl)Controles.FindControl("ContributionPeriod");
            RetirementPeriodLabel = (HtmlGenericControl)Controles.FindControl("RetirementPeriod");
            DefermentPeriodLabel = (HtmlGenericControl)Controles.FindControl("DefermentPeriod");
            InitialContributionPeriodLabel = (HtmlGenericControl)Controles.FindControl("InitialContribution");
            AmountLabel = (HtmlGenericControl)Controles.FindControl("Amount");
            Amount2Label = (HtmlGenericControl)Controles.FindControl("Amount2");
            EducationPeriodLabel = (HtmlGenericControl)Controles.FindControl("EducationPeriod");
            HaveDesignatedPensionerLabel = (HtmlGenericControl)Controles.FindControl("HaveDesignatedPensioner");
            YesLabel = (HtmlGenericControl)Controles.FindControl("Yes");
            NoLabel = (HtmlGenericControl)Controles.FindControl("No");
            InvestmentProfileLabel = (HtmlGenericControl)Controles.FindControl("InvestmentProfile");
            planinformationLabel = (Literal)Controles.FindControl("planinformation");
            ltStudentInformation = (Literal)Controles.FindControl("ltStudentInformation");
            StudentName = (HtmlGenericControl)Controles.FindControl("StudentName");
            DateOfBirthLabel = (HtmlGenericControl)Controles.FindControl("DateOfBirth");
            ContributionTypeLabel = (HtmlGenericControl)Controles.FindControl("ContributionType");
            YearsLabel = (HtmlGenericControl)Controles.FindControl("Years");
            InsuredAmountLabel = (HtmlGenericControl)Controles.FindControl("InsuredAmount");
            SpouseOtherInsuredLabel = (HtmlGenericControl)Controles.FindControl("SpouseOtherInsured");
            InsuredSpouseOtherInsuredAmountLabel = (HtmlGenericControl)Controles.FindControl("InsuredSpouseOtherInsuredAmount");
            YearsspouseOtherLabel = (HtmlGenericControl)Controles.FindControl("YearsspouseOther");
            AdditionalTermInsuranceLabel = (HtmlGenericControl)Controles.FindControl("AdditionalTermInsurance");
            AdditonalTermInsuredAmount = (HtmlGenericControl)Controles.FindControl("AdditonalTermInsuredAmount");
            AccidentalDeathBenefitsLabel = (HtmlGenericControl)Controles.FindControl("AccidentalDeathBenefits");
            FinancialGoal = (HtmlGenericControl)Controles.FindControl("FinancialGoal");
            AmountGoal = (HtmlGenericControl)Controles.FindControl("AmountGoal");
            AtAgeGoal = (HtmlGenericControl)Controles.FindControl("AtAgeGoal");
            ltRidersLabel = (Literal)Controles.FindControl("ltRidersLabel");
            AdditionalTermInsuranceInsuredAmount = (HtmlGenericControl)Controles.FindControl("AdditionalTermInsuranceInsuredAmount");
            CritialIllnessInsuredAmount = (HtmlGenericControl)Controles.FindControl("CritialIllnessInsuredAmount");
            CriticalIllnessLabel = (HtmlGenericControl)Controles.FindControl("CriticalIllness");
            OthersInsuredsLabel = (HtmlGenericControl)Controles.FindControl("OthersInsureds");
            Repatriation = (HtmlGenericControl)Controles.FindControl("Repatriation");
            Lote = (HtmlGenericControl)Controles.FindControl("Lote");
            LoteType = (HtmlGenericControl)Controles.FindControl("LoteType");
            lblDeducible = (HtmlGenericControl)Controles.FindControl("lblDeducible");
            lblEffectiveDate = (HtmlGenericControl)Controles.FindControl("lblEffectiveDate");
            DepositAmount = (HtmlGenericControl)Controles.FindControl("DepositAmount");
            lblDependents = (HtmlGenericControl)Controles.FindControl("lblDependents");
            MaternityAndNewBornComplication = (HtmlGenericControl)Controles.FindControl("MaternityAndNewBornComplication");
            OrganTransplant = (HtmlGenericControl)Controles.FindControl("OrganTransplant");
            ddlDeducible = ((DropDownList)Controles.FindControl("ddlDeducible"));
            ddlDeducibleType = ((DropDownList)Controles.FindControl("ddlDeducibleType"));
            hdnDeductibleTypeID = ((HiddenField)Controles.FindControl("hdnDeductibleTypeID"));

            //Bmarroquin 19/01/2017 a raiz de tropicalizacion ESA, agregando codigo 
            lblVidaBasico = (HtmlGenericControl)Controles.FindControl("lblVidaBasico");
            lblMontoVidaBasico = (HtmlGenericControl)Controles.FindControl("lblMontoVidaBasico");
            ddlVidaBasico = (DropDownList)Controles.FindControl("ddlVidaBasico");
            txtMontoAseguradoCorBasica = (TextBox)Controles.FindControl("txtMontoAseguradoCorBasica");
            // Fin cambios 19/01/2017 a raiz de tropicalizacion ESA,

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            var vView = mvSelectControl.GetActiveView();

            if (vView == VHorizon)
            {
                udpPlanInformation.AddTrigger(UCHorizon._si1.ID);
                udpPlanInformation.AddTrigger(UCHorizon._no1.ID);
                ddlInitialContribution_SelectedIndexChanged(UCHorizon._ddlInitialContribution, null);
            }
            else
                if (vView == VAxy)
                {
                    udpPlanInformation.AddTrigger(UCAxy._si1.ID);
                    udpPlanInformation.AddTrigger(UCAxy._no1.ID);
                    ddlInitialContribution_SelectedIndexChanged(UCAxy._ddlInitialContribution, null);
                }
                else
                    if (vView == VEduplan)
                    {
                        udpPlanInformation.AddTrigger(UCEduplan._si1.ID);
                        udpPlanInformation.AddTrigger(UCEduplan._no1.ID);
                        ddlInitialContribution_SelectedIndexChanged(UCEduplan._ddlInitialContribution, null);
                    }
                    else
                        if (vView == VScholar)
                        {
                            udpPlanInformation.AddTrigger(UCScholar._si1.ID);
                            udpPlanInformation.AddTrigger(UCScholar._no1.ID);
                            ddlInitialContribution_SelectedIndexChanged(UCScholar._ddlInitialContribution, null);
                        }
                        else
                            if (vView == VCompassIndex)
                            {
                                ddlManageRidersDropDowns_SelectedIndexChanged(UCCompassIndex._ddlAccidentalDeathBenefits, null);
                                ddlManageRidersDropDowns_SelectedIndexChanged(UCCompassIndex._ddlSpouseOtherInsured, null);
                                ddlManageRidersDropDowns_SelectedIndexChanged(UCCompassIndex._ddlAdditionalTermInsurance, null);
                                ContributionType_SelectedIndexChanged(UCCompassIndex._ddlContributionType, null);
                                ddlInitialContribution_SelectedIndexChanged(UCCompassIndex._ddlInitialContribution, null);
                                ddlInvestmentProfile_SelectedIndexChanged(UCCompassIndex._ddlInvestmentProfile, null);
                            }
                            else
                                if (vView == VLegacy)
                                {
                                    ddlManageRidersDropDowns_SelectedIndexChanged(UCLegacy._ddlAccidentalDeathBenefits, null);
                                    ddlManageRidersDropDowns_SelectedIndexChanged(UCLegacy._ddlSpouseOtherInsured, null);
                                    ddlManageRidersDropDowns_SelectedIndexChanged(UCLegacy._ddlAdditionalTermInsurance, null);
                                    ContributionType_SelectedIndexChanged(UCLegacy._ddlContributionType, null);
                                    ddlInitialContribution_SelectedIndexChanged(UCLegacy._ddlInitialContribution, null);
                                    ddlInvestmentProfile_SelectedIndexChanged(UCLegacy._ddlInvestmentProfile, null);
                                }
                                else
                                    if (vView == VSentinel)
                                    {
                                        ddlManageRidersDropDowns_SelectedIndexChanged(UCSentinel._ddlAccidentalDeathBenefits, null);
                                        ddlManageRidersDropDowns_SelectedIndexChanged(UCSentinel._ddlSpouseOtherInsured, null);
                                        ddlManageRidersDropDowns_SelectedIndexChanged(UCSentinel._ddlAdditionalTermInsurance, null);
                                        ddlManageRidersDropDowns_SelectedIndexChanged(UCSentinel._ddlCritialIllness, null);
                                        ContributionType_SelectedIndexChanged(UCSentinel._ddlContributionType, null);
                                        ddlInitialContribution_SelectedIndexChanged(UCSentinel._ddlInitialContribution, null);
                                    }
                                    else
                                        if (vView == VLightHouse)
                                        {
                                            ddlManageRidersDropDowns_SelectedIndexChanged(UCLightHouse._ddlAccidentalDeathBenefits, null);
                                            ddlManageRidersDropDowns_SelectedIndexChanged(UCLightHouse._ddlSpouseOtherInsured, null);
                                            ddlManageRidersDropDowns_SelectedIndexChanged(UCLightHouse._ddlAdditionalTermInsurance, null);
                                            ddlManageRidersDropDowns_SelectedIndexChanged(UCLightHouse._ddlCritialIllness, null);
                                            ContributionType_SelectedIndexChanged(UCLightHouse._ddlContributionType, null);
                                            ddlInitialContribution_SelectedIndexChanged(UCLightHouse._ddlInitialContribution, null);
                                            ddlSpecialPayments_SelectedIndexChanged(UCLightHouse._ddlSpecialPayment, null);
                                        }
                                        else
                                            if (vView == VSelect)
                                            {
                                                ddlInitialContribution_SelectedIndexChanged(UCSelect._ddlInitialContribution, null);
                                                ddlManageRidersDropDowns_SelectedIndexChanged(UCSelect._ddlDependents, null);
                                            }
                                            else
                                                if (vView == VElite)
                                                {
                                                    ddlInitialContribution_SelectedIndexChanged(UCElite._ddlInitialContribution, null);
                                                    ddlManageRidersDropDowns_SelectedIndexChanged(UCElite._ddlDependents, null);
                                                }
                                                else
                                                    if (vView == VFortis)
                                                    {
                                                        ddlInitialContribution_SelectedIndexChanged(UCFortis._ddlInitialContribution, null);
                                                        ddlManageRidersDropDowns_SelectedIndexChanged(UCFortis._ddlDependents, null);
                                                    }
                                                    else
                                                        if (vView == VSerenity)
                                                        {
                                                            ddlInitialContribution_SelectedIndexChanged(UCSerenity._ddlInitialContribution, null);
                                                            ddlManageRidersDropDowns_SelectedIndexChanged(UCSerenity._ddlDependents, null);
                                                        }
                                                        else
                                                            if (vView == VAsistencia30dias)
                                                            {
                                                                ddlInitialContribution_SelectedIndexChanged(UCAsistenciaalViajeroAnual30diascontinuos._ddlInitialContribution, null);
                                                                ddlManageRidersDropDowns_SelectedIndexChanged(UCAsistenciaalViajeroAnual30diascontinuos._ddlDependents, null);
                                                            }
                                                            else
                                                                if (vView == VAsistencia60dias)
                                                                {
                                                                    ddlInitialContribution_SelectedIndexChanged(UCAsistenciaalViajeroAnual60díascontinuos._ddlInitialContribution, null);
                                                                    ddlManageRidersDropDowns_SelectedIndexChanged(UCAsistenciaalViajeroAnual60díascontinuos._ddlDependents, null);
                                                                }
                                                                else
                                                                    if (vView == VAsistencia90dias)
                                                                    {
                                                                        ddlInitialContribution_SelectedIndexChanged(UCAsistenciaalViajerohasta90dias._ddlInitialContribution, null);
                                                                        ddlManageRidersDropDowns_SelectedIndexChanged(UCAsistenciaalViajerohasta90dias._ddlDependents, null);
                                                                    }
                                                                    else
                                                                        if (vView == VFunerarios)
                                                                        {
                                                                            ddlManageRidersDropDowns_SelectedIndexChanged(UCFunerarios._ddlOtherInsured, null);
                                                                            ddlManageRidersDropDowns_SelectedIndexChanged(UCFunerarios._ddlRepatriation, null);
                                                                            ContributionType_SelectedIndexChanged(UCFunerarios._ddlContributionType, null);
                                                                            ddlInitialContribution_SelectedIndexChanged(UCFunerarios._ddlInitialContribution, null);
                                                                        }

            Translator("");
        }

        public void Translator(string Lang)
        {
            var ltTitleDesignatedPensionerOrAdditionalInsured = oWUCDesignatedPensionerInformation.FindControl("ltTitleDesignatedPensionerOrAdditionalInsured") as Literal;

            var vView = mvSelectControl.GetActiveView();

            if (vView == VHorizon ||
                vView == VAxy ||
                vView == VEduplan ||
                vView == VScholar)
                ltTitleDesignatedPensionerOrAdditionalInsured.Text = Resources.DesiganetdPensionerInfoLabel;
            else if (vView == VFunerarios)
                ltTitleDesignatedPensionerOrAdditionalInsured.Text = Resources.ADDITIONALINFORMATIONINCLUDINGFAMILY;
            else if (vView == VSelect ||
                     vView == VElite ||
                     vView == VSerenity ||
                     vView == VFortis ||
                     vView == VAsistencia30dias ||
                     vView == VAsistencia60dias ||
                     vView == VAsistencia90dias)
                ltTitleDesignatedPensionerOrAdditionalInsured.Text = Resources.Dependents;
            else
                ltTitleDesignatedPensionerOrAdditionalInsured.Text = Resources.AdditionalInsuredInfoLabel;

            setControls();

            if (!planinformationLabel.isNullReferenceControl())
                planinformationLabel = (Literal)Controles.FindControl("planinformation");

            if (!FamilyProductLabel.isNullReferenceControl())
                FamilyProductLabel.InnerHtml = Resources.FamilyProductLabel;

            if (!PlanNameLabel.isNullReferenceControl())
                PlanNameLabel.InnerHtml = Resources.PlanName;

            if (!PlanTypeLabel.isNullReferenceControl())
                PlanTypeLabel.InnerHtml = Resources.PlanTypeLabel;

            if (!CurrencyLabel.isNullReferenceControl())
                CurrencyLabel.InnerHtml = Resources.CurrencyLabel;

            if (!FrequencyOfPaymentLabel.isNullReferenceControl())
                FrequencyOfPaymentLabel.InnerHtml = Resources.FrequencyofPaymentLabel;

            if (!ContributionPeriodLabel.isNullReferenceControl())
                ContributionPeriodLabel.InnerHtml = Resources.ContributionPeriodLabel;

            if (!RetirementPeriodLabel.isNullReferenceControl())
                RetirementPeriodLabel.InnerHtml = Resources.RetirementPeriod;

            if (!DefermentPeriodLabel.isNullReferenceControl())
                DefermentPeriodLabel.InnerHtml = Resources.DefermentPeriodLabel;

            if (!InitialContributionPeriodLabel.isNullReferenceControl())
                InitialContributionPeriodLabel.InnerHtml = Resources.InitialContributionLabel;

            if (!AmountLabel.isNullReferenceControl())
                AmountLabel.InnerHtml = Resources.AmountLabel;

            if (!HaveDesignatedPensionerLabel.isNullReferenceControl())
                HaveDesignatedPensionerLabel.InnerHtml = Resources.HaveDesignatedPensionerLabel;

            if (!YesLabel.isNullReferenceControl())
                YesLabel.InnerHtml = Resources.YesLabel;

            if (!NoLabel.isNullReferenceControl())
                NoLabel.InnerHtml = Resources.NoLabel;

            if (!InvestmentProfileLabel.isNullReferenceControl())
                InvestmentProfileLabel.InnerHtml = Resources.InvestmentProfile;

            if (!planinformationLabel.isNullReferenceControl())
                planinformationLabel.Text = Resources.PlanInformationLabel;

            if (!EducationPeriodLabel.isNullReferenceControl())
                EducationPeriodLabel.InnerHtml = Resources.EducationPeriod;

            if (!DateOfBirthLabel.isNullReferenceControl())
                DateOfBirthLabel.InnerHtml = Resources.DateofBirthLabel;

            if (!StudentName.isNullReferenceControl())
                StudentName.InnerHtml = Resources.StudentNameLabel;

            if (!ltStudentInformation.isNullReferenceControl())
                ltStudentInformation.Text = Resources.InformationStudentLabel;

            if (!ContributionTypeLabel.isNullReferenceControl())
                ContributionTypeLabel.InnerHtml = Resources.ContributionTypeLabel;

            if (!YearsLabel.isNullReferenceControl())
                YearsLabel.InnerHtml = Resources.Years;

            if (!InsuredAmountLabel.isNullReferenceControl())
                InsuredAmountLabel.InnerHtml = Resources.InsuredAmountLabel;

            if (!SpouseOtherInsuredLabel.isNullReferenceControl())
                SpouseOtherInsuredLabel.InnerHtml = Resources.SpouseOtherInsuredLabel;

            if (!InsuredSpouseOtherInsuredAmountLabel.isNullReferenceControl())
                InsuredSpouseOtherInsuredAmountLabel.InnerHtml = Resources.InsuredAmount;

            if (!YearsspouseOtherLabel.isNullReferenceControl())
                YearsspouseOtherLabel.InnerHtml = Resources.Years;

            if (!AdditionalTermInsuranceLabel.isNullReferenceControl())
                //Bmarroquin 13/01/2017 Se cambio el RESOURCE-->
                //AdditionalTermInsuranceLabel.InnerHtml = Resources.AdditionalTermInsuranceLabel;
                AdditionalTermInsuranceLabel.InnerHtml = Resources.FuneralExpenses;

            if (!AdditonalTermInsuredAmount.isNullReferenceControl())
                AdditonalTermInsuredAmount.InnerHtml = Resources.InsuredAmount;

            if (!AccidentalDeathBenefitsLabel.isNullReferenceControl())
                  //Bmarroquin 13/01/2017 Se cambio el RESOURCE-->
                 //AccidentalDeathBenefitsLabel.InnerHtml = Resources.AccidentalDeathBenefitsLabel;
                 AccidentalDeathBenefitsLabel.InnerHtml = Resources.BenefitDeath;

            if (!FinancialGoal.isNullReferenceControl())
                FinancialGoal.InnerHtml = Resources.FinancialGoalLabel;

            if (!AmountGoal.isNullReferenceControl())
                AmountGoal.InnerHtml = Resources.AmountGoalLabel;

            if (!AtAgeGoal.isNullReferenceControl())
                AtAgeGoal.InnerHtml = Resources.AtAgeGoalLabel;

            if (!ltRidersLabel.isNullReferenceControl())
                ltRidersLabel.Text = Resources.RidersLabel;

            if (!Amount2Label.isNullReferenceControl())
                Amount2Label.InnerHtml = Resources.AmountGoalLabel;

            if (!AdditionalTermInsuranceInsuredAmount.isNullReferenceControl())
                AdditionalTermInsuranceInsuredAmount.InnerHtml = Resources.InsuredAmount;

            if (!CritialIllnessInsuredAmount.isNullReferenceControl())
                CritialIllnessInsuredAmount.InnerHtml = Resources.InsuredAmount;

            if (!CriticalIllnessLabel.isNullReferenceControl())
                //Bmarroquin 13/01/2017 Se cambio el RESOURCE-->
                //CriticalIllnessLabel.InnerHtml = Resources.CriticalIllnessLabel;
                CriticalIllnessLabel.InnerHtml = Resources.BenefitIllness;          

            if (!OthersInsuredsLabel.isNullReferenceControl())
                OthersInsuredsLabel.InnerHtml = Resources.OthersInsuredsLabel;

            if (!Repatriation.isNullReferenceControl())
                Repatriation.InnerHtml = Resources.Repatriation;

            if (!Lote.isNullReferenceControl())
                Lote.InnerHtml = Resources.Lote;

            if (!LoteType.isNullReferenceControl())
                LoteType.InnerHtml = Resources.LoteType;

            if (!lblEffectiveDate.isNullReferenceControl())
                lblEffectiveDate.InnerHtml = Resources.EffectiveDate.Capitalize();

            if (!DepositAmount.isNullReferenceControl())
                //2016-02-18 | Marcos J. Perez
                //DepositAmount.InnerHtml = Resources.DepositAmount;
                DepositAmount.InnerHtml = Resources.InitialPayment;

            if (!lblDependents.isNullReferenceControl())
                lblDependents.InnerHtml = Resources.Dependents;

            if (!MaternityAndNewBornComplication.isNullReferenceControl())
                MaternityAndNewBornComplication.InnerHtml = Resources.MaternityAndNewBornComplication;

            if (!OrganTransplant.isNullReferenceControl())
                OrganTransplant.InnerHtml = Resources.OrganTransplant;

            //Bmarroquin 19/01/2017 a raiz de tropicalizacion ESA, se agregan las rutinas siguientes...
            if (!lblVidaBasico.isNullReferenceControl())
                lblVidaBasico.InnerHtml = Resources.CoverageBasicLabel;

            if (!lblMontoVidaBasico.isNullReferenceControl())
                lblMontoVidaBasico.InnerHtml = Resources.InsuredAmountLabel;
            //Fin cambios Bmarroquin 19/01/2017 

            if (isChangingLang)
                Initialize();
        }

        public void setContainer()
        {
            try
            {
                var bodyContent = this.Page.Master.FindControl("bodyContent");

                if (!ObjServices.IsDataReviewMode)
                {
                    var PlanPolicyContainer = bodyContent.FindControl("PlanPolicyContainer");
                    var WUCDesignatedPensionerInformation = (WUCDesignatedPensionerInformation)PlanPolicyContainer.FindControl("WUCDesignatedPensionerInformation");
                    opnDesignatedPensioner = (Panel)WUCDesignatedPensionerInformation.FindControl("pnDesignatedPensionerOrAddicionalInsured");
                    oudpDesignatedPensioner = (UpdatePanel)WUCDesignatedPensionerInformation.FindControl("udpDesignatedPensioner");
                    oWUCDesignatedPensionerInformation = (WUCDesignatedPensionerInformation)PlanPolicyContainer.FindControl("WUCDesignatedPensionerInformation");
                }
                else
                {
                    var PlanPolicyContainer = bodyContent.FindControl("DReviewContainer").FindControl("PlanPolicyContainer");
                    opnDesignatedPensioner = (Panel)PlanPolicyContainer.FindControl("WUCDesignatedPensionerInformation").FindControl("pnDesignatedPensionerOrAddicionalInsured");
                    oudpDesignatedPensioner = (UpdatePanel)PlanPolicyContainer.FindControl("WUCDesignatedPensionerInformation").FindControl("udpDesignatedPensioner");
                    oWUCDesignatedPensionerInformation = (WUCDesignatedPensionerInformation)PlanPolicyContainer.FindControl("WUCDesignatedPensionerInformation");
                }
            }
            catch (Exception)
            {


            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            setContainer();

            //Compass Index
            UCCompassIndex._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCCompassIndex._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCCompassIndex._ddlSpouseOtherInsured.SelectedIndexChanged += ManageHaveRider;
            UCCompassIndex._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCCompassIndex._btnPprofile.Click += btnPProfile_Click;

            UCCompassIndex._ddlAccidentalDeathBenefits.SelectedIndexChanged += ddlManageRidersDropDowns_SelectedIndexChanged;
            UCCompassIndex._ddlAdditionalTermInsurance.SelectedIndexChanged += ddlManageRidersDropDowns_SelectedIndexChanged;
            UCCompassIndex._ddlInvestmentProfile.SelectedIndexChanged += ddlInvestmentProfile_SelectedIndexChanged;

            UCCompassIndex._ddlContributionType.SelectedIndexChanged += ContributionType_SelectedIndexChanged;
            //End Compass Index

            //Legacy
            UCLegacy._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCLegacy._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCLegacy._ddlSpouseOtherInsured.SelectedIndexChanged += ManageHaveRider;
            UCLegacy._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCLegacy._ddlInvestmentProfile.SelectedIndexChanged += ddlInvestmentProfile_SelectedIndexChanged;
            UCLegacy._btnPprofile.Click += btnPProfile_Click;
            //End Legacy

            //LightHouse
            UCLightHouse._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCLightHouse._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCLightHouse._ddlSpouseOtherInsured.SelectedIndexChanged += ManageHaveRider;
            UCLightHouse._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;

            this.UCLightHouse._ddlSpecialPayment.SelectedIndexChanged += ddlSpecialPayments_SelectedIndexChanged;
            //End LightHouse

            //Sentinel
            UCSentinel._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCSentinel._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCSentinel._ddlSpouseOtherInsured.SelectedIndexChanged += ManageHaveRider;
            UCSentinel._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            //End Sentinel

            //Eduplan
            UCEduplan._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCEduplan._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCEduplan._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCEduplan._ddlInitialContribution.SelectedIndexChanged += ddlInitialContribution_SelectedIndexChanged;

            UCEduplan._si1.CheckedChanged += ManageHaveDesigantedPensionerQuestion;
            UCEduplan._no1.CheckedChanged += ManageHaveDesigantedPensionerQuestion;
            //End Eduplan

            //Horizon
            UCHorizon._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCHorizon._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCHorizon._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCHorizon._ddlInitialContribution.SelectedIndexChanged += ddlInitialContribution_SelectedIndexChanged;

            UCHorizon._si1.CheckedChanged += ManageHaveDesigantedPensionerQuestion;
            UCHorizon._no1.CheckedChanged += ManageHaveDesigantedPensionerQuestion;
            //End Horizon

            //Scholar
            UCScholar._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCScholar._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCScholar._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCScholar._ddlInvestmentProfile.SelectedIndexChanged += ddlInvestmentProfile_SelectedIndexChanged;

            UCScholar._ddlInitialContribution.SelectedIndexChanged += ddlInitialContribution_SelectedIndexChanged;

            UCScholar._si1.CheckedChanged += ManageHaveDesigantedPensionerQuestion;
            UCScholar._no1.CheckedChanged += ManageHaveDesigantedPensionerQuestion;
            UCScholar._btnPprofile.Click += btnPProfile_Click;
            //End Scholar

            //Axy
            UCAxy._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCAxy._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCAxy._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCAxy._ddlInitialContribution.SelectedIndexChanged += ddlInitialContribution_SelectedIndexChanged;
            UCAxy._si1.CheckedChanged += ManageHaveDesigantedPensionerQuestion;
            UCAxy._no1.CheckedChanged += ManageHaveDesigantedPensionerQuestion;
            UCAxy._ddlInvestmentProfile.SelectedIndexChanged += ddlInvestmentProfile_SelectedIndexChanged;
            UCAxy._btnPprofile.Click += btnPProfile_Click;
            //End Axy  

            //Select
            UCSelect._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCSelect._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCSelect._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCSelect._ddlInitialContribution.SelectedIndexChanged += ddlInitialContribution_SelectedIndexChanged;
            //End Select

            //Elite
            UCElite._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCElite._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCElite._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCElite._ddlInitialContribution.SelectedIndexChanged += ddlInitialContribution_SelectedIndexChanged;
            //End Elite  

            //Fortis
            UCFortis._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCFortis._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCFortis._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCFortis._ddlInitialContribution.SelectedIndexChanged += ddlInitialContribution_SelectedIndexChanged;
            UCFortis._ddlDeducibleType.SelectedIndexChanged += ddlDeducibleType_SelectedIndexChanged;
            //End Fortis  

            //Serenity
            UCSerenity._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCSerenity._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCSerenity._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCSerenity._ddlInitialContribution.SelectedIndexChanged += ddlInitialContribution_SelectedIndexChanged;
            //End Serenity

            //AsistenciaalViajeroAnual30diascontinuos
            UCAsistenciaalViajeroAnual30diascontinuos._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCAsistenciaalViajeroAnual30diascontinuos._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCAsistenciaalViajeroAnual30diascontinuos._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCAsistenciaalViajeroAnual30diascontinuos._ddlInitialContribution.SelectedIndexChanged += ddlInitialContribution_SelectedIndexChanged;
            //End AsistenciaalViajeroAnual30diascontinuos

            //AsistenciaalViajeroAnual60díascontinuos
            UCAsistenciaalViajeroAnual60díascontinuos._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCAsistenciaalViajeroAnual60díascontinuos._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCAsistenciaalViajeroAnual60díascontinuos._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCAsistenciaalViajeroAnual60díascontinuos._ddlInitialContribution.SelectedIndexChanged += ddlInitialContribution_SelectedIndexChanged;
            //End AsistenciaalViajeroAnual60díascontinuos

            //AsistenciaalViajeroAnual90díascontinuos
            UCAsistenciaalViajerohasta90dias._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCAsistenciaalViajerohasta90dias._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCAsistenciaalViajerohasta90dias._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCAsistenciaalViajerohasta90dias._ddlInitialContribution.SelectedIndexChanged += ddlInitialContribution_SelectedIndexChanged;
            //End AsistenciaalViajeroAnual90díascontinuos

            //Funerarios
            UCFunerarios._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            UCFunerarios._ddlCurrency.SelectedIndexChanged += ddlCurrency_SelectedIndexChanged;
            UCFunerarios._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCFunerarios._ddlOtherInsured.SelectedIndexChanged += ManageHaveRider;
            //End Funerario

            //Basic Plan
            UCBasicPlan._ddlProductName.SelectedIndexChanged += ddlProductName_SelectedIndexChanged;
            UCBasicPlan._ddlFamilyProduct.SelectedIndexChanged += ddlFamilyProduct_SelectedIndexChanged;
            //End Basic Plan



        }

        /// <summary>
        /// primero busco los datos de la poliza para saber si tiene un producto cargado y seleccionar su pantalla en caso
        /// contrario cargo como defecto a plan horizon 
        /// </summary>
        public void Initialize()
        {
            setVariables();

            setContainer();

            var datos = ObjServices.oPolicyManager.GetPlanData(CorpId, RegionId, CountryId, DomesticregId, StateProvId
               , CityId, OfficeId, CaseSeqNo, HistSeqNo);

            var isSelect = (datos != null && datos.BlId.HasValue && datos.BlTypeId.HasValue && datos.ProductId.HasValue);

            //Caso por defecto para ver pantalla de producto                  
            Utility.itemProduct product = null;
            ViewState["GenerateSelect"] = !isSelect;

            ObjServices.isSavePlan = isSelect;

            /*esto quiere decir que el producto fue asignado*/
            if (isSelect)
            {
                product = new Utility.itemProduct()
                {
                    BlId = datos.BlId.Value,
                    BlTypeId = datos.BlTypeId.Value,
                    ProductTypeId = datos.ProductTypeId.Value,
                    ProductId = datos.ProductId.Value,
                    /*estos valores corresponde a la coorporacion o compania*/
                    CorpId = datos.CorpId,
                    CountryId = datos.CountryId,
                    RegionId = datos.RegionId,
                    NameKey = datos.NameKey
                };
            }
            else
                ClearData();

            SelectControl(product);

            FillData();

            var TabConfig = ObjServices.getTabConfig();
            var TabPlanPolicyInfo = TabConfig.FirstOrDefault(x => x.TabId == Utility.Tab.PlanPolicy.ToInt());

            if ((ObjServices.IsDataReviewMode || TabPlanPolicyInfo.IsValid)
                && !product.isNullReferenceObject())
            {
                ddlFamilyProduct.Enabled = false;
                ddlProductName.Enabled = false;
                if (!ddlCurrency.isNullReferenceControl())
                    ddlCurrency.Enabled = !ObjServices.IsDataReviewMode;
            }

            if (ObjServices.IsReadOnly)
                ReadOnlyControls(ObjServices.IsReadOnly);
        }

        /// <summary>
        /// Llenado de DropDowns
        /// </summary>
        /// <param name="NameControls"></param>
        /// <param name="SelectProduct"></param>
        public void fillDefaultDrop(string NameControls, Utility.itemProduct SelectProduct = null)
        {
            bool isFuneral = false;

            Utility.ProductBehavior ProductBehavior = Utility.ProductBehavior.None;

            if (SelectProduct != null)
            {
                ProductBehavior = (Utility.ProductBehavior)Utility.getvalueFromEnumType(SelectProduct.NameKey, typeof(Utility.ProductBehavior));
                ObjServices.ProductLine = ObjServices.GetProductLine(ProductBehavior);
                isFuneral = ObjServices.ProductLine == Utility.ProductLine.Funeral;
                ObjServices.KeyNameProduct = SelectProduct.NameKey;
                ObjServices.esFunerario(isFuneral);
            }

            var generateselect = ViewState["GenerateSelect"].ToBoolean();
            //UserControl Controles;
            hfSelectControls.Value = NameControls;

            /*este es el control por defecto que se presenta*/
            if (string.IsNullOrEmpty(NameControls))
                hfSelectControls.Value = "VBasicPlan";

            setVariables();
            setControls();

            var datos = ObjServices.oPolicyManager.GetPlanData(CorpId, RegionId, CountryId, DomesticregId, StateProvId
                 , CityId, OfficeId, CaseSeqNo, HistSeqNo);


            if (ddlFinancialInstitution != null)
            {
                ObjServices.GettingAllDropsJSON(ref this.ddlFinancialInstitution, Utility.DropDownType.Provider, "ElementDesc", false,
                                         ObjServices.Corp_Id,
                                         ObjServices.Region_Id,
                                         ObjServices.Country_Id
                                         , ObjServices.Domesticreg_Id
                                         , ObjServices.State_Prov_Id
                                         , ObjServices.City_Id
                                         , ObjServices.Office_Id
                                         , ObjServices.Case_Seq_No
                                         , ObjServices.Hist_Seq_No,
                                         agentId: 1);

                if (!string.IsNullOrEmpty(datos.ProviderName) && datos.ProviderId.HasValue && datos.ProviderTypeId.HasValue)
                {
                    var SelectProvider = new Utility.provider();
                    SelectProvider.ElementDesc = datos.ProviderName;
                    SelectProvider.ProviderId = datos.ProviderId;
                    SelectProvider.ProviderTypeId = datos.ProviderTypeId;
                    var x2 = Utility.serializeToJSON(SelectProvider);
                    ddlFinancialInstitution.SelectIndexByValueJSON(x2);
                    ViewState["GenerateSelect"] = false;
                }
            }

            if (SelectProduct != null)
            {
                ObjServices.GettingAllDropsJSON(ref ddlFamilyProduct, Utility.DropDownType.PlanType, "BlDesc"
                    , corpId: CorpId
                    , regionId: RegionId
                    , countryId: CountryId
                    , domesticregId: DomesticregId
                    , stateProvId: StateProvId
                    , cityId: CityId
                    , officeId: OfficeId
                    , GenerateItemSelect: false
                    );

                ObjServices.GettingAllDropsJSON(ref ddlProductName, Utility.DropDownType.ProductByFamily, "ProductDesc"
                    , corpId: CorpId
                    , regionId: RegionId
                    , countryId: CountryId
                    , domesticregId: DomesticregId
                    , stateProvId: StateProvId
                    , cityId: CityId
                    , officeId: OfficeId
                    , ProductTypeId: SelectProduct.ProductTypeId
                    , BlId: SelectProduct.BlId
                    , BlTypeId: SelectProduct.BlTypeId
                    , GenerateItemSelect: generateselect
                    );

                var x = Utility.serializeToJSON(SelectProduct);

                var SelectProductFamily = new Utility.FamilyProduct();
                SelectProductFamily.BlId = SelectProduct.BlId;
                SelectProductFamily.BlTypeId = SelectProduct.BlTypeId;

                SelectProductFamily.ProductTypeId = SelectProduct.ProductTypeId;

                SelectProductFamily.CorpId = SelectProduct.CorpId;
                SelectProductFamily.CountryId = SelectProduct.CountryId;
                SelectProductFamily.RegionId = SelectProduct.RegionId;
                var x2 = Utility.serializeToJSON(SelectProductFamily);
                ddlFamilyProduct.SelectIndexByValueJSON(x2);
                ddlProductName.SelectIndexByValueJSON(x);
                ViewState["GenerateSelect"] = false;
            }
            else
            {
                ObjServices.GettingAllDropsJSON(ref ddlFamilyProduct
                    , Utility.DropDownType.PlanType, "BlDesc"
                    , corpId: CorpId
                    , regionId: RegionId
                    , countryId: CountryId
                    , domesticregId: DomesticregId
                    , stateProvId: StateProvId
                    , cityId: CityId
                    , officeId: OfficeId
                    );

                var SelectProductFamily = new Utility.FamilyProduct();
                SelectProductFamily.BlId = 1;
                SelectProductFamily.BlTypeId = 1;
                SelectProductFamily.ProductTypeId = 3;
                SelectProductFamily.CorpId = ObjServices.Corp_Id;
                SelectProductFamily.CountryId = ObjServices.Country_Id;
                SelectProductFamily.RegionId = ObjServices.Region_Id;

                var x2 = Utility.serializeToJSON(SelectProductFamily);
                ddlFamilyProduct.SelectIndexByValueJSON(x2);

                ObjServices.GettingAllDropsJSON(ref ddlProductName,
                    Utility.DropDownType.ProductByFamily, "ProductDesc"
                    , corpId: CorpId
                    , regionId: RegionId
                    , countryId: CountryId
                    , domesticregId: DomesticregId
                    , stateProvId: StateProvId
                    , cityId: CityId
                    , officeId: OfficeId
                    , BlId: 1
                    , BlTypeId: 1
                    , GenerateItemSelect: generateselect
                    );
            }

            #region POLICY DATA
            if (datos != null)
            {
                if (ddlLoteType != null)
                {
                    ObjServices.GettingAllDrops(ref ddlLoteType,
                                             Utility.DropDownType.LoteType,
                                             "CodeName",
                                             "CodeName",
                                             corpId: ObjServices.Corp_Id,
                                             NameKey: SelectProduct.NameKey
                                             );
                }

                if (ddlPlanType != null)
                {
                    var keyProduct = Utility.deserializeJSON<Utility.itemProduct>(ddlProductName.SelectedValue);

                    ObjServices.GettingAllDrops(ref ddlPlanType,
                                            Utility.DropDownType.Serie,
                                            "SerieDesc",
                                            "PolicySerieId",
                                            corpId: ObjServices.Corp_Id,
                                            NameKey: SelectProduct.NameKey,
                                            ProductTypeId: keyProduct.ProductTypeId
                                            );

                    if (datos.PolicySerieId != 0)
                        ddlPlanType.SelectIndexByValue(datos.PolicySerieId.ToString());
                }

                if (ddlCurrency != null)
                {
                    ObjServices.GettingAllDrops(ref ddlCurrency,
                                            Utility.DropDownType.Currency,
                                            "CurrencyDesc",
                                            "CurrencyId",
                                            corpId: ObjServices.Corp_Id,
                                            NameKey: SelectProduct.NameKey
                                            );

                    if (datos.CurrencyId.HasValue)
                        ddlCurrency.SelectIndexByValue(datos.CurrencyId.ToString());
                    else if (ddlCurrency.Items.Count == 2)
                        ddlCurrency.SelectedIndex = 1;
                }

                if (ddlInvestmentProfile != null)
                {
                    int? currencyId = Utility.IsIntReturnNull(ddlCurrency.SelectedValue);
                    ObjServices.GettingAllDropsJSON(
                          ref ddlInvestmentProfile, Utility.DropDownType.ProfileType_NewBusiness
                        , "ProfileTypeDesc"
                        , corpId: ObjServices.Corp_Id
                        , regionId: ObjServices.Region_Id
                        , countryId: ObjServices.Country_Id
                        , domesticregId: ObjServices.Domesticreg_Id
                        , stateProvId: ObjServices.State_Prov_Id
                        , cityId: ObjServices.City_Id, officeId: ObjServices.Office_Id
                        , caseSeqNo: ObjServices.Case_Seq_No
                        , histSeqNo: ObjServices.Hist_Seq_No
                        , currencyId: currencyId
                        );

                    if (datos.ProfileTypeId.HasValue)
                        ddlInvestmentProfile.SelectIndexByValue(ddlInvestmentProfile.Items.FindByText(datos.ProfileTypeDesc).Value);
                }

                if (ddlUntilAge != null)
                    ObjServices.GettingAllDrops(ref ddlUntilAge,
                                            Utility.DropDownType.ContributionType,
                                            "ContributionTypeDesc",
                                            "ContributionTypeId",
                                             corpId: ObjServices.Corp_Id,
                                             NameKey: SelectProduct.NameKey
                                            );

                if (!ddlContributionType.isNullReferenceControl())
                {
                    ObjServices.GettingAllDrops(ref ddlContributionType,
                                            Utility.DropDownType.ContributionType,
                                            "ContributionTypeDesc",
                                            "ContributionTypeId",
                                             corpId: ObjServices.Corp_Id,
                                             NameKey: SelectProduct.NameKey
                                            );

                    if (ProductBehavior != Utility.ProductBehavior.VIDACRED)
                    {
                        //4 es el contribution type id que hace referencia al numero de meses.
                        ddlContributionType.Items.Remove(ddlContributionType.Items.FindByValue("4"));
                    }
                    if (datos.ContributionTypeId.HasValue)
                        ddlContributionType.SelectIndexByValue(datos.ContributionTypeId.ToString());
                    else
                    {
                        ddlContributionType.SelectIndexByValue("3", true);
                        ContributionType_SelectedIndexChanged(ddlContributionType, null);
                    }

                }

                if (ddlFrequencyofPayment != null)
                {
                    ObjServices.GettingAllDrops(ref ddlFrequencyofPayment,
                                            Utility.DropDownType.PaymentFrequency,
                                            "PaymentFreqTypeDesc",
                                            "PaymentFreqTypeId",
                                            corpId: ObjServices.Corp_Id,
                                            NameKey: SelectProduct.NameKey
                                            );

                    if (datos.CurrencyId.HasValue)
                        ddlFrequencyofPayment.SelectIndexByValue(datos.PaymentFreqTypeId.ToString());

                }

                if (txtEffectiveDate != null)
                    txtEffectiveDate.Text = datos.EffectiveDate.ToString();


                if (ddlRetirementPeriod != null)
                {
                    ObjServices.GettingAllDrops(ref ddlRetirementPeriod,
                                            Utility.DropDownType.RetirementPeriod,
                                            NameKey: SelectProduct.NameKey
                                            );

                    if (datos.RetirementPeriod.HasValue)
                        ddlRetirementPeriod.SelectIndexByValue(datos.RetirementPeriod.ToString());
                }

                if (ddlEducationPeriod != null)
                {
                    ddlEducationPeriod.Items.Clear();
                    for (int i = 5; i <= 30; i++)
                        ddlEducationPeriod.Items.Add(new ListItem(i.ToString(), i.ToString()));

                    ddlEducationPeriod.Items.Insert(0, new ListItem() { Value = "-1", Text = "----" });

                    if (datos.RetirementPeriod.HasValue)
                        ddlEducationPeriod.SelectIndexByValue(datos.RetirementPeriod.ToString());
                }

                if (ddlDefermentPeriod != null)
                {
                    ObjServices.GettingAllDrops(ref ddlDefermentPeriod,
                                            Utility.DropDownType.DefermentPeriod,
                                            NameKey: SelectProduct.NameKey
                                            );

                    if (datos.DefermentPeriod.HasValue)
                        ddlDefermentPeriod.SelectIndexByValue(datos.DefermentPeriod.ToString());
                }

                if (!isFuneral)
                {
                    if (ddlProductName.SelectedValue != "")
                        FillDropSentinelAndLightHouse(ddlProductName.SelectedItem.Text);

                    if (ddlContributionPeriod != null)
                    {
                        if (ddlProductName.SelectedItem.Text.Contains("Vida Credito"))
                        {
                            var yearsConvertedToMonth = datos.ContributionYears * 12;
                            var contributionPeriod = (yearsConvertedToMonth + datos.ContributionMonths);
                            if (contributionPeriod.HasValue && contributionPeriod > 0)
                            {
                                ddlContributionPeriod.SelectIndexByValue(contributionPeriod.ToString());
                            }
                            //value= value+ datos.contri
                        }
                        else if (datos.ContributionYears.HasValue)
                        {
                            //txtContributionPeriod.Text = datos.ContributionYears.Value.ToString(NumberFormatInfo.InvariantInfo);
                            ddlContributionPeriod.SelectIndexByValue(datos.ContributionYears.Value.ToString(NumberFormatInfo.InvariantInfo));
                        }
                    }
                }
                else
                    if (datos.ContributionYears.HasValue)
                        ddlContributionPeriod.SelectIndexByValue(datos.ContributionYears.Value.ToString(NumberFormatInfo.InvariantInfo).ToString());

                if (txtAmount2 != null)
                    if (datos.GoalAmount.HasValue)
                        txtAmount2.Text = datos.GoalAmount.HasValue ? datos.GoalAmount.Value.ToString(NumberFormatInfo.InvariantInfo) : "0.00";


                if (txtAtAge != null)
                    txtAtAge.Text = datos.GoalAtAge.HasValue ? datos.GoalAtAge.Value.ToString(NumberFormatInfo.InvariantInfo) : "00";


                if (ddlFinancialGlobal != null)
                {
                    var Value = datos.GoalAmount.HasValue && datos.GoalAtAge.HasValue ? "1" : "2";
                    ddlFinancialGlobal.SelectedValue = Value;
                }
                if (ddlDestinyFund != null)
                {
                    var Value = !string.IsNullOrEmpty(datos.DestinationFund) ? datos.DestinationFund : string.Empty;
                    if (!string.IsNullOrEmpty(datos.DestinationFund))
                        ddlDestinyFund.SelectedValue = Value;
                }


                if (txtAmount != null)
                {
                    if (datos.InitialContribution.HasValue)
                    {
                        txtAmount.Text = datos.InitialContribution.Value.ToString(NumberFormatInfo.InvariantInfo);

                        if (ddlInitialContribution != null && datos.InitialContribution.Value != 0)
                            ddlInitialContribution.SelectIndexByValue("1");
                        else
                            ddlInitialContribution.SelectIndexByValue("2");

                    }
                    else
                        if (ddlInitialContribution != null)
                            ddlInitialContribution.SelectIndexByValue("2");
                }

                if (txtSpecialPayment != null)
                {
                    if (datos.SpecialPayment.HasValue)
                    {
                        txtSpecialPayment.Text = datos.SpecialPayment.Value.ToString(NumberFormatInfo.InvariantInfo);

                        if (ddlSpecialPayment != null && datos.SpecialPayment.Value != 0)
                            ddlSpecialPayment.SelectIndexByValue("1");
                        else
                            ddlSpecialPayment.SelectIndexByValue("2");

                    }
                    else
                        if (ddlSpecialPayment != null)
                            ddlSpecialPayment.SelectIndexByValue("2");
                }



                if (txtFinancingRate != null)
                    if (datos.InterestRate.HasValue)
                        txtFinancingRate.Text = datos.InterestRate.HasValue ? datos.InterestRate.Value.ToString(NumberFormatInfo.InvariantInfo) : "0.00";


                if (ddlDeducibleType != null)
                {
                    if (ProductBehavior == Utility.ProductBehavior.Fortis)
                    {   //Bindear el dropdown
                        ObjServices.GettingAllDrops(ref ddlDeducibleType,
                                                    Utility.DropDownType.DeductibleType,
                                                    DataTextField: "DeductibleTypeDesc",
                                                    DataValueField: "DeductibleTypeId",
                                                    NameKey: SelectProduct.NameKey
                                                   );

                        if (datos.DeductibleTypeId.HasValue)
                            ddlDeducibleType.SelectIndexByValue(datos.DeductibleTypeId.ToString());
                    }
                    else
                    {
                        //Bindear el hiddenfield
                        var data = ObjServices.GettingDropData(Utility.DropDownType.DeductibleType, NameKey: SelectProduct.NameKey);
                        hdnDeductibleTypeID.Value = data.FirstOrDefault(o => o.Namekey == "Default").DeductibleTypeId.ToString();
                    }

                }


                if (ddlDeducible != null)
                {
                    if (ProductBehavior != Utility.ProductBehavior.Fortis)
                    {

                        ObjServices.GettingAllDropsJSON(ref ddlDeducible,
                                                        Utility.DropDownType.DeductibleCategory,
                                                        DataTextField: "DeductibleCategoryValue",
                                                        NameKey: SelectProduct.NameKey
                                                       );

                        //Formatear valores del dropdown
                        for (int i = 1; i < ddlDeducible.Items.Count; i++)
                            ddlDeducible.Items[i].Text
                                = double.Parse(ddlDeducible.Items[i].Text).ToFormatNumeric();
                    }
                    else
                        ddlDeducibleType_SelectedIndexChanged(ddlDeducibleType, null);

                    if (datos.DeductibleCategoryId.HasValue && datos.DeductibleTypeId.HasValue)
                    {
                        var deductibleTypeID = datos.DeductibleTypeId;
                        var deductibleCategoryId = datos.DeductibleCategoryId;
                        var itemSelect = new Utility.itemDeductible
                        {
                            DeductibleTypeId = deductibleTypeID.Value,
                            DeductibleCategoryId = deductibleCategoryId.Value
                        };

                        var jsonString = Utility.serializeToJSON<Utility.itemDeductible>(itemSelect);
                        ddlDeducible.SelectIndexByValueJSON(jsonString);

                    }

                }
            }
            #endregion

            #region STUDENTS DATA
            /*estudiantes*/
            if (hfSelectControls.Value == "VEduplan" || hfSelectControls.Value == "VScholar")
            {
                Entity.UnderWriting.Entities.Contact info =
                       ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, ObjServices.StudentContactId.Value, languageId: ObjServices.Language.ToInt());

                txtStudentName = ((TextBox)Controles.FindControl("txtStudentName"));

                txtAge = ((TextBox)Controles.FindControl("txtAge"));

                if (info != null)
                {
                    txtAge.Text = (info.Dob.HasValue ? info.Dob.Value.ToShortDateString() : "");
                    txtStudentName.Text = (string.IsNullOrEmpty(info.FirstName) == false ? info.FirstName.ToString() : "");
                }
            }
            #endregion

            /*RIDERS MANAGER*/

            #region RIDERS DATA

            if (hfSelectControls.Value == "VCompassIndex"
                || hfSelectControls.Value == "VLegacy"
                || hfSelectControls.Value == "VSentinel"
                || hfSelectControls.Value == "VLightHouse"
                || hfSelectControls.Value == "VElite"
                || hfSelectControls.Value == "VSelect"
                || hfSelectControls.Value == "VFortis"
                || hfSelectControls.Value == "VSerenity"
                || hfSelectControls.Value == "VAsistencia90dias"
                || hfSelectControls.Value == "VAsistencia30dias"
                || hfSelectControls.Value == "VAsistencia60dias"
                || hfSelectControls.Value == "VFunerarios"
                )
            {
                var riders = ObjServices.oRider.GetAllRider(new Entity.UnderWriting.Entities.Policy.Parameter
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
                    LanguageId = ObjServices.Language.ToInt()
                });

                if (ddlAccidentalDeathBenefits != null)
                    ddlAccidentalDeathBenefits.SelectIndexByValue("2");

                if (txtAccidentalDeathInsuredAmount != null)
                    txtAccidentalDeathInsuredAmount.Clear();

                //Bmarroquin 19/01/2017 a raiz de tropicalizacion ESA, agrego el ddl de ITP
                if (ddlCritialIllness != null)
                    ddlCritialIllness.SelectIndexByValue("2");

                if (txtCritialIllnessInsuredAmount != null)
                    txtCritialIllnessInsuredAmount.Clear();

                if (ddlSpouseOtherInsured != null)
                    ddlSpouseOtherInsured.SelectIndexByValue("2");

                if (txtSpouseOtherInsured != null)
                    txtSpouseOtherInsured.Clear();

                if (txtUntilAge != null)
                    txtUntilAge.Text = "";

                if (txtYearsSpouseOther != null)
                    txtYearsSpouseOther.Clear();

                if (ddlAdditionalTermInsurance != null)
                    ddlAdditionalTermInsurance.SelectIndexByValue("2");


                if (txtAdditionalTermInsuranceInsuredAmount != null)
                    txtAdditionalTermInsuranceInsuredAmount.Clear();

                //Bmarroquin 19/01/2017 a raiz de tropicalizacion ESA, se agregan lineas
                if (txtMontoAseguradoCorBasica != null)
                    txtMontoAseguradoCorBasica.Clear();


                foreach (var item in riders)
                {
                    //Rider_Type_Id	Code_Name	Ryder_Type_Desc
                    //0	Otros	Otros
                    //1	ADB	Seguro Muerte Accidental
                    //2	SEGTEMAD	Seguro Temporal Adicional
                    //3	SPINS	Seguro Asegurado Adicional
                    if (item.RiderTypeId == Utility.RyderType.SeguroMuerteAccidental.ToInt())
                    {
                        if (ddlAccidentalDeathBenefits != null)
                            ddlAccidentalDeathBenefits.SelectIndexByValue("1");

                        if (txtAccidentalDeathInsuredAmount != null)
                            txtAccidentalDeathInsuredAmount.Text = (item.BeneficiaryAmount.HasValue ? item.BeneficiaryAmount.Value.ToString(NumberFormatInfo.InvariantInfo) : "0.00");
                    }


                    if (item.RiderTypeId == Utility.RyderType.MaternityandNewbornComplication.ToInt())
                        if (ddlMaternityAndNewBornComplication != null)
                            ddlMaternityAndNewBornComplication.SelectIndexByValue("1");

                    if (item.RiderTypeId == Utility.RyderType.OrganTransplant.ToInt())
                        if (ddlOrganTransplant != null)
                            ddlOrganTransplant.SelectIndexByValue("1");

                    if (item.RiderTypeId == Utility.RyderType.Dependent.ToInt())
                        if (ddlDependents != null)
                            ddlDependents.SelectIndexByValue("1");

                    if (item.RiderTypeId == Utility.RyderType.SeguroAseguradoAdicional.ToInt())
                    {
                        if (ddlSpouseOtherInsured != null)
                            ddlSpouseOtherInsured.SelectIndexByValue("1");


                        if (txtSpouseOtherInsured != null)
                            txtSpouseOtherInsured.Text = (item.BeneficiaryAmount.HasValue ? item.BeneficiaryAmount.Value.ToString(NumberFormatInfo.InvariantInfo) : "0.00");

                        if (txtYearsSpouseOther != null)
                            txtYearsSpouseOther.Text = (item.NumberOfYear.HasValue ? item.NumberOfYear.Value.ToString(NumberFormatInfo.InvariantInfo) : "0");
                    }

                    if (item.RiderTypeId == Utility.RyderType.SeguroTemporalAdicional.ToInt())
                    {
                        if (ddlAdditionalTermInsurance != null)
                            ddlAdditionalTermInsurance.SelectIndexByValue("1");


                        if (txtAdditionalTermInsuranceInsuredAmount != null)
                            txtAdditionalTermInsuranceInsuredAmount.Text = (item.BeneficiaryAmount.HasValue ? item.BeneficiaryAmount.Value.ToString(NumberFormatInfo.InvariantInfo) : "0.00");
                    }


                    if (item.RiderTypeId == Utility.RyderType.Repatriation.ToInt())
                        if (ddlRepatriation != null)
                            ddlRepatriation.SelectIndexByValue("1");


                    if (item.RiderTypeId == Utility.RyderType.SeguroFamiliarAdicional.ToInt())
                        if (ddlOtherInsured != null)
                            ddlOtherInsured.SelectIndexByValue("1");


                    if (item.RiderTypeId == Utility.RyderType.Lote.ToInt())
                    {
                        if (ddlLote != null)
                            ddlLote.SelectIndexByValue("1");

                        if (ddlLoteType != null)
                            ddlLoteType.SelectIndexByValue(item.RiderInfo);
                    }

                    //Bmarroquin 25/01/2017 a raiz de tropicalizacion ESA, se agrega logica para las coberturas de ITP y GF 
                    if (item.RiderTypeId == Utility.RyderType.GastosFunerarios.ToInt())
                    {
                        if (ddlAdditionalTermInsurance != null)
                            ddlAdditionalTermInsurance.SelectIndexByValue("1");

                        if (txtAdditionalTermInsuranceInsuredAmount != null)
                            txtAdditionalTermInsuranceInsuredAmount.Text = (item.BeneficiaryAmount.HasValue ? item.BeneficiaryAmount.Value.ToString(NumberFormatInfo.InvariantInfo) : "0.00");
                    }

                    if (item.RiderTypeId == Utility.RyderType.InvalidesTotal.ToInt())
                    {
                        if (ddlCritialIllness != null)
                            ddlCritialIllness.SelectIndexByValue("1");

                        if (txtCritialIllnessInsuredAmount != null)
                            txtCritialIllnessInsuredAmount.Text = (item.BeneficiaryAmount.HasValue ? item.BeneficiaryAmount.Value.ToString(NumberFormatInfo.InvariantInfo) : "0.00");
                    }

                    // ********  Fin Cambios Bmarroquin 25/01/2017

                }
            }
            #endregion

        }

        public bool ValidationPolicy(Utility.ProductBehavior productBehavior)
        {
            bool respuesta = true;

            var isFuneral = ObjServices.ProductLine == Utility.ProductLine.Funeral;

            setControls();

            var mensa = new StringBuilder();
            decimal? MinPremium = null;
            //decimal? MinAmount = null;
            //decimal? EducationAmount = null;
            decimal? TotalRetiment = null;
            decimal? TotalinsuredAmount = null;
            //Currency
            //1 = USD
            //3 = RD
            //Payment_Freq_Type_Id	Payment_Freq_Type_Desc
            //1	QT
            //2	MO
            //3	AN
            //4	SA
            int? PaymentTypeID = null;

            int? ContributionPeriod = null;
            int? RetirementPeriod = null;
            int? DeferringPeriod = null;
            int? EducationPeriod = null;

            decimal? AnnualPremium = 0;
            decimal? TargetPremium = 0;
            decimal? PeriodicPremium = 0;
            decimal? ReturnOfPremium = 0;

            decimal? AccidentalDeathInsuredAmount = 0;
            decimal? SpouseOtherInsuredAmount = 0;
            decimal? CritialIllnessInsuredAmount = 0;
            decimal? AdditionalTermInsuranceInsuredAmount = 0;
            decimal? InitialContributionAmount = 0;
            decimal? FinancialGoalAmount = 0;

            decimal? SpecialPaymentAmount = 0;
            decimal? FinancingRate = 0;

            int? AgeLimitContact = null;
            int? AgeLimitOwner = null;
            int? AgeContact = 0;
            int? AgeOwner = 0;

            int count = 0;
            Utility.Currency currency;
            bool contributionPeriodValidation = false;
            int cpStart, cpEnd;
            cpStart = 0;
            cpEnd = 0;
            bool isYear = true;
            string SpecialPaymentConfirmation = string.Empty;
            string DestinyFund = string.Empty;

            var footer = ((WUCFieldFooter)this.Parent.FindControl("WUCFieldFooter"));
            footer.setControls();

            try
            {
                var Comma = ",";

                if (!isFuneral)
                {
                    TargetPremium = Decimal.Parse((string.IsNullOrEmpty(footer.txtTargetAnnualPremium.Text) ? "0.00" : footer.txtTargetAnnualPremium.Text).Replace(Comma, ""), CultureInfo.InvariantCulture);
                    PeriodicPremium = Decimal.Parse((string.IsNullOrEmpty(footer.txtPeriodicPremium.Text) ? "0.00" : footer.txtPeriodicPremium.Text).Replace(Comma, ""), CultureInfo.InvariantCulture);
                    AnnualPremium = Decimal.Parse((string.IsNullOrEmpty(footer.txtAnnualPremium.Text) ? "0.00" : footer.txtAnnualPremium.Text).Replace(Comma, ""), CultureInfo.InvariantCulture);
                    ReturnOfPremium = Decimal.Parse((string.IsNullOrEmpty(footer.txtReturnofPremium.Text) ? "0.00" : footer.txtReturnofPremium.Text).Replace(Comma, ""), CultureInfo.InvariantCulture);
                    AccidentalDeathInsuredAmount = Decimal.Parse((string.IsNullOrEmpty(txtAccidentalDeathInsuredAmount.Text) ? "0.00" : txtAccidentalDeathInsuredAmount.Text).Replace(Comma, ""), CultureInfo.InvariantCulture);
                    SpouseOtherInsuredAmount = Decimal.Parse((string.IsNullOrEmpty(txtSpouseOtherInsured.Text)?"0.00": txtSpouseOtherInsured.Text).Replace(Comma, ""), CultureInfo.InvariantCulture);
                    CritialIllnessInsuredAmount = Decimal.Parse((string.IsNullOrEmpty(txtCritialIllnessInsuredAmount.Text) ? "0.00" : txtCritialIllnessInsuredAmount.Text).Replace(Comma, ""), CultureInfo.InvariantCulture);
                    AdditionalTermInsuranceInsuredAmount = Decimal.Parse((string.IsNullOrEmpty(txtAdditionalTermInsuranceInsuredAmount.Text) ? "0.00" : txtAdditionalTermInsuranceInsuredAmount.Text).Replace(Comma, ""), CultureInfo.InvariantCulture);
                    InitialContributionAmount = Decimal.Parse((string.IsNullOrEmpty(txtAmount.Text) ? "0.00" : txtAmount.Text).Replace(Comma, ""), CultureInfo.InvariantCulture);
                    FinancialGoalAmount = Decimal.Parse((string.IsNullOrEmpty(txtAmount2.Text) ? "0.00" : txtAmount2.Text).Replace(Comma, ""), CultureInfo.InvariantCulture);
                }
                else
                {
                    AnnualPremium = Decimal.Parse(footer.txtAnnualPremium.Text, CultureInfo.InvariantCulture);
                    TargetPremium = Decimal.Parse(footer.txtTargetAnnualPremium.Text.Replace(Comma, ""), CultureInfo.InvariantCulture);
                    PeriodicPremium = Decimal.Parse(footer.txtPeriodicPremium.Text.Replace(Comma, ""), CultureInfo.InvariantCulture);
                }
            }
            catch (Exception)
            {
                if(footer.txtReturnofPremium != null)
                {
                    if(!string.IsNullOrEmpty(footer.txtReturnofPremium.Text) && !footer.txtReturnofPremium.Text.Equals("0.00"))
                    {
                        ReturnOfPremium = Decimal.Parse(footer.txtReturnofPremium.Text.Replace(",", ""), CultureInfo.InvariantCulture);
                    }
                }
            }

            //Edad del Asegurado
            var contact = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, ObjServices.Contact_Id.Value, languageId: ObjServices.Language.ToInt());
            //Edad del Owner
            var contactOwner = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, ObjServices.Owner_Id.Value, languageId: ObjServices.Language.ToInt());

            if (contact != null)
            {
                Utility.SetContactAge(contact.Dob.Value, ref AgeLimitContact, ref isYear);
                AgeContact = AgeLimitContact;
            }

            ObjServices.isCompanyOwner = (contactOwner != null && contactOwner.ContactTypeId == Utility.ContactTypeId.Company.ToInt());

            if (contactOwner != null && !ObjServices.isCompanyOwner)
            {
                Utility.SetContactAge(contactOwner.Dob.Value, ref AgeLimitOwner, ref isYear);
                AgeOwner = AgeLimitOwner;
            }

            var RangeVal = new List<int>() { 5, 10, 15, 20 };

            //ContributionPeriod = isFuneral ?
            //                     !ddlContributionPeriod.isNullReferenceControl() ? Utility.IsIntReturnNull(ddlContributionPeriod.SelectedValue) : null
            //                     :
            //                     !ddlContributionPeriod.isNullReferenceControl() ? Utility.IsIntReturnNull(ddlContributionPeriod.Text) : null;

            ContributionPeriod = !ddlContributionPeriod.isNullReferenceControl() ? Utility.IsIntReturnNull(ddlContributionPeriod.SelectedValue)
                                                                                 : null;

            DeferringPeriod = !ddlDefermentPeriod.isNullReferenceControl() ? Utility.IsIntReturnNull(ddlDefermentPeriod.SelectedItem.Value)
                                                                           : null;

            RetirementPeriod = !ddlRetirementPeriod.isNullReferenceControl() ? Utility.IsIntReturnNull(ddlRetirementPeriod.SelectedItem.Value)
                                                                             : null;

            MinPremium = !footer.txtAnnualPremium.isNullReferenceControl() ? Utility.IsDecimalReturnNull(footer.txtAnnualPremium.Text)
                                                                           : null;

            PaymentTypeID = !ddlFrequencyofPayment.isNullReferenceControl() ? Utility.IsIntReturnNull(ddlFrequencyofPayment.SelectedValue)
                                                                            : null;

            TotalRetiment = !footer.txtTotalRetirementAmount.isNullReferenceControl() ? Utility.IsDecimalReturnNull(footer.txtTotalRetirementAmount.Text)
                                                                                      : null;

            EducationPeriod = !ddlEducationPeriod.isNullReferenceControl() ? Utility.IsIntReturnNull(ddlEducationPeriod.SelectedValue)
                                                                           : null;

            currency = (Utility.Currency)Utility.getEnumTypeFromValue(typeof(Utility.Currency), Utility.IsIntReturnNull(ddlCurrency.SelectedValue).Value);

            TotalinsuredAmount = !footer.txtInsuredAmount.isNullReferenceControl() ? Utility.IsDecimalReturnNull(footer.txtInsuredAmount.Text.Replace(",", ""))
                                                                                   : null;

            MinPremium = !footer.txtAnnualPremium.isNullReferenceControl() ? Utility.IsDecimalReturnNull(footer.txtAnnualPremium.Text.Replace(",", ""))
                                                                           : null;

            SpecialPaymentConfirmation = !this.ddlSpecialPayment.isNullReferenceControl() && !this.ddlSpecialPayment.SelectedItem.isNullReferenceObject() && !string.IsNullOrEmpty(this.ddlSpecialPayment.SelectedItem.Value) ? this.ddlSpecialPayment.SelectedItem.Value : string.Empty;
            SpecialPaymentAmount = !this.txtSpecialPayment.isNullReferenceControl() ? Utility.IsDecimalReturnNull(this.txtSpecialPayment.Text.Replace(",", "")) : null;
            //FinancingRate = !this.txtFinancingRate.isNullReferenceControl() ? Utility.IsDecimalReturnNull(this.txtFinancingRate.Text.Replace(",", "")) : null;
            DestinyFund = !this.ddlDestinyFund.isNullReferenceControl() && !string.IsNullOrEmpty(this.ddlDestinyFund.SelectedValue) ? this.ddlDestinyFund.SelectedValue : string.Empty;


            //switch (productBehavior)
            //{
            //    case Utility.ProductBehavior.VIDACRED:
            //        var FinancialInstitution = Utility.deserializeJSON<Utility.provider>(ddlFinancialInstitution.SelectedValue);
            //        break;
            //}

            var FinancialInstitution = Utility.deserializeJSON<Utility.provider>(!this.ddlFinancialInstitution.isNullReferenceControl()? ddlFinancialInstitution.SelectedValue : string.Empty);
            //FinancialInstitutionId = !this.ddlFinancialInstitution.isNullReferenceControl() && !string.IsNullOrEmpty(this.ddlFinancialInstitution.SelectedValue) ? this.ddlFinancialInstitution.SelectedValue.IsIntReturnNull() : 0;
            //var FinancialInstitutionId = !this.ddlFinancialInstitution.isNullReferenceControl() && !string.IsNullOrEmpty(this.ddlFinancialInstitution.SelectedValue) ? this.ddlFinancialInstitution.SelectedValue.IsIntReturnNull() : 0;

            //Valores para insured Amount de los planes Funerarios y los de salud
            var dataVal = ObjServices.GettingDropData(
                                                     Utility.DropDownType.ProjectConfigurationValue,
                                                     corpId: ObjServices.Corp_Id,
                                                     pProjectId: int.Parse(System.Configuration.ConfigurationManager.AppSettings["ProjectIdNewBusiness"])
                                                    );
            switch (productBehavior)
            {
                case Utility.ProductBehavior.Horizon:
                    #region Horizon

                    if (MinPremium < 1200)
                    {
                        mensa.AppendLine(Resources.MinimumPremiumIS + " 1,200.00");
                        respuesta = false;
                    }

                    if (ContributionPeriod.HasValue || DeferringPeriod.HasValue)
                    {
                        count = 0;
                        count = count + (ContributionPeriod ?? 0);
                        count = count + (DeferringPeriod ?? 0);
                        if (count < 10 || count > 99)
                        {
                            mensa.AppendLine(@String.Format(Resources.SumoftheContributionPeriodAndDefermentPeriod, "10", "99"));
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(@String.Format(Resources.SumoftheContributionPeriodAndDefermentPeriod, "10", "99"));
                        respuesta = false;

                    }

                    if (RetirementPeriod.HasValue)
                    {
                        count = 0;
                        count = count + (RetirementPeriod.HasValue ? RetirementPeriod.Value : 0);
                        if (RetirementPeriod < 5)
                        {
                            mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.RetirementPeriod + ">= 5");
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.RetirementPeriod + " >= 5");
                        respuesta = false;
                    }
                    if (TotalRetiment.HasValue)
                    {

                        if (TotalRetiment.Value < 6000)
                        {
                            mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalRetirementAmount + " >= 6,000.00");
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalRetirementAmount + " >= 6,000.00");
                        respuesta = false;
                    }
                    #endregion
                    break;
                case Utility.ProductBehavior.Axys:
                    #region Axy
                    if (MinPremium < 1400)
                    {
                        mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.MinimumPremiumIS + " 1,400.00");
                        respuesta = false;
                    }

                    if (ContributionPeriod.HasValue || DeferringPeriod.HasValue)
                    {
                        count = 0;
                        count = count + (ContributionPeriod.HasValue ? ContributionPeriod.Value : 0);
                        count = count + (DeferringPeriod.HasValue ? DeferringPeriod.Value : 0);
                        if (count < 10 || count > 99)
                        {
                            mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.SumoftheContributionPeriodAndDefermentPeriod, "10", "99"));
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.SumoftheContributionPeriodAndDefermentPeriod, "10", "99"));
                        respuesta = false;

                    }

                    if (TotalRetiment.HasValue)
                    {

                        if (TotalRetiment.Value < 6000)
                        {

                            mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalRetirementAmount + " >= 6,000.00");
                            respuesta = false;
                        }
                    }
                    else
                    {

                        mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalRetirementAmount + " >= 6,000.00");
                        respuesta = false;
                    }

                    if (RetirementPeriod.HasValue)
                    {
                        count = 0;
                        count = count + (RetirementPeriod.HasValue ? RetirementPeriod.Value : 0);
                        if (RetirementPeriod < 5)
                        {
                            mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.RetirementPeriod + " >= 5");
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.RetirementPeriod + " >= 5");
                        respuesta = false;
                    }
                    #endregion
                    break;
                case Utility.ProductBehavior.EduPlan:
                    #region Eduplan
                    if (MinPremium.HasValue)
                    {
                        if (MinPremium < 1200)
                        {
                            mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.MinimumPremiumIS + " 1,200");
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.MinimumPremiumIS + " 1,200");
                        respuesta = false;
                    }

                    if (EducationPeriod.HasValue)
                    {
                        if (EducationPeriod < 2 || EducationPeriod > 9)
                        {
                            mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.TotalEducationPeriodfieldMustLess, "2", "9"));
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.TotalEducationPeriodfieldMustLess, "2", "9"));
                        respuesta = false;
                    }



                    if (ContributionPeriod.HasValue || DeferringPeriod.HasValue)
                    {
                        count = 0;
                        count = count + (ContributionPeriod.HasValue ? ContributionPeriod.Value : 0);
                        count = count + (DeferringPeriod.HasValue ? DeferringPeriod.Value : 0);
                        if (count < 9 || count > 99)
                        {
                            mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.SumoftheContributionPeriodAndDefermentPeriod, "9", "99"));
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.SumoftheContributionPeriodAndDefermentPeriod, "9", "99"));
                        respuesta = false;
                    }
                    #endregion
                    break;
                case Utility.ProductBehavior.Scholar:
                    #region Scholar
                    if (MinPremium.HasValue)
                    {
                        if (MinPremium < 1400)
                        {
                            mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.MinimumPremiumIS + " 1,400");
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.MinimumPremiumIS + " 1,400");
                        respuesta = false;
                    }

                    if (EducationPeriod.HasValue)
                    {
                        if (EducationPeriod < 2 || EducationPeriod > 9)
                        {
                            mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.TotalEducationPeriodfieldMustLess, "2", "9"));
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.TotalEducationPeriodfieldMustLess, "2", "9"));
                        respuesta = false;
                    }



                    if (ContributionPeriod.HasValue || DeferringPeriod.HasValue)
                    {
                        count = 0;
                        count = count + (ContributionPeriod.HasValue ? ContributionPeriod.Value : 0);
                        count = count + (DeferringPeriod.HasValue ? DeferringPeriod.Value : 0);
                        if (count < 9 || count > 99)
                        {
                            mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.SumoftheContributionPeriodAndDefermentPeriod, "9", "99"));
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.SumoftheContributionPeriodAndDefermentPeriod, "9", "99"));
                        respuesta = false;
                    }

                    #endregion
                    break;
                case Utility.ProductBehavior.CompassIndex:
                    #region CompassIndex
                    if (ddlContributionType.SelectedValue != "-1")
                    {
                        int AgeResult = 0;
                        //var YearContribution = !txtContributionPeriod.isEmpty() ? txtContributionPeriod.ToInt() : 0;
                        var YearContribution = ddlContributionPeriod.SelectedItem.Text != "----" ? ddlContributionPeriod.ToInt() : 0;
                        var YearsSpouseOther = !txtYearsSpouseOther.isEmpty() ? txtYearsSpouseOther.ToInt() : 0;

                        if (YearsSpouseOther != 0)
                        {
                            switch (ddlContributionType.SelectedValue)
                            {
                                case "1": //CONTINUO
                                    AgeResult = Math.Abs(99 - AgeContact.Value);
                                    break;
                                case "2": //UNTIL AGE Y NUMBER OF YEARS
                                case "3":
                                    AgeResult = Math.Abs(YearContribution - AgeContact.Value);
                                    break;
                            }

                            if (AgeResult < YearsSpouseOther)
                            {
                                mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmountAdditionalInsuredAmount);
                                respuesta = false;
                            }
                        }
                    }

                    if (!txtSpouseOtherInsured.isEmpty())
                    {
                        if (txtSpouseOtherInsured.ToDecimal() > oWUCFieldFooter.txtInsuredAmount.ToDecimal())
                        {
                            mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmountAdditionalInsuredAmount);
                            respuesta = false;
                        }
                    }

                    if (TotalinsuredAmount.HasValue && AgeLimitContact.HasValue)
                    {

                        if ((TotalinsuredAmount.Value < 50000))
                        {
                            mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmountCannotBeLess + " 50,000.00");
                            respuesta = false;
                        }

                        if (TotalinsuredAmount.Value > 2500000)
                        {
                            mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmountCannotBeMore + " 250,0000.00");
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmount + "< 50,000.00");
                        mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmount + " > 250,000.00");
                        respuesta = false;
                    }

                    if (MinPremium.HasValue)
                    {
                        if ((MinPremium.Value < 1200))
                        {
                            mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.MinimumPremiumIS + " 1,400.00");
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.MinimumPremiumIS + " 1,400.00");
                        respuesta = false;
                    }
                    #endregion
                    break;
                case Utility.ProductBehavior.Legacy:
                    #region Legacy
                    if (ddlContributionType.SelectedValue != "-1")
                    {
                        int AgeResult = 0;
                        //var YearContribution = !txtContributionPeriod.isEmpty() ? txtContributionPeriod.ToInt() : 0;
                        var YearContribution = ddlContributionPeriod.SelectedItem.Text != "----" ? ddlContributionPeriod.ToInt() : 0;
                        var YearsSpouseOther = !txtYearsSpouseOther.isEmpty() ? txtYearsSpouseOther.ToInt() : 0;

                        if (YearsSpouseOther != 0)
                        {
                            switch (ddlContributionType.SelectedValue)
                            {
                                case "1": //CONTINUO
                                    AgeResult = Math.Abs(99 - AgeContact.Value);
                                    break;
                                case "2": //UNTIL AGE Y NUMBER OF YEARS
                                case "3":
                                    AgeResult = Math.Abs(YearContribution - AgeContact.Value);
                                    break;
                            }

                            if (AgeResult < YearsSpouseOther)
                            {
                                mensa.AppendLine(Resources.ContributionPeriod);
                                respuesta = false;
                            }
                        }
                    }

                    if (!txtSpouseOtherInsured.isEmpty())
                    {
                        if (txtSpouseOtherInsured.ToDecimal() > oWUCFieldFooter.txtInsuredAmount.ToDecimal())
                        {
                            mensa.AppendLine(Resources.TotalInsuredAmountAdditionalInsuredAmount);
                            respuesta = false;
                        }
                    }

                    if (TotalinsuredAmount.HasValue && AgeLimitContact.HasValue)
                    {

                        if ((TotalinsuredAmount.Value < 50000))
                        {
                            mensa.AppendLine(Resources.TotalInsuredAmountCannotBeLess + " 50,000.00");
                            respuesta = false;
                        }

                        if (TotalinsuredAmount.Value > 250000)
                        {
                            mensa.AppendLine(Resources.TotalInsuredAmountCannotBeMore + " 250,000.00");
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(Resources.TotalInsuredAmount + " < 50,000.00");
                        mensa.AppendLine(Resources.TotalInsuredAmount + " > 250,000.00");
                        respuesta = false;
                    }

                    if (MinPremium.HasValue)
                    {
                        if ((MinPremium.Value < 1400))
                        {
                            mensa.AppendLine(Resources.MinimumPremiumIS + " 1,400.00");
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(Resources.MinimumPremiumIS + " 1,400.00");
                        respuesta = false;
                    }
                    #endregion
                    break;
                case Utility.ProductBehavior.Sentinel:
                    #region Sentinel
                    if (!txtSpouseOtherInsured.isEmpty())
                    {
                        if (txtSpouseOtherInsured.ToDecimal() > oWUCFieldFooter.txtInsuredAmount.ToDecimal())
                        {
                            mensa.AppendLine(Resources.TotalInsuredAmountAdditionalInsuredAmount);
                            respuesta = false;
                        }
                    }

                    if (TotalinsuredAmount.HasValue && AgeLimitContact.HasValue)
                    {

                        if ((TotalinsuredAmount.Value < 50000 && AgeLimitContact < 18))
                        {
                            mensa.AppendLine(@String.Format(Resources.TotalInsuredAmountAndAge, "50,000.00", "18"));
                            respuesta = false;
                        }

                        if ((TotalinsuredAmount.Value < 100000 && AgeLimitContact > 18))
                        {
                            mensa.AppendLine(@String.Format(Resources.TotalInsuredAmountAndAge, "100,000.00", "18"));
                            respuesta = false;
                        }

                        if (TotalinsuredAmount.Value > 250000)
                        {
                            mensa.AppendLine(Resources.TotalInsuredAmountCannotBeMore + @" 250,000.00");
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(@String.Format(Resources.TotalInsuredAmountAndAge, "50,000.00", "18"));
                        mensa.AppendLine(@String.Format(Resources.TotalInsuredAmountAndAge, "100,000.00", "18"));
                        mensa.AppendLine(Resources.TotalInsuredAmountCannotBeMore + " 250,000.00");
                        respuesta = false;
                    }

                    if (MinPremium.HasValue)
                    {
                        if ((MinPremium.Value < 500 && AgeLimitContact > 32))
                        {
                            mensa.AppendLine(@String.Format(Resources.MinimumPremiumISAndAge, "500", "32"));
                            respuesta = false;
                        }

                        if ((MinPremium.Value < 400 && AgeLimitContact < 32))
                        {
                            mensa.AppendLine(@String.Format(Resources.MinimumPremiumISAndAge, "400", "32"));
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(@String.Format(Resources.MinimumPremiumISAndAge, "500", "32"));
                        mensa.AppendLine(@String.Format(Resources.MinimumPremiumISAndAge, "400", "32"));
                        respuesta = false;
                    }
                    #endregion
                    break;
                case Utility.ProductBehavior.Lighthouse:
                    #region Lighthouse
                    if (!txtSpouseOtherInsured.isEmpty())
                    {
                        if (txtSpouseOtherInsured.ToDecimal() > oWUCFieldFooter.txtInsuredAmount.ToDecimal())
                        {
                            mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmountAdditionalInsuredAmount);
                            respuesta = false;
                        }
                    }

                    if (TotalinsuredAmount.HasValue && AgeLimitContact.HasValue)
                    {

                        if ((TotalinsuredAmount.Value < 50000 && AgeLimitContact < 18))
                        {
                            mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmountAndAge, "50,000.00", "18"));
                            respuesta = false;
                        }

                        if ((TotalinsuredAmount.Value < 100000 && AgeLimitContact > 18))
                        {
                            mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmountAndAge, "100,000.00", "18"));
                            respuesta = false;
                        }

                        if (TotalinsuredAmount.Value > 250000)
                        {
                            mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmountCannotBeMore + " 250,000.00");
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmountAndAge, "50,000.00", "18"));
                        mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmountAndAge, "100,000.00 ", "18"));
                        mensa.AppendLine(RESOURCE.UnderWriting.NewBussiness.Resources.TotalInsuredAmountCannotBeMore + " 250,000.00");
                        respuesta = false;
                    }

                    if (MinPremium.HasValue)
                    {
                        if ((MinPremium.Value < 500 && AgeLimitContact > 32))
                        {
                            mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.MinimumPremiumISAndAge, "500", "32"));
                            respuesta = false;
                        }
                        if ((MinPremium.Value < 400 && AgeLimitContact < 32))
                        {
                            mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.MinimumPremiumISAndAge, "400", "32"));
                            respuesta = false;
                        }
                    }
                    else
                    {
                        mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.MinimumPremiumISAndAge, "500", "32"));
                        mensa.AppendLine(@String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.MinimumPremiumISAndAge, "400", "32"));
                        respuesta = false;
                    }


                    #endregion
                    break;
                case Utility.ProductBehavior.Guardian:
                case Utility.ProductBehavior.Orion:
                    #region Guardian & Orion
                    /*
                    Mínimo RD$250,000
                    Máximo RD$1,250,000
                    */

                    if (TotalinsuredAmount < 250000 || TotalinsuredAmount > 1250000)
                    {
                        mensa.AppendLine(Resources.TotalInsuredAmountValidate);
                        respuesta = false;
                    }

                    if (ContributionPeriod.HasValue)
                    {
                        if (ContributionPeriod.Value < txtYearsSpouseOther.ToInt())
                        {
                            mensa.AppendLine(Resources.RidersYearMsjValidation);
                            respuesta = false;
                        }

                        if (productBehavior == Utility.ProductBehavior.Orion)
                        {
                            cpStart = 5;
                            cpEnd = 30;
                            if (ContributionPeriod.Value < cpStart || ContributionPeriod.Value > cpEnd)
                                contributionPeriodValidation = true;
                        }

                        if (productBehavior == Utility.ProductBehavior.Guardian)
                        {
                            cpStart = 10;
                            cpEnd = 30;
                            if (ContributionPeriod.Value < cpStart || ContributionPeriod.Value > cpEnd)
                                contributionPeriodValidation = true;
                        }
                    }

                    if (contributionPeriodValidation)
                    {
                        mensa.AppendLine(string.Format(Resources.ContributionPeriodValidation, cpStart, cpEnd));
                        respuesta = false;
                    }

                    if (AnnualPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.AnnualPremiumLabel.InnerText));
                        respuesta = false;
                    }

                    if (TargetPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.TargetPremiumLabel.InnerText));
                        respuesta = false;
                    }

                    if (PeriodicPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.PeriodicPremiumLabel.InnerText));
                        respuesta = false;
                    }

                    if (ReturnOfPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.ReturnOfPremiumLabel.InnerText));
                        respuesta = false;
                    }

                    if (AccidentalDeathInsuredAmount > TotalinsuredAmount)
                    {

                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, AccidentalDeathBenefitsLabel.InnerText));
                        respuesta = false;
                    }

                    if (SpouseOtherInsuredAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, SpouseOtherInsuredLabel.InnerText));
                        respuesta = false;
                    }

                    if (CritialIllnessInsuredAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, CriticalIllnessLabel.InnerText));
                        respuesta = false;
                    }

                    if (AdditionalTermInsuranceInsuredAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, AdditionalTermInsuranceLabel.InnerText));
                        respuesta = false;
                    }

                    if (InitialContributionAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, InitialContributionPeriodLabel.InnerText));
                        respuesta = false;
                    }

                    if (FinancialGoalAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, FinancialGoal.InnerText));
                        respuesta = false;
                    }

                    if (isFuneral)
                    {
                        if (InitialContributionAmount > TotalinsuredAmount)
                        {
                            mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, InitialContributionPeriodLabel.InnerText));
                            respuesta = false;
                        }
                    }

                    #endregion
                    break;
                case Utility.ProductBehavior.GuardianPlus:
                case Utility.ProductBehavior.OrionPlus:
                    #region Guardian Plus & Orion Plus
                    /*
                    Monto asegurado
                    Mínimo: RD$1,250,000 (US$25,000)
                    Máximo: RD$90,000,000 (US$2,000,000)                              
                    Disponible para personas desde 3 meses hasta 65
                    años de edad.
                    */

                    if (currency == Utility.Currency.DOP)
                    {
                        if (TotalinsuredAmount < 1250000 || TotalinsuredAmount > 90000000)
                        {
                            mensa.AppendLine(Resources.GuardianOrionPlusValidationDOP);
                            respuesta = false;
                        }
                    }
                    else if (currency == Utility.Currency.USD)
                    {
                        if (TotalinsuredAmount < 25000 || TotalinsuredAmount > 2000000)
                        {
                            mensa.AppendLine(Resources.GuardianOrionPlusValidationUSD);
                            respuesta = false;
                        }
                    }

                    contributionPeriodValidation = false;

                    if (ContributionPeriod.HasValue)
                    {
                        if (productBehavior == Utility.ProductBehavior.OrionPlus)
                        {
                            cpStart = 5;
                            cpEnd = 30;
                            if (ContributionPeriod.Value < cpStart || ContributionPeriod.Value > cpEnd)
                                contributionPeriodValidation = true;
                        }

                        if (productBehavior == Utility.ProductBehavior.GuardianPlus)
                        {
                            cpStart = 10;
                            cpEnd = 30;
                            if (ContributionPeriod.Value < cpStart || ContributionPeriod.Value > cpEnd)
                                contributionPeriodValidation = true;
                        }
                    }

                    if (contributionPeriodValidation)
                    {
                        mensa.AppendLine(string.Format(Resources.ContributionPeriodValidation, cpStart, cpEnd));
                        respuesta = false;
                    }


                    if (AccidentalDeathInsuredAmount > TotalinsuredAmount)
                    {

                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, AccidentalDeathBenefitsLabel.InnerText));
                        respuesta = false;
                    }

                    if (SpouseOtherInsuredAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, SpouseOtherInsuredLabel.InnerText));
                        respuesta = false;
                    }

                    if (CritialIllnessInsuredAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, CriticalIllnessLabel.InnerText));
                        respuesta = false;
                    }

                    if (AdditionalTermInsuranceInsuredAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, AdditionalTermInsuranceLabel.InnerText));
                        respuesta = false;
                    }

                    if (InitialContributionAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, InitialContributionPeriodLabel.InnerText));
                        respuesta = false;
                    }

                    if (FinancialGoalAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, FinancialGoal.InnerText));
                        respuesta = false;
                    }
                    if (ReturnOfPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.ReturnOfPremiumLabel.InnerText));
                        respuesta = false;
                    }
                    if (TargetPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.TargetPremiumLabel.InnerText));
                        respuesta = false;
                    }
                    if (AnnualPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.AnnualPremiumLabel.InnerText));
                        respuesta = false;
                    }
                    if (PeriodicPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.PeriodicPremiumLabel.InnerText));
                        respuesta = false;
                    }

                    //Bmarroquin 15-03-2017 Validar que las coberturas ITP y MA tengan el monto asegurado que la cobertura Basica
                    var Comma = ",";

                    //Muerte Accidental
                    /*
                    if (ddlAccidentalDeathBenefits.SelectedValue == "1") //Significa Si
                    {
                        decimal? lNum_MontoaValidar,lNum_SumaAsegMA;
                        bool expresion = false;

                        if (txtMontoAseguradoCorBasica != null)
	                    {
                            expresion = string.IsNullOrWhiteSpace(txtMontoAseguradoCorBasica.Text);
                        }
                        else
                        {
                            expresion = true;
                        }
                        
                        if (expresion == false)
                        {
                            lNum_MontoaValidar = Utility.IsDecimalReturnNull(txtMontoAseguradoCorBasica.Text.Replace(Comma, ""));
                        }
                        else
                        {
                            lNum_MontoaValidar = TotalinsuredAmount;
                        }

                        if (lNum_MontoaValidar.HasValue)
                        {
                            if (string.IsNullOrWhiteSpace(txtAccidentalDeathInsuredAmount.Text) == false)
                            {
                                lNum_SumaAsegMA = Utility.IsDecimalReturnNull(txtAccidentalDeathInsuredAmount.Text.Replace(Comma, ""));
                                if (lNum_SumaAsegMA.HasValue)
                                {
                                    if (lNum_MontoaValidar != lNum_SumaAsegMA)
                                    {
                                        mensa.AppendLine("La suma asegurada del suplemento muerte accidental y desmembramiento no puede ser diferente a la suma asegurada de cobertura base");
                                        respuesta = false;
                                    }
                                }
                                else
                                {
                                    mensa.AppendLine("El valor de suma asegurada de muerte accidental no es numerico, favor ingreselo");
                                    respuesta = false;
                                }
                            }
                            else
                            {
                                mensa.AppendLine("No se ha podido determinar el monto de suma asegurada de muerte accidental, favor ingresela");
                                respuesta = false;
                            }
                        }
                        else
                        {
                            mensa.AppendLine("No se ha podido determinar el monto de suma asegurada de cobertura base, favor ingresela dado que es necesario para validar la suma asegurada de la cobertura muerte accidental");
                            respuesta = false;
                        }
                    }
                    */

                    //Invalidez Total 
                    if (ddlCritialIllness.SelectedValue == "1") //Significa Si
                    {
                        decimal? lNum_MontoaValidar, lNum_SumaAsegITP;

                        if (string.IsNullOrWhiteSpace(txtMontoAseguradoCorBasica.Text) == false)
                        {
                            lNum_MontoaValidar = Utility.IsDecimalReturnNull(txtMontoAseguradoCorBasica.Text.Replace(Comma, ""));
                        }
                        else
                        {
                            lNum_MontoaValidar = TotalinsuredAmount;
                        }

                        if (lNum_MontoaValidar.HasValue)
                        {
                            if (string.IsNullOrWhiteSpace(txtCritialIllnessInsuredAmount.Text) == false)
                            {
                                lNum_SumaAsegITP = Utility.IsDecimalReturnNull(txtCritialIllnessInsuredAmount.Text.Replace(Comma, ""));
                                if (lNum_SumaAsegITP.HasValue)
                                {
                                    if (lNum_MontoaValidar != lNum_SumaAsegITP)
                                    {
                                        mensa.AppendLine("La suma asegurada del suplemento invalidez total y permanente no puede ser diferente a la suma asegurada de cobertura base");
                                        respuesta = false;
                                    }
                                }
                                else
                                {
                                    mensa.AppendLine("El valor de suma asegurada de invalidez total y permanente no es numerico, favor ingreselo");
                                    respuesta = false;
                                }
                            }
                            else
                            {
                                mensa.AppendLine("No se ha podido determinar el monto de suma asegurada de invalidez total y permanente, favor ingresela");
                                respuesta = false;
                            }
                        }
                        else
                        {
                            mensa.AppendLine("No se ha podido determinar el monto de suma asegurada de cobertura base, favor ingresela dado que es necesario para validar la suma asegurada de la cobertura invalidez total y permanente");
                            respuesta = false;
                        }
                    }

                    //Gastos Funerarios, validar minimos y maximos de sumas aseguradas
                    if (ddlAdditionalTermInsurance.SelectedValue == "1")
                    {
                        decimal?  lNum_SumaAsegGF;
                        if (string.IsNullOrWhiteSpace(txtAdditionalTermInsuranceInsuredAmount.Text) == false)
                        {
                            lNum_SumaAsegGF = Utility.IsDecimalReturnNull(txtAdditionalTermInsuranceInsuredAmount.Text.Replace(Comma, ""));
                            if (lNum_SumaAsegGF.HasValue)
                            {
                                //Bmaloquin 22-03-2017 el monto minimo de gastos funerarios debe ser $1,000.00                                
                                if (lNum_SumaAsegGF < 1000)
                                {
                                    mensa.AppendLine("La suma asegurada de Gastos Funerarios no puede ser menor que $1,000.00");
                                    respuesta = false;
                                }

                                if (lNum_SumaAsegGF > 2500)
                                {
                                    mensa.AppendLine("La suma asegurada de Gastos Funerarios no puede ser mayor que $2,500.00");
                                    respuesta = false;
                                }

                            }
                            else
                            {
                                mensa.AppendLine("El valor de suma asegurada de gastos funerarios no es numerico, favor ingreselo");
                                respuesta = false;
                            }
                        }
                        else
                        {
                            mensa.AppendLine("No se ha podido determinar el monto de suma asegurada de gastos funerarios, favor ingresela");
                            respuesta = false;
                        }

                    }
                    //Fin Cambio Bmarroquin 15-03-2017
                    #endregion
                    break;

                case Utility.ProductBehavior.VIDACRED:
                    #region VIDACRED
                    /*
                    Monto asegurado
                    Mínimo: RD$1,250,000 (US$25,000)
                    Máximo: RD$90,000,000 (US$2,000,000)                              
                    Disponible para personas desde 3 meses hasta 65
                    años de edad.
                    */
                    if (currency == Utility.Currency.DOP)
                    {

                        if (TotalinsuredAmount < 10000 || TotalinsuredAmount > 50000000)
                        {
                            mensa.AppendLine(Resources.VidaCreditValidationDOP);
                            respuesta = false;
                        }
                    }
                    else if (currency == Utility.Currency.USD)
                    {
                        if (TotalinsuredAmount < 215 || TotalinsuredAmount > 1077711)
                        {
                            mensa.AppendLine(Resources.VidaCreditValidationUSD);
                            respuesta = false;
                        }
                    }

                    contributionPeriodValidation = false;

                    if (ContributionPeriod.HasValue)
                    {
                        //Hace referencia a numero de meses
                        if (this.ddlContributionType.SelectedValue == "4")
                        {
                            cpStart = 6;
                            if (ContributionPeriod.Value < cpStart)
                            {
                                mensa.AppendLine(string.Format(Resources.ContributionPeriodValidationMonths));
                                respuesta = false;
                                return respuesta;
                            }

                        }
                        else
                        {
                            cpStart = 5;
                            cpEnd = 30;
                            if (ContributionPeriod.Value < cpStart || ContributionPeriod.Value > cpEnd)
                                contributionPeriodValidation = true;
                        }

                    }

                    if (contributionPeriodValidation)
                    {
                        mensa.AppendLine(string.Format(Resources.ContributionPeriodValidation, cpStart, cpEnd));
                        respuesta = false;
                    }


                    if (AccidentalDeathInsuredAmount > TotalinsuredAmount)
                    {

                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, AccidentalDeathBenefitsLabel.InnerText));
                        respuesta = false;
                    }

                    if (SpouseOtherInsuredAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, SpouseOtherInsuredLabel.InnerText));
                        respuesta = false;
                    }

                    if (CritialIllnessInsuredAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, CriticalIllnessLabel.InnerText));
                        respuesta = false;
                    }

                    if (AdditionalTermInsuranceInsuredAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, AdditionalTermInsuranceLabel.InnerText));
                        respuesta = false;
                    }

                    if (InitialContributionAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, InitialContributionPeriodLabel.InnerText));
                        respuesta = false;
                    }

                    if (FinancialGoalAmount > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, FinancialGoal.InnerText));
                        respuesta = false;
                    }
                    if (ReturnOfPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.ReturnOfPremiumLabel.InnerText));
                        respuesta = false;
                    }
                    if (TargetPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.TargetPremiumLabel.InnerText));
                        respuesta = false;
                    }
                    if (AnnualPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.AnnualPremiumLabel.InnerText));
                        respuesta = false;
                    }
                    if (PeriodicPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.PeriodicPremiumLabel.InnerText));
                        respuesta = false;
                    }
                    if (SpecialPaymentConfirmation == Resources.YesLabel.ToUpper())
                    {
                        if (SpecialPaymentAmount.isNullReferenceObject() || !SpecialPaymentAmount.HasValue || SpecialPaymentAmount.Value <= 0)
                        {
                            mensa.AppendLine(string.Format(Resources.VidaCreditValidationNotHaveSpecialPayment));
                            respuesta = false;
                        }
                    }
                    if (FinancingRate.isNullReferenceObject() || !FinancingRate.HasValue || FinancingRate.Value <= 0)
                    {
                        mensa.AppendLine(string.Format(Resources.VidaCreditValidationFinancingRate));
                        respuesta = false;
                    }
                    if (DestinyFund == string.Empty)
                    {
                        mensa.AppendLine(string.Format(Resources.VidaCreditValidationDestinyFund));
                        respuesta = false;
                    }

                    if (FinancialInstitution.isNullReferenceObject() || !FinancialInstitution.ProviderId.HasValue || !FinancialInstitution.ProviderTypeId.HasValue)
                    {
                        mensa.AppendLine(string.Format(Resources.VidaCreditValidationFinancialInstitution));
                        respuesta = false;
                    }
                    #endregion
                    break;
                case Utility.ProductBehavior.Luminis:
                    #region Luminis Basico
                    if (ContributionPeriod.HasValue)
                        contributionPeriodValidation = (!RangeVal.Where(x => x == ContributionPeriod.Value).Any());

                    if (contributionPeriodValidation)
                    {
                        mensa.AppendLine("");
                        respuesta = false;
                    }
                    if (TargetPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.TargetPremiumLabel.InnerText));
                        respuesta = false;
                    }

                    if (PeriodicPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.PeriodicPremiumLabel.InnerText));
                        respuesta = false;
                    }
                    #endregion
                    break;
                case Utility.ProductBehavior.LuminisVIP:
                    #region LuminisVIP

                    if (ContributionPeriod.HasValue)
                    {
                        contributionPeriodValidation = (!RangeVal.Where(x => x == ContributionPeriod.Value).Any());
                    }

                    if (contributionPeriodValidation)
                    {
                        mensa.AppendLine(Resources.ContributionPeriodFuneralValidation);
                        respuesta = false;
                    }
                    if (TargetPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.TargetPremiumLabel.InnerText));
                        respuesta = false;
                    }

                    if (PeriodicPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.PeriodicPremiumLabel.InnerText));
                        respuesta = false;
                    }

                    #endregion
                    break;
                case Utility.ProductBehavior.Exequium:
                    #region Exequium Basico

                    if (ContributionPeriod.HasValue)
                    {
                        contributionPeriodValidation = (!RangeVal.Where(x => x == ContributionPeriod.Value).Any());
                    }

                    if (contributionPeriodValidation)
                    {
                        mensa.AppendLine(Resources.ContributionPeriodFuneralValidation);
                        respuesta = false;
                    }
                    if (TargetPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.TargetPremiumLabel.InnerText));
                        respuesta = false;
                    }

                    if (PeriodicPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.PeriodicPremiumLabel.InnerText));
                        respuesta = false;
                    }
                    #endregion
                    break;
                case Utility.ProductBehavior.ExequiumVIP:
                    #region ExequiumVIP

                    if (ContributionPeriod.HasValue)
                    {
                        contributionPeriodValidation = (!RangeVal.Where(x => x == ContributionPeriod.Value).Any());
                    }

                    if (contributionPeriodValidation)
                    {
                        mensa.AppendLine(Resources.ContributionPeriodFuneralValidation);
                        respuesta = false;
                    }
                    if (TargetPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.TargetPremiumLabel.InnerText));
                        respuesta = false;
                    }

                    if (PeriodicPremium > TotalinsuredAmount)
                    {
                        mensa.AppendLine(string.Format(Resources.TotalInsuredAmountValidation, footer.PeriodicPremiumLabel.InnerText));
                        respuesta = false;
                    }
                    #endregion
                    break;
                case Utility.ProductBehavior.Serenity:
                    var listaval = dataVal
                                   .Where(x => x.Namekey
                                                .Contains("InsuranceSerenity"))
                                                .Select(d => Decimal.Parse(d.ConfigurationValue))
                                                .ToList();

                    if (!listaval.Contains(TotalinsuredAmount.Value))
                    {
                        mensa.AppendLine(string.Format(Resources.ValidationAmount, string.Join(" , ", listaval.ToArray())));
                        respuesta = false;
                    }

                    break;
                case Utility.ProductBehavior.Asistencia30dias:
                    var listaval2 = dataVal
                                   .Where(x => x.Namekey
                                                .Contains("InsuranceAsistencia30dias"))
                                                .Select(d => Decimal.Parse(d.ConfigurationValue))
                                                .ToList();

                    if (!listaval2.Contains(TotalinsuredAmount.Value))
                    {
                        mensa.AppendLine(string.Format(Resources.ValidationAmount, string.Join(" , ", listaval2.ToArray())));
                        respuesta = false;
                    }

                    break;
                case Utility.ProductBehavior.Asistencia60dias:
                    var listaval3 = dataVal
                                    .Where(x => x.Namekey
                                                 .Contains("InsuranceAsistencia60dias"))
                                                 .Select(d => Decimal.Parse(d.ConfigurationValue))
                                                 .ToList();

                    if (!listaval3.Contains(TotalinsuredAmount.Value))
                    {
                        mensa.AppendLine(string.Format(Resources.ValidationAmount, string.Join(" , ", listaval3.ToArray())));
                        respuesta = false;
                    }

                    break;
                case Utility.ProductBehavior.Asistencia90dias:
                    var listaval4 = dataVal
                                    .Where(x => x.Namekey
                                                 .Contains("InsuranceAsistencia90dias"))
                                                 .Select(d => Decimal.Parse(d.ConfigurationValue))
                                                 .ToList();

                    if (!listaval4.Contains(TotalinsuredAmount.Value))
                    {
                        mensa.AppendLine(string.Format(Resources.ValidationAmount, string.Join(" , ", listaval4.ToArray())));
                        respuesta = false;
                    }

                    break;
                default:
                    break;
            }

            /*
             Elite	    $3,200,000 
             Select	    $2,200,000 
             Fortis   	$2,200,000 
             Serenity	$50,000 
                        $25,000 
             Asistencia al Viajero hasta 90 días	
                         $250,000 
                         $100,000 
                         $50,000 
                         $25,000 
             Asistencia al Viajero Anual - 30 días continuos
                         $250,000 
                         $100,000 
                         $50,000 
             Asistencia al Viajero Anual - 60 días continuos	
                         $250,000 
                         $100,000 
                         $50,000 
             */


            /*Validar Edad del Asegurado*/
            if (ObjServices.Contact_Id == ObjServices.Owner_Id)
                ValidateAge(isYear, false, AgeContact, ref mensa, ref respuesta);
            else
            {
                ValidateAge(isYear, false, AgeContact, ref mensa, ref respuesta);

                //Si el owner de la poliza no es una compañia validar la edad
                if (!ObjServices.isCompanyOwner)
                    ValidateAge(isYear, true, AgeOwner, ref mensa, ref respuesta);
            }

            /*
             Luminis Basico $125,000 / Luminis VIP $200,000 / Exequium Básico $75,000 / Exequium VIP $140,000              
             La suma asegurada debe de colocarse automáticamente dependiendo del plan seleccionado: Luminis Basico $125,000
             Luminis VIP $200,000 
             Exequium Básico $75,000
             Exequium VIP $140,000
             */

            if (!respuesta)
            {
                ReturnOfPremiumBk = ReturnOfPremium; //Guardamos en variable miembro el valor del retorno de prima

                var Msgarray = mensa.ToString().Split('\n');
                var msg = string.Empty;

                foreach (var item in Msgarray)
                    if (!string.IsNullOrEmpty(item))
                        msg += item.Replace("\r", "") + "<br/>" + "<br/>";

                this.MessageBox(msg.ToString(), Title: Resources.Warning);
            }

            return respuesta;
        }

        public void save()
        {
            saveAll();
        }
        /// <summary>
        /// Method created to persitir validation of new business
        /// </summary>
        /// <returns></returns>
        public bool saveOtherProcess()
        {
            var result = false;
            result = saveAll();
            return result;

        }
        public bool saveAll()
        {
            setVariables();
            setControls();
            /*PRODUCTO SELECCIONADO*/
            var ProductSelect = Utility.deserializeJSON<Utility.itemProduct>(ddlProductName.SelectedValue); 

            var ProductBehavior = (Utility.ProductBehavior)Utility.getvalueFromEnumType(ProductSelect.NameKey, typeof(Utility.ProductBehavior));

            var isFuneral = ObjServices.ProductLine == Utility.ProductLine.Funeral;

            var respuesta = ValidationPolicy(ProductSelect.Product);
            if (respuesta)
            {
                #region POLICY DATA SAVER
                var datos = ObjServices.oPolicyManager.GetPlanData(CorpId, RegionId, CountryId, DomesticregId, StateProvId
                    , CityId, OfficeId, CaseSeqNo, HistSeqNo);

                var pData = ObjServices.oPolicyManager.GetPolicy(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo);

                /*KEY*/
                pData.CorpId = CorpId;
                pData.RegionId = RegionId;
                pData.CountryId = CountryId;
                pData.DomesticregId = DomesticregId;
                pData.StateProvId = StateProvId;
                pData.CityId = CityId;
                pData.OfficeId = OfficeId;
                pData.CaseSeqNo = CaseSeqNo;
                pData.HistSeqNo = HistSeqNo;

                /*MODIFY FIELD*/
                pData.BussinessLineId = ProductSelect.BlId;
                pData.BussinessLineType = ProductSelect.BlTypeId;
                pData.ProductId = ProductSelect.ProductId;

                if (ddlCurrency != null)
                    pData.CurrencyId = (Utility.IsInt(ddlCurrency.SelectedValue) ? int.Parse(ddlCurrency.SelectedValue) : new Nullable<int>());

                if (!isFuneral)
                {
                    if (ddlContributionPeriod != null)
                    {

                        //pData.ContributionYears = Utility.IsIntReturnNull(txtContributionPeriod.Text);
                        pData.ContributionYears = Utility.IsIntReturnNull(ddlContributionPeriod.SelectedValue);
                        pData.InsuredPeriod = pData.ContributionYears;
                    }
                }
                else
                {
                    if (ddlContributionPeriod != null)
                    {
                        pData.ContributionYears = Utility.IsIntReturnNull(ddlContributionPeriod.SelectedValue);
                        pData.InsuredPeriod = pData.ContributionYears;
                    }
                }

                if (txtEffectiveDate != null)
                    pData.PolicyEffectiveDate = txtEffectiveDate.ToDateTime();

                /*PARA LOS CASOS DE RETIRO Y EDUTION PERIOD EN LA BASE DE DATOS SE UTILIZA */
                if (ddlRetirementPeriod != null)
                    pData.RetirementPeriod = (Utility.IsInt(ddlRetirementPeriod.SelectedValue) ? int.Parse(ddlRetirementPeriod.SelectedValue) : new Nullable<int>());

                if (ddlEducationPeriod != null)
                    pData.RetirementPeriod = (Utility.IsInt(ddlEducationPeriod.SelectedValue) ? int.Parse(ddlEducationPeriod.SelectedValue) : new Nullable<int>());

                if (ddlDefermentPeriod != null)
                    pData.DefermentPeriod = Utility.IsIntReturnNull(ddlDefermentPeriod.SelectedValue);

                if (ddlContributionType != null)
                {
                    pData.ContributionTypeId = ddlContributionType.ToInt();

                    var Val = 0;

                    if (ddlContributionType.SelectedValue == "2" || ddlContributionType.SelectedValue == "3")
                        //Val = (!isFuneral) ? txtContributionPeriod.ToInt() : ddlContributionPeriod.ToInt();
                        Val = ddlContributionPeriod.ToInt();
                    pData.ContributionYears = Val;
                    pData.InsuredPeriod = Val;
                }

                if (ObjServices.ProductLine == Utility.ProductLine.HealthInsurance)
                {
                    pData.DeductibleTypeId = (ddlDeducibleType != null) ?
                        ddlDeducibleType.ToInt() : hdnDeductibleTypeID.ToInt();

                    if (ddlDeducible != null)
                    {
                        var DeductibleData = Utility.deserializeJSON<Utility.itemDeductible>(ddlDeducible.SelectedValue);
                        var DeductibleCategoryId = DeductibleData.DeductibleCategoryId;
                        pData.DeductibleCategoryId = DeductibleCategoryId;
                    }
                }


                if (txtAmount2 != null)
                    pData.GoalAmount = Utility.IsDecimalReturnNull(txtAmount2.Text.Replace(",", ""));

                pData.InitialContribution = Utility.IsDecimalReturnNull(txtAmount.Text.Replace(",", ""));

                if (txtAtAge != null)
                    pData.GoalAtAge = Utility.IsDecimalReturnNull(txtAtAge.Text);

                if (ddlPlanType != null && ddlPlanType.SelectedValue != "-1")
                    pData.PolicySerieId = (Utility.IsInt(ddlPlanType.SelectedValue) ? int.Parse(ddlPlanType.SelectedValue) : 0);

                switch (ProductBehavior)
                {
                    case Utility.ProductBehavior.Guardian:
                    case Utility.ProductBehavior.GuardianPlus:
                        pData.PolicySerieId = 4;
                        break;
                    case Utility.ProductBehavior.Orion:
                    case Utility.ProductBehavior.OrionPlus:
                        pData.PolicySerieId = 3;
                        break;
                    case Utility.ProductBehavior.Luminis:
                    case Utility.ProductBehavior.LuminisVIP:
                    case Utility.ProductBehavior.Exequium:
                    case Utility.ProductBehavior.ExequiumVIP:
                        pData.PolicySerieId = 30;
                        break;
                    case Utility.ProductBehavior.VIDACRED:
                        pData.PolicySerieId = 3;
                        var FinancialInstitution = Utility.deserializeJSON<Utility.provider>(ddlFinancialInstitution.SelectedValue);
                        pData.ProviderTypeId = FinancialInstitution.ProviderTypeId.Value;
                        pData.ProviderId = FinancialInstitution.ProviderId.Value;
                        pData.ContributionTypeId = ddlContributionType.ToInt();
                        int contributionyears = 0;
                        var contributionMonths = 0;
                        //Hace referencia al tipo de contribucion Numero de Meses.
                        if (pData.ContributionTypeId == 4)
                        {
                            contributionyears = ddlContributionPeriod.ToInt() / 12;
                            contributionMonths = ddlContributionPeriod.ToInt() % 12;
                            pData.ContributionMonths = contributionMonths;
                            pData.ContributionYears = contributionyears;
                            pData.InsuredPeriod = pData.ContributionYears;
                            pData.MonthsInsuredPeriod = pData.ContributionMonths;

                        }
                        else
                        {
                            pData.ContributionYears = ddlContributionPeriod.ToInt();
                            pData.InsuredPeriod = pData.ContributionYears;
                            pData.ContributionMonths = 0;
                            pData.MonthsInsuredPeriod = 0;
                        }
                        break;
                }

                if (datos != null)
                {
                    if (ddlFrequencyofPayment != null && ddlFrequencyofPayment.SelectedValue != "-1")
                    {
                        var freg = new Entity.UnderWriting.Entities.Policy.PaymentFrequency();
                        freg.CorpId = CorpId;
                        freg.RegionId = RegionId;
                        freg.CountryId = CountryId;
                        freg.DomesticregId = DomesticregId;
                        freg.StateProvId = StateProvId;
                        freg.CityId = CityId;
                        freg.OfficeId = OfficeId;
                        freg.CaseSeqNo = CaseSeqNo;
                        freg.HistSeqNo = HistSeqNo;
                        freg.PaymentFreqTypeId = int.Parse(ddlFrequencyofPayment.SelectedValue);
                        freg.UserId = ObjServices.UserID.Value;
                        ObjServices.oPolicyManager.InsertPaymentFrequency(freg);
                    }

                    if (ddlInvestmentProfile != null && ddlInvestmentProfile.SelectedValue != "-1")
                    {
                        var InsItem = Utility.deserializeJSON<Utility.ProfileType>(ddlInvestmentProfile.SelectedValue);

                        var invs = new Entity.UnderWriting.Entities.Policy.InvestProfile();

                        invs.CorpId = CorpId;
                        invs.RegionId = RegionId;
                        invs.CountryId = CountryId;
                        invs.DomesticregId = DomesticregId;
                        invs.StateProvId = StateProvId;
                        invs.CityId = CityId;
                        invs.OfficeId = OfficeId;
                        invs.CaseSeqNo = CaseSeqNo;
                        invs.HistSeqNo = HistSeqNo;
                        invs.InvestmentProductDate = DateTime.Now;
                        invs.ProfileTypeId = InsItem.ProfileTypeId;
                        invs.UserId = ObjServices.UserID.Value;
                        invs.InvestProductDateId = int.Parse
                            (
                               DateTime.Now.Year.ToString()

                            + (DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString())

                            + (DateTime.Now.Day.ToString().Length == 1 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString())

                            );

                        invs.InvstProfileDesc = ddlInvestmentProfile.SelectedValue;
                        ObjServices.oPolicyManager.InsertInvestmentProfile(invs);
                    }

                    ///INITIAL PREMIUM
                    var initial_premium = 0.00m;
                    if (datos.PaymentFreqTypeId != null || (ddlFrequencyofPayment != null && ddlFrequencyofPayment.SelectedValue != "-1"))
                    {
                        var Comma = ",";
                        var values = ((WUCFieldFooter)this.Parent.FindControl("WUCFieldFooter"));
                        values.setControls();
                        var freq = (datos.PaymentFreqTypeId ?? int.Parse(ddlFrequencyofPayment.SelectedValue));
                        var PeriodicPremium = Decimal.Parse((string.IsNullOrEmpty(values.txtPeriodicPremium.Text) ? "0.00" : values.txtPeriodicPremium.Text).Replace(Comma, ""), CultureInfo.InvariantCulture);
                        var AnnualPremium = Decimal.Parse((string.IsNullOrEmpty(values.txtAnnualPremium.Text) ? "0.00" : values.txtAnnualPremium.Text).Replace(Comma, ""), CultureInfo.InvariantCulture);
                        switch (freq)
                        {
                            case 1:
                                initial_premium = PeriodicPremium * 4 + (Utility.IsDecimalReturnNull(txtAmount.Text.Replace(",", "")) ?? 0);
                                break;
                            case 2:
                                initial_premium = PeriodicPremium * 12 + (Utility.IsDecimalReturnNull(txtAmount.Text.Replace(",", "")) ?? 0);
                                break;
                            case 4:
                                initial_premium = PeriodicPremium * 2 + (Utility.IsDecimalReturnNull(txtAmount.Text.Replace(",", "")) ?? 0);
                                break;
                            case 3:
                            case 0:
                                initial_premium = PeriodicPremium + (Utility.IsDecimalReturnNull(txtAmount.Text.Replace(",", "")) ?? 0);
                                break;
                            default:
                                break;
                        }
                        initial_premium = PeriodicPremium + (Utility.IsDecimalReturnNull(txtAmount.Text.Replace(",", "")) ?? 0.00m);
                    }
                    else
                    {
                        initial_premium = 0.00m;
                    }
                    pData.InitialPremium = initial_premium;
                }
                /*falta grabar*/
                //ddlRisk
                //ddlPerThousand  

                //Bmarroquin 29-04-2017 cambio pa que guarde el Recargo Fraccionado  
                var footer = ((WUCFieldFooter)this.Parent.FindControl("WUCFieldFooter"));
                footer.setControls();

                if (footer.txtFraccionamiento != null)
                {
                    if (!string.IsNullOrEmpty(footer.txtFraccionamiento.Text) && !footer.txtFraccionamiento.Text.Equals("0.00"))
                    {
                        pData.Fraction_Surcharge = Decimal.Parse(footer.txtFraccionamiento.Text.Replace(",", ""), CultureInfo.InvariantCulture);
                    }
                }
                //Fin Bmarroquin 29-04-2017

                //Bmarroquin 05-05-2017 cambio pa que guarde la prima Neta o comercial
                if (footer.txtPrimaAnualNeta != null)
                {
                    if (!string.IsNullOrEmpty(footer.txtPrimaAnualNeta.Text) && !footer.txtPrimaAnualNeta.Text.Equals("0.00"))
                    {
                        pData.Net_Annual_Premium = Decimal.Parse(footer.txtPrimaAnualNeta.Text.Replace(",", ""), CultureInfo.InvariantCulture);
                    }
                }
                //Fin Bmarroquin 05-05-2017

                //pData.Fraction_Surcharge = 
                ObjServices.oPolicyManager.UpdatePolicy(pData);

                /* Ya no procede el cambio dado que se puede dar el escenario que se dupliquen los numeros de cotizacion...Revienta una UNIQUECONSTRAINT !!
                 * Dejo el codigo para futuras implementacionees
                //Bmarroquin 07-04-2017 Fix para Issue, cuando se adjunta la cotizacion desde IllusData el numero de cotizacion no se toma de IllusData sino que se esta generando uno nuevo...
                if (string.IsNullOrWhiteSpace(hdnNumCotizacionIllusData.Value) == false)
                {
                    if (hdnNumCotizacionIllusData.Value != "-1")
                    {
                        ObjServices.updateCotizacionNumber(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, ObjServices.UserID.Value, hdnNumCotizacionIllusData.Value);
                        //Luego limpio para que no se este actulizando N veces...
                        hdnNumCotizacionIllusData.Value = string.Empty;
                    }
                }
                */
                #endregion

                #region STUDENTS DATA SAVE
                /*esto es la parte de estudiante*/
                if (hfSelectControls.Value == "VEduplan" || hfSelectControls.Value == "VScholar")
                {
                    txtStudentName = ((TextBox)Controles.FindControl("txtStudentName"));
                    txtAge = ((TextBox)Controles.FindControl("txtAge"));


                    if (ObjServices.StudentContactId.HasValue)
                    {
                        if (ObjServices.StudentContactId <= 0)
                        {
                            /*SI EL STUDENT NO EXISTE LO CREO*/
                            ObjServices.StudentContactId = ObjServices.oPolicyManager.AddContactToPolicy(CorpId, RegionId, CountryId, DomesticregId, StateProvId
                             , CityId, OfficeId, CaseSeqNo, HistSeqNo, -1/*esto es cuando es nuevo*/
                             , Utility.ContactTypeId.Contact.ToInt() /*1*/, Utility.ContactRoleIDType.Student.ToInt() /*6*/, ObjServices.Agent_Id.Value, ObjServices.UserID.Value);
                        }

                        if (ObjServices.StudentContactId > 0)
                        {
                            Entity.UnderWriting.Entities.Contact info =
                                ObjServices.oContactManager.GetContact(CorpId, ObjServices.StudentContactId.Value, languageId: ObjServices.Language.ToInt());


                            info.Dob = (Utility.IsDate(txtAge.Text) ? txtAge.Text.ParseFormat() : new Nullable<DateTime>());
                            info.FirstName = txtStudentName.Text;
                            ObjServices.oContactManager.UpdateContact(info);
                        }
                    }
                }
                else
                {
                    ObjServices.oPolicyManager.DeleteContactAndRole(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, ObjServices.StudentContactId.Value, 6, ObjServices.UserID.Value);
                    ObjServices.StudentContactId = 0;
                }
                #endregion

                #region RIDER SAVE

                IEnumerable<Entity.UnderWriting.Entities.Rider> riders = null;

                //Rider_Type_Id	Code_Name	Ryder_Type_Desc
                //0	Otros	Otros
                //1	ADB	Seguro Muerte Accidental
                //2	SEGTEMAD	Seguro Temporal Adicional
                //3	SPINS	Seguro Asegurado Adicional

                bool SaveADB = false;
                bool SaveSEGTEMAD = false;
                bool SaveSPINS = false;
                bool SaveSEGFAMAD = false;
                bool SaveLote = false;
                bool SaveRPT = false;
                bool SaveDE = false;
                bool SaveMANC = false;
                bool SaveOT = false;

                //Bmarroquin 25-01-2017 cambio como parte de la tropicalizacion en ESA
                bool SaveITP = false;
                bool SaveGF = false;


                if (hfSelectControls.Value == "VFunerarios"
                     || hfSelectControls.Value == "VCompassIndex"
                     || hfSelectControls.Value == "VLegacy"
                     || hfSelectControls.Value == "VSentinel"
                     || hfSelectControls.Value == "VLightHouse"
                     || hfSelectControls.Value == "VElite"
                     || hfSelectControls.Value == "VSelect"
                     || hfSelectControls.Value == "VFortis"
                     || hfSelectControls.Value == "VSerenity"
                     || hfSelectControls.Value == "VAsistencia90dias"
                     || hfSelectControls.Value == "VAsistencia30dias"
                     || hfSelectControls.Value == "VAsistencia60dias"
                    )
                    riders = ObjServices.oRider.GetAllRider(new Entity.UnderWriting.Entities.Policy.Parameter
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
                        LanguageId = ObjServices.Language.ToInt()
                    });

                #region Planes de vida y de salud
                if (
                        hfSelectControls.Value == "VCompassIndex"
                     || hfSelectControls.Value == "VLegacy"
                     || hfSelectControls.Value == "VSentinel"
                     || hfSelectControls.Value == "VLightHouse"
                     || hfSelectControls.Value == "VElite"
                     || hfSelectControls.Value == "VSelect"
                     || hfSelectControls.Value == "VFortis"
                     || hfSelectControls.Value == "VSerenity"
                     || hfSelectControls.Value == "VAsistencia90dias"
                     || hfSelectControls.Value == "VAsistencia30dias"
                     || hfSelectControls.Value == "VAsistencia60dias"
                   )
                {

                    if (riders != null) //pueden existir unos RIDERS y solo debo modificar
                    {
                        foreach (var item in riders)
                        {
                            if (item.RiderTypeId == Utility.RyderType.SeguroMuerteAccidental.ToInt() && ddlAccidentalDeathBenefits.SelectedValue == "1" /*YES*/)
                            {
                                SaveADB = true;
                                if (txtAccidentalDeathInsuredAmount != null)
                                    item.BeneficiaryAmount = Utility.IsDecimalReturnNull(txtAccidentalDeathInsuredAmount.Text.Replace(",", ""));

                                ObjServices.oRider.SetRider(item);

                            }
                            else
                                if (item.RiderTypeId == Utility.RyderType.SeguroMuerteAccidental.ToInt() && ddlAccidentalDeathBenefits.SelectedValue == "2" /*NO*/)
                                    ObjServices.oRider.DeleteRider(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, item.RiderTypeId, item.RiderId);

                            if (item.RiderTypeId == Utility.RyderType.SeguroAseguradoAdicional.ToInt() && ddlSpouseOtherInsured.SelectedValue == "1" /*YES*/)
                            {
                                SaveSEGTEMAD = true;
                                if (txtSpouseOtherInsured != null)
                                    item.BeneficiaryAmount = Utility.IsDecimalReturnNull(txtSpouseOtherInsured.Text.Replace(",", ""));

                                if (txtYearsSpouseOther != null)
                                    item.NumberOfYear = Utility.IsIntReturnNull(txtYearsSpouseOther.Text.Replace(",", ""));

                                ObjServices.oRider.SetRider(item);
                            }
                            else
                                if (item.RiderTypeId == Utility.RyderType.SeguroAseguradoAdicional.ToInt() && ddlSpouseOtherInsured.SelectedValue == "2" /*NO*/)
                                    ObjServices.oRider.DeleteRider(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, item.RiderTypeId, item.RiderId);

                            if (item.RiderTypeId == Utility.RyderType.SeguroTemporalAdicional.ToInt() && ddlAdditionalTermInsurance.SelectedValue == "1" /*YES*/)
                            {
                                SaveSPINS = true;
                                if (txtAdditionalTermInsuranceInsuredAmount != null)
                                    item.BeneficiaryAmount = Utility.IsDecimalReturnNull(txtAdditionalTermInsuranceInsuredAmount.Text.Replace(",", ""));

                                ObjServices.oRider.SetRider(item);
                            }
                            else if (item.RiderTypeId == Utility.RyderType.SeguroTemporalAdicional.ToInt() && ddlAdditionalTermInsurance.SelectedValue == "2" /*NO*/)
                                ObjServices.oRider.DeleteRider(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, item.RiderTypeId, item.RiderId);


                            /* Bmarroquin 25-01-2017  Lo comento xq me da un error de: Object reference not set to an instance of an object
                            if (item.RiderTypeId == Utility.RyderType.Dependent.ToInt() && ddlDependents.SelectedValue == "1")
                            {
                                SaveDE = true;
                                item.BeneficiaryAmount = 0;
                                ObjServices.oRider.SetRider(item);
                            }
                            else if (item.RiderTypeId == Utility.RyderType.Dependent.ToInt() && ddlDependents.SelectedValue == "2")
                                ObjServices.oRider.DeleteRider(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, item.RiderTypeId, item.RiderId);
                            */

                            if (item.RiderTypeId == Utility.RyderType.MaternityandNewbornComplication.ToInt() && ddlMaternityAndNewBornComplication.SelectedValue == "1" /*YES*/)
                            {
                                SaveMANC = true;
                                item.BeneficiaryAmount = 0;
                                ObjServices.oRider.SetRider(item);
                            }
                            else if (item.RiderTypeId == Utility.RyderType.MaternityandNewbornComplication.ToInt() && ddlMaternityAndNewBornComplication.SelectedValue == "2" /*NO*/)
                                ObjServices.oRider.DeleteRider(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, item.RiderTypeId, item.RiderId);


                            if (item.RiderTypeId == Utility.RyderType.OrganTransplant.ToInt() && ddlOrganTransplant.SelectedValue == "1" /*YES*/)
                            {
                                SaveOT = true;
                                item.BeneficiaryAmount = 0;
                                ObjServices.oRider.SetRider(item);
                            }
                            else if (item.RiderTypeId == Utility.RyderType.OrganTransplant.ToInt() && ddlOrganTransplant.SelectedValue == "2" /*NO*/)
                                ObjServices.oRider.DeleteRider(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, item.RiderTypeId, item.RiderId);


                            // *****   Bmarroquin 25-01-2017 cambio como parte de la tropicalizacion en ESA, por el tema de las coberturas de ESA

                            //Gastos Funerarios
                            if (item.RiderTypeId == Utility.RyderType.GastosFunerarios.ToInt() && ddlAdditionalTermInsurance.SelectedValue == "1" /*YES*/)
                            {
                                SaveGF = true;
                                if (txtAdditionalTermInsuranceInsuredAmount != null)
                                    item.BeneficiaryAmount = Utility.IsDecimalReturnNull(txtAdditionalTermInsuranceInsuredAmount.Text.Replace(",", ""));
                                ObjServices.oRider.SetRider(item);
                            }
                            else if (item.RiderTypeId == Utility.RyderType.GastosFunerarios.ToInt() && ddlAdditionalTermInsurance.SelectedValue == "2" /*NO*/)
                                ObjServices.oRider.DeleteRider(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, item.RiderTypeId, item.RiderId);

                            //Invalidez Total y Permanente
                            if (item.RiderTypeId == Utility.RyderType.InvalidesTotal.ToInt() && ddlCritialIllness.SelectedValue == "1" /*YES*/)
                            {
                                SaveITP = true;
                                if (txtCritialIllnessInsuredAmount != null)
                                    item.BeneficiaryAmount = Utility.IsDecimalReturnNull(txtCritialIllnessInsuredAmount.Text.Replace(",", ""));
                                ObjServices.oRider.SetRider(item);
                            }
                            else if (item.RiderTypeId == Utility.RyderType.InvalidesTotal.ToInt() && ddlCritialIllness.SelectedValue == "2" /*NO*/)
                                ObjServices.oRider.DeleteRider(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, item.RiderTypeId, item.RiderId);

                            //Fin Cambios Bmarroquin 25-01-2017
                        }
                    }

                    if (!SaveADB && ddlAccidentalDeathBenefits != null && ddlAccidentalDeathBenefits.SelectedValue == "1" /*YES*/) // esto quiere decir que es nuevo 
                    {
                        Entity.UnderWriting.Entities.Rider ADB = new Entity.UnderWriting.Entities.Rider();
                        ADB.CorpId = CorpId;
                        ADB.RegionId = RegionId;
                        ADB.CountryId = CountryId;
                        ADB.DomesticregId = DomesticregId;
                        ADB.StateProvId = StateProvId;
                        ADB.CityId = CityId;
                        ADB.OfficeId = OfficeId;
                        ADB.CaseSeqNo = CaseSeqNo;
                        ADB.HistSeqNo = HistSeqNo;

                        ADB.RiderStatusId = 1; //pendiente
                        ADB.UserId = ObjServices.UserID.Value;
                        ADB.RiderTypeId = Utility.RyderType.SeguroMuerteAccidental.ToInt();/*	ADB	Seguro Muerte Accidental */

                        if (txtAccidentalDeathInsuredAmount != null) ADB.BeneficiaryAmount = Utility.IsDecimalReturnNull(txtAccidentalDeathInsuredAmount.Text.Replace(",", ""));

                        ObjServices.oRider.SetRider(ADB);
                    }


                    if (!SaveDE && ddlDependents != null && ddlDependents.SelectedValue == "1" /*YES*/) // esto quiere decir que es nuevo 
                    {
                        Entity.UnderWriting.Entities.Rider DE = new Entity.UnderWriting.Entities.Rider()
                        {
                            CorpId = CorpId,
                            RegionId = RegionId,
                            CountryId = CountryId,
                            DomesticregId = DomesticregId,
                            StateProvId = StateProvId,
                            CityId = CityId,
                            OfficeId = OfficeId,
                            CaseSeqNo = CaseSeqNo,
                            HistSeqNo = HistSeqNo,
                            RiderStatusId = 1, //pendiente
                            UserId = ObjServices.UserID.Value,
                            RiderTypeId = Utility.RyderType.Dependent.ToInt(),
                            BeneficiaryAmount = 0
                        };

                        ObjServices.oRider.SetRider(DE);
                    }

                    if (!SaveMANC && ddlMaternityAndNewBornComplication != null && ddlMaternityAndNewBornComplication.SelectedValue == "1" /*YES*/) // esto quiere decir que es nuevo 
                    {
                        Entity.UnderWriting.Entities.Rider MANC = new Entity.UnderWriting.Entities.Rider()
                        {
                            CorpId = CorpId,
                            RegionId = RegionId,
                            CountryId = CountryId,
                            DomesticregId = DomesticregId,
                            StateProvId = StateProvId,
                            CityId = CityId,
                            OfficeId = OfficeId,
                            CaseSeqNo = CaseSeqNo,
                            HistSeqNo = HistSeqNo,
                            RiderStatusId = 1, //pendiente
                            UserId = ObjServices.UserID.Value,
                            RiderTypeId = Utility.RyderType.MaternityandNewbornComplication.ToInt(),
                            BeneficiaryAmount = 0
                        };

                        ObjServices.oRider.SetRider(MANC);
                    }

                    if (!SaveOT && ddlOrganTransplant != null && ddlOrganTransplant.SelectedValue == "1" /*YES*/) // esto quiere decir que es nuevo 
                    {
                        Entity.UnderWriting.Entities.Rider OT = new Entity.UnderWriting.Entities.Rider()
                        {
                            CorpId = CorpId,
                            RegionId = RegionId,
                            CountryId = CountryId,
                            DomesticregId = DomesticregId,
                            StateProvId = StateProvId,
                            CityId = CityId,
                            OfficeId = OfficeId,
                            CaseSeqNo = CaseSeqNo,
                            HistSeqNo = HistSeqNo,
                            RiderStatusId = 1, //pendiente
                            UserId = ObjServices.UserID.Value,
                            RiderTypeId = Utility.RyderType.OrganTransplant.ToInt(),
                            BeneficiaryAmount = 0
                        };

                        ObjServices.oRider.SetRider(OT);
                    }


                    if (!SaveSEGTEMAD && ddlSpouseOtherInsured != null && ddlSpouseOtherInsured.SelectedValue == "1" /*YES*/) // esto quiere decir que es nuevo 
                    {
                        Entity.UnderWriting.Entities.Rider SEGTEMAD = new Entity.UnderWriting.Entities.Rider();
                        SEGTEMAD.CorpId = CorpId;
                        SEGTEMAD.RegionId = RegionId;
                        SEGTEMAD.CountryId = CountryId;
                        SEGTEMAD.DomesticregId = DomesticregId;
                        SEGTEMAD.StateProvId = StateProvId;
                        SEGTEMAD.CityId = CityId;
                        SEGTEMAD.OfficeId = OfficeId;
                        SEGTEMAD.CaseSeqNo = CaseSeqNo;
                        SEGTEMAD.HistSeqNo = HistSeqNo;

                        SEGTEMAD.RiderStatusId = 1; //pendiente
                        SEGTEMAD.UserId = ObjServices.UserID.Value;
                        SEGTEMAD.RiderTypeId = Utility.RyderType.SeguroAseguradoAdicional.ToInt();/*	SEGTEMAD	Seguro Temporal Adicional*/

                        if (txtSpouseOtherInsured != null)
                            SEGTEMAD.BeneficiaryAmount = Utility.IsDecimalReturnNull(txtSpouseOtherInsured.Text.Replace(",", ""));

                        if (txtYearsSpouseOther != null)
                            SEGTEMAD.NumberOfYear = Utility.IsIntReturnNull(txtYearsSpouseOther.Text.Replace(",", ""));

                        ObjServices.oRider.SetRider(SEGTEMAD);
                    }

                    if (!SaveSPINS && ddlAdditionalTermInsurance != null && ddlAdditionalTermInsurance.SelectedValue == "1" /*YES*/) // esto quiere decir que es nuevo 
                    {
                        Entity.UnderWriting.Entities.Rider SPINS = new Entity.UnderWriting.Entities.Rider();
                        SPINS.CorpId = CorpId;
                        SPINS.RegionId = RegionId;
                        SPINS.CountryId = CountryId;
                        SPINS.DomesticregId = DomesticregId;
                        SPINS.StateProvId = StateProvId;
                        SPINS.CityId = CityId;
                        SPINS.OfficeId = OfficeId;
                        SPINS.CaseSeqNo = CaseSeqNo;
                        SPINS.HistSeqNo = HistSeqNo;

                        SPINS.RiderStatusId = 1; //pendiente
                        SPINS.UserId = ObjServices.UserID.Value;
                        SPINS.RiderTypeId = Utility.RyderType.SeguroTemporalAdicional.ToInt();

                        if (txtAdditionalTermInsuranceInsuredAmount != null)
                            SPINS.BeneficiaryAmount = Utility.IsDecimalReturnNull(txtAdditionalTermInsuranceInsuredAmount.Text.Replace(",", ""));

                        ObjServices.oRider.SetRider(SPINS);
                    }
                    

                    // *****   Bmarroquin 25-01-2017 cambio como parte de la tropicalizacion en ESA, por el tema de las coberturas de ESA

                    //Invalidez Total 
                    if (!SaveITP && ddlCritialIllness != null && ddlCritialIllness.SelectedValue == "1" /*YES*/) // esto quiere decir que es nuevo 
                    {
                        Entity.UnderWriting.Entities.Rider ITP = new Entity.UnderWriting.Entities.Rider();
                        ITP.CorpId = CorpId;
                        ITP.RegionId = RegionId;
                        ITP.CountryId = CountryId;
                        ITP.DomesticregId = DomesticregId;
                        ITP.StateProvId = StateProvId;
                        ITP.CityId = CityId;
                        ITP.OfficeId = OfficeId;
                        ITP.CaseSeqNo = CaseSeqNo;
                        ITP.HistSeqNo = HistSeqNo;

                        ITP.RiderStatusId = 1; //pendiente
                        ITP.UserId = ObjServices.UserID.Value;
                        ITP.RiderTypeId = Utility.RyderType.InvalidesTotal.ToInt();

                        if (txtCritialIllnessInsuredAmount != null)
                            ITP.BeneficiaryAmount = Utility.IsDecimalReturnNull(txtCritialIllnessInsuredAmount.Text.Replace(",", ""));

                        ObjServices.oRider.SetRider(ITP);
                    }

                    //Gastos Funerarios 
                    //Nota: se ocupa ddlAdditionalTermInsurance como el control de Gastos Funerarios
                    if (!SaveGF && ddlAdditionalTermInsurance != null && ddlAdditionalTermInsurance.SelectedValue == "1" /*YES*/) // esto quiere decir que es nuevo 
                    {
                        Entity.UnderWriting.Entities.Rider GF = new Entity.UnderWriting.Entities.Rider();
                        GF.CorpId = CorpId;
                        GF.RegionId = RegionId;
                        GF.CountryId = CountryId;
                        GF.DomesticregId = DomesticregId;
                        GF.StateProvId = StateProvId;
                        GF.CityId = CityId;
                        GF.OfficeId = OfficeId;
                        GF.CaseSeqNo = CaseSeqNo;
                        GF.HistSeqNo = HistSeqNo;

                        GF.RiderStatusId = 1; //pendiente
                        GF.UserId = ObjServices.UserID.Value;
                        GF.RiderTypeId = Utility.RyderType.GastosFunerarios.ToInt();

                        if (txtAdditionalTermInsuranceInsuredAmount != null)
                            GF.BeneficiaryAmount = Utility.IsDecimalReturnNull(txtAdditionalTermInsuranceInsuredAmount.Text.Replace(",", ""));

                        ObjServices.oRider.SetRider(GF);
                    }

                    // *****   Fin Cambios 25-01-2017

                }
                #endregion
                #region Planes funerarios
                else if (hfSelectControls.Value == "VFunerarios")
                {
                    if (!riders.isNullReferenceObject())
                    {
                        foreach (var item in riders)
                        {
                            if (item.RiderTypeId == Utility.RyderType.SeguroFamiliarAdicional.ToInt() && ddlOtherInsured.SelectedValue == "1" /*YES*/)
                                SaveSEGFAMAD = true;
                            else
                                if (item.RiderTypeId == Utility.RyderType.SeguroFamiliarAdicional.ToInt() && ddlOtherInsured.SelectedValue == "2" /*NO*/)
                                    ObjServices.oRider.DeleteRider(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, item.RiderTypeId, item.RiderId);

                            if (item.RiderTypeId == Utility.RyderType.Repatriation.ToInt() && ddlRepatriation.SelectedValue == "1" /*YES*/)
                                SaveRPT = true;
                            else
                                if (item.RiderTypeId == Utility.RyderType.Repatriation.ToInt() && ddlRepatriation.SelectedValue == "2" /*NO*/)
                                    ObjServices.oRider.DeleteRider(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, item.RiderTypeId, item.RiderId);


                            if (item.RiderTypeId == Utility.RyderType.Lote.ToInt() && ddlLote.SelectedValue == "1" /*YES*/)
                            {
                                SaveLote = true;

                                if (ddlLoteType != null)
                                    item.RiderInfo = ddlLoteType.SelectedValue;

                                ObjServices.oRider.SetRider(item);
                            }
                            else
                                if (item.RiderTypeId == Utility.RyderType.Lote.ToInt() && ddlLote.SelectedValue == "2" /*NO*/)
                                    ObjServices.oRider.DeleteRider(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, item.RiderTypeId, item.RiderId);

                        }

                        if (!SaveSEGFAMAD && ddlOtherInsured.SelectedValue == "1" /*YES*/) // esto quiere decir que es nuevo 
                        {
                            oWUCFieldFooter.setControls();
                            var SEGFAMAD = new Entity.UnderWriting.Entities.Rider()
                            {
                                CorpId = CorpId,
                                RegionId = RegionId,
                                CountryId = CountryId,
                                DomesticregId = DomesticregId,
                                StateProvId = StateProvId,
                                CityId = CityId,
                                OfficeId = OfficeId,
                                CaseSeqNo = CaseSeqNo,
                                HistSeqNo = HistSeqNo,
                                BeneficiaryAmount = oWUCFieldFooter.txtInsuredAmount.ToDecimal(),
                                RiderStatusId = 1, //pendiente
                                UserId = ObjServices.UserID.Value,
                                RiderTypeId = Utility.RyderType.SeguroFamiliarAdicional.ToInt()
                            };

                            ObjServices.oRider.SetRider(SEGFAMAD);
                        }


                        if (!SaveRPT && ddlRepatriation.SelectedValue == "1" /*YES*/) // esto quiere decir que es nuevo 
                        {
                            var RPT = new Entity.UnderWriting.Entities.Rider()
                            {
                                CorpId = CorpId,
                                RegionId = RegionId,
                                CountryId = CountryId,
                                DomesticregId = DomesticregId,
                                StateProvId = StateProvId,
                                CityId = CityId,
                                OfficeId = OfficeId,
                                CaseSeqNo = CaseSeqNo,
                                HistSeqNo = HistSeqNo,
                                RiderStatusId = 1, //pendiente
                                UserId = ObjServices.UserID.Value,
                                RiderTypeId = Utility.RyderType.Repatriation.ToInt()
                            };

                            ObjServices.oRider.SetRider(RPT);
                        }

                        if (!SaveLote && ddlLote.SelectedValue == "1" /*YES*/) // esto quiere decir que es nuevo                         
                        {
                            var LOTE = new Entity.UnderWriting.Entities.Rider()
                            {
                                CorpId = CorpId,
                                RegionId = RegionId,
                                CountryId = CountryId,
                                DomesticregId = DomesticregId,
                                StateProvId = StateProvId,
                                CityId = CityId,
                                OfficeId = OfficeId,
                                CaseSeqNo = CaseSeqNo,
                                HistSeqNo = HistSeqNo,
                                RiderStatusId = 1, //pendiente
                                UserId = ObjServices.UserID.Value,
                                RiderInfo = ddlLoteType.SelectedValue,
                                RiderTypeId = Utility.RyderType.Lote.ToInt()
                            };

                            ObjServices.oRider.SetRider(LOTE);
                        }
                    }
                }
                #endregion
                else /*si el tipo de plan no lleva rider lo borro todos*/
                    ObjServices.oRider.DeleteRider(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, new Nullable<int>(), new Nullable<int>());

                #endregion

                #region ADDITIONAL INSURED INFORMATION or DESIGNATED PENSIONER INFORMATION
                var hdnValidateFormDesignatedPensionerOrAddicionalInsured = (oWUCDesignatedPensionerInformation.FindControl("hdnValidateFormDesignatedPensionerOrAddicionalInsured") as HiddenField);
                if (
                  hfSelectControls.Value == "VCompassIndex"
               || hfSelectControls.Value == "VLegacy"
               || hfSelectControls.Value == "VSentinel"
               || hfSelectControls.Value == "VLightHouse"
               || hfSelectControls.Value == "VElite"
               || hfSelectControls.Value == "VSelect"
               || hfSelectControls.Value == "VFortis"
               || hfSelectControls.Value == "VSerenity"
               || hfSelectControls.Value == "VAsistencia90dias"
               || hfSelectControls.Value == "VAsistencia30dias"
               || hfSelectControls.Value == "VAsistencia60dias"
                    /*la poliza puede tener un  ADDITIONAL INSURED INFORMATION*/
               )
                {
                    if (ddlSpouseOtherInsured != null && ddlSpouseOtherInsured.SelectedValue == "2" /*NO*/)
                    {
                        if (ObjServices.InsuredAddContactId.HasValue)
                        {
                            if (ObjServices.InsuredAddContactId.Value > 0)
                            {
                                //Eliminar los cuestionarios
                                ObjServices.oHealthDeclarationManager.DeleteAllQuestionnaire(new Entity.UnderWriting.Entities.Questionnaire()
                                {
                                    CorpId = CorpId,
                                    RegionId = RegionId,
                                    CountryId = CountryId,
                                    DomesticregId = DomesticregId,
                                    StateProvId = StateProvId,
                                    CityId = CityId,
                                    OfficeId = OfficeId,
                                    CaseSeqNo = CaseSeqNo,
                                    HistSeqNo = HistSeqNo,
                                    ContactId = ObjServices.InsuredAddContactId.Value,
                                    ContactRoleTypeId = Utility.ContactRoleIDType.AddicionalInsured.ToInt(),
                                    UserId = ObjServices.UserID.Value
                                });

                                //Despegar el additional insured
                                ObjServices.oPolicyManager.DeleteContactRole(CorpId,
                                                                                RegionId,
                                                                                CountryId,
                                                                                DomesticregId,
                                                                                StateProvId,
                                                                                CityId,
                                                                                OfficeId,
                                                                                CaseSeqNo,
                                                                                HistSeqNo,
                                                                                ObjServices.InsuredAddContactId.Value,
                                                                                Utility.ContactRoleIDType.AddicionalInsured.ToInt(),
                                                                                ObjServices.UserID.Value
                                                                                );
                                ObjServices.InsuredAddContactId = -1;
                                ObjServices.ContactEntityID = -1;

                                oWUCDesignatedPensionerInformation.FillData();
                            }
                        }

                        hdnValidateFormDesignatedPensionerOrAddicionalInsured.Value = "false";

                    }
                    else
                        if (ddlSpouseOtherInsured != null && ddlSpouseOtherInsured.SelectedValue == "1" /*SI*/) /*QUIERE DECIR QUE DEBE VALIDAR LOS DATOS*/
                            hdnValidateFormDesignatedPensionerOrAddicionalInsured.Value = "true";

                    //si existe el Desidnated Pensioner lo borrro
                    if (ObjServices.DesignatedPensionerContactId.HasValue)
                    {
                        if (ObjServices.DesignatedPensionerContactId.Value > 0)
                        {
                            ObjServices.oPolicyManager.DeleteContactRole(CorpId,
                                                                         RegionId,
                                                                         CountryId,
                                                                         DomesticregId,
                                                                         StateProvId,
                                                                         CityId,
                                                                         OfficeId,
                                                                         CaseSeqNo,
                                                                         HistSeqNo,
                                                                         ObjServices.DesignatedPensionerContactId.Value,
                                                                         Utility.ContactRoleIDType.DesignatedPensioner.ToInt(),
                                                                         ObjServices.UserID.Value
                                                                        );

                            ObjServices.DesignatedPensionerContactId = -1;
                            ObjServices.ContactEntityID = -1;
                            hdnValidateFormDesignatedPensionerOrAddicionalInsured.Value = "false";
                        }
                    }
                }
                /*la poliza puede tener un  DESIGNATED PENSIONER INFORMATION*/
                else
                {
                    var View = mvSelectControl.GetActiveView();

                    if (View != VFunerarios)
                    {
                        #region Planes de Vida
                        if (!HaveDesignatedPensioner())
                        {
                            if (ObjServices.DesignatedPensionerContactId.HasValue)
                            {
                                if (ObjServices.DesignatedPensionerContactId.Value > 0)
                                {
                                    ObjServices.oPolicyManager.DeleteContactRole(CorpId,
                                                                                RegionId,
                                                                                CountryId,
                                                                                DomesticregId,
                                                                                StateProvId,
                                                                                CityId,
                                                                                OfficeId,
                                                                                CaseSeqNo,
                                                                                HistSeqNo,
                                                                                ObjServices.DesignatedPensionerContactId.Value,
                                                                                Utility.ContactRoleIDType.DesignatedPensioner.ToInt(),
                                                                                ObjServices.UserID.Value
                                                                                );

                                    ObjServices.DesignatedPensionerContactId = -1;
                                    ObjServices.ContactEntityID = -1;
                                }
                            }

                            hdnValidateFormDesignatedPensionerOrAddicionalInsured.Value = "false";

                            //si existe lo borrro
                            if (ObjServices.InsuredAddContactId.HasValue)
                            {
                                if (ObjServices.InsuredAddContactId.Value > 0)
                                {
                                    ObjServices.saveSetValidTab(Utility.Tab.HealthDeclaration, false);

                                    ObjServices.oHealthDeclarationManager.DeleteAllQuestionnaire(new Entity.UnderWriting.Entities.Questionnaire
                                    {
                                        CorpId = CorpId,
                                        RegionId = RegionId,
                                        CountryId = CountryId,
                                        DomesticregId = DomesticregId,
                                        StateProvId = StateProvId,
                                        CityId = CityId,
                                        OfficeId = OfficeId,
                                        CaseSeqNo = CaseSeqNo,
                                        HistSeqNo = HistSeqNo,
                                        ContactId = ObjServices.InsuredAddContactId.Value,
                                        ContactRoleTypeId = Utility.ContactRoleIDType.AddicionalInsured.ToInt(),
                                        UserId = ObjServices.UserID.Value
                                    });

                                    ObjServices.oPolicyManager.DeleteContactRole(CorpId,
                                                                                    RegionId,
                                                                                    CountryId,
                                                                                    DomesticregId,
                                                                                    StateProvId,
                                                                                    CityId,
                                                                                    OfficeId,
                                                                                    CaseSeqNo,
                                                                                    HistSeqNo,
                                                                                    ObjServices.InsuredAddContactId.Value,
                                                                                    Utility.ContactRoleIDType.AddicionalInsured.ToInt(),
                                                                                    ObjServices.UserID.Value);
                                    ObjServices.InsuredAddContactId = -1;
                                    ObjServices.ContactEntityID = -1;
                                }
                            }


                        }
                        //else /*QUIERE DECIR QUE DEBE VALIDAR LOS DATOS*/
                        //  hdnValidateFormDesignatedPensionerOrAddicionalInsured.Value = "true";
                        #endregion
                    }
                    else
                    {
                        #region Planes Funerarios
                        //Eliminar los asegurados adicionales
                        if (ddlOtherInsured != null && ddlOtherInsured.SelectedValue == "2")
                            DeleteIncludeFamiliarOrDependents(Utility.ContactRoleIDType.IncludedFamiliar);
                        #endregion
                    }
                }
                #endregion

                switch (ProductBehavior)
                {
                    case Utility.ProductBehavior.VIDACRED:

                        #region Add info to Contact Policy
                        var SpecialPaymentConfirmation = !this.ddlSpecialPayment.isNullReferenceControl() && !this.ddlSpecialPayment.SelectedItem.isNullReferenceObject() && !string.IsNullOrEmpty(this.ddlSpecialPayment.SelectedItem.Text) ? this.ddlSpecialPayment.SelectedItem.Text : string.Empty;
                        var SpecialPaymentAmount = !this.txtSpecialPayment.isNullReferenceControl() && Utility.IsDecimalReturnNull(this.txtSpecialPayment.Text) != new Nullable<decimal>() ? Utility.IsDecimalReturnNull(this.txtSpecialPayment.Text) : 0;
                        var FinancingRate = !this.txtFinancingRate.isNullReferenceControl() && Utility.IsDecimalReturnNull(this.txtFinancingRate.Text) != new Nullable<decimal>() ? Utility.IsDecimalReturnNull(this.txtFinancingRate.Text) : 0;
                        var DestinyFund = !this.ddlDestinyFund.isNullReferenceControl() && !string.IsNullOrEmpty(this.ddlDestinyFund.SelectedValue) ? this.ddlDestinyFund.SelectedValue : string.Empty;

                        var policyContacts = ObjServices.oPolicyManager.GetContactPolicy(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId, CaseSeqNo, HistSeqNo, null, null);
                        foreach (var policyContact in policyContacts)
                        {
                            //asignar los valores que requieren.
                            policyContact.InterestRate = FinancingRate.HasValue ? FinancingRate.Value : 0;
                            if (SpecialPaymentConfirmation == Resources.YesLabel.ToUpper().ToString())
                            {
                                policyContact.SpecialPayment = SpecialPaymentAmount.HasValue ? SpecialPaymentAmount.Value : 0;
                            }
                            policyContact.DestinationFund = DestinyFund;
                            ObjServices.oPolicyManager.SetContactPolicyInfo(policyContact);
                        }

                        #endregion
                        break;
                }
            }
            //Mandamos a setear la variable nuevamente, porque como no entro al if se pierde
            else
            {
                if (ObjServices.KeyNameProduct.Equals("GuardianPlus"))//Esta funcionalidad solo aplica para guardian
                {
                    var datos = ObjServices.oPolicyManager.GetPlanData(
                        CorpId, 
                        RegionId, 
                        CountryId, 
                        DomesticregId, 
                        StateProvId, 
                        CityId, 
                        OfficeId, 
                        CaseSeqNo, 
                        HistSeqNo);

                    //oWUCFieldFooter.setRurnPremium(datos.ReturnAmount);
                    if (datos.ReturnAmount == null)
                    {
                        oWUCFieldFooter.setRurnPremium(ReturnOfPremiumBk);
                    }
                    else
                    {
                        oWUCFieldFooter.setRurnPremium(datos.ReturnAmount);
                    }
                }

                //Bmarroquin 29-04-2017 setear el Recargo x Fraccionamiento, se pierde cuando se dispara una validacion...
                var datos2 = ObjServices.oPolicyManager.GetPlanData(CorpId, RegionId, CountryId, DomesticregId, StateProvId
                        , CityId, OfficeId, CaseSeqNo, HistSeqNo);

                if (datos2.Fraction_Surcharge != null)
                {
                    if (datos2.Fraction_Surcharge > 0)
                    {
                        var footer = ((WUCFieldFooter)this.Parent.FindControl("WUCFieldFooter"));
                        footer.setControls();
                        footer.txtFraccionamiento.Text = datos2.Fraction_Surcharge.Value.ToString("#0.00", NumberFormatInfo.InvariantInfo);
                    }
                }

                //Bmarroquin 05-05-2017 setear la Prima Comercial, se pierde cuando se dispara una validacion...
                if (datos2.NetAnnualPremium != null)
                {
                    if (datos2.NetAnnualPremium > 0)
                    {
                        var footer = ((WUCFieldFooter)this.Parent.FindControl("WUCFieldFooter"));
                        footer.setControls();
                        footer.txtPrimaAnualNeta.Text = datos2.NetAnnualPremium.Value.ToString("#0.00", NumberFormatInfo.InvariantInfo);
                    }
                }
            }

            if (ddlProductName.Enabled)
            {

                ObjServices.oPolicyManager.GenerateTempPolicyNo(new Entity.UnderWriting.Entities.Policy.Parameter
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
                    UnderwriterId = ObjServices.Agent_LoginId
                });

                //Refrescar el search
                var bodyContent = this.Page.Master.FindControl("bodyContent");
                var WUCSearch = bodyContent.FindControl("WUCSearch");
                if (!WUCSearch.isNullReferenceControl())
                    ((WEB.NewBusiness.NewBusiness.UserControls.PlanPolicy.WUCSearch)WUCSearch).FillData();
            }

            ObjServices.isSavePlan = respuesta;
            udpPlanInformation.Update();
            return respuesta; 
        }

        private void DeleteIncludeFamiliarOrDependents(Utility.ContactRoleIDType Role)
        {
            var dataAdditionalInsured = ObjServices.oPolicyManager.GetPolicyAddInsured(new Entity.UnderWriting.Entities.Policy.Parameter
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
            }).ToList();

            foreach (var item in dataAdditionalInsured)
            {
                //Borrar los cuestionarios
                ObjServices.oHealthDeclarationManager.DeleteAllQuestionnaire(new Entity.UnderWriting.Entities.Questionnaire
                {
                    CorpId = CorpId,
                    RegionId = RegionId,
                    CountryId = CountryId,
                    DomesticregId = DomesticregId,
                    StateProvId = StateProvId,
                    CityId = CityId,
                    OfficeId = OfficeId,
                    CaseSeqNo = CaseSeqNo,
                    HistSeqNo = HistSeqNo,
                    ContactId = item.ContactId,
                    ContactRoleTypeId = Role.ToInt(),
                    UserId = ObjServices.UserID.Value
                });

                //Despegar el contacto de la poliza
                ObjServices.oPolicyManager.DeleteContactRole(CorpId,
                                                             RegionId,
                                                             CountryId,
                                                             DomesticregId,
                                                             StateProvId,
                                                             CityId,
                                                             OfficeId,
                                                             CaseSeqNo,
                                                             HistSeqNo,
                                                             item.ContactId,
                                                             Role.ToInt(),
                                                             ObjServices.UserID.Value
                                                            );
            }

            oWUCDesignatedPensionerInformation.FillData();
        }

        private void DeleteAll()
        {
            ObjServices.saveSetValidTab(Utility.Tab.PlanPolicy, false);

            setVariables();

            var view = mvSelectControl.GetActiveView();
            if (view != VFunerarios)
            {
                #region Planes de vida
                /*la poliza puede tener un  ADDITIONAL INSURED INFORMATION*/
                if (ObjServices.InsuredAddContactId.HasValue)
                {
                    if (ObjServices.InsuredAddContactId.Value > 0)
                    {
                        ObjServices.oPolicyManager.DeleteContactAndRole(CorpId,
                                                                        RegionId,
                                                                        CountryId,
                                                                        DomesticregId,
                                                                        StateProvId,
                                                                        CityId,
                                                                        OfficeId,
                                                                        CaseSeqNo,
                                                                        HistSeqNo,
                                                                        ObjServices.InsuredAddContactId.Value,
                                                                        3,
                                                                        ObjServices.UserID.Value
                                                                        );
                        ObjServices.InsuredAddContactId = -1;
                    }
                }

                /*la poliza puede tener un  Designate Pensioner INFORMATION*/
                if (ObjServices.DesignatedPensionerContactId.HasValue)
                {
                    if (ObjServices.DesignatedPensionerContactId.Value > 0)
                    {
                        ObjServices.oPolicyManager.DeleteContactAndRole(CorpId,
                                                                        RegionId,
                                                                        CountryId,
                                                                        DomesticregId,
                                                                        StateProvId,
                                                                        CityId,
                                                                        OfficeId,
                                                                        CaseSeqNo,
                                                                        HistSeqNo,
                                                                        ObjServices.DesignatedPensionerContactId.Value,
                                                                        5,
                                                                        ObjServices.UserID.Value
                                                                        );
                        ObjServices.DesignatedPensionerContactId = -1;
                    }
                }

                //la poliza puede tener rider 
                ObjServices.oRider.DeleteRider(CorpId,
                                               RegionId,
                                               CountryId,
                                               DomesticregId,
                                               StateProvId,
                                               CityId,
                                               OfficeId,
                                               CaseSeqNo,
                                               HistSeqNo,
                                               new Nullable<int>(),
                                               new Nullable<int>()
                                               );

                ObjServices.oPolicyManager.DeleteContactAndRole(CorpId,
                                                                RegionId,
                                                                CountryId,
                                                                DomesticregId,
                                                                StateProvId,
                                                                CityId,
                                                                OfficeId,
                                                                CaseSeqNo,
                                                                HistSeqNo,
                                                                ObjServices.StudentContactId.Value,
                                                                6,
                                                                ObjServices.UserID.Value
                                                                );


                if (!ddlInvestmentProfile.isNullReferenceControl())
                {
                    //borrar perfil de inversiones 
                    var invs = new Entity.UnderWriting.Entities.Policy.InvestProfile();

                    invs.CorpId = CorpId;
                    invs.RegionId = RegionId;
                    invs.CountryId = CountryId;
                    invs.DomesticregId = DomesticregId;
                    invs.StateProvId = StateProvId;
                    invs.CityId = CityId;
                    invs.OfficeId = OfficeId;
                    invs.CaseSeqNo = CaseSeqNo;
                    invs.HistSeqNo = HistSeqNo;
                    invs.ProfileTypeId = ddlInvestmentProfile.ToInt();
                    invs.UserId = ObjServices.UserID.Value;
                    ObjServices.oPolicyManager.DeleteInvestmentProfile(invs);
                }

                var freg = new Entity.UnderWriting.Entities.Policy.PaymentFrequency();
                freg.CorpId = CorpId;
                freg.RegionId = RegionId;
                freg.CountryId = CountryId;
                freg.DomesticregId = DomesticregId;
                freg.StateProvId = StateProvId;
                freg.CityId = CityId;
                freg.OfficeId = OfficeId;
                freg.CaseSeqNo = CaseSeqNo;
                freg.HistSeqNo = HistSeqNo;
                freg.UserId = ObjServices.UserID.Value;
                ObjServices.oPolicyManager.DeletePaymentFrequency(freg);
                #endregion
            }
            else
            {
                #region Planes Funerarios

                var dataAdditionalInsured = ObjServices.oPolicyManager.GetPolicyAddInsured(new Entity.UnderWriting.Entities.Policy.Parameter()
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
                }).ToList();

                foreach (var item in dataAdditionalInsured)
                {
                    ObjServices.oPolicyManager.DeleteContactAndRole(CorpId,
                                                                   RegionId,
                                                                   CountryId,
                                                                   DomesticregId,
                                                                   StateProvId,
                                                                   CityId,
                                                                   OfficeId,
                                                                   CaseSeqNo,
                                                                   HistSeqNo,
                                                                   item.ContactId,
                                                                   Utility.ContactRoleIDType.IncludedFamiliar.ToInt(),
                                                                   ObjServices.UserID.Value
                                                                   );
                }


                #endregion
            }

            //reset los datos de la poliza a como se creo 
            var datos = ObjServices.oPolicyManager.GetPlanData(CorpId, RegionId, CountryId, DomesticregId, StateProvId
               , CityId, OfficeId, CaseSeqNo, HistSeqNo);

            if (datos != null)
            {
                var pData = new Entity.UnderWriting.Entities.Policy();
                /*KEY*/
                pData.CorpId = datos.CorpId;
                pData.RegionId = datos.RegionId;
                pData.CountryId = datos.CountryId;
                pData.DomesticregId = datos.DomesticregId;
                pData.StateProvId = datos.StateProvId;
                pData.CityId = datos.CityId;
                pData.OfficeId = datos.OfficeId;
                pData.CaseSeqNo = datos.CaseSeqNo;
                pData.HistSeqNo = datos.HistSeqNo;

                /*Campos obligatorios para crear producto*/
                pData.PolicyNo = datos.PolicyNo;
                //pData.PolicySerieId = datos.PolicySerieId;
                pData.PolicyStatusId = datos.PolicyStatusId;
                pData.Priority = true;
                pData.UserId = ObjServices.UserID.Value;


                ObjServices.oPolicyManager.UpdatePolicy(pData);
            }
        }

        #region EVENT OTHER CONTROL

        protected void ddlDeducibleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            setControls();
            var dropDeducibleType = ((DropDownList)sender).SelectedValue;

            if (dropDeducibleType != "-1")
            {
                var data = ObjServices.GettingDropData(
                                            Utility.DropDownType.DeductibleCategory,
                                            NameKey: ObjServices.KeyNameProduct,
                                            DeductibleTypeId: dropDeducibleType.ToInt()
                                           ).Select(x => new
                                           {
                                               DeductibleCategoryValue = x.DeductibleCategoryValue,
                                               Value = "{\"DeductibleTypeId\":" + x.DeductibleTypeId.Value.ToString() + ",\"DeductibleCategoryId\":" + x.DeductibleCategoryId.Value.ToString() + "}"
                                           });


                ddlDeducible.DataSource = data;
                ddlDeducible.DataTextField = "DeductibleCategoryValue";
                ddlDeducible.DataValueField = "Value";
                ddlDeducible.DataBind();
                ddlDeducible.Items.Insert(0, new ListItem { Value = "-1", Text = "----" });

                //Formatear valores del dropdown
                for (int i = 1; i < ddlDeducible.Items.Count; i++)
                    ddlDeducible.Items[i].Text
                        = double.Parse(ddlDeducible.Items[i].Text).ToFormatNumeric();

                ddlDeducible.Enabled = (ddlDeducible.Items.Count > 0);
            }
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            setControls();

            if (ddlInvestmentProfile != null)
            {
                int? currencyId = Utility.IsIntReturnNull(((DropDownList)sender).SelectedValue);

                ObjServices.GettingAllDropsJSON(
                      ref ddlInvestmentProfile, Utility.DropDownType.ProfileType_NewBusiness
                    , "ProfileTypeDesc"
                    , corpId: ObjServices.Corp_Id
                    , regionId: ObjServices.Region_Id
                    , countryId: ObjServices.Country_Id
                    , domesticregId: ObjServices.Domesticreg_Id
                    , stateProvId: ObjServices.State_Prov_Id
                    , cityId: ObjServices.City_Id, officeId: ObjServices.Office_Id
                    , caseSeqNo: ObjServices.Case_Seq_No
                    , histSeqNo: ObjServices.Hist_Seq_No
                    , currencyId: currencyId
                    );
            }

        }

        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ManageHaveRider(object sender, EventArgs e)
        {
            var drop = (sender as DropDownList);

            if (drop == null) return;

            if (oWUCDesignatedPensionerInformation != null && !ObjServices.IsReadOnly)
            {
                oWUCDesignatedPensionerInformation.EnableControls((drop.SelectedValue == "1"));
                (oWUCDesignatedPensionerInformation.FindControl("hdnValidateFormDesignatedPensionerOrAddicionalInsured")
                    as HiddenField).Value = drop.SelectedValue == "1" ? "true" : "false";
            }

            #region Delete Contact Role Designated Pensioner and Additional Insured
            if (ObjServices.InsuredAddContactId.HasValue &&
                ObjServices.InsuredAddContactId > 0)
            {
                if (!HaveAdditionalInsured())
                    this.MessageBox(Resources.AdditionalInsuredOrDesigantedPensionervalidation, Title: ObjServices.Language == Utility.Language.en ? "Warning" : "Advertencia");
            }
            else
                if (oWUCDesignatedPensionerInformation != null)
                    oWUCDesignatedPensionerInformation.ClearData();

            #endregion
            if (oudpDesignatedPensioner != null)
                oudpDesignatedPensioner.Update();
        }

        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ManageHaveDesigantedPensionerQuestion(object sender, EventArgs e)
        {
            setVariables();

            if (oWUCDesignatedPensionerInformation != null && !ObjServices.IsReadOnly)
            {
                oWUCDesignatedPensionerInformation.EnableControls(((sender as RadioButton).ID == "si1" && (sender as RadioButton).Checked));

                (oWUCDesignatedPensionerInformation.FindControl("hdnValidateFormDesignatedPensionerOrAddicionalInsured") as HiddenField).Value = ((sender as RadioButton).ID == "si1"
                    && (sender as RadioButton).Checked) ? "true" : "false";
            }

            #region Delete Contact Role Designated Pensioner and Additional Insured
            if (ObjServices.DesignatedPensionerContactId.HasValue &&
               ObjServices.DesignatedPensionerContactId > 0)
            {
                if (!HaveDesignatedPensioner())
                    this.MessageBox(Resources.AdditionalInsuredOrDesigantedPensionervalidation, Title: ObjServices.Language == Utility.Language.en ? "Warning" : "Advertencia");
            }
            else
                if (oWUCDesignatedPensionerInformation != null)
                    oWUCDesignatedPensionerInformation.ClearData();
            #endregion
            if (oudpDesignatedPensioner != null)
                oudpDesignatedPensioner.Update();
        }
        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ContributionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            setControls();
            setVariables();
            var drop = sender as DropDownList;
            if (drop.SelectedValue == "-1") return;
            try
            {
                //txtContributionPeriod.Enabled = (drop.SelectedValue == "2" || drop.SelectedValue == "3");

                //if (!txtContributionPeriod.Enabled)
                //{
                //    txtContributionPeriod.Clear("0.00");
                //    txtContributionPeriod.Attributes.Remove("validation");
                //}
                //else
                //    txtContributionPeriod.Attributes.Add("validation", "Required");

                ddlContributionPeriod.Enabled = (drop.SelectedValue == "2" || drop.SelectedValue == "3" || drop.SelectedValue == "4");
                var planName = this.ddlProductName.SelectedItem.Text;

                if (!ddlContributionPeriod.Enabled)
                {
                    //ddlContributionPeriod.Clear("0.00");
                    ddlContributionPeriod.Attributes.Remove("validation");
                }
                else
                {
                    // this.FillDropSentinelAndLightHouse(planName);
                    ddlContributionPeriod.Attributes.Add("validation", "Required");
                }
                //if (drop.SelectedItem.Text.ToUpper().Contains("NUMERO DE MESES"))
                //{
                //    this.FillDropSentinelAndLightHouse(planName);
                //}
                this.FillDropSentinelAndLightHouse(planName);
            }
            catch (Exception)
            {
                ddlContributionPeriod.Enabled = (drop.SelectedValue == "2" || drop.SelectedValue == "3");

                if (!ddlContributionPeriod.Enabled)
                {
                    ddlContributionPeriod.SelectedValue = "-1";
                    ddlContributionPeriod.Attributes.Remove("validation");
                }
                else
                    ddlContributionPeriod.Attributes.Add("validation", "Required");
            }
        }

        protected void ddlManageRidersDropDowns_SelectedIndexChanged(object sender, EventArgs e)
        {
            setControls();

            var drop = (sender as DropDownList);

            //Esto se comento porque mandaron a quitar el Rider Lote
            //if (drop == ddlLote)
            //{
            //    ddlLoteType.Enabled = (drop.SelectedValue == "1");

            //    if (!ddlLoteType.Enabled)
            //    {
            //        ddlLoteType.SelectedValue = "-1";
            //        ddlLoteType.Attributes.Remove("validation");
            //    }
            //    else
            //        ddlLoteType.Attributes.Add("validation", "Required");

            //    return;
            //}

            if (drop == ddlAccidentalDeathBenefits)
            {
                txtAccidentalDeathInsuredAmount.Enabled = (drop.SelectedValue == "1");

                if (!txtAccidentalDeathInsuredAmount.Enabled)
                {
                    txtAccidentalDeathInsuredAmount.Clear();
                    txtAccidentalDeathInsuredAmount.Attributes.Remove("validation");
                }
                else
                    txtAccidentalDeathInsuredAmount.Attributes.Add("validation", "Required");

                return;
            }

            if (drop == ddlCritialIllness)
            {
                txtCritialIllnessInsuredAmount.Enabled = (drop.SelectedValue == "1");

                if (!txtCritialIllnessInsuredAmount.Enabled)
                {
                    txtCritialIllnessInsuredAmount.Clear();
                    txtCritialIllnessInsuredAmount.Attributes.Remove("validation");
                }
                else
                    txtCritialIllnessInsuredAmount.Attributes.Add("validation", "Required");

                return;
            }


            if (drop == ddlOtherInsured || drop == ddlDependents)
            {
                oWUCDesignatedPensionerInformation.EnableControls((drop.SelectedValue == "1"));
                (oWUCDesignatedPensionerInformation.FindControl("hdnValidateFormDesignatedPensionerOrAddicionalInsured")
                     as HiddenField).Value = drop.SelectedValue == "1" ? "true" : "false";
            }

            if (drop == ddlDependents)
            {
                if (!ObjServices.isSavePlan && ddlDependents.SelectedValue == "1")
                {
                    ddlDependents.SelectedValue = "2";
                    oWUCDesignatedPensionerInformation.EnableControls(false);
                    this.MessageBox(Resources.DependentsValidationPolicy, Title: Resources.Warning);
                    (oWUCDesignatedPensionerInformation.FindControl("hdnValidateFormDesignatedPensionerOrAddicionalInsured")
                     as HiddenField).Value = "false";
                    return;
                }

                oWUCDesignatedPensionerInformation.EnableControls((drop.SelectedValue == "1"));
                return;
            }

            if (drop == ddlSpouseOtherInsured)
            {
                txtSpouseOtherInsured.Enabled = (drop.SelectedValue == "1");
                txtYearsSpouseOther.Enabled = (drop.SelectedValue == "1");
                oWUCDesignatedPensionerInformation.EnableControls((drop.SelectedValue == "1"));

                if (!txtSpouseOtherInsured.Enabled)
                {
                    txtSpouseOtherInsured.Clear();
                    txtSpouseOtherInsured.Attributes.Remove("validation");
                }
                else
                    txtSpouseOtherInsured.Attributes.Add("validation", "Required");

                if (!txtYearsSpouseOther.Enabled)
                {
                    txtYearsSpouseOther.Clear();
                    txtYearsSpouseOther.Attributes.Remove("validation");
                }
                else
                    txtYearsSpouseOther.Attributes.Add("validation", "Required");

                return;
            }

            if (drop == ddlAdditionalTermInsurance)
            {
                txtAdditionalTermInsuranceInsuredAmount.Enabled = (drop.SelectedValue == "1");
                if (!txtAdditionalTermInsuranceInsuredAmount.Enabled)
                {
                    txtAdditionalTermInsuranceInsuredAmount.Clear();
                    txtAdditionalTermInsuranceInsuredAmount.Attributes.Remove("validation");
                }
                else
                    txtAdditionalTermInsuranceInsuredAmount.Attributes.Add("validation", "Required");
                return;
            }


            if (drop == ddlFinancialGlobal)
            {
                txtAmount2.Enabled = (drop.SelectedValue == "1");
                txtAtAge.Enabled = (drop.SelectedValue == "1");

                if (txtAmount2 != null)
                {
                    if (!txtAmount2.Enabled)
                    {
                        txtAmount2.Clear();
                        txtAmount2.Attributes.Remove("validation");
                    }
                    else
                        txtAmount2.Attributes.Add("validation", "Required");
                }
                if (txtAtAge != null)
                {
                    if (!txtAtAge.Enabled)
                    {
                        txtAtAge.Clear();
                        txtAtAge.Attributes.Remove("validation");
                    }
                    else
                        txtAtAge.Attributes.Add("validation", "Required");
                }
                return;
            }
        }

        protected void btnPProfile_Click(object sender, EventArgs e)
        {
            setControls();
            var drop = ddlInvestmentProfile;
            var keyInvestProfdata = Utility.deserializeJSON<Utility.KeyInvestProfile>(drop.SelectedValue);
            var selectedProfileId = keyInvestProfdata.ProfileTypeId;

            var data = ObjServices.oPolicyManager.GetInvestProfilePersonalized(
                        ObjServices.Corp_Id,
                        ObjServices.Region_Id,
                        ObjServices.Country_Id,
                        ObjServices.Domesticreg_Id,
                        ObjServices.State_Prov_Id,
                        ObjServices.City_Id,
                        ObjServices.Office_Id,
                        ObjServices.Case_Seq_No,
                        ObjServices.Hist_Seq_No,
                        selectedProfileId);

            if (data.Any())
            {
                UCPopPerzonalizedProfile.FillData(data, new List<DropDownList>() { ddlProductName, ddlFamilyProduct });
                hdnShowPopPersonalizeInvstProf.Value = "true";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlInvestmentProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            setControls();

            var drop = (sender as DropDownList);

            if (drop.Items.Count > 0 && drop.SelectedValue != "-1")
            {
                var keyInvestProfdata = Utility.deserializeJSON<Utility.KeyInvestProfile>(drop.SelectedValue);
                var isModifiable = keyInvestProfdata.Modifiable;
                btnPProfile.Enabled = isModifiable;
            }
        }

        protected void ddlInitialContribution_SelectedIndexChanged(object sender, EventArgs e)
        {
            setControls();
            txtAmount.Enabled = (sender as DropDownList).SelectedValue == "1";
            if (!txtAmount.Enabled)
            {
                txtAmount.Clear();
                txtAmount.Attributes.Remove("validation");
            }
            else
            {
                txtAmount.Attributes.Add("validation", "Required");
                txtAmount.Focus();
            }
        }
        private void EnabledSpecialAmount(DropDownList select)
        {

            this.UCLightHouse._txtSpecialPayment.Enabled = select.SelectedValue == "1";
            if (this.UCLightHouse._txtSpecialPayment.Enabled)
            {
                this.UCLightHouse._txtSpecialPayment.Attributes.Add("validation", "Required");
                this.UCLightHouse._txtSpecialPayment.Focus();
            }
            else
            {
                this.UCLightHouse._txtSpecialPayment.Clear();
                this.UCLightHouse._txtSpecialPayment.Attributes.Remove("validation");
            }
        }

        protected void ddlSpecialPayments_SelectedIndexChanged(object sender, EventArgs e)
        {
            setControls();
            this.UCLightHouse._txtSpecialPayment.Enabled = (sender as DropDownList).SelectedValue == "1";
            if (this.UCLightHouse._txtSpecialPayment.Enabled)
            {
                this.UCLightHouse._txtSpecialPayment.Attributes.Add("validation", "Required");
                this.UCLightHouse._txtSpecialPayment.Focus();
            }
            else
            {
                this.UCLightHouse._txtSpecialPayment.Clear();
                this.UCLightHouse._txtSpecialPayment.Attributes.Remove("validation");
            }
        }
        //protected void ddlSpecialPayment_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    setControls();
        //    this.UCLightHouse._txtSpecialPayment.Enabled = (sender as DropDownList).SelectedValue == "1";
        //    if (this.UCLightHouse._txtSpecialPayment.Enabled)
        //    {
        //        this.UCLightHouse._txtSpecialPayment.Attributes.Add("validation", "Required");
        //        this.UCLightHouse._txtSpecialPayment.Focus();
        //    }
        //    else
        //    {
        //        this.UCLightHouse._txtSpecialPayment.Clear();
        //        this.UCLightHouse._txtSpecialPayment.Attributes.Remove("validation");
        //    }

        //}

        protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            setControls();
            ObjServices.IsPlanChange = true;
            setControls();

            DeleteAll();
            var Product = Utility.deserializeJSON<Utility.itemProduct>(((DropDownList)sender).SelectedValue);
            SelectControl(Product);
            FillData();

            if (ddlCurrency != null && ddlCurrency.DataSource != null)
            {
                ddlCurrency.SelectedIndex = 1;
                var count = ddlCurrency.Items.Count;
                ddlCurrency.Enabled = !(count <= 2);

            }
            if (ddlContributionType != null && ddlContributionType.DataSource != null)
            {
                this.ddlContributionType.SelectedIndex = 1;
                var count = ddlContributionType.Items.Count;
                this.ddlContributionType.Enabled = !(count <= 2);
            }
        }

        protected void ddlFamilyProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            var generateSelect = (Boolean)ViewState["GenerateSelect"];

            setControls();

            DeleteAll();

            var ddlfamilyProduct = (DropDownList)sender;

            //Validacion solo para los planes de Salud
            var ProductFamilySelect = Utility.deserializeJSON<Utility.FamilyProduct>(ddlfamilyProduct.SelectedValue);
            if (ProductFamilySelect.ProductTypeId == 28)
            {
                var LatinAmericanCountries = ObjServices
                                            .GettingDropData(Utility.DropDownType.LatinAmericanCountries).Select(y => y.CountryId);


                var ContactData = ObjServices.GetContact(ObjServices.Contact_Id.Value);
                if (ContactData != null)
                {
                    var IDPaisSeleccionado = ContactData.CountryOfResidenceId;
                    if (!LatinAmericanCountries.Where(x => x.Value == IDPaisSeleccionado).Any())
                    {
                        this.MessageBox(Resources.CountryOfResidenceValidation);
                        return;
                    }

                }

            }

            if (ddlfamilyProduct.SelectedValue != "-1")
            {
                var family = Utility.deserializeJSON<Utility.FamilyProduct>(ddlfamilyProduct.SelectedValue);

                ObjServices.GettingAllDropsJSON(ref ddlProductName
                                            , Utility.DropDownType.ProductByFamily
                                            , "ProductDesc"
                                            , corpId: CorpId
                                            , regionId: RegionId
                                            , countryId: CountryId
                                            , domesticregId: DomesticregId
                                            , stateProvId: StateProvId
                                            , cityId: CityId
                                            , officeId: OfficeId
                                            , BlId: family.BlId
                                            , BlTypeId: family.BlTypeId
                                            , ProductTypeId: family.ProductTypeId
                                            , GenerateItemSelect: generateSelect
                                           );

                if (ddlProductName.SelectedValue != "-1")
                {
                    var Product = Utility.deserializeJSON<Utility.itemProduct>(ddlProductName.SelectedValue);
                    SelectControl(Product);
                    FillData();
                }
            }
            else
                ddlProductName.Items.Clear();
        }

        #endregion

        private void SelectControl(Utility.itemProduct Product)
        {
            setControls();
            var pnFooter = oWUCFieldFooter.FindControl("pnFooter");

            string dropDown;
            int changeView, contactType;
            View oView = null;
            int indexView = 0;

            if (!pnFooter.isNullReferenceControl())
                Utility.EnableControls(pnFooter.Controls, true);

            if (Product.isNullReferenceObject())
            {   //Mostrar pantalla por defecto
                mvSelectControl.SetActiveView(VBasicPlan);
                fillDefaultDrop("VBasicPlan", Product);
                ClearData();
                if (!opnDesignatedPensioner.isNullReferenceControl())
                    Utility.EnableControls(opnDesignatedPensioner.Controls, false);

                if (!pnFooter.isNullReferenceControl())
                    Utility.EnableControls(pnFooter.Controls, false);
            }
            else
            {
                dropDown = string.Empty;
                changeView = 0;
                oView = new View();
                contactType = Utility.ContactRoleIDType.DesignatedPensioner.ToInt();

                switch (Product.Product)
                {
                    case Utility.ProductBehavior.Horizon:
                        oView = VHorizon;
                        dropDown = "VHorizon";
                        changeView = 1;
                        contactType = Utility.ContactRoleIDType.DesignatedPensioner.ToInt();
                        break;
                    case Utility.ProductBehavior.Axys:
                        oView = VAxy;
                        dropDown = "VAxy";
                        changeView = 8;
                        contactType = Utility.ContactRoleIDType.DesignatedPensioner.ToInt();
                        break;
                    case Utility.ProductBehavior.EduPlan:
                        oView = VEduplan;
                        dropDown = "VEduplan";
                        changeView = 2;
                        contactType = Utility.ContactRoleIDType.DesignatedPensioner.ToInt();
                        break;
                    case Utility.ProductBehavior.Scholar:
                        oView = VScholar;
                        dropDown = "VScholar";
                        changeView = 3;
                        contactType = Utility.ContactRoleIDType.DesignatedPensioner.ToInt();
                        break;
                    case Utility.ProductBehavior.CompassIndex:
                        oView = VCompassIndex;
                        dropDown = "VCompassIndex";
                        changeView = 4;
                        contactType = Utility.ContactRoleIDType.AddicionalInsured.ToInt();
                        break;
                    case Utility.ProductBehavior.Legacy:
                        oView = VLegacy;
                        dropDown = "VLegacy";
                        changeView = 5;
                        contactType = Utility.ContactRoleIDType.AddicionalInsured.ToInt();
                        break;
                    case Utility.ProductBehavior.Sentinel:
                        oView = VSentinel;
                        dropDown = "VSentinel";
                        changeView = 7;
                        contactType = Utility.ContactRoleIDType.AddicionalInsured.ToInt();
                        break;
                    case Utility.ProductBehavior.Lighthouse:
                        oView = VLightHouse;
                        dropDown = "VLightHouse";
                        changeView = 6;
                        contactType = Utility.ContactRoleIDType.AddicionalInsured.ToInt();
                        break;
                    #region Planes de Salud
                    case Utility.ProductBehavior.Select:
                        oView = VSelect;
                        dropDown = "VSelect";
                        changeView = 9;
                        indexView = 0;
                        contactType = Utility.ContactRoleIDType.Dependent.ToInt();
                        break;
                    case Utility.ProductBehavior.Elite:
                        oView = VElite;
                        dropDown = "VElite";
                        changeView = 10;
                        indexView = 0;
                        contactType = Utility.ContactRoleIDType.Dependent.ToInt();
                        break;
                    case Utility.ProductBehavior.Fortis:
                        oView = VFortis;
                        dropDown = "VFortis";
                        changeView = 10;
                        indexView = 0;
                        contactType = Utility.ContactRoleIDType.Dependent.ToInt();
                        break;
                    case Utility.ProductBehavior.Serenity:
                        oView = VSerenity;
                        dropDown = "VSerenity";
                        changeView = 10;
                        indexView = 0;
                        contactType = Utility.ContactRoleIDType.Dependent.ToInt();
                        break;
                    case Utility.ProductBehavior.Asistencia30dias:
                        oView = VAsistencia30dias;
                        dropDown = "VAsistencia30dias";
                        changeView = 10;
                        indexView = 0;
                        contactType = Utility.ContactRoleIDType.Dependent.ToInt();
                        break;
                    case Utility.ProductBehavior.Asistencia60dias:
                        oView = VAsistencia60dias;
                        dropDown = "VAsistencia60dias";
                        changeView = 10;
                        indexView = 0;
                        contactType = Utility.ContactRoleIDType.Dependent.ToInt();
                        break;
                    case Utility.ProductBehavior.Asistencia90dias:
                        oView = VAsistencia90dias;
                        dropDown = "VAsistencia90dias";
                        changeView = 10;
                        indexView = 0;
                        contactType = Utility.ContactRoleIDType.Dependent.ToInt();
                        break;
                    #endregion
                    #region Guardian
                    //Sentinel and Guardian have the same behavior that's why the view, dropdown and the chageView are the same.  
                    case Utility.ProductBehavior.Guardian:
                        oView = VSentinel;
                        dropDown = "VSentinel";
                        changeView = 7;
                        contactType = Utility.ContactRoleIDType.AddicionalInsured.ToInt();
                        break;
                    case Utility.ProductBehavior.GuardianPlus:
                        oView = VSentinel;
                        dropDown = "VSentinel";
                        changeView = 7;
                        contactType = Utility.ContactRoleIDType.AddicionalInsured.ToInt();
                        break;
                    #endregion
                    #region Orion
                    //LightHouse and Orion have the same behavior that's why the view, dropdown and the chageView are the same.                       
                    case Utility.ProductBehavior.Orion:
                        oView = VLightHouse;
                        dropDown = "VLightHouse";
                        changeView = 6;
                        contactType = Utility.ContactRoleIDType.AddicionalInsured.ToInt();
                        this.UCLightHouse._divDllFinancialInstitucionGroup.Visible = false;
                        this.UCLightHouse._divSpecialPayment.Visible = false;
                        this.UCLightHouse._divDestinyFund.Visible = false;
                        break;
                    case Utility.ProductBehavior.OrionPlus:
                        oView = VLightHouse;
                        dropDown = "VLightHouse";
                        changeView = 6;
                        contactType = Utility.ContactRoleIDType.AddicionalInsured.ToInt();
                        this.UCLightHouse._divDllFinancialInstitucionGroup.Visible = false;
                        this.UCLightHouse._divSpecialPayment.Visible = false;
                        this.UCLightHouse._divDestinyFund.Visible = false;
                        break;

                    #endregion

                    #region VIDACRED
                    //LightHouse and Orion have the same behavior that's why the view, dropdown and the chageView are the same.                                           
                    case Utility.ProductBehavior.VIDACRED:
                        oView = VLightHouse;
                        dropDown = "VLightHouse";
                        changeView = 6;
                        contactType = Utility.ContactRoleIDType.AddicionalInsured.ToInt();
                        this.UCLightHouse._divDllFinancialInstitucionGroup.Visible = true;
                        this.UCLightHouse._divSpecialPayment.Visible = true;
                        this.UCLightHouse._divDestinyFund.Visible = true;
                        this.EnabledSpecialAmount(this.UCLightHouse._ddlSpecialPayment);
                        this.ddlFinancialInstitution = this.UCLightHouse._ddlFinancialInstitution;
                        ObjServices.GettingAllDropsJSON(ref this.ddlFinancialInstitution, Utility.DropDownType.Provider, "ElementDesc", false,
                                         ObjServices.Corp_Id,
                                         ObjServices.Region_Id,
                                         ObjServices.Country_Id
                                         , ObjServices.Domesticreg_Id
                                         , ObjServices.State_Prov_Id
                                         , ObjServices.City_Id
                                         , ObjServices.Office_Id
                                         , ObjServices.Case_Seq_No
                                         , ObjServices.Hist_Seq_No,
                                         agentId: 1);
                        break;
                    #endregion
                    #region Luminis, Luminis VIP, Exequium, Exequium VIP
                    case Utility.ProductBehavior.Luminis:
                    case Utility.ProductBehavior.LuminisVIP:
                    case Utility.ProductBehavior.Exequium:
                    case Utility.ProductBehavior.ExequiumVIP:
                        oView = VFunerarios;
                        dropDown = "VFunerarios";
                        changeView = 11;
                        contactType = Utility.ContactRoleIDType.AddicionalInsured.ToInt();
                        indexView = 1;
                        break;
                    #endregion
                    default:
                        break;
                }

                hfSelectControls.Value = dropDown;

                setControls();

                if (!pnPlanType.isNullReferenceControl())
                {
                    var isVisible = true;

                    if (Product.Product == Utility.ProductBehavior.GuardianPlus ||
                                    Product.Product == Utility.ProductBehavior.Guardian ||
                                    Product.Product == Utility.ProductBehavior.Orion ||
                                    Product.Product == Utility.ProductBehavior.OrionPlus)
                        isVisible = false;

                    pnPlanType.Visible = isVisible;
                }

                mvSelectControl.SetActiveView(oView);
                fillDefaultDrop(dropDown, Product);
                oWUCFieldFooter.ChangeView(changeView);
                oWUCFieldFooter.FillData();

                var txtInsuredAmount = (oWUCFieldFooter.Controles.FindControl("txtInsuredAmount") as TextBox);
                var txtAnnualPremium = (oWUCFieldFooter.Controles.FindControl("txtAnnualPremium") as TextBox);
                var txtSelectiveTax = (oWUCFieldFooter.Controles.FindControl("txtSelectiveTax") as TextBox);
                var txtAnnualPremiumWithTax = (oWUCFieldFooter.Controles.FindControl("txtAnnualPremiumWithTax") as TextBox);

                var Lote = ((Panel)Controles.FindControl("pnLote"));

                var data = ObjServices.GettingDropData(
                                                       Utility.DropDownType.ProjectConfigurationValue,
                                                       corpId: ObjServices.Corp_Id,
                                                       pProjectId: int.Parse(System.Configuration.ConfigurationManager.AppSettings["ProjectIdNewBusiness"])
                                                      );

                if (txtInsuredAmount != null && txtAnnualPremiumWithTax != null && txtAnnualPremium != null && txtSelectiveTax != null)
                {
                    string Amount = string.Empty;

                    if (data != null)
                    {
                        switch (Product.Product)
                        {
                            case Utility.ProductBehavior.Luminis:
                                Amount = data.FirstOrDefault(x => x.Namekey == "InsuranceAmountLuminis").ConfigurationValue;
                                break;
                            case Utility.ProductBehavior.LuminisVIP:
                                Amount = data.FirstOrDefault(x => x.Namekey == "InsuranceAmountLuminisVIP").ConfigurationValue;
                                break;
                            case Utility.ProductBehavior.Exequium:
                                Amount = data.FirstOrDefault(x => x.Namekey == "InsuranceAmountExequium").ConfigurationValue;
                                Lote.Visible = false;
                                break;
                            case Utility.ProductBehavior.ExequiumVIP:
                                Amount = data.FirstOrDefault(x => x.Namekey == "InsuranceAmountExequiumVIP").ConfigurationValue;
                                Lote.Visible = false;
                                break;
                            case Utility.ProductBehavior.Elite:
                                Amount = data.FirstOrDefault(x => x.Namekey == "InsuranceAmountElite").ConfigurationValue;
                                break;
                            case Utility.ProductBehavior.Select:
                                Amount = data.FirstOrDefault(x => x.Namekey == "InsuranceAmountSelect").ConfigurationValue;
                                break;
                            case Utility.ProductBehavior.Fortis:
                                Amount = data.FirstOrDefault(x => x.Namekey == "InsuranceAmountFortis").ConfigurationValue;
                                break;
                            case Utility.ProductBehavior.Asistencia30dias:
                            case Utility.ProductBehavior.Asistencia60dias:
                            case Utility.ProductBehavior.Asistencia90dias:
                            case Utility.ProductBehavior.Serenity:
                                oWUCFieldFooter.txtInsuredAmount.ReadOnly = false;
                                break;
                            default:
                                break;
                        }
                    }

                    txtInsuredAmount.Text = Amount;

                    var AnnualPremium = decimal.Parse(txtAnnualPremium.Text, NumberFormatInfo.InvariantInfo);

                    txtSelectiveTax.Text = (AnnualPremium * Utility.GetItbis()).ToString("#,0.00", NumberFormatInfo.InvariantInfo);

                    txtAnnualPremiumWithTax.Text = (AnnualPremium +
                                                   decimal.Parse(txtSelectiveTax.Text, NumberFormatInfo.InvariantInfo)).ToString("#,0.00", NumberFormatInfo.InvariantInfo);
                }

                oWUCDesignatedPensionerInformation.ContactRoleTypeID = contactType;

                oWUCDesignatedPensionerInformation.ChangeView(indexView);

                if (indexView == 1)
                {
                    oWUCDesignatedPensionerInformation.ClearData();
                    oWUCDesignatedPensionerInformation.FillData();
                    oWUCDesignatedPensionerInformation.Operation = Utility.OperationType.Insert;
                }
            }

            udpPlanInformation.Update();
        }

        public void EnableControls(bool x)
        {
            if (!Controles.isNullReferenceControl())
                Utility.EnableControls(Controles.FindControl("frmPlan").Controls, x);
        }

        public void FillData()
        {
            var vView = mvSelectControl.GetActiveView();

            if (vView == VHorizon)
            {
                UCHorizon._si1.Checked = (ObjServices.DesignatedPensionerContactId.Value > 0);
                UCHorizon._no1.Checked = (ObjServices.DesignatedPensionerContactId.Value < 0);
                ManageHaveDesigantedPensionerQuestion(UCHorizon._si1, null);
            }
            else
                if (vView == VAxy)
                {
                    UCAxy._si1.Checked = (ObjServices.DesignatedPensionerContactId.Value > 0);
                    UCAxy._no1.Checked = (ObjServices.DesignatedPensionerContactId.Value < 0);
                    ManageHaveDesigantedPensionerQuestion(UCAxy._si1, null);
                }
                else
                    if (vView == VEduplan)
                    {
                        UCEduplan._si1.Checked = (ObjServices.DesignatedPensionerContactId.Value > 0);
                        UCEduplan._no1.Checked = (ObjServices.DesignatedPensionerContactId.Value < 0);
                        ManageHaveDesigantedPensionerQuestion(UCEduplan._si1, null);
                    }
                    else
                        if (vView == VScholar)
                        {
                            UCScholar._si1.Checked = (ObjServices.DesignatedPensionerContactId.Value > 0);
                            UCScholar._no1.Checked = (ObjServices.DesignatedPensionerContactId.Value < 0);
                            ManageHaveDesigantedPensionerQuestion(UCScholar._si1, null);
                        }
                        else
                        {
                            if (ObjServices.isSavePlan)
                            {
                                DropDownList drop = null;
                                drop = (!ObjServices.esFunerario()) ? (DropDownList)Controles.FindControl("ddlSpouseOtherInsured")
                                        :
                                        (DropDownList)Controles.FindControl("ddlOtherInsured");

                                ManageHaveRider(drop, null);
                            }
                        }

            (oudpDesignatedPensioner as UpdatePanel).Update();
        }

        public void ClearData()
        {
            setControls();
            var frmPlan = Controles.FindControl("frmPlan");
            Utility.ClearAll(frmPlan.Controls);
        }

        /// <summary>
        /// Retorna si en el form se selecciono que tiene un Addicional Insured
        /// </summary>
        /// <returns></returns>
        public bool HaveAdditionalInsured()
        {
            var Result = false;
            var vView = mvSelectControl.GetActiveView();
            #region Planes de vida
            if (vView == VCompassIndex)
                Result = (UCCompassIndex._ddlSpouseOtherInsured.SelectedValue == "1");
            else
                if (vView == VLegacy)
                    Result = (UCLegacy._ddlSpouseOtherInsured.SelectedValue == "1");
                else
                    if (vView == VSentinel)
                        Result = (UCSentinel._ddlSpouseOtherInsured.SelectedValue == "1");
                    else
                        if (vView == VLightHouse)
                            Result = (UCLightHouse._ddlSpouseOtherInsured.SelectedValue == "1");
                        else
            #endregion
                            #region Planes Funerarios
                            if (vView == VFunerarios)
                                Result = (UCFunerarios._ddlOtherInsured.SelectedValue == "1");
                            #endregion
            return Result;
        }

        /// <summary>
        /// Retorna si en el form se selecciono que tiene un Designated Pensioner 
        /// </summary>
        /// <returns></returns>
        public bool HaveDesignatedPensioner()
        {
            var Result = false;

            var vView = mvSelectControl.GetActiveView();

            if (vView == VHorizon)
                Result = UCHorizon._si1.Checked;
            else
                if (vView == VAxy)
                    Result = UCAxy._si1.Checked;
                else
                    if (vView == VEduplan)
                        Result = UCEduplan._si1.Checked;
                    else
                        if (vView == VScholar)
                            Result = UCScholar._si1.Checked;

            return Result;
        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            var frmPlan = Controles.FindControl("frmPlan");
            var si1 = frmPlan.FindControl("si1");
            var no1 = frmPlan.FindControl("no1");
            var ControlExclude = new List<Control>();

            if (!si1.isNullReferenceControl() && !no1.isNullReferenceControl())
            {
                ControlExclude.Add(si1);
                ControlExclude.Add(no1);
                Utility.ReadOnlyControls(frmPlan.Controls, isReadOnly, ControlExclude);
            }
            else
                Utility.ReadOnlyControls(frmPlan.Controls, isReadOnly);
        }

        private void ValidateAge(bool isYear, bool isOwner, int? ageContact, ref StringBuilder mensa, ref bool respuesta)
        {
            var productSelect = (Utility.ProductBehavior)Utility.getvalueFromEnumType(ObjServices.KeyNameProduct, typeof(Utility.ProductBehavior));

            //Validacion de la edad para los planes de Vida
            if (ObjServices.ProductLine == Utility.ProductLine.LifeInsurance)
            {
                if (!isOwner)
                {
                    //Validar el Insured
                    if (ageContact != null && (isYear && ageContact.Value > 65))
                    {
                        mensa.AppendLine(string.Format(Resources.AgeBetweenMonthsYears, Resources.INSURED.Capitalize(), "3", "65"));
                        respuesta = false;
                    }
                    else if (ageContact != null && ageContact.Value < 3)
                    {
                        mensa.AppendLine(string.Format(Resources.AgeBetweenMonthsYears, Resources.INSURED.Capitalize(), "3", "65"));
                        respuesta = false;
                    }
                }
                //Validar el Owner
                else if (ageContact != null && (isYear && ageContact.Value < 18))
                {
                    mensa.AppendLine(string.Format(Resources.OwnerShouldbeAdult));
                    respuesta = false;
                }
            }
            //Validacion de la edad para los planes de Salud
            else if (ObjServices.ProductLine == Utility.ProductLine.HealthInsurance)
            {
                //Validar el Insured
                if (ageContact != null && isYear)
                {
                    switch (productSelect)
                    {
                        case Utility.ProductBehavior.Elite:
                        case Utility.ProductBehavior.Select:
                        case Utility.ProductBehavior.Fortis:
                            //Validacion de la edad del Titular
                            if (!(ageContact.Value >= 18 && ageContact.Value <= 74))
                            {
                                mensa.AppendLine(string.Format(Resources.AgeValidationMessage, Resources.INSURED.Capitalize(), "18", "74"));
                                respuesta = false;
                            }

                            break;
                        case Utility.ProductBehavior.Serenity:
                            //Validacion de la edad del Titular
                            if (!(ageContact.Value >= 18 && ageContact.Value <= 65))
                            {
                                mensa.AppendLine(string.Format(Resources.AgeValidationMessage, Resources.INSURED.Capitalize(), "18", "65"));
                                respuesta = false;
                            }
                            break;
                        case Utility.ProductBehavior.Asistencia90dias:
                        case Utility.ProductBehavior.Asistencia30dias:
                        case Utility.ProductBehavior.Asistencia60dias:
                            //Validacion de la edad del Titular
                            if (!(ageContact.Value >= 18 && ageContact.Value <= 65))
                            {
                                mensa.AppendLine(string.Format(Resources.AgeValidationMessage, Resources.INSURED.Capitalize(), "18", "70"));
                                respuesta = false;
                            }
                            break;
                        default:
                            break;
                    }

                }

            }
            //Validaciones de la edad para los planes funerarios
            else if (ObjServices.ProductLine == Utility.ProductLine.Funeral)
            {
                if (ageContact == null || (ageContact.Value >= 18 && ageContact.Value <= 69))
                    return;

                if (isOwner)
                    mensa.AppendLine(string.Format(Resources.OwnerShouldbeAdult));
                else
                    mensa.AppendLine(string.Format(Resources.AgeShouldbeYears, "18", "69"));

                respuesta = false;
            }
        }

        public void FillDropSentinelAndLightHouse(string plan)
        {

            var data = ObjServices.GettingDropData(Utility.DropDownType.ProjectConfigurationValue, corpId: ObjServices.Corp_Id, pProjectId: int.Parse(System.Configuration.ConfigurationManager.AppSettings["ProjectIdNewBusiness"]));
            if (data != null)
            {
                DropDownList drop = null;

                drop = (!ObjServices.esFunerario()) ? (DropDownList)Controles.FindControl("ddlContributionPeriod") : null;
                var paran = plan.Contains("Guardian") ? "DropSentinel" : "DropLightHouse";
                int? yearsConvertedToMonth = null;
                int? contributionPeriod = null;
                var datos = ObjServices.oPolicyManager.GetPlanData(CorpId, RegionId, CountryId, DomesticregId, StateProvId
                , CityId, OfficeId, CaseSeqNo, HistSeqNo);
                //El 4 hace referencia a numero de meses opcion incluida solamente en el plan de vida credito al momento de crear una poliza.
                if (plan.Contains("Vida Credito") && this.ddlContributionType.SelectedValue == "4")
                {
                    paran = "DropLightHouseVidaCredit";
                    if (datos != null && datos.ContributionYears.HasValue)
                    {
                        yearsConvertedToMonth = datos.ContributionYears * 12;
                        if (datos.ContributionMonths.HasValue)
                        {
                            contributionPeriod = (yearsConvertedToMonth + datos.ContributionMonths);
                        }
                        else
                        {
                            contributionPeriod = yearsConvertedToMonth;
                        }
                    }

                }
                else
                {
                    if (datos != null && datos.ContributionYears.HasValue)
                    {
                        contributionPeriod = datos.ContributionYears.Value;
                    }

                }
                var valores = data.FirstOrDefault(x => x.Namekey == paran).ConfigurationValue;
                var valorfrom = valores.Split(':')[0].ToInt();
                var valorto = valores.Split(':')[1].ToInt();
                int y = 0;

                if (drop != null)
                {
                    if (drop.Items.Count <= 0)
                    {
                        drop.Items.Clear();
                        for (int i = valorfrom; i <= valorto; i++)
                        {
                            drop.Items.Add(i.ToString());
                            drop.Items[y].Value = i.ToString();
                            y++;
                        }

                        drop.Items.Insert(0, new ListItem() { Value = "-1", Text = "----" });
                        if (contributionPeriod.HasValue)
                        {
                            drop.SelectIndexByValue(contributionPeriod.ToString());
                        } 
                    } 
                }
            }
        }
    }
}