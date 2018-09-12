﻿using System;

namespace WEB.NewBusiness.Common.Illustration
{
    [Serializable]
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
        public string PlanGroupCode { get; set; }
        public string Office { get; set; }
        public string IllustrationNo { get; set; }
        public string IllustrationStatusCode { get; set; }
        public string Status { get; set; }
        public string Identification { get; set; }
        public string InsuredName { get; set; }
        public string AgentName { get; set; }
        public string PlanType { get; set; }
        public string Frequency { get; set; }
        public string Channel { get; set; }
        public decimal? InsuredAmount { get; set; }
        public decimal? Deductible { get; set; }
        public decimal? TotalPremium { get; set; }
        public decimal? InitialPremium { get; set; }
        public string InsuredAmountF { get; set; }
        public string DeductibleF { get; set; }
        public string TotalPremiumF { get; set; }
        public string InitialPremiumF { get; set; }
        public DateTime? IllustrationDate { get; set; }
        public DateTime? InspectionQuoDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? NewStatusDate { get; set; }
        public DateTime? DeclinedQuoDate { get; set; }
        public DateTime? QuoDate { get; set; }
        public string SupervisorAgentName { get; set; }
        public string QuoCreateUserName { get; set; }
        public int QuoCreateUserId { get; set; }
        public int? MissingDocuments { get; set; }
        public int? AvailableDays { get; set; }
        public long InsuredId { get; set; }
        public bool Priority { get; set; }
        public int? AgentId { get; set; }
        public int? AssignedSubscriberId { get; set; }
        public string AssignedSubscriber { get; set; }
        public int? InspectorAgentId { get; set; }
        public string InspectorAgent { get; set; }
        public string IllustrationNoTemp { get; set; }
        public bool isExpired { get; set; }
        public bool IsExpiring { get; set; }
        public DateTime? QuoPosDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? WorkMinute { get; set; }
        public int? SubscriptionMinute { get; set; }
        public int? InspectionMinute { get; set; }
        public bool? HasDiscount { get; set; }
        public decimal? Discount { get; set; }
        public string DiscountF { get; set; }
        public bool isCompleted { get; set; }
        public string TipoRiesgoNameKey { get; set; }
        public string FinancialClearance { get; set; }
        public string DeclinedQuoReason { get; set; }
        public string MissingDocumentQuoReason { get; set; }
        public string ConfirmationCallerName { get; set; }
        public bool HasSurcharge { get; set; }
        public bool MakeDiscount { get; set; }
        public string TieneDescuento { get; set; }
        public string TieneRecargo { get; set; }
        public string ProductSubTypeDesc { get; set; }
        public bool DocumentRequiredEnabled { get; set; }
        public string DocumentRequiredCssClass { get; set; }
        public DateTime? PolicyExpirationDate { get; set; }
        public bool InspectionFormEnabled { get; set; }
        public string InspectionFormCssClass { get; set; }
        public string PolicyStatusNameKey { get; set; }
        public string PolicyNoMain { get; set; }
        public bool HasFacultative { get; set; }
        public int? RequestTypeId { get; set; }
        public string RequestTypeDesc { get; set; }
        public decimal? ProratedPremium { get; set; }
        public string Financed { get; set; }
        public string AgentPhones { get; set; }
        public string Rate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string ModelAccidentRate { get; set; }
        public string VendorAccidentRate { get; set; }
        public string AgentAccidentRate { get; set; }
        public string MinDeducCristales { get; set; }
        public string MinDeducDP { get; set; }
        public string PorcDeducDP { get; set; }

        public class Fake
        {
            public string FamilyProduct { get; set; }
            public string PlanType { get; set; }
            public string IllustrationNoTemp { get; set; }
            public string IllustrationNo { get; set; }
            public string Status { get; set; }
            public string Office { get; set; }
            public string Channel { get; set; }
            public string AgentName { get; set; }
            public string InsuredName { get; set; }
            public string Identification { get; set; }
            public string TotalPremiumF { get; set; }
            public string InitialPremiumF { get; set; }
            public string InsuredAmountF { get; set; }
            public string IllustrationDate { get; set; }
            public string ExpirationDate { get; set; }
            public string NewStatusDate { get; set; }
            public string AssignedSubscriber { get; set; }
            public string MissingDocuments { get; set; }
            public string AvailableDays { get; set; }
        }
    }
}