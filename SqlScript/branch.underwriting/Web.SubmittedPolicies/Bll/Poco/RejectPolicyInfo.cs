﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.SubmittedPolicies.Bll.Poco
{
    public class RejectPolicyInfo
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
        public int Step_Type_Id { get; set; }
        public int Step_Id { get; set; }
        public Nullable<int> Step_Case_No { get; set; }
        public string Policy_No { get; set; }
        public string Product_Desc { get; set; }
        public string AgentFullName { get; set; }
        public string OwnerFullName { get; set; }
        public Nullable<decimal> Initial_Premium { get; set; }
        public string Office_Desc { get; set; }
        public String Initial_Premium_F
        {
            get { return Initial_Premium.HasValue ? Initial_Premium.Value.ToString("C2") : "-"; }
        }
    }
}