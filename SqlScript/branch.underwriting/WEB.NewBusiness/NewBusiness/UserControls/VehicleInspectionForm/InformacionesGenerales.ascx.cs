﻿using Entity.UnderWriting.Entities;
using iTextSharp.text;
using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.VehicleInspectionForm
{
    public partial class InformacionesGenerales : UC, IUC
    {
        private bool AutoSaveEnabled;

        private int AutoSaveMinutes;

        Policy poliza;

        string OfficeDesc = string.Empty;

        int InspectorAgentId = 0;

        public string IllustrationStatusCode
        {
            get
            {
                return Convert.ToString(Session["IllustrationStatusCode"]);
            }
        }

        public int IllustrationSuscriptorAgentId
        {
            get
            {
                int result;

                if (Session["IllustrationSuscriptorAgentId"] != null)
                {
                    if (!int.TryParse(Session["IllustrationSuscriptorAgentId"].ToString(), out result))
                        result = -1;
                }
                else
                    result = -1;

                return
                    result;
            }
        }

        public int IllustrationInspectorAgentId
        {
            get
            {
                int result;

                if (Session["IllustrationInspectorAgentId"] != null)
                {
                    if (!int.TryParse(Session["IllustrationInspectorAgentId"].ToString(), out result))
                        result = -1;
                }
                else
                    result = -1;

                return
                    result;
            }
        }

        private bool IllustrationIsSubcription()
        {
            var codes = new[] {
                    Utility.IllustrationStatus.Subscription.Code(),
                    Utility.IllustrationStatus.Submitted.Code()};

            return codes.Contains(IllustrationStatusCode);
        }

        public string getQuotationNumber()
        {
            return txtNumeroCotizacion.Text;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Translator(string.Empty);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //var initialize = Session["Initialize"] != null ? true : false;
            //if (initialize)
            //    Initialize();
            //else
            //    VehicleSelected();
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

        public void Initialize()
        {
            Session["Initialize"] = null;

            ClearData();

            FillDrops();

            //FillData();
        }

        public void ClearData()
        {
            Session["AutoSave"] = false;

            this.ExcecuteJScript("DisabledSections('true');");

            //Informaciones Generales
            txtFecha.Text = DateTime.Now.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture).ToUpper().Replace(".", string.Empty);

            txtHoraInicio.Text = DateTime.Now.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture).ToUpper();

            hdnModeloId.Value =
            txtModelo.Text =
            txtAno.Text =
            txtColor.Text =
            txtCilindros.Text =
            txtPlaca.Text =
            txtTipo.Text =
            txtUso.Text =
            txtKilometraje.Text =
            txtChasis.Text =
            txtCapacidad.Text =
            txtTelefono.Text =
            txtCorreoElectronico.Text = string.Empty;

            ddlVersion.SelectedIndex =
            ddlTransmision.SelectedIndex =
            ddlClase.SelectedIndex =
            ddlTraccion.SelectedIndex = 0;

            rbSi.Checked = rbNo.Checked = false;

            this.ExcecuteJScript("cleanAll();");
        }

        private List<Policy.Vehicle.Detail> Vehiculos(Policy poliza)
        {
            #region test
            //var vehicles = ObjServices.oPolicyManager.GetVehicleInsured(new Policy.Parameter
            //{
            //    CorpId = poliza.CorpId,
            //    RegionId = poliza.RegionId,
            //    CountryId = poliza.CountryId,
            //    DomesticregId = poliza.DomesticregId,
            //    StateProvId = poliza.StateProvId,
            //    CityId = poliza.CityId,
            //    OfficeId = poliza.OfficeId,
            //    CaseSeqNo = poliza.CaseSeqNo,
            //    HistSeqNo = poliza.HistSeqNo
            //}).Select(v => new Policy.Vehicle.Detail
            //{
            //    CorpId = v.CorpId,
            //    RegionId = v.RegionId,
            //    CountryId = v.CountryId,
            //    DomesticRegId = v.DomesticregId,
            //    StateProvId = v.StateProvId,
            //    CityId = v.CityId,
            //    OfficeId = v.OfficeId,
            //    CaseSeqNo = v.CaseSeqNo,
            //    HistSeqNo = v.HistSeqNo,
            //    VehicleUniqueId = v.VehicleUniqueId,
            //    ModelId = v.ModelId,
            //    MakeId = v.MakeId,
            //    MakeDesc = v.MakeDesc,
            //    ModelDesc = v.ModelDesc,
            //    Registry = v.Registry,
            //    Chassis = v.Chassis,
            //    ColorId = v.ColorId.GetValueOrDefault(),
            //    ColorDesc = v.ColorDesc,
            //    Year = v.Year.GetValueOrDefault(),
            //    ExpirationDate = v.ExpirationDate.GetValueOrDefault().ToString(),
            //    PremiumAmount = v.PremiumAmount,
            //    InsuredVehicleId = v.InsuredVehicleId
            //}).ToList();
            #endregion

            var vehicles = ObjServices.oPolicyManager.GetVehicleInsured(new Policy.Parameter
            {
                CorpId = poliza.CorpId,
                RegionId = poliza.RegionId,
                CountryId = poliza.CountryId,
                DomesticregId = poliza.DomesticregId,
                StateProvId = poliza.StateProvId,
                CityId = poliza.CityId,
                OfficeId = poliza.OfficeId,
                CaseSeqNo = poliza.CaseSeqNo,
                HistSeqNo = poliza.HistSeqNo
            }).Where(v => v.InspectionRequired.GetValueOrDefault()).Select(v => new Policy.Vehicle.Detail
            {
                CorpId = v.CorpId,
                RegionId = v.RegionId,
                CountryId = v.CountryId,
                DomesticRegId = v.DomesticregId,
                StateProvId = v.StateProvId,
                CityId = v.CityId,
                OfficeId = v.OfficeId,
                CaseSeqNo = v.CaseSeqNo,
                HistSeqNo = v.HistSeqNo,
                VehicleUniqueId = v.VehicleUniqueId,
                ModelId = v.ModelId,
                MakeId = v.MakeId,
                MakeDesc = v.MakeDesc,
                ModelDesc = v.ModelDesc,
                Registry = v.Registry,
                Chassis = v.Chassis,
                ColorId = v.ColorId.GetValueOrDefault(),
                ColorDesc = v.ColorDesc,
                Year = v.Year.GetValueOrDefault(),
                ExpirationDate = v.ExpirationDate.GetValueOrDefault().ToString(),
                PremiumAmount = v.PremiumAmount,
                InsuredVehicleId = v.InsuredVehicleId,
                InspectionAddress = v.InspectionAddress,
                FuelTypeDesc = Utility.getFuelTypeDesc(v.rateJsonSysflex)
            }).ToList();

            return vehicles;
        }

        private Policy Poliza()
        {
            Policy poliza = ObjServices.oPolicyManager.GetPolicy(ObjServices.Corp_Id,
                                                                 ObjServices.Region_Id,
                                                                 ObjServices.Country_Id,
                                                                 ObjServices.Domesticreg_Id,
                                                                 ObjServices.State_Prov_Id,
                                                                 ObjServices.City_Id,
                                                                 ObjServices.Office_Id,
                                                                 ObjServices.Case_Seq_No,
                                                                 ObjServices.Hist_Seq_No);
            return poliza;
        }

        private Policy.Contact Contact(Policy poliza)
        {
            var contact = ObjServices.oPolicyManager.GetContactPolicy(poliza.CorpId,
                                                                      poliza.RegionId,
                                                                      poliza.CountryId,
                                                                      poliza.DomesticregId,
                                                                      poliza.StateProvId,
                                                                      poliza.CityId,
                                                                      poliza.OfficeId,
                                                                      poliza.CaseSeqNo,
                                                                      poliza.HistSeqNo,
                                                                      null,
                                                                      null).FirstOrDefault();
            return contact;
        }

        private void GetPoliza()
        {
            poliza = Poliza();
        }

        private List<Policy.Vehicle.Detail> GetVehicles()
        {
            return Vehiculos(poliza);
        }

        public void FillData()
        {
            try
            {
                GetPoliza();

                List<Policy.Vehicle.Detail> vehicles = GetVehicles();
                ViewState["vehicles"] = vehicles;

                MarcasBinding(vehicles);

                var contact = Contact(poliza);

                txtNumeroCotizacion.Text = poliza.PolicyNo;
                ObjServices.Policy_Id = poliza.PolicyNo;

                string pais = ObjServices.GettingDropData(Utility.DropDownType.Country).FirstOrDefault(c => c.CountryId == poliza.CountryId).GlobalCountryDesc;

                string provincia = ObjServices.GettingDropData(Utility.DropDownType.StateProvince,
                                                               countryId: poliza.CountryId).FirstOrDefault(p => p.StateProvId == poliza.StateProvId).StateProvDesc;

                var Ciudad_Municipio = ObjServices.GettingDropData(Utility.DropDownType.City2,
                                                                   corpId: poliza.CorpId,
                                                                   countryId: poliza.CountryId,
                                                                   domesticregId: poliza.DomesticregId,
                                                                   stateProvId: poliza.StateProvId,
                                                                   cityId: poliza.CityId);

                string ciudad = string.Empty,
                       municipio = string.Empty;

                if (Ciudad_Municipio != null)
                {
                    var cm = Ciudad_Municipio.FirstOrDefault();

                    ciudad = Ciudad_Municipio != null ? cm.CityDesc : string.Empty;
                    municipio = Ciudad_Municipio != null ? cm.MunicipioDesc : string.Empty;
                }

                txtPais.Text = pais;
                txtProvincia.Text = provincia;
                txtMunicipio.Text = municipio;
                txtCiudad.Text = ciudad;

                #region InspectorName
                var quoTemp = ObjServices.oPolicyManager.GetQuotationInfoTemp(new Policy.Quo.Temp
                {
                    PolicyNo = poliza.PolicyNo
                });
                if (quoTemp != null)
                {
                    var data = quoTemp.FirstOrDefault();
                    //txtInspector.Text = data.InspectorName;
                    
                    ObjServices.InspectorName = data.InspectorName;
                    InspectorAgentId = data.InspectorAgentId; // este es el Id del inspector
                    ObjServices.InspectorAgentId = InspectorAgentId;
                    OfficeDesc = data.OfficeDesc;
                }
                #endregion

                #region Oficina
                var assignedOffice = ObjServices.oVehicleManager.GetAgentAssignedOffice(new Vehicle.Agent
                {
                    CorpId = poliza.CorpId,
                    AgentId = ObjServices.Agent_LoginId
                });

                Session["Sucursal"] = assignedOffice != null ? assignedOffice.OfficeDesc : OfficeDesc;
                #endregion

                txtRegistradaPor.Text = ObjServices.isUserCot ? ObjServices.InspectorName : ObjServices.UserFullName;

                if (ObjServices.InspectorAgentId > 0)
                {
                    drpInspectors.SelectedValue = ObjServices.InspectorAgentId.ToString();
                    drpInspectors.Enabled = false;
                }else
                    drpInspectors.Enabled = true;

                txtAsegurado.Text = contact.FullName;
                txtIntermediario.Text = poliza.Agent_Name;

                VehicleSelected();

                #region AutoSave
                var AutoSave = ObjServices.GettingDropData(Utility.DropDownType.AutoSave);
                if (AutoSave != null)
                {
                    AutoSaveEnabled = AutoSave.FirstOrDefault().AutoSaveEnabled.GetValueOrDefault();
                    AutoSaveMinutes = AutoSaveEnabled ? AutoSave.FirstOrDefault().AutoSaveMinutes.GetValueOrDefault() : 0;
                }
                #endregion
            }
            catch (Exception ex)
            {
                this.MessageBox(ex.Message, null, null, true, Resources.Warning);
            }
        }

        private void MarcasBinding(List<Policy.Vehicle.Detail> vehicles)
        {
            int vehicles_count = vehicles.Count;
            Session["vehiclesCount"] = vehicles_count;

            var data = new Dictionary<string, string>();
            data.Add("0", Resources.Select);
            if (vehicles_count > 0)
            {
                foreach (var item in vehicles)
                    data.Add(string.Format("{0}|{1}|{2}", item.MakeId.Value, item.VehicleUniqueId, item.InsuredVehicleId), item.MakeDesc);
            }

            ddlMarca.DataSource = data;
            ddlMarca.DataBind();
            //ddlMarca.SelectedIndex = 0;
            ddlMarca.SelectedIndex = ddlMarca.Items.Count == 2 ? 1 : 0;
        }

        private bool DisableInspection()
        {
            IEnumerable<Policy.Agent> myWonAgentIdList;
            int agentId;
            bool result, isAssingToMe, isMissingInspectionStatus;

            result = false;

            if (ObjServices.IsInspectorQuotRole)
            {
                agentId = ObjServices.Agent_Id.HasValue
                                ? ObjServices.Agent_Id.Value
                                : -1;

                myWonAgentIdList = ObjServices.oPolicyManager.GetAgentIdListByAgentId(new Policy.Agent
                {
                    CorpId = ObjServices.Corp_Id,
                    AgentId = agentId
                });

                //Si esto es true, esto significa que la persona que esta login es el inspector que tiene asignada la cotizacion.
                isAssingToMe = myWonAgentIdList.Select(a => a.AgentId).ToArray().Contains(IllustrationInspectorAgentId);

                //Si esto es true, significa que la inpeccion aun no esta lista.
                isMissingInspectionStatus = IllustrationStatusCode == Utility.IllustrationStatus.MissingInspection.Code();

                if (isAssingToMe && isMissingInspectionStatus)
                    result = true;
            }

            return
                result;
        }

        private void FillDrops(int? version_id = null, int? class_id = null, int? transmission_type_id = null, int? wheel_drive_type_id = null, int? mileagekilometer_id = null)
        {
            var data = new Dictionary<string, string>();

            #region Clases
            var Classes = ObjServices.oVehicleManager.GetVehicleClasses(new Vehicle.Classes() { ClassId = class_id }).ToList();
            if (Classes.Count > 0)
            {
                data.Add("0", Resources.Select);
                foreach (var item in Classes)
                    data.Add(Convert.ToString(item.ClassId), item.ClassDesc.ToUpper());

                ddlClase.Items.Clear();
                ddlClase.DataSource = data;
                ddlClase.DataBind();

                ddlClase.SelectedIndex = 0;

                if (class_id != null)
                    ddlClase.SelectedValue = Convert.ToString(class_id);
            }
            #endregion

            data.Clear();

            #region Transmision
            var TransmissionTypes = ObjServices.oVehicleManager.GetVehicleTransmissionTypes(new Vehicle.TransmissionType() { TransmissionTypeId = transmission_type_id }).ToList();
            if (TransmissionTypes.Count > 0)
            {
                data.Add("0", Resources.Select);
                foreach (var item in TransmissionTypes)
                    data.Add(Convert.ToString(item.TransmissionTypeId), item.TransmissionTypeDesc.ToUpper());

                ddlTransmision.Items.Clear();
                ddlTransmision.DataSource = data;
                ddlTransmision.DataBind();

                ddlTransmision.SelectedIndex = 0;

                if (transmission_type_id != null)
                    ddlTransmision.SelectedValue = Convert.ToString(transmission_type_id);
            }
            #endregion

            data.Clear();

            #region Traccion
            var WheelDriveTypes = ObjServices.oVehicleManager.GetVehicleWheelDriveTypes(new Vehicle.WheelDriveType() { WheelDriveTypeId = wheel_drive_type_id }).ToList();
            if (WheelDriveTypes.Count > 0)
            {
                data.Add("0", Resources.Select);
                foreach (var item in WheelDriveTypes)
                    data.Add(Convert.ToString(item.WheelDriveTypeId), item.WheelDriveTypeDesc.ToUpper());

                ddlTraccion.Items.Clear();
                ddlTraccion.DataSource = data;
                ddlTraccion.DataBind();

                ddlTraccion.SelectedIndex = 0;

                if (wheel_drive_type_id != null)
                    ddlTraccion.SelectedValue = Convert.ToString(wheel_drive_type_id);
            }
            #endregion

            data.Clear();

            #region Version
            var Versions = ObjServices.oVehicleManager.GetVehicleVersions(new Vehicle.Version() { VersionId = version_id.GetValueOrDefault() }).ToList();
            if (Versions.Count > 0)
            {
                data.Add("0", Resources.Select);
                foreach (var item in Versions)
                    data.Add(Convert.ToString(item.VersionId), item.VersionDesc.ToUpper());

                ddlVersion.Items.Clear();
                ddlVersion.DataSource = data;
                ddlVersion.DataBind();

                ddlVersion.SelectedIndex = 0;

                if (version_id != null)
                    ddlVersion.SelectedValue = Convert.ToString(version_id);
            }
            #endregion

            data.Clear();

            #region Mileage/Kilometer
            data.Add("0", Resources.Select);
            data.Add("1", Resources.VehicleKilometers);
            data.Add("2", Resources.VehicleMiles);

            ddlMileageKilometer.Items.Clear();
            ddlMileageKilometer.DataValueField = "Key";
            ddlMileageKilometer.DataTextField = "Value";
            ddlMileageKilometer.DataSource = data;
            ddlMileageKilometer.DataBind();

            ddlMileageKilometer.SelectedIndex = 0;
            if (mileagekilometer_id != null)
                ddlMileageKilometer.SelectedValue = Convert.ToString(mileagekilometer_id);
            #endregion

            #region  Inspectors
            data.Clear();
            //drpInspectors
            var dataAgent = ObjServices.GettingDropData(Utility.DropDownType.InspectorCot,
                                                            corpId: ObjServices.Corp_Id,
                                                            regionId: ObjServices.Region_Id,
                                                            countryId: ObjServices.Country_Id,
                                                            domesticregId: ObjServices.Domesticreg_Id,
                                                            stateProvId: ObjServices.State_Prov_Id,
                                                            cityId: ObjServices.City_Id,
                                                            officeId: ObjServices.Office_Id,
                                                            caseSeqNo: ObjServices.Case_Seq_No,
                                                            histSeqNo: ObjServices.Hist_Seq_No
                                                            ).ToList();

            if (dataAgent.Count > 0)
            {
                data.Add("0", Resources.Select);
                foreach (var item in dataAgent)
                {
                    if (!data.ContainsValue(item.AgentName.ToUpper()))
                        data.Add(Convert.ToString(item.AgentId), item.AgentName.ToUpper());
                }

                drpInspectors.Items.Clear();
                drpInspectors.DataSource = data;
                drpInspectors.DataBind();

                drpInspectors.SelectedIndex = 0;

                if (ObjServices.InspectorAgentId > 0)
                    drpInspectors.SelectedValue = Convert.ToString(ObjServices.InspectorAgentId);
            }
            
            #endregion
            data.Clear();
        }
            
        
        public void Clean()
        {
            ddlMarca.SelectedIndex = 0;
            VehicleSelected();
            this.ExcecuteJScript("cleanAll();");
            hdnClean.Value = "false";
        }

        private void VehicleSelected()
        {
            Session["MarcaChasis"] = null;

            if (ddlMarca.SelectedValue == "0")
            {
                #region Cancelar AutoSave
                if (AutoSaveEnabled && Convert.ToBoolean(Session["AutoSave"]))
                {
                    Session["AutoSave"] = false;
                    this.ExcecuteJScript("clearInterval(auto_save_interval_id);");
                }
                #endregion

                ClearData();
                return;
            }

            if (ddlMarca.SelectedValue.Contains("|"))
            {
                string isEditString;
                var arr = ddlMarca.SelectedValue.Split('|');
                int MakeId = arr[0].ToInt();
                long VehicleUniqueId = arr[1].ToInt();

                ObjServices.InsuredVehicleId = arr[2].ToInt();

                Session["VehicleUniqueId"] = VehicleUniqueId;

                this.ExcecuteJScript("cleanAll('false');");

                //Jheiron Dotel 19-06-2018 Esta seccion (01)

                var poliza = Poliza();

                hdnMissingInspection.Value = poliza.PolicyStatusId == Utility.IllustrationStatus.MissingInspection.ID().ToInt() ? "true" : "false";

                var contact = Contact(poliza);

                var vehicles = Vehiculos(poliza);

                var vehicle = vehicles.FirstOrDefault(v => v.MakeId == MakeId &&
                                                           v.VehicleUniqueId == VehicleUniqueId &&
                                                           v.InsuredVehicleId == ObjServices.InsuredVehicleId);

                ObjServices.FuelTypeDesc = vehicle.FuelTypeDesc;
                /**/

                var VehicleInspectionFormPage = Page as WEB.NewBusiness.NewBusiness.Pages.VehicleInspectionForm;
                if (VehicleInspectionFormPage != null)
                {
                    var inspectionFormUC = Utility.GetAllChildren(VehicleInspectionFormPage).FirstOrDefault(uc => uc is InspectionForm);
                    if (inspectionFormUC != null)
                        (inspectionFormUC as InspectionForm).FillData();
                }


                //Jheiron Dotel 16-09-208 La seccion (01) estaba aqui, lo movi para que me carge el tipo de combustible antes de llamar al FillData()
                /**/

                bool Inspection = false;

                var vehicle_details = ObjServices.oVehicleManager.GetVehicleInsuredDetail(new Vehicle.Policy
                {
                    ContactId = contact.ContactId,
                    CorpId = vehicle.CorpId,
                    RegionId = vehicle.RegionId,
                    CountryId = vehicle.CountryId,
                    DomesticRegId = vehicle.DomesticRegId,
                    StateProvId = vehicle.StateProvId,
                    CityId = vehicle.CityId,
                    OfficeId = vehicle.OfficeId,
                    CaseSeqNo = vehicle.CaseSeqNo,
                    HistSeqNo = vehicle.HistSeqNo
                });

                Vehicle.Insured.Detail vehicle_detail = new Vehicle.Insured.Detail();
                if (vehicle_details != null)
                    vehicle_detail = vehicle_details.FirstOrDefault(v => v.VehicleUniqueId == vehicle.VehicleUniqueId);

                #region Informaciones Generales
                string Modelo = string.IsNullOrWhiteSpace(vehicle.ModelDesc) ? string.Empty : vehicle.ModelDesc.Trim(),
                       Cilindros = vehicle_detail != null ? vehicle_detail.Cylinders.ToString() : string.Empty,
                       Placa = vehicle_detail != null ? vehicle_detail.Registry : string.Empty,
                       Tipo = vehicle_detail != null ? vehicle_detail.Type : string.Empty,
                       Uso = vehicle_detail != null ? vehicle_detail.Use : string.Empty,
                       Kilometraje = vehicle_detail != null ? vehicle_detail.Odometer.ToString() : string.Empty,
                       Capacidad = vehicle_detail != null ? vehicle_detail.Seats.ToString() : string.Empty,
                       Chassis = string.IsNullOrWhiteSpace(vehicle.Chassis) ? string.Empty : vehicle.Chassis.Trim(),
                       Color = string.IsNullOrWhiteSpace(vehicle.ColorDesc) ? string.Empty : vehicle.ColorDesc.Trim(),
                       InspectionAddress = string.IsNullOrWhiteSpace(vehicle.InspectionAddress) ? string.Empty : vehicle.InspectionAddress.Trim();

                txtDireccionInspeccion.Text = InspectionAddress.RemoveCR();
                txtDireccionInspeccion.ToolTip = txtDireccionInspeccion.Text;

                txtModelo.Text = string.IsNullOrWhiteSpace(Modelo) ? "N/A" : Modelo.RemoveCR();
                ViewState["txtModelo"] = txtModelo.Text;
                hdnModeloId.Value = vehicle.ModelId.HasValue ? vehicle.ModelId.Value.ToString() : "0";
                txtAno.Text = vehicle.Year.ToString();
                txtColor.Text = Color.RemoveCR();
                txtCilindros.Text = Cilindros;
                txtPlaca.Text = string.IsNullOrWhiteSpace(Placa) ? string.Empty : Placa.RemoveCR();
                txtTipo.Text = string.IsNullOrWhiteSpace(Tipo) ? string.Empty : Tipo.RemoveCR();
                txtUso.Text = string.IsNullOrWhiteSpace(Uso) ? string.Empty : Uso.RemoveCR();
                txtKilometraje.Text = Kilometraje;
                txtCapacidad.Text = Capacidad;
                txtChasis.Text = Chassis.RemoveCR();
                txtChasis.Enabled = txtChasis.Text.Trim().Length > 0;

                string VersionId = "0",
                       TransmissionTypeId = "0",
                       VehicleClassId = "0",
                       WheelDriveTypeId = "0",
                       MileageKilometerId = "0";

                if (vehicle_detail != null)
                {
                    VersionId = Convert.ToString(vehicle_detail.VersionId);
                    TransmissionTypeId = Convert.ToString(vehicle_detail.TransmissionTypeId.HasValue ? vehicle_detail.TransmissionTypeId.Value : 0);
                    VehicleClassId = Convert.ToString(vehicle_detail.VehicleClassId.HasValue ? vehicle_detail.VehicleClassId.Value : 0);
                    WheelDriveTypeId = Convert.ToString(vehicle_detail.WheelDriveTypeId.HasValue ? vehicle_detail.WheelDriveTypeId.Value : 0);
                }

                if (ddlMarca.Items.Count == 2)
                {
                    this.ExcecuteJScript(string.Format("fillDrops({0}, {1}, {2}, {3}, {4});", VersionId,
                                                                                              VehicleClassId,
                                                                                              TransmissionTypeId,
                                                                                              WheelDriveTypeId,
                                                                                              null));

                    this.ExcecuteJScript(string.Format("fillInformacionVehiculo('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');", txtModelo.Text,
                                                                                                                                                         txtAno.Text,
                                                                                                                                                         txtColor.Text,
                                                                                                                                                         txtUso.Text,
                                                                                                                                                         txtPlaca.Text,
                                                                                                                                                         txtTipo.Text,
                                                                                                                                                         txtChasis.Text,
                                                                                                                                                         txtCilindros.Text,
                                                                                                                                                         txtKilometraje.Text,
                                                                                                                                                         txtCapacidad.Text));

                }
                else
                {
                    ddlVersion.SelectedValue = VersionId;
                    ddlTransmision.SelectedValue = TransmissionTypeId;
                    ddlClase.SelectedValue = VehicleClassId;
                    ddlTraccion.SelectedValue = WheelDriveTypeId;
                }
                #endregion

                var vehicle_insured = ObjServices.oVehicleManager.GetVehicleInsured(new Vehicle.Policy
                {
                    CorpId = vehicle.CorpId,
                    RegionId = vehicle.RegionId,
                    CountryId = vehicle.CountryId,
                    DomesticRegId = vehicle.DomesticRegId,
                    StateProvId = vehicle.StateProvId,
                    CityId = vehicle.CityId,
                    OfficeId = vehicle.OfficeId,
                    CaseSeqNo = vehicle.CaseSeqNo,
                    HistSeqNo = vehicle.HistSeqNo
                }).FirstOrDefault(v => v.VehicleUniqueId == vehicle.VehicleUniqueId &&
                                       v.InsuredVehicleId == ObjServices.InsuredVehicleId);

                if (vehicle_insured != null)
                {
                    string sucursal = Session["Sucursal"] != null ? Convert.ToString(Session["Sucursal"]) : string.Empty;
                    hdnLongitudVEH.Value = vehicle_insured.Longitud;
                    hdnLatitudVEH.Value = vehicle_insured.Latitud;

                    var vehicleReview = ObjServices.oVehicleManager.GetVehicleReview(new Vehicle
                    {
                        CorpId = vehicle_insured.CorpId,
                        RegionId = vehicle_insured.RegionId,
                        CountryId = vehicle_insured.CountryId,
                        DomesticRegId = vehicle_insured.DomesticRegId,
                        StateProvId = vehicle_insured.StateProvId,
                        CityId = vehicle_insured.CityId,
                        OfficeId = vehicle_insured.OfficeId,
                        CaseSeqNo = vehicle_insured.CaseSeqNo,
                        HistSeqNo = vehicle_insured.HistSeqNo,
                        InsuredVehicleId = vehicle_insured.InsuredVehicleId
                    }).FirstOrDefault();

                    if (vehicleReview != null)
                    {
                        #region Existe Inspeccion
                        Inspection = true;

                        ObjServices.ReviewId = vehicleReview.ReviewId.GetValueOrDefault();

                        #region Informaciones Generales
                        #region INFORMACIÓN DELA COTIZACIÓN
                        txtCorreoElectronico.Text = string.IsNullOrWhiteSpace(vehicleReview.Email) ? string.Empty : vehicleReview.Email.RemoveCR();
                        txtTelefono.Text = string.IsNullOrWhiteSpace(vehicleReview.Phone) ? string.Empty : vehicleReview.Phone.RemoveCR();
                        #endregion

                        #region INFORMACIÓN DE LA INSPECCIÓN
                        txtFecha.Text = vehicleReview.ReviewDate.Value.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture).ToUpper().Replace(".", string.Empty);
                        txtHoraInicio.Text = vehicleReview.ReviewDate.Value.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture).ToUpper();
                        txtRegistradaPor.Text = string.IsNullOrWhiteSpace(vehicleReview.InspectedByName) ? string.Empty : vehicleReview.InspectedByName.RemoveCR();
                        //txtDireccionInspeccion.Text = string.IsNullOrWhiteSpace(vehicleReview.InspectionAddress) ? string.Empty : vehicleReview.InspectionAddress.RemoveCR();
                        //txtDireccionInspeccion.ToolTip = txtDireccionInspeccion.Text;
                        #endregion

                        #region INFORMACIÓN DEL VEHÍCULO
                        hdnModeloId.Value = vehicleReview.ModelId.ToString();
                        txtAno.Text = vehicleReview.ModelYear.ToString();
                        VersionId = Convert.ToString(vehicleReview.VersionId);
                        txtColor.Text = string.IsNullOrWhiteSpace(vehicleReview.ColorDesc) ? Color.RemoveCR() : vehicleReview.ColorDesc.RemoveCR();
                        TransmissionTypeId = Convert.ToString(vehicleReview.TransmissionTypeId);
                        VehicleClassId = Convert.ToString(vehicleReview.VehicleClassId);
                        txtUso.Text = string.IsNullOrWhiteSpace(vehicleReview.UsageDesc) ? Uso.RemoveCR() : vehicleReview.UsageDesc.RemoveCR();
                        txtPlaca.Text = string.IsNullOrWhiteSpace(vehicleReview.RegistryPlate) ? Placa.RemoveCR() : vehicleReview.RegistryPlate.RemoveCR();
                        txtTipo.Text = string.IsNullOrWhiteSpace(vehicleReview.VehicleTypeDesc) ? Tipo.RemoveCR() : vehicleReview.VehicleTypeDesc.RemoveCR();
                        WheelDriveTypeId = Convert.ToString(vehicleReview.WheelDriveTypeId);
                        txtChasis.Text = string.IsNullOrWhiteSpace(vehicleReview.Mark) ? Chassis.RemoveCR() : vehicleReview.Mark.RemoveCR();
                        txtChasis.Enabled = txtChasis.Text.Trim().Length > 0;
                        txtCilindros.Text = vehicleReview.Cylinder.ToString();
                        MileageKilometerId = Convert.ToString(vehicleReview.MileageKilometer);
                        txtKilometraje.Text = vehicleReview.Odometer.ToString();
                        txtCapacidad.Text = vehicleReview.Seats.ToString();
                        rbSi.Checked = vehicleReview.RegistrationDocument.Value;
                        rbNo.Checked = !rbSi.Checked;

                        if (ddlMarca.Items.Count == 2)
                        {
                            this.ExcecuteJScript(string.Format("fillDrops({0}, {1}, {2}, {3}, {4});", VersionId,
                                                                                                      VehicleClassId,
                                                                                                      TransmissionTypeId,
                                                                                                      WheelDriveTypeId,
                                                                                                      MileageKilometerId));

                            this.ExcecuteJScript(string.Format("fillInformacionVehiculo('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');", ViewState["txtModelo"].ToString(),
                                                                                                                                                                 txtAno.Text,
                                                                                                                                                                 txtColor.Text,
                                                                                                                                                                 txtUso.Text,
                                                                                                                                                                 txtPlaca.Text,
                                                                                                                                                                 txtTipo.Text,
                                                                                                                                                                 txtChasis.Text,
                                                                                                                                                                 txtCilindros.Text,
                                                                                                                                                                 txtKilometraje.Text,
                                                                                                                                                                 txtCapacidad.Text));
                        }
                        else
                        {
                            ddlVersion.SelectedValue = VersionId;
                            ddlTransmision.SelectedValue = TransmissionTypeId;
                            ddlClase.SelectedValue = VehicleClassId;
                            ddlTraccion.SelectedValue = WheelDriveTypeId;
                            ddlMileageKilometer.SelectedValue = MileageKilometerId;
                        }
                        #endregion
                        #endregion

                        Session["MarcaChasis"] = string.Format("{0}|{1}", ddlMarca.SelectedItem.Text, txtChasis.Text);

                        #region Valores JSON
                        var horafull = txtHoraInicio.Text;
                        var ampmfull = horafull.Split(' ');
                        var array_hora = ampmfull[0].Split(':');
                        var ampm = ampmfull[1];
                        int hora = Convert.ToInt32(array_hora[0]),
                            minutos = Convert.ToInt32(array_hora[1]),
                            segundos = Convert.ToInt32(array_hora[2]);

                        if (ampm == "PM" && hora < 12)
                            hora += 12;

                        var ReviewDate = new DateTime(vehicleReview.ReviewDate.Value.Year,
                                                      vehicleReview.ReviewDate.Value.Month,
                                                      vehicleReview.ReviewDate.Value.Day,
                                                      hora,
                                                      minutos,
                                                      segundos);

                        var valores = Utility.serializeToJSON(new ValoresJSON
                        {
                            CorpId = poliza.CorpId,
                            RegionId = poliza.RegionId,
                            CountryId = poliza.CountryId,
                            DomesticRegId = poliza.DomesticregId,
                            StateProvId = poliza.StateProvId,
                            CityId = poliza.CityId,
                            OfficeId = poliza.OfficeId,
                            CaseSeqNo = poliza.CaseSeqNo,
                            HistSeqNo = poliza.HistSeqNo,
                            PolicyStatusId = poliza.PolicyStatusId,
                            InsuredVehicleId = vehicle_insured.InsuredVehicleId,
                            ColorId = vehicle.ColorId,
                            VehicleTypeId = vehicle_detail.TypeId,
                            VehicleReviewId = vehicleReview.ReviewId,
                            MarcaId = vehicleReview.MakeId,
                            ModeloId = vehicleReview.ModelId,
                            UsageId = vehicle_detail.UseId,
                            InspectedBy = ObjServices.InspectorAgentId,
                            Inspection = Inspection,
                            VehicleUniqueId = vehicle.VehicleUniqueId,
                            UserID = ObjServices.UserID,
                            Sucursal = Session["Sucursal"] != null ? Convert.ToString(Session["Sucursal"]) : string.Empty,
                            LanguageId = ObjServices.Language.ToInt(),
                            Quotation = ObjServices.Policy_Id,
                            ReviewId = vehicleReview.ReviewId.GetValueOrDefault(),
                            ModelYear = txtAno.Text.ToInt(),
                            Seats = txtCapacidad.Text.ToInt(),
                            Cylinder = txtCilindros.Text.ToInt(),
                            RegistryPlate = txtPlaca.Text,
                            VersionId = ddlVersion.SelectedValue.ToInt(),
                            TransmissionTypeId = ddlTransmision.SelectedValue.ToInt(),
                            WheelDriveTypeId = ddlTraccion.SelectedValue.ToInt(),
                            VehicleClassId = ddlClase.SelectedValue.ToInt(),
                            Odometer = txtKilometraje.Text.ToInt(),
                            RegistrationDocument = rbSi.Checked,
                            ReviewDate = ReviewDate.ToString("yyyy-MM-dd hh:mm:ss tt"), /*Convert.ToDateTime(string.Format("{0} {1}", vehicleReview.ReviewDate.Value.ToString("yyyy-MM-dd"), txtHoraInicio.Text), CultureInfo.InvariantCulture),*/
                            Mark = txtChasis.Text,
                            InspectionAddress = vehicle.InspectionAddress
                        });

                        hdnValoresJSON.Value = valores;
                        #endregion

                        #region Verificacion Informaciones Generales / Secciones
                        bool btnClean = hdnClean.Value != null ? Convert.ToBoolean(hdnClean.Value) : false;
                        if (!btnClean)
                        {
                            string optionsReviews = string.Format("OptionsReviews('{0}');", vehicleReview.RegistrationDocument.Value ? "true" : "false");
                            this.ExcecuteJScript(optionsReviews);
                        }
                        #endregion

                        #region Fotos
                        this.ExcecuteJScript("PhotoReviews();");
                        #endregion

                        #region Otras Informaciones
                        if (!btnClean)
                        {
                            OtrasInformaciones.DictamenDanos = vehicleReview.ReviewNotes;
                            OtrasInformaciones.InspectorSuggestsAcceptRisk = vehicleReview.InspectorSuggestsAcceptRisk;
                            OtrasInformaciones.HoraCulminacion = vehicleReview.ReviewFinishDate != null ? vehicleReview.ReviewFinishDate.Value.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture) : string.Empty;
                            OtrasInformaciones.Sucursal = sucursal;

                            #region Valores JSON
                            var valoresOI = Utility.serializeToJSON(new OtraInformacion
                            {
                                DictamenDanos = vehicleReview.ReviewNotes,
                                Sucursal = sucursal,
                                HoraCulminacion = vehicleReview.ReviewFinishDate != null ? vehicleReview.ReviewFinishDate.Value.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture) : string.Empty,
                                InspectorSuggestsAcceptRisk = vehicleReview.InspectorSuggestsAcceptRisk,
                                Mensaje = string.Empty,
                                UsuarioInspeccion = vehicleReview.UsuarioInspeccion,
                                CorpId = vehicle_insured.CorpId,
                                RegionId = vehicle_insured.RegionId,
                                CountryId = vehicle_insured.CountryId,
                                DomesticRegId = vehicle_insured.DomesticRegId,
                                StateProvId = vehicle_insured.StateProvId,
                                CityId = vehicle_insured.CityId,
                                OfficeId = vehicle_insured.OfficeId,
                                CaseSeqNo = vehicle_insured.CaseSeqNo,
                                HistSeqNo = vehicle_insured.HistSeqNo
                            });

                            hdnValoresOIJSON.Value = valoresOI;
                            #endregion

                            this.ExcecuteJScript("OtherInformation();");
                        }
                        else
                        {
                            OtrasInformaciones.DictamenDanos = string.Empty;
                            OtrasInformaciones.InspectorSuggestsAcceptRisk = null;
                            OtrasInformaciones.HoraCulminacion = string.Empty;
                            OtrasInformaciones.Sucursal = sucursal;

                            #region Valores JSON
                            var valoresOI = Utility.serializeToJSON(new OtraInformacion
                            {
                                DictamenDanos = string.Empty,
                                Sucursal = sucursal,
                                HoraCulminacion = string.Empty,
                                InspectorSuggestsAcceptRisk = null,
                                Mensaje = string.Empty
                            });

                            hdnValoresOIJSON.Value = valoresOI;
                            #endregion
                        }
                        #endregion
                        #endregion
                    }
                    else
                    {
                        #region No existe inspeccion
                        Inspection = false;

                        txtCorreoElectronico.Text =
                        txtTelefono.Text = string.Empty;

                        #region Valores JSON
                        var horafull = txtHoraInicio.Text;
                        var ampmfull = horafull.Split(' ');
                        var array_hora = ampmfull[0].Split(':');
                        var ampm = ampmfull[1];
                        int hora = Convert.ToInt32(array_hora[0]),
                            minutos = Convert.ToInt32(array_hora[1]),
                            segundos = Convert.ToInt32(array_hora[2]);

                        if (ampm == "PM" && hora < 12)
                            hora += 12;

                        var ReviewDate = new DateTime(DateTime.Now.Year,
                                                      DateTime.Now.Month,
                                                      DateTime.Now.Day,
                                                      hora,
                                                      minutos,
                                                      segundos);

                        var valores = Utility.serializeToJSON(new ValoresJSON
                        {
                            CorpId = poliza.CorpId,
                            RegionId = poliza.RegionId,
                            CountryId = poliza.CountryId,
                            DomesticRegId = poliza.DomesticregId,
                            StateProvId = poliza.StateProvId,
                            CityId = poliza.CityId,
                            OfficeId = poliza.OfficeId,
                            CaseSeqNo = poliza.CaseSeqNo,
                            HistSeqNo = poliza.HistSeqNo,
                            PolicyStatusId = poliza.PolicyStatusId,
                            InsuredVehicleId = vehicle_insured.InsuredVehicleId,
                            ColorId = vehicle.ColorId,
                            VehicleTypeId = vehicle_detail != null ? vehicle_detail.TypeId : 1,
                            VehicleReviewId = null,
                            MarcaId = MakeId,
                            ModeloId = vehicle.ModelId.HasValue ? vehicle.ModelId.Value : 1,
                            UsageId = vehicle_detail != null ? vehicle_detail.UseId : 1,
                            InspectedBy = ObjServices.InspectorAgentId,
                            Inspection = Inspection,
                            VehicleUniqueId = vehicle.VehicleUniqueId,
                            UserID = ObjServices.UserID,
                            Sucursal = sucursal.RemoveCR(),
                            LanguageId = ObjServices.Language.ToInt(),
                            Quotation = ObjServices.Policy_Id,
                            ModelYear = txtAno.Text.ToInt(),
                            Seats = txtCapacidad.Text.ToInt(),
                            Cylinder = txtCilindros.Text.ToInt(),
                            RegistryPlate = txtPlaca.Text,
                            VersionId = ddlVersion.SelectedValue.ToInt(),
                            TransmissionTypeId = ddlTransmision.SelectedValue.ToInt(),
                            WheelDriveTypeId = ddlTraccion.SelectedValue.ToInt(),
                            VehicleClassId = ddlClase.SelectedValue.ToInt(),
                            Odometer = txtKilometraje.Text.ToInt(),
                            RegistrationDocument = rbSi.Checked,
                            ReviewDate = ReviewDate.ToString("yyyy-MM-dd hh:mm:ss tt"), /*Convert.ToDateTime(string.Format("{0} {1}", DateTime.Today.ToString("yyyy-MM-dd"), txtHoraInicio.Text), CultureInfo.InvariantCulture),*/
                            Mark = txtChasis.Text,
                            InspectionAddress = vehicle.InspectionAddress
                        });

                        hdnValoresJSON.Value = valores;

                        var valoresOI = Utility.serializeToJSON(new OtraInformacion
                        {
                            DictamenDanos = string.Empty,
                            Sucursal = sucursal,
                            HoraCulminacion = DateTime.Now.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture),
                            InspectorSuggestsAcceptRisk = null,
                            Mensaje = string.Empty
                        });

                        hdnValoresOIJSON.Value = valoresOI;

                        this.ExcecuteJScript("OtherInformation();");
                        #endregion

                        OtrasInformaciones.Sucursal = sucursal;
                        #endregion
                    }

                    string script = string.Empty,
                           horaInicio = string.Empty;

                    #region Desabilitar Secciones
                    //isEdit = DisableInspection();

                    /*
                     Jheiron Dotel 23-06-2017 ORIGINAL
                     Se solicito que se pueda hacer la inspeccion independientemente del tab/status en que se encuentre la cotizacion
                     Que cualquera pueda editarla siempre y cuando no este efectiva la cotizacion
                     
                     El codigo original sera comentado, por si luego piden que se cambie el nuevo requerimiento.                     
                     */
                    /*bool isMissingInspectionStatus = IllustrationStatusCode == Utility.IllustrationStatus.MissingInspection.Code();
                    if (!isMissingInspectionStatus)
                        isEditString = isEdit & !Inspection ? "false" : "true";
                    else
                        isEditString = "false";*/

                    bool isEffectiveStatus = IllustrationStatusCode == Utility.IllustrationStatus.Effective.Code();
                    if (isEffectiveStatus)
                        isEditString = "true";
                    else if (ObjServices.isUserCot || ObjServices.IsInspectorQuotRole)
                        isEditString = "false";
                    else
                        isEditString = "true";

                    script = string.Format("DisabledSections('{0}');", isEditString);
                    this.ExcecuteJScript(script);
                    #endregion

                    #region Fijar Hora de Inicio
                    horaInicio = Convert.ToDateTime(Inspection ? vehicleReview.ReviewDate.Value : DateTime.Now).ToString("hh:mm:ss tt", CultureInfo.InvariantCulture).ToUpper();
                    script = string.Format("SetHoraInicio('{0}');", horaInicio);
                    this.ExcecuteJScript(script);
                    #endregion

                    #region AutoSave
                    if (isEditString == "false" && AutoSaveEnabled)
                    {
                        Session["AutoSave"] = true;
                        string saveDraft = string.Format("var auto_save_interval_id = setInterval(saveDraft, {0});", (AutoSaveMinutes * 60000));
                        this.ExcecuteJScript(saveDraft);
                    }
                    #endregion
                }
            }
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            VehicleSelected();
        }

        public void Translator(string Lang)
        {
            string select = Resources.Select;

            if (ddlMarca.Items.Count > 0)
                ddlMarca.Items[0].Text = select;

            if (ddlVersion.Items.Count > 0)
                ddlVersion.Items[0].Text = select;

            if (ddlTransmision.Items.Count > 0)
                ddlTransmision.Items[0].Text = select;

            if (ddlClase.Items.Count > 0)
                ddlClase.Items[0].Text = select;

            if (ddlTraccion.Items.Count > 0)
                ddlTraccion.Items[0].Text = select;
        }

        //protected void drpInspectors_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (drpInspectors.SelectedIndex > 0)
        //        ObjServices.InspectorAgentId = drpInspectors.SelectedValue.ToInt();
        //    else
        //        ObjServices.InspectorAgentId = 0;
        //}
    }

    #region Clases
    public class ValoresJSON
    {
        public int CorpId { get; set; }
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public int DomesticRegId { get; set; }
        public int StateProvId { get; set; }
        public int CityId { get; set; }
        public int OfficeId { get; set; }
        public int CaseSeqNo { get; set; }
        public int HistSeqNo { get; set; }
        public int? PolicyStatusId { get; set; }
        public int? VehicleReviewId { get; set; }
        public int InsuredVehicleId { get; set; }
        public int? ColorId { get; set; }
        public int? VehicleTypeId { get; set; }
        public int? UsageId { get; set; }
        public int? InspectedBy { get; set; }
        public bool Inspection { get; set; }
        public long VehicleUniqueId { get; set; }
        public int MarcaId { get; set; }
        public int ModeloId { get; set; }
        public int? UserID { get; set; }
        public string Sucursal { get; set; }
        public int LanguageId { get; set; }
        public string Quotation { get; set; }
        public int ReviewId { get; set; }
        public int ModelYear { get; set; }
        public int Seats { get; set; }
        public int Cylinder { get; set; }
        public string RegistryPlate { get; set; }
        public int VersionId { get; set; }
        public int TransmissionTypeId { get; set; }
        public int WheelDriveTypeId { get; set; }
        public int VehicleClassId { get; set; }
        public int? Odometer { get; set; }
        public bool? RegistrationDocument { get; set; }
        public string ReviewDate { get; set; }
        public string Mark { get; set; }
        public string InspectionAddress { get; set; }
    }

    public class OtraInformacion
    {
        public string DictamenDanos { get; set; }
        public string Sucursal { get; set; }
        public string HoraCulminacion { get; set; }
        public bool? InspectorSuggestsAcceptRisk { get; set; }
        public string Mensaje { get; set; }
        public string UsuarioInspeccion { get; set; }
        public int CorpId { get; set; }
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public int DomesticRegId { get; set; }
        public int StateProvId { get; set; }
        public int CityId { get; set; }
        public int OfficeId { get; set; }
        public int CaseSeqNo { get; set; }
        public int HistSeqNo { get; set; }
    }
    #endregion
}