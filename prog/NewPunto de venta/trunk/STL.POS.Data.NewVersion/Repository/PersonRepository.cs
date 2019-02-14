﻿using Entity.Entities;
using STL.POS.Data.NewVersion.EdmxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STL.POS.Data.NewVersion.Repository
{
    public class PersonRepository : BaseRepository
    {
        public PersonRepository() { }

        #region Set
        public virtual BaseEntity SetPerson(Person.PersonParameters parameter)
        {
            BaseEntity result;
            IEnumerable<SP_SET_PERSONS_Result> temp;
            temp = PosContex.SP_SET_PERSONS
                (
                  parameter.id,
                  parameter.firstName,
                  parameter.secondName,
                  parameter.firstSurname,
                  parameter.secondSurname,
                  parameter.dateOfBirth,
                  parameter.isPrincipal,
                  parameter.address,
                  parameter.phoneNumber,
                  parameter.mobile,
                  parameter.workPhone,
                  parameter.maritalStatus,
                  parameter.job,
                  parameter.company,
                  parameter.yearsInCompany,
                  parameter.sex,
                  parameter.country_Id,
                  parameter.domesticreg_Id,
                  parameter.state_Prov_Id,
                  parameter.city_Id,
                  parameter.nationalityGlobalCountry_Id,
                  parameter.email,
                  parameter.identificationType,
                  parameter.identificationNumber,
                  parameter.foreignLicense,
                  parameter.identificationNumberValidDate,
                  parameter.invoiceTypeId,
                  parameter.postalCode,
                  parameter.annualIncome,
                  parameter.socialReasonId,
                  parameter.ownershipStructureId,
                  parameter.identificationFinalBeneficiaryOptionsId,
                  parameter.pepFormularyOptionsId,
                  parameter.home_Owner,
                  parameter.qtyPersonsDepend,
                  parameter.qtyEmployees,
                  parameter.linked,
                  parameter.segment,
                  parameter.fax,
                  parameter.uRL,
                  parameter.userId,
                  parameter.WorkAddress,
                  parameter.PlaceOfBirth,
                  parameter.TypeOfPerson,
                  parameter.ManagerName,
                  parameter.ManagerPepOptionId
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

        #endregion


        #region Get
        public virtual int GetPersonCountryUbicationOnSysflex(int Country_Id, int State_Prov_Id, int City_Id)
        {
            int result = 0;
            IEnumerable<SP_GET_COUNTRY_UBICACION_Result> temp;
            temp = PosContex.SP_GET_COUNTRY_UBICACION
                (
                null,
                null,
                Country_Id,
                null,
                State_Prov_Id,
                City_Id
                );

            var r = temp.FirstOrDefault();
            if (r != null)
            {
                result = r.UbicacionId;
            }

            return
                 result;
        }

        public virtual bool IsAgentFinancial(int AgentId)
        {
            var result = PosContex.SP_GET_AGENT_IS_FINANCIAL(AgentId).ToList().FirstOrDefault();
            return
                 result.GetValueOrDefault();
        }



        public virtual IEnumerable<Person.PersonUbication> GetPersonCountryUbicationByUbicationSysflex(int ubicationID)
        {
            IEnumerable<Person.PersonUbication> result;
            IEnumerable<SP_GET_COUNTRY_BY_UBICATION_ID_Result> temp;
            temp = PosContex.SP_GET_COUNTRY_BY_UBICATION_ID(ubicationID);

            result = temp.Select(x => new Person.PersonUbication()
            {
                Country_Id = x.Country_Id,
                Domesticreg_Id = x.Domesticreg_Id,
                State_Prov_Id = x.State_Prov_Id,
                City_Id = x.City_Id
            }).ToArray();

            return
                 result;
        }

        #endregion

    }
}