﻿using DI.UnderWriting.Interfaces;
using Entity.UnderWriting.Entities;
using LOGIC.UnderWriting.Global;
using System;
using System.Collections.Generic;

namespace DI.UnderWriting.Implementations
{
    public class VehicleBll : IVehicle
    {
        private VehicleManager _vehicleManager;

        public VehicleBll()
        {
            _vehicleManager = new VehicleManager();
        }

        IEnumerable<Vehicle.Insured.Detail> IVehicle.GetVehicleInsuredDetail(Vehicle.Policy policy)
        {
            return
                _vehicleManager.GetVehicleInsuredDetail(policy);
        }

        IEnumerable<Vehicle.Insured> IVehicle.GetVehicleInsured(Vehicle.Policy policy)
        {
            return
                _vehicleManager.GetVehicleInsured(policy);
        }

        IEnumerable<Vehicle.Review> IVehicle.GetVehicleReview(Vehicle vehicle)
        {
            return
                _vehicleManager.GetVehicleReview(vehicle);
        }

        IEnumerable<Vehicle.Review.Pic> IVehicle.GetVehicleReviewPic(Vehicle.Review review)
        {
            return
                _vehicleManager.GetVehicleReviewPic(review);
        }

        IEnumerable<Vehicle.Review.Item.Option> IVehicle.GetVehicleReviewItemOption(Vehicle.Review review)
        {
            return
                _vehicleManager.GetVehicleReviewItemOption(review);
        }

        Vehicle.Document.Category IVehicle.GetDocumentCategory(string nameKey)
        {
            return
                _vehicleManager.GetDocumentCategory(nameKey);
        }

        Vehicle.Review IVehicle.SetVehicleReview(Vehicle.Review review)
        {
            return
                _vehicleManager.SetVehicleReview(review);
        }

        int? IVehicle.DeleteVehicleReviewDetail(Vehicle.Review.Detail detail)
        {
            return
                _vehicleManager.DeleteVehicleReviewDetail(detail);
        }

        Vehicle.Review.Detail IVehicle.SetVehicleReviewDetail(Vehicle.Review.Detail detail)
        {
            return
                _vehicleManager.SetVehicleReviewDetail(detail);
        }

        IEnumerable<Vehicle.Review.Detail> IVehicle.GetVehicleReviewDetail(Vehicle.Review review)
        {
            return
                _vehicleManager.GetVehicleReviewDetail(review);
        }

        Vehicle.Document IVehicle.SetDocument(Vehicle.Document document)
        {
            return
                _vehicleManager.SetDocument(document);
        }

        Vehicle.Document IVehicle.SetPolicyDocument(Vehicle.Document document)
        {
            return
                _vehicleManager.SetPolicyDocument(document);
        }

        Vehicle.Review.Pic IVehicle.SetVehicleReviewPic(Vehicle.Review.Pic review)
        {
            return
                _vehicleManager.SetVehicleReviewPic(review);
        }

        Vehicle.Agent.AssignedOffice IVehicle.GetAgentAssignedOffice(Vehicle.Agent agent)
        {
            return
                _vehicleManager.GetAgentAssignedOffice(agent);
        }

        Vehicle.Review.VIG IVehicle.SetVehicleReviewVIG(Vehicle.Review.VIG vig)
        {
            return
                _vehicleManager.SetVehicleReviewVIG(vig);
        }

        IEnumerable<Vehicle.Review.VIG> IVehicle.GetVehicleReviewVIG(Vehicle.Review.VIG vig)
        {
            return
                _vehicleManager.GetVehicleReviewVIG(vig);
        }

        IEnumerable<Vehicle.TransmissionType> IVehicle.GetVehicleTransmissionTypes(Vehicle.TransmissionType vehicle_transmission_type)
        {
            return
                _vehicleManager.GetVehicleTransmissionTypes(vehicle_transmission_type);
        }

        IEnumerable<Vehicle.Classes> IVehicle.GetVehicleClasses(Vehicle.Classes vehicle_class)
        {
            return
                _vehicleManager.GetVehicleClasses(vehicle_class);
        }

        IEnumerable<Vehicle.WheelDriveType> IVehicle.GetVehicleWheelDriveTypes(Vehicle.WheelDriveType vehicle_wheel_drive_type)
        {
            return
                _vehicleManager.GetVehicleWheelDriveTypes(vehicle_wheel_drive_type);
        }

        IEnumerable<Vehicle.Version> IVehicle.GetVehicleVersions(Vehicle.Version vehicle_version)
        {
            return
                _vehicleManager.GetVehicleVersions(vehicle_version);
        }

        IEnumerable<Vehicle.Document> IVehicle.GetPolicyDocument(Vehicle.Policy policy)
        {
            return
                _vehicleManager.GetPolicyDocument(policy);
        }

        int? IVehicle.DeletePolicyDocument(Vehicle.Review review)
        {
            return
                _vehicleManager.DeletePolicyDocument(review);
        }

        IEnumerable<RequestChanges> IVehicle.GetVehicleRequestChange(string PolicyNumber, int? ConditionID, bool IsLastRecord = true)
        {
            return
                _vehicleManager.GetVehicleRequestChange(PolicyNumber, ConditionID, IsLastRecord);
        }
        RequestChanges IVehicle.SetVehicleRequestChange(RequestChanges.Parameter parameter)
        {
            return
                _vehicleManager.SetVehicleRequestChange(parameter);
        }

        Vehicle.FuelType IVehicle.GetFuelTypes(Vehicle.FuelType parameter)
        {
            return
                _vehicleManager.GetFuelTypes(parameter);
        }
    }



    public class VehicleWS : IVehicle
    {

        IEnumerable<Vehicle.Insured.Detail> IVehicle.GetVehicleInsuredDetail(Vehicle.Policy policy)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Vehicle.Insured> IVehicle.GetVehicleInsured(Vehicle.Policy policy)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Vehicle.Review> IVehicle.GetVehicleReview(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Vehicle.Review.Pic> IVehicle.GetVehicleReviewPic(Vehicle.Review review)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Vehicle.Review.Item.Option> IVehicle.GetVehicleReviewItemOption(Vehicle.Review review)
        {
            throw new NotImplementedException();
        }

        Vehicle.Document.Category IVehicle.GetDocumentCategory(string nameKey)
        {
            throw new NotImplementedException();
        }

        Vehicle.Review IVehicle.SetVehicleReview(Vehicle.Review review)
        {
            throw new NotImplementedException();
        }

        int? IVehicle.DeleteVehicleReviewDetail(Vehicle.Review.Detail detail)
        {
            throw new NotImplementedException();
        }

        Vehicle.Review.Detail IVehicle.SetVehicleReviewDetail(Vehicle.Review.Detail detail)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Vehicle.Review.Detail> IVehicle.GetVehicleReviewDetail(Vehicle.Review review)
        {
            throw new NotImplementedException();
        }

        Vehicle.Document IVehicle.SetDocument(Vehicle.Document document)
        {
            throw new NotImplementedException();
        }

        Vehicle.Document IVehicle.SetPolicyDocument(Vehicle.Document document)
        {
            throw new NotImplementedException();
        }

        Vehicle.Review.Pic IVehicle.SetVehicleReviewPic(Vehicle.Review.Pic review)
        {
            throw new NotImplementedException();
        }

        Vehicle.Agent.AssignedOffice IVehicle.GetAgentAssignedOffice(Vehicle.Agent agent)
        {
            throw new NotImplementedException();
        }

        Vehicle.Review.VIG IVehicle.SetVehicleReviewVIG(Vehicle.Review.VIG vig)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Vehicle.Review.VIG> IVehicle.GetVehicleReviewVIG(Vehicle.Review.VIG vig)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Vehicle.TransmissionType> IVehicle.GetVehicleTransmissionTypes(Vehicle.TransmissionType vehicle_transmission_type)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Vehicle.Classes> IVehicle.GetVehicleClasses(Vehicle.Classes vehicle_class)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Vehicle.WheelDriveType> IVehicle.GetVehicleWheelDriveTypes(Vehicle.WheelDriveType vehicle_wheel_drive_type)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Vehicle.Version> IVehicle.GetVehicleVersions(Vehicle.Version vehicle_version)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Vehicle.Document> IVehicle.GetPolicyDocument(Vehicle.Policy policy)
        {
            throw new NotImplementedException();
        }

        int? IVehicle.DeletePolicyDocument(Vehicle.Review review)
        {
            throw new NotImplementedException();
        }
        IEnumerable<RequestChanges> IVehicle.GetVehicleRequestChange(string PolicyNumber, int? ConditionID, bool IsLastRecord = true)
        {
            throw new NotImplementedException();
        }
        RequestChanges IVehicle.SetVehicleRequestChange(RequestChanges.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        Vehicle.FuelType IVehicle.GetFuelTypes(Vehicle.FuelType parameter)
        {
            throw new NotImplementedException();
        }
    }
}