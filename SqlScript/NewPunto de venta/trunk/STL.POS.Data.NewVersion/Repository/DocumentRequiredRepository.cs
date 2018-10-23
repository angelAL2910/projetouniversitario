﻿using Entity.Entities;
using STL.POS.Data.NewVersion.EdmxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STL.POS.Data.NewVersion.Repository
{
    public class DocumentRequiredRepository : BaseRepository
    {
        public DocumentRequiredRepository() { }

        #region Set

        public virtual int SetQuotationRequeriment(Requeriments.SetRequerimentsParameters parameter)
        {
            return PosContex.SP_SET_QUOTATION_REQUIREMENT_INSERT
                (
                parameter.quotationId,
                parameter.documentId,
                parameter.requirementTypeId,
                parameter.vehicleId,
                parameter.personId,
                parameter.documentBinary,
                parameter.documentName,
                parameter.userId
                );
        }

        public virtual int DeleteQuotationRequeriment(Requeriments.SetRequerimentsParameters parameter)
        {
            return PosContex.SP_SET_QUOTATION_REQUIREMENT_DELETE
                (
                parameter.quotationId,
                parameter.documentId,
                parameter.userId
                );
        }

        public virtual int ValidateQuotationRequeriment(Requeriments.SetRequerimentsParameters parameter)
        {
            return PosContex.SP_SET_QUOTATION_REQUIREMENT_VALIDATED
                (
                parameter.quotationId,
                parameter.documentId,
                parameter.validated,
                parameter.userId
                );
        }

        public virtual int SendDocumentRequiredToGlobal(Requeriments.SetRequerimentsParameters parameter)
        {
            return PosContex.SP_SET_QUOTATION_REQUIRED_DOCUMENT_TO_GLOBAL
                (
                parameter.quotationId,
                parameter.userId
                );
        }

        #endregion


        #region Get

        public virtual IEnumerable<Requeriments> GeQuotationRequeriments(Requeriments.GetRequerimentsParameters parameters)
        {
            IEnumerable<SP_GET_QUOTATION_REQUIREMENT_Result> temp;
            IEnumerable<Requeriments> result = null;

            temp = PosContex.SP_GET_QUOTATION_REQUIREMENT(parameters.quotationId, parameters.RiskLevel);

            result = temp.Select(r => new Requeriments()
            {
                RequirementTypeId = r.Requirement_Type_Id,
                RequirementTypeDesc = r.Requirement_Type_Desc,
                OnBaseNameKey = r.On_Base_Name_Key,
                Required = (r.Required == 1 ? true : false),
                QuotationId = r.Quotation_Id,
                DocumentId = r.Document_Id,
                VehicleId = r.Vehicle_Id,
                PersonId = r.Person_Id,
                DocumentName = r.Document_Name,
                Validated = r.Validated,
                ValidatedUsrId = r.Validated_UsrId,
                CreateUsrId = r.Create_UsrId,
                ValidatedUserName = r.Validated_UserName,
                CreateUserName = r.Create_UserName
            });

            return
                 result;
        }

        public virtual IEnumerable<Requeriments.Documents> GeQuotationRequerimentsByDocument(Requeriments.GetRequerimentsParameters parameters)
        {
            IEnumerable<SP_GET_QUOTATION_REQUIREMENT_DOC_Result> temp;
            IEnumerable<Requeriments.Documents> result = null;

            temp = PosContex.SP_GET_QUOTATION_REQUIREMENT_DOC(parameters.quotationId, parameters.documentId);

            result = temp.Select(rd => new Requeriments.Documents()
            {
                QuotationId = rd.Quotation_Id,
                DocumentId = rd.Document_Id,
                DocumentBinary = rd.Document_Binary
            });

            return
                 result;
        }

        #endregion

    }
}