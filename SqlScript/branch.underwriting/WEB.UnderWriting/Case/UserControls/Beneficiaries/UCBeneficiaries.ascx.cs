﻿using DevExpress.Web;
using PdfViewer4AspNet;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.UnderWriting.Common;
using System.Globalization;
using System.Collections.Generic;

namespace WEB.UnderWriting.Case.UserControls.Beneficiaries
{
    public partial class UCBeneficiaries : UC, IUC
    {
        //UnderWritingDIManager diManager = new UnderWritingDIManager();
        readonly DropDownManager _dropDowns = new DropDownManager();
        //IBeneficiary BeneficiaryManager
        //{
        //    get { return diManager.BeneficiaryManager; }
        //}
        private Boolean IsInsured
        {
            get { return Boolean.Parse(String.IsNullOrEmpty(hdnIsInsured.Value) ? "false" : hdnIsInsured.Value); }
            set { hdnIsInsured.Value = value.ToString(); }
        }
        public int BeneficiaryTypeId
        {
            get { return int.Parse(String.IsNullOrEmpty(hdnBeneficiarieTypeID.Value) ? "0" : hdnBeneficiarieTypeID.Value); }
            set { hdnBeneficiarieTypeID.Value = value.ToString(); }
        }
        public UCBeneficiaries UcContingentBeneficiarie { get; set; }
        public class ValidationType
        {
            public Boolean Exists { get; set; }
            public Boolean IsSameId { get; set; }
        }
        private Boolean IsFuneral
        {
            get { return Boolean.Parse(String.IsNullOrEmpty(hdnIsFuneral.Value) ? "false" : hdnIsFuneral.Value); }
            set { hdnIsFuneral.Value = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hdnIsEdit.Value = "false";
            hdnEditIndex.Value = "0";
            hdnUploadedPDFPath.Text = "";

            hdnIsEditCompany.Value = "false";
            hdnEditIndexCompany.Value = "0";
            hdnUploadedPDFPathCompany.Text = "";

            IsInsured = false;
            BeneficiaryTypeId = 0;

        }

        #region Interface Methods
        public void Translator(string Lang)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            var Corp_Id = Service.Corp_Id;
            var Region_Id = Service.Region_Id;
            var Country_Id = Service.Country_Id;
            var Domesticreg_Id = Service.Domesticreg_Id;
            var State_Prov_Id = Service.State_Prov_Id;
            var City_Id = Service.City_Id;
            var Office_Id = Service.Office_Id;
            var Case_Seq_No = Service.Case_Seq_No;
            var Hist_Seq_No = Service.Hist_Seq_No;

            if (gvBeneficiariesCompany.Rows.Count > 0 || gvBeneficiaries.Rows.Count > 0)
                Services.BeneficiaryManager.UpdateComment(Corp_Id, Region_Id, Country_Id, Domesticreg_Id, State_Prov_Id, City_Id, Office_Id, Case_Seq_No, Hist_Seq_No, IsInsured, BeneficiaryTypeId, txtSpecialInstructions.Text, 1);
        }

        public void readOnly(bool x)
        {
            throw new NotImplementedException();
        }

        public void edit()
        {
            throw new NotImplementedException();
        }

        public void addBeneficiaryAddress(int directoryid, string address, string[] aCity, string postalcode, bool isPrimaryChecked, string SelectedContactId)
        {

            /* var CorpId = Service.Corp_Id;
             var DirectoryTypeId = directoryid;
             var StreetAddress = address;


             var RegionId = Convert.ToInt32(aCity[0]);
             var CountryId = Convert.ToInt32(aCity[1]);
             var DomesticregId = Convert.ToInt32(aCity[2]);
             var StateProvId = Convert.ToInt32(aCity[3]);
             var CityId = Convert.ToInt32(aCity[4]);

             var CommunicationType = CommType.Address.ToString();
             var ContactId = Service.Contact_Id;
             */

            var Address = Services.ContactManager.SetAddress(new Entity.UnderWriting.Entities.Contact.Address
            {
                CorpId = Service.Corp_Id,
                DirectoryId = -1,
                DirectoryDetailId = -1,
                DirectoryTypeId = directoryid,
                StreetAddress = address,
                CreateUser = 1,
                ModifyUser = 1,

                RegionId = Convert.ToInt32(aCity[0]),
                CountryId = Convert.ToInt32(aCity[1]),
                DomesticregId = Convert.ToInt32(aCity[2]),
                StateProvId = Convert.ToInt32(aCity[3]),
                CityId = Convert.ToInt32(aCity[4]),

                ZipCode = String.IsNullOrEmpty(postalcode) ? null : postalcode,
                IsPrimary = isPrimaryChecked,
                CommunicationType = CommType.Address.ToString(),
                ContactId = Convert.ToInt32(SelectedContactId)
            });


        }

        /// <summary>
        /// Limpia la data de los Grids para que al cambiar de tab libere la carga del ViewState.
        /// </summary>
        public void clearData()
        {
            CleanPdf();

            gvBeneficiaries.DataSource = null;
            gvBeneficiaries.DataBind();

            gvBeneficiariesCompany.DataSource = null;
            gvBeneficiariesCompany.DataBind();
            txtSpecialInstructions.Text = String.Empty;

            ClearFields(true);
            ClearFields(false);
        }

        public void FillData()
        {
            IsFuneral = Service.GetProductFamily() == Tools.EFamilyProductType.Funeral;

            gvBeneficiaries.Columns[6].Visible = !IsFuneral;
            pnlPercentage.Visible = !IsFuneral;

            var Corp_Id = Service.Corp_Id;
            var Region_Id = Service.Region_Id;
            var Country_Id = Service.Country_Id;
            var Domesticreg_Id = Service.Domesticreg_Id;
            var State_Prov_Id = Service.State_Prov_Id;
            var City_Id = Service.City_Id;
            var Office_Id = Service.Office_Id;
            var Case_Seq_No = Service.Case_Seq_No;
            var Hist_Seq_No = Service.Hist_Seq_No;

            /* 
             --- Como llamar al metodo que trae la data según el tipo de Beneficiario ---
            Main Insured: isInsured = True, beneficiarieTypeId = 1
            Main Insured Contingent: isInsured = True, beneficiarieTypeId = 2
            Aditional Insured: isInsured = False, beneficiarieTypeId = 1
            Aditional Insured Contingent: isInsured = False, beneficiarieTypeId = 2 
            */

            //Lleno la data de los Beneficiarios segun el Valor del HiddenField (1 - Insured Main, 2 - Insured Contingent, 3 - Additional Insured Main, 4 - Additional Insured Contingent)
            switch (hdnBeneficiarieType.Value)
            {
                case "2":
                    IsInsured = true;
                    BeneficiaryTypeId = 2;
                    break;
                case "3":
                    IsInsured = false;
                    BeneficiaryTypeId = 1;
                    break;
                case "4":
                    IsInsured = false;
                    BeneficiaryTypeId = 2;
                    break;
                default:
                    IsInsured = true;
                    BeneficiaryTypeId = 1;
                    break;
            }
            var contacto = Service.Contact_Id;
            var data = Services.BeneficiaryManager.GetAllBeneficiaryExtended(Corp_Id, Region_Id, Country_Id, Domesticreg_Id, State_Prov_Id, City_Id, Office_Id, Case_Seq_No, Hist_Seq_No, IsInsured, BeneficiaryTypeId, null, Service.LanguageId).ToList();
            //var BeneficiariesNumber = data.Count(r => r.ContactId.HasValue);

            // Write the modified stock picks list back to session state.
            // Session["Bcodes"] = stockPicks;

            //ROJAS : TO BRING ADDRESS, EMAIL AND PHONE INFORMATION ABOUT BENEFICIARIES) .SP_GET_CONTACT_BASIC_INFORMATION(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, contactId, contactRoleTypeId);
            var ListContactIds = data.Select(r => r.ContactId);
            var DataSelect = from cust in data orderby cust.ContactId select new { Fullname = cust.FirstName + " " + cust.FirstLastName, codes = cust.ContactId };
            var ListContactsNames = data.Select(r => r.FirstName + " " + r.FirstLastName);
            var ListContactDetails = new List<Entity.UnderWriting.Entities.Contact>();
            IEnumerable<Entity.UnderWriting.Entities.Contact.Address> addressList = null;
            foreach (var Mydata in DataSelect)
            {
                int temporal = Convert.ToInt32(Mydata.codes);

                if (temporal > 0)
                {
                    ListContactDetails.Add(Services.ContactManager.GetContact(Corp_Id, Region_Id, Country_Id, Domesticreg_Id, State_Prov_Id, City_Id, Office_Id, Case_Seq_No, Hist_Seq_No, temporal, null, Service.LanguageId));
                    addressList = Services.ContactManager.GetCommunicatonAdress(Service.Corp_Id, temporal, Service.LanguageId);
                    /*  Services.ContactManager.GetCommunicatonEmail(Service.Corp_Id, temporal, Service.LanguageId);
                      Services.ContactManager.GetCommunicatonPhone(Service.Corp_Id, temporal, Service.LanguageId);
                      if (addressList != null) {
                          ((UCBAddresses)Parent.FindControl("UCBAddresses")).FillAddress(Mydata.Fullname, addressList); 
                      }       */
                }
                //do  the same for emails and phones --> need to specify the beneficiary name
            }

            ((UCBAddresses)Parent.FindControl("UCBAddresses")).FillData2();  //FILL ADDRESSES DROPDOWNS
            ((UCBAddresses)Parent.FindControl("UCBAddresses")).SetInitialAddress(); //CLEAN ADDRESSES
            ((UCBEmailPhone)Parent.FindControl("UCBEmailPhone")).fillDropDownEmailPhones(); //FILL EMAILS DROPDOWNS
            ((UCBEmailPhone)Parent.FindControl("UCBEmailPhone")).SetInitial(); //CLEAN EMAILANDPHONES


            /*ROJAS: BLOCK TO FILL ADDRESS DATA OF BENEFICIARIES*/
            // var addressData = Services.ContactManager.GetCommunicatonAdress(Service.Corp_Id, Service.Contact_Id, Service.LanguageId);
            /*END OF BLOCK ADDRES DATA*/

            //Llenar Grid Detalle de Beneficiarios --> Contactos
            //TODO: DISPLAY MULTIPLE 
            gvBeneficiaries.DataSource = ListContactDetails;
            //GridView1.DataBind();

            //Llenar Grid  de Beneficiarios
            gvBeneficiaries.DataSource = data.Where(r => r.IsCompany.Value == false);
            gvBeneficiaries.DataBind();


            //Llenar Grid  de Beneficiarios tipo Compañía
            gvBeneficiariesCompany.DataSource = data.Where(r => r.IsCompany.Value);
            gvBeneficiariesCompany.DataBind();

            //Llenar el Textbox de Comentarios (Special Instructions)
            txtSpecialInstructions.Text = data.Any() ? (String.IsNullOrWhiteSpace(data.First().Comments) ? txtSpecialInstructions.Text : data.First().Comments) : txtSpecialInstructions.Text;

            //Llenar la data del Ultima Actualización
            var lastUpdate = data.OrderByDescending(r => r.LastModiDate);

            lblLUDateTime.Text = lastUpdate.Any() ? lastUpdate.First().LastModiDate.ToString("MM/dd/yyyy") + "  |  " + lastUpdate.First().LastModiDate.ToString("hh:mm tt") : "-  |  -";
            lblLUUserName.Text = lastUpdate.Any() ? String.IsNullOrWhiteSpace(lastUpdate.First().LastModiUser) ? " - " : lastUpdate.First().LastModiUser : " - ";

            //Llena los Dropdowns de los Mantenimientos
            FillDropDowns();

            //Muestra u oculta los campos segun el tipo de beneficiario
            ShowHideControls();

            if (BeneficiaryTypeId == 1 && IsInsured)
                UcContingentBeneficiarie.FillData("2");
            else if (BeneficiaryTypeId == 1 && !IsInsured)
                UcContingentBeneficiarie.FillData("4");

        }

        public void FillData(String beneficiarieType)
        {
            hdnBeneficiarieType.Value = beneficiarieType;
            FillData();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Limpia los campos de los mantenimientos de los beneficiarios.
        /// </summary>
        /// <param name="companyBeneficiarie" >Parametro para indicar si se van a limpiar los campos del Beneficiario o del Beneficiario Compañía </param>
        private void ClearFields(bool companyBeneficiarie)
        {
            if (!companyBeneficiarie)
            {
                txtFirstName.Text = String.Empty;
                txtMiddleName.Text = String.Empty;
                txtLastName.Text = String.Empty;
                txtSecondLastName.Text = String.Empty;
                txtBEDateofBirth.Text = String.Empty;
                txtPercentage.Text = String.Empty;
                txtIDNo.Text = String.Empty;
                ddlRelationship.SelectedIndex = ddlRelationship.Items.Count > 0 ? 0 : -1;
                ddlReplacing.SelectedIndex = ddlReplacing.Items.Count > 0 ? 0 : -1;
                gvBeneficiaries.Enabled = true;

                hdnEditIndex.Value = "0";
                hdnIsEdit.Value = "false";
                hdnUploadedPDFPath.Text = "";
            }
            else
            {
                txtEntityName.Text = String.Empty;
                txtBEIncorporationDate.Text = String.Empty;
                txtEntityPercentage.Text = String.Empty;
                txtEntityIDNo.Text = String.Empty;
                ddlEntityType.SelectedIndex = ddlEntityType.Items.Count > 0 ? 0 : -1;
                ddlReplacingCompany.SelectedIndex = ddlReplacingCompany.Items.Count > 0 ? 0 : -1;
                gvBeneficiariesCompany.Enabled = true;

                hdnEditIndexCompany.Value = "0";
                hdnIsEditCompany.Value = "false";
                hdnUploadedPDFPathCompany.Text = "";
            }
        }

        /// <summary>
        /// Llena los dropdowns de los mantenimientos.
        /// </summary>
        private void FillDropDowns()
        {
            ddlRelationship.SelectedIndex = -1;
            ddlEntityType.SelectedIndex = -1;
            ddlReplacing.SelectedIndex = -1;
            ddlReplacingCompany.SelectedIndex = -1;

            _dropDowns.GetDropDown(ref ddlRelationship, Language.English, DropDownType.Relationship, Service.Corp_Id, null, null, null, null, null, null, null, null, null, null, projectId: Service.ProjectId, companyId: Service.CompanyId);
            _dropDowns.GetDropDown(ref ddlEntityType, Language.English, DropDownType.CompanyType, Service.Corp_Id, null, null, null, null, null, null, null, null, null, null, 1, projectId: Service.ProjectId, companyId: Service.CompanyId);

            if (ddlEntityType.Items.Count > 0)
                ddlEntityType.SelectedIndex = 0;

            if (ddlRelationship.Items.Count > 0)
                ddlRelationship.SelectedIndex = 0;

            if (BeneficiaryTypeId != 2) return;
            _dropDowns.GetDropDown(ref ddlReplacing, Language.English, DropDownType.PrimaryBeneficiary, Service.Corp_Id, Service.Region_Id, Service.Country_Id, Service.Domesticreg_Id, Service.State_Prov_Id, Service.City_Id, Service.Office_Id, Service.Case_Seq_No, Service.Hist_Seq_No, null, null, null, IsInsured, projectId: Service.ProjectId, companyId: Service.CompanyId);
            _dropDowns.GetDropDown(ref ddlReplacingCompany, Language.English, DropDownType.PrimaryBeneficiary, Service.Corp_Id, Service.Region_Id, Service.Country_Id, Service.Domesticreg_Id, Service.State_Prov_Id, Service.City_Id, Service.Office_Id, Service.Case_Seq_No, Service.Hist_Seq_No, null, null, null, IsInsured, projectId: Service.ProjectId, companyId: Service.CompanyId);

            if (ddlReplacing.Items.Count > 0)
                ddlReplacing.SelectedIndex = 0;

            if (ddlReplacingCompany.Items.Count > 0)
                ddlReplacingCompany.SelectedIndex = 0;
        }

        /// <summary>
        /// Obtiene la suma total del porcentaje de ambos tipos de Beneficiarios
        /// </summary>
        /// <param name="contactId">Este parametro sirve para que obvie la suma de un Beneficiario en especifico, en caso de querer sumar todos enviar nulo o no enviar.</param>
        /// <returns></returns>
        private Decimal GetTotalPercentage(int? contactId = null)
        {
            Decimal sumPercent = 0;

            if (contactId != null)
            {
                foreach (GridViewRow item in gvBeneficiaries.Rows)
                {
                    var rContactId = int.Parse(gvBeneficiaries.DataKeys[item.RowIndex]["ContactId"].ToString());

                    if (rContactId != contactId)
                    {
                        var lbl = ((Label)item.FindControl("lblBenefitsPercent")).Text.Replace("%", "");
                        sumPercent += Decimal.Parse(lbl);
                    }
                }

                foreach (GridViewRow item in gvBeneficiariesCompany.Rows)
                {
                    var rContactId = int.Parse(gvBeneficiariesCompany.DataKeys[item.RowIndex]["ContactId"].ToString());

                    if (rContactId != contactId)
                    {
                        var lbl = ((Label)item.FindControl("lblBenefitsPercent")).Text.Replace("%", "");
                        sumPercent += Decimal.Parse(lbl);
                    }
                }
            }
            else
            {
                foreach (GridViewRow item in gvBeneficiaries.Rows)
                {
                    var lbl = ((Label)item.FindControl("lblBenefitsPercent")).Text.Replace("%", "");
                    sumPercent += Decimal.Parse(lbl);
                }

                foreach (GridViewRow item in gvBeneficiariesCompany.Rows)
                {
                    var lbl = ((Label)item.FindControl("lblBenefitsPercent")).Text.Replace("%", "");
                    sumPercent += Decimal.Parse(lbl);
                }
            }

            return sumPercent;
        }

        public Boolean CompletedData()
        {
            var isFuneral = Service.GetProductFamily() == Tools.EFamilyProductType.Funeral;

            if (!isFuneral)
                return GetTotalPercentage() >= 100;
            else
                return isFuneral;
        }

        /// <summary>
        /// Muestra u oculta los campos segun el tipo de beneficiario
        /// </summary>
        private void ShowHideControls()
        {
            pnlReplacingCompany.Visible = BeneficiaryTypeId != 1;
            pnlReplacing.Visible = BeneficiaryTypeId != 1;
            gvBeneficiaries.Columns[8].Visible = BeneficiaryTypeId == 2;
            gvBeneficiariesCompany.Columns[5].Visible = BeneficiaryTypeId == 2;

            switch (hdnBeneficiarieType.Value)
            {
                case "2":
                    //FileUpload Controls
                    fuBenediciarieFile.CssClass = "MC";
                    hdnUploadedPDFPath.CssClass = "MC";

                    txtBEDateofBirth.Attributes.Add("class", "datepicker MC");
                    btnAdd.Attributes.Add("class", "boton MC");
                    txtFirstName.Attributes.Add("class", "MC");
                    txtLastName.Attributes.Add("class", "MC");
                    txtPercentage.Attributes.Add("class", "MC");
                    ddlRelationship.Attributes.Add("class", "MC");

                    ///Company

                    //FileUpload Controls
                    fuBenediciarieFileCompany.CssClass = "MC";
                    hdnUploadedPDFPathCompany.CssClass = "MC";

                    txtEntityName.Attributes.Add("class", "MC");
                    txtEntityPercentage.Attributes.Add("class", "MC");
                    ddlEntityType.Attributes.Add("class", "MC");
                    btnBECompanyAdd.Attributes.Add("class", "boton MC");
                    txtBEIncorporationDate.Attributes.Add("class", "datepicker alignL MC");
                    break;
                case "3":
                    //FileUpload Controls
                    fuBenediciarieFile.CssClass = "AP";
                    hdnUploadedPDFPath.CssClass = "AP";

                    txtBEDateofBirth.Attributes.Add("class", "datepicker AP");
                    btnAdd.Attributes.Add("class", "boton AP");
                    txtFirstName.Attributes.Add("class", "AP");
                    txtLastName.Attributes.Add("class", "AP");
                    txtPercentage.Attributes.Add("class", "AP");
                    ddlRelationship.Attributes.Add("class", "AP");

                    ///Company

                    //FileUpload Controls
                    fuBenediciarieFileCompany.CssClass = "AP";
                    hdnUploadedPDFPathCompany.CssClass = "AP";

                    txtEntityName.Attributes.Add("class", "AP");
                    txtEntityPercentage.Attributes.Add("class", "AP");
                    ddlEntityType.Attributes.Add("class", "AP");
                    btnBECompanyAdd.Attributes.Add("class", "boton AP");
                    txtBEIncorporationDate.Attributes.Add("class", "datepicker alignL AP");
                    break;
                case "4":
                    //FileUpload Controls
                    fuBenediciarieFile.CssClass = "AC";
                    hdnUploadedPDFPath.CssClass = "AC";

                    txtBEDateofBirth.Attributes.Add("class", "datepicker AC");
                    btnAdd.Attributes.Add("class", "boton AC");
                    txtFirstName.Attributes.Add("class", "AC");
                    txtLastName.Attributes.Add("class", "AC");
                    txtPercentage.Attributes.Add("class", "AC");
                    ddlRelationship.Attributes.Add("class", "AC");

                    ///Company

                    //FileUpload Controls
                    fuBenediciarieFileCompany.CssClass = "AC";
                    hdnUploadedPDFPathCompany.CssClass = "AC";

                    txtEntityName.Attributes.Add("class", "AC");
                    txtEntityPercentage.Attributes.Add("class", "AC");
                    ddlEntityType.Attributes.Add("class", "AC");
                    btnBECompanyAdd.Attributes.Add("class", "boton AC");
                    txtBEIncorporationDate.Attributes.Add("class", "datepicker alignL AC");
                    break;
                default:
                    //FileUpload Controls
                    fuBenediciarieFile.CssClass = "MP";
                    hdnUploadedPDFPath.CssClass = "MP";


                    txtBEDateofBirth.Attributes.Add("class", "datepicker MP");
                    btnAdd.Attributes.Add("class", "boton MP");
                    txtFirstName.Attributes.Add("class", "MP");
                    txtLastName.Attributes.Add("class", "MP");
                    txtPercentage.Attributes.Add("class", "MP");
                    ddlRelationship.Attributes.Add("class", "MP");

                    ///Company

                    //FileUpload Controls
                    fuBenediciarieFileCompany.CssClass = "MP";
                    hdnUploadedPDFPathCompany.CssClass = "MP";

                    txtEntityName.Attributes.Add("class", "MP");
                    txtEntityPercentage.Attributes.Add("class", "MP");
                    ddlEntityType.Attributes.Add("class", "MP");
                    btnBECompanyAdd.Attributes.Add("class", "boton MP");
                    txtBEIncorporationDate.Attributes.Add("class", "datepicker alignL MP");
                    break;
            }
        }

        private void CleanPdf()
        {
            var pdfViewerControl = (PdfViewer)Page.Master.FindControl("Right").FindControl("Right").FindControl("UCPdfViewer").FindControl("Viewer");
            pdfViewerControl.PdfSourceBytes = null;
            pdfViewerControl.DataBind();
        }

        /// <summary>
        /// Metodo para validar que un beneficiario ya existe.
        /// </summary>
        /// <param name="isCompany">Para especificar si el beneficiario es una compañia o una persona.</param>
        /// <param name="excludeContacId"> Para que no tome en cuenta a una persona en especifico en la validación.</param>
        /// <param name="excludeContactRoleTypeId">Para que no tome en cuenta a una persona en especifico en la validación.</param>
        /// <returns></returns>
        private ValidationType ValidateCurrentBeneficiaries(Boolean isCompany, int? excludeContacId = null, int? excludeContactRoleTypeId = null)
        {
            var ExistsItem = new ValidationType { Exists = false, IsSameId = false };

            var data = Services.BeneficiaryManager.GetAllBeneficiary(Service.Corp_Id, Service.Region_Id, Service.Country_Id, Service.Domesticreg_Id,
                Service.State_Prov_Id, Service.City_Id, Service.Office_Id, Service.Case_Seq_No,
                Service.Hist_Seq_No, IsInsured, BeneficiaryTypeId, null, Service.LanguageId).Where(r => r.IsCompany == isCompany).ToList();

            if (excludeContacId.HasValue)
                data = data.Where(r => !(r.ContactId == excludeContacId.Value && r.ContactRoleTypeId == excludeContactRoleTypeId.Value)).ToList();

            if (isCompany)
            {
                foreach (var item in data)
                {
                    var entityType = item.OccupGroupTypeId.Value.ToString() + "|" + item.OccupationId.Value.ToString();

                    if (item.InstitutionalName.Trim() == txtEntityName.Text.Trim() &&
                                          entityType == ddlEntityType.SelectedValue &&
                                           item.Dob.Value == DateTime.Parse(txtBEIncorporationDate.Text))
                    {
                        ExistsItem.IsSameId = false;
                        ExistsItem.Exists = true;
                        return ExistsItem;
                    }
                }
            }
            else
            {
                foreach (var item in data)
                {
                    //CODIGO ORIGINAL
                    //if (item.FirstName.Trim() == txtFirstName.Text.Trim() &&
                    //    item.FirstLastName.Trim() == txtLastName.Text.Trim() &&
                    //    item.Dob.Value == DateTime.Parse(txtBEDateofBirth.Text))
                    //FIN CODIGO ORIGINAL 


                    //mavelar 3/28/17
                    if ((item.FirstName.Trim() == txtFirstName.Text.Trim()) &&
                       (item.FirstLastName.Trim() == txtLastName.Text.Trim())
                       && (item.ContactMainId.Trim() == txtIDNo.Text.Trim()) ||
                       (item.Dob.Value == DateTime.Parse(txtBEDateofBirth.Text)))
                    //fin mavelar 3/28/17
                    {
                        ExistsItem.IsSameId = false;
                        ExistsItem.Exists = true;
                        return ExistsItem;
                    }
                    else if ((hdnBeneficiarieType.Value.Trim() == "1" || hdnBeneficiarieType.Value.Trim() == "2") && !String.IsNullOrWhiteSpace(txtIDNo.Text))
                    {
                        if (item.ContactMainId.Trim() == txtIDNo.Text.Trim())
                        {
                            ExistsItem.IsSameId = true;
                            ExistsItem.Exists = false;
                            return ExistsItem;
                        };
                    }
                }
            }

            return ExistsItem;
        }

        private void SetButtons(Boolean isCompany, Boolean isEdit)
        {
            if (isCompany)
            {
                if (isEdit)
                {
                    //Add Button
                    dvBtnAddCompany.Attributes["class"] = "boton_wrapper gradient_vd2 bdrVd2 fl mR";
                    spBtnAddCompany.Attributes["class"] = "save";
                    btnBECompanyAdd.Text = "Save";

                    //Clear Button
                    dvBtnClearCompany.Attributes["class"] = "boton_wrapper fr gradient_RJ bdrRJ";
                    spBtnClearCompany.Attributes["class"] = "equis";
                    btnBECompanyClear.Text = "Cancel";
                }
                else
                {
                    //Add Button
                    dvBtnAddCompany.Attributes["class"] = "boton_wrapper gradient_AM_btn bdrAM fl mR";
                    spBtnAddCompany.Attributes["class"] = "add";
                    btnBECompanyAdd.Text = "Add";

                    //Clear Button
                    dvBtnClearCompany.Attributes["class"] = "boton_wrapper fr gris";
                    spBtnClearCompany.Attributes["class"] = "erase";
                    btnBECompanyClear.Text = "Clear";
                }
            }
            else
            {
                if (isEdit)
                {
                    //Add Button
                    dvBtnAdd.Attributes["class"] = "boton_wrapper gradient_vd2 bdrVd2 fl mR";
                    spBtnAdd.Attributes["class"] = "save";
                    btnAdd.Text = "Save";

                    //Clear Button
                    dvBtnClear.Attributes["class"] = "boton_wrapper fl gradient_RJ bdrRJ";
                    spBtnClear.Attributes["class"] = "equis";
                    btnBEClear.Text = "Cancel";
                }
                else
                {
                    //Add Button
                    dvBtnAdd.Attributes["class"] = "boton_wrapper gradient_AM_btn bdrAM fl mR";
                    spBtnAdd.Attributes["class"] = "add";
                    btnAdd.Text = "Add";

                    //Clear Button
                    dvBtnClear.Attributes["class"] = "boton_wrapper fl gris";
                    spBtnClear.Attributes["class"] = "erase";
                    btnBEClear.Text = "Clear";
                }
            }
        }
        #endregion

        #region Company Beneficiaries Events
        protected void btnBECompanyAdd_Click(object sender, EventArgs e)
        {
            var BItem = new Entity.UnderWriting.Entities.Beneficiary();

            BItem.BeneficiaryTypeId = BeneficiaryTypeId; //Aqui va si el beneficiario es contigente o primario. --1 es Primario y 2 Contingente.
            BItem.PrimaryBeneficiary = IsInsured; //True si es del Insured, False si es del Additional.

            //Obtener Key desde la Session
            var entTypeSplit = ddlEntityType.SelectedValue.Split('|');
            var FileName = Service.TempFilePath + "\\" + hdnUploadedPDFPathCompany.Text;
           
            Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation Add = new Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation()
            {
                CorpId = Service.Corp_Id,
                RegionId = Service.Region_Id,
                CountryId = Service.Country_Id,
                DomesticregId = Service.Domesticreg_Id,
                StateProvId = Service.State_Prov_Id,
                CityId = Service.City_Id,
                OfficeId = Service.Office_Id,
                CaseSeqNo = Service.Case_Seq_No,
                HistSeqNo = Service.Hist_Seq_No,
                InsuredName = txtFirstName.Text + " " + txtMiddleName.Text + " " + txtLastName.Text + " " + txtSecondLastName.Text,
                Role_Type_ID = 4,
                identification = txtIDNo.Text
            };

            if (hdnIsEditCompany.Value == "true")
            {
                var rIndex = int.Parse(hdnEditIndexCompany.Value);

                var ContactId = int.Parse(gvBeneficiariesCompany.DataKeys[rIndex]["ContactId"].ToString());
                var ContactRoleTypeId = int.Parse(gvBeneficiariesCompany.DataKeys[rIndex]["ContactRoleTypeId"].ToString());

                //Valido que este beneficiario no exista en el grid antes de agregarlo
                if (ValidateCurrentBeneficiaries(true, ContactId, ContactRoleTypeId).Exists)
                {
                    string message = "This Beneficiary is already in the list, please verify and try again.";
                    ScriptManager.RegisterStartupScript(upBeneficiaries, upBeneficiaries.GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Invalid Beneficiary');", true);
                    return;
                }

                int? SeqNo = null;
                if (gvBeneficiariesCompany.DataKeys[rIndex]["SeqNo"] != null) //Esto quiere decir que el ID del contacto no existe en la tabla de contactto
                    SeqNo = int.Parse(gvBeneficiariesCompany.DataKeys[rIndex]["SeqNo"].ToString());

                //Busco en el grid la suma de los porcentajes de los beneficiarios, para asegurarme de que el mismo nunca pase de 100%.
                if ((GetTotalPercentage(ContactId) + Decimal.Parse(txtEntityPercentage.Text)) > 100)
                {
                    string message = "The Percentage of all Beneficiaries can not exceed 100%, please verify and try again.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Invalid Value');", true);
                    return;
                }

                //Actualiza o Inserta el Documento del Beneficiario
                if (!String.IsNullOrEmpty(hdnUploadedPDFPathCompany.Text))
                {  
                    if (gvBeneficiariesCompany.DataKeys[rIndex]["DocumentId"] != null)
                    {

                        Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation Add2 = new Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation()
                        {
                            CorpId = Service.Corp_Id,
                            RegionId = Service.Region_Id,
                            CountryId = Service.Country_Id,
                            DomesticregId = Service.Domesticreg_Id,
                            StateProvId = Service.State_Prov_Id,
                            CityId = Service.City_Id,
                            OfficeId = Service.Office_Id,
                            CaseSeqNo = Service.Case_Seq_No,
                            HistSeqNo = Service.Hist_Seq_No,
                            Contact_ID = ContactId
                        };

                        //Obtener Informacion del Documento desde el Grid
                        var DocumentCategoryId = int.Parse(gvBeneficiariesCompany.DataKeys[rIndex]["DocumentCategoryId"].ToString());
                        var DocumentTypeId = int.Parse(gvBeneficiariesCompany.DataKeys[rIndex]["DocumentTypeId"].ToString());
                        var DocumentId = int.Parse(gvBeneficiariesCompany.DataKeys[rIndex]["DocumentId"].ToString());

                        //Actualizar Documento
                        Services.BeneficiaryManager.SetDocument(ContactId, SeqNo, DocumentId, Tools.ReadBinaryFile(FileName), 1);
                        SendFileToOnBase(Add2, DocumentCategoryId, DocumentTypeId, FileName);
                    }
                    else
                    {
                        //Insertar Documento
                        Services.BeneficiaryManager.SetDocument(ContactId, SeqNo, null, Tools.ReadBinaryFile(FileName), 1);
                        SendFileToOnBase(Add, 25, 1, FileName);
                    }
                }

                //Seteo el Key del Item
                BItem.CorpId = Service.Corp_Id;
                BItem.RegionId = Service.Region_Id;
                BItem.CountryId = Service.Country_Id;
                BItem.DomesticregId = Service.Domesticreg_Id;
                BItem.StateProvId = Service.State_Prov_Id;
                BItem.CityId = Service.City_Id;
                BItem.OfficeId = Service.Office_Id;
                BItem.CaseSeqNo = Service.Case_Seq_No;
                BItem.HistSeqNo = Service.Hist_Seq_No;
                BItem.ContactId = ContactId;
                BItem.ContactRoleTypeId = ContactRoleTypeId;

                BItem.ContactTypeId = 5; //Aqui el tipo de cliente 5 Compañia y  4 beneficiario.
                BItem.IsCompany = true;
                BItem.InstitutionalName = txtEntityName.Text;
                BItem.InstitutionalCountryId = Service.Country_Id;
                BItem.Dob = DateTime.Parse(txtBEIncorporationDate.Text);
                BItem.BenefitsPercent = Decimal.Parse(txtEntityPercentage.Text);
                BItem.ContactMainId = txtEntityIDNo.Text;
                BItem.OccupGroupTypeId = int.Parse(entTypeSplit[0]);
                BItem.OccupationId = int.Parse(entTypeSplit[1]);
                BItem.PrimaryBeneficiaryId = BeneficiaryTypeId == 2 && ddlReplacingCompany.SelectedIndex > 0 ? int.Parse(ddlReplacingCompany.SelectedValue) : (int?)null;
                BItem.SeqNo = SeqNo;
                BItem.CreateUser = Service.Underwriter_Id;


                Services.BeneficiaryManager.UpdatetBeneficiary(BItem);
            }
            else
            {
                var ContactRoleTypeId = 4;

                //Valido que este beneficiario no exista en el grid antes de agregarlo
                if (ValidateCurrentBeneficiaries(true).Exists)
                {
                    string message = "This Beneficiary is already in the list, please verify and try again.";
                    ScriptManager.RegisterStartupScript(upBeneficiaries, upBeneficiaries.GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Invalid Beneficiary');", true);
                    return;
                }

                //Busco en el grid la suma de los porcentajes de los beneficiarios, para asegurarme de que el mismo nunca pase de 100%.
                if ((GetTotalPercentage() + Decimal.Parse(txtEntityPercentage.Text)) > 100)
                {
                    string message = "The Percentage of all Beneficiaries can not exceed 100%, please verify and try again.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Invalid Value');", true);
                    return;
                }

                //Seteo el Key del Item
                BItem.CorpId = Service.Corp_Id;
                BItem.RegionId = Service.Region_Id;
                BItem.CountryId = Service.Country_Id;
                BItem.DomesticregId = Service.Domesticreg_Id;
                BItem.StateProvId = Service.State_Prov_Id;
                BItem.CityId = Service.City_Id;
                BItem.OfficeId = Service.Office_Id;
                BItem.CaseSeqNo = Service.Case_Seq_No;
                BItem.HistSeqNo = Service.Hist_Seq_No;
                BItem.ContactRoleTypeId = ContactRoleTypeId;

                BItem.ContactTypeId = 5; //Aqui el tipo de cliente 5 Compañia y  4 beneficiario.
                BItem.IsCompany = true;
                BItem.InstitutionalName = txtEntityName.Text;
                BItem.InstitutionalCountryId = Service.Country_Id;
                BItem.Dob = DateTime.Parse(txtBEIncorporationDate.Text);
                BItem.BenefitsPercent = Decimal.Parse(txtEntityPercentage.Text);
                BItem.ContactMainId = txtEntityIDNo.Text;
                BItem.OccupGroupTypeId = int.Parse(entTypeSplit[0]);
                BItem.OccupationId = int.Parse(entTypeSplit[1]);
                BItem.PrimaryBeneficiaryId = BeneficiaryTypeId == 2 && ddlReplacingCompany.SelectedIndex > 0 ? int.Parse(ddlReplacingCompany.SelectedValue) : (int?)null;
                BItem.IssuedBy = null;
                BItem.CreateUser = Service.Underwriter_Id;
                BItem.DocumentBinary = String.IsNullOrEmpty(hdnUploadedPDFPathCompany.Text) ? null : Tools.ReadBinaryFile(FileName);

                Services.BeneficiaryManager.InsertBeneficiary(BItem);
                SendFileToOnBase(Add, 25, 1, FileName);
            }

            //Limpio los Campos y refresco la Data de los Grids
            btnBECompanyClear_Click(null, null);
            FillData();
        }

        protected void btnBECompanyClear_Click(object sender, EventArgs e)
        {
            ClearFields(true);

            if (btnBECompanyClear.Text.ToLower() == "cancel")
                SetButtons(true, false);
        }

        protected void btnBECompanyRemove_Click(object sender, EventArgs e)
        {
            var gridRow = (GridViewRow)((Button)sender).NamingContainer;

            var ContactId = int.Parse(gvBeneficiariesCompany.DataKeys[gridRow.RowIndex]["ContactId"].ToString());
            var ContactRoleTypeId = int.Parse(gvBeneficiariesCompany.DataKeys[gridRow.RowIndex]["ContactRoleTypeId"].ToString());

            //Obtener Key desde la Session
            Services.BeneficiaryManager.DeleteBeneficiary(Service.Corp_Id, Service.Region_Id, Service.Country_Id, Service.Domesticreg_Id, Service.State_Prov_Id, Service.City_Id, Service.Office_Id, Service.Case_Seq_No, Service.Hist_Seq_No, ContactId, ContactRoleTypeId);
            FillData();
        }

        protected void btnBECompanyEdit_Click(object sender, EventArgs e)
        {
            var a = (GridViewRow)((LinkButton)sender).NamingContainer;

            txtEntityName.Text = ((Label)a.FindControl("lblInstitutionalName")).Text;
            txtBEIncorporationDate.Text = ((Label)a.FindControl("lblDob")).Text;
            txtEntityPercentage.Text = String.IsNullOrEmpty(((Label)a.FindControl("lblBenefitsPercent")).Text) ? "0" : decimal.Parse(((Label)a.FindControl("lblBenefitsPercent")).Text.Replace("%", "")).ToString("F2");
            txtEntityIDNo.Text = ((Label)a.FindControl("lblContactID")).Text;

            var OccupationId = "1|" + gvBeneficiariesCompany.DataKeys[a.RowIndex]["OccupationId"].ToString();
            UnderWriting.Common.Tools.SelectIndexByValue(ref ddlEntityType, OccupationId, false);

            if (hdnBeneficiarieType.Value == "2" || hdnBeneficiarieType.Value == "4")
            {
                var RelatedID = gvBeneficiariesCompany.DataKeys[a.RowIndex]["PrimaryBeneficiaryId"] == null ? "-1" : gvBeneficiariesCompany.DataKeys[a.RowIndex]["PrimaryBeneficiaryId"].ToString();
                ddlReplacingCompany.SelectedValue = RelatedID;
            }

            gvBeneficiariesCompany.Enabled = false;
            hdnIsEditCompany.Value = "true";
            hdnEditIndexCompany.Value = a.RowIndex.ToString();

            //Set Buttons
            SetButtons(true, true);
        }

        protected void btnBECompanyFile_Click(object sender, EventArgs e)
        {
            var gridRow = (GridViewRow)((LinkButton)sender).NamingContainer;

            //Obtener Informacion del Documento desde el Grid
            var ContactId = int.Parse(gvBeneficiariesCompany.DataKeys[gridRow.RowIndex]["ContactId"].ToString());
            var DocumentCategoryId = int.Parse(gvBeneficiariesCompany.DataKeys[gridRow.RowIndex]["DocumentCategoryId"].ToString());
            var DocumentTypeId = int.Parse(gvBeneficiariesCompany.DataKeys[gridRow.RowIndex]["DocumentTypeId"].ToString());
            var DocumentId = int.Parse(gvBeneficiariesCompany.DataKeys[gridRow.RowIndex]["DocumentId"].ToString());

            string documentType = "D";
            Services ServicesOn = new Services();
            Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation Add = new Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation()
            {              
                CorpId = Service.Corp_Id,
                RegionId = Service.Region_Id,
                CountryId = Service.Country_Id,
                DomesticregId = Service.Domesticreg_Id,
                StateProvId = Service.State_Prov_Id,
                CityId = Service.City_Id,
                OfficeId = Service.Office_Id,
                CaseSeqNo = Service.Case_Seq_No,
                HistSeqNo = Service.Hist_Seq_No,
                Contact_ID = ContactId
            };
            byte[] pdfOnBase = ServicesOn.ViewFileFromOnBase(Add,documentType, DocumentCategoryId, DocumentTypeId);

            if (pdfOnBase == null)
            {
                //Buscar Data del documento.
                var beneficiaryDoc = Services.BeneficiaryManager.GetIdDocument(ContactId, DocumentCategoryId, DocumentTypeId, DocumentId);

                //Buscar control del PDF Viewer y pasarle el documento a mostrar.
                if (beneficiaryDoc.DocumentBinary != null)
                {
                    ViewPDF(beneficiaryDoc.DocumentBinary);
                }
            }
            else
            {
                ViewPDF(pdfOnBase);
            }
        }
        #endregion

        #region Beneficiaries Events
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var bItem = new Entity.UnderWriting.Entities.Beneficiary
            {
                BeneficiaryTypeId = BeneficiaryTypeId, //Aqui va si el beneficiario es contigente o primario. --1 es Primario y 2 Contingente.
                PrimaryBeneficiary = IsInsured //True si es del Insured, False si es del Additional.
            };

            var FileName = Service.TempFilePath + "\\" + hdnUploadedPDFPath.Text;

            Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation Add = new Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation()
            {
                CorpId = Service.Corp_Id,
                RegionId = Service.Region_Id,
                CountryId = Service.Country_Id,
                DomesticregId = Service.Domesticreg_Id,
                StateProvId = Service.State_Prov_Id,
                CityId = Service.City_Id,
                OfficeId = Service.Office_Id,
                CaseSeqNo = Service.Case_Seq_No,
                HistSeqNo = Service.Hist_Seq_No,
                InsuredName = txtFirstName.Text + " " + txtMiddleName.Text + " " + txtLastName.Text + " " + txtSecondLastName.Text,
                Role_Type_ID = 4,
                identification = txtIDNo.Text 
            };

            if (hdnIsEdit.Value == "true")
            {
                var rIndex = int.Parse(hdnEditIndex.Value);

                var ContactId = int.Parse(gvBeneficiaries.DataKeys[rIndex]["ContactId"].ToString());
                var ContactRoleTypeId = int.Parse(gvBeneficiaries.DataKeys[rIndex]["ContactRoleTypeId"].ToString());

                //Valido que este beneficiario no exista en el grid antes de agregarlo
                var Validation = ValidateCurrentBeneficiaries(false, ContactId, ContactRoleTypeId);
                if (Validation.Exists)
                {
                    string message = "This Beneficiary is already in the list, please verify and try again.";
                    ScriptManager.RegisterStartupScript(upBeneficiaries, upBeneficiaries.GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Invalid Beneficiary');", true);
                    return;
                }
                else if (Validation.IsSameId)
                {
                    string message = "There is already a Beneficiary with this ID, please try with other ID Number.";
                    ScriptManager.RegisterStartupScript(upBeneficiaries, upBeneficiaries.GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Invalid Value');", true);
                    return;
                }

                int? SeqNo = null;
                if (gvBeneficiaries.DataKeys[rIndex]["SeqNo"] != null) //Esto quiere decir que el ID del contacto no existe en la tabla de contacto
                    SeqNo = int.Parse(gvBeneficiaries.DataKeys[rIndex]["SeqNo"].ToString());

                //Busco en el grid la suma de los porcentajes de los beneficiarios, para asegurarme de que el mismo nunca pase de 100%.
                if (!IsFuneral)
                {
                    if ((GetTotalPercentage(ContactId) + Decimal.Parse(txtPercentage.Text)) > 100)
                    {
                        const string message = "The Percentage of all Beneficiaries can not exceed 100%, please verify and try again.";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Invalid Value');", true);
                        return;
                    }
                }    

                //Actualiza o Inserta el Documento del Beneficiario
                if (!String.IsNullOrEmpty(hdnUploadedPDFPath.Text))
                {            
                    if (gvBeneficiaries.DataKeys[rIndex]["DocumentId"] != null)
                    {

                        Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation Add2 = new Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation()
                        {
                            CorpId = Service.Corp_Id,
                            RegionId = Service.Region_Id,
                            CountryId = Service.Country_Id,
                            DomesticregId = Service.Domesticreg_Id,
                            StateProvId = Service.State_Prov_Id,
                            CityId = Service.City_Id,
                            OfficeId = Service.Office_Id,
                            CaseSeqNo = Service.Case_Seq_No,
                            HistSeqNo = Service.Hist_Seq_No,
                            Contact_ID = ContactId
                        };

                        //Obtener Informacion del Documento desde el Grid
                        var DocumentCategoryId = int.Parse(gvBeneficiaries.DataKeys[rIndex]["DocumentCategoryId"].ToString());
                        var DocumentTypeId = int.Parse(gvBeneficiaries.DataKeys[rIndex]["DocumentTypeId"].ToString());
                        var DocumentId = int.Parse(gvBeneficiaries.DataKeys[rIndex]["DocumentId"].ToString());

                        //Actualizar Documento
                        Services.BeneficiaryManager.SetDocument(ContactId, SeqNo, DocumentId, Tools.ReadBinaryFile(FileName), 1);
                        SendFileToOnBase(Add2,DocumentCategoryId, DocumentTypeId, FileName);
                    }
                    else
                    {
                        //Insertar Documento
                        Services.BeneficiaryManager.SetDocument(ContactId, SeqNo, null, Tools.ReadBinaryFile(FileName), 1);
                        SendFileToOnBase(Add,25, 1, FileName);
                    }

                }

                //Seteo el Key del Item
                bItem.CorpId = Service.Corp_Id;
                bItem.RegionId = Service.Region_Id;
                bItem.CountryId = Service.Country_Id;
                bItem.DomesticregId = Service.Domesticreg_Id;
                bItem.StateProvId = Service.State_Prov_Id;
                bItem.CityId = Service.City_Id;
                bItem.OfficeId = Service.Office_Id;
                bItem.CaseSeqNo = Service.Case_Seq_No;
                bItem.HistSeqNo = Service.Hist_Seq_No;
                bItem.ContactId = ContactId;
                bItem.ContactRoleTypeId = ContactRoleTypeId;


                bItem.ContactTypeId = 4; //Aqui el tipo de cliente 5 Compañia y  4 Beneficiario.
                bItem.FirstName = txtFirstName.Text;
                bItem.MiddleName = txtMiddleName.Text;
                bItem.FirstLastName = txtLastName.Text;
                bItem.SecondLastName = txtSecondLastName.Text;
                CultureInfo provider = CultureInfo.InvariantCulture;
                string date = txtBEDateofBirth.Text;
                string format = "d";
                DateTime dt = DateTime.ParseExact(date, format, provider);

                bItem.Dob = dt;
                bItem.BenefitsPercent = String.IsNullOrEmpty(txtPercentage.Text) ? (Decimal?)null : Decimal.Parse(txtPercentage.Text);
                bItem.ContactMainId = txtIDNo.Text;
                bItem.RelationshipToOwnerId = int.Parse(ddlRelationship.SelectedValue);
                bItem.PrimaryBeneficiaryId = BeneficiaryTypeId == 2 && ddlReplacing.SelectedIndex > 0 ? int.Parse(ddlReplacing.SelectedValue) : (int?)null;
                bItem.SeqNo = SeqNo;
                bItem.CreateUser = 1;

                //agregar campos de email, phones and addressses here

                Services.BeneficiaryManager.UpdatetBeneficiary(bItem);
                //consumir metodo para actualizar estos campos
            }
            else
            {
                const int contactRoleTypeId = 4;

                //Valido que este beneficiario no exista en el grid antes de agregarlo
                var Validation = ValidateCurrentBeneficiaries(false);
                if (Validation.Exists)
                {
                    string message = "This Beneficiary is already in the list, please verify and try again.";
                    ScriptManager.RegisterStartupScript(upBeneficiaries, upBeneficiaries.GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Invalid Beneficiary');", true);
                    return;
                }
                else if (Validation.IsSameId)
                {
                    string message = "There is already a Beneficiary with this ID, please try with other ID Number.";
                    ScriptManager.RegisterStartupScript(upBeneficiaries, upBeneficiaries.GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Invalid Value');", true);
                    return;
                }

                //Busco en el grid la suma de los porcentajes de los beneficiarios, para asegurarme de que el mismo nunca pase de 100%.
                if (!IsFuneral)
                {
                    if ((GetTotalPercentage() + Decimal.Parse(txtPercentage.Text)) > 100)
                    {
                        string message =
                            "The Percentage of all Beneficiaries can not exceed 100%, please verify and try again.";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Invalid Value');", true);
                        return;
                    }
                }
                //Seteo el Key del Item
                bItem.CorpId = Service.Corp_Id;
                bItem.RegionId = Service.Region_Id;
                bItem.CountryId = Service.Country_Id;
                bItem.DomesticregId = Service.Domesticreg_Id;
                bItem.StateProvId = Service.State_Prov_Id;
                bItem.CityId = Service.City_Id;
                bItem.OfficeId = Service.Office_Id;
                bItem.CaseSeqNo = Service.Case_Seq_No;
                bItem.HistSeqNo = Service.Hist_Seq_No;
                bItem.ContactRoleTypeId = contactRoleTypeId;

                bItem.ContactTypeId = 4; //Aqui el tipo de cliente 5 Compañia y  4 beneficiario.
                bItem.FirstName = txtFirstName.Text;
                bItem.MiddleName = txtMiddleName.Text;
                bItem.FirstLastName = txtLastName.Text;
                bItem.SecondLastName = txtSecondLastName.Text;
                CultureInfo provider = CultureInfo.InvariantCulture;
                string date = txtBEDateofBirth.Text;
                string format = "d";
                DateTime dt = DateTime.ParseExact(date, format, provider);
                bItem.Dob = dt;
                bItem.BenefitsPercent = String.IsNullOrEmpty(txtPercentage.Text) ? (Decimal?)null : Decimal.Parse(txtPercentage.Text);
                bItem.ContactMainId = txtIDNo.Text;
                bItem.RelationshipToOwnerId = int.Parse(ddlRelationship.SelectedValue);
                bItem.PrimaryBeneficiaryId = BeneficiaryTypeId == 2 && ddlReplacing.SelectedIndex > 0 ? int.Parse(ddlReplacing.SelectedValue) : (int?)null;
                bItem.IssuedBy = "-";
                bItem.CreateUser = 1;
                bItem.DocumentBinary = String.IsNullOrEmpty(hdnUploadedPDFPath.Text) ? null : Tools.ReadBinaryFile(FileName);

                //agregar campos telefonos, email, direccion aqui
                // string phone = Phones.Text;
                //string email = Email.Text;
                //string address = Address.Text;
                /*     Entity.UnderWriting.Entities.Contact.Address direccion = new Entity.UnderWriting.Entities.Contact.Address();
                     direccion.CountryId = Service.Country_Id;
                     direccion.DomesticregId = Service.Domesticreg_Id;
                     direccion.StateProvId = Service.State_Prov_Id;
                     direccion.CityId = Service.City_Id;
                     direccion.RegionId = Service.Region_Id;


                     Services.ContactManager.SetAddress(direccion);
     */
                Services.BeneficiaryManager.InsertBeneficiary(bItem);
                //consumir metodo para insertar estos detalles en un contacto

              

                SendFileToOnBase(Add,25, 1, FileName);
            }

            //Limpio los Campos y refresco la Data de los Grids
            btnBEClear_Click(null, null);
            FillData();
        }

        public void SendFileToOnBase(Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation Add, int catid, int typeid, string pdfpath)
        {
            try
            {
                Services ServicesOn = new Services();
                string TemplateIndexFile = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LifeOnBaseTemplatePath"]);
                string documentType = "D";

                ServicesOn.SendFileToOnBase(Add,TemplateIndexFile,
                                             documentType,
                                             catid,
                                             typeid,
                                             pdfpath);
            }
            catch (Exception ex)
            {
                Services.PolicyManager.InsertLog(new Entity.UnderWriting.Entities.Policy.LogParameter
                {
                    LogTypeId = 3,
                    CorpId = Service.Corp_Id,
                    CompanyId = Service.CompanyId,
                    ProjectId = Service.ProjectId,
                    Identifier = Guid.NewGuid(),
                    LogValue = "Se encontro un problema con el proceso OnBaseTranfer Beneficiary, Detalle: " + ex.Message.ToString()
                });
            }
        }

        protected void btnBEClear_Click(object sender, EventArgs e)
        {
            ClearFields(false);

            if (btnBEClear.Text.ToLower() == "cancel")
                SetButtons(false, false);
        }

        protected void btnFile_Click(object sender, EventArgs e)
        {
            var gridRow = (GridViewRow)((LinkButton)sender).NamingContainer;

            //Obtener Informacion del Documento desde el Grid
            var ContactId = int.Parse(gvBeneficiaries.DataKeys[gridRow.RowIndex]["ContactId"].ToString());
            var DocumentCategoryId = int.Parse(gvBeneficiaries.DataKeys[gridRow.RowIndex]["DocumentCategoryId"].ToString());
            var DocumentTypeId = int.Parse(gvBeneficiaries.DataKeys[gridRow.RowIndex]["DocumentTypeId"].ToString());
            var DocumentId = int.Parse(gvBeneficiaries.DataKeys[gridRow.RowIndex]["DocumentId"].ToString());

            string documentType = "D";
            Services ServicesOn = new Services();
            Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation Add = new Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation()
            {
                CorpId = Service.Corp_Id,
                RegionId = Service.Region_Id,
                CountryId = Service.Country_Id,
                DomesticregId = Service.Domesticreg_Id,
                StateProvId = Service.State_Prov_Id,
                CityId = Service.City_Id,
                OfficeId = Service.Office_Id,
                CaseSeqNo = Service.Case_Seq_No,
                HistSeqNo = Service.Hist_Seq_No,
                Contact_ID = ContactId
            };
            byte[] pdfOnBase = ServicesOn.ViewFileFromOnBase(Add,documentType, DocumentCategoryId, DocumentTypeId);

            if (pdfOnBase == null)
            {
                //Buscar Data del documento.
                var beneficiaryDoc = Services.BeneficiaryManager.GetIdDocument(ContactId, DocumentCategoryId, DocumentTypeId, DocumentId);

                //Buscar control del PDF Viewer y pasarle el documento a mostrar.
                if (beneficiaryDoc.DocumentBinary != null)
                {
                    ViewPDF(beneficiaryDoc.DocumentBinary);
                }
            }
            else
            {
                ViewPDF(pdfOnBase);
            }
        }

        public void ViewPDF(byte[] PdfFile)
        {
            var pdfViewerControl = (PdfViewer)Page.Master.FindControl("Right").FindControl("Right").FindControl("UCPdfViewer").FindControl("Viewer");
            pdfViewerControl.PdfSourceBytes = PdfFile;
            pdfViewerControl.DataBind();
            ((HiddenField)Page.Master.FindControl("Right").FindControl("Right").FindControl("hfMenuCasesRight")).Value = "pdfViewer";
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var gridRow = (GridViewRow)((LinkButton)sender).NamingContainer;

            txtFirstName.Text = ((Label)gridRow.FindControl("lblFirstName")).Text;
            txtMiddleName.Text = ((Label)gridRow.FindControl("lblMiddleName")).Text;
            txtLastName.Text = ((Label)gridRow.FindControl("lblFirstLastName")).Text;
            txtSecondLastName.Text = ((Label)gridRow.FindControl("lblSecondLastName")).Text;
            txtBEDateofBirth.Text = ((Label)gridRow.FindControl("lblDob")).Text;
            txtPercentage.Text = String.IsNullOrEmpty(((Label)gridRow.FindControl("lblBenefitsPercent")).Text) ? "0" : decimal.Parse(((Label)gridRow.FindControl("lblBenefitsPercent")).Text.Replace("%", "")).ToString("F2");
            txtIDNo.Text = ((Label)gridRow.FindControl("lblContactID")).Text;

            var RelationshipID = gvBeneficiaries.DataKeys[gridRow.RowIndex]["RelationshipToOwnerId"].ToString();
            UnderWriting.Common.Tools.SelectIndexByValue(ref ddlRelationship, RelationshipID, false);

            if (hdnBeneficiarieType.Value == "2" || hdnBeneficiarieType.Value == "4")
            {
                var RelatedID = gvBeneficiaries.DataKeys[gridRow.RowIndex]["PrimaryBeneficiaryId"] == null ? "-1" : gvBeneficiaries.DataKeys[gridRow.RowIndex]["PrimaryBeneficiaryId"].ToString();
                ddlReplacing.SelectedValue = RelatedID;
            }

            gvBeneficiaries.Enabled = false;
            hdnIsEdit.Value = "true";
            hdnEditIndex.Value = gridRow.RowIndex.ToString();
            txtMiddleName.Attributes.Remove("Validation");
            //Set Buttons
            SetButtons(false, true);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            var gridRow = (GridViewRow)((Button)sender).NamingContainer;

            var ContactId = int.Parse(gvBeneficiaries.DataKeys[gridRow.RowIndex]["ContactId"].ToString());
            var ContactRoleTypeId = int.Parse(gvBeneficiaries.DataKeys[gridRow.RowIndex]["ContactRoleTypeId"].ToString());

            Services.BeneficiaryManager.DeleteBeneficiary(Service.Corp_Id, Service.Region_Id, Service.Country_Id, Service.Domesticreg_Id, Service.State_Prov_Id, Service.City_Id, Service.Office_Id, Service.Case_Seq_No, Service.Hist_Seq_No, ContactId, ContactRoleTypeId);
            FillData();
        }
        #endregion

        protected void fuBenediciarieFile_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            var message = "";
            try
            {
                var file = e.UploadedFile;
                if (file.IsValid)
                {
                    var fileName = Tools.GetSerialId() + "~~" + file.FileName;
                    var savePath = Service.TempFilePath + "\\" + fileName;
                    file.SaveAs(savePath);

                    message = String.Format("{{ \"file\": \"{0}\", \"error\": \"{1}\"}}", fileName, "");
                }
                else
                    message = String.Format("{{ \"file\": \"{0}\", \"error\": \"{1}\"}}", "", "Error");
            }
            catch (Exception ex)
            {
                message = String.Format("{{ \"file\": \"{0}\", \"error\": \"{1}\"}}", "", ex.Message);
            }
            e.CallbackData = message;
        }

    }
}