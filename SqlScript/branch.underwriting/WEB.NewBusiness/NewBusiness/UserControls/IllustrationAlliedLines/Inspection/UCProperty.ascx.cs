﻿using Entity.UnderWriting.Entities;
using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.IllustrationAlliedLines.Inspection
{
    public partial class UCProperty : UC, IUC
    {

        public bool HasSign
        {
            get
            {
                return hdnHasSign.Value == "true";
            }
        }

        #region Arrays
        string[] quotationTextDisabled = new string[] 
        {
            "txtPolizaNo",
            "txtFechaInspeccion",
            "txtIntermediario",
            "txtInspeccionadoPor",
            "txtTipoRiesgo",
            "txtSumasAseguradasEdificio",
            "txtSumasAseguradasMobiliarios",
            "txtSumasAseguradasMaquinarias",
            "txtSumasAseguradasExistencia",
            "txtNombrePropietario",            
            "txtTipoConstruccion",            
            "txtCalle",
            "txtSectorParajeSeccion",
            "txtMunicipio",
            "txtProvincia",
            "txtLongitud",
            "txtLatitud",
            "txtUbicacionInspeccionada",
            "txtNumeroIdentificacion"
        };

        string[] setTextBoxDisabled = new string[] 
        {
            "txtIntermediario",
            "txtInspeccionadoPor",
            "txtFechaInspeccion",
            "txtPolizaNo",
            "txtTipoConstruccion",
            "txtCalle",
            "txtSectorParajeSeccion",
            "txtMunicipio",
            "txtProvincia",
            "txtLongitud",
            "txtLatitud",
            "txtUbicacionInspeccionada",
            "txtNumeroIdentificacion",
            "txtIdentificationType"
        };

        string[] quotationValues = new string[] 
        {
            "txtNumeroIdentificacion",
            "txtSumasAseguradasEdificio",
            "txtSumasAseguradasMobiliarios",
            "txtSumasAseguradasMaquinarias",
            "txtSumasAseguradasExistencia"
        };

        string[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".bmp" };
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // Store the FileUpload object in Session. 
            // This condition occurs for first time you upload a file
            if (Session["fuFotografia"] == null && fuFotografia1.HasFile && Session["PhotoSaved"].ToBoolean() == false)
            {
                Session["fuFotografia"] = fuFotografia1;
            }
            // This condition will occur on next postbacks        
            else if (Session["fuFotografia"] != null && (!fuFotografia1.HasFile))
            {
                Session["PhotoSaved"] = true;
                fuFotografia1 = (FileUpload)Session["FileUpload1"];
            }
            //  when Session will have File but user want to change the file 
            // i.e. wants to upload a new file using same FileUpload control
            // so update the session to have the newly uploaded file
            else if (fuFotografia1.HasFile && Session["PhotoSaved"].ToBoolean() == false)
            {
                Session["PhotoSaved"] = true;
                Session["fuFotografia"] = fuFotografia1;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (ObjServices.isChangingLang || !IsPostBack)
                Translator(ObjServices.Language.ToString());
        }

        public void Translator(string Lang)
        {
            ViewState["IntCat"] = new Dictionary<int, string>() 
            {
                {  0, Resources.Select },
                {  1, Resources.Ceiling },
                {  2, Resources.WorkAreaOperations },
                {  3, Resources.Warehouses },
                {  4, Resources.Stocks },
                {  5, Resources.RawMaterial },
                {  6, Resources.Kitchen },
                {  7, Resources.LivingRoom },
                {  8, Resources.Pool },
                {  9, Resources.Room },
                { 10, Resources.Others }
            };

            ViewState["ExtCat"] = new Dictionary<int, string>() 
            {
                { 0, Resources.Select },
                { 1, Resources.FrontPart },
                { 2, Resources.RightSide },
                { 3, Resources.LeftSide },
                { 4, Resources.Rear },
                { 5, Resources.ElectricGenerator },
                { 6, Resources.Inverter },
                { 7, Resources.EquipmentRoom },
                { 8, Resources.Others }
            };

            if (ObjServices.hdnQuotationTabs != "lnkMissingInspections")
                btnSendToSubscription.Text = Resources.CompleteInspection;
            else
                btnSendToSubscription.Text = Resources.SendToSubscribe;


            lblLatitud.Visible = txtLatitud.Visible = ObjServices.Country == Utility.Country.RepublicaDominicana ? true : false;

            lblLatitud.Text = string.Format("{0}:", Resources.Latitude);

            pnlDO.Visible = ObjServices.Country == Utility.Country.RepublicaDominicana ? true : false;
            pnlSV.Visible = !pnlDO.Visible;

            lblLocalizacionCalle.Text = ObjServices.Country == Utility.Country.RepublicaDominicana ? Resources.FormularioRiesgoLocalizacionCalleDO
                                                                                                   : Resources.FormularioRiesgoLocalizacionCalleSV;

            lblLocalizacionSector.Text = ObjServices.Country == Utility.Country.RepublicaDominicana ? Resources.FormularioRiesgoLocalizacionSectorDO
                                                                                                    : Resources.FormularioRiesgoLocalizacionSectorSV;

            lblLocalizacionProvincia.Text = ObjServices.Country == Utility.Country.RepublicaDominicana ? Resources.FormularioRiesgoLocalizacionProvinciaDO
                                                                                                       : Resources.FormularioRiesgoLocalizacionProvinciaSV;

            lblLocalizacionLongitud.Text = ObjServices.Country == Utility.Country.RepublicaDominicana ? Resources.FormularioRiesgoLocalizacionLongitudDO
                                                                                                      : Resources.FormularioRiesgoLocalizacionLongitudSV;

            RadioButtonListSetDataSource();

            btnBackToIllustrations.Text = Resources.IllustrationDetail;
            btnClean.Text = Resources.Clean;
            btnSave.Text = Resources.Save;

            string sumasAseguradas = string.Format("2. {0} {1}", Resources.SummariesInsuredIn.ToUpper(),
                                                                 ObjServices.Country == Utility.Country.RepublicaDominicana ? Resources.FormularioRiesgoSimboloMonedaDO
                                                                                                                            : Resources.FormularioRiesgoSimboloMonedaSV),
                   JScript = string.Format("setSumasAseguradasText('{0}');", sumasAseguradas);

            this.ExcecuteJScript(JScript);
        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            try
            {
                if (string.IsNullOrEmpty(txtUsuarioInspeccion.Text.Trim()))
                {
                    this.MessageBoxALIF(string.Format(Resources.YouMustIndicateInspectionUserName), null, null, true, Resources.InformationLabel);
                    return;
                }


                if (ddlTipoconstruccion.SelectedIndex <= 0)
                {
                    this.MessageBoxALIF(string.Format(Resources.YouMustIndicateBuildType), null, null, true, Resources.InformationLabel);
                    return;
                }

                int? policyStatusId = (int?)ViewState["PolicyStatusId"],
                     reviewId = null;

                long uniquePropertyId = 0;


                #region Set Inspection Date/Time
                var horafull = txtFechaInspeccion.Text;
                var ampmfull = horafull.Split(' ');
                var array_hora = ampmfull[1].Split(':');
                var ampm = ampmfull[2];
                int hora = array_hora[0].ToInt(),
                    minutos = array_hora[1].ToInt(),
                    segundos = array_hora[2].ToInt();

                if (ampm == "PM" && hora < 12)
                    hora += 12;

                var InspectionDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hora, minutos, segundos);
                #endregion

                #region Get Unique Property Id
                uniquePropertyId = Convert.ToInt64(ddlPropiedades.SelectedValue);
                #endregion

                #region Document Category
                var document_category = ObjServices.oAlliedLinesReviewManager.GetDocumentCategory(new AlliedLines.Document.Category.Parameters.Get
                {
                    NameKey = Utility.AlliedLinesInspectionFormPhotos
                });
                #endregion

                #region Get Property
                var property = ObjServices.oPropertyManager.GetProperty(new Property.Key
                {
                    CorpId = ObjServices.Corp_Id,
                    RegionId = ObjServices.Region_Id,
                    CountryId = ObjServices.Country_Id,
                    DomesticregId = ObjServices.Domesticreg_Id,
                    StateProvId = ObjServices.State_Prov_Id,
                    CityId = ObjServices.City_Id,
                    OfficeId = ObjServices.Office_Id,
                    CaseSeqNo = ObjServices.Case_Seq_No,
                    HistSeqNo = ObjServices.Hist_Seq_No
                }).FirstOrDefault(p => p.UniquePropertyId == uniquePropertyId);
                #endregion

                var norte = property.NorthBorderId;
                var sur = property.SouthBorderId;
                var este = property.EastBorderId;
                var oeste = property.WestBorderId;

                if (ddlColindanciaNorte.SelectedIndex > 0) { norte = ddlColindanciaNorte.SelectedValue.ToInt(); }
                if (ddlColindanciaSur.SelectedIndex > 0) { sur = ddlColindanciaSur.SelectedValue.ToInt(); }
                if (ddlColindanciaEste.SelectedIndex > 0) { este = ddlColindanciaEste.SelectedValue.ToInt(); }
                if (ddlColindanciaOeste.SelectedIndex > 0) { oeste = ddlColindanciaOeste.SelectedValue.ToInt(); }

                #region Set Property
                var propiedad = ObjServices.oPropertyManager.SetProperty(new Property
                {
                    CorpId = property.CorpId,
                    PropertyId = property.PropertyId,
                    RegionId = property.RegionId,
                    CountryId = property.CountryId,
                    DomesticregId = property.DomesticregId,
                    StateProvId = property.StateProvId,
                    CityId = property.CityId,
                    BusinessTypeId = property.BusinessTypeId,
                    PropertyBuildTypeId = ddlTipoconstruccion.SelectedValue.ToInt(),
                    ActivfityTypeId = property.ActivfityTypeId,
                    ReinsuranceId = property.ReinsuranceId,
                    ReinsuranceAmount = property.ReinsuranceAmount,
                    AddressStreet = txtUbicacionInspeccionada.Text.Trim(),
                    AddressNumber = txtNumero.Text.Trim(),
                    EvaluationValue = property.EvaluationValue,
                    EdificationValue = property.EdificationValue,
                    MachineryValue = property.MachineryValue,
                    FurnitureAndEquipmentValue = property.FurnitureAndEquipmentValue,
                    StockValue = property.StockValue,
                    RemodelingAndFittingValue = property.RemodelingAndFittingValue,
                    ValueObjectAndArtValue = property.ValueObjectAndArtValue,
                    Rooms = property.Rooms,
                    Bathrooms = property.Bathrooms,
                    LocationActivityTypeId = property.LocationActivityTypeId,
                    Registry = property.Registry,
                    PropertyYear = property.PropertyYear,
                    BuildAreaSqFeet = property.BuildAreaSqFeet,
                    BuildAreaSqMeters = property.BuildAreaSqMeters,
                    GeographicLimitation = property.GeographicLimitation,
                    SouthBorderId = sur,
                    NorthBorderId = norte,
                    EastBorderId = este,
                    WestBorderId = oeste,
                    PhysicalAddress = property.PhysicalAddress,
                    accidents = property.accidents,
                    Garage = property.Garage,
                    Pool = property.Pool,
                    DistanceKilometersSea = property.DistanceKilometersSea,
                    DistanceKilometersRiver = property.DistanceKilometersRiver,
                    DistanceKilometersStream = property.DistanceKilometersStream,
                    Longitude = !string.IsNullOrEmpty(txtLongitud.Text) ? txtLongitud.ToDecimal() : property.Longitude,
                    Latitude = !string.IsNullOrEmpty(txtLatitud.Text) ? txtLatitud.ToDecimal() : property.Latitude,
                    PropertyStatusId = property.PropertyStatusId,
                    UserId = property.UserId != null ? property.UserId.GetValueOrDefault() : ObjServices.UserID.GetValueOrDefault(),
                    SourceId = property.SourceId
                });
                #endregion

                #region Review
                var getReview = ObjServices.oAlliedLinesReviewManager.GetAlliedLineReview(new AlliedLines.Review.Parameters.Get
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
                    AlliedLineId = property.PropertyId,
                    UniqueAlliedLineId = uniquePropertyId,
                    AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                    ReviewId = null,
                    BlTypeId = property.BlTypeId,
                    BlId = property.BlId,
                    ProductId = property.ProductId
                }).LastOrDefault();

                if (getReview != null)
                {
                    reviewId = getReview.ReviewId;
                    InspectionDate = getReview.InspectionDate;
                }

                var review = ObjServices.oAlliedLinesReviewManager.SetAlliedLineReview(new AlliedLines.Review.Parameters.Set
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
                    AlliedLineId = property.PropertyId,
                    UniqueAlliedLineId = uniquePropertyId,
                    AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                    BlTypeId = property.BlTypeId,
                    BlId = property.BlId,
                    ProductId = property.ProductId,
                    ReviewId = reviewId,
                    InspectionDate = InspectionDate,
                    RiskName = txtNombreRiesgo.Text,
                    InspectedLocation = txtUbicacionInspeccionada.Text,
                    RiskType = txtTipoRiesgo.Text,
                    InspectorId = ObjServices.InspectorAgentId.GetValueOrDefault(),
                    DocTypeId = document_category.DocTypeId,
                    DocCategoryId = document_category.DocCategoryId,
                    ReviewStatus = true,
                    UsrId = ObjServices.UserID.GetValueOrDefault()
                });
                #endregion

                if (review != null)
                {
                    #region Review Detail
                    var getReviewDetail = ObjServices.oAlliedLinesReviewManager.GetAlliedLineReviewDetail(new AlliedLines.Review.Detail.Parameters.Get
                    {
                        CorpId = review.CorpId,
                        RegionId = review.RegionId,
                        CountryId = review.CountryId,
                        DomesticregId = review.DomesticregId,
                        StateProvId = review.StateProvId,
                        CityId = review.CityId,
                        OfficeId = review.OfficeId,
                        CaseSeqNo = review.CaseSeqNo,
                        HistSeqNo = review.HistSeqNo,
                        AlliedLineId = review.AlliedLineId,
                        UniqueAlliedLineId = uniquePropertyId,
                        AlliedLineTypeId = review.AlliedLineTypeId,
                        ReviewId = review.ReviewId,
                        ReviewGroupId = null,
                        ReviewGroupEndorsementClarifying = null,
                        ReviewOptionEndorsementClarifying = null
                    }).ToList();



                    if (getReviewDetail.Count > 0)
                    {
                        var delReviewDetail = ObjServices.oAlliedLinesReviewManager.DelAlliedLineReviewDetail(new AlliedLines.Review.Detail.Del.Parameters.Set
                        {
                            CorpId = review.CorpId,
                            RegionId = review.RegionId,
                            CountryId = review.CountryId,
                            DomesticregId = review.DomesticregId,
                            StateProvId = review.StateProvId,
                            CityId = review.CityId,
                            OfficeId = review.OfficeId,
                            CaseSeqNo = review.CaseSeqNo,
                            HistSeqNo = review.HistSeqNo,
                            AlliedLineId = review.AlliedLineId,
                            UniqueAlliedLineId = uniquePropertyId,
                            AlliedLineTypeId = review.AlliedLineTypeId,
                            ReviewId = review.ReviewId
                        });


                    }

                    AlliedLines.Review.Detail.Result.Set detail = new AlliedLines.Review.Detail.Result.Set();
                    AlliedLines.Review.Detail.Parameters.Set parameter = new AlliedLines.Review.Detail.Parameters.Set();
                    parameter.CorpId = review.CorpId;
                    parameter.RegionId = review.RegionId;
                    parameter.CountryId = review.CountryId;
                    parameter.DomesticregId = review.DomesticregId;
                    parameter.StateProvId = review.StateProvId;
                    parameter.CityId = review.CityId;
                    parameter.OfficeId = review.OfficeId;
                    parameter.CaseSeqNo = review.CaseSeqNo;
                    parameter.HistSeqNo = review.HistSeqNo;
                    parameter.AlliedLineId = review.AlliedLineId;
                    parameter.UniqueAlliedLineId = uniquePropertyId;
                    parameter.AlliedLineTypeId = review.AlliedLineTypeId;
                    parameter.ReviewId = review.ReviewId;
                    parameter.ReviewDetailId = null;
                    parameter.Reviewtatus = true;
                    parameter.UserId = ObjServices.UserID.GetValueOrDefault();
                    parameter.UsuarioInspeccion = txtUsuarioInspeccion.Text;

                    foreach (var control in this.pnlGeneral.Controls)
                    {
                        int GroupId = 0,
                            ClassId = 0,
                            ItemId = 0,
                            OptionId = 0;

                        int ValueChecked = -1;

                        string[] grpclsitmopt = new string[] { };

                        string ValueText = string.Empty;

                        bool? Required = false;

                        bool _pnldosv = false;

                        if (control is TextBox)
                        {
                            #region TextBox
                            TextBox txt = control as TextBox;
                            if (txt.Attributes["grpclsitmopt"] != null && txt.Attributes["grpclsitmopt"] != "0|0|0|0")
                            {
                                grpclsitmopt = txt.Attributes["grpclsitmopt"].Split('|');
                                ValueChecked = -1;
                                ValueText = txt.Text;

                                if (grpclsitmopt[4].ToString().ToLower() == "n")
                                    Required = false;
                                else if (grpclsitmopt[4].ToString().ToLower() == "y")
                                    Required = true;
                                else
                                    Required = false;
                            }
                            #endregion
                        }
                        else if (control is RadioButtonList)
                        {
                            #region RadioButtonList
                            RadioButtonList rbl = control as RadioButtonList;
                            foreach (ListItem item in rbl.Items)
                            {
                                if (item.Selected)
                                {
                                    grpclsitmopt = item.Value.Split('|');
                                    if (grpclsitmopt.Length == 4)
                                    {
                                        ValueChecked = grpclsitmopt[3].ToInt();
                                        ValueText = string.Empty;
                                        Required = true;
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (control is DropDownList)
                        {
                            #region DropDownList
                            DropDownList ddl = control as DropDownList;
                            if (ddl.Attributes["GroupId"] != null)
                            {
                                var array = string.Format("{0}|{1}|1|0", ddl.Attributes["GroupId"].ToString(),
                                                                         ddl.Attributes["ClassId"].ToString());
                                grpclsitmopt = array.Split('|');
                                ValueChecked = ddl.SelectedValue.ToInt();
                                ValueText = ddl.SelectedItem.Text;
                                Required = true;
                            }
                            #endregion
                        }
                        else if (control is Panel)
                        {
                            #region Panel
                            Panel pnl = control as Panel;
                            if (pnl.Visible)
                            {
                                _pnldosv = true;

                                foreach (var ctrl in pnl.Controls)
                                {
                                    if (ctrl is TextBox)
                                    {
                                        #region TextBox
                                        TextBox txt = ctrl as TextBox;
                                        if (txt.Attributes["grpclsitmopt"] != null && txt.Attributes["grpclsitmopt"] != "0|0|0|0")
                                        {
                                            grpclsitmopt = txt.Attributes["grpclsitmopt"].Split('|');
                                            ValueChecked = -1;
                                            ValueText = txt.Text;

                                            if (grpclsitmopt[4].ToString().ToLower() == "n")
                                                Required = false;
                                            else if (grpclsitmopt[4].ToString().ToLower() == "y")
                                                Required = true;
                                            else
                                                Required = false;
                                        }
                                        #endregion
                                    }
                                    else if (ctrl is RadioButtonList)
                                    {
                                        #region RadioButtonList
                                        RadioButtonList rbl = ctrl as RadioButtonList;
                                        foreach (ListItem item in rbl.Items)
                                        {
                                            if (item.Selected)
                                            {
                                                grpclsitmopt = item.Value.Split('|');
                                                if (grpclsitmopt.Length == 4)
                                                {
                                                    ValueChecked = grpclsitmopt[3].ToInt();
                                                    ValueText = string.Empty;
                                                    Required = true;
                                                    break;
                                                }
                                            }
                                        }
                                        #endregion
                                    }

                                    #region Guardar opciones paneles "República Dominicana" o "El Salvador"
                                    if (grpclsitmopt.Count() > 0 && (grpclsitmopt.Length >= 4))
                                    {
                                        GroupId = grpclsitmopt[0].ToInt();
                                        ClassId = grpclsitmopt[1].ToInt();
                                        ItemId = grpclsitmopt[2].ToInt();
                                        OptionId = grpclsitmopt[3].ToInt();

                                        parameter.ReviewGroupId = GroupId;
                                        parameter.ReviewClassId = ClassId;
                                        parameter.ReviewItemId = ItemId;
                                        parameter.ReviewOptionId = OptionId;
                                        parameter.ValueChecked = ValueChecked;
                                        parameter.ValueText = ValueText;
                                        parameter.Required = Required;

                                        detail = ObjServices.oAlliedLinesReviewManager.SetAlliedLineReviewDetail(parameter);
                                    }
                                    #endregion
                                }
                            }
                            #endregion
                        }

                        #region Guardar opciones generales
                        if (!_pnldosv)
                        {
                            if (grpclsitmopt.Count() > 0 && (grpclsitmopt.Length >= 4))
                            {
                                GroupId = grpclsitmopt[0].ToInt();
                                ClassId = grpclsitmopt[1].ToInt();
                                ItemId = grpclsitmopt[2].ToInt();
                                OptionId = grpclsitmopt[3].ToInt();

                                parameter.ReviewGroupId = GroupId;
                                parameter.ReviewClassId = ClassId;
                                parameter.ReviewItemId = ItemId;
                                parameter.ReviewOptionId = OptionId;
                                parameter.ValueChecked = ValueChecked;
                                parameter.ValueText = ValueText;
                                parameter.Required = Required;

                                detail = ObjServices.oAlliedLinesReviewManager.SetAlliedLineReviewDetail(parameter);
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region Review Photos
                    var pics = ObjServices.oAlliedLinesReviewManager.GetPolicyDocument(new AlliedLines.Document.Policy.Parameters.Get
                    {
                        CorpId = ObjServices.Corp_Id,
                        RegionId = ObjServices.Region_Id,
                        CountryId = ObjServices.Country_Id,
                        DomesticregId = ObjServices.Domesticreg_Id,
                        StateProvId = ObjServices.State_Prov_Id,
                        CityId = ObjServices.City_Id,
                        OfficeId = ObjServices.Office_Id,
                        CaseSeqNo = ObjServices.Case_Seq_No,
                        HistSeqNo = ObjServices.Hist_Seq_No
                    }).ToList();
                    if (pics.Count > 0)
                    {
                        foreach (var pic in pics)
                        {
                            var reviewPic = ObjServices.oAlliedLinesReviewManager.GetAlliedLineReviewPic(new AlliedLines.Review.Pic.Parameters.Get
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
                                AlliedLineId = review.AlliedLineId,
                                UniqueAlliedLineId = uniquePropertyId,
                                AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                                ReviewId = review.ReviewId,
                                DocTypeId = pic.DocTypeId,
                                DocCategoryId = pic.DocCategoryId,
                                DocumentId = pic.DocumentId
                            }).FirstOrDefault();

                            int? pictureId = null;

                            if (reviewPic != null)
                                pictureId = reviewPic.PictureId;

                            if (pictureId == null)
                            {
                                var reviewPics = ObjServices.oAlliedLinesReviewManager.SetVehicleReviewPic(new AlliedLines.Review.Pic.Parameters.Set
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
                                    AlliedLineId = review.AlliedLineId,
                                    UniqueAlliedLineId = uniquePropertyId,
                                    AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                                    ReviewId = review.ReviewId,
                                    PictureId = pictureId,
                                    DocTypeId = pic.DocTypeId,
                                    DocCategoryId = pic.DocCategoryId,
                                    DocumentId = pic.DocumentId,
                                    PictureStatus = true,
                                    UsrId = ObjServices.UserID.GetValueOrDefault()
                                });
                            }
                        }
                    }
                    #endregion



                    #region Set Property Insured
                    var insured = ObjServices.oPropertyManager.SetPropertyInsured(new Property.Insured.key
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
                        PropertyId = property.PropertyId,
                        InsuredDate = property.InsuredDate,
                        InsuredAmount = property.InsuredAmount,
                        PremiumAmount = property.PremiumAmount,
                        BasePremiumAmount = property.BasePremiumAmount,
                        DeductiblePercentage = property.DeductiblePercentage,
                        DeductibleAmount = property.DeductibleAmount,
                        PropertyInspectedValue = property.PropertyInspectedValue,
                        Inspection = true,
                        PolicyPropertyStatusId = property.PropertyStatusId,
                        UserId = property.UserId != null ? property.UserId.GetValueOrDefault() : ObjServices.UserID.GetValueOrDefault(),
                        SourceId = property.SourceId
                    });
                    #endregion

                    #region Update Property Insured Detail

                    #region Endoso Aclaratorio
                    bool endosoAclaratorioSumasAseguradas = false,
                         endosoAclaratorioDescripcion = false,
                         endosoAclaratorioDescripcionPeligros = false,
                         endosoAclaratorioPrevencionProteccion = false,
                         EndorsementClarifying = false;

                    endosoAclaratorioSumasAseguradas = (rblSumasAseguradasEdificio.SelectedItem != null && rblSumasAseguradasEdificio.SelectedItem.Text == Resources.NoLabel ||
                                                        rblSumasAseguradasMobiliarios.SelectedItem != null && rblSumasAseguradasMobiliarios.SelectedItem.Text == Resources.NoLabel ||
                                                        rblSumasAseguradasMaquinarias.SelectedItem != null && rblSumasAseguradasMaquinarias.SelectedItem.Text == Resources.NoLabel ||
                                                        rblSumasAseguradasExistencia.SelectedItem != null && rblSumasAseguradasExistencia.SelectedItem.Text == Resources.NoLabel);

                    endosoAclaratorioDescripcion = (rblTipoConstruccionNoSi.SelectedItem != null && rblTipoConstruccionNoSi.SelectedItem.Text == Resources.NoLabel);

                    endosoAclaratorioDescripcionPeligros = (rblAlmacenamientoUsoCombustible.SelectedItem != null && rblAlmacenamientoUsoCombustible.SelectedItem.Text == "Sí" ||
                                                            rblEdificacionCargaCombustible.SelectedItem != null && rblEdificacionCargaCombustible.SelectedItem.Text == "Sí" ||
                                                            rblSubastacionElectrica.SelectedItem != null && rblSubastacionElectrica.SelectedItem.Text == "Sí" ||
                                                            rblGeneradoresElectricos.SelectedItem != null && rblGeneradoresElectricos.SelectedItem.Text == "Sí" ||
                                                            rblCalderas.SelectedItem != null && rblCalderas.SelectedItem.Text == "Sí" ||
                                                            rblAireComprimido.SelectedItem != null && rblAireComprimido.SelectedItem.Text == "Sí" ||
                                                            rblPasillosLibres.SelectedItem != null && rblPasillosLibres.SelectedItem.Text == Resources.NoLabel ||
                                                            rblFluidosInflamables.SelectedItem != null && rblFluidosInflamables.SelectedItem.Text == "Sí" ||
                                                            rblOrdenLimpiezaDentroRiesgo.SelectedItem != null && (rblOrdenLimpiezaDentroRiesgo.SelectedItem.Text.StartsWith("R") ||
                                                                                                                  rblOrdenLimpiezaDentroRiesgo.SelectedItem.Text.StartsWith("M")) ||
                                                            rblOrdenLimpiezaFueraRiesgo.SelectedItem != null && (rblOrdenLimpiezaFueraRiesgo.SelectedItem.Text.StartsWith("R") ||
                                                                                                                 rblOrdenLimpiezaFueraRiesgo.SelectedItem.Text.StartsWith("M")) ||
                                                            rblOrdenLimpiezaGeneral.SelectedItem != null && (rblOrdenLimpiezaGeneral.SelectedItem.Text.StartsWith("R") ||
                                                                                                             rblOrdenLimpiezaGeneral.SelectedItem.Text.StartsWith("M")) ||
                                                            rblInstalacionesElectricas.SelectedItem != null && (rblInstalacionesElectricas.SelectedItem.Text.StartsWith("R") ||
                                                                                                                rblInstalacionesElectricas.SelectedItem.Text.StartsWith("M")));

                    endosoAclaratorioPrevencionProteccion = (rblManguerasContraIncendios.SelectedItem != null && rblManguerasContraIncendios.SelectedItem.Text == Resources.NoLabel ||
                                                             rblSenalesEmergencias.SelectedItem != null && rblSenalesEmergencias.SelectedItem.Text == Resources.NoLabel ||
                                                             rblBombasAaguaContraIncendio.SelectedItem != null && rblBombasAaguaContraIncendio.SelectedItem.Text == Resources.NoLabel ||
                                                             rblPararrayos.SelectedItem != null && rblPararrayos.SelectedItem.Text == Resources.NoLabel ||
                                                             rblTomaAguaBomberos.SelectedItem != null && rblTomaAguaBomberos.SelectedItem.Text == Resources.NoLabel ||
                                                             rblRociadoresAutomaticos.SelectedItem != null && rblRociadoresAutomaticos.SelectedItem.Text == Resources.NoLabel ||
                                                             rblBrigadaContraIncendios.SelectedItem != null && rblBrigadaContraIncendios.SelectedItem.Text == Resources.NoLabel ||
                                                             rblEscalerasEmergencias.SelectedItem != null && rblEscalerasEmergencias.SelectedItem.Text == Resources.NoLabel ||
                                                             rblSistemaContraIncendio.SelectedItem != null && rblSistemaContraIncendio.SelectedItem.Text == Resources.NoLabel ||
                                                             rblPuertasEnrollables.SelectedItem != null && rblPuertasEnrollables.SelectedItem.Text == Resources.NoLabel ||
                                                             rblSistemaAlarma.SelectedItem != null && rblSistemaAlarma.SelectedItem.Text == Resources.NoLabel ||
                                                             rblServicioMonitoreoCCTV.SelectedItem != null && rblServicioMonitoreoCCTV.SelectedItem.Text == Resources.NoLabel ||
                                                             rblCajaSeguridad.SelectedItem != null && rblCajaSeguridad.SelectedItem.Text == Resources.NoLabel ||
                                                             rblRejasVentanasPuertas.SelectedItem != null && rblRejasVentanasPuertas.SelectedItem.Text == Resources.NoLabel ||
                                                             rblPulsadoresManuales.SelectedItem != null && rblPulsadoresManuales.SelectedItem.Text == Resources.NoLabel ||
                                                             rblMantenimientoPreventivo.SelectedItem != null && rblMantenimientoPreventivo.SelectedItem.Text == Resources.NoLabel ||
                                                             rblAlmacenamiento.SelectedItem != null && rblAlmacenamiento.SelectedItem.Text == Resources.NoLabel ||
                                                             rblAlmacenamientoBRM.SelectedItem != null && (rblAlmacenamientoBRM.SelectedItem.Text.StartsWith("R") || rblAlmacenamientoBRM.SelectedItem.Text.StartsWith("M")) ||
                                                             rblVigilantes.SelectedItem != null && rblVigilantes.SelectedItem.Text == Resources.NoLabel);

                    EndorsementClarifying = (endosoAclaratorioSumasAseguradas ||
                                             endosoAclaratorioDescripcion ||
                                             endosoAclaratorioDescripcionPeligros ||
                                             endosoAclaratorioPrevencionProteccion);
                    #endregion

                    var update = ObjServices.oPropertyManager.UpdatePropertyInsuredDetail(new Property.Insured.Detail.key
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
                        PropertyId = property.PropertyId,
                        SeqId = property.SeqId,
                        UniquePropertyId = property.UniquePropertyId,
                        Inspected = false,
                        EndorsementClarifying = EndorsementClarifying,
                        UserId = property.UserId != null ? property.UserId : ObjServices.UserID
                    });
                    #endregion

                    //Guardar la firma del cliente
                    if (HasSign)
                    {
                        var FirmaCiente = hdnCustomerSign.Value;
                        //Verificar si ya se ha guardado previamente la firma
                        var ExistSign = ObjServices.oPolicyManager.GetCustomerSing(ObjServices.Corp_Id,
                                                                                   ObjServices.Region_Id,
                                                                                   ObjServices.Country_Id,
                                                                                   ObjServices.Domesticreg_Id,
                                                                                   ObjServices.State_Prov_Id,
                                                                                   ObjServices.City_Id,
                                                                                   ObjServices.Office_Id,
                                                                                   ObjServices.Case_Seq_No,
                                                                                   ObjServices.Hist_Seq_No
                                                                                   ) != null;

                        if (!ExistSign)
                        {
                            ObjServices.oPolicyManager.SetCustomerSing(ObjServices.Corp_Id,
                                                                       ObjServices.Region_Id,
                                                                       ObjServices.Country_Id,
                                                                       ObjServices.Domesticreg_Id,
                                                                       ObjServices.State_Prov_Id,
                                                                       ObjServices.City_Id,
                                                                       ObjServices.Office_Id,
                                                                       ObjServices.Case_Seq_No,
                                                                       ObjServices.Hist_Seq_No,
                                                                       FirmaCiente
                                                                      );
                        }
                    }

                    string regact = reviewId != null ? "actualizado" : "registrado";
                    this.MessageBoxALIF(string.Format("Formulario {0} correctamente.", regact), null, null, true, Resources.InformationLabel);
                    FillData();
                    ddlPropiedades.SelectedValue = uniquePropertyId.ToString();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.MessageBoxALIF(string.Format("{0}<br /><br /><b>InnerException:</b> {1}", ex.Message, ex.InnerException), null, null, true, Resources.Warning);
                else
                    this.MessageBoxALIF(ex.Message, null, null, true, Resources.Warning);
            }
        }
        public void saveTMP()
        {
            try
            {

                int? policyStatusId = (int?)ViewState["PolicyStatusId"],
                     reviewId = null;

                long uniquePropertyId = 0;

                //if (policyStatusId == Utility.IllustrationStatus.MissingInspection.ID().ToInt())
                {
                    #region Set Inspection Date/Time
                    var horafull = txtFechaInspeccion.Text;
                    var ampmfull = horafull.Split(' ');
                    var array_hora = ampmfull[1].Split(':');
                    var ampm = ampmfull[2];
                    int hora = array_hora[0].ToInt(),
                        minutos = array_hora[1].ToInt(),
                        segundos = array_hora[2].ToInt();

                    if (ampm == "PM" && hora < 12)
                        hora += 12;

                    var InspectionDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hora, minutos, segundos);
                    #endregion

                    #region Get Unique Property Id
                    uniquePropertyId = Convert.ToInt64(ddlPropiedades.SelectedValue);
                    #endregion

                    #region Document Category
                    var document_category = ObjServices.oAlliedLinesReviewManager.GetDocumentCategory(new AlliedLines.Document.Category.Parameters.Get
                    {
                        NameKey = Utility.AlliedLinesInspectionFormPhotos
                    });
                    #endregion

                    #region Get Property
                    var property = ObjServices.oPropertyManager.GetProperty(new Property.Key
                    {
                        CorpId = ObjServices.Corp_Id,
                        RegionId = ObjServices.Region_Id,
                        CountryId = ObjServices.Country_Id,
                        DomesticregId = ObjServices.Domesticreg_Id,
                        StateProvId = ObjServices.State_Prov_Id,
                        CityId = ObjServices.City_Id,
                        OfficeId = ObjServices.Office_Id,
                        CaseSeqNo = ObjServices.Case_Seq_No,
                        HistSeqNo = ObjServices.Hist_Seq_No
                    }).FirstOrDefault(p => p.UniquePropertyId == uniquePropertyId);
                    #endregion

                    var norte = property.NorthBorderId;
                    var sur = property.SouthBorderId;
                    var este = property.EastBorderId;
                    var oeste = property.WestBorderId;

                    if (ddlColindanciaNorte.SelectedIndex > 0) { norte = ddlColindanciaNorte.SelectedValue.ToInt(); }
                    if (ddlColindanciaSur.SelectedIndex > 0) { sur = ddlColindanciaSur.SelectedValue.ToInt(); }
                    if (ddlColindanciaEste.SelectedIndex > 0) { este = ddlColindanciaEste.SelectedValue.ToInt(); }
                    if (ddlColindanciaOeste.SelectedIndex > 0) { oeste = ddlColindanciaOeste.SelectedValue.ToInt(); }

                    #region Set Property
                    var propiedad = ObjServices.oPropertyManager.SetProperty(new Property
                    {
                        CorpId = property.CorpId,
                        PropertyId = property.PropertyId,
                        RegionId = property.RegionId,
                        CountryId = property.CountryId,
                        DomesticregId = property.DomesticregId,
                        StateProvId = property.StateProvId,
                        CityId = property.CityId,
                        BusinessTypeId = property.BusinessTypeId,
                        PropertyBuildTypeId = ddlTipoconstruccion.SelectedValue.ToInt(),
                        ActivfityTypeId = property.ActivfityTypeId,
                        ReinsuranceId = property.ReinsuranceId,
                        ReinsuranceAmount = property.ReinsuranceAmount,
                        AddressStreet = txtUbicacionInspeccionada.Text.Trim(),
                        AddressNumber = txtNumero.Text.Trim(),
                        EvaluationValue = property.EvaluationValue,
                        EdificationValue = property.EdificationValue,
                        MachineryValue = property.MachineryValue,
                        FurnitureAndEquipmentValue = property.FurnitureAndEquipmentValue,
                        StockValue = property.StockValue,
                        RemodelingAndFittingValue = property.RemodelingAndFittingValue,
                        ValueObjectAndArtValue = property.ValueObjectAndArtValue,
                        Rooms = property.Rooms,
                        Bathrooms = property.Bathrooms,
                        LocationActivityTypeId = property.LocationActivityTypeId,
                        Registry = property.Registry,
                        PropertyYear = property.PropertyYear,
                        BuildAreaSqFeet = property.BuildAreaSqFeet,
                        BuildAreaSqMeters = property.BuildAreaSqMeters,
                        GeographicLimitation = property.GeographicLimitation,
                        SouthBorderId = sur,
                        NorthBorderId = norte,
                        EastBorderId = este,
                        WestBorderId = oeste,
                        PhysicalAddress = property.PhysicalAddress,
                        accidents = property.accidents,
                        Garage = property.Garage,
                        Pool = property.Pool,
                        DistanceKilometersSea = property.DistanceKilometersSea,
                        DistanceKilometersRiver = property.DistanceKilometersRiver,
                        DistanceKilometersStream = property.DistanceKilometersStream,
                        Longitude = property.Longitude,
                        Latitude = property.Latitude,
                        PropertyStatusId = property.PropertyStatusId,
                        UserId = property.UserId != null ? property.UserId.GetValueOrDefault() : ObjServices.UserID.GetValueOrDefault(),
                        SourceId = property.SourceId
                    });
                    #endregion

                    #region Review
                    var getReview = ObjServices.oAlliedLinesReviewManager.GetAlliedLineReview(new AlliedLines.Review.Parameters.Get
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
                        AlliedLineId = property.PropertyId,
                        UniqueAlliedLineId = uniquePropertyId,
                        AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                        ReviewId = null,
                        BlTypeId = property.BlTypeId,
                        BlId = property.BlId,
                        ProductId = property.ProductId
                    }).LastOrDefault();

                    if (getReview != null)
                    {
                        reviewId = getReview.ReviewId;
                        InspectionDate = getReview.InspectionDate;
                    }

                    var review = ObjServices.oAlliedLinesReviewManager.SetAlliedLineReview(new AlliedLines.Review.Parameters.Set
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
                        AlliedLineId = property.PropertyId,
                        UniqueAlliedLineId = uniquePropertyId,
                        AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                        BlTypeId = property.BlTypeId,
                        BlId = property.BlId,
                        ProductId = property.ProductId,
                        ReviewId = reviewId,
                        InspectionDate = InspectionDate,
                        RiskName = txtNombreRiesgo.Text,
                        InspectedLocation = txtUbicacionInspeccionada.Text,
                        RiskType = txtTipoRiesgo.Text,
                        InspectorId = ObjServices.InspectorAgentId.GetValueOrDefault(),
                        DocTypeId = document_category.DocTypeId,
                        DocCategoryId = document_category.DocCategoryId,
                        ReviewStatus = true,
                        UsrId = ObjServices.UserID.GetValueOrDefault()
                    });
                    #endregion

                    if (review != null)
                    {
                        #region Review Detail
                        var getReviewDetail = ObjServices.oAlliedLinesReviewManager.GetAlliedLineReviewDetail(new AlliedLines.Review.Detail.Parameters.Get
                        {
                            CorpId = review.CorpId,
                            RegionId = review.RegionId,
                            CountryId = review.CountryId,
                            DomesticregId = review.DomesticregId,
                            StateProvId = review.StateProvId,
                            CityId = review.CityId,
                            OfficeId = review.OfficeId,
                            CaseSeqNo = review.CaseSeqNo,
                            HistSeqNo = review.HistSeqNo,
                            AlliedLineId = review.AlliedLineId,
                            UniqueAlliedLineId = uniquePropertyId,
                            AlliedLineTypeId = review.AlliedLineTypeId,
                            ReviewId = review.ReviewId,
                            ReviewGroupId = null,
                            ReviewGroupEndorsementClarifying = null,
                            ReviewOptionEndorsementClarifying = null
                        }).ToList();



                        if (getReviewDetail.Count > 0)
                        {
                            var delReviewDetail = ObjServices.oAlliedLinesReviewManager.DelAlliedLineReviewDetail(new AlliedLines.Review.Detail.Del.Parameters.Set
                            {
                                CorpId = review.CorpId,
                                RegionId = review.RegionId,
                                CountryId = review.CountryId,
                                DomesticregId = review.DomesticregId,
                                StateProvId = review.StateProvId,
                                CityId = review.CityId,
                                OfficeId = review.OfficeId,
                                CaseSeqNo = review.CaseSeqNo,
                                HistSeqNo = review.HistSeqNo,
                                AlliedLineId = review.AlliedLineId,
                                UniqueAlliedLineId = uniquePropertyId,
                                AlliedLineTypeId = review.AlliedLineTypeId,
                                ReviewId = review.ReviewId
                            });


                        }

                        AlliedLines.Review.Detail.Result.Set detail = new AlliedLines.Review.Detail.Result.Set();
                        AlliedLines.Review.Detail.Parameters.Set parameter = new AlliedLines.Review.Detail.Parameters.Set();
                        parameter.CorpId = review.CorpId;
                        parameter.RegionId = review.RegionId;
                        parameter.CountryId = review.CountryId;
                        parameter.DomesticregId = review.DomesticregId;
                        parameter.StateProvId = review.StateProvId;
                        parameter.CityId = review.CityId;
                        parameter.OfficeId = review.OfficeId;
                        parameter.CaseSeqNo = review.CaseSeqNo;
                        parameter.HistSeqNo = review.HistSeqNo;
                        parameter.AlliedLineId = review.AlliedLineId;
                        parameter.UniqueAlliedLineId = uniquePropertyId;
                        parameter.AlliedLineTypeId = review.AlliedLineTypeId;
                        parameter.ReviewId = review.ReviewId;
                        parameter.ReviewDetailId = null;
                        parameter.Reviewtatus = true;
                        parameter.UserId = ObjServices.UserID.GetValueOrDefault();
                        parameter.UsuarioInspeccion = txtUsuarioInspeccion.Text;

                        foreach (var control in this.pnlGeneral.Controls)
                        {
                            int GroupId = 0,
                                ClassId = 0,
                                ItemId = 0,
                                OptionId = 0;

                            int ValueChecked = -1;

                            string[] grpclsitmopt = new string[] { };

                            string ValueText = string.Empty;

                            bool? Required = false;

                            bool _pnldosv = false;

                            if (control is TextBox)
                            {
                                #region TextBox
                                TextBox txt = control as TextBox;
                                if (txt.Attributes["grpclsitmopt"] != null && txt.Attributes["grpclsitmopt"] != "0|0|0|0")
                                {
                                    grpclsitmopt = txt.Attributes["grpclsitmopt"].Split('|');
                                    ValueChecked = -1;
                                    ValueText = txt.Text;

                                    if (grpclsitmopt[4].ToString().ToLower() == "n")
                                        Required = false;
                                    else if (grpclsitmopt[4].ToString().ToLower() == "y")
                                        Required = true;
                                    else
                                        Required = false;
                                }
                                #endregion
                            }
                            else if (control is RadioButtonList)
                            {
                                #region RadioButtonList
                                RadioButtonList rbl = control as RadioButtonList;
                                foreach (ListItem item in rbl.Items)
                                {
                                    if (item.Selected)
                                    {
                                        grpclsitmopt = item.Value.Split('|');
                                        if (grpclsitmopt.Length == 4)
                                        {
                                            ValueChecked = grpclsitmopt[3].ToInt();
                                            ValueText = string.Empty;
                                            Required = true;
                                            break;
                                        }
                                    }
                                }
                                #endregion
                            }
                            else if (control is DropDownList)
                            {
                                #region DropDownList
                                DropDownList ddl = control as DropDownList;
                                if (ddl.Attributes["GroupId"] != null)
                                {
                                    var array = string.Format("{0}|{1}|1|0", ddl.Attributes["GroupId"].ToString(),
                                                                             ddl.Attributes["ClassId"].ToString());
                                    grpclsitmopt = array.Split('|');
                                    ValueChecked = ddl.SelectedValue.ToInt();
                                    ValueText = ddl.SelectedItem.Text;
                                    Required = true;
                                }
                                #endregion
                            }
                            else if (control is Panel)
                            {
                                #region Panel
                                Panel pnl = control as Panel;
                                if (pnl.Visible)
                                {
                                    _pnldosv = true;

                                    foreach (var ctrl in pnl.Controls)
                                    {
                                        if (ctrl is TextBox)
                                        {
                                            #region TextBox
                                            TextBox txt = ctrl as TextBox;
                                            if (txt.Attributes["grpclsitmopt"] != null && txt.Attributes["grpclsitmopt"] != "0|0|0|0")
                                            {
                                                grpclsitmopt = txt.Attributes["grpclsitmopt"].Split('|');
                                                ValueChecked = -1;
                                                ValueText = txt.Text;

                                                if (grpclsitmopt[4].ToString().ToLower() == "n")
                                                    Required = false;
                                                else if (grpclsitmopt[4].ToString().ToLower() == "y")
                                                    Required = true;
                                                else
                                                    Required = false;
                                            }
                                            #endregion
                                        }
                                        else if (ctrl is RadioButtonList)
                                        {
                                            #region RadioButtonList
                                            RadioButtonList rbl = ctrl as RadioButtonList;
                                            foreach (ListItem item in rbl.Items)
                                            {
                                                if (item.Selected)
                                                {
                                                    grpclsitmopt = item.Value.Split('|');
                                                    if (grpclsitmopt.Length == 4)
                                                    {
                                                        ValueChecked = grpclsitmopt[3].ToInt();
                                                        ValueText = string.Empty;
                                                        Required = true;
                                                        break;
                                                    }
                                                }
                                            }
                                            #endregion
                                        }

                                        #region Guardar opciones paneles "República Dominicana" o "El Salvador"
                                        if (grpclsitmopt.Count() > 0 && (grpclsitmopt.Length >= 4))
                                        {
                                            GroupId = grpclsitmopt[0].ToInt();
                                            ClassId = grpclsitmopt[1].ToInt();
                                            ItemId = grpclsitmopt[2].ToInt();
                                            OptionId = grpclsitmopt[3].ToInt();

                                            parameter.ReviewGroupId = GroupId;
                                            parameter.ReviewClassId = ClassId;
                                            parameter.ReviewItemId = ItemId;
                                            parameter.ReviewOptionId = OptionId;
                                            parameter.ValueChecked = ValueChecked;
                                            parameter.ValueText = ValueText;
                                            parameter.Required = Required;

                                            detail = ObjServices.oAlliedLinesReviewManager.SetAlliedLineReviewDetail(parameter);
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #region Guardar opciones generales
                            if (!_pnldosv)
                            {
                                if (grpclsitmopt.Count() > 0 && (grpclsitmopt.Length >= 4))
                                {
                                    GroupId = grpclsitmopt[0].ToInt();
                                    ClassId = grpclsitmopt[1].ToInt();
                                    ItemId = grpclsitmopt[2].ToInt();
                                    OptionId = grpclsitmopt[3].ToInt();

                                    parameter.ReviewGroupId = GroupId;
                                    parameter.ReviewClassId = ClassId;
                                    parameter.ReviewItemId = ItemId;
                                    parameter.ReviewOptionId = OptionId;
                                    parameter.ValueChecked = ValueChecked;
                                    parameter.ValueText = ValueText;
                                    parameter.Required = Required;

                                    detail = ObjServices.oAlliedLinesReviewManager.SetAlliedLineReviewDetail(parameter);
                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region Review Photos
                        var pics = ObjServices.oAlliedLinesReviewManager.GetPolicyDocument(new AlliedLines.Document.Policy.Parameters.Get
                        {
                            CorpId = ObjServices.Corp_Id,
                            RegionId = ObjServices.Region_Id,
                            CountryId = ObjServices.Country_Id,
                            DomesticregId = ObjServices.Domesticreg_Id,
                            StateProvId = ObjServices.State_Prov_Id,
                            CityId = ObjServices.City_Id,
                            OfficeId = ObjServices.Office_Id,
                            CaseSeqNo = ObjServices.Case_Seq_No,
                            HistSeqNo = ObjServices.Hist_Seq_No
                        }).ToList();
                        if (pics.Count > 0)
                        {
                            foreach (var pic in pics)
                            {
                                var reviewPic = ObjServices.oAlliedLinesReviewManager.GetAlliedLineReviewPic(new AlliedLines.Review.Pic.Parameters.Get
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
                                    AlliedLineId = review.AlliedLineId,
                                    UniqueAlliedLineId = uniquePropertyId,
                                    AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                                    ReviewId = review.ReviewId,
                                    DocTypeId = pic.DocTypeId,
                                    DocCategoryId = pic.DocCategoryId,
                                    DocumentId = pic.DocumentId
                                }).FirstOrDefault();

                                int? pictureId = null;

                                if (reviewPic != null)
                                    pictureId = reviewPic.PictureId;

                                if (pictureId == null)
                                {
                                    var reviewPics = ObjServices.oAlliedLinesReviewManager.SetVehicleReviewPic(new AlliedLines.Review.Pic.Parameters.Set
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
                                        AlliedLineId = review.AlliedLineId,
                                        UniqueAlliedLineId = uniquePropertyId,
                                        AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                                        ReviewId = review.ReviewId,
                                        PictureId = pictureId,
                                        DocTypeId = pic.DocTypeId,
                                        DocCategoryId = pic.DocCategoryId,
                                        DocumentId = pic.DocumentId,
                                        PictureStatus = true,
                                        UsrId = ObjServices.UserID.GetValueOrDefault()
                                    });
                                }
                            }
                        }
                        #endregion



                        #region Set Property Insured
                        var insured = ObjServices.oPropertyManager.SetPropertyInsured(new Property.Insured.key
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
                            PropertyId = property.PropertyId,
                            InsuredDate = property.InsuredDate,
                            InsuredAmount = property.InsuredAmount,
                            PremiumAmount = property.PremiumAmount,
                            BasePremiumAmount = property.BasePremiumAmount,
                            DeductiblePercentage = property.DeductiblePercentage,
                            DeductibleAmount = property.DeductibleAmount,
                            PropertyInspectedValue = property.PropertyInspectedValue,
                            Inspection = true,
                            PolicyPropertyStatusId = property.PropertyStatusId,
                            UserId = property.UserId != null ? property.UserId.GetValueOrDefault() : ObjServices.UserID.GetValueOrDefault(),
                            SourceId = property.SourceId
                        });
                        #endregion

                        #region Update Property Insured Detail

                        #region Endoso Aclaratorio
                        bool endosoAclaratorioSumasAseguradas = false,
                             endosoAclaratorioDescripcion = false,
                             endosoAclaratorioDescripcionPeligros = false,
                             endosoAclaratorioPrevencionProteccion = false,
                             EndorsementClarifying = false;

                        endosoAclaratorioSumasAseguradas = (rblSumasAseguradasEdificio.SelectedItem != null && rblSumasAseguradasEdificio.SelectedItem.Text == Resources.NoLabel ||
                                                            rblSumasAseguradasMobiliarios.SelectedItem != null && rblSumasAseguradasMobiliarios.SelectedItem.Text == Resources.NoLabel ||
                                                            rblSumasAseguradasMaquinarias.SelectedItem != null && rblSumasAseguradasMaquinarias.SelectedItem.Text == Resources.NoLabel ||
                                                            rblSumasAseguradasExistencia.SelectedItem != null && rblSumasAseguradasExistencia.SelectedItem.Text == Resources.NoLabel);

                        endosoAclaratorioDescripcion = (rblTipoConstruccionNoSi.SelectedItem != null && rblTipoConstruccionNoSi.SelectedItem.Text == Resources.NoLabel);

                        endosoAclaratorioDescripcionPeligros = (rblAlmacenamientoUsoCombustible.SelectedItem != null && rblAlmacenamientoUsoCombustible.SelectedItem.Text == "Sí" ||
                                                                rblEdificacionCargaCombustible.SelectedItem != null && rblEdificacionCargaCombustible.SelectedItem.Text == "Sí" ||
                                                                rblSubastacionElectrica.SelectedItem != null && rblSubastacionElectrica.SelectedItem.Text == "Sí" ||
                                                                rblGeneradoresElectricos.SelectedItem != null && rblGeneradoresElectricos.SelectedItem.Text == "Sí" ||
                                                                rblCalderas.SelectedItem != null && rblCalderas.SelectedItem.Text == "Sí" ||
                                                                rblAireComprimido.SelectedItem != null && rblAireComprimido.SelectedItem.Text == "Sí" ||
                                                                rblPasillosLibres.SelectedItem != null && rblPasillosLibres.SelectedItem.Text == Resources.NoLabel ||
                                                                rblFluidosInflamables.SelectedItem != null && rblFluidosInflamables.SelectedItem.Text == "Sí" ||
                                                                rblOrdenLimpiezaDentroRiesgo.SelectedItem != null && (rblOrdenLimpiezaDentroRiesgo.SelectedItem.Text.StartsWith("R") ||
                                                                                                                      rblOrdenLimpiezaDentroRiesgo.SelectedItem.Text.StartsWith("M")) ||
                                                                rblOrdenLimpiezaFueraRiesgo.SelectedItem != null && (rblOrdenLimpiezaFueraRiesgo.SelectedItem.Text.StartsWith("R") ||
                                                                                                                     rblOrdenLimpiezaFueraRiesgo.SelectedItem.Text.StartsWith("M")) ||
                                                                rblOrdenLimpiezaGeneral.SelectedItem != null && (rblOrdenLimpiezaGeneral.SelectedItem.Text.StartsWith("R") ||
                                                                                                                 rblOrdenLimpiezaGeneral.SelectedItem.Text.StartsWith("M")) ||
                                                                rblInstalacionesElectricas.SelectedItem != null && (rblInstalacionesElectricas.SelectedItem.Text.StartsWith("R") ||
                                                                                                                    rblInstalacionesElectricas.SelectedItem.Text.StartsWith("M")));

                        endosoAclaratorioPrevencionProteccion = (rblManguerasContraIncendios.SelectedItem != null && rblManguerasContraIncendios.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblSenalesEmergencias.SelectedItem != null && rblSenalesEmergencias.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblBombasAaguaContraIncendio.SelectedItem != null && rblBombasAaguaContraIncendio.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblPararrayos.SelectedItem != null && rblPararrayos.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblTomaAguaBomberos.SelectedItem != null && rblTomaAguaBomberos.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblRociadoresAutomaticos.SelectedItem != null && rblRociadoresAutomaticos.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblBrigadaContraIncendios.SelectedItem != null && rblBrigadaContraIncendios.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblEscalerasEmergencias.SelectedItem != null && rblEscalerasEmergencias.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblSistemaContraIncendio.SelectedItem != null && rblSistemaContraIncendio.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblPuertasEnrollables.SelectedItem != null && rblPuertasEnrollables.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblSistemaAlarma.SelectedItem != null && rblSistemaAlarma.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblServicioMonitoreoCCTV.SelectedItem != null && rblServicioMonitoreoCCTV.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblCajaSeguridad.SelectedItem != null && rblCajaSeguridad.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblRejasVentanasPuertas.SelectedItem != null && rblRejasVentanasPuertas.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblPulsadoresManuales.SelectedItem != null && rblPulsadoresManuales.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblMantenimientoPreventivo.SelectedItem != null && rblMantenimientoPreventivo.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblAlmacenamiento.SelectedItem != null && rblAlmacenamiento.SelectedItem.Text == Resources.NoLabel ||
                                                                 rblAlmacenamientoBRM.SelectedItem != null && (rblAlmacenamientoBRM.SelectedItem.Text.StartsWith("R") || rblAlmacenamientoBRM.SelectedItem.Text.StartsWith("M")) ||
                                                                 rblVigilantes.SelectedItem != null && rblVigilantes.SelectedItem.Text == Resources.NoLabel);

                        EndorsementClarifying = (endosoAclaratorioSumasAseguradas ||
                                                 endosoAclaratorioDescripcion ||
                                                 endosoAclaratorioDescripcionPeligros ||
                                                 endosoAclaratorioPrevencionProteccion);
                        #endregion

                        var update = ObjServices.oPropertyManager.UpdatePropertyInsuredDetail(new Property.Insured.Detail.key
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
                            PropertyId = property.PropertyId,
                            SeqId = property.SeqId,
                            UniquePropertyId = property.UniquePropertyId,
                            Inspected = false,
                            EndorsementClarifying = EndorsementClarifying,
                            UserId = property.UserId != null ? property.UserId : ObjServices.UserID
                        });
                        #endregion

                        FillData();
                        ddlPropiedades.SelectedValue = uniquePropertyId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.MessageBoxALIF(string.Format("{0}<br /><br /><b>InnerException:</b> {1}", ex.Message, ex.InnerException), null, null, true, Resources.Warning);
                else
                    this.MessageBoxALIF(ex.Message, null, null, true, Resources.Warning);
            }
        }

        private void SavePicture(AlliedLines.Document.Category.Result.Get document_category, DateTime InspectionDate, long uniquePropertyId, Property property)
        {
            bool saved = false;

            int? reviewId = null;

            try
            {
                #region Review
                var getReview = ObjServices.oAlliedLinesReviewManager.GetAlliedLineReview(new AlliedLines.Review.Parameters.Get
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
                    AlliedLineId = property.PropertyId,
                    UniqueAlliedLineId = uniquePropertyId,
                    AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                    ReviewId = null,
                    BlTypeId = property.BlTypeId,
                    BlId = property.BlId,
                    ProductId = property.ProductId
                }).LastOrDefault();

                if (getReview != null)
                {
                    reviewId = getReview.ReviewId;
                    InspectionDate = getReview.InspectionDate;
                }

                var review = ObjServices.oAlliedLinesReviewManager.SetAlliedLineReview(new AlliedLines.Review.Parameters.Set
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
                    AlliedLineId = property.PropertyId,
                    UniqueAlliedLineId = uniquePropertyId,
                    AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                    BlTypeId = property.BlTypeId,
                    BlId = property.BlId,
                    ProductId = property.ProductId,
                    ReviewId = reviewId,
                    InspectionDate = InspectionDate,
                    RiskName = txtNombreRiesgo.Text,
                    InspectedLocation = string.Concat(txtUbicacionInspeccionada.Text, " ", txtNumero.Text.NTrim()),
                    RiskType = txtTipoRiesgo.Text,
                    InspectorId = ObjServices.InspectorAgentId.GetValueOrDefault(),
                    DocTypeId = document_category.DocTypeId,
                    DocCategoryId = document_category.DocCategoryId,
                    ReviewStatus = true,
                    UsrId = ObjServices.UserID.GetValueOrDefault()
                });
                #endregion

                if (review != null)
                {

                    ViewState["ReviewId"] = review.ReviewId;
                    ViewState["AlliedLineId"] = review.AlliedLineId;
                    ViewState["UniqueAlliedLineId"] = uniquePropertyId;

                    #region Review Photos
                    var pics = ObjServices.oAlliedLinesReviewManager.GetPolicyDocument(new AlliedLines.Document.Policy.Parameters.Get
                    {
                        CorpId = ObjServices.Corp_Id,
                        RegionId = ObjServices.Region_Id,
                        CountryId = ObjServices.Country_Id,
                        DomesticregId = ObjServices.Domesticreg_Id,
                        StateProvId = ObjServices.State_Prov_Id,
                        CityId = ObjServices.City_Id,
                        OfficeId = ObjServices.Office_Id,
                        CaseSeqNo = ObjServices.Case_Seq_No,
                        HistSeqNo = ObjServices.Hist_Seq_No
                    }).ToList();
                    if (pics.Count > 0)
                    {
                        foreach (var pic in pics)
                        {
                            var reviewPic = ObjServices.oAlliedLinesReviewManager.GetAlliedLineReviewPic(new AlliedLines.Review.Pic.Parameters.Get
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
                                AlliedLineId = review.AlliedLineId,
                                UniqueAlliedLineId = uniquePropertyId,
                                AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                                ReviewId = review.ReviewId,
                                DocTypeId = pic.DocTypeId,
                                DocCategoryId = pic.DocCategoryId,
                                DocumentId = pic.DocumentId
                            }).FirstOrDefault();

                            int? pictureId = null;

                            if (reviewPic != null)
                                pictureId = reviewPic.PictureId;

                            if (pictureId == null)
                            {
                                var reviewPics = ObjServices.oAlliedLinesReviewManager.SetVehicleReviewPic(new AlliedLines.Review.Pic.Parameters.Set
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
                                    AlliedLineId = review.AlliedLineId,
                                    UniqueAlliedLineId = uniquePropertyId,
                                    AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                                    ReviewId = review.ReviewId,
                                    PictureId = pictureId,
                                    DocTypeId = pic.DocTypeId,
                                    DocCategoryId = pic.DocCategoryId,
                                    DocumentId = pic.DocumentId,
                                    PictureStatus = true,
                                    UsrId = ObjServices.UserID.GetValueOrDefault()
                                });

                                saved = true;
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception) { saved = false; }

            Session["PhotoSaved"] = saved;
        }

        public void edit()
        {
            throw new NotImplementedException();
        }

        public void FillData()
        {
            try
            {
                Policy policy = ObjServices.oPolicyManager.GetPolicy(ObjServices.Corp_Id,
                                                                     ObjServices.Region_Id,
                                                                     ObjServices.Country_Id,
                                                                     ObjServices.Domesticreg_Id,
                                                                     ObjServices.State_Prov_Id,
                                                                     ObjServices.City_Id,
                                                                     ObjServices.Office_Id,
                                                                     ObjServices.Case_Seq_No,
                                                                     ObjServices.Hist_Seq_No);

                txtPolizaNo.Text = ObjServices.Policy_Id = policy.PolicyNo;
                txtFechaInspeccion.Text = Convert.ToString(ViewState["txtFechaInspeccion"]);
                ViewState["PolicyStatusId"] = policy.PolicyStatusId;



                var properties = ObjServices.oPropertyManager.GetProperty(new Property.Key
                {
                    CorpId = ObjServices.Corp_Id,
                    RegionId = ObjServices.Region_Id,
                    CountryId = ObjServices.Country_Id,
                    DomesticregId = ObjServices.Domesticreg_Id,
                    StateProvId = ObjServices.State_Prov_Id,
                    CityId = ObjServices.City_Id,
                    OfficeId = ObjServices.Office_Id,
                    CaseSeqNo = ObjServices.Case_Seq_No,
                    HistSeqNo = ObjServices.Hist_Seq_No
                }).Where(p => p.RequiresInspection && p.PropertyStatusId == Utility.PropertyStatus.Active.ToInt()).ToList();


                if (properties.Count > 0)
                {

                    var lstPropiedades = new Dictionary<long, string>();
                    lstPropiedades.Add(0, Resources.Select);

                    string Address = string.Empty;
                    foreach (var item in properties)
                    {

                        if (item.AddressStreet.Trim().Length > 0)
                        {
                            Address = item.AddressStreet.Trim();
                        }
                        else
                        {
                            Address = string.Concat(Resources.ItemWithoutLocation, ", ", item.UniqueId);
                        }

                        if (item.AddressNumber.Trim().Length > 0)
                            Address += string.Concat((Address.Trim().Length > 0 ? ", " : string.Empty), item.AddressNumber.Trim());

                        lstPropiedades.Add(item.UniquePropertyId, Address);
                    }

                    ddlPropiedades.Items.Clear();
                    ddlPropiedades.DataSource = lstPropiedades;
                    ddlPropiedades.DataTextField = "Value";
                    ddlPropiedades.DataValueField = "Key";
                    ddlPropiedades.DataBind();
                    ddlPropiedades.SelectedIndex = ddlPropiedades.Items.Count == 2 ? 1 : 0;

                    //txtUbicacionInspeccionada.Text = ddlPropiedades.Items.Count == 2 ? Address : string.Empty;
                    //this.ExcecuteJScript("setUbicacionInspeccionada('" + (ddlPropiedades.Items.Count == 2 ? Address : string.Empty) + "');");

                    #region Colindancias
                    var colindancias = ObjServices.oDropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = Utility.DropDownType.Boundary.ToString() }).ToList();
                    if (colindancias.Count > 0)
                    {
                        #region Norte
                        ddlColindanciaNorte.Items.Clear();
                        ddlColindanciaNorte.DataTextField = "StateProvDesc";
                        ddlColindanciaNorte.DataValueField = "RegionId";
                        ddlColindanciaNorte.DataSource = colindancias;
                        ddlColindanciaNorte.DataBind();
                        ddlColindanciaNorte.Items.Insert(0, new ListItem(Resources.Select, "0"));
                        #endregion

                        #region Sur
                        ddlColindanciaSur.Items.Clear();
                        ddlColindanciaSur.DataTextField = "StateProvDesc";
                        ddlColindanciaSur.DataValueField = "RegionId";
                        ddlColindanciaSur.DataSource = colindancias;
                        ddlColindanciaSur.DataBind();
                        ddlColindanciaSur.Items.Insert(0, new ListItem(Resources.Select, "0"));
                        #endregion

                        #region Este
                        ddlColindanciaEste.Items.Clear();
                        ddlColindanciaEste.DataTextField = "StateProvDesc";
                        ddlColindanciaEste.DataValueField = "RegionId";
                        ddlColindanciaEste.DataSource = colindancias;
                        ddlColindanciaEste.DataBind();
                        ddlColindanciaEste.Items.Insert(0, new ListItem(Resources.Select, "0"));
                        #endregion

                        #region Oeste
                        ddlColindanciaOeste.Items.Clear();
                        ddlColindanciaOeste.DataTextField = "StateProvDesc";
                        ddlColindanciaOeste.DataValueField = "RegionId";
                        ddlColindanciaOeste.DataSource = colindancias;
                        ddlColindanciaOeste.DataBind();
                        ddlColindanciaOeste.Items.Insert(0, new ListItem(Resources.Select, "0"));
                        #endregion
                    }
                    #endregion

                    #region Tipos Construccion
                    var TipoConstruccion = ObjServices.oDropDownManager.GetDropDownByType(new DropDown.Parameter { DropDownType = Utility.DropDownType.BuildType.ToString(), CorpId = ObjServices.Corp_Id }).ToList();
                    if (TipoConstruccion.Count > 0)
                    {

                        ddlTipoconstruccion.Items.Clear();
                        ddlTipoconstruccion.DataTextField = "ElementDesc";
                        ddlTipoconstruccion.DataValueField = "ElementId";
                        ddlTipoconstruccion.DataSource = TipoConstruccion;
                        ddlTipoconstruccion.DataBind();
                        ddlTipoconstruccion.Items.Insert(0, new ListItem(Resources.Select, "0"));
                    }
                    #endregion

                    propiedadesIndexChanged();
                }

                #region InspectorName
                var quoTemp = ObjServices.oPolicyManager.GetQuotationInfoTemp(new Policy.Quo.Temp
                {
                    PolicyNo = policy.PolicyNo
                }).ToList();
                if (quoTemp.Any())
                {
                    var data = quoTemp.FirstOrDefault();
                    txtInspeccionadoPor.Text = data.InspectorName;
                    ObjServices.InspectorName = data.InspectorName;
                    ObjServices.InspectorAgentId = data.InspectorAgentId;
                }
                #endregion

                txtIntermediario.Text = policy.Agent_Name;
                txtUsuarioInspeccion.Text = ObjServices.IsInspectorQuotRole ? ObjServices.InspectorName : ObjServices.UserFullName;

                var dataSign = ObjServices.oPolicyManager.GetCustomerSing
                    (
                        ObjServices.Corp_Id,
                        ObjServices.Region_Id,
                        ObjServices.Country_Id,
                        ObjServices.Domesticreg_Id,
                        ObjServices.State_Prov_Id,
                        ObjServices.City_Id,
                        ObjServices.Office_Id,
                        ObjServices.Case_Seq_No,
                        ObjServices.Hist_Seq_No
                    );


                var HasSign = dataSign != null;
                hdnHasSign.Value = HasSign.ToString().ToLower();
                if (HasSign)
                    hdnCustomerSign.Value = dataSign;
                                
                if (ObjServices.InspectorAgentId != null && ObjServices.InspectorAgentId > 0)
                {
                    drpInspectors.SelectedValue = Convert.ToString(ObjServices.InspectorAgentId);
                    drpInspectors.Enabled = false;
                }

                this.ExcecuteJScript(string.Format("setUserInspection('{0}');", ObjServices.IsInspectorQuotRole ? ObjServices.InspectorName : ObjServices.UserFullName));
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.MessageBoxALIF(string.Format("{0}<br /><br /><b>InnerException:</b> {1}", ex.Message, ex.InnerException), null, null, true, Resources.Warning);
                else
                    this.MessageBoxALIF(ex.Message, null, null, true, Resources.Warning);
            }
        }

        public void Initialize()
        {
            Session["PhotoSaved"] = null;
            Session["fuFotografia"] = null;

            RadioButtonListSetDataSource();
            FillDrop();
        }

        public void ClearData()
        {
            ViewState["txtFechaInspeccion"] = System.DateTime.Now.ToString("dd-MMM-yyyy");// +" " + ampm;
            ClearData(false);
            setTextBox(false);
        }

        private void ClearData(bool enabled)
        {
            this.ExcecuteJScript("clearControls(" + enabled.ToString().ToLower() + ");");
            setTextBox(enabled);
        }

        private Policy.Contact Contact()
        {

            var contact = ObjServices.oPolicyManager.GetContactPolicy(ObjServices.Corp_Id,
                                                                      ObjServices.Region_Id,
                                                                      ObjServices.Country_Id,
                                                                      ObjServices.Domesticreg_Id,
                                                                      ObjServices.State_Prov_Id,
                                                                      ObjServices.City_Id,
                                                                      ObjServices.Office_Id,
                                                                      ObjServices.Case_Seq_No,
                                                                      ObjServices.Hist_Seq_No,
                                                                      null,
                                                                      null).FirstOrDefault();
            return contact;
        }

        private void setTextBox(bool enabled)
        {
            foreach (var control in this.pnlGeneral.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txt = control as TextBox;
                    if (txt.ID != "txtIntermediario" && txt.ID != "txtInspeccionadoPor" && txt.ID != "txtTipoRiesgo")
                        txt.Clear();
                    if (txt.ID == "txtUsuarioInspeccion") { txt.Enabled = true; }
                    else
                    {
                        txt.Enabled = enabled;
                    }
                    if (txt.ID == "txtPolizaNo")
                        txt.Text = ObjServices.Policy_Id;

                    if (txt.ID == "txtFechaInspeccion")
                        txt.Text = Convert.ToString(ViewState["txtFechaInspeccion"]);

                    if (setTextBoxDisabled.Contains(txt.ID))
                        txt.Enabled = false;
                }
            }
        }

        private void RadioButtonListSetDataSource()
        {
            #region No/Yes Options
            Dictionary<int, string> NoYesOptions = new Dictionary<int, string> 
            {
                { 6, Resources.NoLabel },
                { 5, Resources.YesLabel.Replace("i","í") } //"Sí"
            };
            #endregion

            #region High/Medium/Low Options
            Dictionary<int, string> HighMediumLowOptions = new Dictionary<int, string> 
            {
                { 1, Resources.High },
                { 2, Resources.Medium },
                { 3, Resources.Low }
            };
            #endregion

            #region Good/Regular/Bad Options
            Dictionary<int, string> GoodRegularBadOptions = new Dictionary<int, string> 
            {
                { 1, Resources.Good },
                { 2, "Regular" },
                { 3, Resources.Bad }
            };
            #endregion

            #region DistanceOptions
            Dictionary<int, string> DistanceOptions = new Dictionary<int, string> 
            {
                { 1, Resources.Less500Meters }, //"< de 500 metros" }
                { 2, Resources.More500Meters }  //"> de 500 metros" }
            };
            #endregion

            #region Movimiento Comercial
            rblMovimientoComercial.Items.Clear();
            rblMovimientoComercial.DataValueField = "Key";
            rblMovimientoComercial.DataTextField = "Value";
            rblMovimientoComercial.DataSource = GoodRegularBadOptions;
            rblMovimientoComercial.DataBind();
            #endregion

            #region Organizacion Contable
            rblOrganizacionContable.Items.Clear();
            rblOrganizacionContable.DataValueField = "Key";
            rblOrganizacionContable.DataTextField = "Value";
            rblOrganizacionContable.DataSource = NoYesOptions;
            rblOrganizacionContable.DataBind();
            #endregion

            #region Sumas Aseguradas
            rblSumasAseguradasEdificio.Items.Clear();
            rblSumasAseguradasEdificio.DataValueField = "Key";
            rblSumasAseguradasEdificio.DataTextField = "Value";
            rblSumasAseguradasEdificio.DataSource = NoYesOptions;
            rblSumasAseguradasEdificio.DataBind();

            rblSumasAseguradasMobiliarios.Items.Clear();
            rblSumasAseguradasMobiliarios.DataValueField = "Key";
            rblSumasAseguradasMobiliarios.DataTextField = "Value";
            rblSumasAseguradasMobiliarios.DataSource = NoYesOptions;
            rblSumasAseguradasMobiliarios.DataBind();

            rblSumasAseguradasMaquinarias.Items.Clear();
            rblSumasAseguradasMaquinarias.DataValueField = "Key";
            rblSumasAseguradasMaquinarias.DataTextField = "Value";
            rblSumasAseguradasMaquinarias.DataSource = NoYesOptions;
            rblSumasAseguradasMaquinarias.DataBind();

            rblSumasAseguradasExistencia.Items.Clear();
            rblSumasAseguradasExistencia.DataValueField = "Key";
            rblSumasAseguradasExistencia.DataTextField = "Value";
            rblSumasAseguradasExistencia.DataSource = NoYesOptions;
            rblSumasAseguradasExistencia.DataBind();
            #endregion

            #region Edificio
            Dictionary<int, string> Edificio = new Dictionary<int, string> 
            {
                { 1, Resources.Own },
                { 2, Resources.Rented }
            };
            rblEdificio.Items.Clear();
            rblEdificio.DataValueField = "Key";
            rblEdificio.DataTextField = "Value";
            rblEdificio.DataSource = Edificio;
            rblEdificio.DataBind();
            #endregion

            #region Descripcion
            #region Tipo Edificio
            Dictionary<int, string> TipoEdificio = new Dictionary<int, string> 
            {
                { 1, Resources.Commerce },
                { 2, Resources.FactoryIndustry },
                { 3, Resources.Bureau },
                { 4, Resources.LivingPlace },
                { 5, Resources.Mall }
            };
            rblTipoEdificio.Items.Clear();
            rblTipoEdificio.DataValueField = "Key";
            rblTipoEdificio.DataTextField = "Value";
            rblTipoEdificio.DataSource = TipoEdificio;
            rblTipoEdificio.DataBind();
            #endregion

            #region Tipo Construccion
            rblTipoConstruccionNoSi.Items.Clear();
            rblTipoConstruccionNoSi.DataValueField = "Key";
            rblTipoConstruccionNoSi.DataTextField = "Value";
            rblTipoConstruccionNoSi.DataSource = NoYesOptions;
            rblTipoConstruccionNoSi.DataBind();
            #endregion
            #endregion

            #region ¿Ha tenido pérdidas?
            rblHaTenidoPerdidas.Items.Clear();
            rblHaTenidoPerdidas.DataValueField = "Key";
            rblHaTenidoPerdidas.DataTextField = "Value";
            rblHaTenidoPerdidas.DataSource = NoYesOptions;
            rblHaTenidoPerdidas.DataBind();
            #endregion

            #region Nivel de Pérdidas
            Dictionary<int, string> NivelPerdidas = new Dictionary<int, string> 
            {
                { 1, Resources.High },
                { 2, Resources.Medium },
                { 3, Resources.Low },
                { 4, Resources.NoneOfTheAbove }
            };
            rblNivelPerdidas.Items.Clear();
            rblNivelPerdidas.DataValueField = "Key";
            rblNivelPerdidas.DataTextField = "Value";
            rblNivelPerdidas.DataSource = NivelPerdidas;
            rblNivelPerdidas.DataBind();
            #endregion

            #region Causa de la pérdida
            Dictionary<int, string> CausaPerdida = new Dictionary<int, string> 
            {
                { 1, Resources.Fire },
                { 2, Resources.Thunderbolt },
                { 3, Resources.Explosion },
                { 4, Resources.MaliciousDamage },
                { 5, Resources.Riot },
                { 6, Resources.Strike },
                { 7, Resources.Flood },
                { 8, Resources.RainwaterDamage },
                { 9, Resources.TheftWithScaling },
                { 10, Resources.TheftWithViolence },
                { 11, Resources.Hurricane },
                { 12, Resources.AccidentalWaterDamage }
            };

            if (ObjServices.Country == Utility.Country.ElSalvador)
                CausaPerdida.Remove(9);

            rblCausaPerdida.Items.Clear();
            rblCausaPerdida.DataValueField = "Key";
            rblCausaPerdida.DataTextField = "Value";
            rblCausaPerdida.DataSource = CausaPerdida;
            rblCausaPerdida.DataBind();
            #endregion

            #region Siniestralidad Zona
            rblSiniestralidadZona.Items.Clear();
            rblSiniestralidadZona.DataValueField = "Key";
            rblSiniestralidadZona.DataTextField = "Value";
            rblSiniestralidadZona.DataSource = HighMediumLowOptions;
            rblSiniestralidadZona.DataBind();
            #endregion

            #region Almacenamiento Uso Combustible
            rblAlmacenamientoUsoCombustible.Items.Clear();
            rblAlmacenamientoUsoCombustible.DataValueField = "Key";
            rblAlmacenamientoUsoCombustible.DataTextField = "Value";
            rblAlmacenamientoUsoCombustible.DataSource = NoYesOptions;
            rblAlmacenamientoUsoCombustible.DataBind();
            #endregion

            #region Tipo Combustible
            Dictionary<int, string> TipoCombustible = new Dictionary<int, string> 
            {
                { 1, Resources.Gasoline },
                { 2, "Gasoil" },
                { 3, "GLP" }
            };
            rbTipoCombustible.Items.Clear();
            rbTipoCombustible.DataValueField = "Key";
            rbTipoCombustible.DataTextField = "Value";
            rbTipoCombustible.DataSource = TipoCombustible;
            rbTipoCombustible.DataBind();
            #endregion

            #region Edificacion Carga Combustible
            rblEdificacionCargaCombustible.Items.Clear();
            rblEdificacionCargaCombustible.DataValueField = "Key";
            rblEdificacionCargaCombustible.DataTextField = "Value";
            rblEdificacionCargaCombustible.DataSource = NoYesOptions;
            rblEdificacionCargaCombustible.DataBind();
            #endregion

            #region Carga Combustible Como Es
            rblCargaCombustibleComoEs.Items.Clear();
            rblCargaCombustibleComoEs.DataValueField = "Key";
            rblCargaCombustibleComoEs.DataTextField = "Value";
            rblCargaCombustibleComoEs.DataSource = HighMediumLowOptions;
            rblCargaCombustibleComoEs.DataBind();
            #endregion

            #region Subastacion Electrica
            rblSubastacionElectrica.Items.Clear();
            rblSubastacionElectrica.DataValueField = "Key";
            rblSubastacionElectrica.DataTextField = "Value";
            rblSubastacionElectrica.DataSource = NoYesOptions;
            rblSubastacionElectrica.DataBind();
            #endregion

            #region Generadores Eléctricos
            rblGeneradoresElectricos.Items.Clear();
            rblGeneradoresElectricos.DataValueField = "Key";
            rblGeneradoresElectricos.DataTextField = "Value";
            rblGeneradoresElectricos.DataSource = NoYesOptions;
            rblGeneradoresElectricos.DataBind();
            #endregion

            #region Calderas
            rblCalderas.Items.Clear();
            rblCalderas.DataValueField = "Key";
            rblCalderas.DataTextField = "Value";
            rblCalderas.DataSource = NoYesOptions;
            rblCalderas.DataBind();
            #endregion

            #region Aire Comprimido
            rblAireComprimido.Items.Clear();
            rblAireComprimido.DataValueField = "Key";
            rblAireComprimido.DataTextField = "Value";
            rblAireComprimido.DataSource = NoYesOptions;
            rblAireComprimido.DataBind();
            #endregion

            #region Pasillos Libres
            rblPasillosLibres.Items.Clear();
            rblPasillosLibres.DataValueField = "Key";
            rblPasillosLibres.DataTextField = "Value";
            rblPasillosLibres.DataSource = NoYesOptions;
            rblPasillosLibres.DataBind();
            #endregion

            #region Fluidos Inflamables
            rblFluidosInflamables.Items.Clear();
            rblFluidosInflamables.DataValueField = "Key";
            rblFluidosInflamables.DataTextField = "Value";
            rblFluidosInflamables.DataSource = NoYesOptions;
            rblFluidosInflamables.DataBind();
            #endregion

            #region Orden Limpieza Dentro Riesgo
            rblOrdenLimpiezaDentroRiesgo.Items.Clear();
            rblOrdenLimpiezaDentroRiesgo.DataValueField = "Key";
            rblOrdenLimpiezaDentroRiesgo.DataTextField = "Value";
            rblOrdenLimpiezaDentroRiesgo.DataSource = GoodRegularBadOptions;
            rblOrdenLimpiezaDentroRiesgo.DataBind();
            #endregion

            #region Orden Limpieza Fuera Riesgo
            rblOrdenLimpiezaFueraRiesgo.Items.Clear();
            rblOrdenLimpiezaFueraRiesgo.DataValueField = "Key";
            rblOrdenLimpiezaFueraRiesgo.DataTextField = "Value";
            rblOrdenLimpiezaFueraRiesgo.DataSource = GoodRegularBadOptions;
            rblOrdenLimpiezaFueraRiesgo.DataBind();
            #endregion

            #region Orden Limpieza General
            rblOrdenLimpiezaGeneral.Items.Clear();
            rblOrdenLimpiezaGeneral.DataValueField = "Key";
            rblOrdenLimpiezaGeneral.DataTextField = "Value";
            rblOrdenLimpiezaGeneral.DataSource = GoodRegularBadOptions;
            rblOrdenLimpiezaGeneral.DataBind();
            #endregion

            #region Instalaciones Electricas
            rblInstalacionesElectricas.Items.Clear();
            rblInstalacionesElectricas.DataValueField = "Key";
            rblInstalacionesElectricas.DataTextField = "Value";
            rblInstalacionesElectricas.DataSource = GoodRegularBadOptions;
            rblInstalacionesElectricas.DataBind();
            #endregion

            #region Mangueras Contra Incendios
            rblManguerasContraIncendios.Items.Clear();
            rblManguerasContraIncendios.DataValueField = "Key";
            rblManguerasContraIncendios.DataTextField = "Value";
            rblManguerasContraIncendios.DataSource = NoYesOptions;
            rblManguerasContraIncendios.DataBind();
            #endregion

            #region Señales de emergencias
            rblSenalesEmergencias.Items.Clear();
            rblSenalesEmergencias.DataValueField = "Key";
            rblSenalesEmergencias.DataTextField = "Value";
            rblSenalesEmergencias.DataSource = NoYesOptions;
            rblSenalesEmergencias.DataBind();
            #endregion

            #region Bombas de agua contra incendio
            rblBombasAaguaContraIncendio.Items.Clear();
            rblBombasAaguaContraIncendio.DataValueField = "Key";
            rblBombasAaguaContraIncendio.DataTextField = "Value";
            rblBombasAaguaContraIncendio.DataSource = NoYesOptions;
            rblBombasAaguaContraIncendio.DataBind();
            #endregion

            #region Pararrayos
            rblPararrayos.Items.Clear();
            rblPararrayos.DataValueField = "Key";
            rblPararrayos.DataTextField = "Value";
            rblPararrayos.DataSource = NoYesOptions;
            rblPararrayos.DataBind();
            #endregion

            #region Toma de agua para bomberos
            rblTomaAguaBomberos.Items.Clear();
            rblTomaAguaBomberos.DataValueField = "Key";
            rblTomaAguaBomberos.DataTextField = "Value";
            rblTomaAguaBomberos.DataSource = NoYesOptions;
            rblTomaAguaBomberos.DataBind();
            #endregion

            #region Rociadores automáticos
            rblRociadoresAutomaticos.Items.Clear();
            rblRociadoresAutomaticos.DataValueField = "Key";
            rblRociadoresAutomaticos.DataTextField = "Value";
            rblRociadoresAutomaticos.DataSource = NoYesOptions;
            rblRociadoresAutomaticos.DataBind();
            #endregion

            #region Brigada contra incendios
            rblBrigadaContraIncendios.Items.Clear();
            rblBrigadaContraIncendios.DataValueField = "Key";
            rblBrigadaContraIncendios.DataTextField = "Value";
            rblBrigadaContraIncendios.DataSource = NoYesOptions;
            rblBrigadaContraIncendios.DataBind();
            #endregion

            #region Escaleras de emergencias
            rblEscalerasEmergencias.Items.Clear();
            rblEscalerasEmergencias.DataValueField = "Key";
            rblEscalerasEmergencias.DataTextField = "Value";
            rblEscalerasEmergencias.DataSource = NoYesOptions;
            rblEscalerasEmergencias.DataBind();
            #endregion

            #region Sistema contra incendio
            rblSistemaContraIncendio.Items.Clear();
            rblSistemaContraIncendio.DataValueField = "Key";
            rblSistemaContraIncendio.DataTextField = "Value";
            rblSistemaContraIncendio.DataSource = NoYesOptions;
            rblSistemaContraIncendio.DataBind();
            #endregion

            #region Puertas enrollables
            rblPuertasEnrollables.Items.Clear();
            rblPuertasEnrollables.DataValueField = "Key";
            rblPuertasEnrollables.DataTextField = "Value";
            rblPuertasEnrollables.DataSource = NoYesOptions;
            rblPuertasEnrollables.DataBind();
            #endregion

            #region Sistema de alarma
            rblSistemaAlarma.Items.Clear();
            rblSistemaAlarma.DataValueField = "Key";
            rblSistemaAlarma.DataTextField = "Value";
            rblSistemaAlarma.DataSource = NoYesOptions;
            rblSistemaAlarma.DataBind();
            #endregion

            #region Servicio de monitoreo CCTV
            rblServicioMonitoreoCCTV.Items.Clear();
            rblServicioMonitoreoCCTV.DataValueField = "Key";
            rblServicioMonitoreoCCTV.DataTextField = "Value";
            rblServicioMonitoreoCCTV.DataSource = NoYesOptions;
            rblServicioMonitoreoCCTV.DataBind();
            #endregion

            #region Caja de seguridad
            rblCajaSeguridad.Items.Clear();
            rblCajaSeguridad.DataValueField = "Key";
            rblCajaSeguridad.DataTextField = "Value";
            rblCajaSeguridad.DataSource = NoYesOptions;
            rblCajaSeguridad.DataBind();
            #endregion

            #region Rejas en ventanas y/o puertas
            rblRejasVentanasPuertas.Items.Clear();
            rblRejasVentanasPuertas.DataValueField = "Key";
            rblRejasVentanasPuertas.DataTextField = "Value";
            rblRejasVentanasPuertas.DataSource = NoYesOptions;
            rblRejasVentanasPuertas.DataBind();
            #endregion

            #region Pulsadores manuales
            rblPulsadoresManuales.Items.Clear();
            rblPulsadoresManuales.DataValueField = "Key";
            rblPulsadoresManuales.DataTextField = "Value";
            rblPulsadoresManuales.DataSource = NoYesOptions;
            rblPulsadoresManuales.DataBind();
            #endregion

            #region Mantenimiento preventivo
            rblMantenimientoPreventivo.Items.Clear();
            rblMantenimientoPreventivo.DataValueField = "Key";
            rblMantenimientoPreventivo.DataTextField = "Value";
            rblMantenimientoPreventivo.DataSource = NoYesOptions;
            rblMantenimientoPreventivo.DataBind();
            #endregion

            #region Almacenamiento
            rblAlmacenamiento.Items.Clear();
            rblAlmacenamiento.DataValueField = "Key";
            rblAlmacenamiento.DataTextField = "Value";
            rblAlmacenamiento.DataSource = NoYesOptions;
            rblAlmacenamiento.DataBind();

            rblAlmacenamientoBRM.Items.Clear();
            rblAlmacenamientoBRM.DataValueField = "Key";
            rblAlmacenamientoBRM.DataTextField = "Value";
            rblAlmacenamientoBRM.DataSource = GoodRegularBadOptions;
            rblAlmacenamientoBRM.DataBind();
            #endregion

            #region Alimentación eléctrica
            Dictionary<int, string> AlimentacionElectrica = new Dictionary<int, string> 
            {
                { 1, Resources.Public },
                { 2, Resources.Private },
                { 3, Resources.Own }
            };
            rblAlimentacionElectrica.Items.Clear();
            rblAlimentacionElectrica.DataValueField = "Key";
            rblAlimentacionElectrica.DataTextField = "Value";
            rblAlimentacionElectrica.DataSource = AlimentacionElectrica;
            rblAlimentacionElectrica.DataBind();
            #endregion

            #region Tipo Suministro de agua
            Dictionary<int, string> TipoSuministroAgua = new Dictionary<int, string> 
            {
                { 1, Resources.PublicNetwork },
                { 2, Resources.WaterWell }
            };
            rblTipoSuministroAgua.Items.Clear();
            rblTipoSuministroAgua.DataValueField = "Key";
            rblTipoSuministroAgua.DataTextField = "Value";
            rblTipoSuministroAgua.DataSource = TipoSuministroAgua;
            rblTipoSuministroAgua.DataBind();
            #endregion

            #region Tipo Almacenamiento de agua
            string TinacoTanque = ObjServices.Country == Utility.Country.RepublicaDominicana ? Resources.FormularioRiesgoTinacoTanqueDO
                                                                                             : Resources.FormularioRiesgoTinacoTanqueSV;

            Dictionary<int, string> TipoAlmacenamientoAgua = new Dictionary<int, string> 
            {
                { 1, TinacoTanque },
                { 2, Resources.Cistern }
            };
            rblTipoAlmacenamientoAgua.Items.Clear();
            rblTipoAlmacenamientoAgua.DataValueField = "Key";
            rblTipoAlmacenamientoAgua.DataTextField = "Value";
            rblTipoAlmacenamientoAgua.DataSource = TipoAlmacenamientoAgua;
            rblTipoAlmacenamientoAgua.DataBind();
            #endregion

            #region Vigilantes
            rblVigilantes.Items.Clear();
            rblVigilantes.DataValueField = "Key";
            rblVigilantes.DataTextField = "Value";
            rblVigilantes.DataSource = NoYesOptions;
            rblVigilantes.DataBind();
            #endregion

            #region Distancia del Mar
            rblDistanciaMar.Items.Clear();
            rblDistanciaMar.DataValueField = "Key";
            rblDistanciaMar.DataTextField = "Value";
            rblDistanciaMar.DataSource = DistanceOptions;
            rblDistanciaMar.DataBind();
            #endregion

            #region Distancia de otras fuentes de agua
            rblDistanciaOtrasFuentesAgua.Items.Clear();
            rblDistanciaOtrasFuentesAgua.DataValueField = "Key";
            rblDistanciaOtrasFuentesAgua.DataTextField = "Value";
            rblDistanciaOtrasFuentesAgua.DataSource = DistanceOptions;
            rblDistanciaOtrasFuentesAgua.DataBind();
            #endregion

            #region Distancia Bomberos
            rblDistanciaBomberos.Items.Clear();
            rblDistanciaBomberos.DataValueField = "Key";
            rblDistanciaBomberos.DataTextField = "Value";
            rblDistanciaBomberos.DataSource = DistanceOptions;
            rblDistanciaBomberos.DataBind();
            #endregion

            #region Distancia P. N.
            rblDistanciaPN.Items.Clear();
            rblDistanciaPN.DataValueField = "Key";
            rblDistanciaPN.DataTextField = "Value";
            rblDistanciaPN.DataSource = DistanceOptions;
            rblDistanciaPN.DataBind();
            #endregion

            #region Categoría del Riesgo
            Dictionary<int, string> CategoriaRiesgo = new Dictionary<int, string> 
            {
                { 1, Resources.VeryGood },
                { 2, Resources.Good },
                { 3, Resources.Satisfactory },
                { 4, Resources.Unsatisfying },
                { 5, Resources.NotSatisfactory }
            };
            rblCategoriaRiesgo.Items.Clear();
            rblCategoriaRiesgo.DataValueField = "Key";
            rblCategoriaRiesgo.DataTextField = "Value";
            rblCategoriaRiesgo.DataSource = CategoriaRiesgo;
            rblCategoriaRiesgo.DataBind();
            #endregion

            #region Tipo de Foto
            Dictionary<int, string> TipoFoto = new Dictionary<int, string> 
            {
                { 1, Resources.Internal },
                { 2, Resources.External }
            };
            rblTipoFoto.Items.Clear();
            rblTipoFoto.DataValueField = "Key";
            rblTipoFoto.DataTextField = "Value";
            rblTipoFoto.DataSource = TipoFoto;
            rblTipoFoto.DataBind();
            #endregion

            #region Paneles
            if (ObjServices.Country == Utility.Country.RepublicaDominicana)
            {
                pnlDO.Visible = true;
                pnlSV.Visible = !pnlDO.Visible;
            }
            else if (ObjServices.Country == Utility.Country.ElSalvador)
            {
                pnlSV.Visible = true;
                pnlDO.Visible = !pnlSV.Visible;
            }
            #endregion

            SetRadioButtonListGroupClassItemOption();
        }

        private void SetRadioButtonListGroupClassItemOption()
        {
            try
            {
                foreach (var control in this.pnlGeneral.Controls)
                {
                    if (control is RadioButtonList)
                    {
                        #region RadioButtonList
                        RadioButtonList rbl = control as RadioButtonList;

                        int GroupId = rbl.Attributes["GroupId"].ToInt(),
                            ClassId = rbl.Attributes["ClassId"].ToInt();

                        var options = ObjServices.oAlliedLinesReviewManager.GetAlliedLineGroupsClassesItemsOptions(new AlliedLines.Review.GroupsClassesItemsOptions.Parameters.Get
                        {
                            CorpId = ObjServices.Corp_Id,
                            AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                            GroupId = GroupId
                        }).Where(o => o.ClassId == ClassId).ToList();

                        if (options.Count > 0)
                        {
                            #region Ninguna de las anteriores
                            if (GroupId == 12 && ClassId == 2)
                            {
                                var option = ObjServices.oAlliedLinesReviewManager.GetAlliedLineGroupsClassesItemsOptions(new AlliedLines.Review.GroupsClassesItemsOptions.Parameters.Get
                                {
                                    CorpId = ObjServices.Corp_Id,
                                    AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                                    GroupId = GroupId
                                }).FirstOrDefault(o => o.ClassId == 3);

                                if (option != null)
                                    options.Add(new AlliedLines.Review.GroupsClassesItemsOptions.Result.Get
                                    {
                                        ClassDesc = option.ClassDesc,
                                        ClassId = option.ClassId,
                                        CorpId = option.CorpId,
                                        GroupDesc = option.GroupDesc,
                                        GroupId = option.GroupId,
                                        ItemDesc = option.ItemDesc,
                                        ItemId = option.ItemId,
                                        OptionDesc = option.OptionDesc,
                                        OptionId = option.OptionId
                                    });
                            }
                            #endregion

                            foreach (var option in options)
                            {
                                foreach (ListItem item in rbl.Items)
                                {
                                    #region asignacion
                                    if (
                                            (Utility.ReplaceVowels(item.Text.ToLower().Trim()) == Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim())) ||
                                            (item.Text.ToLower() == "yes" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "si") ||
                                            (item.Text.ToLower() == "high" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "alta") ||
                                            (item.Text.ToLower() == "medium" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "media") ||
                                            (item.Text.ToLower() == "low" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "baja") ||
                                            ((item.Text.ToLower() == "ninguna" || item.Text.ToLower() == "none") && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "ningunas de las anteriores") ||
                                            (item.Text.ToLower() == "good" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "bueno") ||
                                            (item.Text.ToLower() == "bad" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "malo") ||
                                            (item.Text.ToLower() == "< 500 meters" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "< de 500 metros") ||
                                            (item.Text.ToLower() == "> 500 meters" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "> de 500 metros") ||
                                            (item.Text.ToLower() == "own" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "propio") ||
                                            (item.Text.ToLower() == "rented" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "alquilado") ||
                                            (item.Text.ToLower() == "commerce" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "comercio") ||
                                            (item.Text.ToLower() == "factory/industry" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "fabrica/industria") ||
                                            (item.Text.ToLower() == "office" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "oficina") ||
                                            (item.Text.ToLower() == "living place" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "vivienda") ||
                                            (item.Text.ToLower() == "mall" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "plaza comercial") ||
                                            (item.Text.ToLower() == "gasoline" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "gasolina") ||
                                            (item.Text.ToLower() == "public" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "publica") ||
                                            (item.Text.ToLower() == "private" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "privada") ||
                                            (item.Text.ToLower() == "public network" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "red publica") ||
                                            (item.Text.ToLower() == "water well" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "pozo") ||
                                            (item.Text.ToLower() == "cistern" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "cisterna") ||
                                            (item.Text.ToLower() == "very good" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "muy bueno") ||
                                            (item.Text.ToLower() == "satisfactory" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "satisfactorio") ||
                                            (item.Text.ToLower() == "unsatisfying" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "poco satisfactorio") ||
                                            (item.Text.ToLower() == "not satisfactory" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "no satisfactorio") ||
                                            (item.Text.ToLower() == "internal" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "internas") ||
                                            (item.Text.ToLower() == "external" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "externas") ||
                                            (item.Text.ToLower() == "fire" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "incendio") ||
                                            (item.Text.ToLower() == "thunderbolt" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "rayo") ||
                                            (item.Text.ToLower() == "explosion" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "explosion") ||
                                            (item.Text.ToLower() == "malicious damage" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "daños maliciosos") ||
                                            (item.Text.ToLower() == "riot" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "motin") ||
                                            (item.Text.ToLower() == "strike" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "huelga") ||
                                            (item.Text.ToLower() == "flood" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "inundacion") ||
                                            (item.Text.ToLower() == "rainwater damage" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "daños por agua de lluvia") ||
                                            (item.Text.ToLower() == "theft with scaling" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "robo con escalamiento") ||
                                            (item.Text.ToLower() == "theft with violence" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "robo con violencia") ||
                                            (item.Text.ToLower() == "hurricane" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "huracan") ||
                                            (item.Text.ToLower() == "accidental water damage" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "daños por agua accidental")
                                        )
                                        item.Value = string.Format("{0}|{1}|{2}|{3}", option.GroupId, option.ClassId, option.ItemId, option.OptionId);
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }
                    else if (control is Panel)
                    {
                        #region Panel
                        Panel pnl = control as Panel;
                        if (pnl.Visible)
                        {
                            foreach (var ctrl in pnl.Controls)
                            {
                                if (ctrl is RadioButtonList)
                                {
                                    #region RadioButtonList
                                    RadioButtonList rbl = ctrl as RadioButtonList;

                                    int GroupId = rbl.Attributes["GroupId"].ToInt(),
                                        ClassId = rbl.Attributes["ClassId"].ToInt();

                                    var options = ObjServices.oAlliedLinesReviewManager.GetAlliedLineGroupsClassesItemsOptions(new AlliedLines.Review.GroupsClassesItemsOptions.Parameters.Get
                                    {
                                        CorpId = ObjServices.Corp_Id,
                                        AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                                        GroupId = GroupId
                                    }).Where(o => o.ClassId == ClassId).ToList();

                                    if (options.Count > 0)
                                    {
                                        #region Ninguna de las anteriores
                                        if (GroupId == 12 && ClassId == 2)
                                        {
                                            var option = ObjServices.oAlliedLinesReviewManager.GetAlliedLineGroupsClassesItemsOptions(new AlliedLines.Review.GroupsClassesItemsOptions.Parameters.Get
                                            {
                                                CorpId = ObjServices.Corp_Id,
                                                AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                                                GroupId = GroupId
                                            }).FirstOrDefault(o => o.ClassId == 3);

                                            if (option != null)
                                                options.Add(new AlliedLines.Review.GroupsClassesItemsOptions.Result.Get
                                                {
                                                    ClassDesc = option.ClassDesc,
                                                    ClassId = option.ClassId,
                                                    CorpId = option.CorpId,
                                                    GroupDesc = option.GroupDesc,
                                                    GroupId = option.GroupId,
                                                    ItemDesc = option.ItemDesc,
                                                    ItemId = option.ItemId,
                                                    OptionDesc = option.OptionDesc,
                                                    OptionId = option.OptionId
                                                });
                                        }
                                        #endregion

                                        foreach (var option in options)
                                        {
                                            foreach (ListItem item in rbl.Items)
                                            {
                                                #region asignacion
                                                if (
                                                        (Utility.ReplaceVowels(item.Text.ToLower().Trim()) == Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim())) ||
                                                        (item.Text.ToLower() == "yes" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "si") ||
                                                        (item.Text.ToLower() == "high" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "alta") ||
                                                        (item.Text.ToLower() == "medium" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "media") ||
                                                        (item.Text.ToLower() == "low" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "baja") ||
                                                        ((item.Text.ToLower() == "ninguna" || item.Text.ToLower() == "none") && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "ningunas de las anteriores") ||
                                                        (item.Text.ToLower() == "good" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "bueno") ||
                                                        (item.Text.ToLower() == "bad" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "malo") ||
                                                        (item.Text.ToLower() == "< 500 meters" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "< de 500 metros") ||
                                                        (item.Text.ToLower() == "> 500 meters" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "> de 500 metros") ||
                                                        (item.Text.ToLower() == "own" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "propio") ||
                                                        (item.Text.ToLower() == "rented" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "alquilado") ||
                                                        (item.Text.ToLower() == "commerce" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "comercio") ||
                                                        (item.Text.ToLower() == "factory/industry" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "fabrica/industria") ||
                                                        (item.Text.ToLower() == "office" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "oficina") ||
                                                        (item.Text.ToLower() == "living place" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "vivienda") ||
                                                        (item.Text.ToLower() == "mall" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "plaza comercial") ||
                                                        (item.Text.ToLower() == "gasoline" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "gasolina") ||
                                                        (item.Text.ToLower() == "public" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "publica") ||
                                                        (item.Text.ToLower() == "private" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "privada") ||
                                                        (item.Text.ToLower() == "public network" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "red publica") ||
                                                        (item.Text.ToLower() == "water well" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "pozo") ||
                                                        (item.Text.ToLower() == "cistern" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "cisterna") ||
                                                        (item.Text.ToLower() == "very good" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "muy bueno") ||
                                                        (item.Text.ToLower() == "satisfactory" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "satisfactorio") ||
                                                        (item.Text.ToLower() == "unsatisfying" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "poco satisfactorio") ||
                                                        (item.Text.ToLower() == "not satisfactory" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "no satisfactorio") ||
                                                        (item.Text.ToLower() == "internal" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "internas") ||
                                                        (item.Text.ToLower() == "external" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "externas") ||
                                                        (item.Text.ToLower() == "fire" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "incendio") ||
                                                        (item.Text.ToLower() == "thunderbolt" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "rayo") ||
                                                        (item.Text.ToLower() == "explosion" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "explosion") ||
                                                        (item.Text.ToLower() == "malicious damage" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "daños maliciosos") ||
                                                        (item.Text.ToLower() == "riot" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "motin") ||
                                                        (item.Text.ToLower() == "strike" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "huelga") ||
                                                        (item.Text.ToLower() == "flood" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "inundacion") ||
                                                        (item.Text.ToLower() == "rainwater damage" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "daños por agua de lluvia") ||
                                                        (item.Text.ToLower() == "theft with scaling" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "robo con escalamiento") ||
                                                        (item.Text.ToLower() == "theft with violence" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "robo con violencia") ||
                                                        (item.Text.ToLower() == "hurricane" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "huracan") ||
                                                        (item.Text.ToLower() == "accidental water damage" && Utility.ReplaceVowels(option.ItemDesc.ToLower().Trim()) == "daños por agua accidental")
                                                    )
                                                    item.Value = string.Format("{0}|{1}|{2}|{3}", option.GroupId, option.ClassId, option.ItemId, option.OptionId);
                                                #endregion
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.MessageBoxALIF(string.Format("{0}<br /><br /><b>InnerException:</b> {1}", ex.Message, ex.InnerException), null, null, true, Resources.Warning);
                else
                    this.MessageBoxALIF(ex.Message, null, null, true, Resources.Warning);
            }
        }

        private void propiedadesIndexChanged()
        {
            if (ddlPropiedades.SelectedIndex == 0)
            {
                btnSendToSubscription.Enabled = false;
                ClearData(false);
                txtFechaInspeccion.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                this.ExcecuteJScript("setFechaInspeccion(); setUbicacionInspeccionada('');");
                return;
            }

            try
            {
                btnSendToSubscription.Enabled = true;

                ViewState["txtFechaInspeccion"] = txtFechaInspeccion.Text;

                ClearData(true);

                txtFechaInspeccion.Text = Convert.ToString(ViewState["txtFechaInspeccion"]);

                RadioButtonListSetDataSource();

                var contact = Contact();
                if (contact != null)
                {
                    txtNombrePropietario.Text = contact.FullName;
                    txtNombrePropietario.Enabled = false;
                }

                #region Get Unique Property Id
                long uniquePropertyId = Convert.ToInt64(ddlPropiedades.SelectedValue);
                #endregion

                ViewState["ReviewId"] =
                ViewState["AlliedLineId"] =
                ViewState["UniqueAlliedLineId"] = null;

                if (uniquePropertyId > 0)
                {
                    #region Get Property
                    var alliedLine = ObjServices.oPropertyManager.GetProperty(new Property.Key
                    {
                        CorpId = ObjServices.Corp_Id,
                        RegionId = ObjServices.Region_Id,
                        CountryId = ObjServices.Country_Id,
                        DomesticregId = ObjServices.Domesticreg_Id,
                        StateProvId = ObjServices.State_Prov_Id,
                        CityId = ObjServices.City_Id,
                        OfficeId = ObjServices.Office_Id,
                        CaseSeqNo = ObjServices.Case_Seq_No,
                        HistSeqNo = ObjServices.Hist_Seq_No
                    }).FirstOrDefault(p => p.UniquePropertyId == uniquePropertyId);
                    #endregion

                    if (alliedLine != null)
                    {
                        #region Datos Generales
                        #region Identificacion
                        var lstIdentification = ObjServices.oContactManager.GetAllIdDocumentInformation(ObjServices.ContactEntityID.GetValueOrDefault(), ObjServices.getCurrentLanguage());
                        if (lstIdentification != null && lstIdentification.Any())
                        {
                            var dataId = lstIdentification.First();
                            if (dataId != null)
                            {
                                txtNumeroIdentificacion.Text = dataId.Id;
                                txtIdentificationType.Text = dataId.ContactIdTypeDescription;
                            }
                        }
                        #endregion
                        txtNoDeEmpleados.Text = alliedLine.EmployeesQty.ToString();
                        #endregion

                        #region Sumas Aseguradas
                        txtSumasAseguradasEdificio.Text = alliedLine.EdificationValue.ToFormatNumeric();
                        txtSumasAseguradasMobiliarios.Text = alliedLine.FurnitureAndEquipmentValue.ToFormatNumeric();
                        txtSumasAseguradasMaquinarias.Text = alliedLine.MachineryValue.ToFormatNumeric();
                        txtSumasAseguradasExistencia.Text = alliedLine.StockValue.ToFormatNumeric();

                        txtSumasAseguradasEdificio.Enabled =
                        txtSumasAseguradasMobiliarios.Enabled =
                        txtSumasAseguradasMaquinarias.Enabled =
                        txtSumasAseguradasExistencia.Enabled = false;
                        #endregion

                        //txtTipoConstruccion.Text = alliedLine.PropertyBuildTypeDesc;
                        ddlTipoconstruccion.SelectedValue = alliedLine.PropertyBuildTypeId.ToString();

                        #region Prevencion y Proteccion
                        if (ObjServices.Country == Utility.Country.RepublicaDominicana)
                        {
                            pnlDO.Visible = true;
                            pnlSV.Visible = !pnlDO.Visible;
                        }
                        else if (ObjServices.Country == Utility.Country.ElSalvador)
                        {
                            pnlSV.Visible = true;
                            pnlDO.Visible = !pnlSV.Visible;

                            txtPrevencionProteccionDistanciaRiosLagosMares.Text = alliedLine.DistanceKilometersRiver;
                            txtPrevencionProteccionDistanciaVolcanes.Text = alliedLine.DistanceKilometersStream;
                        }
                        #endregion

                        #region Colindancias
                        ddlColindanciaNorte.SelectedValue = alliedLine.NorthBorderId.ToString();
                        ddlColindanciaSur.SelectedValue = alliedLine.SouthBorderId.ToString();
                        ddlColindanciaEste.SelectedValue = alliedLine.EastBorderId.ToString();
                        ddlColindanciaOeste.SelectedValue = alliedLine.WestBorderId.ToString();
                        #endregion

                        #region Localizacion Riesgo
                        if (ObjServices.Country == Utility.Country.RepublicaDominicana)
                        {
                            txtCalle.Text = alliedLine.AddressStreet;
                            txtSectorParajeSeccion.Text = "";
                            txtMunicipio.Text = alliedLine.CityDescLoc;
                            txtProvincia.Text = alliedLine.StateProvDescLoc;

                            txtLatitud.Text = alliedLine.Latitude != null ? alliedLine.Latitude.ToString() : "0";
                            txtLongitud.Text = alliedLine.Longitude != null ? alliedLine.Longitude.ToString() : "0";
                        }
                        else if (ObjServices.Country == Utility.Country.ElSalvador)
                        {
                            txtCalle.Text = alliedLine.CountryDescLoc;                  //Pais
                            txtSectorParajeSeccion.Text = alliedLine.StateProvDescLoc;  //Departamento
                            txtMunicipio.Text = alliedLine.CityDescLoc;                 //Municipio
                            txtProvincia.Text = alliedLine.AddressStreet;                     //Calle/Avenida
                            txtLongitud.Text = "";  //Numero
                        }
                        #endregion

                        #region Review / Review Detail

                        var getReview = ObjServices.oAlliedLinesReviewManager.GetAlliedLineReview(new AlliedLines.Review.Parameters.Get
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
                            AlliedLineId = alliedLine.PropertyId,
                            UniqueAlliedLineId = uniquePropertyId,
                            AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                            ReviewId = null,
                            BlTypeId = alliedLine.BlTypeId,
                            BlId = alliedLine.BlId,
                            ProductId = alliedLine.ProductId
                        }).LastOrDefault();

                        if (getReview != null)
                        {
                            ViewState["ReviewId"] = getReview.ReviewId;
                            ViewState["AlliedLineId"] = getReview.PropertyId;
                            ViewState["UniqueAlliedLineId"] = uniquePropertyId;


                            #region FechaInspeccion
                            string fecha = getReview.InspectionDate.ToString("dd-MMM-yyyy").ToUpper(),
                                   hora = string.Empty,
                                   minutos = string.Empty,
                                   segundos = string.Empty,
                                   ampm = string.Empty,
                                   FechaInspeccion = string.Empty;

                            var hora_split = getReview.InspectionDate.ToString("HH:mm:ss").Split(':');
                            if (hora_split.Length > 0)
                            {
                                hora = hora_split[0];
                                minutos = hora_split[1];
                                segundos = hora_split[2];

                                if (hora.ToInt() == 12)
                                    ampm = "M";
                                else if (hora.ToInt() >= 12)
                                {
                                    ampm = "PM";
                                    hora = (hora.ToInt() - 12).ToString();
                                    if (hora.ToInt() < 10)
                                        hora = string.Format("0{0}", hora);
                                }
                                else
                                {
                                    ampm = "AM";
                                    hora = string.Format("0{0}", hora);
                                }
                            }

                            FechaInspeccion = string.Format("{0} {1}:{2}:{3} {4}", fecha, hora, minutos, segundos, ampm);

                            txtFechaInspeccion.Text = FechaInspeccion;
                            #endregion

                            txtNombreRiesgo.Text = getReview.RiskName;


                            if (getReview.InspectedLocation.Trim().Length > 0)
                                this.ExcecuteJScript("setUbicacionInspeccionada('" + getReview.InspectedLocation + "');");

                            txtTipoRiesgo.Text = getReview.RiskType;

                            #region Review Detail
                            List<AlliedLines.Review.Detail.Result.Get> getReviewDetails = ObjServices.oAlliedLinesReviewManager.GetAlliedLineReviewDetail(new AlliedLines.Review.Detail.Parameters.Get
                            {
                                CorpId = getReview.CorpId,
                                RegionId = getReview.RegionId,
                                CountryId = getReview.CountryId,
                                DomesticregId = getReview.DomesticregId,
                                StateProvId = getReview.StateProvId,
                                CityId = getReview.CityId,
                                OfficeId = getReview.OfficeId,
                                CaseSeqNo = getReview.CaseSeqNo,
                                HistSeqNo = getReview.HistSeqNo,
                                AlliedLineId = alliedLine.PropertyId,
                                UniqueAlliedLineId = uniquePropertyId,
                                AlliedLineTypeId = getReview.AlliedLineTypeId,
                                ReviewId = getReview.ReviewId,
                                ReviewGroupId = null,
                                ReviewGroupEndorsementClarifying = null,
                                ReviewOptionEndorsementClarifying = null
                            }).ToList<AlliedLines.Review.Detail.Result.Get>();

                            if (getReviewDetails.Count > 0)
                            {
                                if (!string.IsNullOrWhiteSpace(getReviewDetails.FirstOrDefault().UsuarioInspeccion))
                                {
                                    this.ExcecuteJScript("setUserInspection('" + getReviewDetails.FirstOrDefault().UsuarioInspeccion + "');");
                                }

                                List<string> seleccionados = new List<string>() { };

                                foreach (var control in this.pnlGeneral.Controls)
                                {
                                    int GroupId = 0,
                                        ClassId = 0,
                                        ItemId = 0,
                                        OptionId = 0;

                                    string[] grpclsitmopt = new string[] { };

                                    if (control is TextBox)
                                    {
                                        #region TextBox
                                        TextBox txt = control as TextBox;
                                        if (txt.Attributes["grpclsitmopt"] != null && (txt.Attributes["grpclsitmopt"] != "0|0|0|0|n" && txt.Attributes["grpclsitmopt"] != "0|0|0|0|y"))
                                        {
                                            grpclsitmopt = txt.Attributes["grpclsitmopt"].Split('|');

                                            GroupId = grpclsitmopt[0].ToInt();
                                            ClassId = grpclsitmopt[1].ToInt();
                                            ItemId = grpclsitmopt[2].ToInt();
                                            OptionId = grpclsitmopt[3].ToInt();

                                            var getReviewDetail = getReviewDetails.Where(d => d.ReviewGroupId == GroupId &&
                                                                                              d.ReviewClassId == ClassId &&
                                                                                              d.ReviewItemId == ItemId &&
                                                                                              d.ReviewOptionId == OptionId).FirstOrDefault();
                                            string valueText = string.Empty;
                                            if (getReviewDetail != null)
                                            {
                                                if (quotationValues.Contains(txt.ID))
                                                    valueText = (txt.Text.Trim() != string.Empty && getReviewDetail.ValueText.Trim() == string.Empty) ? txt.Text : getReviewDetail.ValueText;
                                                else
                                                    valueText = getReviewDetail.ValueText;
                                            }
                                            txt.Text = valueText.Trim();

                                            txt.Enabled = (quotationTextDisabled.Contains(txt.ID) && txt.Text.Length > 0) ? false : true;
                                        }
                                        #endregion
                                    }
                                    else if (control is RadioButtonList)
                                    {
                                        #region RadioButtonList
                                        RadioButtonList rbl = control as RadioButtonList;
                                        foreach (ListItem item in rbl.Items)
                                        {
                                            item.Selected = false;

                                            grpclsitmopt = item.Value.Split('|');
                                            if (grpclsitmopt.Length == 4)
                                            {
                                                GroupId = grpclsitmopt[0].ToInt();
                                                ClassId = grpclsitmopt[1].ToInt();
                                                ItemId = grpclsitmopt[2].ToInt();
                                                OptionId = grpclsitmopt[3].ToInt();

                                                bool selected = (getReviewDetails.Count > 0) ? getReviewDetails.Any(d => d.ReviewGroupId == GroupId &&
                                                                                                                    d.ReviewClassId == ClassId &&
                                                                                                                    d.ReviewItemId == ItemId &&
                                                                                                                    d.ReviewOptionId == OptionId &&
                                                                                                                    d.ValueChecked == OptionId)
                                                                                             : false;
                                                if (selected)
                                                {
                                                    seleccionados.Add(item.Value);
                                                    break;
                                                }
                                            }

                                        }
                                        #endregion
                                    }
                                    else if (control is DropDownList)
                                    {
                                        #region DropDownList
                                        DropDownList ddl = control as DropDownList;
                                        if (ddl.Attributes["GroupId"] != null)
                                        {
                                            int ReviewGroupId = ddl.Attributes["GroupId"].ToInt(),
                                                ReviewClassId = ddl.Attributes["ClassId"].ToInt();

                                            var valueChecked = getReviewDetails.FirstOrDefault(d => d.ReviewGroupId == ReviewGroupId &&
                                                                                                    d.ReviewClassId == ReviewClassId &&
                                                                                                    d.ReviewItemId == 1 &&
                                                                                                    d.ReviewOptionId == 0);
                                            if (valueChecked != null)
                                                ddl.SelectedValue = valueChecked.ValueChecked.ToString();
                                        }
                                        #endregion
                                    }
                                    else if (control is Panel)
                                    {
                                        #region Panel RD o SV
                                        Panel pnl = control as Panel;
                                        if (pnl.Visible)
                                        {
                                            foreach (var ctrl in pnl.Controls)
                                            {
                                                if (ctrl is TextBox)
                                                {
                                                    #region TextBox
                                                    TextBox txt = ctrl as TextBox;
                                                    if (txt.Attributes["grpclsitmopt"] != null && (txt.Attributes["grpclsitmopt"] != "0|0|0|0|n" && txt.Attributes["grpclsitmopt"] != "0|0|0|0|y"))
                                                    {
                                                        grpclsitmopt = txt.Attributes["grpclsitmopt"].Split('|');

                                                        GroupId = grpclsitmopt[0].ToInt();
                                                        ClassId = grpclsitmopt[1].ToInt();
                                                        ItemId = grpclsitmopt[2].ToInt();
                                                        OptionId = grpclsitmopt[3].ToInt();

                                                        var getReviewDetail = getReviewDetails.Where(d => d.ReviewGroupId == GroupId &&
                                                                                                          d.ReviewClassId == ClassId &&
                                                                                                          d.ReviewItemId == ItemId &&
                                                                                                          d.ReviewOptionId == OptionId).FirstOrDefault();
                                                        string valueText = string.Empty;
                                                        if (getReviewDetail != null)
                                                        {
                                                            if (quotationValues.Contains(txt.ID))
                                                                valueText = (txt.Text.Trim() != string.Empty && getReviewDetail.ValueText.Trim() == string.Empty) ? txt.Text : getReviewDetail.ValueText;
                                                            else
                                                                valueText = getReviewDetail.ValueText;
                                                        }
                                                        txt.Text = valueText.Trim();

                                                        txt.Enabled = (quotationTextDisabled.Contains(txt.ID) && txt.Text.Length > 0) ? false : true;
                                                    }
                                                    #endregion
                                                }
                                                else if (ctrl is RadioButtonList)
                                                {
                                                    #region RadioButtonList
                                                    RadioButtonList rbl = ctrl as RadioButtonList;
                                                    foreach (ListItem item in rbl.Items)
                                                    {
                                                        item.Selected = false;

                                                        grpclsitmopt = item.Value.Split('|');
                                                        if (grpclsitmopt.Length == 4)
                                                        {
                                                            GroupId = grpclsitmopt[0].ToInt();
                                                            ClassId = grpclsitmopt[1].ToInt();
                                                            ItemId = grpclsitmopt[2].ToInt();
                                                            OptionId = grpclsitmopt[3].ToInt();

                                                            bool selected = false;
                                                            if (getReviewDetails != null)
                                                                selected = (getReviewDetails != null) ? getReviewDetails.Any(d => d.ReviewGroupId == GroupId &&
                                                                                                                                  d.ReviewClassId == ClassId &&
                                                                                                                                  d.ReviewItemId == ItemId &&
                                                                                                                                  d.ReviewOptionId == OptionId &&
                                                                                                                                  d.ValueChecked == OptionId)
                                                                                                      : false;
                                                            if (selected)
                                                            {
                                                                seleccionados.Add(item.Value);
                                                                break;
                                                            }
                                                        }

                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                }

                                if (seleccionados.Count > 0)
                                    this.ExcecuteJScript("checkRBL(" + Utility.serializeToJSON(seleccionados) + ");");
                            }
                            #endregion


                        }
                        else
                        {
                            this.ExcecuteJScript("setFechaInspeccion();");
                        }
                        #endregion

                        #region Review Pictures
                        GridFillData();
                        #endregion
                        txtUbicacionInspeccionada.Text = alliedLine.AddressStreet;
                        txtNumero.Text = alliedLine.AddressNumber;
                        txtUbicacionInspeccionada.Enabled = true;
                        //if (txtUbicacionInspeccionada.Text.Trim().Length == 0)
                        //{
                        //var Address = string.Empty;
                        //if (alliedLine.Address.Trim().Length > 0)
                        //    Address = alliedLine.Address.Trim();
                        //if (alliedLine.addressNumber.Trim().Length > 0)
                        //    Address += string.Concat((Address.Trim().Length > 0 ? ", " : string.Empty), alliedLine.addressNumber.Trim());
                        //txtUbicacionInspeccionada.Text = alliedLine.Address;
                        //txtNumero.Text = alliedLine.addressNumber;
                        //this.ExcecuteJScript("setUbicacionInspeccionada('" + Address + "');");
                        //}

                        if (ObjServices.InspectorAgentId != null && ObjServices.InspectorAgentId > 0)
                            drpInspectors.SelectedValue = ObjServices.InspectorAgentId.GetValueOrDefault().ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.MessageBoxALIF(string.Format("{0}<br /><br /><b>InnerException:</b> {1}", ex.Message, ex.InnerException), null, null, true, Resources.Warning);
                else
                    this.MessageBoxALIF(ex.Message, null, null, true, Resources.Warning);
            }
        }

        private void GridFillData()
        {
            try
            {
                List<AlliedLines.Review.Pic.Result.Get> pictures = new List<AlliedLines.Review.Pic.Result.Get>() { };

                if (ViewState["ReviewId"] != null &&
                    ViewState["AlliedLineId"] != null &&
                    ViewState["UniqueAlliedLineId"] != null)
                {
                    #region Document Category
                    var document_category = ObjServices.oAlliedLinesReviewManager.GetDocumentCategory(new AlliedLines.Document.Category.Parameters.Get
                    {
                        NameKey = Utility.AlliedLinesInspectionFormPhotos
                    });
                    #endregion

                    int reviewId = ViewState["ReviewId"].ToInt(),
                        alliedLineId = ViewState["AlliedLineId"].ToInt();

                    long uniqueAlliedLineId = ViewState["UniqueAlliedLineId"].ToLong();

                    pictures = ObjServices.oAlliedLinesReviewManager.GetAlliedLineReviewPic(new AlliedLines.Review.Pic.Parameters.Get
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
                        AlliedLineId = alliedLineId,
                        UniqueAlliedLineId = uniqueAlliedLineId,
                        AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                        ReviewId = reviewId,
                        DocTypeId = document_category.DocTypeId,
                        DocCategoryId = document_category.DocCategoryId
                    }).ToList();
                }

                gvPictures.DataSource = pictures;
                gvPictures.DataBind();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.MessageBoxALIF(string.Format("{0}<br /><br /><b>InnerException:</b> {1}", ex.Message, ex.InnerException), null, null, true, Resources.Warning);
                else
                    this.MessageBoxALIF(ex.Message, null, null, true, Resources.Warning);
            }
        }

        protected void ddlPropiedades_SelectedIndexChanged(object sender, EventArgs e)
        {
            propiedadesIndexChanged();
        }

        protected void btnBackToIllustrations_Click(object sender, EventArgs e)
        {
            Session.Remove("PhotoSaved");
            Session.Remove("fuFotografia");

            Session["fromInspection"] = true;
            Response.Redirect("IllustrationsVehicle.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearData(false);
            gvPictures.DataSource = new List<AlliedLines.Document.Policy.Result.Get>() { };
            gvPictures.DataBind();
        }

        protected void rblTipoFoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            var IntExtCat = (((Utility.AlliedLinePropertyPhotos)rblTipoFoto.SelectedValue.ToInt() == Utility.AlliedLinePropertyPhotos.Internal) ? ViewState["IntCat"]
                                                                                                                                                : ViewState["ExtCat"]) as Dictionary<int, string>;
            ddlCategoriaFoto.Items.Clear();
            ddlCategoriaFoto.DataValueField = "Key";
            ddlCategoriaFoto.DataTextField = "Value";
            ddlCategoriaFoto.DataSource = IntExtCat;
            ddlCategoriaFoto.DataBind();

            ddlCategoriaFoto.Enabled = true;

            Session["PhotoSaved"] = false;
        }

        protected void ddlCategoriaFoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["PhotoSaved"] = false;

                int numberOfPhotos = Utility.PropertyNumberOfPhotos((Utility.AlliedLinePropertyPhotos)rblTipoFoto.SelectedValue.ToInt(),
                                                                    ddlCategoriaFoto.SelectedValue.ToInt());

                if (numberOfPhotos == -1)
                {
                    this.ExcecuteJScript("enabledPhotoButton(false);");
                    return;
                }

                #region Document Category
                var document_category = ObjServices.oAlliedLinesReviewManager.GetDocumentCategory(new AlliedLines.Document.Category.Parameters.Get
                {
                    NameKey = Utility.AlliedLinesInspectionFormPhotos
                });
                #endregion

                int reviewId = ViewState["ReviewId"].ToInt(),
                    alliedLineId = ViewState["AlliedLineId"].ToInt();

                long uniqueAlliedLineId = ViewState["UniqueAlliedLineId"].ToLong();

                int count = ObjServices.oAlliedLinesReviewManager.GetAlliedLineReviewPic(new AlliedLines.Review.Pic.Parameters.Get
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
                    AlliedLineId = alliedLineId,
                    UniqueAlliedLineId = uniqueAlliedLineId,
                    AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                    ReviewId = reviewId,
                    DocTypeId = document_category.DocTypeId,
                    DocCategoryId = document_category.DocCategoryId
                }).Count(d => d.DocumentDesc == rblTipoFoto.SelectedItem.Text &&
                              d.DocumentName == ddlCategoriaFoto.SelectedItem.Text);

                this.ExcecuteJScript("enabledPhotoButton(" + (!(count >= numberOfPhotos)).ToString().ToLower() + ");");

                if (count >= numberOfPhotos)
                {
                    this.MessageBoxALIF(string.Format("Ha alcanzado la cantidad máxima de fotos para este Tipo/Categoría.<br /><br />Tipo: {0}.<br />Categoría: {1}.<br />Cantidad de fotos: {2}.", rblTipoFoto.SelectedItem.Text,
                                                                                                                                                                                                    ddlCategoriaFoto.SelectedItem.Text,
                                                                                                                                                                                                    numberOfPhotos),
                                        null, null, true, Resources.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.MessageBoxALIF(string.Format("{0}<br /><br /><b>InnerException:</b> {1}", ex.Message, ex.InnerException), null, null, true, Resources.Warning);
                else
                    this.MessageBoxALIF(ex.Message, null, null, true, Resources.Warning);
            }
        }

        protected void gvPictures_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            var command = e.CommandArgs.CommandName;
            var index = e.VisibleIndex;

            int DocCategoryId = gvPictures.GetKeyFromAspxGridView("DocCategoryId", index).ToInt(),
                DocTypeId = gvPictures.GetKeyFromAspxGridView("DocTypeId", index).ToInt(),
                DocumentId = gvPictures.GetKeyFromAspxGridView("DocumentId", index).ToInt();

            string DocumentDesc = gvPictures.GetKeyFromAspxGridView("DocumentDesc", index).ToString(),
                   DocumentName = gvPictures.GetKeyFromAspxGridView("DocumentName", index).ToString();

            switch (command)
            {
                case "Delete":
                    DeletePhoto(DocCategoryId, DocTypeId, DocumentDesc, DocumentId, DocumentName);
                    break;
            }
        }

        protected void gvPictures_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            var CallbackName = e.CallbackName;

            if (Utility.CallBackList.Contains(CallbackName))
            {
                gvPictures.FocusedRowIndex = -1;
                gvPictures.SetFilterSettings();
                GridFillData();
            }
        }

        protected void gvPictures_BeforeHeaderFilterFillItems(object sender, DevExpress.Web.ASPxGridViewBeforeHeaderFilterFillItemsEventArgs e)
        {
            GridFillData();
        }

        protected void gvPictures_PreRender(object sender, EventArgs e)
        {
            var grid = (DevExpress.Web.ASPxGridView)sender;
            grid.TranslateColumnsAspxGrid();
        }

        protected void gvPictures_PageIndexChanged(object sender, EventArgs e)
        {
            GridFillData();
        }

        protected void btnCargarFoto_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["fuFotografia"] != null && Session["PhotoSaved"].ToBoolean() == false)
                {
                    FileUpload fuFotografia = Session["fuFotografia"] as FileUpload;
                    if (fuFotografia.HasFiles)
                    {
                        #region Document Category
                        var document_category = ObjServices.oAlliedLinesReviewManager.GetDocumentCategory(new AlliedLines.Document.Category.Parameters.Get
                        {
                            NameKey = Utility.AlliedLinesInspectionFormPhotos
                        });
                        #endregion

                        #region Set Inspection Date/Time
                        var horafull = txtFechaInspeccion.Text;
                        var ampmfull = horafull.Split(' ');
                        var array_hora = ampmfull[1].Split(':');
                        var ampm = ampmfull[2];
                        int hora = array_hora[0].ToInt(),
                            minutos = array_hora[1].ToInt(),
                            segundos = array_hora[2].ToInt();

                        if (ampm == "PM" && hora < 12)
                            hora += 12;

                        var InspectionDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hora, minutos, segundos);
                        #endregion

                        #region Get Unique Property Id
                        long uniquePropertyId = Convert.ToInt64(ddlPropiedades.SelectedValue);
                        #endregion

                        #region Get Property
                        var property = ObjServices.oPropertyManager.GetProperty(new Property.Key
                        {
                            CorpId = ObjServices.Corp_Id,
                            RegionId = ObjServices.Region_Id,
                            CountryId = ObjServices.Country_Id,
                            DomesticregId = ObjServices.Domesticreg_Id,
                            StateProvId = ObjServices.State_Prov_Id,
                            CityId = ObjServices.City_Id,
                            OfficeId = ObjServices.Office_Id,
                            CaseSeqNo = ObjServices.Case_Seq_No,
                            HistSeqNo = ObjServices.Hist_Seq_No
                        }).FirstOrDefault(p => p.UniquePropertyId == uniquePropertyId);
                        #endregion

                        foreach (var foto in fuFotografia.PostedFiles)
                        {
                            string fileExtension = System.IO.Path.GetExtension(foto.FileName).ToLower();

                            bool fileAccepted = allowedExtensions.Any(item => fileExtension.Contains(item));
                            if (fileAccepted)
                            {
                                #region Set Document / Policy Document
                                byte[] DocumentBinary = new byte[] { };
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    foto.InputStream.CopyTo(ms);
                                    DocumentBinary = ms.ToArray();
                                }

                                #region Set Document
                                var doc = ObjServices.oAlliedLinesReviewManager.SetDocument(new AlliedLines.Document.Parameters.Set
                                {
                                    DocTypeId = document_category.DocTypeId,
                                    DocCategoryId = document_category.DocCategoryId,
                                    DocumentId = 0,
                                    DocumentBinary = Utility.CompressImage(DocumentBinary, 30),
                                    DocumentName = ddlCategoriaFoto.SelectedItem.Text,
                                    DocumentDesc = rblTipoFoto.SelectedItem.Text,
                                    FileCreationDate = DateTime.Now,
                                    FileExpireDate = null,
                                    UserId = ObjServices.UserID.GetValueOrDefault()
                                });
                                #endregion

                                if (doc != null)
                                {
                                    #region Set Policy Document
                                    var poldoc = ObjServices.oAlliedLinesReviewManager.SetPolicyDocument(new AlliedLines.Document.Policy.Parameters.Set
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
                                        DocTypeId = document_category.DocTypeId,
                                        DocCategoryId = document_category.DocCategoryId,
                                        DocumentId = doc.DocumentId.GetValueOrDefault(),
                                        DocumentStatusId = true,
                                        UserId = ObjServices.UserID.GetValueOrDefault()
                                    });
                                    #endregion
                                }
                                #endregion

                                SavePicture(document_category, InspectionDate, uniquePropertyId, property);
                            }
                            else
                            {
                                Session["PhotoSaved"] = false;
                                this.MessageBoxALIF(Resources.CannotAcceptFilesType, null, null, true, Resources.Warning);
                                break;
                            }
                        }
                    }
                    Session["fuFotografia"] = null;
                }

                rblTipoFoto.ClearSelection();
                ddlCategoriaFoto.SelectedIndex = 0;

                GridFillData();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.MessageBoxALIF(string.Format("{0}<br /><br /><b>InnerException:</b> {1}", ex.Message, ex.InnerException), null, null, true, Resources.Warning);
                else
                    this.MessageBoxALIF(ex.Message, null, null, true, Resources.Warning);
            }
        }

        private void DeletePhoto(int DocCategoryId, int DocTypeId, string DocumentDesc, int DocumentId, string DocumentName)
        {
            try
            {
                long uniquePropertyId = Convert.ToInt64(ddlPropiedades.SelectedValue);

                int propertyId = ViewState["propertyId"].ToInt(),
                    reviewId = ViewState["ReviewId"].ToInt();

                var del = ObjServices.oAlliedLinesReviewManager.DelAlliedLineReviewPhoto(new AlliedLines.Review.Pic.Del.Parameters.Set
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
                    AlliedLineId = propertyId,
                    UniqueAlliedLineId = uniquePropertyId,
                    AlliedLineTypeId = Utility.AlliedLinesType.Property.ToInt(),
                    ReviewId = reviewId,
                    DocTypeId = DocTypeId,
                    DocCategoryId = DocCategoryId,
                    DocumentId = DocumentId,
                    DocumentDesc = DocumentDesc,
                    DocumentName = DocumentName
                });

                GridFillData();
                this.ExcecuteJScript("enabledPhotoButton(true);");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.MessageBoxALIF(string.Format("{0}<br /><br /><b>InnerException:</b> {1}", ex.Message, ex.InnerException), null, null, true, Resources.Warning);
                else
                    this.MessageBoxALIF(ex.Message, null, null, true, Resources.Warning);
            }
        }

        protected void btnSendToSubscription_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUsuarioInspeccion.Text.Trim()))
                {
                    this.MessageBoxALIF(string.Format(Resources.YouMustIndicateInspectionUserName), null, null, true, Resources.InformationLabel);
                    return;
                }

                if (ddlTipoconstruccion.SelectedIndex <= 0)
                {
                    this.MessageBoxALIF(string.Format(Resources.YouMustIndicateBuildType), null, null, true, Resources.InformationLabel);
                    return;
                }

                if (string.IsNullOrWhiteSpace(ObjServices.InspectorName) && (ObjServices.InspectorAgentId <= 0 || ObjServices.InspectorAgentId == null))
                {
                    this.MessageBox(Resources.YouMustIndicateInspectorName, Title: "Error", Width: 500, Height: 150);
                    return;
                }

                //asigno el inspector antes de procesar los demas datos
                if (string.IsNullOrWhiteSpace(ObjServices.InspectorName) && ObjServices.InspectorAgentId > 0)
                {
                    ObjServices.AssignIllustrationToSubscriber(ObjServices.Corp_Id,
                           ObjServices.Region_Id,
                           ObjServices.Country_Id,
                           ObjServices.Domesticreg_Id,
                           ObjServices.State_Prov_Id,
                           ObjServices.City_Id,
                           ObjServices.Office_Id,
                           ObjServices.Case_Seq_No,
                           ObjServices.Hist_Seq_No,
                           drpInspectors.SelectedValue.ToInt(),
                           "Inspector"
                           );

                    //Actualizo la flag table para que luego que tenga asignado el inspector, que el proceso reconozca ese dato en la tabla que llena el GridView de la bandeja. si no se hace esto, es como si no se efectuara la asignacion del inspector
                    var Result = ObjServices.UpdateTempTable(ObjServices.Policy_Id, ObjServices.UserID.GetValueOrDefault());
                }



                saveTMP();

                long uniquePropertyId = Convert.ToInt64(ddlPropiedades.SelectedValue);

                if (ObjServices.hdnQuotationTabs != "lnkMissingInspections")
                {
                    Session["firstOption"] = false;


                    var policy = new Utility.itemPolicy()
                    {
                        CorpId = ObjServices.Corp_Id,
                        RegionId = ObjServices.Region_Id,
                        CountryId = ObjServices.Country_Id,
                        DomesticregId = ObjServices.Domesticreg_Id,
                        StateProvId = ObjServices.State_Prov_Id,
                        CityId = ObjServices.City_Id,
                        OfficeId = ObjServices.Office_Id,
                        CaseSeqNo = ObjServices.Case_Seq_No,
                        HistSeqNo = ObjServices.Hist_Seq_No
                    };

                    string result = ObjServices.InspectionCompleted(policy, ObjServices.ProductLine);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        this.ExcecuteJScript("$('#divReason').hide();");
                        this.MessageBox(result, Width: 500, Title: Resources.Warning);
                        return;
                    }
                    else
                    {
                        bool r = ObjServices.hasInspectionAllProperties(new Property.Key
                        {
                            CorpId = ObjServices.Corp_Id,
                            RegionId = ObjServices.Region_Id,
                            CountryId = ObjServices.Country_Id,
                            DomesticregId = ObjServices.Domesticreg_Id,
                            StateProvId = ObjServices.State_Prov_Id,
                            CityId = ObjServices.City_Id,
                            OfficeId = ObjServices.Office_Id,
                            CaseSeqNo = ObjServices.Case_Seq_No,
                            HistSeqNo = ObjServices.Hist_Seq_No
                        });
                        if (!r)
                        {
                            this.MessageBox(Resources.QuotationInspectionNoCompleted, Title: "Error", Width: 500, Height: 150);
                            Session["areInspected"] = "False";
                            return;
                        }

                    }
                }
                else
                {
                    var policy = new Utility.itemPolicy()
                    {
                        CorpId = ObjServices.Corp_Id,
                        RegionId = ObjServices.Region_Id,
                        CountryId = ObjServices.Country_Id,
                        DomesticregId = ObjServices.Domesticreg_Id,
                        StateProvId = ObjServices.State_Prov_Id,
                        CityId = ObjServices.City_Id,
                        OfficeId = ObjServices.Office_Id,
                        CaseSeqNo = ObjServices.Case_Seq_No,
                        HistSeqNo = ObjServices.Hist_Seq_No
                    };

                    string result = ObjServices.InspectionCompleted(policy, ObjServices.ProductLine);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        this.ExcecuteJScript("$('#divReason').hide();");
                        this.MessageBox(result, Width: 500, Title: Resources.Warning);
                        return;
                    }
                    else
                    {
                        bool r = ObjServices.hasInspectionAllProperties(new Property.Key
                        {
                            CorpId = ObjServices.Corp_Id,
                            RegionId = ObjServices.Region_Id,
                            CountryId = ObjServices.Country_Id,
                            DomesticregId = ObjServices.Domesticreg_Id,
                            StateProvId = ObjServices.State_Prov_Id,
                            CityId = ObjServices.City_Id,
                            OfficeId = ObjServices.Office_Id,
                            CaseSeqNo = ObjServices.Case_Seq_No,
                            HistSeqNo = ObjServices.Hist_Seq_No
                        });
                        if (!r)
                        {
                            this.MessageBox(Resources.QuotationInspectionNoCompleted, Title: "Error", Width: 500, Height: 150);
                            Session["areInspected"] = "False";
                            return;
                        }
                        else
                        {
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
                                                                 Utility.IllustrationStatus.Subscription,
                                                                 string.Empty,
                                                                 ObjServices.Agent_Id,
                                                                 string.Empty);

                            Session["firstOption"] = false;
                            Utility.ExcecuteJScript(this, string.Format("BackToIllustrationList2('{0}', '{1}');", Resources.StatusChangedSuccessfully, Resources.Success));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.MessageBoxALIF(string.Format("{0}<br /><br /><b>InnerException:</b> {1}", ex.Message, ex.InnerException), null, null, true, Resources.Warning);
                else
                    this.MessageBoxALIF(ex.Message, null, null, true, Resources.Warning);
            }

        }

        protected void drpInspectors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpInspectors.SelectedIndex > 0)
            {
                ObjServices.InspectorAgentId = drpInspectors.SelectedValue.ToInt();
                txtInspeccionadoPor.Text = drpInspectors.SelectedItem.Text;
            }
            else
                ObjServices.InspectorAgentId = 0;
        }

        public void FillDrop()
        {
            var data = new Dictionary<string, string>();

            //drpInspectors
            data.Clear();
            #region Inspectors
            var dataAgent = ObjServices.GettingDropData(Utility.DropDownType.InspectorCot,
                                                            corpId: ObjServices.Corp_Id,
                                                            regionId: ObjServices.Region_Id,
                                                            countryId: ObjServices.Country_Id,
                                                            domesticregId: ObjServices.Domesticreg_Id,
                                                            stateProvId: ObjServices.State_Prov_Id,
                                                            cityId: ObjServices.City_Id,
                                                            officeId: ObjServices.Office_Id,
                                                            caseSeqNo: ObjServices.Case_Seq_No,
                                                            histSeqNo: ObjServices.Hist_Seq_No
                                                            );

            if (dataAgent != null && dataAgent.Count() > 0)
            {
                data.Add("0", Resources.Select);
                foreach (var item in dataAgent)
                {
                    if (!data.ContainsValue(item.AgentName.ToUpper()))
                        data.Add(Convert.ToString(item.AgentId), item.AgentName.ToUpper());
                }

                drpInspectors.Items.Clear();
                drpInspectors.DataSource = data;
                drpInspectors.DataBind();

                drpInspectors.SelectedIndex = 0;

                if (ObjServices.InspectorAgentId != null && ObjServices.InspectorAgentId > 0)
                {
                    drpInspectors.SelectedValue = Convert.ToString(ObjServices.InspectorAgentId);
                    drpInspectors.Enabled = false;
                }
            }

            #endregion
            data.Clear();
        }
    }
}