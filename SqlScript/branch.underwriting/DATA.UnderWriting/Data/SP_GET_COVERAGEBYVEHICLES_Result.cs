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
    
    public partial class SP_GET_COVERAGEBYVEHICLES_Result
    {
        public string Policy_No { get; set; }
        public int corp_id { get; set; }
        public int region_id { get; set; }
        public int country_id { get; set; }
        public int domesticreg_id { get; set; }
        public int state_prov_id { get; set; }
        public int city_id { get; set; }
        public int office_id { get; set; }
        public int case_seq_no { get; set; }
        public int hist_seq_no { get; set; }
        public int insured_vehicle_id { get; set; }
        public string registration_id { get; set; }
        public Nullable<int> vehicle_type_id { get; set; }
        public string Vehicle_Type_Desc { get; set; }
        public Nullable<int> year { get; set; }
        public string chassis { get; set; }
        public string registry { get; set; }
        public Nullable<int> bl_id { get; set; }
        public Nullable<int> bl_type_id { get; set; }
        public Nullable<int> product_id { get; set; }
        public string product_desc { get; set; }
        public Nullable<decimal> unitary_price { get; set; }
        public Nullable<decimal> package_price { get; set; }
        public Nullable<decimal> tollerable_minimal_price { get; set; }
        public Nullable<int> coverage_status { get; set; }
        public Nullable<bool> is_delete { get; set; }
        public Nullable<int> delete_usrid { get; set; }
        public Nullable<int> sale_currency_id { get; set; }
        public Nullable<int> assig_curr_sale { get; set; }
        public Nullable<int> product_type_id { get; set; }
        public string product_type_desc { get; set; }
        public string namekey { get; set; }
        public Nullable<bool> required { get; set; }
        public Nullable<int> detail_type { get; set; }
        public string detail_value { get; set; }
        public Nullable<int> Group_Id { get; set; }
        public string Group_Desc { get; set; }
        public Nullable<int> coverage_id { get; set; }
        public Nullable<int> coverage_type_id { get; set; }
        public string coverage_desc { get; set; }
        public Nullable<int> Coverage_Limit_MaX { get; set; }
        public string Namekey_coverage { get; set; }
        public string Make_Desc { get; set; }
        public string Model_Desc { get; set; }
        public string Cilindros { get; set; }
        public string Color_Desc { get; set; }
    }
}