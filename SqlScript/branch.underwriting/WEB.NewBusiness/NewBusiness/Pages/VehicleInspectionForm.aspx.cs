﻿using DI.UnderWriting;
using Entity.UnderWriting.Entities;
using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using WEB.NewBusiness.Common;
using WEB.NewBusiness.NewBusiness.UserControls.VehicleInspectionForm;

namespace WEB.NewBusiness.NewBusiness.Pages
{
    public partial class VehicleInspectionForm : BasePage
    {
        private static UnderWritingDIManager idManager = new UnderWritingDIManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CompanyId"] = ObjServices.CompanyId;
            Session["ProjectId"] = ObjServices.ProjectId;

        }

        #region WebMethods
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string saveAll(string ValoresJson,
                                     InformacionGeneral InformacionesGenerales,
                                     List<VerificacionInformacionGeneral> VerificacionInformacionesGenerales,
                                     TipoCombustible TipoCombustible,
                                     List<Funcionamiento> Funcionamiento,
                                     ParteFisica PartesFisicas,
                                     AccesorioTapiceria AccesoriosTapiceria,
                                     SeguridadComplemento SeguridadComplementos,
                                     OtraInformacion OtrasInformaciones)
        {

            string msg = string.Empty;
            var requiredDisabled = new int[]
            {
                 Utility.IllustrationStatus.ApprovedBySubscription.Code().ToInt(),
                 Utility.IllustrationStatus.DeclinedByClient.Code().ToInt(),
                 Utility.IllustrationStatus.DeclinedBySubscription.Code().ToInt(),
                 Utility.IllustrationStatus.Effective.Code().ToInt(),
                 Utility.IllustrationStatus.TimeExpired.Code().ToInt(),
                 Utility.IllustrationStatus.Cancelled.Code().ToInt()
            };


            try
            {
                if (string.IsNullOrEmpty(OtrasInformaciones.UsuarioInspeccion))
                {
                    throw new Exception(Resources.YouMustIndicateInspectionUserName);
                }
                //if (string.IsNullOrWhiteSpace(InformacionesGenerales.Inspector))
                //    throw new Exception(string.Format("{0} - saveAll", Resources.YouMustIndicateInspectorName));

                var vj = Newtonsoft.Json.JsonConvert.DeserializeObject<ValoresJSON>(ValoresJson);

                if (vj == null)
                    throw new Exception(string.Format("{0} - saveAll", Resources.DeserializationError));

                //if (policyStatusId == Utility.IllustrationStatus.MissingInspection.ID().ToInt() ||  policyStatusId == Utility.IllustrationStatus.Subscription.ID().ToInt())
                //if (vj.PolicyStatusId == Utility.IllustrationStatus.MissingInspection.ID().ToInt())//ORGINAL 23-06-2017: No importa el status para hacerce la inspeccion
                if (InformacionesGenerales.InspectorID != null && InformacionesGenerales.InspectorID > 0)
                {
                    var objService = new Services();
                    vj.InspectedBy = InformacionesGenerales.InspectorID;

                    objService.AssignIllustrationToSubscriber(vj.CorpId,
                        vj.RegionId,
                        vj.CountryId,
                        vj.DomesticRegId,
                        vj.StateProvId,
                        vj.CityId,
                        vj.OfficeId,
                        vj.CaseSeqNo,
                        vj.HistSeqNo,
                        InformacionesGenerales.InspectorID.GetValueOrDefault(),
                        "Inspector"
                        );

                    objService.UpdateTempTable(vj.Quotation, vj.UserID.GetValueOrDefault());

                }

                if (!requiredDisabled.Contains(vj.PolicyStatusId.GetValueOrDefault()))
                {
                    bool EndosoAclaratorioFuncionamiento = false,
                         EndosoAclaratorioPartesFisicasExterior = false,
                         EndosoAclaratorioPartesFisicasInterior = false,
                         EndosoAclaratorioPartesFisicasOtros = false,
                         EndosoAclaratorioAccesoriosTapiceriaAccesorios = false,
                         EndosoAclaratorioAccesoriosTapiceriaTapiceria = false;

                    var document_category = idManager.VehicleManager.GetDocumentCategory(Utility.VehicleInspectionFormPhotos);

                    #region Informaciones Generales / Otras Informaciones
                    var getReview = idManager.VehicleManager.GetVehicleReview(new Vehicle
                    {
                        CorpId = vj.CorpId,
                        RegionId = vj.RegionId,
                        CountryId = vj.CountryId,
                        DomesticRegId = vj.DomesticRegId,
                        StateProvId = vj.StateProvId,
                        CityId = vj.CityId,
                        OfficeId = vj.OfficeId,
                        CaseSeqNo = vj.CaseSeqNo,
                        HistSeqNo = vj.HistSeqNo,
                        InsuredVehicleId = vj.InsuredVehicleId,
                        MakeId = vj.MarcaId,
                        ModelId = vj.ModeloId
                    }).FirstOrDefault();

                    Vehicle.Review review = new Vehicle.Review();

                    if (getReview != null)
                    {
                        #region Update
                        int? ReviewId = getReview.ReviewId;

                        getReview.CorpId = vj.CorpId;
                        getReview.RegionId = vj.RegionId;
                        getReview.CountryId = vj.CountryId;
                        getReview.DomesticRegId = vj.DomesticRegId;
                        getReview.StateProvId = vj.StateProvId;
                        getReview.CityId = vj.CityId;
                        getReview.OfficeId = vj.OfficeId;
                        getReview.CaseSeqNo = vj.CaseSeqNo;
                        getReview.HistSeqNo = vj.HistSeqNo;
                        getReview.ReviewId = ReviewId;
                        getReview.InsuredVehicleId = vj.InsuredVehicleId;
                        getReview.MakeId = InformacionesGenerales.Marca;
                        getReview.ModelId = InformacionesGenerales.Modelo;
                        getReview.ModelYear = InformacionesGenerales.Ano;
                        getReview.Seats = InformacionesGenerales.Capacidad;
                        getReview.Cylinder = InformacionesGenerales.Cilindros;
                        getReview.RegistryPlate = InformacionesGenerales.Placa;
                        getReview.ColorId = vj.ColorId;
                        getReview.VersionId = InformacionesGenerales.VersionId;
                        getReview.TransmissionTypeId = InformacionesGenerales.TransmisionId;
                        getReview.WheelDriveTypeId = InformacionesGenerales.TraccionId;
                        getReview.VehicleClassId = InformacionesGenerales.ClaseId;
                        getReview.VehicleTypeId = vj.VehicleTypeId;
                        getReview.UsageId = vj.UsageId;
                        getReview.MileageKilometer = InformacionesGenerales.MileageKilometer;
                        getReview.Odometer = InformacionesGenerales.Kilometraje;
                        getReview.RegistrationDocument = InformacionesGenerales.MatriculaDocumentoLegalBL;
                        getReview.InspectedBy = vj.InspectedBy;
                        getReview.ReviewDate = vj.ReviewDate;
                        getReview.Mark = InformacionesGenerales.Chasis;
                        getReview.ReviewStatusId = InformacionesGenerales.PartialSave ? (int?)null : 1;
                        getReview.ReviewStatus = !InformacionesGenerales.PartialSave;
                        getReview.DocTypeId = document_category.DocTypeId;
                        getReview.DocCategoryId = document_category.DocCategoryId;
                        getReview.ReviewNotes = OtrasInformaciones.DictamenDanos;
                        getReview.InspectorSuggestsAcceptRisk = OtrasInformaciones.InspectorSuggestsAcceptRisk;
                        getReview.Phone = InformacionesGenerales.Telefono;
                        getReview.Email = InformacionesGenerales.CorreoElectronico;
                        getReview.ReviewFinishDate = Convert.ToDateTime(OtrasInformaciones.HoraCulminacion, CultureInfo.InvariantCulture);
                        getReview.CreateDate = DateTime.Now;
                        getReview.CreateUserId = vj.UserID;
                        getReview.UsuarioInspeccion = OtrasInformaciones.UsuarioInspeccion;
                        getReview.InspectedByName = InformacionesGenerales.Inspector;
                        review = idManager.VehicleManager.SetVehicleReview(getReview);
                        #endregion
                    }
                    else
                    {
                        #region Insert
                        var horafull = InformacionesGenerales.HoraInicio;
                        var ampmfull = horafull.Split(' ');
                        var array_hora = ampmfull[0].Split(':');
                        var ampm = ampmfull[1];
                        int hora = Convert.ToInt32(array_hora[0]),
                            minutos = Convert.ToInt32(array_hora[1]),
                            segundos = Convert.ToInt32(array_hora[2]);

                        if (ampm == "PM" && hora < 12)
                            hora += 12;

                        var ReviewDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hora, minutos, segundos);

                        review = idManager.VehicleManager.SetVehicleReview(new Vehicle.Review
                        {
                            CorpId = vj.CorpId,
                            RegionId = vj.RegionId,
                            CountryId = vj.CountryId,
                            DomesticRegId = vj.DomesticRegId,
                            StateProvId = vj.StateProvId,
                            CityId = vj.CityId,
                            OfficeId = vj.OfficeId,
                            CaseSeqNo = vj.CaseSeqNo,
                            HistSeqNo = vj.HistSeqNo,
                            InsuredVehicleId = vj.InsuredVehicleId,
                            ReviewId = null,
                            MakeId = InformacionesGenerales.Marca,
                            ModelId = InformacionesGenerales.Modelo,
                            ModelYear = InformacionesGenerales.Ano,
                            Seats = InformacionesGenerales.Capacidad,
                            Cylinder = InformacionesGenerales.Cilindros,
                            RegistryPlate = InformacionesGenerales.Placa,
                            ColorId = vj.ColorId,
                            VersionId = InformacionesGenerales.VersionId,
                            TransmissionTypeId = InformacionesGenerales.TransmisionId,
                            WheelDriveTypeId = InformacionesGenerales.TraccionId,
                            VehicleClassId = InformacionesGenerales.ClaseId,
                            VehicleTypeId = vj.VehicleTypeId,
                            UsageId = vj.UsageId,
                            MileageKilometer = InformacionesGenerales.MileageKilometer,
                            Odometer = InformacionesGenerales.Kilometraje,
                            Hubodometer = null,
                            RegistrationDocument = InformacionesGenerales.MatriculaDocumentoLegalBL,
                            FuelInt = null,
                            InspectedBy = vj.InspectedBy,
                            Capacity = null,
                            ReviewDate = ReviewDate, /*Convert.ToDateTime(string.Format("{0} {1}", DateTime.Now, InformacionesGenerales.HoraInicio), CultureInfo.InvariantCulture),*/
                            ReviewNotes = OtrasInformaciones.DictamenDanos,
                            ReviewAmount = 0M,
                            Mark = InformacionesGenerales.Chasis,
                            ReviewFinishDate = Convert.ToDateTime(OtrasInformaciones.HoraCulminacion, CultureInfo.InvariantCulture),
                            DocTypeId = document_category.DocTypeId,
                            DocCategoryId = document_category.DocCategoryId,
                            DocumentId = null,
                            ReviewStatusId = 1,     /*InformacionesGenerales.PartialSave ? (int?)null : 1,*/
                            ReviewStatus = true,    /*!InformacionesGenerales.PartialSave,*/
                            InspectionNumber = string.Empty,
                            ApplicantInspection = string.Empty,
                            IdentificationDocument = string.Empty,
                            InspectorSuggestsAcceptRisk = OtrasInformaciones.InspectorSuggestsAcceptRisk,
                            Phone = InformacionesGenerales.Telefono,
                            Email = InformacionesGenerales.CorreoElectronico,
                            CreateDate = DateTime.Now,
                            CreateUserId = vj.UserID,
                            InspectedByName = InformacionesGenerales.Inspector,
                            UsuarioInspeccion = OtrasInformaciones.UsuarioInspeccion
                        });
                        #endregion
                    }
                    #endregion

                    #region Eliminar opciones seleccionadas en RadioButtons
                    var del = idManager.VehicleManager.DeleteVehicleReviewDetail(new Vehicle.Review.Detail
                    {
                        CorpId = review.CorpId,
                        RegionId = review.RegionId,
                        CountryId = review.CountryId,
                        DomesticRegId = review.DomesticRegId,
                        StateProvId = review.StateProvId,
                        CityId = review.CityId,
                        OfficeId = review.OfficeId,
                        CaseSeqNo = review.CaseSeqNo,
                        HistSeqNo = review.HistSeqNo,
                        InsuredVehicleId = review.InsuredVehicleId,
                        ReviewId = review.ReviewId
                    });
                    #endregion

                    #region Verificacion de Informaciones Generales
                    if (VerificacionInformacionesGenerales.Count > 0)
                    {
                        var verificacionInformacionesGenerales = VerificacionInformacionesGenerales.Where(v => !v.Erase).ToList();
                        foreach (var verificacion in verificacionInformacionesGenerales)
                        {
                            var vig = idManager.VehicleManager.SetVehicleReviewVIG(new Vehicle.Review.VIG
                            {
                                Corp_Id = review.CorpId,
                                Vehicle_Unique_Id = vj.VehicleUniqueId,
                                VIG_Type_Id = verificacion.ReviewItemId,
                                Good = verificacion.Checked,
                                UserId = vj.InspectedBy
                            });
                        }
                    }
                    #endregion

                    #region Combustible
                    if (!TipoCombustible.Erase)
                    {
                        var combustible = idManager.VehicleManager.SetVehicleReviewDetail(new Vehicle.Review.Detail
                        {
                            CorpId = review.CorpId,
                            RegionId = review.RegionId,
                            CountryId = review.CountryId,
                            DomesticRegId = review.DomesticRegId,
                            StateProvId = review.StateProvId,
                            CityId = review.CityId,
                            OfficeId = review.OfficeId,
                            CaseSeqNo = review.CaseSeqNo,
                            HistSeqNo = review.HistSeqNo,
                            InsuredVehicleId = vj.InsuredVehicleId,
                            ReviewId = review.ReviewId,
                            ReviewDetailId = ((TipoCombustible.ReviewDetailId == 0 || TipoCombustible.ReviewDetailId == null) ? null : TipoCombustible.ReviewDetailId),
                            ReviewGroupId = TipoCombustible.ReviewGroupId,
                            ReviewClassId = TipoCombustible.ReviewClassId,
                            ReviewItemId = TipoCombustible.ReviewItemId,
                            ReviewOptionId = TipoCombustible.ReviewOptionId,
                            Checked = TipoCombustible.Checked,
                            OptionNotes = default(string),
                            ReviewStatus = true,
                            CreateUserId = vj.InspectedBy
                        });
                    }
                    #endregion

                    #region Funcionamiento
                    if (Funcionamiento.Count > 0)
                    {
                        var funcionamiento = Funcionamiento.Where(f => !f.Erase).ToList();
                        EndosoAclaratorioFuncionamiento = funcionamiento.Any(a => a.ReviewOptionDesc == "R" || a.ReviewOptionDesc == "M");

                        #region SetVehicleReviewDetail
                        foreach (var funcion in funcionamiento)
                        {
                            var func = idManager.VehicleManager.SetVehicleReviewDetail(new Vehicle.Review.Detail
                            {
                                CorpId = review.CorpId,
                                RegionId = review.RegionId,
                                CountryId = review.CountryId,
                                DomesticRegId = review.DomesticRegId,
                                StateProvId = review.StateProvId,
                                CityId = review.CityId,
                                OfficeId = review.OfficeId,
                                CaseSeqNo = review.CaseSeqNo,
                                HistSeqNo = review.HistSeqNo,
                                InsuredVehicleId = vj.InsuredVehicleId,
                                ReviewId = review.ReviewId,
                                ReviewDetailId = ((funcion.ReviewDetailId == 0 || funcion.ReviewDetailId == null) ? null : funcion.ReviewDetailId),
                                ReviewGroupId = funcion.ReviewGroupId,
                                ReviewClassId = funcion.ReviewClassId,
                                ReviewItemId = funcion.ReviewItemId,
                                ReviewOptionId = funcion.ReviewOptionId,
                                Checked = funcion.Checked,
                                OptionNotes = default(string),
                                ReviewStatus = true,
                                CreateUserId = vj.InspectedBy
                            });
                        }
                        #endregion
                    }
                    #endregion

                    #region Partes Fisicas
                    #region Exterior
                    if (PartesFisicas.Exterior.Count > 0)
                    {
                        var parFisExt = PartesFisicas.Exterior.Where(e => !e.Erase).ToList();
                        EndosoAclaratorioPartesFisicasExterior = parFisExt.Any(a => a.ReviewOptionDesc == "R" || a.ReviewOptionDesc == "M");

                        #region SetVehicleReviewDetail
                        foreach (var exterior in parFisExt)
                        {
                            var pfe = idManager.VehicleManager.SetVehicleReviewDetail(new Vehicle.Review.Detail
                            {
                                CorpId = review.CorpId,
                                RegionId = review.RegionId,
                                CountryId = review.CountryId,
                                DomesticRegId = review.DomesticRegId,
                                StateProvId = review.StateProvId,
                                CityId = review.CityId,
                                OfficeId = review.OfficeId,
                                CaseSeqNo = review.CaseSeqNo,
                                HistSeqNo = review.HistSeqNo,
                                InsuredVehicleId = vj.InsuredVehicleId,
                                ReviewId = review.ReviewId,
                                ReviewDetailId = ((exterior.ReviewDetailId == 0 || exterior.ReviewDetailId == null) ? null : exterior.ReviewDetailId),
                                ReviewGroupId = exterior.ReviewGroupId,
                                ReviewClassId = exterior.ReviewClassId,
                                ReviewItemId = exterior.ReviewItemId,
                                ReviewOptionId = exterior.ReviewOptionId,
                                Checked = exterior.Checked,
                                OptionNotes = default(string),
                                ReviewStatus = true,
                                CreateUserId = vj.InspectedBy
                            });
                        }
                        #endregion
                    }
                    #endregion

                    #region Interior
                    if (PartesFisicas.Interior.Count > 0)
                    {
                        var parFisInt = PartesFisicas.Interior.Where(i => !i.Erase).ToList();
                        EndosoAclaratorioPartesFisicasInterior = parFisInt.Any(a => a.ReviewOptionDesc == "R" || a.ReviewOptionDesc == "M");

                        #region SetVehicleReviewDetail
                        foreach (var interior in parFisInt)
                        {
                            var pfi = idManager.VehicleManager.SetVehicleReviewDetail(new Vehicle.Review.Detail
                            {
                                CorpId = review.CorpId,
                                RegionId = review.RegionId,
                                CountryId = review.CountryId,
                                DomesticRegId = review.DomesticRegId,
                                StateProvId = review.StateProvId,
                                CityId = review.CityId,
                                OfficeId = review.OfficeId,
                                CaseSeqNo = review.CaseSeqNo,
                                HistSeqNo = review.HistSeqNo,
                                InsuredVehicleId = vj.InsuredVehicleId,
                                ReviewId = review.ReviewId,
                                ReviewDetailId = ((interior.ReviewDetailId == 0 || interior.ReviewDetailId == null) ? null : interior.ReviewDetailId),
                                ReviewGroupId = interior.ReviewGroupId,
                                ReviewClassId = interior.ReviewClassId,
                                ReviewItemId = interior.ReviewItemId,
                                ReviewOptionId = interior.ReviewOptionId,
                                Checked = interior.Checked,
                                OptionNotes = default(string),
                                ReviewStatus = true,
                                CreateUserId = vj.InspectedBy
                            });
                        }
                        #endregion
                    }
                    #endregion

                    #region Otros
                    if (PartesFisicas.Otros.Count > 0)
                    {
                        var parFisOtr = PartesFisicas.Otros.Where(o => !o.Erase).ToList();
                        EndosoAclaratorioPartesFisicasOtros = parFisOtr.Any(a => a.ReviewOptionDesc == "R" || a.ReviewOptionDesc == "M");

                        #region SetVehicleReviewDetail
                        foreach (var otro in parFisOtr)
                        {
                            var otr = idManager.VehicleManager.SetVehicleReviewDetail(new Vehicle.Review.Detail
                            {
                                CorpId = review.CorpId,
                                RegionId = review.RegionId,
                                CountryId = review.CountryId,
                                DomesticRegId = review.DomesticRegId,
                                StateProvId = review.StateProvId,
                                CityId = review.CityId,
                                OfficeId = review.OfficeId,
                                CaseSeqNo = review.CaseSeqNo,
                                HistSeqNo = review.HistSeqNo,
                                InsuredVehicleId = vj.InsuredVehicleId,
                                ReviewId = review.ReviewId,
                                ReviewDetailId = ((otro.ReviewDetailId == 0 || otro.ReviewDetailId == null) ? null : otro.ReviewDetailId),
                                ReviewGroupId = otro.ReviewGroupId,
                                ReviewClassId = otro.ReviewClassId,
                                ReviewItemId = otro.ReviewItemId,
                                ReviewOptionId = otro.ReviewOptionId,
                                Checked = otro.Checked,
                                OptionNotes = default(string),
                                ReviewStatus = true,
                                CreateUserId = vj.InspectedBy
                            });
                        }
                        #endregion
                    }
                    #endregion
                    #endregion

                    #region Accesorios y Tapiceria
                    #region Accesorios
                    if (AccesoriosTapiceria.Accesorios.Count > 0)
                    {
                        var accTapAcc = AccesoriosTapiceria.Accesorios.Where(a => !a.Erase).ToList();
                        EndosoAclaratorioAccesoriosTapiceriaAccesorios = accTapAcc.Any(a => a.ReviewOptionDesc == "R" || a.ReviewOptionDesc == "M");

                        #region SetVehicleReviewDetail
                        foreach (var accesorio in accTapAcc)
                        {
                            var acc = idManager.VehicleManager.SetVehicleReviewDetail(new Vehicle.Review.Detail
                            {
                                CorpId = review.CorpId,
                                RegionId = review.RegionId,
                                CountryId = review.CountryId,
                                DomesticRegId = review.DomesticRegId,
                                StateProvId = review.StateProvId,
                                CityId = review.CityId,
                                OfficeId = review.OfficeId,
                                CaseSeqNo = review.CaseSeqNo,
                                HistSeqNo = review.HistSeqNo,
                                InsuredVehicleId = vj.InsuredVehicleId,
                                ReviewId = review.ReviewId,
                                ReviewDetailId = ((accesorio.ReviewDetailId == 0 || accesorio.ReviewDetailId == null) ? null : accesorio.ReviewDetailId),
                                ReviewGroupId = accesorio.ReviewGroupId,
                                ReviewClassId = accesorio.ReviewClassId,
                                ReviewItemId = accesorio.ReviewItemId,
                                ReviewOptionId = accesorio.ReviewOptionId,
                                Checked = accesorio.Checked,
                                OptionNotes = default(string),
                                ReviewStatus = true,
                                CreateUserId = vj.InspectedBy
                            });
                        }
                        #endregion
                    }
                    #endregion

                    #region Tapiceria
                    if (AccesoriosTapiceria.Tapiceria.Count > 0)
                    {
                        var accTapTap = AccesoriosTapiceria.Tapiceria.Where(t => !t.Erase).ToList();
                        EndosoAclaratorioAccesoriosTapiceriaTapiceria = accTapTap.Any(a => a.ReviewOptionDesc == "R" || a.ReviewOptionDesc == "M");

                        #region SetVehicleReviewDetail
                        foreach (var tapiceria in accTapTap)
                        {
                            var tap = idManager.VehicleManager.SetVehicleReviewDetail(new Vehicle.Review.Detail
                            {
                                CorpId = review.CorpId,
                                RegionId = review.RegionId,
                                CountryId = review.CountryId,
                                DomesticRegId = review.DomesticRegId,
                                StateProvId = review.StateProvId,
                                CityId = review.CityId,
                                OfficeId = review.OfficeId,
                                CaseSeqNo = review.CaseSeqNo,
                                HistSeqNo = review.HistSeqNo,
                                InsuredVehicleId = vj.InsuredVehicleId,
                                ReviewId = review.ReviewId,
                                ReviewDetailId = ((tapiceria.ReviewDetailId == 0 || tapiceria.ReviewDetailId == null) ? null : tapiceria.ReviewDetailId),
                                ReviewGroupId = tapiceria.ReviewGroupId,
                                ReviewClassId = tapiceria.ReviewClassId,
                                ReviewItemId = tapiceria.ReviewItemId,
                                ReviewOptionId = tapiceria.ReviewOptionId,
                                Checked = tapiceria.Checked,
                                OptionNotes = default(string),
                                ReviewStatus = true,
                                CreateUserId = vj.InspectedBy
                            });
                        }
                        #endregion
                    }
                    #endregion
                    #endregion

                    #region Seguridad y Complementos
                    #region Seguridad
                    if (SeguridadComplementos.Seguridad.Count > 0)
                    {
                        var segComSeg = SeguridadComplementos.Seguridad.Where(s => !s.Erase).ToList();
                        foreach (var seguridad in segComSeg)
                        {
                            var seg = idManager.VehicleManager.SetVehicleReviewDetail(new Vehicle.Review.Detail
                            {
                                CorpId = review.CorpId,
                                RegionId = review.RegionId,
                                CountryId = review.CountryId,
                                DomesticRegId = review.DomesticRegId,
                                StateProvId = review.StateProvId,
                                CityId = review.CityId,
                                OfficeId = review.OfficeId,
                                CaseSeqNo = review.CaseSeqNo,
                                HistSeqNo = review.HistSeqNo,
                                InsuredVehicleId = vj.InsuredVehicleId,
                                ReviewId = review.ReviewId,
                                ReviewDetailId = ((seguridad.ReviewDetailId == 0 || seguridad.ReviewDetailId == null) ? null : seguridad.ReviewDetailId),
                                ReviewGroupId = seguridad.ReviewGroupId,
                                ReviewClassId = seguridad.ReviewClassId,
                                ReviewItemId = seguridad.ReviewItemId,
                                ReviewOptionId = seguridad.ReviewOptionId,
                                Checked = seguridad.Checked,
                                OptionNotes = default(string),
                                ReviewStatus = true,
                                CreateUserId = vj.InspectedBy
                            });
                        }
                    }
                    #endregion

                    #region Complementos
                    if (SeguridadComplementos.Complementos.Count > 0)
                    {
                        var SegComCom = SeguridadComplementos.Complementos.Where(c => !c.Erase).ToList();
                        foreach (var complemento in SegComCom)
                        {
                            var com = idManager.VehicleManager.SetVehicleReviewDetail(new Vehicle.Review.Detail
                            {
                                CorpId = review.CorpId,
                                RegionId = review.RegionId,
                                CountryId = review.CountryId,
                                DomesticRegId = review.DomesticRegId,
                                StateProvId = review.StateProvId,
                                CityId = review.CityId,
                                OfficeId = review.OfficeId,
                                CaseSeqNo = review.CaseSeqNo,
                                HistSeqNo = review.HistSeqNo,
                                InsuredVehicleId = vj.InsuredVehicleId,
                                ReviewId = review.ReviewId,
                                ReviewDetailId = ((complemento.ReviewDetailId == 0 || complemento.ReviewDetailId == null) ? null : complemento.ReviewDetailId),
                                ReviewGroupId = complemento.ReviewGroupId,
                                ReviewClassId = complemento.ReviewClassId,
                                ReviewItemId = complemento.ReviewItemId,
                                ReviewOptionId = complemento.ReviewOptionId,
                                Checked = complemento.Checked,
                                OptionNotes = default(string),
                                ReviewStatus = true,
                                CreateUserId = vj.InspectedBy
                            });
                        }
                    }
                    #endregion
                    #endregion

                    #region Endoso Aclaratorio
                    bool EndorsementClarifying = (EndosoAclaratorioFuncionamiento ||
                                                  EndosoAclaratorioPartesFisicasExterior ||
                                                  EndosoAclaratorioPartesFisicasInterior ||
                                                  EndosoAclaratorioPartesFisicasOtros ||
                                                  EndosoAclaratorioAccesoriosTapiceriaAccesorios ||
                                                  EndosoAclaratorioAccesoriosTapiceriaTapiceria);

                    #endregion

                    #region Actualizar campo Inspection/EndorsementClarifying en [Policy].[PL_POLICY_VEHICLE_INSURED]
                    var inspected = idManager.PolicyManager.SetVehicleInsuredInspection(new Policy.VehicleInsured.InspectionV
                    {
                        CorpId = review.CorpId,
                        RegionId = review.RegionId,
                        CountryId = review.CountryId,
                        DomesticRegId = review.DomesticRegId,
                        StateProvId = review.StateProvId,
                        CityId = review.CityId,
                        OfficeId = review.OfficeId,
                        CaseSeqNo = review.CaseSeqNo,
                        HistSeqNo = review.HistSeqNo,
                        VehicleUniqueId = vj.VehicleUniqueId,
                        Inspection = InformacionesGenerales.Inspeccionado,
                        EndorsementClarifying = EndorsementClarifying,
                        UserId = vj.InspectedBy.GetValueOrDefault()
                    });
                    #endregion

                    #region Actualizar direccion de inspeccion en [Policy].[PL_PCY_VEHICLE_REVIEWS]
                    var setStatus = idManager.PolicyManager.SetVehicleInsuredInspectionAddress(new Policy.VehicleInsured.InspectionV
                    {
                        CorpId = vj.CorpId,
                        RegionId = vj.RegionId,
                        CountryId = vj.CountryId,
                        DomesticRegId = vj.DomesticRegId,
                        StateProvId = vj.StateProvId,
                        CityId = vj.CityId,
                        OfficeId = vj.OfficeId,
                        CaseSeqNo = vj.CaseSeqNo,
                        HistSeqNo = vj.HistSeqNo,
                        InsuredVehicleId = vj.InsuredVehicleId,
                        InspectionAddress = vj.InspectionAddress,
                        UserId = vj.UserID.GetValueOrDefault()
                    });
                    #endregion

                    //Guardar la firma del cliente
                    if (!string.IsNullOrEmpty(OtrasInformaciones.FirmaCiente))
                    {
                        //Verificar si ya se ha guardado previamente la firma
                        var ExistSign = idManager.PolicyManager.GetCustomerSing(vj.CorpId,
                                                                                 vj.RegionId,
                                                                                 vj.CountryId,
                                                                                 vj.DomesticRegId,
                                                                                 vj.StateProvId,
                                                                                 vj.CityId,
                                                                                 vj.OfficeId,
                                                                                 vj.CaseSeqNo,
                                                                                 vj.HistSeqNo
                                                                                 ) != null;

                        if (!ExistSign)
                        {
                            idManager.PolicyManager.SetCustomerSing(vj.CorpId,
                                                                    vj.RegionId,
                                                                    vj.CountryId,
                                                                    vj.DomesticRegId,
                                                                    vj.StateProvId,
                                                                    vj.CityId,
                                                                    vj.OfficeId,
                                                                    vj.CaseSeqNo,
                                                                    vj.HistSeqNo,
                                                                    OtrasInformaciones.FirmaCiente
                                                                   );
                        }
                    }

                    msg = vj.Inspection ? "FormUpdated" : "FormCreated";
                }
                else
                {
                    msg = "QuotationNotInMissingInspection";
                }
            }
            catch (Exception ex)
            {
                msg = string.Format("Exception: {0} , {1}", ex.Message, ex.InnerException);
            }

            return msg;
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<VehiclePics> PhotoReviews(string ValoresJson)
        {
            var vj = Newtonsoft.Json.JsonConvert.DeserializeObject<ValoresJSON>(ValoresJson);

            if (vj == null)
                throw new Exception(string.Format("{0} - PhotoReviews", Resources.DeserializationError));

            List<VehiclePics> photosSaved = new List<VehiclePics>() { };

            try
            {
                var document_category = idManager.VehicleManager.GetDocumentCategory(Utility.VehicleInspectionFormPhotos);

                List<Vehicle.Review.Pic> photos = idManager.VehicleManager.GetVehicleReviewPic(new Vehicle.Review
                {
                    CorpId = vj.CorpId,
                    RegionId = vj.RegionId,
                    CountryId = vj.CountryId,
                    DomesticRegId = vj.DomesticRegId,
                    StateProvId = vj.StateProvId,
                    CityId = vj.CityId,
                    OfficeId = vj.OfficeId,
                    CaseSeqNo = vj.CaseSeqNo,
                    HistSeqNo = vj.HistSeqNo,
                    InsuredVehicleId = vj.InsuredVehicleId,
                    ReviewId = vj.ReviewId
                }).Where(f => f.PictureStatus.GetValueOrDefault() && f.DocTypeId == document_category.DocTypeId && f.DocCategoryId == document_category.DocCategoryId).ToList();

                if (photos.Count > 0 && vj.Inspection)
                {
                    photosSaved.Clear();
                    string guid = Guid.NewGuid().ToString();
                    foreach (var photo in photos)
                    {
                        string imgFileName = string.Format("{0}_{1}_{2}.jpg", vj.UserID, photo.DocumentName, guid),
                               Base64String = Convert.ToBase64String(photo.DocumentBinary),
                               img = "bodyContent_InspectionForm_Fotos1_Foto{0}_{1}";

                        photosSaved.Add(new VehiclePics
                        {
                            DocumentId = photo.DocumentId,
                            DocumentName = photo.DocumentName,
                            DocumentDesc = photo.DocumentDesc,
                            imgId = string.Format(img, photo.DocumentName, "imgFoto"),
                            imgRelativePath = string.Empty,
                            lnkId = string.Format(img, photo.DocumentName, "lnkFoto"),
                            Base64String = string.Format(@"data:image/jpeg;base64,{0}", Base64String),
                            Mensaje = string.Empty
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                photosSaved.Add(new VehiclePics { Mensaje = string.Format("Exception: {0}", ex.Message) });
            }
            return photosSaved;
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static ReviewDetails OptionsReviews(string ValoresJson)
        {
            var vj = Newtonsoft.Json.JsonConvert.DeserializeObject<ValoresJSON>(ValoresJson);

            if (vj == null)
                throw new Exception(string.Format("{0} - OptionReviews", Resources.DeserializationError));

            ReviewDetails details = new ReviewDetails();

            try
            {
                int? vehicleReviewId = vj.VehicleReviewId;
                int insuredVehicleId = vj.InsuredVehicleId;
                long VehicleUniqueId = vj.VehicleUniqueId;

                List<Vehicle.Review.Detail> reviewDetails = idManager.VehicleManager.GetVehicleReviewDetail(new Vehicle.Review
                {
                    CorpId = vj.CorpId,
                    RegionId = vj.RegionId,
                    CountryId = vj.CountryId,
                    DomesticRegId = vj.DomesticRegId,
                    StateProvId = vj.StateProvId,
                    CityId = vj.CityId,
                    OfficeId = vj.OfficeId,
                    CaseSeqNo = vj.CaseSeqNo,
                    HistSeqNo = vj.HistSeqNo,
                    InsuredVehicleId = insuredVehicleId,
                    ReviewId = vehicleReviewId,
                    LanguageId = vj.LanguageId
                }).ToList();

                details.Count = reviewDetails.Count;

                if (reviewDetails.Count > 0)
                {
                    #region Combustibles
                    details.TipoCombustible = new TipoCombustible();
                    details.TipoCombustible = reviewDetails.Where(c => c.ReviewClassDesc == "Combustibles")
                                                           .Select(c => new TipoCombustible
                                                           {
                                                               Checked = c.Checked.Value,
                                                               Erase = false,
                                                               ReviewClassId = c.ReviewClassId,
                                                               ReviewDetailId = c.ReviewDetailId,
                                                               ReviewGroupId = c.ReviewGroupId,
                                                               ReviewItemId = c.ReviewItemId,
                                                               ReviewOptionId = c.ReviewOptionId
                                                           }).FirstOrDefault();
                    #endregion

                    #region Funcionamiento
                    details.Funcionamiento = new List<Funcionamiento>() { };
                    details.Funcionamiento = reviewDetails.Where(f => f.ReviewClassDesc == "Funcionamiento")
                                                          .Select(f => new Funcionamiento
                                                          {
                                                              Checked = f.Checked.Value,
                                                              Erase = false,
                                                              ReviewClassId = f.ReviewClassId,
                                                              ReviewDetailId = f.ReviewDetailId,
                                                              ReviewGroupId = f.ReviewGroupId,
                                                              ReviewItemId = f.ReviewItemId,
                                                              ReviewOptionId = f.ReviewOptionId
                                                          }).ToList();
                    #endregion

                    #region Partes Fisicas
                    ParteFisica partesFisicas = new ParteFisica();

                    #region Exterior
                    partesFisicas.Exterior = new List<ParteFisicaExterior>() { };
                    partesFisicas.Exterior = reviewDetails.Where(pfe => pfe.ReviewClassDesc == "Exterior")
                                                          .Select(pfe => new ParteFisicaExterior
                                                          {
                                                              Checked = pfe.Checked.Value,
                                                              Erase = false,
                                                              ReviewClassId = pfe.ReviewClassId,
                                                              ReviewDetailId = pfe.ReviewDetailId,
                                                              ReviewGroupId = pfe.ReviewGroupId,
                                                              ReviewItemId = pfe.ReviewItemId,
                                                              ReviewOptionId = pfe.ReviewOptionId
                                                          }).ToList();
                    #endregion

                    #region Interior
                    partesFisicas.Interior = new List<ParteFisicaInterior>() { };
                    partesFisicas.Interior = reviewDetails.Where(pfi => pfi.ReviewClassDesc == "Interior")
                                                          .Select(pfi => new ParteFisicaInterior
                                                          {
                                                              Checked = pfi.Checked.Value,
                                                              Erase = false,
                                                              ReviewClassId = pfi.ReviewClassId,
                                                              ReviewDetailId = pfi.ReviewDetailId,
                                                              ReviewGroupId = pfi.ReviewGroupId,
                                                              ReviewItemId = pfi.ReviewItemId,
                                                              ReviewOptionId = pfi.ReviewOptionId
                                                          }).ToList();
                    #endregion

                    #region Otros
                    partesFisicas.Otros = new List<ParteFisicaOtro>() { };
                    partesFisicas.Otros = reviewDetails.Where(pfo => pfo.ReviewClassDesc == "Otros")
                                                       .Select(pfo => new ParteFisicaOtro
                                                       {
                                                           Checked = pfo.Checked.Value,
                                                           Erase = false,
                                                           ReviewClassId = pfo.ReviewClassId,
                                                           ReviewDetailId = pfo.ReviewDetailId,
                                                           ReviewGroupId = pfo.ReviewGroupId,
                                                           ReviewItemId = pfo.ReviewItemId,
                                                           ReviewOptionId = pfo.ReviewOptionId
                                                       }).ToList();
                    #endregion

                    details.PartesFisicas = partesFisicas;
                    #endregion

                    #region Accesorios y Tapiceria
                    AccesorioTapiceria accesoriosTapiceria = new AccesorioTapiceria();

                    #region Accesorios
                    accesoriosTapiceria.Accesorios = new List<Accesorio>() { };
                    accesoriosTapiceria.Accesorios = reviewDetails.Where(ata => ata.ReviewClassDesc == "Accesorios")
                                                                  .Select(ata => new Accesorio
                                                                  {
                                                                      Checked = ata.Checked.Value,
                                                                      Erase = false,
                                                                      ReviewClassId = ata.ReviewClassId,
                                                                      ReviewDetailId = ata.ReviewDetailId,
                                                                      ReviewGroupId = ata.ReviewGroupId,
                                                                      ReviewItemId = ata.ReviewItemId,
                                                                      ReviewOptionId = ata.ReviewOptionId
                                                                  }).ToList();
                    #endregion

                    #region Tapiceria
                    accesoriosTapiceria.Tapiceria = new List<Tapiceria>() { };
                    accesoriosTapiceria.Tapiceria = reviewDetails.Where(att => att.ReviewClassDesc == "Tapiceria")
                                                                 .Select(att => new Tapiceria
                                                                 {
                                                                     Checked = att.Checked.Value,
                                                                     Erase = false,
                                                                     ReviewClassId = att.ReviewClassId,
                                                                     ReviewDetailId = att.ReviewDetailId,
                                                                     ReviewGroupId = att.ReviewGroupId,
                                                                     ReviewItemId = att.ReviewItemId,
                                                                     ReviewOptionId = att.ReviewOptionId
                                                                 }).ToList();
                    #endregion

                    details.AccesoriosTapiceria = accesoriosTapiceria;
                    #endregion

                    #region Seguridad y Complementos
                    SeguridadComplemento seguridadComplemento = new SeguridadComplemento();

                    #region Seguridad
                    seguridadComplemento.Seguridad = new List<Seguridad>() { };
                    seguridadComplemento.Seguridad = reviewDetails.Where(scs => scs.ReviewClassDesc == "Seguridad")
                                                                  .Select(scs => new Seguridad
                                                                  {
                                                                      Checked = scs.Checked.Value,
                                                                      Erase = false,
                                                                      ReviewClassId = scs.ReviewClassId,
                                                                      ReviewDetailId = scs.ReviewDetailId,
                                                                      ReviewGroupId = scs.ReviewGroupId,
                                                                      ReviewItemId = scs.ReviewItemId,
                                                                      ReviewOptionId = scs.ReviewOptionId
                                                                  }).ToList();
                    #endregion

                    #region Complementos
                    seguridadComplemento.Complementos = new List<Complemento>() { };
                    seguridadComplemento.Complementos = reviewDetails.Where(scc => scc.ReviewClassDesc == "Complementos")
                                                                     .Select(scc => new Complemento
                                                                     {
                                                                         Checked = scc.Checked.Value,
                                                                         Erase = false,
                                                                         ReviewClassId = scc.ReviewClassId,
                                                                         ReviewDetailId = scc.ReviewDetailId,
                                                                         ReviewGroupId = scc.ReviewGroupId,
                                                                         ReviewItemId = scc.ReviewItemId,
                                                                         ReviewOptionId = scc.ReviewOptionId
                                                                     }).ToList();
                    #endregion

                    details.SeguridadComplementos = seguridadComplemento;
                    #endregion
                }

                List<Vehicle.Review.VIG> vigs = idManager.VehicleManager.GetVehicleReviewVIG(new Vehicle.Review.VIG
                {
                    Corp_Id = vj.CorpId,
                    Vehicle_Unique_Id = VehicleUniqueId
                }).ToList();

                details.VIGCount = vigs.Count;

                if (vigs.Count > 0)
                {
                    details.VerificacionInformacionesGenerales = vigs.Select(v => new VerificacionInformacionGeneral
                    {
                        Checked = v.Good.Value,
                        Erase = false,
                        ReviewClassId = -1,
                        ReviewDetailId = -1,
                        ReviewGroupId = -1,
                        ReviewItemId = v.VIG_Type_Id.GetValueOrDefault(),
                        ReviewOptionId = -1
                    }).ToList();
                }

                details.Message = string.Empty;
            }
            catch (Exception ex)
            {
                details.Message = string.Format("Exception: {0}.", ex.Message);
            }

            return details;
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static OtraInformacion OtherInformation(string ValoresJson)
        {
            var vj = Newtonsoft.Json.JsonConvert.DeserializeObject<OtraInformacion>(ValoresJson);

            if (vj == null)
                throw new Exception(string.Format("{0} - OtherInformation", Resources.DeserializationError));


            var Firma = idManager.PolicyManager.GetCustomerSing(vj.CorpId, vj.RegionId, vj.CountryId, vj.DomesticRegId, vj.StateProvId, vj.CityId, vj.OfficeId, vj.CaseSeqNo, vj.HistSeqNo);

            OtraInformacion oi = new OtraInformacion();
            try
            {
                oi.DictamenDanos = vj.DictamenDanos;
                oi.HoraCulminacion = vj.HoraCulminacion;
                oi.InspectorSuggestsAcceptRisk = vj.InspectorSuggestsAcceptRisk;
                oi.Sucursal = vj.Sucursal;
                oi.UsuarioInspeccion = vj.UsuarioInspeccion;
                oi.FirmaCiente = string.IsNullOrEmpty(Firma) ? string.Empty : Firma;
            }
            catch (Exception ex)
            {
                oi.Mensaje = string.Format("Exception: {0}.", ex.Message);
            }
            return oi;
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string savePhoto(string ValoresJson,
                                       InformacionGeneral InformacionesGenerales,
                                       Photo photo,
                                       OtraInformacion OtrasInformaciones)
        {
            string ret = ret = Resources.SelectedPhotoCouldNotBeUploaded,
                   exm = string.Empty,
                   msg = string.Empty;

            ValoresJSON vj = new ValoresJSON();

            int CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]),
                ProjectId = Convert.ToInt32(HttpContext.Current.Session["ProjectId"]);

            try
            {
                vj = Newtonsoft.Json.JsonConvert.DeserializeObject<ValoresJSON>(ValoresJson);
                if (vj == null)
                    throw new Exception(string.Format("{0} - savePhoto", Resources.DeserializationError));


                if (InformacionesGenerales != null && photo != null && OtrasInformaciones != null)
                {
                    msg = string.Format("La foto seleccionada [{0}] no pudo ser cargada.", photo.DocumentDesc);

                    #region Guardar Review / Foto
                    int DocumentId = 0;

                    var document_category = idManager.VehicleManager.GetDocumentCategory(Utility.VehicleInspectionFormPhotos);

                    int? ReviewId = null;

                    #region Verificar si existe review / Guardar review
                    var getReview = idManager.VehicleManager.GetVehicleReview(new Vehicle
                    {
                        CorpId = vj.CorpId,
                        RegionId = vj.RegionId,
                        CountryId = vj.CountryId,
                        DomesticRegId = vj.DomesticRegId,
                        StateProvId = vj.StateProvId,
                        CityId = vj.CityId,
                        OfficeId = vj.OfficeId,
                        CaseSeqNo = vj.CaseSeqNo,
                        HistSeqNo = vj.HistSeqNo,
                        InsuredVehicleId = vj.InsuredVehicleId,
                        MakeId = vj.MarcaId,
                        ModelId = vj.ModeloId
                    }).FirstOrDefault();

                    Vehicle.Review review = new Vehicle.Review();

                    if (getReview != null)
                    {
                        #region Update
                        ReviewId = getReview.ReviewId;

                        getReview.CorpId = vj.CorpId;
                        getReview.RegionId = vj.RegionId;
                        getReview.CountryId = vj.CountryId;
                        getReview.DomesticRegId = vj.DomesticRegId;
                        getReview.StateProvId = vj.StateProvId;
                        getReview.CityId = vj.CityId;
                        getReview.OfficeId = vj.OfficeId;
                        getReview.CaseSeqNo = vj.CaseSeqNo;
                        getReview.HistSeqNo = vj.HistSeqNo;
                        getReview.ReviewId = ReviewId;
                        getReview.InsuredVehicleId = vj.InsuredVehicleId;
                        getReview.MakeId = InformacionesGenerales.Marca;
                        getReview.ModelId = InformacionesGenerales.Modelo;
                        getReview.ModelYear = InformacionesGenerales.Ano;
                        getReview.Seats = InformacionesGenerales.Capacidad;
                        getReview.Cylinder = InformacionesGenerales.Cilindros;
                        getReview.RegistryPlate = InformacionesGenerales.Placa;
                        getReview.ColorId = vj.ColorId;
                        getReview.VersionId = InformacionesGenerales.VersionId;
                        getReview.TransmissionTypeId = InformacionesGenerales.TransmisionId;
                        getReview.WheelDriveTypeId = InformacionesGenerales.TraccionId;
                        getReview.VehicleClassId = InformacionesGenerales.ClaseId;
                        getReview.VehicleTypeId = vj.VehicleTypeId;
                        getReview.UsageId = vj.UsageId;
                        getReview.MileageKilometer = InformacionesGenerales.MileageKilometer;
                        getReview.Odometer = InformacionesGenerales.Kilometraje;
                        getReview.RegistrationDocument = InformacionesGenerales.MatriculaDocumentoLegalBL;
                        getReview.InspectedBy = vj.InspectedBy;
                        getReview.ReviewDate = vj.ReviewDate;
                        getReview.Mark = InformacionesGenerales.Chasis;
                        getReview.ReviewStatusId = null;
                        getReview.ReviewStatus = false;
                        getReview.DocTypeId = document_category.DocTypeId;
                        getReview.DocCategoryId = document_category.DocCategoryId;
                        getReview.ReviewNotes = OtrasInformaciones.DictamenDanos;
                        getReview.InspectorSuggestsAcceptRisk = OtrasInformaciones.InspectorSuggestsAcceptRisk;
                        getReview.Phone = InformacionesGenerales.Telefono;
                        getReview.Email = InformacionesGenerales.CorreoElectronico;
                        getReview.ReviewFinishDate = Convert.ToDateTime(OtrasInformaciones.HoraCulminacion, CultureInfo.InvariantCulture);
                        getReview.CreateDate = DateTime.Now;
                        getReview.CreateUserId = vj.UserID;

                        review = idManager.VehicleManager.SetVehicleReview(getReview);
                        #endregion
                    }
                    else
                    {
                        #region Insert
                        var horafull = InformacionesGenerales.HoraInicio;
                        var ampmfull = horafull.Split(' ');
                        var array_hora = ampmfull[0].Split(':');
                        var ampm = ampmfull[1];
                        int hora = Convert.ToInt32(array_hora[0]),
                            minutos = Convert.ToInt32(array_hora[1]),
                            segundos = Convert.ToInt32(array_hora[2]);

                        if (ampm == "PM" && hora < 12)
                            hora += 12;

                        var ReviewDate = new DateTime(DateTime.Now.Year,
                                                      DateTime.Now.Month,
                                                      DateTime.Now.Day,
                                                      hora,
                                                      minutos,
                                                      segundos);

                        review = idManager.VehicleManager.SetVehicleReview(new Vehicle.Review
                        {
                            CorpId = vj.CorpId,
                            RegionId = vj.RegionId,
                            CountryId = vj.CountryId,
                            DomesticRegId = vj.DomesticRegId,
                            StateProvId = vj.StateProvId,
                            CityId = vj.CityId,
                            OfficeId = vj.OfficeId,
                            CaseSeqNo = vj.CaseSeqNo,
                            HistSeqNo = vj.HistSeqNo,
                            InsuredVehicleId = vj.InsuredVehicleId,
                            ReviewId = null,
                            MakeId = InformacionesGenerales.Marca,
                            ModelId = InformacionesGenerales.Modelo,
                            ModelYear = InformacionesGenerales.Ano,
                            Seats = InformacionesGenerales.Capacidad,
                            Cylinder = InformacionesGenerales.Cilindros,
                            RegistryPlate = InformacionesGenerales.Placa,
                            ColorId = vj.ColorId,
                            VersionId = InformacionesGenerales.VersionId,
                            TransmissionTypeId = InformacionesGenerales.TransmisionId,
                            WheelDriveTypeId = InformacionesGenerales.TraccionId,
                            VehicleClassId = InformacionesGenerales.ClaseId,
                            VehicleTypeId = vj.VehicleTypeId,
                            UsageId = vj.UsageId,
                            MileageKilometer = InformacionesGenerales.MileageKilometer,
                            Odometer = InformacionesGenerales.Kilometraje,
                            Hubodometer = null,
                            RegistrationDocument = InformacionesGenerales.MatriculaDocumentoLegalBL,
                            FuelInt = null,
                            InspectedBy = vj.InspectedBy,
                            Capacity = null,
                            ReviewDate = ReviewDate,
                            ReviewNotes = OtrasInformaciones.DictamenDanos,
                            ReviewAmount = 0M,
                            Mark = InformacionesGenerales.Chasis,
                            ReviewFinishDate = Convert.ToDateTime(OtrasInformaciones.HoraCulminacion, CultureInfo.InvariantCulture),
                            DocTypeId = document_category.DocTypeId,
                            DocCategoryId = document_category.DocCategoryId,
                            DocumentId = null,
                            ReviewStatusId = InformacionesGenerales.PartialSave ? (int?)null : 1,
                            ReviewStatus = !InformacionesGenerales.PartialSave,
                            InspectionNumber = string.Empty,
                            ApplicantInspection = string.Empty,
                            IdentificationDocument = string.Empty,
                            InspectorSuggestsAcceptRisk = OtrasInformaciones.InspectorSuggestsAcceptRisk,
                            Phone = InformacionesGenerales.Telefono,
                            Email = InformacionesGenerales.CorreoElectronico,
                            CreateDate = DateTime.Now,
                            CreateUserId = vj.UserID
                        });
                        #endregion
                    }

                    if (ReviewId == null)
                        ReviewId = review.ReviewId;
                    #endregion

                    #region Verificar si existe ReviewPic / Obtener DocumentId
                    var ReviewPics = idManager.VehicleManager.GetVehicleReviewPic(new Vehicle.Review
                    {
                        CorpId = vj.CorpId,
                        RegionId = vj.RegionId,
                        CountryId = vj.CountryId,
                        DomesticRegId = vj.DomesticRegId,
                        StateProvId = vj.StateProvId,
                        CityId = vj.CityId,
                        OfficeId = vj.OfficeId,
                        CaseSeqNo = vj.CaseSeqNo,
                        HistSeqNo = vj.HistSeqNo,
                        InsuredVehicleId = vj.InsuredVehicleId,
                        ReviewId = ReviewId
                    }).ToList();

                    Vehicle.Review.Pic reviewPic = null;

                    if (ReviewPics.Count > 0)
                    {
                        reviewPic = new Vehicle.Review.Pic();
                        reviewPic = ReviewPics.FirstOrDefault(p => p.DocumentName.Trim().ToLower() == photo.DocumentName.Trim().ToLower() &&
                                                                   p.DocTypeId == document_category.DocTypeId &&
                                                                   p.DocCategoryId == document_category.DocCategoryId);

                        DocumentId = reviewPic != null ? reviewPic.DocumentId.GetValueOrDefault() : 0;
                    }
                    #endregion

                    #region Guardar foto
                    string oldValue = photo.Base64String.Substring(0, (photo.Base64String.IndexOf(",") + 1));
                    byte[] fromBase64String = Convert.FromBase64String(photo.Base64String.Replace(oldValue, string.Empty));
                    byte[] DocumentBinary = Utility.CompressImage(fromBase64String, 30);

                    var doc = idManager.VehicleManager.SetDocument(new Vehicle.Document
                    {
                        DocTypeId = document_category.DocTypeId,
                        DocCategoryId = document_category.DocCategoryId,
                        DocumentId = DocumentId,
                        DocumentBinary = DocumentBinary,
                        DocumentName = photo.DocumentName,
                        DocumentDesc = photo.DocumentDesc,
                        FileCreationDate = DateTime.Now,
                        FileExpireDate = null,
                        UserId = vj.UserID
                    });
                    if (doc != null)
                    {
                        var poldoc = idManager.VehicleManager.SetPolicyDocument(new Vehicle.Document
                        {
                            CorpId = vj.CorpId,
                            RegionId = vj.RegionId,
                            CountryId = vj.CountryId,
                            DomesticRegId = vj.DomesticRegId,
                            StateProvId = vj.StateProvId,
                            CityId = vj.CityId,
                            OfficeId = vj.OfficeId,
                            CaseSeqNo = vj.CaseSeqNo,
                            HistSeqNo = vj.HistSeqNo,
                            DocTypeId = document_category.DocTypeId,
                            DocCategoryId = document_category.DocCategoryId,
                            DocumentId = doc.DocumentId,
                            DocumentStatus = true,
                            UserId = vj.UserID
                        });
                        if (poldoc != null)
                        {
                            if (reviewPic == null)
                            {
                                var pic = idManager.VehicleManager.SetVehicleReviewPic(new Vehicle.Review.Pic
                                {
                                    CorpId = vj.CorpId,
                                    RegionId = vj.RegionId,
                                    CountryId = vj.CountryId,
                                    DomesticRegId = vj.DomesticRegId,
                                    StateProvId = vj.StateProvId,
                                    CityId = vj.CityId,
                                    OfficeId = vj.OfficeId,
                                    CaseSeqNo = vj.CaseSeqNo,
                                    HistSeqNo = vj.HistSeqNo,
                                    InsuredVehicleId = vj.InsuredVehicleId,
                                    ReviewId = ReviewId,
                                    PictureId = null,
                                    DocTypeId = document_category.DocTypeId,
                                    DocCategoryId = document_category.DocCategoryId,
                                    DocumentId = poldoc.DocumentId,
                                    PictureStatus = true,
                                    CreateDate = DateTime.Now,
                                    CreateUserId = vj.UserID
                                });
                            }
                        }
                    }
                    #endregion
                    #endregion

                    ret = "saved";
                }
            }
            catch (Exception ex)
            {
                msg = "savePhoto error";
                exm = ex.Message;
            }

            if (ret != "saved")
            {
                //Loguear Error
                idManager.PolicyManager.InsertLog(new Policy.LogParameter
                {
                    LogTypeId = Utility.LogTypeId.Exception.ToInt(),
                    CorpId = 1,
                    CompanyId = CompanyId,
                    ProjectId = ProjectId,
                    Identifier = Guid.NewGuid(),
                    LogValue = string.Format("{0} Exception: {1}", msg, exm)
                });
            }

            return ret;
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static bool InspectionCompleted(string ValoresJson)
        {

            /*Verificando si la inspeccion esta para asi activar el boton de enviar a suscripcion o completar*/
            var vj = Newtonsoft.Json.JsonConvert.DeserializeObject<ValoresJSON>(ValoresJson);
            if (vj == null)
                throw new Exception(string.Format("{0} - InspectionCompleted", Resources.DeserializationError));

            Services sv = new Services();

            bool completed = sv.isInspectedCompleted(vj.CaseSeqNo,
                                                     vj.CityId,
                                                     vj.CorpId,
                                                     vj.CountryId,
                                                     vj.DomesticRegId,
                                                     vj.HistSeqNo,
                                                     vj.OfficeId,
                                                     vj.RegionId,
                                                     vj.StateProvId);

            return completed;
        }
        #endregion
    }

    #region class
    public class InformacionGeneral
    {
        public string NumeroCotizacion { get; set; }
        public string Pais { get; set; }
        public string Provincia { get; set; }
        public string Ciudad { get; set; }
        public string Fecha { get; set; }
        public string HoraInicio { get; set; }
        public string Inspector { get; set; }
        public string Asegurado { get; set; }
        public string Intermediario { get; set; }
        public int Marca { get; set; }
        public int Modelo { get; set; }
        public int Ano { get; set; }
        public int VersionId { get; set; }
        public int TransmisionId { get; set; }
        public int ClaseId { get; set; }
        public int TraccionId { get; set; }
        public string Color { get; set; }
        public int Cilindros { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public string Uso { get; set; }
        public int MileageKilometer { get; set; }
        public int Kilometraje { get; set; }
        public int Capacidad { get; set; }
        public bool MatriculaDocumentoLegalBL { get; set; }
        public string Chasis { get; set; }
        public bool Inspeccionado { get; set; }
        public bool AutoSaveMode { get; set; }
        public bool PartialSave { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public int? InspectorID { get; set; }
    }

    public class VerificacionInformacionGeneral
    {
        public int? ReviewDetailId { get; set; }
        public int ReviewGroupId { get; set; }
        public int ReviewClassId { get; set; }
        public int? ReviewItemId { get; set; }
        public int ReviewOptionId { get; set; }
        public bool Checked { get; set; }
        public bool Erase { get; set; }
    }

    public class TipoCombustible
    {
        public int? ReviewDetailId { get; set; }
        public int ReviewGroupId { get; set; }
        public int ReviewClassId { get; set; }
        public int ReviewItemId { get; set; }
        public int ReviewOptionId { get; set; }
        public bool Checked { get; set; }
        public bool Erase { get; set; }
    }

    public class Funcionamiento
    {
        public int? ReviewDetailId { get; set; }
        public int ReviewGroupId { get; set; }
        public int ReviewClassId { get; set; }
        public int ReviewItemId { get; set; }
        public int ReviewOptionId { get; set; }
        public string ReviewOptionDesc { get; set; }
        public bool Checked { get; set; }
        public bool Erase { get; set; }
    }

    public class ParteFisicaExterior
    {
        public int? ReviewDetailId { get; set; }
        public int ReviewGroupId { get; set; }
        public int ReviewClassId { get; set; }
        public int ReviewItemId { get; set; }
        public int ReviewOptionId { get; set; }
        public string ReviewOptionDesc { get; set; }
        public bool Checked { get; set; }
        public bool Erase { get; set; }
    }

    public class ParteFisicaInterior
    {
        public int? ReviewDetailId { get; set; }
        public int ReviewGroupId { get; set; }
        public int ReviewClassId { get; set; }
        public int ReviewItemId { get; set; }
        public int ReviewOptionId { get; set; }
        public string ReviewOptionDesc { get; set; }
        public bool Checked { get; set; }
        public bool Erase { get; set; }
    }

    public class ParteFisicaOtro
    {
        public int? ReviewDetailId { get; set; }
        public int ReviewGroupId { get; set; }
        public int ReviewClassId { get; set; }
        public int ReviewItemId { get; set; }
        public int ReviewOptionId { get; set; }
        public string ReviewOptionDesc { get; set; }
        public bool Checked { get; set; }
        public bool Erase { get; set; }
    }

    public class ParteFisica
    {
        public List<ParteFisicaExterior> Exterior { get; set; }
        public List<ParteFisicaInterior> Interior { get; set; }
        public List<ParteFisicaOtro> Otros { get; set; }
    }

    public class Accesorio
    {
        public int? ReviewDetailId { get; set; }
        public int ReviewGroupId { get; set; }
        public int ReviewClassId { get; set; }
        public int ReviewItemId { get; set; }
        public int ReviewOptionId { get; set; }
        public string ReviewOptionDesc { get; set; }
        public bool Checked { get; set; }
        public bool Erase { get; set; }
    }

    public class Tapiceria
    {
        public int? ReviewDetailId { get; set; }
        public int ReviewGroupId { get; set; }
        public int ReviewClassId { get; set; }
        public int ReviewItemId { get; set; }
        public int ReviewOptionId { get; set; }
        public string ReviewOptionDesc { get; set; }
        public bool Checked { get; set; }
        public bool Erase { get; set; }
    }

    public class AccesorioTapiceria
    {
        public List<Accesorio> Accesorios { get; set; }
        public List<Tapiceria> Tapiceria { get; set; }
    }

    public class Seguridad
    {
        public int? ReviewDetailId { get; set; }
        public int ReviewGroupId { get; set; }
        public int ReviewClassId { get; set; }
        public int ReviewItemId { get; set; }
        public int ReviewOptionId { get; set; }
        public bool Checked { get; set; }
        public bool Erase { get; set; }
    }

    public class Complemento
    {
        public int? ReviewDetailId { get; set; }
        public int ReviewGroupId { get; set; }
        public int ReviewClassId { get; set; }
        public int ReviewItemId { get; set; }
        public int ReviewOptionId { get; set; }
        public bool Checked { get; set; }
        public bool Erase { get; set; }
    }

    public class SeguridadComplemento
    {
        public List<Seguridad> Seguridad { get; set; }
        public List<Complemento> Complementos { get; set; }
    }

    public class OtraInformacion
    {
        public string DictamenDanos { get; set; }
        public string Sucursal { get; set; }
        public string HoraCulminacion { get; set; }
        public bool? InspectorSuggestsAcceptRisk { get; set; }
        public string Mensaje { get; set; }
        public string UsuarioInspeccion { get; set; }
        public string FirmaCiente { get; set; }
        public bool HasSign { get; set; }
        public int CorpId { get; set; }
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public int DomesticRegId { get; set; }
        public int StateProvId { get; set; }
        public int CityId { get; set; }
        public int OfficeId { get; set; }
        public int CaseSeqNo { get; set; }
        public int HistSeqNo { get; set; }
    }

    public class VehiclePics
    {
        public string imgId { get; set; }
        public string imgRelativePath { get; set; }
        public string lnkId { get; set; }
        public int? DocumentId { get; set; }
        public string Base64String { get; set; }
        public string Mensaje { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDesc { get; set; }
        public int? UserID { get; set; }
        public string Quotation { get; set; }
    }

    public class ReviewDetails
    {
        public List<VerificacionInformacionGeneral> VerificacionInformacionesGenerales { get; set; }
        public TipoCombustible TipoCombustible { get; set; }
        public List<Funcionamiento> Funcionamiento { get; set; }
        public ParteFisica PartesFisicas { get; set; }
        public AccesorioTapiceria AccesoriosTapiceria { get; set; }
        public SeguridadComplemento SeguridadComplementos { get; set; }
        public int Count { get; set; }
        public int VIGCount { get; set; }
        public string Message { get; set; }
    }

    public class ValoresJSON
    {
        public int CorpId { get; set; }
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public int DomesticRegId { get; set; }
        public int StateProvId { get; set; }
        public int CityId { get; set; }
        public int OfficeId { get; set; }
        public int CaseSeqNo { get; set; }
        public int HistSeqNo { get; set; }
        public int? PolicyStatusId { get; set; }
        public int? VehicleReviewId { get; set; }
        public int InsuredVehicleId { get; set; }
        public int? ColorId { get; set; }
        public int? VehicleTypeId { get; set; }
        public int? UsageId { get; set; }
        public int? InspectedBy { get; set; }
        public bool Inspection { get; set; }
        public long VehicleUniqueId { get; set; }
        public int MarcaId { get; set; }
        public int ModeloId { get; set; }
        public string Sucursal { get; set; }
        public int LanguageId { get; set; }
        public int? UserID { get; set; }
        public string Quotation { get; set; }
        public int ReviewId { get; set; }
        public int ModelYear { get; set; }
        public int Seats { get; set; }
        public int Cylinder { get; set; }
        public string RegistryPlate { get; set; }
        public int VersionId { get; set; }
        public int TransmissionTypeId { get; set; }
        public int WheelDriveTypeId { get; set; }
        public int VehicleClassId { get; set; }
        public int? Odometer { get; set; }
        public bool? RegistrationDocument { get; set; }
        public System.DateTime? ReviewDate { get; set; }
        public string Mark { get; set; }
        public string InspectionAddress { get; set; }
    }

    public class Photo
    {
        public string Base64String { get; set; }
        public string DocumentDesc { get; set; }
        public string DocumentName { get; set; }
    }

    #endregion
}