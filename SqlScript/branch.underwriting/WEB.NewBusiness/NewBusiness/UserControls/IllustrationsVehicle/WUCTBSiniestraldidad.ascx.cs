﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle
{
    public partial class WUCTBSiniestraldidad : UC, IUC
    {
        public class ItemTbSiniestralidad
        {
            public string Make { get; set; }
            public string Model { get; set; }
            public int Year { get; set; }
            public string VehicleCount { get; set; }
            public string AccidentRate { get; set; }
            public string Frequency { get; set; }
            public string Liquidation { get; set; }
        }

        public string Make
        {
            get { return ViewState["Make"].ToString(); }

            set
            {
                ViewState["Make"] = value;
            }
        }

        public string Model
        {
            get { return ViewState["Model"].ToString(); }

            set
            {
                ViewState["Model"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvTBSiniestralidad_PreRender(object sender, EventArgs e)
        {

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
            var data = ObjServices.oPolicyManager.GetTbAccidentRateByMakeAndModel(Make, Model)
                                  .Select(r => new ItemTbSiniestralidad
                                  {
                                      Make = r.CARBRANDNAME,
                                      Model = r.CARMODELNAME,
                                      Year = r.POLICYITEMCARYEAR,
                                      AccidentRate = r.SINIESTRALIDAD.HasValue ? string.Format(CultureInfo.InvariantCulture, "{0:0.00}" + "%", r.SINIESTRALIDAD.GetValueOrDefault()) : "-",
                                      Frequency = r.FRECUENCIA.HasValue ? string.Format(CultureInfo.InvariantCulture, "{0:0,.00}" + "%", r.FRECUENCIA) : "-",
                                      VehicleCount = r.CANTIDADVEHICULOS.HasValue ? string.Format(CultureInfo.InvariantCulture, "{0:0}", r.CANTIDADVEHICULOS) : "-",
                                      Liquidation = r.LIQUIDACION.HasValue ? string.Format(CultureInfo.InvariantCulture, "{0:0.00}" + "%", r.LIQUIDACION) : "-"
                                  });

            gvTBSiniestralidad.DataSource = data;
            gvTBSiniestralidad.DataBind();
        }

        public void Initialize()
        {
            FillData();
        }

        public void ClearData()
        {
            throw new NotImplementedException();
        }

        protected void gvTBSiniestralidad_PageIndexChanged(object sender, EventArgs e)
        {
            FillData();
        }
    }
}