﻿using Entity.Entities;
using Newtonsoft.Json;
using STL.POS.Frontend.Web.NewVersion.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace STL.POS.Frontend.Web.NewVersion.Controllers
{
    public partial class HomeController : BaseController
    {
        #region Info Additional Vehicle

        public class ItemPaymentFreq
        {
            public int id { get; set; }
            public decimal Discount { get; set; }
            public decimal initial { get; set; }
        }

        private Entity.Entities.ProductLimits getVehicleProductLimits(int VehicleId)
        {
            var vehicleProductLimits = oQuotationManager.GetQuotationProductLimits(VehicleId).Select(a => new QuotationViewModel.VehicleProductLimits
            {
                Id = a.Id,
                IsSelected = a.IsSelected,
                SdPrime = a.SdPrime,
                TpPrime = a.TpPrime,
                ServicesPrime = a.ServicesPrime,
                ISC = a.ISC,
                ISCPercentage = a.ISCPercentage,
                TotalPrime = a.TotalPrime,
                TotalIsc = a.TotalIsc,
                TotalDiscount = a.TotalDiscount,
                SelectedDeductibleCoreId = a.SelectedDeductibleCoreId,
                SelectedDeductibleName = a.SelectedDeductibleName,
                VehicleProduct_Id = a.VehicleProduct_Id,
                UserId = a.UserId
            }).FirstOrDefault();

            return
               vehicleProductLimits;
        }

        private new Tuple<QuotationViewModel.Vehicles, QuotationViewModel> getData()
        {
            ViewBag.QuoId = QuotationId;
            var DataQuotation = getQuotationData(QuotationId);
            ViewBag.IsFinanced = DataQuotation.Financed;
            ViewBag.QuotationNumber = DataQuotation.QuotationNumber;
            var FirstVehicle = getVehicleData(QuotationId).FirstOrDefault();
            var DataDrivers = getDriverData(QuotationId);

            Colors = oDropDownManager.GetDropDown("COLORS").Select(c => new SelectListItem
            {
                Text = c.name,
                Value = c.name
            });

            ViewBag.Colors = Colors;

            Drivers = DataDrivers.Select(d => new SelectListItem
            {
                Text = d.GetFullName(),
                Value = d.Id.ToString()
            });

            ViewBag.Drivers = Drivers;

            var dropDownTypePaymentFreq = DataQuotation.Financed.GetValueOrDefault() ? "PaymentFreqFinancedJSON" : "PaymentFreq";
            var dataDrop = oDropDownManager.GetDropDown(dropDownTypePaymentFreq).Select(p => new SelectListItem
            {
                Text = p.name,
                Value = p.Value.Replace("\"", "'")
            });

            if (RequestType == CommonEnums.RequestType.Inclusion && !base.isNotLawProduct)
                dataDrop = GetPaymentFreq(dataDrop, DataQuotation.StartDate, DataQuotation.EndDate);

            dataPaymentFreq = dataDrop;
            ViewBag.PaymentFreq = dataPaymentFreq;

            ViewBag.applyToDocumentRequired = DataQuotation.ApplyToDocumentRequired;


            var DataView = new Tuple<QuotationViewModel.Vehicles, QuotationViewModel>(FirstVehicle, DataQuotation);
            base.RiskLevel = DataQuotation.RiskLevel;

            return
                 DataView;
        }

        private bool isCompletedAllVehicles(IEnumerable<QuotationViewModel.Vehicles> vehicles)
        {
            bool pass = false;
            foreach (var item in vehicles)
            {
                if (string.IsNullOrEmpty(item.Chassis) || string.IsNullOrEmpty(item.Plate) || string.IsNullOrEmpty(item.Color) || item.Driver_Id == 0)
                {
                    return pass;
                }
            }

            pass = true;

            return pass;
        }

        public ActionResult InfoAddVehicle()
        {
            ViewBag.TitlePage = "INFORMACIÓN ADICIONAL VEHÍCULO";

            var AllVehicleData = getVehicleData(QuotationId);

            //Seteando las informaciones de depreciacion a todos los vehiculos
            var getquo = oQuotationManager.GetQuotation(QuotationId);

            foreach (var v in AllVehicleData)
            {
                //Guardar el porciento de depreciacion minima
                base.SetPorcDeprecMin(v, getquo);
                //Guardar el porciento de depreciacion total
                base.SetPorcTotalDeprec(v, getquo);
            }

            var DataView = getData();

            CurrentDataQuotation = DataView;

            ViewBag.isNotLawProduct = isNotLawProduct;
            QuotationNumber = DataView != null ? DataView.Item2.QuotationNumber : "";

            AllVehicleData = getVehicleData(QuotationId);

            int totalVehicles = AllVehicleData.Count();
            ViewBag.totalVehicles = totalVehicles;

            if (!isCompletedAllVehicles(AllVehicleData))
            {
                //Al menos a 1 vehiculo no se le ha completado todas sus informaciones
                TotalVehiclesCompleted = 0;
            }
            else
            {
                TotalVehiclesCompleted = totalVehicles;

                ViewBag.totalVehiclesCompleted = TotalVehiclesCompleted;
            }

            ViewBag.onlyLoggedUsers = allowOnlyLoggedUsers();
            ViewBag.RequestType = base.RequestType;

            ViewBag.APPFULLMODE = false;

            if (base._actionData == null)
            {
                if (SetModeFull())
                {
                    ViewBag.APPFULLMODE = SetModeFull();
                }
            }
            else if (base._actionData.Trim() == CommonEnums.AppModes.FULLMODE.ToString())
            {
                ViewBag.APPFULLMODE = true;
            }

            return PartialView(DataView);
        }

        /// <summary>
        /// Metodo de guardado para el vehiculo
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="QuotId"></param>
        /// <param name="VehicleNumber"></param>
        /// <returns></returns>
        public ActionResult SaveInfoVehicle(FormCollection frm)
        {
            try
            {
                var VehicleNumber = frm["hdnVehicleNumber"].ToInt();
                var StartDate = frm["StartDate"].ToString();
                var EndDate = frm["EndDate"].ToString();

                int? PaymentFreqIdSelected = null;

                var FreqSelected = frm["ddlPaymentFreq"];
                if (!string.IsNullOrEmpty(FreqSelected))
                {
                    FreqSelected = FreqSelected.Replace("'", "\"");
                    var PaymenFreqData = Utility.deserializeJSON<ItemPaymentFreq>(FreqSelected);
                    PaymentFreqIdSelected = PaymenFreqData.id; //PaymenFreqData.id == 0 ? 1 : PaymenFreqData.id;//0 es pago unico
                }

                var chassis = frm["chassis"];
                var Plate = frm["plate"];
                var Color = frm["ddlColor"];
                var NumeroFormulario = frm["NumeroFormulario"];
                var driver = frm["ddlDriver"].ToInt();

                var policyNoDecla = frm["noPolicyDecla"];

                var dataVehicle = getVehicleData(QuotationId).FirstOrDefault(v => v.VehicleNumber == VehicleNumber);

                var param = new Entity.Entities.VehicleProduct.Parameter
                {
                    id = dataVehicle.Id,
                    vehicleDescription = dataVehicle.VehicleDescription,
                    year = dataVehicle.Year,
                    cylinders = dataVehicle.Cylinders,
                    passengers = dataVehicle.Passengers,
                    weight = dataVehicle.Weight,
                    chassis = chassis,
                    plate = Plate,
                    color = Color,
                    vehiclePrice = dataVehicle.VehiclePrice,
                    insuredAmount = dataVehicle.InsuredAmount,
                    percentageToInsure = dataVehicle.PercentageToInsure,
                    totalPrime = dataVehicle.TotalPrime,
                    totalIsc = dataVehicle.TotalIsc,
                    totalDiscount = dataVehicle.TotalDiscount,
                    selectedProductCoreId = dataVehicle.SelectedProductCoreId,
                    vehicleTypeCoreId = dataVehicle.VehicleTypeCoreId,
                    selectedProductName = dataVehicle.SelectedProductName,
                    vehicleTypeName = dataVehicle.VehicleTypeName,
                    vehicleMakeName = dataVehicle.VehicleMakeName,
                    usageId = dataVehicle.UsageId,
                    usageName = dataVehicle.UsageName,
                    storeId = dataVehicle.StoreId,
                    storeName = dataVehicle.StoreName,
                    driver_Id = driver,
                    vehicleModel_Make_Id = dataVehicle.VehicleModel_Make_Id,
                    vehicleModel_Model_Id = dataVehicle.VehicleModel_Model_Id,
                    quotation_Id = dataVehicle.Quotation_Id,
                    selectedVehicleTypeId = dataVehicle.SelectedVehicleTypeId,
                    selectedVehicleTypeName = dataVehicle.SelectedVehicleTypeName,
                    selectedCoverageCoreId = dataVehicle.SelectedCoverageCoreId,
                    selectedCoverageName = dataVehicle.SelectedCoverageName,
                    vehicleYearOld = dataVehicle.VehicleYearOld,
                    surChargePercentage = dataVehicle.SurChargePercentage,
                    numeroFormulario = NumeroFormulario,
                    rateJson = dataVehicle.RateJson,
                    userId = GetCurrentUserID(),
                    modi_Date = DateTime.Now,
                    secuenciaVehicleSysflex = dataVehicle.SecuenciaVehicleSysflex,
                    isFacultative = dataVehicle.IsFacultative,
                    amountFacultative = dataVehicle.AmountFacultative,
                    vehicleQuantity = dataVehicle.VehicleQuantity
                };

                //Guardar los datos del vehiculo
                oVehicleProductManager.SetVehicleProduct(param);

                string pagoDesc = "N/A";

                switch (PaymentFreqIdSelected.HasValue ? PaymentFreqIdSelected.Value : -1)
                {
                    case 0:
                        pagoDesc = "Pago Único (5% de Descuento)";
                        break;
                    case 1:
                        pagoDesc = "Inicial + 1 Cuota";
                        break;
                    case 2:
                        pagoDesc = "Inicial + 2 Cuotas";
                        break;
                    case 3:
                        pagoDesc = "Inicial + 3 Cuotas";
                        break;
                    case 4:
                        pagoDesc = "Inicial + 4 Cuotas";
                        break;
                    default:
                        break;
                }

                var quotation = oQuotationManager.GetQuotation(QuotationId);

                bool useDateSpecified = false;

                if (RequestType == CommonEnums.RequestType.Emision || RequestType == CommonEnums.RequestType.InclusionDeclarativa)
                {
                    useDateSpecified = true;
                }

                //Actualizar datos de la cotizacion
                oQuotationManager.SetQuotation(new Entity.Entities.Quotation.parameter
                {
                    id = QuotationId,
                    startDate = DateTime.Parse(StartDate, System.Globalization.CultureInfo.InvariantCulture).Date,
                    endDate = useDateSpecified ? DateTime.Parse(EndDate, System.Globalization.CultureInfo.InvariantCulture).Date : quotation.EndDate.GetValueOrDefault().Date,
                    paymentFrequencyId = PaymentFreqIdSelected,
                    paymentFrequency = pagoDesc,
                    lastStepVisited = 4,
                    monthlyPayment = quotation != null ? quotation.MonthlyPayment : null,
                    financed = quotation != null ? quotation.Financed.GetValueOrDefault() : false,
                    period = quotation != null ? quotation.Period : null,
                    modi_UserId = (GetCurrentUserID() != null ? GetCurrentUserID() : quotation.Modi_UserId),
                    messaging = quotation != null ? quotation.Messaging : false,
                    RiskLevel = !string.IsNullOrEmpty(base.RiskLevel) ? base.RiskLevel : null,
                    achType = 0,
                    policy_No_Main = policyNoDecla
                });

                TotalVehiclesCompleted = TotalVehiclesCompleted + 1;
                CurrentDataQuotation = getData();

                return
                    RedirectToAction("GetNextVehicle", new { QuotId = QuotationId, VehicleNumber = VehicleNumber });
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(CommonEnums.Categories.Error, (GetCurrentUsuario() != null ? GetCurrentUsuario().UserLogin : "POS-VentaDirecta"), QuotationId, "Error completando la informacion del vehiculo", "Detalle: Nro de Cotización fallida: " + QuotationId.ToString() + " MENSAJE " + ex.Message);
                throw;
            }
        }

        public ActionResult GetNextVehicle(int QuotId, long? VehicleNumber)
        {
            var DataVehicles = getVehicleData(QuotationId);
            var DataQuotation = base.getQuotationData(QuotationId);
            var CountVehicles = DataVehicles.Count();
            VehicleNumber = VehicleNumber + 1;
            if (VehicleNumber > CountVehicles)
                VehicleNumber = 1;

            ViewBag.QuoId = QuotId;
            ViewBag.Drivers = Drivers;
            ViewBag.Colors = Colors;
            ViewBag.PaymentFreq = dataPaymentFreq;
            ViewBag.IsFinanced = DataQuotation.Financed;
            ViewBag.QuotationNumber = DataQuotation.QuotationNumber;
            var NextVehicle = DataVehicles.FirstOrDefault(v => v.VehicleNumber == VehicleNumber);
            var DataView = new Tuple<QuotationViewModel.Vehicles, QuotationViewModel>(NextVehicle, DataQuotation);

            ViewBag.totalVehicles = CountVehicles;

            ViewBag.totalVehiclesCompleted = TotalVehiclesCompleted;

            ViewBag.isNotLawProduct = isNotLawProduct;
            ViewBag.onlyLoggedUsers = allowOnlyLoggedUsers();

            ViewBag.APPFULLMODE = false;

            if (base._actionData == null)
            {
                if (SetModeFull())
                {
                    ViewBag.APPFULLMODE = SetModeFull();
                }
            }
            else if (base._actionData.Trim() == CommonEnums.AppModes.FULLMODE.ToString())
            {
                ViewBag.APPFULLMODE = true;
            }

            return PartialView("Vehicles", DataView);
        }

        public IEnumerable<Entity.Entities.ProductLimits> getVehicleProductLimitVehicle(int VehicleId)
        {
            var vehicleProductLimits = oQuotationManager.GetQuotationProductLimits(VehicleId).Select(a => new Entity.Entities.ProductLimits
            {
                Id = a.Id,
                IsSelected = a.IsSelected,
                SdPrime = a.SdPrime,
                TpPrime = a.TpPrime,
                ServicesPrime = a.ServicesPrime,
                ISC = a.ISC,
                ISCPercentage = a.ISCPercentage,
                TotalPrime = a.TotalPrime,
                TotalIsc = a.TotalIsc,
                TotalDiscount = a.TotalDiscount,
                SelectedDeductibleCoreId = a.SelectedDeductibleCoreId,
                SelectedDeductibleName = a.SelectedDeductibleName,
                VehicleProduct_Id = a.VehicleProduct_Id,
                UserId = a.UserId
            });

            return
                vehicleProductLimits;
        }

        public JsonResult SendQuotToGlobal(int quotationID)
        {
            var quotation = oQuotationManager.GetQuotation(quotationID);
            Entity.Entities.WSEntities.SendQuotationResultWS result = new Entity.Entities.WSEntities.SendQuotationResultWS();

            if (quotation == null)
            {
                return Json(new { success = false, message = "Cotización no encontrada" }, JsonRequestBehavior.AllowGet);
            }

            string message = "";

            var vhData = getVehicleData(quotation.Id.GetValueOrDefault()).ToList().FirstOrDefault(x =>
                            (string.IsNullOrEmpty(x.Chassis) || string.IsNullOrWhiteSpace(x.Chassis)) ||
                            (string.IsNullOrEmpty(x.Plate) || string.IsNullOrWhiteSpace(x.Plate)) ||
                            (string.IsNullOrEmpty(x.Color) || string.IsNullOrWhiteSpace(x.Color))
                            );
            if (vhData != null)
            {
                message = string.Format("El vehículo {0} falta completarle la información del chasis, placa o color. Si el error persiste refresque la página e intente otra vez.", vhData.VehicleDescription);
                return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
            }

            bool response = false;

            var s = oVirtualOfficeProxy.GetPolicy(quotation.QuotationNumber);
            if (s == true)
            {
                var usuario = GetCurrentUsuario();
                int realuserid = oDropDownManager.GetParameter("PARAMETER_KEY_USERID_DEFAULT_VO").Value.ToInt();
                if (usuario != null)
                {
                    realuserid = usuario.UserID;
                }

                //Aqui llamar sp que Eliminara la cotizacion duplicada
                bool wasDeleted = oVirtualOfficeProxy.DeleteDuplicatePolicy(quotation.QuotationNumber, realuserid);

                if (!wasDeleted)
                {
                    //Existe en vo
                    message = "Esta cotización ya ha sido enviada a nuestros sistemas.";
                    return Json(new { success = response, quotationId = quotation.Id, message = message }, JsonRequestBehavior.AllowGet);
                }
            }

            var path = oDropDownManager.GetParameter("PARAMETER_KEY_PATH_PDF_THUNDERHEAD").Value;
            var sender = oDropDownManager.GetParameter("PARAMETER_KEY_EMAIL_SENDER").Value;
            var coreResult = true;
            List<string> statusMessages = new List<string>();
            bool success = true;
            string realMessage = "";

            Entity.Entities.WSEntities.SendQuotationResultWS sqr = new Entity.Entities.WSEntities.SendQuotationResultWS();
            try
            {

                /*Validando que no existan la placa y el chasis introducido*/
                try
                {
                    var vehicle = getVehicleData(quotation.Id.GetValueOrDefault()).ToList();

                    foreach (var item in vehicle)
                    {
                        if (!quotation.SendInspectionOnly.GetValueOrDefault())
                        {
                            var ccp = this.CheckChassisPlate(item.Chassis, item.Plate);

                            if (ccp.Count() > 0)
                            {
                                statusMessages.Add(string.Format("El chasis {0} o placa {1} ya estan registrados en nuestro sistema.", item.Chassis, item.Plate));
                                success = false;
                            }
                        }
                    }

                    if (statusMessages.Count() == 0)
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    statusMessages.Add("Falla en el método CheckChassisPlate. Detalle: " + ex.Message);
                    success = false;
                }
                if (!success)
                {
                    foreach (var stmsg in statusMessages)
                    {
                        realMessage += "\n" + stmsg;
                    }

                    LoggerHelper.Log(CommonEnums.Categories.Error, (GetCurrentUsuario() != null ? GetCurrentUsuario().UserLogin : "POS-VentaDirecta"), quotation.Id.GetValueOrDefault(), "Error en envío de cotización a Sysflex/VirtualOffice", "Detalle: Nro de Cotización fallida: " + quotation.Id.ToString() + " MENSAJE " + realMessage);
                    return Json(new { success = false, message = realMessage }, JsonRequestBehavior.AllowGet);
                }
                /**/

                result.resultQuotation = new Entity.Entities.Quotation();

                result = SendQuotationToCore(quotationID);

                if (result.SentToCore && result.SentToVO)
                {
                    coreResult = true;
                }
                else if (result.SentToCore && !result.SentToVO)
                {
                    coreResult = true;
                }
                else if (!result.SentToCore && result.SentToVO)
                {
                    coreResult = true;
                }
                else
                {
                    coreResult = false;
                }

                if (coreResult)
                {
                    Entity.Entities.Quotation.parameter qparam = new Entity.Entities.Quotation.parameter();
                    qparam.id = quotation.Id;
                    qparam.status = 1;
                    qparam.monthlyPayment = quotation.MonthlyPayment;
                    qparam.financed = quotation.Financed.GetValueOrDefault();
                    qparam.period = quotation.Period;
                    qparam.modi_UserId = (GetCurrentUserID() != null ? GetCurrentUserID() : quotation.Modi_UserId);
                    if (!string.IsNullOrEmpty(base.RiskLevel))
                        qparam.RiskLevel = base.RiskLevel;

                    oQuotationManager.SetQuotation(qparam);
                    base.QuotationId = 0;

                    if (SetModeFull())
                    {
                        //aqui proceso de envio de correo de notificacion
                        sendNotificationModeFull(quotation.Id.GetValueOrDefault(), quotation.QuotationNumber);
                        //
                    }

                    //Enviando nota a global siempre que haya
                    var allNotes = oQuotationManager.GetQuotationNotes(quotation.Id.GetValueOrDefault()).ToList();

                    if (allNotes.Count() > 0)
                    {
                        foreach (var n in allNotes)
                        {
                            oQuotationManager.SendQuotationNotesToGlobal(quotation.Id.GetValueOrDefault(), n.Id);
                        }
                    }
                    //Enviando nota a global siempre que haya
                }
                else
                {
                    LoggerHelper.Log(CommonEnums.Categories.Error, (GetCurrentUsuario() != null ? GetCurrentUsuario().UserLogin : "POS-VentaDirecta"), quotation.Id.GetValueOrDefault(), "Error en envío de cotización a Sysflex/VirtualOffice", "Detalle: Nro de Cotización fallida: " + quotation.Id.ToString());

                    string realMessage2 = "No se ha podido cargar la cotización en nuestros sistemas.";

                    if (!string.IsNullOrEmpty(result.SentToCoreErrorChasis))
                    {
                        realMessage2 = result.SentToCoreErrorChasis;
                    }

                    return Json(new { success = false, message = realMessage2 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var quotationNumerError = quotation != null ? " No Cotizacion: " + quotation.QuotationNumber + " " : "N/A ";

                LoggerHelper.Log(CommonEnums.Categories.Error, (GetCurrentUsuario() != null ? GetCurrentUsuario().UserLogin : "POS-VentaDirecta"), quotation.Id.GetValueOrDefault(), "Error en envío de cotizacion a Sysflex", "Detalle: " + quotationNumerError + ex.Message + ex.StackTrace, ex);
                coreResult = false;

                SendErrorQuotation((quotation != null ? quotation.QuotationNumber : "N/A"), "Error", ex);
            }

            return Json(new { success = true, message = "Se ha cargado satisfactoriamente su nueva cotización." }, JsonRequestBehavior.AllowGet);
        }

        private STL.POS.WsProxy.SysflexService.PolicyVehicleVehicleIdentification[] CheckChassisPlate(string chassis, string plate)
        {
            var result = oCoreProxy.CheckChassisPlate(chassis, plate);
            return result;
        }

        public int SendQuotationInspectionPending(int QuotationId)
        {
            byte[] Xml = GenerateXMLQuotation(QuotationId);
            return oThunderheadProxy.SendToTH_NewPV(Xml);
        }

        public ActionResult FinalStepToInbox(int quotationID)
        {
            var vehicles = getVehicleData(quotationID);
            List<string> model = new List<string>();
            ViewBag.IsFinanced = isFinanced;
            ViewBag.QuotationId = quotationID;

            ViewBag.QuotationNumber = QuotationNumber;

            foreach (var v in vehicles)
            {
                string[] prod = new string[] { "DE LEY", "ULTRA" };

                var Year = string.Empty;
                if (!prod.Contains(v.SelectedProductName) && (RequestType == CommonEnums.RequestType.Emision || RequestType == CommonEnums.RequestType.Inclusion
                    || RequestType == CommonEnums.RequestType.InclusionDeclarativa || RequestType == CommonEnums.RequestType.PropuestaRecuperacion))
                {
                    switch (RequestType)
                    {
                        case CommonEnums.RequestType.Emision:
                        case CommonEnums.RequestType.InclusionDeclarativa:
                        case CommonEnums.RequestType.PropuestaRecuperacion:
                            Year = v.Year.ToString();
                            break;
                        case CommonEnums.RequestType.Inclusion:
                            break;
                        case CommonEnums.RequestType.Renovacion:
                            break;
                    }

                    string vehiclName = v.VehicleDescription + " " + Year;
                    model.Add(vehiclName);
                }
                else if (RequestType == CommonEnums.RequestType.Exclusion || RequestType == CommonEnums.RequestType.ExclusionDeclarativa)
                {
                    string desc = string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                                              v.VehicleMakeName,
                                              v.ModelDesc,
                                              v.Year,
                                              v.Chassis,
                                              v.Plate,
                                              v.Color
                                              );
                    model.Add(desc);
                }
                else if (RequestType == CommonEnums.RequestType.Cambios)
                {
                    var DataQuotation = getDataQuotation(quotationID);
                    List<RequestChanges> dataVehicleRequestChange = Utility.getRequestChanges(DataQuotation.policyNoMain, null, v.SecuenciaVehicleSysflex.GetValueOrDefault(), true);

                    string oldPlate = dataVehicleRequestChange.FirstOrDefault(x => x.Condition_Id == 7) != null ? dataVehicleRequestChange.FirstOrDefault(x => x.Condition_Id == 7).Old_Value : "";//7 = Placa
                    string oldChassis = dataVehicleRequestChange.FirstOrDefault(x => x.Condition_Id == 6) != null ? dataVehicleRequestChange.FirstOrDefault(x => x.Condition_Id == 6).Old_Value : "";//6 = Chasis
                    string oldColor = dataVehicleRequestChange.FirstOrDefault(x => x.Condition_Id == 5) != null ? dataVehicleRequestChange.FirstOrDefault(x => x.Condition_Id == 5).Old_Value : "";//5 = Color

                    string desc = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                                              v.VehicleMakeName,
                                              v.ModelDesc,
                                              v.Year,
                                              oldChassis,
                                              oldPlate,
                                              oldColor,
                                              v.Chassis,
                                              v.Plate,
                                              v.Color
                                              );
                    model.Add(desc);
                }
            }

            ViewBag.RequestType = base.RequestType;
            return PartialView("FinalStepToInbox", model);
        }

        private void sendNotificationModeFull(int quotationid, string quotationNumber)
        {
            try
            {
                var cData = getDriverData(quotationid).FirstOrDefault(x => x.IsPrincipal);

                if (cData != null)
                {
                    string template = oDropDownManager.GetParameter("PARAMETER_KEY_TEMPLATE_EMAIL_MODE_FULL").Value;
                    string templateEmailReceiver = oDropDownManager.GetParameter("PARAMETER_KEY_TEMPLATE_EMAIL_RECEIVER").Value;

                    var body = string.Format(template, quotationNumber, cData.GetFullName(), cData.GetDriverPhone());

                    SendModeFullNotification(body, templateEmailReceiver);
                }
            }
            catch (Exception ex)
            {
                var quotationNumerError = " No Cotizacion: " + quotationNumber;

                LoggerHelper.Log(CommonEnums.Categories.Error, (GetCurrentUsuario() != null ? GetCurrentUsuario().UserLogin : "POS-VentaDirecta"), quotationid, "Error en envío correo de notificacion", "Detalle: " + quotationNumerError + ex.Message + ex.StackTrace, ex);

                SendErrorQuotation(quotationNumber, "Error", ex);
            }
        }

        #endregion

        #region Payments

        public ActionResult _PaymentCheckout()
        {
            //IEnumerable<SelectListItem> paymentFreqList, wayToPayList;
            IEnumerable<SelectListItem> wayToPayList;
            IEnumerable<Generic> paymentFreqList;

            Dictionary<int, string> wayToPayListTemp;
            string dropDownType, wayToPay;
            Quotation.PaymentCheckOut data;
            bool _allowOnlyLoggedUsers = allowOnlyLoggedUsers();
            QuotationViewModel dataQuotation;
            DateTime? BeginDate = null;
            DateTime? EndDate = null;

            dropDownType = "PaymentFreq2";
            //paymentFreqList = oDropDownManager.GetDropDown(dropDownType).Select(p => new SelectListItem
            //{
            //    Text = p.name,
            //    Value = p.Value.Replace("\"", "'")
            //});

            paymentFreqList = oDropDownManager.GetDropDown(dropDownType).Select(p => new Generic
            {
                name = p.name,
                Value = p.Value.Replace("\"", "'")
            });

            ViewBag.RequestType = base.RequestType;
            dataQuotation = getQuotationData(base.QuotationId);
            if (RequestType == CommonEnums.RequestType.Inclusion && !base.isNotLawProduct)
            {
                BeginDate = dataQuotation.StartDate;
                EndDate = dataQuotation.EndDate;
                //paymentFreqList = GetPaymentFreq(paymentFreqList, BeginDate, EndDate); REVISAR
            }

            wayToPay = oDropDownManager.GetParameter("PARAMETER_KEY_WAY_TO_PAY").Value;
            wayToPayListTemp = JsonConvert.DeserializeObject<Dictionary<int, string>>(wayToPay);
            wayToPayList = wayToPayListTemp.Select(p => new SelectListItem
            {
                Value = p.Key.ToString(),
                Text = p.Value
            });

            ViewBag.PaymentFreq = paymentFreqList;

            if (_allowOnlyLoggedUsers == false)
            {
                ViewBag.WayToPay = wayToPayList.Where(x => x.Value == "2");//Tarjeta de credito
            }
            else
            {
                ViewBag.WayToPay = wayToPayList.Where(x => x.Value != "4");//ACH;
            }


            data = oQuotationManager.GetQuotationPaymentInfoViewModel(QuotationId);


            data.TotalAmountWithDiscount = (data.TotalPrime.ToDecimal() - data.sumAllDiscount.ToDecimal()).ToString(System.Globalization.CultureInfo.InvariantCulture);
            data.TotalISC = (data.TotalAmountWithDiscount.ToDecimal() * (dataQuotation.ISCPercentage / 100)).ToString();
            data.TotalAmountWithDiscountAndTax = (data.TotalAmountWithDiscount.ToDecimal() + data.TotalISC.ToDecimal()).ToString(System.Globalization.CultureInfo.InvariantCulture);


            if (RequestType == CommonEnums.RequestType.Inclusion && !base.isNotLawProduct)
            {
                var DiasCotizados = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Day, BeginDate.GetValueOrDefault(), EndDate.GetValueOrDefault());
                var vehicleData = getVehicleData(base.QuotationId);
                var AnnualPremium = vehicleData.Sum(k => (k.ProratedPremium * k.VehicleQuantity) * DiasCotizados);
                data.TotalPrime = (AnnualPremium.Value - data.sumAllDiscount.ToDecimal()).ToString();
                data.TotalAnnualPrime = (dataQuotation.TotalPrime).ToString();

                data.TotalFlotillaDiscount = (AnnualPremium * (data.FlotillaDiscountPercent.ToDecimal() / 100)).ToString();

                data.TotalISC = (data.TotalPrime.ToDecimal() * (dataQuotation.ISCPercentage / 100)).ToString();

                data.TotalAmountWithDiscountAndTax = (data.TotalPrime.ToDecimal() + data.TotalISC.ToDecimal()).ToString(System.Globalization.CultureInfo.InvariantCulture);
            }


            ViewBag.isDomiciliation = dataQuotation.Domiciliation;
            return
                PartialView(data);
        }

        [HttpPost]
        public JsonResult _PaymentCheckoutSave(Quotation.Payment formValue)
        {
            Quotation.parameter parameter;
            bool result;
            bool canMovement = true;

            IsAQuotation = false;

            try
            {
                int cod_COMPANY = oDropDownManager.GetParameter("PARAMETER_KEY_COMPANY_ID_SYSFLEX").Value.ToInt();

                var TotalDiscount = formValue.TotalDiscount == null ? 0 : formValue.TotalDiscount;
                QuotationId = formValue.QuotationID;

                string pagoDesc = "N/A";

                switch (formValue.PaymentFrequencyId.HasValue ? formValue.PaymentFrequencyId.Value : -1)
                {
                    case 0:
                        pagoDesc = "Pago Único (5% de Descuento)";
                        break;
                    case 1:
                        pagoDesc = "Inicial + 1 Cuota";
                        break;
                    case 2:
                        pagoDesc = "Inicial + 2 Cuotas";
                        break;
                    case 3:
                        pagoDesc = "Inicial + 3 Cuotas";
                        break;
                    case 4:
                        pagoDesc = "Inicial + 4 Cuotas";
                        break;
                    default:
                        break;
                }

                var quotation = oQuotationManager.GetQuotation(QuotationId);
                if (RequestType == CommonEnums.RequestType.Inclusion)
                    canMovement = oCoreProxy.GetPolicyMov(quotation.policyNoMain).GetValueOrDefault();

                parameter = new Quotation.parameter
                {
                    id = QuotationId,
                    paymentFrequencyId = formValue.PaymentFrequencyId,
                    paymentFrequency = pagoDesc,
                    paymentWay = formValue.PaymentWay,
                    amountToPayEnteredByUser = formValue.AmountToPayEnteredByUser,
                    totalDiscount = TotalDiscount,
                    discountPercentage = TotalDiscount > 0 ? formValue.DiscountPercentage : 0,
                    messaging = formValue.Messaging,
                    lastStepVisited = 6,
                    monthlyPayment = quotation != null ? quotation.MonthlyPayment : null,
                    financed = quotation != null ? quotation.Financed.GetValueOrDefault() : false,
                    period = quotation != null ? quotation.Period : null,
                    modi_UserId = (GetCurrentUserID() != null ? GetCurrentUserID() : quotation.Modi_UserId),
                    RiskLevel = !string.IsNullOrEmpty(base.RiskLevel) ? base.RiskLevel : null
                };


                oQuotationManager.SetQuotation(parameter);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return
                Json(new { Result = result, canMovement = canMovement }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult _SendToCardnet()
        {
            Quotation.Payment data;
            Cardnet.RequestModel cardnetModel;
            string baseUrl;

            data = oQuotationManager.GetQuotationPaymentInfo(QuotationId);

            if (data != null)
            {
                cardnetModel = new Cardnet.RequestModel();

                baseUrl = string.Concat(Request.Url.Scheme, "://", Request.Url.Authority, Request.ApplicationPath.TrimEnd('/'));
                cardnetModel.TransactionType = oDropDownManager.GetParameter("PARAMETER_KEY_CARDNET_TRANSACTION_TYPE").Value;
                cardnetModel.AcquiringInstitutionCode = oDropDownManager.GetParameter("PARAMETER_KEY_CARDNET_ACQUIRING_INSTITUTION_CODE").Value;
                cardnetModel.Amount = (data.AmountToPayEnteredByUser.GetValueOrDefault(0m) * 100).ToString("000000000000");
                cardnetModel.CancelUrl = baseUrl + Url.Action("CarnetResponse", "Home", new { id = QuotationId });
                cardnetModel.CardnetUrl = oDropDownManager.GetParameter("PARAMETER_KEY_CARDNET_PAYMENT_URL").Value;
                cardnetModel.CurrencyCode = oDropDownManager.GetParameter("PARAMETER_KEY_CARDNET_CURRENCY_CODE").Value;
                cardnetModel.Ipclient = Request.UserHostAddress;
                cardnetModel.MerchantName = oDropDownManager.GetParameter("PARAMETER_KEY_CARDNET_MERCHANT_NAME").Value.PadRight(40, ' ');
                cardnetModel.MerchantNumber = oDropDownManager.GetParameter("PARAMETER_KEY_CARDNET_MERCHANT_NUMBER").Value.PadRight(15, ' ');
                cardnetModel.MerchantNumber_amex = oDropDownManager.GetParameter("PARAMETER_KEY_CARDNET_MERCHANT_NUMBER_AMEX").Value;
                cardnetModel.MerchantTerminal = oDropDownManager.GetParameter("PARAMETER_KEY_CARDNET_MERCHANT_TERMINAL").Value;
                cardnetModel.MerchantTerminal_amex = oDropDownManager.GetParameter("PARAMETER_KEY_CARDNET_MERCHANT_TERMINAL_AMEX").Value;
                cardnetModel.MerchantType = oDropDownManager.GetParameter("PARAMETER_KEY_CARDNET_MERCHANT_TYPE").Value;
                cardnetModel.OrdenId = QuotationId.ToString();
                cardnetModel.PageLanguaje = oDropDownManager.GetParameter("PARAMETER_KEY_CARDNET_PAGE_LANGUAGE").Value;
                cardnetModel.ReturnUrl = baseUrl + Url.Action("CarnetResponse", "Home", new { id = QuotationId });
                cardnetModel.Tax = (data.TotalISC.GetValueOrDefault(0) * 100).ToString("000000000000");
                cardnetModel.TransactionId = data.QuotationDailyNumber.ToString("d0").PadLeft(6, '0');
                cardnetModel.CardnetUrl = oDropDownManager.GetParameter("PARAMETER_KEY_CARDNET_PAYMENT_URL").Value;

                return
                    PartialView(cardnetModel);
            }
            else
                throw new Exception("Cotización no encontrada");
        }

        public ActionResult CarnetResponse(Cardnet.ResponseModel response)
        {
            Entity.Entities.WSEntities.SendQuotationResultWS sysData = new Entity.Entities.WSEntities.SendQuotationResultWS();
            Quotation quoData;
            int quoId = 0;
            string achErrorMsg = "", paramErr = ""; ;
            bool resultWS = false;
            decimal amountPayByClient = 0;

            ViewBag.failInsentingQuotationOnSysFlexOrVO = "";
            bool inserted = false;
            var usuario = GetCurrentUsuario();

            bool _allowOnlyLoggedUsers = allowOnlyLoggedUsers();

            int.TryParse(response.OrdenId, out quoId);

            if (response.ResponseCode == "00" && quoId > 0)
            {
                sysData = this.SendQuotationToCore(quoId);
                quoData = oQuotationManager.GetQuotation(quoId);
                amountPayByClient = quoData.AmountToPayEnteredByUser.GetValueOrDefault();
                try
                {
                    if (!sysData.CantMovement)
                    {
                        inserted = false;
                        ViewBag.errorGPToSysflexMessage = string.Format("A la Poliza \"{0}\" no se le pueden hacer movimientos ya que la misma esta en Transito", quoData.policyNoMain);
                        ViewBag.failInsentingQuotationOnSysFlexOrVO = "Z";
                    }

                    if (sysData.SentToCore && sysData.SentToVO && sysData.SentToCoreWithErrorGP)
                    {
                        inserted = true;
                        ViewBag.failInsentingQuotationOnSysFlexOrVO = "GP";

                        if (_allowOnlyLoggedUsers)
                        {
                            ViewBag.errorGPToSysflexMessage = oDropDownManager.GetParameter("PARAMETER_KEY_MESSAGE_GP_PASS_TO_SYSFLEX").Value;
                            ViewBag.errorGPToSysflexMessage += ". Hora del Mensaje: " + DateTime.Now.ToString("hh:mm:ss tt");
                        }
                        else
                        {
                            ViewBag.errorGPToSysflexMessage = oDropDownManager.GetParameter("PARAMETER_KEY_MESSAGE_GP_PASS_TO_SYSFLEX_CLIENT").Value;
                        }

                        sendEmailErrorInvoice(ViewBag.errorGPToSysflexMessage.ToString().Replace("{0}", sysData.resultQuotation.PolicyNumber));
                    }
                    else if (sysData.SentToCore && !sysData.SentToVO && sysData.SentToCoreWithErrorGP)
                    {
                        inserted = true;
                        ViewBag.failInsentingQuotationOnSysFlexOrVO = "GP2";

                        if (_allowOnlyLoggedUsers)
                        {
                            ViewBag.errorGPToSysflexMessage2 = oDropDownManager.GetParameter("PARAMETER_KEY_MESSAGE_GP_PASS_TO_SYSFLEX2").Value;
                            ViewBag.errorGPToSysflexMessage2 += ". Hora del Mensaje: " + DateTime.Now.ToString("hh:mm:ss tt");
                        }
                        else
                        {
                            ViewBag.errorGPToSysflexMessage2 = oDropDownManager.GetParameter("PARAMETER_KEY_MESSAGE_GP_PASS_TO_SYSFLEX_CLIENT").Value;
                        }

                        sendEmailErrorInvoice(ViewBag.errorGPToSysflexMessage2.ToString().Replace("{0}", sysData.resultQuotation.PolicyNumber));
                    }
                    else if (sysData.SentToCore && sysData.SentToVO && !sysData.SentToCoreWithErrorGP)//CUANDO TODO SALE BIEN
                    {
                        inserted = true;

                        ViewBag.failInsentingQuotationOnSysFlexOrVO = inserted == false ? "S" : "N";
                    }
                    else if (sysData.SentToCore && !sysData.SentToVO && !sysData.SentToCoreWithErrorGP)//CUANDO TODO SALE BIEN MENOS EN VO
                    {
                        inserted = true;

                        ViewBag.failInsentingQuotationOnSysFlexOrVO = "W";
                    }
                    else if (!sysData.SentToCore && !sysData.SentToVO)
                    {
                        inserted = false;
                        ViewBag.failInsentingQuotationOnSysFlexOrVO = "Z";

                        if (!string.IsNullOrEmpty(sysData.SentToCoreErrorChasis))
                        {
                            ViewBag.failInsentingQuotationOnSysFlexOrVO = "CH";
                            ViewBag.errorGPToSysflexMessage2 = sysData.SentToCoreErrorChasis;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LoggerHelper.Log(CommonEnums.Categories.Error, (usuario != null ? "POS-" + usuario.UserLogin : "POS-Venta Directa"), quoData.Id.GetValueOrDefault(), "Error en servicio SysFlex", "Se ha producido un error al llamar al servicio de Sysflex.", ex);
                    SendErrorQuotation((quoData != null ? quoData.QuotationNumber : "N/A"), "Error", ex);
                }

                if (inserted == true)
                {
                    try
                    {
                        resultWS = oAchPaymentProxy.CreateCreditCardPaymentForAuto(quoData,
                                              "1",//Peso Dominicano
                                              response.CreditCardNumber == null ? "" : response.CreditCardNumber,
                                              response.TransactionId,
                                              sysData.resultQuotation.PolicyNumber,
                                              out achErrorMsg,
                                              out paramErr);
                    }
                    catch (Exception ex)
                    {
                        var errMsg = "Error desconocido al comunicarse con GP.\nQuotationId: " + quoData.Id.GetValueOrDefault() + "\nDetalles:" + ex.Message + ex.StackTrace;
                        LoggerHelper.Log(CommonEnums.Categories.Error, (usuario != null ? "POS-" + usuario.UserLogin : "POS-Venta Directa"), quoData.Id.GetValueOrDefault(), "No se ha podido informar a GP sobre el pago realizado.", errMsg, ex);
                    }

                    if (!resultWS)
                    {
                        var errMsg = "Error desconocido al comunicarse con GP.\nQuotationId: " + quoData.Id.GetValueOrDefault() + "- Mensaje: " + achErrorMsg + "\nDetalles:Parametros:" + paramErr;
                        LoggerHelper.Log(CommonEnums.Categories.Error, (usuario != null ? "POS-" + usuario.UserLogin : "POS-Venta Directa"), quoData.Id.GetValueOrDefault(), "GP ha devuelto error ante informe de pago realizado.", errMsg);
                    }

                    /*Guardando respuesta del pago de cardnet*/
                    var parameter = new Quotation.parameter
                    {
                        id = quoData.Id.GetValueOrDefault(),
                        cardnetLastResponseCode = response.ResponseCode,
                        cardnetLastResponseMessage = response.ResponseMsg,
                        cardnetAuthorizationCode = response.AuthorizationCode,
                        cardnetPaymentStatus = 1,//Pago por Cardnet Completado
                        policyNumber = sysData.resultQuotation.PolicyNumber,
                        status = 1,
                        monthlyPayment = quoData.MonthlyPayment,
                        financed = quoData.Financed.GetValueOrDefault(),
                        period = quoData.Period,
                        quotationCoreIdNumber = sysData.resultQuotation.QuotationCoreIdNumber,
                        quotationCoreNumber = sysData.resultQuotation.QuotationCoreIdNumber.ToString(),
                        modi_UserId = (GetCurrentUserID() != null ? GetCurrentUserID() : quoData.Modi_UserId),
                        RiskLevel = !string.IsNullOrEmpty(base.RiskLevel) ? base.RiskLevel : null
                    };
                    oQuotationManager.SetQuotation(parameter);
                    /**/

                    GenerateOnbaseFile(quoData.Id.GetValueOrDefault());

                    if (SetModeLey())
                    {
                        var dataClient = getDriverData(quoData.Id.GetValueOrDefault()).FirstOrDefault();

                        string body = string.Format("Se ha emitido una nueva poliza de ley numero: {0} <br/> Cliente: {1} <br/> Telefono: {2} <br/> Email: {3}",
                            sysData.resultQuotation.PolicyNumber,
                            dataClient.GetFullName(),
                            dataClient.GetDriverPhone(),
                            dataClient.Email
                            );

                        SendModeLeyNotificationEmission(body);
                    }

                    //Enviando nota a global siempre que haya
                    var allNotes = oQuotationManager.GetQuotationNotes(quoData.Id.GetValueOrDefault()).ToList();

                    if (allNotes.Count() > 0)
                    {
                        foreach (var n in allNotes)
                        {
                            oQuotationManager.SendQuotationNotesToGlobal(quoData.Id.GetValueOrDefault(), n.Id);
                        }
                    }
                    //Enviando nota a global siempre que haya


                    if (base.RequestType == CommonEnums.RequestType.Emision && !string.IsNullOrEmpty(quoData.couponCode))
                    {
                        var quoPrincipalDr = oQuotationManager.GetQuotationDrivers(quoData.Id.GetValueOrDefault()).FirstOrDefault(x => x.IsPrincipal);
                        SetCouponProspect(quoPrincipalDr.FirstName, quoPrincipalDr.FirstSurname, quoPrincipalDr.Email, quoPrincipalDr.PhoneNumber, sysData.resultQuotation.PolicyNumber, quoData.CouponProspectId.GetValueOrDefault());
                    }
                }
            }
            else
            {
                var body = string.Format("El pago de la cotización Nro {0} ha sido enviado al proveedor de pagos. El proveedor de pagos respondió con el código de error {1}: {2}.",
                    QuotationNumber,
                    response.ResponseCode,
                    response.ResponseMsg);

                LoggerHelper.Log(CommonEnums.Categories.General, (usuario != null ? "POS-" + usuario.UserLogin : "POS-Venta Directa"), QuotationId, "Error al volver del gateway de pagos", body);
            }

            ViewBag.PaymentFromCardnet = true;
            ViewBag.PaymentStatus = response.ResponseCode == "00" && inserted;
            ViewBag.PaymentMessage = response.ResponseMsg;

            ViewBag.PolicyNumber = sysData.resultQuotation != null ? sysData.resultQuotation.PolicyNumber : "";
            ViewBag.AuthorizationCode = response.AuthorizationCode;
            ViewBag.allowOnlyLoggedUsers = _allowOnlyLoggedUsers;

            TempData["PaymentFromCardnet"] = true;
            TempData["PaymentStatus"] = response.ResponseCode == "00" && inserted;
            TempData["PaymentMessage"] = response.ResponseMsg;
            TempData["PolicyNumber"] = sysData.resultQuotation != null ? sysData.resultQuotation.PolicyNumber : "";
            TempData["AuthorizationCode"] = response.AuthorizationCode;
            TempData["allowOnlyLoggedUsers"] = _allowOnlyLoggedUsers;
            TempData["failInsentingQuotationOnSysFlexOrVO"] = ViewBag.failInsentingQuotationOnSysFlexOrVO;
            TempData["errorGPToSysflexMessage"] = ViewBag.errorGPToSysflexMessage;
            TempData["errorGPToSysflexMessage2"] = ViewBag.errorGPToSysflexMessage2;
            TempData["QuotationNumber"] = base.QuotationNumber;
            TempData["AmountPayByClient"] = amountPayByClient;


            var idEncoded = Utility.Encode(quoId.ToString());
            return RedirectToAction("Index", "Home", new
            {
                id = idEncoded
            });
        }

        public JsonResult CashPayment(int quotationId)
        {
            Entity.Entities.WSEntities.SendQuotationResultWS sysData = new Entity.Entities.WSEntities.SendQuotationResultWS();
            Quotation quotation = oQuotationManager.GetQuotation(quotationId);

            string goodandbad = "";
            string errorGPToSysflexMessage = "";
            string errorGPToSysflexMessage2 = "";
            string policyNo = "";

            bool inserted = false;
            string msgProxy = "";
            var usuario = GetCurrentUsuario();

            if (quotation != null)
            {
                var s = oVirtualOfficeProxy.GetPolicy(quotation.QuotationNumber);
                if (s == true)
                {
                    int realuserid = oDropDownManager.GetParameter("PARAMETER_KEY_USERID_DEFAULT_VO").Value.ToInt();
                    if (usuario != null)
                    {
                        realuserid = usuario.UserID;
                    }

                    //Aqui llamar sp que Eliminara la cotizacion duplicada
                    bool wasDeleted = oVirtualOfficeProxy.DeleteDuplicatePolicy(quotation.QuotationNumber, realuserid); ;

                    if (!wasDeleted)
                    {
                        //Existe en vo
                        msgProxy = "Esta cotización ya ha sido enviada a nuestros sistemas.";
                        return Json(new { success = false, message = msgProxy, quotationId = quotation.Id, policyNo = quotation.PolicyNumber }, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            sysData = this.SendQuotationToCore(quotationId);

            try
            {
                if (!sysData.CantMovement)
                {
                    inserted = false;
                    msgProxy = string.Format("A la Poliza \"{0}\" no se le pueden hacer movimientos ya que la misma esta en Transito", quotation.policyNoMain);
                    return Json(new { success = inserted, message = msgProxy, quotationId = quotation.Id, policyNo = sysData.resultQuotation.PolicyNumber }, JsonRequestBehavior.AllowGet);
                }

                if (sysData.SentToCore && sysData.SentToVO && sysData.SentToCoreWithErrorGP)
                {
                    inserted = true;

                    errorGPToSysflexMessage = oDropDownManager.GetParameter("PARAMETER_KEY_MESSAGE_GP_PASS_TO_SYSFLEX").Value;

                    goodandbad = "GP";

                    sendEmailErrorInvoice(errorGPToSysflexMessage);
                }
                else if (sysData.SentToCore && !sysData.SentToVO && sysData.SentToCoreWithErrorGP)
                {
                    inserted = true;

                    errorGPToSysflexMessage2 = oDropDownManager.GetParameter("PARAMETER_KEY_MESSAGE_GP_PASS_TO_SYSFLEX2").Value;

                    goodandbad = "GP2";

                    errorGPToSysflexMessage2 += ". Hora del Mensaje: " + DateTime.Now.ToString("hh:mm:ss tt");

                    sendEmailErrorInvoice(errorGPToSysflexMessage2);
                }
                else if (sysData.SentToCore && sysData.SentToVO && !sysData.SentToCoreWithErrorGP)//CUANDO TODO SALE BIEN
                {
                    inserted = true;

                    msgProxy = "La cotización se ha pagado y cargado en nuestros sistemas satisfactoriamente.";
                }
                else if (sysData.SentToCore && !sysData.SentToVO && !sysData.SentToCoreWithErrorGP)//CUANDO TODO SALE BIEN MENOS EN VO
                {
                    inserted = true;

                    msgProxy = "Ha ocurrido un error al tratar envíar la cotización a la Bandeja, pero, se genero correctamente el número de póliza en SysFlex....... No. Póliza: " + sysData.resultQuotation.PolicyNumber;

                    goodandbad = "S";
                }
                else if (!sysData.SentToCore && !sysData.SentToVO)
                {
                    inserted = false;

                    msgProxy = "Ha ocurrido un error al tratar envíar la cotización a Sysflex y a la Bandeja.";

                    if (!string.IsNullOrEmpty(sysData.SentToCoreErrorChasis))
                    {
                        msgProxy = sysData.SentToCoreErrorChasis;
                    }

                    return Json(new { success = inserted, message = msgProxy, quotationId = quotation.Id, policyNo = sysData.resultQuotation.PolicyNumber }, JsonRequestBehavior.AllowGet);
                }

                if (inserted)
                {
                    policyNo = sysData.resultQuotation.PolicyNumber;
                    var parameter = new Quotation.parameter
                    {
                        id = quotation.Id.GetValueOrDefault(),
                        policyNumber = sysData.resultQuotation.PolicyNumber,
                        status = 1,
                        monthlyPayment = quotation.MonthlyPayment,
                        financed = quotation.Financed.GetValueOrDefault(),
                        period = quotation.Period,
                        modi_UserId = (GetCurrentUserID() != null ? GetCurrentUserID() : quotation.Modi_UserId),
                        quotationCoreIdNumber = sysData.resultQuotation.QuotationCoreIdNumber,
                        quotationCoreNumber = sysData.resultQuotation.QuotationCoreIdNumber.ToString(),
                    };
                    oQuotationManager.SetQuotation(parameter);

                    GenerateOnbaseFile(quotation.Id.GetValueOrDefault());


                    if (base.RequestType == CommonEnums.RequestType.Emision && !string.IsNullOrEmpty(quotation.couponCode))
                    {
                        var quoPrincipalDr = oQuotationManager.GetQuotationDrivers(quotation.Id.GetValueOrDefault()).FirstOrDefault(x => x.IsPrincipal);
                        SetCouponProspect(quoPrincipalDr.FirstName, quoPrincipalDr.FirstSurname, quoPrincipalDr.Email, quoPrincipalDr.PhoneNumber, policyNo, quotation.CouponProspectId.GetValueOrDefault());
                    }

                    //Enviando nota a global siempre que haya
                    var allNotes = oQuotationManager.GetQuotationNotes(quotation.Id.GetValueOrDefault()).ToList();

                    if (allNotes.Count() > 0)
                    {
                        foreach (var n in allNotes)
                        {
                            oQuotationManager.SendQuotationNotesToGlobal(quotation.Id.GetValueOrDefault(), n.Id);
                        }
                    }
                    //Enviando nota a global siempre que haya
                }
                else
                {
                    msgProxy = "No se ha podido realizar el pago de la cotización mediante Efectivo.";
                }
            }
            catch (Exception ex)
            {
                var quotationNumerError = quotation != null ? " No Cotizacion: " + quotation.QuotationNumber + " " : "N/A ";
                LoggerHelper.Log(CommonEnums.Categories.Error, (usuario != null ? "POS-" + usuario.UserLogin : "POS-Venta Directa"), quotation.Id.GetValueOrDefault(), "Error al realizar un pago mediante Efectivo", "Se produjo un error al enviar un pago mediante el webservice de Cash.\nMensaje: " + quotationNumerError + ex.Message + "\nDetalle: " + ex.StackTrace, ex);
                SendErrorQuotation((quotation != null ? quotation.QuotationNumber : "N/A"), "Error", ex);
            }

            return Json(new { success = inserted, message = msgProxy, quotationId = quotation.Id, policyNo = policyNo, goodandbad = goodandbad, errorGPToSysflexMessage = errorGPToSysflexMessage, errorGPToSysflexMessage2 = errorGPToSysflexMessage2 }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        public JsonResult GetMarbete(int quotationId)
        {
            var quotationNumerError = "";
            try
            {
                string qNumber = "N/A";
                var quotation = oQuotationManager.GetQuotation(quotationId);
                if (quotation == null)
                {
                    qNumber = QuotationNumber;
                }
                else
                {
                    qNumber = quotation.QuotationNumber;
                }

                quotationNumerError = " No Cotizacion: " + qNumber + " ";

                string PathFile = string.Empty;
                //crear el archivo pdf 
                var FileName = string.Concat("Marbete_", qNumber, ".pdf");
                var FullFileName = string.Concat(Server.MapPath("~/Tmp/"), FileName);

                var marbeteDocumentID = oDropDownManager.GetParameter("PARAMETER_KEY_DOCUMENT_ID_MARBETE").Value;

                byte[] Xml = GenerateXMLQuotation_Marbete(quotationId, marbeteDocumentID);

                int THProjectID = oDropDownManager.GetParameter("PARAMETER_KEY_TH_PROJECT_ID").Value.ToInt();
                int THBatchConfigResID = oDropDownManager.GetParameter("PARAMETER_KEY_TH_BATCH_CONFIG_RES_ID").Value.ToInt();

                //var PdfFileByteArray = oThunderheadProxy.SendToTHExecutePreview(null, Xml, THProjectID, THBatchConfigResID);
                var PdfFileByteArray = oThunderheadProxy.NewSendToTHExecutePreview(Xml, THProjectID, THBatchConfigResID);

                System.IO.File.WriteAllBytes(FullFileName, PdfFileByteArray);
                PathFile = @"\Tmp\" + FileName;

                return Json(new { reportName = PathFile }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(CommonEnums.Categories.Error, (GetCurrentUsuario() != null ? "POS-" + GetCurrentUsuario().UserLogin : "POS-Venta Directa"), quotationId, "Error al generar el mabete.", "Detalle: " + quotationNumerError + ex.Message + ex.StackTrace, ex);

                return Json(new { error = "No se ha podido obtener su marbete" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getPolicyInfo(string policyNo, int quotationId)
        {
            IsAQuotation = false;

            var DataCoreCustomer = oCoreProxy.GetClienteFromPoliza(policyNo);
            WsProxy.SysflexService.Policyinclusionvehicle firsVehiclePoliza = null;
            string endDate = "";

            if (DataCoreCustomer != null)
            {
                var vehiclesFromPoliza = oCoreProxy.GetVehiculosFromPoliza(policyNo);

                bool isActivePolicy = true;

                if (vehiclesFromPoliza.Any())
                {
                    firsVehiclePoliza = vehiclesFromPoliza.FirstOrDefault(x => x.EstatusPoliza == "ACTIVO" && x.EstatusItem == "ACTIVO");
                    isActivePolicy = (firsVehiclePoliza == null ? false : true);
                    endDate = firsVehiclePoliza.FechaFin.GetValueOrDefault().ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }

                if (!isActivePolicy)
                {
                    return Json(new { code = "002", msg = string.Format("La póliza declarativa \"{0}\" no esta Activa", policyNo) }, JsonRequestBehavior.AllowGet);
                }

                var quot = oQuotationManager.GetQuotation(quotationId);
                if (quot != null)
                {
                    var quotationUser = oUserManager.GetUser(quot.User_Id, null, null, null);

                    int agentCode = -1;
                    if (quotationUser != null && quotationUser.AgentId.HasValue)
                    {
                        var userAgenCode = getAgenteUserInfo(quotationUser.AgentId.Value);
                        if (userAgenCode != null)
                        {
                            if (userAgenCode.AgentId <= 0)
                            {
                                userAgenCode = getAgenteUserInfo(quotationUser.Username);//que es el nameid
                            }
                            int.TryParse(userAgenCode.AgentCode, out agentCode);
                        }
                    }

                    if (agentCode != DataCoreCustomer.Intermediario.GetValueOrDefault())
                    {
                        return Json(new { code = "003", msg = string.Format("El representante seleccionado no corresponde al de la poliza declarativa \"{0}\" favor revisar.", policyNo) }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(new { code = "001", msg = string.Format("La poliza declarativa \"{0}\" no existe en nuestros sistemas", policyNo) }, JsonRequestBehavior.AllowGet);
            }


            //Si todo lo anterior sale bien valido que la polzia tenga algun vehiculo que sea de un subramo de declarativa
            var isSubRamoDeclarativa = oDropDownManager.GetParameter("PARAMETER_KEY_SUBRAMOS_DECLARATIVAS").Value;
            if (firsVehiclePoliza != null)
            {
                if (!isSubRamoDeclarativa.Contains(firsVehiclePoliza.SubRamo.ToString()))
                {
                    return Json(new { code = "004", msg = string.Format("La poliza \"{0}\" no es una poliza declarativa", policyNo) }, JsonRequestBehavior.AllowGet);
                }
            }
            //

            IsAQuotation = true;

            return Json(new { code = "000", msg = "", realEndDate = endDate }, JsonRequestBehavior.AllowGet);
        }


        public void SendModeFullNotification(string body, string emailToSend)
        {
            if (!string.IsNullOrEmpty(emailToSend))
            {
                var senderError = oDropDownManager.GetParameter("PARAMETER_KEY_EMAIL_SENDER").Value;
                var subject = "Nuevo caso para trabajar en la bandeja de auto";

                List<string> destinatariosList = new List<string>();

                foreach (var e in emailToSend.Split(','))
                {
                    destinatariosList.Add(e);
                }

                SendEmailHelper.SendMail(senderError, destinatariosList, subject, body, null);
            }
        }

        public void SendModeLeyNotificationEmission(string body = "")
        {
            var senderError = oDropDownManager.GetParameter("PARAMETER_KEY_EMAIL_SENDER").Value;
            var subject = "Nueva emisión de una poliza ley";

            string emailToSend = oDropDownManager.GetParameter("PARAMETER_KEY_TEMPLATE_EMAIL_RECEIVER").Value;

            List<string> destinatariosList = new List<string>();

            foreach (var e in emailToSend.Split(','))
            {
                destinatariosList.Add(e);
            }

            SendEmailHelper.SendMail(senderError, destinatariosList, subject, body, null);

        }
    }

}