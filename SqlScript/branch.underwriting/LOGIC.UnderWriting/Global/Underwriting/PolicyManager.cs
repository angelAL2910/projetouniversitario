﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DATA.UnderWriting.Repositories.Global;
using DATA.UnderWriting.UnitOfWork;
using Entity.UnderWriting.Entities;
using LOGIC.UnderWriting.Common;
using System.Reflection;
using DATA.UnderWriting.Repositories.Base;

namespace LOGIC.UnderWriting.Global
{
    public class PolicyManager
    {
        private PolicyRepository _policyRepository;
        private CaseRepository _caseRepository;
        private StepManager _stepManager;
        private string _commentFormat, _commentSeparator, _dateFormat, _msg;

        public PolicyManager()
        {
            _policyRepository = SingletonUnitOfWork.Instance.PolicyRepository;
            _caseRepository = SingletonUnitOfWork.Instance.CaseRepository;
            _stepManager = new StepManager();
            _commentFormat = "{0}\n{1}\n{2}";
            _commentSeparator = "\n------------------------------------\n";
            _dateFormat = "MM/dd/yyyy hh:mm:ss";
            _msg = "This property can't be under 0.";
        }

        public virtual Policy.PlanData GetPlanData(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo)
        {
            Policy.Parameter policyParameter;

            policyParameter = this.ConvertPolicyParameters(
                coprId,
                regionId,
                countryId,
                domesticRegId,
                stateProvId,
                cityId,
                officeId,
                caseSeqNo,
                histSeqNo);

            return
                _policyRepository.GetPlanData(policyParameter);
        }

        public virtual int AddContactToPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo
            , int? contactId, int contactTypeId, int contactRoleTypeId, int agentId, int userId)
        {
            int result;
            int? temp;

            temp = _policyRepository.AddContactToPolicy(
              coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
              , contactId, contactTypeId, contactRoleTypeId, agentId, userId
               );

            result = temp.HasValue ? temp.Value : -1;

            return
                result;
        }

        public virtual int SetReninsuranceFacultative(Policy.ReinsuranceFacultative reinsuranceFac)
        {
            int result;
            int? temp;

            temp = _policyRepository.SetReninsuranceFacultative(reinsuranceFac);

            result = temp.HasValue ? temp.Value : -1;

            return
                result;
        }

        public virtual int DeletePolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int userId)
        {
            int result;
            Policy.Parameter policyParameter;

            policyParameter = this.ConvertPolicyParameters(
                coprId,
                regionId,
                countryId,
                domesticRegId,
                stateProvId,
                cityId,
                officeId,
                caseSeqNo,
                histSeqNo,
                null,
                null,
                null,
                userId);

            result = _policyRepository.DeletePolicy(policyParameter);

            return
                result;
        }

        public virtual IEnumerable<Policy.Profile> GetProfilePersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int profileTypeId)
        {
            return
                _policyRepository.GetProfilePersonalized(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, profileTypeId);
        }

        public virtual IEnumerable<Policy.Profile> GetProfileNoPersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int profileTypeId)
        {
            return
                _policyRepository.GetProfileNoPersonalized(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, profileTypeId);
        }

        public virtual IEnumerable<Policy.PolicySummaryByPolicy> GetSummaryByPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo)
        {
            Policy.Parameter policyParameter;

            policyParameter = this.ConvertPolicyParameters(
                coprId,
                regionId,
                countryId,
                domesticRegId,
                stateProvId,
                cityId,
                officeId,
                caseSeqNo,
                histSeqNo);

            return
                _policyRepository.GetSummaryByPolicy(policyParameter);
        }

        public virtual IEnumerable<Policy.PolicySummaryByContact> GetSummaryByContact(int contactId)
        {
            return
               _policyRepository.GetSummaryByContact(contactId);
        }

        public virtual IEnumerable<Policy.RequirementSummary> GetRequirementSummary(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? requirementCatId, int? requirementTypeId)
        {
            return
                _policyRepository.GetRequirementSummary(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, requirementCatId, requirementTypeId);
        }

        public virtual IEnumerable<Policy.PaymentSummary> GetPaymentSummary(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId
            , int? officeId, int? caseSeqNo, int? histSeqNo, int OwnerContactId)
        {
            return
                _policyRepository.GetPaymentSummary(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, OwnerContactId);
        }

        public virtual IEnumerable<Policy.AssignCase> GetPolicyAssingCase(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo)
        {
            return _policyRepository.GetPolicyAssingCase(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);
        }

        public virtual int UpdatePolicy(Policy policy)
        {
            if (policy.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (policy.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (policy.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (policy.DomesticregId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticregId");
            if (policy.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (policy.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (policy.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (policy.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (policy.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");

            policy.PolicyNo = null;

            return
                 _policyRepository.SetPolicy(policy);
        }

        public virtual int UpdatePaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            this.IsValid(paymentFreq, Utility.DataBaseActionType.Update);

            return
                 _policyRepository.SetPaymentFrequency(paymentFreq);
        }
        public virtual int InsertPaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            this.IsValid(paymentFreq, Utility.DataBaseActionType.Insert);

            paymentFreq.PaymentFreqId = -1;
            paymentFreq.PaymentDate = DateTime.Now;

            return
                 _policyRepository.SetPaymentFrequency(paymentFreq);
        }

        public virtual int DeleteContactRole(int corpId, int regionId, int countryId, int domesticregId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int contactRoleTypeId, int userId)
        {
            if (corpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "corpId");
            if (regionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "regionId");
            if (countryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "countryId");
            if (domesticregId <= 0)
                throw new ArgumentException("This property can't be under 0.", "domesticregId");
            if (stateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "stateProvId");
            if (cityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "cityId");
            if (officeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "officeId");
            if (caseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "caseSeqNo");
            if (histSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "histSeqNo");
            if (contactRoleTypeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "contactRoleTypeId");

            return
                 _policyRepository.DeleteContactRole(
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

        public virtual IEnumerable<Policy.CategoryDocument> GetCategoryDocument(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? docTypeId, int? docCategoryId, int languageId)
        {
            return
                 _policyRepository.GetCategoryDocument(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, docTypeId, docCategoryId, languageId);
        }

        public virtual IEnumerable<Policy.AgentChainDetail> GetAgentChainDetail(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo)
        {
            Policy.Parameter policyParameter;

            policyParameter = this.ConvertPolicyParameters(
                coprId,
                regionId,
                countryId,
                domesticRegId,
                stateProvId,
                cityId,
                officeId,
                caseSeqNo,
                histSeqNo);

            return
                 _policyRepository.GetAgentChainDetail(policyParameter);
        }

        public virtual IEnumerable<Policy.PolicyCommunication> GetPolicyCommunication(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? callNoteId)
        {
            return
                 _policyRepository.GetPolicyCommunication(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, callNoteId);
        }

        public virtual int RecentViewed(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int underwriterId)
        {
            int result, viewId;
            DateTime viewDate;

            viewId = -1;
            viewDate = DateTime.Now;

            result = _policyRepository.RecentViewed(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , underwriterId, viewId, viewDate, underwriterId);

            return
                result;
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
        public virtual int DeleteContactAndRole(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int contactId, int contactRoleTypeId, int userId)
        {
            int result;

            try
            {
                result = _policyRepository.DeleteContactAndRole(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                                , contactId, contactRoleTypeId, userId);
            }
            catch (Exception)
            {
                result = -1;
            }

            return
                result;
        }

        public virtual IEnumerable<Policy.PolicyCommentSummary> GetCommentSummary(int? coprId, int? regionId, int? countryId, int? domesticRegId, int? stateProvId, int? cityId
            , int? officeId, int? caseSeqNo, int? histSeqNo)
        {
            IEnumerable<Policy.PolicyCommentSummary> comments, result;
            List<Policy.PolicyCommentSummary> temp;
            Policy.PolicyCommentSummary oTempa, oTempb;
            string tempNote;
            IEnumerable<int> aTypes;
            Policy.Parameter policyParameter;

            policyParameter = this.ConvertPolicyParameters(
                coprId,
                regionId,
                countryId,
                domesticRegId,
                stateProvId,
                cityId,
                officeId,
                caseSeqNo,
                histSeqNo);

            comments = _policyRepository.GetCommentSummary(policyParameter);

            aTypes = comments
                        .Where(c => c.IsDefault)
                        .Select(c => c.StepId)
                        .Distinct();

            if (comments != null && comments.Any())
            {
                temp = new List<Policy.PolicyCommentSummary>(1);

                foreach (int type in aTypes)
                {
                    tempNote = string.Join(_commentSeparator,
                             comments
                                 .Where(c => c.StepId == type && !c.IsDefault)
                                 .OrderByDescending(c => c.CreateDate)
                                 .Select(c => string.Format(_commentFormat, c.CreateDate.ToString(_dateFormat), c.OriginatedByName, c.NoteDesc))
                         );

                    oTempa = comments
                                 .FirstOrDefault(c => c.StepId == type && c.IsDefault);

                    oTempb = new Policy.PolicyCommentSummary
                    {
                        StepId = type,
                        TypeDesc = oTempa.TypeDesc,
                        NoteDesc = tempNote
                    };

                    temp.Add(oTempb);
                }

                result = temp;
            }
            else
            {
                result = null;
            }

            return
                result;
        }

        public virtual int DeletePaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            return
                 _policyRepository.DeletePaymentFrequency(paymentFreq);
        }

        public virtual Policy GetPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo)
        {
            Policy.Parameter policyParameter;

            policyParameter = this.ConvertPolicyParameters(
                coprId,
                regionId,
                countryId,
                domesticRegId,
                stateProvId,
                cityId,
                officeId,
                caseSeqNo,
                histSeqNo);

            return
                _policyRepository.GetPolicy(policyParameter);
        }

        public virtual bool? GetCheckPolicyActive(string PolicyNo)
        {
            return
                _policyRepository.GetCheckPolicyActive(PolicyNo);
        }

        #region UnderwritingCall
        public virtual int SetUnderwritingCallSecurityQuestion(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int questionId, int contactId, bool answer, int userId)
        {
            return
                _policyRepository.SetUnderwritingCallSecurityQuestion(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , stepTypeId, stepId, stepCaseNo, questionId, contactId, answer, userId);
        }

        public virtual int SetUnderwritingCallTabAnswer(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int tabId, bool answer, int userId)
        {
            return
                _policyRepository.SetUnderwritingCallTabAnswer(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                , stepTypeId, stepId, stepCaseNo, tabId, answer, userId);
        }

        private string UnderwritingCallSecurityQuestions(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo, int contactId, string html, string questionHtml)
        {
            IEnumerable<Policy.SecurityQuestion> sc;
            string result;

            sc = _policyRepository.GetUnderwritingCallSecurityQuestion(coprId, regionId, countryId, domesticRegId, stateProvId, cityId
                        , officeId, caseSeqNo, histSeqNo, stepTypeId, stepId, stepCaseNo, contactId);

            if (sc != null && sc.Any())
            {
                result = html.Replace("{QuestionsProtocol}",
                    string.Join(string.Empty,
                        sc.Select(q =>
                            questionHtml
                                .Replace("{QuestionId}", q.QuestionId.ToString())
                                .Replace("{Question}", q.QuestionDesc)
                                .Replace("{Answer}", q.Answer)
                                .Replace("{Response}", q.Response ? "checked" : string.Empty)
                                )
                            )
                        );
            }
            else
                result = html.Replace("{QuestionsProtocol}", string.Empty);

            return
                result;
        }

        public virtual IEnumerable<Policy.UnderwritingCallTemplate> GetUnderwritingCallTemplate(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int contactId, int languageId)
        {
            DataTable dTResult;
            IEnumerable<Policy.UnderwritingCallTemplate> result, tempResult;
            List<Policy.UnderwritingCallTemplate> templates;
            Policy.UnderwritingCallTemplate oTemp;
            string newHtml;

            tempResult = _policyRepository.GetUnderwritingCallTemplate(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, languageId);

            if (tempResult != null && tempResult.Any())
            {
                oTemp = tempResult.FirstOrDefault();

                dTResult = _policyRepository.GetUnderwritingCallTemplateData(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                                    , oTemp.StepTypeId, oTemp.StepId, oTemp.StepCaseNo, languageId);

                if (dTResult != null && dTResult.Rows.Count > 0)
                {
                    templates = new List<Policy.UnderwritingCallTemplate>(1);

                    foreach (Policy.UnderwritingCallTemplate t in tempResult)
                    {
                        newHtml = t.Html;

                        newHtml = newHtml.Replace("{CategoryId}", t.CategoryId.ToString());

                        if (t.CategoryId == 2) //Underwriting Call - Protocol
                            newHtml = this.UnderwritingCallSecurityQuestions(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
                                        , oTemp.StepTypeId, oTemp.StepId, oTemp.StepCaseNo, contactId, newHtml, t.QuestionHtml);



                        for (int i = 0; i < dTResult.Columns.Count; i++)
                            newHtml = newHtml.Replace("{" + dTResult.Columns[i].Caption + "}", dTResult.Rows[0][i].ToString());

                        oTemp = new Policy.UnderwritingCallTemplate
                        {
                            CorpId = t.CorpId,
                            RegionId = t.RegionId,
                            CountryId = t.CountryId,
                            DomesticregId = t.DomesticregId,
                            StateProvId = t.StateProvId,
                            CityId = t.CityId,
                            OfficeId = t.OfficeId,
                            CaseSeqNo = t.CaseSeqNo,
                            HistSeqNo = t.HistSeqNo,
                            StepTypeId = t.StepTypeId,
                            StepId = t.StepId,
                            StepCaseNo = t.StepCaseNo,
                            TemplateId = t.TemplateId,
                            LanguageId = t.LanguageId,
                            CategoryId = t.CategoryId,
                            LanguageDesc = t.LanguageDesc,
                            CategoryDesc = t.CategoryDesc,
                            IsClose = t.IsClose,
                            Html = newHtml,
                            QuestionHtml = t.QuestionHtml
                        };
                        templates.Add(oTemp);
                    }

                    result = templates;
                }
                else
                    result = tempResult;
            }
            else
                result = null;

            return
                result;
        }

        public virtual IEnumerable<Policy.UnderwritingCallTemplate> GetUnderwritingCallTemplateByCategory(int coprId, int categoryId, int languageId)
        {
            return
                _policyRepository.GetUnderwritingCallTemplateByCategory(coprId, categoryId, languageId);
        }

        public virtual Policy.UnderwritingCallTemplate GetUnderwritingCallTemplateByCategoryFirst(int coprId, int categoryId, int languageId)
        {
            return
                this.GetUnderwritingCallTemplateByCategory(coprId, categoryId, languageId).FirstOrDefault();
        }

        public virtual IEnumerable<Policy.UnderwritingCallComment> GetUnderwritingCallComments(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId, int stepCaseNo)
        {
            return
                 _policyRepository.GetUnderwritingCallComments(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, stepTypeId, stepId, stepCaseNo);
        }

        public virtual int UpdateUnderwritingCallComment(Case.Comment comment)
        {
            comment.CommentId = 1;
            comment.CommentDate = DateTime.Now;

            return
                 _caseRepository.SetComment(comment);
        }

        #endregion

        public virtual IEnumerable<Policy.ProductByContact> GetProductByContactAndRole(int? contactTypeId, int contactId, int languageId)
        {
            return
                _policyRepository.GetProductByContactAndRole(contactTypeId, contactId, languageId);
        }

        public virtual Policy.Call InsertCall(Policy.Call call)
        {
            this.IsValid(call, Utility.DataBaseActionType.Insert);

            call.CallId = -1;

            return
                _policyRepository.SetCall(call);
        }

        public virtual Policy.Call UpdateCall(Policy.Call call)
        {
            this.IsValid(call, Utility.DataBaseActionType.Update);

            return
                _policyRepository.SetCall(call);
        }

        public virtual int SetValidTab(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int projectId, int tabId, bool isValid, int userId)
        {
            if (coprId <= 0)
                throw new ArgumentException("This property can't be under 0.", "coprId");
            if (regionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "regionId");
            if (countryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "countryId");
            if (domesticRegId <= 0)
                throw new ArgumentException("This property can't be under 0.", "domesticRegId");
            if (stateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "stateProvId");
            if (cityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "cityId");
            if (officeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (caseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "caseSeqNo");
            if (histSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "histSeqNo");

            return
               _policyRepository.SetValidTab(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo
               , projectId, tabId, isValid, userId);
        }

        public virtual IEnumerable<Policy.Tab> GetTabValidation(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
         , int officeId, int caseSeqNo, int histSeqNo)
        {
            Policy.Parameter policyParameter;

            policyParameter = this.ConvertPolicyParameters(
                coprId,
                regionId,
                countryId,
                domesticRegId,
                stateProvId,
                cityId,
                officeId,
                caseSeqNo,
                histSeqNo);

            return
               _policyRepository.GetTabValidation(policyParameter);
        }

        public virtual void SetValidTabRequirementForNewBusiness(Policy.Parameter policy)
        {
            bool requirementResult;

            this.IsValid(policy, Utility.DataBaseActionType.Update);

            requirementResult = this.GetTabValidationRequirement(policy);

            this.SetValidTab(
                policy.CorpId,
                policy.RegionId,
                policy.CountryId,
                policy.DomesticregId,
                policy.StateProvId,
                policy.CityId,
                policy.OfficeId,
                policy.CaseSeqNo,
                policy.HistSeqNo,
                2, //NewBusiness
                5, //Requirements
                requirementResult,
                policy.UnderwriterId);
        }

        private bool GetTabValidationRequirement(Policy.Parameter policy)
        {
            return
                _policyRepository.GetTabValidationRequirement(policy);
        }


        public virtual IEnumerable<Policy.Contact> GetContactPolicy(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int? contactId, int? contactRoleTypeId)
        {
            return
                _policyRepository.GetContactPolicy(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, contactRoleTypeId);
        }

        public virtual IEnumerable<Policy.Contact> GetPolicyAddInsured(Policy.Parameter policy)
        {
            return
                _policyRepository.GetPolicyAddInsured(policy);
        }

        public virtual int UpdateActivitiesFinancial(Policy.Contact contact)
        {
            if (contact.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (contact.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (contact.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (contact.DomesticregId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticregId");
            if (contact.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (contact.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (contact.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (contact.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (contact.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");
            if (contact.ContactId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ContactId");
            if (contact.ContactRoleTypeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ContactRoleTypeId");
            if (contact.UserId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UserId");

            contact.RegionOfResidenceId = null;
            contact.CountryOfResidenceId = null;
            contact.RegionOfBirthId = null;
            contact.CountryOfBirthId = null;
            contact.BlockConfirmationCall = null;
            contact.HealthWeigth = null;
            contact.HealthWeigthTypeId = null;
            contact.HealthHeight = null;
            contact.HealthHeigthTypeId = null;
            contact.HealthAge = null;
            contact.HealthGender = null;
            contact.HealthSmoke = null;
            contact.HealthExcercise = null;
            contact.HealthDrugs = null;
            contact.HealthSystolic = null;
            contact.HealthDiastolic = null;
            contact.HealthLastMedVisit = null;
            contact.HealthLastMedReason = null;
            contact.HealthLastMedResult = null;
            contact.HealthDrName = null;
            contact.HealthDrAddress = null;
            contact.HealthDrPhonePrefix = null;
            contact.HealthDrPhoneArea = null;
            contact.HealthDrPhoneNum = null;
            contact.HealthMedication = null;
            //contact.AsstTotalAssets = null;
            //contact.AsstRealEstate = null;
            //contact.AsstPersonalEffects = null;
            //contact.AsstVehicle = null;
            //contact.AsstMachineryEqpmnt = null;
            //contact.AsstStockBonds = null;
            //contact.AsstOtherAssets = null;
            //contact.LblTotalLiabilities = null;
            //contact.LblMachineryEqpmnt = null;
            //contact.LblNotePayable = null;
            //contact.LblBankDebts = null;
            //contact.LblPersonalDebts = null;
            //contact.LblMortgageDebts = null;
            //contact.LblOutstandingTaxes = null;
            //contact.LblShortTermsLoans = null;
            //contact.LblOtherLiabilities = null;
            //contact.FncTotalEstateAmnt = null;
            //contact.FncAnnualRevMainActvt = null;
            //contact.FncAnnualIncomeOtherJobs = null;
            //contact.FncAnnualIncomeInvst = null;
            //contact.FncAnnualIncomeTrade = null;
            //contact.HomeStatusId = null;
            //contact.LaborPlayedId = null;
            contact.LineOfBusiness = null;
            contact.LineOfBusiness2 = null;
            contact.CompanyName = null;
            contact.LengthWorkYear = null;
            contact.LengthWorkMonth = null;
            contact.Labortasks = null;
            contact.CompanyActivity = null;
            contact.CompanyFoundationDate = null;
            contact.OccupGroupTypeId = null;
            contact.OccupationId = null;
            contact.RelationshiptoAgent = null;
            contact.RelationshiptoOwner = null;
            contact.AnnualPersonalIncome = null;
            contact.AnnualFamilyIncome = null;
            contact.Smoker = null;
            contact.MaritalStatId = null;
            contact.BeneficiaryTypeId = null;
            contact.PrimaryBeneficiaryId = null;
            contact.PrimaryBeneficiary = null;
            contact.RelationshipToOwnerId = null;
            contact.RelToPrimaryBenefId = null;
            contact.BenefitsPercent = null;
            contact.Comment = null;

            return
                this.SetContactPolicy(contact);
        }
        public virtual int UpdatePersonalInfoContact(Contact contact)
        {
            Policy.Contact pContact;

            if (contact.PolicyInfo == null)
                throw new ArgumentException("This property can't be null.", "PolicyInfo");
            if (contact.PolicyInfo.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (contact.PolicyInfo.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (contact.PolicyInfo.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (contact.PolicyInfo.DomesticregId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticregId");
            if (contact.PolicyInfo.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (contact.PolicyInfo.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (contact.PolicyInfo.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (contact.PolicyInfo.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (contact.PolicyInfo.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");
            if (contact.PolicyInfo.ContactId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ContactId");
            if (contact.PolicyInfo.ContactRoleTypeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ContactRoleTypeId");
            if (contact.PolicyInfo.UserId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UserId");
            if (contact.ContactId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ContactId");

            pContact = new Policy.Contact();

            pContact.CorpId = contact.PolicyInfo.CorpId;
            pContact.RegionId = contact.PolicyInfo.RegionId;
            pContact.CountryId = contact.PolicyInfo.CountryId;
            pContact.DomesticregId = contact.PolicyInfo.DomesticregId;
            pContact.StateProvId = contact.PolicyInfo.StateProvId;
            pContact.CityId = contact.PolicyInfo.CityId;
            pContact.OfficeId = contact.PolicyInfo.OfficeId;
            pContact.CaseSeqNo = contact.PolicyInfo.CaseSeqNo;
            pContact.HistSeqNo = contact.PolicyInfo.HistSeqNo;
            pContact.ContactId = contact.PolicyInfo.ContactId;
            pContact.ContactRoleTypeId = contact.PolicyInfo.ContactRoleTypeId;
            pContact.UserId = contact.PolicyInfo.UserId;

            pContact.RegionOfResidenceId = contact.RegionOfResidenceId;
            pContact.CountryOfResidenceId = contact.CountryOfResidenceId;
            pContact.RegionOfBirthId = contact.RegionOfBirthId;
            pContact.CountryOfBirthId = contact.CountryOfBirthId;
            pContact.LineOfBusiness = contact.LineOfBusiness;
            pContact.LineOfBusiness2 = contact.LineOfBusiness2;
            pContact.CompanyName = contact.CompanyName;
            pContact.LengthWorkYear = contact.LengthWorkYear;
            pContact.LengthWorkMonth = contact.LengthWorkMonth;
            pContact.Labortasks = contact.LaborTasks;
            pContact.OccupGroupTypeId = contact.OccupGroupTypeId;
            pContact.OccupationId = contact.OccupationId;
            pContact.RelationshiptoAgent = contact.RelationshiptoAgent;
            pContact.RelationshiptoOwner = contact.RelationshiptoOwner;
            pContact.AnnualPersonalIncome = contact.AnnualPersonalIncome;
            pContact.AnnualFamilyIncome = contact.AnnualFamilyIncome;
            pContact.Smoker = contact.Smoker;
            pContact.MaritalStatId = contact.MaritalStatId;
            pContact.TipoRiesgoNameKey = contact.TipoRiesgoNameKey;
            pContact.InvoiceTypeId = contact.InvoiceTypeId;

            pContact.CompanyActivity = null;
            pContact.CompanyFoundationDate = null;
            pContact.BlockConfirmationCall = null;
            pContact.HealthWeigth = null;
            pContact.HealthWeigthTypeId = null;
            pContact.HealthHeight = null;
            pContact.HealthHeigthTypeId = null;
            pContact.HealthAge = null;
            pContact.HealthGender = null;
            pContact.HealthSmoke = null;
            pContact.HealthExcercise = null;
            pContact.HealthDrugs = null;
            pContact.HealthSystolic = null;
            pContact.HealthDiastolic = null;
            pContact.HealthLastMedVisit = null;
            pContact.HealthLastMedReason = null;
            pContact.HealthLastMedResult = null;
            pContact.HealthDrName = null;
            pContact.HealthDrAddress = null;
            pContact.HealthDrPhonePrefix = null;
            pContact.HealthDrPhoneArea = null;
            pContact.HealthDrPhoneNum = null;
            pContact.HealthMedication = null;
            pContact.AsstTotalAssets = null;
            pContact.AsstRealEstate = null;
            pContact.AsstPersonalEffects = null;
            pContact.AsstVehicle = null;
            pContact.AsstMachineryEqpmnt = null;
            pContact.AsstStockBonds = null;
            pContact.AsstOtherAssets = null;
            pContact.LblTotalLiabilities = null;
            pContact.LblMachineryEqpmnt = null;
            pContact.LblNotePayable = null;
            pContact.LblBankDebts = null;
            pContact.LblPersonalDebts = null;
            pContact.LblMortgageDebts = null;
            pContact.LblOutstandingTaxes = null;
            pContact.LblShortTermsLoans = null;
            pContact.LblOtherLiabilities = null;
            pContact.FncTotalEstateAmnt = null;
            pContact.FncAnnualRevMainActvt = null;
            pContact.FncAnnualIncomeOtherJobs = null;
            pContact.FncAnnualIncomeInvst = null;
            pContact.FncAnnualIncomeTrade = null;
            pContact.HomeStatusId = null;
            pContact.LaborPlayedId = null;
            pContact.BeneficiaryTypeId = null;
            pContact.PrimaryBeneficiaryId = null;
            pContact.PrimaryBeneficiary = null;
            pContact.RelationshipToOwnerId = null;
            pContact.RelToPrimaryBenefId = null;
            pContact.BenefitsPercent = null;
            pContact.Comment = null;

            SingletonUnitOfWork.Instance.ContactRepository.UpdateContact(contact);

            return
                this.SetContactPolicy(pContact);
        }

        public virtual int SetContactPolicyInfo(Policy.Contact contact)
        {
            return
                _policyRepository.SetContactPolicy(contact);
        }
        private int SetContactPolicy(Policy.Contact contact)
        {
            return
                _policyRepository.SetContactPolicy(contact);
        }



        #region InvestmentProfile
        public virtual IEnumerable<Policy.InvestProfilePersonalized> GetInvestProfilePersonalized(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
           , int officeId, int caseSeqNo, int histSeqNo, int profileTypeId)
        {
            return
                _policyRepository.GetInvestProfilePersonalized(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, profileTypeId);
        }

        public virtual int SetInvestProfilePersonalized(Policy.InvestProfilePersonalized investProfilePersonalized)
        {
            IsValid(investProfilePersonalized, Utility.DataBaseActionType.Insert);

            return
               this.PrivateSetInvestProfilePersonalized(investProfilePersonalized);
        }
        public virtual int SetInvestProfilePersonalized(IEnumerable<Policy.InvestProfilePersonalized> investProfilePersonalizedList)
        {
            int result;

            if (investProfilePersonalizedList != null && investProfilePersonalizedList.Any())
                foreach (Policy.InvestProfilePersonalized investProfilePersonalized in investProfilePersonalizedList)
                    IsValid(investProfilePersonalized, Utility.DataBaseActionType.Insert);
            else
                throw new ArgumentException("This parameter can't be null or empty.", "investProfilePersonalizedList");

            result = -1;

            foreach (Policy.InvestProfilePersonalized investProfilePersonalized in investProfilePersonalizedList)
                result = this.PrivateSetInvestProfilePersonalized(investProfilePersonalized);

            return
                result;
        }

        public virtual int InsertInvestmentProfile(Policy.InvestProfile investProfile)
        {
            IsValid(investProfile, Utility.DataBaseActionType.Insert);

            investProfile.InvestmentProductDate = DateTime.Now;
            investProfile.InvestProductDateId = int.Parse(investProfile.InvestmentProductDate.ToString("yyyyMMdd"));

            return
                this.SetInvestmentProfile(investProfile);
        }
        public virtual int UpdateInvestmentProfile(Policy.InvestProfile investProfile)
        {
            IsValid(investProfile, Utility.DataBaseActionType.Update);

            return
                this.SetInvestmentProfile(investProfile);
        }

        [ObsoleteAttribute("This method is deprecated.")]
        public virtual int DeleteInvestmentProfile(Policy.InvestProfile investProfile)
        {
            return
                _policyRepository.DeleteInvestmentProfile(investProfile);
        }

        private int SetInvestmentProfile(Policy.InvestProfile investProfile)
        {
            return
                _policyRepository.SetInvestmentProfile(investProfile);
        }
        private int PrivateSetInvestProfilePersonalized(Policy.InvestProfilePersonalized investProfilePersonalized)
        {
            return
                _policyRepository.SetInvestProfilePersonalized(investProfilePersonalized);
        }
        private void IsValid(Policy.InvestProfile investProfile, Utility.DataBaseActionType action)
        {
            if (investProfile.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (investProfile.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (investProfile.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (investProfile.DomesticregId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticregId");
            if (investProfile.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (investProfile.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (investProfile.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (investProfile.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (investProfile.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");
            if (!investProfile.ProfileTypeId.HasValue || investProfile.ProfileTypeId.Value <= 0)
                throw new ArgumentException("This property can't be null or under 0.", "ProfileTypeId");
            if (investProfile.UserId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UserId");

            switch (action)
            {
                case Utility.DataBaseActionType.Update:
                case Utility.DataBaseActionType.Delete:
                    if (investProfile.InvestmentProductDate == default(DateTime))
                        throw new ArgumentException("This property can't be the default value.", "InvestmentProductDate");
                    if (!investProfile.InvestProductDateId.HasValue || investProfile.InvestProductDateId.Value <= 0)
                        throw new ArgumentException("This property can't be null or under 0.", "InvestProductDateId");
                    break;
                case Utility.DataBaseActionType.Insert:
                    break;
                case Utility.DataBaseActionType.Select:
                default:
                    break;
            }
        }
        private void IsValid(Policy.InvestProfilePersonalized investProfilePersonalized, Utility.DataBaseActionType action)
        {
            if (investProfilePersonalized.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (investProfilePersonalized.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (investProfilePersonalized.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (investProfilePersonalized.DomesticregId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticregId");
            if (investProfilePersonalized.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (investProfilePersonalized.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (investProfilePersonalized.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (investProfilePersonalized.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (investProfilePersonalized.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");
            if (investProfilePersonalized.ProfileTypeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ProfileTypeId");
            if (!investProfilePersonalized.InvestProductDateId.HasValue || investProfilePersonalized.InvestProductDateId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "InvestProductDateId");
            if (investProfilePersonalized.SymbolId <= 0)
                throw new ArgumentException("This property can't be under 0.", "SymbolId");
            if (investProfilePersonalized.UserId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UserId");

            switch (action)
            {
                case Utility.DataBaseActionType.Update:
                case Utility.DataBaseActionType.Delete:
                case Utility.DataBaseActionType.Insert:
                case Utility.DataBaseActionType.Select:
                default:
                    break;
            }
        }
        #endregion

        #region Rating
        public virtual IEnumerable<Policy.OverPricePercentage> GetOverPricePercentage(Policy.RiskRatingCondition condition)
        {
            IEnumerable<Policy.OverPricePercentage> tempResult, result;

            if (condition.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (condition.RiskGroupId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RiskGroupId");
            if (condition.RiskDetId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RiskDetId");
            if (condition.PageId <= 0)
                throw new ArgumentException("This property can't be under 0.", "PageId");
            if (condition.GridId <= 0)
                throw new ArgumentException("This property can't be under 0.", "GridId");
            //if (condition.ElementId <= 0)
            //    throw new ArgumentException("This property can't be under 0.", "ElementId");
            if (condition.ColumnId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ColumnId");
            if (condition.RiskTypeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RiskTypeId");

            tempResult = _policyRepository.GetOverPricePercentage(condition);

            result = tempResult.Select(p =>
            {
                p.MinValueIsNumeric = this.DecimalTryParse(p.MinValue);
                p.MaxValueIsNumeric = this.DecimalTryParse(p.MaxValue);

                return p;
            });

            return
                result;
        }

        public virtual int InsertRiskRating(IEnumerable<Policy.RiskRating> riskRatingList)
        {
            int result;
            Policy.RiskRating tempResult;

            if (riskRatingList != null && riskRatingList.Any())
                foreach (Policy.RiskRating riskRating in riskRatingList)
                    this.IsValid(riskRating, Utility.DataBaseActionType.Insert);
            else
                throw new ArgumentException("This parameter can't be null or empty.", "riskRatingList");

            result = -1;
            tempResult = null;

            foreach (Policy.RiskRating riskRating in riskRatingList)
            {
                if (tempResult == null)
                {
                    riskRating.SequenceReference = null;
                    riskRating.RiskId = null;
                    tempResult = _policyRepository.SetRiskRatingWithHeader(riskRating);
                    result = 1;
                }
                else
                {
                    riskRating.SequenceReference = tempResult.SequenceReference;
                    riskRating.RiskId = tempResult.RiskId;
                    _policyRepository.SetRiskRatingWithHeader(riskRating);
                }
            }


            return
                result;
        }
        public virtual int UpdateRiskRating(IEnumerable<Policy.RiskRating> riskRatingList)
        {
            int result;

            if (riskRatingList != null && riskRatingList.Any())
                foreach (Policy.RiskRating riskRating in riskRatingList)
                    this.IsValid(riskRating, Utility.DataBaseActionType.Update);
            else
                throw new ArgumentException("This parameter can't be null or empty.", "riskRatingList");

            result = -1;

            foreach (Policy.RiskRating riskRating in riskRatingList)
            {
                _policyRepository.SetRiskRatingWithHeader(riskRating);

                if (riskRating.RiskSelectionStatus.HasValue && !riskRating.RiskSelectionStatus.Value)
                    this.DeleteRiskRatingSelection(riskRating);
            }

            return
                result;
        }

        public virtual IEnumerable<Policy.RiskRating> GetRiskRating(Policy.RiskRating riskRating)
        {
            return
                _policyRepository.GetRiskRating(riskRating);
        }

        public virtual Policy.RiskRating TerminateExclusion(Policy.RiskRating riskRating)
        {
            int document_Id, docType_Id, docCategory_Id;

            if (riskRating.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (string.IsNullOrWhiteSpace(riskRating.SequenceReference))
                throw new ArgumentException("This property can't be null or whitespace.", "SequenceReference");
            if (!riskRating.RiskId.HasValue || riskRating.RiskId.Value <= 0)
                throw new ArgumentException("This property can't be null or under 0.", "RiskId");
            if (!riskRating.EndDate.HasValue || riskRating.EndDate.Value == default(DateTime))
                throw new ArgumentException("This property can't be null or the default value.", "EndDate");
            if (!riskRating.NotificationDate.HasValue || riskRating.NotificationDate.Value == default(DateTime))
                throw new ArgumentException("This property can't be null or the default value.", "NotificationDate");
            if (string.IsNullOrWhiteSpace(riskRating.Comment))
                throw new ArgumentException("This property can't be null or whitespace.", "Comment");
            if (riskRating.UserId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UserId");

            //riskRating.CorpId = null;
            //riskRating.SequenceReference = null;
            //riskRating.RiskId = null;
            riskRating.SuggestedRating = null;
            riskRating.TableRating = null;
            riskRating.PerThousandRating = null;
            riskRating.StartDate = null;
            riskRating.Duration = null;
            //riskRating.NotificationDate = null;
            riskRating.RequestedBy = null;
            //riskRating.EndDate = null;
            riskRating.RiskRateStatusId = 4;
            riskRating.YearToReconsider = null;
            riskRating.ReconsiderDate = null;
            //riskRating.Comment = null;
            //riskRating.UserId = null;        

            if (riskRating.DocumentBinary != null && riskRating.DocumentBinary.Length > 0)
            {
                docType_Id = 1;
                docCategory_Id = 171;
                document_Id = -1;

                document_Id = this.SetDocument(
                        docTypeId: docType_Id,
                        docCategoryId: docCategory_Id,
                        documentId: document_Id,
                        documentBinary: riskRating.DocumentBinary,
                        documentName: !string.IsNullOrWhiteSpace(riskRating.DocumentName) ? riskRating.DocumentName.Trim() : "Exclusion",
                        creationDate: DateTime.Now,
                        expireDate: null,
                        userId: riskRating.UserId
                    );

                riskRating.DocTypeId = docType_Id;
                riskRating.DocCategoryId = docCategory_Id;
                riskRating.DocumentId = document_Id;
            }
            else
            {
                riskRating.DocTypeId = null;
                riskRating.DocCategoryId = null;
                riskRating.DocumentId = null;
            }

            return
                _policyRepository.SetRiskRating(riskRating);
        }

        public virtual IEnumerable<Policy.RiskRating> GetRiskRatingSelection(Policy.RiskRating riskRating)
        {
            return
                _policyRepository.GetRiskRatingSelection(riskRating);
        }

        public virtual int DeleteRiskRating(Policy.RiskRating riskRating)
        {
            if (riskRating.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (string.IsNullOrWhiteSpace(riskRating.SequenceReference))
                throw new ArgumentException("This property can't be null or whitespace.", "SequenceReference");
            if (!riskRating.RiskId.HasValue || riskRating.RiskId.Value <= 0)
                throw new ArgumentException("This property can't be null or under 0.", "RiskId");

            return
                _policyRepository.DeleteRiskRating(riskRating);
        }

        public virtual int SetDocument(int docTypeId, int docCategoryId, int documentId, byte[] documentBinary, string documentName, DateTime creationDate
          , DateTime? expireDate, int userId)
        {
            return
                _policyRepository.SetDocument(docTypeId, docCategoryId, documentId, documentBinary, documentName, creationDate, expireDate, userId);
        }
        private int DeleteRiskRatingSelection(Policy.RiskRating riskRating)
        {
            if (riskRating.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (string.IsNullOrWhiteSpace(riskRating.SequenceReference))
                throw new ArgumentException("This property can't be null or whitespace.", "SequenceReference");
            if (riskRating.RiskId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RiskId");
            if (riskRating.RiskTypeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RiskTypeId");
            if (riskRating.RiskGroupId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RiskGroupId");
            if (riskRating.RiskDetId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RiskDetId");
            if (riskRating.PageId <= 0)
                throw new ArgumentException("This property can't be under 0.", "PageId");
            if (riskRating.GridId <= 0)
                throw new ArgumentException("This property can't be under 0.", "GridId");
            if (riskRating.ElementId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ElementId");
            if (riskRating.ColumnId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ColumnId");
            if (riskRating.UserId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UserId");

            return
                _policyRepository.DeleteRiskRatingSelection(riskRating);
        }
        private Policy.RiskRating SetRiskRating(Policy.RiskRating riskRating)
        {
            return
                _policyRepository.SetRiskRating(riskRating);
        }
        private bool DecimalTryParse(string value)
        {
            bool result;
            decimal tempValue;

            result = decimal.TryParse(value, out tempValue);

            return
                result;
        }
        private void IsValid(Policy.RiskRating riskRating, Utility.DataBaseActionType action)
        {
            if (riskRating.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (riskRating.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (riskRating.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (riskRating.DomesticRegId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticRegId");
            if (riskRating.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (riskRating.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (riskRating.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (riskRating.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (riskRating.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");
            if (riskRating.ContactId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ContactId");
            if (riskRating.ContactRoleTypeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "ContactRoleTypeId");
            if (riskRating.OperationId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OperationId");
            if (riskRating.UserId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UserId");

            switch (action)
            {
                case Utility.DataBaseActionType.Update:
                case Utility.DataBaseActionType.Delete:
                    if (string.IsNullOrWhiteSpace(riskRating.SequenceReference))
                        throw new ArgumentException("This property can't be null or whitespace.", "SequenceReference");
                    if (!riskRating.RiskId.HasValue || riskRating.RiskId.Value <= 0)
                        throw new ArgumentException("This property can't be null or under 0.", "RiskId");
                    break;
                case Utility.DataBaseActionType.Insert:
                    break;
                case Utility.DataBaseActionType.Select:
                default:
                    break;
            }
        }
        #endregion

        #region SendToReinsurance
        public virtual IEnumerable<Reinsurance.Communication> GetReinsuranceCommunication(Reinsurance.Communication comm)
        {
            return
                _policyRepository.GetReinsuranceCommunication(comm);
        }
        public virtual IEnumerable<Reinsurance.Communication> GetReinsuranceCommunicationHtmlAndAttachments(Reinsurance.Communication comm)
        {
            return
                 _policyRepository.GetReinsuranceCommunicationHtmlAndAttachments(comm);
        }

        public virtual Reinsurance.Communication InsertReinsuranceCommunication(Reinsurance.Communication comm)
        {
            this.IsValid(comm, Utility.DataBaseActionType.Insert);
            comm.CommunicationId = -1;

            return
                this.SetReinsuranceCommunication(comm);
        }
        public virtual Reinsurance.Communication UpdateReinsuranceCommunication(Reinsurance.Communication comm)
        {
            this.IsValid(comm, Utility.DataBaseActionType.Update);

            return
                this.SetReinsuranceCommunication(comm);
        }

        public virtual Reinsurance.Communication SetReinsuranceCommunicationAttachment(IEnumerable<Reinsurance.Communication> comms)
        {
            Reinsurance.Communication result;

            result = null;

            if (comms != null && comms.Any())
                foreach (Reinsurance.Communication comm in comms)
                    result = this.SetReinsuranceCommunicationAttachment(comm);

            return
                result;
        }
        public virtual Reinsurance.Communication SetReinsuranceCommunicationAttachment(Reinsurance.Communication comm)
        {
            if (comm.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (comm.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (comm.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (comm.DomesticRegId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticRegId");
            if (comm.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (comm.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (comm.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (comm.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (comm.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");
            if (comm.StepTypeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StepTypeId");
            if (comm.StepId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StepId");
            if (comm.StepCaseNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "StepCaseNo");
            if (!comm.CommunicationId.HasValue || comm.CommunicationId.Value <= 0)
                throw new ArgumentException("This property can't be null or under 0.", "CommunicationId");
            if (!comm.DocTypeId.HasValue || comm.DocTypeId.Value <= 0)
                throw new ArgumentException("This property can't be null or under 0.", "DocTypeId");
            if (!comm.DocCategoryId.HasValue || comm.DocCategoryId.Value <= 0)
                throw new ArgumentException("This property can't be null or under 0.", "DocCategoryId");
            if (!comm.DocumentId.HasValue || comm.DocumentId.Value <= 0)
                throw new ArgumentException("This property can't be null or under 0.", "DocumentId");
            if (comm.UserId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UserId");

            return
                _policyRepository.SetReinsuranceCommunicationAttachment(comm);
        }

        public virtual Reinsurance.StepAvailable GetStepAvailable(Reinsurance.StepAvailable step)
        {
            this.IsValid(step, Utility.DataBaseActionType.Select);

            return
                _policyRepository.GetStepAvailable(step);
        }
        public virtual Reinsurance.StepAvailable GetStepAvailable(string stepSeqReference)
        {
            if (string.IsNullOrWhiteSpace(stepSeqReference))
                throw new ArgumentException("This property can't either be null or whitespace.", "stepSeqReference");

            stepSeqReference = stepSeqReference.Trim();

            return
                _policyRepository.GetStepAvailable(stepSeqReference);
        }

        public virtual decimal GetDocumentSize(int docTypeId, int docCategoryId, int documentId)
        {
            if (docTypeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "docTypeId");
            if (docCategoryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "docCategoryId");
            if (documentId <= 0)
                throw new ArgumentException("This property can't be under 0.", "documentId");

            return
                _policyRepository.GetDocumentSize(docTypeId, docCategoryId, documentId);
        }

        private Reinsurance.Communication SetReinsuranceCommunication(Reinsurance.Communication comm)
        {
            return
                _policyRepository.SetReinsuranceCommunication(comm);
        }
        private void IsValid(Reinsurance.Communication comm, Utility.DataBaseActionType action)
        {
            if (comm.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (comm.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (comm.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (comm.DomesticRegId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticRegId");
            if (comm.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (comm.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (comm.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (comm.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (comm.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");
            if (comm.StepTypeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StepTypeId");
            if (comm.StepId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StepId");
            if (comm.StepCaseNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "StepCaseNo");
            if (comm.UserId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UserId");

            switch (action)
            {
                case Utility.DataBaseActionType.Update:
                case Utility.DataBaseActionType.Delete:
                    if (!comm.CommunicationId.HasValue || comm.CommunicationId.Value <= 0)
                        throw new ArgumentException("This property can't be null or under 0.", "CommunicationId");
                    break;
                case Utility.DataBaseActionType.Insert:
                    break;
                case Utility.DataBaseActionType.Select:
                default:
                    break;
            }
        }
        private void IsValid(Reinsurance.StepAvailable step, Utility.DataBaseActionType action)
        {
            if (step.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (step.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (step.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (step.DomesticRegId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticRegId");
            if (step.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (step.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (step.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (step.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (step.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");

            //switch (action)
            //{
            //    case Utility.DataBaseActionType.Update:
            //    case Utility.DataBaseActionType.Delete:
            //        break;
            //    case Utility.DataBaseActionType.Insert:
            //        break;
            //    case Utility.DataBaseActionType.Select:
            //    default:
            //        break;
            //}
        }
        #endregion

        public virtual IEnumerable<Policy.Form> GetFormPolicyContact(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
            , int officeId, int caseSeqNo, int histSeqNo, int contactId, int formId, int languageId)
        {
            return
                 _policyRepository.GetFormPolicyContact(
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

        #region BackgroundCheck
        public virtual Policy.BackgroundCheck InsertBackgroundCheck(Policy.BackgroundCheck backgroundCheck)
        {
            this.IsValid(backgroundCheck, Utility.DataBaseActionType.Insert);

            backgroundCheck.BackgroundCheckId = -1;

            return
                this.SetBackgroundCheck(backgroundCheck);
        }
        public virtual Policy.BackgroundCheck UpdateBackgroundCheck(Policy.BackgroundCheck backgroundCheck)
        {
            this.IsValid(backgroundCheck, Utility.DataBaseActionType.Update);

            return
                this.SetBackgroundCheck(backgroundCheck);
        }
        public virtual void SetBackgroundCheckAndCloseStep(Policy.BackgroundCheck backgroundCheck)
        {
            Policy.Parameter policyParameter;
            IEnumerable<Step> pendingSteps;
            Step.Note note;

            this.IsValid(backgroundCheck, Utility.DataBaseActionType.Insert);
            this.SetBackgroundCheck(backgroundCheck);

            policyParameter = new Policy.Parameter
            {
                CorpId = backgroundCheck.CorpId,
                RegionId = backgroundCheck.RegionId,
                CountryId = backgroundCheck.CountryId,
                DomesticregId = backgroundCheck.DomesticRegId,
                StateProvId = backgroundCheck.StateProvId,
                CityId = backgroundCheck.CityId,
                OfficeId = backgroundCheck.OfficeId,
                CaseSeqNo = backgroundCheck.CaseSeqNo,
                HistSeqNo = backgroundCheck.HistSeqNo
            };

            if (this.CheckBackgroundCheckComplete(policyParameter))
            {
                pendingSteps = this.GetBackgroundCheckPending(policyParameter);

                if (pendingSteps != null && pendingSteps.Any())
                {
                    foreach (Step step in pendingSteps)
                    {
                        if (backgroundCheck.ExtraInfoList != null && backgroundCheck.ExtraInfoList.Any())
                        {
                            foreach (Step.ExtraInfo extraInfo in backgroundCheck.ExtraInfoList)
                            {
                                switch (extraInfo.StepExtraInfoTypeId)
                                {
                                    case 1:
                                        _stepManager.InsertDocumentExtraInfo(extraInfo);
                                        break;
                                    case 2:
                                        _stepManager.InsertLinkExtraInfo(extraInfo);
                                        break;
                                    default:
                                        _stepManager.SetExtraInfo(extraInfo);
                                        break;
                                }
                            }
                        }


                        note = new Step.Note
                        {
                            CorpId = step.CorpId,
                            RegionId = step.RegionId,
                            CountryId = step.CountryId,
                            DomesticregId = step.DomesticregId,
                            StateProvId = step.StateProvId,
                            CityId = step.CityId,
                            OfficeId = step.OfficeId,
                            CaseSeqNo = step.CaseSeqNo,
                            HistSeqNo = step.HistSeqNo,
                            StepTypeId = step.StepTypeId,
                            StepId = step.StepId,
                            StepCaseNo = step.StepCaseNo,
                            NoteId = -1,
                            ContactId = step.ContactId,
                            ContactRoleTypeId = step.ContactRoleTypeId,
                            NoteTypeId = null,
                            ReferenceId = null,
                            NoteName = "BackGroundCheck-Note",
                            DateAdded = DateTime.Now,
                            DateModified = null,
                            OriginatedBy = backgroundCheck.UserId,
                            NoteDesc = backgroundCheck.Comments,
                            UnderwriterId = null,
                            UserId = backgroundCheck.UserId
                        };
                        this._stepManager.InsertNote(note);
                        this._stepManager.CloseStep(step);
                    }
                }
            }
        }
        private IEnumerable<Step> GetBackgroundCheckPending(Policy.Parameter parameter)
        {
            parameter.UnderwriterId = 1;
            this.IsValid(parameter, Utility.DataBaseActionType.Select);

            return
                 _policyRepository.GetBackgroundCheckPending(parameter);
        }
        private bool CheckBackgroundCheckComplete(Policy.Parameter parameter)
        {
            parameter.UnderwriterId = 1;
            this.IsValid(parameter, Utility.DataBaseActionType.Select);


            return
                 _policyRepository.CheckBackgroundCheckComplete(parameter);
        }
        #endregion

        public virtual Requirement.Document GetIdCopyRequirement(Requirement requirement)
        {
            return
                _policyRepository.GetIdCopyRequirement(requirement);
        }

        public virtual bool CheckPolicyMedical(Policy.Parameter parameter)
        {
            parameter.UnderwriterId = parameter.UnderwriterId > 0 ? parameter.UnderwriterId : 1;
            this.IsValid(parameter, Utility.DataBaseActionType.Select);

            return
                 _policyRepository.CheckPolicyMedical(parameter);
        }

        public virtual int GenerateTempPolicyNo(Policy.Parameter parameter)
        {
            this.IsValid(parameter, Utility.DataBaseActionType.Update);

            return
                  _policyRepository.GenerateTempPolicyNo(parameter);
        }

        public virtual IEnumerable<Policy.Contact.Action> GetPolicyAction(Policy.Parameter parameter)
        {
            return
                _policyRepository.GetPolicyAction(parameter);
        }

        public virtual int ChangePolicyChain(Policy.Parameter parameter)
        {
            if (!parameter.AgentId.HasValue || parameter.AgentId.Value <= 0)
                throw new ArgumentException(_msg, "AgentId");
            if (!parameter.UserId.HasValue || parameter.UserId.Value <= 0)
                throw new ArgumentException(_msg, "UserId");

            parameter.UnderwriterId = parameter.UserId.Value;
            this.IsValid(parameter, Utility.DataBaseActionType.Update);

            return
                _policyRepository.ChangePolicyChain(parameter);
        }

        public virtual Policy.Parameter GetPolicyKey(string policyNo)
        {
            if (string.IsNullOrWhiteSpace(policyNo))
                throw new ArgumentException("This property can't be null or whitespace.", "policyNo");

            return
                _policyRepository.GetPolicyKey(policyNo);
        }

        public virtual Agent.Key GetAgentId(string nameId)
        {
            if (string.IsNullOrWhiteSpace(nameId))
                throw new ArgumentException("This property can't be null or whitespace.", "nameId");

            return
                _policyRepository.GetAgentId(nameId);
        }

        public virtual IEnumerable<Policy.VehicleInsured> GetVehicleInsured(Policy.Parameter parameter)
        {
            return
                 _policyRepository.GetVehicleInsured(parameter);
        }

        public virtual IEnumerable<Policy.VehicleCoverage> GetVehicleCoverage(Policy.VehicleCoverageGet parameter)
        {
            return
                  _policyRepository.GetVehicleCoverage(parameter);
        }

        public virtual Policy.VehicleCoverage SetVehicleCoverage(Policy.VehicleCoverage vehicleCoverage)
        {
            return
                  _policyRepository.SetVehicleCoverage(vehicleCoverage);
        }

        public virtual Policy.VehicleInsured SetVehicleInsured(Policy.VehicleInsured vehicleInsured)
        {
            return
                 _policyRepository.SetVehicleInsured(vehicleInsured);
        }

        #region Private Method
        private Policy.BackgroundCheck SetBackgroundCheck(Policy.BackgroundCheck backgroundCheck)
        {
            return
                _policyRepository.SetBackgroundCheck(backgroundCheck);
        }

        private void IsValid(Policy.PaymentFrequency paymentFreq, Utility.DataBaseActionType action)
        {
            bool checkUserId;

            if (paymentFreq.CorpId <= 0)
                throw new ArgumentException(_msg, "CorpId");
            if (paymentFreq.RegionId <= 0)
                throw new ArgumentException(_msg, "RegionId");
            if (paymentFreq.CountryId <= 0)
                throw new ArgumentException(_msg, "CountryId");
            if (paymentFreq.DomesticregId <= 0)
                throw new ArgumentException(_msg, "DomesticregId");
            if (paymentFreq.StateProvId <= 0)
                throw new ArgumentException(_msg, "StateProvId");
            if (paymentFreq.CityId <= 0)
                throw new ArgumentException(_msg, "CityId");
            if (paymentFreq.OfficeId <= 0)
                throw new ArgumentException(_msg, "OfficeId");
            if (paymentFreq.CaseSeqNo <= 0)
                throw new ArgumentException(_msg, "CaseSeqNo");
            if (paymentFreq.HistSeqNo <= 0)
                throw new ArgumentException(_msg, "HistSeqNo");

            switch (action)
            {
                case Utility.DataBaseActionType.Update:
                case Utility.DataBaseActionType.Delete:
                    checkUserId = true;
                    if (!paymentFreq.PaymentFreqTypeId.HasValue || paymentFreq.PaymentFreqTypeId.Value <= 0)
                        throw new ArgumentException("This property can't be under 0 or null.", "PaymentFreqTypeId");
                    if (!paymentFreq.PaymentFreqId.HasValue || paymentFreq.PaymentFreqId <= 0)
                        throw new ArgumentException("This property can't be under 0 or null.", "PaymentFreqId");
                    break;
                case Utility.DataBaseActionType.Insert:
                    checkUserId = true;
                    break;
                case Utility.DataBaseActionType.Select:
                default:
                    checkUserId = false;
                    break;
            }

            if (checkUserId && paymentFreq.UserId <= 0)
                throw new ArgumentException(_msg, "UserId");
        }
        private void IsValid(Policy.Call call, Utility.DataBaseActionType action)
        {
            bool checkUserId;

            if (call.CorpId <= 0)
                throw new ArgumentException(_msg, "CorpId");
            if (call.RegionId <= 0)
                throw new ArgumentException(_msg, "RegionId");
            if (call.CountryId <= 0)
                throw new ArgumentException(_msg, "CountryId");
            if (call.DomesticregId <= 0)
                throw new ArgumentException(_msg, "DomesticregId");
            if (call.StateProvId <= 0)
                throw new ArgumentException(_msg, "StateProvId");
            if (call.CityId <= 0)
                throw new ArgumentException(_msg, "CityId");
            if (call.OfficeId <= 0)
                throw new ArgumentException(_msg, "OfficeId");
            if (call.CaseSeqNo <= 0)
                throw new ArgumentException(_msg, "CaseSeqNo");
            if (call.HistSeqNo <= 0)
                throw new ArgumentException(_msg, "HistSeqNo");
            if (call.CommunicationTypeId <= 0)
                throw new ArgumentException(_msg, "CommunicationTypeId");

            switch (action)
            {
                case Utility.DataBaseActionType.Update:
                case Utility.DataBaseActionType.Delete:
                    checkUserId = true;
                    if (call.CallId <= 0)
                        throw new ArgumentException(_msg, "CallId");
                    break;
                case Utility.DataBaseActionType.Insert:
                    checkUserId = true;
                    break;
                case Utility.DataBaseActionType.Select:
                default:
                    checkUserId = false;
                    break;
            }

            if (checkUserId && call.UserId <= 0)
                throw new ArgumentException(_msg, "UserId");
        }
        private void IsValid(Policy.Parameter policy, Utility.DataBaseActionType action)
        {
            // bool checkUserId;

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
            if (policy.UnderwriterId <= 0)
                throw new ArgumentException(_msg, "UnderwriterId");

            //checkUserId = false;

            //switch (action)
            //{
            //    case Utility.DataBaseActionType.Update:
            //    case Utility.DataBaseActionType.Delete:
            //        checkUserId = true;
            //        if (policy.CallId <= 0)
            //            throw new ArgumentException(_msg, "CallId");
            //        break;
            //    case Utility.DataBaseActionType.Insert:
            //        checkUserId = true;
            //        break;
            //    case Utility.DataBaseActionType.Select:
            //    default:
            //        break;
            //}

            //if (checkUserId && policy.UserId <= 0)
            //    throw new ArgumentException(_msg, "UserId");
        }
        private void IsValid(Policy.BackgroundCheck backgroundCheck, Utility.DataBaseActionType action)
        {
            bool checkUserId;

            if (backgroundCheck.CorpId <= 0)
                throw new ArgumentException(_msg, "CorpId");
            if (backgroundCheck.RegionId <= 0)
                throw new ArgumentException(_msg, "RegionId");
            if (backgroundCheck.CountryId <= 0)
                throw new ArgumentException(_msg, "CountryId");
            if (backgroundCheck.DomesticRegId <= 0)
                throw new ArgumentException(_msg, "DomesticRegId");
            if (backgroundCheck.StateProvId <= 0)
                throw new ArgumentException(_msg, "StateProvId");
            if (backgroundCheck.CityId <= 0)
                throw new ArgumentException(_msg, "CityId");
            if (backgroundCheck.OfficeId <= 0)
                throw new ArgumentException(_msg, "OfficeId");
            if (backgroundCheck.CaseSeqNo <= 0)
                throw new ArgumentException(_msg, "CaseSeqNo");
            if (backgroundCheck.HistSeqNo <= 0)
                throw new ArgumentException(_msg, "HistSeqNo");
            if (backgroundCheck.ContactId <= 0)
                throw new ArgumentException(_msg, "ContactId");

            switch (action)
            {
                case Utility.DataBaseActionType.Update:
                case Utility.DataBaseActionType.Delete:
                    checkUserId = true;
                    if (!backgroundCheck.BackgroundCheckId.HasValue || backgroundCheck.BackgroundCheckId.Value <= 0)
                        throw new ArgumentException("This property can't be under 0 or null.", "BackgroundCheckId");
                    break;
                case Utility.DataBaseActionType.Insert:
                    checkUserId = true;
                    break;
                case Utility.DataBaseActionType.Select:
                default:
                    checkUserId = false;
                    break;
            }

            if (checkUserId && backgroundCheck.UserId <= 0)
                throw new ArgumentException(_msg, "UserId");
        }

        private Policy.Parameter ConvertPolicyParameters(
              int? coprId = null, int? regionId = null, int? countryId = null, int? domesticRegId = null
            , int? stateProvId = null, int? cityId = null, int? officeId = null, int? caseSeqNo = null
            , int? histSeqNo = null, int? underwriterId = null, int? languageId = null, int? agentId = null
            , int? userId = null)
        {
            Policy.Parameter policyParameter;

            policyParameter = new Policy.Parameter
            {
                CorpId = coprId.GetValueOrDefault(),
                RegionId = regionId.GetValueOrDefault(),
                CountryId = countryId.GetValueOrDefault(),
                DomesticregId = domesticRegId.GetValueOrDefault(),
                StateProvId = stateProvId.GetValueOrDefault(),
                CityId = cityId.GetValueOrDefault(),
                OfficeId = officeId.GetValueOrDefault(),
                CaseSeqNo = caseSeqNo.GetValueOrDefault(),
                HistSeqNo = histSeqNo.GetValueOrDefault(),
                UnderwriterId = underwriterId.GetValueOrDefault(),
                LanguageId = languageId.GetValueOrDefault(),
                AgentId = agentId,
                UserId = userId
            };


            return
               policyParameter;
        }

        #endregion

        public virtual IEnumerable<Policy.StatusChange> SetPolicyStatus(Policy.Parameter parameter)
        {
            if (!parameter.StatusChangeTypeId.HasValue || parameter.StatusChangeTypeId.Value <= 0)
                throw new ArgumentException(_msg, "StatusChangeTypeId");
            if (!parameter.StatusId.HasValue || parameter.StatusId.Value <= 0)
                throw new ArgumentException(_msg, "StatusId");
            if (!parameter.UserId.HasValue || parameter.UserId.Value <= 0)
                throw new ArgumentException(_msg, "UserId");

            parameter.UnderwriterId = parameter.UserId.Value;
            this.IsValid(parameter, Utility.DataBaseActionType.Update);

            return
                _policyRepository.SetPolicyStatus(parameter);
        }

        public virtual Policy NewPolicyWithoutAgent(Case.NewCase newCase)
        {
            if (newCase.CorpId <= 0)
                throw new ArgumentException(_msg, "CorpId");
            if (newCase.RegionId <= 0)
                throw new ArgumentException(_msg, "RegionId");
            if (newCase.CountryId <= 0)
                throw new ArgumentException(_msg, "CountryId");
            if (newCase.DomesticregId <= 0)
                throw new ArgumentException(_msg, "DomesticregId");
            if (newCase.StateProvId <= 0)
                throw new ArgumentException(_msg, "StateProvId");
            if (newCase.CityId <= 0)
                throw new ArgumentException(_msg, "CityId");
            if (newCase.OfficeId <= 0)
                throw new ArgumentException(_msg, "OfficeId");
            if (newCase.UserId <= 0)
                throw new ArgumentException(_msg, "UserId");

            return
                _policyRepository.NewPolicyWithoutAgent(newCase);
        }

        public virtual IEnumerable<Policy.VehiclesCoverage> GetVehiclesCoverage(Policy.Parameter policyParameter)
        {
            if (policyParameter.CorpId <= 0)
                throw new ArgumentException(_msg, "CorpId");
            if (policyParameter.RegionId <= 0)
                throw new ArgumentException(_msg, "RegionId");
            if (policyParameter.CountryId <= 0)
                throw new ArgumentException(_msg, "CountryId");
            if (policyParameter.DomesticregId <= 0)
                throw new ArgumentException(_msg, "DomesticregId");
            if (policyParameter.StateProvId <= 0)
                throw new ArgumentException(_msg, "StateProvId");
            if (policyParameter.CityId <= 0)
                throw new ArgumentException(_msg, "CityId");
            if (policyParameter.OfficeId <= 0)
                throw new ArgumentException(_msg, "OfficeId");
            if (policyParameter.CaseSeqNo <= 0)
                throw new ArgumentException(_msg, "CaseSeqNo");
            if (policyParameter.HistSeqNo <= 0)
                throw new ArgumentException(_msg, "HistSeqNo");

            return
                 _policyRepository.GetVehiclesCoverage(policyParameter);
        }

        public virtual IEnumerable<Policy.CRBS> GetCRBS(Policy.CRBSParameter parameters)
        {
            return
                _policyRepository.GetCRBS(parameters);
        }

        #region Discount
        public virtual IEnumerable<Policy.Vehicle.Discount> GetPolicyVehicleDiscount(Policy.DVParameter parameter)
        {
            return
                _policyRepository.GetPolicyVehicleDiscount(parameter);
        }
        public virtual IEnumerable<Policy.Vehicle.Discount.RulesAndDetails> GetDiscountRulesAndDetails(Policy.DParameter parameter)
        {
            return
                _policyRepository.GetDiscountRulesAndDetails(parameter);
        }

        public virtual Policy.Vehicle.Discount SetPolicyVehicleDiscount(Policy.Vehicle.Discount parameter)
        {
            return
                _policyRepository.SetPolicyVehicleDiscount(parameter);
        }
        #endregion

        public virtual IEnumerable<Policy.Agent.SaleChannelInfo> GetAgentSaleChannelInfo(Policy.Parameter parameter)
        {
            return
                _policyRepository.GetAgentSaleChannelInfo(parameter);
        }

        public virtual IEnumerable<Policy.VehicleCoverageSurcharge> GetVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            return
                _policyRepository.GetVehicleCoverageSurcharge(parameter);
        }

        public virtual Policy.VehicleCoverageSurcharge SetVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            return
                _policyRepository.SetVehicleCoverageSurcharge(parameter);
        }

        public virtual IEnumerable<Policy.Document> GetPolicyDocument(Policy.Document parameter)
        {
            return
                _policyRepository.GetPolicyDocument(parameter);
        }

        public virtual Policy.Document SetPolicyDocument(Policy.Document parameter)
        {
            return
                _policyRepository.SetPolicyDocument(parameter);
        }

        public virtual int SetPolicyNo(Policy.Number parameter)
        {
            return
                _policyRepository.SetPolicyNo(parameter);
        }

        public virtual int? SetPolicyLoanNo(int? corp_Id, int? region_Id, int? country_Id, int? domesticreg_Id, int? state_Prov_Id, int? city_Id, int? office_Id, int? case_Seq_No, int? hist_Seq_No, string loanPetitionNo, int? userId)
        {
            return
                _policyRepository.SetPolicyLoanNo(corp_Id, region_Id, country_Id, domesticreg_Id, state_Prov_Id, city_Id, office_Id, case_Seq_No, hist_Seq_No, loanPetitionNo, userId);
        }

        public virtual int SetPaymentFrequency(Policy.PaymentFrequency paymentFreq)
        {
            return
                _policyRepository.SetPaymentFrequency(paymentFreq);
        }
        public virtual int SetPolicy(Policy policy)
        {
            return
                _policyRepository.SetPolicy(policy);
        }

        public virtual IEnumerable<Policy.VehicleInsured.CoverageTypePremiun> GetVehicleInsuredCoverageTypePremiun(Policy.VehicleInsured.CoverageTypePremiun.Key parameter)
        {
            return
                _policyRepository.GetVehicleInsuredCoverageTypePremiun(parameter);
        }
        public virtual Policy.VehicleInsured.CoverageTypePremiun.Key SetVehicleInsuredCoverageTypePremiun(Policy.VehicleInsured.CoverageTypePremiun parameter)
        {
            return
                _policyRepository.SetVehicleInsuredCoverageTypePremiun(parameter);
        }

        public virtual IEnumerable<Policy.VehicleInsured.Discount> GetVehicleInsuredDiscount(Policy.VehicleInsured.Discount.Key parameter)
        {
            return
                _policyRepository.GetVehicleInsuredDiscount(parameter);
        }
        public virtual Policy.VehicleInsured.Discount.Key SetVehicleInsuredDiscount(Policy.VehicleInsured.Discount parameter)
        {
            return
                _policyRepository.SetVehicleInsuredDiscount(parameter);
        }

        public virtual IEnumerable<Policy.DocumentQuotation> GetQuotationDocumentReview(Policy.Parameter parameter)
        {
            return
                _policyRepository.GetQuotationDocumentReview(parameter);
        }

        public virtual int SetVehicleInsuredInspection(Policy.VehicleInsured.InspectionV parameter)
        {
            return
                _policyRepository.SetVehicleInsuredInspection(parameter);
        }

        public virtual int SetVehicleInsuredApplyDiscountAndSurcharge(Policy.Parameter parameter)
        {
            return
                _policyRepository.SetVehicleInsuredApplyDiscountAndSurcharge(parameter);
        }

        public virtual int DeleteVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            return
                _policyRepository.DeleteVehicleCoverageSurcharge(parameter);
        }

        public virtual int UpdateVehicleCoverageSurcharge(Policy.VehicleCoverageSurcharge parameter)
        {
            return
                _policyRepository.UpdateVehicleCoverageSurcharge(parameter);
        }

        public virtual IEnumerable<Policy.Vehicle.Requirement> GetVehicleRequirement(Policy.Parameter parameter)
        {
            return
                _policyRepository.GetVehicleRequirement(parameter);
        }

        public virtual int SetAssingQuotation(Policy.Parameter parameter)
        {
            return
                _policyRepository.SetAssingQuotation(parameter);
        }

        public virtual int UpdateQuotationInfoTemp(Policy.Quo.Temp parameter)
        {
            if (parameter.UserId <= 0)
                throw new ArgumentException(_msg, "UserId");

            if (string.IsNullOrWhiteSpace(parameter.PolicyNo))
                parameter.PolicyNo = null;

            parameter.ReturnResultSet = true;

            return
                _policyRepository.UpdateQuotationInfoTemp(parameter);
        }

        public virtual Policy.Quo.Temp.TempResult UpdateOneQuotationInfoTemp(Policy.Quo.Temp parameter)
        {
            if (parameter.UserId <= 0)
                throw new ArgumentException(_msg, "UserId");

            if (string.IsNullOrWhiteSpace(parameter.PolicyNo))
                parameter.PolicyNo = null;

            parameter.ReturnResultSet = true;

            return
                _policyRepository.UpdateOneQuotationInfoTemp(parameter);
        }

        public virtual IEnumerable<Policy.Quo> GetQuotationInfoTemp(Policy.Quo.Temp parameter)
        {
            return
                _policyRepository.GetQuotationInfoTemp(parameter);
        }

        public virtual int SetPolicySourceId(Policy.PSourceId parameter)
        {
            return
                _policyRepository.SetPolicySourceId(parameter);
        }

        public virtual IEnumerable<Policy.Agent> GetAgentIdListByAgentId(Policy.Agent parameter)
        {
            return
                _policyRepository.GetAgentIdListByAgentId(parameter);
        }

        public virtual IEnumerable<Policy.LogResult> InsertLog(Policy.LogParameter parameter)
        {
            return
                _policyRepository.InsertLog(parameter);
        }

        public virtual int UpdatePolicyEffectiveDate(Policy.OEffectiveDate parameter)
        {
            Policy.Parameter policyParameter;
            Policy tempResult;

            if (parameter.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (parameter.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (parameter.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (parameter.DomesticRegId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticregId");
            if (parameter.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (parameter.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (parameter.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (parameter.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (parameter.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");

            policyParameter = this.ConvertPolicyParameters(
                parameter.CorpId,
                parameter.RegionId,
                parameter.CountryId,
                parameter.DomesticRegId,
                parameter.StateProvId,
                parameter.CityId,
                parameter.OfficeId,
                parameter.CaseSeqNo,
                parameter.HistSeqNo);

            tempResult = _policyRepository.GetPolicy(policyParameter);
            tempResult.PolicyEffectiveDate = parameter.EffectiveDate;

            return
                 _policyRepository.SetPolicy(tempResult);
        }

        public virtual int UpdatePolicyExpirationDate(Policy.OExpirationDate parameter)
        {
            Policy.Parameter policyParameter;
            Policy tempResult;

            if (parameter.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (parameter.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (parameter.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (parameter.DomesticRegId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticregId");
            if (parameter.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (parameter.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (parameter.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (parameter.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (parameter.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");

            policyParameter = this.ConvertPolicyParameters(
                parameter.CorpId,
                parameter.RegionId,
                parameter.CountryId,
                parameter.DomesticRegId,
                parameter.StateProvId,
                parameter.CityId,
                parameter.OfficeId,
                parameter.CaseSeqNo,
                parameter.HistSeqNo);

            tempResult = _policyRepository.GetPolicy(policyParameter);
            tempResult.ExpirationDate = parameter.ExpirationDate;

            return
                 _policyRepository.SetPolicy(tempResult);
        }

        public virtual Policy.UQuo UpdatePolicyQuo(Policy.UQuo parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter.PolicyNo))
                throw new ArgumentException("This property can't be null or whitespace.", "PolicyNo");
            if (parameter.UserId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UserId");

            return
                 _policyRepository.UpdatePolicyQuo(parameter);
        }

        public virtual IEnumerable<Policy.ConditionForSysflexIL> GetConditionIL(string QuotationNumber, long EntityId)
        {
            if (string.IsNullOrWhiteSpace(QuotationNumber))
                throw new ArgumentException("This property can't be null or whitespace.", "QuotationNumber");
            if (EntityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "EntityId");

            return
                _policyRepository.GetConditionIL(QuotationNumber, EntityId);
        }

        public virtual IEnumerable<Policy.Facultative.Contract> GetFacultativeContract(Policy.Facultative.Key parameters)
        {
            return
                _policyRepository.GetFacultativeContract(parameters);
        }

        public virtual DataTable GetFacultativeContractCoverage(Policy.Facultative.Key parameters)
        {
            return
                _policyRepository.GetFacultativeContractCoverage(parameters);
        }

        public virtual DataTable SetFacultativeContractCoverage(Policy.Facultative.SetContractCoverage parameters)
        {
            DataTable dt;

            if (parameters == null)
                throw new ArgumentException("This property can't be null.", "parameters");

            if (parameters.Coverages == null || !parameters.Coverages.Any())
                throw new ArgumentException("This property can't be null or empty.", "parameters.Coverages");

            dt = this.AsDataTable<Policy.Facultative.Coverage>(parameters.Coverages);

            return
                _policyRepository.SetFacultativeContractCoverage(parameters, dt);
        }

        private DataTable AsDataTable<T>(IEnumerable<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public virtual int DeleteFacultativeContract(int corpId, long contractUniqueId, int userId)
        {
            if (corpId <= 0)
                throw new ArgumentException(_msg, "corpId");
            if (contractUniqueId <= 0)
                throw new ArgumentException(_msg, "contractUniqueId");
            if (userId <= 0)
                throw new ArgumentException(_msg, "userId");

            return
                _policyRepository.DeleteFacultativeContract(corpId, contractUniqueId, userId);
        }

        public virtual IEnumerable<Policy.Facultative.Contract2> GetFacultativeContractCoverage2(Policy.Facultative.Key parameters)
        {
            return
               _policyRepository.GetFacultativeContractCoverage2(parameters);
        }

        //Add By Jheiron 29-05-2017
        public virtual IEnumerable<Policy.Agent.AgentSupervisor> GetAgentSupervisor(int corpId, int agentID)
        {
            return
               _policyRepository.GetAgentSupervisor(corpId, agentID);
        }

        public IEnumerable<Policy.PolicyContact> GetPolicyByContact(int? ContactId)
        {
            return
                _policyRepository.GetPolicyByContact(ContactId);
        }
        public virtual IEnumerable<Reinsurance.FacultativeStatus> GetReinsuranceFacultativeStatus()
        {
            return _policyRepository.GetReinsuranceFacultativeStatus();
        }

        public virtual IEnumerable<Reinsurance.FacultativeDetails> GetReinsuranceFacultativeDetails(Reinsurance.FacultativeDetails facultativeDets)
        {
            return _policyRepository.GetReinsuranceFacultativeDetails(facultativeDets);
        }
        public virtual IEnumerable<Reinsurance.RatingRisk> GetRatingRisk(string ratingTable)
        {
            return _policyRepository.GetRatingRisk(ratingTable);
        }
        public virtual string GetNewCotizacionNumber(int countryId, string productCode)
        {
            return _policyRepository.GetNewCotizacionNumber(countryId, productCode);
        }
        //Bmarroquin 13-05-2017 se crea metodo 
        public virtual string getIsValidFacultativeID(int Case_seq_No, string Facultative_Reinsurance_Id)
        {
            return _policyRepository.getIsValidFacultativeID(Case_seq_No, Facultative_Reinsurance_Id);
        }

        public virtual IEnumerable<Policy.QuoGrid.AgentChain> GetAgentChain(Policy.QuoGrid.AgentChain parameter)
        {
            parameter.CorpId = 1;
            parameter.AgentNameId = null;
            parameter.BlId = 2;

            return
                _policyRepository.GetAgentChain(parameter);
        }

        public virtual DataTable GetAgentChain(int agentId)
        {
            DataTable agentChain;
            DataRow agentChainr;
            IEnumerable<Policy.QuoGrid.AgentChain> data;

            data = this.GetAgentChain(new Policy.QuoGrid.AgentChain { AgentId = agentId });
            agentChain = new DataTable();
            agentChain.Columns.Add("Agent_Id", typeof(Int32));

            foreach (Policy.QuoGrid.AgentChain ac in data)
            {
                agentChainr = agentChain.NewRow();
                agentChainr["Agent_Id"] = ac.AgentId;
                agentChain.Rows.Add(agentChainr);
            }

            return
                agentChain;
        }

        public virtual IEnumerable<Policy.QuoGrid> GetAllCustomerPlanDetailQuo(Policy.QuoGrid.Key parameter)
        {
            DataTable data;
            IEnumerable<Policy.QuoGrid> result;

            if (parameter.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");

            if (string.IsNullOrWhiteSpace(parameter.Tab))
                throw new ArgumentException("This property can't be Null Or WhiteSpace.", "Tab");

            if (parameter.CompanyId.HasValue && parameter.CompanyId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "CompanyId");

            if (parameter.OfficeId.HasValue && parameter.OfficeId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");

            if (parameter.AgentId.HasValue && parameter.AgentId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "AgentId");

            if (parameter.BlId.HasValue && parameter.BlId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "BlId");

            data = _policyRepository.GetAllCustomerPlanDetailQuo(parameter);

            result = data.AsEnumerable().Select(x => new Policy.QuoGrid
            {
                CorpId = x.Field<int>("Corp_Id"),
                RegionId = x.Field<int>("Region_Id"),
                CountryId = x.Field<int>("Country_Id"),
                DomesticregId = x.Field<int>("Domesticreg_Id"),
                StateProvId = x.Field<int>("State_Prov_Id"),
                CityId = x.Field<int>("City_Id"),
                OfficeId = x.Field<int>("Office_Id"),
                CaseSeqNo = x.Field<int>("Case_Seq_No"),
                HistSeqNo = x.Field<int>("Hist_Seq_No"),
                BussinessLineType = x.Field<int>("Bussiness_Line_Type"),
                BussinessLineId = x.Field<int>("Bussiness_Line_Id"),
                CompanyId = x.Field<int>("Company_Id"),
                CompanyDesc = x.Field<string>("Company_Desc"),
                PolicyNo = x.Field<string>("Policy_No"),
                BlDesc = x.Field<string>("Bl_Desc"),
                ProductTypeDesc = x.Field<string>("Product_Type_Desc"),
                ProductSubTypeDesc = x.Field<string>("Product_Sub_Type_Desc"),
                OfficeDesc = x.Field<string>("Office_Desc"),
                AgentId = x.Field<int>("Agent_Id"),
                DistributionDesc = x.Field<string>("Distribution_Desc"),
                PolicyStatusId = x.Field<int>("Policy_Status_Id"),
                PolicyStatusDesc = x.Field<string>("Policy_Status_Desc"),
                AgentName = x.Field<string>("Agent_Name"),
                ContactId = x.Field<int>("Contact_Id"),
                FullName = x.Field<string>("Full_Name"),
                IdContact = x.Field<string>("Id_Contact"),
                InsuredAmount = x.Field<decimal?>("Insured_Amount"),
                AnnualPremium = x.Field<decimal?>("Annual_Premium"),
                InitialContribution = x.Field<decimal?>("Initial_Contribution"),
                QuoDate = x.Field<DateTime>("QuoDate"),
                Days = x.Field<int>("Days"),
                IsExpiring = x.Field<bool>("IsExpiring"),
                IsExpired = x.Field<bool>("IsExpired"),
                InspectionQuoDate = x.Field<DateTime?>("InspectionQuoDate"),
                DeclinedQuoDate = x.Field<DateTime?>("DeclinedQuoDate"),
                DocumentMissing = x.Field<int>("Document_Missing"),
                SubscriberAgentId = x.Field<int?>("Subscriber_Agent_Id"),
                SubscriberName = x.Field<string>("Subscriber_Name"),
                InspectorAgentId = x.Field<int?>("Inspector_Agent_Id"),
                InspectorName = x.Field<string>("Inspector_Name"),
                Tab = x.Field<string>("Tab"),
                PolicyStatusNameKey = x.Field<string>("Policy_Status_Name_Key"),
                PolicyNoTemp = x.Field<string>("Policy_No_Temp"),
                TipoRiesgoNameKey = x.Field<string>("Tipo_Riesgo_Name_Key"),
                QuoPosDate = x.Field<DateTime?>("QuoPosDate"),
                EffectiveDate = x.Field<DateTime?>("EffectiveDate"),
                ExpirationDate = x.Field<DateTime?>("ExpirationDate"),
                WorkMinute = x.Field<int?>("Work_Minute"),
                SubscriptionMinute = x.Field<int?>("Subscription_Minute"),
                InspectionMinute = x.Field<int?>("Inspection_Minute"),
                HasDiscount = x.Field<bool>("HasDiscount"),
                MakeDiscount = x.Field<bool>("MakeDiscount"),
                HasSurcharge = x.Field<bool>("HasSurcharge"),
                DiscountAmount = x.Field<decimal?>("DiscountAmount"),
                SupervisorAgentId = x.Field<int?>("Supervisor_Agent_Id"),
                SupervisorAgentName = x.Field<string>("Supervisor_Agent_Name"),
                DeclinedQuoReason = x.Field<string>("DeclinedQuoReason"),
                MissingDocumentQuoReason = x.Field<string>("MissingDocumentQuoReason"),
                ConfirmationCallerAgentId = x.Field<int>("ConfirmationCaller_Agent_Id"),
                ConfirmationCallerName = x.Field<string>("ConfirmationCaller_Name"),
                PolicyExpirationDate = x.Field<DateTime?>("PolicyExpirationDate"),
                QuoCreateUserName = x.Field<string>("QuoCreateUserName"),
                PolicyNoMain = x.Field<string>("Policy_No_Main"),
                NeedInspection = x.Field<bool>("Need_Inspection"),
                HasFacultative = x.Field<bool>("HasFacultative"),
                ProratedPremium = x.Field<decimal?>("Prorated_Premium"),
                RequestTypeId = x.Field<int?>("Request_Type_Id"),
                RequestTypeDesc = x.Field<string>("Request_Type_Desc"),
                Financed = x.Field<bool>("Financed"),
                Rate = x.Field<decimal?>("Rate"),
                Make = x.Field<string>("Make"),
                Model = x.Field<string>("Model"),
                Year = x.Field<string>("Year"),
                ModelAccidentRate = x.Field<decimal?>("ModelAccidentRate"),
                VendorAccidentRate = x.Field<decimal?>("VendorAccidentRate"),
                AgentAccidentRate = x.Field<decimal?>("AgentAccidentRate"),
                MinDeducCristales = x.Field<string>("MinDeducCristales"),
                MinDeducDP = x.Field<string>("MinDeducDP"),
                PorcDeducDP = x.Field<string>("PorcDeducDP")
            }).ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.QuoGrid.Counter> GetAllCustomerPlanDetailCountQuo(Policy.QuoGrid.Key parameter)
        {
            DataTable data;
            IEnumerable<Policy.QuoGrid.Counter> result;

            if (parameter.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");

            if (parameter.AgentId.HasValue && parameter.AgentId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "AgentId");

            if (parameter.CompanyId.HasValue && parameter.CompanyId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "CompanyId");

            if (parameter.OfficeId.HasValue && parameter.OfficeId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");

            if (parameter.BlId.HasValue && parameter.BlId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "BlId");

            data = _policyRepository.GetAllCustomerPlanDetailCountQuo(parameter);

            result = data.AsEnumerable().Select(x => new Policy.QuoGrid.Counter
            {
                Count = x.Field<int>("Count"),
                Tab = x.Field<string>("Tab")
            }).ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.QuoGrid> GetAllPuntoVentaQuo(Policy.QuoGrid.Key parameter)
        {
            if (parameter.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");

            if (string.IsNullOrWhiteSpace(parameter.Tab))
                throw new ArgumentException("This property can't be Null Or WhiteSpace.", "Tab");

            if (parameter.CompanyId.HasValue && parameter.CompanyId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "CompanyId");

            if (parameter.OfficeId.HasValue && parameter.OfficeId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");

            if (parameter.AgentId.HasValue && parameter.AgentId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "AgentId");

            if (parameter.BlId.HasValue && parameter.BlId.Value <= 0)
                throw new ArgumentException("This property can't be under 0.", "BlId");

            if (parameter.DateFrom.HasValue || parameter.DateTo.HasValue)
            {
                if (parameter.DateFrom.HasValue && parameter.DateTo.HasValue)
                {
                    if (parameter.DateTo.Value > parameter.DateFrom.Value)
                        throw new ArgumentException("DateTo cannot be greater than DateFrom.", "DateFrom,DateTo");
                }
                else
                    throw new ArgumentException("If one of this properties have value the other must to.", "DateFrom,DateTo");
            }

            return
                _policyRepository.GetAllPuntoVentaQuo(parameter);
        }

        public virtual int SetVehicleInsuredInspectionStatus(Policy.VehicleInsured.InspectionV parameter)
        {
            return
                _policyRepository.SetVehicleInsuredInspectionStatus(parameter);
        }

        public virtual int SetVehicleInsuredInspectionAddress(Policy.VehicleInsured.InspectionV parameter)
        {
            return
                _policyRepository.SetVehicleInsuredInspectionAddress(parameter);
        }

        public virtual IEnumerable<Policy.TabRol> GetAllTabsByRole(int UsrId)
        {
            return
                _policyRepository.GetAllTabsByRole(UsrId);
        }

        public virtual int BlackListMember(Policy.BlackListMember.Parameter parameter)
        {
            return
                _policyRepository.BlackListMember(parameter);
        }

        public virtual int SetBlakList(Policy.BlackList.Parameter parameter)
        {
            return
                 _policyRepository.SetBlakList(parameter);
        }

        public virtual Policy.AgentInfo GetAgentInfo(int? CorpId, int? AgentId)
        {
            return
                 _policyRepository.GetAgentInfo(CorpId, AgentId);
        }

        public virtual IEnumerable<Policy.AgentInfo.Directory> GetAgentDirectoryInfo(int? DirectoryId)
        {
            return
                  _policyRepository.GetAgentDirectoryInfo(DirectoryId);
        }

        public virtual Policy.AgentInfo.Agent SetAgent(Policy.AgentInfo.Agent.parameter parameter)
        {
            return
                   _policyRepository.SetAgent(parameter);
        }

        public virtual int SetAgentUniqueID(int? CorpId, int? AgentId, string UniqueId)
        {
            return
                    _policyRepository.SetAgentUniqueID(CorpId, AgentId, UniqueId);

        }

        public virtual int? SetPolicyDirectDebit(int? corp_Id, int? region_Id, int? country_Id, int? domesticreg_Id, int? state_Prov_Id, int? city_Id, int? office_Id, int? case_Seq_No, int? hist_Seq_No, bool? DirectDebit, bool? includeInitial, int? userId)
        {
            return
                     _policyRepository.SetPolicyDirectDebit(corp_Id, region_Id, country_Id, domesticreg_Id, state_Prov_Id, city_Id, office_Id, case_Seq_No, hist_Seq_No, DirectDebit, includeInitial, userId);
        }

        public virtual decimal? GetQuotFromSysFlex(string PolicyNo)
        {
            return
                      _policyRepository.GetQuotFromSysFlex(PolicyNo);
        }

        public virtual Policy.CouponInfo GetCouponInfo(string policyNo)
        {
            return
                   _policyRepository.GetCouponInfo(policyNo);
        }

        public virtual IEnumerable<Policy.Vehicle.AccidentRate> GetTbAccidentRateByMakeAndModel(string Make, string Model)
        {
            return
                  _policyRepository.GetTbAccidentRateByMakeAndModel(Make, Model);
        }  

        public int SetCustomerSing(Nullable<int> corpId, Nullable<int> regionId, Nullable<int> countryId, Nullable<int> domesticRegId, Nullable<int> stateProvId, Nullable<int> cityId, Nullable<int> officeId, Nullable<int> caseSeqNo, Nullable<int> histSeqNo, string dataSign)
        {
            return
                _policyRepository.SetCustomerSing(corpId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, dataSign);
        }

        public string GetCustomerSing(Nullable<int> corpId, Nullable<int> regionId, Nullable<int> countryId, Nullable<int> domesticRegId, Nullable<int> stateProvId, Nullable<int> cityId, Nullable<int> officeId, Nullable<int> caseSeqNo, Nullable<int> histSeqNo)
        {
            return
               _policyRepository.GetCustomerSing(corpId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);
        }

        #region BackgroundCheck Underwriting
        public IEnumerable<Policy.BackGroundCheckLink> Bg_Get_Match_Links(Policy.BackGroundCheckLink param)
        {
            return
                _policyRepository.Bg_Get_Match_Links(param);
        }

        public virtual int Bg_Set_Match_Links(Policy.BackGroundCheckLink param)
        {
            return
                _policyRepository.Bg_Set_Match_Links(param);
        }

        #endregion
    }
}