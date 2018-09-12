﻿using System;
using System.Linq;
using WEB.UnderWriting.Common;

namespace WEB.UnderWriting.Case.UserControls.MedicalInfo
{
    public partial class UCHealtDeclaration : WEB.UnderWriting.Common.UC, WEB.UnderWriting.Common.IUC
    {
        //IMedical MedicalManager
        //{
        //    get { return diManager.MedicalManager; }
        //}

        DropDownManager DropDowns = new DropDownManager();
        // UnderWritingDIManager diManager = new UnderWritingDIManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            divBP.Visible =
            gvMedicalCondition.Visible = !(Service.GetProductFamily() == Tools.EFamilyProductType.Funeral);
        }

        void UnderWriting.Common.IUC.Translator(string Lang)
        {
            throw new NotImplementedException();
        }

        void UnderWriting.Common.IUC.readOnly(bool x)
        {
            throw new NotImplementedException();
        }

        void UnderWriting.Common.IUC.edit()
        {
            throw new NotImplementedException();
        }

        void UnderWriting.Common.IUC.FillData()
        {
            throw new NotImplementedException();
        }

        public void clearData()
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            Entity.UnderWriting.Entities.Medical info = new Entity.UnderWriting.Entities.Medical();
            info.CorpId = Service.Corp_Id;
            info.RegionId = Service.Region_Id;
            info.CountryId = Service.Country_Id;
            info.DomesticregId = Service.Domesticreg_Id;
            info.StateProvId = Service.State_Prov_Id;
            info.CityId = Service.City_Id;
            info.OfficeId = Service.Office_Id;
            info.CaseSeqNo = Service.Case_Seq_No;
            info.HistSeqNo = Service.Hist_Seq_No;
            info.ContactId = int.Parse(ddlInsuredType.SelectedValue.Split('|')[0]);
            info.ContactRoleTypeId = int.Parse(ddlInsuredType.SelectedValue.Split('|')[1]);


            if (!string.IsNullOrWhiteSpace(txtDiastolic.Text))
                info.HealthDiastolic = int.Parse(txtDiastolic.Text);
            else
                info.HealthDiastolic = null;


            if (!string.IsNullOrWhiteSpace(txtSystolic.Text))
                info.HealthSystolic = int.Parse(txtSystolic.Text);
            else
                info.HealthSystolic = null;

            if (!string.IsNullOrWhiteSpace(txtAge.Text))
                info.HealthAge = int.Parse(txtAge.Text);
            else
                info.HealthAge = null;

            if (!string.IsNullOrWhiteSpace(txtLastVisit.Text))
                info.HealthLastMedVisit = Convert.ToDateTime(txtLastVisit.Text);
            else
                info.HealthLastMedVisit = null;

            if (Convert.ToInt32(ddlSmoker.SelectedValue) == -1)
                info.HealthSmoke = null;
            else
                info.HealthSmoke = Convert.ToBoolean(Convert.ToInt32(ddlSmoker.SelectedValue));

            info.HealthLastMedResult = txtResult.Text;
            info.HealthLastMedReason = txtReason.Text;

            info.HealthGender = ddlGender.SelectedValue;
            info.HealthDrName = txtDrName.Text;
            info.HealthDrAddress = txtAddresses.Text;
            //info.HealthDrPhoneArea = txtPhoneNumber1.Text;
            //info.HealthDrPhonePrefix = txtPhoneNumber2.Text;
            info.HealthDrPhoneNum = txtPhoneNumber3.Text;
            info.HealthMedication = txtMedications.Text;
            info.HealthWeigth = decimal.Parse(txtHealthWeight.Text);
            info.HealthHeight = decimal.Parse(txtHealthHeight.Text);

            //Entity.UnderWriting.Entities.
            Services.MedicalManager.SetInfo(info);
        }

        public void fillData()
        {
            int Corp_Id = Service.Corp_Id;
            int Region_Id = Service.Region_Id;
            int Country_Id = Service.Country_Id;
            int Domesticreg_Id = Service.Domesticreg_Id;
            int State_Prov_Id = Service.State_Prov_Id;
            int City_Id = Service.City_Id;
            int Office_Id = Service.Office_Id;
            int Case_Seq_No = Service.Case_Seq_No;
            int Hist_Seq_No = Service.Hist_Seq_No;
            var Contact_id = int.Parse(ddlInsuredType.SelectedValue.Split('|')[0]);
            var Contact_Role_type_id = int.Parse(ddlInsuredType.SelectedValue.Split('|')[1]);

            gvMedicalCondition.DataSource = Services.MedicalManager.GetCondition(Corp_Id,
                                                                                 Region_Id,
                                                                                 Country_Id,
                                                                                 Domesticreg_Id,
                                                                                 State_Prov_Id,
                                                                                 City_Id,
                                                                                 Office_Id,
                                                                                 Case_Seq_No,
                                                                                 Hist_Seq_No,
                                                                                 Contact_id,
                                                                                 Contact_Role_type_id);

            gvFamilyMedicalHistory.DataSource = Services.MedicalManager.GetFamilyHistory(Corp_Id,
                                                                                         Region_Id,
                                                                                         Country_Id,
                                                                                         Domesticreg_Id,
                                                                                         State_Prov_Id,
                                                                                         City_Id,
                                                                                         Office_Id,
                                                                                         Case_Seq_No,
                                                                                         Hist_Seq_No,
                                                                                         Contact_id,
                                                                                         Contact_Role_type_id);

            gvFamilyMedicalHistory.DataBind();
            gvMedicalCondition.DataBind();

            //2016-01-29 | Marcos J. Perez
            var medicalInfo = Services.MedicalManager.GetInfo(Corp_Id,
                                                              Region_Id,
                                                              Country_Id,
                                                              Domesticreg_Id,
                                                              State_Prov_Id,
                                                              City_Id,
                                                              Office_Id,
                                                              Case_Seq_No,
                                                              Hist_Seq_No,
                                                              Contact_id,
                                                              Contact_Role_type_id).FirstOrDefault();


            //1 es kilogramos 2 es libras.
            //Si el peso esta en libras convertir a kilogramos
            var weight = 0.0M;
            var height = 0.0M;
            this.hdHealthHeightType.Value = medicalInfo.HealthHeigthTypeId.ToString();
            this.hdHealthWeightType.Value = medicalInfo.HealthWeigthTypeId.ToString();
            if (medicalInfo.HealthWeigthTypeId == 2)
            {
                if (medicalInfo.HealthWeigth.HasValue && medicalInfo.HealthWeigth.Value > 0)
                {
                    weight = (medicalInfo.HealthWeigth.Value / 2.2046M);
                }
            }
            else
            {
                if (medicalInfo.HealthWeigth.HasValue && medicalInfo.HealthWeigth.Value > 0)
                {
                    weight = medicalInfo.HealthWeigth.Value;
                }

            }

            //3 es metro si es 4 es pies.
            //Si la altura esta en pies hay que convertir en metro
            if (medicalInfo.HealthHeigthTypeId == 4)
            {
                if (medicalInfo.HealthHeight.HasValue && medicalInfo.HealthHeight.Value > 0)
                {
                    height = (medicalInfo.HealthHeight.Value / 3.2808M);
                }

            }
            else
            {
                if (medicalInfo.HealthHeight.HasValue && medicalInfo.HealthHeight.Value > 0)
                {
                    height = medicalInfo.HealthHeight.Value;
                }

            }
            //2016-02-12 | Marcos J. Perez
            this.txtHealthHeight.Text = medicalInfo.HealthHeight.HasValue ? medicalInfo.HealthHeight.Value.ToString("N2") : "0";
            this.txtHealthWeight.Text = medicalInfo.HealthWeigth.HasValue ? medicalInfo.HealthWeigth.Value.ToString("N0") : "0";
            //this.txtHealthBMI.Text = Tools.BMI(weight.ToString("N0"), height.ToString("N2"), true);
            this.txtHealthBMI.Text = Tools.BMI(weight.ToString(), height.ToString(), true);
        }

        public void fillDdl()
        {
            DropDowns.GetDropDown(ref ddlGender,
                                  Language.English,
                                  DropDownType.Gender,
                                  Service.Corp_Id,
                                  projectId: Service.ProjectId,
                                  companyId: Service.CompanyId);

            DropDowns.GetDropDown(ref ddlSmoker,
                                  Language.English,
                                  DropDownType.Smoker,
                                  Service.Corp_Id,
                                  projectId: Service.ProjectId,
                                  companyId: Service.CompanyId);

            ddlInsuredType.DataSource = DropDowns.GetDropDown(DropDownType.Summary,
                                                              Service.Corp_Id,
                                                              Service.Region_Id,
                                                              Service.Country_Id,
                                                              Service.Domesticreg_Id,
                                                              Service.State_Prov_Id,
                                                              Service.City_Id,
                                                              Service.Office_Id,
                                                              Service.Case_Seq_No,
                                                              Service.Hist_Seq_No,
                                                              Service.Contact_Id,
                                                              projectId: Service.ProjectId,
                                                              companyId: Service.CompanyId,
                                                              languageId: Service.LanguageId);
            ddlInsuredType.DataBind();

            ddlInsuredType_SelectedIndexChanged(null, null);
        }

        protected void ddlInsuredType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Corp_Id = Service.Corp_Id;
            int Region_Id = Service.Region_Id;
            int Country_Id = Service.Country_Id;
            int Domesticreg_Id = Service.Domesticreg_Id;
            int State_Prov_Id = Service.State_Prov_Id;
            int City_Id = Service.City_Id;
            int Office_Id = Service.Office_Id;
            int Case_Seq_No = Service.Case_Seq_No;
            int Hist_Seq_No = Service.Hist_Seq_No;
            var Contact_id = int.Parse(ddlInsuredType.SelectedValue.Split('|')[0]);
            var Contact_Role_type_id = int.Parse(ddlInsuredType.SelectedValue.Split('|')[1]);


            var medicalInfo = Services.MedicalManager.GetInfo(Corp_Id,
                                                              Region_Id,
                                                              Country_Id,
                                                              Domesticreg_Id,
                                                              State_Prov_Id,
                                                              City_Id,
                                                              Office_Id,
                                                              Case_Seq_No,
                                                              Hist_Seq_No,
                                                              Contact_id,
                                                              Contact_Role_type_id).FirstOrDefault();

            UnderWriting.Common.Tools.SelectIndexByValue(ref ddlSmoker, Convert.ToInt32(medicalInfo.HealthSmoke).ToString(), true);
            UnderWriting.Common.Tools.SelectIndexByValue(ref ddlGender, medicalInfo.HealthGender, true);
            txtMainInsured.Text = ddlInsuredType.SelectedItem.Text;
            txtAge.Text = medicalInfo.HealthAge.ToString();
            txtAddresses.Text = medicalInfo.HealthDrAddress;
            txtDrName.Text = medicalInfo.HealthDrName;
            txtHealthWeight.Text = medicalInfo.HealthWeigth.ToString();
            txtHealthHeight.Text = medicalInfo.HealthHeight.ToString();
            txtSystolic.Text = medicalInfo.HealthSystolic.ToString();
            txtDiastolic.Text = medicalInfo.HealthDiastolic.ToString();
            txtLastVisit.Text = medicalInfo.HealthLastMedVisit.ToString();
            txtReason.Text = medicalInfo.HealthLastMedReason;
            txtMedications.Text = medicalInfo.HealthMedication;
            txtResult.Text = medicalInfo.HealthLastMedResult;
            //txtPhoneNumber1.Text = medicalInfo.HealthDrPhoneArea;
            //txtPhoneNumber2.Text = medicalInfo.HealthDrPhonePrefix;
            txtPhoneNumber3.Text = medicalInfo.HealthDrPhoneNum != null ? medicalInfo.HealthDrPhoneNum.Replace("-", "") : string.Empty;

            var pageControl = (this.Page.Master.FindControl("Left").FindControl("Left").FindControl("UCMedicalExams") as UCMedicalExams);
            pageControl.fillData(Contact_id);
            fillData();
        }
    }
}