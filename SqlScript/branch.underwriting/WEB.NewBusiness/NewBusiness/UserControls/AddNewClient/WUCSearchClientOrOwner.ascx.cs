﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;
using DevExpress.Web;
using System.Reflection;
using RESOURCE.UnderWriting.NewBussiness;

namespace WEB.NewBusiness.NewBusiness.UserControls.AddNewClient.Common
{
    public partial class WUCSearchClientOrOwner : UC, IUC
    {
        public void ReadOnlyControls(bool isReadOnly) { }
        public void save() { }
        public void readOnly(bool x) { }
        public void edit() { }

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

        /// <summary>
        /// Set or get ContactRoleTypeID  Designated Pensioner or Addicional Insured
        /// </summary>
        public bool ISFiltering
        {
            get { return bool.Parse(hdnISFiltering.Value); }
            set { hdnISFiltering.Value = value.ToString(); }
        }

        public bool IsConting
        {
            get { return bool.Parse(HFIsConting.Value); }
            set { HFIsConting.Value = value.ToString(); }
        }

        public bool IsMain
        {
            get { return bool.Parse(HFIsMain.Value); }
            set { HFIsMain.Value = value.ToString(); }
        }

        /// <summary>
        /// Set or get ContactRoleTypeID  Designated Pensioner or Addicional Insured
        /// </summary>
        public int ContactTypeID
        {
            get { return int.Parse(hdnContactTypeId.Value); }
            set { hdnContactTypeId.Value = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ISFiltering = true;
                gvSearch.FocusedRowIndex = -1;
                gvSearch.SetFilterSettings();
            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            Translator("");
        }

        public void Translator(string Lang)
        {
            gvSearch.Columns[0].Caption = Resources.Institution;
            gvSearch.Columns[1].Caption = Resources.FirstNameLabel;
            gvSearch.Columns[2].Caption = Resources.LastNameLabel;
            gvSearch.Columns[3].Caption = Resources.IDNumberLabel;

            var isCompany = ((Utility.ContactTypeId)Utility.getEnumTypeFromValue(typeof(Utility.ContactTypeId), ContactTypeID) == Utility.ContactTypeId.Company);

            if (isCompany)
            {
                gvSearch.Columns[3].Caption = Resources.RegistrationNumberLabel;
                gvSearch.Columns[4].Caption = Resources.RegistrationDateLabel;
                gvSearch.Columns[5].Caption = Resources.CountryLabel;
            }
            else
            {
                gvSearch.Columns[4].Caption = Resources.DateofBirthLabel;
                gvSearch.Columns[5].Caption = Resources.CountryOfResidenceLabel;
                gvSearch.Columns[6].Caption = Resources.LastUpdateLabel;
            }
        }

        public void FillData()
        {
            gvSearch.DataBind();
            gvSearch.FocusedRowIndex = -1;
            gvSearch.PageIndex = 0;
        }

        protected void LinqDS_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            Func<Entity.UnderWriting.Entities.Contact.Search, bool> pwhere = null;

            if (currentTab == "OwnerInfo")
                //Excluir el asegurado
                pwhere = x => x.ContactId != ObjServices.Contact_Id;
            else
                if (currentTab == "PlanPolicy")
                {
                    if (ObjServices.Contact_Id == ObjServices.Owner_Id)
                        pwhere = x => x.ContactId != ObjServices.Contact_Id &&
                                      x.ContactId != ObjServices.Owner_Id &&
                                      x.ContactId != ObjServices.DesignatedPensionerContactId &&
                                      x.ContactId != ObjServices.InsuredAddContactId;
                    else
                        pwhere = x => x.ContactId != ObjServices.Contact_Id &&
                                      x.ContactId != ObjServices.DesignatedPensionerContactId &&
                                      x.ContactId != ObjServices.InsuredAddContactId;

                }
                else
                    if (currentTab == "Beneficiaries")
                        pwhere = x => x.ContactId != ObjServices.Contact_Id &&
                                      x.ContactId != ObjServices.Owner_Id;
                    else
                        pwhere = x => x.ContactId == x.ContactId;


            var contactTypeId = (Utility.ContactTypeId)Utility.getEnumTypeFromValue(typeof(Utility.ContactTypeId), ContactTypeID);

            var Data = ObjServices.GettingContactByAgent(ObjServices.Agent_Id.Value, contactTypeId);

            Data = Data.Where(pwhere).ToList();

            //e.KeyExpression = "ContactId;FirstName;LastName";
            e.KeyExpression = "ContactId;FirstName;LastName,ContactAgentLegalId";
            e.QueryableSource = Data.AsQueryable();
            updSearchClientOrOwner.Update();
            gvSearch.SetFilterSettings();
        }

        protected void gvSearch_FocusedRowChanged(object sender, EventArgs e)
        {
            var SaveCase = false;
            var msjEntity = string.Empty;
            var gv = ((ASPxGridView)sender);

            var bodyContent = this.Page.Master.FindControl("bodyContent");

            if (IsPostBack)
            {
                #region Todo este codigo es para el funcionanmiento de los tab Client Info y Owner Info exclusivamente
                if (currentTab == "ClientInfo" || currentTab == "OwnerInfo")
                {
                    ObjServices.isOwnerContact = (currentTab == "OwnerInfo");

                    var RoleTypeOwner = Utility.getvalueFromEnumType("Owner", typeof(Utility.ContactRoleIDType));
                    var RoleTypeClient = Utility.getvalueFromEnumType("Client", typeof(Utility.ContactRoleIDType));
                    var RoleTypeAgentLegal = Utility.getvalueFromEnumType("Legal", typeof(Utility.ContactRoleIDType));

                    if (gv.FocusedRowIndex > -1 && !ISFiltering)
                    {
                        if (currentTab == "ClientInfo" && ObjServices.isNewCase)
                            ObjServices.Contact_Id = -1;
                        else
                            if (currentTab == "OwnerInfo" && ObjServices.isNewCase)
                                ObjServices.Owner_Id = -1;

                        var ContactID = gv.GetKeyFromAspxGridView("ContactId");

                        if (ContactID != null)
                        {
                            if (currentTab != "OwnerInfo")
                                ObjServices.isNewCase = true;

                            if (!ObjServices.isOwnerContact)
                            {
                                if (ObjServices.isNewCase && ObjServices.Contact_Id.Value > 0)
                                {
                                    //Despegar la compañia  o un contacto cualquiera
                                    ObjServices.oPolicyManager.DeleteContactRole(ObjServices.Corp_Id,
                                                                                 ObjServices.Region_Id,
                                                                                 ObjServices.Country_Id,
                                                                                 ObjServices.Domesticreg_Id,
                                                                                 ObjServices.State_Prov_Id,
                                                                                 ObjServices.City_Id,
                                                                                 ObjServices.Office_Id,
                                                                                 ObjServices.Case_Seq_No,
                                                                                 ObjServices.Hist_Seq_No,
                                                                                 ObjServices.Contact_Id.Value,
                                                                                 RoleTypeClient,
                                                                                 ObjServices.UserID.Value
                                                                                 );
                                    ObjServices.isNewCase = false;
                                    SaveCase = true;
                                    msjEntity = Resources.INSURED.Capitalize();
                                }

                                ObjServices.Contact_Id = int.Parse(ContactID.ToString());
                                ObjServices.ContactEntityID = ObjServices.Contact_Id;
                            }
                            else
                            {

                                if (ObjServices.Owner_Id.Value > 0)
                                {
                                    //Despegar la compañia  o un contacto cualquiera
                                    ObjServices.oPolicyManager.DeleteContactRole(ObjServices.Corp_Id,
                                                                                    ObjServices.Region_Id,
                                                                                    ObjServices.Country_Id,
                                                                                    ObjServices.Domesticreg_Id,
                                                                                    ObjServices.State_Prov_Id,
                                                                                    ObjServices.City_Id,
                                                                                    ObjServices.Office_Id,
                                                                                    ObjServices.Case_Seq_No,
                                                                                    ObjServices.Hist_Seq_No,
                                                                                    ObjServices.Owner_Id.Value,
                                                                                    RoleTypeOwner,
                                                                                    ObjServices.UserID.Value
                                                                                    );

                                    ObjServices.Owner_Id = -1;
                                    ObjServices.ContactEntityID = -1;
                                    SaveCase = true;
                                    msjEntity = Resources.OwnerLabel.Capitalize();
                                }

                                ObjServices.Owner_Id = int.Parse(ContactID.ToString());
                                ObjServices.ContactEntityID = ObjServices.Owner_Id;
                                var AgentContact = gv.GetKeyFromAspxGridView("ContactAgentLegalId");

                                if (ObjServices.Agent_Legal.Value > 0)
                                {
                                    //Despegar la compañia  o un contacto cualquiera
                                    ObjServices.oPolicyManager.DeleteContactRole(ObjServices.Corp_Id,
                                                                                    ObjServices.Region_Id,
                                                                                    ObjServices.Country_Id,
                                                                                    ObjServices.Domesticreg_Id,
                                                                                    ObjServices.State_Prov_Id,
                                                                                    ObjServices.City_Id,
                                                                                    ObjServices.Office_Id,
                                                                                    ObjServices.Case_Seq_No,
                                                                                    ObjServices.Hist_Seq_No,
                                                                                    ObjServices.Agent_Legal.Value,
                                                                                    RoleTypeAgentLegal,
                                                                                    ObjServices.UserID.Value
                                                                                    );

                                    ObjServices.Agent_Legal = -1;
                                    //ObjServices.ContactEntityID = -1;
                                    SaveCase = true;
                                    //msjEntity = Resources.OwnerLabel.Capitalize();
                                }

                                if (AgentContact != null)
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(AgentContact)))
                                    {
                                        ObjServices.Agent_Legal = int.Parse(Convert.ToString(AgentContact));
                                    }
                                }
                            }

                            var ClientInfoContainer = bodyContent.FindControl("ContactsInfoContainer");

                            ObjServices.IsDataSearch = true;

                            ((WEB.NewBusiness.NewBusiness.UserControls.ContactsInfo.ContactsInfoContainer)ClientInfoContainer).Initialize();
                            var txtClientSearch = bodyContent.FindControl("WUCSearchContacts").FindControl("txtClientSearch");
                            var Name = string.Concat(gvSearch.GetKeyFromAspxGridView("FirstName"), " ", gvSearch.GetKeyFromAspxGridView("LastName"));
                            (txtClientSearch as TextBox).Text = Name;
                            (bodyContent.FindControl("WUCSearchContacts").FindControl("chkClientorOwnerAlreadyinContacts") as CheckBox).Checked = true;

                            var chkOwnerIsSameAsInsured = (bodyContent.FindControl("WUCSearchContacts").FindControl("chkOwnerIsSameAsInsured") as CheckBox);
                            var chkIsCompany = (bodyContent.FindControl("WUCSearchContacts").FindControl("chkIsCompany") as CheckBox);

                            chkOwnerIsSameAsInsured.Checked = false;
                            var oddl_P_ANC_Relationship = (bodyContent.FindControl("WUCSearchContacts").FindControl("ddl_P_ANC_Relationship") as DropDownList);
                            oddl_P_ANC_Relationship.Enabled = true;

                            if (SaveCase)
                                //Guardar si ya habia un contact guardado previamente
                                ((WEB.NewBusiness.NewBusiness.UserControls.ContactsInfo.ContactsInfoContainer)ClientInfoContainer).save();

                            this.ExcecuteJScript("if ($('#ddl_P_ANC_Relationship').val()=='-1') {CustomToolTips($('#ddl_P_ANC_Relationship'), '" +
                                @String.Format(RESOURCE.UnderWriting.NewBussiness.Resources.RelationshipToAgent, msjEntity) + "', 'right',5000);}");

                            ClosePopup(bodyContent, gv);
                        }
                    }
                }
                #endregion// Todo este codigo es para el funcionamiento de los tab Client Info y Owner Info exclusivamente

                #region Plan Policy Additional Insured or Designated Pensioner
                else if (currentTab == "PlanPolicy")
                {
                    if (gv.FocusedRowIndex > -1 && !ISFiltering)
                    {
                        var ContactID = gv.GetKeyFromAspxGridView("ContactId").ToInt();

                        #region Planes funeraios
                        var WUCDesignatedPensionerInformation = (!ObjServices.IsDataReviewMode) ?
                        bodyContent.FindControl("PlanPolicyContainer").FindControl("WUCDesignatedPensionerInformation") :
                        bodyContent.FindControl("DReviewContainer").FindControl("PlanPolicyContainer").FindControl("WUCDesignatedPensionerInformation");

                        var objWUCDesignatedPensionerInformation = ((WEB.NewBusiness.NewBusiness.UserControls.PlanPolicy.WUCDesignatedPensionerInformation)WUCDesignatedPensionerInformation);

                        if (IsFuneario())
                        {
                            if (!objWUCDesignatedPensionerInformation.AllowAdd())
                            {
                                this.MessageBox(Resources.AdditionalInsuredValidationFunerario);
                                return;
                            }

                            objWUCDesignatedPensionerInformation.CreateAddionalInsured(ContactID);
                            objWUCDesignatedPensionerInformation.FillData();
                        }
                        #endregion
                        else
                        #region Planes de vida
                        {
                            objWUCDesignatedPensionerInformation.removalAdditional(ContactID);
                            objWUCDesignatedPensionerInformation.Initialize();
                        }
                        #endregion

                        ClosePopup(bodyContent, gv);
                    }
                }
                #endregion

                #region Beneficiarios
                else if (currentTab == "Beneficiaries")
                {
                    if (gv.FocusedRowIndex > -1 && !ISFiltering)
                    {
                        var BeneficiariesContainer = (!ObjServices.IsDataReviewMode) ?
                               bodyContent.FindControl("BeneficiariesContainer") :
                               bodyContent.FindControl("DReviewContainer").FindControl("BeneficiariesContainer");

                        var MainOrAdditional = IsMain ?
                                               BeneficiariesContainer.FindControl("WUCMainInsured") :
                                               BeneficiariesContainer.FindControl("WUCAdditionalInsured");

                        var Beneficiaries = IsConting ?
                                            MainOrAdditional.FindControl("WUCContingentBeneficiaries") :
                                            MainOrAdditional.FindControl("WUCMainBeneficiaries");

                        #region Planes funeraios
                        if (IsFuneario())
                        {
                            var objBeneficiariesContainer = ((WEB.NewBusiness.NewBusiness.UserControls.Beneficiaries.BeneficiariesContainer)BeneficiariesContainer);
                            var ContactID = gv.GetKeyFromAspxGridView("ContactId").ToInt();
                            objBeneficiariesContainer.createBeneficiarie(ContactID);
                            objBeneficiariesContainer.FillData();
                        }
                        #endregion
                        else
                        #region Planes de vida
                        {
                            var objBeneficiaries = ((WEB.NewBusiness.NewBusiness.UserControls.Beneficiaries.WUCBeneficiaries)Beneficiaries);
                            var ContactID = gv.GetKeyFromAspxGridView("ContactId").ToInt();
                            objBeneficiaries.createBeneficiarie(ContactID);
                            objBeneficiaries.FillData(false);
                        }

                        #endregion

                        ClosePopup(bodyContent, gv);
                    }
                }
                #endregion
            }

        }

        private bool IsFuneario()
        {
            var Product = (Utility.ProductBehavior)Utility.getvalueFromEnumType(ObjServices.KeyNameProduct, typeof(Utility.ProductBehavior));

            return (
                      Product == Utility.ProductBehavior.Luminis ||
                      Product == Utility.ProductBehavior.LuminisVIP ||
                      Product == Utility.ProductBehavior.Exequium ||
                      Product == Utility.ProductBehavior.ExequiumVIP
                   );
        }

        private void ClosePopup(Control bodyContent, ASPxGridView gv)
        {
            var hdnShowPopClientInfoSearch = bodyContent.FindControl("hdnShowPopClientInfoSearch");
            (hdnShowPopClientInfoSearch as HiddenField).Value = "false";
            var udpAddNewClient = bodyContent.FindControl("udpAddNewClient");
            (udpAddNewClient as UpdatePanel).Update();
            gv.FocusedRowIndex = -1;
        }

        public void ClearData()
        {
            this.ExcecuteJScript("$('#gvSearch').find('.dxgvFilterBarClearButtonCell_DevEx').find('a:first').click();");
            CleanControls(this);
            gvSearch.DataSource = null;
            gvSearch.DataBind();
        }

        public void Initialize()
        {
            ClearData();
            FillData();

            var isCompany = ((Utility.ContactTypeId)Utility.getEnumTypeFromValue(typeof(Utility.ContactTypeId), ContactTypeID) == Utility.ContactTypeId.Company);

            gvSearch.Columns[0].Visible = isCompany;
            gvSearch.Columns[1].Visible = !isCompany;
            gvSearch.Columns[2].Visible = !isCompany;

            if (isCompany)
            {
                gvSearch.Columns[3].Caption = Resources.RegistrationNumberLabel;
                gvSearch.Columns[4].Caption = Resources.RegistrationDateLabel;
                gvSearch.Columns[5].Caption = Resources.CountryLabel;
            }

        }

        protected void gvSearch_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            gvSearch.FocusedRowIndex = -1;
        }

        protected void gvSearch_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            ISFiltering = true;
        }

        protected void gvSearch_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "APPLYFILTER" || e.CallbackName == "APPLYCOLUMNFILTER" || e.CallbackName == "APPLYHEADERCOLUMNFILTER")
            {
                ISFiltering = true;
                gvSearch.FocusedRowIndex = -1;
                gvSearch.SetFilterSettings();
            }
        }
    }
}