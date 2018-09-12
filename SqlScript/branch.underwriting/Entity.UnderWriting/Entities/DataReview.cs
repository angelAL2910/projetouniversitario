﻿using System;

namespace Entity.UnderWriting.Entities
{
    public class DataReview
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
        public int InsuredContactId { get; set; }
        public int AgentId { get; set; }
        public string CompanyDesc { get; set; }
        public string PolicyNo { get; set; }
        public string ProductDesc { get; set; }
        public string UserName { get; set; }
        public string InsuredFullName { get; set; }
        public string OwnerFullName { get; set; }
        public string CountryDesc { get; set; }
        public string OfficeDesc { get; set; }
        public string AgentFullName { get; set; }
        public string Exception { get; set; }
        public DateTime? SubmitDate { get; set; }
        public int? DaysPending { get; set; }
        public bool? IsReview { get; set; }
        public int? AddInsuredContactId { get; set; }
        public int? DesignatedPensionerContactId { get; set; }
        public int? StudentContactId { get; set; }
        public int? PaymentId { get; set; }
        public int OwnerContactId { get; set; }
        public int? AgentLegalContactId { get; set; }
        public int CommentCount { get; set; }
        public int? RelationshiptoAgent { get; set; }
        public int? RelationshiptoOwner { get; set; }
        public bool? isComplianceOK { get; set; } 

        public class DocumentToReview
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
            public int? ContactId { get; set; }
            public int? RequirementCatId { get; set; }
            public int? RequirementTypeId { get; set; }
            public int? RequirementId { get; set; }
            public int? RequirementDocId { get; set; }
            public int DocTypeId { get; set; }
            public int DocCategoryId { get; set; }
            public int DocumentId { get; set; }
            public string TabDesc { get; set; }
            public DateTime LastUpdate { get; set; }
            public bool IsReviewed { get; set; }
            public int? PaymentDetId { get; set; }
            public int ProjectId { get; set; }
            public int TabId { get; set; }
            public int AgentId { get; set; }
            public int FunctionalityId { get; set; }
            public int? FunctionalitySeqNo { get; set; }
            public string NameDesc { get; set; }
            public int UserId { get; set; }
            public int? PaymentId { get; set; }
            public int? OwnerContactId { get; set; }
            public int? InsuredContactId { get; set; }
            public int? AddInsuredContactId { get; set; }
            public int? DesignatedPensionerContactId { get; set; }
            public int? StudentContactId { get; set; }
            public string ProductNameKey { get; set; }              
        }

        public struct DocumentInfomation
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
            public int DocumentCategoryId { get; set; }
            public int DocumentTypeId { get; set; }
            public int DocumentId { get; set; }
            public string DocumentName { get; set; }
            public byte[] DocumentBinary { get; set; }
            public DateTime? FileCreationDate { get; set; }
            public string DocumentTypeDescription { get; set; }
            public string ContentType { get; set; }
            public string Extension { get; set; }
        }

        public struct Change
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
            public int InsuredContactId { get; set; }
            public int AgentId { get; set; }
            public string CompanyDesc { get; set; }
            public string PolicyNo { get; set; }
            public string ProductDesc { get; set; }
            public string UserName { get; set; }
            public string InsuredFullName { get; set; }
            public string CountryDesc { get; set; }
            public string OfficeDesc { get; set; }
            public string AgentFullName { get; set; }
            public string Exception { get; set; }
            public DateTime? SubmitDate { get; set; }
            public DateTime? ChangeDate { get; set; }
            public int TimeChange { get; set; }
            public int OwnerContactId { get; set; }
            public int? InsuredAddContactId { get; set; }
            public int? DesignatedPensionerContactId { get; set; }
            public int? StudentContactId { get; set; }
            public int? PaymentId { get; set; }
            public string ProductNameKey { get; set; }
        }

        public class DRCase
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
            public int CurrentContactId { get; set; }
            public int? NewContactId { get; set; }
            public int UserId { get; set; }
        }

        public struct DRCaseResult
        {                
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }



        [Serializable]
        public struct ContactMerge
        {
            public int ContactIdMergeOwner { get; set; }
            public int ContactId { get; set; }
            public int ContactTypeId { get; set; }            
            public string Roles { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string FirstLastName { get; set; }
            public string SecondLastName { get; set; }
            public DateTime? Dob { get; set; }
            public string CountryDesc { get; set; }
            public string Ids { get; set; }
            public string FullName { get; set; }
            public bool? IsCompany { get; set; }
            public double? MathName { get; set; }
            public bool? MathDob { get; set; }
            public bool? MathIds { get; set; }
            public int RowNumber { get; set; }
        }
    }
}
