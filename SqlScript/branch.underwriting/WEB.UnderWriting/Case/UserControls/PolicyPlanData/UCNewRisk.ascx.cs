﻿using Entity.UnderWriting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.UnderWriting.Common;

namespace WEB.UnderWriting.Case.UserControls.PolicyPlanData
{
    public partial class UCNewRisk : WEB.UnderWriting.Common.UC, WEB.UnderWriting.Common.IUC
    {
        //UnderWritingDIManager diManager = new UnderWritingDIManager();
        DropDownManager DropDowns = new DropDownManager();
        //IPolicy PolicyManager
        //{
        //    get { return diManager.PolicyManager; }
        //}
        bool? FillRiskDrop
        {
            get { return string.IsNullOrWhiteSpace(hdnPopNRiskFillDrop.Value) ? (bool?)null : bool.Parse(hdnPopNRiskFillDrop.Value); }
            set { hdnPopNRiskFillDrop.Value = (value == null ? "" : value.ToString()); }
        }
        bool IsAditionalInsured
        {
            get { return string.IsNullOrWhiteSpace(hdnNRIsAditional.Value) ? false : bool.Parse(hdnNRIsAditional.Value); }
            set { hdnNRIsAditional.Value = value.ToString(); }
        }
        bool IsEdit
        {
            get { return string.IsNullOrWhiteSpace(hdnNRIsEdit.Value) ? false : bool.Parse(hdnNRIsEdit.Value); }
            set { hdnNRIsEdit.Value = value.ToString(); }
        }


        int contactId
        {
            get { return string.IsNullOrWhiteSpace(hdnNRContactId.Value) ? 0 : int.Parse(hdnNRContactId.Value); }
            set { hdnNRContactId.Value = value.ToString(); }
        }
        int contactRoleTypeId
        {
            get { return string.IsNullOrWhiteSpace(hdnNRContactRoleTypeId.Value) ? 0 : int.Parse(hdnNRContactRoleTypeId.Value); }
            set { hdnNRContactRoleTypeId.Value = value.ToString(); }
        }
        bool ContactSex
        {
            get { return string.IsNullOrWhiteSpace(hdnNRSex.Value) ? false : bool.Parse(hdnNRSex.Value); }
            set { hdnNRSex.Value = value.ToString(); }
        }
        int ContactAge
        {
            get { return string.IsNullOrWhiteSpace(hdnNRAge.Value) ? 0 : int.Parse(hdnNRAge.Value); }
            set { hdnNRAge.Value = value.ToString(); }
        }

        List<RiskGridItem> RiskGridList
        {
            get { return (List<RiskGridItem>)Session["RiskGridItemList"]; }
            set { Session["RiskGridItemList"] = value; }
        }

        private class RiskGridItem
        {
            public int? RiskId { get; set; }
            public String SequenceReference { get; set; }
            public int RiskGroupId { get; set; }
            public int RiskDetId { get; set; }
            public int RiskTypeId { get; set; }
            public int PageId { get; set; }
            public int GridId { get; set; }
            public int ElementId { get; set; }
            public int ColumnId { get; set; }

            public string RiskTypeDesc { get; set; }
            public string CategoryDesc { get; set; }
            public string ConditionTypeDesc { get; set; }
            public string ReasonDesc { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Translator(string Lang)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public void clearData()
        {
            var newListItem = new List<ListItem>();

            txtPopNRiskDuration.Text =
                txtPopNRiskReconsider.Text =
                txtPopNRiskSuggestedRaiting.Text = String.Empty;

            ddlPopNRiskReason.SelectedIndex = -1;
            ddlPopNRiskReason.DataSource = newListItem;
            ddlPopNRiskReason.DataBind();

            ddlPopNRiskConditionType.DataSource = newListItem;
            ddlPopNRiskConditionType.DataBind();

            rBtnPopNRiskPerThousand.Checked = false;
            rBtnPopNRiskTableRating.Checked = false;

            rBtnPopNRiskPerThousand.Enabled = false;
            rBtnPopNRiskTableRating.Enabled = false;

            ddlPopNRiskTableRating.DataSource = newListItem;
            ddlPopNRiskTableRating.DataBind();

            ddlPopNRiskPerThousand.DataSource = newListItem;
            ddlPopNRiskPerThousand.DataBind();
            //Bmarroquin 06-03-Nuevos campos por limpiar 
            txtPopNRiskConditionType.Text = string.Empty;
            txtPopNRiskReason.Text = string.Empty;
            txtPopNRiskTableRating.Text = string.Empty;
            txtPopNRiskPerThousand.Text = string.Empty;

            ddlTableRatingRisk.SelectedIndex = -1;
            ddlTableRatingRisk.DataSource = newListItem;
            ddlTableRatingRisk.DataBind();
        }

        public void readOnly(bool x)
        {
            throw new NotImplementedException();
        }

        public void edit()
        {
            throw new NotImplementedException();
        }

        public void FillRiskData(Boolean IsAditional, int ContactId, int ContactRoleTypeId)
        {
            IsEdit = false;
            IsAditionalInsured = IsAditional;
            contactId = ContactId;
            contactRoleTypeId = ContactRoleTypeId;

            pnlRiderType.Visible = !IsAditional;

            //Clear Fields
            clearData();
            ClearRatingInfo();

            //Initialize Variables
            RiskGridList = new List<RiskGridItem>();
            FillRiskDrop = null;

            //Fill Data
            FillDrops();
            FillGrid();

            //Enable Controls
            EnableControls(true);

            //Fill Contact Info
            ContactAge = Service.ContactAge;
            ContactSex = !String.IsNullOrEmpty(Service.ContactGender) && Service.ContactGender.ToLower() == "f";
        }

        private void FillGrid()
        {
            gvPopNRisks.DataSource = RiskGridList;
            gvPopNRisks.DataBind();
        }

        private void FillDrops()
        {
            //Category DropDown
            DropDowns.GetDropDown(
                ref ddlPopNRiskCategory,
                Language.English, 
                DropDownType.RiskCategory, 
                Service.Corp_Id, 
                projectId: Service.ProjectId, 
                companyId: Service.CompanyId);

            //RiskType DropDown
            DropDowns.GetDropDown(
                ref ddlPopNRiskType, 
                Language.English, 
                DropDownType.RiskType, 
                Service.Corp_Id, 
                projectId: Service.ProjectId, 
                companyId: Service.CompanyId);

            //RiderType DropDown
            DropDowns.GetDropDown(
                ref ddlPopNRiderType, 
                Language.English, 
                DropDownType.RiskRiderType, 
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

            //Bmarroquin 07-03-17 cambio dado que ahora el text box debe ser un DropDown List
            //Table Rating
            DropDowns.GetDropDown(
                ref ddlTableRatingRisk, 
                Language.English, 
                DropDownType.TableRatingRisk, 
                Service.Corp_Id, 
                projectId: Service.ProjectId, 
                companyId: Service.CompanyId);
        }

        protected void ddlPopNRiskCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear Info
            if (gvPopNRisks.Rows.Count < 1)
            {
                ClearRatingInfo();
            }

            //ConditionType DropDown
            DropDowns.GetDropDown(
                ref ddlPopNRiskConditionType, 
                Language.English, 
                DropDownType.RiskConditionType, 
                Service.Corp_Id, 
                riskGroupId: int.Parse(ddlPopNRiskCategory.SelectedValue), 
                projectId: Service.ProjectId, 
                companyId: Service.CompanyId);

            //Reason DropDown
            ddlPopNRiskReason.DataSource = new List<ListItem>();
            ddlPopNRiskReason.DataBind();
        }

        protected void ddlPopNRiskConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear Info
            if (gvPopNRisks.Rows.Count < 1)
            {
                ClearRatingInfo();
            }

            if (ddlPopNRiskType.SelectedIndex < 1 || ddlPopNRiskCategory.SelectedIndex < 1 || ddlPopNRiskConditionType.SelectedIndex < 1) return;

            //Reason DropDown
            DropDowns.GetDropDown(
                ref ddlPopNRiskReason, 
                Language.English, 
                DropDownType.RiskReason, 
                Service.Corp_Id, 
                riskGroupId: int.Parse(ddlPopNRiskCategory.SelectedValue), 
                riskDetId: int.Parse(ddlPopNRiskConditionType.SelectedValue), 
                riskTypeId: int.Parse(ddlPopNRiskType.SelectedValue), 
                contactAge: ContactAge, 
                contactSex: ContactSex, 
                projectId: Service.ProjectId, 
                companyId: Service.CompanyId);
        }

        protected void ddlPopNRiskType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear Info
            if (gvPopNRisks.Rows.Count < 1)
            {
                ClearRatingInfo();
            }

            if (ddlPopNRiskType.SelectedIndex < 1 || ddlPopNRiskCategory.SelectedIndex < 1 || ddlPopNRiskConditionType.SelectedIndex < 1) return;

            //Reason DropDown
            DropDowns.GetDropDown(ref ddlPopNRiskReason, Language.English, DropDownType.RiskReason, Service.Corp_Id, riskGroupId: int.Parse(ddlPopNRiskCategory.SelectedIndex < 1 ? "-1" : ddlPopNRiskCategory.SelectedValue), riskDetId: int.Parse(ddlPopNRiskConditionType.SelectedIndex < 1 ? "-1" : ddlPopNRiskConditionType.SelectedValue), riskTypeId: int.Parse(ddlPopNRiskType.SelectedIndex < 1 ? "-1" : ddlPopNRiskType.SelectedValue), contactAge: ContactAge, contactSex: ContactSex, projectId: Service.ProjectId, companyId: Service.CompanyId);
        }

        protected void ddlPopNRiskReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPopNRiskReason.SelectedIndex > 0)
            {
                pnlPopNRiskAddReason.Visible = true;

                if (gvPopNRisks.Rows.Count < 1)
                    FillReasonData();
            }
        }

        private void FillRatings(IEnumerable<Entity.UnderWriting.Entities.Policy.OverPricePercentage> dataList, bool isMultiple = false)
        {
            if (isMultiple)
            {
                if (FillRiskDrop.Value)
                {
                    ddlPopNRiskTableRating.DataSource = DropDowns.GetDropDown(DropDownType.RiskRating, Service.Corp_Id, tableTypeId: 1, projectId: Service.ProjectId, companyId: Service.CompanyId).ToList().OrderBy(r => decimal.Parse(r.Text));
                    ddlPopNRiskTableRating.DataBind();

                    ddlPopNRiskPerThousand.DataSource = DropDowns.GetDropDown(DropDownType.RiskRating, Service.Corp_Id, tableTypeId: 2, projectId: Service.ProjectId, companyId: Service.CompanyId).ToList().OrderBy(r => decimal.Parse(r.Text));
                    ddlPopNRiskPerThousand.DataBind();

                    rBtnPopNRiskTableRating.Enabled = true;
                    rBtnPopNRiskPerThousand.Enabled = true;

                    rBtnPopNRiskTableRating.Checked = true;
                    ddlPopNRiskTableRating.Attributes.Remove("disabled");

                    rBtnPopNRiskPerThousand.Checked = false;
                    ddlPopNRiskPerThousand.Attributes.Add("disabled", "disabled");
                }
            }
            else
            {
                if (!dataList.Any(r => !r.MaxValueIsNumeric || !r.MinValueIsNumeric))
                {
                    ClearRatingInfo();

                    var tableRating = dataList.Where(r => r.RatingTypeId == (int)Tools.RisksRatingTypes.TableRating);
                    var perThousand = dataList.Where(r => r.RatingTypeId == (int)Tools.RisksRatingTypes.PerThousand);

                    var tableRatingAny = tableRating.Any();
                    var perThousandAny = perThousand.Any();

                    //Ratings Validation
                    rBtnPopNRiskTableRating.Enabled = tableRatingAny;
                    rBtnPopNRiskTableRating.Checked = tableRatingAny;
                    if (tableRatingAny)
                        ddlPopNRiskTableRating.Attributes.Remove("disabled");
                    else
                        ddlPopNRiskTableRating.Attributes.Add("disabled", "disabled");

                    rBtnPopNRiskPerThousand.Checked = !tableRatingAny && perThousandAny;
                    rBtnPopNRiskPerThousand.Enabled = !tableRatingAny && perThousandAny;

                    if (!tableRatingAny && perThousandAny)
                        ddlPopNRiskPerThousand.Attributes.Remove("disabled");
                    else
                        ddlPopNRiskPerThousand.Attributes.Add("disabled", "disabled");


                    if (tableRatingAny)
                    {
                        var ddlTempData = new List<ListItem>();

                        foreach (var item in tableRating)
                        {
                            //Ratings
                            var maxRating = decimal.Parse(item.MaxValue);
                            var minRating = decimal.Parse(item.MinValue);

                            if (minRating != maxRating)
                            {
                                ddlTempData.Add(new ListItem() { Value = item.MinValue, Text = Decimal.Parse(item.MinValue).ToString("N2") });
                                ddlTempData.Add(new ListItem() { Value = item.MaxValue, Text = Decimal.Parse(item.MaxValue).ToString("N2") });
                            }
                            else
                                ddlTempData.Add(new ListItem() { Value = item.MinValue, Text = Decimal.Parse(item.MinValue).ToString("N2") });
                        }

                        ddlTempData = ddlTempData.Distinct().OrderBy(a => decimal.Parse(String.IsNullOrEmpty(a.Text) ? "0" : a.Text)).ToList();

                        ddlPopNRiskTableRating.DataSource = ddlTempData;
                        ddlPopNRiskTableRating.DataBind();
                    }

                    if (perThousandAny)
                    {
                        var ddlTempData = new List<ListItem>();

                        foreach (var item in perThousand)
                        {
                            //Ratings
                            var maxRating = decimal.Parse(item.MaxValue);
                            var minRating = decimal.Parse(item.MinValue);

                            if (minRating != maxRating)
                            {
                                ddlTempData.Add(new ListItem() { Value = item.MinValue, Text = Decimal.Parse(item.MinValue).ToString("N2") });
                                ddlTempData.Add(new ListItem() { Value = item.MaxValue, Text = Decimal.Parse(item.MaxValue).ToString("N2") });
                            }
                            else
                                ddlTempData.Add(new ListItem() { Value = item.MinValue, Text = Decimal.Parse(item.MinValue).ToString("N2") });
                        }

                        ddlTempData = ddlTempData.Distinct().OrderBy(a => decimal.Parse(String.IsNullOrEmpty(a.Text) ? "0" : a.Text)).ToList();

                        ddlPopNRiskPerThousand.DataSource = ddlTempData;
                        ddlPopNRiskPerThousand.DataBind();
                    }
                }
                else
                    ClearRatingInfo();
            }
        }

        private void ClearRatingInfo()
        {
            rBtnPopNRiskPerThousand.Enabled = false;
            rBtnPopNRiskTableRating.Enabled = false;

            rBtnPopNRiskPerThousand.Checked = false;
            rBtnPopNRiskTableRating.Checked = false;

            ddlPopNRiskTableRating.DataSource = new List<ListItem>();
            ddlPopNRiskTableRating.DataBind();

            ddlPopNRiskPerThousand.DataSource = new List<ListItem>();
            ddlPopNRiskPerThousand.DataBind();

            ddlPopNRiskTableRating.Attributes.Add("disabled", "disabled");
            ddlPopNRiskPerThousand.Attributes.Add("disabled", "disabled");
        }

        private void ClearData()
        {
            ddlPopNRiskType.SelectedIndex = 0;
            ddlPopNRiskCategory.SelectedIndex = 0;

            ddlPopNRiskConditionType.DataSource = new List<ListItem>();
            ddlPopNRiskConditionType.DataBind();

            ddlPopNRiskReason.SelectedIndex = -1;
            ddlPopNRiskReason.DataSource = new List<ListItem>(); ;
            ddlPopNRiskReason.DataBind();
        }

        protected void btnPopNRiskAddReason_Click(object sender, EventArgs e)
        {
            var tempList = new List<RiskGridItem>(RiskGridList);

            var idArray = ddlPopNRiskReason.SelectedValue.ToString().Split('|');

            var pageId = int.Parse(idArray[0]);
            var gridId = int.Parse(idArray[1]);
            var elementId = int.Parse(idArray[2]);
            var columnId = int.Parse(idArray[3]);

            //Fill New Item
            var newRiskItem = new RiskGridItem
            {
                RiskGroupId = int.Parse(ddlPopNRiskCategory.SelectedValue),
                RiskDetId = int.Parse(ddlPopNRiskConditionType.SelectedValue),
                RiskTypeId = int.Parse(ddlPopNRiskType.SelectedValue),
                PageId = pageId,
                GridId = gridId,
                ElementId = elementId,
                ColumnId = columnId,

                RiskTypeDesc = ddlPopNRiskType.SelectedItem.Text,
                CategoryDesc = ddlPopNRiskCategory.SelectedItem.Text,
                ConditionTypeDesc = ddlPopNRiskConditionType.SelectedItem.Text,
                ReasonDesc = ddlPopNRiskReason.SelectedItem.Text
            };

            //Validate that ther Reason is not on the list before adding
            if (tempList.Any(r => r.RiskGroupId == newRiskItem.RiskGroupId
                && r.RiskDetId == newRiskItem.RiskDetId
                && r.RiskTypeId == newRiskItem.RiskTypeId
                && r.PageId == newRiskItem.PageId
                && r.GridId == newRiskItem.GridId
                && r.ElementId == newRiskItem.ElementId
                && r.ColumnId == newRiskItem.ColumnId))
            {
                string message = "This Reason is already in the list, please select another one and try again.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "CustomDialogMessageEx('" + message + "', 500, 150, true, 'Invalid Value');", true);
                return;
            }

            //Add Item to the List and fill the Grid
            tempList.Add(newRiskItem);
            RiskGridList = tempList;
            FillGrid();

            //Clean Data
            //txtPopNRiskSuggestedRaiting.Text = "";
            ClearData();

            if (tempList.Count > 1)
            {
                FillRiskDrop = FillRiskDrop == null;
                FillRatings(new List<Entity.UnderWriting.Entities.Policy.OverPricePercentage>(), true);
            }

            pnlPopNRiskAddReason.Visible = false;
        }

        private void FillReasonData(int? pageId = null, int? gridId = null, int? elementId = null, int? columnId = null,
            int? riskGroupId = null, int? riskDetId = null, int? riskTypeId = null)
        {
            if (!pageId.HasValue)
            {
                var idArray = ddlPopNRiskReason.SelectedValue.ToString().Split('|');

                pageId = int.Parse(idArray[0]);
                gridId = int.Parse(idArray[1]);
                elementId = int.Parse(idArray[2]);
                columnId = int.Parse(idArray[3]);
                riskGroupId = int.Parse(String.IsNullOrWhiteSpace(ddlPopNRiskCategory.SelectedValue) ? "-1" : ddlPopNRiskCategory.SelectedValue);
                riskDetId = int.Parse(ddlPopNRiskConditionType.SelectedValue);
                riskTypeId = int.Parse(String.IsNullOrWhiteSpace(ddlPopNRiskType.SelectedValue) ? "-1" : ddlPopNRiskType.SelectedValue.ToString());
            }

            var data = Services.PolicyManager.GetOverPricePercentage(new Entity.UnderWriting.Entities.Policy.RiskRatingCondition()
            {
                CorpId = Service.Corp_Id,
                RiskGroupId = riskGroupId.Value,
                RiskDetId = riskDetId.Value,
                PageId = pageId.Value,
                GridId = gridId.Value,
                ElementId = elementId.Value,
                ColumnId = columnId.Value,
                RiskTypeId = riskTypeId.Value
            });


            if (!data.Any())
                txtPopNRiskSuggestedRaiting.Text = "--- No Results ---";
            else
            {
                string format, value;

                format = "Table Rating\n----------------\n{0}\n\nPer Thousand\n----------------\n{1}";
                value = string.Format(format,
                    string.Join(";", data.Where(r => r.RatingTypeId == (int)Tools.RisksRatingTypes.TableRating).Select(r => r.MinValue != r.MaxValue ? (r.MinValueIsNumeric ? Decimal.Parse(r.MinValue).ToString("N2") : r.MinValue) + "-" + (r.MaxValueIsNumeric ? Decimal.Parse(r.MaxValue).ToString("N2") : r.MaxValue) : (r.MaxValueIsNumeric ? Decimal.Parse(r.MaxValue).ToString("N2") : r.MaxValue))),
                    string.Join(";", data.Where(r => r.RatingTypeId == (int)Tools.RisksRatingTypes.PerThousand).Select(r => r.MinValue != r.MaxValue ? (r.MinValueIsNumeric ? Decimal.Parse(r.MinValue).ToString("N2") : r.MinValue) + "-" + (r.MaxValueIsNumeric ? Decimal.Parse(r.MaxValue).ToString("N2") : r.MaxValue) : (r.MaxValueIsNumeric ? Decimal.Parse(r.MaxValue).ToString("N2") : r.MaxValue)))
                    );

                txtPopNRiskSuggestedRaiting.Text = value;
            }

            FillRatings(data);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            var tempList = new List<RiskGridItem>(RiskGridList);
            var gridRow = (GridViewRow)((Button)sender).NamingContainer;

            var RiskGroupId = int.Parse(gvPopNRisks.DataKeys[gridRow.RowIndex]["RiskGroupId"].ToString());
            var RiskDetId = int.Parse(gvPopNRisks.DataKeys[gridRow.RowIndex]["RiskDetId"].ToString());
            var RiskTypeId = int.Parse(gvPopNRisks.DataKeys[gridRow.RowIndex]["RiskTypeId"].ToString());
            var PageId = int.Parse(gvPopNRisks.DataKeys[gridRow.RowIndex]["PageId"].ToString());
            var GridId = int.Parse(gvPopNRisks.DataKeys[gridRow.RowIndex]["GridId"].ToString());
            var ElementId = int.Parse(gvPopNRisks.DataKeys[gridRow.RowIndex]["ElementId"].ToString());
            var ColumnId = int.Parse(gvPopNRisks.DataKeys[gridRow.RowIndex]["ColumnId"].ToString());

            //Validate that ther Reason is on the list before Removing
            var tempItem = tempList.Where(r => r.RiskGroupId == RiskGroupId
                && r.RiskDetId == RiskDetId
                && r.RiskTypeId == RiskTypeId
                && r.PageId == PageId
                && r.GridId == GridId
                && r.ElementId == ElementId
                && r.ColumnId == ColumnId).First();

            if (tempItem != null)
            {
                //Remove Item to the List and fill the Grid
                tempList.Remove(tempItem);
                RiskGridList = tempList;
                FillGrid();

                //Clean Data
                //txtPopNRiskSuggestedRaiting.Text = "";
                ClearData();

                if (tempList.Count > 1)
                {
                    FillRiskDrop = FillRiskDrop == null;
                    FillRatings(new List<Entity.UnderWriting.Entities.Policy.OverPricePercentage>(), true);
                }
                else if (tempList.Count() == 1)
                {
                    var remainingItem = tempList.First();

                    var ratingData = Services.PolicyManager.GetOverPricePercentage(new Entity.UnderWriting.Entities.Policy.RiskRatingCondition()
                    {
                        CorpId = Service.Corp_Id,
                        RiskGroupId = remainingItem.RiskGroupId,
                        RiskDetId = remainingItem.RiskDetId,
                        PageId = remainingItem.PageId,
                        GridId = remainingItem.GridId,
                        ElementId = remainingItem.ElementId,
                        ColumnId = remainingItem.ColumnId,
                        RiskTypeId = remainingItem.RiskTypeId
                    });

                    FillRatings(ratingData);
                }
                else
                {
                    FillRiskDrop = null;
                    ClearRatingInfo();
                }
            }
        }

        protected void btnPopNRiskAdd_Click(object sender, EventArgs e)
        {
            var riskList = new List<Policy.RiskRating>();

            int? riderId = pnlRiderType.Visible ? ddlPopNRiderType.SelectedValue == "-1" || String.IsNullOrWhiteSpace(ddlPopNRiderType.SelectedValue.Split('|')[1]) ? (int?)null : int.Parse(ddlPopNRiderType.SelectedValue.Split('|')[1]) : (int?)null;
            int? riderTypeId = pnlRiderType.Visible ? ddlPopNRiderType.SelectedValue == "-1" || String.IsNullOrWhiteSpace(ddlPopNRiderType.SelectedValue.Split('|')[0]) ? (int?)null : int.Parse(ddlPopNRiderType.SelectedValue.Split('|')[0]) : (int?)null;

            Decimal? yearsReconsider = String.IsNullOrEmpty(txtPopNRiskReconsider.Text) ? (Decimal?)null : Decimal.Parse(txtPopNRiskReconsider.Text);
            Decimal? duration = String.IsNullOrEmpty(txtPopNRiskDuration.Text) ? (Decimal?)null : Decimal.Parse(txtPopNRiskDuration.Text);

            int durationMonths = duration.HasValue ? Decimal.ToInt32(Math.Round((duration.Value * 12), 0)) : 0;
            int reconsiderMonths = yearsReconsider.HasValue ? Decimal.ToInt32(Math.Round((yearsReconsider.Value * 12), 0)) : 0;

            /* Bmarroquin 04-03-17 Comento Codigo 
            if (!RiskGridList.Any())
            {
                var idArray = ddlPopNRiskReason.SelectedValue.ToString().Split('|');

                var pageId = int.Parse(idArray[0]);
                var gridId = int.Parse(idArray[1]);
                var elementId = int.Parse(idArray[2]);
                var columnId = int.Parse(idArray[3]);

                //Fill New Item
                var newRiskItem = new RiskGridItem
                {
                    RiskId = null,
                    SequenceReference = null,
                    RiskGroupId = int.Parse(ddlPopNRiskCategory.SelectedValue),
                    RiskDetId = int.Parse(ddlPopNRiskConditionType.SelectedValue),
                    RiskTypeId = int.Parse(ddlPopNRiskType.SelectedValue),
                    PageId = pageId,
                    GridId = gridId,
                    ElementId = elementId,
                    ColumnId = columnId,

                    RiskTypeDesc = ddlPopNRiskType.SelectedItem.Text,
                    CategoryDesc = ddlPopNRiskCategory.SelectedItem.Text,
                    ConditionTypeDesc = ddlPopNRiskConditionType.SelectedItem.Text,
                    ReasonDesc = ddlPopNRiskReason.SelectedItem.Text
                };

                RiskGridList.Add(newRiskItem);
            }
            */

            //Bmarroquin nueva implementacion... 
            var riskItem = new Policy.RiskRating()
            {
                //Para que funcione el Update !!
                RiskId = string.IsNullOrEmpty(hdnRiksID.Value) ? (int?)null : int.Parse(hdnRiksID.Value),
                SequenceReference = hdnSequenceRef.Value,
                //Key
                CorpId = Service.Corp_Id,
                RegionId = Service.Region_Id,
                CountryId = Service.Country_Id,
                DomesticRegId = Service.Domesticreg_Id,
                StateProvId = Service.State_Prov_Id,
                CityId = Service.City_Id,
                OfficeId = Service.Office_Id,
                CaseSeqNo = Service.Case_Seq_No,
                HistSeqNo = Service.Hist_Seq_No,

                //Rating Info
                OperationId = (int)Tools.RisksOperations.Risk,
                ClassificationId = 1,

                //Nuevos Seteos...
                RiskGroupId = int.Parse(ddlPopNRiskCategory.SelectedValue),
                RiskTypeId = int.Parse(ddlPopNRiskType.SelectedValue),
                RiskDetId = 1, //Quemado temporalmente hasta nueva implementacion....
                PageId = 1,
                GridId = 1,
                ElementId = 1,
                ColumnId = 1,
                SuggestedRating = String.IsNullOrWhiteSpace(txtPopNRiskSuggestedRaiting.Text) ? null : txtPopNRiskSuggestedRaiting.Text, //Los comentarios
                //Aqui guardare la condicion y la razon separados por el simbolo | esto para no crear mas campos y dado la premura del caso...pasado manana viene la SSF !!!! Dios MIO !!
                Comment = (String.IsNullOrWhiteSpace(txtPopNRiskConditionType.Text) ? (String.IsNullOrWhiteSpace(txtPopNRiskReason.Text) ? "" : txtPopNRiskReason.Text) : (String.IsNullOrWhiteSpace(txtPopNRiskReason.Text) ? txtPopNRiskConditionType.Text : (txtPopNRiskConditionType.Text + "|" + txtPopNRiskReason.Text))),
                //Bmarroquin 07-03-2017 Cambio para que el Table Rating como lista ya no es texto Editable
                //TableRating = String.IsNullOrWhiteSpace(txtPopNRiskTableRating.Text) ? (decimal?)null : Convert.ToDecimal(txtPopNRiskTableRating.Text),
                TableRating = ddlTableRatingRisk.SelectedValue != "-1" ? Decimal.Parse(ddlTableRatingRisk.SelectedValue) : (decimal?)null,
                //Fin Cambio  07-03-2017 
                PerThousandRating = String.IsNullOrWhiteSpace(txtPopNRiskPerThousand.Text) ? (decimal?)null : Convert.ToDecimal(txtPopNRiskPerThousand.Text),
                //Fin Nuevos Seteos

                StartDate = DateTime.Now,
                ReconsiderDate = yearsReconsider.HasValue ? DateTime.Now.AddMonths(reconsiderMonths) : (DateTime?)null,
                EndDate = duration.HasValue ? DateTime.Now.AddMonths(durationMonths) : (DateTime?)null,
                Duration = duration,
                YearToReconsider = yearsReconsider,
                RiskRateStatusId = 1, //Active                
                //ReasonDesc = item.ReasonDesc,

                //Contact
                ContactId = contactId,
                ContactRoleTypeId = contactRoleTypeId,

                //Rider Info
                RiderId = riderId,
                RiderTypeId = riderTypeId,

                //Underwriter
                UserId = Service.Underwriter_Id,
                RequestedBy = Service.Underwriter_Id
            };

            riskList.Add(riskItem);

            /*
            foreach (var item in RiskGridList)
            {
                
            }*/

            //Insert Risks
            if (IsEdit)
                Services.PolicyManager.UpdateRiskRating(riskList);
            else
                Services.PolicyManager.InsertRiskRating(riskList);

            ((HiddenField)this.Parent.Parent.Parent.FindControl("hfAddNewRisk")).Value = "false";
            ((UCPolicyPlanDataContainer)this.Parent.Parent.Parent).FillRiskData(IsAditionalInsured);

        }

        public void FillEditData(Boolean IsAditional, int ContactId, int ContactRoleTypeId, IEnumerable<Policy.RiskRating> riskRatingList,
            Decimal duration, Decimal yearsToReconsider, Decimal? tableRating = null, Decimal? perThousand = null)
        {
            IsEdit = true;
            IsAditionalInsured = IsAditional;
            contactId = ContactId;
            contactRoleTypeId = ContactRoleTypeId;

            pnlRiderType.Visible = !IsAditional;

            
            //Clear Fields
            clearData();
            ClearRatingInfo();            

            /*
            var tempList = riskRatingList.Select(item => new RiskGridItem()
            {
                RiskId = item.RiskId,
                SequenceReference = item.SequenceReference,
                ColumnId = item.ColumnId.Value,
                ElementId = item.ElementId.Value,
                GridId = item.GridId.Value,
                PageId = item.PageId.Value,
                RiskDetId = item.RiskDetId.Value,
                RiskGroupId = item.RiskGroupId.Value,
                RiskTypeId = item.RiskTypeId.Value,
                CategoryDesc = item.CategoryDesc,
                ConditionTypeDesc = item.ConditionTypeDesc,
                ReasonDesc = item.ReasonDesc,
                RiskTypeDesc = item.RiskTypeDesc,
                

            }).ToList();


            RiskGridList = tempList;
            */
            FillRiskDrop = null;

            //Fill Data
            FillDrops();
            //FillGrid();

            //Bmarroquin 03-03-2017 Cambio para que carguen los datos cuando se seleccionada la opcion de Editar 
            ddlPopNRiderType.SelectedIndex = 1; //q aparezca seleccionado Insured           
            ddlPopNRiskType.SelectedValue = riskRatingList.FirstOrDefault().RiskTypeId.ToString();
            ddlPopNRiskCategory.SelectedValue = riskRatingList.FirstOrDefault().RiskGroupId.ToString();

            //Bmarroquin 07-03-2017 Cambio para que el Table Rating como lista ya no es texto Editable 
            //txtPopNRiskTableRating.Text = riskRatingList.FirstOrDefault().TableRating.HasValue ? riskRatingList.FirstOrDefault().TableRating.ToString() : "" ;
            if (riskRatingList.FirstOrDefault().TableRating.HasValue)
            {
                var lIntCode = Convert.ToInt32(riskRatingList.FirstOrDefault().TableRating);
                string lStrRealCode;
                if (lIntCode <= 9)
                {
                    lStrRealCode = "0" + lIntCode.ToString();
                }
                else
                    lStrRealCode =  lIntCode.ToString();

                ddlTableRatingRisk.SelectedValue = lStrRealCode;
            }
            //Fin cambio Bmarroquin 07-03-2017

            txtPopNRiskPerThousand.Text = riskRatingList.FirstOrDefault().PerThousandRating.HasValue ? riskRatingList.FirstOrDefault().PerThousandRating.ToString() : "";
            txtPopNRiskSuggestedRaiting.Text = riskRatingList.FirstOrDefault().SuggestedRating; //Ahora son comentarios
            var lStr_Values = riskRatingList.FirstOrDefault().Comment;
            if (lStr_Values != null)
            {
                var lStr_ArrVal = lStr_Values.Split('|');
                if (lStr_ArrVal.IsValidIndex(0))
                {
                    txtPopNRiskConditionType.Text = lStr_ArrVal[0];
                }
                if (lStr_ArrVal.IsValidIndex(1))
                {
                    txtPopNRiskReason.Text = lStr_ArrVal[1];
                }
            }

            //Necesario para que funcione el Update
            hdnRiksID.Value = riskRatingList.FirstOrDefault().RiskId.HasValue ? riskRatingList.FirstOrDefault().RiskId.ToString() : "";
            hdnSequenceRef.Value = riskRatingList.FirstOrDefault().SequenceReference;

            //Fin cambios Bmarroquin 03-03-2017 

            EnableControls(false);

            //Fill Contact Info
            txtPopNRiskDuration.Text = duration.ToString();
            txtPopNRiskReconsider.Text = yearsToReconsider.ToString();
            
            ContactAge = Service.ContactAge;
            ContactSex = !String.IsNullOrEmpty(Service.ContactGender) && Service.ContactGender.ToLower() == "f";

            /* Bmarroquin comento codigo por nueva implementacion sobre la pantalla
            if (RiskGridList.Count > 1)
            {
                FillRiskDrop = FillRiskDrop == null;
                FillRatings(new List<Policy.OverPricePercentage>(), true);
            }
            else
            {
                var item = tempList.FirstOrDefault();
                FillReasonData(item.PageId, item.GridId, item.ElementId, item.ColumnId, item.RiskGroupId, item.RiskDetId, item.RiskTypeId);
            }

            if (tableRating.HasValue)
            {
                rBtnPopNRiskTableRating.Checked = true;
                ddlPopNRiskTableRating.Attributes.Remove("disabled");
                ddlPopNRiskTableRating.SelectedValue = tableRating.Value.ToString("N2");

                rBtnPopNRiskPerThousand.Checked = false;
                ddlPopNRiskPerThousand.Attributes.Add("disabled", "disabled");
            }
            else if (perThousand.HasValue)
            {
                rBtnPopNRiskTableRating.Checked = false;
                ddlPopNRiskTableRating.Attributes.Add("disabled", "disabled");

                rBtnPopNRiskPerThousand.Checked = true;
                ddlPopNRiskPerThousand.Attributes.Remove("disabled");
                ddlPopNRiskPerThousand.SelectedValue = perThousand.Value.ToString("N2");
            }
            */
        }

        public void FillData()
        {
            throw new NotImplementedException();
        }

        private void EnableControls(bool status)
        {
            //ddlPopNRiskCategory.Enabled = status;
            ddlPopNRiskConditionType.Enabled = status;
            ddlPopNRiskType.Enabled = status;
            ddlPopNRiskReason.Enabled = status;

            gvPopNRisks.Columns[4].Visible = status;

            if (!pnlRiderType.Visible) return;
            ddlPopNRiderType.Enabled = status;
            ddlPopNRiderType.SelectedIndex = 1;
        }
    }
}