﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.NewBusiness.Common.Illustration.BusinessLogic
{
    public class CalculatePlanModel
    {
        public double PeriodicPremium { get; set; }
        public double PeriodicPremiumPerFrequency { get; set; }
        public double MinimumPremium { get; set; }
        public double InsuredAmount { get; set; }
        public double AnnuityAmount { get; set; }
        public double TotalInsuredAmount { get; set; }
        public double TotalRetirementAmount { get; set; }
        public double SumAnnualPremium { get; set; }
        public double TargetPremium { get; set; }
        public double FractionSurcharge { get; set; }
        public double NetAnnualPremium { get; set; } //Bmarroquin 04-05-2017
    }
}