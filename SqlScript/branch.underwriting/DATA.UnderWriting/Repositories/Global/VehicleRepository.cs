﻿using DATA.UnderWriting.Data;
using DATA.UnderWriting.Repositories.Base;
using Entity.UnderWriting.Entities;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace DATA.UnderWriting.Repositories.Global
{
    public class VehicleRepository : GlobalRepository
    {
        public VehicleRepository(GlobalEntityDataModel globalModel, GlobalEntities globalModelExtended) : base(globalModel, globalModelExtended) { }

        public virtual IEnumerable<Vehicle.Insured.Detail> GetVehicleInsuredDetail(Vehicle.Policy policy)
        {
            IEnumerable<Vehicle.Insured.Detail> result;
            IEnumerable<SP_GET_VEHICLE_INSURED_DETAILS_Result> temp;

            temp = globalModel.SP_GET_VEHICLE_INSURED_DETAILS(
                    policy.ContactId,
                    policy.CorpId,
                    policy.RegionId,
                    policy.CountryId,
                    policy.DomesticRegId,
                    policy.StateProvId,
                    policy.CityId,
                    policy.OfficeId,
                    policy.CaseSeqNo,
                    policy.HistSeqNo,
                    policy.PolicyStatusId
                );

            result = temp.Select(dk => new Vehicle.Insured.Detail
            {
                CorpId = dk.Corp_Id,
                RegionId = dk.Region_Id,
                CountryId = dk.Country_Id,
                DomesticRegId = dk.Domesticreg_Id,
                StateProvId = dk.State_Prov_Id,
                CityId = dk.City_Id,
                OfficeId = dk.Office_Id,
                CaseSeqNo = dk.Case_Seq_No,
                HistSeqNo = dk.Hist_Seq_No,
                Vehicle = dk.Vehicle,
                VIN = dk.VIN,
                Make = dk.Make,
                Model = dk.Model,
                Type = dk.Type,
                TypeId = dk.TypeId.GetValueOrDefault(),
                Registry = dk.Registry,
                Cylinders = dk.Cylinders,
                Tons = dk.Tons,
                Use = dk.Use,
                UseId = dk.UseId,
                Year = dk.Year,
                PhotosInsp = dk.Photos_Insp,
                VehicleUniqueId = dk.Vehicle_Unique_Id,
                PendingDeleted = dk.PendingDeleted,
                WheelDriveTypeId = dk.Wheel_Drive_Type_Id,
                WheelDriveTypeDesc = dk.Wheel_Drive_Type_Desc,
                VehicleClassId = dk.Vehicle_Class_Id,
                VehicleClassDesc = dk.Vehicle_Class_Desc,
                TransmissionTypeId = dk.Transmission_Type_Id,
                TransmissionTypeDesc = dk.Transmission_Type_Desc,

                Odometer = dk.Odometer,
                VersionDesc = dk.Version_Desc,
                VersionId = dk.Version_Id,
                Seats = dk.Seats
            })
            .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Vehicle.Insured> GetVehicleInsured(Vehicle.Policy policy)
        {
            IEnumerable<Vehicle.Insured> result;
            IEnumerable<SP_GET_PL_POLICY_VEHICLE_INSURED_Result> temp;

            temp = globalModel.SP_GET_PL_POLICY_VEHICLE_INSURED(
                    policy.CorpId,
                    policy.RegionId,
                    policy.CountryId,
                    policy.DomesticRegId,
                    policy.StateProvId,
                    policy.CityId,
                    policy.OfficeId,
                    policy.CaseSeqNo,
                    policy.HistSeqNo
                );

            result = temp.Select(dk => new Vehicle.Insured
            {
                CorpId = dk.Corp_Id,
                RegionId = dk.Region_Id,
                CountryId = dk.Country_Id,
                DomesticRegId = dk.Domesticreg_Id,
                StateProvId = dk.State_Prov_Id,
                CityId = dk.City_Id,
                OfficeId = dk.Office_Id,
                CaseSeqNo = dk.Case_Seq_No,
                HistSeqNo = dk.Hist_Seq_No,
                InsuredVehicleId = dk.Insured_Vehicle_Id,
                VehicleUniqueId = dk.Vehicle_Unique_Id,
                RegistrationId = dk.Registration_Id,
                InsuredDate = dk.Insured_Date,
                ExpirationDate = dk.Expiration_Date,
                PremiumAmount = dk.Premium_Amount,
                MakeId = dk.Make_Id,
                ModelId = dk.Model_Id,
                ColorId = dk.Color_Id,
                VehicleTypeId = dk.Vehicle_Type_Id,
                Year = dk.Year,
                PassengerNumber = dk.Passenger_Number,
                UsageId = dk.Usage_Id,
                VehicleValue = dk.Vehicle_Value,
                StoredId = dk.Stored_Id,
                RentTypeId = dk.Rent_Type_Id,
                RentLengthId = dk.Rent_Length_Id,
                Garage = dk.Garage,
                MakeDesc = dk.Make_Desc,
                ModelDesc = dk.Model_Desc,
                ColorDesc = dk.Color_Desc,
                VehicleTypeDesc = dk.Vehicle_Type_Desc,
                Chassis = dk.Chassis,
                Registry = dk.Registry,
                CylindersTons = dk.Cylinders_Tons,
                UsageDesc = dk.Usage_Desc,
                StoredDesc = dk.Stored_Desc,
                AmbulanceTypeId = dk.Ambulance_Type_Id,
                GeographicLimitation = dk.Geographic_Limitation,
                ReinsuranceAmount = dk.Reinsurance_Amount,
                Latitud = dk.Latitud,
                Longitud = dk.Longitud
            })
            .ToArray();

            return result;
        }

        public virtual IEnumerable<Vehicle.Review> GetVehicleReview(Vehicle vehicle)
        {
            IEnumerable<Vehicle.Review> result;
            IEnumerable<SP_GET_PL_PCY_VEHICLE_REVIEWS_Result> temp;

            temp = globalModelExtended.SP_GET_PL_PCY_VEHICLE_REVIEWS(
                    vehicle.CorpId,
                    vehicle.RegionId,
                    vehicle.CountryId,
                    vehicle.DomesticRegId,
                    vehicle.StateProvId,
                    vehicle.CityId,
                    vehicle.OfficeId,
                    vehicle.CaseSeqNo,
                    vehicle.HistSeqNo,
                    vehicle.InsuredVehicleId,
                    vehicle.MakeId,
                    vehicle.ModelId
            );

            result = temp.Select(dk => new Vehicle.Review
            {
                MakeId = dk.Make_Id,
                ModelId = dk.Model_Id,
                VersionId = dk.Version_Id.GetValueOrDefault(),
                VersionDesc = dk.Version_Desc,
                ModelYear = dk.Model_Year,
                Seats = dk.Seats,
                Cylinder = dk.Cylinder,
                RegistryPlate = dk.Registry_Plate,
                ColorId = dk.Color_Id,
                ColorDesc = dk.Color_Desc,
                TransmissionTypeId = dk.Transmission_Type_Id.GetValueOrDefault(),
                TransmissionTypeDesc = dk.Transmission_Type_Desc,
                WheelDriveTypeId = dk.Wheel_Drive_Id.GetValueOrDefault(),
                WheelDriveTypeDesc = dk.Wheel_Drive_Type_Desc,
                VehicleTypeId = dk.Vehicle_Type_Id,
                VehicleTypeDesc = dk.Vehicle_Type_Desc,
                VehicleClassId = dk.Vehicle_Class_Id,
                VehicleClassDesc = dk.Vehicle_Class_Desc,
                UsageId = dk.Usage_Id,
                UsageDesc = dk.Usage_Desc,
                Odometer = dk.Odometer,
                Hubodometer = dk.Hubodometer,
                RegistrationDocument = dk.Registration_Document,
                FuelInt = dk.Fuel_Int,
                InspectedBy = dk.Inspected_By,
                Capacity = dk.Capacity,
                ReviewDate = dk.Review_Date,
                ReviewNotes = dk.Review_Notes,
                ReviewAmount = dk.Review_Amount,
                Mark = dk.Mark,
                ReviewFinishDate = dk.Review_Finish_Date,
                DocTypeId = dk.Doc_Type_Id,
                DocCategoryId = dk.Doc_Category_Id,
                DocumentId = dk.Document_Id,
                ReviewStatusId = dk.Review_Status_Id,
                ReviewStatus = dk.Review_Status,
                InspectionNumber = dk.Inspection_Number,
                ApplicantInspection = dk.Applicant_Inspection,
                IdentificationDocument = dk.IdentificationDocument,
                Inspection = dk.Inspection,
                ReviewId = dk.Review_Id,
                InspectorSuggestsAcceptRisk = dk.InspectorSuggestsAcceptRisk,
                InspectedByName = dk.Inspected_By_Name,
                Phone = dk.Phone,
                Email = dk.Email,
                MileageKilometer = dk.Mileage_Kilometer,
                InspectionAddress = dk.Inspection_Address,
                UsuarioInspeccion = dk.UsuarioInspeccion
            })
                        .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Vehicle.Review.Pic> GetVehicleReviewPic(Vehicle.Review review)
        {
            IEnumerable<Vehicle.Review.Pic> result;
            IEnumerable<SP_GET_PL_PCY_VEHICLE_REVIEW_PICS_Result> temp;

            temp = globalModel.SP_GET_PL_PCY_VEHICLE_REVIEW_PICS(
                        review.CorpId,
                        review.RegionId,
                        review.CountryId,
                        review.DomesticRegId,
                        review.StateProvId,
                        review.CityId,
                        review.OfficeId,
                        review.CaseSeqNo,
                        review.HistSeqNo,
                        review.InsuredVehicleId,
                        review.ReviewId
                );

            result = temp.Select(dk => new Vehicle.Review.Pic
            {
                ReviewId = dk.Review_Id,
                InsuredVehicleId = dk.Insured_Vehicle_Id,
                PictureId = dk.Picture_Id,
                PictureStatus = dk.Picture_Status,
                DocumentId = dk.Document_Id,
                DocCategoryId = dk.Doc_Category_Id,
                DocTypeId = dk.Doc_Type_Id,
                DocumentBinary = dk.Document_Binary,
                DocumentDesc = dk.Document_Desc,
                DocumentName = dk.Document_Name
            })
                        .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Vehicle.Review.Item.Option> GetVehicleReviewItemOption(Vehicle.Review review)
        {
            IEnumerable<Vehicle.Review.Item.Option> result;
            IEnumerable<SP_GET_ST_REVIEW_ITEM_OPTIONS_Result> temp;

            temp = globalModel.SP_GET_ST_REVIEW_ITEM_OPTIONS(
                        review.CorpId,
                        review.LanguageId,
                        review.ReviewGroupId
                   );

            result = temp.Select(dk => new Vehicle.Review.Item.Option
            {
                GroupId = dk.GroupId,
                GroupDesc = dk.GroupDesc,
                ClassId = dk.ClassId,
                ClassDesc = dk.ClassDesc,
                ItemId = dk.ItemId,
                ItemDesc = dk.ItemDesc,
                OptionId = dk.OptionId,
                OptionDesc = dk.OptionDesc
            })
                        .ToArray();

            return
                result;
        }

        public virtual Vehicle.Document.Category GetDocumentCategory(string nameKey)
        {
            Vehicle.Document.Category result;
            IEnumerable<SP_GET_DOCUMENT_CATEGORY_Result> temp;

            temp = globalModel.SP_GET_DOCUMENT_CATEGORY(
                        nameKey
                   );

            result = temp.Select(dk => new Vehicle.Document.Category
            {
                DocTypeId = dk.Doc_Type_Id,
                DocCategoryId = dk.Doc_Category_Id,
                NameKey = dk.NameKey,
                DocCategoryDesc = dk.Doc_Category_Desc,
                DocCategoryParentId = dk.Doc_Category_Parent_Id,
                CreateDate = dk.Create_Date,
                ModiDate = dk.Modi_Date,
                CreateUsrId = dk.Create_UsrId,
                ModiUsrId = dk.Modi_UsrId,
                HostName = dk.Hostname
            })
                        .FirstOrDefault();
            return
                result;
        }

        public virtual Vehicle.Review SetVehicleReview(Vehicle.Review review)
        {
            Vehicle.Review result;
            IEnumerable<SP_SET_PL_PCY_VEHICLE_REVIEWS_Result> temp;

            temp = globalModel.SP_SET_PL_PCY_VEHICLE_REVIEWS(
                            review.CorpId,
                            review.RegionId,
                            review.CountryId,
                            review.DomesticRegId,
                            review.StateProvId,
                            review.CityId,
                            review.OfficeId,
                            review.CaseSeqNo,
                            review.HistSeqNo,
                            review.InsuredVehicleId,
                            review.ReviewId,
                            review.MakeId,
                            review.ModelId,
                            review.VersionId,
                            review.TransmissionTypeId,
                            review.WheelDriveTypeId,
                            review.VehicleClassId,
                            review.ModelYear,
                            review.Seats,
                            review.Cylinder,
                            review.RegistryPlate,
                            review.ColorId,
                            review.VehicleTypeId,
                            review.UsageId,
                            review.MileageKilometer,
                            review.Odometer,
                            review.Hubodometer,
                            review.RegistrationDocument,
                            review.FuelInt,
                            review.InspectedBy,
                            review.Capacity,
                            review.ReviewDate,
                            review.ReviewNotes,
                            review.ReviewAmount,
                            review.Mark,
                            review.ReviewFinishDate,
                            review.DocTypeId,
                            review.DocCategoryId,
                            review.DocumentId,
                            review.ReviewStatusId,
                            review.ReviewStatus,
                            review.InspectionNumber,
                            review.ApplicantInspection,
                            review.IdentificationDocument,
                            review.InspectorSuggestsAcceptRisk,
                            review.Email,
                            review.Phone,
                            review.CreateDate,
                            review.CreateUserId,
                            review.UsuarioInspeccion
                   );

            result = temp.Select(dk => new Vehicle.Review
            {
                CorpId = dk.Corp_Id,
                RegionId = dk.Region_Id,
                CountryId = dk.Country_Id,
                DomesticRegId = dk.Domesticreg_Id,
                StateProvId = dk.State_Prov_Id,
                CityId = dk.City_Id,
                OfficeId = dk.Office_Id,
                CaseSeqNo = dk.Case_Seq_No,
                HistSeqNo = dk.Hist_Seq_No,
                InsuredVehicleId = dk.Insured_Vehicle_Id,
                ReviewId = dk.Review_Id
            })
                        .FirstOrDefault();
            return
                result;
        }

        public virtual int? DeleteVehicleReviewDetail(Vehicle.Review.Detail detail)
        {
            int? result;
            IEnumerable<SP_DELETE_PL_PCY_VEHICLE_REVIEW_DETAILS_Result> temp;

            temp = globalModel.SP_DELETE_PL_PCY_VEHICLE_REVIEW_DETAILS(
                                detail.CorpId,
                                detail.RegionId,
                                detail.CountryId,
                                detail.DomesticRegId,
                                detail.StateProvId,
                                detail.CityId,
                                detail.OfficeId,
                                detail.CaseSeqNo,
                                detail.HistSeqNo,
                                detail.InsuredVehicleId,
                                detail.ReviewId
                   );

            result = temp.Select(dk =>
                            dk.Result
                             )
                         .FirstOrDefault();
            return
                result;
        }

        public virtual Vehicle.Review.Detail SetVehicleReviewDetail(Vehicle.Review.Detail detail)
        {
            Vehicle.Review.Detail result;
            IEnumerable<SP_SET_PL_PCY_VEHICLE_REVIEW_DETAILS_Result> temp;

            temp = globalModel.SP_SET_PL_PCY_VEHICLE_REVIEW_DETAILS(
                            detail.CorpId,
                            detail.RegionId,
                            detail.CountryId,
                            detail.DomesticRegId,
                            detail.StateProvId,
                            detail.CityId,
                            detail.OfficeId,
                            detail.CaseSeqNo,
                            detail.HistSeqNo,
                            detail.InsuredVehicleId,
                            detail.ReviewId,
                            detail.ReviewDetailId,
                            detail.ReviewGroupId,
                            detail.ReviewClassId,
                            detail.ReviewItemId,
                            detail.ReviewOptionId,
                            detail.Checked,
                            detail.OptionNotes,
                            detail.ReviewStatus,
                            detail.CreateUserId
                   );

            result = temp.Select(dk => new Vehicle.Review.Detail
            {
                Action = dk.Action,
                CorpId = dk.Corp_Id,
                RegionId = dk.Region_Id,
                CountryId = dk.Country_Id,
                DomesticRegId = dk.Domesticreg_Id,
                StateProvId = dk.State_Prov_Id,
                CityId = dk.City_Id,
                OfficeId = dk.Office_Id,
                CaseSeqNo = dk.Case_Seq_No,
                HistSeqNo = dk.Hist_Seq_No,
                InsuredVehicleId = dk.Insured_Vehicle_Id,
                ReviewId = dk.Review_Id,
                ReviewDetailId = dk.Review_Detail_Id,
                ReviewGroupId = dk.Review_Group_Id,
                ReviewClassId = dk.Review_Class_Id,
                ReviewItemId = dk.Review_Item_Id,
                ReviewOptionId = dk.Review_Option_Id
            })
                        .FirstOrDefault();
            return
                result;
        }

        public virtual IEnumerable<Vehicle.Review.Detail> GetVehicleReviewDetail(Vehicle.Review review)
        {
            IEnumerable<Vehicle.Review.Detail> result;
            IEnumerable<SP_GET_PL_PCY_VEHICLE_REVIEW_DETAILS_Result> temp;

            temp = globalModel.SP_GET_PL_PCY_VEHICLE_REVIEW_DETAILS(
                            review.CorpId,
                            review.RegionId,
                            review.CountryId,
                            review.DomesticRegId,
                            review.StateProvId,
                            review.CityId,
                            review.OfficeId,
                            review.CaseSeqNo,
                            review.HistSeqNo,
                            review.InsuredVehicleId,
                            review.ReviewId,
                            review.ReviewGroupId,
                            review.ReviewGroupEndorsementClarifying,
                            review.ReviewOptionEndorsementClarifying,
                            review.LanguageId
                   );

            result = temp.Select(dk => new Vehicle.Review.Detail
            {
                CorpId = dk.Corp_Id,
                RegionId = dk.Region_Id,
                CountryId = dk.Country_Id,
                DomesticRegId = dk.Domesticreg_Id,
                StateProvId = dk.State_Prov_Id,
                CityId = dk.City_Id,
                OfficeId = dk.Office_Id,
                CaseSeqNo = dk.Case_Seq_No,
                HistSeqNo = dk.Hist_Seq_No,
                InsuredVehicleId = dk.Insured_Vehicle_Id,
                ReviewDetailId = dk.Review_Detail_Id,
                ReviewGroupId = dk.Review_Group_Id,
                ReviewGroupDesc = dk.Review_Group_Desc,
                ReviewClassId = dk.Review_Class_Id,
                ReviewClassDesc = dk.Review_Class_Desc,
                ReviewItemId = dk.Review_Item_Id,
                ReviewItemDesc = dk.Review_Item_Desc,
                ReviewOptionId = dk.Review_Option_Id,
                ReviewOptionDesc = dk.Review_Option_Desc,
                Checked = dk.Checked,
                ReviewGroupEndorsementClarifying = dk.Review_Group_Endorsement_Clarifying,
                ReviewOptionEndorsementClarifying = dk.Review_Option_Endorsement_Clarifying,
                VehicleVersionDesc = dk.Vehicle_Version_Desc,
                VehicleVersionId = dk.Vehicle_Version_Id
            });

            return
                result;
        }

        public virtual Vehicle.Document SetDocument(Vehicle.Document document)
        {
            Vehicle.Document result;
            IEnumerable<SP_SET_DOCUMENT2_Result> temp;
            ObjectParameter document_Id;

            document_Id = new ObjectParameter("Document_Id", document.DocumentId);

            temp = globalModel.SP_SET_DOCUMENT2(
                            document.DocTypeId,
                            document.DocCategoryId,
                            document_Id,
                            document.DocumentBinary,
                            document.DocumentName,
                            document.DocumentDesc,
                            document.FileCreationDate,
                            document.FileExpireDate,
                            document.UserId
                   );

            result = temp.Select(dk => new Vehicle.Document
            {
                Action = dk.Action,
                DocTypeId = dk.Doc_Type_Id,
                DocCategoryId = dk.Doc_Category_Id,
                DocumentId = dk.Document_Id
            })
                        .FirstOrDefault();
            return
                result;
        }

        public virtual Vehicle.Document SetPolicyDocument(Vehicle.Document document)
        {
            Vehicle.Document result;
            IEnumerable<SP_SET_PL_PCY_DOCUMENTS_Result> temp;

            temp = globalModel.SP_SET_PL_PCY_DOCUMENTS(
                            document.CorpId,
                            document.RegionId,
                            document.CountryId,
                            document.DomesticRegId,
                            document.StateProvId,
                            document.CityId,
                            document.OfficeId,
                            document.CaseSeqNo,
                            document.HistSeqNo,
                            document.DocTypeId,
                            document.DocCategoryId,
                            document.DocumentId,
                            document.DocumentStatus,
                            document.UserId
                   );

            result = temp.Select(dk => new Vehicle.Document
            {
                Action = dk.Action,
                CorpId = dk.Corp_Id,
                RegionId = dk.Region_Id,
                CountryId = dk.Country_Id,
                DomesticRegId = dk.Domesticreg_Id,
                StateProvId = dk.State_Prov_Id,
                CityId = dk.City_Id,
                OfficeId = dk.Office_Id,
                CaseSeqNo = dk.Case_Seq_No,
                HistSeqNo = dk.Hist_Seq_No,
                DocTypeId = dk.Doc_Type_Id,
                DocCategoryId = dk.Doc_Category_Id,
                DocumentId = dk.Document_Id
            })
                        .FirstOrDefault();
            return
                result;
        }

        public virtual Vehicle.Review.Pic SetVehicleReviewPic(Vehicle.Review.Pic review)
        {
            Vehicle.Review.Pic result;
            IEnumerable<SP_SET_PL_PCY_VEHICLE_REVIEW_PICS_Result> temp;

            temp = globalModel.SP_SET_PL_PCY_VEHICLE_REVIEW_PICS(
                            review.CorpId,
                            review.RegionId,
                            review.CountryId,
                            review.DomesticRegId,
                            review.StateProvId,
                            review.CityId,
                            review.OfficeId,
                            review.CaseSeqNo,
                            review.HistSeqNo,
                            review.InsuredVehicleId,
                            review.ReviewId,
                            review.PictureId,
                            review.DocTypeId,
                            review.DocCategoryId,
                            review.DocumentId,
                            review.PictureStatus,
                            review.CreateDate,
                            review.CreateUserId
                   );

            result = temp.Select(dk => new Vehicle.Review.Pic
            {
                CorpId = dk.Corp_Id,
                RegionId = dk.Region_Id,
                CountryId = dk.Country_Id,
                DomesticRegId = dk.Domesticreg_Id,
                StateProvId = dk.State_Prov_Id,
                CityId = dk.City_Id,
                OfficeId = dk.Office_Id,
                CaseSeqNo = dk.Case_Seq_No,
                HistSeqNo = dk.Hist_Seq_No,
                InsuredVehicleId = dk.Insured_Vehicle_Id,
                ReviewId = dk.Review_Id,
                PictureId = dk.Picture_Id,
                DocTypeId = dk.Doc_Type_Id,
                DocCategoryId = dk.Doc_Category_Id,
                DocumentId = dk.Document_Id
            })
                        .FirstOrDefault();
            return
                result;
        }

        public virtual Vehicle.Agent.AssignedOffice GetAgentAssignedOffice(Vehicle.Agent agent)
        {
            Vehicle.Agent.AssignedOffice result;
            IEnumerable<SP_GET_EN_AGENT_ASSIGNED_OFFICE_Result> temp;

            temp = globalModel.SP_GET_EN_AGENT_ASSIGNED_OFFICE(
                            agent.CorpId,
                            agent.AgentId
                   );

            result = temp.Select(dk => new Vehicle.Agent.AssignedOffice
            {
                AgentId = dk.Agent_Id,
                OfficeDesc = dk.Office_Desc,
                OfficeId = dk.Office_Id
            })
                        .FirstOrDefault();
            return
                result;
        }

        public virtual Vehicle.Review.VIG SetVehicleReviewVIG(Vehicle.Review.VIG vig)
        {
            Vehicle.Review.VIG result;
            IEnumerable<SP_SET_ST_REVIEW_GIV_Result> temp;

            temp = globalModel.SP_SET_ST_REVIEW_GIV(
                            vig.Corp_Id,
                            vig.Vehicle_Unique_Id,
                            vig.VIG_Type_Id,
                            vig.Good,
                            vig.UserId
                   );

            result = temp.Select(dk => new Vehicle.Review.VIG
            {
                Action = dk.Action,
                Corp_Id = dk.Corp_Id,
                Vehicle_Unique_Id = dk.Vehicle_Unique_Id,
                VIG_Type_Id = dk.Giv_Type_Id
            })
                        .FirstOrDefault();
            return
                result;
        }

        public virtual IEnumerable<Vehicle.Review.VIG> GetVehicleReviewVIG(Vehicle.Review.VIG vig)
        {
            IEnumerable<Vehicle.Review.VIG> result;
            IEnumerable<SP_GET_ST_REVIEW_GIV_Result> temp;

            temp = globalModel.SP_GET_ST_REVIEW_GIV(
                            vig.Corp_Id,
                            vig.Vehicle_Unique_Id
                   );

            result = temp.Select(dk => new Vehicle.Review.VIG
            {
                Corp_Id = dk.Corp_Id,
                Vehicle_Unique_Id = dk.Vehicle_Unique_Id,
                VIG_Type_Id = dk.Giv_Type_Id,
                Good = dk.Good,
                VIG_Type_Desc = dk.Giv_Type_Desc,
                VIG_Type_Name_Key = dk.Giv_Type_Name_Key,
                VIG_Type_Status = dk.Giv_Type_Status
            });

            return
                result;
        }

        public virtual IEnumerable<Vehicle.TransmissionType> GetVehicleTransmissionTypes(Vehicle.TransmissionType vehicle_transmission_type)
        {
            IEnumerable<Vehicle.TransmissionType> result;
            IEnumerable<SP_GET_ST_TRANSMISSION_TYPES_Result> temp;

            temp = globalModel.SP_GET_ST_TRANSMISSION_TYPES(
                            vehicle_transmission_type.TransmissionTypeId
                   );

            result = temp.Select(dk => new Vehicle.TransmissionType
            {
                TransmissionTypeDesc = dk.Transmission_Type_Desc,
                TransmissionTypeId = dk.Transmission_Type_Id,
                TransmissionTypeStatus = dk.Transmission_Type_Status
            });

            return
                result;
        }

        public virtual IEnumerable<Vehicle.Classes> GetVehicleClasses(Vehicle.Classes vehicle_class)
        {
            IEnumerable<Vehicle.Classes> result;
            IEnumerable<SP_GET_ST_VEHICLE_CLASSES_Result> temp;

            temp = globalModel.SP_GET_ST_VEHICLE_CLASSES(
                            vehicle_class.ClassId
                   );

            result = temp.Select(dk => new Vehicle.Classes
            {
                ClassDesc = dk.Vehicle_Class_Desc,
                ClassId = dk.Vehicle_Class_Id,
                ClassStatus = dk.Vehicle_Class_Status
            });

            return
                result;
        }

        public virtual IEnumerable<Vehicle.WheelDriveType> GetVehicleWheelDriveTypes(Vehicle.WheelDriveType vehicle_wheel_drive_type)
        {
            IEnumerable<Vehicle.WheelDriveType> result;
            IEnumerable<SP_GET_ST_WHEEL_DRIVE_TYPES_Result> temp;

            temp = globalModel.SP_GET_ST_WHEEL_DRIVE_TYPES(
                            vehicle_wheel_drive_type.WheelDriveTypeId
                   );

            result = temp.Select(dk => new Vehicle.WheelDriveType
            {
                WheelDriveTypeDesc = dk.Wheel_Drive_Type_Desc,
                WheelDriveTypeId = dk.Wheel_Drive_Type_Id,
                WheelDriveTypeStatus = dk.Wheel_Drive_Type_Status
            });

            return
                result;
        }

        public virtual IEnumerable<Vehicle.Version> GetVehicleVersions(Vehicle.Version vehicle_version)
        {
            IEnumerable<Vehicle.Version> result;
            IEnumerable<SP_GET_ST_VEHICLE_VERSION_CAT_Result> temp;

            temp = globalModel.SP_GET_ST_VEHICLE_VERSION_CAT(
                            vehicle_version.VersionId
                   );

            result = temp.Select(dk => new Vehicle.Version
            {
                VersionDesc = dk.Vehicle_Version_Desc,
                VersionId = dk.Vehicle_Version_Id
            });

            return
                result;
        }

        public virtual IEnumerable<Vehicle.Document> GetPolicyDocument(Vehicle.Policy policy)
        {
            IEnumerable<Vehicle.Document> result;
            IEnumerable<SP_GET_PL_PCY_DOCUMENTS_Result> temp;

            temp = globalModel.SP_GET_PL_PCY_DOCUMENTS(
                            policy.CorpId,
                            policy.RegionId,
                            policy.CountryId,
                            policy.DomesticRegId,
                            policy.StateProvId,
                            policy.CityId,
                            policy.OfficeId,
                            policy.CaseSeqNo,
                            policy.HistSeqNo
                   );

            result = temp.Select(dk => new Vehicle.Document
            {
                CorpId = dk.Corp_Id,
                RegionId = dk.Region_Id,
                CountryId = dk.Country_Id,
                DomesticRegId = dk.Domesticreg_Id,
                StateProvId = dk.State_Prov_Id,
                CityId = dk.City_Id,
                OfficeId = dk.Office_Id,
                CaseSeqNo = dk.Case_Seq_No,
                HistSeqNo = dk.Hist_Seq_No,
                DocTypeId = dk.Doc_Type_Id,
                DocCategoryId = dk.Doc_Category_Id,
                DocumentId = dk.Document_Id,
                DocumentDesc = dk.Document_Desc,
                DocumentStatus = dk.Document_Status_Id,
                DocumentName = dk.Document_Name,
                DocumentBinary = dk.Document_Binary
            });

            return
                result;
        }

        public virtual int? DeletePolicyDocument(Vehicle.Review review)
        {
            int? result;
            ObjectResult<int?> temp;

            temp = globalModel.SP_UPDATE_POLICY_DOCUMENT_DELETE_REVIEW_PICS(
                            review.CorpId,
                            review.RegionId,
                            review.CountryId,
                            review.DomesticRegId,
                            review.StateProvId,
                            review.CityId,
                            review.OfficeId,
                            review.CaseSeqNo,
                            review.HistSeqNo,
                            review.InsuredVehicleId
                   );

            result = temp.Select(dk =>
                            dk.Value
                             )
                         .FirstOrDefault();
            return
                result;
        }

        #region Request Changes
        public virtual IEnumerable<RequestChanges> GetVehicleRequestChange(string PolicyNumber, int? ConditionId, bool IsLastRecord = true)
        {
            IEnumerable<RequestChanges> result;
            IEnumerable<SP_GET_REQUEST_CHANGES_Result> temp;

            temp = globalModelExtended.SP_GET_REQUEST_CHANGES(
                            PolicyNumber,
                            IsLastRecord,
                            ConditionId
                   );

            result = temp.Select(dk => new RequestChanges
            {
                Change_Id = dk.Change_Id,
                Policy_Number = dk.Policy_Number,
                Condition_Id = dk.Condition_Id,
                Vehicle_Secuence = dk.Vehicle_Secuence,
                Old_Value = dk.Old_Value,
                New_Value = dk.New_Value,
                Source = dk.Source,
                Create_Date = dk.Create_Date,
                Create_User = dk.Create_User
            });

            return
                result;
        }

        public virtual RequestChanges SetVehicleRequestChange(RequestChanges.Parameter parameter)
        {
            RequestChanges result;
            IEnumerable<SP_SET_REQUEST_CHANGES_Result> temp;

            temp = globalModelExtended.SP_SET_REQUEST_CHANGES(
                            parameter.change_Id,
                            parameter.policy_Number,
                            parameter.condition_Id,
                            parameter.vehicle_Secuence,
                            parameter.old_Value,
                            parameter.new_Value,
                            parameter.source,
                            parameter.create_User
                   );

            result = temp.Select(dk => new RequestChanges
            {
                Action = dk.Action,
                Change_Id = dk.Change_Id
            })
            .FirstOrDefault();
            return
                result;
        }

        #endregion


        public virtual Vehicle.FuelType GetFuelTypes(Vehicle.FuelType parameter)
        {
            Vehicle.FuelType result;
            IEnumerable<SP_GET_FUEL_TYPES_Result> temp;

            temp = globalModelExtended.SP_GET_FUEL_TYPES(
                            parameter.VehicleFuelTypeIdSysflex,
                            parameter.VehicleFuelTypeDesc);

            result = temp.Select(dk => new Vehicle.FuelType
            {
                VehicleFuelTypeIdSysflex = dk.VehicleFuelTypeIdSysflex,
                VehicleFuelTypeDesc = dk.VehicleFuelTypeDesc
            }).FirstOrDefault();

            return
                result;
        }
    }
}
