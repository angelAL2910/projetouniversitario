﻿using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace WEB.NewBusiness.Common.Thunderhead.KSI
{
    [XmlRoot(ElementName = "Fee", Namespace = "POS_AUTO_SCHEMA")]
    public class Fee
    {
        [XmlElement(ElementName = "Number", Namespace = "POS_AUTO_SCHEMA")]
        public string Number { get; set; }
        [XmlElement(ElementName = "Date", Namespace = "POS_AUTO_SCHEMA")]
        public string Date { get; set; }
        [XmlElement(ElementName = "Amount", Namespace = "POS_AUTO_SCHEMA")]
        public string Amount { get; set; }
        [XmlElement(ElementName = "Capital", Namespace = "POS_AUTO_SCHEMA")]
        public string Capital { get; set; }
        [XmlElement(ElementName = "Interests", Namespace = "POS_AUTO_SCHEMA")]
        public string Interests { get; set; }
        [XmlElement(ElementName = "Comission", Namespace = "POS_AUTO_SCHEMA")]
        public string Comission { get; set; }
        [XmlElement(ElementName = "Spends", Namespace = "POS_AUTO_SCHEMA")]
        public string Spends { get; set; }
        [XmlElement(ElementName = "Total", Namespace = "POS_AUTO_SCHEMA")]
        public string Total { get; set; }
    }

    [XmlRoot(ElementName = "Loan", Namespace = "POS_AUTO_SCHEMA")]
    public class Loan
    {
        [XmlElement(ElementName = "Account", Namespace = "POS_AUTO_SCHEMA")]
        public string Account { get; set; }
        [XmlElement(ElementName = "Id", Namespace = "POS_AUTO_SCHEMA")]
        public string Id { get; set; }
        [XmlElement(ElementName = "Status", Namespace = "POS_AUTO_SCHEMA")]
        public string Status { get; set; }
        [XmlElement(ElementName = "AccountName", Namespace = "POS_AUTO_SCHEMA")]
        public string AccountName { get; set; }
        [XmlElement(ElementName = "Client", Namespace = "POS_AUTO_SCHEMA")]
        public string Client { get; set; }
        [XmlElement(ElementName = "FoundsSource", Namespace = "POS_AUTO_SCHEMA")]
        public string FoundsSource { get; set; }
        [XmlElement(ElementName = "FundsDestination", Namespace = "POS_AUTO_SCHEMA")]
        public string FundsDestination { get; set; }
        [XmlElement(ElementName = "CredtitFacility", Namespace = "POS_AUTO_SCHEMA")]
        public string CredtitFacility { get; set; }
        [XmlElement(ElementName = "Comite", Namespace = "POS_AUTO_SCHEMA")]
        public string Comite { get; set; }
        [XmlElement(ElementName = "PaymentMethod", Namespace = "POS_AUTO_SCHEMA")]
        public string PaymentMethod { get; set; }
        [XmlElement(ElementName = "RequestedAmount", Namespace = "POS_AUTO_SCHEMA")]
        public string RequestedAmount { get; set; }
        [XmlElement(ElementName = "ApprovedAmount", Namespace = "POS_AUTO_SCHEMA")]
        public string ApprovedAmount { get; set; }
        [XmlElement(ElementName = "ReleasedAmount", Namespace = "POS_AUTO_SCHEMA")]
        public string ReleasedAmount { get; set; }
        [XmlElement(ElementName = "CapitalReturn", Namespace = "POS_AUTO_SCHEMA")]
        public string CapitalReturn { get; set; }
        [XmlElement(ElementName = "LastCut", Namespace = "POS_AUTO_SCHEMA")]
        public string LastCut { get; set; }
        [XmlElement(ElementName = "Interest", Namespace = "POS_AUTO_SCHEMA")]
        public string Interest { get; set; }
        [XmlElement(ElementName = "Comission", Namespace = "POS_AUTO_SCHEMA")]
        public string Comission { get; set; }
        [XmlElement(ElementName = "DelayFee", Namespace = "POS_AUTO_SCHEMA")]
        public string DelayFee { get; set; }
        [XmlElement(ElementName = "FeeAmount", Namespace = "POS_AUTO_SCHEMA")]
        public string FeeAmount { get; set; }
        [XmlElement(ElementName = "PaymentPeriod", Namespace = "POS_AUTO_SCHEMA")]
        public string PaymentPeriod { get; set; }
        [XmlElement(ElementName = "Frequency", Namespace = "POS_AUTO_SCHEMA")]
        public string Frequency { get; set; }
        [XmlElement(ElementName = "FeeNumber", Namespace = "POS_AUTO_SCHEMA")]
        public string FeeNumber { get; set; }
        [XmlElement(ElementName = "RequestDate", Namespace = "POS_AUTO_SCHEMA")]
        public string RequestDate { get; set; }
        [XmlElement(ElementName = "ApprovementDate", Namespace = "POS_AUTO_SCHEMA")]
        public string ApprovementDate { get; set; }
        [XmlElement(ElementName = "ReleasedDate", Namespace = "POS_AUTO_SCHEMA")]
        public string ReleasedDate { get; set; }
        [XmlElement(ElementName = "ExpirationDate", Namespace = "POS_AUTO_SCHEMA")]
        public string ExpirationDate { get; set; }
        [XmlElement(ElementName = "NextPaymentDate", Namespace = "POS_AUTO_SCHEMA")]
        public string NextPaymentDate { get; set; }
        [XmlElement(ElementName = "TotalCapital", Namespace = "POS_AUTO_SCHEMA")]
        public string TotalCapital { get; set; }
        [XmlElement(ElementName = "TotalInterests", Namespace = "POS_AUTO_SCHEMA")]
        public string TotalInterests { get; set; }
        [XmlElement(ElementName = "TotalComissions", Namespace = "POS_AUTO_SCHEMA")]
        public string TotalComissions { get; set; }
        [XmlElement(ElementName = "TotalSpends", Namespace = "POS_AUTO_SCHEMA")]
        public string TotalSpends { get; set; }
        [XmlElement(ElementName = "TotalAmount", Namespace = "POS_AUTO_SCHEMA")]
        public string TotalAmount { get; set; }
        [XmlElement(ElementName = "Fee", Namespace = "POS_AUTO_SCHEMA")]
        public List<Fee> Fee { get; set; }
    }

    [XmlRoot(ElementName = "Transaction", Namespace = "POS_AUTO_SCHEMA")]
    public class Transaction
    {
        [XmlElement(ElementName = "DocumentId", Namespace = "POS_AUTO_SCHEMA")]
        public string DocumentId { get; set; }
        [XmlElement(ElementName = "Direccion", Namespace = "POS_AUTO_SCHEMA")]
        public string Direccion { get; set; }
        [XmlElement(ElementName = "Ciudad", Namespace = "POS_AUTO_SCHEMA")]
        public string Ciudad { get; set; }
        [XmlElement(ElementName = "Pais", Namespace = "POS_AUTO_SCHEMA")]
        public string Pais { get; set; }
        [XmlElement(ElementName = "MontoEndosado", Namespace = "POS_AUTO_SCHEMA")]
        public string MontoEndosado { get; set; }
        [XmlElement(ElementName = "Rnc", Namespace = "POS_AUTO_SCHEMA")]
        public string Rnc { get; set; }
        [XmlElement(ElementName = "Beneficiary", Namespace = "POS_AUTO_SCHEMA")]
        public string Beneficiary { get; set; }
        [XmlElement(ElementName = "Fullname", Namespace = "POS_AUTO_SCHEMA")]
        public string Fullname { get; set; }
        [XmlElement(ElementName = "Banco", Namespace = "POS_AUTO_SCHEMA")]
        public string Banco { get; set; }
        [XmlElement(ElementName = "Intermediario", Namespace = "POS_AUTO_SCHEMA")]
        public string Intermediario { get; set; }
        [XmlElement(ElementName = "Loan", Namespace = "POS_AUTO_SCHEMA")]
        public Loan Loan { get; set; }
    }

    [XmlRoot(ElementName = "Contract", Namespace = "POS_AUTO_SCHEMA")]
    public class Contract
    {
        [XmlElement(ElementName = "LoanNumber", Namespace = "POS_AUTO_SCHEMA")]
        public string LoanNumber { get; set; }
        [XmlElement(ElementName = "CustomerName", Namespace = "POS_AUTO_SCHEMA")]
        public string CustomerName { get; set; }
        [XmlElement(ElementName = "Citizenship", Namespace = "POS_AUTO_SCHEMA")]
        public string Citizenship { get; set; }
        [XmlElement(ElementName = "CivilStatus", Namespace = "POS_AUTO_SCHEMA")]
        public string CivilStatus { get; set; }
        [XmlElement(ElementName = "Id", Namespace = "POS_AUTO_SCHEMA")]
        public string Id { get; set; }
        [XmlElement(ElementName = "Address1", Namespace = "POS_AUTO_SCHEMA")]
        public string Address1 { get; set; }
        [XmlElement(ElementName = "Address2", Namespace = "POS_AUTO_SCHEMA")]
        public string Address2 { get; set; }
        [XmlElement(ElementName = "CompanyRepresentative", Namespace = "POS_AUTO_SCHEMA")]
        public string CompanyRepresentative { get; set; }
        [XmlElement(ElementName = "CompanyRepCiticenship", Namespace = "POS_AUTO_SCHEMA")]
        public string CompanyRepCiticenship { get; set; }
        [XmlElement(ElementName = "CompanyRepId", Namespace = "POS_AUTO_SCHEMA")]
        public string CompanyRepId { get; set; }
        [XmlElement(ElementName = "QuotationNumber", Namespace = "POS_AUTO_SCHEMA")]
        public string QuotationNumber { get; set; }
        [XmlElement(ElementName = "InsuranceCompany", Namespace = "POS_AUTO_SCHEMA")]
        public string InsuranceCompany { get; set; }
        [XmlElement(ElementName = "LoanAmountString", Namespace = "POS_AUTO_SCHEMA")]
        public string LoanAmountString { get; set; }
        [XmlElement(ElementName = "NumberOfPaymentString", Namespace = "POS_AUTO_SCHEMA")]
        public string NumberOfPaymentString { get; set; }
        [XmlElement(ElementName = "PaymentAmountString", Namespace = "POS_AUTO_SCHEMA")]
        public string PaymentAmountString { get; set; }
        [XmlElement(ElementName = "LoanRateString", Namespace = "POS_AUTO_SCHEMA")]
        public string LoanRateString { get; set; }
        [XmlElement(ElementName = "ContractDateString", Namespace = "POS_AUTO_SCHEMA")]
        public string ContractDateString { get; set; }
        [XmlElement(ElementName = "CreditCardType", Namespace = "POS_AUTO_SCHEMA")]
        public string CreditCardType { get; set; }
        [XmlElement(ElementName = "CreditCardNumber", Namespace = "POS_AUTO_SCHEMA")]
        public string CreditCardNumber { get; set; }
        [XmlElement(ElementName = "CreditCardExpirationDate", Namespace = "POS_AUTO_SCHEMA")]
        public string CreditCardExpirationDate { get; set; }
    }

    [XmlRoot(ElementName = "POS_AUTO", Namespace = "POS_AUTO_SCHEMA")]
    public class POS_AUTO
    {
        [XmlElement(ElementName = "Transaction", Namespace = "POS_AUTO_SCHEMA")]
        public Transaction Transaction { get; set; }
        [XmlElement(ElementName = "Contract", Namespace = "POS_AUTO_SCHEMA")]
        public Contract Contract { get; set; }
    } 
}



