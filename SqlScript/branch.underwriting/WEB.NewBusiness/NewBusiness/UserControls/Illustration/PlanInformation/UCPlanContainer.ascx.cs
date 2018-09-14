﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.UnderWriting.IllusData;
using RESOURCE.UnderWriting.NewBussiness;
using WEB.NewBusiness.Common;
using System.Globalization;

namespace WEB.NewBusiness.NewBusiness.UserControls.Illustration.PlanInformation
{
    public partial class UCPlanContainer : UC, IUC
    {
        #region Properties
        #region Private
        private UCIllustrationContainer UCContainer
        {
            get
            {
                return (UCIllustrationContainer)Page.Controls[0].FindControl("bodyContent").FindControl("UCIllustrationContainer");
            }
        }

        private string ProductDetailInformation
        {
            get
            {
                var pdi = ViewState["ProductDetailInformation"];
                return pdi == null ? null : ViewState["ProductDetailInformation"].ToString();
            }
            set
            {
                ViewState["ProductDetailInformation"] = value;
            }
        }

        private string ProductName
        {
            get
            {
                var pdi = ProductDetailInformation;
                if (pdi.isNullReferenceObject())
                    return null;
                dynamic objPdi = Newtonsoft.Json.JsonConvert.DeserializeObject(pdi);
                return objPdi.ProductName;
            }
        }

        private string IllustrationNo
        {
            get
            {
                var v = ViewState["IllustrationNo"];
                return v == null ? null : ViewState["IllustrationNo"].ToString();
            }
            set
            {
                ViewState["IllustrationNo"] = value;
            }
        }
        private string FinancialInstitionSelected { get; set; }
        #endregion
        #region Public
        public string ProductCode
        {
            get
            {
                var pdi = ProductDetailInformation;
                if (pdi.isNullReferenceObject())
                    return null;
                dynamic objPdi = Newtonsoft.Json.JsonConvert.DeserializeObject(pdi);
                return objPdi.ProductCode;
            }
        }

        public string FamilyProductCode
        {
            get
            {
                var pdi = ProductDetailInformation;
                if (pdi.isNullReferenceObject())
                    return null;
                dynamic objPdi = Newtonsoft.Json.JsonConvert.DeserializeObject(pdi);
                return objPdi.FamilyProductCode;
            }
        }

        public int? OwnerId
        {
            get
            {
                return String.IsNullOrEmpty(hdnOwnerId.Value) ? null : hdnOwnerId.Value.IsIntReturnNull();
            }
            set
            {
                hdnOwnerId.Value = value.ToString();
            }
        }

        public bool CanSaveWithOtherAgent
        {
            get
            {
                return hdnCanSaveWithOtherAgent.Value.ToBoolean();
            }
            set
            {
                hdnCanSaveWithOtherAgent.Value = value.ToString();
            }
        }
        #endregion
        #region Plan Controls
        #region Plan Body
        private TextBox txtContributionPeriodUntilAge;
        private TextBox txtInitialContributionAmount;
        private TextBox txtPeriodicPremiumAmount;
        private TextBox txtInsuredBenefitRetirementAmount;
        private CheckBox chkOtherPlanWithSTL;
        private TextBox txtStudentName; //Scholar, Eduplan
        private TextBox txtStudentAge; //Scholar, Eduplan
        private TextBox txtAmountGoal; //Compass Index, Legacy
        private TextBox txtAtAge; //Compass Index, Legacy
        private DropDownList ddlCurrency;
        private DropDownList ddlFrequencyPayment;
        private DropDownList ddlInitialContribution;
        private DropDownList ddlCalculate;
        private DropDownList ddlRisk;
        private DropDownList ddlPerThousand;
        private DropDownList ddlPlanType;//Diferents of Sentinel, Lighthouse
        private DropDownList ddlContributionType; //Sentinel, Lighthouse, Compass Index, Legacy
        private DropDownList ddlEducationRetirementPeriod; //Axys, Horizon, Scholar, Eduplan
        private DropDownList ddlDefermentPeriod; //Axys, Horizon, Scholar, Eduplan
        private DropDownList ddlInvestmentProfile; //Legacy, Axys, Eduplan
        private DropDownList ddlFinancialGoal; //Compass Index, Legacy
        private DropDownList ddlContributionPeriod; //Funeral
        private DropDownList ddlContributionPeriodYear; //Bmarroquin 09-01-2017 Se agrega como parte de la tropicalizacion de ESA
		private TextBox txtFechaVigenciaPlan; //Lgonzalez 31-01-2017

        private DropDownList ddlSpecialPayment;
        #endregion
        #region Plan Footer
        private TextBox txtFooterTotalInsuredBenefitRetirementAmount;
        private TextBox txtFooterAnnualPremium;
        private TextBox txtFooterPeriodicPremium;
        private TextBox txtFooterPeriodicPremiumTotal;
        private TextBox txtFooterTargetAnnualPremium;
        private TextBox txtFooterMinimumAnnualPremium;//Diferents of Axys, Compass Index, Legacy, Scholar
        private TextBox txtFooterInsuredProspectAge;//Diferents of Legacy, Compass Index
        private TextBox txtFooterInsuredPeriod;//Lighthouse, Sentinel
        private TextBox txtFooterReturnPremium;//Sentinel
        private TextBox txtFooterAtAge;//Sentinel
        private Label lblFooterTax;//Term
        private TextBox txtFooterTax;//Term
        private TextBox txtSpecialPayment;
        private TextBox txtFooterFractionSurcharge; //Lgonzalez 28-04-2017
        private TextBox txtFooterPrimaAnualNeta; //bmarroquin 04-05-2017
        #endregion
        #endregion
        #endregion
        #region Methods
        #region Private
        private void GetFieldsReference()
        {
            var productName = ProductName.Replace(" ", String.Empty);
            var productCode = ProductCode;
            var familyProductCode = FamilyProductCode;
            UserControl uc;
            UserControl ucFooter;

            if (familyProductCode == Utility.EFamilyProductType.TermInsurance.Code())
            {
                uc = UCTermInsurance;
                ucFooter = UCTermInsuranceFooter;
            }
            else if (familyProductCode == Utility.EFamilyProductType.Funeral.Code())
            {
                uc = UCFuneral;
                ucFooter = UCFuneralFooter;
            }
            else
            {
                uc = (UserControl)FindControl("UC" + productName);
                ucFooter = (UserControl)FindControl("UC" + productName + "Footer");
            }

            txtContributionPeriodUntilAge = (TextBox)uc.FindControl("txtContributionPeriod");
            chkOtherPlanWithSTL = (CheckBox)uc.FindControl("chkOtherPlanWithSTL");
            txtInitialContributionAmount = (TextBox)uc.FindControl("txtInitialContributionAmount");
            txtPeriodicPremiumAmount = (TextBox)uc.FindControl("txtPeriodicPremiumAmount");
            txtAmountGoal = (TextBox)uc.FindControl("txtAmountGoal");
            txtAtAge = (TextBox)uc.FindControl("txtAtAge");
            txtStudentName = (TextBox)uc.FindControl("txtStudentName");
            txtStudentAge = (TextBox)uc.FindControl("txtStudentAge");

            ddlCurrency = (DropDownList)uc.FindControl("ddlCurrency");
            ddlFrequencyPayment = (DropDownList)uc.FindControl("ddlFrequencyPayment");
            ddlInitialContribution = (DropDownList)uc.FindControl("ddlInitialContribution");
            ddlCalculate = (DropDownList)uc.FindControl("ddlCalculate");
            ddlRisk = (DropDownList)uc.FindControl("ddlRisk");
            ddlPerThousand = (DropDownList)uc.FindControl("ddlPerThousand");
            txtInsuredBenefitRetirementAmount = (TextBox)uc.FindControl("txtInsuredBenefitRetirementAmount");
            ddlPlanType = (DropDownList)uc.FindControl("ddlPlanType");
            ddlContributionType = (DropDownList)uc.FindControl("ddlContributionType");
            ddlEducationRetirementPeriod = (DropDownList)uc.FindControl("ddlEducationRetirementPeriod");
            ddlDefermentPeriod = (DropDownList)uc.FindControl("ddlDefermentPeriod");
            ddlInvestmentProfile = (DropDownList)uc.FindControl("ddlInvestmentProfile");
            ddlFinancialGoal = (DropDownList)uc.FindControl("ddlFinancialGoal");
            ddlContributionPeriod = (DropDownList)uc.FindControl("ddlContributionPeriod");
            //Bmarroquin 09-01-2017 Se agrega como parte de la tropicalizacion de ESA
            ddlContributionPeriodYear = (DropDownList)uc.FindControl("ddlContributionPeriodYear");
            //Lgonzalez 31-01-2017
            txtFechaVigenciaPlan = (TextBox)uc.FindControl("txtEffectiveDate");

            txtFooterTotalInsuredBenefitRetirementAmount = (TextBox)ucFooter.FindControl("txtTotalInsuredBenefitRetirementAmount");
            txtFooterAnnualPremium = (TextBox)ucFooter.FindControl("txtAnnualPremium");

            //Lgonzalez 28-04-2017
            txtFooterFractionSurcharge = (TextBox)ucFooter.FindControl("txtFraccionamiento");

            txtFooterPeriodicPremium = (TextBox)ucFooter.FindControl("txtPeriodicPremium");
            txtFooterPeriodicPremiumTotal = (TextBox)ucFooter.FindControl("txtPeriodicPremiumTotal");
            txtFooterTargetAnnualPremium = (TextBox)ucFooter.FindControl("txtTargetAnnualPremium");
            txtFooterMinimumAnnualPremium = (TextBox)ucFooter.FindControl("txtMinimumAnnualPremium");
            txtFooterInsuredProspectAge = (TextBox)ucFooter.FindControl("txtInsuredProspectAge");
            txtFooterInsuredPeriod = (TextBox)ucFooter.FindControl("txtInsuredPeriod");
            txtFooterReturnPremium = (TextBox)ucFooter.FindControl("txtReturnPremium");
            txtFooterAtAge = (TextBox)ucFooter.FindControl("txtInsuredProspectAge");
            lblFooterTax = (Label)ucFooter.FindControl("lblTax");
            txtFooterTax = (TextBox)ucFooter.FindControl("txtTax");
			//bmarroquin 04-05-2017
            txtFooterPrimaAnualNeta = (TextBox)ucFooter.FindControl("txtPrimaAnualNeta");
            if (productCode.Contains("VCR"))
            {
                txtSpecialPayment = (TextBox)uc.FindControl("txtSpecialPayment");
                //ddlFinancialInstitution = (DropDownList)uc.FindControl("ddlFinancialInstitution");
                ddlSpecialPayment = (DropDownList)uc.FindControl("ddlSpecialPayment");
            }
        }

        private void FillDropDown(string familyProductCode, string productCode)
        {
            if (ddlPlanType != null && ddlPlanType.DataSource == null)
                Utility.GettingAllDropsToIllus(ref ddlPlanType, Utility.DropDownType.PlanType, "PlanType", "PlanTypeCode", familyProductCode: familyProductCode, GenerateItemSelect: true, pLang: ObjServices.Language);
            if (ddlCurrency != null && ddlCurrency.DataSource == null)
                Utility.GettingAllDropsToIllus(ref ddlCurrency, Utility.DropDownType.Currency, "Currency", "PClass", productCode: productCode, GenerateItemSelect: true, pLang: ObjServices.Language);
            if (ddlContributionType != null && ddlContributionType.DataSource == null)
                Utility.GettingAllDropsToIllus(ref ddlContributionType, Utility.DropDownType.ContributionType, "ContributionType", "ContributionTypeCode", productCode: productCode, GenerateItemSelect: true, pLang: ObjServices.Language);
            if (ddlFrequencyPayment != null && ddlFrequencyPayment.DataSource == null)
                Utility.GettingAllDropsToIllus(ref ddlFrequencyPayment, Utility.DropDownType.FrequencyType, "FrequencyType", "FrequencyTypeCode|FrequencyValue", familyProductCode: familyProductCode, GenerateItemSelect: true, pLang: ObjServices.Language);
            if (ddlInvestmentProfile != null)
                Utility.GettingAllDropsToIllus(ref ddlInvestmentProfile, Utility.DropDownType.InvestmentProfile, "InvestmentProfile", "InvestmentProfileCode", productCode: productCode, pClass: ddlCurrency.SelectedValue, GenerateItemSelect: true, pLang: ObjServices.Language);
            if (ddlEducationRetirementPeriod != null)
                Utility.GettingAllDropsToIllus(ref ddlEducationRetirementPeriod, Utility.DropDownType.AnnuityPeriod, "Period", "Period", productCode: productCode, GenerateItemSelect: true, pLang: ObjServices.Language);
            if (ddlDefermentPeriod != null)
                Utility.GettingAllDropsToIllus(ref ddlDefermentPeriod, Utility.DropDownType.DefermentPeriod, "Period", "Period", productCode: productCode, GenerateItemSelect: true, pLang: ObjServices.Language);
            if (ddlInitialContribution != null && ddlInitialContribution.DataSource == null)
                Utility.GettingAllDropsToIllus(ref ddlInitialContribution, Utility.DropDownType.Boolean, pLang: ObjServices.Language);
            if (ddlCalculate != null)
                Utility.GettingAllDropsToIllus(ref ddlCalculate, Utility.DropDownType.CalculateType, "CalculateType", "CalculateTypeCode", productCode: productCode, pLang: ObjServices.Language);
            if (ddlRisk != null)
                Utility.GettingAllDropsToIllus(ref ddlRisk,
                    Utility.DropDownType.ActivityRiskType, "ActivityRiskType", "ActivityRiskTypeNo", productCode: productCode, pLang: ObjServices.Language);
            if (ddlPerThousand != null)
                Utility.GettingAllDropsToIllus(ref ddlPerThousand,
                    Utility.DropDownType.HealthRiskType, "HealthRiskType", "HealthRiskTypeNo", productCode: productCode, pLang: ObjServices.Language);
            if (ddlFinancialGoal != null && ddlFinancialGoal.DataSource == null)
                Utility.GettingAllDropsToIllus(ref ddlFinancialGoal, Utility.DropDownType.Boolean, pLang: ObjServices.Language);
            //Bmarroquin 09-01-2017 Se agrega como parte de la tropicalizacion de ESA
            if (ddlContributionPeriodYear != null && ddlContributionPeriodYear.DataSource == null)
                Utility.GettingAllDropsToIllus(ref ddlContributionPeriodYear, Utility.DropDownType.ContributionPeriod, "ContributionPeriod", "ContributionPeriod", familyProductCode: "F", GenerateItemSelect: true, pLang: ObjServices.Language);
		    if (ddlContributionPeriod != null && ddlContributionPeriod.DataSource == null)
                Utility.GettingAllDropsToIllus(ref ddlContributionPeriod, Utility.DropDownType.ContributionPeriod, "ContributionPeriod", "ContributionPeriod", familyProductCode: familyProductCode, GenerateItemSelect: true, pLang: ObjServices.Language);
            if (ddlContributionPeriod != null && ddlContributionPeriod.DataSource == null)
                Utility.GettingAllDropsToIllus(ref ddlContributionPeriod, Utility.DropDownType.ContributionPeriod, "ContributionPeriod", "ContributionPeriod", familyProductCode: familyProductCode, GenerateItemSelect: true, pLang: ObjServices.Language);
            //if (ddlFinancialInstitution != null && ddlFinancialInstitution.DataSource == null)
            //    Utility.GettingAllDropsToIllus(ref ddlFinancialInstitution, Utility.DropDownType.Provider, "ProviderName", "ProviderId", familyProductCode: familyProductCode, GenerateItemSelect: true, pLang: ObjServices.Language, CorpId: ObjServices.Corp_Id
            //        , RegionId: ObjServices.Region_Id, CountryId: ObjServices.Country_Id, DomesticregId: ObjServices.Domesticreg_Id, StateProvId: ObjServices.State_Prov_Id, ProviderTypeId: 1);
        }

        private void SetCustomerPlan(Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetail customerPlan)
        {
            customerPlan.OwnerCustomerNo = hdnOwnerId.Value.IslongReturnNull();
            customerPlan.IsCustomerOwner = String.IsNullOrWhiteSpace(hdnOwnerId.Value) ? "Y" : "N";
            customerPlan.ActivityRiskTypeNo = ddlRisk.SelectedValue.ToInt();
            customerPlan.HealthRiskTypeNo = ddlPerThousand.SelectedValue.ToInt();
            customerPlan.ProductCode = ProductCode;
            customerPlan.PlanEffectiveDate = Convert.ToDateTime(DateTime.ParseExact(txtFechaVigenciaPlan.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture));

            customerPlan.OtherPlans = chkOtherPlanWithSTL.Checked ? "Y" : "N";
            var familyProductCode = FamilyProductCode;
            if (familyProductCode == Utility.EFamilyProductType.Education.Code())
            {
                customerPlan.StudentName = txtStudentName.Text;
                customerPlan.StudentAge = txtStudentAge.Text.IsIntReturnNull();
            }

            customerPlan.PClass = ddlCurrency.SelectedValue;
            dynamic objFrequency = Newtonsoft.Json.JsonConvert.DeserializeObject(ddlFrequencyPayment.SelectedValue);
            customerPlan.FrequencyTypeCode = objFrequency.FrequencyTypeCode;
            customerPlan.Frequency = objFrequency.FrequencyValue;
            if (ddlPlanType != null)
                customerPlan.PlanTypeCode = ddlPlanType.SelectedValue;
            if (ddlCalculate != null)
                customerPlan.CalculateTypeCode = ddlCalculate.SelectedValue;
            customerPlan.ContributionTypeCode = ddlContributionType != null ? ddlContributionType.SelectedValue :
                Utility.EContributionType.NumberOfYears.Code();

            var investmentProfileCode = ddlInvestmentProfile != null ? ddlInvestmentProfile.SelectedValue : "U";//With Guaranteed
            var objInvestmentProfile =
                Utility.GetIllusDropDownByType(Utility.DropDownType.InvestmentProfile, ProductCode, pClass: customerPlan.PClass)
               .FirstOrDefault(o => o.InvestmentProfileCode == investmentProfileCode);
            customerPlan.InvestmentProfileCode = investmentProfileCode;
            customerPlan.InvestmentProfilePercent = objInvestmentProfile != null ? objInvestmentProfile.InvestmentProfileRate.GetValueOrDefault() : 0;

            customerPlan.ContributionPeriod =
                ddlContributionType == null ||
                ddlContributionType.SelectedValue == Utility.EContributionType.NumberOfYears.Code() ?
                (ddlContributionPeriod == null ? txtContributionPeriodUntilAge.ToInt() : ddlContributionPeriod.SelectedValue.ToInt())
                : 0;
            customerPlan.ContributionUntilAge =
                ddlContributionType != null && ddlContributionType.SelectedValue == Utility.EContributionType.UntilAge.Code() ?
                txtContributionPeriodUntilAge.ToInt() : 0;
            customerPlan.FinancialGoal = ddlFinancialGoal != null && ddlContributionType.SelectedValue == "1" ? "Y" : "N";
            customerPlan.FinancialGoalAge = txtAtAge != null ? txtAtAge.ToInt(0) : 0;
            customerPlan.FinancialGoalAmount = txtAmountGoal != null ? txtAmountGoal.ToDecimal(defaultValue: 0) : 0;
            customerPlan.InitialContribution = ddlInitialContribution.SelectedValue == "1" ? txtInitialContributionAmount.ToDecimal(0) : 0;
            customerPlan.InsuredAmount =
                customerPlan.CalculateTypeCode == Utility.CalculateType.AnnuityAmount.Code() ||
                customerPlan.CalculateTypeCode == Utility.CalculateType.InsuredAmount.Code() ||
                txtInsuredBenefitRetirementAmount == null ?
                0 : txtInsuredBenefitRetirementAmount.ToDecimal(defaultValue: 0);
            customerPlan.PremiumAmount = txtPeriodicPremiumAmount.ToDecimal(0, true);
            customerPlan.ChangeType = customerPlan.InsuredAmount > 0 ? "Insured Amount" :
                customerPlan.PremiumAmount > 0 ? "Premium Amount" : "";
            customerPlan.DateUpdated = DateTime.Now;
            customerPlan.BoUpdatedBy = ObjIllustrationServices.IllusUserID;
            customerPlan.BoLastUpdatedBy = ObjIllustrationServices.IllusUserID;
            customerPlan.UpdatedBy = ObjIllustrationServices.IllusUserID.GetValueOrDefault();
            customerPlan.TargetPremium = txtFooterTargetAnnualPremium.ToDecimal(0, true);
            customerPlan.MinimumPremium = txtFooterMinimumAnnualPremium != null ? txtFooterMinimumAnnualPremium.ToDecimal(0, true) : 0;
            customerPlan.AnnualizedPremium = txtFooterAnnualPremium.ToDecimal(0, true);

            if (txtInsuredBenefitRetirementAmount != null)
                if (familyProductCode == Utility.EFamilyProductType.Education.Code() ||
                    familyProductCode == Utility.EFamilyProductType.Retirement.Code())
                    customerPlan.AnnuityAmount = txtInsuredBenefitRetirementAmount.ToDecimal(0, true);
                else
                    customerPlan.BenefitAmount = txtInsuredBenefitRetirementAmount.ToDecimal(0, true);

            if (txtFooterFractionSurcharge != null)
                customerPlan.FractionSurcharge = Convert.ToDouble(txtFooterFractionSurcharge.ToDecimal(2, true));

            //bmarroquin 04-05-2017
            if (txtFooterPrimaAnualNeta != null)
                customerPlan.NetAnnualPremium = Convert.ToDouble(txtFooterPrimaAnualNeta.ToDecimal(2, true));

            if (ddlEducationRetirementPeriod != null)
                customerPlan.RetirementPeriod = ddlEducationRetirementPeriod.SelectedValue.ToDecimal(0);
            if (ddlDefermentPeriod != null)
                customerPlan.DefermentPeriod = ddlDefermentPeriod.SelectedValue.ToDecimal(0);
            if (OwnerId.GetValueOrDefault() > 0)
            {
                ObjServices.Owner_Id = OwnerId;
                var ownerInfo = ObjIllustrationServices.oIllusDataManager.GetCustomerDetailById(null, OwnerId.GetValueOrDefault());
                if (ownerInfo == null && ObjServices.SetCustomerDetailToIllusdata(ObjServices.Corp_Id, OwnerId.GetValueOrDefault(), ObjServices.CompanyId, null, true, this))
                    ownerInfo = ObjIllustrationServices.oIllusDataManager.GetCustomerDetailById(null, OwnerId.GetValueOrDefault());

                customerPlan.IsCustomerOwner = "Y";
                customerPlan.OwnerCustomerNo = ownerInfo.CustomerNo;
            }
            else
            {
                ObjServices.Owner_Id = null;
                customerPlan.IsCustomerOwner = "N";
                customerPlan.OwnerCustomerNo = null;
            }

            if (ObjServices.Country_Id > 0)
            {
                var countryGlobal = ObjServices.GetDropDownByType(Utility.DropDownType.Country).FirstOrDefault(o => o.CountryId == ObjServices.Country_Id);
                if (countryGlobal != null)
                {
                    var countryIllus = Utility.GetIllusDropDownByType(Utility.DropDownType.Country).FirstOrDefault(o => o.CountryName.ToLower() == countryGlobal.GlobalCountryDesc.ToLower());
                    customerPlan.CountryNo = countryIllus == null ? 300 : countryIllus.CountryNo.Value;
                }
                else
                    customerPlan.CountryNo = 300;
            }
            if (customerPlan.ProductCode != null && customerPlan.ProductCode.Contains("VCR"))
            {

                //El 1 representa que se eligió pago especial
                if (this.UCTermInsurance._ddlSpecialPayment.SelectedValue == "1")
                {

                    if (!string.IsNullOrEmpty(this.UCTermInsurance._txtSpecialPayment.Text))
                    {
                        customerPlan.SpecialPayment = Utility.IsDecimalReturnNull(this.UCTermInsurance._txtSpecialPayment.Text);
                    }
                    else
                    {
                        JSTools.MessageBox(this, "Es requerido un pago especial ya que se seleccionó que habría");
                        return;
                    }
                }
                var provider = this.UCTermInsurance._ddlFinancialInstitution.SelectedValue;
                if (provider == null || provider.Split('|') == null || provider.Split('|').Count() < 2)
                {
                    JSTools.MessageBox(this, "Es requerido la institución financiera");
                    return;
                }
                var providerTypeId = 0;
                var providerId = 0;
                int.TryParse(provider.Split('|')[0], out providerTypeId);
                int.TryParse(provider.Split('|')[1], out providerId);
                customerPlan.ProviderTypeId = providerTypeId;
                customerPlan.ProviderId = providerId;
                //Y es si se seleccionó el tipo de contribución en Años.
                //M es si se seleccionó el tipo de contribución en Meses.
                if (this.UCTermInsurance._ddlContributionType.SelectedValue == "Y")
                {
                    var contributionYears = 0;
                    int.TryParse(this.UCTermInsurance._txtContributionPeriod.Text, out contributionYears);
                    if (contributionYears < 1)
                    {
                        JSTools.MessageBox(this, "Es requerido los años  de contribución ya que se seleccionó dicha opción ");
                        throw new Exception("Es requerido los años  de contribución ya que se seleccionó dicha opción ");

                    }
                    customerPlan.ContributionPeriod = contributionYears;
                }
                else if (this.UCTermInsurance._ddlContributionType.SelectedValue == "M")
                {
                    var contributionMonth = 0;
                    int.TryParse(this.UCTermInsurance._txtContributionPeriodMonth.Text, out contributionMonth);

                    if (contributionMonth < 1)
                    {
                        JSTools.MessageBox(this, "Es requerido los meses de contribución  ya que se seleccionó dicha opción ");
                        throw new Exception("Es requerido los años  de contribución ya que se seleccionó dicha opción ");
                    }
                    customerPlan.ContributionPeriodMonth = contributionMonth;
                }
                var financingRate = new Nullable<decimal>();
                financingRate = Utility.IsDecimalReturnNull(this.UCTermInsurance._txtFinancingRate.Text);
                //decimal.TryParse(this.UCTermInsurance._txtFinancingRate.Text, out financingRate);
                if (financingRate <= 0)
                {
                    JSTools.MessageBox(this, "Es requerido la tasa de interes para este producto ");
                    throw new Exception("Es requerido los años  de contribución ya que se seleccionó dicha opción ");

                }
                customerPlan.FinancingRate = financingRate;
                var destinyFund = this.UCTermInsurance._ddlDestinyFund.SelectedValue;
                if (string.IsNullOrEmpty(destinyFund))
                {
                    JSTools.MessageBox(this, "Es requerido el destino de los fondos para este producto ");
                    throw new Exception("Es requerido los años  de contribución ya que se seleccionó dicha opción ");

                }
                customerPlan.DestinyFund = destinyFund;
            }
        }

        private bool SavePlan(bool showAlert = true)
        {
            try
            {
                GetFieldsReference();
                var officeData = GetOffice();

                if (ObjServices.UserType != Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.Agent)
                {
                    FillAgentToSubmit();

                    if (ddlAgentToSubmit.Items.Count <= 1)
                    {
                        JSTools.MessageBox(this, Resources.Office0DontHaveAgents.SFormat(GetOfficeName()));
                        return false;
                    }
                }

                if (ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault() <= 0)
                    UCContainer.save();
                else
                    UCContainer.edit();

                UCRequirementContainer.FillData();

                var isSetIllustrationToGlobal = ObjServices.SetIllustrationToGlobal(
                                                ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault(),
                                                  officeData.CorpId,
                                                  officeData.RegionId,
                                                  officeData.CountryId,
                                                  officeData.DomesticregId,
                                                  officeData.StateProvId,
                                                  officeData.CityId,
                                                  officeData.OfficeId,
                                                  ObjServices.UserID.GetValueOrDefault(),
                                                  ObjServices.CompanyId,
                                                  ObjServices.ProjectId,
                                                  this
                                                 );

                if (isSetIllustrationToGlobal)
                {
                    if (showAlert)
                        JSTools.MessageBox(this, Resources.SaveSucessfully);

                    ObjServices.FillCaseSeqNoFromIllusdata(ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault());
                    return true;
                }
            }
            catch (Exception ex)
            {
                JSTools.MessageBox(this, ex.GetLastInnerException().Message.RemoveInvalidCharacters());
            }

            return false;
        }

        private bool CanSaveIllustration(string illustrationStatus)
        {
            var lstStatusCantSave = new[] { 
                    Utility.IllustrationStatus.DeclinedByClient.Code(), 
                    Utility.IllustrationStatus.ApprovedBySubscription.Code(), 
                    Utility.IllustrationStatus.DeclinedBySubscription.Code(), 
                    Utility.IllustrationStatus.Issued.Code(), 
                    Utility.IllustrationStatus.Submitted.Code(), 
                    Utility.IllustrationStatus.TimeExpired.Code() };
            return !lstStatusCantSave.Contains(illustrationStatus);

        }

        private bool Calculate(bool showAlert = true)
        {
            try
            {
                if (!CanSaveIllustration(ObjIllustrationServices.IllustrationStatusCode))
                {
                    JSTools.MessageBox(this, Resources.CantCalculateThisIllustration);
                    return false;
                }

                if (!SavePlan(false))
                    return false;
                bool? haveSpecialPayment = null;
                //1 Significa que se seleccionó el dropdown que que habra pagos especiales.
                if (this.UCTermInsurance._ddlSpecialPayment.SelectedValue == "1")
                {
                    haveSpecialPayment = true;
                }
                else
                {
                    haveSpecialPayment = false;
                }
                var calculatePlanModel = ObjIllustrationServices.CalculatePlan(ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault(), haveSpecialPayment.GetValueOrDefault());

                var currencyCode = Utility.GetIllusDropDownByType(Utility.DropDownType.Currency, ProductCode).First(o => o.PClass == ddlCurrency.SelectedValue).CurrencyCode;

                txtInsuredBenefitRetirementAmount.Text =
                    (FamilyProductCode == Utility.EFamilyProductType.LifeInsurance.Code() ||
                    FamilyProductCode == Utility.EFamilyProductType.TermInsurance.Code() ||
                    FamilyProductCode == Utility.EFamilyProductType.Funeral.Code() ?
                    calculatePlanModel.InsuredAmount : calculatePlanModel.AnnuityAmount).ToFormatNumeric();

                txtPeriodicPremiumAmount.Text = calculatePlanModel.PeriodicPremium.ToFormatNumeric();

                double tax = 0;
                if (txtFooterTax != null)
                {
                    tax = Math.Round(calculatePlanModel.PeriodicPremium * Utility.GetItbis().ToDouble(), MidpointRounding.AwayFromZero);
                    txtFooterTax.Text = tax.ToFormatCurrency(currencyCode);
                }
                

                //Bmarroquin 10-01-2017 Se agregan mejoras como parte de la tropicalizacion de ESA, se estaban perdiendo decimales al realizar la conversion a Double !!
                //Usando la funcion ToFormatCurrency(currencyCode) se creo el extend method ToStringCurrency

                if (txtFooterPeriodicPremium != null)
                    txtFooterPeriodicPremium.Text = Math.Round(calculatePlanModel.PeriodicPremium).ToFormatCurrency(currencyCode);

                if (txtFooterPeriodicPremiumTotal != null)
                    txtFooterPeriodicPremiumTotal.Text = Math.Round(tax + calculatePlanModel.PeriodicPremium).ToFormatCurrency(currencyCode);

                if (txtFooterTotalInsuredBenefitRetirementAmount != null)
                    txtFooterTotalInsuredBenefitRetirementAmount.Text =
                        Math.Round(FamilyProductCode == Utility.EFamilyProductType.LifeInsurance.Code() ||
                        FamilyProductCode == Utility.EFamilyProductType.TermInsurance.Code() ||
                    FamilyProductCode == Utility.EFamilyProductType.Funeral.Code() ?
                        calculatePlanModel.TotalInsuredAmount : calculatePlanModel.TotalRetirementAmount).ToFormatCurrency(currencyCode);

                if (txtFooterMinimumAnnualPremium != null)
                    txtFooterMinimumAnnualPremium.Text = Math.Round(calculatePlanModel.MinimumPremium).ToFormatCurrency(currencyCode);

                if (txtFooterTargetAnnualPremium != null)
                    txtFooterTargetAnnualPremium.Text = Math.Round(calculatePlanModel.TargetPremium).ToFormatCurrency(currencyCode);

                if (txtFooterAnnualPremium != null)
                    txtFooterAnnualPremium.Text = Math.Round(calculatePlanModel.SumAnnualPremium).ToFormatCurrency(currencyCode);
				//txtFooterAnnualPremium.Text = Math.Round(calculatePlanModel.SumAnnualPremium, 2).ToStringCurrency(); //Math.Round(calculatePlanModel.SumAnnualPremium).ToFormatCurrency(currencyCode);
                //Commit prueba - recibida en actualización - commit de las 10.42 am 17-02-17-Commit a las 10.43 Commit 10.48 am

                if (txtFooterFractionSurcharge != null)
                    txtFooterFractionSurcharge.Text = Math.Round(calculatePlanModel.FractionSurcharge,2).ToStringCurrency();

                //bmarroquin 04-05-2017
                if (txtFooterPrimaAnualNeta != null)
                    txtFooterPrimaAnualNeta.Text = Math.Round(calculatePlanModel.NetAnnualPremium, 2).ToStringCurrency();

                if (txtFooterReturnPremium != null)
                    txtFooterReturnPremium.Text = Math.Round(calculatePlanModel.MinimumPremium).ToFormatCurrency(currencyCode);

                if (txtFooterInsuredPeriod != null)
                    txtFooterInsuredPeriod.Text = txtContributionPeriodUntilAge.Text;

                edit(WEB.NewBusiness.Common.Utility.IllustrationStatus.NewPlan.Code());
                ObjIllustrationServices.IllustrationStatusCode = WEB.NewBusiness.Common.Utility.IllustrationStatus.NewPlan.Code();
                UCRequirementContainer.FillData();
                UCContainer.UCHeaderIllustrationInfo.ChangeStatusLabel(Resources.New_Plan);
                btnCompareIllustration.Enabled =
                btnSubmit.Enabled = ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault() > 0;
                if (showAlert)
                    JSTools.MessageBox(this, Resources.CalculateFinished);
                return true;
            }
            catch (Exception ex)
            {
                JSTools.MessageBox(this, ex.GetLastInnerException().Message.RemoveInvalidCharacters());
            }
            return true;
        }

        private void FillAgentToSubmit()
        {
            CanSaveWithOtherAgent = ObjServices.UserType != Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.Agent &&
                 ddlAgentToSubmit.DataSource == null &&
                  ObjIllustrationServices.IllustrationStatusCode != WEB.NewBusiness.Common.Utility.IllustrationStatus.Submitted.Code();

            if (CanSaveWithOtherAgent)
            {
                int? agentId = ObjServices.UserType != Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.User ? ObjServices.Agent_LoginId
                    : (int?)null;

                var officeData = GetOffice();
                if (officeData != null)
                    ObjServices.GettingAllDrops(ref ddlAgentToSubmit,
                                    Utility.DropDownType.Agent,
                                    "AgentName",
                                    "AgentId",
                                    corpId: officeData.CorpId,
                                    regionId: officeData.RegionId,
                                    countryId: officeData.CountryId,
                                    domesticregId: officeData.DomesticregId,
                                    stateProvId: officeData.StateProvId,
                                    cityId: officeData.CityId,
                                    officeId: officeData.OfficeId,
                                    agentId: agentId,
                                    GenerateItemSelect: true
                                   );
            }
        }

        private Utility.itemOfficceWithoutAgent GetOffice()
        {
            return UCContainer.UCHeaderIllustrationInfo.GetOffice();
        }

        public string GetOfficeName()
        {
            return UCContainer.UCHeaderIllustrationInfo.GetOfficeName();
        }
        #endregion
        #region Public
        public void ChangePlan(string familyProductCode, string productCode, string productName)
        {
            ClearData();
            var productTypeInformation = new
            {
                FamilyProductCode = familyProductCode,
                ProductCode = productCode,
                ProductName = productName
            };
            ProductDetailInformation = Newtonsoft.Json.JsonConvert.SerializeObject(productTypeInformation);
            productName = productName.Replace(" ", String.Empty).Replace("Básico", "");

            View vPlanInformation;
            View vPlanInformationFooter;
            if (familyProductCode == Utility.EFamilyProductType.TermInsurance.Code())
            {
                vPlanInformation = vTermInsurance;
                vPlanInformationFooter = vTermInsuranceFooter;
            }
            else if (familyProductCode == Utility.EFamilyProductType.Funeral.Code())
            {
                vPlanInformation = vFuneral;
                vPlanInformationFooter = vFuneralFooter;
            }
            else
            {
                vPlanInformation = (View)mvPlanInformation.FindControl("v" + productName);
                vPlanInformationFooter = (View)mvPlanInformationFooter.FindControl("v" + productName + "Footer");
            }

            mvPlanInformation.SetActiveView(vPlanInformation);
            mvPlanInformationFooter.SetActiveView(vPlanInformationFooter);
            GetFieldsReference();
            FillDropDown(familyProductCode, productCode);


            var lstPlanWithRider = new[] { "CPI", "LEG" };
            var hasRider = familyProductCode == Utility.EFamilyProductType.TermInsurance.Code() ||
                familyProductCode == Utility.EFamilyProductType.Funeral.Code() ||
                lstPlanWithRider.Contains(productCode);
            UCRiderContainer.Visible = hasRider;
            if (hasRider)
                UCRiderContainer.Initialize();

            UCBeneficiariesContainer.ChangePlan(familyProductCode);
            UCPlanSummary1.FillData(productName: productTypeInformation.ProductName);
            if (productCode.Contains("VCR"))
            {
                this.UCTermInsurance._divFinancialInstitution.Visible = true;
                this.UCTermInsurance._divFinancigRateAndDestiny.Visible = true;
                this.UCTermInsurance._divSpecialPayment.Visible = true;
                this.UCTermInsurance._divPaymentSpecial.Visible = true;
                this.UCTermInsurance._ddlSpecialPayment.SelectedIndexChanged += ddlSpecialPayments_SelectedIndexChanged;
                this.UCTermInsurance._ddlFrequencyPayment.SelectedIndexChanged += ddlFrequencyPayment_SelectedIndexChanged;
                if (!string.IsNullOrEmpty(this.FinancialInstitionSelected))
                {
                    //this.ddlFinancialInstitution.SelectIndexByValue(this.FinancialInstitionSelected);
                }
            }
            else
            {
                this.UCTermInsurance._divFinancialInstitution.Visible = false;
                this.UCTermInsurance._divFinancigRateAndDestiny.Visible = false;
                this.UCTermInsurance._divSpecialPayment.Visible = false;
                this.UCTermInsurance._divPaymentSpecial.Visible = false;
            }
            if (ddlCurrency != null && ddlCurrency.DataSource != null)
            {
                ddlCurrency.SelectedIndex = 1;
                var count = ddlCurrency.Items.Count;
                ddlCurrency.Enabled = !(count <= 2);

            }
        }
        protected void ddlSpecialPayments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ProductCode != null && this.ProductCode.Contains("VCR"))
            {
                this.UCTermInsurance._txtSpecialPayment.Enabled = (sender as DropDownList).SelectedValue == "1";
                if (this.UCTermInsurance._txtSpecialPayment.Enabled)
                {
                    this.UCTermInsurance._txtSpecialPayment.Attributes.Add("validation", "Required");
                    this.UCTermInsurance._txtSpecialPayment.Focus();
                }
                else
                {
                    this.UCTermInsurance._txtSpecialPayment.Clear();
                    this.UCTermInsurance._txtSpecialPayment.Attributes.Remove("validation");
                }
            }
            else
            {
                this.UCTermInsurance._txtSpecialPayment.Enabled = false;
                this.UCTermInsurance._txtSpecialPayment.Clear();
                this.UCTermInsurance._txtSpecialPayment.Attributes.Remove("validation");
            }
        }
        protected void ddlFrequencyPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ProductCode != null && this.ProductCode.Contains("VCR"))
            {
                if ((sender as DropDownList).SelectedValue != "-1")
                {
                    dynamic objPdi = Newtonsoft.Json.JsonConvert.DeserializeObject((sender as DropDownList).SelectedValue);
                    //Si la frecuencia de pago seleccionada fue anual
                    if (objPdi.FrequencyTypeCode != null && objPdi.FrequencyTypeCode == "A" && objPdi.FrequencyValue == "1")
                    {

                        var item = this.UCTermInsurance._ddlContributionType.Items.FindByValue("M");
                        if (item != null)
                        {
                            this.UCTermInsurance._ddlContributionType.Items.Remove(item);
                        }
                    }
                    else
                    {
                        //var item = this.UCTermInsurance._ddlContributionType.Items.FindByValue("Y");
                        //if (item != null)
                        //{
                        //    this.UCTermInsurance._ddlContributionType.Items.Remove(item);
                        //}                        
                        this.ddlContributionType = this.UCTermInsurance._ddlContributionType;
                        var valorSeleccionado = string.Empty;
                        if (this.UCTermInsurance._ddlContributionType.SelectedValue != "-1")
                        {
                            valorSeleccionado = this.ddlContributionType.SelectedValue;
                        }

                        Utility.GettingAllDropsToIllus(ref this.ddlContributionType, Utility.DropDownType.ContributionType, "ContributionType", "ContributionTypeCode", productCode: this.ProductCode, GenerateItemSelect: true, pLang: ObjServices.Language);
                        if (valorSeleccionado != string.Empty)
                        {
                            this.ddlContributionType.SelectIndexByValue(valorSeleccionado);
                        }
                    }
                }
            }
            if (ddlContributionType != null && ddlContributionType.DataSource != null)
            {
                this.ddlContributionType.SelectedIndex = 1;
                var count = ddlContributionType.Items.Count;
                this.ddlContributionType.Enabled = !(count <= 2);
            }
            if (ddlCalculate != null && ddlCalculate.DataSource != null)
            {
                this.ddlCalculate.SelectedIndex = 0;
                var count = ddlCalculate.Items.Count;
                this.ddlCalculate.Enabled = !(count < 2);
            }
        }

        public void Translator(string Lang)
        {
            btnCalculate.Text = Resources.Calculate;
            btnSeeIllustration.Text = Resources.SeeIllustration;
            btnCompareIllustration.Text = Resources.CompareIllustration;
            btnAddIllustration.Text = Resources.AddIllustration;
            btnEmailIllustration.Text = "E-mail " + Resources.Illustration;
            btnSave.Text = Resources.Save;
            btnSaveAs.Text = Resources.SaveAs;
            btnDelete.Text = Resources.DeleteLabel;
            btnSaveWithAgent.Text = Resources.Save + " " + Resources.AGENT;
            ppcAgentToSubmit.HeaderText = Resources.AGENT;
            btnSubmit.Text = Resources.SubmitPolicy;
            if (isChangingLang)
                FillDropDown(FamilyProductCode, ProductCode);
            if (lblFooterTax != null)
                lblFooterTax.Text = "{0} {1}%".SFormat(Resources.Tax, (Utility.GetItbis() * 100).ToFormatNumeric());
        }

        public void save()
        {
            long illustrationNo = ObjIllustrationServices.oIllusDataManager.GetMaxIllustrationNo() + 1;
            //Bmarroquin 25-03-2017 se cambia la Nomenclatura de la cotizacion, que no la genere en base a IllusData sino en base a Global...
            //var dispIllustrationNo = IllustrationNo = ProductCode + "-" + illustrationNo.ToString("000-00000"); //ANTES de Cambio
            var dispIllustrationNo = ObjServices.GetNewCotizacionNumber(ObjServices.Country_Id, ProductCode); //IllustrationNo = ProductCode + "-" + illustrationNo.ToString("000-00000");

            var company = ObjIllustrationServices.oIllusDataManager.GetCompany(ObjServices.CompanyId);

            if (ObjIllustrationServices.IllustrationStatusCode == Utility.IllustrationStatus.Submitted.Code())
                ObjIllustrationServices.IllustrationStatusCode = Utility.IllustrationStatus.NewPlan.Code();
            else if (String.IsNullOrEmpty(ObjIllustrationServices.IllustrationStatusCode))
                ObjIllustrationServices.IllustrationStatusCode = Utility.IllustrationStatus.New.Code();


            var customerPlan = new Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetail
            {
                CustomerNo = ObjIllustrationServices.CustomerNo,
                IllustrationStatusCode = ObjIllustrationServices.IllustrationStatusCode,
                IllustrationNo = illustrationNo,
                DispIllustrationNo = dispIllustrationNo,
                IsDeleted = "N",
                IsoPeningBalance = "N",
                DateCreated = DateTime.Now,
                UserId = ObjIllustrationServices.IllusUserID.GetValueOrDefault(),
                CreatedBy = ObjIllustrationServices.IllusUserID.GetValueOrDefault(),
                IllustrationVerified = "N",
                PlanDate = DateTime.Now,
                InsuredAmount = 0,
                AnnualizedPremium = 0,
                AnnuityAmount = 0,
                ContributionPeriod = 0,
                ContributionUntilAge = 0,
                DefermentPeriod = 0,
                FinancialGoal = "N",
                FinancialGoalAmount = 0,
                FinancialGoalAge = 0,
                InitialCommission = 0,
                InitialContribution = 0,
                BenefitAmount = 0,
                InvestmentProfilePercent = 0,
                MinimumPremium = 0,
                OpeningBalance = 0,
                PremiumAmount = 0,
                ProjectedPremium = 0,
                RetirementPeriod = 0,
                RiderAdb = "N",
                RiderAcdb = "N",
                RiderAdbAmount = 0,
                RiderAdbCost = 0,
                RiderAcdbCost = 0,
                RiderCi = "N",
                RiderCiAmount = 0,
                RiderCiCost = 0,
                RiderOir = "N",
                RiderTerm = "N",
                RiderTermAmount = 0,
                RiderTermCost = 0,
                RiderTermUntilAge = 0,
                TargetPremium = 0,
                TermPeriod = 0,
                StudentName = null,
                StudentAge = null,
                OtherPlans = "N",
                CompanyNo = ObjServices.CompanyId,
                IsPolicyChangesApproved = "N",
                TermContributionTypeCode = "U",
                IsSpecial = "N",
                IsCustomerOwner = "N",
                CountryNo = 300,
                Familiar = "N",
                Repatriacion = "N",
                SepulturaLote = "N",
                PlanTypeCode = "S",
                CompanyId = company.BrandName
            };

            SetCustomerPlan(customerPlan);
            ObjIllustrationServices.CustomerPlanNo = customerPlan.CustomerPlanNo = ObjIllustrationServices.oIllusDataManager.InsertCustomerPlanDetail(customerPlan).CustomerPlanNo;
            UCContainer.UCHeaderIllustrationInfo.ChangeStatusLabel(ObjIllustrationServices.IllustrationStatusCode == Utility.IllustrationStatus.NewPlan.Code() ?
                Resources.New_Plan : Resources.New);
            UCContainer.SetIllustrationNo(dispIllustrationNo);

            UCBeneficiariesContainer.save();
            if (UCRiderContainer.Visible)
                UCRiderContainer.save();
            //UCRequirementContainer.save();
            UCBeneficiariesContainer.FillData();
        }

        public void edit()
        {
            var customerPlan = ObjIllustrationServices.oIllusDataManager.GetAllCustomerPlanDetail(new Illustrator.CustomerPlanDetailP
            {
                CustomerPlanNo = ObjIllustrationServices.CustomerPlanNo.Value
            }).FirstOrDefault();
            SetCustomerPlan(customerPlan);
            ObjIllustrationServices.oIllusDataManager.UpdateCustomerPlanDetail(customerPlan);

            UCBeneficiariesContainer.edit();
            if (UCRiderContainer.Visible)
                UCRiderContainer.edit();
            //UCRequirementContainer.edit();

            UCBeneficiariesContainer.FillData();
        }

        public void edit(string newStatus = null)
        {
            var customerPlan = ObjIllustrationServices.oIllusDataManager.GetAllCustomerPlanDetail(new Illustrator.CustomerPlanDetailP
            {
                CustomerPlanNo = ObjIllustrationServices.CustomerPlanNo.Value
            }).FirstOrDefault();
            SetCustomerPlan(customerPlan);
            customerPlan.IllustrationStatusCode = newStatus;
            if (newStatus == WEB.NewBusiness.Common.Utility.IllustrationStatus.NewPlan.Code())
                customerPlan.IsApproved = "Y";
            ObjIllustrationServices.oIllusDataManager.UpdateCustomerPlanDetail(customerPlan);

            UCBeneficiariesContainer.edit();
            if (UCRiderContainer.Visible)
                UCRiderContainer.edit();
            //UCRequirementContainer.edit();

            UCBeneficiariesContainer.FillData();

            var officeData = GetOffice();

            ObjServices.SetIllustrationToGlobal(ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault(),
            officeData.CorpId,
            officeData.RegionId,
            officeData.CountryId,
            officeData.DomesticregId,
            officeData.StateProvId,
            officeData.CityId,
            officeData.OfficeId,
            ObjServices.UserID.GetValueOrDefault(),
            ObjServices.CompanyId,
            ObjServices.ProjectId,
            this
            );
        }

        public void FillData()
        {
            var ownerName = "";
            var insuredName = "";
            CleanControls(this);
            var canEdit = true;
            ObjIllustrationServices.IllustrationStatusCode = null;
            if (ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault() > 0)
            {
                var customerPlan = ObjIllustrationServices.oIllusDataManager.GetAllCustomerPlanDetail(new Illustrator.CustomerPlanDetailP
                {
                    CustomerPlanNo = ObjIllustrationServices.CustomerPlanNo.Value
                }).FirstOrDefault();
                var customer = ObjIllustrationServices.oIllusDataManager.GetCustomerDetailById(ObjIllustrationServices.CustomerNo, null);
                if (customerPlan == null)
                {
                    CleanControls(this);
                    return;
                }
                //VCR es cuando el plan es de tipo VIDA CREDITO
                if (customerPlan.ProductCode.Contains("VCR"))
                {
                    //La M es para ver si fue seleccionado Cantidad En Meses
                    if (customerPlan.ContributionTypeCode.Contains("M"))
                        this.UCTermInsurance._txtContributionPeriodMonth.Text = customerPlan.ContributionPeriodMonth.ToString();
                    this.UCTermInsurance._ddlDestinyFund.SelectIndexByValue(customerPlan.DestinyFund);
                    if (customerPlan.ProviderTypeId.HasValue && customerPlan.ProviderId.HasValue && customerPlan.ProviderTypeId.Value > 0 && customerPlan.ProviderId.Value > 0)
                    {
                        this.FinancialInstitionSelected = customerPlan.ProviderTypeId + "|" + customerPlan.ProviderId;
                    }
                    else
                    {
                        this.FinancialInstitionSelected = string.Empty;
                    }

                    if (customerPlan.SpecialPayment.HasValue && customerPlan.SpecialPayment.Value > 0)
                    {
                        this.UCTermInsurance._txtSpecialPayment.Text = customerPlan.SpecialPayment.Value.ToFormatNumeric();
                    }

                    this.UCTermInsurance._txtFinancingRate.Text = customerPlan.FinancingRate.Value.ToFormatNumeric();
                }
                else
                {
                    this.FinancialInstitionSelected = string.Empty;
                }
                ObjIllustrationServices.IllustrationStatusCode = customerPlan.IllustrationStatusCode;

                canEdit = CanSaveIllustration(customerPlan.IllustrationStatusCode);

                ObjServices.FillCaseSeqNoFromIllusdata(ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault());

                IllustrationNo = customerPlan.DispIllustrationNo;

                if (customerPlan.OwnerCustomerNo.HasValue)
                {
                    var ownerCustomer = ObjIllustrationServices.oIllusDataManager.GetCustomerDetailById(customerPlan.OwnerCustomerNo, null);
                    ownerName = "{0} {1}".SFormat(ownerCustomer.FirstName.NTrim(), ownerCustomer.LastName.NTrim());
                    insuredName = "{0} {1}".SFormat(customer.FirstName.NTrim(), customer.LastName.NTrim());
                }
                else
                {
                    ownerName = "{0} {1}".SFormat(customer.FirstName.NTrim(), customer.LastName.NTrim());
                    insuredName = "{0} {1}".SFormat(customer.FirstName.NTrim(), customer.LastName.NTrim());
                }

                ChangePlan(customerPlan.PlanGroupCode, customerPlan.ProductCode, customerPlan.Product);
                hdnOwnerId.Value = customerPlan.OwnerCustomerNo.ToString();
                ddlRisk.SelectedValue = customerPlan.ActivityRiskTypeNo.ToString();
                ddlPerThousand.SelectedValue = customerPlan.HealthRiskTypeNo.ToString();
                if (txtStudentName != null)
                    txtStudentName.Text = customerPlan.StudentName;
                if (txtStudentAge != null)
                    txtStudentAge.Text = customerPlan.StudentAge.ToString();
                ddlCurrency.SelectedValue = customerPlan.PClass;

                var currencyCode = Utility.GetIllusDropDownByType(Utility.DropDownType.Currency, ProductCode).First(o => o.PClass == ddlCurrency.SelectedValue).CurrencyCode;

                if (ddlFrequencyPayment != null)
                {
                    var frequency = Newtonsoft.Json.JsonConvert.SerializeObject(new { customerPlan.FrequencyTypeCode, FrequencyValue = customerPlan.Frequency.ToString() });
                    ddlFrequencyPayment.SelectIndexByValueJSON(frequency);
                }

                if (ddlPlanType != null)
                    ddlPlanType.SelectedValue = customerPlan.PlanTypeCode;
                chkOtherPlanWithSTL.Checked = customerPlan.OtherPlans == "Y";
                if (ddlCalculate != null)
                    ddlCalculate.SelectedValue = customerPlan.CalculateTypeCode;
                if (ddlContributionType != null)
                    ddlContributionType.SelectedValue = customerPlan.ContributionTypeCode;
                if (ddlInvestmentProfile != null)
                    ddlInvestmentProfile.SelectedValue = customerPlan.InvestmentProfileCode;

                //Bmarroquin 09-01-2017 Se agrega como parte de la tropicalizacion de ESA
                if (ddlContributionPeriodYear != null)
                    ddlContributionPeriodYear.SelectedValue = customerPlan.ContributionPeriod.ToString();

                //Lgonzalez 16-02-2017
                txtFechaVigenciaPlan.Text = customerPlan.PlanEffectiveDate.ToString();

                
                if (txtContributionPeriodUntilAge != null)
                    txtContributionPeriodUntilAge.Text = (customerPlan.ContributionUntilAge == 0 ? customerPlan.ContributionPeriod : customerPlan.ContributionUntilAge).ToString();
                else if (ddlContributionPeriod != null)
                    ddlContributionPeriod.SelectedValue = customerPlan.ContributionPeriod.ToString();

                if (txtFooterInsuredPeriod != null)
                    txtFooterInsuredPeriod.Text = customerPlan.ContributionPeriod.ToString();

                if (ddlFinancialGoal != null)
                    ddlFinancialGoal.SelectedValue = customerPlan.FinancialGoal == "Y" ? "1" : "0";
                if (txtAtAge != null)
                    txtAtAge.Text = customerPlan.FinancialGoalAge.ToString();
                if (txtAmountGoal != null)
                    txtAmountGoal.Text = customerPlan.FinancialGoalAmount.ToFormatNumeric();
                ddlInitialContribution.SelectedValue = customerPlan.InitialContribution > 0 ? "1" : "0";
                txtInitialContributionAmount.Text = customerPlan.InitialContribution.ToFormatNumeric();

                decimal tax = 0;
                if (txtFooterTax != null)
                {
                    tax = Math.Round(customerPlan.PremiumAmount * Utility.GetItbis(), MidpointRounding.AwayFromZero);
                    txtFooterTax.Text = tax.ToFormatCurrency(currencyCode);
                }

                txtPeriodicPremiumAmount.Text = customerPlan.PremiumAmount.ToFormatNumeric();

                txtFooterTargetAnnualPremium.Text = customerPlan.TargetPremium.ToFormatCurrency(currencyCode);

                if (txtFooterPeriodicPremium != null)
                    txtFooterPeriodicPremium.Text = Math.Round(customerPlan.PremiumAmount).ToFormatCurrency(currencyCode);

                if (txtFooterPeriodicPremiumTotal != null)
                    txtFooterPeriodicPremiumTotal.Text = Math.Round(tax + customerPlan.PremiumAmount).ToFormatCurrency(currencyCode);

                if (txtFooterTotalInsuredBenefitRetirementAmount != null)
                    txtFooterTotalInsuredBenefitRetirementAmount.Text =
                      Math.Round(
                      FamilyProductCode == Utility.EFamilyProductType.Education.Code() || FamilyProductCode == Utility.EFamilyProductType.Retirement.Code() ?
                      customerPlan.AnnuityAmount * customerPlan.RetirementPeriod :
                      customerPlan.BenefitAmount.GetValueOrDefault() + customerPlan.RiderTermAmount).ToFormatCurrency(currencyCode);

                if (txtFooterMinimumAnnualPremium != null)
                    txtFooterMinimumAnnualPremium.Text = Math.Round(customerPlan.MinimumPremium).ToFormatCurrency(currencyCode);

                if (txtFooterAnnualPremium != null)
                    txtFooterAnnualPremium.Text = Math.Round(customerPlan.AnnualizedPremium).ToFormatCurrency(currencyCode);
                ddlCurrency.SelectedValue = customerPlan.PClass;

                if (txtInsuredBenefitRetirementAmount != null)
                    if (FamilyProductCode == Utility.EFamilyProductType.Education.Code() || FamilyProductCode == Utility.EFamilyProductType.Retirement.Code())
                        txtInsuredBenefitRetirementAmount.Text = customerPlan.AnnuityAmount.ToFormatNumeric();
                    else
                        txtInsuredBenefitRetirementAmount.Text = customerPlan.BenefitAmount.ToFormatNumeric();

                if (ddlEducationRetirementPeriod != null)
                    ddlEducationRetirementPeriod.SelectedValue = customerPlan.RetirementPeriod.ToString();
                if (ddlDefermentPeriod != null)
                    ddlDefermentPeriod.SelectedValue = customerPlan.DefermentPeriod.ToString();

                if (txtFooterReturnPremium != null)
                    txtFooterReturnPremium.Text = Math.Round(customerPlan.PremiumAmount * customerPlan.Frequency.GetValueOrDefault() * customerPlan.ContributionPeriod).ToFormatCurrency(currencyCode);
				
				if (txtFooterFractionSurcharge != null)     //Lgonzalez 29-04-2017
                    txtFooterFractionSurcharge.Text = customerPlan.FractionSurcharge.ToFormatCurrency(currencyCode);

                if (txtFooterPrimaAnualNeta != null)   //bmarroquin 04-05-2017
                    txtFooterPrimaAnualNeta.Text = customerPlan.NetAnnualPremium.ToFormatCurrency(currencyCode);

                var lstPlanWithRider = new[] { "CPI", "LEG" };
                var hasRider = FamilyProductCode == Utility.EFamilyProductType.TermInsurance.Code() ||
                    FamilyProductCode == Utility.EFamilyProductType.Funeral.Code() ||
                    lstPlanWithRider.Contains(ProductCode);
                if (hasRider)
                    UCRiderContainer.FillData();
            }
            else
                UCContainer.UCHeaderIllustrationInfo.ChangeFamilyProduct();

            EnabledControls(this.Controls, canEdit);
            EnablePrincipalControls();

            UCBeneficiariesContainer.FillData();
            UCRequirementContainer.FillData();
            FillAgentToSubmit();

            btnCompareIllustration.Enabled =
            btnSubmit.Enabled = ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault() > 0 && canEdit;
        }

        public void Initialize()
        {
            UCBeneficiariesContainer.Initialize();
            UCRequirementContainer.Initialize();
        }

        public void ClearData()
        {

        }

        public WEB.NewBusiness.Common.Utility.EPlanType GetPlanType()
        {
            if (ddlPlanType == null)
                return Utility.EPlanType.None;

            switch (ddlPlanType.SelectedValue)
            {
                case "F":
                    return Utility.EPlanType.Level;
                case "I":
                    return Utility.EPlanType.Incremental;
                case "N":
                    return Utility.EPlanType.NonInsured;
                case "S":
                default:
                    return Utility.EPlanType.Level;
            }
        }

        public bool HasOtherInsured()
        {
            return UCRiderContainer.Visible && UCRiderContainer.HasOtherInsured();
        }

        public void ReadOnlyControls(bool isReadOnly)
        {

        }

        public void EnablePrincipalControls()
        {
            btnSaveWithAgent.Enabled =
            ddlAgentToSubmit.Enabled =
            btnAddIllustration.Enabled =
            btnSeeIllustration.Enabled =
            btnCompareIllustration.Enabled =
            btnSaveAs.Enabled =
            btnEform.Enabled = true;
        }
        #endregion
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

            this.UCTermInsurance._ddlFrequencyPayment.SelectedIndexChanged += ddlFrequencyPayment_SelectedIndexChanged;
            this.UCTermInsurance._ddlSpecialPayment.SelectedIndexChanged += ddlSpecialPayments_SelectedIndexChanged;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (UCPlanSummary1 != null && ddlRisk != null)
                UCPlanSummary1.FillData(
                           UCContainer.UCHeaderIllustrationInfo.IllustrationNumber,
                           ProductName,
                           ddlCurrency != null ? ddlCurrency.SelectedItem.Text : null,
                           UCContainer.UCHeaderIllustrationInfo.InsuredName,
                           UCContainer.UCHeaderIllustrationInfo.OwnerName,
                           txtStudentName != null ? txtStudentName.Text : null,
                           (txtStudentAge != null ? txtStudentAge.ToInt() : 0).ToString(),
                           txtInitialContributionAmount.Text.ToFormatCurrency(),
                           Resources.ResourceManager.GetString(ddlFrequencyPayment.SelectedItem.Text) ?? ddlFrequencyPayment.SelectedItem.Text,
                           ddlInvestmentProfile != null ? ddlInvestmentProfile.SelectedItem.Text : null,
                           ddlRisk.SelectedItem == null ? "" : Resources.ResourceManager.GetString(ddlRisk.SelectedItem.Text) ?? ddlRisk.SelectedItem.Text,
                           ddlPerThousand.SelectedItem == null ? "" : Resources.ResourceManager.GetString(ddlPerThousand.SelectedItem.Text) ?? ddlPerThousand.SelectedItem.Text,
                           UCRiderContainer.GetAccidentalDeath(),
                           UCRiderContainer.GetAdditionalTerm(),
                           UCRiderContainer.GetOtherInsured(),
                           UCRiderContainer.GetCriticalIllness(),
						   /*Adicionado el 3/02/2017 Merlyn Avelar*/
                           UCRiderContainer.GetGastosFunerarios()
                           );
            Translator(null);
            ddlSpecialPayments_SelectedIndexChanged(this.UCTermInsurance._ddlSpecialPayment, null);
            ddlFrequencyPayment_SelectedIndexChanged(this.UCTermInsurance._ddlFrequencyPayment, null);

        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        protected void btnSeeIllustration_Click(object sender, EventArgs e)
        {
            try
            {
                if (CanSaveIllustration(ObjIllustrationServices.IllustrationStatusCode))
                    if (!Calculate(false)) return;
                var reportArray = ObjIllustrationServices.SeeIllustration(ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault());
                UCContainer.FillDataPreview(reportArray, IllustrationNo + ProductName);
                UCContainer.SetMultiView(UCIllustrationContainer.MultiViewIllustrator.Preview);
            }
            catch (Exception ex)
            {
                JSTools.MessageBox(this, ex.GetLastInnerException().Message);
            }
        }

        protected void btnCompareIllustration_Click(object sender, EventArgs e)
        {
            UCContainer.SetMultiView(UCIllustrationContainer.MultiViewIllustrator.Compare);
        }

        protected void btnAddIllustration_Click(object sender, EventArgs e)
        {
            ObjIllustrationServices.CustomerPlanNo = null;
            ObjServices.Case_Seq_No =
            ObjServices.Hist_Seq_No = -1;
            UCContainer.Initialize();
            UCContainer.FillData();
        }

        protected void btnEmailIllustration_Click(object sender, EventArgs e)
        {
            if (ObjIllustrationServices.IllustrationStatusCode == WEB.NewBusiness.Common.Utility.IllustrationStatus.New.Code())
            {
                JSTools.MessageBox(this, Resources.IllustrationIsntComplete);
                return;
            }

            var to = ((WEB.NewBusiness.NewBusiness.Pages.Contact)this.Page).Usuario.Email;
            if (String.IsNullOrEmpty(to))
            {
                JSTools.MessageBox(this, Resources.UserDontHaveEmail);
                return;
            }

            var from = System.Configuration.ConfigurationManager.AppSettings["EmailFrom"];

            var reportArray = ObjIllustrationServices.SeeIllustration(ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault());
            var path = Server.MapPath("~/TempFiles/");
            var reportName = IllustrationNo + ProductName + ".pdf";
            var reportPath = path + reportName;
            File.WriteAllBytes(reportPath, reportArray);

            var t = new Thread(delegate()
            {
                MailManager.SendMessage(to, null, null, null, null, from, "Illustration: " + IllustrationNo + ProductName,
                    new List<AttachedFiles>
                {
                    new AttachedFiles{
                    FileName = reportName,
                    FilePath = reportPath
                    }
                }, true);
            });

            JSTools.MessageBox(this, Resources.EmailSended);
            t.Start();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SavePlan();
        }

        protected void btnSaveAs_Click(object sender, EventArgs e)
        {
            ObjIllustrationServices.CustomerPlanNo = null;
            SavePlan();
            FillAgentToSubmit();
            EnabledControls(this.Controls, true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var customerPlanNo = ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault();

                var agent_Id =
                    ObjServices.UserType != Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.Agent ?
                    Utility.deserializeJSON<Utility.itemOfficce>(ddlAgentToSubmit.SelectedValue).AgentId : ObjServices.Agent_Id.GetValueOrDefault();

                var officeData = GetOffice();

                if (ObjServices.SubmitIllustrationToGlobal(customerPlanNo,
                     officeData.CorpId,
                     officeData.RegionId,
                     officeData.CountryId,
                     officeData.DomesticregId,
                     officeData.StateProvId,
                     officeData.CityId,
                     officeData.OfficeId,
                     ObjServices.Case_Seq_No,
                     ObjServices.Hist_Seq_No,
                     agent_Id,
                     ObjServices.ContactEntityID.GetValueOrDefault(),
                     ObjServices.UserID.GetValueOrDefault()
                     ))
                {
                    ObjIllustrationServices.IllustrationStatusCode = WEB.NewBusiness.Common.Utility.IllustrationStatus.Submitted.Code();
                    EnabledControls(this.Controls, false);
                    EnablePrincipalControls();
                    var globalPolicy = ObjIllustrationServices.oIllusDataManager.GetCustomerPlanDetGlobalPolicy(new Illustrator.CustomerPlanDetGlobalPolicy
                    {
                        CustomerPlanNo = customerPlanNo
                    });

                    ObjServices.Case_Seq_No = globalPolicy.CaseSeqNo;
                    ObjServices.Hist_Seq_No = globalPolicy.HistSeqNo;

                    UCContainer.UCHeaderIllustrationInfo.ChangeStatusLabel(Resources.Submitted);
                    CanSaveWithOtherAgent = false;
                    JSTools.MessageBox(this, Resources.SubmittedPolicyFinished);
                }
            }
            catch (Exception ex)
            {
                JSTools.MessageBox(this, ex.GetLastInnerException().Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var customerPlanNo = ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault();
            var officeData = GetOffice();

            ObjServices.DeleteIllustration(
                customerPlanNo,
                     officeData.CorpId,
                     officeData.RegionId,
                     officeData.CountryId,
                     officeData.DomesticregId,
                     officeData.StateProvId,
                     officeData.CityId,
                     officeData.OfficeId,
                     ObjServices.Case_Seq_No,
                     ObjServices.Hist_Seq_No,
                     ObjServices.UserID.GetValueOrDefault()
                );

            ObjServices.Case_Seq_No =
            ObjServices.Hist_Seq_No = -1;
            ObjIllustrationServices.CustomerPlanNo = null;

            UCContainer.Initialize();
            UCContainer.FillData();
        }

        protected void btnEform_Click(object sender, EventArgs e)
        {
            if (ObjIllustrationServices.IllustrationStatusCode == null)
            {
                JSTools.MessageBox(this, Resources.IllustrationIsntComplete);
                return;
            }

            UCContainer.SetMultiView(UCIllustrationContainer.MultiViewIllustrator.Eform);
        }
        #endregion
    }
}