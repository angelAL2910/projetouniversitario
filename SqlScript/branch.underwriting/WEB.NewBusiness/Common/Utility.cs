﻿using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid;
using Entity.UnderWriting.Entities;
using iTextSharp.text.pdf;
using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WEB.NewBusiness.Common.Illustration;
using GlobalService = WebServices.GlobalService;


namespace WEB.NewBusiness.Common
{
    /// <summary>
    /// Author: Lic. Carlos Ml. Lebron
    /// </summary>
    public static class Utility
    {
        #region Enums
        public enum RecordStatus : byte { New = 0, Old = 1 }

        public enum TemplateType { GridViewDataTextColumn, GridViewDataColumn, GridViewDataCheckColumn }

        public enum WebControlType { TextBox, Literal, HiddenField, DataBoundField, CheckBox, Button }

        public enum RequirementSubType { PersonalIdentification, VehicleIdentification, CompanyInformation1, CompanyInformation2, CompanyInformation3 }

        public enum TipoRiesgo { NONE, RA, RM, RB }

        public enum TipoNivelRiesgo { NONE, RIESGOALTO, RIESGOMEDIO, RIESGOBAJO }

        public enum BlackListType { NONE, MA, NM }

        public enum SysFlexACTION { INSERT, UPDATE, DELETE }

        public enum DeclineType { Transunion, BlackList }

        public enum BlackListAction { Yes, No }


        public enum RequestType
        {
            Emision = 1,
            Inclusion = 2,
            Exclusion = 3,
            Renovacion = 4,
            Cambios = 5,
            InclusionDeclarativa = 6
        }

        public enum GpFrecuencytype { Daily = 1, Weekly = 2, Biweekly = 3, Monthly = 4, Bimonthly = 5, Quarterly = 6, Halfyearly = 7, Yearly = 8, Onetime = 9 };

        public enum CreditCardType
        {
            MasterCard = 1,
            American_Express = 2,
            Visa = 3,
            Diners = 4,
            Discover = 5,
            Amex = 6
        }

        public enum TabsGroup
        {
            lnkPreSuscripcion,
            lnkSuscripcion,
            lnkHistorico
        }

        public enum Tabs
        {
            None,
            lnkIllustrationsToWork,
            lnkIncompleteIllustration,
            lnkCompleteIllustrations,
            lnkDeclinedByClient,
            lnkExpired,
            lnkExpiring,
            lnkApprovedByClient,
            lnkSubscriptions,
            lnkDeclinedBySubscription,
            lnkMissingDocuments,
            lnkMissingInspections,
            lnkApprovedBySubscription,
            lnkHistoricalIllustrations,
            lnkPuntoVentaTab,
            lnkStatistics,
            lnkConfirmationCall,
            lnkDiscounts,
            lnkFacultative
        }

        public enum LogTypeId { Request = 1, Response = 2, Exception = 3 }

        public enum DropDownType
        {
            Occupation,
            Profession,
            OccupationType,
            City,
            City2,
            Country,
            MaritalStatus,
            PolicyStatus,
            ByDate,
            Manager,
            SubManager,
            Office,
            AddressType,
            PhoneType,
            EmailType,
            Smoker,
            Gender,
            Summary,
            StateProvince,
            Relationship,
            RelationshipHealth,
            IdType,
            Agent,
            AmmendmentType,
            AmmendmentTypeScope,
            RiderStatus,
            LengthatWork,
            Months,
            IssuedBy,
            PrimaryBeneficiary,
            PlanType,
            Product,
            ScaleType,
            Currency,
            Serie,
            ProfileType,
            PaymentFrequency,
            PolicyContactByRole,
            PolicyContactByRoleOnlyInsured,
            ProfileType_NewBusiness,
            ContributionType,
            ContributionPeriod,
            PaymentType,
            PaymentTypeCheck,
            PaymentTypeCC,
            PaymentSource,
            ReceiptType,
            RetirementPeriod,
            DefermentPeriod,
            ProductByFamily,
            BankABANumber,
            ProductType,
            FamilyProduct,
            AnnuityPeriod,
            CalculateType,
            ActivityRiskType,
            HealthRiskType,
            FrequencyType,
            InvestmentProfile,
            IllustrationType,
            IllustrationStatus,
            QuestionDisease,
            Boolean,
            DataReviewNoteType,
            LoteType,
            CountryOfResidence,
            Mortality,
            Commission,
            SurrenderPenal,
            RelationshipFuneral,
            CompanyType,
            RelationshipPayment,
            RelationshipReferred,
            RelationshipAgent,
            BloodType,
            RejectReason,
            ProviderTransactionType,
            ProviderResponseCode,
            ProjectConfigurationValue,
            Company,
            LatinAmericanCountries,
            DeductibleCategory,
            DeductibleType,
            StudentStatus,
            NotePredefinied,
            DocumentCategory,
            Subscriber,
            AgentQuotation,
            AgentChainQoutations,
            GIV,
            Roles,
            OfficeCot,
            AgentCot,
            AutoSave,
            StatisticsReportType,
            StatisticsReportEmission,
            StatisticsReportPerformance,
            StatisticsReportSalesChannels,
            StatisticsProduct,
            AgentQuoEmail,
            VIFSessionCount,
            VehicleColor,
            Provider,
            QtyPayments,
            ContactInvoiceType,
            BusinessLineQuo,
            AlliedLinesType,
            FacultativeCompany,
            Facultative,
            ViewPrimeAndRate,
            FacultativeDocValidation,

            CustomerBusinessLine,
            CustomerBusinessLine2,
            //Bmarroquin 11-02-2017 a raiz de tropicalizacion de ESA, se agrega nueva Enumeracion para la pantalla pagos de New Business
            BankList,
            FinalBeneficiaryOption,
            PepFormularyOption,
            CompanyStructure,
            CompanyActivity,
            RelationshipPEP,
            Boundary,
            Endorsement,
            CanValidDocumentTab,
            Municipio,
            BuildType,
            ValidateBlackList,
            InspectorCot,
            CreditCard,
            StatisticsReportInclusions,
            StatisticsReportExclusions,
            TypeOfPerson,
            PrintInvoices,
            TimeDimension
        }

        public class TiposQuestionarios
        {
            public string QuestionarieName { get; set; }
        }

        public enum PaymentSourceType
        {
            Other = 0,
            ACH,
            CreditCard,
            Deposit,
            Check,
            Wire,
            Cash
        }

        public enum TriggerType { PostBackTrigger, AsyncPostBackTrigger };

        public enum Language { en = 1, es = 2 };

        public enum Currency { USD = 1, EUR = 2, DOP = 3 };

        public enum RyderType
        {
            SeguroMuerteAccidental = 1,
            SeguroTemporalAdicional = 2,
            SeguroAseguradoAdicional = 3,
            SeguroFamiliarAdicional = 4,
            Lote = 5,
            Repatriation = 6,
            Dependent = 9,
            MaternityandNewbornComplication = 7,
            OrganTransplant = 88,
            GastosFunerarios = 9,
            InvalidesTotal = 8
        }

        public enum Project { UnderWriting = 1, NewBusiness = 2, DataReview = 3 };

        public enum Tab { ClientInfo = 1, OwnerInfo = 2, PlanPolicy = 3, Beneficiaries = 4, Requirements = 5, Payment = 6, HealthDeclaration = 7, Compliance = 8 }

        public enum ContactRoleIDType { Owner = 1, Client = 2, AddicionalInsured = 3, Beneficiarie = 4, DesignatedPensioner = 5, Student = 6, IncludedFamiliar = 7, Dependent = 9, Legal = 10 };

        public enum ContactTypeId { Client = 1, Contact = 6, Company = 7, Beneficiary = 4, AgentLegal = 10 };

        public enum OperationType { Insert, InsertAll, Edit, Delete, None };

        public enum AlliedLinesType
        {
            None = 0,
            Airplane = 1,
            Bail,
            Navy,
            Property,
            Transport,
            Vehicle
        }

        public enum AlliedLinePropertyPhotos
        {
            Internal = 1,
            External
        }

        public enum ProductBehavior
        {
            None,
            Horizon,
            Axys,
            EduPlan,
            Scholar,
            CompassIndex,
            Legacy,
            Sentinel,
            Lighthouse,
            Guardian,
            GuardianPlus,
            Orion,
            OrionPlus,
            Luminis,
            LuminisVIP,
            Exequium,
            ExequiumVIP,
            Elite,
            Select,
            Fortis,
            Serenity,
            Asistencia90dias,
            Asistencia30dias,
            Asistencia60dias,
            basico,
            ECONOMAX,
            VIDACRED,
            GRPLIFE
        }

        public enum ProductLine { None, LifeInsurance, Funeral, HealthInsurance, Retirement, Education, TermInsurance, Auto, AlliedLines }

        public enum TypeReportGenerate
        {
            ExportToExcel,
            ExportToPDF,
            ExportToHTML,
            ExportToCSV
        }

        public enum Order : byte { Asc, Desc }

        public enum CommType
        {
            Phone,
            Email,
            Address
        }

        public enum EFamilyProductType
        {
            Education,
            Retirement,
            LifeInsurance,
            TermInsurance,
            Funeral,
            Auto,
            Health,
            IncendioLineasAliadas,
            LineasComerciales
        }

        public enum EmailType : int { Home = 6, Work = 7, Other = 1 };

        public enum DirectoryType
        {
            OtherPhone = 1,
            HomePhone = 6,
            BusinessPhone = 7,
            CellPhone = 8,
            Fax = 11
        }

        public enum CalculateType
        {
            AnnuityAmount,
            InsuredAmount,
            PremiumAmount,
            VerifyNone
        }

        public enum EPlanType
        {
            Level,
            Incremental,
            NonInsured,
            Insured,
            None
        }

        public enum EContributionType
        {
            Continuous,
            UntilAge,
            NumberOfYears
        }

        public enum RiderType
        {
            ACB,
            ACDB,
            Illness,
            OIR,
            Primary,
            Term
        }

        public enum IllustrationStatus
        {
            New,
            NewPlan,
            Illustration,
            Submitted,
            Delete,
            ApprovedBySubscription,
            DeclinedByClient,
            DeclinedBySubscription,
            Issued,
            PendingByClient,
            TimeExpired,
            TimeExpiring,
            Subscription,
            Incomplete,
            ApprovedByClient,
            Effective,
            MissingDocuments,
            MissingInspection,
            Complete,
            FacultativesCases,
            Cancelled
        }

        public enum PolicyNotesReferenceType
        {
            IllustrationNotes = 15
        }

        public enum PolicyStatusChangeType
        {
            IllustrationToSubscription = 11,
            IllustrationChanges,
            IllustrationExpired = 15,
            IllustrationExpiring = 16

        }

        public enum PaymentStatus { Other = 0, Approved = 1, Pending = 2, InProcess = 3, Denied = 4 }

        public enum RelationShipNameKey
        {
            Daughter,
            Husband,
            Son,
            Wife
        }

        public enum PeriodsDate
        {
            YTD = 1, 
            MTD = 2,
            ThirtyDays =3,
            Quarter1 = 4,
            Quarter2 = 5,
            Quarter3 = 6,
            Quarter4 = 7,
            CustomDate = 8
        }

        public enum DiscountRules
        {
            DiscountRulesBySalesChannelId,
            DiscountRulesByRoleType,
            CommercialDiscountRulesBySalesChannelId,
            SubscriptionDiscountRulesByRoleType,
            DiscountRulesByFlotilla,
            ByYearDiscountRules,
            DiscountRulesIndexByProductType,
            Surcharge,
            SurchargeByDriverAge,
            SurchargeByVehicleType,
            SurchargeByUseType
        }

        public enum DiscountRulesDetail
        {
            MinYear,
            MaxYear,
            Index,
            VehicleType,
            MinCantVehicles,
            MaxCantVehicles,
            Discount
        }

        public enum CoverageType
        {
            DamageToThirdParties = 1,
            OwnDamages = 2,
            Sumplements = 3
        }

        public enum DocumentCategoryNameKey
        {
            Auto_QuotDocEnrollment,
            AtlParticularsConditionsTemplates,
            AtlCSMarbTemplates
        }

        public enum ReasonPredefinieds
        {
            DeniedIllustrationReason,
            DeniedSubscriptionIllustrationReason,
            DiscountIllustrationReason,
            SurchargeIllustrationReason,
        }

        public enum tabsQoutationsPopUp
        {
            QoutationsToWork,
            QoutationsCompleted,
            QoutationsSubscription,
            QoutationsMissingDocuments,
            QoutationsMissingInspections,
            QoutationsDeclinedByClient,
            QoutationsExpired,
            QoutationsExpiring,
            QoutationsApprovedByClient,
            QoutationsDeclinedBySubscription,
            QoutationsApprovedBySubscription,
            QoutationsHistoricalIllustrations,
            QoutationsConfirmationCall,
            QoutationsDiscount
        }

        public enum DiscountRoles
        {
            DirectorCot,
            DirectorSuscricion,
            SuscripcionManagerCot,
            DescuentoCot,
            DescuentoCot100Porc,
            UserCot
        }

        public static AgentRoleType[] AgentRoles = new AgentRoleType[] { AgentRoleType.Agent, AgentRoleType.Subscritor, AgentRoleType.Facultativo };

        public enum AgentRoleType
        {
            Agent,
            Subscritor,
            Facultativo
        }

        public enum IdentificationType : int
        {
            Other = 0,
            ID = 1,
            Passport = 2,
            DriverLicense = 3,
            CompanyRegistration = 5,
            BirthCertificate = 6
        };

        public enum ReviewGroups
        {
            InformacionesGenerales = 1,
            TipoCombustible,
            VerificarFuncionamientoVehiculo,
            VerificarPartesFisicasVehiculo,
            AccesoriosTapiceria,
            SistemasSeguridadComplementos,
            Fotos,
            OtrasInformaciones
        }

        public enum TransmissionType
        {
            Automatica = 1,
            Mecanica,
            Sequencial
        }

        public enum BusinessLine
        {
            Life = 1,
            Vehicle,
            Health,
            IncendioLineasAliadas
        }

        public enum StatisticsReportType
        {
            StatisticsReportEmission = 1,
            StatisticsReportPerformance,
            StatisticsReportInclusions = 3,
            StatisticsReportExclusions = 4
        }

        public enum StatisticsViewBy
        {
            Producto = 1,
            Oficina = 2,
            CanalVenta = 5
        }

        public enum StatisticsPerformanceViewBy
        {
            Suscriptor = 0,
            Inspector
        }

        public enum StatisticsEmisionesViewBy
        {
            Count,
            AnnualPremium,
            Insured_Amount,
            Rate,
            VehicleCount
        }

        public enum VIFSessionCount
        {
            InformacionesGenerales,
            VerificacionDatosGenerales,
            Combustible,
            Funcionamiento,
            PartesFisicas,
            AccesoriosTapiceria,
            SeguridadComplementos,
            OtrasInformaciones,
            Fotos
        }

        public enum PropertyInspectionFormSections
        {
            DatosGenerales,
            SumasAseguradas,
            Descripcion,
            HistorialPerdidas,
            HistorialPerdidasNingunaAnteriores,
            SiniestralidadZona,
            Colindancias,
            LocalizacionRiesgo,
            DescripcionProcesos,
            DescripcionPeligros,
            PrevencionProteccion,
            EstimacionPerdidasCoberturaIncendio,
            ExposicionRiesgos,
            CategoriaRiesgo,
            OpinionRiesgo,
            RecomendacionesTecnicas,
            RecomendacionesHechasEnviadasAsegurado,
            Fotografias
        }

        public enum PropertyStatus
        {
            NA = 1,
            Active,
            Inactive
        }

        public enum Country
        {
            RepublicaDominicana = 129,
            ElSalvador = 150
        }

        public enum AdditionalType
        {
            Cristal,
            Objeto,
            Electronico,
            EquipoContratista,
            AveriaDeMaquinaria,
            ResponsabilidadCivilCentrosMedicos
        }

        public enum TranportAdditional
        {
            Land,
            Maritime,
            Air
        }
        public enum ChangeConditionCatalog
        {
            Color = 5,
            Chasis = 6,
            NoRegistro = 7
        }

        #endregion

        #region Items DropDown

        public class ItemAcuerdos
        {
            public int Cuota { get; set; }
            public System.DateTime FechaCuota { get; set; }
            public decimal MontoCuota { get; set; }
            public decimal PorcInicial { get; set; }
        }

        public class ItemGpCustomer
        {
            public string CustClass { get; set; }
            public string Custname { get; set; }
            public string Custnmbr { get; set; }
            public int Inactive { get; set; }
        }

        public class GpItemfrecuencyTypes
        {
            public List<string> frecuency { get; set; }
        }

        public class GpItemCardTypes
        {
            public List<string> CARDNAME { get; set; }
        }

        public class EndorsementSysFlex
        {
            public int Codigo { get; set; }
            public string NombreCliente { get; set; }
            public string RNC { get; set; }
        }

        public class ItemEndorsement
        {
            public int EndorsementId { get; set; }
            public string EndorsementNameKey { get; set; }
            public string Rnc { get; set; }
            public string ContactName { get; set; }
            public string ContactTel { get; set; }
            public string ContactTypeIdDesc { get; set; }
            public string ContactAdress { get; set; }
        }

        [Serializable]
        public class PolicyKey
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
        }

        [Serializable]
        public class ContractKey
        {
            public int? IdCompania { get; set; }
            public string NombreCompania { get; set; }
            public int RecordIndex { get; set; }
            public int? IdContrato { get; set; }
            public string NombreContrato { get; set; }
            public decimal MontoContrato { get; set; }
            public decimal PorcCommision { get; set; }
            public DateTime FechaLimitePago { get; set; }
        }

        public class MyGenericTemplate : ITemplate
        {
            Dictionary<string, string> _AttrList;
            Utility.WebControlType _controlType;
            string _IdControl;
            string _CssClass;
            string _TextValue;
            bool _IsReadOnly;
            bool _WithHiddenField;
            string _CommandName;

            public MyGenericTemplate(Dictionary<string, string> Attr, Utility.WebControlType controlType, string IdControl, string CssClass, string TextValue, bool IsReadOnly = false, bool WithHiddenField = false, string CommandName = "")
            {
                _AttrList = Attr;
                _controlType = controlType;
                _IdControl = IdControl;
                _CssClass = CssClass;
                _TextValue = TextValue;
                _IsReadOnly = IsReadOnly;
                _CommandName = CommandName;
                _WithHiddenField = WithHiddenField;
            }

            public void InstantiateIn(Control container)
            {
                TextBox ctxt;
                Literal cLt;
                CheckBox chk;
                Button btn;

                switch (_controlType)
                {
                    case Utility.WebControlType.TextBox:
                        ctxt = new TextBox();
                        ctxt.ID = _IdControl;
                        ctxt.Text = string.IsNullOrEmpty(_TextValue) ? "0.00" : _TextValue;
                        ctxt.CssClass = _CssClass;
                        ctxt.ReadOnly = _IsReadOnly;
                        foreach (var item in _AttrList)
                            ctxt.Attributes.Add(item.Key, item.Value);
                        container.Controls.Add(ctxt);
                        if (_WithHiddenField)
                        {
                            var hdn = new HiddenField();
                            hdn.ID = "hdn" + _IdControl;
                            container.Controls.Add(hdn);
                        }
                        break;
                    case Utility.WebControlType.Literal:
                        cLt = new Literal();
                        container.Controls.Add(cLt);
                        break;
                    case Utility.WebControlType.CheckBox:
                        chk = new CheckBox();
                        container.Controls.Add(chk);
                        break;
                    case Utility.WebControlType.Button:
                        btn = new Button();
                        btn.ID = _IdControl;
                        btn.CommandName = _CommandName;
                        btn.CssClass = _CssClass;
                        foreach (var item in _AttrList)
                            btn.Attributes.Add(item.Key, item.Value);
                        container.Controls.Add(btn);
                        break;
                }
            }
        }

        public class MyColumn
        {
            public string Name { get; set; }
            public string Caption { get; set; }
            public object value { get; set; }
            public string FieldName { get; set; }
        }

        [Serializable]
        public class Contrato
        {
            public RecordStatus Status { get; set; }
            public bool isLocal { get; set; }
            public int corpId { get; set; }
            public long contractUniqueId { get; set; }
            public int? IdCompania { get; set; }
            public string NombreCompania { get; set; }
            public int RecordIndex { get; set; }
            public int? IdContrato { get; set; }
            public string NombreContrato { get; set; }
            public string NameKeyContrato { get; set; }
            public decimal MontoContrato { get; set; }
            public decimal PorcCommision { get; set; }
            public string MontoContratoF { get; set; }
            public string PorcCommisionF { get; set; }
            public DateTime FechaLimitePago { get; set; }
            public string FechaLimitePagoF { get; set; }
            public decimal PorcContrato { get; set; }
        }

        [Serializable]
        public class CoverageDetail
        {
            public RecordStatus Status { get; set; }
            public bool isLocal { get; set; }
            public string CoverageName { get; set; }
            public decimal ContractualPercentage { get; set; }
            public string CoverageTypeDesc { get; set; }
            public int CoverageCorpId { get; set; }
            public int CoverageRegionId { get; set; }
            public int CoverageCountryId { get; set; }
            public long UniqueId { get; set; }
            public int BlTypeId { get; set; }
            public int BlId { get; set; }
            public int ProductId { get; set; }
            public int VehicleTypeId { get; set; }
            public int GroupId { get; set; }
            public int CoverageTypeId { get; set; }
            public int CoverageId { get; set; }
            public int? IdCompania { get; set; }
            public string NombreCompania { get; set; }
            public int RecordIndex { get; set; }
            public int? IdContrato { get; set; }
            public string NombreContrato { get; set; }
            public decimal MontoContrato { get; set; }
            public decimal PorcCommision { get; set; }
        }

        [Serializable]
        public class KeyCoverageContract : PolicyKey
        {
            public int CoverageCorpId { get; set; }
            public int CoverageRegionId { get; set; }
            public int CoverageCountryId { get; set; }
            public int UniqueId { get; set; }
            public int BlTypeId { get; set; }
            public int BlId { get; set; }
            public int ProductId { get; set; }
            public int VehicleTypeId { get; set; }
            public int GroupId { get; set; }
            public int CoverageTypeId { get; set; }
            public int CoverageId { get; set; }
            public int ContratctId { get; set; }
            public int CompanyContractId { get; set; }
            public int ContractCoverageId { get; set; }
            public string NameKeyContrato { get; set; }
            public string NombreContrato { get; set; }
            public decimal MontoContrato { get; set; }
            public decimal PorcCommision { get; set; }
            public DateTime? FechaLimitePago { get; set; }
        }

        [Serializable]
        public class KeyItem
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int UniqueId { get; set; }
            public decimal InsuredAmount { get; set; }
            public decimal ReinsuranceAmount { get; set; }
            public decimal ReinsurancePercentage { get; set; }
            public decimal ReinsurancePremiumAmount { get; set; }
            public string IdGrid { get; set; }
        }

        public class itemGp
        {
            public DateTime pPaymentDate { get; set; }
            public decimal pAmount { get; set; }
            public int DetailId { get; set; }
            public String currencyCode { get; set; }
            public string AbaNumber { get; set; }
            public string BankAccountNumber { get; set; }
            public int BankAccountType { get; set; }
            public string BankAccountHolder { get; set; }
            public string AccountNumber { get; set; }
            public string AccountName { get; set; }
            public string transactionNumber { get; set; }
            public int PaymentTypeId { get; set; }
            public string PolicyNo { get; set; }
            public string UserName { get; set; }
            public Utility.PaymentSourceType PaymentSourceType { get; set; }
        }

        public class rateJsonSysFlex
        {
            public string anoVehiculo { get; set; }
            public string beneficiarioEndoso { get; set; }
            public int? cantidadMeses { get; set; }
            public string capacidad { get; set; }
            public string categoria { get; set; }
            public object chasis { get; set; }
            public int? codigoTarifa { get; set; }
            public string color { get; set; }
            public int? compania { get; set; }
            public string correoContactoBeneficiarioEndoso { get; set; }
            public decimal? cotizacion { get; set; }
            public string deducible { get; set; }
            public string estacionaEn { get; set; }
            public string estatus { get; set; }
            public DateTime? fechaFin { get; set; }
            public DateTime? fechaInicio { get; set; }
            public string formadePago { get; set; }
            public int? idTipoVehiculo { get; set; }
            public int? idMarcaVehiculo { get; set; }
            public int? idModeloVehiculo { get; set; }
            public int? idVersion { get; set; }
            public int? idAnoVehiculo { get; set; }
            public int? idColor { get; set; }
            public int? idCapacidad { get; set; }
            public int? idUso { get; set; }
            public int? idEstacionaEn { get; set; }
            public int? iddeducible { get; set; }
            public int? kilomatraje { get; set; }
            public string marcaVehiculo { get; set; }
            public string modeloVehiculo { get; set; }
            public decimal? montoAsegurado { get; set; }
            public decimal? montoDescuento { get; set; }
            public decimal? montoImpuesto { get; set; }
            public decimal? montoRecargo { get; set; }
            public decimal? neto { get; set; }
            public string noFormulario { get; set; }
            public string nombreContactoBeneficiarioEndoso { get; set; }
            public string placa { get; set; }
            public decimal? porcDescuento { get; set; }
            public decimal? porcImpuesto { get; set; }
            public decimal? porcRecargo { get; set; }
            public decimal? porciendoCobertura { get; set; }
            public decimal? primaBruta { get; set; }
            public int? ramo { get; set; }
            public string renovacionAutomatica { get; set; }
            public string rncBeneficiarioEndoso { get; set; }
            public int? secuencia { get; set; }
            public string sexoEdad { get; set; }
            public int? subRamo { get; set; }
            public decimal? tasa { get; set; }
            public string telefonoContactoBeneficiarioEndoso { get; set; }
            public string tipoVehiculo { get; set; }
            public string uso { get; set; }
            public string usuario { get; set; }
            public decimal? valorEndoso { get; set; }
            public string version { get; set; }
            public decimal PorcientoRecargoVentas { get; set; }
            public bool licenciaExtranjera { get; set; }

            public int idTipoCombustible { get; set; }
            public string tipoCombustible { get; set; }
        }

        public class SysflexComboCondicion
        {
            public int? Ramo { get; set; }
            public int? SubRamo { get; set; }
            public decimal? SecuenciaCondicion { get; set; }
            public string NombreArchivo { get; set; }
            public int? Codigo { get; set; }
            public string Descripcion { get; set; }
            public decimal? Porciento { get; set; }
            public decimal? Prima { get; set; }
            public int? Reaseguro { get; set; }
        }

        public class itemMessage
        {
            public string ErrorType { get; set; }
            public string Field { get; set; }
            public string Field2 { get; set; }
            public string Length { get; set; }
            public string Date { get; set; }
            public string MaximumVal { get; set; }
            public string MinimumVal { get; set; }
        }

        public class itemSelectContact
        {
            public int currentContact { get; set; }
            public int? NewContact { get; set; }
        }

        public class itemDeductible
        {
            public int DeductibleTypeId { get; set; }
            public int DeductibleCategoryId { get; set; }
        }

        public class itemInsuredType
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int ContactId { get; set; }
            public int ContactRoleTypeId { get; set; }
        }

        public class itemABANumber
        {
            public string BankDesc { get; set; }
            public string AbaNumber { get; set; }
        }

        public class itemOccupation
        {
            public string OccupationGroupDesc { get; set; }
            public int OccupationGroupId { get; set; }
            public string description { get; set; }
            public int value { get; set; }
        }

        public class itemOccupationType
        {
            public string description { get; set; }
            public int value { get; set; }
        }


        public class itemEndorsementBeneficiary
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }

        public class itemAgent
        {
            public string description { get; set; }
            public int value { get; set; }
        }

        public class ListTabError
        {
            public String Policy { get; set; }
            public String Errors { get; set; }
        }

        public class itemDocument
        {
            public int ContactId { get; set; }
            public string DocumentDesc { get; set; }
            public int? RequirementCatId { get; set; }
            public int? RequirementTypeId { get; set; }
            public int? RequirementId { get; set; }
            public string key { get; set; }
        }

        public class DocumentItem
        {
            public string DocumentDesc { get; set; }
            public int DocumentID { get; set; }
            public int DocumentCategporyID { get; set; }
            public int DocumentTypeID { get; set; }
            public bool isReview { get; set; }
            public string key { get; set; }
        }

        public class Errors
        {
            public Errors() { _MessageErrorList = new List<string>(); }

            private List<string> _MessageErrorList;

            public String Policy { get; set; }

            public List<string> MessageErrorList
            {
                get { return _MessageErrorList; }
            }
        }

        public class YearsItem
        {
            public Int32 Year { get; set; }
            public string YearDescription { get; set; }
        }

        public class itemOfficce
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int AgentId { get; set; }
        }

        public class itemOfficceWithoutAgent
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
        }

        public class itemPolicy
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
        }

        public class provider
        {
            public int? ProviderTypeId { get; set; }
            public int? ProviderId { get; set; }
            //public string ProviderName { get; set; }
            public string ElementDesc { get; set; }
        }

        public class StateProvince
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
        }

        public class Municipaly
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
        }

        public class KeyInvestProfile
        {
            public int CorpId { get; set; }
            public int CurrencyId { get; set; }
            public int ProfileTypeId { get; set; }
            public bool Modifiable { get; set; }
        }

        /// <summary>
        /// Para llenar el Dropdown de los Tipos de Compañías en el tab de Beneficiarios
        /// </summary>
        public class CompanyType
        {
            public Int32 OccupationId { get; set; }
            public Int32 OccupGroupTypeId { get; set; }
        }

        public class itemRelationship
        {
            public int? RelationshipId { get; set; }
            public string NameKey { get; set; }
        }

        public class itemProduct
        {
            public int CorpId { get; set; }

            public int CountryId { get; set; }

            public int BlId { get; set; }
            public int ProductId { get; set; }

            public int RegionId { get; set; }
            public int BlTypeId { get; set; }
            public int ProductTypeId { get; set; }
            public string NameKey { get; set; }

            public ProductBehavior Product
            {
                get
                {
                    var Product = (ProductBehavior)Enum.Parse(typeof(ProductBehavior), NameKey.Replace("-", ""), true);
                    return Product;
                }
            }
        }

        public class ProfileType
        {
            public int CorpId { get; set; }
            public int CurrencyId { get; set; }
            public int ProfileTypeId { get; set; }


        }

        public class FamilyProduct
        {
            public int CorpId { get; set; }
            public int BlTypeId { get; set; }
            public int CountryId { get; set; }
            public int ProductTypeId { get; set; }

            public int BlId { get; set; }

            public int RegionId { get; set; }
        }

        public struct RequirementsValues
        {
            public int ID { get; set; }
            public int CategoryID { get; set; }
            public int TypeID { get; set; }
            public int ContactID { get; set; }
            public int? DocId { get; set; }
            public bool HasDocument { get; set; }

            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }

            public int CityId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int OfficeId { get; set; }
        }

        public class PolicyContactByRole
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }

            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int ContactId { get; set; }
            public int ContactRoleTypeId { get; set; }
            public string RoleDesc { get; set; }
        }

        public class ddlEntityTypeItem
        {
            public int OccupationId { get; set; }
            public int OccupGroupTypeId { get; set; }
        }

        public class PaymentSource
        {
            public int PaymentSourceTypeId { get; set; }
            public int PaymentSourceId { get; set; }
            public int PaymentControlId { get; set; }
        }

        #endregion

        #region Items Class


        public class OverPremiumResult
        {
            public Boolean? Result { get; set; }
            public decimal? Percent { get; set; }
        }

        [Serializable]
        public class InvoiceData
        {
            public string FacturaNumero { get; set; }
            public string Poliza { get; set; }
            public DateTime? Fecha { get; set; }
            public string Ncf { get; set; }
            public string Concepto { get; set; }
            public decimal? Valor { get; set; }
            public decimal? ValorItbis { get; set; }
            public string NombreCliente { get; set; }
            public string Direccion { get; set; }
            public string NombreVendedor { get; set; }
            public string CodigoVendedor { get; set; }
            public string NombreSupervisor { get; set; }
            public string TipoComprobante { get; set; }
            public string cedulaCliente { get; set; }
            public string TelRes { get; set; }
            public string TelOfic { get; set; }
            public string TelCel { get; set; }
            public string CodigoSupervisor { get; set; }
            public string CodigoAgente { get; set; }
            public string DireccionAgente { get; set; }
            public string VigenciaDesde { get; set; }
            public string VigenciaHasta { get; set; }
            public string Oficina { get; set; }
            public string Ramo { get; set; }
            public string Producto { get; set; }
            public decimal SumaAsegurada { get; set; }
            public decimal PrimaNeta { get; set; }
            public decimal ISC { get; set; }
            public decimal TotalAPagar { get; set; }
            public string CodigoCliente { get; set; }
            public string UserName { get; set; }
        }

        public class ExclusionResult
        {
            public int Compania { get; set; }
            public long Cotizacion { get; set; }
            public string Estatus { get; set; }
            public string Mensaje { get; set; }
            public int MesesVigencia { get; set; }
            public string Nuevo { get; set; }
            public int Secuencia { get; set; }
            public int SecuenciaMov { get; set; }
            public int ExtensionData { get; set; }
        }

        public class itemProjectionPayment
        {
            public int Numero { get; set; }
            public decimal Inicial { get; set; }
            public string Cuotas { get; set; }
        }

        public class CLoanResult : GlobalService.ResultCode
        {
            public string LoanNo { get; set; }
        }
        public class ItemRequirement
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int ContactId { get; set; }
            public int RequirementCatId { get; set; }
            public int RequirementTypeId { get; set; }
            public int RequirementId { get; set; }
            public int userId { get; set; }
            public int SelectedInsuredVehicleId { get; set; }
        }

        public class ContactParameter
        {
            public string Email { get; set; }
            public string Direccion { get; set; }
            public string TelefonoCasa { get; set; }
            public string TelefonoTrabajo { get; set; }
            public string TelefonoCelular { get; set; }
            public short? tipoCedula { get; set; }
            public string CedulaRncOther { get; set; }
            public Contact.Address oAddress { get; set; }
            public IEnumerable<Contact.Phone> oPhones { get; set; }
        }

        public class GPResultGeNextNCF
        {
            public string InvoiceNumber { get; set; }
            public string Error { get; set; }
            public string NCFNumber { get; set; }
            public bool Successful { get; set; }
        }

        public class GPResultGetNextCreditNCF
        {
            public string CreditNumber { get; set; }
            public string Error { get; set; }
            public string NCFNumber { get; set; }
            public bool Successful { get; set; }
            public int ExtensionData { get; set; }
        }

        [Serializable]
        public class CumplimientoItem
        {
            public int? finalBeneficiaryOptionId { get; set; }
            public int? pepFormularyOptionId { get; set; }
            public int? companyStructureId { get; set; }
            public int? companyActivityId { get; set; }
        }

        public class inclusionResult
        {
            public object CantidadMarbetes { get; set; }
            public int CantidadMeses { get; set; }
            public object CodigoTarifa { get; set; }
            public int Compania { get; set; }
            public decimal Cotizacion { get; set; }
            public string Estatus { get; set; }
            public DateTime FechaAdiciona { get; set; }
            public DateTime FechaFinVigencia { get; set; }
            public object FechaImpMarbetes { get; set; }
            public DateTime FechaInicioVigencia { get; set; }
            public DateTime FechaModifica { get; set; }
            public string FormadePago { get; set; }
            public int ImpuestoBomberos { get; set; }
            public double MontoAsegurado { get; set; }
            public object MontoDescuento { get; set; }
            public double MontoImpuesto { get; set; }
            public double MontoMov { get; set; }
            public object MontoRecargo { get; set; }
            public double Neto { get; set; }
            public string NoFormulario { get; set; }
            public double PorcComision { get; set; }
            public double PorcDescuento { get; set; }
            public double PorcImpuesto { get; set; }
            public double PorcRecargo { get; set; }
            public double PrimaBruta { get; set; }
            public int Ramo { get; set; }
            public string RenovacionAutomatica { get; set; }
            public int Secuencia { get; set; }
            public int SecuenciaMov { get; set; }
            public int SubRamo { get; set; }
            public double Tasa { get; set; }
            public string UsuarioAdiciona { get; set; }
            public string UsuarioModifica { get; set; }
        }

        public class itemSecuenciaMov
        {
            public int SecuenciaMov { get; set; }
        }

        public class VehicleInclusion
        {
            public string Ano { get; set; }
            public long Cotizacion { get; set; }
            public string Deducible { get; set; }
            public string DescripcionSubramo { get; set; }
            public string Estacionamiento { get; set; }
            public string EstatusItem { get; set; }
            public string EstatusPoliza { get; set; }
            public DateTime FechaFin { get; set; }
            public string IdEstacionamiento { get; set; }
            public string IdMarca { get; set; }
            public string Idano { get; set; }
            public string Iddeducible { get; set; }
            public string Idmodelo { get; set; }
            public string Idtipovehiculo { get; set; }
            public string Iduso { get; set; }
            public int? Item { get; set; }
            public string Marca { get; set; }
            public string Modelo { get; set; }
            public string Poliza { get; set; }
            public int? SubRamo { get; set; }
            public string TipoVehiculo { get; set; }
            public string Uso { get; set; }
            public double? ValorVehiculo { get; set; }
            public string chasis { get; set; }
            public string color { get; set; }
            public string placa { get; set; }
            public int? ramo { get; set; }
        }

        public class VehicleExclusion : VehicleInclusion
        {

        }

        public class itemCounter
        {
            public int CorpId { get; set; }
            public int? AgentId { get; set; }
            public int CompanyId { get; set; }
            public DateTime? DateTo { get; set; }
            public DateTime? DateFrom { get; set; }
            public int? OfficeId { get; set; }
            public int? BlId { get; set; }
            public string Bandeja { get; set; }
            public int? UserId { get; set; }
            public bool FilterAgent { get; set; }
        }

        public class TabsCounter
        {
            public string Tab { get; set; }
            public Int64 Count { get; set; }
        }

        public class PolicyAndContactInfo
        {
            public class ContactData
            {
                public string Email { get; set; }
                public string Direccion { get; set; }
                public string TelefonoCasa { get; set; }
                public string TelefonoCelular { get; set; }
                public string TelefonoTrabajo { get; set; }
                public string CedulaRncOther { get; set; }
                public DateTime? ExpirationDate { get; set; }
                public int TipoCedula { get; set; }
                public int CurrencyId { get; set; }
                public string Sexo { get; set; }
            }

            public class PolicyInfo
            {
                public Policy policy { get; set; }
                public decimal TasaCalc { get; set; }
                public decimal AnnualPremium { get; set; }
                public decimal PrimaTotal { get; set; }
                public oSysFlexService.UtilityNCFType NCFType { get; set; }
                public int Oficina { get; set; }
                public string FrequenciaPago { get; set; }
                public string PolicyNo { get; set; }
                public int Intermediario { get; set; }
            }
        }

        public class PaymentAgreementSV
        {
            public int? Cuota { get; set; }
            public DateTime? FechaCuota { get; set; }
            public decimal? ValorCuota { get; set; }
            public decimal? Porciento { get; set; }
        }

        public class CoverageIL
        {
            public int CurrencyId { get; set; }
            public decimal UnitaryPrice { get; set; }
            public decimal PackagePrice { get; set; }
            public decimal? DeductibleAmount { get; set; }
            public decimal? DeductiblePercentage { get; set; }
            public decimal? ManualDeductibleAmount { get; set; }
            public decimal? ManualDeductiblePercentage { get; set; }
            public decimal? CoverageLimit { get; set; }
            public bool CoverageStatus { get; set; }
            public int CorpId { get; set; }
            public long UniqueId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int BlTypeId { get; set; }
            public int BlId { get; set; }
            public int ProductId { get; set; }
            public int VehicleTypeId { get; set; }
            public int GroupId { get; set; }
            public int CoverageTypeId { get; set; }
            public int CoverageId { get; set; }
            public string CoverageTypeDesc { get; set; }
            public string GroupDesc { get; set; }
            public string CoverageDesc { get; set; }
            public int Ramo { get; set; }
            public int SubRamo { get; set; }
            public decimal? CoveragePercentage { get; set; }
            public decimal? PremiumPercentage { get; set; }
            public decimal? CoinsurancePercentage { get; set; }
        }

        public class OfficeMatchWS
        {
            public int? OfficeIdGlobal { get; set; }
            public int? OfficeIdSysFlex { get; set; }
            public string OfficeDesc { get; set; }
        }

        [Serializable]
        public class DetailPilot
        {
            public bool isLocal { get; set; }
            public string Name { get; set; }
            public int? Flighthours { get; set; }
            public string FlighthoursF { get; set; }
            public bool AirplanePilotStatus { get; set; }
            public int CorpId { get; set; }
            public long UniqueAirplaneId { get; set; }
            public int SeqId { get; set; }
            public int UserId { get; set; }
        }

        [Serializable]
        public class DetailFeature
        {
            public RecordStatus Status { get; set; }
            public bool isLocal { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string SerialKey { get; set; }
            public Nullable<int> Year { get; set; }
            public Nullable<decimal> Value { get; set; }
            public Nullable<int> Quantity { get; set; }
            public int CorpId { get; set; }
            public long UniquePropertyId { get; set; }
            public int SeqId { get; set; }
            public string Description { get; set; }
            public string Capacity { get; set; }
            public string Author { get; set; }
            public Nullable<int> PositionId { get; set; }
            public Nullable<int> CertificateId { get; set; }
            public Nullable<int> MeasureTypeId { get; set; }
            public string Height { get; set; }
            public string Width { get; set; }
            public Nullable<decimal> Deductible { get; set; }
            public Nullable<decimal> MinimumDeductible { get; set; }
            public int FeaturePropertyStatusId { get; set; }

            public string MeasureTypeDesc { get; set; }
            public string PositionDesc { get; set; }
            public string CertificateDesc { get; set; }

            public string ValueF { get; set; }
            public string QuantityF { get; set; }
            public string MinimumDeductibleF { get; set; }
            public string DeductibleF { get; set; }
        }

        public class Property
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int PropertyId { get; set; }
            public int SeqId { get; set; }
            public Int64 UniquePropertyId { get; set; }
            public int InsurableTypeId { get; set; }
            public string InsurableTypeDesc { get; set; }
            public int InsuredDetailTypeId { get; set; }
            public string InsuredDetailTypeDesc { get; set; }
            public int EquipmentTypeId { get; set; }
            public string EquipmentTypeDesc { get; set; }
            public int ConditionId { get; set; }
            public string ConditionDesc { get; set; }
            public int BlTypeId { get; set; }
            public int BlId { get; set; }
            public int ProductId { get; set; }
            public string ProductDesc { get; set; }
            public int? ProductTypeId { get; set; }
            public string ProductTypeDesc { get; set; }
            public int? Quantity { get; set; }
            public string Placement { get; set; }
            public string Height { get; set; }
            public string Width { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string SerialKey { get; set; }
            public decimal? Value { get; set; }
            public decimal? ReplacementValue { get; set; }
            public DateTime? ConstructionDateStart { get; set; }
            public DateTime? ConstructionDateEnd { get; set; }
            public decimal? PercentageOfCompletion { get; set; }
            public decimal? EquipmentValue { get; set; }
            public int? MarketExperience { get; set; }
            public string Capacity { get; set; }
            public string Maker { get; set; }
            public string Author { get; set; }
            public int? EmployeesQty { get; set; }
            public DateTime? WorkStartDate { get; set; }
            public DateTime? WorkEndDate { get; set; }
            public int? Experience { get; set; }
            public decimal? CompletionPercentage { get; set; }
            public DateTime? InsuredDate { get; set; }
            public decimal InsuredAmount { get; set; }
            public string InsuredAmountF { get; set; }
            public decimal Rate { get; set; }
            public decimal PremiumAmount { get; set; }
            public string PremiumAmountF { get; set; }
            public decimal BasePremiumAmount { get; set; }
            public decimal DeductiblePercentage { get; set; }
            public string DeductiblePercentageF { get; set; }
            public decimal DeductibleAmount { get; set; }
            public decimal MinimumDeductibleAmount { get; set; }
            public bool RequiresInspection { get; set; }
            public bool Inspected { get; set; }
            public string CssClassInspected { get; set; }
            public bool EndorsementClarifying { get; set; }
            public bool Endorsement { get; set; }
            public string EndorsementBeneficiary { get; set; }
            public string EndorsementBeneficiaryRnc { get; set; }
            public decimal? EndorsementAmount { get; set; }
            public string EndorsementContactName { get; set; }
            public string EndorsementContactPhone { get; set; }
            public string EndorsementContactEmail { get; set; }
            public int PropertyDetailStatusId { get; set; }
            public string PropertyDetailStatusDesc { get; set; }
            public string PropertyDetailSourceId { get; set; }
            public int RegionIdLoc { get; set; }
            public int CountryIdLoc { get; set; }
            public int DomesticregIdLoc { get; set; }
            public Int64 CoaseguroRobo { get; set; }
            public int StateProvIdLoc { get; set; }
            public int CityIdLoc { get; set; }
            public string RegionDescLoc { get; set; }
            public string CountryDescLoc { get; set; }
            public string DomesticregDescLoc { get; set; }
            public string StateProvDescLoc { get; set; }
            public string CityDescLoc { get; set; }
            public int BusinessTypeId { get; set; }
            public string BusinessTypeDesc { get; set; }
            public int PropertyBuildTypeId { get; set; }
            public string PropertyBuildTypeDesc { get; set; }
            public int ActivfityTypeId { get; set; }
            public string ActivfityTypeDesc { get; set; }
            public int ReinsuranceId { get; set; }
            public string ReinsuranceDesc { get; set; }
            public string AddressStreet { get; set; }
            public string addressNumber { get; set; }
            public decimal EvaluationValue { get; set; }
            public string EvaluationValueF { get; set; }
            public decimal EdificationValue { get; set; }
            public string EdificationValueF { get; set; }
            public decimal MachineryValue { get; set; }
            public string MachineryValueF { get; set; }
            public decimal FurnitureAndEquipmentValue { get; set; }
            public string FurnitureAndEquipmentValueF { get; set; }
            public decimal StockValue { get; set; }
            public string StockValueF { get; set; }
            public decimal RemodelingAndFittingValue { get; set; }
            public string RemodelingAndFittingValueF { get; set; }
            public decimal ValueObjectAndArtValue { get; set; }
            public string ValueObjectAndArtValueF { get; set; }
            public int? Rooms { get; set; }
            public int? Bathrooms { get; set; }
            public int? LocationActivityTypeId { get; set; }
            public string Registry { get; set; }
            public decimal? BuildAreaSqFeet { get; set; }
            public decimal? BuildAreaSqMeters { get; set; }
            public string GeographicLimitation { get; set; }
            public int? SouthBorderId { get; set; }
            public string SouthBorderDesc { get; set; }
            public int? NorthBorderId { get; set; }
            public string NorthBorderDesc { get; set; }
            public int? EastBorderId { get; set; }
            public string EastBorderDesc { get; set; }
            public int? WestBorderId { get; set; }
            public string WestBorderDesc { get; set; }
            public string PhysicalAddress { get; set; }
            public bool? Garage { get; set; }
            public bool? Pool { get; set; }
            public string DistanceKilometersSea { get; set; }
            public string DistanceKilometersRiver { get; set; }
            public string DistanceKilometersStream { get; set; }
            public decimal? Longitude { get; set; }
            public decimal? Latitude { get; set; }
            public int PropertyStatusId { get; set; }
            public string PropertyStatusDesc { get; set; }
            public string PropertySourceId { get; set; }
            public int? PropertyDetailYear { get; set; }
            public int? PropertyYear { get; set; }
            public int? UserId { get; set; }
            public string SourceId { get; set; }
            public string ClassEndoso { get; set; }
            public bool surchargeApplied { get; set; }
            public string CssClassEndorsementClarifying { get; set; }
            public bool EndorsementClarifyingVisible { get; set; }
            public decimal? ReinsuranceAmount { get; set; }
            public decimal? ReinsurancePercentage { get; set; }
            public bool AppliesToReinsurance { get; set; }
            public decimal? ReinsurancePremiumAmount { get; set; }
            public string fireProtectionequipment { get; set; }
            public string organization { get; set; }
            public string security { get; set; }
            public string electricsystem { get; set; }
            public string countrydesc { get; set; }
            public string StateProvDesc { get; set; }
            public string MunicipDesc { get; set; }
            public string citydesc { get; set; }
            public string PolicyFomat { get; set; }
            public string CompensationPeriod { get; set; }
            public string GrossProfit { get; set; }
            public string DeductibleInDays { get; set; }
            public string PercentofCoinsuranceAgreed { get; set; }
            public string AddressCountryDesc { get; set; }
            public string AddressStateProvDesc { get; set; }
            public string AddressMunicipioDesc { get; set; }
            public string AddressCityDesc { get; set; }
            public string PolicyNoMain { get; set; }
            public string AddressStreetFull { get; set; }
            /*
              Estas 2 propiedades sigientes solo se usan para activar o no algunos controles del detalle de propiedades (icono ver inspeccion y chexbox de inspeccionado)
             */
            public bool VisibleChkInspection { get; set; }
            public bool VisibleLnkInspeccion { get; set; }
            public string InsideBusiness { get; set; }
            public string OutofLocalBankDeposits { get; set; }
            public string TransportVehicles { get; set; }
            public string ControlMeasures { get; set; }
            public string ControlSecurity { get; set; }
            public string TypeCoverage { get; set; }
            public string Exequatur { get; set; }
            public string GraduationYear { get; set; }
            public string ExequaturRegistrationDate { get; set; }
            public string MinimumDeductible { get; set; }
            public string UniversityStudies { get; set; }
            public string NameCentre { get; set; }
            public string Speciality { get; set; }
            public decimal? ProjectAmount { get; set; }
            public string ProjectType { get; set; }
            public string ConstructionMaterial { get; set; }
            public string TypeMachinery { get; set; }
            public string UndergroundFeatures { get; set; }
            public string ProjectAccountWithGuaranteeorDeposit { get; set; }
            public string TheContractorHasContractualRCCoverage { get; set; }
            public string SecurityProtectionDuringConstructionProcess { get; set; }
            public string ProjectHasSubcontractor { get; set; }
            public string ContractorExperienceInSimilarProjects { get; set; }
            public string SubcontractorExperience { get; set; }
            public string RiskofTempest { get; set; }
            public string PenaltyForDelayedProjectDelivery { get; set; }
            public string EquipmentWeight { get; set; }
            public string UseOfMachinery { get; set; }
            public string EquipmentMaintenance { get; set; }
            public string MachineryOperatorExperience { get; set; }
            public string SpecialDangers { get; set; }
            public string Surveillance { get; set; }
            public string TypeofEquipmentGuard { get; set; }
            public string GPS { get; set; }
            public string ProjectDescription { get; set; }
            public string OtherRisks { get; set; }
            public string CatastrophicRisks { get; set; }
            public string CivilLiability { get; set; }

        }


        public class AirPlane
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int AirplaneId { get; set; }
            public int? BlTypeId { get; set; }
            public int? BlId { get; set; }
            public int? ProductId { get; set; }
            public int? ReinsuranceId { get; set; }
            public decimal? ReinsuranceAmount { get; set; }
            public string AirplaneBase { get; set; }
            public string YearProduction { get; set; }
            public string YearProductionEngine { get; set; }
            public string AirportFeatures { get; set; }
            public string LandingState { get; set; }
            public string FuselageFailures { get; set; }
            public string PlaceRefuge { get; set; }
            public string HullMaintenance { get; set; }
            public string BrandModel { get; set; }
            public string HullMaterial { get; set; }
            public string Name { get; set; }
            public string EngineBrandModel { get; set; }
            public string SerialKey { get; set; }
            public string EngineOverhaul { get; set; }
            public string Usage { get; set; }
            public string Year { get; set; }
            public decimal? CoverageLimitMedicalExpensesOnePassenger { get; set; }
            public decimal? CoverageLimitMedicalExpensesAllPassenger { get; set; }
            public decimal? CoverageLimitMedicalExpensesOneCrewman { get; set; }
            public decimal? CoverageLimitMedicalExpensesAllCrewman { get; set; }
            public decimal? InsuredAmount { get; set; }
            public string InsuredAmountF { get; set; }
            public decimal? Rate { get; set; }
            public decimal? PremiumAmount { get; set; }
            public string PremiumAmountF { get; set; }
            public decimal? BasePremiumAmount { get; set; }
            public decimal? DeductiblePercentage { get; set; }
            public string DeductiblePercentageF { get; set; }
            public decimal? DeductibleAmount { get; set; }
            public decimal? MinimumDeductibleAmount { get; set; }
            public bool? IsNew { get; set; }
            public bool? RequiresInspection { get; set; }
            public bool? Reinsurance { get; set; }
            public bool? Inspected { get; set; }
            public bool? EndorsementClarifying { get; set; }
            public bool? Endorsement { get; set; }
            public string EndorsementBeneficiary { get; set; }
            public string EndorsementBeneficiaryRnc { get; set; }
            public decimal? EndorsementAmount { get; set; }
            public string EndorsementContactName { get; set; }
            public string EndorsementContactPhone { get; set; }
            public string EndorsementContactEmail { get; set; }
            public string InspectionAddress { get; set; }
            public int? AirplaneStatusId { get; set; }
            public int? UserId { get; set; }
            public string SourceId { get; set; }
            public decimal? ReinsurancePercentage { get; set; }
            public long UniqueAirplaneId { get; set; }
            public string ClassEndoso { get; set; }
            public bool surchargeApplied { get; set; }
            public bool VisibleChkNew { get; set; }
            public bool VisibleLnkNew { get; set; }
            public string CssClassnew { get; set; }
            public bool AppliesToReinsurance { get; set; }
            public string ProductDesc { get; set; }
            public int? ProductTypeId { get; set; }
            public decimal? ReinsurancePremiumAmount { get; set; }
            public string PolicyNoMain { get; set; }
        }


        public class Navy
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int? NavyId { get; set; }
            public int? BlTypeId { get; set; }
            public int? BlId { get; set; }
            public int? ProductId { get; set; }
            public int? ReinsuranceId { get; set; }
            public decimal? ReinsuranceAmount { get; set; }
            public string Name { get; set; }
            public string BrandModel { get; set; }
            public string YearProduction { get; set; }
            public string YearProductionEngine { get; set; }
            public string SerialKey { get; set; }
            public string BrandEngine { get; set; }
            public string Casco { get; set; }
            public string Purtal { get; set; }
            public string Eslora { get; set; }
            public string Manga { get; set; }
            public string Usage { get; set; }
            public string NavigationLimit { get; set; }
            public string BasePort { get; set; }

            public string PlaceRefuge { get; set; }
            public string Year { get; set; }
            public string PortLoadingType { get; set; }
            public DateTime? InsuredDate { get; set; }
            public decimal? InsuredAmount { get; set; }
            public decimal? Rate { get; set; }
            public decimal? PremiumAmount { get; set; }
            public decimal? BasePremiumAmount { get; set; }
            public decimal? DeductiblePercentage { get; set; }
            public decimal? DeductibleAmount { get; set; }
            public decimal? MinimumDeductibleAmount { get; set; }
            public bool? IsNew { get; set; }
            public bool? RequiresInspection { get; set; }
            public bool? Reinsurance { get; set; }
            public bool? Inspected { get; set; }
            public bool? EndorsementClarifying { get; set; }
            public bool? Endorsement { get; set; }
            public string EndorsementBeneficiary { get; set; }
            public string EndorsementBeneficiaryRnc { get; set; }
            public decimal? EndorsementAmount { get; set; }
            public string EndorsementContactName { get; set; }
            public string EndorsementContactPhone { get; set; }
            public string EndorsementContactEmail { get; set; }
            public string InspectionAddress { get; set; }
            public int? NavyStatusId { get; set; }
            public int? UserId { get; set; }
            public string SourceId { get; set; }
            public long UniqueNavyId { get; set; }
            public decimal? ReinsurancePercentage { get; set; }

            public decimal? ReinsurancePremiumAmount { get; set; }

            public decimal? CoverageLimitMedicalExpensesOnePerson { get; set; }
            public decimal? CoverageLimitMedicalExpensesByEvent { get; set; }
            public decimal? CoverageLimitMedicalExpensesOneCrewMember { get; set; }
            public decimal? CoverageLimitMedicalExpensesAllCrewMember { get; set; }
            public decimal? CoverageLimitPersonalAccidentOnePassengers { get; set; }
            public decimal? CoverageLimitPersonalAccidentAllPassengersByEvent { get; set; }
            public decimal? CoverageLimitPersonalAccidentOneCrewMember { get; set; }
            public decimal? CoverageLimitPersonalAccidentAllCrewMember { get; set; }
            public decimal? CoverageLimitPersonalEffects { get; set; }

            public string ProductDesc { get; set; }
            public int? ProductTypeId { get; set; }
            public string ClassEndoso { get; set; }
            public bool surchargeApplied { get; set; }
            public string useType { get; set; }
            public bool VisibleChkNew { get; set; }
            //public bool VisibleLnkNew { get; set; }
            public string CssClassnew { get; set; }
            public bool AppliesToReinsurance { get; set; }
            public string PremiumAmountF { get; set; }
            public string InsuredAmountF { get; set; }
            public string PolicyNoMain { get; set; }
        }

        [Serializable]
        public class Engine
        {
            public bool isLocal { get; set; }
            public string Action { get; set; }
            public int CorpId { get; set; }
            public long UniqueNavyId { get; set; }
            public int EngineId { get; set; }
            public string EngineType { get; set; }
            public string EngineQty { get; set; }
            public string CapacityHP { get; set; }
            public string Serial { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Year { get; set; }
            public string FuelType { get; set; }
            public int EngineStatusId { get; set; }
            public int usrId { get; set; }
            public string sourceId { get; set; }
        }


        public class Bail
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int? BailId { get; set; }
            public int? BlTypeId { get; set; }
            public int? BlId { get; set; }
            public int? ProductId { get; set; }
            public int? ReinsuranceId { get; set; }
            public decimal? ReinsuranceAmount { get; set; }
            public int? EquipmentQty { get; set; }
            public decimal? Contractamount { get; set; }
            public string Beneficiary { get; set; }
            public string Activity { get; set; }
            public string Businesstype { get; set; }
            public string Bailtype { get; set; }
            public string Percentageinsuredamount { get; set; }
            public string Obligations { get; set; }
            public string ToDepositIn { get; set; }
            public string AddressStreet { get; set; }
            public string AddressNumber { get; set; }
            public int? AddressCountryId { get; set; }
            public int? AddressDomesticregId { get; set; }
            public int? AddressStateProvId { get; set; }
            public int? AddressCityId { get; set; }
            public string HasEndOfTerm { get; set; }
            public DateTime? InsuredDate { get; set; }
            public decimal? InsuredAmount { get; set; }
            public string InsuredAmountF { get; set; }
            public decimal? Rate { get; set; }
            public decimal? PremiumAmount { get; set; }
            public string PremiumAmountF { get; set; }
            public decimal? BasePremiumAmount { get; set; }
            public decimal? DeductiblePercentage { get; set; }
            public decimal? DeductibleAmount { get; set; }
            public decimal? MinimumDeductibleAmount { get; set; }
            public bool? IsNew { get; set; }
            public bool? RequiresInspection { get; set; }
            public bool? Reinsurance { get; set; }
            public bool? Inspected { get; set; }
            public bool? EndorsementClarifying { get; set; }
            public bool? Endorsement { get; set; }
            public string EndorsementBeneficiary { get; set; }
            public string EndorsementBeneficiaryRnc { get; set; }
            public decimal? EndorsementAmount { get; set; }
            public string EndorsementContactName { get; set; }
            public string EndorsementContactPhone { get; set; }
            public string EndorsementContactEmail { get; set; }
            public string InspectionAddress { get; set; }
            public int? BailStatusId { get; set; }
            public int? UserId { get; set; }
            public string SourceId { get; set; }
            public long UniqueBailId { get; set; }
            public string ProductDesc { get; set; }
            public int? ProductTypeId { get; set; }
            public decimal? ReinsurancePercentage { get; set; }
            public decimal? ContractAmount { get; set; }
            public string ReinsuranceDesc { get; set; }
            public string ClassEndoso { get; set; }
            public bool surchargeApplied { get; set; }
            public int Vigency { get; set; }
            public bool AppliesToReinsurance { get; set; }
            public decimal? ReinsurancePremiumAmount { get; set; }
            public string AddressCountryDesc { get; set; }
            public string AddressStateProvDesc { get; set; }
            public string AddressMunicipioDesc { get; set; }
            public string AddressCityDesc { get; set; }
            public string AddressDomesticregDesc { get; set; }
            public string IsBuilding { get; set; }
            public string PolicyNoMain { get; set; }
        }

        [Serializable]
        public class Guarantors
        {
            public RecordStatus Status { get; set; }
            public bool isLocal { get; set; }
            public int? CorpId { get; set; }
            public long? UniqueBailId { get; set; }
            public int? SeqId { get; set; }
            public int IdentificationTypeId { get; set; }
            public string Identification { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public Nullable<int> CountryId { get; set; }
            public Nullable<int> DomesticregId { get; set; }
            public Nullable<int> StateProvId { get; set; }
            public Nullable<int> CityId { get; set; }
            public Nullable<int> NationalityCountryId { get; set; }
            public string RepresentativeName { get; set; }
            public Nullable<int> RepresentativeIdentificationTypeId { get; set; }
            public string RepresentativeIdentification { get; set; }
            public int BaileeStatusId { get; set; }
            public string SourceId { get; set; }
            public string Sector { get; set; }
            public string CityDesc { get; set; }
            public string IdentificationTypeDesc { get; set; }
            public string CountryDesc { get; set; }
            public string NationalityCountryDesc { get; set; }
            public string RepresentativeIdentificationTypeDesc { get; set; }
            public string TipoRiesgoNameKey { get; set; }
            public string FinancialClearance { get; set; }
            public int? MunicipioId { get; set; }
            public string MunicipioDesc { get; set; }
            public string ClassNameTU { get; set; }
            public bool isEnableTU { get; set; }
        }

        public class Transport
        {
            public int CorpId { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int DomesticregId { get; set; }
            public int StateProvId { get; set; }
            public int CityId { get; set; }
            public int OfficeId { get; set; }
            public int CaseSeqNo { get; set; }
            public int HistSeqNo { get; set; }
            public int? TransportId { get; set; }
            public long UniqueTransportId { get; set; }
            public int? BlTypeId { get; set; }
            public int? BlId { get; set; }
            public int? ProductId { get; set; }
            public int? TransportInsuredId { get; set; }
            public int? ReinsuranceId { get; set; }
            public decimal? ReinsuranceAmount { get; set; }
            public string BuninessType { get; set; }
            public string Activity { get; set; }
            public string MerchandasingType { get; set; }
            public string PackagingType { get; set; }
            public decimal? LimitLiability { get; set; }
            public string ValuationClause { get; set; }
            public string Deductible { get; set; }
            public string GeographicalLimitFrom { get; set; }
            public string GeographicalLimitTo { get; set; }
            public string Conveyance { get; set; }
            public string VehicleQty { get; set; }
            public decimal? FreightAmount { get; set; }
            public decimal? TruckAmount { get; set; }
            public decimal? ContainerAmount { get; set; }
            public decimal? ChassisAmount { get; set; }
            public string Transfer { get; set; }
            public string NoRating { get; set; }
            public string Barge { get; set; }
            public string UnknownValue { get; set; }
            public string LowTonnage { get; set; }
            public string ByAge { get; set; }
            public int? AddressCountryId { get; set; }
            public int? AddressDomesticregId { get; set; }
            public int? AddressStateProvId { get; set; }
            public int? AddressCityId { get; set; }
            public string AddressStreet { get; set; }
            public string AddressNumber { get; set; }
            public string VehicleHasInsure { get; set; }
            public string MerchandiseDesc { get; set; }
            public string Security { get; set; }
            public string WayOfTransport { get; set; }
            public string Warehouse { get; set; }
            public DateTime? InsuredDate { get; set; }
            public decimal? InsuredAmount { get; set; }
            public string InsuredAmountF { get; set; }
            public decimal? Rate { get; set; }
            public decimal? PremiumAmount { get; set; }
            public string PremiumAmountF { get; set; }
            public decimal? BasePremiumAmount { get; set; }
            public decimal? DeductiblePercentage { get; set; }
            public decimal? DeductibleAmount { get; set; }
            public decimal? MinimumDeductibleAmount { get; set; }
            public bool? IsNew { get; set; }
            public bool? RequiresInspection { get; set; }
            public bool? Reinsurance { get; set; }
            public bool? Inspected { get; set; }
            public bool? EndorsementClarifying { get; set; }
            public bool? Endorsement { get; set; }
            public string EndorsementBeneficiary { get; set; }
            public string EndorsementBeneficiaryRnc { get; set; }
            public decimal? EndorsementAmount { get; set; }
            public string EndorsementContactName { get; set; }
            public string EndorsementContactPhone { get; set; }
            public string EndorsementContactEmail { get; set; }
            public string InspectionAddress { get; set; }
            public int? TransportStatusId { get; set; }
            public int? UserId { get; set; }
            public string SourceId { get; set; }
            public string ProductDesc { get; set; }
            public Nullable<int> ProductTypeId { get; set; }
            public Nullable<decimal> ReinsurancePercentage { get; set; }
            public string ClassEndoso { get; set; }
            public bool surchargeApplied { get; set; }
            public bool AppliesToReinsurance { get; set; }
            public decimal? ReinsurancePremiumAmount { get; set; }
            public string billingType { get; set; }
            public string AddressCountryDesc { get; set; }
            public string AddressStateProvDesc { get; set; }
            public string AddressMunicipioDesc { get; set; }
            public string AddressCityDesc { get; set; }
            public int? Ramo { get; set; }
            public int? SubRamo { get; set; }
            public string PolicyNoMain { get; set; }
        }

        [Serializable]
        public class TransportExtraInfo
        {
            public RecordStatus Status { get; set; }
            public bool isLocal { get; set; }
            public int? CorpId { get; set; }
            public long? UniqueTransportId { get; set; }
            public int SeqId { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Plate { get; set; }
            public string Vin { get; set; }
            public Nullable<int> Year { get; set; }
            public string SerialKey { get; set; }
            public int TransportExtraInfoStatusId { get; set; }
        }

        public class DataReaseguro
        {
            public int ExtensionData { get; set; }
            public string Descripcion { get; set; }
            public string MaximoReaseguro { get; set; }
            public int SubRamo { get; set; }
            public string Tipoproducto { get; set; }
            public int ramo { get; set; }
        }

        public class Reason
        {
            public string Id { get; set; }
            public string Tipo { get; set; }
            public string Descripcion { get; set; }
            public string NameKey { get; set; }
        }

        public class VehicleIdentification
        {
            public string Policy { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
        }

        public class EndorstmentData
        {
            public string EndorsementBeneficiary { get; set; }
            public string EndorsementBbeneficiaryRnc { get; set; }
            public decimal? EndorsementAmount { get; set; }
            public string EndorsementContactName { get; set; }
            public string EndorsementContactPhone { get; set; }
            public string EndorsementContactEmail { get; set; }
        }

        public class QuotationAmmount
        {
            public decimal MontoDescuento { get; set; }
            public decimal MontoRecargo { get; set; }
            public decimal PorcDescuento { get; set; }
            public decimal PorcRecargo { get; set; }
            public int Ramo { get; set; }
            public int SubRamo { get; set; }
            public string DeducibleDesc { get; set; }
        }

        public class SysflexProductIL
        {
            public int Ramo { get; set; }
            public int SubRamo { get; set; }
            public int Secuencia { get; set; }
            public string Descripcion { get; set; }
        }

        [Serializable]
        public class SysflexProduct
        {
            public int Ramo { get; set; }
            public int SubRamo { get; set; }
            public string DescRamo { get; set; }
            public string DescCobertura { get; set; }
            public string Ruptura { get; set; }
            public int Secuencia { get; set; }
            public string Descripcion { get; set; }
            public decimal? Porciento { get; set; }
            public decimal? Deducible { get; set; }
            public decimal? MinimoDeducible { get; set; }
            public string BaseDeducible { get; set; }
            public decimal? Prima { get; set; }
            public string MontoInformativo { get; set; }
        }

        [Serializable]
        public class SysFlexRate
        {
            public decimal? PrimaTerceros { get; set; }
            public decimal? PrimaServicio { get; set; }
            public decimal? PorcDanosPropios { get; set; }
            public decimal? SumaAsegurada { get; set; }
            public decimal? PrimaDanosPropio { get; set; }
            public decimal? PrimaMinima { get; set; }
            public decimal? PorcDescuentos { get; set; }
            public decimal? MontoDescuentos { get; set; }
            public decimal? PorcRecargos { get; set; }
            public decimal? MontoRecargos { get; set; }
            public int? MesesCotiza { get; set; }
            public int? MesesVigencia { get; set; }
            public decimal? PrimaDiaria { get; set; }
            public int? DiasVigencia { get; set; }
            public int? DiasCotiza { get; set; }
            public decimal? PrimaProrrata { get; set; }
            public decimal? PorcImpuesto { get; set; }
            public decimal? PrimaNeta { get; set; }
            public decimal? MontoImpuesto { get; set; }
            public decimal? PrimaBruta { get; set; }
        }

        public class itemRequestPayApi
        {
            public String TransactionType { get; set; }
            public String CurrencyCode { get; set; }
            public String AcquiringInstitutionCode { get; set; }
            public String MerchantType { get; set; }
            public String MerchantNumber { get; set; }
            public String MerchantNumber_amex { get; set; }
            public String MerchantTerminal { get; set; }
            public String MerchantTerminal_amex { get; set; }
            public String ReturnUrl { get; set; }
            public String CancelUrl { get; set; }
            public String PageLanguaje { get; set; }
            public String OrdenId { get; set; }
            public String TransactionId { get; set; }
            public String Amount { get; set; }
            public String Tax { get; set; }
            public String MerchantName { get; set; }
            public String KeyEncriptionKey { get; set; }
            public String Ipclient { get; set; }
            public String URLPay { get; set; }
            public Double ITBISPorc { get; set; }
            public String Url { get; set; }
        }

        public class itemResponsePayApi
        {
            public String CreditCardNumber { get; set; }
            public String ResponseCode { get; set; }
            public String AuthorizationCode { get; set; }
            public String RetrivalReferenceNumber { get; set; }
            public String OrdenID { get; set; }
            public String TransactionID { get; set; }
        }


        public class ContractCoveragesSysflex
        {
            public int Id { get; set; }
            public string ContratoNombre { get; set; }
            public decimal ContratoMonto { get; set; }
            public decimal ContratoPorcComision { get; set; }
            public int AseguradoraId { get; set; }
            public DateTime FechaDePago { get; set; }
            public int CoberturaId { get; set; }
            public decimal CoberturaPorcComisio { get; set; }
        }


        #endregion

        public struct DateRange
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        private static byte[] key = { };

        private static byte[] IV = { 38, 55, 206, 48, 28, 64, 20, 16 };

        public static decimal ToDecimal(this TextBox txt, decimal? defaultValue = null, bool removeSpecialCharacter = false)
        {
            return ConvertStringToDecimal(txt.Text, defaultValue, removeSpecialCharacter);
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string GetBase64FromImage(byte[] img)
        {
            var result = "";

            if (img != null)
                result = "data:;base64," + Convert.ToBase64String(img);

            return result;
        }

        /// <summary>
        /// Obtiene la imagen a mostrar segun el tipo de riesgo
        /// </summary>
        public static Func<TipoRiesgo, string> GetDescRiesgo = (RiesgoNameKey) =>
        {
            string result;

            switch (RiesgoNameKey)
            {
                case TipoRiesgo.RA:
                    result = Resources.HighRisk;
                    break;
                case TipoRiesgo.RM:
                    result = Resources.MediumRisk;
                    break;
                case TipoRiesgo.RB:
                    result = Resources.LowRisk;
                    break;
                case TipoRiesgo.NONE:
                    result = "N/A";
                    break;
                default:
                    result = "";
                    break;
            }

            return result;
        };

        public static GridViewColumn CreateTemplate(Utility.MyColumn myColumn, Dictionary<string, string> AttrList, Utility.WebControlType controlType, Utility.TemplateType templateType, string IdControl, string CssClass, string TextValue = "", bool IsReadOnly = false, bool WithHiddenField = false, string CommandName = "")
        {
            GridViewColumn result = null;

            if (templateType == Utility.TemplateType.GridViewDataColumn)
            {
                var colItemTemplate = new DevExpress.Web.GridViewDataColumn();
                colItemTemplate.DataItemTemplate = new Utility.MyGenericTemplate(AttrList, controlType, IdControl, CssClass, TextValue, IsReadOnly, WithHiddenField, CommandName);
                colItemTemplate.Name = myColumn.Name;
                colItemTemplate.Caption = myColumn.Caption;
                result = colItemTemplate;
            }
            else
                if (templateType == Utility.TemplateType.GridViewDataTextColumn)
                {
                    var colItemDataTextColumn = new DevExpress.Web.GridViewDataTextColumn();
                    colItemDataTextColumn.Name = myColumn.Name;
                    colItemDataTextColumn.Caption = myColumn.Caption;
                    colItemDataTextColumn.FieldName = myColumn.FieldName;
                    result = colItemDataTextColumn;
                }
                else
                    if (templateType == Utility.TemplateType.GridViewDataCheckColumn)
                    {

                        var colItemTemplate = new DevExpress.Web.GridViewDataCheckColumn();
                        colItemTemplate.DataItemTemplate = new Utility.MyGenericTemplate(AttrList, controlType, IdControl, CssClass, TextValue, IsReadOnly, WithHiddenField, CommandName);
                        colItemTemplate.Name = myColumn.Name;
                        colItemTemplate.Caption = myColumn.Caption;
                        result = colItemTemplate;
                    }


            return
                result;
        }


        public static void HideOrShowColumnGrid(this ASPxGridView GridView, bool visible, string Name)
        {
            //La Columna no existe poner invisible si se agrego al grid
            var ColumnInvisible = GridView.getThisColumn(Name);
            if (ColumnInvisible != null)
                ColumnInvisible.Visible = visible;
        }

        /// <summary>
        /// Obtiene la imagen a mostrar segun el tipo de riesgo
        /// </summary>
        public static Func<TipoNivelRiesgo, string> GetImgNivelRiesgo = (RiesgoNameKey) =>
        {
            string result;

            switch (RiesgoNameKey)
            {
                case TipoNivelRiesgo.RIESGOALTO:
                    result = "~/Content/images/equis.png";
                    break;
                case TipoNivelRiesgo.RIESGOMEDIO:
                    result = "~/Content/images/cotejo-am.png";
                    break;
                case TipoNivelRiesgo.RIESGOBAJO:
                    result = "~/Content/images/cotejo.png";
                    break;
                default:
                    result = "~/Content/images/Question.png";
                    break;
            }

            return result;
        };

        /// <summary>
        /// Obtiene la imagen a mostrar segun el tipo de riesgo
        /// </summary>
        public static Func<TipoRiesgo, string> GetImgRiesgo = (RiesgoNameKey) =>
        {
            string result;

            switch (RiesgoNameKey)
            {
                case TipoRiesgo.RA:
                    result = "~/Content/images/equis.png";
                    break;
                case TipoRiesgo.RM:
                    result = "~/Content/images/cotejo-am.png";
                    break;
                case TipoRiesgo.RB:
                    result = "~/Content/images/cotejo.png";
                    break;
                case TipoRiesgo.NONE:
                    result = "~/Content/images/Question.png";
                    break;
                default:
                    result = "";
                    break;
            }

            return result;
        };

        public static string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }

        /// <summary>
        /// 
        /// </summary>
        public static Func<BlackListType, string> GetDescBlackList = (BlNameKey) =>
        {
            string result;

            switch (BlNameKey)
            {
                case BlackListType.MA:
                    result = Resources.Match;
                    break;
                case BlackListType.NM:
                    result = Resources.NoMatch;
                    break;
                case BlackListType.NONE:
                    result = "N/A";
                    break;
                default:
                    result = "";
                    break;
            }

            return result;
        };

        public static Func<BlackListType, string> GetImgBlackList = (BlNameKey) =>
        {
            string result;

            switch (BlNameKey)
            {
                case BlackListType.MA:
                    result = "~/Content/images/equis.png";
                    break;
                case BlackListType.NM:
                    result = "~/Content/images/cotejo.png";
                    break;
                case BlackListType.NONE:
                    result = "~/Content/images/Question.png";
                    break;
                default:
                    result = "";
                    break;
            }

            return result;
        };

        public static string[] CallBackList = new[] { "APPLYFILTER", "APPLYCOLUMNFILTER", "APPLYHEADERCOLUMNFILTER", "SORT", "COLUMNMOVE", "FILTERROWMENU", "PAGERONCLICK" };

        public static PaymentSourceType GetPaymentSourceType(int Payment_Source_Type_Id, int Payment_Source_Id)
        {
            PaymentSourceType result = PaymentSourceType.Other;
            /*
             Payment_Source_Type_Id	Payment_Source_Id	Payment_Source_Desc
                 1	                  1	                  	ACH                  
                 2	                  1	                    Credit Card
                 4	                  1	                    Check
                 5	                  1	                    Deposit
                 5	                  2	                    Wire              
                 6	                  1	                    Cash                 
            */

            var SourceType = Payment_Source_Type_Id.ToString() + Payment_Source_Id.ToString();

            switch (SourceType)
            {
                case "11":
                    result = PaymentSourceType.ACH;
                    break;
                case "21":
                    result = PaymentSourceType.CreditCard;
                    break;
                case "41":
                    result = PaymentSourceType.Check;
                    break;
                case "51":
                    result = PaymentSourceType.Deposit;
                    break;
                case "52":
                    result = PaymentSourceType.Wire;
                    break;
                case "61":
                    result = PaymentSourceType.Cash;
                    break;
                default:
                    break;
            }

            return result;
        }

        public static string GetCurrentPageName(this UserControl uc)
        {
            var AbsolutePath = HttpContext.Current.Request.Url.AbsolutePath;
            var CurrentPage = string.Empty;
            var index = AbsolutePath.Length - 1;

            do { index--; } while (AbsolutePath[index] != '/');

            CurrentPage = AbsolutePath.Substring(index, AbsolutePath.Length - index).Replace("/", "");
            return
                CurrentPage;
        }

        /// <summary>
        /// Obtiene el codigo HTML de la pagina pasada en la url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static String getCodigoHTML(String url)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            // Realizamos la petición
            HttpWebResponse miPeticionWeb = (HttpWebResponse)myHttpWebRequest.GetResponse();
            // Obtenemos el flujo de la respuesta
            Stream receiveStream = miPeticionWeb.GetResponseStream();
            // Leemos el flujo de la respuesta obtenida, seleccionando el tipo de codificación que deseamos
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(receiveStream, encode);
            // Realizamos la conversión a String y devolvemos el valor
            return (readStream.ReadToEnd());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="PathXmlFile"></param>
        public static void RemoveXmlnsOfDescendants(string PathXmlFile)
        {
            XDocument Doc = XDocument.Load(PathXmlFile);

            foreach (var node in Doc.Root.Descendants())
            {
                // If we have an empty namespace...
                if (node.Name.NamespaceName == "")
                {
                    // Remove the xmlns='' attribute. Note the use of
                    // Attributes rather than Attribute, in case the
                    // attribute doesn't exist (which it might not if we'd
                    // created the document "manually" instead of loading
                    // it from a file.)
                    node.Attributes("xmlns").Remove();
                    // Inherit the parent namespace instead
                    node.Name = node.Parent.Name.Namespace + node.Name.LocalName;
                }
            }

            Doc.Save(PathXmlFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string CleanXMLToParse(string xml)
        {
            var result = string.Empty;
            result = xml.Replace("utf-16", "utf-8").Trim().Replace("^([\\W]+)<", "<");
            return result;
        }

        /// <summary>
        ///  Forza a que una fecha se parsee en un formato especifico
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime? ParseFormat(this String value, string format = "MM/dd/yyyy")
        {
            DateTime resultDate;
            var resulTry = DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out resultDate);
            if (resulTry)
                return resultDate;
            else
                return (DateTime?)null;
        }

        /// <summary>
        /// Retorna el tiempo en años y en meses
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <returns></returns>
        public static Tuple<Int32, Int32> GetTime(DateTime DateFrom, DateTime? DateTo = null)
        {
            var Today = DateTime.Now;

            if (DateTo.HasValue)
                Today = DateTo.Value;

            // Años
            int edadAnos = Today.Year - DateFrom.Year;

            if (Today.Month < DateFrom.Month || (Today.Month == DateFrom.Month &&
            Today.Day < DateFrom.Day))
                edadAnos--;

            // Meses
            int edadMeses = Today.Month - DateFrom.Month;

            if (Today.Day < DateFrom.Day)
                edadMeses--;

            if (edadMeses < 0)
                edadMeses += 12;

            return new Tuple<int, int>(edadAnos, edadMeses);
        }

        public static Int32 GetAge(this DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
            return (a - b) / 10000;
        }

        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Devuelve true or false si el textbox esta vacio o no
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static bool isEmpty(this TextBox txt)
        {
            return string.IsNullOrEmpty(txt.Text);
        }

        public static string GetNumber(this string value)
        {
            var reg = new Regex(@"[^\d]");
            return reg.Replace(value, "").Trim();
        }

        public static string RemoveInvalidCharacters(this string value)
        {
            return value.Replace("\r", "").Replace("\n", "");
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MyRemoveInvalidCharactersFilName(this string value)
        {
            return value.Replace("\\", "").Replace("/", "").Replace("?", "").Replace("*", "").Replace(":", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("í", "i")
                         .Replace("á", "a").Replace("é", "e").Replace("ó", "o").Replace("ú", "u");
        }

        public static string MyRemoveInvalidCharacters(this string value)
        {
            return value.Replace("\r", "<br>").Replace("\n", "<br>").Replace("'", "");
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value, decimal? defaultValue = null, bool removeSpecialCharacter = false)
        {
            return ConvertStringToDecimal(value, defaultValue, removeSpecialCharacter);
        }

        /// <summary>
        /// Author: Lic. Carlos Lebron.
        /// </summary>
        /// <param name="dob"></param>
        /// <param name="age"></param>
        /// <param name="isYear"></param>
        public static void SetContactAge(DateTime dob, ref int? age, ref bool isYear)
        {
            var time = Utility.GetTime(dob);
            var edad = 0;

            if (time.Item1 > 0)
            {   //Al menos tiene un año o mas 
                edad = time.Item1;
                isYear = true;
            }
            else
            {   //Tiene Meses de nacido
                edad = time.Item2;
                isYear = false;
            }

            age = edad;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <param name="removeSpecialCharacter"></param>
        /// <returns></returns>
        private static decimal ConvertStringToDecimal(string value, decimal? defaultValue = null, bool removeSpecialCharacter = false)
        {
            decimal result;

            value = !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : string.Empty;

            if (removeSpecialCharacter)
                value = Regex.Replace(value, @"(?![\d\.\,]).", "");

            if (!decimal.TryParse(value, System.Globalization.NumberStyles.Number, CultureInfo.InvariantCulture, out result))
                result = defaultValue.HasValue ? defaultValue.Value : -1;

            return
                result;
        }

        /// <summary>
        ///  Marcos Perez
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static IEnumerable<Control> GetAllChildren(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                foreach (Control grandChild in GetAllChildren(control))
                    yield return grandChild;

                yield return control;
            }
        }

        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron
        /// Devuelvel el valor del enum pasando como parametro su nombre y el tipo del enmum
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static int getvalueFromEnumType(String Name, Type enumType)
        {
            var value = -1;

            if (!string.IsNullOrEmpty(Name))
            {
                var lst = enumType.ToListEnum();
                value = int.Parse(lst.Where(x => x.Value.ToLower() == Name.ToLower()).First().Key);
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static List<Control> FindControl(this Control container, String ID)
        {
            var TempList = new List<Control>();

            foreach (Control item in container.Controls)
            {
                Control oControl = item.FindControl(ID);
                if (oControl != null)
                {
                    TempList.Add(oControl);
                }
            }

            return TempList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Object getEnumTypeFromValue(Type enumType, int val)
        {
            return Enum.ToObject(enumType, val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Object getEnumTypeFromValue(Type enumType, string val, bool ignoreParse = false)
        {
            return Enum.Parse(enumType, val, ignoreParse);
        }

        /// <summary>
        /// 
        /// </summary>
        public class NetworkingUtilities
        {
            public static string GetHostName()
            {
                var hostName = Dns.GetHostName();
                return hostName;
            }

            public static IPAddress GetLocalIpaddress()
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                var localIp = host.AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
                return localIp;
            }
        }

        /// <summary>
        /// Eliminar acentos
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string RemoveAccentsWithRegEx(this string inputString)
        {
            var replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            var replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            var replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            var replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            var replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            inputString = replace_a_Accents.Replace(inputString, "a");
            inputString = replace_e_Accents.Replace(inputString, "e");
            inputString = replace_i_Accents.Replace(inputString, "i");
            inputString = replace_o_Accents.Replace(inputString, "o");
            inputString = replace_u_Accents.Replace(inputString, "u");
            return inputString;
        }

        /// <summary>
        /// Elimina acentos con normalizacion 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string RemoveAccentsWithNormalization(string inputString)
        {
            string normalizedString = inputString.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < normalizedString.Length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(normalizedString[i]);
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static void ExportToXLSX(this ASPxGridView gv)
        {
            var oASPxGridViewExporter = new DevExpress.Web.ASPxGridViewExporter();
            oASPxGridViewExporter.GridViewID = gv.ID;
            oASPxGridViewExporter.WriteXlsxToResponse();
            oASPxGridViewExporter.Dispose();
        }

        public static System.Web.UI.HtmlControls.HtmlGenericControl AsHtmlGenericControl(this Control control)
        {
            return (System.Web.UI.HtmlControls.HtmlGenericControl)control;
        }

        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron
        /// Devuelve true or false si la referencia al control es nula
        /// Created Date: 03/16/2015
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool isNullReferenceControl(this Control control)
        {
            return (control == null);
        }

        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron
        /// Devuelve true or false si la referencia al objeto es nula
        /// Created Date: 03/19/2015
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool isNullReferenceObject(this Object obj)
        {
            return (obj == null);
        }

        /// <summary>
        /// Devuelve el valor del enum como par Key Value
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private static List<KeyValuePair<string, string>> ToListEnum(this Type enumType)
        {
            var arrValues = Enum.GetValues(enumType);
            var arrNames = Enum.GetNames(enumType);

            List<KeyValuePair<string, string>> lstCodigos =
              arrNames.Select((name, index) => new KeyValuePair<string, string>(
                arrValues.GetValue(index).GetHashCode().ToString(), name)).ToList();

            return lstCodigos;
        }

        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Añade al UpdatePanel pasado en el parametro en udp un trigger al control pasado en el parametro ControlID
        ///  Created Date: 11/29/2014  
        /// </summary>
        /// <param name="udp"></param>
        /// <param name="ControlID"></param>
        /// <param name="EventName"></param>
        public static void AddTrigger(this UpdatePanel udp, string ControlID, String EventName = "", TriggerType triggerType = TriggerType.PostBackTrigger)
        {
            dynamic Trigger;

            if (triggerType == TriggerType.PostBackTrigger)
                Trigger = new PostBackTrigger();
            else Trigger = new AsyncPostBackTrigger();

            //ControlID = ID del control que provoca el evento.
            Trigger.ControlID = ControlID;
            //EventName = Nombre del evento, p.e: Click, SelectedIndexChange.
            //Trigger.EventName = EventName;
            //Se añade el trigger al update panel                            
            udp.Triggers.Add(Trigger);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gv"></param>
        public static void SetFilterSettings(this ASPxGridView gv)
        {
            if (gv.VisibleRowCount > 0)
                foreach (GridViewColumn column in gv.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        var Field = ((GridViewDataColumn)column);
                        if (!string.IsNullOrEmpty(Field.FieldName))
                        {
                            object value = gv.GetRowValues(0, Field.FieldName);
                            var C = ((GridViewDataColumn)column);
                            if (value is String)
                                C.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                            else
                                C.Settings.AutoFilterCondition = AutoFilterCondition.Equals;

                            C.Settings.AllowAutoFilterTextInputTimer = DefaultBoolean.False;
                        }
                    }
                }
        }

        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Busca el registro dentro de la lista segun el criterio pasado en el predicate oItem
        ///  Created Date: 11/29/2014  
        /// <summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oList"></param>
        /// <param name="oItem"></param>
        /// <returns></returns>
        public static bool RecordExistInList<T>(this List<T> oList, Func<T, bool> oItem) where T : class
        {
            return oList.Where(oItem).Any();
        }

        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Es un metodo de extencion para los controles que ejecuta del lado del cliente el codigo javascript pasado como parametro en la variable JScript   
        /// </summary>
        /// <param name="JScript"></param>
        public static void ExcecuteJScript(this Control Container, string JScript)
        {
            var key = Guid.NewGuid().ToString();
            ScriptManager.RegisterStartupScript(Container.Page, Container.Page.GetType(), key, JScript, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool GetOwnerIsCompany(Language pLang)
        {
            bool isCompany = false;
            var temp = new Services();
            if (temp.Owner_Id.HasValue && temp.Owner_Id.Value != -1)
            {
                var data = temp.oContactManager.GetContact(
                                                            temp.Corp_Id,
                                                            temp.Owner_Id.Value,
                                                            languageId: pLang.ToInt()
                                                          );

                if (data != null)
                    isCompany = (data.ContactTypeId == Utility.ContactTypeId.Company.ToInt());
            }

            return isCompany;
        }

        /// <summary>
        /// Obtiene el Tab actual de la pagina Add New Client
        /// Debes enviarle this en el parametro UC
        /// </summary>
        /// <param name="UC"></param>
        /// <returns></returns>
        public static string GetCurrentTabAddNewClient(UserControl UC)
        {
            var Tab = string.Empty;
            var hdnCurrentTabAddNewClient = UC.Page.Master.FindControl("bodyContent").FindControl("hdnCurrentTabAddNewClient");
            if (hdnCurrentTabAddNewClient != null)
                Tab = (hdnCurrentTabAddNewClient as HiddenField).Value.Split('|')[0];
            return Tab;
        }

        /// <summary>
        /// Obtiene el Tab actual de la pagina Add New Client
        /// Debes enviarle this en el parametro UC
        /// </summary>
        /// <param name="UC"></param>
        /// <returns></returns>
        public static string GetCurrentTabDataReviewCompareData(UserControl UC)
        {
            var Tab = string.Empty;
            var hdnCurrentTab = UC.Page.Master.FindControl("bodyContent").FindControl("DReviewContainer").FindControl("hdnCurrentTab");
            if (hdnCurrentTab != null)
                Tab = (hdnCurrentTab as HiddenField).Value;
            return Tab;
        }

        public static string getCurrentTabView(UserControl UC)
        {
            var Tab = string.Empty;
            var hdnCurrentTab = UC.Page.Master.FindControl("bodyContent").FindControl("WUCView").FindControl("hdnCurrentTabView");
            if (hdnCurrentTab != null)
                Tab = (hdnCurrentTab as HiddenField).Value;
            return Tab;
        }

        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Created Date: 11/29/2014        
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this TextBox txt)
        {
            DateTime result;

            DateTime.TryParse(txt.Text, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

            return result;
        }

        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Created Date: 11/29/2014        
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static int ToInt(this TextBox txt, int? defaultValue = null)
        {
            return
               ConvertStringToInt(txt.Text, defaultValue);
        }
        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Created Date: 11/30/2014
        /// </summary>
        /// <param name="hdn"></param>
        /// <returns></returns>
        public static int ToInt(this HiddenField hdn)
        {
            return
               ConvertStringToInt(hdn.Value);
        }

        /// <summary>
        /// Try to convert a string to a int, if the string is not a number it return a default value in case that you no provide a default value it return -1.
        /// </summary>
        /// <param name="value">A string to try to convert.</param>
        /// <param name="defaultValue">The default value that you want to return in case the convertion fail.</param>
        /// <returns>int</returns>
        private static DateTime ConvertStringToDateTime(string value)
        {
            DateTime result;

            value = !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : string.Empty;

            DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

            return
                result;
        }

        /// <summary>
        /// Try to convert a string to a int, if the string is not a number it return a default value in case that you no provide a default value it return -1.
        /// </summary>
        /// <param name="value">A string to try to convert.</param>
        /// <param name="defaultValue">The default value that you want to return in case the convertion fail.</param>
        /// <returns>int</returns>
        private static int ConvertStringToInt(string value, int? defaultValue = null)
        {
            int result;

            value = !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : string.Empty;

            if (!int.TryParse(value, out result))
                result = defaultValue.HasValue ? defaultValue.Value : -1;

            return
                result;
        }


        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Created Date: 11/28/2014
        /// </summary>
        /// <param name="drop"></param>
        /// <returns></returns>
        public static int ToInt(this DropDownList drop)
        {
            return
                ConvertStringToInt(drop.SelectedValue);
        }

        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Created Date: 11/28/2014
        ///  devuelve la cantidad de filas de in gridview
        ///  <param name="gv"></param>
        /// <returns></returns>
        public static int getRowcountFromGridView(this GridView gv)
        {
            return gv.Rows.Count;
        }

        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Created Date: 11/28/2014
        ///  devuelve un arreglo de los valores de los keyfieldname de un aspxGridView
        /// </summary>
        /// <param name="aspxGridView"></param>
        /// <param name="RowIndex"></param>
        /// <returns></returns>
        public static object[] GetKeysFromAspxGridView(this ASPxGridView aspxGridView)
        {
            var KeyArray = aspxGridView.GetRowValues(aspxGridView.FocusedRowIndex, aspxGridView.KeyFieldName).ToString().Split('|');
            return KeyArray;
        }

        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Created Date: 11/28/2014
        /// devuelve el keyvalue del keyname pasado como paramero un aspxGridView el grid debe tener configurado AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="true"
        /// </summary>
        /// <param name="aspxGridView"></param>
        /// <param name="RowIndex"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static object GetKeyFromAspxGridView(this ASPxGridView aspxGridView, string keyName)
        {
            dynamic result = null;
            var Key = aspxGridView.GetRowValues(aspxGridView.FocusedRowIndex, keyName);

            if (key != null)
            {
                result = Key;
            }

            return result;
        }

        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Created Date: 11/28/2014
        /// devuelve el keyvalue del keyname pasado como paramero un aspxGridView
        /// </summary>
        /// <param name="aspxGridView"></param>
        /// <param name="RowIndex"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static object GetKeyFromAspxGridView(this ASPxGridView aspxGridView, string keyName, int RowIndex, object defaultValue = null)
        {
            object result = null;
            defaultValue = defaultValue.isNullReferenceObject() ? -1 : defaultValue;
            var KeyVal = aspxGridView.GetRowValues(RowIndex, keyName);
            result = (KeyVal != null) ? KeyVal : defaultValue;
            return result;
        }

        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Serializa un objeto T a formato JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string serializeToJSON<T>(T item)
        {
            var objeto = new JavaScriptSerializer().Serialize(item);
            return objeto;
        }



        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Serializa un objeto T a formato JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ToJSON(this object item)
        {
            var oSerializer = new JavaScriptSerializer();
            oSerializer.MaxJsonLength = Int32.MaxValue;
            var objeto = oSerializer.Serialize(item);
            return objeto;
        }

        public static void SaveExceptions(Exception exception, Int64 UserID)
        {
            var HostName = NetworkingUtilities.GetHostName();

            if (ConfigurationManager.AppSettings["SendEmail"].ToString().ToLower() == "true")
            {
                MailManager.SendMessage(
                ConfigurationManager.AppSettings["TestEmails"].ToString().ToLower(),
                 "clebron@statetrustlife.com,ggarcia@statetrustlife.com",
                 "",
                 "The error was generated from : " + HostName + "\n" + exception.Message + "******************************" + exception.StackTrace,
                 ConfigurationManager.AppSettings["EmailFrom"].ToString(),
                 ConfigurationManager.AppSettings["SubjectEmails"].ToString(),
                 "",
                 false
                 );
            }

        }

        /// <summary>
        ///  Author: Lic. Carlos Ml. Lebron
        ///  Serializa un objeto T a formato JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string serializeToJSON(object item)
        {
            var objeto = new JavaScriptSerializer().Serialize(item);
            return objeto;
        }

        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron
        /// Created Date : 11-28-2014
        /// Deserializa un json a objeto T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static T deserializeJSON<T>(string Json) where T : class
        {
            dynamic result = null;

            try
            {
                result = new JavaScriptSerializer().Deserialize<T>(Json);
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron
        /// Created Date : 11-28-2014
        /// Deserializa un json a objeto T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static T FromJsonToObject<T>(this string Json) where T : class
        {
            dynamic result = null;

            try
            {
                var oSerializer = new JavaScriptSerializer();
                oSerializer.MaxJsonLength = Int32.MaxValue;
                var objeto = oSerializer.Deserialize<T>(Json);
                result = objeto;
            }
            catch (Exception)
            {
            }
            return result;
        }

        public static IEnumerable<Entity.UnderWriting.IllusData.DropDown> GetIllusDropDownByType(Utility.DropDownType type, string productCode = null,
            int? age = null, string riderTypeCode = null, string pClass = null, string companyId = null)
        {
            return new IllustrationService().oIllusDataManager
                       .GetDropDownByType(
                       new Entity.UnderWriting.IllusData.DropDown.Parameter
                       {
                           DropDownType = Enum.GetName(typeof(Utility.DropDownType), type),
                           ProductCode = productCode,
                           Age = age,
                           RiderTypeCode = riderTypeCode,
                           PClass = pClass,
                           CompanyId = companyId
                       });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl">Dropdown </param>
        /// <param name="dropDownType"></param>
        /// <param name="DataTextField"></param>
        /// <param name="DataValueField"></param>
        /// <param name="GenerateItemSelect"></param>
        /// <param name="corpId"></param>
        public static void GettingAllDropsToIllus(
                                           ref DropDownList ddl,
                                           DropDownType dropDownType,
                                           string DataTextField = null,
                                           string DataValueField = null,
                                           bool GenerateItemSelect = false,
                                           string familyProductCode = null,
                                           string productCode = null,
                                           string pClass = null,
                                           int companyId = 0,
                                           Language pLang = Language.en,
                                           int? CorpId = null,
                                           int? RegionId = null,
                                           int? CountryId = null,
                                           int? DomesticregId = null,
                                           int? StateProvId = null,
                                           int? ProviderTypeId = null
                                           )
        {
            if (ddl == null) return;

            var culture = new System.Globalization.CultureInfo(pLang.ToString());
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            //Limpiar el dropdown antes de bindearlo
            ddl.Items.Clear();

            if (dropDownType == DropDownType.Boolean)
            {
                ddl.DataSource = new Dictionary<int, string>
                {
                  { 0, "No" }, { 1, Resources.YesLabel }
                };
                ddl.DataTextField = "Value";
                ddl.DataValueField = "Key";
            }
            else
            {
                var serviceManager = new IllustrationService().oIllusDataManager;
                var company = "";
                if (companyId > 0)
                    company = serviceManager.GetCompany(companyId).BrandName;

                Entity.UnderWriting.IllusData.DropDown.Parameter parameterIllus = new Entity.UnderWriting.IllusData.DropDown.Parameter
                {
                    DropDownType = Enum.GetName(typeof(DropDownType), dropDownType),
                    ProductCode = productCode,
                    PlanGroupCode = familyProductCode,
                    Life = familyProductCode == "L" ? "Y" : null,
                    Education = familyProductCode == "E" ? "Y" : null,
                    Retirement = familyProductCode == "R" ? "Y" : null,
                    TermInsurance = familyProductCode == "T" ? "Y" : null,
                    PClass = pClass,
                    CompanyId = company,
                    CorpId = CorpId,
                    RegionId = RegionId,
                    CountryId = CountryId,
                    DomesticregId = DomesticregId,
                    StateProvId = StateProvId,
                    ProviderTypeId = ProviderTypeId
                };

                var data = serviceManager.GetDropDownByType(parameterIllus).ToList();
                //if (data.Any())

                if (dropDownType == DropDownType.Country)
                    data.RemoveAll(o => o.CountryNo == 300);
                else if (dropDownType == DropDownType.MaritalStatus)
                    data.RemoveAll(o => o.MaritalStatusCode == "E");

                if (DataValueField.Split('|').Length > 1)
                {
                    var dataTable = data.ToDataTable<Entity.UnderWriting.IllusData.DropDown>();
                    var dataValueFieldList = DataValueField.Split('|');
                    var DataColumn = "'{";
                    for (var i = 0; i < dataValueFieldList.Length; i++)
                        DataColumn += (i > 0 ? "," : "") + String.Format("\"{0}\":\"'+{0}+'\"", dataValueFieldList[i]);
                    DataColumn += "}'";
                    DataValueField = "ValueField";
                    dataTable.Columns.Add(DataValueField, typeof(string), DataColumn);
                    ddl.DataSource = dataTable;
                }
                else if (productCode != null && !productCode.Contains("VCR") && dropDownType == DropDownType.ContributionType)
                {
                    if (data.Count > 1)
                    {
                        foreach (var item in data)
                        {
                            if (item.ContributionTypeCode.Contains('M'))
                            {
                                data.Remove(item);
                                break;
                            }
                        }
                    }

                    ddl.DataSource = data;
                }
                else
                {
                    if (dropDownType == DropDownType.Relationship)
                        data = data.OrderBy(o => o.RelationshipType).ToList();

                    ddl.DataSource = data;
                }

                ddl.DataTextField = DataTextField;
                if (dropDownType == DropDownType.Provider)
                {
                    foreach (var item in data)
                    {

                        item.ValueFieldSource = item.ProviderTypeId + "|" + item.ProviderId;
                    }
                    ddl.DataValueField = "ValueFieldSource";
                }
                else
                {
                    ddl.DataValueField = DataValueField;
                }
            }

            ddl.SelectedIndex = -1;

            ddl.DataBind();

            if (dropDownType == DropDownType.ActivityRiskType)
                for (int i = 0; i <= ddl.Items.Count - 1; i++)
                    ddl.Items[i].Text = ddl.Items[i].Text.Contains("Tabla") ? ddl.Items[i].Text.Replace("Tabla", Resources.Table)
                                                                            : Resources.ResourceManager.GetString(ddl.Items[i].Text) ?? ddl.Items[i].Text;
            else
                for (int i = 0; i <= ddl.Items.Count - 1; i++)
                    ddl.Items[i].Text = Resources.ResourceManager.GetString(ddl.Items[i].Text) ?? ddl.Items[i].Text;

            if (ddl.Items.Count > 0)
            {
                if (GenerateItemSelect)
                {
                    ddl.Items.Insert(0, new ListItem() { Value = "-1", Text = RESOURCE.UnderWriting.NewBussiness.Resources.Select });
                    ddl.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        ///   Author: Lic. Carlos Ml Lebron
        ///   devuelve una lista de datos dummy del tipo que se le pase como parametro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GenerateDummyData<T>(int totalrecords, bool generateSecuencialCodeInFirstField = false) where T : class
        {   //Obtener las propiedades
            PropertyInfo[] Props = typeof(T).GetProperties();

            var FirstField = string.Empty;
            var JsonData = "[";
            var record = "{";
            var CountField = 0;
            //Crear Registro JSON
            foreach (var item in Props)
            {
                CountField++;

                if (CountField == 1)
                    FirstField = item.Name;

                var ptype = item.PropertyType.Name;

                dynamic resultFieldAndValue = string.Empty;

                var NumericRandom = new Random().Next(1, 999999);

                switch (ptype)
                {
                    case "Boolean":
                        resultFieldAndValue = "true";
                        break;
                    case "Int32":
                        resultFieldAndValue = NumericRandom;
                        break;
                    case "Decimal":
                        resultFieldAndValue = decimal.Parse(NumericRandom.ToString());
                        break;
                    case "DateTime":
                        DateTime d;
                        DateTime.TryParseExact(DateTime.Now.ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d);
                        resultFieldAndValue = d;
                        break;
                    default:
                        resultFieldAndValue = item.Name;
                        break;
                }

                record += ("\"" + item.Name) + "\":\"" + resultFieldAndValue + "\",";
            }

            record = record.Substring(0, record.Length - 1);
            record += "}";

            for (int x = 1; x <= totalrecords; x++)
                JsonData += (generateSecuencialCodeInFirstField) ? record.Replace(":\"" + FirstField + "\"", ":\"" + FirstField + " - " + string.Format("{0:000000}", x) + "\"") + ","
                                                                 : record + ",";

            JsonData = JsonData.Substring(0, JsonData.Length - 1);
            JsonData += "]";

            var Data = deserializeJSON<List<T>>(JsonData);

            return Data;
        }

        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron
        /// Este metodo es para limpiar o inicializa los componentes de un WebUserControl
        /// </summary>
        /// <param name="uc"></param>
        public static void InitializateFormControls(this System.Web.UI.UserControl uc)
        {
            foreach (Control control in uc.Controls)
            {
                if (control is TextBox)
                    ((TextBox)control).Text = String.Empty;
                else if (control is DropDownList)
                {
                    //Valido si el DropDownList para luego posicionarnos 
                    //en el inice 0 de la lista
                    if (((DropDownList)control).Items.Count > 0)
                        ((DropDownList)control).SelectedIndex = 0;
                }
                else if (control is CheckBox)
                    ((CheckBox)control).Checked = false;
                else if (control is GridView)
                {
                    ((GridView)control).DataSource = null;
                    ((GridView)control).DataBind();
                }
            }

        }

        /// <summary>
        ///   Author: Lic. Carlos Ml Lebron
        ///   Oculta o Muestra los elementos que estan dentro de un contenedor de controles
        /// </summary>
        /// <param name="Controls"></param>
        /// <param name="Show"></param>
        public static void HideShowAllControls(ControlCollection Controls, bool Show)
        {
            foreach (Control control in Controls)
                control.Visible = Show;
        }

        /// <summary>
        /// Author: Lic. Carlos ML. Lebron B.
        /// Verifica que la contraseña Tenga al menos un caracter en mayuscula,
        /// al menos un caracter en miniscula, un caracter numerico y un tamaño minimo de 8 caracteres
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="messageError"></param>
        /// <returns></returns>
        public static bool ValiateSecurityPassword(String pass, String Lang, out String messageError)
        {
            var errorSummary = new StringBuilder();

            messageError = "";

            var mayusculas = 0;
            var minusculas = 0;
            var caracteres = pass.Length;
            var numeros = 0;
            string msj;

            //Verificar si la cadena tiene al menos una mayuscula, minuscula, numeros y un minimo de 8 caracteres
            for (var x = 0; x <= pass.Length - 1; x++)
            {
                if (char.IsUpper(pass[x]))
                    mayusculas++;

                if (char.IsLower(pass[x]))
                    minusculas++;

                if (char.IsNumber(pass[x]))
                    numeros++;
            }

            if (mayusculas == 0)
            {
                msj = (Lang == "en") ? "* The new password must have at least one character in uppercase" : "* La nueva clave debe tener al menos un carácter en mayúscula";
                errorSummary.AppendLine(msj);
            }

            if (minusculas == 0)
            {
                msj = (Lang == "en") ? "* The new password must have at least one character lowercase" : "* La nueva clave debe tener al menos un carácter en minúscula";
                errorSummary.AppendLine(msj);
            }

            if (numeros == 0)
            {
                msj = (Lang == "en") ? "* The new password must have at least one number" : "* La nueva clave debe tener al menos un número";
                errorSummary.AppendLine(msj);
            }

            if (caracteres < 8)
            {
                msj = (Lang == "en") ? "* The new password must be a minimum of 8 characters" : "* La nueva clave debe tener un mínimo de 8 caracteres";
                errorSummary.AppendLine(msj);
            }


            if (errorSummary.Length > 0)
                messageError = errorSummary.ToString();

            return (mayusculas >= 1) && (minusculas >= 1) && (numeros >= 1) && (caracteres >= 8);

        }

        public static Tuple<bool, FormatException> EmailIsValid(string emailaddress)
        {
            try
            {
                var m = new MailAddress(emailaddress);

                return new Tuple<bool, FormatException>(true, null);
            }
            catch (FormatException ex)
            {
                return new Tuple<bool, FormatException>(false, ex);
            }
        }

        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron.
        /// Quitar el Chequeo de todos los items de un CheckBoxList
        /// </summary>
        /// <param name="chkList"></param>
        public static void UnCheckAll(this CheckBoxList chkList)
        {
            chkList.Items.Cast<ListItem>().ToList().ForEach(x => x.Selected = false);
        }

        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron.
        /// Contar cuantos items han sido chequeados
        /// </summary>
        /// <param name="chkList"></param>
        /// <returns></returns>
        public static int CheckedCount(this CheckBoxList chkList)
        {
            return chkList.Items.Cast<ListItem>().Count(item => item.Selected);
        }

        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron.
        /// Chequear todos los items de un CheckBoxList
        /// </summary>
        /// <param name="chkList"></param>
        public static void CheckAll(this CheckBoxList chkList)
        {
            chkList.Items.Cast<ListItem>().ToList().ForEach(x => x.Selected = true);
        }

        /// <summary>
        ///  Author:Lic. Carlos Ml Lebron
        ///  Trunca el valor decimal pasado como parametro
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Truncate(this decimal x)
        {
            return Math.Truncate(x);
        }

        /// <summary>
        /// Limpiar el texto de los textBox
        /// Author: Lic. Carlos Ml. Lebron.
        /// </summary>
        /// <param name="txt"></param>
        public static void Clear(this TextBox txt)
        {
            txt.Text = string.Empty;
        }

        /// <summary>
        /// Limpiar el texto de los Literal
        /// Author: Lic. Carlos Ml. Lebron.
        /// </summary>
        /// <param name="txt"></param>
        public static void Clear(this Literal Lt)
        {
            Lt.Text = string.Empty;
        }

        /// Limpiar el texto de los textBox
        /// Author: Lic. Carlos Ml. Lebron.
        /// </summary>
        /// <param name="txt"></param>
        public static void Clear(this Literal Lt, String InicializerCharacter)
        {
            Lt.Text = InicializerCharacter;
        }

        /// Limpiar el texto de los textBox
        /// Author: Lic. Carlos Ml. Lebron.
        /// </summary>
        /// <param name="txt"></param>
        public static void Clear(this TextBox txt, String InicializerCharacter)
        {
            txt.Text = InicializerCharacter;
        }

        /// <summary>
        /// Bindea el aspxGridView Con el datasource Pasado en la variable DataSource
        /// Author: Lic. Carlos Ml. Lebron.
        /// </summary>
        /// <param name="GridView"></param>
        /// <param name="DataSource"></param>
        public static void DatabindAspxGridView<T>(this ASPxGridView GridView, IEnumerable<T> DataSource)
        {
            if (DataSource == null) return;

            GridView.DataSource = DataSource;
            GridView.DataBind();

        }

        /// <summary>
        /// Bindea el GridView Con el datasource Pasado en la variable DataSource
        /// Author: Lic. Carlos Ml. Lebron.
        /// </summary>
        /// <param name="GridView"></param>
        /// <param name="DataSource"></param>
        public static void DatabindGridView(this GridView GridView, Object DataSource)
        {
            if (DataSource == null) return;

            GridView.DataSource = DataSource;
            GridView.DataBind();

        }

        /// <summary>
        /// Convertir DataTable a array byte de csv
        /// Author: Lic. Carlos Ml. Lebron.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static byte[] AsCSVArraByte(this DataTable dt)
        {
            var Line = string.Empty;
            var Header = string.Empty;

            var Listado = new List<string>();

            //Obtener el Header
            Header = dt.Columns.Cast<DataColumn>().Aggregate(Header, (current, item) => current + (item.ColumnName.ToString().QuotedString() + ","));

            Header = Header.Remove(Header.Length - 1, 1);

            Listado.Add(Header);

            foreach (DataRow Row in dt.Rows)
            {
                Line = dt.Columns.Cast<DataColumn>().Aggregate(Line, (current, Col) => current + (Row[Col].ToString().QuotedString() + ","));

                Line = Line.Remove(Line.Length - 1, 1);
                Listado.Add(Line);
                Line = string.Empty;
            }

            string[] strArrayResult = Listado.ToArray();

            var sb = new StringBuilder();

            foreach (var item in strArrayResult)
                sb.AppendLine(item);



            return System.Text.Encoding.UTF8.GetBytes(sb.ToString());
        }

        /// <summary>
        /// Convertir IEnumerable<T> a array byte de csv
        /// Autores: Lic. Carlos Ml Lebron B
        ///          Ing. Carlos Soriano.        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static byte[] AsCSVArraByte<T>(this IEnumerable<T> Data) where T : class
        {
            var Line = string.Empty;
            var Header = string.Empty;

            var Listado = new List<string>();

            //Obtener las propiedades
            PropertyInfo[] Props = typeof(T).GetProperties();

            //Obtener el Header
            Header = Props.Aggregate(Header, (current, item) => current + (item.Name.ToString().QuotedString() + ","));

            Header = Header.Remove(Header.Length - 1, 1);

            Listado.Add(Header);

            foreach (var item in Data)
            {
                Line = Props.Aggregate(Line, (current, prop) => current + (item.GetType().GetProperty(prop.Name).GetValue(item, null).ToString().QuotedString() + ","));

                Line = Line.Remove(Line.Length - 1, 1);
                Listado.Add(Line);
                Line = string.Empty;
            }

            string[] strArrayResult = Listado.ToArray();

            var sb = new StringBuilder();

            foreach (var item in strArrayResult)
                sb.AppendLine(item);

            return System.Text.Encoding.UTF8.GetBytes(sb.ToString());
        }

        /// <summary>
        /// Author: Lic. Carlos Ml. Lebron.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties, bool AllToString = false) where T : new()
        {
            T item = new T();

            foreach (var property in properties)
            {
                try
                {
                    if (AllToString == false)
                        property.SetValue(item, row[property.Name], null);
                    else
                        if (row[property.Name].GetType() == typeof(DateTime))
                            property.SetValue(item, DateTime.Parse(row[property.Name].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy"), null);
                        else
                            property.SetValue(item, row[property.Name].ToString(), null);
                }
                catch (Exception)
                {
                }

            }
            return item;
        }
        /// <summary>
        /// Author : Lic. Carlos Ml Lebron B.
        /// Serializar un DataTable a IList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this DataTable table, bool AllToString = false) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            IList<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                try
                {
                    var item = CreateItemFromRow<T>((DataRow)row, properties, AllToString);
                    result.Add(item);
                }
                catch (Exception)
                {
                    throw;
                }

            }

            return result;
        }

        public static string UppercaseFirst(string value)
        {
            try
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value.Length == 0)
                    return value;

                var result = new StringBuilder(value);
                result[0] = char.ToUpper(result[0]);
                for (int i = 1; i < result.Length; ++i)
                {
                    if (char.IsWhiteSpace(result[i - 1]))
                        result[i] = char.ToUpper(result[i]);
                    else
                        result[i] = char.ToLower(result[i]);
                }
                return result.ToString();
            }
            catch (Exception)
            {

                return value;
            }

        }

        public static string JSON_DataTable(this DataTable dt)
        {

            /****************************************************************************
            * Without goingin to the depth of the functioning
            * of this method, i will try to give an overview
            * As soon as this method gets a DataTable
            * it starts to convert it into JSON String,
            * it takes each row and ineach row it creates
            * an array of cells and in each cell is having its data
            * on the client side it is very usefull for direct binding of object to  TABLE.
            * Values Can be Access on clien in this way. OBJ.TABLE[0].ROW[0].CELL[0].DATA 
            * NOTE: One negative point. by this method user
            * will not be able to call any cell by its name.
            * *************************************************************************/

            StringBuilder JsonString = new StringBuilder();

            JsonString.Append("{ ");
            JsonString.Append("\"TABLE\":[{ ");
            JsonString.Append("\"ROW\":[ ");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                JsonString.Append("{ ");
                JsonString.Append("\"COL\":[ ");

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j < dt.Columns.Count - 1)
                    {
                        JsonString.Append("{" + "\"DATA\":\"" +
                                          dt.Rows[i][j] + "\"},");
                    }
                    else if (j == dt.Columns.Count - 1)
                    {
                        JsonString.Append("{" + "\"DATA\":\"" +
                                          dt.Rows[i][j] + "\"}");
                    }
                }
                /*end Of String*/
                if (i == dt.Rows.Count - 1)
                {
                    JsonString.Append("]} ");
                }
                else
                {
                    JsonString.Append("]}, ");
                }
            }
            JsonString.Append("]}]}");
            return JsonString.ToString();
        }

        public static string CreateJsonParameters(DataTable dt)
        {
            /* /****************************************************************************
             * Without goingin to the depth of the functioning
             * of this method, i will try to give an overview
             * As soon as this method gets a DataTable it starts to convert it into JSON String,
             * it takes each row and in each row it grabs the cell name and its data.
             * This kind of JSON is very usefull when developer have to have Column name of the .
             * Values Can be Access on clien in this way. OBJ.HEAD[0].<ColumnName>
             * NOTE: One negative point. by this method user
             * will not be able to call any cell by its index.
             * *************************************************************************/

            StringBuilder JsonString = new StringBuilder();

            //Exception Handling
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{ ");
                JsonString.Append("\"Head\":[ ");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() +
                                  "\":" + "\"" +
                                  dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" +
                               dt.Columns[j].ColumnName.ToString() + "\":" +
                               "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }

                    /*end Of String*/
                    if (i == dt.Rows.Count - 1)
                        JsonString.Append("} ");
                    else
                        JsonString.Append("}, ");
                }

                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///  Author : Lic. Carlos Ml Lebron B.
        ///  Serializar un List<T> a DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));

            var table = new DataTable();

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            object[] values = new object[props.Count];

            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }

                table.Rows.Add(values);
            }

            return table;
        }

        public static string Encrypt(object Argument)
        {
            byte[] keyArray;
            byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(Argument.ToString());
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(System.Configuration.ConfigurationManager.AppSettings["EncriptionKey"]));
            hashmd5.Clear();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
            tdes.Clear();
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }

        /// <summary>
        /// Author : Lic. Carlos Ml Lebron B.
        /// Capitalize
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Capitalize(this string value, char? separator = null)
        {
            string result = string.Empty;

            value = value.ToLower();

            if (string.IsNullOrWhiteSpace(value))
                return result;

            if (separator.isNullReferenceObject())
            {
                result = value.Substring(0, 1).ToUpper() +
                         value.Substring(1, value.Length - 1).ToLower();
            }
            else
            {
                var s = value.Split(separator.Value);

                for (int i = 0; i < s.Length; i++)
                {
                    if (!string.IsNullOrEmpty(s[i]))
                    {
                        result += (s[i].Substring(0, 1).ToUpper() +
                                   s[i].Substring(1, s[i].Length - 1).ToLower()) + " ";
                    }
                }

                result = result.Remove(result.LastIndexOf(' '), 1);
            }

            return result;
        }

        /// <summary>
        /// Author : Lic. Carlos Ml Lebron B.
        /// QuotedString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string QuotedString(this string value)
        {
            return "\"" + value + "\"";
        }

        /// <summary>
        /// Author : Lic. Carlos Ml Lebron B.
        /// QuotedString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SimpleQuotedString(this string value)
        {
            return "'" + value + "'";
        }

        /// <summary>
        /// Busca una columna dentro de un aspxgridview usando el nombre como parametro de busqueda
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static GridViewColumn getThisColumn(this ASPxGridView grid, string Name)
        {
            GridViewColumn result = null;
            foreach (var item in grid.Columns)
            {
                var Columna = item as GridViewColumn;
                if (Columna.Name == Name)
                {
                    result = Columna;
                    break;
                }
            }

            return
                result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static GridViewDataColumn getThisColumnEx(this ASPxGridView grid, string Name)
        {
            GridViewDataColumn result = null;
            foreach (var item in grid.Columns)
            {
                var Columna = item as GridViewDataColumn;
                if (Columna.Name == Name)
                {
                    result = Columna;
                    break;
                }
            }

            return
                result;
        }

        public static string Decrypt(object Argument)
        {
            byte[] keyArray;
            byte[] Array_a_Descifrar = Convert.FromBase64String(Argument.ToString());
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(System.Configuration.ConfigurationManager.AppSettings["EncriptionKey"]));
            hashmd5.Clear();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
            tdes.Clear();
            string res = UTF8Encoding.UTF8.GetString(resultArray);
            return res;
        }

        public static string Encrypt_Query(string cadena)
        {
            string key = "ABCDEFG54669525PQRSTUVWXYZabcdef852846opqrstuvwxyz";
            try
            {

                byte[] keyArray;
                //arreglo de bytes donde guardaremos el texto  
                //que vamos a encriptar  
                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(cadena);
                //se utilizan las clases de encriptación  
                //provistas por el Framework  
                //Algoritmo MD5     
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                //se guarda la llave para que se le realice  
                //hashing          
                keyArray = hashmd5.ComputeHash(

                UTF8Encoding.UTF8.GetBytes(key));



                hashmd5.Clear();


                //Algoritmo 3DAS  
                TripleDESCryptoServiceProvider tdes =

                new TripleDESCryptoServiceProvider();


                tdes.Key = keyArray;

                tdes.Mode = CipherMode.ECB;

                tdes.Padding = PaddingMode.PKCS7;


                //se empieza con la transformación de la cadena  
                ICryptoTransform cTransform = tdes.CreateEncryptor();

                //arreglo de bytes donde se guarda la  
                //cadena cifrada  

                byte[] ArrayResultado =

                 cTransform.TransformFinalBlock(Arreglo_a_Cifrar,

                0, Arreglo_a_Cifrar.Length);

                tdes.Clear();

                //se regresa el resultado en forma de una cadena  

                return Convert.ToBase64String(ArrayResultado,

                      0, ArrayResultado.Length);

            }

            catch (Exception)
            {

            }

            return string.Empty;

        }

        public static string Decrypt_Query(string clave)
        {
            string key = "ABCDEFG54669525PQRSTUVWXYZabcdef852846opqrstuvwxyz";
            try
            {

                byte[] keyArray;
                //convierte el texto en una secuencia de bytes  

                byte[] Array_a_Descifrar =
              Convert.FromBase64String(clave);

                //se llama a las clases que tienen los algoritmos  

                //de encriptación se le aplica hashing  

                //algoritmo MD5  
                MD5CryptoServiceProvider hashmd5 =
               new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(
                 UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider tdes =
                new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;

                tdes.Padding = PaddingMode.PKCS7;


                ICryptoTransform cTransform =

                 tdes.CreateDecryptor();


                byte[] resultArray =

                cTransform.TransformFinalBlock(Array_a_Descifrar,
                0, Array_a_Descifrar.Length);


                tdes.Clear();

                //se regresa en forma de cadena  
                return UTF8Encoding.UTF8.GetString(resultArray);
            }

            catch (Exception)
            {

            }

            return string.Empty;

        }

        public static String GetSerialId()
        {
            return System.Guid.NewGuid().ToString().Replace("-", "").Substring(1, 12).ToUpper();
        }

        public static byte[] ConvertStreamToByteBuffer(System.IO.Stream theStream)
        {
            int b1;
            System.IO.MemoryStream tempStream = new System.IO.MemoryStream();

            while ((b1 = theStream.ReadByte()) != -1)
            {
                tempStream.WriteByte(((byte)b1));
            }
            return tempStream.ToArray();
        }

        public static byte[] ReadBinaryFile(string path, string fileName)
        {
            if (File.Exists(path + fileName))
            {
                try
                {
                    ///Open and read a file&#12290;
                    FileStream fileStream = File.OpenRead(path + fileName);
                    byte[] datos = ConvertStreamToByteBuffer(fileStream);
                    fileStream.Close();
                    return datos;
                }
                catch
                {
                    return new byte[0];
                }
            }
            else
            {
                return new byte[0];
            }
        }

        public static byte[] ReadBinaryFile(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    ///Open and read a file&#12290;
                    FileStream fileStream = File.OpenRead(path);
                    byte[] datos = ConvertStreamToByteBuffer(fileStream);
                    fileStream.Close();
                    return datos;
                }
                catch
                {
                    return new byte[0];
                }
            }
            else
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// Guarda en la ruta especificada el archivo expresado como arreglo de bytes
        /// </summary>
        /// <param name="_FileName"></param>
        /// <param name="_ByteArray"></param>
        /// <returns></returns>
        public static bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            var result = false;

            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                // Writes a block of bytes to this stream using data from a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Flush();
                _FileStream.Close();
                _FileStream.Dispose();
                result = true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            // error occured, return false
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_FileName"></param>
        /// <param name="_ByteArray"></param>
        /// <returns></returns>
        public static Tuple<bool, string> MyByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            var ErrMessage = string.Empty;
            var result = false;

            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                // Writes a block of bytes to this stream using data from a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Flush();
                _FileStream.Close();
                _FileStream.Dispose();
                result = true;
            }
            catch (Exception _Exception)
            {
                // Error
                ErrMessage = _Exception.ToString();
            }

            // error occured, return false
            return new Tuple<bool, string>(result, ErrMessage);
        }

        public static void ReadOnlyControls(ControlCollection controles, bool Estado)
        {
            foreach (Control control in controles)
            {
                if (control is TextBox)
                    ((TextBox)control).ReadOnly = Estado;
                else if (control is DropDownList)
                    ((DropDownList)control).Enabled = !Estado;
                else if (control is Button)
                    ((Button)control).Enabled = !Estado;
                else if (control is CheckBox)
                    ((CheckBox)control).Enabled = !Estado;
                else if (control is CheckBoxList)
                    ((CheckBoxList)control).Enabled = !Estado;
                else if (control is FileUpload)
                    ((FileUpload)control).Enabled = !Estado;
                else if (control is ASPxUploadControl)
                    ((ASPxUploadControl)control).Enabled = !Estado;
                else if (control is GridView)
                    ((GridView)control).Enabled = !Estado;
                else if (control is Panel)
                    ((Panel)control).Enabled = Estado;
                else if (control is System.Web.UI.HtmlControls.HtmlGenericControl)
                    (control as System.Web.UI.HtmlControls.HtmlGenericControl).Disabled = !Estado;
                else if (control is DevExpress.Web.ASPxGridView)
                    ((DevExpress.Web.ASPxGridView)control).Enabled = !Estado;
                else if (control is DevExpress.Web.ASPxPageControl)
                    (control as DevExpress.Web.ASPxPageControl).Enabled = !Estado;
                ReadOnlyControls(control.Controls, Estado);
            }
        }

        public static void ReadOnlyControls(ControlCollection controles, bool Estado, List<Control> ExcludeControls)
        {
            foreach (Control control in controles)
            {
                var Exclude = ExcludeControls.Where(x => x.UniqueID == control.UniqueID).Any();

                if (!Exclude)
                {
                    if (control is TextBox)
                        ((TextBox)control).ReadOnly = Estado;
                    else if (control is DropDownList)
                        ((DropDownList)control).Enabled = !Estado;
                    else if (control is Button)
                        ((Button)control).Enabled = !Estado;
                    else if (control is CheckBox)
                        ((CheckBox)control).Enabled = !Estado;
                    else if (control is CheckBoxList)
                        ((CheckBoxList)control).Enabled = !Estado;
                    else if (control is FileUpload)
                        ((FileUpload)control).Enabled = !Estado;
                    else if (control is ASPxUploadControl)
                        ((ASPxUploadControl)control).Enabled = !Estado;
                    else if (control is GridView)
                        ((GridView)control).Enabled = !Estado;
                    else if (control is Panel)
                        ((Panel)control).Enabled = !Estado;
                    else if (control is System.Web.UI.HtmlControls.HtmlGenericControl)
                        (control as System.Web.UI.HtmlControls.HtmlGenericControl).Disabled = !Estado;
                    else if (control is DevExpress.Web.ASPxGridView)
                        ((DevExpress.Web.ASPxGridView)control).Enabled = !Estado;
                    else if (control is DevExpress.Web.ASPxPageControl)
                        (control as DevExpress.Web.ASPxPageControl).Enabled = !Estado;

                    ReadOnlyControls(control.Controls, Estado);
                }
            }
        }

        public static void EnableControlswithoutRecursion(ControlCollection controles, bool Estado)
        {
            foreach (Control control in controles)
            {
                if (control is TextBox)
                    ((TextBox)control).Enabled = Estado;
                else if (control is DropDownList)
                    ((DropDownList)control).Enabled = Estado;
                else if (control is Button)
                    ((Button)control).Enabled = Estado;
                else if (control is CheckBox)
                    ((CheckBox)control).Enabled = Estado;
                else if (control is CheckBoxList)
                    ((CheckBoxList)control).Enabled = Estado;
                else if (control is FileUpload)
                    ((FileUpload)control).Enabled = Estado;
                else if (control is GridView)
                    ((GridView)control).Enabled = Estado;
                else if (control is ASPxUploadControl)
                    ((ASPxUploadControl)control).Enabled = Estado;
                else if (control is Panel)
                    ((Panel)control).Enabled = Estado;
                else if (control is System.Web.UI.HtmlControls.HtmlGenericControl)
                    (control as System.Web.UI.HtmlControls.HtmlGenericControl).Disabled = !Estado;
                else if (control is DevExpress.Web.ASPxGridView)
                {
                    ((DevExpress.Web.ASPxGridView)control).Enabled = Estado;
                    EnableControlswithoutRecursion(control.Controls, Estado);
                }
                else if (control is DevExpress.Web.ASPxPageControl)
                    (control as DevExpress.Web.ASPxPageControl).Enabled = Estado;
            }
        }

        public static void EnableControls(ControlCollection controles, bool Estado)
        {
            foreach (Control control in controles)
            {
                if (control is TextBox)
                    ((TextBox)control).Enabled = Estado;
                else if (control is DropDownList)
                    ((DropDownList)control).Enabled = Estado;
                else if (control is Button)
                    ((Button)control).Enabled = Estado;
                else if (control is CheckBox)
                    ((CheckBox)control).Enabled = Estado;
                else if (control is CheckBoxList)
                    ((CheckBoxList)control).Enabled = Estado;
                else if (control is FileUpload)
                    ((FileUpload)control).Enabled = Estado;
                else if (control is GridView)
                    ((GridView)control).Enabled = Estado;
                else if (control is ASPxUploadControl)
                    ((ASPxUploadControl)control).Enabled = Estado;
                else if (control is Panel)
                    ((Panel)control).Enabled = Estado;
                else if (control is System.Web.UI.HtmlControls.HtmlGenericControl)
                    (control as System.Web.UI.HtmlControls.HtmlGenericControl).Disabled = !Estado;
                else if (control is DevExpress.Web.ASPxGridView)
                    ((DevExpress.Web.ASPxGridView)control).Enabled = Estado;
                else if (control is DevExpress.Web.ASPxPageControl)
                    (control as DevExpress.Web.ASPxPageControl).Enabled = Estado;

                EnableControls(control.Controls, Estado);
            }
        }

        public static void EnableControls(ControlCollection controles, Control ExcludeControl, bool Estado)
        {
            foreach (Control control in controles)
            {
                if (control != ExcludeControl)
                {
                    if (control is TextBox)
                        ((TextBox)control).Enabled = Estado;
                    else if (control is DropDownList)
                        ((DropDownList)control).Enabled = Estado;
                    else if (control is Button)
                        ((Button)control).Enabled = Estado;
                    else if (control is CheckBox)
                        ((CheckBox)control).Enabled = Estado;
                    else if (control is CheckBoxList)
                        ((CheckBoxList)control).Enabled = Estado;
                    else if (control is FileUpload)
                        ((FileUpload)control).Enabled = Estado;
                    else if (control is GridView)
                        ((GridView)control).Enabled = Estado;
                    else if (control is ASPxUploadControl)
                        ((ASPxUploadControl)control).Enabled = Estado;
                    else if (control is Panel)
                        ((Panel)control).Enabled = Estado;
                    else if (control is System.Web.UI.HtmlControls.HtmlGenericControl)
                        (control as System.Web.UI.HtmlControls.HtmlGenericControl).Disabled = !Estado;
                    else if (control is DevExpress.Web.ASPxGridView)
                        ((DevExpress.Web.ASPxGridView)control).Enabled = Estado;
                    else if (control is DevExpress.Web.ASPxPageControl)
                        (control as DevExpress.Web.ASPxPageControl).Enabled = Estado;

                    EnableControls(control.Controls, ExcludeControl, Estado);

                }
            }
        }

        /// <summary>
        /// Inicializa algunos componentes tales como TextBox,DropDownList,CheckBox y GridView pasando al metodo
        /// Una coleccion de controles
        /// </summary>
        /// <param name="controles"></param>
        public static void ClearAll(ControlCollection controles)
        {
            //Recorrer los componentes de la lista            
            foreach (Control control in controles)
            {
                if (control is TextBox)
                {
                    var isDecimalTxt = false;
                    var txt = ((TextBox)control);

                    foreach (var item in txt.Attributes.Keys)
                    {
                        if (item.ToString() == "decimal" || item.ToString() == "decimal-us")
                        {
                            isDecimalTxt = true;
                            break;
                        }
                    }

                    if (isDecimalTxt)
                        txt.Clear("0.00");
                    else
                        txt.Clear();
                }
                else if (control is DropDownList)
                {
                    //Valido si el DropDownList para luego posicionarnos 
                    //en el inice 0 de la lista                    
                    if (((DropDownList)control).Items.Count > 0)
                        ((DropDownList)control).SelectedIndex = 0;
                }
                else if (control is CheckBox)
                    ((CheckBox)control).Checked = false;
                //else if (control is CheckBoxList)
                //    ((CheckBoxList)control).Items.Clear();
                else if (control is GridView)
                {
                    ((GridView)control).DataSource = null;
                    ((GridView)control).DataBind();
                }

                ClearAll(control.Controls);
            }
        }

        /// <summary>
        /// Inicializa algunos componentes tales como TextBox,DropDownList,CheckBox y GridView pasando al metodo
        /// Una coleccion de controles y Excluye del proceso el control pasado en la variable ExcludeControl 
        /// </summary>
        /// <param name="controles"></param>
        /// <param name="ExcludeControl"></param>
        public static void ClearAll(ControlCollection controles, Control ExcludeControl)
        {

            foreach (Control control in controles)
            {
                if (control != ExcludeControl)
                {
                    if (control is TextBox)
                    {

                        var isDecimalTxt = false;
                        var txt = ((TextBox)control);

                        foreach (var item in txt.Attributes.Keys)
                        {
                            if (item.ToString() == "decimal" || item.ToString() == "decimal-us")
                            {
                                isDecimalTxt = true;
                                break;
                            }
                        }

                        if (isDecimalTxt)
                            txt.Clear("0.00");
                        else
                            txt.Clear();
                    }
                    else if (control is DropDownList)
                    {
                        //Valido si el DropDownList para luego posicionarnos 
                        //en el inice 0 de la lista
                        if (((DropDownList)control).Items.Count > 0)
                            ((DropDownList)control).SelectedIndex = 0;
                        else ((DropDownList)control).Items.Clear();
                    }
                    else if (control is CheckBox)
                        ((CheckBox)control).Checked = false;
                    //else if (control is HiddenField)
                    //    ((HiddenField)control).Value = string.Empty;
                    else if (control is GridView)
                    {
                        ((GridView)control).DataSource = null;
                        ((GridView)control).DataBind();
                    }
                    else if (control is ASPxGridView)
                    {
                        ((ASPxGridView)control).DataSource = null;
                        ((ASPxGridView)control).DataBind();
                    }

                    ClearAll(control.Controls, ExcludeControl);
                }

            }
        }


        public static void ClearAll(ControlCollection controles, List<Control> ExcludeControls)
        {
            foreach (Control control in controles)
            {
                var Exclude = ExcludeControls.Where(x => x.UniqueID == control.UniqueID).Any();

                if (!Exclude)
                {
                    if (control is TextBox)
                    {

                        var isDecimalTxt = false;
                        var txt = ((TextBox)control);

                        foreach (var item in txt.Attributes.Keys)
                        {
                            if (item.ToString() == "decimal" || item.ToString() == "decimal-us")
                            {
                                isDecimalTxt = true;
                                break;
                            }
                        }

                        if (isDecimalTxt)
                            txt.Clear("0.00");
                        else
                            txt.Clear();
                    }
                    else if (control is DropDownList)
                    {    //Valido si el DropDownList para luego posicionarnos 
                        //en el inice 0 de la lista
                        if (((DropDownList)control).Items.Count > 0)
                            ((DropDownList)control).SelectedIndex = 0;

                        else ((DropDownList)control).Items.Clear();
                    }
                    else if (control is CheckBox)
                        ((CheckBox)control).Checked = false;
                    //else if (control is CheckBoxList)
                    //    ((CheckBoxList)control).Items.Clear();
                    else if (control is GridView)
                    {
                        ((GridView)control).DataSource = null;
                        ((GridView)control).DataBind();
                    }
                    else if (control is ASPxGridView)
                    {
                        ((ASPxGridView)control).DataSource = null;
                        ((ASPxGridView)control).DataBind();
                    }
                }
            }
        }


        public static void ClearAll(ControlCollection controles, Control ExcludeControl, Control ExcludeControl2)
        {

            foreach (Control control in controles)
            {
                if (control != ExcludeControl && control != ExcludeControl2)
                {
                    if (control is TextBox)
                    {
                        var isDecimalTxt = false;
                        var txt = ((TextBox)control);

                        foreach (var item in txt.Attributes.Keys)
                        {
                            if (item.ToString() == "decimal" || item.ToString() == "decimal-us")
                            {
                                isDecimalTxt = true;
                                break;
                            }
                        }

                        if (isDecimalTxt)
                            txt.Clear("0.00");
                        else
                            txt.Clear();
                    }
                    else if (control is DropDownList)
                    {    //Valido si el DropDownList para luego posicionarnos 
                        //en el inice 0 de la lista
                        if (((DropDownList)control).Items.Count > 0)
                            ((DropDownList)control).SelectedIndex = 0;

                        else ((DropDownList)control).Items.Clear();
                    }
                    else if (control is CheckBox)
                        ((CheckBox)control).Checked = false;
                    //else if (control is CheckBoxList)
                    //    ((CheckBoxList)control).Items.Clear();
                    else if (control is GridView)
                    {
                        ((GridView)control).DataSource = null;
                        ((GridView)control).DataBind();
                    }
                    else if (control is ASPxGridView)
                    {
                        ((ASPxGridView)control).DataSource = null;
                        ((ASPxGridView)control).DataBind();
                    }
                    ClearAll(control.Controls, ExcludeControl, ExcludeControl2);
                }
            }
        }

        public static List<YearsItem> GetYearsList()
        {
            try
            {
                List<YearsItem> Result = new List<YearsItem>();

                for (int i = 2000; i < DateTime.Now.Year + 1; i++)
                {
                    YearsItem YearItem = new YearsItem();
                    YearItem.Year = i;
                    YearItem.YearDescription = i.ToString();

                    Result.Add(YearItem);
                }

                return Result;
            }
            catch (Exception)
            {
                return new List<YearsItem>();
            }

        }

        public static List<YearsItem> GetYearsList(byte year, Order order = Order.Desc)
        {
            try
            {
                var Result = new List<YearsItem>();

                for (int i = DateTime.Now.Year; i > DateTime.Now.Year - year; i--)
                {
                    YearsItem YearItem = new YearsItem();
                    YearItem.Year = i;
                    YearItem.YearDescription = i.ToString();

                    Result.Add(YearItem);
                }

                if (order == Order.Desc)
                    return Result;
                else
                    return Result.OrderBy(i => i.Year).ToList();
            }
            catch (Exception)
            {
                return new List<YearsItem>();
            }

        }

        public static List<YearsItem> GetYearsList(byte year, int startYear, Order order = Order.Desc)
        {
            try
            {
                var Result = new List<YearsItem>();

                for (int i = startYear; i > DateTime.Now.Year - year; i--)
                {
                    YearsItem YearItem = new YearsItem();
                    YearItem.Year = i;
                    YearItem.YearDescription = i.ToString();

                    Result.Add(YearItem);
                }
                if (order == Order.Desc)

                    return Result;
                else
                    return Result.OrderBy(i => i.Year).ToList();
            }
            catch (Exception)
            {
                return new List<YearsItem>();
            }

        }

        public static IEnumerable<string> DtToCommaList(GridView gv, String Campo, String SelectCheckName)
        {

            var commaString = "";

            foreach (var row in from GridViewRow row in gv.Rows
                                let cb = row.FindControl(SelectCheckName) as CheckBox
                                where cb != null && cb.Checked
                                select row)
            {
                if (!String.IsNullOrEmpty(commaString))
                    commaString = commaString + "," + gv.DataKeys[row.RowIndex].Values[Campo];
                else
                    commaString = commaString + gv.DataKeys[row.RowIndex].Values[Campo];
            }

            return commaString.Split(',');
        }

        /// <summary>
        /// Método que devuelve en un arreglo el trimestre.
        /// </summary>
        /// <param name="aDate">fecha</param>
        /// <returns></returns>
        public static DateRange GetQuarterDateRange(DateTime aDate)
        {
            var result = new DateRange();
            var aMonth = aDate.Month;
            var aYear = aDate.Year;

            if (aMonth >= 1 && aMonth <= 3)
            {
                result.StartDate = new DateTime(aYear, 1, 1);
                result.EndDate = new DateTime(aYear, 3, 31, 23, 59, 59);
            }
            else if (aMonth >= 4 && aMonth <= 6)
            {
                result.StartDate = new DateTime(aYear, 4, 1);
                result.EndDate = new DateTime(aYear, 6, 30, 23, 59, 59);
            }
            else if (aMonth >= 7 && aMonth <= 9)
            {
                result.StartDate = new DateTime(aYear, 7, 1);
                result.EndDate = new DateTime(aYear, 9, 30, 23, 59, 59);
            }
            else if (aMonth >= 9 && aMonth <= 12)
            {
                result.StartDate = new DateTime(aYear, 10, 1);
                result.EndDate = new DateTime(aYear, 12, 31, 23, 59, 59);
            }

            return result;
        }

        /// <summary>
        ///  Author: Lic. Carlos Ml Lebron
        ///  Forza a renderizar las etiquetas thead y tfoooter en un gridview
        /// </summary>
        /// <param name="gv"></param>
        public static void RenderTableHeaderOrTableFooterOnGridView(this GridView gv)
        {
            if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
            {
                gv.UseAccessibleHeader = false;
                if (gv.HeaderRow != null)
                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            if (gv.ShowFooter == true && gv.Rows.Count > 0)
            {
                gv.UseAccessibleHeader = false;
                gv.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        /// <summary>
        /// Author: Ronny Alvarez
        /// Para Seleccionar un valor del DropDown segun el Indice
        /// </summary>
        /// <param name="dropDown"></param>
        /// <param name="Value"></param>
        /// <param name="SelectFistIndex"></param>

        public static void SelectIndexByValueJSON(this DropDownList dropDown, string value)
        {
            try
            {
                var val = string.Empty;

                if (dropDown != null && dropDown.Items.Count >= 1)
                {
                    for (int i = 0; i < dropDown.Items.Count; i++)
                    {
                        if (dropDown.Items[i].Value != "-1")
                        {
                            val = dropDown.Items[i].Value;
                            i = dropDown.Items.Count;
                        }
                    }

                    if (string.IsNullOrEmpty(val) == false)
                    {
                        var temp = value.Trim().Replace("{", "").Replace("}", "").Split(',');

                        var realValue = val.Trim().Replace("{", "").Replace("}", "").Split(',');

                        System.Collections.Hashtable valores = new Hashtable();
                        System.Collections.Hashtable formato = new Hashtable();

                        for (int i = 0; i < temp.Length; i++)
                        {
                            valores.Add(temp[i].Split(':')[0], temp[i].Split(':')[1]);
                        }
                        for (int i = 0; i < realValue.Length; i++)
                        {
                            formato.Add(realValue[i].Split(':')[0], realValue[i].Split(':')[1]);
                        }

                        foreach (DictionaryEntry deEntry in valores)
                        {
                            // Get value from Registry and assign to sValue.
                            // ...
                            // Change value in hashtable.
                            var sKey = deEntry.Key.ToString();
                            formato[sKey] = valores[sKey];
                        }


                        StringBuilder select = new StringBuilder();
                        select.Append("{");

                        int first = 0;
                        for (int i = 0; i < realValue.Length; i++)
                        {
                            var sKey = realValue[i].Split(':')[0];
                            var v = formato[realValue[i].Split(':')[0]];

                            if (first == 0)
                                select.Append(sKey + ":" + v + "");
                            else
                                select.Append("," + sKey + ":" + v + "");

                            first++;
                        }

                        select.Append("}");

                        dropDown.SelectIndexByValue(select.ToString());

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Author: Ronny Alvarez
        /// Para Seleccionar un valor del DropDown segun el Indice
        /// </summary>
        /// <param name="DropDownList1"></param>
        /// <param name="Value"></param>
        /// <param name="SelectFistIndex"></param>

        public static void SelectIndexByValue(this System.Web.UI.WebControls.DropDownList Drop, String Value, bool SelectFirstIndex = false)
        {
            var listItem = Drop.Items.FindByValue(Value.NTrim());
            if (listItem != null)
                Drop.SelectedIndex = Drop.Items.IndexOf(listItem);
            else if (SelectFirstIndex)
            {
                if (Drop.Items.Count > 0)
                    Drop.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Author: Ronny Alvarez
        /// Para Seleccionar un valor del DropDown segun el Valor
        /// <param name="DropDownList1"></param>
        /// <param name="Text"></param>
        /// <param name="SelectFistIndex"></param>
        public static void SelectIndexByText(ref DropDownList DropDownList1, String Text, bool SelectFirstIndex = false)
        {
            var listItem = DropDownList1.Items.FindByText(Text.NTrim());

            if (listItem != null)
                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(listItem);
            else if (SelectFirstIndex)
            {
                if (DropDownList1.Items.Count > 0)
                    DropDownList1.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPercent(this string value)
        {
            var reg = new Regex("\\d{1,3}[.\\d{1,2}]{0,1}%");
            return reg.IsMatch(value);
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDate(this string value)
        {
            DateTime d;
            return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDecimal(this string value)
        {
            decimal d;
            return Decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out d);
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDouble(this string value)
        {
            double d;
            return Double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out d);
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInt(this string value)
        {
            int d;
            return Int32.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out d);
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRange(this int value, int from, int to)
        {
            return value >= from && value <= to;
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this object value)
        {
            try
            {
                return Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string ZeroIfEmpty(this string s)
        {
            return ReturnZeroIfEmpty(s);
        }

        /// <summary>
        /// Retorna 0 en tipo texto, para casos en que el campo evaluado esta vacio.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>string</returns>
        private static string ReturnZeroIfEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) ? "0" : s;
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble(this object value)
        {
            try
            {
                return Convert.ToDouble(value, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object value)
        {
            try
            {
                return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return 0m;
            }
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int64 ToLong(this object value)
        {
            try
            {
                return Convert.ToInt64(value, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this object value)
        {
            bool ret = false;
            try
            {
                if (value != null)
                    ret = Convert.ToBoolean(value, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToLong(this string value)
        {
            try
            {
                return Convert.ToInt64(value, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string ToJson(this object obj)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static T ToObject<T>(this string json)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// Valida si el string es nulo para devolver un vacio vacio, en caso contrario, devuelve el valor y ejecuta trim.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string NTrim(this string value)
        {
            return (value ?? "").Trim();
        }

        public static bool IsEmail(this string email)
        {
            try
            {
                var m = new MailAddress(email);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Nullable<DateTime> IsDateReturnNull(this string value)
        {
            DateTime d;

            bool resut = DateTime.TryParseExact(value, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d);

            if (resut)
                return d;
            else
                return new Nullable<DateTime>();

        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Nullable<decimal> IsDecimalReturnNull(this string value)
        {
            decimal d;
            bool resut = Decimal.TryParse(value, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out d);

            if (resut)
                return d;
            else
                return new Nullable<decimal>();
        }
        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Nullable<double> IsDoubleReturnNull(this string value)
        {
            double d;
            bool resut = double.TryParse(value, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out d);

            if (resut)
                return d;
            else
                return new Nullable<double>();
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Nullable<long> IslongReturnNull(this string value)
        {
            long d;
            bool resut = long.TryParse(value, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out d);

            if (resut)
                return d;
            else
                return new Nullable<long>();
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Nullable<Int32> IsIntReturnNull(this string value)
        {
            int d;
            bool resut = Int32.TryParse(value, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out d);

            if (resut)
                return d;
            else
                return new Nullable<Int32>();
        }

        /// <summary>
        /// Author: Gregory Garcia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Nullable<Int16> IsShortReturnNull(this string value)
        {
            Int16 d;
            bool resut = Int16.TryParse(value, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out d);

            if (resut)
                return d;
            else
                return new Nullable<Int16>();
        }

        /// <summary>
        /// Traduce las columnas del los grid de devExpress
        /// </summary>
        /// <param name="grid"></param>
        public static void TranslateColumnsGridView(this GridView grid)
        {
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                var column = grid.Columns[i];
                var key = column.AccessibleHeaderText;
                var result = RESOURCE.UnderWriting.NewBussiness.Resources.ResourceManager.GetString(key);
                column.HeaderText = !string.IsNullOrEmpty(result) ? result.ToUpper() : column.HeaderText;
            }
        }

        /// <summary>
        /// Traduce las columnas del los grid de devExpress
        /// </summary>
        /// <param name="grid"></param>
        public static void TranslateColumnsAspxGrid(this ASPxGridView grid, string[] excludeColumns = null)
        {
            foreach (GridViewColumn column in grid.Columns)
            {
                if (column is GridViewDataColumn)
                {
                    if (excludeColumns != null)
                    {
                        if (!excludeColumns.Contains(column.Name))
                        {
                            var key = column.Name;
                            var result = Resources.ResourceManager.GetString(key);
                            column.Caption = !string.IsNullOrEmpty(result) ? result.ToUpper() : column.Caption.ToUpper();
                        }
                    }
                    else
                    {
                        var key = column.Name;
                        var result = Resources.ResourceManager.GetString(key);
                        column.Caption = !string.IsNullOrEmpty(result) ? result.ToUpper() : column.Caption.ToUpper();
                    }
                }
            }
        }

        public static void SetGridFilterSettings(ASPxGridView grid)
        {
            foreach (GridViewColumn column in grid.Columns)
            {
                if (column is GridViewDataColumn)
                {
                    object value = grid.GetRowValues(0, ((GridViewDataColumn)column).FieldName);
                    if (value is String)
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    else if (value is int)
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Equals;
                }
                else if (column is GridViewDataTextColumn)
                {
                    object value = grid.GetRowValues(0, ((GridViewDataTextColumn)column).FieldName);
                    if (value is String)
                        ((GridViewDataTextColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    else if (value is int)
                        ((GridViewDataTextColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Equals;
                }
            }
        }

        /// <summary>
        /// Muestra o no las columnas del los grid de devExpress
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="visible"></param>
        /// <param name="name"></param>        
        public static void VisibleColumnsAspxGrid(this ASPxGridView grid, bool visible, string[] names)
        {
            foreach (GridViewColumn column in grid.Columns)
            {
                if (column is GridViewDataColumn)
                {
                    foreach (string name in names)
                    {
                        if (name == column.Name)
                            column.Visible = visible;
                    }
                }
            }
        }

        /// <summary>
        /// Trae el primer día de una fecha.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Trae el último día de una fecha.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns> 
        public static DateTime LastDayOfMonth(this DateTime date)
        {
            date = date.AddMonths(1);
            return new DateTime(date.Year, date.Month, 1).AddMilliseconds(-1);
        }

        //public static Dictionary<int, string> GetListPeriodDate()
        //{
        //    return new Dictionary<int, string>
        //    {
        //        { PeriodsDate.All.ToInt(), Resources.All },
        //        { PeriodsDate.Today.ToInt(), Resources.Today },
        //        { PeriodsDate.MonthToDate.ToInt(), Resources.MonthToDate },
        //        { PeriodsDate.Last6Months.ToInt(), Resources.LastNMonths.SFormat(6) },
        //        { PeriodsDate.Last13Months.ToInt(), Resources.LastNMonths.SFormat(13) },
        //        { PeriodsDate.CustomDate.ToInt(), Resources.CustomDate}
        //    };
        //}

        //public static DateTime? GetFromByPeriodDate(this PeriodsDate periodDate)
        //{
        //    DateTime? from = DateTime.Now.Date;
        //    switch (periodDate)
        //    {
        //        case PeriodsDate.All:
        //            from = null;
        //            break;
        //        case PeriodsDate.MonthToDate:
        //            from = from.GetValueOrDefault().FirstDayOfMonth();
        //            break;
        //        case PeriodsDate.Last6Months:
        //            from = from.GetValueOrDefault().AddMonths(-6).Date;
        //            break;
        //        case PeriodsDate.Last13Months:
        //            from = from.GetValueOrDefault().AddMonths(-13).Date;
        //            break;
        //    }
        //    return from;
        //}

        //public static DateTime? GetToByPeriodDate(this PeriodsDate periodDate)
        //{
        //    return periodDate == PeriodsDate.All ? null : (DateTime?)DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);
        //}

        public static Dictionary<int, string> GetStatisticsPeriodDateList()
        {
            return new Dictionary<int, string>
                {
                    { (int)Period.Periods.Monthly, Resources.Last13Month },
                    { (int)Period.Periods.Semestral, "Semestral" },
                    { (int)Period.Periods.Quarterly, Resources.Quarterly },
                    { (int)Period.Periods.Yearly, Resources.Yearly },
                    { (int)Period.Periods.SeasonalMonth, Resources.SeasonalMonth },
                    { (int)Period.Periods.SeasonalQuarter, Resources.SeasonalQuarter },
                    { (int)Period.Periods.SeasonalSemestral, Resources.SeasonalSemestral }
                };
        }

        public static string ToFormatCurrency(this object value, string currency = "USD")
        {
            string returnValue;
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            var culture = new System.Globalization.CultureInfo(currency == "EUR" ? "es" : "en");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = culture;
            returnValue = value.ToDouble().ToString("C");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = oldCulture;
            return returnValue;
        }

        //Bmarroquin 10-01-2017 Se agregan mejoras como parte de la tropicalizacion de ESA, se estaban perdiendo decimales al realizar la conversion a Double !! usando ToFormatCurrency
        public static string ToStringCurrency(this double value)
        {
            return value.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("en-USD"));
        }

        public static string ToFormatNumeric(this decimal value)
        {
            return value.ToString("#,##0.00", CultureInfo.InvariantCulture);
        }

        public static string ToFormatNumeric(this double value)
        {
            return value.ToString("#,##0.00", CultureInfo.InvariantCulture);
        }

        public static string ToFormatNumeric(this int? value)
        {
            return value.GetValueOrDefault().ToString("#,##0", CultureInfo.InvariantCulture);
        }

        public static string ToFormatNumeric(this decimal? value, string Format = "#,##0.00")
        {
            return value.GetValueOrDefault().ToString(Format, CultureInfo.InvariantCulture);
        }

        public static string ToFormatNumeric(this double? value)
        {
            return value.GetValueOrDefault().ToString("#,##0.00", CultureInfo.InvariantCulture);
        }

        public static T Clone<T>(this object obj)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static Exception GetLastInnerException(this Exception ex)
        {
            if (ex.InnerException != null) return GetLastInnerException(ex.InnerException);
            return ex;
        }

        public static string SFormat(this string value, params object[] args)
        {
            return String.Format(value, args);
        }

        public static bool SIsNullOrEmpty(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        public static string IsNullOrEmptyReturnValue(this string value, string returnValue)
        {
            return String.IsNullOrEmpty(value) ? returnValue : value;
        }

        public static string FirstIsNotNullOrEmpty(params string[] arrValue)
        {
            return arrValue.FirstOrDefault(o => !String.IsNullOrEmpty(o));
        }

        public static string ToPercent(this decimal value, bool divideByHundred = true)
        {
            return (value / (divideByHundred ? 100 : 1)).ToString("p", CultureInfo.InvariantCulture);
        }

        public static string ToPercent(this double value, bool divideByHundred = true)
        {
            return (value / (divideByHundred ? 100 : 1)).ToString("p", CultureInfo.InvariantCulture);
        }
        public static string Translate(this string value)
        {
            try
            {
                return Resources.ResourceManager.GetString(value.NTrim()) ?? value;
            }
            catch (Exception)
            {

                return value;
            }
        }

        public static void ChangeLanguage(string lang)
        {
            var culture = new System.Globalization.CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
        #region Get Enum Code
        public static string Code(this EFamilyProductType enumType)
        {
            return enumType.ToString().Substring(0, 1).ToUpper();
        }
        public static string Code(this CalculateType enumType)
        {
            return enumType.ToString().Substring(0, 1).ToUpper();
        }
        public static string Code(this EPlanType enumType)
        {
            switch (enumType)
            {
                case EPlanType.Level:
                    return "F";
                case EPlanType.Incremental:
                    return "I";
                case EPlanType.NonInsured:
                    return "N";
                case EPlanType.Insured:
                    return "S";
                default:
                    return null;
            }
        }

        public static string Code(this ProductBehavior enumType)
        {
            switch (enumType)
            {
                case ProductBehavior.Horizon:
                    return "HRZ";
                case ProductBehavior.Axys:
                    return "AXS";
                case ProductBehavior.EduPlan:
                    return "EDU";
                case ProductBehavior.Scholar:
                    return "SCH";
                case ProductBehavior.CompassIndex:
                    return "CPI";
                case ProductBehavior.Legacy:
                    return "LEG";
                case ProductBehavior.Sentinel:
                    return "SNT";
                case ProductBehavior.Lighthouse:
                    return "LGT";
                case ProductBehavior.Guardian:
                    return "GRD";
                case ProductBehavior.GuardianPlus:
                    return "GRP";
                case ProductBehavior.Orion:
                    return "ORN";
                case ProductBehavior.OrionPlus:
                    return "ORP";
                case ProductBehavior.Luminis:
                    return "LUM";
                case ProductBehavior.LuminisVIP:
                    return "LUV";
                case ProductBehavior.Exequium:
                    return "EXE";
                case ProductBehavior.ExequiumVIP:
                    return "EXV";
                case ProductBehavior.ECONOMAX:
                    return "ECONO MAX";
                case ProductBehavior.None:
                default:
                    return "";
            }
        }
        public static string Code(this EContributionType enumType)
        {
            switch (enumType)
            {
                case EContributionType.Continuous:
                    return "C";
                case EContributionType.NumberOfYears:
                    return "Y";
                case EContributionType.UntilAge:
                    return "U";
                default:
                    return null;
            }
        }
        public static string Code(this RiderType enumType)
        {
            switch (enumType)
            {
                case RiderType.ACB:
                    return "A";
                case RiderType.ACDB:
                    return "C";
                case RiderType.Illness:
                    return "I";
                case RiderType.OIR:
                    return "O";
                case RiderType.Primary:
                    return "P";
                case RiderType.Term:
                    return "T";
                default:
                    return null;
            }
        }

        public static string Code(this IllustrationStatus status)
        {
            switch (status)
            {
                case IllustrationStatus.New:
                    return "NWI";
                case IllustrationStatus.NewPlan:
                    return "NWP";
                case IllustrationStatus.Illustration:
                    return "Illustration";
                case IllustrationStatus.Submitted:
                    return "SBT";
                case IllustrationStatus.Delete:
                    return "DEL";
                case IllustrationStatus.ApprovedBySubscription:
                    return "IAS";
                case IllustrationStatus.DeclinedByClient:
                    return "IDC";
                case IllustrationStatus.DeclinedBySubscription:
                    return "IDS";
                case IllustrationStatus.Issued:
                    return "IIS";
                case IllustrationStatus.PendingByClient:
                    return "IPC";
                case IllustrationStatus.TimeExpired:
                    return "ITE";
                case IllustrationStatus.TimeExpiring:
                    return "ITEI";
                case IllustrationStatus.Subscription:
                    return "ISU";
                case IllustrationStatus.Incomplete:
                    return "II";
                case IllustrationStatus.ApprovedByClient:
                    return "IAC";
                case IllustrationStatus.Effective:
                    return "EFECT";
                case IllustrationStatus.Complete:
                    return "IC";
                case IllustrationStatus.MissingDocuments:
                    return "IMD";
                case IllustrationStatus.MissingInspection:
                    return "IMI";
                case IllustrationStatus.Cancelled:
                    return "CANCEL";
                default:
                    return null;
            }
        }

        public static string ID(this IllustrationStatus status)
        {
            switch (status)
            {
                case IllustrationStatus.TimeExpired:
                    return "23";
                case IllustrationStatus.Subscription:
                    return "24";
                case IllustrationStatus.MissingInspection:
                    return "28";
                case IllustrationStatus.TimeExpiring:
                    return "30";
                default:
                    return null;
            }
        }

        public static string Type(this TransmissionType type)
        {
            switch (type)
            {
                case TransmissionType.Automatica:
                    return "AUT";
                case TransmissionType.Mecanica:
                    return "MEC";
                case TransmissionType.Sequencial:
                    return "SECUENCIAL";
                default:
                    return null;
            }
        }

        public enum InspeccionPropiedadCategoriaRiesgo
        {
            MuyBueno = 1,
            Bueno,
            Satisfactorio,
            PocoSatisfactorio,
            NoSatisfactorio
        }

        public static string Code(this InspeccionPropiedadCategoriaRiesgo enumType)
        {
            string categoria = string.Empty;
            switch (enumType)
            {
                case InspeccionPropiedadCategoriaRiesgo.MuyBueno: categoria = "Muy Bueno"; break;
                case InspeccionPropiedadCategoriaRiesgo.Bueno: categoria = "Bueno"; break;
                case InspeccionPropiedadCategoriaRiesgo.Satisfactorio: categoria = "Satisfactorio"; break;
                case InspeccionPropiedadCategoriaRiesgo.PocoSatisfactorio: categoria = "Poco Satisfactorio"; break;
                case InspeccionPropiedadCategoriaRiesgo.NoSatisfactorio: categoria = "No Satisfactorio"; break;
                default: categoria = string.Empty; break;
            }
            return categoria;
        }

        public enum InspeccionPropiedadTipoConstruccion
        {
            PrimeraClase = 1,
            PrimeraClaseEspecial,
            SegundaClase,
            SegundaClaseEspecial,
            TerceraClase,
            TerceraClaseEspecial,
            Superior
        }

        public static string Code(this InspeccionPropiedadTipoConstruccion enumType)
        {
            string tipo = string.Empty;
            switch (enumType)
            {
                case InspeccionPropiedadTipoConstruccion.PrimeraClase: tipo = "Primera Clase"; break;
                case InspeccionPropiedadTipoConstruccion.PrimeraClaseEspecial: tipo = "Primera Clase Especial"; break;
                case InspeccionPropiedadTipoConstruccion.SegundaClase: tipo = "Segunda Clase"; break;
                case InspeccionPropiedadTipoConstruccion.SegundaClaseEspecial: tipo = "Segunda Clase Especial"; break;
                case InspeccionPropiedadTipoConstruccion.TerceraClase: tipo = "Tercera Clase"; break;
                case InspeccionPropiedadTipoConstruccion.TerceraClaseEspecial: tipo = "Tercera Clase Especial"; break;
                case InspeccionPropiedadTipoConstruccion.Superior: tipo = "Superior"; break;
                default: tipo = string.Empty; break;
            }
            return tipo;
        }

        public static string TipoEdificacion(this InspeccionPropiedadTipoConstruccion enumType)
        {
            string tipo = string.Empty;
            switch (enumType)
            {
                case InspeccionPropiedadTipoConstruccion.PrimeraClase: tipo = "1ra. Clase"; break;
                case InspeccionPropiedadTipoConstruccion.PrimeraClaseEspecial: tipo = "1ra. Clase Especial"; break;
                case InspeccionPropiedadTipoConstruccion.SegundaClase: tipo = "2da. Clase"; break;
                case InspeccionPropiedadTipoConstruccion.SegundaClaseEspecial: tipo = "2da. Clase Especial"; break;
                case InspeccionPropiedadTipoConstruccion.TerceraClase: tipo = "3ra. Clase"; break;
                case InspeccionPropiedadTipoConstruccion.TerceraClaseEspecial: tipo = "3ra. Clase Especial"; break;
                case InspeccionPropiedadTipoConstruccion.Superior: tipo = "Superior"; break;
                default: tipo = string.Empty; break;
            }
            return tipo;
        }


        public enum InspeccionPropiedadTipoEdificio
        {
            Comercio = 1,
            Fabrica,
            Industria,
            Oficina,
            PlazaComercial,
            Vivienda
        }

        public static string Code(this InspeccionPropiedadTipoEdificio enumType)
        {
            string tipo = string.Empty;
            switch (enumType)
            {
                case InspeccionPropiedadTipoEdificio.Comercio: tipo = "Comercio"; break;
                case InspeccionPropiedadTipoEdificio.Fabrica: tipo = "Fabrica"; break;
                case InspeccionPropiedadTipoEdificio.Industria: tipo = "Industria"; break;
                case InspeccionPropiedadTipoEdificio.Oficina: tipo = "Oficina"; break;
                case InspeccionPropiedadTipoEdificio.PlazaComercial: tipo = "Plaza Comercial"; break;
                case InspeccionPropiedadTipoEdificio.Vivienda: tipo = "Vivienda"; break;
                default: tipo = string.Empty; break;
            }
            return tipo;
        }

        public enum InspeccionPropiedadCausaPerdida
        {
            DanosMaliciosos = 1,
            DanosPorAguaAccidental,
            DanosPorAguaLluvia,
            Explosion,
            Huelga,
            Huracan,
            Incendio,
            Inundacion,
            Motin,
            Rayo,
            RoboConEscalamiento,
            RoboConViolencia
        }

        public static string Code(this InspeccionPropiedadCausaPerdida enumType)
        {
            string tipo = string.Empty;
            switch (enumType)
            {
                case InspeccionPropiedadCausaPerdida.DanosMaliciosos: tipo = "Daños Maliciosos"; break;
                case InspeccionPropiedadCausaPerdida.DanosPorAguaAccidental: tipo = "Daños por Agua accidental"; break;
                case InspeccionPropiedadCausaPerdida.DanosPorAguaLluvia: tipo = "Daños por Agua de Lluvia"; break;
                case InspeccionPropiedadCausaPerdida.Explosion: tipo = "Explosion"; break;
                case InspeccionPropiedadCausaPerdida.Huelga: tipo = "Huelga"; break;
                case InspeccionPropiedadCausaPerdida.Huracan: tipo = "Huracan"; break;
                case InspeccionPropiedadCausaPerdida.Incendio: tipo = "Incendio"; break;
                case InspeccionPropiedadCausaPerdida.Inundacion: tipo = "Inundacion"; break;
                case InspeccionPropiedadCausaPerdida.Motin: tipo = "Motin"; break;
                case InspeccionPropiedadCausaPerdida.Rayo: tipo = "Rayo"; break;
                case InspeccionPropiedadCausaPerdida.RoboConEscalamiento: tipo = "Robo con Escalamiento"; break;
                case InspeccionPropiedadCausaPerdida.RoboConViolencia: tipo = "Robo con Violencia"; break;
                default: tipo = string.Empty; break;
            }
            return tipo;
        }

        public static string DB(this InspeccionPropiedadCausaPerdida enumType)
        {
            string tipo = string.Empty;
            switch (enumType)
            {
                case InspeccionPropiedadCausaPerdida.DanosMaliciosos: tipo = "Daños Maliciosos"; break;
                case InspeccionPropiedadCausaPerdida.DanosPorAguaAccidental: tipo = "Daños por Agua Accidental"; break;
                case InspeccionPropiedadCausaPerdida.DanosPorAguaLluvia: tipo = "Daños por Agua de Lluvia"; break;
                case InspeccionPropiedadCausaPerdida.Explosion: tipo = "Explosión"; break;
                case InspeccionPropiedadCausaPerdida.Huelga: tipo = "Huelga"; break;
                case InspeccionPropiedadCausaPerdida.Huracan: tipo = "Huracán"; break;
                case InspeccionPropiedadCausaPerdida.Incendio: tipo = "Incendio"; break;
                case InspeccionPropiedadCausaPerdida.Inundacion: tipo = "Inundación"; break;
                case InspeccionPropiedadCausaPerdida.Motin: tipo = "Motín"; break;
                case InspeccionPropiedadCausaPerdida.Rayo: tipo = "Rayo"; break;
                case InspeccionPropiedadCausaPerdida.RoboConEscalamiento: tipo = "Robo con Escalamiento"; break;
                case InspeccionPropiedadCausaPerdida.RoboConViolencia: tipo = "Robo con Violencia"; break;
                default: tipo = string.Empty; break;
            }
            return tipo;
        }

        #endregion


        /// <summary>
        /// Returns a MD5 hash as a string
        /// </summary>
        /// <param name="TextToHash">String to be hashed.</param>
        /// <returns>Hash as string.</returns>
        public static String GetMD5Hash(String TextToHash)
        {
            //Check wether data was passed
            if ((TextToHash == null) || (TextToHash.Length == 0))
            {
                return String.Empty;
            }

            //Calculate MD5 hash. This requires that the string is splitted into a byte[].
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(textToHash);

            //Convert result back to string.
            return System.BitConverter.ToString(result).Replace("-", "");
        }
        /// <summary>
        /// Author: Dirson Breton
        /// </summary>
        /// <returns></returns>
        public static decimal GetItbis()
        {
            decimal Result = 0;

            var itbis = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["ITBISPorc"], NumberFormatInfo.InvariantInfo);

            Result = itbis / 100;

            return Result;
        }

        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            var context = HttpContext.Current;
            var isCallbackRequest = false;// callback requests are ajax requests
            if (context != null && context.CurrentHandler != null && context.CurrentHandler is System.Web.UI.Page)
            {
                isCallbackRequest = ((System.Web.UI.Page)context.CurrentHandler).IsCallback;
            }
            return isCallbackRequest || (request["X-Requested-With"] == "XMLHttpRequest") || (request.Headers["X-Requested-With"] == "XMLHttpRequest");
        }
        public static string GetShortMonthFromIndex(int? index)
        {
            var month = "";
            switch (index)
            {
                case 1:
                    month = Resources.Jan;
                    break;
                case 2:
                    month = "Feb";
                    break;
                case 3:
                    month = "Mar";
                    break;
                case 4:
                    month = Resources.Apr;
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "Jun";
                    break;
                case 7:
                    month = "Jul";
                    break;
                case 8:
                    month = Resources.Aug;
                    break;
                case 9:
                    month = "Sep";
                    break;
                case 10:
                    month = "Oct";
                    break;
                case 11:
                    month = "Nov";
                    break;
                case 12:
                    month = Resources.Dec;
                    break;
            }
            return month;
        }

        public static Dictionary<int, string> GetListPeriods()
        {
            return new Dictionary<int, string>{
            {(int) Period.Periods.Monthly, Resources.Last13Month },
            {(int) Period.Periods.Quarterly, Resources.Quarterly },
            {(int) Period.Periods.Yearly, Resources.Yearly },
            {(int) Period.Periods.SeasonalMonth, Resources.SeasonalMonth },
            {(int) Period.Periods.SeasonalQuarter, Resources.SeasonalQuarter }
        };
        }

        public static Dictionary<int, string> GetListYears()
        {
            Dictionary<int, string> Years = new Dictionary<int, string>();
            Years.Add(0, Resources.All);
            for (int y = 0; y < 10; y++)
                Years.Add((y + 1), String.Format(Resources.LastNoYears, (y + 1)));

            return Years;
        }

        public static Dictionary<int, string> GetListQuarters()
        {
            return new Dictionary<int, string>{
            {1, "Q1" },
            {4, "Q2" },
            {7, "Q3" },
            {10, "Q4" }
        };
        }

        public static Dictionary<int, string> GetListMonths()
        {
            return new Dictionary<int, string>{
            {1, Resources.January },
            {2, Resources.February },
            {3, Resources.March },
            {4, Resources.April },
            {5, Resources.May1 },
            {6, Resources.June },
            {7, Resources.July },
            {8, Resources.August },
            {9, Resources.September },
            {10, Resources.October},
            {11, Resources.November },
            {12, Resources.December }
        };
        }

        /// <summary>
        /// Trae el primer mes en donde empieza un trimestre.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime StartQuarterDate(this DateTime date)
        {
            if (date.Month >= 1 && date.Month <= 3)
                date = new DateTime(date.Year, 1, 1);
            else if (date.Month >= 4 && date.Month <= 6)
                date = new DateTime(date.Year, 4, 1);
            else if (date.Month >= 7 && date.Month <= 9)
                date = new DateTime(date.Year, 7, 1);
            else if (date.Month >= 10 && date.Month <= 12)
                date = new DateTime(date.Year, 10, 1);
            return date;
        }

        /// <summary>
        /// Trae el primer mes en donde empieza un Semestre.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime StartSemestreDate(this DateTime date)
        {
            if (date.Month >= 1 && date.Month <= 6)
                date = new DateTime(date.Year, 1, 1);
            if (date.Month >= 7 && date.Month <= 12)
                date = new DateTime(date.Year, 7, 1);
            return date;
        }

        /// <summary>
        /// Trae el último mes en donde empieza un trimestre.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime EndQuarterDate(this DateTime date)
        {
            if (date.Month >= 1 && date.Month <= 3)
                date = new DateTime(date.Year, 4, 1).AddMilliseconds(-1);
            else if (date.Month >= 4 && date.Month <= 6)
                date = new DateTime(date.Year, 7, 1).AddMilliseconds(-1);
            else if (date.Month >= 7 && date.Month <= 9)
                date = new DateTime(date.Year, 10, 1).AddMilliseconds(-1);
            else if (date.Month >= 10 && date.Month <= 12)
                date = new DateTime(date.Year + 1, 1, 1).AddMilliseconds(-1);
            return date;
        }

        /// <summary>
        /// Trae la lista de fechas por el período seleccionado
        /// </summary>
        /// <param name="dateToCalc">Fecha inicial</param>
        /// <param name="period">Período seleccionado</param>
        /// <param name="periodQuantity">Cantidad de períodos a traer</param>
        /// <returns>Lista de fechas</returns>
        public static List<Period> GetPeriodsDate(this DateTime dateToCalc, Period.Periods period, int periodQuantity = 13)
        {
            var lstPeriods = new List<Period>();
            DateTime date;
            for (int i = 0; i < periodQuantity; i++)
            {
                var periodModel = new Period { Index = i };

                switch (period)
                {
                    case Period.Periods.Daily:
                        date = dateToCalc.AddDays(-i);
                        periodModel.StartDate = date.Date;
                        periodModel.EndDate = date.Date.AddDays(1).AddMilliseconds(-1);
                        break;
                    case Period.Periods.Quarterly:
                        date = dateToCalc.AddMonths(-i * 3);
                        periodModel.StartDate = date.StartQuarterDate();
                        periodModel.EndDate = date.EndQuarterDate();
                        break;
                    case Period.Periods.SeasonalMonth:
                        date = dateToCalc.AddYears(-i);
                        periodModel.StartDate = date.FirstDayOfMonth();
                        periodModel.EndDate = date.LastDayOfMonth();
                        break;
                    case Period.Periods.SeasonalQuarter:
                        date = dateToCalc.AddYears(-i);
                        periodModel.StartDate = date.StartQuarterDate();
                        periodModel.EndDate = date.EndQuarterDate();
                        break;
                    case Period.Periods.Yearly:
                        date = dateToCalc.AddYears(-i);
                        periodModel.StartDate = new DateTime(date.Year, 1, 1);
                        periodModel.EndDate = new DateTime(date.Year, 12, 31);
                        break;
                    default:
                        date = dateToCalc.AddMonths(-i);
                        periodModel.StartDate = date.FirstDayOfMonth();
                        periodModel.EndDate = date.LastDayOfMonth();
                        break;
                }

                periodModel.Month = date.Month;
                periodModel.Year = date.Year;
                lstPeriods.Add(periodModel);
            }
            return lstPeriods;
        }

        public static string GetQuarterFromMonth(int? month)
        {
            var quarter = "";
            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    quarter = "Q1";
                    break;
                case 4:
                case 5:
                case 6:
                    quarter = "Q2";
                    break;
                case 7:
                case 8:
                case 9:
                    quarter = "Q3";
                    break;
                case 10:
                case 11:
                case 12:
                    quarter = "Q4";
                    break;
            }
            return quarter;
        }

        public static string GetJSPivotEndCallbackClickHandler(string addScript = null)
        {
            return string.Format(@"function(){{ 
                                    configurePivotColumn();
                                    {0}
                                    EndRequestHandler(); 
                                    addtxtFilterToFilterGrid(); }}", addScript);
        }

        public static string GetSystemVersionInfo()
        {
            var currAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var v = currAssembly.GetName().Version;
            var stringVersion = v.Major + "." + v.Minor + "." + v.Build + "." + v.Revision;

            return stringVersion;
        }

        public static string VehicleInspectionFormPhotos { get { return "VIFPhotos"; } }
        public static string AlliedLinesInspectionFormPhotos { get { return "ALIFPhotos"; } }

        public static int PropertyNumberOfPhotos(Utility.AlliedLinePropertyPhotos type, int category)
        {
            /*
                Internas:    1 - Techo: (5)
                             2 - Área trabajo/Operaciones: (3)
                             3 - Almacenes: (3)
                             4 - Existencias: (3)
                             5 - Materia Prima: (4)
                             6 - Cocina: (2)
                             7 - Sala: (3)
                             8 - Piscina: (3)
                             9 - Habitación: (3)
                            10 - Otros: (3)
             
                Externas:    1 - Parte frontal: (2)
                             2 - Lateral derecho: (2)
                             3 - Lateral izquierdo (2)
                             4 - Parte trasera: (2)
                             5 - Generador eléctrico: (1)
                             6 - Inversor: (1)
                             7 - Cuarto de equipos: (6)
                             8 - Otros: (2)
            */

            int result = 0;
            switch (category)
            {
                case 0: result = -1; break;
                case 1: result = type == AlliedLinePropertyPhotos.Internal ? 5 : 2; break;
                case 2: result = type == AlliedLinePropertyPhotos.Internal ? 3 : 2; break;
                case 3: result = type == AlliedLinePropertyPhotos.Internal ? 3 : 2; break;
                case 4: result = type == AlliedLinePropertyPhotos.Internal ? 3 : 2; break;
                case 5: result = type == AlliedLinePropertyPhotos.Internal ? 4 : 1; break;
                case 6: result = type == AlliedLinePropertyPhotos.Internal ? 2 : 1; break;
                case 7: result = type == AlliedLinePropertyPhotos.Internal ? 3 : 6; break;
                case 8: result = type == AlliedLinePropertyPhotos.Internal ? 3 : 2; break;
                case 9: result = 3; break;
                case 10: result = 3; break;
            }
            return result;
        }

        public static string ReplaceVowels(string text)
        {
            return text.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
        }

        /// <summary>
        /// Author: Marcos J. Pérez
        /// Compresión de imágenes
        /// </summary>
        /// <param name="fileBytes">Imágen a comprimir</param>
        /// <param name="compression">% de compresión. Si es 0 comprime al máximo sin perdida de calidad</param>
        /// <returns>Imágen comprimida</returns>
        public static byte[] CompressImage(byte[] fileBytes, long compression)
        {
            MemoryStream ms = new MemoryStream();

            try
            {
                Bitmap bitmap = new Bitmap(new MemoryStream(fileBytes, false));
                if (bitmap == null)
                    return new byte[] { };

                ImageCodecInfo encoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(e => e.FormatID == ImageFormat.Jpeg.Guid);

                System.Drawing.Imaging.Encoder myEncoder = compression == 0 ? System.Drawing.Imaging.Encoder.Compression : System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, compression);
                myEncoderParameters.Param[0] = myEncoderParameter;

                bitmap.Save(ms, encoder, myEncoderParameters);
            }
            catch { }

            ms.Position = 0;

            return ms.ToArray();
        }

        /// <summary>
        /// Author: Marcos J. Pérez
        /// Compresión de PDF
        /// </summary>
        /// <param name="pdf">PDF a comprimir</param>
        /// <returns>PDF comprimido</returns>
        public static byte[] CompressPDF(byte[] pdf)
        {
            byte[] result = new byte[] { };

            PdfReader reader = new PdfReader(pdf);
            for (int i = 1; i <= reader.NumberOfPages; i++)
                reader.SetPageContent(i, reader.GetPageContent(i), PdfStream.BEST_COMPRESSION);

            using (MemoryStream memStream = new MemoryStream())
            {
                PdfStamper stamper = new PdfStamper(reader, memStream, PdfWriter.VERSION_1_7);
                stamper.FormFlattening = true;
                stamper.SetFullCompression();
                stamper.Writer.CompressionLevel = PdfStream.BEST_COMPRESSION;
                stamper.Writer.SetFullCompression();
                stamper.Close();

                result = memStream.ToArray();
            }

            return result;
        }

        #region Pivots
        public static string GetValueFromFormatCurrency(string currency = "USD", decimal? value = null)
        {
            if (!value.HasValue) return null;
            string language = "en-US";
            switch (currency)
            {
                case "EUR":
                    language = "fr-FR";
                    break;
            }
            return string.Format(new CultureInfo(language), "{0:c}", value);
        }

        public static string GetJSPivotEndCallbackClickHandler(string pivotGridId, string addScript = null)
        {
            return string.Format(@"function(){{ 
                                    IncludeScrollRowArea('#{0}'); 
                                    configurePivotColumn();
                                    {1}
                                    $('#loading').hide(); 
                                    addtxtFilterToFilterGrid(); }}", pivotGridId, addScript);
        }

        public static Dictionary<int, string> GetListSemestres()
        {
            return new Dictionary<int, string>
            {
                {1, "S1" },
                {7, "S2" }
            };
        }

        public static string GetFormatCurrency(string currency = "USD")
        {
            string format = string.Empty;
            switch (currency)
            {
                case "EUR":
                    format = "€#.##0,00";
                    break;
                default:
                    format = "$#,##0.00";
                    break;
            }
            return format;
        }

        public static string GetValueFromFormatPercent(string currency = "USD", decimal? value = null)
        {
            if (!value.HasValue) return null;
            string language = "en-US";
            switch (currency)
            {
                case "EUR":
                    language = "fr-FR";
                    break;
            }
            return string.Format(new CultureInfo(language), "{p1}", value);
        }

        public static string GetSemestralFromMonth(int? month)
        {
            var semestral = "";
            switch (month)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    semestral = "S1";
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                    semestral = "S2";
                    break;
            }
            return semestral;
        }
        #endregion

        public static DataTable AsDataTable<T>(IEnumerable<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        /// <summary>
        /// Author: Marcos J. Pérez
        /// Remove CR character from string
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string without CR character</returns>
        public static string RemoveCR(this string value)
        {
            return Regex.Replace(value, @"\t|\n|\r", "");
        }

        public static string GetTabName(string tab)
        {
            var TabSelected = (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), tab);
            string TabName = string.Empty;

            switch (TabSelected)
            {
                case Utility.Tabs.lnkIllustrationsToWork:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.IllustrationsToWork;
                    break;
                case Utility.Tabs.lnkIncompleteIllustration:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.IncompleteIllustrations;
                    break;
                case Utility.Tabs.lnkCompleteIllustrations:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.CompleteIllustrations;
                    break;
                case Utility.Tabs.lnkDeclinedByClient:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.DeclinedByClient;
                    break;
                case Utility.Tabs.lnkExpired:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.Expired;
                    break;
                case Utility.Tabs.lnkExpiring:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.Expiring;
                    break;
                case Utility.Tabs.lnkSubscriptions:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.Subscription;
                    break;
                case Utility.Tabs.lnkDeclinedBySubscription:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.DeclinedBySubscription;
                    break;
                case Utility.Tabs.lnkMissingDocuments:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.MissingDocuments;
                    break;
                case Utility.Tabs.lnkMissingInspections:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.MissingInspections;
                    break;
                case Utility.Tabs.lnkApprovedBySubscription:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.ApprovedBySubscription;
                    break;
                case Utility.Tabs.lnkHistoricalIllustrations:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.IllustrationHistoric;
                    break;
                case Utility.Tabs.lnkConfirmationCall:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.ConfirmationCall;
                    break;
                case Utility.Tabs.lnkDiscounts:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.Discount;
                    break;
                case Utility.Tabs.lnkFacultative:
                    TabName = RESOURCE.UnderWriting.NewBussiness.Resources.FacultativesCases;
                    break;
            }

            return TabName;
        }

        public class Percent_Flotilla_Discount
        {
            public int From { get; set; }
            public int To { get; set; }
            public decimal Porc { get; set; }
        }


        public static string getFuelTypeDesc(string _rateJsonSysFlex)
        {
            Utility.rateJsonSysFlex rateJsonSysFlex;
            string fuelTypeDesc = "No Definido";

            if (!string.IsNullOrEmpty(_rateJsonSysFlex))
            {
                rateJsonSysFlex = Utility.deserializeJSON<Utility.rateJsonSysFlex>(_rateJsonSysFlex.Replace("[", "").Replace("]", ""));
                fuelTypeDesc = string.IsNullOrEmpty(rateJsonSysFlex.tipoCombustible) ? "No Definido" : rateJsonSysFlex.tipoCombustible;
            }

            return fuelTypeDesc;
        }


        public static bool FuelTypeMatch(string fueltype, string radioDesc)
        {
            bool f = false;

            Dictionary<string, string> dicFuelTypes = new Dictionary<string, string>();
            dicFuelTypes.Add("Gasolina", "Gasolina".ToLower());
            dicFuelTypes.Add("Gasoil", "Diesel o Gasoil".ToLower());
            dicFuelTypes.Add("Gas Propano", "Glp".ToLower());
            dicFuelTypes.Add("Gas Natural", "Gas Natural".ToLower());
            dicFuelTypes.Add("Electrico", "Electrico / Gasolina".ToLower());
            dicFuelTypes.Add("Hibrido", "");
            dicFuelTypes.Add("Hidrogeno", "");
            dicFuelTypes.Add("Panel Solar", "");
            dicFuelTypes.Add("Gasolina/Gas Propano", "Gasolina / Glp".ToLower());
            dicFuelTypes.Add("Gasolina/Gas Natural", "Gasolina / Gas natural".ToLower());

            var match = dicFuelTypes.ContainsValue(fueltype.ToLower());
            if (match)
            {
                var key = dicFuelTypes.FirstOrDefault(x => x.Value == fueltype.ToLower());
                if (key.Key == radioDesc)
                {
                    f = true;
                }
            }
            return f;
        }

        public static string FuelTypeMatchInverted(string InspectionFuelTypeDesc)
        {

            Dictionary<string, string> dicFuelTypes = new Dictionary<string, string>();
            dicFuelTypes.Add("Gasolina", "Gasolina");
            dicFuelTypes.Add("Diesel o Gasoil", "Gasoil");
            dicFuelTypes.Add("Glp", "Gas Propano");
            dicFuelTypes.Add("Gas Natural", "Gas Natural");
            dicFuelTypes.Add("Electrico / Gasolina", "Electrico");
            if (InspectionFuelTypeDesc == "Hibrido" || InspectionFuelTypeDesc == "Hidrogeno" || InspectionFuelTypeDesc == "Panel Solar")
            {
                dicFuelTypes.Add(("No Definido - " + InspectionFuelTypeDesc), InspectionFuelTypeDesc);
            }
            dicFuelTypes.Add("Gasolina / Glp", "Gasolina/Gas Propano");
            dicFuelTypes.Add("Gasolina / Gas natural", "Gasolina/Gas Natural");

            var match = dicFuelTypes.ContainsValue(InspectionFuelTypeDesc);
            if (match)
            {
                var key = dicFuelTypes.FirstOrDefault(x => x.Value == InspectionFuelTypeDesc);
                return key.Key;
            }
            return "No Definido";
        }
    }

    #region Period
    public class Period
    {
        public int Index { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public enum Periods
        {
            Monthly = 1,
            Quarterly,
            Yearly,
            SeasonalMonth,
            SeasonalQuarter,
            Daily,
            Last3Month,
            Last6Month,
            Last13Month,
            Custom,
            ActualMonth,
            Semestral,
            SeasonalSemestral
        }

        public enum DateRanges
        {
            Last = 1,
            Current = 2,
            MonthToDate = 3,
            YearToDate = 4,
            FirstQuarter = 5,
            SecondQuarter = 6,
            ThirdQuarter = 7,
            FourthQuarter = 8
        }
    }
    #endregion

    #region Pivot

    public class MonthTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            var fieldValue = (PivotGridFieldValueTemplateContainer)container;
            if (fieldValue.Value != null)
                container.Controls.Add(new LiteralControl(Utility.GetShortMonthFromIndex((int)fieldValue.Value)));
        }

        public static void AddMonthToPivot(ASPxPivotGrid grid)
        {
            grid.Fields.Add(new DevExpress.Web.ASPxPivotGrid.PivotGridField
            {
                FieldName = "PivotMonth",
                Area = PivotArea.ColumnArea,
                TotalsVisibility = PivotTotalsVisibility.None,
                SortOrder = PivotSortOrder.Descending
            });
            grid.Fields["PivotMonth"].Options.AllowDrag = DefaultBoolean.False;
            grid.Fields["PivotMonth"].Options.AllowSort = DefaultBoolean.False;
            grid.Fields["PivotMonth"].Options.AllowFilter = DefaultBoolean.True;
        }

        public static void AddSemestralToPivot(ASPxPivotGrid grid)
        {
            grid.Fields.Add(new DevExpress.Web.ASPxPivotGrid.PivotGridField
            {
                FieldName = "PivotSemestral",
                Area = PivotArea.ColumnArea,
                TotalsVisibility = PivotTotalsVisibility.None,
                SortOrder = PivotSortOrder.Descending
            });
            grid.Fields["PivotSemestral"].Options.AllowDrag = DefaultBoolean.False;
            grid.Fields["PivotSemestral"].Options.AllowSort = DefaultBoolean.False;
            grid.Fields["PivotSemestral"].Options.AllowFilter = DefaultBoolean.False;
        }

        public static void AddAmountToPivot(ASPxPivotGrid grid, string field)
        {
            grid.Fields.Add(new DevExpress.Web.ASPxPivotGrid.PivotGridField
            {
                FieldName = field,
                Area = PivotArea.DataArea
            });
            grid.Fields[field].CellFormat.FormatString = "c2";
            grid.Fields[field].CellFormat.FormatType = FormatType.Numeric;
            grid.Fields[field].CellStyle.ForeColor = System.Drawing.Color.Blue;
        }

        public static void AddCountToPivot(ASPxPivotGrid grid)
        {
            grid.Fields.Add(new DevExpress.Web.ASPxPivotGrid.PivotGridField
            {
                FieldName = "Count",
                Area = PivotArea.DataArea
            });
            grid.Fields["Count"].CellStyle.ForeColor = System.Drawing.Color.Blue;
        }

        public static void AddQuarterToPivot(ASPxPivotGrid grid)
        {
            grid.Fields.Add(new DevExpress.Web.ASPxPivotGrid.PivotGridField
            {
                FieldName = "PivotQuarter",
                Area = PivotArea.ColumnArea,
                TotalsVisibility = PivotTotalsVisibility.None,
                SortOrder = PivotSortOrder.Descending
            });
            grid.Fields["PivotQuarter"].Options.AllowDrag = DefaultBoolean.False;
            grid.Fields["PivotQuarter"].Options.AllowSort = DefaultBoolean.False;
            grid.Fields["PivotQuarter"].Options.AllowFilter = DefaultBoolean.False;
        }

        public static void AddYearToPivot(ASPxPivotGrid grid)
        {
            grid.Fields.Add(new DevExpress.Web.ASPxPivotGrid.PivotGridField
            {
                FieldName = "Year",
                Area = PivotArea.ColumnArea,
                TotalsVisibility = PivotTotalsVisibility.None,
                SortOrder = PivotSortOrder.Descending
            });
            grid.Fields["Year"].Options.AllowDrag = DefaultBoolean.False;
            grid.Fields["Year"].Options.AllowSort = DefaultBoolean.False;
            grid.Fields["Year"].Options.AllowFilter = DefaultBoolean.False;
            grid.Fields["Year"].TotalsVisibility = PivotTotalsVisibility.AutomaticTotals;
        }

        public static void ConfigMonthToPivot(ASPxPivotGrid grid, PivotArea PivotArea)
        {
            if (grid.Fields["PivotMonth"] != null)
            {
                if (PivotArea == DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                {
                    grid.Fields["PivotMonth"].TotalsVisibility = PivotTotalsVisibility.None;
                    grid.Fields["PivotMonth"].Area = PivotArea;
                    grid.Fields["PivotMonth"].SortOrder = PivotSortOrder.Descending;
                    grid.Fields["PivotMonth"].Options.AllowDrag = DefaultBoolean.False;
                    grid.Fields["PivotMonth"].Options.AllowSort = DefaultBoolean.False;
                    grid.Fields["PivotMonth"].Options.AllowFilter = DefaultBoolean.True;
                }
                else
                {
                    grid.Fields["PivotMonth"].TotalsVisibility = PivotTotalsVisibility.AutomaticTotals;
                    grid.Fields["PivotMonth"].Area = PivotArea;
                    grid.Fields["PivotMonth"].SortOrder = PivotSortOrder.Descending;
                    grid.Fields["PivotMonth"].Options.AllowDrag = DefaultBoolean.True;
                    grid.Fields["PivotMonth"].Options.AllowSort = DefaultBoolean.True;
                    grid.Fields["PivotMonth"].Options.AllowFilter = DefaultBoolean.True;
                }
            }
            else if (grid.Fields["PivotQuarter"] != null)
            {
                if (PivotArea == DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                    grid.Fields["PivotQuarter"].TotalsVisibility = PivotTotalsVisibility.None;
                else
                    grid.Fields["PivotQuarter"].TotalsVisibility = PivotTotalsVisibility.AutomaticTotals;

                grid.Fields["PivotQuarter"].Area = PivotArea;
                grid.Fields["PivotQuarter"].SortOrder = PivotSortOrder.Descending;
                grid.Fields["PivotQuarter"].Options.AllowDrag = DefaultBoolean.True;
                grid.Fields["PivotQuarter"].Options.AllowSort = DefaultBoolean.True;
                grid.Fields["PivotQuarter"].Options.AllowFilter = DefaultBoolean.False;
            }
        }
    }
    #endregion



}