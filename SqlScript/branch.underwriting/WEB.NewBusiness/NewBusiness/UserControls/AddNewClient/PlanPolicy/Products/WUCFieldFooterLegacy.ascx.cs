﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.AddNewClient.PlanPolicy.Products
{
    public partial class WUCFieldFooterLegacy : UC
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void EnableControls(bool x)
        {
            Utility.EnableControls(pnFooter.Controls, x);
        }
    }
}