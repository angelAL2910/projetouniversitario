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
    
    public partial class SP_GET_POLICIES_INSURED_Result
    {
        public int corp_id { get; set; }
        public int region_id { get; set; }
        public int country_id { get; set; }
        public int office_id { get; set; }
        public int case_seq_no { get; set; }
        public int hist_seq_no { get; set; }
        public int domesticreg_id { get; set; }
        public int state_prov_id { get; set; }
        public int city_id { get; set; }
        public int weigth { get; set; }
        public int weight_scale_type_id { get; set; }
        public int height { get; set; }
        public int height_scale_type_id { get; set; }
        public string Labor_tasks { get; set; }
        public Nullable<int> Company_Start_Date { get; set; }
        public int Relationship_to_Agent_Id { get; set; }
        public int Keep_Age { get; set; }
        public Nullable<decimal> Reinsurance_Amount { get; set; }
        public Nullable<decimal> Benefit_Amount { get; set; }
        public Nullable<decimal> Annual_Premium { get; set; }
        public Nullable<decimal> Target_Premium { get; set; }
        public Nullable<decimal> Rop_Amount { get; set; }
        public System.DateTime create_date { get; set; }
        public System.DateTime modi_date { get; set; }
        public int create_usrid { get; set; }
        public int modi_usrid { get; set; }
        public string hostname { get; set; }
        public string serialid { get; set; }
        public int contact_role_type_id { get; set; }
        public Nullable<int> beneficiary_type_id { get; set; }
        public Nullable<int> primary_beneficiary_id { get; set; }
        public Nullable<int> primary_beneficiary { get; set; }
        public Nullable<int> relationship_to_owner_id { get; set; }
        public Nullable<int> rel_to_primary_benef_id { get; set; }
        public Nullable<decimal> benefits_percent { get; set; }
        public string relation { get; set; }
        public string Source_ID { get; set; }
    }
}
