﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad.DbVentas;
using CapaLogicaNegocio.NegocioDbVentas;

namespace SistemaFacturacion.Formularios
    {
    public partial class FrmArticulos : Form
        {
        public FrmArticulos()
            {
            InitializeComponent();
            }
        ComboxBosxTools cbo = new ComboxBosxTools();
        private void FrmArticulos_Load(object sender, EventArgs e)
            {
            GetProveedorCbox();
            GetCategotia();

            }
        private void GetProveedorCbox()
            {
            var c = cbo.ListaProve();
            cboProv.DataSource = c;
            cboProv.DisplayMember = "NombreProveedor";
            cboProv.ValueMember = "idproveedor";

            }
        private void GetCategotia()
            {
            var c = cbo.GetCategotia();
            cboCat.DataSource = c;
            cboCat.DisplayMember = "nombre";
            cboCat.ValueMember = "idcategoria";

            }

        }
    }