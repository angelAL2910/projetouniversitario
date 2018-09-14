﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.UnderWriting.Entities
{
    public class Transport
    {
        public class Insured
        {
            public int? CorpId { get; set; }
            public int? RegionId { get; set; }
            public int? CountryId { get; set; }
            public int? DomesticRegId { get; set; }
            public int? StateProvId { get; set; }
            public int? CityId { get; set; }
            public int? OfficeId { get; set; }
            public int? CaseSeqNo { get; set; }
            public int? HistSeqNo { get; set; }
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
            public string billingType { get; set; }
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
            public int? TransportStatusId { get; set; }
            public int? UserId { get; set; }
            public string SourceId { get; set; }
            public string ProductDesc { get; set; }
            public Nullable<int> ProductTypeId { get; set; }
            public Nullable<decimal> ReinsurancePercentage { get; set; }
            public bool AppliesToReinsurance { get; set; }
            public decimal? ReinsurancePremiumAmount { get; set; }
            public string AddressCountryDesc { get; set; }
            public string AddressStateProvDesc { get; set; }
            public string AddressMunicipioDesc { get; set; }
            public string AddressCityDesc { get; set; }
            public int? Ramo { get; set; }
            public int? SubRamo { get; set; }

            public class Coverage
            {
                public int? CorpId { get; set; }
                public long? UniqueTransportId { get; set; }
                public int? RegionId { get; set; }
                public int? CountryId { get; set; }
                public int? BlTypeId { get; set; }
                public int? BlId { get; set; }
                public int? ProductId { get; set; }
                public int? VehicleTypeId { get; set; }
                public int? GroupId { get; set; }
                public int? CoverageTypeId { get; set; }
                public int? CoverageId { get; set; }
                public int? CurrencyId { get; set; }
                public decimal? UnitaryPrice { get; set; }
                public decimal? PackagePrice { get; set; }
                public decimal? DeductibleAmount { get; set; }
                public decimal? DeductiblePercentage { get; set; }
                public decimal? ManualDeductibleAmount { get; set; }
                public decimal? ManualDeductiblePercentage { get; set; }
                public decimal? CoverageLimit { get; set; }
                public bool? CoverageStatus { get; set; }
                public int? UserId { get; set; }
                public string SourceId { get; set; }
                public string CoverageTypeDesc { get; set; }
                public string GroupDesc { get; set; }
                public string CoverageDesc { get; set; }
                public int? Ramo { get; set; }
                public int? SubRamo { get; set; }
                public string RamoDesc { get; set; }
                public string SubRamoDesc { get; set; }
                public decimal? CoveragePercentage { get; set; }
                public decimal? PremiumPercentage { get; set; }
                public decimal? CoinsurancePercentage { get; set; }
                public string BaseDeducible { get; set; }

                public class Key
                {
                    public string Action { get; set; }
                    public int CorpId { get; set; }
                    public int UniqueTransportId { get; set; }
                    public int RegionId { get; set; }
                    public int CountryId { get; set; }
                    public int BlTypeId { get; set; }
                    public int BlId { get; set; }
                    public int ProductId { get; set; }
                    public int VehicleTypeId { get; set; }
                    public int GroupId { get; set; }
                    public int CoverageTypeId { get; set; }
                    public int CoverageId { get; set; }
                }

                public class Surcharge
                {
                    public int CorpId { get; set; }
                    public int RegionId { get; set; }
                    public int CountryId { get; set; }
                    public long uniqueTransportId { get; set; }
                    public int BlTypeId { get; set; }
                    public int BlId { get; set; }
                    public int ProductId { get; set; }
                    public int VehicleTypeId { get; set; }
                    public int GroupId { get; set; }
                    public int CoverageTypeId { get; set; }
                    public int CoverageId { get; set; }
                    public int SurchargeId { get; set; }
                    public int DiscountRuleId { get; set; }
                    public int DiscountRuleDetailId { get; set; }
                    public decimal OldCoverageAmount { get; set; }
                    public int? NotePredefiniedId { get; set; }
                    public string DiscountRuleDesc { get; set; }
                    public string DiscountRuleNameKey { get; set; }
                    public DateTime DetailApplyDate { get; set; }
                    public string DetailRuleValue { get; set; }
                    public string DetailRuleNameKey { get; set; }
                    public string NotePredefiniedDesc { get; set; }
                    public string NoteNameKey { get; set; }
                    public decimal BasePremiumAmount { get; set; }
                    public int UserId { get; set; }
                    public bool SurchargeStatus { get; set; }
                    public string TipoRecargo { get; set; }
                    public string PorcentajeRecargo { get; set; }
                    public decimal Recargo { get; set; }
                    public decimal MontoRecargo { get; set; }

                    public string RecargoF { get; set; }
                    public string MontoRecargoF { get; set; }

                    public class Key
                    {
                        public string Action { get; set; }
                        public int CorpId { get; set; }
                        public int RegionId { get; set; }
                        public int CountryId { get; set; }
                        public long UniqueTransportId { get; set; }
                        public int BlTypeId { get; set; }
                        public int BlId { get; set; }
                        public int ProductId { get; set; }
                        public int VehicleTypeId { get; set; }
                        public int GroupId { get; set; }
                        public int CoverageTypeId { get; set; }
                        public int CoverageId { get; set; }
                        public int? SurchargeId { get; set; }
                        public int? discountRuleId { get; set; }
                        public int? discountRuleDetailId { get; set; }
                        public int languageId { get; set; }
                    }
                }

            }

            public class Discount
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
                public int TransportId { get; set; }
                public int DiscountId { get; set; }
                public int DiscountRuleId { get; set; }
                public int DiscountRuleDetailId { get; set; }
                public int NotePredefiniedId { get; set; }
                public decimal PremiumAmount { get; set; }
                public decimal OldPremiumAmount { get; set; }
                public System.DateTime DetailApplyDate { get; set; }
                public string DetailRuleValue { get; set; }
                public string DetailRuleNameKey { get; set; }
                public string DiscountRuleDesc { get; set; }
                public string DiscountNameKey { get; set; }
                public string NotePredefiniedDesc { get; set; }
                public string NoteNameKey { get; set; }
                public string Comment { get; set; }
                public string FullName { get; set; }
                public bool? DiscountStatus { get; set; }
                public int UserId { get; set; }

                public string TipoDescuento { get; set; }
                public string PorcentajeDescuento { get; set; }
                public decimal Descuento { get; set; }
                public decimal MontoDescuento { get; set; }
                public string MontoDescuentoF { get; set; }
                public bool VisibleButton { get; set; }



                public class Key
                {
                    public string Action { get; set; }
                    public int CorpId { get; set; }
                    public int RegionId { get; set; }
                    public int CountryId { get; set; }
                    public int DomesticregId { get; set; }
                    public int StateProvId { get; set; }
                    public int CityId { get; set; }
                    public int OfficeId { get; set; }
                    public int CaseSeqNo { get; set; }
                    public int HistSeqNo { get; set; }
                    public int TransportId { get; set; }
                    public int? DiscountId { get; set; }
                    public int LanguageId { get; set; }
                }
            }

            public class ExtraInfo
            {
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
                public Nullable<int> TransportInsuredId { get; set; }
                public string TransportInsuredDesc { get; set; }

                public class Key
                {
                    public int? CorpId { get; set; }
                    public int? UniqueTransportId { get; set; }
                    public int? SeqId { get; set; }
                    public string Name { get; set; }
                    public string Brand { get; set; }
                    public string Model { get; set; }
                    public string Plate { get; set; }
                    public string Vin { get; set; }
                    public Nullable<int> Year { get; set; }
                    public string SerialKey { get; set; }
                    public int TransportExtraInfoStatusId { get; set; }
                    public int? UserId { get; set; }
                    public string SourceId { get; set; }
                }

                public class Surcharge
                {
                    public int CorpId { get; set; }
                    public int RegionId { get; set; }
                    public int CountryId { get; set; }
                    public long uniqueTransportId { get; set; }
                    public int BlTypeId { get; set; }
                    public int BlId { get; set; }
                    public int ProductId { get; set; }
                    public int VehicleTypeId { get; set; }
                    public int GroupId { get; set; }
                    public int CoverageTypeId { get; set; }
                    public int CoverageId { get; set; }
                    public int SurchargeId { get; set; }
                    public int DiscountRuleId { get; set; }
                    public int DiscountRuleDetailId { get; set; }
                    public decimal OldCoverageAmount { get; set; }
                    public int? NotePredefiniedId { get; set; }
                    public string DiscountRuleDesc { get; set; }
                    public string DiscountRuleNameKey { get; set; }
                    public DateTime DetailApplyDate { get; set; }
                    public string DetailRuleValue { get; set; }
                    public string DetailRuleNameKey { get; set; }
                    public string NotePredefiniedDesc { get; set; }
                    public string NoteNameKey { get; set; }
                    public decimal BasePremiumAmount { get; set; }
                    public int UserId { get; set; }
                    public bool SurchargeStatus { get; set; }
                    public string TipoRecargo { get; set; }
                    public string PorcentajeRecargo { get; set; }
                    public decimal Recargo { get; set; }
                    public decimal MontoRecargo { get; set; }

                    public string RecargoF { get; set; }
                    public string MontoRecargoF { get; set; }

                    public class Key
                    {
                        public string Action { get; set; }
                        public int CorpId { get; set; }
                        public int RegionId { get; set; }
                        public int CountryId { get; set; }
                        public long UniqueTransportId { get; set; }
                        public int BlTypeId { get; set; }
                        public int BlId { get; set; }
                        public int ProductId { get; set; }
                        public int VehicleTypeId { get; set; }
                        public int GroupId { get; set; }
                        public int CoverageTypeId { get; set; }
                        public int CoverageId { get; set; }
                        public int? SurchargeId { get; set; }
                        public int? discountRuleId { get; set; }
                        public int? discountRuleDetailId { get; set; }
                        public int languageId { get; set; }
                    }
                }

            }

            public class Key
            {
                public string Action { get; set; }
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
            }
        }
    }
}