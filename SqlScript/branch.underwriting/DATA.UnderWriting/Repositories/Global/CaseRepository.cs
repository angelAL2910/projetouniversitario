﻿using System.Collections.Generic;
using System.Linq;
using DATA.UnderWriting.Data;
using DATA.UnderWriting.Repositories.Base;
using Entity.UnderWriting.Entities;

namespace DATA.UnderWriting.Repositories.Global
{
    public class CaseRepository : GlobalRepository
    {
        public CaseRepository(GlobalEntityDataModel globalModel, GlobalEntities globalModelExtended) : base(globalModel, globalModelExtended) { }

        public virtual IEnumerable<Case> GetAllOpen(int companyId, int underwriterId)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_ALL_OPEN_Result> temp;

            temp = globalModel.SP_GET_CASE_ALL_OPEN(companyId, underwriterId);

            result = temp
                .Select(oc => new Case
                {
                    CorpId = oc.Corp_Id,
                    RegionId = oc.Region_Id,
                    CountryId = oc.Country_Id,
                    DomesticregId = oc.Domesticreg_Id,
                    StateProvId = oc.State_Prov_Id,
                    CityId = oc.City_Id,
                    OfficeId = oc.Office_Id,
                    CaseSeqNo = oc.Case_Seq_No,
                    HistSeqNo = oc.Hist_Seq_No,
                    PolicyStatusId = oc.Policy_Status_Id,
                    ContactId = oc.Contact_Id,
                    AddInsuredContactId = oc.AddInsuredContactId,
                    ProductId = oc.Product_Id,
                    PolicySerieId = oc.Policy_Serie_Id,
                    CompanyId = oc.Company_Id,
                    PolicyNo = oc.Policy_No,
                    PolicyStatusDesc = oc.Policy_Status_Desc,
                    SerieCode = oc.Serie_Code,
                    SerieDesc = oc.Serie_Desc,
                    ProductDesc = oc.Product_Desc,
                    FirstNameInsured = oc.FirstNameInsured,
                    MiddleNameInsured = oc.MiddleNameInsured,
                    FirstLastNameInsured = oc.FirstLastNameInsured,
                    SecondLastNameInsured = oc.SecondLastNameInsured,
                    FullNameInsured = oc.FullNameInsured,
                    BenefitAmount = oc.Benefit_Amount,
                    GlobalCountryDesc = oc.Global_Country_Desc,
                    OfficeDesc = oc.Office_Desc,
                    CompanyDesc = oc.Company_Desc,
                    AssigedTo = oc.AssigedTo,
                    Priority = oc.Priority,
                    SubmitDate = oc.Submit_Date,
                    ContactRoleTypeId = oc.Contact_Role_Type_Id,
                    OwnerIsInsured = oc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = oc.Insured_Period,
                    ProductTypeDesc = oc.Product_Type_Desc
                })
                .ToArray();

            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllProcessing(int companyId, int underwriterId)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_PROCESSING_Result> temp;

            temp = globalModel.SP_GET_CASE_PROCESSING(companyId, underwriterId);

            result = temp
                .Select(pc => new Case
                {
                    CorpId = pc.Corp_Id,
                    RegionId = pc.Region_Id,
                    CountryId = pc.Country_Id,
                    DomesticregId = pc.Domesticreg_Id,
                    StateProvId = pc.State_Prov_Id,
                    CityId = pc.City_Id,
                    OfficeId = pc.Office_Id,
                    CaseSeqNo = pc.Case_Seq_No,
                    HistSeqNo = pc.Hist_Seq_No,
                    PolicyStatusId = pc.Policy_Status_Id,
                    ContactId = pc.Contact_Id,
                    AddInsuredContactId = pc.AddInsuredContactId,
                    ProductId = pc.Product_Id,
                    PolicySerieId = pc.Policy_Serie_Id,
                    CompanyId = pc.Company_Id,
                    PolicyNo = pc.Policy_No,
                    PolicyStatusDesc = pc.Policy_Status_Desc,
                    SerieCode = pc.Serie_Code,
                    SerieDesc = pc.Serie_Desc,
                    ProductDesc = pc.Product_Desc,
                    FirstNameInsured = pc.FirstNameInsured,
                    MiddleNameInsured = pc.MiddleNameInsured,
                    FirstLastNameInsured = pc.FirstLastNameInsured,
                    SecondLastNameInsured = pc.SecondLastNameInsured,
                    FullNameInsured = pc.FullNameInsured,
                    FirstNameAgent = pc.FirstNameAgent,
                    MiddleNameAgent = pc.MiddleNameAgent,
                    FirstLastNameAgent = pc.FirstLastNameAgent,
                    SecondLastNameAgent = pc.SecondLastNameAgent,
                    FullNameAgent = pc.FullNameAgent,
                    BenefitAmount = pc.Benefit_Amount,
                    OfficeDesc = pc.Office_Desc,
                    Days = pc.Days,
                    CompanyDesc = pc.Company_Desc,
                    AssigedTo = pc.AssigedTo,
                    Priority = pc.Priority,
                    Steps = pc.Steps,
                    ContactRoleTypeId = pc.Contact_Role_Type_Id,
                    OwnerIsInsured = pc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = pc.Insured_Period,
                    ProductTypeDesc = pc.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllAwaitingInformation(int companyId, int underwriterId)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_AWAITING_INFO_Result> temp;

            temp = globalModel.SP_GET_CASE_AWAITING_INFO(companyId, underwriterId);

            result = temp
                .Select(aic => new Case
                {
                    CorpId = aic.Corp_Id,
                    RegionId = aic.Region_Id,
                    CountryId = aic.Country_Id,
                    DomesticregId = aic.Domesticreg_Id,
                    StateProvId = aic.State_Prov_Id,
                    CityId = aic.City_Id,
                    OfficeId = aic.Office_Id,
                    CaseSeqNo = aic.Case_Seq_No,
                    HistSeqNo = aic.Hist_Seq_No,
                    PolicyStatusId = aic.Policy_Status_Id,
                    ContactId = aic.Contact_Id,
                    AddInsuredContactId = aic.AddInsuredContactId,
                    ProductId = aic.Product_Id,
                    PolicySerieId = aic.Policy_Serie_Id,
                    CompanyId = aic.Company_Id,
                    PolicyNo = aic.Policy_No,
                    PolicyStatusDesc = aic.Policy_Status_Desc,
                    SerieCode = aic.Serie_Code,
                    SerieDesc = aic.Serie_Desc,
                    ProductDesc = aic.Product_Desc,
                    FirstNameInsured = aic.FirstNameInsured,
                    MiddleNameInsured = aic.MiddleNameInsured,
                    FirstLastNameInsured = aic.FirstLastNameInsured,
                    SecondLastNameInsured = aic.SecondLastNameInsured,
                    FullNameInsured = aic.FullNameInsured,
                    FirstNameAgent = aic.FirstNameAgent,
                    MiddleNameAgent = aic.MiddleNameAgent,
                    FirstLastNameAgent = aic.FirstLastNameAgent,
                    SecondLastNameAgent = aic.SecondLastNameAgent,
                    FullNameAgent = aic.FullNameAgent,
                    BenefitAmount = aic.Benefit_Amount,
                    OfficeDesc = aic.Office_Desc,
                    CompanyDesc = aic.Company_Desc,
                    AssigedTo = aic.AssigedTo,
                    SubmitDate = aic.Submit_Date,
                    StepInAwaiting = aic.StepInAwaiting,
                    TimeInAwaiting = aic.TimeInAwaiting,
                    ContactRoleTypeId = aic.Contact_Role_Type_Id,
                    OwnerIsInsured = aic.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = aic.Insured_Period,
                    ProductTypeDesc = aic.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllReinsurance(int companyId, int underwriterId)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_REINSURANCE_Result> temp;

            temp = globalModel.SP_GET_CASE_REINSURANCE(companyId, underwriterId);

            result = temp
                .Select(rc => new Case
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
                    PolicyStatusId = rc.Policy_Status_Id,
                    ContactId = rc.Contact_Id,
                    AddInsuredContactId = rc.AddInsuredContactId,
                    ProductId = rc.Product_Id,
                    PolicySerieId = rc.Policy_Serie_Id,
                    CompanyId = rc.Company_Id,
                    PolicyNo = rc.Policy_No,
                    PolicyStatusDesc = rc.Policy_Status_Desc,
                    SerieCode = rc.Serie_Code,
                    SerieDesc = rc.Serie_Desc,
                    ProductDesc = rc.Product_Desc,
                    FirstNameInsured = rc.FirstNameInsured,
                    MiddleNameInsured = rc.MiddleNameInsured,
                    FirstLastNameInsured = rc.FirstLastNameInsured,
                    SecondLastNameInsured = rc.SecondLastNameInsured,
                    FullNameInsured = rc.FullNameInsured,
                    FirstNameAgent = rc.FirstNameAgent,
                    MiddleNameAgent = rc.MiddleNameAgent,
                    FirstLastNameAgent = rc.FirstLastNameAgent,
                    SecondLastNameAgent = rc.SecondLastNameAgent,
                    FullNameAgent = rc.FullNameAgent,
                    BenefitAmount = rc.Benefit_Amount,
                    OfficeDesc = rc.Office_Desc,
                    CompanyDesc = rc.Company_Desc,
                    AssigedTo = rc.AssigedTo,
                    RyderTypeDesc = rc.Ryder_Type_Desc,
                    ReinsuredAmount = rc.Reinsured_Amount,
                    Reinsurer = rc.Reinsurer,
                    DateSentToReinsurance = rc.Date_Sent_To_Reinsurance,
                    TimeInReinsurance = rc.Time_In_Reinsurance,
                    ContactRoleTypeId = rc.Contact_Role_Type_Id,
                    OwnerIsInsured = rc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = rc.Insured_Period,
                    ProductTypeDesc = rc.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllAlert(int companyId, int underwriterId)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_ALERTS_Result> temp;

            temp = globalModel.SP_GET_CASE_ALERTS(companyId, underwriterId);

            result = temp
                .Select(ac => new Case
                {
                    CorpId = ac.Corp_Id,
                    RegionId = ac.Region_Id,
                    CountryId = ac.Country_Id,
                    DomesticregId = ac.Domesticreg_Id,
                    StateProvId = ac.State_Prov_Id,
                    CityId = ac.City_Id,
                    OfficeId = ac.Office_Id,
                    CaseSeqNo = ac.Case_Seq_No,
                    HistSeqNo = ac.Hist_Seq_No,
                    PolicyStatusId = ac.Policy_Status_Id,
                    ContactId = ac.Contact_Id,
                    AddInsuredContactId = ac.AddInsuredContactId,
                    ProductId = ac.Product_Id,
                    PolicySerieId = ac.Policy_Serie_Id,
                    CompanyId = ac.Company_Id,
                    PolicyNo = ac.Policy_No,
                    PolicyStatusDesc = ac.Policy_Status_Desc,
                    SerieCode = ac.Serie_Code,
                    SerieDesc = ac.Serie_Desc,
                    ProductDesc = ac.Product_Desc,
                    FirstNameInsured = ac.FirstNameInsured,
                    MiddleNameInsured = ac.MiddleNameInsured,
                    FirstLastNameInsured = ac.FirstLastNameInsured,
                    SecondLastNameInsured = ac.SecondLastNameInsured,
                    FullNameInsured = ac.FullNameInsured,
                    FirstNameAgent = ac.FirstNameAgent,
                    MiddleNameAgent = ac.MiddleNameAgent,
                    FirstLastNameAgent = ac.FirstLastNameAgent,
                    SecondLastNameAgent = ac.SecondLastNameAgent,
                    FullNameAgent = ac.FullNameAgent,
                    BenefitAmount = ac.Benefit_Amount,
                    OfficeDesc = ac.Office_Desc,
                    CompanyDesc = ac.Company_Desc,
                    AssigedTo = ac.AssigedTo,
                    RyderTypeDesc = ac.Ryder_Type_Desc,
                    ContactRoleTypeId = ac.Contact_Role_Type_Id,
                    OwnerIsInsured = ac.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = ac.Insured_Period,
                    ProductTypeDesc = ac.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllException(int companyId, int underwriterId)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_EXCEPTIONS_Result> temp;

            temp = globalModel.SP_GET_CASE_EXCEPTIONS(companyId, underwriterId);

            result = temp
                .Select(ec => new Case
                {
                    CorpId = ec.Corp_Id,
                    RegionId = ec.Region_Id,
                    CountryId = ec.Country_Id,
                    DomesticregId = ec.Domesticreg_Id,
                    StateProvId = ec.State_Prov_Id,
                    CityId = ec.City_Id,
                    OfficeId = ec.Office_Id,
                    CaseSeqNo = ec.Case_Seq_No,
                    HistSeqNo = ec.Hist_Seq_No,
                    PolicyStatusId = ec.Policy_Status_Id,
                    ContactId = ec.Contact_Id,
                    AddInsuredContactId = ec.AddInsuredContactId,
                    ProductId = ec.Product_Id,
                    PolicySerieId = ec.Policy_Serie_Id,
                    CompanyId = ec.Company_Id,
                    PolicyNo = ec.Policy_No,
                    PolicyStatusDesc = ec.Policy_Status_Desc,
                    SerieCode = ec.Serie_Code,
                    SerieDesc = ec.Serie_Desc,
                    ProductDesc = ec.Product_Desc,
                    FirstNameInsured = ec.FirstNameInsured,
                    MiddleNameInsured = ec.MiddleNameInsured,
                    FirstLastNameInsured = ec.FirstLastNameInsured,
                    SecondLastNameInsured = ec.SecondLastNameInsured,
                    FullNameInsured = ec.FullNameInsured,
                    FirstNameAgent = ec.FirstNameAgent,
                    MiddleNameAgent = ec.MiddleNameAgent,
                    FirstLastNameAgent = ec.FirstLastNameAgent,
                    SecondLastNameAgent = ec.SecondLastNameAgent,
                    FullNameAgent = ec.FullNameAgent,
                    BenefitAmount = ec.Benefit_Amount,
                    OfficeDesc = ec.Office_Desc,
                    CompanyDesc = ec.Company_Desc,
                    AssigedTo = ec.AssigedTo,
                    ExceptionTypeDesc = ec.Exception_Type_Desc,
                    SubmitDate = ec.Submit_Date,
                    EffectiveDate = ec.Effective_Date,
                    ContactRoleTypeId = ec.Contact_Role_Type_Id,
                    OwnerIsInsured = ec.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = ec.Insured_Period,
                    ProductTypeDesc = ec.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllRecent(int companyId, int underwriterId)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_RECENT_Result> temp;

            temp = globalModel.SP_GET_CASE_RECENT(companyId, underwriterId);

            result = temp
                .Select(rc => new Case
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
                    PolicyStatusId = rc.Policy_Status_Id,
                    ContactId = rc.Contact_Id,
                    AddInsuredContactId = rc.AddInsuredContactId,
                    ProductId = rc.Product_Id,
                    PolicySerieId = rc.Policy_Serie_Id,
                    CompanyId = rc.Company_Id,
                    PolicyNo = rc.Policy_No,
                    PolicyStatusDesc = rc.Policy_Status_Desc,
                    SerieCode = rc.Serie_Code,
                    SerieDesc = rc.Serie_Desc,
                    ProductDesc = rc.Product_Desc,
                    FirstNameInsured = rc.FirstNameInsured,
                    MiddleNameInsured = rc.MiddleNameInsured,
                    FirstLastNameInsured = rc.FirstLastNameInsured,
                    SecondLastNameInsured = rc.SecondLastNameInsured,
                    FullNameInsured = rc.FullNameInsured,
                    FirstNameAgent = rc.FirstNameAgent,
                    MiddleNameAgent = rc.MiddleNameAgent,
                    FirstLastNameAgent = rc.FirstLastNameAgent,
                    SecondLastNameAgent = rc.SecondLastNameAgent,
                    FullNameAgent = rc.FullNameAgent,
                    BenefitAmount = rc.Benefit_Amount,
                    OfficeDesc = rc.Office_Desc,
                    CompanyDesc = rc.Company_Desc,
                    AssigedTo = rc.AssigedTo,
                    SubmitDate = rc.Submit_Date,
                    UserAuditTrail = rc.User_Audit_Trail,
                    ReinsuredAmount = rc.Reinsured_Amount,
                    ContactRoleTypeId = rc.Contact_Role_Type_Id,
                    OwnerIsInsured = rc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = rc.Insured_Period,
                    ProductTypeDesc = rc.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllChange(int companyId, int underwriterId)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_CHANGES_Result> temp;

            temp = globalModel.SP_GET_CASE_CHANGES(companyId, underwriterId);

            result = temp
                .Select(cc => new Case
                {
                    CorpId = cc.Corp_Id,
                    RegionId = cc.Region_Id,
                    CountryId = cc.Country_Id,
                    DomesticregId = cc.Domesticreg_Id,
                    StateProvId = cc.State_Prov_Id,
                    CityId = cc.City_Id,
                    OfficeId = cc.Office_Id,
                    CaseSeqNo = cc.Case_Seq_No,
                    HistSeqNo = cc.Hist_Seq_No,
                    PolicyStatusId = cc.Policy_Status_Id,
                    ContactId = cc.Contact_Id,
                    AddInsuredContactId = cc.AddInsuredContactId,
                    ProductId = cc.Product_Id,
                    PolicySerieId = cc.Policy_Serie_Id,
                    CompanyId = cc.Company_Id,
                    PolicyNo = cc.Policy_No,
                    PolicyStatusDesc = cc.Policy_Status_Desc,
                    SerieCode = cc.Serie_Code,
                    SerieDesc = cc.Serie_Desc,
                    ProductDesc = cc.Product_Desc,
                    FirstNameInsured = cc.FirstNameInsured,
                    MiddleNameInsured = cc.MiddleNameInsured,
                    FirstLastNameInsured = cc.FirstLastNameInsured,
                    SecondLastNameInsured = cc.SecondLastNameInsured,
                    FullNameInsured = cc.FullNameInsured,
                    BenefitAmount = cc.Benefit_Amount,
                    OfficeDesc = cc.Office_Desc,
                    CompanyDesc = cc.Company_Desc,
                    AssigedTo = cc.AssigedTo,
                    EffectiveDate = cc.Effective_Date,
                    RequestedDate = cc.Requested_Date,
                    RequestedBy = cc.Requested_By,
                    RequestedByName = cc.Requested_By_Name,
                    StepDesc = cc.Step_Desc,
                    StepTypeDesc = cc.Step_Type_Desc,
                    ContactRoleTypeId = cc.Contact_Role_Type_Id,
                    OwnerIsInsured = cc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = cc.Insured_Period,
                    ProductTypeDesc = cc.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllSearchResult(Case.SearchResult searchResult)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_SEARCH_RESULTS_Result> temp;

            temp = globalModel.SP_GET_CASE_SEARCH_RESULTS(
                    searchResult.CompanyId,
                    searchResult.FromDate,
                    searchResult.ToDate,
                    searchResult.BlTypeId,
                    searchResult.BlId,
                    searchResult.ProductId,
                    searchResult.PolicyNo,
                    searchResult.ContactFullName,
                    searchResult.CorpId,
                    searchResult.RegionId,
                    searchResult.CountryId,
                    searchResult.DomesticregId,
                    searchResult.StateProvId,
                    searchResult.CityId,
                    searchResult.OfficeId,
                    searchResult.AgentIdManager,
                    searchResult.UnderwriterId,
                    searchResult.AgentIdSubManager,
                    searchResult.AgentId
                );

            result = temp
                .Select(src => new Case
                {
                    CorpId = src.Corp_Id,
                    RegionId = src.Region_Id,
                    CountryId = src.Country_Id,
                    DomesticregId = src.Domesticreg_Id,
                    StateProvId = src.State_Prov_Id,
                    CityId = src.City_Id,
                    OfficeId = src.Office_Id,
                    CaseSeqNo = src.Case_Seq_No,
                    HistSeqNo = src.Hist_Seq_No,
                    PolicyStatusId = src.Policy_Status_Id,
                    ContactId = src.Contact_Id,
                    AddInsuredContactId = src.AddInsuredContactId,
                    ProductId = src.Product_Id,
                    PolicySerieId = src.Policy_Serie_Id,
                    CompanyId = src.Company_Id,
                    PolicyNo = src.Policy_No,
                    PolicyStatusDesc = src.Policy_Status_Desc,
                    SerieCode = src.Serie_Code,
                    SerieDesc = src.Serie_Desc,
                    ProductDesc = src.Product_Desc,
                    FirstNameInsured = src.FirstNameInsured,
                    MiddleNameInsured = src.MiddleNameInsured,
                    FirstLastNameInsured = src.FirstLastNameInsured,
                    SecondLastNameInsured = src.SecondLastNameInsured,
                    FullNameInsured = src.FullNameInsured,
                    FirstNameAgent = src.FirstNameAgent,
                    MiddleNameAgent = src.MiddleNameAgent,
                    FirstLastNameAgent = src.FirstLastNameAgent,
                    SecondLastNameAgent = src.SecondLastNameAgent,
                    FullNameAgent = src.FullNameAgent,
                    BenefitAmount = src.Benefit_Amount,
                    OfficeDesc = src.Office_Desc,
                    CompanyDesc = src.Company_Desc,
                    AssigedTo = src.AssigedTo,
                    EffectiveDate = src.Effective_Date,
                    SubmittedDays = src.Submitted_Days,
                    ContactRoleTypeId = src.Contact_Role_Type_Id,
                    OwnerIsInsured = src.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = src.Insured_Period,
                    ProductTypeDesc = src.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }

        public virtual IEnumerable<Case> GetAllHistory(int companyId, int underwriterId)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_ALL_HISTORY_Result> temp;

            temp = globalModel.SP_GET_CASE_ALL_HISTORY(companyId, underwriterId);

            result = temp
                .Select(oc => new Case
                {
                    CorpId = oc.Corp_Id,
                    RegionId = oc.Region_Id,
                    CountryId = oc.Country_Id,
                    DomesticregId = oc.Domesticreg_Id,
                    StateProvId = oc.State_Prov_Id,
                    CityId = oc.City_Id,
                    OfficeId = oc.Office_Id,
                    CaseSeqNo = oc.Case_Seq_No,
                    HistSeqNo = oc.Hist_Seq_No,
                    PolicyStatusId = oc.Policy_Status_Id,
                    ContactId = oc.Contact_Id,
                    AddInsuredContactId = oc.AddInsuredContactId,
                    ProductId = oc.Product_Id,
                    PolicySerieId = oc.Policy_Serie_Id,
                    CompanyId = oc.Company_Id,
                    PolicyNo = oc.Policy_No,
                    PolicyStatusDesc = oc.Policy_Status_Desc,
                    SerieCode = oc.Serie_Code,
                    SerieDesc = oc.Serie_Desc,
                    ProductDesc = oc.Product_Desc,
                    FirstNameInsured = oc.FirstNameInsured,
                    MiddleNameInsured = oc.MiddleNameInsured,
                    FirstLastNameInsured = oc.FirstLastNameInsured,
                    SecondLastNameInsured = oc.SecondLastNameInsured,
                    FullNameInsured = oc.FullNameInsured,
                    BenefitAmount = oc.Benefit_Amount,
                    GlobalCountryDesc = oc.Global_Country_Desc,
                    OfficeDesc = oc.Office_Desc,
                    CompanyDesc = oc.Company_Desc,
                    AssigedTo = oc.AssigedTo,
                    Priority = oc.Priority,
                    SubmitDate = oc.Submit_Date,
                    ContactRoleTypeId = oc.Contact_Role_Type_Id,
                    OwnerIsInsured = oc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = oc.Insured_Period,
                    ProductTypeDesc = oc.Product_Type_Desc,
                    Policy_No_Temp = oc.Policy_No_Temp
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Case> GetRejected(int companyId, int underwriterId)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_REJECTED_Result> temp;

            temp = globalModel.SP_GET_CASE_REJECTED(companyId, underwriterId);

            result = temp
                .Select(oc => new Case
                {
                    CorpId = oc.Corp_Id,
                    RegionId = oc.Region_Id,
                    CountryId = oc.Country_Id,
                    DomesticregId = oc.Domesticreg_Id,
                    StateProvId = oc.State_Prov_Id,
                    CityId = oc.City_Id,
                    OfficeId = oc.Office_Id,
                    CaseSeqNo = oc.Case_Seq_No,
                    HistSeqNo = oc.Hist_Seq_No,
                    PolicyStatusId = oc.Policy_Status_Id,
                    ContactId = oc.Contact_Id,
                    AddInsuredContactId = oc.AddInsuredContactId,
                    ProductId = oc.Product_Id,
                    PolicySerieId = oc.Policy_Serie_Id,
                    CompanyId = oc.Company_Id,
                    PolicyNo = oc.Policy_No,
                    PolicyStatusDesc = oc.Policy_Status_Desc,
                    SerieCode = oc.Serie_Code,
                    SerieDesc = oc.Serie_Desc,
                    ProductDesc = oc.Product_Desc,
                    FirstNameInsured = oc.FirstNameInsured,
                    MiddleNameInsured = oc.MiddleNameInsured,
                    FirstLastNameInsured = oc.FirstLastNameInsured,
                    SecondLastNameInsured = oc.SecondLastNameInsured,
                    FullNameInsured = oc.FullNameInsured,
                    BenefitAmount = oc.Benefit_Amount,
                    GlobalCountryDesc = oc.Global_Country_Desc,
                    OfficeDesc = oc.Office_Desc,
                    CompanyDesc = oc.Company_Desc,
                    AssigedTo = oc.AssigedTo,
                    Priority = oc.Priority,
                    SubmitDate = oc.Submit_Date,
                    ContactRoleTypeId = oc.Contact_Role_Type_Id,
                    OwnerIsInsured = oc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = oc.Insured_Period,
                    ProductTypeDesc = oc.Product_Type_Desc,
                    Policy_No_Temp = oc.Policy_No_Temp
                })
                .ToArray();

            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllOpen(Policy.Parameter policyParameter)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_ALL_OPEN_BYPOLICY_Result> temp;

            temp = globalModel.SP_GET_CASE_ALL_OPEN_BYPOLICY(
                policyParameter.CorpId,
                policyParameter.RegionId,
                policyParameter.CountryId,
                policyParameter.DomesticregId,
                policyParameter.StateProvId,
                policyParameter.CityId,
                policyParameter.OfficeId,
                policyParameter.CaseSeqNo,
                policyParameter.HistSeqNo);

            result = temp
                .Select(oc => new Case
                {
                    CorpId = oc.Corp_Id,
                    RegionId = oc.Region_Id,
                    CountryId = oc.Country_Id,
                    DomesticregId = oc.Domesticreg_Id,
                    StateProvId = oc.State_Prov_Id,
                    CityId = oc.City_Id,
                    OfficeId = oc.Office_Id,
                    CaseSeqNo = oc.Case_Seq_No,
                    HistSeqNo = oc.Hist_Seq_No,
                    PolicyStatusId = oc.Policy_Status_Id,
                    ContactId = oc.Contact_Id,
                    AddInsuredContactId = oc.AddInsuredContactId,
                    ProductId = oc.Product_Id,
                    PolicySerieId = oc.Policy_Serie_Id,
                    CompanyId = oc.Company_Id,
                    PolicyNo = oc.Policy_No,
                    PolicyStatusDesc = oc.Policy_Status_Desc,
                    SerieCode = oc.Serie_Code,
                    SerieDesc = oc.Serie_Desc,
                    ProductDesc = oc.Product_Desc,
                    FirstNameInsured = oc.FirstNameInsured,
                    MiddleNameInsured = oc.MiddleNameInsured,
                    FirstLastNameInsured = oc.FirstLastNameInsured,
                    SecondLastNameInsured = oc.SecondLastNameInsured,
                    FullNameInsured = oc.FullNameInsured,
                    BenefitAmount = oc.Benefit_Amount,
                    GlobalCountryDesc = oc.Global_Country_Desc,
                    OfficeDesc = oc.Office_Desc,
                    CompanyDesc = oc.Company_Desc,
                    AssigedTo = oc.AssigedTo,
                    Priority = oc.Priority,
                    SubmitDate = oc.Submit_Date,
                    ContactRoleTypeId = oc.Contact_Role_Type_Id,
                    OwnerIsInsured = oc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = oc.Insured_Period,
                    ProductTypeDesc = oc.Product_Type_Desc
                })
                .ToArray();

            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllProcessing(Policy.Parameter policyParameter)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_PROCESSING_BYPOLICY_Result> temp;

            temp = globalModel.SP_GET_CASE_PROCESSING_BYPOLICY(
                policyParameter.CorpId,
                policyParameter.RegionId,
                policyParameter.CountryId,
                policyParameter.DomesticregId,
                policyParameter.StateProvId,
                policyParameter.CityId,
                policyParameter.OfficeId,
                policyParameter.CaseSeqNo,
                policyParameter.HistSeqNo);

            result = temp
                .Select(pc => new Case
                {
                    CorpId = pc.Corp_Id,
                    RegionId = pc.Region_Id,
                    CountryId = pc.Country_Id,
                    DomesticregId = pc.Domesticreg_Id,
                    StateProvId = pc.State_Prov_Id,
                    CityId = pc.City_Id,
                    OfficeId = pc.Office_Id,
                    CaseSeqNo = pc.Case_Seq_No,
                    HistSeqNo = pc.Hist_Seq_No,
                    PolicyStatusId = pc.Policy_Status_Id,
                    ContactId = pc.Contact_Id,
                    AddInsuredContactId = pc.AddInsuredContactId,
                    ProductId = pc.Product_Id,
                    PolicySerieId = pc.Policy_Serie_Id,
                    CompanyId = pc.Company_Id,
                    PolicyNo = pc.Policy_No,
                    PolicyStatusDesc = pc.Policy_Status_Desc,
                    SerieCode = pc.Serie_Code,
                    SerieDesc = pc.Serie_Desc,
                    ProductDesc = pc.Product_Desc,
                    FirstNameInsured = pc.FirstNameInsured,
                    MiddleNameInsured = pc.MiddleNameInsured,
                    FirstLastNameInsured = pc.FirstLastNameInsured,
                    SecondLastNameInsured = pc.SecondLastNameInsured,
                    FullNameInsured = pc.FullNameInsured,
                    FirstNameAgent = pc.FirstNameAgent,
                    MiddleNameAgent = pc.MiddleNameAgent,
                    FirstLastNameAgent = pc.FirstLastNameAgent,
                    SecondLastNameAgent = pc.SecondLastNameAgent,
                    FullNameAgent = pc.FullNameAgent,
                    BenefitAmount = pc.Benefit_Amount,
                    OfficeDesc = pc.Office_Desc,
                    Days = pc.Days,
                    CompanyDesc = pc.Company_Desc,
                    AssigedTo = pc.AssigedTo,
                    Priority = pc.Priority,
                    Steps = pc.Steps,
                    ContactRoleTypeId = pc.Contact_Role_Type_Id,
                    OwnerIsInsured = pc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = pc.Insured_Period,
                    ProductTypeDesc = pc.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllAwaitingInformation(Policy.Parameter policyParameter)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_AWAITING_INFO_BYPOLICY_Result> temp;

            temp = globalModel.SP_GET_CASE_AWAITING_INFO_BYPOLICY(
                policyParameter.CorpId,
                policyParameter.RegionId,
                policyParameter.CountryId,
                policyParameter.DomesticregId,
                policyParameter.StateProvId,
                policyParameter.CityId,
                policyParameter.OfficeId,
                policyParameter.CaseSeqNo,
                policyParameter.HistSeqNo);

            result = temp
                .Select(aic => new Case
                {
                    CorpId = aic.Corp_Id,
                    RegionId = aic.Region_Id,
                    CountryId = aic.Country_Id,
                    DomesticregId = aic.Domesticreg_Id,
                    StateProvId = aic.State_Prov_Id,
                    CityId = aic.City_Id,
                    OfficeId = aic.Office_Id,
                    CaseSeqNo = aic.Case_Seq_No,
                    HistSeqNo = aic.Hist_Seq_No,
                    PolicyStatusId = aic.Policy_Status_Id,
                    ContactId = aic.Contact_Id,
                    AddInsuredContactId = aic.AddInsuredContactId,
                    ProductId = aic.Product_Id,
                    PolicySerieId = aic.Policy_Serie_Id,
                    CompanyId = aic.Company_Id,
                    PolicyNo = aic.Policy_No,
                    PolicyStatusDesc = aic.Policy_Status_Desc,
                    SerieCode = aic.Serie_Code,
                    SerieDesc = aic.Serie_Desc,
                    ProductDesc = aic.Product_Desc,
                    FirstNameInsured = aic.FirstNameInsured,
                    MiddleNameInsured = aic.MiddleNameInsured,
                    FirstLastNameInsured = aic.FirstLastNameInsured,
                    SecondLastNameInsured = aic.SecondLastNameInsured,
                    FullNameInsured = aic.FullNameInsured,
                    FirstNameAgent = aic.FirstNameAgent,
                    MiddleNameAgent = aic.MiddleNameAgent,
                    FirstLastNameAgent = aic.FirstLastNameAgent,
                    SecondLastNameAgent = aic.SecondLastNameAgent,
                    FullNameAgent = aic.FullNameAgent,
                    BenefitAmount = aic.Benefit_Amount,
                    OfficeDesc = aic.Office_Desc,
                    CompanyDesc = aic.Company_Desc,
                    AssigedTo = aic.AssigedTo,
                    SubmitDate = aic.Submit_Date,
                    StepInAwaiting = aic.StepInAwaiting,
                    TimeInAwaiting = aic.TimeInAwaiting,
                    ContactRoleTypeId = aic.Contact_Role_Type_Id,
                    OwnerIsInsured = aic.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = aic.Insured_Period,
                    ProductTypeDesc = aic.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllReinsurance(Policy.Parameter policyParameter)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_REINSURANCE_BYPOLICY_Result> temp;

            temp = globalModel.SP_GET_CASE_REINSURANCE_BYPOLICY(
                policyParameter.CorpId,
                policyParameter.RegionId,
                policyParameter.CountryId,
                policyParameter.DomesticregId,
                policyParameter.StateProvId,
                policyParameter.CityId,
                policyParameter.OfficeId,
                policyParameter.CaseSeqNo,
                policyParameter.HistSeqNo);

            result = temp
                .Select(rc => new Case
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
                    PolicyStatusId = rc.Policy_Status_Id,
                    ContactId = rc.Contact_Id,
                    AddInsuredContactId = rc.AddInsuredContactId,
                    ProductId = rc.Product_Id,
                    PolicySerieId = rc.Policy_Serie_Id,
                    CompanyId = rc.Company_Id,
                    PolicyNo = rc.Policy_No,
                    PolicyStatusDesc = rc.Policy_Status_Desc,
                    SerieCode = rc.Serie_Code,
                    SerieDesc = rc.Serie_Desc,
                    ProductDesc = rc.Product_Desc,
                    FirstNameInsured = rc.FirstNameInsured,
                    MiddleNameInsured = rc.MiddleNameInsured,
                    FirstLastNameInsured = rc.FirstLastNameInsured,
                    SecondLastNameInsured = rc.SecondLastNameInsured,
                    FullNameInsured = rc.FullNameInsured,
                    FirstNameAgent = rc.FirstNameAgent,
                    MiddleNameAgent = rc.MiddleNameAgent,
                    FirstLastNameAgent = rc.FirstLastNameAgent,
                    SecondLastNameAgent = rc.SecondLastNameAgent,
                    FullNameAgent = rc.FullNameAgent,
                    BenefitAmount = rc.Benefit_Amount,
                    OfficeDesc = rc.Office_Desc,
                    CompanyDesc = rc.Company_Desc,
                    AssigedTo = rc.AssigedTo,
                    RyderTypeDesc = rc.Ryder_Type_Desc,
                    ReinsuredAmount = rc.Reinsured_Amount,
                    Reinsurer = rc.Reinsurer,
                    DateSentToReinsurance = rc.Date_Sent_To_Reinsurance,
                    TimeInReinsurance = rc.Time_In_Reinsurance,
                    ContactRoleTypeId = rc.Contact_Role_Type_Id,
                    OwnerIsInsured = rc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = rc.Insured_Period,
                    ProductTypeDesc = rc.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllAlert(Policy.Parameter policyParameter)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_ALERTS_BYPOLICY_Result> temp;

            temp = globalModel.SP_GET_CASE_ALERTS_BYPOLICY(
                policyParameter.CorpId,
                policyParameter.RegionId,
                policyParameter.CountryId,
                policyParameter.DomesticregId,
                policyParameter.StateProvId,
                policyParameter.CityId,
                policyParameter.OfficeId,
                policyParameter.CaseSeqNo,
                policyParameter.HistSeqNo);

            result = temp
                .Select(ac => new Case
                {
                    CorpId = ac.Corp_Id,
                    RegionId = ac.Region_Id,
                    CountryId = ac.Country_Id,
                    DomesticregId = ac.Domesticreg_Id,
                    StateProvId = ac.State_Prov_Id,
                    CityId = ac.City_Id,
                    OfficeId = ac.Office_Id,
                    CaseSeqNo = ac.Case_Seq_No,
                    HistSeqNo = ac.Hist_Seq_No,
                    PolicyStatusId = ac.Policy_Status_Id,
                    ContactId = ac.Contact_Id,
                    AddInsuredContactId = ac.AddInsuredContactId,
                    ProductId = ac.Product_Id,
                    PolicySerieId = ac.Policy_Serie_Id,
                    CompanyId = ac.Company_Id,
                    PolicyNo = ac.Policy_No,
                    PolicyStatusDesc = ac.Policy_Status_Desc,
                    SerieCode = ac.Serie_Code,
                    SerieDesc = ac.Serie_Desc,
                    ProductDesc = ac.Product_Desc,
                    FirstNameInsured = ac.FirstNameInsured,
                    MiddleNameInsured = ac.MiddleNameInsured,
                    FirstLastNameInsured = ac.FirstLastNameInsured,
                    SecondLastNameInsured = ac.SecondLastNameInsured,
                    FullNameInsured = ac.FullNameInsured,
                    FirstNameAgent = ac.FirstNameAgent,
                    MiddleNameAgent = ac.MiddleNameAgent,
                    FirstLastNameAgent = ac.FirstLastNameAgent,
                    SecondLastNameAgent = ac.SecondLastNameAgent,
                    FullNameAgent = ac.FullNameAgent,
                    BenefitAmount = ac.Benefit_Amount,
                    OfficeDesc = ac.Office_Desc,
                    CompanyDesc = ac.Company_Desc,
                    AssigedTo = ac.AssigedTo,
                    RyderTypeDesc = ac.Ryder_Type_Desc,
                    ContactRoleTypeId = ac.Contact_Role_Type_Id,
                    OwnerIsInsured = ac.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = ac.Insured_Period,
                    ProductTypeDesc = ac.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllException(Policy.Parameter policyParameter)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_EXCEPTIONS_BYPOLICY_Result> temp;

            temp = globalModel.SP_GET_CASE_EXCEPTIONS_BYPOLICY(
                policyParameter.CorpId,
                policyParameter.RegionId,
                policyParameter.CountryId,
                policyParameter.DomesticregId,
                policyParameter.StateProvId,
                policyParameter.CityId,
                policyParameter.OfficeId,
                policyParameter.CaseSeqNo,
                policyParameter.HistSeqNo);

            result = temp
                .Select(ec => new Case
                {
                    CorpId = ec.Corp_Id,
                    RegionId = ec.Region_Id,
                    CountryId = ec.Country_Id,
                    DomesticregId = ec.Domesticreg_Id,
                    StateProvId = ec.State_Prov_Id,
                    CityId = ec.City_Id,
                    OfficeId = ec.Office_Id,
                    CaseSeqNo = ec.Case_Seq_No,
                    HistSeqNo = ec.Hist_Seq_No,
                    PolicyStatusId = ec.Policy_Status_Id,
                    ContactId = ec.Contact_Id,
                    AddInsuredContactId = ec.AddInsuredContactId,
                    ProductId = ec.Product_Id,
                    PolicySerieId = ec.Policy_Serie_Id,
                    CompanyId = ec.Company_Id,
                    PolicyNo = ec.Policy_No,
                    PolicyStatusDesc = ec.Policy_Status_Desc,
                    SerieCode = ec.Serie_Code,
                    SerieDesc = ec.Serie_Desc,
                    ProductDesc = ec.Product_Desc,
                    FirstNameInsured = ec.FirstNameInsured,
                    MiddleNameInsured = ec.MiddleNameInsured,
                    FirstLastNameInsured = ec.FirstLastNameInsured,
                    SecondLastNameInsured = ec.SecondLastNameInsured,
                    FullNameInsured = ec.FullNameInsured,
                    FirstNameAgent = ec.FirstNameAgent,
                    MiddleNameAgent = ec.MiddleNameAgent,
                    FirstLastNameAgent = ec.FirstLastNameAgent,
                    SecondLastNameAgent = ec.SecondLastNameAgent,
                    FullNameAgent = ec.FullNameAgent,
                    BenefitAmount = ec.Benefit_Amount,
                    OfficeDesc = ec.Office_Desc,
                    CompanyDesc = ec.Company_Desc,
                    AssigedTo = ec.AssigedTo,
                    ExceptionTypeDesc = ec.Exception_Type_Desc,
                    SubmitDate = ec.Submit_Date,
                    EffectiveDate = ec.Effective_Date,
                    ContactRoleTypeId = ec.Contact_Role_Type_Id,
                    OwnerIsInsured = ec.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = ec.Insured_Period,
                    ProductTypeDesc = ec.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllRecent(Policy.Parameter policyParameter)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_RECENT_BYPOLICY_Result> temp;

            temp = globalModel.SP_GET_CASE_RECENT_BYPOLICY(
                policyParameter.CorpId,
                policyParameter.RegionId,
                policyParameter.CountryId,
                policyParameter.DomesticregId,
                policyParameter.StateProvId,
                policyParameter.CityId,
                policyParameter.OfficeId,
                policyParameter.CaseSeqNo,
                policyParameter.HistSeqNo);

            result = temp
                .Select(rc => new Case
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
                    PolicyStatusId = rc.Policy_Status_Id,
                    ContactId = rc.Contact_Id,
                    AddInsuredContactId = rc.AddInsuredContactId,
                    ProductId = rc.Product_Id,
                    PolicySerieId = rc.Policy_Serie_Id,
                    CompanyId = rc.Company_Id,
                    PolicyNo = rc.Policy_No,
                    PolicyStatusDesc = rc.Policy_Status_Desc,
                    SerieCode = rc.Serie_Code,
                    SerieDesc = rc.Serie_Desc,
                    ProductDesc = rc.Product_Desc,
                    FirstNameInsured = rc.FirstNameInsured,
                    MiddleNameInsured = rc.MiddleNameInsured,
                    FirstLastNameInsured = rc.FirstLastNameInsured,
                    SecondLastNameInsured = rc.SecondLastNameInsured,
                    FullNameInsured = rc.FullNameInsured,
                    FirstNameAgent = rc.FirstNameAgent,
                    MiddleNameAgent = rc.MiddleNameAgent,
                    FirstLastNameAgent = rc.FirstLastNameAgent,
                    SecondLastNameAgent = rc.SecondLastNameAgent,
                    FullNameAgent = rc.FullNameAgent,
                    BenefitAmount = rc.Benefit_Amount,
                    OfficeDesc = rc.Office_Desc,
                    CompanyDesc = rc.Company_Desc,
                    AssigedTo = rc.AssigedTo,
                    SubmitDate = rc.Submit_Date,
                    UserAuditTrail = rc.User_Audit_Trail,
                    ReinsuredAmount = rc.Reinsured_Amount,
                    ContactRoleTypeId = rc.Contact_Role_Type_Id,
                    OwnerIsInsured = rc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = rc.Insured_Period,
                    ProductTypeDesc = rc.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllChange(Policy.Parameter policyParameter)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_CHANGES_BYPOLICY_Result> temp;

            temp = globalModel.SP_GET_CASE_CHANGES_BYPOLICY(
                policyParameter.CorpId,
                policyParameter.RegionId,
                policyParameter.CountryId,
                policyParameter.DomesticregId,
                policyParameter.StateProvId,
                policyParameter.CityId,
                policyParameter.OfficeId,
                policyParameter.CaseSeqNo,
                policyParameter.HistSeqNo);

            result = temp
                .Select(cc => new Case
                {
                    CorpId = cc.Corp_Id,
                    RegionId = cc.Region_Id,
                    CountryId = cc.Country_Id,
                    DomesticregId = cc.Domesticreg_Id,
                    StateProvId = cc.State_Prov_Id,
                    CityId = cc.City_Id,
                    OfficeId = cc.Office_Id,
                    CaseSeqNo = cc.Case_Seq_No,
                    HistSeqNo = cc.Hist_Seq_No,
                    PolicyStatusId = cc.Policy_Status_Id,
                    ContactId = cc.Contact_Id,
                    AddInsuredContactId = cc.AddInsuredContactId,
                    ProductId = cc.Product_Id,
                    PolicySerieId = cc.Policy_Serie_Id,
                    CompanyId = cc.Company_Id,
                    PolicyNo = cc.Policy_No,
                    PolicyStatusDesc = cc.Policy_Status_Desc,
                    SerieCode = cc.Serie_Code,
                    SerieDesc = cc.Serie_Desc,
                    ProductDesc = cc.Product_Desc,
                    FirstNameInsured = cc.FirstNameInsured,
                    MiddleNameInsured = cc.MiddleNameInsured,
                    FirstLastNameInsured = cc.FirstLastNameInsured,
                    SecondLastNameInsured = cc.SecondLastNameInsured,
                    FullNameInsured = cc.FullNameInsured,
                    BenefitAmount = cc.Benefit_Amount,
                    OfficeDesc = cc.Office_Desc,
                    CompanyDesc = cc.Company_Desc,
                    AssigedTo = cc.AssigedTo,
                    EffectiveDate = cc.Effective_Date,
                    RequestedDate = cc.Requested_Date,
                    RequestedBy = cc.Requested_By,
                    RequestedByName = cc.Requested_By_Name,
                    StepDesc = cc.Step_Desc,
                    StepTypeDesc = cc.Step_Type_Desc,
                    ContactRoleTypeId = cc.Contact_Role_Type_Id,
                    OwnerIsInsured = cc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = cc.Insured_Period,
                    ProductTypeDesc = cc.Product_Type_Desc
                })
                .ToArray();


            return
                result;
        }
        
        public virtual IEnumerable<Case> GetAllSearchResult(Policy.Parameter policyParameter)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_SEARCH_RESULTS_BYPOLICY_Result> temp;

            temp = globalModel.SP_GET_CASE_SEARCH_RESULTS_BYPOLICY(
                policyParameter.CorpId,
                policyParameter.RegionId,
                policyParameter.CountryId,
                policyParameter.DomesticregId,
                policyParameter.StateProvId,
                policyParameter.CityId,
                policyParameter.OfficeId,
                policyParameter.CaseSeqNo,
                policyParameter.HistSeqNo);

            result = temp
                .Select(src => new Case
                {
                    CorpId = src.Corp_Id,
                    RegionId = src.Region_Id,
                    CountryId = src.Country_Id,
                    DomesticregId = src.Domesticreg_Id,
                    StateProvId = src.State_Prov_Id,
                    CityId = src.City_Id,
                    OfficeId = src.Office_Id,
                    CaseSeqNo = src.Case_Seq_No,
                    HistSeqNo = src.Hist_Seq_No,
                    PolicyStatusId = src.Policy_Status_Id,
                    ContactId = src.Contact_Id,
                    AddInsuredContactId = src.AddInsuredContactId,
                    ProductId = src.Product_Id,
                    PolicySerieId = src.Policy_Serie_Id,
                    CompanyId = src.Company_Id,
                    PolicyNo = src.Policy_No,
                    PolicyStatusDesc = src.Policy_Status_Desc,
                    SerieCode = src.Serie_Code,
                    SerieDesc = src.Serie_Desc,
                    ProductDesc = src.Product_Desc,
                    FirstNameInsured = src.FirstNameInsured,
                    MiddleNameInsured = src.MiddleNameInsured,
                    FirstLastNameInsured = src.FirstLastNameInsured,
                    SecondLastNameInsured = src.SecondLastNameInsured,
                    FullNameInsured = src.FullNameInsured,
                    FirstNameAgent = src.FirstNameAgent,
                    MiddleNameAgent = src.MiddleNameAgent,
                    FirstLastNameAgent = src.FirstLastNameAgent,
                    SecondLastNameAgent = src.SecondLastNameAgent,
                    FullNameAgent = src.FullNameAgent,
                    BenefitAmount = src.Benefit_Amount,
                    OfficeDesc = src.Office_Desc,
                    CompanyDesc = src.Company_Desc,
                    AssigedTo = src.AssigedTo,
                    EffectiveDate = src.Effective_Date,
                    SubmittedDays = src.Submitted_Days,
                    ContactRoleTypeId = src.Contact_Role_Type_Id,
                    OwnerIsInsured = src.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = src.Insured_Period,
                    ProductTypeDesc = src.Product_Type_Desc
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Case> GetAllHistory(Policy.Parameter policyParameter)
        {
            IEnumerable<Case> result;
            IEnumerable<SP_GET_CASE_ALL_HISTORY_BYPOLICY_Result> temp;

            temp = globalModelExtended.SP_GET_CASE_ALL_HISTORY_BYPOLICY(
                policyParameter.CorpId,
                policyParameter.RegionId,
                policyParameter.CountryId,
                policyParameter.DomesticregId,
                policyParameter.StateProvId,
                policyParameter.CityId,
                policyParameter.OfficeId,
                policyParameter.CaseSeqNo,
                policyParameter.HistSeqNo);

            result = temp
                .Select(oc => new Case
                {
                    CorpId = oc.Corp_Id,
                    RegionId = oc.Region_Id,
                    CountryId = oc.Country_Id,
                    DomesticregId = oc.Domesticreg_Id,
                    StateProvId = oc.State_Prov_Id,
                    CityId = oc.City_Id,
                    OfficeId = oc.Office_Id,
                    CaseSeqNo = oc.Case_Seq_No,
                    HistSeqNo = oc.Hist_Seq_No,
                    PolicyStatusId = oc.Policy_Status_Id,
                    ContactId = oc.Contact_Id,
                    AddInsuredContactId = oc.AddInsuredContactId,
                    ProductId = oc.Product_Id,
                    PolicySerieId = oc.Policy_Serie_Id,
                    CompanyId = oc.Company_Id,
                    PolicyNo = oc.Policy_No,
                    PolicyStatusDesc = oc.Policy_Status_Desc,
                    SerieCode = oc.Serie_Code,
                    SerieDesc = oc.Serie_Desc,
                    ProductDesc = oc.Product_Desc,
                    FirstNameInsured = oc.FirstNameInsured,
                    MiddleNameInsured = oc.MiddleNameInsured,
                    FirstLastNameInsured = oc.FirstLastNameInsured,
                    SecondLastNameInsured = oc.SecondLastNameInsured,
                    FullNameInsured = oc.FullNameInsured,
                    BenefitAmount = oc.Benefit_Amount,
                    GlobalCountryDesc = oc.Global_Country_Desc,
                    OfficeDesc = oc.Office_Desc,
                    CompanyDesc = oc.Company_Desc,
                    AssigedTo = oc.AssigedTo,
                    Priority = oc.Priority,
                    SubmitDate = oc.Submit_Date,
                    ContactRoleTypeId = oc.Contact_Role_Type_Id,
                    OwnerIsInsured = oc.OwnerIsInsured.ConvertToNoNullable(),
                    InsuredPeriod = oc.Insured_Period,
                    ProductTypeDesc = oc.Product_Type_Desc
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Case.Process> GetAllInProcess(Policy.NBParameter paramerter)
        {
            IEnumerable<Case.Process> result;
            IEnumerable<SP_GET_CASE_IN_PROCESS_Result> temp;

            temp = globalModel.SP_GET_CASE_IN_PROCESS(
                    paramerter.CorpId,
                    paramerter.RegionId,
                    paramerter.CountryId,
                    paramerter.DomesticregId,
                    paramerter.StateProvId,
                    paramerter.CityId,
                    paramerter.OfficeId,
                    paramerter.AgentId,
                    paramerter.LanguageId
                );

            result = temp
                .Select(cp => new Case.Process
                {
                    CorpId = cp.Corp_Id,
                    RegionId = cp.Region_Id,
                    CountryId = cp.Country_Id,
                    DomesticregId = cp.Domesticreg_Id,
                    StateProvId = cp.State_Prov_Id,
                    CityId = cp.City_Id,
                    OfficeId = cp.Office_Id,
                    CaseSeqNo = cp.Case_Seq_No,
                    HistSeqNo = cp.Hist_Seq_No,
                    PolicyNo = cp.Policy_No,
                    OwnerContactId = cp.OwnerContactId,
                    InsuredContactId = cp.InsuredContactId,
                    DesignatedPensionerContactId = cp.DesignatedPensionerContactId,
                    AddInsuredContactId = cp.AddInsuredContactId,
                    AgentLegalContactId = cp.AgentLegalContactId,
                    StudentNameContactId = cp.StudentNameContactId,
                    AgentId = cp.Agent_Id,
                    BussinessLineType = cp.Bussiness_Line_Type,
                    BussinessLineId = cp.Bussiness_Line_Id,
                    ProductId = cp.Product_Id,
                    LastUpdate = cp.LastUpdate,
                    UserId = cp.UserId,
                    ProductDesc = cp.Product_Desc,
                    OwnerFullName = cp.OwnerFullName,
                    InsuranceFullName = cp.InsuranceFullName,
                    AgentFullName = cp.AgentFullName,
                    UserFullName = cp.UserFullName,
                    HasComment = cp.HasComment.ConvertToNoNullable(),
                    PaymentId = cp.Payment_Id,
                    CanGoRequirement = cp.CanGoRequirement.ConvertToNoNullable(),
                    OfficeDesc = cp.Office_Desc,
                    CountryDesc = cp.Country_Desc,
                    Exception = cp.Exception,
                    ProductNameKey = cp.Product_Name_Key,
                    ProductTypeDesc = cp.Product_Type_Desc
                })
                .ToArray();

            return
                result;
        }
        
        public virtual IEnumerable<Case.Process> GetAllInReview(Policy.NBParameter paramerter)
        {
            IEnumerable<Case.Process> result;
            IEnumerable<SP_GET_CASE_IN_REVIEW_Result> temp;

            temp = globalModel.SP_GET_CASE_IN_REVIEW(
                    paramerter.CorpId,
                    paramerter.RegionId,
                    paramerter.CountryId,
                    paramerter.DomesticregId,
                    paramerter.StateProvId,
                    paramerter.CityId,
                    paramerter.OfficeId,
                    paramerter.AgentId
                );

            result = temp
                .Select(cp => new Case.Process
                {
                    CorpId = cp.Corp_Id,
                    RegionId = cp.Region_Id,
                    CountryId = cp.Country_Id,
                    DomesticregId = cp.Domesticreg_Id,
                    StateProvId = cp.State_Prov_Id,
                    CityId = cp.City_Id,
                    OfficeId = cp.Office_Id,
                    CaseSeqNo = cp.Case_Seq_No,
                    HistSeqNo = cp.Hist_Seq_No,
                    PolicyNo = cp.Policy_No,
                    OwnerContactId = cp.OwnerContactId,
                    InsuredContactId = cp.InsuredContactId,
                    DesignatedPensionerContactId = cp.DesignatedPensionerContactId,
                    AddInsuredContactId = cp.AddInsuredContactId,
                    StudentNameContactId = cp.StudentNameContactId,
                    AgentId = cp.Agent_Id,
                    BussinessLineType = cp.Bussiness_Line_Type,
                    BussinessLineId = cp.Bussiness_Line_Id,
                    ProductId = cp.Product_Id,
                    LastUpdate = cp.LastUpdate,
                    UserId = cp.UserId,
                    ProductDesc = cp.Product_Desc,
                    OwnerFullName = cp.OwnerFullName,
                    InsuranceFullName = cp.InsuranceFullName,
                    AgentFullName = cp.AgentFullName,
                    UserFullName = cp.UserFullName,
                    HasComment = cp.HasComment.ConvertToNoNullable(),
                    PaymentId = cp.Payment_Id,
                    IsPaymentCompleted = cp.IsPaymentCompleted.ConvertToNoNullable(),
                    OfficeDesc = cp.Office_Desc,
                    CountryDesc = cp.Country_Desc,
                    Exception = cp.Exception,
                    ProductNameKey = cp.Product_Name_Key,
                    ProductTypeId = cp.Product_Type_Id,
                    ProductTypeDesc = cp.Product_Type_Desc
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Case.Process> GetAllEffectivePendingReceipt(Policy.NBParameter paramerter)
        {
            IEnumerable<Case.Process> result;
            IEnumerable<SP_GET_EFFECTIVE_PENDING_RECEIPT_Result> temp;

            temp = globalModel.SP_GET_EFFECTIVE_PENDING_RECEIPT(
                    paramerter.CorpId,
                    paramerter.RegionId,
                    paramerter.CountryId,
                    paramerter.DomesticregId,
                    paramerter.StateProvId,
                    paramerter.CityId,
                    paramerter.OfficeId,
                    paramerter.AgentId
                );

            result = temp
                .Select(ep => new Case.Process
                {
                    CorpId = ep.Corp_Id,
                    RegionId = ep.Region_Id,
                    CountryId = ep.Country_Id,
                    DomesticregId = ep.Domesticreg_Id,
                    StateProvId = ep.State_Prov_Id,
                    CityId = ep.City_Id,
                    OfficeId = ep.Office_Id,
                    CaseSeqNo = ep.Case_Seq_No,
                    HistSeqNo = ep.Hist_Seq_No,
                    PolicyNo = ep.Policy_No,
                    OwnerContactId = ep.OwnerContactId,
                    InsuredContactId = ep.InsuredContactId,
                    AgentId = ep.Agent_Id,
                    BussinessLineType = ep.Bussiness_Line_Type,
                    BussinessLineId = ep.Bussiness_Line_Id,
                    ProductId = ep.Product_Id,
                    ProductDesc = ep.Product_Desc,
                    OwnerFullName = ep.OwnerFullName,
                    InsuranceFullName = ep.InsuredFullName,
                    AgentFullName = ep.AgentFullName,
                    DaysLate = ep.DaysLate,
                    Status = ep.Status,
                    EffectiveDate = ep.EffectiveDate,
                    AmmendmentId = ep.Ammendment_Id,
                    RequirementContactId = ep.Requirement_Contact_Id,
                    RequirementCatId = ep.Requirement_Cat_Id,
                    RequirementTypeId = ep.Requirement_Type_Id,
                    RequirementId = ep.Requirement_Id,
                    IsAmmendReq = ep.IsAmmendReq,
                    AmmendmentDate = ep.Ammendment_Date
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Case.Queue> GetQueue(int companyId, int underwriterId)
        {
            IEnumerable<Case.Queue> result;
            IEnumerable<SP_GET_CASE_QUEUE_Result> temp;

            temp = globalModel.SP_GET_CASE_QUEUE(companyId, underwriterId);

            result = temp
                .Select(cq => new Case.Queue
                {
                    TabName = cq.TabName,
                    OnTime = cq.OnTime.ConvertToNoNullable(),
                    Delayed = cq.Delayed.ConvertToNoNullable()
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Case.TabCount> GetAllTabCounts(int companyId, int underwriterId)
        {
            IEnumerable<Case.TabCount> result;
            IEnumerable<SP_GET_CASE_TAB_COUNT_Result> temp;

            temp = globalModel.SP_GET_CASE_TAB_COUNT(companyId, underwriterId);

            result = temp
                    .Select(tc => new Case.TabCount
                    {
                        Order = tc.Order.ConvertToNoNullable(),
                        TabName = tc.TabName,
                        Count = tc.Count.ConvertToNoNullable()
                    })
                    .ToArray();

            return
                result;
        }

        public virtual Policy GenerateNew(Case.NewCase newCase)
        {
            Policy result;
            IEnumerable<SP_SET_NEW_CASE_WITH_INSURED_Result> temp;

            temp = globalModel.SP_SET_NEW_CASE_WITH_INSURED(
                  newCase.CorpId,
                  newCase.RegionId,
                  newCase.CountryId,
                  newCase.DomesticregId,
                  newCase.StateProvId,
                  newCase.CityId,
                  newCase.OfficeId,
                  newCase.ContactId,
                  newCase.AgentId,
                  newCase.ContactTypeId,
                  newCase.FirstName,
                  newCase.MiddleName,
                  newCase.FirstLastName,
                  newCase.SecondLastName,
                  newCase.Nickname,
                  newCase.InstitutionalName,
                  newCase.InstitutionalCountryId,
                  newCase.Dob,
                  newCase.Age,
                  newCase.NearAge,
                  newCase.Gender,
                  newCase.MaritalStatId,
                  newCase.DirectoryId,
                  newCase.RegionOfResidenceId,
                  newCase.CountryOfResidenceId,
                  newCase.DomesticRegOfResidenceId,
                  newCase.StateOfResidenceId,
                  newCase.CityOfResidenceId,
                  newCase.RegionOfBirthId,
                  newCase.CountryOfBirthId,
                  newCase.Weigth,
                  newCase.WeigthTypeId,
                  newCase.Height,
                  newCase.HeigthTypeId,
                  newCase.LineOfBusiness,
                  newCase.LineOfBusiness2,
                  newCase.CompanyName,
                  newCase.LengthWorkYear,
                  newCase.LengthWorkMonth,
                  newCase.LaborTasks,
                  newCase.IsCompany,
                  newCase.CompanyActivity,
                  newCase.CompanyFoundationDate,
                  newCase.OccupGroupTypeId,
                  newCase.OccupationId,
                  newCase.RelationshiptoAgent,
                  newCase.RelationshiptoOwner,
                  newCase.AnnualFamilyIncome,
                  newCase.AnnualPersonalIncome,
                  newCase.Smoker,
                  newCase.ReferredByRelationshipId,
                  newCase.ReferredByContactId,
                  newCase.ContactStatusId,
                  newCase.NameId,
                  newCase.NcfTypeId,
                  newCase.IsIllustration,
                  newCase.IllustrationNo,
                  newCase.UserId,
                  newCase.isGroupIllustration
                  );

            result = temp
                    .Select(p => new Policy
                    {
                        CorpId = p.Corp_Id.ConvertToNoNullable(),
                        RegionId = p.Region_Id.ConvertToNoNullable(),
                        CountryId = p.Country_Id.ConvertToNoNullable(),
                        DomesticregId = p.Domesticreg_Id.ConvertToNoNullable(),
                        StateProvId = p.State_Prov_Id.ConvertToNoNullable(),
                        CityId = p.City_Id.ConvertToNoNullable(),
                        OfficeId = p.Office_Id.ConvertToNoNullable(),
                        CaseSeqNo = p.Case_Seq_No.ConvertToNoNullable(),
                        HistSeqNo = p.Hist_Seq_No.ConvertToNoNullable(),
                        PolicyNo = p.Policy_No,
                        ContactId = p.Contact_Id.ConvertToNoNullable(),
                        PaymentId = p.Payment_Id.ConvertToNoNullable(),
                    })
                    .FirstOrDefault();

            return
                result;
        }

        public virtual Case.CaseResult SendToReview(Case currentCase)
        {
            Case.CaseResult result;
            IEnumerable<SP_SET_SEND_TO_REVIEW_Result> temp;

            temp = globalModel.SP_SET_SEND_TO_REVIEW(
                      currentCase.CorpId,
                      currentCase.RegionId,
                      currentCase.CountryId,
                      currentCase.DomesticregId,
                      currentCase.StateProvId,
                      currentCase.CityId,
                      currentCase.OfficeId,
                      currentCase.CaseSeqNo,
                      currentCase.HistSeqNo,
                      currentCase.ProjectId,
                      currentCase.StatusChangeTypeId,
                      currentCase.UserId
                  );

            result = temp
                        .Select(c => new Case.CaseResult
                        {
                            Result = c.Result.ConvertToNoNullable(),
                            ResultMessage = c.ResultMessage
                        })
                        .FirstOrDefault();

            return
                result;
        }
        
        public virtual Case.CaseResult SendToReject(Case currentCase)
        {
            Case.CaseResult result;
            IEnumerable<SP_SET_SEND_TO_REJECT_Result> temp;

            temp = globalModel.SP_SET_SEND_TO_REJECT(
                  currentCase.CorpId,
                  currentCase.RegionId,
                  currentCase.CountryId,
                  currentCase.DomesticregId,
                  currentCase.StateProvId,
                  currentCase.CityId,
                  currentCase.OfficeId,
                  currentCase.CaseSeqNo,
                  currentCase.HistSeqNo,
                  currentCase.UserId
                  )
                  .ToArray();

            if (temp != null && temp.Any())
            {
                result = temp
                            .Select(c => new Case.CaseResult
                            {
                                Result = c.Result.Value,
                                ResultMessage = c.ResultMessage
                            })
                            .FirstOrDefault();
            }
            else
                result = new Case.CaseResult
                {
                    Result = false,
                    ResultMessage = "Error!!!"
                };

            return
                result;
        }
        
        public virtual Case.CaseResult SendToStl(Case currentCase)
        {
            Case.CaseResult result;
            IEnumerable<SP_SET_SEND_TO_STL_Result> temp;

            temp = globalModel.SP_SET_SEND_TO_STL(
                      currentCase.CorpId,
                      currentCase.RegionId,
                      currentCase.CountryId,
                      currentCase.DomesticregId,
                      currentCase.StateProvId,
                      currentCase.CityId,
                      currentCase.OfficeId,
                      currentCase.CaseSeqNo,
                      currentCase.HistSeqNo,
                      currentCase.UserId
                  );

            result = temp
                        .Select(c => new Case.CaseResult
                        {
                            Result = c.Result.ConvertToNoNullable(),
                            ResultMessage = c.ResultMessage
                        })
                        .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Case.Comment> GetAllComments(Case currentCase)
        {
            IEnumerable<Case.Comment> result;
            IEnumerable<SP_GET_PL_PCY_COMMENTS_Result> temp;

            temp = globalModel.SP_GET_PL_PCY_COMMENTS(
                        currentCase.CorpId,
                        currentCase.RegionId,
                        currentCase.CountryId,
                        currentCase.DomesticregId,
                        currentCase.StateProvId,
                        currentCase.CityId,
                        currentCase.OfficeId,
                        currentCase.CaseSeqNo,
                        currentCase.HistSeqNo
                    );

            result = temp
                        .Select(c => new Case.Comment
                        {
                            CorpId = c.Corp_Id,
                            RegionId = c.Region_Id,
                            CountryId = c.Country_Id,
                            DomesticregId = c.Domesticreg_Id,
                            StateProvId = c.State_Prov_Id,
                            CityId = c.City_Id,
                            OfficeId = c.Office_Id,
                            CaseSeqNo = c.Case_Seq_No,
                            HistSeqNo = c.Hist_Seq_No,
                            CommentTypeId = c.Comment_Type_Id,
                            CommentId = c.Comment_Id,
                            CommentDate = c.Comment_Date,
                            Comments = c.Comments,
                            UserName = c.UserName,
                            UserId = c.UserId
                        })
                        .ToArray();

            return
                result;
        }
        
        public virtual int SetComment(Case.Comment comment)
        {
            int result;
            IEnumerable<SP_SET_PL_PCY_COMMENTS_Result> temp;

            result = -1;

            temp = globalModel.SP_SET_PL_PCY_COMMENTS(
                        comment.CorpId,
                        comment.RegionId,
                        comment.CountryId,
                        comment.DomesticregId,
                        comment.StateProvId,
                        comment.CityId,
                        comment.OfficeId,
                        comment.CaseSeqNo,
                        comment.HistSeqNo,
                        comment.CommentTypeId,
                        comment.CommentId,
                        comment.StepTypeId,
                        comment.StepId,
                        comment.StepCaseNo,
                        comment.CommentDate,
                        comment.Comments,
                        comment.UserId
                    );

            return
                result;
        }

        public virtual Case.AssignCase SetAssignCase(Case.AssignCase paramter)
        {
            Case.AssignCase result;
            IEnumerable<SP_SET_PL_PCY_ASSIGN_CASE_Result> temp;

            temp = globalModel.SP_SET_PL_PCY_ASSIGN_CASE(
                      paramter.CorpId,
                      paramter.RegionId,
                      paramter.CountryId,
                      paramter.DomesticRegId,
                      paramter.StateProvId,
                      paramter.CityId,
                      paramter.OfficeId,
                      paramter.CaseSeqNo,
                      paramter.HistSeqNo,
                      paramter.UnderwriterId,
                      paramter.AssignCaseId,
                      paramter.AssignRol,
                      paramter.UserId
                  );

            result = temp
                        .Select(ac => new Case.AssignCase
                        {
                            CorpId = ac.Corp_Id.ConvertToNoNullable(),
                            RegionId = ac.Region_Id.ConvertToNoNullable(),
                            CountryId = ac.Country_Id.ConvertToNoNullable(),
                            DomesticRegId = ac.Domesticreg_Id.ConvertToNoNullable(),
                            StateProvId = ac.State_Prov_Id.ConvertToNoNullable(),
                            CityId = ac.City_Id.ConvertToNoNullable(),
                            OfficeId = ac.Office_Id.ConvertToNoNullable(),
                            CaseSeqNo = ac.Case_Seq_No.ConvertToNoNullable(),
                            HistSeqNo = ac.Hist_Seq_No.ConvertToNoNullable(),
                            UnderwriterId = ac.Underwriter_Id.ConvertToNoNullable(),
                            AssignCaseId = ac.Assign_Case_Id
                        })
                        .FirstOrDefault();

            return
                result;
        }
    }
}
