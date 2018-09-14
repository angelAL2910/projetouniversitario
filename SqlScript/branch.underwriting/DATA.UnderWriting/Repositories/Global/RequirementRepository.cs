﻿using System;
using System.Collections.Generic;
using System.Linq;
using DATA.UnderWriting.Data;
using DATA.UnderWriting.Repositories.Base;
using Entity.UnderWriting.Entities;

namespace DATA.UnderWriting.Repositories.Global
{
    public class RequirementRepository : GlobalRepository
    {
        public RequirementRepository(GlobalEntityDataModel globalModel, GlobalEntities globalModelExtended) : base(globalModel, globalModelExtended) { }

        public virtual IEnumerable<Requirement> GetAll(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int languageId)
        {
            IEnumerable<Requirement> result;
            IEnumerable<SP_GET_PL_REQUIREMENT_Result> temp;

            temp = globalModel.SP_GET_PL_REQUIREMENT(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, languageId);

            result = temp
                .Select(r => new Requirement
                {
                    CorpId = r.Corp_Id,
                    RegionId = r.Region_Id,
                    CountryId = r.Country_Id,
                    DomesticregId = r.Domesticreg_Id,
                    StateProvId = r.State_Prov_Id,
                    CityId = r.City_Id,
                    OfficeId = r.Office_Id,
                    CaseSeqNo = r.Case_Seq_No,
                    HistSeqNo = r.Hist_Seq_No,
                    ContactId = r.Contact_Id,
                    RequirementCatId = r.Requirement_Cat_Id,
                    RequirementTypeId = r.Requirement_Type_Id,
                    RequirementId = r.Requirement_Id,
                    RequirementTypeDesc = r.Requirement_Type_Desc,
                    ContactRoleTypeId = r.Contact_Role_Type_Id,
                    RoleDesc = r.Role_Desc,
                    RequestedBy = r.Requested_By,
                    RequestedByUserName = r.Requested_By_UserName,
                    RequestedDate = r.Requested_Date,
                    ReceivedDate = r.Received_Date,
                    IsManual = r.IsManual,
                    SendToReinsurance = r.Send_To_Reinsurance,
                    RequirementDocId = r.Requirement_Doc_Id,
                    DocTypeId = r.Doc_Type_Id,
                    DocCategoryId = r.Doc_Category_Id,
                    DocumentId = r.Document_Id,
                    IsComplete = r.IsComplete,
                    RequirementCatDesc = r.Requirement_Cat_Desc
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Requirement> GetAllNewBusiness(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? requirementCatId, int languageId)
        {
            IEnumerable<Requirement> result;
            IEnumerable<SP_GET_PL_REQUIREMENT_NEWBUSINESS_Result> temp;

            temp = globalModel.SP_GET_PL_REQUIREMENT_NEWBUSINESS(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId
            , caseSeqNo, histSeqNo, contactId, requirementCatId, languageId);

            result = temp
                .Select(r => new Requirement
                {
                    CorpId = (int)r.Corp_Id,
                    RegionId = (int)r.Region_Id,
                    CountryId = (int)r.Country_Id,
                    DomesticregId = (int)r.Domesticreg_Id,
                    StateProvId = (int)r.State_Prov_Id,
                    CityId = (int)r.City_Id,
                    OfficeId = (int)r.Office_Id,
                    CaseSeqNo = (int)r.Case_Seq_No,
                    HistSeqNo = (int)r.Hist_Seq_No,
                    ContactId = (int)r.Contact_Id,
                    RequirementCatId = (int)r.Requirement_Cat_Id,
                    RequirementTypeId = (int)r.Requirement_Type_Id,
                    RequirementId = (int)r.Requirement_Id,
                    RequirementTypeDesc = r.Requirement_Type_Desc,
                    LastUpdate = r.LastUpdate,
                    RequirementDocId = r.Requirement_Doc_Id,
                    DocTypeId = r.Doc_Type_Id,
                    DocCategoryId = r.Doc_Category_Id,
                    DocumentId = r.Document_Id,
                    HasDocument = r.HasDocument.Value,
                    RequimentPolicyOnly = (bool)r.Requiment_Policy_Only,
                    ContactRoleDesc = r.Contact_Role_Desc,
                    ClientName = r.ClientName,
                    Is_Mandatory = (bool)r.Is_Mandatory
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Requirement.CategoryByContactRole> GetAllCategoryByContactRole(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo)
        {
            IEnumerable<Requirement.CategoryByContactRole> result;
            IEnumerable<SP_GET_PL_REQUIREMENT_CATEGORY_BY_CONTACT_ROLE_Result> temp;

            temp = globalModel.SP_GET_PL_REQUIREMENT_CATEGORY_BY_CONTACT_ROLE(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);

            result = temp
                .Select(ccr => new Requirement.CategoryByContactRole
                {
                    CorpId = ccr.Corp_Id,
                    RegionId = ccr.Region_Id,
                    CountryId = ccr.Country_Id,
                    DomesticregId = ccr.Domesticreg_Id,
                    StateProvId = ccr.State_Prov_Id,
                    CityId = ccr.City_Id,
                    OfficeId = ccr.Office_Id,
                    CaseSeqNo = ccr.Case_Seq_No,
                    HistSeqNo = ccr.Hist_Seq_No,
                    RequirementCatId = ccr.Requirement_Cat_Id,
                    ContactId = ccr.Contact_Id,
                    RequirementCatDesc = ccr.Requirement_Cat_Desc,
                    Roles = ccr.Roles,
                    Key = ccr.Requirement_Cat_Id.ToString() + '|' + ccr.Contact_Id.ToString()
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Requirement.Document> GetAllDocuments(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int contactId, int requirementCatId, int requirementTypeId, int requirementId)
        {
            IEnumerable<Requirement.Document> result;
            IEnumerable<SP_GET_PL_REQUIREMENT_DOCUMENTS_Result> temp;

            temp = globalModel.SP_GET_PL_REQUIREMENT_DOCUMENTS(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , contactId, requirementCatId, requirementTypeId, requirementId);

            result = temp
                .Select(rd => new Requirement.Document
                {
                    CorpId = rd.Corp_Id,
                    RegionId = rd.Region_Id,
                    CountryId = rd.Country_Id,
                    DomesticregId = rd.Domesticreg_Id,
                    StateProvId = rd.State_Prov_Id,
                    CityId = rd.City_Id,
                    OfficeId = rd.Office_Id,
                    CaseSeqNo = rd.Case_Seq_No,
                    HistSeqNo = rd.Hist_Seq_No,
                    ContactId = rd.Contact_Id,
                    RequirementCatId = rd.Requirement_Cat_Id,
                    RequirementTypeId = rd.Requirement_Type_Id,
                    RequirementId = rd.Requirement_Id,
                    RequirementDocId = rd.Requirement_Doc_Id,
                    DocumentTypeId = rd.Doc_Type_Id,
                    DocumentCategoryId = rd.Doc_Category_Id,
                    DocumentId = rd.Document_Id,
                    DocumentStatusId = rd.Document_Status_Id,
                    DocumentStatusDesc = rd.Document_Status_Desc,
                    DocumentBinary = null,
                    DocumentName = rd.Document_Name,
                    DocumentTypeDesc = rd.Doc_Type_Desc,
                    ContentType = rd.Content_Type,
                    Extension = rd.Extension
                })
                .ToArray();

            return
                result;
        }

        public virtual Requirement.Document GetDocument(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int contactId, int requirementCatId, int requirementTypeId, int requirementId, int requirementDocId)
        {
            Requirement.Document result;
            IEnumerable<SP_GET_PL_REQUIREMENT_DOCUMENT_Result> temp;

            temp = globalModel.SP_GET_PL_REQUIREMENT_DOCUMENT(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , contactId, requirementCatId, requirementTypeId, requirementId, requirementDocId);

            result = temp
                .Select(rd => new Requirement.Document
                {
                    CorpId = rd.Corp_Id,
                    RegionId = rd.Region_Id,
                    CountryId = rd.Country_Id,
                    DomesticregId = rd.Domesticreg_Id,
                    StateProvId = rd.State_Prov_Id,
                    CityId = rd.City_Id,
                    OfficeId = rd.Office_Id,
                    CaseSeqNo = rd.Case_Seq_No,
                    HistSeqNo = rd.Hist_Seq_No,
                    ContactId = rd.Contact_Id,
                    RequirementCatId = rd.Requirement_Cat_Id,
                    RequirementTypeId = rd.Requirement_Type_Id,
                    RequirementDocId = rd.Requirement_Doc_Id,
                    DocumentTypeId = rd.Doc_Type_Id,
                    DocumentCategoryId = rd.Doc_Category_Id,
                    DocumentId = rd.Document_Id,
                    DocumentStatusId = rd.Document_Status_Id,
                    DocumentStatusDesc = rd.Document_Status_Desc,
                    DocumentBinary = rd.Document_Binary,
                    DocumentName = rd.Document_Name,
                    DocumentTypeDesc = rd.Doc_Type_Desc,
                    ContentType = rd.Content_Type,
                    Extension = rd.Extension
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual Requirement.DocumentRequirementOnBase GetRequirementDocumentOnBase(string type, int coprId, int requirementCatId, int requirementTypeId)
        {
            Requirement.DocumentRequirementOnBase result;
            IEnumerable<SP_GET_REQUIREMENT_DOCUMENT_ONBASE_Result> temp;

            temp = globalModelExtended.SP_GET_REQUIREMENT_DOCUMENT_ONBASE(coprId, requirementCatId, requirementTypeId, type);

            result = temp
                .Select(rd => new Requirement.DocumentRequirementOnBase
                {
                    DescriptionName = rd.DescriptionName,
                    Doc_Type_Id = rd.Doc_Type_Id,
                    Doc_Category_Id = rd.Doc_Category_Id,
                    On_Base_Name_Key = rd.On_Base_Name_Key,
                    Clasification = rd.Clasification

                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual Requirement.Document GetSpecialDocument(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int documentType)
        {
            Requirement.Document result;
            IEnumerable<SP_GET_PL_REQUIREMENT_DOCUMENT_SPECIAL_Result> temp;

            temp = globalModel.SP_GET_PL_REQUIREMENT_DOCUMENT_SPECIAL(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, documentType);

            result = temp
                .Select(rd => new Requirement.Document
                {
                    CorpId = rd.Corp_Id,
                    RegionId = rd.Region_Id,
                    CountryId = rd.Country_Id,
                    DomesticregId = rd.Domesticreg_Id,
                    StateProvId = rd.State_Prov_Id,
                    CityId = rd.City_Id,
                    OfficeId = rd.Office_Id,
                    CaseSeqNo = rd.Case_Seq_No,
                    HistSeqNo = rd.Hist_Seq_No,
                    ContactId = rd.Contact_Id,
                    RequirementCatId = rd.Requirement_Cat_Id,
                    RequirementTypeId = rd.Requirement_Type_Id,
                    RequirementDocId = rd.Requirement_Doc_Id,
                    DocumentTypeId = rd.Doc_Type_Id.ConvertToNoNullable(),
                    DocumentCategoryId = rd.Doc_Category_Id.ConvertToNoNullable(),
                    DocumentId = rd.Document_Id.ConvertToNoNullable(),
                    DocumentStatusId = rd.Document_Status_Id,
                    DocumentStatusDesc = rd.Document_Status_Desc,
                    DocumentBinary = rd.Document_Binary,
                    DocumentName = rd.Document_Name,
                    DocumentTypeDesc = rd.Doc_Type_Desc,
                    ContentType = rd.Content_Type,
                    Extension = rd.Extension
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Requirement.Comunication> GetAllComunications(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int contactId, int requirementCatId, int requirementTypeId, int requirementId)
        {
            IEnumerable<Requirement.Comunication> result;
            IEnumerable<SP_GET_PL_REQUIREMENT_COMUNICATION_Result> temp;

            temp = globalModel.SP_GET_PL_REQUIREMENT_COMUNICATION(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , contactId, requirementCatId, requirementTypeId, requirementId);

            result = temp
                .Select(rc => new Requirement.Comunication
                {
                    CorpId = rc.Corp_Id,
                    RegionId = rc.Region_Id,
                    CountryId = rc.Country_Id,
                    DomesticregId = rc.Domesticreg_Id,
                    StateProvId = rc.State_Prov_Id,
                    CityId = rc.City_Id,
                    OfficeId = rc.Office_Id,
                    CaseSeqNo = rc.Case_Seq_No,
                    HistSeqNo = rc.Hist_Seq_No,
                    ContactId = rc.Contact_Id,
                    RequirementCatId = rc.Requirement_Cat_Id,
                    RequirementTypeId = rc.Requirement_Type_Id,
                    ComunicationId = rc.Comunication_Id,
                    Comment = rc.Comment,
                    CommentBy = rc.Comment_By,
                    CommentByUserName = rc.Comment_By_UserName,
                    CommentDate = rc.CommentDate
                })
                .ToArray();

            return
                result;
        }

        public virtual int DeleteDocument(Requirement.Document document)
        {
            int result;
            IEnumerable<SP_DELETE_PL_REQUIREMENT_DOCUMENT_Result> temp;

            result = 1;

            temp = globalModel.SP_DELETE_PL_REQUIREMENT_DOCUMENT(
                    document.CorpId,
                    document.RegionId,
                    document.CountryId,
                    document.DomesticregId,
                    document.StateProvId,
                    document.CityId,
                    document.OfficeId,
                    document.CaseSeqNo,
                    document.HistSeqNo,
                    document.ContactId,
                    document.RequirementCatId,
                    document.RequirementTypeId,
                    document.RequirementId,
                    document.RequirementDocId,
                    document.UserId
                    );

            return
                result;
        }

        public virtual int DeleteAll(Requirement policy)
        {
            int result;



            try
            {
                result = globalModel.SP_DELETE_PL_REQUIREMENT_ALL(
                               policy.CorpId,
                               policy.RegionId,
                               policy.CountryId,
                               policy.DomesticregId,
                               policy.StateProvId,
                               policy.CityId,
                               policy.OfficeId,
                               policy.CaseSeqNo,
                               policy.HistSeqNo,
                               policy.UserId
                               );
            }
            catch (Exception)
            {
                result = -2;
            }


            return
                result;
        }

        public virtual Requirement Set(Requirement requirement)
        {
            Requirement result;
            IEnumerable<SP_SET_PL_REQUIREMENT_Result> temp;

            temp = globalModel.SP_SET_PL_REQUIREMENT(
                    requirement.CorpId,
                    requirement.RegionId,
                    requirement.CountryId,
                    requirement.DomesticregId,
                    requirement.StateProvId,
                    requirement.CityId,
                    requirement.OfficeId,
                    requirement.CaseSeqNo,
                    requirement.HistSeqNo,
                    requirement.ContactId,
                    requirement.RequirementCatId,
                    requirement.RequirementTypeId,
                    requirement.RequirementId,
                    requirement.StepTypeId,
                    requirement.StepId,
                    requirement.StepCaseNo,
                    requirement.Automatic,
                    requirement.RequestedBy,
                    requirement.RequestedDate,
                    requirement.ReceivedDate,
                    requirement.IsManual,
                    requirement.SendToReinsurance,
                    requirement.Comment,
                    requirement.UserId
                    )
                .ToArray();

            result = temp
                .Select(r => new Requirement
                {
                    CorpId = r.Corp_Id.ConvertToNoNullable(),
                    RegionId = r.Region_Id.ConvertToNoNullable(),
                    CountryId = r.Country_Id.ConvertToNoNullable(),
                    DomesticregId = r.Domesticreg_Id.ConvertToNoNullable(),
                    StateProvId = r.State_Prov_Id.ConvertToNoNullable(),
                    CityId = r.City_Id.ConvertToNoNullable(),
                    OfficeId = r.Office_Id.ConvertToNoNullable(),
                    CaseSeqNo = r.Case_Seq_No.ConvertToNoNullable(),
                    HistSeqNo = r.Hist_Seq_No.ConvertToNoNullable(),
                    ContactId = r.Contact_Id.ConvertToNoNullable(),
                    RequirementCatId = r.Requirement_Cat_Id.ConvertToNoNullable(),
                    RequirementTypeId = r.Requirement_Type_Id.ConvertToNoNullable(),
                    RequirementId = r.Requirement_Id.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual int SetDocumentStatus(Requirement.Document document)
        {
            int result;
            IEnumerable<SP_SET_PL_REQUIREMENT_DOCUMENT_STATUS_Result> temp;

            result = 1;

            temp = globalModel.SP_SET_PL_REQUIREMENT_DOCUMENT_STATUS(
                document.CorpId,
                document.RegionId,
                document.CountryId,
                document.DomesticregId,
                document.StateProvId,
                document.CityId,
                document.OfficeId,
                document.CaseSeqNo,
                document.HistSeqNo,
                document.ContactId,
                document.RequirementCatId,
                document.RequirementTypeId,
                document.RequirementId,
                document.RequirementDocId,
                document.DocumentStatusId,
                document.UserId
                );

            return
                result;
        }

        public virtual int SendToReinsurance(Requirement requirement)
        {
            int result;
            IEnumerable<SP_SET_PL_REQUIREMENT_SEND_TO_REINSURANCE_Result> temp;

            result = 1;

            temp = globalModel.SP_SET_PL_REQUIREMENT_SEND_TO_REINSURANCE(
                requirement.CorpId,
                requirement.RegionId,
                requirement.CountryId,
                requirement.DomesticregId,
                requirement.StateProvId,
                requirement.CityId,
                requirement.OfficeId,
                requirement.CaseSeqNo,
                requirement.HistSeqNo,
                requirement.ContactId,
                requirement.RequirementCatId,
                requirement.RequirementTypeId,
                requirement.RequirementId,
                requirement.SendToReinsurance,
                requirement.UserId
                );

            return
                result;
        }

        public virtual int InsertDocument(Requirement.Document document)
        {
            int result;
            IEnumerable<SP_INSERT_PL_REQUIREMENT_DOCUMENT_Result> temp;

            result = 1;

            temp = globalModel.SP_INSERT_PL_REQUIREMENT_DOCUMENT(
                document.CorpId,
                document.RegionId,
                document.CountryId,
                document.DomesticregId,
                document.StateProvId,
                document.CityId,
                document.OfficeId,
                document.CaseSeqNo,
                document.HistSeqNo,
                document.ContactId,
                document.RequirementCatId,
                document.RequirementTypeId,
                document.RequirementId,
                document.DocumentStatusId,
                document.DocumentBinary,
                document.DocumentName,
                document.ClientSignatureDate,
                document.UserId
                );

            return
                result;
        }

        public virtual int InsertComunication(Requirement.Comunication comunication)
        {
            int result;
            IEnumerable<SP_INSERT_PL_REQUIREMENT_COMUNICATION_Result> temp;

            result = 1;

            temp = globalModel.SP_INSERT_PL_REQUIREMENT_COMUNICATION(
                comunication.CorpId,
                comunication.RegionId,
                comunication.CountryId,
                comunication.DomesticregId,
                comunication.StateProvId,
                comunication.CityId,
                comunication.OfficeId,
                comunication.CaseSeqNo,
                comunication.HistSeqNo,
                comunication.ContactId,
                comunication.RequirementCatId,
                comunication.RequirementTypeId,
                comunication.RequirementId,
                comunication.Comment,
                comunication.CommentBy,
                comunication.UserId
                );

            return
                result;
        }

        public virtual int SetReqAgent(Requirement reqAgent)
        {
            int result;
            IEnumerable<SP_SET_PL_REQUIREMENT_AGENT_Result> temp;

            result = 1;

            temp = globalModel.SP_SET_PL_REQUIREMENT_AGENT(
                reqAgent.CorpId,
                reqAgent.RegionId,
                reqAgent.CountryId,
                reqAgent.DomesticregId,
                reqAgent.StateProvId,
                reqAgent.CityId,
                reqAgent.OfficeId,
                reqAgent.CaseSeqNo,
                reqAgent.HistSeqNo,
                reqAgent.ContactId,
                reqAgent.RequirementCatId,
                reqAgent.RequirementTypeId,
                reqAgent.RequirementId,
                reqAgent.AgentId,
                reqAgent.UserId
                )
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Requirement.Compliance> GetComplianceContacts(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int languageId, int CompanyId)
        {
            IEnumerable<Requirement.Compliance> result;
            IEnumerable<SP_GET_PL_CONTACT_TO_COMPLIANCE_Result> temp;

            temp = globalModel.SP_GET_PL_CONTACT_TO_COMPLIANCE(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, languageId, CompanyId);

            result = temp
                .Select(r => new Requirement.Compliance
                {
                    ContactId = r.Contact_Id,
                    Nombre_Rol = r.Nombre_Rol,
                    ClientName = r.Full_Name,
                    Dob = r.Dob,
                    Identificacion = r.Identificacion,
                    TipoIdentificacion = r.TipoIdentificaion,
                    statusCompliance = r.Status_Cumplimiento
                    ,
                    Address = r.Direccion
                    ,
                    CountryOfBirth = r.Pais_Nacimiento
                    ,
                    ID = r.ID
                    ,
                    IDType = r.IDType
                })
                .ToArray();
            return result;
        }

        public virtual IEnumerable<Requirement.Product> GetRequirementProduct(Requirement.Product.Key parameter)
        {
            IEnumerable<Requirement.Product> result;
            IEnumerable<SP_GET_PL_POLICY_PRODUCT_REQUIREMENT_Result> temp;

            temp = globalModel.SP_GET_PL_POLICY_PRODUCT_REQUIREMENT(
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticRegId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.ContactId
                    )
                .ToArray();

            result = temp
                .Select(rq => new Requirement.Product
                {
                    CorpId = rq.Corp_Id,
                    RequirementCatId = rq.Requirement_Cat_Id,
                    RequirementCatDesc = rq.Requirement_Cat_Desc,
                    RequirementTypeId = rq.Requirement_Type_Id,
                    RequirementTypeDesc = rq.Requirement_Type_Desc,
                    Automatic = rq.Automatic,
                    RequimentPolicyOnly = rq.Requiment_Policy_Only,
                    RequimentAssingToInsured = rq.Requiment_Assing_To_Insured,
                    RequimentOnBaseNameKey = rq.Requiment_On_Base_Name_Key,
                    RequirementTypeSubType = rq.Requirement_Type_SubType,
                    RegionId = rq.Region_Id,
                    CountryId = rq.Country_Id,
                    DomesticregId = rq.Domesticreg_Id,
                    StateProvId = rq.State_Prov_Id,
                    CityId = rq.City_Id,
                    OfficeId = rq.Office_Id,
                    CaseSeqNo = rq.Case_Seq_No,
                    HistSeqNo = rq.Hist_Seq_No,
                    ContactId = rq.Contact_Id,
                    RequirementId = rq.Requirement_Id,
                    RequirementDocId = rq.Requirement_Doc_Id,
                    DocTypeId = rq.Doc_Type_Id,
                    DocCategoryId = rq.Doc_Category_Id,
                    DocumentId = rq.Document_Id,
                    IsValid = rq.IsValid,
                    EndorsementBeneficiary = rq.Endorsement_beneficiary,
                    FunctionalityId = rq.Functionality_Id,
                    FunctionalitySeqNo = rq.Functionality_Seq_No,
                    IsMandatory = rq.Is_Mandatory,
                    InsuredId = rq.Insured_Id,
                    SeqId = rq.Seq_Id,
                    UniqueId = rq.Unique_Id,
                    UploadById = rq.UploadById,
                    UploadByUserName = rq.UploadByUserName,
                    ValidById = rq.ValidById,
                    ValidByUserName = rq.ValidByUserName,
                    ValidByDate = rq.ValidByDate,
                    AssingTo = rq.AssingTo
                })
                .ToArray();

            return
                result;
        }
    }
}