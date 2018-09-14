﻿using DATA.UnderWriting.Repositories.Global;
using DATA.UnderWriting.UnitOfWork;
using Entity.UnderWriting.Entities;
using System.Collections.Generic;

namespace LOGIC.UnderWriting.Global
{
    public class VehicleManager
    {
        private VehicleRepository _vehicleRepository;

        public VehicleManager()
        {
            _vehicleRepository = SingletonUnitOfWork.Instance.VehicleRepository;
        }

        public virtual IEnumerable<Vehicle.Insured.Detail> GetVehicleInsuredDetail(Vehicle.Policy policy)
        {
            return
                _vehicleRepository.GetVehicleInsuredDetail(policy);
        }

        public virtual IEnumerable<Vehicle.Insured> GetVehicleInsured(Vehicle.Policy policy)
        {
            return
                _vehicleRepository.GetVehicleInsured(policy);
        }
        
        public virtual IEnumerable<Vehicle.Review> GetVehicleReview(Vehicle vehicle)
        {
            return
                _vehicleRepository.GetVehicleReview(vehicle);
        }

        public virtual IEnumerable<Vehicle.Review.Pic> GetVehicleReviewPic(Vehicle.Review review)
        {
            return
                   _vehicleRepository.GetVehicleReviewPic(review);
        }

        public virtual IEnumerable<Vehicle.Review.Item.Option> GetVehicleReviewItemOption(Vehicle.Review review)
        {
            return
                   _vehicleRepository.GetVehicleReviewItemOption(review);
        }

        public virtual Vehicle.Document.Category GetDocumentCategory(string nameKey)
        {
            return
                   _vehicleRepository.GetDocumentCategory(nameKey);
        }

        public virtual Vehicle.Review SetVehicleReview(Vehicle.Review review)
        {
            return
                   _vehicleRepository.SetVehicleReview(review);
        }

        public virtual int? DeleteVehicleReviewDetail(Vehicle.Review.Detail detail)
        {
            return
                   _vehicleRepository.DeleteVehicleReviewDetail(detail);
        }

        public virtual Vehicle.Review.Detail SetVehicleReviewDetail(Vehicle.Review.Detail detail)
        {
            return
                   _vehicleRepository.SetVehicleReviewDetail(detail);
        }

        public virtual IEnumerable<Vehicle.Review.Detail> GetVehicleReviewDetail(Vehicle.Review review)
        {
            return
                   _vehicleRepository.GetVehicleReviewDetail(review);
        }

        public virtual Vehicle.Document SetDocument(Vehicle.Document document)
        {
            return
                   _vehicleRepository.SetDocument(document);
        }

        public virtual Vehicle.Document SetPolicyDocument(Vehicle.Document document)
        {
            return
                   _vehicleRepository.SetPolicyDocument(document);
        }

        public virtual Vehicle.Review.Pic SetVehicleReviewPic(Vehicle.Review.Pic review)
        {
            return
                   _vehicleRepository.SetVehicleReviewPic(review);
        }

        public virtual Vehicle.Agent.AssignedOffice GetAgentAssignedOffice(Vehicle.Agent agent)
        {
            return
                   _vehicleRepository.GetAgentAssignedOffice(agent);
        }

        public virtual Vehicle.Review.VIG SetVehicleReviewVIG(Vehicle.Review.VIG vig) 
        {
            return
                   _vehicleRepository.SetVehicleReviewVIG(vig);
        }

        public virtual IEnumerable<Vehicle.Review.VIG> GetVehicleReviewVIG(Vehicle.Review.VIG vig)
        {
            return
                   _vehicleRepository.GetVehicleReviewVIG(vig);
        }

        public virtual IEnumerable<Vehicle.TransmissionType> GetVehicleTransmissionTypes(Vehicle.TransmissionType vehicle_transmission_type)
        {
            return
                _vehicleRepository.GetVehicleTransmissionTypes(vehicle_transmission_type);
        }

        public virtual IEnumerable<Vehicle.Classes> GetVehicleClasses(Vehicle.Classes vehicle_class)
        {
            return
                _vehicleRepository.GetVehicleClasses(vehicle_class);
        }

        public virtual IEnumerable<Vehicle.WheelDriveType> GetVehicleWheelDriveTypes(Vehicle.WheelDriveType vehicle_wheel_drive_type)
        {
            return
                _vehicleRepository.GetVehicleWheelDriveTypes(vehicle_wheel_drive_type);
        }

        public virtual IEnumerable<Vehicle.Version> GetVehicleVersions(Vehicle.Version vehicle_version) 
        {
            return
                _vehicleRepository.GetVehicleVersions(vehicle_version);
        }

        public virtual IEnumerable<Vehicle.Document> GetPolicyDocument(Vehicle.Policy policy)
        {
            return
                _vehicleRepository.GetPolicyDocument(policy);
        }

        public virtual int? DeletePolicyDocument(Vehicle.Review review)
        {
            return
                _vehicleRepository.DeletePolicyDocument(review);
        }

        public virtual IEnumerable<RequestChanges> GetVehicleRequestChange(string PolicyNumber,int? ConditionId, bool IsLastRecord = true)
        {
            return
                _vehicleRepository.GetVehicleRequestChange(PolicyNumber, ConditionId, IsLastRecord);
        }

        public virtual RequestChanges SetVehicleRequestChange(RequestChanges.Parameter parameter)
        {
            return
                _vehicleRepository.SetVehicleRequestChange(parameter);
        }

        public virtual Vehicle.FuelType GetFuelTypes(Vehicle.FuelType parameter)
        {
            return
                _vehicleRepository.GetFuelTypes(parameter);
        }
    }
}