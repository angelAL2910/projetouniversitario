﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using DI.UnderWriting;
using DI.UnderWriting.IllusData.Interfaces;
using Entity.UnderWriting.IllusData;
using Microsoft.Reporting.WebForms;
using RESOURCE.UnderWriting.NewBussiness;
using WEB.NewBusiness.Common.Illustration.BusinessLogic;
using WEB.NewBusiness.IllusdataWebServices;
using Microsoft.VisualBasic;
using System.Data.Linq.SqlClient;

namespace WEB.NewBusiness.Common.Illustration
{
    /// <summary>
    /// Author       : Lic. Carlos Ml. Lebron
    /// Created Date : 10/05/2014
    /// </summary>
    public class IllustrationService
    {
        private string key = "SessionData";
        public Common.SessionList datos;

        private static UnderWritingDIManager idManager = new UnderWritingDIManager();
        private ServicesApi.ContactService.ContactServiceClient _oContactServicesClient;
        private System.Text.StringBuilder sb = new System.Text.StringBuilder();

        public IllustrationService(string KeyName = "SessionData")
        {
            key = KeyName;

            if (HttpContext.Current.Session == null)
            {

                HttpContext.Current.Session.Add(key, new SessionList(KeyName));
                (HttpContext.Current.Session[key] as SessionList).ContactInfo = new SessionContact();
                (HttpContext.Current.Session[key] as SessionList).IllustrationInfo = new SessionIllustration();
            }
            else
            {
                if (HttpContext.Current.Session[key] == null)
                {
                    HttpContext.Current.Session.Add(key, new SessionList(KeyName));
                    (HttpContext.Current.Session[key] as SessionList).ContactInfo = new SessionContact();
                    (HttpContext.Current.Session[key] as SessionList).IllustrationInfo = new SessionIllustration();
                }
            }
            datos = (HttpContext.Current.Session[key] as SessionList);
        }

        #region Illustration Service
        #region Illustration Fields
        #region Private
        ReportViewer _reportViewer = new ReportViewer();
        private string _prospect = "";
        private byte[] _reportBinary = null;
        #region Illustration Configurations Properties
        private string _reportPath;
        private string _reportSignaturePath;
        private string _numberPageIllus;
        private string _checkedSettings;
        private string _unCheckedSettings;
        #endregion
        #endregion
        #region Public
        public RulesService RulesService { get; set; }
        public bool ProductIsFixed { get; set; }
        public Utility.Language Language
        {
            get
            {
                return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Language;
            }
        }
        public string FamilyProductCode { get; set; }
        public string ProductCode { get; set; }
        public IIllusData oIllusDataManager
        {
            get
            {
                return
                    idManager.IIllusDataManager;
            }
        }
        public long? CustomerPlanNo
        {
            get
            {
                return (HttpContext.Current.Session[key] as SessionList).Stored.IllustrationInfo.CustomerPlanNo;
            }
            set
            {
                datos.IllustrationInfo.CustomerPlanNo = value;
                datos.Save();
            }
        }
        public long? CustomerPlanOwnerNo
        {
            get
            {
                return (HttpContext.Current.Session[key] as SessionList).Stored.IllustrationInfo.CustomerPlanOwnerNo;
            }
            set
            {
                datos.IllustrationInfo.CustomerPlanOwnerNo = value;
                datos.Save();
            }
        }
        public long? CustomerNo
        {
            get
            {
                return (HttpContext.Current.Session[key] as SessionList).Stored.IllustrationInfo.CustomerNo;
            }
            set
            {
                datos.IllustrationInfo.CustomerNo = value;
                datos.Save();
            }
        }
        public string IllustrationStatusCode
        {
            get
            {
                return (HttpContext.Current.Session[key] as SessionList).Stored.IllustrationInfo.IllustrationStatusCode;
            }
            set
            {
                datos.IllustrationInfo.IllustrationStatusCode = value;
                datos.Save();
            }
        }
        public string IllusCompanyId
        {
            get
            {
                return (HttpContext.Current.Session[key] as SessionList).Stored.IllustrationInfo.IllusCompanyId;
            }
            set
            {
                datos.IllustrationInfo.IllusCompanyId = value;
                datos.Save();
            }
        }
        public int? IllusUserID
        {
            get
            {
                return (HttpContext.Current.Session[key] as SessionList).Stored.IllustrationInfo.IllusUserID;
            }
            set
            {
                datos.IllustrationInfo.IllusUserID = value;
                datos.Save();
            }
        }
        public IllustrationData IllustrationDataModel { get; set; }
        #endregion
        #endregion
        #region Illustration Methods
        #region Private
        private DataTable Getegr_slide5DataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("InstitucionesColumn");
            table.Columns.Add("_Matricula_2009_US__Column");
            table.Columns.Add("_sostenimiento_2009_US__Column");
            table.Columns.Add("tasa_de_inflacion_anualColumn");
            table.Columns.Add("_Matricula_estimada_2015_US__Column");
            table.Columns.Add("_sostenimiento_estimada_2015_US__Column");
            table.Columns.Add("snoColumn");
            table.Columns.Add("Count");
            return table;
        }

        private String getspanishmonth(int i)
        {
            if (i == 1)
            {
                return "Enero";
            }
            else if (i == 2)
            {
                return "Febrero";
            }
            else if (i == 3)
            {
                return "Marzo";
            }
            else if (i == 4)
            {
                return "Abril";
            }
            else if (i == 5)
            {
                return "Mayo";
            }
            else if (i == 6)
            {
                return "Junio";
            }
            else if (i == 7)
            {
                return "Julio";
            }
            else if (i == 8)
            {
                return "Agosto";
            }
            else if (i == 9)
            {
                return "Septiembre";
            }
            else if (i == 10)
            {
                return "Octubre";
            }
            else if (i == 11)
            {
                return "Noviembre";
            }
            else if (i == 12)
            {
                return "Diciembre";
            }
            else
            {
                return "";
            }
        }

        private String getspanishmonth(String s)
        {
            if (s == "January")
            {
                return "Enero";
            }
            else if (s == "February")
            {
                return "Febrero";
            }
            else if (s == "March")
            {
                return "Marzo";
            }
            else if (s == "April")
            {
                return "Abril";
            }
            else if (s == "May")
            {
                return "Mayo";
            }
            else if (s == "June")
            {
                return "Junio";
            }
            else if (s == "July")
            {
                return "Julio";
            }
            else if (s == "August")
            {
                return "Agosto";
            }
            else if (s == "September")
            {
                return "Septiembre";
            }
            else if (s == "Octomber")
            {
                return "Octubre";
            }
            else if (s == "November")
            {
                return "Noviembre";
            }
            else if (s == "December")
            {
                return "Diciembre";
            }
            else
            {
                return "";
            }
        }

        private string validateGSdata(string productCode, int frequencyTypeValue, double periodicPremium, double insuredAnnuityAmount)
        {
            var gsmaxpremiumamount = RulesService.GetValue(RulesService.Rules.GS_MAXIMUM_PREMIUM_AMOUNT).ToDouble();

            var premiumamount = periodicPremium * frequencyTypeValue;

            if (premiumamount > gsmaxpremiumamount)
                return Resources.CannotBeGreaterThan.SFormat(Resources.PremiumAmount, gsmaxpremiumamount);

            if (FamilyProductCode == Utility.EFamilyProductType.LifeInsurance.Code() || FamilyProductCode == Utility.EFamilyProductType.TermInsurance.Code())
            {
                var gsmaxinsuredamount = RulesService.GetValue(RulesService.Rules.GS_MAXIMUM_INSURED_AMOUNT).ToDouble();

                if (insuredAnnuityAmount > gsmaxinsuredamount)
                    return Resources.CannotBeGreaterThan.SFormat(Resources.InsuredAmount, gsmaxinsuredamount);
            }
            else if (FamilyProductCode == Utility.EFamilyProductType.Education.Code() || FamilyProductCode == Utility.EFamilyProductType.Retirement.Code())
            {
                var gsmaxannuityamount = RulesService.GetValue(RulesService.Rules.GS_MAXIMUM_ANNUITY_AMOUNT).ToDouble();

                if (gsmaxannuityamount != 0 && insuredAnnuityAmount > gsmaxannuityamount)
                    return Resources.CannotBeGreaterThan.SFormat(Resources.AnnuityAmount, gsmaxannuityamount);
            }

            return null;
        }

        private double GetAnnualizedPremium(string productCode, double investmentProfileRate, int frequencyTypeValue, decimal frequencyTypeCost, double periodicPremium)
        {
            try
            {
                double periodicgrowthrate = Math.Pow((1 + investmentProfileRate), 1.0 / frequencyTypeValue * 1.0) - 1;
                double netperiodicpayment = periodicPremium / (1 + frequencyTypeCost.ToDouble());
                double annualizedpremiumamount = CalculatePv(investmentProfileRate, frequencyTypeValue, netperiodicpayment);
                return annualizedpremiumamount;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private void ValidateError(WSValidationError[] wsError)
        {
            StringBuilder sb = new StringBuilder();
            if (wsError != null && wsError.Any())
            {
                foreach (var err in wsError.Where(o => !String.IsNullOrEmpty(o.errormessage)))
                    sb.AppendLine(" -" + (Language == Utility.Language.en ? err.errormessage : err.errormessageespanol) + "</br>");
                if (sb.Length > 0)
                    throw new Exception(sb.ToString());
            }
        }

        private void SetCalculatePlanModel(IllusdataWebServices.WSResult model, CalculatePlanModel calculatePlanModel)
        {
            calculatePlanModel.InsuredAmount = model.insuredamount;
            calculatePlanModel.PeriodicPremium = model.periodicpremiumamount;
            calculatePlanModel.TotalInsuredAmount = model.totalinsuredamount;
            calculatePlanModel.TargetPremium = model.targetpremiumamount;
            calculatePlanModel.SumAnnualPremium = model.annualpremiumamount;
            calculatePlanModel.FractionSurcharge = model.fractionsurcharge;
        }

        private void SetIllusPlan(IllusdataWebServices.WSCustomerPlan wsPlan, Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetail customerPlanDetail)
        {
            wsPlan.activityrisktypeno = customerPlanDetail.ActivityRiskTypeNo;
            wsPlan.annuityamount = customerPlanDetail.AnnuityAmount.ToDouble();
            wsPlan.insuranceperiod = wsPlan.annuityperiod = customerPlanDetail.RetirementPeriod.ToInt();
            wsPlan.calculatetypecode = customerPlanDetail.CalculateTypeCode;
            wsPlan.classcode = customerPlanDetail.PClass;
            var company = oIllusDataManager.GetCompany(customerPlanDetail.CompanyNo);
            wsPlan.company_id = company.BrandName;
            wsPlan.contributionperiod = customerPlanDetail.ContributionPeriod;
            wsPlan.contributiontypecode = customerPlanDetail.ContributionTypeCode;
            wsPlan.contributionuntilage = customerPlanDetail.ContributionUntilAge;
            wsPlan.countryno = customerPlanDetail.CountryNo;
            wsPlan.customerno = customerPlanDetail.CustomerNo.GetValueOrDefault();
            wsPlan.defermentperiod = customerPlanDetail.DefermentPeriod.ToInt();
            wsPlan.financialgoal = customerPlanDetail.FinancialGoal;
            wsPlan.financialgoalage = customerPlanDetail.FinancialGoalAge;
            wsPlan.financialgoalamount = customerPlanDetail.FinancialGoalAmount.ToDouble();
            wsPlan.frequencytypecode = customerPlanDetail.FrequencyTypeCode;
            wsPlan.healthrisktypeno = customerPlanDetail.HealthRiskTypeNo;
            wsPlan.initialcontributionamount = customerPlanDetail.InitialContribution.ToDouble();
            wsPlan.initialcontributiontype = customerPlanDetail.InitialContribution > 0 ? "Y" : "N";
            wsPlan.insurancelevelcode = customerPlanDetail.InsuranceLevelCode;
            wsPlan.insuredamount = customerPlanDetail.InsuredAmount.ToDouble();
            wsPlan.investmentprofilecode = customerPlanDetail.InvestmentProfileCode;
            wsPlan.plantypecode = customerPlanDetail.PlanTypeCode;
            wsPlan.premiumamount = customerPlanDetail.PremiumAmount.ToDouble();
            wsPlan.productcode = customerPlanDetail.ProductCode;
            wsPlan.retirementperiod = customerPlanDetail.RetirementPeriod.ToInt();
            wsPlan.rideroir = customerPlanDetail.RiderOir;
            wsPlan.studentage = customerPlanDetail.StudentAge.GetValueOrDefault();
            wsPlan.studentname = customerPlanDetail.StudentName;
            wsPlan.SpecialPayment = customerPlanDetail.SpecialPayment;
            wsPlan.ProviderTypeId = customerPlanDetail.ProviderTypeId;
            wsPlan.ProviderId = customerPlanDetail.ProviderId;
            wsPlan.FinancingRate = customerPlanDetail.FinancingRate;
            wsPlan.DestinyFund = customerPlanDetail.DestinyFund;
            wsPlan.HaveSpecialPayment = customerPlanDetail.HaveSpecialPayment.GetValueOrDefault();
            //La M significa que se selecciono cantidad en meses.
            if (customerPlanDetail.ProductCode.Contains("VCR") && customerPlanDetail.ContributionTypeCode.Contains("M") &&  customerPlanDetail.ContributionPeriodMonth.HasValue && customerPlanDetail.ContributionPeriodMonth.Value > 0)
            {
                //Convertimos los meses digitados en años
                wsPlan.contributionperiod = customerPlanDetail.ContributionPeriodMonth.Value / 12;
            }
        }

        /// <summary>
        /// Revisar si la prima es mayor al tener riesgos que sin tenerlo.
        /// </summary>
        /// <param name="customerPlanNo"></param>
        /// <param name="customerPlanDetail"></param>
        /// <param name="oldPeriodicPremium"></param>
        private void ReviewSentinelActivityHealthRisk(long customerPlanNo, Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetail customerPlanDetail, double oldPeriodicPremium)
        {
            var activityRiskModel = Utility.GetIllusDropDownByType(Utility.DropDownType.ActivityRiskType, ProductCode)
                    .Single(o => o.ActivityRiskType == "Preferential");

            var healthRiskModel = Utility.GetIllusDropDownByType(Utility.DropDownType.HealthRiskType, ProductCode)
            .Single(o => o.HealthRiskType == "Preferential");

            if (ProductCode == "SNT" && (customerPlanDetail.ActivityRiskTypeNo != activityRiskModel.ActivityRiskTypeNo ||
               customerPlanDetail.HealthRiskTypeNo != healthRiskModel.HealthRiskTypeNo))
            {
                var oldActivityRiskTypeNo = customerPlanDetail.ActivityRiskTypeNo;
                var oldHealthRiskTypeNo = customerPlanDetail.HealthRiskTypeNo;

                customerPlanDetail.ActivityRiskTypeNo = activityRiskModel.ActivityRiskTypeNo.GetValueOrDefault();
                customerPlanDetail.HealthRiskTypeNo = healthRiskModel.HealthRiskTypeNo.GetValueOrDefault();
                ///var calculatePlanModel = CalculatePlanOrSeeIllustration(customerPlanNo, false, customerPlanDetail).Item1;

                //if (oldPeriodicPremium < calculatePlanModel.PeriodicPremium)
                //    oIllusDataManager.UpdateCustomerPlanDetail(customerPlanDetail);
            }
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            this._reportViewer.LocalReport.Refresh();
            this._reportViewer.Reset();

            var egr_age = oIllusDataManager.GetEgrAge();
            e.DataSources.Add(new ReportDataSource("Charts_egr_age", egr_age));
            var rpt_investment = oIllusDataManager.GetRptInvestmentsInflacion();
            e.DataSources.Add(new ReportDataSource("Charts_rpt_investments_inflacion", rpt_investment));
            var rpt_Axys_slide11 = oIllusDataManager.GetRptAxysSlide11();
            e.DataSources.Add(new ReportDataSource("Charts_rpt_Axys_slide11", rpt_Axys_slide11));

            ReportDataSource rds = new ReportDataSource();

            DataTable dtNew = new DataTable();
            dtNew = IllustrationDataModel.DTIllustration.Clone();
            int rowcounter = 1;
            for (int i = 0; i <= IllustrationDataModel.DTIllustration.Rows.Count - 1; i++)
                if (rowcounter < 5)
                    rowcounter++;
                else
                {
                    dtNew.Rows.Add(IllustrationDataModel.DTIllustration.Rows[i].ItemArray);
                    rowcounter = 1;
                }

            dtNew.AcceptChanges();

            rds.Value = dtNew;

            var lstRptLegacy10Priciple = oIllusDataManager.GetRptLegacy10Priciple();

            if (ProductCode == Utility.ProductBehavior.Legacy.Code() ||
                ProductCode == Utility.ProductBehavior.CompassIndex.Code() ||
                ProductCode == Utility.ProductBehavior.Lighthouse.Code() ||
                ProductCode == Utility.ProductBehavior.Sentinel.Code())
            {
                var rpt_Legacy_10_principle = lstRptLegacy10Priciple.Where(o => o.Type == "Paisesde_Bajos");
                e.DataSources.Add(new ReportDataSource("Charts_rpt_Legacy_10_principles", rpt_Legacy_10_principle));
                var rpt_Legacy_10_principle1 = lstRptLegacy10Priciple.Where(o => o.Type == "Paisesde_Altos");
                e.DataSources.Add(new ReportDataSource("Charts_rpt_Legacy_10_principles1", rpt_Legacy_10_principle1));

                e.DataSources.Add(new ReportDataSource("illustratorDataSet_illusdet", dtNew));
            }
            if (ProductCode == Utility.ProductBehavior.Legacy.Code())
                this._reportViewer.LocalReport.ReportPath = _reportPath + ("LegacyTwo.rdlc");
            else if (ProductCode == Utility.ProductBehavior.CompassIndex.Code())
                this._reportViewer.LocalReport.ReportPath = _reportPath + ("CompassIndex_Two.rdlc");
            else if (ProductCode == Utility.ProductBehavior.Lighthouse.Code())
                this._reportViewer.LocalReport.ReportPath = _reportPath + ("LightHouseTwo.rdlc");
            else if (ProductCode == Utility.ProductBehavior.Sentinel.Code())
                this._reportViewer.LocalReport.ReportPath = _reportPath + ("SentinelTwo.rdlc");
            else if (ProductCode == Utility.ProductBehavior.EduPlan.Code() ||
                ProductCode == Utility.ProductBehavior.Scholar.Code() ||
                ProductCode == Utility.ProductBehavior.Axys.Code() ||
                ProductCode == Utility.ProductBehavior.Horizon.Code())
            {
                this._reportViewer.LocalReport.ReportPath = _reportPath +
                    (ProductCode == Utility.ProductBehavior.EduPlan.Code() ? "EduplanTwo" :
                    ProductCode == Utility.ProductBehavior.Scholar.Code() ? "ScholarTwo" :
                    ProductCode == Utility.ProductBehavior.Axys.Code() ? "AxysTwo" :
                    ProductCode == Utility.ProductBehavior.Horizon.Code() ? "HorizonTwo" : "")
                    + ".rdlc";

                var egr_slide8 = oIllusDataManager.GetEgrSlide8();
                e.DataSources.Add(new ReportDataSource("Charts_egr_slide8", egr_slide8));

                if (ProductCode == Utility.ProductBehavior.EduPlan.Code() || ProductCode == Utility.ProductBehavior.Scholar.Code())
                {
                    var egr_slide9 = oIllusDataManager.GetEgrSlide9();
                    e.DataSources.Add(new ReportDataSource("Charts_egr_slide9", egr_slide9));
                    var egr_slide10 = oIllusDataManager.GetEgrSlide10();
                    e.DataSources.Add(new ReportDataSource("Charts_egr_slide10", egr_slide10));
                }
                else if (ProductCode == Utility.ProductBehavior.Axys.Code() || ProductCode == Utility.ProductBehavior.Horizon.Code())
                {
                    var rpt_axys_slide5 = oIllusDataManager.GetRptAxysSlide5();
                    e.DataSources.Add(new ReportDataSource("Charts_rpt_axys_slide5", rpt_axys_slide5));
                    var rpt_axys_slide6 = oIllusDataManager.GetRptAxysSlide6();
                    e.DataSources.Add(new ReportDataSource("Charts_rpt_axys_slide6", rpt_axys_slide6));
                    var rpt_axys_slide8 = oIllusDataManager.GetRptAxysSlide8();
                    e.DataSources.Add(new ReportDataSource("Charts_rpt_axys_slide8", rpt_axys_slide8));
                }

                e.DataSources.Add(new ReportDataSource("illustratorDataSet_illushorizon", dtNew));
            }

            var rpt_Life_Expectancy = oIllusDataManager.GetRptLifeExpectancy();
            e.DataSources.Add(new ReportDataSource("Charts_rpt_lifeexpectancy", rpt_Life_Expectancy));
            var rpt_Compass_Slide5 = oIllusDataManager.GetRptCompassSlide5();
            e.DataSources.Add(new ReportDataSource("Charts_rpt_Compass_Slide5", rpt_Compass_Slide5));
        }

        private void showIllustrationRED(DataTable dt, IMaineduretire insmain,
            Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetail customerPlanDetail,
            Entity.UnderWriting.IllusData.Illustrator.CustomerDetail customer
            )
        {
            this._reportViewer.LocalReport.Refresh();
            this._reportViewer.Reset();

            if (ProductCode == Utility.ProductBehavior.Axys.Code())
                this._reportViewer.LocalReport.ReportPath = _reportPath + "Axys_long.rdlc";
            else if (ProductCode == Utility.ProductBehavior.Scholar.Code())
                this._reportViewer.LocalReport.ReportPath = _reportPath + "Scholar_long.rdlc";

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "illustratorDataSet_illushorizon";

            if (ProductCode == Utility.ProductBehavior.Scholar.Code() || ProductCode == Utility.ProductBehavior.Axys.Code())
                if (dt.Rows.Count >= 16 && dt.Rows.Count <= 20 || dt.Rows.Count >= 36 && dt.Rows.Count <= 40 ||
                   dt.Rows.Count >= 56 && dt.Rows.Count <= 60 || dt.Rows.Count >= 76 && dt.Rows.Count <= 80 ||
                   dt.Rows.Count >= 96 && dt.Rows.Count <= 100)
                {
                    DataRow myRow;
                    myRow = dt.NewRow();
                    var countdt = 0;

                    if (dt.Rows.Count == 19 || dt.Rows.Count == 39 || dt.Rows.Count == 59 || dt.Rows.Count == 79 || dt.Rows.Count == 99)
                        countdt = 1;
                    else if (dt.Rows.Count == 18 || dt.Rows.Count == 38 || dt.Rows.Count == 58 || dt.Rows.Count == 78 || dt.Rows.Count == 98)
                        countdt = 2;
                    else if (dt.Rows.Count == 17 || dt.Rows.Count == 37 || dt.Rows.Count == 57 || dt.Rows.Count == 77 || dt.Rows.Count == 97)
                        countdt = 3;
                    else if (dt.Rows.Count == 16 || dt.Rows.Count == 36 || dt.Rows.Count == 56 || dt.Rows.Count == 76 || dt.Rows.Count == 96)
                        countdt = 4;

                    for (int c = 0; c < countdt; c++)
                        dt.Rows.Add("", "", "", "", "", "", "");

                    dt.Rows.Add(myRow);
                    rds.Value = dt;
                }

            rds.Value = dt;

            string defermentperiod = "-";
            string contributionperiod = "-";
            string ddlinitialcontribution = "-";
            string initialcontributionamount = "-";
            string txtsumannualpremium = "-";
            string risk = "-";
            string almillar = "-";
            string plantype = "-";
            string maritalstatus = "-";
            string investmentprofile = "-";
            string freqofpayment = "-";

            investmentprofile = Resources.ResourceManager.GetString(customerPlanDetail.InvestmentProfile);

            if (customerPlanDetail != null)
            {
                if (customerPlanDetail.FrequencyTypeCode != null)
                    freqofpayment = Resources.ResourceManager.GetString(customerPlanDetail.FrequencyType);

                defermentperiod = customerPlanDetail.DefermentPeriod.ToString();
                contributionperiod = customerPlanDetail.ContributionPeriod.ToString();
                txtsumannualpremium = customerPlanDetail.AnnualizedPremium.ToString();
            }
            if (customerPlanDetail.InitialContribution > 0)
            {
                ddlinitialcontribution = "Yes";
                initialcontributionamount = customerPlanDetail.InitialContribution.ToString();
            }
            else
            {
                ddlinitialcontribution = "No";
                initialcontributionamount = "0";
            }

            risk = Resources.ResourceManager.GetString(customerPlanDetail.ActivityRiskType);

            almillar = Resources.ResourceManager.GetString(customerPlanDetail.HealthRiskType);

            plantype = Resources.ResourceManager.GetString(customerPlanDetail.PlanType);

            maritalstatus = Resources.ResourceManager.GetString(customer.MaritalStatus);
            string primaryreq = "";

            var exams = oIllusDataManager.GetCustomerPlanExam(CustomerPlanNo.GetValueOrDefault(), Insuredtypes.PRIMARY);

            String[] req = new String[12];

            int i = 0;
            foreach (var exam in exams)
            {
                primaryreq = exam.ExamName + "/" + primaryreq;
                req[i] = Resources.ResourceManager.GetString(exam.ExamName);
                i++;
            }

            string tempPrimary = " ";
            int lno1 = 0;

            for (int j = 0; j < req.Length; j++)
                if (!string.IsNullOrEmpty(req[j]))
                    if (!req[j].Trim().Equals(""))
                    {
                        tempPrimary += req[j];
                        if (j != (req.Length - 1))
                            tempPrimary += "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t";
                        if (tempPrimary.Length > (lno1 + 1) * 150)
                        {
                            tempPrimary += Environment.NewLine;
                            lno1 = lno1 + 1;
                        }
                    }

            List<ReportParameter> paramlist = new List<ReportParameter>();

            if (ProductCode == Utility.ProductBehavior.Axys.Code() || ProductCode == Utility.ProductBehavior.Scholar.Code())
            {
                ReportParameter param100 = new ReportParameter("PolicyholderExams1", "" + tempPrimary);
                paramlist.Add(param100);
            }

            _prospect = (((customer.FirstName + " " + customer.MiddleName).Trim() + " " + customer.LastName).Trim() + " " + customer.LastName2).Trim();

            const int MaxLengthHeading = 30;
            const int MaxLengthName = 50;
            var custHeading = (customer.FirstName + " " + customer.LastName).Trim();
            var custName = (customer.FirstName + " " + customer.LastName).Trim();
            if (custHeading.Length > MaxLengthHeading)
            {
                custHeading = custHeading.Substring(0, MaxLengthHeading);
                custHeading = custHeading + "...";
            }
            if (custName.Length > MaxLengthName)
            {
                custName = custName.Substring(0, MaxLengthName);
                custName = custName + "...";
            }

            ReportParameter param1 = new ReportParameter("heading", custHeading);
            ReportParameter param3 = new ReportParameter("name", custName);
            ReportParameter param4 = new ReportParameter("periodofcontribution", customerPlanDetail.ContributionPeriod + "");
            ReportParameter param5 = new ReportParameter("amountofcontribution", customerPlanDetail.InitialContribution.ToFormatCurrency());
            ReportParameter param6 = new ReportParameter("withdrawalperiod", customerPlanDetail.RetirementPeriod + "");
            ReportParameter param7 = new ReportParameter("withdrawalamount", customerPlanDetail.AnnuityAmount.ToFormatCurrency());
            ReportParameter param8 = new ReportParameter("plantype", plantype + "");

            ReportParameter param9 = new ReportParameter("age", customer.Age + "");
            ReportParameter param10 = new ReportParameter("gender", customer.GenderCode + "");
            ReportParameter param11 = new ReportParameter("smoker", customer.Smoker == "Y" ? Resources.YesLabel : "No");

            ReportParameter param12 = new ReportParameter("ageatretirement", customerPlanDetail.ContributionUntilAge + "");
            ReportParameter param13 = new ReportParameter("risk", risk + "");
            ReportParameter param14 = new ReportParameter("almillar", almillar + "");
            ReportParameter param15 = new ReportParameter("maritalstatus", maritalstatus + "");
            ReportParameter param16 = new ReportParameter("plan", customerPlanDetail.Product);
            ReportParameter param17 = new ReportParameter("deferralperiod", customerPlanDetail.DefermentPeriod + "");
            var country = Utility.GetIllusDropDownByType(Utility.DropDownType.Country)
         .Single(o => o.CountryNo == customer.ResCountryNo).CountryName;
            ReportParameter param18 = new ReportParameter("country", Resources.ResourceManager.GetString(country));
            ReportParameter param19 = new ReportParameter("investmentprofile", investmentprofile + "");
            ReportParameter param20 = new ReportParameter("frequencyofcontribution", freqofpayment);
            ReportParameter param21 = new ReportParameter("initialcontribution", ddlinitialcontribution);
            ReportParameter param22 = new ReportParameter("totalamount", (customerPlanDetail.AnnuityAmount * customerPlanDetail.RetirementPeriod).ToFormatCurrency());
            ReportParameter param23 = new ReportParameter("primatarget", customerPlanDetail.TargetPremium.ToFormatCurrency());
            ReportParameter param24 = new ReportParameter("minprima", customerPlanDetail.MinimumPremium.ToFormatCurrency());
            ReportParameter param31 = new ReportParameter("InitialContributionAmount", initialcontributionamount);
            ReportParameter param45 = new ReportParameter("Examinations", primaryreq);
            ReportParameter param46 = new ReportParameter("BottomText", "Esta presentación tiene una validez de 15 días hábiles y en ningún caso más allá del" + " 31-Diciembre-" + DateTime.Now.Year.ToString());
            ReportParameter param47 = new ReportParameter("number", _numberPageIllus);
            ReportParameter param1_13 = new ReportParameter("lastname", (customer.LastName + " ").Trim());
            paramlist.Add(param1_13);

            paramlist.Add(param1);
            paramlist.Add(param3);
            paramlist.Add(param4);
            paramlist.Add(param5);

            paramlist.Add(param6);
            paramlist.Add(param7);


            paramlist.Add(param8);
            paramlist.Add(param9);
            paramlist.Add(param10);
            paramlist.Add(param11);
            paramlist.Add(param12);
            paramlist.Add(param13);
            paramlist.Add(param14);

            paramlist.Add(param15);
            paramlist.Add(param16);
            paramlist.Add(param17);
            paramlist.Add(param18);
            paramlist.Add(param19);
            paramlist.Add(param20);
            paramlist.Add(param21);
            paramlist.Add(param22);
            paramlist.Add(param23);
            paramlist.Add(param24);
            paramlist.Add(param31);
            paramlist.Add(param45);
            paramlist.Add(param46);
            paramlist.Add(param47);

            if (!String.IsNullOrEmpty(_checkedSettings) || !String.IsNullOrEmpty(_unCheckedSettings))
            {
                string[] checkedSettings = _checkedSettings.NTrim().TrimStart(',').Split(',');
                string[] unCheckedSettings = _unCheckedSettings.NTrim().TrimStart(',').Split(',');

                for (int counter1 = 0; counter1 < checkedSettings.Count(); counter1++)
                {
                    ReportParameter visibilityParam = new ReportParameter(checkedSettings[counter1].ToString(), "checked");
                    paramlist.Add(visibilityParam);
                }

                for (int counter2 = 0; counter2 < unCheckedSettings.Count(); counter2++)
                {
                    if (!(unCheckedSettings[counter2].ToString() == null || unCheckedSettings[counter2].ToString() == ""))
                    {
                        ReportParameter visibilityParam = new ReportParameter(unCheckedSettings[counter2].ToString(), "unchecked");
                        paramlist.Add(visibilityParam);
                    }
                }
            }
            String[] clientsign = new string[3];
            String[] agentsign = new string[3];

            clientsign[0] = "N";
            clientsign[1] = "N";
            clientsign[2] = "N";

            agentsign[0] = "N";
            agentsign[1] = "N";
            agentsign[2] = "N";

            ReportParameter Cpath1 = null;
            ReportParameter Apath1 = null;
            ReportParameter Cpath2 = null;
            ReportParameter Apath2 = null;
            ReportParameter Cpath3 = null;
            ReportParameter Apath3 = null;

            try
            {
                var isig = oIllusDataManager.GetIllustrationSignature(CustomerPlanNo.GetValueOrDefault()).First();

                clientsign[0] = isig.CustomerSign1.ToString();
                clientsign[1] = isig.CustomerSign2.ToString();
                clientsign[2] = isig.CustomerSign3.ToString();

                agentsign[0] = isig.AgentSign1.ToString();
                agentsign[1] = isig.AgentSign2.ToString();
                agentsign[2] = isig.AgentSign3.ToString();
            }
            catch (Exception ex)
            {

            }

            if (clientsign[0].Equals("Y"))
                Cpath1 = new ReportParameter("clientsignpath1", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_01C.jpg");
            else
                Cpath1 = new ReportParameter("clientsignpath1", _reportSignaturePath + "empty.jpg");

            if (agentsign[0].Equals("Y"))
                Apath1 = new ReportParameter("agentsignpath1", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_01A.jpg");
            else
                Apath1 = new ReportParameter("agentsignpath1", _reportSignaturePath + "empty.jpg");

            if (clientsign[1].Equals("Y"))
                Cpath2 = new ReportParameter("clientsignpath2", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_02C.jpg");
            else
                Cpath2 = new ReportParameter("clientsignpath2", _reportSignaturePath + "empty.jpg");

            if (agentsign[1].Equals("Y"))
                Apath2 = new ReportParameter("agentsignpath2", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_02A.jpg");
            else
                Apath2 = new ReportParameter("agentsignpath2", _reportSignaturePath + "empty.jpg");

            if (clientsign[2].Equals("Y"))
                Cpath3 = new ReportParameter("clientsignpath3", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_03C.jpg");
            else
                Cpath3 = new ReportParameter("clientsignpath3", _reportSignaturePath + "empty.jpg");

            if (agentsign[2].Equals("Y"))
                Apath3 = new ReportParameter("agentsignpath3", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_03A.jpg");
            else
                Apath3 = new ReportParameter("agentsignpath3", _reportSignaturePath + "empty.jpg");

            paramlist.Add(Cpath1);
            paramlist.Add(Apath1);
            paramlist.Add(Cpath2);
            paramlist.Add(Apath2);
            paramlist.Add(Cpath3);
            paramlist.Add(Apath3);


            var rpt_investment = oIllusDataManager.GetRptInvestmentsInflacion();
            var count = rpt_investment.Count();
            var inflation_Last = (from ms in rpt_investment
                                  select new { PequenasAcciones = ms.Pequenas_Acciones, GrandesAcciones = ms.Grandes_Acciones, PapelesdelTesoro = ms.Papelesdel_Tesoro, BonosdelGobierno = ms.Bonosdel_Gobierno, ms.Inflacion }).Last();
            var inflation_first = (from ms in rpt_investment
                                   select new { PequenasAcciones = ms.Pequenas_Acciones, GrandesAcciones = ms.Grandes_Acciones, PapelesdelTesoro = ms.Papelesdel_Tesoro, BonosdelGobierno = ms.Bonosdel_Gobierno, ms.Inflacion }).First();
            var percent_Pequenas_last = inflation_Last.PequenasAcciones;
            var percent_Pequenas_First = inflation_first.PequenasAcciones;
            var Grandes_Acciones_last = inflation_Last.GrandesAcciones;
            var Grandes_Acciones_First = inflation_first.GrandesAcciones;
            var Papelesdel_Tesoro_last = inflation_Last.PapelesdelTesoro;
            var Papelesdel_Tesoro_First = inflation_first.PapelesdelTesoro;
            var Bonosdel_Gobierno_last = inflation_Last.BonosdelGobierno;
            var Bonosdel_Gobierno_First = inflation_first.BonosdelGobierno;
            var Inflacion1_last = inflation_Last.Inflacion;
            var Inflacion1_First = inflation_first.Inflacion;


            var result_total = percent_Pequenas_last / percent_Pequenas_First;
            var numberofyears = 0.0119;
            var Pequenas_percent = (Math.Pow(Convert.ToDouble(result_total), numberofyears) - 1) * 100;

            var result1_total = Grandes_Acciones_last / Grandes_Acciones_First;
            var numberofyears1 = 0.0119;
            var Grandes_percent = (Math.Pow(Convert.ToDouble(result1_total), numberofyears1) - 1) * 100;

            var result2_total = Papelesdel_Tesoro_last / Papelesdel_Tesoro_First;
            var numberofyears2 = 0.0119;
            var Papelesdel_percent = (Math.Pow(Convert.ToDouble(result2_total), numberofyears2) - 1) * 100;

            var result3_total = Bonosdel_Gobierno_last / Bonosdel_Gobierno_First;
            var numberofyears3 = 0.0119;
            var Bonosdel_percent = (Math.Pow(Convert.ToDouble(result3_total), numberofyears3) - 1) * 100;

            var result4_total = Inflacion1_last / Inflacion1_First;
            var numberofyears4 = 0.0119;
            var Inflacion_percent = (Math.Pow(Convert.ToDouble(result4_total), numberofyears4) - 1) * 100;

            ReportParameter Pequenas_Acciones = new ReportParameter("Pequenas_Acciones", Pequenas_percent.ToString());
            ReportParameter Grandes = new ReportParameter("Grandes", Grandes_percent.ToString());
            ReportParameter Papelesdel_Tesoro = new ReportParameter("Papelesdel_Tesoro", Papelesdel_percent.ToString());
            ReportParameter Bonosdel_Gobierno = new ReportParameter("Bonosdel_Gobierno", Bonosdel_percent.ToString());
            ReportParameter Inflacion = new ReportParameter("Inflacion", Inflacion_percent.ToString());
            paramlist.Add(Pequenas_Acciones);
            paramlist.Add(Grandes);
            paramlist.Add(Papelesdel_Tesoro);
            paramlist.Add(Bonosdel_Gobierno);
            paramlist.Add(Inflacion);

            ReportParameter param308 = new ReportParameter("S308", "unchecked");
            paramlist.Add(param308);

            var lstInvestmentMaster = oIllusDataManager.GetRptInvestmentsCompassMaster();
            var lstInvestmentsCompass = oIllusDataManager.GetRptInvestmentsCompass(0);

            var st_rpt_compass_investment_master_moderado = lstInvestmentMaster.Where(o => o.ReturnType == "Moderado" && o.Region == "Americano/Internacional");
            ReportDataSource rds_rpt_compss_investment_master_moderado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_moderado);

            var st_rpt_compass_investment_distribution_moderado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 1);
            ReportDataSource rds_rpt_compass_investment_distribution_moderado = new ReportDataSource("Charts_rpt_compass_investment_details_MODERADO", st_rpt_compass_investment_distribution_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_moderado);

            var st_rpt_compass_investment_master_balanceado = lstInvestmentMaster.Where(o => o.ReturnType == "Balanceado" && o.Region == "Americano/Internacional");
            ReportDataSource rds_rpt_compss_investment_master_balanceado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_balanceado);

            var st_rpt_compass_investment_distribution_balanceado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 2);
            ReportDataSource rds_rpt_compass_investment_distribution_balanceado = new ReportDataSource("Charts_rpt_compass_investment_details_BALANCEADO", st_rpt_compass_investment_distribution_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_balanceado);

            var st_rpt_compass_investment_master_cricimiento = lstInvestmentMaster.Where(o => o.ReturnType == "Crecimiento" && o.Region == "Americano/Internacional");
            ReportDataSource rds_rpt_compss_investment_master_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_cricimiento);

            var st_rpt_compass_investment_distribution_cricimiento = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 3);
            ReportDataSource rds_rpt_compass_investment_distribution_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_details_CRICIMIENTO", st_rpt_compass_investment_distribution_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_cricimiento);

            var st_rpt_compass_investment_master_euro_moderado = lstInvestmentMaster.Where(o => o.ReturnType == "Moderado" && o.Region == "Europeo");
            ReportDataSource rds_rpt_compss_investment_master_euro_moderado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_euro_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_euro_moderado);

            var st_rpt_compass_investment_distribution_euro_moderado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 4);
            ReportDataSource rds_rpt_compass_investment_distribution_euro_moderado = new ReportDataSource("Charts_rpt_compass_investment_details_euro_MODERADO", st_rpt_compass_investment_distribution_euro_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_euro_moderado);

            var st_rpt_compass_investment_master_euro_balanceado = lstInvestmentMaster.Where(o => o.ReturnType == "Balanceado" && o.Region == "Europeo");
            ReportDataSource rds_rpt_compss_investment_master_euro_balanceado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_euro_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_euro_balanceado);

            var st_rpt_compass_investment_distribution_euro_balanceado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 5);
            ReportDataSource rds_rpt_compass_investment_distribution_euro_balanceado = new ReportDataSource("Charts_rpt_compass_investment_details_euro_BALANCEADO", st_rpt_compass_investment_distribution_euro_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_euro_balanceado);

            var st_rpt_compass_investment_master_euro_cricimiento = lstInvestmentMaster.Where(o => o.ReturnType == "Crecimiento" && o.Region == "Europeo");
            ReportDataSource rds_rpt_compss_investment_master_euro_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_euro_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_euro_cricimiento);

            var st_rpt_compass_investment_distribution_euro_cricimiento = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 6);
            ReportDataSource rds_rpt_compass_investment_distribution_euro_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_details_euro_CRICIMIENTO", st_rpt_compass_investment_distribution_euro_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_euro_cricimiento);

            var lst_rpt_compass_investment_yearreturn_americano =
            (from item in lstInvestmentsCompass
             join o in lstInvestmentMaster
             on item.ReturnTypeid equals o.Sno
             where (item.Years == 2010 && o.Region == "Americano/Internacional")
             select item).ToList();

            ReportDataSource rds_rpt_compass_investment_yearreturn_americano = new ReportDataSource("Charts_rpt_compass_investment_details_yearreturn_Americano", lst_rpt_compass_investment_yearreturn_americano);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_yearreturn_americano);
            var lst_rpt_compass_investment_yearreturn_europeo =
            (from item in lstInvestmentsCompass
             join o in lstInvestmentMaster
             on item.ReturnTypeid equals o.Sno
             where (item.Years == 2010 && o.Region == "Europeo")
             select item).ToList();

            ReportDataSource rds_rpt_compass_investment_yearreturn_europeo = new ReportDataSource("Charts_rpt_compass_investment_details_yearreturn_Europeo", lst_rpt_compass_investment_yearreturn_europeo);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_yearreturn_europeo);

            var lst_rpt_profile_de_inversion = oIllusDataManager.GetInvestmentsProfile();

            var lst_rpt_profile_de_inversion_euro = oIllusDataManager.GetInvestmentsProfileEuro();

            ReportDataSource rds_profile_de_inversion = new ReportDataSource("Charts_profile_de_inversion", lst_rpt_profile_de_inversion);
            _reportViewer.LocalReport.DataSources.Add(rds_profile_de_inversion);
            ReportDataSource rds_profile_de_inversion_euro = new ReportDataSource("Charts_profile_de_inversion_euro", lst_rpt_profile_de_inversion_euro);
            _reportViewer.LocalReport.DataSources.Add(rds_profile_de_inversion_euro);

            var lstInvestmentType = oIllusDataManager.GetRptInvestmentsType(null, null, null);

            var lst_rpt_invest_distribution_Moderado = lstInvestmentType.Where(o => o.FundCategory == "Moderado" && o.Region == "Americano/Internacional");

            var lst_rpt_invest_distribution_Balanceado = lstInvestmentType.Where(o => o.FundCategory == "Balanceado" && o.Region == "Americano/Internacional");

            var lst_rpt_invest_distribution_Crecimiento = lstInvestmentType.Where(o => o.FundCategory == "Crecimiento" && o.Region == "Americano/Internacional");

            ReportDataSource rds_invest_distribution_Moderado = new ReportDataSource("Charts_rpt_InvestType_MODERADO", lst_rpt_invest_distribution_Moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_distribution_Moderado);
            ReportDataSource rds_invest_distribution_Balanceado = new ReportDataSource("Charts_rpt_InvestType_BALANCEADO", lst_rpt_invest_distribution_Balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_distribution_Balanceado);
            ReportDataSource rds_invest_distribution_Crecimiento = new ReportDataSource("Charts_rpt_InvestType_CRECIMIENTO", lst_rpt_invest_distribution_Crecimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_distribution_Crecimiento);
            var lst_rpt_invest_euro_distribution_Moderado = lstInvestmentType.Where(o => o.FundCategory == "Moderado" && o.Region == "Europeo");

            ReportDataSource rds_invest_euro_distribution_Moderado = new ReportDataSource("Charts_rpt_euro_InvestType_MODERADO", lst_rpt_invest_euro_distribution_Moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_euro_distribution_Moderado);
            var lst_rpt_invest_euro_distribution_Balanceado = lstInvestmentType.Where(o => o.FundCategory == "Balanceado" && o.Region == "Europeo");

            ReportDataSource rds_invest_euro_distribution_Balanceado = new ReportDataSource("Charts_rpt_euro_InvestType_BALANCEADO", lst_rpt_invest_euro_distribution_Balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_euro_distribution_Balanceado);
            var lst_rpt_invest_euro_distribution_Crecimiento = lstInvestmentType.Where(o => o.FundCategory == "Crecimiento" && o.Region == "Europeo");

            ReportDataSource rds_invest_euro_distribution_Crecimiento = new ReportDataSource("Charts_rpt_euro_InvestType_CRECIMIENTO", lst_rpt_invest_euro_distribution_Crecimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_euro_distribution_Crecimiento);

            var totMonderadoBond = from mb in lst_rpt_invest_distribution_Moderado
                                   where mb.FundType == "Bond" && mb.FundCategory == "Moderado"
                                   group mb by mb.FundCategory into g
                                   select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var totMonderadoStock = from ms in lst_rpt_invest_distribution_Moderado
                                    where ms.FundType == "Stock" && ms.FundCategory == "Moderado"
                                    group ms by ms.FundCategory into g
                                    select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter prmMonderadoBondShare = new ReportParameter("MonderadoBondShare", totMonderadoBond.First().total.ToString());
            paramlist.Add(prmMonderadoBondShare);
            ReportParameter prmMonderadoStockShare = new ReportParameter("MonderadoStockShare", totMonderadoStock.First().total.ToString());
            paramlist.Add(prmMonderadoStockShare);
            var totBalanceadoBond = from mb in lst_rpt_invest_distribution_Balanceado
                                    where mb.FundType == "Bond" && mb.FundCategory == "Balanceado"
                                    group mb by mb.FundCategory into g
                                    select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var totBalanceadoStock = from ms in lst_rpt_invest_distribution_Balanceado
                                     where ms.FundType == "Stock" && ms.FundCategory == "Balanceado"
                                     group ms by ms.FundCategory into g
                                     select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter prmBalanceadoBondShare = new ReportParameter("BalanceadoBondShare", totBalanceadoBond.First().total.ToString());
            paramlist.Add(prmBalanceadoBondShare);
            ReportParameter prmBalanceadoStockShare = new ReportParameter("BalanceadoStockShare", totBalanceadoStock.First().total.ToString());
            paramlist.Add(prmBalanceadoStockShare);

            var totCrecimientoBond = from mb in lst_rpt_invest_distribution_Crecimiento
                                     where mb.FundType == "Bond" && mb.FundCategory == "Crecimiento"
                                     group mb by mb.FundCategory into g
                                     select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var totCrecimientoStock = from ms in lst_rpt_invest_distribution_Crecimiento
                                      where ms.FundType == "Stock" && ms.FundCategory == "Crecimiento"
                                      group ms by ms.FundCategory into g
                                      select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            string shareCrecimientoBond;
            if (totCrecimientoBond.FirstOrDefault() != null)
                shareCrecimientoBond = totCrecimientoBond.FirstOrDefault().total.ToString();
            else
                shareCrecimientoBond = "0";

            ReportParameter prmCrecimientoBondShare = new ReportParameter("CrecimientoBondShare", shareCrecimientoBond);
            paramlist.Add(prmCrecimientoBondShare);
            ReportParameter prmCrecimientoStockShare = new ReportParameter("CrecimientoStockShare", totCrecimientoStock.First().total.ToString());
            paramlist.Add(prmCrecimientoStockShare);

            var euro_totBalanceadoBond = from mb in lst_rpt_invest_euro_distribution_Balanceado
                                         where mb.FundType == "Bond" && mb.FundCategory == "Balanceado"
                                         group mb by mb.FundCategory into g
                                         select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var euro_totBalanceadoStock = from ms in lst_rpt_invest_euro_distribution_Balanceado
                                          where ms.FundType == "Stock" && ms.FundCategory == "Balanceado"
                                          group ms by ms.FundCategory into g
                                          select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter euro_prmBalanceadoBondShare = new ReportParameter("euro_BalanceadoBondShare", euro_totBalanceadoBond.First().total.ToString());
            paramlist.Add(euro_prmBalanceadoBondShare);
            ReportParameter euro_prmBalanceadoStockShare = new ReportParameter("euro_BalanceadoStockShare", euro_totBalanceadoStock.First().total.ToString());
            paramlist.Add(euro_prmBalanceadoStockShare);

            var euro_totMonderadoBond = from mb in lst_rpt_invest_euro_distribution_Moderado
                                        where mb.FundType == "Bond" && mb.FundCategory == "Moderado"
                                        group mb by mb.FundCategory into g
                                        select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var euro_totMonderadoStock = from ms in lst_rpt_invest_euro_distribution_Moderado
                                         where ms.FundType == "Stock" && ms.FundCategory == "Moderado"
                                         group ms by ms.FundCategory into g
                                         select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter euro_prmMonderadoBondShare = new ReportParameter("euro_MonderadoBondShare", euro_totMonderadoBond.First().total.ToString());
            paramlist.Add(euro_prmMonderadoBondShare);
            ReportParameter euro_prmMonderadoStockShare = new ReportParameter("euro_MonderadoStockShare", euro_totMonderadoStock.First().total.ToString());
            paramlist.Add(euro_prmMonderadoStockShare);

            var euro_totCrecimientoBond = from mb in lst_rpt_invest_distribution_Crecimiento
                                          where mb.FundType == "Bond" && mb.FundCategory == "Crecimiento"
                                          group mb by mb.FundCategory into g
                                          select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var euro_totCrecimientoStock = from ms in lst_rpt_invest_distribution_Crecimiento
                                           where ms.FundType == "Stock" && ms.FundCategory == "Crecimiento"
                                           group ms by ms.FundCategory into g
                                           select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            string euro_shareCrecimientoBond;
            if (totCrecimientoBond.FirstOrDefault() != null)
                euro_shareCrecimientoBond = euro_totCrecimientoBond.FirstOrDefault().total.ToString();
            else
                euro_shareCrecimientoBond = "0";

            ReportParameter euro_prmCrecimientoBondShare = new ReportParameter("euro_CrecimientoBondShare", euro_shareCrecimientoBond);
            paramlist.Add(euro_prmCrecimientoBondShare);
            ReportParameter euro_prmCrecimientoStockShare = new ReportParameter("euro_CrecimientoStockShare", euro_totCrecimientoStock.First().total.ToString());
            paramlist.Add(euro_prmCrecimientoStockShare);

            this._reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

            string studentname = "-";
            if (!string.IsNullOrEmpty(customerPlanDetail.StudentName))
                if (!customerPlanDetail.StudentName.Trim().Equals(""))
                    studentname = customerPlanDetail.StudentName;

            string studentage = "-";
            if (customerPlanDetail.StudentAge.GetValueOrDefault() != 0)
                studentage = customerPlanDetail.StudentAge.ToString();

            if (ProductCode == Utility.ProductBehavior.Axys.Code())
            {
                ReportParameter param25 = new ReportParameter("agepension", "" + (defermentperiod.ToInt() + contributionperiod.ToInt() + customer.Age.ToInt()).ToString());
                ReportParameter param26 = new ReportParameter("lastline", Generalmethods.getLastline(_prospect, new Random().Next(1, 20), customerPlanDetail.@PClass[0]));
                ReportParameter param27 = new ReportParameter("class", customerPlanDetail.@PClass);
                ReportParameter param30 = new ReportParameter("AnnualPremium", txtsumannualpremium.ToDecimal().ToFormatCurrency());
                ReportParameter param32 = new ReportParameter("premiumamount", customerPlanDetail.PremiumAmount.ToFormatCurrency());
                ReportParameter param1_12 = new ReportParameter("Lastname", (customer.LastName + " ").Trim());
                ReportParameter param120 = new ReportParameter("date", getspanishmonth(Int32.Parse(DateTime.Now.ToString("MM"))) + " " + DateTime.Now.ToString("dd, yyyy"));
                paramlist.Add(param120);
                paramlist.Add(param1_12);
                paramlist.Add(param25);
                paramlist.Add(param26);
                paramlist.Add(param27);
                paramlist.Add(param30);
                paramlist.Add(param32);

                var rptincome = oIllusDataManager.GetRptAxysFixedinComeSlide12();
                var rpthigh = oIllusDataManager.GetRptAxysHighperFormSlide12();
                var rptrisk = oIllusDataManager.GetRptAxysLowRiskSlide12();
                var rpt_axys_slide5 = oIllusDataManager.GetRptAxysSlide5();
                var rpt_axys_slide6 = oIllusDataManager.GetRptAxysSlide6();
                var rpt_axys_slide8 = oIllusDataManager.GetRptAxysSlide8();

                ReportDataSource axysslide5 = new ReportDataSource("Charts_rpt_axys_slide5", rpt_axys_slide5);
                ReportDataSource axysslide8 = new ReportDataSource("Charts_rpt_axys_slide8", rpt_axys_slide8);
                ReportDataSource axysslide6 = new ReportDataSource("Charts_rpt_axys_slide6", rpt_axys_slide6);

                _reportViewer.LocalReport.DataSources.Add(axysslide5);
                _reportViewer.LocalReport.DataSources.Add(axysslide6);
                _reportViewer.LocalReport.DataSources.Add(axysslide8);

                ReportParameter param100 = new ReportParameter("PolicyholderExams1", "" + tempPrimary);

                paramlist.Add(param100);
            }

            if (ProductCode == Utility.ProductBehavior.Scholar.Code())
            {
                ReportParameter param25 = new ReportParameter("periodofstudy", "-");
                ReportParameter param26 = new ReportParameter("studentage", studentage + "");
                ReportParameter param27 = new ReportParameter("studentname", studentname + "");
                ReportParameter param28 = new ReportParameter("kind", customerPlanDetail.PClass);
                ReportParameter param29 = new ReportParameter("agepension", (defermentperiod.ToInt() + contributionperiod.ToInt() + customerPlanDetail.StudentAge.GetValueOrDefault()).ToString());
                ReportParameter param30 = new ReportParameter("lastline", Generalmethods.getScholarLastline(_prospect, new Random().Next(1, 20), customerPlanDetail.PClass.ToString().ToCharArray()[0]));
                ReportParameter param39 = new ReportParameter("class", customerPlanDetail.PClass.ToString());
                ReportParameter param32 = new ReportParameter("premiumamount", customerPlanDetail.PremiumAmount.ToFormatCurrency());
                ReportParameter param35 = new ReportParameter("AnnualPremium", txtsumannualpremium.ToDecimal().ToFormatCurrency());
                ReportParameter param40 = new ReportParameter("investmentprofile", customerPlanDetail.InvestmentProfile);
                ReportParameter param120 = new ReportParameter("date", getspanishmonth(Int32.Parse(DateTime.Now.ToString("MM"))) + " " + DateTime.Now.ToString("dd, yyyy"));

                paramlist.Add(param120);
                paramlist.Add(param25);
                paramlist.Add(param26);
                paramlist.Add(param27);
                paramlist.Add(param28);
                paramlist.Add(param29);
                paramlist.Add(param30);
                paramlist.Add(param39);
                paramlist.Add(param32);
                paramlist.Add(param35);
                paramlist.Add(param40);
            }

            IGrowthdata[] grdata = insmain.getAssumedGrowthdata();
            ReportParameter param50 = new ReportParameter("prrateU", (grdata[Productdata.PROFILEU].growthRate).ToPercent());
            ReportParameter param51 = new ReportParameter("pramountU", grdata[Productdata.PROFILEU].growthAmount.ToFormatNumeric());
            paramlist.Add(param50);
            paramlist.Add(param51);

            ReportParameter param52 = new ReportParameter("prrateM", (grdata[Productdata.PROFILEM].growthRate).ToPercent());
            ReportParameter param53 = new ReportParameter("pramountM", grdata[Productdata.PROFILEM].growthAmount.ToFormatNumeric());
            paramlist.Add(param52);
            paramlist.Add(param53);

            ReportParameter param54 = new ReportParameter("prrateB", (grdata[Productdata.PROFILEB].growthRate).ToPercent());
            ReportParameter param55 = new ReportParameter("pramountB", grdata[Productdata.PROFILEB].growthAmount.ToFormatNumeric());
            paramlist.Add(param54);
            paramlist.Add(param55);

            ReportParameter param56 = new ReportParameter("prrateG", (grdata[Productdata.PROFILEG].growthRate).ToPercent());
            ReportParameter param57 = new ReportParameter("pramountG", grdata[Productdata.PROFILEG].growthAmount.ToFormatNumeric());
            paramlist.Add(param56);
            paramlist.Add(param57);

            var egr_age = oIllusDataManager.GetEgrAge();

            var egr_slide7 = oIllusDataManager.GetEgrSlide7();

            var egr_slide8 = oIllusDataManager.GetEgrSlide8();

            var egr_slide9 = oIllusDataManager.GetEgrSlide9();

            var egr_slide10 = oIllusDataManager.GetEgrSlide10();

            var egr_slide5 = Getegr_slide5DataTable();
            egr_slide5.Rows.Add("Princeton University", "33.000", "9.600", "5%", "42.117", "12.252", 1);
            egr_slide5.Rows.Add("California Institute of Technology", "34.337", "10.146", "5%", "43.823", "12.949", 2);
            egr_slide5.Rows.Add("Harvard University", "32.557", "11.402", "5%", "41.551", "14.092", 3);
            egr_slide5.Rows.Add("Swarthmore College", "36.154", "11.314", "5%", "46.142", "14.439", 4);
            egr_slide5.Rows.Add("Williams College", "37.400", "10.130", "5%", "47.732", "12.928", 5);
            egr_slide5.Rows.Add("USA Military College", "Publico", "Publico", "-", "Publico", "Publico", 6);
            egr_slide5.Rows.Add("Amherst College", "21.729", "8.114", "5%", "27.732", "10.355", 7);
            egr_slide5.Rows.Add("Wellesley College", "36.404", "11.336", "5%", "46.461", "14.467", 8);
            egr_slide5.Rows.Add("Yale University", "36.500", "11.000", "5%", "46.584", "14.039", 9);
            egr_slide5.Rows.Add("Columbia University", "37.470", "11.386", "5%", "47.822", "15.106", 10);

            ReportDataSource slide3 = new ReportDataSource("Charts_egr_age", egr_age);
            ReportDataSource slide7 = new ReportDataSource("Charts_egr_slide7", egr_slide7);
            ReportDataSource slide8 = new ReportDataSource("Charts_egr_slide8", egr_slide8);
            ReportDataSource slide9 = new ReportDataSource("Charts_egr_slide9", egr_slide9);
            ReportDataSource slide10 = new ReportDataSource("Charts_egr_slide10", egr_slide10);
            ReportDataSource slide5 = new ReportDataSource("Charts_egr_slide5", (DataTable)egr_slide5);

            _reportViewer.LocalReport.DataSources.Add(slide3);
            _reportViewer.LocalReport.DataSources.Add(slide7);
            _reportViewer.LocalReport.DataSources.Add(slide8);
            _reportViewer.LocalReport.DataSources.Add(slide9);
            _reportViewer.LocalReport.DataSources.Add(slide10);
            _reportViewer.LocalReport.DataSources.Add(slide5);
            _reportViewer.LocalReport.DataSources.Add(slide3);

            _reportViewer.LocalReport.EnableExternalImages = true;
            _reportViewer.LocalReport.SetParameters(paramlist);
            _reportViewer.LocalReport.DataSources.Add(rds);
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            _reportBinary = this._reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        }

        public void showIllustrationREDfixed(DataTable dt, IMaineduretirefixed insmain,
            Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetail customerPlanDetail,
            Entity.UnderWriting.IllusData.Illustrator.CustomerDetail customer)
        {
            this._reportViewer.LocalReport.Refresh();
            this._reportViewer.Reset();

            if (ProductCode == Utility.ProductBehavior.Horizon.Code())
                this._reportViewer.LocalReport.ReportPath = _reportPath + "Horizon_Long.rdlc";
            else if (ProductCode == Utility.ProductBehavior.EduPlan.Code())
                this._reportViewer.LocalReport.ReportPath = _reportPath + "Eduplan_long.rdlc";



            ReportDataSource rds = new ReportDataSource();
            rds.Name = "illustratorDataSet_illushorizon";
            if (dt.Rows.Count >= 16 && dt.Rows.Count <= 20 || dt.Rows.Count >= 36 && dt.Rows.Count <= 40 ||
                dt.Rows.Count >= 56 && dt.Rows.Count <= 60 || dt.Rows.Count >= 76 && dt.Rows.Count <= 80 ||
                dt.Rows.Count >= 96 && dt.Rows.Count <= 100)
            {
                if (ProductCode == Utility.ProductBehavior.EduPlan.Code())
                {
                    if (dt.Rows.Count >= 16 && dt.Rows.Count <= 20 || dt.Rows.Count >= 36 && dt.Rows.Count <= 40 ||
                        dt.Rows.Count >= 56 && dt.Rows.Count <= 60 || dt.Rows.Count >= 76 && dt.Rows.Count <= 80 ||
                        dt.Rows.Count >= 96 && dt.Rows.Count <= 100)
                    {
                        DataRow myRow;
                        myRow = dt.NewRow();
                        if (dt.Rows.Count == 16 || dt.Rows.Count == 36 || dt.Rows.Count == 56 || dt.Rows.Count == 76 || dt.Rows.Count == 96)
                        {
                            dt.Rows.Add("", "", "", "", "", "", "", "");
                            dt.Rows.Add("", "", "", "", "", "", "", "");
                            dt.Rows.Add("", "", "", "", "", "", "", "");
                        }
                        else if (dt.Rows.Count == 17 || dt.Rows.Count == 37 || dt.Rows.Count == 57 || dt.Rows.Count == 77 || dt.Rows.Count == 97 || dt.Rows.Count == 18 || dt.Rows.Count == 38 || dt.Rows.Count == 58 || dt.Rows.Count == 78 || dt.Rows.Count == 98)
                            dt.Rows.Add("", "", "", "", "", "", "", "");

                        if (!(dt.Rows.Count == 20 || dt.Rows.Count == 40 || dt.Rows.Count == 60 || dt.Rows.Count == 80))
                            dt.Rows.Add("", "", "", "", "", "", "", "");

                        dt.Rows.Add(myRow);
                        rds.Value = dt;
                    }
                }

                else if (ProductCode == Utility.ProductBehavior.Horizon.Code())
                {
                    DataRow myRow;
                    myRow = dt.NewRow();
                    if (dt.Rows.Count == 16 || dt.Rows.Count == 36 || dt.Rows.Count == 56 || dt.Rows.Count == 76 || dt.Rows.Count == 96)
                    {
                        dt.Rows.Add("", "", "", "", "", "", "");
                        dt.Rows.Add("", "", "", "", "", "", "");
                        dt.Rows.Add("", "", "", "", "", "", "");
                    }
                    else if (dt.Rows.Count == 17 || dt.Rows.Count == 37 || dt.Rows.Count == 57 || dt.Rows.Count == 77 || dt.Rows.Count == 97)
                    {
                        dt.Rows.Add("", "", "", "", "", "", "");
                        dt.Rows.Add("", "", "", "", "", "", "");
                    }
                    else if (dt.Rows.Count == 18 || dt.Rows.Count == 38 || dt.Rows.Count == 58 || dt.Rows.Count == 78 || dt.Rows.Count == 98)
                        dt.Rows.Add("", "", "", "", "", "", "");

                    if (!(dt.Rows.Count == 20 || dt.Rows.Count == 40 || dt.Rows.Count == 60 || dt.Rows.Count == 80))
                        dt.Rows.Add("", "", "", "", "", "", "", "");

                    dt.Rows.Add(myRow);
                    rds.Value = dt;
                }
            }
            rds.Value = dt;

            string defermentperiod = "-";
            string contributionperiod = "-";
            string initialcontributionamount = "-";
            string intialcontribution = "-";
            string txtsumannualpremium = "-";
            string ageatretirement = "-";
            string risk = "-";
            string almillar = "-";
            string plantype = "-";
            string maritalstatus = "-";
            string freqofpayment = "-";

            if (customerPlanDetail != null)
            {
                if (customerPlanDetail.FrequencyTypeCode != null)
                    freqofpayment = Resources.ResourceManager.GetString(customerPlanDetail.FrequencyType);

                if (customerPlanDetail.DefermentPeriod != null)
                    defermentperiod = customerPlanDetail.DefermentPeriod.ToString();

                if (customerPlanDetail.ContributionPeriod != null)
                    contributionperiod = customerPlanDetail.ContributionPeriod.ToString();


                txtsumannualpremium = (customerPlanDetail.PremiumAmount * customerPlanDetail.Frequency.GetValueOrDefault()).ToString();

                if (customerPlanDetail.InitialContribution != null)
                    if (customerPlanDetail.InitialContribution > 0)
                    {
                        initialcontributionamount = customerPlanDetail.InitialContribution.ToString();
                        intialcontribution = "YES";
                    }
                    else
                    {
                        initialcontributionamount = "0";
                        intialcontribution = "NO";
                    }

                ageatretirement = (customerPlanDetail.DefermentPeriod.ToInt() + customerPlanDetail.ContributionPeriod.ToInt() + customer.Age.ToInt()).ToString();

                risk = Resources.ResourceManager.GetString(customerPlanDetail.ActivityRiskType);

                almillar = Resources.ResourceManager.GetString(customerPlanDetail.HealthRiskType);

                plantype = Resources.ResourceManager.GetString(customerPlanDetail.PlanType);

                maritalstatus = Resources.ResourceManager.GetString(customer.MaritalStatus);

            }
            string primaryreq = "";

            var exams = oIllusDataManager.GetCustomerPlanExam(CustomerPlanNo.GetValueOrDefault(), Insuredtypes.PRIMARY);
            String[] req = new String[12];

            int i = 0;
            foreach (var exam in exams)
            {
                primaryreq = Resources.ResourceManager.GetString(exam.ExamName).ToUpper() + "/" + primaryreq;
                req[i] = Resources.ResourceManager.GetString(exam.ExamName);
                i++;
            }

            string tempPrimary = " ";
            int lno1 = 0;

            for (int j = 0; j < req.Length; j++)
                if (!string.IsNullOrEmpty(req[j]))
                    if (!req[j].Trim().Equals(""))
                    {
                        tempPrimary += req[j];
                        if (j != (req.Length - 1))
                            tempPrimary += "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t";
                        if (tempPrimary.Length > (lno1 + 1) * 150)
                        {
                            tempPrimary += Environment.NewLine;
                            lno1 = lno1 + 1;
                        }
                    }

            List<ReportParameter> paramlist = new List<ReportParameter>();
            if (ProductCode == Utility.ProductBehavior.Horizon.Code() || ProductCode == Utility.ProductBehavior.EduPlan.Code())
            {
                ReportParameter param100 = new ReportParameter("PolicyholderExams1", "" + tempPrimary);
                paramlist.Add(param100);

            }
            _prospect = (((customer.FirstName + " " + customer.MiddleName).Trim() + " " + customer.LastName).Trim() + " " + customer.LastName2).Trim();
            const int MaxLengthHeading = 30;
            const int MaxLengthName = 50;
            var custHeading = (customer.FirstName + " " + customer.LastName).Trim();
            var custName = (customer.FirstName + " " + customer.LastName).Trim();
            if (custHeading.Length > MaxLengthHeading)
            {
                custHeading = custHeading.Substring(0, MaxLengthHeading);
                custHeading = custHeading + "...";
            }
            if (custName.Length > MaxLengthName)
            {
                custName = custName.Substring(0, MaxLengthName);
                custName = custName + "...";
            }

            ReportParameter param1 = new ReportParameter("heading", custHeading);

            ReportParameter param3 = new ReportParameter("name", custName);
            ReportParameter param4 = new ReportParameter("periodofcontribution", customerPlanDetail.ContributionPeriod + "");
            ReportParameter param5 = new ReportParameter("amountofcontribution", customerPlanDetail.InitialContribution.ToFormatCurrency());
            ReportParameter param6 = new ReportParameter("withdrawalperiod", customerPlanDetail.RetirementPeriod + "");
            ReportParameter param7 = new ReportParameter("withdrawalamount", customerPlanDetail.AnnuityAmount.ToFormatCurrency());
            ReportParameter param8 = new ReportParameter("plantype", plantype + "");
            ReportParameter param9 = new ReportParameter("age", customer.Age + "");
            ReportParameter param10 = new ReportParameter("gender", customer.GenderCode.ToString());
            ReportParameter param11 = new ReportParameter("smoker", customer.Smoker == "Y" ? Resources.YesLabel : "No");

            ReportParameter param12 = new ReportParameter("ageatretirement", ageatretirement);
            ReportParameter param13 = new ReportParameter("risk", risk + "");
            ReportParameter param14 = new ReportParameter("almillar", almillar + "");
            ReportParameter param15 = new ReportParameter("maritalstatus", maritalstatus);
            ReportParameter param16 = new ReportParameter("plan", customerPlanDetail.Product);
            ReportParameter param17 = new ReportParameter("deferralperiod", customerPlanDetail.DefermentPeriod + "");
            var country = Utility.GetIllusDropDownByType(Utility.DropDownType.Country)
         .Single(o => o.CountryNo == customer.ResCountryNo).CountryName;
            ReportParameter param22 = new ReportParameter("country", Resources.ResourceManager.GetString(country));
            ReportParameter param18 = new ReportParameter("frequencyofcontribution", freqofpayment);
            ReportParameter param19 = new ReportParameter("initialcontribution", intialcontribution);
            ReportParameter param20 = new ReportParameter("primatarget", customerPlanDetail.TargetPremium.ToFormatCurrency());
            ReportParameter param23 = new ReportParameter("totalamount", (customerPlanDetail.AnnuityAmount * customerPlanDetail.RetirementPeriod).ToFormatCurrency());
            ReportParameter param44 = new ReportParameter("InitialContributionAmount", initialcontributionamount.ToDouble().ToFormatCurrency());
            ReportParameter param46 = new ReportParameter("Examinations", "-");
            ReportParameter param47 = new ReportParameter("BottomText", "Esta presentación tiene una validez de 15 días hábiles y en ningún caso más allá del" + " 31-Diciembre-" + DateTime.Now.Year.ToString());
            ReportParameter param48 = new ReportParameter("number", _numberPageIllus);
            ReportParameter param120_1 = new ReportParameter("date", getspanishmonth(Int32.Parse(DateTime.Now.ToString("MM"))) + " " + DateTime.Now.ToString("dd, yyyy"));
            paramlist.Add(param120_1);
            ReportParameter prmcalcular = new ReportParameter("calcular", Resources.ResourceManager.GetString(customerPlanDetail.CalculateType));
            paramlist.Add(prmcalcular);
            ReportParameter param135 = new ReportParameter("lastname", customer.LastName.ToString());
            paramlist.Add(param135);
            paramlist.Add(param1);
            paramlist.Add(param3);
            paramlist.Add(param4);
            paramlist.Add(param5);
            paramlist.Add(param6);
            paramlist.Add(param7);
            paramlist.Add(param8);
            paramlist.Add(param9);
            paramlist.Add(param10);
            paramlist.Add(param11);
            paramlist.Add(param12);
            paramlist.Add(param13);
            paramlist.Add(param14);
            paramlist.Add(param15);
            paramlist.Add(param16);
            paramlist.Add(param17);
            paramlist.Add(param18);
            paramlist.Add(param19);
            paramlist.Add(param20);
            paramlist.Add(param22);
            paramlist.Add(param23);
            paramlist.Add(param44);
            paramlist.Add(param46);
            paramlist.Add(param47);
            paramlist.Add(param48);

            string studentname = "-";
            if (!string.IsNullOrEmpty(customerPlanDetail.StudentName))
            {
                if (!customerPlanDetail.StudentName.Trim().Equals(""))
                {
                    studentname = customerPlanDetail.StudentName;
                }
            }

            string studentage = "-";
            if (customerPlanDetail.StudentAge.GetValueOrDefault() != 0)
                studentage = customerPlanDetail.StudentAge.ToString();

            String[] clientsign = new string[3];
            String[] agentsign = new string[3];

            clientsign[0] = "N";
            clientsign[1] = "N";
            clientsign[2] = "N";

            agentsign[0] = "N";
            agentsign[1] = "N";
            agentsign[2] = "N";

            ReportParameter Cpath1 = null;
            ReportParameter Apath1 = null;
            ReportParameter Cpath2 = null;
            ReportParameter Apath2 = null;
            ReportParameter Cpath3 = null;
            ReportParameter Apath3 = null;

            try
            {

                var isig = oIllusDataManager.GetIllustrationSignature(CustomerPlanNo.GetValueOrDefault()).First();
                clientsign[0] = isig.CustomerSign1.ToString();
                clientsign[1] = isig.CustomerSign2.ToString();
                clientsign[2] = isig.CustomerSign3.ToString();

                agentsign[0] = isig.AgentSign1.ToString();
                agentsign[1] = isig.AgentSign2.ToString();
                agentsign[2] = isig.AgentSign3.ToString();

            }
            catch (Exception ex)
            {
            }

            if (clientsign[0].Equals("Y"))
                Cpath1 = new ReportParameter("clientsignpath1", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_01C.jpg");
            else
                Cpath1 = new ReportParameter("clientsignpath1", _reportSignaturePath + "empty.jpg");

            if (agentsign[0].Equals("Y"))
                Apath1 = new ReportParameter("agentsignpath1", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_01A.jpg");
            else
                Apath1 = new ReportParameter("agentsignpath1", _reportSignaturePath + "empty.jpg");

            if (clientsign[1].Equals("Y"))
                Cpath2 = new ReportParameter("clientsignpath2", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_02C.jpg");
            else
                Cpath2 = new ReportParameter("clientsignpath2", _reportSignaturePath + "empty.jpg");

            if (agentsign[1].Equals("Y"))
                Apath2 = new ReportParameter("agentsignpath2", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_02A.jpg");
            else
                Apath2 = new ReportParameter("agentsignpath2", _reportSignaturePath + "empty.jpg");

            if (clientsign[2].Equals("Y"))
                Cpath3 = new ReportParameter("clientsignpath3", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_03C.jpg");
            else
                Cpath3 = new ReportParameter("clientsignpath3", _reportSignaturePath + "empty.jpg");

            if (agentsign[2].Equals("Y"))
                Apath3 = new ReportParameter("agentsignpath3", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_03A.jpg");
            else
                Apath3 = new ReportParameter("agentsignpath3", _reportSignaturePath + "empty.jpg");

            paramlist.Add(Cpath1);
            paramlist.Add(Apath1);
            paramlist.Add(Cpath2);
            paramlist.Add(Apath2);
            paramlist.Add(Cpath3);
            paramlist.Add(Apath3);

            var rpt_investment = oIllusDataManager.GetRptInvestmentsInflacion();

            var count = rpt_investment.Count();
            var inflation_Last = (from ms in rpt_investment
                                  select new { PequenasAcciones = ms.Pequenas_Acciones, GrandesAcciones = ms.Grandes_Acciones, PapelesdelTesoro = ms.Papelesdel_Tesoro, BonosdelGobierno = ms.Bonosdel_Gobierno, ms.Inflacion }).Last();
            var inflation_first = (from ms in rpt_investment
                                   select new { PequenasAcciones = ms.Pequenas_Acciones, GrandesAcciones = ms.Grandes_Acciones, PapelesdelTesoro = ms.Papelesdel_Tesoro, BonosdelGobierno = ms.Bonosdel_Gobierno, ms.Inflacion }).First();
            var percent_Pequenas_last = inflation_Last.PequenasAcciones;
            var percent_Pequenas_First = inflation_first.PequenasAcciones;
            var Grandes_Acciones_last = inflation_Last.GrandesAcciones;
            var Grandes_Acciones_First = inflation_first.GrandesAcciones;
            var Papelesdel_Tesoro_last = inflation_Last.PapelesdelTesoro;
            var Papelesdel_Tesoro_First = inflation_first.PapelesdelTesoro;
            var Bonosdel_Gobierno_last = inflation_Last.BonosdelGobierno;
            var Bonosdel_Gobierno_First = inflation_first.BonosdelGobierno;
            var Inflacion1_last = inflation_Last.Inflacion;
            var Inflacion1_First = inflation_first.Inflacion;


            var result_total = percent_Pequenas_last / percent_Pequenas_First;
            var numberofyears = 0.0119;
            var Pequenas_percent = (Math.Pow(Convert.ToDouble(result_total), numberofyears) - 1) * 100;

            var result1_total = Grandes_Acciones_last / Grandes_Acciones_First;
            var numberofyears1 = 0.0119;
            var Grandes_percent = (Math.Pow(Convert.ToDouble(result1_total), numberofyears1) - 1) * 100;

            var result2_total = Papelesdel_Tesoro_last / Papelesdel_Tesoro_First;
            var numberofyears2 = 0.0119;
            var Papelesdel_percent = (Math.Pow(Convert.ToDouble(result2_total), numberofyears2) - 1) * 100;

            var result3_total = Bonosdel_Gobierno_last / Bonosdel_Gobierno_First;
            var numberofyears3 = 0.0119;
            var Bonosdel_percent = (Math.Pow(Convert.ToDouble(result3_total), numberofyears3) - 1) * 100;

            var result4_total = Inflacion1_last / Inflacion1_First;
            var numberofyears4 = 0.0119;
            var Inflacion_percent = (Math.Pow(Convert.ToDouble(result4_total), numberofyears4) - 1) * 100;


            ReportParameter Pequenas_Acciones = new ReportParameter("Pequenas_Acciones", Pequenas_percent.ToString());
            ReportParameter Grandes = new ReportParameter("Grandes", Grandes_percent.ToString());
            ReportParameter Papelesdel_Tesoro = new ReportParameter("Papelesdel_Tesoro", Papelesdel_percent.ToString());
            ReportParameter Bonosdel_Gobierno = new ReportParameter("Bonosdel_Gobierno", Bonosdel_percent.ToString());
            ReportParameter Inflacion = new ReportParameter("Inflacion", Inflacion_percent.ToString());
            paramlist.Add(Pequenas_Acciones);
            paramlist.Add(Grandes);
            paramlist.Add(Papelesdel_Tesoro);
            paramlist.Add(Bonosdel_Gobierno);
            paramlist.Add(Inflacion);

            if (!String.IsNullOrEmpty(_checkedSettings) || !String.IsNullOrEmpty(_unCheckedSettings))
            {
                string[] checkedSettings = _checkedSettings.NTrim().TrimStart(',').Split(',');
                string[] unCheckedSettings = _unCheckedSettings.NTrim().TrimStart(',').Split(',');

                for (int counter1 = 0; counter1 < checkedSettings.Count(); counter1++)
                {
                    ReportParameter visibilityParam = new ReportParameter(checkedSettings[counter1].ToString(), "checked");
                    paramlist.Add(visibilityParam);
                }

                for (int counter2 = 0; counter2 < unCheckedSettings.Count(); counter2++)
                {
                    if (!(unCheckedSettings[counter2].ToString() == null || unCheckedSettings[counter2].ToString() == ""))
                    {
                        ReportParameter visibilityParam = new ReportParameter(unCheckedSettings[counter2].ToString(), "unchecked");
                        paramlist.Add(visibilityParam);
                    }
                }
            }
            ReportParameter param308 = new ReportParameter("S308", "unchecked");
            paramlist.Add(param308);
            var lstInvestmentMaster = oIllusDataManager.GetRptInvestmentsCompassMaster();
            var lstInvestmentsCompass = oIllusDataManager.GetRptInvestmentsCompass(0);

            var st_rpt_compass_investment_master_moderado = lstInvestmentMaster.Where(o => o.ReturnType == "Moderado" && o.Region == "Americano/Internacional");
            ReportDataSource rds_rpt_compss_investment_master_moderado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_moderado);

            var st_rpt_compass_investment_distribution_moderado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 1);
            ReportDataSource rds_rpt_compass_investment_distribution_moderado = new ReportDataSource("Charts_rpt_compass_investment_details_MODERADO", st_rpt_compass_investment_distribution_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_moderado);

            var st_rpt_compass_investment_master_balanceado = lstInvestmentMaster.Where(o => o.ReturnType == "Balanceado" && o.Region == "Americano/Internacional");
            ReportDataSource rds_rpt_compss_investment_master_balanceado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_balanceado);

            var st_rpt_compass_investment_distribution_balanceado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 2);
            ReportDataSource rds_rpt_compass_investment_distribution_balanceado = new ReportDataSource("Charts_rpt_compass_investment_details_BALANCEADO", st_rpt_compass_investment_distribution_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_balanceado);

            var st_rpt_compass_investment_master_cricimiento = lstInvestmentMaster.Where(o => o.ReturnType == "Crecimiento" && o.Region == "Americano/Internacional");
            ReportDataSource rds_rpt_compss_investment_master_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_cricimiento);

            var st_rpt_compass_investment_distribution_cricimiento = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 3);
            ReportDataSource rds_rpt_compass_investment_distribution_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_details_CRICIMIENTO", st_rpt_compass_investment_distribution_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_cricimiento);

            var st_rpt_compass_investment_master_euro_moderado = lstInvestmentMaster.Where(o => o.ReturnType == "Moderado" && o.Region == "Europeo");
            ReportDataSource rds_rpt_compss_investment_master_euro_moderado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_euro_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_euro_moderado);

            var st_rpt_compass_investment_distribution_euro_moderado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 4);
            ReportDataSource rds_rpt_compass_investment_distribution_euro_moderado = new ReportDataSource("Charts_rpt_compass_investment_details_euro_MODERADO", st_rpt_compass_investment_distribution_euro_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_euro_moderado);

            var st_rpt_compass_investment_master_euro_balanceado = lstInvestmentMaster.Where(o => o.ReturnType == "Balanceado" && o.Region == "Europeo");
            ReportDataSource rds_rpt_compss_investment_master_euro_balanceado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_euro_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_euro_balanceado);

            var st_rpt_compass_investment_distribution_euro_balanceado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 5);
            ReportDataSource rds_rpt_compass_investment_distribution_euro_balanceado = new ReportDataSource("Charts_rpt_compass_investment_details_euro_BALANCEADO", st_rpt_compass_investment_distribution_euro_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_euro_balanceado);

            var st_rpt_compass_investment_master_euro_cricimiento = lstInvestmentMaster.Where(o => o.ReturnType == "Crecimiento" && o.Region == "Europeo");
            ReportDataSource rds_rpt_compss_investment_master_euro_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_euro_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_euro_cricimiento);

            var st_rpt_compass_investment_distribution_euro_cricimiento = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 6);
            ReportDataSource rds_rpt_compass_investment_distribution_euro_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_details_euro_CRICIMIENTO", st_rpt_compass_investment_distribution_euro_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_euro_cricimiento);

            var lst_rpt_compass_investment_yearreturn_americano =
           (from item in lstInvestmentsCompass
            join o in lstInvestmentMaster
            on item.ReturnTypeid equals o.Sno
            where (item.Years == 2010 && o.Region == "Americano/Internacional")
            select item).ToList();

            ReportDataSource rds_rpt_compass_investment_yearreturn_americano = new ReportDataSource("Charts_rpt_compass_investment_details_yearreturn_Americano", lst_rpt_compass_investment_yearreturn_americano);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_yearreturn_americano);
            var lst_rpt_compass_investment_yearreturn_europeo =
            (from item in lstInvestmentsCompass
             join o in lstInvestmentMaster
             on item.ReturnTypeid equals o.Sno
             where (item.Years == 2010 && o.Region == "Europeo")
             select item).ToList();

            ReportDataSource rds_rpt_compass_investment_yearreturn_europeo = new ReportDataSource("Charts_rpt_compass_investment_details_yearreturn_Europeo", lst_rpt_compass_investment_yearreturn_europeo);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_yearreturn_europeo);

            var lst_rpt_profile_de_inversion = oIllusDataManager.GetInvestmentsProfile();

            var lst_rpt_profile_de_inversion_euro = oIllusDataManager.GetInvestmentsProfileEuro();

            ReportDataSource rds_profile_de_inversion = new ReportDataSource("Charts_profile_de_inversion", lst_rpt_profile_de_inversion);
            _reportViewer.LocalReport.DataSources.Add(rds_profile_de_inversion);
            ReportDataSource rds_profile_de_inversion_euro = new ReportDataSource("Charts_profile_de_inversion_euro", lst_rpt_profile_de_inversion_euro);
            _reportViewer.LocalReport.DataSources.Add(rds_profile_de_inversion_euro);

            var lstInvestmentType = oIllusDataManager.GetRptInvestmentsType(null, null, null);

            var lst_rpt_invest_distribution_Moderado = lstInvestmentType.Where(o => o.FundCategory == "Moderado" && o.Region == "Americano/Internacional");

            var lst_rpt_invest_distribution_Balanceado = lstInvestmentType.Where(o => o.FundCategory == "Balanceado" && o.Region == "Americano/Internacional");

            var lst_rpt_invest_distribution_Crecimiento = lstInvestmentType.Where(o => o.FundCategory == "Crecimiento" && o.Region == "Americano/Internacional");

            ReportDataSource rds_invest_distribution_Moderado = new ReportDataSource("Charts_rpt_InvestType_MODERADO", lst_rpt_invest_distribution_Moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_distribution_Moderado);
            ReportDataSource rds_invest_distribution_Balanceado = new ReportDataSource("Charts_rpt_InvestType_BALANCEADO", lst_rpt_invest_distribution_Balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_distribution_Balanceado);
            ReportDataSource rds_invest_distribution_Crecimiento = new ReportDataSource("Charts_rpt_InvestType_CRECIMIENTO", lst_rpt_invest_distribution_Crecimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_distribution_Crecimiento);
            var lst_rpt_invest_euro_distribution_Moderado = lstInvestmentType.Where(o => o.FundCategory == "Moderado" && o.Region == "Europeo");

            ReportDataSource rds_invest_euro_distribution_Moderado = new ReportDataSource("Charts_rpt_euro_InvestType_MODERADO", lst_rpt_invest_euro_distribution_Moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_euro_distribution_Moderado);
            var lst_rpt_invest_euro_distribution_Balanceado = lstInvestmentType.Where(o => o.FundCategory == "Balanceado" && o.Region == "Europeo");

            ReportDataSource rds_invest_euro_distribution_Balanceado = new ReportDataSource("Charts_rpt_euro_InvestType_BALANCEADO", lst_rpt_invest_euro_distribution_Balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_euro_distribution_Balanceado);
            var lst_rpt_invest_euro_distribution_Crecimiento = lstInvestmentType.Where(o => o.FundCategory == "Crecimiento" && o.Region == "Europeo");

            ReportDataSource rds_invest_euro_distribution_Crecimiento = new ReportDataSource("Charts_rpt_euro_InvestType_CRECIMIENTO", lst_rpt_invest_euro_distribution_Crecimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_euro_distribution_Crecimiento);

            var totMonderadoBond = from mb in lst_rpt_invest_distribution_Moderado
                                   where mb.FundType == "Bond" && mb.FundCategory == "Moderado"
                                   group mb by mb.FundCategory into g
                                   select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var totMonderadoStock = from ms in lst_rpt_invest_distribution_Moderado
                                    where ms.FundType == "Stock" && ms.FundCategory == "Moderado"
                                    group ms by ms.FundCategory into g
                                    select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter prmMonderadoBondShare = new ReportParameter("MonderadoBondShare", totMonderadoBond.First().total.ToString());
            paramlist.Add(prmMonderadoBondShare);
            ReportParameter prmMonderadoStockShare = new ReportParameter("MonderadoStockShare", totMonderadoStock.First().total.ToString());
            paramlist.Add(prmMonderadoStockShare);
            var totBalanceadoBond = from mb in lst_rpt_invest_distribution_Balanceado
                                    where mb.FundType == "Bond" && mb.FundCategory == "Balanceado"
                                    group mb by mb.FundCategory into g
                                    select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var totBalanceadoStock = from ms in lst_rpt_invest_distribution_Balanceado
                                     where ms.FundType == "Stock" && ms.FundCategory == "Balanceado"
                                     group ms by ms.FundCategory into g
                                     select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter prmBalanceadoBondShare = new ReportParameter("BalanceadoBondShare", totBalanceadoBond.First().total.ToString());
            paramlist.Add(prmBalanceadoBondShare);
            ReportParameter prmBalanceadoStockShare = new ReportParameter("BalanceadoStockShare", totBalanceadoStock.First().total.ToString());
            paramlist.Add(prmBalanceadoStockShare);

            var totCrecimientoBond = from mb in lst_rpt_invest_distribution_Crecimiento
                                     where mb.FundType == "Bond" && mb.FundCategory == "Crecimiento"
                                     group mb by mb.FundCategory into g
                                     select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var totCrecimientoStock = from ms in lst_rpt_invest_distribution_Crecimiento
                                      where ms.FundType == "Stock" && ms.FundCategory == "Crecimiento"
                                      group ms by ms.FundCategory into g
                                      select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            string shareCrecimientoBond;
            if (totCrecimientoBond.FirstOrDefault() != null)
                shareCrecimientoBond = Convert.ToString(totCrecimientoBond.FirstOrDefault().total);
            else
                shareCrecimientoBond = "0";

            ReportParameter prmCrecimientoBondShare = new ReportParameter("CrecimientoBondShare", shareCrecimientoBond);
            paramlist.Add(prmCrecimientoBondShare);
            ReportParameter prmCrecimientoStockShare = new ReportParameter("CrecimientoStockShare", totCrecimientoStock.First().total.ToString());
            paramlist.Add(prmCrecimientoStockShare);

            var euro_totBalanceadoBond = from mb in lst_rpt_invest_euro_distribution_Balanceado
                                         where mb.FundType == "Bond" && mb.FundCategory == "Balanceado"
                                         group mb by mb.FundCategory into g
                                         select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var euro_totBalanceadoStock = from ms in lst_rpt_invest_euro_distribution_Balanceado
                                          where ms.FundType == "Stock" && ms.FundCategory == "Balanceado"
                                          group ms by ms.FundCategory into g
                                          select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter euro_prmBalanceadoBondShare = new ReportParameter("euro_BalanceadoBondShare", euro_totBalanceadoBond.First().total.ToString());
            paramlist.Add(euro_prmBalanceadoBondShare);
            ReportParameter euro_prmBalanceadoStockShare = new ReportParameter("euro_BalanceadoStockShare", euro_totBalanceadoStock.First().total.ToString());
            paramlist.Add(euro_prmBalanceadoStockShare);

            var euro_totMonderadoBond = from mb in lst_rpt_invest_euro_distribution_Moderado
                                        where mb.FundType == "Bond" && mb.FundCategory == "Moderado"
                                        group mb by mb.FundCategory into g
                                        select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var euro_totMonderadoStock = from ms in lst_rpt_invest_euro_distribution_Moderado
                                         where ms.FundType == "Stock" && ms.FundCategory == "Moderado"
                                         group ms by ms.FundCategory into g
                                         select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter euro_prmMonderadoBondShare = new ReportParameter("euro_MonderadoBondShare", euro_totMonderadoBond.First().total.ToString());
            paramlist.Add(euro_prmMonderadoBondShare);
            ReportParameter euro_prmMonderadoStockShare = new ReportParameter("euro_MonderadoStockShare", euro_totMonderadoStock.First().total.ToString());
            paramlist.Add(euro_prmMonderadoStockShare);

            var euro_totCrecimientoBond = from mb in lst_rpt_invest_distribution_Crecimiento
                                          where mb.FundType == "Bond" && mb.FundCategory == "Crecimiento"
                                          group mb by mb.FundCategory into g
                                          select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var euro_totCrecimientoStock = from ms in lst_rpt_invest_distribution_Crecimiento
                                           where ms.FundType == "Stock" && ms.FundCategory == "Crecimiento"
                                           group ms by ms.FundCategory into g
                                           select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            string euro_shareCrecimientoBond;
            if (totCrecimientoBond.FirstOrDefault() != null)
                euro_shareCrecimientoBond = Convert.ToString(euro_totCrecimientoBond.FirstOrDefault().total);
            else
                euro_shareCrecimientoBond = "0";

            ReportParameter euro_prmCrecimientoBondShare = new ReportParameter("euro_CrecimientoBondShare", euro_shareCrecimientoBond);
            paramlist.Add(euro_prmCrecimientoBondShare);
            ReportParameter euro_prmCrecimientoStockShare = new ReportParameter("euro_CrecimientoStockShare", euro_totCrecimientoStock.First().total.ToString());
            paramlist.Add(euro_prmCrecimientoStockShare);

            this._reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            if (ProductCode == Utility.ProductBehavior.EduPlan.Code())
            {
                ReportParameter param25 = new ReportParameter("studentname", studentname + "");
                ReportParameter param26 = new ReportParameter("studentage", studentage + "");
                ReportParameter param27 = new ReportParameter("investmentprofile", customerPlanDetail.InvestmentProfile);
                ReportParameter param28 = new ReportParameter("agepension", "" + defermentperiod.ToLong() + contributionperiod.ToLong() + customerPlanDetail.StudentAge);
                ReportParameter param29 = new ReportParameter("lastline", Generalmethods.geteduplanLastline(_prospect, new Random().Next(1, 20), customerPlanDetail.@PClass[0]));
                ReportParameter param31 = new ReportParameter("class", customerPlanDetail.@PClass.ToString());
                ReportParameter param30 = new ReportParameter("AnnualPremium", txtsumannualpremium.ToDouble().ToFormatCurrency());
                ReportParameter param32 = new ReportParameter("premiumamount", customerPlanDetail.PremiumAmount.ToFormatCurrency());

                paramlist.Add(param25);
                paramlist.Add(param26);
                paramlist.Add(param27);
                paramlist.Add(param28);
                paramlist.Add(param29);
                paramlist.Add(param31);
                paramlist.Add(param30);
                paramlist.Add(param32);

                var egr_age = oIllusDataManager.GetEgrAge();

                var egr_slide7 = oIllusDataManager.GetEgrSlide7();

                var egr_slide8 = oIllusDataManager.GetEgrSlide8();

                var egr_slide9 = oIllusDataManager.GetEgrSlide9();

                var egr_slide10 = oIllusDataManager.GetEgrSlide10();

                var egr_slide5 = Getegr_slide5DataTable();
                egr_slide5.Rows.Add("Princeton University", "33.000", "9.600", "5%", "42.117", "12.252", 1);
                egr_slide5.Rows.Add("California Institute of Technology", "34.337", "10.146", "5%", "43.823", "12.949", 2);
                egr_slide5.Rows.Add("Harvard University", "32.557", "11.402", "5%", "41.551", "14.092", 3);
                egr_slide5.Rows.Add("Swarthmore College", "36.154", "11.314", "5%", "46.142", "14.439", 4);
                egr_slide5.Rows.Add("Williams College", "37.400", "10.130", "5%", "47.732", "12.928", 5);
                egr_slide5.Rows.Add("USA Military College", "Publico", "Publico", "-", "Publico", "Publico", 6);
                egr_slide5.Rows.Add("Amherst College", "21.729", "8.114", "5%", "27.732", "10.355", 7);
                egr_slide5.Rows.Add("Wellesley College", "36.404", "11.336", "5%", "46.461", "14.467", 8);
                egr_slide5.Rows.Add("Yale University", "36.500", "11.000", "5%", "46.584", "14.039", 9);
                egr_slide5.Rows.Add("Columbia University", "37.470", "11.386", "5%", "47.822", "15.106", 10);

                ReportDataSource slide3 = new ReportDataSource("Charts_egr_age", egr_age);
                ReportDataSource slide7 = new ReportDataSource("Charts_egr_slide7", egr_slide7);
                ReportDataSource slide8 = new ReportDataSource("Charts_egr_slide8", egr_slide8);
                ReportDataSource slide9 = new ReportDataSource("Charts_egr_slide9", egr_slide9);
                ReportDataSource slide10 = new ReportDataSource("Charts_egr_slide10", egr_slide10);
                ReportDataSource slide5 = new ReportDataSource("Charts_egr_slide5", (DataTable)egr_slide5); ///se convirtio a datatable por error con reportviewer 10

                _reportViewer.LocalReport.DataSources.Add(slide3);
                _reportViewer.LocalReport.DataSources.Add(slide7);
                _reportViewer.LocalReport.DataSources.Add(slide8);
                _reportViewer.LocalReport.DataSources.Add(slide9);
                _reportViewer.LocalReport.DataSources.Add(slide10);
                _reportViewer.LocalReport.DataSources.Add(slide5);
                _reportViewer.LocalReport.DataSources.Add(slide3);

            }
            if (ProductCode == Utility.ProductBehavior.Horizon.Code())
            {

                ReportParameter param28 = new ReportParameter("agepension", "" + defermentperiod.ToLong() + contributionperiod.ToLong() + customer.Age.ToLong());
                ReportParameter param29 = new ReportParameter("lastline", Generalmethods.getHorizonLastline(_prospect, new Random().Next(1, 20), customerPlanDetail.PClass.ToString().ToCharArray()[0]));
                ReportParameter param30 = new ReportParameter("AnnualPremium", txtsumannualpremium.ToDouble().ToFormatCurrency());
                ReportParameter param32 = new ReportParameter("premiumamount", customerPlanDetail.PremiumAmount.ToFormatCurrency());
                ReportParameter param27 = new ReportParameter("class", customerPlanDetail.PClass.ToString());
                ReportParameter param1_12 = new ReportParameter("heading1", (customer.LastName + " ").Trim());
                ReportParameter param24 = new ReportParameter("minprima", customerPlanDetail.TargetPremium.ToFormatCurrency());
                paramlist.Add(param1_12);
                paramlist.Add(param24);
                paramlist.Add(param28);
                paramlist.Add(param29);
                paramlist.Add(param30);
                paramlist.Add(param32);
                paramlist.Add(param27);

                var egr_age = oIllusDataManager.GetEgrAge();

                var rpt_axys_slide5 = oIllusDataManager.GetRptAxysSlide5();

                var rpt_axys_slide6 = oIllusDataManager.GetRptAxysSlide6();

                var rpt_axys_slide8 = oIllusDataManager.GetRptAxysSlide8();

                var rpt_Axys_slide10 = oIllusDataManager.GetRptAxysSlide10();

                ReportDataSource horizonslide4 = new ReportDataSource("Charts_egr_age", egr_age);
                ReportDataSource axysslide10 = new ReportDataSource("Charts_rpt_Axys_slide10", rpt_Axys_slide10);
                ReportDataSource axysslide5 = new ReportDataSource("Charts_rpt_axys_slide5", rpt_axys_slide5);
                ReportDataSource axysslide8 = new ReportDataSource("Charts_rpt_axys_slide8", rpt_axys_slide8);
                ReportDataSource axysslide6 = new ReportDataSource("Charts_rpt_axys_slide6", rpt_axys_slide6);

                _reportViewer.LocalReport.DataSources.Add(axysslide5);
                _reportViewer.LocalReport.DataSources.Add(axysslide6);
                _reportViewer.LocalReport.DataSources.Add(axysslide8);
                _reportViewer.LocalReport.DataSources.Add(axysslide10);
                _reportViewer.LocalReport.DataSources.Add(horizonslide4);
            }
            _reportViewer.LocalReport.EnableExternalImages = true;
            _reportViewer.LocalReport.SetParameters(paramlist);
            _reportViewer.LocalReport.DataSources.Add(rds);
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            _reportBinary = this._reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        }

        public void showIllustrationLife(DataTable dt, IMainInsuranceData insmain,
            Illustrator.CustomerPlanDetail customerPlanDetail,
            Illustrator.CustomerDetail customer)
        {

            this._reportViewer.LocalReport.Refresh();
            this._reportViewer.Reset();

            var partins = oIllusDataManager.GetCustomerPlanPartnerInsurance(CustomerPlanNo.GetValueOrDefault());

            List<string> planOptions = new List<string>();

            try
            {
                var suminsu = oIllusDataManager.GetCustomerPlanVarInsured(CustomerPlanNo.GetValueOrDefault()).FirstOrDefault();
                if (suminsu != null)
                    planOptions.Add("Suma Asegurada Variable");
            }
            catch (Exception ex)
            {
            }

            try
            {
                var investprofile = oIllusDataManager.GetCustomerPlanVarProfile(CustomerPlanNo.GetValueOrDefault()).FirstOrDefault();
                if (investprofile != null)
                    planOptions.Add("Perfil de Inversión Variable");
            }
            catch (Exception ex)
            {
            }

            try
            {
                var premiums = oIllusDataManager.GetCustomerPlanVarPremium(CustomerPlanNo.GetValueOrDefault()).FirstOrDefault();
                if (premiums != null)
                    planOptions.Add("Prima Variable");
            }
            catch (Exception ex)
            {
            }

            try
            {
                var partialSurrender = oIllusDataManager.GetCustomerPlanVarSurrender(CustomerPlanNo.GetValueOrDefault()).FirstOrDefault();
                if (partialSurrender != null)
                    planOptions.Add("Rescate Parcial");
            }
            catch (Exception ex) { }

            try
            {
                var loan = oIllusDataManager.GetCustomerPlanLoan(CustomerPlanNo.GetValueOrDefault()).FirstOrDefault();
                if (loan != null)
                    planOptions.Add("Prestamo");
            }
            catch (Exception ex) { }

            try
            {
                var loanRepay = oIllusDataManager.GetCustomerPlanLoanRepay(CustomerPlanNo.GetValueOrDefault()).FirstOrDefault();
                if (loanRepay != null)
                    planOptions.Add("Pago al Prestamo");
            }
            catch (Exception ex) { }

            string[] planOptionsArray = new string[6] { "", "", "", "", "", "" };

            int p = 0;
            foreach (string str in planOptions)
            {
                planOptionsArray[p] = str;
                p++;
            }

            ReportParameter paramPlanOption1 = new ReportParameter("PlanOption1", planOptionsArray[0]);
            ReportParameter paramPlanOption2 = new ReportParameter("PlanOption2", planOptionsArray[1]);
            ReportParameter paramPlanOption3 = new ReportParameter("PlanOption3", planOptionsArray[2]);
            ReportParameter paramPlanOption4 = new ReportParameter("PlanOption4", planOptionsArray[3]);
            ReportParameter paramPlanOption5 = new ReportParameter("PlanOption5", planOptionsArray[4]);
            ReportParameter paramPlanOption6 = new ReportParameter("PlanOption6", planOptionsArray[5]);

            ReportParameter paramOther = new ReportParameter("hideother", "Y");

            if (customerPlanDetail.RiderOir.ToString().Equals("Y"))
                paramOther = new ReportParameter("hideother", "N");

            if (ProductCode == Utility.ProductBehavior.Legacy.Code())
                this._reportViewer.LocalReport.ReportPath = _reportPath + ("Legacy_long.rdlc");
            else if (ProductCode == Utility.ProductBehavior.CompassIndex.Code())
                this._reportViewer.LocalReport.ReportPath = _reportPath + ("Compass_Index_long.rdlc");

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "illustratorDataSet_illusdet";
            if (dt.Rows.Count >= 17 && dt.Rows.Count <= 20 || dt.Rows.Count >= 37 && dt.Rows.Count <= 40 ||
                dt.Rows.Count >= 57 && dt.Rows.Count <= 60 || dt.Rows.Count >= 77 && dt.Rows.Count <= 80 ||
                dt.Rows.Count >= 97 && dt.Rows.Count <= 100)
            {
                DataRow myRow;
                myRow = dt.NewRow();
                var countdt = 0;
                if (dt.Rows.Count == 17 || dt.Rows.Count == 37 || dt.Rows.Count == 57 || dt.Rows.Count == 77 || dt.Rows.Count == 97)
                    countdt = 3;
                else if (dt.Rows.Count == 18 || dt.Rows.Count == 38 || dt.Rows.Count == 58 || dt.Rows.Count == 78 || dt.Rows.Count == 98)
                    countdt = 2;
                else if (dt.Rows.Count == 19 || dt.Rows.Count == 39 || dt.Rows.Count == 59 || dt.Rows.Count == 79 || dt.Rows.Count == 99)
                    countdt = 1;
                for (int c = 0; c < countdt; c++)
                    dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "");
                dt.Rows.Add(myRow);
                rds.Value = dt;
            }
            rds.Value = dt;
            const int MaxLengthHeading = 30;
            const int MaxLengthName = 50;
            var custHeading = (customer.FirstName + " " + customer.LastName).Trim();
            var custName = (customer.FirstName + " " + customer.LastName).Trim();
            if (custHeading.Length > MaxLengthHeading)
            {
                custHeading = custHeading.Substring(0, MaxLengthHeading);
                custHeading = custHeading + "...";
            }
            if (custName.Length > MaxLengthName)
            {
                custName = custName.Substring(0, MaxLengthName);
                custName = custName + "...";
            }

            ReportParameter param1 = new ReportParameter("heading", custHeading);
            ReportParameter param2 = new ReportParameter("per1", insmain.comprate1);
            ReportParameter param3 = new ReportParameter("per2", insmain.comprate2);
            ReportParameter param4 = new ReportParameter("per3", insmain.comprate3);
            ReportParameter param6 = new ReportParameter("header3", "Asumiendo un Rendimiento de Inversión de");
            ReportParameter param7 = new ReportParameter("dynrate", insmain.dynratecaption);
            ReportParameter param124 = new ReportParameter("number", _numberPageIllus);
            ReportParameter param1_13 = new ReportParameter("lastname", (customer.LastName + " ").Trim());

            string ddlfingoal = "-";
            string ddlinitialcontribution = "-";
            string initialcontributionamount = "-";
            string ddlinvestprofile = "-";
            string txtsumannualpremium = "-";
            string initialcomission = "-";
            string crticleillnessamount = "-";
            string freqofpayment = "-";
            string plantype = "-";
            string almillar = "-";
            string risk = "-";
            string investmentprofile = "-";
            string maritalstatus = "-";
            string fingoalamount = "-";

            if (customerPlanDetail != null)
            {
                if (customerPlanDetail.FrequencyTypeCode != null)
                    freqofpayment = Resources.ResourceManager.GetString(customerPlanDetail.FrequencyType);

                if (customerPlanDetail.FinancialGoal != null)
                    ddlfingoal = customerPlanDetail.FinancialGoal == "Y" ? "Yes" : "No";

                if (customerPlanDetail.InitialContribution > 0)
                {
                    ddlinitialcontribution = "Yes";
                    initialcontributionamount = customerPlanDetail.InitialContribution.ToString();
                }
                else
                    ddlinitialcontribution = "No";

                if (customerPlanDetail.InvestmentProfileCode != null)
                    investmentprofile = ddlinvestprofile = Resources.ResourceManager.GetString(customerPlanDetail.InvestmentProfile); ;

                txtsumannualpremium = customerPlanDetail.AnnualizedPremium.ToString();

                crticleillnessamount = customerPlanDetail.RiderCiAmount.ToString();

                plantype = Resources.ResourceManager.GetString(customerPlanDetail.PlanType);

                risk = Resources.ResourceManager.GetString(customerPlanDetail.ActivityRiskType);

                almillar = Resources.ResourceManager.GetString(customerPlanDetail.HealthRiskType);

                maritalstatus = Resources.ResourceManager.GetString(customer.MaritalStatus);

                fingoalamount = customerPlanDetail.FinancialGoalAmount.ToString();
            }

            initialcomission = customerPlanDetail.InitialCommission.ToString();

            string rideroiramt = "-";
            string rideroirname = "-";
            string Riesgo = "-";
            string Fumador = "-";
            string Sexo = "-";
            string HastalaEdadde = "-";
            string Edad = "-";
            string AlMillar1 = "-";

            if (partins != null && customerPlanDetail.RiderOir.Equals("Y"))
            {

                rideroiramt = partins.RideroirAmount.ToString();
                rideroirname = partins.FirstName + " " + partins.MiddleName + " " + partins.LastName + " " + partins.LastName2;

                var riskOir = Utility.GetIllusDropDownByType(Utility.DropDownType.ActivityRiskType).FirstOrDefault(o => o.ActivityRiskTypeNo == partins.ActivityRiskTypeNo);
                if (riskOir != null)
                    Riesgo = Resources.ResourceManager.GetString(riskOir.ActivityRiskType);

                Fumador = partins.Smoker == "Y" ? RESOURCE.UnderWriting.NewBussiness.Resources.YesLabel : "No";

                var genderOir = Utility.GetIllusDropDownByType(Utility.DropDownType.Gender).FirstOrDefault(o => o.GenderCode == partins.GenderCode);
                if (genderOir != null)
                    Sexo = genderOir.GenderName;

                if (partins.ContributionTypeCode.Equals(Contributiontypes.CONTINUOUS))
                    HastalaEdadde = "99";
                else if (partins.ContributionTypeCode.Equals(Contributiontypes.NUMBEROFYEARS))
                    HastalaEdadde = (Convert.ToInt32(partins.Age) + partins.UntilAge - 1).ToString();
                else if (partins.ContributionTypeCode.Equals(Contributiontypes.UNTILAGE))
                    HastalaEdadde = partins.UntilAge.ToString();

                Edad = partins.Age.ToString();

                var healthOir = Utility.GetIllusDropDownByType(Utility.DropDownType.HealthRiskType).FirstOrDefault(o => o.HealthRiskTypeNo == partins.HealthRiskTypeNo);
                if (healthOir != null)
                    AlMillar1 = Resources.ResourceManager.GetString(healthOir.HealthRiskType);
            }
            //primary
            string primaryreq = "-", otherreq = "-";

            var exams = oIllusDataManager.GetCustomerPlanExam(CustomerPlanNo.GetValueOrDefault(), Insuredtypes.PRIMARY);
            String[] req = new String[exams.Count()];

            int i = 0;
            foreach (var exam in exams)
            {
                primaryreq = exam.ExamName + "/" + primaryreq;
                req[i] = Resources.ResourceManager.GetString(exam.ExamName);
                i++;
            }

            string tempPrimary = " ";
            int lno1 = 0;

            for (int j = 0; j < req.Length; j++)
                if (!string.IsNullOrEmpty(req[j]))
                    if (!req[j].Trim().Equals(""))
                    {
                        tempPrimary += req[j];
                        if (j != (req.Length - 1))
                            tempPrimary += "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t";
                        if (tempPrimary.Length > (lno1 + 1) * 150)
                        {
                            tempPrimary += Environment.NewLine;
                            lno1 = lno1 + 1;
                        }

                    }
            //other

            exams = oIllusDataManager.GetCustomerPlanExam(CustomerPlanNo.GetValueOrDefault(), Insuredtypes.OTHER);
            String[] oreq = new String[exams.Count()];

            int k = 0;
            foreach (var exam in exams)
            {
                otherreq = exam.ExamName + "/" + primaryreq;
                oreq[k] = Resources.ResourceManager.GetString(exam.ExamName);
                k++;
            }

            List<ReportParameter> paramlist = new List<ReportParameter>();
            ReportParameter param8 = new ReportParameter("name", custName);
            ReportParameter param9 = new ReportParameter("age", customer.Age);
            ReportParameter param10 = new ReportParameter("gender", customer.GenderCode + "");
            ReportParameter param14 = new ReportParameter("smoker", customer.Smoker == "Y" ? RESOURCE.UnderWriting.NewBussiness.Resources.YesLabel : "No");

            ReportParameter param13 = new ReportParameter("maritalstatus", maritalstatus + "");
            var country = Utility.GetIllusDropDownByType(Utility.DropDownType.Country)
         .Single(o => o.CountryNo == customer.ResCountryNo).CountryName;
            ReportParameter param27 = new ReportParameter("country", country);
            ReportParameter param43 = new ReportParameter("InitialContributionAmount", initialcontributionamount.ToFormatCurrency());
            ReportParameter param47 = new ReportParameter("financialgoals", ddlfingoal);
            ReportParameter param48 = new ReportParameter("ValueAccount", fingoalamount.ToDouble().ToFormatCurrency());
            ReportParameter param49 = new ReportParameter("AlaAge", customerPlanDetail.FinancialGoalAge.ToString());

            ReportParameter param50 = new ReportParameter("PolicyholderExams", primaryreq);
            ReportParameter param51 = new ReportParameter("TestsSecondInsured", otherreq);

            ReportParameter param100 = new ReportParameter("PolicyholderExams1", "" + tempPrimary);
            ReportParameter param120_1 = new ReportParameter("date", getspanishmonth(Int32.Parse(DateTime.Now.ToString("MM"))) + " " + DateTime.Now.ToString("dd, yyyy"));
            paramlist.Add(param120_1);

            //primary

            string tempOther = " ";
            if (customerPlanDetail.RiderOir.ToString().Equals("Y"))
                for (int j = 0; j < oreq.Length; j++)
                    if (!string.IsNullOrEmpty(oreq[j]))
                        if (!oreq[j].Trim().Equals(""))
                            tempOther += oreq[j] + ", ";

            if (tempOther.Trim().Length > 0)
                tempOther = tempOther.Substring(0, tempOther.Length - 2);

            ReportParameter param112 = new ReportParameter("other1", "" + tempOther);
            paramlist.Add(param112);
            paramlist.Add(paramOther);
            paramlist.Add(param100);
            paramlist.Add(param1_13);
            paramlist.Add(param1);
            paramlist.Add(param2);
            paramlist.Add(param3);
            paramlist.Add(param4);
            paramlist.Add(param6);
            paramlist.Add(param7);
            paramlist.Add(param8);
            paramlist.Add(param9);
            paramlist.Add(param10);
            paramlist.Add(param13);
            paramlist.Add(param14);
            paramlist.Add(param27);
            paramlist.Add(param43);
            paramlist.Add(param47);
            paramlist.Add(param48);
            paramlist.Add(param49);
            paramlist.Add(param50);
            paramlist.Add(param51);
            paramlist.Add(param124);
            paramlist.Add(paramPlanOption1);
            paramlist.Add(paramPlanOption2);
            paramlist.Add(paramPlanOption3);
            paramlist.Add(paramPlanOption4);
            paramlist.Add(paramPlanOption5);
            paramlist.Add(paramPlanOption6);

            //calculating contribution period
            string contributionPeriod = "-";

            if (customerPlanDetail.ContributionTypeCode.Equals(Contributiontypes.CONTINUOUS))
                contributionPeriod = (99 - Convert.ToInt32(customer.Age) + 1).ToString();
            else if (customerPlanDetail.ContributionTypeCode.Equals(Contributiontypes.UNTILAGE))
                contributionPeriod = (customerPlanDetail.ContributionUntilAge - Convert.ToInt32(customer.Age) + 1).ToString();
            else if (customerPlanDetail.ContributionTypeCode.Equals(Contributiontypes.NUMBEROFYEARS))
                contributionPeriod = customerPlanDetail.ContributionPeriod.ToString();

            if (contributionPeriod.Equals("0"))
                contributionPeriod = "-";
            _prospect = (((customer.FirstName + " " + customer.MiddleName).Trim() + " " + customer.LastName).Trim() + " " + customer.LastName2).Trim();

            var rpt_Life_Expectancy = oIllusDataManager.GetRptLifeExpectancy();
            var termContributionType = Utility.GetIllusDropDownByType(Utility.DropDownType.ContributionType)
         .Single(o => o.ContributionTypeCode == customerPlanDetail.TermContributionTypeCode).ContributionType;
            if (ProductCode == Utility.ProductBehavior.Legacy.Code())
            {
                ReportParameter param15 = new ReportParameter("heading", custHeading);
                ReportParameter param11 = new ReportParameter("risk", risk + "");
                ReportParameter param20 = new ReportParameter("almillar", almillar + "");
                ReportParameter param12 = new ReportParameter("plan", customerPlanDetail.Product);
                ReportParameter param19 = new ReportParameter("calcular", customerPlanDetail.CalculateType);
                ReportParameter param18 = new ReportParameter("periodofcontribution", contributionPeriod);
                ReportParameter param16 = new ReportParameter("suminsured", (customerPlanDetail.InsuredAmount + customerPlanDetail.RiderTermAmount).ToFormatCurrency());
                ReportParameter paramsib = new ReportParameter("suminsuredbase", customerPlanDetail.InsuredAmount.ToFormatCurrency());

                ReportParameter param24 = new ReportParameter("initialcontribution", ddlinitialcontribution);
                ReportParameter param25 = new ReportParameter("annualpremium", txtsumannualpremium.ToFormatCurrency());

                ReportParameter param26 = new ReportParameter("investmentprofile", investmentprofile + "");
                ReportParameter param29 = new ReportParameter("accidentaldeath", customerPlanDetail.RiderAdbAmount.ToFormatCurrency());
                ReportParameter param29_1 = new ReportParameter("Accidental", customerPlanDetail.RiderAcdb == "Y" ? RESOURCE.UnderWriting.NewBussiness.Resources.YesLabel : "No");

                ReportParameter param21 = new ReportParameter("additionaltemporary", customerPlanDetail.RiderTermAmount.ToFormatCurrency());
                ReportParameter param22_1 = new ReportParameter("riderterm", customerPlanDetail.RiderTerm == "Y" ? RESOURCE.UnderWriting.NewBussiness.Resources.YesLabel : "No");
                ReportParameter param22_3 = new ReportParameter("TemporalAdicionaluntilage", termContributionType);

                ReportParameter param23 = new ReportParameter("primatarget", customerPlanDetail.TargetPremium.ToFormatCurrency());
                ReportParameter param28 = new ReportParameter("minprima", customerPlanDetail.MinimumPremium.ToFormatCurrency());

                ReportParameter param30 = new ReportParameter("lastline", Generalmethods.getLastline(_prospect, new Random().Next(1, 20), customerPlanDetail.PClass[0]));
                ReportParameter param31 = new ReportParameter("planincrement", " ");
                ReportParameter param32 = new ReportParameter("PerodoContribucinSemester", " ");

                ReportParameter param33 = new ReportParameter("rideroiramount", rideroiramt.ToFormatCurrency());
                ReportParameter param64 = new ReportParameter("rideroirname", rideroirname);
                ReportParameter param65 = new ReportParameter("Riesgo", Riesgo);
                ReportParameter param66 = new ReportParameter("Edad", Edad);
                ReportParameter param67 = new ReportParameter("Fumador", Fumador);
                ReportParameter param68 = new ReportParameter("Sexo", Sexo);
                ReportParameter param69 = new ReportParameter("HastalaEdadde", HastalaEdadde);
                ReportParameter param53 = new ReportParameter("AlMillar1", AlMillar1 + "");
                ReportParameter param70 = new ReportParameter("BottomText", "Esta presentación tiene una validez de 15 días hábiles y en ningún caso más allá del " + " 31-Diciembre-" + DateTime.Now.Year.ToString());
                ReportParameter param75 = new ReportParameter("criticleillness", crticleillnessamount.ToFormatCurrency());
                ReportParameter param76 = new ReportParameter("PaymentFrequency", freqofpayment);
                ReportParameter param77 = new ReportParameter("PlanType", plantype + "");
                ReportParameter param78 = new ReportParameter("premiumamount", customerPlanDetail.PremiumAmount.ToFormatCurrency());
                ReportParameter param79 = new ReportParameter("Actualparticipation", "-");
                ReportParameter param80 = new ReportParameter("Biannualrateguarante", "Tasa Bi-Anual Garantizada");
                ReportParameter param81 = new ReportParameter("Class", customerPlanDetail.PClass);
                ReportParameter param82 = new ReportParameter("Foryears", "Por Años");
                ReportParameter param83 = new ReportParameter("BottomText", "Esta presentación tiene una validez de 15 días hábiles y en ningún caso más allá del " + " 31-Diciembre-" + DateTime.Now.Year.ToString());
                ReportParameter param57 = new ReportParameter("Monto", "-");

                paramlist.Add(param57);
                paramlist.Add(paramsib);
                paramlist.Add(param33);
                paramlist.Add(param53);
                paramlist.Add(param64);
                paramlist.Add(param65);
                paramlist.Add(param66);
                paramlist.Add(param67);
                paramlist.Add(param68);
                paramlist.Add(param69);
                paramlist.Add(param70);
                paramlist.Add(param75);
                paramlist.Add(param76);
                paramlist.Add(param77);
                paramlist.Add(param78);
                paramlist.Add(param79);
                paramlist.Add(param80);
                paramlist.Add(param81);
                paramlist.Add(param82);
                paramlist.Add(param83);
                paramlist.Add(param7);
                paramlist.Add(param12);
                paramlist.Add(param15);
                paramlist.Add(param16);
                paramlist.Add(param18);
                paramlist.Add(param19);
                paramlist.Add(param20);
                paramlist.Add(param21);
                paramlist.Add(param23);
                paramlist.Add(param24);
                paramlist.Add(param25);
                paramlist.Add(param26);
                paramlist.Add(param28);
                paramlist.Add(param29);
                paramlist.Add(param11);
                paramlist.Add(param30);
                paramlist.Add(param31);
                paramlist.Add(param32);
                paramlist.Add(param22_1);
                paramlist.Add(param22_3);
                paramlist.Add(param29_1);

                ReportParameter paramActRisk = new ReportParameter("activityrisk", risk);
                ReportParameter paramHealthRisk = new ReportParameter("healthrisk", almillar);

                paramlist.Add(paramActRisk);
                paramlist.Add(paramHealthRisk);

                ReportParameter prmplanname = new ReportParameter("planname", customerPlanDetail.Product);
                ReportParameter prmplantype = new ReportParameter("plantype", plantype + "");

                paramlist.Add(prmplanname);
                paramlist.Add(prmplantype);

                if (partins != null)
                {
                    ReportParameter param22 = new ReportParameter("spouceinsurance", partins.RideroirAmount.ToFormatCurrency());
                    paramlist.Add(param22);
                }
                else
                {
                    ReportParameter param22 = new ReportParameter("spouceinsurance", "-");
                    paramlist.Add(param22);
                }


                decimal MaxAge = 0;
                if (customer.GenderCode == "M")
                    MaxAge = Convert.ToDecimal((from item in rpt_Life_Expectancy where item.Current_Age == 0 select item.Men).First());
                else if (customer.GenderCode == "F")
                    MaxAge = Convert.ToDecimal((from item in rpt_Life_Expectancy where item.Current_Age == 0 select item.Woman).First());

                ReportParameter param110 = new ReportParameter("LifeExpectancy", (MaxAge - Convert.ToDecimal(customer.Age)).ToString());
                paramlist.Add(param110);
                ReportDataSource lifeexpetancy = new ReportDataSource("Charts_rpt_lifeexpectancy", rpt_Life_Expectancy);
                _reportViewer.LocalReport.DataSources.Add(lifeexpetancy);
            }
            else if (ProductCode == Utility.ProductBehavior.CompassIndex.Code())
            {
                decimal MaxAge = 0;
                if (customer.GenderCode == "M")
                {
                    MaxAge = Convert.ToDecimal((from item in rpt_Life_Expectancy where item.Current_Age == 0 select item.Men).First());
                }
                else if (customer.GenderCode == "F")
                {
                    MaxAge = Convert.ToDecimal((from item in rpt_Life_Expectancy where item.Current_Age == 0 select item.Woman).First());
                }

                if (ProductCode == Utility.ProductBehavior.CompassIndex.Code()) { investmentprofile = "Indexado"; }

                ReportParameter param110 = new ReportParameter("LifeExpectancy", (MaxAge - Convert.ToDecimal(customer.Age)).ToString());
                paramlist.Add(param110);
                ReportDataSource lifeexpetancy = new ReportDataSource("Charts_rpt_lifeexpectancy", rpt_Life_Expectancy);
                _reportViewer.LocalReport.DataSources.Add(lifeexpetancy);

                ReportParameter param15 = new ReportParameter("heading", custHeading);
                ReportParameter param11 = new ReportParameter("risk", risk + "");
                ReportParameter param20 = new ReportParameter("almillar", almillar + "");
                ReportParameter param12 = new ReportParameter("plan", customerPlanDetail.Product);
                ReportParameter param19 = new ReportParameter("calcular", customerPlanDetail.CalculateType);
                ReportParameter param18 = new ReportParameter("periodofcontribution", contributionPeriod);
                ReportParameter param16 = new ReportParameter("suminsured", (customerPlanDetail.InsuredAmount + customerPlanDetail.RiderTermAmount).ToFormatCurrency());
                ReportParameter paramsib = new ReportParameter("suminsuredbase", customerPlanDetail.InsuredAmount.ToFormatCurrency());


                ReportParameter param24 = new ReportParameter("initialcontribution", ddlinitialcontribution);
                ReportParameter param25 = new ReportParameter("annualpremium", txtsumannualpremium.ToFormatCurrency());

                ReportParameter param26 = new ReportParameter("investmentprofile", investmentprofile + "");
                ReportParameter param29 = new ReportParameter("accidentaldeath", customerPlanDetail.RiderAdbAmount.ToFormatCurrency());
                ReportParameter param29_1 = new ReportParameter("Accidental", customerPlanDetail.RiderAcdb == "Y" ? RESOURCE.UnderWriting.NewBussiness.Resources.YesLabel : "No");

                ReportParameter param21 = new ReportParameter("additionaltemporary", customerPlanDetail.RiderTermAmount.ToFormatCurrency());
                ReportParameter param22_1 = new ReportParameter("riderterm", customerPlanDetail.RiderTerm == "Y" ? RESOURCE.UnderWriting.NewBussiness.Resources.YesLabel : "No");
                ReportParameter param22_3 = new ReportParameter("TemporalAdicionaluntilage", termContributionType);

                ReportParameter param23 = new ReportParameter("primatarget", customerPlanDetail.TargetPremium.ToFormatCurrency());
                ReportParameter param28 = new ReportParameter("minprima", customerPlanDetail.MinimumPremium.ToFormatCurrency());

                ReportParameter param31 = new ReportParameter("planincrement", " ");
                ReportParameter param32 = new ReportParameter("PerodoContribucinSemester", " ");

                ReportParameter param33 = new ReportParameter("rideroiramount", rideroiramt.ToFormatCurrency());
                ReportParameter param64 = new ReportParameter("rideroirname", rideroirname);
                ReportParameter param65 = new ReportParameter("Riesgo", Riesgo);
                ReportParameter param66 = new ReportParameter("Edad", Edad);
                ReportParameter param67 = new ReportParameter("Fumador", Fumador);
                ReportParameter param68 = new ReportParameter("Sexo", Sexo);
                ReportParameter param69 = new ReportParameter("HastalaEdadde", HastalaEdadde);
                ReportParameter param53 = new ReportParameter("AlMillar1", AlMillar1 + "");
                ReportParameter param70 = new ReportParameter("BottomText", "Esta presentación tiene una validez de 15 días hábiles y en ningún caso más allá del " + " 31-Diciembre-" + DateTime.Now.Year.ToString());
                ReportParameter param75 = new ReportParameter("criticleillness", crticleillnessamount.ToFormatCurrency());
                ReportParameter param76 = new ReportParameter("PaymentFrequency", freqofpayment);
                ReportParameter param77 = new ReportParameter("PlanType", plantype + "");
                ReportParameter param78 = new ReportParameter("premiumamount", customerPlanDetail.PremiumAmount.ToFormatCurrency());
                ReportParameter param79 = new ReportParameter("Actualparticipation", "-");
                ReportParameter param80 = new ReportParameter("Biannualrateguarante", "Tasa Bi-Anual Garantizada");
                ReportParameter param81 = new ReportParameter("Class", customerPlanDetail.PClass);
                ReportParameter param82 = new ReportParameter("Foryears", "Por Años");
                ReportParameter param83 = new ReportParameter("BottomText", "Esta presentación tiene una validez de 15 días hábiles y en ningún caso más allá del " + " 31-Diciembre-" + DateTime.Now.Year.ToString());

                ReportParameter param30 = null;

                if (ProductCode == Utility.ProductBehavior.CompassIndex.Code())
                    param30 = new ReportParameter("lastline", Generalmethods.getLastline(_prospect, new Random().Next(1, 20), customerPlanDetail.PClass[0]));

                paramlist.Add(paramsib);
                paramlist.Add(param33);
                paramlist.Add(param53);
                paramlist.Add(param64);
                paramlist.Add(param65);
                paramlist.Add(param66);
                paramlist.Add(param67);
                paramlist.Add(param68);
                paramlist.Add(param69);
                paramlist.Add(param70);
                paramlist.Add(param75);
                paramlist.Add(param76);
                paramlist.Add(param77);
                paramlist.Add(param78);
                paramlist.Add(param79);
                paramlist.Add(param80);
                paramlist.Add(param81);
                paramlist.Add(param82);
                paramlist.Add(param83);
                paramlist.Add(param7);
                paramlist.Add(param12);
                paramlist.Add(param16);
                paramlist.Add(param18);
                paramlist.Add(param19);
                paramlist.Add(param20);
                paramlist.Add(param21);
                paramlist.Add(param23);
                paramlist.Add(param24);
                paramlist.Add(param25);
                paramlist.Add(param26);
                paramlist.Add(param28);
                paramlist.Add(param29);
                paramlist.Add(param11);
                paramlist.Add(param30);
                paramlist.Add(param31);
                paramlist.Add(param32);
                paramlist.Add(param22_1);
                paramlist.Add(param22_3);
                paramlist.Add(param29_1);

                if (partins != null)
                {
                    ReportParameter param22 = new ReportParameter("spouceinsurance", partins.RideroirAmount.ToFormatCurrency());
                    paramlist.Add(param22);
                }
                else
                {
                    ReportParameter param22 = new ReportParameter("spouceinsurance", "-");
                    paramlist.Add(param22);
                }
            }

            String[] clientsign = new string[3];
            String[] agentsign = new string[3];

            clientsign[0] = "N";
            clientsign[1] = "N";
            clientsign[2] = "N";

            agentsign[0] = "N";
            agentsign[1] = "N";
            agentsign[2] = "N";

            ReportParameter Cpath1 = null;
            ReportParameter Apath1 = null;
            ReportParameter Cpath2 = null;
            ReportParameter Apath2 = null;
            ReportParameter Cpath3 = null;
            ReportParameter Apath3 = null;

            try
            {
                var isig = oIllusDataManager.GetIllustrationSignature(CustomerPlanNo.GetValueOrDefault()).First();

                clientsign[0] = isig.CustomerSign1;
                clientsign[1] = isig.CustomerSign2;
                clientsign[2] = isig.CustomerSign3;

                agentsign[0] = isig.AgentSign1;
                agentsign[1] = isig.AgentSign2;
                agentsign[2] = isig.AgentSign3;
            }
            catch (Exception ex)
            {
            }

            if (clientsign[0].Equals("Y"))
                Cpath1 = new ReportParameter("clientsignpath1", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_01C.jpg");
            else
                Cpath1 = new ReportParameter("clientsignpath1", _reportSignaturePath + "empty.jpg");

            if (agentsign[0].Equals("Y"))
                Apath1 = new ReportParameter("agentsignpath1", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_01A.jpg");
            else
                Apath1 = new ReportParameter("agentsignpath1", _reportSignaturePath + "empty.jpg");

            if (clientsign[1].Equals("Y"))
                Cpath2 = new ReportParameter("clientsignpath2", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_02C.jpg");
            else
                Cpath2 = new ReportParameter("clientsignpath2", _reportSignaturePath + "empty.jpg");

            if (agentsign[1].Equals("Y"))
                Apath2 = new ReportParameter("agentsignpath2", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_02A.jpg");
            else
                Apath2 = new ReportParameter("agentsignpath2", _reportSignaturePath + "empty.jpg");

            if (clientsign[2].Equals("Y"))
                Cpath3 = new ReportParameter("clientsignpath3", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_03C.jpg");
            else
                Cpath3 = new ReportParameter("clientsignpath3", _reportSignaturePath + "empty.jpg");

            if (agentsign[2].Equals("Y"))
                Apath3 = new ReportParameter("agentsignpath3", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_03A.jpg");
            else
                Apath3 = new ReportParameter("agentsignpath3", _reportSignaturePath + "empty.jpg");

            paramlist.Add(Cpath1);
            paramlist.Add(Apath1);
            paramlist.Add(Cpath2);
            paramlist.Add(Apath2);
            paramlist.Add(Cpath3);
            paramlist.Add(Apath3);

            var rpt_investment = oIllusDataManager.GetRptInvestmentsInflacion();

            var count = rpt_investment.Count();
            var inflation_Last = (from ms in rpt_investment
                                  select new { ms.Pequenas_Acciones, ms.Grandes_Acciones, ms.Papelesdel_Tesoro, ms.Bonosdel_Gobierno, ms.Inflacion }).Last();
            var inflation_first = (from ms in rpt_investment
                                   select new { ms.Pequenas_Acciones, ms.Grandes_Acciones, ms.Papelesdel_Tesoro, ms.Bonosdel_Gobierno, ms.Inflacion }).First();
            var percent_Pequenas_last = inflation_Last.Pequenas_Acciones;
            var percent_Pequenas_First = inflation_first.Pequenas_Acciones;
            var Grandes_Acciones_last = inflation_Last.Grandes_Acciones;
            var Grandes_Acciones_First = inflation_first.Grandes_Acciones;
            var Papelesdel_Tesoro_last = inflation_Last.Papelesdel_Tesoro;
            var Papelesdel_Tesoro_First = inflation_first.Papelesdel_Tesoro;
            var Bonosdel_Gobierno_last = inflation_Last.Bonosdel_Gobierno;
            var Bonosdel_Gobierno_First = inflation_first.Bonosdel_Gobierno;
            var Inflacion1_last = inflation_Last.Inflacion;
            var Inflacion1_First = inflation_first.Inflacion;

            var result_total = percent_Pequenas_last / percent_Pequenas_First;
            var numberofyears = 0.0119;
            var Pequenas_percent = (Math.Pow(Convert.ToDouble(result_total), numberofyears) - 1) * 100;

            var result1_total = Grandes_Acciones_last / Grandes_Acciones_First;
            var numberofyears1 = 0.0119;
            var Grandes_percent = (Math.Pow(Convert.ToDouble(result1_total), numberofyears1) - 1) * 100;

            var result2_total = Papelesdel_Tesoro_last / Papelesdel_Tesoro_First;
            var numberofyears2 = 0.0119;
            var Papelesdel_percent = (Math.Pow(Convert.ToDouble(result2_total), numberofyears2) - 1) * 100;

            var result3_total = Bonosdel_Gobierno_last / Bonosdel_Gobierno_First;
            var numberofyears3 = 0.0119;
            var Bonosdel_percent = (Math.Pow(Convert.ToDouble(result3_total), numberofyears3) - 1) * 100;

            var result4_total = Inflacion1_last / Inflacion1_First;
            var numberofyears4 = 0.0119;
            var Inflacion_percent = (Math.Pow(Convert.ToDouble(result4_total), numberofyears4) - 1) * 100;

            ReportParameter Pequenas_Acciones = new ReportParameter("Pequenas_Acciones", Pequenas_percent.ToString());
            ReportParameter Grandes = new ReportParameter("Grandes", Grandes_percent.ToString());
            ReportParameter Papelesdel_Tesoro = new ReportParameter("Papelesdel_Tesoro", Papelesdel_percent.ToString());
            ReportParameter Bonosdel_Gobierno = new ReportParameter("Bonosdel_Gobierno", Bonosdel_percent.ToString());
            ReportParameter Inflacion = new ReportParameter("Inflacion", Inflacion_percent.ToString());
            paramlist.Add(Pequenas_Acciones);
            paramlist.Add(Grandes);
            paramlist.Add(Papelesdel_Tesoro);
            paramlist.Add(Bonosdel_Gobierno);
            paramlist.Add(Inflacion);

            if (!String.IsNullOrEmpty(_checkedSettings) || !String.IsNullOrEmpty(_unCheckedSettings))
            {
                string[] checkedSettings = _checkedSettings.NTrim().TrimStart(',').Split(',');
                string[] unCheckedSettings = _unCheckedSettings.NTrim().TrimStart(',').Split(',');

                for (int counter1 = 0; counter1 < checkedSettings.Count(); counter1++)
                {
                    ReportParameter visibilityParam = new ReportParameter(checkedSettings[counter1].ToString(), "checked");
                    paramlist.Add(visibilityParam);
                }

                for (int counter2 = 0; counter2 < unCheckedSettings.Count(); counter2++)
                {
                    if (!(unCheckedSettings[counter2].ToString() == null || unCheckedSettings[counter2].ToString() == ""))
                    {
                        ReportParameter visibilityParam = new ReportParameter(unCheckedSettings[counter2].ToString(), "unchecked");
                        paramlist.Add(visibilityParam);
                    }
                }
            }

            ReportParameter param308 = new ReportParameter("S308", "unchecked");
            paramlist.Add(param308);
            var rpt_Compass_Slide5 = oIllusDataManager.GetRptCompassSlide5();
            var Compass_slide7 = oIllusDataManager.RptCompassSlide7();

            foreach (var item in Compass_slide7)
            {
                if (item.Continent == "Europa")
                {
                    ReportParameter param60 = new ReportParameter("Europe_Deaths", item.Deaths.ToString() + " muertes");
                    ReportParameter param61 = new ReportParameter("Europe_Area", item.Area.ToString());
                    paramlist.Add(param60);
                    paramlist.Add(param61);
                }
                else if (item.Continent == "Mediterráneo Oriental")
                {
                    ReportParameter param62 = new ReportParameter("Eastern_Mediterranean_Deaths", item.Deaths.ToString() + " muertes");
                    ReportParameter param63 = new ReportParameter("Eastern_Mediterranean_Area", item.Area.ToString());
                    paramlist.Add(param62);
                    paramlist.Add(param63);
                }
                else if (item.Continent == "Pacífico Occidental")
                {
                    ReportParameter param72 = new ReportParameter("Pacífico_Occidental_Deaths", item.Deaths.ToString() + " muertes");
                    ReportParameter param73 = new ReportParameter("Pacífico_Occidental_Area", item.Area.ToString());
                    paramlist.Add(param72);
                    paramlist.Add(param73);
                }
                else if (item.Continent == "Asia Sur Oriental")
                {
                    ReportParameter param74 = new ReportParameter("Asia_Deaths", item.Deaths.ToString() + " muertes");//Activityrisktypes.getActivityrisktype(Numericdata.getIntegervalue(customerPlanDetail.activityrisktypeno.ToString())));
                    ReportParameter param105 = new ReportParameter("Asia_Area", item.Area.ToString());//Activityrisktypes.getActivityrisktype(Numericdata.getIntegervalue(customerPlanDetail.activityrisktypeno.ToString())));
                    paramlist.Add(param74);
                    paramlist.Add(param105);
                }
                else if (item.Continent == "Centro y Suramérica")
                {
                    ReportParameter param106 = new ReportParameter("america_Deaths", item.Deaths.ToString() + " muertes");//Activityrisktypes.getActivityrisktype(Numericdata.getIntegervalue(customerPlanDetail.activityrisktypeno.ToString())));
                    ReportParameter param107 = new ReportParameter("america_Area", item.Area.ToString());//Activityrisktypes.getActivityrisktype(Numericdata.getIntegervalue(customerPlanDetail.activityrisktypeno.ToString())));
                    paramlist.Add(param106);
                    paramlist.Add(param107);
                }
                else if (item.Continent == "Africa")
                {
                    ReportParameter param108 = new ReportParameter("Africa_Deaths", item.Deaths.ToString() + " muertes");//Activityrisktypes.getActivityrisktype(Numericdata.getIntegervalue(customerPlanDetail.activityrisktypeno.ToString())));
                    ReportParameter param109 = new ReportParameter("Africa_Area", item.Area.ToString());//Activityrisktypes.getActivityrisktype(Numericdata.getIntegervalue(customerPlanDetail.activityrisktypeno.ToString())));
                    paramlist.Add(param108);
                    paramlist.Add(param109);
                }
            }
            ReportDataSource Compassslide7 = new ReportDataSource("Charts_rpt_Compass_slide7", Compass_slide7);

            ReportDataSource Compassslide5 = new ReportDataSource("Charts_rpt_Compass_Slide5", rpt_Compass_Slide5);

            _reportViewer.LocalReport.DataSources.Add(Compassslide5);
            _reportViewer.LocalReport.DataSources.Add(Compassslide7);
            string untilAge = "-";

            if (customerPlanDetail.RiderTerm.Equals("Y"))
            {
                if (customerPlanDetail.TermContributionTypeCode.Equals(Contributiontypes.CONTINUOUS))
                    untilAge = "99";
                else if (customerPlanDetail.TermContributionTypeCode.Equals(Contributiontypes.NUMBEROFYEARS))
                    untilAge = (Convert.ToInt32(customer.Age) + customerPlanDetail.RiderTermUntilAge - 1).ToString();
                else if (customerPlanDetail.TermContributionTypeCode.Equals(Contributiontypes.UNTILAGE))
                    untilAge = customerPlanDetail.RiderTermUntilAge.ToString();
            }

            ReportParameter paramAdditionalTermUntilAge = new ReportParameter("AdditionalTermUntilAge", untilAge);
            paramlist.Add(paramAdditionalTermUntilAge);

            maritalstatus = "-";

            if (customerPlanDetail.RiderOir.Equals("Y") && partins != null)
                maritalstatus = Utility.GetIllusDropDownByType(Utility.DropDownType.MaritalStatus)
             .Single(o => o.MaritalStatusCode == partins.MaritalStatusCode).MaritalStatus;

            ReportParameter paramSpouseMaritalStatus = new ReportParameter("SpouseMaritalStatus", maritalstatus);
            //*****************Adding Parameters of Sub Report*********************//

            this._reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

            var lstInvestmentType = oIllusDataManager.GetRptInvestmentsType(null, null, null);

            var lst_rpt_invest_distribution_Moderado = lstInvestmentType.Where(o => o.FundCategory == "Moderado" && o.Region == "Americano/Internacional");

            var totMonderadoBond = from mb in lst_rpt_invest_distribution_Moderado
                                   where mb.FundType == "Bond" && mb.FundCategory == "Moderado"
                                   group mb by mb.FundCategory into g
                                   select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var totMonderadoStock = from ms in lst_rpt_invest_distribution_Moderado
                                    where ms.FundType == "Stock" && ms.FundCategory == "Moderado"
                                    group ms by ms.FundCategory into g
                                    select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter prmMonderadoBondShare = new ReportParameter("MonderadoBondShare", totMonderadoBond.First().total.ToString());
            paramlist.Add(prmMonderadoBondShare);
            ReportParameter prmMonderadoStockShare = new ReportParameter("MonderadoStockShare", totMonderadoStock.First().total.ToString());
            paramlist.Add(prmMonderadoStockShare);

            var lst_rpt_invest_distribution_Balanceado = lstInvestmentType.Where(o => o.FundCategory == "Balanceado" && o.Region == "Americano/Internacional");

            var totBalanceadoBond = from mb in lst_rpt_invest_distribution_Balanceado
                                    where mb.FundType == "Bond" && mb.FundCategory == "Balanceado"
                                    group mb by mb.FundCategory into g
                                    select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var totBalanceadoStock = from ms in lst_rpt_invest_distribution_Balanceado
                                     where ms.FundType == "Stock" && ms.FundCategory == "Balanceado"
                                     group ms by ms.FundCategory into g
                                     select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter prmBalanceadoBondShare = new ReportParameter("BalanceadoBondShare", totBalanceadoBond.First().total.ToString());
            paramlist.Add(prmBalanceadoBondShare);
            ReportParameter prmBalanceadoStockShare = new ReportParameter("BalanceadoStockShare", totBalanceadoStock.First().total.ToString());
            paramlist.Add(prmBalanceadoStockShare);

            var lst_rpt_invest_distribution_Crecimiento = lstInvestmentType.Where(o => o.FundCategory == "Crecimiento" && o.Region == "Americano/Internacional");

            var totCrecimientoBond = from mb in lst_rpt_invest_distribution_Crecimiento
                                     where mb.FundType == "Bond" && mb.FundCategory == "Crecimiento"
                                     group mb by mb.FundCategory into g
                                     select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var totCrecimientoStock = from ms in lst_rpt_invest_distribution_Crecimiento
                                      where ms.FundType == "Stock" && ms.FundCategory == "Crecimiento"
                                      group ms by ms.FundCategory into g
                                      select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            string shareCrecimientoBond;
            if (totCrecimientoBond.FirstOrDefault() != null)
                shareCrecimientoBond = Convert.ToString(totCrecimientoBond.FirstOrDefault().total);
            else
                shareCrecimientoBond = "0";

            ReportParameter prmCrecimientoBondShare = new ReportParameter("CrecimientoBondShare", shareCrecimientoBond);
            paramlist.Add(prmCrecimientoBondShare);
            ReportParameter prmCrecimientoStockShare = new ReportParameter("CrecimientoStockShare", totCrecimientoStock.First().total.ToString());
            paramlist.Add(prmCrecimientoStockShare);
            var lst_rpt_invest_euro_distribution_Moderado = lstInvestmentType.Where(o => o.FundCategory == "Moderado" && o.Region == "Europeo");

            var euro_totMonderadoBond = from mb in lst_rpt_invest_euro_distribution_Moderado
                                        where mb.FundType == "Bond" && mb.FundCategory == "Moderado"
                                        group mb by mb.FundCategory into g
                                        select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var euro_totMonderadoStock = from ms in lst_rpt_invest_euro_distribution_Moderado
                                         where ms.FundType == "Stock" && ms.FundCategory == "Moderado"
                                         group ms by ms.FundCategory into g
                                         select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter euro_prmMonderadoBondShare = new ReportParameter("euro_MonderadoBondShare", euro_totMonderadoBond.First().total.ToString());
            paramlist.Add(euro_prmMonderadoBondShare);
            ReportParameter euro_prmMonderadoStockShare = new ReportParameter("euro_MonderadoStockShare", euro_totMonderadoStock.First().total.ToString());
            paramlist.Add(euro_prmMonderadoStockShare);
            var lst_rpt_invest_euro_distribution_Balanceado = lstInvestmentType.Where(o => o.FundCategory == "Balanceado" && o.Region == "Europeo");

            var euro_totBalanceadoBond = from mb in lst_rpt_invest_euro_distribution_Balanceado
                                         where mb.FundType == "Bond" && mb.FundCategory == "Balanceado"
                                         group mb by mb.FundCategory into g
                                         select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var euro_totBalanceadoStock = from ms in lst_rpt_invest_euro_distribution_Balanceado
                                          where ms.FundType == "Stock" && ms.FundCategory == "Balanceado"
                                          group ms by ms.FundCategory into g
                                          select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter euro_prmBalanceadoBondShare = new ReportParameter("euro_BalanceadoBondShare", euro_totBalanceadoBond.First().total.ToString());
            paramlist.Add(euro_prmBalanceadoBondShare);
            ReportParameter euro_prmBalanceadoStockShare = new ReportParameter("euro_BalanceadoStockShare", euro_totBalanceadoStock.First().total.ToString());
            paramlist.Add(euro_prmBalanceadoStockShare);

            //************************Adding Subreport to Master Report*******************//
            var lst_rpt_invest_euro_distribution_Crecimiento = lstInvestmentType.Where(o => o.FundCategory == "Crecimiento" && o.Region == "Europeo");

            var euro_totCrecimientoBond = from mb in lst_rpt_invest_distribution_Crecimiento
                                          where mb.FundType == "Bond" && mb.FundCategory == "Crecimiento"
                                          group mb by mb.FundCategory into g
                                          select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var euro_totCrecimientoStock = from ms in lst_rpt_invest_distribution_Crecimiento
                                           where ms.FundType == "Stock" && ms.FundCategory == "Crecimiento"
                                           group ms by ms.FundCategory into g
                                           select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            var lstInvestmentMaster = oIllusDataManager.GetRptInvestmentsCompassMaster();
            var lstInvestmentsCompass = oIllusDataManager.GetRptInvestmentsCompass(0);

            var st_rpt_compass_investment_master_moderado = lstInvestmentMaster.Where(o => o.ReturnType == "Moderado" && o.Region == "Americano/Internacional");
            ReportDataSource rds_rpt_compss_investment_master_moderado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_moderado);

            var st_rpt_compass_investment_distribution_moderado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 1);
            ReportDataSource rds_rpt_compass_investment_distribution_moderado = new ReportDataSource("Charts_rpt_compass_investment_details_MODERADO", st_rpt_compass_investment_distribution_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_moderado);

            var st_rpt_compass_investment_master_balanceado = lstInvestmentMaster.Where(o => o.ReturnType == "Balanceado" && o.Region == "Americano/Internacional");
            ReportDataSource rds_rpt_compss_investment_master_balanceado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_balanceado);

            var st_rpt_compass_investment_distribution_balanceado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 2);
            ReportDataSource rds_rpt_compass_investment_distribution_balanceado = new ReportDataSource("Charts_rpt_compass_investment_details_BALANCEADO", st_rpt_compass_investment_distribution_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_balanceado);

            var st_rpt_compass_investment_master_cricimiento = lstInvestmentMaster.Where(o => o.ReturnType == "Crecimiento" && o.Region == "Americano/Internacional");
            ReportDataSource rds_rpt_compss_investment_master_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_cricimiento);

            var st_rpt_compass_investment_distribution_cricimiento = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 3);
            ReportDataSource rds_rpt_compass_investment_distribution_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_details_CRICIMIENTO", st_rpt_compass_investment_distribution_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_cricimiento);

            var st_rpt_compass_investment_master_euro_moderado = lstInvestmentMaster.Where(o => o.ReturnType == "Moderado" && o.Region == "Europeo");
            ReportDataSource rds_rpt_compss_investment_master_euro_moderado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_euro_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_euro_moderado);

            var st_rpt_compass_investment_distribution_euro_moderado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 4);
            ReportDataSource rds_rpt_compass_investment_distribution_euro_moderado = new ReportDataSource("Charts_rpt_compass_investment_details_euro_MODERADO", st_rpt_compass_investment_distribution_euro_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_euro_moderado);

            var st_rpt_compass_investment_master_euro_balanceado = lstInvestmentMaster.Where(o => o.ReturnType == "Balanceado" && o.Region == "Europeo");
            ReportDataSource rds_rpt_compss_investment_master_euro_balanceado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_euro_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_euro_balanceado);

            var st_rpt_compass_investment_distribution_euro_balanceado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 5);
            ReportDataSource rds_rpt_compass_investment_distribution_euro_balanceado = new ReportDataSource("Charts_rpt_compass_investment_details_euro_BALANCEADO", st_rpt_compass_investment_distribution_euro_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_euro_balanceado);

            var st_rpt_compass_investment_master_euro_cricimiento = lstInvestmentMaster.Where(o => o.ReturnType == "Crecimiento" && o.Region == "Europeo");
            ReportDataSource rds_rpt_compss_investment_master_euro_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_euro_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_euro_cricimiento);

            var st_rpt_compass_investment_distribution_euro_cricimiento = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 6);
            ReportDataSource rds_rpt_compass_investment_distribution_euro_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_details_euro_CRICIMIENTO", st_rpt_compass_investment_distribution_euro_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_euro_cricimiento);

            List<Illustrator.InvestmentsCompass> lst_rpt_compass_investment_yearreturn_americano =
                (from item in lstInvestmentsCompass
                 join o in lstInvestmentMaster
                 on item.ReturnTypeid equals o.Sno
                 where (item.Years == 2010 && o.Region == "Americano/Internacional")
                 select item).ToList();

            ReportDataSource rds_rpt_compass_investment_yearreturn_americano = new ReportDataSource("Charts_rpt_compass_investment_details_yearreturn_Americano", lst_rpt_compass_investment_yearreturn_americano);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_yearreturn_americano);
            List<Illustrator.InvestmentsCompass> lst_rpt_compass_investment_yearreturn_europeo =
                    (from item in lstInvestmentsCompass
                     join o in lstInvestmentMaster
                     on item.ReturnTypeid equals o.Sno
                     where (item.Years == 2010 && o.Region == "Europeo")
                     select item).ToList();

            ReportDataSource rds_rpt_compass_investment_yearreturn_europeo = new ReportDataSource("Charts_rpt_compass_investment_details_yearreturn_Europeo", lst_rpt_compass_investment_yearreturn_europeo);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_yearreturn_europeo);

            var lst_rpt_profile_de_inversion = oIllusDataManager.GetInvestmentsProfile();

            var lst_rpt_profile_de_inversion_euro = oIllusDataManager.GetInvestmentsProfileEuro();

            ReportDataSource rds_invest_distribution_Moderado = new ReportDataSource("Charts_rpt_InvestType_MODERADO", lst_rpt_invest_distribution_Moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_distribution_Moderado);
            ReportDataSource rds_profile_de_inversion = new ReportDataSource("Charts_profile_de_inversion", lst_rpt_profile_de_inversion);
            _reportViewer.LocalReport.DataSources.Add(rds_profile_de_inversion);

            ReportDataSource rds_profile_de_inversion_euro = new ReportDataSource("Charts_profile_de_inversion_euro", lst_rpt_profile_de_inversion_euro);
            _reportViewer.LocalReport.DataSources.Add(rds_profile_de_inversion_euro);
            ReportDataSource rds_invest_distribution_Balanceado = new ReportDataSource("Charts_rpt_InvestType_BALANCEADO", lst_rpt_invest_distribution_Balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_distribution_Balanceado);
            ReportDataSource rds_invest_distribution_Crecimiento = new ReportDataSource("Charts_rpt_InvestType_CRECIMIENTO", lst_rpt_invest_distribution_Crecimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_distribution_Crecimiento);
            ReportDataSource rds_invest_euro_distribution_Moderado = new ReportDataSource("Charts_rpt_euro_InvestType_MODERADO", lst_rpt_invest_euro_distribution_Moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_euro_distribution_Moderado);
            ReportDataSource rds_invest_euro_distribution_Balanceado = new ReportDataSource("Charts_rpt_euro_InvestType_BALANCEADO", lst_rpt_invest_euro_distribution_Balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_euro_distribution_Balanceado);
            ReportDataSource rds_invest_euro_distribution_Crecimiento = new ReportDataSource("Charts_rpt_euro_InvestType_CRECIMIENTO", lst_rpt_invest_euro_distribution_Crecimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_euro_distribution_Crecimiento);

            string euro_shareCrecimientoBond;
            if (totCrecimientoBond.FirstOrDefault() != null)
                euro_shareCrecimientoBond = Convert.ToString(euro_totCrecimientoBond.FirstOrDefault().total);
            else
                euro_shareCrecimientoBond = "0";

            ReportParameter euro_prmCrecimientoBondShare = new ReportParameter("euro_CrecimientoBondShare", euro_shareCrecimientoBond);
            paramlist.Add(euro_prmCrecimientoBondShare);
            ReportParameter euro_prmCrecimientoStockShare = new ReportParameter("euro_CrecimientoStockShare", euro_totCrecimientoStock.First().total.ToString());
            paramlist.Add(euro_prmCrecimientoStockShare);
            this._reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

            paramlist.Add(paramSpouseMaritalStatus);

            _reportViewer.LocalReport.EnableExternalImages = true;
            _reportViewer.LocalReport.SetParameters(paramlist);
            _reportViewer.LocalReport.DataSources.Add(rds);
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            _reportBinary = this._reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        }

        public void showIllustrationTERMfixed(DataTable dt, DataTable dttwo, IMaintermfixed insmain,
            Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetail customerPlanDetail,
            Entity.UnderWriting.IllusData.Illustrator.CustomerDetail customer)
        {
            this._reportViewer.LocalReport.Refresh();
            this._reportViewer.Reset();

            var partins = oIllusDataManager.GetCustomerPlanPartnerInsurance(CustomerPlanNo.GetValueOrDefault());

            ReportParameter paramOther = new ReportParameter("hideother", "Y");

            if (customerPlanDetail.RiderOir.Equals("Y"))
                paramOther = new ReportParameter("hideother", "N");
            if (ProductCode == Utility.ProductBehavior.Lighthouse.Code())
                this._reportViewer.LocalReport.ReportPath = _reportPath + "LightHouse_long.rdlc";
            else if (ProductCode == Utility.ProductBehavior.Sentinel.Code())
                this._reportViewer.LocalReport.ReportPath = _reportPath + "Sentinel_long.rdlc";

            ReportDataSource rds = new ReportDataSource();

            rds.Name = "illustratorDataSet_termillusdet";
            rds.Value = dt;

            ReportDataSource rdstwo = new ReportDataSource();
            rdstwo.Name = "illustratorDataSet_termtwo";
            if (dttwo.Rows.Count >= 17 && dttwo.Rows.Count <= 20 || dttwo.Rows.Count >= 37 && dttwo.Rows.Count <= 40 ||
                dttwo.Rows.Count >= 57 && dttwo.Rows.Count <= 60 || dttwo.Rows.Count >= 77 && dttwo.Rows.Count <= 80 ||
                dttwo.Rows.Count >= 97 && dttwo.Rows.Count <= 100)
            {
                DataRow myRow;
                myRow = dttwo.NewRow();
                var countdt = 0;
                if (dttwo.Rows.Count == 17 || dttwo.Rows.Count == 37 || dttwo.Rows.Count == 57 || dttwo.Rows.Count == 77 || dttwo.Rows.Count == 97)
                    countdt = 3;
                else if (dttwo.Rows.Count == 18 || dttwo.Rows.Count == 38 || dttwo.Rows.Count == 58 || dttwo.Rows.Count == 78 || dttwo.Rows.Count == 98)
                    countdt = 2;
                else if (dttwo.Rows.Count == 19 || dttwo.Rows.Count == 39 || dttwo.Rows.Count == 59 || dttwo.Rows.Count == 79 || dttwo.Rows.Count == 99)
                    countdt = 1;
                for (int c = 0; c < countdt; c++)
                    dttwo.Rows.Add("", "", "", "", "", "", "");
                dttwo.Rows.Add(myRow);
                rds.Value = dttwo;
            }

            rdstwo.Value = dttwo;

            string spouseinsuredamt = "-";
            if (partins != null && partins.RideroirAmount != null)
                spouseinsuredamt = partins.RideroirAmount.ToString();

            string contributionperiod = "-";
            string termamount = "-";
            string calculate = "-";
            string freqofpayment = "-";
            string rideradbamount = "-";
            string annualizepremium = "-";
            string initialcontributionamount = "-";
            string ddlinitialcontribution = "-";
            string premiumamount = "-";
            string insuredamount = "-";
            string crticleillnessamount = "-";
            string maritalstatus = "-";
            string plantype = "-";
            string almillar = "-";
            string risk = "-";
            string rawreturn = "-";
            string culminationage = "-";

            if (customerPlanDetail != null)
            {
                if (customerPlanDetail.ContributionPeriod != null)
                    contributionperiod = customerPlanDetail.ContributionPeriod.ToString();
                if (customerPlanDetail.RiderAdbAmount != null)
                    rideradbamount = customerPlanDetail.RiderAdbAmount.ToString();
                if (customerPlanDetail.RiderTermAmount != null)
                    termamount = customerPlanDetail.RiderTermAmount.ToString();
                if (customerPlanDetail.CalculateTypeCode != null)
                    calculate = customerPlanDetail.CalculateType;
                if (customerPlanDetail.FrequencyTypeCode != null)
                    freqofpayment = Resources.ResourceManager.GetString(customerPlanDetail.FrequencyType);
                if (customerPlanDetail.AnnualizedPremium != null)
                    annualizepremium = customerPlanDetail.AnnualizedPremium.ToString();

                initialcontributionamount = customerPlanDetail.InitialContribution.ToString();
                ddlinitialcontribution = customerPlanDetail.InitialContribution > 0 ? "Yes" : "No";

                if (customerPlanDetail.PremiumAmount > 0)
                    premiumamount = customerPlanDetail.PremiumAmount.ToString();
                else
                    premiumamount = "0";

                if (customerPlanDetail.InsuredAmount > 0)
                    insuredamount = customerPlanDetail.InsuredAmount.ToString();
                else
                    insuredamount = "0";

                if (customerPlanDetail.RiderCiAmount > 0)
                    crticleillnessamount = customerPlanDetail.RiderCiAmount.ToString();
                else
                    crticleillnessamount = "0";
                maritalstatus = Resources.ResourceManager.GetString(customer.MaritalStatus);

                plantype = Resources.ResourceManager.GetString(customerPlanDetail.PlanType);

                risk = Resources.ResourceManager.GetString(customerPlanDetail.ActivityRiskType);

                almillar = Resources.ResourceManager.GetString(customerPlanDetail.HealthRiskType);

                rawreturn = (customerPlanDetail.InitialContribution + customerPlanDetail.Frequency * customerPlanDetail.ContributionPeriod * customerPlanDetail.PremiumAmount).ToString();

                culminationage = (customerPlanDetail.ContributionPeriod + customer.Age.ToInt() - 1).ToString();
            }

            string rideroiramt = "-";
            string rideroirname = "-";
            string Riesgo = "-";
            string Fumador = "-";
            string Sexo = "-";
            string HastalaEdadde = "-";
            string Edad = "-";
            string AlMillar1 = "-";

            if (partins != null && customerPlanDetail.RiderOir.Equals("Y"))
            {

                rideroiramt = partins.RideroirAmount.ToString();
                rideroirname = partins.FirstName + " " + partins.MiddleName + " " + partins.LastName + " " + partins.LastName2;

                var riskOir = Utility.GetIllusDropDownByType(Utility.DropDownType.ActivityRiskType).FirstOrDefault(o => o.ActivityRiskTypeNo == partins.ActivityRiskTypeNo);
                if (riskOir != null)
                    Riesgo = Resources.ResourceManager.GetString(riskOir.ActivityRiskType);

                Fumador = partins.Smoker == "Y" ? RESOURCE.UnderWriting.NewBussiness.Resources.YesLabel : "No";

                var genderOir = Utility.GetIllusDropDownByType(Utility.DropDownType.Gender).FirstOrDefault(o => o.GenderCode == partins.GenderCode);
                if (genderOir != null)
                    Sexo = genderOir.GenderName;

                if (partins.ContributionTypeCode.Equals(Contributiontypes.CONTINUOUS))
                    HastalaEdadde = "99";
                else if (partins.ContributionTypeCode.Equals(Contributiontypes.NUMBEROFYEARS))
                    HastalaEdadde = (Convert.ToInt32(partins.Age) + partins.UntilAge - 1).ToString();
                else if (partins.ContributionTypeCode.Equals(Contributiontypes.UNTILAGE))
                    HastalaEdadde = partins.UntilAge.ToString();

                Edad = partins.Age.ToString();

                var healthOir = Utility.GetIllusDropDownByType(Utility.DropDownType.HealthRiskType).FirstOrDefault(o => o.HealthRiskTypeNo == partins.HealthRiskTypeNo);
                if (healthOir != null)
                    AlMillar1 = Resources.ResourceManager.GetString(healthOir.HealthRiskType);
            }
            //primary
            string primaryreq = "-", otherreq = "-";

            var exams = oIllusDataManager.GetCustomerPlanExam(CustomerPlanNo.GetValueOrDefault(), Insuredtypes.PRIMARY);

            String[] req = new String[20];

            int i = 0;
            foreach (var exam in exams)
            {
                primaryreq = exam.ExamName + "/" + primaryreq;
                req[i] = Resources.ResourceManager.GetString(exam.ExamName);
                i++;
            }

            string tempPrimary = " ";
            int lno1 = 0;

            for (int j = 0; j < req.Length; j++)
                if (!string.IsNullOrEmpty(req[j]))
                    if (!req[j].Trim().Equals(""))
                    {
                        tempPrimary += req[j];
                        if (j != (req.Length - 1))
                            tempPrimary += "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t";
                        if (tempPrimary.Length > (lno1 + 1) * 150)
                        {
                            tempPrimary += Environment.NewLine;
                            lno1 = lno1 + 1;
                        }

                    }
            //other

            exams = oIllusDataManager.GetCustomerPlanExam(CustomerPlanNo.GetValueOrDefault(), Insuredtypes.OTHER);

            String[] oreq = new String[12];

            int k = 0;
            foreach (var exam in exams)
            {
                otherreq = exam.ExamName + "/" + primaryreq;
                oreq[k] = Resources.ResourceManager.GetString(exam.ExamName);
                k++;
            }

            List<ReportParameter> paramlist = new List<ReportParameter>();

            ReportParameter param100 = new ReportParameter("PolicyholderExams1", "" + tempPrimary);

            //primary
            string tempOther = " ";
            //if (partins != null)
            if (customerPlanDetail.RiderOir.Equals("Y"))
                for (int j = 0; j < oreq.Length; j++)
                    if (!string.IsNullOrEmpty(oreq[j]))
                        if (!oreq[j].Trim().Equals(""))
                            tempOther += oreq[j] + ", ";
            if (tempOther.Trim().Length > 0)
                tempOther = tempOther.Substring(0, tempOther.Length - 2);

            ReportParameter param112 = new ReportParameter("other1", "" + tempOther);

            paramlist.Add(paramOther);
            paramlist.Add(param112);
            paramlist.Add(param100);

            String[] clientsign = new string[3];
            String[] agentsign = new string[3];

            clientsign[0] = "N";
            clientsign[1] = "N";
            clientsign[2] = "N";

            agentsign[0] = "N";
            agentsign[1] = "N";
            agentsign[2] = "N";

            ReportParameter Cpath1 = null;
            ReportParameter Apath1 = null;
            ReportParameter Cpath2 = null;
            ReportParameter Apath2 = null;
            ReportParameter Cpath3 = null;
            ReportParameter Apath3 = null;

            try
            {
                var isig = oIllusDataManager.GetIllustrationSignature(CustomerPlanNo.GetValueOrDefault()).First();

                clientsign[0] = isig.CustomerSign1;
                clientsign[1] = isig.CustomerSign2;
                clientsign[2] = isig.CustomerSign3;

                agentsign[0] = isig.AgentSign1;
                agentsign[1] = isig.AgentSign2;
                agentsign[2] = isig.AgentSign3;
            }
            catch (Exception ex)
            {
            }

            if (clientsign[0].Equals("Y"))
                Cpath1 = new ReportParameter("clientsignpath1", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_01C.jpg");
            else
                Cpath1 = new ReportParameter("clientsignpath1", _reportSignaturePath + "empty.jpg");

            if (agentsign[0].Equals("Y"))
                Apath1 = new ReportParameter("agentsignpath1", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_01A.jpg");
            else
                Apath1 = new ReportParameter("agentsignpath1", _reportSignaturePath + "empty.jpg");

            if (clientsign[1].Equals("Y"))
                Cpath2 = new ReportParameter("clientsignpath2", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_02C.jpg");
            else
                Cpath2 = new ReportParameter("clientsignpath2", _reportSignaturePath + "empty.jpg");

            if (agentsign[1].Equals("Y"))
                Apath2 = new ReportParameter("agentsignpath2", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_02A.jpg");
            else
                Apath2 = new ReportParameter("agentsignpath2", _reportSignaturePath + "empty.jpg");

            if (clientsign[2].Equals("Y"))
                Cpath3 = new ReportParameter("clientsignpath3", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_03C.jpg");
            else
                Cpath3 = new ReportParameter("clientsignpath3", _reportSignaturePath + "empty.jpg");

            if (agentsign[2].Equals("Y"))
                Apath3 = new ReportParameter("agentsignpath3", _reportSignaturePath + CustomerPlanNo.GetValueOrDefault().ToString("00000000") + "_03A.jpg");
            else
                Apath3 = new ReportParameter("agentsignpath3", _reportSignaturePath + "empty.jpg");

            paramlist.Add(Cpath1);
            paramlist.Add(Apath1);
            paramlist.Add(Cpath2);
            paramlist.Add(Apath2);
            paramlist.Add(Cpath3);
            paramlist.Add(Apath3);

            var lstInvestmentMaster = oIllusDataManager.GetRptInvestmentsCompassMaster();
            var lstInvestmentsCompass = oIllusDataManager.GetRptInvestmentsCompass(0);

            var st_rpt_compass_investment_master_moderado = lstInvestmentMaster.Where(o => o.ReturnType == "Moderado" && o.Region == "Americano/Internacional");
            ReportDataSource rds_rpt_compss_investment_master_moderado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_moderado);

            var st_rpt_compass_investment_distribution_moderado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 1);
            ReportDataSource rds_rpt_compass_investment_distribution_moderado = new ReportDataSource("Charts_rpt_compass_investment_details_MODERADO", st_rpt_compass_investment_distribution_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_moderado);

            var st_rpt_compass_investment_master_balanceado = lstInvestmentMaster.Where(o => o.ReturnType == "Balanceado" && o.Region == "Americano/Internacional");
            ReportDataSource rds_rpt_compss_investment_master_balanceado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_balanceado);

            var st_rpt_compass_investment_distribution_balanceado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 2);
            ReportDataSource rds_rpt_compass_investment_distribution_balanceado = new ReportDataSource("Charts_rpt_compass_investment_details_BALANCEADO", st_rpt_compass_investment_distribution_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_balanceado);

            var st_rpt_compass_investment_master_cricimiento = lstInvestmentMaster.Where(o => o.ReturnType == "Crecimiento" && o.Region == "Americano/Internacional");
            ReportDataSource rds_rpt_compss_investment_master_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_cricimiento);

            var st_rpt_compass_investment_distribution_cricimiento = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 3);
            ReportDataSource rds_rpt_compass_investment_distribution_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_details_CRICIMIENTO", st_rpt_compass_investment_distribution_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_cricimiento);

            var st_rpt_compass_investment_master_euro_moderado = lstInvestmentMaster.Where(o => o.ReturnType == "Moderado" && o.Region == "Europeo");
            ReportDataSource rds_rpt_compss_investment_master_euro_moderado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_euro_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_euro_moderado);

            var st_rpt_compass_investment_distribution_euro_moderado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 4);
            ReportDataSource rds_rpt_compass_investment_distribution_euro_moderado = new ReportDataSource("Charts_rpt_compass_investment_details_euro_MODERADO", st_rpt_compass_investment_distribution_euro_moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_euro_moderado);

            var st_rpt_compass_investment_master_euro_balanceado = lstInvestmentMaster.Where(o => o.ReturnType == "Balanceado" && o.Region == "Europeo");
            ReportDataSource rds_rpt_compss_investment_master_euro_balanceado = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_euro_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_euro_balanceado);

            var st_rpt_compass_investment_distribution_euro_balanceado = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 5);
            ReportDataSource rds_rpt_compass_investment_distribution_euro_balanceado = new ReportDataSource("Charts_rpt_compass_investment_details_euro_BALANCEADO", st_rpt_compass_investment_distribution_euro_balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_euro_balanceado);

            var st_rpt_compass_investment_master_euro_cricimiento = lstInvestmentMaster.Where(o => o.ReturnType == "Crecimiento" && o.Region == "Europeo");
            ReportDataSource rds_rpt_compss_investment_master_euro_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_master", st_rpt_compass_investment_master_euro_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compss_investment_master_euro_cricimiento);

            var st_rpt_compass_investment_distribution_euro_cricimiento = lstInvestmentsCompass.Where(o => o.ReturnTypeid == 6);
            ReportDataSource rds_rpt_compass_investment_distribution_euro_cricimiento = new ReportDataSource("Charts_rpt_compass_investment_details_euro_CRICIMIENTO", st_rpt_compass_investment_distribution_euro_cricimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_distribution_euro_cricimiento);

            List<Illustrator.InvestmentsCompass> lst_rpt_compass_investment_yearreturn_americano =
                (from item in lstInvestmentsCompass
                 join o in lstInvestmentMaster
                 on item.ReturnTypeid equals o.Sno
                 where (item.Years == 2010 && o.Region == "Americano/Internacional")
                 select item).ToList();

            ReportDataSource rds_rpt_compass_investment_yearreturn_americano = new ReportDataSource("Charts_rpt_compass_investment_details_yearreturn_Americano", lst_rpt_compass_investment_yearreturn_americano);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_yearreturn_americano);
            List<Illustrator.InvestmentsCompass> lst_rpt_compass_investment_yearreturn_europeo =
                    (from item in lstInvestmentsCompass
                     join o in lstInvestmentMaster
                     on item.ReturnTypeid equals o.Sno
                     where (item.Years == 2010 && o.Region == "Europeo")
                     select item).ToList();

            ReportDataSource rds_rpt_compass_investment_yearreturn_europeo = new ReportDataSource("Charts_rpt_compass_investment_details_yearreturn_Europeo", lst_rpt_compass_investment_yearreturn_europeo);
            _reportViewer.LocalReport.DataSources.Add(rds_rpt_compass_investment_yearreturn_europeo);

            var lst_rpt_profile_de_inversion = oIllusDataManager.GetInvestmentsProfile();

            var lst_rpt_profile_de_inversion_euro = oIllusDataManager.GetInvestmentsProfileEuro();
            var lstInvestmentType = oIllusDataManager.GetRptInvestmentsType(null, null, null);

            var lst_rpt_invest_distribution_Moderado = lstInvestmentType.Where(o => o.FundCategory == "Moderado" && o.Region == "Americano/Internacional");
            var lst_rpt_invest_distribution_Balanceado = lstInvestmentType.Where(o => o.FundCategory == "Balanceado" && o.Region == "Americano/Internacional");
            var lst_rpt_invest_distribution_Crecimiento = lstInvestmentType.Where(o => o.FundCategory == "Crecimiento" && o.Region == "Americano/Internacional");

            ReportDataSource rds_invest_distribution_Moderado = new ReportDataSource("Charts_rpt_InvestType_MODERADO", lst_rpt_invest_distribution_Moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_distribution_Moderado);
            ReportDataSource rds_profile_de_inversion = new ReportDataSource("Charts_profile_de_inversion", lst_rpt_profile_de_inversion);
            _reportViewer.LocalReport.DataSources.Add(rds_profile_de_inversion);

            var lst_rpt_invest_euro_distribution_Moderado = lstInvestmentType.Where(o => o.FundCategory == "Moderado" && o.Region == "Europeo");
            var lst_rpt_invest_euro_distribution_Balanceado = lstInvestmentType.Where(o => o.FundCategory == "Balanceado" && o.Region == "Europeo");
            var lst_rpt_invest_euro_distribution_Crecimiento = lstInvestmentType.Where(o => o.FundCategory == "Crecimiento" && o.Region == "Europeo");

            ReportDataSource rds_profile_de_inversion_euro = new ReportDataSource("Charts_profile_de_inversion_euro", lst_rpt_profile_de_inversion_euro);
            _reportViewer.LocalReport.DataSources.Add(rds_profile_de_inversion_euro);
            ReportDataSource rds_invest_distribution_Balanceado = new ReportDataSource("Charts_rpt_InvestType_BALANCEADO", lst_rpt_invest_distribution_Balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_distribution_Balanceado);
            ReportDataSource rds_invest_distribution_Crecimiento = new ReportDataSource("Charts_rpt_InvestType_CRECIMIENTO", lst_rpt_invest_distribution_Crecimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_distribution_Crecimiento);
            ReportDataSource rds_invest_euro_distribution_Moderado = new ReportDataSource("Charts_rpt_euro_InvestType_MODERADO", lst_rpt_invest_euro_distribution_Moderado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_euro_distribution_Moderado);
            ReportDataSource rds_invest_euro_distribution_Balanceado = new ReportDataSource("Charts_rpt_euro_InvestType_BALANCEADO", lst_rpt_invest_euro_distribution_Balanceado);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_euro_distribution_Balanceado);
            ReportDataSource rds_invest_euro_distribution_Crecimiento = new ReportDataSource("Charts_rpt_euro_InvestType_CRECIMIENTO", lst_rpt_invest_euro_distribution_Crecimiento);
            _reportViewer.LocalReport.DataSources.Add(rds_invest_euro_distribution_Crecimiento);

            var totMonderadoBond = from mb in lst_rpt_invest_distribution_Moderado
                                   where mb.FundType == "Bond" && mb.FundCategory == "Moderado"
                                   group mb by mb.FundCategory into g
                                   select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var totMonderadoStock = from ms in lst_rpt_invest_distribution_Moderado
                                    where ms.FundType == "Stock" && ms.FundCategory == "Moderado"
                                    group ms by ms.FundCategory into g
                                    select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter prmMonderadoBondShare = new ReportParameter("MonderadoBondShare", totMonderadoBond.First().total.ToString());
            paramlist.Add(prmMonderadoBondShare);
            ReportParameter prmMonderadoStockShare = new ReportParameter("MonderadoStockShare", totMonderadoStock.First().total.ToString());
            paramlist.Add(prmMonderadoStockShare);
            var totBalanceadoBond = from mb in lst_rpt_invest_distribution_Balanceado
                                    where mb.FundType == "Bond" && mb.FundCategory == "Balanceado"
                                    group mb by mb.FundCategory into g
                                    select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var totBalanceadoStock = from ms in lst_rpt_invest_distribution_Balanceado
                                     where ms.FundType == "Stock" && ms.FundCategory == "Balanceado"
                                     group ms by ms.FundCategory into g
                                     select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter prmBalanceadoBondShare = new ReportParameter("BalanceadoBondShare", totBalanceadoBond.First().total.ToString());
            paramlist.Add(prmBalanceadoBondShare);
            ReportParameter prmBalanceadoStockShare = new ReportParameter("BalanceadoStockShare", totBalanceadoStock.First().total.ToString());
            paramlist.Add(prmBalanceadoStockShare);

            var totCrecimientoBond = from mb in lst_rpt_invest_distribution_Crecimiento
                                     where mb.FundType == "Bond" && mb.FundCategory == "Crecimiento"
                                     group mb by mb.FundCategory into g
                                     select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var totCrecimientoStock = from ms in lst_rpt_invest_distribution_Crecimiento
                                      where ms.FundType == "Stock" && ms.FundCategory == "Crecimiento"
                                      group ms by ms.FundCategory into g
                                      select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            string shareCrecimientoBond;
            if (totCrecimientoBond.FirstOrDefault() != null)
                shareCrecimientoBond = Convert.ToString(totCrecimientoBond.FirstOrDefault().total);
            else
                shareCrecimientoBond = "0";

            ReportParameter prmCrecimientoBondShare = new ReportParameter("CrecimientoBondShare", shareCrecimientoBond);
            paramlist.Add(prmCrecimientoBondShare);
            ReportParameter prmCrecimientoStockShare = new ReportParameter("CrecimientoStockShare", totCrecimientoStock.First().total.ToString());
            paramlist.Add(prmCrecimientoStockShare);

            var euro_totBalanceadoBond = from mb in lst_rpt_invest_euro_distribution_Balanceado
                                         where mb.FundType == "Bond" && mb.FundCategory == "Balanceado"
                                         group mb by mb.FundCategory into g
                                         select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var euro_totBalanceadoStock = from ms in lst_rpt_invest_euro_distribution_Balanceado
                                          where ms.FundType == "Stock" && ms.FundCategory == "Balanceado"
                                          group ms by ms.FundCategory into g
                                          select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter euro_prmBalanceadoBondShare = new ReportParameter("euro_BalanceadoBondShare", euro_totBalanceadoBond.First().total.ToString());
            paramlist.Add(euro_prmBalanceadoBondShare);
            ReportParameter euro_prmBalanceadoStockShare = new ReportParameter("euro_BalanceadoStockShare", euro_totBalanceadoStock.First().total.ToString());
            paramlist.Add(euro_prmBalanceadoStockShare);

            var euro_totMonderadoBond = from mb in lst_rpt_invest_euro_distribution_Moderado
                                        where mb.FundType == "Bond" && mb.FundCategory == "Moderado"
                                        group mb by mb.FundCategory into g
                                        select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var euro_totMonderadoStock = from ms in lst_rpt_invest_euro_distribution_Moderado
                                         where ms.FundType == "Stock" && ms.FundCategory == "Moderado"
                                         group ms by ms.FundCategory into g
                                         select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            ReportParameter euro_prmMonderadoBondShare = new ReportParameter("euro_MonderadoBondShare", euro_totMonderadoBond.First().total.ToString());
            paramlist.Add(euro_prmMonderadoBondShare);
            ReportParameter euro_prmMonderadoStockShare = new ReportParameter("euro_MonderadoStockShare", euro_totMonderadoStock.First().total.ToString());
            paramlist.Add(euro_prmMonderadoStockShare);

            var euro_totCrecimientoBond = from mb in lst_rpt_invest_distribution_Crecimiento
                                          where mb.FundType == "Bond" && mb.FundCategory == "Crecimiento"
                                          group mb by mb.FundCategory into g
                                          select new { category = g.Key, total = g.Sum(mb => mb.FundValue) };

            var euro_totCrecimientoStock = from ms in lst_rpt_invest_distribution_Crecimiento
                                           where ms.FundType == "Stock" && ms.FundCategory == "Crecimiento"
                                           group ms by ms.FundCategory into g
                                           select new { category = g.Key, total = g.Sum(ms => ms.FundValue) };

            string euro_shareCrecimientoBond;
            if (totCrecimientoBond.FirstOrDefault() != null)
                euro_shareCrecimientoBond = Convert.ToString(euro_totCrecimientoBond.FirstOrDefault().total);
            else
                euro_shareCrecimientoBond = "0";

            ReportParameter euro_prmCrecimientoBondShare = new ReportParameter("euro_CrecimientoBondShare", euro_shareCrecimientoBond);
            paramlist.Add(euro_prmCrecimientoBondShare);
            ReportParameter euro_prmCrecimientoStockShare = new ReportParameter("euro_CrecimientoStockShare", euro_totCrecimientoStock.First().total.ToString());
            paramlist.Add(euro_prmCrecimientoStockShare);

            var rpt_Life_Expectancy = oIllusDataManager.GetRptLifeExpectancy();

            decimal MaxAge = 0;
            if (customer.GenderCode.ToString() == "M")
                MaxAge = Convert.ToDecimal((from item in rpt_Life_Expectancy where item.Current_Age == 0 select item.Men).First());
            else if (customer.GenderCode.ToString() == "F")
                MaxAge = Convert.ToDecimal((from item in rpt_Life_Expectancy where item.Current_Age == 0 select item.Woman).First());

            ReportParameter param110 = new ReportParameter("LifeExpectancy", (MaxAge - Convert.ToDecimal(customer.Age)).ToString());
            paramlist.Add(param110);
            ReportDataSource lifeexpetancy = new ReportDataSource("Charts_rpt_lifeexpectancy", rpt_Life_Expectancy);
            _reportViewer.LocalReport.DataSources.Add(lifeexpetancy);

            _prospect = (((customer.FirstName + " " + customer.MiddleName).Trim() + " " + customer.LastName).Trim() + " " + customer.LastName2).Trim();
            if (ProductCode == "")
            {
                const int MaxLengthHeading = 30;
                const int MaxLengthName = 50;
                var custHeading = (customer.FirstName + " " + customer.LastName).Trim();
                var custName = (customer.FirstName + " " + customer.LastName).Trim();
                if (custHeading.Length > MaxLengthHeading)
                {
                    custHeading = custHeading.Substring(0, MaxLengthHeading);
                    custHeading = custHeading + "...";
                }
                if (custName.Length > MaxLengthName)
                {
                    custName = custName.Substring(0, MaxLengthName);
                    custName = custName + "...";
                }
                ReportParameter param1 = new ReportParameter("heading", custHeading);
                ReportParameter param3 = new ReportParameter("name", custName);
                paramlist.Add(param1);
                paramlist.Add(param3);
            }
            else
            {
                const int MaxLengthHeading = 25;
                const int MaxLengthName = 50;
                var custHeading = (customer.FirstName + " " + customer.LastName).Trim();
                var custName = (customer.FirstName + " " + customer.LastName).Trim();
                if (custHeading.Length > MaxLengthHeading)
                {
                    custHeading = custHeading.Substring(0, MaxLengthHeading);
                    custHeading = custHeading + "...";
                }
                if (custName.Length > MaxLengthName)
                {
                    custName = custName.Substring(0, MaxLengthName);
                    custName = custName + "...";
                }
                ReportParameter param1 = new ReportParameter("heading", custHeading);
                ReportParameter param3 = new ReportParameter("name", custName);
                paramlist.Add(param1);
                paramlist.Add(param3);
            }


            ReportParameter param135 = new ReportParameter("lastname", customer.LastName.ToString());
            paramlist.Add(param135);
            ReportParameter param4 = new ReportParameter("periodofcontribution", customerPlanDetail.ContributionPeriod + "");
            ReportParameter param5 = new ReportParameter("amountofcontribution", initialcontributionamount.ToFormatCurrency());
            ReportParameter param6 = new ReportParameter("premiumamount", premiumamount.ToFormatCurrency());
            ReportParameter param7 = new ReportParameter("withdrawalamount", insuredamount.ToFormatCurrency());
            ReportParameter param8 = new ReportParameter("plantype", "Temporal" + "");
            ReportParameter param9 = new ReportParameter("age", customer.Age + "");
            ReportParameter param10 = new ReportParameter("gender", customer.GenderCode + "");
            ReportParameter param11 = new ReportParameter("smoker", customer.Smoker == "Y" ? RESOURCE.UnderWriting.NewBussiness.Resources.YesLabel : "No");
            ReportParameter param12 = new ReportParameter("ageatretirement", customerPlanDetail.ContributionUntilAge + "");
            ReportParameter param13 = new ReportParameter("risk", risk);
            ReportParameter param14 = new ReportParameter("almillar", almillar);
            ReportParameter param15 = new ReportParameter("maritalstatus", maritalstatus);
            ReportParameter param16 = new ReportParameter("suminsured", (customerPlanDetail.InsuredAmount + customerPlanDetail.RiderTermAmount).ToFormatCurrency());
            ReportParameter param17 = new ReportParameter("annualpremium", annualizepremium.ToFormatCurrency());
            ReportParameter param18 = new ReportParameter("period", freqofpayment);
            ReportParameter param19 = new ReportParameter("calcular", calculate);
            ReportParameter param20 = new ReportParameter("additionaltemporary", customerPlanDetail.RiderTermAmount.ToFormatCurrency());
            ReportParameter param21 = new ReportParameter("kind", customerPlanDetail.PClass.ToString());
            var country = Utility.GetIllusDropDownByType(Utility.DropDownType.Country)
         .Single(o => o.CountryNo == customer.ResCountryNo).CountryName;
            ReportParameter param22 = new ReportParameter("country", country);
            ReportParameter param23 = new ReportParameter("initialcontribution", ddlinitialcontribution);
            ReportParameter param24 = new ReportParameter("accidentaldeath", customerPlanDetail.RiderAdbAmount.ToFormatCurrency());
            ReportParameter param25 = new ReportParameter("spouceinsurance", rideroirname);
            ReportParameter param26 = new ReportParameter("primatarget", customerPlanDetail.TargetPremium.ToFormatCurrency());
            ReportParameter param27 = new ReportParameter("timeinsurance", contributionperiod);
            ReportParameter param28 = new ReportParameter("financialgoals", customerPlanDetail.FinancialGoal + "");
            ReportParameter param29 = new ReportParameter("plan", customerPlanDetail.Product);
            ReportParameter param120_1 = new ReportParameter("date", getspanishmonth(Int32.Parse(DateTime.Now.ToString("MM"))) + " " + DateTime.Now.ToString("dd, yyyy"));
            paramlist.Add(param120_1);
            ReportParameter param81 = new ReportParameter("class", customerPlanDetail.PClass.ToString());
            paramlist.Add(param81);
            ReportParameter paramsib = new ReportParameter("suminsuredbase", customerPlanDetail.InsuredAmount.ToFormatCurrency());
            paramlist.Add(paramsib);

            string investmentprofile = "-";
            ReportParameter param26_12 = new ReportParameter("investmentprofile", investmentprofile + "");
            paramlist.Add(param26_12);
            ReportParameter param76_12 = new ReportParameter("PaymentFrequency", freqofpayment);
            paramlist.Add(param76_12);
            string desc = "";
            string desc1 = "";
            if (ProductCode == Utility.ProductBehavior.Lighthouse.Code())
                desc = Generalmethods.getLightHouseDescriptionline(contributionperiod);
            else if (ProductCode == Utility.ProductBehavior.Sentinel.Code())
                desc = Generalmethods.getSentinalDescriptionline(contributionperiod);
            desc1 = Generalmethods.getSentinalDescriptionline1(contributionperiod);

            ReportParameter param30 = new ReportParameter("descriptionline", desc);
            ReportParameter param31 = new ReportParameter("lastline", Generalmethods.getLastline(_prospect, new Random().Next(1, 20), customerPlanDetail.PClass[0]));
            ReportParameter param33 = new ReportParameter("InitialContributionAmount", initialcontributionamount.ToFormatCurrency());
            ReportParameter param34 = new ReportParameter("Temporal", "-");
            ReportParameter param35 = new ReportParameter("PeriodoContribucion1", "-");
            ReportParameter param63 = new ReportParameter("PolicyholderSubscriptionRequirements", primaryreq);
            ReportParameter param64 = new ReportParameter("UnderwritingRequirementsAdditionalInsured", otherreq);
            ReportParameter param65 = new ReportParameter("Riesgo", Riesgo);
            ReportParameter param66 = new ReportParameter("AlMillar1", AlMillar1);
            ReportParameter param67 = new ReportParameter("Edad", Edad);
            ReportParameter param68 = new ReportParameter("Fumador", Fumador);
            ReportParameter param69 = new ReportParameter("Sexo", Sexo);
            ReportParameter param70 = new ReportParameter("ForYears", HastalaEdadde);
            ReportParameter param71 = new ReportParameter("HastalaEdadde", HastalaEdadde);
            ReportParameter param72 = new ReportParameter("AccidentalAmount", rideradbamount.ToFormatCurrency());
            ReportParameter param73 = new ReportParameter("TemporalAdicionalForYears", customerPlanDetail.RiderTermUntilAge + "");
            ReportParameter param74 = new ReportParameter("TemporalAdicionalAmount", customerPlanDetail.RiderTermAmount.ToFormatCurrency());
            ReportParameter param75 = new ReportParameter("criticleillness", customerPlanDetail.RiderCi == "Y" ? RESOURCE.UnderWriting.NewBussiness.Resources.YesLabel : "No");
            ReportParameter param76 = new ReportParameter("BottomText", "Esta presentación tiene una validez de 15 días hábiles y en ningún caso más allá del" + " 31-Diciembre-" + DateTime.Now.Year.ToString());
            ReportParameter param77 = new ReportParameter("rideroiramount", rideroiramt.ToFormatCurrency());
            ReportParameter param78 = new ReportParameter("ValueAccount", "-");
            ReportParameter param82 = new ReportParameter("foryear", "Por Años");
            ReportParameter param83 = new ReportParameter("withdrawalperiod", "-");
            ReportParameter param84 = new ReportParameter("rideroirname", rideroirname);
            ReportParameter param85 = new ReportParameter("crticleillnessamount", crticleillnessamount.ToFormatCurrency());

            if (!String.IsNullOrEmpty(_checkedSettings) || !String.IsNullOrEmpty(_unCheckedSettings))
            {
                string[] checkedSettings = _checkedSettings.NTrim().TrimStart(',').Split(',');
                string[] unCheckedSettings = _unCheckedSettings.NTrim().TrimStart(',').Split(',');

                for (int counter1 = 0; counter1 < checkedSettings.Count(); counter1++)
                {
                    ReportParameter visibilityParam = new ReportParameter(checkedSettings[counter1].ToString(), "checked");
                    paramlist.Add(visibilityParam);
                }

                for (int counter2 = 0; counter2 < unCheckedSettings.Count(); counter2++)
                {
                    if (!(unCheckedSettings[counter2].ToString() == null || unCheckedSettings[counter2].ToString() == ""))
                    {
                        ReportParameter visibilityParam = new ReportParameter(unCheckedSettings[counter2].ToString(), "unchecked");
                        paramlist.Add(visibilityParam);
                    }
                }
            }

            ReportParameter param308 = new ReportParameter("S308", "unchecked");
            paramlist.Add(param308);
            ReportParameter param124 = new ReportParameter("number", _numberPageIllus);
            ReportParameter param125 = new ReportParameter("culminationage", culminationage + "");

            ReportParameter paramcnp = new ReportParameter("contributionperiod", "PERÍODO INICIAL:" + " " + customerPlanDetail.ContributionPeriod + "Años - Prima Nivelada Garantizada");
            paramlist.Add(paramcnp);

            if (ProductCode == Utility.ProductBehavior.Sentinel.Code())
            {
                ReportParameter paramrop = new ReportParameter("returnamount", rawreturn);
                paramlist.Add(paramrop);
            }

            paramlist.Add(param4);
            paramlist.Add(param5);
            paramlist.Add(param6);
            paramlist.Add(param7);
            paramlist.Add(param8);
            paramlist.Add(param9);
            paramlist.Add(param10);
            paramlist.Add(param11);
            paramlist.Add(param12);
            paramlist.Add(param13);
            paramlist.Add(param14);
            paramlist.Add(param15);
            paramlist.Add(param16);
            paramlist.Add(param17);
            paramlist.Add(param18);
            paramlist.Add(param19);
            paramlist.Add(param20);
            paramlist.Add(param21);
            paramlist.Add(param22);
            paramlist.Add(param23);
            paramlist.Add(param24);
            paramlist.Add(param25);
            paramlist.Add(param26);
            paramlist.Add(param27);
            paramlist.Add(param28);
            paramlist.Add(param29);
            paramlist.Add(param30);

            paramlist.Add(param31);
            paramlist.Add(param33);
            paramlist.Add(param34);
            paramlist.Add(param35);
            paramlist.Add(param63);
            paramlist.Add(param64);
            paramlist.Add(param65);
            paramlist.Add(param66);
            paramlist.Add(param67);
            paramlist.Add(param68);
            paramlist.Add(param69);
            paramlist.Add(param70);
            paramlist.Add(param71);
            paramlist.Add(param72);
            paramlist.Add(param73);
            paramlist.Add(param74);
            paramlist.Add(param75);
            paramlist.Add(param76);
            paramlist.Add(param77);
            paramlist.Add(param78);
            paramlist.Add(param83);
            paramlist.Add(param84);
            paramlist.Add(param82);
            paramlist.Add(param85);
            paramlist.Add(param124);
            paramlist.Add(param125);

            var rpt_investment = oIllusDataManager.GetRptInvestmentsInflacion();

            var count = rpt_investment.Count();
            var inflation_Last = (from ms in rpt_investment
                                  select new { ms.Pequenas_Acciones, ms.Grandes_Acciones, ms.Papelesdel_Tesoro, ms.Bonosdel_Gobierno, ms.Inflacion }).Last();
            var inflation_first = (from ms in rpt_investment
                                   select new { ms.Pequenas_Acciones, ms.Grandes_Acciones, ms.Papelesdel_Tesoro, ms.Bonosdel_Gobierno, ms.Inflacion }).First();
            var percent_Pequenas_last = inflation_Last.Pequenas_Acciones;
            var percent_Pequenas_First = inflation_first.Pequenas_Acciones;
            var Grandes_Acciones_last = inflation_Last.Grandes_Acciones;
            var Grandes_Acciones_First = inflation_first.Grandes_Acciones;
            var Papelesdel_Tesoro_last = inflation_Last.Papelesdel_Tesoro;
            var Papelesdel_Tesoro_First = inflation_first.Papelesdel_Tesoro;
            var Bonosdel_Gobierno_last = inflation_Last.Bonosdel_Gobierno;
            var Bonosdel_Gobierno_First = inflation_first.Bonosdel_Gobierno;
            var Inflacion1_last = inflation_Last.Inflacion;
            var Inflacion1_First = inflation_first.Inflacion;

            var result_total = percent_Pequenas_last / percent_Pequenas_First;
            var numberofyears = 0.0119;
            var Pequenas_percent = (Math.Pow(Convert.ToDouble(result_total), numberofyears) - 1) * 100;

            var result1_total = Grandes_Acciones_last / Grandes_Acciones_First;
            var numberofyears1 = 0.0119;
            var Grandes_percent = (Math.Pow(Convert.ToDouble(result1_total), numberofyears1) - 1) * 100;

            var result2_total = Papelesdel_Tesoro_last / Papelesdel_Tesoro_First;
            var numberofyears2 = 0.0119;
            var Papelesdel_percent = (Math.Pow(Convert.ToDouble(result2_total), numberofyears2) - 1) * 100;

            var result3_total = Bonosdel_Gobierno_last / Bonosdel_Gobierno_First;
            var numberofyears3 = 0.0119;
            var Bonosdel_percent = (Math.Pow(Convert.ToDouble(result3_total), numberofyears3) - 1) * 100;

            var result4_total = Inflacion1_last / Inflacion1_First;
            var numberofyears4 = 0.0119;
            var Inflacion_percent = (Math.Pow(Convert.ToDouble(result4_total), numberofyears4) - 1) * 100;

            ReportParameter Pequenas_Acciones = new ReportParameter("Pequenas_Acciones", Pequenas_percent.ToString());
            ReportParameter Grandes = new ReportParameter("Grandes", Grandes_percent.ToString());
            ReportParameter Papelesdel_Tesoro = new ReportParameter("Papelesdel_Tesoro", Papelesdel_percent.ToString());
            ReportParameter Bonosdel_Gobierno = new ReportParameter("Bonosdel_Gobierno", Bonosdel_percent.ToString());
            ReportParameter Inflacion = new ReportParameter("Inflacion", Inflacion_percent.ToString());
            paramlist.Add(Pequenas_Acciones);
            paramlist.Add(Grandes);
            paramlist.Add(Papelesdel_Tesoro);
            paramlist.Add(Bonosdel_Gobierno);
            paramlist.Add(Inflacion);

            if (ProductCode == Utility.ProductBehavior.Sentinel.Code())
            {
                ReportParameter param30_12 = new ReportParameter("descriptionline1", desc1);
                paramlist.Add(param30_12);
            }

            string untilAge = "-";

            if (customerPlanDetail.RiderTerm.Equals("Y"))
                if (customerPlanDetail.TermContributionTypeCode.Equals(Contributiontypes.CONTINUOUS))
                    untilAge = "99";
                else if (customerPlanDetail.TermContributionTypeCode.Equals(Contributiontypes.NUMBEROFYEARS))
                    untilAge = (Convert.ToInt32(customer.Age) + customerPlanDetail.RiderTermUntilAge - 1).ToString();
                else if (customerPlanDetail.TermContributionTypeCode.Equals(Contributiontypes.UNTILAGE))
                    untilAge = customerPlanDetail.RiderTermUntilAge.ToString();

            ReportParameter paramAdditionalTermUntilAge = new ReportParameter("AdditionalTermUntilAge", untilAge);
            paramlist.Add(paramAdditionalTermUntilAge);

            maritalstatus = "-";
            if (customerPlanDetail.RiderOir.Equals("Y") && partins != null)
                maritalstatus = Utility.GetIllusDropDownByType(Utility.DropDownType.MaritalStatus)
                    .Single(o => o.MaritalStatusCode == partins.MaritalStatusCode).MaritalStatus;

            ReportParameter paramSpouseMaritalStatus = new ReportParameter("SpouseMaritalStatus", maritalstatus);
            paramlist.Add(paramSpouseMaritalStatus);

            string temp = "-";
            if (customerPlanDetail.RiderTerm.Equals("Y"))
                temp = customerPlanDetail.RiderTermAmount.ToFormatCurrency();

            temp = "-";
            if (customerPlanDetail.RiderAdb.Equals("Y"))
                temp = customerPlanDetail.RiderAdbAmount.ToFormatCurrency();

            string partinsRiderOirAmount = "-";
            if (customerPlanDetail.RiderOir.Equals("Y"))
                if (partins != null)
                    partinsRiderOirAmount = partins.RideroirAmount.ToFormatCurrency();

            untilAge = "-";

            if (customerPlanDetail.RiderTerm.Equals("Y"))
                if (customerPlanDetail.TermContributionTypeCode.Equals(Contributiontypes.CONTINUOUS))
                    untilAge = "99";
                else if (customerPlanDetail.TermContributionTypeCode.Equals(Contributiontypes.NUMBEROFYEARS))
                    untilAge = (Convert.ToInt32(customer.Age) + customerPlanDetail.RiderTermUntilAge - 1).ToString();
                else if (customerPlanDetail.TermContributionTypeCode.Equals(Contributiontypes.UNTILAGE))
                    untilAge = customerPlanDetail.RiderTermUntilAge.ToString();

            if (untilAge.Equals("0"))
                untilAge = "-";

            string untilAge2 = "-";

            if (customerPlanDetail.RiderOir.Equals("Y") && partins != null)
            {
                if (partins.ContributionTypeCode.Equals(Contributiontypes.CONTINUOUS))
                    untilAge2 = "99";
                else if (partins.ContributionTypeCode.Equals(Contributiontypes.NUMBEROFYEARS))
                    untilAge2 = (Convert.ToInt32(partins.Age) + partins.UntilAge - 1).ToString();
                else if (partins.ContributionTypeCode.Equals(Contributiontypes.UNTILAGE))
                    untilAge2 = partins.UntilAge.ToString();
            }
            if (untilAge2.Equals("0"))
                untilAge2 = "-";

            var rpt_Compass_Slide5 = oIllusDataManager.GetRptCompassSlide5();
            var Compass_slide7 = oIllusDataManager.RptCompassSlide7();

            foreach (var item in Compass_slide7)
            {
                if (item.Continent == "Europa")
                {
                    ReportParameter param60 = new ReportParameter("Europe_Deaths", item.Deaths.ToString() + " muertes");
                    ReportParameter param61 = new ReportParameter("Europe_Area", item.Area.ToString() + " Km2");
                    paramlist.Add(param60);
                    paramlist.Add(param61);
                }
                else if (item.Continent == "Mediterráneo Oriental")
                {
                    ReportParameter param62 = new ReportParameter("Eastern_Mediterranean_Deaths", item.Deaths.ToString() + " muertes");
                    ReportParameter param126 = new ReportParameter("Eastern_Mediterranean_Area", item.Area.ToString() + " Km2");
                    paramlist.Add(param62);
                    paramlist.Add(param126);
                }
                else if (item.Continent == "Pacífico Occidental")
                {
                    ReportParameter param127 = new ReportParameter("Pacífico_Occidental_Deaths", item.Deaths.ToString() + " muertes");
                    ReportParameter param128 = new ReportParameter("Pacífico_Occidental_Area", item.Area.ToString() + " Km2");
                    paramlist.Add(param127);
                    paramlist.Add(param128);
                }
                else if (item.Continent == "Asia Sur Oriental")
                {
                    ReportParameter param129 = new ReportParameter("Asia_Deaths", item.Deaths.ToString() + " muertes");
                    ReportParameter param105 = new ReportParameter("Asia_Area", item.Area.ToString());
                    paramlist.Add(param129);
                    paramlist.Add(param105);
                }
                else if (item.Continent == "Centro y Suramérica")
                {
                    ReportParameter param106 = new ReportParameter("america_Deaths", item.Deaths.ToString() + " muertes");
                    ReportParameter param107 = new ReportParameter("america_Area", item.Area.ToString() + " Km2");
                    paramlist.Add(param106);
                    paramlist.Add(param107);
                }
                else if (item.Continent == "Africa")
                {
                    ReportParameter param108 = new ReportParameter("Africa_Deaths", item.Deaths.ToString() + " muertes");
                    ReportParameter param109 = new ReportParameter("Africa_Area", item.Area.ToString() + " Km2");
                    paramlist.Add(param108);
                    paramlist.Add(param109);
                }
            }
            ReportDataSource Compassslide7 = new ReportDataSource("Charts_rpt_Compass_slide7", Compass_slide7);

            ReportDataSource Compassslide5 = new ReportDataSource("Charts_rpt_Compass_Slide5", rpt_Compass_Slide5);

            _reportViewer.LocalReport.DataSources.Add(Compassslide5);
            _reportViewer.LocalReport.DataSources.Add(Compassslide7);

            _reportViewer.LocalReport.EnableExternalImages = true;
            _reportViewer.LocalReport.SetParameters(paramlist);

            _reportViewer.LocalReport.DataSources.Add(rds);
            _reportViewer.LocalReport.DataSources.Add(rdstwo);
            this._reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            _reportBinary = this._reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        }

        private Tuple<
            IllusdataWebServices.WSCustomer,
            IllusdataWebServices.WSCustomerPlan,
            IllusdataWebServices.WSRider[],
            IllusdataWebServices.WSCustomerPlanPartner> SetIllustrationData(Illustrator.CustomerPlanDetail customerPlanDetail,
          Illustrator.CustomerDetail customer, bool QuitFuneral = false, bool? havePaymentSpecial = null)
        {
            var calculatePlanModel = new CalculatePlanModel();
            byte[] illusPDF = new byte[1];
            var familyProductCode = FamilyProductCode = customerPlanDetail.PlanGroupCode;

            // Lgonzalez - inicio 
            
            int ActuarialAge = 0;
            DateTime dEffecDay = (DateTime) customerPlanDetail.PlanEffectiveDate; // Obtengo Fecha Efectiva de Plan
            DateTime dDob = (DateTime) customer.BirthDate; //Obtengo fecha de nacimiento de customer
            
            DateTime dNextBday = new DateTime(dEffecDay.Year, dDob.Month, dDob.Day); //Obtengo fecha de cumpleanos en ano de efectividad de poliza

            ActuarialAge = SqlMethods.DateDiffMonth(dEffecDay, dNextBday);
            ActuarialAge = Convert.ToInt32( Microsoft.VisualBasic.DateAndTime.DateDiff(DateInterval.Month, dEffecDay, dNextBday));

            if (ActuarialAge == 6)
            {
                if (dEffecDay.Day < dNextBday.Day)
                    ActuarialAge += 1;
            }

            decimal ActuarialAge2 = 0;
            ActuarialAge2 = ((decimal) dNextBday.Subtract(dEffecDay).TotalDays)/30;


            if (ActuarialAge <= 6)
            {
                ActuarialAge = dNextBday.Year - dDob.Year; // Convert.ToInt32(customer.Age) + 1;
            }
            else
            {
                ActuarialAge = Convert.ToInt32(customer.Age);
            }
            
            /*int mesDiaEfect, mesDiaDob;
            string s = dEffecDay.ToString("yyMMdd").Substring(2);
            mesDiaEfect = Convert.ToInt32(dEffecDay.ToString("yyMMdd").Substring(2));
            mesDiaDob = Convert.ToInt32(dDob.ToString("yyMMdd").Substring(2));

            int ajuste = 0;
            if (mesDiaEfect >= mesDiaDob)
            {
                ajuste = 0;
            }
            else
            {
                ajuste = 1;
            }

            ActuarialAge = dEffecDay.Year - dDob.Year - ajuste;*/

            // Lgonzalez - fin


            var wsCustomer = new IllusdataWebServices.WSCustomer
            {
                Age = ActuarialAge, // customer.Age.ToInt(),        //Lgonzalez 31-01-2017
                FirstName = customer.FirstName,
                gendercode = customer.GenderCode,
                LastName = customer.LastName,
                LastName2 = customer.LastName2,
                MaritalStatuscode = customer.MaritalStatusCode,
                MiddleName = customer.MiddleName,
                Smoker = customer.Smoker
            };
            customerPlanDetail.HaveSpecialPayment = havePaymentSpecial;
            var wsPlan = new IllusdataWebServices.WSCustomerPlan();
            var wsRiders = new List<IllusdataWebServices.WSRider>();
            IllusdataWebServices.WSCustomerPlanPartner wsPartner = null;
            if (FamilyProductCode != Utility.EFamilyProductType.Funeral.Code() || QuitFuneral)
            {
                if (customerPlanDetail.RiderAdb == "Y")
                    wsRiders.Add(new IllusdataWebServices.WSRider
                    {
                        amount = customerPlanDetail.RiderAdbAmount.ToDouble(),
                        ridertypecode = "ADB"
                    });

                if (customerPlanDetail.RiderTerm == "Y")
                    wsRiders.Add(new IllusdataWebServices.WSRider
                    {
                        amount = customerPlanDetail.RiderTermAmount.ToDouble(),
                        ridertypecode = "TERM",
                        term = customerPlanDetail.RiderTermUntilAge,
                        type = customerPlanDetail.TermContributionTypeCode
                    });

                if (customerPlanDetail.RiderCi == "Y")
                    wsRiders.Add(new IllusdataWebServices.WSRider
                    {
                        amount = customerPlanDetail.RiderCiAmount.ToDouble(),
                        ridertypecode = "CI"
                    });

                if (customerPlanDetail.RiderOir == "Y")
                {
                    var customerPlanOI = oIllusDataManager.GetCustomerPlanPartnerInsurance(customerPlanDetail.CustomerPlanNo.GetValueOrDefault());
                    wsPartner = new IllusdataWebServices.WSCustomerPlanPartner
                    {
                        activityrisktypeno = customerPlanOI.ActivityRiskTypeNo.ToInt(),
                        age = customerPlanOI.Age.ToInt(),
                        amount = customerPlanOI.InsuredAmount.ToDouble(),
                        contributiontype = customerPlanOI.ContributionTypeCode,
                        dateofbirth = customerPlanOI.BirthDate.GetValueOrDefault().ToShortDateString(),
                        firstname = customerPlanOI.FirstName,
                        gendercode = customerPlanOI.GenderCode,
                        healthrisktypeno = customerPlanOI.HealthRiskTypeNo.ToInt(),
                        lastname = customerPlanOI.LastName,
                        LastName2 = customerPlanOI.LastName2,
                        maritalstatuscode = customerPlanOI.MaritalStatusCode,
                        middlename = customerPlanOI.MiddleName,
                        relationshiptypecode = customerPlanOI.RelationshipTypeCode,
                        smoker = customerPlanOI.Smoker,
                        term = customerPlanOI.ContributionTypeCode == "C" ? (99 - customerPlanOI.Age.ToInt()) : customerPlanOI.UntilAge
                    };
                }
            }
            else
            {
                wsPlan = new IllusdataWebServices.WSCustomerPlanFuneral();
                ((WSCustomerPlanFuneral)wsPlan).Repatriacion = customerPlanDetail.Repatriacion;
                ((WSCustomerPlanFuneral)wsPlan).Familiar = customerPlanDetail.Familiar;
                ((WSCustomerPlanFuneral)wsPlan).SepulturaLote = customerPlanDetail.SepulturaLote;
            }

            SetIllusPlan(wsPlan, customerPlanDetail);
            if (wsPlan.contributiontypecode == "C")
                customerPlanDetail.ContributionPeriod = 99 - customer.Age.ToInt();
            wsPlan.insuranceperiod = wsPlan.annuityperiod = customerPlanDetail.ContributionPeriod;
            if (wsPlan.retirementperiod == 0)
                wsPlan.retirementperiod = customerPlanDetail.ContributionPeriod;
            //Contribution typecode =m es cuando seleccionan cantidad en meses
            if (wsPlan.productcode == "VCR" && wsPlan.contributiontypecode == "M" && wsPlan.retirementperiod == 0)
            {
                if (customerPlanDetail.ContributionPeriodMonth.HasValue)
                {
                    //Convertimos en meses
                    wsPlan.retirementperiod = customerPlanDetail.ContributionPeriodMonth.Value / 12;
                    wsPlan.insuranceperiod = wsPlan.retirementperiod;
                }

            }


            wsPlan.countryno = customer.ResCountryNo.GetValueOrDefault();

            return Tuple.Create<
            IllusdataWebServices.WSCustomer,
            IllusdataWebServices.WSCustomerPlan,
            IllusdataWebServices.WSRider[],
            IllusdataWebServices.WSCustomerPlanPartner>(wsCustomer, wsPlan, wsRiders.ToArray(), wsPartner);
        }

        private void RefreshExamCondition(long customerPlanNo, string insuredTypeCode, WSExam[] lstExam)
        {
            oIllusDataManager.DeleteCustomerPlanExam(customerPlanNo, insuredTypeCode, IllusUserID.GetValueOrDefault());
            if (lstExam != null)
                foreach (var item in lstExam)
                    oIllusDataManager.UpdateCustomerPlanExam(new Illustrator.CustomerPlanExam
                    {
                        ExamCode = item.examcode,
                        CustomerPlanNo = customerPlanNo,
                        InsuredTypeCode = insuredTypeCode,
                        DateCreated = DateTime.Now,
                        CreatedBy = IllusUserID
                    });
        }
        #endregion
        #region Public
        public byte[] SeeIllustration(long customerPlanNo)
        {
            var customerPlanDetail = oIllusDataManager.GetAllCustomerPlanDetail(new Illustrator.CustomerPlanDetailP
            {
                CustomerPlanNo = customerPlanNo
            }).FirstOrDefault();
            var customer = oIllusDataManager.GetCustomerDetailById(customerPlanDetail.CustomerNo, null);

            var setDataIllustration = SetIllustrationData(customerPlanDetail, customer);
            var familyProductCode = FamilyProductCode = customerPlanDetail.PlanGroupCode;

            var wsCustomer = setDataIllustration.Item1;
            var wsPlan = setDataIllustration.Item2;
            var wsRiders = setDataIllustration.Item3;
            var wsPartner = setDataIllustration.Item4;
            var calculatePlanModel = new CalculatePlanModel();
            var wsIllus = new QuotesServiceClient();
            var wsResult = new WSResult();

            if (FamilyProductCode != Utility.EFamilyProductType.Funeral.Code())
                if (FamilyProductCode == Utility.EFamilyProductType.TermInsurance.Code())
                    wsResult = wsIllus.getTermModelIllustration(wsCustomer, wsPlan, wsRiders, wsPartner);
                else if (FamilyProductCode == Utility.EFamilyProductType.LifeInsurance.Code())
                    wsResult = wsIllus.getLifeModelIllustration(wsCustomer, wsPlan, wsRiders, wsPartner);
                else
                    wsResult = wsIllus.getAnnuityModelIllustration(wsCustomer, wsPlan);
            else
                wsResult = wsIllus.getFuneralModelIllustration(wsCustomer, (WSCustomerPlanFuneral)wsPlan);

            ValidateError(wsResult.errorslist);

            if (FamilyProductCode != Utility.EFamilyProductType.Funeral.Code())
                if (FamilyProductCode == Utility.EFamilyProductType.TermInsurance.Code())
                    _reportBinary = ((WSTermResult)wsResult).illuspdf;
                else if (FamilyProductCode == Utility.EFamilyProductType.LifeInsurance.Code())
                    _reportBinary = ((WSLifeResult)wsResult).illuspdf;
                else
                    _reportBinary = ((WSAnnuityResult)wsResult).illuspdf;
            else
                _reportBinary = ((WSFuneralResult)wsResult).illuspdf;

            RefreshExamCondition(customerPlanNo, "P", wsResult.primaryexamsrequiredlist);
            RefreshExamCondition(customerPlanNo, "O", wsResult.partnerexamsrequiredlist);

            return _reportBinary;
        }

        public byte[] CompareIllustrations(List<long> lstCustomerPlanNo)
        {
            Illustrator.CustomerDetail customer = null;
            var lstCompareCustomer = new List<WSCompareCustomer>();
            var wsIllus = new QuotesServiceClient();

            foreach (var customerPlanNo in lstCustomerPlanNo)
            {
                var customerPlanDetail = oIllusDataManager.GetAllCustomerPlanDetail(new Illustrator.CustomerPlanDetailP
                {
                    CustomerPlanNo = customerPlanNo
                }).FirstOrDefault();
                if (customer == null)
                    customer = oIllusDataManager.GetCustomerDetailById(customerPlanDetail.CustomerNo.GetValueOrDefault(), null);

                var setDataIllustration = SetIllustrationData(customerPlanDetail, customer, true);
                var familyProductCode = FamilyProductCode = customerPlanDetail.PlanGroupCode;

                var wsCustomer = setDataIllustration.Item1;
                var wsPlan = setDataIllustration.Item2;
                var wsRiders = setDataIllustration.Item3;
                var wsPartner = setDataIllustration.Item4;

                lstCompareCustomer.Add(new WSCompareCustomer
                {
                    customer = wsCustomer,
                    custplan = wsPlan,
                    partins = wsPartner,
                    riderslist = wsRiders.Any() ? wsRiders : null
                });
            }

            var wsResult = wsIllus.getCompareIllustration(lstCompareCustomer.ToArray());

            if (wsResult.list != null && wsResult.list.Any())
            {
                var sb = new StringBuilder();
                foreach (var i in wsResult.list)
                    foreach (var j in i.Where(o => !String.IsNullOrEmpty(o.errormessage)))
                        sb.AppendLine(" -" + (Language == Utility.Language.en ? j.errormessage : j.errormessageespanol) + "</br>");
                if (sb.Length > 0)
                    throw new Exception(sb.ToString());
            }

            _reportBinary = wsResult.result;

            return _reportBinary;
        }

		public double CalculateGoalSeek(double InsuredAmmount)
        {
            double result = 0;


            return result;
        }

        public CalculatePlanModel CalculatePlan(long customerPlanNo, bool? havePaymentSpecial = null)
        {
            var customerPlanDetail = oIllusDataManager.GetAllCustomerPlanDetail(new Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetailP
            {
                CustomerPlanNo = customerPlanNo
            }).FirstOrDefault();
            var customer = oIllusDataManager.GetCustomerDetailById(customerPlanDetail.CustomerNo.GetValueOrDefault(), null);
            customerPlanDetail.HaveSpecialPayment = havePaymentSpecial.GetValueOrDefault();
            var setDataIllustration = SetIllustrationData(customerPlanDetail, customer);
            var familyProductCode = FamilyProductCode = customerPlanDetail.PlanGroupCode;

            var wsCustomer = setDataIllustration.Item1;
            var wsPlan = setDataIllustration.Item2;
            var wsRiders = setDataIllustration.Item3;
            var wsPartner = setDataIllustration.Item4;
            var calculatePlanModel = new CalculatePlanModel();
            var wsIllus = new QuotesServiceClient();
            var wsResult = new WSResult();

            if (FamilyProductCode != Utility.EFamilyProductType.Funeral.Code())
            {

                if (FamilyProductCode == Utility.EFamilyProductType.TermInsurance.Code())
                { 
                    wsResult = wsIllus.getTermModel(wsCustomer, wsPlan, wsRiders, wsPartner);
                    calculatePlanModel.NetAnnualPremium = customerPlanDetail.NetAnnualPremium; //Bmarroquin 04-05-2017
                }
                else if (FamilyProductCode == Utility.EFamilyProductType.LifeInsurance.Code())
                {
                    wsResult = wsIllus.getLifeModel(wsCustomer, wsPlan, wsRiders.ToArray(), wsPartner);
                    calculatePlanModel.MinimumPremium = ((WSLifeResult)wsResult).minimumpremiumamount;
                }
                else
                {
                    wsResult = wsIllus.getAnnuityModel(wsCustomer, wsPlan);
                    calculatePlanModel.MinimumPremium = ((WSAnnuityResult)wsResult).minimumpremiumamount;
                    calculatePlanModel.AnnuityAmount = ((WSAnnuityResult)wsResult).annuityamount;
                    calculatePlanModel.TotalRetirementAmount = ((WSAnnuityResult)wsResult).totalannuityamount;
                }
            }
            else
                wsResult = wsIllus.getFuneralModel(wsCustomer, (WSCustomerPlanFuneral)wsPlan);

            ValidateError(wsResult.errorslist);
            SetCalculatePlanModel(wsResult, calculatePlanModel);

            RefreshExamCondition(customerPlanNo, "P", wsResult.primaryexamsrequiredlist);
            RefreshExamCondition(customerPlanNo, "O", wsResult.partnerexamsrequiredlist);

            calculatePlanModel.PeriodicPremiumPerFrequency = Convert.ToDouble(customerPlanDetail.Frequency * Math.Round(calculatePlanModel.PeriodicPremium,2));  //Lgonzalez 17-02-2017
            calculatePlanModel.NetAnnualPremium = calculatePlanModel.PeriodicPremiumPerFrequency - calculatePlanModel.FractionSurcharge;

            return calculatePlanModel;
        }

        public double CalculatePv(double growthrate, int frequency, double amount)
        {
            double netamount = amount;
            for (int i = 1; i < frequency; i++)
                netamount = netamount + amount * (1.0 / Math.Pow((1 + growthrate), i));

            return netamount;
        }

        public bool GetProductIsFixed(string productCode)
        {
            return Utility
                       .GetIllusDropDownByType(
                        Utility.DropDownType.ProductType)
                       .Single(o => o.ProductCode == productCode)
                       .PFixed == "Y";
        }

        public string ValidateDataBeforeCalculate(Entity.UnderWriting.IllusData.Illustrator.CustomerDetail customer,
            Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetail customerPlanDetail)
        {
            try
            {
                var customerPlanNo = customerPlanDetail.CustomerPlanNo;
                var productCode = customerPlanDetail.ProductCode;
                var msgToValidate = "";
                var isFixed = ProductIsFixed;

                var frequencyTypeModel = Utility.GetIllusDropDownByType(Utility.DropDownType.FrequencyType)
                .Single(o => o.FrequencyTypeCode == customerPlanDetail.FrequencyTypeCode);

                var minimumcontributionperiod = RulesService.GetValue(RulesService.Rules.MINIMUM_CONTRIBUTION_PERIOD);

                var maxageendannuity = RulesService.GetValue(RulesService.Rules.MAXIMUM_AGE_END_ANNUITY).ToInt();

                var insuredAnnuityAmount = (
                    new string[] { "AXS", "HRZ", "EDU", "SCH" }.Contains(productCode) ?
                    customerPlanDetail.AnnuityAmount :
                    customerPlanDetail.InsuredAmount).ToDouble();

                msgToValidate = validateGSdata(productCode, frequencyTypeModel.FrequencyValue.Value, customerPlanDetail.PremiumAmount.ToDouble(), insuredAnnuityAmount);

                if (!String.IsNullOrEmpty(msgToValidate))
                    return msgToValidate;

                var insuranceminage = RulesService.GetValue(RulesService.Rules.INSURANCE_MIN_AGE).ToInt();
                var insurancemaxage = RulesService.GetValue(RulesService.Rules.INSURANCE_MAX_AGE).ToInt();
                var maxage = RulesService.GetValue(RulesService.Rules.MAX_AGE).ToInt();

                var numberofyears = 0;
                var age = customer.Age.ToInt();

                var mininsuredamount = RulesService.GetValue(RulesService.Rules.MINIMUM_INSURED_AMT);
                var maxinsuredamount = RulesService.GetValue(RulesService.Rules.MAXIMUM_INSURED_AMT);

                var contributiontypecode = customerPlanDetail.ContributionTypeCode[0].ToString();

                var calculatetypecode = customerPlanDetail.CalculateTypeCode[0].ToString();
                var plantypecode = customerPlanDetail.PlanTypeCode[0].ToString();

                var annualpremium = GetAnnualizedPremium(productCode, customerPlanDetail.InvestmentProfilePercent.ToDouble(), frequencyTypeModel.FrequencyValue.GetValueOrDefault(), oIllusDataManager.GetFrequencyCost(productCode, customerPlanDetail.FrequencyTypeCode).Single().FrequencyCost.GetValueOrDefault(), customerPlanDetail.PremiumAmount.ToDouble());
                var premiumamount = customerPlanDetail.PremiumAmount.ToDouble();
                var insuredamount = customerPlanDetail.InsuredAmount.ToDouble();
                var minumumpremium = RulesService.GetValue(RulesService.Rules.MINIMUM_YEARLY_PREMIUM);

                int insuranceunderage = RulesService.GetValue(RulesService.Rules.INSURANCE_UNDERAGE).ToInt();
                var underagemaxinsuredamount = RulesService.GetValue(RulesService.Rules.MAXIMUM_INSURED_AMT_UNDERAGE);

                if (contributiontypecode == Utility.EContributionType.NumberOfYears.Code())
                    numberofyears = customerPlanDetail.ContributionPeriod;
                else if (contributiontypecode == Utility.EContributionType.UntilAge.Code())
                    numberofyears = customerPlanDetail.ContributionUntilAge - age + 1;
                else if (contributiontypecode == Utility.EContributionType.Continuous.Code())
                    numberofyears = maxage - age;

                if (FamilyProductCode == Utility.EFamilyProductType.LifeInsurance.Code())
                {
                    // the rules for min and max insurance age
                    int fingoalage = customerPlanDetail.FinancialGoalAge;
                    if (numberofyears <= 0)
                        return Resources.Premium_payment_years_need_to_be_more_than_1;

                    if ((numberofyears + age) > maxage)
                        return Resources.CannotBeGreaterThan.SFormat(Resources.ContributionPeriodLabel, maxage);

                    if (numberofyears <= minimumcontributionperiod)
                        return Resources.Premium_Payment_Yearsneed_to_be_at_least.SFormat(minimumcontributionperiod);

                    if ((insuranceminage > 0 && age < insuranceminage) || (insurancemaxage > 0 && age > insurancemaxage))
                        return Resources.FieldIsNotBetween.SFormat(Resources.Age, Resources.Minimum_a, Resources.Maximum_a + " " + Resources.InsuranceAge);

                    if ((numberofyears + age) > maxage)
                        return Resources.CannotBeGreaterThan.SFormat(Resources.ContributionPeriodLabel + " + " + Resources.Age, Resources.MaximumAge);

                    if ((age <= insuranceunderage) && (calculatetypecode != Utility.CalculateType.InsuredAmount.Code()) && (insuredamount > underagemaxinsuredamount))
                        return Resources.CannotBeGreaterThan.SFormat(Resources.UnderageInsured, underagemaxinsuredamount);

                    if ((calculatetypecode != Utility.CalculateType.InsuredAmount.Code()) && (insuredamount <= mininsuredamount) && (insuredamount > maxinsuredamount))
                        return Resources.FieldIsNotBetween.SFormat(Resources.InsuredAmount, mininsuredamount, maxinsuredamount);

                    if (customerPlanDetail.FinancialGoal == "Y" && (fingoalage <= age || fingoalage > maxage))
                        return Resources.FieldIsNotBetween.SFormat(Resources.FinancialGoalLabel, (age + 1), maxage);
                }
                else if (FamilyProductCode == Utility.EFamilyProductType.Education.Code() || FamilyProductCode == Utility.EFamilyProductType.Retirement.Code())
                {
                    int contributionperiod = contributiontypecode == Utility.EContributionType.UntilAge.Code() ?
                        customerPlanDetail.ContributionUntilAge : customerPlanDetail.ContributionPeriod;
                    int minperiodpriorannuity = RulesService.GetValue(RulesService.Rules.MINIMUM_PERIOD_PRIOR_ANNUITY).ToInt();

                    int maxannuityperiod = RulesService.GetValue(RulesService.Rules.MAXIMUM_ANNUITY_PERIOD).ToInt();
                    int minannuityperiod = RulesService.GetValue(RulesService.Rules.MINIMUM_ANNUITY_PERIOD).ToInt();

                    double maxannuityamountyear = RulesService.GetValue(RulesService.Rules.MAXIMUM_ANNUITY_AMT_YEAR);
                    double minannuityamountyear = RulesService.GetValue(RulesService.Rules.MINIMUM_ANNUITY_AMT_YEAR);

                    int maxagepriorannuity = RulesService.GetValue(RulesService.Rules.MAXIMUM_AGE_PRIOR_ANNUITY).ToInt();

                    int maxcontributionperiod = RulesService.GetValue(RulesService.Rules.MAXIMUM_CONTRIBUTION_PERIOD).ToInt();

                    int annuityperiod = customerPlanDetail.RetirementPeriod.ToInt();
                    int defermentperiod = customerPlanDetail.DefermentPeriod.ToInt();

                    if (maxageendannuity > 0 && (age + contributionperiod + defermentperiod + annuityperiod) > maxageendannuity)
                        return Resources.CannotBeGreaterThan.SFormat(
                            "{0} + {1} + {2} + {3}".SFormat(
                            Resources.Age, Resources.ContributionPeriodLabel, Resources.DefermentPeriod, Resources.AnnuityPeriod), maxageendannuity);

                    if (maxagepriorannuity > 0 && (age + contributionperiod + defermentperiod) > maxagepriorannuity)
                        return Resources.CannotBeGreaterThan.SFormat(
                            "{0} + {1} + {2}".SFormat(
                            Resources.Age, Resources.ContributionPeriodLabel, Resources.DefermentPeriod), maxagepriorannuity);

                    if (minperiodpriorannuity > 0 && (contributionperiod + defermentperiod) < minperiodpriorannuity)
                        return Resources.ShouldBeGreaterOrEqualTo.SFormat(
                            "{0} + {1}".SFormat(Resources.ContributionPeriodLabel, Resources.DefermentPeriod), minperiodpriorannuity);

                    if (maxageendannuity > 0 && (age + defermentperiod + contributionperiod) > maxageendannuity)
                        return Resources.ShouldBeGreaterThan.SFormat(
                            "{0} + {1} + {2} + {3}".SFormat(Resources.Age,
                            Resources.ContributionPeriodLabel, Resources.DefermentPeriod, Resources.AnnuityPeriod), maxageendannuity);

                    if (maxannuityperiod > 0 && (annuityperiod) > maxannuityperiod)
                        return Resources.CannotBeGreaterThan.SFormat(Resources.AnnuityPeriod, maxannuityperiod);

                    if (minannuityperiod > 0 && (annuityperiod) < minannuityperiod)
                        return Resources.CannotBeLessThan.SFormat(Resources.AnnuityPeriod, minannuityperiod);

                    if (maxcontributionperiod > 0 && (contributionperiod > maxcontributionperiod))
                        return Resources.CannotBeGreaterThan.SFormat(Resources.ContributionPeriodLabel, maxcontributionperiod);

                    if (numberofyears <= 0)
                        return Resources.CannotBeLessThan.SFormat(Resources.PremiumPaymentYears, 1);

                    if (plantypecode == Utility.EPlanType.Insured.Code())
                    {
                        if (maxage > 0 && (numberofyears + age) > maxage)
                            return Resources.CannotBeGreaterThan.SFormat(
                                "{0} + {1}".SFormat(
                                Resources.Age, Resources.ContributionPeriodLabel), maxage);

                        if (insurancemaxage > 0 && (age < insuranceminage ||
                            age > insurancemaxage))
                            return Resources.FieldIsNotBetween.SFormat(Resources.Age, Resources.Minimum_a, Resources.Maximum_a + " " + Resources.InsuranceAge);
                    }

                    if (numberofyears <= minimumcontributionperiod)
                        return Resources.CannotBeLessThan.SFormat(Resources.PremiumPaymentYears, minimumcontributionperiod + " " + Resources.Years);
                }
                else if (FamilyProductCode == Utility.EFamilyProductType.TermInsurance.Code())
                {
                    var insuranceperiod = customerPlanDetail.RetirementPeriod.ToInt();
                    var mininsuranceperiod = RulesService.GetValue(RulesService.Rules.MINIMUM_INSURANCE_PERIOD).ToInt();
                    var maxinsuranceperiod = RulesService.GetValue(RulesService.Rules.MAXIMUM_INSURANCE_PERIOD).ToInt();
                    var maxageinsuranceterm = RulesService.GetValue(RulesService.Rules.MAX_AGE_INSURANCE_TERM).ToInt();

                    if (numberofyears <= 0)
                        return Resources.ShouldBeGreaterThan.SFormat(Resources.PremiumPaymentYears, 1);

                    if (maxageinsuranceterm > 0 && (numberofyears + age) > maxageinsuranceterm)
                        return Resources.CannotBeGreaterThan.SFormat(
                            "{0} + {1}".SFormat(
                            Resources.Age, Resources.ContributionPeriodLabel), maxageinsuranceterm);

                    if (minimumcontributionperiod > 0 && numberofyears <= minimumcontributionperiod)
                        return Resources.CannotBeLessThan.SFormat(Resources.PremiumPaymentYears, minimumcontributionperiod + " " + Resources.Years);

                    if (insuranceminage > 0 && insurancemaxage > 0 && (age < insuranceminage || age > insurancemaxage))
                        return Resources.FieldIsNotBetween.SFormat(Resources.Age, Resources.Minimum_a, Resources.Maximum_a + " " + Resources.InsuranceAge);

                    if (maxageinsuranceterm > 0 && (numberofyears + age) > maxageinsuranceterm)
                        return Resources.CannotBeGreaterThan.SFormat(
                            "{0} + {1}".SFormat(
                            Resources.Age, Resources.ContributionPeriodLabel), maxageinsuranceterm);

                    if (minumumpremium > 0 && (annualpremium < minumumpremium) && (calculatetypecode != Utility.CalculateType.PremiumAmount.Code()))
                        return Resources.ShouldBeGreaterThan.SFormat(Resources.Premium, minumumpremium);

                    if (underagemaxinsuredamount > 0 && (age <= insuranceunderage)
                        && (calculatetypecode != Utility.CalculateType.InsuredAmount.Code())
                        && (insuredamount > underagemaxinsuredamount))
                        return Resources.CannotBeGreaterThan.SFormat(Resources.UnderageInsuredAmount, underagemaxinsuredamount);

                    if (mininsuredamount > 0 && maxinsuredamount > 0 &&
                        (calculatetypecode != Utility.CalculateType.InsuredAmount.Code()) &&
                        (insuredamount < mininsuredamount || insuredamount > maxinsuredamount) && age > 17)
                        return Resources.FieldIsNotBetween.SFormat(Resources.InsuredAmount, mininsuredamount, maxinsuredamount);

                    if (insuranceperiod <= 0)
                        return Resources.FieldCannotBeEmpty.SFormat(Resources.InsurancePeriod);

                    if (mininsuranceperiod > 0 && maxinsuranceperiod > 0 &&
                        (insuranceperiod < mininsuranceperiod || insuranceperiod > maxinsuranceperiod))
                        return Resources.FieldIsNotBetween.SFormat(Resources.InsurancePeriod, mininsuranceperiod, maxinsuranceperiod);

                    if (maxageinsuranceterm > 0 && (age + insuranceperiod) > maxageinsuranceterm)
                        return Resources.Term_Insurance_can_be_provide_only_until.SFormat(maxageinsuranceterm);

                    if ((numberofyears) != insuranceperiod)
                        return Resources.FieldAndFieldShouldBeEqual.SFormat(Resources.InsurancePeriod, Resources.PremiumPeriod);
                }

                if (FamilyProductCode == Utility.EFamilyProductType.LifeInsurance.Code() || FamilyProductCode == Utility.EFamilyProductType.TermInsurance.Code())
                {
                    msgToValidate = ValidateDataRider(customer, customerPlanDetail);

                    if (!String.IsNullOrEmpty(msgToValidate))
                        return msgToValidate;
                }

                return null;
            }
            catch (Exception ex)
            {
                return ex.GetLastInnerException().Message;
            }
        }

        public string ValidateDataRider(Entity.UnderWriting.IllusData.Illustrator.CustomerDetail customer,
            Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetail customerPlanDetail)
        {
            String productcode = customerPlanDetail.ProductCode;

            int insuranceminage = RulesService.GetValue(RulesService.Rules.INSURANCE_MIN_AGE).ToInt();
            int insurancemaxage = RulesService.GetValue(RulesService.Rules.INSURANCE_MAX_AGE).ToInt();
            int maxage = RulesService.GetValue(RulesService.Rules.MAX_AGE).ToInt();

            int age = customer.Age.ToInt();
            char contributiontypecode = customerPlanDetail.ContributionTypeCode[0];

            char calculatetypecode = customerPlanDetail.CalculateTypeCode[0];

            double premiumamount = customerPlanDetail.PremiumAmount.ToDouble();
            double insuredamount = customerPlanDetail.InsuredAmount.ToDouble();
            double minumumpremium = RulesService.GetValue(RulesService.Rules.MINIMUM_YEARLY_PREMIUM);

            int insuranceunderage = RulesService.GetValue(RulesService.Rules.INSURANCE_UNDERAGE).ToInt();
            double underagemaxinsuredamount = RulesService.GetValue(RulesService.Rules.MAXIMUM_INSURED_AMT_UNDERAGE);

            double mininsuredamount = RulesService.GetValue(RulesService.Rules.MINIMUM_INSURED_AMT);
            double maxinsuredamount = RulesService.GetValue(RulesService.Rules.MAXIMUM_INSURED_AMT);

            if (customerPlanDetail.RiderAdb == "Y")
            {
                double maxadbamount = RulesService.GetValue(RulesService.Rules.ADB_MAX_INSURED_AMOUNT);
                double minadbamount = RulesService.GetValue(RulesService.Rules.ADB_MIN_INSURED_AMOUNT);
                var adbamount = customerPlanDetail.RiderAdbAmount.ToDouble();

                if (adbamount > maxadbamount || adbamount < minadbamount)
                    return Resources.FieldIsNotBetween.SFormat(Resources.AdbAmount, minadbamount, maxadbamount);

                if (adbamount < mininsuredamount || adbamount > maxinsuredamount)
                    return Resources.FieldIsNotBetween.SFormat(Resources.AdbAmount, mininsuredamount, maxinsuredamount);

                if ((calculatetypecode != Calculatetypes.INSUREDAMOUNT) && (adbamount > insuredamount))
                    return Resources.CannotBeGreaterThan.SFormat(Resources.AdbAmount, Resources.InsuredAmount);
            }

            if (customerPlanDetail.RiderTerm == "Y")
            {
                double maxtermamount = RulesService.GetValue(RulesService.Rules.ADDL_MAX_INSURED_AMOUNT);
                double mintermamount = RulesService.GetValue(RulesService.Rules.ADDL_MIN_INSURED_AMOUNT);
                int termuntilage = customerPlanDetail.RiderTermUntilAge;
                var termamount = customerPlanDetail.RiderTermAmount.ToDouble();

                if ((calculatetypecode != Calculatetypes.INSUREDAMOUNT) && (termamount > insuredamount))
                    return Resources.CannotBeGreaterThan.SFormat(Resources.TermAmount, Resources.InsuredAmount);

                if (termamount > maxtermamount || termamount < mintermamount)
                    return Resources.FieldIsNotBetween.SFormat(Resources.AdditionalInsuranceAmount, mininsuredamount, maxtermamount);

                if (termamount < mininsuredamount || termamount > maxinsuredamount)
                    return Resources.FieldIsNotBetween.SFormat(Resources.AdditionalInsuranceAmount, mininsuredamount, maxinsuredamount);

                if ((calculatetypecode != Calculatetypes.INSUREDAMOUNT) && (termamount > insuredamount))
                    return Resources.CannotBeGreaterThan.SFormat(Resources.AdditionalInsuranceAmount, Resources.InsuredAmount);
            }

            if (customerPlanDetail.RiderOir == "Y")
            {
                var customerPlanOI = oIllusDataManager.GetCustomerPlanPartnerInsurance(customerPlanDetail.CustomerPlanNo.GetValueOrDefault());
                double oiramount = customerPlanOI.RideroirAmount.ToDouble();
                char spousecontrtypecode = customerPlanOI.ContributionTypeCode[0];

                double maxoiramount = RulesService.GetValue(RulesService.Rules.OIR_MAX_INSURED_AMOUNT);
                double minoiramount = RulesService.GetValue(RulesService.Rules.OIR_MIN_INSURED_AMOUNT);

                int oirage = customerPlanOI.Age.GetValueOrDefault();
                int oiruntilage = customerPlanOI.UntilAge;


                if (oirage < insuranceminage || oirage > insurancemaxage)
                    return Resources.FieldIsNotBetween.SFormat(Resources.Age, Resources.Minimum_a, Resources.Maximum_a + " " + Resources.InsuranceAge);

                if (spousecontrtypecode == Contributiontypes.UNTILAGE)
                {
                    if (oiruntilage < oirage || oiruntilage > maxage)
                        return Resources.FieldIsNotBetween.SFormat(Resources.InsuranceAge, oirage + 1, maxage);
                }
                else if (spousecontrtypecode == Contributiontypes.NUMBEROFYEARS)
                {
                    if (oiruntilage + oirage > maxage)
                        return Resources.CannotBeGreaterThan.SFormat(Resources.NumberOfYears, maxage - oirage);
                }
                if ((calculatetypecode != Calculatetypes.INSUREDAMOUNT) && (oiramount > insuredamount))
                    return Resources.Spouse_Other_Insured_rider_has_an_Insured_Amount_that_is_greater_than_the_Main_Insured_Amount_Please_try_again;

                if ((oirage <= insuranceunderage) && (oiramount > underagemaxinsuredamount))
                    return Resources.CannotBeGreaterThan.SFormat(Resources.UnderageSpouseOtherInsuredAmount, underagemaxinsuredamount);

                if (oiramount > maxoiramount || oiramount < minoiramount)
                    return Resources.FieldIsNotBetween.SFormat(Resources.UnderageSpouseOtherInsuredAmount, minoiramount, maxoiramount);

                if (oiramount < mininsuredamount || oiramount > maxinsuredamount)
                    return Resources.FieldIsNotBetween.SFormat(Resources.UnderageSpouseOtherInsuredAmount, mininsuredamount, maxinsuredamount);

                if ((calculatetypecode != Calculatetypes.INSUREDAMOUNT) && (oiramount > insuredamount))
                    return Resources.CannotBeGreaterThan.SFormat(Resources.UnderageSpouseOtherInsuredAmount, Resources.InsuredAmount);
            }

            return null;
        }

        public string validateDataAfterCalculate(Entity.UnderWriting.IllusData.Illustrator.CustomerDetail customer,
            Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetail customerPlanDetail, double premiumAmount, double insuredAmount, double annuityAmount)
        {
            var msgToValidate = "";
            try
            {
                var partins = oIllusDataManager.GetCustomerPlanPartnerInsurance(CustomerPlanNo.GetValueOrDefault());

                string adbamount1 = "";
                string termamount1 = "";
                string spouseinsuredamount1 = "";

                if (customerPlanDetail != null)
                {
                    adbamount1 = customerPlanDetail.RiderAdbAmount.ToString();
                    termamount1 = customerPlanDetail.RiderTermAmount.ToString();
                }

                if (partins != null)
                    spouseinsuredamount1 = partins.RideroirAmount.ToString();

                double adbamount = adbamount1.ToDouble();
                double termamount = termamount1.ToDouble();
                double oiramount = spouseinsuredamount1.ToDouble();
                double insuranceamount;
                double insuredamount = insuranceamount = insuredAmount;

                char plangroupcode = FamilyProductCode[0];
                string productCode = customerPlanDetail.ProductCode;

                double premiumamount = premiumAmount;
                int numberofyears = 0;
                int age = customer.Age.ToInt();


                double minimumpremium = RulesService.GetValue(RulesService.Rules.MINIMUM_YEARLY_PREMIUM);
                double minimumtotalpremium = RulesService.GetValue(RulesService.Rules.MINIMUM_TOTAL_PREMIUM);
                double minimuminsuredamount = RulesService.GetValue(RulesService.Rules.MINIMUM_INSURED_AMT);
                double maximuminsuredamount = RulesService.GetValue(RulesService.Rules.MAXIMUM_INSURED_AMT);

                int insuranceunderage = RulesService.GetValue(RulesService.Rules.INSURANCE_UNDERAGE).ToInt();
                double underagemaxinsuredamount = RulesService.GetValue(RulesService.Rules.MAXIMUM_INSURED_AMT_UNDERAGE);

                bool isFixed = ProductIsFixed;
                int minimumcontributionperiod = RulesService.GetValue(RulesService.Rules.MINIMUM_CONTRIBUTION_PERIOD).ToInt();
                int maximumcontributionperiod = RulesService.GetValue(RulesService.Rules.MAXIMUM_CONTRIBUTION_PERIOD).ToInt();

                var frequencyTypeModel = Utility.GetIllusDropDownByType(Utility.DropDownType.FrequencyType)
             .Single(o => o.FrequencyTypeCode == customerPlanDetail.FrequencyTypeCode);

                var insuredAnnuityAmount = (
                    new string[] { "AXS", "HRZ", "EDU", "SCH" }.Contains(productCode) ?
                    customerPlanDetail.AnnuityAmount :
                    customerPlanDetail.InsuredAmount).ToDouble();

                msgToValidate = validateGSdata(productCode, frequencyTypeModel.FrequencyValue.Value, customerPlanDetail.PremiumAmount.ToDouble(), insuredAnnuityAmount);

                if (!String.IsNullOrEmpty(msgToValidate))
                    return msgToValidate;

                int insuranceminage = RulesService.GetValue(RulesService.Rules.INSURANCE_MIN_AGE).ToInt();
                int insurancemaxage = RulesService.GetValue(RulesService.Rules.INSURANCE_MAX_AGE).ToInt();
                int maxage = RulesService.GetValue(RulesService.Rules.MAX_AGE).ToInt();

                double mininsuredamount = RulesService.GetValue(RulesService.Rules.MINIMUM_INSURED_AMT);
                double maxinsuredamount = RulesService.GetValue(RulesService.Rules.MAXIMUM_INSURED_AMT);

                string contributiontypecode = customerPlanDetail.ContributionTypeCode;

                char calculatetypecode = customerPlanDetail.CalculateTypeCode[0];
                char plantypecode = customerPlanDetail.PlanTypeCode[0];

                var annualpremium = GetAnnualizedPremium(productCode, customerPlanDetail.InvestmentProfilePercent.ToDouble(), frequencyTypeModel.FrequencyValue.GetValueOrDefault(), oIllusDataManager.GetFrequencyCost(productCode, customerPlanDetail.FrequencyTypeCode).Single().FrequencyCost.GetValueOrDefault(), customerPlanDetail.PremiumAmount.ToDouble());

                double minumumpremium = RulesService.GetValue(RulesService.Rules.MINIMUM_YEARLY_PREMIUM);
                double minumumtotalpremium = RulesService.GetValue(RulesService.Rules.MINIMUM_TOTAL_PREMIUM);
                double initialcontributionamount = customerPlanDetail.InitialContribution.ToDouble();

                if (contributiontypecode == Utility.EContributionType.NumberOfYears.Code())
                    numberofyears = customerPlanDetail.ContributionPeriod;
                else if (contributiontypecode == Utility.EContributionType.UntilAge.Code())
                    numberofyears = customerPlanDetail.ContributionUntilAge - age + 1;
                else if (contributiontypecode == Utility.EContributionType.Continuous.Code())
                    numberofyears = maxage - age;


                int frequencytypevalue = customerPlanDetail.Frequency.GetValueOrDefault();
                double actualannualpremium = premiumamount * frequencytypevalue;

                if (plangroupcode == Plangroups.LIFE)
                {
                    if (minimumpremium > 0 && (actualannualpremium < minumumpremium))
                        return Resources.ShouldBeGreaterThan.SFormat(Resources.Premium, (Math.Round(Math.Ceiling(minumumpremium / frequencytypevalue) / 100, 2) * 100));

                    if (minumumtotalpremium > 0 && ((actualannualpremium * numberofyears + initialcontributionamount) < minumumtotalpremium))
                        return Resources.ShouldBeGreaterThan.SFormat(Resources.TotalPremium, minumumtotalpremium);

                    if ((maximuminsuredamount > 0 && insuranceamount > maximuminsuredamount) || (minimuminsuredamount > 0 && insuranceamount < minimuminsuredamount))
                        return Resources.FieldIsNotBetween.SFormat(Resources.RegularInsurance, maximuminsuredamount, minimuminsuredamount);

                    if (maximuminsuredamount > 0 && (termamount + insuranceamount) > maximuminsuredamount)
                        return Resources.CannotBeGreaterThan.SFormat(
                            "{0} + {1}".SFormat(Resources.RegularInsurance, Resources.AdditionalInsurance), maximuminsuredamount);

                    if ((termamount) > insuranceamount)
                        return Resources.CannotBeGreaterThan.SFormat(Resources.AdditionalInsurance, Resources.RegularInsurance);

                    if ((adbamount) > insuranceamount)
                        return Resources.CannotBeGreaterThan.SFormat(Resources.AdbAmount, Resources.RegularInsurance);

                    if ((oiramount) > insuranceamount && customerPlanDetail.RiderAdbAmount > 0)
                        return Resources.CannotBeGreaterThan.SFormat(Resources.SpouseOtherInsuredRiderAmount, Resources.RegularInsurance);

                    if (numberofyears <= 0)
                        return Resources.Premium_payment_years_need_to_be_more_than_1;

                    if (maxage > 0 && (numberofyears + age) > maxage)
                        return Resources.CannotBeGreaterThan.SFormat(
                            "{0} + {1}".SFormat(Resources.ContributionPeriodLabel, Resources.Age)
                            , maxage);

                    if ((minimumcontributionperiod > 0 && numberofyears <= minimumcontributionperiod) || (maximumcontributionperiod > 0 && numberofyears <= maximumcontributionperiod))
                        return Resources.FieldIsNotBetween.SFormat(Resources.PremiumPaymentYears, maximumcontributionperiod, minimumcontributionperiod);

                    if ((insurancemaxage > 0 && customer.Age.ToInt() > insurancemaxage) || (insuranceminage > 0 && customer.Age.ToInt() < insuranceminage))
                        return Resources.FieldIsNotBetween.SFormat(Resources.Age, Resources.Minimum_a, Resources.Maximum_a + " " + Resources.InsuranceAge);


                    if (maxage > 0 && (numberofyears + age) > maxage)
                        return Resources.ShouldNotBeGreaterThan.SFormat(
                      "{0} + {1}".SFormat(Resources.ContributionPeriodLabel, Resources.Age)
                      , Resources.MaximumAge);

                    if (underagemaxinsuredamount > 0 && (age <= insuranceunderage) && (insuredamount > underagemaxinsuredamount))
                        return Resources.ShouldNotBeGreaterThan.SFormat(Resources.UnderageInsuredAmount, underagemaxinsuredamount);

                    if ((mininsuredamount > 0 && insuredamount < mininsuredamount) || (maxinsuredamount > 0 && insuredamount > maxinsuredamount))
                        return Resources.FieldIsNotBetween.SFormat(Resources.InsuredAmount, mininsuredamount, maxinsuredamount);
                }
                else if (plangroupcode == Plangroups.RETIREMENT || plangroupcode == Plangroups.EDUCATION)
                {
                    int annuityperiod = customerPlanDetail.RetirementPeriod.ToInt();
                    int defermentperiod = customerPlanDetail.DefermentPeriod.ToInt();
                    int contributionperiod = customerPlanDetail.ContributionPeriod;
                    int minperiodpriorannuity = RulesService.GetValue(RulesService.Rules.MINIMUM_PERIOD_PRIOR_ANNUITY).ToInt();
                    int maxageendannuity = RulesService.GetValue(RulesService.Rules.MAXIMUM_AGE_END_ANNUITY).ToInt();

                    int maxannuityperiod = RulesService.GetValue(RulesService.Rules.MAXIMUM_ANNUITY_PERIOD).ToInt();
                    int minannuityperiod = RulesService.GetValue(RulesService.Rules.MINIMUM_ANNUITY_PERIOD).ToInt();

                    double maxannuityamountyear = RulesService.GetValue(RulesService.Rules.MAXIMUM_ANNUITY_AMT_YEAR);
                    double minannuityamountyear = RulesService.GetValue(RulesService.Rules.MINIMUM_ANNUITY_AMT_YEAR);

                    double maxannuityamount = RulesService.GetValue(RulesService.Rules.MAXIMUM_ANNUITY_AMT);
                    double minannuityamount = RulesService.GetValue(RulesService.Rules.MINIMUM_ANNUITY_AMT);

                    double annuityamountyear = annuityAmount;
                    double totalannuityamount = annuityamountyear * annuityperiod;

                    int maxcontributionperiod = RulesService.GetValue(RulesService.Rules.MAXIMUM_CONTRIBUTION_PERIOD).ToInt();

                    if (minimumpremium > 0 && (actualannualpremium < minumumpremium))
                        return Resources.ShouldBeGreaterThan.SFormat(Resources.Premium, (Math.Round(Math.Ceiling(minumumpremium / frequencytypevalue) / 100, 2) * 100));

                    if (minumumtotalpremium > 0 && ((actualannualpremium * numberofyears + initialcontributionamount) < minumumtotalpremium))
                        return Resources.ShouldBeGreaterThan.SFormat(Resources.TotalPremium, minumumtotalpremium);

                    if ((maxannuityamountyear > 0 && annuityamountyear > maxannuityamountyear) || (minannuityamountyear > 0 && annuityamountyear < minannuityamountyear))
                        return Resources.FieldIsNotBetween.SFormat(Resources.AnnuityAmount, minannuityamountyear, maxannuityamountyear);

                    if ((maxannuityamount > 0 && totalannuityamount > maxannuityamount) || (minannuityamount > 0 && totalannuityamount < minannuityamount))
                        return Resources.FieldIsNotBetween.SFormat(Resources.TotalAnnuityAmount, minannuityamount, maxannuityamount);

                    if (numberofyears <= 0)
                        return Resources.Premium_payment_years_need_to_be_more_than_1;

                    if ((insurancemaxage > 0 && age > insurancemaxage) || (insuranceminage > 0 && age < insuranceminage))
                        return Resources.FieldIsNotBetween.SFormat(Resources.Age, Resources.Minimum_a, Resources.Maximum_a + " " + Resources.InsuranceAge);

                    if (plantypecode == Plantypes.INSURED)
                    {
                        if (maxage > 0 && (numberofyears + age) > maxage)
                            return Resources.CannotBeGreaterThan.SFormat(
                          "{0} + {1}".SFormat(Resources.ContributionPeriodLabel, Resources.Age)
                          , maxage);

                        if (underagemaxinsuredamount > 0 && (age <= insuranceunderage) && (insuredamount > underagemaxinsuredamount))
                            return Resources.ShouldNotBeGreaterThan.SFormat(Resources.UnderageInsuredAmount, underagemaxinsuredamount);

                        if (minimuminsuredamount > 0 && maxinsuredamount > 0 && ((insuredamount < mininsuredamount) || (insuredamount > maxinsuredamount)))
                            return Resources.FieldIsNotBetween.SFormat(Resources.InsuredAmount, mininsuredamount, maxinsuredamount);
                    }

                    if ((minimumcontributionperiod > 0 && numberofyears < minimumcontributionperiod) || (maximumcontributionperiod > 0 && numberofyears > maximumcontributionperiod))
                        return Resources.FieldIsNotBetween.SFormat(Resources.PremiumPaymentYears, minimumcontributionperiod, maximumcontributionperiod);
                }
                else if (plangroupcode == Plangroups.TERMINSURANCE)
                {
                    if (minimumpremium > 0 && actualannualpremium < minumumpremium)
                        return Resources.ShouldBeGreaterThan.SFormat(Resources.Premium, Math.Round(Math.Ceiling(minumumpremium / frequencytypevalue) / 100, 2) * 100);

                    if (minumumtotalpremium > 0 && ((actualannualpremium * numberofyears + initialcontributionamount) < minumumtotalpremium))
                        return Resources.ShouldBeGreaterThan.SFormat(Resources.TotalPremium, minumumtotalpremium);

                    if (((maximuminsuredamount > 0 && (insuranceamount) > maximuminsuredamount) || (minimuminsuredamount > 0 && insuranceamount < minimuminsuredamount)) && age > 17)
                        return Resources.FieldIsNotBetween.SFormat(Resources.RegularInsurance, minimuminsuredamount, maximuminsuredamount);

                    if (minimumpremium > 0 && (premiumamount * frequencytypevalue) < minimumpremium)
                        return Resources.CannotBeLessThan.SFormat(Resources.PremiumAmount, minimuminsuredamount);

                    if (maxinsuredamount > 0 && (termamount + insuranceamount) > maximuminsuredamount)
                        return Resources.CannotBeGreaterThan.SFormat(
                    "{0} + {1}".SFormat(Resources.RegularInsurance, Resources.AdditionalInsurance)
                    , maximuminsuredamount);

                    if (termamount > insuranceamount)
                        return Resources.CannotBeGreaterThan.SFormat(Resources.AdditionalInsurance, Resources.RegularInsurance);

                    if (adbamount > insuranceamount)
                        return Resources.CannotBeGreaterThan.SFormat(Resources.AdbAmount, Resources.RegularInsurance);

                    if (oiramount > insuranceamount)
                        return Resources.CannotBeGreaterThan.SFormat(Resources.SpouseOtherInsuredRiderAmount, Resources.RegularInsurance);

                    if (numberofyears <= 0)
                        return Resources.Premium_payment_years_need_to_be_more_than_1;

                    if (maxage > 0 && (numberofyears + age) > maxage)
                        return Resources.CannotBeGreaterThan.SFormat(
                    "{0} + {1}".SFormat(Resources.ContributionPeriodLabel, Resources.Age)
                    , maxage);

                    if (insurancemaxage > 0 && (age < insuranceminage || age > insurancemaxage))
                        return Resources.FieldIsNotBetween.SFormat(Resources.Age, Resources.Maximum_a, Resources.Minimum_a + " " + Resources.InsuranceAge);

                    if (maxage > 0 && (numberofyears + age) > maxage)
                        return Resources.ShouldNotBeGreaterThan.SFormat(
                    "{0} + {1}".SFormat(Resources.ContributionPeriodLabel, Resources.Age)
                    , Resources.MaximumAge);

                    if (underagemaxinsuredamount > 0 && age <= insuranceunderage && insuredamount > underagemaxinsuredamount)
                        return Resources.CannotBeGreaterThan.SFormat(Resources.UnderageInsuredAmount, underagemaxinsuredamount);

                    if (((minimuminsuredamount > 0 && insuredamount < mininsuredamount) || (maxinsuredamount > 0 && insuredamount > maxinsuredamount)) && age > 17)
                        return Resources.FieldIsNotBetween.SFormat(Resources.InsuredAmount, mininsuredamount, maxinsuredamount);

                    if ((minimumcontributionperiod > 0 && numberofyears <= minimumcontributionperiod) || (maximumcontributionperiod > 0 && numberofyears <= maximumcontributionperiod))
                        return Resources.FieldIsNotBetween.SFormat(Resources.PremiumPaymentYears, minimumcontributionperiod, maximumcontributionperiod);
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        #endregion
        #endregion
    }
}