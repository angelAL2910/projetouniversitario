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
    
    public partial class SP_SPC_GET_POLICY_DATA_INVEST_PROFILE_DETAIL_Result
    {
        public int Corp_Id { get; set; }
        public int Region_Id { get; set; }
        public int Country_Id { get; set; }
        public int Domesticreg_Id { get; set; }
        public int State_Prov_Id { get; set; }
        public int City_Id { get; set; }
        public int Office_Id { get; set; }
        public int Case_Seq_No { get; set; }
        public int Hist_Seq_No { get; set; }
        public int Profile_Type_Id { get; set; }
        public int Invest_Product_Date_Id { get; set; }
        public int Symbol_Id { get; set; }
        public string SYMBOL_DESC { get; set; }
        public string SYMBOL_ABBR { get; set; }
        public System.DateTime Investment_Profile_Date { get; set; }
        public System.DateTime Invst_Product_Date { get; set; }
        public decimal Invst_Profile_Percent { get; set; }
        public int Stock_Exchange_Id { get; set; }
        public string STOCK_EXCHANGE_DESC { get; set; }
        public decimal Projection_Rate { get; set; }
        public int Investment_Currency { get; set; }
        public decimal Min_Percent_Allowed { get; set; }
        public decimal Max_Percent_Allowed { get; set; }
        public System.DateTime initial_valid_date { get; set; }
        public System.DateTime end_valid_date { get; set; }
        public System.DateTime Create_Date { get; set; }
        public Nullable<System.DateTime> Modi_Date { get; set; }
        public int Create_UsrId { get; set; }
        public Nullable<int> Modi_UsrId { get; set; }
        public string Source_ID { get; set; }
    }
}
