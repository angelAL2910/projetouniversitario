﻿using Entity.UnderWriting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace WEB.UnderWriting.Common
{
    public enum DropDownType
    {
        Occupation,
        OccupationType,
        City,
        Country,
        MaritalStatus,
        PolicyStatus,
        ByDate,
        Manager,
        SubManager,
        Office,
        AddressType,
        PhoneType,
        EmailType,
        Smoker,
        Gender,
        Summary,
        StateProvince,
        Relationship,
        IdType,
        AmmendmentType,
        AmmendmentTypeScope,
        RiderStatus,
        OwnerPolicy,
        PaymentStatus,
        RiderType,
        PrimaryBeneficiary,
        IssuedBy,
        RequirementCategory,
        RequirementType,
        RequirementRole,
        MedicalExamReceived,
        PlanType,
        Product,
        Currency,
        Serie,
        ProfileType,
        PaymentFrequency,
        NoteReference,
        PolicyContactByRole,
        PolicyDocument,
        StepCatalog,
        ManagerByOffice,
        Agent,
        LaborPlayed,
        HomeStatus,
        RiskCategory, //RiskGroup
        RiskConditionType, //Detail
        RiskType,
        RiskReason, //PageElement
        RiskRating, // Rating
        RiskRiderType,
        RatingType,
        CreditReason,
        Credit,
        Exclusion,
        ExclusionType,
        RiskRequestedBy,
        Participant,
        Language,
        CountryOfResidence,
        RequirementCommAgent,
        Underwriter,
        AgentBySubManager,
        CompanyType,
        RelationshipAgent,
        Provider,
		//Bmarroquin 07-03-17 cambio dado que ahora el text box debe ser un DropDown List leer el table rating
        TableRatingRisk,
        CompanyStructure,
        FinalBeneficiaryOption,
        CompanyActivity
    }
    public enum CommType
	{
		Phone,
		Email,
		Address
	}
	public enum Language
	{
		English = 1,
		Spanish = 2
	}
	public enum DirType
	{

	}
	public class DropDownManager
	{
		// IDropDown dropdownManager;
		//UnderWritingDIManager diManager;

		public IEnumerable<ListItem> GetDropDown(DropDownType Type, int corpId, int? regionId = null, int? countryId = null, int? domesticregId = null,
			int? stateProvId = null, int? cityId = null, int? officeId = null, int? caseSeqNo = null, int? histSeqNo = null, int? contactId = null, int? agentId = null,
			int? occupationTypeId = null, bool? isInsured = null, int? RequirementCategory = null, int? RequirementType = null,
			int? riskGroupId = null, int? riskDetId = null, int? riskTypeId = null, int? tableTypeId = null, int? exclusionTypeId = null,
			int? contactAge = null, bool? contactSex = null, int? currencyId = null, int? companyId = null, int? projectId = null, int? creditTypeId = null, int? userId = null, int? languageId = null)
		{
			// diManager = new UnderWritingDIManager();
			// dropdownManager = diManager.DropDownManager;
			IEnumerable<DropDown> DropDownData = null;
			IEnumerable<ListItem> Result = null;

			switch (Type)
			{

				case DropDownType.PhoneType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.PhoneType.ToString(), LanguageId = languageId, CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.DirTypeShortDesc,
														  Value = d.CorpId.ToString() + '|' + d.DirectoryTypeId.ToString()
													  }) : null);
					break;

				case DropDownType.Language:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Language.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.LanguageDesc,
														  Value = d.LanguageId + "|" + d.ImageName
													  }) : null);
					break;
				case DropDownType.EmailType:

					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.EmailType.ToString(), LanguageId = languageId, CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.DirTypeShortDesc,
														  Value = d.CorpId.ToString() + '|' + d.DirectoryTypeId.ToString()
													  }) : null);
					break;
				case DropDownType.AddressType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.AddressType.ToString(), LanguageId = languageId, CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.DirTypeShortDesc,
														  Value = d.CorpId.ToString() + '|' + d.DirectoryTypeId.ToString()
													  }) : null);
					break;
				case DropDownType.Country:

					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Country.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured, LanguageId = languageId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.GlobalCountryDesc,
														  Value = d.CountryId.ToString()
													  }) : null);
					break;
				case DropDownType.CountryOfResidence:

					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.CountryOfResidence.ToString(), LanguageId = languageId, CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.GlobalCountryDesc,
														  Value = d.CountryId.ToString()
													  }) : null);
					break;
				case DropDownType.City:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.City.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.CityDesc,
														  Value = d.RegionId.ToString() + '|' + d.CountryId.ToString() + '|' + d.DomesticregId.ToString() + '|' + d.StateProvId.ToString() + '|' + d.CityId.ToString()
													  }) : null);
					break;
				case DropDownType.Manager:

					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Manager.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.AgentName,
														  Value = d.CorpId.ToString() + '|' + d.AgentId.ToString()
													  }) : null);
					break;

				case DropDownType.SubManager:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.SubManager.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.AgentName,
														  Value = d.CorpId.ToString() + '|' + d.AgentId.ToString()
													  }) : null);
					break;
				case DropDownType.ManagerByOffice:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.ManagerByOffice.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.AgentName,
														  Value = d.CorpId.ToString() + '|' + d.AgentId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.Agent:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Agent.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, AgentId = agentId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.AgentName,
														  Value = d.CorpId.ToString() + '|' + d.AgentId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.Office:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Office.ToString(), LanguageId = languageId, CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.OfficeDesc,
														  Value = d.CorpId.ToString() + '|'
														  + d.RegionId.ToString() + '|'
														  + d.CountryId.ToString() + '|'
														  + d.DomesticregId.ToString() + '|'
														  + d.StateProvId.ToString() + '|'
														  + d.CityId.ToString() + '|' + d.OfficeId.ToString()
													  }) : null);
					break;
				case DropDownType.MaritalStatus:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.MaritalStatus.ToString(), LanguageId = languageId, CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.MaritalStatusDesc,
														  Value = d.MaritalStatId.ToString()
													  }) : null);
					break;
				case DropDownType.PolicyStatus:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.PolicyStatus.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.PolicyStatusDesc,
														  Value = d.CorpId.ToString() + '|' + d.PolicyStatusId.ToString()
													  }) : null);
					break;
				case DropDownType.Smoker:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Smoker.ToString(), LanguageId = languageId, CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.SmokerDesc,
														  Value = d.SmokerId.ToString()
													  }) : null);
					break;
				case DropDownType.Gender:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Gender.ToString(), LanguageId = languageId, CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.GenderDesc,
														  Value = d.GenderId
													  }) : null);
					break;
				case DropDownType.Occupation:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Occupation.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured, LanguageId = languageId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.OccupationDesc,
														  Value = d.OccupGroupTypeId.ToString() + "|" + d.OccupationId.ToString()
													  }) : null);
					break;
				case DropDownType.CompanyType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.CompanyType.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured, LanguageId = languageId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.OccupationDesc,
														  Value = d.OccupGroupTypeId.ToString() + "|" + d.OccupationId.ToString()
													  }) : null);
					break;
				case DropDownType.OccupationType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.OccupationType.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured, LanguageId = languageId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.OccupationGroupDesc,
														  Value = d.OccupGroupTypeId.ToString()
													  }) : null);
					break;
				case DropDownType.Summary:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Summary.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured, LanguageId = languageId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.RoleDesc,
														  Value = d.ContactId.ToString() + '|' + d.ContactRoleTypeId.ToString()
													  }) : null);
					break;
				case DropDownType.StateProvince:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.StateProvince.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.StateProvDesc,
														  Value = d.CountryId.ToString() + '|' + d.DomesticregId.ToString() + '|' + d.StateProvId.ToString()
													  }) : null);
					break;
				case DropDownType.Relationship:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Relationship.ToString(), LanguageId = languageId, CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.RelationshipDesc,
														  Value = d.RelationshipId.ToString()
													  }) : null);
					break;
				case DropDownType.IdType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.IdType.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured, LanguageId = languageId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
																	{
																		Text = d.ContactTypeIdDesc.ToString(),
																		Value = d.ContactTypeId.ToString()
																	}) : null);
					break;
				case DropDownType.AmmendmentType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.AmmendmentType.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.AmmendTypeDesc.ToString(),
														  Value = d.AmmendTypeId.ToString()
													  }) : null);
					break;
				case DropDownType.AmmendmentTypeScope:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.AmmendmentTypeScope.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.InsuredScopeDesc.ToString(),
														  Value = d.InsuredScopeId.ToString()
													  }) : null);
					break;
				case DropDownType.RiderStatus:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.RiderStatus.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.RiderStatusDesc.ToString(),
														  Value = d.RiderStatusId.ToString()
													  }) : null);
					break;
				case DropDownType.RiderType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.RiderType.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.CodeName.ToString(),
														  Value = d.RiderTypeId.ToString()
													  }) : null);
					break;
				case DropDownType.OwnerPolicy:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.OwnerPolicy.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.PolicyNo,
														  Value = d.CorpId.ToString() + '|' + d.RegionId.ToString() + '|' + d.CountryId.ToString() + '|' + d.DomesticregId.ToString() + '|' + d.StateProvId.ToString() + '|' + d.CityId.ToString() + '|' + d.OfficeId.ToString()
														  + '|' + d.CaseSeqNo.ToString() + '|' + d.HistSeqNo.ToString() + '|' + d.PolicyNo.ToString()
													  }) : null);
					break;
				case DropDownType.PaymentStatus:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.PaymentStatus.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.PaymentStatusDesc,
														  Value = d.CorpId.ToString() + '|' + d.PaymentStatusId.ToString()
													  }) : null);
					break;
				case DropDownType.PrimaryBeneficiary:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = "Beneficiary", CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  where d.CodeName.Trim().Length > 0
													  select new ListItem
														  {
															  Text = d.CodeName,
															  Value = d.ContactId.HasValue ? d.ContactId.Value.ToString() : "-1"
														  }) : null);
					break;
				case DropDownType.IssuedBy:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.IssuedBy.ToString(), CompanyId = companyId, LanguageId = languageId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
														  {
															  Text = d.GlobalCountryDesc,
															  Value = d.CountryId.ToString()
														  }) : null);
					break;
				case DropDownType.RequirementCategory:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.RequirementCategory.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.RequirementCatDesc,
														  Value = d.RequirementCatId.ToString()
													  }) : null);
					break;
				case DropDownType.RequirementRole:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.RequirementRole.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured, RequirementCatId = RequirementCategory });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.CodeName,
														  Value = d.ContactId.ToString(),
													  }) : null);
					break;
				case DropDownType.RequirementType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.RequirementType.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured, RequirementCatId = RequirementCategory });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.RequirementTypeDesc,
														  Value = d.CorpId.ToString() + '|' + d.RequirementCatId.ToString() + '|' + d.RequirementTypeId.ToString(),
													  }) : null);
					break;
				case DropDownType.MedicalExamReceived:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.MedicalExamReceived.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.RequirementTypeDesc,
														  Value = d.RequirementCatId.ToString() + '|' + d.RequirementTypeId.ToString() + '|' + d.RequirementId.ToString() + '|' + d.MedicalTestId.ToString(),
													  }) : null);
					break;
				case DropDownType.PlanType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.PlanType.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.BlDesc,
														  //Value = d.CorpId.ToString() + '|' + d.RegionId.ToString() + '|' + d.CountryId.ToString() + '|' + d.BlTypeId.ToString() + '|' + d.BlId.ToString(),
														  Value = d.BlTypeId.ToString() + '|' + d.BlId.ToString(),
													  }) : null);
					break;
				case DropDownType.Product:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Product.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.ProductDesc,
														  Value = d.ProductId.ToString() + '|' + d.BlTypeId.ToString() + "|" + d.BlId.ToString(),
													  }) : null);
					break;
				case DropDownType.Currency:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Currency.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.CurrencyDesc,
														  Value = d.CorpId.ToString() + '|' + d.CurrencyId.ToString()
													  }) : null);
					break;
				case DropDownType.Serie:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Serie.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.SerieDesc,
														  Value = d.PolicySerieId.ToString()
													  }) : null);
					break;
				case DropDownType.ProfileType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.ProfileType.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, CurrencyId = currencyId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.ProfileTypeDesc,
														  Value = d.ProfileTypeId.ToString() + "|" + d.Modifiable.ToString()
													  }) : null);
					break;
				case DropDownType.PaymentFrequency:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.PaymentFrequency.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, LanguageId = languageId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.PaymentFreqTypeDesc,
														  Value = d.PaymentFreqTypeId.ToString()
													  }) : null);
					break;
				case DropDownType.NoteReference:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.NoteReference.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.NoteReferenceTypeDesc,
														  Value = d.NoteReferenceTypeId.ToString()
													  }) : null);
					break;
				case DropDownType.PolicyDocument:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.PolicyDocument.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.DocCategoryDesc.ToString(),
														  Value = d.DocTypeId.ToString() + "|" + d.DocCategoryId.ToString()
													  }) : null);
					break;
				case DropDownType.StepCatalog:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.StepCatalog.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.StepDesc,
														  Value = d.StepTypeId.ToString() + "|" + d.StepId.ToString() + "|" + d.StepCode
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.HomeStatus:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.HomeStatus.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.HomeStatusDesc,
														  Value = d.HomeStatusId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.LaborPlayed:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.LaborPlayed.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.LaborPlayedDesc,
														  Value = d.LaborPlayedId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.RiskCategory:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = "RiskGroup", ProjectId = projectId, CompanyId = companyId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.RiskGroupDesc,
														  Value = d.RiskGroupId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.RiskConditionType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = "RiskDetail", ProjectId = projectId, CompanyId = companyId, CorpId = corpId, RiskGroupId = riskGroupId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.RiskDetDesc,
														  Value = d.RiskDetId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.RiskType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.RiskType.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.RiskTypeDesc,
														  Value = d.RiskTypeId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.RiskReason:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = "PageElement", ProjectId = projectId, CompanyId = companyId, CorpId = corpId, RiskGroupId = riskGroupId, RiskDetId = riskDetId, RiskTypeId = riskTypeId, Age = contactAge, Sex = contactSex });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.ElementDesc,
														  Value = d.PageId.ToString() + "|" + d.GridId.ToString() + "|" + d.ElementId.ToString() + "|" + d.ColumnId.ToString()
													  }).OrderBy(r => r.Text) : null);

					break;
				case DropDownType.RiskRating:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = "Rating", CompanyId = companyId, ProjectId = projectId, CorpId = corpId, TableTypeId = tableTypeId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = Decimal.Parse(d.RatingValue).ToString("N2"),
														  Value = d.RegionId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.RiskRiderType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = "RiderByPolicy", CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.CodeName,
														  Value = d.RiderTypeId.ToString() + '|' + d.RiderId.ToString()
													  }).OrderBy(r => r.Text) : null);

					break;
				case DropDownType.CreditReason:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.CreditReason.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.CreditReasonDesc,
														  Value = d.CreditReasonId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.Credit:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Credit.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, CreditTypeId = creditTypeId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.CreditDesc,
														  Value = d.CreditId.ToString()
													  }).OrderBy(r => Decimal.Parse(r.Text.Replace("%", ""))) : null);
					break;
				case DropDownType.Exclusion:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Exclusion.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, ExclusionTypeId = exclusionTypeId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.ExclusionDesc,
														  Value = d.ExclusionId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.ExclusionType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.ExclusionType.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.ExclusionTypeDesc,
														  Value = d.ExclusionTypeId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.RiskRequestedBy:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.RiskRequestedBy.ToString(), CompanyId = companyId, ProjectId = projectId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.CodeName,
														  Value = d.RequestedBy.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.Participant:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Participant.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, UserId = userId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.UnderwritingName,
														  Value = d.UnderwritingId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.RequirementCommAgent:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.RequirementCommAgent.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.AgentName + "|" + d.OfficeDesc,
														  Value = d.AgentId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.RatingType:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.RatingType.ToString(), CompanyId = companyId, ProjectId = projectId, CorpId = corpId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.RatingTypeDesc,
														  Value = d.RatingTypeId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.Underwriter:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.Underwriter.ToString(), UserId = userId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.UnderwritingName,
														  Value = d.UnderwritingId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.AgentBySubManager:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.AgentBySubManager.ToString(), CorpId = corpId, AgentId = agentId });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.AgentName,
														  Value = d.AgentId.ToString()
													  }).OrderBy(r => r.Text) : null);
					break;
				case DropDownType.RelationshipAgent:
					DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.RelationshipAgent.ToString(), LanguageId = languageId, CompanyId = companyId, ProjectId = projectId, CorpId = corpId, RegionId = regionId, CountryId = countryId, DomesticregId = domesticregId, StateProvId = stateProvId, CityId = cityId, OfficeId = officeId, CaseSeqNo = caseSeqNo, HistSeqNo = histSeqNo, ContactId = contactId, AgentId = agentId, OccupGroupTypeId = occupationTypeId, IsInsured = isInsured });
					Result = (DropDownData != null ? (from d in DropDownData
													  select new ListItem
													  {
														  Text = d.RelationshipDesc,
														  Value = d.RelationshipId.ToString()
													  }).OrderBy(r => r.Text) : null);

					break;
                    //Bmarroquin 07-03-2017 se crea dado que ahora el table Rating se lee desde una tabla 
                case DropDownType.TableRatingRisk:
                    DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.TableRatingRisk.ToString(), UserId = userId });
                    Result = (DropDownData != null ? (from d in DropDownData
                                                      select new ListItem
                                                      {
                                                          Text = d.ElementDesc,
                                                          Value = (d.ElementId <= 9) ? "0" + d.ElementId.ToString() : d.ElementId.ToString()
                                                      }).OrderBy(r => r.Text) : null);

                    break;
                //Fin Cambios Bmarroquin 07-03-2017 
                case DropDownType.CompanyStructure:
                    DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.CompanyStructure.ToString(), UserId = userId });
                    Result = (DropDownData != null ? (from d in DropDownData
                                                      select new ListItem
                                                      {
                                                          Text = d.AgentName,
                                                          Value = d.AgentId.ToString()
                                                      }).OrderBy(r => r.Text) : null);

                    break;
                 case DropDownType.FinalBeneficiaryOption:
                    DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.FinalBeneficiaryOption.ToString(), UserId = userId });
                    Result = (DropDownData != null ? (from d in DropDownData
                                                      select new ListItem
                                                      {
                                                          Text = d.AgentName,
                                                          Value = d.AgentId.ToString()
                                                      }).OrderBy(r => r.Text) : null);

                    break;
                case DropDownType.CompanyActivity:
                    DropDownData = Services.DropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = DropDownType.CompanyActivity.ToString(), UserId = userId });
                    Result = (DropDownData != null ? (from d in DropDownData
                                                      select new ListItem
                                                      {
                                                          Text = d.AgentName,
                                                          Value = d.AgentId.ToString()
                                                      }).OrderBy(r => r.Text) : null);

                    break;

            }
            return Result;
		}

		public void GetDropDown(ref DropDownList drop, Language language, DropDownType Type, int corpId, int? regionId = null, int? countryId = null, int? domesticregId = null,
			int? stateProvId = null, int? cityId = null, int? officeId = null, int? caseSeqNo = null, int? histSeqNo = null, int? contactId = null, int? agentId = null,
			int? occupationTypeId = null, bool? isInsured = null, int? RequirementCategory = null, int? RequirementType = null,
			int? riskGroupId = null, int? riskDetId = null, int? riskTypeId = null, int? tableTypeId = null, int? exclusionTypeId = null, int? contactAge = null, bool? contactSex = null, int? currencyId = null, int? companyId = null, int? projectId = null, int? creditTypeId = null, int? userId = null)
		{
			int? languageId = (int)language;


			var result = GetDropDown(Type, corpId, regionId, countryId, domesticregId,
				stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, agentId, occupationTypeId, isInsured,
				RequirementCategory, RequirementType, riskGroupId, riskDetId, riskTypeId, tableTypeId, exclusionTypeId,
				contactAge, contactSex, currencyId, companyId, projectId, creditTypeId, userId, languageId);

			drop.Items.Clear();
			drop.DataSource = result;
			drop.DataTextField = "Text";
			drop.DataValueField = "Value";

			if (result == null) return;
			drop.DataBind();
			drop.Items.Insert(0, new ListItem { Text = "Select", Value = "-1" });
		}

		public void GetDropDown(ref DropDownList drop, string SelectedName, Language language, DropDownType Type, int corpId, int? regionId = null, int? countryId = null, int? domesticregId = null,
			int? stateProvId = null, int? cityId = null, int? officeId = null, int? caseSeqNo = null, int? histSeqNo = null, int? contactId = null, int? agentId = null,
			int? occupationTypeId = null, bool? isInsured = null, int? RequirementCategory = null, int? RequirementType = null,
			int? riskGroupId = null, int? riskDetId = null, int? riskTypeId = null, int? tableTypeId = null, int? exclusionTypeId = null, int? contactAge = null, bool? contactSex = null, int? currencyId = null, int? companyId = null, int? projectId = null, int? creditTypeId = null, int? userId = null)
		{
			int? languageId = (int)language;

			var result = GetDropDown(Type, corpId, regionId, countryId, domesticregId,
				stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, agentId, occupationTypeId, isInsured,
				RequirementCategory, RequirementType, riskGroupId, riskDetId, riskTypeId, tableTypeId, exclusionTypeId,
				contactAge, contactSex, currencyId, companyId, projectId, creditTypeId, userId, languageId);

			drop.DataSource = result;
			drop.DataTextField = "Text";
			drop.DataValueField = "Value";
			if (result != null)
				drop.DataBind();
			if (SelectedName != null)
				drop.Items.Insert(0, new ListItem { Text = SelectedName, Value = "-1" });
		}
	}
}