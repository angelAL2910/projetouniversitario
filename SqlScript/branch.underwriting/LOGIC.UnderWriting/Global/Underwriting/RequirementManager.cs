﻿using System;
using System.Collections.Generic;
using System.Linq;
using DATA.UnderWriting.Repositories.Global;
using DATA.UnderWriting.UnitOfWork;
using Entity.UnderWriting.Entities;
using LOGIC.UnderWriting.Common;

namespace LOGIC.UnderWriting.Global
{
    public class RequirementManager
    {
        private RequirementRepository _requirementRepository;
        private string _msg;

        public RequirementManager()
        {
            _requirementRepository = SingletonUnitOfWork.Instance.RequirementRepository;
            _msg = "This property can't be under 0.";
        }

        public virtual IEnumerable<Requirement> GetAll(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int languageId)
        {
            return
                _requirementRepository.GetAll(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, languageId);
        }
        public virtual IEnumerable<Requirement> GetAllNewBusiness(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
       , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? requirementCatId, int languageId)
        {
            return
               _requirementRepository.GetAllNewBusiness(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId
            , caseSeqNo, histSeqNo, contactId, requirementCatId, languageId);
        }
        public virtual IEnumerable<Requirement.CategoryByContactRole> GetAllCategoryByContactRole(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo)
        {
            return
                _requirementRepository.GetAllCategoryByContactRole(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);
        }
        public virtual IEnumerable<Requirement.Document> GetAllDocuments(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
        , int officeId, int caseSeqNo, int histSeqNo, int contactId, int requirementCatId, int requirementTypeId, int requirementId)
        {
            return
                _requirementRepository.GetAllDocuments(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , contactId, requirementCatId, requirementTypeId, requirementId);
        }
        public virtual Requirement.Document GetDocument(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
        , int officeId, int caseSeqNo, int histSeqNo, int contactId, int requirementCatId, int requirementTypeId, int requirementId, int requirementDocId)
        {
            return
                _requirementRepository.GetDocument(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , contactId, requirementCatId, requirementTypeId, requirementId, requirementDocId);
        }

        public virtual Requirement.DocumentRequirementOnBase GetRequirementDocumentOnBase(string type, int coprId, int requirementCatId, int requirementTypeId)
        {
            return
                _requirementRepository.GetRequirementDocumentOnBase(type,coprId, requirementCatId, requirementTypeId);
        }

        public virtual IEnumerable<Requirement.Comunication> GetAllComunications(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
        , int officeId, int caseSeqNo, int histSeqNo, int contactId, int requirementCatId, int requirementTypeId, int requirementId)
        {
            return
                _requirementRepository.GetAllComunications(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , contactId, requirementCatId, requirementTypeId, requirementId);
        }
        public virtual Requirement.Document GetSpecialDocument(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int documentType)
        {
            return
                _requirementRepository.GetSpecialDocument(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, documentType);
        }

        public virtual int Insert(Requirement requirement)
        {
            int result;
            Requirement temp;
            this.IsValid(requirement, Utility.DataBaseActionType.Insert);
            requirement.RequirementId = -1;

            temp = this.Set(requirement);
            result = temp != null ? 1 : -1;

            if (requirement.AgentList != null && requirement.AgentList.Any())
                foreach (Requirement ag in requirement.AgentList)
                {
                    ag.RequirementId = temp.RequirementId;
                    this.SetReqAgent(ag);
                }

            return
                result;
        }
        public virtual int Update(Requirement requirement)
        {
            int result;
            Requirement temp;

            this.IsValid(requirement, Utility.DataBaseActionType.Update);

            temp = this.Set(requirement);
            result = temp != null ? 1 : -1;

            return
                result;
        }
        public virtual int DeleteDocument(Requirement.Document document)
        {
            this.IsValid(document, Utility.DataBaseActionType.Delete);

            return
                _requirementRepository.DeleteDocument(document);
        }
        public virtual int DeleteAll(Requirement policy)
        {
            if (policy.CorpId <= 0)
                throw new ArgumentException(_msg, "CorpId");
            if (policy.RegionId <= 0)
                throw new ArgumentException(_msg, "RegionId");
            if (policy.CountryId <= 0)
                throw new ArgumentException(_msg, "CountryId");
            if (policy.DomesticregId <= 0)
                throw new ArgumentException(_msg, "DomesticregId");
            if (policy.StateProvId <= 0)
                throw new ArgumentException(_msg, "StateProvId");
            if (policy.CityId <= 0)
                throw new ArgumentException(_msg, "CityId");
            if (policy.OfficeId <= 0)
                throw new ArgumentException(_msg, "OfficeId");
            if (policy.CaseSeqNo <= 0)
                throw new ArgumentException(_msg, "CaseSeqNo");
            if (policy.HistSeqNo <= 0)
                throw new ArgumentException(_msg, "HistSeqNo");
            if (policy.UserId <= 0)
                throw new ArgumentException(_msg, "UserId");

            return
                _requirementRepository.DeleteAll(policy);
        }
        public virtual int SetList(IEnumerable<Requirement> requirements)
        {
            int result;

            result = -1;

            if (requirements != null && requirements.Any())
                foreach (Requirement requirement in requirements)
                    result = this.Insert(requirement);
            else
                result = -2;

            return
                result;
        }
        public virtual int SetDocumentStatus(Requirement.Document document)
        {
            this.IsValid(document, Utility.DataBaseActionType.Update);

            return
                 _requirementRepository.SetDocumentStatus(document);
        }
        public virtual int SendToReinsurance(Requirement requirement)
        {
            this.IsValid(requirement, Utility.DataBaseActionType.Update);

            return
                _requirementRepository.SendToReinsurance(requirement);
        }
        public virtual int InsertDocument(Requirement.Document document)
        {
            this.IsValid(document, Utility.DataBaseActionType.Insert);

            return
               _requirementRepository.InsertDocument(document);
        }
        public virtual int InsertComunication(Requirement.Comunication comunication)
        {
            this.IsValid(comunication, Utility.DataBaseActionType.Insert);

            return
              _requirementRepository.InsertComunication(comunication);
        }

        public virtual int SetReqAgent(Requirement reqAgent)
        {
            this.IsValid(reqAgent, Utility.DataBaseActionType.Update);
            if (reqAgent.AgentId <= 0)
                throw new ArgumentException(_msg, "AgentId");

            return
              _requirementRepository.SetReqAgent(reqAgent);
        }

        public virtual int SetReqAgent(IEnumerable<Requirement> reqAgents)
        {
            int result;

            result = -1;

            if (reqAgents != null && reqAgents.Any())
                foreach (Requirement comm in reqAgents)
                    result = this.SetReqAgent(reqAgents);

            return
                result;
        }

        public virtual IEnumerable<Requirement.Product> GetRequirementProduct(Requirement.Product.Key parameter)
        {
            return
                _requirementRepository.GetRequirementProduct(parameter);
        }

        private Requirement Set(Requirement requirement)
        {
            requirement.Automatic = false;

            return
                _requirementRepository.Set(requirement);
        }

        private void IsValid(Requirement requirement, Utility.DataBaseActionType action)
        {
            if (requirement.CorpId <= 0)
                throw new ArgumentException(_msg, "CorpId");
            if (requirement.RegionId <= 0)
                throw new ArgumentException(_msg, "RegionId");
            if (requirement.CountryId <= 0)
                throw new ArgumentException(_msg, "CountryId");
            if (requirement.DomesticregId <= 0)
                throw new ArgumentException(_msg, "DomesticregId");
            if (requirement.StateProvId <= 0)
                throw new ArgumentException(_msg, "StateProvId");
            if (requirement.CityId <= 0)
                throw new ArgumentException(_msg, "CityId");
            if (requirement.OfficeId <= 0)
                throw new ArgumentException(_msg, "OfficeId");
            if (requirement.CaseSeqNo <= 0)
                throw new ArgumentException(_msg, "CaseSeqNo");
            if (requirement.HistSeqNo <= 0)
                throw new ArgumentException(_msg, "HistSeqNo");
            if (requirement.ContactId <= 0)
                throw new ArgumentException(_msg, "ContactId");
            if (requirement.RequirementCatId <= 0)
                throw new ArgumentException(_msg, "RequirementCatId");
            if (requirement.RequirementTypeId <= 0)
                throw new ArgumentException(_msg, "RequirementTypeId");
            if (requirement.UserId <= 0)
                throw new ArgumentException(_msg, "UserId");

            switch (action)
            {
                case Utility.DataBaseActionType.Update:
                case Utility.DataBaseActionType.Delete:
                    if (requirement.RequirementId <= 0)
                        throw new ArgumentException(_msg, "RequirementId");
                    break;
                case Utility.DataBaseActionType.Insert:
                case Utility.DataBaseActionType.Select:
                default:
                    break;
            }
        }
        private void IsValid(Requirement.Document document, Utility.DataBaseActionType action)
        {
            if (document.CorpId <= 0)
                throw new ArgumentException(_msg, "CorpId");
            if (document.RegionId <= 0)
                throw new ArgumentException(_msg, "RegionId");
            if (document.CountryId <= 0)
                throw new ArgumentException(_msg, "CountryId");
            if (document.DomesticregId <= 0)
                throw new ArgumentException(_msg, "DomesticregId");
            if (document.StateProvId <= 0)
                throw new ArgumentException(_msg, "StateProvId");
            if (document.CityId <= 0)
                throw new ArgumentException(_msg, "CityId");
            if (document.OfficeId <= 0)
                throw new ArgumentException(_msg, "OfficeId");
            if (document.CaseSeqNo <= 0)
                throw new ArgumentException(_msg, "CaseSeqNo");
            if (document.HistSeqNo <= 0)
                throw new ArgumentException(_msg, "HistSeqNo");
            if (document.ContactId <= 0)
                throw new ArgumentException(_msg, "ContactId");
            if (document.RequirementCatId <= 0)
                throw new ArgumentException(_msg, "RequirementCatId");
            if (document.RequirementTypeId <= 0)
                throw new ArgumentException(_msg, "RequirementTypeId");
            if (document.RequirementId <= 0)
                throw new ArgumentException(_msg, "RequirementId");
            if (document.UserId <= 0)
                throw new ArgumentException(_msg, "UserId");

            switch (action)
            {
                case Utility.DataBaseActionType.Update:
                case Utility.DataBaseActionType.Delete:
                    if (document.RequirementDocId <= 0)
                        throw new ArgumentException(_msg, "RequirementDocId");
                    break;
                case Utility.DataBaseActionType.Insert:
                case Utility.DataBaseActionType.Select:
                default:
                    break;
            }
        }
        private void IsValid(Requirement.Comunication comunication, Utility.DataBaseActionType action)
        {
            if (comunication.CorpId <= 0)
                throw new ArgumentException(_msg, "CorpId");
            if (comunication.RegionId <= 0)
                throw new ArgumentException(_msg, "RegionId");
            if (comunication.CountryId <= 0)
                throw new ArgumentException(_msg, "CountryId");
            if (comunication.DomesticregId <= 0)
                throw new ArgumentException(_msg, "DomesticregId");
            if (comunication.StateProvId <= 0)
                throw new ArgumentException(_msg, "StateProvId");
            if (comunication.CityId <= 0)
                throw new ArgumentException(_msg, "CityId");
            if (comunication.OfficeId <= 0)
                throw new ArgumentException(_msg, "OfficeId");
            if (comunication.CaseSeqNo <= 0)
                throw new ArgumentException(_msg, "CaseSeqNo");
            if (comunication.HistSeqNo <= 0)
                throw new ArgumentException(_msg, "HistSeqNo");
            if (comunication.ContactId <= 0)
                throw new ArgumentException(_msg, "ContactId");
            if (comunication.RequirementCatId <= 0)
                throw new ArgumentException(_msg, "RequirementCatId");
            if (comunication.RequirementTypeId <= 0)
                throw new ArgumentException(_msg, "RequirementTypeId");
            if (comunication.RequirementId <= 0)
                throw new ArgumentException(_msg, "RequirementId");
            if (comunication.UserId <= 0)
                throw new ArgumentException(_msg, "UserId");

            //switch (action)
            //{
            //    case Utility.DataBaseActionType.Update:
            //    case Utility.DataBaseActionType.Delete:
            //        break;
            //    case Utility.DataBaseActionType.Insert:
            //    case Utility.DataBaseActionType.Select:
            //    default:
            //        break;
            //}
        }

        //Bmarroquin 03-04-2017 se crea metodo
        public IEnumerable<Requirement.Compliance> GetComplianceContacts(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int languageId, int CompanyId)
        {
            return _requirementRepository.GetComplianceContacts(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, languageId, CompanyId);
        }

    }
}
