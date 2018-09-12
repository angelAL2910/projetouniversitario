﻿using System;
using System.Collections.Generic;

namespace Entity.UnderWriting.Entities
{
    public class Contact
    {
        public class PepFormulary
        {
            public class PEPFormResult
            {
                public int ContactId { get; set; }
                public int PepFormularyId { get; set; }
                public string Name { get; set; }
                public int? RelationshipId { get; set; }
                public string Position { get; set; }
                public int? FromYear { get; set; }
                public int? ToYear { get; set; }
                public int? BeneficiaryId { get; set; }
                public bool? IsPepManagerCompany { get; set; }
                public int PepFormularyOptionsId { get; set; }
            }

            public string Action { get; set; }
            public int? ContactId { get; set; }
            public int? PepFormularyId { get; set; }
        }

        public class FinalBeneficiary
        {
            public class FinalBenResult
            {
                public int ContactId { get; set; }
                public int FinalBeneficiaryId { get; set; }
                public string Name { get; set; }
                public decimal? PercentageParticipation { get; set; }
                public Nullable<bool> IsPEP { get; set; }
                public Nullable<int> PepFormularyOptionsId { get; set; }
                public Nullable<int> IdentificationTypeId { get; set; }
                public string IdentificationNumber { get; set; }
                public Nullable<int> NationalityCountryId { get; set; }
                public string ContactIdTypeDesc { get; set; }
                public string GlobalCountryDesc { get; set; }

            }

            public string Action { get; set; }
            public int? ContactId { get; set; }
            public int? FinalBeneficiaryId { get; set; }
        }

        public int Agent_Legal { get; set; }
        public int? CorpId { get; set; }
        public int? AgentId { get; set; }
        public int ContactId { get; set; }
        public int? Age { get; set; }
        public int? NearAge { get; set; }
        public string Gender { get; set; }
        public string GenderDesc { get; set; }
        public int? MaritalStatId { get; set; }
        public int? RegionOfResidenceId { get; set; }
        public int? CountryOfResidenceId { get; set; }
        public int? DomesticRegOfResidenceId { get; set; }
        public int? StateOfResidenceId { get; set; }
        public int? CityOfResidenceId { get; set; }
        public int? MunicipioOfResidenceId { get; set; }
        public int? RegionOfBirthId { get; set; }
        public int? CountryOfBirthId { get; set; }
        public decimal? Weight { get; set; }
        public int? WeightTypeId { get; set; }
        public string Height { get; set; }
        public int? HeightTypeId { get; set; }
        public int? BloodTypeId { get; set; }
        public bool? Smoker { get; set; }
        public string LineOfBusiness { get; set; }
        public string LineOfBusiness2 { get; set; }
        public string LaborTasks { get; set; }
        public int? LengthWorkYear { get; set; }
        public int? LengthWorkMonth { get; set; }
        public int? StudentStatusId { get; set; }
        public int? RelationshiptoOwner { get; set; }
        public int? RelationshiptoAgent { get; set; }
        public decimal? AnnualPersonalIncome { get; set; }
        public decimal? AnnualFamilyIncome { get; set; }
        public int? OccupGroupTypeId { get; set; }
        public int? OccupationId { get; set; }
        public bool? BackgroundCheckResult { get; set; }
        public int? SeqNo { get; set; }
        public int? ContactIdType { get; set; }
        public int ContactTypeId { get; set; }
        public string Id { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int? CountryIssuedBy { get; set; }
        public string IssuedBy { get; set; }
        public int? DocumentId { get; set; }
        public int ContactRoleTypeId { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public int? InstitutionalCountryId { get; set; }
        public string InstitutionalName { get; set; }
        public string InstitutionalPrincipal { get; set; }
        public string InstitutionalPositionAtCompany { get; set; }
        public DateTime? Dob { get; set; }
        public string ContactTypeDesc { get; set; }
        public string CompanyName { get; set; }
        public string ContactTypeDescription { get; set; }
        public string DeceaseCause { get; set; }
        public DateTime? DeceaseDate { get; set; }
        public DateTime? NotifiedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Remarks { get; set; }
        public string Occupation_Desc { get; set; }
        public string Occupation_Group_Desc { get; set; }
        public int? finalBeneficiaryOptionId { get; set; }
        public int? pepFormularyOptionId { get; set; }
        public int? companyStructureId { get; set; }
        public int? companyActivityId { get; set; }
        public bool? FinalBeneficiaryIncludeForCompanyOrNot { get; set; }
        public bool? FinalBeneficiaryAllowed { get; set; }
        public bool? PepFormularyAllowed { get; set; }
        public string SourceId { get; set; }
        public string Representative { get; set; }
        public int? RepresentativeIdentificationTypeId { get; set; }
        public string RepresentativeIdentification { get; set; }
        public int UbicacionId { get; set; }
        public bool? homeOwner { get; set; }
        public int? qtyPersonsDepend { get; set; }
        public int? qtyEmployees { get; set; }
        public string linked { get; set; }
        public string segment { get; set; }
        public string InvoiceTypeDesc { get; set; }
        public int? CreditCardTypeId { get; set; }
        public string CreditCardNumberKey { get; set; }
        public string CreditCardNumber { get; set; }
        public int? ExpirationDateYear { get; set; }
        public int? ExpirationDateMonth { get; set; }
        public string CardHolder { get; set; }
        public string CreditCardMask { get; set; }
        public string KcoUniqueId { get; set; }

        public string WorkAddress { get; set; }
        public string PlaceOfBirth { get; set; }
        public int? TypeOfPerson { get; set; }
        public string ManagerName { get; set; }
        public int? ManagerPepOptionId { get; set; }

        /// <summary>
        /// Aca se va a guardar el NIT de la compañia 
        /// </summary>
        public string DocumentCompany { get; set; }
        public int? LegalContactId { get; set; }

        //Deprecate
        [ObsoleteAttribute("This property is obsolete.", false)]
        public IEnumerable<Address> Addresses { get; set; }
        [ObsoleteAttribute("This property is obsolete.", false)]
        public IEnumerable<Phone> Phones { get; set; }
        [ObsoleteAttribute("This property is obsolete.", false)]
        public IEnumerable<Email> Emails { get; set; }
        [ObsoleteAttribute("This property is obsolete.", false)]
        public IEnumerable<Citizenship> Citizenships { get; set; }
        [ObsoleteAttribute("This property is obsolete.", false)]
        public IEnumerable<CitizenQuestion> CitizenQuestions { get; set; }
        //Deprecate
        public IEnumerable<SocialExposure> SocialExposures { get; set; }
        public IEnumerable<SocialExposureRelationship> SocialExposureRelationships { get; set; }
        public int ModifyUser { get; set; }
        public int CreateUser { get; set; }

        public int? ReferredByRelationshipId { get; set; }
        public int? ReferredByContactId { get; set; }
        public string MaritalStatusDesc { get; set; }
        public string Identification { get; set; }

        public PolicyContact PolicyInfo { get; set; }
        public bool IsCompany { get; set; }
        public string RelationshiptoOwnerDesc { get; set; }

        public string CountryOfResidenceDesc { get; set; }
        public string CountryOfBirthDesc { get; set; }

        public string CustomerNumber { get; set; }

        public int? NcfTypeId { get; set; }

        public string TipoRiesgoNameKey { get; set; }

        public int? InvoiceTypeId { get; set; }

        public int? ModiUsrId { get; set; }

        public int? ForeignLicense { get; set; }

        public string Documentos { get; set; }

        public string TipoPersona { get; set; }

        public class Address
        {
            public int CorpId { get; set; }
            public int DirectoryId { get; set; }
            public int DirectoryDetailId { get; set; }
            public int CommunicationTypeId { get; set; }
            public int DirectoryTypeId { get; set; }
            public string DirectoryTypeDesc { get; set; }
            public string StreetAddress { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public string CountryDesc { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public string StateProvDesc { get; set; }
            public int CityId { get; set; }
            public string CityDesc { get; set; }
            public string ZipCode { get; set; }
            public bool IsPrimary { get; set; }
            public string CommunicationType { get; set; }
            public int ContactId { get; set; }
            public int CreateUser { get; set; }
            public int ModifyUser { get; set; }
            public int? MunicipioId { get; set; }
            public string MunicipioDesc { get; set; }
        }

        public class Phone
        {
            public int CorpId { get; set; }
            public int DirectoryId { get; set; }
            public int DirectoryDetailId { get; set; }
            public int CommunicationTypeId { get; set; }
            public int DirectoryTypeId { get; set; }
            public string DirectoryTypeDesc { get; set; }
            public string CountryCode { get; set; }
            public string AreaCode { get; set; }
            public string PhoneNumber { get; set; }
            public string PhoneExt { get; set; }
            public string PersonToContact { get; set; }
            public bool IsPrimary { get; set; }
            public string CommunicationType { get; set; }
            public int ContactId { get; set; }
            public int CreateUser { get; set; }
            public int ModifyUser { get; set; }
        }

        public class Email
        {
            public int Agent_Legal { get; set; }
            public int CorpId { get; set; }
            public int DirectoryId { get; set; }
            public int DirectoryDetailId { get; set; }
            public int CommunicationTypeId { get; set; }
            public int DirectoryTypeId { get; set; }
            public string DirectoryTypeDesc { get; set; }
            public string EmailAdress { get; set; }
            public bool IsPrimary { get; set; }
            public string CommunicationType { get; set; }
            public int ContactId { get; set; }
            public int CreateUser { get; set; }
            public int ModifyUser { get; set; }
        }

        public class CitizenQuestion
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
            public int CitizenQuestId { get; set; }
            public string CitizenQuestDesc { get; set; }
            public bool? CitizenQuestAnswer { get; set; }
            public string NameKey { get; set; }
            public int CreateUser { get; set; }
            public int ModifyUser { get; set; }
        }

        public class Citizenship
        {
            public int ContactId { get; set; }
            public int GlobalCountryId { get; set; }
            public string GlobalCountryDesc { get; set; }
            public bool Status { get; set; }
            public int CreateUser { get; set; }
            public int ModifyUser { get; set; }
        }

        public class CitizenContact
        {
            public int Corp_Id { get; set; }
            public int Region_Id { get; set; }
            public int Country_Id { get; set; }
            public int Domesticreg_Id { get; set; }
            public int State_Prov_Id { get; set; }
            public int City_Id { get; set; }
            public int Office_Id { get; set; }
            public int Case_Seq_No { get; set; }
            public int Hist_Seq_No { get; set; }
            public int? Contact_Id { get; set; }
            public int? Exposure_Related_Id { get; set; }
            public string FullName { get; set; }
            public int Relationship { get; set; }
            public string RelationshipDesc { get; set; }
            public string Position { get; set; }
            public DateTime? JobFromDate { get; set; }
            public DateTime? JobToDate { get; set; }
            public int CreateUser { get; set; }
            public int ModifyUser { get; set; }
            public int Language_id { get; set; }
        }

        public class SocialExposure
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
            public int SocialFunctionTypeId { get; set; }
            public string SocialTypeDesc { get; set; }
            public string SocialFunctionTypePosition { get; set; }
            public string NameKey { get; set; }
            public int CreateUser { get; set; }
            public int ModifyUser { get; set; }
        }

        public class SocialExposureRelationship
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
            public int SocialFunctionTypeId { get; set; }
            public string SocialTypeDesc { get; set; }
            public string SocFuncRelName { get; set; }
            public string SocialFunctionTypePosition { get; set; }
            public string NameKey { get; set; }
            public int CreateUser { get; set; }
            public int ModifyUser { get; set; }
        }

        public class IdDocument
        {
            public int Agent_Legal { get; set; }
            public int ContactId { get; set; }
            public int SeqNo { get; set; }
            public int ContactIdType { get; set; }
            public string Id { get; set; }
            public DateTime? ValidDate { get; set; }
            public DateTime? ExpireDate { get; set; }
            public int? CountryIssuedBy { get; set; }
            public string IssuedBy { get; set; }
            public string CountryIssuedByDesc { get; set; }
            public int DocumentCategoryId { get; set; }
            public int DocumentTypeId { get; set; }
            public int DocumentId { get; set; }
            public string DocumentName { get; set; }
            public byte[] DocumentBinary { get; set; }
            public string DocumentTypeDescription { get; set; }
            public string ContentType { get; set; }
            public string Extension { get; set; }
            public bool MainIdentity { get; set; }
            public string ContactIdTypeDescription { get; set; }
            public DateTime? FileCreationDate { get; set; }
            public DateTime? FileExpireDate { get; set; }
            public int UserId { get; set; }
        }

        public class BackGroundCheck
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
            public int BackgroundCheckId { get; set; }
            public int BackgroundCheckerId { get; set; }
            public string Reason { get; set; }
            public bool Results { get; set; }
            public DateTime Date { get; set; }
            public string Comments { get; set; }
            public string BackgroundCheckUserName { get; set; }
        }

        public class BackGroundCheckDocumentInfomation
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

        public struct Search
        {
            public int ContactId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string IdNumber { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string Country { get; set; }
            public DateTime? LastUpdate { get; set; }
            public Nullable<int> ContactAgentLegalId { get; set; }
        }

        public class SecurityQuestion
        {
            public int CorpId { get; set; }
            public int ContactId { get; set; }
            public int QuestionId { get; set; }
            public string NameId { get; set; }
            public string QuestionDesc { get; set; }
            public string Answer { get; set; }
            public int UserId { get; set; }
        }

        public class PolicyContact
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
            public int UserId { get; set; }
        }

        public class AgentAppointment
        {
            public int CorpId { get; set; }
            public int AgentId { get; set; }
            public int? AppointmentId { get; set; }
            public int? AppointmentSeqNo { get; set; }
            public DateTime? DateStart { get; set; }
            public DateTime? DateEnd { get; set; }
            public string AppointmentTitle { get; set; }
            public string AppointmentDesc { get; set; }
            public long? Label { get; set; }
            public string Location { get; set; }
            public bool? AllDay { get; set; }
            public int? EventType { get; set; }
            public string RecurrenceInfo { get; set; }
            public string ReminderInfo { get; set; }
            public int? OwnerId { get; set; }
            public decimal? Price { get; set; }
            public int AppointmentStatus { get; set; }
            public int UserId { get; set; }
        }

        public class AgentContact
        {
            public int CorpId { get; set; }
            public int AgentId { get; set; }
            public int ContactId { get; set; }
            public int UserId { get; set; }
        }

        public class AgentTree
        {
            public string NameId { get; set; }
            public int? AgentId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class AgentTreeParameter
        {
            public int? CorpId { get; set; }
            public int? AgentId { get; set; }
            public string AgentType { get; set; }
            public int? AgentStatusId { get; set; }
        }

        public class GenerateNameId
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public string NameId { get; set; }
        }

        public class GenerateCustomerNumber
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public string CustomerNumber { get; set; }
        }

        public class Math
        {
            public int ContactTypeId { get; set; }
            public int LanguageId { get; set; }
            public string FirstName { get; set; }
            public string FirstLastName { get; set; }
            public DateTime Dob { get; set; }
            public string Ids { get; set; }

            public int ContactId { get; set; }
            public string MiddleName { get; set; }
            public string SecondLastName { get; set; }
            public string FullName { get; set; }
            public string CountryDesc { get; set; }
        }

        public class Directory
        {
            public int CorpId { get; set; }
            public int ContactId { get; set; }
            public int DirectoryId { get; set; }
        }

        public class ValidateDocumentCedula
        {
            public int? Cedula { get; set; }
        }

        public class ValidateDocumentRNC
        {
            public int? RNC { get; set; }
        }

        public class ValidateDocumentIDS
        {
            public int Contact_id { get; set; }
            public string IDs { get; set; }
        }
    }
}