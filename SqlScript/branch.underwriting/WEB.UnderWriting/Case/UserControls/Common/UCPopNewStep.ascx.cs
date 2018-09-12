﻿using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.UnderWriting.Common;

namespace WEB.UnderWriting.Case.UserControls.Common
{
    public partial class UCPopNewStep : WEB.UnderWriting.Common.UC, WEB.UnderWriting.Common.IUC
    {
        public Common.Right UCRight
        {
            get { return (Common.Right)this.Parent.Parent.Parent.Parent; }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
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
            FillDdls();
            ClearFields();
        }

        private void ClearFields()
        {
            txtNSComments.Text = String.Empty;
            ddlNSSelectStep.SelectedIndex = ddlNSSelectStep.Items.Count > 0 ? 0 : -1;
            upPopAddNewStep.Update();
        }

        private void FillDdls()
        {

            Service.DropDowns.GetDropDown(ref ddlNSSelectStep, Language.English, DropDownType.StepCatalog, Service.Corp_Id, null, null, null, null, null, null, null, null, null, null, projectId: Service.ProjectId, companyId: Service.CompanyId);
        }

        protected void btnNSAdd_Click(object sender, EventArgs e)
        {
            if (ddlNSSelectStep.SelectedIndex < 1)
            {
                string message = "Please select a \"New Step\" and try again.";
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Required Field');", true);
                return;
            }
            else if (String.IsNullOrEmpty(txtNSComments.Text.Trim()))
            {
                string message = "Please provide a \"Step Comment\" and try again.";
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Required Field');", true);
                
            }


            if (ddlNSSelectStep.SelectedValue.Split('|')[2] == "PRINTPLA")
            {
                var messages = Service.isPolicyComplete().Where(x => x.isComplete == false);
                if (messages.Any())
                {
                    string message = "";
                    messages.ToList().ForEach(x => message += x.message + "<br>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 350, true, 'Warning');", true);

                    //lnkAllOpen
                    return;

                }
                else{

                    Services.StepManager.Insert(GetNewStep());
                    ScriptManager.RegisterStartupScript(this, GetType(), "GoOutGrids", "GoOutGrids();", true); 
                }

            }/*
              else if(ddlNSSelectStep.SelectedValue.Split('|')[2] == "CHANGES"){
              * 
              * INSERTAR SPECIAL STEP CON EL STATUS ESPECIAL
              * }
              */
            // wcastro. 10-03-2017
            else if (ddlNSSelectStep.SelectedValue.Split('|')[2] == "REJBYSUS")
            {
                Services.StepManager.Insert(GetNewStep());
                ScriptManager.RegisterStartupScript(this, GetType(), "GoOutGrids", "GoOutGrids();", true);
            }
            //fin. wcastro. 10-03-2017
            else
            {
                Services.StepManager.Insert(GetNewStep());
            }

              //  Services.StepManager.Insert(GetNewStep());
            
            if (UCRight != null)
            {
                var hdnVisible = (HiddenField)UCRight.FindControl("hdnNSShowPop");

                if (hdnVisible != null)
                    hdnVisible.Value = "false";

                UCRight.FillData();
                ClearFields();
                this.ExcecuteJScript("initializatePopupsAndClose('#dvPopNSShowPop')");
            }
        }

        private Entity.UnderWriting.Entities.Step.NewStep GetNewStep()
        {
            var step = new Entity.UnderWriting.Entities.Step.NewStep
            {
                StepId = int.Parse(ddlNSSelectStep.SelectedValue.Split('|')[1]),
                StepTypeId = int.Parse(ddlNSSelectStep.SelectedValue.Split('|')[0]),
                Note = txtNSComments.Text,
                UserId = Service.Underwriter_Id,
                CorpId = Service.Corp_Id,
                RegionId = Service.Region_Id,
                CountryId = Service.Country_Id,
                DomesticregId = Service.Domesticreg_Id,
                StateProvId = Service.State_Prov_Id,
                CityId = Service.City_Id,
                OfficeId = Service.Office_Id,
                CaseSeqNo = Service.Case_Seq_No,
                HistSeqNo = Service.Hist_Seq_No
            };

            return step;
        }
    }
}