﻿using Entity.UnderWriting.Entities;
using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Globalization;
using System.Linq;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle
{
    public partial class UCIllustrationInformation : UC, IUC
    {
        public string IllustrationStatusCode
        {
            get { return ViewState["IllustrationStatusCode"] == null ? null : ViewState["IllustrationStatusCode"].ToString(); }
            set { ViewState["IllustrationStatusCode"] = value; }
        }

        public bool HasDiscount
        {
            get { return ViewState["HasDiscount"].ToBoolean(); }
            set { ViewState["HasDiscount"] = value; }
        }

        public bool AccidentRateVisible
        {
            get { return ViewState["AccidentRateVisible"].ToBoolean(); }
            set { ViewState["AccidentRateVisible"] = value; }
        }

        public bool IsFinanced
        {
            get { return ViewState["IsFinanced"].ToBoolean(); }
            set { ViewState["IsFinanced"] = value; }
        }

        public bool HasSurcharge
        {
            get { return ViewState["HasSurcharge"].ToBoolean(); }
            set { ViewState["HasSurcharge"] = value; }
        }

        public Utility.TipoRiesgo Riesgo
        {
            get
            {
                var result = (Utility.TipoRiesgo)ViewState["Riesgo"];
                return result;
            }
            set
            {
                ViewState["Riesgo"] = value;
            }
        }

        public Utility.BlackListType BlackListType
        {
            get
            {
                var result = (Utility.BlackListType)ViewState["BlackListType"];
                return result;
            }
            set
            {
                ViewState["BlackListType"] = value;
            }
        }

        public DateTime? IllustrationDate
        {
            get { return ViewState["IllustrationDate"] == null ? null : ViewState["IllustrationDate"].ToString().IsDateReturnNull(); }
            set { ViewState["IllustrationDate"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pdfViewerMyPreviewPDF.LicenseKey = System.Configuration.ConfigurationManager.AppSettings["PDFViewer"];
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Translator(string.Empty);
        }

        public void Translator(string Lang)
        {
            Session["IllustrationStatusCode"] = IllustrationStatusCode;
            txtStatus.Text = ("Illustration_" + IllustrationStatusCode).Translate();

            if (ObjServices.isChangingLang)
                FillData();
        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public void edit()
        {
            throw new NotImplementedException();
        }

        public void FillData()
        {
            var TabSelected = (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), ObjServices.hdnQuotationTabs);

            var illustration = ObjServices.getillustrationData();

            if (illustration == null) return;

            var Financed = illustration.Financed.GetValueOrDefault();
            var MonthlyPayment = illustration.MonthlyPayment;
            var Period = illustration.Period;
            this.IsFinanced = Financed;
            SetValueTxtFinanced();
            setCouponInfo();
            ObjServices.MonthlyPayment = MonthlyPayment;
            ObjServices.Period = Period;
            var TaxPercentage = 16;
            var TasaCalc = (illustration.TaxPercentage.HasValue ? illustration.TaxPercentage : TaxPercentage) / 100;
            ObjServices.TaxPercentage = (double)TasaCalc.GetValueOrDefault();
            var TaxAmount = illustration.AnnualPremium * TasaCalc;
            ObjServices.annualPremium = illustration.AnnualPremium + TaxAmount;
            lnkContractView.CssClass = Financed ? "view_file" : "aspNetDisabled view_file_dbl";
            lnkContractView.Enabled = Financed;            
            var isEffectivePolicy = ObjServices.StatusNameKey == Utility.IllustrationStatus.Effective.Code();
            var isVisibleLoanNumber = (Financed && isEffectivePolicy);
            lnkContractView.Visible = !isEffectivePolicy && Financed;
            ObjServices.LoanPetitionNo = illustration.LoanPetitionNo;
            pnLoanNumber.Visible = isVisibleLoanNumber;
            var cssError = "color: yellow !important;  background: red;";
            var isError = string.IsNullOrEmpty(illustration.LoanPetitionNo) && isEffectivePolicy;
            ObjServices.ECreateLoanKCO = isError;
            txtLoanNumber.Text = isError ? "Se produjo Error creando el prestamo" : illustration.LoanPetitionNo;
            btnRecreateLoanKCO.Visible = Financed && isError;

            if (isError)
                txtLoanNumber.Attributes.Add("style", cssError);
            else
                txtLoanNumber.Attributes.Remove("style");

            var BlackListTypeNameKey = illustration.BlacklistMember;
            Riesgo = (Utility.TipoRiesgo)Enum.Parse(typeof(Utility.TipoRiesgo), illustration.TipoRiesgoNameKey == "N/A" ? "NONE" : illustration.TipoRiesgoNameKey);
            BlackListType = (Utility.BlackListType)Enum.Parse(typeof(Utility.BlackListType), BlackListTypeNameKey == "N/A" || string.IsNullOrEmpty(BlackListTypeNameKey) ? "NONE" : BlackListTypeNameKey);

            ObjServices.BlackListHasProblem = BlackListTypeNameKey != "NM" && BlackListTypeNameKey != "N/A" && !string.IsNullOrEmpty(BlackListTypeNameKey);

            ObjServices.BlacklistCheck = illustration.BlacklistCheck;
            ObjServices.BlacklistCheckUser = illustration.BlacklistCheckUser;
            ObjServices.BlacklistCheckUserName = illustration.BlacklistCheckUserName;
            ObjServices.BlacklistMember = illustration.BlacklistMember;

            HasDiscount = illustration.HasDiscount;
            txtIllustrationNoTemp.Text = illustration.PolicyNoTemp;
            txtIllustrationNoTemp.Visible = (!String.IsNullOrEmpty(illustration.PolicyNoTemp) && illustration.PolicyNo != illustration.PolicyNoTemp);
            txtIllustrationNo.Text = illustration.PolicyNo;
            IllustrationDate = illustration.QuoDate;
            txtIllustrationDate.Text = illustration.QuoDate.ToString("dd-MMM-yyyy hh:mm tt", CultureInfo.InvariantCulture).ToUpper();
            txtInsuredAmount.Text = illustration.InsuredAmount.ToFormatCurrency();
            txtTotalPremium.Text = illustration.AnnualPremium.ToFormatCurrency();
            txtOffice.Text = illustration.OfficeDesc;
            txtComercialAgent.Text = illustration.SupervisorAgentName;

            AccidentRateVisible = (TabSelected == Utility.Tabs.lnkSubscriptions) && (ObjServices.IsSuscripcionQuotRole ||
                                                                                     ObjServices.IsSuscripcionManagerQuotRole ||
                                                                                     ObjServices.IsSucripcionDirectorQuotRole ||
                                                                                     ObjServices.isUserCot);

            txtAgentAccidentRate.Visible = AccidentRateVisible;
            txtVendorAccidentRate.Visible = AccidentRateVisible;

            pnAgent.CssClass = string.Format("{0} {1} {2}", "label_plus_input par ", (!AccidentRateVisible ? "" : "inputDoble"), " pc");
            pnVendor.CssClass = string.Format("{0} {1} {2}", "label_plus_input par ", (!AccidentRateVisible ? "" : "inputDoble"), " pc");

            txtAgentAccidentRate.Text = illustration.AgentAccidentRate.ToFormatNumeric() + "%";
            txtAgent.Text = illustration.AgentName;
            txtVendorAccidentRate.Text = illustration.VendorAccidentRate.ToFormatNumeric() + "%";
            txtChannel.Text = illustration.DistributionDesc;
            IllustrationStatusCode = illustration.PolicyStatusNameKey;
            txtBusinessLine.Text = illustration.BlDesc;
            btnSeeDiscount.Enabled = HasDiscount;
            
            if (!HasDiscount)
                btnSeeDiscount.Style.Add("Visibility","hidden");

            ObjServices.PolicyOffice = illustration.OfficeDesc;
            ObjServices.InsuranceAmount = illustration.InsuredAmount.GetValueOrDefault().ToDecimal();
            DateTime? BeginDate = null;
            DateTime? EndDate = null;

            BeginDate = illustration.EffectiveDate.HasValue ? illustration.EffectiveDate.GetValueOrDefault() : DateTime.Now;
            EndDate = illustration.PolicyExpirationDate.HasValue ? illustration.PolicyExpirationDate.Value : BeginDate.Value.AddYears(1);

            txtEffectiveDate.Text = BeginDate.HasValue ? BeginDate.Value.ToString("dd-MMM-yyyy hh:mm tt", CultureInfo.InvariantCulture) : string.Empty;
            txtExpirationDate.Text = EndDate.HasValue ? EndDate.Value.ToString("dd-MMM-yyyy hh:mm tt", CultureInfo.InvariantCulture) : string.Empty;

            var Css = string.Format("{0}", btnSeeDiscount.Enabled ? "view_file" : "view_file_dbl");
            btnSeeDiscount.CssClass = string.Format("{0}", Css);

            HasSurcharge = illustration.HasSurcharge;
            btnSeeSurcharge.Enabled = HasSurcharge;
       
            if (!HasSurcharge)
                btnSeeSurcharge.Style.Add("Visibility", "hidden");
       
            btnSeeSurcharge.CssClass = string.Format("{0}", btnSeeSurcharge.Enabled ? "view_file" : "view_file_dbl");

            imgRiesgo.ImageUrl = !string.IsNullOrEmpty(illustration.TipoRiesgoNameKey) ? Utility.GetImgRiesgo(Riesgo)
                                                                                       : string.Empty;

            imgRiskLevel.ImageUrl = !string.IsNullOrEmpty(illustration.RiskLevel) ? Utility.GetImgNivelRiesgo((Utility.TipoNivelRiesgo)Enum.Parse(typeof(Utility.TipoNivelRiesgo), illustration.RiskLevel.Replace(" ", "")))
                                                                                  : string.Empty;
            ImgBlackList.ImageUrl = Utility.GetImgBlackList(BlackListType);

            txtRiskLevel.Text = illustration.RiskLevel == "NONE" ? illustration.RiskLevel.Replace("NONE", "N/A") : illustration.RiskLevel.Capitalize(' ');

            Session["IllustrationInspectorAgentId"] = illustration.InspectorAgentId;
            Session["IllustrationSuscriptorAgentId"] = illustration.SubscriberAgentId;
            txtProratedPremium.Visible = false;



            var RequestType = (Utility.RequestType)Enum.Parse(typeof(Utility.RequestType), illustration.RequestTypeDesc.Replace(" ", "").MyRemoveInvalidCharactersFilName());

            if (ObjServices.Bandeja == "Auto" && (RequestType != Utility.RequestType.Exclusion && RequestType != Utility.RequestType.Cambios))
                txtTasa.Text = ((illustration.AnnualPremium / illustration.InsuredAmount) * 100).ToFormatNumeric() + "%";
            else
                pnTasa.Visible = false;

            if (RequestType == Utility.RequestType.Inclusion || RequestType == Utility.RequestType.Exclusion)
            {
                txtTotalPremium.Attributes.Add("style", "width: 25% !important;");
                txtProratedPremium.Attributes.Add("style", "width: 25% !important;");
                txtProratedPremium.Text = "$" + illustration.ProratedPremium.GetValueOrDefault().ToString("#0,0.00", CultureInfo.InvariantCulture);
                txtProratedPremium.Visible = true;

                dvApplyDays.Visible = true;
                txtApplyDays.Text = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Day, BeginDate.GetValueOrDefault().Date, EndDate.GetValueOrDefault().Date).ToString();
            }

            ObjServices.isExclusion = (RequestType == Utility.RequestType.Exclusion);
            ObjServices.isVehicleChange = (RequestType == Utility.RequestType.Cambios);

            ltTotalPremiumWithoutTax.Text = string.Concat("<span>", RequestType == Utility.RequestType.Emision || RequestType == Utility.RequestType.InclusionDeclarativa ? Resources.TotalPremiumWithoutTax : Resources.TotalPremiumWithoutTaxProrratedPremium, "</span>");

            if (ObjServices.isExclusion || ObjServices.isVehicleChange)
            {
                lnkContractView.CssClass = "aspNetDisabled view_file_dbl";
                lnkContractView.Enabled = false;
            }


        }

        public string GetIllustrationNo()
        {
            return txtIllustrationNo.Text;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void ClearData()
        {
            throw new NotImplementedException();
        }

        protected void btnSeeIllustration_Click(object sender, EventArgs e)
        {
            //Ver los decuentos aplicados
            UCSeeDiscount.Initialize();
            ModalPopupSeeDiscount.Show();
            hdnShowDiscount.Value = "true";
        }

        protected void btnSeeDiscount_PreRender(object sender, EventArgs e)
        {
            txtDiscount.Text = HasDiscount ? Resources.YesLabel : Resources.NoLabel;
            btnSeeDiscount.Attributes.Add("title", RESOURCE.UnderWriting.NewBussiness.Resources.seediscounts);
        }

        protected void txtFinancialClearance_PreRender(object sender, EventArgs e)
        {
            //Traducir
            txtFinancialClearance.Text = Utility.GetDescRiesgo(Riesgo);
        }

        protected void btnSeeSurcharge_PreRender(object sender, EventArgs e)
        {
            txtSurcharge.Text = HasSurcharge ? Resources.YesLabel : Resources.NoLabel;
            btnSeeSurcharge.Attributes.Add("title", RESOURCE.UnderWriting.NewBussiness.Resources.SeeSurcharge);
        }

        protected void btnSeeSurcharge_Click(object sender, EventArgs e)
        {
            var IllustrationsVehiclePage = Page as WEB.NewBusiness.NewBusiness.Pages.IllustrationsVehicle;
            if (IllustrationsVehiclePage != null)
            {
                var ApplySurchargeUC = Utility.GetAllChildren(IllustrationsVehiclePage).FirstOrDefault(uc => uc is UCPopupApplySurcharge);
                if (ApplySurchargeUC != null)
                    (ApplySurchargeUC as UCPopupApplySurcharge).SeeAllSurcharges();
            }
        }

        protected void txtBlackList_PreRender(object sender, EventArgs e)
        {
            txtBlackList.Text = Utility.GetDescBlackList(BlackListType);
        }

        protected void lnkContractView_Click(object sender, EventArgs e)
        {
            try
            {
                //Objeto de la Data de la Poliza
                var PolicyData = ObjServices.oPolicyManager.GetPolicy(ObjServices.Corp_Id, ObjServices.Region_Id, ObjServices.Country_Id, ObjServices.Domesticreg_Id, ObjServices.State_Prov_Id, ObjServices.City_Id
                 , ObjServices.Office_Id, ObjServices.Case_Seq_No, ObjServices.Hist_Seq_No);

                //Objeto de la data del Contacto
                var dataContact = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, PolicyData.ContactId, ObjServices.Language.ToInt());

                if (this.IsFinanced)
                {
                    if (!dataContact.CreditCardTypeId.HasValue || string.IsNullOrEmpty(dataContact.CreditCardNumber) || string.IsNullOrEmpty(dataContact.CardHolder))
                    {
                        this.MessageBox(Resources.DomiciliationValidation);
                        return;
                    }
                }

                byte[] XMLByteArray = null;
                ////Mostrar la cotizacion generada via thunderhead
                //Generar el Documento XML con la data del contrato
                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                {
                    XMLByteArray = ObjServices.GenerateXMLContratoKCO(ObjServices.Corp_Id,
                                                                      ObjServices.Region_Id,
                                                                      ObjServices.Country_Id,
                                                                      ObjServices.Domesticreg_Id,
                                                                      ObjServices.State_Prov_Id,
                                                                      ObjServices.City_Id,
                                                                      ObjServices.Office_Id,
                                                                      ObjServices.Case_Seq_No,
                                                                      ObjServices.Hist_Seq_No,
                                                                      ServerMaptPath
                                                                      );
                }
                else if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                {
                    XMLByteArray = ObjServices.GenerateXMLContratoKCOPropiedad(ObjServices.Corp_Id,
                                                                               ObjServices.Region_Id,
                                                                               ObjServices.Country_Id,
                                                                               ObjServices.Domesticreg_Id,
                                                                               ObjServices.State_Prov_Id,
                                                                               ObjServices.City_Id,
                                                                               ObjServices.Office_Id,
                                                                               ObjServices.Case_Seq_No,
                                                                               ObjServices.Hist_Seq_No,
                                                                               ServerMaptPath
                                                                              );
                }

                var PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray,
                                                                 ThunderheadWrap.Service.TemplateType.ContratoKSI,
                                                                 ObjServices.ProductLine == Utility.ProductLine.Auto ? ThunderheadWrap.Service.BusinessLine.Vehicle
                                                                                                                     : ThunderheadWrap.Service.BusinessLine.IncendioLineasAliadas);

                pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                pdfViewerMyPreviewPDF.DataBind();

                //Visualizar el pdf
                hdnShowPopAmortizationTable.Value = "true";
                //udpQuotationPrev.Update();
                ModalPopupAmortizationTable.Show();
                this.ExcecuteJScript("$('#popupBhvr_backgroundElement').css('display', 'none');");
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }
        }

        protected void btnRecreateLoanKCO_Click(object sender, EventArgs e)
        {
            var hasError = false;
            var Email = string.Empty;

            try
            {
                //Objeto de la Data de la Poliza
                var PolicyData = ObjServices.oPolicyManager.GetPolicy(ObjServices.Corp_Id, ObjServices.Region_Id, ObjServices.Country_Id, ObjServices.Domesticreg_Id, ObjServices.State_Prov_Id, ObjServices.City_Id
                 , ObjServices.Office_Id, ObjServices.Case_Seq_No, ObjServices.Hist_Seq_No);

                //Objeto de la data del Contacto
                var dataContact = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, PolicyData.ContactId, ObjServices.Language.ToInt());

                if (this.IsFinanced)
                {
                    if (!dataContact.CreditCardTypeId.HasValue || string.IsNullOrEmpty(dataContact.CreditCardNumber) || string.IsNullOrEmpty(dataContact.CardHolder))
                    {
                        this.MessageBox(Resources.DomiciliationValidation);
                        return;
                    }

                    var IsPerson = !dataContact.IsCompany;

                    //Si es una persona
                    if (IsPerson)
                    {
                        if (!dataContact.homeOwner.HasValue)
                        {
                            this.MessageBox(Resources.homeOwnerValidation);
                            return;
                        }

                        if (!dataContact.qtyPersonsDepend.HasValue)
                        {
                            this.MessageBox(Resources.DepValidation);
                            return;
                        }

                        if (!dataContact.MaritalStatId.HasValue)
                        {
                            this.MessageBox(Resources.MaritalStatusValidationMessage);
                            return;
                        }
                    }
                    //Si es una compañia
                    else
                    {
                        if (!dataContact.qtyEmployees.HasValue)
                        {
                            this.MessageBox(Resources.QtyemployeeValidation);
                            return;
                        }
                    }
                }

                Utility.ContactParameter contactParameter;

                //Validar el contacto
                if (dataContact == null)
                    throw new Exception(Resources.QuotationDontHasContact);

                //Correo del contacto
                var DataEmail = ObjServices.oContactManager
                               .GetCommunicatonEmail(ObjServices.Corp_Id, dataContact.ContactId, ObjServices.Language.ToInt());

                //Validar Email
                if (DataEmail.Any())
                {
                    var EmailResult = DataEmail
                                     .FirstOrDefault(x => x.IsPrimary);

                    if (EmailResult == null)
                        EmailResult = DataEmail.FirstOrDefault();

                    if (EmailResult != null)
                        Email = EmailResult.EmailAdress;
                }

                //Direccion del Contacto
                var oAddress = ObjServices.oContactManager
                              .GetCommunicatonAdress(ObjServices.Corp_Id, dataContact.ContactId, ObjServices.Language.ToInt())
                              .FirstOrDefault(x => x.DirectoryTypeId == 5);

                var Direccion = (oAddress != null) ? oAddress.StreetAddress.Replace("'", "`") : "-";

                //Telefonos del contacto
                var oPhones = ObjServices.oContactManager
                              .GetCommunicatonPhone(ObjServices.Corp_Id, dataContact.ContactId, ObjServices.Language.ToInt());

                //Data Telefonos

                //Casa
                var DataTelefonoCasa = oPhones
                                       .FirstOrDefault(x => x.DirectoryTypeId == 6);
                //Trabajo
                var DataTelefonoTrabajo = oPhones
                                       .FirstOrDefault(x => x.DirectoryTypeId == 7);
                //Celular
                var DataTelefonoCelular = oPhones
                                       .FirstOrDefault(x => x.DirectoryTypeId == 8);

                //Casa            
                var TelefonoCasa = DataTelefonoCasa != null ?
                                   string.Format("{0}{1}{2}", DataTelefonoCasa.CountryCode, DataTelefonoCasa.AreaCode, DataTelefonoCasa.PhoneNumber)
                                   : default(string);
                //Trabajo
                var TelefonoTrabajo = DataTelefonoTrabajo != null ?
                                   string.Format("{0}{1}{2}", DataTelefonoTrabajo.CountryCode, DataTelefonoTrabajo.AreaCode, DataTelefonoTrabajo.PhoneNumber)
                                   : default(string);
                //Celular
                var TelefonoCelular = DataTelefonoCelular != null ?
                                   string.Format("{0}{1}{2}", DataTelefonoCelular.CountryCode, DataTelefonoCelular.AreaCode, DataTelefonoCelular.PhoneNumber)
                                   : default(string);

                //Id Doc
                var dataId = ObjServices.oContactManager.GetAllIdDocumentInformation(PolicyData.ContactId, ObjServices.Language.ToInt());

                var tipoCedula = default(string);
                var Registro = dataId.FirstOrDefault();
                var ContactIdType = Registro.ContactIdType;
                var CedulaRncOther = Registro.Id;
                var ExpirationDate = Registro.ExpireDate;

                /*
                 Contact_Id_Type	Contact_Id_Type_Desc
                        0	            Other
                        1	            Id
                        2	            Passport
                        3	            Driver License
                        4	            Beneficiary Document
                        5	            Company Registration
                        6	            Birth Certificate
                        7	            School Registration
                        8	            Auto Generated ID
                 */

                switch (ContactIdType)
                {   //Other 
                    case 0: tipoCedula = "2"; break;
                    //Cedula
                    case 1: tipoCedula = "1"; break;
                    //RNC
                    case 5: tipoCedula = "0"; break;
                    default: tipoCedula = "2"; break;
                }

                short? ksitipoCedula = null;

                switch (ContactIdType)
                {
                    case 1:
                    case 3:
                        ksitipoCedula = 1; //Cedula
                        break;
                    case 2:
                        ksitipoCedula = 2; //Pasaporte
                        break;
                    case 5:
                        ksitipoCedula = 3; //RNC
                        break;
                }

                contactParameter = new Utility.ContactParameter
                {
                    Email = Email,
                    Direccion = Direccion,
                    TelefonoCasa = !string.IsNullOrEmpty(TelefonoCasa) ? TelefonoCasa.Replace("_", "") : string.Empty,
                    TelefonoTrabajo = !string.IsNullOrEmpty(TelefonoTrabajo) ? TelefonoTrabajo.Replace("_", "") : string.Empty,
                    TelefonoCelular = !string.IsNullOrEmpty(TelefonoCelular) ? TelefonoCelular.Replace("_", "") : string.Empty,
                    tipoCedula = ksitipoCedula,
                    CedulaRncOther = !string.IsNullOrEmpty(CedulaRncOther) ? CedulaRncOther.Replace("_", "") : string.Empty,
                    oAddress = oAddress,
                    oPhones = oPhones
                };

                var resultCreateLoan = ObjServices.kcoCreateLoan(dataContact, PolicyData, contactParameter, PolicyData.PolicyNo);
                hasError = ObjServices.ErrorCode.Contains(resultCreateLoan.Code);

                if (!hasError)
                {
                    var LoanNumber = resultCreateLoan.LoanNo;

                    //Actualizar le numero de prestamo en caso de que esta poliza sea financiada
                    ObjServices.oPolicyManager.SetPolicyLoanNo
                    (
                        ObjServices.Corp_Id,
                        ObjServices.Region_Id,
                        ObjServices.Country_Id,
                        ObjServices.Domesticreg_Id,
                        ObjServices.State_Prov_Id,
                        ObjServices.City_Id,
                        ObjServices.Office_Id,
                        ObjServices.Case_Seq_No,
                        ObjServices.Hist_Seq_No,
                        LoanNumber,
                        ObjServices.UserID.GetValueOrDefault()
                    );

                    //Actualizar la tabla temporal Grid de la Flat Table
                    ObjServices.UpdateTempTable(PolicyData.PolicyNo, ObjServices.UserID.GetValueOrDefault());

                    FillData();

                    //Copiar los archivo en Onbase
                    ObjServices.GenerateOnBaseFiles(ObjServices.Corp_Id,
                                                    ObjServices.Region_Id,
                                                    ObjServices.Country_Id,
                                                    ObjServices.Domesticreg_Id,
                                                    ObjServices.State_Prov_Id,
                                                    ObjServices.City_Id,
                                                    ObjServices.Office_Id,
                                                    ObjServices.Case_Seq_No,
                                                    ObjServices.Hist_Seq_No,
                                                    false,
                                                    Server.MapPath("~/NewBusiness/XML/"),
                                                    Financed: this.IsFinanced,
                                                    LoanNumber: LoanNumber,
                                                    CopyKCOFile: true,
                                                    CopyATLFile: false
                                                   );


                    var subject = string.Format("Bandeja - Se ha creado una solicitud de préstamo en KCO para la póliza número : {0}", PolicyData.PolicyNo);
                    var msg = string.Format("Notificación de creacion de prestamo en KCO Número de solicitud {0}", LoanNumber);
                    ObjServices.SendEmailKCOFinanced(msg, subject, "EmailSendNotificationFinancedPolicy");

                    var message = string.Format("El número de solicitud de prestamo {0} se ha creado en KREDICO", LoanNumber);
                    this.MessageBox(message);
                }
                else
                {
                    var isTest = System.Configuration.ConfigurationManager.AppSettings["isTestingQuotDebug"] == "true";
                    var msg = string.Format("Error creando la solicitud de prestamo en KREDICO \n\n detalle del error: {0}", resultCreateLoan.Message);
                    var subject = string.Format("Bandeja {0} - Error creando préstamo en KCO para la póliza número : {1}", isTest ? "Dev" : "Prod", PolicyData.PolicyNo);
                    ObjServices.SendEmailError(resultCreateLoan.Message, subject, "EmailSendErrorKCO");
                    throw new Exception(msg);
                }
            }
            catch (Exception ex)
            {
                base.DownloadErrorDescripcion(ex);
            }

        }

        private void SetValueTxtFinanced()
        {
            txtFinanced.Text = this.IsFinanced ? Resources.YesLabel : Resources.NoLabel;
            lnkContractView.Attributes.Add("title", RESOURCE.UnderWriting.NewBussiness.Resources.SeeFinanceContract);
        }

        protected void lnkContractView_PreRender(object sender, EventArgs e)
        {
            SetValueTxtFinanced();
        }

        private void setCouponInfo()
        {
            var idata = ObjServices.getillustrationData();
            var coupon = ObjServices.getCouponInfo(idata.PolicyNoTemp);

            if (coupon != null && !string.IsNullOrEmpty(coupon.CouponCode))
            {
                dvCouponInfo.Visible = true;
                txtCouponCode.Text = coupon.CouponCode;
                txtCouponDiscount.Text = coupon.CouponPercentageDiscount.GetValueOrDefault().ToString().Replace(".00", "").Replace(",00", "") + "%";
            }
        }
    }
}