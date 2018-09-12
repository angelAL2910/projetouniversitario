﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Entity.UnderWriting.Entities;
using RESOURCE.UnderWriting.NewBussiness;
using WEB.NewBusiness.Common;
using WEB.NewBusiness.Common.Illustration.IllustrationVehicle.Models;
using Statetrust.Framework.Security.Bll;
using System.Web;
using DevExpress.Web;
using System.Configuration;
using System.Resources;
using System.Globalization;
using WEB.NewBusiness.TransunionServiceReference;
using System.Web.UI;
using System.Text;
using System.Net;

namespace WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle
{
    public partial class UCDocuments : UC, IUC
    {
        private int RequiredFileSize
        {
            get
            {
                return ConfigurationManager.AppSettings["RequiredFileSize"].ToInt();
            }
        }

        private Utility.Tabs TabSelected
        {
            get { return (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), ObjServices.hdnQuotationTabs); }
        }

        public delegate void ExportToPDFHandler(byte[] byteArray, string FileName);

        public event ExportToPDFHandler ExportFile;

        private static string StatusChangedSuccessfully = string.Empty,
                              Success = string.Empty,
                              AnErrorOccuredChangingStatus = string.Empty;

        bool StatusCompletedChange
        {
            get
            {
                return ViewState["StatusCompletedChange"].ToBoolean();
            }
            set
            {
                ViewState["StatusCompletedChange"] = value;
            }
        }

        int _SelectedRequirementTypeId
        {
            get
            {
                return ViewState["SelectedRequirementTypeId"].ToInt();
            }
            set { ViewState["SelectedRequirementTypeId"] = value.ToString(); }
        }

        int _SelectedRequirementId
        {
            get { return ViewState["SelectedRequierementId"].ToInt(); }
            set { ViewState["SelectedRequierementId"] = value.ToString(); }
        }

        int _SelectedFunctionalitySeqNo
        {
            get { return ViewState["SelectedFunctionalitySeqNo"].ToInt(); }
            set { ViewState["SelectedFunctionalitySeqNo"] = value.ToString(); }
        }

        byte[] _SelectedByteArrayFile
        {
            get { return (byte[])ViewState["_SelectedByteArrayFile"]; }
            set { ViewState["_SelectedByteArrayFile"] = value; }
        }

        string _SelectedFileName
        {
            get { return ViewState["_SelectedFileName"].ToString(); }
            set { ViewState["_SelectedFileName"] = value; }
        }

        int _SelectedFunctionalityId
        {
            get { return ViewState["SelectedFunctionalityId"].ToInt(); }
            set { ViewState["SelectedFunctionalityId"] = value.ToString(); }
        }

        int _SelectedInsuredVehicleId
        {
            get { return ViewState["SelectedInsuredVehicleId"].ToInt(); }
            set { ViewState["SelectedInsuredVehicleId"] = value.ToString(); }
        }

        string _SelectedRequimentOnBaseNameKey
        {
            get
            {
                return ViewState["SelectedRequimentOnBaseNameKey"].ToString();
            }
            set
            {
                ViewState["SelectedRequimentOnBaseNameKey"] = value;
            }
        }

        bool _isReadOnly
        {
            get { return ViewState["isReadOnly"].ToBoolean(); }
            set { ViewState["isReadOnly"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Translator(string.Empty);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ObjServices.DeclineCase += DeclineCase;

            pdfViewerMyPreviewPDF.LicenseKey = System.Configuration.ConfigurationManager.AppSettings["PDFViewer"];

            if (hdnShowUploadFile.Value == "true")
            {
                mpeUploadFile.Show();
                updUploadFile.Update();
            }
        }

        public void Translator(string Lang)
        {
            StatusChangedSuccessfully = Resources.StatusChangedSuccessfully;
            Success = Resources.Success;
            AnErrorOccuredChangingStatus = Resources.AErrorOccuredChangingStatus;
        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        private void generatePDFDoc(Tuple<byte[], string> fileInf, int? DocId = null, int RequirementId = -1)
        {
            try
            {
                var pRequirementCatId = 6;
                var pRequirementTypeId = 49;

                if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                {
                    //Incendio y lineas aliadas

                    switch (ObjServices.AlliedLinesProductBehavior)
                    {
                        case Utility.AlliedLinesType.Property:
                            pRequirementCatId = 7;
                            pRequirementTypeId = 10;
                            break;
                        case Utility.AlliedLinesType.Navy:
                            pRequirementCatId = 8;
                            pRequirementTypeId = 10;
                            break;
                        case Utility.AlliedLinesType.Transport:
                            pRequirementCatId = 9;
                            pRequirementTypeId = 10;
                            break;
                        case Utility.AlliedLinesType.Airplane:
                            pRequirementCatId = 10;
                            pRequirementTypeId = 10;
                            break;
                        case Utility.AlliedLinesType.Bail:
                            pRequirementCatId = 11;
                            pRequirementTypeId = 10;
                            break;
                    }
                }

                var pRequirementId = RequirementId;

                byte[] fileBytes = fileInf.Item1;

                if (pRequirementId <= 0)
                {
                    pRequirementId = 1;

                    var parameter = new Requirement()
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
                        ContactId = ObjServices.ContactEntityID.GetValueOrDefault(),
                        RequirementCatId = pRequirementCatId,
                        RequirementTypeId = pRequirementTypeId,
                        RequirementId = pRequirementId,
                        RequestedBy = 1,
                        ReceivedDate = DateTime.Now,
                        RequestedDate = DateTime.Now,
                        IsManual = false,
                        SendToReinsurance = false,
                        Comment = null,
                        UserId = ObjServices.UserID.GetValueOrDefault()
                    };

                    ObjServices.oRequirementManager.Update(parameter);
                }

                ObjServices.oRequirementManager.InsertDocument(new Requirement.Document
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
                    ContactId = ObjServices.ContactEntityID.GetValueOrDefault(),
                    RequirementCatId = pRequirementCatId,
                    RequirementTypeId = pRequirementTypeId,
                    RequirementId = pRequirementId,
                    DocumentStatusId = 1, //Accepted                  
                    DocumentBinary = fileBytes,
                    DocumentName = Path.GetFileName(fileInf.Item2),
                    UserId = ObjServices.UserID.GetValueOrDefault()
                });
            }
            catch (Exception ex)
            {
                var msg = ex.GetLastInnerException().Message;
                this.MessageBox(msg.RemoveInvalidCharacters().Replace('\'', '\"'), Title: "Error", Width: 800);
            }
        }

        public void save(string path)
        {
            try
            {
                var pRequirementCatId = 6; // Vehiculo

                if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                {
                    //Incendio y lineas aliadas  
                    switch (ObjServices.AlliedLinesProductBehavior)
                    {
                        case Utility.AlliedLinesType.Property:
                            pRequirementCatId = 7;
                            break;
                        case Utility.AlliedLinesType.Navy:
                            pRequirementCatId = 8;
                            break;
                        case Utility.AlliedLinesType.Transport:
                            pRequirementCatId = 9;
                            break;
                        case Utility.AlliedLinesType.Airplane:
                            pRequirementCatId = 10;
                            break;
                        case Utility.AlliedLinesType.Bail:
                            pRequirementCatId = 11;
                            break;
                    }
                }

                var pRequirementTypeId = _SelectedRequirementTypeId;
                var pRequirementId = _SelectedRequirementId;

                if (File.Exists(path))
                {
                    byte[] fileBytes = File.ReadAllBytes(path);

                    if (fileBytes.Length > RequiredFileSize)
                    {
                        this.MessageBox(Resources.FileTooLarge, Title: "Error", Width: 800);
                        return;
                    }

                    if (pRequirementId <= 0)
                    {

                        pRequirementId = _SelectedInsuredVehicleId <= 0 ? 1 : _SelectedInsuredVehicleId;

                        var parameter = new Requirement()
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
                            ContactId = ObjServices.ContactEntityID.GetValueOrDefault(),
                            RequirementCatId = pRequirementCatId,
                            RequirementTypeId = pRequirementTypeId,
                            RequirementId = pRequirementId,
                            RequestedBy = 1,
                            ReceivedDate = DateTime.Now,
                            RequestedDate = DateTime.Now,
                            IsManual = false,
                            SendToReinsurance = false,
                            Comment = null,
                            UserId = ObjServices.UserID.GetValueOrDefault()
                        };

                        ObjServices.oRequirementManager.Update(parameter);
                    }

                    byte[] DocumentBinary = Utility.CompressPDF(fileBytes);

                    ObjServices.oRequirementManager.InsertDocument(new Requirement.Document
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
                        ContactId = ObjServices.ContactEntityID.GetValueOrDefault(),
                        RequirementCatId = pRequirementCatId,
                        RequirementTypeId = pRequirementTypeId,
                        RequirementId = pRequirementId,
                        DocumentStatusId = 1, //Accepted
                        DocumentBinary = DocumentBinary,
                        DocumentName = Path.GetFileName(path),
                        UserId = ObjServices.UserID.GetValueOrDefault()
                    });
                }

                if (File.Exists(path))
                    File.Delete(path);

                FillData();
                hdnShowUploadFile.Value = "false";
                Session["pdfUploadedPath"] = null;
                Session["RequirementPDF"] = null;
                this.ExcecuteJScript("CloseFileUpload();");
            }
            catch (Exception ex)
            {
                var msg = ex.GetLastInnerException().Message;
                this.MessageBox(msg.RemoveInvalidCharacters().Replace('\'', '\"'), Title: "Error", Width: 800);
            }
        }

        public void edit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Actualizar la vista de los documentos requeridos para excluir documentos segun sea el caso
        /// </summary>
        /// <param name="data"></param>
        private void UpdateDataRequirement(ref IEnumerable<Policy.Vehicle.Requirement> data)
        {
            var result = data.Where(b => (
                                          b.RequirementTypeSubType == Utility.RequirementSubType.VehicleIdentification.ToString() ||
                                          b.RequirementTypeSubType == Utility.RequirementSubType.PersonalIdentification.ToString()
                                         ) && b.IsMandatory.GetValueOrDefault()).OrderByDescending(x => x.RequirementTypeSubType);

            foreach (var item in result)
            {
                var OrderId = item.OrderId;

                if (item.RequirementTypeSubType == Utility.RequirementSubType.VehicleIdentification.ToString())
                {
                    #region Actualizar los vehicle Identification

                    var Prefijo = item.RequirementTypeDesc.Split('-')[0];
                    var VehicleUniqueId = item.VehicleUniqueId;


                    var Requirement = result.Where(u => u.RequirementTypeDesc.Contains(Prefijo) &&
                                                        u.VehicleUniqueId == VehicleUniqueId);

                    var Record = Requirement.FirstOrDefault(h => h.OrderId != item.OrderId);

                    if (Record != null)
                    {
                        if (!Record.DocumentId.HasValue || !item.DocumentId.HasValue)
                            if (Record.DocumentId.HasValue)
                                result.FirstOrDefault(h => h.OrderId == item.OrderId).IsMandatory = false;
                    }

                    #endregion
                }

                if (item.RequirementTypeSubType == Utility.RequirementSubType.PersonalIdentification.ToString())
                {
                    #region Actualizar los Personal Identification
                    if (item.DocumentId.HasValue)
                    {
                        var records = result.Where(k => k.RequirementTypeSubType == Utility.RequirementSubType.PersonalIdentification.ToString() &&
                                                        k.OrderId != OrderId);

                        foreach (var itemR in records)
                            if (!itemR.DocumentId.HasValue)
                                result.FirstOrDefault(h => h.OrderId == itemR.OrderId).IsMandatory = false;
                    }

                    #endregion
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void UpdateDataRequirementVehicle(ref IEnumerable<Requirement.Product> data)
        {
            var result = data.Where(b => (
                                          b.RequirementTypeSubType == Utility.RequirementSubType.VehicleIdentification.ToString() ||
                                          b.RequirementTypeSubType == Utility.RequirementSubType.PersonalIdentification.ToString() ||
                                          b.RequirementTypeSubType == Utility.RequirementSubType.CompanyInformation1.ToString() ||
                                          b.RequirementTypeSubType == Utility.RequirementSubType.CompanyInformation2.ToString() ||
                                          b.RequirementTypeSubType == Utility.RequirementSubType.CompanyInformation3.ToString()
                                         ) && b.IsMandatory.GetValueOrDefault()).OrderByDescending(x => x.RequirementTypeSubType);

            foreach (var item in result)
            {
                var OrderId = item.OrderId;

                if (item.RequirementTypeSubType == Utility.RequirementSubType.VehicleIdentification.ToString())
                {
                    #region Actualizar los vehicle Identification

                    var Prefijo = item.RequirementTypeDesc.Split('-')[0];
                    var VehicleUniqueId = item.UniqueId;


                    var Requirement = result.Where(u => u.RequirementTypeDesc.Contains(Prefijo) &&
                                                        u.UniqueId == VehicleUniqueId);

                    var Record = Requirement.FirstOrDefault(h => h.OrderId != item.OrderId);

                    if (Record != null)
                    {
                        if (!Record.DocumentId.HasValue || !item.DocumentId.HasValue)
                            if (Record.DocumentId.HasValue)
                                result.FirstOrDefault(h => h.OrderId == item.OrderId).IsMandatory = false;
                    }

                    #endregion
                }

                if (item.RequirementTypeSubType == Utility.RequirementSubType.PersonalIdentification.ToString())
                {
                    #region Actualizar los Personal Identification
                    if (item.DocumentId.HasValue)
                    {
                        var records = result.Where(k => k.RequirementTypeSubType == Utility.RequirementSubType.PersonalIdentification.ToString() &&
                                                        k.OrderId != OrderId);

                        foreach (var itemR in records)
                            if (!itemR.DocumentId.HasValue)
                                result.FirstOrDefault(h => h.OrderId == itemR.OrderId).IsMandatory = false;
                    }

                    #endregion
                }


                if (item.RequirementTypeSubType == Utility.RequirementSubType.CompanyInformation1.ToString())
                {
                    if (item.DocumentId.HasValue)
                    {
                        var records = result.Where(k => k.RequirementTypeSubType == Utility.RequirementSubType.CompanyInformation1.ToString() &&
                                                        k.OrderId != OrderId);

                        foreach (var itemR in records)
                            if (!itemR.DocumentId.HasValue)
                                result.FirstOrDefault(h => h.OrderId == itemR.OrderId).IsMandatory = false;
                    }
                }

                if (item.RequirementTypeSubType == Utility.RequirementSubType.CompanyInformation2.ToString())
                {
                    if (item.DocumentId.HasValue)
                    {
                        var records = result.Where(k => k.RequirementTypeSubType == Utility.RequirementSubType.CompanyInformation2.ToString() &&
                                                        k.OrderId != OrderId);

                        foreach (var itemR in records)
                            if (!itemR.DocumentId.HasValue)
                                result.FirstOrDefault(h => h.OrderId == itemR.OrderId).IsMandatory = false;
                    }
                }

                if (item.RequirementTypeSubType == Utility.RequirementSubType.CompanyInformation3.ToString())
                {
                    if (item.DocumentId.HasValue)
                    {
                        var records = result.Where(k => k.RequirementTypeSubType == Utility.RequirementSubType.CompanyInformation3.ToString() &&
                                                        k.OrderId != OrderId);

                        foreach (var itemR in records)
                            if (!itemR.DocumentId.HasValue)
                                result.FirstOrDefault(h => h.OrderId == itemR.OrderId).IsMandatory = false;
                    }
                }
            }

        }

        private void UpdateDataRequirement_IL(ref IEnumerable<Requirement.Product> data)
        {
            var result = data.Where(b => (
                                          b.RequirementTypeSubType == Utility.RequirementSubType.VehicleIdentification.ToString() ||
                                          b.RequirementTypeSubType == Utility.RequirementSubType.PersonalIdentification.ToString()
                                         ) && b.IsMandatory.GetValueOrDefault()).OrderByDescending(x => x.RequirementTypeSubType);

            foreach (var item in result)
            {
                var OrderId = item.OrderId;

                if (item.RequirementTypeSubType == Utility.RequirementSubType.VehicleIdentification.ToString())
                {
                    #region Actualizar los vehicle Identification

                    var Prefijo = item.RequirementTypeDesc.Split('-')[0];
                    var VehicleUniqueId = item.UniqueId;


                    var Requirement = result.Where(u => u.RequirementTypeDesc.Contains(Prefijo) &&
                                                        u.UniqueId == VehicleUniqueId);

                    var Record = Requirement.FirstOrDefault(h => h.OrderId != item.OrderId);

                    if (Record != null)
                    {
                        if (!Record.DocumentId.HasValue || !item.DocumentId.HasValue)
                            if (Record.DocumentId.HasValue)
                                result.FirstOrDefault(h => h.OrderId == item.OrderId).IsMandatory = false;
                    }

                    #endregion
                }

                if (item.RequirementTypeSubType == Utility.RequirementSubType.PersonalIdentification.ToString())
                {
                    #region Actualizar los Personal Identification
                    if (item.DocumentId.HasValue)
                    {
                        var records = result.Where(k => k.RequirementTypeSubType == Utility.RequirementSubType.PersonalIdentification.ToString() &&
                                                        k.OrderId != OrderId);

                        foreach (var itemR in records)
                        {
                            if (!itemR.DocumentId.HasValue)
                            {
                                result.FirstOrDefault(h => h.OrderId == itemR.OrderId).IsMandatory = false;
                            }
                        }
                    }

                    #endregion
                }
            }

        }

        private void fillGrid()
        {
            if (ObjServices.ProductLine == Utility.ProductLine.Auto)
            {
                //IEnumerable<Policy.Vehicle.Requirement> Result = ObjServices.GetDataDocument();//Original
                IEnumerable<Requirement.Product> Result = ObjServices.GetDataDocumentVehicle();

                //UpdateDataRequirement(ref Result);//Original
                UpdateDataRequirementVehicle(ref Result);

                //Si es un Subscriptor o un usuario de colocacion de facultativo
                if (ObjServices.IsSuscripcionQuotRole || ObjServices.IsFacultativeCot)
                    Result = Result.Where(k => k.AssingTo == Utility.AgentRoleType.Agent.ToString() ||
                                               k.AssingTo == Utility.AgentRoleType.Subscritor.ToString() ||
                                               k.AssingTo == Utility.AgentRoleType.Facultativo.ToString()
                                               ).ToList();
                else
                    if (ObjServices.IsAgentQuotRole)
                        Result = Result.Where(k => k.AssingTo == Utility.AgentRoleType.Agent.ToString()).ToList();

                //var IsCompletedQuotation = ObjServices.isQuotationComplete(Result);//Original
                var IsCompletedQuotation = ObjServices.isQuotationCompleteVehicle(Result);

                if (StatusCompletedChange)
                {
                    if (!IsRefreshPage())
                    {
                        if (ObjServices.hdnQuotationTabs == Utility.Tabs.lnkIllustrationsToWork.ToString())
                        {
                            var StatusChange = IsCompletedQuotation ? Utility.IllustrationStatus.Complete : Utility.IllustrationStatus.ApprovedByClient;
                            var StatusChangeCode = IsCompletedQuotation ? Utility.IllustrationStatus.Complete.Code() : Utility.IllustrationStatus.ApprovedByClient.Code();

                            if (ObjServices.StatusNameKey != StatusChangeCode)
                            {
                                var Note = IsCompletedQuotation ? "Illustration Complete" : "Illustration Approved Client";

                                //Verificar si la cotizacion esta completada si no lo esta entonces ponerla
                                //Cambiar el status de la poliza a Completada
                                ObjServices.ChangeIllustrationStatus(-1,
                                                                     ObjServices.Corp_Id,
                                                                     ObjServices.Region_Id,
                                                                     ObjServices.Country_Id,
                                                                     ObjServices.Domesticreg_Id,
                                                                     ObjServices.State_Prov_Id,
                                                                     ObjServices.City_Id,
                                                                     ObjServices.Office_Id,
                                                                     ObjServices.Case_Seq_No,
                                                                     ObjServices.Hist_Seq_No,
                                                                     ObjServices.UserID.GetValueOrDefault(),
                                                                     StatusChange,
                                                                     Note
                                                                    );

                                ObjServices.UpdateTempTable(ObjServices.Policy_Id, ObjServices.UserID.GetValueOrDefault());
                                UpdateResumen();
                                StatusCompletedChange = false;
                                ObjServices.StatusNameKey = StatusChangeCode;
                            }
                        }
                    }
                }

                var Dataform = Result.FirstOrDefault(h => h.RequimentOnBaseNameKey == "SUS-Formulario de Autorizacion de Descuento Comercial");

                if (Dataform != null && Dataform.DocumentId.HasValue)
                    //Actualizar la tabla plana de cotizaciones y/o polizas
                    ObjServices.UpdateTempTable(ObjServices.Policy_Id, ObjServices.UserID.GetValueOrDefault());

                var dataPolicy = ObjServices.getillustrationData();

                //Sacar de la llamada de confirmacion los casos de cambios y las inclusion declarativas
                var ExluirDocumentoLLamadaConfirmacion = ObjServices.ExConfirmationCall.Contains(dataPolicy.RequestTypeId.GetValueOrDefault());

                if (ExluirDocumentoLLamadaConfirmacion)
                    Result = Result.Where(x => x.RequimentOnBaseNameKey != "ClienteContactadoVíaTelefonica").ToList();              

                gvRequirement.DatabindAspxGridView(Result);
            }
            else
                if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                {
                    //Obtener los documentos requeridos para incendio y lineas aliadas
                    var dataReq = ObjServices.oRequirementManager.GetRequirementProduct(new Requirement.Product.Key
                     {
                         CorpId = ObjServices.Corp_Id,
                         RegionId = ObjServices.Region_Id,
                         CountryId = ObjServices.Country_Id,
                         DomesticRegId = ObjServices.Domesticreg_Id,
                         StateProvId = ObjServices.State_Prov_Id,
                         CityId = ObjServices.City_Id,
                         OfficeId = ObjServices.Office_Id,
                         CaseSeqNo = ObjServices.Case_Seq_No,
                         HistSeqNo = ObjServices.Hist_Seq_No,
                         ContactId = null
                     });

                    var order = 1;
                    dataReq = dataReq.Select(x =>
                                               {
                                                   x.OrderId = order++;
                                                   return x;
                                               }).ToList();


                    UpdateDataRequirement_IL(ref dataReq);

                    if (ObjServices.IsAgentQuotRole)
                        dataReq = dataReq.Where(k => k.AssingTo == Utility.AgentRoleType.Agent.ToString()).ToList();
                    else
                        if (ObjServices.IsSuscripcionQuotRole)
                            dataReq = dataReq.Where(k => k.AssingTo == Utility.AgentRoleType.Agent.ToString() ||
                                                         k.AssingTo == Utility.AgentRoleType.Subscritor.ToString() ||
                                                         k.AssingTo == Utility.AgentRoleType.Facultativo.ToString()).ToList();

                    var IsCompletedQuotation = ObjServices.isQuotationCompleteIL(dataReq);

                    if (StatusCompletedChange)
                    {
                        if (!IsRefreshPage())
                        {
                            if (ObjServices.hdnQuotationTabs == Utility.Tabs.lnkIllustrationsToWork.ToString())
                            {
                                var StatusChange = IsCompletedQuotation ? Utility.IllustrationStatus.Complete : Utility.IllustrationStatus.ApprovedByClient;
                                var StatusChangeCode = IsCompletedQuotation ? Utility.IllustrationStatus.Complete.Code() : Utility.IllustrationStatus.ApprovedByClient.Code();

                                var Note = IsCompletedQuotation ? "Illustration Complete" : "Illustration Approved Client";
                                if (ObjServices.StatusNameKey != StatusChangeCode)
                                {
                                    //Verificar si la cotizacion esta completada si no lo esta entonces ponerla
                                    //Cambiar el status de la poliza a Completada
                                    ObjServices.ChangeIllustrationStatus(-1,
                                                                         ObjServices.Corp_Id,
                                                                         ObjServices.Region_Id,
                                                                         ObjServices.Country_Id,
                                                                         ObjServices.Domesticreg_Id,
                                                                         ObjServices.State_Prov_Id,
                                                                         ObjServices.City_Id,
                                                                         ObjServices.Office_Id,
                                                                         ObjServices.Case_Seq_No,
                                                                         ObjServices.Hist_Seq_No,
                                                                         ObjServices.UserID.GetValueOrDefault(),
                                                                         StatusChange,
                                                                         Note
                                                                        );

                                    ObjServices.UpdateTempTable(ObjServices.Policy_Id, ObjServices.UserID.GetValueOrDefault());
                                    UpdateResumen();
                                    StatusCompletedChange = false;
                                    ObjServices.StatusNameKey = StatusChangeCode;
                                }
                            }
                        }
                    }

                    var Dataform = dataReq.FirstOrDefault(h => h.RequimentOnBaseNameKey == "PROSUS-Formulario de Autorizacion de Descuento Comercial");

                    if (Dataform != null && Dataform.DocumentId.HasValue)
                        //Actualizar la tabla plana de cotizaciones y/o polizas
                        ObjServices.UpdateTempTable(ObjServices.Policy_Id, ObjServices.UserID.GetValueOrDefault());

                    gvRequirement.DatabindAspxGridView(dataReq);
                }
        }

        /// <summary>
        ///  Obtiene los documentos requeridos para la cotizacion
        /// </summary>
        public void FillData()
        {
            fillGrid();
        }

        private void PostInit()
        {
            var dataPolicy = ObjServices.getillustrationData();
            if (dataPolicy.RequestTypeId != Utility.RequestType.Cambios.ToInt())
            {
                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                {
                    //Verificar si ya se cargo el documento de Cliente contactado vía telefonica
                    var dataDoc = ObjServices.GetDataDocument().FirstOrDefault(h => h.RequimentOnBaseNameKey == "ClienteContactadoVíaTelefonica");
                    if (dataDoc != null && dataDoc.IsValid.GetValueOrDefault())
                        return;

                    GenerateReq(dataDoc);
                }
                else
                    if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                    {
                        //Verificar si ya se cargo el documento de Cliente contactado vía telefonica
                        var dataDoc = ObjServices.GetDataDocumentAlliedLines().FirstOrDefault(h => h.RequimentOnBaseNameKey == "ClienteContactadoVíaTelefonica");
                        if (dataDoc != null && dataDoc.IsValid.GetValueOrDefault())
                            return;

                        GenerateReq(dataDoc);
                    }
            }
        }

        public void Initialize()
        {
            StatusCompletedChange = true;
            ClearData();
            PostInit();
            fillGrid();
        }

        /// <summary>
        /// Generar documento requerido para lineas aliadas
        /// </summary>
        /// <param name="dataDoc"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        private Tuple<int?, int?, int, int> GenerateReq(Requirement.Product dataDoc, bool isValid = false)
        {

            Tuple<int?, int?, int, int> result = null;

            var body = isValid ? string.Format("Yo {0} certifico que al Sr(a) {1} se le ha contactado vía telefónica", ObjServices.UserFullName, ObjServices.CustomerName)
                               : string.Format("Al Sr(a) {0} se le ha contactado vía telefónica", ObjServices.CustomerName);

            var byteFile = ITextSharpService.CreatePDFDocument(Server.MapPath("~/TempFiles"),
                          "Cliente contactado vía telefónica",
                          "Cliente contactado vía telefónica",
                          "NewBusiness",
                          body);

            var pRequirementId = dataDoc.RequirementId.GetValueOrDefault();
            //Eliminar el documento actual para luego actualizar             
            var pRequirementTypeId = 10;

            var DocumentData = getDocumentData(pRequirementTypeId, pRequirementId);

            //Si el objeto DocumentData viene null es porque borraron el documento por base  de datos entonces tendre que generarlo de nuevo
            if (dataDoc != null)
                if (!dataDoc.DocumentId.HasValue || DocumentData == null)
                    //Insertarlo por primera vez
                    generatePDFDoc(byteFile);
                else
                {
                    //Eliminar el documento                    
                    var ResultDataDocument = ObjServices.GetDataDocumentAlliedLines().FirstOrDefault(g => g.RequirementId == pRequirementId);

                    if (ResultDataDocument != null)
                        validateDocGeneratingSystem(false, ResultDataDocument.FunctionalityId, ResultDataDocument.FunctionalitySeqNo, pRequirementTypeId, pRequirementId);

                    if (DocumentData != null)
                    {
                        ObjServices.oRequirementManager.DeleteDocument(new Requirement.Document()
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
                            RequirementCatId = DocumentData.RequirementCatId,
                            RequirementDocId = DocumentData.RequirementDocId,
                            RequirementTypeId = DocumentData.RequirementTypeId,
                            RequirementId = DocumentData.RequirementId,
                            DocumentCategoryId = DocumentData.DocumentCategoryId,
                            DocumentTypeId = DocumentData.DocumentTypeId,
                            DocumentId = DocumentData.DocumentId,
                            ContactId = ObjServices.ContactEntityID.GetValueOrDefault(),
                            UserId = ObjServices.UserID.Value
                        });

                        //Actualizar
                        generatePDFDoc(byteFile, dataDoc.DocumentId, pRequirementId);
                    }

                    result = new Tuple<int?, int?, int, int>(ResultDataDocument.FunctionalityId, ResultDataDocument.FunctionalitySeqNo, pRequirementTypeId, pRequirementId);
                }

            return result;

        }

        /// <summary>
        /// Generar documento requerido para vehiculos
        /// </summary>
        /// <param name="dataDoc"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        private Tuple<int?, int?, int, int> GenerateReq(Policy.Vehicle.Requirement dataDoc, bool isValid = false)
        {
            Tuple<int?, int?, int, int> result = null;

            var body = isValid ? string.Format("Yo {0} certifico que al Sr(a) {1} se le ha contactado via telefónica", ObjServices.UserFullName, ObjServices.CustomerName)
                               : string.Format("Al Sr(a) {0} se le ha contactado via telefónica", ObjServices.CustomerName);

            var byteFile = ITextSharpService.CreatePDFDocument(Server.MapPath("~/TempFiles"),
                          "Cliente contactado vía telefónica",
                          "Cliente contactado vía telefónica",
                          "NewBusiness",
                          body);

            var pRequirementId = dataDoc.RequirementId.GetValueOrDefault();
            //Eliminar el documento actual para luego actualizar             
            var pRequirementTypeId = 49;

            var DocumentData = getDocumentData(pRequirementTypeId, pRequirementId);

            //Si el objeto DocumentData viene null es porque borraron el documento por base  de datos entonces tendre que generarlo de nuevo
            if (dataDoc != null)
                if (!dataDoc.DocumentId.HasValue || DocumentData == null)
                {
                    //Insertarlo por primera vez
                    generatePDFDoc(byteFile);
                }
                else
                {
                    //Eliminar el documento                    
                    var ResultDataDocument = ObjServices.GetDataDocument().FirstOrDefault(g => g.RequirementId == pRequirementId);

                    if (ResultDataDocument != null)
                        validateDocGeneratingSystem(false, ResultDataDocument.FunctionalityId, ResultDataDocument.FunctionalitySeqNo, pRequirementTypeId, pRequirementId);

                    if (DocumentData != null)
                    {
                        ObjServices.oRequirementManager.DeleteDocument(new Requirement.Document()
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
                            RequirementCatId = DocumentData.RequirementCatId,
                            RequirementDocId = DocumentData.RequirementDocId,
                            RequirementTypeId = DocumentData.RequirementTypeId,
                            RequirementId = DocumentData.RequirementId,
                            DocumentCategoryId = DocumentData.DocumentCategoryId,
                            DocumentTypeId = DocumentData.DocumentTypeId,
                            DocumentId = DocumentData.DocumentId,
                            ContactId = ObjServices.ContactEntityID.GetValueOrDefault(),
                            UserId = ObjServices.UserID.Value
                        });

                        //Actualizar
                        generatePDFDoc(byteFile, dataDoc.DocumentId, pRequirementId);
                    }

                    result = new Tuple<int?, int?, int, int>(ResultDataDocument.FunctionalityId, ResultDataDocument.FunctionalitySeqNo, pRequirementTypeId, pRequirementId);
                }

            return result;
        }

        public void ClearData()
        {
            gvRequirement.KeyFieldName = string.Empty;
            pnChkValidate.Visible = false;
            _isReadOnly = false;
            _SelectedRequirementTypeId = 0;
            _SelectedRequirementId = 0;
            _SelectedFunctionalitySeqNo = 0;
            _SelectedFunctionalityId = 0;
            _SelectedInsuredVehicleId = 0;
            _SelectedRequimentOnBaseNameKey = string.Empty;
        }

        /// <summary>
        /// Obtener data de los documentos
        /// </summary>
        /// <param name="pSelectedRequirementTypeId"></param>
        /// <param name="pSelectedRequirementId"></param>
        /// <returns></returns>
        public Requirement.Document getDocumentData(int? pSelectedRequirementTypeId = null, int? pSelectedRequirementId = null)
        {
            var RequirementCatId = 6; // Vehiculo

            if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
            {
                //Incendio y lineas aliadas

                switch (ObjServices.AlliedLinesProductBehavior)
                {
                    case Utility.AlliedLinesType.Property:
                        RequirementCatId = 7;
                        break;
                    case Utility.AlliedLinesType.Navy:
                        RequirementCatId = 8;
                        break;
                    case Utility.AlliedLinesType.Transport:
                        RequirementCatId = 9;
                        break;
                    case Utility.AlliedLinesType.Airplane:
                        RequirementCatId = 10;
                        break;
                    case Utility.AlliedLinesType.Bail:
                        RequirementCatId = 11;
                        break;
                }
            }

            var DocumentData = ObjServices.oRequirementManager.GetAllDocuments
                (
                     ObjServices.Corp_Id,
                     ObjServices.Region_Id,
                     ObjServices.Country_Id,
                     ObjServices.Domesticreg_Id,
                     ObjServices.State_Prov_Id,
                     ObjServices.City_Id,
                     ObjServices.Office_Id,
                     ObjServices.Case_Seq_No,
                     ObjServices.Hist_Seq_No,
                     ObjServices.ContactEntityID.GetValueOrDefault(),
                     RequirementCatId,
                     pSelectedRequirementTypeId.HasValue ? pSelectedRequirementTypeId.GetValueOrDefault() : _SelectedRequirementTypeId,
                     pSelectedRequirementId.HasValue ? pSelectedRequirementId.GetValueOrDefault() : _SelectedRequirementId
                ).FirstOrDefault();

            return DocumentData;
        }

        private void UpdateResumen()
        {
            //Actualizar el resumen   
            var IllustrationInformationUC = Utility.GetAllChildren(this.Page).FirstOrDefault(uc => uc is UCIllustrationInformation);

            if (IllustrationInformationUC != null)
                (IllustrationInformationUC as UCIllustrationInformation).FillData();
        }

        /// <summary>
        /// Salvar el documento como requirement y guarda el binario en documents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveDocument_Click(object sender, EventArgs e)
        {
            save();
        }

        public void validateDocGeneratingSystem(bool pIsReviewed, int? pSelectedFunctionalityId = null, int? pSelectedFunctionalitySeqNo = null, int? pSelectedRequirementTypeId = null, int? pSelectedRequirementId = null)
        {
            //Validar todas las polizas del contacto de la cotización seleccionada
            //var ContactId = ObjServices.Contact_Id;
            //var DataPolicyContact = ObjServices.oPolicyManager.GetPolicyByContact(ContactId);

            //if (DataPolicyContact.Count() > 1)
            //{

            //}            

            var DocData = getDocumentData(pSelectedRequirementTypeId, pSelectedRequirementId);
            if (DocData != null)
            {
                var parameter = new DataReview.DocumentToReview
                {
                    CorpId = DocData.CorpId,
                    RegionId = DocData.RegionId,
                    CountryId = DocData.CountryId,
                    DomesticregId = DocData.DomesticregId,
                    StateProvId = DocData.StateProvId,
                    CityId = DocData.CityId,
                    OfficeId = DocData.OfficeId,
                    CaseSeqNo = DocData.CaseSeqNo,
                    HistSeqNo = DocData.HistSeqNo,
                    IsReviewed = pIsReviewed,
                    UserId = ObjServices.UserID.Value,
                    ProjectId = ObjServices.ProjectId,
                    TabId = 8,
                    PaymentDetId = DocData.DocumentId,
                    FunctionalityId = _SelectedFunctionalityId > 0 ? pSelectedFunctionalityId.HasValue ? pSelectedFunctionalityId.GetValueOrDefault() : _SelectedFunctionalityId : 1,
                    FunctionalitySeqNo = _SelectedFunctionalitySeqNo > 0 ? pSelectedFunctionalitySeqNo.HasValue ? pSelectedFunctionalitySeqNo.GetValueOrDefault() : _SelectedFunctionalitySeqNo : -1
                };

                ObjServices.oDataReviewManager.SetDocumentReview(parameter);
            }
        }

        public void validateDoc(bool pIsReviewed, int? pSelectedFunctionalityId = null, int? pSelectedFunctionalitySeqNo = null, int? pSelectedRequirementTypeId = null, int? pSelectedRequirementId = null)
        {
            var TransunionClient = ObjServices.TransunionServiceLogIn(ObjServices.user, ObjServices.pass, ObjServices.DefaulltPassword);
            var TitleMessageError = new string[] { "Error", "Advertencia", "Información" };
            var DocumentoTransunion = "{0}-Verificacion historial crediticio cliente (datacredito)";
            var DocumentoClienteContactadoViaTelefonica = "ClienteContactadoVíaTelefonica";
            var TitleError = TitleMessageError[0];
            var _declinarPorBlackList = false;
            var hasProblem = false;

            try
            {
                var DocumentTransunionValidationAndBlackList = new[] { "PROSUS-Solicitud", "SUS-Solicitud de Seguro" };

                var DocData = getDocumentData(pSelectedRequirementTypeId, pSelectedRequirementId);
                if (DocData == null)
                    return;

                var isClienteContactadoVíaTelefonica = _SelectedRequimentOnBaseNameKey == DocumentoClienteContactadoViaTelefonica;

                var parameter = new DataReview.DocumentToReview
                {
                    CorpId = DocData.CorpId,
                    RegionId = DocData.RegionId,
                    CountryId = DocData.CountryId,
                    DomesticregId = DocData.DomesticregId,
                    StateProvId = DocData.StateProvId,
                    CityId = DocData.CityId,
                    OfficeId = DocData.OfficeId,
                    CaseSeqNo = DocData.CaseSeqNo,
                    HistSeqNo = DocData.HistSeqNo,
                    IsReviewed = pIsReviewed,
                    UserId = ObjServices.UserID.Value,
                    ProjectId = ObjServices.ProjectId,
                    TabId = 8,
                    PaymentDetId = DocData.DocumentId,
                    FunctionalityId = _SelectedFunctionalityId > 0 ? pSelectedFunctionalityId.HasValue ? pSelectedFunctionalityId.GetValueOrDefault() : _SelectedFunctionalityId : 1,
                    FunctionalitySeqNo = _SelectedFunctionalitySeqNo > 0 ? pSelectedFunctionalitySeqNo.HasValue ? pSelectedFunctionalitySeqNo.GetValueOrDefault() : _SelectedFunctionalitySeqNo : -1
                };

                if (!isClienteContactadoVíaTelefonica)
                    ObjServices.oDataReviewManager.SetDocumentReview(parameter);

                if (_SelectedRequimentOnBaseNameKey == DocumentoClienteContactadoViaTelefonica)
                {
                    if (!ObjServices.IsConfirmationCallCot && !ObjServices.IsConfirmationCallManagerCot && !ObjServices.IsSuscripcionQuotRole && !ObjServices.isUserCot)
                    {
                        this.MessageBox(Resources.ValidationMessageConfirmationCallDoc);
                        return;
                    }

                    if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                    {
                        //Verificar si ya se cargo el documento de Cliente contactado vía telefonica
                        var dataDoc = ObjServices.GetDataDocument().FirstOrDefault(h => h.RequimentOnBaseNameKey == DocumentoClienteContactadoViaTelefonica);
                        var result = GenerateReq(dataDoc, pIsReviewed);
                        validateDocGeneratingSystem(pIsReviewed, result.Item1, result.Item2, result.Item3, result.Item4);
                    }
                    else
                        if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                        {
                            //Verificar si ya se cargo el documento de Cliente contactado vía telefonica
                            var dataDoc = ObjServices.GetDataDocumentAlliedLines().FirstOrDefault(h => h.RequimentOnBaseNameKey == DocumentoClienteContactadoViaTelefonica);
                            var result = GenerateReq(dataDoc, pIsReviewed);
                            validateDocGeneratingSystem(pIsReviewed, result.Item1, result.Item2, result.Item3, result.Item4);
                        }

                    ObjServices.UpdateTempTable(ObjServices.Policy_Id, ObjServices.UserID.GetValueOrDefault());

                    if (pIsReviewed)
                    {
                        //Poner una nota de que se valido el documento de llamada de confirmación
                        var messageNote = string.Format("El cliente fue contactado vía telefonica por : {0}", ObjServices.UserFullName);
                        ObjServices.SaveNotes(DocData.CorpId,
                                              DocData.RegionId,
                                              DocData.CountryId,
                                              DocData.DomesticregId,
                                              DocData.StateProvId,
                                              DocData.CityId,
                                              DocData.OfficeId,
                                              DocData.CaseSeqNo,
                                              DocData.HistSeqNo,
                                              ObjServices.UserID.GetValueOrDefault(),
                                              messageNote);
                    }
                }

                //Verificar si se subio el documento solicitud de seguro firmada para consumir el servicio de transunion
                if (DocumentTransunionValidationAndBlackList.Contains(_SelectedRequimentOnBaseNameKey) && pIsReviewed)
                {
                    //Objeto de la Data de la Poliza
                    var PolicyData = ObjServices.oPolicyManager.GetPolicy(ObjServices.Corp_Id,
                                                                          ObjServices.Region_Id,
                                                                          ObjServices.Country_Id,
                                                                          ObjServices.Domesticreg_Id,
                                                                          ObjServices.State_Prov_Id,
                                                                          ObjServices.City_Id,
                                                                          ObjServices.Office_Id,
                                                                          ObjServices.Case_Seq_No,
                                                                          ObjServices.Hist_Seq_No
                                                                         );

                    bool _Declinar = false;
                    string _TipoNameKey = "";
                    var Reasons = new List<Utility.Reason>() { };

                    //Id Doc
                    var dataId = ObjServices.oContactManager.GetAllIdDocumentInformation(PolicyData.ContactId, ObjServices.Language.ToInt());
                    var RecordId = dataId.FirstOrDefault();

                    var ContactData = ObjServices.oContactManager.GetContact(ObjServices.Corp_Id, PolicyData.ContactId, ObjServices.Language.ToInt());

                    if (RecordId.ContactIdType == Utility.IdentificationType.ID.ToInt() ||
                        RecordId.ContactIdType == Utility.IdentificationType.DriverLicense.ToInt())
                    {
                        /*
                         22300857731 RB
                         00116209784 RB
                         00114188618 RA
                         00102583895 RM 
                         
                         Probar  
                         00116209784
                         00114188618
                         00102583895
                         22300857731
                        */

                        var pCedulaOrDriverLicense = RecordId.Id.Replace("-", "").RemoveInvalidCharacters().RemoveAccentsWithRegEx();
                        #region El salvador
                        if (ObjServices.Country == Utility.Country.ElSalvador)
                        {
                            var EquiFaxclient = ObjServices.EquifaxServiceLogIn(ObjServices.userEF, ObjServices.passEF, ObjServices.DefaulltPasswordEF);
                            var lstIdentification = ObjServices.oContactManager.GetAllIdDocumentInformation(ObjServices.ContactEntityID.GetValueOrDefault(),
                            ObjServices.getCurrentLanguage());

                            string _dui = "";
                            string _nit = "";
                            string FechaNac = "";
                            EquifaxService.TipoPersona tipoPers;

                            if (lstIdentification != null && lstIdentification.Any())
                            {
                                foreach (var id in lstIdentification)
                                {
                                    if (id.ContactIdTypeDescription.ToUpper() == "DUI")
                                        _dui = id.Id;

                                    if (id.ContactIdTypeDescription.ToUpper() == "NIT")
                                        _nit = id.Id;
                                }
                            }

                            if (ContactData.Dob.HasValue)
                                FechaNac = ContactData.Dob.Value.ToString("ddMMyyyy").ToUpper();

                            tipoPers = (ContactData.TipoPersona == "Natural") ? EquifaxService.TipoPersona.Natural
                                                                              : EquifaxService.TipoPersona.Juridica;

                            var Parametros = new EquifaxService.Input
                            {
                                dui = _dui,                                     //"033485386"       ,  
                                nit = _nit,                                     //"03070311841016"  ,  
                                fechaNacimiento = FechaNac,                     //"03111984"        ,  
                                primerNombre = ContactData.FirstName,           //"Joel"            ,  
                                segundoNombre = ContactData.MiddleName,         //"Alfonso"         ,  
                                primerApellido = ContactData.FirstLastName,     //"Solis"           ,  
                                segundoApellido = ContactData.SecondLastName,   //"Santos"          ,  
                                apellidoCasada = ContactData.SecondLastName,    //""                ,  
                                UserId = ObjServices.user,                      //"jsolis"          ,  
                                tipoPersona = tipoPers
                            };

                            var RiesgoEquifax = EquiFaxclient.GetRiesgoBaseScoreFromEquifax(Parametros);
                            Reasons = Utility.deserializeJSON<List<Utility.Reason>>(RiesgoEquifax.Razones);
                            _Declinar = RiesgoEquifax.Declinar;
                            _TipoNameKey = RiesgoEquifax.TipoNameKey;
                        }
                        #endregion
                        #region Republica Dominicana
                        else if (ObjServices.Country == Utility.Country.RepublicaDominicana)
                        {
                            #region validacion de Black List
                            if (string.IsNullOrEmpty(ObjServices.BlacklistMember))
                            {
                                var ResultMessage = string.Empty;

                                try
                                {
                                    ResultMessage = ObjServices.ValidacionBlackList(PolicyData.PolicyNo,
                                                                                    ContactData,
                                                                                    pCedulaOrDriverLicense,
                                                                                    Utility.BlackListAction.Yes,
                                                                                    ref hasProblem,
                                                                                    ref _declinarPorBlackList
                                                                                    );

                                    if (!string.IsNullOrEmpty(ResultMessage))
                                        this.MessageBox(ResultMessage);


                                    UpdateResumen();

                                    //Actualizar Footer de la pagina
                                    (this.Page as NewBusiness.Pages.IllustrationsVehicle).Initialize();
                                }
                                catch (Exception ex)
                                {
                                    var isTest = System.Configuration.ConfigurationManager.AppSettings["isTestingQuotDebug"] == "true";
                                    var msg = string.Format("Se ha generado un error al intentar hacer la validación de BlackList Mensaje de Error : {0}", ex.Message);
                                    var Subject = string.Format("Bandeja - {0} Error haciendo validación de BlackList Cotizacion No {1}", isTest ? "Dev" : "Prod", PolicyData.PolicyNo);
                                    ObjServices.SendEmailError(msg, Subject, "EmailSendErrorBlackList");
                                }

                            }
                            #endregion

                            #region Validacion de Transunion
                            var param = new Identification
                                        {
                                            Cedula = pCedulaOrDriverLicense,
                                            UserId = ObjServices.UserName
                                        };

                            var Riesgo = TransunionClient.GetRiesgoBaseScore(param);
                            if (Riesgo != null)
                            {
                                Reasons = Utility.deserializeJSON<List<Utility.Reason>>(Riesgo.Razones);
                                _TipoNameKey = Riesgo.TipoNameKey;
                                _Declinar = Riesgo.Declinar;

                                Policy.Vehicle.Requirement dataDocReqAuto = null;
                                Requirement.Product dataDocReqAlliedLines = null;
                                var isAuto = false;
                                var HasDoc = false;

                                //Generar el pdf con la depuracion financiera  
                                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                                {
                                    DocumentoTransunion = string.Format(DocumentoTransunion, "SUS");
                                    //Verificar si ya se cargo el documento de Cliente contactado vía telefonica
                                    dataDocReqAuto = ObjServices.GetDataDocument().FirstOrDefault(h => h.RequimentOnBaseNameKey == DocumentoTransunion);
                                    isAuto = true;
                                    HasDoc = dataDocReqAuto.DocumentId.HasValue;
                                }
                                else
                                    if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                                    {
                                        DocumentoTransunion = string.Format(DocumentoTransunion, "PROSUS");
                                        //Verificar si ya se cargo el documento de Cliente contactado vía telefonica
                                        dataDocReqAlliedLines = ObjServices.GetDataDocumentAlliedLines().FirstOrDefault(h => h.RequimentOnBaseNameKey == DocumentoTransunion);
                                        HasDoc = dataDocReqAlliedLines.DocumentId.HasValue;
                                    }

                                if (!HasDoc)
                                {
                                    var dataReqTransunion = new Utility.ItemRequirement
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
                                        ContactId = ObjServices.ContactEntityID.GetValueOrDefault(),
                                        RequirementCatId = isAuto ? dataDocReqAuto.RequirementCatId : dataDocReqAlliedLines.RequirementCatId,
                                        RequirementTypeId = isAuto ? dataDocReqAuto.RequirementTypeId.GetValueOrDefault() : dataDocReqAlliedLines.RequirementTypeId.GetValueOrDefault(),
                                        RequirementId = isAuto ? dataDocReqAuto.RequirementId.GetValueOrDefault() : dataDocReqAlliedLines.RequirementId.GetValueOrDefault(),
                                        userId = ObjServices.UserID.GetValueOrDefault(),
                                        SelectedInsuredVehicleId = _SelectedInsuredVehicleId
                                    };

                                    hdndReq.Value = Utility.serializeToJSON(dataReqTransunion);

                                    var url = "'http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/NewBusiness/Pages/Transunion.aspx?data={0}'";
                                    var urlParams = string.Format(url, HttpUtility.UrlEncode(Utility.Encrypt_Query(pCedulaOrDriverLicense + "|" + _TipoNameKey))).Replace("'", "");
                                    this.ExcecuteJScript(string.Format("RequestPage('{0}')", urlParams));
                                }
                            }
                            else
                            {
                                TitleError = TitleMessageError[1];
                                throw new Exception("No se pudo hacer la validacion crediticia");
                            }

                            #endregion
                        }
                        #endregion

                        #region Si el producto es de fianzas hacer validacion crediticia para los fiadores
                        if (ObjServices.AlliedLinesProductBehavior == Utility.AlliedLinesType.Bail)
                        {
                            var dataBail = ObjServices.GetDataBail();

                            foreach (var item in dataBail)
                            {
                                var prt = new Bail.Insured.Guarantors.Key
                                {
                                    CorpId = item.CorpId,
                                    UniqueBailId = item.UniqueBailId,
                                    SeqId = null
                                };

                                var dataGuarantors = ObjServices.oBailManager.GetBailInsuredGuarantors(prt);

                                foreach (var itemG in dataGuarantors)
                                {
                                    if (itemG.IdentificationTypeDesc.ToLower() == "id")
                                    {
                                        var param = new Identification
                                        {
                                            Cedula = itemG.Identification,
                                            UserId = ObjServices.UserName
                                        };

                                        var Riesgo = TransunionClient.GetRiesgoBaseScore(param);

                                        if (Riesgo != null)
                                        {
                                            var _TipoNameKeyGuarantor = Riesgo.TipoNameKey;

                                            var itemGuarantor = new Bail.Insured.Guarantors.Key
                                            {
                                                CorpId = itemG.CorpId,
                                                UniqueBailId = itemG.UniqueBailId,
                                                SeqId = itemG.SeqId,
                                                IdentificationTypeId = itemG.IdentificationTypeId,
                                                Identification = itemG.Identification,
                                                Name = itemG.Name,
                                                LastName = itemG.LastName,
                                                Email = itemG.Email,
                                                Phone = itemG.Phone,
                                                Address = itemG.Address,
                                                CountryId = itemG.CountryId,
                                                DomesticregId = itemG.DomesticregId,
                                                StateProvId = itemG.StateProvId,
                                                CityId = itemG.CityId,
                                                NationalityCountryId = itemG.NationalityCountryId,
                                                RepresentativeName = itemG.RepresentativeName,
                                                RepresentativeIdentificationTypeId = itemG.RepresentativeIdentificationTypeId,
                                                RepresentativeIdentification = itemG.RepresentativeIdentification,
                                                BaileeStatusId = itemG.BaileeStatusId,
                                                TipoRiesgoNameKey = _TipoNameKeyGuarantor,
                                                UserId = ObjServices.UserID
                                            };

                                            ObjServices.oBailManager.SetBailInsuredGuarantors(itemGuarantor);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        //Actualizar el contacto
                        ContactData.TipoRiesgoNameKey = _TipoNameKey;

                        var Policy = new Entity.UnderWriting.Entities.Contact.PolicyContact
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
                            UserId = ObjServices.UserID.GetValueOrDefault(),
                            ContactRoleTypeId = Utility.ContactRoleIDType.Client.ToInt(),
                            ContactId = ContactData.ContactId
                        };

                        ContactData.PolicyInfo = Policy;

                        //Actualizar el contacto de la poliza
                        ObjServices.oPolicyManager.UpdatePersonalInfoContact(ContactData);

                        //Actualizar la tabla plana de cotizaciones y/o polizas
                        ObjServices.UpdateTempTable(PolicyData.PolicyNo, ObjServices.UserID.GetValueOrDefault());

                        UpdateResumen();

                        //declinar por subscripcion automaticamente                        
                        var hasDeclined = _Declinar;

                        #region declinar por transunion
                        if (!_declinarPorBlackList)
                            if (hasDeclined)
                                DeclineCase(Utility.DeclineType.Transunion, Reasons);

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                //hasProblem = true significa que el cliente o los vehiculos estan en lista negra no hacer esta validación
                if (!hasProblem)
                {
                    var msg = ex.GetLastInnerException().Message.ToUpper();
                    if (msg.IndexOf("cedula no.") > 0)
                        this.MessageBox(RESOURCE.UnderWriting.NewBussiness.Resources.IDnoValid);
                    else
                        this.MessageBox(msg.RemoveInvalidCharacters().Replace('\'', '\"'), Title: TitleError, Width: 800);
                }
            }
        }

        private void DeclineCase(Utility.DeclineType declineType, List<Utility.Reason> Reasons = null)
        {
            var DeclinedReasonsBySuscripcion = new List<string>(0);
            DeclinedReasonsBySuscripcion.Add(Resources.QuotationDeclinedBySubscription);
            var RazonDeclinacion = string.Empty;

            if (Reasons.Any())
            {
                foreach (var item in Reasons)
                    DeclinedReasonsBySuscripcion.Add(item.Descripcion);

                if (DeclinedReasonsBySuscripcion.Count() > 1)
                    RazonDeclinacion = string.Join("<br/><br/>", DeclinedReasonsBySuscripcion.ToArray());
                else
                {
                    DeclinedReasonsBySuscripcion.RemoveAt(0);
                    RazonDeclinacion = string.Join("", DeclinedReasonsBySuscripcion.ToArray());
                }
            }

            //Cambiar el status a declinada por subscripcion                            
            ObjServices.ChangeIllustrationStatus(-1,
                                                 ObjServices.Corp_Id,
                                                 ObjServices.Region_Id,
                                                 ObjServices.Country_Id,
                                                 ObjServices.Domesticreg_Id,
                                                 ObjServices.State_Prov_Id,
                                                 ObjServices.City_Id,
                                                 ObjServices.Office_Id,
                                                 ObjServices.Case_Seq_No,
                                                 ObjServices.Hist_Seq_No,
                                                 ObjServices.UserID.GetValueOrDefault(),
                                                 Utility.IllustrationStatus.DeclinedBySubscription,
                                                 RazonDeclinacion,
                                                 Comment: declineType == Utility.DeclineType.Transunion ? "Credit Information" : "Black List"
                                                );

            //Llevar documentos requeridos a onbase
            ObjServices.GenerateOnBaseFiles(
                                            ObjServices.Corp_Id,
                                            ObjServices.Region_Id,
                                            ObjServices.Country_Id,
                                            ObjServices.Domesticreg_Id,
                                            ObjServices.State_Prov_Id,
                                            ObjServices.City_Id,
                                            ObjServices.Office_Id,
                                            ObjServices.Case_Seq_No,
                                            ObjServices.Hist_Seq_No,
                                            true,
                                            Server.MapPath("~/NewBusiness/XML/")
                                          );

            ObjServices.UpdateTempTable(ObjServices.Policy_Id, ObjServices.UserID.GetValueOrDefault());

            if (declineType == Utility.DeclineType.Transunion)
            {
                //Informar al usuario del cambio de estatus
                var Message = string.Concat("'", Resources.TestFinancial, "<br/><br/> Razónes de la declinación:<br/><br/>", RazonDeclinacion, "'");
                var FuncCallBack = string.Concat("function(){", string.Format("BackToIllustrationList('{0}', '{1}');", StatusChangedSuccessfully, Success), "}");
                var titlex = string.Concat("'", Resources.Alert, "'");
                var OnCloseFunc = FuncCallBack;
                var key = "null";
                var script = string.Format("CustomDialogMessageWithCallBack({0},{1},{2},{3},{4})", Message, FuncCallBack, titlex, OnCloseFunc, key);
                this.ExcecuteJScript(script);
            }
        }

        protected void chkValidateDoc_CheckedChanged(object sender, EventArgs e)
        {
            validateDoc(((CheckBox)sender).Checked);
            FillData();
        }

        protected void gvRequirement_PreRender(object sender, EventArgs e)
        {
            var StrKeyField = string.Empty;
            var CampoInsuredId = string.Empty;

            var Grid = ((ASPxGridView)sender);

            switch (ObjServices.ProductLine)
            {
                case Utility.ProductLine.Auto:
                    CampoInsuredId = "InsuredId;";
                    break;
                case Utility.ProductLine.AlliedLines:
                    CampoInsuredId = "InsuredId;";
                    break;
            }

            Grid.KeyFieldName = string.Format("{0};RequirementTypeId;RequirementId;FunctionalitySeqNo;FunctionalityId;RequimentOnBaseNameKey;IsMandatory;RequirementTypeDesc;DocTypeId;DocCategoryId;DocumentId;RequirementDocId;RequirementCatId;RequirementId", CampoInsuredId);

            Grid.TranslateColumnsAspxGrid();
        }

        protected void gvRequirement_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            var Command = e.CommandArgs.CommandName;
            var Grid = (ASPxGridView)sender;

            try
            {
                var TabSelected = (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), ObjServices.hdnQuotationTabs);
                //Llenar datos necesarios para trabajar con los documentos
                var RequirementTypeDesc = Grid.GetKeyFromAspxGridView("RequirementTypeDesc", e.VisibleIndex).ToString();
                _SelectedRequirementTypeId = Grid.GetKeyFromAspxGridView("RequirementTypeId", e.VisibleIndex).ToInt();
                _SelectedRequirementId = Grid.GetKeyFromAspxGridView("RequirementId", e.VisibleIndex).ToInt();
                _SelectedFunctionalityId = Grid.GetKeyFromAspxGridView("FunctionalityId", e.VisibleIndex).ToInt();
                _SelectedFunctionalitySeqNo = Grid.GetKeyFromAspxGridView("FunctionalitySeqNo", e.VisibleIndex).ToInt();

                var DocumentTypeId = Grid.GetKeyFromAspxGridView("DocTypeId", e.VisibleIndex).ToInt();
                var DocumentCategoryId = Grid.GetKeyFromAspxGridView("DocCategoryId", e.VisibleIndex).ToInt();
                var DocumentId = Grid.GetKeyFromAspxGridView("DocumentId", e.VisibleIndex).ToInt();
                var RequirementDocId = Grid.GetKeyFromAspxGridView("RequirementDocId", e.VisibleIndex).ToInt();
                var RequirementCatId = Grid.GetKeyFromAspxGridView("RequirementCatId", e.VisibleIndex).ToInt();
                var RequirementId = Grid.GetKeyFromAspxGridView("RequirementId", e.VisibleIndex).ToInt();


                if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                    _SelectedInsuredVehicleId = Grid.GetKeyFromAspxGridView("InsuredId", e.VisibleIndex).ToInt();
                else
                    if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                        _SelectedInsuredVehicleId = Grid.GetKeyFromAspxGridView("InsuredId", e.VisibleIndex).ToInt();

                _SelectedRequimentOnBaseNameKey = Grid.GetKeyFromAspxGridView("RequimentOnBaseNameKey", e.VisibleIndex).ToString();

                switch (Command)
                {
                    case "Upload":
                        Session["pdfUploadedPath"] = null;
                        hdnShowUploadFile.Value = "true";
                        UCRequirementPdfPopUp.SetTitle(RequirementTypeDesc);
                        mpeUploadFile.Show();
                        updUploadFile.Update();
                        StatusCompletedChange = true;
                        break;
                    #region Delete
                    case "Delete":
                        //DocumentData = getDocumentData();

                        validateDoc(false);

                        ObjServices.oRequirementManager.DeleteDocument(new Requirement.Document()
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
                                   RequirementCatId = RequirementCatId,
                                   RequirementDocId = RequirementDocId,
                                   RequirementId = RequirementId,
                                   DocumentCategoryId = DocumentCategoryId,
                                   DocumentTypeId = DocumentTypeId,
                                   DocumentId = DocumentId,
                                   RequirementTypeId = _SelectedRequirementTypeId,
                                   ContactId = ObjServices.ContactEntityID.GetValueOrDefault(),
                                   UserId = ObjServices.UserID.Value
                               });

                        StatusCompletedChange = true;

                        if (_SelectedRequimentOnBaseNameKey == "SUS-Formulario de Autorizacion de Descuento Comercial" ||
                            _SelectedRequimentOnBaseNameKey == "PROSUS-Formulario de Autorizacion de Descuento Comercial")
                            //Actualizar la tabla plana de cotizaciones y/o polizas
                            ObjServices.UpdateTempTable(ObjServices.Policy_Id, ObjServices.UserID.GetValueOrDefault());

                        FillData();
                        break;
                    #endregion
                    #region View
                    case "View":
                        chkValidateDoc.AutoPostBack = false;
                        chkValidateDoc.Checked = false;

                        var TabsValidateDocs = new List<Utility.Tabs>(0);

                        //Tabs donde solo el suscriptor puede validar los documentos
                        ObjServices.GettingDropData(Utility.DropDownType.CanValidDocumentTab).Select(g => new
                        {
                            TabName = (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), string.Concat("lnk", g.StateProvDesc))
                        }).ToList().ForEach(p => { TabsValidateDocs.Add(p.TabName); });

                        //Mostrar el check de validacion solo los tab necesarios que son Suscripcion y Confirmation Call
                        pnChkValidate.Visible = TabsValidateDocs.Contains(TabSelected);

                        if (TabSelected == Utility.Tabs.lnkMissingInspections)
                            pnChkValidate.Visible = ObjServices.IsSuscripcionQuotRole;

                        //Mostrar el PDF guardado en un popup                        
                        var Document = ObjServices.oPaymentManager.GetDocument(ObjServices.Corp_Id,
                                                                               ObjServices.Region_Id,
                                                                               ObjServices.Country_Id,
                                                                               ObjServices.Domesticreg_Id,
                                                                               ObjServices.State_Prov_Id,
                                                                               ObjServices.City_Id,
                                                                               ObjServices.Office_Id,
                                                                               ObjServices.Case_Seq_No,
                                                                               ObjServices.Hist_Seq_No,
                                                                               DocumentCategoryId,
                                                                               DocumentTypeId,
                                                                               DocumentId
                                                                               );
                        if (Document == null)
                        {
                            this.MessageBox("Este documento no existe en la base de datos!");
                            return;
                        }

                        if (Document.DocumentName.ToLower().IndexOf(".pdf") > -1)
                        {
                            //Chequear si ya ha sido validado el documento
                            if (ObjServices.ProductLine == Utility.ProductLine.Auto)
                            {
                                var dataResult = ObjServices.GetDataDocument()
                                                            .FirstOrDefault(x => x.FunctionalityId == _SelectedFunctionalityId &&
                                                                                 x.FunctionalitySeqNo == _SelectedFunctionalitySeqNo);
                                if (dataResult != null)
                                    chkValidateDoc.Checked = dataResult.IsValid.GetValueOrDefault();
                            }
                            else if (ObjServices.ProductLine == Utility.ProductLine.AlliedLines)
                            {
                                var dataResultReqAlliedLines = ObjServices.GetDataDocumentAlliedLines()
                                                                          .FirstOrDefault(x => x.FunctionalityId == _SelectedFunctionalityId &&
                                                                                               x.FunctionalitySeqNo == _SelectedFunctionalitySeqNo);

                                if (dataResultReqAlliedLines != null)
                                    chkValidateDoc.Checked = dataResultReqAlliedLines.IsValid.GetValueOrDefault();
                            }

                            pdfViewerMyPreviewPDF.PdfSourceBytes = Document.DocumentBinary;
                            pdfViewerMyPreviewPDF.DataBind();

                            ltTypeDoc2.Text = RequirementTypeDesc;
                            hdnShowPDF.Value = "true";
                            udpPrev.Update();
                            MPopPDFViewer.Show();
                            chkValidateDoc.AutoPostBack = true;

                            var isClienteContactadoVíaTelefonicaReq = _SelectedRequimentOnBaseNameKey == "ClienteContactadoVíaTelefonica";
                            var isContratoFacultativo = Grid.GetKeyFromAspxGridView("RequimentOnBaseNameKey", e.VisibleIndex).ToString() == "PROSUS-Contrato Facultativo";

                            if (isClienteContactadoVíaTelefonicaReq || isContratoFacultativo)
                            {
                                ////Si es el documento de llamada de confirmación y el usuario es un Confirmation call Manager o Confirmation call entonces puede validarlo
                                //Validacion especial para el boton de validacion
                                if (isClienteContactadoVíaTelefonicaReq && chkValidateDoc.Checked)
                                    pnChkValidate.Visible = false;

                                //Validar el factultativo
                                if (isContratoFacultativo && !ObjServices.IsValidateFacultativeCot && !ObjServices.isUserCot)
                                    pnChkValidate.Visible = false;
                            }
                            else if (!ObjServices.IsSuscripcionQuotRole && !ObjServices.isUserCot)
                                pnChkValidate.Visible = false;


                            if (isClienteContactadoVíaTelefonicaReq)
                                chkValidateDoc.Attributes.Add("onclick", "return ConfirmationCallConfirmation(this);");
                            else
                                chkValidateDoc.Attributes.Remove("onclick");
                        }
                        else
                        {
                            _SelectedByteArrayFile = Document.DocumentBinary;
                            _SelectedFileName = Document.DocumentName;
                            this.ExcecuteJScript("$('#btnDownload').click();");
                        }
                        break;
                    #endregion
                }

                this.ExcecuteJScript("$('#popupBhvr_backgroundElement').css('display', 'none');");
            }
            catch (Exception ex)
            {
                var MessageEx = ex.Message.Replace('\'', '\"').MyRemoveInvalidCharacters();
                (this.Page as BasePage).ErrorDescription = ex.InnerException != null ? ex.InnerException.ToString() : string.Empty;
                var msg = string.Format("{0}  <br> <br> Presione Ok para descargar un archivo con el detalle del error", MessageEx);
                this.CustomDialogMessageWithCallBack(msg, "function(){$('#btnGenerateFileError').click();}", "Error", "", "");
            }
        }

        protected void gvRequirement_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            var Grid = (sender as ASPxGridView);

            if (e.RowType == GridViewRowType.Data)
            {
                #region Validar operaciones roles

                //Tabs donde ningun usuario puede hacer nada 
                var isTabApprovedBySubscription = TabSelected == Utility.Tabs.lnkApprovedBySubscription;
                var isTabMissingInspections = TabSelected == Utility.Tabs.lnkMissingInspections;
                var isTabExpired = TabSelected == Utility.Tabs.lnkExpired;
                var isDeclinedBySubscription = TabSelected == Utility.Tabs.lnkDeclinedBySubscription;
                var isDeclinedByClient = TabSelected == Utility.Tabs.lnkDeclinedByClient;
                var isHistoricalIllustrations = TabSelected == Utility.Tabs.lnkHistoricalIllustrations;
                //End                

                var isTabSubscriptions = TabSelected == Utility.Tabs.lnkSubscriptions || TabSelected == Utility.Tabs.lnkConfirmationCall;

                if (isTabApprovedBySubscription || isTabExpired || isDeclinedBySubscription || isDeclinedByClient || isHistoricalIllustrations)
                    _isReadOnly = true;

                if ((ObjServices.IsInspectorQuotRole || ObjServices.IsDirectorQuotRole) && !ObjServices.IsAngetInspectorQuotRole)
                    _isReadOnly = true;

                if (isTabSubscriptions && (ObjServices.IsAgentQuotRole || ObjServices.IsAngetInspectorQuotRole))
                    _isReadOnly = true;

                if ((isTabSubscriptions || isTabMissingInspections) && (ObjServices.IsAgentQuotRole || ObjServices.IsAngetInspectorQuotRole))
                    _isReadOnly = true;

                if (isTabSubscriptions && ((ObjServices.IsAgentQuotRole || ObjServices.IsAngetInspectorQuotRole) && ObjServices.IsSuscripcionQuotRole))
                    _isReadOnly = false;

                #endregion

                #region Habilitar/Deshabilitar operaciones
                var hasDocument = Grid.GetKeyFromAspxGridView("DocumentId", e.VisibleIndex).ToInt() > 0;
                var btnDelete = Grid.FindRowCellTemplateControl(e.VisibleIndex, null, "btnDelete") as Button;
                var btnUpload = Grid.FindRowCellTemplateControl(e.VisibleIndex, null, "btnUpload") as Button;

                btnUpload.Enabled = true;

                if (!_isReadOnly)
                {
                    btnDelete.Enabled = hasDocument;
                    btnDelete.CssClass = hasDocument ? "delete_file" : "delete_grids_gris";
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnDelete.CssClass = "delete_grids_gris";
                }

                btnUpload.CommandName = hasDocument ? "View" : "Upload";
                btnUpload.CssClass = hasDocument ? "view_file" : "upload_file";

                /*
                 Si esta en modo solo lectura y el tab seleccionado es Aprobado por Subscripcion y no tienen un documento entonces el boton de upload file se
                 debe deshabilitar 
                */
                if (_isReadOnly && !hasDocument)
                {
                    btnUpload.Enabled = false;
                    btnUpload.CssClass = "upload_file_des";
                }
                /*
                 En caso de que esta en modo solo lectura y el tab seleccionado es Aprobado por Subscripcion y tiene un documento cargado entonces 
                 * permitir que el usuario pueda ver el documento ya guardado
                 */
                else
                    if (_isReadOnly && hasDocument)
                        btnUpload.Enabled = true;

                //Validacion especial si el requerido es 
                var isNotAvailableDelete = Grid.GetKeyFromAspxGridView("RequimentOnBaseNameKey", e.VisibleIndex).ToString() == "ClienteContactadoVíaTelefonica";

                var isClienteContactadoVíaTelefonicaDoc = Grid.GetKeyFromAspxGridView("RequimentOnBaseNameKey", e.VisibleIndex).ToString() == "ClienteContactadoVíaTelefonica";

                if (isNotAvailableDelete)
                {
                    btnDelete.Enabled = false;
                    btnDelete.CssClass = "delete_grids_gris";
                }

                var RequirementDoc = Grid.GetKeyFromAspxGridView("RequimentOnBaseNameKey", e.VisibleIndex).ToString();
                var isContratoFacultativo = RequirementDoc == "PROSUS-Contrato Facultativo" || RequirementDoc == "SUS-Contrato Facultativo";
                var IsContratoFinanciamiento = RequirementDoc == "PROSUS-Contrato Financiamiento de Prima" || RequirementDoc == "SUS-Contrato Financiamiento de Prima";

                /* Validacion especial para role FacultativeCot */
                if (ObjServices.IsFacultativeCot)
                {
                    //Deshabilitar todos los botones de upload
                    if (!isContratoFacultativo && hasDocument && !isClienteContactadoVíaTelefonicaDoc)
                        btnUpload.Enabled = true;
                    else
                        if (ObjServices.isUserCot)
                            btnUpload.Enabled = true;
                        else
                            btnUpload.Enabled = isContratoFacultativo;
                }
                else
                {
                    if (isContratoFacultativo && ObjServices.IsValidateFacultativeCot || isContratoFacultativo && ObjServices.isUserCot)
                        btnUpload.Enabled = true;
                    else
                        if (isContratoFacultativo)
                            btnUpload.Enabled = false;
                    /*
                      Si el usuario tiene role de llamada de confirmación y el documento no es el requerido de llamada de confirmación
                     y esta en el tab de llamada de confirmacion entonces deshabilitar el boton de Upload/View                 
                    */

                    if ((ObjServices.IsConfirmationCallCot || ObjServices.IsConfirmationCallManagerCot) &&
                        !isClienteContactadoVíaTelefonicaDoc && TabSelected == Utility.Tabs.lnkConfirmationCall)
                        btnUpload.Enabled = false;
                    /*
                      Valida si el item es el doucmento requerido de llamada de confirmación y que este en los tabs de suscripcion o llamada de confirmación
                      si esto es correcto entonces se valida que ese documento solo se pueda validar por un suscriptor, llamada de confirmación o manager de 
                      llamada de confirmación
                    */

                    if (IsContratoFinanciamiento && (TabSelected == Utility.Tabs.lnkSubscriptions || TabSelected == Utility.Tabs.lnkConfirmationCall))
                        btnUpload.Enabled = (ObjServices.IsConfirmationCallCot || ObjServices.IsConfirmationCallManagerCot || ObjServices.IsSuscripcionQuotRole || ObjServices.isUserCot);

                    if (isClienteContactadoVíaTelefonicaDoc && (TabSelected == Utility.Tabs.lnkSubscriptions || TabSelected == Utility.Tabs.lnkConfirmationCall))
                        btnUpload.Enabled = (ObjServices.IsConfirmationCallCot || ObjServices.IsConfirmationCallManagerCot || ObjServices.IsSuscripcionQuotRole || ObjServices.isUserCot);

                    /*
                      Valida si el item es el doucmento requerido de llamada de confirmación y no esta en los tabs de suscripcion o llamada de confirmación
                      que el boton de upload/view este deshabilitado esto es para que los demas roles solo puedan ver que el documento esta ahi pero no puedan ni verlo ni validarlo  
                    */
                    if (isClienteContactadoVíaTelefonicaDoc && (TabSelected != Utility.Tabs.lnkSubscriptions && TabSelected != Utility.Tabs.lnkConfirmationCall))
                        btnUpload.Enabled = false;
                }


                #endregion

                /*
                     Rabel Obispo
                     Si hay un documento, no importa donde que se pueda ver, sin distincion.
                     Pata!!!
                 */

                if (btnUpload != null && btnUpload.CommandName == "View")
                    btnUpload.Enabled = true;

                if (!btnUpload.Enabled && hasDocument)
                    btnUpload.CssClass = "view_file_dbl";
                else if (btnUpload.Enabled && !hasDocument)
                    btnUpload.CssClass = "upload_file";
                else if (!btnUpload.Enabled)
                    btnUpload.CssClass = "upload_file_des";
            }
        }

        protected void gvRequirement_PageIndexChanged(object sender, EventArgs e)
        {
            FillData();
        }

        protected void gvRequirement_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "SORT")
                FillData();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            //Descargar el archivo
            ExportFile(_SelectedByteArrayFile, _SelectedFileName);
        }

        protected void btnRefreshList_Click(object sender, EventArgs e)
        {
            FillData();
            PostInit();
        }
    }
}