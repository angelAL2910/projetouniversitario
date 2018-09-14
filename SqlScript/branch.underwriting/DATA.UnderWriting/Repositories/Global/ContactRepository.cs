﻿using System.Collections.Generic;
using System.Linq;
using DATA.UnderWriting.Data;
using DATA.UnderWriting.Repositories.Base;
using Entity.UnderWriting.Entities;

namespace DATA.UnderWriting.Repositories.Global
{
    public class ContactRepository : GlobalRepository
    {
        public ContactRepository(GlobalEntityDataModel globalModel, GlobalEntities globalModelExtended) : base(globalModel, globalModelExtended) { }

        public virtual Contact GetContact(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? contactRoleTypeId, int languageId)
        {
            Contact result;
            IEnumerable<Contact.Address> adresses;
            IEnumerable<Contact.Phone> phones;
            IEnumerable<Contact.Email> emails;
            IEnumerable<SP_GET_CONTACT_BASIC_INFORMATION_Result> temp;

            temp = globalModel.SP_GET_CONTACT_BASIC_INFORMATION(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, contactRoleTypeId,languageId);

            result = temp
                .Select(c => new Contact
                {
                    ContactId = c.Contact_Id,
                    CustomerNumber = c.Customer_Number,
                    Age = c.Age,
                    NearAge = c.NearAge,
                    Gender = c.Gender,
                    MaritalStatId = c.Marital_Stat_Id,
                    RegionOfResidenceId = c.Region_Of_Residence_Id,
                    CountryOfResidenceId = c.Country_Of_Residence_Id,
                    DomesticRegOfResidenceId = c.DomesticReg_Of_Residence_Id,
                    StateOfResidenceId = c.State_Of_Residence_Id,
                    CityOfResidenceId = c.City_Of_Residence_Id,
                    RegionOfBirthId = c.Region_Of_Birth_Id,
                    CountryOfBirthId = c.Country_Of_Birth_Id,
                    Weight = c.Weigth,
                    WeightTypeId = c.Weigth_Type_Id,
                    Height = c.Height,
                    HeightTypeId = c.Heigth_Type_Id,
                    BloodTypeId = c.Blood_Type_Id,
                    Smoker = c.Smoker,
                    LineOfBusiness = c.Line_Of_Business,
                    LineOfBusiness2 = c.Line_Of_Business_2,
                    LaborTasks = c.Labor_tasks,
                    IsCompany = c.Is_Company.ConvertToNoNullable(),
                    LengthWorkYear = c.Length_Work_Year,
                    LengthWorkMonth = c.Length_Work_Month,
                    RelationshiptoOwner = c.Relationship_to_Owner,
                    RelationshiptoAgent = c.Relationship_to_Agent,
                    AnnualPersonalIncome = c.Annual_Personal_Income,
                    AnnualFamilyIncome = c.Annual_Family_Income,
                    OccupGroupTypeId = c.OccupGroup_Type_Id,
                    OccupationId = c.Occupation_Id,
                    BackgroundCheckResult = c.BackgroundCheckResult,
                    SeqNo = c.Seq_No,
                    ContactIdType = c.Contact_Id_Type,
                    Id = c.Id,
                    ExpireDate = c.Expire_date,
                    CountryIssuedBy = c.Country_Issued_By,
                    IssuedBy = c.Issued_By,
                    DocumentId = c.Document_Id,
                    ContactRoleTypeId = c.Contact_Role_Type_Id,
                    FirstName = c.First_Name,
                    MiddleName = c.Middle_Name,
                    FirstLastName = c.Lastname,
                    SecondLastName = c.Second_Lastname,
                    InstitutionalName = c.Institutional_Name,
                    InstitutionalCountryId = c.Institutional_Country_Id,
                    InstitutionalPrincipal = c.Institutional_Principal,
                    InstitutionalPositionAtCompany = c.Institutional_Position_At_Company,
                    Dob = c.Dob,
                    ContactTypeId = c.Contact_Type_Id,
                    ContactTypeDesc = c.Contact_Type_Desc,
                    CompanyName = c.Company_Name,
                    DeceaseCause = c.Decease_Cause,
                    DeceaseDate = c.Decease_Date,
                    NotifiedDate = c.Notified_Date,
                    CompletedDate = c.Completed_Date,
                    Remarks = c.Remarks,
                    ReferredByRelationshipId = c.Referred_By_Relationship_Id,
                    ReferredByContactId = c.Referred_By_Contact_Id,
                    Citizenships = GetCitizenshipByContact(c.Contact_Id),
                    CitizenQuestions = GetCitizenQuestionByContact(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, c.Contact_Id, languageId),
                    SocialExposures = GetSocialExposureByContact(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, c.Contact_Id, languageId),
                    SocialExposureRelationships = GetSocialExposureRelationshipByContact(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, c.Contact_Id, languageId),
                    CountryOfResidenceDesc = c.Country_Of_Residence_Desc,
                    CountryOfBirthDesc = c.Country_Of_Birth_Desc,
                    NcfTypeId = c.Ncf_Type_Id,
                    FullName = c.Full_Name,
                    InvoiceTypeId = c.Invoice_Type_Id,
                    finalBeneficiaryOptionId = c.Final_Beneficiary_Option_Id,
                    companyStructureId = c.Company_Structure_Id,
                    companyActivityId = c.Company_Activity_Id,
                    Representative = c.Representative,
                    RepresentativeIdentificationTypeId = c.Representative_Identification_Type_Id,
                    RepresentativeIdentification = c.Representative_Identification
                })
                .FirstOrDefault();

            if (result != null)
            {
                GetCommunicationAll(coprId, result.ContactId, languageId, out adresses, out phones, out emails);

                result.Addresses = adresses;
                result.Phones = phones;
                result.Emails = emails;
            }

            return
                result;
        }

        public virtual Contact GetContact(int coprId, int contactId, int languageId)
        {
            Contact result;
            IEnumerable<Contact.Address> adresses;
            IEnumerable<Contact.Phone> phones;
            IEnumerable<Contact.Email> emails;
            IEnumerable<SP_GET_CONTACT_INFORMATION_Result> temp;

            temp = globalModel.SP_GET_CONTACT_INFORMATION(contactId);

            result = temp
                .Select(c => new Contact
                {
                    ContactId = c.Contact_Id,
                    CustomerNumber = c.Customer_Number,
                    Age = c.Age,
                    NearAge = c.NearAge,
                    Gender = c.Gender,
                    MaritalStatId = c.Marital_Stat_Id,
                    RegionOfResidenceId = c.Region_Of_Residence_Id,
                    CountryOfResidenceId = c.Country_Of_Residence_Id,
                    DomesticRegOfResidenceId = c.DomesticReg_Of_Residence_Id,
                    StateOfResidenceId = c.State_Of_Residence_Id,
                    CityOfResidenceId = c.City_Of_Residence_Id,
                    RegionOfBirthId = c.Region_Of_Birth_Id,
                    CountryOfBirthId = c.Country_Of_Birth_Id,
                    Weight = c.Weigth,
                    WeightTypeId = c.Weigth_Type_Id,
                    Height = c.Height,
                    HeightTypeId = c.Heigth_Type_Id,
                    Smoker = c.Smoker,
                    LineOfBusiness = c.Line_Of_Business,
                    LineOfBusiness2 = c.Line_Of_Business_2,
                    LaborTasks = c.Labor_tasks,
                    IsCompany = c.Is_Company.ConvertToNoNullable(),
                    LengthWorkYear = c.Length_Work_Year,
                    LengthWorkMonth = c.Length_Work_Month,
                    RelationshiptoOwner = c.Relationship_to_Owner,
                    RelationshiptoOwnerDesc = c.Relationship_Desc,
                    RelationshiptoAgent = c.Relationship_to_Agent,
                    AnnualPersonalIncome = c.Annual_Personal_Income,
                    AnnualFamilyIncome = c.Annual_Family_Income,
                    OccupGroupTypeId = c.OccupGroup_Type_Id,
                    OccupationId = c.Occupation_Id,
                    Occupation_Desc = c.Occupation_Desc,
                    Occupation_Group_Desc = c.Occupation_Group_Desc,
                    SeqNo = c.Seq_No,
                    ContactIdType = c.Contact_Id_Type,
                    ContactTypeId = c.Contact_Type_Id,
                    Id = c.Id,
                    ExpireDate = c.Expire_date,
                    IssuedBy = c.Issued_By,
                    DocumentId = c.Document_Id,
                    FirstName = c.First_Name,
                    MiddleName = c.Middle_Name,
                    FirstLastName = c.Lastname,
                    SecondLastName = c.Second_Lastname,
                    InstitutionalName = c.Institutional_Name,
                    InstitutionalCountryId = c.Institutional_Country_Id,
                    InstitutionalPrincipal = c.Institutional_Principal,
                    InstitutionalPositionAtCompany = c.Institutional_Position_At_Company,
                    Dob = c.Dob,
                    ContactTypeDesc = c.Contact_Type_Desc,
                    CompanyName = c.Company_Name,
                    ReferredByRelationshipId = c.Referred_By_Relationship_Id,
                    ReferredByContactId = c.Referred_By_Contact_Id,
                    Citizenships = GetCitizenshipByContact(c.Contact_Id),
                    CitizenQuestions = null,
                    SocialExposures = null,
                    SocialExposureRelationships = null,
                    CountryOfResidenceDesc = c.Country_Of_Residence_Desc,
                    StudentStatusId = c.Student_Status_Id,
                    NcfTypeId = c.Ncf_Type_Id,
                    FullName = c.Full_Name,
                    TipoRiesgoNameKey = c.Tipo_Riesgo_Name_Key,
                    InvoiceTypeId = c.Invoice_Type_Id,
                    CountryOfBirthDesc = c.Country_Of_Birth_Desc,
                    finalBeneficiaryOptionId = c.Final_Beneficiary_Option_Id,
                    pepFormularyOptionId = c.Pep_Formulary_Option_Id,
                    companyStructureId = c.Company_Structure_Id,
                    companyActivityId = c.Company_Activity_Id,
                    FinalBeneficiaryIncludeForCompanyOrNot = c.Final_Beneficiary_IncludeForCompanyOrNot,
                    FinalBeneficiaryAllowed = c.Final_Beneficiary_Allowed,
                    PepFormularyAllowed = c.Pep_Formulary_Allowed,
                    Representative = c.Representative,
                    RepresentativeIdentificationTypeId = c.Representative_Identification_Type_Id,
                    RepresentativeIdentification = c.Representative_Identification,
                    LegalContactId = c.Legal_Contact_Id,
                    UbicacionId = c.Ubicacion_Id,
                    homeOwner = c.Home_Owner,
                    qtyPersonsDepend = c.QtyPersonsDepend,
                    qtyEmployees = c.QtyEmployees,
                    segment = c.Segment,
                    linked = c.Linked,
                    InvoiceTypeDesc = c.Invoice_Type_Desc,
                    CreditCardTypeId = c.Credit_Card_Type_Id,
                    CreditCardNumberKey = c.Credit_Card_Number_Key,
                    CreditCardNumber = c.Credit_Card_Number,
                    ExpirationDateYear = c.Expiration_Date_Year,
                    ExpirationDateMonth = c.Expiration_Date_Month,
                    CardHolder = c.Card_Holder,
                    CreditCardMask = c.Credit_Card_Mask,
                    KcoUniqueId = c.Kco_Unique_Id,
                    WorkAddress = c.WorkAddress,
                    PlaceOfBirth = c.PlaceOfBirth,
                    TypeOfPerson = c.TypeOfPerson,
                    ManagerName = c.ManagerName,
                    ManagerPepOptionId = c.ManagerPepOptionId
                })
                .FirstOrDefault();

            if (result != null)
            {
                GetCommunicationAll(coprId, result.ContactId, languageId, out adresses, out phones, out emails);

                result.Addresses = adresses;
                result.Phones = phones;
                result.Emails = emails;
            }

            return
                result;
        }

        public virtual Contact GetContactSummary(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? contactRoleTypeId)
        {
            Contact result;
            IEnumerable<SP_GET_CONTACT_SUMMARY_INFORMATION_Result> temp;

            temp = globalModel.SP_GET_CONTACT_SUMMARY_INFORMATION(coprId, regionId, countryId, domesticRegId
                    , stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, contactRoleTypeId);

            result = temp
                .Select(c => new Contact
                {
                    ContactId = c.ContactId,
                    FirstName = c.FirstName,
                    MiddleName = c.MiddleName,
                    FirstLastName = c.FirstLastName,
                    SecondLastName = c.SecondLastName,
                    Dob = c.DateOfBirth,
                    Age = c.Age.ConvertToNoNullable(),
                    NearAge = c.NearAge.ConvertToNoNullable(),
                    ContactTypeDescription = c.ContactTypeDescription,
                    Gender = c.Gender
                })
                .FirstOrDefault();
            return
                result;
        }

        public virtual int UpdateContact(Contact contact)
        {
            int result;
            IEnumerable<SP_SET_CONTACT_BASIC_INFORMATION_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_CONTACT_BASIC_INFORMATION(
                contact.ContactId,
                contact.FirstName,
                contact.MiddleName,
                contact.FirstLastName,
                contact.SecondLastName,
                contact.InstitutionalName,
                contact.InstitutionalCountryId,
                contact.InstitutionalPrincipal,
                contact.InstitutionalPositionAtCompany,
                contact.Dob,
                contact.Age,
                contact.Gender,
                contact.MaritalStatId,
                contact.RegionOfResidenceId,
                contact.CountryOfResidenceId,
                contact.DomesticRegOfResidenceId,
                contact.StateOfResidenceId,
                contact.CityOfResidenceId,
                contact.RegionOfBirthId,
                contact.CountryOfBirthId,
                contact.Weight,
                contact.WeightTypeId,
                contact.Height,
                contact.HeightTypeId,
                contact.BloodTypeId,
                contact.LineOfBusiness,
                contact.LineOfBusiness2,
                contact.CompanyName,
                contact.LengthWorkYear,
                contact.LengthWorkMonth,
                contact.LaborTasks,
                contact.IsCompany,
                contact.OccupGroupTypeId,
                contact.OccupationId,
                contact.StudentStatusId,
                contact.RelationshiptoAgent,
                contact.RelationshiptoOwner,
                contact.AnnualFamilyIncome,
                contact.AnnualPersonalIncome,
                contact.Smoker,
                contact.ReferredByRelationshipId,
                contact.ReferredByContactId,
                contact.SeqNo,
                contact.Id,
                contact.ExpireDate,
                contact.CountryIssuedBy,
                contact.IssuedBy,
                contact.ContactIdType,
                contact.NcfTypeId,
                contact.TipoRiesgoNameKey,
                contact.InvoiceTypeId,
                contact.ForeignLicense,
                contact.SourceId,
                contact.ModifyUser,
                contact.LegalContactId,
                contact.finalBeneficiaryOptionId,
                contact.pepFormularyOptionId,
                contact.companyStructureId,
                contact.companyActivityId,
                contact.Representative,
                contact.RepresentativeIdentificationTypeId,
                contact.RepresentativeIdentification,
                contact.homeOwner,
                contact.qtyPersonsDepend,
                contact.qtyEmployees,
                contact.linked,
                contact.segment,
                contact.CreditCardTypeId,
                contact.CreditCardNumberKey,
                contact.CreditCardNumber,
                contact.ExpirationDateYear,
                contact.ExpirationDateMonth,
                contact.CardHolder,
                contact.KcoUniqueId, string.Empty, null,
                contact.WorkAddress,
                contact.PlaceOfBirth,
                contact.TypeOfPerson,
                contact.ManagerName,
                contact.ManagerPepOptionId
                );

            return
                result;
        }

        public virtual int InsertContact(Contact contact)
        {
            int result;
            IEnumerable<int?> temp;

            temp = globalModel.SP_INSERT_NEW_CONTACT(
               contact.CorpId,
               contact.AgentId,
               contact.ContactTypeId,
               contact.FirstName,
               contact.MiddleName,
               contact.FirstLastName,
               contact.SecondLastName,
               contact.InstitutionalName,
               contact.InstitutionalCountryId,
               contact.InstitutionalPrincipal,
               contact.InstitutionalPositionAtCompany,
               contact.Dob,
               contact.Age,
               contact.Gender,
               contact.MaritalStatId,
               contact.CountryOfResidenceId,
               contact.DomesticRegOfResidenceId,
               contact.StateOfResidenceId,
               contact.CityOfResidenceId,
               contact.CountryOfBirthId,
               contact.LineOfBusiness,
               contact.LineOfBusiness2,
               contact.CompanyName,
               contact.LengthWorkYear,
               contact.LengthWorkMonth,
               contact.LaborTasks,
               contact.IsCompany,
               contact.OccupGroupTypeId,
               contact.OccupationId,
               contact.RelationshiptoAgent,
               contact.RelationshiptoOwner,
               contact.AnnualFamilyIncome,
               contact.AnnualPersonalIncome,
               contact.Smoker,
               contact.ReferredByRelationshipId,
               contact.ReferredByContactId,
               contact.NcfTypeId,
               contact.finalBeneficiaryOptionId,
               contact.pepFormularyOptionId,
               contact.companyStructureId,
               contact.companyActivityId,
               string.Empty,
               null,
               string.Empty,
               contact.CreateUser, null, null, null, null
               )
               .ToArray();

            result = temp != null && temp.Any()
                        ? temp.FirstOrDefault().ConvertToNoNullable()
                        : -1;

            return
                result;
        }

        public virtual IEnumerable<Contact.Citizenship> GetCitizenshipByContact(int contactId)
        {
            IEnumerable<Contact.Citizenship> result;
            IEnumerable<SP_GET_CONTACT_CITIZENSHIP_Result> temp;

            temp = globalModel.SP_GET_CONTACT_CITIZENSHIP(contactId);

            result = temp
                .Select(cc => new Contact.Citizenship
                {
                    ContactId = cc.Contact_Id,
                    GlobalCountryId = cc.Global_Country_Id,
                    GlobalCountryDesc = cc.Global_Country_Desc
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Contact.CitizenQuestion> GetCitizenQuestionByContact(
           int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? contactId
            , int languageId)
        {
            IEnumerable<Contact.CitizenQuestion> result;
            IEnumerable<SP_GET_CITIZEN_QUESTIONS_Result> temp;

            temp = globalModel.SP_GET_CITIZEN_QUESTIONS(coprId, regionId, countryId, domesticRegId
                , stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, languageId);

            result = temp
                .Select(ccq => new Contact.CitizenQuestion
                {
                    CorpId = ccq.Corp_Id,
                    RegionId = ccq.Region_Id,
                    CountryId = ccq.Country_Id,
                    DomesticregId = ccq.Domesticreg_Id,
                    StateProvId = ccq.State_Prov_Id,
                    CityId = ccq.City_Id,
                    OfficeId = ccq.Office_Id,
                    CaseSeqNo = ccq.Case_Seq_No,
                    HistSeqNo = ccq.Hist_Seq_No,
                    ContactId = ccq.Contact_Id,
                    CitizenQuestId = ccq.Citizen_Quest_Id,
                    CitizenQuestDesc = ccq.Citizen_Quest_Desc,
                    CitizenQuestAnswer = ccq.Citizen_Quest_Answer,
                    NameKey = ccq.Name_Key
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Contact.SocialExposure> GetSocialExposureByContact(
           int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? contactId
            , int languageId)
        {
            IEnumerable<Contact.SocialExposure> result;
            IEnumerable<SP_GET_SOCIAL_EXPOSURE_CONTACT_Result> temp;

            temp = globalModel.SP_GET_SOCIAL_EXPOSURE_CONTACT(coprId, regionId, countryId, domesticRegId
                , stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, languageId);

            result = temp
                .Select(cse => new Contact.SocialExposure
                {
                    CorpId = cse.Corp_Id,
                    RegionId = cse.Region_Id,
                    CountryId = cse.Country_Id,
                    DomesticregId = cse.Domesticreg_Id,
                    StateProvId = cse.State_Prov_Id,
                    CityId = cse.City_Id,
                    OfficeId = cse.Office_Id,
                    CaseSeqNo = cse.Case_Seq_No,
                    HistSeqNo = cse.Hist_Seq_No,
                    ContactId = cse.Contact_Id,
                    SocialFunctionTypeId = cse.Social_Function_Type_Id,
                    SocialTypeDesc = cse.Social_Type_Desc,
                    SocialFunctionTypePosition = cse.Social_Function_Type_Position,
                    NameKey = cse.Name_Key
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Contact.SocialExposureRelationship> GetSocialExposureRelationshipByContact(
             int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int? contactId
            , int languageId)
        {
            IEnumerable<Contact.SocialExposureRelationship> result;
            IEnumerable<SP_GET_SOCIAL_EXPOSURE_RELATIONSHIP_Result> temp;

            temp = globalModel.SP_GET_SOCIAL_EXPOSURE_RELATIONSHIP(coprId, regionId, countryId, domesticRegId
                , stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, languageId);

            result = temp
                .Select(cser => new Contact.SocialExposureRelationship
                {
                    CorpId = cser.Corp_Id,
                    RegionId = cser.Region_Id,
                    CountryId = cser.Country_Id,
                    DomesticregId = cser.Domesticreg_Id,
                    StateProvId = cser.State_Prov_Id,
                    CityId = cser.City_Id,
                    OfficeId = cser.Office_Id,
                    CaseSeqNo = cser.Case_Seq_No,
                    HistSeqNo = cser.Hist_Seq_No,
                    ContactId = cser.Contact_Id,
                    SocialFunctionTypeId = cser.Social_Function_Type_Id,
                    SocialTypeDesc = cser.Social_Type_Desc,
                    SocFuncRelName = cser.Soc_Func_Rel_Name,
                    SocialFunctionTypePosition = cser.Social_Function_Type_Position,
                    NameKey = cser.Name_Key
                })
                .ToArray();

            return
                result;
        }

        public virtual int SetContactCitizenship(Contact.Citizenship citizenship)
        {
            int result;
            IEnumerable<SP_SET_CONTACT_CITIZENSHIP_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_CONTACT_CITIZENSHIP(
                citizenship.ContactId,
                citizenship.GlobalCountryId,
                citizenship.Status,
                citizenship.CreateUser,
                citizenship.ModifyUser
                );

            return
                result;
        }

        public virtual int SetCitizenQuestionByContact(Contact.CitizenQuestion question)
        {
            int result;
            IEnumerable<SP_SET_CITIZEN_QUESTIONS_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_CITIZEN_QUESTIONS(
                    question.CorpId,
                    question.RegionId,
                    question.CountryId,
                    question.DomesticregId,
                    question.StateProvId,
                    question.CityId,
                    question.OfficeId,
                    question.CaseSeqNo,
                    question.HistSeqNo,
                    question.ContactId,
                    question.CitizenQuestId,
                    question.CitizenQuestAnswer,
                    question.CreateUser,
                    question.ModifyUser
                );

            return
                result;
        }

        public virtual int SetCitizenQuestionByContactJuridico(Contact.CitizenQuestion question)
        {
            int result;
            IEnumerable<SP_SET_CITIZEN_QUESTIONS_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_CITIZEN_QUESTIONS(
                    question.CorpId,
                    question.RegionId,
                    question.CountryId,
                    question.DomesticregId,
                    question.StateProvId,
                    question.CityId,
                    question.OfficeId,
                    question.CaseSeqNo,
                    question.HistSeqNo,
                    question.ContactId,
                    question.CitizenQuestId,
                    question.CitizenQuestAnswer,
                    question.CreateUser,
                    question.ModifyUser
                );

            return
                result;
        }

        public virtual int SetSocialExposureByContact(Contact.SocialExposure socialExposure)
        {
            int result;
            IEnumerable<SP_SET_SOCIAL_EXPOSURE_CONTACT_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_SOCIAL_EXPOSURE_CONTACT(
                    socialExposure.CorpId,
                    socialExposure.RegionId,
                    socialExposure.CountryId,
                    socialExposure.DomesticregId,
                    socialExposure.StateProvId,
                    socialExposure.CityId,
                    socialExposure.OfficeId,
                    socialExposure.CaseSeqNo,
                    socialExposure.HistSeqNo,
                    socialExposure.ContactId,
                    socialExposure.SocialFunctionTypeId,
                    socialExposure.SocialFunctionTypePosition,
                    socialExposure.CreateUser,
                    socialExposure.ModifyUser
                );

            return
                result;
        }

        public virtual int SetSocialExposureByContactJuridico(Contact.SocialExposure socialExposure)
        {
            int result;
            IEnumerable<SP_SET_SOCIAL_EXPOSURE_CONTACT_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_SOCIAL_EXPOSURE_CONTACT(
                    socialExposure.CorpId,
                    socialExposure.RegionId,
                    socialExposure.CountryId,
                    socialExposure.DomesticregId,
                    socialExposure.StateProvId,
                    socialExposure.CityId,
                    socialExposure.OfficeId,
                    socialExposure.CaseSeqNo,
                    socialExposure.HistSeqNo,
                    socialExposure.ContactId,
                    socialExposure.SocialFunctionTypeId,
                    socialExposure.SocialFunctionTypePosition,
                    socialExposure.CreateUser,
                    socialExposure.ModifyUser
                );

            return
                result;
        }

        public virtual int SetSocialExposureRelationshipByContact(Contact.SocialExposureRelationship socialExposureRelationship)
        {
            int result;
            IEnumerable<SP_SET_SOCIAL_EXPOSURE_RELATIONSHIP_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_SOCIAL_EXPOSURE_RELATIONSHIP(
                    socialExposureRelationship.CorpId,
                    socialExposureRelationship.RegionId,
                    socialExposureRelationship.CountryId,
                    socialExposureRelationship.DomesticregId,
                    socialExposureRelationship.StateProvId,
                    socialExposureRelationship.CityId,
                    socialExposureRelationship.OfficeId,
                    socialExposureRelationship.CaseSeqNo,
                    socialExposureRelationship.HistSeqNo,
                    socialExposureRelationship.ContactId,
                    socialExposureRelationship.SocialFunctionTypeId,
                    socialExposureRelationship.SocFuncRelName,
                    socialExposureRelationship.SocialFunctionTypePosition,
                    socialExposureRelationship.CreateUser,
                    socialExposureRelationship.ModifyUser
                );

            return
                result;
        }

        public virtual int SetSocialExposureRelationshipByContactJuridico(Contact.SocialExposureRelationship socialExposureRelationship)
        {
            int result;
            IEnumerable<SP_SET_SOCIAL_EXPOSURE_RELATIONSHIP_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_SOCIAL_EXPOSURE_RELATIONSHIP(
                    socialExposureRelationship.CorpId,
                    socialExposureRelationship.RegionId,
                    socialExposureRelationship.CountryId,
                    socialExposureRelationship.DomesticregId,
                    socialExposureRelationship.StateProvId,
                    socialExposureRelationship.CityId,
                    socialExposureRelationship.OfficeId,
                    socialExposureRelationship.CaseSeqNo,
                    socialExposureRelationship.HistSeqNo,
                    socialExposureRelationship.ContactId,
                    socialExposureRelationship.SocialFunctionTypeId,
                    socialExposureRelationship.SocFuncRelName,
                    socialExposureRelationship.SocialFunctionTypePosition,
                    socialExposureRelationship.CreateUser,
                    socialExposureRelationship.ModifyUser
                );

            return
                result;
        }

        public virtual void GetCommunicationAll(int corpId, int contactId, int languageId, out IEnumerable<Contact.Address> addresses, out IEnumerable<Contact.Phone> phones, out IEnumerable<Contact.Email> emails)
        {
            List<Contact.Address> rAdresses;
            List<Contact.Phone> rPhones;
            List<Contact.Email> rEmails;
            IEnumerable<SP_GET_CONTACT_COMMUNICATION_INFORMATION_Result> temp;

            temp = globalModel.SP_GET_CONTACT_COMMUNICATION_INFORMATION(corpId, contactId, languageId);

            rAdresses = new List<Contact.Address>(1);
            rPhones = new List<Contact.Phone>(1);
            rEmails = new List<Contact.Email>(1);

            foreach (SP_GET_CONTACT_COMMUNICATION_INFORMATION_Result row in temp)
            {
                switch (row.CommunicationType)
                {
                    case "Address":
                        rAdresses.Add(new Contact.Address
                        {
                            CorpId = row.Corp_Id,
                            DirectoryId = row.Directory_Id,
                            DirectoryDetailId = row.Dir_Detail_Id,
                            CommunicationTypeId = row.Comm_Type_Id,
                            DirectoryTypeId = row.Directory_Type_Id,
                            DirectoryTypeDesc = row.Dir_Type_Short_Desc,
                            StreetAddress = row.Address,
                            RegionId = row.Region_Id.ConvertToNoNullable(),
                            CountryId = row.Country_Id.ConvertToNoNullable(),
                            CountryDesc = row.Global_Country_Desc,
                            DomesticregId = row.Domestic_Region_Id.ConvertToNoNullable(),
                            StateProvId = row.State_Prov_Id.ConvertToNoNullable(),
                            StateProvDesc = row.State_Prov_Desc,
                            CityId = row.City_Id.ConvertToNoNullable(),
                            CityDesc = row.City_Desc,
                            ZipCode = row.Zip_Code,
                            IsPrimary = row.isPrimary,
                            MunicipioId = row.Municipio_Id,
                            MunicipioDesc = row.Municipio_Desc
                        });
                        break;
                    case "Phone":
                        rPhones.Add(new Contact.Phone
                        {
                            CorpId = row.Corp_Id,
                            DirectoryId = row.Directory_Id,
                            DirectoryDetailId = row.Dir_Detail_Id,
                            CommunicationTypeId = row.Comm_Type_Id,
                            DirectoryTypeId = row.Directory_Type_Id,
                            DirectoryTypeDesc = row.Dir_Type_Short_Desc,
                            CountryCode = row.Phone_Prefix,
                            AreaCode = row.Area_Code,
                            PhoneNumber = row.Phone_Number,
                            PhoneExt = row.Phone_Ext,
                            PersonToContact = row.Person_To_Contact,
                            IsPrimary = row.isPrimary
                        });
                        break;
                    case "Email":
                        rEmails.Add(new Contact.Email
                        {
                            CorpId = row.Corp_Id,
                            DirectoryId = row.Directory_Id,
                            DirectoryDetailId = row.Dir_Detail_Id,
                            CommunicationTypeId = row.Comm_Type_Id,
                            DirectoryTypeId = row.Directory_Type_Id,
                            DirectoryTypeDesc = row.Dir_Type_Short_Desc,
                            EmailAdress = row.Address,
                            IsPrimary = row.isPrimary
                        });
                        break;
                    default:
                        break;
                }
            }

            addresses = rAdresses.ToArray();
            phones = rPhones.ToArray();
            emails = rEmails.ToArray();
        }

        public virtual void GetCommunicationAllJuridico(int corpId, int Agent_Legal, int languageId, out IEnumerable<Contact.Address> addresses, out IEnumerable<Contact.Phone> phones, out IEnumerable<Contact.Email> emails)
        {
            List<Contact.Address> rAdresses;
            List<Contact.Phone> rPhones;
            List<Contact.Email> rEmails;
            IEnumerable<SP_GET_CONTACT_COMMUNICATION_INFORMATION_Result> temp;

            temp = globalModel.SP_GET_CONTACT_COMMUNICATION_INFORMATION(corpId, Agent_Legal, languageId);

            rAdresses = new List<Contact.Address>(1);
            rPhones = new List<Contact.Phone>(1);
            rEmails = new List<Contact.Email>(1);

            foreach (SP_GET_CONTACT_COMMUNICATION_INFORMATION_Result row in temp)
            {
                switch (row.CommunicationType)
                {
                    case "Address":
                        rAdresses.Add(new Contact.Address
                        {
                            CorpId = row.Corp_Id,
                            DirectoryId = row.Directory_Id,
                            DirectoryDetailId = row.Dir_Detail_Id,
                            CommunicationTypeId = row.Comm_Type_Id,
                            DirectoryTypeId = row.Directory_Type_Id,
                            DirectoryTypeDesc = row.Dir_Type_Short_Desc,
                            StreetAddress = row.Address,
                            RegionId = row.Region_Id.ConvertToNoNullable(),
                            CountryId = row.Country_Id.ConvertToNoNullable(),
                            CountryDesc = row.Global_Country_Desc,
                            DomesticregId = row.Domestic_Region_Id.ConvertToNoNullable(),
                            StateProvId = row.State_Prov_Id.ConvertToNoNullable(),
                            StateProvDesc = row.State_Prov_Desc,
                            CityId = row.City_Id.ConvertToNoNullable(),
                            CityDesc = row.City_Desc,
                            ZipCode = row.Zip_Code,
                            IsPrimary = row.isPrimary
                        });
                        break;
                    case "Phone":
                        rPhones.Add(new Contact.Phone
                        {
                            CorpId = row.Corp_Id,
                            DirectoryId = row.Directory_Id,
                            DirectoryDetailId = row.Dir_Detail_Id,
                            CommunicationTypeId = row.Comm_Type_Id,
                            DirectoryTypeId = row.Directory_Type_Id,
                            DirectoryTypeDesc = row.Dir_Type_Short_Desc,
                            CountryCode = row.Phone_Prefix,
                            AreaCode = row.Area_Code,
                            PhoneNumber = row.Phone_Number,
                            PhoneExt = row.Phone_Ext,
                            PersonToContact = row.Person_To_Contact,
                            IsPrimary = row.isPrimary
                        });
                        break;
                    case "Email":
                        rEmails.Add(new Contact.Email
                        {
                            CorpId = row.Corp_Id,
                            DirectoryId = row.Directory_Id,
                            DirectoryDetailId = row.Dir_Detail_Id,
                            CommunicationTypeId = row.Comm_Type_Id,
                            DirectoryTypeId = row.Directory_Type_Id,
                            DirectoryTypeDesc = row.Dir_Type_Short_Desc,
                            EmailAdress = row.Address,
                            IsPrimary = row.isPrimary
                        });
                        break;
                    default:
                        break;
                }
            }

            addresses = rAdresses.ToArray();
            phones = rPhones.ToArray();
            emails = rEmails.ToArray();
        }

        public virtual IEnumerable<Contact.Address> GetCommunicationAdress(int corpId, int contactId, int languageId)
        {
            IEnumerable<Contact.Address> result;
            IEnumerable<Contact.Phone> rPhones;
            IEnumerable<Contact.Email> rEmails;

            this.GetCommunicationAll(corpId, contactId, languageId, out result, out  rPhones, out rEmails);

            return
                result;
        }

        public virtual IEnumerable<Contact.Phone> GetCommunicationPhone(int corpId, int contactId, int languageId)
        {
            IEnumerable<Contact.Address> rAddress;
            IEnumerable<Contact.Phone> result;
            IEnumerable<Contact.Email> rEmails;

            this.GetCommunicationAll(corpId, contactId, languageId, out rAddress, out  result, out rEmails);

            return
                result;
        }

        public virtual IEnumerable<Contact.Email> GetCommunicationEmail(int corpId, int contactId, int languageId)
        {
            IEnumerable<Contact.Address> rAddress;
            IEnumerable<Contact.Phone> rPhones;
            IEnumerable<Contact.Email> result;

            this.GetCommunicationAll(corpId, contactId, languageId, out rAddress, out  rPhones, out result);

            return
                result;
        }

        public virtual IEnumerable<Contact.Email> GetCommunicationEmailJuridico(int corpId, int Agent_Legal, int languageId)
        {
            IEnumerable<Contact.Address> rAddress;
            IEnumerable<Contact.Phone> rPhones;
            IEnumerable<Contact.Email> result;

            this.GetCommunicationAll(corpId, Agent_Legal, languageId, out rAddress, out rPhones, out result);

            return
                result;
        }

        public virtual void GetAgentCommunicationAll(int corpId, int agentId, int languageId, out IEnumerable<Contact.Address> addresses, out IEnumerable<Contact.Phone> phones, out IEnumerable<Contact.Email> emails)
        {
            List<Contact.Address> rAdresses;
            List<Contact.Phone> rPhones;
            List<Contact.Email> rEmails;
            IEnumerable<SP_GET_AGENT_COMMUNICATION_INFORMATION_Result> temp;

            temp = globalModel.SP_GET_AGENT_COMMUNICATION_INFORMATION(corpId, agentId, languageId);

            rAdresses = new List<Contact.Address>(1);
            rPhones = new List<Contact.Phone>(1);
            rEmails = new List<Contact.Email>(1);

            foreach (SP_GET_AGENT_COMMUNICATION_INFORMATION_Result row in temp)
            {
                switch (row.CommunicationType)
                {
                    case "Address":
                        rAdresses.Add(new Contact.Address
                        {
                            CorpId = row.Corp_Id,
                            DirectoryId = row.Directory_Id,
                            DirectoryDetailId = row.Dir_Detail_Id,
                            CommunicationTypeId = row.Comm_Type_Id,
                            DirectoryTypeId = row.Directory_Type_Id,
                            DirectoryTypeDesc = row.Dir_Type_Short_Desc,
                            StreetAddress = row.Address,
                            RegionId = row.Region_Id.ConvertToNoNullable(),
                            CountryId = row.Country_Id.ConvertToNoNullable(),
                            CountryDesc = row.Global_Country_Desc,
                            DomesticregId = row.Domestic_Region_Id.ConvertToNoNullable(),
                            StateProvId = row.State_Prov_Id.ConvertToNoNullable(),
                            StateProvDesc = row.State_Prov_Desc,
                            CityId = row.City_Id.ConvertToNoNullable(),
                            CityDesc = row.City_Desc,
                            ZipCode = row.Zip_Code,
                            IsPrimary = row.isPrimary
                        });
                        break;
                    case "Phone":
                        rPhones.Add(new Contact.Phone
                        {
                            CorpId = row.Corp_Id,
                            DirectoryId = row.Directory_Id,
                            DirectoryDetailId = row.Dir_Detail_Id,
                            CommunicationTypeId = row.Comm_Type_Id,
                            DirectoryTypeId = row.Directory_Type_Id,
                            DirectoryTypeDesc = row.Dir_Type_Short_Desc,
                            CountryCode = row.Phone_Prefix,
                            AreaCode = row.Area_Code,
                            PhoneNumber = row.Phone_Number,
                            PhoneExt = row.Phone_Ext,
                            PersonToContact = row.Person_To_Contact,
                            IsPrimary = row.isPrimary
                        });
                        break;
                    case "Email":
                        rEmails.Add(new Contact.Email
                        {
                            CorpId = row.Corp_Id,
                            DirectoryId = row.Directory_Id,
                            DirectoryDetailId = row.Dir_Detail_Id,
                            CommunicationTypeId = row.Comm_Type_Id,
                            DirectoryTypeId = row.Directory_Type_Id,
                            DirectoryTypeDesc = row.Dir_Type_Short_Desc,
                            EmailAdress = row.Address,
                            IsPrimary = row.isPrimary
                        });
                        break;
                    default:
                        break;
                }
            }

            addresses = rAdresses.ToArray();
            phones = rPhones.ToArray();
            emails = rEmails.ToArray();
        }

        public virtual IEnumerable<Contact.Address> GetAgentCommunicationAdress(int corpId, int agentId, int languageId)
        {
            IEnumerable<Contact.Address> result;
            IEnumerable<Contact.Phone> rPhones;
            IEnumerable<Contact.Email> rEmails;

            this.GetAgentCommunicationAll(corpId, agentId, languageId, out result, out  rPhones, out rEmails);

            return
                result;
        }

        public virtual IEnumerable<Contact.Phone> GetAgentCommunicationPhone(int corpId, int agentId, int languageId)
        {
            IEnumerable<Contact.Address> rAddress;
            IEnumerable<Contact.Phone> result;
            IEnumerable<Contact.Email> rEmails;

            this.GetAgentCommunicationAll(corpId, agentId, languageId, out rAddress, out  result, out rEmails);

            return
                result;
        }

        public virtual IEnumerable<Contact.Email> GetAgentCommunicationEmail(int corpId, int agentId, int languageId)
        {
            IEnumerable<Contact.Address> rAddress;
            IEnumerable<Contact.Phone> rPhones;
            IEnumerable<Contact.Email> result;

            this.GetAgentCommunicationAll(corpId, agentId, languageId, out rAddress, out  rPhones, out result);

            return
                result;
        }

        public virtual IEnumerable<Contact.AgentAppointment> GetAgentAppointment(int corpId, int agentId)
        {
            IEnumerable<Contact.AgentAppointment> result;
            IEnumerable<SP_GET_APPOINTMENT_CALENDAR_AGENT_Result> temp;

            temp = globalModel.SP_GET_APPOINTMENT_CALENDAR_AGENT(corpId, agentId);

            result = temp
                .Select(aa => new Contact.AgentAppointment
                {
                    CorpId = aa.Corp_Id,
                    AgentId = aa.Agent_Id,
                    AppointmentId = aa.Appointment_Id,
                    AppointmentSeqNo = aa.Appointment_Seq_No,
                    DateStart = aa.Date_Start,
                    DateEnd = aa.Date_End,
                    AppointmentTitle = aa.Appointment_Title,
                    AppointmentDesc = aa.Appointment_Desc,
                    Label = aa.Label,
                    Location = aa.Location,
                    AllDay = aa.All_Day,
                    EventType = aa.Event_Type,
                    RecurrenceInfo = aa.Recurrence_Info,
                    ReminderInfo = aa.Reminder_Info,
                    OwnerId = aa.Owner_Id,
                    Price = aa.Price,
                    AppointmentStatus = aa.Appointment_Status
                })
                .ToArray();

            return
                result;
        }

        public virtual int SetAgentAppointment(Contact.AgentAppointment agentAppitment)
        {
            int result;

            result = globalModel.SP_INSERT_APPOINTMENT_CALENDAR_AGENT(
                agentAppitment.CorpId,
                agentAppitment.AgentId,
                agentAppitment.AppointmentId,
                agentAppitment.AppointmentSeqNo,
                agentAppitment.DateStart,
                agentAppitment.DateEnd,
                agentAppitment.AppointmentTitle,
                agentAppitment.AppointmentDesc,
                agentAppitment.Label,
                agentAppitment.Location,
                agentAppitment.AllDay,
                agentAppitment.EventType,
                agentAppitment.RecurrenceInfo,
                agentAppitment.ReminderInfo,
                agentAppitment.OwnerId,
                agentAppitment.Price,
                agentAppitment.AppointmentStatus,
                agentAppitment.UserId
                );

            return
                result;
        }

        public virtual int SetAgentContact(Contact.AgentContact parameter)
        {
            int result;
            IEnumerable<SP_INSERT_EN_AGENT_CONTACTS_Result> temp;

            result = -1;

            temp = globalModel.SP_INSERT_EN_AGENT_CONTACTS(
                parameter.CorpId,
                parameter.AgentId,
                parameter.ContactId,
                parameter.UserId
                );

            return
                result;
        }

        public virtual IEnumerable<Contact.AgentTree> GetAgentTree(Contact.AgentTreeParameter parameter)
        {
            IEnumerable<Contact.AgentTree> result;
            IEnumerable<SP_GET_AGENT_TREE_Result> temp;

            temp = globalModel.SP_GET_AGENT_TREE(parameter.CorpId, parameter.AgentId, parameter.AgentType, parameter.AgentStatusId);

            result = temp
                .Select(aa => new Contact.AgentTree
                {
                    AgentId = aa.Agentid,
                    FirstName = aa.FirstName,
                    LastName = aa.LastName,
                    NameId = aa.nameid
                })
                .ToArray();

            return
                result;
        }

        public virtual int SetAddress(Contact.Address address)
        {
            int result;
            IEnumerable<SP_SET_CONTACT_COMMUNICATION_INFORMATION_Result> temp;

            temp = globalModel.SP_SET_CONTACT_COMMUNICATION_INFORMATION(
                address.CorpId,
                address.DirectoryId,
                address.DirectoryDetailId,
                address.CommunicationTypeId,
                address.DirectoryTypeId,
                null, //CountryCode
                null, //AreaCode
                null, //PhoneNumber
                null, //PhoneExt
                address.StreetAddress.Trim(),
                null, //PersonToContact                
                address.RegionId,
                address.CountryId,
                address.DomesticregId,
                address.StateProvId,
                address.CityId,
                address.ZipCode,
                address.IsPrimary,
                address.CommunicationType,
                address.ContactId,
                address.CreateUser,
                address.ModifyUser,
                null
                )
                .ToArray();

            if (temp != null && temp.Any())
                result = temp.FirstOrDefault().Dir_Detail_Id.Value;
            else
                result = -1;

            return
                result;
        }

        public virtual int SetPhone(Contact.Phone phone)
        {
            int result;
            IEnumerable<SP_SET_CONTACT_COMMUNICATION_INFORMATION_Result> temp;

            temp = globalModel.SP_SET_CONTACT_COMMUNICATION_INFORMATION(
                    phone.CorpId,
                    phone.DirectoryId,
                    phone.DirectoryDetailId,
                    phone.CommunicationTypeId,
                    phone.DirectoryTypeId,
                    phone.CountryCode,
                    phone.AreaCode,
                    phone.PhoneNumber,
                    phone.PhoneExt,
                    null, //StreetAddress
                    phone.PersonToContact,
                    null, //RegionId
                    null, //CountryId                    
                    null, //DomesticregId
                    null, //StateProvId
                    null, //CityId
                    null, //ZipCode
                    phone.IsPrimary,
                    phone.CommunicationType,
                    phone.ContactId,
                    phone.CreateUser,
                    phone.ModifyUser,null                    
                    )
                .ToArray();

            if (temp != null && temp.Any())
                result = temp.FirstOrDefault().Dir_Detail_Id.Value;
            else
                result = -1;

            return
                result;
        }

        public virtual int SetEmail(Contact.Email email)
        {
            int result;
            IEnumerable<SP_SET_CONTACT_COMMUNICATION_INFORMATION_Result> temp;

            temp = globalModel.SP_SET_CONTACT_COMMUNICATION_INFORMATION(
                    email.CorpId,
                    email.DirectoryId,
                    email.DirectoryDetailId,
                    email.CommunicationTypeId,
                    email.DirectoryTypeId,
                    null, //CountryCode
                    null, //AreaCode
                    null, //PhoneNumber
                    null, //PhoneExt
                    email.EmailAdress,
                    null, //PersonToContact  
                    null, //RegionId
                    null, //CountryId
                    null, //DomesticregId
                    null, //StateProvId
                    null, //CityId
                    null, //ZipCode
                    email.IsPrimary,
                    email.CommunicationType,
                    email.ContactId,
                    email.CreateUser,
                    email.ModifyUser,null
                )
                .ToArray();

            if (temp != null && temp.Any())
                result = temp.FirstOrDefault().Dir_Detail_Id.Value;
            else
                result = -1;

            return
                result;
        }

        public virtual int DeleteCommunicaton(int corpId, int directoryId, int directoryDetailId, int modifyUser)
        {
            int result;
            IEnumerable<SP_DELETE_CONTACT_COMMUNICATION_INFORMATION_Result> temp;

            result = -1;

            temp = globalModel.SP_DELETE_CONTACT_COMMUNICATION_INFORMATION(corpId, directoryId, directoryDetailId, modifyUser);

            return
                result;
        }

        public virtual int DeleteCommunicatonJuridico(int corpId, int directoryId, int directoryDetailId, int modifyUser)
        {
            int result;
            IEnumerable<SP_DELETE_CONTACT_COMMUNICATION_INFORMATION_Result> temp;

            result = -1;

            temp = globalModel.SP_DELETE_CONTACT_COMMUNICATION_INFORMATION(corpId, directoryId, directoryDetailId, modifyUser);

            return
                result;
        }

        public virtual IEnumerable<Contact.CitizenContact> GetAllCitizenContact(Contact.CitizenContact Contact)
        {
            IEnumerable<Contact.CitizenContact> result;
            IEnumerable<SP_GET_SOCIAL_EXPOSURE_CONTACT_RELATIVE_DETAILS_Result> temp;

            temp = globalModelExtended.SP_GET_SOCIAL_EXPOSURE_CONTACT_RELATIVE_DETAILS(
                        Contact.Corp_Id
                       , Contact.Region_Id
                       , Contact.Country_Id
                       , Contact.Domesticreg_Id
                       , Contact.State_Prov_Id
                       , Contact.City_Id
                       , Contact.Office_Id
                       , Contact.Case_Seq_No
                       , Contact.Hist_Seq_No
                       , Contact.Contact_Id
                       , Contact.Language_id).ToArray();

            result = temp
                .Select(id => new Contact.CitizenContact
                {
                    Corp_Id = id.Corp_Id,
                    Region_Id = id.Region_Id,
                    Country_Id = id.Country_Id,
                    Domesticreg_Id = id.Domesticreg_Id,
                    State_Prov_Id = id.State_Prov_Id,
                    City_Id = id.City_Id,
                    Office_Id = id.Office_Id,
                    Case_Seq_No = id.Case_Seq_No,
                    Hist_Seq_No = id.Hist_Seq_No,
                    Contact_Id = id.Contact_Id,
                    Exposure_Related_Id = id.Exposure_Related_Id,
                    FullName = id.Exposure_Related_Name,
                    Relationship = id.Relationship_Id,
                    RelationshipDesc = id.Relationship_Id_desc,
                    Position = id.Exposure_Related_Position,
                    JobFromDate = id.Exposure_Related_Date_From_Position,
                    JobToDate = id.Exposure_Related_Date_To_Position
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Contact.IdDocument> GetAllIdDocumentInformation(int contactId, int languageId)
        {
            IEnumerable<Contact.IdDocument> result;
            IEnumerable<SP_GET_CONTACT_ID_DOCUMENT_INFORMATION_Result> temp;

            temp = globalModel.SP_GET_CONTACT_ID_DOCUMENT_INFORMATION(contactId, languageId);

            result = temp
                .Select(id => new Contact.IdDocument
                {
                    ContactId = id.Contact_Id,
                    SeqNo = id.Seq_No,
                    DocumentTypeId = id.Doc_Type_Id,
                    DocumentCategoryId = id.Doc_Category_Id,
                    DocumentId = id.Document_Id,
                    DocumentName = id.Document_Name,
                    DocumentTypeDescription = id.Doc_Type_Desc,
                    ContentType = id.Content_Type,
                    Extension = id.Extension,
                    DocumentBinary = null,
                    ContactIdType = id.Contact_Id_Type,
                    ContactIdTypeDescription = id.Contact_Id_Type_Desc,
                    ExpireDate = id.Expire_date,
                    Id = id.Id,
                    CountryIssuedBy = id.Country_Issued_By,
                    CountryIssuedByDesc = id.Global_Country_Desc,
                    IssuedBy = id.Issued_By,
                    MainIdentity = id.Main_Identity,
                    ValidDate = id.Valid_Date
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Contact.IdDocument> GetAllIdDocumentInformationJuridico(int Agent_Legal, int languageId)
        {
            IEnumerable<Contact.IdDocument> result;
            IEnumerable<SP_GET_CONTACT_ID_DOCUMENT_INFORMATION_Result> temp;

            temp = globalModel.SP_GET_CONTACT_ID_DOCUMENT_INFORMATION(Agent_Legal, languageId);

            result = temp
                .Select(id => new Contact.IdDocument
                {
                    ContactId = id.Contact_Id,
                    SeqNo = id.Seq_No,
                    DocumentTypeId = id.Doc_Type_Id,
                    DocumentCategoryId = id.Doc_Category_Id,
                    DocumentId = id.Document_Id,
                    DocumentName = id.Document_Name,
                    DocumentTypeDescription = id.Doc_Type_Desc,
                    ContentType = id.Content_Type,
                    Extension = id.Extension,
                    DocumentBinary = null,
                    ContactIdType = id.Contact_Id_Type,
                    ContactIdTypeDescription = id.Contact_Id_Type_Desc,
                    ExpireDate = id.Expire_date,
                    Id = id.Id,
                    CountryIssuedBy = id.Country_Issued_By,
                    CountryIssuedByDesc = id.Global_Country_Desc,
                    IssuedBy = id.Issued_By,
                    MainIdentity = id.Main_Identity,
                    ValidDate = id.Valid_Date
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Contact.IdDocument> GetAllIdDocumentBenefitary(int contactId, int languageId)
        {
            IEnumerable<Contact.IdDocument> result;
            IEnumerable<SP_GET_CONTACT_ID_DOCUMENT_BENEFICIARY_Result> temp;

            temp = globalModel.SP_GET_CONTACT_ID_DOCUMENT_BENEFICIARY(contactId, languageId);

            result = temp
                .Select(id => new Contact.IdDocument
                {
                    ContactId = id.Contact_Id,
                    SeqNo = id.Seq_No,
                    DocumentTypeId = id.Doc_Type_Id,
                    DocumentCategoryId = id.Doc_Category_Id,
                    DocumentId = id.Document_Id,
                    DocumentName = id.Document_Name,
                    DocumentTypeDescription = id.Doc_Type_Desc,
                    ContentType = id.Content_Type,
                    Extension = id.Extension,
                    DocumentBinary = null,
                    ContactIdType = id.Contact_Id_Type,
                    ContactIdTypeDescription = id.Contact_Id_Type_Desc,
                    ExpireDate = id.Expire_date,
                    Id = id.Id,
                    CountryIssuedBy = id.Country_Issued_By,
                    CountryIssuedByDesc = id.Global_Country_Desc,
                    IssuedBy = id.Issued_By,
                    MainIdentity = id.Main_Identity,
                    ValidDate = id.Valid_Date
                })
                .ToArray();

            return
                result;
        }

        public virtual int SetIdDocument(Contact.IdDocument document)
        {
            int result;
            IEnumerable<SP_SET_CONTACT_ID_Result> temp;

            temp = globalModel.SP_SET_CONTACT_ID(
                        document.ContactId,
                        document.SeqNo,
                        document.ContactIdType,
                        document.Id,
                        document.ValidDate,
                        document.ExpireDate,
                        document.CountryIssuedBy,
                        document.IssuedBy,
                        document.DocumentTypeId,
                        document.DocumentCategoryId,
                        document.DocumentId,
                        document.MainIdentity,
                        document.DocumentBinary,
                        document.DocumentName,
                        document.FileCreationDate,
                        document.FileExpireDate,
                        document.UserId
                    )
                    .ToArray();

            if (temp != null && temp.Any())
                result = 0;
            else
                result = -1;

            return
                result;
        }

        public virtual int SetCitizenContact(Contact.CitizenContact Contact)
        {
            int result;
            IEnumerable<SP_SET_SOCIAL_EXPOSURE_CONTACT_RELATIVE_DETAILS_Result> temp;

            temp = globalModelExtended.SP_SET_SOCIAL_EXPOSURE_CONTACT_RELATIVE_DETAILS(
                        Contact.Corp_Id
                       , Contact.Region_Id
                       , Contact.Country_Id
                       , Contact.Domesticreg_Id
                       , Contact.State_Prov_Id
                       , Contact.City_Id
                       , Contact.Office_Id
                       , Contact.Case_Seq_No
                       , Contact.Hist_Seq_No
                       , Contact.Contact_Id
                       , Contact.Exposure_Related_Id
                       , Contact.FullName
                       , Contact.Relationship
                       , Contact.Position
                       , Contact.JobFromDate
                       , Contact.JobToDate
                       , Contact.CreateUser
                       , Contact.ModifyUser
                    )
                    .ToArray();

            if (temp != null && temp.Any())
                result = 0;
            else
                result = -1;

            return
                result;
        }

        public virtual int SetIdDocumentJuridico(Contact.IdDocument documentJuridico)
        {
            int result;
            IEnumerable<SP_SET_CONTACT_ID_Result> temp;

            temp = globalModel.SP_SET_CONTACT_ID(
                        documentJuridico.ContactId,
                // documentJuridico.Agent_Legal,
                        documentJuridico.SeqNo,
                        documentJuridico.ContactIdType,
                        documentJuridico.Id,
                        documentJuridico.ValidDate,
                        documentJuridico.ExpireDate,
                        documentJuridico.CountryIssuedBy,
                        documentJuridico.IssuedBy,
                        documentJuridico.DocumentTypeId,
                        documentJuridico.DocumentCategoryId,
                        documentJuridico.DocumentId,
                        documentJuridico.MainIdentity,
                        documentJuridico.DocumentBinary,
                        documentJuridico.DocumentName,
                        documentJuridico.FileCreationDate,
                        documentJuridico.FileExpireDate,
                        documentJuridico.UserId
                    )
                    .ToArray();

            if (temp != null && temp.Any())
                result = 0;
            else
                result = -1;

            return
                result;
        }

        public virtual bool CheckIdDocument(int contactId, int contactIdType, int countryIssuedBy, string id)
        {
            bool result;
            IEnumerable<bool?> temp;

            temp = globalModel.SP_CK_CONTACT_ID(
                        contactId,
                        contactIdType,
                        countryIssuedBy,
                        id
                    )
                    .ToArray();

            result = temp != null && temp.Any()
                        ? temp.FirstOrDefault().ConvertToNoNullable()
                        : false;

            return
                result;
        }

        public virtual Contact.IdDocument GetIdDocument(int contactId, int documentCategoryId, int documentTypeId, int documentId)
        {
            Contact.IdDocument result;
            IEnumerable<SP_GET_CONTACT_ID_DOCUMENT_Result> temp;

            temp = globalModel.SP_GET_CONTACT_ID_DOCUMENT(contactId, documentCategoryId, documentTypeId, documentId);

            result = temp
                .Select(id => new Contact.IdDocument
                {
                    ContactId = id.Contact_Id,
                    DocumentCategoryId = id.Doc_Category_Id,
                    DocumentTypeId = id.Doc_Type_Id,
                    DocumentId = id.Document_Id,
                    DocumentName = id.Document_Name,
                    DocumentTypeDescription = id.Doc_Type_Desc,
                    ContentType = id.Content_Type,
                    Extension = id.Extension,
                    DocumentBinary = id.Document_Binary,
                    SeqNo = id.Seq_No,
                    ContactIdType = id.Contact_Id_Type,
                    ContactIdTypeDescription = id.Contact_Id_Type_Desc,
                    ExpireDate = id.Expire_date,
                    Id = id.Id,
                    CountryIssuedBy = id.Country_Issued_By,
                    IssuedBy = id.Issued_By,
                    CountryIssuedByDesc = id.Global_Country_Desc,
                    MainIdentity = id.Main_Identity,
                    ValidDate = id.Valid_Date
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual int DeleteIdDocument(int contactId, int seqNo, int userId)
        {
            int result;
            IEnumerable<SP_DELETE_CONTACT_ID_Result> temp;

            result = -1;

            temp = globalModel.SP_DELETE_CONTACT_ID(contactId, seqNo, userId);

            return
                result;
        }

        public virtual int DeleteIdDocumentJuridico(int Agent_Legal, int seqNo, int userId)
        {
            int result;
            IEnumerable<SP_DELETE_CONTACT_ID_Result> temp;

            result = -1;

            temp = globalModel.SP_DELETE_CONTACT_ID(Agent_Legal, seqNo, userId);

            return
                result;
        }

        public virtual int DeleteCitizenContact(Contact.CitizenContact Contact)
        {
            int result = -1;

            var temp = globalModelExtended.SP_DEL_SOCIAL_EXPOSURE_CONTACT_RELATIVE_DETAILS(
                          Contact.Corp_Id
                        , Contact.Region_Id
                        , Contact.Country_Id
                        , Contact.Domesticreg_Id
                        , Contact.State_Prov_Id
                        , Contact.City_Id
                        , Contact.Office_Id
                        , Contact.Case_Seq_No
                        , Contact.Hist_Seq_No
                        , Contact.Contact_Id
                        , Contact.Exposure_Related_Id);

            return
                result;
        }

        public virtual Contact.BackGroundCheck GetBackGroundCheck(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int contactId)
        {
            Contact.BackGroundCheck result;
            IEnumerable<SP_GET_CONTACT_BACKGROUNDCHECK_INFORMATION_Result> temp;

            temp = globalModel.SP_GET_CONTACT_BACKGROUNDCHECK_INFORMATION(coprId, regionId, countryId, domesticRegId, stateProvId
                , cityId, officeId, caseSeqNo, histSeqNo, contactId);

            result = temp
                .Select(bgc => new Contact.BackGroundCheck
                {
                    CorpId = bgc.Corp_Id,
                    RegionId = bgc.Region_Id,
                    CountryId = bgc.Country_Id,
                    DomesticregId = bgc.Domesticreg_Id,
                    StateProvId = bgc.State_Prov_Id,
                    CityId = bgc.City_Id,
                    OfficeId = bgc.Office_Id,
                    CaseSeqNo = bgc.Case_Seq_No,
                    HistSeqNo = bgc.Hist_Seq_No,
                    ContactId = bgc.Contact_Id,
                    BackgroundCheckId = bgc.Background_Check_Id,
                    BackgroundCheckerId = bgc.Background_Checker_Id,
                    Reason = bgc.Reason,
                    Results = bgc.Results,
                    Date = bgc.Date,
                    Comments = bgc.Comments,
                    BackgroundCheckUserName = bgc.Background_UserName
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Contact.BackGroundCheckDocumentInfomation> GetAllBackGroundCheckDocumentInformation(int coprId, int regionId, int countryId
            , int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            IEnumerable<Contact.BackGroundCheckDocumentInfomation> result;
            IEnumerable<SP_GET_CONTACT_BACKGROUNDCHECK_DOCUMENTS_INFORMATION_Result> temp;

            temp = globalModel.SP_GET_CONTACT_BACKGROUNDCHECK_DOCUMENTS_INFORMATION(coprId, regionId, countryId, domesticRegId, stateProvId
                , cityId, officeId, caseSeqNo, histSeqNo);

            result = temp
                .Select(gdi => new Contact.BackGroundCheckDocumentInfomation
                {
                    CorpId = gdi.Corp_Id,
                    RegionId = gdi.Region_Id,
                    CountryId = gdi.Country_Id,
                    DomesticregId = gdi.Domesticreg_Id,
                    StateProvId = gdi.State_Prov_Id,
                    CityId = gdi.City_Id,
                    OfficeId = gdi.Office_Id,
                    CaseSeqNo = gdi.Case_Seq_No,
                    HistSeqNo = gdi.Hist_Seq_No,
                    DocumentCategoryId = gdi.Doc_Category_Id,
                    DocumentTypeId = gdi.Doc_Type_Id,
                    DocumentId = gdi.Document_Id,
                    DocumentName = gdi.Document_Name,
                    FileCreationDate = gdi.File_Creation_Date,
                    DocumentTypeDescription = gdi.Doc_Type_Desc,
                    ContentType = gdi.Content_Type,
                    Extension = gdi.Extension,
                    DocumentBinary = null
                })
                .ToArray();

            return
                result;
        }

        public virtual Contact.BackGroundCheckDocumentInfomation GetBackGroundCheckDocument(int coprId, int regionId, int countryId
           , int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int documentCategoryId, int documentTypeId, int documentId)
        {
            Contact.BackGroundCheckDocumentInfomation result;
            IEnumerable<SP_GET_CONTACT_BACKGROUNDCHECK_DOCUMENT_Result> temp;

            temp = globalModel.SP_GET_CONTACT_BACKGROUNDCHECK_DOCUMENT(coprId, regionId, countryId, domesticRegId, stateProvId
                , cityId, officeId, caseSeqNo, histSeqNo, documentCategoryId, documentTypeId, documentId);

            result = temp
                .Select(gdi => new Contact.BackGroundCheckDocumentInfomation
                {
                    CorpId = gdi.Corp_Id,
                    RegionId = gdi.Region_Id,
                    CountryId = gdi.Country_Id,
                    DomesticregId = gdi.Domesticreg_Id,
                    StateProvId = gdi.State_Prov_Id,
                    CityId = gdi.City_Id,
                    OfficeId = gdi.Office_Id,
                    CaseSeqNo = gdi.Case_Seq_No,
                    HistSeqNo = gdi.Hist_Seq_No,
                    DocumentCategoryId = gdi.Doc_Category_Id,
                    DocumentTypeId = gdi.Doc_Type_Id,
                    DocumentId = gdi.Document_Id,
                    DocumentName = gdi.Document_Name,
                    FileCreationDate = gdi.File_Creation_Date,
                    DocumentTypeDescription = gdi.Doc_Type_Desc,
                    ContentType = gdi.Content_Type,
                    Extension = gdi.Extension,
                    DocumentBinary = gdi.Document_Binary
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Contact.Search> ContactSearchByAgent(int coprId, int agentId, int? contactTypeId)
        {
            IEnumerable<Contact.Search> result;
            IEnumerable<SP_GET_CONTACT_SEARCH_BY_AGENT_Result> temp;

            temp = globalModel.SP_GET_CONTACT_SEARCH_BY_AGENT(coprId, agentId, contactTypeId);

            result = temp
                .Select(s => new Contact.Search
                {
                    ContactId = s.ContactId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    IdNumber = s.IdNumber,
                    DateOfBirth = s.DateOfBirth,
                    Country = s.Country,
                    LastUpdate = s.LastUpdate,
                    ContactAgentLegalId = s.ContactAgentLegalId
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Contact.SecurityQuestion> GetAllSecurityQuestion(int corpId, int contactId, int languageId)
        {
            IEnumerable<Contact.SecurityQuestion> result;
            IEnumerable<SP_GET_CONTACT_QUESTIONS_Result> temp;

            temp = globalModel.SP_GET_CONTACT_QUESTIONS(corpId, contactId, languageId);

            result = temp
                .Select(cq => new Contact.SecurityQuestion
                {
                    CorpId = cq.Corp_Id,
                    QuestionId = cq.Question_Id,
                    ContactId = cq.Contact_Id.ConvertToNoNullable(),
                    NameId = cq.Name_Id,
                    QuestionDesc = cq.Question_Desc,
                    Answer = cq.Answer
                })
                .ToArray();

            return
                result;
        }

        public virtual int SetSecurityQuestion(Contact.SecurityQuestion question)
        {
            int result;
            IEnumerable<SP_SET_CONTACT_QUESTIONS_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_CONTACT_QUESTIONS(
                    question.CorpId,
                    question.QuestionId,
                    question.ContactId,
                    question.Answer,
                    question.UserId
                );

            return
                result;
        }

        public virtual IEnumerable<Contact.GenerateNameId> SetNameIdToContactId(int contactId, string nameId, int userId)
        {
            IEnumerable<Contact.GenerateNameId> result;
            IEnumerable<SP_SET_CONTACT_GENERATE_NAMEID_Result> temp;

            temp = globalModel.SP_SET_CONTACT_GENERATE_NAMEID(contactId, nameId, userId);

            result = temp
                .Select(gn => new Contact.GenerateNameId
                {
                    Code = gn.Code,
                    Message = gn.Message,
                    NameId = gn.NameId
                })
                .ToArray();
            return
                result;
        }

        public virtual int GetContactIdByNameId(string nameId)
        {
            int result;
            IEnumerable<SP_GET_CONTACT_CONTACTID_BY_NAMEID_Result> temp;

            temp = globalModel.SP_GET_CONTACT_CONTACTID_BY_NAMEID(nameId);

            result = temp
                       .Select(ci => ci.Contact_Id)
                       .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Contact.Math> GetContactMath(Contact.Math parameters)
        {
            IEnumerable<Contact.Math> result;
            IEnumerable<SP_GET_CONTACT_MATH_Result> temp;

            temp = globalModel.SP_GET_CONTACT_MATH(
                parameters.ContactTypeId,
                parameters.LanguageId,
                parameters.FirstName,
                parameters.FirstLastName,
                parameters.Dob,
                parameters.Ids
                );

            result = temp
                .Select(cm => new Contact.Math
                {
                    ContactTypeId = cm.Contact_Type_Id.ConvertToNoNullable(),
                    LanguageId = -1,
                    FirstName = cm.First_Name,
                    FirstLastName = cm.Lastname,
                    Dob = cm.Dob.ConvertToNoNullable(),
                    Ids = cm.Ids,
                    ContactId = cm.Contact_Id.ConvertToNoNullable(),
                    MiddleName = cm.Middle_Name,
                    SecondLastName = cm.Second_Lastname,
                    FullName = cm.Full_Name,
                    CountryDesc = cm.Country_Desc,
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Contact.GenerateCustomerNumber> SetCustomerNumberToContactId(int contactId, string customerNumber, int userId)
        {
            IEnumerable<Contact.GenerateCustomerNumber> result;
            IEnumerable<SP_SET_CONTACT_GENERATE_CUSTOMERNUMBER_Result> temp;

            temp = globalModel.SP_SET_CONTACT_GENERATE_CUSTOMERNUMBER(contactId, customerNumber, userId);

            result = temp
                .Select(gn => new Contact.GenerateCustomerNumber
                {
                    Code = gn.Code,
                    Message = gn.Message,
                    CustomerNumber = gn.CustomerNumber
                })
                .ToArray();
            return
                result;
        }

        public virtual int GetContactIdByCustomerNumber(string customerNumber)
        {
            int result;
            IEnumerable<SP_GET_CONTACT_CONTACTID_BY_CUSTOMERNUMBER_Result> temp;

            temp = globalModel.SP_GET_CONTACT_CONTACTID_BY_CUSTOMERNUMBER(customerNumber);

            result = temp
                       .Select(ci => ci.Contact_Id)
                       .FirstOrDefault();

            return
                result;
        }

        public virtual Contact.Directory GetDirectoryId(int corpId, int contactId)
        {
            Contact.Directory result;
            IEnumerable<SP_GET_CONTACT_DIRECTORYID_BY_CONTACTID_Result> temp;

            temp = globalModel.SP_GET_CONTACT_DIRECTORYID_BY_CONTACTID(corpId, contactId);

            result = temp
                .Select(d => new Contact.Directory
                {
                    CorpId = d.Corp_Id,
                    ContactId = d.Contact_Id,
                    DirectoryId = d.Directory_Id
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual Contact.ValidateDocumentCedula GetResultDUI(string DocumentCedula)
        {
            Contact.ValidateDocumentCedula result;
            IEnumerable<int?> temp;

            temp = globalModel.SP_VALIDAR_CEDULA(DocumentCedula);

            result = temp
                .Select(d => new Contact.ValidateDocumentCedula
                {
                    Cedula = d.HasValue ? d.Value : 0
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual Contact.ValidateDocumentRNC GetResultRNC(string DocumentRNC)
        {
            Contact.ValidateDocumentRNC result;
            IEnumerable<int?> temp;

            temp = globalModel.SP_VALIDAR_RNC(DocumentRNC);

            result = temp
                .Select(d => new Contact.ValidateDocumentRNC
                {
                    RNC = d.HasValue ? d.Value : 0
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Contact.ValidateDocumentIDS> GetAllDocumentIDs(string IDs)
        {
            IEnumerable<Contact.ValidateDocumentIDS> result;
            IEnumerable<SP_GET_CONTACT_IDS_IN_BENEFICIARY_Result> temp;

            temp = globalModelExtended.SP_GET_CONTACT_IDS_IN_BENEFICIARY(IDs);

            result = temp
            .Select(d => new Contact.ValidateDocumentIDS
            {
                Contact_id = d.Contact_Id,
                IDs = d.Id

            })
            .ToArray();

            return result;
        }

        public virtual Contact.FinalBeneficiary SetContactFinalBeneficiary(int? contactId, int? finalBeneficiaryId, string name, decimal? percentageParticipation, bool? finalBeneficiaryStatus, int? userId, bool? isPEP, int? pepFormularyOptionsId, int? identificationTypeId, string identificationNumber, int? nationalityCountryId)
        {
            Contact.FinalBeneficiary result;
            IEnumerable<SP_SET_EN_CONTACT_FINAL_BENEFICIARY_Result> temp;

            temp = globalModelExtended.SP_SET_EN_CONTACT_FINAL_BENEFICIARY(contactId,
                                                                          finalBeneficiaryId,
                                                                          name,
                                                                          percentageParticipation,
                                                                          finalBeneficiaryStatus,
                                                                          userId,
                                                                          isPEP,
                                                                          pepFormularyOptionsId,
                                                                          identificationTypeId,
                                                                          identificationNumber,
                                                                          nationalityCountryId
                                                                         )
                                                                         .ToArray();
            result = temp.Select(h => new Contact.FinalBeneficiary
            {
                Action = h.Action,
                ContactId = h.Contact_Id,
                FinalBeneficiaryId = h.Final_Beneficiary_Id
            })
            .FirstOrDefault();

            return
                 result;
        }

        public virtual Contact.PepFormulary SetContactPepFormulary(int? contactId, int? pepFormularyId, string name, int? relationshipId, string position, int? fromYear, int? toYear, bool? pepFormularyStatus, int? userId, int? BeneficiaryId, bool? IsPepManagerCompany)
        {
            Contact.PepFormulary result;
            IEnumerable<SP_SET_EN_CONTACT_PEP_FORMULARY_Result> temp;

            temp = globalModelExtended.SP_SET_EN_CONTACT_PEP_FORMULARY(
                                                                contactId,
                                                                pepFormularyId,
                                                                name,
                                                                relationshipId,
                                                                position,
                                                                fromYear,
                                                                toYear,
                                                                pepFormularyStatus,
                                                                userId,
                                                                BeneficiaryId,
                                                                IsPepManagerCompany
                                                             )
                                                             .ToArray();

            result = temp.Select(h => new Contact.PepFormulary
            {
                Action = h.Action,
                ContactId = h.Contact_Id,
                PepFormularyId = h.Pep_Formulary_Id
            })
            .FirstOrDefault();

            return
                 result;

        }

        public virtual IEnumerable<Contact.PepFormulary.PEPFormResult> GetContactPEPFormulary(int? ContactId, string Source)
        {
            IEnumerable<Contact.PepFormulary.PEPFormResult> result;
            IEnumerable<SP_GET_EN_CONTACT_PEP_FORMULARY_Result> temp;

            temp = globalModel.SP_GET_EN_CONTACT_PEP_FORMULARY(ContactId, Source);
            result = temp.Select(c => new Contact.PepFormulary.PEPFormResult
            {
                ContactId = c.Contact_Id,
                PepFormularyId = c.Pep_Formulary_Id,
                Name = c.Name,
                RelationshipId = c.Relationship_Id,
                Position = c.Position,
                FromYear = c.FromYear,
                ToYear = c.ToYear,
                BeneficiaryId = c.BeneficiaryId,
                IsPepManagerCompany = c.IsPepManagerCompany,
                PepFormularyOptionsId = c.PepFormularyOptionsId
            })
            .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Contact.FinalBeneficiary.FinalBenResult> GetContactFinalBeneficiary(int? ContactId)
        {
            IEnumerable<Contact.FinalBeneficiary.FinalBenResult> result;
            IEnumerable<SP_GET_EN_CONTACT_FINAL_BENEFICIARY_Result> temp;

            temp = globalModelExtended.SP_GET_EN_CONTACT_FINAL_BENEFICIARY(ContactId);
            result = temp.Select(c => new Contact.FinalBeneficiary.FinalBenResult
            {
                ContactId = c.Contact_Id,
                FinalBeneficiaryId = c.Final_Beneficiary_Id,
                Name = c.Name,
                PercentageParticipation = c.PercentageParticipation,
                IsPEP = c.IsPEP,
                PepFormularyOptionsId = c.PepFormularyOptionsId,
                IdentificationTypeId = c.Identification_Type_Id,
                IdentificationNumber = c.Identification_Number,
                NationalityCountryId = c.Nationality_Country_Id,
                ContactIdTypeDesc = c.Contact_Id_Type_Desc,
                GlobalCountryDesc = c.Global_Country_Desc
            })
            .ToArray();

            return
                result;
        }
    }
}