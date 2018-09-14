﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.AddNewClient.HealthDeclaration
{
    public partial class UCTextboxComments : UC
    {
        public string SetTitles
        {

            set
            {
                lblDrownDawnTitles.Text = value;
            }

        }

        public int OptionId
        {
            get
            {
                int result = -1;

                if (ViewState["OptionId"] != null)
                {
                    result = (int)ViewState["OptionId"];
                }

                return result;
            }
            set
            {
                ViewState["OptionId"] = value;
            }
        }

        public TextBox Value
        {
            get
            {
                return txtTextBoxt;
            }

        }

        public void setDiv(bool IsWidht100)
        {
            divDrop.Attributes.Remove("class");

            if (IsWidht100)
                divDrop.Attributes.Add("class", "de_uno fix_height");
            else
                divDrop.Attributes.Add("class", "de_dos fix_height");

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}