﻿using Entity.UnderWriting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.UnderWriting.Common;

namespace WEB.UnderWriting.Case.UserControls.Riders
{
	public partial class UCRiderInformation : WEB.UnderWriting.Common.UC, WEB.UnderWriting.Common.IUC
	{
		//IRider RiderManager
		//{
		//    get { return diManager.RiderManager; }
		//}

		//UnderWritingDIManager diManager = new UnderWritingDIManager();
		DropDownManager DropDowns = new DropDownManager();

		//2016-01-29 | Marcos J. Perez
		//Flag to delete a Rider
		bool delete = false;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				ddlStatus.DataSource = DropDowns.GetDropDown(DropDownType.RiderStatus,
															 Service.Corp_Id,
															 projectId: Service.ProjectId,
															 companyId: Service.CompanyId);
				ddlStatus.DataBind();
				ddlStatus.Items.Insert(0, new ListItem("Select", "0"));
				ddlStatus.SelectedIndex = 0;
			}
		}

		public void FillData()
		{
			//Anterior
			//var Corp_Id = Service.Corp_Id;
			//var Region_Id = Service.Region_Id;
			//var Country_Id = Service.Country_Id;
			//var Domesticreg_Id = Service.Domesticreg_Id;
			//var State_Prov_Id = Service.State_Prov_Id;
			//var City_Id = Service.City_Id;
			//var Office_Id = Service.Office_Id;
			//var Case_Seq_No = Service.Case_Seq_No;
			//var Hist_Seq_No = Service.Hist_Seq_No;

			//Nuevo Forma Cambio Rabel O
			//Entity.UnderWriting.Entities.Policy.Parameter PolicyParameter = new Entity.UnderWriting.Entities.Policy.Parameter();			
			//PolicyParameter.CorpId = Service.Corp_Id;
			//PolicyParameter.RegionId = Service.Region_Id;
			//PolicyParameter.CountryId = Service.Country_Id;
			//PolicyParameter.DomesticregId = Service.Domesticreg_Id;
			//PolicyParameter.StateProvId = Service.State_Prov_Id;
			//PolicyParameter.CityId = Service.City_Id;
			//PolicyParameter.OfficeId = Service.Office_Id;
			//PolicyParameter.CaseSeqNo = Service.Case_Seq_No;
			//PolicyParameter.HistSeqNo = Service.Hist_Seq_No;
			//PolicyParameter.LanguageId = Service.LanguageId;
			//var riders = Services.RiderManager.GetAllRider(PolicyParameter);


			IEnumerable<Rider> riders = Services.RiderManager.GetAllRider(new Policy.Parameter
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
				LanguageId = Service.LanguageId
			});

			gvRiders.DataSource = riders;
			gvRiders.DataBind();

			fillDdl(false, riders);

			btnCancel.Enabled = false;
			pnBtnCancel.Attributes.Add("class", "boton_wrapper btn-disabled fr");

			bool existRiderTypeItems = ddlRiderType.Items.Count >= 1;
			readOnlyMode(existRiderTypeItems ? false : true);
			ddlRiderType.Enabled = existRiderTypeItems ? true : false;
			btnAdd.Enabled = existRiderTypeItems ? true : false;
			pnBtnAdd.Attributes.Add("class", existRiderTypeItems ? "boton_wrapper gradient_AM_btn bdrAM fr mR" : "boton_wrapper btn-disabled fr mR");
			spnAdd.Attributes.Add("class", "add");
			if (!existRiderTypeItems) { addMode(); }

			ValidateRyderType();

			//if (ddlRiderType.Items.Count >= 1)
			//{
			//	readOnlyMode(false);
			//	ddlRiderType.Enabled = true;

			//	//btnCancel.Enabled = true;
			//	//pnBtnCancel.Attributes.Add("class", "boton_wrapper gradient_RJ bdrRJ fr");

			//	btnAdd.Enabled = true;
			//	pnBtnAdd.Attributes.Add("class", "boton_wrapper gradient_AM_btn bdrAM fr mR");
			//	spnAdd.Attributes.Add("class", "add");

			//	//2016-01-29 | Marcos J. Perez
			//	//btnDelete.Enabled = true;
			//	//pnBtnDelete.Attributes.Add("class", "boton_wrapper gradient_vd bdrVd fr mR");
			//}
			//else
			//{
			//	//btnCancel.Enabled = false;
			//	//pnBtnCancel.Attributes.Add("class", "boton_wrapper btn-disabled fr");

			//	btnAdd.Enabled = false;
			//	pnBtnAdd.Attributes.Add("class", "boton_wrapper btn-disabled fr mR");
			//	spnAdd.Attributes.Add("class", "add");

			//	//2016-01-29 | Marcos J. Perez
			//	//btnDelete.Enabled = false;
			//	//pnBtnDelete.Attributes.Add("class", "boton_wrapper btn-disabled fr mR");

			//	readOnlyMode(true);
			//	ddlRiderType.Enabled = false;
			//	addMode();
			//}

			//ValidateRyderType();

			//if (gvRiders.Rows.Count == 0)
			//{
			//	//pnBtnEdit.Attributes.Add("class", "boton_wrapper btn-disabled fr mR");
			//	//btnEdit.Enabled = false;

			//	//2016-01-29 | Marcos J. Perez
			//	//btnDelete.Enabled = false;
			//	//pnBtnDelete.Attributes.Add("class", "boton_wrapper btn-disabled fr mR");
			//}
			//else
			//{
			//	//pnBtnEdit.Attributes.Add("class", "boton_wrapper gradient_vd2 bdrVd2 fr mR");
			//	//btnEdit.Enabled = true;

			//	//2016-01-29 | Marcos J. Perez
			//	//btnDelete.Enabled = true;
			//	//pnBtnDelete.Attributes.Add("class", "boton_wrapper gradient_vd bdrVd fr mR");
			//}
		}

		public void fillDdl(bool isEdit, IEnumerable<Rider> riders)
		{
			//if (isEdit)
			//{
			//	ddlRiderType.DataSource = DropDowns.GetDropDown(DropDownType.RiderType,
			//													Service.Corp_Id,
			//													Service.Region_Id,
			//													Service.Country_Id,
			//													Service.Domesticreg_Id,
			//													Service.State_Prov_Id,
			//													Service.City_Id,
			//													Service.Office_Id,
			//													Service.Case_Seq_No,
			//													Service.Hist_Seq_No,
			//													projectId: Service.ProjectId,
			//													companyId: Service.CompanyId);
			//}
			//else if (riders != null && riders.Any())
			//{
			//	if (ddlRiderType.DataSource != null)
			//	{
			//		ddlRiderType.DataSource = DropDowns.GetDropDown(DropDownType.RiderType,
			//														Service.Corp_Id,
			//														Service.Region_Id,
			//														Service.Country_Id,
			//														Service.Domesticreg_Id,
			//														Service.State_Prov_Id,
			//														Service.City_Id,
			//														Service.Office_Id,
			//														Service.Case_Seq_No,
			//														Service.Hist_Seq_No,
			//														projectId: Service.ProjectId,
			//														companyId: Service.CompanyId).Where(x => riders.All(x2 => x2.RiderTypeId != int.Parse(x.Value)) && x.Value != "4");
			//	}
			//}
			//else
			//{
			//	ddlRiderType.DataSource = null;
			//}

			IEnumerable<ListItem> dataSource = null,
								  dropdowns = DropDowns.GetDropDown(DropDownType.RiderType,
																	Service.Corp_Id,
																	Service.Region_Id,
																	Service.Country_Id,
																	Service.Domesticreg_Id,
																	Service.State_Prov_Id,
																	Service.City_Id,
																	Service.Office_Id,
																	Service.Case_Seq_No,
																	Service.Hist_Seq_No,
																	projectId: Service.ProjectId,
																	companyId: Service.CompanyId);

            dataSource = dropdowns; /* isEdit ? (dropdowns != null && dropdowns.Any()) ? dropdowns.Where(x => !x.Value.Equals("4")) 
																		 : null 
							    : (dropdowns != null && dropdowns.Any()) ? (riders != null && riders.Any()) ? dropdowns.Where(x => riders.All(x2 => !x2.RiderTypeId.Equals(Convert.ToInt32(x.Value)) && !x.Value.Equals("4"))) 
																										    : dropdowns.Where(x => !x.Value.Equals("4"))
																		 : (riders != null && riders.Any()) ? riders.Where(x => !x.RiderTypeId.Equals(4)).Select(x => new ListItem { Text = x.RyderTypeDesc, Value = x.RiderTypeId.ToString() }) 
																											: null;*/
			ddlRiderType.Items.Clear();
			ddlRiderType.DataSource = dataSource;
			ddlRiderType.DataBind();

			//2016-01-29 | Marcos J. Perez
			if (!isEdit)
			{
				ddlRiderType.Items.Insert(0, new ListItem("Select", "0"));
				ddlRiderType.SelectedIndex = 0;
			}
		}

		void UnderWriting.Common.IUC.Translator(string Lang)
		{
			throw new NotImplementedException();
		}

		public void save()
		{

		}

		void UnderWriting.Common.IUC.readOnly(bool x)
		{
			throw new NotImplementedException();
		}

		void UnderWriting.Common.IUC.edit()
		{
			throw new NotImplementedException();
		}

		protected void lnkAddCommentButton_Click(object sender, EventArgs e)
		{
			hfRiderReason.Value = "true";
			var gridRow = (GridViewRow)((Button)sender).NamingContainer;
			var RiderId = int.Parse(gvRiders.DataKeys[gridRow.RowIndex]["RiderId"].ToString());
			var RiderTypeId = int.Parse(gvRiders.DataKeys[gridRow.RowIndex]["RiderTypeId"].ToString());

			int riderId = RiderId;
			int riderTypeId = RiderTypeId;

			UCRiderReason.FillData(riderId, riderTypeId);
		}

		public void clearData()
		{
			clearFields();
			readOnlyMode(true);
			editMode();
		}

		private void readOnlyMode(bool readOnly)
		{
			//ddlRiderType.Enabled = (btnEdit.Text == "Save") ? readOnly : !readOnly;
			ddlRiderType.Enabled = (btnAdd.Text == "Save") ? readOnly : !readOnly;
			ddlStatus.Enabled = !readOnly;
			txtBeneficiaryAmount.ReadOnly = readOnly;
			txtEffectiveDate.Enabled = !readOnly;
			txtNumberOfYear.ReadOnly = readOnly;
		}

		private void saveEditMode()
		{
			//btnEdit.Text = "Save";
			btnAdd.Text = "Save";
			//btnAdd.Enabled = false;
			//pnBtnAdd.Attributes.Add("class", "boton_wrapper btn-disabled fr mR");
			pnBtnAdd.Attributes.Add("class", "boton_wrapper gradient_vd2 bdrVd2 fr mR");
			spnAdd.Attributes.Add("class", "edit");

			foreach (GridViewRow row in gvRiders.Rows)
			{
				row.Cells[0].Attributes.Remove("OnClick");
				row.Cells[1].Attributes.Remove("OnClick");
				row.Cells[2].Attributes.Remove("OnClick");
				row.Cells[3].Attributes.Remove("OnClick");
				row.Cells[4].Attributes.Remove("OnClick");
				row.Cells[5].Attributes.Remove("OnClick");
				row.Cells[7].Attributes.Remove("OnClick");

				row.Cells[9].Attributes.Remove("OnClick");
				row.Cells[10].Attributes.Remove("OnClick");
			}

			gvRiders.Enabled = false;
		}

		private void addMode()
		{
			btnAdd.Text = "Add";
			//btnEdit.Enabled = true;
			if (!gvRiders.Enabled)
			{
				gvRiders.Enabled = true;
			}

			foreach (GridViewRow row in gvRiders.Rows)
			{
				row.Cells[0].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[1].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[2].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[3].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[4].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[5].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[7].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));

				row.Cells[9].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[10].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
			}
		}

		private void editMode()
		{
			//btnEdit.Text = "Edit";
			btnAdd.Text = "Add";
			btnAdd.Enabled = true;
			pnBtnAdd.Attributes.Add("class", "boton_wrapper gradient_AM_btn bdrAM fr mR");
			spnAdd.Attributes.Add("class", "add");
			gvRiders.Enabled = true;
			gvRiders.SelectedIndex = -1;
			foreach (GridViewRow row in gvRiders.Rows)
			{
				row.Cells[0].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[1].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[2].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[3].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[4].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[5].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[7].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));

				row.Cells[9].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
				row.Cells[10].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + row.RowIndex));
			}
		}

		private bool addOrEditRider(int riderId, bool isEdit = false)
		{
			DateTime policyDate, riderStartDate, policyEndDate;
			int insuredPeriod, numberOfYear;

			int index = Convert.ToInt32(ViewState["SelectedIndex"]);

			bool ret = true;

			string msg = string.Empty;

			//2016-01-29 | Marcos J. Perez
			//if not deleting, Add or Edit a Rider
			if (!delete)
			{
				insuredPeriod = Service.InsuredPeriod.HasValue ? Service.InsuredPeriod.Value : 0;
				numberOfYear = int.Parse(txtNumberOfYear.Text);

				if (numberOfYear > insuredPeriod)
				{
					msg = string.Format("You cannot add a rider for a duration longer than the policy, Policy Duration: {0} years, please try again.", insuredPeriod.ToString());
					ret = false;

					//this.MessageBox("You cannot add a rider for a duration longer than the policy, Policy Duration: " + insuredPeriod.ToString() + " years, please try again.", 500, 150, true, "Invalid Value");
					//return;
				}

				policyDate = Service.SubmitDate.HasValue ? Service.SubmitDate.Value : DateTime.Now;
				DateTime.TryParse(txtEffectiveDate.Text, out riderStartDate);

				policyEndDate = policyDate.AddYears(insuredPeriod - numberOfYear);

				if (riderStartDate > policyEndDate)
				{
					msg = string.Format("You cannot add a rider for a date greater than the policy, Policy Date: {0}, please try again.", policyDate.ToString());
					ret = false;

					//this.MessageBox("You cannot add a rider for a date greater than the policy, Policy Date: " + policyDate.ToString() + ", please try again.", 500, 150, true, "Invalid Value");
					//return;
				}

				if (ret.Equals(false))
				{
					btnAdd.Text = isEdit ? "Save" : "Add";
					pnBtnAdd.Attributes.Add("class", isEdit ? "boton_wrapper gradient_vd2 bdrVd2 fr mR" : "boton_wrapper gradient_AM_btn bdrAM fr mR");
					spnAdd.Attributes.Add("class", isEdit ? "edit" : "add");

					btnCancel.Enabled = isEdit ? true : false;
					pnBtnCancel.Attributes.Add("class", isEdit ? "boton_wrapper gradient_RJ bdrRJ fr" : "boton_wrapper btn-disabled fr");

					this.MessageBox(msg, 500, 150, true, "Invalid Value");

					return ret;
				}
			}

			//else return;

			//var submitDate = Service.SubmitDate.Value;
			//submitDate.AddYears(int.Parse(txtNumberOfYear.Text));
			//var efect = Convert.ToDateTime(txtEffectiveDate.Text);

			var rider = new Entity.UnderWriting.Entities.Rider
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
				RiderId = !delete ? riderId : int.Parse(Convert.ToString(gvRiders.DataKeys[index]["RiderId"])) //2016-01-29 | Marcos J. Perez
			};

			//2016-01-29 | Marcos J. Perez
			//If Edit or Delete get RiderTypeId from GridView
			rider.RiderTypeId = int.Parse((isEdit || delete) ? Convert.ToString(gvRiders.DataKeys[index]["RiderTypeId"]) : ddlRiderType.SelectedValue);

			rider.BeneficiaryAmount = decimal.Parse(txtBeneficiaryAmount.Text == "" ? "0" : txtBeneficiaryAmount.Text);

			if (string.IsNullOrWhiteSpace(txtEffectiveDate.Text))
			{
				rider.EffectiveDate = null;
				rider.ExpireDate = null;
			}
			else
			{
				var efect = Convert.ToDateTime(txtEffectiveDate.Text);
				rider.EffectiveDate = efect;
				rider.ExpireDate = efect.AddYears(int.Parse(txtNumberOfYear.Text));
			}

			rider.SerieId = 0;
			rider.RiderStatusId = int.Parse(ddlStatus.SelectedValue);
			rider.NumberOfYear = int.Parse(txtNumberOfYear.Text == "" ? "0" : txtNumberOfYear.Text);
			rider.ExtraPremiumCommentCompleted = txtExtraPremiumComment.Text;
			rider.UserId = Service.Underwriter_Id;

			//2016-01-29 | Marcos J. Perez
			//If not deleting, SetRider
			if (!delete)
			{
				Services.RiderManager.SetRider(rider);
			}
			else
			{
				Services.RiderManager.DeleteRider(rider.CorpId,
												  rider.RegionId,
												  rider.CountryId,
												  rider.DomesticregId,
												  rider.StateProvId,
												  rider.CityId,
												  rider.OfficeId,
												  rider.CaseSeqNo,
												  rider.HistSeqNo,
												  rider.RiderTypeId,
												  rider.RiderId);
			}

			clearFields();
			return ret;
		}

		private void clearFields()
		{
			txtBeneficiaryAmount.Text = "";
			txtNumberOfYear.Text = "";
			txtExtraPremiumComment.Text = "";
			txtEffectiveDate.Text = "";
			ddlStatus.SelectedIndex = 0;
		}

		protected void gvRiders_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes.Add("onmouseover", "this.originalstyle = this.style.backgroundColor; this.style.backgroundColor = '#8CC65E'; this.style.cursor = 'pointer'");
				e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor = this.originalstyle;");
				e.Row.Cells[0].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + e.Row.RowIndex));
				e.Row.Cells[1].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + e.Row.RowIndex));
				e.Row.Cells[2].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + e.Row.RowIndex));
				e.Row.Cells[3].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + e.Row.RowIndex));
				e.Row.Cells[4].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + e.Row.RowIndex));
				e.Row.Cells[5].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + e.Row.RowIndex));
				e.Row.Cells[7].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + e.Row.RowIndex));

				e.Row.Cells[9].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + e.Row.RowIndex));
				e.Row.Cells[10].Attributes.Add("OnClick", Page.ClientScript.GetPostBackClientHyperlink(gvRiders, "Select$" + e.Row.RowIndex));
			}
		}

		protected void gvRiders_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
				this.upRiderInformation2.Triggers.Add(new AsyncPostBackTrigger
				{
					ControlID = lnkEdit.UniqueID,
					EventName = "Click"
				});

				Button btnRemove = e.Row.FindControl("btnRemove") as Button;
				this.upRiderInformation2.Triggers.Add(new AsyncPostBackTrigger
				{
					ControlID = btnRemove.UniqueID,
					EventName = "Click"
				});
			}
		}

		protected void ddlRiderType_SelectedIndexChanged(object sender, EventArgs e)
		{
			ValidateRyderType();
		}

		private void ValidateRyderType()
		{
			var productType = Service.GetProductFamily().ToString();

			if (productType.ToLower() == "funeral" && ddlRiderType.SelectedValue == "6")
			{
				txtBeneficiaryAmount.Text = "200000.00";
				txtBeneficiaryAmount.ReadOnly = true;
			}
			else
			{
				if (ddlRiderType.Items.Count > 0)
				{
					txtBeneficiaryAmount.ReadOnly = false;
					//if (ddlRiderType.Enabled)
					//  txtBeneficiaryAmount.Text = "";
				}
			}
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			if (btnAdd.Text.ToLower().Equals("add"))
			{
				if (ddlRiderType.Items.Count >= 1 && ddlRiderType.Enabled)
				{
					//2016-01-29 | Marcos J. Perez
					//Flag to delete a Rider
					delete = false;

					if (addOrEditRider(-1))
					{
						FillData();
					}
				}
				else
				{
					string message = "Cannot add more Riders.";
					ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Warning');", true);
				}
			}
			else if (btnAdd.Text.ToLower().Equals("save"))
			{
				var statuses = ddlStatus.Items;
				var statusselect = ddlStatus.SelectedValue;

				//2016-01-29 | Marcos J. Perez
				//Flag to delete a Rider
				delete = false;

				int index = Convert.ToInt32(ViewState["SelectedIndex"]);
				if (addOrEditRider(int.Parse((gvRiders.DataKeys[index]["RiderId"]).ToString()), true))
				{
					editMode();
					FillData();
					btnCancel.Enabled = false;
					pnBtnCancel.Attributes.Add("class", "boton_wrapper btn-disabled fr");
				}
			}
		}

		//protected void btnEdit_Click(object sender, EventArgs e)
		//{
		//	int row = gvRiders.SelectedIndex;

		//	if (btnEdit.Text == "Edit" & row > -1)
		//	{
		//		btnCancel.Enabled = true;
		//		pnBtnCancel.Attributes.Add("class", "boton_wrapper gradient_RJ bdrRJ fr");

		//		//IEnumerable<Entity.UnderWriting.Entities.Rider> riders = null;
		//		//fillDdl(true, riders);

		//		fillDdl(true, null);

		//		ddlRiderType.SelectedValue = (gvRiders.DataKeys[row]["RiderTypeId"]).ToString();

		//		txtBeneficiaryAmount.Text = (gvRiders.DataKeys[row]["BeneficiaryAmount"] == null ? 0 : gvRiders.DataKeys[row]["BeneficiaryAmount"]).ToString();
		//		txtNumberOfYear.Text = (gvRiders.DataKeys[row]["NumberOfYear"] == null ? 0 : gvRiders.DataKeys[row]["NumberOfYear"]).ToString();

		//		if (gvRiders.DataKeys[row]["ExtraPremiumCommentCompleted"] == null)
		//		{
		//			txtExtraPremiumComment.Text = "";
		//		}
		//		else
		//		{
		//			txtExtraPremiumComment.Text = (gvRiders.DataKeys[row]["ExtraPremiumCommentCompleted"]).ToString();
		//		}

		//		ddlStatus.SelectedValue = (gvRiders.DataKeys[row]["RiderStatusId"]).ToString();

		//		if (gvRiders.DataKeys[row]["EffectiveDate"] == null)
		//		{
		//			txtEffectiveDate.Text = "";
		//		}
		//		else
		//		{
		//			txtEffectiveDate.Text = Convert.ToDateTime((gvRiders.DataKeys[row]["EffectiveDate"]).ToString()).ToString("MM/dd/yyyy");
		//		}

		//		saveEditMode();
		//		readOnlyMode(false);
		//		ValidateRyderType();
		//	}
		//	else if (btnEdit.Text == "Save")
		//	{
		//		var statuses = ddlStatus.Items;
		//		var statusselect = ddlStatus.SelectedValue;

		//		//2016-01-29 | Marcos J. Perez
		//		//Flag to delete a Rider
		//		delete = false;

		//		addOrEditRider(int.Parse((gvRiders.DataKeys[row]["RiderId"]).ToString()), true);
		//		editMode();
		//		FillData();
		//		btnCancel.Enabled = false;
		//		pnBtnCancel.Attributes.Add("class", "boton_wrapper btn-disabled fr");
		//	}
		//	else
		//	{
		//		string message = "You must select a Rider to edit.";
		//		ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Warning');", true);
		//	}
		//}

		//2016-01-29 | Marcos J. Perez
		//Delete a Rider
		//protected void btnDelete_Click(object sender, EventArgs e)
		//{
		//	int row = gvRiders.SelectedIndex;

		//	if (row > -1)
		//	{
		//		if (ddlRiderType.Items.Count >= 1 && ddlRiderType.Enabled)
		//		{
		//			delete = true;
		//			addOrEditRider(-1);
		//			FillData();
		//		}
		//		else
		//		{
		//			string message = "Cannot delete more Riders.";
		//			ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Warning');", true);
		//		}
		//	}
		//	else
		//	{
		//		string message = "You must select a Rider to delete.";
		//		ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Warning');", true);
		//	}
		//}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			editMode();
			clearFields();
			FillData();

			if (ddlRiderType.Items.Count < 1)
			{
				readOnlyMode(true);
			}
		}

		protected void lnkEdit_Click(object sender, EventArgs e)
		{
			int index = Convert.ToInt32((sender as LinkButton).CommandArgument);
			ViewState["SelectedIndex"] = index;

			btnCancel.Enabled = true;
			pnBtnCancel.Attributes.Add("class", "boton_wrapper gradient_RJ bdrRJ fr");

			fillDdl(true, null);

			ddlRiderType.SelectedValue = (gvRiders.DataKeys[index]["RiderTypeId"]).ToString();

			txtBeneficiaryAmount.Text = (gvRiders.DataKeys[index]["BeneficiaryAmount"] == null ? 0 : gvRiders.DataKeys[index]["BeneficiaryAmount"]).ToString();
			txtNumberOfYear.Text = (gvRiders.DataKeys[index]["NumberOfYear"] == null ? 0 : gvRiders.DataKeys[index]["NumberOfYear"]).ToString();

			txtExtraPremiumComment.Text = gvRiders.DataKeys[index]["ExtraPremiumCommentCompleted"] == null ? "" : gvRiders.DataKeys[index]["ExtraPremiumCommentCompleted"].ToString();

			ddlStatus.SelectedValue = (gvRiders.DataKeys[index]["RiderStatusId"]).ToString();

			txtEffectiveDate.Text = gvRiders.DataKeys[index]["EffectiveDate"] == null ? "" : Convert.ToDateTime((gvRiders.DataKeys[index]["EffectiveDate"]).ToString()).ToString("MM/dd/yyyy");

			saveEditMode();
			readOnlyMode(false);
			ValidateRyderType();
		}

		protected void btnRemove_Click(object sender, EventArgs e)
		{
			ViewState["SelectedIndex"] = Convert.ToInt32((sender as Button).CommandArgument);
			delete = true;
			if (addOrEditRider(-1))
			{
				FillData();
			}
		}
	}
}