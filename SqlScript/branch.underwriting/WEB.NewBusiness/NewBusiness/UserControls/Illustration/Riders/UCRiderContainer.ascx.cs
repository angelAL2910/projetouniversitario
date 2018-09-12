﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.UnderWriting.IllusData;
using WEB.NewBusiness.Common;
using WEB.NewBusiness.NewBusiness.UserControls.Illustration.PlanInformation;

namespace WEB.NewBusiness.NewBusiness.UserControls.Illustration.Riders
{
    public partial class UCRiderContainer : UC, IUC
    {
        #region Private Properties
        private UCPlanContainer UCIllustrationPlanContainer
        {
            get
            {
                return (UCPlanContainer)Page.Controls[0].FindControl("bodyContent").FindControl("UCIllustrationContainer").FindControl("UCPlanContainer");
            }
        }
        #endregion
        #region Methods
        #region Private
        private void Save()
        {
            var customerPlan = ObjIllustrationServices.oIllusDataManager.GetAllCustomerPlanDetail(new Illustrator.CustomerPlanDetailP
            {
                CustomerPlanNo = ObjIllustrationServices.CustomerPlanNo.Value
            }).First();

            var hasAdb = ddlAccidentalDeathBenefit.SelectedValue == "1";
            customerPlan.RiderAdb = hasAdb ? "Y" : "N";
            customerPlan.RiderAdbAmount = hasAdb ? txtAccidentalDeathBenefitInsuredAmount.Text.ToDecimal() : 0;

            var hasAdditionalTI = ddlAdditionalTermInsurance.SelectedValue == "1";
            customerPlan.RiderTerm = hasAdditionalTI ? "Y" : "N";
            customerPlan.RiderTermAmount = hasAdditionalTI ? txtAdditionalTermInsuranceInsuredAmount.Text.ToDecimal() : 0;
            if (hasAdditionalTI)
            {
                customerPlan.TermContributionTypeCode = ddlAdditionalTermInsuranceUntilAge.SelectedValue;
                if (customerPlan.TermContributionTypeCode == Utility.EContributionType.Continuous.Code())
                    customerPlan.RiderTermUntilAge = 0;
                else
                    customerPlan.RiderTermUntilAge = txtAdditionalTermInsuranceYears.ToInt();
            }
            else
            {
                customerPlan.TermContributionTypeCode = Utility.EContributionType.UntilAge.Code();
                customerPlan.RiderTermUntilAge = 0;
            }
            
            customerPlan.TermContributionTypeCode = Utility.EContributionType.UntilAge.Code();
            customerPlan.RiderTermUntilAge = 0;

            var hasCi = ddlCriticalIllness.SelectedValue == "1";
            customerPlan.RiderCi = hasCi ? "Y" : "N";
            customerPlan.RiderCiAmount = hasCi ? txtCriticalIllnessInsuredAmount.Text.ToDecimal() : 0;

            //Bmarroquin 09-01-2017 Mejoras incorporadas a raiz de tropicalizacion del El Salvador
            var hasGF = ddlGastosFunerarios.SelectedValue == "1";
            customerPlan.RiderTerm = hasGF ? "Y" : "N";
            customerPlan.RiderTermAmount = hasGF ? txtGastosFunerariosInsuredAmount.Text.ToDecimal() : 0;

            var hasOtherInsured = ddlSpouseInsured.SelectedValue == "1";
            customerPlan.RiderOir = hasOtherInsured ? "Y" : "N";

            customerPlan.Familiar = ddlFamiliar.SelectedValue == "1" ? "Y" : "N";
            customerPlan.Repatriacion = ddlRepatriation.SelectedValue == "1" ? "Y" : "N";
            ObjIllustrationServices.oIllusDataManager.UpdateCustomerPlanDetail(customerPlan);

            if (hasOtherInsured)
            {
                var customerPlanOI = ObjIllustrationServices.oIllusDataManager.GetCustomerPlanPartnerInsurance(ObjIllustrationServices.CustomerPlanNo.Value);
                if (customerPlanOI == null)
                    customerPlanOI = new Illustrator.CustomerPlanPartnerInsurance
                    {
                        ContributionTypeCode = Utility.EContributionType.UntilAge.Code(),
                        CustomerPlanNo = ObjIllustrationServices.CustomerPlanNo.Value,
                        DateCreated = DateTime.Now,
                        CreatedBy = ObjIllustrationServices.IllusUserID,
                        OtherPlans = "N"
                    };

                customerPlanOI.DateUpdated = DateTime.Now;
                customerPlanOI.UpdatedBy = ObjIllustrationServices.IllusUserID;
                customerPlanOI.FirstName = txtName.Text;
                customerPlanOI.MiddleName = txtMiddleName.Text;
                customerPlanOI.LastName = txtLastName.Text;
                customerPlanOI.LastName2 = txtSecondLastName.Text;
                customerPlanOI.InsuredAmount = customerPlanOI.RideroirAmount = txtSpouseInsuredInsuredAmount.Text.ToDecimal();
                customerPlanOI.ContributionTypeCode = ddlSpouseInsuredUntilAge.SelectedValue;
                if (customerPlanOI.ContributionTypeCode == Utility.EContributionType.Continuous.Code())
                    customerPlanOI.UntilAge = 0;
                else
                    customerPlanOI.UntilAge = txtSpouseInsuredYears.ToInt();
                customerPlanOI.Smoker = ddlSmoker.SelectedValue == "1" ? "Y" : "N";
                customerPlanOI.Age = hdnAge.Value.IsIntReturnNull();
                customerPlanOI.BirthDate = txtOtherInsuredDateOfBirth.Text.IsDateReturnNull();
                customerPlanOI.GenderCode = ddlGender.SelectedValue;
                customerPlanOI.MaritalStatusCode = ddlMaritalStatus.SelectedValue;
                customerPlanOI.ActivityRiskTypeNo = ddlRisk.SelectedValue.ToInt();
                customerPlanOI.HealthRiskTypeNo = ddlPerThousand.SelectedValue.ToInt();
                customerPlanOI.RelationshipTypeCode = ddlRelationship.SelectedIndex == 0 ? null : ddlRelationship.SelectedValue;
                ObjIllustrationServices.oIllusDataManager.SetCustomerPlanPartnerInsurance(customerPlanOI);
            }
            else
                ObjIllustrationServices.oIllusDataManager.DeleteCustomerPlanPartnerInsurance(ObjIllustrationServices.CustomerPlanNo.Value);
        }

        private void FillDropDown()
        {
            //Bmarroquin 09-01-2017 Se agrega el DropDownList de Gastos Funerarios
            Utility.GettingAllDropsToIllus(ref ddlGastosFunerarios, Utility.DropDownType.Boolean, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlAccidentalDeathBenefit, Utility.DropDownType.Boolean, pLang:ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlSpouseInsured, Utility.DropDownType.Boolean, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlCriticalIllness, Utility.DropDownType.Boolean, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlSmoker, Utility.DropDownType.Boolean, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlAdditionalTermInsurance, Utility.DropDownType.Boolean, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlSpouseInsuredUntilAge, Utility.DropDownType.ContributionType, "ContributionType", "ContributionTypeCode", productCode: "LEG", pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlAdditionalTermInsuranceUntilAge, Utility.DropDownType.ContributionType, "ContributionType", "ContributionTypeCode", productCode: "LEG", pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlGender, Utility.DropDownType.Gender, "GenderName", "GenderCode", GenerateItemSelect: true, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlMaritalStatus, Utility.DropDownType.MaritalStatus, "MaritalStatus", "MaritalStatusCode", pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlRelationship, Utility.DropDownType.Relationship, "RelationshipType", "RelationshipTypeCode", GenerateItemSelect: true, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlRisk,
                Utility.DropDownType.ActivityRiskType, "ActivityRiskType", "ActivityRiskTypeNo", productCode: UCIllustrationPlanContainer.ProductCode, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlPerThousand,
                Utility.DropDownType.HealthRiskType, "HealthRiskType", "HealthRiskTypeNo", productCode: UCIllustrationPlanContainer.ProductCode, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlFamiliar, Utility.DropDownType.Boolean, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlRepatriation, Utility.DropDownType.Boolean, pLang: ObjServices.Language);
        }
        #endregion
        #region Public
        public void Translator(string Lang)
        {
            if (isChangingLang)
                FillDropDown();
        }

        public void save()
        {
            Save();
        }

        public void edit()
        {
            Save();
        }

        public void FillData()
        {
            this.ClearControls();
            var customerPlan = ObjIllustrationServices.oIllusDataManager.GetAllCustomerPlanDetail(new Illustrator.CustomerPlanDetailP
            {
                CustomerPlanNo = ObjIllustrationServices.CustomerPlanNo.Value
            }).FirstOrDefault();

            ddlAccidentalDeathBenefit.SelectIndexByValue(customerPlan.RiderAdb == "Y" ? "1" : "0");
            ddlFamiliar.SelectIndexByValue(customerPlan.Familiar == "Y" ? "1" : "0");
            ddlRepatriation.SelectIndexByValue(customerPlan.Repatriacion == "Y" ? "1" : "0");
            txtAccidentalDeathBenefitInsuredAmount.Text = customerPlan.RiderAdbAmount.ToFormatNumeric(); //customerPlan.RiderAdbAmount.ToString(); //Lgonzalez 16-02-2017

            ddlAdditionalTermInsurance.SelectIndexByValue(customerPlan.RiderTerm == "Y" ? "1" : "0");
            txtAdditionalTermInsuranceInsuredAmount.Text = customerPlan.RiderTermAmount.ToString();
            ddlAdditionalTermInsuranceUntilAge.SelectIndexByValue(customerPlan.TermContributionTypeCode);
            txtAdditionalTermInsuranceYears.Text = customerPlan.RiderTermUntilAge.ToString();
            ddlCriticalIllness.SelectIndexByValue(customerPlan.RiderCi == "Y" ? "1" : "0");
            //txtCriticalIllnessInsuredAmount.Text = customerPlan.RiderCiAmount.ToString();
			txtCriticalIllnessInsuredAmount.Text = customerPlan.RiderCiAmount.ToFormatNumeric(); // customerPlan.RiderCiAmount.ToString(); //Lgonzalez 16-02-2017
            ddlSpouseInsured.SelectIndexByValue(customerPlan.RiderOir == "Y" ? "1" : "0");
             
            //Bmarroquin 09-01-2017 -Internamente la CB gastos funerarios se guarda como Rider Term
            ddlGastosFunerarios.SelectIndexByValue(customerPlan.RiderTerm == "Y" ? "1" : "0");
            txtGastosFunerariosInsuredAmount.Text = customerPlan.RiderTermAmount.ToFormatNumeric(); // customerPlan.RiderTermAmount.ToString(); //Lgonzalez 16-02-2017

            if (customerPlan.RiderOir == "Y")
            {
                var CustomerPlanOI = ObjIllustrationServices.oIllusDataManager.GetCustomerPlanPartnerInsurance(ObjIllustrationServices.CustomerPlanNo.Value);
                if (CustomerPlanOI != null)
                {
                    txtName.Text = CustomerPlanOI.FirstName;
                    txtMiddleName.Text = CustomerPlanOI.MiddleName;
                    txtLastName.Text = CustomerPlanOI.LastName;
                    txtSecondLastName.Text = CustomerPlanOI.LastName2;
                    txtSpouseInsuredInsuredAmount.Text = CustomerPlanOI.InsuredAmount.ToString();
                    ddlSpouseInsuredUntilAge.SelectIndexByValue(CustomerPlanOI.ContributionTypeCode);
                    txtSpouseInsuredYears.Text = CustomerPlanOI.UntilAge.ToString();
                    ddlSmoker.SelectIndexByValue(CustomerPlanOI.Smoker == "Y" ? "1" : "0");
                    hdnAge.Value = txtAge.Text = CustomerPlanOI.Age.ToString();
                    txtOtherInsuredDateOfBirth.Text = CustomerPlanOI.BirthDate.HasValue ? CustomerPlanOI.BirthDate.Value.ToShortDateString() : "";
                    ddlGender.SelectIndexByValue(CustomerPlanOI.GenderCode);
                    ddlMaritalStatus.SelectIndexByValue(CustomerPlanOI.MaritalStatusCode);
                    ddlRisk.SelectIndexByValue(CustomerPlanOI.ActivityRiskTypeNo.ToString());
                    ddlPerThousand.SelectIndexByValue(CustomerPlanOI.HealthRiskTypeNo.ToString());
                    ddlRelationship.SelectIndexByValue(CustomerPlanOI.RelationshipTypeCode);
                }
            }
        }

        public void Initialize()
        {
            FillDropDown();
            var isFuneral = UCIllustrationPlanContainer.FamilyProductCode == Utility.EFamilyProductType.Funeral.Code();
            divOtherInsuredInfo.Visible = pnlRiderMainInsured.Visible = !isFuneral;
            pnlRiderFuneral.Visible = isFuneral;
        }

        public void ClearData()
        {

        }

        public bool HasOtherInsured()
        {
            return ddlSpouseInsured.SelectedValue == "1";
        }

        public string GetCriticalIllness() {
            return ddlCriticalIllness.SelectedValue == "1" ? txtCriticalIllnessInsuredAmount.Text.ToFormatCurrency() : "";
        }

        public string GetAccidentalDeath()
        {
            return ddlAccidentalDeathBenefit.SelectedValue == "1" ? txtAccidentalDeathBenefitInsuredAmount.Text.ToFormatCurrency() : "";
        }

        public string GetOtherInsured()
        {
            return ddlSpouseInsured.SelectedValue == "1" ? txtSpouseInsuredInsuredAmount.Text.ToFormatCurrency() : "";
        }

        public string GetAdditionalTerm()
        {
            return ddlAdditionalTermInsurance.SelectedValue == "1" ? txtAdditionalTermInsuranceInsuredAmount.Text.ToFormatCurrency() : "";
        }



        /*Adicionado el 3/02/2017 Merlyn Avelar*/
        public string GetGastosFunerarios()
        {
            
            return ddlGastosFunerarios.SelectedValue == "1" ? txtGastosFunerariosInsuredAmount.Text.ToFormatCurrency() : "";
        }

        #endregion
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Translator(null);
        }
        #endregion
        public void ReadOnlyControls(bool isReadOnly)
        {
            throw new NotImplementedException();
        }
    }
}