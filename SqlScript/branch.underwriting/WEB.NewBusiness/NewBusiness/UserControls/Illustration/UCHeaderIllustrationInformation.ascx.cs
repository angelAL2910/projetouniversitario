﻿using System;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using Entity.UnderWriting.Entities;
using RESOURCE.UnderWriting.NewBussiness;
using WEB.NewBusiness.Common;
using WEB.NewBusiness.NewBusiness.UserControls.Illustration.PlanInformation;

namespace WEB.NewBusiness.NewBusiness.UserControls
{
    public partial class UCHeaderIllustrationInformation : UC, IUC
    {
        #region Properties
        #region Private
        private UCPlanContainer UCIllustrationPlanContainer
        {
            get
            {
                return (UCPlanContainer)Page.Controls[0].FindControl("bodyContent").FindControl("UCIllustrationContainer").FindControl("UCPlanContainer");
            }
        }
        #endregion
        #region Public
        public string IllustrationNumber
        {
            get
            {
                return txtIllustrationNumber.Text;
            }
            set
            {
                txtIllustrationNumber.Text = value;
            }
        }

        public int CustomerAge
        {
            get
            {
                return txtAge.Text.ToInt();
            }
        }

        public string OwnerName
        {
            get
            {
                return txtClientSearch.Text.SIsNullOrEmpty() ? InsuredName : txtClientSearch.Text;
            }
        }

        public string InsuredName
        {
            get
            {
                return txtFirstName.Text.NTrim() + " " + txtLastName.Text.NTrim();
            }
        }
        #endregion
        #endregion
        #region Methods
        #region Private
        private void FillDropDown()
        {
            Utility.GettingAllDropsToIllus(ref ddlFamilyProduct, Utility.DropDownType.FamilyProduct, "PlanGroup", "PlanGroupCode", GenerateItemSelect: true, companyId: ObjServices.CompanyId, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlIllusGender, Utility.DropDownType.Gender, "GenderName", "GenderCode", GenerateItemSelect: true, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlIllusMaritalStatus, Utility.DropDownType.MaritalStatus, "MaritalStatus", "MaritalStatusCode", GenerateItemSelect: true, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlCountryResidence, Utility.DropDownType.Country, "CountryName", "CountryNo", GenerateItemSelect: true, pLang: ObjServices.Language);
            Utility.GettingAllDropsToIllus(ref ddlSmoker, Utility.DropDownType.Boolean, GenerateItemSelect: true, pLang: ObjServices.Language);
            ObjServices.GettingAllDropsJSON(ref ddlOffice,
                                  Utility.DropDownType.Office,
                                  "OfficeDesc",
                                  GenerateItemSelect: true,
                                  GenerateItemSelectText: Resources.Select
                                 );

        }

        private void SetContact(Entity.UnderWriting.Entities.Contact contact)
        {
            DateTime? dob = null;
            int? age = null;
            if (txtAge.Text.ToInt() > 0)
            {
                age = txtAge.Text.ToInt();
                dob = DateTime.Now.AddYears(-age.Value);
            }
            contact.FirstName = txtFirstName.Text;
            contact.MiddleName = txtMiddleName.Text;
            contact.FirstLastName = txtLastName.Text;
            contact.SecondLastName = txtSecondLastName.Text;
            if (contact.Dob == null)
                contact.Dob = dob;
            contact.Age = age;

            if (ddlIllusMaritalStatus.SelectedIndex > 0)
            {
                var maritalStatusIllus = Utility.GetIllusDropDownByType(Utility.DropDownType.MaritalStatus).FirstOrDefault(o => o.MaritalStatusCode == ddlIllusMaritalStatus.SelectedValue);
                var dropdownParameter = new DropDown.Parameter { LanguageId = 1 };
                var maritalStatusGlobal = ObjServices.GetDropDownByType(Utility.DropDownType.MaritalStatus, dropdownParameter).FirstOrDefault(o => o.MaritalStatusDesc.ToLower() == maritalStatusIllus.MaritalStatus.ToLower());
                contact.MaritalStatId = maritalStatusGlobal == null ? null : maritalStatusGlobal.MaritalStatId;
            }

            if (ddlIllusGender.SelectedIndex > 0)
            {
                var genderIllus = Utility.GetIllusDropDownByType(Utility.DropDownType.Gender).FirstOrDefault(o => o.GenderCode == ddlIllusGender.SelectedValue);
                var dropdownParameter = new DropDown.Parameter { LanguageId = 1 };
                var genderGlobal = ObjServices.GetDropDownByType(Utility.DropDownType.Gender, dropdownParameter).FirstOrDefault(o => o.GenderDesc.ToLower() == genderIllus.GenderName.ToLower());
                contact.Gender = genderGlobal == null ? null : genderGlobal.GenderId;
            }

            if (ddlCountryResidence.SelectedIndex > 0)
            {
                var countryIllus = Utility.GetIllusDropDownByType(Utility.DropDownType.Country).FirstOrDefault(o => o.CountryNo == ddlCountryResidence.SelectedValue.ToInt());
                var dropdownParameter = new DropDown.Parameter { LanguageId = 1 };
                var countryGlobal = ObjServices.GetDropDownByType(Utility.DropDownType.Country, dropdownParameter).FirstOrDefault(o => o.GlobalCountryDesc.ToLower() == countryIllus.CountryName.ToLower());
                contact.CountryOfResidenceId = countryGlobal == null ? null : countryGlobal.CountryId;
            }

            contact.Smoker = ddlSmoker.SelectedIndex > 0 ? (Boolean?)(ddlSmoker.SelectedValue == "1")
                                                         : null;

            if (!chkClientSearch.Checked)
                hdnClientName.Value = txtClientSearch.Text = "";
            else
                txtClientSearch.Text = hdnClientName.Value;
        }

        private void ChangeProductInformation()
        {
            if (ddlPlanInformation.SelectedIndex > 0)
                UCIllustrationPlanContainer.ChangePlan(ddlFamilyProduct.SelectedValue, ddlPlanInformation.SelectedValue, ddlPlanInformation.SelectedItem.Text);
        }
        #endregion
        #region Public
        public void ChangeFamilyProduct(int planIndex = 0)
        {
            Utility.GettingAllDropsToIllus(ref ddlPlanInformation, Utility.DropDownType.ProductType, "Product", "ProductCode", GenerateItemSelect: true, familyProductCode: ddlFamilyProduct.SelectedValue, companyId: ObjServices.CompanyId, pLang: ObjServices.Language);
            if (ddlPlanInformation.DataSource != null && ddlPlanInformation.Items.Count > 0)
            {
                ddlPlanInformation.SelectedIndex = planIndex;
                ChangeProductInformation();
            }
        }

        public void ReadOnlyControls(bool isReadOnly) { }

        public void Translator(string Lang)
        {
            gvContactList.TranslateColumnsAspxGrid();
            if (isChangingLang)
            {
                var familyProduct = ddlFamilyProduct.SelectedIndex;
                FillDropDown();
                ddlFamilyProduct.SelectedIndex = familyProduct;
                var planInformation = ddlPlanInformation.SelectedIndex;
                ChangeFamilyProduct(planInformation);
                ppcListClient.HeaderText = Resources.CONTACT;
            }

            ltPlanType.Text = ddlPlanInformation.SelectedItem == null ? "" : ddlPlanInformation.SelectedItem.Text;
        }

        public void FillData()
        {
            //ObjIllustrationServices.CustomerPlanNo = 25;
            ddlOffice.Enabled = ObjServices.UserType == Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.User;
            if (ObjIllustrationServices.CustomerPlanNo.GetValueOrDefault() > 0)
            {
                var customerPlanDetail = ObjIllustrationServices.oIllusDataManager.GetAllCustomerPlanDetail(new Entity.UnderWriting.IllusData.Illustrator.CustomerPlanDetailP
                {
                    CustomerPlanNo = ObjIllustrationServices.CustomerPlanNo.Value
                }).FirstOrDefault();
                if (customerPlanDetail == null)
                {
                    ddlFamilyProduct.Enabled =
                    ddlPlanInformation.Enabled = true;
                    CleanControls(this);
                    return;
                }
                #region Illustration Information
                txtIllustrationNumber.Text = customerPlanDetail.DispIllustrationNo;
                txtStatus.Text = ("Illustration_" + customerPlanDetail.IllustrationStatusCode).Translate();
                chkClientSearch.Checked = customerPlanDetail.IsCustomerOwner.Contains("Y");

                if (chkClientSearch.Checked && customerPlanDetail.OwnerCustomerNo.GetValueOrDefault() > 0)
                {
                    var ownerCustomerDetail = ObjIllustrationServices.oIllusDataManager.GetCustomerDetailById(customerPlanDetail.OwnerCustomerNo.GetValueOrDefault(), null);
                    if (ownerCustomerDetail != null)
                    {
                        hdnClientName.Value = txtClientSearch.Text = ownerCustomerDetail.FirstName + " " + ownerCustomerDetail.LastName;
                        UCIllustrationPlanContainer.OwnerId = ObjServices.Owner_Id = ownerCustomerDetail.RCustomerNo.ToInt();
                    }
                }
                else
                    hdnClientName.Value = txtClientSearch.Text = "";
                ObjIllustrationServices.CustomerNo = customerPlanDetail.CustomerNo;
                ddlFamilyProduct.SelectedValue = customerPlanDetail.PlanGroupCode;

                Utility.GettingAllDropsToIllus(ref ddlPlanInformation,
                                               Utility.DropDownType.ProductType,
                                               "Product",
                                               "ProductCode",
                                               GenerateItemSelect: false,
                                               familyProductCode: ddlFamilyProduct.SelectedValue,
                                               companyId: ObjServices.CompanyId,
                                               pLang: ObjServices.Language
                                               );

                ddlPlanInformation.SelectedValue = customerPlanDetail.ProductCode;
                ddlFamilyProduct.Enabled =
                ddlPlanInformation.Enabled =
                ddlOffice.Enabled = false;

                var office = new Utility.itemOfficceWithoutAgent
                {
                    CorpId = customerPlanDetail.CorpId,
                    RegionId = customerPlanDetail.RegionId,
                    CountryId = customerPlanDetail.CountryId,
                    DomesticregId = customerPlanDetail.DomesticregId,
                    StateProvId = customerPlanDetail.StateProvId,
                    CityId = customerPlanDetail.CityId,
                    OfficeId = customerPlanDetail.OfficeId
                };

                var jsonOffice = Utility.serializeToJSON(office);
                ddlOffice.SelectIndexByValueJSON(jsonOffice);
                #endregion
            }
            else if (ObjServices.UserType != Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.User)
            {
                var office = ObjServices.GetCurrentOfficeWithoutAgent();
                var jsonOffice = Utility.serializeToJSON(office);
                ddlOffice.SelectIndexByValueJSON(jsonOffice);
                var officeText = ddlOffice.SelectedItem.Text;
                var officeValue = ddlOffice.SelectedValue;

                ddlOffice.Items.Clear();
                ddlOffice.Items.Add(new ListItem
                {
                    Text = officeText,
                    Value = officeValue
                });
            }

            #region Customer Information
            if (ObjIllustrationServices.CustomerNo.GetValueOrDefault() > 0 || ObjServices.ContactEntityID.GetValueOrDefault() > 0)
            {
                Entity.UnderWriting.IllusData.Illustrator.CustomerDetail customerDetail;
                if (ObjIllustrationServices.CustomerNo.GetValueOrDefault() <= 0)
                {
                    customerDetail = ObjIllustrationServices.oIllusDataManager.GetCustomerDetailById(null, ObjServices.ContactEntityID.GetValueOrDefault());
                    //Si el contacto no existe en illusdata, lo inserta y vuelve a buscarlo
                    if (customerDetail == null)
                        if (ObjServices.SetCustomerDetailToIllusdata(ObjServices.Corp_Id, ObjServices.ContactEntityID.GetValueOrDefault(), ObjServices.CompanyId, ObjIllustrationServices.CustomerNo.GetValueOrDefault(), true, this))
                            customerDetail = ObjIllustrationServices.oIllusDataManager.GetCustomerDetailById(null, ObjServices.ContactEntityID.GetValueOrDefault());
                }
                else
                    customerDetail = ObjIllustrationServices.oIllusDataManager.GetCustomerDetailById(ObjIllustrationServices.CustomerNo.Value, null);

                if (customerDetail == null) return;

                if (ObjServices.ContactEntityID.GetValueOrDefault() <= 0)
                    ObjServices.ContactEntityID = customerDetail.RCustomerNo.ToInt();

                if (ObjIllustrationServices.CustomerNo.GetValueOrDefault() <= 0)
                    ObjIllustrationServices.CustomerNo = customerDetail.CustomerNo;

                txtFirstName.Text = customerDetail.FirstName;
                txtMiddleName.Text = customerDetail.MiddleName;
                txtLastName.Text = customerDetail.LastName;
                txtSecondLastName.Text = customerDetail.LastName2;
                ddlIllusGender.SelectIndexByValue(customerDetail.GenderCode, true);
                ddlIllusMaritalStatus.SelectIndexByValue(customerDetail.MaritalStatusCode, true);
                ddlCountryResidence.SelectIndexByValue(customerDetail.ResCountryNo.GetValueOrDefault().ToString(), true);
                ddlSmoker.SelectIndexByValue(customerDetail.Smoker == "Y" ? "1" : "0", true);
                txtAge.Text = customerDetail.Age;
            }
            else
                CleanControls(this);


            #endregion
        }

        public void save()
        {
            if (ObjServices.ContactEntityID.GetValueOrDefault() <= 0 || ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, ObjServices.ContactEntityID.GetValueOrDefault(), languageId: ObjServices.Language.ToInt()) == null)
            {
                var contact = new Entity.UnderWriting.Entities.Contact
                {
                    CorpId = ObjServices.Corp_Id,
                    ContactTypeId = 6, //Contact
                    ContactIdType = 6, //Contact
                    AgentId = ObjServices.UserType == Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.User ?
                    null : ObjServices.Agent_Id,
                    ModifyUser = ObjServices.UserID.GetValueOrDefault()
                };
                SetContact(contact);
                ObjServices.ContactEntityID = ObjServices.oContactManager.InsertContact(contact);
                //ObjServices.Country_Id = contact.CountryOfBirthId.HasValue ? contact.CountryOfBirthId.Value : 0;
                ObjServices.SetCustomerDetailToIllusdata(ObjServices.Corp_Id, ObjServices.ContactEntityID.GetValueOrDefault(), ObjServices.CompanyId, ObjIllustrationServices.CustomerNo.GetValueOrDefault(), false, this);
            }
            else
                edit();

            var customerDetail = ObjIllustrationServices.oIllusDataManager.GetCustomerDetailById(null, ObjServices.ContactEntityID.GetValueOrDefault());
            if (customerDetail != null)
                ObjIllustrationServices.CustomerNo = customerDetail.CustomerNo;

            ddlFamilyProduct.Enabled =
            ddlOffice.Enabled =
            ddlPlanInformation.Enabled = false;
        }

        public void edit()
        {
            var contact = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, ObjServices.ContactEntityID.GetValueOrDefault(), languageId: ObjServices.Language.ToInt());
            if (contact == null)
                save();
            else
            {
                SetContact(contact);
                ObjServices.oContactManager.UpdateContact(contact);
                ObjServices.SetCustomerDetailToIllusdata(ObjServices.Corp_Id, ObjServices.ContactEntityID.GetValueOrDefault(), ObjServices.CompanyId, ObjIllustrationServices.CustomerNo.GetValueOrDefault(), false, this);
            }
        }

        public void Initialize()
        {
            CleanControls(this);
            ddlFamilyProduct.Enabled =
            ddlOffice.Enabled =
            ddlPlanInformation.Enabled = true;
            FillDropDown();
            gvContactList.DataBind();
            gvContactList.FocusedRowIndex = -1;
        }

        public void ClearData()
        {
            throw new NotImplementedException();
        }

        public void ChangeStatusLabel(string statusLabel)
        {
            txtStatus.Text = statusLabel;
        }

        public void UpdatePanelUpdate()
        {
            //UpdatePanel1.Update();
            UpdatePanel2.Update();
        }

        public Utility.itemOfficceWithoutAgent GetOffice()
        {
            var officeJson = ddlOffice.SelectedValue;
            return Utility.deserializeJSON<Utility.itemOfficceWithoutAgent>(officeJson);
        }

        public string GetOfficeName()
        {
            return ddlOffice.SelectedItem.Text;
        }
        #endregion
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Translator(null);
        }

        protected void ddlFamilyProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFamilyProduct();
        }

        protected void ddlPlanInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeProductInformation();
        }

        protected void LinqDS_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            var data = ObjServices.GettingContactByAgent(ObjServices.Agent_Id.Value, Utility.ContactTypeId.Contact).OrderBy(x => x.FirstName).ToList();
            e.KeyExpression = "ContactId;FirstName;LastName";
            e.QueryableSource = data.AsQueryable();
        }

        protected void gvContactList_PageIndexChanged(object sender, EventArgs e)
        {
            gvContactList.FocusedRowIndex = -1;
        }

        protected void gvContactList_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "APPLYFILTER" || e.CallbackName == "APPLYCOLUMNFILTER" || e.CallbackName == "APPLYHEADERCOLUMNFILTER")
            {
                gvContactList.FocusedRowIndex = -1;
                gvContactList.SetFilterSettings();
            }
        }

        protected void UpdatePanel_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                  .Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {

            }

        }
        #endregion
    }
}