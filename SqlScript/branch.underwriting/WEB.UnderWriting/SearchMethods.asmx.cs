﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using WEB.UnderWriting.Common;
using Entity.UnderWriting.Entities;

namespace WEB.UnderWriting
{
    /// <summary>
    /// Summary description for SearchMethods
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SearchMethods : System.Web.Services.WebService
    {

        /// <summary>
        /// Autocomplete de Occupation Type
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<ControlsUtility.itemOccupationType> GetOccupationType(string description)
        {
            int? corpId = null;
            int? regionId = null;
            int? countryId = null;
            int? domesticregId = null;
            int? stateProvId = null;
            int? cityId = null;
            int? officeId = null;
            int? caseSeqNo = null;
            int? histSeqNo = null;
            int? contactId = null;
            int? agentId = null;
            bool? isInsured = null;
            int? occupGroupTypeId = null;
            int? requirementCategory = null;
            //int? requirementType = null;
            int? BlTypeId = null;
            int? BlId = null;
            int? currencyId = null;
            bool? appliedByFreqOrCountry = null;

            var parameter = new DropDown.Parameter
            {
                DropDownType = Enum.GetName(typeof(DropDownType), DropDownType.OccupationType),
                CorpId = corpId,
                RegionId = regionId,
                CountryId = countryId,
                DomesticregId = domesticregId,
                StateProvId = stateProvId,
                CityId = cityId,
                OfficeId = officeId,
                CaseSeqNo = caseSeqNo,
                HistSeqNo = histSeqNo,
                ContactId = contactId,
                AgentId = agentId,
                IsInsured = isInsured,
                OccupGroupTypeId = occupGroupTypeId,
                RequirementCatId = requirementCategory,
                BlTypeId = BlTypeId,
                BlId = BlId,
                CurrencyId = currencyId,
                AppliedByFreqOrCountry = appliedByFreqOrCountry
            };

            var d = Services.DropDownManager.GetDropDownByType(parameter).Where(y => y.OccupationGroupDesc.ToUpper().StartsWith(description.ToUpper()));

            var result = new List<ControlsUtility.itemOccupationType>();

            if (d != null && d.Any())
            {
                result = d.Select(x => new ControlsUtility.itemOccupationType
                {
                    description = x.OccupationGroupDesc,
                    value = x.OccupGroupTypeId.Value
                }).ToList();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<ControlsUtility.itemOccupation> GetOccupation(string description, string _LanguageId)
        {
            int? corpId = null;
            int? regionId = null;
            int? countryId = null;
            int? domesticregId = null;
            int? stateProvId = null;
            int? cityId = null;
            int? officeId = null;
            int? caseSeqNo = null;
            int? histSeqNo = null;
            int? contactId = null;
            int? agentId = null;
            bool? isInsured = null;
            int? occupGroupTypeId = null;
            int? requirementCategory = null;
            //int? requirementType = null;
            int? BlTypeId = null;
            int? BlId = null;
            int? currencyId = null;
            int? CompanyId = null;
            int? ProjectId = null;
            int LanguageId = _LanguageId == "en" ? ControlsUtility.Language.en.ToInt() : ControlsUtility.Language.es.ToInt();
            bool? appliedByFreqOrCountry = null;


            var parameter = new DropDown.Parameter
            {
                DropDownType = Enum.GetName(typeof(DropDownType), DropDownType.Occupation),
                CorpId = corpId,
                RegionId = regionId,
                CountryId = countryId,
                DomesticregId = domesticregId,
                StateProvId = stateProvId,
                CityId = cityId,
                OfficeId = officeId,
                CaseSeqNo = caseSeqNo,
                HistSeqNo = histSeqNo,
                ContactId = contactId,
                AgentId = agentId,
                IsInsured = isInsured,
                OccupGroupTypeId = occupGroupTypeId,
                RequirementCatId = requirementCategory,
                BlTypeId = BlTypeId,
                BlId = BlId,
                CurrencyId = currencyId,
                AppliedByFreqOrCountry = appliedByFreqOrCountry,
                CompanyId = CompanyId,
                ProjectId = ProjectId,
                LanguageId = LanguageId
            };

            var d = Services.DropDownManager.GetDropDownByType(parameter);

            var result = new List<ControlsUtility.itemOccupation>(0);

            if (d != null && d.Any())
            {
                d = d.Where(y => y.OccupationDesc.ToUpper().StartsWith(description.ToUpper()));

                if (d != null && d.Any())
                {
                    result = d.Select(x => new ControlsUtility.itemOccupation
                    {
                        description = x.OccupationDesc,
                        value = x.OccupationId.Value,
                        OccupationGroupDesc = x.OccupationGroupDesc,
                        OccupationGroupId = x.OccupGroupTypeId.Value
                    }).ToList();
                }
            }
            return
                result;
        }

        /// <summary>
        /// Autocomplete de Occupation Type
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<ControlsUtility.itemOccupationType> GetConditionType(int RiskGGroup_id,string description)
        {
            var parameter = new DropDown.Parameter
            {
                DropDownType = "RiskDetail",
                CorpId = 1,
                RiskGroupId = RiskGGroup_id
            };

            var result = new List<ControlsUtility.itemOccupationType>();

            if (RiskGGroup_id != -1 && !string.IsNullOrEmpty(description))
            {
                var d = Services.DropDownManager.GetDropDownByType(parameter).Where(y => y.RiskDetDesc.ToUpper().Contains(description.ToUpper()));

                if (d != null && d.Any())
                {
                    result = d.Select(x => new ControlsUtility.itemOccupationType
                    {
                        description = x.RiskDetDesc,
                        value = x.RiskDetId.Value
                    }).ToList();
                }
            }
       
            return result;
        }
    }
}
