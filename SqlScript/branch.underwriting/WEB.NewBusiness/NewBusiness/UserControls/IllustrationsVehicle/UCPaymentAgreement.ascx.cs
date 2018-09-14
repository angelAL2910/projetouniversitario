﻿using Entity.UnderWriting.Entities;
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
    public partial class UCPaymentAgreement : UC, IUC
    {
        private const string NumberFormat = "#0.00";

        private decimal TotalPremium
        {
            get { return ViewState["TotalPremium"].ToDecimal(); }
            set { ViewState["TotalPremium"] = value; }
        }

        private decimal DiscountPercentage
        {
            get { return ViewState["DiscountPercentage"].ToDecimal(); }
            set { ViewState["DiscountPercentage"] = value; }
        }

        private decimal DiscountAmount
        {
            get { return ViewState["DiscountAmount"].ToDecimal(); }
            set { ViewState["DiscountAmount"] = value; }
        }

        private decimal MonthlyPayment
        {
            get { return ViewState["MonthlyPayment"].ToDecimal(); }
            set { ViewState["MonthlyPayment"] = value; }
        }

        private decimal MinimumAmount
        {
            get { return ViewState["MinimumAmount"].ToDecimal(); }
            set { ViewState["MinimumAmount"] = value; }
        }

        private decimal AnnualPremium
        {
            get { return ViewState["AnnualPremium"].ToDecimal(); }
            set { ViewState["AnnualPremium"] = value; }
        }

        private decimal TaxPercentage
        {
            get { return ViewState["TaxPercentage"].ToDecimal(); }
            set { ViewState["TaxPercentage"] = value; }
        }

        private int PaymentsAgreementQty
        {
            get { return ViewState["PaymentsAgreementQty"].ToInt(); }
            set { ViewState["PaymentsAgreementQty"] = value; }
        }

        private int? PaymentAgreementId
        {
            get { return (int?)ViewState["PaymentAgreementId"]; }
            set { ViewState["PaymentAgreementId"] = value; }
        }

        private bool HasFinancedFreq
        {
            get { return (bool)ViewState["HasFinancedFreq"]; }
            set { ViewState["HasFinancedFreq"] = value; }
        }

        private bool IsFinanced
        {
            get { return ViewState["IsFinanced"].ToBoolean(); }
            set { ViewState["IsFinanced"] = value; }
        }

        public void edit() { }
        protected void Page_Load(object sender, EventArgs e) { }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var illustrationData = ObjServices.getillustrationData();

            var MinimunAmount = txtMinimunAmount.ToDecimal();
            var AgreementAmount = txtAgreementAmount.ToDecimal();

            if (AgreementAmount < MinimunAmount && !HasFinancedFreq)
            {
                this.MessageBox(RESOURCE.UnderWriting.NewBussiness.Resources.ValidationMinimumAmount);
                return;
            }

            save();
        }

        public void Translator(string Lang) { }

        public void ReadOnlyControls(bool isReadOnly)
        {
            Utility.ReadOnlyControls(dvPaymentAgreementForm.Controls, isReadOnly);
        }

        public void save()
        {
            try
            {
                var item = new Entity.UnderWriting.Entities.Payment.Agreement
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
                    SurchargeNameKey = string.Empty,
                    DiscountNameKey = string.Empty,
                    PaymentAgreementId = this.PaymentAgreementId.HasValue ? this.PaymentAgreementId.GetValueOrDefault() : -1,
                    PaymentsAgreementQty = this.PaymentsAgreementQty,
                    DiscountPercentage = this.DiscountPercentage,
                    DiscountAmount = this.DiscountAmount,
                    InitialPayment = IsFinanced ? 0 : txtAgreementAmount.ToDecimal(),
                    UserId = ObjServices.UserID.GetValueOrDefault(),
                    TotalAgreementPayment = IsFinanced ? ObjServices.annualPremium.GetValueOrDefault() : txtTotalPremium.ToDecimal()
                };

                ObjServices.oPaymentManager.SetPaymentAgreement(item);

                //Guardar en la cabecera de la poliza los campos de financiamiento
                var dataPolicy = ObjServices.oPolicyManager.GetPolicy(
                    ObjServices.Corp_Id,
                    ObjServices.Region_Id,
                    ObjServices.Country_Id,
                    ObjServices.Domesticreg_Id,
                    ObjServices.State_Prov_Id,
                    ObjServices.City_Id,
                    ObjServices.Office_Id,
                    ObjServices.Case_Seq_No,
                    ObjServices.Hist_Seq_No);

                dataPolicy.Financed = IsFinanced;
                dataPolicy.MonthlyPayment = IsFinanced ? this.MonthlyPayment : DBNull.Value.ToDecimal();
                dataPolicy.Period = IsFinanced ? PaymentsAgreementQty : DBNull.Value.ToInt();
                ObjServices.oPolicyManager.UpdatePolicy(dataPolicy);
                //Llamar la flat table
                ObjServices.UpdateTempTable(ObjServices.Policy_Id, ObjServices.UserID.GetValueOrDefault());

                //Actualizar el resumen   
                var IllustrationInformationUC = Utility.GetAllChildren(this.Page).FirstOrDefault(uc => uc is UCIllustrationInformation);
                if (IllustrationInformationUC != null)
                    (IllustrationInformationUC as UCIllustrationInformation).FillData();

                //Actualizar inforamcion del contacto
                var InsuredInformationUC = Utility.GetAllChildren(this.Page).FirstOrDefault(uc => uc is UCInsuredInformation);
                if (InsuredInformationUC != null)
                    (InsuredInformationUC as UCInsuredInformation).Initialize();

                //Si se estan mostrando los requeridos en ese momento acualizar el listado
                var DocumentsUC = Utility.GetAllChildren(this.Page).FirstOrDefault(uc => uc is UCDocuments);
                if (DocumentsUC != null)
                    (DocumentsUC as UCDocuments).Initialize();

                FillDataEx();

                this.MessageBox(RESOURCE.UnderWriting.NewBussiness.Resources.SaveSucessfully);
            }
            catch (Exception ex)
            {
                var MessageEx = ex.Message.Replace('\'', '\"').MyRemoveInvalidCharacters();
                (this.Page as BasePage).ErrorDescription = ex.InnerException != null ? ex.InnerException.ToString() : string.Empty;
                var msg = string.Format("{0}  <br> <br> Presione Ok para descargar un archivo con el detalle del error", MessageEx);
                this.CustomDialogMessageWithCallBack(msg, "function(){$('#btnGenerateFileError').click();}", "Error", "", "");
            }
        }

        public void FillDataEx()
        {
            var dataResult = ObjServices.oPaymentManager.GetPaymentAgreement(new Entity.UnderWriting.Entities.Payment.Agreement
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

            var hasPaymentAgreement = (dataResult != null);

            if (hasPaymentAgreement)
            {
                this.PaymentAgreementId = dataResult.PaymentAgreementId;
                var qtyPay = dataResult.PaymentsAgreementQty;
                var TextFind = "";

                /*
                 Pago Único                 0
                 Inicial + 1 Cuota          1
                 Inicial + 2 Cuota          2
                 Inicial + 3 Cuota          3
                 Inicial + 4 Cuota          4
                */

                switch (qtyPay)
                {
                    case 0: TextFind = "Pago Único"; break;
                    case 1: TextFind = "Inicial + 1 Cuota"; break;
                    case 2: TextFind = "Inicial + 2 Cuotas"; break;
                    case 3: TextFind = "Inicial + 3 Cuotas"; break;
                    case 4: TextFind = "Inicial + 4 Cuotas"; break;
                    case 5: TextFind = "Financiado a 5 Cuotas"; break;
                    case 6: TextFind = "Financiado a 6 Cuotas"; break;
                    case 7: TextFind = "Financiado a 7 Cuotas"; break;
                    case 8: TextFind = "Financiado a 8 Cuotas"; break;
                    case 9: TextFind = "Financiado a 9 Cuotas"; break;
                    case 10: TextFind = "Financiado a 10 Cuotas"; break;
                    case 11: TextFind = "Financiado a 11 Cuotas"; break;
                    case 12: TextFind = "Financiado a 12 Cuotas"; break;
                }

                var element = ddlPaymentFreq.Items.FindByText(TextFind);
                ddlPaymentFreq.SelectedValue = element != null ? element.Value : "-1";
                ddlPaymentFreq_SelectedIndexChanged(ddlPaymentFreq, null);
                txtAgreementAmount.Text = dataResult.InitialPayment.ToString(NumberFormat, CultureInfo.InvariantCulture);
            }

            var isEfective = (ObjServices.StatusNameKey == "EFECT");
            lnkCancelPaymentAgreement.Visible = (hasPaymentAgreement && (dataResult.InitialPayment > 0 || this.MonthlyPayment > 0)) && !isEfective;
        }

        public void FillData()
        {
            var illustrationData = ObjServices.getillustrationData();

            //Objeto de la Data de la Poliza
            var PolicyData = ObjServices.oPolicyManager.GetPolicy(ObjServices.Corp_Id, ObjServices.Region_Id, ObjServices.Country_Id, ObjServices.Domesticreg_Id, ObjServices.State_Prov_Id, ObjServices.City_Id, ObjServices.Office_Id, ObjServices.Case_Seq_No, ObjServices.Hist_Seq_No);
            this.TaxPercentage = PolicyData.TaxPercentage.GetValueOrDefault();
            this.AnnualPremium = PolicyData.AnnualPremium.HasValue ? PolicyData.AnnualPremium.Value : 0;
            txtAnnualPremiun.Text = this.AnnualPremium.ToString(NumberFormat, CultureInfo.InvariantCulture);
            FillDataEx();
            pnDataPayment.Visible = !illustrationData.Financed.GetValueOrDefault();
        }

        private void FillDrop()
        {
            var dataQtyPayments = ObjServices.GettingDropData(Utility.DropDownType.QtyPayments,
                                                              corpId: ObjServices.Corp_Id,
                                                              regionId: ObjServices.Region_Id,
                                                              countryId: ObjServices.Country_Id,
                                                              domesticregId: ObjServices.Domesticreg_Id,
                                                              stateProvId: ObjServices.State_Prov_Id,
                                                              cityId: ObjServices.City_Id,
                                                              officeId: ObjServices.Office_Id,
                                                              caseSeqNo: ObjServices.Case_Seq_No,
                                                              histSeqNo: ObjServices.Hist_Seq_No).Select(v => new
                                                              {
                                                                  Text = v.ActionDesc,
                                                                  Value = string.Concat(v.ActionId, "|", v.DeductibleCategoryValue.ToString().Replace(",", "."), "|", v.StudentStatusDesc)
                                                              });

            ddlPaymentFreq.DataSource = dataQtyPayments;
            ddlPaymentFreq.DataTextField = "Text";
            ddlPaymentFreq.DataValueField = "Value";
            ddlPaymentFreq.DataBind();
            ddlPaymentFreq.Items.Insert(0, new ListItem { Text = "----", Value = "-1" });
        }

        public void Initialize()
        {
            ClearData();
            FillDrop();
            FillData();

            var isEffectivePolicy = ObjServices.StatusNameKey == Utility.IllustrationStatus.Effective.Code();

            var tabSeleccionado = (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), ObjServices.hdnQuotationTabs);
            var isReadOnly = false;

            switch (tabSeleccionado)
            {
                case Utility.Tabs.lnkIllustrationsToWork:
                case Utility.Tabs.lnkCompleteIllustrations:
                case Utility.Tabs.lnkExpiring:
                case Utility.Tabs.lnkMissingDocuments:
                    isReadOnly = isEffectivePolicy;
                    break;
                case Utility.Tabs.lnkSubscriptions:
                    isReadOnly = ObjServices.IsAgentQuotRole || isEffectivePolicy;
                    break;
                case Utility.Tabs.lnkHistoricalIllustrations:
                case Utility.Tabs.lnkApprovedBySubscription:
                    isReadOnly = true;
                    break;
            }

            btnGuardar.Visible = !isReadOnly;
            ReadOnlyControls(isReadOnly);
        }

        public void ClearData()
        {
            Utility.ClearAll(dvPaymentAgreementForm.Controls, txtAnnualPremiun);
        }

        protected void lnkCancelPaymentAgreement_Click(object sender, EventArgs e)
        {
            this.PaymentsAgreementQty = 0;
            this.DiscountPercentage = 0;
            this.DiscountAmount = 0;
            txtAgreementAmount.Text = "0";
            save();
            ClearData();
        }

        protected void ddlPaymentFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentFreq.SelectedValue != "-1")
            {
                var values = ddlPaymentFreq.SelectedValue.Split('|');
                int QtyPaid = values[0].ToInt();

                IsFinanced = false;

                switch (QtyPaid)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        pnDataPayment.Visible = true;
                        pnDataPayment2.Visible = !pnDataPayment.Visible;
                        decimal DescuentoPorc = values[1].ToDecimal();
                        decimal MontoMinimoPorc = values[2].ToDecimal();
                        decimal totalDesc = (this.AnnualPremium * DescuentoPorc);
                        decimal MontoDescuento = totalDesc;
                        txtDiscount.Text = MontoDescuento.ToString(NumberFormat, CultureInfo.InvariantCulture);
                        decimal totalPremiumWithDiscount = this.AnnualPremium - MontoDescuento;
                        txtPremiumWithDesc.Text = totalPremiumWithDiscount.ToString(NumberFormat, CultureInfo.InvariantCulture);
                        var TasaCalc = (this.TaxPercentage / 100);
                        var AmountTax = totalPremiumWithDiscount * TasaCalc;
                        txtTax.Text = AmountTax.ToString(NumberFormat, CultureInfo.InvariantCulture);
                        this.TotalPremium = totalPremiumWithDiscount + AmountTax;
                        txtTotalPremium.Text = this.TotalPremium.ToString(NumberFormat, CultureInfo.InvariantCulture);
                        var montoMin = (int)(MontoMinimoPorc * 100);
                        txtPorcMinimum.Text = string.Concat(montoMin, "%");
                        var PagoMinimo = this.TotalPremium * MontoMinimoPorc;
                        txtMinimunAmount.Text = PagoMinimo.ToString(NumberFormat, CultureInfo.InvariantCulture);

                        //Setear variables del viewstate                
                        this.PaymentsAgreementQty = QtyPaid;
                        this.DiscountPercentage = DescuentoPorc;
                        this.DiscountAmount = MontoDescuento;
                        this.MinimumAmount = PagoMinimo;

                        var AgreementAmountQuotas = QtyPaid > 0 ? (this.TotalPremium - PagoMinimo) / QtyPaid : (this.TotalPremium - PagoMinimo);
                        txtAgreementAmount.Text = txtMinimunAmount.Text;
                        txtAmomuntQuotas.Text = AgreementAmountQuotas.ToString("#,0.00", CultureInfo.InvariantCulture);

                        #region Codigo comentado
                        //if (this.PaymentsAgreementQty == 0)
                        // txtAgreementAmount.Text = txtMinimunAmount.Text;
                        //else
                        //    txtAgreementAmount.Clear("0.00");
                        #endregion

                        HasFinancedFreq = false;

                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        IsFinanced = true;
                        pnDataPayment.Visible = false;
                        pnDataPayment2.Visible = !pnDataPayment.Visible;

                        var loantype = WebServices.GlobalService.LoanType.VehicleInsurance;

                        switch (ObjServices.ProductLine)
                        {
                            case Utility.ProductLine.Auto:
                                loantype = WebServices.GlobalService.LoanType.VehicleInsurance;
                                break;
                            case Utility.ProductLine.AlliedLines:
                                loantype = WebServices.GlobalService.LoanType.HouseInsurance;
                                break;
                            default:
                                break;
                        }

                        //Obtener los datos de financiamiento
                        var annualPremium = (double)ObjServices.annualPremium.GetValueOrDefault();
                        var Principal = ObjServices.GetPorcKCO();
                        var Impuesto = (Principal / 100) * annualPremium;
                        var FinancedAmount = annualPremium;

                        //Campos a mostrar
                        var AnnPre = double.Parse(txtAnnualPremiun.Text.Replace(",", ""), CultureInfo.InvariantCulture);
                        var ImpuestoFinanced = ObjServices.TaxPercentage * AnnPre;
                        txtTaxFinanced.Text = ImpuestoFinanced.ToString(NumberFormat, CultureInfo.InvariantCulture);
                        txtTotalPrimeFinanced.Text = (AnnPre + ImpuestoFinanced).ToString(NumberFormat, CultureInfo.InvariantCulture);

                        var DataTA = ObjServices.GetAmortizationTable(FinancedAmount,
                                                                      loantype,
                                                                      (QtyPaid),
                                                                      Principal,
                                                                      (double)ObjServices.annualPremium
                                                                      );

                        this.MonthlyPayment = (decimal)DataTA.productCalculatorResult.Payment;
                        txtMonthlyPayment.Text = MonthlyPayment.ToString(NumberFormat, CultureInfo.InvariantCulture);
                        //Setear variables del viewstate                
                        this.PaymentsAgreementQty = QtyPaid;
                        this.DiscountPercentage = 0;
                        this.DiscountAmount = 0;
                        this.MinimumAmount = 0;
                        HasFinancedFreq = true;
                        break;
                }
            }
            else
                ClearData();
        }
    }
}