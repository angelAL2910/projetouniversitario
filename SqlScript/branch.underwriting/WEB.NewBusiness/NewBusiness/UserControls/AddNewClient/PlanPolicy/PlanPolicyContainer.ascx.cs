﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;
using RESOURCE.UnderWriting.NewBussiness;

namespace WEB.NewBusiness.NewBusiness.UserControls.PlanPolicy
{
    public partial class PlanPolicyContainer : UC, IUC
    {
  
        protected void Page_Load(object sender, EventArgs e)
        {
            WUCFieldFooter._btnSaveFooter.Click += btnSaveFooter_Click;
        }

        public void edit() { }
        public void FillData() { }
        public void Translator(string Lang) { }

        protected void btnSaveFooter_Click(object sender, EventArgs e)
        {
            save();
            if (sender == WUCFieldFooter._btnSaveFooter)
                this.MessageBox(Resources.DataInsertedSucessfully,
                    Title: Resources.InformationLabel.ToUpper());
        }

        private bool validaPayment()
        {
            var result = true;
            //Validar si ya se registro un pago en el tab de payments  
            var Pagado = ObjServices.oPaymentManager.GetAllApplyPaymentDetail
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
               ).Sum(a => a.UsdCreditAmount - a.UsdDebitAmount);

            if (Pagado.HasValue && Pagado.Value > 0)
            {
                result = false;
                this.MessageBox(Resources.ValidacionPlanPolicyPaymentMsj, Title: Resources.Warning);
            }

            return result;
        }

        public void save()
        {
            if (validaPayment())
            {
                if (WUCPlanInformation.saveAll())
                {
                    if (!(ObjServices.ProductLine == Utility.ProductLine.HealthInsurance))
                        WUCDesignatedPensionerInformation.save();

                    WUCFieldFooter.save();
                    //El Tab esta completo
                    ObjServices.saveSetValidTab(Utility.Tab.PlanPolicy);
                    /* Si el plan seleccionado es de salud entonces completar el tab 
                       de Beneficiarios, deshabilitarlo y pasar directamente al tab de Health declaration
                    */
                    //Bmarroquin 07-04-2017 estaba dejando valido el tab beneficiarios cuando no procedia 
                    //ObjServices.saveSetValidTab(Utility.Tab.Beneficiaries);
                }
            }
        }

        public bool saveValidator()
        {
            var result = true;

            if (validaPayment())
            {
                result = WUCPlanInformation.saveAll();

                if (result)
                {
                    if (!(ObjServices.ProductLine == Utility.ProductLine.HealthInsurance))
                        WUCDesignatedPensionerInformation.save();

                    WUCFieldFooter.save();
                    //El Tab esta completo
                    ObjServices.saveSetValidTab(Utility.Tab.PlanPolicy);
                    //si el plan seleccionado es de salud entonces completar el tab de Beneficiarios, deshabilitarlo y pasar directamente al tab de Health declaration
                    //Bmarroquin 29-04-2017 estaba dejando valido el tab beneficiarios cuando no procedia 
                    //ObjServices.saveSetValidTab(Utility.Tab.Beneficiaries);
                }
            }

            return result;
        }

        public void EnabledControls(bool x)
        {
            WUCPlanInformation.EnableControls(x);
            WUCDesignatedPensionerInformation.EnableControls(x);
        }

        public void Initialize()
        {
            WUCFieldFooter.SetMultiploAnualAndItbis();
            WUCDesignatedPensionerInformation.Initialize();
            WUCPlanInformation.Initialize();
            WUCFieldFooter.Initialize();
        }

        public void ClearData()
        {
            WUCDesignatedPensionerInformation.ClearData(currentTab);
            WUCPlanInformation.ClearData();
            WUCFieldFooter.ClearData();
            ClearControls();
        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            WUCPlanInformation.ReadOnlyControls(isReadOnly);
            WUCDesignatedPensionerInformation.ReadOnlyControls(isReadOnly);
        }
    }
}