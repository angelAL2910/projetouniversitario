﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.AddNewClient.Payment
{
    public partial class UCContainerACH : UC, IUC
    {
        public void edit() { }
        public void FillData() { }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Translator(string.Empty);
            this.UCACHDomicile._ddlAccountHolderRelationshipOwnerInsured.SelectedIndexChanged += ddlAccountHolderRelationshipOwnerInsured_SelectedIndexChanged;
        }

        public void Translator(string Lang){}

        public void Initialize() { }

        public delegate void SelectPaymemtForm(int PaymentSourceId, int PaymentSourceTypeId, int PaymentControlId);
        public event SelectPaymemtForm SelectPaymemtFormEvent;

        public delegate void RefreshPaymentDocuments();
        public event RefreshPaymentDocuments RefreshPaymentDocumentsEvent;
        public delegate void SaveDocumentDetail(int PaymentDetId);
        public event SaveDocumentDetail SaveDocumentDetailEvent;

        #region CONTROLES
        TextBox txtOriginationDate;
        DropDownList ddlFormofPayment;
        DropDownList ddlCardType;
        TextBox txtAccountHolderName;
        TextBox txtBankName;
        TextBox txtAccountNumber;
        TextBox txtRepeatAccountNumber;
        TextBox txtABANumber;
        DropDownList ddlAccountHolderRelationshipOwnerInsured;
        TextBox txtAmount;
        UserControl Controles;
        public void setControls()
        {
            switch (hfSelectControls.Value)
            {
                case "VACHDomicile":
                    Controles = UCACHDomicile;

                    break;
                case "VCACHOneTime":
                    Controles = UCACHOneTime;
                    break;
                case "VACHStbDomicile":
                    Controles = UCACHStbDomicile;
                    break;
                case "VACHStbOnetime":
                    Controles = UCACHStbOnetime;
                    break;
                default:
                    Controles = UCACHDomicile;
                    break;
            }

            /*BUSCO LOS CONTRLES QUE QUIERO GUARDAR*/
            txtOriginationDate = ((TextBox)Controles.FindControl("txtOriginationDate"));
            ddlFormofPayment = ((DropDownList)Controles.FindControl("ddlFormofPayment"));
            ddlCardType = ((DropDownList)Controles.FindControl("ddlCardType"));
            txtAccountHolderName = ((TextBox)Controles.FindControl("txtAccountHolderName"));
            txtBankName = ((TextBox)Controles.FindControl("txtBankName"));
            txtAccountNumber = ((TextBox)Controles.FindControl("txtAccountNumber"));
            txtRepeatAccountNumber = ((TextBox)Controles.FindControl("txtRepeatAccountNumber"));
            txtABANumber = ((TextBox)Controles.FindControl("txtABANumber"));
            ddlAccountHolderRelationshipOwnerInsured = ((DropDownList)Controles.FindControl("ddlAccountHolderRelationshipOwnerInsured"));
            txtAmount = ((TextBox)Controles.FindControl("txtAmount"));
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            UCACHDomicile._ddlAccountHolderRelationshipOwnerInsured.SelectedIndexChanged += ddlAccountHolderRelationshipOwnerInsured_SelectedIndexChanged;
            UCACHDomicile._ddlCardType.SelectedIndexChanged += ddlCardType_SelectedIndexChanged;
            UCACHDomicile._ddlFormofPayment.SelectedIndexChanged += ddlFormofPayment_SelectedIndexChanged;
            UCACHDomicile._btnSave.Click += btnSave_Click;
            UCACHDomicile._btnCancel.Click += btnCancel_Click;

            UCACHOneTime._ddlAccountHolderRelationshipOwnerInsured.SelectedIndexChanged += ddlAccountHolderRelationshipOwnerInsured_SelectedIndexChanged;
            UCACHOneTime._ddlCardType.SelectedIndexChanged += ddlCardType_SelectedIndexChanged;
            UCACHOneTime._ddlFormofPayment.SelectedIndexChanged += ddlFormofPayment_SelectedIndexChanged;
            UCACHOneTime._btnSave.Click += btnSave_Click;
            UCACHOneTime._btnCancel.Click += btnCancel_Click;

            UCACHStbDomicile._ddlAccountHolderRelationshipOwnerInsured.SelectedIndexChanged += ddlAccountHolderRelationshipOwnerInsured_SelectedIndexChanged;
            UCACHStbDomicile._ddlCardType.SelectedIndexChanged += ddlCardType_SelectedIndexChanged;
            UCACHStbDomicile._ddlFormofPayment.SelectedIndexChanged += ddlFormofPayment_SelectedIndexChanged;
            UCACHStbDomicile._btnSave.Click += btnSave_Click;
            UCACHStbDomicile._btnCancel.Click += btnCancel_Click;

            UCACHStbOnetime._ddlAccountHolderRelationshipOwnerInsured.SelectedIndexChanged += ddlAccountHolderRelationshipOwnerInsured_SelectedIndexChanged;
            UCACHStbOnetime._ddlCardType.SelectedIndexChanged += ddlCardType_SelectedIndexChanged;
            UCACHStbOnetime._ddlFormofPayment.SelectedIndexChanged += ddlFormofPayment_SelectedIndexChanged;
            UCACHStbOnetime._btnSave.Click += btnSave_Click;
            UCACHStbOnetime._btnCancel.Click += btnCancel_Click;
        }

        #region EVENTOS CONTROLES
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();

            //Inicializar el formulario 
            var WUCFormOfPayment = (WEB.NewBusiness.NewBusiness.UserControls.Payment.WUCFormOfPayment)this.Page.Master.FindControl("bodyContent")
                             .FindControl("PaymentContainer")
                             .FindControl("WUCFormOfPayment");

            if (!WUCFormOfPayment.isNullReferenceControl())
                WUCFormOfPayment.MethodSelectPaymemtForm(-1, -1, -1);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            save();
            RefreshPaymentDocumentsEvent();
        }

        protected void ddlFormofPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (DropDownList)sender;

            if ((drop).SelectedValue != "-1")
            {
                var InsItem = Utility.deserializeJSON<Utility.PaymentSource>((drop).SelectedValue);
                ObjServices.PaymentDetId = new Nullable<int>();
                SelectPaymemtFormEvent(InsItem.PaymentSourceId, InsItem.PaymentSourceTypeId, InsItem.PaymentControlId);
            }
            else
                SelectPaymemtFormEvent(-1, -1, -1);
        }

        protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlAccountHolderRelationshipOwnerInsured_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region COMMON METHODS
        public void save()
        {
            setControls();

            bool ProcessPayment = false;
            var InsItem = Utility.deserializeJSON<Utility.PaymentSource>(ddlFormofPayment.SelectedValue);
            var amount = Utility.IsDecimal(txtAmount.Text.Replace(",", "")) ?
                         Utility.IsDecimalReturnNull(txtAmount.Text.Replace(",", ""))
                         : 0;
            //Colocamos de forma fija en el parametro contactRoleType un 1 el cual es para el contacto propietario OWNER ponerle la relacion  si es un hijo etc.
            var contact = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, ObjServices.Region_Id, ObjServices.Country_Id, ObjServices.Domesticreg_Id, ObjServices.State_Prov_Id, ObjServices.City_Id, ObjServices.Office_Id, ObjServices.Case_Seq_No, ObjServices.Hist_Seq_No, null, 1, ObjServices.Language.ToInt());
            if (contact != null)
            {
                this.SetContactInfo(contact);
            }
            var Policy = ObjServices.oPolicyManager.GetPlanData(ObjServices.Corp_Id, ObjServices.Region_Id, ObjServices.Country_Id, ObjServices.Domesticreg_Id, ObjServices.State_Prov_Id
                , ObjServices.City_Id, ObjServices.Office_Id, ObjServices.Case_Seq_No, ObjServices.Hist_Seq_No);

            decimal Total = 0;

            Total = ObjServices.oPaymentManager.GetPayment
                   (
                         ObjServices.Corp_Id
                       , ObjServices.Region_Id
                       , ObjServices.Country_Id
                       , ObjServices.Domesticreg_Id
                       , ObjServices.State_Prov_Id
                       , ObjServices.City_Id
                       , ObjServices.Office_Id
                       , ObjServices.Case_Seq_No
                       , ObjServices.Hist_Seq_No
                       , ObjServices.PaymentId.Value
                   ).DueAmount.Value;

            decimal? Pay = 0;
            int? HasdomicilePayment = 0;
            //esta parte investiga si se esta editando un valor en caso de que si 
            //se excluye el pago a total
            if (ObjServices.PaymentDetId.HasValue)
            {
                Pay = ObjServices.oPaymentManager.GetAllApplyPaymentDetail
                  (
                        ObjServices.Corp_Id
                      , ObjServices.Region_Id
                      , ObjServices.Country_Id
                      , ObjServices.Domesticreg_Id
                      , ObjServices.State_Prov_Id
                      , ObjServices.City_Id
                      , ObjServices.Office_Id
                      , ObjServices.Case_Seq_No
                      , ObjServices.Hist_Seq_No
                      , ObjServices.PaymentId.Value
                      , ObjServices.Language.ToInt()
                  ).Where(det => det.PaymentDetId != ObjServices.PaymentDetId.Value).Sum(s => s.UsdCreditAmount - s.UsdDebitAmount);

                HasdomicilePayment = ObjServices.oPaymentManager.GetAllApplyPaymentDetail
                   (
                         ObjServices.Corp_Id
                       , ObjServices.Region_Id
                       , ObjServices.Country_Id
                       , ObjServices.Domesticreg_Id
                       , ObjServices.State_Prov_Id
                       , ObjServices.City_Id
                       , ObjServices.Office_Id
                       , ObjServices.Case_Seq_No
                       , ObjServices.Hist_Seq_No
                       , ObjServices.PaymentId.Value
                       , ObjServices.Language.ToInt()
                   ).Where(dom => dom.PaymentControlId == 2 && dom.PaymentDetId != ObjServices.PaymentDetId.Value).Count();
            }
            else
            {
                Pay = ObjServices.oPaymentManager.GetAllApplyPaymentDetail
                  (
                        ObjServices.Corp_Id
                      , ObjServices.Region_Id
                      , ObjServices.Country_Id
                      , ObjServices.Domesticreg_Id
                      , ObjServices.State_Prov_Id
                      , ObjServices.City_Id
                      , ObjServices.Office_Id
                      , ObjServices.Case_Seq_No
                      , ObjServices.Hist_Seq_No
                      , ObjServices.PaymentId.Value
                      , ObjServices.Language.ToInt()
                  ).Sum(s => s.UsdCreditAmount - s.UsdDebitAmount);

                HasdomicilePayment = ObjServices.oPaymentManager.GetAllApplyPaymentDetail
                   (
                         ObjServices.Corp_Id
                       , ObjServices.Region_Id
                       , ObjServices.Country_Id
                       , ObjServices.Domesticreg_Id
                       , ObjServices.State_Prov_Id
                       , ObjServices.City_Id
                       , ObjServices.Office_Id
                       , ObjServices.Case_Seq_No
                       , ObjServices.Hist_Seq_No
                       , ObjServices.PaymentId.Value
                       , ObjServices.Language.ToInt()
                   ).Where(dom => dom.PaymentControlId == 2).Count();
            }

            if (InsItem.PaymentControlId == 2) // domicile
            {
                if (HasdomicilePayment == 0)
                {
                    if ((amount >= (Policy.PeriodicPremium) && amount <= (Total - Pay) && amount > 0))
                        // si el pago  es mayor o igual a la prima y menor que la resta del total pago y lo que debe pagar entonces se puede realizar el pago
                        ProcessPayment = true;
                    else
                        this.MessageBox(RESOURCE.UnderWriting.NewBussiness.Resources.PeriodicPaymentWarning, Title: ObjServices.Language.ToString() == "en" ? "Warning" : "Advertencia");
                }
                else
                    this.MessageBox(RESOURCE.UnderWriting.NewBussiness.Resources.AccountDomiciled, null, null, true, Title: ObjServices.Language.ToString() == "en" ? "Warning" : "Advertencia");
            }
            else
            {
                if (amount <= (Total - Pay) && amount > 0)
                    // si el pago  es mayor o igual a la prima y menor que la resta del total pago y lo que debe pagar entonces se puede realizar el pago  
                    ProcessPayment = true;
                else
                    this.MessageBox(RESOURCE.UnderWriting.NewBussiness.Resources.PeriodicPaymentWarning, Title: ObjServices.Language.ToString() == "en" ? "Warning" : "Advertencia");
            }

            if (ProcessPayment)
            {
                /*primero verifico si existe un pago pendiente en la poliza antes de insertar el detalle*/
                if (!ObjServices.PaymentId.HasValue)
                {
                    Entity.UnderWriting.Entities.Payment.ApplyPayment item = new Entity.UnderWriting.Entities.Payment.ApplyPayment();

                    item.CorpId = ObjServices.Corp_Id;
                    item.CityId = ObjServices.City_Id;
                    item.CountryId = ObjServices.Country_Id;
                    item.RegionId = ObjServices.Region_Id;
                    item.StateProvId = ObjServices.State_Prov_Id;
                    item.DomesticregId = ObjServices.Domesticreg_Id;
                    item.OfficeId = ObjServices.Office_Id;
                    item.CaseSeqNo = ObjServices.Case_Seq_No;
                    item.HistSeqNo = ObjServices.Hist_Seq_No;

                    item.DueAmount = 0;
                    item.DueDate = DateTime.Now;
                    item.PaidAmount = 0;
                    item.PaymentStatusId = 2;
                    item.UserId = ObjServices.UserID.Value;
                    var payReturn = ObjServices.oPaymentManager.InsertPayment(item);
                    if (payReturn != null)
                        ObjServices.PaymentId = payReturn.PaymentId;
                }
                if (ObjServices.PaymentId.HasValue)
                {
                    Entity.UnderWriting.Entities.Payment.ApplyPaymentDetail itemDetail = new Entity.UnderWriting.Entities.Payment.ApplyPaymentDetail();

                    itemDetail.CorpId = ObjServices.Corp_Id;
                    itemDetail.CityId = ObjServices.City_Id;
                    itemDetail.CountryId = ObjServices.Country_Id;
                    itemDetail.RegionId = ObjServices.Region_Id;
                    itemDetail.StateProvId = ObjServices.State_Prov_Id;
                    itemDetail.DomesticregId = ObjServices.Domesticreg_Id;
                    itemDetail.OfficeId = ObjServices.Office_Id;
                    itemDetail.CaseSeqNo = ObjServices.Case_Seq_No;
                    itemDetail.HistSeqNo = ObjServices.Hist_Seq_No;
                    itemDetail.PaymentId = ObjServices.PaymentId.Value;


                    /*campos editables para agregar montos de la transaccion*/
                    itemDetail.Rate = 1; //los pagos son siempre en dolares


                    itemDetail.OriginCreditAmount = amount;
                    itemDetail.OriginDebitAmount = 0;
                    itemDetail.UsdCreditAmount = amount;
                    itemDetail.UsdDebitAmount = 0;

                    itemDetail.PaymentSourceId = InsItem.PaymentSourceId;
                    itemDetail.PaymentSourceTypeId = InsItem.PaymentSourceTypeId;
                    itemDetail.PaymentControlId = InsItem.PaymentControlId;
                    itemDetail.PaymentDetailSourceId = System.Guid.NewGuid().ToString();
                    itemDetail.CurrencyId = -1; //Oyeme esto ta fijo aqui pero en realidad tu sabe que se va a poner automaticamente en el procedure donde guarda
                    itemDetail.AccountTypeId = int.Parse(ddlCardType.SelectedValue);
                    itemDetail.UserId = ObjServices.UserID.Value;
                    itemDetail.DueDate = Utility.IsDateReturnNull(txtOriginationDate.Text);
                    itemDetail.RelationshipToOwnerOrInsured = int.Parse(ddlAccountHolderRelationshipOwnerInsured.SelectedValue);
                    itemDetail.PaymentStatusId = 2;//pendiente

                    itemDetail.EFTAccountHolder = txtAccountHolderName.Text.Trim();
                    itemDetail.EFTAccountHolderSource = txtBankName.Text.Trim();
                    itemDetail.EFTAccountNumber = txtAccountNumber.Text;

                    if (!txtABANumber.isNullReferenceObject())
                        itemDetail.EFTABANumber = txtABANumber.Text;

                    int DetailID = 0;
                    if (ObjServices.PaymentDetId.HasValue)
                    {
                        DetailID = ObjServices.PaymentDetId.Value;
                        itemDetail.PaymentDetId = ObjServices.PaymentDetId.Value;
                        ObjServices.oPaymentManager.UpdatePaymentDetail(itemDetail);
                    }
                    else
                    {
                        itemDetail.PaymentDetailSourceId = System.Guid.NewGuid().ToString();
                        DetailID = ObjServices.oPaymentManager.InsertPaymentDetail(itemDetail).PaymentDetId;
                    }

                    SaveDocumentDetailEvent(DetailID);
                    
                    //Inicializar el formulario 
                    var WUCFormOfPayment = (WEB.NewBusiness.NewBusiness.UserControls.Payment.WUCFormOfPayment)this.Page.Master.FindControl("bodyContent")
                                     .FindControl("PaymentContainer")
                                     .FindControl("WUCFormOfPayment");
                                 
                    if (!WUCFormOfPayment.isNullReferenceControl())
                        WUCFormOfPayment.MethodSelectPaymemtForm(-1, -1, -1);

                }

                ClearData();
            }
        }
        /// <summary>
        /// aqui es donde veo que control debo de llenar  por defecto estare UCACHDOMICILE
        /// </summary>
        public void FillDataSelectControl(String SelectControles, int PaymentSourceId, int PaymentSourceTypeId
            , int PaymentControlId, Entity.UnderWriting.Entities.Payment.ApplyPaymentDetail Payment = null)
        {
            /*AQUI BUSCO QUE CONTROL DEBO PRESENTAR PRIMERO*/
            ClearData();
            switch (SelectControles)
            {
                case "VACHDomicile":
                    MVACH.SetActiveView(VACHDomicile);
                    hfSelectControls.Value = "VACHDomicile";
                    break;

                case "VCACHOneTime":
                    MVACH.SetActiveView(VCACHOneTime);
                    hfSelectControls.Value = "VCACHOneTime";
                    break;

                case "VACHStbDomicile":
                    MVACH.SetActiveView(VACHStbDomicile);
                    hfSelectControls.Value = "VACHStbDomicile";
                    break;

                case "VACHStbOnetime":
                    MVACH.SetActiveView(VACHStbOnetime);
                    hfSelectControls.Value = "VACHStbOnetime";
                    break;
                default:
                    MVACH.SetActiveView(VACHDomicile);
                    hfSelectControls.Value = "VACHDomicile";
                    break;
            }

            /*BUSCO LOS OBJECTOS QUE NECESITO LLENAR DEL CONTROL SELECCIONADO*/
            setControls();

            ObjServices.GettingAllDropsJSON(ref ddlFormofPayment, WEB.NewBusiness.Common.Utility.DropDownType.PaymentSource, "PaymentSourceDesc"

                    , corpId: ObjServices.Corp_Id
                    , regionId: ObjServices.Region_Id
                    , countryId: ObjServices.Country_Id
                    , domesticregId: ObjServices.Domesticreg_Id
                    , stateProvId: ObjServices.State_Prov_Id
                    , cityId: ObjServices.City_Id
                    , officeId: ObjServices.Office_Id
                    , caseSeqNo: ObjServices.Case_Seq_No
                    , histSeqNo: ObjServices.Hist_Seq_No
                    , appliedByFreqOrCountry: true
                );

            string x2 = "{\"PaymentSourceId\":{1},\"PaymentSourceTypeId\":{2},\"PaymentControlId\":{3}}"
                     .Replace("{1}", PaymentSourceId.ToString())
                     .Replace("{2}", PaymentSourceTypeId.ToString())
                     .Replace("{3}", PaymentControlId.ToString())
                     ;

            ddlFormofPayment.SelectIndexByValueJSON(x2);


            if (ddlAccountHolderRelationshipOwnerInsured != null)
            {
                ObjServices.GettingAllDrops(ref ddlAccountHolderRelationshipOwnerInsured,
                                    Utility.DropDownType.RelationshipPayment,
                                   "RelationshipDesc",
                                   "RelationshipId",
                                    GenerateItemSelect: true
                                   );
            }

            if (ddlCardType != null)
            {
                ObjServices.GettingAllDrops(ref ddlCardType,
                                        Utility.DropDownType.PaymentType,
                                        "AccountDesc",
                                        "AccountTypeId",
                                        GenerateItemSelect: true,
                                        PaymentSourceId: PaymentSourceId,
                                        PaymentSourceTypeId: PaymentSourceTypeId,
                                        corpId: ObjServices.Corp_Id
                                   );
            }


            if (!ObjServices.IsDataReviewMode)
            {
                if (txtOriginationDate != null)
                    txtOriginationDate.Text = DateTime.Now.ToString("MM/dd/yyyy",CultureInfo.InvariantCulture);
            }

            /*esto quiere decir el pago esta en modo de editar*/
            if (Payment != null)
            {
                ObjServices.PaymentDetId = Payment.PaymentDetId;

                ddlCardType.SelectIndexByValue(Payment.AccountTypeId.ToString());

                if (Payment.OriginCreditAmount.HasValue)
                    txtAmount.Text = Payment.OriginCreditAmount.Value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);

                txtOriginationDate.Text = Payment.PaidDate.HasValue ? Payment.PaidDate.Value.ToString() : txtOriginationDate.Text;

                ddlAccountHolderRelationshipOwnerInsured.SelectIndexByValue(Payment.RelationshipToOwnerOrInsured.ToString());


                if (string.IsNullOrEmpty(Payment.EFTAccountHolder) == false)
                    txtAccountHolderName.Text = Payment.EFTAccountHolder;


                if (string.IsNullOrEmpty(Payment.EFTAccountHolderSource) == false)
                    txtBankName.Text = Payment.EFTAccountHolderSource;


                if (string.IsNullOrEmpty(Payment.EFTAccountNumber) == false)
                {
                    txtAccountNumber.Text = Payment.EFTAccountNumber;
                    txtRepeatAccountNumber.Text = Payment.EFTAccountNumber;
                }

                if (string.IsNullOrEmpty(Payment.EFTABANumber) == false)
                    if (txtABANumber != null)
                        txtABANumber.Text = Payment.EFTABANumber;

                readOnly(!(Payment.PaymentStatusId == 1));
            }
            else
            {
                var paymentHeader = ObjServices.oPaymentManager.GetPayment
                   (
                         ObjServices.Corp_Id
                       , ObjServices.Region_Id
                       , ObjServices.Country_Id
                       , ObjServices.Domesticreg_Id
                       , ObjServices.State_Prov_Id
                       , ObjServices.City_Id
                       , ObjServices.Office_Id
                       , ObjServices.Case_Seq_No
                       , ObjServices.Hist_Seq_No
                       , ObjServices.PaymentId.HasValue ? ObjServices.PaymentId.Value : -1
                   );

                if (paymentHeader != null)
                {
                    if (paymentHeader.PaymentStatusId == 1)
                        readOnly(false);
                    else
                        readOnly(true);
                }
                else
                    readOnly(true);
            }
        }

        public void readOnly(bool Enabled = true)
        {
            setControls();
            UCACHDomicile.activeSaveButton(Enabled);
            UCACHOneTime.activeSaveButton(Enabled);
            UCACHStbDomicile.activeSaveButton(Enabled);
            UCACHStbOnetime.activeSaveButton(Enabled);

            txtOriginationDate.Enabled = Enabled;
            txtAccountHolderName.Enabled = Enabled;

            txtAccountNumber.Enabled = Enabled;
            txtRepeatAccountNumber.Enabled = Enabled;

            if (txtABANumber != null)
                txtABANumber.Enabled = Enabled;

            if (txtAmount != null)
                txtAmount.Enabled = Enabled;

            if (ddlFormofPayment != null)
                ddlFormofPayment.Enabled = Enabled;


            if (ddlCardType != null)
                ddlCardType.Enabled = Enabled;


            if (ddlAccountHolderRelationshipOwnerInsured != null)
                ddlAccountHolderRelationshipOwnerInsured.Enabled = Enabled;

        }

        public void ClearData()
        {
            setControls();
            txtOriginationDate.Text = string.Empty;
            txtAccountHolderName.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtAccountNumber.Text = string.Empty;
            txtRepeatAccountNumber.Text = string.Empty;

            if (txtABANumber != null)
                txtABANumber.Text = string.Empty;

            if (txtAmount != null)
                txtAmount.Text = string.Empty;

            if (ddlFormofPayment != null)
                ddlFormofPayment.SelectIndexByValue("-1");


            if (ddlCardType != null)
                ddlCardType.SelectIndexByValue("-1");


            if (ddlAccountHolderRelationshipOwnerInsured != null)
                ddlAccountHolderRelationshipOwnerInsured.SelectIndexByValue("-1");
        }

        #endregion

        public void ReadOnlyControls(bool isReadOnly)
        {
            Utility.ReadOnlyControls(this.Controls, isReadOnly);
        }
        #region Private
        private bool SetContactInfo(Entity.UnderWriting.Entities.Contact contact)
        {
            var result = false;
            var relationShipToOwner = !string.IsNullOrEmpty(ddlAccountHolderRelationshipOwnerInsured.SelectedValue) ? ddlAccountHolderRelationshipOwnerInsured.SelectedValue.ToInt() : 0;
            contact.RelationshiptoOwner = relationShipToOwner;
            ObjServices.oContactManager.UpdateContact(contact);
            return result;
        }
        #endregion        
    }
}