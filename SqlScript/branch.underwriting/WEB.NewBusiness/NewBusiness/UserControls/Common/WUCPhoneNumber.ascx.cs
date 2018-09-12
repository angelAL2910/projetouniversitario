﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;
using RESOURCE.UnderWriting.NewBussiness;

namespace WEB.NewBusiness.NewBusiness.UserControls
{
    public partial class WUCPhoneNumber : UC, IUC
    {
        protected void UpdatePanel_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
            }

        }

        #region TempData
        public List<Entity.UnderWriting.Entities.Contact.Phone> TempDataContactPhone = new List<Entity.UnderWriting.Entities.Contact.Phone>();
        #endregion
        public void UpdateUpdatePanel()
        {
            udpPhoneNumber.Update();
        }

        public int? RowIndex
        {
            get { return int.Parse(Session[PrefixSession + "_RowIndexPhoneContact"].ToString()); }
            set { Session[PrefixSession + "_RowIndexPhoneContact"] = value; }
        }

        public Utility.OperationType Operation
        {
            get { return ((Utility.OperationType)Session[PrefixSession + "_OperationPhone"]); }
            set
            {
                Session[PrefixSession + "_OperationPhone"] = value;
                gvCommonPhone.Enabled = (value == Utility.OperationType.Insert);
                if (currentTab == "PlanPolicy")
                    gvCommonPhone.Columns[7].Visible = (value == Utility.OperationType.Insert);
            }
        }

        public String PrefixSession
        {
            get { return hdnCurrentSession.Value; }
            set { hdnCurrentSession.Value = value; }
        }

        public List<Entity.UnderWriting.Entities.Contact.Phone> oTemDataPhoneContact
        {
            get
            {
                return Session[PrefixSession + "_TemDataPhoneContact"] == null ?
                    new List<Entity.UnderWriting.Entities.Contact.Phone>() :
                    Session[PrefixSession + "_TemDataPhoneContact"] as List<Entity.UnderWriting.Entities.Contact.Phone>;
            }

            set
            {
                List<Entity.UnderWriting.Entities.Contact.Phone> tempList = null;

                if (value != null)
                {
                    tempList = new List<Entity.UnderWriting.Entities.Contact.Phone>(
                            Session[PrefixSession + "_TemDataPhoneContact"] != null
                            ?
                            (
                               (List<Entity.UnderWriting.Entities.Contact.Phone>)Session[PrefixSession + "_TemDataPhoneContact"]
                            )
                            :
                            new List<Entity.UnderWriting.Entities.Contact.Phone>()
                      );

                    tempList.AddRange(value);
                }

                Session[PrefixSession + "_TemDataPhoneContact"] = tempList;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Operation = Utility.OperationType.Insert;
                gvCommonPhone.FocusedRowIndex = -1;
            }
            udpPhoneNumber.Update();
        }

        public void LoadSameDataFromInsured(int? ContactID)
        {
            if (ContactID.HasValue)
            {
                oTemDataPhoneContact = null;

                oTemDataPhoneContact = ObjServices.oContactManager.GetCommunicatonPhone(this.ObjServices.Corp_Id, ContactID.Value, languageId: ObjServices.Language.ToInt()).ToList();

                var data = (from source in oTemDataPhoneContact
                            select new
                            {
                                source.DirectoryId,
                                source.DirectoryDetailId,
                                Type = source.DirectoryTypeDesc,
                                source.CountryCode,
                                source.AreaCode,
                                PhoneNumber = Int64.Parse(source.PhoneNumber.GetNumber()),
                                Ext = source.PhoneExt,
                                source.IsPrimary,
                                imgClassIsPrimary = source.IsPrimary ? "primary_chk" : ""
                            }).ToList();

                hdnTotalPhones.Value = data.Count().ToString();
                gvCommonPhone.DataSource = data;
                gvCommonPhone.DataBind();
                gvCommonPhone.FocusedRowIndex = -1;
            }
            else
            {
                gvCommonPhone.DataSource = null;
                gvCommonPhone.DataBind();
            }

            udpPhoneNumber.Update();
        }

        /// <summary>
        /// Bindear el grid
        /// </summary>
        public void FillData()
        {
            if (ObjServices.ContactEntityID > 0)
            {
                oTemDataPhoneContact = null;
                oTemDataPhoneContact = ObjServices.GetCommunicatonPhone();
            }


            var data = (from source in oTemDataPhoneContact
                        select new
                        {
                            source.DirectoryId,
                            source.DirectoryDetailId,
                            Type = source.DirectoryTypeDesc,
                            source.AreaCode,
                            source.CountryCode,
                            PhoneNumber = Int64.Parse(source.PhoneNumber.GetNumber()),
                            Ext = source.PhoneExt,
                            source.IsPrimary,
                            imgClassIsPrimary = source.IsPrimary ? "primary_chk" : ""
                        }).ToList();


            if (!ObjServices.isNewCase)
            {
                if (!data.Any())
                {
                    if (currentTab == "ClientInfo" || currentTab == "OwnerInfo")
                    {
                        Utility.Tab Tab = (currentTab == "ClientInfo") ? Utility.Tab.ClientInfo : Utility.Tab.OwnerInfo;
                        ObjServices.saveSetValidTab(Tab, false);
                    }
                }
            }

            hdnTotalPhones.Value = data.Count().ToString();
            gvCommonPhone.DataSource = data;
            gvCommonPhone.DataBind();
            gvCommonPhone.Selection.UnselectAll();
            udpPhoneNumber.Update();
        }

        protected override void OnPreRender(EventArgs e)
        {
            Translator("");
        }

        public void Translator(string Lang)
        {
            ltPhoneType.InnerHtml = Resources.PhoneTypeLabel;
            Primary.InnerHtml = Resources.PrimaryLabel;
            btnAdd.Text = Operation == Utility.OperationType.Insert ? Resources.Add : Resources.Edit;
            CountryCode.InnerHtml = Resources.CountryCodeLabel;
            AreaCode.InnerHtml = Resources.AreaCodeLabel;
            PhoneNumber.InnerHtml = Resources.PhoneNumberLabel;
            ltPhoneNumbers.Text = Resources.PhoneNumbersLabel;
            gvCommonPhone.Columns[0].Caption = Resources.TypeLabel.ToUpper();
            gvCommonPhone.Columns[1].Caption = Resources.CountryCodeLabel.ToUpper();
            gvCommonPhone.Columns[2].Caption = Resources.AreaCodeLabel.ToUpper();
            gvCommonPhone.Columns[3].Caption = Resources.PhoneNumberLabel.ToUpper();
            gvCommonPhone.Columns[4].Caption = "EXT";
            gvCommonPhone.Columns[5].Caption = Resources.PrimaryLabel.ToUpper();
            gvCommonPhone.Columns[6].Caption = Resources.Edit.ToUpper();
            gvCommonPhone.Columns[7].Caption = Resources.DeleteLabel.ToUpper();

            if (isChangingLang)
            {
                FillDrop();

                if (ObjServices.ContactEntityID < 0)
                    oTemDataPhoneContact.ForEach(x => x.DirectoryTypeDesc = cbxPhoneType.Items.FindByValue(x.DirectoryTypeId.ToString()).Text);
                FillData();
            }
        }

        public void save()
        {
            Entity.UnderWriting.Entities.Contact.Phone item = null;

            var record = new Entity.UnderWriting.Entities.Contact.Phone();

            if (Operation == Utility.OperationType.Edit)
                record = oTemDataPhoneContact.ElementAt(RowIndex.Value);
            
            if (cbxPhoneType.SelectedValue != "-1" &&
                !string.IsNullOrEmpty(txtCountryCode.Text) &&
                !string.IsNullOrEmpty(txtCityCode.Text) &&
                !string.IsNullOrEmpty(txtPhoneNumber.Text))
            {

                //Agregar un item
                item = new Entity.UnderWriting.Entities.Contact.Phone()
                {
                    //Key
                    CorpId = ObjServices.Corp_Id,
                    DirectoryId = (Operation == Utility.OperationType.Edit) ? record.DirectoryId : -1,
                    DirectoryDetailId = (Operation == Utility.OperationType.Edit) ? record.DirectoryDetailId : -1,
                    CommunicationType = Utility.CommType.Phone.ToString(),
                    ContactId = ObjServices.ContactEntityID.Value,

                    //Campos 
                    DirectoryTypeId = int.Parse(cbxPhoneType.SelectedValue),
                    DirectoryTypeDesc = cbxPhoneType.SelectedItem.Text,
                    CountryCode = txtCountryCode.Text,
                    AreaCode = txtCityCode.Text,
                    PhoneNumber = txtPhoneNumber.Text.Replace("-", ""),
                    PhoneExt = txtExtension.Text,
                    IsPrimary = chkIsPrimary.Checked,

                    //Campo aun Indefinido --Preguntar
                    PersonToContact = null,

                    //Información Usuario
                    CreateUser = (Operation == Utility.OperationType.Insert) ? ObjServices.UserID.Value : record.CreateUser,
                    ModifyUser = (Operation == Utility.OperationType.Edit) ? ObjServices.UserID.Value : record.ModifyUser
                };

                var PhoneNumber = item.CountryCode + item.AreaCode + item.PhoneNumber.Replace("-", "");

                //Si es un nuevo caso  guardar en lista temporal
                if ((ObjServices.isNewCase && !ObjServices.IsDataSearch) || ObjServices.ContactEntityID < 0)
                {
                    if (item.IsPrimary)
                        //Quitar todos los que tienen isprimary de true a false                                                
                        foreach (var vrecord in oTemDataPhoneContact)
                            vrecord.IsPrimary = false;

                    if (Operation == Utility.OperationType.Insert)
                    {
                        if (oTemDataPhoneContact.RecordExistInList(x => x.CountryCode + x.AreaCode + x.PhoneNumber.Replace("-", "") == PhoneNumber))
                        {
                            this.ExcecuteJScript("CustomDialogMessageEx('" + RESOURCE.UnderWriting.NewBussiness.Resources.PhoneAlreadyExist + "',null, null, true, 'Warning')");
                            return;
                        }

                        TempDataContactPhone.Add(item);
                    }
                    else
                        if (Operation == Utility.OperationType.Edit)
                        {

                            List<Entity.UnderWriting.Entities.Contact.Phone> oTemDataPhoneContactEdit;
                            oTemDataPhoneContactEdit = new List<Entity.UnderWriting.Entities.Contact.Phone>(oTemDataPhoneContact);
                            oTemDataPhoneContactEdit.RemoveAt(RowIndex.Value);

                            if (oTemDataPhoneContactEdit.RecordExistInList(x => x.CountryCode + x.AreaCode + x.PhoneNumber.Replace("-", "") == PhoneNumber))
                            {
                                this.ExcecuteJScript("CustomDialogMessageEx('" + RESOURCE.UnderWriting.NewBussiness.Resources.PhoneAlreadyExist + "',null, null, true, 'Warning')");
                                return;
                            }

                            edit(); 
                        }

                    oTemDataPhoneContact = TempDataContactPhone;

                }
                else
                {   //Guardar directamente en la base de datos tanto si es un insert como un edit
                    if (Operation == Utility.OperationType.Insert || Operation == Utility.OperationType.Edit)
                    {
                        if (ObjServices.SetPhoneContact(item) == -2)
                        {
                            this.ExcecuteJScript("CustomDialogMessageEx('" + RESOURCE.UnderWriting.NewBussiness.Resources.PhoneAlreadyExist + "',null, null, true, 'Warning')");
                            return;
                        }
                        //Limpiar Grid
                        oTemDataPhoneContact = null;

                        if (ObjServices.ContactServicesIsActive)
                        {
                            //Invocar el metodo del webservice para guardar en illusdata
                            ObjServices.oContactServicesClient.SetContactPhone(corpId: Utility.Encrypt(ObjServices.Corp_Id.ToString()), contactId: Utility.Encrypt(ObjServices.ContactEntityID.ToString()));
                        }

                        if (Operation == Utility.OperationType.Insert)
                            //ir a la ultima pagina
                            gvCommonPhone.PageIndex = (gvCommonPhone.PageCount - 1);
                    }
                } 
            }
            else if (Operation == Utility.OperationType.InsertAll)
            {
                foreach (var vitem in oTemDataPhoneContact)
                    vitem.ContactId = ObjServices.ContactEntityID.Value;

                ObjServices.SetPhoneContact(oTemDataPhoneContact);
            }

            btnAdd.Text = "Add";
            //Bindear el grid
            FillData();

            //Limpiar los controles a excepcion del grid
            ClearControls(gvCommonPhone);
            Operation = Utility.OperationType.Insert;
        }

        public void edit()
        {
            var record = oTemDataPhoneContact.ElementAt(RowIndex.Value);
            record.DirectoryTypeId = int.Parse(cbxPhoneType.SelectedValue);
            record.DirectoryTypeDesc = cbxPhoneType.SelectedItem.Text;
            record.CountryCode = txtCountryCode.Text;
            record.AreaCode = txtCityCode.Text;
            record.PhoneNumber = txtPhoneNumber.Text.Replace("-", "");
            record.PhoneExt = txtExtension.Text;
            record.IsPrimary = chkIsPrimary.Checked;
        }

        public void FillDrop()
        {
            ObjServices.GettingAllDrops(ref cbxPhoneType,
                                        Utility.DropDownType.PhoneType,
                                        "DirTypeShortDesc",
                                        "DirectoryTypeId",
                                        GenerateItemSelect: true,
                                        corpId: ObjServices.Corp_Id
                                        );

        }

        public void setDataForm(Entity.UnderWriting.Entities.Contact.Phone item)
        {
            cbxPhoneType.SelectedValue = item.DirectoryTypeId.ToString();
            txtCountryCode.Text = item.CountryCode;
            txtCityCode.Text = item.AreaCode;
            txtPhoneNumber.Text = item.PhoneNumber;
            txtExtension.Text = item.PhoneExt;
            chkIsPrimary.Checked = item.IsPrimary;
        }

        public void Initialize(String value = "")
        {
            hdnCurrentSession.Value = String.IsNullOrEmpty(value) ? "" : value;
            Initialize();
        }

        public void Initialize()
        {
            gvCommonPhone.PageIndex = 0;
            oTemDataPhoneContact = new List<Entity.UnderWriting.Entities.Contact.Phone>();
            Operation = Utility.OperationType.Insert;
            ClearData();
            FillDrop();
            FillData();

            if (ObjServices.IsDataReviewMode)
                EnabledControls(!(currentTab == "OwnerInfo" && ObjServices.Contact_Id == ObjServices.Owner_Id));
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            save();
        }

        public void ClearData()
        {
            Session[PrefixSession + "_OperationPhone"] = null;
            oTemDataPhoneContact = null;
            RowIndex = null;
            Operation = Utility.OperationType.Insert;
            hdnTotalPhones.Value = "0";
            ClearControls(this);
        }

        public void ClearData(String value = "")
        {
            PrefixSession = value;
            ClearData();
        }

        public void EnabledControls(bool x)
        {
            EnabledControls(frmPhoneNumbers.Controls, x);
        }

        protected void gvCommonPhone_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            var Commando = e.CommandArgs.CommandName;
            RowIndex = e.VisibleIndex;
            var GridView = (sender as DevExpress.Web.ASPxGridView);
            GridView.Selection.UnselectAll();

            switch (Commando)
            {
                case "Modify":
                    //Editar
                    Operation = Utility.OperationType.Edit;
                    setDataForm(oTemDataPhoneContact.ElementAt(RowIndex.Value));
                    gvCommonPhone.FocusedRowIndex = -1;
                    btnAdd.Text = "Save";
                    break;
                case "Delete":
                    //Eliminar           
                    if ((ObjServices.isNewCase || ObjServices.ContactEntityID < 0) &&
                        !ObjServices.IsDataSearch)
                        oTemDataPhoneContact.RemoveAt(RowIndex.Value);
                    else
                    {
                        var directoryDetailId = int.Parse(gvCommonPhone.GetKeyFromAspxGridView("DirectoryDetailId", RowIndex.Value).ToString());
                        var directoryId = int.Parse(gvCommonPhone.GetKeyFromAspxGridView("DirectoryId", RowIndex.Value).ToString());
                        ObjServices.oContactManager.DeleteCommunicaton(ObjServices.Corp_Id, directoryId, directoryDetailId, ObjServices.UserID.Value);
                    }
                    //Llenar Data
                    FillData();
                    break;
            }
        }

        protected void gvCommonPhone_PageIndexChanged(object sender, EventArgs e)
        {
            FillData();
        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            Utility.ReadOnlyControls(pnForm.Controls, isReadOnly);
        }

        protected void gvCommonPhone_PreRender(object sender, EventArgs e)
        {
            Utility.ReadOnlyControls(gvCommonPhone.Controls, ObjServices.IsReadOnly);
        }
    }

}