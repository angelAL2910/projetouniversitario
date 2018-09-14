﻿using System;

namespace Entity.UnderWriting.Entities
{
    public class Bail
    {
        public class Insured
        {
            public Nullable<int> CorpId { get; set; }
            public Nullable<int> RegionId { get; set; }
            public Nullable<int> CountryId { get; set; }
            public Nullable<int> DomesticregId { get; set; }
            public Nullable<int> StateProvId { get; set; }
            public Nullable<int> CityId { get; set; }
            public Nullable<int> OfficeId { get; set; }
            public Nullable<int> CaseSeqNo { get; set; }
            public Nullable<int> HistSeqNo { get; set; }
            public Nullable<int> BailId { get; set; }
            public Nullable<int> BlTypeId { get; set; }
            public Nullable<int> BlId { get; set; }
            public Nullable<int> ProductId { get; set; }
            public Nullable<int> ReinsuranceId { get; set; }
            public Nullable<decimal> ReinsuranceAmount { get; set; }
            public Nullable<int> EquipmentQty { get; set; }
            public Nullable<decimal> ContractAmount { get; set; }
            public string Beneficiary { get; set; }
            public string Activity { get; set; }
            public string BusinessType { get; set; }
            public string BailType { get; set; }
            public string PercentageInsuredAmount { get; set; }
            public string Obligations { get; set; }
            public string ToDepositIn { get; set; }
            public string AddressStreet { get; set; }
            public string AddressNumber { get; set; }
            public Nullable<int> AddressCountryId { get; set; }
            public Nullable<int> AddressDomesticregId { get; set; }
            public Nullable<int> AddressStateProvId { get; set; }
            public Nullable<int> AddressCityId { get; set; }
            public string HasEndOfTerm { get; set; }
            public string IsBuilding { get; set; }
            public Nullable<System.DateTime> InsuredDate { get; set; }
            public Nullable<decimal> InsuredAmount { get; set; }
            public Nullable<decimal> Rate { get; set; }
            public Nullable<decimal> PremiumAmount { get; set; }
            public Nullable<decimal> BasePremiumAmount { get; set; }
            public Nullable<decimal> DeductiblePercentage { get; set; }
            public Nullable<decimal> DeductibleAmount { get; set; }
            public Nullable<decimal> MinimumDeductibleAmount { get; set; }
            public bool? IsNew { get; set; }
            public Nullable<bool> RequiresInspection { get; set; }
            public Nullable<bool> Reinsurance { get; set; }
            public Nullable<bool> Inspected { get; set; }
            public Nullable<bool> EndorsementClarifying { get; set; }
            public Nullable<bool> Endorsement { get; set; }
            public string EndorsementBeneficiary { get; set; }
            public string EndorsementBeneficiaryRnc { get; set; }
            public Nullable<decimal> EndorsementAmount { get; set; }
            public string EndorsementContactName { get; set; }
            public string EndorsementContactPhone { get; set; }
            public string EndorsementContactEmail { get; set; }
            public string InspectionAddress { get; set; }
            public Nullable<int> BailStatusId { get; set; }
            public Nullable<int> UsrId { get; set; }
            public string SourceId { get; set; }
            public bool AppliesToReinsurance { get; set; }
            public long UniqueBailId { get; set; }
            public string ProductDesc { get; set; }
            public int? ProductTypeId { get; set; }
            public decimal? ReinsurancePercentage { get; set; }
            public decimal? ReinsurancePremiumAmount { get; set; }
            public int? Ramo { get; set; }
            public int? SubRamo { get; set; }
            public string AddressCountryDesc { get; set; }
            public string AddressDomesticregDesc { get; set; }
            public string AddressStateProvDesc { get; set; }
            public string AddressMunicipioDesc { get; set; }
            public string AddressCityDesc { get; set; }

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
                public int BailId { get; set; }
                public int DiscountId { get; set; }
                public int DiscountRuleId { get; set; }
                public int DiscountRuleDetailId { get; set; }
                public int NotePredefiniedId { get; set; }
                public decimal PremiumAmount { get; set; }
                public decimal OldPremiumAmount { get; set; }
                public DateTime DetailApplyDate { get; set; }
                public string DetailRuleValue { get; set; }
                public string DetailRuleNameKey { get; set; }
                public string DiscountRuleDesc { get; set; }
                public string DiscountNameKey { get; set; }
                public string NotePredefiniedDesc { get; set; }
                public string NoteNameKey { get; set; }
                public string Comment { get; set; }
                public string FullName { get; set; }
                public bool? discountStatus { get; set; }
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
                    public int BailId { get; set; }
                    public int? DiscountId { get; set; }
                    public int LanguageId { get; set; }
                }
            }


            public class Key
            {
                public string Action { get; set; }
                public int CorpId { get; set; }
                public int RegionId { get; set; }
                public int CountryId { get; set; }
                public int DomesticRegId { get; set; }
                public int StateProvId { get; set; }
                public int CityId { get; set; }
                public int OfficeId { get; set; }
                public int CaseSeqNo { get; set; }
                public int HistSeqNo { get; set; }
                public int? BailId { get; set; }
                public int BlTypeId { get; set; }
                public int BlId { get; set; }
                public int ProductId { get; set; }
            }

            public class Coverage
            {
                public int? CorpId { get; set; }
                public long? UniqueBailId { get; set; }
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
                    public long UniqueBailId { get; set; }
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
                    public long UniqueBailId { get; set; }
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
                        public long UniqueBailId { get; set; }
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


            public class Guarantors
            {
                public int CorpId { get; set; }
                public long UniqueBailId { get; set; }
                public int SeqId { get; set; }
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
                public string NationalityCountryDesc { get; set; }
                public string CountryDesc { get; set; }
                public string RepresentativeIdentificationTypeDesc { get; set; }
                public string IdentificationTypeDesc { get; set; }
                public string TipoRiesgoNameKey { get; set; }
                public int? MunicipioId { get; set; }
                public string MunicipioDesc { get; set; }
                public Nullable<int> IdPais { get; set; }
                public Nullable<int> IdProvincia { get; set; }
                public Nullable<int> IdMunicipio { get; set; }
                public Nullable<int> IdSector { get; set; }
                public int IdentificationTypeIdSysflex { get; set; }
                public int RepresentativeIdentificationTypeIdSysflex { get; set; }

                public class Key
                {
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
                    public string TipoRiesgoNameKey { get; set; }
                    public string SourceId { get; set; }
                    public int? UserId { get; set; }
                }
            }
        }

    }
}