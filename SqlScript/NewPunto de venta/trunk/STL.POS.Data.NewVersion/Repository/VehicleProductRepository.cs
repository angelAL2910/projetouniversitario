﻿using Entity.Entities;
using STL.POS.Data.NewVersion.EdmxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STL.POS.Data.NewVersion.Repository
{
    public class VehicleProductRepository : BaseRepository
    {
        public VehicleProductRepository() { }

        #region Set
        public virtual BaseEntity SetVehicleProduct(VehicleProduct.Parameter parameter)
        {
            BaseEntity result;
            IEnumerable<SP_SET_VEHICLE_PRODUCT_Result> temp;
            temp = PosContex.SP_SET_VEHICLE_PRODUCT(
                                                      parameter.id,
                                                      parameter.vehicleDescription,
                                                      parameter.year,
                                                      parameter.cylinders,
                                                      parameter.passengers,
                                                      parameter.weight,
                                                      parameter.chassis,
                                                      parameter.plate,
                                                      parameter.color,
                                                      parameter.vehiclePrice,
                                                      parameter.insuredAmount,
                                                      parameter.percentageToInsure,
                                                      parameter.totalPrime,
                                                      parameter.totalIsc,
                                                      parameter.totalDiscount,
                                                      parameter.selectedProductCoreId,
                                                      parameter.vehicleTypeCoreId,
                                                      parameter.selectedProductName,
                                                      parameter.vehicleTypeName,
                                                      parameter.vehicleMakeName,
                                                      parameter.usageId,
                                                      parameter.usageName,
                                                      parameter.storeId,
                                                      parameter.storeName,
                                                      parameter.driver_Id,
                                                      parameter.vehicleModel_Make_Id,
                                                      parameter.vehicleModel_Model_Id,
                                                      parameter.quotation_Id,
                                                      parameter.selectedVehicleTypeId,
                                                      parameter.selectedVehicleTypeName,
                                                      parameter.selectedCoverageCoreId,
                                                      parameter.selectedCoverageName,
                                                      parameter.vehicleYearOld,
                                                      parameter.surChargePercentage,
                                                      parameter.numeroFormulario,
                                                      parameter.rateJson,
                                                      parameter.userId,
                                                      parameter.secuenciaVehicleSysflex,
                                                      parameter.isFacultative,
                                                      parameter.amountFacultative,
                                                      parameter.vehicleQuantity,
                                                      parameter.ProratedPremium,
                                                      parameter.SelectedVehicleFuelTypeId,
                                                      parameter.SelectedVehicleFuelTypeDesc
                                                   );

            result = temp.Select(q => new BaseEntity()
            {
                EntityId = q.Id,
                Action = q.Action
            })
            .FirstOrDefault();

            return result;
        }

        public virtual BaseEntity SetRequestChanges(RequestChanges.Parameter parameter)
        {
            BaseEntity result;
            IEnumerable<SP_SET_REQUEST_CHANGES_Result> temp;
            temp = PosContex.SP_SET_REQUEST_CHANGES(
                                                      parameter.change_Id,
                                                      parameter.policy_Number,
                                                      parameter.condition_Id,
                                                      parameter.vehicle_Secuence,
                                                      parameter.old_Value,
                                                      parameter.new_Value,
                                                      parameter.source,
                                                      parameter.create_User
                                                   );

            result = temp.Select(q => new BaseEntity()
            {
                EntityId = q.Change_Id,
                Action = q.Action
            })
            .FirstOrDefault();

            return result;
        }

        #endregion


        #region Get

        public virtual IEnumerable<RequestChanges> GetRequestChanges(RequestChanges.Parameter parameter)
        {
            IEnumerable<RequestChanges> result;
            IEnumerable<SP_GET_REQUEST_CHANGES_Result> temp;
            temp = PosContex.SP_GET_REQUEST_CHANGES(
                parameter.policy_Number,
                parameter.IsLasRecord,
                parameter.condition_Id
                );

            result = temp.Select(p => new RequestChanges
            {
                Change_Id = p.Change_Id,
                Policy_Number = p.Policy_Number,
                Condition_Id = p.Condition_Id,
                Create_Date = p.Create_Date,
                Create_User = p.Create_User,
                New_Value = p.New_Value,
                Old_Value = p.Old_Value,
                Source = p.Source,
                Vehicle_Secuence = p.Vehicle_Secuence
            })
            .ToArray();

            return
                 result;
        }

        #endregion

        #region Delete
        public virtual int DeleteVehicleProductCoveragesServices(int VehicleID, bool? deleteVehicle)
        {
            var result = 0;
            PosContex.SP_DELETE_VEHICLE_PRODUCT_COVERAGES(VehicleID, deleteVehicle);
            return
                result;
        }
        #endregion

    }
}