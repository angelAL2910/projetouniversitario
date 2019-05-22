﻿using STL.POS.Data.NewVersion.EdmxModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STL.POS.Data.NewVersion.Repository
{
    public class BaseRepository
    {
        private PosSiteDataContext DataContext;
        protected POS_SITEEntities PosContex;

        public enum MappingElementType
        {
            Storage = 1,
            Usage = 2,
            Color = 3,
            MaritalStatus = 4,
            VehicleType = 5
        }

        public BaseRepository()
        {
            DataContext = new PosSiteDataContext();
            PosContex = DataContext.PosContex;
        }      

        public Dictionary<int, int> getJobsByDesc(string desc)
        {
            var occupation = PosContex.SP_GET_JOBS().FirstOrDefault(x => x.Name == desc);
            Dictionary<int, int> dic = new Dictionary<int, int>();
            if (occupation != null)
            {
                dic.Add(occupation.Id, occupation.OccupGroup_Type_Id.GetValueOrDefault());

                return dic;
            }
            else
            {
                dic = null;
                
                return dic;
            }
        }

        public int GetMappingInfo(MappingElementType type, string posId, int defaultId = 1)
        {

            if (type == MappingElementType.VehicleType)
            {
                var intType = int.Parse(posId);
                var sfId = PosContex.SP_GET_ST_VEHICLE_TYPE().FirstOrDefault(v => v.Vehicle_Type_Id == intType);

                if (sfId == null)
                    return defaultId;
                else
                    return sfId.Core_Vehicle_Type_Id;
            }
            else if (type == MappingElementType.Usage)
            {
                var mapping = PosContex.SP_GET_VIRTUAL_OFFICE_INTEGRATION().FirstOrDefault(v => v.ElementType == Convert.ToInt32(type, CultureInfo.InvariantCulture) && v.ElementName.Contains(posId));

                if (mapping == null)
                    return defaultId;
                else
                    return mapping.VirtualOfficeId;
            }
            else
            {
                var mapping = PosContex.SP_GET_VIRTUAL_OFFICE_INTEGRATION().FirstOrDefault(v => v.ElementType == Convert.ToInt32(type, CultureInfo.InvariantCulture) && v.ElementName == posId);

                if (mapping == null)
                    return defaultId;
                else
                    return mapping.VirtualOfficeId;
            }
        }

    }
}