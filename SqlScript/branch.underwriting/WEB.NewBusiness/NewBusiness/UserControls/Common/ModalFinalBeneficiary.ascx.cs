﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.Common
{
    public partial class ModalFinalBeneficiary : UC, IUC
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public TextBox txtName { get; set; }
        public TextBox txtPorcentaje { get; set; }
        public CheckBox chkIsPep { get; set; }
        public DropDownList ddlTipoPepBenef { get; set; }

        private List<FinalBenef> dataFinalBenef = new List<FinalBenef>(0);

        [Serializable]
        public class FinalBenef
        {
            public Utility.RecordStatus Status { get; set; }
            public int RecordIndex { get; set; }
            public string NombreCompleto { get; set; }
            public decimal? Porcentaje { get; set; }
            public Nullable<bool> IsPEP { get; set; }
            public Nullable<int> PepFormularyOptionsId { get; set; }
        }

        public string ClickButtonScript
        {
            get
            {

                if (ViewState["ClickButtonScript"] != null)
                {
                    return
               ViewState["ClickButtonScript"].ToString();
                }
                else
                {
                    return
                         string.Empty;
                }


            }

            set
            {
                ViewState["ClickButtonScript"] = value;
            }

        }

        public int ContactId
        {
            get
            {
                return
                      ViewState["ContactId"].ToInt();
            }

            set
            {
                ViewState["ContactId"] = value;
            }

        }

        public bool? IsCompany
        {
            get
            {
                return
                      ViewState["IsCompany"].ToBoolean();
            }

            set
            {
                ViewState["IsCompany"] = value;
            }

        }

        public bool HasFinalBenef
        {
            get
            {
                return
                      ViewState["HasFinalBenef"].ToBoolean();
            }

            set
            {
                ViewState["HasFinalBenef"] = value;
            }

        }

        public List<FinalBenef> oTemDataFinalBenef
        {
            get
            {
                return ViewState["TemDataFinalBenef"] == null ?
                    new List<FinalBenef>() :
                    ViewState["TemDataFinalBenef"] as List<FinalBenef>;
            }

            set
            {
                List<FinalBenef> tempList = null;

                if (value != null)
                {
                    tempList = new List<FinalBenef>(
                            ViewState["TemDataFinalBenef"] != null
                            ?
                            (
                               (List<FinalBenef>)ViewState["TemDataFinalBenef"]
                            )
                            :
                            new List<FinalBenef>()
                      );

                    tempList.AddRange(value);
                }

                ViewState["TemDataFinalBenef"] = tempList;
            }
        }

        private bool isInsertingNewRecord
        {
            get
            {
                return ViewState["isInsertingNewRecord"].ToBoolean();
            }
            set
            {
                ViewState["isInsertingNewRecord"] = value;
            }

        }

        public void Translator(string Lang)
        {
            throw new NotImplementedException();
        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            throw new NotImplementedException();
        }

        public void save()
        {

        }

        public void edit()
        {

        }

        private void FillGridFinalBeneficiary(bool isLocal = false)
        {
            gvFinalBeneficiary.DataSource = oTemDataFinalBenef;
            gvFinalBeneficiary.DataBind();
        }

        public IEnumerable<Entity.UnderWriting.Entities.Contact.FinalBeneficiary.FinalBenResult> HasFinalBenefEx(int ContactId)
        {
            var dataFinalBenficiary = ObjServices.oContactManager.GetContactFinalBeneficiary(ContactId);
            return
                dataFinalBenficiary;
        }

        public void FillData()
        {
            //Verificar si existen datos en la base de datos
            var dataFinalBenficiary = ObjServices.oContactManager.GetContactFinalBeneficiary(this.ContactId);
            HasFinalBenef = dataFinalBenficiary.Any();
            oTemDataFinalBenef = null;
            var dataFormated = dataFinalBenficiary.Select(h => new FinalBenef
            {
                Status = Utility.RecordStatus.Old,
                RecordIndex = h.FinalBeneficiaryId,
                NombreCompleto = h.Name,
                Porcentaje = h.PercentageParticipation,
                IsPEP = h.IsPEP,
                PepFormularyOptionsId = h.PepFormularyOptionsId
            });

            dataFinalBenef.AddRange(dataFormated);
            oTemDataFinalBenef = dataFinalBenef;

            FillGridFinalBeneficiary();
        }

        public void Initialize()
        {
            ClearData();
            FillData();
        }

        public void ClearData()
        {
            IsCompany = null;
            HasFinalBenef = false;
            isInsertingNewRecord = false;
            oTemDataFinalBenef = null;
        }

        private void DoClickEdit(int Index)
        {
            var script = string.Format(this.ClickButtonScript, Index);
            this.ExcecuteJScript(script);
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            int RecIndex = oTemDataFinalBenef.Count() + 1;

            if (isInsertingNewRecord)
                return;

            dataFinalBenef.Add(new FinalBenef
            {
                RecordIndex = RecIndex,
                Status = Utility.RecordStatus.New,
                NombreCompleto = string.Empty,
                Porcentaje = 0m,
                IsPEP = false,
                PepFormularyOptionsId = 0
            });

            isInsertingNewRecord = true;
            oTemDataFinalBenef = dataFinalBenef;
            FillGridFinalBeneficiary(true);
            gvFinalBeneficiary.PageIndex = gvFinalBeneficiary.PageCount;
            var Index = oTemDataFinalBenef.Count - 1;
            DoClickEdit(Index);
        }

        private void SetControls(DevExpress.Web.ASPxGridView grid, int VisibleIndex)
        {
            txtName = grid.FindRowCellTemplateControl(VisibleIndex, null, "txtName") as TextBox;
            txtPorcentaje = grid.FindRowCellTemplateControl(VisibleIndex, null, "txtPorcentaje") as TextBox;
            chkIsPep = grid.FindRowCellTemplateControl(VisibleIndex, null, "chkIsPep") as CheckBox;
            ddlTipoPepBenef = grid.FindRowCellTemplateControl(VisibleIndex, null, "ddlTipoPepBenef") as DropDownList;
        }

        private void EditModeFinalBen(bool isEdit, DevExpress.Web.ASPxGridView grid, int VisibleIndex, FinalBenef Record = null)
        {
            SetControls(grid, VisibleIndex);
            var ltName = grid.FindRowCellTemplateControl(VisibleIndex, null, "ltName") as Control;
            var ltPorcentaje = grid.FindRowCellTemplateControl(VisibleIndex, null, "ltPorcentaje") as Control;

            if (txtName != null) txtName.Visible = isEdit;
            if (ltName != null) ltName.Visible = !isEdit;

            if (txtPorcentaje != null) txtPorcentaje.Visible = isEdit;
            if (ltPorcentaje != null) ltPorcentaje.Visible = !isEdit;

            txtPorcentaje.Enabled = isEdit;

            if (txtPorcentaje.Enabled)
                txtPorcentaje.Attributes.Add("validation", "Required");
            else
                txtPorcentaje.Attributes.Remove("validation");

            if (chkIsPep != null)
            {
                if (chkIsPep.Checked)
                    ddlTipoPepBenef.Attributes.Add("validation", "Required");
                else
                    ddlTipoPepBenef.Attributes.Remove("validation");
            }

        }

        //private void ActualizaContactForm()
        //{
        //    var HasRecords = oTemDataFinalBenef.Any();

        //    var IllustrationsVehiclePage = Page as WEB.NewBusiness.NewBusiness.Pages.IllustrationsVehicle;

        //    if (IllustrationsVehiclePage != null)
        //    {
        //        var UCContactEditForm = Utility.GetAllChildren(IllustrationsVehiclePage).FirstOrDefault(uc => uc is UCContactEditForm);
        //        if (UCContactEditForm != null)
        //        {
        //            var oForm = (UCContactEditForm as UCContactEditForm);

        //            var btnVerBeneficiariosFinales = oForm.FindControl("btnVerBeneficiariosFinales");
        //            if (btnVerBeneficiariosFinales != null)
        //            {
        //                (btnVerBeneficiariosFinales as LinkButton).Visible = HasRecords;

        //                var udpContactEditForm = oForm.FindControl("udpContactEditForm");
        //                if (udpContactEditForm != null)
        //                    (udpContactEditForm as UpdatePanel).Update();

        //                if (!HasRecords)
        //                {
        //                    var ddlBeneFinal = oForm.FindControl("ddlBeneFinal");
        //                    if (ddlBeneFinal != null)
        //                        (ddlBeneFinal as DropDownList).SelectIndexByValue("-1");
        //                }

        //                var mpeFinalBeneficiary = oForm.FindControl("mpeFinalBeneficiary");
        //                if (mpeFinalBeneficiary != null)
        //                    (mpeFinalBeneficiary as AjaxControlToolkit.ModalPopupExtender).Show();

        //            }

        //            oForm.save();
        //        }
        //    }
        //}

        protected void gvPEP_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            var grid = sender as DevExpress.Web.ASPxGridView;
            var Command = e.CommandArgs.CommandName;

            var RecordIndex = grid.GetKeyFromAspxGridView("RecordIndex", e.VisibleIndex).ToInt();
            var StatusStr = grid.GetKeyFromAspxGridView("Status", e.VisibleIndex).ToString();
            var Status = (Utility.RecordStatus)Enum.Parse(typeof(Utility.RecordStatus), StatusStr);

            var btnEditOrSave = grid.FindRowCellTemplateControl(e.VisibleIndex, null, "btnEditOrSave") as LinkButton;
            var btnCancel = grid.FindRowCellTemplateControl(e.VisibleIndex, null, "btnCancel") as LinkButton;
            var btnDelete = grid.FindRowCellTemplateControl(e.VisibleIndex, null, "pnDelete") as Panel;

            var Record = (isInsertingNewRecord) ? oTemDataFinalBenef.Last()
                                                : oTemDataFinalBenef.FirstOrDefault(r => r.RecordIndex == RecordIndex);

            int? identificationTypeId = -1;
            string identificationNumber = string.Empty;
            int? nationalityCountryId = -1;

            switch (Command)
            {
                case "Delete":
                    ObjServices.oContactManager.SetContactFinalBeneficiary(this.ContactId,
                                                                          (Status == Utility.RecordStatus.Old) ? Record.RecordIndex : (int?)null,
                                                                          Record.NombreCompleto,
                                                                          Record.Porcentaje,
                                                                          false,
                                                                          ObjServices.UserID.GetValueOrDefault(),
                                                                          Record.IsPEP,
                                                                          Record.PepFormularyOptionsId,
                                                                          null,
                                                                          null,
                                                                          null
                                                                         );

                    FillData();
                    break;
                case "Cancel":
                    if (btnCancel != null) btnCancel.Visible = false;
                    if (btnEditOrSave != null) btnEditOrSave.CssClass = "myedit_file";
                    if (btnDelete != null) btnDelete.Visible = true;
                    EditModeFinalBen(false, grid, e.VisibleIndex);
                    btnEditOrSave.CommandName = "Edit";

                    var hasNewRecord = oTemDataFinalBenef.Any(p => p.Status == Utility.RecordStatus.New);
                    if (hasNewRecord)
                    {
                        var result = oTemDataFinalBenef.Where(p => p.Status != Utility.RecordStatus.New).ToList();
                        oTemDataFinalBenef = null;
                        oTemDataFinalBenef = result;
                        FillGridFinalBeneficiary();
                        gvFinalBeneficiary.PageIndex = 0;
                    }

                    isInsertingNewRecord = false;
                    break;
                case "Edit":
                    if (btnCancel != null) btnCancel.Visible = true;
                    if (btnEditOrSave != null) btnEditOrSave.CssClass = "mysave_file";
                    if (btnDelete != null) btnDelete.Visible = false;
                    btnEditOrSave.CommandName = "Save";
                    EditModeFinalBen(true, grid, e.VisibleIndex, Record);
                    break;
                case "Save":
                    btnEditOrSave.CommandName = "Edit";
                    if (btnCancel != null) btnCancel.Visible = false;
                    if (btnEditOrSave != null) btnEditOrSave.CssClass = "myedit_file";
                    if (btnDelete != null) btnDelete.Visible = true;
                    isInsertingNewRecord = false;
                    EditModeFinalBen(false, grid, e.VisibleIndex, Record);
                    SetControls(grid, e.VisibleIndex);
                    Record.NombreCompleto = txtName.Text;
                    Record.Status = Utility.RecordStatus.Old;
                    Record.Porcentaje = txtPorcentaje.ToDecimal();
                    Record.IsPEP = chkIsPep.Checked;
                    Record.PepFormularyOptionsId = ddlTipoPepBenef.SelectedValue.ToInt();

                    ObjServices.oContactManager.SetContactFinalBeneficiary(this.ContactId,
                                                                          (Status == Utility.RecordStatus.Old) ? Record.RecordIndex : (int?)null,
                                                                           Record.NombreCompleto,
                                                                           Record.Porcentaje,
                                                                           true,
                                                                           ObjServices.UserID.GetValueOrDefault(),
                                                                           Record.IsPEP,
                                                                           Record.PepFormularyOptionsId,
                                                                           identificationTypeId,
                                                                           identificationNumber,
                                                                           nationalityCountryId
                                                                         );
                    FillData();
                    break;
            }
        }
    }
}