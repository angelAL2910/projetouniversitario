﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STL.POS.Frontend.Web.NewVersion.CustomCode;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace STL.POS.Frontend.Web.NewVersion.Controllers
{
    public partial class HomeController : BaseController
    {
        public ActionResult InfoAddDriver()
        {
            try
            {
                ViewBag.TitlePage = "INFORMACIÓN ADICIONAL CONDUCTORES";
                ViewBag.QuoId = QuotationId;
                QuotationViewModel quotation = GetDataQuotation(ViewBag.QuoId);
                ViewBag.CountryList = getCountries();
                ViewBag.JobList = getJobs();
                ViewBag.SocialReasonList = getSocialReasonList();

                ViewBag.idetificationTypes = getIdentificationTypes();
                ViewBag.InvoceTypeList = getInvoiceTypes();
                ViewBag.OwnerShipStructureList = getOwnerShipStructure();
                ViewBag.CreditCardTypes = getCreditCardTypes();
                ViewBag.YearList = getYearList();
                ViewBag.TypeOfPersonsList = getTypeOfPersons();
                var principal = quotation._drivers.FirstOrDefault(x => x.IsPrincipal);

                ViewBag.IdentificationFinalBeneficiaryOptionsList = getIdentificationFinalBeneficiaryOptionsList(true);

                ViewBag.PepFormularyOptionsList = getPepFormularyOptionsList(principal.PepFormularyOptionsId.GetValueOrDefault());

                if (quotation != null)
                {
                    ViewBag.DriverList = new SelectList(quotation._drivers.ToList().Select(i => new SelectListItem { Text = string.Concat(i.FirstName, " ", i.FirstSurname), Value = i.Id.ToString() }), "Value", "Text");
                    ViewBag.QuotationNumber = quotation.QuotationNumber;
                }

                ViewBag.onlyLoggedUsers = allowOnlyLoggedUsers();

                ViewBag.Sexes = getSexes("");
                ViewBag.ForeingLicence = getForeingLicence("");


                return
                    PartialView(principal);
            }
            catch (Exception ex)
            {
                var user = GetCurrentUsuario();
                LoggerHelper.Log(CommonEnums.Categories.Error, "POS-" + (user != null ? user.UserLogin : ""), QuotationId, "Error cargando los datos del conductor", "Mensaje: " + ex.Message, ex);

                return
                    RedirectToAction("Index");
            }

        }

        #region Get
        private SelectList getCountries()
        {
            var countries = oDropDownManager.GetDropDown(CommonEnums.DropDownType.COUNTRY.ToString());

            return new SelectList(countries.ToList().Select(i => new SelectListItem { Text = i.name, Value = i.Value }), "Value", "Text");
        }

        private SelectList getJobs()
        {
            var countries = oDropDownManager.GetDropDown(CommonEnums.DropDownType.JOBS.ToString());

            return new SelectList(countries.ToList().Select(i => new SelectListItem { Text = i.name, Value = i.name }), "Value", "Text");
        }

        private SelectList getSocialReasonList()
        {
            var countries = oDropDownManager.GetDropDown(CommonEnums.DropDownType.SOCIALREASON.ToString());

            return new SelectList(countries.ToList().Select(i => new SelectListItem { Text = i.name, Value = i.Value }), "Value", "Text");
        }

        private SelectList getIdentificationFinalBeneficiaryOptionsList(bool IsCompany)
        {
            var countries = oDropDownManager.GetIdentificationFinalBeneficaryOptions(IsCompany);

            return new SelectList(countries.ToList().Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }), "Value", "Text");
        }

        private SelectList getIdentificationTypes()
        {
            var param = oDropDownManager.GetParameter("PARAMETER_KEY_IDENTIFICATION_TYPE").Value;

            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(param);
            var output = from pair in values
                         where pair.Value != "Licencia"
                         select new { id = pair.Value, name = pair.Value };

            return new SelectList(output.ToList().Select(i => new SelectListItem { Text = i.name, Value = i.name }), "Value", "Text");
        }

        private SelectList getInvoiceTypes()
        {
            var param = oDropDownManager.GetParameter("PARAMETER_KEY_TYPE_INVOICE").Value;

            Dictionary<int, string> values = JsonConvert.DeserializeObject<Dictionary<int, string>>(param);

            var output = from pair in values
                         select new { id = pair.Key, name = pair.Value };

            return new SelectList(output.ToList().Select(i => new SelectListItem { Text = i.name, Value = i.id.ToString() }), "Value", "Text");
        }


        public ActionResult GetTypesInvoice()
        {
            var param = oDropDownManager.GetParameter("PARAMETER_KEY_TYPE_INVOICE").Value;

            Dictionary<int, string> values = JsonConvert.DeserializeObject<Dictionary<int, string>>(param);

            var output = from pair in values
                         select new { id = pair.Key, name = pair.Value };

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProvinces()
        {
            var provinces = oDropDownManager.GetProvices(currentCountry);

            return Json(provinces.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMunicipalities(int provinceID)
        {
            var municipalities = oDropDownManager.GetMunicipalities(currentCountry, provinceID);

            return Json(municipalities.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSectors(string municipalityID)
        {

            var keys = !string.IsNullOrEmpty(municipalityID) ? (municipalityID).Split('-') : new object[] { };

            var stprovId = Convert.ToInt32(keys[1]);
            var municipalityId = Convert.ToInt32(keys[2]);

            var cities = oDropDownManager.GetCities(currentCountry, stprovId, municipalityId);

            return Json(cities.ToList(), JsonRequestBehavior.AllowGet);
        }

        private SelectList getOwnerShipStructure()
        {
            var param = oDropDownManager.GetDropDown(CommonEnums.DropDownType.OWNERSHIPSTRUCTURE.ToString()).ToList();
            param = param.Where(o => o.Value != "4").ToList();

            return new SelectList(param.Select(i => new SelectListItem { Text = i.name, Value = i.Value }), "Value", "Text");
        }

        private SelectList getPepFormularyOptionsList(int SelectedValue)
        {
            var param = oDropDownManager.GetPepFormularyOptions();
            return new SelectList(param.ToList().Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString(), Selected = i.Id.ToString() == SelectedValue.ToString() ? true : false }), "Value", "Text");
        }

        public ActionResult GetDriver(int driverID)
        {
            var driver = oDriverManager.GetDriver(driverID);
            driver.BirthDay = driver.DateOfBirth.ToString("dd-MMM-yyyy", culturelanguaje).ToLower();
            //driver.DateOfBirth = driver.DateOfBirth.ToString("dd-MMM-yyyy", culturelanguaje);

            driver.IdentificationValidDate = driver.IdentificationNumberValidDate.GetValueOrDefault().ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture).ToLower();

            return Json(driver, JsonRequestBehavior.AllowGet);
        }

        private SelectList getYearList()
        {
            int anioActual = DateTime.Now.Year;
            int paramTop = 0;
            List<Entity.Entities.Generic> ListYear = new List<Entity.Entities.Generic>();
            Entity.Entities.Generic result;
            paramTop = anioActual + oDropDownManager.GetParameter("PARAMETER_KEY_TOP_LIST_YEAR").Value.ToInt();

            for (int i = anioActual; i <= paramTop; i++)
            {
                result = new Entity.Entities.Generic();
                result.Value = i.ToString();
                result.name = i.ToString();
                ListYear.Add(result);
            }

            return new SelectList(ListYear.Select(i => new SelectListItem { Text = i.name, Value = i.Value }), "Value", "Text");
        }

        private SelectList getCreditCardTypes()
        {
            var param = oDropDownManager.GetDropDown(CommonEnums.DropDownType.CRIDITCARTYPES.ToString());

            return new SelectList(param.ToList().Select(i => new SelectListItem { Text = i.name, Value = i.Value }), "Value", "Text");
        }

        public ActionResult GetRelationShip()
        {
            var Param = oDropDownManager.GetDropDown(CommonEnums.DropDownType.RELATIONSHIP.ToString());

            return Json(Param.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPepsFormularyByDriver(int driverID, string Source)
        {
            var peps = oDriverManager.GetPepsFormularyByDriver(driverID, Source);

            return Json(peps.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getBeneficiariesByDriver(int driverID)
        {

            var beneficiaries = oDriverManager.GetDriverBeneficiaries(driverID);

            return Json(beneficiaries.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowPepExplication()
        {
            var Param = oDropDownManager.GetParameter("PARAMETER_KEY_PEP_EXPLICACION").Value;

            StringBuilder text = new StringBuilder();
            text.AppendFormat("{0}", Param);
            //text.AppendFormat("<p>{0}</p>", Param);
            //text.AppendFormat("<p>Definición de Opciones:</p>");
            //text.AppendFormat("<p>Si, Designado: {0}</p>", des);
            //text.AppendFormat("<p>Si, Vinculado: {0}</p>", vin);

            return Json(text.ToString(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowBenefExplication(string pOrgin)
        {
            var Param = pOrgin != "2" ? oDropDownManager.GetParameter("PARAMETER_KEY_BENEFICIARY_FINAL_EXPLICACION").Value : oDropDownManager.GetParameter("PARAMETER_KEY_BENEFICIARY_FINAL_EXPLICACION_2").Value;

            StringBuilder text = new StringBuilder();
            text.AppendFormat("{0}", Param);

            return Json(text.ToString(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetQuotationData()
        {
            var quotation = getDataQuotation(QuotationId);
            if (quotation.Financed.GetValueOrDefault())
                ViewBag.Financed = true;
            else
                ViewBag.Financed = false;

            return Json(quotation, JsonRequestBehavior.AllowGet);
        }
        public IEnumerable<Entity.Entities.PepFormulary> HasPEP(int driverID)
        {
            var pep = oDriverManager.GetPepsFormularyByDriver(driverID, "CalidadPep");
            return (pep.ToList());
        }
        public bool HasBenef(int driverID)
        {
            var benef = oDriverManager.GetDriverBeneficiaries(driverID);
            return (benef.ToList().Count > 0);
        }

        public ActionResult DocumentValidator(string Number, string DocumentType, string noQuot)
        {
            var result = true;
            result = ValidateDocument(Number, DocumentType, noQuot);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool ValidateDocument(string Number, string DocumentType, string noQuot)
        {
            var result = true;
            //valido si el QuotationNumber que se envió desde el Hidden field no esta vacio para entonces tomarlo desde la db con el quotationID que tenemos en la sesion
            if (string.IsNullOrEmpty(noQuot))
            {
                var quotation = getQuotationData(QuotationId);
                if (quotation != null)
                {
                    noQuot = quotation.QuotationNumber;
                }
            }

            if (!string.IsNullOrEmpty(noQuot))
            {
                var r = oQuotationManager.getQuotationToNotValidate(noQuot);
                if (r)
                {
                    return true;
                }

                Number = Number.Replace("-", "");
                var documentType = (Entity.Entities.DocumentValidator.DocumentType)Enum.Parse(typeof(Entity.Entities.DocumentValidator.DocumentType), DocumentType);

                var oDocumentValidator = new Entity.Entities.DocumentValidator();

                switch (documentType)
                {
                    case Entity.Entities.DocumentValidator.DocumentType.Cedula:
                    case Entity.Entities.DocumentValidator.DocumentType.Licencia:
                        result = oDocumentValidator.IsValidModulo10(Number);
                        break;
                    case Entity.Entities.DocumentValidator.DocumentType.Rnc:
                        result = oDocumentValidator.IsValidModulo11(Number);
                        break;
                }
            }
            return result;
        }
        public ActionResult getBeneficiaryPercents()
        {
            Entity.Entities.Generic result = new Entity.Entities.Generic();
            var param = oDropDownManager.GetParameter("PARAMETER_KEY_BENEFICIARY_PERCENTS").Value;
            string[] separators = { "|" };
            var Key = param.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToArray();
            result.Value = Key[0];
            result.name = Key[1];

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRncCedulaValidation()
        {
            var Param = oDropDownManager.GetParameter("PARAMETER_KEY_RNC_CEDULA_VALIDACION").Value;
            return Json(Param, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRncCedulaValidationShow()
        {
            var Param = oDropDownManager.GetParameter("PARAMETER_KEY_RNC_CEDULA_VALIDACION_SHOW").Value;
            return Json(Param, JsonRequestBehavior.AllowGet);
        }
        public string getParameterTotalPrimium()
        {
            var result = "0";
            var oResult = oDropDownManager.GetParameter("PARAMETER_KEY_TOTAL_PRIMIUM_VALIDATION_LAW").Value;
            if (!string.IsNullOrEmpty(oResult))
                result = oResult;

            oResult = "";
            return result;
        }
        private SelectList getTypeOfPersons()
        {
            var param = oDropDownManager.GetDropDown(CommonEnums.DropDownType.TYPEOFPERSONS.ToString()).ToList();
            if (param.Count > 0)
            {
                var result = param.Select(x => new
                {
                    TypeOfPerson = x.Value,
                    name = x.name
                });

                return new SelectList(result.ToList().Select(i => new SelectListItem { Text = i.name, Value = i.TypeOfPerson }), "Value", "Text");
            }

            return new SelectList(param.ToList().Select(i => new SelectListItem { Text = i.name, Value = i.Value }), "Value", "Text");
        }
        public ActionResult GetDefaultCityCode()
        {
            string result = "";
            var Param = oDropDownManager.GetDropDown(CommonEnums.DropDownType.DEFAULTCITY.ToString()).ToList().FirstOrDefault();
            var city = Param.Value.Split('-');
            result = city[city.Length - 1];
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        public int GetVirtualOfficeIdentificationType(string posType)
        {
            if (posType == "Cédula")
                return 1;
            else if (posType == "Licencia")
                return 3;
            else if (posType == "RNC")
                return 5;
            else if (posType == "Pasaporte")
                return 2;
            else return 0;
        }
        public ActionResult ValidateRiskLevelCountry(int CustomerNationalityID, decimal pTotalPremium, int? TypeOfPerson, int? PepOptionID)
        {
            var result = "";

            #region Este codigo fue comentado porque se centralizó la validacion del nivel de riesgo para que todo esté del lado de global este codigo es funcional si eligen un pais de la lista indicada por cumplimiento
            //List<int> HightRiskLevelCountries = new List<int>();
            //var vehicles = oQuotationManager.GetQuotationVehicles(QuotationId);

            //#region Buscando los parametros de la DB

            //var pHightRiskLevelCountries = oDropDownManager.GetParameter("PARAMETER_KEY_LIST_COUNTRY_HIGHT_RISK_LEVEL").Value; // Codigos unicos de los tipos de actividades a evaluar para objetos valiosos

            //#endregion

            //#region Creando una lista con los id de tipos de vivienda que se deben evaluar
            //if (pHightRiskLevelCountries.Contains("|"))
            //{
            //    var countries = pHightRiskLevelCountries.Split('|');
            //    if (countries.Any())
            //    {
            //        for (var i = 0; i <= countries.Length - 1; i++)
            //        {
            //            HightRiskLevelCountries.Add(countries[i].ToInt());
            //        }
            //    }
            //}
            //else if (!string.IsNullOrEmpty(pHightRiskLevelCountries))
            //    HightRiskLevelCountries.Add(pHightRiskLevelCountries.ToInt());
            //#endregion

            //if (HightRiskLevelCountries.Any())
            //{
            //    if (HightRiskLevelCountries.Contains(CustomerNationalityID))
            //        result = "RA"; //riesgo alto
            //}
            #endregion
            if (TypeOfPerson != null && (TypeOfPerson == 5 || TypeOfPerson == 6)) //Valido si el tipo de cliente es un Organismos Públicos Nacional e Internacional para marcarlo como Riesgo bajo por defecto
                result = "RB";
            else if (PepOptionID != null && PepOptionID != 3)
                result = "RA";
            else
            {
                var QuotationDataVehicles = oQuotationManager.GetQuotationVehicles(QuotationId);
                var vechicleCount = QuotationDataVehicles.Count();
                var vehicleValues = QuotationDataVehicles.Sum(x => x.VehiclePrice);
                var insuredAmount = QuotationDataVehicles.Sum(x => x.InsuredAmount);

                result = this.getRiskGetRiskLevelAuto(vechicleCount, vechicleCount > 1 ? "" : QuotationDataVehicles.FirstOrDefault().UsageName, pTotalPremium, vechicleCount <= 1 ? 0 : vehicleValues, vechicleCount <= 1 ? 0 : insuredAmount, CustomerNationalityID);
                result = string.IsNullOrEmpty(result) ? "" : JsonConvert.DeserializeObject(result).ToString();
            }

            base.RiskLevel = result;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string getRiskGetRiskLevelAuto(int? vechicleCount, string Uso, decimal? PremiumAmount, decimal? TotalVehicleValue, decimal? TotalVechicleInsuredAmount, int? PaisId)
        {
            return
                oVirtualOfficeProxy.getRiskGetRiskLevelAuto(vechicleCount, vechicleCount > 1 ? "" : Uso, PremiumAmount, TotalVehicleValue, TotalVechicleInsuredAmount, PaisId);
        }

        public ActionResult GetAlertBenefRNC()
        {
            var result = oDropDownManager.GetParameter("PARAMETER_KEY_BENEFICIARY_RNC_ALERT").Value; // busco la alerta definida por cumplimiento para mostrar al usuario cuando haya un beneficiario tipo RNC

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShowAutoCertificationFinalBeneficiaries()
        {
            string PathFile = string.Empty;

            //crear el archivo pdf 
            var FileName = string.Concat(oDropDownManager.GetParameter("PARAMETER_KEY_CERTIFICATION_BENEFICIARY_FILE_NAME").Value, ".pdf");


            //crear el archivo pdf 

            var FullFileName = string.Concat(Server.MapPath("~/Tmp/"), FileName);

            int THProjectID = oDropDownManager.GetParameter("PARAMETER_KEY_TH_PROJECT_ID").Value.ToInt();
            int THBatchConfigResID = oDropDownManager.GetParameter("PARAMETER_KEY_TH_BATCH_CONFIG_RES_ID").Value.ToInt();


            byte[] Xml = GenerateXMLAutoCertificationBeneficiaries();
            //var PdfFileByteArray = oThunderheadProxy.SendToTHExecutePreview(null, Xml, THProjectID, THBatchConfigResID);
            var PdfFileByteArray = oThunderheadProxy.NewSendToTHExecutePreview(Xml, THProjectID, THBatchConfigResID);
            System.IO.File.WriteAllBytes(FullFileName, PdfFileByteArray);
            PathFile = @"\Tmp\" + FileName;

            return
                  Json(PathFile, JsonRequestBehavior.AllowGet);
        }

        private byte[] GenerateXMLAutoCertificationBeneficiaries()
        {


            var result = new byte[] { };

            var oPosAuto = new CustomCode.TH.POS_AUTO();
            var oTransaction = new CustomCode.TH.Transaction();





            oTransaction.DocumentId = oDropDownManager.GetParameter("PARAMETER_KEY_DOC_ID_CERTIFICATION_BENEF").Value.ToString();//Document Id



            // oPosAuto.Quotation = oQuotation;
            oPosAuto.Transaction = oTransaction;

            var DocXML = Utility.SerializeToXMLString(oPosAuto);

            result = Encoding.UTF8.GetBytes(DocXML);

            return
                 result;
        }
        #endregion
        #region Set

        public ActionResult SetAditionalInformationDriver(QuotationViewModel.drivers drivers)
        {
            Entity.Entities.Generic result = new Entity.Entities.Generic();
            try
            {

                string[] separators = { "-" };
                var cityKey = drivers.strCityID.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToArray();
                int muncicipalty = 0;
                int cityID = 0;
                int domesticregID = 0;
                string action = "";
                if (cityKey != null)
                {
                    domesticregID = cityKey[0].ToInt();
                    muncicipalty = cityKey[2].ToInt();
                    cityID = cityKey[3].ToInt();
                }


                if (drivers.Id <= 0)
                {
                    throw new Exception("El id del conductor no fue seleccionado correctamente");
                }

                if (string.IsNullOrEmpty(drivers.IdentificationType))
                {
                    throw new Exception("Debe seleccionar el Tipo de Identificación");
                }
                else if (string.IsNullOrEmpty(drivers.IdentificationNumber))
                {
                    throw new Exception("Debe indicar el Número de Identificación");
                }
                else
                {
                    var docType = "";
                    if (drivers.IdentificationType != "Pasaporte")
                    {
                        if (drivers.IdentificationType == "Cédula" || drivers.IdentificationType == "Licencia")
                        {
                            docType = "Cedula";
                        }
                        else if (drivers.IdentificationType == "RNC")
                        {
                            docType = "Rnc";
                        }

                        var IsValid = ValidateDocument(drivers.IdentificationNumber, docType, string.Empty);
                        if (!IsValid)
                        {
                            throw new Exception("El Número de Identificación '" + drivers.IdentificationNumber + "' no es valido, por favor verifique.");
                        }
                    }
                }

                //pregunto si posee calidad pep para validar que hayan insertado los registros correspondientes
                if (drivers.PepFormularyOptionsId.HasValue && drivers.PepFormularyOptionsId != CommonEnums.PepFormularyOptions.No.ToInt())
                {
                    var peps = HasPEP(drivers.Id).ToList();
                    if (peps.Count <= 0)
                    {
                        throw new Exception("Debe indicar las personas que están políticamente Expuestas");
                    }
                    else
                    {
                        foreach (var item in peps)
                        {
                            if (item.RelationshipId <= 0 || string.IsNullOrEmpty(item.name) || string.IsNullOrEmpty(item.Position) || item.Position == "-")
                                throw new Exception("Encontramos algunos PEPs con información incompleta, favor verificar");
                        }

                    }
                }

                //pregunto si posee beneficiario final
                if (drivers.IdentificationFinalBeneficiaryOptionsId.HasValue && drivers.IdentificationFinalBeneficiaryOptionsId != CommonEnums.FinalBeneficiaryOptions.No.ToInt())
                {
                    if (!HasBenef(drivers.Id))
                    {
                        throw new Exception("Debe indicar los beneficiarios Finales");
                    }
                }

                if (string.IsNullOrEmpty(drivers.IdentificationNumber.Replace("-", "")))
                {
                    throw new Exception("Debe indicar el número de identificación");
                }

                if (!string.IsNullOrEmpty(drivers.ManagerName)) //valido si digitaron algun nombre de gerente
                {
                    //pregunto si no posee beneficiario para validar que se hayan capturado los peps del gerente general, administrador o representante legal de la compañia
                    if (drivers.IdentificationFinalBeneficiaryOptionsId.HasValue)
                    {
                        if (drivers.IdentificationFinalBeneficiaryOptionsId == CommonEnums.FinalBeneficiaryOptions.No.ToInt())
                        {

                            if (drivers.ManagerPepOptionId.HasValue && drivers.ManagerPepOptionId != CommonEnums.PepFormularyOptions.No.ToInt())
                            {
                                var ManagerPeps = oDriverManager.GetPepsFormularyByDriver(drivers.Id, "AdminPep").ToList();
                                if (ManagerPeps.Count <= 0)
                                {
                                    throw new Exception("Debe indicar las personas que están politicamente expuestas (PEPs) para el Gerente general, Administrador o Representante Legal de la compañía");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (drivers.ManagerPepOptionId.HasValue && drivers.ManagerPepOptionId != CommonEnums.PepFormularyOptions.No.ToInt())
                        {
                            var ManagerPeps = oDriverManager.GetPepsFormularyByDriver(drivers.Id, "AdminPep").ToList();
                            if (ManagerPeps.Count <= 0)
                            {
                                throw new Exception("Debe indicar las personas que están politicamente expuestas (PEPs) para el Gerente general, Administrador o Representante Legal de la compañía");
                            }
                        }
                    }
                }

                var parameter = new QuotationViewModel.drivers.PersonParameters
                {
                    id = drivers.Id,
                    firstName = drivers.FirstName,
                    secondName = drivers.SecondName,
                    firstSurname = string.IsNullOrEmpty(drivers.FirstSurname) ? "" : drivers.FirstSurname,
                    secondSurname = string.IsNullOrEmpty(drivers.SecondSurname) ? "" : drivers.SecondSurname,
                    dateOfBirth = drivers.IdentificationType != "RNC" ? drivers.DateOfBirth : new DateTime(1753, 01, 01),
                    address = string.IsNullOrEmpty(drivers.Address) ? "" : drivers.Address,
                    phoneNumber = string.IsNullOrEmpty(drivers.PhoneNumber) ? "" : drivers.PhoneNumber,
                    mobile = string.IsNullOrEmpty(drivers.Mobile) ? "" : drivers.Mobile,
                    workPhone = string.IsNullOrEmpty(drivers.WorkPhone) ? "" : drivers.WorkPhone,
                    maritalStatus = drivers.MaritalStatus,
                    job = string.IsNullOrEmpty(drivers.Job) ? "" : drivers.Job,
                    company = string.IsNullOrEmpty(drivers.Company) ? "" : drivers.Company,
                    yearsInCompany = drivers.YearsInCompany,
                    sex = drivers.Sex,
                    country_Id = null,
                    domesticreg_Id = domesticregID,
                    state_Prov_Id = drivers.City_State_Prov_Id,
                    city_Id = cityID,
                    nationalityGlobalCountry_Id = drivers.Nationality_Global_Country_Id,
                    email = string.IsNullOrEmpty(drivers.Email) ? "" : drivers.Email,
                    identificationType = drivers.IdentificationType,
                    identificationNumber = drivers.IdentificationNumber.ToString().Replace("-", ""),
                    identificationNumberValidDate = drivers.IdentificationNumberValidDate,
                    invoiceTypeId = drivers.InvoiceTypeId,
                    userId = GetCurrentUserID(),
                    postalCode = drivers.PostalCode,
                    annualIncome = drivers.AnnualIncome,
                    socialReasonId = drivers.SocialReasonId,
                    ownershipStructureId = drivers.OwnershipStructureId,
                    identificationFinalBeneficiaryOptionsId = drivers.IdentificationFinalBeneficiaryOptionsId,
                    pepFormularyOptionsId = drivers.PepFormularyOptionsId,
                    isPrincipal = drivers.IsPrincipal,
                    home_Owner = drivers.Home_Owner.GetValueOrDefault(),
                    linked = drivers.Linked,
                    segment = drivers.Segment,
                    qtyPersonsDepend = drivers.QtyPersonsDepend.GetValueOrDefault() > 0 ? drivers.QtyPersonsDepend.GetValueOrDefault() : 0,
                    qtyEmployees = drivers.QtyEmployees.GetValueOrDefault() > 0 ? drivers.QtyEmployees.GetValueOrDefault() : 0,
                    WorkAddress = drivers.WorkAddress,
                    PlaceOfBirth = drivers.PlaceOfBirth,
                    TypeOfPerson = drivers.TypeOfPerson,
                    ManagerName = drivers.ManagerName,
                    ManagerPepOptionId = drivers.ManagerPepOptionId,

                    foreignLicense = drivers.ForeignLicense
                };

                var oresult = oPersonManagerManager.SetPerson(parameter);

                var quotation = oQuotationManager.GetQuotation(QuotationId);
                decimal? MonthlyPayment = null;
                bool financed = false;
                int? period = null;

                if (quotation != null)
                {
                    MonthlyPayment = quotation.MonthlyPayment;
                    financed = quotation.Financed.GetValueOrDefault();
                    period = quotation.Period;
                }

                string fullnameclient = string.Concat(drivers.FirstName, string.IsNullOrEmpty(drivers.FirstSurname) ? "" : " " + drivers.FirstSurname);

                var quotationParameter = new Entity.Entities.Quotation.parameter
                {
                    id = QuotationId,
                    lastStepVisited = 3,
                    monthlyPayment = MonthlyPayment,
                    financed = financed,
                    period = period,
                    modi_UserId = (GetCurrentUserID() != null ? GetCurrentUserID() : quotation.Modi_UserId),
                    RiskLevel = !string.IsNullOrEmpty(base.RiskLevel) ? base.RiskLevel : null,
                    principalFullName = fullnameclient,
                    principalIdentificationNumber = drivers.IdentificationNumber.ToString().Replace("-", "")
                };

                try
                {
                    var lastVisited = oQuotationManager.SetQuotation(quotationParameter);
                }
                catch (Exception)
                {
                    throw new Exception("error guardando el último paso de la cotización");
                }

                result.Value = "success";
                result.name = "success";
            }
            catch (Exception ex)
            {
                result.Value = "Error";
                result.name = ex.Message;

                var user = GetCurrentUsuario();
                LoggerHelper.Log(CommonEnums.Categories.Error, "POS-" + (user != null ? user.UserLogin : ""), QuotationId, "Error al guardar los datos del conductor", "Mensaje: " + ex.Message, ex);
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SetDomiciliationQuotation(QuotationViewModel thisQuotation)
        {
            string strCreditCardNumber = string.Empty;
            string Credit_Card_Number_Key = "";
            int longitudMask = 0;
            int capturedLeght = 0;
            string MaskValidation = "";
            Entity.Entities.Generic result = new Entity.Entities.Generic();
            try
            {
                //var quotation = oQuotationManager.GetQuotation(thisQuotation.Id);//Esa variable siempre llega null
                var quotation = oQuotationManager.GetQuotation(QuotationId);
                string Mask = oDropDownManager.GetParameter("PARAMETER_KEY_CREDIT_CARD_MASK").Value; //leo desde la base de datos el key completo de la tarjeta para reemplazar los * necesarios

                if (thisQuotation.Domiciliation.GetValueOrDefault())
                {
                    #region Validaciones de la domiciliación
                    if (thisQuotation.Credit_Card_Type_Id.GetValueOrDefault() <= 0)
                    {
                        throw new Exception("El tipo de tarjeta de crédito no fue seleccionado correctamente");
                    }

                    if (string.IsNullOrEmpty(thisQuotation.Credit_Card_Number.Replace("-", "").Replace("_", "")))
                    {
                        throw new Exception("El número de tarjeta de crédito no fue capturado");
                    }

                    if (thisQuotation.Expiration_Date_Year.GetValueOrDefault() <= 0)
                    {
                        throw new Exception("El año de expiración de la tarjeta de crédito no fue capturado");
                    }
                    if (thisQuotation.Expiration_Date_Month.GetValueOrDefault() <= 0)
                    {
                        throw new Exception("El mes de expiración de la tarjeta de crédito no fue capturado");
                    }

                    if (string.IsNullOrEmpty(thisQuotation.Card_Holder))
                    {
                        throw new Exception("El Tarjetahabiente de la tarjeta de crédito no fue capturado");
                    }

                    if (thisQuotation.Credit_Card_Type_Id.GetValueOrDefault() == CommonEnums.CreditCardType.AmericanExpress.ToInt())
                    {
                        var paramter_Mask = oDropDownManager.GetParameter("PARAMETER_KEY_CREDIT_CARD_AMERICAN_EXPRESS_MASK");
                        if (paramter_Mask != null)
                            MaskValidation = paramter_Mask.Value.Replace("-", string.Empty);
                    }
                    else if (thisQuotation.Credit_Card_Type_Id.GetValueOrDefault() == CommonEnums.CreditCardType.Visa.ToInt())
                    {
                        var parameterMask = oDropDownManager.GetParameter("PARAMETER_KEY_CREDIT_CARD_VISA_MASK");
                        if (parameterMask != null)
                            MaskValidation = parameterMask.Value.Replace("-", string.Empty);

                        //MaskValidation = oDropDownManager.GetParameter("PARAMETER_KEY_CREDIT_CARD_VISA_MASK").Value.ToString().Replace("-", string.Empty);
                    }
                    else if (thisQuotation.Credit_Card_Type_Id.GetValueOrDefault() == CommonEnums.CreditCardType.MasterCard.ToInt())
                    {
                        var parameterMask = oDropDownManager.GetParameter("PARAMETER_KEY_CREDIT_CARD_MASTER_CARD_MASK");
                        if (parameterMask != null)
                            MaskValidation = parameterMask.Value.Replace("-", string.Empty);

                    }

                    //valido que se haya encontrado la mascara de las tarjetas de creditos contempladas
                    if (string.IsNullOrEmpty(MaskValidation))
                    {
                        throw new Exception("No se encontró el parámetro de la tarjeta de crédito seleccionado; consulte al administrador del sistema");
                    }

                    capturedLeght = thisQuotation.Credit_Card_Number.Replace("-", "").Replace("_", "").Length;
                    if (!thisQuotation.Credit_Card_Number.Contains("*"))
                    {
                        if (capturedLeght != MaskValidation.Length)
                        {
                            throw new Exception("El número de tarjeta suministrado es incorrecto, favor verificar");
                        }
                    }
                    #endregion
                }

                if (!string.IsNullOrEmpty(thisQuotation.Credit_Card_Number))
                {
                    if (!thisQuotation.Credit_Card_Number.Contains("*"))
                    {
                        Credit_Card_Number_Key = thisQuotation.Credit_Card_Number.Replace("-", "");
                        longitudMask = Credit_Card_Number_Key.Length - 4;
                        strCreditCardNumber = Utility.Encrypt_Query(Credit_Card_Number_Key);
                        Credit_Card_Number_Key = string.Concat(Mask.Substring(0, longitudMask), Credit_Card_Number_Key.Substring(longitudMask, Credit_Card_Number_Key.Length - longitudMask));
                    }
                    else
                    {
                        strCreditCardNumber = quotation.Credit_Card_Number;
                        Credit_Card_Number_Key = thisQuotation.Credit_Card_Number;
                    }
                }
                else
                {
                    strCreditCardNumber = string.Empty;
                }

                decimal? MonthlyPayment = null;
                bool financed = false;
                int? period = null;

                if (quotation != null)
                {
                    MonthlyPayment = quotation.MonthlyPayment;
                    financed = quotation.Financed.GetValueOrDefault();
                    period = quotation.Period;
                }

                var parameter = new Entity.Entities.Quotation.parameter
                {
                    id = QuotationId,
                    credit_Card_Type_Id = thisQuotation.Credit_Card_Type_Id,
                    credit_Card_Number_Key = Credit_Card_Number_Key,
                    credit_Card_Number = strCreditCardNumber,
                    expiration_Date_Year = thisQuotation.Expiration_Date_Year,
                    expiration_Date_Month = thisQuotation.Expiration_Date_Month,
                    card_Holder = thisQuotation.Card_Holder,
                    domiciliation = thisQuotation.Domiciliation,
                    modi_UserId = (GetCurrentUserID() != null ? GetCurrentUserID() : quotation.Modi_UserId),
                    DomicileInitialPayment = thisQuotation.DomicileInitialPayment,
                    monthlyPayment = MonthlyPayment,
                    financed = financed,
                    period = period,
                    RiskLevel = !string.IsNullOrEmpty(base.RiskLevel) ? base.RiskLevel : null
                };

                var oresult = oQuotationManager.SetQuotation(parameter);
                quotation = null;

                result.Value = "success";
                result.name = "success";

            }
            catch (Exception ex)
            {
                result.Value = "Error";
                result.name = ex.Message;
                Credit_Card_Number_Key = "";

                var user = GetCurrentUsuario();
                LoggerHelper.Log(CommonEnums.Categories.Error, "POS-" + (user != null ? user.UserLogin : ""), QuotationId, "Error al guardar la domiciliación", "Mensaje: " + ex.Message, ex);
            }

            return Json(new { result, Credit_Card_Number = Credit_Card_Number_Key }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SetPepsFormularyDriver(string pep)
        {
            var result = new Entity.Entities.Generic();
            var param = new List<Entity.Entities.PepFormulary>();
            try
            {
                param = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Entity.Entities.PepFormulary>>(pep);

                if (param.Count > 0)
                {
                    var Deleted = oDriverManager.DeletePepsByDriver(param.FirstOrDefault().PersonsID.ToInt(), param[0].IsPepManagerCompany == false ? "CalidadPep" : "AdminPep");

                    if (param.FirstOrDefault().PersonsID.ToInt() <= 0)
                        return Json(new { result }, JsonRequestBehavior.AllowGet);

                    if (string.IsNullOrEmpty(param.FirstOrDefault().name))
                        return Json(new { result }, JsonRequestBehavior.AllowGet);

                    if (string.IsNullOrEmpty(param.FirstOrDefault().Position))
                        return Json(new { result }, JsonRequestBehavior.AllowGet);

                    foreach (var item in param)
                    {
                        var parameter = new Entity.Entities.PepFormulary.Parameter
                        {
                            personsID = item.PersonsID,
                            name = item.name,
                            position = item.Position,
                            fromYear = item.FromYear,
                            toYear = item.ToYear,
                            relationshipId = item.RelationshipId,
                            userId = GetCurrentUserID(),
                            BeneficiaryId = item.BeneficiaryId,
                            IsPepManagerCompany = item.IsPepManagerCompany
                        };

                        var ExecutionResult = oDriverManager.SetPepByDriver(parameter);
                    }
                    result.Value = "success";
                    result.name = "success";
                }
            }
            catch (Exception ex)
            {
                result.Value = "Error";
                result.name = ex.Message;
                var user = GetCurrentUsuario();
                LoggerHelper.Log(CommonEnums.Categories.Error, "POS-" + (user != null ? user.UserLogin : ""), QuotationId, "Error al guardar los datos de PEP", "Mensaje: " + ex.Message, ex);
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeletePeps(string driverID)
        {
            var result = new Entity.Entities.Generic();
            try
            {
                var Deleted = oDriverManager.DeletePepsByDriver(driverID.ToInt(), "CalidadPep");

                result.Value = "success";
                result.name = "success";

            }
            catch (Exception ex)
            {
                result.Value = "Error";
                result.name = ex.Message;
                var user = GetCurrentUsuario();
                LoggerHelper.Log(CommonEnums.Categories.Error, "POS-" + (user != null ? user.UserLogin : ""), QuotationId, "Error al eliminar los datos de PEP", "Mensaje: " + ex.Message, ex);
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetDriverBeneficiaries(string beneficiaries)
        {
            bool hasPeps = false;
            int driverId = 0;

            Entity.Entities.Generic result = new Entity.Entities.Generic();
            if (string.IsNullOrEmpty(beneficiaries))
            {
                return Json(new { result }, JsonRequestBehavior.AllowGet);
            };
            var param = JsonConvert.DeserializeObject<List<Entity.Entities.IdentificationFinalBeneficiary>>(beneficiaries);

            if (param.Count > 0)
            {
                var Deleted = oDriverManager.DeleteBeneficiariesByDriver(param.FirstOrDefault().personsID.ToInt());

                try
                {
                    driverId = param.FirstOrDefault().personsID.ToInt();
                    foreach (var item in param)
                    {
                        if (item.personsID.ToInt() <= 0)
                        {
                            result.Value = "Error";
                            result.name = "El id del conductor no fue seleccionado correctamente";
                            return Json(new { result }, JsonRequestBehavior.AllowGet);
                        }

                        if (string.IsNullOrEmpty(item.name))
                        {
                            result.Value = "Error";
                            result.name = "El nombre del beneficiario no fue seleccionado correctamente";
                            return Json(new { result }, JsonRequestBehavior.AllowGet);
                        }

                        if (item.percentageParticipation == null)
                        {
                            result.Value = "Error";
                            result.name = "Debe indicar el porcentaje de participación de " + item.name;
                            return Json(new { result }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrEmpty(item.IdentificationTypeId))
                        {
                            result.Value = "Error";
                            result.name = "Delebe seleccionar el tipo de Identificación de " + item.name;
                            return Json(new { result }, JsonRequestBehavior.AllowGet);
                        }

                        if (string.IsNullOrEmpty(item.IdentificationNumber))
                        {
                            result.Value = "Error";
                            result.name = "Delebe indicar el número de Identificación de " + item.name;
                            return Json(new { result }, JsonRequestBehavior.AllowGet);
                        }

                        if (!item.NationalityCountryId.HasValue)
                        {
                            result.Value = "Error";
                            result.name = "Delebe indicar la nacionalidad de " + item.name;
                            return Json(new { result }, JsonRequestBehavior.AllowGet);
                        }

                        var parameter = new Entity.Entities.IdentificationFinalBeneficiary.Parameter
                        {
                            id = item.Id,
                            personsID = item.personsID,
                            name = item.name,
                            percentageParticipation = item.percentageParticipation,
                            //isPEP = item.isPEP,
                            //pepFormularyOptionsId = item.pepFormularyOptionsId,
                            // IdentificationTypeId = item.IdentificationTypeId,
                            IdentificationTypeId = GetVirtualOfficeIdentificationType(item.IdentificationTypeId.ToString()),
                            IdentificationNumber = item.IdentificationNumber,
                            NationalityCountryId = item.NationalityCountryId,
                            userId = GetCurrentUserID()
                        };

                        var ExecutionResult = oDriverManager.SetBeneficiariesByDriver(parameter);

                        if (item.isPEP.HasValue && item.isPEP.GetValueOrDefault())
                        {
                            hasPeps = true;
                            var parameterPep = new Entity.Entities.PepFormulary.Parameter
                            {
                                personsID = driverId,
                                name = item.name,
                                BeneficiaryId = ExecutionResult.EntityId,
                                //BeneficiaryId = item.IdentityId,
                                position = "-",
                                IsPepManagerCompany = false
                            };

                            var ExecutionPepResult = oDriverManager.SetPepByDriver(parameterPep);
                        }
                    }

                    //var DatosPep = oDriverManager.GetDriverBeneficiaries(driverId).ToList();
                    //DatosPep = DatosPep.Where(x => x.isPEP == true).ToList();

                    //if (DatosPep.Count > 0)
                    //{
                    //    hasPeps = true;
                    //    foreach (var item in DatosPep)
                    //    {
                    //        var parameter = new Entity.Entities.PepFormulary.Parameter
                    //        {
                    //            id = item.Id,
                    //            personsID = driverId,
                    //            name = item.name,
                    //            BeneficiaryId = item.Id,
                    //            //BeneficiaryId = item.IdentityId,
                    //            position = "-",
                    //            IsPepManagerCompany = false
                    //        };

                    //        var ExecutionResult = oDriverManager.SetPepByDriver(parameter);
                    //    }

                    //}

                    result.Value = "success";
                    result.name = "success";
                }
                catch (Exception ex)
                {
                    result.Value = "Error";
                    result.name = ex.Message;

                    var user = GetCurrentUsuario();
                    LoggerHelper.Log(CommonEnums.Categories.Error, "POS-" + (user != null ? user.UserLogin : ""), QuotationId, "Error al guardar los beneficiarios", "Mensaje: " + ex.Message, ex);
                }
            }

            return Json(new { result = result, HasPep = hasPeps }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteBeneficiaries(string driverID)
        {
            var result = new Entity.Entities.Generic();
            try
            {
                var Deleted = oDriverManager.DeleteBeneficiariesByDriver(driverID.ToInt());

                result.Value = "success";
                result.name = "success";

            }
            catch (Exception ex)
            {
                result.Value = "Error";
                result.name = ex.Message;
                var user = GetCurrentUsuario();
                LoggerHelper.Log(CommonEnums.Categories.Error, "POS-" + (user != null ? user.UserLogin : ""), QuotationId, "Error al eliminar los Beneficiarios", "Mensaje: " + ex.Message, ex);
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}