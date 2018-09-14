﻿using System.Collections.Generic;
using Entity.UnderWriting.Entities;

namespace DI.UnderWriting.Interfaces
{
    public interface IContact
    {
        Contact GetContact(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? contactRoleTypeId, int languageId);
        
        Contact GetContact(int coprId, int contactId, int languageId);
        
        Contact GetContactSummary(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? contactRoleTypeId);
        
        int UpdateContact(Contact contact);
        
        int InsertContact(Contact contact);
        
        IEnumerable<Contact.Citizenship> GetContactCitizenshipByContact(int contactId);
        
        IEnumerable<Contact.CitizenQuestion> GetContactCitizenQuestionByContact(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? contactId, int languageId);
        
        IEnumerable<Contact.CitizenQuestion> GetContactCitizenQuestionByContactJuridico(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? Agent_Legal, int languageId);
       
        IEnumerable<Contact.SocialExposure> GetContactSocialExposureByContact(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? contactId, int languageId);
        
        IEnumerable<Contact.SocialExposure> GetContactSocialExposureByContactJuridico(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? Agent_Legal, int languageId);
        
        IEnumerable<Contact.SocialExposureRelationship> GetContactSocialExposureRelationshipByContact(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? contactId, int languageId);
        
        IEnumerable<Contact.SocialExposureRelationship> GetContactSocialExposureRelationshipByContactJuridico(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? Agent_Legal, int languageId);
        
        int InsertContactCitizenship(Contact.Citizenship citizenship);
        
        int DeleteContactCitizenship(Contact.Citizenship citizenship);
        
        int UpdateContactCitizenQuestionByContact(Contact.CitizenQuestion question);
        
        int UpdateContactCitizenQuestionByContactJuridico(Contact.CitizenQuestion question);
       
        int UpdateContactSocialExposureByContact(Contact.SocialExposure socialExposure);
        
        int UpdateContactSocialExposureByContactJuridico(Contact.SocialExposure socialExposure);
        
        int UpdateContactSocialExposureRelationshipByContact(Contact.SocialExposureRelationship socialExposureRelationship);
        
        int UpdateContactSocialExposureRelationshipByContactJuridico(Contact.SocialExposureRelationship socialExposureRelationship);
        
        void GetCommunicatonAll(int corpId, int contactId, int languageId, out IEnumerable<Contact.Address> adresses, out IEnumerable<Contact.Phone> phones, out IEnumerable<Contact.Email> emails);
        
        void GetCommunicatonAllJuridico(int corpId, int Agent_Legal, int languageId, out IEnumerable<Contact.Address> adresses, out IEnumerable<Contact.Phone> phones, out IEnumerable<Contact.Email> emails);
        
        IEnumerable<Contact.Address> GetCommunicatonAdress(int corpId, int contactId, int languageId);
        
        IEnumerable<Contact.Email> GetCommunicatonEmailJuridico(int corpId, int Agent_Legal, int languageId);
        
        IEnumerable<Contact.Phone> GetCommunicatonPhone(int corpId, int contactId, int languageId);
        
        IEnumerable<Contact.CitizenContact> GetAllCitizenContact(Contact.CitizenContact Contact);
        
        IEnumerable<Contact.IdDocument> GetAllIdDocumentInformationJuridico(int Agent_Legal, int languageId);
        
        IEnumerable<Contact.Email> GetCommunicatonEmail(int corpId, int contactId, int languageId);
        
        IEnumerable<Contact.IdDocument> GetAllIdDocumentInformation(int contactId, int languageId);
        
        IEnumerable<Contact.IdDocument> GetAllIdDocumentBenefitary(int contactId, int languageId);
        
        Contact.IdDocument GetIdDocument(int contactId, int documentCategoryId, int documentTypeId, int documentId);
        
        int SetAddress(Contact.Address address);
        
        int SetPhone(Contact.Phone phone);
        
        int SetEmail(Contact.Email email);
        
        int DeleteCommunicaton(int corpId, int directoryId, int directoryDetailId, int modifyUser);
        
        int DeleteCommunicatonJuridico(int corpId, int directoryId, int directoryDetailId, int modifyUser);
        
        Contact.BackGroundCheck GetBackGroundCheck(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int contactId);
        
        IEnumerable<Contact.BackGroundCheckDocumentInfomation> GetAllBackGroundCheckDocumentInformation(int coprId, int regionId, int countryId
          , int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo);
        
        Contact.BackGroundCheckDocumentInfomation GetBackGroundCheckDocument(int coprId, int regionId, int countryId
           , int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int documentCategoryId, int documentTypeId, int documentId);
        
        IEnumerable<Contact.Search> ContactSearchByAgent(int coprId, int agentId, int? contactTypeId);
        
        int SetIdDocument(Contact.IdDocument document);
        
        int SetCitizenContact(Contact.CitizenContact Contact);
        
        int SetIdDocumentJuridico(Contact.IdDocument documentJuridico);
        
        int DeleteIdDocument(int contactId, int seqNo, int userId);
        
        int DeleteIdDocumentJuridico(int Agent_Legal, int seqNo, int userId);
        
        int DeleteCitizenContact(Contact.CitizenContact Contact);
        
        bool CheckIdDocument(int contactId, int contactIdType, int countryIssuedBy, string id);
        
        int SetSecurityQuestion(Contact.SecurityQuestion securityQuestion);
        
        IEnumerable<Contact.SecurityQuestion> GetAllSecurityQuestion(int corpId, int contactId, int languageId);
        
        Contact.Directory GetDirectoryId(int corpId, int contactId);

        Contact.ValidateDocumentCedula GetResultCedula(string DocumentCedula);
        
        Contact.ValidateDocumentRNC GetResultRNC(string DocumentRNC);

        IEnumerable<Contact.ValidateDocumentIDS> GetAllDocumentIDs(string IDs);

        Contact.FinalBeneficiary SetContactFinalBeneficiary(int? contactId, int? finalBeneficiaryId, string name, decimal? percentageParticipation, bool? finalBeneficiaryStatus, int? userId, bool? IsPEP, int? PepFormularyOptionsId,int? identificationTypeId, string identificationNumber, int? nationalityCountryId);

        Contact.PepFormulary SetContactPepFormulary(int? contactId, int? pepFormularyId, string name, int? relationshipId, string position, int? fromYear, int? toYear, bool? pepFormularyStatus, int? userId, int? BeneficiaryId, bool? IsPepManagerCompany);

        IEnumerable<Contact.PepFormulary.PEPFormResult> GetContactPEPFormulary(int? ContactId, string Source);
        
        IEnumerable<Contact.FinalBeneficiary.FinalBenResult> GetContactFinalBeneficiary(int? ContactId);

        #region Agent

        void GetAgentCommunicationAll(int corpId, int agentId, int languageId, out IEnumerable<Contact.Address> addresses, out IEnumerable<Contact.Phone> phones, out IEnumerable<Contact.Email> emails);
        
        IEnumerable<Contact.Address> GetAgentCommunicationAdress(int corpId, int agentId, int languageId);
        
        IEnumerable<Contact.Phone> GetAgentCommunicationPhone(int corpId, int agentId, int languageId);
        
        IEnumerable<Contact.Email> GetAgentCommunicationEmail(int corpId, int agentId, int languageId);
        
        int UpdateAgentAppointment(Contact.AgentAppointment agentAppointment);
        
        int InsertAgentAppointment(Contact.AgentAppointment agentAppointment);
        
        IEnumerable<Contact.AgentAppointment> GetAgentAppointment(int corpId, int agentId);
        
        int SetAgentContact(Contact.AgentContact parameter);
        
        IEnumerable<Contact.AgentTree> GetAgentTree(Contact.AgentTreeParameter parameter);

        #endregion
    }
}