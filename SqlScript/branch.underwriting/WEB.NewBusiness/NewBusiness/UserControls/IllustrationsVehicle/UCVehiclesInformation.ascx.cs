﻿using DevExpress.Web;
using Entity.UnderWriting.Entities;
using iTextSharp.text.pdf;
using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle
{
    public partial class UCVehiclesInformation : UC, IUC
    {
        public delegate void ExportToPDFHandler(byte[] pdfFile, string FileName);
        public event ExportToPDFHandler ExportToPdf;

        private string _totalInsuredAmount;
        private string _totalDeductible;
        private string _totalPremiumAmount;
        private AcroFields _addressChangeForm;
        private string IllustrationStatusCode
        {
            get
            {
                return ((WEB.NewBusiness.NewBusiness.Pages.IllustrationsVehicle)Page).IllustrationStatusCode;
            }
        }

        public void ExportPDF(byte[] pdfFile, string FileName)
        {
            ExportToPdf(pdfFile, FileName);
        }

        public List<Policy.VehicleInsured> ListVehicles
        {
            get
            {
                var val = ViewState["ListVehicles"];
                return val == null ? new List<Policy.VehicleInsured>() : val.ToString().FromJsonToObject<List<Policy.VehicleInsured>>();
            }

            private set
            {
                ViewState["ListVehicles"] = value.ToJSON();
            }
        }

        private string[] Estados
        {
            get
            {
                return new string[]
                {
                    Utility.IllustrationStatus.Effective.Code(),
                    Utility.IllustrationStatus.TimeExpired.Code(),
                    Utility.IllustrationStatus.DeclinedByClient.Code(),
                    Utility.IllustrationStatus.DeclinedBySubscription.Code(),
                    Utility.IllustrationStatus.ApprovedBySubscription.Code()
                };
            }
        }

        public bool EnableTag
        {
            get
            {
                return ViewState["EnableTag"].ToBoolean();
            }
            set
            {
                ViewState["EnableTag"] = value;
            }
        }

        public string DriverName
        {
            get
            {
                return ViewState["DriverName"].ToString();
            }
            set
            {
                ViewState["DriverName"] = value;
            }
        }

        public bool CanEnableInspection
        {
            get
            {
                return ViewState["CanEnableInspection"].ToBoolean();
            }
            set
            {
                ViewState["CanEnableInspection"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Translator(string.Empty);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pdfViewerMyPreviewPDF.LicenseKey = System.Configuration.ConfigurationManager.AppSettings["PDFViewer"];
            UCEndosoCesion.ExportToPdf += ExportPDF;
            WUCVechicleEditForm.BindGrid += FillData;

            if (hdnEndosoPopup.Value == "true")
                ModalPopupEndoso.Show();


            if (hdnInspectionAddressPopup.Value == "true")
            {
                this.ExcecuteJScript("initializeSubmitMap(document.getElementById('map'),null,true,true,true);");
                ModalPopupInspectionAddress.Show();
            }

            if (hdnPopTBSiniestralidad.Value == "true")
                ModalPopupTBSiniestralidad.Show();

            if (IsPostBack) return;
        }

        public void Translator(string Lang)
        {
            Title.Text = RESOURCE.UnderWriting.NewBussiness.Resources.Coverage;
            btnSaveAddress.Text = RESOURCE.UnderWriting.NewBussiness.Resources.Save;
        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public void edit()
        {
            throw new NotImplementedException();
        }

        public void FillData()
        {
            FillData("", this.EnableTag, this.CanEnableInspection);                    

        }

        public void FillData(string driverName, bool enableTag, bool canEnableInspection)
        {
            var lstVehicles = ObjServices.oPolicyManager.GetVehicleInsured(new Policy.Parameter
            {
                CorpId = ObjServices.Corp_Id,
                RegionId = ObjServices.Region_Id,
                CountryId = ObjServices.Country_Id,
                DomesticregId = ObjServices.Domesticreg_Id,
                StateProvId = ObjServices.State_Prov_Id,
                CityId = ObjServices.City_Id,
                OfficeId = ObjServices.Office_Id,
                CaseSeqNo = ObjServices.Case_Seq_No,
                HistSeqNo = ObjServices.Hist_Seq_No
            }).ToList();

            lstVehicles.ForEach(o => o.DriverFullName = o.DriverFullName.SIsNullOrEmpty() ? driverName : o.DriverFullName);

            _totalDeductible = lstVehicles.Sum(o => o.DeductiblePercentage.GetValueOrDefault()).Truncate() + "%";
            _totalPremiumAmount = lstVehicles.Sum(o => o.PremiumAmount).ToFormatCurrency();
            _totalInsuredAmount = lstVehicles.Sum(o => o.VehicleValue).ToFormatCurrency();

            hdntotalInsuredAmount.Value = _totalInsuredAmount;
            hdntotalDeductible.Value = _totalDeductible;
            hdntotalPremiumAmount.Value = _totalPremiumAmount;

            EnableTag = enableTag;
            DriverName = driverName;
            CanEnableInspection = canEnableInspection;

            ListVehicles = lstVehicles;
            BindGrid();

            var BlackListCol = gvVehicle.getThisColumn("BlackList");
            if (BlackListCol != null)
                BlackListCol.Visible = ObjServices.BlackListHasProblem;
        }

        private void BindGrid()
        {
            bool vAvaible = true;

            var isEffectivePolicy = ObjServices.StatusNameKey == Utility.IllustrationStatus.Effective.Code() ||
                                    ObjServices.StatusNameKey == Utility.IllustrationStatus.Cancelled.Code()
                                    ;

            //Si la poliza esta efectiva deshabilitar
            if (isEffectivePolicy)
                vAvaible = false;
            //Si el usuario es un inspector y no es un agente deshabilitar
            else if ((ObjServices.IsInspectorQuotRole && !ObjServices.IsAgentQuotRole))
                vAvaible = false;

            string CssClassEndorsement = vAvaible && isEffectivePolicy ? "view_file" : "hasdocument_False",
                   CssClassMarbete = EnableTag ? "view_file" : "hasdocument_False";

            var data = ListVehicles.OrderBy(o => o.InsuredVehicleId).Select(o => new
            {
                Deductible = o.DeductiblePercentage.GetValueOrDefault().Truncate() + "%",
                Driver = o.DriverFullName,
                InsuredAmount = o.VehicleValue.ToFormatCurrency(),
                o.VehicleUniqueId,
                o.InsuredVehicleId,
                o.MakeDesc,
                o.ModelDesc,
                o.StoredDesc,
                o.Registry,
                o.Chassis,
                o.VehicleTypeDesc,
                o.VehicleCapacity,
                PlanName = o.ProductTypeDesc,
                PremiumAmount = o.PremiumAmount.ToFormatCurrency(),
                Rate = o.VehicleValue > 1 ? ((o.PremiumAmount / o.VehicleValue) * 100).ToFormatNumeric() + "%" : "-",
                o.UsageDesc,
                o.Year,
                EnableTag = EnableTag,
                EnableInspection = false,
                Inspection = o.Inspection.GetValueOrDefault(),
                o.New,
                EndorsementAmount = o.EndorsementAmount.GetValueOrDefault(),
                EndorsementBeneficiary = o.EndorsementBeneficiary,
                EndorsementBeneficiaryRnc = o.EndorsementBbeneficiaryRnc,
                EndorsementClarifying = o.EndorsementClarifying && isEffectivePolicy,
                ClassEndoso = (o.EndorsementAmount.HasValue && !isEffectivePolicy) ? "myedit_file"
                                                                                   : (o.EndorsementAmount.HasValue && isEffectivePolicy) ? "myedit_file_dis"
                                                                                                                                         : (vAvaible ? "myinsert_file" : "myinsert_file_dis"),
                o.HasOwnDamage,
                available = vAvaible,
                availableEndorsementOfTransferOfRight = (o.EndorsementAmount.HasValue && isEffectivePolicy),
                CssClassEndorsementOfTransferOfRight = (o.EndorsementAmount.HasValue && isEffectivePolicy) ? "view_file" : "hasdocument_False",
                CssClassEndorsement = CssClassEndorsement,
                CssClassMarbete = CssClassMarbete,
                CssClassEndorsementClarifying = o.EndorsementClarifying && isEffectivePolicy ? "view_file" : "hasdocument_False",
                CssClassInspection = o.Inspection.GetValueOrDefault() ? "view_file" : "hasdocument_False",
                CssInspectionAddress = enabledInspectionAddress(o.InspectionAddress, o.InspectionRequired) ? cssInspectionAddress(o.InspectionAddress, o.InspectionRequired) : "myedit_file_dis",
                EnabledInspectionAddress = enabledInspectionAddress(o.InspectionAddress, o.InspectionRequired),
                VisibleChkInspection = !o.IsInspected,
                VisibleLnkInspeccion = o.IsInspected,
                Color = o.ColorDesc,
                SIModelo = string.Format(CultureInfo.InvariantCulture, "{0:0.00}" + "%", o.AccidentRate.HasValue ? o.AccidentRate : 0),
                fuelTypeDesc = Utility.getFuelTypeDesc(o.rateJsonSysflex)
            });

            gvVehicle.DatabindAspxGridView(data.ToList());

            var TabSelected = (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), ObjServices.hdnQuotationTabs);
            var isVisible = (TabSelected == Utility.Tabs.lnkSubscriptions) && (ObjServices.IsSuscripcionQuotRole ||
                                                                               ObjServices.IsSuscripcionManagerQuotRole ||
                                                                               ObjServices.IsSucripcionDirectorQuotRole ||
                                                                               ObjServices.isUserCot);
            //Ocultar columnas de sinistralidad
            gvVehicle.HideOrShowColumnGrid(isVisible, "TBSiniestralidad");
            gvVehicle.HideOrShowColumnGrid(isVisible, "SIModelo");   
        }

        public void Initialize()
        {               
            ClearData();
            FillData();
        }

        public void ClearData()
        {

        }

        private List<Policy.VehicleCoverage> GetVehicleCoverage(long? vehicleUniqueId)
        {
            if (!vehicleUniqueId.HasValue) return new List<Policy.VehicleCoverage>();

            return ObjServices.oPolicyManager.GetVehicleCoverage(new Policy.VehicleCoverageGet
            {
                CorpId = ObjServices.Corp_Id,
                VehicleUniqueId = vehicleUniqueId.GetValueOrDefault()
            }).ToList();
        }

        protected void UpdatePanel5_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
           .Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }

        protected void btnSaveCoverage_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RepeaterCoveragesThirdDamage.Items)
            {
                var checkBox1 = (CheckBox)item.Controls[3];

                var objModified = TransformarVehicleCoverage(item);

                objModified.CoverageStatus = ((checkBox1.Checked) ? 1 : 0).ToInt();

                ObjServices.oPolicyManager.SetVehicleCoverage(objModified);
            }
        }

        private Policy.VehicleCoverage TransformarVehicleCoverage(RepeaterItem item)
        {
            var objModified = new Policy.VehicleCoverage();
            var textKeys = (HiddenField)item.Controls[1];
            string valoresSinRN = textKeys.Value.Replace(System.Environment.NewLine, "").Replace(" ", "").Replace('"', ' ').Replace("{", "").Replace("}", "");
            var valores = valoresSinRN.Split(';').ToList();
            foreach (var linea in valores)
            {
                var nombre = linea.Split(':').First().Trim();
                var valor = linea.Split(':').Last().Trim();
                foreach (PropertyInfo prop in typeof(Policy.VehicleCoverage).GetProperties())
                {
                    if (nombre == prop.Name)
                    {
                        if (prop.PropertyType == typeof(Int32) || prop.PropertyType == typeof(Nullable<Int32>))
                        {
                            prop.SetValue(objModified, Convert.ToInt32(valor));
                        }
                        else if (prop.PropertyType == typeof(Int64) || prop.PropertyType == typeof(Nullable<Int64>))
                        {
                            prop.SetValue(objModified, Convert.ToInt64(valor));
                        }
                        else if (prop.PropertyType == typeof(Decimal) || prop.PropertyType == typeof(Nullable<Decimal>))
                        {
                            decimal temp = 0;
                            Decimal.TryParse(valor, out temp);
                            prop.SetValue(objModified, temp);
                        }
                        else
                        {
                            prop.SetValue(objModified, valor);
                        }
                    }
                }
            }

            return objModified;
        }

        protected void gvVehicle_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            var ServerMaptPathXML = Server.MapPath("~/NewBusiness/XML/");
            byte[] XMLByteArray;
            byte[] PDFByteArray;
            var index = e.VisibleIndex;

            FillData(DriverName, EnableTag, CanEnableInspection);

            var vehicle = ListVehicles[index];

            switch (e.CommandArgs.CommandName)
            {
                case "BlackList":
                    if (!ObjServices.IsValidateBlackListCot)
                    {
                        this.MessageBox(Resources.BlackListUserValidateAlert);
                        return;
                    }
                    hdnPopBlackList.Value = "true";
                    WUCBlackListValidation.Initialize();
                    ModalPopupBlackList.Show();
                    break;
                case "Endoso":
                    #region Endoso
                    UCEndosoCesion.Initialize();
                    UCEndosoCesion.VehicleValue = vehicle.VehicleValue;
                    UCEndosoCesion.InsuredVehicleId = vehicle.InsuredVehicleId;
                    UCEndosoCesion.VehicleUniqueId = vehicle.VehicleUniqueId;

                    var itemEndorsment = new Utility.EndorstmentData
                    {
                        EndorsementBeneficiary = vehicle.EndorsementBeneficiary,
                        EndorsementBbeneficiaryRnc = vehicle.EndorsementBbeneficiaryRnc,
                        EndorsementAmount = vehicle.EndorsementAmount.HasValue ? vehicle.EndorsementAmount.Value : 0,
                        EndorsementContactName = vehicle.EndorsementContactName,
                        EndorsementContactPhone = vehicle.EndorsementContactPhone,
                        EndorsementContactEmail = vehicle.EndorsementContactEmail
                    };

                    UCEndosoCesion.FillData(itemEndorsment);
                    hdnEndosoPopup.Value = "true";
                    ModalPopupEndoso.Show();
                    #endregion
                    break;
                case "EndosoAclaratorio":
                    //Mostrar el Endoso Aclaratorio generado via thunderhead                    
                    #region Endoso Aclaratorio
                    XMLByteArray = ObjServices.GenerateXMLQuotationToThunderhead(ObjServices.Corp_Id,
                                                                                 ObjServices.Region_Id,
                                                                                 ObjServices.Country_Id,
                                                                                 ObjServices.Domesticreg_Id,
                                                                                 ObjServices.State_Prov_Id,
                                                                                 ObjServices.City_Id,
                                                                                 ObjServices.Office_Id,
                                                                                 ObjServices.Case_Seq_No,
                                                                                 ObjServices.Hist_Seq_No,
                                                                                 ServerMaptPathXML,
                                                                                 vehicleUniqueID: vehicle.VehicleUniqueId,
                                                                                 templateType: ThunderheadWrap.Service.TemplateType.EndosoAclaratorio
                                                                                );

                    PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, ThunderheadWrap.Service.TemplateType.EndosoAclaratorio);
                    pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                    pdfViewerMyPreviewPDF.DataBind();
                    hdnShowPDF.Value = "true";
                    ModalPopupShowPDF.Show();
                    #endregion
                    break;
                case "particularsconditions":
                    //Mostrar el Condiciones particulares generado via thunderhead                    
                    #region Condiciones Particulares
                    XMLByteArray = ObjServices.GenerateXMLMarbeteCondicionadoToThunderhead(ObjServices.Corp_Id,
                                                                                           ObjServices.Region_Id,
                                                                                           ObjServices.Country_Id,
                                                                                           ObjServices.Domesticreg_Id,
                                                                                           ObjServices.State_Prov_Id,
                                                                                           ObjServices.City_Id,
                                                                                           ObjServices.Office_Id,
                                                                                           ObjServices.Case_Seq_No,
                                                                                           ObjServices.Hist_Seq_No,
                                                                                           ServerMaptPathXML,
                                                                                           ThunderheadWrap.Service.TemplateType.CondicionesParticulares.ToString(),
                                                                                           vehicle.VehicleUniqueId
                                                                                           );

                    PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, ThunderheadWrap.Service.TemplateType.CondicionesParticulares);
                    pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                    pdfViewerMyPreviewPDF.DataBind();
                    hdnShowPDF.Value = "true";
                    ModalPopupShowPDF.Show();
                    #endregion
                    break;
                case "tag":
                    //Mostrar el marbete generado via thunderhead                    
                    #region Tag
                    XMLByteArray = ObjServices.GenerateXMLMarbeteCondicionadoToThunderhead(ObjServices.Corp_Id,
                                                                                            ObjServices.Region_Id,
                                                                                            ObjServices.Country_Id,
                                                                                            ObjServices.Domesticreg_Id,
                                                                                            ObjServices.State_Prov_Id,
                                                                                            ObjServices.City_Id,
                                                                                            ObjServices.Office_Id,
                                                                                            ObjServices.Case_Seq_No,
                                                                                            ObjServices.Hist_Seq_No,
                                                                                            ServerMaptPathXML,
                                                                                            ThunderheadWrap.Service.TemplateType.Marbete.ToString(),
                                                                                            vehicle.VehicleUniqueId
                                                                                           );

                    PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, ThunderheadWrap.Service.TemplateType.Marbete);
                    pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                    pdfViewerMyPreviewPDF.DataBind();
                    hdnShowPDF.Value = "true";
                    ModalPopupShowPDF.Show();
                    #endregion
                    break;
                case "EndosoCesionDerecho":
                    //Mostrar el endoso de cesion de derechos generado via thunderhead                    
                    #region Endoso de Cesion de Derecho
                    XMLByteArray = ObjServices.GenerateXMLQuotationToThunderhead(ObjServices.Corp_Id,
                                                                                 ObjServices.Region_Id,
                                                                                 ObjServices.Country_Id,
                                                                                 ObjServices.Domesticreg_Id,
                                                                                 ObjServices.State_Prov_Id,
                                                                                 ObjServices.City_Id,
                                                                                 ObjServices.Office_Id,
                                                                                 ObjServices.Case_Seq_No,
                                                                                 ObjServices.Hist_Seq_No,
                                                                                 ServerMaptPathXML,
                                                                                 vehicleUniqueID: vehicle.VehicleUniqueId,
                                                                                 templateType: ThunderheadWrap.Service.TemplateType.EndosoCesionDerecho
                                                                                );

                    PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, ThunderheadWrap.Service.TemplateType.EndosoCesionDerecho);
                    pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                    pdfViewerMyPreviewPDF.DataBind();

                    //Visualizar el pdf 
                    hdnShowPDF.Value = "true";
                    ModalPopupShowPDF.Show();
                    this.ExcecuteJScript("$('#popupBhvr_backgroundElement').css('display', 'none');");
                    #endregion
                    break;
                case "conditioned":
                    //Codicionado
                    //Mostrar el Condicionado generado via thunderhead                    
                    #region Condicionado
                    XMLByteArray = ObjServices.GenerateXMLMarbeteCondicionadoToThunderhead(ObjServices.Corp_Id,
                                                                                           ObjServices.Region_Id,
                                                                                           ObjServices.Country_Id,
                                                                                           ObjServices.Domesticreg_Id,
                                                                                           ObjServices.State_Prov_Id,
                                                                                           ObjServices.City_Id,
                                                                                           ObjServices.Office_Id,
                                                                                           ObjServices.Case_Seq_No,
                                                                                           ObjServices.Hist_Seq_No,
                                                                                           ServerMaptPathXML,
                                                                                           ThunderheadWrap.Service.TemplateType.Condicionado.ToString(),
                                                                                           vehicle.VehicleUniqueId
                                                                                           );

                    PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, ThunderheadWrap.Service.TemplateType.Condicionado);
                    pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                    pdfViewerMyPreviewPDF.DataBind();
                    hdnShowPDF.Value = "true";
                    ModalPopupShowPDF.Show();
                    #endregion
                    break;
                case "Coverage":
                    #region Cobertura
                    try
                    {
                        var dataCoverage = ObjServices.oPolicyManager.GetVehicleCoverage(new Policy.VehicleCoverageGet
                        {
                            CorpId = vehicle.CorpId,
                            VehicleUniqueId = vehicle.VehicleUniqueId
                        }).Select(o => new
                        {
                            o.CorpId,
                            o.RegionId,
                            o.CountryId,
                            o.VehicleUniqueId,
                            o.BlTypeId,
                            o.BlId,
                            o.ProductId,
                            o.VehicleTypeId,
                            o.GroupId,
                            o.CoverageTypeId,
                            o.CoverageTypeDesc,
                            o.CoverageId,
                            o.CurrencyId,
                            o.UnitaryPrice,
                            o.PremiumPercentage,
                            o.PackagePrice,
                            o.CoverageStatus,
                            o.UserId,
                            o.CoverageNameKey,
                            o.CoverageDesc,
                            o.DeductibleAmount,
                            DeductiblePercentage = o.DeductiblePercentage.GetValueOrDefault().ToFormatNumeric() + "%",
                            o.ManualDeductibleAmount,
                            o.ManualDeductiblePercentage,
                            o.CoverageLimit,
                            Activo = Convert.ToBoolean(o.CoverageStatus),
                            DatoJson = "CorpId:" + o.CorpId.ToString() +
                                       ";CoverageId:" + o.CoverageId.ToString() +
                                       ";RegionId:" + o.RegionId.ToString() +
                                       ";CountryId:" + o.CountryId.ToString() +
                                       ";VehicleUniqueId:" + o.VehicleUniqueId.ToString() +
                                       ";BlTypeId:" + o.BlTypeId.ToString() +
                                       ";BlId:" + o.BlId.ToString() +
                                       ";ProductId:" + o.ProductId.ToString() +
                                       ";VehicleTypeId:" + o.VehicleTypeId.ToString() +
                                       ";GroupId:" + o.GroupId.ToString() +
                                       ";CoverageTypeId:" + o.CoverageTypeId.ToString() +
                                       ";CurrencyId:" + o.CurrencyId.ToString() + ";",
                            isVisiblePrimeAndRate = ObjServices.IsViewPrimeAndRateCot ? "'display:block'" : "'display:none'"

                        });

                        var ThirdDamage = dataCoverage.Where(x => x.CoverageTypeId == 1).Distinct(); //Daños a terceros
                        var SelfDamage = dataCoverage.Where(x => x.CoverageTypeId == 2).Distinct().OrderBy(x=>x.CoverageDesc);  //Daños Propios
                        var Additional = dataCoverage.Where(x => x.CoverageTypeId == 3).Distinct();  //Servicios adicionales


                        pnThirDamage.Visible = ThirdDamage.Any();

                        pnSelfDamage.Visible = SelfDamage.Any();
                        pnServices.Visible = Additional.Any();

                        Translator(string.Empty);
                        RepeaterCoveragesThirdDamage.DataSource = ThirdDamage;
                        RepeaterCoveragesThirdDamage.DataBind();

                        RepeaterCoveragesSelfDamage.DataSource = SelfDamage;
                        RepeaterCoveragesSelfDamage.DataBind();

                        RepeaterCoveragesAdditional.DataSource = Additional;
                        RepeaterCoveragesAdditional.DataBind();

                        ModalPopupCoverage.Show();
                        hdnShowPopCoverage.Value = "true";
                    }
                    catch (Exception ex)
                    {
                        this.MessageBox(string.Format("ExceptionMessage {0} TraceStack {1}", ex.Message, ex.StackTrace).RemoveInvalidCharacters());
                    }
                    #endregion
                    break;
                case "Inspeccion":
                    //Mostrar la Inspeccion generado via thunderhead                    
                    #region Inspeccion
                    XMLByteArray = ObjServices.GenerateXMLVIFToThunderhead(ObjServices.Corp_Id,
                                                                           ObjServices.Region_Id,
                                                                           ObjServices.Country_Id,
                                                                           ObjServices.Domesticreg_Id,
                                                                           ObjServices.State_Prov_Id,
                                                                           ObjServices.City_Id,
                                                                           ObjServices.Office_Id,
                                                                           ObjServices.Case_Seq_No,
                                                                           ObjServices.Hist_Seq_No,
                                                                           vehicle.VehicleUniqueId,
                                                                           ServerMaptPathXML);

                    string msg = ASCIIEncoding.ASCII.GetString(XMLByteArray);
                    if (msg == Resources.VehicleDontHasInspection || msg == Resources.QuotationDontHasVehicle)
                        this.MessageBox(msg, null, null, true, Resources.InformationLabel);
                    else
                    {
                        PDFByteArray = ObjServices.SendToThunderHead(XMLByteArray, ThunderheadWrap.Service.TemplateType.Inspeccion);
                        pdfViewerMyPreviewPDF.PdfSourceBytes = PDFByteArray;
                        pdfViewerMyPreviewPDF.DataBind();
                        hdnShowPDF.Value = "true";
                        ModalPopupShowPDF.Show();
                    }
                    #endregion
                    break;
                case "InspectionAddress":
                    #region Direccion de la inspeccion
                    txtInspectionAddress.Text = vehicle.InspectionAddress;
                    ViewState["InsuredVehicleId"] = vehicle.InsuredVehicleId;

                    bool canDo = ObjServices.IsAgentQuotRole || ObjServices.IsAngetInspectorQuotRole || ObjServices.isUserCot;

                    btnSaveAddress.Visible = canDo;
                    txtInspectionAddress.ReadOnly = !canDo;

                    bool ReadOnly = Estados.Contains(ObjServices.StatusNameKey);

                    if (ReadOnly)
                    {
                        btnSaveAddress.Visible = !ReadOnly;
                        txtInspectionAddress.ReadOnly = ReadOnly;
                    }

                    hdnlongitudSelectedVehicle.Value = string.IsNullOrEmpty(vehicle.longitud) ? "0" : vehicle.longitud;
                    hdnlatitudelectedVehicle.Value = string.IsNullOrEmpty(vehicle.latitud) ? "0" : vehicle.latitud;
                    hdnInspectionAddressPopup.Value = "true";
                    ModalPopupInspectionAddress.Show();

                    #endregion
                    break;
                case "EditVehicle":
                    WUCVechicleEditForm.Initialize();
                    hdnPopVehicleEditForm.Value = "true";
                    WUCVechicleEditForm.InsuredVehicleId = vehicle.InsuredVehicleId;
                    WUCVechicleEditForm.FillText(vehicle.Registry, vehicle.Chassis, vehicle.ColorId.GetValueOrDefault(), vehicle.New.GetValueOrDefault());
                    mpeVehicleEditForm.Show();
                    break;
                case "TBSiniestralidad":
                    WUCTBSiniestraldidad.Make = vehicle.MakeDesc;
                    WUCTBSiniestraldidad.Model = vehicle.ModelDesc;
                    WUCTBSiniestraldidad.Initialize();
                    hdnPopTBSiniestralidad.Value = "true";
                    ModalPopupTBSiniestralidad.Show();
                    break;
            }

            this.ExcecuteJScript("$('#ModalPopupShowPDF_backgroundElement').css('display', 'none');");
        }

        protected void gvVehicle_PreRender(object sender, EventArgs e)
        {
            var Grid = (sender as DevExpress.Web.ASPxGridView);

            //Traducir las columnas
            Grid.TranslateColumnsAspxGrid();
        }

        protected void gvVehicle_PageIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnSaveAddress_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtInspectionAddress.Text.Trim()))
            {
                this.MessageBox(Resources.FieldCannotBeEmpty);
                return;
            }

            var vehicleInsuredId = ViewState["InsuredVehicleId"].ToInt();

            var dataVehicle = ObjServices.oPolicyManager.GetVehicleInsured(new Policy.Parameter
            {
                CorpId = ObjServices.Corp_Id,
                RegionId = ObjServices.Region_Id,
                CountryId = ObjServices.Country_Id,
                DomesticregId = ObjServices.Domesticreg_Id,
                StateProvId = ObjServices.State_Prov_Id,
                CityId = ObjServices.City_Id,
                OfficeId = ObjServices.Office_Id,
                CaseSeqNo = ObjServices.Case_Seq_No,
                HistSeqNo = ObjServices.Hist_Seq_No
            }).FirstOrDefault(h => h.InsuredVehicleId == vehicleInsuredId);

            var oLongitud = this.Page.Master.FindControl("hdnlongitud");
            var oLatitud = this.Page.Master.FindControl("hdnlatitud");
            var vLong = string.Empty;
            var vLat = string.Empty;

            if (oLongitud != null)
                vLong = (oLongitud as HiddenField).Value;

            if (oLatitud != null)
                vLat = (oLatitud as HiddenField).Value;

            var useTheSameLocationForAllItems = (chkCloneAllAddress.Checked);
            if (useTheSameLocationForAllItems)
            {
                if (string.IsNullOrEmpty(vLat) || string.IsNullOrEmpty(vLong))
                {
                    this.MessageBox("Debe marcar un punto en el mapa");
                    return;
                }

                //Obtener todos los vehiculos y setearles a todos la misma longitud y latitud
                foreach (var item in ListVehicles)
                {
                    item.latitud = vLat;
                    item.longitud = vLong;
                    item.InspectionAddress = txtInspectionAddress.Text.Trim();
                    ObjServices.oPolicyManager.SetVehicleInsured(item);
                }
            }

            if (dataVehicle != null)
            {
                dataVehicle.InspectionAddress = txtInspectionAddress.Text.Trim();
                dataVehicle.UserId = ObjServices.UserID.GetValueOrDefault();
                if (!useTheSameLocationForAllItems)
                {
                    dataVehicle.longitud = vLong;
                    dataVehicle.latitud = vLat;
                }
                ObjServices.oPolicyManager.SetVehicleInsured(dataVehicle);
                ViewState["InsuredVehicleId"] = null;
                ModalPopupInspectionAddress.Hide();
                FillData();
            }

            hdnInspectionAddressPopup.Value = "false";
        }

        protected void gvVehicle_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            var Grid = (sender as ASPxGridView);
            var TabSelected = (Utility.Tabs)Enum.Parse(typeof(Utility.Tabs), ObjServices.hdnQuotationTabs);

            if (e.RowType == GridViewRowType.Data)
            {
                var isEffectivePolicy = ObjServices.StatusNameKey == Utility.IllustrationStatus.Effective.Code() ||
                                        ObjServices.StatusNameKey == Utility.IllustrationStatus.Cancelled.Code()
                                        ;

                var lnkEdit = Grid.FindRowCellTemplateControl(e.VisibleIndex, null, "lnkEdit") as LinkButton;
                var lnkCoverage = Grid.FindRowCellTemplateControl(e.VisibleIndex, null, "lnkCoverage") as LinkButton;
                var lnkTag = Grid.FindRowCellTemplateControl(e.VisibleIndex, null, "lnkTag") as LinkButton;

                var isAvailable =
                     (ObjServices.isUserCot ||
                      ObjServices.IsSuscripcionQuotRole ||
                      ObjServices.IsSuscripcionManagerQuotRole ||
                      ObjServices.IsSucripcionDirectorQuotRole) && !isEffectivePolicy && (TabSelected != Utility.Tabs.lnkConfirmationCall ||
                                                                                          TabSelected != Utility.Tabs.lnkApprovedBySubscription ||
                                                                                          TabSelected != Utility.Tabs.lnkHistoricalIllustrations ||
                                                                                          TabSelected != Utility.Tabs.lnkDeclinedByClient ||
                                                                                          TabSelected != Utility.Tabs.lnkDeclinedBySubscription
                                                                                          );

                //Si es un movimiento de cambio se debe habilitar el boton de edicion de datos del vehiculos siempre y cuando el estatus no es efectiva
                if (ObjServices.isVehicleChange && !isEffectivePolicy)
                    isAvailable = true;

                lnkEdit.Enabled = isAvailable;
                lnkEdit.CssClass = lnkEdit.Enabled ? "myedit_file" : "myedit_file_dis";

                //Cuando es una exclusion o un cambio hay que deshabilitar el boton de ver coberturas y el marbete
                if (ObjServices.isExclusion || ObjServices.isVehicleChange)
                {
                    var cssClass = "hasdocument_False";
                    lnkCoverage.Enabled = false;
                    lnkCoverage.CssClass = cssClass;
                    lnkTag.Enabled = false;
                    lnkTag.CssClass = cssClass;
                }
            }
        }

        private string cssInspectionAddress(string inspectionAddress, bool? inspectionRequired)
        {
            string result = string.Empty;
            bool required = inspectionRequired.HasValue ? inspectionRequired.GetValueOrDefault() : false;

            if (string.IsNullOrWhiteSpace(inspectionAddress) && required)
                result = "myedit_file";
            else if (string.IsNullOrWhiteSpace(inspectionAddress) && !required)
                result = "myedit_file";
            else if (!string.IsNullOrWhiteSpace(inspectionAddress) && required)
                result = "view_file";
            else if (!string.IsNullOrWhiteSpace(inspectionAddress) && !required)
                result = "myedit_file";

            return result;
        }

        private bool enabledInspectionAddress(string inspectionAddress, bool? inspectionRequired)
        {
            bool edit = false,
                 required = false;

            if (!string.IsNullOrWhiteSpace(inspectionAddress))
                edit = true;
            else
                if (!Estados.Contains(ObjServices.StatusNameKey))
                    if (!ObjServices.IsAgentQuotRole && !ObjServices.isUserCot)
                        edit = false;
                    else
                        edit = true;
                else
                    edit = false;

            required = inspectionRequired.HasValue ? inspectionRequired.GetValueOrDefault() : false;

            return (edit && required);
        }

        protected void btnCloneAddress_Click(object sender, EventArgs e)
        {

        }
    }
}