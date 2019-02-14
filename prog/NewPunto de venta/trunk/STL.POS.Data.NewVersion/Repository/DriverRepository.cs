﻿using Entity.Entities;
using STL.POS.Data.NewVersion.EdmxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STL.POS.Data.NewVersion.Repository
{
    public class DriverRepository : BaseRepository
    {
        public DriverRepository() { }

        #region Set
        public virtual BaseEntity SetDriver(Driver.parameters parameters)
        {
            BaseEntity result;
            IEnumerable<SP_SET_DRIVERS_Result> temp;
            temp = PosContex.SP_SET_DRIVERS(
                                           parameters.id,
                                           parameters.quotationId,
                                           parameters.yearsDriving,
                                           parameters.accidentsLast3Years,
                                           parameters.userId
                                           );

            result = temp.Select(p => new BaseEntity
            {
                Action = p.Action,
                EntityId = p.Id
            })
           .FirstOrDefault();

            return
                 result;
        }

        public virtual BaseEntity SetPepByDriver(PepFormulary.Parameter parameters)
        {
            BaseEntity result;
            IEnumerable<SP_SET_PEP_FORMULARY_Result> temp;
            temp = PosContex.SP_SET_PEP_FORMULARY(parameters.id,
                                                  parameters.personsID,
                                                  parameters.name,
                                                  parameters.relationshipId,
                                                  parameters.position,
                                                  parameters.fromYear,
                                                  parameters.toYear,
                                                  parameters.userId,
                                                  parameters.BeneficiaryId,
                                                  parameters.IsPepManagerCompany
                                                 );

            result = temp.Select(p => new BaseEntity
            {
                Action = p.Action,
                EntityId = p.Id
            })
           .FirstOrDefault();

            return
                 result;
        }

        public BaseEntity SetIdentificationFinalBeneneficiary(IdentificationFinalBeneficiary.Parameter parameter)
        {
            BaseEntity result;
            IEnumerable<SP_SET_IDENTIFICATION_FINAL_BENEFICIARY_Result> temp;
            temp = PosContex.SP_SET_IDENTIFICATION_FINAL_BENEFICIARY(
                                                                parameter.id,
                                                                parameter.personsID,
                                                                parameter.name,
                                                                parameter.percentageParticipation,
                                                                parameter.userId,
                                                                parameter.isPEP,
                                                                parameter.pepFormularyOptionsId,
                                                                parameter.IdentificationTypeId,
                                                                parameter.IdentificationNumber,
                                                                parameter.NationalityCountryId
                                                             );

            result = temp.Select(q => new BaseEntity()
            {
                EntityId = q.Id,
                Action = q.Action
            }).FirstOrDefault();

            return result;
        }
        #endregion

        #region Get

        public virtual Driver GetDriver(int driverID)
        {
            Driver result;
            IEnumerable<SP_GET_DRIVER_BY_ID_Result> temp;
            temp = PosContex.SP_GET_DRIVER_BY_ID(driverID);

            result = temp.Select(q => new Driver()
            {
                Id = q.Id,
                FirstName = q.FirstName,
                SecondName = q.SecondName,
                FirstSurname = q.FirstSurname,
                SecondSurname = q.SecondSurname,
                DateOfBirth = q.DateOfBirth,
                IsPrincipal = q.IsPrincipal,
                Address = q.Address,
                PhoneNumber = q.PhoneNumber,
                Mobile = q.Mobile,
                WorkPhone = q.WorkPhone,
                MaritalStatus = q.MaritalStatus,
                Job = q.Job,
                Company = q.Company,
                YearsInCompany = q.YearsInCompany,
                Sex = q.Sex,
                City_Country_Id = q.City_Country_Id,
                City_Domesticreg_Id = q.City_Domesticreg_Id,
                City_State_Prov_Id = q.City_State_Prov_Id,
                City_City_Id = q.City_City_Id,
                Nationality_Global_Country_Id = q.Nationality_Global_Country_Id.GetValueOrDefault(),
                Email = q.Email,
                IdentificationType = q.IdentificationType,
                IdentificationNumber = q.IdentificationNumber,
                ForeignLicense = q.ForeignLicense,
                IdentificationNumberValidDate = q.IdentificationNumberValidDate,
                InvoiceTypeId = q.InvoiceTypeId,
                UserId = q.UserId,
                PostalCode = q.PostalCode,
                AnnualIncome = q.AnnualIncome,
                SocialReasonId = q.SocialReasonId.GetValueOrDefault(),
                OwnershipStructureId = q.OwnershipStructureId.GetValueOrDefault(),
                IdentificationFinalBeneficiaryOptionsId = q.IdentificationFinalBeneficiaryOptionsId.GetValueOrDefault(),
                PepFormularyOptionsId = q.PepFormularyOptionsId.GetValueOrDefault(),
                Home_Owner = q.Home_Owner.GetValueOrDefault(),
                QtyPersonsDepend = q.QtyPersonsDepend.GetValueOrDefault(),
                QtyEmployees = q.QtyEmployees.GetValueOrDefault(),
                Linked = q.Linked,
                Segment = q.Segment,
                Fax = q.Fax,
                URL = q.URL,
                CityDesc = q.CityDesc,
                MunicipioDesc = q.MunicipioDesc,
                GlobalCountryDesc = q.GlobalCountryDesc,
                GlobalCountryDescEN = q.GlobalCountryDescEN,
                StateProvDesc = q.StateProvDesc,
                SocialReasonDesc = q.SocialReasonDesc,
                PepFormularyOptionsDesc = q.PepFormularyOptionsDesc,
                OwnershipStructureDesc = q.OwnershipStructureDesc,
                IdentificationFinalBeneficiaryOptionsDesc = q.IdentificationFinalBeneficiaryOptionsDesc,
                municipality_Id = q.Municipio_Id.GetValueOrDefault(),
                WorkAddress = q.WorkAddress,
                PlaceOfBirth = q.PlaceOfBirth,
                TypeOfPerson = q.TypeOfPerson,
                ManagerName = q.ManagerName,
                ManagerPepOptionId = q.ManagerPepOptionId
            })
            .FirstOrDefault();

            return result;
        }

        public virtual IEnumerable<PepFormulary> GetPepsFormularyByDriver(int driverID, string Source)
        {
            IEnumerable<PepFormulary> result;
            IEnumerable<SP_GET_PEP_FORMULARY_BY_DRIVER_Result> temp;
            temp = PosContex.SP_GET_PEP_FORMULARY_BY_DRIVER(driverID,Source);
            
            result = temp.Select(q => new PepFormulary()
            {
                Id = q.Id,
                PersonsID = q.PersonsID,
                name = q.Name,
                RelationshipId = q.RelationshipId,
                Position = q.Position,
                FromYear = q.FromYear,
                ToYear = q.ToYear,
                BeneficiaryId = q.BeneficiaryId,
                PepFormularyOptionsId = q.PepFormularyOptionsId
            })
            .ToArray();

            return result;
        }

        public IEnumerable<IdentificationFinalBeneficiary> GetIdentificationFinalBeneficiaries(int personID)
        {
            IEnumerable<IdentificationFinalBeneficiary> result;
            IEnumerable<SP_GET_BENEFICIARIES_BY_DRIVER_Result> temp;
            temp = PosContex.SP_GET_BENEFICIARIES_BY_DRIVER(personID);

            result = temp.Select(q => new IdentificationFinalBeneficiary()
            {
                Id = q.Id,
                name = q.Name,
                percentageParticipation = q.PercentageParticipation,
                isPEP = q.IsPEP,
                pepFormularyOptionsId = q.PepFormularyOptionsId,
                IdentityId = q.IdentityId,
                IdentificationTypeId = q.IdentificationTypeId.HasValue ? q.IdentificationTypeId.ToString(): null,
                IdentificationNumber = q.IdentificationNumber,
                NationalityCountryId = q.NationalityCountryId
            })
            .ToArray();

            return
                result;
        }
        #endregion

        public virtual int DeletePepsByDriver(int driverID, string Source)
        {
            int result = 0;
            result = PosContex.SP_DELETE_PEP_FORMULARY_BY_DRIVER(driverID, Source);
            return result;
        }

        public int DeleteBeneficiariesByDriver(int personID)
        {
            int result = 0;
            result = PosContex.SP_DELETE_IDENTIFICATION_FINAL_BENEFICIARY_BY_DRIVER(personID);
            return
                result;
        }
    }
}