﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WEB.NewBusiness.Common.Thunderhead
{
    [XmlRoot(ElementName = "Transaction")]
    public class Transaction
    {
        [XmlElement(ElementName = "DocumentId")]
        public string DocumentId { get; set; }
        [XmlElement(ElementName = "Username")]
        public string Username { get; set; }
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }
        [XmlElement(ElementName = "BCCopy")]
        public string BCCopy { get; set; }
        [XmlElement(ElementName = "CCopy")]
        public string CCopy { get; set; }

        [XmlElement(ElementName = "FullName")]
        public string FullName { get; set; }

        [XmlElement(ElementName = "Plan")]
        public string Plan { get; set; }
    }

    [XmlRoot(ElementName = "PolicyInfo")]
    public class PolicyInfo
    {
        [XmlElement(ElementName = "FullName")]
        public string FullName { get; set; }
        [XmlElement(ElementName = "DUI")]
        public string DUI { get; set; }
        [XmlElement(ElementName = "NoPoliza")]
        public string NoPoliza { get; set; }
        [XmlElement(ElementName = "Ramo")]
        public string Ramo { get; set; }
        [XmlElement(ElementName = "Product")]
        public string Product { get; set; }
        [XmlElement(ElementName = "AgenteComercial")]
        public string AgenteComercial { get; set; }
        [XmlElement(ElementName = "NumeroAgenteComercial")]
        public string NumeroAgenteComercial { get; set; }
        [XmlElement(ElementName = "OficinaComercial")]
        public string OficinaComercial { get; set; }
        [XmlElement(ElementName = "Supervisor")]
        public string Supervisor { get; set; }
        [XmlElement(ElementName = "InsuredAmount")]
        public string InsuredAmount { get; set; }
        [XmlElement(ElementName = "Coverage")]
        public string Coverage { get; set; }
        [XmlElement(ElementName = "PolicyPeriodStart")]
        public string PolicyPeriodStart { get; set; }
        [XmlElement(ElementName = "PolicyPeriodEnd")]
        public string PolicyPeriodEnd { get; set; }
        [XmlElement(ElementName = "EffectiveDate")]
        public string EffectiveDate { get; set; }
        [XmlElement(ElementName = "PolicyCancellationDate")]
        public string PolicyCancellationDate { get; set; }
        [XmlElement(ElementName = "DiasVencimiento")]
        public string DiasVencimiento { get; set; }
        [XmlElement(ElementName = "IssueDate")]
        public string IssueDate { get; set; }
        [XmlElement(ElementName = "IssueTime")]
        public string IssueTime { get; set; }
        [XmlElement(ElementName = "Clause")]
        public string Clause { get; set; }
        [XmlElement(ElementName = "EndorsementNo")]
        public string EndorsementNo { get; set; }
        [XmlElement(ElementName = "TextoConvenio")]
        public string TextoConvenio { get; set; }
        [XmlElement(ElementName = "CoinsuranceAmericanFormat")]
        public int CoinsuranceAmericanFormat { get; set; }
        [XmlElement(ElementName = "CompensationPeriod")]
        public int CompensationPeriod { get; set; }
        [XmlElement(ElementName = "RelatedPolicyNo")]
        public string RelatedPolicyNo { get; set; }
        [XmlElement(ElementName = "CompanyName")]
        public string CompanyName { get; set; }
    }

    [XmlRoot(ElementName = "Business")]
    public class Business
    {
        [XmlElement(ElementName = "OperatingProfit")]
        public decimal OperatingProfit { get; set; }
        [XmlElement(ElementName = "FeesEarned")]
        public decimal FeesEarned { get; set; }
        [XmlElement(ElementName = "DividendsReceived")]
        public decimal DividendsReceived { get; set; }
        [XmlElement(ElementName = "EarnedIncome")]
        public decimal EarnedIncome { get; set; }
        [XmlElement(ElementName = "ProfitBeforeTax")]
        public decimal ProfitBeforeTax { get; set; }
    }

    [XmlRoot(ElementName = "FixedExpenses")]
    public class FixedExpenses
    {
        [XmlElement(ElementName = "Valor")]
        public string Valor { get; set; }
        [XmlElement(ElementName = "Descripcion")]
        public string Descripcion { get; set; }
    }

    [XmlRoot(ElementName = "PaymentArray")]
    public class PaymentArray
    {
        [XmlElement(ElementName = "NoFactura")]
        public string NoFactura { get; set; }
        [XmlElement(ElementName = "NoAcuerdo")]
        public string NoAcuerdo { get; set; }
        [XmlElement(ElementName = "NoPoliza")]
        public string NoPoliza { get; set; }
        [XmlElement(ElementName = "Moviemiento")]
        public string Moviemiento { get; set; }
        [XmlElement(ElementName = "Cuota")]
        public string Cuota { get; set; }
        [XmlElement(ElementName = "FechaPago")]
        public string FechaPago { get; set; }
        [XmlElement(ElementName = "Financiamiento")]
        public string Financiamiento { get; set; }
        [XmlElement(ElementName = "ValorCuota")]
        public string ValorCuota { get; set; }
        [XmlElement(ElementName = "TotalPrime")]
        public string TotalPrime { get; set; }
    }

    [XmlRoot(ElementName = "PaymentDetail")]
    public class PaymentDetail
    {
        [XmlElement(ElementName = "MotivoCancelacion")]
        public string MotivoCancelacion { get; set; }
        [XmlElement(ElementName = "ValorPoliza")]
        public string ValorPoliza { get; set; }
        [XmlElement(ElementName = "TotalPagado")]
        public string TotalPagado { get; set; }
        [XmlElement(ElementName = "ValorProrrata")]
        public string ValorProrrata { get; set; }
        [XmlElement(ElementName = "PaymentAmount")]
        public string PaymentAmount { get; set; }
        [XmlElement(ElementName = "NumberOfPayments")]
        public string NumberOfPayments { get; set; }
        [XmlElement(ElementName = "PaymentMethod")]
        public string PaymentMethod { get; set; }
        [XmlElement(ElementName = "AmountReceived")]
        public string AmountReceived { get; set; }
        [XmlElement(ElementName = "ValorTotalaPagar")]
        public string ValorTotalaPagar { get; set; }
        [XmlElement(ElementName = "ValorVencido")]
        public string ValorVencido { get; set; }
        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "NoAprobacion")]
        public string NoAprobacion { get; set; }
        [XmlElement(ElementName = "CreditCardNumber")]
        public string CreditCardNumber { get; set; }
        [XmlElement(ElementName = "TransactionDate")]
        public string TransactionDate { get; set; }
        [XmlElement(ElementName = "FechaPago")]
        public string FechaPago { get; set; }
        [XmlElement(ElementName = "FechaCalculo")]
        public string FechaCalculo { get; set; }
        [XmlElement(ElementName = "FechaRecepciondelAporte")]
        public string FechaRecepciondelAporte { get; set; }
        [XmlElement(ElementName = "FechaAutorizacion")]
        public string FechaAutorizacion { get; set; }
        [XmlElement(ElementName = "NoRecibo")]
        public string NoRecibo { get; set; }
        [XmlElement(ElementName = "NRC")]
        public string NRC { get; set; }
        [XmlElement(ElementName = "ReceivedFrom")]
        public string ReceivedFrom { get; set; }
        [XmlElement(ElementName = "ConceptOf")]
        public string ConceptOf { get; set; }
        [XmlElement(ElementName = "QuotaNo")]
        public string QuotaNo { get; set; }
        [XmlElement(ElementName = "PaymentArray")]
        public PaymentArray PaymentArray { get; set; }
    }

    [XmlRoot(ElementName = "Reclamo")]
    public class Reclamo
    {
        [XmlElement(ElementName = "Reclamante")]
        public string Reclamante { get; set; }
        [XmlElement(ElementName = "NoReclamo")]
        public string NoReclamo { get; set; }
        [XmlElement(ElementName = "FechaSiniestro")]
        public string FechaSiniestro { get; set; }
        [XmlElement(ElementName = "FechaIndemnizacion")]
        public string FechaIndemnizacion { get; set; }
    }

    [XmlRoot(ElementName = "ElementoAsegurado")]
    public class ElementoAsegurado
    {
        [XmlElement(ElementName = "Descripcion")]
        public string Descripcion { get; set; }
        [XmlElement(ElementName = "Valor")]
        public string Valor { get; set; }
        [XmlElement(ElementName = "ValorCedido")]
        public string ValorCedido { get; set; }
    }

    [XmlRoot(ElementName = "Construccion")]
    public class Construccion
    {
        [XmlElement(ElementName = "Tipo")]
        public string Tipo { get; set; }
        [XmlElement(ElementName = "Descripcion")]
        public string Descripcion { get; set; }

        [XmlElement(ElementName = "ValorProyecto")]
        public decimal ValorProyecto { get; set; }
        [XmlElement(ElementName = "Location")]
        public string Location { get; set; }
        [XmlElement(ElementName = "Proyecto")]
        public string Proyecto { get; set; }
        [XmlElement(ElementName = "Exclusions")]
        public string Exclusions { get; set; }
        [XmlElement(ElementName = "DuracionDeLaObra")]
        public string DuracionDeLaObra { get; set; }
    }


    [XmlRoot(ElementName = "CrystalsandSigns")]
    public class CrystalsandSigns
    {
        [XmlElement(ElementName = "CrystalLocation")]
        public string CrystalLocation { get; set; }
        [XmlElement(ElementName = "Deducible")]
        public string Deducible { get; set; }
        [XmlElement(ElementName = "Minimo")]
        public string Minimo { get; set; }
        [XmlElement(ElementName = "TipoMedida")]
        public string TipoMedida { get; set; }
        [XmlElement(ElementName = "Alto")]
        public string Alto { get; set; }
        [XmlElement(ElementName = "Ancho")]
        public string Ancho { get; set; }
        [XmlElement(ElementName = "ValorAsegurado")]
        public decimal ValorAsegurado { get; set; }
    }


    [XmlRoot(ElementName = "CotizacionFire")]
    public class CotizacionFire
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "LastName")]
        public string LastName { get; set; }
        [XmlElement(ElementName = "QuotationNumber")]
        public string QuotationNumber { get; set; }
        [XmlElement(ElementName = "QuotationDate")]
        public string QuotationDate { get; set; }
        [XmlElement(ElementName = "ProposalDate")]
        public string ProposalDate { get; set; }
        [XmlElement(ElementName = "TipoNegocioOActividad")]
        public string TipoNegocioOActividad { get; set; }
        [XmlElement(ElementName = "TipoMoneda")]
        public string TipoMoneda { get; set; }
        [XmlElement(ElementName = "ColindanciaNorte")]
        public string ColindanciaNorte { get; set; }
        [XmlElement(ElementName = "ColindanciaSur")]
        public string ColindanciaSur { get; set; }
        [XmlElement(ElementName = "ColindanciaEste")]
        public string ColindanciaEste { get; set; }
        [XmlElement(ElementName = "ColindanciaOeste")]
        public string ColindanciaOeste { get; set; }
        [XmlElement(ElementName = "DistanciaRios")]
        public string DistanciaRios { get; set; }
        [XmlElement(ElementName = "DistanciaMares")]
        public string DistanciaMares { get; set; }
        [XmlElement(ElementName = "DistanciaCanadas")]
        public string DistanciaCanadas { get; set; }
        [XmlElement(ElementName = "ElementoAsegurado")]
        public List<ElementoAsegurado> ElementoAsegurado { get; set; }
        [XmlElement(ElementName = "Construccion")]
        public List<Construccion> Construccion { get; set; }
        [XmlElement(ElementName = "Total")]
        public string Total { get; set; }
        [XmlElement(ElementName = "SeaShips")]
        public List<SeaShips> SeaShips { get; set; }
        [XmlElement(ElementName = "FreightTransportation")]
        public List<FreightTransportation> FreightTransportation { get; set; }
        [XmlElement(ElementName = "Business")]
        public List<Business> Business { get; set; }
        [XmlElement(ElementName = "Format")]
        public string Format { get; set; }
        [XmlElement(ElementName = "Deducible")]
        public decimal Deducible { get; set; }
        [XmlElement(ElementName = "BusinessType")]
        public string BusinessType { get; set; }
        [XmlElement(ElementName = "FixedExpenses")]
        public List<FixedExpenses> FixedExpenses { get; set; }
        //Nuevo 22-05-2017
        [XmlElement(ElementName = "Producto")]
        public string Producto { get; set; }
        //add 26-05-2017
        [XmlElement(ElementName = "CondicionesPago")]
        public CondicionesPago CondicionesPago { get; set; }
        [XmlElement(ElementName = "Location")]
        public string Location { get; set; }
        [XmlElement(ElementName = "Jurisdiction")]
        public string Jurisdiction { get; set; }
        [XmlElement(ElementName = "ValoresAsegurados")]
        public List<ValorAsegurado> ValoresAsegurados { get; set; }
        [XmlElement(ElementName = "Equitment")]
        public List<Equitment> Equitment { get; set; }
        [XmlElement(ElementName = "Bail")]
        public Bail Bail { get; set; }
        [XmlElement(ElementName = "Coverages")]
        public List<Coverages> Coverages { get; set; }
        [XmlElement(ElementName = "CrystalsandSigns")]
        public List<CrystalsandSigns> CrystalsandSigns { get; set; }

        [XmlElement(ElementName = "InsuredName")]
        public string InsuredName { get; set; }

        [XmlElement(ElementName = "QtyDoctors")]
        public int QtyDoctors { get; set; }

        [XmlElement(ElementName = "Jewelry")]
        public string Jewelry { get; set; }

        [XmlElement(ElementName = "Artwork")]
        public string Artwork { get; set; }

        [XmlElement(ElementName = "ValuableObjects")]
        public string ValuableObjects { get; set; }
    }

    [XmlRoot(ElementName = "Equitment")]
    public class Equitment
    {
        [XmlElement(ElementName = "EquitmentType")]
        public string EquitmentType { get; set; }
        [XmlElement(ElementName = "Model")]
        public string Model { get; set; }
        [XmlElement(ElementName = "Make")]
        public string Make { get; set; }
        [XmlElement(ElementName = "Serial")]
        public string Serial { get; set; }
        [XmlElement(ElementName = "Value")]
        public decimal Value { get; set; }
        [XmlElement(ElementName = "InsuredAmount")]
        public decimal InsuredAmount { get; set; }
        [XmlElement(ElementName = "REF")]
        public string REF { get; set; }
        [XmlElement(ElementName = "Chasis")]
        public string Chasis { get; set; }
        [XmlElement(ElementName = "Registro")]
        public string Registro { get; set; }
        [XmlElement(ElementName = "Year")]
        public string Year { get; set; }
        [XmlElement(ElementName = "Category")]
        public string Category { get; set; }
        [XmlElement(ElementName = "Period")]
        public string Period { get; set; }
        [XmlElement(ElementName = "InsuredName")]
        public string InsuredName { get; set; }
        [XmlElement(ElementName = "LastOverHaul")]
        public string LastOverHaul { get; set; }
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "Use")]
        public string Use { get; set; }
        [XmlElement(ElementName = "Enrollment")]
        public string Enrollment { get; set; }
        [XmlElement(ElementName = "BaseAirport")]
        public string BaseAirport { get; set; }
        [XmlElement(ElementName = "Sinister")]
        public string Sinister { get; set; }
        [XmlElement(ElementName = "Exclusions")]
        public string Exclusions { get; set; }
        [XmlElement(ElementName = "Clauses")]
        public string Clauses { get; set; }
        [XmlElement(ElementName = "Manufacturer")]
        public string Manufacturer { get; set; }
        [XmlElement(ElementName = "Capacity")]
        public decimal Capacity { get; set; }
        [XmlElement(ElementName = "Crew")]
        public int Crew { get; set; }
        [XmlElement(ElementName = "Limit")]
        public string Limit { get; set; }

        [XmlElement(ElementName = "Minimo")]
        public string Minimo { get; set; }

        [XmlElement(ElementName = "Deducible")]
        public string Deducible { get; set; }
    }

    [XmlRoot(ElementName = "Bail")]
    public class Bail
    {
        [XmlElement(ElementName = "PaymentSites")]
        public string PaymentSites { get; set; }
        [XmlElement(ElementName = "TravelAgency")]
        public string TravelAgency { get; set; }
        [XmlElement(ElementName = "Agent")]
        public string Agent { get; set; }
        [XmlElement(ElementName = "Enviroment")]
        public string Enviroment { get; set; }
        [XmlElement(ElementName = "Bidding")]
        public string Bidding { get; set; }
        [XmlElement(ElementName = "CashAdvance")]
        public decimal CashAdvance { get; set; }
        [XmlElement(ElementName = "Execution")]
        public string Execution { get; set; }
        [XmlElement(ElementName = "HiddenVice")]
        public string HiddenVice { get; set; }
        [XmlElement(ElementName = "MissingDoc")]
        public string MissingDoc { get; set; }
        [XmlElement(ElementName = "TempAdmission")]
        public string TempAdmission { get; set; }
        [XmlElement(ElementName = "FeesAdnRights")]
        public string FeesAdnRights { get; set; }
        [XmlElement(ElementName = "Lifting")]
        public string Lifting { get; set; }
        [XmlElement(ElementName = "Events")]
        public string Events { get; set; }
        [XmlElement(ElementName = "Others")]
        public string Others { get; set; }
        [XmlElement(ElementName = "Recipient")]
        public string Recipient { get; set; }
        [XmlElement(ElementName = "Amount")]
        public decimal Amount { get; set; }
        [XmlElement(ElementName = "Start")]
        public string Start { get; set; }
        [XmlElement(ElementName = "End")]
        public string End { get; set; }
        [XmlElement(ElementName = "Prime")]
        public decimal Prime { get; set; }
        [XmlElement(ElementName = "Obligations")]
        public string Obligations { get; set; }
        [XmlElement(ElementName = "Liquid")]
        public string Liquid { get; set; }
        [XmlElement(ElementName = "Sponsor")]
        public List<Sponsor> Sponsor { get; set; }
        [XmlElement(ElementName = "FinancialReports")]
        public string FinancialReports { get; set; }
        [XmlElement(ElementName = "Financier")]
        public int Financier { get; set; }


        [XmlElement(ElementName = "GattTax")]
        public string GattTax { get; set; }
        [XmlElement(ElementName = "RestrictedLaborBond")]
        public string RestrictedLaborBond { get; set; }
        [XmlElement(ElementName = "TaxExemption")]
        public string TaxExemption { get; set; }
        [XmlElement(ElementName = "Commercial")]
        public string Commercial { get; set; }
        [XmlElement(ElementName = "Guarantor")]
        public string Guarantor { get; set; }
    }

    [XmlRoot(ElementName = "Sponsor")]
    public class Sponsor
    {
        [XmlElement(ElementName = "Names")]
        public string Names { get; set; }
        [XmlElement(ElementName = "LastName")]
        public string LastName { get; set; }
        [XmlElement(ElementName = "IdRnc")]
        public string IdRnc { get; set; }
        [XmlElement(ElementName = "Ocupation")]
        public string Ocupation { get; set; }
        [XmlElement(ElementName = "Phone")]
        public string Phone { get; set; }
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }
        [XmlElement(ElementName = "Address")]
        public string Address { get; set; }
        [XmlElement(ElementName = "neighborhood")]
        public string neighborhood { get; set; }
        [XmlElement(ElementName = "City")]
        public string City { get; set; }
    }

    [XmlRoot(ElementName = "FreightTransportation")]
    public class FreightTransportation
    {
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "From")]
        public string From { get; set; }
        [XmlElement(ElementName = "To")]
        public string To { get; set; }
        [XmlElement(ElementName = "Daily")]
        public string Daily { get; set; }
        [XmlElement(ElementName = "Weekly")]
        public string Weekly { get; set; }
        [XmlElement(ElementName = "Monthly")]
        public string Monthly { get; set; }
        [XmlElement(ElementName = "OneTripOnly")]
        public string OneTripOnly { get; set; }

        [XmlElement(ElementName = "TypeOfMerchandise")]
        public string TypeOfMerchandise { get; set; }
        [XmlElement(ElementName = "TypeOfShipment")]
        public string TypeOfShipment { get; set; }
        [XmlElement(ElementName = "Departure")]
        public string Departure { get; set; }
        [XmlElement(ElementName = "Arrival")]
        public string Arrival { get; set; }
    }

    [XmlRoot(ElementName = "SeaShips")]
    public class SeaShips
    {
        [XmlElement(ElementName = "NameOfShip")]
        public string NameOfShip { get; set; }
        [XmlElement(ElementName = "Model")]
        public string Model { get; set; }
        [XmlElement(ElementName = "Sleeve")]
        public string Sleeve { get; set; }
        [XmlElement(ElementName = "Use")]
        public string Use { get; set; }
        [XmlElement(ElementName = "SerialNumber")]
        public string SerialNumber { get; set; }
        [XmlElement(ElementName = "Puntual")]
        public string Puntual { get; set; }
        [XmlElement(ElementName = "BasePort")]
        public string BasePort { get; set; }
        [XmlElement(ElementName = "ConstructionMaterials")]
        public string ConstructionMaterials { get; set; }
        [XmlElement(ElementName = "NavigationLimits")]
        public string NavigationLimits { get; set; }
        [XmlElement(ElementName = "Hasta")]
        public string Hasta { get; set; }
        [XmlElement(ElementName = "CrewNo")]
        public string CrewNo { get; set; }
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "Length")]
        public string Length { get; set; }
        [XmlElement(ElementName = "YearOfManufacture")]
        public string YearOfManufacture { get; set; }
        [XmlElement(ElementName = "PassengersNo")]
        public string PassengersNo { get; set; }
        [XmlElement(ElementName = "NumberOfEngines")]
        public string NumberOfEngines { get; set; }
        [XmlElement(ElementName = "EnginesBrand")]
        public string EnginesBrand { get; set; }
        [XmlElement(ElementName = "EnginesModel")]
        public string EnginesModel { get; set; }
        [XmlElement(ElementName = "EnginesSerial")]
        public string EnginesSerial { get; set; }
        [XmlElement(ElementName = "EnginesPower")]
        public string EnginesPower { get; set; }
        [XmlElement(ElementName = "EnginesNavigationLimits")]
        public string EnginesNavigationLimits { get; set; }
        [XmlElement(ElementName = "Fuel")]
        public string Fuel { get; set; }
        [XmlElement(ElementName = "FuelCapacity")]
        public string FuelCapacity { get; set; }
        [XmlElement(ElementName = "EnginesBasePort")]
        public string EnginesBasePort { get; set; }
        [XmlElement(ElementName = "InsuredAmount")]
        public string InsuredAmount { get; set; }
        [XmlElement(ElementName = "Make")]
        public string Make { get; set; }
        [XmlElement(ElementName = "Eslora")]
        public string Eslora { get; set; }
        [XmlElement(ElementName = "Limits")]
        public string Limits { get; set; }
    }

    [XmlRoot(ElementName = "Cliente")]
    public class Cliente
    {
        [XmlElement(ElementName = "FullName")]
        public string FullName { get; set; }
        [XmlElement(ElementName = "IdNumber")]
        public string IdNumber { get; set; }
        [XmlElement(ElementName = "DUI")]
        public string DUI { get; set; }
        [XmlElement(ElementName = "NIT")]
        public string NIT { get; set; }
        [XmlElement(ElementName = "TelephoneNumber")]
        public string TelephoneNumber { get; set; }
        [XmlElement(ElementName = "Direccion")]
        public string Direccion { get; set; }
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }
        [XmlElement(ElementName = "TelResidencia")]
        public string TelResidencia { get; set; }
        [XmlElement(ElementName = "TelOficina")]
        public string TelOficina { get; set; }
        [XmlElement(ElementName = "TelCelular")]
        public string TelCelular { get; set; }

        //Nuevo 22-05-2017
        [XmlElement(ElementName = "ShippingAddress")]
        public string ShippingAddress { get; set; }
    }

    [XmlRoot(ElementName = "PrimeResume")]
    public class PrimeResume
    {
        [XmlElement(ElementName = "TotalAnualPrime")]
        public string TotalAnualPrime { get; set; }
        [XmlElement(ElementName = "Taxes")]
        public string Taxes { get; set; }
        [XmlElement(ElementName = "TotalPayment")]
        public string TotalPayment { get; set; }
        [XmlElement(ElementName = "PendingPrime")]
        public string PendingPrime { get; set; }
        [XmlElement(ElementName = "IssueExpenses")]
        public string IssueExpenses { get; set; }
        [XmlElement(ElementName = "FractionationSurcharge")]
        public string FractionationSurcharge { get; set; }
        [XmlElement(ElementName = "ExemptPrime")]
        public string ExemptPrime { get; set; }
    }

    [XmlRoot(ElementName = "Coverages")]
    public class Coverages
    {
        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "Percentage")]
        public string Percentage { get; set; }
        [XmlElement(ElementName = "Limit")]
        public string Limit { get; set; }

        //add 26-05-2017
        [XmlElement(ElementName = "Coaseguro")]
        public string Coaseguro { get; set; }
        [XmlElement(ElementName = "Deducible")]
        public string Deducible { get; set; }
        [XmlElement(ElementName = "Minimo")]
        public decimal Minimo { get; set; }
        [XmlElement(ElementName = "Maximo")]
        public decimal Maximo { get; set; }

        [XmlElement(ElementName = "Base")]
        public string Base { get; set; }
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }

        /**/
    }

    [XmlRoot(ElementName = "Additionals")]
    public class Additionals
    {
        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "Percentage")]
        public string Percentage { get; set; }
        [XmlElement(ElementName = "Limit")]
        public decimal Limit { get; set; }
    }

    //add 26-05-2017
    [XmlRoot(ElementName = "CondicionesPago")]
    public class CondicionesPago
    {
        [XmlElement(ElementName = "NetPrime")]
        public decimal NetPrime { get; set; }
        [XmlElement(ElementName = "ISC")]
        public decimal ISC { get; set; }
        [XmlElement(ElementName = "AnnualPrime")]
        public decimal AnnualPrime { get; set; }
        [XmlElement(ElementName = "Inicial")]
        public decimal Inicial { get; set; }
        [XmlElement(ElementName = "Cuota1")]
        public decimal Cuota1 { get; set; }
        [XmlElement(ElementName = "Cuota2")]
        public decimal Cuota2 { get; set; }
        [XmlElement(ElementName = "Cuota3")]
        public decimal Cuota3 { get; set; }
        [XmlElement(ElementName = "Cuota4")]
        public decimal Cuota4 { get; set; }
        [XmlElement(ElementName = "TotalPrime")]
        public decimal TotalPrime { get; set; }


    }

    [XmlRoot(ElementName = "Exposures")]
    public class Exposures
    {
        [XmlElement(ElementName = "Descripcion")]
        public string Descripcion { get; set; }
        [XmlElement(ElementName = "Exposure")]
        public string Exposure { get; set; }
        [XmlElement(ElementName = "MPL")]
        public string MPL { get; set; }
        [XmlElement(ElementName = "EML")]
        public string EML { get; set; }
    }

    [XmlRoot(ElementName = "Propiedad")]
    public class Propiedad
    {
        [XmlElement(ElementName = "PropiedadField")]
        public string PropiedadField { get; set; }
        [XmlElement(ElementName = "Tipo")]
        public string Tipo { get; set; }
        [XmlElement(ElementName = "TipoEdificacionOtra")]
        public string TipoEdificacionOtra { get; set; }
        [XmlElement(ElementName = "TipoEdificioOtro")]
        public string TipoEdificioOtro { get; set; }
        [XmlElement(ElementName = "DescripcionRiesgo")]
        public string DescripcionRiesgo { get; set; }
        [XmlElement(ElementName = "Direccion")]
        public string Direccion { get; set; }
        [XmlElement(ElementName = "EdadConstrucccion")]
        public int EdadConstrucccion { get; set; }
        [XmlElement(ElementName = "MovimientoComercial")]
        public string MovimientoComercial { get; set; }
        [XmlElement(ElementName = "OrganizacionContable")]
        public string OrganizacionContable { get; set; }
        [XmlElement(ElementName = "NoEmpleados")]
        public int NoEmpleados { get; set; }
        [XmlElement(ElementName = "Horario")]
        public string Horario { get; set; }
        [XmlElement(ElementName = "FormaDeOcupacion")]
        public string FormaDeOcupacion { get; set; }
        [XmlElement(ElementName = "AseguradoraAnterior")]
        public string AseguradoraAnterior { get; set; }
        [XmlElement(ElementName = "TipoEdificio")]
        public string TipoEdificio { get; set; }
        [XmlElement(ElementName = "TipoEdificacion")]
        public string TipoEdificacion { get; set; }
        [XmlElement(ElementName = "FechaDeConstruccion")]
        public DateTime FechaDeConstruccion { get; set; }
        [XmlElement(ElementName = "CantidadDeNiveles")]
        public int CantidadDeNiveles { get; set; }
        [XmlElement(ElementName = "NiveldeSiniestroEnZona")]
        public string NiveldeSiniestroEnZona { get; set; }
        [XmlElement(ElementName = "SiniestralidadDesc")]
        public string SiniestralidadDesc { get; set; }
        [XmlElement(ElementName = "Street")]
        public string Street { get; set; }
        [XmlElement(ElementName = "Sector")]
        public string Sector { get; set; }
        [XmlElement(ElementName = "Municipio")]
        public string Municipio { get; set; }
        [XmlElement(ElementName = "ElementName")]
        public string Departamento { get; set; }
        [XmlElement(ElementName = "Longitud")]
        public decimal Longitud { get; set; }
        [XmlElement(ElementName = "Latitud")]
        public decimal Latitud { get; set; }
        [XmlElement(ElementName = "Texto")]
        public string Texto { get; set; }
        [XmlElement(ElementName = "Categoria")]
        public string Categoria { get; set; }
        [XmlElement(ElementName = "OpinionRiesgo")]
        public string OpinionRiesgo { get; set; }
        [XmlElement(ElementName = "RecomendacionesTecnicas")]
        public string RecomendacionesTecnicas { get; set; }
        [XmlElement(ElementName = "RecomendacionesHechas")]
        public string RecomendacionesHechas { get; set; }
        [XmlElement(ElementName = "Descripcion")]
        public List<Descripcion> Descripcion { get; set; }
        [XmlElement(ElementName = "Inspeccion")]
        public List<Inspeccion> Inspeccion { get; set; }
        [XmlElement(ElementName = "Nivel")]
        public List<Nivel> Nivel { get; set; }
        [XmlElement(ElementName = "Perdida")]
        public List<Perdida> Perdida { get; set; }
        [XmlElement(ElementName = "Peligro")]
        public List<Peligro> Peligro { get; set; }
        [XmlElement(ElementName = "Proteccion")]
        public List<Proteccion> Proteccion { get; set; }
        [XmlElement(ElementName = "Exposures")]
        public List<Exposures> Exposures { get; set; }
    }

    [XmlRoot(ElementName = "Descripcion")]
    public class Descripcion
    {
        [XmlElement(ElementName = "Detalle")]
        public string Detalle { get; set; }
        [XmlElement(ElementName = "Tipo")]
        public string Tipo { get; set; }
    }

    [XmlRoot(ElementName = "Inspeccion")]
    public class Inspeccion
    {
        [XmlElement(ElementName = "Inspector")]
        public string Inspector { get; set; }
        [XmlElement(ElementName = "FechaInspeccion")]
        public DateTime FechaInspeccion { get; set; }
        [XmlElement(ElementName = "Entrevistador")]
        public string Entrevistador { get; set; }
    }

    [XmlRoot(ElementName = "Nivel")]
    public class Nivel
    {
        [XmlElement(ElementName = "AreaPorPiso")]
        long AreaPorPiso { get; set; }
        [XmlElement(ElementName = "CantidadLocales")]
        int CantidadLocales { get; set; }
        [XmlElement(ElementName = "AreaPorAptOficina")]
        long AreaPorAptOficina { get; set; }
    }

    [XmlRoot(ElementName = "Perdida")]
    public class Perdida
    {
        [XmlElement(ElementName = "Tiene")]
        public string Tiene { get; set; }
        [XmlElement(ElementName = "Nivel")]
        public string Nivel { get; set; }
        [XmlElement(ElementName = "Tipo")]
        public string Tipo { get; set; }
        [XmlElement(ElementName = "Otros")]
        public string Otros { get; set; }
    }

    [XmlRoot(ElementName = "Peligro")]
    public class Peligro
    {
        [XmlElement(ElementName = "Descripcion")]
        public string Descripcion { get; set; }
        [XmlElement(ElementName = "Valor")]
        public long Valor { get; set; }
        [XmlElement(ElementName = "Comentario")]
        public string Comentario { get; set; }
    }

    [XmlRoot(ElementName = "Proteccion")]
    public class Proteccion
    {
        [XmlElement(ElementName = "Descripcion")]
        public string Descripcion { get; set; }
        [XmlElement(ElementName = "Valor")]
        public long Valor { get; set; }
        [XmlElement(ElementName = "Horario")]
        public string Horario { get; set; }
        [XmlElement(ElementName = "Cantidad")]
        public long Cantidad { get; set; }
    }
    /**/
    [XmlRoot(ElementName = "ValorAsegurado")]
    public class ValorAsegurado
    {
        [XmlElement(ElementName = "Descripcion")]
        public string Descripcion { get; set; }
        [XmlElement(ElementName = "Valor")]
        public decimal Valor { get; set; }
    }

    [XmlRoot(ElementName = "dataset")]
    public class Dataset
    {
        [XmlElement(ElementName = "Transaction")]
        public Transaction Transaction { get; set; }
        [XmlElement(ElementName = "PolicyInfo")]
        public PolicyInfo PolicyInfo { get; set; }
        [XmlElement(ElementName = "PaymentDetail")]
        public PaymentDetail PaymentDetail { get; set; }
        [XmlElement(ElementName = "Reclamo")]
        public Reclamo Reclamo { get; set; }
        [XmlElement(ElementName = "CotizacionFire")]
        public List<CotizacionFire> CotizacionFire { get; set; }
        [XmlElement(ElementName = "Cliente")]
        public Cliente Cliente { get; set; }
        [XmlElement(ElementName = "PrimeResume")]
        public PrimeResume PrimeResume { get; set; }
        [XmlElement(ElementName = "Coverages")]
        public List<Coverages> Coverages { get; set; }
        [XmlElement(ElementName = "Additionals")]
        public List<Additionals> Additionals { get; set; }

        //add 26-05-2017
        [XmlElement(ElementName = "Propiedad")]
        public List<Propiedad> Propiedad { get; set; }

    }
}
