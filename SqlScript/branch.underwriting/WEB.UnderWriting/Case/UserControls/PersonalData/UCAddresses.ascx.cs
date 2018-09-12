﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WEB.UnderWriting.Common;

namespace WEB.UnderWriting.Case.UserControls.PersonalData
{
	public partial class UCAddresses : UC, IUC
	{
		//DropDownManager DropDowns = new DropDownManager();
		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
			{

			}
		}

		void IUC.Translator(string Lang)
		{
			throw new NotImplementedException();
		}

		public void save()
		{

		}

		void IUC.readOnly(bool x)
		{
			throw new NotImplementedException();
		}

		void IUC.edit()
		{
			throw new NotImplementedException();
		}

		private void FillAddress(IEnumerable<Entity.UnderWriting.Entities.Contact.Address> addressList = null)
		{
			var addressData = addressList ?? Services.ContactManager.GetCommunicatonAdress(Service.Corp_Id, Service.Contact_Id, Service.LanguageId);
			var addressCount = addressData.Count();
			gvAddress.DataSource = addressData;
			gvAddress.DataBind();

			setPagerIndex(gvAddress, addressCount);
		}

		public void FillData()
		{
		}

		public void FillData(IEnumerable<Entity.UnderWriting.Entities.Contact.Address> addressList = null)
		{
			FillAddress(addressList);

			Service.DropDowns.GetDropDown(ref CountryDDL, 
										  Language.English, 
										  DropDownType.Country, 
										  Service.Corp_Id, 
										  Service.Region_Id, 
										  Service.Country_Id,
										  Service.Domesticreg_Id, 
										  Service.State_Prov_Id, 
										  Service.City_Id, 
										  Service.Office_Id,
										  Service.Case_Seq_No, 
										  Service.Hist_Seq_No, 
										  Service.Contact_Id, 
										  projectId: Service.ProjectId, 
										  companyId: Service.CompanyId);

			UnderWriting.Common.Tools.SelectIndexByValue(ref CountryDDL, "-1", true);

			Service.DropDowns.GetDropDown(ref AddresTypeDDL, 
										  Language.English, 
										  DropDownType.AddressType, 
										  Service.Corp_Id, 
										  Service.Region_Id, 
										  Service.Country_Id,
										  Service.Domesticreg_Id, 
										  Service.State_Prov_Id, 
										  Service.City_Id, 
										  Service.Office_Id,
										  Service.Case_Seq_No, 
										  Service.Hist_Seq_No, 
										  Service.Contact_Id, 
										  projectId: Service.ProjectId, 
										  companyId: Service.CompanyId);
		}

		protected void AddAddresBTN_Click(Object sender, EventArgs e)
		{
			var aCity = CityDDL.SelectedValue.Split('|');

            //Bmarroquin 16-02-2017 Se incorpora nueva funcionaliad a raiz de disparador INA hacia OIPA 
            
            var lObjAddress = new Entity.UnderWriting.Entities.Contact.Address
            {
                CorpId = Service.Corp_Id,
                DirectoryId = -1,
                DirectoryDetailId = -1,
                DirectoryTypeId = Convert.ToInt32(AddresTypeDDL.SelectedValue.Split('|')[1]),
                StreetAddress = StreetAdressTxt.Text,
                CreateUser = 1,
                ModifyUser = 1,

                RegionId = Convert.ToInt32(aCity[0]),
                CountryId = Convert.ToInt32(aCity[1]),
                DomesticregId = Convert.ToInt32(aCity[2]),
                StateProvId = Convert.ToInt32(aCity[3]),
                CityId = Convert.ToInt32(aCity[4]),

                ZipCode = String.IsNullOrEmpty(PostalCodeTxt.Text) ? null : PostalCodeTxt.Text,
                IsPrimary = AddressPrimaryChk.Checked,
                CommunicationType = CommType.Address.ToString(),
                ContactId = Service.Contact_Id
            };

            /*
            if (lObjAddress.DirectoryTypeId == 4 || lObjAddress.DirectoryTypeId == 9) //Solo se dispara cuando el tipo de direccion es HOME o BUSINESS para las demas no hay homologacion en OIPA
            {
                var lObjCOntact = Services.ContactManager.GetContact(Service.Corp_Id, Service.Contact_Id, Service.LanguageId);
                var lListDoc = Services.ContactManager.GetAllIdDocumentInformation(Service.Contact_Id, Service.LanguageId);

                IEnumerable<Entity.UnderWriting.Entities.Contact.IdDocument> listDocs;
                //Buscar el NIT para Mandarle al INA 
                listDocs = lListDoc.Where(x => x.ContactIdType == 7);

                if (listDocs.Count() > 0)
                {
                    lObjCOntact.Id = listDocs.FirstOrDefault().DocumentId.ToString();
                    string resp = string.Empty;
                    try
                    {
                        var lObjProxyGI = new GlobalIntegracionProxy.GlobalIntegracionProxy();
                        resp = lObjProxyGI.setNewAddressOIPA(lObjCOntact, lObjAddress);
                        if (resp != "ok")
                        {
                            var msjTest = " CustomDialogMessageEx('Ocurrio un error al tratar de ingresar la nueva direccion en OIPA', 500, 150, true,'Advertencia');";
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock((sender as System.Web.UI.Control), this.GetType(), "alert", msjTest, true);
                            return;
                        }
                    }
                    catch(Exception ex)
                    {
                        var msjTest = " CustomDialogMessageEx('Ocurrio un error inesperado al tratar de ingresar la nueva direccion en OIPA', 500, 150, true,'Advertencia');";
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock((sender as System.Web.UI.Control), this.GetType(), "alert", msjTest, true);
                        return;
                    }
                }
                else
                {
                    var msjTest = " CustomDialogMessageEx('No se Encontro el NIT del Asegurado, No se puede continuar por favor verifique que exista el NIT del cliente', 500, 150, true,'Advertencia');";
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock((sender as System.Web.UI.Control), this.GetType(), "alert", msjTest, true);
                    return;
                }
            //}
            //Fin Incorporacion Bmarroquin 16-02-2017
            */

            //Bmarroquin 17-02-2017 cambio a raiz de mejora al codigo le envio el objecto lObjAddress
            Services.ContactManager.SetAddress(lObjAddress); 

            EmptyAddressControls();
			FillAddress();
		}

		protected void EditAddressBTN_Click(Object sender, EventArgs e)
		{
			string[] aCity;
			aCity = this.CityDDL.SelectedValue.Split('|');
			int RownIndex = Convert.ToInt32(this.hdfRowIndex.Value);
			Services.ContactManager.SetAddress(new Entity.UnderWriting.Entities.Contact.Address
			{
				CorpId = Convert.ToInt32(gvAddress.DataKeys[RownIndex]["CorpId"]),
				DirectoryId = Convert.ToInt32(gvAddress.DataKeys[RownIndex]["DirectoryId"]),
				DirectoryDetailId = Convert.ToInt32(gvAddress.DataKeys[RownIndex]["DirectoryDetailId"]),
				DirectoryTypeId = Convert.ToInt32(AddresTypeDDL.SelectedValue.Split('|')[1]),
				CommunicationTypeId = Convert.ToInt32(gvAddress.DataKeys[RownIndex]["CommunicationTypeId"]),
				StreetAddress = this.StreetAdressTxt.Text,
				CreateUser = 1,
				ModifyUser = 1,
				ContactId = Service.Contact_Id,
				RegionId = Convert.ToInt32(aCity[0]),
				CountryId = Convert.ToInt32(aCity[1]),
				DomesticregId = Convert.ToInt32(aCity[2]),
				StateProvId = Convert.ToInt32(aCity[3]),
				CityId = Convert.ToInt32(aCity[4]),

				ZipCode = String.IsNullOrEmpty(this.PostalCodeTxt.Text) ? null : this.PostalCodeTxt.Text,
				IsPrimary = this.AddressPrimaryChk.Checked
			});

			EmptyAddressControls();
			FillAddress();
		}

		protected void UpdateAddress(object sender, EventArgs e)
		{
			var rowIndex = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
			this.hdfRowIndex.Value = rowIndex.ToString();
			Tools.SelectIndexByValue(ref CountryDDL, gvAddress.DataKeys[rowIndex]["CountryId"].ToString(), false);


			Service.DropDowns.GetDropDown(ref StateDDL, Language.English, DropDownType.StateProvince, Service.Corp_Id, null, Convert.ToInt32(CountryDDL.SelectedValue), projectId: Service.ProjectId, companyId: Service.CompanyId);
			Tools.SelectIndexByValue(ref StateDDL, gvAddress.DataKeys[rowIndex]["CountryId"].ToString() + '|' + gvAddress.DataKeys[rowIndex]["DomesticregId"].ToString() + '|' + gvAddress.DataKeys[rowIndex]["StateProvId"].ToString(), false);


			Service.DropDowns.GetDropDown(ref CityDDL, Language.English, DropDownType.City,
				Service.Corp_Id,
				null,
				Convert.ToInt32(this.StateDDL.SelectedValue.Split('|')[0]),
				Convert.ToInt32(this.StateDDL.SelectedValue.Split('|')[1]), Convert.ToInt32(this.StateDDL.SelectedValue.Split('|')[2]), projectId: Service.ProjectId, companyId: Service.CompanyId);

			Tools.SelectIndexByValue(ref CityDDL, gvAddress.DataKeys[rowIndex]["RegionId"].ToString() + '|' +
				gvAddress.DataKeys[rowIndex]["CountryId"] + '|' +
				gvAddress.DataKeys[rowIndex]["DomesticregId"] + '|' +
				gvAddress.DataKeys[rowIndex]["StateProvId"] + '|' +
				gvAddress.DataKeys[rowIndex]["CityId"], false);

			Tools.SelectIndexByValue(ref AddresTypeDDL, gvAddress.DataKeys[rowIndex]["CorpId"].ToString() + '|' + gvAddress.DataKeys[rowIndex]["DirectoryTypeId"].ToString(), false);
			StreetAdressTxt.Text = gvAddress.DataKeys[rowIndex]["StreetAddress"].ToString();

			PostalCodeTxt.Text = gvAddress.DataKeys[rowIndex]["ZipCode"] == null ? "" : gvAddress.DataKeys[rowIndex]["ZipCode"].ToString();

			AddressPrimaryChk.Checked = !String.IsNullOrEmpty(gvAddress.DataKeys[rowIndex]["IsPrimary"].ToString()) && Boolean.Parse(gvAddress.DataKeys[rowIndex]["IsPrimary"].ToString());

			EditAddressBTN.Visible = true;
			AddAddresBTN.Visible = false;
		}

		protected void DeleteAddress(Object sender, EventArgs e)
		{
			var rowIndex = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
			Services.ContactManager.DeleteCommunicaton(Convert.ToInt32(gvAddress.DataKeys[rowIndex]["CorpId"]), Convert.ToInt32(gvAddress.DataKeys[rowIndex]["DirectoryId"]), Convert.ToInt32(gvAddress.DataKeys[rowIndex]["DirectoryDetailId"]), 1);
			EmptyAddressControls();
			FillAddress();
		}

		protected void CountryDDL_SelectedIndexChanged(Object sender, EventArgs e)
		{
			Service.DropDowns.GetDropDown(ref StateDDL, 
										  Language.English, 
										  DropDownType.StateProvince, 
										  Service.Corp_Id, 
										  null, 
										  Convert.ToInt32(CountryDDL.SelectedValue), 
										  projectId: Service.ProjectId, 
										  companyId: Service.CompanyId);
	
			UnderWriting.Common.Tools.SelectIndexByValue(ref StateDDL, "-1", true);
			
			this.CityDDL.Items.Clear();
		}

		protected void StateDDL_SelectedIndexChanged(Object sender, EventArgs e)
		{
			var cityId = 0;
			var officeId = 0;

			if (StateDDL.SelectedValue.Split('|').Count() > 1)
			{
				cityId = Convert.ToInt32(this.StateDDL.SelectedValue.Split('|')[1]);
				officeId = Convert.ToInt32(this.StateDDL.SelectedValue.Split('|')[2]);
			}

			Service.DropDowns.GetDropDown(ref CityDDL, 
										  Language.English, 
										  DropDownType.City, 
										  Service.Corp_Id, 
										  null, 
										  Convert.ToInt32(this.StateDDL.SelectedValue.Split('|')[0]), 
										  cityId, 
										  officeId, 
										  null, 
										  null, 
										  null, 
										  null, 
										  null, 
										  null);
			
			UnderWriting.Common.Tools.SelectIndexByValue(ref CityDDL, "-1", true);
		}

		public void EmptyAddressControls()
		{
			this.AddressPrimaryChk.Checked = false;
			this.StreetAdressTxt.Text = string.Empty;
			this.PostalCodeTxt.Text = string.Empty;

			//Fill GridData
			//var addressData = Services.ContactManager.GetCommunicatonAdress(Service.Corp_Id, Service.Contact_Id);
			//gvAddress.DataSource = addressData;
			//gvAddress.DataBind();

			this.AddAddresBTN.Visible = true;
			this.EditAddressBTN.Visible = false;
			UnderWriting.Common.Tools.SelectIndexByValue(ref CountryDDL, "-1", false);
			UnderWriting.Common.Tools.SelectIndexByValue(ref StateDDL, "-1", false);
			UnderWriting.Common.Tools.SelectIndexByValue(ref CityDDL, "-1", false);
			UnderWriting.Common.Tools.SelectIndexByValue(ref AddresTypeDDL, "-1", false);
		}

		public void clearData()
		{
			throw new NotImplementedException();
		}

		void setPagerIndex(GridView gv, int Count)
		{

			if (gv.BottomPagerRow != null)
			{
				var lnkPrev = (LinkButton)gv.BottomPagerRow.FindControl("prevButton");
				var lnkFirst = (LinkButton)gv.BottomPagerRow.FindControl("firstButton");
				var lnkNext = (LinkButton)gv.BottomPagerRow.FindControl("nextButton");
				var lnkLast = (LinkButton)gv.BottomPagerRow.FindControl("lastButton");
				var indexText = (Literal)gv.BottomPagerRow.FindControl("indexPage");
				var totalText = (Literal)gv.BottomPagerRow.FindControl("totalPage");


				var count = gv.PageCount;
				var index = gv.PageIndex + 1;

				indexText.Text = index.ToString();
				totalText.Text = count.ToString();

				if (index == 1)
				{
					DisableLinkButton(lnkPrev, "prev_dis");
					DisableLinkButton(lnkFirst, "rewd_dis");
				}
				else if (index == count)
				{
					DisableLinkButton(lnkNext, "next_dis");
					DisableLinkButton(lnkLast, "fwrd_dis");

				}

				var totalItems = (Literal)gv.BottomPagerRow.FindControl("totalItems");
				totalItems.Text = Count.ToString();
			}
		}

		public void DisableLinkButton(LinkButton linkButton, string disable_class)
		{

			linkButton.CssClass = disable_class;
			linkButton.Enabled = false;
		}

		protected void gvAddress_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvAddress.PageIndex = e.NewPageIndex;
			gvAddress.DataBind();
			FillAddress();
		}

	}
}