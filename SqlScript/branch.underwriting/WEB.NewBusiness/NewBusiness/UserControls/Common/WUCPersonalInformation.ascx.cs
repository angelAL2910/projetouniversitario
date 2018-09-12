﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;
using WEB.NewBusiness.NewBusiness.Pages;
using RESOURCE.UnderWriting.NewBussiness;
using System.Globalization;

namespace WEB.NewBusiness.NewBusiness.UserControls
{
    public partial class WUCPersonalInformation : UC, IUC
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public String PrefixSession33
        {
            get { return hdnCurrentSession.Value; }
            set { hdnCurrentSession.Value = value; }
        }
        /// <summary>
        ///   DataBinding de los DropDownList
        /// </summary>
        public void FillDrops()
        {
            //Llenar el dropDpown de Generos
            ObjServices.GettingAllDrops(ref ddl_WUC_PI_Gender,
                                    Utility.DropDownType.Gender,
                                    "GenderDesc",
                                    "GenderId",
                                    GenerateItemSelect: true
                                   );

            //Llenar el dropDpown de Smoker
            ObjServices.GettingAllDrops(ref ddl_WUC_PI_Smoker,
                                    Utility.DropDownType.Smoker,
                                    "SmokerDesc",
                                    "SmokerId",
                                    GenerateItemSelect: true
                                   );

            //Llenar el dropDpown de Country Birth
            ObjServices.GettingAllDrops(ref ddl_WUC_PI_CountryBirth,
                                    Utility.DropDownType.Country,
                                    "GlobalCountryDesc",
                                    "CountryId",
                                    GenerateItemSelect: true
                                   );

            //Llenar el dropDpown de Country Citizenship
            ObjServices.GettingAllDrops(ref ddl_WUC_PI_CountryCitizenship,
                                    Utility.DropDownType.Country,
                                    "GlobalCountryDesc",
                                    "CountryId",
                                    GenerateItemSelect: true
                                   );

            //Llenar el dropDpown de  Marital status
            ObjServices.GettingAllDrops(ref ddl_WUC_PI_MaritalStatus,
                                    Utility.DropDownType.MaritalStatus,
                                    "MaritalStatusDesc", "MaritalStatId",
                                    GenerateItemSelect: true
                                   );

            //Llenar el dropDpown de LengOfWork
            ObjServices.GettingAllDrops(ref ddl_WUC_PI_LengthWorkFrom,
                                    Utility.DropDownType.LengthatWork,
                                    GenerateItemSelect: true
                                   );

            //Llenar el dropDpown de Months 
            ObjServices.GettingAllDrops(ref ddl_WUC_PI_LengthWorkTo,
                                    Utility.DropDownType.Months,
                                    GenerateItemSelect: true
                                   );

            //Llenar el dropDpown de Country Of Residence
            ObjServices.GettingAllDrops(ref ddlCountryOfResidence,
                                    Utility.DropDownType.CountryOfResidence,
                                    "GlobalCountryDesc",
                                    "CountryId",
                                    GenerateItemSelect: true
                                   );

            //Llenar dropDown Customer BL 1
            ObjServices.GettingAllDrops(ref ddl_BusinessLine2,
                                      Utility.DropDownType.CustomerBusinessLine2,
                                      "GlobalCountryDesc",
                                      "CountryId",
                                    GenerateItemSelect: true
                                   );

            /*if (ddl_BusinessLine1.SelectedIndex == 0)
                ddl_BusinessLine2.Items.Clear();*/
            //Llenar dropDown Customer BL 2
            /*
            ObjServices.GettingAllDrops(ref ddl_BusinessLine2,
                                      Utility.DropDownType.CustomerBusinessLine,
                                      "GlobalCountryDesc",
                                      "CountryId",
                                    GenerateItemSelect: true
                                   );
            */
        }
        protected override void OnPreRender(EventArgs e)
        {
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
            CountryOfBirth.InnerHtml = Resources.CountryofBirthLabel;
            CountryOfCitizenship.InnerHtml = Resources.CountryofCitizenshipLabel;
            CountryofResidence.InnerHtml = Resources.CountryOfResidenceLabel;
            DateOfBirth.InnerHtml = Resources.DateofBirthLabel;
            Age.InnerHtml = Resources.AgeLabel;
            MaritalStatus.InnerHtml = Resources.MaritalStatusLabel;
            YearlyFamilyIncome.InnerHtml = Resources.YearlyFamilyIncomeLabel;
            YearlyPersonalIncome.InnerHtml = Resources.YearlyPersonalIncomeLabel;
            OccupationType.InnerHtml = Resources.OccupationTypeLabel;
            Occupation.InnerHtml = Resources.OccupationLabel;
            CompanyName.InnerHtml = Resources.CompanyNameLabel;
            LineOfBusiness1.InnerHtml = Resources.LineofBusinessLabel + " 1";
            LineOfBusiness2.InnerHtml = Resources.LineofBusinessLabel + " 2";
            TaskPerformed.InnerHtml = Resources.TasksPerformedLabel;
            YearsatWork.InnerHtml = Resources.YearsatWorkLabel;
            Months.InnerHtml = Resources.MonthsLabel;
            ltPersonalInformation.Text = Resources.PersonalInformationLabel;

            if (isChangingLang)
                FillDrops();

            if (!hdnOccupationGroupId.Value.SIsNullOrEmpty() && !hdnOccupationId.Value.SIsNullOrEmpty())
            {
                var data = ObjServices.GettingDropData(Utility.DropDownType.Occupation);

                if (data != null)
                {
                    var dataOccup = data.FirstOrDefault(y => y.OccupGroupTypeId == hdnOccupationGroupId.ToInt() && y.OccupationId == hdnOccupationId.ToInt());

                    if (dataOccup != null)
                    {
                        txtOccupation.Text = dataOccup.OccupationDesc;
                        txtProfession.Text = dataOccup.OccupationGroupDesc;
                    }
                }
            }
        }

        public void SetDataAndUpdate(Entity.UnderWriting.Entities.Contact oContact, Utility.ContactRoleIDType Role, bool UpdateRelationShip = false)
        {
            if (oContact != null)
            {
                //Setear los datos del contacto para actualizar
                //oContact.RelationshiptoAgent = Role == Utility.ContactRoleIDType.Client ? this.ObjServices.Relationship_With_Insured_Id :
                // this.ObjServices.Relationship_With_Owner_ToAgentId;

                if (!ObjServices.IsDataReviewMode)//Que no se actualice en DR, ya que arruina la relacion
                {
                    //Setear los datos del contacto para actualizar
                    oContact.RelationshiptoAgent = Role == Utility.ContactRoleIDType.Client ? this.ObjServices.Relationship_With_Insured_Id :
                                                                                              this.ObjServices.Relationship_With_Owner_ToAgentId;
                }

                oContact.FirstName = tb_WUC_PI_FirstName.Text;
                oContact.MiddleName = tb_WUC_PI_MiddleName.Text;
                oContact.FirstLastName = tb_WUC_PI_FirstLastName.Text;
                oContact.SecondLastName = tb_WUC_PI_SecondLastName.Text;
                oContact.Gender = ddl_WUC_PI_Gender.SelectedValue;
                oContact.Smoker = (ddl_WUC_PI_Smoker.SelectedValue == "1");
                oContact.CountryOfBirthId = ddl_WUC_PI_CountryBirth.ToInt();
                oContact.CountryOfResidenceId = ddlCountryOfResidence.ToInt();
                oContact.Dob = tb_WUC_PI_DateBirth.ToDateTime();
                oContact.Age = hdnAge.ToInt();
                oContact.MaritalStatId = ddl_WUC_PI_MaritalStatus.ToInt() == -1 ? DBNull.Value.ToInt() : ddl_WUC_PI_MaritalStatus.ToInt();
                oContact.AnnualPersonalIncome = tb_WUC_PI_PersonalIncome.ToDecimal();
                var familyIncome = tb_WUC_PI_YearLyFamilyIncome.ToDecimal();
                if (familyIncome > -1)
                {
                    oContact.AnnualFamilyIncome = familyIncome;
                }
                else
                {
                    oContact.AnnualFamilyIncome = 0.0M;
                }

                oContact.OccupGroupTypeId = !string.IsNullOrEmpty(hdnOccupationGroupId.Value) ? hdnOccupationGroupId.ToInt() : (int?)null;
                oContact.OccupationId = !string.IsNullOrEmpty(hdnOccupationId.Value) ? hdnOccupationId.ToInt() : (int?)null;
                oContact.CompanyName = tb_WUC_PI_CompanyName.Text;

                //Bmarroquin 05-03-2017 Correccion a Issue se pierde el Giro de Negocio 1, como no carga con un valor de "Select" el Index 1 ya esta ocupado con un valor seleccionable...
                //El Bug se daba cuando usuario dejaba el primer item del DropDownList como el que deseaba guardar
                if (ddl_BusinessLine1.Items.Count == 0 || ddl_BusinessLine1.SelectedItem.Text.Trim().Length == 0)
                {
                    oContact.LineOfBusiness = "";
                    //oContact.LineOfBusiness = (ddl_BusinessLine1.SelectedValue.ToString() == "-1" ? "" : ddl_BusinessLine1.SelectedItem.Text); //tb_WUC_PI_FirstLineBusinnes.Text;
                }
                else
                {
                    oContact.LineOfBusiness = ddl_BusinessLine1.SelectedItem.Text;
                }

                if (ddl_BusinessLine2 != null && ddl_BusinessLine2.Items != null && ddl_BusinessLine2.Items.Count > 0)
                {
                    oContact.LineOfBusiness2 = ddl_BusinessLine2.SelectedItem.Text; // (ddl_BusinessLine2.SelectedValue.ToString() == "-1" ? "" : ddl_BusinessLine2.SelectedItem.Text); //tb_WUC_PI_SecondLineBusiness.Text;
                }
                else
                {
                    oContact.LineOfBusiness2 = "";
                }
                oContact.LaborTasks = tb_WUC_PI_TaskPerformed.Text;
                oContact.LengthWorkYear = ddl_WUC_PI_LengthWorkFrom.ToInt();
                oContact.LengthWorkMonth = ddl_WUC_PI_LengthWorkTo.ToInt();

                var WUCAddress = this.Parent.FindControl("WUCAddress");
                var ddl_WUC_A_HomeState = (DropDownList)WUCAddress.FindControl("ddl_WUC_A_HomeState");
                var ddl_WUC_A_HomeCountry = (DropDownList)WUCAddress.FindControl("ddl_WUC_A_HomeCountry");
                var ddl_WUC_A_HomeCity = (DropDownList)WUCAddress.FindControl("ddl_WUC_A_HomeCity");

                if (ddl_WUC_A_HomeCountry.SelectedValue != "-1" &&
                       ddl_WUC_A_HomeState.SelectedValue != "-1" &&
                       (ddl_WUC_A_HomeCity.SelectedValue != "-1" && ddl_WUC_A_HomeCity.Items.Count > 0)
                    )
                {
                    var HomeState = Utility.deserializeJSON<Utility.StateProvince>(ddl_WUC_A_HomeState.SelectedValue);
                    var DocRegHome = HomeState.DomesticregId;
                    var StateHome = HomeState.StateProvId;
                    var RegionID = HomeState.RegionId;


                    oContact.RegionOfResidenceId = RegionID;
                    oContact.CountryOfResidenceId = (!string.IsNullOrEmpty(ddl_WUC_A_HomeCountry.SelectedValue) ? Convert.ToInt32(ddl_WUC_A_HomeCountry.SelectedValue) : 0);
                    oContact.DomesticRegOfResidenceId = DocRegHome;
                    oContact.StateOfResidenceId = StateHome;
                    oContact.CityOfResidenceId = int.Parse(ddl_WUC_A_HomeCity.SelectedValue);

                }



                if (UpdateRelationShip)
                {
                    Control WUCBackgroundInformation = null;

                    var bodyContent = this.Page.Master.FindControl("bodyContent");

                    WUCBackgroundInformation = (!ObjServices.IsDataReviewMode) ?
                                               bodyContent.FindControl("ContactsInfoContainer").FindControl("WUCBackgroundInformation")
                                             : bodyContent.FindControl("DReviewContainer").FindControl("WUCBackgroundInformation");

                    var RelationshiptoOwner = -1;

                    if (WUCBackgroundInformation != null)
                    {
                        var cbxRelationshipwithinsured = WUCBackgroundInformation.FindControl("cbxRelationshipwithinsured");
                        if (cbxRelationshipwithinsured != null)
                        {
                            if (!string.IsNullOrEmpty((cbxRelationshipwithinsured as DropDownList).SelectedValue))
                                RelationshiptoOwner = int.Parse((cbxRelationshipwithinsured as DropDownList).SelectedValue);
                        }
                    }

                    //Bmarroquin 26-01-2017 cambios a raiz de tropicalizacion, se agrega mejora para que no truene la pantalla al guardar un Owner diferente del Insured
                    oContact.RelationshiptoOwner = RelationshiptoOwner == -1 ? (int?)null : RelationshiptoOwner;
                }

                //Actualizar el contacto
                ObjServices.oContactManager.UpdateContact(oContact);

                //Borrar
                var ContactCitizenship = ObjServices.GetContactCitizenship();
                if (!ContactCitizenship.isNullReferenceObject())
                {
                    ObjServices.oContactManager.InsertContactCitizenship(
                        new Entity.UnderWriting.Entities.Contact.Citizenship
                        {
                            ContactId = oContact.ContactId,
                            GlobalCountryId = ContactCitizenship.GlobalCountryId,
                            Status = false,
                            CreateUser = ObjServices.UserID.Value,
                            ModifyUser = ObjServices.UserID.Value
                        });
                }

                if (ddl_WUC_PI_CountryCitizenship.SelectedValue != "-1")
                {
                    ObjServices.oContactManager.InsertContactCitizenship(
                        new Entity.UnderWriting.Entities.Contact.Citizenship
                        {
                            ContactId = oContact.ContactId,
                            GlobalCountryId = ddl_WUC_PI_CountryCitizenship.ToInt(),
                            Status = true,
                            CreateUser = ObjServices.UserID.Value,
                            ModifyUser = ObjServices.UserID.Value
                        });
                }
            }
        }

        public void SetDataAndUpdateLegal(Entity.UnderWriting.Entities.Contact oContact, Utility.ContactRoleIDType Role, bool UpdateRelationShip = false)
        {
            if (oContact != null)
            {
                //Setear los datos del contacto para actualizar

                if (!ObjServices.IsDataReviewMode)//Que no se actualice en DR, ya que arruina la relacion
                {
                    oContact.RelationshiptoAgent = this.ObjServices.Relationship_With_Owner_ToAgentId;
                    //oContact.RelationshiptoAgent = Role == Utility.ContactRoleIDType.Legal ? this.ObjServices.Relationship_With_Insured_Id :
                    //                                                                          this.ObjServices.Relationship_With_Owner_ToAgentId;
                }


                Decimal familyIncome = 0.0M;
                Control WUCPersonalInformationLegalInfo = null;

                var Container = (!ObjServices.IsDataReviewMode) ? "ContactsInfoContainer" : "DReviewContainer";
                WUCPersonalInformationLegalInfo = this.Page.Master.FindControl("bodyContent").FindControl(Container).FindControl("WUCPersonalInformationRepLegal");

                //Llenar las variables con los controles del formulario
                if (WUCPersonalInformationLegalInfo != null)
                {
                    oContact.FirstName = (WUCPersonalInformationLegalInfo.FindControl("tb_WUC_PI_FirstName_Legal") as TextBox).Text;
                    oContact.MiddleName = (WUCPersonalInformationLegalInfo.FindControl("tb_WUC_PI_MiddleName_Legal") as TextBox).Text;
                    oContact.FirstLastName = (WUCPersonalInformationLegalInfo.FindControl("tb_WUC_PI_FirstLastName_Legal") as TextBox).Text;
                    oContact.SecondLastName = (WUCPersonalInformationLegalInfo.FindControl("tb_WUC_PI_SecondLastName_Legal") as TextBox).Text;

                    oContact.Gender = (WUCPersonalInformationLegalInfo.FindControl("ddl_WUC_PI_Gender_Legal") as DropDownList).SelectedValue;
                    oContact.Smoker = (WUCPersonalInformationLegalInfo.FindControl("ddl_WUC_PI_Smoker_Legal") as DropDownList).SelectedValue == "1";
                    oContact.CountryOfBirthId = (WUCPersonalInformationLegalInfo.FindControl("ddl_WUC_PI_CountryBirth_Legal") as DropDownList).ToInt();


                    //oContact.Dob = (WUCPersonalInformationLegalInfo.FindControl("tb_WUC_PI_DateBirth_Legal") as TextBox).ToDateTime();
                    if ((WUCPersonalInformationLegalInfo.FindControl("tb_WUC_PI_DateBirth_Legal") as TextBox) != null && !string.IsNullOrEmpty((WUCPersonalInformationLegalInfo.FindControl("tb_WUC_PI_DateBirth_Legal") as TextBox).Text))
                    {
                        oContact.Dob = (WUCPersonalInformationLegalInfo.FindControl("tb_WUC_PI_DateBirth_Legal") as TextBox).ToDateTime();
                    }

                    oContact.Age = (WUCPersonalInformationLegalInfo.FindControl("hdnAge_Legal") as HiddenField).ToInt();

                    oContact.CountryOfResidenceId = (WUCPersonalInformationLegalInfo.FindControl("ddlCountryOfResidence_Legal") as DropDownList).ToInt();

                    oContact.MaritalStatId = (WUCPersonalInformationLegalInfo.FindControl("ddl_WUC_PI_MaritalStatus_Legal") as DropDownList).ToInt() == -1 ? DBNull.Value.ToInt() : (WUCPersonalInformationLegalInfo.FindControl("ddl_WUC_PI_MaritalStatus_Legal") as DropDownList).ToInt();
                    oContact.AnnualPersonalIncome = (WUCPersonalInformationLegalInfo.FindControl("tb_WUC_PI_PersonalIncome_Legal") as TextBox).Text.ToDecimal();
                    familyIncome = (WUCPersonalInformationLegalInfo.FindControl("tb_WUC_PI_YearLyFamilyIncome_Legal") as TextBox).Text.ToDecimal();
                    if (familyIncome > -1)
                    {
                        oContact.AnnualFamilyIncome = familyIncome;
                    }
                    else
                    {
                        oContact.AnnualFamilyIncome = 0.0M;
                    }

                    oContact.OccupGroupTypeId = !string.IsNullOrEmpty((WUCPersonalInformationLegalInfo.FindControl("hdnOccupationGroupId_Legal") as HiddenField).Value) ? (WUCPersonalInformationLegalInfo.FindControl("hdnOccupationGroupId_Legal") as HiddenField).ToInt() : (int?)null;
                    oContact.OccupationId = !string.IsNullOrEmpty((WUCPersonalInformationLegalInfo.FindControl("hdnOccupationId_Legal") as HiddenField).Value) ? (WUCPersonalInformationLegalInfo.FindControl("hdnOccupationId_Legal") as HiddenField).ToInt() : (int?)null;
                    oContact.CompanyName = (WUCPersonalInformationLegalInfo.FindControl("tb_WUC_PI_CompanyName_Legal") as TextBox).Text;

                    //codigo original
                    //if ((WUCPersonalInformationLegalInfo.FindControl("ddl_BusinessLine1_Legal") as DropDownList).SelectedIndex < 1 || (WUCPersonalInformationLegalInfo.FindControl("ddl_BusinessLine1_Legal") as DropDownList).SelectedItem.Text.Trim().Length == 0)
                    //{
                    //    oContact.LineOfBusiness = "";
                    //}
                    //else
                    //{
                    //    oContact.LineOfBusiness = (WUCPersonalInformationLegalInfo.FindControl("ddl_BusinessLine1_Legal") as DropDownList).SelectedItem.Text;
                    //}

                    if ((WUCPersonalInformationLegalInfo.FindControl("ddl_BusinessLine1_Legal") as DropDownList).Items.Count == 0 || (WUCPersonalInformationLegalInfo.FindControl("ddl_BusinessLine1_Legal") as DropDownList).SelectedItem.Text.Trim().Length == 0)
                    {
                        oContact.LineOfBusiness = "";
                    }
                    else
                    {
                        oContact.LineOfBusiness = (WUCPersonalInformationLegalInfo.FindControl("ddl_BusinessLine1_Legal") as DropDownList).SelectedItem.Text;
                    }

                    if ((WUCPersonalInformationLegalInfo.FindControl("ddl_BusinessLine2_Legal") as DropDownList) != null && (WUCPersonalInformationLegalInfo.FindControl("ddl_BusinessLine2_Legal") as DropDownList).Items != null && (WUCPersonalInformationLegalInfo.FindControl("ddl_BusinessLine2_Legal") as DropDownList).Items.Count > 0)
                    {
                        oContact.LineOfBusiness2 = (WUCPersonalInformationLegalInfo.FindControl("ddl_BusinessLine2_Legal") as DropDownList).SelectedItem.Text;
                    }
                    else
                    {
                        oContact.LineOfBusiness2 = "";
                    }
                    oContact.LaborTasks = (WUCPersonalInformationLegalInfo.FindControl("tb_WUC_PI_TaskPerformed_Legal") as TextBox).Text;
                    oContact.LengthWorkYear = (WUCPersonalInformationLegalInfo.FindControl("ddl_WUC_PI_LengthWorkFrom_Legal") as DropDownList).ToInt();
                    oContact.LengthWorkMonth = (WUCPersonalInformationLegalInfo.FindControl("ddl_WUC_PI_LengthWorkTo_Legal") as DropDownList).ToInt();

                    //Guarda la direccion completa del representante legal
                    var WUCAddressLe = this.Parent.FindControl("WUCAddressLegal");
                    var ddl_WUC_A_HomeStateL = (DropDownList)WUCAddressLe.FindControl("ddl_WUC_A_HomeState_Legal");
                    var ddl_WUC_A_HomeCountryL = (DropDownList)WUCAddressLe.FindControl("ddl_WUC_A_HomeCountry_Legal");
                    var ddl_WUC_A_HomeCityL = (DropDownList)WUCAddressLe.FindControl("ddl_WUC_A_HomeCity_Legal");

                    if (ddl_WUC_A_HomeCountryL.SelectedValue != "-1" &&
                                           ddl_WUC_A_HomeStateL.SelectedValue != "-1" &&
                                           (ddl_WUC_A_HomeCityL.SelectedValue != "-1" && ddl_WUC_A_HomeCityL.Items.Count > 0)
                                        )
                    {
                        var HomeStatele = Utility.deserializeJSON<Utility.StateProvince>(ddl_WUC_A_HomeStateL.SelectedValue);
                        var DocRegHomele = HomeStatele.DomesticregId;
                        var StateHomele = HomeStatele.StateProvId;
                        var RegionIDle = HomeStatele.RegionId;


                        oContact.RegionOfResidenceId = RegionIDle;
                        oContact.CountryOfResidenceId = (!string.IsNullOrEmpty(ddl_WUC_A_HomeCountryL.SelectedValue) ? Convert.ToInt32(ddl_WUC_A_HomeCountryL.SelectedValue) : 0);
                        oContact.DomesticRegOfResidenceId = DocRegHomele;
                        oContact.StateOfResidenceId = StateHomele;
                        oContact.CityOfResidenceId = int.Parse(ddl_WUC_A_HomeCityL.SelectedValue);

                    }

                    //guarda relacion con el propietario
                    if (UpdateRelationShip)
                    {
                        Control WUCBackgroundInformationLegal = null;

                        var bodyContent = this.Page.Master.FindControl("bodyContent");

                        WUCBackgroundInformationLegal = (!ObjServices.IsDataReviewMode) ?
                                                      bodyContent.FindControl("ContactsInfoContainer").FindControl("WUCBackgroundInformation")
                                                    : bodyContent.FindControl("DReviewContainer").FindControl("WUCBackgroundInformation");

                        var RelationshiptoOwner = -1;

                        if (WUCBackgroundInformationLegal != null)
                        {
                            var cbxRelationshipwithinsuredLegal = WUCBackgroundInformationLegal.FindControl("cbxRelationshipwithinsured_Legal");
                            if (cbxRelationshipwithinsuredLegal != null)
                            {
                                if (!string.IsNullOrEmpty((cbxRelationshipwithinsuredLegal as DropDownList).SelectedValue))
                                    RelationshiptoOwner = int.Parse((cbxRelationshipwithinsuredLegal as DropDownList).SelectedValue);
                            }
                        }

                        oContact.RelationshiptoOwner = RelationshiptoOwner == -1 ? (int?)null : RelationshiptoOwner;
                    }

                    //Actualizar el contacto
                    ObjServices.oContactManager.UpdateContact(oContact);

                    //Borrar
                    var ContactCitizenship = ObjServices.GetContactCitizenship();
                    if (!ContactCitizenship.isNullReferenceObject())
                    {
                        ObjServices.oContactManager.InsertContactCitizenship(
                            new Entity.UnderWriting.Entities.Contact.Citizenship
                            {
                                ContactId = oContact.ContactId,
                                GlobalCountryId = ContactCitizenship.GlobalCountryId,
                                Status = false,
                                CreateUser = ObjServices.UserID.Value,
                                ModifyUser = ObjServices.UserID.Value
                            });
                    }

                    if ((WUCPersonalInformationLegalInfo.FindControl("ddl_WUC_PI_CountryCitizenship_Legal") as DropDownList).SelectedValue != "-1")
                    {
                        ObjServices.oContactManager.InsertContactCitizenship(
                            new Entity.UnderWriting.Entities.Contact.Citizenship
                            {
                                ContactId = oContact.ContactId,
                                GlobalCountryId = (WUCPersonalInformationLegalInfo.FindControl("ddl_WUC_PI_CountryCitizenship_Legal") as DropDownList).ToInt(),
                                Status = true,
                                CreateUser = ObjServices.UserID.Value,
                                ModifyUser = ObjServices.UserID.Value
                            });
                    }

                }


            }
        }

        public void SetDataAndUpdateCompany(Entity.UnderWriting.Entities.Contact oContact, Utility.ContactRoleIDType Role, bool UpdateRelationShip = false)
        {
            //Guarda la direccion completa de la empresa
            Control WUCCompanyInfo = null;
            var Container = (!ObjServices.IsDataReviewMode) ? "ContactsInfoContainer" : "DReviewContainer";
            WUCCompanyInfo = this.Page.Master.FindControl("bodyContent").FindControl(Container).FindControl("WUCCompanyInfo");
            var WUCAddressLe = (WUCAddress)WUCCompanyInfo.FindControl("WUCAddress1");

            var ddl_WUC_A_HomeStateL = (DropDownList)WUCAddressLe.FindControl("ddl_WUC_A_BusinessState");
            var ddl_WUC_A_HomeCountryL = (DropDownList)WUCAddressLe.FindControl("ddl_WUC_A_BusinessCountry");
            var ddl_WUC_A_HomeCityL = (DropDownList)WUCAddressLe.FindControl("ddl_WUC_A_BusinessCity");

            if (ddl_WUC_A_HomeCountryL.SelectedValue != "-1" &&
                                   ddl_WUC_A_HomeStateL.SelectedValue != "-1" &&
                                   (ddl_WUC_A_HomeCityL.SelectedValue != "-1" && ddl_WUC_A_HomeCityL.Items.Count > 0))
            {
                var HomeStatele = Utility.deserializeJSON<Utility.StateProvince>(ddl_WUC_A_HomeStateL.SelectedValue);
                var DocRegHomele = HomeStatele.DomesticregId;
                var StateHomele = HomeStatele.StateProvId;
                var RegionIDle = HomeStatele.RegionId;


                oContact.RegionOfResidenceId = RegionIDle;
                oContact.CountryOfResidenceId = (!string.IsNullOrEmpty(ddl_WUC_A_HomeCountryL.SelectedValue) ? Convert.ToInt32(ddl_WUC_A_HomeCountryL.SelectedValue) : 0);
                oContact.DomesticRegOfResidenceId = DocRegHomele;
                oContact.StateOfResidenceId = StateHomele;
                oContact.CityOfResidenceId = int.Parse(ddl_WUC_A_HomeCityL.SelectedValue);
                oContact.CountryOfBirthId = int.Parse(ddl_WUC_A_HomeCountryL.SelectedValue);

                //Bussines DomesticRegID & StateID
                var BussinesState = Utility.deserializeJSON<Utility.StateProvince>(ddl_WUC_A_HomeStateL.SelectedValue);
                int DocRegBussines = BussinesState.DomesticregId;
                int StateBussines = BussinesState.StateProvId;
                var RegionID = BussinesState.RegionId;

            }

            //guarda relacion con el propietario
            if (UpdateRelationShip)
            {
                Control WUCBackgroundInformationLegal = null;

                var bodyContent = this.Page.Master.FindControl("bodyContent");

                WUCBackgroundInformationLegal = (!ObjServices.IsDataReviewMode) ?
                                           bodyContent.FindControl("ContactsInfoContainer").FindControl("WUCBackgroundInformationLegal")
                                         : bodyContent.FindControl("DReviewContainer").FindControl("WUCBackgroundInformationLegal");

                var RelationshiptoOwner = -1;

                if (WUCBackgroundInformationLegal != null)
                {
                    var cbxRelationshipwithinsuredCompany = WUCBackgroundInformationLegal.FindControl("cbxRelationshipwithinsured_Legal");
                    if (cbxRelationshipwithinsuredCompany != null)
                    {
                        if (!string.IsNullOrEmpty((cbxRelationshipwithinsuredCompany as DropDownList).SelectedValue))
                            RelationshiptoOwner = int.Parse((cbxRelationshipwithinsuredCompany as DropDownList).SelectedValue);
                    }
                }

                oContact.RelationshiptoOwner = RelationshiptoOwner == -1 ? (int?)null : RelationshiptoOwner;
            }
        }

        public bool validateData()
        {
            bool isValid = true;
            String message = "";

            if (String.IsNullOrWhiteSpace(tb_WUC_PI_FirstName.Text))
            {
                isValid = false;
                message = RESOURCE.UnderWriting.NewBussiness.Resources.FirstNameRequired + "\n";
            }

            if (String.IsNullOrWhiteSpace(tb_WUC_PI_FirstLastName.Text))
            {
                isValid = false;
                message += RESOURCE.UnderWriting.NewBussiness.Resources.LastNameRequired + "\n";
            }

            if (ddl_WUC_PI_Gender.SelectedValue == "-1")
            {
                isValid = false;
                message += RESOURCE.UnderWriting.NewBussiness.Resources.GenderRequired + "\n";
            }

            if (ddl_WUC_PI_Smoker.SelectedValue == "-1")
            {
                isValid = false;
                message += RESOURCE.UnderWriting.NewBussiness.Resources.SmokerRequired + "\n";
            }

            if (ddl_WUC_PI_CountryBirth.SelectedValue == "-1")
            {
                isValid = false;
                message += RESOURCE.UnderWriting.NewBussiness.Resources.CountryOfBirthRequired + "\n";
            }

            if (ddl_WUC_PI_CountryCitizenship.SelectedValue == "-1")
            {
                isValid = false;
                message += RESOURCE.UnderWriting.NewBussiness.Resources.CountryOfCitizenshipRequired + "\n";
            }

            if (String.IsNullOrWhiteSpace(tb_WUC_PI_DateBirth.Text))
            {
                isValid = false;
                message += RESOURCE.UnderWriting.NewBussiness.Resources.DateOfBirthRequired + "\n";
            }

            if (String.IsNullOrWhiteSpace(tb_WUC_PI_YearLyFamilyIncome.Text))
            {
                isValid = false;
                message += RESOURCE.UnderWriting.NewBussiness.Resources.YearlyFamilyIncomeRequired + "\n";
            }

            if (!isValid)
                this.MessageBox(message, null, null, true, "Warning");

            return isValid;
        }

        public void save()
        {
            object WUCSearch = null;
            CheckBox chkchkOwnerIsSameAsInsured = null;
            CheckBox chkIsCompany = null;

            if (!ObjServices.IsDataReviewMode)
            {
                WUCSearch = this.Page.Master.FindControl("bodyContent").FindControl("WUCSearchContacts");
                //Buscar el checkboxes
                chkchkOwnerIsSameAsInsured = (CheckBox)(WUCSearch as WEB.NewBusiness.NewBusiness.UserControls.ContactsInfo.WUCSearch).FindControl("chkOwnerIsSameAsInsured");
                chkIsCompany = (CheckBox)(WUCSearch as WEB.NewBusiness.NewBusiness.UserControls.ContactsInfo.WUCSearch).FindControl("chkIsCompany");
            }
            else//Esto solo debe suceder si se guarda desde datareview
            {
                //Si no es un Owner
                if (!ObjServices.isOwnerContact)
                {
                    chkchkOwnerIsSameAsInsured = new CheckBox();
                    chkchkOwnerIsSameAsInsured.Checked = false;
                    chkIsCompany = new CheckBox();
                    chkIsCompany.Checked = false;
                }
                else // Si es un owner
                {
                    //Si es un compañia
                    if (ObjServices.isCompanyOwner)
                    {
                        chkchkOwnerIsSameAsInsured = new CheckBox();
                        chkchkOwnerIsSameAsInsured.Checked = false;
                        chkIsCompany = new CheckBox();
                        chkIsCompany.Checked = true;
                    }
                    else
                    {
                        chkchkOwnerIsSameAsInsured = new CheckBox();
                        chkchkOwnerIsSameAsInsured.Checked = false;
                        chkIsCompany = new CheckBox();
                        chkIsCompany.Checked = false;
                    }
                }

            }

            /*
             Si es un cliente y se hizo un busqueda por el seearch y no es un nuevo caso
             
             */
            if (!ObjServices.isOwnerContact && ObjServices.IsDataSearch && !ObjServices.isNewCase)
                SetDataAndUpdate(ObjServices.GetContact(ObjServices.AddInsuredToPolicy(ObjServices.Contact_Id.Value)), Utility.ContactRoleIDType.Client);
            else
                /*
                 si es un cliente y no es el mismo que el asegurado y no es una compañia                 
                 */
                if (!ObjServices.isOwnerContact && (!chkchkOwnerIsSameAsInsured.Checked && !chkIsCompany.Checked))
                {
                    var oContact = ObjServices.SaveNewContact();
                    SetDataAndUpdate(oContact, Utility.ContactRoleIDType.Client);
                    ObjServices.isNewCase = false;
                    ObjServices.IsDataSearch = false;
                }
                else
                {
                    /*  Salvar Owner de la poliza  */
                    var OwnerID = 0;

                    OwnerID = (ObjServices.Owner_Id < 0 || ObjServices.IsDataSearch) ? ObjServices.AddOwnerToPolicy(chkIsCompany.Checked, chkchkOwnerIsSameAsInsured.Checked)
                                                      : ObjServices.Owner_Id.Value;
                    ObjServices.Owner_Id = OwnerID;
                    ObjServices.ContactEntityID = ObjServices.Owner_Id;

                    var OwnerContact = ObjServices.GetContact(OwnerID);

                    if (!chkIsCompany.Checked)
                    {
                        if (chkchkOwnerIsSameAsInsured.Checked)
                            ObjServices.oContactManager.UpdateContact(OwnerContact);
                        else
                            SetDataAndUpdate(OwnerContact, Utility.ContactRoleIDType.Owner, true);
                    }
                    else
                    {
                        //Declaro las variables para capturar los datos
                        var InstitutionalName = string.Empty;
                        var InstitutionalCountryId = string.Empty;
                        var InstitutionalPositionAtCompany = string.Empty;
                        var InstitutionalPrincipal = string.Empty;
                        var RegistrationNumber = string.Empty;
                        var RegistrationDate = string.Empty;
                        var rnc = string.Empty;
                        var ddlSocietyType = string.Empty;
                        var ddlCommercialActivity = string.Empty;
                        var ddlSocietyFinalBeneficiary = string.Empty;
                        var ddlIDType = string.Empty;

                        Control WUCCompanyInfo = null;

                        var Container = (!ObjServices.IsDataReviewMode) ? "ContactsInfoContainer" : "DReviewContainer";
                        WUCCompanyInfo = this.Page.Master.FindControl("bodyContent").FindControl(Container).FindControl("WUCCompanyInfo");

                        //Llenar las variables con los controles del formulario
                        if (WUCCompanyInfo != null)
                        {
                            //************codigo original antes de cambio de pantalla jutidico*************************//
                            /*InstitutionalCountryId = (WUCCompanyInfo.FindControl("ddlCountry") as DropDownList).SelectedValue;
                              InstitutionalPositionAtCompany = (WUCCompanyInfo.FindControl("txtPositionHeld") as TextBox).Text;
                              InstitutionalPrincipal = (WUCCompanyInfo.FindControl("txtContactPerson") as TextBox).Text;*/
                            InstitutionalName = (WUCCompanyInfo.FindControl("txtCompanyName") as TextBox).Text;
                            rnc = (WUCCompanyInfo.FindControl("txtCompanyRNC") as TextBox).Text;
                            RegistrationNumber = (WUCCompanyInfo.FindControl("txtRegistrationNumber") as TextBox).Text;
                            RegistrationDate = (WUCCompanyInfo.FindControl("txtRegistrationDate") as TextBox).Text;
                            ddlSocietyType = (WUCCompanyInfo.FindControl("ddlSocietyType") as DropDownList).SelectedValue;
                            ddlCommercialActivity = (WUCCompanyInfo.FindControl("ddlCommercialActivity") as DropDownList).SelectedValue;
                            ddlSocietyFinalBeneficiary = (WUCCompanyInfo.FindControl("ddlSocietyFinalBeneficiary") as DropDownList).SelectedValue;
                            ddlIDType = (WUCCompanyInfo.FindControl("ddlIDType") as DropDownList).SelectedValue;

                            SetDataAndUpdateCompany(OwnerContact, Utility.ContactRoleIDType.Owner, true);
                        }

                        //Actualizar la compañia
                        if (!string.IsNullOrWhiteSpace(InstitutionalName))
                            OwnerContact.InstitutionalName = InstitutionalName;

                        if (!string.IsNullOrWhiteSpace(InstitutionalCountryId))
                            OwnerContact.InstitutionalCountryId = int.Parse(InstitutionalCountryId);

                        if (!string.IsNullOrWhiteSpace(InstitutionalPositionAtCompany))
                            OwnerContact.InstitutionalPositionAtCompany = InstitutionalPositionAtCompany;

                        if (!string.IsNullOrWhiteSpace(InstitutionalPrincipal))
                            OwnerContact.InstitutionalPrincipal = InstitutionalPrincipal;

                        if (!string.IsNullOrWhiteSpace(ddlSocietyType))
                            OwnerContact.companyStructureId = int.Parse(ddlSocietyType);

                        if (!string.IsNullOrWhiteSpace(ddlSocietyFinalBeneficiary))
                            OwnerContact.finalBeneficiaryOptionId = int.Parse(ddlSocietyFinalBeneficiary);

                        if (!string.IsNullOrWhiteSpace(ddlCommercialActivity))
                            OwnerContact.companyActivityId = int.Parse(ddlCommercialActivity);

                        if (!ObjServices.IsDataReviewMode)
                        {
                            if (this.ObjServices.Relationship_With_Owner_ToAgentId.HasValue)
                                OwnerContact.RelationshiptoAgent = this.ObjServices.Relationship_With_Owner_ToAgentId;
                        }

                        if (!string.IsNullOrWhiteSpace(rnc))
                            OwnerContact.DocumentCompany = rnc;

                        OwnerContact.IsCompany = true;

                        OwnerContact.Dob = RegistrationDate.ParseFormat();

                        //Guardar la informacion de la compañia
                        ObjServices.oContactManager.UpdateContact(OwnerContact);

                        var vSeqNo = -1;
                        var vSeqNo2 = -1;

                        var IdDataList = ObjServices.GetAllIdDocumentInformation();

                        var IdDataItem = IdDataList.ToList();

                        if (IdDataItem != null)
                            //vSeqNo = IdDataItem.SeqNo;
                            vSeqNo = IdDataItem.Where(x => x.ContactIdType == 5).Select(x => x.SeqNo).FirstOrDefault();
                        vSeqNo2 = IdDataItem.Where(x => x.ContactIdType == 0).Select(x => x.SeqNo).FirstOrDefault();


                        //Item IDdocument
                        var objIDDoc = new Entity.UnderWriting.Entities.Contact.IdDocument()
                        {
                            //Key
                            ContactId = OwnerID,
                            SeqNo = vSeqNo,
                            //Campos 
                            ContactIdType = 5,//Company Registration
                            ContactIdTypeDescription = string.Empty,
                            Id = rnc,
                            MainIdentity = true,
                            ValidDate = Utility.IsDate(RegistrationDate) ? RegistrationDate.ParseFormat() : (DateTime?)null,
                            IssuedBy = null,
                            CountryIssuedBy = null,
                            //Información Usuario
                            UserId = ObjServices.UserID.Value
                        };

                        //Actualizar el id de la compañia RNC             
                        ObjServices.oContactManager.SetIdDocument(objIDDoc);

                        var objIDDoc2 = new Entity.UnderWriting.Entities.Contact.IdDocument()
                        {
                            ContactId = OwnerID,
                            SeqNo = vSeqNo2,
                            ContactIdType = 0,
                            ContactIdTypeDescription = string.Empty,
                            Id = RegistrationNumber,
                            MainIdentity = false,
                            ValidDate = Utility.IsDate(RegistrationDate) ? RegistrationDate.ParseFormat() : (DateTime?)null,
                            IssuedBy = null,
                            CountryIssuedBy = null,
                            UserId = ObjServices.UserID.Value
                        };

                        ObjServices.oContactManager.SetIdDocument(objIDDoc2);

                        ObjServices.Owner_Id = OwnerID;
                        ObjServices.ContactEntityID = ObjServices.Owner_Id;

                        var ContactPolicy = ObjServices.oPolicyManager.GetContactPolicy(
                             ObjServices.Corp_Id,
                             ObjServices.Region_Id,
                             ObjServices.Country_Id,
                             ObjServices.Domesticreg_Id,
                             ObjServices.State_Prov_Id,
                             ObjServices.City_Id,
                             ObjServices.Office_Id,
                             ObjServices.Case_Seq_No,
                             ObjServices.Hist_Seq_No,
                             ObjServices.Owner_Id,
                             null
                             ).FirstOrDefault();

                        ContactPolicy.companyActivityId = int.Parse(ddlCommercialActivity);
                        ContactPolicy.finalBeneficiaryOptionId = int.Parse(ddlSocietyFinalBeneficiary);
                        ContactPolicy.companyStructureId = int.Parse(ddlSocietyType);

                        //foreach (var item in ContactPolicy)
                        //{
                        //    item.companyActivityId = int.Parse(ddlCommercialActivity);
                        //    item.finalBeneficiaryOptionId = int.Parse(ddlSocietyFinalBeneficiary);
                        //    item.companyStructureId = int.Parse(ddlSocietyType);
                        //}

                        ObjServices.oPolicyManager.SetContactPolicyInfo(ContactPolicy);

                        //Aqui va la logica de la informacion del representante legal
                        var AgentLegalID = 0;

                        AgentLegalID = (ObjServices.Agent_Legal < 0 || ObjServices.IsDataSearch) ? ObjServices.AddAgentLegalToPolicy() : ObjServices.Agent_Legal.Value;

                        ObjServices.Agent_Legal = AgentLegalID;

                        var AgentContact = ObjServices.GetContact(AgentLegalID);

                        SetDataAndUpdateLegal(AgentContact, Utility.ContactRoleIDType.Legal, true);

                        OwnerContact.LegalContactId = AgentLegalID;
                        ObjServices.oContactManager.UpdateContact(OwnerContact);
                    }
                }

            ObjServices.IsDataSearch = false;
            //El Tab esta completo            
            ObjServices.saveSetValidTab(currentTab == "ClientInfo" ? Utility.Tab.ClientInfo : Utility.Tab.OwnerInfo);
        }

        public void edit()
        {

        }

        public void LoadSameDataFromInsured(Entity.UnderWriting.Entities.Contact nContact)
        {
            FillDrops();

            if (nContact != null)
            {
                tb_WUC_PI_FirstName.Text = nContact.FirstName;
                tb_WUC_PI_MiddleName.Text = nContact.MiddleName;
                tb_WUC_PI_FirstLastName.Text = nContact.FirstLastName;
                tb_WUC_PI_SecondLastName.Text = nContact.SecondLastName;
                ddl_WUC_PI_Gender.SelectIndexByValue((string.IsNullOrEmpty(nContact.Gender)) ? "-1" : nContact.Gender);
                ddl_WUC_PI_Smoker.SelectIndexByValue(!nContact.Smoker.HasValue ? "-1" : (nContact.Smoker.Value ? "1" : "0"));
                ddl_WUC_PI_CountryBirth.SelectIndexByValue(nContact.CountryOfBirthId.ToString());
                tb_WUC_PI_DateBirth.Text = !nContact.Dob.HasValue ? "" : nContact.Dob.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                tb_WUC_PI_Age.Text = !nContact.Age.HasValue ? "" : nContact.Age.Value.ToString();
                hdnAge.Value = !nContact.Age.HasValue ? "" : nContact.Age.Value.ToString();
                ddlCountryOfResidence.SelectIndexByValue(nContact.CountryOfResidenceId.ToString(), true);

                ddl_WUC_PI_MaritalStatus.SelectIndexByValue(nContact.MaritalStatId.ToString());
                tb_WUC_PI_PersonalIncome.Text = !nContact.AnnualPersonalIncome.HasValue ? ""
                    : nContact.AnnualPersonalIncome.Value.ToString(NumberFormatInfo.InvariantInfo);
                tb_WUC_PI_YearLyFamilyIncome.Text = !nContact.AnnualFamilyIncome.HasValue ? ""
                    : nContact.AnnualFamilyIncome.Value.ToString(NumberFormatInfo.InvariantInfo);

                if (nContact.OccupGroupTypeId.HasValue)
                {
                    txtOccupation.Text = nContact.Occupation_Desc;
                    hdnOccupationGroupId.Value = nContact.OccupGroupTypeId.Value.ToString();
                }

                if (nContact.OccupationId.HasValue)
                {
                    txtProfession.Text = nContact.Occupation_Group_Desc;
                    hdnOccupationId.Value = nContact.OccupationId.Value.ToString();
                }

                tb_WUC_PI_CompanyName.Text = nContact.CompanyName;
                //tb_WUC_PI_FirstLineBusinnes.Text = nContact.LineOfBusiness;

                //if (nContact.LineOfBusiness != null) estaba asi
                //{
                //    ddl_BusinessLine1.SelectedItem.Text = (nContact.LineOfBusiness.Length == 0 ? "----" : nContact.LineOfBusiness);
                //    if (ddl_BusinessLine2.Items.Count > 0)
                //        ddl_BusinessLine2.Items.Clear();
                //    ListItem lstBL2 = new ListItem(nContact.LineOfBusiness2, "-1");
                //    ddl_BusinessLine2.Items.Add(lstBL2);
                //}

                if (nContact.LineOfBusiness2 != null && nContact.LineOfBusiness2.Length > 0)
                {
                    Utility.SelectIndexByText(ref ddl_BusinessLine2, nContact.LineOfBusiness2);
                    //ddl_BusinessLine1.SelectedItem.Text = (nContact.LineOfBusiness.Length == 0 ? "----" : nContact.LineOfBusiness);
                    if (ddl_BusinessLine1.Items.Count > 0)
                        ddl_BusinessLine1.Items.Clear();

                    ListItem lstBL2 = new ListItem(nContact.LineOfBusiness, "-1");
                    ddl_BusinessLine1.Items.Add(lstBL2);
                }

                //Bmarroquin 05-03-2017 Correccion para que no se pierda la Linea o Giro de Negocio 1 
                if (nContact.LineOfBusiness != null && ddl_BusinessLine2 != null && ddl_BusinessLine2.SelectedIndex > 0)
                {
                    var countryId = ddl_BusinessLine2 != null && ddl_BusinessLine2.Items != null && ddl_BusinessLine2.Items.Count > 0 ? ddl_BusinessLine2.SelectedValue.ToInt() : 0;
                    if (countryId > 0)
                    {
                        ObjServices.GettingAllDrops(ref ddl_BusinessLine1,
                                  Utility.DropDownType.CustomerBusinessLine,
                                  "GlobalCountryDesc",
                                  "CountryId",
                                  countryId: countryId,
                                GenerateItemSelect: true
                               );
                        if (nContact.LineOfBusiness.Length > 0)
                        {
                            Utility.SelectIndexByText(ref ddl_BusinessLine1, nContact.LineOfBusiness);
                        }
                    }
                }
                //Fin Correccion Bmarroquin 05-03-2017

                //ddl_BusinessLine1.SelectedItem.Text = (nContact.LineOfBusiness.Length == 0 ? "----" : nContact.LineOfBusiness);

                //tb_WUC_PI_SecondLineBusiness.Text = nContact.LineOfBusiness2;
                //ddl_BusinessLine2.SelectedItem.Text = (nContact.LineOfBusiness2.Length == 0 ? "----" : nContact.LineOfBusiness2);
                /*if (ddl_BusinessLine1.SelectedIndex > 0)
                {
                    ObjServices.GettingAllDrops(ref ddl_BusinessLine2,
                                      Utility.DropDownType.CustomerBusinessLine2,
                                      "GlobalCountryDesc",
                                      "CountryId",
                                      countryId: int.Parse(ddl_BusinessLine1.SelectedValue),
                                    GenerateItemSelect: false
                                   );
                    ddl_BusinessLine2.SelectedItem.Text = (nContact.LineOfBusiness2.Length == 0 ? "----" : nContact.LineOfBusiness2);
                }*/

                tb_WUC_PI_TaskPerformed.Text = nContact.LaborTasks;
                ddl_WUC_PI_LengthWorkFrom.SelectIndexByValue(!nContact.LengthWorkYear.HasValue ? "-1"
                    : nContact.LengthWorkYear.Value.ToString());
                ddl_WUC_PI_LengthWorkTo.SelectIndexByValue(!nContact.LengthWorkMonth.HasValue ? "-1"
                    : nContact.LengthWorkMonth.Value.ToString());
                var GlobalCountryId = ObjServices.oContactManager.GetContactCitizenshipByContact(nContact.ContactId).FirstOrDefault().GlobalCountryId.ToString();
                ddl_WUC_PI_CountryCitizenship.SelectIndexByValue(GlobalCountryId);
            }
            else
                ClearControls();

            udpPersonalInformation.Update();

        }

        public void FillData()
        {
            var nContact = ObjServices.GetContact(ObjServices.ContactEntityID.Value);

            if (nContact != null)
            {
                tb_WUC_PI_FirstName.Text = nContact.FirstName;
                tb_WUC_PI_MiddleName.Text = nContact.MiddleName;
                tb_WUC_PI_FirstLastName.Text = nContact.FirstLastName;
                tb_WUC_PI_SecondLastName.Text = nContact.SecondLastName;
                ddl_WUC_PI_Gender.SelectIndexByValue((string.IsNullOrEmpty(nContact.Gender)) ? "-1" : nContact.Gender);
                ddl_WUC_PI_Smoker.SelectIndexByValue(!nContact.Smoker.HasValue ? "-1" : (nContact.Smoker.Value ? "1" : "0"));

                ddl_WUC_PI_CountryBirth.SelectIndexByValue(nContact.CountryOfBirthId.ToString(), true);
                ddlCountryOfResidence.SelectIndexByValue(nContact.CountryOfResidenceId.ToString(), true);

                tb_WUC_PI_DateBirth.Text = !nContact.Dob.HasValue ? "" :
                                           nContact.Dob.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                hdnAge.Value = !nContact.Age.HasValue ? "" : nContact.Age.Value.ToString();

                if (nContact.MaritalStatId.HasValue)
                    ddl_WUC_PI_MaritalStatus.SelectIndexByValue(nContact.MaritalStatId.ToString(), true);

                tb_WUC_PI_PersonalIncome.Text = !nContact.AnnualPersonalIncome.HasValue ? ""
                                                                                        : nContact.AnnualPersonalIncome.Value.ToString(NumberFormatInfo.InvariantInfo);

                tb_WUC_PI_YearLyFamilyIncome.Text = !nContact.AnnualFamilyIncome.HasValue ? ""
                                                                                          : nContact.AnnualFamilyIncome.Value.ToString(NumberFormatInfo.InvariantInfo);

                if (nContact.OccupGroupTypeId.HasValue)
                {
                    txtOccupation.Text = nContact.Occupation_Desc;
                    hdnOccupationGroupId.Value = nContact.OccupGroupTypeId.Value.ToString();
                }

                if (nContact.OccupationId.HasValue)
                {
                    txtProfession.Text = nContact.Occupation_Group_Desc;
                    hdnOccupationId.Value = nContact.OccupationId.Value.ToString();
                }

                tb_WUC_PI_CompanyName.Text = nContact.CompanyName;

                if (nContact.LineOfBusiness2 != null)
                {

                    if (nContact.LineOfBusiness2.Length > 0)
                    {
                        Utility.SelectIndexByText(ref ddl_BusinessLine2, nContact.LineOfBusiness2);
                    }
                    //ddl_BusinessLine2.SelectedItem.Text = (nContact.LineOfBusiness2.Length == 0 ? "----" : nContact.LineOfBusiness2);
                    if (ddl_BusinessLine1.Items.Count > 0)
                        ddl_BusinessLine1.Items.Clear();
                    ListItem lstBL1 = new ListItem(nContact.LineOfBusiness, "-1");
                    ddl_BusinessLine1.Items.Add(lstBL1);

                }
                if (nContact.LineOfBusiness != null && ddl_BusinessLine2 != null && ddl_BusinessLine2.SelectedIndex > 0)
                {
                    var countryId = ddl_BusinessLine2 != null && ddl_BusinessLine2.Items != null && ddl_BusinessLine2.Items.Count > 0 ? ddl_BusinessLine2.SelectedValue.ToInt() : 0;
                    if (countryId > 0)
                    {
                        ObjServices.GettingAllDrops(ref ddl_BusinessLine1,
                                  Utility.DropDownType.CustomerBusinessLine,
                                  "GlobalCountryDesc",
                                  "CountryId",
                                  countryId: countryId,
                                GenerateItemSelect: true
                               );
                        if (nContact.LineOfBusiness.Length > 0)
                        {
                            Utility.SelectIndexByText(ref ddl_BusinessLine1, nContact.LineOfBusiness);
                        }
                    }
                }

                //tb_WUC_PI_SecondLineBusiness.Text = nContact.LineOfBusiness2;
                //ddl_BusinessLine2.SelectedItem.Text = (nContact.LineOfBusiness2.Length == 0 ? "----" : nContact.LineOfBusiness2);
                /*if (ddl_BusinessLine1.SelectedIndex > 0)
                {
                    ObjServices.GettingAllDrops(ref ddl_BusinessLine2,
                                      Utility.DropDownType.CustomerBusinessLine2,
                                      "GlobalCountryDesc",
                                      "CountryId",
                                      countryId: int.Parse(ddl_BusinessLine1.SelectedValue),
                                      abaNumber: ddl_BusinessLine1.SelectedItem.Text,
                                    GenerateItemSelect: false
                                   );
                    
                }
                else
                {
                    if (nContact.LineOfBusiness2.Length > 0)
                        ddl_BusinessLine2.SelectedItem.Text = nContact.LineOfBusiness2;
                    else
                        ddl_BusinessLine2.SelectedItem.Text = "----";
                }*/
                tb_WUC_PI_TaskPerformed.Text = nContact.LaborTasks;

                if (nContact.LengthWorkYear.HasValue)
                    ddl_WUC_PI_LengthWorkFrom.SelectIndexByValue(nContact.LengthWorkYear.Value.ToString(), true);

                if (nContact.LengthWorkMonth.HasValue)
                    ddl_WUC_PI_LengthWorkTo.SelectIndexByValue(nContact.LengthWorkMonth.Value.ToString(), true);

                var ContactCitizenship = ObjServices.GetContactCitizenship();

                if (ContactCitizenship != null)
                    ddl_WUC_PI_CountryCitizenship.SelectIndexByValue(ContactCitizenship.GlobalCountryId.ToString());
            }
            udpPersonalInformation.Update();

        }

        public void Initialize(String value = "")
        {
            hdnCurrentSession.Value = String.IsNullOrEmpty(value) ? "" : value;
            Initialize();
        }

        public void Initialize()
        {
            ClearData();
            FillDrops();
            FillData();

            if (ObjServices.IsDataReviewMode)
                EnabledControls(!(currentTab == "OwnerInfo" && ObjServices.Contact_Id == ObjServices.Owner_Id));
        }

        public void ClearData()
        {
            hdnOccupationGroupId.Value = string.Empty;
            hdnOccupationId.Value = string.Empty;
            ClearControls(this);
            if (ddl_BusinessLine2.Items.Count > 0)
                ddl_BusinessLine2.Items.Clear();
            if (ddl_BusinessLine1.Items.Count > 0)
            {
                ddl_BusinessLine1.Items.Clear();
            }
        }

        public void EnabledControls(bool x)
        {
            EnabledControls(frmPersonalInformation.Controls, x);
            //Bmarroquin 01-07-2017 cambio para que no se pueda modificar la edad dado que cambia la prima calculada solo se debe poder realizar desde el cotizador, a raiz de tropicalizacion ESA
            //tb_WUC_PI_DateBirth.Enabled = false;
            udpPersonalInformation.Update();
        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            Utility.ReadOnlyControls(frmPersonalInformation.Controls, isReadOnly);
            udpPersonalInformation.Update();
        }

        protected void ddl_BusinessLine2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjServices.GettingAllDrops(ref ddl_BusinessLine1,
                                      Utility.DropDownType.CustomerBusinessLine,
                                      "GlobalCountryDesc",
                                      "CountryId",
                                      countryId: int.Parse(ddl_BusinessLine2.SelectedValue),
                                    GenerateItemSelect: false
                                   );
            if (ddl_BusinessLine1.Items.Count > 0)
            {
                ddl_BusinessLine1.SelectedIndex = 0;
            }
        }
    }
}