﻿using System;
using System.Collections.Generic;
using Entity.UnderWriting.Entities;
using System.Data;

namespace DI.UnderWriting.Interfaces
{
    public interface IPolicy
    {
        Policy.PlanData GetPlanData(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo);

        int AddContactToPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo
            , int? contactId, int contactTypeId, int contactRoleTypeId, int agentId, int userId);

        IEnumerable<Policy.Profile> GetProfilePersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int profileTypeId);

        IEnumerable<Policy.Profile> GetProfileNoPersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int profileTypeId);

        IEnumerable<Policy.PolicySummaryByPolicy> GetSummaryByPolicy(int coprId, int regionId, int countryId
            , int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo);

        IEnumerable<Policy.PolicySummaryByContact> GetSummaryByContact(int contactId);

        IEnumerable<Policy.RequirementSummary> GetRequirementSummary(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? requirementCatId, int? requirementTypeId);

        IEnumerable<Policy.PaymentSummary> GetPaymentSummary(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId
            , int? officeId, int? caseSeqNo, int? histSeqNo, int OwnerContactId);

        IEnumerable<Policy.AssignCase> GetPolicyAssingCase(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo);

        int DeletePolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int userId);

        int UpdatePolicy(Policy policy);

        int UpdatePaymentFrequency(Policy.PaymentFrequency paymentFreq);

        int InsertPaymentFrequency(Policy.PaymentFrequency paymentFreq);

        int DeleteContactRole(int corpId, int regionId, int countryId, int domesticregId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int contactRoleTypeId, int userId);

        IEnumerable<Policy.CategoryDocument> GetCategoryDocument(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? docTypeId, int? docCategoryId, int languageId);

        IEnumerable<Policy.AgentChainDetail> GetAgentChainDetail(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo);

        IEnumerable<Policy.PolicyCommunication> GetPolicyCommunication(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? callNoteId);

        int RecentViewed(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int underwriterId);

        int DeleteContactAndRole(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int contactId, int contactRoleTypeId, int userId);

        IEnumerable<Policy.PolicyCommentSummary> GetCommentSummary(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId
            , int? officeId, int? caseSeqNo, int? histSeqNo);

        IEnumerable<Policy.UnderwritingCallComment> GetUnderwritingCallComments(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo);

        int DeletePaymentFrequency(Policy.PaymentFrequency paymentFreq);

        Policy GetPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo);

        IEnumerable<Policy.UnderwritingCallTemplate> GetUnderwritingCallTemplate(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int contactId, int languageId);

        IEnumerable<Policy.UnderwritingCallTemplate> GetUnderwritingCallTemplateByCategory(int coprId, int categoryId, int languageId);

        Policy.UnderwritingCallTemplate GetUnderwritingCallTemplateByCategoryFirst(int coprId, int categoryId, int languageId);

        int SetUnderwritingCallTabAnswer(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int tabId, bool answer, int userId);

        int SetUnderwritingCallSecurityQuestion(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int questionId, int contactId, bool answer, int userId);

        int UpdateUnderwritingCallComment(Case.Comment comment);

        IEnumerable<Policy.ProductByContact> GetProductByContactAndRole(int? contactTypeId, int contactId, int languageId);

        Policy.Call InsertCall(Policy.Call call);

        Policy.Call UpdateCall(Policy.Call call);

        int SetValidTab(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int projectId, int tabId, bool isValid, int userId);

        IEnumerable<Policy.Tab> GetTabValidation(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
         , int officeId, int caseSeqNo, int histSeqNo);

        void SetValidTabRequirementForNewBusiness(Policy.Parameter policy);

        IEnumerable<Policy.Contact> GetContactPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? contactRoleTypeId);

        IEnumerable<Policy.Contact> GetPolicyAddInsured(Policy.Parameter policy);

        int UpdateActivitiesFinancial(Policy.Contact contact);

        int UpdatePersonalInfoContact(Contact contact);

        int SetContactPolicyInfo(Policy.Contact contact);

        bool? GetCheckPolicyActive(string PolicyNo);

        string GetNewCotizacionNumber(int countryId, string productCode);

        string getIsValidFacultativeID(int Case_seq_No, string Facultative_Reinsurance_Id);

        #region InvestmentProfile
        IEnumerable<Policy.InvestProfilePersonalized> GetInvestProfilePersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int profileTypeId);

        int SetInvestProfilePersonalized(Policy.InvestProfilePersonalized investProfilePersonalized);
        int SetInvestProfilePersonalized(IEnumerable<Policy.InvestProfilePersonalized> investProfilePersonalizedList);

        int InsertInvestmentProfile(Policy.InvestProfile investProfile);
        int UpdateInvestmentProfile(Policy.InvestProfile investProfile);

        [ObsoleteAttribute("This method is deprecated.")]
        int DeleteInvestmentProfile(Policy.InvestProfile investProfile);
        #endregion

        #region Rating
        IEnumerable<Policy.OverPricePercentage> GetOverPricePercentage(Policy.RiskRatingCondition condition);

        int InsertRiskRating(IEnumerable<Policy.RiskRating> riskRatingList);
        int UpdateRiskRating(IEnumerable<Policy.RiskRating> riskRatingList);

        IEnumerable<Policy.RiskRating> GetRiskRating(Policy.RiskRating riskRating);
        Policy.RiskRating TerminateExclusion(Policy.RiskRating riskRating);

        IEnumerable<Policy.RiskRating> GetRiskRatingSelection(Policy.RiskRating riskRating);

        int DeleteRiskRating(Policy.RiskRating riskRating);
        #endregion

        #region SendToReinsurance
        IEnumerable<Reinsurance.Communication> GetReinsuranceCommunication(Reinsurance.Communication comm);
        IEnumerable<Reinsurance.Communication> GetReinsuranceCommunicationHtmlAndAttachments(Reinsurance.Communication comm);
        IEnumerable<Reinsurance.FacultativeStatus> GetReinsuranceFacultativeStatus();
        IEnumerable<Reinsurance.FacultativeDetails> GetReinsuranceFacultativeDetails(Reinsurance.FacultativeDetails facultativeDets);
        IEnumerable<Reinsurance.RatingRisk> GetRatingRisk(string ratingTable);
        Reinsurance.Communication InsertReinsuranceCommunication(Reinsurance.Communication comm);
        Reinsurance.Communication UpdateReinsuranceCommunication(Reinsurance.Communication comm);
        Reinsurance.StepAvailable GetStepAvailable(Reinsurance.StepAvailable step);
        Reinsurance.StepAvailable GetStepAvailable(string stepSeqReference);
        Reinsurance.Communication SetReinsuranceCommunicationAttachment(IEnumerable<Reinsurance.Communication> comms);
        Reinsurance.Communication SetReinsuranceCommunicationAttachment(Reinsurance.Communication comm);
        decimal GetDocumentSize(int docTypeId, int docCategoryId, int documentId);
        int SetDocument(int docTypeId, int docCategoryId, int documentId, byte[] documentBinary, string documentName, DateTime creationDate, DateTime? expireDate, int userId);
        #endregion

        IEnumerable<Policy.Form> GetFormPolicyContact(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int contactId, int formId, int languageId);

        Policy.BackgroundCheck InsertBackgroundCheck(Policy.BackgroundCheck backgroundCheck);
        Policy.BackgroundCheck UpdateBackgroundCheck(Policy.BackgroundCheck backgroundCheck);
        void SetBackgroundCheckAndCloseStep(Policy.BackgroundCheck backgroundCheck);

        Requirement.Document GetIdCopyRequirement(Requirement requirement);

        bool CheckPolicyMedical(Policy.Parameter parameter);

        int GenerateTempPolicyNo(Policy.Parameter parameter);

        IEnumerable<Policy.Contact.Action> GetPolicyAction(Policy.Parameter parameter);

        int ChangePolicyChain(Policy.Parameter parameter);

        IEnumerable<Policy.StatusChange> SetPolicyStatus(Policy.Parameter parameter);

        Policy NewPolicyWithoutAgent(Case.NewCase newCase);

        IEnumerable<Policy.VehiclesCoverage> GetVehiclesCoverage(Policy.Parameter policyParameter);

        IEnumerable<Policy.CRBS> GetCRBS(Policy.CRBSParameter parameters);

        #region Discount
        IEnumerable<Policy.Vehicle.Discount> GetPolicyVehicleDiscount(Policy.DVParameter parameter);
        IEnumerable<Policy.Vehicle.Discount.RulesAndDetails> GetDiscountRulesAndDetails(Policy.DParameter parameter);

        Policy.Vehicle.Discount SetPolicyVehicleDiscount(Policy.Vehicle.Discount parameter);
        #endregion

        IEnumerable<Policy.Agent.SaleChannelInfo> GetAgentSaleChannelInfo(Policy.Parameter parameter);

        IEnumerable<Policy.VehicleInsured> GetVehicleInsured(Policy.Parameter parameter);
        IEnumerable<Policy.VehicleCoverage> GetVehicleCoverage(Policy.VehicleCoverageGet parameter);
        Policy.VehicleCoverage SetVehicleCoverage(Policy.VehicleCoverage vehicleCoverage);
        Policy.VehicleInsured SetVehicleInsured(Policy.VehicleInsured vehicleInsured);

        IEnumerable<Policy.VehicleCoverageSurcharge> GetVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter);
        Policy.VehicleCoverageSurcharge SetVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter);

        IEnumerable<Policy.Document> GetPolicyDocument(Policy.Document parameter);
        Policy.Document SetPolicyDocument(Policy.Document parameter);

        int SetPolicyNo(Policy.Number parameter);

        int SetPaymentFrequency(Policy.PaymentFrequency paymentFreq);
        int SetPolicy(Policy policy);

        IEnumerable<Policy.VehicleInsured.CoverageTypePremiun> GetVehicleInsuredCoverageTypePremiun(Policy.VehicleInsured.CoverageTypePremiun.Key parameter);
        Policy.VehicleInsured.CoverageTypePremiun.Key SetVehicleInsuredCoverageTypePremiun(Policy.VehicleInsured.CoverageTypePremiun parameter);

        IEnumerable<Policy.VehicleInsured.Discount> GetVehicleInsuredDiscount(Policy.VehicleInsured.Discount.Key parameter);
        Policy.VehicleInsured.Discount.Key SetVehicleInsuredDiscount(Policy.VehicleInsured.Discount parameter);

        IEnumerable<Policy.DocumentQuotation> GetQuotationDocumentReview(Policy.Parameter parameter);

        int SetVehicleInsuredInspection(Policy.VehicleInsured.InspectionV parameter);

        int SetVehicleInsuredApplyDiscountAndSurcharge(Policy.Parameter parameter);

        int DeleteVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter);

        int UpdateVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter);

        IEnumerable<Policy.Vehicle.Requirement> GetVehicleRequirement(Policy.Parameter parameter);

        int SetAssingQuotation(Policy.Parameter parameter);

        int UpdateQuotationInfoTemp(Policy.Quo.Temp parameter);
        Policy.Quo.Temp.TempResult UpdateOneQuotationInfoTemp(Policy.Quo.Temp parameter);


        IEnumerable<Policy.Quo> GetQuotationInfoTemp(Policy.Quo.Temp parameter);

        int SetPolicySourceId(Policy.PSourceId parameter);

        IEnumerable<Policy.Agent> GetAgentIdListByAgentId(Policy.Agent parameter);

        IEnumerable<Policy.LogResult> InsertLog(Policy.LogParameter parameter);

        int UpdatePolicyEffectiveDate(Policy.OEffectiveDate parameter);
        int UpdatePolicyExpirationDate(Policy.OExpirationDate parameter);

        Policy.UQuo UpdatePolicyQuo(Policy.UQuo parameter);

        IEnumerable<Policy.ConditionForSysflexIL> GetConditionIL(string QuotationNumber, long EntityId);

        IEnumerable<Policy.Facultative.Contract> GetFacultativeContract(Policy.Facultative.Key parameters);
        DataTable GetFacultativeContractCoverage(Policy.Facultative.Key parameters);
        DataTable SetFacultativeContractCoverage(Policy.Facultative.SetContractCoverage parameters);
        int DeleteFacultativeContract(int corpId, long contractUniqueId, int userId);
        IEnumerable<Policy.Facultative.Contract2> GetFacultativeContractCoverage2(Policy.Facultative.Key parameters);


        //Add By Jheiron 29-05-2017
        IEnumerable<Policy.Agent.AgentSupervisor> GetAgentSupervisor(int corpId, int agentID);
        IEnumerable<Policy.PolicyContact> GetPolicyByContact(int? ContactId);

        int SetReninsuranceFacultative(Policy.ReinsuranceFacultative parameter);

        IEnumerable<Policy.QuoGrid> GetAllCustomerPlanDetailQuo(Policy.QuoGrid.Key parameter);
        IEnumerable<Policy.QuoGrid.Counter> GetAllCustomerPlanDetailCountQuo(Policy.QuoGrid.Key parameter);
        IEnumerable<Policy.QuoGrid> GetAllPuntoVentaQuo(Policy.QuoGrid.Key parameter);

        int SetVehicleInsuredInspectionStatus(Policy.VehicleInsured.InspectionV parameter);

        int SetVehicleInsuredInspectionAddress(Policy.VehicleInsured.InspectionV parameter);

        IEnumerable<Policy.TabRol> GetAllTabsByRole(int UsrId);

        IEnumerable<Policy.QuoGrid.AgentChain> GetAgentChain(Policy.QuoGrid.AgentChain parameter);
        DataTable GetAgentChain(int agentId);
        int BlackListMember(Policy.BlackListMember.Parameter parameter);
        int SetBlakList(Policy.BlackList.Parameter parameter);

        IEnumerable<Policy.BackGroundCheckLink> Bg_Get_Match_Links(Policy.BackGroundCheckLink param);

        int Bg_Set_Match_Links(Policy.BackGroundCheckLink param);

        Policy.AgentInfo GetAgentInfo(int? CorpId, int? AgentId);
        IEnumerable<Policy.AgentInfo.Directory> GetAgentDirectoryInfo(int? DirectoryId);
        Policy.AgentInfo.Agent SetAgent(Policy.AgentInfo.Agent.parameter parameter);
        int SetAgentUniqueID(int? CorpId, int? AgentId, string UniqueId);
        int? SetPolicyLoanNo(int? corp_Id, int? region_Id, int? country_Id, int? domesticreg_Id, int? state_Prov_Id, int? city_Id, int? office_Id, int? case_Seq_No, int? hist_Seq_No, string loanPetitionNo, int? userId);
        int? SetPolicyDirectDebit(int? corp_Id, int? region_Id, int? country_Id, int? domesticreg_Id, int? state_Prov_Id, int? city_Id, int? office_Id, int? case_Seq_No, int? hist_Seq_No, bool? DirectDebit, bool? includeInitial, int? userId);
        decimal? GetQuotFromSysFlex(string PolicyNo);

        Policy.CouponInfo GetCouponInfo(string policyNo);

        IEnumerable<Policy.Vehicle.AccidentRate> GetTbAccidentRateByMakeAndModel(string Make,string Model);
        int SetCustomerSing(Nullable<int> corpId, Nullable<int> regionId, Nullable<int> countryId, Nullable<int> domesticRegId, Nullable<int> stateProvId, Nullable<int> cityId, Nullable<int> officeId, Nullable<int> caseSeqNo, Nullable<int> histSeqNo, string dataSign);
        string GetCustomerSing(Nullable<int> corpId, Nullable<int> regionId, Nullable<int> countryId, Nullable<int> domesticRegId, Nullable<int> stateProvId, Nullable<int> cityId, Nullable<int> officeId, Nullable<int> caseSeqNo, Nullable<int> histSeqNo);
    }
}