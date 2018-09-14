﻿using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using DATA.UnderWriting.Data;
using DATA.UnderWriting.Repositories.Base;
using Entity.UnderWriting.Entities;

namespace DATA.UnderWriting.Repositories.Global
{
    public class BeneficiaryRepository : GlobalRepository
    {
        public BeneficiaryRepository(GlobalEntityDataModel globalModel, GlobalEntities globalModelExtended) : base(globalModel, globalModelExtended) { }

        public virtual IEnumerable<Beneficiary> GetAll(Policy.Parameter policyParameter, bool? isInsured, int? beneficiaryTypeId, int? contactId, int languageId)
        {
            IEnumerable<Beneficiary> result;
            IEnumerable<SP_GET_BENEFICIARY_Result> temp;

            temp = globalModel.SP_GET_BENEFICIARY(
                    policyParameter.CorpId,
                    policyParameter.RegionId,
                    policyParameter.CountryId,
                    policyParameter.DomesticregId,
                    policyParameter.StateProvId,
                    policyParameter.CityId,
                    policyParameter.OfficeId,
                    policyParameter.CaseSeqNo,
                    policyParameter.HistSeqNo,
                    isInsured,
                    beneficiaryTypeId,
                    contactId,
                    policyParameter.LanguageId);

            result = temp
                .Select(b => new Beneficiary
                {
                    CorpId = b.Corp_Id,
                    RegionId = b.Region_Id,
                    CountryId = b.Country_Id,
                    DomesticregId = b.Domesticreg_Id,
                    StateProvId = b.State_Prov_Id,
                    CityId = b.City_Id,
                    OfficeId = b.Office_Id,
                    CaseSeqNo = b.Case_Seq_No,
                    HistSeqNo = b.Hist_Seq_No,
                    ContactId = b.Contact_Id,
                    ContactRoleTypeId = b.Contact_Role_Type_Id,
                    ContactTypeId = b.Contact_Type_Id,
                    BeneficiaryTypeId = b.Beneficiary_Type_Id,
                    PrimaryBeneficiaryId = b.Primary_Beneficiary_Id,
                    RelationshipToOwnerId = b.Relationship_To_Owner_Id,
                    RelToPrimaryBenefId = b.Rel_To_Primary_Benef_Id,
                    SeqNo = b.Seq_No,
                    ContactIdType = b.Contact_Id_Type,
                    ContactIdTypeDesc = b.Contact_Id_Type_Desc,
                    FirstName = b.First_Name,
                    MiddleName = b.Middle_Name,
                    FirstLastName = b.Lastname,
                    SecondLastName = b.Second_Lastname,
                    NickName = b.Nickname,
                    InstitutionalName = b.Institutional_Name,
                    InstitutionalCountryId = b.Institutional_Country_Id,
                    Dob = b.Dob,
                    RelationshipDesc = b.Relationship_Desc,
                    BeneficiaryTypeDesc = b.Beneficiary_Type_Desc,
                    ContactMainId = b.Contact_Main_ID,
                    BenefitsPercent = b.Benefits_Percent,
                    PrimaryBeneficiary = b.Primary_Beneficiary,
                    Comments = b.Comments,
                    IsCompany = b.IsCompany,
                    DocumentCategoryId = b.Doc_Category_Id,
                    DocumentTypeId = b.Doc_Type_Id,
                    DocumentId = b.Document_Id,
                    OccupGroupTypeId = b.OccupGroup_Type_Id,
                    OccupationId = b.Occupation_Id,
                    DocumentExists = b.DocumentExists.Value,
                    OccupationDesc = b.Occupation_Desc,
                    ReplacingBeneficiaryFullName = b.ReplacingBeneficiaryFullName,
                    LastModiUser = b.LastModiUser,
                    LastModiDate = b.LastModiDate,
                    Gender = b.Gender,
                    MaritalStatusDesc = b.Marital_Status_Desc
                })
                .ToArray();

            return
                result;
        }
        
        public virtual IEnumerable<Beneficiary> GetAllExtended(Policy.Parameter policyParameter, bool? isInsured, int? beneficiaryTypeId, int? contactId, int languageId)
        {
            IEnumerable<Beneficiary> result;
            IEnumerable<SP_GET_BENEFICIARY_EXTENDED_Result> temp;

            temp = globalModel.SP_GET_BENEFICIARY_EXTENDED(
                    policyParameter.CorpId,
                    policyParameter.RegionId,
                    policyParameter.CountryId,
                    policyParameter.DomesticregId,
                    policyParameter.StateProvId,
                    policyParameter.CityId,
                    policyParameter.OfficeId,
                    policyParameter.CaseSeqNo,
                    policyParameter.HistSeqNo,
                    isInsured,
                    beneficiaryTypeId,
                    contactId,
                    policyParameter.LanguageId);

            result = temp
                .Select(b => new Beneficiary
                {
                    CorpId = b.Corp_Id,
                    RegionId = b.Region_Id,
                    CountryId = b.Country_Id,
                    DomesticregId = b.Domesticreg_Id,
                    StateProvId = b.State_Prov_Id,
                    CityId = b.City_Id,
                    OfficeId = b.Office_Id,
                    CaseSeqNo = b.Case_Seq_No,
                    HistSeqNo = b.Hist_Seq_No,
                    ContactId = b.Contact_Id,
                    ContactRoleTypeId = b.Contact_Role_Type_Id,
                    ContactTypeId = b.Contact_Type_Id,
                    BeneficiaryTypeId = b.Beneficiary_Type_Id,
                    PrimaryBeneficiaryId = b.Primary_Beneficiary_Id,
                    RelationshipToOwnerId = b.Relationship_To_Owner_Id,
                    RelToPrimaryBenefId = b.Rel_To_Primary_Benef_Id,
                    SeqNo = b.Seq_No,
                    ContactIdType = b.Contact_Id_Type,
                    ContactIdTypeDesc = b.Contact_Id_Type_Desc,
                    FirstName = b.First_Name,
                    MiddleName = b.Middle_Name,
                    FirstLastName = b.Lastname,
                    SecondLastName = b.Second_Lastname,
                    NickName = b.Nickname,
                    InstitutionalName = b.Institutional_Name,
                    InstitutionalCountryId = b.Institutional_Country_Id,
                    Dob = b.Dob,
                    RelationshipDesc = b.Relationship_Desc,
                    BeneficiaryTypeDesc = b.Beneficiary_Type_Desc,
                    ContactMainId = b.Contact_Main_ID,
                    BenefitsPercent = b.Benefits_Percent,
                    PrimaryBeneficiary = b.Primary_Beneficiary,
                    Comments = b.Comments,
                    IsCompany = b.IsCompany,
                    DocumentCategoryId = b.Doc_Category_Id,
                    DocumentTypeId = b.Doc_Type_Id,
                    DocumentId = b.Document_Id,
                    OccupGroupTypeId = b.OccupGroup_Type_Id,
                    OccupationId = b.Occupation_Id,
                    DocumentExists = b.DocumentExists.Value,
                    OccupationDesc = b.Occupation_Desc,
                    ReplacingBeneficiaryFullName = b.ReplacingBeneficiaryFullName,
                    LastModiUser = b.LastModiUser,
                    LastModiDate = b.LastModiDate,
                    Gender = b.Gender,
                    MaritalStatusDesc = b.Marital_Status_Desc
                })
                .ToArray();

            return
                result;
        }
        
        public virtual int Insert(Beneficiary beneficiary)
        {
            return
                Set(beneficiary, "INSERT");
        }
        
        public virtual int Update(Beneficiary beneficiary)
        {
            return
                Set(beneficiary, "UPDATE");
        }
        
        public virtual int Delete(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int contactId, int contactRoleTypeId)
        {
            int result;
            IEnumerable<SP_SET_BENEFICIARY_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_BENEFICIARY(
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
                contactRoleTypeId,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                "DELETE"
                );

            return
                result;
        }
        
        public virtual int SetDocument(int contactId, int? seqNo, int? documentId, byte[] documentBinary, int userId)
        {
            int result;
            IEnumerable<SP_INSERT_BENEFICIARY_DOCUMENT_Result> temp;

            result = -1;

            temp = globalModel.SP_INSERT_BENEFICIARY_DOCUMENT(contactId, seqNo, documentId, string.Empty, string.Empty, documentBinary, userId);

            return
                result;
        }
        
        public virtual int UpdateComment(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, bool isInsured, int beneficiaryTypeId, string comment, int userId)
        {
            int result;
            IEnumerable<SP_SET_BENEFICIARY_COMMENT_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_BENEFICIARY_COMMENT(
               coprId, regionId, countryId, domesticRegId, stateProvId
                    , cityId, officeId, caseSeqNo, histSeqNo, isInsured, beneficiaryTypeId, comment, userId
                );

            return
                result;
        }

        private int Set(Beneficiary beneficiary, string action)
        {
            int result;
            IEnumerable<SP_SET_BENEFICIARY_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_BENEFICIARY(
                    beneficiary.CorpId,
                    beneficiary.RegionId,
                    beneficiary.CountryId,
                    beneficiary.DomesticregId,
                    beneficiary.StateProvId,
                    beneficiary.CityId,
                    beneficiary.OfficeId,
                    beneficiary.CaseSeqNo,
                    beneficiary.HistSeqNo,
                    beneficiary.ContactId,
                    beneficiary.ContactRoleTypeId,
                    beneficiary.BeneficiaryTypeId,
                    beneficiary.PrimaryBeneficiaryId,
                    beneficiary.PrimaryBeneficiary,
                    beneficiary.RelationshipToOwnerId,
                    beneficiary.RelToPrimaryBenefId,
                    beneficiary.ContactTypeId,
                    beneficiary.SeqNo,
                    beneficiary.BenefitsPercent,
                    beneficiary.FirstName,
                    beneficiary.MiddleName,
                    beneficiary.FirstLastName,
                    beneficiary.SecondLastName,
                    beneficiary.NickName,
                    beneficiary.IsCompany,
                    beneficiary.InstitutionalName,
                    beneficiary.InstitutionalCountryId, 
                    beneficiary.Dob,
                    beneficiary.OccupGroupTypeId,
                    beneficiary.OccupationId,
                    beneficiary.ContactMainId,
                    beneficiary.IssuedBy,
                    beneficiary.DocumentBinary,
                    beneficiary.Gender,
                    beneficiary.CreateUser,
                    beneficiary.DocumentTypeId,
                    action
                );

            return
                result;
        }
        
        public virtual int SetDocumentExtended(int contactId, int? seqNo, int? documentId, byte[] documentBinary, int userId)
        {
            int result;
            IEnumerable<SP_INSERT_BENEFICIARY_DOCUMENT_RETURN_SEQ_EN_CONTACT_IDS_Result> temp;

            result = -1;
            var contact_IdSequenceParameter = seqNo.HasValue ?
             new ObjectParameter("Seq_No", seqNo) :
             new ObjectParameter("Seq_No", typeof(int));
            temp = globalModel.SP_INSERT_BENEFICIARY_DOCUMENT_RETURN_SEQ_EN_CONTACT_IDS(contactId, contact_IdSequenceParameter, documentId, string.Empty, string.Empty, documentBinary, userId);
            return
                result;
        }

        public virtual int GetLastSequenceContactIDsByContactId(int contactId)
        {
            int result = 0;
            int? temp;

            temp = globalModel.SP_GETLASTSEQUENCEBYCONTACTID_EN_CONTACT_IDS(contactId).FirstOrDefault();
            if (temp.HasValue && temp.Value > 0)
            {
                result = temp.Value;
            }            
            return
                result;
        }
    }
}