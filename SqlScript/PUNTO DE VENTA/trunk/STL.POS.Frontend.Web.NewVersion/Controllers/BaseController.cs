﻿using Statetrust.Framework.Security.Bll;
using Statetrust.Framework.Web.Mvc.WebParts.Controllers;
using STL.POS.AchWsProxy;
using STL.POS.AchWsProxy.AchPayments;
using STL.POS.AgentWSProxy;
using STL.POS.Frontend.Web.NewVersion.CustomCode;
using STL.POS.Logic;
using STL.POS.PlexysProxy;
using STL.POS.PlexysProxy.PlexisService;
using STL.POS.WsProxy;
using STL.POS.WsProxy.SysflexService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using VoProxy = STL.POS.VirtualOfficeProxy;
using Entity.Entities;


namespace STL.POS.Frontend.Web.NewVersion.Controllers
{
    public class BaseController : STFMainController
    {
        public CultureInfo culturelanguaje = CultureInfo.CreateSpecificCulture("es-DO");
        public SessionList datos;
        private string key = "SessionData";
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var languageId = "es-DO";

            /*if (Request.RequestContext.HttpContext.Session != null && Request.RequestContext.HttpContext.Session[SESSION_LANGUAGE_ID] != null)
            {
                languageId = Request.RequestContext.HttpContext.Session[SESSION_LANGUAGE_ID].ToString();
            }*/

            var currentCulture = CultureInfo.CreateSpecificCulture(languageId);
            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentCulture;

            //ViewBag.LanguageList = GetLanguageList();
            ViewBag.CurrentLanguage = languageId;

            var usuario = GetCurrentUsuario();

            ViewBag.UserOrigin = CommonEnums.UserOrigins.VO;
            ViewBag.UserType = "";
            ViewBag.isAgentUser = false;

            ViewBag.userCanApplySurCharge = "N";

            ViewBag.CanSeeListUsers = CanSeeListUsers;
            if (usuario != null)
            {
                ViewBag.UserType = Usuario.UserType.ToString();
                ViewBag.isAgentUser = (Usuario.UserType == Usuarios.UserTypeEnum.Agent || Usuario.UserType == Usuarios.UserTypeEnum.Assistant) ? true : false;
                ViewBag.userCanApplySurCharge = usuario.CanApplySurcharge ? "S" : "N";

                ViewBag.CanSeeListUsers = CanSeeListUsers;
                ViewBag.ListUsers = ListUsers;
            }


            return base.BeginExecuteCore(callback, state);
        }

        public BaseController()
        {
            if (System.Web.HttpContext.Current.Session == null)
            {
                System.Web.HttpContext.Current.Session.Add(key, new SessionList(key));
                (System.Web.HttpContext.Current.Session[key] as SessionList).sessionObject = new SessionObject();
            }
            else
                if (System.Web.HttpContext.Current.Session[key] == null)
                {
                    System.Web.HttpContext.Current.Session.Add(key, new SessionList(key));
                    (System.Web.HttpContext.Current.Session[key] as SessionList).sessionObject = new SessionObject();
                }

            datos = (System.Web.HttpContext.Current.Session[key] as SessionList);
        }

        protected string UserIdentityName
        {
            get
            {
                return (GetCurrentUsuario() != null ? "POS-" + GetCurrentUsuario().UserLogin : "POS-Venta Directa");
            }
        }

        private string _urlSysflexServices;

        #region Managers

        protected PersonManager oPersonManagerManager
        {
            get
            {
                return new Logic.PersonManager();
            }
        }
        protected QuotationManager oQuotationManager
        {
            get
            {
                return new Logic.QuotationManager();
            }
        }
        protected DropDownManager oDropDownManager
        {
            get
            {
                return new Logic.DropDownManager();
            }
        }
        protected CoverageManager oCoverageManager
        {
            get { return new CoverageManager(); }
        }
        protected DriverManager oDriverManager
        {
            get
            {
                return new DriverManager();
            }
        }
        protected IdentificationFinalBeneficiaryManager oIdentificationFinalBeneficiaryManager
        {
            get { return new IdentificationFinalBeneficiaryManager(); }
        }
        protected PepFormularyManager oPepFormularyManager
        {
            get { return new PepFormularyManager(); }
        }
        protected ProductLimitsManager oProductLimitsManager
        {
            get { return new ProductLimitsManager(); }
        }
        protected ServicesTypesRepositoryManager oServicesTypesRepositoryManager
        {
            get { return new ServicesTypesRepositoryManager(); }
        }
        protected SocialReasonManager oSocialReasonManager
        {
            get { return new SocialReasonManager(); }
        }
        protected VehicleProductManager oVehicleProductManager
        {
            get { return new VehicleProductManager(); }
        }
        protected UserManager oUserManager
        {
            get { return new UserManager(); }
        }
        protected ProductTypeBrochureManager oBrochureManager
        {
            get { return new ProductTypeBrochureManager(); }
        }
        protected DocumentRequiredManager oDocumentRequiredManager
        {
            get { return new DocumentRequiredManager(); }
        }

        #endregion

        #region Services

        protected AchPaymentProxy oAchPaymentProxy
        {
            get
            {
                return
                    new AchPaymentProxy(new STL.POS.AchWsProxy.AchPayments.GPPaymentsClient());
            }
        }
        protected AgentProxy oAgentWSProxy
        {
            get
            {
                return
                    new AgentProxy();
            }
        }
        protected ProxyClient oPlexysProxy
        {
            get
            {
                return
                    new ProxyClient(null);
            }
        }
        protected VoProxy.VirtualOfficeProxy oVirtualOfficeProxy
        {
            get
            {
                return
                    new VoProxy.VirtualOfficeProxy();
            }
        }
        protected CoreProxy oCoreProxy
        {
            get
            {
                var wsClient = new SysFlexServiceClient();
                //wsClient.Endpoint.Address.Uri. = _urlSysflexServices; 
                _urlSysflexServices = IsAQuotation ? System.Web.Configuration.WebConfigurationManager.AppSettings["SysFlexServiceQuoteUrl"].ToString(CultureInfo.InvariantCulture) : null;

                if (!string.IsNullOrEmpty(_urlSysflexServices))
                {
                    wsClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(_urlSysflexServices);
                }

                return
                    new CoreProxy(wsClient);
            }
        }
        protected THProxy.THProxy oThunderheadProxy
        {
            get
            {
                return new THProxy.THProxy();
            }
        }
        protected QueuesProxy oQueuesProxy
        {
            get
            {
                return new QueuesProxy();
            }
        }

        #endregion

        #region Sessions

        public int? SecuenciaVehicleInclusion
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.SecuenciaVehicleInclusion; }
            set
            {
                datos.sessionObject.SecuenciaVehicleInclusion = value;
                datos.Save();
            }
        }

        public bool onlyLoggedUsers
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.onlyLoggedUsers; }
            set
            {
                datos.sessionObject.onlyLoggedUsers = value;
                datos.Save();
            }
        }

        public bool IsShowPolicy
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.IsShowPolicy; }
            set
            {
                datos.sessionObject.IsShowPolicy = value;
                datos.Save();
            }
        }

        public int QuotationId
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.QuotationId; }
            set
            {
                datos.sessionObject.QuotationId = value;
                datos.Save();
            }
        }

        public bool CanDoInclusion
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.CanDoInclusion; }
            set
            {
                datos.sessionObject.CanDoInclusion = value;
                datos.Save();
            }
        }

        public bool CanDoExclusion
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.CanDoExclusion; }
            set
            {
                datos.sessionObject.CanDoExclusion = value;
                datos.Save();
            }
        }

        public bool CanDoCambios
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.CanDoCambios; }
            set
            {
                datos.sessionObject.CanDoCambios = value;
                datos.Save();
            }
        }

        public bool CanDoNuevaPropuesta
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.CanDoNuevaPropuesta; }
            set
            {
                datos.sessionObject.CanDoNuevaPropuesta = value;
                datos.Save();
            }
        }        

        public CustomCode.CommonEnums.RequestType RequestType
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.RequestType; }
            set
            {
                datos.sessionObject.RequestType = value;
                datos.Save();
            }
        }

        public String QuotationNumber
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.QuotationNumber; }
            set
            {
                datos.sessionObject.QuotationNumber = value;
                datos.Save();
            }
        }

        public String CoreQuotationNumber
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.CoreQuotationNumber; }
            set
            {
                datos.sessionObject.CoreQuotationNumber = value;
                datos.Save();
            }
        }

        public List<AgentTreeInfoNew> AgentTreeInfoNew
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.AgentTreeInfoNew; }
            set
            {
                datos.sessionObject.AgentTreeInfoNew = value;
                datos.Save();
            }
        }

        public Tuple<QuotationViewModel.Vehicles, QuotationViewModel> CurrentDataQuotation
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.CurrentDataQuotation; }
            set
            {
                datos.sessionObject.CurrentDataQuotation = value;
                datos.Save();
            }
        }

        public IEnumerable<SelectListItem> Colors
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.Colors; }
            set
            {
                datos.sessionObject.Colors = value;
                datos.Save();
            }
        }

        public IEnumerable<SelectListItem> Drivers
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.Drivers; }
            set
            {
                datos.sessionObject.Drivers = value;
                datos.Save();
            }
        }

        public IEnumerable<SelectListItem> dataPaymentFreq
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.dataPaymentFreq; }
            set
            {
                datos.sessionObject.dataPaymentFreq = value;
                datos.Save();
            }
        }

        public bool isNotLawProduct
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.isNotLawProduct; }
            set
            {
                datos.sessionObject.isNotLawProduct = value;
                datos.Save();
            }
        }

        public int currentCountry
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.currentCountry; }
            set
            {
                datos.sessionObject.currentCountry = value;
                datos.Save();
            }
        }

        public bool isFinanced
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.isFinanced; }
            set
            {
                datos.sessionObject.isFinanced = value;
                datos.Save();
            }
        }

        public int VehicleNumber
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.VehicleNumber; }
            set
            {
                datos.sessionObject.VehicleNumber = value;
                datos.Save();
            }
        }

        public int TotalVehiclesCompleted
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.TotalVehiclesCompleted; }
            set
            {
                datos.sessionObject.TotalVehiclesCompleted = value;
                datos.Save();
            }
        }

        public bool CanSeeListUsers
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.CanSeeListUsers; }
            set
            {
                datos.sessionObject.CanSeeListUsers = value;
                datos.Save();
            }
        }

        public SelectList ListUsers
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.ListUsers; }
            set
            {
                datos.sessionObject.ListUsers = value;
                datos.Save();
            }
        }

        public string _actionData
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject._actionData; }
            set
            {
                datos.sessionObject._actionData = value;
                datos.Save();
            }
        }


        //public string _actionData
        //{
        //    get
        //    {
        //        var val = Session["ActionModel"];
        //        if (val == null)
        //        {
        //            Session.Remove("ActionModel");
        //            PolicySendByVO = "";
        //            return "";
        //        }
        //        var actionModel = val.ToString().ToObject<Utility.ActionModel>();
        //        Session.Remove("ActionModel");
        //        //if (actionModel.ActionType == ActionTypes.Payments)
        //        if (actionModel != null)
        //        {
        //            return actionModel.ActionJson;
        //        }
        //        else
        //        {
        //            PolicySendByVO = "";
        //            return "";
        //        }
        //    }
        //}

        public string PolicySendByVO
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.PolicySendByVO; }
            set
            {
                datos.sessionObject.PolicySendByVO = value;
                datos.Save();
            }
        }

        public bool IsAQuotation
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.IsAQuotation; }
            set
            {
                datos.sessionObject.IsAQuotation = value;
                datos.Save();
            }
        }

        public string RiskLevel
        {
            get { return (System.Web.HttpContext.Current.Session[key] as SessionList).Stored.sessionObject.RiskLevel; }
            set
            {
                datos.sessionObject.RiskLevel = value;
                datos.Save();
            }
        }

        #endregion

        #region Datos Usuario de Pos_Site

        protected int CheckUser(string userId, string name, string surname, string email, int? agentId = null)
        {
            int user_id = 0;

            var usuario = GetCurrentUsuario();
            if (usuario == null) //POS User
            {
                var user = oUserManager.GetUser(null, userId);

                if (user == null) //Create user
                {
                    Entity.Entities.User.Parameter paramUser = new Entity.Entities.User.Parameter();
                    paramUser.email = email;
                    paramUser.name = name;
                    paramUser.surname = surname;
                    paramUser.username = userId;
                    paramUser.userOrigin = CommonEnums.UserOrigins.POS.ToInt();
                    paramUser.userType = CommonEnums.UserType.WebUser.ToInt();
                    paramUser.userStatus = 1;

                    var userSaved = oUserManager.SetUser(paramUser);
                    user_id = userSaved.EntityId;
                }
                else
                {
                    user_id = user.Id;
                }
                return user_id;
            }
            else // User, Agent or suscriptor
            {
                Entity.Entities.User user = null;
                Entity.Entities.User suscriptorUser = null;

                if (usuario.UserType == Usuarios.UserTypeEnum.User)
                {
                    user = oUserManager.GetUser(null, userId);
                }
                else //Agent or suscriptor
                {
                    user = oUserManager.GetUser(null, userId, CommonEnums.UserType.Agent.ToInt());
                    suscriptorUser = oUserManager.GetUser(null, usuario.UserLogin, CommonEnums.UserType.Subscriptor.ToInt());
                }

                if (user == null) //Create user
                {
                    var realAgent = getAgenteUserInfo(userId);

                    Entity.Entities.User.Parameter paramUser = new Entity.Entities.User.Parameter();
                    paramUser.email = email;
                    paramUser.name = name;
                    paramUser.surname = surname;
                    paramUser.username = userId;
                    paramUser.userType = usuario.UserType == Usuarios.UserTypeEnum.User ? CommonEnums.UserType.WebUser.ToInt() : CommonEnums.UserType.Agent.ToInt();
                    paramUser.userOrigin = CommonEnums.UserOrigins.VO.ToInt();
                    paramUser.suscriptor_Id = suscriptorUser != null ? suscriptorUser.Id : (int?)null;
                    paramUser.agentId = realAgent != null ? realAgent.AgentId : agentId;
                    paramUser.userStatus = 1;

                    var userSaved = oUserManager.SetUser(paramUser);
                    user_id = userSaved.EntityId;
                }
                else
                {
                    Entity.Entities.User.Parameter paramUser = new Entity.Entities.User.Parameter();
                    paramUser.id = user.Id;

                    if (user.Suscriptor_Id == 0)
                    {
                        paramUser.suscriptor_Id = suscriptorUser.Id;
                    }
                    if (user.AgentId == null || user.AgentId == 0)
                    {
                        var realAgent = getAgenteUserInfo(userId);

                        paramUser.agentId = realAgent != null ? realAgent.AgentId : agentId;
                    }

                    var userSaved = oUserManager.SetUser(paramUser);
                    user_id = userSaved.EntityId;
                }

                return user_id;
            }
        }

        protected int CheckQuotationHasUser(int quotationID)
        {
            Entity.Entities.User user = null;

            var quotation = oQuotationManager.GetQuotation(quotationID);

            if (quotation != null)
            {
                var quotationUser = oUserManager.GetUser(quotation.User_Id);

                if (quotationUser != null && quotationUser.AgentId.HasValue)
                {
                    //usuario que tiene la cotizacion agregada
                    string nameidAgentQuo = quotationUser.Username;
                    var agentIDAgentQuo = quotationUser.AgentId;

                    user = oUserManager.GetUser(null, nameidAgentQuo, CommonEnums.UserType.Agent.ToInt());
                    if (user == null)
                    {
                        user = oUserManager.GetUser(null, null, CommonEnums.UserType.Agent.ToInt(), agentIDAgentQuo);
                    }
                }
                else if (quotationUser != null && !quotationUser.AgentId.HasValue)
                {
                    user = oUserManager.GetUser(quotation.User_Id);
                }
            }
            return user != null ? user.Id : 0;
        }

        protected Entity.Entities.User getQuotationUser(string userName)
        {
            var user = oUserManager.GetUser(null, userName);

            return user;
        }

        protected Entity.Entities.User getQuotationUserById(int userID)
        {
            var user = oUserManager.GetUser(userID, null);

            return user;
        }

        protected Usuarios GetCurrentUsuario()
        {
            var sessionManager = Statetrust.Framework.Security.Core.Util.SessionManager.Get(Session);

            if (sessionManager == null)
                return null;
            else
                return Usuario;
        }

        public SelectList getSexes(string sex = "")
        {
            Dictionary<string, string> sexes = new Dictionary<string, string>();
            sexes.Add("Femenino", "Femenino");
            sexes.Add("Masculino", "Masculino");
            sexes.Add("Empresa", "Empresa");

            return new SelectList(sexes.Select(i => new SelectListItem { Text = i.Key.ToString(), Value = i.Value.ToString() }), "Value", "Text", sex);
        }

        public SelectList getForeingLicence(string foreinglic = "")
        {
            Dictionary<string, string> foreingLicence = new Dictionary<string, string>();
            foreingLicence.Add("Si", "Si");
            foreingLicence.Add("No", "No");

            return new SelectList(foreingLicence.Select(i => new SelectListItem { Text = i.Key.ToString(), Value = i.Value.ToString() }), "Value", "Text", foreinglic);
        }



        public int? GetCurrentUserID()
        {
            var usuario = GetCurrentUsuario();
            if (usuario != null)
            {
                return usuario.UserID;
            }

            return
                null;
        }

        /// <summary>
        /// 1 (true) = Solo usuarios Logueados, 0(false) =  Todo el mundo
        /// </summary>
        /// <returns></returns>
        public bool allowOnlyLoggedUsers()
        {
            /*
           1 = Solo usuarios Logueados
           0 =  Todo el mundo
           */
            var onlyLoggedUsers = System.Web.Configuration.WebConfigurationManager.AppSettings["PARAMETER_KEY_ONLY_LOGGED_USER"].ToString(CultureInfo.InvariantCulture);

            var usuario = GetCurrentUsuario();

            bool opt = onlyLoggedUsers == "1";

            /*
             Si hay un usuario logueado y solo deben acceder usuarios logueados devolvera True, pero
             si el usuario que esta logueado es un tipo User, entonces poner el sistema como que es un cliente
             normal que hizo una cotizacion
             */
            if (usuario != null && opt)
            {
                if (usuario.UserType == Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum.User)
                {
                    opt = false;
                }
            }

            return opt;
        }


        public bool SetModeLey()
        {
            var leymode = System.Web.Configuration.WebConfigurationManager.AppSettings["PARAMETER_KEY_SET_MODE_LEY"].ToString();

            if (leymode == "1")
            {
                return true;
            }

            return false;
        }

        public bool SetModeFull()
        {
            try
            {
                var fullmode = System.Web.Configuration.WebConfigurationManager.AppSettings["PARAMETER_KEY_SET_MODE_FULL"].ToString();

                if (fullmode == "1")
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        #endregion

        #region Metodos compartidos

        protected QuotationViewModel getQuotationData(int QuotationID)
        {
            var quotData = oQuotationManager.GetQuotation(QuotationID);
            var data = new QuotationViewModel();

            data.StartDate = quotData.StartDate;
            data.EndDate = quotData.EndDate;
            data.PaymentFreqIdSelected = quotData.PaymentFreqIdSelected != null ? quotData.PaymentFreqIdSelected.Replace("\"", "'") : "";

            data.Id = quotData.Id;
            data.QuotationCoreNumber = quotData.QuotationCoreNumber;
            data.QuotationNumber = quotData.QuotationNumber;
            data.TotalPrime = quotData.TotalPrime;
            data.TotalISC = quotData.TotalISC;
            data.TotalDiscount = quotData.TotalDiscount;
            data.PaymentFrequency = quotData.PaymentFrequency;
            data.SendInspectionOnly = quotData.SendInspectionOnly;
            data.Financed = quotData.Financed;
            data.MonthlyPayment = quotData.MonthlyPayment;
            data.Period = quotData.Period;
            data.Domiciliation = quotData.Domiciliation;
            data.Credit_Card_Type_Id = quotData.Credit_Card_Type_Id;
            data.Expiration_Date_Month = quotData.Expiration_Date_Month.GetValueOrDefault();
            data.Expiration_Date_Year = quotData.Expiration_Date_Year;
            data.Credit_Card_Number = quotData.Credit_Card_Number;
            data.Credit_Card_Number_Key = quotData.Credit_Card_Number_Key;
            data.Card_Holder = quotData.Card_Holder;
            data.TotalFlotillaDiscount = quotData.TotalFlotillaDiscount;
            data.ISCPercentage = quotData.ISCPercentage;
            data.User_Id = quotData.User_Id;
            data.PolicyNumber = quotData.PolicyNumber;
            data.RequestTypeDesc = quotData.RequestTypeDesc;
            data.RequestTypeId = quotData.RequestTypeId;
            data.DomicileInitialPayment = quotData.DomicileInitialPayment;
            data.policyNoMain = quotData.policyNoMain;
            data.QuotationProduct = quotData.QuotationProduct;
            data.ApplyToDocumentRequired = quotData.ApplyToDocumentRequired;
            data.couponPercentageDiscount = quotData.couponPercentageDiscount.HasValue ? quotData.couponPercentageDiscount.Value : 0;
            data.couponCode = quotData.couponCode;
            data.RiskLevel = quotData.RiskLevel;
            data.CurrencySymbol = quotData.CurrencySymbol;
            data.FlotillaDiscountPercent = quotData.FlotillaDiscountPercent;
            return
                data;
        }
        protected IEnumerable<QuotationViewModel.drivers> getDriverData(int QuotationID)
        {
            var quotDriver = oQuotationManager.GetQuotationDrivers(QuotationID).Select(x => new QuotationViewModel.drivers
            {
                Id = x.Id,
                FirstName = x.FirstName,
                SecondName = x.SecondName,
                FirstSurname = x.FirstSurname,
                SecondSurname = x.SecondSurname,
                DateOfBirth = x.DateOfBirth,
                IsPrincipal = x.IsPrincipal,
                Address = x.Address,
                PhoneNumber = x.PhoneNumber,
                Mobile = x.Mobile,
                WorkPhone = x.WorkPhone,
                MaritalStatus = x.MaritalStatus,
                Job = x.Job,
                Company = x.Company,
                YearsInCompany = x.YearsInCompany,
                Sex = x.Sex,
                City_Country_Id = x.City_Country_Id,
                City_Domesticreg_Id = x.City_Domesticreg_Id,
                City_State_Prov_Id = x.City_State_Prov_Id,
                City_City_Id = x.City_City_Id,
                Nationality_Global_Country_Id = x.Nationality_Global_Country_Id,
                Email = x.Email,
                IdentificationType = x.IdentificationType,
                IdentificationNumber = x.IdentificationNumber,
                ForeignLicense = x.ForeignLicense,
                IdentificationNumberValidDate = x.IdentificationNumberValidDate,
                InvoiceTypeId = x.InvoiceTypeId,
                UserId = x.UserId,
                Modi_Date = x.Modi_Date,
                PostalCode = x.PostalCode,
                AnnualIncome = x.AnnualIncome,
                SocialReasonId = x.SocialReasonId,
                OwnershipStructureId = x.OwnershipStructureId,
                IdentificationFinalBeneficiaryOptionsId = x.IdentificationFinalBeneficiaryOptionsId,
                PepFormularyOptionsId = x.PepFormularyOptionsId,
                Home_Owner = x.Home_Owner,
                QtyPersonsDepend = x.QtyPersonsDepend,
                QtyEmployees = x.QtyEmployees,
                Linked = x.Linked,
                Segment = x.Segment,
                Fax = x.Fax,
                URL = x.URL,
                CityDesc = x.CityDesc,
                MunicipioDesc = x.MunicipioDesc,
                GlobalCountryDesc = x.GlobalCountryDesc,
                GlobalCountryDescEN = x.GlobalCountryDescEN,
                StateProvDesc = x.StateProvDesc,
                SocialReasonDesc = x.SocialReasonDesc,
                PepFormularyOptionsDesc = x.PepFormularyOptionsDesc,
                OwnershipStructureDesc = x.OwnershipStructureDesc,
                IdentificationFinalBeneficiaryOptionsDesc = x.IdentificationFinalBeneficiaryOptionsDesc
            });

            return
                  quotDriver;
        }
        protected IEnumerable<QuotationViewModel.Vehicles> getVehicleData(int QuotationID)
        {
            var defaultFuelType = oDropDownManager.GetParameter("PARAMETER_KEY_FUEL_TYPE_DEFAULT_VALUE").Value.ToInt();
            var defaultFuelTypeText = oDropDownManager.GetParameter("PARAMETER_KEY_FUEL_TYPE_DEFAULT_VALUE_TEXT").Value;

            var quotVehicle = oQuotationManager.GetQuotationVehicles(QuotationID).Select(x => new QuotationViewModel.Vehicles
            {
                VehicleNumber = x.VehicleNumber,
                Id = x.Id,
                VehicleDescription = x.VehicleDescription,
                Year = x.Year,
                Cylinders = x.Cylinders,
                Passengers = x.Passengers,
                Weight = x.Weight,
                Chassis = x.Chassis,
                Plate = x.Plate,
                Color = x.Color,
                VehiclePrice = x.VehiclePrice,
                InsuredAmount = x.InsuredAmount,
                PercentageToInsure = x.PercentageToInsure,
                TotalPrime = x.TotalPrime,
                TotalIsc = x.TotalIsc,
                TotalDiscount = x.TotalDiscount,
                SelectedProductCoreId = x.SelectedProductCoreId,
                VehicleTypeCoreId = x.VehicleTypeCoreId,
                SelectedProductName = x.SelectedProductName,
                VehicleTypeName = x.VehicleTypeName,
                VehicleMakeName = x.VehicleMakeName,
                ModelDesc = x.ModelDesc,
                UsageId = x.UsageId,
                UsageName = x.UsageName,
                StoreId = x.StoreId,
                StoreName = x.StoreName,
                Driver_Id = x.Driver_Id,
                VehicleModel_Make_Id = x.VehicleModel_Make_Id,
                VehicleModel_Model_Id = x.VehicleModel_Model_Id,
                Quotation_Id = x.Quotation_Id,
                SelectedVehicleTypeId = x.SelectedVehicleTypeId,
                SelectedVehicleTypeName = x.SelectedVehicleTypeName,
                SelectedCoverageCoreId = x.SelectedCoverageCoreId,
                SelectedCoverageName = x.SelectedCoverageName,
                VehicleYearOld = x.VehicleYearOld,
                SurChargePercentage = (x.SurChargePercentage.HasValue ? x.SurChargePercentage.Value : 0),
                NumeroFormulario = x.NumeroFormulario,
                RateJson = x.RateJson,
                UserId = x.UserId,
                Modi_Date = x.Modi_Date,
                SecuenciaVehicleSysflex = x.SecuenciaVehicleSysflex,
                IsFacultative = x.IsFacultative,
                AmountFacultative = x.AmountFacultative,
                VehicleQuantity = x.VehicleQuantity,
                TotalPrimeVehicle = x.TotalPrime.GetValueOrDefault() + x.TotalIsc.GetValueOrDefault(),
                ProratedPremium = x.ProratedPremium,
                SelectedVehicleFuelTypeId = x.SelectedVehicleFuelTypeId.HasValue ? x.SelectedVehicleFuelTypeId.Value : defaultFuelType,
                SelectedVehicleFuelTypeDesc = x.SelectedVehicleFuelTypeId.HasValue ? x.SelectedVehicleFuelTypeDesc : defaultFuelTypeText,
                minimumdepreciation = x.minimumdepreciation,
                IsOverPreMium = x.IsOverPreMium,
                minimumdepreciationId = x.minimumdepreciationId,
                TotalDeppreciation = x.TotalDeppreciation,
                TotalDepreciationId  = x.TotalDepreciationId,

                _services = oQuotationManager.GetQuotationCoverage(x.Id.GetValueOrDefault(), CommonEnums.CoverageFilterType.ServiciosSeleccionados.ToInt()).Select(a => new QuotationViewModel.coverages
                {
                    Id = a.Id,
                    IsSelected = a.IsSelected,
                    CoverageDetailCoreId = a.CoverageDetailCoreId,
                    Name = a.Name,
                    Amount = a.Amount,
                    MinDeductible = a.MinDeductible,
                    SelfDamagesToProductLimits = a.SelfDamagesToProductLimits,
                    ThirdPartyToProductLimits = a.ThirdPartyToProductLimits,
                    ServiceType_Id = a.ServiceType_Id,
                    Limit = a.Limit,
                    UserId = a.UserId,
                    Deductible = a.Deductible,
                    CoverageType = a.CoverageType,
                    coveragePercentage = a.coveragePercentage,
                    baseDeductible = a.baseDeductible,
                    AllowsToSummarize = a.AllowsToSummarize
                }).ToList()
            });
            return
                quotVehicle;
        }
        protected IEnumerable<QuotationViewModel.coverages> getCoverageData(int VehicleId)
        {
            var coverages = oQuotationManager.GetQuotationCoverage(VehicleId, CommonEnums.CoverageFilterType.Todo.ToInt()).Select(a => new QuotationViewModel.coverages
            {
                Id = a.Id,
                IsSelected = a.IsSelected,
                CoverageDetailCoreId = a.CoverageDetailCoreId,
                Name = a.Name,
                Amount = a.Amount,
                MinDeductible = a.MinDeductible,
                SelfDamagesToProductLimits = a.SelfDamagesToProductLimits,
                ThirdPartyToProductLimits = a.ThirdPartyToProductLimits,
                ServiceType_Id = a.ServiceType_Id,
                Limit = a.Limit,
                UserId = a.UserId,
                Deductible = a.Deductible,
                CoverageType = a.CoverageType,
                coveragePercentage = a.coveragePercentage,
                baseDeductible = a.baseDeductible,
                AllowsToSummarize = a.AllowsToSummarize
            });

            return
                   coverages;

        }
        protected QuotationViewModel.VehicleProductLimits getVehicleProductLimit(int VehicleId)
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

        protected IEnumerable<SelectListItem> GetPaymentFreq(IEnumerable<SelectListItem> dataDrop, DateTime? BeginDate, DateTime? EndDate)
        {
            var ConfigFreq = new List<Tuple<string[], long, long>>(0);
            ConfigFreq.Add(new Tuple<string[], long, long>(new string[] { "Inicial + 1 Cuota", "Inicial + 2 Cuotas", "Inicial + 3 Cuotas" }, 6, 9));
            ConfigFreq.Add(new Tuple<string[], long, long>(new string[] { "Inicial + 1 Cuota", "Inicial + 2 Cuotas" }, 3, 6));
            var Months = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Month, BeginDate.GetValueOrDefault(), EndDate.GetValueOrDefault());

            //Mayor de 9 meses todas las opciones
            if (Months > 9)
                dataDrop = dataDrop.ToList();
            else
                //Menor de 3 meses pago unico	
                if (Months < 3)
                    dataDrop = dataDrop.Where(d => d.Text.Contains("Pago Único")).ToList();
                else
                    //Entre 6 y 9  (3 cuotas, 2 cuotas, 1 cuota)
                    if (new long[] { 6, 9 }.Contains(Months))
                    {
                        var data = ConfigFreq.FirstOrDefault(c => c.Item2 == 6 && c.Item3 == 9).Item1.ToArray();
                        dataDrop = dataDrop.Where(d => data.Contains(d.Text)).ToList();
                    }
                    else
                        //Entre 3 y 6 (2 cuotas, 1 cuota)
                        if (new long[] { 3, 6 }.Contains(Months))
                        {
                            var data = ConfigFreq.FirstOrDefault(c => c.Item2 == 3 && c.Item3 == 6).Item1.ToArray();
                            dataDrop = dataDrop.Where(d => data.Contains(d.Text)).ToList();
                        }

            return
                dataDrop;

        }

        protected List<RequestChanges> getRequestChanges(string policyNo, int? conditionID, int secuenceVehicle)
        {
            var result = oVehicleProductManager.GetRequestChanges(new RequestChanges.Parameter()
            {
                policy_Number = policyNo,
                condition_Id = conditionID,
                IsLasRecord = true
            }).ToList();


            var realResult = result.Where(x => x.Vehicle_Secuence == secuenceVehicle).ToList();

            return realResult;
        }

        /// <summary>
        /// Obtiene el porcentaje de depreciacion minima de un vehiculo desde sysflex
        /// </summary>
        /// <param name="VehicleMakeName"></param>
        /// <param name="ModelDesc"></param>
        /// <param name="Year"></param>
        /// <param name="Ramo"></param>
        /// <param name="SubRamo"></param>
        /// <returns></returns>
        protected Tuple<decimal?, bool?, int?> GetPercentageMinimumDeductible(string VehicleMakeName, string ModelDesc, int Year, int Ramo, int SubRamo, int AgentCode)
        {
            var result = new Tuple<decimal?, bool?, int?>(null, null, null);

            var r = oCoreProxy.GetIsOverPremium(AgentCode,
                                                VehicleMakeName,
                                                ModelDesc,
                                                Year,
                                                Ramo,
                                                SubRamo);

            return
                result = new Tuple<decimal?, bool?, int?>(r.Percent.GetValueOrDefault(), r.Result, r.PercentOverId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="VehicleMakeName"></param>
        /// <param name="ModelDesc"></param>
        /// <param name="Year"></param>
        /// <param name="Ramo"></param>
        /// <param name="SubRamo"></param>
        /// <param name="AgentCode"></param>
        /// <param name="TipoVehiculo"></param>
        /// <returns></returns>
        protected Tuple<decimal?, int?> GetPercentageTotalDepreciation(int AgentCode, string VehicleMakeName, string ModelDesc, int Year, int Ramo, int SubRamo, int TipoVehiculo)
        {
            var result = new Tuple<decimal?, int?>(null, null);

            var r = oCoreProxy.GetTotalDepreciationPercent(AgentCode, VehicleMakeName, ModelDesc, Year, Ramo, SubRamo, TipoVehiculo);

            return
                result = new Tuple<decimal?, int?>(r.PercentTotalDepreciation, r.PercentTotalDepreciationId);
        }

        #endregion 


        #region Envio Error
        public void SendErrorQuotation(string quotationNumber, string messageError, Exception ex = null)
        {
            try
            {

                var emails = oDropDownManager.GetParameter("PARAMETER_KEY_LIST_EMAIL_ERROR").Value;
                var senderError = oDropDownManager.GetParameter("PARAMETER_KEY_EMAIL_SENDER").Value;
                if (!string.IsNullOrEmpty(emails))
                {
                    var subject = string.Format("Error enviando la cotizacion {0} ", quotationNumber);
                    var body = messageError + (ex != null ? " EXCEPTION: " + ex : "");

                    List<string> destinatariosListError = new List<string>();

                    foreach (var e in emails.Split(','))
                    {
                        destinatariosListError.Add(e);
                    }

                    SendEmailHelper.SendMail(senderError, destinatariosListError, subject, body, null);
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        protected void SetPorcDeprecMin(VehicleProduct v, Quotation getquo)
        {
            var quotationUser = getQuotationUserById(getquo.User_Id.GetValueOrDefault());
            int AgentCode = -1;
            if (getquo != null && quotationUser != null && quotationUser.AgentId.HasValue)
            {
                var userAgenCode = getAgenteUserInfo(quotationUser.AgentId.Value);
                if (userAgenCode != null)
                {
                    if (userAgenCode.AgentId <= 0)
                        userAgenCode = getAgenteUserInfo(quotationUser.Username);//que es el nameid

                    int.TryParse(userAgenCode.AgentCode, out AgentCode);
                }
            }

            //Obtener el porcentage minimo de depreciacion
            var r = GetPercentageMinimumDeductible(v.VehicleMakeName, v.ModelDesc, v.Year.GetValueOrDefault(), 106, v.SelectedCoverageCoreId.GetValueOrDefault(), AgentCode);

            var vehicleSaved = oVehicleProductManager.SetVehicleProduct(new VehicleProduct.Parameter
            {
                id = v.Id,
                isOverPreMium = r.Item2.GetValueOrDefault(),
                minimumdepreciation = r.Item1,
                minimumdepreciationId = r.Item3
            });
        }

        protected void SetPorcTotalDeprec(VehicleProduct v, Quotation getquo)
        {
            var quotationUser = getQuotationUserById(getquo.User_Id.GetValueOrDefault());
            int AgentCode = -1;
            if (getquo != null && quotationUser != null && quotationUser.AgentId.HasValue)
            {
                var userAgenCode = getAgenteUserInfo(quotationUser.AgentId.Value);
                if (userAgenCode != null)
                {
                    if (userAgenCode.AgentId <= 0)
                        userAgenCode = getAgenteUserInfo(quotationUser.Username);//que es el nameid

                    int.TryParse(userAgenCode.AgentCode, out AgentCode);
                }
            }

            //Obtener la depreciacion total
            var r = GetPercentageTotalDepreciation(AgentCode, v.VehicleMakeName, v.ModelDesc, v.Year.GetValueOrDefault(), 106, v.SelectedCoverageCoreId.GetValueOrDefault(), v.SelectedVehicleTypeId.GetValueOrDefault());

            var vehicleSaved = oVehicleProductManager.SetVehicleProduct(new VehicleProduct.Parameter
            {
                id = v.Id,
                totalDeppreciation = r.Item1,
                totalDepreciationId= r.Item2
            });
        }


        public decimal getFlotillaPercentByQty(int qtyVehicles)
        {
            decimal percentParam = 0;

            if (qtyVehicles > 0)
            {
                var jsonParam = oDropDownManager.GetParameter("PARAMETER_KEY_PERCENT_FLOTILLA_DISCOUNT").Value;

                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Utility.Percent_Flotilla_Discount>>(jsonParam);

                foreach (var qty in json)
                {
                    if (qtyVehicles >= qty.From && qtyVehicles <= qty.To)
                    {
                        percentParam = (qty.Porc * 100);

                        return percentParam;
                    }
                }
            }
            return percentParam;
        }


        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue // Use this value to set your maximum size for all of your Requests
            };
        }


        public bool IsAllDeclarativaProduct(int quotationID)
        {
            var data = getVehicleData(quotationID);

            if (data != null)
            {
                var prods = data.Select(c => c.SelectedProductName).Distinct();
                string[] prodDecla = new string[] { "DECLARATIVA" };

                //Si hay un producto diferente de declarativa entonces todos los vehiculos no son declarativa
                var notOnlyDecla = prods.Where(x => !prodDecla.Contains(x)).Count();
                if (notOnlyDecla <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }
    }
}