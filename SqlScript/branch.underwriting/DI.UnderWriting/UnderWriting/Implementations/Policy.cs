﻿using System;
using System.Collections.Generic;
using DI.UnderWriting.Interfaces;
using Entity.UnderWriting.Entities;
using LOGIC.UnderWriting.Global;
using System.Data;

namespace DI.UnderWriting.Implementations
{
    public class PolicyBll : IPolicy
    {
        private PolicyManager _policyManager;

        public PolicyBll()
        {
            _policyManager = new PolicyManager();
        }

        Policy.PlanData IPolicy.GetPlanData(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            return
                _policyManager.GetPlanData(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);
        }

        int IPolicy.AddContactToPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? contactId, int contactTypeId, int contactRoleTypeId, int agentId, int userId)
        {
            return
                _policyManager.AddContactToPolicy(
                     coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                    , contactId, contactTypeId, contactRoleTypeId, agentId, userId
               );
        }

        IEnumerable<Policy.Profile> IPolicy.GetProfilePersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int profileTypeId)
        {
            return
                _policyManager.GetProfilePersonalized(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, profileTypeId);
        }

        int IPolicy.SetReninsuranceFacultative(Policy.ReinsuranceFacultative parameter)
        {
            return
                _policyManager.SetReninsuranceFacultative(parameter);
        }

        IEnumerable<Policy.Profile> IPolicy.GetProfileNoPersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int profileTypeId)
        {
            return
                _policyManager.GetProfileNoPersonalized(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, profileTypeId);
        }

        IEnumerable<Policy.PolicySummaryByPolicy> IPolicy.GetSummaryByPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            return
                _policyManager.GetSummaryByPolicy(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);
        }

        IEnumerable<Policy.PolicySummaryByContact> IPolicy.GetSummaryByContact(int contactId)
        {
            return
                  _policyManager.GetSummaryByContact(contactId);
        }

        IEnumerable<Policy.RequirementSummary> IPolicy.GetRequirementSummary(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo
            , int? contactId, int? requirementCatId, int? requirementTypeId)
        {
            return
                  _policyManager.GetRequirementSummary(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, requirementCatId, requirementTypeId);
        }

        IEnumerable<Policy.PaymentSummary> IPolicy.GetPaymentSummary(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int OwnerContactId)
        {
            return
                 _policyManager.GetPaymentSummary(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, OwnerContactId);
        }

        public virtual IEnumerable<Policy.AssignCase> GetPolicyAssingCase(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo)
        {
            return _policyManager.GetPolicyAssingCase(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);
        }

        int IPolicy.DeletePolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int userId)
        {
            return
                _policyManager.DeletePolicy(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, userId);
        }

        int IPolicy.UpdatePolicy(Policy policy)
        {
            return
               _policyManager.UpdatePolicy(policy);
        }

        int IPolicy.UpdatePaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            return
               _policyManager.UpdatePaymentFrequency(paymentFreq);
        }

        int IPolicy.InsertPaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            return
               _policyManager.InsertPaymentFrequency(paymentFreq);
        }

        int IPolicy.DeleteContactRole(int corpId, int regionId, int countryId, int domesticregId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? contactId, int contactRoleTypeId, int userId)
        {
            return
                 _policyManager.DeleteContactRole(
                    corpId,
                    regionId,
                    countryId,
                    domesticregId,
                    stateProvId,
                    cityId,
                    officeId,
                    caseSeqNo,
                    histSeqNo,
                    contactId,
                    contactRoleTypeId,
                    userId
                );
        }

        IEnumerable<Policy.CategoryDocument> IPolicy.GetCategoryDocument(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? docTypeId, int? docCategoryId, int languageId)
        {
            return
                _policyManager.GetCategoryDocument(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, docTypeId, docCategoryId, languageId);
        }

        IEnumerable<Policy.AgentChainDetail> IPolicy.GetAgentChainDetail(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            return
                _policyManager.GetAgentChainDetail(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);
        }

        IEnumerable<Policy.PolicyCommunication> IPolicy.GetPolicyCommunication(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? callNoteId)
        {
            return
                 _policyManager.GetPolicyCommunication(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, callNoteId);
        }

        int IPolicy.RecentViewed(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int underwriterId)
        {
            return
                _policyManager.RecentViewed(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, underwriterId);
        }

        /// <summary>
        /// Delete a contact and all its dependencies, including its role in the policy.
        /// </summary>
        /// <param name="coprId">Corporation Id</param>
        /// <param name="regionId">Region Id</param>
        /// <param name="countryId">Country Id</param>
        /// <param name="domesticRegId">Domestric Region Id</param>
        /// <param name="stateProvId">State Province Id</param>
        /// <param name="cityId">City Id</param>
        /// <param name="officeId">Office Id</param>
        /// <param name="caseSeqNo">Case Sequence Number</param>
        /// <param name="histSeqNo">History Sequece Number</param>
        /// <param name="contactId">Contact Id</param>
        /// <param name="contactRoleTypeId">Contact Role Type Id</param>
        /// <param name="userId">User Id</param>
        /// <returns>int</returns>
        int IPolicy.DeleteContactAndRole(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int contactId, int contactRoleTypeId, int userId)
        {
            return
                _policyManager.DeleteContactAndRole(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , contactId, contactRoleTypeId, userId);
        }

        IEnumerable<Policy.PolicyCommentSummary> IPolicy.GetCommentSummary(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo)
        {
            return
                _policyManager.GetCommentSummary(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);
        }

        IEnumerable<Policy.UnderwritingCallComment> IPolicy.GetUnderwritingCallComments(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo)
        {
            return
                _policyManager.GetUnderwritingCallComments(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, stepTypeId, stepId, stepCaseNo);
        }

        int IPolicy.DeletePaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            return
                _policyManager.DeletePaymentFrequency(paymentFreq);
        }

        Policy IPolicy.GetPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            return
                _policyManager.GetPolicy(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);
        }

        IEnumerable<Policy.UnderwritingCallTemplate> IPolicy.GetUnderwritingCallTemplate(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int contactId, int languageId)
        {
            return
                 _policyManager.GetUnderwritingCallTemplate(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, languageId);
        }

        int IPolicy.SetUnderwritingCallTabAnswer(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int tabId, bool answer, int userId)
        {
            return
                _policyManager.SetUnderwritingCallTabAnswer(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , stepTypeId, stepId, stepCaseNo, tabId, answer, userId);
        }

        int IPolicy.SetUnderwritingCallSecurityQuestion(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int questionId, int contactId, bool answer, int userId)
        {
            return
                 _policyManager.SetUnderwritingCallSecurityQuestion(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                 , stepTypeId, stepId, stepCaseNo, questionId, contactId, answer, userId);
        }

        int IPolicy.UpdateUnderwritingCallComment(Case.Comment comment)
        {
            return
                _policyManager.UpdateUnderwritingCallComment(comment);
        }

        IEnumerable<Policy.ProductByContact> IPolicy.GetProductByContactAndRole(int? contactTypeId, int contactId, int languageId)
        {
            return
                _policyManager.GetProductByContactAndRole(contactTypeId, contactId, languageId);
        }

        Policy.Call IPolicy.InsertCall(Policy.Call call)
        {
            return
                _policyManager.InsertCall(call);
        }

        Policy.Call IPolicy.UpdateCall(Policy.Call call)
        {
            return
                _policyManager.UpdateCall(call);
        }

        int IPolicy.SetValidTab(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int projectId, int tabId, bool isValid, int userId)
        {
            return
               _policyManager.SetValidTab(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
               , projectId, tabId, isValid, userId);
        }

        IEnumerable<Policy.Tab> IPolicy.GetTabValidation(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            return
               _policyManager.GetTabValidation(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);
        }

        void IPolicy.SetValidTabRequirementForNewBusiness(Policy.Parameter policy)
        {
            _policyManager.SetValidTabRequirementForNewBusiness(policy);
        }

        IEnumerable<Policy.Contact> IPolicy.GetContactPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? contactRoleTypeId)
        {
            return
              _policyManager.GetContactPolicy(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, contactRoleTypeId);
        }

        int IPolicy.UpdateActivitiesFinancial(Policy.Contact contact)
        {
            return
                _policyManager.UpdateActivitiesFinancial(contact);
        }

        int IPolicy.UpdatePersonalInfoContact(Contact contact)
        {
            return
                _policyManager.UpdatePersonalInfoContact(contact);
        }

        #region InvestmentProfile
        IEnumerable<Policy.InvestProfilePersonalized> IPolicy.GetInvestProfilePersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int profileTypeId)
        {
            return
                _policyManager.GetInvestProfilePersonalized(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, profileTypeId);
        }

        int IPolicy.SetInvestProfilePersonalized(Policy.InvestProfilePersonalized investProfilePersonalized)
        {
            return
                _policyManager.SetInvestProfilePersonalized(investProfilePersonalized);
        }

        int IPolicy.SetInvestProfilePersonalized(IEnumerable<Policy.InvestProfilePersonalized> investProfilePersonalizedList)
        {
            return
                _policyManager.SetInvestProfilePersonalized(investProfilePersonalizedList);
        }

        int IPolicy.InsertInvestmentProfile(Policy.InvestProfile investProfile)
        {
            return
                 _policyManager.InsertInvestmentProfile(investProfile);
        }

        int IPolicy.UpdateInvestmentProfile(Policy.InvestProfile investProfile)
        {
            return
                _policyManager.UpdateInvestmentProfile(investProfile);
        }

        int IPolicy.DeleteInvestmentProfile(Policy.InvestProfile investProfile)
        {
            return
                _policyManager.DeleteInvestmentProfile(investProfile);
        }
        #endregion

        #region Rating
        IEnumerable<Policy.OverPricePercentage> IPolicy.GetOverPricePercentage(Policy.RiskRatingCondition condition)
        {
            return
                _policyManager.GetOverPricePercentage(condition);
        }
        int IPolicy.InsertRiskRating(IEnumerable<Policy.RiskRating> riskRatingList)
        {
            return
                _policyManager.InsertRiskRating(riskRatingList);
        }

        IEnumerable<Policy.RiskRating> IPolicy.GetRiskRating(Policy.RiskRating riskRating)
        {
            return
                _policyManager.GetRiskRating(riskRating);
        }

        Policy.RiskRating IPolicy.TerminateExclusion(Policy.RiskRating riskRating)
        {
            return
                _policyManager.TerminateExclusion(riskRating);
        }

        int IPolicy.UpdateRiskRating(IEnumerable<Policy.RiskRating> riskRatingList)
        {
            return
                   _policyManager.UpdateRiskRating(riskRatingList);
        }

        IEnumerable<Policy.RiskRating> IPolicy.GetRiskRatingSelection(Policy.RiskRating riskRating)
        {
            return
                _policyManager.GetRiskRatingSelection(riskRating);
        }

        int IPolicy.DeleteRiskRating(Policy.RiskRating riskRating)
        {
            return
                 _policyManager.DeleteRiskRating(riskRating);
        }
        #endregion

        #region SendToReinsurance
        IEnumerable<Reinsurance.Communication> IPolicy.GetReinsuranceCommunication(Reinsurance.Communication comm)
        {
            return
                _policyManager.GetReinsuranceCommunication(comm);
        }

        IEnumerable<Reinsurance.Communication> IPolicy.GetReinsuranceCommunicationHtmlAndAttachments(Reinsurance.Communication comm)
        {
            return
                _policyManager.GetReinsuranceCommunicationHtmlAndAttachments(comm);
        }

        Reinsurance.Communication IPolicy.InsertReinsuranceCommunication(Reinsurance.Communication comm)
        {
            return
                _policyManager.InsertReinsuranceCommunication(comm);
        }

        Reinsurance.Communication IPolicy.UpdateReinsuranceCommunication(Reinsurance.Communication comm)
        {
            return
                _policyManager.UpdateReinsuranceCommunication(comm);
        }

        Reinsurance.StepAvailable IPolicy.GetStepAvailable(Reinsurance.StepAvailable step)
        {
            return
                _policyManager.GetStepAvailable(step);
        }

        Reinsurance.StepAvailable IPolicy.GetStepAvailable(string stepSeqReference)
        {
            return
                _policyManager.GetStepAvailable(stepSeqReference);
        }

        Reinsurance.Communication IPolicy.SetReinsuranceCommunicationAttachment(IEnumerable<Reinsurance.Communication> comms)
        {
            return
                _policyManager.SetReinsuranceCommunicationAttachment(comms);
        }

        Reinsurance.Communication IPolicy.SetReinsuranceCommunicationAttachment(Reinsurance.Communication comm)
        {
            return
                 _policyManager.SetReinsuranceCommunicationAttachment(comm);
        }

        decimal IPolicy.GetDocumentSize(int docTypeId, int docCategoryId, int documentId)
        {
            return
                _policyManager.GetDocumentSize(docTypeId, docCategoryId, documentId);
        }

        int IPolicy.SetDocument(int docTypeId, int docCategoryId, int documentId, byte[] documentBinary, string documentName, DateTime creationDate, DateTime? expireDate, int userId)
        {
            return
                _policyManager.SetDocument(docTypeId, docCategoryId, documentId, documentBinary, documentName, creationDate, expireDate, userId);
        }
        #endregion

        IEnumerable<Policy.Contact> IPolicy.GetPolicyAddInsured(Policy.Parameter policy)
        {
            return
                _policyManager.GetPolicyAddInsured(policy);
        }

        IEnumerable<Policy.UnderwritingCallTemplate> IPolicy.GetUnderwritingCallTemplateByCategory(int coprId, int categoryId, int languageId)
        {
            return
                _policyManager.GetUnderwritingCallTemplateByCategory(coprId, categoryId, languageId);
        }

        Policy.UnderwritingCallTemplate IPolicy.GetUnderwritingCallTemplateByCategoryFirst(int coprId, int categoryId, int languageId)
        {
            return
                _policyManager.GetUnderwritingCallTemplateByCategoryFirst(coprId, categoryId, languageId);
        }

        IEnumerable<Policy.Form> IPolicy.GetFormPolicyContact(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int contactId, int formId, int languageId)
        {
            return
                 _policyManager.GetFormPolicyContact(
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
                    formId,
                    languageId);
        }

        Policy.BackgroundCheck IPolicy.InsertBackgroundCheck(Policy.BackgroundCheck backgroundCheck)
        {
            return
                _policyManager.InsertBackgroundCheck(backgroundCheck);
        }

        Policy.BackgroundCheck IPolicy.UpdateBackgroundCheck(Policy.BackgroundCheck backgroundCheck)
        {
            return
                _policyManager.UpdateBackgroundCheck(backgroundCheck);
        }

        Requirement.Document IPolicy.GetIdCopyRequirement(Requirement requirement)
        {
            return
                _policyManager.GetIdCopyRequirement(requirement);
        }

        bool IPolicy.CheckPolicyMedical(Policy.Parameter parameter)
        {
            return
                _policyManager.CheckPolicyMedical(parameter);
        }

        int IPolicy.GenerateTempPolicyNo(Policy.Parameter parameter)
        {
            return
                _policyManager.GenerateTempPolicyNo(parameter);
        }

        void IPolicy.SetBackgroundCheckAndCloseStep(Policy.BackgroundCheck backgroundCheck)
        {
            _policyManager.SetBackgroundCheckAndCloseStep(backgroundCheck);
        }

        IEnumerable<Policy.Contact.Action> IPolicy.GetPolicyAction(Policy.Parameter parameter)
        {
            return
                _policyManager.GetPolicyAction(parameter);
        }

        int IPolicy.ChangePolicyChain(Policy.Parameter parameter)
        {
            return
                _policyManager.ChangePolicyChain(parameter);
        }

        IEnumerable<Policy.StatusChange> IPolicy.SetPolicyStatus(Policy.Parameter parameter)
        {
            return
                _policyManager.SetPolicyStatus(parameter);
        }

        Policy IPolicy.NewPolicyWithoutAgent(Case.NewCase newCase)
        {
            return
                 _policyManager.NewPolicyWithoutAgent(newCase);
        }

        IEnumerable<Policy.VehiclesCoverage> IPolicy.GetVehiclesCoverage(Policy.Parameter policyParameter)
        {
            return
                _policyManager.GetVehiclesCoverage(policyParameter);
        }

        IEnumerable<Policy.CRBS> IPolicy.GetCRBS(Policy.CRBSParameter parameters)
        {
            return
                _policyManager.GetCRBS(parameters);
        }

        IEnumerable<Policy.Vehicle.Discount> IPolicy.GetPolicyVehicleDiscount(Policy.DVParameter parameter)
        {
            return
                _policyManager.GetPolicyVehicleDiscount(parameter);
        }

        IEnumerable<Policy.Vehicle.Discount.RulesAndDetails> IPolicy.GetDiscountRulesAndDetails(Policy.DParameter parameter)
        {
            return
                _policyManager.GetDiscountRulesAndDetails(parameter);
        }

        Policy.Vehicle.Discount IPolicy.SetPolicyVehicleDiscount(Policy.Vehicle.Discount parameter)
        {
            return
                _policyManager.SetPolicyVehicleDiscount(parameter);
        }

        IEnumerable<Policy.Agent.SaleChannelInfo> IPolicy.GetAgentSaleChannelInfo(Policy.Parameter parameter)
        {
            return
                _policyManager.GetAgentSaleChannelInfo(parameter);
        }

        IEnumerable<Policy.VehicleInsured> IPolicy.GetVehicleInsured(Policy.Parameter parameter)
        {
            return
                 _policyManager.GetVehicleInsured(parameter);
        }

        IEnumerable<Policy.VehicleCoverage> IPolicy.GetVehicleCoverage(Policy.VehicleCoverageGet parameter)
        {
            return
                  _policyManager.GetVehicleCoverage(parameter);
        }

        Policy.VehicleCoverage IPolicy.SetVehicleCoverage(Policy.VehicleCoverage vehicleCoverage)
        {
            return
                 _policyManager.SetVehicleCoverage(vehicleCoverage);
        }

        Policy.VehicleInsured IPolicy.SetVehicleInsured(Policy.VehicleInsured vehicleInsured)
        {
            return
                 _policyManager.SetVehicleInsured(vehicleInsured);
        }

        IEnumerable<Policy.VehicleCoverageSurcharge> IPolicy.GetVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            return
                 _policyManager.GetVehicleCoverageSurcharge(parameter);
        }

        Policy.VehicleCoverageSurcharge IPolicy.SetVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            return
                 _policyManager.SetVehicleCoverageSurcharge(parameter);
        }

        IEnumerable<Policy.Document> IPolicy.GetPolicyDocument(Policy.Document parameter)
        {
            return
                _policyManager.GetPolicyDocument(parameter);
        }

        Policy.Document IPolicy.SetPolicyDocument(Policy.Document parameter)
        {
            return
               _policyManager.SetPolicyDocument(parameter);
        }

        int IPolicy.SetPolicyNo(Policy.Number parameter)
        {
            return
                _policyManager.SetPolicyNo(parameter);
        }

        int IPolicy.SetPaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            return
                _policyManager.SetPaymentFrequency(paymentFreq);
        }

        int IPolicy.SetPolicy(Policy policy)
        {
            return
                _policyManager.SetPolicy(policy);
        }

        IEnumerable<Policy.VehicleInsured.CoverageTypePremiun> IPolicy.GetVehicleInsuredCoverageTypePremiun(Policy.VehicleInsured.CoverageTypePremiun.Key parameter)
        {
            return
                _policyManager.GetVehicleInsuredCoverageTypePremiun(parameter);
        }

        Policy.VehicleInsured.CoverageTypePremiun.Key IPolicy.SetVehicleInsuredCoverageTypePremiun(Policy.VehicleInsured.CoverageTypePremiun parameter)
        {
            return
                _policyManager.SetVehicleInsuredCoverageTypePremiun(parameter);
        }

        IEnumerable<Policy.VehicleInsured.Discount> IPolicy.GetVehicleInsuredDiscount(Policy.VehicleInsured.Discount.Key parameter)
        {
            return
                _policyManager.GetVehicleInsuredDiscount(parameter);
        }

        Policy.VehicleInsured.Discount.Key IPolicy.SetVehicleInsuredDiscount(Policy.VehicleInsured.Discount parameter)
        {
            return
                _policyManager.SetVehicleInsuredDiscount(parameter);
        }

        IEnumerable<Policy.DocumentQuotation> IPolicy.GetQuotationDocumentReview(Policy.Parameter parameter)
        {
            return
                _policyManager.GetQuotationDocumentReview(parameter);
        }

        int IPolicy.SetVehicleInsuredInspection(Policy.VehicleInsured.InspectionV parameter)
        {
            return
                _policyManager.SetVehicleInsuredInspection(parameter);
        }

        int IPolicy.SetVehicleInsuredApplyDiscountAndSurcharge(Policy.Parameter parameter)
        {
            return
                _policyManager.SetVehicleInsuredApplyDiscountAndSurcharge(parameter);
        }

        int IPolicy.DeleteVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            return
                _policyManager.DeleteVehicleCoverageSurcharge(parameter);
        }

        int IPolicy.UpdateVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            return
                _policyManager.UpdateVehicleCoverageSurcharge(parameter);
        }

        IEnumerable<Policy.Vehicle.Requirement> IPolicy.GetVehicleRequirement(Policy.Parameter parameter)
        {
            return
                _policyManager.GetVehicleRequirement(parameter);
        }

        int IPolicy.SetAssingQuotation(Policy.Parameter parameter)
        {
            return
                _policyManager.SetAssingQuotation(parameter);
        }

        IEnumerable<Policy.AssignCase> IPolicy.GetPolicyAssingCase(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            throw new NotImplementedException();
        }

        int IPolicy.UpdateQuotationInfoTemp(Policy.Quo.Temp parameter)
        {
            return
                _policyManager.UpdateQuotationInfoTemp(parameter);
        }

        Policy.Quo.Temp.TempResult IPolicy.UpdateOneQuotationInfoTemp(Policy.Quo.Temp parameter)
        {
            return
                _policyManager.UpdateOneQuotationInfoTemp(parameter);
        }

        IEnumerable<Policy.Quo> IPolicy.GetQuotationInfoTemp(Policy.Quo.Temp parameter)
        {
            return
                _policyManager.GetQuotationInfoTemp(parameter);
        }

        int IPolicy.SetPolicySourceId(Policy.PSourceId parameter)
        {
            return
                _policyManager.SetPolicySourceId(parameter);
        }

        IEnumerable<Policy.Agent> IPolicy.GetAgentIdListByAgentId(Policy.Agent parameter)
        {
            return
                _policyManager.GetAgentIdListByAgentId(parameter);
        }

        IEnumerable<Policy.LogResult> IPolicy.InsertLog(Policy.LogParameter parameter)
        {
            return
                _policyManager.InsertLog(parameter);
        }

        int IPolicy.UpdatePolicyEffectiveDate(Policy.OEffectiveDate parameter)
        {
            return
                _policyManager.UpdatePolicyEffectiveDate(parameter);
        }

        Policy.UQuo IPolicy.UpdatePolicyQuo(Policy.UQuo parameter)
        {
            return
                _policyManager.UpdatePolicyQuo(parameter);
        }


        public int SetContactPolicyInfo(Policy.Contact contact)
        {
            return _policyManager.SetContactPolicyInfo(contact);
        }

        public IEnumerable<Reinsurance.FacultativeDetails> GetReinsuranceFacultativeDetails(Reinsurance.FacultativeDetails facultativeDets)
        {
            return _policyManager.GetReinsuranceFacultativeDetails(facultativeDets);
        }

        public IEnumerable<Reinsurance.RatingRisk> GetRatingRisk(string ratingTable)
        {
            return _policyManager.GetRatingRisk(ratingTable);
        }

        public string GetNewCotizacionNumber(int countryId, string productCode)
        {
            return _policyManager.GetNewCotizacionNumber(countryId, productCode);
        }

        public IEnumerable<Reinsurance.FacultativeStatus> GetReinsuranceFacultativeStatus()
        {
            return _policyManager.GetReinsuranceFacultativeStatus();
        }


        //Bmarroquin 13-05-2017 se crea metodo 
        public string getIsValidFacultativeID(int Case_seq_No, string Facultative_Reinsurance_Id)
        {
            return _policyManager.getIsValidFacultativeID(Case_seq_No, Facultative_Reinsurance_Id);
        }

        IEnumerable<Policy.ConditionForSysflexIL> IPolicy.GetConditionIL(string QuotationNumber, long EntityId)
        {
            return
                _policyManager.GetConditionIL(QuotationNumber, EntityId);
        }


        int IPolicy.UpdatePolicyExpirationDate(Policy.OExpirationDate parameter)
        {
            return
                _policyManager.UpdatePolicyExpirationDate(parameter);
        }


        IEnumerable<Policy.Facultative.Contract> IPolicy.GetFacultativeContract(Policy.Facultative.Key parameters)
        {
            return
                _policyManager.GetFacultativeContract(parameters);
        }

        DataTable IPolicy.GetFacultativeContractCoverage(Policy.Facultative.Key parameters)
        {
            return
                _policyManager.GetFacultativeContractCoverage(parameters);
        }

        DataTable IPolicy.SetFacultativeContractCoverage(Policy.Facultative.SetContractCoverage parameters)
        {
            return
                _policyManager.SetFacultativeContractCoverage(parameters);
        }

        int IPolicy.DeleteFacultativeContract(int corpId, long contractUniqueId, int userId)
        {
            return
                _policyManager.DeleteFacultativeContract(corpId, contractUniqueId, userId);
        }


        IEnumerable<Policy.Facultative.Contract2> IPolicy.GetFacultativeContractCoverage2(Policy.Facultative.Key parameters)
        {
            return
                _policyManager.GetFacultativeContractCoverage2(parameters);
        }


        //Add By Jheiron 29-05-2017
        IEnumerable<Policy.Agent.AgentSupervisor> IPolicy.GetAgentSupervisor(int corpId, int agentID)
        {
            return
                _policyManager.GetAgentSupervisor(corpId, agentID);
        }


        bool? IPolicy.GetCheckPolicyActive(string PolicyNo)
        {
            return
                 _policyManager.GetCheckPolicyActive(PolicyNo);
        }


        IEnumerable<Policy.PolicyContact> IPolicy.GetPolicyByContact(int? ContactId)
        {
            return
                  _policyManager.GetPolicyByContact(ContactId);
        }


        int IPolicy.SetContactPolicyInfo(Policy.Contact contact)
        {
            return _policyManager.SetContactPolicyInfo(contact);
        }

        string IPolicy.GetNewCotizacionNumber(int countryId, string productCode)
        {
            return _policyManager.GetNewCotizacionNumber(countryId, productCode);
        }

        string IPolicy.getIsValidFacultativeID(int Case_seq_No, string Facultative_Reinsurance_Id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Reinsurance.FacultativeStatus> IPolicy.GetReinsuranceFacultativeStatus()
        {
            return _policyManager.GetReinsuranceFacultativeStatus();
        }

        IEnumerable<Reinsurance.FacultativeDetails> IPolicy.GetReinsuranceFacultativeDetails(Reinsurance.FacultativeDetails facultativeDets)
        {
            return _policyManager.GetReinsuranceFacultativeDetails(facultativeDets);
        }

        IEnumerable<Policy.QuoGrid> IPolicy.GetAllCustomerPlanDetailQuo(Policy.QuoGrid.Key parameter)
        {
            return
                _policyManager.GetAllCustomerPlanDetailQuo(parameter);
        }

        IEnumerable<Policy.QuoGrid> IPolicy.GetAllPuntoVentaQuo(Policy.QuoGrid.Key parameter)
        {
            return
                _policyManager.GetAllPuntoVentaQuo(parameter);
        }

        IEnumerable<Policy.QuoGrid.Counter> IPolicy.GetAllCustomerPlanDetailCountQuo(Policy.QuoGrid.Key parameter)
        {
            return
                _policyManager.GetAllCustomerPlanDetailCountQuo(parameter);
        }

        IEnumerable<Reinsurance.RatingRisk> IPolicy.GetRatingRisk(string ratingTable)
        {
            return _policyManager.GetRatingRisk(ratingTable);
        }

        int IPolicy.SetVehicleInsuredInspectionStatus(Policy.VehicleInsured.InspectionV parameter)
        {
            return
                _policyManager.SetVehicleInsuredInspectionStatus(parameter);
        }

        int IPolicy.SetVehicleInsuredInspectionAddress(Policy.VehicleInsured.InspectionV parameter)
        {
            return
                _policyManager.SetVehicleInsuredInspectionAddress(parameter);
        }

        IEnumerable<Policy.TabRol> IPolicy.GetAllTabsByRole(int UsrId)
        {
            return
                _policyManager.GetAllTabsByRole(UsrId);
        }

        IEnumerable<Policy.QuoGrid.AgentChain> IPolicy.GetAgentChain(Policy.QuoGrid.AgentChain parameter)
        {
            return
                _policyManager.GetAgentChain(parameter);
        }

        DataTable IPolicy.GetAgentChain(int agentId)
        {
            return
               _policyManager.GetAgentChain(agentId);
        }

        int IPolicy.BlackListMember(Policy.BlackListMember.Parameter parameter)
        {
            return
                  _policyManager.BlackListMember(parameter);
        }


        int IPolicy.SetBlakList(Policy.BlackList.Parameter parameter)
        {
            return
                  _policyManager.SetBlakList(parameter);
        }

        #region BackgroundCheck Underwriting
        public IEnumerable<Policy.BackGroundCheckLink> Bg_Get_Match_Links(Policy.BackGroundCheckLink param)
        {
            return
                _policyManager.Bg_Get_Match_Links(param);
        }

        public virtual int Bg_Set_Match_Links(Policy.BackGroundCheckLink param)
        {
            return
                _policyManager.Bg_Set_Match_Links(param);
        }

        #endregion


        IEnumerable<Policy.BackGroundCheckLink> IPolicy.Bg_Get_Match_Links(Policy.BackGroundCheckLink param)
        {
            throw new NotImplementedException();
        }

        int IPolicy.Bg_Set_Match_Links(Policy.BackGroundCheckLink param)
        {
            throw new NotImplementedException();
        }

        Policy.AgentInfo IPolicy.GetAgentInfo(int? CorpId, int? AgentId)
        {
            return
                 _policyManager.GetAgentInfo(CorpId, AgentId);
        }


        IEnumerable<Policy.AgentInfo.Directory> IPolicy.GetAgentDirectoryInfo(int? DirectoryId)
        {
            return
                _policyManager.GetAgentDirectoryInfo(DirectoryId);
        }


        Policy.AgentInfo.Agent IPolicy.SetAgent(Policy.AgentInfo.Agent.parameter parameter)
        {
            return
                  _policyManager.SetAgent(parameter);
        }


        int IPolicy.SetAgentUniqueID(int? CorpId, int? AgentId, string UniqueId)
        {
            return
                   _policyManager.SetAgentUniqueID(CorpId, AgentId, UniqueId);
        }


        int? IPolicy.SetPolicyLoanNo(int? corp_Id, int? region_Id, int? country_Id, int? domesticreg_Id, int? state_Prov_Id, int? city_Id, int? office_Id, int? case_Seq_No, int? hist_Seq_No, string loanPetitionNo, int? userId)
        {
            return
                    _policyManager.SetPolicyLoanNo(corp_Id, region_Id, country_Id, domesticreg_Id, state_Prov_Id, city_Id, office_Id, case_Seq_No, hist_Seq_No, loanPetitionNo, userId);
        }


        int? IPolicy.SetPolicyDirectDebit(int? corp_Id, int? region_Id, int? country_Id, int? domesticreg_Id, int? state_Prov_Id, int? city_Id, int? office_Id, int? case_Seq_No, int? hist_Seq_No, bool? DirectDebit, bool? includeInitial, int? userId)
        {
            return
                      _policyManager.SetPolicyDirectDebit(corp_Id, region_Id, country_Id, domesticreg_Id, state_Prov_Id, city_Id, office_Id, case_Seq_No, hist_Seq_No, DirectDebit, includeInitial, userId);
        }


        decimal? IPolicy.GetQuotFromSysFlex(string PolicyNo)
        {
            return
                  _policyManager.GetQuotFromSysFlex(PolicyNo);
        }

        Policy.CouponInfo IPolicy.GetCouponInfo(string policyNo)
        {
            return
                _policyManager.GetCouponInfo(policyNo);
        }

        IEnumerable<Policy.Vehicle.AccidentRate> IPolicy.GetTbAccidentRateByMakeAndModel(string Make, string Model)
        {
            return
                 _policyManager.GetTbAccidentRateByMakeAndModel(Make, Model);
        }

        int IPolicy.SetCustomerSing(Nullable<int> corpId, Nullable<int> regionId, Nullable<int> countryId, Nullable<int> domesticRegId, Nullable<int> stateProvId, Nullable<int> cityId, Nullable<int> officeId, Nullable<int> caseSeqNo, Nullable<int> histSeqNo, string dataSign)
        {
            return 
                _policyManager.SetCustomerSing(corpId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, dataSign);
        }

        string IPolicy.GetCustomerSing(Nullable<int> corpId, Nullable<int> regionId, Nullable<int> countryId, Nullable<int> domesticRegId, Nullable<int> stateProvId, Nullable<int> cityId, Nullable<int> officeId, Nullable<int> caseSeqNo, Nullable<int> histSeqNo){
            return
                _policyManager.GetCustomerSing(corpId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);
        }
    }

    public class PolicyWS : IPolicy
    {
        Policy.PlanData IPolicy.GetPlanData(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            throw new NotImplementedException();
        }

        int IPolicy.AddContactToPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? contactId, int contactTypeId, int contactRoleTypeId, int agentId, int userId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Profile> IPolicy.GetProfilePersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int profileTypeId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Profile> IPolicy.GetProfileNoPersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int profileTypeId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.PolicySummaryByPolicy> IPolicy.GetSummaryByPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.PolicySummaryByContact> IPolicy.GetSummaryByContact(int contactId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.RequirementSummary> IPolicy.GetRequirementSummary(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? requirementCatId, int? requirementTypeId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.PaymentSummary> IPolicy.GetPaymentSummary(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, int OwnerContactId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.AssignCase> IPolicy.GetPolicyAssingCase(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            throw new NotImplementedException();
        }

        int IPolicy.DeletePolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int userId)
        {
            throw new NotImplementedException();
        }

        int IPolicy.UpdatePolicy(Policy policy)
        {
            throw new NotImplementedException();
        }

        int IPolicy.UpdatePaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            throw new NotImplementedException();
        }

        int IPolicy.InsertPaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            throw new NotImplementedException();
        }

        int IPolicy.DeleteContactRole(int corpId, int regionId, int countryId, int domesticregId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? contactId, int contactRoleTypeId, int userId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.CategoryDocument> IPolicy.GetCategoryDocument(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? docTypeId, int? docCategoryId, int languageId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.AgentChainDetail> IPolicy.GetAgentChainDetail(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.PolicyCommunication> IPolicy.GetPolicyCommunication(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? callNoteId)
        {
            throw new NotImplementedException();
        }

        int IPolicy.RecentViewed(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int underwriterId)
        {
            throw new NotImplementedException();
        }

        int IPolicy.DeleteContactAndRole(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int contactId, int contactRoleTypeId, int userId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.PolicyCommentSummary> IPolicy.GetCommentSummary(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.UnderwritingCallComment> IPolicy.GetUnderwritingCallComments(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo)
        {
            throw new NotImplementedException();
        }

        int IPolicy.DeletePaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            throw new NotImplementedException();
        }

        Policy IPolicy.GetPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.UnderwritingCallTemplate> IPolicy.GetUnderwritingCallTemplate(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int contactId, int languageId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.UnderwritingCallTemplate> IPolicy.GetUnderwritingCallTemplateByCategory(int coprId, int categoryId, int languageId)
        {
            throw new NotImplementedException();
        }

        Policy.UnderwritingCallTemplate IPolicy.GetUnderwritingCallTemplateByCategoryFirst(int coprId, int categoryId, int languageId)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetUnderwritingCallTabAnswer(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int tabId, bool answer, int userId)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetUnderwritingCallSecurityQuestion(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int questionId, int contactId, bool answer, int userId)
        {
            throw new NotImplementedException();
        }

        int IPolicy.UpdateUnderwritingCallComment(Case.Comment comment)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.ProductByContact> IPolicy.GetProductByContactAndRole(int? contactTypeId, int contactId, int languageId)
        {
            throw new NotImplementedException();
        }

        Policy.Call IPolicy.InsertCall(Policy.Call call)
        {
            throw new NotImplementedException();
        }

        Policy.Call IPolicy.UpdateCall(Policy.Call call)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetValidTab(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int projectId, int tabId, bool isValid, int userId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Tab> IPolicy.GetTabValidation(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo)
        {
            throw new NotImplementedException();
        }

        void IPolicy.SetValidTabRequirementForNewBusiness(Policy.Parameter policy)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Contact> IPolicy.GetContactPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? contactRoleTypeId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Contact> IPolicy.GetPolicyAddInsured(Policy.Parameter policy)
        {
            throw new NotImplementedException();
        }

        int IPolicy.UpdateActivitiesFinancial(Policy.Contact contact)
        {
            throw new NotImplementedException();
        }

        int IPolicy.UpdatePersonalInfoContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetContactPolicyInfo(Policy.Contact contact)
        {
            throw new NotImplementedException();
        }

        bool? IPolicy.GetCheckPolicyActive(string PolicyNo)
        {
            throw new NotImplementedException();
        }

        string IPolicy.GetNewCotizacionNumber(int countryId, string productCode)
        {
            throw new NotImplementedException();
        }

        string IPolicy.getIsValidFacultativeID(int Case_seq_No, string Facultative_Reinsurance_Id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.InvestProfilePersonalized> IPolicy.GetInvestProfilePersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int profileTypeId)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetInvestProfilePersonalized(Policy.InvestProfilePersonalized investProfilePersonalized)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetInvestProfilePersonalized(IEnumerable<Policy.InvestProfilePersonalized> investProfilePersonalizedList)
        {
            throw new NotImplementedException();
        }

        int IPolicy.InsertInvestmentProfile(Policy.InvestProfile investProfile)
        {
            throw new NotImplementedException();
        }

        int IPolicy.UpdateInvestmentProfile(Policy.InvestProfile investProfile)
        {
            throw new NotImplementedException();
        }

        int IPolicy.DeleteInvestmentProfile(Policy.InvestProfile investProfile)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.OverPricePercentage> IPolicy.GetOverPricePercentage(Policy.RiskRatingCondition condition)
        {
            throw new NotImplementedException();
        }

        int IPolicy.InsertRiskRating(IEnumerable<Policy.RiskRating> riskRatingList)
        {
            throw new NotImplementedException();
        }

        int IPolicy.UpdateRiskRating(IEnumerable<Policy.RiskRating> riskRatingList)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.RiskRating> IPolicy.GetRiskRating(Policy.RiskRating riskRating)
        {
            throw new NotImplementedException();
        }

        Policy.RiskRating IPolicy.TerminateExclusion(Policy.RiskRating riskRating)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.RiskRating> IPolicy.GetRiskRatingSelection(Policy.RiskRating riskRating)
        {
            throw new NotImplementedException();
        }

        int IPolicy.DeleteRiskRating(Policy.RiskRating riskRating)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Reinsurance.Communication> IPolicy.GetReinsuranceCommunication(Reinsurance.Communication comm)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Reinsurance.Communication> IPolicy.GetReinsuranceCommunicationHtmlAndAttachments(Reinsurance.Communication comm)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Reinsurance.FacultativeStatus> IPolicy.GetReinsuranceFacultativeStatus()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Reinsurance.FacultativeDetails> IPolicy.GetReinsuranceFacultativeDetails(Reinsurance.FacultativeDetails facultativeDets)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Reinsurance.RatingRisk> IPolicy.GetRatingRisk(string ratingTable)
        {
            throw new NotImplementedException();
        }

        Reinsurance.Communication IPolicy.InsertReinsuranceCommunication(Reinsurance.Communication comm)
        {
            throw new NotImplementedException();
        }

        Reinsurance.Communication IPolicy.UpdateReinsuranceCommunication(Reinsurance.Communication comm)
        {
            throw new NotImplementedException();
        }

        Reinsurance.StepAvailable IPolicy.GetStepAvailable(Reinsurance.StepAvailable step)
        {
            throw new NotImplementedException();
        }

        Reinsurance.StepAvailable IPolicy.GetStepAvailable(string stepSeqReference)
        {
            throw new NotImplementedException();
        }

        Reinsurance.Communication IPolicy.SetReinsuranceCommunicationAttachment(IEnumerable<Reinsurance.Communication> comms)
        {
            throw new NotImplementedException();
        }

        Reinsurance.Communication IPolicy.SetReinsuranceCommunicationAttachment(Reinsurance.Communication comm)
        {
            throw new NotImplementedException();
        }

        decimal IPolicy.GetDocumentSize(int docTypeId, int docCategoryId, int documentId)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetDocument(int docTypeId, int docCategoryId, int documentId, byte[] documentBinary, string documentName, DateTime creationDate, DateTime? expireDate, int userId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Form> IPolicy.GetFormPolicyContact(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId, int officeId, int caseSeqNo, int histSeqNo, int contactId, int formId, int languageId)
        {
            throw new NotImplementedException();
        }

        Policy.BackgroundCheck IPolicy.InsertBackgroundCheck(Policy.BackgroundCheck backgroundCheck)
        {
            throw new NotImplementedException();
        }

        Policy.BackgroundCheck IPolicy.UpdateBackgroundCheck(Policy.BackgroundCheck backgroundCheck)
        {
            throw new NotImplementedException();
        }

        void IPolicy.SetBackgroundCheckAndCloseStep(Policy.BackgroundCheck backgroundCheck)
        {
            throw new NotImplementedException();
        }

        Requirement.Document IPolicy.GetIdCopyRequirement(Requirement requirement)
        {
            throw new NotImplementedException();
        }

        bool IPolicy.CheckPolicyMedical(Policy.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.GenerateTempPolicyNo(Policy.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Contact.Action> IPolicy.GetPolicyAction(Policy.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.ChangePolicyChain(Policy.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.StatusChange> IPolicy.SetPolicyStatus(Policy.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        Policy IPolicy.NewPolicyWithoutAgent(Case.NewCase newCase)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.VehiclesCoverage> IPolicy.GetVehiclesCoverage(Policy.Parameter policyParameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.CRBS> IPolicy.GetCRBS(Policy.CRBSParameter parameters)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Vehicle.Discount> IPolicy.GetPolicyVehicleDiscount(Policy.DVParameter parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Vehicle.Discount.RulesAndDetails> IPolicy.GetDiscountRulesAndDetails(Policy.DParameter parameter)
        {
            throw new NotImplementedException();
        }

        Policy.Vehicle.Discount IPolicy.SetPolicyVehicleDiscount(Policy.Vehicle.Discount parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Agent.SaleChannelInfo> IPolicy.GetAgentSaleChannelInfo(Policy.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.VehicleInsured> IPolicy.GetVehicleInsured(Policy.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.VehicleCoverage> IPolicy.GetVehicleCoverage(Policy.VehicleCoverageGet parameter)
        {
            throw new NotImplementedException();
        }

        Policy.VehicleCoverage IPolicy.SetVehicleCoverage(Policy.VehicleCoverage vehicleCoverage)
        {
            throw new NotImplementedException();
        }

        Policy.VehicleInsured IPolicy.SetVehicleInsured(Policy.VehicleInsured vehicleInsured)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.VehicleCoverageSurcharge> IPolicy.GetVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            throw new NotImplementedException();
        }

        Policy.VehicleCoverageSurcharge IPolicy.SetVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Document> IPolicy.GetPolicyDocument(Policy.Document parameter)
        {
            throw new NotImplementedException();
        }

        Policy.Document IPolicy.SetPolicyDocument(Policy.Document parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetPolicyNo(Policy.Number parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetPaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetPolicy(Policy policy)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.VehicleInsured.CoverageTypePremiun> IPolicy.GetVehicleInsuredCoverageTypePremiun(Policy.VehicleInsured.CoverageTypePremiun.Key parameter)
        {
            throw new NotImplementedException();
        }

        Policy.VehicleInsured.CoverageTypePremiun.Key IPolicy.SetVehicleInsuredCoverageTypePremiun(Policy.VehicleInsured.CoverageTypePremiun parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.VehicleInsured.Discount> IPolicy.GetVehicleInsuredDiscount(Policy.VehicleInsured.Discount.Key parameter)
        {
            throw new NotImplementedException();
        }

        Policy.VehicleInsured.Discount.Key IPolicy.SetVehicleInsuredDiscount(Policy.VehicleInsured.Discount parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.DocumentQuotation> IPolicy.GetQuotationDocumentReview(Policy.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetVehicleInsuredInspection(Policy.VehicleInsured.InspectionV parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetVehicleInsuredApplyDiscountAndSurcharge(Policy.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.DeleteVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.UpdateVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Vehicle.Requirement> IPolicy.GetVehicleRequirement(Policy.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetAssingQuotation(Policy.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.UpdateQuotationInfoTemp(Policy.Quo.Temp parameter)
        {
            throw new NotImplementedException();
        }

        Policy.Quo.Temp.TempResult IPolicy.UpdateOneQuotationInfoTemp(Policy.Quo.Temp parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Quo> IPolicy.GetQuotationInfoTemp(Policy.Quo.Temp parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetPolicySourceId(Policy.PSourceId parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Agent> IPolicy.GetAgentIdListByAgentId(Policy.Agent parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.LogResult> IPolicy.InsertLog(Policy.LogParameter parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.UpdatePolicyEffectiveDate(Policy.OEffectiveDate parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.UpdatePolicyExpirationDate(Policy.OExpirationDate parameter)
        {
            throw new NotImplementedException();
        }

        Policy.UQuo IPolicy.UpdatePolicyQuo(Policy.UQuo parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.ConditionForSysflexIL> IPolicy.GetConditionIL(string QuotationNumber, long EntityId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Facultative.Contract> IPolicy.GetFacultativeContract(Policy.Facultative.Key parameters)
        {
            throw new NotImplementedException();
        }

        DataTable IPolicy.GetFacultativeContractCoverage(Policy.Facultative.Key parameters)
        {
            throw new NotImplementedException();
        }

        DataTable IPolicy.SetFacultativeContractCoverage(Policy.Facultative.SetContractCoverage parameters)
        {
            throw new NotImplementedException();
        }

        int IPolicy.DeleteFacultativeContract(int corpId, long contractUniqueId, int userId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Facultative.Contract2> IPolicy.GetFacultativeContractCoverage2(Policy.Facultative.Key parameters)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Agent.AgentSupervisor> IPolicy.GetAgentSupervisor(int corpId, int agentID)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.PolicyContact> IPolicy.GetPolicyByContact(int? ContactId)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetReninsuranceFacultative(Policy.ReinsuranceFacultative parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.QuoGrid> IPolicy.GetAllCustomerPlanDetailQuo(Policy.QuoGrid.Key parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.QuoGrid.Counter> IPolicy.GetAllCustomerPlanDetailCountQuo(Policy.QuoGrid.Key parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.QuoGrid> IPolicy.GetAllPuntoVentaQuo(Policy.QuoGrid.Key parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetVehicleInsuredInspectionStatus(Policy.VehicleInsured.InspectionV parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetVehicleInsuredInspectionAddress(Policy.VehicleInsured.InspectionV parameter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.TabRol> IPolicy.GetAllTabsByRole(int UsrId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.QuoGrid.AgentChain> IPolicy.GetAgentChain(Policy.QuoGrid.AgentChain parameter)
        {
            throw new NotImplementedException();
        }

        DataTable IPolicy.GetAgentChain(int agentId)
        {
            throw new NotImplementedException();
        }


        int IPolicy.BlackListMember(Policy.BlackListMember.Parameter parameter)
        {
            throw new NotImplementedException();
        }


        int IPolicy.SetBlakList(Policy.BlackList.Parameter parameter)
        {
            throw new NotImplementedException();
        }


        #region BackgroundCheck Underwriting
        public IEnumerable<Policy.BackGroundCheckLink> Bg_Get_Match_Links(Policy.BackGroundCheckLink param)
        {
            throw new NotImplementedException();
        }

        public virtual int Bg_Set_Match_Links(Policy.BackGroundCheckLink param)
        {
            throw new NotImplementedException();
        }

        #endregion


        IEnumerable<Policy.BackGroundCheckLink> IPolicy.Bg_Get_Match_Links(Policy.BackGroundCheckLink param)
        {
            throw new NotImplementedException();
        }

        int IPolicy.Bg_Set_Match_Links(Policy.BackGroundCheckLink param)
        {
            throw new NotImplementedException();
        }

        Policy.AgentInfo IPolicy.GetAgentInfo(int? CorpId, int? AgentId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.AgentInfo.Directory> IPolicy.GetAgentDirectoryInfo(int? DirectoryId)
        {
            throw new NotImplementedException();
        }

        Policy.AgentInfo.Agent IPolicy.SetAgent(Policy.AgentInfo.Agent.parameter parameter)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetAgentUniqueID(int? CorpId, int? AgentId, string UniqueId)
        {
            throw new NotImplementedException();
        }

        int? IPolicy.SetPolicyLoanNo(int? corp_Id, int? region_Id, int? country_Id, int? domesticreg_Id, int? state_Prov_Id, int? city_Id, int? office_Id, int? case_Seq_No, int? hist_Seq_No, string loanPetitionNo, int? userId)
        {
            throw new NotImplementedException();
        }


        int? IPolicy.SetPolicyDirectDebit(int? corp_Id, int? region_Id, int? country_Id, int? domesticreg_Id, int? state_Prov_Id, int? city_Id, int? office_Id, int? case_Seq_No, int? hist_Seq_No, bool? DirectDebit, bool? includeInitial, int? userId)
        {
            throw new NotImplementedException();
        }


        decimal? IPolicy.GetQuotFromSysFlex(string PolicyNo)
        {
            throw new NotImplementedException();
        }

        public Policy.CouponInfo GetCouponInfo(string policyNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Policy.Vehicle.AccidentRate> IPolicy.GetTbAccidentRateByMakeAndModel(string Make, string Model)
        {
            throw new NotImplementedException();
        }        

        Policy.CouponInfo IPolicy.GetCouponInfo(string policyNo)
        {
            throw new NotImplementedException();
        }

        int IPolicy.SetCustomerSing(int? corpId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId, int? officeId, int? caseSeqNo, int? histSeqNo, string dataSign)
        {
            throw new NotImplementedException();
        }

        string IPolicy.GetCustomerSing(Nullable<int> corpId, Nullable<int> regionId, Nullable<int> countryId, Nullable<int> domesticRegId, Nullable<int> stateProvId, Nullable<int> cityId, Nullable<int> officeId, Nullable<int> caseSeqNo, Nullable<int> histSeqNo) 
        {
            throw new NotImplementedException();
        }
    }
}