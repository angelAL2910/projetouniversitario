﻿using DI.UnderWriting;
using DI.UnderWriting.Interfaces;
using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;
using WEB.NewBusiness.NewBusiness.UserControls.AddNewClient.Requirements;

namespace WEB.NewBusiness.NewBusiness.UserControls.Requirements
{
    public partial class UCRequirements : UC, IUC
    {
        public void Initialize() { }
        public void ClearData() { }
        protected void btnUpload_Click(object sender, EventArgs e) { }
        public void save() { }
        public void readOnly(bool x) { }
        public void edit() { }
        public void FillData() { }

        public class item
        {
            public string Requirement { get; set; }
            public string LastUpdate { get; set; }
        }

        public IEnumerable<Entity.UnderWriting.Entities.Requirement> RequirementList { get; set; }

        public string Category { get; set; }
        public IEnumerable<Entity.UnderWriting.Entities.Requirement> DataSource { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ObjServices.IsDataReviewMode || ObjServices.IsReadOnly)
            {
                GridViewRequirements.Columns[3].SetColVisible(false);
                GridViewRequirements.Columns[5].SetColVisible(false);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Translator("");
        }

        public void Translator(string Lang)
        {
            GridViewRequirements.Columns[0].Caption = Resources.RequirementsLabel.ToUpper();
            GridViewRequirements.Columns[1].Caption = Resources.LastUpdateLabel.ToUpper();
            GridViewRequirements.Columns[2].Caption = Resources.Role.ToUpper();
            GridViewRequirements.Columns[3].Caption = Resources.UPLOAD.ToUpper();
            GridViewRequirements.Columns[4].Caption = Resources.View.ToUpper();
            GridViewRequirements.Columns[5].Caption = Resources.DeleteLabel.ToUpper();
        }

        public void FillData(string RequirementCatId, bool RequimentISForPolicyOnly = false)
        {
            var CorpId = ObjServices.Corp_Id;
            var RegionId = ObjServices.Region_Id;
            var CountryId = ObjServices.Country_Id;
            var DomesticregId = ObjServices.Domesticreg_Id;
            var StateProvId = ObjServices.State_Prov_Id;
            var CityId = ObjServices.City_Id;
            var OfficeId = ObjServices.Office_Id;
            var CaseSeqNo = ObjServices.Case_Seq_No;
            var HistSeqNo = ObjServices.Hist_Seq_No;
            int RequirementCatId2 = 0;
            var RequirementCatIdTemp = new Nullable<int>();
            var ContactIdTemp = new Nullable<int>();

            if (RequirementCatId.Split('|').Length > 1)
            {
                if (RequimentISForPolicyOnly == false)
                {
                    RequirementCatIdTemp = int.Parse(RequirementCatId.Split('|')[0]);
                    RequirementCatId2 = int.Parse(RequirementCatId.Split('|')[1]);
                }
            }
            else
                if (RequimentISForPolicyOnly == false)
                {
                    RequirementCatIdTemp = int.Parse(RequirementCatId);
                }

            if (RequirementCatId2 == 0)
            {
                var reqs = ObjServices.oRequirementManager.GetAllNewBusiness(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId,
                    CaseSeqNo, HistSeqNo, ContactIdTemp, RequirementCatIdTemp, languageId: ObjServices.Language.ToInt())
                    .Select(r => new
                    {
                        Requirement = r.RequirementTypeDesc,
                        LastUpdate = r.LastUpdate.HasValue ? r.LastUpdate.Value.ToShortDateString() : r.RequestedDate.ToShortDateString(),
                        ID = r.RequirementId,
                        CategoryID = r.RequirementCatId,
                        TypeId = r.RequirementTypeId,
                        ContactID = r.ContactId,
                        HasDocument = r.HasDocument,
                        DocId = r.RequirementDocId,
                        ContactRoleDesc = r.ContactRoleDesc,
                        Is_Mandatory = r.Is_Mandatory
                    });
                //no se de donde estamos sacando lastchange, no esta en el procedure.

                this.GridViewRequirements.DataSource = reqs;
                this.GridViewRequirements.DataBind();
            }
            else
            {
                var reqs = ObjServices.oRequirementManager.GetAllNewBusiness(CorpId, RegionId, CountryId, DomesticregId, StateProvId, CityId, OfficeId,
                       CaseSeqNo, HistSeqNo, ContactIdTemp, new Nullable<int>(), languageId: ObjServices.Language.ToInt()).Where(re => re.RequirementCatId == RequirementCatIdTemp || re.RequirementCatId == RequirementCatId2)
            .Select(r => new
            {
                Requirement = r.RequirementTypeDesc,
                LastUpdate = r.LastUpdate.HasValue ? r.LastUpdate.Value.ToShortDateString() : r.RequestedDate.ToShortDateString(),
                ID = r.RequirementId,
                CategoryID = r.RequirementCatId,
                TypeId = r.RequirementTypeId,
                ContactID = r.ContactId,
                HasDocument = r.HasDocument,
                DocId = r.RequirementDocId,
                ContactRoleDesc = r.ContactRoleDesc,
                Is_Mandatory = r.Is_Mandatory
            });
                //no se de donde estamos sacando lastchange, no esta en el procedure.
                this.GridViewRequirements.DataSource = reqs;
                this.GridViewRequirements.DataBind();
            }
        }

        protected void GridViewRequirements_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {   //Comando del grid
            var comando = e.CommandArgs.CommandName;
            var Grid = (sender as DevExpress.Web.ASPxGridView);
            var RowIndex = e.VisibleIndex;
            var ContactID = int.Parse(Grid.GetKeyFromAspxGridView("ContactID", RowIndex).ToString());
            var CategoryID = int.Parse(Grid.GetKeyFromAspxGridView("CategoryID", RowIndex).ToString());
            var TypeID = int.Parse(Grid.GetKeyFromAspxGridView("TypeId", RowIndex).ToString());
            var ID = int.Parse(Grid.GetKeyFromAspxGridView("ID", RowIndex).ToString());
            var DocId = int.Parse(Grid.GetKeyFromAspxGridView("DocId", RowIndex).ToString());
            var hasdocument = bool.Parse(Grid.GetKeyFromAspxGridView("HasDocument", RowIndex).ToString());

            switch (comando)
            {
                case "view":
                    if (hasdocument)
                    {
                        string documentType = "R";

                        Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation add = new Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation()
                        {
                            Contact_ID = ContactID
                        };

                        byte[] pdfOnBase = ObjServices.ViewFileFromOnBase(add, documentType, CategoryID, TypeID);

                        if (pdfOnBase == null)
                        {
                            var document = ObjServices.oRequirementManager.GetDocument(
                                   ObjServices.Corp_Id,
                                   ObjServices.Region_Id,
                                   ObjServices.Country_Id,
                                   ObjServices.Domesticreg_Id,
                                   ObjServices.State_Prov_Id,
                                   ObjServices.City_Id,
                                   ObjServices.Office_Id,
                                   ObjServices.Case_Seq_No,
                                   ObjServices.Hist_Seq_No,
                                   ContactID,
                                   CategoryID,
                                   TypeID,
                                   ID,
                                   DocId);

                            if (!document.isNullReferenceObject() && !document.DocumentBinary.isNullReferenceObject())
                            {
                                ViewPDF(document.DocumentBinary);
                            }
                            else
                            {
                                this.MessageBox(Resources.ViewRequirement);
                            }
                        }
                        else
                        {
                            ViewPDF(pdfOnBase);
                        }
                    }
                    else
                    {
                        this.MessageBox("Todavia no ha subido documento.");
                    }
                    break;
                case "delete":
                    if (ID > 0 && DocId > 0)
                    {

                        ObjServices.oRequirementManager.DeleteDocument(new Entity.UnderWriting.Entities.Requirement.Document()
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
                            ContactId = ContactID,
                            RequirementCatId = CategoryID,
                            RequirementTypeId = TypeID,
                            RequirementId = ID,
                            RequirementDocId = DocId,
                            UserId = ObjServices.UserID.Value
                        });
                    }
                    else
                        this.MessageBox(Resources.DeleteDocument);

                    break;
            }

            var oRequirementsContainer = this.Page.Master.FindControl("bodyContent").FindControl("RequirementsContainer");
            if (!oRequirementsContainer.isNullReferenceControl())
                ((WEB.NewBusiness.NewBusiness.UserControls.Requirements.RequirementsContainer)oRequirementsContainer).FillData();
        }


        public void ViewPDF(byte[] pdffile)
        {

            var UCShowPDFPopup1 = (NewBusiness.UserControls.Common.WUCShowPDFPopup)this.Page.Master.FindControl("WUCShowPDFPopup1");
            var ModalPopUp = (AjaxControlToolkit.ModalPopupExtender)this.Page.Master.FindControl("ModalPopupPDFViewer");
            //var PdfTitle = (Literal)UCShowPDFPopup1.FindControl("ltTypeDoc");
            UCShowPDFPopup1.LoadPDFPreview(pdffile);
            //PdfTitle.Text = RESOURCE.UnderWriting.NewBussiness.Resources.DocumentView;
            ModalPopUp.Show();
            this.ExcecuteJScript("$('#pnModalPopupPDFViewer').find('div:first').prepend(CreateNewPopFrame());");

        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            throw new NotImplementedException();
        }
    }
}