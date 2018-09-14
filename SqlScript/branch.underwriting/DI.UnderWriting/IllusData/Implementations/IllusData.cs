﻿using System;
using System.Collections.Generic;
using DI.UnderWriting.IllusData.Interfaces;
using Entity.UnderWriting.IllusData;
using LOGIC.UnderWriting.IllusData;

namespace DI.UnderWriting.IllusData.Implementations
{
    public class IllusDataBll : IIllusData
    {
        private IllusDataManager _illusDataManager;

        public IllusDataBll()
        {
            _illusDataManager = new IllusDataManager();
        }

        int IIllusData.DeleteCustomerDetail(int customerNo, int userIdSystem)
        {
            return
                _illusDataManager.DeleteCustomerDetail(customerNo, userIdSystem);
        }

        int IIllusData.DeleteCustomerPlanDetail(int customerPlanNo, int userIdSystem)
        {
            return
                _illusDataManager.DeleteCustomerPlanDetail(customerPlanNo, userIdSystem);
        }

        IEnumerable<Illustrator.CustomerDetail> IIllusData.GetAllCustomerDetail()
        {
            return
                _illusDataManager.GetAllCustomerDetail();
        }

        Illustrator.CustomerDetail IIllusData.GetCustomerDetailById(long? customerNo, long? rCustomerNo)
        {
            return
                _illusDataManager.GetCustomerDetailById(customerNo, rCustomerNo);
        }

        IEnumerable<Illustrator.CustomerPlanDetail> IIllusData.GetAllCustomerPlanDetail(Illustrator.CustomerPlanDetailP parameters)
        {
            return
                _illusDataManager.GetAllCustomerPlanDetailOrById(parameters);
        }

        //Illustrator.CustomerPlanDetail IIllusData.GetCustomerPlanDetailById(long? customerPlanNo, long? customerNo, long? rCustomerNo, int? userId, int? companyId, DateTime? dateTo, DateTime? dateFrom)
        //{
        //    return
        //        _illusDataManager.GetCustomerPlanDetailById(customerPlanNo, customerNo, rCustomerNo, userId, companyId, dateTo, dateFrom);
        //}

        Illustrator.CustomerDetail IIllusData.InsertCustomerDetail(Illustrator.CustomerDetail customerDetail)
        {
            return
                _illusDataManager.InsertCustomerDetail(customerDetail);
        }

        Illustrator.CustomerDetail IIllusData.UpdateCustomerDetail(Illustrator.CustomerDetail customerDetail)
        {
            return
                _illusDataManager.UpdateCustomerDetail(customerDetail);
        }

        Illustrator.CustomerPlanDetail IIllusData.InsertCustomerPlanDetail(Illustrator.CustomerPlanDetail customerPlanDetail)
        {
            return
                _illusDataManager.InsertCustomerPlanDetail(customerPlanDetail);
        }

        Illustrator.CustomerPlanDetail IIllusData.UpdateCustomerPlanDetail(Illustrator.CustomerPlanDetail customerPlanDetail)
        {
            return
                _illusDataManager.UpdateCustomerPlanDetail(customerPlanDetail);
        }

        IEnumerable<DropDown> IIllusData.GetDropDownByType(DropDown.Parameter parameters)
        {
            return
                _illusDataManager.GetDropDownByType(parameters);
        }

        int IIllusData.DeleteCustomerEmails(long customerNo, int userIdSystem)
        {
            return
                _illusDataManager.DeleteCustomerEmails(customerNo, userIdSystem);
        }

        int IIllusData.DeleteCustomerPhones(long customerNo, int userIdSystem)
        {
            return
                _illusDataManager.DeleteCustomerPhones(customerNo, userIdSystem);
        }

        int IIllusData.InsertCustomerEmail(Illustrator.CustomerEmail customerEmail)
        {
            return
                _illusDataManager.InsertCustomerEmail(customerEmail);
        }

        int IIllusData.InsertCustomerPhone(Illustrator.CustomerPhone customerPhone)
        {
            return
                _illusDataManager.InsertCustomerPhone(customerPhone);
        }

        int IIllusData.DeleteCustomerOccupations(long customerNo, int userIdSystem)
        {
            return
                _illusDataManager.DeleteCustomerOccupations(customerNo, userIdSystem);
        }

        int IIllusData.DeleteCustomerIdentifications(long customerNo, int userIdSystem)
        {
            return
                _illusDataManager.DeleteCustomerIdentifications(customerNo, userIdSystem);
        }

        int IIllusData.InsertPlanIdentification(Illustrator.CustomerPlanIdentification customerPlanIdentification)
        {
            return
                _illusDataManager.InsertPlanIdentification(customerPlanIdentification);
        }

        Illustrator.CustomerOccupation IIllusData.InsertCustomerOccupation(Illustrator.CustomerOccupation customerOccupation)
        {
            return
                _illusDataManager.InsertCustomerOccupation(customerOccupation);
        }

        Illustrator.CustomerOccupation IIllusData.UpdateCustomerOccupation(Illustrator.CustomerOccupation customerOccupation)
        {
            return
                _illusDataManager.UpdateCustomerOccupation(customerOccupation);
        }

        Illustrator.Signature IIllusData.SetIllustrationSignature(Illustrator.Signature signature)
        {
            return
                 _illusDataManager.SetIllustrationSignature(signature);
        }

        Illustrator.User IIllusData.SetUser(Illustrator.User user)
        {
            return
                 _illusDataManager.SetUser(user);
        }

        IEnumerable<Illustrator.User> IIllusData.GetUser(string nameId)
        {
            return
                 _illusDataManager.GetUser(nameId);
        }

        IEnumerable<Illustrator.Signature> IIllusData.GetIllustrationSignature(long? customerPlanNo)
        {
            return
                _illusDataManager.GetIllustrationSignature(customerPlanNo);
        }

        long IIllusData.GetMaxIllustrationNo()
        {
            return
                _illusDataManager.GetMaxIllustrationNo();
        }

        IEnumerable<Illustrator.CustomerPlanBeneficiary> IIllusData.GetCustomerPlanBeneficiary(long customerPlanNo, string insuredTypeCode, string beneficiaryTypeCode)
        {
            return
                _illusDataManager.GetCustomerPlanBeneficiary(customerPlanNo, insuredTypeCode, beneficiaryTypeCode);
        }

        Illustrator.CustomerPlanBeneficiary IIllusData.SetCustomerPlanBeneficiary(Illustrator.CustomerPlanBeneficiary beneficiary)
        {
            return
                _illusDataManager.SetCustomerPlanBeneficiary(beneficiary);
        }

        int IIllusData.DeleteCustomerPlanBeneficiary(long customerplanbeneficiaryno)
        {
            return
                _illusDataManager.DeleteCustomerPlanBeneficiary(customerplanbeneficiaryno);
        }

        Illustrator.CustomerPlanPartnerInsurance IIllusData.GetCustomerPlanPartnerInsurance(long customerPlanNo)
        {
            return
                _illusDataManager.GetCustomerPlanPartnerInsurance(customerPlanNo);
        }

        Illustrator.CustomerPlanPartnerInsurance IIllusData.SetCustomerPlanPartnerInsurance(Illustrator.CustomerPlanPartnerInsurance partnerInsurance)
        {
            return
                _illusDataManager.SetCustomerPlanPartnerInsurance(partnerInsurance);
        }

        int IIllusData.DeleteCustomerPlanPartnerInsurance(long customerPlanNo)
        {
            return
                _illusDataManager.DeleteCustomerPlanPartnerInsurance(customerPlanNo);
        }

        IEnumerable<Illustrator.CustomerPlanOtherInsurance> IIllusData.GetCustomerPlanOtherInsurance(long customerPlanNo, string insuredTypeCode)
        {
            return
                _illusDataManager.GetCustomerPlanOtherInsurance(customerPlanNo, insuredTypeCode);
        }

        Illustrator.CustomerPlanOtherInsurance IIllusData.SetCustomerPlanOtherInsurance(Illustrator.CustomerPlanOtherInsurance customerPlanOtherInsurance)
        {
            return
                _illusDataManager.SetCustomerPlanOtherInsurance(customerPlanOtherInsurance);
        }

        int IIllusData.DeleteCustomerPlanOtherInsurance(long customerPlanOtherInsuranceNo)
        {
            return
                _illusDataManager.DeleteCustomerPlanOtherInsurance(customerPlanOtherInsuranceNo);
        }

        IEnumerable<Illustrator.CustomerPlanExam> IIllusData.GetCustomerPlanExam(long customerPlanNo, string insuredTypeCode)
        {
            return
                 _illusDataManager.GetCustomerPlanExam(customerPlanNo, insuredTypeCode);
        }

        IEnumerable<Illustrator.CustomerPlanExamCondition> IIllusData.GetCustomerExamCondition(string productCode, int age, string genderCode, string maritalStatusCode, decimal insuredAmount)
        {
            return
                  _illusDataManager.GetCustomerExamCondition(productCode, age, genderCode, maritalStatusCode, insuredAmount);
        }

        Illustrator.CustomerPlanExam IIllusData.UpdateCustomerPlanExam(Illustrator.CustomerPlanExam examCondition)
        {
            return
                _illusDataManager.UpdateCustomerPlanExam(examCondition);
        }

        int IIllusData.DeleteCustomerPlanExam(long customerPlanNo, string insuredTypeCode, int userIdSystem)
        {
            return
                _illusDataManager.DeleteCustomerPlanExam(customerPlanNo, insuredTypeCode, userIdSystem);
        }

        decimal IIllusData.GetTotalInsuredAmount(long customerPlanNo, string insuredTypeCode)
        {
            return
                _illusDataManager.GetTotalInsuredAmount(customerPlanNo, insuredTypeCode);
        }

        IEnumerable<Illustrator.RuleParameter> IIllusData.GetAllRuleParameter(int? ruleParameterNo, string productCode)
        {
            return
                 _illusDataManager.GetAllRuleParameter(ruleParameterNo, productCode);
        }


        IEnumerable<Illustrator.CustomerPlaNopBal> IIllusData.GetCustomerPlaNopBal(long customerNo, long customerPlanNo)
        {
            return
                _illusDataManager.GetCustomerPlaNopBal(customerNo, customerPlanNo);
        }

        IEnumerable<Illustrator.CustomerPlanVarPremium> IIllusData.GetCustomerPlanVarPremium(long customerPlanNo)
        {
            return
                _illusDataManager.GetCustomerPlanVarPremium(customerPlanNo);
        }

        IEnumerable<Illustrator.CustomerPlanVarInsured> IIllusData.GetCustomerPlanVarInsured(long customerPlanNo)
        {
            return
                 _illusDataManager.GetCustomerPlanVarInsured(customerPlanNo);
        }

        IEnumerable<Illustrator.CustomerPlanLoan> IIllusData.GetCustomerPlanLoan(long customerPlanNo)
        {
            return
                _illusDataManager.GetCustomerPlanLoan(customerPlanNo);
        }

        IEnumerable<Illustrator.CustomerPlanVarSurrender> IIllusData.GetCustomerPlanVarSurrender(long customerPlanNo)
        {
            return
                _illusDataManager.GetCustomerPlanVarSurrender(customerPlanNo);
        }

        IEnumerable<Illustrator.CustomerPlanLoanRepay> IIllusData.GetCustomerPlanLoanRepay(long customerPlanNo)
        {
            return
                _illusDataManager.GetCustomerPlanLoanRepay(customerPlanNo);
        }

        IEnumerable<Illustrator.CustomerPlanVarProfile> IIllusData.GetCustomerPlanVarProfile(long customerPlanNo)
        {
            return
                _illusDataManager.GetCustomerPlanVarProfile(customerPlanNo);
        }

        IEnumerable<Illustrator.InvProfileCompareRates> IIllusData.GetInvProfileCompareRates(string classCode, string productCode, string investmentProfileCode)
        {
            return
                _illusDataManager.GetInvProfileCompareRates(classCode, productCode, investmentProfileCode);
        }

        Illustrator.CustomerPlaNopBal IIllusData.SetCustomerPlaNopBal(Illustrator.CustomerPlaNopBal plaNopBal)
        {
            return
                _illusDataManager.SetCustomerPlaNopBal(plaNopBal);
        }

        Illustrator.CustomerPlanVarPremium IIllusData.SetCustomerPlanVarPremium(Illustrator.CustomerPlanVarPremium planVarPremium)
        {
            return
                _illusDataManager.SetCustomerPlanVarPremium(planVarPremium);
        }

        Illustrator.CustomerPlanVarInsured IIllusData.SetCustomerPlanVarInsured(Illustrator.CustomerPlanVarInsured planVarInsured)
        {
            return
                 _illusDataManager.SetCustomerPlanVarInsured(planVarInsured);
        }

        Illustrator.CustomerPlanLoan IIllusData.SetCustomerPlanLoan(Illustrator.CustomerPlanLoan planLoan)
        {
            return
                _illusDataManager.SetCustomerPlanLoan(planLoan);
        }

        Illustrator.CustomerPlanVarSurrender IIllusData.SetCustomerPlanVarSurrender(Illustrator.CustomerPlanVarSurrender planVarSurrender)
        {
            return
                 _illusDataManager.SetCustomerPlanVarSurrender(planVarSurrender);
        }

        Illustrator.CustomerPlanLoanRepay IIllusData.SetCustomerPlanLoanRepay(Illustrator.CustomerPlanLoanRepay planLoanRepay)
        {
            return
                _illusDataManager.SetCustomerPlanLoanRepay(planLoanRepay);
        }

        Illustrator.CustomerPlanVarProfile IIllusData.SetCustomerPlanVarProfile(Illustrator.CustomerPlanVarProfile compareRates)
        {
            return
                _illusDataManager.SetCustomerPlanVarProfile(compareRates);
        }

        int IIllusData.DeleteCustomerPlaNopBal(long customerPlanNo)
        {
            return
                _illusDataManager.DeleteCustomerPlaNopBal(customerPlanNo);
        }

        int IIllusData.DeleteCustomerPlanVarPremium(long customerPlanNo)
        {
            return
                _illusDataManager.DeleteCustomerPlanVarPremium(customerPlanNo);
        }

        int IIllusData.DeleteCustomerPlanVarInsured(long customerPlanNo)
        {
            return
                _illusDataManager.DeleteCustomerPlanVarInsured(customerPlanNo);
        }

        int IIllusData.DeleteCustomerPlanLoan(long customerPlanNo)
        {
            return
                _illusDataManager.DeleteCustomerPlanLoan(customerPlanNo);
        }

        int IIllusData.DeleteCustomerPlanVarSurrender(long customerPlanNo)
        {
            return
                _illusDataManager.DeleteCustomerPlanVarSurrender(customerPlanNo);
        }

        int IIllusData.DeleteCustomerPlanLoanRepay(long customerPlanNo)
        {
            return
                _illusDataManager.DeleteCustomerPlanLoanRepay(customerPlanNo);
        }

        int IIllusData.DeleteCustomerPlanVarProfile(long customerPlanNo)
        {
            return
                 _illusDataManager.DeleteCustomerPlanVarProfile(customerPlanNo);
        }

        IEnumerable<Illustrator.ProductCancel> IIllusData.GetProductCancel(string productCode, int retirementPeriod)
        {
            return
                _illusDataManager.GetProductCancel(productCode, retirementPeriod);
        }

        IEnumerable<Illustrator.ProductCancelDetail> IIllusData.GetProductCancelDetail(int productCancelNo)
        {
            return
                _illusDataManager.GetProductCancelDetail(productCancelNo);
        }

        IEnumerable<Illustrator.FrequencyCostDetail> IIllusData.GetFrequencyCost(string productCode, string frequencyTypeCode)
        {
            return
                _illusDataManager.GetFrequencyCost(productCode, frequencyTypeCode);
        }

        int IIllusData.DeleteCustomerPlanTerm(long customerPlanNo)
        {
            return
                _illusDataManager.DeleteCustomerPlanTerm(customerPlanNo);
        }

        int IIllusData.DeleteCustomerPlanAnnuity(long customerPlanNo)
        {
            return
                _illusDataManager.DeleteCustomerPlanAnnuity(customerPlanNo);
        }

        int IIllusData.DeleteCustomerPlanLife(long customerPlanNo)
        {
            return
                _illusDataManager.DeleteCustomerPlanLife(customerPlanNo);
        }

        IEnumerable<Illustrator.CustomerPlanTerm> IIllusData.GetCustomerPlanTerm(long customerPlanNo)
        {
            return
                _illusDataManager.GetCustomerPlanTerm(customerPlanNo);
        }

        IEnumerable<Illustrator.CustomerPlanAnnuity> IIllusData.GetCustomerPlanAnnuity(long customerPlanNo)
        {
            return
                _illusDataManager.GetCustomerPlanAnnuity(customerPlanNo);
        }

        IEnumerable<Illustrator.CustomerPlanLife> IIllusData.GetCustomerPlanLife(long customerPlanNo)
        {
            return
                _illusDataManager.GetCustomerPlanLife(customerPlanNo);
        }

        Illustrator.CustomerPlanTerm IIllusData.InsertCustomerPlanTerm(Illustrator.CustomerPlanTerm planTerm)
        {
            return
                _illusDataManager.InsertCustomerPlanTerm(planTerm);
        }

        Illustrator.CustomerPlanAnnuity IIllusData.InsertCustomerPlanAnnuity(Illustrator.CustomerPlanAnnuity planAnnuity)
        {
            return
                _illusDataManager.InsertCustomerPlanAnnuity(planAnnuity);
        }

        Illustrator.CustomerPlanLife IIllusData.InsertCustomerPlanLife(Illustrator.CustomerPlanLife planLife)
        {
            return
                _illusDataManager.InsertCustomerPlanLife(planLife);
        }

        IEnumerable<Illustrator.InvestmentsInflacion> IIllusData.GetRptInvestmentsInflacion()
        {
            return
                _illusDataManager.GetRptInvestmentsInflacion();
        }

        IEnumerable<Illustrator.InvestmentsType> IIllusData.GetRptInvestmentsType(string fundType, string fundCategory, string region)
        {
            return
                _illusDataManager.GetRptInvestmentsType(fundType, fundCategory, region);
        }

        IEnumerable<Illustrator.InvestmentsCompass> IIllusData.GetRptInvestmentsCompass(int ReturnTypeid)
        {
            return
                 _illusDataManager.GetRptInvestmentsCompass(ReturnTypeid);
        }

        IEnumerable<Illustrator.InvestmentsSlide3> IIllusData.GetRptInvestmentsSlide3()
        {
            return
                _illusDataManager.GetRptInvestmentsSlide3();
        }

        IEnumerable<Illustrator.InvestmentsSlide4> IIllusData.GetRptInvestmentsSlide4()
        {
            return
                _illusDataManager.GetRptInvestmentsSlide4();
        }

        IEnumerable<Illustrator.InvestmentsSlide5Chart1> IIllusData.GetRptInvestmentsSlide5Chart1()
        {
            return
                _illusDataManager.GetRptInvestmentsSlide5Chart1();
        }

        IEnumerable<Illustrator.InvestmentsSlide5Chart2> IIllusData.GetRptInvestmentsSlide5Chart2()
        {
            return
                _illusDataManager.GetRptInvestmentsSlide5Chart2();
        }

        IEnumerable<Illustrator.InvestmentsSlide6> IIllusData.GetRptInvestmentsSlide6()
        {
            return
                _illusDataManager.GetRptInvestmentsSlide6();
        }

        IEnumerable<Illustrator.InvestmentsReturns> IIllusData.GetRptInvestmentsReturns()
        {
            return
                _illusDataManager.GetRptInvestmentsReturns();
        }

        IEnumerable<Illustrator.InvestmentsProfile> IIllusData.GetInvestmentsProfile()
        {
            return
                _illusDataManager.GetInvestmentsProfile();
        }

        IEnumerable<Illustrator.InvestmentsProfileEuro> IIllusData.GetInvestmentsProfileEuro()
        {
            return
                _illusDataManager.GetInvestmentsProfileEuro();
        }

        IEnumerable<Illustrator.RptAxysFixedinComeSlide12> IIllusData.GetRptAxysFixedinComeSlide12()
        {
            return
                _illusDataManager.GetRptAxysFixedinComeSlide12();
        }

        IEnumerable<Illustrator.RptAxysHighperFormSlide12> IIllusData.GetRptAxysHighperFormSlide12()
        {
            return
                _illusDataManager.GetRptAxysHighperFormSlide12();
        }

        IEnumerable<Illustrator.RptAxysLowRiskSlide12> IIllusData.GetRptAxysLowRiskSlide12()
        {
            return
                _illusDataManager.GetRptAxysLowRiskSlide12();
        }

        IEnumerable<Illustrator.RptAxysSlide10> IIllusData.GetRptAxysSlide10()
        {
            return
                _illusDataManager.GetRptAxysSlide10();
        }

        IEnumerable<Illustrator.RptAxysSlide11> IIllusData.GetRptAxysSlide11()
        {
            return
                _illusDataManager.GetRptAxysSlide11();
        }

        IEnumerable<Illustrator.RptAxysSlide5> IIllusData.GetRptAxysSlide5()
        {
            return
                _illusDataManager.GetRptAxysSlide5();
        }

        IEnumerable<Illustrator.RptAxysSlide6> IIllusData.GetRptAxysSlide6()
        {
            return
                _illusDataManager.GetRptAxysSlide6();
        }

        IEnumerable<Illustrator.RptAxysSlide8> IIllusData.GetRptAxysSlide8()
        {
            return
                _illusDataManager.GetRptAxysSlide8();
        }

        IEnumerable<Illustrator.EgrAge> IIllusData.GetEgrAge()
        {
            return
                _illusDataManager.GetEgrAge();
        }

        IEnumerable<Illustrator.EgrSlide7> IIllusData.GetEgrSlide7()
        {
            return
                _illusDataManager.GetEgrSlide7();
        }

        IEnumerable<Illustrator.EgrSlide8> IIllusData.GetEgrSlide8()
        {
            return
                _illusDataManager.GetEgrSlide8();
        }

        IEnumerable<Illustrator.EgrSlide9> IIllusData.GetEgrSlide9()
        {
            return
                _illusDataManager.GetEgrSlide9();
        }

        IEnumerable<Illustrator.EgrSlide10> IIllusData.GetEgrSlide10()
        {
            return
                _illusDataManager.GetEgrSlide10();
        }

        IEnumerable<Illustrator.RptLegacy10Priciple> IIllusData.GetRptLegacy10Priciple()
        {
            return
                _illusDataManager.GetRptLegacy10Priciple();
        }

        IEnumerable<Illustrator.RptCompassSlide5> IIllusData.GetRptCompassSlide5()
        {
            return
                _illusDataManager.GetRptCompassSlide5();
        }

        IEnumerable<Illustrator.RptLifeExpectancy> IIllusData.GetRptLifeExpectancy()
        {
            return
                _illusDataManager.GetRptLifeExpectancy();
        }

        IEnumerable<Illustrator.RptInvestmentsCompassMaster> IIllusData.GetRptInvestmentsCompassMaster()
        {
            return
                _illusDataManager.GetRptInvestmentsCompassMaster();
        }

        IEnumerable<Illustrator.RptCompassSlide7> IIllusData.RptCompassSlide7()
        {
            return
                _illusDataManager.RptCompassSlide7();
        }

        int IIllusData.InsertCustomerPlanDetGlobalPolicy(Illustrator.CustomerPlanDetGlobalPolicy planGlobal)
        {
            return
                _illusDataManager.InsertCustomerPlanDetGlobalPolicy(planGlobal);
        }

        Illustrator.Company IIllusData.GetCompany(int companyNo)
        {
            return
                _illusDataManager.GetCompany(companyNo);
        }

        Illustrator.CustomerPlanDetGlobalPolicy IIllusData.GetCustomerPlanDetGlobalPolicy(Illustrator.CustomerPlanDetGlobalPolicy planGlobal)
        {
            return
                _illusDataManager.GetCustomerPlanDetGlobalPolicy(planGlobal);
        }          
     
		public double GetGoalSeek(double InsuredAmmount) //Lgonzalez 23-02-2017
        {
            return _illusDataManager.GetGoalSeek(InsuredAmmount);
        }
    }

    public class IllusDataWS : IIllusData
    {
       
        int IIllusData.DeleteCustomerDetail(int customerNo, int userIdSystem)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanDetail(int customerPlanNo, int userIdSystem)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerDetail> IIllusData.GetAllCustomerDetail()
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerDetail IIllusData.GetCustomerDetailById(long? customerNo, long? rCustomerNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanDetail> IIllusData.GetAllCustomerPlanDetail(Illustrator.CustomerPlanDetailP parameters)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerDetail IIllusData.InsertCustomerDetail(Illustrator.CustomerDetail customerDetail)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerDetail IIllusData.UpdateCustomerDetail(Illustrator.CustomerDetail customerDetail)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanDetail IIllusData.InsertCustomerPlanDetail(Illustrator.CustomerPlanDetail customerPlanDetail)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanDetail IIllusData.UpdateCustomerPlanDetail(Illustrator.CustomerPlanDetail customerPlanDetail)
        {
            throw new NotImplementedException();
        }

        IEnumerable<DropDown> IIllusData.GetDropDownByType(DropDown.Parameter parameters)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerEmails(long customerNo, int userIdSystem)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPhones(long customerNo, int userIdSystem)
        {
            throw new NotImplementedException();
        }

        int IIllusData.InsertCustomerEmail(Illustrator.CustomerEmail customerEmail)
        {
            throw new NotImplementedException();
        }

        int IIllusData.InsertCustomerPhone(Illustrator.CustomerPhone customerPhone)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerOccupations(long customerNo, int userIdSystem)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerIdentifications(long customerNo, int userIdSystem)
        {
            throw new NotImplementedException();
        }

        int IIllusData.InsertPlanIdentification(Illustrator.CustomerPlanIdentification customerPlanIdentification)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerOccupation IIllusData.InsertCustomerOccupation(Illustrator.CustomerOccupation customerOccupation)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerOccupation IIllusData.UpdateCustomerOccupation(Illustrator.CustomerOccupation customerOccupation)
        {
            throw new NotImplementedException();
        }

        Illustrator.Signature IIllusData.SetIllustrationSignature(Illustrator.Signature signature)
        {
            throw new NotImplementedException();
        }

        Illustrator.User IIllusData.SetUser(Illustrator.User user)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.User> IIllusData.GetUser(string nameId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.Signature> IIllusData.GetIllustrationSignature(long? customerPlanNo)
        {
            throw new NotImplementedException();
        }

        long IIllusData.GetMaxIllustrationNo()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanBeneficiary> IIllusData.GetCustomerPlanBeneficiary(long customerPlanNo, string insuredTypeCode, string beneficiaryTypeCode)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanBeneficiary IIllusData.SetCustomerPlanBeneficiary(Illustrator.CustomerPlanBeneficiary beneficiary)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanBeneficiary(long customerplanbeneficiaryno)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanPartnerInsurance IIllusData.GetCustomerPlanPartnerInsurance(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanPartnerInsurance IIllusData.SetCustomerPlanPartnerInsurance(Illustrator.CustomerPlanPartnerInsurance partnerInsurance)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanPartnerInsurance(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanOtherInsurance> IIllusData.GetCustomerPlanOtherInsurance(long customerPlanNo, string insuredTypeCode)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanOtherInsurance IIllusData.SetCustomerPlanOtherInsurance(Illustrator.CustomerPlanOtherInsurance customerPlanOtherInsurance)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanOtherInsurance(long customerPlanOtherInsuranceNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanExam> IIllusData.GetCustomerPlanExam(long customerPlanNo, string insuredTypeCode)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanExamCondition> IIllusData.GetCustomerExamCondition(string productCode, int age, string genderCode, string maritalStatusCode, decimal insuredAmount)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanExam IIllusData.UpdateCustomerPlanExam(Illustrator.CustomerPlanExam examCondition)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanExam(long customerPlanNo, string insuredTypeCode, int userIdSystem)
        {
            throw new NotImplementedException();
        }

        decimal IIllusData.GetTotalInsuredAmount(long customerPlanNo, string insuredTypeCode)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RuleParameter> IIllusData.GetAllRuleParameter(int? ruleParameterNo, string productCode)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlaNopBal> IIllusData.GetCustomerPlaNopBal(long customerNo, long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanVarPremium> IIllusData.GetCustomerPlanVarPremium(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanVarInsured> IIllusData.GetCustomerPlanVarInsured(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanLoan> IIllusData.GetCustomerPlanLoan(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanVarSurrender> IIllusData.GetCustomerPlanVarSurrender(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanLoanRepay> IIllusData.GetCustomerPlanLoanRepay(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanVarProfile> IIllusData.GetCustomerPlanVarProfile(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.InvProfileCompareRates> IIllusData.GetInvProfileCompareRates(string classCode, string productCode, string investmentProfileCode)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlaNopBal IIllusData.SetCustomerPlaNopBal(Illustrator.CustomerPlaNopBal plaNopBal)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanVarPremium IIllusData.SetCustomerPlanVarPremium(Illustrator.CustomerPlanVarPremium planVarPremium)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanVarInsured IIllusData.SetCustomerPlanVarInsured(Illustrator.CustomerPlanVarInsured planVarInsured)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanLoan IIllusData.SetCustomerPlanLoan(Illustrator.CustomerPlanLoan planLoan)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanVarSurrender IIllusData.SetCustomerPlanVarSurrender(Illustrator.CustomerPlanVarSurrender planVarSurrender)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanLoanRepay IIllusData.SetCustomerPlanLoanRepay(Illustrator.CustomerPlanLoanRepay planLoanRepay)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanVarProfile IIllusData.SetCustomerPlanVarProfile(Illustrator.CustomerPlanVarProfile compareRates)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlaNopBal(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanVarPremium(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanVarInsured(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanLoan(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanVarSurrender(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanLoanRepay(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanVarProfile(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.ProductCancel> IIllusData.GetProductCancel(string productCode, int retirementPeriod)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.ProductCancelDetail> IIllusData.GetProductCancelDetail(int productCancelNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.FrequencyCostDetail> IIllusData.GetFrequencyCost(string productCode, string frequencyTypeCode)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanTerm(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanAnnuity(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        int IIllusData.DeleteCustomerPlanLife(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanTerm> IIllusData.GetCustomerPlanTerm(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanAnnuity> IIllusData.GetCustomerPlanAnnuity(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.CustomerPlanLife> IIllusData.GetCustomerPlanLife(long customerPlanNo)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanTerm IIllusData.InsertCustomerPlanTerm(Illustrator.CustomerPlanTerm planTerm)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanAnnuity IIllusData.InsertCustomerPlanAnnuity(Illustrator.CustomerPlanAnnuity planAnnuity)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanLife IIllusData.InsertCustomerPlanLife(Illustrator.CustomerPlanLife planLife)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.InvestmentsInflacion> IIllusData.GetRptInvestmentsInflacion()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.InvestmentsType> IIllusData.GetRptInvestmentsType(string fundType, string fundCategory, string region)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.InvestmentsCompass> IIllusData.GetRptInvestmentsCompass(int ReturnTypeid)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.InvestmentsSlide3> IIllusData.GetRptInvestmentsSlide3()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.InvestmentsSlide4> IIllusData.GetRptInvestmentsSlide4()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.InvestmentsSlide5Chart1> IIllusData.GetRptInvestmentsSlide5Chart1()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.InvestmentsSlide5Chart2> IIllusData.GetRptInvestmentsSlide5Chart2()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.InvestmentsSlide6> IIllusData.GetRptInvestmentsSlide6()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.InvestmentsReturns> IIllusData.GetRptInvestmentsReturns()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.InvestmentsProfile> IIllusData.GetInvestmentsProfile()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.InvestmentsProfileEuro> IIllusData.GetInvestmentsProfileEuro()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptAxysFixedinComeSlide12> IIllusData.GetRptAxysFixedinComeSlide12()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptAxysHighperFormSlide12> IIllusData.GetRptAxysHighperFormSlide12()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptAxysLowRiskSlide12> IIllusData.GetRptAxysLowRiskSlide12()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptAxysSlide10> IIllusData.GetRptAxysSlide10()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptAxysSlide11> IIllusData.GetRptAxysSlide11()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptAxysSlide5> IIllusData.GetRptAxysSlide5()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptAxysSlide6> IIllusData.GetRptAxysSlide6()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptAxysSlide8> IIllusData.GetRptAxysSlide8()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.EgrAge> IIllusData.GetEgrAge()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.EgrSlide7> IIllusData.GetEgrSlide7()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.EgrSlide8> IIllusData.GetEgrSlide8()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.EgrSlide9> IIllusData.GetEgrSlide9()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.EgrSlide10> IIllusData.GetEgrSlide10()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptLegacy10Priciple> IIllusData.GetRptLegacy10Priciple()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptCompassSlide5> IIllusData.GetRptCompassSlide5()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptLifeExpectancy> IIllusData.GetRptLifeExpectancy()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptInvestmentsCompassMaster> IIllusData.GetRptInvestmentsCompassMaster()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Illustrator.RptCompassSlide7> IIllusData.RptCompassSlide7()
        {
            throw new NotImplementedException();
        }

        int IIllusData.InsertCustomerPlanDetGlobalPolicy(Illustrator.CustomerPlanDetGlobalPolicy planGlobal)
        {
            throw new NotImplementedException();
        }

        Illustrator.Company IIllusData.GetCompany(int companyNo)
        {
            throw new NotImplementedException();
        }

        Illustrator.CustomerPlanDetGlobalPolicy IIllusData.GetCustomerPlanDetGlobalPolicy(Illustrator.CustomerPlanDetGlobalPolicy planGlobal)
        {
            throw new NotImplementedException();
        }
       
        public double GetGoalSeek(double InsuredAmmount)  //Lgonzalez 23-02-2017
        {
            throw new NotImplementedException();
        }
    }
}