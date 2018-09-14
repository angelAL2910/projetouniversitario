﻿using System;

namespace WEB.UnderWriting.Common
{
    public partial class WUCLeftMenu : WEB.UnderWriting.Common.UC, WEB.UnderWriting.Common.IUC
    {
        public delegate void AdvanceSearchFilter();
        public event AdvanceSearchFilter LeftAdvanceSearchFilter;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            lblVersionNo.Text = System.Configuration.ConfigurationManager.AppSettings["Version"];
            FillDrops();
            FillData();
        }

        private void FillDrops()
        {
            Service.DropDowns.GetDropDown(ref OfficeDDL, Language.English, DropDownType.Office, Service.Corp_Id, null, null, null, null, null, null, null, null, null, null,companyId:Service.CompanyId);
            Service.DropDowns.GetDropDown(ref UnderWriterDDL, Language.English, DropDownType.Underwriter, Service.Corp_Id, userId: Service.Underwriter_Id,companyId:Service.CompanyId);
            Service.DropDowns.GetDropDown(ref PlanNameDDL, Language.English, DropDownType.Product, Service.Corp_Id, companyId: Service.CompanyId);
        }
        
        public void Translator(string Lang)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public void clearData()
        {
            dateFromTxt.Text = String.Empty;
            dateToTxt.Text = String.Empty;
            txtSearchByPolicy.Text = String.Empty;
            txtClient.Text = String.Empty;
            OfficeDDL.SelectedIndex = -1;
            PlanNameDDL.SelectedIndex = -1;
            ManagerDDL.SelectedIndex = -1;
            UnderWriterDDL.SelectedIndex = -1;
            SubManagerDDL.SelectedIndex = -1;
            AgentDDL.SelectedIndex = -1;

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
            gvQueue.DataSource = Services.CaseManager.GetQueue(Service.CompanyId, Service.Underwriter_Id);
            gvQueue.DataBind();
        }

        protected void btnAdSearchSubmit_Click(object sender, EventArgs e)
        {

           // var selectPlan = Request.Form[PlanNameDDL.UniqueID] == null ? "-1" : Request.Form[PlanNameDDL.UniqueID];
            var searchResult = new Entity.UnderWriting.Entities.Case.SearchResult()
            {
                CorpId = OfficeDDL.SelectedIndex < 1 ? (int?)null : int.Parse(OfficeDDL.SelectedValue.Split('|')[0]),
                RegionId = OfficeDDL.SelectedIndex < 1 ? (int?)null : int.Parse(OfficeDDL.SelectedValue.Split('|')[1]),
                CountryId = OfficeDDL.SelectedIndex < 1 ? (int?)null : int.Parse(OfficeDDL.SelectedValue.Split('|')[2]),
                DomesticregId = OfficeDDL.SelectedIndex < 1 ? (int?)null : int.Parse(OfficeDDL.SelectedValue.Split('|')[3]),
                StateProvId = OfficeDDL.SelectedIndex < 1 ? (int?)null : int.Parse(OfficeDDL.SelectedValue.Split('|')[4]),
                CityId = OfficeDDL.SelectedIndex < 1 ? (int?)null : int.Parse(OfficeDDL.SelectedValue.Split('|')[5]),
                OfficeId = OfficeDDL.SelectedIndex < 1 ? (int?)null : int.Parse(OfficeDDL.SelectedValue.Split('|')[6]),

                CompanyId = Service.CompanyId,
                FromDate = String.IsNullOrEmpty(dateFromTxt.Text) ? (DateTime?)null : DateTime.Parse(dateFromTxt.Text),
                ToDate = String.IsNullOrEmpty(dateToTxt.Text) ? (DateTime?)null : DateTime.Parse(dateToTxt.Text),
                BlTypeId = PlanNameDDL.SelectedIndex < 1 ? (int?)null : int.Parse(PlanNameDDL.SelectedValue.Split('|')[1]),
                BlId = PlanNameDDL.SelectedIndex < 1 ? (int?)null : int.Parse(PlanNameDDL.SelectedValue.Split('|')[2]),
                ProductId = PlanNameDDL.SelectedIndex < 1 ? (int?)null : int.Parse(PlanNameDDL.SelectedValue.Split('|')[0]),
                PolicyNo = String.IsNullOrEmpty(txtSearchByPolicy.Text) ? null : txtSearchByPolicy.Text,
                ContactFullName = String.IsNullOrEmpty(txtClient.Text) ? null : txtClient.Text,

                AgentIdManager = ManagerDDL.SelectedIndex < 1 ? (int?)null : int.Parse(ManagerDDL.SelectedValue.Split('|')[1]),
                AgentIdSubManager = SubManagerDDL.SelectedIndex < 1 ? (int?)null : int.Parse(SubManagerDDL.SelectedValue.Split('|')[1]),
                AgentId = AgentDDL.SelectedIndex < 1 ? (int?)null : int.Parse(AgentDDL.SelectedValue.Split('|')[1]),
                UnderwriterId = UnderWriterDDL.SelectedIndex < 1 ? (int?)null : int.Parse(UnderWriterDDL.SelectedValue)
            };

            Service.SearchResultParameters = searchResult;
            LeftAdvanceSearchFilter();
        }

        protected void btnUWLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["SecurityLogin"]);
        }

        protected void btnAdSearchClear_Click(object sender, EventArgs e)
        {
            clearData();
        }
    }
}