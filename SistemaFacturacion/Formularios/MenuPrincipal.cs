﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaFacturacion.Formularios
    {
    public partial class MenuPrincipal : Form
        {
        private static Seccion seccion;
        string cargo = "";
        public MenuPrincipal()
            {
            seccion = Seccion.Instance;
            InitializeComponent();
            }
        private void MenuPrincipal_Load(object sender, EventArgs e)
            {
                
              cargo = AppTools.LogicRoll.Cargos(seccion.Rolid);
            }
        private void panel3_Paint(object sender, PaintEventArgs e)
            {

            }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Salir_Click(object sender, EventArgs e)
            {
            
            DialogResult resul = MessageBox.Show("Esta seguro que desea apagar el Sistema?, " + seccion.Usuario + " " + cargo, "Mensage de Confirmacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resul == System.Windows.Forms.DialogResult.OK)
                {
                this.Close();
                }
            }

        private void timer1_Tick(object sender, EventArgs e)
            {

            toolStripStatusLabel1.Text = DateTime.Now.ToString("F");
            toolStripStatusLabel2.Text="***Usuario: "+ seccion.Usuario.ToString()+" Cargo : " + cargo;
            }

        private void BtnCategoria_Click(object sender, EventArgs e)
            {
            FrmCategoria f = new Formularios.FrmCategoria();
            f.ShowDialog();
            }

        private void BtnCliente_Click(object sender, EventArgs e)
            {
            FrmClientes cliente = new Formularios.FrmClientes();

            cliente.ShowDialog();
            }

        private void BtnIngreso_Click(object sender, EventArgs e)
            {
            Formularios.FrmIngresos f = new FrmIngresos();
            f.ShowDialog();
            }

        private void BtnArticulos_Click(object sender, EventArgs e)
            {
            FrmArticulos f = new FrmArticulos();
            f.ShowDialog();
            }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }
