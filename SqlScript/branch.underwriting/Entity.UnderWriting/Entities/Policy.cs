﻿using System;
using System.Collections.Generic;
using System.Data;

namespace Entity.UnderWriting.Entities
{
    public class Policy
    {
        public class BlackList
        {
            public class Parameter
            {
                public int? corpId { get; set; }
                public int? regionId { get; set; }
                public int? countryId { get; set; }
                public int? domesticregId { get; set; }
                public int? stateProvId { get; set; }
                public int? cityId { get; set; }
                public int? officeId { get; set; }
                public int? caseSeqNo { get; set; }
                public int? histSeqNo { get; set; }
                public bool blacklistCheck { get; set; }
                public int? userId { get; set; }
            }
        }

        public class BlackListMember
        {
            public class Parameter
            {
                public int? corpId { get; set; }
                public int? regionId { get; set; }
                public int? countryId { get; set; }
                public int? domesticregId { get; set; }
                public int? stateProvId { get; set; }
                public int? cityId { get; set; }
                public int? officeId { get; set; }
                public int? caseSeqNo { get; set; }
                public int? histSeqNo { get; set; }
                public string blacklistMember { get; set; }
                public int? userId { get; set; }
            }
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
            public string PolicyNoMain { get; set; }
            public bool NeedInspection { get; set; }
            public bool HasFacultative { get; set; }
            public string AgentPhones { get; set; }
            public int? RequestTypeId { get; set; }
            public string RequestTypeDesc { get; set; }
            public decimal? ProratedPremium { get; set; }
            public bool? Financed { get; set; }
            public decimal? Rate { get; set; }
            public string Make { get; set; }
            public string Model { get; set; }
            public string Year { get; set; }
            public decimal? ModelAccidentRate { get; set; }
            public decimal? VendorAccidentRate { get; set; }
            public decimal? AgentAccidentRate { get; set; }
            public string MinDeducCristales { get; set; }
            public string MinDeducDP { get; set; }
            public string PorcDeducDP { get; set; }

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
                public int? UserId { get; set; }
                public string Bandeja { get; set; }
                public DataTable AgentChain { get; set; }
            }

            public class AgentChain
            {
                public int CorpId { get; set; }
                public int AgentId { get; set; }
                public string AgentNameId { get; set; }
                public int BlId { get; set; }
            }

            public class Counter
            {
                public string Tab { get; set; }
                public int? Count { get; set; }
            }
        }

        public class OnBase
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
            public string policyNo { get; set; }
            public int IsQuotation { get; set; }
        }

        public class AgentInfo
        {
            public int AgentId { get; set; }
            public Nullable<int> IdentityTypeId { get; set; }
            public string ID { get; set; }
            public string AgentCode { get; set; }
            public string NameId { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string FirstLastname { get; set; }
            public string SecondLastname { get; set; }
            public string Nickname { get; set; }
            public DateTime? Dob { get; set; }
            public string Gender { get; set; }
            public int? MaritalStatId { get; set; }
            public int? ResidenceCountryId { get; set; }
            public int? BirthCountryId { get; set; }
            public int? CitizenshipCountryId { get; set; }
            public int DirectoryId { get; set; }
            public string AgentTypeDesc { get; set; }
            public string MaritalStatusDesc { get; set; }
            public string CountryOfBirthDesc { get; set; }
            public string KcoUniqueId { get; set; }

            public class Directory
            {
                public int CorpId { get; set; }
                public int DirectoryId { get; set; }
                public int DirDetailId { get; set; }
                public int CommTypeId { get; set; }
                public string CommTypeDesc { get; set; }
                public int DirectoryTypeId { get; set; }
                public string DirTypeDesc { get; set; }
                public int PhoneTypeId { get; set; }
                public string PhoneTypeDesc { get; set; }
                public string PhonePrefix { get; set; }
                public string AreaCode { get; set; }
                public string PhoneNumber { get; set; }
                public string PhoneExt { get; set; }
                public string Address { get; set; }
                public int RegionId { get; set; }
                public string RegionDesc { get; set; }
                public int CountryId { get; set; }
                public string CountryDesc { get; set; }
                public int DomesticRegionId { get; set; }
                public string DomesticregDesc { get; set; }
                public int StateProvId { get; set; }
                public string StateProvDesc { get; set; }
                public int CityId { get; set; }
                public string CityDesc { get; set; }
                public string OfficeDesc { get; set; }
                public string AddressNo { get; set; }
                public string BlgdNumber { get; set; }
                public string Floor { get; set; }
                public string Door { get; set; }
                public string NearToReference { get; set; }
                public string ZipCode { get; set; }
                public bool isPrimary { get; set; }
                public int DirectoryOwnerType { get; set; }
            }

            public class Agent
            {
                public string Action { get; set; }
                public int? CorpId { get; set; }
                public int? AgentId { get; set; }

                public class parameter
                {
                    public int? corpId { get; set; }
                    public int? agentId { get; set; }
                    public int? agentTypeId { get; set; }
                    public int? identityTypeId { get; set; }
                    public int? agentSubTypeId { get; set; }
                    public string iD { get; set; }
                    public string agentCode { get; set; }
                    public string nameId { get; set; }
                    public string firstName { get; set; }
                    public string middleName { get; set; }
                    public string firstLastname { get; set; }
                    public string secondLastname { get; set; }
                    public string nickname { get; set; }
                    public DateTime? dob { get; set; }
                    public int? paymentTypeId { get; set; }
                    public string abaNumber { get; set; }
                    public string bankAccountNumber { get; set; }
                    public decimal? allocation { get; set; }
                    public int? allocationTypeId { get; set; }
                    public string gender { get; set; }
                    public int? maritalStatId { get; set; }
                    public string title { get; set; }
                    public int? residenceCountryId { get; set; }
                    public int? birthCountryId { get; set; }
                    public int? citizenshipCountryId { get; set; }
                    public int? directoryId { get; set; }
                    public DateTime? activeDate { get; set; }
                    public DateTime? inactiveDate { get; set; }
                    public string kcoUniqueId { get; set; }
                    public int? userId { get; set; }
                }
            }
        }

        public int CorpId { get; set; }
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public int DomesticregId { get; set; }
        public int StateProvId { get; set; }
        public int CityId { get; set; }
        public int OfficeId { get; set; }
        public int CaseSeqNo { get; set; }
        public int HistSeqNo { get; set; }
        public int ContactId { get; set; }
        public int? DocumentType { get; set; }
        public string PolicyNo { get; set; }
        public string PolicyNoTemp { get; set; }
        public int? PolicySerieId { get; set; }
        public int? CurrencyId { get; set; }
        public int? QuotationCurrencyId { get; set; }
        public int? PolicyCurrencyId { get; set; }
        public int? PaymentsCurrencyId { get; set; }
        public int? BussinessLineType { get; set; }
        public int? BussinessLineId { get; set; }
        public int? ProductId { get; set; }
        public int? DeductibleTypeId { get; set; }
        public int? DeductibleCategoryId { get; set; }
        public decimal? DeductibleManualValue { get; set; }
        public bool? Reinsured { get; set; }
        public decimal? ReinsuredAmount { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? VanishDate { get; set; }
        public DateTime? TermDate { get; set; }
        public decimal? PeriodicPremium { get; set; }
        public int? ContributionTypeId { get; set; }
        public decimal? GoalAmount { get; set; }
        public decimal? GoalAtAge { get; set; }
        public DateTime? PolicyEffectiveDate { get; set; }
        public DateTime? BenefitEndingDate { get; set; }
        public string BenefitPeriod { get; set; }
        public int? DefermentPeriod { get; set; }
        public decimal? MinimunPremiunAmount { get; set; }
        public decimal? InsuredAmount { get; set; }
        public decimal? TargetPremium { get; set; }
        public decimal? InitialContribution { get; set; }
        public int? YearContributionPeriod { get; set; }
        public int? InsuredPeriod { get; set; }
        public int? CollectionStatusId { get; set; }
        public decimal? BenefitAmount { get; set; }
        public decimal? AnnualPremium { get; set; }
        public decimal? AnnualBenefit { get; set; }
        public DateTime? EndingContributionDate { get; set; }
        public DateTime? InitialBenefitDate { get; set; }
        public decimal? ModalPremium { get; set; }
        public decimal? Rate { get; set; }
        public decimal? ExcessPremium { get; set; }
        public int? RolId { get; set; }
        public int? PolicyStatusId { get; set; }
        public bool? SumARisk { get; set; }
        public decimal? RopAmount { get; set; }
        public decimal? InitialPremium { get; set; }
        public decimal? InsurancePremium { get; set; }
        public decimal? RetirementAmount { get; set; }
        public decimal? AnnualRetirement { get; set; }
        public decimal? MinAnnualPremium { get; set; }
        public int? InsuranceDuration { get; set; }
        public int? ContributionYears { get; set; }
        public int? RetirementPeriod { get; set; }
        public decimal? ReturnAmount { get; set; }
        public int? InvestmentProfile { get; set; }
        public DateTime? ContributionEndDate { get; set; }
        public DateTime? RetirementBeginDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? RetirementEndDate { get; set; }
        public DateTime? PaidThrough { get; set; }
        public bool? Priority { get; set; }
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal? TaxPremium { get; set; }
        public decimal? TaxPercentage { get; set; }
        public int Agent_Id { get; set; }
        public string Agent_Name { get; set; }
        public string StatusNameKey { get; set; }
        public string AgentCode { get; set; }
        public string NameId { get; set; }
        public int? ProviderTypeId { get; set; }
        public int? ProviderId { get; set; }
        public decimal DiscountPremium { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int? MonthContributionPeriod { get; set; }
        public int? MonthsInsuredPeriod { get; set; }
        public int? InsuranceDurationMonths { get; set; }
        public int? ContributionMonths { get; set; }
        public int? PaymentFreqTypeId { get; set; }
        public string PaymentFreqTypeDesc { get; set; }
        public string PolicyNoMain { get; set; }
        public decimal? Fraction_Surcharge { get; set; }
        public decimal? Net_Annual_Premium { get; set; }
        public int? MyProperty { get; set; }
        public decimal? MonthlyPayment { get; set; }
        public bool? Financed { get; set; }
        public int? Period { get; set; }
        public bool? directDebit { get; set; }
        public int? loanPeitionNo { get; set; }
        public bool? AgentIsFinancial { get; set; }
        public string InvoiceNumber { get; set; }

        public class PolicyContact
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int State_ProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public string PolicyNo { get; set; }
            public string PolicyNoTemp { get; set; }
            public string Tab { get; set; }
        }

        public class VehicleCoverage
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public long VehicleUniqueId { get; set; }
            public int BlTypeId { get; set; }
            public int BlId { get; set; }
            public int ProductId { get; set; }
            public int VehicleTypeId { get; set; }
            public int GroupId { get; set; }
            public int CoverageTypeId { get; set; }
            public string CoverageTypeDesc { get; set; }
            public int CoverageId { get; set; }
            public int? CurrencyId { get; set; }
            public decimal UnitaryPrice { get; set; }
            public decimal PackagePrice { get; set; }
            public int? CoverageStatus { get; set; }
            public int UserId { get; set; }
            public string CoverageNameKey { get; set; }
            public string CoverageDesc { get; set; }
            public decimal? CoinsurancePercentage { get; set; }
            public decimal? DeductibleAmount { get; set; }
            public decimal? DeductiblePercentage { get; set; }
            public decimal? ManualDeductibleAmount { get; set; }
            public decimal? ManualDeductiblePercentage { get; set; }
            public decimal? CoverageLimit { get; set; }
            public decimal PremiumPercentage { get; set; }
            public int? SubRamo { get; set; }

        }

        public class VehicleCoverageGet
        {
            public int CorpId { get; set; }
            public long VehicleUniqueId { get; set; }
            public int? CoverageTypeId { get; set; }
        }

        public class ConditionForSysflexIL
        {
            public int? ramo { get; set; }
            public int? subramo { get; set; }
            public string descripcion { get; set; }
            public string valor { get; set; }
            public int? secuenciacondicion { get; set; }
            public int Decimales { get; set; }
            public string esnumero { get; set; }
        }

        public class VehicleInsured
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
            public int? InsuredVehicleId { get; set; }
            public long VehicleUniqueId { get; set; }
            public string RegistrationId { get; set; }
            public DateTime? InsuredDate { get; set; }
            public decimal? PremiumAmount { get; set; }
            public string PremiumAmountF { get; set; }
            public decimal? BasePremiumAmount { get; set; }
            public int? MakeId { get; set; }
            public int? ModelId { get; set; }
            public int? ColorId { get; set; }
            public int? VehicleTypeId { get; set; }
            public int? Year { get; set; }
            public string Chassis { get; set; }
            public string Registry { get; set; }
            public int? PassengerNumber { get; set; }
            public string CylindersTons { get; set; }
            public int? UsageId { get; set; }
            public int? VehicleValue { get; set; }
            public int? StoredId { get; set; }
            public bool? Garage { get; set; }
            public int? RentTypeId { get; set; }
            public int? RentLengthId { get; set; }
            public string AmbulanceTypeId { get; set; }
            public string GeographicLimitation { get; set; }
            public string MakeDesc { get; set; }
            public string ModelDesc { get; set; }
            public string ColorDesc { get; set; }
            public string VehicleTypeDesc { get; set; }
            public string UsageDesc { get; set; }
            public string StoredDesc { get; set; }
            public DateTime? ExpirationDate { get; set; }
            public bool? Inspection { get; set; }
            public string InspectionAddress { get; set; }
            public string ProductTypeDesc { get; set; }
            public bool? New { get; set; }
            public string EndorsementBeneficiary { get; set; }
            public string EndorsementBbeneficiaryRnc { get; set; }
            public decimal? EndorsementAmount { get; set; }
            public string EndorsementContactName { get; set; }
            public string EndorsementContactPhone { get; set; }
            public string EndorsementContactEmail { get; set; }
            public bool? AppliesToReinsurance { get; set; }
            public bool EndorsementClarifying { get; set; }
            public decimal OwnDamage { get; set; }
            public string OwnDamageF { get; set; }
            public string VehicleDesc { get; set; }
            public decimal? InsuredAmount { get; set; }
            public string InsuredAmountF { get; set; }
            public bool SurchargeApplied { get; set; }
            public int? DriverContactId { get; set; }
            public string DriverFullName { get; set; }
            public string ProductNameKey { get; set; }
            public decimal? DeductiblePercentage { get; set; }
            public bool HasOwnDamage { get; set; }
            public bool? IsInspected { get; set; }
            public bool? InspectionRequired { get; set; }
            public int UserId { get; set; }
            public int? VehicleVersionId { get; set; }
            public string VehicleVersionDesc { get; set; }
            public string SourceId { get; set; }
            public decimal? ReinsuranceAmount { get; set; }
            public decimal? ReinsurancePercentage { get; set; }
            public string rateJsonSysflex { get; set; }
            public string VehicleCapacity { get; set; }
            public decimal? DPPremiumAmount { get; set; }
            public decimal? ReinsurancePremiumAmount { get; set; }
            public decimal? ProratedPremium { get; set; }
            public string longitud { get; set; }
            public string latitud { get; set; }
            public string cssClassCoverages { get; set; }
            public bool isExclusion { get; set; }
            public string fuelTypeDesc { get; set; }
            public decimal? AccidentRate { get; set; }

            public class InspectionV
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
                public long VehicleUniqueId { get; set; }
                public bool Inspection { get; set; }
                public int UserId { get; set; }
                public bool? EndorsementClarifying { get; set; }
                public int InsuredVehicleId { get; set; }
                public int ReviewStatusId { get; set; }
                public bool ReviewStatus { get; set; }
                public string InspectionAddress { get; set; }
            }

            public class CoverageTypePremiun
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
                public int InsuredVehicleId { get; set; }
                public int CoverageTypeId { get; set; }
                public decimal PremiumAmount { get; set; }
                public decimal BasePremiumAmount { get; set; }
                public string CoverageTypeDesc { get; set; }
                public long VehicleUniqueId { get; set; }

                public int UserId { get; set; }

                public class Key
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
                    public int InsuredVehicleId { get; set; }
                    public int? CoverageTypeId { get; set; }

                }
            }

            public class Discount
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
                public int InsuredVehicleId { get; set; }
                public int? DiscountId { get; set; }
                public int DiscountRuleId { get; set; }
                public int DiscountRuleDetailId { get; set; }
                public int NotePredefiniedId { get; set; }
                public decimal PremiumAmount { get; set; }
                public decimal OldPremiumAmount { get; set; }
                public DateTime DetailApplyDate { get; set; }
                public string DetailRuleValue { get; set; }
                public string DetailRuleNameKey { get; set; }
                public string DiscountRuleDesc { get; set; }
                public string DiscountNameKey { get; set; }
                public string NotePredefiniedDesc { get; set; }
                public string NoteNameKey { get; set; }
                public string Comment { get; set; }
                public string FullName { get; set; }
                public bool DiscountStatus { get; set; }
                public int UserId { get; set; }

                public string TipoDescuento { get; set; }
                public string PorcentajeDescuento { get; set; }
                public decimal Descuento { get; set; }
                public decimal MontoDescuento { get; set; }
                public string MontoDescuentoF { get; set; }
                public bool VisibleButton { get; set; }

                public class Key
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
                    public int InsuredVehicleId { get; set; }
                    public int? DiscountId { get; set; }
                    public int LanguageId { get; set; }
                }
            }
        }

        public class PlanData
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
            public int? CurrencyId { get; set; }
            public int PolicyStatusId { get; set; }
            public string PolicyNo { get; set; }
            public string InsuredName { get; set; }
            public string OwnerName { get; set; }
            public decimal? RetentionAmount { get; set; }
            public decimal? RopAmount { get; set; }
            public int? DefermentPeriod { get; set; }
            public int? RetirementPeriod { get; set; }
            public string PolicyStatus { get; set; }
            public string PlanName { get; set; }
            public string PlanType { get; set; }
            public string Currency { get; set; }
            public string AdditionalInsured { get; set; }
            public decimal? InsuredAmount { get; set; }
            public decimal? CumulativeRisk { get; set; }
            public int? CumulativeRiskCount { get; set; }
            public decimal? ReinsuredAmount { get; set; }
            public decimal? ReinsuredAmountTotal { get; set; }
            public decimal? InitialContribution { get; set; }
            public decimal? AnnualPremium { get; set; }
            public decimal? MinAnnualPremium { get; set; }
            public decimal? TargetPremium { get; set; }
            public int? ContributionYears { get; set; }
            public decimal? RetentionAmountTotal { get; set; }
            public decimal? PeriodicPremium { get; set; }
            public decimal? PeriodicPremiumUncalculated { get; set; }
            public int? ContributionTypeId { get; set; }
            public decimal? GoalAmount { get; set; }
            public decimal? GoalAtAge { get; set; }
            public string PaymentCycle { get; set; }
            public string ProfileTypeDesc { get; set; }
            public DateTime? SubmittedDate { get; set; }
            public DateTime? EffectiveDate { get; set; }
            public DateTime? CompletedDate { get; set; }
            public DateTime? ExpiredDate { get; set; }
            public DateTime? LastPaymentDate { get; set; }
            public DateTime? TerminationDate { get; set; }
            public int? ContributionPeriod { get; set; }
            public DateTime? RetirementBeginDate { get; set; }
            public DateTime? RetirementEndDate { get; set; }
            public int? BlId { get; set; }
            public int? BlTypeId { get; set; }
            public int? InsuredContactId { get; set; }
            public int? OwnerContactId { get; set; }
            public int? AdditionalContactId { get; set; }
            public int? ProfileTypeId { get; set; }
            public int? InvestProductDateId { get; set; }
            public DateTime? InvestmentProductDate { get; set; }
            public int? ProductId { get; set; }
            public int? PolicySerieId { get; set; }
            public int? PaymentFreqTypeId { get; set; }
            public int? PaymentFreqId { get; set; }
            public string InsuredFirstName { get; set; }
            public string InsuredMiddleName { get; set; }
            public string InsuredLastName { get; set; }
            public string InsuredSecondLastName { get; set; }
            public int? AgeAtStartOfRetirement { get; set; }
            public decimal? MinimunPremiunAmount { get; set; }
            public decimal? BenefitAmount { get; set; }
            public decimal? ReturnAmount { get; set; }
            public string OfficeDesc { get; set; }
            public string CountryOfficeDesc { get; set; }
            public string AgentFullName { get; set; }
            public string PaymentFreqTypeDesc { get; set; }
            public decimal? FuturePayment { get; set; }
            public string DetailMethod { get; set; }
            public string PaymentMethod { get; set; }
            public string PaymentStatusDesc { get; set; }
            public string DesignatedPensionerName { get; set; }
            public int? InsuredPeriod { get; set; }
            public decimal? RetirementAmount { get; set; }
            public int? ProductTypeId { get; set; }
            public int? AgentId { get; set; }
            public string NameKey { get; set; }
            public DateTime? InsuredDob { get; set; }
            public DateTime? OwnerDob { get; set; }
            public DateTime? InsuredAddDob { get; set; }
            public int? DeductibleTypeId { get; set; }
            public int? DeductibleCategoryId { get; set; }
            public decimal? DeductibleManualValue { get; set; }
            public string ProviderName { get; set; }
            public int? ProviderTypeId { get; set; }
            public int? ProviderId { get; set; }
            public decimal? InterestRate { get; set; }
            public decimal? SpecialPayment { get; set; }
            public string DestinationFund { get; set; }
            public int? MonthContributionPeriod { get; set; }
            public int? MonthsInsuredPeriod { get; set; }
            public int? ContributionMonths { get; set; }
            public int? InsuranceDurationMonths { get; set; }
            public decimal? Fraction_Surcharge { get; set; } //Bmarroquin 28-07-2017
            public decimal? NetAnnualPremium { get; set; } //Bmarroquin 05-05-2017
        }

        public class Profile
        {
            public int CorpId { get; set; }
            public int ProfileTypeId { get; set; }
            public int InvstDateId { get; set; }
            public int CurrencyId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int StockExChangeId { get; set; }
            public int SymbolId { get; set; }
            public DateTime GlobalInvestmentProductDate { get; set; }
            public string ProfileTypeDesc { get; set; }
            public string CurrencyDesc { get; set; }
            public decimal GlobalInvstProfilePercent { get; set; }
            public string SymbolAbbr { get; set; }
            public string SymbolDesc { get; set; }
            public bool? HasProfile { get; set; }
            public decimal? ProductInvstProfilePercent { get; set; }
            public string PolicyNo { get; set; }
            public int? ProductCorpId { get; set; }
            public int? ProductRegionId { get; set; }
            public int? ProductCountryId { get; set; }
            public int? ProductDomesticregId { get; set; }
            public int? ProductStateProvId { get; set; }
            public int? ProductCityId { get; set; }
            public int? ProductOfficeId { get; set; }
            public int? ProductCaseSeqNo { get; set; }
            public int? ProductHistSeqNo { get; set; }
            public int? ProductInvestProductDateId { get; set; }
            public DateTime? ProductInvestmentProductDate { get; set; }
        }

        public struct PolicySummaryByPolicy
        {
            public string RoleDesc { get; set; }
            public string Gender { get; set; }
            public string MaritalStatusDesc { get; set; }
            public string PolicyNo { get; set; }
            public int ContactId { get; set; }
            public string ContactFullName { get; set; }
            public string CountryOfBirth { get; set; }
            public string CountryOfResidence { get; set; }
            public string Compliance { get; set; }
            public bool IsPreApproved { get; set; }
        }

        public struct PolicySummaryByContact
        {
            public string PlanType { get; set; }
            public int ContactId { get; set; }
            public string RoleDesc { get; set; }
            public string PolicyNo { get; set; }
            public string PlanName { get; set; }
            public DateTime? EffectiveDate { get; set; }
            public string RiskClass { get; set; }
            public decimal? BenefitAmount { get; set; }
        }

        public struct PolicyCommentSummary
        {
            public int StepId { get; set; }
            public int OriginatedBy { get; set; }
            public string TypeDesc { get; set; }
            public string OriginatedByName { get; set; }
            public string NoteDesc { get; set; }
            public DateTime CreateDate { get; set; }
            public bool IsDefault { get; set; }
        }

        public struct RequirementSummary
        {
            public int? CorpId { get; set; }
            public int? RegionId { get; set; }
            public int? CountryId { get; set; }
            public int? DomesticregId { get; set; }
            public int? StateProvId { get; set; }
            public int? CityId { get; set; }
            public int? OfficeId { get; set; }
            public int? CaseSeqNo { get; set; }
            public int? HistSeqNo { get; set; }
            public int? ContactId { get; set; }
            public int RequirementCatId { get; set; }
            public int? RequirementTypeId { get; set; }
            public int? RequirementId { get; set; }
            public int? RequirementDocId { get; set; }
            public int? DocTypeId { get; set; }
            public int? DocCategoryId { get; set; }
            public int? DocumentId { get; set; }
            public string RequirementTypeDesc { get; set; }
            public DateTime? ReceivedDate { get; set; }
            public int RequestedBy { get; set; }
            public DateTime RequestedDate { get; set; }
            public bool SendToReinsurance { get; set; }
            public bool IsManual { get; set; }
            public bool? Automatic { get; set; }
            public bool? HasDocument { get; set; }
        }

        public class AssignCase
        {
            public int Underwriter_Id { get; set; }
            public string RoleName { get; set; }
        }

        public struct PaymentSummary
        {
            public string PolicyNo { get; set; }
            public decimal? DueAmount { get; set; }
            public DateTime? DueDate { get; set; }
            public decimal? PaidAmount { get; set; }
            public DateTime? PaidDate { get; set; }
            public decimal? BasePremium { get; set; }
            public decimal? Exceptionalpremium { get; set; }
            public decimal? BaseCommision { get; set; }
            public decimal? BaseCommision2 { get; set; }
            public decimal? ExceptionalCommisions { get; set; }
            public decimal? Exceptional2Commisions { get; set; }
            public string PaymentStatusDesc { get; set; }
        }

        public class PaymentFrequency
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
            public int? PaymentFreqTypeId { get; set; }
            public int? PaymentFreqId { get; set; }
            public DateTime? PaymentDate { get; set; }
            public int UserId { get; set; }
        }

        public class InvestProfile
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
            public int? ProfileTypeId { get; set; }
            public int? InvestProductDateId { get; set; }
            public DateTime InvestmentProductDate { get; set; }
            public string InvstProfileDesc { get; set; }
            public int UserId { get; set; }
        }
        public class InvestProfilePersonalized
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
            public int ProfileTypeId { get; set; }
            public int? InvestProductDateId { get; set; }
            public int SymbolId { get; set; }
            public DateTime? InvstProductDate { get; set; }
            public DateTime InvestmentProfileDate { get; set; }
            public decimal? InvstProfilePercent { get; set; }
            public int? StockExchangeId { get; set; }
            public decimal ProjectionRate { get; set; }
            public int? InvestmentCurrency { get; set; }
            public decimal MinPercentAllowed { get; set; }
            public decimal MaxPercentAllowed { get; set; }
            public DateTime InitialValidDate { get; set; }
            public DateTime EndValidDate { get; set; }
            public string PolicyNo { get; set; }
            public string SerieDesc { get; set; }
            public string ProductDesc { get; set; }
            public string ProfileTypeDesc { get; set; }
            public string CurrencyDesc { get; set; }
            public string ProfileType { get; set; }
            public string Symbol { get; set; }
            public string SymbolDesc { get; set; }
            public int UserId { get; set; }
        }

        public struct CategoryDocument
        {
            public int DocTypeId { get; set; }
            public int DocCategoryId { get; set; }
            public int DocumentId { get; set; }
            public string DocumentName { get; set; }
            public string DocCategoryDesc { get; set; }
            public DateTime? UploadDate { get; set; }
            public DateTime DocumentDate { get; set; }
            public bool HasDocument { get; set; }
            public string KeyName { get; set; }
        }

        public struct AgentChainDetail
        {
            public int OrderId { get; set; }
            public string FullName { get; set; }
            public string CommTable { get; set; }
            public string ProductDescription { get; set; }
            public string OfficeDescription { get; set; }
            public string PositionDescription { get; set; }
        }

        public class PolicyCommunication
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
            public int? CommunicationTypeId { get; set; }
            public string CommunicationTypeDesc { get; set; }
            public int? CallId { get; set; }
            public int CallNoteId { get; set; }
            public int? NoteTypeId { get; set; }
            public string NoteTypeDesc { get; set; }
            public int? PriorityId { get; set; }
            public string PriorityDesc { get; set; }
            public int? CategoryId { get; set; }
            public string CategoryDesc { get; set; }
            public int? ResultId { get; set; }
            public string ResultDesc { get; set; }
            public string Subject { get; set; }
            public string ShortText { get; set; }
            public string LargeText { get; set; }
            public int? PersonToContact { get; set; }
            public string ContactedPerson { get; set; }
            public bool? CallDirectionOutbound { get; set; }
            public int? ContactedBy { get; set; }
            public string Duration { get; set; }
            public bool? Timeless { get; set; }
            public bool? Recurring { get; set; }
            public string Result { get; set; }
            public DateTime? CompletedDate { get; set; }
            public int? CompletedByUserId { get; set; }
            public bool? Attachment { get; set; }
            public bool? HasCall { get; set; }
            public DateTime? DateAdded { get; set; }
            public DateTime? DateModified { get; set; }
            public int? OriginatedBy { get; set; }
            public int? StepTypeId { get; set; }
            public int? StepId { get; set; }
            public int? StepCaseNo { get; set; }
            public DateTime? DateSent { get; set; }
            public string CallregNotehistoryid { get; set; }
            public string HistoryId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string CallerIdNumber { get; set; }
            public string CallerIdName { get; set; }
            public string OutboundNumber { get; set; }
            public string RecordingFile { get; set; }
            public DateTime? ProcessedDate { get; set; }
            public string ProcessedBy { get; set; }
        }

        public struct UnderwritingCallTemplate
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
            public int StepTypeId { get; set; }
            public int StepId { get; set; }
            public int StepCaseNo { get; set; }
            public int TemplateId { get; set; }
            public int LanguageId { get; set; }
            public int CategoryId { get; set; }
            public string CategoryDesc { get; set; }
            public string LanguageDesc { get; set; }
            public bool IsClose { get; set; }
            public string Html { get; set; }
            public string QuestionHtml { get; set; }
        }

        public class UnderwritingCallComment
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
            public int CommentTypeId { get; set; }
            public int? CommentId { get; set; }
            public string CommentTypeDesc { get; set; }
            public string Comments { get; set; }
        }

        public struct SecurityQuestion
        {
            public int CorpId { get; set; }
            public int ContactId { get; set; }
            public int QuestionId { get; set; }
            public string QuestionDesc { get; set; }
            public string Answer { get; set; }
            public bool Response { get; set; }
        }

        public struct ProductByContact
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
            public string PolicyNo { get; set; }
            public DateTime? ProductDate { get; set; }
            public string PolicyStatusDesc { get; set; }
            public string ProductDesc { get; set; }
            public string BlDesc { get; set; }
            public string RoleDesc { get; set; }
        }

        public class Call
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
            public int CommunicationTypeId { get; set; }
            public int? CallId { get; set; }
            public int? StepTypeId { get; set; }
            public int? StepId { get; set; }
            public int? StepCaseNo { get; set; }
            public int? ContactId { get; set; }
            public int? ContactRoleTypeId { get; set; }
            public DateTime DateSent { get; set; }
            public string CallregNotehistoryid { get; set; }
            public string HistoryId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string Duration { get; set; }
            public string CallerIdNumber { get; set; }
            public string CallerIdName { get; set; }
            public string OutboundNumber { get; set; }
            public string RecordingFile { get; set; }
            public DateTime? ProcessedDate { get; set; }
            public string ProcessedBy { get; set; }
            public DateTime? DateAdded { get; set; }
            public DateTime? DateModified { get; set; }
            public int? OriginatedBy { get; set; }
            public string OriginatedByName { get; set; }
            public int UserId { get; set; }
        }

        public struct Tab
        {
            public int CorpId { get; set; }
            public int ProjectId { get; set; }
            public int TabId { get; set; }
            public string TabDesc { get; set; }
            public bool IsValid { get; set; }
        }

        public class Contact
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
            public int ContactId { get; set; }
            public int ContactRoleTypeId { get; set; }
            public int? RegionOfResidenceId { get; set; }
            public int? CountryOfResidenceId { get; set; }
            public int? RegionOfBirthId { get; set; }
            public int? CountryOfBirthId { get; set; }
            public bool? BlockConfirmationCall { get; set; }
            public decimal? HealthMemberPremium { get; set; }
            public decimal? HealthWeigth { get; set; }
            public int? HealthWeigthTypeId { get; set; }
            public decimal? HealthHeight { get; set; }
            public int? HealthHeigthTypeId { get; set; }
            public int? HealthAge { get; set; }
            public string HealthGender { get; set; }
            public bool? HealthSmoke { get; set; }
            public int? HealthExcercise { get; set; }
            public int? HealthDrugs { get; set; }
            public int? HealthSystolic { get; set; }
            public int? HealthDiastolic { get; set; }
            public DateTime? HealthLastMedVisit { get; set; }
            public string HealthLastMedReason { get; set; }
            public string HealthLastMedResult { get; set; }
            public string HealthDrName { get; set; }
            public string HealthDrAddress { get; set; }
            public string HealthDrPhonePrefix { get; set; }
            public string HealthDrPhoneArea { get; set; }
            public string HealthDrPhoneNum { get; set; }
            public string HealthMedication { get; set; }
            public bool? HealthFullTimeStudent { get; set; }
            public decimal? AsstTotalAssets { get; set; }
            public decimal? AsstRealEstate { get; set; }
            public decimal? AsstPersonalEffects { get; set; }
            public decimal? AsstVehicle { get; set; }
            public decimal? AsstMachineryEqpmnt { get; set; }
            public decimal? AsstStockBonds { get; set; }
            public decimal? AsstOtherAssets { get; set; }
            public decimal? LblTotalLiabilities { get; set; }
            public decimal? LblMachineryEqpmnt { get; set; }
            public decimal? LblNotePayable { get; set; }
            public decimal? LblBankDebts { get; set; }
            public decimal? LblPersonalDebts { get; set; }
            public decimal? LblMortgageDebts { get; set; }
            public decimal? LblOutstandingTaxes { get; set; }
            public decimal? LblShortTermsLoans { get; set; }
            public decimal? LblOtherLiabilities { get; set; }
            public decimal? FncTotalEstateAmnt { get; set; }
            public decimal? FncAnnualRevMainActvt { get; set; }
            public decimal? FncAnnualIncomeOtherJobs { get; set; }
            public decimal? FncAnnualIncomeInvst { get; set; }
            public decimal? FncAnnualIncomeTrade { get; set; }
            public int? HomeStatusId { get; set; }
            public int? LaborPlayedId { get; set; }
            public string LineOfBusiness { get; set; }
            public string LineOfBusiness2 { get; set; }
            public string CompanyName { get; set; }
            public int? LengthWorkYear { get; set; }
            public int? LengthWorkMonth { get; set; }
            public string Labortasks { get; set; }
            public string CompanyActivity { get; set; }
            public DateTime? CompanyFoundationDate { get; set; }
            public int? OccupGroupTypeId { get; set; }
            public int? OccupationId { get; set; }
            public int? StudentStatusId { get; set; }
            public int? RelationshiptoAgent { get; set; }
            public int? RelationshiptoOwner { get; set; }
            public decimal? AnnualPersonalIncome { get; set; }
            public decimal? AnnualFamilyIncome { get; set; }
            public bool? Smoker { get; set; }
            public int? MaritalStatId { get; set; }
            public int? BeneficiaryTypeId { get; set; }
            public int? PrimaryBeneficiaryId { get; set; }
            public bool? PrimaryBeneficiary { get; set; }
            public int? RelationshipToOwnerId { get; set; }
            public int? RelToPrimaryBenefId { get; set; }
            public decimal? BenefitsPercent { get; set; }
            public string Comment { get; set; }
            public string TipoRiesgoNameKey { get; set; }
            public int? InvoiceTypeId { get; set; }
            public int UserId { get; set; }

            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string FirstLastName { get; set; }
            public string SecondLastName { get; set; }
            public int? RelationshiptoOwnerId { get; set; }
            public string RelationshiptoOwnerDesc { get; set; }
            public string Gender { get; set; }

            public DateTime? Dob { get; set; }
            public string MaritalStatusDesc { get; set; }
            public string Id { get; set; }
            public string GenderDesc { get; set; }

            public string FullName { get; set; }
            public int? SeqNo { get; set; }
            public int? ContactIdType { get; set; }
            public DateTime? ValidDate { get; set; }
            public DateTime? ExpireDate { get; set; }
            public decimal? InterestRate { get; set; }
            public decimal? SpecialPayment { get; set; }
            public string DestinationFund { get; set; }
            public int? finalBeneficiaryOptionId { get; set; }
            public int? pepFormularyOptionId { get; set; }
            public int? companyStructureId { get; set; }
            public int? companyActivityId { get; set; }
            public bool? ForeignLicense { get; set; }
            public string workAddress { get; set; }
            public string placeOfBirth { get; set; }
            public Nullable<int> typeOfPerson { get; set; }
            public string managerName { get; set; }
            public Nullable<int> managerPepOptionId { get; set; }

            public class Action
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
                public int ContactId { get; set; }
                public int ContactRoleTypeId { get; set; }
                public int ActionId { get; set; }
                public int ActionSeqNo { get; set; }
                public int StepTypeId { get; set; }
                public int StepId { get; set; }
                public int StepCaseNo { get; set; }
                public string ActionDesc { get; set; }
            }
        }

        public class Parameter
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
            public int UnderwriterId { get; set; }
            public int LanguageId { get; set; }
            public int? AgentId { get; set; }
            public int? UserId { get; set; }
            public int? StatusChangeTypeId { get; set; }
            public int? StatusId { get; set; }
            public int? ContactId { get; set; }
            public decimal? InterestedRate { get; set; }
            public decimal? SpecialPayment { get; set; }
            public string DestinationOfFunds { get; set; }
        }

        public class Facultative
        {
            public class Key
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
                public long UniqueId { get; set; }
            }

            public class SetContractCoverage
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
                public long UniqueId { get; set; }
                public IEnumerable<Facultative.Coverage> Coverages { get; set; }
                public decimal UserId { get; set; }
            }

            public class Contract2
            {
                public int CorpId { get; set; }
                public long ContractUniqueId { get; set; }
                public int ContractCoverageId { get; set; }
                public int RegionId { get; set; }
                public int CountryId { get; set; }
                public long UniqueId { get; set; }
                public int BlTypeId { get; set; }
                public int BlId { get; set; }
                public int ProductId { get; set; }
                public int VehicleTypeId { get; set; }
                public int GroupId { get; set; }
                public int CoverageTypeId { get; set; }
                public int CoverageId { get; set; }
                public decimal ContractCoveragePercentage { get; set; }
                public int CompanyFacultativeId { get; set; }
                public string ContractName { get; set; }
                public decimal ContractAmount { get; set; }
                public decimal ContractCommissionPercentage { get; set; }
                public string CompanyFacultativeDesc { get; set; }
                public string CompanyFacultativeNameKey { get; set; }

                public DateTime? PaymentDate { get; set; }
                public string CoverageDesc { get; set; }
            }

            public class Contract
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
                public long UniqueId { get; set; }
                public int ContractId { get; set; }
                public long ContractUniqueId { get; set; }
                public int CompanyFacultativeId { get; set; }
                public string CompanyFacultativeDesc { get; set; }
                public string CompanyFacultativeNameKey { get; set; }
                public string ContractName { get; set; }
                public decimal ContractAmount { get; set; }
                public decimal ContractCommissionPercentage { get; set; }
                public DateTime PaymentDate { get; set; }
            }

            public class Coverage
            {
                public int Corp_Id { get; set; }
                public int Contract_Id { get; set; }
                public int Company_Facultative_Id { get; set; }
                public string Contract_Name { get; set; }
                public decimal Contract_Amount { get; set; }
                public decimal Contract_Commission_Percentage { get; set; }
                public DateTime PaymentDate { get; set; }
                public int Contract_Coverage_Id { get; set; }
                public int Region_Id { get; set; }
                public int Country_Id { get; set; }
                public int Bl_Type_Id { get; set; }
                public int Bl_Id { get; set; }
                public int Product_Id { get; set; }
                public int Vehicle_Type_Id { get; set; }
                public int Group_Id { get; set; }
                public int Coverage_Type_Id { get; set; }
                public int Coverage_Id { get; set; }
                public decimal Contract_Coverage_Percentage { get; set; }
                public int Contract_Coverage_Status { get; set; }
            }
        }

        public class PSourceId
        {
            public string PolicyNo { get; set; }
            public string SourceId { get; set; }
            public int UserId { get; set; }
        }

        public class NBParameter
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int AgentId { get; set; }
            public int LanguageId { get; set; }
        }

        public struct OverPricePercentage
        {
            public int RatingTypeId { get; set; }
            public string MinValue { get; set; }
            public string MaxValue { get; set; }
            public bool MinValueIsNumeric { get; set; }
            public bool MaxValueIsNumeric { get; set; }
        }

        public struct RiskRatingCondition
        {
            public int CorpId { get; set; }
            public int RiskGroupId { get; set; }
            public int RiskDetId { get; set; }
            public int PageId { get; set; }
            public int GridId { get; set; }
            public int ElementId { get; set; }
            public int ColumnId { get; set; }
            public int RiskTypeId { get; set; }
        }


        public class RiskRating
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
            public int ContactId { get; set; }
            public int ContactRoleTypeId { get; set; }
            public int OperationId { get; set; }
            public int? RiskId { get; set; }
            public string SequenceReference { get; set; }
            public int? ClassificationId { get; set; }
            public string SuggestedRating { get; set; }
            public decimal? TableRating { get; set; }
            public decimal? PerThousandRating { get; set; }
            public DateTime? StartDate { get; set; }
            public decimal? Duration { get; set; }
            public DateTime? NotificationDate { get; set; }
            public int? RequestedBy { get; set; }
            public DateTime? EndDate { get; set; }
            public int? RiskRateStatusId { get; set; }
            public decimal? YearToReconsider { get; set; }
            public DateTime? ReconsiderDate { get; set; }
            public int? RiskTypeId { get; set; }
            public int? RiskGroupId { get; set; }
            public int? RiskDetId { get; set; }
            public int? PageId { get; set; }
            public int? GridId { get; set; }
            public int? ElementId { get; set; }
            public int? ColumnId { get; set; }
            public bool? RiskSelectionStatus { get; set; }
            public string Comment { get; set; }
            public int? RiderTypeId { get; set; }
            public int? RiderId { get; set; }
            public string RyderTypeDesc { get; set; }
            public string ReasonDesc { get; set; }
            public string StatusDesc { get; set; }
            public int? CreditReasonId { get; set; }
            public int? ExclusionTypeId { get; set; }
            public int? ExclusionId { get; set; }
            public int? CreditTypeId { get; set; }
            public string CreditTypeDesc { get; set; }
            public int? CreditId { get; set; }
            public string CreditDesc { get; set; }
            public string CreditReasonDesc { get; set; }
            public string ExclusionTypeDesc { get; set; }
            public string ExclusionDesc { get; set; }
            public string RequestedByName { get; set; }
            public int? DocTypeId { get; set; }
            public int? DocCategoryId { get; set; }
            public int? DocumentId { get; set; }
            public string UnderwriterName { get; set; }
            public byte[] DocumentBinary { get; set; }
            public string DocumentName { get; set; }
            public string CategoryDesc { get; set; }
            public string ConditionTypeDesc { get; set; }
            public string RiskTypeDesc { get; set; }
            public int UserId { get; set; }
        }

        public class Form
        {
            public int FormId { get; set; }
            public int FormCategoryId { get; set; }
            public int TemplateId { get; set; }
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticRegId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int ContactId { get; set; }
            public string FormDesc { get; set; }
            public string FormCatDesc { get; set; }
            public string HtmlTemplate { get; set; }
            public string PDFTemplatePath { get; set; }
            public DateTime CreateDate { get; set; }
        }

        public class BackgroundCheck
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
            public int ContactId { get; set; }
            public int? BackgroundCheckId { get; set; }
            public string Reason { get; set; }
            public bool? Results { get; set; }
            public DateTime Date { get; set; }
            public string Comments { get; set; }
            public IEnumerable<Step.ExtraInfo> ExtraInfoList { get; set; }
            public int UserId { get; set; }
        }

        public class StatusChange
        {
            public int Code { get; set; }
            public string Message { get; set; }
        }

        public class VehiclesCoverage
        {
            public string PolicyNo { get; set; }
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticRegId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int InsuredVehicleId { get; set; }
            public string MakeDesc { get; set; }
            public string ModelDesc { get; set; }
            public int? GroupId { get; set; }
            public int? CoverageId { get; set; }
            public int? CoverageTypeId { get; set; }
            public int? Oroductid { get; set; }
            public int? BlTypeId { get; set; }
            public int? BlId { get; set; }
            public int? YearVehicle { get; set; }
            public string PrincipalUse { get; set; }
            public string Parking { get; set; }
            public decimal InsuredAmount { get; set; }
            public decimal Deductible { get; set; }
            public decimal PremiumAmount { get; set; }
            public string Driver { get; set; }
            public string ProductTypeNameKey { get; set; }
            public string PlanName { get; set; }
            public long? VehicleUniqueId { get; set; }
            public int? VehicleTypeId { get; set; }
            public string Registry { get; set; }
            public decimal? JuditialSecurity { get; set; }
            public string Chassis { get; set; }
            public DateTime? InsuredDate { get; set; }
            public DateTime? EndDate { get; set; }
            public bool? HasDriverHouse { get; set; }
            public bool? HasRoadsideAssistance { get; set; }
            public int? ConditionedDocTypeId { get; set; }
            public int? ConditionedDocCategoryId { get; set; }
            public int? ConditionedDocumentId { get; set; }
            public string VehicleTypeDesc { get; set; }
            public string Cilindres { get; set; }
            public int? PassengersNo { get; set; }
            public string Closure { get; set; }
            public decimal? MovementPremiumAmount { get; set; }
            public bool? Inspection { get; set; }
            public bool? New { get; set; }
            public decimal? EndorsementAmount { get; set; }
            public string EndorsementBeneficiary { get; set; }
            public string EndorsementBeneficiaryRnc { get; set; }
            public bool? EndorsementClarifying { get; set; }
            public bool HasOwnDamage { get; set; }
            public string VehicleDesc { get; set; }
            public string TipoDescuento { get; set; }
            public string PorcentajeDescuento { get; set; }
            public decimal Descuento { get; set; }
            public decimal MontoDescuento { get; set; }
            public int DiscountId { get; set; }
        }

        public class CRBSParameter
        {
            public int? ContactId { get; set; }
            public int? ContactRoleTypeId { get; set; }
            public int? BussinessLineId { get; set; }
            public int? PolicyStatusId { get; set; }
            public string GetHistorical { get; set; }
        }

        public class CRBS
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
            public int? BussinessLineId { get; set; }
            public int ContactId { get; set; }
            public string PolicyNo { get; set; }
            public decimal? PeriodicPremium { get; set; }
            public DateTime? EffectiveDate { get; set; }
            public DateTime? ExpirationDate { get; set; }
            public DateTime? CreateDate { get; set; }
            public int PolicyStatusId { get; set; }
            public string PolicyStatusDesc { get; set; }
            public string PolicyNameKey { get; set; }
            public string OfficeDesc { get; set; }
            public string CompanyDesc { get; set; }
            public string ProductDesc { get; set; }
            public string ProductTypeDesc { get; set; }
            public string BusinessLineDesc { get; set; }
            //public bool? IsHistorical { get; set; }
        }

        public class Vehicle
        {
            public class AccidentRate
            {
                public string CARBRANDNAME { get; set; }
                public string CARMODELNAME { get; set; }
                public int POLICYITEMCARYEAR { get; set; }
                public Nullable<decimal> SINIESTRALIDAD { get; set; }
                public Nullable<decimal> FRECUENCIA { get; set; }
                public Nullable<int> CANTIDADVEHICULOS { get; set; }
                public Nullable<decimal> LIQUIDACION { get; set; }
            }

            public class Discount
            {
                public int InsuredVehicleId { get; set; }
                public int DiscountId { get; set; }
                public int DiscountRuleId { get; set; }
                public int DiscountRuleDetailId { get; set; }
                public decimal OldPremiumAmount { get; set; }
                public int CorpId { get; set; }
                public int RegionId { get; set; }
                public int CountryId { get; set; }
                public int DomesticregId { get; set; }
                public int StateProvId { get; set; }
                public int CityId { get; set; }
                public int OfficeId { get; set; }
                public int CaseSeqNo { get; set; }
                public int HistSeqNo { get; set; }
                public string DetailRuleValue { get; set; }
                public int UserId { get; set; }
                public int? NotePredefiniedId { get; set; }

                public class RulesAndDetails
                {
                    public int CorpId { get; set; }
                    public int DiscountRuleId { get; set; }
                    public string DiscountRuleDesc { get; set; }
                    public string NameKey { get; set; }
                    public bool Active { get; set; }
                    public int DetailId { get; set; }
                    public DateTime DetailApplyDate { get; set; }
                    public string DetailRuleValue { get; set; }
                    public string DetailNameKey { get; set; }
                    public bool DetailActive { get; set; }
                }
            }

            public class Requirement
            {
                public int OrderId { get; set; }
                public int CorpId { get; set; }
                public int? RequirementDocId { get; set; }
                public int RequirementCatId { get; set; }
                public int? RequirementId { get; set; }
                public string RequirementCatDesc { get; set; }
                public int? RequirementTypeId { get; set; }
                public string RequirementTypeDesc { get; set; }
                public bool? Automatic { get; set; }
                public bool? RequimentPolicyOnly { get; set; }
                public bool? RequimentAssingToInsured { get; set; }
                public int? RegionId { get; set; }
                public int? CountryId { get; set; }
                public int? DomesticregId { get; set; }
                public int? StateProvId { get; set; }
                public int? CityId { get; set; }
                public int? OfficeId { get; set; }
                public int? CaseSeqNo { get; set; }
                public int? HistSeqNo { get; set; }
                public int? ContactId { get; set; }
                public int? DocTypeId { get; set; }
                public int? DocCategoryId { get; set; }
                public int? DocumentId { get; set; }
                public bool? IsValid { get; set; }
                public int? FunctionalitySeqNo { get; set; }
                public bool? IsMandatory { get; set; }
                public int? InsuredVehicleId { get; set; }
                public long? VehicleUniqueId { get; set; }
                public string AssingTo { get; set; }
                public int? FunctionalityId { get; set; }
                public int? UploadById { get; set; }
                public string UploadByUserName { get; set; }
                public int? ValidById { get; set; }
                public string ValidByUserName { get; set; }
                public DateTime? ValidByDate { get; set; }
                public string RequimentOnBaseNameKey { get; set; }
                public string RequirementTypeSubType { get; set; }
                public string EndorsementBeneficiary { get; set; }
                public string DocumentName { get; set; }
            }

            [Serializable]
            public class Detail
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
                public long VehicleUniqueId { get; set; }
                public int? ModelId { get; set; }
                public int? MakeId { get; set; }
                public string MakeDesc { get; set; }
                public string ModelDesc { get; set; }
                public string Registry { get; set; }
                public string Chassis { get; set; }
                public int ColorId { get; set; }
                public string ColorDesc { get; set; }
                public int Year { get; set; }
                public string ExpirationDate { get; set; }
                public string PlanName { get; set; }
                public DateTime? FromVigencia { get; set; }
                public string PolicyNo { get; set; }
                public int? PolicyStatusId { get; set; }
                public string PolicyStatusDesc { get; set; }
                public int? ProductId { get; set; }
                public string ProductDesc { get; set; }
                public decimal? PremiumAmount { get; set; }
                public long? R { get; set; }
                public int Contactid { get; set; }
                public int? InsuredVehicleId { get; set; }
                public string InspectionAddress { get; set; }
                public string FuelTypeDesc { get; set; }
            }
        }

        public class DVParameter
        {
            public int? InsuredVehicleId { get; set; }
            public int? DiscountId { get; set; }
            public int? DiscountRuleId { get; set; }
            public int? DiscountRuleDetailId { get; set; }
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int Language { get; set; }
        }

        public class DParameter
        {
            public string NameKey { get; set; }
            public int? DiscountRuleId { get; set; }
            public bool? Active { get; set; }
            public int? CorpId { get; set; }
            public string Role { get; set; }
        }

        public class Agent
        {
            public int CorpId { get; set; }
            public int AgentId { get; set; }

            public class SaleChannelInfo
            {
                public int CorpId { get; set; }
                public int? RegionId { get; set; }
                public int? CountryId { get; set; }
                public int? BlTypeId { get; set; }
                public int? BlId { get; set; }
                public int AgentId { get; set; }
                public int DistributionId { get; set; }
                public string DistributionDesc { get; set; }
                public string BlDesc { get; set; }
                public string ChainLevelDesc { get; set; }
                public int? OrderNumber { get; set; }
            }

            public class AgentSupervisor
            {
                public int CorpId { get; set; }
                public int ChainId { get; set; }
                public int ChainDetId { get; set; }
                public int AgentId { get; set; }
                public int OrderId { get; set; }
                public int ChainLevelId { get; set; }
                public bool AgentChainStatus { get; set; }
                public Nullable<int> RelationshipToSupervisor { get; set; }
                public Nullable<int> SupervisorAgentId { get; set; }
                public string SupervisorAgentCode { get; set; }
                public string SupervisporFullName { get; set; }
            }
        }


        public class VehicleCoverageSurcharge
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int Language { get; set; }
            public long VehicleUniqueId { get; set; }
            public int BlTypeId { get; set; }
            public int BlId { get; set; }
            public int ProductId { get; set; }
            public int VehicleTypeId { get; set; }
            public int GroupId { get; set; }
            public int CoverageTypeId { get; set; }
            public int CoverageId { get; set; }
            public int? SurchargeId { get; set; }
            public int? DiscountRuleId { get; set; }
            public int? DiscountRuleDetailId { get; set; }
            public decimal? OldCoverageAmount { get; set; }
            public string DetailRuleValue { get; set; }
            public int? NotePredefiniedId { get; set; }
            public string DiscountRuleDesc { get; set; }
            public string DiscountRuleNameKey { get; set; }
            public DateTime DetailApplyDate { get; set; }
            public string DetailRuleNameKey { get; set; }
            public string NotePredefiniedDesc { get; set; }
            public string NoteNameKey { get; set; }
            public decimal BasePremiumAmount { get; set; }

            public int UserId { get; set; }

            public string TipoRecargo { get; set; }
            public string PorcentajeRecargo { get; set; }
            public decimal Recargo { get; set; }
            public decimal MontoRecargo { get; set; }

            public string RecargoF { get; set; }
            public string MontoRecargoF { get; set; }


            public string VehicleDesc { get; set; }
            public string Registry { get; set; }
        }

        public class Document
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
            public int DocumentTypeId { get; set; }
            public int DocumentCategoryId { get; set; }
            public int DocumentId { get; set; }
            public bool DocumentStatusId { get; set; }
            public int UserId { get; set; }
            public string DocumentName { get; set; }
        }

        public class Number
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
            public string PolicyNo { get; set; }
            public int? UserId { get; set; }
        }

        public class DocumentQuotation
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
            public int DocTypeId { get; set; }
            public int DocCategoryId { get; set; }
            public int DocumentId { get; set; }
            public int ProjectId { get; set; }
            public int TabId { get; set; }
            public int FunctionalityId { get; set; }
            public int? FunctionalitySeqNo { get; set; }
            public string NameDesc { get; set; }
            public string NameKey { get; set; }
            public string TabDesc { get; set; }
            public DateTime LastUpdate { get; set; }
            public bool IsReviewed { get; set; }
        }

        public class Quo
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
            public string SupervisorAgentName { get; set; }
            public string AgentName { get; set; }
            public int ContactId { get; set; }
            public string FullName { get; set; }
            public string IdContact { get; set; }
            public decimal? InsuredAmount { get; set; }
            public decimal? AnnualPremium { get; set; }
            public decimal? InitialContribution { get; set; }
            public DateTime QuoDate { get; set; }
            public DateTime? InspectionQuoDate { get; set; }
            public int DocumentMissing { get; set; }
            public int SubscriberAgentId { get; set; }
            public string SubscriberName { get; set; }
            public int InspectorAgentId { get; set; }
            public string InspectorName { get; set; }
            public int Days { get; set; }
            public bool IsExpiring { get; set; }
            public bool IsExpired { get; set; }
            public string PolicyNoTemp { get; set; }
            public string Tab { get; set; }
            public bool HasDiscount { get; set; }
            public bool? IsPolicy { get; set; }
            public DateTime? EffectiveDate { get; set; }
            public DateTime? ExpirationDate { get; set; }
            public string TipoRiesgoNameKey { get; set; }
            public DateTime? DeclinedQuoDate { get; set; }
            public bool HasSurcharge { get; set; }
            public DateTime? PolicyExpirationDate { get; set; }
            public bool? BlacklistCheck { get; set; }
            public int? BlacklistCheckUser { get; set; }
            public string BlacklistCheckUserName { get; set; }
            public string BlacklistMember { get; set; }
            public decimal? MonthlyPayment { get; set; }
            public bool? Financed { get; set; }
            public int? Period { get; set; }
            public string LoanPetitionNo { get; set; }
            public decimal? TaxPercentage { get; set; }
            public bool? DirectDebit { get; set; }
            public bool? DomicileInitialPayment { get; set; }
            public Nullable<int> RequestTypeId { get; set; }
            public string RequestTypeDesc { get; set; }
            public Nullable<decimal> ProratedPremium { get; set; }
            public string RiskLevel { get; set; }
            public decimal VendorAccidentRate { get; set; }
            public decimal AgentAccidentRate { get; set; }

            public class Temp
            {
                public string PolicyNo { get; set; }
                public int UserId { get; set; }
                public bool ReturnResultSet { get; set; }
                public int? ContactId { get; set; }

                public class TempResult
                {
                    public string Action { get; set; }
                    public string Tab { get; set; }
                }

            }
        }

        public class LogParameter
        {
            public int LogTypeId { get; set; }
            public int? CorpId { get; set; }
            public int? CompanyId { get; set; }
            public int? ProjectId { get; set; }
            public Guid? Identifier { get; set; }
            public string LogValue { get; set; }

        }

        public class LogResult
        {
            public int Code { get; set; }
            public string Message { get; set; }
        }

        public class OEffectiveDate
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
            public DateTime EffectiveDate { get; set; }
            public int UserId { get; set; }
        }

        public class OExpirationDate
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
            public DateTime ExpirationDate { get; set; }
            public int UserId { get; set; }
        }

        public class UQuo
        {
            public int? CorpId { get; set; }
            public int? RegionId { get; set; }
            public int? CountryId { get; set; }
            public int? DomesticregId { get; set; }
            public int? StateProvId { get; set; }
            public int? CityId { get; set; }
            public int? OfficeId { get; set; }
            public int? CaseSeqNo { get; set; }
            public int? HistSeqNo { get; set; }
            public int? BussinessLineType { get; set; }
            public int? BussinessLineId { get; set; }
            public int? CompanyId { get; set; }
            public string CompanyDesc { get; set; }
            public string PolicyNo { get; set; }
            public string BlDesc { get; set; }
            public string ProductTypeDesc { get; set; }
            public string GroupDesc { get; set; }
            public string OfficeDesc { get; set; }
            public int? AgentOfficeId { get; set; }
            public int? AgentId { get; set; }
            public string DistributionDesc { get; set; }
            public int? PolicyStatusId { get; set; }
            public string PolicyStatusDesc { get; set; }
            public string PolicyStatusNameKey { get; set; }
            public string AgentName { get; set; }
            public int? SupervisorAgentId { get; set; }
            public string SupervisorAgentName { get; set; }
            public int? ContactId { get; set; }
            public string FullName { get; set; }
            public string TipoRiesgoNameKey { get; set; }
            public string IdContact { get; set; }
            public decimal? InsuredAmount { get; set; }
            public decimal? AnnualPremium { get; set; }
            public decimal? InitialContribution { get; set; }
            public DateTime? QuoDate { get; set; }
            public DateTime? QuoPosDate { get; set; }
            public DateTime? EffectiveDate { get; set; }
            public DateTime? ExpirationDate { get; set; }
            public DateTime? InspectionQuoDate { get; set; }
            public DateTime? DeclinedQuoDate { get; set; }
            public string DeclinedQuoReason { get; set; }
            public string MissingDocumentQuoReason { get; set; }
            public int? WorkMinute { get; set; }
            public int? SubscriptionMinute { get; set; }
            public int? InspectionMinute { get; set; }
            public int? DocumentMissing { get; set; }
            public int? SubscriberAgentId { get; set; }
            public string SubscriberName { get; set; }
            public int? InspectorAgentId { get; set; }
            public string InspectorName { get; set; }
            public int? Days { get; set; }
            public bool? IsExpiring { get; set; }
            public bool? IsExpired { get; set; }
            public string PolicyNoTemp { get; set; }
            public decimal? DiscountAmount { get; set; }
            public bool? HasDiscount { get; set; }
            public bool? HasSurcharge { get; set; }
            public bool? HasEndorsement { get; set; }
            public bool? HasEndorsementClarifying { get; set; }
            public int? VehicleCount { get; set; }
            public string Tab { get; set; }
            public int UserId { get; set; }
        }

        public class ReinsuranceFacultative
        {
            public int? CorpId { get; set; }
            public int? RegionId { get; set; }
            public int? CountryId { get; set; }
            public int? DomesticregId { get; set; }
            public int? StateProvId { get; set; }
            public int? CityId { get; set; }
            public int? OfficeId { get; set; }
            public int? CaseSeqNo { get; set; }
            public int? HistSeqNo { get; set; }
            public int? RiderTypeId { get; set; }
            public int? RiderId { get; set; }
            public string CoverageTypeDesc { get; set; }
            public decimal? BeneficiaryAmount { get; set; }
            public DateTime? RequestedDate { get; set; }
            public DateTime? ProcessedDate { get; set; }
            public decimal? CompanyRiskAmount { get; set; }
            public decimal? ReinsuranceRiskAmount { get; set; }
            public decimal? AuthorizedAmount { get; set; }
            public string RiskRatingTable { get; set; }
            public decimal? RiskRatingAmount { get; set; }
            public decimal? PerThousendRiskAmount { get; set; }
            public string FacultativeReinsuranceId { get; set; }
            public int? FacultativeStatusId { get; set; }
            public bool? ReinsuranceFacultativeStatus { get; set; }
            public int? UserId { get; set; }
            public string SourceID { get; set; }
        }

        public class TabRol
        {
            public string TabName { get; set; }
            public bool Visible { get; set; }
            public string TabGroupDesc { get; set; }
        }

        public struct BackGroundCheckLink
        {

            public string Operation { get; set; }
            public int Corp_Id { get; set; }
            public int Region_Id { get; set; }
            public int Country_Id { get; set; }
            public int Domesticreg_Id { get; set; }
            public int State_Prov_Id { get; set; }
            public int City_Id { get; set; }
            public int Office_Id { get; set; }
            public int Case_Seq_No { get; set; }
            public int Hist_Seq_No { get; set; }
            public int Contact_Id { get; set; }
            public int Link_Id { get; set; }
            public string Link_Url { get; set; }
            public bool Link_Status { get; set; }
            public byte[] Link_Image { get; set; }
            public bool Matched { get; set; }
            public string UserName { get; set; }
            public int userid { get; set; }
            public string Match_Status_Img { get; set; }
            public DateTime Create_Date { get; set; }

        }

        public class CouponInfo
        {
            public string Policy_Number { get; set; }
            public string CouponCode { get; set; }
            public Nullable<decimal> CouponPercentageDiscount { get; set; }
            public Nullable<int> CouponProspectId { get; set; }
        }
    }
}
