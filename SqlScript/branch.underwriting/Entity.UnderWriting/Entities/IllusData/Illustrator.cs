﻿using System;

namespace Entity.UnderWriting.IllusData
{
    public class Illustrator
    {
        public class CustomerDetail
        {
            public long? CustomerNo { get; set; }
            public string ClientId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string LastName2 { get; set; }
            public string MiddleName { get; set; }
            public DateTime? BirthDate { get; set; }
            public string Age { get; set; }
            public string GenderCode { get; set; }
            public string MaritalStatusCode { get; set; }
            public string Smoker { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string Dtate { get; set; }
            public int? CountryNo { get; set; }
            public string ZipCode { get; set; }
            public string Emailid1 { get; set; }
            public string Emailid2 { get; set; }
            public string BusAddress1 { get; set; }
            public string BusAddress2 { get; set; }
            public string BusZipCode { get; set; }
            public string BusCity { get; set; }
            public int? BusCountryNo { get; set; }
            public string BusState { get; set; }
            public string AgentCode { get; set; }
            public string Notes { get; set; }
            public string ReferralTypeCode { get; set; }
            public string BusAddress3 { get; set; }
            public string Address3 { get; set; }
            public int? UserId { get; set; }
            public DateTime? DateCreated { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? UpdatedBy { get; set; }
            public DateTime? Appointment { get; set; }
            public int? IllusCount { get; set; }
            public long? RCustomerNo { get; set; }
            public string CustomerStatusCode { get; set; }
            public int? Calls { get; set; }
            public int? Visits { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
            public string RefName { get; set; }
            public string RefLastName { get; set; }
            public string RefEmail { get; set; }
            public string Ext { get; set; }
            public string PhoneNo { get; set; }
            public int? ResCountryNo { get; set; }
            public long? RefCustomerNo { get; set; }
            public string IsDeleted { get; set; }
            public string IdNo { get; set; }
            public string GenderName { get; set; }
            public string MaritalStatus { get; set; }
            public string CountryName { get; set; }
            public string ReferralType { get; set; }
            public string CustomerStatus { get; set; }
            public string CompanyId { get; set; }
            public int UserIdSystem { get; set; }
        }

        public class CustomerPlanDetail
        {
            public long? CustomerPlanNo { get; set; }
            public DateTime? PlanDate { get; set; }
            public string ProductCode { get; set; }
            public string PClass { get; set; }
            public long? CustomerNo { get; set; }
            public string FrequencyTypeCode { get; set; }
            public int? Frequency { get; set; }
            public decimal InsuredAmount { get; set; }
            public decimal PremiumAmount { get; set; }
            public decimal AnnualizedPremium { get; set; }
            public DateTime? EndDate { get; set; }
            public decimal ProjectedPremium { get; set; }
            public decimal InitialContribution { get; set; }
            public decimal InitialCommission { get; set; }
            public decimal TargetPremium { get; set; }
            public string InsuranceLevelCode { get; set; }
            public string CalculateTypeCode { get; set; }
            public string ContributionTypeCode { get; set; }
            public string InvestmentProfileCode { get; set; }
            public decimal InvestmentProfilePercent { get; set; }
            public int ActivityRiskTypeNo { get; set; }
            public int HealthRiskTypeNo { get; set; }
            public int ContributionPeriod { get; set; }
            public string FinancialGoal { get; set; }
            public int FinancialGoalAge { get; set; }
            public decimal FinancialGoalAmount { get; set; }
            public string CurrencyCode { get; set; }
            public string RiderAdb { get; set; }
            public string RiderTerm { get; set; }
            public string RiderOir { get; set; }
            public int CountryNo { get; set; }
            public string PlanTypeCode { get; set; }
            public DateTime DateCreated { get; set; }
            public int CreatedBy { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int UpdatedBy { get; set; }
            public decimal RiderAdbAmount { get; set; }
            public decimal RiderTermAmount { get; set; }
            public int RiderTermUntilAge { get; set; }
            public string RiderCi { get; set; }
            public decimal RiderCiAmount { get; set; }
            public string RiderAcdb { get; set; }
            public long? RCustomerPlanNo { get; set; }
            public long? IllustrationNo { get; set; }
            public string DataEntryTypeCode { get; set; }
            public long? PlanCode { get; set; }
            public int UserId { get; set; }
            public int ContributionUntilAge { get; set; }
            public decimal OpeningBalance { get; set; }
            public decimal MinimumPremium { get; set; }
            public int? OpeningYear { get; set; }
            public DateTime? PlanEffectiveDate { get; set; }
            public string IllustrationVerified { get; set; }
            public decimal RiderAdbCost { get; set; }
            public decimal RiderAcdbCost { get; set; }
            public decimal RiderTermCost { get; set; }
            public decimal RiderCiCost { get; set; }
            public decimal TermPeriod { get; set; }
            public decimal RetirementPeriod { get; set; }
            public decimal DefermentPeriod { get; set; }
            public decimal AnnuityAmount { get; set; }
            public int CompanyNo { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
            public string OtherPlans { get; set; }
            public string DispIllustrationNo { get; set; }
            public decimal? BenefitAmount { get; set; }
            public string DispPlanCode { get; set; }
            public string TermContributionTypeCode { get; set; }
            public string IsSpecial { get; set; }
            public string ChangeType { get; set; }
            public int? BoUpdatedBy { get; set; }
            public int? BoLastUpdatedBy { get; set; }
            public DateTime? NewStatusDate { get; set; }
            public string IsoPeningBalance { get; set; }
            public string IsApproved { get; set; }
            public string PolicyStatusCode { get; set; }
            public string IsPolicyChangesApproved { get; set; }
            public string IsDeleted { get; set; }
            public string StudentName { get; set; }
            public int? StudentAge { get; set; }
            public string IsCustomerOwner { get; set; }
            public long? OwnerCustomerNo { get; set; }
            public string IllustrationStatusCode { get; set; }
            public string PlanGroupCode { get; set; }
            public string Product { get; set; }
            public string ActivityRiskType { get; set; }
            public string Currency { get; set; }
            public string FrequencyType { get; set; }
            public string InsuranceLevel { get; set; }
            public string CalculateType { get; set; }
            public string InvestmentProfile { get; set; }
            public string HealthRiskType { get; set; }
            public string CountryName { get; set; }
            public string PlanType { get; set; }
            public string ContributionTypeDescCode { get; set; }
            public string ContributionTypeDescTerm { get; set; }
            public string IllustrationStatus { get; set; }
            public string InsuredName { get; set; }
            public string PlanGroup { get; set; }

            public string Familiar { get; set; }
            public string Repatriacion { get; set; }
            public string SepulturaLote { get; set; }
            public string CompanyId { get; set; }

            public string OfficeDesc { get; set; }
            public string Agent { get; set; }

            public string CompanyDesc { get; set; }
            public string Identification { get; set; }

            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }

            public long? InsuredId { get; set; }

            public int UserIdSystem { get; set; }

            public int CountryId { get; set; }
            public string Channel { get; set; }
            public bool Priority { get; set; }
            public int? PendingDocumentsNo { get; set; }
            public int? AgentId { get; set; }
            public int? AssignedSubscriberId { get; set; }
            public string AssignedSubscriber { get; set; }

            public string PolicyNoTemp { get; set; }
            public decimal? SpecialPayment { get; set; }
            public int? ProviderTypeId { get; set; }
            public int? ProviderId { get; set; }
            public decimal? FinancingRate { get; set; }
            public string DestinyFund { get; set; }
            public int? ContributionPeriodMonth { get; set; }
            public bool? HaveSpecialPayment { get; set; }

			public double FractionSurcharge { get; set; }
            public double NetAnnualPremium { get; set; } //Bmarroquin 04-05-2017
        }

        public class CustomerEmail
        {
            public long? CustomerNo { get; set; }
            public string EmailTypeCode { get; set; }
            public string EmailId { get; set; }
            public string Additional { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
            public int UserIdSystem { get; set; }
        }

        public class CustomerPhone
        {
            public long? CustomerNo { get; set; }
            public string PhoneTypeCode { get; set; }
            public string IntCode { get; set; }
            public string AreaCode { get; set; }
            public string PhoneNo { get; set; }
            public string Ext { get; set; }
            public string Additional { get; set; }
            public long? RCustomerPhoneNo { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
            public string SpecialPhoneType { get; set; }
            public int UserIdSystem { get; set; }
        }

        public class CustomerOccupation
        {
            public long? CustomerOccupationNo { get; set; }
            public long? CustomerNo { get; set; }
            public string CompanyName { get; set; }
            public string BusinessType { get; set; }
            public int? WorkYears { get; set; }
            public int? WorkMonths { get; set; }
            public string OccupationTypeCode { get; set; }
            public string ProfessionTypeCode { get; set; }
            public string Tasks { get; set; }
            public decimal? AnnualFamilyIncome { get; set; }
            public long? RCustomerOccupationNo { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
            public int UserIdSystem { get; set; }

        }

        public class CustomerPlanIdentification
        {
            public string InsuredTypeCode { get; set; }
            public string IdentificationTypeCode { get; set; }
            public string IdentificationCode { get; set; }
            public DateTime? ExpiryDate { get; set; }
            public int? CountryNo { get; set; }
            public long? RCustomerIdentificationNo { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
            public long CustomerNo { get; set; }
            public int UserIdSystem { get; set; }
        }

        public class Signature
        {
            public long? CustomerPlanNo { get; set; }
            public string CustomerSign1 { get; set; }
            public string AgentSign1 { get; set; }
            public string CustomerSign2 { get; set; }
            public string AgentSign2 { get; set; }
            public string CustomerSign3 { get; set; }
            public string AgentSign3 { get; set; }
            public string CustomerSign4 { get; set; }
            public string AgentSign4 { get; set; }
            public string CustomerSign5 { get; set; }
            public string AgentSign5 { get; set; }
            public int? Sign1PageNo { get; set; }
            public int? Sign2PageNo { get; set; }
            public int? Sign3PageNo { get; set; }
            public int? Sign4PageNo { get; set; }
            public int? Sign5PageNo { get; set; }
            public string PdfFileName { get; set; }
            public string IsPdfLocked { get; set; }
            public DateTime? DateSynced { get; set; }
        }

        public class User
        {
            public long? UserId { get; set; }
            public string UserTypeCode { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public int RoleNo { get; set; }
            public string EmailSent { get; set; }
            public string EmailId { get; set; }
            public string Active { get; set; }
            public DateTime? DateCreated { get; set; }
            public DateTime? UpdatePassDate { get; set; }
            public string CanLogIn { get; set; }
            public DateTime? LastLogInDate { get; set; }
            public DateTime? DateSynced { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? SupervisorId { get; set; }
            public string Designation { get; set; }
            public int? HierarchyCode { get; set; }
            public string IsAdmin { get; set; }
            public int? UserGroupNo { get; set; }
            public string NameId { get; set; }
            public Guid? MsreplTranVersion { get; set; }
            public bool? IsBlocked { get; set; }
            public string EncryptPassword { get; set; }
        }

        public class CustomerPlanBeneficiary
        {
            public long? CustomerPlanNo { get; set; }
            public string InsuredTypeCode { get; set; }
            public string BeneficiaryTypeCode { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public DateTime? Dob { get; set; }
            public string RelationshipTypeCode { get; set; }
            public decimal? PercentValue { get; set; }
            public long CustomerPlanbeneficiaryNo { get; set; }
            public DateTime? DateCreated { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? UpdatedBy { get; set; }
            public long? RCustomerPlanBeneficiaryNo { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
            public string RelationshipType { get; set; }
            public string SecondLastName { get; set; } //lgonzalez 11-02-17
        }

        public class CustomerPlanPartnerInsurance
        {
            public long CustomerPlanNo { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public decimal InsuredAmount { get; set; }
            public int? Age { get; set; }
            public string GenderCode { get; set; }
            public string MaritalStatusCode { get; set; }
            public string Smoker { get; set; }
            public int? ActivityRiskTypeNo { get; set; }
            public int? HealthRiskTypeNo { get; set; }
            public string OtherPlans { get; set; }
            public DateTime? DateCreated { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? UpdatedBy { get; set; }
            public decimal RideroirAmount { get; set; }
            public int UntilAge { get; set; }
            public long? RCustomerPlanPartnerInsuranceNo { get; set; }
            public decimal RideroirCost { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
            public string RelationshipTypeCode { get; set; }
            public long? CustomerNo { get; set; }
            public string ContributionTypeCode { get; set; }
            public string LastName2 { get; set; }
            public DateTime? BirthDate { get; set; }

        }

        public class CustomerPlanOtherInsurance
        {
            public long? CustomerPlanNo { get; set; }
            public string InsuredTypeCode { get; set; }
            public string ProductCode { get; set; }
            public decimal? InsuredAmount { get; set; }
            public decimal? AnnuityAmount { get; set; }
            public int? AnnuityPeriod { get; set; }
            public long CustomerPlanOtherInsuranceNo { get; set; }
            public DateTime? DateCreated { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? UpdatedBy { get; set; }
            public long? RCustomerPlanOtherInsuranceNo { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
            public string Product { get; set; }
        }

        public class CustomerPlanExam
        {
            public string ExamCode { get; set; }
            public DateTime? DateCreated { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? CreatedBy { get; set; }
            public int? UpdatedBy { get; set; }
            public long? CustomerExamNo { get; set; }
            public long? RCustomerExamNo { get; set; }
            public long? CustomerPlanNo { get; set; }
            public string InsuredTypeCode { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
            public string ExamName { get; set; }
        }

        public class CustomerPlanExamCondition
        {
            public int? SNo { get; set; }
            public string ExamCode { get; set; }
            public string ExamName { get; set; }
            public int? MinAge { get; set; }
            public int? MaxAge { get; set; }
            public decimal? MinInsuredAmount { get; set; }
            public decimal? MaxInsuredAmount { get; set; }
            public decimal? Randomness { get; set; }
            public string Active { get; set; }
            public DateTime? DateCreated { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? CreatedBy { get; set; }
            public int? UpdatedBy { get; set; }
            public int ExamConditionNo { get; set; }
            public string MaritalStatusCode { get; set; }
            public string GenderCode { get; set; }
            public string ProductCode { get; set; }
            public DateTime? DateSynced { get; set; }
        }

        public class RuleParameter
        {
            public int RuleParameterValueNo { get; set; }
            public int? RuleParameterNo { get; set; }
            public decimal? RuleParameterValue { get; set; }
            public string ProductCode { get; set; }
            public DateTime? DateCreated { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? CreatedBy { get; set; }
            public int? UpdatedBy { get; set; }
            public DateTime? DateSynced { get; set; }
        }

        public class CustomerPlaNopBal
        {
            public long? CustomerNo { get; set; }
            public long CustomerPlanNo { get; set; }
            public decimal? Currentvalue { get; set; }
            public decimal? PlanYear { get; set; }
            public decimal? TargetAmount { get; set; }
            public decimal? MinimumPremium { get; set; }
            public string FrequencyTypeCode { get; set; }
            public decimal? PeriodicPremium { get; set; }
            public int? NooFunPaidPremiums { get; set; }
            public string InvestmentProfileCode { get; set; }
            public decimal? MonthlyInsuranceCost { get; set; }
            public decimal? ForceAccountValue { get; set; }
            public decimal? OpeningBalance { get; set; }
            public string CalculateTypeObCode { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? DateCreated { get; set; }
            public int? UpdatedBy { get; set; }
            public DateTime? DateUpdated { get; set; }
            public string ProductCode { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
            public decimal? TotalAmountPaid { get; set; }
            public decimal? NoPaymentsRecieved { get; set; }
            public decimal? AmountDue { get; set; }
            public decimal? IllustrationMonth { get; set; }
            public decimal? ForceTarget { get; set; }
            public string IsOverride { get; set; }
            public int? FullPaymentYear { get; set; }
            public decimal? ExpectedPremiums { get; set; }
            public decimal? AdjustedAccountValue { get; set; }
            public decimal? InsuredAmount { get; set; }
        }

        public class CustomerPlanVarPremium
        {
            public long? CustomerPlanNo { get; set; }
            public int? FromYearNo { get; set; }
            public int? ToYearNo { get; set; }
            public decimal? PremiumAmount { get; set; }
            public long CustomerPlanVarPremiumNo { get; set; }
            public DateTime? DateCreated { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? UpdatedBy { get; set; }
            public int? SNo { get; set; }
            public long? RCustomerPlanVarPremiumNo { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
        }

        public class CustomerPlanVarInsured
        {
            public long? CustomerPlanNo { get; set; }
            public int? FromYearNo { get; set; }
            public int? ToYearNo { get; set; }
            public decimal? InsuredAmount { get; set; }
            public long CustomerPlanVarInsuredNo { get; set; }
            public DateTime? DateCreated { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? UpdatedBy { get; set; }
            public int? SNo { get; set; }
            public long? RCustomerPlanVarInsuredNo { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
        }

        public class CustomerPlanLoan
        {
            public long? CustomerPlanNo { get; set; }
            public int? FromYearNo { get; set; }
            public int? ToYearNo { get; set; }
            public decimal? LoanAmount { get; set; }
            public long CustomerPlanVarLoanNo { get; set; }
            public DateTime? DateCreated { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? UpdatedBy { get; set; }
            public int? SNo { get; set; }
            public long? RCustomerPlanLoanNo { get; set; }
            public long? RCustomerPlanLoanRepayNo { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
        }
        public class CustomerPlanVarSurrender
        {
            public long? CustomerPlanNo { get; set; }
            public int? FromYearNo { get; set; }
            public int? ToYearNo { get; set; }
            public decimal? SurrenderAmount { get; set; }
            public long CustomerPlanSurrenderNo { get; set; }
            public DateTime? DateCreated { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? UpdatedBy { get; set; }
            public int? SNo { get; set; }
            public long? RCustomerPlanVarSurrenderNo { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
        }
        public class CustomerPlanLoanRepay
        {
            public long? CustomerPlanNo { get; set; }
            public int? FromYearNo { get; set; }
            public int? ToYearNo { get; set; }
            public decimal? PaymentAmount { get; set; }
            public long CustomerPlanVarPaymentNo { get; set; }
            public DateTime? DateCreated { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? UpdatedBy { get; set; }
            public int? SNo { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
        }
        public class CustomerPlanVarProfile
        {
            public long? CustomerPlanNo { get; set; }
            public int? FromYearNo { get; set; }
            public int? ToYearNo { get; set; }
            public string InvestmentProfileCode { get; set; }
            public long CustomerPlanVarProfileNo { get; set; }
            public DateTime? DateCreated { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? DateUpdated { get; set; }
            public int? UpdatedBy { get; set; }
            public int? SNo { get; set; }
            public long? RCustomerPlanVarProfileDetNo { get; set; }
            public DateTime? DateSynced { get; set; }
            public long? RecordId { get; set; }
        }

        public class InvProfileCompareRates
        {
            public string InvestmentProfileCode { get; set; }
            public string ProductCode { get; set; }
            public decimal? GrowthRate1 { get; set; }
            public decimal? GrowthRate2 { get; set; }
            public string ClassCode { get; set; }
            public DateTime? DateCreated { get; set; }
            public DateTime? DateUpdated { get; set; }
            public DateTime? DateSynced { get; set; }
            public int InvprofileComparerateNo { get; set; }
        }

        public class ProductCancel
        {
            public string ProductCode { get; set; }
            public int? FromContributionPriod { get; set; }
            public int? ToContributionPriod { get; set; }
            public int ProductCancelNo { get; set; }
        }
        public class ProductCancelDetail
        {
            public int? ProductCancelNo { get; set; }
            public int? YearNo { get; set; }
            public decimal? CancelPercent { get; set; }
            public long ProductCancelDetailNo { get; set; }
        }

        public class FrequencyCostDetail
        {
            public string ProductCode { get; set; }
            public string FrequencyTypeCode { get; set; }
            public decimal? FrequencyCost { get; set; }
            public int FrequencyCostNo { get; set; }
        }

        public class CustomerPlanTerm
        {
            public long CustomerPlanTermNo { get; set; }
            public long CustomerPlanNo { get; set; }
            public int TableNo { get; set; }
            public int Age { get; set; }
            public int Year { get; set; }
            public decimal? PrimaBasicCoverage { get; set; }
            public decimal? PremiumExtras { get; set; }
            public decimal? TotalPremium { get; set; }
            public decimal? AccumulatedPremiums { get; set; }
            public decimal? DeathBenefit { get; set; }
        }

        public class CustomerPlanAnnuity
        {
            public long CustomerPlanAnnuityNo { get; set; }
            public long CustomerPlanNo { get; set; }
            public int Age { get; set; }
            public int Year { get; set; }
            public decimal? AccumulatedContributions { get; set; }
            public decimal? DeathBenefit { get; set; }
            public decimal? BenefitExclusion { get; set; }
            public decimal? AccountValue { get; set; }
            public decimal? SurrenderValue { get; set; }
            public decimal? AnnualPartialWithDrawal { get; set; }
        }

        public class CustomerPlanLife
        {
            public long CustomerPlanLifeNo { get; set; }
            public long CustomerPlanNo { get; set; }
            public int Age { get; set; }
            public int Year { get; set; }
            public decimal? Premium { get; set; }
            public decimal? AccountValue1 { get; set; }
            public decimal? SurrenderValue1 { get; set; }
            public decimal? DeathBenefit1 { get; set; }
            public decimal? AccountValue2 { get; set; }
            public decimal? SurrenderValue2 { get; set; }
            public decimal? DeathBenefit2 { get; set; }
            public decimal? AccountValue3 { get; set; }
            public decimal? SurrenderValue3 { get; set; }
            public decimal? DeathBenefit3 { get; set; }
        }

        public class InvestmentsInflacion
        {
            public int Sno { get; set; }
            public int? Years { get; set; }
            public decimal? Pequenas_Acciones { get; set; }
            public decimal? Grandes_Acciones { get; set; }
            public decimal? Bonosdel_Gobierno { get; set; }
            public decimal? Papelesdel_Tesoro { get; set; }
            public decimal? Inflacion { get; set; }
        }

        public class InvestmentsType
        {
            public string FundName { get; set; }
            public decimal? FundValue { get; set; }
            public string FundType { get; set; }
            public string FundCategory { get; set; }
            public string Region { get; set; }
        }
        public class InvestmentsCompass
        {
            public int? sno1 { get; set; }
            public int? ReturnTypeid { get; set; }
            public int? Years { get; set; }
            public double? ReturnValue { get; set; }
        }
        public class InvestmentsSlide3
        {
            public int Sno { get; set; }
            public string Type { get; set; }
            public decimal? Percentage { get; set; }
        }
        public class InvestmentsSlide4
        {
            public int Sno { get; set; }
            public decimal? Risk { get; set; }
            public decimal? Return { get; set; }
            public decimal? Stocks { get; set; }
            public decimal? Bonds { get; set; }
        }
        public class InvestmentsSlide5Chart1
        {
            public int Sno { get; set; }
            public string Years { get; set; }
            public decimal? Acciones { get; set; }
            public decimal? Portafolio_Diversificado { get; set; }
        }
        public class InvestmentsSlide5Chart2
        {
            public int Sno { get; set; }
            public string Years { get; set; }
            public decimal? Acciones { get; set; }
            public decimal? Portafolio_Diversificado { get; set; }
        }
        public class InvestmentsSlide6
        {
            public int Sno { get; set; }
            public string Type { get; set; }
            public long? Riesgo { get; set; }
            public long? Rendimiento { get; set; }
        }
        public class InvestmentsReturns
        {
            public int SNo { get; set; }
            public string ReturnType { get; set; }
            public decimal ReturnValue { get; set; }
            public string Region { get; set; }
        }

        public class InvestmentsProfile
        {
            public string region { get; set; }
            public decimal? cricimiento { get; set; }
            public decimal? balancedo { get; set; }
            public decimal? Moderado { get; set; }
            public string stl { get; set; }
            public string categoria { get; set; }
            public string simbolo { get; set; }
            public string nombre_del_indice { get; set; }
            public string year2008 { get; set; }
            public string year2009 { get; set; }
            public string fstterm { get; set; }
            public string secndterm { get; set; }
            public string trdterm { get; set; }
            public string year2006 { get; set; }
            public string year2007 { get; set; }
            public string year2010 { get; set; }
        }
        public class InvestmentsProfileEuro
        {
            public string region { get; set; }
            public decimal? cricimiento { get; set; }
            public decimal? balancedo { get; set; }
            public decimal? Moderado { get; set; }
            public string stl { get; set; }
            public string categoria { get; set; }
            public string simbolo { get; set; }
            public string nombre_del_indice { get; set; }
            public string year2008 { get; set; }
            public string year2009 { get; set; }
            public string fstterm { get; set; }
            public string secndterm { get; set; }
            public string trdterm { get; set; }
            public string year2006 { get; set; }
            public string year2007 { get; set; }
            public string year2010 { get; set; }
        }

        public class RptAxysFixedinComeSlide12
        {
            public long Sno { get; set; }
            public int? Cash { get; set; }
            public int? Bond { get; set; }
            public string Yield { get; set; }
            public string Risk { get; set; }
        }
        public class RptAxysHighperFormSlide12
        {
            public long Sno { get; set; }
            public int? Cash { get; set; }
            public int? bond { get; set; }
            public int? Actions { get; set; }
            public string Yield { get; set; }
            public string Risk { get; set; }
        }
        public class RptAxysLowRiskSlide12
        {
            public long Sno { get; set; }
            public int? Cash { get; set; }
            public int? bond { get; set; }
            public int? Actions { get; set; }
            public string Yield { get; set; }
            public string Risk { get; set; }
        }
        public class RptAxysSlide10
        {
            public long sno { get; set; }
            public string Category { get; set; }
            public decimal? Percentage { get; set; }
        }
        public class RptAxysSlide11
        {
            public long Sno { get; set; }
            public string Type { get; set; }
            public string Years { get; set; }
            public int MaxYear { get; set; }
            public int MinYear { get; set; }
            public int Compound_Percent { get; set; }
        }
        public class RptAxysSlide5
        {
            public int? Year { get; set; }
            public decimal? Saving_Rate { get; set; }
            public long sno { get; set; }
        }
        public class RptAxysSlide6
        {
            public long sno { get; set; }
            public int? Year { get; set; }
            public int? Miles_USD { get; set; }
        }
        public class RptAxysSlide8
        {
            public long sno { get; set; }
            public string category { get; set; }
            public int? Percent { get; set; }
        }

        public class EgrAge
        {
            public long sno { get; set; }
            public string age { get; set; }
            public long? Amount { get; set; }
        }
        public class EgrSlide7
        {
            public int Sno { get; set; }
            public string catogory { get; set; }
            public decimal? percent { get; set; }
            public decimal? average { get; set; }
        }
        public class EgrSlide8
        {
            public int Sno { get; set; }
            public string catogory { get; set; }
            public long? Amount { get; set; }
        }
        public class EgrSlide9
        {
            public int Sno { get; set; }
            public string catogory { get; set; }
            public decimal? Amount { get; set; }
        }
        public class EgrSlide10
        {
            public long sno { get; set; }
            public int? Year { get; set; }
            public decimal? Percent { get; set; }
            public string type { get; set; }
        }

        public class RptLegacy10Priciple
        {
            public int sno { get; set; }
            public int? num { get; set; }
            public string Causesde_Falle { get; set; }
            public decimal? Paisesde_Bajos_millones { get; set; }
            public decimal? Paisesde_Bajos_percentage { get; set; }
            public decimal? Paisesde_Altos_millones { get; set; }
            public decimal? Paisesde_Altos_percentage { get; set; }
            public string Type { get; set; }
        }
        public class RptCompassSlide5
        {
            public long Sno { get; set; }
            public string Category { get; set; }
            public Nullable<decimal> Percentage { get; set; }
        }
        public class RptLifeExpectancy
        {
            public int Sno { get; set; }
            public int? Current_Age { get; set; }
            public decimal? Men { get; set; }
            public decimal? Woman { get; set; }
        }

        public class RptInvestmentsCompassMaster
        {
            public int Sno { get; set; }
            public string ReturnType { get; set; }
            public string Region { get; set; }
        }

        public class RptCompassSlide7
        {
            public long Sno { get; set; }
            public string Continent { get; set; }
            public decimal? Deaths { get; set; }
            public long? Area { get; set; }
        }

        public class CustomerPlanDetGlobalPolicy
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticRegId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public long CustomerPlanNo { get; set; }
        }

        public class Company
        {
            public string CompanyName { get; set; }
            public string BrandName { get; set; }
            public int companyNo { get; set; }
            public DateTime? DateCreated { get; set; }
            public DateTime? DateUpdated { get; set; }
            public DateTime? DateSynced { get; set; }
        }

        public class IllustrationInformation
        {
            public long CustomerPlanNo { get; set; }
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public string Company { get; set; }
            public string Country { get; set; }
            public string FamilyProduct { get; set; }
            public string Office { get; set; }
            public string IllustrationNo { get; set; }
            public string Status { get; set; }
            public string Identification { get; set; }
            public string InsuredName { get; set; }
            public string AgentName { get; set; }
            public string PlanType { get; set; }
            public string Frequency { get; set; }
            public decimal? InsuredAmount { get; set; }
            public decimal? Deductible { get; set; }
            public decimal? TotalPremium { get; set; }
            public decimal? InitialPremium { get; set; }
            public DateTime? IllustrationDate { get; set; }
            public DateTime? ExpirationDate { get; set; }
            public int? AvailableDays { get; set; }
            public long InsuredId { get; set; }
        }

        public class CustomerPlanDetailP
        {
            public long? CustomerPlanNo { get; set; }
            public long? CustomerNo { get; set; }
            public long? RCustomerNo { get; set; }
            public int? UserId { get; set; }
            public int? CompanyId { get; set; }
            public DateTime? DateTo { get; set; }
            public DateTime? DateFrom { get; set; }
            public int? CorpId { get; set; }
            public int? RegionId { get; set; }
            public int? CountryId { get; set; }
            public int? DomesticregId { get; set; }
            public int? StateProvId { get; set; }
            public int? CityId { get; set; }
            public int? OfficeId { get; set; }
            public int? CaseSeqNo { get; set; }
            public int? HistSeqNo { get; set; }
            public int? LanguageId { get; set; }
            public bool? GetHistorical { get; set; }
            public string IllustrationStatusCode { get; set; }
            public string TabFilter { get; set; }
            public string AgentNameId { get; set; }
            public string AgentType { get; set; }
            public string AssignedSubscriberNameId { get; set; }
        }

        public class QuoGrid
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int BussinessLineType { get; set; }
            public int BussinessLineId { get; set; }
            public int CompanyId { get; set; }
            public string CompanyDesc { get; set; }
            public string PolicyNo { get; set; }
            public string BlDesc { get; set; }
            public string ProductTypeDesc { get; set; }
            public string OfficeDesc { get; set; }
            public int AgentId { get; set; }
            public string DistributionDesc { get; set; }
            public int PolicyStatusId { get; set; }
            public string PolicyStatusDesc { get; set; }
            public string PolicyStatusNameKey { get; set; }
            public string AgentName { get; set; }
            public int ContactId { get; set; }
            public string FullName { get; set; }
            public string IdContact { get; set; }
            public decimal? InsuredAmount { get; set; }
            public decimal? AnnualPremium { get; set; }
            public decimal? InitialContribution { get; set; }
            public DateTime QuoDate { get; set; }
            public DateTime? InspectionQuoDate { get; set; }
            public int Days { get; set; }
            public int DocumentMissing { get; set; }
            public int? SubscriberAgentId { get; set; }
            public string SubscriberName { get; set; }
            public int? InspectorAgentId { get; set; }
            public string InspectorName { get; set; }
            public string PolicyNoTemp { get; set; }
            public string Tab { get; set; }
            public DateTime? DeclinedQuoDate { get; set; }
            public bool IsExpiring { get; set; }
            public bool IsExpired { get; set; }
            public string TipoRiesgoNameKey { get; set; }
            public DateTime? QuoPosDate { get; set; }
            public DateTime? EffectiveDate { get; set; }
            public DateTime? ExpirationDate { get; set; }
            public int? WorkMinute { get; set; }
            public int? SubscriptionMinute { get; set; }
            public int? InspectionMinute { get; set; }
            public bool HasDiscount { get; set; }
            public decimal? DiscountAmount { get; set; }
            public int? SupervisorAgentId { get; set; }
            public string SupervisorAgentName { get; set; }
            public string DeclinedQuoReason { get; set; }
            public string MissingDocumentQuoReason { get; set; }
            public int ConfirmationCallerAgentId { get; set; }
            public string ConfirmationCallerName { get; set; }
            public bool HasSurcharge { get; set; }
            public bool MakeDiscount { get; set; }
            public string ProductSubTypeDesc { get; set; }
            public DateTime? PolicyExpirationDate { get; set; }
            public int QuoCreateUserId { get; set; }
            public string QuoCreateUserName { get; set; }

            public class Key
            {
                public int CorpId { get; set; }
                public string Tab { get; set; }
                public int? CompanyId { get; set; }
                public DateTime? DateTo { get; set; }
                public DateTime? DateFrom { get; set; }
                public int? OfficeId { get; set; }
                public int? AgentId { get; set; }
                public int? BlId { get; set; }
            }

            public class Counter
            {
                public string Tab { get; set; }
                public int? Count { get; set; }
            }
        }
    }
}