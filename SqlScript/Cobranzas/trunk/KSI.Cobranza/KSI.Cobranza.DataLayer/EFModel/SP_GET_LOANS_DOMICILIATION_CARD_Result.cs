//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KSI.Cobranza.DataLayer.EFModel
{
    using System;
    
    public partial class SP_GET_LOANS_DOMICILIATION_CARD_Result
    {
        public long clientId { get; set; }
        public Nullable<long> accountId { get; set; }
        public int CardTypeId { get; set; }
        public string CardTypeDesc { get; set; }
        public string LastFourDigits { get; set; }
        public string CardNumber { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public string CVV2 { get; set; }
        public string CardHolder { get; set; }
        public string ExpirationDateMMYYYY { get; set; }
        public Nullable<bool> IsMain { get; set; }
        public Nullable<bool> ApplyRange { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModiDate { get; set; }
        public Nullable<int> CreateUsrId { get; set; }
        public Nullable<int> ModiUsrId { get; set; }
        public string hostName { get; set; }
        public Nullable<bool> HasLoan { get; set; }
    }
}