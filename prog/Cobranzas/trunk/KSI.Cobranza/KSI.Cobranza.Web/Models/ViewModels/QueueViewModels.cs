﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace KSI.Cobranza.Web.Models.ViewModels
{

    public class QueueViewModels
    {
        public string LoanNumber { get; set; }
        public string Id { get; set; }
        public string CustomerFullName { get; set; }
        public decimal AmountFinanced { get; set; }    
        public decimal InterestRate { get; set; }
        public string ExecutiveName { get; set; }
        public string SponsorName { get; set; }
        public int Term { get; set; }
        public int queueTypeId { get; set; }
        public string CreditState { get; set; }
        public string Office { get; set; }
        public string Product { get; set; }
        public int QueuePosition { get; set; }
        public DateTime ActionDate { get; set; }
        public string ReferrerDepartment { get; set; }
        public string AssignTo { get; set; }
        public string Typeoftracking { get; set; }
        public string ToCall { get; set; }
        public decimal AmountOwed { get; set; }
        public string KindOfFollowup { get; set; }
        public decimal Charge { get; set; }
        public string _dataRequest { get; set; }
        public long quotationId { get; set; }
        public string COMMENTS { get; set; }
        public long relatedContactId { get; set; }

        public string AmountFinancedF
        {
            get
            {

                return this.AmountFinanced.ToString("#,0.00", CultureInfo.InvariantCulture);
            }
        }

        public string InterestRateF
        {
            get
            {

                return this.InterestRate.ToString("#,0.00", CultureInfo.InvariantCulture);
            }
        }

        public string AmountOwedF
        {
            get
            {

                return this.AmountOwed.ToString("#,0.00", CultureInfo.InvariantCulture);
            }
        }

        public string ActionDateF
        {
            get
            {

                return this.ActionDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }    
        
    }
}