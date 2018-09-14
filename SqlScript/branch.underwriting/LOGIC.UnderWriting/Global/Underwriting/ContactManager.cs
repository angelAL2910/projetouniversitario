﻿using System;
using System.Collections.Generic;
using DATA.UnderWriting.Repositories.Global;
using DATA.UnderWriting.UnitOfWork;
using Entity.UnderWriting.Entities;
using LOGIC.UnderWriting.Common;

namespace LOGIC.UnderWriting.Global
{
    public class ContactManager
    {
        private ContactRepository _contactRepository;

        public ContactManager()
        {
            _contactRepository = SingletonUnitOfWork.Instance.ContactRepository;
        }

        public Contact GetContact(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? contactRoleTypeId, int languageId)
        {
            Contact result;

            result = _contactRepository.GetContact(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, contactRoleTypeId, languageId);

            if (result != null && string.IsNullOrWhiteSpace(result.FullName))
                result.FullName = Utility.FullName(result.FirstName, result.MiddleName, result.FirstLastName, result.SecondLastName);

            return
                result;
        }

        public Contact GetContact(int coprId, int contactId, int languageId)
        {
            Contact result;

            result = _contactRepository.GetContact(coprId, contactId, languageId);

            if (result != null && string.IsNullOrWhiteSpace(result.FullName))
                result.FullName = Utility.FullName(result.FirstName, result.MiddleName, result.FirstLastName, result.SecondLastName);

            return
                result;
        }

        public Contact GetContactSummary(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? contactRoleTypeId)
        {
            Contact result;

            result = _contactRepository.GetContactSummary(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, contactRoleTypeId);

            if (result != null)
                result.FullName = Utility.FullName(result.FirstName, result.MiddleName, result.FirstLastName, result.SecondLastName);

            return
                result;
        }

        public IEnumerable<Contact.Citizenship> GetContactCitizenshipByContact(int contactId)
        {
            return
                _contactRepository.GetCitizenshipByContact(contactId);
        }

        public IEnumerable<Contact.CitizenQuestion> GetContactCitizenQuestionByContact(
            int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? contactId, int languageId)
        {
            return
                _contactRepository.GetCitizenQuestionByContact(coprId, regionId, countryId, domesticRegId
                , stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, languageId);
        }

        public IEnumerable<Contact.CitizenQuestion> GetContactCitizenQuestionByContactJuridico(
            int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? Agent_Legal, int languageId)
        {
            return
                _contactRepository.GetCitizenQuestionByContact(coprId, regionId, countryId, domesticRegId
                , stateProvId, cityId, officeId, caseSeqNo, histSeqNo, Agent_Legal, languageId);
        }

        public IEnumerable<Contact.SocialExposure> GetContactSocialExposureByContact(
            int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? contactId, int languageId)
        {
            return
                _contactRepository.GetSocialExposureByContact(coprId, regionId, countryId, domesticRegId
                , stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, languageId);
        }

        public IEnumerable<Contact.SocialExposure> GetContactSocialExposureByContactJuridico(
           int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? Agen_Legal, int languageId)
        {
            return
                _contactRepository.GetSocialExposureByContact(coprId, regionId, countryId, domesticRegId
                , stateProvId, cityId, officeId, caseSeqNo, histSeqNo, Agen_Legal, languageId);
        }

        public IEnumerable<Contact.SocialExposureRelationship> GetContactSocialExposureRelationshipByContact(
             int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? contactId, int languageId)
        {
            return
                _contactRepository.GetSocialExposureRelationshipByContact(coprId, regionId, countryId, domesticRegId
                , stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, languageId);
        }

        public IEnumerable<Contact.SocialExposureRelationship> GetContactSocialExposureRelationshipByContactJuridico(
             int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? Agent_Legal, int languageId)
        {
            return
                _contactRepository.GetSocialExposureRelationshipByContact(coprId, regionId, countryId, domesticRegId
                , stateProvId, cityId, officeId, caseSeqNo, histSeqNo, Agent_Legal, languageId);
        }

        public int InsertContactCitizenship(Contact.Citizenship citizenship)
        {
            citizenship.Status = true;

            return
                _contactRepository.SetContactCitizenship(citizenship);
        }
        public int DeleteContactCitizenship(Contact.Citizenship citizenship)
        {
            citizenship.Status = false;

            return
                _contactRepository.SetContactCitizenship(citizenship);
        }
        public int UpdateContactCitizenQuestionByContact(Contact.CitizenQuestion question)
        {
            return
                _contactRepository.SetCitizenQuestionByContact(question);
        }

        public int UpdateContactCitizenQuestionByContactJuridico(Contact.CitizenQuestion question)
        {
            return
                _contactRepository.SetCitizenQuestionByContactJuridico(question);
        }

        public int UpdateContactSocialExposureByContact(Contact.SocialExposure socialExposure)
        {
            return
                _contactRepository.SetSocialExposureByContact(socialExposure);
        }

        public int UpdateContactSocialExposureByContactJuridico(Contact.SocialExposure socialExposure)
        {
            return
                _contactRepository.SetSocialExposureByContactJuridico(socialExposure);
        }

        public int UpdateContactSocialExposureRelationshipByContact(Contact.SocialExposureRelationship socialExposureRelationship)
        {
            return
                _contactRepository.SetSocialExposureRelationshipByContact(socialExposureRelationship);
        }

        public int UpdateContactSocialExposureRelationshipByContactJuridico(Contact.SocialExposureRelationship socialExposureRelationship)
        {
            return
                _contactRepository.SetSocialExposureRelationshipByContactJuridico(socialExposureRelationship);
        }

        public void GetCommunicatonAll(int corpId, int contactId, int languageId, out IEnumerable<Contact.Address> addresses, out IEnumerable<Contact.Phone> phones, out IEnumerable<Contact.Email> emails)
        {
            _contactRepository.GetCommunicationAll(corpId, contactId, languageId, out  addresses, out  phones, out emails);
        }

        public void GetCommunicatonAllJuridico(int corpId, int Agent_Legal, int languageId, out IEnumerable<Contact.Address> addresses, out IEnumerable<Contact.Phone> phones, out IEnumerable<Contact.Email> emails)
        {
            _contactRepository.GetCommunicationAll(corpId, Agent_Legal, languageId, out addresses, out phones, out emails);
        }

        public virtual IEnumerable<Contact.Address> GetCommunicatonAdress(int corpId, int contactId, int languageId)
        {
            return
                _contactRepository.GetCommunicationAdress(corpId, contactId, languageId);
        }
        public virtual IEnumerable<Contact.Phone> GetCommunicatonPhone(int corpId, int contactId, int languageId)
        {
            return
                _contactRepository.GetCommunicationPhone(corpId, contactId, languageId);
        }

        public virtual IEnumerable<Contact.CitizenContact> GetAllCitizenContact(Contact.CitizenContact Contact)
        {
            return
                _contactRepository.GetAllCitizenContact(Contact);
        }
        public virtual IEnumerable<Contact.Email> GetCommunicatonEmail(int corpId, int contactId, int languageId)
        {
            return
                _contactRepository.GetCommunicationEmail(corpId, contactId, languageId);
        }

        public virtual IEnumerable<Contact.Email> GetCommunicatonEmailJuridico(int corpId, int Agent_Legal, int languageId)
        {
            return
                _contactRepository.GetCommunicationEmailJuridico(corpId, Agent_Legal, languageId);
        }


        #region Agent
        public virtual void GetAgentCommunicationAll(int corpId, int agentId, int languageId, out IEnumerable<Contact.Address> addresses, out IEnumerable<Contact.Phone> phones, out IEnumerable<Contact.Email> emails)
        {
            if (corpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "corpId");
            if (agentId <= 0)
                throw new ArgumentException("This property can't be under 0.", "agentId");
            if (languageId <= 0)
                throw new ArgumentException("This property can't be under 0.", "languageId");

            _contactRepository.GetAgentCommunicationAll(corpId, agentId, languageId, out addresses, out phones, out emails);
        }
        public virtual IEnumerable<Contact.Address> GetAgentCommunicationAdress(int corpId, int agentId, int languageId)
        {
            if (corpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "corpId");
            if (agentId <= 0)
                throw new ArgumentException("This property can't be under 0.", "agentId");
            if (languageId <= 0)
                throw new ArgumentException("This property can't be under 0.", "languageId");

            return
                _contactRepository.GetAgentCommunicationAdress(corpId, agentId, languageId);
        }
        public virtual IEnumerable<Contact.Phone> GetAgentCommunicationPhone(int corpId, int agentId, int languageId)
        {
            if (corpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "corpId");
            if (agentId <= 0)
                throw new ArgumentException("This property can't be under 0.", "agentId");
            if (languageId <= 0)
                throw new ArgumentException("This property can't be under 0.", "languageId");

            return
                _contactRepository.GetAgentCommunicationPhone(corpId, agentId, languageId);
        }
        public virtual IEnumerable<Contact.Email> GetAgentCommunicationEmail(int corpId, int agentId, int languageId)
        {
            if (corpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "corpId");
            if (agentId <= 0)
                throw new ArgumentException("This property can't be under 0.", "agentId");
            if (languageId <= 0)
                throw new ArgumentException("This property can't be under 0.", "languageId");

            return
                _contactRepository.GetAgentCommunicationEmail(corpId, agentId, languageId);
        }
        public virtual IEnumerable<Contact.AgentAppointment> GetAgentAppointment(int corpId, int agentId)
        {
            return
                _contactRepository.GetAgentAppointment(corpId, agentId);
        }
        public virtual int UpdateAgentAppointment(Contact.AgentAppointment agentAppointment)
        {
            this.IsValid(agentAppointment, Utility.DataBaseActionType.Update);

            return
                 this.SetAgentAppointment(agentAppointment);
        }
        public virtual int InsertAgentAppointment(Contact.AgentAppointment agentAppointment)
        {
            this.IsValid(agentAppointment, Utility.DataBaseActionType.Insert);

            agentAppointment.AppointmentId = null;
            agentAppointment.AppointmentSeqNo = null;

            return
                 this.SetAgentAppointment(agentAppointment);
        }

        public virtual int SetAgentContact(Contact.AgentContact parameter)
        {
            this.IsValid(parameter, Utility.DataBaseActionType.Insert);

            return
                _contactRepository.SetAgentContact(parameter);
        }

        private int SetAgentAppointment(Contact.AgentAppointment agentAppointment)
        {
            return
                 _contactRepository.SetAgentAppointment(agentAppointment);
        }
        private void IsValid(Contact.AgentAppointment agentAppointment, Utility.DataBaseActionType action)
        {
            if (agentAppointment.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (agentAppointment.AgentId <= 0)
                throw new ArgumentException("This property can't be under 0.", "AgentId");
            if (agentAppointment.UserId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UserId");

            switch (action)
            {
                case Utility.DataBaseActionType.Update:
                case Utility.DataBaseActionType.Delete:
                    if (!agentAppointment.AppointmentId.HasValue || agentAppointment.AppointmentId.Value <= 0)
                        throw new ArgumentException("This property can't be under 0.", "AppointmentId");
                    if (!agentAppointment.AppointmentSeqNo.HasValue || agentAppointment.AppointmentSeqNo.Value <= 0)
                        throw new ArgumentException("This property can't be under 0.", "AppointmentSeqNo");
                    break;
                case Utility.DataBaseActionType.Insert:
                    break;
                case Utility.DataBaseActionType.Select:
                default:
                    break;
            }
        }
        private void IsValid(Contact.AgentContact agentContact, Utility.DataBaseActionType action)
        {
            if (agentContact.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (agentContact.AgentId <= 0)
                throw new ArgumentException("This property can't be under 0.", "AgentId");
            if (agentContact.ContactId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ContactId");
            if (agentContact.UserId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UserId");

            switch (action)
            {
                case Utility.DataBaseActionType.Update:
                case Utility.DataBaseActionType.Delete:
                    break;
                case Utility.DataBaseActionType.Insert:
                    break;
                case Utility.DataBaseActionType.Select:
                default:
                    break;
            }
        }
        public virtual IEnumerable<Contact.AgentTree> GetAgentTree(Contact.AgentTreeParameter parameter)
        {
            return
                _contactRepository.GetAgentTree(parameter);
        }
        #endregion

        public virtual int SetAddress(Contact.Address address)
        {
            return
                _contactRepository.SetAddress(address);
        }
        public virtual int SetPhone(Contact.Phone phone)
        {
            phone.CountryCode = string.IsNullOrWhiteSpace(phone.CountryCode) ? null : phone.CountryCode.Trim();
            phone.AreaCode = string.IsNullOrWhiteSpace(phone.AreaCode) ? null : phone.AreaCode.Trim();
            phone.PhoneNumber = string.IsNullOrWhiteSpace(phone.PhoneNumber) ? null : phone.PhoneNumber.Trim();
            phone.PhoneExt = string.IsNullOrWhiteSpace(phone.PhoneExt) ? null : phone.PhoneExt.Trim();

            return
                _contactRepository.SetPhone(phone);
        }
        public virtual int SetEmail(Contact.Email email)
        {
            email.EmailAdress = string.IsNullOrWhiteSpace(email.EmailAdress) ? null : email.EmailAdress.Trim().ToLower();

            return
                _contactRepository.SetEmail(email);
        }
        public virtual int DeleteCommunicaton(int corpId, int directoryId, int directoryDetailId, int modifyUser)
        {
            return
                _contactRepository.DeleteCommunicaton(corpId, directoryId, directoryDetailId, modifyUser);
        }

        public virtual int DeleteCommunicatonJuridico(int corpId, int directoryId, int directoryDetailId, int modifyUser)
        {
            return
                _contactRepository.DeleteCommunicatonJuridico(corpId, directoryId, directoryDetailId, modifyUser);
        }


        public virtual IEnumerable<Contact.IdDocument> GetAllIdDocumentInformation(int contactId, int languageId)
        {
            return
                 _contactRepository.GetAllIdDocumentInformation(contactId, languageId);
        }

        public virtual IEnumerable<Contact.IdDocument> GetAllIdDocumentInformationJuridico(int Agent_Legal, int languageId)
        {
            return
                 _contactRepository.GetAllIdDocumentInformationJuridico(Agent_Legal, languageId);
        }

        public virtual IEnumerable<Contact.IdDocument> GetAllIdDocumentBenefitary(int contactId, int languageId)
        {
            return
                 _contactRepository.GetAllIdDocumentBenefitary(contactId, languageId);
        }


        public virtual Contact.IdDocument GetIdDocument(int contactId, int documentCategoryId, int documentTypeId, int documentId)
        {
            return
                _contactRepository.GetIdDocument(contactId, documentCategoryId, documentTypeId, documentId);
        }
        public virtual bool CheckIdDocument(int contactId, int contactIdType, int countryIssuedBy, string id)
        {
            return
                 _contactRepository.CheckIdDocument(contactId, contactIdType, countryIssuedBy, id);
        }
        public virtual Contact.BackGroundCheck GetBackGroundCheck(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int contactId)
        {
            return
                _contactRepository.GetBackGroundCheck(coprId, regionId, countryId, domesticRegId, stateProvId
                , cityId, officeId, caseSeqNo, histSeqNo, contactId);
        }

        public virtual IEnumerable<Contact.BackGroundCheckDocumentInfomation> GetAllBackGroundCheckDocumentInformation(int coprId, int regionId, int countryId
          , int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            return
                _contactRepository.GetAllBackGroundCheckDocumentInformation(coprId, regionId, countryId, domesticRegId, stateProvId
                , cityId, officeId, caseSeqNo, histSeqNo);
        }
        public virtual Contact.BackGroundCheckDocumentInfomation GetBackGroundCheckDocument(int coprId, int regionId, int countryId
           , int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int documentCategoryId, int documentTypeId, int documentId)
        {
            return
                  _contactRepository.GetBackGroundCheckDocument(coprId, regionId, countryId, domesticRegId, stateProvId
                , cityId, officeId, caseSeqNo, histSeqNo, documentCategoryId, documentTypeId, documentId);
        }

        public virtual int UpdateContact(Contact contact)
        {
            if (contact.ContactId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ContactId");

            return
                _contactRepository.UpdateContact(contact);
        }

        public virtual int InsertContact(Contact contact)
        {
            if (!contact.CorpId.HasValue || contact.CorpId.Value <= 0)
                throw new ArgumentException("This property can't be under 0 or null.", "CorpId");

            //if (!contact.AgentId.HasValue || contact.AgentId.Value <= 0)
            //    throw new ArgumentException("This property can't be under 0 or null.", "AgentId");

            contact.ContactId = -1;

            return
                _contactRepository.InsertContact(contact);
        }

        public virtual IEnumerable<Contact.Search> ContactSearchByAgent(int coprId, int agentId, int? contactTypeId)
        {
            return
                _contactRepository.ContactSearchByAgent(coprId, agentId, contactTypeId);
        }

        public virtual int SetIdDocument(Contact.IdDocument document)
        {
            if (!document.FileCreationDate.HasValue)
                document.FileCreationDate = DateTime.Now;

            if (document.ExpireDate.HasValue)
                document.FileExpireDate = document.ExpireDate;


            return
                _contactRepository.SetIdDocument(document);
        }

        public virtual int UpdateCitizenContact(Contact.CitizenContact Contact)
        {
            return
                _contactRepository.SetCitizenContact(Contact);
        }

        public virtual int DeleteCitizenContact(Contact.CitizenContact Contact)
        {
            return
                _contactRepository.DeleteCitizenContact(Contact);
        }

        public virtual int SetIdDocumentJuridico(Contact.IdDocument documentJuridico)
        {
            if (!documentJuridico.FileCreationDate.HasValue)
                documentJuridico.FileCreationDate = DateTime.Now;

            return
                _contactRepository.SetIdDocumentJuridico(documentJuridico);
        }

        public virtual int DeleteIdDocument(int contactId, int seqNo, int userId)
        {
            return
                _contactRepository.DeleteIdDocument(contactId, seqNo, userId);
        }

        public virtual int DeleteIdDocumentJuridico(int Agent_Legal, int seqNo, int userId)
        {
            return
                _contactRepository.DeleteIdDocumentJuridico(Agent_Legal, seqNo, userId);
        }

        public virtual IEnumerable<Contact.SecurityQuestion> GetAllSecurityQuestion(int corpId, int contactId, int languageId)
        {
            return
                _contactRepository.GetAllSecurityQuestion(corpId, contactId, languageId);
        }

        public virtual int SetSecurityQuestion(Contact.SecurityQuestion securityQuestion)
        {
            if (securityQuestion.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (securityQuestion.QuestionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "QuestionId");
            if (securityQuestion.ContactId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ContactId");

            if (securityQuestion.Answer == null)
                throw new ArgumentException("This property can't be null.", "Answer");

            securityQuestion.Answer = securityQuestion.Answer.Trim();

            return
                _contactRepository.SetSecurityQuestion(securityQuestion);
        }

        public virtual IEnumerable<Contact.GenerateNameId> SetNameIdToContactId(int contactId, string nameId, int userId)
        {
            if (contactId <= 0)
                throw new ArgumentException("This property can't be under 0.", "contactId");
            if (userId <= 0)
                throw new ArgumentException("This property can't be under 0.", "userId");

            nameId = string.IsNullOrWhiteSpace(nameId)
                    ? null
                    : nameId.Trim();

            return
                _contactRepository.SetNameIdToContactId(contactId, nameId, userId);
        }

        public virtual int GetContactIdByNameId(string nameId)
        {
            if (string.IsNullOrWhiteSpace(nameId))
                throw new ArgumentException("This property can't be null or whitespace.", "nameId");

            return
                _contactRepository.GetContactIdByNameId(nameId);
        }

        public virtual IEnumerable<Contact.Math> GetContactMath(Contact.Math parameters)
        {
            if (parameters.ContactTypeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ContactTypeId");
            if (parameters.LanguageId <= 0)
                throw new ArgumentException("This property can't be under 0.", "LanguageId");
            if (string.IsNullOrWhiteSpace(parameters.FirstName))
                throw new ArgumentException("This property can't be null or whitespace.", "FirstName");
            if (string.IsNullOrWhiteSpace(parameters.FirstLastName))
                throw new ArgumentException("This property can't be null or whitespace.", "FirstLastName");
            if (string.IsNullOrWhiteSpace(parameters.Ids))
                throw new ArgumentException("This property can't be null or whitespace.", "Ids");

            parameters.FirstName = parameters.FirstName.Trim();
            parameters.FirstLastName = parameters.FirstLastName.Trim();
            parameters.Ids = parameters.Ids.Trim();

            return
                _contactRepository.GetContactMath(parameters);
        }

        public virtual IEnumerable<Contact.GenerateCustomerNumber> SetCustomerNumberToContactId(int contactId, string customerNumber, int userId)
        {
            if (contactId <= 0)
                throw new ArgumentException("This property can't be under 0.", "contactId");
            if (userId <= 0)
                throw new ArgumentException("This property can't be under 0.", "userId");

            customerNumber = string.IsNullOrWhiteSpace(customerNumber)
                    ? null
                    : customerNumber.Trim();

            return
                _contactRepository.SetCustomerNumberToContactId(contactId, customerNumber, userId);
        }
        public virtual int GetContactIdByCustomerNumber(string customerNumber)
        {
            if (string.IsNullOrWhiteSpace(customerNumber))
                throw new ArgumentException("This property can't be null or whitespace.", "customerNumber");

            return
                _contactRepository.GetContactIdByCustomerNumber(customerNumber);
        }

        public virtual Contact.Directory GetDirectoryId(int corpId, int contactId)
        {
            if (corpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "corpId");
            if (contactId <= 0)
                throw new ArgumentException("This property can't be under 0.", "contactId");

            return
                _contactRepository.GetDirectoryId(corpId, contactId);
        }

        public virtual Contact.ValidateDocumentCedula GetResultCedula(string DocumentCedula)
        {
            if (string.IsNullOrWhiteSpace(DocumentCedula))
                throw new ArgumentException("This property can't be null or whitespace.", "DocumentCedula");
            return
                _contactRepository.GetResultDUI(DocumentCedula);
        }
        public virtual Contact.ValidateDocumentRNC GetResultRNC(string DocumentRNC)
        {
            if (string.IsNullOrWhiteSpace(DocumentRNC))
                throw new ArgumentException("This property can't be null or whitespace.", "DocumentRNC");
            return
                _contactRepository.GetResultRNC(DocumentRNC);
        }

        public virtual IEnumerable<Contact.ValidateDocumentIDS> GetAllDocumentIDs(string IDs)
        { 
            if (string.IsNullOrWhiteSpace(IDs))
                throw new ArgumentException("This property can't be null or whitespace.", "IDs");

            return
                _contactRepository.GetAllDocumentIDs(IDs);
        }

        public virtual Contact.FinalBeneficiary SetContactFinalBeneficiary(int? contactId, int? finalBeneficiaryId, string name, decimal? percentageParticipation, bool? finalBeneficiaryStatus, int? userId, bool? IsPEP, int? PepFormularyOptionsId, int? identificationTypeId, string identificationNumber, int? nationalityCountryId)
        {
            return
                _contactRepository.SetContactFinalBeneficiary(contactId, finalBeneficiaryId, name, percentageParticipation, finalBeneficiaryStatus, userId,IsPEP, PepFormularyOptionsId,identificationTypeId,identificationNumber,nationalityCountryId);
        }

        public virtual Contact.PepFormulary SetContactPepFormulary(int? contactId, int? pepFormularyId, string name, int? relationshipId, string position, int? fromYear, int? toYear, bool? pepFormularyStatus, int? userId, int? BeneficiaryId, bool? IsPepManagerCompany)
        {
            return
                _contactRepository.SetContactPepFormulary(contactId, pepFormularyId, name, relationshipId, position, fromYear, toYear, pepFormularyStatus, userId, BeneficiaryId, IsPepManagerCompany);
        }

        public virtual IEnumerable<Contact.PepFormulary.PEPFormResult> GetContactPEPFormulary(int? ContactId, string Source)
        {
            return
                _contactRepository.GetContactPEPFormulary(ContactId, Source);
        }

        public virtual IEnumerable<Contact.FinalBeneficiary.FinalBenResult> GetContactFinalBeneficiary(int? ContactId)
        {
            return
                _contactRepository.GetContactFinalBeneficiary(ContactId);
        }


    }
}