﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity.Entities;

namespace STL.POS.Frontend.Web.NewVersion.CustomCode
{
    public class QuotationViewModel : Quotation
    {
        public List<drivers> _drivers { get; set; }
        public List<Vehicles> _vehicles { get; set; }
        public List<serviceTypes> _serviceTypes { get; set; }
        public User _agentQuotation { get; set; }

        public class drivers : Driver
        {
            public string GetFullName()
            {
                return FirstName + " " + FirstSurname;
            }
        }

        public class Vehicles : VehicleProduct
        {
            public long? VehicleNumber { get; set; }
            public List<coverages> coverages { get; set; }
            public VehicleProductLimits vehicleProductLimits { get; set; }
            public List<coverages> _services { get; set; }
            public decimal TotalPrimeVehicle { get; set; }
            public string SelectedDeductibleName { get; set; }
        }

        public class coverages : Coverage
        {

        }

        public class VehicleProductLimits : ProductLimits
        {

        }

        public class serviceTypes : ServicesTypes
        {

        }

        public drivers GetPrincipalDriver()
        {
            if (_drivers != null && _drivers.Count > 0)
            {
                var principal = _drivers.Where(d => d.IsPrincipal).FirstOrDefault();
                if (principal != null)
                    return principal;
                else
                    return new drivers();
            }
            else
                return new drivers();
        }

        public List<drivers> GetListOfDriver()
        {
            if (_drivers != null && _drivers.Count > 0)
            {
                return _drivers;
            }
            else
                return new List<drivers>();
        }

        public class OldRequestValues
        {
            public string OldChasis { get; set; }
            public string OldPlate { get; set; }
            public string OldColor { get; set; }
        }

    }
}