﻿using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.Contact.ContactInformation
{
    public partial class WUCInsuredPersonalIinformation : UC, IUC
    {
        public void ReadOnlyControls(bool isReadOnly) { }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                txtAge.Attributes.Add("readonly", "readonly");
            }
        }
        public void edit() { }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Translator("");
        }

        public void Translator(string Lang)
        {
            FirstName.InnerHtml = Resources.FirstNameLabel;
            MiddleName.InnerHtml = Resources.MiddleNameLabel;
            LastName.InnerHtml = Resources.LastNameLabel;
            SecondLastName.InnerHtml = Resources.SecondLastNameLabel;
            Gender.InnerHtml = Resources.GenderLabel;
            Smoker.InnerHtml = Resources.SmokerLabel;
            CountryofBirth.InnerHtml = Resources.CountryofBirthLabel;
            CountryofCitizenship.InnerHtml = Resources.CountryofCitizenshipLabel;
            CountryofResidence.InnerHtml = Resources.CountryOfResidenceLabel;
            DateofBirth.InnerHtml = Resources.DateofBirthLabel;
            Age.InnerHtml = Resources.AgeLabel;
            MaritalStatus.InnerHtml = Resources.MaritalStatusLabel;
            ContactPersonalInformation.InnerHtml = Resources.CONTACTPERSONALINFORMATIONLabel;

            if (isChangingLang)
                FillDrop();
        }

        public void SetDataAndUpdate(Entity.UnderWriting.Entities.Contact oContact)
        {
            if (oContact != null)
            {
                oContact.FirstName = txtFirtName.Text;
                oContact.MiddleName = txtMidleName.Text;
                oContact.FirstLastName = txtLastName.Text;
                oContact.SecondLastName = txt2ndLastName.Text;
                oContact.Gender = ddlGender.SelectedValue;
                oContact.Smoker = (ddlSmoker.SelectedValue == "1");
                oContact.MaritalStatId = ddlMaritalStatus.SelectedValue != "-1" ? ddlMaritalStatus.ToInt() : (int?)null;
                oContact.CountryOfBirthId = ddlCountryOfBirth.SelectedValue != "-1" ? ddlCountryOfBirth.ToInt() : (int?)null;
                oContact.CountryOfResidenceId = ddlCountryOfResidence.SelectedValue != "-1" ? ddlCountryOfResidence.ToInt() : (int?)null;
                oContact.Dob = !string.IsNullOrEmpty(txtDateOfBirth.Text) ? txtDateOfBirth.ToDateTime() : (DateTime?)null;
                oContact.Age = txtAge.ToInt();
                //Actualizar el contacto
                ObjServices.oContactManager.UpdateContact(oContact);

                //Borrar
                var ContactCitizenship = ObjServices.GetContactCitizenship();

                if (!ContactCitizenship.isNullReferenceObject())
                {
                    ObjServices.oContactManager.DeleteContactCitizenship(
                        new Entity.UnderWriting.Entities.Contact.Citizenship
                        {
                            ContactId = oContact.ContactId,
                            GlobalCountryId = ContactCitizenship.GlobalCountryId,
                            Status = false,
                            CreateUser = ObjServices.UserID.Value,
                            ModifyUser = ObjServices.UserID.Value
                        });
                }

                if (ddlCountryOfCitizenship.SelectedValue != "-1")
                {

                    ObjServices.oContactManager.InsertContactCitizenship(
                        new Entity.UnderWriting.Entities.Contact.Citizenship
                        {
                            ContactId = oContact.ContactId,
                            GlobalCountryId = ddlCountryOfCitizenship.ToInt(),
                            Status = true,
                            CreateUser = ObjServices.UserID.Value,
                            ModifyUser = ObjServices.UserID.Value
                        });
                }

            }
        }

        public void save()
        {
            int ContactID;

            if (!ObjServices.ContactEntityID.HasValue || ObjServices.ContactEntityID.Value <= 0)
            {
                ContactID = ObjServices.oContactManager.InsertContact(new Entity.UnderWriting.Entities.Contact()
                                                                      {
                                                                          CorpId = ObjServices.Corp_Id,
                                                                          AgentId = ObjServices.isUser ? default(int?) : ObjServices.Agent_Id.Value,
                                                                          ModifyUser = ObjServices.UserID.Value
                                                                      });
                ObjServices.ContactEntityID = ContactID;
            }
            else
                ContactID = ObjServices.ContactEntityID.Value;

            var oContact = ObjServices.GetContact(ContactID);
            SetDataAndUpdate(oContact);
        }

        public void FillDrop()
        {
            //Llenar el dropDpown de Generos
            ObjServices.GettingAllDrops(ref ddlGender,
                                    Utility.DropDownType.Gender,
                                    "GenderDesc",
                                    "GenderId",
                                    GenerateItemSelect: true
                                   );

            //Llenar el dropDpown de Smoker
            ObjServices.GettingAllDrops(ref ddlSmoker,
                                    Utility.DropDownType.Smoker,
                                    "SmokerDesc",
                                    "SmokerId",
                                    GenerateItemSelect: true
                                   );

            //Llenar el dropDpown de Country Birth
            ObjServices.GettingAllDrops(ref ddlCountryOfBirth,
                                    Utility.DropDownType.Country,
                                    "GlobalCountryDesc",
                                    "CountryId",
                                    GenerateItemSelect: true
                                   );

            //Llenar el dropDpown de Country Birth
            ObjServices.GettingAllDrops(ref ddlCountryOfResidence,
                                    Utility.DropDownType.CountryOfResidence,
                                    "GlobalCountryDesc",
                                    "CountryId",
                                    GenerateItemSelect: true
                                   );

            //Llenar el dropDpown de Country Citizenship
            ObjServices.GettingAllDrops(ref ddlCountryOfCitizenship,
                                    Utility.DropDownType.Country,
                                    "GlobalCountryDesc",
                                    "CountryId",
                                    GenerateItemSelect: true
                                   );

            //Llenar el dropDpown de  Marital status
             ObjServices.GettingAllDrops(ref ddlMaritalStatus,
                                    Utility.DropDownType.MaritalStatus,
                                    "MaritalStatusDesc", "MaritalStatId",
                                    GenerateItemSelect: true
                                   );

        }

        public void FillData()
        {
            Entity.UnderWriting.Entities.Contact oContact = null;

            oContact = ObjServices.GetContact(ObjServices.ContactEntityID.Value);

            if (oContact != null)
            {
                txtFirtName.Text = oContact.FirstName;
                txtMidleName.Text = oContact.MiddleName;
                txtLastName.Text = oContact.FirstLastName;
                txt2ndLastName.Text = oContact.SecondLastName;
                ddlGender.SelectIndexByValue((string.IsNullOrEmpty(oContact.Gender)) ? "-1" : oContact.Gender);
                ddlSmoker.SelectIndexByValue(!oContact.Smoker.HasValue ? "-1" : (oContact.Smoker.Value ? "1" : "0"));
                ddlCountryOfBirth.SelectIndexByValue(oContact.CountryOfBirthId.HasValue ? oContact.CountryOfBirthId.ToString() : "-1");
                txtDateOfBirth.Text = !oContact.Dob.HasValue ? "" : oContact.Dob.Value.ToString("MM/dd/yyyy");
                hdnAge.Value = !oContact.Age.HasValue ? "" : oContact.Age.Value.ToString();
                txtAge.Text = hdnAge.Value;

                ddlCountryOfResidence.SelectIndexByValue(oContact.CountryOfResidenceId.HasValue ? oContact.CountryOfResidenceId.Value.ToString() : "-1");

                ddlMaritalStatus.SelectIndexByValue(oContact.MaritalStatId.ToString());

                var ContactCitizenship = ObjServices.GetContactCitizenship();

                if (ContactCitizenship != null)
                    ddlCountryOfCitizenship.SelectIndexByValue(ContactCitizenship.GlobalCountryId.ToString());
            }
        }

        public void Initialize()
        {
            FillDrop();
            FillData();
        }

        public void ClearData()
        {
            Utility.ClearAll(this.Controls);
        }
    }
}