﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using DATA.UnderWriting.Data;
using DATA.UnderWriting.Repositories.Base;
using Entity.UnderWriting.Entities;

namespace DATA.UnderWriting.Repositories.Global
{
    public class PolicyRepository : GlobalRepository
    {
        public PolicyRepository(GlobalEntityDataModel globalModel, GlobalEntities globalModelExtended, string globalconexionStringForAdo) : base(globalModel, globalModelExtended, globalconexionStringForAdo) { }

        public virtual Policy.PlanData GetPlanData(Policy.Parameter policyParameter)
        {
            Policy.PlanData result;
            IEnumerable<SP_GET_POLICY_PLAN_DATA_Result> temp;

            temp = globalModel.SP_GET_POLICY_PLAN_DATA(
                    policyParameter.CorpId,
                    policyParameter.RegionId,
                    policyParameter.CountryId,
                    policyParameter.DomesticregId,
                    policyParameter.StateProvId,
                    policyParameter.CityId,
                    policyParameter.OfficeId,
                    policyParameter.CaseSeqNo,
                    policyParameter.HistSeqNo
                    );

            result = temp
                .Select(pd => new Policy.PlanData
                {
                    CorpId = pd.Corp_Id,
                    RegionId = pd.Region_Id,
                    CountryId = pd.Country_Id,
                    DomesticregId = pd.Domesticreg_Id,
                    StateProvId = pd.State_Prov_Id,
                    CityId = pd.City_Id,
                    OfficeId = pd.Office_Id,
                    CaseSeqNo = pd.Case_Seq_No,
                    HistSeqNo = pd.Hist_Seq_No,
                    CurrencyId = pd.Currency_Id,
                    PolicyStatusId = pd.Policy_Status_Id,
                    PolicyNo = pd.Policy_No,
                    InsuredName = pd.InsuredName,
                    OwnerName = pd.OwnerName,
                    RetentionAmount = pd.RetentionAmount,
                    RopAmount = pd.RopAmount,
                    DefermentPeriod = pd.Deferment_Period,
                    RetirementPeriod = pd.Retirement_Period,
                    PolicyStatus = pd.PolicyStatus,
                    PlanName = pd.PlanName,
                    PlanType = pd.PlanType,
                    Currency = pd.currency,
                    AdditionalInsured = pd.AdditionalInsured,
                    InsuredAmount = pd.Insured_Amount,
                    CumulativeRisk = pd.CumulativeRisk,
                    CumulativeRiskCount = pd.CumulativeRiskCount,
                    ReinsuredAmount = pd.ReinsuredAmount,
                    ReinsuredAmountTotal = pd.ReinsuredAmountTotal,
                    InitialContribution = pd.InitialContribution,
                    AnnualPremium = pd.AnnualPremium,
                    MinAnnualPremium = pd.Min_Annual_Premium,
                    TargetPremium = pd.TargetPremium,
                    ContributionYears = pd.ContributionYears,
                    RetentionAmountTotal = pd.RetentionAmountTotal,
                    PeriodicPremium = pd.PeriodicPremium,
                    PeriodicPremiumUncalculated = pd.Periodic_Premium,
                    ContributionTypeId = pd.Contribution_Type_Id,
                    GoalAmount = pd.Goal_Amount,
                    GoalAtAge = pd.Goal_At_Age,
                    PaymentCycle = pd.PaymentCycle,
                    ProfileTypeDesc = pd.PROFILE_TYPE_DESC,
                    SubmittedDate = pd.SubmittedDate,
                    EffectiveDate = pd.EffectiveDate,
                    CompletedDate = pd.CompletedDate,
                    ExpiredDate = pd.ExpiredDate,
                    LastPaymentDate = pd.LastPaymentDate,
                    TerminationDate = pd.TerminationDate,
                    ContributionPeriod = pd.ContributionPeriod,
                    RetirementBeginDate = pd.Retirement_Begin_Date,
                    RetirementEndDate = pd.Retirement_End_Date,
                    BlId = pd.Bl_Id,
                    BlTypeId = pd.Bl_Type_Id,
                    InsuredContactId = pd.InsuredContactID,
                    OwnerContactId = pd.OwnerContactID,
                    AdditionalContactId = pd.AdditionalContactID,
                    ProfileTypeId = pd.Profile_Type_Id,
                    InvestProductDateId = pd.Invest_Product_Date_Id,
                    InvestmentProductDate = pd.Investment_Product_Date,
                    ProductId = pd.Product_Id,
                    PolicySerieId = pd.Policy_Serie_Id,
                    PaymentFreqTypeId = pd.Payment_Freq_Type_Id,
                    PaymentFreqId = pd.Payment_Freq_Id,
                    InsuredFirstName = pd.Insured_First_Name,
                    InsuredMiddleName = pd.Insured_Middle_Name,
                    InsuredLastName = pd.Insured_Lastname,
                    InsuredSecondLastName = pd.Insured_Second_Lastname,
                    AgeAtStartOfRetirement = pd.AgeAtStartOfRetirement,
                    BenefitAmount = pd.Benefit_Amount,
                    MinimunPremiunAmount = pd.Minimun_Premiun_Amount,
                    ReturnAmount = pd.Return_Amount,
                    OfficeDesc = pd.Office_Desc,
                    CountryOfficeDesc = pd.Country_Office_Desc,
                    AgentFullName = pd.AgentFullName,
                    PaymentFreqTypeDesc = pd.Payment_Freq_Type_Desc,
                    FuturePayment = pd.FuturePayment,
                    DetailMethod = pd.DetailMethod,
                    PaymentMethod = pd.PaymentMethod,
                    PaymentStatusDesc = pd.Payment_Status_Desc,
                    DesignatedPensionerName = pd.DesignatedPensionerName,
                    InsuredPeriod = pd.InsuredPeriod,
                    RetirementAmount = pd.Retirement_Amount,
                    ProductTypeId = pd.Product_Type_Id,
                    AgentId = pd.Agent_Id,
                    NameKey = pd.Name_Key,
                    InsuredDob = pd.Insured_Dob,
                    OwnerDob = pd.Owner_Dob,
                    InsuredAddDob = pd.InsuredAdd_Dob,
                    DeductibleTypeId = pd.Deductible_Type_Id,
                    DeductibleCategoryId = pd.Deductible_Category_Id,
                    DeductibleManualValue = pd.Deductible_Manual_Value,
                    ProviderTypeId = pd.Provider_Type_Id,
                    ProviderId = pd.Provider_Id,
                    ProviderName = pd.Provider_Name,
                    SpecialPayment = pd.Special_Payment,
                    InterestRate = pd.Interest_Rate,
                    DestinationFund = pd.Destination_Of_Funds,
                    MonthContributionPeriod = pd.Month_Contribution_Period,
                    MonthsInsuredPeriod = pd.Months_Insured_Period,
                    ContributionMonths = pd.Contribution_Months,
                    InsuranceDurationMonths = pd.Insurance_Duration_Months,
                    Fraction_Surcharge = pd.Fraction_Surcharge //Bmarroquin 28-04-2017
                    ,
                    NetAnnualPremium = pd.Net_Annual_Premium //Bmarroquin 05-05-2017

                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual Policy GetPolicy(Policy.Parameter policyParameter)
        {
            Policy result;
            IEnumerable<SP_GET_PL_POLICY_Result> temp;

            temp = globalModel.SP_GET_PL_POLICY(
                    policyParameter.CorpId,
                    policyParameter.RegionId,
                    policyParameter.CountryId,
                    policyParameter.DomesticregId,
                    policyParameter.StateProvId,
                    policyParameter.CityId,
                    policyParameter.OfficeId,
                    policyParameter.CaseSeqNo,
                    policyParameter.HistSeqNo
                    );

            result = temp
                .Select(p => new Policy
                {
                    CorpId = p.Corp_Id,
                    RegionId = p.Region_Id,
                    CountryId = p.Country_Id,
                    DomesticregId = p.Domesticreg_Id,
                    StateProvId = p.State_Prov_Id,
                    CityId = p.City_Id,
                    OfficeId = p.Office_Id,
                    CaseSeqNo = p.Case_Seq_No,
                    HistSeqNo = p.Hist_Seq_No,
                    ContactId = p.Contact_Id_Owner,
                    DocumentType = p.Document_Type,
                    PolicyNo = p.Policy_No,
                    PolicySerieId = p.Policy_Serie_Id,
                    CurrencyId = p.Currency_Id,
                    QuotationCurrencyId = p.Quotation_Currency_Id,
                    PolicyCurrencyId = p.Policy_Currency_Id,
                    PaymentsCurrencyId = p.Payments_Currency_Id,
                    BussinessLineType = p.Bussiness_Line_Type,
                    BussinessLineId = p.Bussiness_Line_Id,
                    ProductId = p.Product_Id,
                    Reinsured = p.Reinsured,
                    ReinsuredAmount = p.Reinsured_Amount,
                    SubmitDate = p.Submit_Date,
                    VanishDate = p.Vanish_Date,
                    TermDate = p.Term_Date,
                    ContributionTypeId = p.Contribution_Type_Id,
                    GoalAmount = p.Goal_Amount,
                    GoalAtAge = p.Goal_At_Age,
                    PeriodicPremium = p.Periodic_Premium,
                    PolicyEffectiveDate = p.Policy_Effective_Date,
                    BenefitEndingDate = p.Benefit_Ending_Date,
                    BenefitPeriod = p.Benefit_Period,
                    MinimunPremiunAmount = p.Minimun_Premiun_Amount,
                    InsuredAmount = p.Insured_Amount,
                    TargetPremium = p.Target_Premium,
                    InitialContribution = p.Initial_Contribution,
                    YearContributionPeriod = p.Year_Contribution_Period,
                    CollectionStatusId = p.Collection_Status_Id,
                    BenefitAmount = p.Benefit_Amount,
                    AnnualPremium = p.Annual_Premium,
                    AnnualBenefit = p.Annual_Benefit,
                    EndingContributionDate = p.Ending_Contribution_Date,
                    InitialBenefitDate = p.Initial_Benefit_Date,
                    ModalPremium = p.Modal_Premium,
                    Rate = p.Rate,
                    ExcessPremium = p.Excess_Premium,
                    RolId = p.Rol_Id,
                    PolicyStatusId = p.Policy_Status_Id,
                    SumARisk = p.Sum_A_Risk,
                    RopAmount = p.Rop_Amount,
                    InitialPremium = p.Initial_Premium,
                    InsurancePremium = p.Insurance_Premium,
                    RetirementAmount = p.Retirement_Amount,
                    AnnualRetirement = p.Annual_Retirement,
                    MinAnnualPremium = p.Min_Annual_Premium,
                    InsuranceDuration = p.Insurance_Duration,
                    ContributionYears = p.Contribution_Years,
                    DefermentPeriod = p.Deferment_Period,
                    RetirementPeriod = p.Retirement_Period,
                    ReturnAmount = p.Return_Amount,
                    InsuredPeriod = p.Insured_Period,
                    InvestmentProfile = p.Investment_Profile,
                    ContributionEndDate = p.Contribution_End_Date,
                    RetirementBeginDate = p.Retirement_Begin_Date,
                    ExpirationDate = p.Expiration_Date,
                    RetirementEndDate = p.Retirement_End_Date,
                    PaidThrough = p.Paid_Through,
                    Priority = p.Priority,
                    DeductibleTypeId = p.Deductible_Type_Id,
                    DeductibleCategoryId = p.Deductible_Category_Id,
                    DeductibleManualValue = p.Deductible_Manual_Value,
                    CreateDate = p.Create_Date,
                    TaxPremium = p.Tax_Premium,
                    TaxPercentage = p.Tax_Percentage,
                    Agent_Id = p.Agent_Id.GetValueOrDefault(),
                    Agent_Name = p.Agent_Name,
                    StatusNameKey = p.StatusNameKey,
                    AgentCode = p.Agent_Code,
                    NameId = p.Name_Id,
                    DiscountPercentage = p.Discount_Percentage.ConvertToNoNullable(),
                    DiscountPremium = p.Discount_Premium.ConvertToNoNullable(),
                    MonthContributionPeriod = p.Month_Contribution_Period,
                    MonthsInsuredPeriod = p.Months_Insured_Period,
                    ContributionMonths = p.Contribution_Months,
                    InsuranceDurationMonths = p.Insurance_Duration_Months,
                    PaymentFreqTypeId = p.Payment_Freq_Type_Id,
                    PaymentFreqTypeDesc = p.Payment_Freq_Type_Desc,
                    PolicyNoTemp = p.Policy_No_Temp,
                    PolicyNoMain = p.Policy_No_Main,
                    Fraction_Surcharge = p.Fraction_Surcharge, //Bmarroquin 28-04-2017,                    
                    Net_Annual_Premium = p.Net_Annual_Premium, //Lgonzalez 06-05-2017
                    MonthlyPayment = p.MonthlyPayment,
                    Financed = p.Financed,
                    Period = p.Period,
                    AgentIsFinancial = p.AgentIsFinancial,
                    InvoiceNumber = p.InvoiceNumber
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual int? AddContactToPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo
            , int? contactId, int contactTypeId, int contactRoleTypeId, int agentId, int userId)
        {
            int? result;
            SP_SET_CONTACT_POLICY_Result temp;

            temp = globalModel.SP_SET_CONTACT_POLICY(
               coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
               , contactId, contactTypeId, contactRoleTypeId, agentId, userId
                ).FirstOrDefault();

            if (temp != null)
                result = temp.Contact_Id;
            else
                result = -1;

            return
                result;
        }

        public virtual int DeletePolicy(Policy.Parameter policyParameter)
        {
            int result;
            IEnumerable<SP_DELETE_PL_POLICY_Result> temp;

            result = -1;

            temp = globalModel.SP_DELETE_PL_POLICY(
                    policyParameter.CorpId,
                    policyParameter.RegionId,
                    policyParameter.CountryId,
                    policyParameter.DomesticregId,
                    policyParameter.StateProvId,
                    policyParameter.CityId,
                    policyParameter.OfficeId,
                    policyParameter.CaseSeqNo,
                    policyParameter.HistSeqNo,
                    policyParameter.UserId.ConvertToNoNullable()
                    );

            return
                result;
        }

        public virtual IEnumerable<Policy.Profile> GetProfilePersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int profileTypeId)
        {
            IEnumerable<Policy.Profile> result;
            IEnumerable<SP_GET_PROFILE_PERSONALIZED_Result> temp;

            temp = globalModel.SP_GET_PROFILE_PERSONALIZED(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, profileTypeId);

            result = temp
                .Select(cc => new Policy.Profile
                {
                    CorpId = cc.Corp_Id,
                    CountryId = cc.Country_Id,
                    CurrencyDesc = cc.Currency_Desc,
                    CurrencyId = cc.Currency_Id,
                    GlobalInvestmentProductDate = cc.Global_Investment_Product_Date,
                    GlobalInvstProfilePercent = cc.Global_Invst_Profile_Percent,
                    HasProfile = cc.HasProfile,
                    InvstDateId = cc.Invst_Date_Id,
                    PolicyNo = cc.Policy_No,
                    ProductCaseSeqNo = cc.Product_Case_Seq_No,
                    ProductCityId = cc.Product_City_Id,
                    ProductCorpId = cc.Product_Corp_ID,
                    ProductCountryId = cc.Product_Country_Id,
                    ProductDomesticregId = cc.Product_Domesticreg_Id,
                    ProductHistSeqNo = cc.Product_Hist_Seq_No,
                    ProductInvestmentProductDate = cc.Product_Investment_Product_Date,
                    ProductInvestProductDateId = cc.Product_Invest_Product_Date_Id,
                    ProductInvstProfilePercent = cc.Product_Invst_Profile_Percent,
                    ProductOfficeId = cc.Product_Office_Id,
                    ProductRegionId = cc.Product_Region_Id,
                    ProductStateProvId = cc.Product_State_Prov_Id,
                    ProfileTypeDesc = cc.PROFILE_TYPE_DESC,
                    ProfileTypeId = cc.Profile_Type_Id,
                    RegionId = cc.Region_Id,
                    StockExChangeId = cc.STOCK_EXCHANGE_ID,
                    SymbolAbbr = cc.SYMBOL_ABBR,
                    SymbolDesc = cc.SYMBOL_DESC,
                    SymbolId = cc.SYMBOL_ID
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.Profile> GetProfileNoPersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int profileTypeId)
        {
            IEnumerable<Policy.Profile> result;
            IEnumerable<SP_GET_PROFILE_NO_PERSONALIZED_Result> temp;

            temp = globalModel.SP_GET_PROFILE_NO_PERSONALIZED(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, profileTypeId);

            result = temp
                .Select(cc => new Policy.Profile
                {
                    CorpId = cc.Corp_Id,
                    CountryId = cc.Country_Id,
                    CurrencyDesc = cc.Currency_Desc,
                    CurrencyId = cc.Currency_Id,
                    GlobalInvestmentProductDate = cc.Investment_Profile_Date,
                    GlobalInvstProfilePercent = cc.Invst_Profile_Percent,
                    HasProfile = cc.HasProfile,
                    InvstDateId = cc.Invst_Date_Id,
                    PolicyNo = cc.Policy_No,
                    ProductCaseSeqNo = cc.Product_Case_Seq_No,
                    ProductCityId = cc.Product_City_Id,
                    ProductCorpId = cc.Product_Corp_ID,
                    ProductCountryId = cc.Product_Country_Id,
                    ProductDomesticregId = cc.Product_Domesticreg_Id,
                    ProductHistSeqNo = cc.Product_Hist_Seq_No,
                    ProductInvestmentProductDate = cc.Product_Investment_Product_Date,
                    ProductInvestProductDateId = cc.Product_Invest_Product_Date_Id,
                    ProductInvstProfilePercent = cc.Invst_Profile_Percent,
                    ProductOfficeId = cc.Product_Office_Id,
                    ProductRegionId = cc.Product_Region_Id,
                    ProductStateProvId = cc.Product_State_Prov_Id,
                    ProfileTypeDesc = cc.PROFILE_TYPE_DESC,
                    ProfileTypeId = cc.Profile_Type_Id,
                    RegionId = cc.Region_Id,
                    StockExChangeId = cc.STOCK_EXCHANGE_ID,
                    SymbolAbbr = cc.SYMBOL_ABBR,
                    SymbolDesc = cc.SYMBOL_DESC,
                    SymbolId = cc.SYMBOL_ID
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.PolicySummaryByPolicy> GetSummaryByPolicy(Policy.Parameter policyParameter)
        {
            IEnumerable<Policy.PolicySummaryByPolicy> result;
            IEnumerable<SP_GET_SUMMARY_BY_POLICY_Result> temp;

            temp = globalModel.SP_GET_SUMMARY_BY_POLICY(
                    policyParameter.CorpId,
                    policyParameter.RegionId,
                    policyParameter.CountryId,
                    policyParameter.DomesticregId,
                    policyParameter.StateProvId,
                    policyParameter.CityId,
                    policyParameter.OfficeId,
                    policyParameter.CaseSeqNo,
                    policyParameter.HistSeqNo
                    );

            result = temp
                .Select(pd => new Policy.PolicySummaryByPolicy
                {
                    RoleDesc = pd.Role_Desc,
                    Gender = pd.Gender,
                    MaritalStatusDesc = pd.Marital_Status_Desc,
                    PolicyNo = pd.Policy_No,
                    ContactId = pd.Contact_Id,
                    ContactFullName = pd.ContactFullName,
                    CountryOfBirth = pd.CountryOfBirth,
                    CountryOfResidence = pd.CountryOfResidence,
                    Compliance = pd.Compliance,
                    IsPreApproved = pd.IsPreApproved.ConvertToNoNullable()
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.PolicySummaryByContact> GetSummaryByContact(int contactId)
        {
            IEnumerable<Policy.PolicySummaryByContact> result;
            IEnumerable<SP_GET_SUMMARY_BY_CONTACT_Result> temp;

            temp = globalModel.SP_GET_SUMMARY_BY_CONTACT(contactId);

            result = temp
                .Select(pc => new Policy.PolicySummaryByContact
                {
                    PlanType = pc.PlanType,
                    ContactId = pc.ContactId,
                    RoleDesc = pc.RoleDesc,
                    PolicyNo = pc.PolicyNo,
                    PlanName = pc.PlanName,
                    EffectiveDate = pc.EffectiveDate,
                    RiskClass = pc.RiskClass,
                    BenefitAmount = pc.BenefitAmount
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.PolicyCommentSummary> GetCommentSummary(Policy.Parameter policyParameter)
        {
            IEnumerable<Policy.PolicyCommentSummary> result;
            IEnumerable<SP_GET_SUMMARY_COMMENTS_Result> temp;

            temp = globalModel.SP_GET_SUMMARY_COMMENTS(
                    policyParameter.CorpId,
                    policyParameter.RegionId,
                    policyParameter.CountryId,
                    policyParameter.DomesticregId,
                    policyParameter.StateProvId,
                    policyParameter.CityId,
                    policyParameter.OfficeId,
                    policyParameter.CaseSeqNo,
                    policyParameter.HistSeqNo
                    );

            result = temp
                .Select(pc => new Policy.PolicyCommentSummary
                {
                    StepId = pc.Step_Id,
                    OriginatedBy = pc.Originated_By.ConvertToNoNullable(),
                    TypeDesc = pc.TypeDesc,
                    OriginatedByName = pc.Originated_By_Name,
                    NoteDesc = pc.Note_Desc,
                    CreateDate = pc.Create_Date.ConvertToNoNullable(),
                    IsDefault = pc.IsDefault.ConvertToNoNullable()
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.RequirementSummary> GetRequirementSummary(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
       , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? requirementCatId, int? requirementTypeId)
        {
            IEnumerable<Policy.RequirementSummary> result;
            IEnumerable<SP_GET_SUMMARY_REQUIREMENTS_Result> temp;

            temp = globalModel.SP_GET_SUMMARY_REQUIREMENTS(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId
                , requirementCatId, requirementTypeId);

            result = temp
                .Select(rs => new Policy.RequirementSummary
                {
                    CorpId = rs.Corp_Id,
                    RegionId = rs.Region_Id,
                    CountryId = rs.Country_Id,
                    DomesticregId = rs.Domesticreg_Id,
                    StateProvId = rs.State_Prov_Id,
                    CityId = rs.City_Id,
                    OfficeId = rs.Office_Id,
                    CaseSeqNo = rs.Case_Seq_No,
                    HistSeqNo = rs.Hist_Seq_No,
                    ContactId = rs.Contact_Id,
                    RequirementCatId = rs.Requirement_Cat_Id,
                    RequirementTypeId = rs.Requirement_Type_Id,
                    RequirementId = rs.Requirement_Id,
                    RequirementDocId = rs.Requirement_Doc_Id,
                    DocTypeId = rs.Doc_Type_Id,
                    DocCategoryId = rs.Doc_Category_Id,
                    DocumentId = rs.Document_Id,
                    RequirementTypeDesc = rs.Requirement_Type_Desc,
                    ReceivedDate = rs.Received_Date,
                    RequestedBy = rs.Requested_By,
                    RequestedDate = rs.Requested_Date,
                    SendToReinsurance = rs.Send_To_Reinsurance,
                    IsManual = rs.IsManual,
                    Automatic = rs.Automatic,
                    HasDocument = rs.HasDocument
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.PaymentSummary> GetPaymentSummary(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId
            , int? officeId, int? caseSeqNo, int? histSeqNo, int OwnerContactId)
        {
            IEnumerable<Policy.PaymentSummary> result;
            IEnumerable<SP_GET_SUMMARY_PAYMENTS_Result> temp;

            temp = globalModel.SP_GET_SUMMARY_PAYMENTS(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, OwnerContactId);

            result = temp
                .Select(ps => new Policy.PaymentSummary
                {
                    PolicyNo = ps.Policy_No,
                    DueAmount = ps.Due_Amount,
                    DueDate = ps.Due_Date,
                    PaidAmount = ps.Paid_Amount,
                    PaidDate = ps.Paid_Date,
                    BasePremium = ps.Base_Premium,
                    Exceptionalpremium = ps.Exceptional_premium,
                    BaseCommision = ps.Base_Commision,
                    BaseCommision2 = ps.Base_Commision2,
                    ExceptionalCommisions = ps.Exceptional_Commisions,
                    Exceptional2Commisions = ps.Exceptional2_Commisions,
                    PaymentStatusDesc = ps.Payment_Status_Desc
                })
                .ToArray();

            return
                result;
        }


        public virtual IEnumerable<Policy.AssignCase> GetPolicyAssingCase(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo)
        {
            IEnumerable<Policy.AssignCase> result;
            IEnumerable<SP_GET_PCY_ASSIGN_CASE_Result> temp;

            temp = globalModel.SP_GET_PCY_ASSIGN_CASE(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);

            result = temp
                .Select(ps => new Policy.AssignCase
                {
                    RoleName = ps.RoleName,
                    Underwriter_Id = ps.Underwriter_Id
                })
                .ToArray();

            return
                result;
        }

        public virtual int DeleteContactRole(int corpId, int regionId, int countryId, int domesticregId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int contactRoleTypeId, int userId)
        {
            int result;
            IEnumerable<SP_DELETE_PL_PCY_CONTACTS_Result> temp;

            result = -1;

            temp = globalModel.SP_DELETE_PL_PCY_CONTACTS(
                corpId,
                regionId,
                countryId,
                domesticregId,
                stateProvId,
                cityId,
                officeId,
                caseSeqNo,
                histSeqNo,
                contactId,
                contactRoleTypeId,
                userId
                );

            return
                result;
        }
        public virtual int SetPaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            int result;
            IEnumerable<SP_SET_PL_PCY_PAYMENTS_FREQ_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_PL_PCY_PAYMENTS_FREQ(
                paymentFreq.CorpId,
                paymentFreq.RegionId,
                paymentFreq.CountryId,
                paymentFreq.DomesticregId,
                paymentFreq.StateProvId,
                paymentFreq.CityId,
                paymentFreq.OfficeId,
                paymentFreq.CaseSeqNo,
                paymentFreq.HistSeqNo,
                paymentFreq.PaymentFreqTypeId,
                paymentFreq.PaymentFreqId,
                paymentFreq.PaymentDate,
                paymentFreq.UserId
                );
            return
                result;
        }

        public virtual int DeletePaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            int result;
            IEnumerable<SP_DELETE_PL_PCY_PAYMENTS_FREQ_Result> temp;

            result = -1;

            temp = globalModel.SP_DELETE_PL_PCY_PAYMENTS_FREQ(
                paymentFreq.CorpId,
                paymentFreq.RegionId,
                paymentFreq.CountryId,
                paymentFreq.DomesticregId,
                paymentFreq.StateProvId,
                paymentFreq.CityId,
                paymentFreq.OfficeId,
                paymentFreq.CaseSeqNo,
                paymentFreq.HistSeqNo,
                paymentFreq.PaymentFreqTypeId,
                paymentFreq.PaymentFreqId,
                paymentFreq.UserId
                );
            return
                result;
        }

        public virtual int SetPolicy(Policy policy)
        {
            int result;
            IEnumerable<SP_SET_PL_POLICY_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_PL_POLICY(
                policy.CorpId,
                policy.RegionId,
                policy.CountryId,
                policy.DomesticregId,
                policy.StateProvId,
                policy.CityId,
                policy.OfficeId,
                policy.CaseSeqNo,
                policy.HistSeqNo,
                policy.DocumentType,
                policy.PolicyNo,
                policy.PolicySerieId,
                policy.CurrencyId,
                policy.QuotationCurrencyId,
                policy.PolicyCurrencyId,
                policy.PaymentsCurrencyId,
                policy.BussinessLineType,
                policy.BussinessLineId,
                policy.ProductId,
                policy.DeductibleTypeId,
                policy.DeductibleCategoryId,
                policy.DeductibleManualValue,
                policy.Reinsured,
                policy.ReinsuredAmount,
                policy.SubmitDate,
                policy.VanishDate,
                policy.TermDate,
                policy.ContributionTypeId,
                policy.GoalAmount,
                policy.GoalAtAge,
                policy.PeriodicPremium,
                policy.PolicyEffectiveDate,
                policy.BenefitEndingDate,
                policy.BenefitPeriod,
                policy.MinimunPremiunAmount,
                policy.InsuredAmount,
                policy.TargetPremium,
                policy.InitialContribution,
                policy.YearContributionPeriod,
                policy.InsuredPeriod,
                policy.CollectionStatusId,
                policy.BenefitAmount,
                policy.AnnualPremium,
                policy.AnnualBenefit,
                policy.EndingContributionDate,
                policy.InitialBenefitDate,
                policy.ModalPremium,
                policy.Rate,
                policy.ExcessPremium,
                policy.RolId,
                policy.PolicyStatusId,
                policy.SumARisk,
                policy.RopAmount,
                policy.InitialPremium,
                policy.InsurancePremium,
                policy.RetirementAmount,
                policy.AnnualRetirement,
                policy.MinAnnualPremium,
                policy.InsuranceDuration,
                policy.ContributionYears,
                policy.DefermentPeriod,
                policy.RetirementPeriod,
                policy.ReturnAmount,
                policy.InvestmentProfile,
                policy.ContributionEndDate,
                policy.RetirementBeginDate,
                policy.ExpirationDate,
                policy.RetirementEndDate,
                policy.PaidThrough,
                policy.Priority,
                policy.TaxPremium,
                policy.TaxPercentage,
                policy.UserId,
                policy.ProviderTypeId,
                policy.ProviderId,
                policy.DiscountPremium,
                policy.DiscountPercentage,
                policy.MonthContributionPeriod,
                policy.MonthsInsuredPeriod,
                policy.ContributionMonths,
                policy.InsuranceDurationMonths,
                null,//policy_No_Temp
                null, //policy_No_Main
                policy.Fraction_Surcharge, //Lgonzalez 28-04-2017
                policy.Net_Annual_Premium, //Lgonzalez 04-05-2017
                policy.MonthlyPayment,
                policy.Financed,
                policy.Period,
                policy.directDebit,
                policy.loanPeitionNo,
                string.Empty, null, null, null, null
                );

            return
                result;
        }

        public virtual IEnumerable<Policy.CategoryDocument> GetCategoryDocument(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int? docTypeId, int? docCategoryId, int languageId)
        {
            IEnumerable<Policy.CategoryDocument> result;
            IEnumerable<SP_GET_CATEGORY_DOCUMENT_BY_POLICY_Result> temp;

            temp = globalModel.SP_GET_CATEGORY_DOCUMENT_BY_POLICY(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, docTypeId, docCategoryId, languageId);

            result = temp
                .Select(cd => new Policy.CategoryDocument
                {
                    DocTypeId = cd.Doc_Type_Id,
                    DocCategoryId = cd.Doc_Category_Id,
                    DocumentId = cd.Document_Id,
                    DocumentName = cd.Document_Name,
                    DocCategoryDesc = cd.Doc_Category_Desc,
                    UploadDate = cd.Upload_Date,
                    DocumentDate = cd.Document_Date,
                    HasDocument = cd.HasDocument.ConvertToNoNullable(),
                    KeyName = cd.KeyName
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.AgentChainDetail> GetAgentChainDetail(Policy.Parameter policyParameter)
        {
            IEnumerable<Policy.AgentChainDetail> result;
            IEnumerable<SP_GET_AGENT_CHAIN_DETAIL_Result> temp;

            temp = globalModel.SP_GET_AGENT_CHAIN_DETAIL(
                policyParameter.CorpId,
                    policyParameter.RegionId,
                    policyParameter.CountryId,
                    policyParameter.DomesticregId,
                    policyParameter.StateProvId,
                    policyParameter.CityId,
                    policyParameter.OfficeId,
                    policyParameter.CaseSeqNo,
                    policyParameter.HistSeqNo
                    );

            result = temp
                .Select(acd => new Policy.AgentChainDetail
                {
                    OrderId = acd.Order_Id,
                    FullName = acd.FullName,
                    CommTable = acd.CommTable,
                    ProductDescription = acd.Product_Desc,
                    OfficeDescription = acd.Office_Desc,
                    PositionDescription = acd.Agent_Sub_Type_Position_Desc
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.PolicyCommunication> GetPolicyCommunication(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? callNoteId)
        {
            IEnumerable<Policy.PolicyCommunication> result;
            IEnumerable<SP_GET_CONTACT_COMMUNICATION_CALL_Result> temp;

            temp = globalModel.SP_GET_CONTACT_COMMUNICATION_CALL(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, callNoteId);

            result = temp
                .Select(pc => new Policy.PolicyCommunication
                {
                    CorpId = pc.Corp_Id,
                    RegionId = pc.Region_Id,
                    CountryId = pc.Country_Id,
                    DomesticregId = pc.Domesticreg_Id,
                    StateProvId = pc.State_Prov_Id,
                    CityId = pc.City_Id,
                    OfficeId = pc.Office_Id,
                    CaseSeqNo = pc.Case_Seq_No,
                    HistSeqNo = pc.Hist_Seq_No,
                    CommunicationTypeId = pc.Communication_Type_Id,
                    CommunicationTypeDesc = pc.Communication_Type_Desc,
                    CallId = pc.Call_Id,
                    CallNoteId = pc.Call_Note_Id,
                    NoteTypeId = pc.Note_Type_Id,
                    NoteTypeDesc = pc.Note_Type_Desc,
                    PriorityId = pc.Priority_Id,
                    PriorityDesc = pc.Priority_Desc,
                    CategoryId = pc.Category_Id,
                    CategoryDesc = pc.Category_Desc,
                    ResultId = pc.Result_Id,
                    ResultDesc = pc.Result_Desc,
                    Subject = pc.Subject,
                    ShortText = pc.Short_Text,
                    LargeText = pc.Large_Text,
                    PersonToContact = pc.Person_To_Contact,
                    ContactedPerson = pc.Contacted_Person,
                    CallDirectionOutbound = pc.Call_Direction_Outbound,
                    ContactedBy = pc.Contacted_By,
                    Duration = pc.Duration,
                    Timeless = pc.Timeless,
                    Recurring = pc.Recurring,
                    Result = pc.Result,
                    CompletedDate = pc.Completed_Date,
                    CompletedByUserId = pc.Completed_By_User_Id,
                    Attachment = pc.Attachment,
                    HasCall = pc.Has_Call,
                    DateAdded = pc.Date_Added,
                    DateModified = pc.Date_Modified,
                    OriginatedBy = pc.Originated_By,
                    StepTypeId = pc.Step_Type_Id,
                    StepId = pc.Step_Id,
                    StepCaseNo = pc.Step_Case_No,
                    DateSent = pc.Date_Sent,
                    CallregNotehistoryid = pc.Callreg_Notehistoryid,
                    HistoryId = pc.History_Id,
                    StartDate = pc.Start_Date,
                    EndDate = pc.End_Date,
                    CallerIdNumber = pc.CallerId_Number,
                    CallerIdName = pc.CallerId_Name,
                    OutboundNumber = pc.Outbound_Number,
                    RecordingFile = pc.Recording_File,
                    ProcessedDate = pc.Processed_Date,
                    ProcessedBy = pc.Processed_By
                })
                .ToArray();

            return
                result;
        }

        public virtual int RecentViewed(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int underwriterId, int viewId, DateTime viewDate, int userId)
        {
            int result;
            IEnumerable<SP_SET_PL_PCY_RECENT_VIEWS_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_PL_PCY_RECENT_VIEWS(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , underwriterId, viewId, viewDate, userId);

            return
                result;
        }

        public virtual int DeleteContactAndRole(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int contactId, int contactRoleTypeId, int userId)
        {
            int result;
            IEnumerable<SP_DELETE_PL_POLICY_ALLCONTACT_AND_ROLE_Result> temp;

            try
            {
                result = -1;
                temp = globalModel.SP_DELETE_PL_POLICY_ALLCONTACT_AND_ROLE(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , contactId, contactRoleTypeId, userId);
            }
            catch (Exception)
            {
                result = -2;
            }

            return
                result;
        }

        #region UnderwritingCall
        public virtual DataTable GetUnderwritingCallTemplateData(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int languageId)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            SqlConnection connection;

            query = "exec [Underwriting].[SP_GET_UNDERWRITING_CALL] @Corp_Id,@Region_Id,@Country_Id,@Domesticreg_Id,@State_Prov_Id,@City_Id,@Office_Id,@Case_Seq_No,@Hist_Seq_No,@Step_Type_Id,@Step_Id,@Step_Case_No,@Language_Id";
            connection = new SqlConnection(base.globalconexionStringForAdo);
            command = new SqlCommand(query, connection);
            dT = new DataTable();

            command.Parameters.Add(new SqlParameter("@Corp_Id", coprId));
            command.Parameters.Add(new SqlParameter("@Region_Id", regionId));
            command.Parameters.Add(new SqlParameter("@Country_Id", countryId));
            command.Parameters.Add(new SqlParameter("@Domesticreg_Id", domesticRegId));
            command.Parameters.Add(new SqlParameter("@State_Prov_Id", stateProvId));
            command.Parameters.Add(new SqlParameter("@City_Id", cityId));
            command.Parameters.Add(new SqlParameter("@Office_Id", officeId));
            command.Parameters.Add(new SqlParameter("@Case_Seq_No", caseSeqNo));
            command.Parameters.Add(new SqlParameter("@Hist_Seq_No", histSeqNo));
            command.Parameters.Add(new SqlParameter("@Step_Type_Id", stepTypeId));
            command.Parameters.Add(new SqlParameter("@Step_Id", stepId));
            command.Parameters.Add(new SqlParameter("@Step_Case_No", stepCaseNo));
            command.Parameters.Add(new SqlParameter("@Language_Id", languageId));

            try
            {
                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dT);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return
                dT;
        }

        public virtual IEnumerable<Policy.UnderwritingCallTemplate> GetUnderwritingCallTemplate(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int languageId)
        {
            IEnumerable<Policy.UnderwritingCallTemplate> result;
            IEnumerable<SP_GET_UNDERWRITING_CALL_TEMPLATE_Result> temp;

            temp = globalModel.SP_GET_UNDERWRITING_CALL_TEMPLATE(
                    coprId,
                    regionId,
                    countryId,
                    domesticRegId,
                    stateProvId,
                    cityId,
                    officeId,
                    caseSeqNo,
                    histSeqNo,
                    languageId
                );

            result = temp
                .Select(t => new Policy.UnderwritingCallTemplate
                {
                    CorpId = t.Corp_Id.ConvertToNoNullable(),
                    RegionId = t.Region_Id.ConvertToNoNullable(),
                    CountryId = t.Country_Id.ConvertToNoNullable(),
                    DomesticregId = t.Domesticreg_Id.ConvertToNoNullable(),
                    StateProvId = t.State_Prov_Id.ConvertToNoNullable(),
                    CityId = t.City_Id.ConvertToNoNullable(),
                    OfficeId = t.Office_Id.ConvertToNoNullable(),
                    CaseSeqNo = t.Case_Seq_No.ConvertToNoNullable(),
                    HistSeqNo = t.Hist_Seq_No.ConvertToNoNullable(),
                    StepTypeId = t.Step_Type_Id.ConvertToNoNullable(),
                    StepId = t.Step_Id.ConvertToNoNullable(),
                    StepCaseNo = t.Step_Case_No.ConvertToNoNullable(),
                    TemplateId = t.Template_Id,
                    LanguageId = t.Language_Id,
                    CategoryId = t.Category_Id,
                    LanguageDesc = t.Language_Desc,
                    CategoryDesc = t.Category_Desc,
                    IsClose = t.IsClose.ConvertToNoNullable(),
                    Html = t.Html,
                    QuestionHtml = t.QuestionHtml,
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.UnderwritingCallTemplate> GetUnderwritingCallTemplateByCategory(int coprId, int categoryId, int languageId)
        {
            IEnumerable<Policy.UnderwritingCallTemplate> result;
            IEnumerable<SP_GET_UNDERWRITING_CALL_TEMPLATE_CATEGORY_Result> temp;

            temp = globalModel.SP_GET_UNDERWRITING_CALL_TEMPLATE_CATEGORY(
                    coprId,
                    categoryId,
                    languageId
                );

            result = temp
                .Select(t => new Policy.UnderwritingCallTemplate
                {
                    TemplateId = t.Template_Id,
                    LanguageId = t.Language_Id,
                    CategoryId = t.Category_Id,
                    LanguageDesc = t.Language_Desc,
                    CategoryDesc = t.Category_Desc,
                    Html = t.Html,
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.UnderwritingCallComment> GetUnderwritingCallComments(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo)
        {
            IEnumerable<Policy.UnderwritingCallComment> result;
            IEnumerable<SP_GET_UNDERWRITING_CALL_COMMENTS_Result> temp;

            temp = globalModel.SP_GET_UNDERWRITING_CALL_COMMENTS(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, stepTypeId, stepId, stepCaseNo);

            result = temp
                .Select(ucc => new Policy.UnderwritingCallComment
                {
                    CorpId = ucc.Corp_Id.ConvertToNoNullable(),
                    RegionId = ucc.Region_Id.ConvertToNoNullable(),
                    CountryId = ucc.Country_Id.ConvertToNoNullable(),
                    DomesticregId = ucc.Domesticreg_Id.ConvertToNoNullable(),
                    StateProvId = ucc.State_Prov_Id.ConvertToNoNullable(),
                    CityId = ucc.City_Id.ConvertToNoNullable(),
                    OfficeId = ucc.Office_Id.ConvertToNoNullable(),
                    CaseSeqNo = ucc.Case_Seq_No.ConvertToNoNullable(),
                    HistSeqNo = ucc.Hist_Seq_No.ConvertToNoNullable(),
                    CommentTypeId = ucc.Comment_Type_Id,
                    CommentId = ucc.Comment_Id,
                    CommentTypeDesc = ucc.Comment_Type_Desc,
                    Comments = ucc.Comments
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.SecurityQuestion> GetUnderwritingCallSecurityQuestion(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int contactId)
        {
            IEnumerable<Policy.SecurityQuestion> result;
            IEnumerable<SP_GET_UNDERWRITING_CALL_CONTACT_QUESTIONS_Result> temp;

            temp = globalModel.SP_GET_UNDERWRITING_CALL_CONTACT_QUESTIONS(
                    coprId,
                    regionId,
                    countryId,
                    domesticRegId,
                    stateProvId,
                    cityId,
                    officeId,
                    caseSeqNo,
                    histSeqNo,
                    stepTypeId,
                    stepId,
                    stepCaseNo,
                    contactId
                );

            result = temp
                .Select(cq => new Policy.SecurityQuestion
                {
                    CorpId = cq.Corp_Id,
                    QuestionId = cq.Question_Id,
                    ContactId = cq.Contact_Id,
                    QuestionDesc = cq.Question_Desc,
                    Answer = cq.Answer,
                    Response = cq.Response.ConvertToNoNullable(),
                })
                .ToArray();

            return
                result;
        }

        public virtual int SetUnderwritingCallSecurityQuestion(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int questionId, int contactId, bool answer, int userId)
        {
            int result;
            IEnumerable<SP_SET_UW_STEPS_UNDERWRITING_CALL_SEC_ANSWER_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_UW_STEPS_UNDERWRITING_CALL_SEC_ANSWER(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , stepTypeId, stepId, stepCaseNo, questionId, contactId, answer, userId);

            return
                result;
        }

        public virtual int SetUnderwritingCallTabAnswer(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int tabId, bool answer, int userId)
        {
            int result;
            IEnumerable<SP_SET_UW_STEPS_UNDERWRITING_CALL_TAB_ANSWER_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_UW_STEPS_UNDERWRITING_CALL_TAB_ANSWER(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , stepTypeId, stepId, stepCaseNo, tabId, answer, userId);

            return
                result;
        }
        #endregion

        public virtual IEnumerable<Policy.ProductByContact> GetProductByContactAndRole(int? contactTypeId, int contactId, int languageId)
        {
            IEnumerable<Policy.ProductByContact> result;
            IEnumerable<SP_GET_PRODUCT_BY_CONTACT_Result> temp;

            temp = globalModel.SP_GET_PRODUCT_BY_CONTACT(contactTypeId, contactId, languageId);

            result = temp
                .Select(p => new Policy.ProductByContact
                {
                    CorpId = p.Corp_Id,
                    RegionId = p.Region_Id,
                    CountryId = p.Country_Id,
                    DomesticregId = p.Domesticreg_Id,
                    StateProvId = p.State_Prov_Id,
                    CityId = p.City_Id,
                    OfficeId = p.Office_Id,
                    CaseSeqNo = p.Case_Seq_No,
                    HistSeqNo = p.Hist_Seq_No,
                    PolicyNo = p.Policy_No,
                    ProductDate = p.Product_Date,
                    PolicyStatusDesc = p.Policy_Status_Desc,
                    ProductDesc = p.Product_Desc,
                    BlDesc = p.Bl_Desc,
                    RoleDesc = p.Role_Desc
                })
                .ToArray();

            return
                result;
        }

        public virtual Policy.Call SetCall(Policy.Call call)
        {
            Policy.Call result;
            IEnumerable<SP_SET_EN_CALLS_Result> temp;

            temp = globalModel.SP_SET_EN_CALLS(
                call.CorpId,
                call.RegionId,
                call.CountryId,
                call.DomesticregId,
                call.StateProvId,
                call.CityId,
                call.OfficeId,
                call.CaseSeqNo,
                call.HistSeqNo,
                call.CommunicationTypeId,
                call.CallId,
                call.StepTypeId,
                call.StepId,
                call.StepCaseNo,
                call.ContactId,
                call.ContactRoleTypeId,
                call.DateSent,
                call.CallregNotehistoryid,
                call.HistoryId,
                call.StartDate,
                call.EndDate,
                call.Duration,
                call.CallerIdNumber,
                call.CallerIdName,
                call.OutboundNumber,
                call.RecordingFile,
                call.ProcessedDate,
                call.ProcessedBy,
                call.DateAdded,
                call.DateModified,
                call.OriginatedBy,
                call.UserId
                )
                .ToArray();

            result = temp
                .Select(c => new Policy.Call
                {
                    CorpId = c.Corp_Id.ConvertToNoNullable(),
                    RegionId = c.Region_Id.ConvertToNoNullable(),
                    CountryId = c.Country_Id.ConvertToNoNullable(),
                    DomesticregId = c.Domesticreg_Id.ConvertToNoNullable(),
                    StateProvId = c.State_Prov_Id.ConvertToNoNullable(),
                    CityId = c.City_Id.ConvertToNoNullable(),
                    OfficeId = c.Office_Id.ConvertToNoNullable(),
                    CaseSeqNo = c.Case_Seq_No.ConvertToNoNullable(),
                    HistSeqNo = c.Hist_Seq_No.ConvertToNoNullable(),
                    CommunicationTypeId = c.Communication_Type_Id.ConvertToNoNullable(),
                    CallId = c.Call_Id
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual int SetValidTab(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int projectId, int tabId, bool isValid, int userId)
        {
            int result;
            IEnumerable<SP_SET_PL_PCY_TAB_VALID_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_PL_PCY_TAB_VALID(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, projectId, tabId, isValid, userId);

            return
                result;
        }

        public virtual IEnumerable<Policy.Tab> GetTabValidation(Policy.Parameter policyParameter)
        {
            IEnumerable<Policy.Tab> result;
            IEnumerable<SP_GET_TAB_VALIDATION_Result> temp;

            temp = globalModel.SP_GET_TAB_VALIDATION(
                    policyParameter.CorpId,
                    policyParameter.RegionId,
                    policyParameter.CountryId,
                    policyParameter.DomesticregId,
                    policyParameter.StateProvId,
                    policyParameter.CityId,
                    policyParameter.OfficeId,
                    policyParameter.CaseSeqNo,
                    policyParameter.HistSeqNo
                    );

            result = temp
                .Select(t => new Policy.Tab
                {
                    CorpId = t.Corp_Id,
                    ProjectId = t.Project_Id,
                    TabId = t.Tab_Id,
                    TabDesc = t.Tab_Desc,
                    IsValid = t.IsValid.ConvertToNoNullable(),
                })
                .ToArray();

            return
                result;
        }

        public virtual bool GetTabValidationRequirement(Policy.Parameter policy)
        {
            bool result;
            ObjectResult<bool?> temp;

            temp = globalModel.SP_GET_TAB_VALIDATION_REQUIREMENT(
                policy.CorpId,
                policy.RegionId,
                policy.CountryId,
                policy.DomesticregId,
                policy.StateProvId,
                policy.CityId,
                policy.OfficeId,
                policy.CaseSeqNo,
                policy.HistSeqNo);

            result = temp.FirstOrDefault().ConvertToNoNullable();

            return
                result;
        }

        public virtual IEnumerable<Policy.Contact> GetContactPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? contactRoleTypeId)
        {
            IEnumerable<Policy.Contact> result;
            IEnumerable<SP_GET_PL_PCY_CONTACTS_Result> temp;

            temp = globalModel.SP_GET_PL_PCY_CONTACTS(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, contactRoleTypeId);

            result = temp
                .Select(cp => new Policy.Contact
                {
                    CorpId = cp.Corp_Id,
                    RegionId = cp.Region_Id,
                    CountryId = cp.Country_Id,
                    DomesticregId = cp.Domesticreg_Id,
                    StateProvId = cp.State_Prov_Id,
                    CityId = cp.City_Id,
                    OfficeId = cp.Office_Id,
                    CaseSeqNo = cp.Case_Seq_No,
                    HistSeqNo = cp.Hist_Seq_No,
                    ContactId = cp.Contact_Id,
                    ContactRoleTypeId = cp.Contact_Role_Type_Id,
                    RegionOfResidenceId = cp.Region_Of_Residence_Id,
                    CountryOfResidenceId = cp.Country_Of_Residence_Id,
                    RegionOfBirthId = cp.Region_Of_Birth_Id,
                    CountryOfBirthId = cp.Country_Of_Birth_Id,
                    BlockConfirmationCall = cp.Block_Confirmation_Call,
                    HealthWeigth = cp.Health_Weigth,
                    HealthWeigthTypeId = cp.Health_Weigth_Type_Id,
                    HealthHeight = cp.Health_Height,
                    HealthHeigthTypeId = cp.Health_Heigth_Type_Id,
                    HealthAge = cp.Health_Age,
                    HealthGender = cp.Health_Gender,
                    HealthSmoke = cp.Health_Smoke,
                    HealthExcercise = cp.Health_Excercise,
                    HealthDrugs = cp.Health_Drugs,
                    HealthSystolic = cp.Health_Systolic,
                    HealthDiastolic = cp.Health_Diastolic,
                    HealthLastMedVisit = cp.Health_LastMedVisit,
                    HealthLastMedReason = cp.Health_LastMed_Reason,
                    HealthLastMedResult = cp.Health_LastMed_Result,
                    HealthDrName = cp.Health_Dr_Name,
                    HealthDrAddress = cp.Health_Dr_Address,
                    HealthDrPhonePrefix = cp.Health_Dr_Phone_Prefix,
                    HealthDrPhoneArea = cp.Health_Dr_Phone_Area,
                    HealthDrPhoneNum = cp.Health_Dr_Phone_Num,
                    HealthMedication = cp.Health_Medication,
                    AsstTotalAssets = cp.Asst_Total_Assets,
                    AsstRealEstate = cp.Asst_Real_Estate,
                    AsstPersonalEffects = cp.Asst_Personal_Effects,
                    AsstVehicle = cp.Asst_Vehicle,
                    AsstMachineryEqpmnt = cp.Asst_Machinery_Eqpmnt,
                    AsstStockBonds = cp.Asst_Stock_Bonds,
                    AsstOtherAssets = cp.Asst_Other_Assets,
                    LblTotalLiabilities = cp.Lbl_Total_Liabilities,
                    LblMachineryEqpmnt = cp.Lbl_Machinery_Eqpmnt,
                    LblNotePayable = cp.Lbl_Note_Payable,
                    LblBankDebts = cp.Lbl_Bank_Debts,
                    LblPersonalDebts = cp.Lbl_Personal_Debts,
                    LblMortgageDebts = cp.Lbl_Mortgage_Debts,
                    LblOutstandingTaxes = cp.Lbl_Outstanding_Taxes,
                    LblShortTermsLoans = cp.Lbl_Short_Terms_Loans,
                    LblOtherLiabilities = cp.Lbl_Other_Liabilities,
                    FncTotalEstateAmnt = cp.Fnc_Total_Estate_Amnt,
                    FncAnnualRevMainActvt = cp.Fnc_Annual_Rev_Main_Actvt,
                    FncAnnualIncomeOtherJobs = cp.Fnc_Annual_Income_Other_Jobs,
                    FncAnnualIncomeInvst = cp.Fnc_Annual_Income_Invst,
                    FncAnnualIncomeTrade = cp.Fnc_Annual_Income_Trade,
                    HomeStatusId = cp.Home_Status_Id,
                    LaborPlayedId = cp.Labor_Played_Id,
                    LineOfBusiness = cp.Line_Of_Business,
                    LineOfBusiness2 = cp.Line_Of_Business_2,
                    CompanyName = cp.Company_Name,
                    LengthWorkYear = cp.Length_Work_Year,
                    LengthWorkMonth = cp.Length_Work_Month,
                    Labortasks = cp.Labor_tasks,
                    CompanyActivity = cp.Company_Activity,
                    CompanyFoundationDate = cp.Company_Foundation_Date,
                    OccupGroupTypeId = cp.OccupGroup_Type_Id,
                    OccupationId = cp.Occupation_Id,
                    RelationshiptoAgent = cp.Relationship_to_Agent,
                    RelationshiptoOwner = cp.Relationship_to_Owner,
                    AnnualPersonalIncome = cp.Annual_Personal_Income,
                    AnnualFamilyIncome = cp.Annual_Family_Income,
                    Smoker = cp.Smoker,
                    MaritalStatId = cp.Marital_Stat_Id,
                    BeneficiaryTypeId = cp.Beneficiary_Type_Id,
                    PrimaryBeneficiaryId = cp.Primary_Beneficiary_Id,
                    PrimaryBeneficiary = cp.Primary_Beneficiary,
                    RelationshipToOwnerId = cp.Relationship_To_Owner_Id,
                    RelToPrimaryBenefId = cp.Rel_To_Primary_Benef_Id,
                    BenefitsPercent = cp.Benefits_Percent,
                    Comment = cp.Comment,
                    HealthFullTimeStudent = cp.Health_Full_Time_Student,
                    Dob = cp.Dob,
                    Gender = cp.Gender,
                    FirstName = cp.First_Name,
                    MiddleName = cp.Middle_Name,
                    FirstLastName = cp.Lastname,
                    SecondLastName = cp.Second_Lastname,
                    FullName = cp.Full_Name,
                    SeqNo = cp.Seq_No,
                    ContactIdType = cp.Contact_Id_Type,
                    Id = cp.Id,
                    ValidDate = cp.Valid_Date,
                    ExpireDate = cp.Expire_date,
                    companyActivityId = cp.Company_Activity_Id,
                    companyStructureId = cp.Company_Structure_Id,
                    finalBeneficiaryOptionId = cp.Final_Beneficiary_Option_Id
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.Contact> GetPolicyAddInsured(Policy.Parameter parameter)
        {
            IEnumerable<Policy.Contact> result;
            IEnumerable<SP_GET_POLICY_ADDINSURED_Result> temp;

            temp = globalModel.SP_GET_POLICY_ADDINSURED(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.LanguageId);

            result = temp
                .Select(cp => new Policy.Contact
                {
                    CorpId = cp.Corp_Id,
                    RegionId = cp.Region_Id,
                    CountryId = cp.Country_Id,
                    DomesticregId = cp.Domesticreg_Id,
                    StateProvId = cp.State_Prov_Id,
                    CityId = cp.City_Id,
                    OfficeId = cp.Office_Id,
                    CaseSeqNo = cp.Case_Seq_No,
                    HistSeqNo = cp.Hist_Seq_No,
                    ContactId = cp.Contact_Id,
                    FirstName = cp.First_Name,
                    MiddleName = cp.Middle_Name,
                    FirstLastName = cp.Lastname,
                    SecondLastName = cp.Second_Lastname,
                    RelationshiptoOwnerId = cp.Relationship_to_Owner,
                    RelationshiptoOwnerDesc = cp.Relationship_Desc,
                    Gender = cp.Gender,
                    MaritalStatId = cp.Marital_Stat_Id,
                    Dob = cp.Dob,
                    GenderDesc = cp.Gender_Desc,
                    MaritalStatusDesc = cp.Marital_Status_Desc,
                    Id = cp.Id
                })
                .ToArray();

            return
                result;
        }

        public virtual int SetContactPolicy(Policy.Contact contact)
        {
            int result;
            IEnumerable<SP_SET_PL_PCY_CONTACTS_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_PL_PCY_CONTACTS(
                    contact.CorpId,
                    contact.RegionId,
                    contact.CountryId,
                    contact.DomesticregId,
                    contact.StateProvId,
                    contact.CityId,
                    contact.OfficeId,
                    contact.CaseSeqNo,
                    contact.HistSeqNo,
                    contact.ContactId,
                    contact.ContactRoleTypeId,
                    contact.RegionOfResidenceId,
                    contact.CountryOfResidenceId,
                    contact.RegionOfBirthId,
                    contact.CountryOfBirthId,
                    contact.BlockConfirmationCall,
                    contact.HealthMemberPremium,
                    contact.HealthWeigth,
                    contact.HealthWeigthTypeId,
                    contact.HealthHeight,
                    contact.HealthHeigthTypeId,
                    contact.HealthAge,
                    contact.HealthGender,
                    contact.HealthSmoke,
                    contact.HealthExcercise,
                    contact.HealthDrugs,
                    contact.HealthSystolic,
                    contact.HealthDiastolic,
                    contact.HealthLastMedVisit,
                    contact.HealthLastMedReason,
                    contact.HealthLastMedResult,
                    contact.HealthDrName,
                    contact.HealthDrAddress,
                    contact.HealthDrPhonePrefix,
                    contact.HealthDrPhoneArea,
                    contact.HealthDrPhoneNum,
                    contact.HealthMedication,
                    null, null, null,
                    contact.AsstTotalAssets,
                    contact.AsstRealEstate,
                    contact.AsstPersonalEffects,
                    contact.AsstVehicle,
                    contact.AsstMachineryEqpmnt,
                    contact.AsstStockBonds,
                    contact.AsstOtherAssets,
                    contact.LblTotalLiabilities,
                    contact.LblMachineryEqpmnt,
                    contact.LblNotePayable,
                    contact.LblBankDebts,
                    contact.LblPersonalDebts,
                    contact.LblMortgageDebts,
                    contact.LblOutstandingTaxes,
                    contact.LblShortTermsLoans,
                    contact.LblOtherLiabilities,
                    contact.FncTotalEstateAmnt,
                    contact.FncAnnualRevMainActvt,
                    contact.FncAnnualIncomeOtherJobs,
                    contact.FncAnnualIncomeInvst,
                    contact.FncAnnualIncomeTrade,
                    contact.HomeStatusId,
                    contact.LaborPlayedId,
                    contact.LineOfBusiness,
                    contact.LineOfBusiness2,
                    contact.CompanyName,
                    contact.LengthWorkYear,
                    contact.LengthWorkMonth,
                    contact.Labortasks,
                    contact.CompanyActivity,
                    contact.CompanyFoundationDate,
                    contact.OccupGroupTypeId,
                    contact.OccupationId,
                    contact.StudentStatusId,
                    contact.RelationshiptoAgent,
                    contact.RelationshiptoOwner,
                    contact.AnnualPersonalIncome,
                    contact.AnnualFamilyIncome,
                    contact.Smoker,
                    contact.MaritalStatId,
                    contact.BeneficiaryTypeId,
                    contact.PrimaryBeneficiaryId,
                    contact.PrimaryBeneficiary,
                    contact.RelationshipToOwnerId,
                    contact.RelToPrimaryBenefId,
                    contact.BenefitsPercent,
                    contact.Comment,
                    contact.TipoRiesgoNameKey,
                    contact.InvoiceTypeId,
                    contact.UserId,
                    contact.InterestRate,
                    contact.SpecialPayment,
                    contact.DestinationFund,
                    contact.ForeignLicense,
                    contact.finalBeneficiaryOptionId,
                    contact.pepFormularyOptionId,
                    contact.companyStructureId,
                    contact.companyActivityId,
                    contact.workAddress,
                    contact.placeOfBirth,
                    contact.typeOfPerson,
                    contact.managerName,
                    contact.managerPepOptionId
                );

            return
                result;
        }

        public virtual bool? GetCheckPolicyActive(string PolicyNo)
        {
            bool? result = false;
            var temp = globalModel.SP_GET_CHECK_POLICY_ACTIVE(PolicyNo).FirstOrDefault();
            result = temp.GetValueOrDefault();
            return
                  result;
        }


        #region InvestmentProfile
        public virtual IEnumerable<Policy.InvestProfilePersonalized> GetInvestProfilePersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int profileTypeId)
        {
            IEnumerable<Policy.InvestProfilePersonalized> result;
            IEnumerable<SP_GET_PL_PCY_INVEST_PROFILE_PERSONALIZED_Result> temp;

            temp = globalModel.SP_GET_PL_PCY_INVEST_PROFILE_PERSONALIZED(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, profileTypeId);

            result = temp
                .Select(ipp => new Policy.InvestProfilePersonalized
                {
                    CorpId = ipp.Corp_Id.ConvertToNoNullable(),
                    RegionId = ipp.Region_Id.ConvertToNoNullable(),
                    CountryId = ipp.Country_Id.ConvertToNoNullable(),
                    DomesticregId = ipp.Domesticreg_Id.ConvertToNoNullable(),
                    StateProvId = ipp.State_Prov_Id.ConvertToNoNullable(),
                    CityId = ipp.City_Id.ConvertToNoNullable(),
                    OfficeId = ipp.Office_Id.ConvertToNoNullable(),
                    CaseSeqNo = ipp.Case_Seq_No.ConvertToNoNullable(),
                    HistSeqNo = ipp.Hist_Seq_No.ConvertToNoNullable(),
                    ProfileTypeId = ipp.Profile_Type_Id.ConvertToNoNullable(),
                    InvestProductDateId = ipp.Invest_Product_Date_Id,
                    SymbolId = ipp.Symbol_Id,
                    PolicyNo = ipp.Policy_No,
                    SerieDesc = ipp.Serie_Desc,
                    ProductDesc = ipp.Product_Desc,
                    ProfileTypeDesc = ipp.PROFILE_TYPE_DESC,
                    CurrencyDesc = ipp.Currency_Desc,
                    ProfileType = ipp.ProfileType,
                    InvestmentProfileDate = ipp.Investment_Profile_Date,
                    InvstProductDate = ipp.Invst_Product_Date,
                    InvstProfilePercent = ipp.Invst_Profile_Percent,
                    StockExchangeId = ipp.Stock_Exchange_Id,
                    ProjectionRate = ipp.Projection_Rate,
                    InvestmentCurrency = ipp.Investment_Currency,
                    MinPercentAllowed = ipp.Min_Percent_Allowed,
                    MaxPercentAllowed = ipp.Max_Percent_Allowed,
                    InitialValidDate = ipp.initial_valid_date,
                    EndValidDate = ipp.end_valid_date,
                    Symbol = ipp.Symbol,
                    SymbolDesc = ipp.SymbolDesc
                })
                .ToArray();

            return
                result;
        }
        public virtual int SetInvestProfilePersonalized(Policy.InvestProfilePersonalized investProfilePersonalized)
        {
            int result;
            IEnumerable<SP_SET_PL_PCY_INVEST_PROFILE_DETAIL_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_PL_PCY_INVEST_PROFILE_DETAIL(
                    investProfilePersonalized.CorpId,
                    investProfilePersonalized.RegionId,
                    investProfilePersonalized.CountryId,
                    investProfilePersonalized.DomesticregId,
                    investProfilePersonalized.StateProvId,
                    investProfilePersonalized.CityId,
                    investProfilePersonalized.OfficeId,
                    investProfilePersonalized.CaseSeqNo,
                    investProfilePersonalized.HistSeqNo,
                    investProfilePersonalized.ProfileTypeId,
                    investProfilePersonalized.InvestProductDateId,
                    investProfilePersonalized.SymbolId,
                    investProfilePersonalized.InvstProductDate,
                    investProfilePersonalized.InvestmentProfileDate,
                    investProfilePersonalized.InvstProfilePercent,
                    investProfilePersonalized.StockExchangeId,
                    investProfilePersonalized.ProjectionRate,
                    investProfilePersonalized.InvestmentCurrency,
                    investProfilePersonalized.MinPercentAllowed,
                    investProfilePersonalized.MaxPercentAllowed,
                    investProfilePersonalized.InitialValidDate,
                    investProfilePersonalized.EndValidDate,
                    investProfilePersonalized.UserId
                );

            return
                result;
        }

        public virtual int SetInvestmentProfile(Policy.InvestProfile investProfile)
        {
            int result;
            IEnumerable<SP_INSERT_PL_PCY_INVEST_PROFILE_Result> temp;

            result = -1;

            temp = globalModel.SP_INSERT_PL_PCY_INVEST_PROFILE(
                investProfile.CorpId,
                investProfile.RegionId,
                investProfile.CountryId,
                investProfile.DomesticregId,
                investProfile.StateProvId,
                investProfile.CityId,
                investProfile.OfficeId,
                investProfile.CaseSeqNo,
                investProfile.HistSeqNo,
                investProfile.ProfileTypeId,
                investProfile.InvestProductDateId,
                investProfile.InvestmentProductDate,
                investProfile.InvstProfileDesc,
                investProfile.UserId
                );

            return
                result;
        }
        [ObsoleteAttribute("This method is deprecated.")]
        public virtual int DeleteInvestmentProfile(Policy.InvestProfile investProfile)
        {
            int result;
            IEnumerable<SP_DELETE_PL_PCY_INVEST_PROFILE_Result> temp;

            result = -1;

            temp = globalModel.SP_DELETE_PL_PCY_INVEST_PROFILE(
                investProfile.CorpId,
                investProfile.RegionId,
                investProfile.CountryId,
                investProfile.DomesticregId,
                investProfile.StateProvId,
                investProfile.CityId,
                investProfile.OfficeId,
                investProfile.CaseSeqNo,
                investProfile.HistSeqNo,
                investProfile.ProfileTypeId,
                investProfile.InvestProductDateId,
                investProfile.UserId
                );
            return
                result;
        }
        #endregion

        #region Rating
        public virtual IEnumerable<Policy.OverPricePercentage> GetOverPricePercentage(Policy.RiskRatingCondition condition)
        {
            IEnumerable<Policy.OverPricePercentage> result;
            IEnumerable<SP_GET_PL_RISK_RATING_CONDITION_PERCENTAGE_OVER_Result> temp;

            temp = globalModel.SP_GET_PL_RISK_RATING_CONDITION_PERCENTAGE_OVER(
                    condition.CorpId,
                    condition.RiskGroupId,
                    condition.RiskDetId,
                    condition.PageId,
                    condition.GridId,
                    condition.ElementId,
                    condition.ColumnId,
                    condition.RiskTypeId);

            result = temp
                .Select(pp => new Policy.OverPricePercentage
                {
                    RatingTypeId = pp.Rating_Type_Id,
                    MinValue = pp.Min_Value,
                    MaxValue = pp.Max_Value
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Policy.RiskRating> GetRiskRating(Policy.RiskRating riskRating)
        {
            IEnumerable<Policy.RiskRating> result;
            IEnumerable<SP_GET_PL_RISK_RATING_Result> temp;

            temp = globalModel.SP_GET_PL_RISK_RATING(
                    riskRating.CorpId,
                    riskRating.RegionId,
                    riskRating.CountryId,
                    riskRating.DomesticRegId,
                    riskRating.StateProvId,
                    riskRating.CityId,
                    riskRating.OfficeId,
                    riskRating.CaseSeqNo,
                    riskRating.HistSeqNo,
                    riskRating.ContactId,
                    riskRating.ContactRoleTypeId,
                    riskRating.OperationId
                );

            result = temp
                .Select(rr => new Policy.RiskRating
                {
                    CorpId = rr.Corp_Id,
                    RegionId = rr.Region_Id,
                    CountryId = rr.Country_Id,
                    DomesticRegId = rr.DomesticReg_Id,
                    StateProvId = rr.State_Prov_Id,
                    CityId = rr.City_Id,
                    OfficeId = rr.Office_Id,
                    CaseSeqNo = rr.Case_Seq_No,
                    HistSeqNo = rr.Hist_Seq_No,
                    ContactId = rr.Contact_Id,
                    ContactRoleTypeId = rr.Contact_Role_Type_Id,
                    OperationId = rr.Operation_Id,
                    RiskId = rr.Risk_Id,
                    SequenceReference = rr.Sequence_Reference,
                    ClassificationId = rr.Classification_Id,
                    SuggestedRating = rr.Suggested_Rating,
                    TableRating = rr.Table_Rating,
                    PerThousandRating = rr.Per_Thousand_Rating,
                    StartDate = rr.Start_Date,
                    Duration = rr.Duration,
                    NotificationDate = rr.Notification_Date,
                    RequestedBy = rr.Requested_By,
                    EndDate = rr.End_Date,
                    RiskRateStatusId = rr.Risk_Rate_Status_Id,
                    YearToReconsider = rr.Year_To_Reconsider,
                    ReconsiderDate = rr.Reconsider_Date,
                    Comment = rr.Comment,
                    RiderTypeId = rr.Rider_Type_Id.ConvertToNoNullable(),
                    RiderId = rr.Rider_Id.ConvertToNoNullable(),
                    ReasonDesc = rr.ReasonDesc,
                    RyderTypeDesc = rr.Ryder_Type_Desc,
                    StatusDesc = rr.Status_Desc,
                    CreditTypeId = rr.Credit_Type_Id,
                    CreditTypeDesc = rr.Credit_Type_Desc,
                    CreditId = rr.Credit_Id,
                    CreditDesc = rr.Credit_Desc,
                    CreditReasonDesc = rr.Credit_Reason_Desc,
                    ExclusionTypeDesc = rr.Exclusion_Type_Desc,
                    ExclusionDesc = rr.Exclusion_Desc,
                    RequestedByName = rr.Requested_By_Name,
                    DocTypeId = rr.Doc_Type_Id,
                    DocCategoryId = rr.Doc_Category_Id,
                    DocumentId = rr.Document_Id,
                    UnderwriterName = rr.UnderwriterName,
                    RiskTypeId = rr.Risk_Type_Id
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Policy.RiskRating> GetRiskRatingSelection(Policy.RiskRating riskRating)
        {
            IEnumerable<Policy.RiskRating> result;
            IEnumerable<SP_GET_PL_RISK_SELECTION_Result> temp;

            temp = globalModel.SP_GET_PL_RISK_SELECTION(
                    riskRating.CorpId,
                    riskRating.SequenceReference,
                    riskRating.RiskId
                );

            result = temp
                .Select(s => new Policy.RiskRating
                {
                    CorpId = s.corp_id,
                    RiskId = s.Risk_Id.ConvertToNoNullable(),
                    SequenceReference = s.Sequence_Reference,
                    RiskGroupId = s.riskgroup_id,
                    RiskDetId = s.riskdet_id,
                    PageId = s.page_id,
                    GridId = s.grid_id,
                    ElementId = s.element_id,
                    ColumnId = s.column_Id,
                    RiskTypeId = s.Risk_Type_Id.ConvertToNoNullable(),
                    ReasonDesc = s.element_desc,
                    CategoryDesc = s.Category_Desc,
                    ConditionTypeDesc = s.Condition_Type_Desc,
                    RiskTypeDesc = s.Risk_Type_Desc
                })
                .ToArray();

            return
                result;
        }

        public virtual Policy.RiskRating SetRiskRatingWithHeader(Policy.RiskRating riskRating)
        {
            Policy.RiskRating result;
            IEnumerable<SP_SET_PL_RISK_RATING_CU_Result> temp;

            temp = globalModel.SP_SET_PL_RISK_RATING_CU(
                    riskRating.CorpId,
                    riskRating.RegionId,
                    riskRating.CountryId,
                    riskRating.DomesticRegId,
                    riskRating.StateProvId,
                    riskRating.CityId,
                    riskRating.OfficeId,
                    riskRating.CaseSeqNo,
                    riskRating.HistSeqNo,
                    riskRating.ContactId,
                    riskRating.ContactRoleTypeId,
                    riskRating.OperationId,
                    riskRating.RiskId,
                    riskRating.SequenceReference,
                    riskRating.ClassificationId,
                    riskRating.SuggestedRating,
                    riskRating.TableRating,
                    riskRating.PerThousandRating,
                    riskRating.StartDate,
                    riskRating.Duration,
                    riskRating.NotificationDate,
                    riskRating.RequestedBy,
                    riskRating.EndDate,
                    riskRating.RiskRateStatusId,
                    riskRating.YearToReconsider,
                    riskRating.ReconsiderDate,
                    riskRating.RiskTypeId,
                    riskRating.RiskGroupId,
                    riskRating.RiskDetId,
                    riskRating.PageId,
                    riskRating.GridId,
                    riskRating.ElementId,
                    riskRating.ColumnId,
                    riskRating.Comment,
                    riskRating.RiderTypeId,
                    riskRating.RiderId,
                    riskRating.CreditTypeId,
                    riskRating.CreditId,
                    riskRating.CreditReasonId,
                    riskRating.ExclusionTypeId,
                    riskRating.ExclusionId,
                    riskRating.DocTypeId,
                    riskRating.DocCategoryId,
                    riskRating.DocumentId,
                    riskRating.UserId
                );

            result = temp
                .Select(rr => new Policy.RiskRating
                {
                    SequenceReference = rr.Sequence_Reference,
                    RiskId = rr.Risk_Id
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual Policy.RiskRating SetRiskRating(Policy.RiskRating riskRating)
        {
            Policy.RiskRating result;
            IEnumerable<SP_SET_PL_RISK_RATING_Result> temp;
            ObjectParameter riskId;

            riskId = new ObjectParameter("Risk_Id", riskRating.RiskId);

            temp = globalModel.SP_SET_PL_RISK_RATING(
                    riskRating.CorpId,
                    riskRating.SequenceReference,
                    riskId,
                    riskRating.SuggestedRating,
                    riskRating.TableRating,
                    riskRating.PerThousandRating,
                    riskRating.StartDate,
                    riskRating.Duration,
                    riskRating.NotificationDate,
                    riskRating.RequestedBy,
                    riskRating.EndDate,
                    riskRating.RiskRateStatusId,
                    riskRating.YearToReconsider,
                    riskRating.ReconsiderDate,
                    riskRating.Comment,
                    riskRating.RiderTypeId,
                    riskRating.RiderId,
                    riskRating.RiskTypeId,
                    riskRating.CreditTypeId,
                    riskRating.CreditId,
                    riskRating.CreditReasonId,
                    riskRating.ExclusionTypeId,
                    riskRating.ExclusionId,
                    riskRating.DocTypeId,
                    riskRating.DocCategoryId,
                    riskRating.DocumentId,
                    riskRating.UserId
                );

            result = temp
                .Select(rr => new Policy.RiskRating
                {
                    CorpId = rr.Corp_Id.ConvertToNoNullable(),
                    SequenceReference = rr.Sequence_Reference,
                    RiskId = rr.Risk_Id
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual int SetDocument(int docTypeId, int docCategoryId, int documentId, byte[] documentBinary, string documentName, DateTime creationDate
           , DateTime? expireDate, int userId)
        {
            int result;
            IEnumerable<SP_SET_DOCUMENT_Result> tempDoc;
            ObjectParameter document_Id;

            document_Id = new ObjectParameter("Document_Id", documentId);

            tempDoc = globalModel.SP_SET_DOCUMENT(
                    docTypeId,
                    docCategoryId,
                    document_Id,
                    documentBinary,
                    documentName,
                    creationDate,
                    expireDate,
                    userId
                ).ToArray();

            result = tempDoc != null && tempDoc.Any()
                ? tempDoc.First().Document_Id.ConvertToNoNullable()
                : -1;

            return
                result;
        }
        public virtual int DeleteRiskRatingSelection(Policy.RiskRating riskRating)
        {
            int result;

            result = globalModel.SP_DELETE_PL_RISK_SELECTION(
                    riskRating.CorpId,
                    riskRating.SequenceReference,
                    riskRating.RiskId,
                    riskRating.RiskTypeId,
                    riskRating.RiskGroupId,
                    riskRating.RiskDetId,
                    riskRating.PageId,
                    riskRating.GridId,
                    riskRating.ElementId,
                    riskRating.ColumnId,
                    riskRating.UserId
                );

            return
                result;
        }
        public virtual int DeleteRiskRating(Policy.RiskRating riskRating)
        {
            int result;
            //Bmarroquin 03-03-2017 cambio dado que no funciona el delete se daba un error de:
            //New transaction is not allowed because there are other threads running in the session 
            //Se creo el objeto lObjGloMod 
            var lObjGloMod = new GlobalEntityDataModel();
            using (lObjGloMod)
            {
                result = lObjGloMod.SP_DELETE_PL_RISK_RATING(
                        riskRating.CorpId,
                        riskRating.SequenceReference,
                        riskRating.RiskId,
                        riskRating.UserId
                    );
            }
            lObjGloMod = null;
            return result;
        }
        #endregion

        #region SendToReinsurance
        public virtual IEnumerable<Reinsurance.Communication> GetReinsuranceCommunication(Reinsurance.Communication comm)
        {
            IEnumerable<Reinsurance.Communication> result;
            IEnumerable<SP_GET_UW_STEP_REINSURER_COMMS_Result> temp;

            temp = globalModel.SP_GET_UW_STEP_REINSURER_COMMS(
                comm.CorpId,
                comm.RegionId,
                comm.CountryId,
                comm.DomesticRegId,
                comm.StateProvId,
                comm.CityId,
                comm.OfficeId,
                comm.CaseSeqNo,
                comm.HistSeqNo,
                comm.StepTypeId,
                comm.StepId,
                comm.StepCaseNo
                );

            result = temp
                .Select(rc => new Reinsurance.Communication
                {
                    CorpId = rc.Corp_Id,
                    RegionId = rc.Region_Id,
                    CountryId = rc.Country_Id,
                    DomesticRegId = rc.Domesticreg_Id,
                    StateProvId = rc.State_Prov_Id,
                    CityId = rc.City_Id,
                    OfficeId = rc.Office_Id,
                    CaseSeqNo = rc.Case_Seq_No,
                    HistSeqNo = rc.Hist_Seq_No,
                    StepTypeId = rc.Step_Type_Id,
                    StepId = rc.Step_Id,
                    StepCaseNo = rc.Step_Case_No,
                    CommunicationId = rc.Communication_Id,
                    ReinsurerId = rc.Reinsurer_Id,
                    CommTypeId = rc.Comm_Type_Id,
                    CommFrom = rc.Comm_From,
                    CommSubject = rc.Comm_Subject,
                    CommDate = rc.Comm_Date,
                    CommAttachment = rc.Comm_Attachment,
                    ReinsurerDesc = rc.Reinsurer_Desc,
                    CommTypeDesc = rc.Comm_Type_Desc,
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Reinsurance.FacultativeStatus> GetReinsuranceFacultativeStatus()
        {
            IEnumerable<Reinsurance.FacultativeStatus> result;
            IEnumerable<SP_GET_REINSURANCE_FACULTATIVE_STATUS_Result> temp;
            temp = globalModel.SP_GET_REINSURANCE_FACULTATIVE_STATUS();
            result = temp
                .Select(fs => new Reinsurance.FacultativeStatus
                {
                    Corp_Id = fs.Corp_Id,
                    Facultative_Status_Desc = fs.Facultative_Status_Desc,
                    Facultative_Status_Id = fs.Facultative_Status_Id,
                    Facultative_Status_Status = fs.Facultative_Status_Status.GetValueOrDefault(),
                    Name_Key = fs.Name_Key
                }).ToArray();
            return result;
        }
        public virtual IEnumerable<Reinsurance.RatingRisk> GetRatingRisk(string ratingTable)
        {
            IEnumerable<Reinsurance.RatingRisk> result;
            IEnumerable<SP_GET_TABLE_RATING_RISK_DETAILS_Result> temp;
            temp = globalModel.SP_GET_TABLE_RATING_RISK_DETAILS(ratingTable);
            result = temp
                .Select(r => new Reinsurance.RatingRisk
                {
                    Long_Description = r.Long_Description,
                    Rating_Value = r.Rating_Value,
                    Short_Description = r.Short_Description,
                    TableRating_Id = r.TableRating_Id
                }).ToArray();
            return result;
        }


        public virtual IEnumerable<Reinsurance.FacultativeDetails> GetReinsuranceFacultativeDetails(Reinsurance.FacultativeDetails facultativeDets)
        {
            IEnumerable<Reinsurance.FacultativeDetails> result;
            IEnumerable<SP_GET_PL_PCY_REINSURANCE_FACULTATIVE_Result> temp;
            temp = globalModel.SP_GET_PL_PCY_REINSURANCE_FACULTATIVE(facultativeDets.Corp_Id, facultativeDets.Region_Id, facultativeDets.Country_Id, facultativeDets.Domesticreg_Id, facultativeDets.State_Prov_Id, facultativeDets.City_Id, facultativeDets.Office_Id, facultativeDets.Case_seq_No, facultativeDets.Hist_Seq_No, facultativeDets.Rider_Type_Id, facultativeDets.Rider_Id, facultativeDets.Coverage_Type_Desc);
            result = temp
                .Select(f => new Reinsurance.FacultativeDetails
                {
                    Facultative_Status_Id = f.Facultative_Status_Id,
                    Reinsurance_Facultative_Status = f.Reinsurance_Facultative_Status,
                    Authorized_Amount = f.Authorized_Amount.GetValueOrDefault(),
                    Beneficiary_Amount = f.Beneficiary_Amount.GetValueOrDefault(),
                    Case_seq_No = f.Case_seq_No,
                    City_Id = f.City_Id,
                    Company_Risk_Amount = f.Company_Risk_Amount.GetValueOrDefault(),
                    Corp_Id = f.Corp_Id,
                    Country_Id = f.Country_Id,
                    Coverage_Type_Desc = f.Coverage_Type_Desc,
                    Domesticreg_Id = f.Domesticreg_Id,
                    Hist_Seq_No = f.Hist_Seq_No,
                    Office_Id = f.Office_Id,
                    Per_Thousend_Risk_Amount = f.Per_Thousend_Risk_Amount.GetValueOrDefault(),
                    //Processed_Date = f.Processed_Date.GetValueOrDefault(),
                    Processed_Date = f.Processed_Date,
                    Region_Id = f.Region_Id,
                    Risk_Rating = f.Risk_Rating_Table,
                    Reinsurance_Risk_Amount = f.Reinsurance_Risk_Amount.GetValueOrDefault(),
                    //Requested_Date = f.Requested_Date.GetValueOrDefault(),
                    Requested_Date = f.Requested_Date,
                    Rider_Id = f.Rider_Id,
                    Rider_Type_Id = f.Rider_Type_Id,
                    Risk_Rating_Amount = f.Risk_Rating_Amount.GetValueOrDefault(),
                    State_Prov_Id = f.State_Prov_Id,
                    Facultative_Reinsurance_Id = f.Facultative_Reinsurance_Id,
                    Facultative_Status_Desc = f.Facultative_Status_Desc
                }).ToArray();
            return result;
        }

        public virtual IEnumerable<Reinsurance.Communication> GetReinsuranceCommunicationHtmlAndAttachments(Reinsurance.Communication comm)
        {
            IEnumerable<Reinsurance.Communication> result;
            IEnumerable<SP_GET_UW_STEP_COMM_DOCUMENTS_Result> temp;

            temp = globalModel.SP_GET_UW_STEP_COMM_DOCUMENTS(
                comm.CorpId,
                comm.RegionId,
                comm.CountryId,
                comm.DomesticRegId,
                comm.StateProvId,
                comm.CityId,
                comm.OfficeId,
                comm.CaseSeqNo,
                comm.HistSeqNo,
                comm.StepTypeId,
                comm.StepId,
                comm.StepCaseNo,
                comm.CommunicationId);

            result = temp
                .Select(rc => new Reinsurance.Communication
                {
                    CorpId = rc.Corp_Id,
                    RegionId = rc.Region_Id,
                    CountryId = rc.Country_Id,
                    DomesticRegId = rc.Domesticreg_Id,
                    StateProvId = rc.State_Prov_Id,
                    CityId = rc.City_Id,
                    OfficeId = rc.Office_Id,
                    CaseSeqNo = rc.Case_Seq_No,
                    HistSeqNo = rc.Hist_Seq_No,
                    StepTypeId = rc.Step_Type_Id,
                    StepId = rc.Step_Id,
                    StepCaseNo = rc.Step_Case_No,
                    CommunicationId = rc.Communication_Id,
                    ReinsurerId = rc.Reinsurer_Id,
                    CommTypeId = rc.Comm_Type_Id,
                    CommText = rc.Comm_Text,
                    CommFrom = rc.Comm_From,
                    CommSubject = rc.Comm_Subject,
                    CommDate = rc.Comm_Date,
                    ReinsurerDesc = rc.Reinsurer_Desc,
                    CommTypeDesc = rc.Comm_Type_Desc,
                    DocTypeId = rc.Doc_Type_Id,
                    DocCategoryId = rc.Doc_Category_Id,
                    DocumentId = rc.Document_Id,
                    DocumentName = rc.Document_Name,
                    DocTypeDesc = rc.Doc_Type_Desc,
                    Extension = rc.Extension
                })
                .ToArray();

            return
                result;
        }

        public virtual Reinsurance.Communication SetReinsuranceCommunication(Reinsurance.Communication comm)
        {
            Reinsurance.Communication result;
            IEnumerable<SP_SET_UW_STEP_REINSURER_COMMS_Result> temp;

            temp = globalModel.SP_SET_UW_STEP_REINSURER_COMMS(
                    comm.CorpId,
                    comm.RegionId,
                    comm.CountryId,
                    comm.DomesticRegId,
                    comm.StateProvId,
                    comm.CityId,
                    comm.OfficeId,
                    comm.CaseSeqNo,
                    comm.HistSeqNo,
                    comm.StepTypeId,
                    comm.StepId,
                    comm.StepCaseNo,
                    comm.CommunicationId,
                    comm.ReinsurerId,
                    comm.CommTypeId,
                    comm.CommText,
                    comm.CommFrom,
                    comm.CommSubject,
                    comm.CommDate,
                    comm.CommAttachment,
                    comm.UserId
                )
                .ToArray();

            result = temp
                .Select(rc => new Reinsurance.Communication
                {
                    CorpId = rc.Corp_Id.ConvertToNoNullable(),
                    RegionId = rc.Region_Id.ConvertToNoNullable(),
                    CountryId = rc.Country_Id.ConvertToNoNullable(),
                    DomesticRegId = rc.Domesticreg_Id.ConvertToNoNullable(),
                    StateProvId = rc.State_Prov_Id.ConvertToNoNullable(),
                    CityId = rc.City_Id.ConvertToNoNullable(),
                    OfficeId = rc.Office_Id.ConvertToNoNullable(),
                    CaseSeqNo = rc.Case_Seq_No.ConvertToNoNullable(),
                    HistSeqNo = rc.Hist_Seq_No.ConvertToNoNullable(),
                    StepTypeId = rc.Step_Type_Id.ConvertToNoNullable(),
                    StepId = rc.Step_Id.ConvertToNoNullable(),
                    StepCaseNo = rc.Step_Case_No.ConvertToNoNullable(),
                    CommunicationId = rc.Communication_Id.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual Reinsurance.Communication SetReinsuranceCommunicationAttachment(Reinsurance.Communication comm)
        {
            Reinsurance.Communication result;
            IEnumerable<SP_SET_UW_STEP_COMM_DOCUMENTS_Result> temp;

            temp = globalModel.SP_SET_UW_STEP_COMM_DOCUMENTS(
                    comm.CorpId,
                    comm.RegionId,
                    comm.CountryId,
                    comm.DomesticRegId,
                    comm.StateProvId,
                    comm.CityId,
                    comm.OfficeId,
                    comm.CaseSeqNo,
                    comm.HistSeqNo,
                    comm.StepTypeId,
                    comm.StepId,
                    comm.StepCaseNo,
                    comm.CommunicationId,
                    comm.DocTypeId,
                    comm.DocCategoryId,
                    comm.DocumentId,
                    comm.UserId
                )
                .ToArray();

            result = temp
                .Select(rc => new Reinsurance.Communication
                {
                    CorpId = rc.Corp_Id.ConvertToNoNullable(),
                    RegionId = rc.Region_Id.ConvertToNoNullable(),
                    CountryId = rc.Country_Id.ConvertToNoNullable(),
                    DomesticRegId = rc.Domesticreg_Id.ConvertToNoNullable(),
                    StateProvId = rc.State_Prov_Id.ConvertToNoNullable(),
                    CityId = rc.City_Id.ConvertToNoNullable(),
                    OfficeId = rc.Office_Id.ConvertToNoNullable(),
                    CaseSeqNo = rc.Case_Seq_No.ConvertToNoNullable(),
                    HistSeqNo = rc.Hist_Seq_No.ConvertToNoNullable(),
                    StepTypeId = rc.Step_Type_Id.ConvertToNoNullable(),
                    StepId = rc.Step_Id.ConvertToNoNullable(),
                    StepCaseNo = rc.Step_Case_No.ConvertToNoNullable(),
                    CommunicationId = rc.Communication_Id.ConvertToNoNullable(),
                    DocTypeId = rc.Doc_Type_Id,
                    DocCategoryId = rc.Doc_Category_Id,
                    DocumentId = rc.Document_Id,
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual Reinsurance.StepAvailable GetStepAvailable(Reinsurance.StepAvailable step)
        {
            Reinsurance.StepAvailable result;
            IEnumerable<SP_GET_UW_STEP_REINSURER_AVAILABLE_Result> temp;

            temp = globalModel.SP_GET_UW_STEP_REINSURER_AVAILABLE(
                    step.CorpId,
                    step.RegionId,
                    step.CountryId,
                    step.DomesticRegId,
                    step.StateProvId,
                    step.CityId,
                    step.OfficeId,
                    step.CaseSeqNo,
                    step.HistSeqNo
                )
                .ToArray();

            result = temp
                .Select(rc => new Reinsurance.StepAvailable
                {
                    CorpId = rc.Corp_Id.ConvertToNoNullable(),
                    RegionId = rc.Region_Id.ConvertToNoNullable(),
                    CountryId = rc.Country_Id.ConvertToNoNullable(),
                    DomesticRegId = rc.Domesticreg_Id.ConvertToNoNullable(),
                    StateProvId = rc.State_Prov_Id.ConvertToNoNullable(),
                    CityId = rc.City_Id.ConvertToNoNullable(),
                    OfficeId = rc.Office_Id.ConvertToNoNullable(),
                    CaseSeqNo = rc.Case_Seq_No.ConvertToNoNullable(),
                    HistSeqNo = rc.Hist_Seq_No.ConvertToNoNullable(),
                    StepTypeId = rc.Step_Type_Id.ConvertToNoNullable(),
                    StepId = rc.Step_Id.ConvertToNoNullable(),
                    StepCaseNo = rc.Step_Case_No.ConvertToNoNullable(),
                    ReinsurerId = rc.Reinsurer_Id.ConvertToNoNullable(),
                    CommTypeId = rc.Comm_Type_Id.ConvertToNoNullable(),
                    StepSeqReference = rc.Step_Seq_Reference,
                    ReinsurerEmail = rc.Reinsurer_Email
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual Reinsurance.StepAvailable GetStepAvailable(string stepSeqReference)
        {
            Reinsurance.StepAvailable result;
            IEnumerable<SP_GET_UW_STEPS_BY_STEP_SEQ_REFERENCE_Result> temp;

            temp = globalModel.SP_GET_UW_STEPS_BY_STEP_SEQ_REFERENCE(stepSeqReference).ToArray();

            result = temp
                .Select(sa => new Reinsurance.StepAvailable
                {
                    CorpId = sa.Corp_Id,
                    RegionId = sa.Region_Id,
                    CountryId = sa.Country_Id,
                    DomesticRegId = sa.Domesticreg_Id,
                    StateProvId = sa.State_Prov_Id,
                    CityId = sa.City_Id,
                    OfficeId = sa.Office_Id,
                    CaseSeqNo = sa.Case_Seq_No,
                    HistSeqNo = sa.Hist_Seq_No,
                    StepTypeId = sa.Step_Type_Id,
                    StepId = sa.Step_Id,
                    StepCaseNo = sa.Step_Case_No,
                    ReinsurerId = sa.Reinsurer_Id,
                    CommTypeId = sa.Comm_Type_Id,
                    StepSeqReference = sa.Step_Seq_Reference,
                    ReinsurerEmail = sa.Reinsurer_Email
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual decimal GetDocumentSize(int docTypeId, int docCategoryId, int documentId)
        {
            decimal result;
            IEnumerable<SP_GET_DOCUMENT_SIZE_Result> temp;

            temp = globalModel.SP_GET_DOCUMENT_SIZE(docTypeId, docCategoryId, documentId).ToArray();

            result = temp != null && temp.Any()
                        ? temp.First().FileSize.ConvertToNoNullable()
                        : -1;

            return
                result;
        }
        #endregion

        public virtual IEnumerable<Policy.Form> GetFormPolicyContact(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int contactId, int formId, int languageId)
        {
            IEnumerable<Policy.Form> result;
            IEnumerable<SP_GET_FOR_FORM_BY_POLICY_AND_CONTACT_Result> temp;

            temp = globalModel.SP_GET_FOR_FORM_BY_POLICY_AND_CONTACT(
                coprId,
                regionId,
                countryId,
                domesticRegId,
                stateProvId,
                cityId,
                officeId,
                caseSeqNo,
                histSeqNo,
                contactId,
                formId,
                languageId);

            result = temp
                .Select(f => new Policy.Form
                {
                    FormId = f.Form_Id,
                    FormCategoryId = f.Form_Category_Id,
                    TemplateId = f.Template_Id,
                    CorpId = f.Corp_Id,
                    RegionId = f.Region_Id,
                    CountryId = f.Country_Id,
                    DomesticRegId = f.Domesticreg_Id,
                    StateProvId = f.State_Prov_Id,
                    CityId = f.City_Id,
                    OfficeId = f.Office_Id,
                    CaseSeqNo = f.Case_Seq_No,
                    HistSeqNo = f.Hist_Seq_No,
                    ContactId = f.CONTACT_ID,
                    FormDesc = f.Form_Desc,
                    FormCatDesc = f.Form_Cat_Desc,
                    HtmlTemplate = f.Html_Template,
                    PDFTemplatePath = f.PDF_Template_Path,
                    CreateDate = f.Create_date
                })
                .ToArray();

            return
                result;
        }

        #region BackgroundCheck
        public virtual Policy.BackgroundCheck SetBackgroundCheck(Policy.BackgroundCheck backgroundCheck)
        {
            Policy.BackgroundCheck result;
            IEnumerable<SP_SET_UW_BACKGROUND_CHECK_Result> temp;

            temp = globalModel.SP_SET_UW_BACKGROUND_CHECK(
                    backgroundCheck.CorpId,
                    backgroundCheck.RegionId,
                    backgroundCheck.CountryId,
                    backgroundCheck.DomesticRegId,
                    backgroundCheck.StateProvId,
                    backgroundCheck.CityId,
                    backgroundCheck.OfficeId,
                    backgroundCheck.CaseSeqNo,
                    backgroundCheck.HistSeqNo,
                    backgroundCheck.ContactId,
                    backgroundCheck.BackgroundCheckId,
                    backgroundCheck.Reason,
                    backgroundCheck.Results,
                    backgroundCheck.Date,
                    backgroundCheck.Comments,
                    backgroundCheck.UserId
                );

            result = temp
                .Select(bc => new Policy.BackgroundCheck
                {
                    CorpId = bc.Corp_Id.ConvertToNoNullable(),
                    RegionId = bc.Region_Id.ConvertToNoNullable(),
                    CountryId = bc.Country_Id.ConvertToNoNullable(),
                    DomesticRegId = bc.Domesticreg_Id.ConvertToNoNullable(),
                    StateProvId = bc.State_Prov_Id.ConvertToNoNullable(),
                    CityId = bc.City_Id.ConvertToNoNullable(),
                    OfficeId = bc.Office_Id.ConvertToNoNullable(),
                    CaseSeqNo = bc.Case_Seq_No.ConvertToNoNullable(),
                    HistSeqNo = bc.Hist_Seq_No.ConvertToNoNullable(),
                    ContactId = bc.Contact_Id.ConvertToNoNullable(),
                    BackgroundCheckId = bc.Background_Check_Id.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual bool CheckBackgroundCheckComplete(Policy.Parameter parameter)
        {
            IEnumerable<SP_CK_UW_BACKGROUND_CHECK_Result> temp;
            bool result;

            temp = globalModel.SP_CK_UW_BACKGROUND_CHECK(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo
                );

            result = temp
                        .Select(bc => bc.Result.ConvertToNoNullable())
                        .FirstOrDefault();

            return
                result;
        }
        public virtual IEnumerable<Step> GetBackgroundCheckPending(Policy.Parameter parameter)
        {
            IEnumerable<Step> result;
            IEnumerable<SP_GET_UW_BACKGROUND_CHECK_STEP_PENDING_Result> temp;

            temp = globalModel.SP_GET_UW_BACKGROUND_CHECK_STEP_PENDING(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo);

            result = temp
                .Select(s => new Step
                {
                    CorpId = s.Corp_Id,
                    RegionId = s.Region_Id,
                    CountryId = s.Country_Id,
                    DomesticregId = s.Domesticreg_Id,
                    StateProvId = s.State_Prov_Id,
                    CityId = s.City_Id,
                    OfficeId = s.Office_Id,
                    CaseSeqNo = s.Case_Seq_No,
                    HistSeqNo = s.Hist_Seq_No,
                    StepTypeId = s.Step_Type_Id,
                    StepId = s.Step_Id,
                    StepCaseNo = s.Step_Case_No
                })
                .ToArray();

            return
                result;
        }
        #endregion

        public virtual Requirement.Document GetIdCopyRequirement(Requirement requirement)
        {
            Requirement.Document result;
            IEnumerable<SP_GET_PL_REQUIREMENT_ID_COPY_BY_POLICYCONTACT_Result> temp;

            temp = globalModel.SP_GET_PL_REQUIREMENT_ID_COPY_BY_POLICYCONTACT(
                    requirement.CorpId,
                    requirement.RegionId,
                    requirement.CountryId,
                    requirement.DomesticregId,
                    requirement.StateProvId,
                    requirement.CityId,
                    requirement.OfficeId,
                    requirement.CaseSeqNo,
                    requirement.HistSeqNo,
                    requirement.ContactId
                    )
                .ToArray();

            result = temp
                .Select(rd => new Requirement.Document
                {
                    CorpId = rd.Corp_Id,
                    RegionId = rd.Region_Id,
                    CountryId = rd.Country_Id,
                    DomesticregId = rd.Domesticreg_Id,
                    StateProvId = rd.State_Prov_Id,
                    CityId = rd.City_Id,
                    OfficeId = rd.Office_Id,
                    CaseSeqNo = rd.Case_Seq_No,
                    HistSeqNo = rd.Hist_Seq_No,
                    ContactId = rd.Contact_Id,
                    RequirementCatId = rd.Requirement_Cat_Id,
                    RequirementTypeId = rd.Requirement_Type_Id,
                    RequirementId = rd.Requirement_Id,
                    RequirementDocId = rd.Requirement_Doc_Id.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual bool CheckPolicyMedical(Policy.Parameter parameter)
        {
            bool result;
            IEnumerable<SP_CK_PL_POLICY_MEDICAL_Result> temp;

            temp = globalModel.SP_CK_PL_POLICY_MEDICAL(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo
                );

            result = temp
                        .Select(bc => bc.Result.ConvertToNoNullable())
                        .FirstOrDefault();

            return
                result;
        }

        public virtual int GenerateTempPolicyNo(Policy.Parameter parameter)
        {
            int result;
            IEnumerable<SP_SET_NEW_TEMP_POLICY_NO_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_NEW_TEMP_POLICY_NO(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.UnderwriterId
                );

            return
                result;
        }

        public virtual IEnumerable<Policy.Contact.Action> GetPolicyAction(Policy.Parameter parameter)
        {
            IEnumerable<Policy.Contact.Action> result;
            IEnumerable<SP_GET_PL_PCY_CONTACT_ACTIONS_Result> temp;

            temp = globalModel.SP_GET_PL_PCY_CONTACT_ACTIONS(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo);

            result = temp
                .Select(ca => new Policy.Contact.Action
                {
                    CorpId = ca.Corp_Id,
                    RegionId = ca.Region_Id,
                    CountryId = ca.Country_Id,
                    DomesticRegId = ca.Domesticreg_Id,
                    StateProvId = ca.State_Prov_Id,
                    CityId = ca.City_Id,
                    OfficeId = ca.Office_Id,
                    CaseSeqNo = ca.Case_Seq_No,
                    HistSeqNo = ca.Hist_Seq_No,
                    ContactId = ca.Contact_Id,
                    ContactRoleTypeId = ca.Contact_Role_Type_Id,
                    ActionId = ca.Action_Id,
                    ActionSeqNo = ca.Action_Seq_No,
                    StepTypeId = ca.Step_Type_Id,
                    StepId = ca.Step_Id,
                    StepCaseNo = ca.Step_Case_No,
                    ActionDesc = ca.Action_Desc
                })
                .ToArray();

            return
                result;
        }

        public virtual int ChangePolicyChain(Policy.Parameter parameter)
        {
            int result;
            IEnumerable<SP_INSERT_AGENT_CHAIN_DETAIL_Result> temp;

            result = -1;

            temp = globalModel.SP_INSERT_AGENT_CHAIN_DETAIL(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.AgentId.ConvertToNoNullable(),
                    parameter.UserId.ConvertToNoNullable()
                );

            return
                result;
        }

        public virtual Policy.Parameter GetPolicyKey(string policyNo)
        {
            Policy.Parameter result;
            IEnumerable<SP_GET_POLICY_KEY_Result> temp;

            temp = globalModel.SP_GET_POLICY_KEY(policyNo, null);

            result = temp
                .Select(pk => new Policy.Parameter
                {
                    CorpId = pk.Corp_Id,
                    RegionId = pk.Region_Id,
                    CountryId = pk.Country_Id,
                    DomesticregId = pk.Domesticreg_Id,
                    StateProvId = pk.State_Prov_Id,
                    CityId = pk.City_Id,
                    OfficeId = pk.Office_Id,
                    CaseSeqNo = pk.Case_Seq_No,
                    HistSeqNo = pk.Hist_Seq_No
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual Agent.Key GetAgentId(string nameId)
        {
            Agent.Key result;
            IEnumerable<SP_GET_EN_AGENT_AGENTID_BY_NAMEID_Result> temp;

            temp = globalModel.SP_GET_EN_AGENT_AGENTID_BY_NAMEID(nameId);

            result = temp
                .Select(ai => new Agent.Key
                {
                    CorpId = ai.Corp_Id,
                    AgentId = ai.Agent_Id
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Policy.StatusChange> SetPolicyStatus(Policy.Parameter policyParameter)
        {
            IEnumerable<Policy.StatusChange> result;
            IEnumerable<SP_SET_PL_POLICY_STATUS_Result> temp;

            temp = globalModel.SP_SET_PL_POLICY_STATUS(
                    policyParameter.CorpId,
                    policyParameter.RegionId,
                    policyParameter.CountryId,
                    policyParameter.DomesticregId,
                    policyParameter.StateProvId,
                    policyParameter.CityId,
                    policyParameter.OfficeId,
                    policyParameter.CaseSeqNo,
                    policyParameter.HistSeqNo,
                    policyParameter.StatusChangeTypeId.Value,
                    policyParameter.StatusId.Value,
                    policyParameter.UserId.Value
                    );

            result = temp
                .Select(pc => new Policy.StatusChange
                {
                    Code = pc.Code,
                    Message = pc.Message
                })
                .ToArray();

            return
                result;
        }

        public virtual Policy NewPolicyWithoutAgent(Case.NewCase newCase)
        {
            Policy result;
            IEnumerable<SP_SET_NEW_CASE_WITHOUT_AGENT_Result> temp;

            temp = globalModel.SP_SET_NEW_CASE_WITHOUT_AGENT(
                  newCase.CorpId,
                  newCase.RegionId,
                  newCase.CountryId,
                  newCase.DomesticregId,
                  newCase.StateProvId,
                  newCase.CityId,
                  newCase.OfficeId,
                  newCase.ContactId,
                  newCase.AgentId,
                  newCase.ContactTypeId,
                  newCase.FirstName,
                  newCase.MiddleName,
                  newCase.FirstLastName,
                  newCase.SecondLastName,
                  newCase.Nickname,
                  newCase.InstitutionalName,
                  newCase.InstitutionalCountryId,
                  newCase.Dob,
                  newCase.Age,
                  newCase.NearAge,
                  newCase.Gender,
                  newCase.MaritalStatId,
                  newCase.DirectoryId,
                  newCase.RegionOfResidenceId,
                  newCase.CountryOfResidenceId,
                  newCase.DomesticRegOfResidenceId,
                  newCase.StateOfResidenceId,
                  newCase.CityOfResidenceId,
                  newCase.RegionOfBirthId,
                  newCase.CountryOfBirthId,
                  newCase.Weigth,
                  newCase.WeigthTypeId,
                  newCase.Height,
                  newCase.HeigthTypeId,
                  newCase.LineOfBusiness,
                  newCase.LineOfBusiness2,
                  newCase.CompanyName,
                  newCase.LengthWorkYear,
                  newCase.LengthWorkMonth,
                  newCase.LaborTasks,
                  newCase.IsCompany,
                  newCase.CompanyActivity,
                  newCase.CompanyFoundationDate,
                  newCase.OccupGroupTypeId,
                  newCase.OccupationId,
                  newCase.RelationshiptoAgent,
                  newCase.RelationshiptoOwner,
                  newCase.AnnualFamilyIncome,
                  newCase.AnnualPersonalIncome,
                  newCase.Smoker,
                  newCase.ReferredByRelationshipId,
                  newCase.ReferredByContactId,
                  newCase.ContactStatusId,
                  newCase.NameId,
                  newCase.NcfTypeId,
                  newCase.IsIllustration,
                  newCase.IllustrationNo,
                  newCase.UserId,
                  false, null, string.Empty
                  );

            result = temp
                    .Select(p => new Policy
                    {
                        CorpId = p.Corp_Id.ConvertToNoNullable(),
                        RegionId = p.Region_Id.ConvertToNoNullable(),
                        CountryId = p.Country_Id.ConvertToNoNullable(),
                        DomesticregId = p.Domesticreg_Id.ConvertToNoNullable(),
                        StateProvId = p.State_Prov_Id.ConvertToNoNullable(),
                        CityId = p.City_Id.ConvertToNoNullable(),
                        OfficeId = p.Office_Id.ConvertToNoNullable(),
                        CaseSeqNo = p.Case_Seq_No.ConvertToNoNullable(),
                        HistSeqNo = p.Hist_Seq_No.ConvertToNoNullable(),
                        PolicyNo = p.Policy_No,
                        ContactId = p.Contact_Id.ConvertToNoNullable(),
                        PaymentId = p.Payment_Id.ConvertToNoNullable(),
                    })
                    .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Policy.VehiclesCoverage> GetVehiclesCoverage(Policy.Parameter policyParameter)
        {
            IEnumerable<Policy.VehiclesCoverage> result;
            IEnumerable<SP_GET_VEHICLES_COVERAGE_BY_POLICY_Result> temp;

            temp = globalModel.SP_GET_VEHICLES_COVERAGE_BY_POLICY(
                    policyParameter.CorpId,
                    policyParameter.RegionId,
                    policyParameter.CountryId,
                    policyParameter.DomesticregId,
                    policyParameter.StateProvId,
                    policyParameter.CityId,
                    policyParameter.OfficeId,
                    policyParameter.CaseSeqNo,
                    policyParameter.HistSeqNo
                    );

            result = temp
                .Select(vc => new Policy.VehiclesCoverage
                {
                    PolicyNo = vc.Policy_No,
                    CorpId = vc.corp_id,
                    RegionId = vc.region_id,
                    CountryId = vc.country_id,
                    DomesticRegId = vc.domesticreg_id,
                    StateProvId = vc.state_prov_id,
                    CityId = vc.city_id,
                    OfficeId = vc.office_id,
                    CaseSeqNo = vc.case_seq_no,
                    HistSeqNo = vc.hist_seq_no,
                    InsuredVehicleId = vc.insured_vehicle_id,
                    MakeDesc = vc.Make_Desc,
                    ModelDesc = vc.Model_Desc,
                    GroupId = vc.Group_Id,
                    CoverageId = vc.coverage_id,
                    CoverageTypeId = vc.coverage_type_id,
                    Oroductid = vc.product_id,
                    BlTypeId = vc.bl_type_id,
                    BlId = vc.bl_id,
                    YearVehicle = vc.YearVehicle,
                    PrincipalUse = vc.PrincipalUse,
                    Parking = vc.Parking,
                    InsuredAmount = vc.InsuredAmount,
                    Deductible = vc.Deductible,
                    PremiumAmount = vc.PremiumAmount,
                    Driver = vc.Driver,
                    ProductTypeNameKey = vc.ProductTypeNameKey,
                    PlanName = vc.PlanName,
                    VehicleUniqueId = vc.VehicleUniqueId,
                    VehicleTypeId = vc.VehicleTypeId,
                    Registry = vc.Registry,
                    JuditialSecurity = vc.JuditialSecurity,
                    Chassis = vc.Chassis,
                    InsuredDate = vc.InsuredDate,
                    EndDate = vc.EndDate,
                    HasDriverHouse = vc.HasDriverHouse,
                    HasRoadsideAssistance = vc.HasRoadsideAssistance,
                    ConditionedDocTypeId = vc.ConditionedDocTypeId,
                    ConditionedDocCategoryId = vc.ConditionedDocCategoryId,
                    ConditionedDocumentId = vc.ConditionedDocumentId,
                    VehicleTypeDesc = vc.vehicle_Type_Desc,
                    Cilindres = vc.Cilindres,
                    PassengersNo = vc.PassengersNo,
                    Closure = vc.Closure,
                    MovementPremiumAmount = vc.MovementPremiumAmount,
                    Inspection = vc.Inspection,
                    New = vc.New,
                    EndorsementAmount = vc.Endorsement_amount,
                    EndorsementBeneficiary = vc.Endorsement_beneficiary,
                    EndorsementBeneficiaryRnc = vc.Endorsement_beneficiary_rnc,
                    EndorsementClarifying = vc.Endorsement_Clarifying,
                    HasOwnDamage = vc.HasOwnDamage.ConvertToNoNullable()
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.CRBS> GetCRBS(Policy.CRBSParameter parameters)
        {
            IEnumerable<Policy.CRBS> result;
            IEnumerable<SP_GET_PL_POLICY_BY_CRBS_Result> temp;

            temp = globalModel.SP_GET_PL_POLICY_BY_CRBS(
                    parameters.ContactId,
                    parameters.ContactRoleTypeId,
                    parameters.BussinessLineId,
                    parameters.PolicyStatusId,
                    parameters.GetHistorical
                    );

            result = temp
                .Select(c => new Policy.CRBS
                {
                    CorpId = c.Corp_Id,
                    RegionId = c.Region_Id,
                    CountryId = c.Country_Id,
                    DomesticRegId = c.Domesticreg_Id,
                    StateProvId = c.State_Prov_Id,
                    CityId = c.City_Id,
                    OfficeId = c.Office_Id,
                    CaseSeqNo = c.Case_Seq_No,
                    HistSeqNo = c.Hist_Seq_No,
                    BussinessLineId = c.Bussiness_Line_Id,
                    ContactId = c.Contact_Id,
                    PolicyNo = c.Policy_No,
                    PeriodicPremium = c.Periodic_Premium,
                    EffectiveDate = c.Policy_Effective_Date,
                    PolicyStatusId = c.Policy_Status_Id,
                    PolicyStatusDesc = c.Policy_Status_Desc,
                    PolicyNameKey = c.Name_Key,
                    OfficeDesc = c.Office_Desc,
                    CompanyDesc = c.Company_Desc,
                    ProductDesc = c.Product_Desc,
                    ProductTypeDesc = c.Product_Type_Desc,
                    BusinessLineDesc = c.Bl_Desc,
                    ExpirationDate = c.Expiration_Date,
                    CreateDate = c.Create_Date
                    //IsHistorical = c.IsHistorical
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.VehicleInsured> GetVehicleInsured(Policy.Parameter parameter)
        {
            IEnumerable<Policy.VehicleInsured> result;
            IEnumerable<SP_GET_PL_POLICY_VEHICLE_INSURED_Result> temp;

            temp = globalModel.SP_GET_PL_POLICY_VEHICLE_INSURED(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo);

            result = temp
                .Select(vi => new Policy.VehicleInsured
                {
                    CorpId = vi.Corp_Id,
                    RegionId = vi.Region_Id,
                    CountryId = vi.Country_Id,
                    DomesticregId = vi.Domesticreg_Id,
                    StateProvId = vi.State_Prov_Id,
                    CityId = vi.City_Id,
                    OfficeId = vi.Office_Id,
                    CaseSeqNo = vi.Case_Seq_No,
                    HistSeqNo = vi.Hist_Seq_No,
                    InsuredVehicleId = vi.Insured_Vehicle_Id,
                    VehicleUniqueId = vi.Vehicle_Unique_Id,
                    RegistrationId = vi.Registration_Id,
                    InsuredDate = vi.Insured_Date,
                    PremiumAmount = vi.Premium_Amount,
                    MakeId = vi.Make_Id,
                    ModelId = vi.Model_Id,
                    ColorId = vi.Color_Id,
                    VehicleTypeId = vi.Vehicle_Type_Id,
                    Year = vi.Year,
                    Chassis = vi.Chassis,
                    Registry = vi.Registry,
                    PassengerNumber = vi.Passenger_Number,
                    CylindersTons = vi.Cylinders_Tons,
                    UsageId = vi.Usage_Id,
                    VehicleValue = vi.Vehicle_Value,
                    StoredId = vi.Stored_Id,
                    Garage = vi.Garage,
                    RentTypeId = vi.Rent_Type_Id,
                    RentLengthId = vi.Rent_Length_Id,
                    AmbulanceTypeId = vi.Ambulance_Type_Id,
                    GeographicLimitation = vi.Geographic_Limitation,
                    MakeDesc = vi.Make_Desc,
                    ModelDesc = vi.Model_Desc,
                    ColorDesc = vi.Color_Desc,
                    VehicleTypeDesc = vi.Vehicle_Type_Desc,
                    UsageDesc = vi.Usage_Desc,
                    StoredDesc = vi.Stored_Desc,
                    ExpirationDate = vi.Expiration_Date,
                    ProductTypeDesc = vi.Product_Type_Desc,
                    New = vi.New,
                    EndorsementAmount = vi.Endorsement_amount,
                    EndorsementBbeneficiaryRnc = vi.Endorsement_beneficiary_rnc,
                    EndorsementBeneficiary = vi.Endorsement_beneficiary,
                    EndorsementContactName = vi.Endorsement_contact_name,
                    EndorsementContactPhone = vi.Endorsement_contact_phone,
                    EndorsementContactEmail = vi.Endorsement_contact_email,
                    AppliesToReinsurance = vi.Applies_To_Reinsurance,
                    EndorsementClarifying = vi.Endorsement_Clarifying.ConvertToNoNullable(),
                    Inspection = vi.Inspection,
                    DriverContactId = vi.Driver_Contact_Id,
                    DriverFullName = vi.Driver_Full_Name,
                    ProductNameKey = vi.Product_NameKey,
                    DeductiblePercentage = vi.Deductible_Percentage,
                    HasOwnDamage = vi.HasOwnDamage.ConvertToNoNullable(),
                    VehicleVersionDesc = vi.Vehicle_Version_Desc,
                    VehicleVersionId = vi.Vehicle_Version_Id,
                    IsInspected = vi.IsInspected,
                    InspectionAddress = vi.InspectionAddress,
                    InspectionRequired = vi.InspectionRequired,
                    ReinsuranceAmount = vi.Reinsurance_Amount,
                    ReinsurancePercentage = vi.Reinsurance_Percentage,
                    rateJsonSysflex = vi.rateJsonSysflex,
                    VehicleCapacity = vi.Vehicle_Capacity,
                    DPPremiumAmount = vi.DP_Premium_Amount,
                    ReinsurancePremiumAmount = vi.Reinsurance_Premium_Amount,
                    ProratedPremium = vi.Prorated_Premium,
                    latitud = vi.Latitud,
                    longitud = vi.Longitud,
                    AccidentRate = vi.AccidentRate
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Policy.VehicleCoverage> GetVehicleCoverage(Policy.VehicleCoverageGet parameter)
        {
            IEnumerable<Policy.VehicleCoverage> result;
            IEnumerable<SP_GET_PL_POLICY_VEHICLE_COVERAGE_Result> temp;

            temp = globalModel.SP_GET_PL_POLICY_VEHICLE_COVERAGE(
                    parameter.CorpId,
                    parameter.VehicleUniqueId,
                    parameter.CoverageTypeId);

            result = temp
                .Select(vc => new Policy.VehicleCoverage
                {
                    CorpId = vc.Corp_Id,
                    RegionId = vc.Region_Id,
                    CountryId = vc.Country_Id,
                    VehicleUniqueId = vc.Vehicle_Unique_Id,
                    BlTypeId = vc.Bl_Type_Id,
                    BlId = vc.Bl_Id,
                    ProductId = vc.Product_Id,
                    VehicleTypeId = vc.Vehicle_Type_Id,
                    GroupId = vc.Group_Id,
                    CoverageTypeId = vc.Coverage_Type_Id,
                    CoverageId = vc.Coverage_Id,
                    CurrencyId = vc.Currency_Id,
                    UnitaryPrice = vc.Unitary_Price,
                    PackagePrice = vc.Package_Price,
                    DeductibleAmount = vc.Deductible_Amount,
                    DeductiblePercentage = vc.Deductible_Percentage,
                    ManualDeductibleAmount = vc.Manual_Deductible_Amount,
                    ManualDeductiblePercentage = vc.Manual_Deductible_Percentage,
                    CoverageLimit = vc.Coverage_Limit,
                    CoverageNameKey = vc.CoverageNameKey,
                    CoverageDesc = vc.Coverage_Desc,
                    CoverageTypeDesc = vc.Coverage_Type_Desc,
                    CoverageStatus = vc.Coverage_Status,
                    PremiumPercentage = vc.Premium_Percentage,
                    SubRamo = vc.SubRamo,
                    CoinsurancePercentage = vc.Coinsurance_Percentage
                })
                .ToArray();

            return
                result;
        }

        public virtual Policy.VehicleCoverage SetVehicleCoverage(Policy.VehicleCoverage vehicleCoverage)
        {
            Policy.VehicleCoverage result;
            IEnumerable<SP_SET_PL_POLICY_VEHICLE_COVERAGE_Result> temp;

            temp = globalModel.SP_SET_PL_POLICY_VEHICLE_COVERAGE(
                    vehicleCoverage.CorpId,
                    vehicleCoverage.RegionId,
                    vehicleCoverage.CountryId,
                    vehicleCoverage.VehicleUniqueId,
                    vehicleCoverage.BlTypeId,
                    vehicleCoverage.BlId,
                    vehicleCoverage.ProductId,
                    vehicleCoverage.VehicleTypeId,
                    vehicleCoverage.GroupId,
                    vehicleCoverage.CoverageTypeId,
                    vehicleCoverage.CoverageId,
                    vehicleCoverage.CurrencyId,
                    vehicleCoverage.UnitaryPrice,
                    vehicleCoverage.PackagePrice,
                    vehicleCoverage.DeductibleAmount,
                    vehicleCoverage.DeductiblePercentage,
                    vehicleCoverage.ManualDeductibleAmount,
                    vehicleCoverage.ManualDeductiblePercentage,
                    vehicleCoverage.CoverageStatus,
                    vehicleCoverage.CoverageLimit,
                    vehicleCoverage.UserId,
                    vehicleCoverage.PremiumPercentage,
                    null,
                    null,
                    null,
                    null,
                    null
                  );

            result = temp
                    .Select(vc => new Policy.VehicleCoverage
                    {
                        CorpId = vc.Corp_Id.ConvertToNoNullable(),
                        RegionId = vc.Region_Id.ConvertToNoNullable(),
                        CountryId = vc.Country_Id.ConvertToNoNullable(),
                        VehicleUniqueId = vc.Vehicle_Unique_Id.ConvertToNoNullable(),
                        BlTypeId = vc.Bl_Type_Id.ConvertToNoNullable(),
                        BlId = vc.Bl_Id.ConvertToNoNullable(),
                        ProductId = vc.Product_Id.ConvertToNoNullable(),
                        VehicleTypeId = vc.Vehicle_Type_Id.ConvertToNoNullable(),
                        GroupId = vc.Group_Id.ConvertToNoNullable(),
                        CoverageTypeId = vc.Coverage_Type_Id.ConvertToNoNullable(),
                        CoverageId = vc.Coverage_Id.ConvertToNoNullable()
                    })
                    .FirstOrDefault();

            return
                result;
        }
        public virtual Policy.VehicleInsured SetVehicleInsured(Policy.VehicleInsured vehicleInsured)
        {
            Policy.VehicleInsured result;
            IEnumerable<SP_SET_PL_POLICY_VEHICLE_INSURED_Result> temp;

            temp = globalModel.SP_SET_PL_POLICY_VEHICLE_INSURED(
                    vehicleInsured.CorpId,
                    vehicleInsured.RegionId,
                    vehicleInsured.CountryId,
                    vehicleInsured.DomesticregId,
                    vehicleInsured.StateProvId,
                    vehicleInsured.CityId,
                    vehicleInsured.OfficeId,
                    vehicleInsured.CaseSeqNo,
                    vehicleInsured.HistSeqNo,
                    vehicleInsured.InsuredVehicleId,
                    vehicleInsured.RegistrationId,
                    vehicleInsured.InsuredDate,
                    vehicleInsured.PremiumAmount,
                    vehicleInsured.BasePremiumAmount,
                    vehicleInsured.MakeId,
                    vehicleInsured.ModelId,
                    vehicleInsured.ColorId,
                    vehicleInsured.VehicleTypeId,
                    vehicleInsured.Year,
                    vehicleInsured.Chassis,
                    vehicleInsured.Registry,
                    vehicleInsured.PassengerNumber,
                    vehicleInsured.CylindersTons,
                    vehicleInsured.UsageId,
                    vehicleInsured.VehicleValue,
                    vehicleInsured.StoredId,
                    vehicleInsured.Garage,
                    vehicleInsured.RentTypeId,
                    vehicleInsured.RentLengthId,
                    vehicleInsured.AmbulanceTypeId,
                    vehicleInsured.GeographicLimitation,
                    vehicleInsured.Inspection,
                    vehicleInsured.New,
                    vehicleInsured.EndorsementBeneficiary,
                    vehicleInsured.EndorsementBbeneficiaryRnc,
                    vehicleInsured.EndorsementAmount,
                    vehicleInsured.EndorsementContactName,
                    vehicleInsured.EndorsementContactPhone,
                    vehicleInsured.EndorsementContactEmail,
                    vehicleInsured.AppliesToReinsurance,
                    vehicleInsured.ReinsuranceAmount,
                    vehicleInsured.EndorsementClarifying,
                    vehicleInsured.SourceId,
                    vehicleInsured.UserId,
                    vehicleInsured.InspectionAddress,
                    null,
                    null,
                    vehicleInsured.longitud,
                    vehicleInsured.latitud
                  );

            result = temp
                    .Select(vc => new Policy.VehicleInsured
                    {
                        CorpId = vc.Corp_Id,
                        RegionId = vc.Region_Id,
                        CountryId = vc.Country_Id,
                        DomesticregId = vc.Domesticreg_Id,
                        StateProvId = vc.State_Prov_Id,
                        CityId = vc.City_Id,
                        OfficeId = vc.Office_Id,
                        CaseSeqNo = vc.Case_Seq_No,
                        HistSeqNo = vc.Hist_Seq_No,
                        InsuredVehicleId = vc.Insured_Vehicle_Id
                    })
                    .FirstOrDefault();

            return
                result;
        }

        #region Discount
        public virtual IEnumerable<Policy.Vehicle.Discount> GetPolicyVehicleDiscount(Policy.DVParameter parameter)
        {
            IEnumerable<Policy.Vehicle.Discount> result;
            IEnumerable<SP_GET_PL_PCY_VEHICLE_DISCOUNTS_Result> temp;

            temp = globalModel.SP_GET_PL_PCY_VEHICLE_DISCOUNTS(
                    parameter.InsuredVehicleId,
                    parameter.DiscountId,
                    parameter.DiscountRuleId,
                    parameter.DiscountRuleDetailId,
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo
                    );

            result = temp
                .Select(vd => new Policy.Vehicle.Discount
                {
                    InsuredVehicleId = vd.Insured_Vehicle_Id,
                    DiscountId = vd.Discount_Id,
                    DiscountRuleId = vd.Discount_Rule_Id,
                    DiscountRuleDetailId = vd.Discount_Rule_Detail_Id,
                    OldPremiumAmount = vd.Old_Premium_Amount,
                    CorpId = vd.Corp_Id,
                    RegionId = vd.Region_Id,
                    CountryId = vd.Country_Id,
                    DomesticregId = vd.Domesticreg_Id,
                    StateProvId = vd.State_Prov_Id,
                    CityId = vd.City_Id,
                    OfficeId = vd.Office_Id,
                    CaseSeqNo = vd.Case_Seq_No,
                    HistSeqNo = vd.Hist_Seq_No,
                    DetailRuleValue = vd.Detail_Rule_Value
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Policy.Vehicle.Discount.RulesAndDetails> GetDiscountRulesAndDetails(Policy.DParameter parameter)
        {
            IEnumerable<Policy.Vehicle.Discount.RulesAndDetails> result;
            IEnumerable<SP_GET_PL_DISCOUNT_RULES_AND_DETAILS_Result> temp;

            temp = globalModel.SP_GET_PL_DISCOUNT_RULES_AND_DETAILS(
                    parameter.NameKey,
                    parameter.DiscountRuleId,
                    parameter.Active,
                    parameter.CorpId,
                    parameter.Role
                    );

            result = temp
                .Select(rd => new Policy.Vehicle.Discount.RulesAndDetails
                {
                    CorpId = rd.Corp_Id,
                    DiscountRuleId = rd.Discount_Rule_Id,
                    DiscountRuleDesc = rd.Discount_Rule_Desc,
                    NameKey = rd.Name_Key,
                    Active = rd.Active,
                    DetailId = rd.Detail_Id,
                    DetailApplyDate = rd.Detail_Apply_Date,
                    DetailRuleValue = rd.Detail_Rule_Value,
                    DetailActive = rd.Detail_Active,
                    DetailNameKey = rd.Detail_NameKey
                })
                .ToArray();

            return
                result;
        }

        public virtual Policy.Vehicle.Discount SetPolicyVehicleDiscount(Policy.Vehicle.Discount parameter)
        {
            Policy.Vehicle.Discount result;
            IEnumerable<SP_SET_PL_PCY_VEHICLE_DISCOUNTS_Result> temp;

            temp = globalModel.SP_SET_PL_PCY_VEHICLE_DISCOUNTS(
                    parameter.InsuredVehicleId,
                    parameter.DiscountId,
                    parameter.DiscountRuleId,
                    parameter.DiscountRuleDetailId,
                    parameter.OldPremiumAmount,
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.UserId,
                    parameter.NotePredefiniedId
                );

            result = temp
                .Select(vd => new Policy.Vehicle.Discount
                {
                    CorpId = vd.Corp_Id,
                    RegionId = vd.Region_Id,
                    CountryId = vd.Country_Id,
                    DomesticregId = vd.Domesticreg_Id,
                    StateProvId = vd.State_Prov_Id,
                    CityId = vd.City_Id,
                    OfficeId = vd.Office_Id,
                    CaseSeqNo = vd.Case_Seq_No,
                    HistSeqNo = vd.Hist_Seq_No,
                    InsuredVehicleId = vd.Insured_Vehicle_Id,
                    DiscountId = vd.Discount_Id
                })
                .FirstOrDefault();

            return
                result;
        }
        #endregion

        public virtual IEnumerable<Policy.Agent.SaleChannelInfo> GetAgentSaleChannelInfo(Policy.Parameter parameter)
        {
            IEnumerable<Policy.Agent.SaleChannelInfo> result;
            IEnumerable<SP_GET_EN_AGENT_SALES_CHANNELS_INFO_Result> temp;

            temp = globalModel.SP_GET_EN_AGENT_SALES_CHANNELS_INFO(
                    parameter.CorpId,
                    parameter.AgentId
                    );

            result = temp
                .Select(sc => new Policy.Agent.SaleChannelInfo
                {
                    CorpId = sc.Corp_Id,
                    RegionId = sc.Region_Id,
                    CountryId = sc.Country_Id,
                    BlTypeId = sc.Bl_Type_Id,
                    BlId = sc.Bl_Id,
                    AgentId = sc.Agent_Id,
                    DistributionId = sc.Distribution_Id,
                    DistributionDesc = sc.Distribution_Desc,
                    BlDesc = sc.Bl_Desc,
                    ChainLevelDesc = sc.Chain_level_Desc,
                    OrderNumber = sc.Order_Number
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.VehicleCoverageSurcharge> GetVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            IEnumerable<Policy.VehicleCoverageSurcharge> result;
            IEnumerable<SP_GET_PL_PCY_VEHICLE_COVERAGE_SURCHARGE_Result> temp;

            temp = globalModel.SP_GET_PL_PCY_VEHICLE_COVERAGE_SURCHARGE(
                    parameter.SurchargeId,
                    parameter.DiscountRuleId,
                    parameter.DiscountRuleDetailId,
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.VehicleUniqueId,
                    parameter.BlTypeId,
                    parameter.BlId,
                    parameter.ProductId,
                    parameter.VehicleTypeId,
                    parameter.GroupId,
                    parameter.CoverageTypeId,
                    parameter.CoverageId,
                    parameter.Language
                    );

            result = temp
                .Select(vcs => new Policy.VehicleCoverageSurcharge
                {
                    CorpId = vcs.Corp_Id,
                    RegionId = vcs.Region_Id,
                    CountryId = vcs.Country_Id,
                    VehicleUniqueId = vcs.Vehicle_Unique_Id,
                    BlTypeId = vcs.Bl_Type_Id,
                    BlId = vcs.Bl_Id,
                    ProductId = vcs.Product_Id,
                    VehicleTypeId = vcs.Vehicle_Type_Id,
                    GroupId = vcs.Group_Id,
                    CoverageTypeId = vcs.Coverage_Type_Id,
                    CoverageId = vcs.Coverage_Id,
                    SurchargeId = vcs.Surcharge_Id,
                    DiscountRuleId = vcs.Discount_Rule_Id,
                    DiscountRuleDetailId = vcs.Discount_Rule_Detail_Id,
                    OldCoverageAmount = vcs.Old_Coverage_Amount,
                    DetailRuleValue = vcs.Detail_Rule_Value,
                    NotePredefiniedId = vcs.Note_Predefinied_Id,
                    DiscountRuleDesc = vcs.Discount_Rule_Desc,
                    DiscountRuleNameKey = vcs.Discount_Rule_Name_Key,
                    DetailApplyDate = vcs.Detail_Apply_Date,
                    DetailRuleNameKey = vcs.Detail_Rule_NameKey,
                    NotePredefiniedDesc = vcs.Note_Predefinied_Desc,
                    NoteNameKey = vcs.Note_Name_Key,
                    BasePremiumAmount = vcs.Base_Premium_Amount
                })
                .ToArray();

            return
                result;
        }

        public virtual Policy.VehicleCoverageSurcharge SetVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            Policy.VehicleCoverageSurcharge result;
            IEnumerable<SP_SET_PL_PCY_VEHICLE_COVERAGE_SURCHARGE_Result> temp;

            temp = globalModel.SP_SET_PL_PCY_VEHICLE_COVERAGE_SURCHARGE(
                    parameter.SurchargeId,
                    parameter.DiscountRuleId,
                    parameter.DiscountRuleDetailId,
                    parameter.OldCoverageAmount,
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.VehicleUniqueId,
                    parameter.BlTypeId,
                    parameter.BlId,
                    parameter.ProductId,
                    parameter.VehicleTypeId,
                    parameter.GroupId,
                    parameter.CoverageTypeId,
                    parameter.CoverageId,
                    parameter.UserId,
                    parameter.NotePredefiniedId
                  );

            result = temp
                    .Select(vcs => new Policy.VehicleCoverageSurcharge
                    {
                        CorpId = vcs.Corp_Id,
                        RegionId = vcs.Region_Id,
                        CountryId = vcs.Country_Id,
                        VehicleUniqueId = vcs.Vehicle_Unique_Id,
                        BlTypeId = vcs.Bl_Type_Id,
                        BlId = vcs.Bl_Id,
                        ProductId = vcs.Product_Id,
                        VehicleTypeId = vcs.Vehicle_Type_Id,
                        GroupId = vcs.Group_Id,
                        CoverageTypeId = vcs.Coverage_Type_Id,
                        CoverageId = vcs.Coverage_Id,
                        SurchargeId = vcs.Surcharge_Id
                    })
                    .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Policy.Document> GetPolicyDocument(Policy.Document parameter)
        {
            IEnumerable<Policy.Document> result;
            IEnumerable<SP_GET_PL_PCY_DOCUMENTS_Result> temp;

            temp = globalModel.SP_GET_PL_PCY_DOCUMENTS(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticRegId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo
                  );

            result = temp
                    .Select(dk => new Policy.Document
                    {
                        CorpId = dk.Corp_Id,
                        RegionId = dk.Region_Id,
                        CountryId = dk.Country_Id,
                        DomesticRegId = dk.Domesticreg_Id,
                        StateProvId = dk.State_Prov_Id,
                        CityId = dk.City_Id,
                        OfficeId = dk.Office_Id,
                        CaseSeqNo = dk.Case_Seq_No,
                        HistSeqNo = dk.Hist_Seq_No,
                        DocumentTypeId = dk.Doc_Type_Id,
                        DocumentCategoryId = dk.Doc_Category_Id,
                        DocumentId = dk.Document_Id,
                        DocumentName = dk.Document_Name
                    })
                    .ToArray();

            return
                result;
        }

        public virtual Policy.Document SetPolicyDocument(Policy.Document parameter)
        {
            Policy.Document result;
            IEnumerable<SP_SET_PL_PCY_DOCUMENTS_Result> temp;

            temp = globalModel.SP_SET_PL_PCY_DOCUMENTS(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticRegId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.DocumentTypeId,
                    parameter.DocumentCategoryId,
                    parameter.DocumentId,
                    parameter.DocumentStatusId,
                    parameter.UserId
                  );

            result = temp
                    .Select(dk => new Policy.Document
                    {
                        CorpId = dk.Corp_Id,
                        RegionId = dk.Region_Id,
                        CountryId = dk.Country_Id,
                        DomesticRegId = dk.Domesticreg_Id,
                        StateProvId = dk.State_Prov_Id,
                        CityId = dk.City_Id,
                        OfficeId = dk.Office_Id,
                        CaseSeqNo = dk.Case_Seq_No,
                        HistSeqNo = dk.Hist_Seq_No,
                        DocumentTypeId = dk.Doc_Type_Id,
                        DocumentCategoryId = dk.Doc_Category_Id,
                        DocumentId = dk.Document_Id
                    })
                    .FirstOrDefault();

            return
                result;
        }

        public virtual int? SetPolicyDirectDebit(int? corp_Id, int? region_Id, int? country_Id, int? domesticreg_Id, int? state_Prov_Id, int? city_Id, int? office_Id, int? case_Seq_No, int? hist_Seq_No, bool? DirectDebit, bool? includeInitial, int? userId)
        {
            globalModelExtended.SP_SET_PL_POLICY_DIRECTCREDIT
                (
                corp_Id,
                region_Id,
                country_Id,
                domesticreg_Id,
                state_Prov_Id,
                city_Id,
                office_Id,
                case_Seq_No,
                hist_Seq_No,
                DirectDebit,
                includeInitial,
                userId
                );

            return
                -1;
        }

        public virtual int? SetPolicyLoanNo(int? corp_Id, int? region_Id, int? country_Id, int? domesticreg_Id, int? state_Prov_Id, int? city_Id, int? office_Id, int? case_Seq_No, int? hist_Seq_No, string loanPetitionNo, int? userId)
        {
            globalModelExtended.SP_SET_PL_POLICY_LOANNUMBER

                (
                corp_Id,
                region_Id,
                country_Id,
                domesticreg_Id,
                state_Prov_Id,
                city_Id,
                office_Id,
                case_Seq_No,
                hist_Seq_No,
                loanPetitionNo,
                userId
                );

            return
                -1;
        }

        public virtual int SetPolicyNo(Policy.Number parameter)
        {
            int result;
            IEnumerable<SP_SET_PL_POLICY_POLICYNO_Result> temp;

            temp = globalModel.SP_SET_PL_POLICY_POLICYNO(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticRegId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.PolicyNo,
                    parameter.UserId);

            result = temp
                .Select(pn => pn.Result)
                .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Policy.VehicleInsured.CoverageTypePremiun> GetVehicleInsuredCoverageTypePremiun(Policy.VehicleInsured.CoverageTypePremiun.Key parameter)
        {
            IEnumerable<Policy.VehicleInsured.CoverageTypePremiun> result;
            IEnumerable<SP_GET_PL_POLICY_VEHICLE_INSURED_COVERAGE_TYPE_Result> temp;

            temp = globalModel.SP_GET_PL_POLICY_VEHICLE_INSURED_COVERAGE_TYPE(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticRegId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.InsuredVehicleId,
                    parameter.CoverageTypeId);

            result = temp
                .Select(ct => new Policy.VehicleInsured.CoverageTypePremiun
                {
                    CorpId = ct.Corp_Id,
                    RegionId = ct.Region_Id,
                    CountryId = ct.Country_Id,
                    DomesticRegId = ct.Domesticreg_Id,
                    StateProvId = ct.State_Prov_Id,
                    CityId = ct.City_Id,
                    OfficeId = ct.Office_Id,
                    CaseSeqNo = ct.Case_Seq_No,
                    HistSeqNo = ct.Hist_Seq_No,
                    InsuredVehicleId = ct.Insured_Vehicle_Id,
                    CoverageTypeId = ct.Coverage_Type_Id,
                    PremiumAmount = ct.Premium_Amount,
                    BasePremiumAmount = ct.Base_Premium_Amount,
                    CoverageTypeDesc = ct.Coverage_Type_Desc,
                    VehicleUniqueId = ct.Vehicle_Unique_Id
                })
                .ToArray();

            return
                result;
        }
        public virtual Policy.VehicleInsured.CoverageTypePremiun.Key SetVehicleInsuredCoverageTypePremiun(Policy.VehicleInsured.CoverageTypePremiun parameter)
        {
            Policy.VehicleInsured.CoverageTypePremiun.Key result;
            IEnumerable<SP_SET_PL_POLICY_VEHICLE_INSURED_COVERAGE_TYPE_Result> temp;

            temp = globalModel.SP_SET_PL_POLICY_VEHICLE_INSURED_COVERAGE_TYPE(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticRegId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.InsuredVehicleId,
                    parameter.CoverageTypeId,
                    parameter.PremiumAmount,
                    parameter.BasePremiumAmount,
                    parameter.UserId
                  );

            result = temp
                    .Select(vc => new Policy.VehicleInsured.CoverageTypePremiun.Key
                    {
                        CorpId = vc.Corp_Id,
                        RegionId = vc.Region_Id,
                        CountryId = vc.Country_Id,
                        DomesticRegId = vc.Domesticreg_Id,
                        StateProvId = vc.State_Prov_Id,
                        CityId = vc.City_Id,
                        OfficeId = vc.Office_Id,
                        CaseSeqNo = vc.Case_Seq_No,
                        HistSeqNo = vc.Hist_Seq_No,
                        InsuredVehicleId = vc.Insured_Vehicle_Id,
                        CoverageTypeId = vc.Coverage_Type_Id
                    })
                    .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Policy.VehicleInsured.Discount> GetVehicleInsuredDiscount(Policy.VehicleInsured.Discount.Key parameter)
        {
            IEnumerable<Policy.VehicleInsured.Discount> result;
            IEnumerable<SP_GET_PL_POLICY_VEHICLE_INSURED_DISCOUNT_Result> temp;

            temp = globalModel.SP_GET_PL_POLICY_VEHICLE_INSURED_DISCOUNT(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticRegId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.InsuredVehicleId,
                    parameter.DiscountId,
                    parameter.LanguageId);

            result = temp
                .Select(d => new Policy.VehicleInsured.Discount
                {
                    CorpId = d.Corp_Id,
                    RegionId = d.Region_Id,
                    CountryId = d.Country_Id,
                    DomesticRegId = d.Domesticreg_Id,
                    StateProvId = d.State_Prov_Id,
                    CityId = d.City_Id,
                    OfficeId = d.Office_Id,
                    CaseSeqNo = d.Case_Seq_No,
                    HistSeqNo = d.Hist_Seq_No,
                    InsuredVehicleId = d.Insured_Vehicle_Id,
                    DiscountId = d.Discount_Id,
                    DiscountRuleId = d.Discount_Rule_Id,
                    DiscountRuleDetailId = d.Discount_Rule_Detail_Id,
                    NotePredefiniedId = d.Note_Predefinied_Id,
                    PremiumAmount = d.Premium_Amount,
                    OldPremiumAmount = d.Old_Premium_Amount,
                    DetailApplyDate = d.Detail_Apply_Date,
                    DetailRuleValue = d.Detail_Rule_Value,
                    DetailRuleNameKey = d.Detail_Rule_NameKey,
                    DiscountRuleDesc = d.Discount_Rule_Desc,
                    DiscountNameKey = d.Discount_Name_Key,
                    NotePredefiniedDesc = d.Note_Predefinied_Desc,
                    NoteNameKey = d.Note_NameKey,
                    Comment = d.Comment,
                    FullName = d.FullName,
                    UserId = d.UserId
                })
                .ToArray();

            return
                result;
        }
        public virtual Policy.VehicleInsured.Discount.Key SetVehicleInsuredDiscount(Policy.VehicleInsured.Discount parameter)
        {
            Policy.VehicleInsured.Discount.Key result;
            IEnumerable<SP_SET_PL_POLICY_VEHICLE_INSURED_DISCOUNT_Result> temp;

            temp = globalModel.SP_SET_PL_POLICY_VEHICLE_INSURED_DISCOUNT(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticRegId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.InsuredVehicleId,
                    parameter.DiscountId,
                    parameter.DiscountRuleId,
                    parameter.DiscountRuleDetailId,
                    parameter.NotePredefiniedId,
                    parameter.PremiumAmount,
                    parameter.OldPremiumAmount,
                    parameter.Comment,
                    parameter.DiscountStatus,
                    parameter.UserId
                  );

            result = temp
                    .Select(d => new Policy.VehicleInsured.Discount.Key
                    {
                        CorpId = d.Corp_Id,
                        RegionId = d.Region_Id,
                        CountryId = d.Country_Id,
                        DomesticRegId = d.Domesticreg_Id,
                        StateProvId = d.State_Prov_Id,
                        CityId = d.City_Id,
                        OfficeId = d.Office_Id,
                        CaseSeqNo = d.Case_Seq_No,
                        HistSeqNo = d.Hist_Seq_No,
                        InsuredVehicleId = d.Insured_Vehicle_Id,
                        DiscountId = d.Discount_Id
                    })
                    .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Policy.DocumentQuotation> GetQuotationDocumentReview(Policy.Parameter parameter)
        {
            IEnumerable<Policy.DocumentQuotation> result;
            IEnumerable<SP_GET_DOCUMENT_TO_REVIEW_QUOTATION_Result> temp;

            temp = globalModel.SP_GET_DOCUMENT_TO_REVIEW_QUOTATION(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo
                    );

            result = temp
                .Select(qd => new Policy.DocumentQuotation
                {
                    CorpId = qd.Corp_Id,
                    RegionId = qd.Region_Id,
                    CountryId = qd.Country_Id,
                    DomesticRegId = qd.Domesticreg_Id,
                    StateProvId = qd.State_Prov_Id,
                    CityId = qd.City_Id,
                    OfficeId = qd.Office_Id,
                    CaseSeqNo = qd.Case_Seq_No,
                    HistSeqNo = qd.Hist_Seq_No,
                    DocTypeId = qd.Doc_Type_Id,
                    DocCategoryId = qd.Doc_Category_Id,
                    DocumentId = qd.Document_Id,
                    ProjectId = qd.Project_Id,
                    TabId = qd.Tab_Id,
                    FunctionalityId = qd.Functionality_Id,
                    FunctionalitySeqNo = qd.Functionality_Seq_No,
                    NameDesc = qd.Name_Desc,
                    NameKey = qd.Name_Key,
                    TabDesc = qd.Tab_Desc,
                    LastUpdate = qd.LastUpdate,
                    IsReviewed = qd.IsReviewed.ConvertToNoNullable()
                })
                .ToArray();

            return
                result;
        }

        public virtual int SetVehicleInsuredInspection(Policy.VehicleInsured.InspectionV parameter)
        {
            int result;
            IEnumerable<SP_UPDATE_PL_POLICY_VEHICLE_INSURED_INSPECTION_Result> temp;

            temp = globalModelExtended.SP_UPDATE_PL_POLICY_VEHICLE_INSURED_INSPECTION(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticRegId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.VehicleUniqueId,
                    parameter.Inspection,
                    parameter.EndorsementClarifying,
                    parameter.UserId
                  );

            result = temp
                    .Select(d => d.Result)
                    .FirstOrDefault();

            return
                result;
        }

        public virtual int SetVehicleInsuredApplyDiscountAndSurcharge(Policy.Parameter parameter)
        {
            int result;
            IEnumerable<SP_SET_PL_POLICY_VEHICLE_INSURED_APPLY_DISCOUNT_AND_SURCHARGE_Result> temp;

            temp = globalModel.SP_SET_PL_POLICY_VEHICLE_INSURED_APPLY_DISCOUNT_AND_SURCHARGE(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.UserId
                  );

            result = temp
                    .Select(d => d.Result)
                    .FirstOrDefault();

            return
                result;
        }

        public virtual int DeleteVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            int result;
            IEnumerable<SP_DELETE_PL_PCY_VEHICLE_COVERAGE_SURCHARGE_Result> temp;

            temp = globalModel.SP_DELETE_PL_PCY_VEHICLE_COVERAGE_SURCHARGE(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.VehicleUniqueId,
                    parameter.BlTypeId,
                    parameter.BlId,
                    parameter.ProductId,
                    parameter.VehicleTypeId,
                    parameter.GroupId,
                    parameter.CoverageTypeId,
                    parameter.CoverageId,
                    parameter.SurchargeId,
                    parameter.UserId
                  );

            result = temp
                    .Select(d => d.Result)
                    .FirstOrDefault();

            return
                result;
        }

        public virtual int UpdateVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            int result;
            IEnumerable<SP_UPDATE_PL_PCY_VEHICLE_COVERAGE_SURCHARGE_Result> temp;

            temp = globalModel.SP_UPDATE_PL_PCY_VEHICLE_COVERAGE_SURCHARGE(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.VehicleUniqueId,
                    parameter.BlTypeId,
                    parameter.BlId,
                    parameter.ProductId,
                    parameter.VehicleTypeId,
                    parameter.GroupId,
                    parameter.CoverageTypeId,
                    parameter.CoverageId,
                    parameter.SurchargeId,
                    parameter.UserId
                  );

            result = temp
                    .Select(d => d.Result)
                    .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Policy.Vehicle.Requirement> GetVehicleRequirement(Policy.Parameter parameter)
        {
            IEnumerable<Policy.Vehicle.Requirement> result;
            IEnumerable<SP_GET_PL_POLICY_VEHICLE_REQUIREMENT_Result> temp;

            temp = globalModel.SP_GET_PL_POLICY_VEHICLE_REQUIREMENT(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.ContactId
                    );

            result = temp
                .Select(sc => new Policy.Vehicle.Requirement
                {
                    CorpId = sc.Corp_Id,
                    RequirementCatId = sc.Requirement_Cat_Id,
                    RequirementCatDesc = sc.Requirement_Cat_Desc,
                    RequirementTypeId = sc.Requirement_Type_Id,
                    RequirementTypeDesc = sc.Requirement_Type_Desc,
                    DocTypeId = sc.Doc_Type_Id,
                    DocCategoryId = sc.Doc_Category_Id,
                    Automatic = sc.Automatic,
                    RequimentPolicyOnly = sc.Requiment_Policy_Only,
                    RequimentAssingToInsured = sc.Requiment_Assing_To_Insured,
                    RegionId = sc.Region_Id,
                    CountryId = sc.Country_Id,
                    DomesticregId = sc.Domesticreg_Id,
                    StateProvId = sc.State_Prov_Id,
                    CityId = sc.City_Id,
                    OfficeId = sc.Office_Id,
                    CaseSeqNo = sc.Case_Seq_No,
                    HistSeqNo = sc.Hist_Seq_No,
                    ContactId = sc.Contact_Id,
                    RequirementId = sc.Requirement_Id,
                    RequirementDocId = sc.Requirement_Doc_Id,
                    DocumentId = sc.Document_Id,
                    IsValid = sc.IsValid,
                    IsMandatory = sc.Is_Mandatory,
                    InsuredVehicleId = sc.Insured_Vehicle_Id,
                    VehicleUniqueId = sc.Vehicle_Unique_Id,
                    AssingTo = sc.AssingTo,
                    FunctionalityId = sc.Functionality_Id,
                    FunctionalitySeqNo = sc.Functionality_Seq_No,
                    UploadById = sc.UploadById,
                    UploadByUserName = sc.UploadByUserName,
                    ValidById = sc.ValidById,
                    ValidByUserName = sc.ValidByUserName,
                    ValidByDate = sc.ValidByDate,
                    RequimentOnBaseNameKey = sc.Requiment_On_Base_Name_Key,
                    RequirementTypeSubType = sc.Requirement_Type_SubType,
                    EndorsementBeneficiary = sc.Endorsement_beneficiary
                })
                .ToArray();

            return
                result;
        }

        public virtual int SetAssingQuotation(Policy.Parameter parameter)
        {
            int result;
            IEnumerable<SP_SET_PL_PCY_ASSIGN_QUOTATION_Result> temp;

            temp = globalModel.SP_SET_PL_PCY_ASSIGN_QUOTATION(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.AgentId,
                    parameter.UserId
                  );

            result = temp
                    .Select(d => d.Result)
                    .FirstOrDefault();

            return
                result;
        }

        public virtual int UpdateQuotationInfoTemp(Policy.Quo.Temp parameter)
        {
            string query;
            SqlCommand command;
            SqlConnection connection;

            query = "exec [Policy].[SP_SET_FT_GET_QUO_PL_POLICY_GRIDVIEW] @Policy_No,@UserId,@ReturnResultSet";
            connection = new SqlConnection(base.globalconexionStringForAdo);
            command = new SqlCommand(query, connection);
            command.CommandTimeout = 0;

            command.Parameters.Add(new SqlParameter("@Policy_No", parameter.PolicyNo));
            command.Parameters.Add(new SqlParameter("@UserId", parameter.UserId));
            command.Parameters.Add(new SqlParameter("@ReturnResultSet", parameter.ReturnResultSet));

            try
            {
                if (connection != null && connection.State != ConnectionState.Open)
                    connection.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return
                -1;
        }

        public virtual Policy.Quo.Temp.TempResult UpdateOneQuotationInfoTemp(Policy.Quo.Temp parameter)
        {
            string query;
            SqlCommand command;
            SqlConnection connection;
            DataTable dT;
            SqlDataAdapter dataAdapter;

            Policy.Quo.Temp.TempResult result = null;

            query = "exec [Policy].[SP_SET_FT_GET_QUO_PL_POLICY_GRIDVIEW] @Policy_No,@UserId,@ReturnResultSet";
            connection = new SqlConnection(base.globalconexionStringForAdo);
            command = new SqlCommand(query, connection);
            command.CommandTimeout = 0;

            command.Parameters.Add(new SqlParameter("@Policy_No", parameter.PolicyNo));
            command.Parameters.Add(new SqlParameter("@UserId", parameter.UserId));
            command.Parameters.Add(new SqlParameter("@ReturnResultSet", parameter.ReturnResultSet));
            dT = new DataTable();

            try
            {
                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dT);

                if (dT.Rows.Count > 0)
                {
                    result = new Policy.Quo.Temp.TempResult();

                    result.Action = dT.Rows[0]["Action"].ToString();
                    result.Tab = dT.Rows[0]["Tab"].ToString();
                }

                //if (connection != null && connection.State != ConnectionState.Open)
                //    connection.Open();

                //command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return
                result;
        }

        public virtual IEnumerable<Policy.Quo> GetQuotationInfoTemp(Policy.Quo.Temp parameter)
        {
            IEnumerable<Policy.Quo> result;
            IEnumerable<SP_GET_FT_QUO_PL_POLICY_GRIDVIEW_Result> temp;

            temp = globalModel.SP_GET_FT_QUO_PL_POLICY_GRIDVIEW(
                    parameter.PolicyNo,
                    parameter.ContactId
                  );

            result = temp
                .Select(pq => new Policy.Quo
                {
                    CorpId = pq.Corp_Id,
                    RegionId = pq.Region_Id,
                    CountryId = pq.Country_Id,
                    DomesticregId = pq.Domesticreg_Id,
                    StateProvId = pq.State_Prov_Id,
                    CityId = pq.City_Id,
                    OfficeId = pq.Office_Id,
                    CaseSeqNo = pq.Case_Seq_No,
                    HistSeqNo = pq.Hist_Seq_No,
                    BussinessLineType = pq.Bussiness_Line_Type,
                    BussinessLineId = pq.Bussiness_Line_Id,
                    CompanyId = pq.Company_Id,
                    CompanyDesc = pq.Company_Desc,
                    PolicyNo = pq.Policy_No,
                    BlDesc = pq.Bl_Desc,
                    ProductTypeDesc = pq.Product_Type_Desc,
                    OfficeDesc = pq.Office_Desc,
                    AgentId = pq.Agent_Id,
                    DistributionDesc = pq.Distribution_Desc,
                    PolicyStatusId = pq.Policy_Status_Id,
                    PolicyStatusDesc = pq.Policy_Status_Desc,
                    PolicyStatusNameKey = pq.Policy_Status_Name_Key,
                    AgentName = pq.Agent_Name,
                    ContactId = pq.Contact_Id,
                    FullName = pq.Full_Name,
                    IdContact = pq.Id_Contact,
                    InsuredAmount = pq.Insured_Amount,
                    AnnualPremium = pq.Annual_Premium,
                    InitialContribution = pq.Initial_Contribution,
                    QuoDate = pq.QuoDate,
                    EffectiveDate = pq.EffectiveDate,
                    ExpirationDate = pq.ExpirationDate,
                    PolicyExpirationDate = pq.PolicyExpirationDate,
                    InspectionQuoDate = pq.InspectionQuoDate,
                    DocumentMissing = pq.Document_Missing,
                    SubscriberAgentId = pq.Subscriber_Agent_Id,
                    SubscriberName = pq.Subscriber_Name,
                    InspectorAgentId = pq.Inspector_Agent_Id,
                    InspectorName = pq.Inspector_Name,
                    Days = pq.Days,
                    IsExpiring = pq.IsExpiring,
                    IsExpired = pq.IsExpired,
                    IsPolicy = pq.IsPolicy,
                    PolicyNoTemp = pq.Policy_No_Temp,
                    HasDiscount = pq.HasDiscount,
                    Tab = pq.Tab,
                    SupervisorAgentName = pq.Supervisor_Agent_Name,
                    TipoRiesgoNameKey = pq.Tipo_Riesgo_Name_Key,
                    DeclinedQuoDate = pq.DeclinedQuoDate,
                    HasSurcharge = pq.HasSurcharge,
                    BlacklistCheck = pq.Blacklist_Check,
                    BlacklistCheckUser = pq.Blacklist_Check_User,
                    BlacklistCheckUserName = pq.Blacklist_Check_UserName,
                    BlacklistMember = pq.Blacklist_Member,
                    MonthlyPayment = pq.MonthlyPayment,
                    Financed = pq.Financed,
                    Period = pq.Period,
                    TaxPercentage = pq.TaxPercentage,
                    LoanPetitionNo = pq.LoanPetitionNo,
                    DirectDebit = pq.Direct_Debit,
                    DomicileInitialPayment = pq.DomicileInitialPayment,
                    ProratedPremium = pq.Prorated_Premium,
                    RequestTypeId = pq.Request_Type_Id,
                    RequestTypeDesc = pq.Request_Type_Desc,
                    RiskLevel = pq.RiskLevel,
                    VendorAccidentRate = pq.VendorAccidentRate,
                    AgentAccidentRate = pq.AgentAccidentRate
                })
                .ToArray();

            return
                result;
        }

        public virtual int SetPolicySourceId(Policy.PSourceId parameter)
        {
            int result;
            IEnumerable<SP_SET_PL_POLICY_SOURCEID_Result> temp;

            temp = globalModel.SP_SET_PL_POLICY_SOURCEID(
                    parameter.PolicyNo,
                    parameter.SourceId,
                    parameter.UserId
                  );

            result = temp
                    .Select(d => d.Result)
                    .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Policy.Agent> GetAgentIdListByAgentId(Policy.Agent parameter)
        {
            IEnumerable<Policy.Agent> result;
            IEnumerable<SP_GET_EN_AGENT_BY_UNIQUE_Result> temp;

            temp = globalModel.SP_GET_EN_AGENT_BY_UNIQUE(
                    parameter.CorpId,
                    parameter.AgentId
                  );

            result = temp
                .Select(pq => new Policy.Agent
                {
                    CorpId = pq.Corp_Id,
                    AgentId = pq.Agent_Id
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.LogResult> InsertLog(Policy.LogParameter parameter)
        {
            IEnumerable<Policy.LogResult> result;
            IEnumerable<SP_INSERT_GS_LOG_Result> temp;

            temp = globalModel.SP_INSERT_GS_LOG(
                    parameter.LogTypeId,
                    parameter.CorpId,
                    parameter.CompanyId,
                    parameter.ProjectId,
                    parameter.Identifier,
                    parameter.LogValue
                );

            result = temp
                .Select(lr => new Policy.LogResult
                {
                    Code = lr.Code,
                    Message = lr.Message
                })
                .ToArray();

            return
                result;
        }

        public virtual Policy.UQuo UpdatePolicyQuo(Policy.UQuo parameter)
        {
            Policy.UQuo result;
            IEnumerable<SP_UPDATE_FT_QUO_PL_POLICY_GRIDVIEW_Result> temp;

            temp = globalModelExtended.SP_UPDATE_FT_QUO_PL_POLICY_GRIDVIEW(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.BussinessLineType,
                    parameter.BussinessLineId,
                    parameter.CompanyId,
                    parameter.CompanyDesc,
                    parameter.PolicyNo,
                    parameter.BlDesc,
                    parameter.ProductTypeDesc,
                    parameter.GroupDesc,
                    parameter.OfficeDesc,
                    parameter.AgentOfficeId,
                    parameter.AgentId,
                    parameter.DistributionDesc,
                    parameter.PolicyStatusId,
                    parameter.PolicyStatusDesc,
                    parameter.PolicyStatusNameKey,
                    parameter.AgentName,
                    parameter.SupervisorAgentId,
                    parameter.SupervisorAgentName,
                    parameter.ContactId,
                    parameter.FullName,
                    parameter.TipoRiesgoNameKey,
                    parameter.IdContact,
                    parameter.InsuredAmount,
                    parameter.AnnualPremium,
                    parameter.InitialContribution,
                    parameter.QuoDate,
                    parameter.QuoPosDate,
                    parameter.EffectiveDate,
                    parameter.ExpirationDate,
                    parameter.InspectionQuoDate,
                    parameter.DeclinedQuoDate,
                    parameter.DeclinedQuoReason,
                    parameter.MissingDocumentQuoReason,
                    parameter.WorkMinute,
                    parameter.SubscriptionMinute,
                    parameter.InspectionMinute,
                    parameter.DocumentMissing,
                    parameter.SubscriberAgentId,
                    parameter.SubscriberName,
                    parameter.InspectorAgentId,
                    parameter.InspectorName,
                    parameter.Days,
                    parameter.IsExpiring,
                    parameter.IsExpired,
                    parameter.PolicyNoTemp,
                    parameter.DiscountAmount,
                    parameter.HasDiscount,
                    parameter.HasSurcharge,
                    parameter.HasEndorsement,
                    parameter.HasEndorsementClarifying,
                    parameter.VehicleCount,
                    parameter.Tab,
                    parameter.UserId
                  );

            result = temp
                    .Select(d => new Policy.UQuo { })
                    .FirstOrDefault();

            return
                result;
        }

        //Bmarroquin 25-03-2017 se crea metodo GetNewCotizacionNumber
        public virtual string GetNewCotizacionNumber(int CountryId, string ProductCode)
        {
            string result;
            ObjectResult<string> temp;
            temp = globalModel.SP_GET_NEW_COTIZACION_NUMBER(ProductCode, CountryId);

            result = temp != null ? temp.FirstOrDefault() : "";
            return result;
        }

        public virtual IEnumerable<Policy.ConditionForSysflexIL> GetConditionIL(string QuotationNumber, long EntityId)
        {
            IEnumerable<Policy.ConditionForSysflexIL> result;
            IEnumerable<Get_condiciones_For_Sysflex_Result> temp;

            temp = globalModel.Get_condiciones_For_Sysflex(QuotationNumber, EntityId);


            result = temp
                .Select(lr => new Policy.ConditionForSysflexIL
                {
                    ramo = lr.ramo,
                    subramo = lr.subramo,
                    descripcion = lr.descripcion,
                    valor = lr.valor,
                    secuenciacondicion = lr.secuenciacondicion,
                    Decimales = lr.Decimales,
                    esnumero = lr.esnumero
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.Facultative.Contract> GetFacultativeContract(Policy.Facultative.Key parameters)
        {
            IEnumerable<Policy.Facultative.Contract> result;
            IEnumerable<SP_GET_PL_POLICY_FACULTATIVE_CONTRACT_Result> temp;

            temp = globalModel.SP_GET_PL_POLICY_FACULTATIVE_CONTRACT(
                    parameters.CorpId,
                    parameters.RegionId,
                    parameters.CountryId,
                    parameters.DomesticregId,
                    parameters.StateProvId,
                    parameters.CityId,
                    parameters.OfficeId,
                    parameters.CaseSeqNo,
                    parameters.HistSeqNo,
                    parameters.UniqueId
                );

            result = temp
                .Select(fc => new Policy.Facultative.Contract
                {
                    CorpId = fc.Corp_Id,
                    RegionId = fc.Region_Id,
                    CountryId = fc.Country_Id,
                    DomesticregId = fc.Domesticreg_Id,
                    StateProvId = fc.State_Prov_Id,
                    CityId = fc.City_Id,
                    OfficeId = fc.Office_Id,
                    CaseSeqNo = fc.Case_Seq_No,
                    HistSeqNo = fc.Hist_Seq_No,
                    UniqueId = fc.Unique_Id,
                    ContractId = fc.Contract_Id,
                    ContractUniqueId = fc.Contract_Unique_Id,
                    CompanyFacultativeId = fc.Company_Facultative_Id,
                    CompanyFacultativeDesc = fc.Company_Facultative_Desc,
                    CompanyFacultativeNameKey = fc.Company_Facultative_NameKey,
                    ContractName = fc.Contract_Name,
                    ContractAmount = fc.Contract_Amount,
                    ContractCommissionPercentage = fc.Contract_Commission_Percentage,
                    PaymentDate = fc.PaymentDate
                })
                .ToArray();

            return
                result;
        }

        public virtual DataTable GetFacultativeContractCoverage(Policy.Facultative.Key parameters)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            SqlConnection connection;

            query = "[Policy].[SP_GET_PL_POLICY_FACULTATIVE_CONTRACT_COVERAGE]";
            connection = new SqlConnection(base.globalconexionStringForAdo);
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Corp_Id", parameters.CorpId);
            command.Parameters.AddWithValue("@Region_Id", parameters.RegionId);
            command.Parameters.AddWithValue("@Country_Id", parameters.CountryId);
            command.Parameters.AddWithValue("@Domesticreg_Id", parameters.DomesticregId);
            command.Parameters.AddWithValue("@State_Prov_Id", parameters.StateProvId);
            command.Parameters.AddWithValue("@City_Id", parameters.CityId);
            command.Parameters.AddWithValue("@Office_Id", parameters.OfficeId);
            command.Parameters.AddWithValue("@Case_Seq_No", parameters.CaseSeqNo);
            command.Parameters.AddWithValue("@Hist_Seq_No", parameters.HistSeqNo);
            command.Parameters.AddWithValue("@Unique_Id", parameters.UniqueId);
            command.CommandType = CommandType.StoredProcedure;
            dT = new DataTable();

            try
            {
                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dT);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return
                dT;
        }


        public virtual IEnumerable<Policy.Facultative.Contract2> GetFacultativeContractCoverage2(Policy.Facultative.Key parameters)
        {

            IEnumerable<Policy.Facultative.Contract2> result;
            IEnumerable<SP_GET_PL_POLICY_FACULTATIVE_CONTRACT_COVERAGE2_Result> temp;

            temp = globalModel.SP_GET_PL_POLICY_FACULTATIVE_CONTRACT_COVERAGE2(
                    parameters.CorpId,
                    parameters.RegionId,
                    parameters.CountryId,
                    parameters.DomesticregId,
                    parameters.StateProvId,
                    parameters.CityId,
                    parameters.OfficeId,
                    parameters.CaseSeqNo,
                    parameters.HistSeqNo,
                    parameters.UniqueId
                );

            result = temp
                .Select(fc => new Policy.Facultative.Contract2
                {
                    CorpId = fc.Corp_Id,
                    ContractUniqueId = fc.Contract_Unique_Id,
                    ContractCoverageId = fc.Contract_Coverage_Id,
                    RegionId = fc.Region_Id,
                    CountryId = fc.Country_Id,
                    UniqueId = fc.Unique_Id,
                    BlTypeId = fc.Bl_Type_Id,
                    BlId = fc.Bl_Id,
                    ProductId = fc.Product_Id,
                    VehicleTypeId = fc.Vehicle_Type_Id,
                    GroupId = fc.Group_Id,
                    CoverageTypeId = fc.Coverage_Type_Id,
                    CoverageId = fc.Coverage_Id,
                    ContractCoveragePercentage = fc.Contract_Coverage_Percentage,
                    CompanyFacultativeId = fc.Company_Facultative_Id,
                    ContractName = fc.Contract_Name,
                    ContractAmount = fc.Contract_Amount,
                    ContractCommissionPercentage = fc.Contract_Commission_Percentage,
                    CompanyFacultativeDesc = fc.Company_Facultative_Desc,
                    CompanyFacultativeNameKey = fc.Company_Facultative_NameKey,
                    PaymentDate = fc.PaymentDate,
                    CoverageDesc = fc.Coverage_Desc
                })
                .ToArray();

            return
                result;

        }

        public virtual DataTable SetFacultativeContractCoverage(Policy.Facultative.SetContractCoverage parameters, DataTable dtCoverages)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            SqlConnection connection;
            SqlParameter parameter;

            query = "[Policy].[SP_SET_PL_POLICY_FACULTATIVE_CONTRACT]";
            connection = new SqlConnection(base.globalconexionStringForAdo);
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Corp_Id", parameters.CorpId);
            command.Parameters.AddWithValue("@Region_Id", parameters.RegionId);
            command.Parameters.AddWithValue("@Country_Id", parameters.CountryId);
            command.Parameters.AddWithValue("@Domesticreg_Id", parameters.DomesticregId);
            command.Parameters.AddWithValue("@State_Prov_Id", parameters.StateProvId);
            command.Parameters.AddWithValue("@City_Id", parameters.CityId);
            command.Parameters.AddWithValue("@Office_Id", parameters.OfficeId);
            command.Parameters.AddWithValue("@Case_Seq_No", parameters.CaseSeqNo);
            command.Parameters.AddWithValue("@Hist_Seq_No", parameters.HistSeqNo);
            command.Parameters.AddWithValue("@Unique_Id", parameters.UniqueId);
            command.Parameters.AddWithValue("@UserId", parameters.UserId);
            parameter = command.Parameters.AddWithValue("@ContractCoverages", dtCoverages);
            command.CommandType = CommandType.StoredProcedure;
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.TypeName = "[Policy].[UDDT_PL_POLICY_FACULTATIVE_CONTRACT_COVERAGE]";
            dT = new DataTable();

            try
            {
                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dT);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return
                dT;
        }

        public virtual int DeleteFacultativeContract(int corpId, long contractUniqueId, int userId)
        {
            int temp;

            temp = globalModel.SP_DELETE_PL_POLICY_FACULTATIVE_CONTRACT(corpId, contractUniqueId, userId);

            return
                temp;
        }

        //Add By Jheiron 29-05-2017
        public virtual IEnumerable<Policy.Agent.AgentSupervisor> GetAgentSupervisor(int corpId, int agentID)
        {
            IEnumerable<SP_GET_EN_AGENT_SUPERVISOR_Result> temp = null;
            IEnumerable<Policy.Agent.AgentSupervisor> result;

            temp = globalModel.SP_GET_EN_AGENT_SUPERVISOR(corpId, agentID);

            result = temp
                .Select(agsu => new Policy.Agent.AgentSupervisor
                {
                    CorpId = agsu.Corp_Id,
                    ChainId = agsu.Chain_Id,
                    ChainDetId = agsu.Chain_Det_Id,
                    AgentId = agsu.Agent_Id,
                    OrderId = agsu.Order_Id,
                    ChainLevelId = agsu.Chain_Level_Id,
                    AgentChainStatus = agsu.Agent_Chain_Status,
                    RelationshipToSupervisor = agsu.Relationship_To_Supervisor,
                    SupervisorAgentId = agsu.Supervisor_Agent_Id,
                    SupervisorAgentCode = agsu.Agent_Code,
                    SupervisporFullName = agsu.FullName
                }).ToArray();

            return
                result;
        }

        public IEnumerable<Policy.PolicyContact> GetPolicyByContact(int? ContactId)
        {
            IEnumerable<Policy.PolicyContact> result;
            IEnumerable<SP_GET_FT_QUO_PL_POLICY_BY_CONTACT_Result> temp;
            temp = globalModel.SP_GET_FT_QUO_PL_POLICY_BY_CONTACT(ContactId);
            result = temp.Select(p => new Policy.PolicyContact
            {
                CorpId = p.Corp_Id,
                RegionId = p.Region_Id,
                CountryId = p.Country_Id,
                DomesticregId = p.Domesticreg_Id,
                State_ProvId = p.State_Prov_Id,
                CityId = p.City_Id,
                OfficeId = p.Office_Id,
                CaseSeqNo = p.Case_Seq_No,
                HistSeqNo = p.Hist_Seq_No,
                PolicyNo = p.Policy_No,
                PolicyNoTemp = p.Policy_No_Temp,
                Tab = p.Tab
            }).ToArray();

            return
                  result;
        }

        public virtual int SetReninsuranceFacultative(Policy.ReinsuranceFacultative reinsuranceFac)
        {
            int result;
            IEnumerable<SP_SET_PL_PCY_REINSURANCE_FACULTATIVE_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_PL_PCY_REINSURANCE_FACULTATIVE(
                reinsuranceFac.CorpId,
                reinsuranceFac.RegionId,
                reinsuranceFac.CountryId,
                reinsuranceFac.DomesticregId,
                reinsuranceFac.StateProvId,
                reinsuranceFac.CityId,
                reinsuranceFac.OfficeId,
                reinsuranceFac.CaseSeqNo,
                reinsuranceFac.HistSeqNo,
                reinsuranceFac.RiderTypeId,
                reinsuranceFac.RiderId,
                reinsuranceFac.CoverageTypeDesc,
                reinsuranceFac.BeneficiaryAmount,
                reinsuranceFac.RequestedDate,
                reinsuranceFac.ProcessedDate,
                reinsuranceFac.CompanyRiskAmount,
                reinsuranceFac.ReinsuranceRiskAmount,
                reinsuranceFac.AuthorizedAmount,
                reinsuranceFac.RiskRatingTable,
                reinsuranceFac.RiskRatingAmount,
                reinsuranceFac.PerThousendRiskAmount,
                reinsuranceFac.FacultativeReinsuranceId,
                reinsuranceFac.FacultativeStatusId,
                reinsuranceFac.ReinsuranceFacultativeStatus,
                reinsuranceFac.UserId,
                reinsuranceFac.SourceID
                );
            return
                result;
        }

        public virtual string getIsValidFacultativeID(int Case_seq_No, string Facultative_Reinsurance_Id)
        {
            string result;
            ObjectResult<int?> temp;
            temp = globalModel.SP_GET_PL_IS_VALID_FACULTATIVEID(Case_seq_No, Facultative_Reinsurance_Id);

            result = temp != null ? temp.FirstOrDefault().ToString() : string.Empty;
            return result;
        }

        public virtual IEnumerable<Policy.QuoGrid.AgentChain> GetAgentChain(Policy.QuoGrid.AgentChain parameter)
        {
            IEnumerable<Policy.QuoGrid.AgentChain> result;
            IEnumerable<FunctionAgentTreeInfoQuo_AgentId_Result> temp;


            temp = globalModelExtended.FunctionAgentTreeInfoQuo_AgentId(
                    parameter.CorpId,
                    parameter.AgentId,
                    parameter.AgentNameId,
                    parameter.BlId
                );

            result = temp.Select(a => new Policy.QuoGrid.AgentChain { AgentId = a.Agent_Id }).ToArray();

            return
                result;
        }

        public virtual decimal? GetQuotFromSysFlex(string PolicyNo)
        {
            decimal? result;
            IEnumerable<decimal?> temp;
            temp = globalModelExtended.SP_GET_QUOTATION_FROM_SYSFLEX(PolicyNo);
            result = temp.FirstOrDefault();

            return
                result;
        }


        public virtual DataTable GetAllCustomerPlanDetailQuo(Policy.QuoGrid.Key parameter)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            SqlConnection connection;
            SqlParameter sqlParameter;

            query = "exec [Policy].[SP_GET_FT_QUO_PL_POLICY_GRIDVIEW_GRID] @CorpId,@Tab,@CompanyId,@DateTo,@DateFrom,@OfficeId,@Agent_Id,@Bl_Id,@User_Id,@Bandeja,@Agent_Chain";
            connection = new SqlConnection(base.globalconexionStringForAdo);
            command = new SqlCommand(query, connection);
            dT = new DataTable();

            command.Parameters.AddWithValue("@CorpId", parameter.CorpId);
            command.Parameters.AddWithValue("@Tab", parameter.Tab);
            command.Parameters.AddWithValue("@CompanyId", parameter.CompanyId.HasValue ? (object)parameter.CompanyId.Value : DBNull.Value);
            command.Parameters.AddWithValue("@DateTo", parameter.DateTo.HasValue ? (object)parameter.DateTo.Value : DBNull.Value);
            command.Parameters.AddWithValue("@DateFrom", parameter.DateFrom.HasValue ? (object)parameter.DateFrom.Value : DBNull.Value);
            command.Parameters.AddWithValue("@OfficeId", parameter.OfficeId.HasValue ? (object)parameter.OfficeId.Value : DBNull.Value);
            command.Parameters.AddWithValue("@Agent_Id", parameter.AgentId.HasValue ? (object)parameter.AgentId.Value : DBNull.Value);
            command.Parameters.AddWithValue("@Bl_Id", parameter.BlId.HasValue ? (object)parameter.BlId.Value : DBNull.Value);
            command.Parameters.AddWithValue("@User_Id", parameter.UserId);
            command.Parameters.AddWithValue("@Bandeja", parameter.Bandeja);

            sqlParameter = command.Parameters.AddWithValue("@Agent_Chain", parameter.AgentChain);
            sqlParameter.SqlDbType = SqlDbType.Structured;
            sqlParameter.TypeName = "[Policy].[UDDT_AGENT_CHAIN]";

            try
            {
                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dT);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return
                dT;
        }

        #region Codigo Comentado
        /*
        public virtual IEnumerable<Policy.QuoGrid> GetAllCustomerPlanDetailQuo(Policy.QuoGrid.Key parameter)
        {
            IEnumerable<Policy.QuoGrid> result;
            IEnumerable<SP_GET_FT_QUO_PL_POLICY_GRIDVIEW_GRID_Result> temp;

            temp = globalModelExtended.SP_GET_FT_QUO_PL_POLICY_GRIDVIEW_GRID(
                    parameter.CorpId,
                    parameter.Tab,
                    parameter.CompanyId,
                    parameter.DateTo,
                    parameter.DateFrom,
                    parameter.OfficeId,
                    parameter.AgentId,
                    parameter.BlId,
                    parameter.UserId,
                    parameter.Bandeja
                );

            result = temp
                .Select(qg => new Policy.QuoGrid
                {
                    CorpId = qg.Corp_Id,
                    RegionId = qg.Region_Id,
                    CountryId = qg.Country_Id,
                    DomesticregId = qg.Domesticreg_Id,
                    StateProvId = qg.State_Prov_Id,
                    CityId = qg.City_Id,
                    OfficeId = qg.Office_Id,
                    CaseSeqNo = qg.Case_Seq_No,
                    HistSeqNo = qg.Hist_Seq_No,
                    BussinessLineType = qg.Bussiness_Line_Type,
                    BussinessLineId = qg.Bussiness_Line_Id,
                    CompanyId = qg.Company_Id,
                    CompanyDesc = qg.Company_Desc,
                    PolicyNo = qg.Policy_No,
                    BlDesc = qg.Bl_Desc,
                    ProductTypeDesc = qg.Product_Type_Desc,
                    ProductSubTypeDesc = qg.Product_Sub_Type_Desc,
                    OfficeDesc = qg.Office_Desc,
                    AgentId = qg.Agent_Id,
                    DistributionDesc = qg.Distribution_Desc,
                    PolicyStatusId = qg.Policy_Status_Id,
                    PolicyStatusDesc = qg.Policy_Status_Desc,
                    AgentName = qg.Agent_Name,
                    ContactId = qg.Contact_Id,
                    FullName = qg.Full_Name,
                    IdContact = qg.Id_Contact,
                    InsuredAmount = qg.Insured_Amount,
                    AnnualPremium = qg.Annual_Premium,
                    InitialContribution = qg.Initial_Contribution,
                    QuoDate = qg.QuoDate,
                    Days = qg.Days,
                    IsExpiring = qg.IsExpiring,
                    IsExpired = qg.IsExpired,
                    InspectionQuoDate = qg.InspectionQuoDate,
                    DeclinedQuoDate = qg.DeclinedQuoDate,
                    DocumentMissing = qg.Document_Missing,
                    SubscriberAgentId = qg.Subscriber_Agent_Id,
                    SubscriberName = qg.Subscriber_Name,
                    InspectorAgentId = qg.Inspector_Agent_Id,
                    InspectorName = qg.Inspector_Name,
                    Tab = qg.Tab,
                    PolicyStatusNameKey = qg.Policy_Status_Name_Key,
                    PolicyNoTemp = qg.Policy_No_Temp,
                    TipoRiesgoNameKey = qg.Tipo_Riesgo_Name_Key,
                    QuoPosDate = qg.QuoPosDate,
                    EffectiveDate = qg.EffectiveDate,
                    ExpirationDate = qg.ExpirationDate,
                    WorkMinute = qg.Work_Minute,
                    SubscriptionMinute = qg.Subscription_Minute,
                    InspectionMinute = qg.Inspection_Minute,
                    HasDiscount = qg.HasDiscount,
                    MakeDiscount = qg.MakeDiscount,
                    HasSurcharge = qg.HasSurcharge,
                    DiscountAmount = qg.DiscountAmount,
                    SupervisorAgentId = qg.Supervisor_Agent_Id,
                    SupervisorAgentName = qg.Supervisor_Agent_Name,
                    DeclinedQuoReason = qg.DeclinedQuoReason,
                    MissingDocumentQuoReason = qg.MissingDocumentQuoReason,
                    ConfirmationCallerAgentId = qg.ConfirmationCaller_Agent_Id,
                    ConfirmationCallerName = qg.ConfirmationCaller_Name,
                    PolicyExpirationDate = qg.PolicyExpirationDate,
                    QuoCreateUserName = qg.QuoCreateUserName,
                    PolicyNoMain = qg.Policy_No_Main,
                    NeedInspection = qg.Need_Inspection,
                    HasFacultative = qg.HasFacultative
                })
                .ToArray();

            return
                result;
        }
         * */
        #endregion

        public virtual IEnumerable<Policy.QuoGrid> GetAllPuntoVentaQuo(Policy.QuoGrid.Key parameter)
        {
            IEnumerable<Policy.QuoGrid> result;
            IEnumerable<SP_GET_FT_QUO_PL_POLICY_GRIDVIEW_GRID_PV_Result> temp;

            temp = globalModelExtended.SP_GET_FT_QUO_PL_POLICY_GRIDVIEW_GRID_PV(
                    parameter.CorpId,
                    parameter.Tab,
                    parameter.CompanyId,
                    parameter.DateTo,
                    parameter.DateFrom,
                    parameter.OfficeId,
                    parameter.AgentId,
                    parameter.BlId,
                    parameter.UserId,
                    parameter.Bandeja
                );

            result = temp
                .Select(qg => new Policy.QuoGrid
                {
                    CorpId = qg.Corp_Id,
                    RegionId = qg.Region_Id,
                    CountryId = qg.Country_Id,
                    DomesticregId = qg.Domesticreg_Id,
                    StateProvId = qg.State_Prov_Id,
                    CityId = qg.City_Id,
                    OfficeId = qg.Office_Id.ConvertToNoNullable(),
                    CaseSeqNo = qg.Case_Seq_No,
                    HistSeqNo = qg.Hist_Seq_No,
                    BussinessLineType = qg.Bussiness_Line_Type,
                    BussinessLineId = qg.Bussiness_Line_Id,
                    CompanyId = qg.Company_Id,
                    CompanyDesc = qg.Company_Desc,
                    PolicyNo = qg.Policy_No,
                    BlDesc = qg.Bl_Desc,
                    ProductTypeDesc = qg.Product_Type_Desc,
                    OfficeDesc = qg.Office_Desc,
                    AgentId = qg.Agent_Id.ConvertToNoNullable(),
                    DistributionDesc = qg.Distribution_Desc,
                    PolicyStatusId = qg.Policy_Status_Id,
                    PolicyStatusDesc = qg.Policy_Status_Desc,
                    AgentName = qg.Agent_Name,
                    ContactId = qg.Contact_Id,
                    FullName = qg.Full_Name,
                    IdContact = qg.Id_Contact,
                    InsuredAmount = qg.Insured_Amount,
                    AnnualPremium = qg.Annual_Premium,
                    InitialContribution = qg.Initial_Contribution,
                    QuoDate = qg.QuoDate,
                    Days = qg.Days,
                    IsExpiring = qg.IsExpiring.ConvertToNoNullable(),
                    IsExpired = qg.IsExpired.ConvertToNoNullable(),
                    InspectionQuoDate = qg.InspectionQuoDate,
                    DeclinedQuoDate = qg.DeclinedQuoDate,
                    DocumentMissing = qg.Document_Missing,
                    SubscriberAgentId = qg.Subscriber_Agent_Id,
                    SubscriberName = qg.Subscriber_Name,
                    InspectorAgentId = qg.Inspector_Agent_Id,
                    InspectorName = qg.Inspector_Name,
                    Tab = qg.Tab,
                    PolicyStatusNameKey = qg.Policy_Status_Name_Key,
                    PolicyNoTemp = qg.Policy_No_Temp,
                    AgentPhones = qg.Phones
                })
                .ToArray();

            return
                result;
        }

        public virtual DataTable GetAllCustomerPlanDetailCountQuo(Policy.QuoGrid.Key parameter)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            SqlConnection connection;
            SqlParameter sqlParameter;

            query = "exec [Policy].[SP_GET_FT_QUO_PL_POLICY_GRIDVIEW_GRID_COUNT] @CorpId,@Agent_Id,@CompanyId,@DateTo,@DateFrom,@OfficeId,@Bl_Id,@User_Id,@Bandeja,@Agent_Chain";
            connection = new SqlConnection(base.globalconexionStringForAdo);
            command = new SqlCommand(query, connection);
            dT = new DataTable();

            command.Parameters.AddWithValue("@CorpId", parameter.CorpId);
            command.Parameters.AddWithValue("@Agent_Id", parameter.AgentId.HasValue ? (object)parameter.AgentId.Value : DBNull.Value);
            command.Parameters.AddWithValue("@CompanyId", parameter.CompanyId.HasValue ? (object)parameter.CompanyId.Value : DBNull.Value);
            command.Parameters.AddWithValue("@DateTo", parameter.DateTo.HasValue ? (object)parameter.DateTo.Value : DBNull.Value);
            command.Parameters.AddWithValue("@DateFrom", parameter.DateFrom.HasValue ? (object)parameter.DateFrom.Value : DBNull.Value);
            command.Parameters.AddWithValue("@OfficeId", parameter.OfficeId.HasValue ? (object)parameter.OfficeId.Value : DBNull.Value);
            command.Parameters.AddWithValue("@Bl_Id", parameter.BlId.HasValue ? (object)parameter.BlId.Value : DBNull.Value);
            command.Parameters.AddWithValue("@User_Id", parameter.UserId);
            command.Parameters.AddWithValue("@Bandeja", parameter.Bandeja);

            sqlParameter = command.Parameters.AddWithValue("@Agent_Chain", parameter.AgentChain);
            sqlParameter.SqlDbType = SqlDbType.Structured;
            sqlParameter.TypeName = "[Policy].[UDDT_AGENT_CHAIN]";

            try
            {
                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dT);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return
                dT;
            /*IEnumerable<Policy.QuoGrid.Counter> result;
            IEnumerable<SP_GET_FT_QUO_PL_POLICY_GRIDVIEW_GRID_COUNT_Result> temp;

            temp = globalModelExtended.SP_GET_FT_QUO_PL_POLICY_GRIDVIEW_GRID_COUNT(
                    parameter.CorpId,
                    parameter.AgentId,
                    parameter.CompanyId,
                    parameter.DateTo,
                    parameter.DateFrom,
                    parameter.OfficeId,
                    parameter.BlId,
                    parameter.UserId,
                    parameter.Bandeja
                );

            result = temp
                .Select(qg => new Policy.QuoGrid.Counter
                {
                    Count = qg.Count,
                    Tab = qg.Tab
                })
                .ToArray();

            return
                result;*/
        }

        public virtual int SetVehicleInsuredInspectionStatus(Policy.VehicleInsured.InspectionV parameter)
        {
            int result;
            IEnumerable<SP_UPDATE_PL_POLICY_VEHICLE_REVIEW_STATUS_Result> temp;

            temp = globalModelExtended.SP_UPDATE_PL_POLICY_VEHICLE_REVIEW_STATUS(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticRegId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.InsuredVehicleId,
                    parameter.ReviewStatusId,
                    parameter.ReviewStatus,
                    parameter.UserId
                  );

            result = temp
                    .Select(d => d.Result)
                    .FirstOrDefault();

            return
                result;
        }

        public virtual int SetVehicleInsuredInspectionAddress(Policy.VehicleInsured.InspectionV parameter)
        {
            int result;
            IEnumerable<SP_UPDATE_PL_POLICY_VEHICLE_REVIEW_INSPECTION_ADDRESS_Result> temp;

            temp = globalModelExtended.SP_UPDATE_PL_POLICY_VEHICLE_REVIEW_INSPECTION_ADDRESS(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticRegId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.InsuredVehicleId,
                    parameter.InspectionAddress,
                    parameter.UserId
                  );

            result = temp
                    .Select(d => d.Result)
                    .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Policy.TabRol> GetAllTabsByRole(int UsrId)
        {
            IEnumerable<Policy.TabRol> result;
            IEnumerable<SP_GET_FT_QUO_TAB_ROL_VISIBLE_Result> temp;

            temp = globalModelExtended.SP_GET_FT_QUO_TAB_ROL_VISIBLE(
                    UsrId
                );

            result = temp
                .Select(tr => new Policy.TabRol
                {
                    TabName = tr.Tab_Rol_Visible_Desc,
                    Visible = tr.Visible,
                    TabGroupDesc = tr.Tab_Group_Desc
                })
                .ToArray();

            return
                result;
        }

        public virtual int BlackListMember(Policy.BlackListMember.Parameter parameter)
        {
            globalModelExtended.SP_SET_PL_POLICY_BLACKLIST_MEMBER(
                 parameter.corpId,
                 parameter.regionId,
                 parameter.countryId,
                 parameter.domesticregId,
                 parameter.stateProvId,
                 parameter.cityId,
                 parameter.officeId,
                 parameter.caseSeqNo,
                 parameter.histSeqNo,
                 parameter.blacklistMember,
                 parameter.userId
                );

            return
                -1;
        }

        public virtual int SetBlakList(Policy.BlackList.Parameter parameter)
        {
            globalModelExtended.SP_SET_PL_POLICY_BLACKLIST(
                parameter.corpId,
                parameter.regionId,
                parameter.countryId,
                parameter.domesticregId,
                parameter.stateProvId,
                parameter.cityId,
                parameter.officeId,
                parameter.caseSeqNo,
                parameter.histSeqNo,
                parameter.blacklistCheck,
                parameter.userId
           );

            return
                -1;
        }

        public virtual Policy.AgentInfo GetAgentInfo(int? CorpId, int? AgentId)
        {
            Policy.AgentInfo result;
            IEnumerable<SP_GET_AGENT_INFO_Result> temp;
            temp = globalModelExtended.SP_GET_AGENT_INFO(CorpId, AgentId);

            result = temp.Select(a => new Policy.AgentInfo
            {
                AgentId = a.Agent_Id,
                IdentityTypeId = a.Identity_Type_Id,
                ID = a.ID,
                AgentCode = a.Agent_Code,
                NameId = a.Name_Id,
                FirstName = a.First_Name,
                MiddleName = a.Middle_Name,
                FirstLastname = a.First_Lastname,
                SecondLastname = a.Second_Lastname,
                Nickname = a.Nickname,
                Dob = a.Dob,
                Gender = a.Gender,
                MaritalStatId = a.Marital_Stat_Id,
                ResidenceCountryId = a.Residence_Country_Id,
                BirthCountryId = a.Birth_Country_Id,
                CitizenshipCountryId = a.Citizenship_Country_Id,
                DirectoryId = a.Directory_Id,
                AgentTypeDesc = a.Agent_Type_Desc,
                MaritalStatusDesc = a.Marital_Status_Desc,
                CountryOfBirthDesc = a.Country_Of_Birth_Desc,
                KcoUniqueId = a.Kco_Unique_Id
            })
            .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Policy.AgentInfo.Directory> GetAgentDirectoryInfo(int? DirectoryId)
        {
            IEnumerable<Policy.AgentInfo.Directory> result;
            IEnumerable<SP_GET_AGENT_DIRECTORY_Result> temp;
            temp = globalModelExtended.SP_GET_AGENT_DIRECTORY(DirectoryId);

            result = temp.Select(a => new Policy.AgentInfo.Directory
            {
                CorpId = a.Corp_Id,
                DirectoryId = a.Directory_Id,
                DirDetailId = a.Dir_Detail_Id,
                CommTypeId = a.Comm_Type_Id,
                CommTypeDesc = a.Comm_Type_Desc,
                DirectoryTypeId = a.Directory_Type_Id,
                DirTypeDesc = a.Dir_Type_Desc,
                PhoneTypeId = a.Phone_Type_Id,
                PhoneTypeDesc = a.Phone_Type_Desc,
                PhonePrefix = a.Phone_Prefix,
                AreaCode = a.Area_Code,
                PhoneNumber = a.Phone_Number,
                PhoneExt = a.Phone_Ext,
                Address = a.Address,
                RegionId = a.Region_Id.GetValueOrDefault(),
                RegionDesc = a.Region_Desc,
                CountryId = a.Country_Id.GetValueOrDefault(),
                CountryDesc = a.Country_Desc,
                DomesticRegionId = a.Domestic_Region_Id.GetValueOrDefault(),
                DomesticregDesc = a.Domesticreg_Desc,
                StateProvId = a.State_Prov_Id.GetValueOrDefault(),
                StateProvDesc = a.State_Prov_Desc,
                CityId = a.City_Id.GetValueOrDefault(),
                CityDesc = a.City_Desc,
                OfficeDesc = a.Office_Desc,
                AddressNo = a.Address_No,
                BlgdNumber = a.Blgd_Number,
                Floor = a.Floor,
                Door = a.Door,
                NearToReference = a.Near_To_Reference,
                ZipCode = a.Zip_Code,
                isPrimary = a.isPrimary,
                DirectoryOwnerType = a.Directory_Owner_Type.GetValueOrDefault()
            });

            return
                result;
        }

        public virtual Policy.AgentInfo.Agent SetAgent(Policy.AgentInfo.Agent.parameter parameter)
        {
            Policy.AgentInfo.Agent result;
            IEnumerable<SP_SET_AGENT_Result> temp;
            temp = globalModelExtended.SP_SET_AGENT
                (
                    parameter.corpId,
                    parameter.agentId,
                    parameter.agentTypeId,
                    parameter.identityTypeId,
                    parameter.agentSubTypeId,
                    parameter.iD,
                    parameter.agentCode,
                    parameter.nameId,
                    parameter.firstName,
                    parameter.middleName,
                    parameter.firstLastname,
                    parameter.secondLastname,
                    parameter.nickname,
                    parameter.dob,
                    parameter.paymentTypeId,
                    parameter.abaNumber,
                    parameter.bankAccountNumber,
                    parameter.allocation,
                    parameter.allocationTypeId,
                    parameter.gender,
                    parameter.maritalStatId,
                    parameter.title,
                    parameter.residenceCountryId,
                    parameter.birthCountryId,
                    parameter.citizenshipCountryId,
                    parameter.directoryId,
                    parameter.activeDate,
                    parameter.inactiveDate,
                    parameter.kcoUniqueId,
                    parameter.userId
                );

            result = temp.Select(a => new Policy.AgentInfo.Agent
            {
                Action = a.Action,
                CorpId = a.Corp_Id,
                AgentId = a.Agent_Id
            })
            .FirstOrDefault();

            return
                 result;
        }

        public virtual int SetAgentUniqueID(int? CorpId, int? AgentId, string UniqueId)
        {

            globalModelExtended.SP_SET_AGENT_Kco_Unique_Id(CorpId, AgentId, UniqueId);

            return
                -1;
        }

        public virtual IEnumerable<Policy.Vehicle.AccidentRate> GetTbAccidentRateByMakeAndModel(string Make, string Model)
        {
            IEnumerable<Policy.Vehicle.AccidentRate> result;
            IEnumerable<GET_TB_ACCIDENT_RATE_BY_MAKE_AND_MODEL_Result> temp;
            temp = globalModelExtended.GET_TB_ACCIDENT_RATE_BY_MAKE_AND_MODEL(Make, Model);

            result = temp.Select(a => new Policy.Vehicle.AccidentRate
            {
                CARBRANDNAME = a.CAR_BRAND_NAME,
                CARMODELNAME = a.CAR_MODEL_NAME,
                POLICYITEMCARYEAR = a.POLICY_ITEM_CAR_YEAR.GetValueOrDefault(),
                SINIESTRALIDAD = a.SINIESTRALIDAD,
                FRECUENCIA = a.FRECUENCIA,
                CANTIDADVEHICULOS = a.CANTIDADVEHICULOS,
                LIQUIDACION = a.LIQUIDACION
            });

            return
                result;
        }

        public virtual int SetCustomerSing(Nullable<int> corpId, Nullable<int> regionId, Nullable<int> countryId, Nullable<int> domesticRegId, Nullable<int> stateProvId, Nullable<int> cityId, Nullable<int> officeId, Nullable<int> caseSeqNo, Nullable<int> histSeqNo, string dataSign)
        {
            globalModelExtended.SP_SET_CUSTOMER_SIGN(corpId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, dataSign);
            return
               -1;
        }

        public virtual string GetCustomerSing(Nullable<int> corpId, Nullable<int> regionId, Nullable<int> countryId, Nullable<int> domesticRegId, Nullable<int> stateProvId, Nullable<int> cityId, Nullable<int> officeId, Nullable<int> caseSeqNo, Nullable<int> histSeqNo)
        {
            IEnumerable<string> temp;
            temp = globalModelExtended.SP_GET_CUSTOMER_SIGN(corpId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);

            return
                  temp.FirstOrDefault();
        }


        #region BackgroundCheck Underwriting

        public IEnumerable<Policy.BackGroundCheckLink> Bg_Get_Match_Links(Policy.BackGroundCheckLink param)
        {
            IEnumerable<Policy.BackGroundCheckLink> result;
            IEnumerable<SP_GET_BG_MATCH_LINKS_Result> temp;

            temp = globalModelExtended.SP_GET_BG_MATCH_LINKS(
                param.Corp_Id,
                param.Region_Id,
                param.Country_Id,
                param.Domesticreg_Id,
                param.State_Prov_Id,
                param.City_Id,
                param.Office_Id,
                param.Case_Seq_No,
                param.Hist_Seq_No,
                param.Contact_Id);

            result = temp.Select(p => new Policy.BackGroundCheckLink
            {
                Corp_Id = p.CORP_ID,
                Region_Id = p.REGION_ID,
                Country_Id = p.COUNTRY_ID,
                Domesticreg_Id = p.DOMESTICREG_ID,
                State_Prov_Id = p.STATE_PROV_ID,
                City_Id = p.CITY_ID,
                Office_Id = p.OFFICE_ID,
                Case_Seq_No = p.CASE_SEQ_NO,
                Hist_Seq_No = p.HIST_SEQ_NO,
                Contact_Id = p.CONTACT_ID,
                Link_Id = p.LINK_ID,
                Link_Url = p.LINK_URL,
                Link_Status = p.LINK_STATUS,
                Matched = p.Matched,
                UserName = p.UserName,
                Match_Status_Img = p.MATCH_STATUS_IMG,
                Create_Date = p.CREATE_DATE

            }).ToArray();

            return
                  result;
        }

        public virtual int Bg_Set_Match_Links(Policy.BackGroundCheckLink param)
        {
            int temp;

            temp = globalModelExtended.SP_SET_BG_MATCH_LINKS(
                param.Operation,
                param.Corp_Id,
                param.Region_Id,
                param.Country_Id,
                param.Domesticreg_Id,
                param.State_Prov_Id,
                param.City_Id,
                param.Office_Id,
                param.Case_Seq_No,
                param.Hist_Seq_No,
                param.Contact_Id,
                param.Link_Id,
                param.Link_Url,
                param.Link_Status,
                param.Link_Image,
                param.Matched,
                param.userid
                );

            return
                temp;
        }


        public Policy.CouponInfo GetCouponInfo(string policyNo)
        {
            Policy.CouponInfo result;
            IEnumerable<SP_GET_COUPON_INFO_Result> temp;
            temp = globalModelExtended.SP_GET_COUPON_INFO(policyNo);

            result = temp.Select(a => new Policy.CouponInfo
            {
                CouponCode = a.CouponCode,
                CouponPercentageDiscount = a.CouponPercentageDiscount,
                CouponProspectId = a.CouponProspectId,
                Policy_Number = a.Policy_Number
            })
            .FirstOrDefault();

            return
                result;
        }

        #endregion
    }
}