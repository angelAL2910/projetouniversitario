﻿using Entity.Entities;
using STL.POS.Data.NewVersion.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STL.POS.Logic
{
    public class VehicleProductManager : BaseManager
    {
        private readonly VehicleProductRepository vehicleProdyctRepository;

        public VehicleProductManager()
        {
            vehicleProdyctRepository = new VehicleProductRepository();
        }

        public BaseEntity SetVehicleProduct(VehicleProduct.Parameter parameter)
        {
            return vehicleProdyctRepository.SetVehicleProduct(parameter);
        }

        public int DeleteVehicleProductCoveragesServices(int VehicleID, bool? deleteVehicle)
        {
            return vehicleProdyctRepository.DeleteVehicleProductCoveragesServices(VehicleID, deleteVehicle);
        }

        public BaseEntity SetRequestChanges(RequestChanges.Parameter parameter)
        {
            return vehicleProdyctRepository.SetRequestChanges(parameter);
        }

        public IEnumerable<RequestChanges> GetRequestChanges(RequestChanges.Parameter parameter)
        {
            return vehicleProdyctRepository.GetRequestChanges(parameter);
        }
        
    }
}
