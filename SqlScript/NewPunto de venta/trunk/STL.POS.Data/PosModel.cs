namespace STL.POS.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Data;
    using Frontend.Web.Reports;
    using STL.POS.Data.POSEntities.Datasets.QuotationPrevTableAdapters;
    using STL.POS.Data.POSEntities.Datasets.QuotationPrevSaludTableAdapters;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Diagnostics;
    using System.Data.Entity.Infrastructure;

    public partial class PosModel : DbContext
    {
        public PosModel()
            : base("PosModel")
        {
            //var adapter = (IObjectContextAdapter)this;
            //var objectContext = adapter.ObjectContext;
            //objectContext.CommandTimeout = (5 * 60); // value in seconds
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PosModel"].ConnectionString;
        string connectionStringGlobal = System.Configuration.ConfigurationManager.ConnectionStrings["GlobalLogger"].ConnectionString;

        public virtual DbSet<ST_GLOBAL_CITY> ST_GLOBAL_CITY { get; set; }
        public virtual DbSet<ST_GLOBAL_COUNTRY> ST_GLOBAL_COUNTRY { get; set; }
        public virtual DbSet<ST_GLOBAL_STATE_PROVINCE> ST_GLOBAL_STATE_PROVINCE { set; get; }
        public virtual DbSet<ST_VEHICLE_MAKE> ST_VEHICLE_MAKE { get; set; }
        public virtual DbSet<ST_VEHICLE_MODEL> ST_VEHICLE_MODEL { get; set; }
        public virtual DbSet<ST_VEHICLE_TYPE> ST_VEHICLE_TYPE { get; set; }
        public virtual DbSet<Core_Integration> Core_Integration { get; set; }
        public virtual DbSet<VW_ST_VEHICLE_MAKE> VW_ST_VEHICLE_MAKE { get; set; }
        public virtual DbSet<VW_ST_VEHICLE_TYPE> VW_ST_VEHICLE_TYPE { get; set; }

        public virtual DbSet<ST_SURCHARGE_PERCENTAGE> ST_SURCHARGE_PERCENTAGE { get; set; }

        public virtual DbSet<Quotation> Quotations { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<PersonHealth> Persons { get; set; }
        public virtual DbSet<VehicleProduct> VehicleProducts { get; set; }
        public virtual DbSet<CoverageDetail> CoverageDetails { get; set; }
        public virtual DbSet<ProductLimit> ProductLimits { get; set; }
        public virtual DbSet<TermType> TermTypes { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<PosLogEntry> LogEntries { get; set; }
        public virtual DbSet<BusinessLine> BusinessLines { get; set; }
        public virtual DbSet<ProductTypeFamilyBrochure> ProductTypeFamilyBrochures { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<ServiceType> ServicesTypes { get; set; }
        public virtual DbSet<VirtualOfficeIntegration> VirtualOfficeIntegrations { get; set; }
        public virtual DbSet<STUSAGE> STUSAGE { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }

        //AGREGAR TABLAS NUEVAS
        public virtual DbSet<IDENTIFICATION_FINAL_BENEFICIARY_OPTIONS> IdentificationFinalBeneficiaryOption { get; set; }
        public virtual DbSet<OWNERSHIP_STRUCTURE> OwnerShipStructures { get; set; }
        public virtual DbSet<PEP_FORMULARY_OPTIONS> PepFormularyOption { get; set; }
        public virtual DbSet<SOCIAL_REASON> SocialReasons { get; set; }

        public virtual DbSet<EN_RELATIONSHIP> Relationships { get; set; }
        public virtual DbSet<ST_GLOBAL_MUNICIPIO> ST_GLOBAL_MUNICIPIO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("POS");

            #region [Legacy]

            modelBuilder.Entity<ST_GLOBAL_CITY>()
                .Property(e => e.City_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<ST_GLOBAL_CITY>()
                .Property(e => e.City_Abbrv_Name)
                .IsUnicode(false);

            modelBuilder.Entity<ST_GLOBAL_CITY>()
                .Property(e => e.hostname)
                .IsUnicode(false);

            modelBuilder.Entity<ST_GLOBAL_CITY>()
                .Ignore(e => e.Country);

            modelBuilder.Entity<ST_GLOBAL_COUNTRY>()
                .Property(e => e.Global_Country_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<ST_GLOBAL_COUNTRY>()
                .Property(e => e.Global_Country_Desc_EN)
                .IsUnicode(false);

            modelBuilder.Entity<ST_GLOBAL_COUNTRY>()
                .Property(e => e.Global_Time_Zone_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<ST_GLOBAL_COUNTRY>()
                .Property(e => e.Global_Letter_Code_2)
                .IsUnicode(false);

            modelBuilder.Entity<ST_GLOBAL_COUNTRY>()
                .Property(e => e.Global_Letter_Code_3)
                .IsUnicode(false);

            modelBuilder.Entity<ST_GLOBAL_COUNTRY>()
                .Property(e => e.Citizenship)
                .IsUnicode(false);

            modelBuilder.Entity<ST_GLOBAL_COUNTRY>()
                .Property(e => e.hostname)
                .IsUnicode(false);

            modelBuilder.Entity<ST_GLOBAL_COUNTRY>()
                .HasMany<BusinessLine>(p => p.BusinessLines)
                .WithMany();

            modelBuilder.Entity<ST_VEHICLE_MAKE>()
                .Property(e => e.Make_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<ST_VEHICLE_MAKE>()
                .Property(e => e.Hostname)
                .IsUnicode(false);

            modelBuilder.Entity<ST_VEHICLE_MAKE>()
                .HasMany<ST_VEHICLE_MODEL>(e => e.ST_VEHICLE_MODEL)
                .WithRequired(s => s.Make)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ST_VEHICLE_MODEL>()
                .Property(e => e.Model_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<ST_VEHICLE_MODEL>()
                .Property(e => e.Hostname)
                .IsUnicode(false);

            modelBuilder.Entity<ST_VEHICLE_TYPE>()
                .Property(e => e.Vehicle_Type_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<ST_VEHICLE_TYPE>()
                .Property(e => e.Hostname)
                .IsUnicode(false);

            modelBuilder.Entity<ST_VEHICLE_TYPE>()
                .Property(e => e.namekey)
                .IsUnicode(false);

            modelBuilder.Entity<ST_VEHICLE_TYPE>()
                .HasMany(e => e.ST_VEHICLE_MODEL)
                .WithRequired()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Core_Integration>()
                .Property(e => e.Table_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Integration>()
                .Property(e => e.Table_Name_Core)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Integration>()
                .Property(e => e.HostName)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ST_VEHICLE_MAKE>()
                .Property(e => e.Make_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ST_VEHICLE_MAKE>()
                .Property(e => e.Hostname)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ST_VEHICLE_TYPE>()
                .Property(e => e.Vehicle_Type_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ST_VEHICLE_TYPE>()
                .Property(e => e.Hostname)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ST_VEHICLE_TYPE>()
                .Property(e => e.namekey)
                .IsUnicode(false);

            #endregion

            #region [Parameter]

            modelBuilder.Entity<Parameter>()
                .Property(p => p.Name)
                .HasColumnType("nvarchar")
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_parameter_name")))
                .HasMaxLength(50);

            modelBuilder.Entity<Parameter>()
                .Property(p => p.FriendlyName)
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            modelBuilder.Entity<Parameter>()
                .Property(p => p.Value)
                .HasColumnType("nvarchar")
                .HasMaxLength(2000);

            modelBuilder.Entity<Parameter>()
                .Property(p => p.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(4000);

            #endregion

            #region [Quotation]

            modelBuilder.Entity<Quotation>()
                .Property(q => q.PolicyNumber)
                .HasMaxLength(20);

            modelBuilder.Entity<Quotation>()
                .Property(q => q.QuotationNumber)
                .HasMaxLength(20);

            modelBuilder.Entity<Quotation>()
                .Ignore(q => q.AmountPaid);

            modelBuilder.Entity<Quotation>()
                .Property(q => q.TotalISC)
                .HasColumnType("money");

            modelBuilder.Entity<Quotation>()
                .Property(q => q.TotalPrime)
                .HasColumnType("money");

            modelBuilder.Entity<Quotation>()
                .Property(q => q.TotalDiscount)
                .HasColumnType("money");

            modelBuilder.Entity<Quotation>()
                .Property(q => q.PaymentFrequency)
                .HasMaxLength(20);

            modelBuilder.Entity<Quotation>()
                .Property(q => q.CardnetAuthorizationCode)
                .HasMaxLength(10);

            modelBuilder.Entity<Quotation>()
                .Property(q => q.CardnetLastResponseCode)
                .HasMaxLength(10);

            modelBuilder.Entity<Quotation>()
                .Property(q => q.CardnetLastResponseMessage)
                .HasMaxLength(100);

            modelBuilder.Entity<Quotation>()
                .HasOptional<TermType>(v => v.TermType)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Quotation>()
                .Property(p => p.AchName)
                .HasMaxLength(50);

            modelBuilder.Entity<Quotation>()
                .Property(p => p.AchNumber)
                .HasMaxLength(50);

            modelBuilder.Entity<Quotation>()
                .Property(p => p.AchAccountHolderGovId)
                .HasMaxLength(50);

            modelBuilder.Entity<Quotation>()
                .Property(p => p.AchBankRoutingNumber)
                .HasMaxLength(50);

            modelBuilder.Entity<Quotation>()
                .Property(p => p.PrincipalFullName)
                .HasMaxLength(200);

            modelBuilder.Entity<Quotation>()
                .Property(p => p.CurrencySymbol)
                .HasMaxLength(10);

            modelBuilder.Entity<Quotation>()
                .HasRequired<Currency>(q => q.Currency)
                .WithMany();

            #endregion

            #region [QuotationAuto]

            modelBuilder.Entity<QuotationAuto>()
                .ToTable("QUOTATION_AUTO");

            modelBuilder.Entity<QuotationAuto>()
                .HasMany<VehicleProduct>(e => e.VehicleProducts)
                .WithRequired()
                .Map(mc => mc.MapKey("Quotation_Id"))
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<QuotationAuto>()
                .HasMany<Driver>(e => e.Drivers)
                .WithRequired()
                .Map(mc => mc.MapKey("QuotationId"))
                .WillCascadeOnDelete(true);

            #endregion

            #region [QuotationSalud]

            modelBuilder.Entity<QuotationSalud>()
                .ToTable("QUOTATION_SALUD");

            modelBuilder.Entity<QuotationSalud>()
                .HasMany<PersonHealth>(e => e.Persons)
                .WithRequired()
                .Map(qs => qs.MapKey("QuotationId"))
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<QuotationSalud>()
               .Property(qs => qs.Deductible)
                .HasColumnType("money");

            modelBuilder.Entity<QuotationSalud>()
                .Property(qs => qs.Plan)
                .HasMaxLength(50);

            modelBuilder.Entity<QuotationSalud>()
                .Property(qs => qs.PlanType)
                .HasMaxLength(50);

            modelBuilder.Entity<QuotationSalud>()
               .HasMany<PersonHealth>(e => e.Persons)
               .WithRequired()
               .Map(mc => mc.MapKey("QuotationId"))
               .WillCascadeOnDelete(true);

            #endregion

            #region [Person]

            modelBuilder.Entity<Person>()
                .Property(e => e.Email)
                .HasMaxLength(255);

            modelBuilder.Entity<Person>()
               .Property(e => e.FirstName)
               .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(e => e.SecondName)
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(e => e.FirstSurname)
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(e => e.SecondSurname)
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(e => e.Address)
                .IsOptional()
                .HasMaxLength(1000);

            modelBuilder.Entity<Person>()
                .Property(e => e.Company)
                .IsOptional()
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(e => e.Job)
                .IsOptional()
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
              .Property(e => e.MaritalStatus)
              .HasMaxLength(10);

            modelBuilder.Entity<Person>()
               .Property(e => e.Mobile)
               .IsOptional()
               .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(e => e.PhoneNumber)
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(e => e.WorkPhone)
                .IsOptional()
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(e => e.Sex)
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .HasRequired<ST_GLOBAL_CITY>(c => c.City)
                .WithMany()
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Person>()
            //    .HasRequired<ST_GLOBAL_MUNICIPIO>(c => c.Municipio)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);


            modelBuilder.Entity<Person>()
                .HasOptional<SOCIAL_REASON>(e => e.SOCIALREASON)
                .WithMany()
                .Map(mc => mc.MapKey("SocialReasonId"))
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
               .HasOptional<IDENTIFICATION_FINAL_BENEFICIARY_OPTIONS>(z => z.IDENTIFICATIONFINALBENEFICIARYOPTIONS)
                .WithMany()
                .Map(mc => mc.MapKey("IdentificationFinalBeneficiaryOptionsId"))
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Person>()
               .HasOptional<OWNERSHIP_STRUCTURE>(q => q.OWNERSHIPSTRUCTURE)
                .WithMany()
                .Map(mc => mc.MapKey("OwnershipStructureId"))
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Person>()
               .HasOptional<PEP_FORMULARY_OPTIONS>(w => w.PEPFORMULARYOPTIONS)
                .WithMany()
                .Map(mc => mc.MapKey("PepFormularyOptionsId"))
                .WillCascadeOnDelete(false);

            #endregion

            #region [PersonHealth]

            modelBuilder.Entity<PersonHealth>()
                .Property(e => e.Relationship)
                .HasMaxLength(50);

            modelBuilder.Entity<PersonHealth>()
                .Property(e => e.Email2)
                .HasMaxLength(255);

            modelBuilder.Entity<PersonHealth>()
                .Property(e => e.Email3)
                .HasMaxLength(255);

            modelBuilder.Entity<PersonHealth>()
                .Property(e => e.Income)
                .HasMaxLength(50);

            modelBuilder.Entity<PersonHealth>()
               .Property(v => v.Height)
                .HasColumnType("decimal")
                .HasPrecision(5, 2);

            modelBuilder.Entity<PersonHealth>()
               .Property(v => v.Weight)
                .HasColumnType("decimal")
                .HasPrecision(5, 2);

            modelBuilder.Entity<PersonHealth>()
                    .Property(e => e.WorkAddress)
                    .IsOptional()
                    .HasMaxLength(50);

            modelBuilder.Entity<PersonHealth>()
                    .Property(e => e.PartnerName)
                    .IsOptional()
                    .HasMaxLength(10);

            modelBuilder.Entity<PersonHealth>()
                .HasOptional<ST_GLOBAL_CITY>(c => c.WorkCity)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PersonHealth>()
               .Property(v => v.Prime)
                .HasColumnType("money");

            #endregion

            #region [Driver]

            modelBuilder.Entity<Driver>()
                .Property(e => e.AccidentsLast3Years)
                .IsOptional()
                .HasMaxLength(5);

            #endregion

            #region [VehicleProduct]

            modelBuilder.Entity<VehicleProduct>()
             .Property(p => p.VehicleDescription)
             .HasColumnType("nvarchar")
             .HasMaxLength(500);

            modelBuilder.Entity<VehicleProduct>()
                .Property(p => p.Chassis)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            modelBuilder.Entity<VehicleProduct>()
                .Property(p => p.Plate)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            modelBuilder.Entity<VehicleProduct>()
                .Property(p => p.Color)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            modelBuilder.Entity<VehicleProduct>()
                 .Property(p => p.StoreName)
                 .HasColumnType("nvarchar")
                 .HasMaxLength(50);

            modelBuilder.Entity<VehicleProduct>()
                 .Property(p => p.UsageName)
                 .HasColumnType("nvarchar")
                 .HasMaxLength(50);

            modelBuilder.Entity<VehicleProduct>()
                 .Property(p => p.VehicleTypeName)
                 .HasColumnType("nvarchar")
                 .HasMaxLength(60);

            modelBuilder.Entity<VehicleProduct>()
                 .Property(p => p.VehicleMakeName)
                 .HasColumnType("nvarchar")
                 .HasMaxLength(60);

            modelBuilder.Entity<VehicleProduct>()
               .Property(v => v.VehiclePrice)
                .HasColumnType("money");

            modelBuilder.Entity<VehicleProduct>()
              .Property(v => v.InsuredAmount)
               .HasColumnType("money");

            modelBuilder.Entity<VehicleProduct>()
               .Property(v => v.PercentageToInsure)
                .HasColumnType("decimal")
                .HasPrecision(5, 2);

            modelBuilder.Entity<VehicleProduct>()
                .HasRequired<Driver>(s => s.Driver)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VehicleProduct>()
                .Property(p => p.SelectedVehicleTypeName)
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            modelBuilder.Entity<VehicleProduct>()
                .Property(p => p.VehicleYearOld)
                .HasColumnType("nvarchar")
                .HasMaxLength(20);

            modelBuilder.Entity<VehicleProduct>()
               .Property(p => p.SelectedProductName)
               .HasColumnType("nvarchar")
               .HasMaxLength(100);

            modelBuilder.Entity<VehicleProduct>()
               .Property(p => p.SelectedCoverageName)
               .HasColumnType("nvarchar")
               .HasMaxLength(100);

            modelBuilder.Entity<VehicleProduct>()
                .Property(p => p.TotalDiscount)
                .HasColumnType("money");

            modelBuilder.Entity<VehicleProduct>()
                .Property(p => p.TotalIsc)
                .HasColumnType("money");

            modelBuilder.Entity<VehicleProduct>()
              .Property(v => v.SurChargePercentage)
              .HasColumnType("decimal")
              .HasPrecision(18, 2);

            modelBuilder.Entity<VehicleProduct>()
               .Property(p => p.NumeroFormulario)
               .HasColumnType("varchar")
               .HasMaxLength(50);

            modelBuilder.Entity<VehicleProduct>()
               .Property(p => p.RateJson)
               .HasColumnType("varchar")
               .HasMaxLength(null);

            modelBuilder.Entity<VehicleProduct>()
                .HasRequired<ST_VEHICLE_MODEL>(v => v.VehicleModel)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VehicleProduct>()
                .HasMany<ProductLimit>(p => p.ProductLimits)
                .WithRequired()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<VehicleProduct>()
                .Ignore(vp => vp.VehicleTypes);

            modelBuilder.Entity<VehicleProduct>()
                .Ignore(vp => vp.Percentages);

            modelBuilder.Entity<VehicleProduct>()
                .Ignore(vp => vp.Deductibles);

            #endregion

            #region [ProductLimits]

            modelBuilder.Entity<ProductLimit>()
                .Property(e => e.SdPrime)
                .HasColumnType("money");

            modelBuilder.Entity<ProductLimit>()
                .Property(e => e.TpPrime)
                .HasColumnType("money");

            modelBuilder.Entity<ProductLimit>()
                .Property(e => e.ServicesPrime)
                .HasColumnType("money");

            modelBuilder.Entity<ProductLimit>()
                .Property(p => p.TotalDiscount)
                .HasColumnType("money");

            modelBuilder.Entity<ProductLimit>()
                .Property(p => p.TotalIsc)
                .HasColumnType("money");

            modelBuilder.Entity<ProductLimit>()
                .Property(p => p.TotalPrime)
                .HasColumnType("money");

            modelBuilder.Entity<ProductLimit>()
                .HasMany<CoverageDetail>(a => a.SelfDamagesCoverages)
                .WithOptional()
                .Map(c => c.MapKey("SelfDamagesToProductLimits"))
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductLimit>()
                .HasMany<CoverageDetail>(a => a.ThirdPartyCoverages)
                .WithOptional()
                .Map(c => c.MapKey("ThirdPartyToProductLimits"))
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductLimit>()
                .HasMany<ServiceType>(a => a.ServicesCoverages)
                .WithOptional()
                .Map(c => c.MapKey("ServicesTypesToProductLimits"))
                .WillCascadeOnDelete(false);

            #endregion

            #region [TermTypes]

            modelBuilder.Entity<TermType>()
                .Property(p => p.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<TermType>()
                .Property(p => p.TimeSpanInLetters)
                .HasMaxLength(50);

            #endregion

            #region [User]

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Surname)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Telephone)
                .HasMaxLength(20);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.Salt)
                .HasMaxLength(500);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordEncoded)
                .HasMaxLength(500);

            modelBuilder.Entity<User>()
                .Property(u => u.ChangePasswordToken)
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .HasMany<Quotation>(u => u.Quotations)
                .WithRequired(q => q.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
              .HasMany(u => u.Agents)
              .WithOptional(u => u.Suscriptor)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.AgentId)
                .IsOptional();

            #endregion

            #region [LogEntry]

            modelBuilder.Entity<PosLogEntry>()
                .HasKey(p => p.Id);

            #endregion

            #region [BusinessLine]

            modelBuilder.Entity<BusinessLine>()
                .Property(p => p.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<BusinessLine>()
                .Property(p => p.Path)
                .HasMaxLength(200);

            #endregion

            #region [ProductTypeBrochure]

            modelBuilder.Entity<ProductTypeBrochure>()
                .Property(p => p.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<ProductTypeBrochure>()
                .Property(p => p.Coberturas)
                .HasMaxLength(50);

            #endregion

            #region [ProductTypeFamilyBrochure]

            modelBuilder.Entity<ProductTypeFamilyBrochure>()
                .Property(p => p.Name)
                .HasMaxLength(50);

            #endregion

            #region [CoverageTypeBrochure]

            modelBuilder.Entity<CoverageTypeBrochure>()
                .Property(p => p.Name)
                .HasMaxLength(50);

            #endregion

            #region [CoverageBrochure]

            modelBuilder.Entity<CoverageBrochure>()
                .Property(p => p.Name)
                .HasMaxLength(50);

            #endregion

            #region [CoverageDetailBrochure]

            modelBuilder.Entity<CoverageDetailBrochure>()
                .Property(p => p.Name)
                .HasMaxLength(150);

            modelBuilder.Entity<CoverageDetailBrochure>()
                .Property(p => p.Value)
                .HasMaxLength(50);

            #endregion

            #region [BenefitBrochure]

            modelBuilder.Entity<BenefitBrochure>()
                .Property(p => p.Name)
                .HasMaxLength(50);

            #endregion

            #region [Currency]

            modelBuilder.Entity<Currency>()
                .Property(p => p.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Currency>()
                .Property(p => p.Symbol)
                .HasMaxLength(5);

            modelBuilder.Entity<Currency>()
                .Property(p => p.CardnetCode)
                .HasMaxLength(5);

            modelBuilder.Entity<Currency>()
                .Property(p => p.IsoCode)
                .HasMaxLength(5);

            #endregion

            #region [ChangeRate]

            modelBuilder.Entity<ChangeRate>()
                .Property(p => p.Rate)
                .HasColumnType("money");

            #endregion

            #region [ServiceType]

            modelBuilder.Entity<ServiceType>()
              .HasMany<CoverageDetail>(a => a.Coverages)
              .WithOptional()
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<ServiceType>()
                .Property(s => s.Name)
                .HasMaxLength(50);

            #endregion

            #region [ST_SURCHARGE_PERCENTAGE]
            modelBuilder.Entity<ST_SURCHARGE_PERCENTAGE>()
            .Property(p => p.Percentage_Desc)
            .HasColumnType("varchar")
            .HasMaxLength(60);

            modelBuilder.Entity<ST_SURCHARGE_PERCENTAGE>()
                .Property(p => p.Percentage)
                .HasColumnType("money");

            modelBuilder.Entity<ST_SURCHARGE_PERCENTAGE>()
            .Property(p => p.SurCharge_Status)
            .HasColumnType("bit");

            modelBuilder.Entity<ST_SURCHARGE_PERCENTAGE>()
                .Property(p => p.Pos_Flag)
                .HasColumnType("bit");

            modelBuilder.Entity<ST_SURCHARGE_PERCENTAGE>()
                .Property(p => p.Hostname)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            #endregion

            #region [STUSAGE]

            modelBuilder.Entity<STUSAGE>()
            .Property(p => p.Usage_Desc)
            .HasColumnType("varchar")
            .HasMaxLength(60);

            modelBuilder.Entity<STUSAGE>()
           .Property(p => p.Usage_Message)
           .HasColumnType("varchar");

            modelBuilder.Entity<STUSAGE>()
          .Property(p => p.Name_Key)
          .HasColumnType("varchar")
          .HasMaxLength(60);

            modelBuilder.Entity<STUSAGE>()
                .Property(p => p.Usage_Status)
                .HasColumnType("bit");

            modelBuilder.Entity<STUSAGE>()
                .Property(p => p.Hostname)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            #endregion

            #region [Colors]

            modelBuilder.Entity<Colors>()
            .Property(p => p.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(50);

            #endregion

            #region [Jobs]

            modelBuilder.Entity<Jobs>()
            .Property(p => p.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(100);

            #endregion

            #region [VirtualOfficeIntegration]

            modelBuilder.Entity<VirtualOfficeIntegration>()
                .Property(p => p.ElementTypeName)
                .HasMaxLength(50);

            modelBuilder.Entity<VirtualOfficeIntegration>()
               .Property(p => p.PosId)
               .HasMaxLength(50);

            modelBuilder.Entity<VirtualOfficeIntegration>()
             .Property(p => p.ElementName)
             .HasMaxLength(500);


            #endregion

            #region [SocialReason]
            modelBuilder.Entity<SOCIAL_REASON>()
            .Property(p => p.Description)
            .HasColumnType("varchar")
            .HasMaxLength(100);
            #endregion


            #region [IDENTIFICATION_FINAL_BENEFICIARY_OPTIONS]
            modelBuilder.Entity<IDENTIFICATION_FINAL_BENEFICIARY_OPTIONS>()
            .Property(p => p.Description)
            .HasColumnType("varchar")
            .HasMaxLength(100);
            #endregion

            #region [OWNERSHIP_STRUCTURE]
            modelBuilder.Entity<OWNERSHIP_STRUCTURE>()
            .Property(p => p.Description)
            .HasColumnType("varchar")
            .HasMaxLength(100);
            #endregion


            #region [PEP_FORMULARY_OPTIONS]
            modelBuilder.Entity<PEP_FORMULARY_OPTIONS>()
            .Property(p => p.Description)
            .HasColumnType("varchar")
            .HasMaxLength(100);
            #endregion

            //#region MUNICIPIO
            //modelBuilder.Entity<ST_GLOBAL_MUNICIPIO>()
            //    .Property(p => p.Municipio_Desc)
            //    .HasColumnType("varchar");

            //modelBuilder.Entity<ST_GLOBAL_MUNICIPIO>()
            //    .Property(p => p.Municipio_Id);
            //#endregion
        }

        #region [Parameter Helper]

        public Parameter GetParameter(string keyName, string defaulValue = null, bool saveChanges = true)
        {
            var paramFound = Parameters.Where(p => p.Name == keyName).FirstOrDefault();
            if (paramFound == null && defaulValue != null)
            {
                paramFound = new Parameter();
                paramFound.Name = keyName;
                paramFound.Value = defaulValue;
                Parameters.Add(paramFound);
                if (saveChanges)
                    this.SaveChanges();
            }
            return paramFound;
        }

        #endregion

        #region SP's
        public System.Collections.Generic.List<IdentificationFinalBeneficiary> GetAllBeneficiaryByDriver(int PersonID)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            System.Collections.Generic.List<IdentificationFinalBeneficiary> result = new System.Collections.Generic.List<IdentificationFinalBeneficiary>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "EXEC POS.SP_GET_ALL_IDENTIFICATION_FINAL_BENEFICIARY_BY_DRIVER @PersonID";
                command = new SqlCommand(query, connection);
                dT = new DataTable();

                command.Parameters.Add(new SqlParameter("@PersonID", PersonID));

                try
                {
                    dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dT);

                    if (dT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dT.Rows)
                        {
                            result.Add(new IdentificationFinalBeneficiary()
                            {
                                Id = CSEntities.ExtensionMethods.toInt(dr["Id"].ToString()),
                                PersonBenefiId = CSEntities.ExtensionMethods.toInt(dr["PersonsID"].ToString()),
                                Name = dr["Name"].ToString(),
                                PercentageParticipation = CSEntities.ExtensionMethods.toDecimal(dr["PercentageParticipation"].ToString())
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }

            return
                result;
        }

        public void UpdateDeleteCreateBeneficiaryByDriver(int Id, int PersonID, string Name,
            decimal PercentageParticipation, DateTime Create_Date, int Create_UserId, int? Modi_UserId, DateTime? Modi_Date, string Action)
        {
            string query;
            SqlCommand command;

            /*
             Leyenda
                Action
             *      I = Insert
             *      U = Update
             *      D = Delete
             */

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "[POS].[SP_SET_IDENTIFICATION_FINAL_BENEFICIARY_BY_DRIVER]";
                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", Id));
                command.Parameters.Add(new SqlParameter("@PersonID", PersonID));
                command.Parameters.Add(new SqlParameter("@Name", Name));
                command.Parameters.Add(new SqlParameter("@PercentageParticipation", PercentageParticipation));
                command.Parameters.Add(new SqlParameter("@Create_Date", Create_Date));
                command.Parameters.Add(new SqlParameter("@Create_UserId", Create_UserId));
                command.Parameters.Add(new SqlParameter("@Modi_UserId", Modi_UserId));
                command.Parameters.Add(new SqlParameter("@Modi_Date", Modi_Date));
                command.Parameters.Add(new SqlParameter("@Action", Action));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }
        }

        public System.Collections.Generic.List<PepFormulary> GetAllPEPSByDriver(int PersonID)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            System.Collections.Generic.List<PepFormulary> result = new System.Collections.Generic.List<PepFormulary>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "EXEC [POS].[SP_GET_ALL_PEPS_BY_DRIVER] @PersonID";
                command = new SqlCommand(query, connection);
                dT = new DataTable();

                command.Parameters.Add(new SqlParameter("@PersonID", PersonID));

                try
                {
                    dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dT);

                    if (dT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dT.Rows)
                        {
                            result.Add(new PepFormulary()
                            {
                                Id = CSEntities.ExtensionMethods.toInt(dr["Id"].ToString()),
                                Persons_Pep = CSEntities.ExtensionMethods.toInt(dr["PersonsID"].ToString()),
                                Name = dr["Name"].ToString(),
                                RelationshipId = CSEntities.ExtensionMethods.toInt(dr["RelationshipId"].ToString()),
                                FromYear = CSEntities.ExtensionMethods.toInt(dr["FromYear"].ToString()),
                                ToYear = CSEntities.ExtensionMethods.toInt(dr["ToYear"].ToString()),
                                Position = dr["Position"].ToString()
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }

            return
                result;
        }

        public void UpdateDeleteCreatePepsByDriver(int Id, int PersonID, string Name, string Position,
            int RelationshipId, int FromYear, int ToYear, DateTime Create_Date, int Create_UserId, int? Modi_UserId, DateTime? Modi_Date, string Action)
        {
            string query;
            SqlCommand command;

            /*
             Leyenda
                Action
             *      I = Insert
             *      U = Update
             *      D = Delete
             */

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "[POS].[SP_SET_PEPS_BY_DRIVER]";
                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", Id));
                command.Parameters.Add(new SqlParameter("@PersonsID", PersonID));
                command.Parameters.Add(new SqlParameter("@Name", Name));
                command.Parameters.Add(new SqlParameter("@RelationshipId", RelationshipId));
                command.Parameters.Add(new SqlParameter("@Position", Position));
                command.Parameters.Add(new SqlParameter("@FromYear ", FromYear));
                command.Parameters.Add(new SqlParameter("@ToYear ", ToYear));
                command.Parameters.Add(new SqlParameter("@Create_Date", Create_Date));
                command.Parameters.Add(new SqlParameter("@Create_UserId", Create_UserId));
                command.Parameters.Add(new SqlParameter("@Modi_UserId", Modi_UserId));
                command.Parameters.Add(new SqlParameter("@Modi_Date", Modi_Date));
                command.Parameters.Add(new SqlParameter("@Action", Action));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }
        }

        public PersonInfo CamposCumplimientoPorConductor(int PersonID, int Quotationid)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            PersonInfo result = new PersonInfo();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "[POS].[SP_GET_ALL_PERSON_INFO_BY_DRIVER] @PersonsID, @Quotationid";
                command = new SqlCommand(query, connection);
                dT = new DataTable();

                command.Parameters.Add(new SqlParameter("@PersonsID", PersonID));
                command.Parameters.Add(new SqlParameter("@Quotationid", Quotationid));

                try
                {
                    dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dT);

                    if (dT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dT.Rows)
                        {
                            result.Id = CSEntities.ExtensionMethods.toInt(dr["Id"].ToString());
                            result.SocialReasonId = CSEntities.ExtensionMethods.toInt(dr["SocialReasonId"].ToString());
                            result.OwnershipStructureId = CSEntities.ExtensionMethods.toInt(dr["OwnershipStructureId"].ToString());
                            result.PepFormularyOptionsId = CSEntities.ExtensionMethods.toInt(dr["PepFormularyOptionsId"].ToString());
                            result.IdentificationFinalBeneficiaryOptionsId = CSEntities.ExtensionMethods.toInt(dr["IdentificationFinalBeneficiaryOptionsId"].ToString());
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }

            return
                result;
        }

        public void UpdateCumplimientoFields(int PersonID, int PepFormularyOptionsId,
            int SocialReasonId, int OwnershipStructureId, int IdentificationFinalBeneficiaryOptionsId)
        {
            string query;
            SqlCommand command;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "[POS].[SP_UPDATE_CUMPLIMIENTO_FIELDS_BY_DRIVER]";
                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@PersonsID", PersonID));
                command.Parameters.Add(new SqlParameter("@PepFormularyOptionsId", PepFormularyOptionsId));
                command.Parameters.Add(new SqlParameter("@SocialReasonId", SocialReasonId));
                command.Parameters.Add(new SqlParameter("@OwnershipStructureId", OwnershipStructureId));
                command.Parameters.Add(new SqlParameter("@IdentificationFinalBeneficiaryOptionsId", IdentificationFinalBeneficiaryOptionsId));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }
        }

        public int GetUbicationIdOnSysflex(int Country_Id, int State_Prov_Id, int City_Id)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            int UbicationID = 0;

            using (SqlConnection connection = new SqlConnection(connectionStringGlobal))
            { //@Corp_Id=NULL, @Region_Id=NULL, @Country_Id, @Domesticreg_Id=NULL, @State_Prov_Id, @City_Id 
                query = "EXEC [Global].[SP_GET_COUNTRY_UBICACION] @Corp_Id,@Region_Id,@Country_Id,@Domesticreg_Id,@State_Prov_Id,@City_Id;";
                command = new SqlCommand(query, connection);
                dT = new DataTable();

                command.Parameters.AddWithValue("@Corp_Id", 0);
                command.Parameters.AddWithValue("@Region_Id", 0);
                command.Parameters.AddWithValue("@Country_Id", Country_Id);
                command.Parameters.AddWithValue("@Domesticreg_Id", 0);
                command.Parameters.AddWithValue("@State_Prov_Id", State_Prov_Id);
                command.Parameters.AddWithValue("@City_Id", City_Id);

                try
                {
                    dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dT);

                    if (dT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dT.Rows)
                        {
                            UbicationID = CSEntities.ExtensionMethods.toInt(dr["UbicacionId"].ToString());
                            return UbicationID;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }

            return
                UbicationID;
        }

        public System.Collections.Generic.List<STL.POS.Data.CSEntities.ComboCondicion> GetComboConditionsByParameters(
            string type, int? ramo, int? subramo, string nombreArch, string descrip, int? ano, int? id
            )
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            System.Collections.Generic.List<STL.POS.Data.CSEntities.ComboCondicion> result = new System.Collections.Generic.List<STL.POS.Data.CSEntities.ComboCondicion>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "EXEC [POS].[GET_COMBO_CONDICITIONS_BY_PARAMETERS] " +
                   "@TypeCondition," +
                   "@Ramo," +
                   "@SubRamo," +
                   "@NombreArchivo," +
                   "@Description," +
                   "@anio," +
                   "@id";

                command = new SqlCommand(query, connection);
                dT = new DataTable();

                command.Parameters.Add(new SqlParameter("@TypeCondition", !string.IsNullOrEmpty(type) ? type : ""));
                command.Parameters.Add(new SqlParameter("@Ramo", ramo.GetValueOrDefault()));
                command.Parameters.Add(new SqlParameter("@SubRamo", subramo.GetValueOrDefault()));
                command.Parameters.Add(new SqlParameter("@NombreArchivo", !string.IsNullOrEmpty(nombreArch) ? nombreArch : ""));
                command.Parameters.Add(new SqlParameter("@Description", !string.IsNullOrEmpty(descrip) ? descrip : ""));
                command.Parameters.Add(new SqlParameter("@anio", ano.GetValueOrDefault()));
                command.Parameters.Add(new SqlParameter("@id", id.GetValueOrDefault()));

                try
                {
                    dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dT);

                    if (dT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dT.Rows)
                        {
                            result.Add(new STL.POS.Data.CSEntities.ComboCondicion()
                            {
                                Ramo = CSEntities.ExtensionMethods.toInt(dr["Ramo"].ToString()),
                                SubRamo = CSEntities.ExtensionMethods.toInt(dr["SubRamo"].ToString()),
                                SecuenciaCondicion = CSEntities.ExtensionMethods.toInt(dr["SecuenciaCondicion"].ToString()),
                                NombreArchivo = dr["NombreArchivo"].ToString(),
                                Codigo = CSEntities.ExtensionMethods.toInt(dr["Codigo"].ToString()),
                                Descripcion = dr["Descripcion"].ToString(),
                                Porciento = CSEntities.ExtensionMethods.toDecimal(dr["Porciento"].ToString()),
                                Prima = CSEntities.ExtensionMethods.toDecimal(dr["Prima"].ToString()),
                                Reaseguro = CSEntities.ExtensionMethods.toInt(dr["Reaseguro"].ToString())
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }

            return
                result;
        }

        public System.Collections.Generic.IList<VehicleTypeWS> GetProductsSysflex(int vehicleTypeCoreId, int vehicleYear, string coreDeLeyStringDiscriminator, int codRamo)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            System.Collections.Generic.List<STL.POS.Data.POSEntities.ProductsDataFromSysflex> prods = new System.Collections.Generic.List<STL.POS.Data.POSEntities.ProductsDataFromSysflex>();

            var output = new System.Collections.Generic.List<VehicleTypeWS>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "EXEC [POS].[SP_GET_PRODUCTS_SYSFLEX] " +
                   "@Ramo," +
                   "@TipoVehiculo," +
                   "@ProductoId," +
                   "@Ano";

                command = new SqlCommand(query, connection);
                dT = new DataTable();

                command.Parameters.Add(new SqlParameter("@Ramo", codRamo));
                command.Parameters.Add(new SqlParameter("@TipoVehiculo", vehicleTypeCoreId));
                command.Parameters.Add(new SqlParameter("@ProductoId", DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Ano", vehicleYear));

                try
                {
                    dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dT);

                    if (dT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dT.Rows)
                        {
                            prods.Add(new STL.POS.Data.POSEntities.ProductsDataFromSysflex()
                            {
                                Ramo = CSEntities.ExtensionMethods.toInt(dr["Ramo"].ToString()),
                                SubRamo = CSEntities.ExtensionMethods.toInt(dr["SubRamo"].ToString()),
                                SubRamoName = dr["SubRamoName"].ToString(),
                                ProductoId = CSEntities.ExtensionMethods.toInt(dr["ProductoId"].ToString()),
                                Descripcion = dr["Descripcion"].ToString(),
                                PorcientoInicial = CSEntities.ExtensionMethods.toDecimal(dr["PorcientoInicial"].ToString()),
                                PorcientoFinal = CSEntities.ExtensionMethods.toDecimal(dr["PorcientoFinal"].ToString()),
                                MontoInicial = CSEntities.ExtensionMethods.toDecimal(dr["MontoInicial"].ToString()),
                                MontoFinal = CSEntities.ExtensionMethods.toDecimal(dr["MontoFinal"].ToString()),
                                Prima = CSEntities.ExtensionMethods.toDecimal(dr["Prima"].ToString()),
                                Porciento = CSEntities.ExtensionMethods.toDecimal(dr["Porciento"].ToString()),
                                Comentario = dr["Comentario"].ToString(),
                                Calculo = CSEntities.ExtensionMethods.toDecimal(dr["Calculo"].ToString()),
                                Compania = CSEntities.ExtensionMethods.toInt(dr["Compania"].ToString()),
                                TipoPoliza = CSEntities.ExtensionMethods.toInt(dr["TipoPoliza"].ToString()),
                                DescTipoPoliza = dr["DescTipoPoliza"].ToString(),
                                RequiereInspeccion = Convert.ToBoolean(dr["RequiereInspeccion"].ToString()),
                                IdCapacidad = CSEntities.ExtensionMethods.toInt(dr["IdCapacidad"].ToString()),
                                DescCapacidad = dr["DescCapacidad"].ToString(),
                                IdUso = CSEntities.ExtensionMethods.toInt(dr["IdUso"].ToString()),
                                DescUso = dr["DescUso"].ToString()
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }

            try
            {
                var vehicleTypes = (from p in prods
                                    orderby p.Descripcion
                                    select p.Descripcion.Trim()).Distinct().ToList();

                foreach (var vtId in vehicleTypes.OrderBy(t => t))
                {
                    var vehicleType = new VehicleTypeWS();
                    vehicleType.Name = vtId;

                    /*----------------------------------------------*/
                    vehicleType.NewUsages = new System.Collections.Generic.List<STL.POS.Data.POSEntities.WSEntities.UsageByProductWS>();

                    var Usages = (from pr in prods
                                  where pr.Descripcion == vtId
                                  select new { IdUso = pr.IdUso, DescUso = pr.DescUso }).Distinct().ToList();

                    foreach (var u in Usages)
                    {
                        var us = new STL.POS.Data.POSEntities.WSEntities.UsageByProductWS();
                        us.idUso = u.IdUso;
                        us.descUso = u.DescUso;
                        us.allowed = 1;
                        us.message = "";

                        vehicleType.NewUsages.Add(us);
                    }
                    /*----------------------------------------------*/

                    /*Uso por Producto*/
                    vehicleType.ProductByUsages = new System.Collections.Generic.List<STL.POS.Data.POSEntities.WSEntities.ProductByUsageWS>();

                    var UsagesByProd = (from pr in prods
                                        where pr.Descripcion == vtId
                                        select new { UsoDescripcion = pr.DescUso, ProductoDescripcion = pr.DescTipoPoliza }).Distinct().ToList();

                    foreach (var u in UsagesByProd)
                    {
                        var us = new STL.POS.Data.POSEntities.WSEntities.ProductByUsageWS();
                        us.UsoDescripcion = u.UsoDescripcion;
                        us.ProductoDescripcion = u.ProductoDescripcion;

                        vehicleType.ProductByUsages.Add(us);
                    }
                    /**/

                    /*Producto Cobertura por Uso */
                    vehicleType.CoveragesByUsages = new System.Collections.Generic.List<CoveragesByUsageWS>();

                    var CovsByUsages = (from pr in prods
                                        where pr.Descripcion == vtId
                                        //&& pr.TipoPoliza == p.TipoPoliza
                                        //select new { Id = pr.CoverageID, Name = pr.CoverageName, Type = pr.ProducttypeName, IsLaw = pr.RequiereInspeccion == false ? true : false }).Distinct().ToList();
                                        select new
                                        {
                                            Id = pr.SubRamo,
                                            Name = pr.SubRamoName,
                                            Type = pr.DescTipoPoliza,
                                            IsLaw = pr.RequiereInspeccion == false ? true : false,
                                            UsoDescripcion = pr.DescUso,
                                            ProductName = pr.DescTipoPoliza
                                        }).Distinct().ToList();

                    foreach (var c in CovsByUsages)
                    {
                        var cov = new CoveragesByUsageWS();
                        cov.Id = c.Id;
                        cov.Name = c.Name;
                        cov.IsLaw = c.IsLaw;
                        cov.UsoDescripcion = c.UsoDescripcion;
                        cov.ProductName = c.ProductName;

                        vehicleType.CoveragesByUsages.Add(cov);
                    }
                    /**/


                    vehicleType.Products = new System.Collections.Generic.List<ProductWS>();

                    var products = (from p in prods
                                    where p.Descripcion == vtId
                                    select new { p.TipoPoliza, p.IdCapacidad, p.DescCapacidad }).Distinct().ToList();
                    if (products.Count() <= 0) { continue; }

                    foreach (var p in products)
                    {
                        var prod = new ProductWS();
                        prod.Id = p.TipoPoliza;

                        var prodname = prods.FirstOrDefault(pd => pd.TipoPoliza == p.TipoPoliza);

                        if (prodname != null)
                        {
                            prod.Name = prodname.DescTipoPoliza;

                            prod.IdCapacidad = p.IdCapacidad.HasValue ? p.IdCapacidad.Value : (int?)null;
                            prod.DescCapacidad = p.DescCapacidad;
                        }
                        else
                        {
                            continue;
                        }

                        prod.Coverages = new System.Collections.Generic.List<CoverageWS>();

                        //Original 30-08-2017
                        /*
                        var cobs = (from pr in prods
                                    where pr.Descripcion == vtId
                                    && pr.TipoPoliza == p.TipoPoliza
                                    //select new { Id = pr.CoverageID, Name = pr.CoverageName, Type = pr.ProducttypeName, IsLaw = pr.RequiereInspeccion == false ? true : false }).Distinct().ToList();
                                    select new { Id = pr.SubRamo, Name = pr.SubRamoName, Type = pr.DescTipoPoliza, IsLaw = pr.RequiereInspeccion == false ? true : false }).Distinct().ToList();                        

                        foreach (var c in cobs)
                        {
                            var cov = new CoverageWS();
                            cov.Id = c.Id;
                            cov.Name = c.Name;
                            cov.IsLaw = c.IsLaw;

                            prod.Coverages.Add(cov);
                        }                        
                        */

                        prod.Coverages = prod.Coverages.OrderBy(c => c.Name).ToList();

                        vehicleType.Products.Add(prod);
                    }

                    vehicleType.Products = vehicleType.Products.OrderBy(p => p.Name).ToList();

                    vehicleType.NewUsages = vehicleType.NewUsages.OrderBy(c => c.descUso).ToList();

                    vehicleType.ProductByUsages = vehicleType.ProductByUsages.OrderBy(c => c.ProductoDescripcion).ToList();

                    vehicleType.CoveragesByUsages = vehicleType.CoveragesByUsages.OrderBy(c => c.Name).ToList();

                    output.Add(vehicleType);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return output;
        }

        public bool getQuotationToNotValidate(string QuotationNumber = "")
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "EXEC [POS].[SP_GET_ALL_IGNORE_RNC_VALIDATION] @QuotationNumber";

                command = new SqlCommand(query, connection);
                dT = new DataTable();

                command.Parameters.Add(new SqlParameter("@QuotationNumber", QuotationNumber));
                try
                {
                    dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dT);

                    if (dT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dT.Rows)
                        {
                            var quot = dr["QuotationNumber"].ToString();
                            var status = dr["Status"].ToString();

                            if (!string.IsNullOrEmpty(quot) && (status == "1" || status.ToLower() == "true"))
                            {
                                return true;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }

            return
                false;
        }

        public string getMunicipalityName(int municipalityID)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            string result = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "EXEC [POS].[SP_GET_MUNICIPALITY] @municipalityID";

                command = new SqlCommand(query, connection);
                dT = new DataTable();

                command.Parameters.Add(new SqlParameter("@municipalityID", municipalityID));
                try
                {
                    dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dT);

                    if (dT.Rows.Count > 0)
                    {
                        result = dT.Rows[0]["Municipio_Desc"].ToString();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }

            return
                result;
        }

        public bool IsAgentFinancial(int AgentId)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            bool result = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "EXEC [POS].[SP_GET_AGENT_IS_FINANCIAL] @Agent_Id";

                command = new SqlCommand(query, connection);
                dT = new DataTable();

                command.Parameters.Add(new SqlParameter("@Agent_Id", AgentId));
                try
                {
                    dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dT);

                    if (dT.Rows.Count > 0)
                    {
                        result = Convert.ToBoolean(dT.Rows[0]["EsFinanciera"].ToString());
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }

            return
                result;
        }

        #endregion

        #region [Reports]

        public DataTable GetVehicleProductQP(int quotationId,
            int fianzaJudicialCoverageDetailCoreId,
            int casaDelConductorCoverageDetailCoreId,
            string asistenciaVialCoverageDetailCoreIds,
            int serviciosGruaCoverageDetailCoreId)
        {
            var sqlConn = new SqlConnection(connectionString);
            var davp = new VehicleProductsTableAdapter();
            davp.Connection = sqlConn;
            return davp.GetData(quotationId
                , fianzaJudicialCoverageDetailCoreId
                , casaDelConductorCoverageDetailCoreId
                , asistenciaVialCoverageDetailCoreIds
                , serviciosGruaCoverageDetailCoreId);
        }

        public DataTable GetDriversQP(int quotationId)
        {
            var sqlConn = new SqlConnection(connectionString);
            var davp = new DriversTableAdapter();
            davp.Connection = sqlConn;
            return davp.GetData(quotationId);
        }

        public DataTable GetCoverageDetailsQP(int vehicleProductId)
        {
            var sqlConn = new SqlConnection(connectionString);
            var davp = new CoverageDetailsTableAdapter();
            davp.Connection = sqlConn;
            return davp.GetData(vehicleProductId);
        }

        public DataTable GetThirdPartyToProductLimitsQP(int quotationId)
        {
            var sqlConn = new SqlConnection(connectionString);
            var davp = new ThirdPartyToProductLimitsTableAdapter();
            davp.Connection = sqlConn;
            return davp.GetData(quotationId);
        }

        public DataTable GetSelfDamagesToProductLimitsQP(int quotationId)
        {
            var sqlConn = new SqlConnection(connectionString);
            var davp = new SelfDamagesToProductLimitsTableAdapter();
            davp.Connection = sqlConn;
            return davp.GetData(quotationId);
        }

        public DataTable GetServicesToProductLimitsQP(int quotationId)
        {
            var sqlConn = new SqlConnection(connectionString);
            var davp = new ServicesToProductLimitsTableAdapter();
            davp.Connection = sqlConn;
            return davp.GetData(quotationId);
        }

        public DataTable GetQuotationDetailQP(int quotationId)
        {
            var sqlConn = new SqlConnection(connectionString);
            var davp = new QuotationDetailTableAdapter();
            davp.Connection = sqlConn;
            return davp.GetData(quotationId);
        }

        public DataTable GetCuotasQP(int quotationId)
        {
            var sqlConn = new SqlConnection(connectionString);
            var davp = new CuotasTableAdapter();
            davp.Connection = sqlConn;
            return davp.GetData(quotationId);
        }

        //public DataSet GetQuotationDataset(int idQuotation)
        //{

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        string query = 
        //          "SELECT * " +
        //          "FROM dbo.POS.QUOTATION AS Q " +
        //          "WHERE Q.Id = " + idQuotation;

        //        SqlDataAdapter custAdapter = new SqlDataAdapter(query, connection);

        //        DataSet customerOrders = new DataSet();

        //        custAdapter.Fill(customerOrders, "Customers");

        //        return customerOrders;
        //    }
        //}

        public DataSet GetQuotationDs(int quotationId)
        {
            var ds = new DataSet("Quotation");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var sqlComm = new SqlCommand("SELECT [Id],[Status],[Created],[LastModified],[ProductNumber],[QuotationDailyNumber]" +
      ",[PolicyNumber],[QuotationNumber],[StartDate],[EndDate],[TermType_Id] FROM [POS].[QUOTATION] WHERE Id=@p0", connection);
                sqlComm.Parameters.Add(new SqlParameter("@p0", quotationId));

                var dataAdapter = new SqlDataAdapter(sqlComm);

                dataAdapter.Fill(ds);
            }

            return ds;
        }

        public DataSet GetVehicleProductsDataset(int idQuotation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query =
                  "SELECT [Make_Desc], [Model_Desc], [Chassis],[Plate],[Color],[VehiclePrice],[EnsuredAmount]" +
                  ",[PercentageToEnsure],[OthersPropertyLimits],[OnePersonDeathLimits],[AnotherPersonDeathLimits]" +
                  ",[PassengerDeathLimits],[AnotherPassengerDeathLimits],[BailLimits],[DriverRiskLimits],[CollisionRolloverLimits]" +
                  ",[FireTheftLimits],[ComprehensiveRiskLimits],[GlassBreakageLimits],[SpecialEquipmentLimits] " +
                  "FROM POS.VEHICLE_PRODUCT AS VP " +
                  "INNER JOIN Global.ST_VEHICLE_MODEL AS MODEL ON MODEL.Model_Id = VP.ModelId " +
                  "INNER JOIN Global.ST_VEHICLE_MAKE AS MAKE ON MAKE.Make_Id = VP.MakeId " +
                  "WHERE VP.QuotationId = @p0";

                var sqlComm = new SqlCommand(query, connection);
                sqlComm.Parameters.Add(new SqlParameter("@p0", idQuotation));

                DataSet dataSet = new DataSet("VehicleProducts");
                var dataAdapter = new SqlDataAdapter(sqlComm);
                dataAdapter.Fill(dataSet);

                return dataSet;
            }
        }

        public DataSet GetAdditionalProductsDataset(int idQuotation)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query =
                  "SELECT [IsSelected],[Name],[Description],[Prime],[ProductLimits_Id] " +
                  "FROM POS.ADDITIONAL_PRODUCTS AS AP " +
                  "INNER JOIN POS.VEHICLE_PRODUCT AS VP ON VP.Id = AP.Id " +
                  "WHERE VP.QuotationId = @p0";

                var sqlComm = new SqlCommand(query, connection);
                sqlComm.Parameters.Add(new SqlParameter("@p0", idQuotation));

                DataSet dataSet = new DataSet("AdditionalProducts");
                var dataAdapter = new SqlDataAdapter(sqlComm);
                dataAdapter.Fill(dataSet);

                return dataSet;
            }
        }

        public DataSet GetProductLimitsDataset(int idQuotation)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query =
                  "SELECT [IsSelected],[MinDeductible],[OthersProperty],[OnePersonDeath],[AnotherPersonDeath],[PassengerDeath],[AnotherPassengerDeath],[Bail],[SdPrime],[DriverRisk],[CollisionRollover],[FireTheft],[ComprehensiveRisk],[GlassBreakage],[SpecialEquipment],[TpPrime],[VehicleProduct_Id] " +
                  "FROM POS.PRODUCT_LIMITS AS PL " +
                  "INNER JOIN POS.VEHICLE_PRODUCT AS VP ON VP.Id = PL.VehicleProduct_Id " +
                  "WHERE VP.QuotationId = @p0";

                var sqlComm = new SqlCommand(query, connection);
                sqlComm.Parameters.Add(new SqlParameter("@p0", idQuotation));

                DataSet dataSet = new DataSet("ProductLimits");
                var dataAdapter = new SqlDataAdapter(sqlComm);
                dataAdapter.Fill(dataSet);

                return dataSet;
            }
        }

        public DataSet GetDriversDataset(int idQuotation)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query =
                  "SELECT " +
                    "D.FirstName, " +
                    "D.Surname, " +
                    "D.DateOfBirth, " +
                    "D.Sex, " +
                    "D.License, " +
                    "D.YearsDriving " +
                  "FROM POS.DRIVERS AS D " +
                  "WHERE D.QuotationId = " + idQuotation;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                DataSet dataSet = new DataSet("Drivers");

                dataAdapter.Fill(dataSet);

                return dataSet;
            }
        }

        public virtual System.Collections.Generic.List<STL.POS.Data.CSEntities.GetErrorQuotationResult> GetErrorQuotation(string QuotationNumber)
        {
            string query;
            SqlDataAdapter dataAdapter;
            DataTable dT;
            SqlCommand command;
            System.Collections.Generic.List<STL.POS.Data.CSEntities.GetErrorQuotationResult> listError = new System.Collections.Generic.List<STL.POS.Data.CSEntities.GetErrorQuotationResult>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "exec [Global].[GET_ERROR_QUOTATION] @QuotNumber";
                command = new SqlCommand(query, connection);
                dT = new DataTable();

                command.Parameters.Add(new SqlParameter("@QuotNumber", QuotationNumber));

                try
                {
                    dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dT);


                    if (dT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dT.Rows)
                        {
                            listError.Add(new CSEntities.GetErrorQuotationResult()
                            {
                                Corp_Id = CSEntities.ExtensionMethods.toInt(dr["Corp_Id"].ToString()),
                                Log_Id = CSEntities.ExtensionMethods.toInt(dr["Log_Id"].ToString()),
                                Log_Type_Id = CSEntities.ExtensionMethods.toInt(dr["Log_Type_Id"].ToString()),
                                Project_Id = CSEntities.ExtensionMethods.toInt(dr["Project_Id"].ToString()),
                                Company_Id = CSEntities.ExtensionMethods.toInt(dr["Company_Id"].ToString()),
                                Identifier = dr["Identifier"].ToString(),

                                Log_Value = dr["Log_Value"].ToString(),

                                Create_Date = CSEntities.ExtensionMethods.toDatetime(dr["Create_Date"].ToString()),
                                HostName = dr["HostName"].ToString()
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }

            return
                listError;
        }






        //SALUD
        public DataTable GetQuotationDetailSaludQP(int quotationId)
        {
            var sqlConn = new SqlConnection(connectionString);
            var davp = new QuotationSaludDetailTableAdapter();
            davp.Connection = sqlConn;
            return davp.GetData(quotationId);
        }
        public DataTable GetPersonsQP(int quotationId)
        {
            var sqlConn = new SqlConnection(connectionString);
            var davp = new PersonsTableAdapter();
            davp.Connection = sqlConn;
            return davp.GetData(quotationId);
        }

        #endregion
    }
}