﻿using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.AddNewClient.Payment
{
    public partial class UCPagoNPE : UC,IUC
    {

        public DropDownList _ddlFormofPayment { get { return ddlFormofPayment; } }

        public DropDownList _ddlCardType { get { return ddlCardType; } }

        public DropDownList _ddlAccountHolderRelationshipOwnerInsured { get { return ddlAccountHolderRelationshipOwnerInsured; } }

        public Button _btnSave { get { return btnSave; } }

        public Button _btnCancel { get { return btnCancel; } }

        public DropDownList _ddlNombresBancos { get { return ddlNombresBancos; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void ddlFormofPayment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlAccountHolderRelationshipOwnerInsured_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        public void activeSaveButton(bool IsActive = true)
        {
            if (IsActive)
                MultiView1.SetActiveView(vActive);
            else
                MultiView1.SetActiveView(vInactive);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Translator(string.Empty);
        }

        public void Translator(string Lang)
        {
            OriginationDate.InnerHtml = Resources.OriginationDate;
            FormofPayment.InnerHtml = Resources.FormOfPayment;
            PaymentType.InnerHtml = Resources.PaymentType;
            AccountHolderName.InnerHtml = Resources.AccountHolderName;
            BankName.InnerHtml = Resources.BankName;
            AccountNumber.InnerHtml = Resources.AccountNumber;
            RepeatAccountNumber.InnerHtml = Resources.RepeatAccountNumber;
            ABANumber.InnerHtml = Resources.ABANumber;
            AccountHolderRelationshipOwnerInsured.InnerHtml = Resources.AccountHolderRelationshipOwnerInsured;
            Amount.InnerHtml = Resources.AmountLabel;
            btnSave.Text = Resources.Save;
            btnProcessPayment2.Text = Resources.Save;
            btnCancel.Text = Resources.Cancel.Capitalize();
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
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void ClearData()
        {
            throw new NotImplementedException();
        }
    }
}