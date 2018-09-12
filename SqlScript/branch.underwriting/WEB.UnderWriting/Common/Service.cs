﻿using DI.UnderWriting;
using DI.UnderWriting.Interfaces;
using Entity.UnderWriting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnbaseFileTransfer;
using System.Configuration;

namespace WEB.UnderWriting.Common
{
    public class Services
    {
        public static IPayment PaymentManager
        {
            get { return diManager.PaymentManager; }
        }
        public static IContact ContactManager
        {
            get { return diManager.ContactManager; }
        }
        public static IPolicy PolicyManager
        {
            get { return diManager.PolicyManager; }
        }
        public static IStep StepManager
        {
            get { return diManager.StepManager; }
        }
        public static ICase CaseManager
        {
            get { return diManager.CaseManager; }
        }
        public static INote NoteManager
        {
            get { return diManager.NoteManager; }
        }
        public static IRequirement RequirementManager
        {
            get { return diManager.RequirementManager; }
        }
        public static IMedical MedicalManager
        {
            get { return diManager.MedicalManager; }
        }
        public static IAmmendment AmmendmentManager
        {
            get { return diManager.AmmendmentManager; }
        }
        public static IRider RiderManager
        {
            get { return diManager.RiderManager; }
        }
        public static IBeneficiary BeneficiaryManager
        {
            get { return diManager.BeneficiaryManager; }
        }
        public static ICallRex CallRexManager
        {
            get { return diManager.CallRexManager; }
        }
        public static IDropDown DropDownManager
        {
            get { return diManager.DropDownManager; }
        }

        public static UnderWritingDIManager diManager = new UnderWritingDIManager();
        public DropDownManager DropDowns = new DropDownManager();

        private string key = "SessionData";

        public Common.SessionList datos;

        public Services(string KeyName = "SessionData")
        {
            key = KeyName;
            if (HttpContext.Current.Session == null)
            {
                HttpContext.Current.Session.Add(key, new SessionList(KeyName));
                (HttpContext.Current.Session[key] as SessionList).ContactInfo = new SessionContact();
            }
            else
            {
                if (HttpContext.Current.Session[key] == null)
                {
                    HttpContext.Current.Session.Add(key, new SessionList(KeyName));
                    (HttpContext.Current.Session[key] as SessionList).ContactInfo = new SessionContact();
                }
            }
            datos = (HttpContext.Current.Session[key] as SessionList);
        }

        public Entity.UnderWriting.Entities.Case.SearchResult SearchResultParameters
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.SearchResult; }
            set
            {
                datos.ContactInfo.SearchResult = value;
                datos.Save();
            }
        }

        public string PdfViewerKey
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.PdfViewerKey; }
            set
            {
                datos.ContactInfo.PdfViewerKey = value;
                datos.Save();
            }
        }

        public int Corp_Id
        {
            get
            {
                return ((HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Corp_Id == 0) ?
                    int.Parse(System.Configuration.ConfigurationManager.AppSettings["CorpId"]) :
                    (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Corp_Id;
            }
            set
            {
                datos.ContactInfo.Corp_Id = value;
                datos.Save();
            }
        }
        public int Region_Id
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Region_Id; }
            set
            {
                datos.ContactInfo.Region_Id = value;
                datos.Save();
            }
        }
        public int Country_Id
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Country_Id; }
            set
            {
                datos.ContactInfo.Country_Id = value;
                datos.Save();
            }
        }
        public int Domesticreg_Id
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Domesticreg_Id; }
            set
            {
                datos.ContactInfo.Domesticreg_Id = value;
                datos.Save();
            }
        }
        public int State_Prov_Id
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.State_Prov_Id; }
            set
            {
                datos.ContactInfo.State_Prov_Id = value;
                datos.Save();
            }
        }
        public int City_Id
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.City_Id; }
            set
            {
                datos.ContactInfo.City_Id = value;
                datos.Save();
            }
        }
        public int Office_Id
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Office_Id; }
            set
            {
                datos.ContactInfo.Office_Id = value;
                datos.Save();
            }
        }
        public int Case_Seq_No
        {
            get { return ((HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Case_Seq_No != 0) ? (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Case_Seq_No : 0; }
            set
            {
                datos.ContactInfo.Case_Seq_No = value;
                datos.Save();
            }
        }
        public int Hist_Seq_No
        {
            get { return ((HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Hist_Seq_No != 0) ? (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Hist_Seq_No : 0; }
            set
            {
                datos.ContactInfo.Hist_Seq_No = value;
                datos.Save();
            }
        }
        public string Policy_Id
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Policy_Id; }
            set
            {
                datos.ContactInfo.Policy_Id = value;
                datos.Save();
            }
        }
        public bool IsOwner
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.IsOwner; }
            set
            {
                datos.ContactInfo.IsOwner = value;
                datos.Save();
            }
        }
        public bool OwnerIsInsured
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.OwnerIsInsured; }
            set
            {
                datos.ContactInfo.OwnerIsInsured = value;
                datos.Save();
            }
        }
        public int Contact_Id
        {
            get { return ((HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Contact_Id != 0) ? (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Contact_Id : 0; }
            set
            {
                datos.ContactInfo.Contact_Id = value;
                datos.Save();
            }
        }


        public DateTime? SubmitDate
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.SubmitDate; }
            set
            {
                datos.ContactInfo.SubmitDate = value;
                datos.Save();
            }
        }

        public int? InsuredPeriod
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.InsuredPeriod; }
            set
            {
                datos.ContactInfo.InsuredPeriod = value;
                datos.Save();
            }
        }
        public Tools.ProductBehavior ProductDesc
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.ProductDesc; }
            set
            {
                datos.ContactInfo.ProductDesc = value;
                datos.Save();
            }
        }
        public int? AddInsuredContactId
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.AddInsuredContactId; }
            set
            {
                datos.ContactInfo.AddInsuredContactId = value;
                datos.Save();
            }
        }
        public int Directory_Id
        {
            get { return ((HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Directory_Id != 0) ? (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Directory_Id : 0; }
            set
            {
                datos.ContactInfo.Directory_Id = value;
                datos.Save();
            }
        }
        public int Underwriter_Id
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.Underwriter_Id; }
            set
            {
                datos.ContactInfo.Underwriter_Id = value;
                datos.Save();
            }
        }
        public int ContactSeq_No
        {
            get { return ((HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.ContactSeq_No != 0) ? (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.ContactSeq_No : 0; }
            set
            {
                datos.ContactInfo.ContactSeq_No = value;
                datos.Save();
            }
        }
        public int RoleTypeId
        {
            get { return ((HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.RoleTypeId != 0) ? (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.RoleTypeId : 0; }
            set
            {
                datos.ContactInfo.RoleTypeId = value;
                datos.Save();
            }
        }
        public int ContactAge
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.ContactAge.HasValue ? (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.ContactAge.Value : 0; }
            set
            {
                datos.ContactInfo.ContactAge = value;
                datos.Save();
            }
        }
        public string ContactGender
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.ContactGender; }
            set
            {
                datos.ContactInfo.ContactGender = value;
                datos.Save();
            }
        }
        public string UnderwriterEmail
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.UnderwriterEmail; }
            set
            {
                datos.ContactInfo.UnderwriterEmail = value;
                datos.Save();
            }
        }

        public int LanguageId
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.LanguageId; }
            set
            {
                datos.ContactInfo.LanguageId = value;
                datos.Save();
            }
        }


        public string TabName
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored == null ? "AllOpen" : (HttpContext.Current.Session[key] as SessionList).Stored.TabName; }
            set
            {
                datos.TabName = value;
                datos.Save();
            }
        }

        public int CompanyId
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.CompanyId; }
            set
            {
                datos.ContactInfo.CompanyId = value;
                datos.Save();
            }
        }

        public string Mp3TempDir
        {
            get { return HttpContext.Current.Server.MapPath(@"~\TempFiles\Mp3\"); }
        }
        public string RecordingsDir
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Recording_path"]; }
        }
        public string SoxFilePath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SoxExe_Path"]; }
        }
        public string TempFilePath
        {
            get { return HttpContext.Current.Server.MapPath("~/TempFiles"); }
        }
        public string SmtpServer
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SmtpServer"]; }
        }
        public string FromEmail
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["FromEmail"]; }
        }
        public string TestReceiptEmail
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TestReceiptEmail"]; }
        }
        public int ProjectId
        {
            get { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["ProjectId"]); }
        }

        public Tools.EFamilyProductType GetProductFamily()
        {
            var result = Tools.EFamilyProductType.None;

            switch (ProductDesc)
            {
                case Tools.ProductBehavior.Luminis:
                case Tools.ProductBehavior.LuminisVIP:
                case Tools.ProductBehavior.Exequium:
                case Tools.ProductBehavior.ExequiumVIP:
                    result = Tools.EFamilyProductType.Funeral;
                    break;
                case Tools.ProductBehavior.Axys:
                case Tools.ProductBehavior.Horizon:
                    result = Tools.EFamilyProductType.Retirement;
                    break;
                case Tools.ProductBehavior.EduPlan:
                case Tools.ProductBehavior.Scholar:
                    result = Tools.EFamilyProductType.Education;
                    break;
                case Tools.ProductBehavior.Sentinel:
                case Tools.ProductBehavior.Lighthouse:
                case Tools.ProductBehavior.Guardian:
                case Tools.ProductBehavior.GuardianPlus:
                case Tools.ProductBehavior.Orion:
                case Tools.ProductBehavior.OrionPlus:
                case Tools.ProductBehavior.VidaCredito:
                    result = Tools.EFamilyProductType.TermInsurance;
                    break;
                case Tools.ProductBehavior.Legacy:
                case Tools.ProductBehavior.CompassIndex:
                    result = Tools.EFamilyProductType.LifeInsurance;
                    break;
                case Tools.ProductBehavior.None:
                    result = Tools.EFamilyProductType.None;
                    break;
                case Tools.ProductBehavior.Elite:
                case Tools.ProductBehavior.Select:
                case Tools.ProductBehavior.Fortis:
                case Tools.ProductBehavior.Serenity:
                case Tools.ProductBehavior.Asistencia90dias:
                case Tools.ProductBehavior.Asistencia30dias:
                case Tools.ProductBehavior.Asistencia60dias:
                    result = Tools.EFamilyProductType.HealthInsurance;
                    break;
            }
            return result;
        }

        #region PolicyPlan Methods
        public void SavePPInvestmentProfile(Policy.InvestProfile investmentProfile, bool isUpdate = false)
        {
            //Setting Key
            investmentProfile.CorpId = Corp_Id;
            investmentProfile.RegionId = Region_Id;
            investmentProfile.CountryId = Country_Id;
            investmentProfile.DomesticregId = Domesticreg_Id;
            investmentProfile.StateProvId = State_Prov_Id;
            investmentProfile.CityId = City_Id;
            investmentProfile.OfficeId = Office_Id;
            investmentProfile.CaseSeqNo = Case_Seq_No;
            investmentProfile.HistSeqNo = Hist_Seq_No;

            //Setting UserID
            investmentProfile.UserId = Underwriter_Id;

            if (isUpdate)
                PolicyManager.UpdateInvestmentProfile(investmentProfile);
            else
                PolicyManager.InsertInvestmentProfile(investmentProfile);
        }

        public void SavePPPersonalizedProfile(List<Policy.InvestProfilePersonalized> saveData)
        {
            foreach (var item in saveData)
            {
                //Setting Key
                item.CorpId = Corp_Id;
                item.RegionId = Region_Id;
                item.CountryId = Country_Id;
                item.DomesticregId = Domesticreg_Id;
                item.StateProvId = State_Prov_Id;
                item.CityId = City_Id;
                item.OfficeId = Office_Id;
                item.CaseSeqNo = Case_Seq_No;
                item.HistSeqNo = Hist_Seq_No;

                //Setting UserID
                item.UserId = Underwriter_Id;
            }

            PolicyManager.SetInvestProfilePersonalized(saveData);
        }
        #endregion

        #region Send to Reinsurance Methods
        public List<Reinsurance.Communication> FillResinsuranceComm(Reinsurance.Communication commItem = null)
        {
            if (commItem == null)
            {
                commItem = new Reinsurance.Communication()
                {
                    CorpId = Corp_Id,
                    RegionId = Region_Id,
                    CountryId = Country_Id,
                    DomesticRegId = Domesticreg_Id,
                    StateProvId = State_Prov_Id,
                    CityId = City_Id,
                    OfficeId = Office_Id,
                    CaseSeqNo = Case_Seq_No,
                    HistSeqNo = Hist_Seq_No
                };
            }

            var data = PolicyManager.GetReinsuranceCommunication(commItem).ToList();

            return data;
        }

        public Reinsurance.StepAvailable StepAvailable(Reinsurance.StepAvailable step = null)
        {
            if (step == null)
            {
                step = new Reinsurance.StepAvailable()
                {
                    CorpId = Corp_Id,
                    RegionId = Region_Id,
                    CountryId = Country_Id,
                    DomesticRegId = Domesticreg_Id,
                    StateProvId = State_Prov_Id,
                    CityId = City_Id,
                    OfficeId = Office_Id,
                    CaseSeqNo = Case_Seq_No,
                    HistSeqNo = Hist_Seq_No
                };
            }

            var stepAvailable = PolicyManager.GetStepAvailable(step);

            return stepAvailable;
        }

        public IEnumerable<Reinsurance.Communication> GetRCHtmlAndAttach(Reinsurance.Communication commItem = null)
        {
            if (commItem == null)
            {
                commItem = new Reinsurance.Communication()
                {
                    CorpId = Corp_Id,
                    RegionId = Region_Id,
                    CountryId = Country_Id,
                    DomesticRegId = Domesticreg_Id,
                    StateProvId = State_Prov_Id,
                    CityId = City_Id,
                    OfficeId = Office_Id,
                    CaseSeqNo = Case_Seq_No,
                    HistSeqNo = Hist_Seq_No
                };
            }

            var data = PolicyManager.GetReinsuranceCommunicationHtmlAndAttachments(commItem);

            return data;
        }

        public Payment.DocumentInfomation GetCommDocument(int DocumentCategoryId, int DocumentTypeId, int DocumentId)
        {
            var document = PaymentManager.GetDocument(
                                        coprId: Corp_Id,
                                        regionId: Region_Id,
                                        countryId: Country_Id,
                                        domesticRegId: Domesticreg_Id,
                                        stateProvId: State_Prov_Id,
                                        cityId: City_Id,
                                        officeId: Office_Id,
                                        caseSeqNo: Case_Seq_No,
                                        histSeqNo: Hist_Seq_No,
                                        documentCategoryId: DocumentCategoryId,
                                        documentTypeId: DocumentTypeId,
                                        documentId: DocumentId);
            return document;
        }

        public IEnumerable<Reinsurance.FacultativeStatus> GetReinsuranceFacultativeStatus()
        {
            var facultativeStatus = PolicyManager.GetReinsuranceFacultativeStatus();
            return facultativeStatus;
        }

        public IEnumerable<Reinsurance.RatingRisk> GetRatingRisk(string ratingTable)
        {
            var ratingRisk = PolicyManager.GetRatingRisk(ratingTable);
            return ratingRisk;
        }

        public Byte[] GetCommDocBinary(int DocumentCategoryId, int DocumentTypeId, int DocumentId)
        {
            var document = PaymentManager.GetDocument(
                                        coprId: Corp_Id,
                                        regionId: Region_Id,
                                        countryId: Country_Id,
                                        domesticRegId: Domesticreg_Id,
                                        stateProvId: State_Prov_Id,
                                        cityId: City_Id,
                                        officeId: Office_Id,
                                        caseSeqNo: Case_Seq_No,
                                        histSeqNo: Hist_Seq_No,
                                        documentCategoryId: DocumentCategoryId,
                                        documentTypeId: DocumentTypeId,
                                        documentId: DocumentId).DocumentBinary;
            return document;
        }

        public class Item
        {
            public bool isComplete { get; set; }
            public string message { get; set; }

        }

        /**
         * Este metodo se encarga de realizar las siguientes validaciones:
         - Que tenga todas las acciones cerradas.
         - Que tenga la acción de Confirmation Call y Background Check cerradas.
          Que tenga una suma asegurada.
         - Que tenga por lo menos 1 beneficiario principal.	
         - Que la suma de los porcentajes de los beneficiarios sume 100.
         - Que tenga por lo menos una dirección, correo y teléfono marcados como principal.
          Que tenga aplicado el pago por el total o más del monto de prima periódica.
         - Que no tenga ningún requisito pendiente.
          Que tenga una cadena de agentes asignada.
         */
        public List<Item> isPolicyComplete(Tools.PolicyKeyItem Policy = null)
        {
            List<Item> item = new List<Item>();

            if (Policy == null)
            {
                Policy = new Tools.PolicyKeyItem()
                {
                    CorpId = Corp_Id,
                    RegionId = Region_Id,
                    CountryId = Country_Id,
                    DomesticregId = Domesticreg_Id,
                    StateProvId = State_Prov_Id,
                    CityId = City_Id,
                    OfficeId = Office_Id,
                    CaseSeqNo = Case_Seq_No,
                    HistSeqNo = Hist_Seq_No,
                    ContactId = Contact_Id
                };
            }

            var dataSteps = Services.StepManager.GetAll(Policy.CorpId, Policy.RegionId, Policy.CountryId, Policy.DomesticregId,
            Policy.StateProvId, Policy.CityId, Policy.OfficeId, Policy.CaseSeqNo, Policy.HistSeqNo, 1).Where(x => x.ProcessStatus != 3);

            // Que tenga todas las acciones cerradas.
            var pendingSteps = dataSteps.Where(x => x.ProcessStatus != 2);

            if (pendingSteps.Any())
            {

                item.Add(new Item
                {
                    isComplete = false,
                    message = "There are " + dataSteps.Count() + " Steps Pending"

                });

            }
            else
            {


                item.Add(new Item
                {
                    isComplete = true,
                    message = "All steps are completed"

                });


            }

            // Que tenga la acción de Confirmation Call y Background Check cerradas.
            var backgrounCheckStep = dataSteps.Where(x => x.StepCode == "BC");
            var confirmationCallStep = dataSteps.Where(x => x.StepCode == "CONFCALL");

            if (backgrounCheckStep.Any())
            {
                if (!backgrounCheckStep.Where(x => x.ProcessStatus == 2).Any())
                {

                    item.Add(new Item
                    {
                        isComplete = false,
                        message = "The background check step have to be completed."

                    });
                }
                else
                {

                    item.Add(new Item
                    {
                        isComplete = true,
                        message = "The background check step is completed."

                    });

                }
            }
            else
            {

                item.Add(new Item
                {
                    isComplete = false,
                    message = "You must have at least one background check step created."

                });

            }

            if (confirmationCallStep.Any())
            {
                if (!confirmationCallStep.Where(x => x.ProcessStatus == 2).Any())
                {

                    item.Add(new Item
                    {
                        isComplete = false,
                        message = "The confirmation call step have to be completed."

                    });
                }
                else
                {

                    item.Add(new Item
                    {
                        isComplete = true,
                        message = "The confirmation call step is completed."

                    });

                }
            }
            else
            {

                item.Add(new Item
                {
                    isComplete = false,
                    message = "You must have at least one confirmation call step created."

                });

            }

            // Que tenga por lo menos 1 beneficiario principal.

            var dataBeneficiary = Services.BeneficiaryManager.GetAllBeneficiary(Policy.CorpId, Policy.RegionId, Policy.CountryId, Policy.DomesticregId, Policy.StateProvId, Policy.CityId, Policy.OfficeId, Policy.CaseSeqNo, Policy.HistSeqNo, true, 1, null, LanguageId).ToList();


            var hasBeneficiarie = dataBeneficiary.Any();
            item.Add(new Item
            {
                isComplete = hasBeneficiarie,
                message = hasBeneficiarie ? "has a main beneficiary" : "You must have at least main beneficiary added."
            });


            //Que la suma de los porcentajes de los beneficiarios sume 100.

            var suma = dataBeneficiary.Sum(x => x.BenefitsPercent);
            var isFuneral = GetProductFamily() == Tools.EFamilyProductType.Funeral;
            bool isCorrectPercentage = isFuneral;
            if (!isFuneral)
                isCorrectPercentage = (suma == 100);


            item.Add(new Item
            {
                isComplete = isCorrectPercentage,
                message = isCorrectPercentage ? "has 100% percentage" : "The total percentage of main beneficiaries must be 100%"
            });


            //Que tenga por lo menos una dirección, correo y teléfono marcados como principal.
            var phone = Services.ContactManager.GetCommunicatonPhone(Policy.CorpId, Policy.ContactId, LanguageId).Where(x => x.IsPrimary);
            var email = Services.ContactManager.GetCommunicatonEmail(Policy.CorpId, Policy.ContactId, LanguageId).Where(x => x.IsPrimary);
            var address = Services.ContactManager.GetCommunicatonAdress(Policy.CorpId, Policy.ContactId, LanguageId).Where(x => x.IsPrimary);

            var isPrimaryPhone = phone.Any();
            var isPrimaryEmail = email.Any();
            var isPrimaryAddress = address.Any();

            item.Add(new Item
            {
                isComplete = isPrimaryPhone,
                message = isPrimaryPhone ? "has Primary Phone" : "You must have at least one primary phone"
            });

            item.Add(new Item
            {
                isComplete = isPrimaryEmail,
                message = isPrimaryEmail ? "has Primary Email" : "You must have at least one primary email"
            });


            item.Add(new Item
            {
                isComplete = isPrimaryAddress,
                message = isPrimaryAddress ? "has Primary Address" : "You must have at least one primary address"
            });



            //Que tenga aplicado el pago por el total o más del monto de prima periódica.

            //Que no tenga ningún requisito pendiente.
            var requirementData = Services.RequirementManager.GetAll(
                     Policy.CorpId, Policy.RegionId, Policy.CountryId,
                     Policy.DomesticregId, Policy.StateProvId, Policy.CityId,
                     Policy.OfficeId, Policy.CaseSeqNo, Policy.HistSeqNo, LanguageId
                   ).ToList().Where(x => x.IsComplete == false);

            var incompleteRequirements = requirementData.Any();

            item.Add(new Item
            {
                isComplete = !incompleteRequirements,
                message = !incompleteRequirements ? "all requirements are completed" : "All requirements must be completed"
            });


            return item;

        }

        public IEnumerable<Policy.CategoryDocument> GetCategoryDocuments(Tools.PolicyKeyItem Policy = null, int? documentCategoryId = null, int? documentTypeId = null)
        {
            if (Policy == null)
            {
                Policy = new Tools.PolicyKeyItem()
                {
                    CorpId = Corp_Id,
                    RegionId = Region_Id,
                    CountryId = Country_Id,
                    DomesticregId = Domesticreg_Id,
                    StateProvId = State_Prov_Id,
                    CityId = City_Id,
                    OfficeId = Office_Id,
                    CaseSeqNo = Case_Seq_No,
                    HistSeqNo = Hist_Seq_No


                };
            }

            var docsData = PolicyManager.GetCategoryDocument(
                                        coprId: Policy.CorpId,
                                        regionId: Policy.RegionId,
                                        countryId: Policy.CountryId,
                                        domesticRegId: Policy.DomesticregId,
                                        stateProvId: Policy.StateProvId,
                                        cityId: Policy.CityId,
                                        officeId: Policy.OfficeId,
                                        caseSeqNo: Policy.CaseSeqNo,
                                        histSeqNo: Policy.HistSeqNo,
                                        docCategoryId: documentCategoryId,
                                        docTypeId: documentTypeId,
                                        languageId: LanguageId);
            return docsData;
        }

        public Reinsurance.Communication InsertReinsuranceCommunication(Reinsurance.Communication commItem)
        {
            commItem.CorpId = Corp_Id;
            commItem.RegionId = Region_Id;
            commItem.CountryId = Country_Id;
            commItem.DomesticRegId = Domesticreg_Id;
            commItem.StateProvId = State_Prov_Id;
            commItem.CityId = City_Id;
            commItem.OfficeId = Office_Id;
            commItem.CaseSeqNo = Case_Seq_No;
            commItem.HistSeqNo = Hist_Seq_No;

            var comm = PolicyManager.InsertReinsuranceCommunication(commItem);

            return comm;
        }

        public void SetReinsuranceCommunicationAttachment(List<Reinsurance.Communication> commItemList)
        {
            foreach (var item in commItemList)
            {
                item.CorpId = Corp_Id;
                item.RegionId = Region_Id;
                item.CountryId = Country_Id;
                item.DomesticRegId = Domesticreg_Id;
                item.StateProvId = State_Prov_Id;
                item.CityId = City_Id;
                item.OfficeId = Office_Id;
                item.CaseSeqNo = Case_Seq_No;
                item.HistSeqNo = Hist_Seq_No;
            }

            var comm = PolicyManager.SetReinsuranceCommunicationAttachment(commItemList);
        }

        //Bmarroquin 13-05-2017 se crea metodo
        public string getIsValidFacultativeID(int Case_seq_No, string Facultative_Reinsurance_Id)
        {
            return PolicyManager.getIsValidFacultativeID(Case_seq_No, Facultative_Reinsurance_Id);
        }


        #endregion

        #region Steps
        public int InsertNewStep(Step.NewStep stepToAdd)
        {
            stepToAdd.CorpId = Corp_Id;
            stepToAdd.RegionId = Region_Id;
            stepToAdd.CountryId = Country_Id;
            stepToAdd.DomesticregId = Domesticreg_Id;
            stepToAdd.StateProvId = State_Prov_Id;
            stepToAdd.CityId = City_Id;
            stepToAdd.OfficeId = Office_Id;
            stepToAdd.CaseSeqNo = Case_Seq_No;
            stepToAdd.HistSeqNo = Hist_Seq_No;
            stepToAdd.UserId = Underwriter_Id;

            return diManager.StepManager.Insert(stepToAdd);
        }
        #endregion

        #region RidersInfo
        public Boolean HasAdditionalInsured()
        {
            Entity.UnderWriting.Entities.Policy.Parameter PolicyParameter = new Entity.UnderWriting.Entities.Policy.Parameter();
            //Nuevo Forma Cambio Rabel O
            PolicyParameter.CorpId = Corp_Id;
            PolicyParameter.RegionId = Region_Id;
            PolicyParameter.CountryId = Country_Id;
            PolicyParameter.DomesticregId = Domesticreg_Id;
            PolicyParameter.StateProvId = State_Prov_Id;
            PolicyParameter.CityId = City_Id;
            PolicyParameter.OfficeId = Office_Id;
            PolicyParameter.CaseSeqNo = Case_Seq_No;
            PolicyParameter.HistSeqNo = Hist_Seq_No;
            PolicyParameter.LanguageId = LanguageId;

            var ridersInfo = (from r in diManager.RiderManager.GetAllRider(PolicyParameter)
                              join d in DropDowns.GetDropDown(DropDownType.RiderType, Corp_Id, projectId: ProjectId, companyId: CompanyId) on
                              r.RiderTypeId equals int.Parse(d.Value)
                              select new { SerieCode = d.Text }).Where(r => r.SerieCode == "SPINS");


            return ridersInfo.Any();
        }
        #endregion

        #region Underwriting Call
        public List<Policy.UnderwritingCallTemplate> GetUCTemplateByCategory(Tools.UCTemplates categoryTemplate, int language)
        {
            var data = PolicyManager.GetUnderwritingCallTemplateByCategory(Corp_Id, (int)categoryTemplate, (int)language).ToList();

            return data;
        }

        public String GetOwnerName()
        {
            var data = DropDowns.GetDropDown(DropDownType.Summary, 
											 Corp_Id, 
											 Region_Id, 
											 Country_Id,
											 Domesticreg_Id, 
											 State_Prov_Id, 
											 City_Id, 
											 Office_Id, 
											 Case_Seq_No,
											 Hist_Seq_No, 
											 Contact_Id, 
											 projectId: ProjectId, 
											 companyId: CompanyId,
											 languageId: LanguageId);

            var ownerId = data.Count() > 1 ? int.Parse(data.First(r => r.Value.Split('|')[1] == "1").Value.Split('|')[0]) : int.Parse(data.First().Value.Split('|')[0]);

            var contactInfo = ContactManager.GetContact(coprId: Corp_Id, 
														regionId: Region_Id, 
														countryId: Country_Id, 
														domesticRegId: Domesticreg_Id, 
														stateProvId: State_Prov_Id, 
														cityId: City_Id, 
														officeId: Office_Id, 
														caseSeqNo: Case_Seq_No, 
														histSeqNo: Hist_Seq_No, 
														contactId: Contact_Id, 
														contactRoleTypeId: 
														RoleTypeId, 
														languageId: LanguageId);

            return contactInfo != null ? contactInfo.FullName : "";
        }
        #endregion

        #region Contact PEP
        /// <summary>
        /// Inserta o actualiza una lista de contacto politico
        /// </summary>
        /// <param name="itemList"></param>
        public void SetCitizenContact(List<Contact.CitizenContact> itemList)
        {
            //Validaciones o reglas del negocio  
            if (itemList == null || !itemList.Any()) return;

            foreach (var item in itemList)
                ContactManager.SetCitizenContact(item);
        }

        /// <summary>
        /// Obtiene una lista de Social Exposure
        /// </summary>
        /// <returns></returns>
        public List<Entity.UnderWriting.Entities.Contact.SocialExposure> GetContactSocialExposureByContact()
        {
            var SocialExposureList = Services.ContactManager.GetContactSocialExposureByContact(Corp_Id,
                                                                                      Region_Id,
                                                                                      Country_Id,
                                                                                      Domesticreg_Id,
                                                                                      State_Prov_Id,
                                                                                      City_Id, Office_Id,
                                                                                      Case_Seq_No,
                                                                                      Hist_Seq_No,
                                                                                      Contact_Id,
                                                                                      languageId: LanguageId
                                                                                      ).ToList();

            return SocialExposureList;
        }

        /// <summary>
        /// Obtiene una lista de Social Exposure Relationship
        /// </summary>
        /// <returns></returns>
        public List<Entity.UnderWriting.Entities.Contact.SocialExposureRelationship> GetContactSocialExposureRelationshipByContact()
        {
            var SocialExposureRelationshipList = Services.ContactManager.GetContactSocialExposureRelationshipByContact(Corp_Id,
                                                                                                                        Region_Id,
                                                                                                                        Country_Id,
                                                                                                                        Domesticreg_Id,
                                                                                                                        State_Prov_Id,
                                                                                                                        City_Id, Office_Id,
                                                                                                                        Case_Seq_No,
                                                                                                                        Hist_Seq_No,
                                                                                                                        Contact_Id,
                                                                                                                        languageId: LanguageId
                                                                                                                       ).ToList();

            return SocialExposureRelationshipList;
        }

        /// <summary>
        /// Obtiene una lista de Citizen Question        
        ///</summary>
        /// <returns></returns>
        public List<Entity.UnderWriting.Entities.Contact.CitizenQuestion> GetContactCitizenQuestionByContact()
        {
            var CitizenQuestionList = Services.ContactManager.GetContactCitizenQuestionByContact(Corp_Id,
                                                                                              Region_Id,
                                                                                              Country_Id,
                                                                                              Domesticreg_Id,
                                                                                              State_Prov_Id,
                                                                                              City_Id, Office_Id,
                                                                                              Case_Seq_No,
                                                                                              Hist_Seq_No,
                                                                                              Contact_Id,
                                                                                              languageId: LanguageId
                                                                                            ).ToList();
            CitizenQuestionList.ForEach(r => { if (r.CitizenQuestAnswer == null) r.CitizenQuestAnswer = false; });
            return CitizenQuestionList;
        }
        #endregion

        #region Credit Score Revision
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="DefaultPass"></param>
        /// <returns></returns>
        public wsTransunion.TransunionServiceClient TransunionServiceLogIn(string user, string pass, string DefaultPass)
        {
            Transunion.EncryptDecrypt.UserNamePasswordEncrypting en;
            wsTransunion.TransunionServiceClient client;

            client = new wsTransunion.TransunionServiceClient();

            en = Transunion.EncryptDecrypt.Encrypting.UserNamePasswordEncrypt(user, pass, DefaultPass);

            user = en.UserName;
            pass = en.Password;

            client.ClientCredentials.UserName.UserName = user;
            client.ClientCredentials.UserName.Password = pass;

            return
                client;
        }

        public string TransunionUser
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.TransunionUser; }
            set
            {
                datos.ContactInfo.TransunionUser = value;
                datos.Save();
            }
        }
        public string TransunionPass
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.TransunionPass; }
            set
            {
                datos.ContactInfo.TransunionPass = value;
                datos.Save();
            }
        }
        public string TransunionDefaultPassword
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.TransunionDefaultPassword; }
            set
            {
                datos.ContactInfo.TransunionDefaultPassword = value;
                datos.Save();
            }
        }
        /// <summary>
        /// Nombre del Usuario
        /// </summary>        

        public string UserName
        {
            get { return (HttpContext.Current.Session[key] as SessionList).Stored.ContactInfo.UserName; }
            set
            {
                datos.ContactInfo.UserName = value;
                datos.Save();
            }
        }
        #endregion

        #region OnBase Document

        public void SendFileToOnBase(Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation add, string IndexFileTemplateLocation, string DocumentType, int catid, int typeid, string path)
        {
             bool executeProcess;

            try
            {
                var vLifeOnBaseAvailable = ConfigurationManager.AppSettings["LifeOnBaseAvailable"];
                bool.TryParse(vLifeOnBaseAvailable, out executeProcess);
            }
            catch (Exception) 
            { 
                executeProcess = false; 
            }

            if (executeProcess)
            {
                var DocumentRequirement = RequirementManager.GetRequirementDocumentOnBase(DocumentType, Corp_Id, catid, typeid);

                if (DocumentRequirement.On_Base_Name_Key != null)
                {
                    OnBaseFile OBF = new OnBaseFile();

                    var plandata = PolicyManager.GetPlanData(
                         add.CorpId,
                         add.RegionId,
                         add.CountryId,
                         add.DomesticregId,
                         add.StateProvId,
                         add.CityId,
                         add.OfficeId,
                         add.CaseSeqNo,
                         add.HistSeqNo);

                    FileItem File = new FileItem();

                    File.DocTypeName = DocumentRequirement.On_Base_Name_Key;
                    File.CreatedDate = string.Format("{0:MM/dd/yyyy}", DateTime.Now.ToShortDateString());
                    File.OnBaseFileFormat = System.Configuration.ConfigurationManager.AppSettings["LifeOnBaseFileFormat"];
                    File.EsVersion = 1;
                    File.Plan = plandata.PlanName;
                    File.Product = string.Empty;
                    File.NUP = add.CaseSeqNo;
                    File.PolicyNo = plandata.PolicyNo;

                    if (catid == 2)
                    {
                        if (DocumentRequirement.Clasification == "Examen")
                        {
                            File.TypeMedicalExamination = DocumentRequirement.DescriptionName;
                        }

                        if (DocumentRequirement.Clasification == "Formulario")
                        {
                            File.TypeMedicalForm = DocumentRequirement.DescriptionName;
                        }
                    }

                    if (catid == 3)
                    {
                        File.TypeFinancialForm = DocumentRequirement.DescriptionName;
                    }

                    if (catid == 4)
                    {
                        File.TypeFormActivity = DocumentRequirement.DescriptionName;
                    }

                    if (catid == 5)
                    {
                        File.OccupationalFormType = DocumentRequirement.DescriptionName;
                    }

                    if (add.Contact_ID != null)
                    {
                        var contact = ContactManager.GetContact(add.CorpId, add.Contact_ID.Value, 2);

                        File.insured = contact.FullName;
                        File.InsuredNumber = contact.ContactId;
                        File.IdentificationNo = contact.Id;
                        File.RoleType = contact.ContactTypeId;

                        if (File.DocTypeName == "VIDSUS-Cedula")
                        {
                            if (contact.ContactIdType == 2)
                            {
                                File.DocTypeName = "VIDSUS-Pasaporte";
                            } 
                            else if (contact.ContactIdType == 3)
                            {
                                File.DocTypeName = "VIDSUS-Licencia";
                            }
                            else if (contact.ContactIdType != 1)
                            {
                                File.DocTypeName = "VIDSUS-ID"; 
                            }
                        }
                    }
                    else
                    {
                        //Esto para los beneficiarios ya que todavia no estan generados los contact id de estos.

                        File.insured = add.InsuredName.Trim();
                        File.IdentificationNo = add.identification;
                        File.RoleType = add.Role_Type_ID.Value;

                        if (File.DocTypeName == "VIDSUS-Cedula")
                        {
                            if (add.ContactIdType == 2)
                            {
                                File.DocTypeName = "VIDSUS-Pasaporte";
                            }
                            else if (add.ContactIdType == 3)
                            {
                                File.DocTypeName = "VIDSUS-Licencia";
                            }
                            else if (add.ContactIdType != 1)
                            {
                                File.DocTypeName = "VIDSUS-ID";
                            }
                        }
                    }

                    File.EndDate = string.Empty;
                    File.BeginDate = string.Empty;
                    File.Source = "OIPA";
                    File.FullPath = path;
                                             
                    Guid FilesName = Guid.NewGuid();
                    //string IndexFileTemplateLocation = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LifeOnBaseTemplatePath"]);
                    string IndexFileTemplate = System.Configuration.ConfigurationManager.AppSettings["LifeOnBaseTemplateFile"];
                    string OnBaseRemotePath = System.Configuration.ConfigurationManager.AppSettings["LifeOnBaseServerPath"];
                    string currentIdxFile = OBF.CreateIndexFile(FilesName, OnBaseRemotePath);

                    File.FullPath = OBF.CopyFileToServer(FilesName, File.FullPath, OnBaseRemotePath);
                    OBF.ReplaceIndexFileWithTemplate(IndexFileTemplateLocation, IndexFileTemplate, currentIdxFile, File);
                }
                else
                {
                    if (DocumentType == "R")
                    {
                        throw new ArgumentException(string.Format(@"No se encontro el On_Base_Name_Key en la tabla [Policy].[PL_REQUIREMENT_TYPE] 
                                              con los parametros Corp_id = {0}, Requirement_Cat_Id = {1}, Requirement_Type_Id = {2}, 
                                              se utilizo la subida del sistema original.", Corp_Id.ToString(), catid.ToString(), typeid.ToString()));

                    }
                    else
                    {
                        throw new ArgumentException(string.Format(@"No se encontro el On_Base_Name_Key en la tabla [Documents].[DOCUMENT_CATEGORY]
                                              con los parametros Cat_Id = {1}, Type_Id = {2}, 
                                              se utilizo la subida del sistema original.", catid.ToString(), typeid.ToString()));
                    }   
                }
            }
        }

        public byte[] ViewFileFromOnBase(Entity.UnderWriting.Entities.Requirement.OnBaseAditionalInformation add, string DocumentType, int catid, int typeid)
        {
            byte[] pdfOnBase = null;

              bool executeProcess;

            try
            {
                var vLifeOnBaseAvailable = ConfigurationManager.AppSettings["LifeOnBaseAvailable"];
                bool.TryParse(vLifeOnBaseAvailable, out executeProcess);
            }
            catch (Exception)
            {
                executeProcess = false;
            }

            if (executeProcess)
            {

                var DocumentRequirement = RequirementManager.GetRequirementDocumentOnBase(DocumentType, Corp_Id, catid, typeid);

                if (DocumentRequirement.On_Base_Name_Key != null)
                {
                    try
                    {
                        if (add.Contact_ID != null)
                        {
                            var plandata = PolicyManager.GetPlanData(
                                            add.CorpId,
                                            add.RegionId,
                                            add.CountryId,
                                            add.DomesticregId,
                                            add.StateProvId,
                                            add.CityId,
                                            add.OfficeId,
                                            add.CaseSeqNo,
                                            add.HistSeqNo);

                            var contact = ContactManager.GetContact(Corp_Id, add.Contact_ID.Value, 2);

                            OnBaseSearchDocument.CustomQueryKeywordsGetDocument ParametrosBusqueda = new OnBaseSearchDocument.CustomQueryKeywordsGetDocument();

                            ParametrosBusqueda.NombredelAsegurado = contact.FullName.Trim();
                            ParametrosBusqueda.NúmerodePóliza = plandata.PolicyNo;
                            ParametrosBusqueda.Plan = plandata.PlanName;  
                            ParametrosBusqueda.NúmerodeAsegurado = add.Contact_ID.ToString();

                            if (DocumentRequirement.On_Base_Name_Key == "VIDSUS-Cedula")
                            {   
                                if (contact.ContactIdType == 2)
                                {
                                    DocumentRequirement.On_Base_Name_Key = "VIDSUS-Pasaporte";
                                }
                                else if (contact.ContactIdType == 3)
                                {
                                    DocumentRequirement.On_Base_Name_Key = "VIDSUS-Licencia";
                                }
                                else if (contact.ContactIdType != 1)
                                {
                                    DocumentRequirement.On_Base_Name_Key = "VIDSUS-ID";
                                }
                            }

                            if (catid == 2 && DocumentType == "R")
                            {
                                if (DocumentRequirement.Clasification == "Examen")
                                {
                                    ParametrosBusqueda.TipodeExamenMedico = DocumentRequirement.DescriptionName;
                                }

                                if (DocumentRequirement.Clasification == "Formulario")
                                {
                                    ParametrosBusqueda.TipodeFormularioMedico = DocumentRequirement.DescriptionName;
                                }
                            }

                            if (catid == 3 && DocumentType == "R")
                            {
                                ParametrosBusqueda.TipodeFormularioFinanciero = DocumentRequirement.DescriptionName;
                            }

                            if (catid == 4 && DocumentType == "R")
                            {
                                ParametrosBusqueda.TipodeFormularioActividad = DocumentRequirement.DescriptionName;
                            }

                            if (catid == 5 && DocumentType == "R")
                            {
                                ParametrosBusqueda.TipodeFormularioOcupacional = DocumentRequirement.DescriptionName;
                            }

                            OnBaseSearchDocument.OBCustomQueryGetDocument querydata = new OnBaseSearchDocument.OBCustomQueryGetDocument()
                            {
                                Keywords = ParametrosBusqueda
                            };

                            OnBaseSearchDocument.HylandOutBoundContractClient service = new OnBaseSearchDocument.HylandOutBoundContractClient();
                            OnBaseSearchDocument.CustomQueryDispColResponseGetDocument datos = service.GetDocument(querydata);

                            OnBaseSearchDocument.Document[] doc = datos.DocumentResults;

                            var DocumentFilterByNameKey = doc.Where(x => x.docTypeName.ToLower() == DocumentRequirement.On_Base_Name_Key.ToLower() && x.DisplayColumns.NombredelAsegurado.ToLower() == contact.FullName.ToLower());
                                                                   
                            int cantDocument = DocumentFilterByNameKey.Count();

                            if (cantDocument > 0)
                            {
                                string dochandle = DocumentFilterByNameKey.LastOrDefault().documentHandle;

                                OnBaseDownloadDocument.DocumentBytesInput dquerydata = new OnBaseDownloadDocument.DocumentBytesInput()
                                {
                                    documentHandle = dochandle
                                };

                                OnBaseDownloadDocument.HylandOutBoundContractClient ServiceDownload = new OnBaseDownloadDocument.HylandOutBoundContractClient();
                                var Result = ServiceDownload.Get_document_data(dquerydata);

                                pdfOnBase = Convert.FromBase64String(Result.Base64FileStream);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        PolicyManager.InsertLog(new Entity.UnderWriting.Entities.Policy.LogParameter
                        {
                            LogTypeId = 3,
                            CorpId = Corp_Id,
                            CompanyId = CompanyId,
                            ProjectId = ProjectId,
                            Identifier = Guid.NewGuid(),
                            LogValue = "Se encontro un problema con el proceso OnBaseTranfer al momento de ver el documento, Detalle: " + ex.Message.ToString()
                        });
                    }
                }
            }
            return pdfOnBase;
        }

        #endregion

        #region Gettings All Drops
        private IEnumerable<DropDown> getDropDown(Entity.UnderWriting.Entities.DropDown.Parameter param)
        {
            return DropDownManager.GetDropDownByType(param);
        }
        public IEnumerable<DropDown> GettingDropData(
                                                      DropDownType dropDownType,
                                                      int? corpId = null,
                                                      int? regionId = null,
                                                      int? countryId = null,
                                                      int? domesticregId = null,
                                                      int? stateProvId = null,
                                                      int? cityId = null,
                                                      int? officeId = null,
                                                      int? caseSeqNo = null,
                                                      int? histSeqNo = null,
                                                      int? contactId = null,
                                                      int? agentId = null,
                                                      bool? isInsured = null,
                                                      int? occupGroupTypeId = null,
                                                      int? requirementCategory = null,
                                                      int? requirementType = null,
                                                      int? BlTypeId = null,
                                                      int? BlId = null,
                                                      int? PaymentSourceId = null,
                                                      int? PaymentSourceTypeId = null,
                                                      int? currencyId = null,
                                                      int? ScaleTypeId = null,
                                                      string abaNumber = null,
                                                      bool? appliedByFreqOrCountry = null,
                                                      int? ProductTypeId = null,
                                                      int? ProviderId = null,
                                                      int? pProjectId = null,
                                                      int? DeductibleTypeId = null,
                                                      String NameKey = null
                                             )
        {

            var parameter = new DropDown.Parameter
            {
                DropDownType = Enum.GetName(typeof(DropDownType), dropDownType),
                CorpId = this.Corp_Id,
                RegionId = regionId,
                CountryId = countryId,
                DomesticregId = domesticregId,
                StateProvId = stateProvId,
                CityId = cityId,
                OfficeId = officeId,
                CaseSeqNo = caseSeqNo,
                HistSeqNo = histSeqNo,
                ContactId = contactId,
                AgentId = agentId,
                IsInsured = isInsured,
                OccupGroupTypeId = occupGroupTypeId,
                RequirementCatId = requirementCategory,
                BlTypeId = BlTypeId,
                BlId = BlId,
                CurrencyId = currencyId,
                AbaNumber = abaNumber,
                AppliedByFreqOrCountry = appliedByFreqOrCountry,
                ProductTypeId = ProductTypeId,
                CompanyId = this.CompanyId,
                DeductibleTypeId = DeductibleTypeId,
                PaymentSourceId = PaymentSourceId,
                PaymentSourceTypeId = PaymentSourceTypeId,
                NameKey = NameKey,
                LanguageId = this.LanguageId.ToInt(),
                ProjectId = pProjectId,
                ProviderId = ProviderId
            };

            var data = getDropDown(parameter);
            return data;

        }
        #endregion
    }
}