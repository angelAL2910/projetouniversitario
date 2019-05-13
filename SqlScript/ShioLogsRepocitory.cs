﻿using ShipLogs.Data.EdmxModel;
using ShipLogs.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipLogs.Data.Repository
{
    public class ShioLogsRepocitory : BaseRepository
    {
        public ShioLogsRepocitory() { }
        /// <summary>
        /// Esto guarda o actualiza los envios o resivos
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual IEnumerable<Generic> Set_Shimet(ShipmentEntity parameters)
        {
            IEnumerable<Generic> result;
            try
            {

                IEnumerable<SP_SET_SHIPMENTS_UPDATE_INSERT_Result> temp;

                temp = ShipLogsContex.SP_SET_SHIPMENTS_UPDATE_INSERT(
                        parameters.ShipUniqueID
                      , parameters.CarrierName
                      , parameters.AccountNumber
                      , parameters.ShipmentNumber
                      , parameters.ShipmentDate
                      , parameters.ShipmentWeight
                      , parameters.ShipmentQTY
                      , parameters.ShipPackageType
                      , parameters.Operator
                      , parameters.Sender
                      , parameters.Receiver
                      , parameters.ReceiverAttn
                      , parameters.ReceiverAddress
                      , parameters.ReceiverCity
                      , parameters.ReceiverState
                      , parameters.ReceiverZipCode
                      , parameters.ReceiverCountry
                      , parameters.ReceiverPhoneNumber
                      , parameters.ShipmentComments
                      , parameters.Transit
                      , parameters.Incoming
                      , parameters.CommissionChecks
                      , parameters.Materials
                      , parameters.OtherContents
                    );

                result = temp.Select(d => new Generic
                {
                    id = d.Id,
                    Value = d.Value,
                    IdAlf = d.IdAlf

                }).ToArray();
                return
               result;
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        /// <summary>
        /// Esto crea los paquetes n cantidad por envio
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual int Set_Shimet_DetailsSave(ShipmentDetailEntity parameters)
        {
            int result = 0;
            using (var dbo = new ShipLogs_Entities())
            {
                result = dbo.SP_SET_SHIPMENT_DETAIL_SAVE(
                                 parameters.ShipUniqueID,
                                 parameters.AssignedTo,
                                 parameters.ItemDetail,
                                 parameters.DetailUniqueID
            );
                return
                    result;
            }
        }
        /// <summary>
        /// Esto actualiza los detalles de los paquetes
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual int Set_Shimet_DetailsUpdate(ShipmentDetailEntity parameters)
        {
            int result = 0;
            using (var dbo = new ShipLogs_Entities())
            {
                result = dbo.SP_SET_SHIPMENT_DETAIL_UPDATE(
                                 parameters.ShipUniqueID,
                                 parameters.AssignedTo,
                                 parameters.ItemDetail,
                                 parameters.DetailUniqueID
            );
                return
                    result;
            }
        }

        public virtual IEnumerable<ShipmentEntity> GET_Shimet(String parameters)
        {
            using (var dbo = new ShipLogs_Entities())
            {
                IEnumerable<ShipmentEntity> RetornarValue = dbo.Database.SqlQuery<ShipmentEntity>("EXEC [dbo].[SP_FROM_Shipment] @ShipmentNumberParameter",
                    new SqlParameter("@ShipmentNumberParameter", parameters)).ToList();
                return RetornarValue.ToArray();
            }
        }

        public virtual IEnumerable<ShipmentViewModel> GET_ShimetAll()
        {
            try
            {
                using (var dbo = new ShipLogs_Entities())
                {
                    IEnumerable<ShipmentViewModel> RetornarValue = dbo.Database.SqlQuery<ShipmentViewModel>("EXEC [dbo].[SP_FROM_Shipment_All]"
                   ).ToList();
                    return RetornarValue.ToArray();
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public virtual IEnumerable<ShipmentViewModel> GET_Obtain_Shimet_Incoming(ShipmentViewModel param)
        {
            using (var dbo = new ShipLogs_Entities())
            {
                IEnumerable<ShipmentViewModel> RetornarValue = dbo.Database.SqlQuery<ShipmentViewModel>("EXEC [SP_GET_Obtain_Shimet_Incoming] @param",
                    new SqlParameter("@param", param.ShipUniqueID)).ToList();
                return RetornarValue.ToArray();
            }
        }
        public virtual ShipmentEntity GET_Obtain_Shimet_Outgoing(int param)
        {
            using (var dbo = new ShipLogs_Entities())
            {
                IEnumerable<ShipmentEntity> RetornarValue = dbo.Database.SqlQuery<ShipmentEntity>("EXEC  [dbo].[SP_GET_Obtain_Shimet_Outgoing] @param",
                    new SqlParameter("@param", param)).ToList();
                return RetornarValue.FirstOrDefault();
            }
        }
        public virtual ShipmentEntity IsIncomingVerification(ShipmentEntity param)
        {
            using (var dbo = new ShipLogs_Entities())
            {
                IEnumerable<ShipmentEntity> RetornarValue = dbo.Database.SqlQuery<ShipmentEntity>("EXEC [DBO].[SP_GET_CHECK_LOGTYPE] @param",
                    new SqlParameter("@param", param.ShipUniqueID)).ToList();
                return RetornarValue.FirstOrDefault();
            }
        }

        public virtual List<CarrierEntity> GetCarrier(string name)
        {
            var result = new List<CarrierEntity>();
            using (var dbo = new ShipLogs_Entities())
            {
                result = dbo.Carriers.Where(x => x.CarrierName.Contains(name) && x.CarrierStatus == true).Select(x => new CarrierEntity
                {
                    CarrierName = x.CarrierName.ToUpper().TrimEnd().TrimEnd()

                }).ToList();
            }
            return result;
        }

        //public virtual List<CarrierEntity> GetCarrierDirec()
        //{
        //    var result = new List<CarrierEntity>();
        //    using (var dbo = new ShipLogs_Entities())
        //    {
        //        result = dbo.Carriers.Where(x =>x.CarrierStatus == true).OrderBy(x=>x.position).Select(x => new CarrierEntity
        //        {
        //            CarrierName = x.CarrierName.ToUpper().TrimEnd().TrimEnd(),
        //            CarrierUniqueID=x.CarrierUniqueID,
        //            CarrierStatus=x.CarrierStatus

        //        }).ToList();
        //    }
        //    return result;
        //}

        public virtual List<CarrierEntity> GetCarrierDirec()
        {
            using (var dbo = new ShipLogs_Entities())
            {
                IEnumerable<CarrierEntity> RetornarValue = dbo.Database.SqlQuery<CarrierEntity>("EXEC [DBO].[SP_GetCarrierDirec]").ToList();
                return RetornarValue.ToList();
            }
        }
        public virtual List<OperatorEntity> GetOperator()
        {
            using (var dbo = new ShipLogs_Entities())
            {
                IEnumerable<OperatorEntity> RetornarValue = dbo.Database.SqlQuery<OperatorEntity>("EXEC [DBO].[SP_GET_OPERATOR]").ToList();
                return RetornarValue.ToList();
            }
        }

        public virtual List<CarrierEntity> ListCarrier(string name)
        {
            var result = new List<CarrierEntity>();
            using (var dbo = new ShipLogs_Entities())
            {
                result = dbo.Carriers.Where(x =>x.CarrierStatus == true).Select(x => new CarrierEntity
                {
                    CarrierName = x.CarrierName.ToUpper().TrimEnd().TrimEnd()

                }).ToList();
            }
            return result;
        }



    }
}
