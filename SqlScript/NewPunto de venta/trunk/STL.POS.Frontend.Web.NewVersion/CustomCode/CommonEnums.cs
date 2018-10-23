﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STL.POS.Frontend.Web.NewVersion.CustomCode
{
    public static class CommonEnums
    {
        public enum UserOrigins
        {
            POS,
            VO
        }

        public enum UserType
        {
            WebUser,
            Agent,
            Subscriptor
        }

        public enum UserStatus
        {
            Created,
            Active,
            Inactive
        }

        public enum DropDownType
        {
            SOCIALREASON,
            OWNERSHIPSTRUCTURE,
            COUNTRY,
            SURCHARGEPERCENTAGE,
            COLORS,
            JOBS,
            RELATIONSHIP,
            BRANDS,
            NEXTQUOTATION,
            CRIDITCARTYPES,
            OPERATORS,
            PRODUCTTYPEFAMILYBROCHURE,
            FILTROHISTORICO,
            TYPEOFPERSONS,
            DEFAULTCITY,
            GETALLUSERSACCESSPV,
            PHONETYPES,
            CONTACTFORM
        }

        public enum CoverageFilterType
        {
            DanosPropios = 1,
            DanosTerceros = 2,
            ServiciosSeleccionados = 3,
            DanospropiosDanosaTerceros = 4,
            Todo = 5
        }

        public enum QuotationStatus : byte
        {
            InProgress,
            Finalized,
        }

        public enum QuotationCardnetStatus
        {
            NotSent,
            AuthorizationSuccessfull,
            AuthorizationCancelled
        }

        public enum AchAccountType
        {
            Ahorro = 0,
            Corriente
        }

        public enum PaymentWay
        {
            Cash = 1,
            CreditCard = 2,
            ACH = 3
        }

        public enum CreditCardType
        {
            MasterCard = 1,
            AmericanExpress = 2,
            Visa = 3,
            Diners = 4,
            Discover = 5,
            Amex = 6
        }

        public enum Categories
        {
            General,
            Error
        }
        public enum PepFormularyOptions
        {
            SiDesignado = 1,
            SiVinculado = 2,
            No = 3
        }
        public enum FinalBeneficiaryOptions
        {
            SiParticipantesMayores20Porciento = 2,
            No = 5
        }

        public enum UserTypeEnum
        {
            Agent = 1,
            User = 2,
            Assistant = 3,
        }


        public enum RequestType
        {
            Emision = 1,
            Inclusion = 2,
            Exclusion = 3,
            Renovacion = 4,
            Cambios = 5
        }

        public enum ChangeConditionCatalog
        {
            Color = 5,
            Chasis = 6,
            Placa = 7
        }

        public enum ActionTypes
        {
            Exclusion,
            Inclusion           
        }
        public enum RiskLevel
        {
            RA, // Riesgo Alto     
            RM, // Riesgo Moderado     
            RB  // Riesgo Bajo
        }

        public enum AppModes
        {
            LEYMODE,
            FULLMODE
        }
    }
}