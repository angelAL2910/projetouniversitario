﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle
{
    public partial class WUCFinalBeneficary : UC, IUC
    {
        public TextBox txtName { get; set; }
        public TextBox txtPorcentaje { get; set; }
        //public CheckBox chkIsPep { get; set; }
        //public DropDownList ddlTipoPepBenef { get; set; }
        //public Label pepFormularyOptionsId { get; set; }

        public DropDownList ddlTipoId { get; set; }
        public TextBox txtIdentificacion { get; set; }
        public DropDownList ddlCountryOfBirth { get; set; }

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
            public Nullable<int> FinalBeneficiaryId { get; set; }
            public Nullable<int> IdentificationTypeId { get; set; }
            public string IdentificationNumber { get; set; }
            public Nullable<int> NationalityCountryId { get; set; }
            public string ContactIdTypeDesc { get; set; }
            public string GlobalCountryDesc { get; set; }
        }

        public string ClickButtonScript
        {
            get
            {
                return
                      ViewState["ClickButtonScript"].ToString();
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

        public int SelectedRowIndex
        {
            get
            {
                return
                      ViewState["SelectedRowIndex"].ToInt();
            }

            set
            {
                ViewState["SelectedRowIndex"] = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {

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
            int i = 0;
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
                PepFormularyOptionsId = h.PepFormularyOptionsId,
                NationalityCountryId = h.NationalityCountryId,
                IdentificationTypeId = h.IdentificationTypeId,
                IdentificationNumber = h.IdentificationNumber,
                ContactIdTypeDesc = h.ContactIdTypeDesc,
                GlobalCountryDesc = h.GlobalCountryDesc
            });

            dataFinalBenef.AddRange(dataFormated);
            oTemDataFinalBenef = dataFinalBenef;

            FillGridFinalBeneficiary();
            AddDropDownPeps();

            bool HasPep = false;
            #region Buscando beneficiarios que sean pep para insertarlos en la DB
            var peps = oTemDataFinalBenef.Where(o => o.IsPEP == true).ToList();
            HasPep = (peps.Count > 0);


            if (HasPep)
            {
                Utility.ExcecuteJScript(this, "setIsPep('true');");
            }
            else
                Utility.ExcecuteJScript(this, "setIsPep('false')");
            #endregion
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


            AddDropDownPeps();

            DoClickEdit(Index);
        }

        private void SetControls(DevExpress.Web.ASPxGridView grid, int VisibleIndex)
        {
            txtName = grid.FindRowCellTemplateControl(VisibleIndex, null, "txtName") as TextBox;
            txtPorcentaje = grid.FindRowCellTemplateControl(VisibleIndex, null, "txtPorcentaje") as TextBox;
            ddlTipoId = grid.FindRowCellTemplateControl(VisibleIndex, null, "ddlTipoIdentifiacion") as DropDownList;
            ddlCountryOfBirth = grid.FindRowCellTemplateControl(VisibleIndex, null, "ddlCountryOfBirth") as DropDownList;
            txtIdentificacion = grid.FindRowCellTemplateControl(VisibleIndex, null, "txtIdentificacion") as TextBox;
            //chkIsPep = grid.FindRowCellTemplateControl(VisibleIndex, null, "chkIsPep") as CheckBox;
            //ddlTipoPepBenef = grid.FindRowCellTemplateControl(VisibleIndex, null, "ddlTipoPepBenef") as DropDownList;
        }

        private void EditModeFinalBen(bool isEdit, DevExpress.Web.ASPxGridView grid, int VisibleIndex, FinalBenef Record = null)
        {
            SetControls(grid, VisibleIndex);
            var ltName = grid.FindRowCellTemplateControl(VisibleIndex, null, "ltName") as Control;
            var ltPorcentaje = grid.FindRowCellTemplateControl(VisibleIndex, null, "ltPorcentaje") as Control;
            var IsPep = grid.FindRowCellTemplateControl(VisibleIndex, null, "chkIsPep") as CheckBox;
            var tipoPep = grid.FindRowCellTemplateControl(VisibleIndex, null, "ddlTipoPepBenef") as DropDownList;
            var pepFormularyOptionsId = grid.FindRowCellTemplateControl(VisibleIndex, null, "pepFormularyOptionsId") as Label;
            var ltTipoId = grid.FindRowCellTemplateControl(VisibleIndex, null, "ltTipoIdentificacion") as Control;
            var ltIdentificacion = grid.FindRowCellTemplateControl(VisibleIndex, null, "ltIdentificacion") as Control;
            var ltCountryOfBirth = grid.FindRowCellTemplateControl(VisibleIndex, null, "ltCountryOfBirth") as Control;


            if (ddlCountryOfBirth != null && ddlCountryOfBirth.Items.Count <= 0)
            {
                var dataCountry = ObjServices.GetDropDownByType(Utility.DropDownType.Country, new Entity.UnderWriting.Entities.DropDown.Parameter { LanguageId = ObjServices.Language.ToInt() });
                ddlCountryOfBirth.DataSource = null;
                ddlCountryOfBirth.DataBind();
                ddlCountryOfBirth.DataSource = dataCountry;
                ddlCountryOfBirth.DataTextField = "GlobalCountryDesc";
                ddlCountryOfBirth.DataValueField = "CountryId";
                ddlCountryOfBirth.DataBind();
                ddlCountryOfBirth.Items.Insert(0, new ListItem { Text = "----", Value = "-1" });
                ddlCountryOfBirth.Visible = isEdit;
                ltCountryOfBirth.Visible = !isEdit;
            }

            if (ddlTipoId != null && ddlTipoId.Items.Count <= 0)
            {
                var dataIDtype = ObjServices.GetDropDownByType(Utility.DropDownType.IdType, new Entity.UnderWriting.Entities.DropDown.Parameter { LanguageId = ObjServices.Language.ToInt() });
                ddlTipoId.DataSource = null;
                ddlTipoId.DataBind();
                ddlTipoId.DataSource = dataIDtype;
                ddlTipoId.DataTextField = "ContactTypeIdDesc";
                ddlTipoId.DataValueField = "ContactTypeId";
                ddlTipoId.DataBind();
                ddlTipoId.Items.Insert(0, new ListItem { Text = "----", Value = "-1" });
                ddlTipoId.Visible = isEdit;
                ltTipoId.Visible = !isEdit;
            }

            if (txtIdentificacion != null) txtIdentificacion.Visible = isEdit;
            if (ltIdentificacion != null) ltIdentificacion.Visible = !isEdit;

            if (txtName != null) txtName.Visible = isEdit;
            if (ltName != null) ltName.Visible = !isEdit;

            if (txtPorcentaje != null) txtPorcentaje.Visible = isEdit;
            if (ltPorcentaje != null) ltPorcentaje.Visible = !isEdit;

            txtPorcentaje.Enabled = IsCompany.GetValueOrDefault();

            if (txtPorcentaje.Enabled)
                txtPorcentaje.Attributes.Add("validation", "Required");
            else
                txtPorcentaje.Attributes.Remove("validation");

            if (!string.IsNullOrEmpty(pepFormularyOptionsId.Text))
            {
                if (pepFormularyOptionsId.Text.ToInt() > 0)
                    tipoPep.SelectedValue = pepFormularyOptionsId.Text;
            }

            if (IsPep != null)
                IsPep.Enabled = true;

            if (tipoPep != null)
                tipoPep.Enabled = true;

        }

        private void ActualizaContactForm()
        {
            var HasRecords = oTemDataFinalBenef.Any();

            var IllustrationsVehiclePage = Page as WEB.NewBusiness.NewBusiness.Pages.IllustrationsVehicle;

            if (IllustrationsVehiclePage != null)
            {
                var UCContactEditForm = Utility.GetAllChildren(IllustrationsVehiclePage).FirstOrDefault(uc => uc is UCContactEditForm);
                if (UCContactEditForm != null)
                {
                    var oForm = (UCContactEditForm as UCContactEditForm);

                    var btnVerBeneficiariosFinales = oForm.FindControl("btnVerBeneficiariosFinales");
                    if (btnVerBeneficiariosFinales != null)
                    {
                        (btnVerBeneficiariosFinales as LinkButton).Visible = HasRecords;

                        var udpContactEditForm = oForm.FindControl("udpContactEditForm");
                        if (udpContactEditForm != null)
                            (udpContactEditForm as UpdatePanel).Update();

                        if (!HasRecords)
                        {
                            var ddlBeneFinal = oForm.FindControl("ddlBeneFinal");
                            if (ddlBeneFinal != null)
                                (ddlBeneFinal as DropDownList).SelectIndexByValue("-1");
                        }

                        var mpeFinalBeneficiary = oForm.FindControl("mpeFinalBeneficiary");
                        if (mpeFinalBeneficiary != null)
                            (mpeFinalBeneficiary as AjaxControlToolkit.ModalPopupExtender).Show();

                    }

                    oForm.save();
                }
            }
        }

        protected void gvPEP_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            var grid = sender as DevExpress.Web.ASPxGridView;
            var Command = e.CommandArgs.CommandName;
            SelectedRowIndex = e.VisibleIndex;
            var RecordIndex = grid.GetKeyFromAspxGridView("RecordIndex", e.VisibleIndex).ToInt();
            var StatusStr = grid.GetKeyFromAspxGridView("Status", e.VisibleIndex).ToString();
            var Status = (Utility.RecordStatus)Enum.Parse(typeof(Utility.RecordStatus), StatusStr);

            var btnEditOrSave = grid.FindRowCellTemplateControl(e.VisibleIndex, null, "btnEditOrSave") as LinkButton;
            var btnCancel = grid.FindRowCellTemplateControl(e.VisibleIndex, null, "btnCancel") as LinkButton;
            var btnDelete = grid.FindRowCellTemplateControl(e.VisibleIndex, null, "pnDelete") as Panel;
            var IsPep = grid.FindRowCellTemplateControl(e.VisibleIndex, null, "chkIsPep") as CheckBox;
            var ddlTiposPeps = grid.FindRowCellTemplateControl(e.VisibleIndex, null, "ddlTipoPepBenef") as DropDownList;
            var pepFormularyOptionsId = grid.FindRowCellTemplateControl(e.VisibleIndex, null, "pepFormularyOptionsId") as Label;

            if (ddlTiposPeps != null)
                if (ddlTiposPeps.SelectedIndex > 0 && !IsPep.Checked)
                    IsPep.Checked = true;

            var Record = (isInsertingNewRecord) ? oTemDataFinalBenef.Last()
                                                : oTemDataFinalBenef.FirstOrDefault(r => r.RecordIndex == RecordIndex);

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
                                                                          Record.PepFormularyOptionsId, null, null, null
                                                                         );
                    if (Record.IsPEP.GetValueOrDefault())
                    {
                        //busco los peps que tenga ese contacto
                        var DataPeps = ObjServices.oContactManager.GetContactPEPFormulary(this.ContactId, "CalidadPep").ToList();
                        if (DataPeps.Any())
                        {
                            var PepToDelete = DataPeps.Where(p => p.BeneficiaryId == RecordIndex).ToList(); //busco los datos del pep asosciado a ese beneficiaryId
                            if (PepToDelete.Any())
                            {
                                ObjServices.oContactManager.SetContactPepFormulary(this.ContactId,
                                                                                    PepToDelete[0].PepFormularyId,
                                                                                    PepToDelete[0].Name,
                                                                                    PepToDelete[0].RelationshipId,
                                                                                    PepToDelete[0].Position,
                                                                                    PepToDelete[0].FromYear.ToInt(),
                                                                                    PepToDelete[0].ToYear.ToInt(),
                                                                                    false,
                                                                                    ObjServices.UserID.GetValueOrDefault(),
                                                                                    PepToDelete[0].BeneficiaryId,
                                                                                    PepToDelete[0].IsPepManagerCompany
                                                                                    );
                            }

                        }

                    }

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
                    if (Record.IdentificationTypeId.HasValue)
                        ddlTipoId.SelectedValue = Record.IdentificationTypeId.ToString();

                    if (Record.NationalityCountryId.HasValue)
                        ddlCountryOfBirth.SelectedValue = Record.NationalityCountryId.ToString();

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

                    Record.IsPEP = IsPep != null ? IsPep.Checked : (bool?)null;

                    int? identificationTypeId = ddlTipoId.SelectedValue.ToInt();
                    string identificationNumber = txtIdentificacion.Text;
                    int? nationalityCountryId = ddlCountryOfBirth.SelectedValue.ToInt();

                    if (ddlTiposPeps != null)
                    {
                        Record.PepFormularyOptionsId = ddlTiposPeps.SelectedValue.ToInt();
                        pepFormularyOptionsId.Text = ddlTiposPeps.SelectedValue;
                    }

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

                    if (IsPep != null && IsPep.Checked)
                    {
                        var setPepResult = ObjServices.oContactManager.SetContactPepFormulary(this.ContactId,
                                    Record.RecordIndex,
                                    Record.NombreCompleto,
                                    null,
                                    null,
                                    null,
                                    null,
                                    true,
                                    ObjServices.UserID.GetValueOrDefault(),
                                    Record.RecordIndex,
                                    false
                                    );
                    }

                    FillData();
                    break;
            }
        }

        protected void gvFinalBeneficiary_PageIndexChanged(object sender, EventArgs e)
        {
            FillData();
        }

        public void AddDropDownPeps()
        {
            int i = 0;
            var PepsOptions = ObjServices.GettingDropData(Utility.DropDownType.PepFormularyOption).ToList();
            PepsOptions = PepsOptions.Where(o => o.AgentName.ToLower().Contains("si")).ToList();

            //var dropTipoPep = gvFinalBeneficiary.FindRowCellTemplateControl(Index, null, "ddlTipoPepBenef") as DropDownList;

            //seteando el valor de cada dropdown
            for (i = 0; i <= oTemDataFinalBenef.Count() - 1; i++)
            {
                var chk = gvFinalBeneficiary.FindRowCellTemplateControl(i, null, "chkIsPep") as CheckBox;
                var TipoPepBenef = gvFinalBeneficiary.FindRowCellTemplateControl(i, null, "ddlTipoPepBenef") as DropDownList;
                if (chk != null && TipoPepBenef != null)
                {
                    TipoPepBenef.DataSource = PepsOptions;
                    TipoPepBenef.DataTextField = "AgentName";
                    TipoPepBenef.DataValueField = "AgentId";
                    TipoPepBenef.DataBind();

                    TipoPepBenef.Items.Insert(0, new ListItem { Text = "----", Value = "-1" });

                    var pepFormularyOptionsId = gvFinalBeneficiary.FindRowCellTemplateControl(i, null, "pepFormularyOptionsId") as Label;
                    if (!string.IsNullOrEmpty(pepFormularyOptionsId.Text))
                    {
                        if (pepFormularyOptionsId.Text.ToInt() > 0)
                        {
                            TipoPepBenef.SelectedIndex = pepFormularyOptionsId.Text.ToInt();
                            chk.Checked = true;
                        }
                        else
                            chk.Checked = false;
                    }

                    TipoPepBenef.Enabled = false;
                    chk.Enabled = false;

                    if (chk.Checked)
                        Utility.ExcecuteJScript(this, "setIsPep('true');");
                    else
                        Utility.ExcecuteJScript(this, "setIsPep('false');");
                }
            }
        }

        protected void ddlTipoIdentifiacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControls(gvFinalBeneficiary, SelectedRowIndex);

            var drop = (sender as DropDownList);
            var ValueSelected = drop.SelectedValue;
            txtIdentificacion.Attributes.Remove("Cedula");
            txtIdentificacion.Attributes.Remove("RncNumber");

            switch (ValueSelected)
            {
                case "1":
                    txtIdentificacion.Attributes.Add("Cedula", "Cedula");
                    break;
                case "5":
                    txtIdentificacion.Attributes.Add("Rnc", "Rnc");
                    break;
            }

            txtIdentificacion.Focus();
        }
    }
}