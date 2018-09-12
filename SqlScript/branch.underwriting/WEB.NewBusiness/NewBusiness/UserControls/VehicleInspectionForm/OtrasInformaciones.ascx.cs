﻿using Entity.UnderWriting.Entities;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.VehicleInspectionForm
{
    public partial class OtrasInformaciones : UC, IUC
    {
        public static bool Enabled { get; set; }

        public static string DictamenDanos { get; set; }
        public static string Sucursal { get; set; }
        public static string HoraCulminacion { get; set; }
        public static bool? InspectorSuggestsAcceptRisk { get; set; }
        public static string UsuarioInspeccion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        public void Translator(string Lang)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            this.txtUsuarioInspeccion.Enabled = true;
            this.ExcecuteJScript("$('.timePicker').timepicker({ 'step': 1, 'timeFormat': 'h:i A' }).timepicker('setTime', new Date());");

            this.ExcecuteJScript("$('.onlyNumbers').keypress(function (event) { return isNumber(event, this); });");

            string sucursal = Convert.ToString(Session["Sucursal"]);
            if (!string.IsNullOrWhiteSpace(sucursal))
            {
                txtSucursal.Text = sucursal;
                this.ExcecuteJScript(string.Format("SetSucursal('{0}');", sucursal));
            }

            txtUsuarioInspeccion.Text = ObjServices.IsInspectorQuotRole? ObjServices.InspectorName : ObjServices.UserFullName;
            this.ExcecuteJScript(string.Format("SetUserInspector('{0}');", ObjServices.isUserCot ? ObjServices.InspectorName : ObjServices.UserFullName));
            txtUsuarioInspeccion.Enabled = true;
           
        }

        public void ClearData()
        {
            throw new NotImplementedException();
        }
    }
}