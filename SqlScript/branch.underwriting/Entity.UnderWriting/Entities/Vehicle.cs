﻿using System;
using System.Collections.Generic;

namespace Entity.UnderWriting.Entities
{
    public class Vehicle
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

        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public int? InsuredVehicleId { get; set; }

        public class Policy
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
            public int? ContactId { get; set; }
        }

        public class Insured
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

            public int InsuredVehicleId { get; set; }
            public long VehicleUniqueId { get; set; }
            public string RegistrationId { get; set; }
            public DateTime? InsuredDate { get; set; }
            public DateTime? ExpirationDate { get; set; }
            public decimal? PremiumAmount { get; set; }
            public int? MakeId { get; set; }
            public int? ModelId { get; set; }
            public int? ColorId { get; set; }
            public int? VehicleTypeId { get; set; }
            public int? Year { get; set; }
            public int? PassengerNumber { get; set; }
            public int? UsageId { get; set; }
            public int? VehicleValue { get; set; }
            public int? StoredId { get; set; }
            public int? RentTypeId { get; set; }
            public int? RentLengthId { get; set; }
            public bool? Garage { get; set; }
            public string MakeDesc { get; set; }
            public string ModelDesc { get; set; }
            public string ColorDesc { get; set; }
            public string VehicleTypeDesc { get; set; }
            public string Chassis { get; set; }
            public string Registry { get; set; }
            public string CylindersTons { get; set; }
            public string UsageDesc { get; set; }
            public string StoredDesc { get; set; }
            public string AmbulanceTypeId { get; set; }
            public string GeographicLimitation { get; set; }
            public decimal? ReinsuranceAmount { get; set; }
            public string Longitud { get; set; }
            public string Latitud { get; set; }

            public class Detail
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

                public int TypeId { get; set; }
                public int Cylinders { get; set; }
                public int PhotosInsp { get; set; }
                public int PendingDeleted { get; set; }
                public int Odometer { get; set; }
                public int Seats { get; set; }

                public int? UseId { get; set; }
                public int? Year { get; set; }
                public int? WheelDriveTypeId { get; set; }
                public int? VehicleClassId { get; set; }
                public int? TransmissionTypeId { get; set; }

                public long VehicleUniqueId { get; set; }

                public string Vehicle { get; set; }
                public string VIN { get; set; }
                public string Make { get; set; }
                public string Model { get; set; }
                public string Type { get; set; }
                public string Registry { get; set; }
                public string Tons { get; set; }
                public string Use { get; set; }
                public string WheelDriveTypeDesc { get; set; }
                public string VehicleClassDesc { get; set; }
                public string TransmissionTypeDesc { get; set; }
                public string VersionDesc { get; set; }
                public int VersionId { get; set; }
            }
        }

        public class Review
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

            public int MakeId { get; set; }
            public int ModelId { get; set; }
            public int Seats { get; set; }
            public int Cylinder { get; set; }
            public int DocTypeId { get; set; }
            public int DocCategoryId { get; set; }

            public int? ReviewId { get; set; }
            public int? InsuredVehicleId { get; set; }
            public int? ModelYear { get; set; }
            public int? ColorId { get; set; }
            public int? VehicleTypeId { get; set; }
            public int? UsageId { get; set; }
            public int? Odometer { get; set; }
            public int? Hubodometer { get; set; }
            public int? FuelInt { get; set; }
            public int? InspectedBy { get; set; }
            public int? Capacity { get; set; }
            public int? DocumentId { get; set; }
            public int? ReviewStatusId { get; set; }
            public int? ReviewGroupId { get; set; }
            public int? CreateUserId { get; set; }

            public int VersionId { get; set; }
            public string VersionDesc { get; set; }

            public int TransmissionTypeId { get; set; }
            public string TransmissionTypeDesc { get; set; }

            public int VehicleClassId { get; set; }
            public string VehicleClassDesc { get; set; }

            public int WheelDriveTypeId { get; set; }
            public string WheelDriveTypeDesc { get; set; }

            public string RegistryPlate { get; set; }
            public string ColorDesc { get; set; }
            public string VehicleTypeDesc { get; set; }
            public string UsageDesc { get; set; }
            public string ReviewNotes { get; set; }
            public string Mark { get; set; }
            public string InspectionNumber { get; set; }
            public string ApplicantInspection { get; set; }
            public string IdentificationDocument { get; set; }

            public bool? RegistrationDocument { get; set; }
            public bool? ReviewStatus { get; set; }
            public bool? Inspection { get; set; }
            public bool? InspectorSuggestsAcceptRisk { get; set; }

            public DateTime? ReviewDate { get; set; }
            public DateTime? ReviewFinishDate { get; set; }
            public DateTime? CreateDate { get; set; }

            public decimal? ReviewAmount { get; set; }

            public bool? ReviewGroupEndorsementClarifying { get; set; }
            public bool? ReviewOptionEndorsementClarifying { get; set; }

            public int LanguageId { get; set; }

            public string InspectedByName { get; set; }

            public string Phone { get; set; }
            public string Email { get; set; }

            public int MileageKilometer { get; set; }

            public string InspectionAddress { get; set; }
            public string UsuarioInspeccion { get; set; }
            public class VIG
            {
                public int? Corp_Id { get; set; }
                public long? Vehicle_Unique_Id { get; set; }
                public int? VIG_Type_Id { get; set; }
                public string VIG_Type_Desc { get; set; }
                public string VIG_Type_Name_Key { get; set; }
                public bool VIG_Type_Status { get; set; }
                public bool? Good { get; set; }
                public int? UserId { get; set; }
                public string Action { get; set; }
            }

            public class Pic
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

                public int? InsuredVehicleId { get; set; }
                public int? ReviewId { get; set; }
                public int? PictureId { get; set; }
                public int? DocTypeId { get; set; }
                public int? DocCategoryId { get; set; }
                public int? DocumentId { get; set; }
                public int? CreateUserId { get; set; }

                public bool? PictureStatus { get; set; }

                public DateTime? CreateDate { get; set; }

                public byte[] DocumentBinary { get; set; }

                public string DocumentDesc { get; set; }
                public string DocumentName { get; set; }

                public int? UserID { get; set; }
                public string Quotation { get; set; }
            }

            public class Detail
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

                public int? ReviewId { get; set; }
                public int? InsuredVehicleId { get; set; }
                public int? ReviewDetailId { get; set; }
                public int? CreateUserId { get; set; }

                public int ReviewGroupId { get; set; }
                public int ReviewClassId { get; set; }
                public int ReviewItemId { get; set; }
                public int ReviewOptionId { get; set; }

                public string Action { get; set; }
                public string ReviewGroupDesc { get; set; }
                public string ReviewClassDesc { get; set; }
                public string ReviewItemDesc { get; set; }
                public string ReviewOptionDesc { get; set; }
                public string OptionNotes { get; set; }

                public bool? ReviewStatus { get; set; }
                public bool? Checked { get; set; }

                public bool ReviewGroupEndorsementClarifying { get; set; }
                public bool ReviewOptionEndorsementClarifying { get; set; }

                public int? VehicleVersionId { get; set; }
                public string VehicleVersionDesc { get; set; }
            }

            public class Item
            {
                public class Option
                {
                    public int GroupId { get; set; }

                    public int? ClassId { get; set; }
                    public int? ItemId { get; set; }
                    public int? OptionId { get; set; }

                    public string GroupDesc { get; set; }
                    public string ClassDesc { get; set; }
                    public string ItemDesc { get; set; }
                    public string OptionDesc { get; set; }
                }
            }
        }

        public class Classes
        {
            public int? ClassId { get; set; }
            public string ClassDesc { get; set; }
            public bool ClassStatus { get; set; }
        }

        public class TransmissionType
        {
            public int? TransmissionTypeId { get; set; }
            public string TransmissionTypeDesc { get; set; }
            public bool? TransmissionTypeStatus { get; set; }
        }

        public class WheelDriveType
        {
            public int? WheelDriveTypeId { get; set; }
            public string WheelDriveTypeDesc { get; set; }
            public bool? WheelDriveTypeStatus { get; set; }
        }

        public class Version
        {
            public int VersionId { get; set; }
            public string VersionDesc { get; set; }
        }

        [Serializable]
        public class InspectionForm
        {
            public class Option
            {
                public int? GroupId { get; set; }
                public string GroupDesc { get; set; }
                public int? ClassId { get; set; }
                public string ClassDesc { get; set; }
                public int? ItemId { get; set; }
                public string ItemDesc { get; set; }
                public int? OptionId { get; set; }
                public string OptionDesc { get; set; }
            }
        }

        public class Document
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

            public string Action { get; set; }
            public int? DocTypeId { get; set; }
            public int? DocCategoryId { get; set; }
            public int? DocumentId { get; set; }
            public bool? DocumentStatus { get; set; }

            public byte[] DocumentBinary { get; set; }
            public string DocumentName { get; set; }
            public string DocumentDesc { get; set; }
            public DateTime? FileCreationDate { get; set; }
            public DateTime? FileExpireDate { get; set; }
            public int? UserId { get; set; }

            public class Category
            {
                public int DocTypeId { get; set; }
                public int DocCategoryId { get; set; }
                public string NameKey { get; set; }
                public string DocCategoryDesc { get; set; }
                public int? DocCategoryParentId { get; set; }
                public DateTime CreateDate { get; set; }
                public DateTime? ModiDate { get; set; }
                public int CreateUsrId { get; set; }
                public int? ModiUsrId { get; set; }
                public string HostName { get; set; }
            }
        }

        public class Agent
        {
            public int CorpId { get; set; }
            public int? AgentId { get; set; }

            public class AssignedOffice
            {
                public int AgentId { get; set; }
                public int OfficeId { get; set; }
                public string OfficeDesc { get; set; }
            }
        }

        public class FuelType
        {
            public int? VehicleFuelTypeIdSysflex { get; set; }
            public string VehicleFuelTypeDesc { get; set; }
        }
    }
}