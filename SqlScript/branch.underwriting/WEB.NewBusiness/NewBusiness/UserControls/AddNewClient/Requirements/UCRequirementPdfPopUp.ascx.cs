﻿using AjaxControlToolkit;
using DI.UnderWriting;
using DI.UnderWriting.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;
using WEB.NewBusiness.NewBusiness.UserControls.Requirements;
using RESOURCE.UnderWriting.NewBussiness;

namespace WEB.NewBusiness.NewBusiness.UserControls.AddNewClient.Requirements
{
    public partial class UCRequirementPdfPopUp : UC
    {
        IRequirement RequirementManager
        {
            get { return diManager.RequirementManager; }
        }
        private string TempFilePath
        {
            get { return Server.MapPath("~/TempFiles"); }
        }

        public void clearData() { }
        public void readOnly(bool x) { }
        public void edit() { }

        UnderWritingDIManager diManager = new UnderWritingDIManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.pdfViewerPdfPopUp.LicenseKey = System.Configuration.ConfigurationManager.AppSettings["PDFViewer"].ToString();
            this.pdfViewerPdfPopUp.PdfSourceBytes = null;
            this.pdfViewerPdfPopUp.DataBind();
        }

        public void FillData()
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Translator(string.Empty);
        }

        public void Translator(string Lang)
        {
            fuRequirementFile.BrowseButton.Text = Resources.Browse.Capitalize();
            uploadDocument.InnerHtml = Resources.uploadDocument.ToUpper();
            SaveBtn.Text = Resources.Save;
            CancelBtn.Text = Resources.Cancel;
        }

        public void fuRequirementFile_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            var message = "";
            string pdfPath = string.Empty;

            try
            {
                var file = e.UploadedFile;
                if (file.IsValid)
                {
                    var fileName = WEB.NewBusiness.Common.Utility.GetSerialId() + "~~" + file.FileName;
                    var savePath = TempFilePath + "\\" + fileName;
                    file.SaveAs(savePath);
                    message = savePath;
                }
                else
                {
                    message = String.Format("{{ \"file\": \"{0}\", \"error\": \"{1}\"}}", "", "Error");
                }
            }
            catch (Exception ex)
            {
                message = String.Format("{{ \"file\": \"{0}\", \"error\": \"{1}\"}}", "", ex.Message);
            }
            txtPath.Text = message;
            pdfPath = message;
            Session["pdfPath"] = message;
            e.CallbackData = message;
        }

        protected void btnRequirementPreviewPDF_Click(object sender, EventArgs e)
        {
            var Path = Session["pdfPath"].ToString();

            if (File.Exists(Path))
            {
                byte[] fileBytes = File.ReadAllBytes(Path);

                this.pdfViewerPdfPopUp.PdfSourceBytes = fileBytes;
                this.pdfViewerPdfPopUp.DataBind();
                ModalPopupExtender ext = (ModalPopupExtender)this.Parent.FindControl("ModalPopupExtender1");
                if (ext != null)
                {
                    this.SaveBtn.Enabled = true;
                    ext.Show();
                }
            }
        }

        public void savePdf(object sender, EventArgs e)
        {
            if (this.fuRequirementFile.UploadedFiles.Count() > 0)
            {
                string Values = ((HiddenField)this.Parent.FindControl("RequirementsValues")).Value;
                var parser = new JavaScriptSerializer();
                WEB.NewBusiness.Common.Utility.RequirementsValues requirements = parser.Deserialize<WEB.NewBusiness.Common.Utility.RequirementsValues>(Values.ToString().
                        Replace("/", ""));

                if (Values.Length > 0 && Values != "{}")
                {
                    if (Session["pdfPath"] != null)
                    {
                        var Path = Session["pdfPath"].ToString();

                        if (requirements.ID == -1)
                        {
                            requirements.ID = RequirementManager.Insert(new Entity.UnderWriting.Entities.Requirement()
                            {
                                CorpId = this.ObjServices.Corp_Id,
                                RegionId = this.ObjServices.Region_Id,
                                CountryId = this.ObjServices.Country_Id,
                                DomesticregId = this.ObjServices.Domesticreg_Id,
                                StateProvId = this.ObjServices.State_Prov_Id,
                                CityId = this.ObjServices.City_Id,
                                OfficeId = this.ObjServices.Office_Id,
                                CaseSeqNo = this.ObjServices.Case_Seq_No,
                                HistSeqNo = this.ObjServices.Hist_Seq_No,
                                ContactId = requirements.ContactID,
                                RequirementCatId = requirements.CategoryID,
                                RequirementTypeId = requirements.TypeID,
                                RequirementId = requirements.ID,
                                RequestedBy = 1,
                                ReceivedDate = DateTime.Now,
                                RequestedDate = DateTime.Now,
                                IsManual = false,
                                SendToReinsurance = false,
                                Comment = null,
                                UserId = ObjServices.UserID.Value
                            });
                        }

                        RequirementManager.InsertDocument(new Entity.UnderWriting.Entities.Requirement.Document
                        {
                            CorpId = this.ObjServices.Corp_Id,
                            RegionId = this.ObjServices.Region_Id,
                            CountryId = this.ObjServices.Country_Id,
                            DomesticregId = this.ObjServices.Domesticreg_Id,
                            StateProvId = this.ObjServices.State_Prov_Id,
                            CityId = this.ObjServices.City_Id,
                            OfficeId = this.ObjServices.Office_Id,
                            CaseSeqNo = this.ObjServices.Case_Seq_No,
                            HistSeqNo = this.ObjServices.Hist_Seq_No,
                            ContactId = requirements.ContactID,
                            RequirementCatId = requirements.CategoryID,
                            RequirementTypeId = requirements.TypeID,
                            RequirementId = requirements.ID,
                            DocumentStatusId = 1, //Accepted
                            DocumentBinary = File.ReadAllBytes(Path),
                            DocumentName = Path.Split('~')[2].ToString(),
                            UserId = ObjServices.UserID.Value
                        });

                        try
                        {
                          string TemplateIndexFile = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LifeOnBaseTemplatePath"]);
                          string documentType = "R";

                          Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation Add = new Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation() {
                              Contact_ID = requirements.ContactID
                          };

                          ObjServices.SendFileToOnBase(Add,
                                                       TemplateIndexFile, 
                                                       documentType,
                                                       requirements.CategoryID,
                                                       requirements.TypeID,
                                                       Path);
                        }
                        catch (Exception ex)
                        {
                            ObjServices.oPolicyManager.InsertLog(new Entity.UnderWriting.Entities.Policy.LogParameter
                             {
                                 LogTypeId = WEB.NewBusiness.Common.Utility.LogTypeId.Exception.ToInt(),
                                 CorpId = ObjServices.Corp_Id,
                                 CompanyId = ObjServices.CompanyId,
                                 ProjectId = ObjServices.ProjectId,
                                 Identifier = Guid.NewGuid(),
                                 LogValue = "Se encontro un problema con el proceso OnBaseTranfer, Detalle: " + ex.Message.ToString()
                             });
                        }

                        Session["pdfPath"] = null;

                        File.Delete(Path);
                    }
                    else
                    {
                        //"Please browser for a file before saving."
                        this.MessageBox(RESOURCE.UnderWriting.NewBussiness.Resources.BrowseFile);
                    }
                }

                var container = (RequirementsContainer)this.Parent.Parent.Parent.Parent.Parent;//Bmarroquin 10-04-2017 agrego un .parent dado que incoporte un control padre (el panel)

                container.FillData();

                ObjServices.SetValidTabRequirementForNewBusiness();

                ClosePopUp();
            }
        }

        private void ClosePopUp() { }
    }
}