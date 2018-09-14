﻿using System;
using System.Collections.Generic;
using System.Linq;
using DATA.UnderWriting.Data;
using DATA.UnderWriting.Repositories.Base;
using Entity.UnderWriting.IllusData;

namespace DATA.UnderWriting.Repositories.IllusData
{
    public class IllustratorRepository : IllusDataRepository
    {
        public IllustratorRepository(IllusDataEntityDataModel illusDataModel) : base(illusDataModel) { }

        public virtual int DeleteCustomerDetail(int customerNo, int userIdSystem)
        {
            int result;

            result = illusDataModel.sp_delete_customerdet(customerNo, userIdSystem);

            return
                result;
        }

        public virtual int DeleteCustomerPlanDetail(int customerPlanNo, int userIdSystem)
        {
            int result;

            result = illusDataModel.sp_delete_customerPlandet(customerPlanNo, userIdSystem);

            return
                result;
        }

        public virtual Illustrator.CustomerDetail SetCustomerDetail(Illustrator.CustomerDetail customerDetail)
        {
            Illustrator.CustomerDetail result;
            IEnumerable<sp_set_customerdet_Result> temp;

            temp = illusDataModel.sp_set_customerdet(
                    customerDetail.CustomerNo,
                    customerDetail.ClientId,
                    customerDetail.FirstName,
                    customerDetail.LastName,
                    customerDetail.LastName2,
                    customerDetail.MiddleName,
                    customerDetail.BirthDate,
                    customerDetail.Age,
                    customerDetail.GenderCode,
                    customerDetail.MaritalStatusCode,
                    customerDetail.Smoker,
                    customerDetail.Address1,
                    customerDetail.Address2,
                    customerDetail.City,
                    customerDetail.Dtate,
                    customerDetail.CountryNo,
                    customerDetail.ZipCode,
                    customerDetail.Emailid1,
                    customerDetail.Emailid2,
                    customerDetail.BusAddress1,
                    customerDetail.BusAddress2,
                    customerDetail.BusZipCode,
                    customerDetail.BusCity,
                    customerDetail.BusCountryNo,
                    customerDetail.BusState,
                    customerDetail.AgentCode,
                    customerDetail.Notes,
                    customerDetail.ReferralTypeCode,
                    customerDetail.BusAddress3,
                    customerDetail.Address3,
                    customerDetail.UserId,
                    customerDetail.DateCreated,
                    customerDetail.CreatedBy,
                    customerDetail.DateUpdated,
                    customerDetail.UpdatedBy,
                    customerDetail.Appointment,
                    customerDetail.IllusCount,
                    customerDetail.RCustomerNo,
                    customerDetail.CustomerStatusCode,
                    customerDetail.Calls,
                    customerDetail.Visits,
                    customerDetail.DateSynced,
                    customerDetail.RecordId,
                    customerDetail.RefName,
                    customerDetail.RefLastName,
                    customerDetail.RefEmail,
                    customerDetail.Ext,
                    customerDetail.PhoneNo,
                    customerDetail.ResCountryNo,
                    customerDetail.RefCustomerNo,
                    customerDetail.IsDeleted,
                    customerDetail.IdNo,
                    customerDetail.CompanyId,
                    customerDetail.UserIdSystem,
                    null,
                    false,
                    false
                )
                .ToArray();

            result = temp
                .Select(cd => new Illustrator.CustomerDetail
                {
                    CustomerNo = cd.customerno
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual Illustrator.CustomerPlanDetail SetCustomerPlanDetail(Illustrator.CustomerPlanDetail customerPlanDetail)
        {
            Illustrator.CustomerPlanDetail result;
            IEnumerable<sp_set_customerPlandet_Result> temp;

            temp = illusDataModel.sp_set_customerPlandet(
                    customerPlanDetail.CustomerPlanNo,
                    customerPlanDetail.PlanDate,
                    customerPlanDetail.ProductCode,
                    customerPlanDetail.PClass,
                    customerPlanDetail.CustomerNo,
                    customerPlanDetail.FrequencyTypeCode,
                    customerPlanDetail.Frequency,
                    customerPlanDetail.InsuredAmount,
                    customerPlanDetail.PremiumAmount,
                    customerPlanDetail.AnnualizedPremium,
                    customerPlanDetail.EndDate,
                    customerPlanDetail.ProjectedPremium,
                    customerPlanDetail.InitialContribution,
                    customerPlanDetail.InitialCommission,
                    customerPlanDetail.TargetPremium,
                    customerPlanDetail.InsuranceLevelCode,
                    customerPlanDetail.CalculateTypeCode,
                    customerPlanDetail.ContributionTypeCode,
                    customerPlanDetail.InvestmentProfileCode,
                    customerPlanDetail.InvestmentProfilePercent,
                    customerPlanDetail.ActivityRiskTypeNo,
                    customerPlanDetail.HealthRiskTypeNo,
                    customerPlanDetail.ContributionPeriod,
                    customerPlanDetail.FinancialGoal,
                    customerPlanDetail.FinancialGoalAge,
                    customerPlanDetail.FinancialGoalAmount,
                    customerPlanDetail.CurrencyCode,
                    customerPlanDetail.RiderAdb,
                    customerPlanDetail.RiderTerm,
                    customerPlanDetail.RiderOir,
                    customerPlanDetail.CountryNo,
                    customerPlanDetail.PlanTypeCode,
                    customerPlanDetail.DateCreated,
                    customerPlanDetail.CreatedBy,
                    customerPlanDetail.DateUpdated,
                    customerPlanDetail.UpdatedBy,
                    customerPlanDetail.RiderAdbAmount,
                    customerPlanDetail.RiderTermAmount,
                    customerPlanDetail.RiderTermUntilAge,
                    customerPlanDetail.RiderCi,
                    customerPlanDetail.RiderCiAmount,
                    customerPlanDetail.RiderAcdb,
                    customerPlanDetail.RCustomerPlanNo,
                    customerPlanDetail.IllustrationNo,
                    customerPlanDetail.DataEntryTypeCode,
                    customerPlanDetail.PlanCode,
                    customerPlanDetail.UserId,
                    customerPlanDetail.ContributionUntilAge,
                    customerPlanDetail.OpeningBalance,
                    customerPlanDetail.MinimumPremium,
                    customerPlanDetail.OpeningYear,
                    customerPlanDetail.PlanEffectiveDate,
                    customerPlanDetail.IllustrationVerified,
                    customerPlanDetail.RiderAdbCost,
                    customerPlanDetail.RiderAcdbCost,
                    customerPlanDetail.RiderTermCost,
                    customerPlanDetail.RiderCiCost,
                    customerPlanDetail.TermPeriod,
                    customerPlanDetail.RetirementPeriod,
                    customerPlanDetail.DefermentPeriod,
                    customerPlanDetail.AnnuityAmount,
                    customerPlanDetail.CompanyNo,
                    customerPlanDetail.DateSynced,
                    customerPlanDetail.RecordId,
                    customerPlanDetail.OtherPlans,
                    customerPlanDetail.DispIllustrationNo,
                    customerPlanDetail.BenefitAmount,
                    customerPlanDetail.DispPlanCode,
                    customerPlanDetail.TermContributionTypeCode,
                    customerPlanDetail.IsSpecial,
                    customerPlanDetail.ChangeType,
                    customerPlanDetail.BoUpdatedBy,
                    customerPlanDetail.BoLastUpdatedBy,
                    customerPlanDetail.NewStatusDate,
                    customerPlanDetail.IsoPeningBalance,
                    customerPlanDetail.IsApproved,
                    customerPlanDetail.PolicyStatusCode,
                    customerPlanDetail.IsPolicyChangesApproved,
                    customerPlanDetail.IsDeleted,
                    customerPlanDetail.StudentName,
                    customerPlanDetail.StudentAge,
                    customerPlanDetail.IsCustomerOwner,
                    customerPlanDetail.OwnerCustomerNo,
                    customerPlanDetail.IllustrationStatusCode,
                    customerPlanDetail.Familiar,
                    customerPlanDetail.Repatriacion,
                    customerPlanDetail.SepulturaLote,
                    customerPlanDetail.CompanyId,
                    customerPlanDetail.UserIdSystem,
                    customerPlanDetail.SpecialPayment,
                    customerPlanDetail.ProviderTypeId,
                    customerPlanDetail.ProviderId,
                    customerPlanDetail.ContributionPeriodMonth,
                    customerPlanDetail.FinancingRate,
                    customerPlanDetail.DestinyFund,
					Convert.ToDecimal(customerPlanDetail.FractionSurcharge)
                )
                .ToArray();

            result = temp
                .Select(cpd => new Illustrator.CustomerPlanDetail
                {
                    CustomerPlanNo = cpd.customerPlanno
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.CustomerDetail> GetAllCustomerDetailOrById(long? customerNo, long? rCustomerNo)
        {
            IEnumerable<Illustrator.CustomerDetail> result;
            IEnumerable<sp_get_customerdet_Result> temp;

            temp = illusDataModel.sp_get_customerdet(customerNo, rCustomerNo);

            result = temp
                .Select(cd => new Illustrator.CustomerDetail
                {
                    CustomerNo = cd.customerno,
                    ClientId = cd.ClientID,
                    FirstName = cd.FirstName,
                    LastName = cd.LastName,
                    LastName2 = cd.LastName2,
                    MiddleName = cd.MiddleName,
                    BirthDate = cd.Birthdate,
                    Age = cd.Age,
                    GenderCode = cd.gendercode,
                    MaritalStatusCode = cd.MaritalStatuscode,
                    Smoker = cd.Smoker,
                    Address1 = cd.Address1,
                    Address2 = cd.Address2,
                    City = cd.City,
                    Dtate = cd.state,
                    CountryNo = cd.Countryno,
                    ZipCode = cd.ZipCode,
                    Emailid1 = cd.Emailid1,
                    Emailid2 = cd.Emailid2,
                    BusAddress1 = cd.BusAddress1,
                    BusAddress2 = cd.BusAddress2,
                    BusZipCode = cd.BusZipCode,
                    BusCity = cd.BusCity,
                    BusCountryNo = cd.BusCountryno,
                    BusState = cd.BusState,
                    AgentCode = cd.Agentcode,
                    Notes = cd.notes,
                    ReferralTypeCode = cd.referraltypecode,
                    BusAddress3 = cd.BusAddress3,
                    Address3 = cd.Address3,
                    UserId = cd.userid,
                    DateCreated = cd.datecreated,
                    CreatedBy = cd.createdby,
                    DateUpdated = cd.dateupdated,
                    UpdatedBy = cd.updatedby,
                    Appointment = cd.appointment,
                    IllusCount = cd.illuscount,
                    RCustomerNo = cd.rcustomerno,
                    CustomerStatusCode = cd.customerstatuscode,
                    Calls = cd.calls,
                    Visits = cd.visits,
                    DateSynced = cd.datesynced,
                    RecordId = cd.recordid,
                    RefName = cd.refname,
                    RefLastName = cd.reflastname,
                    RefEmail = cd.refemail,
                    Ext = cd.ext,
                    PhoneNo = cd.phoneno,
                    ResCountryNo = cd.rescountryno,
                    RefCustomerNo = cd.refcustomerno,
                    IsDeleted = cd.is_deleted,
                    IdNo = cd.idno,
                    GenderName = cd.gendername,
                    MaritalStatus = cd.maritalstatus,
                    CountryName = cd.countryname,
                    ReferralType = cd.referraltype,
                    CustomerStatus = cd.customerstatus
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.CustomerPlanDetail> GetAllCustomerPlanDetailOrById(Illustrator.CustomerPlanDetailP parameter)
        {
            IEnumerable<Illustrator.CustomerPlanDetail> result;
            IEnumerable<sp_get_customerPlandet_Result> temp;
            illusDataModel.Database.CommandTimeout = 160;
            temp = illusDataModel.sp_get_customerPlandet(
                    parameter.CustomerPlanNo,
                    parameter.CustomerNo,
                    parameter.RCustomerNo,
                    parameter.UserId,
                    parameter.CompanyId,
                    parameter.DateTo,
                    parameter.DateFrom,
                    parameter.CorpId,
                    parameter.RegionId,
                    parameter.CountryId,
                    parameter.DomesticregId,
                    parameter.StateProvId,
                    parameter.CityId,
                    parameter.OfficeId,
                    parameter.CaseSeqNo,
                    parameter.HistSeqNo,
                    parameter.LanguageId,
                    parameter.GetHistorical,
                    parameter.IllustrationStatusCode,
                    parameter.TabFilter,
                    parameter.AgentNameId,
                    parameter.AgentType,
                    parameter.AssignedSubscriberNameId
                );

            result = temp
                .Select(cpd => new Illustrator.CustomerPlanDetail
                {
                    CustomerPlanNo = cpd.customerPlanno,

                    PlanDate = cpd.plandate,
                    ProductCode = cpd.productcode,
                    PClass = cpd.@class,
                    CustomerNo = cpd.customerno,
                    FrequencyTypeCode = cpd.frequencytypecode,
                    Frequency = cpd.frequency,
                    InsuredAmount = cpd.insuredamount.ConvertToNoNullable(),
                    PremiumAmount = cpd.premiumamount.ConvertToNoNullable(),
                    AnnualizedPremium = cpd.annualizedpremium.ConvertToNoNullable(),
                    EndDate = cpd.enddate,
                    ProjectedPremium = cpd.projectedpremium.ConvertToNoNullable(),
                    InitialContribution = cpd.initialcontribution.ConvertToNoNullable(),
                    InitialCommission = cpd.initialcommission.ConvertToNoNullable(),
                    TargetPremium = cpd.targetpremium.ConvertToNoNullable(),
                    InsuranceLevelCode = cpd.insurancelevelcode,
                    CalculateTypeCode = cpd.calculatetypecode,
                    ContributionTypeCode = cpd.contributiontypecode,
                    InvestmentProfileCode = cpd.investmentprofilecode,
                    InvestmentProfilePercent = cpd.investmentprofilepercent.ConvertToNoNullable(),
                    ActivityRiskTypeNo = cpd.activityrisktypeno.ConvertToNoNullable(),
                    HealthRiskTypeNo = cpd.healthrisktypeno.ConvertToNoNullable(),
                    ContributionPeriod = cpd.contributionperiod.ConvertToNoNullable(),
                    FinancialGoal = cpd.financialgoal,
                    FinancialGoalAge = cpd.financialgoalage.ConvertToNoNullable(),
                    FinancialGoalAmount = cpd.financialgoalamount.ConvertToNoNullable(),
                    CurrencyCode = cpd.currencycode,
                    RiderAdb = cpd.rideradb,
                    RiderTerm = cpd.riderterm,
                    RiderOir = cpd.rideroir,
                    CountryNo = cpd.countryno.ConvertToNoNullable(),
                    PlanTypeCode = cpd.plantypecode,
                    DateCreated = cpd.datecreated.ConvertToNoNullable(),
                    CreatedBy = cpd.createdby.ConvertToNoNullable(),
                    DateUpdated = cpd.dateupdated,
                    UpdatedBy = cpd.updatedby.ConvertToNoNullable(),
                    RiderAdbAmount = cpd.rideradbamount.ConvertToNoNullable(),
                    RiderTermAmount = cpd.ridertermamount.ConvertToNoNullable(),
                    RiderTermUntilAge = cpd.ridertermuntilage.ConvertToNoNullable(),
                    RiderCi = cpd.riderci,
                    RiderCiAmount = cpd.riderciamount.ConvertToNoNullable(),
                    RiderAcdb = cpd.rideracdb,
                    RCustomerPlanNo = cpd.rcustomerplanno,
                    IllustrationNo = cpd.illustrationno,
                    DataEntryTypeCode = cpd.dataentrytypecode,
                    PlanCode = cpd.plancode,
                    UserId = cpd.userid.ConvertToNoNullable(),
                    ContributionUntilAge = cpd.contributionuntilage.ConvertToNoNullable(),
                    OpeningBalance = cpd.openingbalance.ConvertToNoNullable(),
                    MinimumPremium = cpd.minimumpremium.ConvertToNoNullable(),
                    OpeningYear = cpd.openingyear,
                    PlanEffectiveDate = cpd.planeffectivedate,
                    IllustrationVerified = cpd.illustrationverified,
                    RiderAdbCost = cpd.rideradbcost.ConvertToNoNullable(),
                    RiderAcdbCost = cpd.rideracdbcost.ConvertToNoNullable(),
                    RiderTermCost = cpd.ridertermcost.ConvertToNoNullable(),
                    RiderCiCost = cpd.ridercicost.ConvertToNoNullable(),
                    TermPeriod = cpd.termperiod.ConvertToNoNullable(),
                    RetirementPeriod = cpd.retirementperiod.ConvertToNoNullable(),
                    DefermentPeriod = cpd.defermentperiod.ConvertToNoNullable(),
                    AnnuityAmount = cpd.annuityamount.ConvertToNoNullable(),
                    CompanyNo = cpd.companyno.ConvertToNoNullable(),
                    DateSynced = cpd.datesynced,
                    RecordId = cpd.recordid,
                    OtherPlans = cpd.otherplans,
                    DispIllustrationNo = cpd.dispillustrationno,
                    BenefitAmount = cpd.benefitamount,
                    DispPlanCode = cpd.dispplancode,
                    TermContributionTypeCode = cpd.termcontributiontypecode,
                    IsSpecial = cpd.isspecial,
                    ChangeType = cpd.changetype,
                    BoUpdatedBy = cpd.boupdatedby,
                    BoLastUpdatedBy = cpd.bolastupdatedby,
                    NewStatusDate = cpd.newstatusdate,
                    IsoPeningBalance = cpd.isopeningbalance,
                    IsApproved = cpd.isapproved,
                    PolicyStatusCode = cpd.policystatuscode,
                    IsPolicyChangesApproved = cpd.ispolicychangesapproved,
                    IsDeleted = cpd.is_deleted,
                    StudentName = cpd.studentname,
                    StudentAge = cpd.studentage,
                    IsCustomerOwner = cpd.is_customer_owner,
                    OwnerCustomerNo = cpd.ownercustomerno,
                    IllustrationStatusCode = cpd.illustrationstatuscode,
                    PlanGroupCode = cpd.plangroupcode,
                    Product = cpd.product,
                    ActivityRiskType = cpd.activityrisktype,
                    Currency = cpd.currency,
                    FrequencyType = cpd.frequencytype,
                    InsuranceLevel = cpd.insurancelevel,
                    CalculateType = cpd.calculatetype,
                    InvestmentProfile = cpd.investmentprofile,
                    HealthRiskType = cpd.healthrisktype,
                    CountryName = cpd.countryname,
                    PlanType = cpd.plantype,
                    ContributionTypeDescCode = cpd.contributiontypedesccode,
                    ContributionTypeDescTerm = cpd.contributiontypedescterm,
                    IllustrationStatus = cpd.illustrationstatus,
                    InsuredName = cpd.InsuredName,
                    PlanGroup = cpd.plangroup,
                    Familiar = cpd.Familiar,
                    Repatriacion = cpd.Repatriacion,
                    SepulturaLote = cpd.SepulturaLote,
                    OfficeDesc = cpd.Office_Desc,
                    Agent = cpd.Agent,
                    CompanyDesc = cpd.CompanyDesc,
                    Identification = cpd.Identification,
                    CorpId = cpd.CorpId.ConvertToNoNullable(),
                    RegionId = cpd.RegionId.ConvertToNoNullable(),
                    DomesticregId = cpd.DomesticregId.ConvertToNoNullable(),
                    StateProvId = cpd.StateProvId.ConvertToNoNullable(),
                    CityId = cpd.CityId.ConvertToNoNullable(),
                    OfficeId = cpd.OfficeId.ConvertToNoNullable(),
                    CaseSeqNo = cpd.CaseSeqNo.ConvertToNoNullable(),
                    HistSeqNo = cpd.HistSeqNo.ConvertToNoNullable(),
                    InsuredId = cpd.InsuredId.ConvertToNoNullable(),
                    CountryId = cpd.CountryId.ConvertToNoNullable(),
                    Channel = cpd.Channel,
                    Priority = cpd.Priority.ConvertToNoNullable(),
                    PendingDocumentsNo = cpd.PendingDocumentsNo,
                    AgentId = cpd.AgentId,
                    AssignedSubscriberId = cpd.AssignedSubscriberId,

                    AssignedSubscriber = cpd.AssignedSubscriber,
                    PolicyNoTemp = cpd.Policy_No_Temp,
                    SpecialPayment = cpd.Special_Payment,
                    FinancingRate = cpd.Interest_Rate,
                    ProviderTypeId = cpd.Provider_Type_Id,
                    ProviderId = cpd.Provider_Id,
                    ContributionPeriodMonth = cpd.ContributionPeriod_Month,
                    DestinyFund = cpd.Destination_Of_Funds,
					FractionSurcharge = Convert.ToDouble(cpd.FractionSurcharge),
                    NetAnnualPremium = Convert.ToDouble(cpd.annualizedpremium - cpd.FractionSurcharge) //Bmarroquin cambio de variable primiumAmount por annualizedpremium
                })
                .ToArray();

            return
                result;


        }

        public virtual IEnumerable<DropDown> GetDropDownByType(DropDown.Parameter parameters)
        {
            IEnumerable<DropDown> result;
            IEnumerable<sp_get_dropdown_Result> temp;

            temp = illusDataModel.sp_get_dropdown(
                        parameters.DropDownType,
                        parameters.CompanyId,
                        parameters.ProductCode,
                        parameters.Life,
                        parameters.PlanGroupCode,
                        parameters.Education,
                        parameters.Retirement,
                        parameters.TermInsurance,
                        parameters.PClass,
                        parameters.RiderTypeCode,
                        parameters.Age,
                        parameters.CorpId,
                        parameters.RegionId,
                        parameters.CountryId,
                        parameters.DomesticregId,
                        parameters.StateProvId,
                        parameters.ProviderTypeId
                );

            result = temp
                .Select(dd => new DropDown
                {
                    Product = dd.product,
                    PlanGroupCode = dd.plangroupcode,
                    ProductCode = dd.productcode,
                    PFixed = dd.@fixed,
                    IsRefund = dd.isrefund,
                    PlanGroup = dd.plangroup,
                    Period = dd.period,
                    ContributionType = dd.contributiontype,
                    CalculateType = dd.calculatetype,
                    PlanType = dd.plantype,
                    PlanTypeCode = dd.plantypecode,
                    Life = dd.life,
                    Education = dd.education,
                    Retirement = dd.retirement,
                    TermInsurance = dd.terminsurance,
                    ActivityRiskType = dd.activityrisktype,
                    ActivityRiskTypeNo = dd.activityrisktypeno,
                    ActivityRiskValue = dd.activityriskvalue,
                    HealthRiskType = dd.healthrisktype,
                    HealthRiskTypeNo = dd.healthrisktypeno,
                    HealthRiskValue = dd.healthriskvalue,
                    FrequencyType = dd.frequencytype,
                    FrequencyTypeCode = dd.frequencytypecode,
                    FrequencyValue = dd.frequencyvalue,
                    FrequencyCost = dd.frequencycost,
                    InvestmentProfile = dd.investmentprofile,
                    InvestmentProfileCode = dd.investmentprofilecode,
                    IllustrationType = dd.illustrationtype,
                    IllustrationTypeCode = dd.illustrationtypecode,
                    IllustrationStatusCode = dd.illustrationstatuscode,
                    IllustrationStatus = dd.illustrationstatus,
                    Adb = dd.adb,
                    CountryCode = dd.countrycode,
                    CountryName = dd.countryname,
                    CountryNo = dd.countryno,
                    EmailType = dd.emailtype,
                    EmailTypeCode = dd.emailtypecode,
                    GenderCode = dd.gendercode,
                    GenderName = dd.gendername,
                    LanguageCodes = dd.languagecodes,
                    MaritalStatus = dd.maritalstatus,
                    MaritalStatusCode = dd.maritalstatuscode,
                    PhoneType = dd.phonetype,
                    PhoneTypeCode = dd.phonetypecode,
                    PrimaryLanguageCode = dd.primarylanguagecode,
                    ReferralType = dd.referraltype,
                    ReferralTypeCode = dd.referraltypecode,
                    RelationshipType = dd.relationshiptype,
                    RelationshipTypeCode = dd.relationshiptypecode,
                    RiskValue = dd.riskvalue,
                    AdvisorCode = dd.advisorCode,
                    IdentificationType = dd.identificationtype,
                    IdentificationTypeCode = dd.identificationtypecode,
                    OccupationType = dd.occupationtype,
                    OccupationTypeCode = dd.occupationtypecode,
                    ProfessionType = dd.professiontype,
                    ProfessionTypeCode = dd.professiontypecode,
                    Currency = dd.currency,
                    CurrencyCode = dd.currencycode,
                    PClass = dd.@class,
                    Symbol = dd.symbol,
                    CalculateTypeCode = dd.calculatetypecode,
                    ContributionTypeCode = dd.contributiontypecode,
                    InvestmentProfileRate = dd.investmentprofilerate,
                    BeneficiaryType = dd.beneficiarytype,
                    BeneficiaryTypeCode = dd.beneficiarytypecode,
                    InsuredType = dd.insuredtype,
                    InsuredTypeCode = dd.insuredtypecode,
                    MortalityNo = dd.mortalityno,
                    Age = dd.age,
                    MaleNonSmoker = dd.malenonsmoker,
                    FemaleNonSmoker = dd.femalenonsmoker,
                    MaleSmoker = dd.malesmoker,
                    FemaleSmoker = dd.femalesmoker,
                    RiderTypeCode = dd.ridertypecode,
                    YearNo = dd.yearno,
                    Vr = dd.vr,
                    AdditionalPenalty = dd.additionalpenalty,
                    RegularComm = dd.regularcomm,
                    ExcessComm = dd.excesscomm,
                    CommissionNo = dd.commissionno,
                    PenalTyperCent = dd.penaltypercent,
                    SurrenderPenaltyNo = dd.surrenderpenaltyno,
                    ContributionPeriod = dd.ContributionPeriod,
                    ProviderId = dd.frequencyvalue,
                    ProviderTypeId = dd.mortalityno,
                    ProviderName = dd.ProviderName

                })
                .ToArray();

            return
                result;
        }

        public virtual int DeleteCustomerEmails(long customerNo, int userIdSystem)
        {
            int result;

            result = illusDataModel.sp_delete_customeremaildet(customerNo, userIdSystem);

            return
                result;
        }
        public virtual int DeleteCustomerPhones(long customerNo, int userIdSystem)
        {
            int result;

            result = illusDataModel.sp_delete_customerphonedet(customerNo, userIdSystem);

            return
                result;
        }

        public virtual int InsertCustomerEmail(Illustrator.CustomerEmail customerEmail)
        {
            int result;

            result = illusDataModel.sp_insert_customeremaildet(
                customerEmail.CustomerNo,
                customerEmail.EmailTypeCode,
                customerEmail.EmailId,
                customerEmail.Additional,
                customerEmail.DateSynced,
                customerEmail.RecordId,
                customerEmail.UserIdSystem
                );

            return
                result;
        }
        public virtual int InsertCustomerPhone(Illustrator.CustomerPhone customerPhone)
        {
            int result;

            result = illusDataModel.sp_insert_customerphonedet(
                customerPhone.CustomerNo,
                customerPhone.PhoneTypeCode,
                customerPhone.IntCode,
                customerPhone.AreaCode,
                customerPhone.PhoneNo,
                customerPhone.Ext,
                customerPhone.Additional,
                customerPhone.RCustomerPhoneNo,
                customerPhone.DateSynced,
                customerPhone.RecordId,
                customerPhone.SpecialPhoneType,
                customerPhone.UserIdSystem
                );

            return
                result;
        }

        public virtual int DeleteCustomerOccupations(long customerNo, int userIdSystem)
        {
            int result;

            result = illusDataModel.sp_delete_customeroccupationdet(customerNo, userIdSystem);

            return
                result;
        }
        public virtual int DeleteCustomerIdentifications(long customerNo, int userIdSystem)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanidentificationdet(customerNo, userIdSystem);

            return
                result;
        }

        public virtual Illustrator.CustomerOccupation SetCustomerOccupation(Illustrator.CustomerOccupation customerOccupation)
        {
            Illustrator.CustomerOccupation result;
            IEnumerable<sp_set_customeroccupationdet_Result> temp;

            temp = illusDataModel.sp_set_customeroccupationdet(
                    customerOccupation.CustomerNo,
                    customerOccupation.CompanyName,
                    customerOccupation.BusinessType,
                    customerOccupation.WorkYears,
                    customerOccupation.WorkMonths,
                    customerOccupation.OccupationTypeCode,
                    customerOccupation.ProfessionTypeCode,
                    customerOccupation.Tasks,
                    customerOccupation.AnnualFamilyIncome,
                    customerOccupation.CustomerOccupationNo,
                    customerOccupation.RCustomerOccupationNo,
                    customerOccupation.DateSynced,
                    customerOccupation.RecordId,
                    customerOccupation.UserIdSystem
                )
                .ToArray();

            result = temp
                .Select(cpd => new Illustrator.CustomerOccupation
                {
                    CustomerNo = cpd.customerno
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual int InsertPlanIdentification(Illustrator.CustomerPlanIdentification customerPlanIdentification)
        {
            int result;

            result = illusDataModel.sp_insert_customerplanidentificationdet(
                customerPlanIdentification.InsuredTypeCode,
                customerPlanIdentification.IdentificationTypeCode,
                customerPlanIdentification.IdentificationCode,
                customerPlanIdentification.ExpiryDate,
                customerPlanIdentification.CountryNo,
                customerPlanIdentification.RCustomerIdentificationNo,
                customerPlanIdentification.DateSynced,
                customerPlanIdentification.RecordId,
                customerPlanIdentification.CustomerNo,
                customerPlanIdentification.UserIdSystem
                );

            return
                result;
        }

        public virtual Illustrator.Signature SetIllustrationSignature(Illustrator.Signature signature)
        {
            Illustrator.Signature result;
            IEnumerable<sp_set_illustrationsignaturedet_Result> temp;

            temp = illusDataModel.sp_set_illustrationsignaturedet(
                    signature.CustomerPlanNo,
                    signature.CustomerSign1,
                    signature.AgentSign1,
                    signature.CustomerSign2,
                    signature.AgentSign2,
                    signature.CustomerSign3,
                    signature.AgentSign3,
                    signature.CustomerSign4,
                    signature.AgentSign4,
                    signature.CustomerSign5,
                    signature.AgentSign5,
                    signature.Sign1PageNo,
                    signature.Sign2PageNo,
                    signature.Sign3PageNo,
                    signature.Sign4PageNo,
                    signature.Sign5PageNo,
                    signature.PdfFileName,
                    signature.IsPdfLocked,
                    signature.DateSynced
                )
                .ToArray();

            result = temp
                .Select(isd => new Illustrator.Signature
                {
                    CustomerPlanNo = isd.customerplanno
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual Illustrator.User SetUser(Illustrator.User user)
        {
            Illustrator.User result;
            IEnumerable<sp_set_userdet_Result> temp;

            temp = illusDataModel.sp_set_userdet(
                    user.UserId,
                    user.UserTypeCode,
                    user.UserName,
                    user.Password,
                    user.RoleNo,
                    user.EmailSent,
                    user.EmailId,
                    user.Active,
                    user.DateCreated,
                    user.UpdatePassDate,
                    user.CanLogIn,
                    user.LastLogInDate,
                    user.DateSynced,
                    user.DateUpdated,
                    user.SupervisorId,
                    user.Designation,
                    user.HierarchyCode,
                    user.IsAdmin,
                    user.UserGroupNo,
                    user.NameId,
                    user.MsreplTranVersion,
                    user.IsBlocked,
                    user.EncryptPassword
                )
                .ToArray();

            result = temp
                .Select(u => new Illustrator.User
                {
                    UserId = u.userid
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.User> GetUser(string nameId)
        {
            IEnumerable<Illustrator.User> result;
            IEnumerable<sp_get_userdet_Result> temp;

            temp = illusDataModel.sp_get_userdet(nameId);

            result = temp
                .Select(u => new Illustrator.User
                {
                    UserId = u.userid,
                    UserTypeCode = u.usertypecode,
                    UserName = u.username,
                    Password = u.password,
                    RoleNo = u.roleno,
                    EmailSent = u.emailsent,
                    EmailId = u.emailid,
                    Active = u.active,
                    DateCreated = u.datecreated,
                    UpdatePassDate = u.UpdatePassDate,
                    CanLogIn = u.canlogin,
                    LastLogInDate = u.Lastlogindate,
                    DateSynced = u.datesynced,
                    DateUpdated = u.dateupdated,
                    SupervisorId = u.supervisorid,
                    Designation = u.designation,
                    HierarchyCode = u.hierarchycode,
                    IsAdmin = u.isadmin,
                    UserGroupNo = u.usergroupno,
                    NameId = u.nameid,
                    MsreplTranVersion = u.msrepl_tran_version,
                    IsBlocked = u.IsBlocked,
                    EncryptPassword = u.EncryptPassword
                })
                .ToArray();

            return
                result;


        }

        public virtual IEnumerable<Illustrator.Signature> GetIllustrationSignature(long? customerPlanNo)
        {
            IEnumerable<Illustrator.Signature> result;
            IEnumerable<sp_get_illustrationsignaturedet_Result> temp;

            temp = illusDataModel.sp_get_illustrationsignaturedet(customerPlanNo);

            result = temp
                .Select(isd => new Illustrator.Signature
                {
                    CustomerPlanNo = isd.customerplanno,
                    CustomerSign1 = isd.customersign1,
                    AgentSign1 = isd.agentsign1,
                    CustomerSign2 = isd.customersign2,
                    AgentSign2 = isd.agentsign2,
                    CustomerSign3 = isd.customersign3,
                    AgentSign3 = isd.agentsign3,
                    CustomerSign4 = isd.customersign4,
                    AgentSign4 = isd.agentsign4,
                    CustomerSign5 = isd.customersign5,
                    AgentSign5 = isd.agentsign5,
                    Sign1PageNo = isd.sign1pageno,
                    Sign2PageNo = isd.sign2pageno,
                    Sign3PageNo = isd.sign3pageno,
                    Sign4PageNo = isd.sign4pageno,
                    Sign5PageNo = isd.sign5pageno,
                    PdfFileName = isd.pdffilename,
                    IsPdfLocked = isd.ispdflocked,
                    DateSynced = isd.datesynced
                })
                .ToArray();

            return
                result;
        }

        public virtual long GetMaxIllustrationNo()
        {
            long result;
            IEnumerable<sp_get_max_illustrationno_Result> temp;

            temp = illusDataModel.sp_get_max_illustrationno().ToArray();

            result = temp.First().illustrationno.ConvertToNoNullable();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.CustomerPlanBeneficiary> GetCustomerPlanBeneficiary(long customerPlanNo, string insuredTypeCode, string beneficiaryTypeCode)
        {
            IEnumerable<Illustrator.CustomerPlanBeneficiary> result;
            IEnumerable<sp_get_customerplanbeneficiarydet_Result> temp;

            temp = illusDataModel.sp_get_customerplanbeneficiarydet(customerPlanNo, insuredTypeCode, beneficiaryTypeCode);

            result = temp
                .Select(cb => new Illustrator.CustomerPlanBeneficiary
                {
                    CustomerPlanNo = cb.customerplanno,
                    InsuredTypeCode = cb.insuredtypecode,
                    BeneficiaryTypeCode = cb.beneficiarytypecode,
                    FirstName = cb.firstname,
                    MiddleName = cb.middlename,
                    LastName = cb.lastname,
                    Dob = cb.dob,
                    RelationshipTypeCode = cb.relationshiptypecode,
                    PercentValue = cb.percentvalue,
                    CustomerPlanbeneficiaryNo = cb.customerplanbeneficiaryno,
                    DateCreated = cb.datecreated,
                    CreatedBy = cb.createdby,
                    DateUpdated = cb.dateupdated,
                    UpdatedBy = cb.updatedby,
                    RCustomerPlanBeneficiaryNo = cb.rcustomerplanbeneficiaryno,
                    DateSynced = cb.datesynced,
                    RecordId = cb.recordid,
                    RelationshipType = cb.relationshiptype
                })
                .ToArray();

            return
                result;
        }

        public virtual Illustrator.CustomerPlanBeneficiary SetCustomerPlanBeneficiary(Illustrator.CustomerPlanBeneficiary beneficiary)
        {
            Illustrator.CustomerPlanBeneficiary result;
            IEnumerable<sp_set_customerplanbeneficiarydet_Result> temp;

            temp = illusDataModel.sp_set_customerplanbeneficiarydet(
                    beneficiary.CustomerPlanNo,
                    beneficiary.InsuredTypeCode,
                    beneficiary.BeneficiaryTypeCode,
                    beneficiary.FirstName,
                    beneficiary.MiddleName,
                    beneficiary.LastName,
                    beneficiary.Dob,
                    beneficiary.RelationshipTypeCode,
                    beneficiary.PercentValue,
                    beneficiary.CustomerPlanbeneficiaryNo,
                    beneficiary.DateCreated,
                    beneficiary.CreatedBy,
                    beneficiary.DateUpdated,
                    beneficiary.UpdatedBy,
                    beneficiary.RCustomerPlanBeneficiaryNo,
                    beneficiary.DateSynced,
                    beneficiary.RecordId,
                    beneficiary.SecondLastName //Lgonzalez 11-02-17
                )
                .ToArray();

            result = temp
                .Select(cb => new Illustrator.CustomerPlanBeneficiary
                {
                    CustomerPlanbeneficiaryNo = cb.customerplanbeneficiaryno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual int DeleteCustomerPlanBeneficiary(long customerplanbeneficiaryno)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanbeneficiarydet(customerplanbeneficiaryno);

            return
                result;
        }

        public virtual Illustrator.CustomerPlanPartnerInsurance GetCustomerPlanPartnerInsurance(long customerPlanNo)
        {
            Illustrator.CustomerPlanPartnerInsurance result;
            IEnumerable<sp_get_customerplanpartnerinsurancedet_Result> temp;

            temp = illusDataModel.sp_get_customerplanpartnerinsurancedet(customerPlanNo);

            result = temp
                .Select(pi => new Illustrator.CustomerPlanPartnerInsurance
                {
                    CustomerPlanNo = pi.customerplanno,
                    FirstName = pi.firstname,
                    MiddleName = pi.middlename,
                    LastName = pi.lastname,
                    InsuredAmount = pi.insuredamount,
                    Age = pi.age,
                    GenderCode = pi.gendercode,
                    MaritalStatusCode = pi.maritalstatuscode,
                    Smoker = pi.smoker,
                    ActivityRiskTypeNo = pi.activityrisktypeno,
                    HealthRiskTypeNo = pi.healthrisktypeno,
                    OtherPlans = pi.otherplans,
                    DateCreated = pi.datecreated,
                    CreatedBy = pi.createdby,
                    DateUpdated = pi.dateupdated,
                    UpdatedBy = pi.updatedby,
                    RideroirAmount = pi.rideroiramount,
                    UntilAge = pi.untilage,
                    RCustomerPlanPartnerInsuranceNo = pi.rcustomerplanpartnerinsuranceno,
                    RideroirCost = pi.rideroircost,
                    DateSynced = pi.datesynced,
                    RecordId = pi.recordid,
                    RelationshipTypeCode = pi.relationshiptypecode,
                    CustomerNo = pi.customerno,
                    ContributionTypeCode = pi.contributiontypecode,
                    LastName2 = pi.LastName2,
                    BirthDate = pi.Birthdate
                })
                .FirstOrDefault();
            //.ToArray();

            return
                result;
        }

        public virtual Illustrator.CustomerPlanPartnerInsurance SetCustomerPlanPartnerInsurance(Illustrator.CustomerPlanPartnerInsurance partnerInsurance)
        {
            Illustrator.CustomerPlanPartnerInsurance result;
            IEnumerable<sp_set_customerplanpartnerinsurancedet_Result> temp;

            temp = illusDataModel.sp_set_customerplanpartnerinsurancedet(
                    partnerInsurance.CustomerPlanNo,
                    partnerInsurance.FirstName,
                    partnerInsurance.MiddleName,
                    partnerInsurance.LastName,
                    partnerInsurance.InsuredAmount,
                    partnerInsurance.Age,
                    partnerInsurance.GenderCode,
                    partnerInsurance.MaritalStatusCode,
                    partnerInsurance.Smoker,
                    partnerInsurance.ActivityRiskTypeNo,
                    partnerInsurance.HealthRiskTypeNo,
                    partnerInsurance.OtherPlans,
                    partnerInsurance.DateCreated,
                    partnerInsurance.CreatedBy,
                    partnerInsurance.DateUpdated,
                    partnerInsurance.UpdatedBy,
                    partnerInsurance.RideroirAmount,
                    partnerInsurance.UntilAge,
                    partnerInsurance.RCustomerPlanPartnerInsuranceNo,
                    partnerInsurance.RideroirCost,
                    partnerInsurance.DateSynced,
                    partnerInsurance.RecordId,
                    partnerInsurance.RelationshipTypeCode,
                    partnerInsurance.CustomerNo,
                    partnerInsurance.ContributionTypeCode,
                    partnerInsurance.LastName2,
                    partnerInsurance.BirthDate
                )
                .ToArray();

            result = temp
                .Select(cb => new Illustrator.CustomerPlanPartnerInsurance
                {
                    CustomerPlanNo = cb.customerplanno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual int DeleteCustomerPlanPartnerInsurance(long customerPlanNo)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanpartnerinsurancedet(customerPlanNo);

            return
                result;
        }

        public virtual IEnumerable<Illustrator.CustomerPlanOtherInsurance> GetCustomerPlanOtherInsurance(long customerPlanNo, string insuredTypeCode)
        {
            IEnumerable<Illustrator.CustomerPlanOtherInsurance> result;
            IEnumerable<sp_get_customerplanotherinsurancedet_Result> temp;

            temp = illusDataModel.sp_get_customerplanotherinsurancedet(customerPlanNo, insuredTypeCode);

            result = temp
                .Select(cpoi => new Illustrator.CustomerPlanOtherInsurance
                {
                    CustomerPlanNo = cpoi.customerplanno,
                    InsuredTypeCode = cpoi.insuredtypecode,
                    ProductCode = cpoi.productcode,
                    InsuredAmount = cpoi.insuredamount,
                    AnnuityAmount = cpoi.annuityamount,
                    AnnuityPeriod = cpoi.annuityperiod,
                    CustomerPlanOtherInsuranceNo = cpoi.customerplanotherinsuranceno,
                    DateCreated = cpoi.datecreated,
                    CreatedBy = cpoi.createdby,
                    DateUpdated = cpoi.dateupdated,
                    UpdatedBy = cpoi.updatedby,
                    RCustomerPlanOtherInsuranceNo = cpoi.rcustomerplanotherinsuranceno,
                    DateSynced = cpoi.datesynced,
                    RecordId = cpoi.recordid,
                    Product = cpoi.product
                })
                //.FirstOrDefault();
                .ToArray();

            return
                result;
        }

        public virtual Illustrator.CustomerPlanOtherInsurance SetCustomerPlanOtherInsurance(Illustrator.CustomerPlanOtherInsurance customerPlanOtherInsurance)
        {
            Illustrator.CustomerPlanOtherInsurance result;
            IEnumerable<sp_set_customerplanotherinsurancedet_Result> temp;

            temp = illusDataModel.sp_set_customerplanotherinsurancedet(
                    customerPlanOtherInsurance.CustomerPlanNo,
                    customerPlanOtherInsurance.InsuredTypeCode,
                    customerPlanOtherInsurance.ProductCode,
                    customerPlanOtherInsurance.InsuredAmount,
                    customerPlanOtherInsurance.AnnuityAmount,
                    customerPlanOtherInsurance.AnnuityPeriod,
                    customerPlanOtherInsurance.CustomerPlanOtherInsuranceNo,
                    customerPlanOtherInsurance.DateCreated,
                    customerPlanOtherInsurance.CreatedBy,
                    customerPlanOtherInsurance.DateUpdated,
                    customerPlanOtherInsurance.UpdatedBy,
                    customerPlanOtherInsurance.RCustomerPlanOtherInsuranceNo,
                    customerPlanOtherInsurance.DateSynced,
                    customerPlanOtherInsurance.RecordId
                )
                .ToArray();

            result = temp
                .Select(cb => new Illustrator.CustomerPlanOtherInsurance
                {
                    CustomerPlanOtherInsuranceNo = cb.customerplanotherinsuranceno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual int DeleteCustomerPlanOtherInsurance(long customerPlanOtherInsuranceNo)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanotherinsurancedet(customerPlanOtherInsuranceNo);

            return
                result;
        }

        public virtual IEnumerable<Illustrator.CustomerPlanExam> GetCustomerPlanExam(long customerPlanNo, string insuredTypeCode)
        {
            IEnumerable<Illustrator.CustomerPlanExam> result;
            IEnumerable<sp_get_customerplanexamdet_Result> temp;

            temp = illusDataModel.sp_get_customerplanexamdet(customerPlanNo, insuredTypeCode);

            result = temp
                .Select(ec => new Illustrator.CustomerPlanExam
                {
                    ExamCode = ec.examcode,
                    DateCreated = ec.datecreated,
                    DateUpdated = ec.dateupdated,
                    CreatedBy = ec.createdby,
                    UpdatedBy = ec.updatedby,
                    CustomerExamNo = ec.customerexamno,
                    RCustomerExamNo = ec.rcustomerexamno,
                    CustomerPlanNo = ec.customerplanno,
                    InsuredTypeCode = ec.insuredtypecode,
                    DateSynced = ec.datesynced,
                    RecordId = ec.recordid,
                    ExamName = ec.examname
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.CustomerPlanExamCondition> GetCustomerExamCondition(string productCode, int age, string genderCode, string maritalStatusCode, decimal insuredAmount)
        {
            IEnumerable<Illustrator.CustomerPlanExamCondition> result;
            IEnumerable<sp_get_examcondition_Result> temp;

            temp = illusDataModel.sp_get_examcondition(productCode, age, genderCode, maritalStatusCode, insuredAmount);

            result = temp
                .Select(ec => new Illustrator.CustomerPlanExamCondition
                {
                    SNo = ec.sno,
                    ExamCode = ec.Examcode,
                    MinAge = ec.minage,
                    MaxAge = ec.maxage,
                    MinInsuredAmount = ec.mininsuredamount,
                    MaxInsuredAmount = ec.maxinsuredamount,
                    Randomness = ec.Randomness,
                    Active = ec.active,
                    DateCreated = ec.datecreated,
                    DateUpdated = ec.dateupdated,
                    CreatedBy = ec.createdby,
                    UpdatedBy = ec.updatedby,
                    ExamConditionNo = ec.examconditionno,
                    MaritalStatusCode = ec.maritalstatuscode,
                    GenderCode = ec.gendercode,
                    ProductCode = ec.productcode,
                    DateSynced = ec.datesynced,
                    ExamName = ec.examname
                })
                .ToArray();

            return
                result;
        }

        public virtual Illustrator.CustomerPlanExam SetCustomerPlanExam(Illustrator.CustomerPlanExam examCondition)
        {
            Illustrator.CustomerPlanExam result;
            IEnumerable<sp_set_customerplanexamdet_Result> temp;

            temp = illusDataModel.sp_set_customerplanexamdet(
                    examCondition.ExamCode,
                    examCondition.DateCreated,
                    examCondition.DateUpdated,
                    examCondition.CreatedBy,
                    examCondition.UpdatedBy,
                    examCondition.CustomerExamNo,
                    examCondition.RCustomerExamNo,
                    examCondition.CustomerPlanNo,
                    examCondition.InsuredTypeCode,
                    examCondition.DateSynced,
                    examCondition.RecordId
                )
                .ToArray();

            result = temp
                .Select(ec => new Illustrator.CustomerPlanExam
                {
                    CustomerExamNo = ec.customerexamno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual int DeleteCustomerPlanExam(long customerPlanNo, string insuredTypeCode, int userIdSystem)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanexamdet(customerPlanNo, insuredTypeCode, userIdSystem);

            return
                result;
        }

        public virtual decimal GetTotalInsuredAmount(long customerPlanNo, string insuredTypeCode)
        {
            decimal result;
            IEnumerable<sp_get_totalinsuredamount_Result> temp;

            temp = illusDataModel.sp_get_totalinsuredamount(customerPlanNo, insuredTypeCode).ToArray();

            result = temp.Any() ? temp.First().TotalInsuredAmount.ConvertToNoNullable() : -1;

            return
                result;
        }

        public virtual IEnumerable<Illustrator.RuleParameter> GetAllRuleParameter(int? ruleParameterNo, string productCode)
        {
            IEnumerable<Illustrator.RuleParameter> result;
            IEnumerable<sp_get_ruleparametervaluesdet_Result> temp;

            temp = illusDataModel.sp_get_ruleparametervaluesdet(ruleParameterNo, productCode);

            result = temp
                .Select(rp => new Illustrator.RuleParameter
                {
                    RuleParameterValueNo = rp.ruleparametervalueno,
                    RuleParameterNo = rp.ruleparameterno,
                    RuleParameterValue = rp.ruleparametervalue,
                    ProductCode = rp.productcode,
                    DateCreated = rp.datecreated,
                    DateUpdated = rp.dateupdated,
                    CreatedBy = rp.createdby,
                    UpdatedBy = rp.updatedby,
                    DateSynced = rp.datesynced,
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.CustomerPlaNopBal> GetCustomerPlaNopBal(long customerNo, long customerPlanNo)
        {
            IEnumerable<Illustrator.CustomerPlaNopBal> result;
            IEnumerable<sp_get_customerplanopbaldet_Result> temp;

            temp = illusDataModel.sp_get_customerplanopbaldet(customerNo, customerPlanNo);

            result = temp
                .Select(pnb => new Illustrator.CustomerPlaNopBal
                {
                    CustomerNo = pnb.customerno,
                    CustomerPlanNo = pnb.customerplanno,
                    Currentvalue = pnb.currentvalue,
                    PlanYear = pnb.planyear,
                    TargetAmount = pnb.targetamount,
                    MinimumPremium = pnb.minimumpremium,
                    FrequencyTypeCode = pnb.frequencytypecode,
                    PeriodicPremium = pnb.periodicpremium,
                    NooFunPaidPremiums = pnb.noofunpaidpremiums,
                    InvestmentProfileCode = pnb.investmentprofilecode,
                    MonthlyInsuranceCost = pnb.monthlyinsurancecost,
                    ForceAccountValue = pnb.forceaccountvalue,
                    OpeningBalance = pnb.openingbalance,
                    CalculateTypeObCode = pnb.calculatetypeobcode,
                    CreatedBy = pnb.createdby,
                    DateCreated = pnb.datecreated,
                    UpdatedBy = pnb.updatedby,
                    DateUpdated = pnb.dateupdated,
                    ProductCode = pnb.productcode,
                    DateSynced = pnb.datesynced,
                    RecordId = pnb.recordid,
                    TotalAmountPaid = pnb.totalamountpaid,
                    NoPaymentsRecieved = pnb.nopaymentsrecieved,
                    AmountDue = pnb.amountdue,
                    IllustrationMonth = pnb.illustrationmonth,
                    ForceTarget = pnb.forcetarget,
                    IsOverride = pnb.isoverride,
                    FullPaymentYear = pnb.fullpaymentyear,
                    ExpectedPremiums = pnb.expectedpremiums,
                    AdjustedAccountValue = pnb.adjustedaccountvalue,
                    InsuredAmount = pnb.insuredamount
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.CustomerPlanVarPremium> GetCustomerPlanVarPremium(long customerPlanNo)
        {
            IEnumerable<Illustrator.CustomerPlanVarPremium> result;
            IEnumerable<sp_get_customerplanvarpremiumdet_Result> temp;

            temp = illusDataModel.sp_get_customerplanvarpremiumdet(customerPlanNo);

            result = temp
                .Select(pvp => new Illustrator.CustomerPlanVarPremium
                {
                    CustomerPlanNo = pvp.customerplanno,
                    FromYearNo = pvp.fromyearno,
                    ToYearNo = pvp.toyearno,
                    PremiumAmount = pvp.premiumamount,
                    CustomerPlanVarPremiumNo = pvp.customerplanvarpremiumno,
                    DateCreated = pvp.datecreated,
                    CreatedBy = pvp.createdby,
                    DateUpdated = pvp.dateupdated,
                    UpdatedBy = pvp.updatedby,
                    SNo = pvp.sno,
                    RCustomerPlanVarPremiumNo = pvp.rcustomerplanvarpremiumno,
                    DateSynced = pvp.datesynced,
                    RecordId = pvp.recordid
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.CustomerPlanVarInsured> GetCustomerPlanVarInsured(long customerPlanNo)
        {
            IEnumerable<Illustrator.CustomerPlanVarInsured> result;
            IEnumerable<sp_get_customerplanvarinsureddet_Result> temp;

            temp = illusDataModel.sp_get_customerplanvarinsureddet(customerPlanNo);

            result = temp
                .Select(pvi => new Illustrator.CustomerPlanVarInsured
                {
                    CustomerPlanNo = pvi.customerplanno,
                    FromYearNo = pvi.fromyearno,
                    ToYearNo = pvi.toyearno,
                    InsuredAmount = pvi.insuredamount,
                    CustomerPlanVarInsuredNo = pvi.customerplanvarinsuredno,
                    DateCreated = pvi.datecreated,
                    CreatedBy = pvi.createdby,
                    DateUpdated = pvi.dateupdated,
                    UpdatedBy = pvi.updatedby,
                    SNo = pvi.sno,
                    RCustomerPlanVarInsuredNo = pvi.rcustomerplanvarinsuredno,
                    DateSynced = pvi.datesynced,
                    RecordId = pvi.recordid
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.CustomerPlanLoan> GetCustomerPlanLoan(long customerPlanNo)
        {
            IEnumerable<Illustrator.CustomerPlanLoan> result;
            IEnumerable<sp_get_customerplanloandet_Result> temp;

            temp = illusDataModel.sp_get_customerplanloandet(customerPlanNo);

            result = temp
                .Select(pl => new Illustrator.CustomerPlanLoan
                {
                    CustomerPlanNo = pl.customerplanno,
                    FromYearNo = pl.fromyearno,
                    ToYearNo = pl.toyearno,
                    LoanAmount = pl.loanamount,
                    CustomerPlanVarLoanNo = pl.customerplanvarloanno,
                    DateCreated = pl.datecreated,
                    CreatedBy = pl.createdby,
                    DateUpdated = pl.dateupdated,
                    UpdatedBy = pl.updatedby,
                    SNo = pl.sno,
                    RCustomerPlanLoanNo = pl.rcustomerplanloanno,
                    RCustomerPlanLoanRepayNo = pl.rcustomerplanloanrepayno,
                    DateSynced = pl.datesynced,
                    RecordId = pl.recordid
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.CustomerPlanVarSurrender> GetCustomerPlanVarSurrender(long customerPlanNo)
        {
            IEnumerable<Illustrator.CustomerPlanVarSurrender> result;
            IEnumerable<sp_get_customerplanvarsurrenderdet_Result> temp;

            temp = illusDataModel.sp_get_customerplanvarsurrenderdet(customerPlanNo);

            result = temp
                .Select(pvs => new Illustrator.CustomerPlanVarSurrender
                {
                    CustomerPlanNo = pvs.customerplanno,
                    FromYearNo = pvs.fromyearno,
                    ToYearNo = pvs.toyearno,
                    SurrenderAmount = pvs.surrenderamount,
                    CustomerPlanSurrenderNo = pvs.customerplansurrenderno,
                    DateCreated = pvs.datecreated,
                    CreatedBy = pvs.createdby,
                    DateUpdated = pvs.dateupdated,
                    UpdatedBy = pvs.updatedby,
                    SNo = pvs.sno,
                    RCustomerPlanVarSurrenderNo = pvs.rcustomerplanvarsurrenderno,
                    DateSynced = pvs.datesynced,
                    RecordId = pvs.recordid
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.CustomerPlanLoanRepay> GetCustomerPlanLoanRepay(long customerPlanNo)
        {
            IEnumerable<Illustrator.CustomerPlanLoanRepay> result;
            IEnumerable<sp_get_customerplanloanrepaydet_Result> temp;

            temp = illusDataModel.sp_get_customerplanloanrepaydet(customerPlanNo);

            result = temp
                .Select(plr => new Illustrator.CustomerPlanLoanRepay
                {
                    CustomerPlanNo = plr.customerplanno,
                    FromYearNo = plr.fromyearno,
                    ToYearNo = plr.toyearno,
                    PaymentAmount = plr.paymentamount,
                    CustomerPlanVarPaymentNo = plr.customerplanvarpaymentno,
                    DateCreated = plr.datecreated,
                    CreatedBy = plr.createdby,
                    DateUpdated = plr.dateupdated,
                    UpdatedBy = plr.updatedby,
                    SNo = plr.sno,
                    DateSynced = plr.datesynced,
                    RecordId = plr.recordid
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.CustomerPlanVarProfile> GetCustomerPlanVarProfile(long customerPlanNo)
        {
            IEnumerable<Illustrator.CustomerPlanVarProfile> result;
            IEnumerable<sp_get_customerplanvarprofiledet_Result> temp;

            temp = illusDataModel.sp_get_customerplanvarprofiledet(customerPlanNo);

            result = temp
                .Select(pvp => new Illustrator.CustomerPlanVarProfile
                {
                    CustomerPlanNo = pvp.customerplanno,
                    FromYearNo = pvp.fromyearno,
                    ToYearNo = pvp.toyearno,
                    InvestmentProfileCode = pvp.investmentprofilecode,
                    CustomerPlanVarProfileNo = pvp.customerplanvarprofileno,
                    DateCreated = pvp.datecreated,
                    CreatedBy = pvp.createdby,
                    DateUpdated = pvp.dateupdated,
                    UpdatedBy = pvp.updatedby,
                    SNo = pvp.sno,
                    RCustomerPlanVarProfileDetNo = pvp.rcustomerplanvarprofiledetno,
                    DateSynced = pvp.datesynced,
                    RecordId = pvp.recordid
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.InvProfileCompareRates> GetInvProfileCompareRates(string classCode, string productCode, string investmentProfileCode)
        {
            IEnumerable<Illustrator.InvProfileCompareRates> result;
            IEnumerable<sp_get_invprofilecompareratesdet_Result> temp;

            temp = illusDataModel.sp_get_invprofilecompareratesdet(classCode, productCode, investmentProfileCode);

            result = temp
                .Select(cr => new Illustrator.InvProfileCompareRates
                {
                    InvestmentProfileCode = cr.investmentprofilecode,
                    ProductCode = cr.productcode,
                    GrowthRate1 = cr.growthrate1,
                    GrowthRate2 = cr.growthrate2,
                    ClassCode = cr.classcode,
                    DateCreated = cr.datecreated,
                    DateUpdated = cr.dateupdated,
                    DateSynced = cr.datesynced,
                    InvprofileComparerateNo = cr.invprofilecomparerateno
                })
                .ToArray();

            return
                result;
        }

        public virtual Illustrator.CustomerPlaNopBal SetCustomerPlaNopBal(Illustrator.CustomerPlaNopBal plaNopBal)
        {
            Illustrator.CustomerPlaNopBal result;
            IEnumerable<sp_set_customerplanopbaldet_Result> temp;

            temp = illusDataModel.sp_set_customerplanopbaldet(
                    plaNopBal.CustomerNo,
                    plaNopBal.CustomerPlanNo,
                    plaNopBal.Currentvalue,
                    plaNopBal.PlanYear,
                    plaNopBal.TargetAmount,
                    plaNopBal.MinimumPremium,
                    plaNopBal.FrequencyTypeCode,
                    plaNopBal.PeriodicPremium,
                    plaNopBal.NooFunPaidPremiums,
                    plaNopBal.InvestmentProfileCode,
                    plaNopBal.MonthlyInsuranceCost,
                    plaNopBal.ForceAccountValue,
                    plaNopBal.OpeningBalance,
                    plaNopBal.CalculateTypeObCode,
                    plaNopBal.CreatedBy,
                    plaNopBal.DateCreated,
                    plaNopBal.UpdatedBy,
                    plaNopBal.DateUpdated,
                    plaNopBal.ProductCode,
                    plaNopBal.DateSynced,
                    plaNopBal.RecordId,
                    plaNopBal.TotalAmountPaid,
                    plaNopBal.NoPaymentsRecieved,
                    plaNopBal.AmountDue,
                    plaNopBal.IllustrationMonth,
                    plaNopBal.ForceTarget,
                    plaNopBal.IsOverride,
                    plaNopBal.FullPaymentYear,
                    plaNopBal.ExpectedPremiums,
                    plaNopBal.AdjustedAccountValue,
                    plaNopBal.InsuredAmount
                );

            result = temp
                .Select(nb => new Illustrator.CustomerPlaNopBal
                {
                    CustomerPlanNo = nb.customerplanno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual Illustrator.CustomerPlanVarPremium SetCustomerPlanVarPremium(Illustrator.CustomerPlanVarPremium planVarPremium)
        {
            Illustrator.CustomerPlanVarPremium result;
            IEnumerable<sp_set_customerplanvarpremiumdet_Result> temp;

            temp = illusDataModel.sp_set_customerplanvarpremiumdet(
                    planVarPremium.CustomerPlanNo,
                    planVarPremium.FromYearNo,
                    planVarPremium.ToYearNo,
                    planVarPremium.PremiumAmount,
                    planVarPremium.CustomerPlanVarPremiumNo,
                    planVarPremium.DateCreated,
                    planVarPremium.CreatedBy,
                    planVarPremium.DateUpdated,
                    planVarPremium.UpdatedBy,
                    planVarPremium.SNo,
                    planVarPremium.RCustomerPlanVarPremiumNo,
                    planVarPremium.DateSynced,
                    planVarPremium.RecordId
                )
                .ToArray();

            result = temp
                .Select(vp => new Illustrator.CustomerPlanVarPremium
                {
                    CustomerPlanVarPremiumNo = vp.customerplanvarpremiumno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual Illustrator.CustomerPlanVarInsured SetCustomerPlanVarInsured(Illustrator.CustomerPlanVarInsured planVarInsured)
        {
            Illustrator.CustomerPlanVarInsured result;
            IEnumerable<sp_set_customerplanvarinsureddet_Result> temp;

            temp = illusDataModel.sp_set_customerplanvarinsureddet(
                    planVarInsured.CustomerPlanNo,
                    planVarInsured.FromYearNo,
                    planVarInsured.ToYearNo,
                    planVarInsured.InsuredAmount,
                    planVarInsured.CustomerPlanVarInsuredNo,
                    planVarInsured.DateCreated,
                    planVarInsured.CreatedBy,
                    planVarInsured.DateUpdated,
                    planVarInsured.UpdatedBy,
                    planVarInsured.SNo,
                    planVarInsured.RCustomerPlanVarInsuredNo,
                    planVarInsured.DateSynced,
                    planVarInsured.RecordId
                )
                .ToArray();

            result = temp
                .Select(vi => new Illustrator.CustomerPlanVarInsured
                {
                    CustomerPlanVarInsuredNo = vi.customerplanvarinsuredno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual Illustrator.CustomerPlanLoan SetCustomerPlanLoan(Illustrator.CustomerPlanLoan planLoan)
        {
            Illustrator.CustomerPlanLoan result;
            IEnumerable<sp_set_customerplanloandet_Result> temp;

            temp = illusDataModel.sp_set_customerplanloandet(
                    planLoan.CustomerPlanNo,
                    planLoan.FromYearNo,
                    planLoan.ToYearNo,
                    planLoan.LoanAmount,
                    planLoan.CustomerPlanVarLoanNo,
                    planLoan.DateCreated,
                    planLoan.CreatedBy,
                    planLoan.DateUpdated,
                    planLoan.UpdatedBy,
                    planLoan.SNo,
                    planLoan.RCustomerPlanLoanNo,
                    planLoan.RCustomerPlanLoanRepayNo,
                    planLoan.DateSynced,
                    planLoan.RecordId
                )
                .ToArray();

            result = temp
                .Select(l => new Illustrator.CustomerPlanLoan
                {
                    CustomerPlanVarLoanNo = l.customerplanvarloanno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual Illustrator.CustomerPlanVarSurrender SetCustomerPlanVarSurrender(Illustrator.CustomerPlanVarSurrender planVarSurrender)
        {
            Illustrator.CustomerPlanVarSurrender result;
            IEnumerable<sp_set_customerplanvarsurrenderdet_Result> temp;

            temp = illusDataModel.sp_set_customerplanvarsurrenderdet(
                    planVarSurrender.CustomerPlanNo,
                    planVarSurrender.FromYearNo,
                    planVarSurrender.ToYearNo,
                    planVarSurrender.SurrenderAmount,
                    planVarSurrender.CustomerPlanSurrenderNo,
                    planVarSurrender.DateCreated,
                    planVarSurrender.CreatedBy,
                    planVarSurrender.DateUpdated,
                    planVarSurrender.UpdatedBy,
                    planVarSurrender.SNo,
                    planVarSurrender.RCustomerPlanVarSurrenderNo,
                    planVarSurrender.DateSynced,
                    planVarSurrender.RecordId
                )
                .ToArray();

            result = temp
                .Select(ec => new Illustrator.CustomerPlanVarSurrender
                {
                    CustomerPlanSurrenderNo = ec.customerplansurrenderno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual Illustrator.CustomerPlanLoanRepay SetCustomerPlanLoanRepay(Illustrator.CustomerPlanLoanRepay planLoanRepay)
        {
            Illustrator.CustomerPlanLoanRepay result;
            IEnumerable<sp_set_customerplanloanrepaydet_Result> temp;

            temp = illusDataModel.sp_set_customerplanloanrepaydet(
                    planLoanRepay.CustomerPlanNo,
                    planLoanRepay.FromYearNo,
                    planLoanRepay.ToYearNo,
                    planLoanRepay.PaymentAmount,
                    planLoanRepay.CustomerPlanVarPaymentNo,
                    planLoanRepay.DateCreated,
                    planLoanRepay.CreatedBy,
                    planLoanRepay.DateUpdated,
                    planLoanRepay.UpdatedBy,
                    planLoanRepay.SNo,
                    planLoanRepay.DateSynced,
                    planLoanRepay.RecordId
                )
                .ToArray();

            result = temp
                .Select(ec => new Illustrator.CustomerPlanLoanRepay
                {
                    CustomerPlanVarPaymentNo = ec.customerplanvarpaymentno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual Illustrator.CustomerPlanVarProfile SetCustomerPlanVarProfile(Illustrator.CustomerPlanVarProfile compareRates)
        {
            Illustrator.CustomerPlanVarProfile result;
            IEnumerable<sp_set_customerplanvarprofiledet_Result> temp;

            temp = illusDataModel.sp_set_customerplanvarprofiledet(
                    compareRates.CustomerPlanNo,
                    compareRates.FromYearNo,
                    compareRates.ToYearNo,
                    compareRates.InvestmentProfileCode,
                    compareRates.CustomerPlanVarProfileNo,
                    compareRates.DateCreated,
                    compareRates.CreatedBy,
                    compareRates.DateUpdated,
                    compareRates.UpdatedBy,
                    compareRates.SNo,
                    compareRates.RCustomerPlanVarProfileDetNo,
                    compareRates.DateSynced,
                    compareRates.RecordId
                )
                .ToArray();

            result = temp
                .Select(ec => new Illustrator.CustomerPlanVarProfile
                {
                    CustomerPlanVarProfileNo = ec.customerplanvarprofileno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual int DeleteCustomerPlaNopBal(long customerPlanNo)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanopbaldet(customerPlanNo);

            return
                result;
        }
        public virtual int DeleteCustomerPlanVarPremium(long customerPlanNo)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanvarpremiumdet(customerPlanNo);

            return
                result;
        }
        public virtual int DeleteCustomerPlanVarInsured(long customerPlanNo)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanvarinsureddet(customerPlanNo);

            return
                result;
        }
        public virtual int DeleteCustomerPlanLoan(long customerPlanNo)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanloandet(customerPlanNo);

            return
                result;
        }
        public virtual int DeleteCustomerPlanVarSurrender(long customerPlanNo)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanvarsurrenderdet(customerPlanNo);

            return
                result;
        }
        public virtual int DeleteCustomerPlanLoanRepay(long customerPlanNo)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanloanrepaydet(customerPlanNo);

            return
                result;
        }
        public virtual int DeleteCustomerPlanVarProfile(long customerPlanNo)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanvarprofiledet(customerPlanNo);

            return
                result;
        }

        public virtual IEnumerable<Illustrator.ProductCancel> GetProductCancel(string productCode, int retirementPeriod)
        {
            IEnumerable<Illustrator.ProductCancel> result;
            IEnumerable<sp_get_productcanceldet_Result> temp;

            temp = illusDataModel.sp_get_productcanceldet(productCode, retirementPeriod);

            result = temp
                .Select(pc => new Illustrator.ProductCancel
                {
                    ProductCode = pc.productcode,
                    FromContributionPriod = pc.fromcontributionpriod,
                    ToContributionPriod = pc.tocontributionpriod,
                    ProductCancelNo = pc.productcancelno
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.ProductCancelDetail> GetProductCancelDetail(int productCancelNo)
        {
            IEnumerable<Illustrator.ProductCancelDetail> result;
            IEnumerable<sp_get_productcanceldetailsdet_Result> temp;

            temp = illusDataModel.sp_get_productcanceldetailsdet(productCancelNo);

            result = temp
                .Select(pcd => new Illustrator.ProductCancelDetail
                {
                    ProductCancelNo = pcd.productcancelno,
                    YearNo = pcd.yearno,
                    CancelPercent = pcd.cancelpercent,
                    ProductCancelDetailNo = pcd.productcanceldetailno
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.FrequencyCostDetail> GetFrequencyCost(string productCode, string frequencyTypeCode)
        {
            IEnumerable<Illustrator.FrequencyCostDetail> result;
            IEnumerable<sp_get_frequencycostdet_Result> temp;

            temp = illusDataModel.sp_get_frequencycostdet(productCode, frequencyTypeCode);

            result = temp
                .Select(fc => new Illustrator.FrequencyCostDetail
                {
                    ProductCode = fc.productcode,
                    FrequencyTypeCode = fc.frequencytypecode,
                    FrequencyCost = fc.frequencycost,
                    FrequencyCostNo = fc.frequencycostno
                })
                .ToArray();

            return
                result;
        }

        public virtual int DeleteCustomerPlanTerm(long customerPlanNo)
        {
            int result;

            result = illusDataModel.sp_delete_customerplantermdet(customerPlanNo);

            return
                result;
        }
        public virtual int DeleteCustomerPlanAnnuity(long customerPlanNo)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanannuitydet(customerPlanNo);

            return
                result;
        }
        public virtual int DeleteCustomerPlanLife(long customerPlanNo)
        {
            int result;

            result = illusDataModel.sp_delete_customerplanlifedet(customerPlanNo);

            return
                result;
        }

        public virtual IEnumerable<Illustrator.CustomerPlanTerm> GetCustomerPlanTerm(long customerPlanNo)
        {
            IEnumerable<Illustrator.CustomerPlanTerm> result;
            IEnumerable<sp_get_customerplantermdet_Result> temp;

            temp = illusDataModel.sp_get_customerplantermdet(customerPlanNo);

            result = temp
                .Select(pt => new Illustrator.CustomerPlanTerm
                {
                    CustomerPlanTermNo = pt.customerplantermno,
                    CustomerPlanNo = pt.customerplanno,
                    TableNo = pt.tableno,
                    Age = pt.age,
                    Year = pt.year,
                    PrimaBasicCoverage = pt.primabasiccoverage,
                    PremiumExtras = pt.premiumextras,
                    TotalPremium = pt.totalpremium,
                    AccumulatedPremiums = pt.accumulatedpremiums,
                    DeathBenefit = pt.deathbenefit
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.CustomerPlanAnnuity> GetCustomerPlanAnnuity(long customerPlanNo)
        {
            IEnumerable<Illustrator.CustomerPlanAnnuity> result;
            IEnumerable<sp_get_customerplanannuitydet_Result> temp;

            temp = illusDataModel.sp_get_customerplanannuitydet(customerPlanNo);

            result = temp
                .Select(pa => new Illustrator.CustomerPlanAnnuity
                {
                    CustomerPlanAnnuityNo = pa.customerplanannuityno,
                    CustomerPlanNo = pa.customerplanno,
                    Age = pa.age,
                    Year = pa.year,
                    AccumulatedContributions = pa.accumulatedcontributions,
                    DeathBenefit = pa.deathbenefit,
                    BenefitExclusion = pa.benefitexclusion,
                    AccountValue = pa.accountvalue,
                    SurrenderValue = pa.surrendervalue,
                    AnnualPartialWithDrawal = pa.annualpartialwithdrawal
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.CustomerPlanLife> GetCustomerPlanLife(long customerPlanNo)
        {
            IEnumerable<Illustrator.CustomerPlanLife> result;
            IEnumerable<sp_get_customerplanlifedet_Result> temp;

            temp = illusDataModel.sp_get_customerplanlifedet(customerPlanNo);

            result = temp
                .Select(pl => new Illustrator.CustomerPlanLife
                {
                    CustomerPlanLifeNo = pl.customerplanlifeno,
                    CustomerPlanNo = pl.customerplanno,
                    Age = pl.age,
                    Year = pl.year,
                    Premium = pl.premium,
                    AccountValue1 = pl.accountvalue1,
                    SurrenderValue1 = pl.surrendervalue1,
                    DeathBenefit1 = pl.deathbenefit1,
                    AccountValue2 = pl.accountvalue2,
                    SurrenderValue2 = pl.surrendervalue2,
                    DeathBenefit2 = pl.deathbenefit2,
                    AccountValue3 = pl.accountvalue3,
                    SurrenderValue3 = pl.surrendervalue3,
                    DeathBenefit3 = pl.deathbenefit3
                })
                .ToArray();

            return
                result;
        }

        public virtual Illustrator.CustomerPlanTerm SetCustomerPlanTerm(Illustrator.CustomerPlanTerm planTerm)
        {
            Illustrator.CustomerPlanTerm result;
            IEnumerable<sp_set_customerplantermdet_Result> temp;

            temp = illusDataModel.sp_set_customerplantermdet(
                    planTerm.CustomerPlanTermNo,
                    planTerm.CustomerPlanNo,
                    planTerm.TableNo,
                    planTerm.Age,
                    planTerm.Year,
                    planTerm.PrimaBasicCoverage,
                    planTerm.PremiumExtras,
                    planTerm.TotalPremium,
                    planTerm.AccumulatedPremiums,
                    planTerm.DeathBenefit
                );

            result = temp
                .Select(nb => new Illustrator.CustomerPlanTerm
                {
                    CustomerPlanTermNo = nb.customerplantermno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual Illustrator.CustomerPlanAnnuity SetCustomerPlanAnnuity(Illustrator.CustomerPlanAnnuity planAnnuity)
        {
            Illustrator.CustomerPlanAnnuity result;
            IEnumerable<sp_set_customerplanannuitydet_Result> temp;

            temp = illusDataModel.sp_set_customerplanannuitydet(
                    planAnnuity.CustomerPlanAnnuityNo,
                    planAnnuity.CustomerPlanNo,
                    planAnnuity.Age,
                    planAnnuity.Year,
                    planAnnuity.AccumulatedContributions,
                    planAnnuity.DeathBenefit,
                    planAnnuity.BenefitExclusion,
                    planAnnuity.AccountValue,
                    planAnnuity.SurrenderValue,
                    planAnnuity.AnnualPartialWithDrawal
                );

            result = temp
                .Select(nb => new Illustrator.CustomerPlanAnnuity
                {
                    CustomerPlanAnnuityNo = nb.customerplanannuityno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual Illustrator.CustomerPlanLife SetCustomerPlanLife(Illustrator.CustomerPlanLife planLife)
        {
            Illustrator.CustomerPlanLife result;
            IEnumerable<sp_set_customerplanlifedet_Result> temp;

            temp = illusDataModel.sp_set_customerplanlifedet(
                    planLife.CustomerPlanLifeNo,
                    planLife.CustomerPlanNo,
                    planLife.Age,
                    planLife.Year,
                    planLife.Premium,
                    planLife.AccountValue1,
                    planLife.SurrenderValue1,
                    planLife.DeathBenefit1,
                    planLife.AccountValue2,
                    planLife.SurrenderValue2,
                    planLife.DeathBenefit2,
                    planLife.AccountValue3,
                    planLife.SurrenderValue3,
                    planLife.DeathBenefit3
                );

            result = temp
                .Select(nb => new Illustrator.CustomerPlanLife
                {
                    CustomerPlanLifeNo = nb.customerplanlifeno.ConvertToNoNullable()
                })
                .FirstOrDefault();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.InvestmentsInflacion> GetRptInvestmentsInflacion()
        {
            IEnumerable<Illustrator.InvestmentsInflacion> result;
            IEnumerable<sp_get_rpt_investments_inflacion_Result> temp;

            temp = illusDataModel.sp_get_rpt_investments_inflacion();

            result = temp
                .Select(ii => new Illustrator.InvestmentsInflacion
                {
                    Sno = ii.Sno,
                    Years = ii.Years,
                    Pequenas_Acciones = ii.Pequenas_Acciones,
                    Grandes_Acciones = ii.Grandes_Acciones,
                    Bonosdel_Gobierno = ii.Bonosdel_Gobierno,
                    Papelesdel_Tesoro = ii.Papelesdel_Tesoro,
                    Inflacion = ii.Inflacion,
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.InvestmentsType> GetRptInvestmentsType(string fundType, string fundCategory, string region)
        {
            IEnumerable<Illustrator.InvestmentsType> result;
            IEnumerable<sp_get_rpt_InvestType_Result> temp;

            temp = illusDataModel.sp_get_rpt_InvestType(fundType, fundCategory, region);

            result = temp
                .Select(it => new Illustrator.InvestmentsType
                {
                    FundName = it.FundName,
                    FundValue = it.FundValue,
                    FundType = it.FundType,
                    FundCategory = it.FundCategory,
                    Region = it.Region
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.InvestmentsCompass> GetRptInvestmentsCompass(int ReturnTypeid)
        {
            IEnumerable<Illustrator.InvestmentsCompass> result;
            IEnumerable<sp_get_rpt_compass_investment_details_Result> temp;

            temp = illusDataModel.sp_get_rpt_compass_investment_details(ReturnTypeid);

            result = temp
                .Select(ic => new Illustrator.InvestmentsCompass
                {
                    sno1 = ic.sno1,
                    ReturnTypeid = ic.ReturnTypeid,
                    Years = ic.Years,
                    ReturnValue = ic.ReturnValue,
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.InvestmentsSlide3> GetRptInvestmentsSlide3()
        {
            IEnumerable<Illustrator.InvestmentsSlide3> result;
            IEnumerable<sp_get_rpt_investment_slide3_Result> temp;

            temp = illusDataModel.sp_get_rpt_investment_slide3();

            result = temp
                .Select(is3 => new Illustrator.InvestmentsSlide3
                {
                    Sno = is3.Sno,
                    Type = is3.Type,
                    Percentage = is3.Percentage
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.InvestmentsSlide4> GetRptInvestmentsSlide4()
        {
            IEnumerable<Illustrator.InvestmentsSlide4> result;
            IEnumerable<sp_get_rpt_investment_slide4_Result> temp;

            temp = illusDataModel.sp_get_rpt_investment_slide4();

            result = temp
                .Select(is4 => new Illustrator.InvestmentsSlide4
                {
                    Sno = is4.Sno,
                    Risk = is4.Risk,
                    Return = is4.Return,
                    Stocks = is4.Stocks,
                    Bonds = is4.Bonds
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.InvestmentsSlide5Chart1> GetRptInvestmentsSlide5Chart1()
        {
            IEnumerable<Illustrator.InvestmentsSlide5Chart1> result;
            IEnumerable<sp_get_rpt_investment_slide5_chart1_Result> temp;

            temp = illusDataModel.sp_get_rpt_investment_slide5_chart1();

            result = temp
                .Select(is5c1 => new Illustrator.InvestmentsSlide5Chart1
                {
                    Sno = is5c1.Sno,
                    Years = is5c1.Years,
                    Acciones = is5c1.Acciones,
                    Portafolio_Diversificado = is5c1.Portafolio_Diversificado
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.InvestmentsSlide5Chart2> GetRptInvestmentsSlide5Chart2()
        {
            IEnumerable<Illustrator.InvestmentsSlide5Chart2> result;
            IEnumerable<sp_get_rpt_investment_slide5_chart2_Result> temp;

            temp = illusDataModel.sp_get_rpt_investment_slide5_chart2();

            result = temp
                .Select(is5c2 => new Illustrator.InvestmentsSlide5Chart2
                {
                    Sno = is5c2.Sno,
                    Years = is5c2.Years,
                    Acciones = is5c2.Acciones,
                    Portafolio_Diversificado = is5c2.Portafolio_Diversificado
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.InvestmentsSlide6> GetRptInvestmentsSlide6()
        {
            IEnumerable<Illustrator.InvestmentsSlide6> result;
            IEnumerable<sp_get_rpt_investment_slide6_Result> temp;

            temp = illusDataModel.sp_get_rpt_investment_slide6();

            result = temp
                .Select(is6 => new Illustrator.InvestmentsSlide6
                {
                    Sno = is6.Sno,
                    Type = is6.Type,
                    Riesgo = is6.Riesgo,
                    Rendimiento = is6.Rendimiento
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.InvestmentsReturns> GetRptInvestmentsReturns()
        {
            IEnumerable<Illustrator.InvestmentsReturns> result;
            IEnumerable<sp_get_rpt_returns_on_investments_Result> temp;

            temp = illusDataModel.sp_get_rpt_returns_on_investments();

            result = temp
                .Select(ir => new Illustrator.InvestmentsReturns
                {
                    SNo = ir.SNo,
                    ReturnType = ir.ReturnType,
                    ReturnValue = ir.ReturnValue,
                    Region = ir.Region
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.InvestmentsProfile> GetInvestmentsProfile()
        {
            IEnumerable<Illustrator.InvestmentsProfile> result;
            IEnumerable<sp_get_profile_de_inversion_Result> temp;

            temp = illusDataModel.sp_get_profile_de_inversion();

            result = temp
                .Select(ip => new Illustrator.InvestmentsProfile
                {
                    region = ip.region,
                    cricimiento = ip.cricimiento,
                    balancedo = ip.balancedo,
                    Moderado = ip.Moderado,
                    stl = ip.stl,
                    categoria = ip.categoria,
                    simbolo = ip.simbolo,
                    nombre_del_indice = ip.nombre_del_indice,
                    year2008 = ip.year2008,
                    year2009 = ip.year2009,
                    fstterm = ip.fstterm,
                    secndterm = ip.secndterm,
                    trdterm = ip.trdterm,
                    year2006 = ip.year2006,
                    year2007 = ip.year2007,
                    year2010 = ip.year2010
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.InvestmentsProfileEuro> GetInvestmentsProfileEuro()
        {
            IEnumerable<Illustrator.InvestmentsProfileEuro> result;
            IEnumerable<sp_get_profile_de_inversion_euro_Result> temp;

            temp = illusDataModel.sp_get_profile_de_inversion_euro();

            result = temp
                .Select(ipe => new Illustrator.InvestmentsProfileEuro
                {
                    region = ipe.region,
                    cricimiento = ipe.cricimiento,
                    balancedo = ipe.balancedo,
                    Moderado = ipe.Moderado,
                    stl = ipe.stl,
                    categoria = ipe.categoria,
                    simbolo = ipe.simbolo,
                    nombre_del_indice = ipe.nombre_del_indice,
                    year2008 = ipe.year2008,
                    year2009 = ipe.year2009,
                    fstterm = ipe.fstterm,
                    secndterm = ipe.secndterm,
                    trdterm = ipe.trdterm,
                    year2006 = ipe.year2006,
                    year2007 = ipe.year2007,
                    year2010 = ipe.year2010
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.RptAxysFixedinComeSlide12> GetRptAxysFixedinComeSlide12()
        {
            IEnumerable<Illustrator.RptAxysFixedinComeSlide12> result;
            IEnumerable<sp_get_rpt_Axys_fixedincome_slide12_Result> temp;

            temp = illusDataModel.sp_get_rpt_Axys_fixedincome_slide12();

            result = temp
                .Select(afcs12 => new Illustrator.RptAxysFixedinComeSlide12
                {
                    Sno = afcs12.Sno,
                    Cash = afcs12.Cash,
                    Bond = afcs12.bond,
                    Yield = afcs12.Yield,
                    Risk = afcs12.Risk
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.RptAxysHighperFormSlide12> GetRptAxysHighperFormSlide12()
        {
            IEnumerable<Illustrator.RptAxysHighperFormSlide12> result;
            IEnumerable<sp_get_rpt_Axys_Highperform_slide12_Result> temp;

            temp = illusDataModel.sp_get_rpt_Axys_Highperform_slide12();

            result = temp
                .Select(ahfs12 => new Illustrator.RptAxysHighperFormSlide12
                {
                    Sno = ahfs12.Sno,
                    Cash = ahfs12.Cash,
                    bond = ahfs12.bond,
                    Actions = ahfs12.Actions,
                    Yield = ahfs12.Yield,
                    Risk = ahfs12.Risk
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.RptAxysLowRiskSlide12> GetRptAxysLowRiskSlide12()
        {
            IEnumerable<Illustrator.RptAxysLowRiskSlide12> result;
            IEnumerable<sp_get_rpt_Axys_lowrisk_slide12_Result> temp;

            temp = illusDataModel.sp_get_rpt_Axys_lowrisk_slide12();

            result = temp
                .Select(alrs12 => new Illustrator.RptAxysLowRiskSlide12
                {
                    Sno = alrs12.Sno,
                    Cash = alrs12.Cash,
                    bond = alrs12.bond,
                    Actions = alrs12.Actions,
                    Yield = alrs12.Yield,
                    Risk = alrs12.Risk
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.RptAxysSlide10> GetRptAxysSlide10()
        {
            IEnumerable<Illustrator.RptAxysSlide10> result;
            IEnumerable<sp_get_rpt_Axys_slide10_Result> temp;

            temp = illusDataModel.sp_get_rpt_Axys_slide10();

            result = temp
                .Select(as10 => new Illustrator.RptAxysSlide10
                {
                    sno = as10.sno,
                    Category = as10.Category,
                    Percentage = as10.Percentage
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.RptAxysSlide11> GetRptAxysSlide11()
        {
            IEnumerable<Illustrator.RptAxysSlide11> result;
            IEnumerable<sp_get_rpt_Axys_slide11_Result> temp;

            temp = illusDataModel.sp_get_rpt_Axys_slide11();

            result = temp
                .Select(as11 => new Illustrator.RptAxysSlide11
                {
                    Sno = as11.Sno,
                    Type = as11.Type,
                    Years = as11.Years,
                    MaxYear = as11.MaxYear,
                    MinYear = as11.MinYear,
                    Compound_Percent = as11.Compound_Percent
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.RptAxysSlide5> GetRptAxysSlide5()
        {
            IEnumerable<Illustrator.RptAxysSlide5> result;
            IEnumerable<sp_get_rpt_axys_slide5_Result> temp;

            temp = illusDataModel.sp_get_rpt_axys_slide5();

            result = temp
                .Select(as5 => new Illustrator.RptAxysSlide5
                {
                    Year = as5.Year,
                    Saving_Rate = as5.Saving_Rate,
                    sno = as5.sno
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.RptAxysSlide6> GetRptAxysSlide6()
        {
            IEnumerable<Illustrator.RptAxysSlide6> result;
            IEnumerable<sp_get_rpt_axys_slide6_Result> temp;

            temp = illusDataModel.sp_get_rpt_axys_slide6();

            result = temp
                .Select(as6 => new Illustrator.RptAxysSlide6
                {
                    sno = as6.sno,
                    Year = as6.Year,
                    Miles_USD = as6.Miles_USD
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.RptAxysSlide8> GetRptAxysSlide8()
        {
            IEnumerable<Illustrator.RptAxysSlide8> result;
            IEnumerable<sp_get_rpt_axys_slide8_Result> temp;

            temp = illusDataModel.sp_get_rpt_axys_slide8();

            result = temp
                .Select(as8 => new Illustrator.RptAxysSlide8
                {
                    sno = as8.sno,
                    category = as8.category,
                    Percent = as8.Percent
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.EgrAge> GetEgrAge()
        {
            IEnumerable<Illustrator.EgrAge> result;
            IEnumerable<sp_get_egr_age_Result> temp;

            temp = illusDataModel.sp_get_egr_age();

            result = temp
                .Select(ea => new Illustrator.EgrAge
                {
                    sno = ea.sno,
                    age = ea.age,
                    Amount = ea.Amount
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.EgrSlide7> GetEgrSlide7()
        {
            IEnumerable<Illustrator.EgrSlide7> result;
            IEnumerable<sp_get_egr_slide7_Result> temp;

            temp = illusDataModel.sp_get_egr_slide7();

            result = temp
                .Select(es7 => new Illustrator.EgrSlide7
                {
                    Sno = es7.Sno,
                    catogory = es7.catogory,
                    percent = es7.percent,
                    average = es7.average
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.EgrSlide8> GetEgrSlide8()
        {
            IEnumerable<Illustrator.EgrSlide8> result;
            IEnumerable<sp_get_egr_slide8_Result> temp;

            temp = illusDataModel.sp_get_egr_slide8();

            result = temp
                .Select(es8 => new Illustrator.EgrSlide8
                {
                    Sno = es8.Sno,
                    catogory = es8.catogory,
                    Amount = es8.Amount
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.EgrSlide9> GetEgrSlide9()
        {
            IEnumerable<Illustrator.EgrSlide9> result;
            IEnumerable<sp_get_egr_slide9_Result> temp;

            temp = illusDataModel.sp_get_egr_slide9();

            result = temp
                .Select(es9 => new Illustrator.EgrSlide9
                {
                    Sno = es9.Sno,
                    catogory = es9.catogory,
                    Amount = es9.Amount
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.EgrSlide10> GetEgrSlide10()
        {
            IEnumerable<Illustrator.EgrSlide10> result;
            IEnumerable<sp_get_egr_slide10_Result> temp;

            temp = illusDataModel.sp_get_egr_slide10();

            result = temp
                .Select(es10 => new Illustrator.EgrSlide10
                {
                    sno = es10.sno,
                    Year = es10.Year,
                    Percent = es10.Percent,
                    type = es10.type
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.RptLegacy10Priciple> GetRptLegacy10Priciple()
        {
            IEnumerable<Illustrator.RptLegacy10Priciple> result;
            IEnumerable<sp_get_rpt_Legacy_10_principles_Result> temp;

            temp = illusDataModel.sp_get_rpt_Legacy_10_principles();

            result = temp
                .Select(l10p => new Illustrator.RptLegacy10Priciple
                {
                    sno = l10p.sno,
                    num = l10p.num,
                    Causesde_Falle = l10p.Causesde_Falle,
                    Paisesde_Bajos_millones = l10p.Paisesde_Bajos_millones,
                    Paisesde_Bajos_percentage = l10p.Paisesde_Bajos_percentage,
                    Paisesde_Altos_millones = l10p.Paisesde_Altos_millones,
                    Paisesde_Altos_percentage = l10p.Paisesde_Altos_percentage,
                    Type = l10p.Type
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.RptCompassSlide5> GetRptCompassSlide5()
        {
            IEnumerable<Illustrator.RptCompassSlide5> result;
            IEnumerable<sp_get_rpt_Compass_Slide5_Result> temp;

            temp = illusDataModel.sp_get_rpt_Compass_Slide5();

            result = temp
                .Select(cs5 => new Illustrator.RptCompassSlide5
                {
                    Sno = cs5.Sno,
                    Category = cs5.Category,
                    Percentage = cs5.Percentage
                })
                .ToArray();

            return
                result;
        }
        public virtual IEnumerable<Illustrator.RptLifeExpectancy> GetRptLifeExpectancy()
        {
            IEnumerable<Illustrator.RptLifeExpectancy> result;
            IEnumerable<sp_get_rpt_lifeexpectancy_Result> temp;

            temp = illusDataModel.sp_get_rpt_lifeexpectancy();

            result = temp
                .Select(le => new Illustrator.RptLifeExpectancy
                {
                    Sno = le.Sno,
                    Current_Age = le.Current_Age,
                    Men = le.Men,
                    Woman = le.Woman
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.RptInvestmentsCompassMaster> GetRptInvestmentsCompassMaster()
        {
            IEnumerable<Illustrator.RptInvestmentsCompassMaster> result;
            IEnumerable<sp_get_rpt_compass_investment_master_Result> temp;

            temp = illusDataModel.sp_get_rpt_compass_investment_master();

            result = temp
                .Select(icm => new Illustrator.RptInvestmentsCompassMaster
                {
                    Sno = icm.Sno,
                    ReturnType = icm.ReturnType,
                    Region = icm.Region
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Illustrator.RptCompassSlide7> GetRptCompassSlide7()
        {
            IEnumerable<Illustrator.RptCompassSlide7> result;
            IEnumerable<sp_get_rpt_Compass_slide7_Result> temp;

            temp = illusDataModel.sp_get_rpt_Compass_slide7();

            result = temp
                .Select(cs7 => new Illustrator.RptCompassSlide7
                {
                    Sno = cs7.Sno,
                    Continent = cs7.Continent,
                    Deaths = cs7.Deaths,
                    Area = cs7.Area
                })
                .ToArray();

            return
                result;
        }

        public virtual int InsertCustomerPlanDetGlobalPolicy(Illustrator.CustomerPlanDetGlobalPolicy planGlobal)
        {
            int result;

            result = illusDataModel.sp_insert_CustomerPlanDetGlobalPolicy(
                    planGlobal.CorpId,
                    planGlobal.RegionId,
                    planGlobal.CountryId,
                    planGlobal.DomesticRegId,
                    planGlobal.StateProvId,
                    planGlobal.CityId,
                    planGlobal.OfficeId,
                    planGlobal.CaseSeqNo,
                    planGlobal.HistSeqNo,
                    planGlobal.CustomerPlanNo
                );

            return
                result;
        }
        public virtual Illustrator.Company GetCompany(int companyNo)
        {
            Illustrator.Company result;
            IEnumerable<sp_get_companydet_Result> temp;

            temp = illusDataModel.sp_get_companydet(companyNo);

            result = temp
                .Select(c => new Illustrator.Company
                {
                    CompanyName = c.companyname,
                    BrandName = c.brandname,
                    companyNo = c.companyno,
                    DateCreated = c.datecreated,
                    DateUpdated = c.dateupdated,
                    DateSynced = c.datesynced
                })
                .FirstOrDefault();

            return
                result;
        }
        public virtual Illustrator.CustomerPlanDetGlobalPolicy GetCustomerPlanDetGlobalPolicy(Illustrator.CustomerPlanDetGlobalPolicy planGlobal)
        {
            Illustrator.CustomerPlanDetGlobalPolicy result;
            IEnumerable<sp_get_CustomerPlanDetGlobalPolicy_Result> temp;

            temp = illusDataModel.sp_get_CustomerPlanDetGlobalPolicy(
                    planGlobal.CorpId,
                    planGlobal.RegionId,
                    planGlobal.CountryId,
                    planGlobal.DomesticRegId,
                    planGlobal.StateProvId,
                    planGlobal.CityId,
                    planGlobal.OfficeId,
                    planGlobal.CaseSeqNo,
                    planGlobal.HistSeqNo,
                    planGlobal.CustomerPlanNo
                );

            result = temp
                .Select(pg => new Illustrator.CustomerPlanDetGlobalPolicy
                {
                    CorpId = pg.Corp_Id,
                    RegionId = pg.Region_Id,
                    CountryId = pg.Country_Id,
                    DomesticRegId = pg.Domesticreg_Id,
                    StateProvId = pg.State_Prov_Id,
                    CityId = pg.City_Id,
                    OfficeId = pg.Office_Id,
                    CaseSeqNo = pg.Case_Seq_No,
                    HistSeqNo = pg.Hist_Seq_No,
                    CustomerPlanNo = pg.CustomerPlanNo
                })
                .FirstOrDefault();

            return
                result;
        }    
   
    }
}