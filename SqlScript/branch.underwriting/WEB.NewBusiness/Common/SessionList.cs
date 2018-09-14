﻿using Entity.UnderWriting.Entities;
using Statetrust.Framework.Security.Bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.Common
{
    public class SessionList
    {
        public SessionContact ContactInfo { get; set; }

        public SessionIllustration IllustrationInfo { get; set; }

        private string key = "SessionData";

        public SessionList(string KeyName = "SessionData")
        {
            key = KeyName;

        }

        public SessionList Stored
        {
            get
            {

                return HttpContext.Current.Session[key] as SessionList;
            }
        }

        public void Save()
        {
            if (HttpContext.Current.Session == null)
            {

                HttpContext.Current.Session.Add(key, new SessionList());
                (HttpContext.Current.Session[key] as SessionList).ContactInfo = new SessionContact();
                (HttpContext.Current.Session[key] as SessionList).IllustrationInfo = new SessionIllustration();
            }
            else
            {
                if (HttpContext.Current.Session[key] == null)
                {
                    HttpContext.Current.Session.Add(key, new SessionList());
                    (HttpContext.Current.Session[key] as SessionList).ContactInfo = new SessionContact();
                    (HttpContext.Current.Session[key] as SessionList).IllustrationInfo = new SessionIllustration();
                }
            }

            foreach (PropertyInfo item in PopertiesToSave())
            {
                switch (item.Name)
                {
                    case "ContactInfo":
                        (HttpContext.Current.Session[key] as SessionList).ContactInfo = (Stored != null ? Stored.ContactInfo : (HttpContext.Current.Session[key] as SessionList).ContactInfo);
                        break;
                }
                switch (item.Name)
                {
                    case "IllustrationInfo":
                        (HttpContext.Current.Session[key] as SessionList).IllustrationInfo = (Stored != null ? Stored.IllustrationInfo : (HttpContext.Current.Session[key] as SessionList).IllustrationInfo);
                        break;
                }
            }
        }

        private List<PropertyInfo> PopertiesToSave()
        {
            List<PropertyInfo> PropertyList = new List<PropertyInfo>((HttpContext.Current.Session[key] as SessionList).GetType().GetProperties());

            List<PropertyInfo> TmpList = new List<PropertyInfo>(PropertyList);

            TmpList.Remove(PropertyList.Where(x => x.Name == "Stored").First());

            foreach (PropertyInfo prop in PropertyList)
                if (prop.GetValue((HttpContext.Current.Session[key] as SessionList), null) != null)
                    TmpList.Remove(PropertyList.Where(x => x.Name == prop.Name).First());

            return TmpList;
        }
    }

    public class SessionContact
    {
        public int Corp_Id { get; set; }
        public int Region_Id { get; set; }
        public int Country_Id { get; set; }
        public int Domesticreg_Id { get; set; }
        public int State_Prov_Id { get; set; }
        public int City_Id { get; set; }
        public int Office_Id { get; set; }
        public int Case_Seq_No { get; set; }
        public int Hist_Seq_No { get; set; }
        public int? Owner_Id { get; set; }
        public int? Agent_Legal { get; set; }
        public string Policy_Id { get; set; }
        public int? Contact_Id { get; set; }
        public int Directory_Id { get; set; }
        public int RoleTypeId { get; set; }
        public int ContactSeq_No { get; set; }
        public bool isNewCase { get; set; }
        public bool isCompanyOwner { get; set; }
        public int? ContactEntityID { get; set; }
        public Boolean isOwnerContact { get; set; }
        public Boolean ContactServicesIsActive { get; set; }
        public int? DesignatedPensionerContactId { get; set; }
        public int? Agent_Id { get; set; }
        public int Agent_LoginId { get; set; }
        public int? Relationship_With_Insured_Id { get; set; }
        public int? Relationship_With_Owner_Id { get; set; }
        public int? Relationship_With_Owner_ToAgentId { get; set; }
        public bool IsDataSearch { get; set; }
        public bool IsReadOnly { get; set; }
        public int? StudentContactId { get; set; }
        public string TabRedirect { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public int? InsuredAddContactId { get; set; }
        public bool ShowAddNewContact { get; set; }
        public int? PaymentId { get; set; }
        public int? PaymentDetId { get; set; }
        public int PagerSize { get; set; }
        public int ProjectId { get; set; }
        public int? UserID { get; set; }
        public int CompanyId { get; set; }
        public Boolean IsReadyToReview { get; set; }
        public Boolean IsDataReviewMode { get; set; }
        public Boolean IsPlanChange { get; set; }
        public Utility.Language Language { get; set; }
        public Utility.Project Proyect { get; set; }
        public Boolean isChangingLang { get; set; }
        public Boolean isSavePlan { get; set; }
        public string AgentName { get; set; }
        public string Office { get; set; }
        public string KeyNameProduct { get; set; }
        public Boolean isUser { get; set; }
        public String InsuredFullName { get; set; }
        public Utility.ProductLine ProductLine { get; set; }
        public Statetrust.Framework.Security.Bll.Usuarios.UserTypeEnum UserType { get; set; }
        public Utility.Tab TabSelected { get; set; }//Esto aplica para unica y exclusivamente para Data Review no usar en nuevos negocios
        public string StatusNameKey { get; set; }
        public bool isReclamacionesQuotRole { get; set; }
        public bool isDescuentocot { get; set; }
        public bool isDescuentoCot100Porc { get; set; }        
        public bool IsAgentQuotRole { get; set; }
        public bool IsSuscripcionQuotRole { get; set; }
        public bool IsInspectorQuotRole { get; set; }
        public bool IsSuscripcionManagerQuotRole { get; set; }
        public bool IsDirectorQuotRole { get; set; }
        public bool IsSucripcionDirectorQuotRole { get; set; }
        public bool IsUserCot { get; set; }
        public bool IsConfirmationCallCot { get; set; }
        public bool IsConfirmationCallManagerCot { get; set; }
        public bool IsValidateFacultativeCot { get; set; }
        public bool IsCreditoCot { get; set; }
        public bool IsFacultativeCot { get; set; }
        public bool IsPreSuscribcionDescuentoCot { get; set; }
        public bool IsPreSuscribcionRecargoCot { get; set; }
        public bool CanViewPersonalInfo { get; set; }
        public bool CanViewStatistics { get; set; }
        public bool IsAngetInspectorQuotRole { get; set; }
        public bool IsViewPrimeAndRateCot { get; set; }
        public int? AssignedSubscriberId { get; set; }
        public string hdnQuotationTabs { get; set; }
        public string Nationality { get; set; }
        public int NationalityCountryId { get; set; }
        public int? InsuredVehicleId { get; set; }
        public int? InspectorAgentId { get; set; }
        public int QuotationAgentId { get; set; }
        public string CustomerName { get; set; }
        public IEnumerable<DropDown> DataConfig { get; set; }
        public int? ReviewId { get; set; }
        public Utility.Country Country { get; set; }
        public string DefaulltPassword { get; set; }
        public string DefaulltPasswordEF { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
        public string userEF { get; set; }
        public string passEF { get; set; }
        public bool RefreshInbox { get; set; }
        public int RefreshFrecuency { get; set; }
        public Utility.AlliedLinesType AlliedLinesProductBehavior { get; set; }
        public string hdnTabGroup { get; set; }
        public IEnumerable<Policy.TabRol> TabsByRol { get; set; }
        public string Bandeja { get; set; }
        public Utility.Tabs InboxTabRedirect { get; set; }
        public int CounterFreQRefresh { get; set; }
        public string PolicyNoMain { get; set; }
        public bool HasFacultative { get; set; }
        public DataTable AgentChain { get; set; }
        public bool IsAgentServiceQuoRole { get; set; }
        public List<String> UsuarioPropiedades { get; set; }
        public bool BlackListHasProblem { get; set; }
        public bool? BlacklistCheck { get; set; }
        public int? BlacklistCheckUser { get; set; }
        public string BlacklistCheckUserName { get; set; }
        public string BlacklistMember { get; set; }
        public bool IsValidateBlackListCot { get; set; }
        public bool Financed { get; set; }
        public decimal? MonthlyPayment { get; set; }
        public int? Period { get; set; }
        public decimal? annualPremium { get; set; }
        public decimal PorcKCO { get; set; }
        public string LoanPetitionNo { get; set; }
        public bool ECreateLoanKCO { get; set; }
        public bool? DirectDebit { get; set; }
        public bool? IncludeInitialPayment { get; set; }
        public double TaxPercentage { get; set; }
        public bool ViewCreditCardInformation { get; set; }
        public bool CanViewContactInformation { get; set; }
        public bool CanDomitiliationPayment { get; set; }
        public bool CanPrintInvoice { get; set; }
        public string FuelTypeDesc { get; set; }
      

        public void CleanSessionCase()
        {
            //Caso
            Case_Seq_No = -1;
            Hist_Seq_No = -1;
            PaymentId = null;
            PaymentDetId = null;
            KeyNameProduct = string.Empty;
            DesignatedPensionerContactId = null;
            InsuredAddContactId = null;
            Owner_Id = null;
            Relationship_With_Insured_Id = null;
            Relationship_With_Owner_Id = null;
            Relationship_With_Owner_ToAgentId = null;
            Contact_Id = null;
            ContactEntityID = -1;
            AssignedSubscriberId = null;
            IsDataSearch = false;
            IsReadOnly = false;
            isNewCase = true;
            IsDataReviewMode = false;
            IsReadyToReview = false;
            IsPlanChange = false;
            isOwnerContact = false;
            isSavePlan = false;
            Proyect = Utility.Project.NewBusiness;
            AgentName = string.Empty;
            Office = string.Empty;
        }
        public bool isExclusion { get; set; }
        public bool isVehicleChange { get; set; }
        

        #region Auto
        public string DefaultImageCar { get; set; }

        public int AutoSectionInformacionesGeneralesCount { get; set; }
        public int AutoSectionVerificacionDatosGeneralesCount { get; set; }
        public int AutoSectionCombustibleCount { get; set; }
        public int AutoSectionFuncionamientoCount { get; set; }
        public int AutoSectionPartesFisicasCount { get; set; }
        public int AutoSectionAccesoriosTapiceriaCount { get; set; }
        public int AutoSectionSeguridadComplementosCount { get; set; }
        public int AutoSectionOtrasInformacionesCount { get; set; }
        public int AutoSectionPhotosCount { get; set; }
        public string InspectorName { get; set; }
        #endregion

        #region Lineas Aliadas

        #region Property
        public int PropertySectionDatosGeneralesCount { get; set; }
        public int PropertySectionSumasAseguradasCount { get; set; }
        public int PropertySectionDescripcionCount { get; set; }
        public int PropertySectionHistorialPerdidasCount { get; set; }
        public int PropertySectionHistorialPerdidasNingunaAnterioresCount { get; set; }
        public int PropertySectionSiniestralidadZonaCount { get; set; }
        public int PropertySectionColindanciasCount { get; set; }
        public int PropertySectionLocalizacionRiesgoCount { get; set; }
        public int PropertySectionDescripcionProcesosCount { get; set; }
        public int PropertySectionDescripcionPeligrosCount { get; set; }
        public int PropertySectionPrevencionProteccionCount { get; set; }
        public int PropertySectionEstimacionPerdidasCoberturaIncendioCount { get; set; }
        public int PropertySectionExposicionRiesgosCount { get; set; }
        public int PropertySectionCategoriaRiesgoCount { get; set; }
        public int PropertySectionOpinionRiesgoCount { get; set; }
        public int PropertySectionRecomendacionesTecnicasCount { get; set; }
        public int PropertySectionRecomendacionesHechasEnviadasAseguradoCount { get; set; }
        public int PropertySectionFotografiasCount { get; set; }
        public string PolicyOffice { get; set; }
        public decimal InsuranceAmount { get; set; }

        #endregion

        #endregion
    }

    public class SessionIllustration
    {
        public long? CustomerPlanNo { get; set; }
        public long? CustomerPlanOwnerNo { get; set; }
        public long? CustomerNo { get; set; }
        public string IllustrationStatusCode { get; set; }
        public string IllusCompanyId { get; set; }
        public int? IllusUserID { get; set; }
    }


}