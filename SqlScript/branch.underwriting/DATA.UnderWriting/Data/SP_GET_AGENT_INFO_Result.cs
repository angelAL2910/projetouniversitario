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
    
    public partial class SP_GET_AGENT_INFO_Result
    {
        public int Corp_Id { get; set; }
        public int Agent_Id { get; set; }
        public Nullable<int> Identity_Type_Id { get; set; }
        public string ID { get; set; }
        public string Agent_Code { get; set; }
        public string Name_Id { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string First_Lastname { get; set; }
        public string Second_Lastname { get; set; }
        public string Nickname { get; set; }
        public Nullable<System.DateTime> Dob { get; set; }
        public string Gender { get; set; }
        public Nullable<int> Marital_Stat_Id { get; set; }
        public Nullable<int> Residence_Country_Id { get; set; }
        public Nullable<int> Birth_Country_Id { get; set; }
        public Nullable<int> Citizenship_Country_Id { get; set; }
        public int Directory_Id { get; set; }
        public string Agent_Type_Desc { get; set; }
        public string Marital_Status_Desc { get; set; }
        public string Country_Of_Birth_Desc { get; set; }
        public string Kco_Unique_Id { get; set; }
    }
}
