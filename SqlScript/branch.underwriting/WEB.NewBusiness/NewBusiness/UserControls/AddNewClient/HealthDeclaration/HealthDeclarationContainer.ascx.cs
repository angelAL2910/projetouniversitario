﻿using Entity.UnderWriting.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;
using RESOURCE.UnderWriting.NewBussiness;
using System.Globalization;

namespace WEB.NewBusiness.NewBusiness.UserControls.AddNewClient.HealthDeclaration
{
    public partial class HealthDeclarationContainer : UC, IUC
    {
        public void ClearData() { }
        public void FillData() { }
        protected void pnHealthDeclaration_PreRender(object sender, EventArgs e) { }

        public int Contact_Id
        {
            get
            {
                return (int)ViewState["Contact_Id"];
            }
            set
            {
                ViewState["Contact_Id"] = value;
            }
        }

        public int ContactRoleTypeId
        {
            get
            {
                return (int)ViewState["ContactRoleTypeId"];
            }
            set
            {
                ViewState["ContactRoleTypeId"] = value;
            }
        }

        public void ChangeColumnSizeQuestionaire()
        {
            if (!ObjServices.IsDataReviewMode)
                gvHealthDeclaration.RepeatColumns = 3;
            else
                if (ObjServices.IsDataReviewMode && getisView)
                    gvHealthDeclaration.RepeatColumns = 3;
                else
                    if (ObjServices.IsDataReviewMode)
                        gvHealthDeclaration.RepeatColumns = 1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ChangeColumnSizeQuestionaire();
        }

        //Corp_Id	Questionnaire_Id	Questionnaire_Desc
        //1	1	health-declaration
        //1	2	additional-questionnarie
        protected void RedirectTab(object sender, EventArgs e)
        {
            var Tab = ((LinkButton)sender).ClientID;
            btnSave_Click(null, null);
            hdnCurrentTabHealth.Value = Tab;
            SelectContact();
        }

        public DataRow SetTempleteRow(ref DataTable dt, int Contact_Id, int Contact_Role_Type_Id, int Questionnaire_Id, int Questionnaire_Seq, int Question_Id, int Option_Id
            , string Textual_Answer, DateTime? Date_Answer = null)
        {
            var r = dt.NewRow();
            r["Corp_Id"] = ObjServices.Corp_Id;
            r["Region_Id"] = ObjServices.Region_Id;
            r["Country_Id"] = ObjServices.Country_Id;
            r["Domesticreg_Id"] = ObjServices.Domesticreg_Id;
            r["State_Prov_Id"] = ObjServices.State_Prov_Id;
            r["City_Id"] = ObjServices.City_Id;
            r["Office_Id"] = ObjServices.Office_Id;
            r["Case_Seq_No"] = ObjServices.Case_Seq_No;
            r["Hist_Seq_No"] = ObjServices.Hist_Seq_No;

            r["Create_Date"] = DateTime.Now;
            r["Modi_Date"] = DateTime.Now;
            r["Create_UsrId"] = ObjServices.UserID.Value;
            r["Modi_UsrId"] = ObjServices.UserID.Value;
            r["HostName"] = "";
            r["Language_Id"] = ObjServices.getCurrentLanguage();
            r["Answer_Status"] = 1;


            r["Contact_Id"] = Contact_Id;
            r["Contact_Role_Type_Id"] = Contact_Role_Type_Id;

            r["Questionnaire_Id"] = Questionnaire_Id;
            r["Questionnaire_Seq"] = Questionnaire_Seq;
            r["Question_Id"] = Question_Id;
            r["Option_Id"] = Option_Id;
            r["Textual_Answer"] = Textual_Answer;
            if (Date_Answer != null)
                r["Date_Answer"] = Date_Answer;


            dt.Rows.Add(r);


            return r;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            save();
            FillData(hdnCurrentTabHealth.Value);
            SelectContact();
            if (sender == btnSave)
            {
                this.MessageBox(Resources.DataInsertedSucessfully, Title: ObjServices.Language == Utility.Language.en ? "INFORMATION" : "INFORMACIÓN");
            }                                                         
        }

        protected void gvHealthDeclaration_ItemCreated(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Questionnaire.Option dt = new Questionnaire.Option();
                dt.CorpId = ObjServices.Corp_Id;
                dt.RegionId = ObjServices.Region_Id;
                dt.CountryId = ObjServices.Country_Id;
                dt.DomesticregId = ObjServices.Domesticreg_Id;
                dt.StateProvId = ObjServices.State_Prov_Id;
                dt.CityId = ObjServices.City_Id;
                dt.OfficeId = ObjServices.Office_Id;
                dt.CaseSeqNo = ObjServices.Case_Seq_No;
                dt.HistSeqNo = ObjServices.Hist_Seq_No;

                /*esto depende*/
                dt.ContactId = this.Contact_Id;
                dt.ContactRoleTypeId = this.ContactRoleTypeId;


                dt.LanguageId = ObjServices.getCurrentLanguage();
                dt.QuestionId = int.Parse(gvHealthDeclaration.DataKeys[e.Item.ItemIndex].ToString().Split('|')[1]);
                dt.QuestionnaireId = int.Parse(gvHealthDeclaration.DataKeys[e.Item.ItemIndex].ToString().Split('|')[0]);
                dt.ParentOptionId = "0"; // esto son los que pertenece de la pregunta 

                RadioButtonList Respuesta = (RadioButtonList)e.Item.FindControl("rbRespuesta");

                var datos = ObjServices.oHealthDeclarationManager.GetAllQuestionOption(dt);

                Respuesta.Items.Clear();

                var rbRespuestaID = "Check" + gvHealthDeclaration.DataKeys[e.Item.ItemIndex].ToString().Split('|')[1];
                Respuesta.ID = rbRespuestaID;
                foreach (var item in datos)
                {
                    var itemLabel = new ListItem(item.OptionLabel, item.OptionKeyName.ToString() + "|" + item.OptionId.ToString());
                    itemLabel.Attributes.Add("for", rbRespuestaID);
                    var index = int.Parse(gvHealthDeclaration.DataKeys[e.Item.ItemIndex].ToString().Split('|')[2]);
                    itemLabel.Attributes.Add("onclick", "hideOrShow(" + index + ",this);");

                    if (item.HasAnswer)
                        itemLabel.Selected = true;
                    Respuesta.Items.Add(itemLabel);
                }

                UCQuestionSelection Requirements = (UCQuestionSelection)e.Item.FindControl("UCQuestionSelection");
                Requirements.Page = this.Page;

                dt.ParentOptionId = null; // esto son los que depende de la pregunta 
                dt.SubOption = false;
                Requirements.Controls.Clear();
                Requirements.Visible = true;
                Requirements.fillData(dt);
            }
        }

        protected void rbRespuesta_SelectedIndexChanged(object sender, EventArgs e)
        {

            UCQuestionSelection Requirements = (UCQuestionSelection)((RadioButtonList)sender).NamingContainer.FindControl("UCQuestionSelection");
            Requirements.Page = this.Page;

            if (((RadioButtonList)sender).SelectedItem.Value.Split('|')[0] == "NO")
            {
                Requirements.Controls.Clear();
                Requirements.Visible = false;
            }
            else
            {
                DataListItem d = (DataListItem)((RadioButtonList)sender).NamingContainer;

                Questionnaire.Option dt = new Questionnaire.Option();
                dt.CorpId = ObjServices.Corp_Id;
                dt.RegionId = ObjServices.Region_Id;
                dt.CountryId = ObjServices.Country_Id;
                dt.DomesticregId = ObjServices.Domesticreg_Id;
                dt.StateProvId = ObjServices.State_Prov_Id;
                dt.CityId = ObjServices.City_Id;
                dt.OfficeId = ObjServices.Office_Id;
                dt.CaseSeqNo = ObjServices.Case_Seq_No;
                dt.HistSeqNo = ObjServices.Hist_Seq_No;
                /*esto depende*/
                dt.ContactId = this.Contact_Id;
                dt.ContactRoleTypeId = this.ContactRoleTypeId;

                dt.SubOption = false;
                dt.LanguageId = ObjServices.getCurrentLanguage();
                dt.QuestionId = int.Parse(gvHealthDeclaration.DataKeys[d.ItemIndex].ToString().Split('|')[1]);
                dt.QuestionnaireId = int.Parse(gvHealthDeclaration.DataKeys[d.ItemIndex].ToString().Split('|')[0]);
                Requirements.Visible = true;
                Requirements.fillData(dt);
            }


        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Translator("");
            if (!(ObjServices.ProductLine == Utility.ProductLine.HealthInsurance))
                liInsuranceIformationPrevious.Attributes.Add("style", "display:none");

        }

        public void Translator(string Lang)
        {
            string dosPuntos = ":";
            PolicyNo.InnerHtml = Resources.PolicyNoLabel;
            InsuredType.InnerHtml = Resources.InsuredType;
            FirstName.InnerHtml = Resources.FirstNameLabel;
            MiddleName.InnerHtml = Resources.MiddleNameLabel;
            LastName.InnerHtml = Resources.LastNameLabel;
            SecondLastName.InnerHtml = Resources.SecondLastNameLabel;
            Age.InnerHtml = Resources.AgeLabel.ToUpper() + dosPuntos;
            Gender.InnerHtml = Resources.GenderLabel.ToUpper() + dosPuntos;
            Smoker.InnerHtml = Resources.SmokerLabel.ToUpper() + dosPuntos;
            WEIGHTTYPE.InnerHtml = Resources.WEIGHTTYPE + dosPuntos;
            WEIGHT.InnerHtml = Resources.WEIGHT + dosPuntos;
            HEIGHTTYPE.InnerHtml = Resources.HEIGHTTYPE + dosPuntos;
            HEIGHT.InnerHtml = Resources.HEIGTH + dosPuntos;
            DRNAME.InnerHtml = Resources.DRNAME + dosPuntos;

            //2016-02-18 | Marcos J. Perez
            //ADDRESS.InnerHtml = Resources.AddressLabel.ToUpper() + dosPuntos;
            ADDRESS.InnerHtml = Resources.OfficeAddressLabel.ToUpper() + dosPuntos;
            lnkInformacionSeguroAnteriorActual.Text = Resources.InsuranceInformationPreviousCurrent.ToUpper();
            lblMDCountry.InnerHtml = Resources.CountryLabel.ToUpper();
            lblMDCity.InnerHtml = Resources.CityLabel.ToUpper();

            //2016-02-19 | Marcos J. Perez
            lblMDStateProvince.InnerHtml = Resources.StateProvinceLabel.ToUpper();

            PHONENUMBER.InnerHtml = Resources.PhoneNumberLabel.ToUpper() + dosPuntos;
            LASTMEDICALVISIT.InnerHtml = Resources.LASTMEDICALVISIT + dosPuntos;
            //REASON.InnerHtml = Resources.REASON.ToUpper() + dosPuntos;
            RESULT.InnerHtml = Resources.RESULT + dosPuntos;
            ltHealtDeclaration.Text = Resources.HealthDeclarationLabel;
            ltAdditionalQuestionarie.Text = Resources.ADDITIONALQUESTIONNAIRE;
            btnSave.Text = Resources.Save;
            lblBloodType.InnerText = Resources.BloodType.ToUpper() + dosPuntos;
            var hdnLang = this.Page.Master.FindControl("STFCUserProfile1").FindControl("hdnLang") as System.Web.UI.WebControls.HiddenField;

            if (ObjServices.isChangingLang)
            {
                FillData(hdnCurrentTabHealth.Value);
                FillDrop();
            }
        }

        public void save()
        {
            var dt = ObjServices.oHealthDeclarationManager.GetQuestionAnswerTemplate();
            int Questionnaire_Id = 0;

            for (int i = 0; i <= gvHealthDeclaration.Items.Count - 1; i++)
            {
                var UCQuestionSelection = (UCQuestionSelection)gvHealthDeclaration.Items[i].FindControl("UCQuestionSelection");
                UCQuestionSelection.Page = this.Page;

                int Question_Id;
                int Questionnaire_Seq = -1;


                /*esto depende*/
                int Contact_Id = this.Contact_Id;
                int Contact_Role_Type_Id = this.ContactRoleTypeId;

                int Option_Id = -1;

                Question_Id = int.Parse(gvHealthDeclaration.DataKeys[i].ToString().Split('|')[1]);
                Questionnaire_Id = int.Parse(gvHealthDeclaration.DataKeys[i].ToString().Split('|')[0]);
                /*aqui contesto la respuesta de las preguntas*/
                var RadioQue = "Check" + Question_Id.ToString();
                var Respuesta = (RadioButtonList)gvHealthDeclaration.Items[i].FindControl(RadioQue);
                string op = "NO";
                if (Respuesta.SelectedItem != null)
                {
                    op = Respuesta.SelectedItem.Value.Split('|')[0];
                    if (Respuesta.SelectedItem != null)
                        Option_Id = int.Parse(Respuesta.SelectedItem.Value.Split('|')[1]);

                    if (Option_Id != -1)
                        SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, new Nullable<DateTime>());
                }


                if (op == "SI")
                {
                    foreach (var item in UCQuestionSelection.Controls)
                    {
                        string Textual_Answer = null;
                        DateTime? Date_Answer = null;

                        if (item is UCCheckBox)
                        {
                            var a = "";
                        }
                        else if (item is UCDropdown)
                        {
                            var cbs = ((UCDropdown)item).Value;
                            Option_Id = int.Parse(cbs.SelectedItem.Value);
                            SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, new Nullable<DateTime>());

                        }
                        else if (item is UCUCDropdownCheckBox)
                        {
                            Saplin.Controls.DropDownCheckBoxes cbs = ((UCUCDropdownCheckBox)item).ValueDropDawn;
                            for (int rows = 0; rows < cbs.Items.Count; rows++)
                            {
                                if (cbs.Items[rows].Selected)
                                {
                                    Option_Id = int.Parse(cbs.Items[rows].Value);
                                    SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, new Nullable<DateTime>());
                                }
                            }
                        }

                        else if (item is UCUCDropdownCheckWithCheckBox)
                        {

                            if (((UCUCDropdownCheckWithCheckBox)item).ValueCheckBox.Checked)
                            {

                                Option_Id = ((UCUCDropdownCheckWithCheckBox)item).OptionId;
                                if (Option_Id != -1)
                                    SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, new Nullable<DateTime>());

                                Saplin.Controls.DropDownCheckBoxes cbs = ((UCUCDropdownCheckWithCheckBox)item).ValueDropDawn;
                                for (int rows = 0; rows < cbs.Items.Count; rows++)
                                {
                                    if (cbs.Items[rows].Selected)
                                    {
                                        Option_Id = int.Parse(cbs.Items[rows].Value);
                                        SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, new Nullable<DateTime>());
                                    }
                                }
                            }
                        }

                        else if (item is UCCheckBoxList)
                        {
                            var cbs = ((UCCheckBoxList)item).Value;
                            for (int rows = 0; rows < cbs.Items.Count; rows++)
                            {
                                if (cbs.Items[rows].Selected)
                                {
                                    Option_Id = int.Parse(cbs.Items[rows].Value);
                                    SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, new Nullable<DateTime>());
                                }
                            }

                        }
                        else if (item is UCCheckBoxListWithQuestion)
                        {
                            var cbs = ((UCCheckBoxListWithQuestion)item).Value;

                            if (cbs.SelectedItem != null)
                            {
                                Option_Id = int.Parse(cbs.SelectedItem.Value);
                                if (Option_Id != -1)
                                    SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, new Nullable<DateTime>());

                            }
                        }
                        else if (item is UCRadioButtonList)
                        {
                            var cbs = ((UCRadioButtonList)item).Value;

                            if (cbs.SelectedItem != null)
                            {
                                Option_Id = int.Parse(cbs.SelectedItem.Value);
                                if (Option_Id != -1)
                                    SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, new Nullable<DateTime>());

                            }
                        }
                        else if (item is UCRadioButton)
                        {
                            var a = "";
                        }
                        else if (item is UCCheckBoxWithDropdawn)
                        {
                            var cbs = ((UCCheckBoxWithDropdawn)item).ValueCheckBox;
                            var drop = ((UCCheckBoxWithDropdawn)item).ValueDropDawn;

                            if (cbs.Checked)
                            {
                                Option_Id = ((UCCheckBoxWithDropdawn)item).OptionId;
                                SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, new Nullable<DateTime>());

                                Option_Id = int.Parse(drop.SelectedItem.Value);
                                SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, new Nullable<DateTime>());
                            }

                        }
                        else if (item is UCTextbox)
                        {
                            var cbs = ((UCTextbox)item).Value;
                            Option_Id = ((UCTextbox)item).OptionId;
                            if (Option_Id != -1)
                                SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, cbs.Text, new Nullable<DateTime>());
                        }
                        else if (item is UCTextboxNumeric)
                        {
                            var cbs = ((UCTextboxNumeric)item).Value;
                            Option_Id = ((UCTextboxNumeric)item).OptionId;
                            if (Option_Id != -1)
                                SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, cbs.Text, new Nullable<DateTime>());
                        }
                        else if (item is UCTextboxDates)
                        {
                            var cbs = ((UCTextboxDates)item).Value;
                            Option_Id = ((UCTextboxDates)item).OptionId;

                            var fecha = Utility.IsDateReturnNull(cbs.Text);

                            if (Option_Id != -1)
                                SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, fecha);
                        }
                        else if (item is UCCheckBoxWithText)
                        {
                            if (((UCCheckBoxWithText)item).ValueCheckBox.Checked)
                            {
                                var cbs = ((UCCheckBoxWithText)item).Value_TXT;
                                Option_Id = ((UCCheckBoxWithText)item).OptionId_TXT;
                                var fecha = Utility.IsDateReturnNull(cbs.Text);
                                if (Option_Id != -1)
                                    SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, fecha);

                                Option_Id = ((UCCheckBoxWithText)item).OptionId;
                                if (Option_Id != -1)
                                    SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, null, new Nullable<DateTime>());

                            }

                        }
                        else if (item is UCTextboxComments)
                        {
                            var cbs = ((UCTextboxComments)item).Value;
                            Option_Id = ((UCTextboxComments)item).OptionId;
                            if (Option_Id != -1)
                                SetTempleteRow(ref dt, Contact_Id, Contact_Role_Type_Id, Questionnaire_Id, Questionnaire_Seq, Question_Id, Option_Id, cbs.Text, new Nullable<DateTime>());
                        }

                    }
                }
                else //ESTO SOLO FUNCIONA PARA EL GRID
                {
                    foreach (var item in UCQuestionSelection.Controls)
                        if (item is UCGridView)
                            ((UCGridView)item).DELETE();
                }
            }

            if (dt.Rows.Count >= 1)
            {
                ObjServices.oHealthDeclarationManager.SetQuestionAnswer(dt);
                var family = Utility.deserializeJSON<Utility.PolicyContactByRole>(ddlInsuredType.SelectedValue);

                if (hdnCurrentTabHealth.Value != "lnkAdditionalQuestionarieTab")
                {
                    var contact = ObjServices.oContactManager.GetContact
                                  (
                                    family.CorpId
                                    , family.RegionId
                                    , family.CountryId
                                    , family.DomesticregId
                                    , family.StateProvId
                                    , family.CityId
                                    , family.OfficeId
                                    , family.CaseSeqNo
                                    , family.HistSeqNo
                                    , family.ContactId
                                    , family.ContactRoleTypeId
                                    , languageId: ObjServices.getCurrentLanguage()
                                  );

                    string sex = "B";

                    if (contact != null)
                    {
                        sex = contact.Gender;
                        contact.Height = txtHEIGHT.Text;
                        contact.Weight = Utility.IsDecimalReturnNull(txtWEIGHT.Text.Replace(",", ""));

                        if (ddlHEIGHT.SelectedValue != "-1")
                            contact.HeightTypeId = Utility.IsIntReturnNull(ddlHEIGHT.SelectedItem.Value);

                        if (ddlWEIGHT.SelectedValue != "-1")
                            contact.WeightTypeId = Utility.IsIntReturnNull(ddlWEIGHT.SelectedItem.Value);

                        if (ddlBloodType.SelectedValue != "-1")
                            contact.BloodTypeId = Utility.IsIntReturnNull(ddlBloodType.SelectedItem.Value);


                        ObjServices.oContactManager.UpdateContact(contact);

                        var item = new Questionnaire()
                        {
                            CorpId = ObjServices.Corp_Id,
                            RegionId = ObjServices.Region_Id,
                            CountryId = ObjServices.Country_Id,
                            DomesticregId = ObjServices.Domesticreg_Id,
                            StateProvId = ObjServices.State_Prov_Id,
                            CityId = ObjServices.City_Id,
                            OfficeId = ObjServices.Office_Id,
                            CaseSeqNo = ObjServices.Case_Seq_No,
                            HistSeqNo = ObjServices.Hist_Seq_No,
                            ContactId = family.ContactId,
                            ContactRoleTypeId = family.ContactRoleTypeId,
                            LanguageId = ObjServices.getCurrentLanguage(),
                            QuestionnaireId = Questionnaire_Id,
                            QuestionnaireSeq = 1,
                            UserId = ObjServices.UserID.Value,
                            LastMedicalVisit = Utility.IsDateReturnNull(txtVISIT.Text),

                            //2016-02-19 | Marcos J. Perez
                            MDCorpId = ObjServices.Corp_Id,
                            MDRegionId = ObjServices.Region_Id,
                            //MDCountryId = Convert.ToInt32(ddlMDCountry.SelectedValue),
                            MDDomesticregId = ObjServices.Domesticreg_Id,
                            //Bmarroquin 25-01-2017 cambio como parte de la tropicalizacion en ESA, al no ser requeridos los campos de pais, departamento y ciudad del doctor hay q validarlos !!!
                            MDCountryId = ddlMDCountry.Items.Count > 0 ? ddlMDCountry.SelectedValue != "-1" ? Convert.ToInt32(ddlMDCountry.SelectedValue):(int?)null:(int?)null,
                            MDStateProvId = ddlMDStateProvince.Items.Count > 0 ? ddlMDStateProvince.SelectedValue!="-1"? Utility.deserializeJSON<Utility.StateProvince>(ddlMDStateProvince.SelectedValue).StateProvId:(int?)null : (int?)null,                         
                            MDCityId = ddlMDCity.Items.Count > 0 ? ddlMDCity.SelectedValue != "-1" ? Convert.ToInt32(ddlMDCity.SelectedValue):(int?)null : (int?)null,
                            //****  Fin Cambio Bmarroquin 25-01-2017

                            MDAddress = txtADDRESSES.Text,
                            MDName = txtNAME.Text,
                            MDPhoneNumber = txtPHONE1.Text + "-" + txtPHONE2.Text + "-" + txtPHONE3.Text,
                            // item.Reason = txtREASON.Text;
                            Result = txtRESULT.Text
                        };
                        ObjServices.oHealthDeclarationManager.UpdateQuestionnaire(item);
                    }
                }
            }

            Session["isValidQuestionaries"] = ObjServices.validateQuestionaries(ddlInsuredType);

            // this.ExcecuteJScript("$('#btnDummy').click()");
        }

        public void edit()
        {

        }

        private void SelectContact()
        {
            Utility.ClearAll(PNDetalle.Controls);

            var family = Utility.deserializeJSON<Utility.PolicyContactByRole>(ddlInsuredType.SelectedValue);
            var contact = ObjServices.oContactManager.GetContact
              (
                    family.CorpId
                  , family.RegionId
                  , family.CountryId
                  , family.DomesticregId
                  , family.StateProvId
                  , family.CityId
                  , family.OfficeId
                  , family.CaseSeqNo
                  , family.HistSeqNo
                  , family.ContactId
                  , family.ContactRoleTypeId
                  , languageId: ObjServices.getCurrentLanguage()
              );

            if (contact != null)
            {
                txtFirstName.Text = contact.FirstName;
                txtMiddleName.Text = contact.MiddleName;
                txtLastName.Text = contact.FirstLastName;
                txtSecondLastName.Text = contact.SecondLastName;
                Contact_Id = family.ContactId;
                ContactRoleTypeId = family.ContactRoleTypeId;
                txtPolicyNumber.Text = ObjServices.Policy_Id;

                txtWEIGHT.Text = contact.Weight.HasValue ? contact.Weight.Value.ToString(NumberFormatInfo.InvariantInfo) : "0.00";

                if (contact.WeightTypeId.HasValue)
                    ddlWEIGHT.SelectIndexByValue(contact.WeightTypeId.ToString());

                txtHEIGHT.Text = !contact.Height.SIsNullOrEmpty() ? contact.Height : string.Empty;

                if (contact.HeightTypeId.HasValue)
                {
                    ddlHEIGHT.SelectIndexByValue(contact.HeightTypeId.ToString());
                    SetAttributesdlHEIGHT(ddlHEIGHT);
                }


                txtAGE.Text = contact.Age.HasValue ? contact.Age.Value.ToString(NumberFormatInfo.InvariantInfo) : string.Empty;
                txtGENDER.Text = string.IsNullOrEmpty(contact.Gender) == false ? (contact.Gender.ToUpper() == "F" ? Resources.Female : Resources.Male) : string.Empty;
                txtSMOKER.Text = contact.Smoker.HasValue ? (contact.Smoker.Value == true ? Resources.YesLabel : "NO") : "N/A";
                ddlBloodType.SelectIndexByValue(contact.BloodTypeId.HasValue ? contact.BloodTypeId.Value.ToString() : "-1");

                FillData(hdnCurrentTabHealth.Value);
                Entity.UnderWriting.Entities.Questionnaire item = new Questionnaire();
                item.CorpId = ObjServices.Corp_Id;
                item.RegionId = ObjServices.Region_Id;
                item.CountryId = ObjServices.Country_Id;
                item.DomesticregId = ObjServices.Domesticreg_Id;
                item.StateProvId = ObjServices.State_Prov_Id;
                item.CityId = ObjServices.City_Id;
                item.OfficeId = ObjServices.Office_Id;
                item.CaseSeqNo = ObjServices.Case_Seq_No;
                item.HistSeqNo = ObjServices.Hist_Seq_No;
                item.ContactId = family.ContactId;
                item.ContactRoleTypeId = family.ContactRoleTypeId;
                item.LanguageId = ObjServices.getCurrentLanguage();
                item.QuestionnaireId = int.Parse(gvHealthDeclaration.DataKeys[0].ToString().Split('|')[0]);
                item.QuestionnaireSeq = 1;
                item.UserId = ObjServices.UserID.Value;

                item = ObjServices.oHealthDeclarationManager.GetQuestionnaire(item);

                if (!item.isNullReferenceObject())
                {
                    //OTROS VALORES                    
                    txtNAME.Text = item.MDName;

                    //2016-02-19 | Marcos J. Perez
                    item.MDCorpId = ObjServices.Corp_Id;
                    item.MDRegionId = ObjServices.Region_Id;
                    item.MDDomesticregId = ObjServices.Domesticreg_Id;

                    int MDCountryId = item.MDCountryId.HasValue ? item.MDCountryId.Value : -1,
                        MDStateProvId = item.MDStateProvId.HasValue ? item.MDStateProvId.Value : -1;

                    string MDCityId = item.MDCityId.HasValue ? item.MDCityId.Value.ToString() : "-1";

                    ddlMDCountry.SelectIndexByValue(MDCountryId.ToString());
                    MDCountrySelectedIndexChanged(MDCountryId);

                    var dbState = new Utility.StateProvince()
                    {
                        CorpId = item.MDCorpId.Value,
                        CountryId = MDCountryId,
                        DomesticregId = item.MDDomesticregId.Value,
                        RegionId = item.MDRegionId.Value,
                        StateProvId = MDStateProvId
                    };
                    var x = Utility.serializeToJSON(dbState);
                    ddlMDStateProvince.SelectIndexByValueJSON(x);
                    MDStateProvinceSelectedIndexChanged(MDCountryId, MDStateProvId);

                    ddlMDCity.SelectIndexByValue(MDCityId);

                    txtADDRESSES.Text = item.MDAddress;
                    txtPHONE1.Text = string.IsNullOrEmpty(item.MDPhoneNumber) == false ? (item.MDPhoneNumber.Split('-').Length > 0 ? item.MDPhoneNumber.Split('-')[0] : string.Empty) : string.Empty;
                    txtPHONE2.Text = string.IsNullOrEmpty(item.MDPhoneNumber) == false ? (item.MDPhoneNumber.Split('-').Length > 1 ? item.MDPhoneNumber.Split('-')[1] : string.Empty) : string.Empty;
                    txtPHONE3.Text = string.IsNullOrEmpty(item.MDPhoneNumber) == false ? (item.MDPhoneNumber.Split('-').Length > 2 ? item.MDPhoneNumber.Split('-')[2] : string.Empty) : string.Empty;
                    txtVISIT.Text = item.LastMedicalVisit.HasValue ? item.LastMedicalVisit.Value.ToShortDateString() : "";
                    //  txtREASON.Text = item.Reason;
                    txtRESULT.Text = item.Result;
                }
            }
        }

        public void FillDrop()
        {

            ObjServices.GettingAllDropsJSON(
                ref ddlInsuredType, Utility.DropDownType.PolicyContactByRoleOnlyInsured, "CodeName"// "RoleDesc"
                  , corpId: ObjServices.Corp_Id
                  , regionId: ObjServices.Region_Id
                  , countryId: ObjServices.Country_Id
                  , domesticregId: ObjServices.Domesticreg_Id
                  , stateProvId: ObjServices.State_Prov_Id
                  , cityId: ObjServices.City_Id
                  , officeId: ObjServices.Office_Id
                  , caseSeqNo: ObjServices.Case_Seq_No
                  , histSeqNo: ObjServices.Hist_Seq_No
                  , GenerateItemSelect: false
                  );


            ObjServices.GettingAllDrops(ref ddlBloodType,
                                   Utility.DropDownType.BloodType,
                                   "BloodTypeDesc",
                                   "BloodTypeId",
                                   GenerateItemSelect: true
                                  );

            DropDown.Parameter parameter = new DropDown.Parameter
            {
                DropDownType = "ScaleType",
                CorpId = ObjServices.Corp_Id,
                LanguageId = ObjServices.Language.ToInt()
            };

            var datos = ObjServices.oDropDownManager.GetDropDownByType(parameter);
            ddlHEIGHT.DataSource = datos.Where(a => a.ScaleTypeId == 2);
            ddlHEIGHT.DataTextField = "ScaleDesc";
            ddlHEIGHT.DataValueField = "ScaleId";
            ddlHEIGHT.DataBind();
            ddlHEIGHT.Items.Insert(0, new ListItem { Text = "----", Value = "-1" });

            ddlWEIGHT.DataSource = datos.Where(a => a.ScaleTypeId == 1);
            ddlWEIGHT.DataTextField = "ScaleDesc";
            ddlWEIGHT.DataValueField = "ScaleId";
            ddlWEIGHT.DataBind();
            ddlWEIGHT.Items.Insert(0, new ListItem { Text = "----", Value = "-1" });


            //2016-02-18 | Marcos J. Perez
            ObjServices.GettingAllDrops(ref ddlMDCountry,
                                    Utility.DropDownType.Country,
                                    "GlobalCountryDesc",
                                    "CountryId",
                                    GenerateItemSelect: true
                                    );

            SelectContact();
        }

        protected void ddlInsuredType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectContact();
        }

        public void FillData(string Tab = "lnkHealthDeclarationTab")
        {
            PNMenuTabs.Visible = !ObjServices.esFunerario();

            if (ObjServices.esFunerario())
                Tab = "lnkHealthDeclarationTab";

            var family = Utility.deserializeJSON<Utility.PolicyContactByRole>(ddlInsuredType.SelectedValue);

            string Sex = "B";

            var contact = ObjServices.oContactManager.GetContact
              (
                    family.CorpId
                  , family.RegionId
                  , family.CountryId
                  , family.DomesticregId
                  , family.StateProvId
                  , family.CityId
                  , family.OfficeId
                  , family.CaseSeqNo
                  , family.HistSeqNo
                  , family.ContactId
                  , family.ContactRoleTypeId
                  , languageId: ObjServices.getCurrentLanguage()
              );

            if (!contact.isNullReferenceObject())
                Sex = contact.Gender;

            var currentLanguage = ObjServices.getCurrentLanguage();

            int? QuestionaireID = null;

            switch (Tab)
            {
                case "lnkHealthDeclarationTab":
                    if (!ObjServices.esFunerario())
                    {
                        QuestionaireID = 1;
                        PNDetalle.Visible = true;
                    }
                    else
                        QuestionaireID = 3;
                    break;
                case "lnkAdditionalQuestionarieTab":
                    QuestionaireID = 2;
                    PNDetalle.Visible = false;
                    break;
                case "lnkInformacionSeguroAnteriorActual":
                    QuestionaireID = 4;
                    PNDetalle.Visible = false;
                    break;
            }

            var dataQuestionaire = ObjServices.oHealthDeclarationManager.GetAllQuestion(ObjServices.Corp_Id, QuestionaireID.Value, currentLanguage, Sex);
            gvHealthDeclaration.DataSource = dataQuestionaire;
            gvHealthDeclaration.DataBind();

            hdnCurrentTabHealth.Value = Tab;
        }

        public void Initialize()
        {
            FillDrop();
            this.ExcecuteJScript("changeTabHealth();");
            ReadOnlyControls(ObjServices.IsReadOnly);
        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            Utility.ReadOnlyControls(PNDetalle.Controls, isReadOnly);
        }

        protected void gvHealthDeclaration_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var panel = e.Item.FindControl("pnQuestion");
                if (!panel.isNullReferenceControl())
                    Utility.EnableControlswithoutRecursion(panel.Controls, !ObjServices.IsReadOnly);
            }

        }

        private void SetAttributesdlHEIGHT(DropDownList drop, bool ClearTextBox = false)
        {
            if (drop.Items.Count > 0 && drop.SelectedValue == "4")
                txtHEIGHT.Attributes.Remove("decimal");
            else
                txtHEIGHT.Attributes.Add("decimal", "decimal3");

            if (ClearTextBox)
            {
                txtHEIGHT.Clear();
                txtHEIGHT.Focus();
            }
        }

        protected void ddlHEIGHT_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = sender as DropDownList;
            SetAttributesdlHEIGHT(drop, true);
        }

        //2016-02-18 | Marcos J. Perez
        //2016-02-22 | Actualiado por Carlos Lebron 
        protected void ddlMDCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMDCountry.SelectedValue!="-1")
               MDCountrySelectedIndexChanged(ddlMDCountry.ToInt());
            else
            {
                ddlMDStateProvince.Items.Clear();
                ddlMDCity.Items.Clear();
            }

        }

        //2016-02-19 | Marcos J. Perez
        protected void MDCountrySelectedIndexChanged(int countrySelected)
        {
            ObjServices.GettingAllDrops(ref ddlMDStateProvince,
                                        Utility.DropDownType.StateProvince,
                                       "StateProvDesc",
                                       "StateProvId",
                                        corpId: ObjServices.Corp_Id,
                                        countryId: countrySelected,
                                        domesticregId: ObjServices.Domesticreg_Id,
                                        GenerateItemSelect: true
                                       );
        }

        //2016-02-19 | Marcos J. Perez
        //2016-02-22 | Actualiado por Carlos Lebron
        protected void ddlMDStateProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMDStateProvince.SelectedValue != "-1")
                MDStateProvinceSelectedIndexChanged(ddlMDCountry.ToInt(),
                                                    Utility.deserializeJSON<Utility.StateProvince>(ddlMDStateProvince.SelectedValue).StateProvId);
            else
                ddlMDCity.Items.Clear();
        }

        //2016-02-19 | Marcos J. Perez
        protected void MDStateProvinceSelectedIndexChanged(int countrySelected, int stateproviceSelected)
        {
            ObjServices.GettingAllDrops(ref ddlMDCity,
                                        Utility.DropDownType.City,
                                        "CityDesc",
                                        "CityId",
                                        countryId: countrySelected,
                                        stateProvId: stateproviceSelected,
                                        domesticregId: ObjServices.Domesticreg_Id,
                                        GenerateItemSelect: true
                                       );
        }
    }
}