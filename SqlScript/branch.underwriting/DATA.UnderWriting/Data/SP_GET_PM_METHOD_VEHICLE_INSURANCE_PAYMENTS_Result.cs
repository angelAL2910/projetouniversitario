//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATA.UnderWriting.Data
{
    using System;
    
    public partial class SP_GET_PM_METHOD_VEHICLE_INSURANCE_PAYMENTS_Result
    {
        public int Corp_Id { get; set; }
        public int Method_Of_Payment_Id { get; set; }
        public int Payment_Id { get; set; }
        public int Method_Of_Payment_Type_Id { get; set; }
        public string Document_No { get; set; }
        public string Approval_Number { get; set; }
        public string Expiration_Date { get; set; }
        public string Card_Type { get; set; }
        public string Bank_Code { get; set; }
        public decimal Payment_Amount { get; set; }
        public System.DateTime Transaction_Date { get; set; }
        public bool Method_Record_Status { get; set; }
        public bool Payment_Status { get; set; }
        public string SerialID { get; set; }
        public string Policy_SerialID { get; set; }
    }
}