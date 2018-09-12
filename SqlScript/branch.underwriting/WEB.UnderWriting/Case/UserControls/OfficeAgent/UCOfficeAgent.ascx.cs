﻿using System;
using System.Linq;
using WEB.UnderWriting.Common;

namespace WEB.UnderWriting.Case.UserControls.OfficeAgent
{
    public partial class UCOfficeAgent : WEB.UnderWriting.Common.UC, WEB.UnderWriting.Common.IUC
    {
        //UnderWritingDIManager diManager = new UnderWritingDIManager();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void clearData()
        {
            throw new NotImplementedException();
        }

        public void Translator(string Lang)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public void readOnly(bool x)
        {
            throw new NotImplementedException();
        }

        public void edit()
        {
            throw new NotImplementedException();
        }

        public void FillData()
        {
            var data = Services.PolicyManager.GetAgentChainDetail(Service.Corp_Id, Service.Region_Id, Service.Country_Id, Service.Domesticreg_Id,
                Service.State_Prov_Id, Service.City_Id, Service.Office_Id, Service.Case_Seq_No, Service.Hist_Seq_No);
            var MainData = data.Where(r => r.OrderId == 1);

            var MainDataWithOutLevel = data.Where(r => r.OrderId == 1).Select(s => new Entity.UnderWriting.Entities.Policy.AgentChainDetail()
            {
                FullName = s.FullName,
                OfficeDescription = s.OfficeDescription,
                CommTable = s.CommTable,
                ProductDescription = s.ProductDescription
            }).Distinct().ToList();

            rptData.DataSource = MainDataWithOutLevel;
            rptData.DataBind();

            gvAllAgents.DataSource = data;
            gvAllAgents.DataBind();
        }
    }
}