﻿using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace WEB.NewBusiness.Common.Thunderhead.KSI.Propiedad
{
    [XmlRoot(ElementName = "Transaction")]
    public class Transaction
    {
        [XmlElement(ElementName = "DocumentId")]
        public string DocumentId { get; set; }
        [XmlElement(ElementName = "Direccion")]
        public string Direccion { get; set; }
        [XmlElement(ElementName = "Ciudad")]
        public string Ciudad { get; set; }
        [XmlElement(ElementName = "Pais")]
        public string Pais { get; set; }
        [XmlElement(ElementName = "MontoEndosado")]
        public string MontoEndosado { get; set; }
        [XmlElement(ElementName = "Rnc")]
        public string Rnc { get; set; }
        [XmlElement(ElementName = "Beneficiary")]
        public string Beneficiary { get; set; }
        [XmlElement(ElementName = "Fullname")]
        public string Fullname { get; set; }
        [XmlElement(ElementName = "Banco")]
        public string Banco { get; set; }
        [XmlElement(ElementName = "Intermediario")]
        public string Intermediario { get; set; }
    }

    [XmlRoot(ElementName = "Fee")]
    public class Fee
    {
        [XmlElement(ElementName = "Number")]
        public string Number { get; set; }
        [XmlElement(ElementName = "Date")]
        public string Date { get; set; }
        [XmlElement(ElementName = "Amount")]
        public string Amount { get; set; }
        [XmlElement(ElementName = "Capital")]
        public string Capital { get; set; }
        [XmlElement(ElementName = "Interests")]
        public string Interests { get; set; }
        [XmlElement(ElementName = "Comission")]
        public string Comission { get; set; }
        [XmlElement(ElementName = "Spends")]
        public string Spends { get; set; }
        [XmlElement(ElementName = "Total")]
        public string Total { get; set; }
    }

    [XmlRoot(ElementName = "Loan")]
    public class Loan
    {
        [XmlElement(ElementName = "Account")]
        public string Account { get; set; }
        [XmlElement(ElementName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "AccountName")]
        public string AccountName { get; set; }
        [XmlElement(ElementName = "Client")]
        public string Client { get; set; }
        [XmlElement(ElementName = "FoundsSource")]
        public string FoundsSource { get; set; }
        [XmlElement(ElementName = "FundsDestination")]
        public string FundsDestination { get; set; }
        [XmlElement(ElementName = "CredtitFacility")]
        public string CredtitFacility { get; set; }
        [XmlElement(ElementName = "Comite")]
        public string Comite { get; set; }
        [XmlElement(ElementName = "PaymentMethod")]
        public string PaymentMethod { get; set; }
        [XmlElement(ElementName = "RequestedAmount")]
        public string RequestedAmount { get; set; }
        [XmlElement(ElementName = "ApprovedAmount")]
        public string ApprovedAmount { get; set; }
        [XmlElement(ElementName = "ReleasedAmount")]
        public string ReleasedAmount { get; set; }
        [XmlElement(ElementName = "CapitalReturn")]
        public string CapitalReturn { get; set; }
        [XmlElement(ElementName = "LastCut")]
        public string LastCut { get; set; }
        [XmlElement(ElementName = "Interest")]
        public string Interest { get; set; }
        [XmlElement(ElementName = "Comission")]
        public string Comission { get; set; }
        [XmlElement(ElementName = "DelayFee")]
        public string DelayFee { get; set; }
        [XmlElement(ElementName = "FeeAmount")]
        public string FeeAmount { get; set; }
        [XmlElement(ElementName = "PaymentPeriod")]
        public string PaymentPeriod { get; set; }
        [XmlElement(ElementName = "Frequency")]
        public string Frequency { get; set; }
        [XmlElement(ElementName = "FeeNumber")]
        public string FeeNumber { get; set; }
        [XmlElement(ElementName = "RequestDate")]
        public string RequestDate { get; set; }
        [XmlElement(ElementName = "ApprovementDate")]
        public string ApprovementDate { get; set; }
        [XmlElement(ElementName = "ReleasedDate")]
        public string ReleasedDate { get; set; }
        [XmlElement(ElementName = "ExpirationDate")]
        public string ExpirationDate { get; set; }
        [XmlElement(ElementName = "NextPaymentDate")]
        public string NextPaymentDate { get; set; }
        [XmlElement(ElementName = "TotalCapital")]
        public string TotalCapital { get; set; }
        [XmlElement(ElementName = "TotalInterests")]
        public string TotalInterests { get; set; }
        [XmlElement(ElementName = "TotalComissions")]
        public string TotalComissions { get; set; }
        [XmlElement(ElementName = "TotalSpends")]
        public string TotalSpends { get; set; }
        [XmlElement(ElementName = "TotalAmount")]
        public string TotalAmount { get; set; }
        [XmlElement(ElementName = "Fee")]
        public List<Fee> Fee { get; set; }
    }

    [XmlRoot(ElementName = "Contract")]
    public class Contract
    {
        [XmlElement(ElementName = "LoanNumber")]
        public string LoanNumber { get; set; }
        [XmlElement(ElementName = "CustomerName")]
        public string CustomerName { get; set; }
        [XmlElement(ElementName = "Citizenship")]
        public string Citizenship { get; set; }
        [XmlElement(ElementName = "CivilStatus")]
        public string CivilStatus { get; set; }
        [XmlElement(ElementName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "Address1")]
        public string Address1 { get; set; }
        [XmlElement(ElementName = "Address2")]
        public string Address2 { get; set; }
        [XmlElement(ElementName = "CompanyRepresentative")]
        public string CompanyRepresentative { get; set; }
        [XmlElement(ElementName = "CompanyRepCiticenship")]
        public string CompanyRepCiticenship { get; set; }
        [XmlElement(ElementName = "CompanyRepId")]
        public string CompanyRepId { get; set; }
        [XmlElement(ElementName = "QuotationNumber")]
        public string QuotationNumber { get; set; }
        [XmlElement(ElementName = "InsuranceCompany")]
        public string InsuranceCompany { get; set; }
        [XmlElement(ElementName = "LoanAmountString")]
        public string LoanAmountString { get; set; }
        [XmlElement(ElementName = "NumberOfPaymentString")]
        public string NumberOfPaymentString { get; set; }
        [XmlElement(ElementName = "PaymentAmountString")]
        public string PaymentAmountString { get; set; }
        [XmlElement(ElementName = "LoanRateString")]
        public string LoanRateString { get; set; }
        [XmlElement(ElementName = "ContractDateString")]
        public string ContractDateString { get; set; }
        [XmlElement(ElementName = "CreditCardType")]
        public string CreditCardType { get; set; }
        [XmlElement(ElementName = "CreditCardNumber")]
        public string CreditCardNumber { get; set; }
        [XmlElement(ElementName = "CreditCardExpirationDate")]
        public string CreditCardExpirationDate { get; set; }        
    }

    [XmlRoot(ElementName = "dataset")]
    public class Dataset
    {
        [XmlElement(ElementName = "Transaction")]
        public Transaction Transaction { get; set; }
        [XmlElement(ElementName = "Loan")]
        public Loan Loan { get; set; }
        [XmlElement(ElementName = "Contract")]
        public Contract Contract { get; set; }
    }

}


