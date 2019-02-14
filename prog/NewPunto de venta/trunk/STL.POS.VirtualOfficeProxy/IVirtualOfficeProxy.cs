﻿using STL.POS.Data;
using STL.POS.VirtualOfficeProxy.VOProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STL.POS.VirtualOfficeProxy
{
    public interface IVirtualOfficeProxy
    {

        bool SetAutoQuotation(QuotationAuto quotation,
            string username,
            int agentId,
            int idOficina,
            int currencyId,
            int retryAmount,
            string policyId,
            string sourceID,
            int codramo,
            List<STL.POS.Data.CSEntities.ListVehicleSourceID> listVehicleSourceID,
            GetMappingElementTypeId getMappingFunction,
            int UserID,
            List<IdentificationFinalBeneficiary> AllFinalBeneficiarys,
            List<PepFormulary> AllPepFormularys,
            PersonInfo personinfo,
            Dictionary<int, int> personJobForGlobal,
            decimal MinAllowedAmountToPay,
            out List<string> statusMessages);

        bool GetPolicy(string policy_no);

        bool DeleteDuplicatePolicy(string policy_no, int userid);
        STL.POS.VirtualOfficeProxy.VOProxy.CalculateLoansResult GetAmortizationTable(double Amount, int period, string loanType, int Percentage, double pTotalPrimium);

        STL.POS.VirtualOfficeProxy.VOProxy.getResult GetEconomicActivities();
        GpDomiciliationResult Domiciliation(Quotation quotation, int Payments, int vCompañia, decimal Cotizacion, DateTime? BeginDate, string PolicyNo, string UserCodeName);
        GpDomiciliationResult SetDomiciliationNewPosSite(Entity.Entities.Quotation quotation, int Payments, int vCompañia, decimal Cotizacion, DateTime? BeginDate, string PolicyNo, string UserCodeName);
        string getRiskGetRiskLevelAuto(System.Nullable<int> vechicleCount, string uso, System.Nullable<decimal> premiumAmount, System.Nullable<decimal> totalVehicleValue, System.Nullable<decimal> totalVechicleInsuredAmount, int? PaisId);
    }
}