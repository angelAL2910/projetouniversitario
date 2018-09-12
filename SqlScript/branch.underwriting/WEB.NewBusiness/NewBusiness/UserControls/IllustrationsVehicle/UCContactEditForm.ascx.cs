﻿using Entity.UnderWriting.Entities;
using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle
{
    public partial class UCContactEditForm : UC, IUC
    {
        private string SelectedCreditCardMask
        {
            get { return ViewState["SelectedCreditCardMask"].ToString(); }
            set { ViewState["SelectedCreditCardMask"] = value; }
        }

        public int ContactId
        {
            get
            {
                return (int)ViewState["ContactId"];
            }
            set
            {
                ViewState["ContactId"] = value;
            }
        }

        private Utility.CumplimientoItem cumplimientoItem
        {
            get
            {
                return (Utility.CumplimientoItem)ViewState["cumplimientoItem"];
            }
            set
            {
                ViewState["cumplimientoItem"] = value;
            }
        }

        private bool IsCompany { get { return ViewState["IsCompany"].ToBoolean(); } set { ViewState["IsCompany"] = value; } }

        public TextBox txtId { get; set; }
        public TextBox txtExpDate { get; set; }
        public DropDownList dropIdType { get; set; }

        public class itemCreditCardType
        {
            public int CreditCardTypeId { get; set; }
            public string CreditCardMask { get; set; }
        }

        public class itemPepFormularyOption
        {
            public int CorpId { get; set; }
            public int AgentId { get; set; }
        }

        public class itemFinalBeneficiary
        {
            public int CorpId { get; set; }
            public int AgentId { get; set; }
            public int CountryId { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Translator(string Lang)
        {

        }

        public void ReadOnlyControls(bool isReadOnly)
        {

        }

        public void save()
        {

        }

        public void save(bool ShowMessage = true)
        {
            try
            {
                //Obtener los datos del contacto
                var dataContact = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, ContactId, ObjServices.Language.ToInt());
                //obtener los telefonos del contacto
                var dataPhones = ObjServices.oContactManager.GetCommunicatonPhone(ObjServices.Corp_Id, ContactId, ObjServices.Language.ToInt());
                //obtener los correos del contacto
                var dataEmail = ObjServices.oContactManager.GetCommunicatonEmail(ObjServices.Corp_Id, ContactId, ObjServices.Language.ToInt());
                //obtener los id
                var lstIdentification = ObjServices.oContactManager.GetAllIdDocumentInformation(ContactId, ObjServices.getCurrentLanguage());
                //obtener las direcciones
                var dataAddress = ObjServices.oContactManager
                                             .GetCommunicatonAdress(ObjServices.Corp_Id, ContactId, languageId: ObjServices.Language.ToInt());


                var dataPeps = ObjServices.oContactManager.GetContactPEPFormulary(this.ContactId, "CalidadPep").ToList();
                if (dataPeps.Count > 0)
                {
                    foreach (var item in dataPeps)
                    {
                        if (string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.Position))
                        {
                            this.MessageBox("Existen algunos PEPs con información incompleta, favor verificar");
                            return;
                        }
                    }
                }

                //Actualizar el nombre del cliente
                dataContact.FirstName = txtFirstName.Text;
                dataContact.MiddleName = txtMiddleName.Text;
                dataContact.FirstLastName = txtLastName.Text;
                dataContact.SecondLastName = txtSecondLastName.Text;
                dataContact.ModifyUser = ObjServices.UserID.GetValueOrDefault();
                dataContact.Dob = txtDob.Text.ParseFormat();
                dataContact.FullName = string.Concat(dataContact.FirstName, " ", dataContact.MiddleName, " ", dataContact.FirstLastName, " ", dataContact.SecondLastName);
                dataContact.InstitutionalName = this.IsCompany ? txtCompanyName.Text : string.Empty;

                dataContact.OccupGroupTypeId = !string.IsNullOrEmpty(hdnOccupationGroupId.Value) ? hdnOccupationGroupId.ToInt() : (int?)null;
                dataContact.OccupationId = string.IsNullOrEmpty(hdnOccupationId.Value) ? (int?)null : int.Parse(hdnOccupationId.Value);
                var annaualPersonalIncome = decimal.Parse(txtIngresoAnual.Text.Replace(",", ""), CultureInfo.InvariantCulture);
                dataContact.AnnualPersonalIncome = annaualPersonalIncome;
                dataContact.CompanyName = txtEmpresaLabora.Text;
                this.IsCompany = ddlTypeOfPerson.SelectedValue.ToInt() != 1 && ddlTypeOfPerson.SelectedValue.ToInt() != 3;
                dataContact.ContactTypeId = this.IsCompany ? Utility.ContactTypeId.Company.ToInt() : Utility.ContactTypeId.Client.ToInt();
                dataContact.IsCompany = this.IsCompany;
                dataContact.MaritalStatId = ddlMaritalStatus.ToInt() == -1 ? (int?)null : ddlMaritalStatus.ToInt();
                dataContact.qtyPersonsDepend = string.IsNullOrEmpty(txtDependencyCount.Text) ? (int?)null : txtDependencyCount.ToInt();
                dataContact.homeOwner = ddlHomeOwner.SelectedValue != "-1" ? (ddlHomeOwner.SelectedValue == "S") : (bool?)null;
                dataContact.linked = "NI";
                dataContact.qtyEmployees = IsCompany ? txtQtyEmployee.ToInt() : (int?)null;

                if (!string.IsNullOrEmpty(txtCreditCardNumber.Text) && !txtCreditCardNumber.ReadOnly)
                {
                    var CreditCardNumber = txtCreditCardNumber.Text.Replace("-", "");
                    txtCreditCardNumber.Clear();
                    dataContact.CreditCardNumber = Utility.Encrypt_Query(CreditCardNumber);
                    dataContact.ExpirationDateYear = ddlYear.ToInt();
                    dataContact.ExpirationDateMonth = ddlMonth.ToInt();
                    var oitemCreditCardType = Utility.deserializeJSON<itemCreditCardType>(ddlCreditCardType.SelectedValue);
                    var MaskKey = string.Empty;
                    var Longitud = 0;
                    var qtyCharacter = 0;
                    var NumVisible = string.Empty;
                    switch (oitemCreditCardType.CreditCardTypeId)
                    {
                        case 1:
                        case 3:
                            Longitud = CreditCardNumber.Length - 4;
                            qtyCharacter = (CreditCardNumber.Length - Longitud);
                            NumVisible = CreditCardNumber.Substring(Longitud, qtyCharacter);
                            MaskKey = NumVisible.PadLeft(Longitud + 4, '*');
                            break;
                        case 2:
                            Longitud = CreditCardNumber.Length - 5;
                            qtyCharacter = (CreditCardNumber.Length - Longitud);
                            NumVisible = CreditCardNumber.Substring(Longitud, qtyCharacter);
                            MaskKey = NumVisible.PadLeft(Longitud + 5, '*');
                            break;
                    }

                    dataContact.CreditCardNumberKey = MaskKey;
                    dataContact.CreditCardTypeId = oitemCreditCardType.CreditCardTypeId;
                    dataContact.CardHolder = txtCardHolder.Text;
                }

                if (ddlRepresentativeIdentificationType.SelectedValue != "-1")
                {
                    dataContact.Representative = txtRepresentativeName.Text;
                    dataContact.RepresentativeIdentification = txtRepresentativeIdentification.Text;
                    dataContact.RepresentativeIdentificationTypeId = ddlRepresentativeIdentificationType.ToInt();
                }

                if (ddlPep.SelectedValue != "-1" && !this.IsCompany)
                {
                    var ItemSelected = Utility.deserializeJSON<itemPepFormularyOption>(ddlPep.SelectedValue);
                    dataContact.pepFormularyOptionId = ItemSelected.AgentId;
                }
                else
                    dataContact.pepFormularyOptionId = null;

                if (ddlBeneFinal.SelectedValue != "-1")
                {
                    var ItemSelected = Utility.deserializeJSON<itemFinalBeneficiary>(ddlBeneFinal.SelectedValue);
                    dataContact.finalBeneficiaryOptionId = ItemSelected.AgentId;
                }
                else
                    dataContact.finalBeneficiaryOptionId = null;

                if (ddlEstructuraTitularidad.SelectedValue != "-1" && this.IsCompany)
                    dataContact.companyStructureId = ddlEstructuraTitularidad.ToInt();
                else
                    dataContact.companyStructureId = null;

                if (ddlActividad.SelectedValue != "-1" && this.IsCompany)
                    dataContact.companyActivityId = ddlActividad.ToInt();
                else
                    dataContact.companyActivityId = null;

                if (ObjServices.CustomerName.ToLower().Trim() != dataContact.FullName.ToLower().Trim())
                {
                    //Borrar el documento de cliente contactado via telefonica siempre y cuando este no haya sido validado ya que esto es irreversible                   
                }

                dataContact.Gender = ddlGender.SelectedValue != "-1" ? ddlGender.SelectedValue : string.Empty;
                dataContact.InvoiceTypeId = ddlNcfType.SelectedValue != "-1" ? ddlNcfType.SelectedValue.ToInt() : (int?)null;

                ObjServices.oContactManager.UpdateContact(dataContact);

                //Borrar
                var ContactCitizenship = ObjServices.GetContactCitizenship(dataContact.ContactId);

                if (!ContactCitizenship.isNullReferenceObject())
                {
                    ObjServices.oContactManager.InsertContactCitizenship(
                        new Entity.UnderWriting.Entities.Contact.Citizenship
                        {
                            ContactId = dataContact.ContactId,
                            GlobalCountryId = ContactCitizenship.GlobalCountryId,
                            Status = false,
                            CreateUser = ObjServices.UserID.Value,
                            ModifyUser = ObjServices.UserID.Value
                        });
                }

                if (ddlCountryCitizenship.SelectedValue != "-1")
                {
                    ObjServices.oContactManager.InsertContactCitizenship(
                        new Entity.UnderWriting.Entities.Contact.Citizenship
                        {
                            ContactId = dataContact.ContactId,
                            GlobalCountryId = ddlCountryCitizenship.ToInt(),
                            Status = true,
                            CreateUser = ObjServices.UserID.Value,
                            ModifyUser = ObjServices.UserID.Value
                        });
                }

                var Policy = new Entity.UnderWriting.Entities.Contact.PolicyContact
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
                    UserId = ObjServices.UserID.GetValueOrDefault(),
                    ContactRoleTypeId = Utility.ContactRoleIDType.Client.ToInt(),
                    ContactId = dataContact.ContactId
                };

                dataContact.PolicyInfo = Policy;

                #region Informacion de Cumplimiento
                dataContact.TypeOfPerson = ddlTypeOfPerson.SelectedValue.ToInt();
                dataContact.WorkAddress = txtWorkAddress.Text;
                dataContact.PlaceOfBirth = txtPlaceOfBirth.Text;
                dataContact.ManagerName = string.IsNullOrEmpty(txtManagerName.Text) ? null : txtManagerName.Text;
                if (ddlAdminPepFormularyOptionsId.SelectedIndex > 0)
                {
                    var PepAdminSelected = Utility.deserializeJSON<itemPepFormularyOption>(ddlAdminPepFormularyOptionsId.SelectedValue);
                    dataContact.ManagerPepOptionId = PepAdminSelected.AgentId;
                }

                #endregion

                //Actualizar el contacto de la poliza
                ObjServices.oPolicyManager.UpdatePersonalInfoContact(dataContact);

                //Actualizar los telefonos
                var DataCellPhone = dataPhones.FirstOrDefault(b => b.DirectoryTypeId == Utility.DirectoryType.CellPhone.ToInt());
                var DataTelephone = dataPhones.FirstOrDefault(c => c.DirectoryTypeId == Utility.DirectoryType.HomePhone.ToInt());
                var DataWorkPhone = dataPhones.FirstOrDefault(c => c.DirectoryTypeId == Utility.DirectoryType.BusinessPhone.ToInt());
                var DataFax = dataPhones.FirstOrDefault(c => c.DirectoryTypeId == Utility.DirectoryType.Fax.ToInt());

                if (DataCellPhone != null)
                {   //Modificar
                    DataCellPhone.PhoneNumber = txtCellPhone.Text;
                    DataCellPhone.ContactId = ContactId;
                    DataCellPhone.ModifyUser = ObjServices.UserID.GetValueOrDefault();
                }
                else if (!string.IsNullOrEmpty(txtCellPhone.Text))
                {
                    //Insertar 
                    DataCellPhone = new Entity.UnderWriting.Entities.Contact.Phone
                    {
                        //Key
                        CorpId = ObjServices.Corp_Id,
                        DirectoryId = -1,
                        DirectoryDetailId = -1,
                        CommunicationType = Utility.CommType.Phone.ToString(),
                        ContactId = ContactId,

                        //Campos 
                        DirectoryTypeId = Utility.DirectoryType.CellPhone.ToInt(),
                        DirectoryTypeDesc = string.Empty,
                        CountryCode = string.Empty,
                        AreaCode = string.Empty,
                        PhoneNumber = txtCellPhone.Text.Replace("-", "").Replace("(", "").Replace(")", ""),
                        PhoneExt = string.Empty,
                        IsPrimary = true,
                        //Campo aun Indefinido --Preguntar
                        PersonToContact = null,
                        //Información Usuario
                        CreateUser = ObjServices.UserID.GetValueOrDefault()
                    };
                }

                if (DataTelephone != null)
                {  //Modificar
                    DataTelephone.PhoneNumber = txtHomePhone.Text;
                    DataTelephone.ContactId = ContactId;
                    DataTelephone.ModifyUser = ObjServices.UserID.GetValueOrDefault();
                }
                else if (!string.IsNullOrEmpty(txtHomePhone.Text))
                {
                    //Insertar
                    DataTelephone = new Entity.UnderWriting.Entities.Contact.Phone
                    {
                        //Key
                        CorpId = ObjServices.Corp_Id,
                        DirectoryId = -1,
                        DirectoryDetailId = -1,
                        CommunicationType = Utility.CommType.Phone.ToString(),
                        ContactId = ContactId,

                        //Campos 
                        DirectoryTypeId = Utility.DirectoryType.HomePhone.ToInt(),
                        DirectoryTypeDesc = string.Empty,
                        CountryCode = string.Empty,
                        AreaCode = string.Empty,
                        PhoneNumber = txtHomePhone.Text.Replace("-", "").Replace("(", "").Replace(")", ""),
                        PhoneExt = string.Empty,
                        IsPrimary = false,
                        //Campo aun Indefinido --Preguntar
                        PersonToContact = null,
                        //Información Usuario
                        CreateUser = ObjServices.UserID.GetValueOrDefault()
                    };
                }


                if (DataWorkPhone != null)
                {  //Modificar
                    DataWorkPhone.PhoneNumber = txtWorkPhone.Text;
                    DataWorkPhone.ContactId = ContactId;
                    DataWorkPhone.ModifyUser = ObjServices.UserID.GetValueOrDefault();
                }
                else if (!string.IsNullOrEmpty(txtWorkPhone.Text))
                {
                    //Insertar
                    DataWorkPhone = new Entity.UnderWriting.Entities.Contact.Phone
                    {
                        //Key
                        CorpId = ObjServices.Corp_Id,
                        DirectoryId = -1,
                        DirectoryDetailId = -1,
                        CommunicationType = Utility.CommType.Phone.ToString(),
                        ContactId = ContactId,

                        //Campos 
                        DirectoryTypeId = Utility.DirectoryType.BusinessPhone.ToInt(),
                        DirectoryTypeDesc = string.Empty,
                        CountryCode = string.Empty,
                        AreaCode = string.Empty,
                        PhoneNumber = txtWorkPhone.Text.Replace("-", "").Replace("(", "").Replace(")", ""),
                        PhoneExt = string.Empty,
                        IsPrimary = false,
                        //Campo aun Indefinido --Preguntar
                        PersonToContact = null,
                        //Información Usuario
                        CreateUser = ObjServices.UserID.GetValueOrDefault()
                    };
                }

                if (DataFax != null)
                {  //Modificar
                    DataFax.PhoneNumber = txtFax.Text;
                    DataFax.ContactId = ContactId;
                    DataFax.ModifyUser = ObjServices.UserID.GetValueOrDefault();
                }
                else if (!string.IsNullOrEmpty(txtFax.Text))
                {
                    //Insertar
                    DataFax = new Entity.UnderWriting.Entities.Contact.Phone
                    {
                        //Key
                        CorpId = ObjServices.Corp_Id,
                        DirectoryId = -1,
                        DirectoryDetailId = -1,
                        CommunicationType = Utility.CommType.Phone.ToString(),
                        ContactId = ContactId,

                        //Campos 
                        DirectoryTypeId = Utility.DirectoryType.Fax.ToInt(),
                        DirectoryTypeDesc = string.Empty,
                        CountryCode = string.Empty,
                        AreaCode = string.Empty,
                        PhoneNumber = txtFax.Text.Replace("-", "").Replace("(", "").Replace(")", ""),
                        PhoneExt = string.Empty,
                        IsPrimary = false,
                        //Campo aun Indefinido --Preguntar
                        PersonToContact = null,
                        //Información Usuario
                        CreateUser = ObjServices.UserID.GetValueOrDefault()
                    };
                }

                if (DataCellPhone != null)
                    ObjServices.oContactManager.SetPhone(DataCellPhone);


                if (DataTelephone != null)
                    ObjServices.oContactManager.SetPhone(DataTelephone);

                if (DataWorkPhone != null)
                    ObjServices.oContactManager.SetPhone(DataWorkPhone);

                if (DataFax != null)
                    ObjServices.oContactManager.SetPhone(DataFax);

                //Actualizar el correo
                var dMail = dataEmail.FirstOrDefault(h => h.IsPrimary);

                if (dMail != null)
                {
                    dMail.EmailAdress = txtEmail.Text;
                    dMail.ContactId = ContactId;
                    dMail.ModifyUser = ObjServices.UserID.GetValueOrDefault();
                }
                else if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    dMail = new Entity.UnderWriting.Entities.Contact.Email
                    {
                        //Key
                        CorpId = ObjServices.Corp_Id,
                        DirectoryId = -1,
                        DirectoryDetailId = -1,
                        CommunicationType = Utility.CommType.Email.ToString(),
                        ContactId = ContactId,

                        //Campos 
                        DirectoryTypeId = Utility.EmailType.Home.ToInt(),
                        DirectoryTypeDesc = string.Empty,
                        EmailAdress = txtEmail.Text,
                        IsPrimary = true,

                        //Información Usuario
                        CreateUser = ObjServices.UserID.GetValueOrDefault()
                    };
                }

                if (dMail != null)
                    ObjServices.oContactManager.SetEmail(dMail);

                if (lstIdentification != null && lstIdentification.Any())
                {
                    var dataId = lstIdentification.First();
                    dataId.Id = txtIDNumber.Text.Replace("-", "");
                    dataId.ContactId = ContactId;
                    dataId.ContactIdType = ddlIdType.SelectedValue.ToInt();
                    dataId.ExpireDate = txtIDExpDate.Text.ParseFormat();
                    ObjServices.SetIdentificationsContact(dataId);
                }

                if (!string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "-1")
                {
                    var HomeState = Utility.deserializeJSON<Utility.StateProvince>(ddlState.SelectedValue);
                    var DocRegHome = HomeState.DomesticregId;
                    var StateHome = HomeState.StateProvId;
                    var RegionID = HomeState.RegionId;

                    var address = dataAddress.FirstOrDefault(r => r.DirectoryTypeId == 5);

                    if (address != null)
                    {
                        address.CorpId = ObjServices.Corp_Id;
                        address.StreetAddress = txtAddress.Text;
                        address.CommunicationType = Utility.CommType.Address.ToString();
                        address.DirectoryTypeId = 5;
                        address.DomesticregId = DocRegHome;
                        address.RegionId = RegionID;
                        address.StateProvId = StateHome;
                        address.ContactId = ContactId;
                        address.CityId = ddlCity.ToInt();
                        address.CountryId = ddlCountry.ToInt();
                        address.ModifyUser = ObjServices.UserID.GetValueOrDefault();
                    }
                    else if (!string.IsNullOrEmpty(txtAddress.Text) && ddlCountry.SelectedValue != "-1" && ddlState.SelectedValue != "-1" && ddlCity.SelectedValue != "-1")
                    {
                        address = new Entity.UnderWriting.Entities.Contact.Address
                        {
                            //Key
                            CorpId = ObjServices.Corp_Id,
                            DirectoryId = -1,
                            DirectoryDetailId = -1,
                            DirectoryTypeId = 5,
                            ContactId = ContactId,
                            CommunicationType = Utility.CommType.Address.ToString(),

                            //Address Info
                            StreetAddress = txtAddress.Text,
                            RegionId = RegionID,
                            CountryId = ddlCountry.ToInt(),
                            DomesticregId = DocRegHome,
                            StateProvId = StateHome,
                            CityId = ddlCity.ToInt(),
                            ZipCode = null,
                            IsPrimary = true,
                            //User                            
                            CreateUser = ObjServices.UserID.GetValueOrDefault()
                        };
                    }

                    if (address != null)
                        ObjServices.oContactManager.SetAddress(address);
                }

                //Actualizar el campo de domiciliación
                ObjServices.oPolicyManager.SetPolicyDirectDebit(ObjServices.Corp_Id,
                                                                ObjServices.Region_Id,
                                                                ObjServices.Country_Id,
                                                                ObjServices.Domesticreg_Id,
                                                                ObjServices.State_Prov_Id,
                                                                ObjServices.City_Id,
                                                                ObjServices.Office_Id,
                                                                ObjServices.Case_Seq_No,
                                                                ObjServices.Hist_Seq_No,
                                                                chkDommiciliation.Checked,
                                                                chkInitialDomiciliation.Checked,
                                                                ObjServices.UserID.GetValueOrDefault()
                                                                );

                //Actualizar la flat table
                ObjServices.UpdateTempTable(ObjServices.Policy_Id, ObjServices.UserID.GetValueOrDefault());

                //Actualizar el perfil del cliente   
                var InsuredInformationUC = Utility.GetAllChildren(this.Page).FirstOrDefault(uc => uc is UCInsuredInformation);

                if (InsuredInformationUC != null)
                    (InsuredInformationUC as UCInsuredInformation).Initialize();

                //Actualizar el resumen
                var IllustrationInformationUC = Utility.GetAllChildren(this.Page).FirstOrDefault(uc => uc is UCIllustrationInformation);

                if (IllustrationInformationUC != null)
                    (IllustrationInformationUC as UCIllustrationInformation).FillData();

                if (ShowMessage)
                    this.MessageBox(Resources.SaveSucessfully);

                hdnDataSaveCumplimiento.Value = "true";

                FillData();

                if (this.IsCompany)
                {
                    //Refrescar el grid de los documentos   
                    var UcDocumennts = Utility.GetAllChildren(this.Page).FirstOrDefault(uc => uc is UCDocuments);

                    if (UcDocumennts != null)
                        (UcDocumennts as UCDocuments).FillData();
                }

            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }
        }

        public void edit()
        {

        }

        private void FillDrop()
        {
            ddlHomeOwner.Items.Clear();
            ddlHomeOwner.Items.AddRange(new ListItem[]{
                   new ListItem{Text = "----",Value="-1" },
                   new ListItem{Text = Resources.YesLabel,Value="S" },
                   new ListItem{Text = Resources.NoLabel,Value="N" }
                });

            var YearFrom = DateTime.Now.Year;
            var YearTo = DateTime.Now.AddYears(20).Year;

            ddlYear.Items.Clear();

            for (int i = YearFrom; i < YearTo; i++)
                ddlYear.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });

            ddlYear.Items.Insert(0, new ListItem { Text = "----", Value = "-1" });

            var dataDropCreditCardType = ObjServices.GettingDropData(Utility.DropDownType.CreditCard)
                                                    .Select(p => new
                                                    {
                                                        Text = p.CityDesc,
                                                        Value = Utility.serializeToJSON(new { CreditCardTypeId = p.AgentId, CreditCardMask = p.AgentName })
                                                    });


            ddlCreditCardType.DataSource = dataDropCreditCardType;
            ddlCreditCardType.DataTextField = "Text";
            ddlCreditCardType.DataValueField = "Value";
            ddlCreditCardType.DataBind();

            ddlCreditCardType.Items.Insert(0, new ListItem { Text = "----", Value = "-1" });

            //Llenar el dropDpown de  Marital status
            ObjServices.GettingAllDrops(ref ddlMaritalStatus,
                                    Utility.DropDownType.MaritalStatus,
                                    "MaritalStatusDesc", "MaritalStatId",
                                    GenerateItemSelect: true
                                   );

            ObjServices.GettingAllDrops(ref ddlRepresentativeIdentificationType,
                                    Utility.DropDownType.IdType,
                                    "ContactTypeIdDesc",
                                    "ContactTypeId",
                                    GenerateItemSelect: true,
                                    corpId: ObjServices.Corp_Id);

            ObjServices.GettingAllDrops(ref ddlCountry,
                                    Utility.DropDownType.CountryOfResidence,
                                    "GlobalCountryDesc",
                                    "CountryId",
                                    GenerateItemSelect: true
                                   );

            ObjServices.GettingAllDrops(ref ddlGender,
                                    Utility.DropDownType.Gender,
                                    "GenderDesc",
                                    "GenderId",
                                    GenerateItemSelect: true
                                   );

            ObjServices.GettingAllDrops(ref ddlNcfType,
                                    Utility.DropDownType.ContactInvoiceType,
                                    "DeductibleTypeDesc",
                                    "ProviderTransactionTypeId",
                                    GenerateItemSelect: true
                                   );


            //Llenar el dropDpown de Country Citizenship
            ObjServices.GettingAllDrops(ref ddlCountryCitizenship,
                                    Utility.DropDownType.Country,
                                    "GlobalCountryDesc",
                                    "CountryId",
                                    GenerateItemSelect: true
                                   );

            ObjServices.GettingAllDropsJSON(ref ddlBeneFinal,
                                       Utility.DropDownType.FinalBeneficiaryOption,
                                       "AgentName",
                                       GenerateItemSelect: true
                                       );

            ObjServices.GettingAllDropsJSON(ref ddlPep,
                                           Utility.DropDownType.PepFormularyOption,
                                           "AgentName",
                                           GenerateItemSelect: true
                                          );

            ObjServices.GettingAllDropsJSON(ref ddlAdminPepFormularyOptionsId,
                                           Utility.DropDownType.PepFormularyOption,
                                           "AgentName",
                                           GenerateItemSelect: true
                                          );

            ObjServices.GettingAllDrops(ref ddlActividad,
                                  Utility.DropDownType.CompanyActivity,
                                  "AgentName",
                                  "AgentId",
                                  GenerateItemSelect: true
                                 );

            ObjServices.GettingAllDrops(ref ddlEstructuraTitularidad,
                                Utility.DropDownType.CompanyStructure,
                                "AgentName",
                                "AgentId",
                                GenerateItemSelect: true
                               );

            ObjServices.GettingAllDrops(ref ddlTypeOfPerson,
                    Utility.DropDownType.TypeOfPerson,
                    "ElementDesc",
                    "ElementId",
                    GenerateItemSelect: true
                   );
        }

        public void FillData()
        {
            try
            {
                var isEffectivePolicy = ObjServices.StatusNameKey == Utility.IllustrationStatus.Effective.Code();

                var DefaultCountryId = "129";  //129 = Republica Dominicana
                //Obtener los datos del contacto
                var dataContact = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, ContactId, ObjServices.Language.ToInt());
                IsCompany = dataContact.IsCompany;

                pnPerson.Visible = !(IsCompany);
                pnCompany.Visible = (IsCompany);
                pnGender.Visible = pnPerson.Visible;
                hdnIsCompany.Value = IsCompany.ToString().ToLower();
                var ItemsSelectedByRemove = new List<ListItem>(0);
                ListItem findResult;

                //Datos de domiciliacion
                ddlYear.SelectIndexByValue(dataContact.ExpirationDateYear.ToString());
                ddlYear_SelectedIndexChanged(ddlYear, null);

                ddlMonth.SelectIndexByValue(dataContact.ExpirationDateMonth.ToString());
                txtCreaditCardNumberKey.Text = dataContact.CreditCardNumberKey;

                //End datos domiciliacion   
                if (dataContact.CreditCardTypeId.HasValue)
                {
                    var SelectedCreditCardTypeItem = Utility.serializeToJSON(new itemCreditCardType { CreditCardTypeId = dataContact.CreditCardTypeId.GetValueOrDefault(), CreditCardMask = dataContact.CreditCardMask });
                    ddlCreditCardType.SelectIndexByValueJSON(SelectedCreditCardTypeItem);
                    ddlCreditCardType_SelectedIndexChanged(ddlCreditCardType, null);
                }
                else
                    ddlCreditCardType.SelectedValue = "-1";

                txtCreditCardNumber.ReadOnly = true;
                txtCreditCardNumber.Attributes.Remove("data-mask");
                txtCreditCardNumber.Attributes.Add("style", "text-align: left;background-color: #f9f9b5;width: 46% !important;");

                var ShowCreditCardNumber = isEffectivePolicy && ObjServices.ViewCreditCardInformation;
                txtCreditCardNumber.Text = ShowCreditCardNumber ? Utility.Decrypt_Query(dataContact.CreditCardNumber) : dataContact.CreditCardNumberKey;

                if (dataContact.CreditCardTypeId.HasValue && (ShowCreditCardNumber))
                    try
                    {
                        txtCreditCardNumber.Attributes.Add("data-mask", SelectedCreditCardMask);
                    }
                    catch (Exception)
                    {

                    }

                txtCardHolder.Text = !string.IsNullOrEmpty(dataContact.CardHolder) ? dataContact.CardHolder : string.Empty;

                var dataIDtype = ObjServices.GettingDropData(Utility.DropDownType.IdType);

                if (!IsCompany)
                    dataIDtype = dataIDtype.Where(id => id.ContactTypeId != 5).ToList();
                else
                    dataIDtype = dataIDtype.Where(id => id.ContactTypeId == 5).ToList();

                if (dataIDtype != null)
                {
                    ddlIdType.DataSource = dataIDtype;
                    ddlIdType.DataTextField = "ContactTypeIdDesc";
                    ddlIdType.DataValueField = "ContactTypeId";
                    ddlIdType.DataBind();
                    ddlIdType.Items.Insert(0, new ListItem { Text = "----", Value = "-1" });
                }

                if (!IsCompany)
                {
                    findResult = ddlBeneFinal.Items.FindByValue("{\"CorpId\":1,\"CountryId\":1,\"AgentId\":2}");
                    if (findResult != null)
                        ItemsSelectedByRemove.Add(findResult);

                    findResult = ddlBeneFinal.Items.FindByValue("{\"CorpId\":0,\"CountryId\":1,\"AgentId\":5}");
                    if (findResult != null)
                        ItemsSelectedByRemove.Add(findResult);
                }
                else
                {
                    findResult = ddlBeneFinal.Items.FindByValue("{\"CorpId\":1,\"CountryId\":0,\"AgentId\":3}");
                    if (findResult != null)
                        ItemsSelectedByRemove.Add(findResult);

                    findResult = ddlBeneFinal.Items.FindByValue("{\"CorpId\":0,\"CountryId\":0,\"AgentId\":4}");
                    if (findResult != null)
                        ItemsSelectedByRemove.Add(findResult);
                }

                if (ItemsSelectedByRemove.Any())
                    foreach (var item in ItemsSelectedByRemove)
                        ddlBeneFinal.Items.Remove(item);

                //obtener los telefonos del contacto
                var dataPhones = ObjServices.oContactManager.GetCommunicatonPhone(ObjServices.Corp_Id, ContactId, ObjServices.Language.ToInt());
                //obtener los correos del contacto
                var dataEmail = ObjServices.oContactManager.GetCommunicatonEmail(ObjServices.Corp_Id, ContactId, ObjServices.Language.ToInt());

                if (dataContact != null)
                {
                    if (dataContact.PepFormularyAllowed.HasValue)
                    {
                        var itemJson = new { CorpId = dataContact.PepFormularyAllowed.ToString().ToLower() == "true" ? 1 : 0, AgentId = dataContact.pepFormularyOptionId };
                        var ValPep = Utility.serializeToJSON(itemJson);
                        ddlPep.SelectIndexByValue(ValPep);
                    }

                    if (dataContact.FinalBeneficiaryAllowed.HasValue)
                    {
                        var itemJson = new { CorpId = dataContact.FinalBeneficiaryAllowed.ToString().ToLower() == "true" ? 1 : 0, CountryId = dataContact.FinalBeneficiaryIncludeForCompanyOrNot.ToString().ToLower() == "true" ? 1 : 0, AgentId = dataContact.finalBeneficiaryOptionId };
                        var ValBenFinal = Utility.serializeToJSON(itemJson);
                        ddlBeneFinal.SelectIndexByValue(ValBenFinal);
                    }

                    ddlEstructuraTitularidad.SelectIndexByValue(dataContact.companyStructureId.ToString());
                    ddlActividad.SelectIndexByValue(dataContact.companyActivityId.ToString());

                    var ocumplimientoItem = new Utility.CumplimientoItem
                    {
                        companyActivityId = dataContact.companyActivityId,
                        companyStructureId = dataContact.companyStructureId,
                        finalBeneficiaryOptionId = dataContact.finalBeneficiaryOptionId,
                        pepFormularyOptionId = dataContact.pepFormularyOptionId
                    };

                    this.cumplimientoItem = ocumplimientoItem;

                    txtFirstName.Text = dataContact.FirstName;

                    txtCompanyName.Text = (!string.IsNullOrEmpty(dataContact.InstitutionalName)) ? dataContact.InstitutionalName : string.Concat(dataContact.FirstName, " ", dataContact.FirstLastName);

                    if (!string.IsNullOrEmpty(txtFirstName.Text))
                        txtFirstName.Attributes.Add("validation", "Required");
                    else
                        txtFirstName.Attributes.Remove("validation");

                    txtMiddleName.Text = dataContact.MiddleName;

                    //if (!string.IsNullOrEmpty(txtMiddleName.Text))
                    //    txtMiddleName.Attributes.Add("validation", "Required");
                    //else
                        txtMiddleName.Attributes.Remove("validation");

                    txtLastName.Text = dataContact.FirstLastName;

                    if (!string.IsNullOrEmpty(txtLastName.Text))
                        txtLastName.Attributes.Add("validation", "Required");
                    else
                        txtLastName.Attributes.Remove("validation");

                    txtSecondLastName.Text = dataContact.SecondLastName;

                    if (!string.IsNullOrEmpty(txtSecondLastName.Text))
                        txtSecondLastName.Attributes.Add("validation", "Required");
                    else
                        txtSecondLastName.Attributes.Remove("validation");

                    ddlGender.SelectedValue = dataContact.Gender == "N" ? "M" : dataContact.Gender; //N es compañia

                    if (ddlGender.SelectedValue != "-1")
                        ddlGender.Attributes.Add("validation", "Required");
                    else
                        ddlGender.Attributes.Remove("validation");

                    txtDob.Text = dataContact.Dob.HasValue ? dataContact.Dob.Value.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture) : string.Empty;

                    if (!string.IsNullOrEmpty(txtDob.Text))
                        txtDob.Attributes.Add("validation", "Required");
                    else
                        txtDob.Attributes.Remove("validation");

                    ddlNcfType.SelectedValue = dataContact.InvoiceTypeId.HasValue ? dataContact.InvoiceTypeId.ToString() : "-1";

                    if (ddlNcfType.SelectedValue != "-1")
                        ddlNcfType.Attributes.Add("validation", "Required");
                    else
                        ddlNcfType.Attributes.Remove("validation");

                    var GlobalCountry = ObjServices.oContactManager.GetContactCitizenshipByContact(dataContact.ContactId).FirstOrDefault();
                    if (GlobalCountry != null)
                    {
                        string GlobalCountryId = GlobalCountry.GlobalCountryId.ToString();
                        ddlCountryCitizenship.SelectIndexByValue(GlobalCountryId);
                    }

                    txtEmpresaLabora.Text = string.IsNullOrEmpty(dataContact.CompanyName) ? string.Empty : dataContact.CompanyName;
                    txtIngresoAnual.Text = dataContact.AnnualPersonalIncome.HasValue ? dataContact.AnnualPersonalIncome.Value.ToString("#0,0.00", CultureInfo.InvariantCulture)
                                                                                     : "0.00";
                    txtOccupation.Text = string.IsNullOrEmpty(dataContact.Occupation_Desc) ? string.Empty : dataContact.Occupation_Desc;
                    hdnOccupationId.Value = dataContact.OccupationId.HasValue ? dataContact.OccupationId.ToString() : string.Empty;
                    hdnOccupationGroupId.Value = dataContact.OccupGroupTypeId.HasValue ? dataContact.OccupGroupTypeId.ToString() : string.Empty;

                    txtRepresentativeName.Text = dataContact.Representative;

                    if (!string.IsNullOrEmpty(txtRepresentativeName.Text))
                        txtRepresentativeName.Attributes.Add("validation", "Required");
                    else
                        txtRepresentativeName.Attributes.Remove("validation");

                    txtRepresentativeIdentification.Text = dataContact.RepresentativeIdentification;

                    if (!string.IsNullOrEmpty(txtRepresentativeIdentification.Text))
                        txtRepresentativeIdentification.Attributes.Add("validation", "Required");
                    else
                        txtRepresentativeIdentification.Attributes.Remove("validation");

                    ddlRepresentativeIdentificationType.SelectIndexByValue(dataContact.RepresentativeIdentificationTypeId.ToString());

                    if (ddlRepresentativeIdentificationType.SelectedValue != "-1")
                        ddlRepresentativeIdentificationType.Attributes.Add("validation", "Required");
                    else
                        ddlRepresentativeIdentificationType.Attributes.Remove("validation");

                    ddlMaritalStatus.SelectIndexByValue(dataContact.MaritalStatId.ToString());
                    ddlHomeOwner.SelectIndexByValue(dataContact.homeOwner.HasValue ? dataContact.homeOwner.GetValueOrDefault() ? "S" : "N" : "-1");
                    txtDependencyCount.Text = dataContact.qtyPersonsDepend.HasValue ? dataContact.qtyPersonsDepend.GetValueOrDefault().ToString("#,0", CultureInfo.InvariantCulture)
                                                                                   : string.Empty;

                }

                if (dataPhones.Any())
                {
                    var DataCellPhone = dataPhones.FirstOrDefault(b => b.DirectoryTypeId == Utility.DirectoryType.CellPhone.ToInt());
                    var DataTelephone = dataPhones.FirstOrDefault(c => c.DirectoryTypeId == Utility.DirectoryType.HomePhone.ToInt());
                    var DataWorkPone = dataPhones.FirstOrDefault(w => w.DirectoryTypeId == Utility.DirectoryType.BusinessPhone.ToInt());
                    var DataFax = dataPhones.FirstOrDefault(w => w.DirectoryTypeId == Utility.DirectoryType.Fax.ToInt());

                    if (DataCellPhone != null)
                    {
                        txtCellPhone.Text = DataCellPhone.PhoneNumber;
                        if (!string.IsNullOrEmpty(txtCellPhone.Text))
                            txtCellPhone.Attributes.Add("validation", "Required");
                        else
                            txtCellPhone.Attributes.Remove("validation");
                    }

                    if (DataTelephone != null)
                    {
                        txtHomePhone.Text = DataTelephone.PhoneNumber;
                        if (!string.IsNullOrEmpty(txtHomePhone.Text))
                            txtHomePhone.Attributes.Add("validation", "Required");
                        else
                            txtHomePhone.Attributes.Remove("validation");
                    }

                    if (DataWorkPone != null)
                    {
                        txtWorkPhone.Text = DataWorkPone.PhoneNumber;
                        if (!string.IsNullOrEmpty(txtWorkPhone.Text))
                            txtWorkPhone.Attributes.Add("validation", "Required");
                        else
                            txtWorkPhone.Attributes.Remove("validation");
                    }

                    if (DataFax != null)
                    {
                        txtFax.Text = DataFax.PhoneNumber;
                        if (!string.IsNullOrEmpty(txtFax.Text))
                            txtFax.Attributes.Add("validation", "Required");
                        else
                            txtFax.Attributes.Remove("validation");
                    }
                }

                if (dataContact.MaritalStatId.HasValue)
                    ddlMaritalStatus.SelectIndexByValue(dataContact.MaritalStatId.ToString(), true);

                if (dataEmail.Any())
                {
                    var MainEmail = dataEmail.FirstOrDefault(b => b.IsPrimary).EmailAdress;
                    txtEmail.Text = MainEmail;

                    if (!string.IsNullOrEmpty(txtEmail.Text))
                        txtEmail.Attributes.Add("validation", "Required");
                    else
                        txtEmail.Attributes.Remove("validation");
                }

                var lstIdentification = ObjServices.oContactManager.GetAllIdDocumentInformation(ContactId, ObjServices.getCurrentLanguage());
                gvIdentitication.DatabindAspxGridView(lstIdentification);

                #region Esto solo aplica para republica dominicana
                if (lstIdentification != null && lstIdentification.Any())
                {
                    var dataId = lstIdentification.First();
                    txtIDNumber.Text = dataId.Id;

                    if (!string.IsNullOrEmpty(txtIDNumber.Text))
                        txtIDNumber.Attributes.Add("validation", "Required");
                    else
                        txtIDNumber.Attributes.Remove("validation");


                    ddlIdType.SelectIndexByValue(dataId.ContactIdType.ToString());

                    if (dataId.ContactIdType > 0)
                        ddlIdType.Attributes.Add("validation", "Required");
                    else
                        ddlIdType.Attributes.Remove("validation");


                    if (dataId.ContactIdType != Utility.IdentificationType.CompanyRegistration.ToInt())
                    {
                        if (dataId.ExpireDate.HasValue)
                        {
                            txtIDExpDate.Text = dataId.ExpireDate.Value.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture).ToUpper();

                            if (!string.IsNullOrEmpty(txtIDExpDate.Text))
                                txtIDExpDate.Attributes.Add("validation", "Required");
                            else
                                txtIDExpDate.Attributes.Remove("validation");
                        }
                    }

                    ddlIdType_SelectedIndexChanged(ddlIdType, null);
                }
                #endregion

                var dataAddress = ObjServices.oContactManager.GetCommunicatonAdress(ObjServices.Corp_Id, ContactId, ObjServices.Language.ToInt());

                if (dataAddress.Any())
                {
                    var homeAddress = dataAddress.Where(r => r.DirectoryTypeId == 5).OrderBy(r => r.DirectoryId).FirstOrDefault();

                    if (dataAddress.Any())

                        if (homeAddress != null)
                        {
                            txtAddress.Text = homeAddress.StreetAddress;

                            if (homeAddress.CountryId > 0)
                                ddlCountry.SelectIndexByValue(homeAddress.CountryId.ToString());
                            else
                                ddlCountry.SelectIndexByValue(DefaultCountryId);

                            ddlCountry_SelectedIndexChanged(ddlCountry, null);


                            //Set State and Fill Cities
                            var dbState = new Utility.StateProvince() { CorpId = homeAddress.CorpId, CountryId = homeAddress.CountryId, DomesticregId = homeAddress.DomesticregId, RegionId = homeAddress.RegionId, StateProvId = homeAddress.StateProvId };

                            var x = Utility.serializeToJSON(dbState);

                            ddlState.SelectIndexByValueJSON(x);

                            ddlState_SelectedIndexChanged(ddlState, null);


                            var dbMunicipaly = new Utility.Municipaly()
                            {
                                CorpId = homeAddress.CorpId,
                                RegionId = homeAddress.RegionId,
                                CountryId = homeAddress.CountryId,
                                DomesticregId = homeAddress.DomesticregId,
                                StateProvId = homeAddress.StateProvId,
                                CityId = homeAddress.MunicipioId.GetValueOrDefault()
                            };

                            var M = Utility.serializeToJSON(dbMunicipaly);
                            ddlMunicipality.SelectIndexByValueJSON(M);

                            ddlMunicipality_SelectedIndexChanged(ddlMunicipality, null);

                            //Set City
                            ddlCity.SelectIndexByValue(homeAddress.CityId.ToString());

                            if (!string.IsNullOrEmpty(txtAddress.Text))
                                txtAddress.Attributes.Add("validation", "Required");
                            else
                                txtAddress.Attributes.Remove("validation");
                        }
                }
                else
                {
                    ddlCountry.SelectIndexByValue(DefaultCountryId);
                    ddlCountry_SelectedIndexChanged(ddlCountry, null);
                }

                var illustrationData = ObjServices.oPolicyManager.GetQuotationInfoTemp(new Policy.Quo.Temp
                {
                    PolicyNo = ObjServices.Policy_Id
                }).FirstOrDefault();

                chkDommiciliation.Checked = illustrationData.DirectDebit.HasValue ? illustrationData.DirectDebit.Value : false;
                chkInitialDomiciliation.Checked = illustrationData.DomicileInitialPayment.GetValueOrDefault();

                #region Cumplimiento

                if (dataContact.TypeOfPerson != null)
                    ddlTypeOfPerson.SelectedValue = dataContact.TypeOfPerson.ToString();

                if (ddlTypeOfPerson.SelectedIndex <= 0)
                {
                    if (IsCompany)
                        ddlTypeOfPerson.SelectedValue = "2";
                    else
                        ddlTypeOfPerson.SelectedValue = "1";
                }

                ddlTypeOfPerson_SelectedIndexChanged(null, null);

                if (!string.IsNullOrEmpty(dataContact.PlaceOfBirth))
                    txtPlaceOfBirth.Text = dataContact.PlaceOfBirth;

                if (!string.IsNullOrEmpty(dataContact.ManagerName))
                {
                    txtManagerName.Text = dataContact.ManagerName;
                    pnAdminPepFormularyOptionsId.Visible = true;
                    ddlAdminPepFormularyOptionsId.SelectedIndex = dataContact.ManagerPepOptionId.ToInt();
                }
                else
                {
                    pnAdminPepFormularyOptionsId.Visible = false;
                    ddlAdminPepFormularyOptionsId.SelectedIndex = -1;
                }


                if (!string.IsNullOrEmpty(dataContact.WorkAddress))
                    txtWorkAddress.Text = dataContact.WorkAddress;

                if (ddlTypeOfPerson.SelectedIndex > 0)
                {
                    if (ddlTypeOfPerson.SelectedValue.ToInt() == 1 || ddlTypeOfPerson.SelectedValue.ToInt() == 3) // persona fisica
                    {   //RepresentativeIdentificationTypeDiv.Visible = false;
                        //RepresentativeIdentificationDiv.Visible = false;
                        //RepresentativeDiv.Visible = false;
                        DisableWorkingInformation(true);
                        ValidateFinalBeneficiaryIdentification(ddlTypeOfPerson.SelectedValue.ToInt());
                    }
                    else
                    {
                        DisableWorkingInformation(false);
                        ValidateFinalBeneficiaryIdentification(ddlTypeOfPerson.SelectedValue.ToInt());
                    }
                }

                var dataFinalBenficiary = ObjServices.oContactManager.GetContactFinalBeneficiary(this.ContactId).ToList();
                if (dataFinalBenficiary.Count > 0)
                {
                    ddlBeneFinal.SelectedIndex = 1;
                    var Peps = dataFinalBenficiary.Where(o => o.IsPEP == true).ToList();
                    if (Peps.Count > 0)
                    {
                        ddlPep.SelectedIndex = 2;
                    }
                }

                if (dataContact.ContactIdType.HasValue)
                {
                    ddlIdType.SelectIndexByValue(dataContact.ContactIdType.ToString());
                }

                ValidateWorkingInformation();

                ValidateFulfillmentInformation(dataFinalBenficiary.Count > 0 ? dataFinalBenficiary : null);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidateEditName()
        {
            var isValidClienteContactadoviaTelefonica = false;

            if (ObjServices.ProductLine == Utility.ProductLine.Auto)
            {
                IEnumerable<Policy.Vehicle.Requirement> Result = ObjServices.GetDataDocument();
                if (Result.Any())
                {
                    var Data = Result.FirstOrDefault(h => h.RequimentOnBaseNameKey == "ClienteContactadoVíaTelefonica");
                    if (Data != null)
                        isValidClienteContactadoviaTelefonica = Data.IsValid.GetValueOrDefault();
                }
            }
            else if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
            {
                //Obtener los documentos requeridos para incendio y lineas aliadas
                var dataReq = ObjServices.oRequirementManager.GetRequirementProduct(new Requirement.Product.Key
                {
                    CorpId = ObjServices.Corp_Id,
                    RegionId = ObjServices.Region_Id,
                    CountryId = ObjServices.Country_Id,
                    DomesticRegId = ObjServices.Domesticreg_Id,
                    StateProvId = ObjServices.State_Prov_Id,
                    CityId = ObjServices.City_Id,
                    OfficeId = ObjServices.Office_Id,
                    CaseSeqNo = ObjServices.Case_Seq_No,
                    HistSeqNo = ObjServices.Hist_Seq_No,
                    ContactId = null
                });

                var Data = dataReq.FirstOrDefault(h => h.RequimentOnBaseNameKey == "ClienteContactadoVíaTelefonica");
                if (Data != null)
                    isValidClienteContactadoviaTelefonica = Data.IsValid.GetValueOrDefault();
            }

            var msg = "No se puede modificar el este campo ya que se ha validado el documento de cliente contactado vía telefonica";

            txtFirstName.ReadOnly = isValidClienteContactadoviaTelefonica;
            txtMiddleName.ReadOnly = isValidClienteContactadoviaTelefonica;
            txtLastName.ReadOnly = isValidClienteContactadoviaTelefonica;
            txtSecondLastName.ReadOnly = isValidClienteContactadoviaTelefonica;

            if (!txtFirstName.CssClass.Contains("readOnlyCCVT"))
            {
                txtFirstName.CssClass = txtFirstName.ReadOnly ? string.Concat(txtFirstName.CssClass, "readOnlyCCVT") : txtFirstName.CssClass;
                txtFirstName.Attributes.Add("alt", txtFirstName.ReadOnly ? msg : string.Empty);
                txtMiddleName.CssClass = txtMiddleName.ReadOnly ? string.Concat(txtMiddleName.CssClass, "readOnlyCCVT") : txtMiddleName.CssClass;
                txtMiddleName.Attributes.Add("alt", txtMiddleName.ReadOnly ? msg : string.Empty);
                txtLastName.CssClass = txtLastName.ReadOnly ? string.Concat(txtLastName.CssClass, "readOnlyCCVT") : txtLastName.CssClass;
                txtLastName.Attributes.Add("alt", txtLastName.ReadOnly ? msg : string.Empty);
                txtSecondLastName.CssClass = txtSecondLastName.ReadOnly ? string.Concat(txtSecondLastName.CssClass, "readOnlyCCVT") : txtSecondLastName.CssClass;
                txtSecondLastName.Attributes.Add("alt", txtSecondLastName.ReadOnly ? msg : string.Empty);
            }
        }

        public void Initialize()
        {
            var illustrationData = ObjServices.getillustrationData();

            var isEffectivePolicy = ObjServices.StatusNameKey == Utility.IllustrationStatus.Effective.Code();
            ClearData();
            FillDrop();
            //ValidateEditName();
            FillData();
            VisiibilidadControles();
            //Validar que si es un producto de lineas aliadas y la prima es menor igual a 20,000.00 pesos bloquear el dropdown de los beneficiarios finales
            var dataPolicy = ObjServices.oPolicyManager.GetPolicy(ObjServices.Corp_Id, ObjServices.Region_Id, ObjServices.Country_Id, ObjServices.Domesticreg_Id, ObjServices.State_Prov_Id, ObjServices.City_Id, ObjServices.Office_Id, ObjServices.Case_Seq_No, ObjServices.Hist_Seq_No);
            var dataKey = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "AnnualPremiumValidationFBenef");
            var ValueToCompare = 20000.00m;

            if (dataKey != null)
                ValueToCompare = decimal.Parse(dataKey.ConfigurationValue, CultureInfo.InvariantCulture);

            var isEnable = !(dataPolicy.AnnualPremium.GetValueOrDefault() <= ValueToCompare);

            //ddlBeneFinal.Enabled = isEnable && this.IsCompany;
            pnDeseaDomiciliar.Visible = !illustrationData.Financed.GetValueOrDefault();
            pnIncluirInicial.Visible = !illustrationData.Financed.GetValueOrDefault();
            var TabSelected = (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), ObjServices.hdnQuotationTabs);
            pnGuardar.Visible = !isEffectivePolicy && (TabSelected != Utility.Tabs.lnkApprovedBySubscription && TabSelected != Utility.Tabs.lnkHistoricalIllustrations);
            lnkEditCreditCard.Visible = pnGuardar.Visible;
        }

        private void VisiibilidadControles()
        {
            txtDob.Enabled = !IsCompany && (ObjServices.IsSuscripcionQuotRole || ObjServices.isUserCot);
            pnDob.Visible = !IsCompany;
            pnExpDate.Visible = !IsCompany;
            pnGridIds.Visible = ObjServices.Country == Utility.Country.ElSalvador;
            pnID.Visible = ObjServices.Country == Utility.Country.RepublicaDominicana;
            btnVerBeneficiariosFinales.Visible = WUCFinalBeneficary.HasFinalBenefEx(this.ContactId).Any();
            //btnVerPEPS.Visible = WUCPEPForm.HasPEPEx(this.ContactId).Any();
            pnRepresentante.Visible = IsCompany;
            pnDependencyCount.Visible = !IsCompany;
            pnHomeOwner.Visible = !IsCompany;
            pnMaritalStatus.Visible = !IsCompany;
            pnqtyEmployee.Visible = IsCompany;
        }

        public void ClearData()
        {
            Utility.ClearAll(this.Controls);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            save(true);
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (sender as DropDownList);

            if (drop.Items.Count > 0 && drop.SelectedValue != "-1")
            {
                //Llenar el dropdown de HomeState
                ObjServices.GettingAllDrops(ref ddlState,
                                            Utility.DropDownType.StateProvince,
                                           "StateProvDesc",
                                           "StateProvId",
                                            corpId: ObjServices.Corp_Id,
                                            countryId: drop.ToInt(),
                                            GenerateItemSelect: true
                                           );

                //ddlStateSelectedIndexChanged(TargetDrop, null);
            }
            //else
            //    TargetDrop.Items.Clear();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (sender as DropDownList);

            if (drop.Items.Count > 0 && drop.SelectedValue != "-1")
            {
                var KeyStateProvince = Utility.deserializeJSON<Utility.StateProvince>(drop.SelectedValue);

                if (drop.SelectedIndex > 0)
                {
                    //Llenar el dropdown de HomeState
                    ObjServices.GettingAllDropsJSON(ref ddlMunicipality,
                                                    Utility.DropDownType.Municipio,
                                                    "StateProvDesc",
                                                    stateProvId: KeyStateProvince.StateProvId,
                                                    domesticregId: KeyStateProvince.DomesticregId,
                                                    countryId: KeyStateProvince.CountryId
                                                    );
                }
            }
            else
            {
                // TargetDrop.Items.Clear();

                //if (drop == ddl_WUC_A_BusinessState)
                //    ddl_WUC_A_BusinessCity.Items.Clear();
                //else ddl_WUC_A_HomeCity.Items.Clear();
            }
        }

        private void EditMode(bool isEdit, DevExpress.Web.ASPxGridView grid, int VisibleIndex)
        {
            dropIdType = grid.FindRowCellTemplateControl(VisibleIndex, null, "dropIdType") as DropDownList;
            var ltTypeId = grid.FindRowCellTemplateControl(VisibleIndex, null, "ltTypeId") as Control;

            txtId = grid.FindRowCellTemplateControl(VisibleIndex, null, "txtId") as TextBox;
            var ltId = grid.FindRowCellTemplateControl(VisibleIndex, null, "ltId") as Control;

            txtExpDate = grid.FindRowCellTemplateControl(VisibleIndex, null, "txtExpDate") as TextBox;
            var ltExpDate = grid.FindRowCellTemplateControl(VisibleIndex, null, "ltExpDate") as Control;

            if (dropIdType != null && isEdit)
            {
                dropIdType.DataSource = ObjServices.GettingDropData(Utility.DropDownType.IdType);
                dropIdType.DataTextField = "ContactTypeIdDesc";
                dropIdType.DataValueField = "ContactTypeId";
                dropIdType.DataBind();

                //Seleccionar el ID que se registro
                var ContactIdType = grid.GetKeyFromAspxGridView("ContactIdType", VisibleIndex).ToString();
                dropIdType.SelectIndexByValue(ContactIdType);
            }

            if (dropIdType != null) dropIdType.Visible = isEdit;

            if (ltTypeId != null) ltTypeId.Visible = !isEdit;

            if (txtId != null) txtId.Visible = isEdit;
            if (ltId != null) ltId.Visible = !isEdit;

            if (txtExpDate != null) txtExpDate.Visible = isEdit;
            if (ltExpDate != null) ltExpDate.Visible = !isEdit;

            udpIds.Update();
        }

        private void SetControls(DevExpress.Web.ASPxGridView grid, int VisibleIndex)
        {
            dropIdType = grid.FindRowCellTemplateControl(VisibleIndex, null, "dropIdType") as DropDownList;
        }

        protected void gvIdentitication_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            var grid = sender as DevExpress.Web.ASPxGridView;
            var Command = e.CommandArgs.CommandName;

            var btnEditOrSave = grid.FindRowCellTemplateControl(e.VisibleIndex, null, "btnEditOrSave") as LinkButton;
            var btnCancel = grid.FindRowCellTemplateControl(e.VisibleIndex, null, "btnCancel") as LinkButton;

            var SeqNo = grid.GetKeyFromAspxGridView("SeqNo", e.VisibleIndex).ToInt();

            switch (Command)
            {
                case "Cancel":
                    if (btnCancel != null) btnCancel.Visible = false;
                    if (btnEditOrSave != null) btnEditOrSave.CssClass = "myedit_file";
                    EditMode(false, grid, e.VisibleIndex);
                    btnEditOrSave.CommandName = "Edit";
                    break;
                case "Edit":
                    if (btnCancel != null) btnCancel.Visible = true;
                    if (btnEditOrSave != null) btnEditOrSave.CssClass = "mysave_file";
                    btnEditOrSave.CommandName = "Save";
                    EditMode(true, grid, e.VisibleIndex);
                    break;
                case "Save":
                    btnEditOrSave.CommandName = "Edit";
                    if (btnCancel != null) btnCancel.Visible = false;
                    if (btnEditOrSave != null) btnEditOrSave.CssClass = "myedit_file";
                    EditMode(false, grid, e.VisibleIndex);
                    SetControls(grid, e.VisibleIndex);

                    var parameter = new Entity.UnderWriting.Entities.Contact.IdDocument()
                    {
                        //Key
                        ContactId = this.ContactId,
                        SeqNo = SeqNo,
                        //Campos 
                        ContactIdType = !string.IsNullOrEmpty(dropIdType.SelectedValue) ? int.Parse(dropIdType.SelectedValue) : int.Parse("0"),
                        ContactIdTypeDescription = dropIdType.SelectedItem.Text,
                        Id = txtId.Text,
                        ExpireDate = !string.IsNullOrEmpty(txtExpDate.Text) ? txtExpDate.Text.ParseFormat() : (DateTime?)null,
                        IssuedBy = null,
                        MainIdentity = true,
                        UserId = ObjServices.UserID.GetValueOrDefault()
                    };

                    ObjServices.SetIdentificationsContact(parameter);

                    FillData();

                    break;
            }

        }

        protected void gvIdentitication_PreRender(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxGridView).TranslateColumnsAspxGrid();
        }

        protected void ddlPep_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Drop = ((sender != null ? sender : (object)ddlPep) as DropDownList);

            if (Drop.SelectedValue == "-1")
                return;

            ValidateWorkingInformation();
            //ValidateFulfillmentInformation();

            var ItemSelectedNo = "{\"CorpId\":0,\"AgentId\":3}";
            if (ddlPep.SelectedItem.Text.ToLower().Contains("designado"))
            {
                if (ddlTypeOfPerson.SelectedIndex > 0)
                {
                    var typeOfPerson = ddlTypeOfPerson.SelectedValue.ToInt();
                    WUCPEPForm.isDesigned = true;

                    if (typeOfPerson == 1 || typeOfPerson == 3) //persona fisica
                    {
                        WUCPEPForm.DesignedFullName = string.Concat(txtFirstName.Text, !string.IsNullOrEmpty(txtSecondLastName.Text) ? " " + txtMiddleName.Text : "", " ", txtLastName.Text, !string.IsNullOrEmpty(txtSecondLastName.Text) ? " " + txtSecondLastName.Text : "");
                    }
                    else
                    {
                        WUCPEPForm.DesignedFullName = txtFirstName.Text;
                    }
                }
            }
            else
            {
                WUCPEPForm.isDesigned = false;
                WUCPEPForm.DesignedFullName = string.Empty;
            }


            if (ddlPep.SelectedItem.Text.ToLower().Contains("si"))
            {
                var ItemSelected = Utility.deserializeJSON<itemPepFormularyOption>(Drop.SelectedValue);

                if (ItemSelected.CorpId == 1)
                {
                    //Mostrar popup con el formulario PEP
                    hdnShowPepPop.Value = "true";
                    this.cumplimientoItem.pepFormularyOptionId = ItemSelected.AgentId;
                    WUCPEPForm.SelectedcumplimientoItem = this.cumplimientoItem;
                    WUCPEPForm.ContactId = this.ContactId;
                    WUCPEPForm.Initialize();
                    btnVerPEPS.Visible = WUCPEPForm.HasPEP || (ItemSelected.CorpId == 1);
                    //mpePepPop.Show();
                }
            }
        }

        protected void btnVerPEPS_Click(object sender, EventArgs e)
        {
            WUCPEPForm.hdnOrigen = "CalidadPep";
            //Mostrar popup con el formulario PEP
            hdnShowPepPop.Value = "true";
            WUCPEPForm.SelectedcumplimientoItem = this.cumplimientoItem;
            WUCPEPForm.ContactId = this.ContactId;
            WUCPEPForm.Initialize();
            mpePepPop.Show();
        }

        protected void ddlBeneFinal_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Drop = (sender as DropDownList);

            if (Drop.SelectedValue == "-1")
                return;

            ValidateFulfillmentInformation();

            var ItemSelected = Utility.deserializeJSON<itemFinalBeneficiary>(Drop.SelectedValue);

            if (ItemSelected.CorpId == 1)
            {
                //Mostrar popup con los beneficiarios finales
                hdnShowFinalBenef.Value = "true";
                WUCFinalBeneficary.ContactId = this.ContactId;
                WUCFinalBeneficary.Initialize();
                WUCFinalBeneficary.IsCompany = this.IsCompany;
                WUCFinalBeneficary.ClickButtonScript = "javascript:__doPostBack('ctl00$bodyContent$ucInsuredInformation$UCContactEditForm$WUCFinalBeneficary$gvFinalBeneficiary$cell{0}_0$TC$btnEditOrSave','')";
                btnVerBeneficiariosFinales.Visible = WUCFinalBeneficary.HasFinalBenef || (ItemSelected.CorpId == 1);
                mpeFinalBeneficiary.Show();
            }
            else
            {
                btnVerBeneficiariosFinales.Visible = false;

                if (ItemSelected.AgentId == 5) //No tiene beneficiario
                {
                    var dataFinalBenficiary = ObjServices.oContactManager.GetContactFinalBeneficiary(this.ContactId).ToList();
                    if (dataFinalBenficiary.Count > 0)
                    {
                        foreach (var Record in dataFinalBenficiary)
                        {
                            var result = ObjServices.oContactManager.SetContactFinalBeneficiary(this.ContactId,
                                                                              Record.FinalBeneficiaryId,
                                                                              Record.Name,
                                                                              Record.PercentageParticipation,
                                                                              false,
                                                                              ObjServices.UserID.GetValueOrDefault(),
                                                                              Record.IsPEP,
                                                                              Record.PepFormularyOptionsId,
                                                                              null,
                                                                              null,
                                                                              null
                                                                             );
                        }


                        var Peps = dataFinalBenficiary.Where(o => o.IsPEP == true).ToList();
                        if (Peps.Any())
                        {
                            //busco los peps que tenga ese contacto
                            var DataPeps = ObjServices.oContactManager.GetContactPEPFormulary(this.ContactId, "CalidadPep").ToList();
                            if (DataPeps.Any())
                            {
                                foreach (var item in Peps)
                                {
                                    var PepToDelete = DataPeps.Where(p => p.BeneficiaryId == item.FinalBeneficiaryId).ToList(); //busco los datos del pep asosciado a ese beneficiaryId
                                    if (PepToDelete.Any())
                                    {
                                        ObjServices.oContactManager.SetContactPepFormulary(this.ContactId,
                                                                                            PepToDelete[0].PepFormularyId,
                                                                                            PepToDelete[0].Name,
                                                                                            PepToDelete[0].RelationshipId,
                                                                                            PepToDelete[0].Position,
                                                                                            PepToDelete[0].FromYear.ToInt(),
                                                                                            PepToDelete[0].ToYear.ToInt(),
                                                                                            false,
                                                                                            ObjServices.UserID.GetValueOrDefault(),
                                                                                            PepToDelete[0].BeneficiaryId,
                                                                                            PepToDelete[0].IsPepManagerCompany
                                                                                            );
                                    }
                                }

                                if (ddlPep.SelectedValue != "-1")
                                {
                                    var ItemPepSelected = Utility.deserializeJSON<itemPepFormularyOption>(ddlPep.SelectedValue);
                                    if (ItemPepSelected.AgentId == 3)
                                    {
                                        btnVerPEPS.Visible = false;
                                    }
                                }
                            }

                        }
                    }


                }
            }
        }

        protected void btnVerBeneficiariosFinales_Click(object sender, EventArgs e)
        {
            hdnShowFinalBenef.Value = "true";
            WUCFinalBeneficary.ContactId = this.ContactId;
            WUCFinalBeneficary.Initialize();
            WUCFinalBeneficary.IsCompany = this.IsCompany;
            WUCFinalBeneficary.ClickButtonScript = "javascript:__doPostBack('ctl00$bodyContent$ucInsuredInformation$UCContactEditForm$WUCFinalBeneficary$gvFinalBeneficiary$cell{0}_0$TC$btnEditOrSave','')";
            mpeFinalBeneficiary.Show();
        }

        protected void ddlIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var TypeOfPerson = ddlTypeOfPerson.SelectedValue.ToInt();
            if (ddlTypeOfPerson.SelectedIndex > 0)
            {
                if (TypeOfPerson == 1 || TypeOfPerson == 3)
                {
                    this.IsCompany = false;
                    hdnIsCompany.Value = this.IsCompany.ToString().ToLower();
                    ddlMaritalStatus.Attributes.Add("validation", "Required");
                }
                else
                {
                    this.IsCompany = true;
                    hdnIsCompany.Value = this.IsCompany.ToString().ToLower();
                    ddlPep.Attributes.Remove("validation");
                    ddlMaritalStatus.Attributes.Remove("validation");
                }
            }


            VisiibilidadControles();
        }

        protected void btnValidateCumplimiento_Click(object sender, EventArgs e)
        {
            //Validar Cumplimiento
            if (this.IsCompany)
            {
                if (ddlActividad.SelectedValue == "-1")
                {
                    this.MessageBox("El campo Actividad es requerido");
                    return;
                }

                if (ddlEstructuraTitularidad.SelectedValue == "-1")
                {
                    this.MessageBox("El campo Estructura de titularidad es requerido");
                    return;
                }
            }
            else
            {
                var itemSelectedNo = "{\"CorpId\":0,\"AgentId\":3}";

                if (ddlPep.SelectedValue == "-1")
                {
                    this.MessageBox("El campo posee calida de PEP es requerido");
                    return;
                }
                else if (ddlPep.SelectedValue != itemSelectedNo)
                {

                    var hasPEP = WUCPEPForm.HasPEPEx(this.ContactId).Any();

                    if (!hasPEP)
                    {
                        this.MessageBox("Debes Indicar el o los PEP");
                        return;
                    }
                }
            }

            var ItemSelectedSi = "{\"CorpId\":1,\"CountryId\":0,\"AgentId\":3}";

            if (ddlBeneFinal.SelectedValue == "-1")
            {
                this.MessageBox("El campo beneficiario final es requerido");
                return;
            }
            else if (ddlBeneFinal.SelectedValue == ItemSelectedSi)
            {
                //Verificar que tenga los beneiciarios finales al menos uno de ellos
                var HasBen = WUCFinalBeneficary.HasFinalBenefEx(this.ContactId).Any();

                if (!HasBen)
                {
                    this.MessageBox("Debes Indicar el o los Beneficiarios Finales");
                    return;
                }
            }

            save();

            this.ExcecuteJScript("closePopEditContact();");
        }

        protected void ddlMunicipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (sender as DropDownList);

            if (drop.Items.Count > 0 && drop.SelectedValue != "-1")
            {
                var KeyStateProvince = Utility.deserializeJSON<Utility.Municipaly>(drop.SelectedValue);

                if (drop.SelectedIndex > 0)
                {
                    //Llenar el dropdown de HomeState
                    ObjServices.GettingAllDrops(ref ddlCity,
                                            Utility.DropDownType.City,
                                            "CityDesc",
                                            "CityId",
                                            stateProvId: KeyStateProvince.StateProvId,
                                            domesticregId: KeyStateProvince.DomesticregId,
                                            countryId: KeyStateProvince.CountryId,
                                            cityId: KeyStateProvince.CityId,
                                            GenerateItemSelect: true
                                           );
                }
            }
            else
            {
                // TargetDrop.Items.Clear();

                //if (drop == ddl_WUC_A_BusinessState)
                //    ddl_WUC_A_BusinessCity.Items.Clear();
                //else ddl_WUC_A_HomeCity.Items.Clear();
            }
        }

        protected void ddlCreditCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Drop = (sender as DropDownList);
            var SelectedValidItem = Drop.SelectedValue != "-1";

            if (SelectedValidItem)
            {
                var oitemCreditCardType = Utility.deserializeJSON<itemCreditCardType>(Drop.SelectedValue);
                var CreditCardMask = oitemCreditCardType.CreditCardMask;
                SelectedCreditCardMask = CreditCardMask;
            }
            else
            {
                txtCreditCardNumber.Clear();
                ddlYear.SelectedValue = "-1";
                ddlYear_SelectedIndexChanged(ddlYear, null);
                txtCardHolder.Clear();
            }

            //Hacer que los campos de domiciliacion sean obligatorios o no
            if (SelectedValidItem)
                txtCreditCardNumber.Attributes.Add("validation", "Required");
            else
                txtCreditCardNumber.Attributes.Remove("validation");

            if (SelectedValidItem)
                ddlYear.Attributes.Add("validation", "Required");
            else
                ddlYear.Attributes.Remove("validation");

            if (SelectedValidItem)
                ddlMonth.Attributes.Add("validation", "Required");
            else
                ddlMonth.Attributes.Remove("validation");

            if (SelectedValidItem)
                txtCardHolder.Attributes.Add("validation", "Required");
            else
                txtCardHolder.Attributes.Remove("validation");

        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (sender as DropDownList);
            var dropIsEnabled = drop.SelectedValue != "-1";
            ddlMonth.Enabled = dropIsEnabled;

            if (dropIsEnabled)
            {
                var YearSelected = drop.SelectedValue.ToInt();
                var InitialMonth = DateTime.Now.Month;
                //Si el año es el año en curso cargar el dropdown de los meses a partir del mes actual
                InitialMonth = (YearSelected == DateTime.Now.Year) ? DateTime.Now.Month : 1;

                ddlMonth.Items.Clear();

                for (int i = InitialMonth; i <= 12; i++)
                    ddlMonth.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });

                ddlMonth.Items.Insert(0, new ListItem { Text = "----", Value = "-1" });
            }

            if (drop.SelectedValue == "-1")
                ddlMonth.SelectedValue = "-1";
        }

        protected void lnkEditCreditCard_Click(object sender, EventArgs e)
        {
            if (txtCreditCardNumber.ReadOnly)
            {
                //Habilitar el campo para que puedan escribir en el
                txtCreditCardNumber.ReadOnly = false;
                //Remover el style y poner el nuevo style
                txtCreditCardNumber.Attributes.Remove("style");
                txtCreditCardNumber.Attributes.Add("style", "text-align: left;background-color:#ffffff;width: 46% !important;");
                //Asignarle la mascara correspondiente segun sea el tipo de tarjeta
                txtCreditCardNumber.Attributes.Add("data-mask", this.SelectedCreditCardMask);
                txtCreditCardNumber.Clear();
                txtCreditCardNumber.Focus();
            }
            else
            {
                txtCreditCardNumber.ReadOnly = true;
                txtCreditCardNumber.Attributes.Remove("style");
                txtCreditCardNumber.Attributes.Add("style", "text-align: left;background-color:#f9f9b5;width: 46% !important;");
                FillData();
            }
        }

        protected void chkDommiciliation_CheckedChanged(object sender, EventArgs e)
        {
            var RequiredFields = (sender as CheckBox).Checked;

            //Hacer que los campos de domiciliacion sean obligatorios o no
            if (RequiredFields)
                txtCreditCardNumber.Attributes.Add("validation", "Required");
            else
                txtCreditCardNumber.Attributes.Remove("validation");

            if (RequiredFields)
                ddlYear.Attributes.Add("validation", "Required");
            else
                ddlYear.Attributes.Remove("validation");

            if (RequiredFields)
                ddlMonth.Attributes.Add("validation", "Required");
            else
                ddlMonth.Attributes.Remove("validation");

            if (RequiredFields)
                txtCardHolder.Attributes.Add("validation", "Required");
            else
                txtCardHolder.Attributes.Remove("validation");
        }

        protected void lnkDomiciliarPago_Click(object sender, EventArgs e)
        {
            try
            {
                save(false);
                //Objeto de la Data de la Poliza
                var PolicyData = ObjServices.oPolicyManager.GetPolicy(ObjServices.Corp_Id, ObjServices.Region_Id, ObjServices.Country_Id, ObjServices.Domesticreg_Id, ObjServices.State_Prov_Id, ObjServices.City_Id
                 , ObjServices.Office_Id, ObjServices.Case_Seq_No, ObjServices.Hist_Seq_No);

                var illustrationData = ObjServices.getillustrationData();

                //Objeto de la data del Contacto
                var ContactData = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, PolicyData.ContactId, ObjServices.Language.ToInt());

                var isEffectivePolicy = ObjServices.StatusNameKey == Utility.IllustrationStatus.Effective.Code();
                if (!isEffectivePolicy)
                {
                    this.MessageBox("Para poder domiliciliar el pago esta cotización debe ser una poliza efectiva");
                    return;
                }

                if (illustrationData.DirectDebit.HasValue)
                {
                    if (illustrationData.DirectDebit.Value)
                    {
                        if (!ContactData.CreditCardTypeId.HasValue || string.IsNullOrEmpty(ContactData.CreditCardNumber) || string.IsNullOrEmpty(ContactData.CardHolder))
                        {
                            this.MessageBox("Se marco esta póliza para domiciliar el pago, sin embargo el sistema no tiene los datos de la tarjeta de credito");
                            return;
                        }
                    }
                }

                //Buscar la data del acuerdo de pago  en global
                var dataResultPaymentAgreement = ObjServices.oPaymentManager.GetPaymentAgreement(new Entity.UnderWriting.Entities.Payment.Agreement
                   {
                       CorpId = ObjServices.Corp_Id,
                       RegionId = ObjServices.Region_Id,
                       CountryId = ObjServices.Country_Id,
                       DomesticregId = ObjServices.Domesticreg_Id,
                       StateProvId = ObjServices.State_Prov_Id,
                       CityId = ObjServices.City_Id,
                       OfficeId = ObjServices.Office_Id,
                       CaseSeqNo = ObjServices.Case_Seq_No,
                       HistSeqNo = ObjServices.Hist_Seq_No
                   });

                decimal Cotizacion = ObjServices.oPolicyManager.GetQuotFromSysFlex(PolicyData.PolicyNo).GetValueOrDefault();
                var result = ObjServices.Domiciliation(ContactData, dataResultPaymentAgreement, 30, Cotizacion, PolicyData.PolicyEffectiveDate, PolicyData.PolicyNo, ObjServices.UserName, illustrationData.DomicileInitialPayment.GetValueOrDefault());
                if (result.oReturn.Message != "Exito")
                {
                    var msg = string.Format("Error domiciliando el pago en Gp  \n\n detalle del error: {0}", result.Message);
                    throw new Exception(msg);
                }
                else
                    this.MessageBox("La domiciliación se ha generado con exito");
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }
        }

        protected void lnkDomiciliarPago_PreRender(object sender, EventArgs e)
        {
            var isEffectivePolicy = ObjServices.StatusNameKey == Utility.IllustrationStatus.Effective.Code();
            (sender as LinkButton).Visible = isEffectivePolicy && (ObjServices.isUserCot || ObjServices.CanDomitiliationPayment);
        }

        #region Validaciones de Cumplimiento

        public void ValidateWorkingInformation()
        {
            var Drop = ddlPep;

            if (Drop.SelectedValue == "-1")
                return;
            var TotalPremiunValidation = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "TotalPremiumValidation").ConfigurationValue.ToDecimal();
            var TotalPremiunQuotation = ObjServices.annualPremium.ToDecimal();

            //var ItemSelectedNo = "{\"CorpId\":0,\"AgentId\":3}";

            var Peps = Utility.deserializeJSON<itemPepFormularyOption>(Drop.SelectedValue);
            var TypeOfPerson = ddlTypeOfPerson.SelectedValue.ToInt();
            if (TypeOfPerson == 1 || TypeOfPerson == 3) // persona fisica
            {
                txtManagerName.Text = string.Empty;
                txtManagerName.Attributes.Remove("validation");

                txtOccupation.Enabled = true;
                txtEmpresaLabora.Enabled = true;
                txtIngresoAnual.Enabled = true;

                if (Peps != null && Peps.AgentId != 3)
                {
                    // si el cliente tiene calidad pep, se solicita toda la informacion laboral 
                    if (string.IsNullOrEmpty(txtWorkAddress.Text.Trim()))
                        txtWorkAddress.Attributes.Add("validation", "Required");


                    divWorkAdress.Visible = true;

                    if (string.IsNullOrEmpty(txtWorkPhone.Text.Trim()))
                        txtWorkPhone.Attributes.Add("validation", "Required");

                    divWorkPhone.Visible = true;
                    txtWorkPhone.Visible = true;
                    txtWorkPhone.Enabled = true;

                    if (string.IsNullOrEmpty(txtOccupation.Text))
                        txtOccupation.Attributes.Add("validation", "Required");

                    if (string.IsNullOrEmpty(txtEmpresaLabora.Text.Trim()))
                        txtEmpresaLabora.Attributes.Add("validation", "Required");
                    //else
                    //    txtEmpresaLabora.Attributes.Remove("validation");

                    var AnnualIncome = string.IsNullOrEmpty(txtIngresoAnual.Text.Trim()) ? 0 : Convert.ToDouble(txtIngresoAnual.Text.Trim().Replace(",", ""));

                    if (AnnualIncome <= 0)
                        txtIngresoAnual.Attributes.Add("validation", "Required");
                    //else
                    //    txtIngresoAnual.Attributes.Remove("validation");

                }
                else if (TotalPremiunQuotation <= TotalPremiunValidation)
                {
                    txtWorkAddress.Text = string.Empty;
                    txtWorkAddress.Attributes.Remove("validation");
                    divWorkAdress.Visible = false;

                    txtWorkPhone.Text = string.Empty;
                    txtWorkPhone.Attributes.Remove("validation");
                    txtWorkPhone.Visible = false;

                    if (string.IsNullOrEmpty(txtOccupation.Text))
                        txtOccupation.Attributes.Add("validation", "Required");
                    //else
                    //    txtOccupation.Attributes.Remove("validation");

                    if (string.IsNullOrEmpty(txtEmpresaLabora.Text.Trim()))
                        txtEmpresaLabora.Attributes.Add("validation", "Required");
                    //else
                    //    txtEmpresaLabora.Attributes.Remove("validation");

                    txtIngresoAnual.Attributes.Remove("validation");
                }
                else
                {

                    //if (string.IsNullOrEmpty(txtOccupation.Text))
                    txtOccupation.Attributes.Add("validation", "Required");
                    //else
                    //    txtOccupation.Attributes.Remove("validation");

                    //if (string.IsNullOrEmpty(txtEmpresaLabora.Text.Trim()))
                    txtEmpresaLabora.Attributes.Add("validation", "Required");
                    //else
                    //    txtEmpresaLabora.Attributes.Remove("validation");

                    //if (string.IsNullOrEmpty(txtWorkAddress.Text.Trim()))
                    txtWorkAddress.Attributes.Add("validation", "Required");
                    //else
                    //    txtWorkAddress.Attributes.Remove("validation");

                    //if (string.IsNullOrEmpty(txtWorkPhone.Text.Trim()))
                    txtWorkPhone.Attributes.Add("validation", "Required");
                    //else
                    //    txtWorkPhone.Attributes.Remove("validation");

                    var AnnualIncome = string.IsNullOrEmpty(txtIngresoAnual.Text.Trim()) ? 0 : Convert.ToDouble(txtIngresoAnual.Text.Trim().Replace(",", ""));

                    //if (AnnualIncome <= 0)
                    txtIngresoAnual.Attributes.Add("validation", "Required");
                    //else
                    //    txtIngresoAnual.Attributes.Remove("validation");


                    divWorkAdress.Visible = true;
                    divWorkPhone.Visible = true;
                }
            }
            else // personas juridicas
            {
                txtWorkAddress.Text = string.Empty;
                txtWorkAddress.Attributes.Remove("validation");
                divWorkAdress.Visible = false;

                txtWorkPhone.Text = string.Empty;
                txtWorkPhone.Attributes.Remove("validation");
                divWorkPhone.Visible = false;

                txtOccupation.Text = string.Empty;
                hdnOccupationId.Value = string.Empty;
                txtOccupation.Enabled = false;

                txtEmpresaLabora.Text = string.Empty; ;
                txtEmpresaLabora.Attributes.Remove("validation");
                txtEmpresaLabora.Enabled = false;

                txtIngresoAnual.Text = "0";
                txtIngresoAnual.Attributes.Remove("validation");
                txtIngresoAnual.Enabled = false;
            }
        }

        public void ValidateFulfillmentInformation(IEnumerable<Entity.UnderWriting.Entities.Contact.FinalBeneficiary.FinalBenResult> beneficiarios = null)
        {
            //var linkbutonPepMaster = this.Page.Master.FindControl("divBtnShowPep");
            ////if (ddlPepOptions.SelectedItem.Text.ToLower().Contains("si"))
            ////{
            ////    if (linkbutonPepMaster != null)
            ////        (linkbutonPepMaster as Panel).Visible = true;
            ////    UCPepFormulary.hdnOrigen = "CalidadPep";
            ////}
            if (ddlTypeOfPerson.SelectedIndex <= 0)
                return;

            var benef = Utility.deserializeJSON<itemFinalBeneficiary>(ddlBeneFinal.SelectedValue);

            var TypeOfPerson = ddlTypeOfPerson.SelectedValue.ToInt();
            if (TypeOfPerson != 1 && TypeOfPerson != 3 && TypeOfPerson != 5 && TypeOfPerson != 6) // persona juridica
            {
                ddlBeneFinal.Attributes.Add("validation", "Required");
                ddlBeneFinal.Enabled = true;

                ddlActividad.Attributes.Add("validation", "Required");
                ddlEstructuraTitularidad.Attributes.Add("validation", "Required");
                ddlActividad.Enabled = true;
                ddlEstructuraTitularidad.Enabled = true;

                ddlPep.Enabled = false;
                ddlPep.Attributes.Remove("validation");
                ddlPep.Visible = true;
                ddlPep.SelectedIndex = 3;


                #region ddlBeneficiarioFinalOptionCompany
                if (ddlBeneFinal.SelectedIndex <= 0)
                {
                    //divBFCompanyOptions.Visible = true;
                    //ddlBeneFinal.Enabled = true;
                    ddlBeneFinal.Attributes.Add("validation", "Required");
                    //ddlBeneFinal.Visible = false;
                    //ddlAdminPepFormularyOptionsId.SelectedIndex = -1;
                    //ddlAdminPepFormularyOptionsId.Attributes.Remove("validation");
                    //ddlBeneficiarioFinalOptions.SelectedIndex = 0;
                    //ddlBeneficiarioFinalOptions.Attributes.Remove("validation");
                    //$('#AdminDisplayPepForm').hide();

                }
                else
                {
                    ddlBeneFinal.Attributes.Remove("validation");
                    if (benef != null && benef.AgentId == 2) // pregunto si tiene beneficiario final para no tener que aplicar la validacion de pep al gerente general
                    {
                        pnAdminPepFormularyOptionsId.Visible = false;
                        ddlAdminPepFormularyOptionsId.SelectedIndex = -1;
                        ddlAdminPepFormularyOptionsId.Attributes.Remove("validation");
                        lnkShowPepsManager.Visible = false;
                        //$('#AdminDisplayPepForm').hide();

                        //valido si tiene Peps para marcarlos como vinculados
                        if (beneficiarios != null)
                        {
                            var peps = beneficiarios.Where(o => o.IsPEP == true).ToList();
                            if (peps.Count > 0)
                            {
                                ddlPep.SelectedIndex = 2;
                                btnVerPEPS.Visible = true;
                                ddlPep_SelectedIndexChanged(null, null);
                            }
                            else
                                btnVerPEPS.Visible = false;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtManagerName.Text.Trim()))
                        {
                            ddlAdminPepFormularyOptionsId.Attributes.Add("validation", "Required");
                            pnAdminPepFormularyOptionsId.Visible = true;
                            if (ddlAdminPepFormularyOptionsId.SelectedIndex > 0)
                            {
                                if (ddlAdminPepFormularyOptionsId.SelectedItem.Text.ToLower().Contains("si"))
                                    lnkShowPepsManager.Visible = true;
                            }
                            else
                                lnkShowPepsManager.Visible = false;
                            //$('#AdminDisplayPepForm').show();
                        }
                        else
                        {
                            lnkShowPepsManager.Visible = false;
                            pnAdminPepFormularyOptionsId.Visible = false;
                            ddlAdminPepFormularyOptionsId.SelectedIndex = -1;
                            ddlAdminPepFormularyOptionsId.Attributes.Remove("validation");
                            //$('#AdminDisplayPepForm').hide();
                        }
                    }
                }
                #endregion


                if (ddlActividad.SelectedIndex <= 0)
                    ddlActividad.Attributes.Add("validation", "Required");
                else
                    ddlActividad.Attributes.Remove("validation");

                if (ddlEstructuraTitularidad.SelectedIndex <= 0)
                    ddlEstructuraTitularidad.Attributes.Add("validation", "Required");
                else
                    ddlEstructuraTitularidad.Attributes.Remove("validation");

                txtManagerName.Enabled = true;

                if (string.IsNullOrEmpty(txtManagerName.Text.Trim()))
                    txtManagerName.Attributes.Add("validation", "Required");
                else
                    txtManagerName.Attributes.Remove("validation");

                txtPlaceOfBirth.Text = string.Empty;
                txtPlaceOfBirth.Attributes.Remove("validation");
                txtPlaceOfBirth.Enabled = false;

            }
            else if (TypeOfPerson == 5 || TypeOfPerson == 6) //organismos nacionales e internacionales
            {
                ddlBeneFinal.SelectedIndex = 2;
                ddlBeneFinal.Attributes.Remove("validation");
                ddlBeneFinal.Enabled = false;

                //ddlBeneficiarioFinalOptions.SelectedIndex = 0;
                //ddlBeneficiarioFinalOptions.Attributes.Remove("validation");

                ddlEstructuraTitularidad.SelectedIndex = -1;
                ddlEstructuraTitularidad.Enabled = false;
                ddlEstructuraTitularidad.Attributes.Remove("validation");
                ddlAdminPepFormularyOptionsId.SelectedIndex = -1;
                ddlAdminPepFormularyOptionsId.Attributes.Remove("validation");
                pnAdminPepFormularyOptionsId.Visible = false;

                ddlActividad.Enabled = true;
                ddlPep.SelectedIndex = 3;
                ddlPep.Enabled = false;
                ddlPep.Attributes.Remove("validation");
                txtManagerName.Enabled = true;

                if (string.IsNullOrEmpty(txtManagerName.Text.Trim()))
                    txtManagerName.Attributes.Add("validation", "Required");
                else
                    txtManagerName.Attributes.Remove("validation");

                btnVerPEPS.Visible = false;
                txtPlaceOfBirth.Text = string.Empty;
                txtPlaceOfBirth.Attributes.Remove("validation");
                txtPlaceOfBirth.Enabled = false;
            }
            else
            {
                ddlBeneFinal.SelectedIndex = -1;
                ddlBeneFinal.Enabled = false;
                ddlActividad.SelectedIndex = -1;
                ddlEstructuraTitularidad.SelectedIndex = -1;
                ddlPep.Attributes.Add("validation", "Required");
                ddlPep.Enabled = true;

                txtPlaceOfBirth.Enabled = true;
                if (string.IsNullOrEmpty(txtPlaceOfBirth.Text))
                    txtPlaceOfBirth.Attributes.Add("validation", "Required");
                else
                    txtPlaceOfBirth.Attributes.Remove("validation");

                ddlActividad.Enabled = false;
                ddlEstructuraTitularidad.Enabled = false;
                if (ddlPep.SelectedIndex > 0)
                    ddlPep.Attributes.Remove("validation");


                ddlBeneFinal.Attributes.Remove("validation");
                ddlActividad.Attributes.Remove("validation");
                ddlEstructuraTitularidad.Attributes.Remove("validation");

                txtManagerName.Text = string.Empty;
                txtManagerName.Enabled = false;

                pnAdminPepFormularyOptionsId.Visible = false;
                ddlAdminPepFormularyOptionsId.SelectedIndex = -1;
                btnVerPEPS.Visible = false;
            }
        }

        public void DisableWorkingInformation(bool enableFields = false)
        {
            if (enableFields)
            {
                txtOccupation.Enabled = true;
                txtEmpresaLabora.Enabled = true;
                txtIngresoAnual.Enabled = true;
            }
            else
            {
                txtOccupation.Text = string.Empty;
                hdnOccupationId.Value = string.Empty;
                txtOccupation.Attributes.Remove("validation");
                txtOccupation.Enabled = false;
                txtEmpresaLabora.Text = string.Empty;
                txtEmpresaLabora.Enabled = false;
                txtEmpresaLabora.Attributes.Remove("validation");

                txtIngresoAnual.Text = "0";
                txtIngresoAnual.Enabled = false;
                txtIngresoAnual.Attributes.Remove("validation");
            }
        }

        protected void txtManagerName_TextChanged(object sender, EventArgs e)
        {
            if (ddlTypeOfPerson.SelectedIndex > 0)
                ValidateFulfillmentInformation();
            else
            {
                pnAdminPepFormularyOptionsId.Visible = false;
                ddlAdminPepFormularyOptionsId.SelectedIndex = -1;
            }
        }

        protected void ddlAdminPepFormularyOptionsId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cuando Sea SI, entonces mostrar el pop de pep
            var spl = Utility.deserializeJSON<itemPepFormularyOption>(ddlAdminPepFormularyOptionsId.SelectedValue);

            if (spl != null)
            {
                if (spl.AgentId == 1 || spl.AgentId == 2)
                {
                    pnAdminPepFormularyOptionsId2.Visible = true;

                    if (spl.AgentId == 1) // Si, Designado
                    {
                        WUCPEPForm.isDesigned = true;
                        WUCPEPForm.DesignedFullName = txtManagerName.Text;
                    }

                    btnVerPEPS.Visible = false;
                    btnVerBeneficiariosFinales.Visible = false;

                    lnkShowPepsManager.Visible = true;
                    WUCPEPForm.hdnOrigen = "AdminPep";
                    //Mostrar popup con el formulario PEP
                    hdnShowPepPop.Value = "true";
                    WUCPEPForm.SelectedcumplimientoItem = this.cumplimientoItem;
                    WUCPEPForm.ContactId = this.ContactId;
                    WUCPEPForm.Initialize();
                    mpePepPop.Show();
                }
                else
                    pnAdminPepFormularyOptionsId2.Visible = true;
            }
            else
            {
                lnkShowPepsManager.Visible = false;
            }
        }

        public void ValidateFinalBeneficiaryIdentification(int CustomerType)
        {
            //if (CustomerType == 5 || CustomerType == 6)
            //{
            //    ddlRepresentativeIdentificationType.SelectedIndex = -1;
            //    ddlRepresentativeIdentificationType.Enabled = false;
            //    txtRepresentanteIdentification.Text = string.Empty;
            //    txtRepresentanteIdentification.Enabled = false;
            //}
            //else
            //{
            //    ddlRepresentativeIdentificationType.Enabled = true;
            //    txtRepresentanteIdentification.Enabled = true;
            //}
        }
        #endregion

        protected void ddlTypeOfPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            var TypeOfSelected = 0;
            if (ddlTypeOfPerson.SelectedIndex > 0)
            {
                TypeOfSelected = ddlTypeOfPerson.SelectedValue.ToInt();
                if (TypeOfSelected == 1 || TypeOfSelected == 3)
                {
                    this.IsCompany = false;
                    hdnIsCompany.Value = this.IsCompany.ToString().ToLower();
                    DisableWorkingInformation(true);
                }
                else
                {
                    this.IsCompany = true;
                    hdnIsCompany.Value = this.IsCompany.ToString().ToLower();
                    DisableWorkingInformation(false);
                }

            }
            else
                DisableWorkingInformation(false);

            ValidateWorkingInformation();
            ValidateFulfillmentInformation();

            var dataIDtype = ObjServices.GettingDropData(Utility.DropDownType.IdType);

            if (!this.IsCompany)
                dataIDtype = dataIDtype.Where(id => id.ContactTypeId != 5).ToList();
            else
                dataIDtype = dataIDtype.Where(id => id.ContactTypeId == 5).ToList();

            if (dataIDtype != null)
            {

                ddlIdType.DataSource = null;
                ddlIdType.DataBind();


                ddlIdType.DataSource = dataIDtype;
                ddlIdType.DataTextField = "ContactTypeIdDesc";
                ddlIdType.DataValueField = "ContactTypeId";
                ddlIdType.DataBind();
                ddlIdType.Items.Insert(0, new ListItem { Text = "----", Value = "-1" });
            }

            ddlIdType_SelectedIndexChanged(null, null);

            //if (ddlPep.SelectedIndex == 3)
            //    ddlPep_SelectedIndexChanged(null, null);
        }

        protected void lnkShowPepsManager_Click(object sender, EventArgs e)
        {
            WUCPEPForm.hdnOrigen = "AdminPep";
            //Mostrar popup con el formulario PEP
            hdnShowPepPop.Value = "true";
            WUCPEPForm.SelectedcumplimientoItem = this.cumplimientoItem;
            WUCPEPForm.ContactId = this.ContactId;
            WUCPEPForm.Initialize();
            mpePepPop.Show();
        }
    }
}