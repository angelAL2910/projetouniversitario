﻿using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Configuration;
using RESOURCE.UnderWriting.NewBussiness;
using WEB.NewBusiness.Common;
using WEB.NewBusiness.Common.Illustration.IllustrationVehicle.Models;
using System.Collections.Generic;
using System.Web.UI;

namespace WEB.NewBusiness.NewBusiness.Pages
{
    public partial class Login : BasePage
    {
        public void setValues(String URLRedirect)
        {
            //Configuracion Inicial        
            ObjServices.ContactServicesIsActive = (ConfigurationManager.AppSettings["ContactServicesIsActive"] == "true");
            var dataOffice = Usuario.AgentOffices.FirstOrDefault() ??
                             new Statetrust.Framework.Security.Bll.Usuarios.AgentOffice();

            ObjServices.CompanyId = Usuario.CurrentCompanyId <= 0 ? dataOffice.CompanyId : Usuario.CurrentCompanyId;
            Usuario.CurrentLanguageKey = "es";
            ObjServices.Corp_Id = dataOffice.CorpId;
            ObjServices.Region_Id = dataOffice.RegionId;
            ObjServices.Country_Id = dataOffice.CountryId;
            ObjServices.Domesticreg_Id = dataOffice.DomesticRegId;
            ObjServices.City_Id = dataOffice.CityId;
            ObjServices.Office_Id = dataOffice.OfficeId;
            ObjServices.State_Prov_Id = dataOffice.StateProvId;

            //End Oficina

            //Set variables roles
                        
            var UsuarioPropiedades = Usuario.rolesByUser.Select(x=>x.Rol_Name).ToList();
            ObjServices.IsAgentQuotRole = UsuarioPropiedades.Any(o => o.Contains("AgentCot"));
            ObjServices.IsSuscripcionQuotRole = UsuarioPropiedades.Any(o => o.Contains("SuscripcionCot"));
            ObjServices.IsInspectorQuotRole = UsuarioPropiedades.Any(o => o.Contains("InspectorCot")) || UsuarioPropiedades.Any(o => o.Contains("InspectorProCot"));
            ObjServices.IsSuscripcionManagerQuotRole = UsuarioPropiedades.Any(o => o.Contains("SuscripcionManagerCot"));
            ObjServices.IsDirectorQuotRole = UsuarioPropiedades.Any(o => o.Contains("DirectorCot"));
            ObjServices.IsSucripcionDirectorQuotRole = UsuarioPropiedades.Any(o => o.Contains("DirectorSuscricion"));
            ObjServices.IsAngetInspectorQuotRole = UsuarioPropiedades.Any(o => o.Contains("AgentCot")) && (UsuarioPropiedades.Any(o => o.Contains("InspectorCot")) || UsuarioPropiedades.Any(o => o.Contains("InspectorProCot")));
            ObjServices.isUserCot = UsuarioPropiedades.Any(o => o.Contains("UserCot"));
            ObjServices.IsConfirmationCallCot = UsuarioPropiedades.Any(o => o.Contains("ConfirmationCallCot"));
            ObjServices.isReclamacionesQuotRole = UsuarioPropiedades.Any(o => o.Contains("ReclamacionesCot"));
            ObjServices.IsConfirmationCallManagerCot = UsuarioPropiedades.Any(o => o.Contains("ConfirmationCallManagerCot"));
            ObjServices.IsCreditoCot = UsuarioPropiedades.Any(o => o.Contains("CreditoCot"));
            ObjServices.isDescuentocot = UsuarioPropiedades.Any(o => o.Contains("DescuentoCot"));
            ObjServices.isDescuentoCot100Porc = UsuarioPropiedades.Any(o => o.Contains("DescuentoCot100%"));            
            ObjServices.IsPreSuscribcionDescuentoCot = UsuarioPropiedades.Any(o => o.Contains("PreSuscribcionDescuentoCot"));
            ObjServices.IsPreSuscribcionRecargoCot = UsuarioPropiedades.Any(o => o.Contains("PreSuscribcionRecargoCot"));
            ObjServices.IsAgentServiceQuoRole = UsuarioPropiedades.Any(o => o.Contains("AgentServiceCot"));
            ObjServices.ViewCreditCardInformation = UsuarioPropiedades.Any(o => o.Contains("ViewCreditCardInformation"));
            ObjServices.CanViewContactInformation = UsuarioPropiedades.Any(o => o.Contains("CanViewContactInformation"));
            ObjServices.CanDomitiliationPayment = UsuarioPropiedades.Any(o => o.Contains("CanDomitiliationPayment"));             

            ObjServices.UsuarioPropiedades = UsuarioPropiedades;
            //ObjServices.Bandeja = UsuarioPropiedades.Any(o => o.Contains("PropiedadCot")) ? "Propiedad" : "Auto"; Esta variable fue comentada ya que no se va a controlar desde aqui,
            // sino que se llenará el valor en la bandeja por Querystring
            
            //Rabel Obispo
            if (Usuario.UserType != Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.User)
                ObjServices.AgentChain = ObjServices.oPolicyManager.GetAgentChain(Usuario.AgentId);
            else
                ObjServices.AgentChain = null;

            #region GetAllTabsByRol
            ObjServices.TabsByRol = ObjServices.oPolicyManager.GetAllTabsByRole(Usuario.UserID);
            #endregion

            #region Can Print Invoice
            var dataCanPrintInvoice = ObjServices.GettingDropData(Utility.DropDownType.PrintInvoices);
            if (dataCanPrintInvoice != null)
            {
                var roles = dataCanPrintInvoice.Select(r => r.ElementDesc);
                ObjServices.CanPrintInvoice = (roles != null && roles.Count() > 0) ? UsuarioPropiedades.Intersect(roles).Any()
                                                                                   : false;
            }
            #endregion

            #region Can Validate Facultative Doc
            var datarolesValidateFacultativeDoc = ObjServices.GettingDropData(Utility.DropDownType.FacultativeDocValidation);
            if (datarolesValidateFacultativeDoc != null)
            {
                var roles = datarolesValidateFacultativeDoc.Select(r => r.ElementDesc);
                ObjServices.IsValidateFacultativeCot = (roles != null && roles.Count() > 0) ? UsuarioPropiedades.Intersect(roles).Any()
                                                                                            : false;
            }
            #endregion

            #region Can View Prime And Rate
            var datarolesViewPrimeAndRate = ObjServices.GettingDropData(Utility.DropDownType.ViewPrimeAndRate);
            if (datarolesViewPrimeAndRate != null)
            {
                var roles = datarolesViewPrimeAndRate.Select(r => r.ElementDesc);
                ObjServices.IsViewPrimeAndRateCot = (roles != null && roles.Count() > 0) ? UsuarioPropiedades.Intersect(roles).Any()
                                                                                         : false;
            }
            #endregion

            #region Can Validate Black List
            var datarolesValidateBlackList = ObjServices.GettingDropData(Utility.DropDownType.ValidateBlackList);
            if (datarolesValidateBlackList != null)
            {
                var roles = datarolesValidateBlackList.Select(r => r.ElementDesc);
                ObjServices.IsValidateBlackListCot = (roles != null && roles.Count() > 0) ? UsuarioPropiedades.Intersect(roles).Any()
                                                                                         : false;
            }
            #endregion

            #region Can View Facultative
            var datarolesFacultative = ObjServices.GettingDropData(Utility.DropDownType.Facultative);
            if (datarolesFacultative != null)
            {
                var roles = datarolesFacultative.Select(r => r.ElementDesc);
                ObjServices.IsFacultativeCot = (roles != null && roles.Count() > 0) ? UsuarioPropiedades.Intersect(roles).Any()
                                                                                    : false;
            }
            #endregion

            #region CanViewPersonalInfo
            var dataroles = ObjServices.GettingDropData(Utility.DropDownType.Roles);
            if (dataroles != null)
            {
                var roles = dataroles.Select(r => r.Namekey);
                ObjServices.CanViewPersonalInfo = (roles != null && roles.Count() > 0) ? UsuarioPropiedades.Intersect(roles).Any()
                                                                                       : false;
            }
            #endregion

            #region CanViewStatistics
            dataroles = null;
            dataroles = ObjServices.GettingDropData(Utility.DropDownType.Roles, NameKey: "Statistics");
            if (dataroles != null)
            {
                var roles = dataroles.Select(r => r.Namekey);
                ObjServices.CanViewStatistics = (roles != null && roles.Count() > 0) ? UsuarioPropiedades.Intersect(roles).Any()
                                                                                     : false;
            }
            #endregion

            try
            {
                ObjServices.Country = (Utility.Country)Enum.Parse(typeof(Utility.Country), ConfigurationManager.AppSettings["CountryKeyMode"]);
            }
            catch (Exception)
            {
                ObjServices.Country = Utility.Country.RepublicaDominicana;
            }

            #region Contador de Sesiones en Inspecciones

            ObjServices.dataConfig = ObjServices.GettingDropData(Utility.DropDownType.ProjectConfigurationValue,
                                                                  corpId: ObjServices.Corp_Id,
                                                                  pProjectId: int.Parse(System.Configuration.ConfigurationManager.AppSettings["ProjectIdNewBusiness"])).ToList();
            if (ObjServices.dataConfig.Any())
            {
                //Frecuencia de Refrescado de los contadores
                var dataCounterFreq = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "CounterFreQRefresh");
                ObjServices.CounterFreQRefresh = dataCounterFreq != null ? dataCounterFreq.ConfigurationValue.ToInt() : 15;

                #region Auto
                var DefaultImageCar = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "VehicleDefaultImageCar");
                ObjServices.DefaultImageCar = DefaultImageCar != null ? DefaultImageCar.ConfigurationValue : string.Empty;

                var AutoSectionInformacionesGeneralesCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "VehicleInformacionesGenerales");
                ObjServices.AutoSectionInformacionesGeneralesCount = AutoSectionInformacionesGeneralesCount != null ? AutoSectionInformacionesGeneralesCount.ConfigurationValue.ToInt() : 0;

                var AutoSectionVerificacionDatosGeneralesCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "VehicleVerificacionDatos");
                ObjServices.AutoSectionVerificacionDatosGeneralesCount = AutoSectionVerificacionDatosGeneralesCount != null ? AutoSectionVerificacionDatosGeneralesCount.ConfigurationValue.ToInt() : 0;

                var AutoSectionCombustibleCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "VehicleCombustible");
                ObjServices.AutoSectionCombustibleCount = AutoSectionCombustibleCount != null ? AutoSectionCombustibleCount.ConfigurationValue.ToInt() : 0;

                var AutoSectionFuncionamientoCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "VehicleFuncionamiento");
                ObjServices.AutoSectionFuncionamientoCount = AutoSectionFuncionamientoCount != null ? AutoSectionFuncionamientoCount.ConfigurationValue.ToInt() : 0;

                var AutoSectionPartesFisicasCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "VehiclePartesFisicas");
                ObjServices.AutoSectionPartesFisicasCount = AutoSectionPartesFisicasCount != null ? AutoSectionPartesFisicasCount.ConfigurationValue.ToInt() : 0;

                var AutoSectionAccesoriosTapiceriaCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "VehicleAccesoriosTapiceria");
                ObjServices.AutoSectionAccesoriosTapiceriaCount = AutoSectionAccesoriosTapiceriaCount != null ? AutoSectionAccesoriosTapiceriaCount.ConfigurationValue.ToInt() : 0;

                var AutoSectionSeguridadComplementosCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "VehicleSeguridadComplementos");
                ObjServices.AutoSectionSeguridadComplementosCount = AutoSectionSeguridadComplementosCount != null ? AutoSectionSeguridadComplementosCount.ConfigurationValue.ToInt() : 0;

                var AutoSectionOtrasInformacionesCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "VehicleOtrasInformaciones");
                ObjServices.AutoSectionOtrasInformacionesCount = AutoSectionOtrasInformacionesCount != null ? AutoSectionOtrasInformacionesCount.ConfigurationValue.ToInt() : 0;

                var AutoSectionPhotosCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "VehiclePhotos");
                ObjServices.AutoSectionPhotosCount = AutoSectionPhotosCount != null ? AutoSectionPhotosCount.ConfigurationValue.ToInt() : 0;
                #endregion

                #region Lineas Aliadas
                #region Property
                var PropertySectionDatosGeneralesCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyDatosGenerales");
                ObjServices.PropertySectionDatosGeneralesCount = PropertySectionDatosGeneralesCount != null ? PropertySectionDatosGeneralesCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionSumasAseguradasCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertySumasAseguradas");
                ObjServices.PropertySectionSumasAseguradasCount = PropertySectionSumasAseguradasCount != null ? PropertySectionSumasAseguradasCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionDescripcionCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyDescripcion");
                ObjServices.PropertySectionDescripcionCount = PropertySectionDescripcionCount != null ? PropertySectionDescripcionCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionHistorialPerdidasCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyHistorialPerdidas");
                ObjServices.PropertySectionHistorialPerdidasCount = PropertySectionHistorialPerdidasCount != null ? PropertySectionHistorialPerdidasCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionSiniestralidadZonaCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertySiniestralidadZona");
                ObjServices.PropertySectionSiniestralidadZonaCount = PropertySectionSiniestralidadZonaCount != null ? PropertySectionSiniestralidadZonaCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionColindanciasCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyColindancias");
                ObjServices.PropertySectionColindanciasCount = PropertySectionColindanciasCount != null ? PropertySectionColindanciasCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionLocalizacionRiesgoDOCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyLocalizacionRiesgoDO");
                int RD = PropertySectionLocalizacionRiesgoDOCount != null ? PropertySectionLocalizacionRiesgoDOCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionLocalizacionRiesgoSVCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyLocalizacionRiesgoSV");
                int SV = PropertySectionLocalizacionRiesgoSVCount != null ? PropertySectionLocalizacionRiesgoSVCount.ConfigurationValue.ToInt() : 0;

                ObjServices.PropertySectionLocalizacionRiesgoCount = ObjServices.Country == Utility.Country.RepublicaDominicana ? RD : SV;

                var PropertySectionDescripcionProcesosCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyDescripcionProcesos");
                ObjServices.PropertySectionDescripcionProcesosCount = PropertySectionDescripcionProcesosCount != null ? PropertySectionDescripcionProcesosCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionDescripcionPeligrosCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyDescripcionPeligros");
                ObjServices.PropertySectionDescripcionPeligrosCount = PropertySectionDescripcionPeligrosCount != null ? PropertySectionDescripcionPeligrosCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionPrevencionProteccionCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyPrevencionProteccion");
                ObjServices.PropertySectionPrevencionProteccionCount = PropertySectionPrevencionProteccionCount != null ? PropertySectionPrevencionProteccionCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionEstimacionPerdidasCoberturaIncendioCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyEstimacionPerdida");
                ObjServices.PropertySectionEstimacionPerdidasCoberturaIncendioCount = PropertySectionEstimacionPerdidasCoberturaIncendioCount != null ? PropertySectionEstimacionPerdidasCoberturaIncendioCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionExposicionRiesgosCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyExposicionRiesgos");
                ObjServices.PropertySectionExposicionRiesgosCount = PropertySectionExposicionRiesgosCount != null ? PropertySectionExposicionRiesgosCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionCategoriaRiesgoCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyCategoriaRiesgo");
                ObjServices.PropertySectionCategoriaRiesgoCount = PropertySectionCategoriaRiesgoCount != null ? PropertySectionCategoriaRiesgoCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionOpinionRiesgoCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyOpinionRiesgo");
                ObjServices.PropertySectionOpinionRiesgoCount = PropertySectionOpinionRiesgoCount != null ? PropertySectionOpinionRiesgoCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionRecomendacionesTecnicasCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyRecomendacionTecnica");
                ObjServices.PropertySectionRecomendacionesTecnicasCount = PropertySectionRecomendacionesTecnicasCount != null ? PropertySectionRecomendacionesTecnicasCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionRecomendacionesHechasEnviadasAseguradoCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyRecomendacionesHechas");
                ObjServices.PropertySectionRecomendacionesHechasEnviadasAseguradoCount = PropertySectionRecomendacionesHechasEnviadasAseguradoCount != null ? PropertySectionRecomendacionesHechasEnviadasAseguradoCount.ConfigurationValue.ToInt() : 0;

                var PropertySectionFotografiasCount = ObjServices.dataConfig.FirstOrDefault(x => x.Namekey == "PropertyFotografias");
                ObjServices.PropertySectionFotografiasCount = PropertySectionFotografiasCount != null ? PropertySectionFotografiasCount.ConfigurationValue.ToInt() : 0;
                #endregion
                #endregion              
            }

            #endregion

            ObjServices.RefreshInbox = ConfigurationManager.AppSettings["RefreshInbox"].ToBoolean();
            ObjServices.RefreshFrecuency = ConfigurationManager.AppSettings["RefreshFrecuency"].ToInt();

            ObjServices.UserType = Usuario.UserType;

            if (ObjServices.Corp_Id <= 0)
            {
                var dataCompany = ObjServices.GettingDropData(Utility.DropDownType.Company);
                if (dataCompany != null)
                    ObjServices.Corp_Id = dataCompany.FirstOrDefault().CorpId.Value;
            }


            #region Transunion

            ObjServices.DefaulltPassword = ConfigurationManager.AppSettings["TransunionDefaultPassword"];
            ObjServices.user = ConfigurationManager.AppSettings["TransunionUserName"];
            ObjServices.pass = ConfigurationManager.AppSettings["TransunionPass"];

            #endregion

            #region Equifax
            ObjServices.DefaulltPasswordEF = ConfigurationManager.AppSettings["EquifaxDefaultPassword"];
            ObjServices.userEF = ConfigurationManager.AppSettings["EquifaxUserName"];
            ObjServices.passEF = ConfigurationManager.AppSettings["EquifaxPass"];
            #endregion

            ObjServices.ProjectId = int.Parse(ConfigurationManager.AppSettings["ProjectIdNewBusiness"]);

            ObjServices.Agent_LoginId = (ObjServices.UserType == Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.User) ? Usuario.UserID
                                                                                                                               : Usuario.AgentId;
            if (
                 ObjServices.UserType == Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.User && ObjServices.IsSuscripcionQuotRole ||
                 ObjServices.UserType == Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.User && ObjServices.IsInspectorQuotRole ||
                 ObjServices.UserType == Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.User && ObjServices.IsSuscripcionManagerQuotRole
                )
                ObjServices.Agent_LoginId = Usuario.AgentId;

            ObjServices.Agent_Id = ObjServices.Agent_LoginId;

            ObjServices.isUser = ObjServices.UserType == Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.User;

            ObjIllustrationServices.IllusUserID = ObjServices.UserID = Usuario.UserID;

            ObjServices.TabRedirect = string.Empty;
            ObjServices.isNewCase = true;
            ObjServices.UserName = Usuario.UserLogin;
            ObjServices.UserFullName = Usuario.FullName;

            var company = ObjIllustrationServices.oIllusDataManager.GetCompany(ObjServices.CompanyId);

            if (company != null)
                ObjIllustrationServices.IllusCompanyId = company.BrandName;

            ObjServices.Language = Usuario.CurrentLanguageKey.ToLower() == "en" ? Utility.Language.en
                                                                                : Utility.Language.es;

            ObjServices.IsDataReviewMode = false;
            ObjServices.IsReadOnly = false;
            ObjServices.isOwnerContact = false;
            ObjServices.CurrentProject = Utility.Project.NewBusiness;

            var jsonAdditionalInfo = Request.QueryString["additionalinfo"];

            if (!String.IsNullOrEmpty(jsonAdditionalInfo))
            {
                var additionalInfo = GetAdditionalInfo(jsonAdditionalInfo);

                ObjServices.Language = Usuario.CurrentLanguageKey.ToLower() == "en" ? Utility.Language.en
                                                                                    : Utility.Language.es;

                ObjServices.CompanyId = Usuario.CurrentCompanyId;

                var TabRedirect = string.Empty;

                if (additionalInfo.RedirectTab == "illustration")
                    TabRedirect = "lnkIllustrations";
                else if (additionalInfo.RedirectUrl.Contains("IllustrationsVehicle"))
                {
                    var actionIllustration = additionalInfo.Action.FromJsonToObject<ActionIllustrationPayment>();
                    ObjServices.Corp_Id = actionIllustration.Corp_Id;
                    ObjServices.Region_Id = actionIllustration.Region_Id;
                    ObjServices.Country_Id = actionIllustration.Country_Id;
                    ObjServices.Domesticreg_Id = actionIllustration.Domesticreg_Id;
                    ObjServices.City_Id = actionIllustration.City_Id;
                    ObjServices.Office_Id = actionIllustration.Office_Id;
                    ObjServices.State_Prov_Id = actionIllustration.State_Prov_Id;
                    ObjServices.Case_Seq_No = actionIllustration.Case_Seq_No;
                    ObjServices.Hist_Seq_No = actionIllustration.Hist_Seq_No;
                    ObjServices.ContactEntityID = actionIllustration.ContactEntityID;
                    ObjServices.ProductLine = (Utility.ProductLine)Utility.getEnumTypeFromValue(typeof(Utility.ProductLine), actionIllustration.ProductLine);
                }

                ObjServices.TabRedirect = TabRedirect;

                Response.Redirect(additionalInfo.RedirectUrl, true);
            }
            else
                Response.Redirect(URLRedirect, true);
        }

        protected override void Page_PreInit(object sender, EventArgs e)
        {
            isLoginPage = true;
            SecurityMenuBar = false;
            int UserID = 0;

            try
            {
                if (!IsPostBack)
                {
                     var QueryString = Request.QueryString["UserName"];

                    if (ConfigurationManager.AppSettings["ApplySecurity"].ToString(CultureInfo.InvariantCulture) == "false" || !string.IsNullOrEmpty(QueryString))
                    {
                        #region Usuarios
                        /*
                        +---------------+--------------+---------------------+------------+
                        | USUARIOS      | CLAVE        | ROLES               |   OFICINA  |
                        +---------------+--------------+---------------------+------------+
                        | EDTAVERAS     | outiiz/77lQ= | DIRECTOR	         | PLAZA URIS |
                        | YOSANTOS      | outiiz/77lQ= | AGENTES	         | PLAZA URIS |
                        | MATRINIDAD    | outiiz/77lQ= | AGENTES	         | PLAZA URIS |
                        | ALMARMOLEJOS  | outiiz/77lQ= | AGENTES Y INSPECTOR | PLAZA URIS |
                        | VIFRAGOSO     | outiiz/77lQ= | INSPECTOR	         | PLAZA URIS |
                        | FAJESUS       | outiiz/77lQ= | INSPECTOR	         | PLAZA URIS |
                        | JOGUZMAN      | outiiz/77lQ= | SUSCRICION	         | PLAZA URIS |
                        | RALOPEZ       | outiiz/77lQ= | SUSCRICION	         | PLAZA URIS |
                        | STPEREZ       | outiiz/77lQ= | SUSCRICION	         | PLAZA URIS |
                        | JUJIMENEZ     | outiiz/77lQ= | SUSCRICIONMANAGER	 | PLAZA URIS |
                        | PARIXAVI      | outiiz/77lQ= | DIRECTORSUSCRIPCION | PLAZA URIS |
                        +---------------+--------------+---------------------+------------+
                         
                         +------------------------------------------------------------------+
                         | A G E N T E S                                                    |
                         +--------+------------+----------------+----------+----------------+
                         | UserId | Login	   | UserName	    | Agent_Id | AgentFullName  |
                         +--------+------------+----------------+----------+----------------+
                         | 166    | elpaniagua | Elena Paniagua | 24       | Elena Paniagua |
                         +--------+------------+----------------+----------+----------------+
                         | 175	  | DIAZFRAI   | Frailin Diaz	| 43	   | Frailin A Diaz |
                         +--------+------------+----------------+----------+----------------+
 	 	 	 	 
                         +-------------------------------------------------------------------------+
                         | S U S C R I P T O R E S                                                 |
                         +--------+-------------+-------------------+----------+-------------------+
                         | UserId | Login	    | UserName	        | Agent_Id | AgentFullName     |
                         +--------+-------------+-------------------+----------+-------------------+
                         | 1454   | ybeltre     | Yuly Beltre       | 38377    | Yuly Beltre       |
                         +--------+-------------+-------------------+----------+-------------------+
                         | 1462	  | hufermin    | Humberto Fermin   | 38368    | Humberto Fermin   |
                         +--------+-------------+-------------------+----------+-------------------+
                         | 1463	  | berodriguez | Betania Rodriguez | 38364    | Betania Rodriguez |
                         +--------+-------------+-------------------+----------+-------------------+
                         | 1464	  | nigarcia    | Nicolas Garcia    | 38373    | Nicolas Garcia    |
                         +--------+-------------+-------------------+----------+-------------------+
                         | 1465	  | rocontreras | Rosanna Contreras | 38375    | Rosanna Contreras |
                         +--------+-------------+-------------------+----------+-------------------+
 	 	 	 	 
                         +--------------------------------------------------------------------------+
                         | I N S P E C T O R E S                                                    |
                         +--------+--------------+-------------------+----------+-------------------+
                         | UserId | Login	     | UserName	         | Agent_Id | AgentFullName     |
                         +--------+--------------+-------------------+----------+-------------------+
                         | 1467   | anlogan	     | Anthony Logan	 | 38363    | Anthony Logan     |
                         +--------+--------------+-------------------+----------+-------------------+
                         | 1468   | edbelen	     | Eduardo Belen	 | 38366	| Eduardo Belen     |
                         +--------+--------------+-------------------+----------+-------------------+
                         | 1469   | wipolanco	 | Wilver Polanco	 | 38376	| Wilver Polanco    |
                         +--------+--------------+-------------------+----------+-------------------+
                         | 1470   | jebruno	     | Jesus Bruno	     | 38369	| Jesus Bruno       |
                         +--------+--------------+-------------------+----------+-------------------+
                         | 1471   | keflorentino | Kelvin Florentino | 38370	| Kelvin Florentino |
                         +--------+--------------+-------------------+----------+-------------------+
                         | 1472   | rabaez	     | Randy Baez	     | 38374	| Randy Baez        |
                         +--------+--------------+-------------------+----------+-------------------+
                         | 1473   | hevelez	     | Hector Vélez	     | 38367	| Hector Vélez      |
                         +--------+--------------+-------------------+----------+-------------------+
                         | 1474   | brrodriguez  | Braulio Rodriguez | 38365    | Braulio Rodriguez |
                         +--------+--------------+-------------------+----------+-------------------+
                     
                         +---------------------------------------------------------------------+
                         | S U S C R I P T I O N   M A N A G E R                               |
                         +--------+-------------+-----------------+----------+-----------------+
                         | UserId | Login	    | UserName	      | Agent_Id | AgentFullName   |
                         +--------+-------------+-----------------+----------+-----------------+
                         | 1479	  | anrodriguez | Andy Rodriguez  | 38362	 | Andy Rodriguez  |
                         +--------+-------------+-----------------+----------+-----------------+
                         | 1480	  | lucarmen    | Luis del Carmen | 38372	 | Luis Del Carmen |
                         +--------+-------------+-----------------+----------+-----------------+
                     
                         +---------------------------------------------------------------------------------------+
                         | D I R E C T O R    D E    S U S C R I P C I O N                                       |
                         +--------+-------------+--------------------------+----------+--------------------------+
                         | UserId | Login	    | UserName	               | Agent_Id | AgentFullName            |
                         +--------+-------------+--------------------------+----------+--------------------------+
                         | 1482	  | lerosario   | Leonel A. Rosario Garcia | 38371    | Leonel A. Rosario Garcia |
                         +--------+-------------+--------------------------+----------+--------------------------+
 	 	 	 	 
                         +----------------------------------------------------------------------+
                         | D I R E C T O R                                                      |
                         +--------+----------+-------------------+----------+-------------------+
                         | UserId | Login    | UserName          | Agent_Id | AgentFullName     |
                         +--------+----------+-------------------+----------+-------------------+
                         | 163    | CAVAANGE | Angelo Cavagliano | 15       | Angelo Cavagliano |
                         +--------+----------+-------------------+----------+-------------------+
                      
                         CLAVES: 123 => outiiz/77lQ=
                        */
                        #endregion

                        //var LoginTest = "PANIELEN";
                        var LoginTest = "Clebron";
                        //var LoginTest = "clebron";
                        //var Password = "UjAsEkU/BxXlACKm4mdWkg=="; prod
                        var Password = "outiiz/77lQ=";
                        //var LoginTest = "EVRICHARDSON";
                        //var Password = "BGWSzPIeBHcHrX1tquZZxA==";
                        

                        if (!string.IsNullOrEmpty(QueryString))
                        {
                            var DataUser = Request.QueryString["UserName"].Split('|');
                            LoginTest = DataUser[0];
                            Password = DataUser[1];
                        }

                        if (Login(LoginTest, Password, ref UserID))
                            setValues(ConfigurationManager.AppSettings["LoginRedirectNewBusiness"]);
                        else
                        {
                            var MessageError = string.Format("El usuario {0} no pudo autenticarse en el sistema", LoginTest);
                            Response.Redirect(string.Format("~/NewBusiness/Pages/Error.aspx?msg={0}", MessageError));
                        }
                    }
                    else
                    {
                        if (Request.QueryString.Count > 0)
                        {
                            int AplicationID = 0;
                            if (Login(Request.QueryString[0], ref UserID, ref  AplicationID))
                            {
                                var URL = GetDefaultPageByUserID(UserID, AplicationID);
                                if (URL.Trim() != "")
                                    setValues(URL);
                            }
                            else
                                Response.Redirect(WebConfigurationManager.AppSettings["SecurityLogin"]);
                        }
                        else
                            Response.Redirect(WebConfigurationManager.AppSettings["SecurityLogin"]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MessageBox(string.Format("ExceptionMessage {0} TraceStack {1}", ex.Message, ex.StackTrace).MyRemoveInvalidCharacters());
            }
        }
    }
}