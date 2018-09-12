﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DATA.UnderWriting.Repositories.Global;
using DATA.UnderWriting.UnitOfWork;
using Entity.UnderWriting.Entities;
using LOGIC.UnderWriting.Common;

namespace LOGIC.UnderWriting.Global
{
    public class CaseManager
    {
        private CaseRepository _caseRepository;
        private PolicyRepository _policyRepository;

        public CaseManager()
        {
            _caseRepository = SingletonUnitOfWork.Instance.CaseRepository;
            _policyRepository = SingletonUnitOfWork.Instance.PolicyRepository;
        }

        public virtual IEnumerable<Case> GetAllOpen(int companyId, int underwriterId)
        {
            return
                _caseRepository.GetAllOpen(companyId, underwriterId);
        }

        public virtual IEnumerable<Case> GetAllHistory(int companyId, int underwriterId)
        {
            return
                _caseRepository.GetAllHistory(companyId, underwriterId);
        }

        public virtual IEnumerable<Case> GetRedjected(int companyId, int underwriterId)
        {
            return
                _caseRepository.GetRejected(companyId, underwriterId);
        }

        public virtual IEnumerable<Case> GetAllProcessing(int companyId, int underwriterId)
        {
            return
                _caseRepository.GetAllProcessing(companyId, underwriterId);
        }
        public virtual IEnumerable<Case> GetAllAwaitingInformation(int companyId, int underwriterId)
        {
            return
                _caseRepository.GetAllAwaitingInformation(companyId, underwriterId);
        }
        public virtual IEnumerable<Case> GetAllReinsurance(int companyId, int underwriterId)
        {
            return
                _caseRepository.GetAllReinsurance(companyId, underwriterId);
        }
        public virtual IEnumerable<Case> GetAllAlert(int companyId, int underwriterId)
        {
            return
                _caseRepository.GetAllAlert(companyId, underwriterId);
        }
        public virtual IEnumerable<Case> GetAllException(int companyId, int underwriterId)
        {
            return
                _caseRepository.GetAllException(companyId, underwriterId);
        }
        public virtual IEnumerable<Case> GetAllRecent(int companyId, int underwriterId)
        {
            return
                _caseRepository.GetAllRecent(companyId, underwriterId);
        }
        public virtual IEnumerable<Case> GetAllChange(int companyId, int underwriterId)
        {
            return
                _caseRepository.GetAllChange(companyId, underwriterId);
        }
        public virtual IEnumerable<Case> GetAllSearchResult(Case.SearchResult searchResult)
        {
            //These two variables must be either null or value, together.
            if (searchResult != null)
            {
                if ((searchResult.FromDate.HasValue && !searchResult.ToDate.HasValue) || (!searchResult.FromDate.HasValue && searchResult.ToDate.HasValue))
                    throw new ArgumentException("The parameters \"FromDate\" and \"ToDate\" must have a value or be null together."
                        , "If the parameter \"FromDate\" has a value the parameter \"ToDate\" must has a value to and the other way around.");

                searchResult.PolicyNo = string.IsNullOrWhiteSpace(searchResult.PolicyNo)
                                            ? null
                                            : searchResult.PolicyNo.Trim();
                searchResult.ContactFullName = string.IsNullOrWhiteSpace(searchResult.ContactFullName)
                                                    ? null
                                                    : searchResult.ContactFullName.Trim();

                return _caseRepository.GetAllSearchResult(searchResult);
            }
            else
            {
                return new List<Case>();

            }

        }

        public virtual IEnumerable<Case> GetAllOpen(Policy.Parameter policyParameter)
        {
            this.IsValid(policyParameter, Utility.DataBaseActionType.Update);
            this.PolicyRecentViewed(policyParameter);

            return
                _caseRepository.GetAllOpen(policyParameter);
        }

        public virtual IEnumerable<Case> GetAllHistory(Policy.Parameter policyParameter)
        {
            this.IsValid(policyParameter, Utility.DataBaseActionType.Update);
            this.PolicyRecentViewed(policyParameter);

            return
                _caseRepository.GetAllHistory(policyParameter);
        }

        public virtual IEnumerable<Case> GetAllProcessing(Policy.Parameter policyParameter)
        {
            this.IsValid(policyParameter, Utility.DataBaseActionType.Update);
            this.PolicyRecentViewed(policyParameter);

            return
                _caseRepository.GetAllProcessing(policyParameter);
        }
        public virtual IEnumerable<Case> GetAllAwaitingInformation(Policy.Parameter policyParameter)
        {
            this.IsValid(policyParameter, Utility.DataBaseActionType.Update);
            this.PolicyRecentViewed(policyParameter);

            return
                _caseRepository.GetAllAwaitingInformation(policyParameter);
        }
        public virtual IEnumerable<Case> GetAllReinsurance(Policy.Parameter policyParameter)
        {
            this.IsValid(policyParameter, Utility.DataBaseActionType.Update);
            this.PolicyRecentViewed(policyParameter);

            return
                _caseRepository.GetAllReinsurance(policyParameter);
        }
        public virtual IEnumerable<Case> GetAllAlert(Policy.Parameter policyParameter)
        {
            this.IsValid(policyParameter, Utility.DataBaseActionType.Update);
            this.PolicyRecentViewed(policyParameter);

            return
                _caseRepository.GetAllAlert(policyParameter);
        }
        public virtual IEnumerable<Case> GetAllException(Policy.Parameter policyParameter)
        {
            this.IsValid(policyParameter, Utility.DataBaseActionType.Update);
            this.PolicyRecentViewed(policyParameter);

            return
                _caseRepository.GetAllException(policyParameter);
        }
        public virtual IEnumerable<Case> GetAllRecent(Policy.Parameter policyParameter)
        {
            this.IsValid(policyParameter, Utility.DataBaseActionType.Update);
            this.PolicyRecentViewed(policyParameter);

            return
                _caseRepository.GetAllRecent(policyParameter);
        }
        public virtual IEnumerable<Case> GetAllChange(Policy.Parameter policyParameter)
        {
            this.IsValid(policyParameter, Utility.DataBaseActionType.Update);
            this.PolicyRecentViewed(policyParameter);

            return
                _caseRepository.GetAllChange(policyParameter);
        }
        public virtual IEnumerable<Case> GetAllSearchResult(Policy.Parameter policyParameter)
        {
            this.IsValid(policyParameter, Utility.DataBaseActionType.Update);
            this.PolicyRecentViewed(policyParameter);

            return
                _caseRepository.GetAllSearchResult(policyParameter);
        }

        public virtual IEnumerable<Case> GetAllByType(string type, int companyId, int underwriterId)
        {
            IEnumerable<Case> result;

            switch (type)
            {
                case "Processing":
                    result = this.GetAllProcessing(companyId, underwriterId);
                    break;
                case "AwaitingInfo":
                    result = this.GetAllAwaitingInformation(companyId, underwriterId);
                    break;
                case "Reinsurance":
                    result = this.GetAllReinsurance(companyId, underwriterId);
                    break;
                case "Alerts":
                    result = this.GetAllAlert(companyId, underwriterId);
                    break;
                case "ShowExceptions":
                    result = this.GetAllException(companyId, underwriterId);
                    break;
                case "Recent":
                    result = this.GetAllRecent(companyId, underwriterId);
                    break;
                case "Changes":
                    result = this.GetAllChange(companyId, underwriterId);
                    break;
                case "History":
                    result = this.GetAllHistory(companyId, underwriterId);
                    break;
                case "Rejected":
                    result = this.GetRedjected(companyId, underwriterId);
                    break;
                case "Open":
                default:
                    result = this.GetAllOpen(companyId, underwriterId);
                    break;
            }

            return
                result;
        }
        public virtual IEnumerable<Case> GetAllByType(string type, Policy.Parameter policyParameter)
        {
            IEnumerable<Case> result;

            switch (type)
            {
                case "Processing":
                    result = this.GetAllProcessing(policyParameter);
                    break;
                case "AwaitingInfo":
                    result = this.GetAllAwaitingInformation(policyParameter);
                    break;
                case "Reinsurance":
                    result = this.GetAllReinsurance(policyParameter);
                    break;
                case "Alerts":
                    result = this.GetAllAlert(policyParameter);
                    break;
                case "ShowExceptions":
                    result = this.GetAllException(policyParameter);
                    break;
                case "Recent":
                    result = this.GetAllRecent(policyParameter);
                    break;
                case "Changes":
                    result = this.GetAllChange(policyParameter);
                    break;
                case "History":
                    result = this.GetAllHistory(policyParameter);
                    break;
                case "Open":
                default:
                    result = this.GetAllOpen(policyParameter);
                    break;
            }

            return
                result;
        }

        public virtual IEnumerable<Case.Process> GetAllInProcess(Policy.NBParameter paramerter)
        {
            return
                _caseRepository.GetAllInProcess(paramerter);
        }
        public virtual IEnumerable<Case.Process> GetAllInReview(Policy.NBParameter paramerter)
        {
            return
                _caseRepository.GetAllInReview(paramerter);
        }
        public virtual IEnumerable<Case.Process> GetAllEffectivePendingReceipt(Policy.NBParameter paramerter)
        {
            return
                _caseRepository.GetAllEffectivePendingReceipt(paramerter);
        }

        public virtual IEnumerable<Case.Queue> GetQueue(int companyId, int underwriterId)
        {
            return
                _caseRepository.GetQueue(companyId, underwriterId);
        }

        public virtual IEnumerable<Case.TabCount> GetAllTabCounts(int companyId, int underwriterId)
        {
            return
                _caseRepository.GetAllTabCounts(companyId, underwriterId);
        }

        public virtual Policy GenerateNewCase(Case.NewCase newCase)
        {
            CheckKey(newCase);

            return
                _caseRepository.GenerateNew(newCase);
        }

        public virtual Case.CaseResult SendToReview(Case currentCase)
        {
            this.IsValid(currentCase, Utility.DataBaseActionType.Update);

            return
                _caseRepository.SendToReview(currentCase);
        }
        public virtual Case.CaseResult SendToReject(Case currentCase)
        {
            this.IsValid(currentCase, Utility.DataBaseActionType.Update);

            return
                _caseRepository.SendToReject(currentCase);
        }
        public virtual Case.CaseResult SendToStl(Case currentCase)
        {
            this.IsValid(currentCase, Utility.DataBaseActionType.Update);

            return
                _caseRepository.SendToStl(currentCase);
        }

        public virtual IEnumerable<Case.Comment> GetAllComments(Case currentCase)
        {
            return
                _caseRepository.GetAllComments(currentCase);
        }
        public virtual int InsertComment(Case.Comment comment)
        {
            this.IsValid(comment, Utility.DataBaseActionType.Insert);

            comment.CommentId = -1;
            comment.CommentDate = DateTime.Now;

            return
                 _caseRepository.SetComment(comment);
        }

        public virtual Case.AssignCase SetAssignCase(Case.AssignCase paramter)
        {
            return
                _caseRepository.SetAssignCase(paramter);
        }

        private void CheckKey(Case.NewCase newCase)
        {
            object oTemp;
            int value;
            PropertyInfo[] properties;
            IEnumerable<string> keys;

            keys = new string[] { "CorpId", "RegionId", "CountryId", "DomesticregId", "StateProvId", "CityId", "OfficeId", "AgentId", "UserId" };
            properties = typeof(Case.NewCase).GetProperties();

            foreach (PropertyInfo prop in properties)
                if (prop.PropertyType.Name == "Int32")
                {
                    if (keys.Contains(prop.Name))
                    {
                        oTemp = prop.GetValue(newCase);

                        value =
                            oTemp != null
                                ? int.Parse(oTemp.ToString())
                                : -1;

                        if (value <= 0)
                            throw new ArgumentException("This property can't be under 0.", prop.Name);

                    }
                }
        }
        private void IsValid(Case ca, Utility.DataBaseActionType action)
        {
            if (ca.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (ca.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (ca.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (ca.DomesticregId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticRegId");
            if (ca.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (ca.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (ca.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (ca.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (ca.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");
            if (ca.UserId <= 0)
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
        private void IsValid(Policy.Parameter policyParameter, Utility.DataBaseActionType action)
        {
            if (policyParameter.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (policyParameter.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (policyParameter.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (policyParameter.DomesticregId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticRegId");
            if (policyParameter.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (policyParameter.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (policyParameter.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (policyParameter.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (policyParameter.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");
            if (policyParameter.UnderwriterId <= 0)
                throw new ArgumentException("This property can't be under 0.", "UnderwriterId");

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
        private void IsValid(Case.Comment comment, Utility.DataBaseActionType action)
        {
            if (comment.CorpId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CorpId");
            if (comment.RegionId <= 0)
                throw new ArgumentException("This property can't be under 0.", "RegionId");
            if (comment.CountryId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CountryId");
            if (comment.DomesticregId <= 0)
                throw new ArgumentException("This property can't be under 0.", "DomesticRegId");
            if (comment.StateProvId <= 0)
                throw new ArgumentException("This property can't be under 0.", "StateProvId");
            if (comment.CityId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CityId");
            if (comment.OfficeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "OfficeId");
            if (comment.CaseSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "CaseSeqNo");
            if (comment.HistSeqNo <= 0)
                throw new ArgumentException("This property can't be under 0.", "HistSeqNo");
            if (comment.CommentTypeId <= 0)
                throw new ArgumentException("This property can't be under 0.", "CommentTypeId");

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
        private void PolicyRecentViewed(Policy.Parameter policyParameter)
        {
            int viewId;

            viewId = -1;

            _policyRepository.RecentViewed(
                               policyParameter.CorpId,
                               policyParameter.RegionId,
                               policyParameter.CountryId,
                               policyParameter.DomesticregId,
                               policyParameter.StateProvId,
                               policyParameter.CityId,
                               policyParameter.OfficeId,
                               policyParameter.CaseSeqNo,
                               policyParameter.HistSeqNo,
                               policyParameter.UnderwriterId,
                               viewId,
                               DateTime.Now,
                               policyParameter.UnderwriterId
                           );
        }
    }
}
