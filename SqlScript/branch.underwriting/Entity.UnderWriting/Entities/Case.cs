﻿using System;

namespace Entity.UnderWriting.Entities
{
    public class Case
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
        public int PolicyStatusId { get; set; }
        public int ContactId { get; set; }
        public int? AddInsuredContactId { get; set; }
        public int? ProductId { get; set; }
        public int? PolicySerieId { get; set; }
        public int CompanyId { get; set; }
        public string PolicyNo { get; set; }
        public string PolicyStatusDesc { get; set; }
        public string SerieCode { get; set; }
        public string SerieDesc { get; set; }
        public string ProductTypeDesc { get; set; }
        public string ProductDesc { get; set; }
        public string FirstNameInsured { get; set; }
        public string MiddleNameInsured { get; set; }
        public string FirstLastNameInsured { get; set; }
        public string SecondLastNameInsured { get; set; }
        public string FullNameInsured { get; set; }
        public decimal? BenefitAmount { get; set; }
        public string GlobalCountryDesc { get; set; }
        public string OfficeDesc { get; set; }
        public string CompanyDesc { get; set; }
        public string AssigedTo { get; set; }
        public bool Priority { get; set; }
        public DateTime? SubmitDate { get; set; }
        public string FirstNameAgent { get; set; }
        public string MiddleNameAgent { get; set; }
        public string FirstLastNameAgent { get; set; }
        public string SecondLastNameAgent { get; set; }
        public string FullNameAgent { get; set; }
        public int? Days { get; set; }
        public string Steps { get; set; }
        public string StepInAwaiting { get; set; }
        public int? TimeInAwaiting { get; set; }
        public string RyderTypeDesc { get; set; }
        public decimal? ReinsuredAmount { get; set; }
        public string Reinsurer { get; set; }
        public DateTime? DateSentToReinsurance { get; set; }
        public int? TimeInReinsurance { get; set; }
        public string ExceptionTypeDesc { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string UserAuditTrail { get; set; }
        public DateTime? RequestedDate { get; set; }
        public int? RequestedBy { get; set; }
        public string RequestedByName { get; set; }
        public string StepDesc { get; set; }
        public string StepTypeDesc { get; set; }
        public int? SubmittedDays { get; set; }
        public int ContactRoleTypeId { get; set; }
        public bool OwnerIsInsured { get; set; }
        public int? InsuredPeriod { get; set; }
        public int? ProjectId { get; set; }
        public int UserId { get; set; }
        public int? StatusChangeTypeId { get; set; }
        public string Policy_No_Temp { get; set; }


        public class NewCase
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int? ContactId { get; set; }
            public int AgentId { get; set; }
            public int ContactTypeId { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string FirstLastName { get; set; }
            public string SecondLastName { get; set; }
            public string Nickname { get; set; }
            public string InstitutionalName { get; set; }
            public int? InstitutionalCountryId { get; set; }
            public DateTime? Dob { get; set; }
            public int? Age { get; set; }
            public int? NearAge { get; set; }
            public string Gender { get; set; }
            public int? MaritalStatId { get; set; }
            public int? DirectoryId { get; set; }
            public int? RegionOfResidenceId { get; set; }
            public int? CountryOfResidenceId { get; set; }
            public int? DomesticRegOfResidenceId { get; set; }
            public int? StateOfResidenceId { get; set; }
            public int? CityOfResidenceId { get; set; }
            public int? RegionOfBirthId { get; set; }
            public int? CountryOfBirthId { get; set; }
            public decimal? Weigth { get; set; }
            public int? WeigthTypeId { get; set; }
            public string Height { get; set; }
            public int? HeigthTypeId { get; set; }
            public string LineOfBusiness { get; set; }
            public string LineOfBusiness2 { get; set; }
            public string CompanyName { get; set; }
            public int? LengthWorkYear { get; set; }
            public int? LengthWorkMonth { get; set; }
            public string LaborTasks { get; set; }
            public string CompanyActivity { get; set; }
            public DateTime? CompanyFoundationDate { get; set; }
            public int? OccupGroupTypeId { get; set; }
            public int? OccupationId { get; set; }
            public int? RelationshiptoAgent { get; set; }
            public int? RelationshiptoOwner { get; set; }
            public decimal? AnnualFamilyIncome { get; set; }
            public decimal? AnnualPersonalIncome { get; set; }
            public bool? Smoker { get; set; }
            public int? ContactStatusId { get; set; }
            public string NameId { get; set; }
            public int UserId { get; set; }
            public int? ReferredByRelationshipId { get; set; }
            public int? ReferredByContactId { get; set; }
            public bool IsCompany { get; set; }
            public bool IsIllustration { get; set; }
            public string IllustrationNo { get; set; }
            public int? NcfTypeId { get; set; }
            public string AssignRol { get; set; }//Agregado
            public bool? isGroupIllustration { get; set; }
        }

        public struct Process
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
            public int? OwnerContactId { get; set; }
            public int? InsuredContactId { get; set; }
            public int? AgentLegalContactId { get; set; }
            public int? AddInsuredContactId { get; set; }
            public int? DesignatedPensionerContactId { get; set; }
            public int? StudentNameContactId { get; set; }
            public int? AgentId { get; set; }
            public int? BussinessLineType { get; set; }
            public int? BussinessLineId { get; set; }
            public int? ProductId { get; set; }
            public DateTime? LastUpdate { get; set; }
            public int? UserId { get; set; }
            public string ProductDesc { get; set; }
            public string OwnerFullName { get; set; }
            public string InsuranceFullName { get; set; }
            public string AgentFullName { get; set; }
            public string UserFullName { get; set; }
            public bool HasComment { get; set; }
            public int? PaymentId { get; set; }
            public bool CanGoRequirement { get; set; }
            public bool IsPaymentCompleted { get; set; }
            public int? DaysLate { get; set; }
            public string Status { get; set; }
            public DateTime? EffectiveDate { get; set; }

            public int? AmmendmentId { get; set; }
            public int? RequirementContactId { get; set; }
            public int? RequirementCatId { get; set; }
            public int? RequirementTypeId { get; set; }
            public int? RequirementId { get; set; }
            public bool? IsAmmendReq { get; set; }
            public DateTime? AmmendmentDate { get; set; }

            public string OfficeDesc { get; set; }
            public string Exception { get; set; }
            public string CountryDesc { get; set; }

            public string ProductNameKey { get; set; }

            public int ProductTypeId { get; set; }
            public string ProductTypeDesc { get; set; }
        }

        public struct CaseResult
        {
            public bool Result { get; set; }
            public string ResultMessage { get; set; }
        }

        public class Comment
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
            public int CommentId { get; set; }
            public int? StepTypeId { get; set; }
            public int? StepId { get; set; }
            public int? StepCaseNo { get; set; }
            public DateTime CommentDate { get; set; }
            public string Comments { get; set; }
            public string UserName { get; set; }
            public int UserId { get; set; }
        }

        public class SearchResult
        {
            public int CompanyId { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public int? BlTypeId { get; set; }
            public int? BlId { get; set; }
            public int? ProductId { get; set; }
            public string PolicyNo { get; set; }
            public string ContactFullName { get; set; }
            public int? CorpId { get; set; }
            public int? RegionId { get; set; }
            public int? CountryId { get; set; }
            public int? DomesticregId { get; set; }
            public int? StateProvId { get; set; }
            public int? CityId { get; set; }
            public int? OfficeId { get; set; }
            public int? AgentIdManager { get; set; }
            public int? UnderwriterId { get; set; }
            public int? AgentIdSubManager { get; set; }
            public int? AgentId { get; set; }
        }

        public struct TabCount
        {
            public int Order { get; set; }
            public string TabName { get; set; }
            public int Count { get; set; }
        }

        public struct Queue
        {
            public string TabName { get; set; }
            public int OnTime { get; set; }
            public int Delayed { get; set; }
        }

        public class AssignCase
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
            public int UnderwriterId { get; set; }
            public int? AssignCaseId { get; set; }
            public string AssignRol { get; set; }
            public int UserId { get; set; }
        }
    }
}
