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
    
    public partial class SP_GET_AGENT_COMMUNICATION_INFORMATION_Result
    {
        public int Corp_Id { get; set; }
        public int Directory_Id { get; set; }
        public int Dir_Detail_Id { get; set; }
        public int Comm_Type_Id { get; set; }
        public int Directory_Type_Id { get; set; }
        public string Dir_Type_Short_Desc { get; set; }
        public Nullable<int> Phone_Type_Id { get; set; }
        public string Phone_Prefix { get; set; }
        public string Area_Code { get; set; }
        public string Phone_Number { get; set; }
        public string Phone_Ext { get; set; }
        public string Address { get; set; }
        public string Person_To_Contact { get; set; }
        public Nullable<int> Region_Id { get; set; }
        public Nullable<int> Country_Id { get; set; }
        public string Global_Country_Desc { get; set; }
        public Nullable<int> Domestic_Region_Id { get; set; }
        public Nullable<int> State_Prov_Id { get; set; }
        public string State_Prov_Desc { get; set; }
        public Nullable<int> City_Id { get; set; }
        public string City_Desc { get; set; }
        public string Zip_Code { get; set; }
        public bool isPrimary { get; set; }
        public string CommunicationType { get; set; }
        public int Agent_Id { get; set; }
    }
}
