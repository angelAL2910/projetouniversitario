﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProcesarCancelaciones
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProcesarCancelaciones))
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.timerTiempoTranscurrido = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnExportCSV = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NombreSupervisor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Diferencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BalanceSF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BalanceGP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Endosada = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaVenceReal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FinVigencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InicioVigencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TipoIntermediario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NombreIntermediario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Beneficiario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NombreCliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReglaSiniestros = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Poliza = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Seleccionar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GrdCancelaciones = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTotalPolizas = New System.Windows.Forms.TextBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblPolizasFiltradas = New System.Windows.Forms.Label()
        Me.txtPolizasFiltradas = New System.Windows.Forms.TextBox()
        Me.lblPasoProceso = New System.Windows.Forms.Label()
        Me.lblTotalPolizasSeleccionadas = New System.Windows.Forms.Label()
        Me.txtTotalPolizasSeleccionadas = New System.Windows.Forms.TextBox()
        Me.chkMostrarSeleccionadas = New System.Windows.Forms.CheckBox()
        Me.LblCondition = New System.Windows.Forms.Label()
        Me.btnProcesar = New System.Windows.Forms.Button()
        Me.BtnCargarDatos = New System.Windows.Forms.Button()
        Me.txtFiltro = New System.Windows.Forms.TextBox()
        Me.cbFiltros = New System.Windows.Forms.ComboBox()
        Me.btnFiltrar = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnEliminarFiltro = New System.Windows.Forms.Button()
        Me.lblFiltrandoPor = New System.Windows.Forms.Label()
        Me.btnPegarPolizas = New System.Windows.Forms.Button()
        Me.lblTiempoDeProcesamiento = New System.Windows.Forms.Label()
        Me.SysFlexSegurosDataSetBindingSource = New Cancelaciones.SysFlexSegurosDataSet()
        Me.TabControlCancelacionPolizas = New System.Windows.Forms.TabControl()
        Me.TabPageCanceladaPago = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TabPageCanceladasPorDocumentos = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.GrdCancelacionesDocument = New System.Windows.Forms.DataGridView()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn26 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        CType(Me.GrdCancelaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SysFlexSegurosDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlCancelacionPolizas.SuspendLayout()
        Me.TabPageCanceladaPago.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPageCanceladasPorDocumentos.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.GrdCancelacionesDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        '
        'btnExportCSV
        '
        Me.btnExportCSV.FlatAppearance.BorderSize = 0
        Me.btnExportCSV.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue
        Me.btnExportCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportCSV.Image = Global.Cancelaciones.My.Resources.Resources.text_csv
        Me.btnExportCSV.Location = New System.Drawing.Point(1211, 75)
        Me.btnExportCSV.Name = "btnExportCSV"
        Me.btnExportCSV.Size = New System.Drawing.Size(35, 35)
        Me.btnExportCSV.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.btnExportCSV, "Descargar Archivo CSV")
        Me.btnExportCSV.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Poliza"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Poliza"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "ReglaSiniestros"
        Me.DataGridViewTextBoxColumn2.HeaderText = "+3 Siniestros"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "NombreCliente"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Nombre del Cliente"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 250
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Beneficiario"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Beneficiario"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 150
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "NombreIntermediario"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Nombre de Intermediario"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 250
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "TipoIntermediario"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Tipo de Intermediario"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "InicioVigencia"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Inicio de Vigencia"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "FinVigencia"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Fin de Vigencia"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "FechaVenceReal"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Fecha Vencimiento Real"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Endosada"
        Me.DataGridViewTextBoxColumn10.HeaderText = "Endosada"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "BalanceGP"
        DataGridViewCellStyle1.Format = "C2"
        DataGridViewCellStyle1.NullValue = "0"
        Me.DataGridViewTextBoxColumn11.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewTextBoxColumn11.HeaderText = "BalanceGP"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "BalanceSF"
        DataGridViewCellStyle2.Format = "C2"
        DataGridViewCellStyle2.NullValue = "0"
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn12.HeaderText = "BalanceSF"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "Diferencia"
        DataGridViewCellStyle3.Format = "C2"
        DataGridViewCellStyle3.NullValue = "0"
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn13.HeaderText = "Diferencia"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "NombreSupervisor"
        Me.DataGridViewTextBoxColumn14.HeaderText = "Nombre de Supervisor"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Width = 250
        '
        'NombreSupervisor
        '
        Me.NombreSupervisor.DataPropertyName = "NombreSupervisor"
        Me.NombreSupervisor.HeaderText = "Nombre de Supervisor"
        Me.NombreSupervisor.Name = "NombreSupervisor"
        Me.NombreSupervisor.ReadOnly = True
        Me.NombreSupervisor.Width = 250
        '
        'Diferencia
        '
        Me.Diferencia.DataPropertyName = "Diferencia"
        DataGridViewCellStyle4.Format = "C2"
        DataGridViewCellStyle4.NullValue = "0"
        Me.Diferencia.DefaultCellStyle = DataGridViewCellStyle4
        Me.Diferencia.HeaderText = "Diferencia"
        Me.Diferencia.Name = "Diferencia"
        Me.Diferencia.ReadOnly = True
        '
        'BalanceSF
        '
        Me.BalanceSF.DataPropertyName = "BalanceSF"
        DataGridViewCellStyle5.Format = "C2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.BalanceSF.DefaultCellStyle = DataGridViewCellStyle5
        Me.BalanceSF.HeaderText = "BalanceSF"
        Me.BalanceSF.Name = "BalanceSF"
        Me.BalanceSF.ReadOnly = True
        '
        'BalanceGP
        '
        Me.BalanceGP.DataPropertyName = "BalanceGP"
        DataGridViewCellStyle6.Format = "C2"
        DataGridViewCellStyle6.NullValue = "0"
        Me.BalanceGP.DefaultCellStyle = DataGridViewCellStyle6
        Me.BalanceGP.HeaderText = "BalanceGP"
        Me.BalanceGP.Name = "BalanceGP"
        Me.BalanceGP.ReadOnly = True
        '
        'Endosada
        '
        Me.Endosada.DataPropertyName = "Endosada"
        Me.Endosada.HeaderText = "Endosada"
        Me.Endosada.Name = "Endosada"
        Me.Endosada.ReadOnly = True
        '
        'FechaVenceReal
        '
        Me.FechaVenceReal.DataPropertyName = "FechaVenceReal"
        Me.FechaVenceReal.HeaderText = "Fecha Vencimiento Real"
        Me.FechaVenceReal.Name = "FechaVenceReal"
        Me.FechaVenceReal.ReadOnly = True
        '
        'FinVigencia
        '
        Me.FinVigencia.DataPropertyName = "FinVigencia"
        Me.FinVigencia.HeaderText = "Fin de Vigencia"
        Me.FinVigencia.Name = "FinVigencia"
        Me.FinVigencia.ReadOnly = True
        '
        'InicioVigencia
        '
        Me.InicioVigencia.DataPropertyName = "InicioVigencia"
        Me.InicioVigencia.HeaderText = "Inicio de Vigencia"
        Me.InicioVigencia.Name = "InicioVigencia"
        Me.InicioVigencia.ReadOnly = True
        '
        'TipoIntermediario
        '
        Me.TipoIntermediario.DataPropertyName = "TipoIntermediario"
        Me.TipoIntermediario.HeaderText = "Tipo de Intermediario"
        Me.TipoIntermediario.Name = "TipoIntermediario"
        Me.TipoIntermediario.ReadOnly = True
        '
        'NombreIntermediario
        '
        Me.NombreIntermediario.DataPropertyName = "NombreIntermediario"
        Me.NombreIntermediario.HeaderText = "Nombre de Intermediario"
        Me.NombreIntermediario.Name = "NombreIntermediario"
        Me.NombreIntermediario.ReadOnly = True
        Me.NombreIntermediario.Width = 250
        '
        'Beneficiario
        '
        Me.Beneficiario.DataPropertyName = "Beneficiario"
        Me.Beneficiario.HeaderText = "Beneficiario"
        Me.Beneficiario.Name = "Beneficiario"
        Me.Beneficiario.ReadOnly = True
        Me.Beneficiario.Width = 150
        '
        'NombreCliente
        '
        Me.NombreCliente.DataPropertyName = "NombreCliente"
        Me.NombreCliente.HeaderText = "Nombre del Cliente"
        Me.NombreCliente.Name = "NombreCliente"
        Me.NombreCliente.ReadOnly = True
        Me.NombreCliente.Width = 250
        '
        'ReglaSiniestros
        '
        Me.ReglaSiniestros.DataPropertyName = "ReglaSiniestros"
        Me.ReglaSiniestros.HeaderText = "+3 Siniestros"
        Me.ReglaSiniestros.Name = "ReglaSiniestros"
        '
        'Poliza
        '
        Me.Poliza.DataPropertyName = "Poliza"
        Me.Poliza.HeaderText = "Poliza"
        Me.Poliza.Name = "Poliza"
        '
        'Seleccionar
        '
        Me.Seleccionar.DataPropertyName = "None"
        Me.Seleccionar.HeaderText = "Seleccionar"
        Me.Seleccionar.Name = "Seleccionar"
        Me.Seleccionar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'GrdCancelaciones
        '
        Me.GrdCancelaciones.AllowUserToOrderColumns = True
        Me.GrdCancelaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdCancelaciones.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Seleccionar, Me.Poliza, Me.ReglaSiniestros, Me.NombreCliente, Me.Beneficiario, Me.NombreIntermediario, Me.TipoIntermediario, Me.InicioVigencia, Me.FinVigencia, Me.FechaVenceReal, Me.Endosada, Me.BalanceGP, Me.BalanceSF, Me.Diferencia, Me.NombreSupervisor})
        Me.GrdCancelaciones.Location = New System.Drawing.Point(6, 52)
        Me.GrdCancelaciones.Name = "GrdCancelaciones"
        Me.GrdCancelaciones.Size = New System.Drawing.Size(1350, 613)
        Me.GrdCancelaciones.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(55, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Total Polizas:"
        '
        'txtTotalPolizas
        '
        Me.txtTotalPolizas.Enabled = False
        Me.txtTotalPolizas.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold)
        Me.txtTotalPolizas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.txtTotalPolizas.Location = New System.Drawing.Point(154, 57)
        Me.txtTotalPolizas.Name = "txtTotalPolizas"
        Me.txtTotalPolizas.Size = New System.Drawing.Size(197, 33)
        Me.txtTotalPolizas.TabIndex = 5
        Me.txtTotalPolizas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(9, 31)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(342, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 13
        '
        'lblPolizasFiltradas
        '
        Me.lblPolizasFiltradas.AutoSize = True
        Me.lblPolizasFiltradas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.lblPolizasFiltradas.Location = New System.Drawing.Point(9, 105)
        Me.lblPolizasFiltradas.Name = "lblPolizasFiltradas"
        Me.lblPolizasFiltradas.Size = New System.Drawing.Size(144, 16)
        Me.lblPolizasFiltradas.TabIndex = 16
        Me.lblPolizasFiltradas.Text = "Total Polizas Filtradas:"
        Me.lblPolizasFiltradas.Visible = False
        '
        'txtPolizasFiltradas
        '
        Me.txtPolizasFiltradas.Enabled = False
        Me.txtPolizasFiltradas.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold)
        Me.txtPolizasFiltradas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.txtPolizasFiltradas.Location = New System.Drawing.Point(154, 96)
        Me.txtPolizasFiltradas.Name = "txtPolizasFiltradas"
        Me.txtPolizasFiltradas.Size = New System.Drawing.Size(197, 33)
        Me.txtPolizasFiltradas.TabIndex = 17
        Me.txtPolizasFiltradas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtPolizasFiltradas.Visible = False
        '
        'lblPasoProceso
        '
        Me.lblPasoProceso.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold)
        Me.lblPasoProceso.Location = New System.Drawing.Point(495, 29)
        Me.lblPasoProceso.Name = "lblPasoProceso"
        Me.lblPasoProceso.Size = New System.Drawing.Size(316, 28)
        Me.lblPasoProceso.TabIndex = 19
        Me.lblPasoProceso.Text = "..."
        Me.lblPasoProceso.Visible = False
        '
        'lblTotalPolizasSeleccionadas
        '
        Me.lblTotalPolizasSeleccionadas.AutoSize = True
        Me.lblTotalPolizasSeleccionadas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.lblTotalPolizasSeleccionadas.Location = New System.Drawing.Point(1025, 47)
        Me.lblTotalPolizasSeleccionadas.Name = "lblTotalPolizasSeleccionadas"
        Me.lblTotalPolizasSeleccionadas.Size = New System.Drawing.Size(183, 16)
        Me.lblTotalPolizasSeleccionadas.TabIndex = 21
        Me.lblTotalPolizasSeleccionadas.Text = "Total Polizas Seleccionadas:"
        '
        'txtTotalPolizasSeleccionadas
        '
        Me.txtTotalPolizasSeleccionadas.Enabled = False
        Me.txtTotalPolizasSeleccionadas.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold)
        Me.txtTotalPolizasSeleccionadas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.txtTotalPolizasSeleccionadas.Location = New System.Drawing.Point(1214, 36)
        Me.txtTotalPolizasSeleccionadas.Name = "txtTotalPolizasSeleccionadas"
        Me.txtTotalPolizasSeleccionadas.Size = New System.Drawing.Size(128, 33)
        Me.txtTotalPolizasSeleccionadas.TabIndex = 22
        Me.txtTotalPolizasSeleccionadas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkMostrarSeleccionadas
        '
        Me.chkMostrarSeleccionadas.AutoSize = True
        Me.chkMostrarSeleccionadas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkMostrarSeleccionadas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.chkMostrarSeleccionadas.Location = New System.Drawing.Point(1040, 86)
        Me.chkMostrarSeleccionadas.Name = "chkMostrarSeleccionadas"
        Me.chkMostrarSeleccionadas.Size = New System.Drawing.Size(279, 20)
        Me.chkMostrarSeleccionadas.TabIndex = 24
        Me.chkMostrarSeleccionadas.Text = "Mostrar solo la(s) Póliza(s) Seleccionadas"
        Me.chkMostrarSeleccionadas.UseVisualStyleBackColor = True
        '
        'LblCondition
        '
        Me.LblCondition.AutoSize = True
        Me.LblCondition.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCondition.Location = New System.Drawing.Point(457, 27)
        Me.LblCondition.Name = "LblCondition"
        Me.LblCondition.Size = New System.Drawing.Size(385, 24)
        Me.LblCondition.TabIndex = 1
        Me.LblCondition.Text = "Cancelaciones a Procesar Por Falta de Pago"
        '
        'btnProcesar
        '
        Me.btnProcesar.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.btnProcesar.Enabled = False
        Me.btnProcesar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProcesar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnProcesar.ForeColor = System.Drawing.Color.White
        Me.btnProcesar.Location = New System.Drawing.Point(1249, 69)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.Size = New System.Drawing.Size(103, 44)
        Me.btnProcesar.TabIndex = 2
        Me.btnProcesar.Text = "Procesar"
        Me.btnProcesar.UseVisualStyleBackColor = False
        '
        'BtnCargarDatos
        '
        Me.BtnCargarDatos.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.BtnCargarDatos.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCargarDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCargarDatos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCargarDatos.ForeColor = System.Drawing.Color.White
        Me.BtnCargarDatos.Location = New System.Drawing.Point(17, 73)
        Me.BtnCargarDatos.Name = "BtnCargarDatos"
        Me.BtnCargarDatos.Size = New System.Drawing.Size(103, 44)
        Me.BtnCargarDatos.TabIndex = 3
        Me.BtnCargarDatos.Text = "Cargar Datos"
        Me.BtnCargarDatos.UseVisualStyleBackColor = False
        '
        'txtFiltro
        '
        Me.txtFiltro.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold)
        Me.txtFiltro.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.txtFiltro.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtFiltro.Location = New System.Drawing.Point(683, 80)
        Me.txtFiltro.Name = "txtFiltro"
        Me.txtFiltro.Size = New System.Drawing.Size(236, 33)
        Me.txtFiltro.TabIndex = 6
        '
        'cbFiltros
        '
        Me.cbFiltros.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold)
        Me.cbFiltros.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.cbFiltros.FormattingEnabled = True
        Me.cbFiltros.Items.AddRange(New Object() {"Beneficiario", "Cliente", "Nombre de Intermediario", "Tipo de Intermediario", "Nombre de Supervisor"})
        Me.cbFiltros.Location = New System.Drawing.Point(307, 81)
        Me.cbFiltros.Name = "cbFiltros"
        Me.cbFiltros.Size = New System.Drawing.Size(321, 33)
        Me.cbFiltros.TabIndex = 9
        '
        'btnFiltrar
        '
        Me.btnFiltrar.Enabled = False
        Me.btnFiltrar.Image = CType(resources.GetObject("btnFiltrar.Image"), System.Drawing.Image)
        Me.btnFiltrar.Location = New System.Drawing.Point(965, 81)
        Me.btnFiltrar.Name = "btnFiltrar"
        Me.btnFiltrar.Size = New System.Drawing.Size(87, 28)
        Me.btnFiltrar.TabIndex = 10
        Me.btnFiltrar.Text = "Filtrar"
        Me.btnFiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFiltrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFiltrar.UseVisualStyleBackColor = True
        Me.btnFiltrar.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(231, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 16)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Filtrar por :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label4.Location = New System.Drawing.Point(634, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Filtro :"
        '
        'btnEliminarFiltro
        '
        Me.btnEliminarFiltro.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEliminarFiltro.Image = CType(resources.GetObject("btnEliminarFiltro.Image"), System.Drawing.Image)
        Me.btnEliminarFiltro.Location = New System.Drawing.Point(1058, 80)
        Me.btnEliminarFiltro.Name = "btnEliminarFiltro"
        Me.btnEliminarFiltro.Size = New System.Drawing.Size(87, 28)
        Me.btnEliminarFiltro.TabIndex = 18
        Me.btnEliminarFiltro.Text = "Borrar Filtrar"
        Me.btnEliminarFiltro.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarFiltro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEliminarFiltro.UseVisualStyleBackColor = True
        Me.btnEliminarFiltro.Visible = False
        '
        'lblFiltrandoPor
        '
        Me.lblFiltrandoPor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFiltrandoPor.ForeColor = System.Drawing.Color.Red
        Me.lblFiltrandoPor.Location = New System.Drawing.Point(925, 85)
        Me.lblFiltrandoPor.Name = "lblFiltrandoPor"
        Me.lblFiltrandoPor.Size = New System.Drawing.Size(291, 20)
        Me.lblFiltrandoPor.TabIndex = 20
        Me.lblFiltrandoPor.Text = "..."
        Me.lblFiltrandoPor.Visible = False
        '
        'btnPegarPolizas
        '
        Me.btnPegarPolizas.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.btnPegarPolizas.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnPegarPolizas.Enabled = False
        Me.btnPegarPolizas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPegarPolizas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnPegarPolizas.ForeColor = System.Drawing.Color.White
        Me.btnPegarPolizas.Location = New System.Drawing.Point(126, 73)
        Me.btnPegarPolizas.Name = "btnPegarPolizas"
        Me.btnPegarPolizas.Size = New System.Drawing.Size(103, 44)
        Me.btnPegarPolizas.TabIndex = 23
        Me.btnPegarPolizas.Text = "Cargar Lista"
        Me.btnPegarPolizas.UseVisualStyleBackColor = False
        '
        'lblTiempoDeProcesamiento
        '
        Me.lblTiempoDeProcesamiento.Enabled = False
        Me.lblTiempoDeProcesamiento.Location = New System.Drawing.Point(954, 16)
        Me.lblTiempoDeProcesamiento.Name = "lblTiempoDeProcesamiento"
        Me.lblTiempoDeProcesamiento.Size = New System.Drawing.Size(402, 14)
        Me.lblTiempoDeProcesamiento.TabIndex = 15
        Me.lblTiempoDeProcesamiento.Text = "Hora Inicio : {0} | Tiempo Transcurrido {1}"
        Me.lblTiempoDeProcesamiento.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTiempoDeProcesamiento.Visible = False
        '
        'SysFlexSegurosDataSetBindingSource
        '
        Me.SysFlexSegurosDataSetBindingSource.DataSetName = "SysFlexSegurosDataSet"
        Me.SysFlexSegurosDataSetBindingSource.EnforceConstraints = False
        Me.SysFlexSegurosDataSetBindingSource.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TabControlCancelacionPolizas
        '
        Me.TabControlCancelacionPolizas.Controls.Add(Me.TabPageCanceladaPago)
        Me.TabControlCancelacionPolizas.Controls.Add(Me.TabPageCanceladasPorDocumentos)
        Me.TabControlCancelacionPolizas.Location = New System.Drawing.Point(0, 168)
        Me.TabControlCancelacionPolizas.Name = "TabControlCancelacionPolizas"
        Me.TabControlCancelacionPolizas.SelectedIndex = 0
        Me.TabControlCancelacionPolizas.Size = New System.Drawing.Size(1373, 837)
        Me.TabControlCancelacionPolizas.TabIndex = 26
        '
        'TabPageCanceladaPago
        '
        Me.TabPageCanceladaPago.Controls.Add(Me.GroupBox1)
        Me.TabPageCanceladaPago.Controls.Add(Me.GrdCancelaciones)
        Me.TabPageCanceladaPago.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCanceladaPago.Name = "TabPageCanceladaPago"
        Me.TabPageCanceladaPago.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCanceladaPago.Size = New System.Drawing.Size(1362, 811)
        Me.TabPageCanceladaPago.TabIndex = 0
        Me.TabPageCanceladaPago.Text = "Polizas A Cancelar Por Falta de Pagos"
        Me.TabPageCanceladaPago.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.txtPolizasFiltradas)
        Me.GroupBox1.Controls.Add(Me.lblPasoProceso)
        Me.GroupBox1.Controls.Add(Me.chkMostrarSeleccionadas)
        Me.GroupBox1.Controls.Add(Me.txtTotalPolizasSeleccionadas)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblTotalPolizasSeleccionadas)
        Me.GroupBox1.Controls.Add(Me.lblPolizasFiltradas)
        Me.GroupBox1.Controls.Add(Me.ProgressBar1)
        Me.GroupBox1.Controls.Add(Me.txtTotalPolizas)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 671)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1353, 135)
        Me.GroupBox1.TabIndex = 183
        Me.GroupBox1.TabStop = False
        '
        'TabPageCanceladasPorDocumentos
        '
        Me.TabPageCanceladasPorDocumentos.Controls.Add(Me.GroupBox3)
        Me.TabPageCanceladasPorDocumentos.Controls.Add(Me.GrdCancelacionesDocument)
        Me.TabPageCanceladasPorDocumentos.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCanceladasPorDocumentos.Name = "TabPageCanceladasPorDocumentos"
        Me.TabPageCanceladasPorDocumentos.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCanceladasPorDocumentos.Size = New System.Drawing.Size(1365, 811)
        Me.TabPageCanceladasPorDocumentos.TabIndex = 1
        Me.TabPageCanceladasPorDocumentos.Text = "Cancelar polizas por falta de documentación"
        Me.TabPageCanceladasPorDocumentos.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.White
        Me.GroupBox3.Controls.Add(Me.TextBox1)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.CheckBox1)
        Me.GroupBox3.Controls.Add(Me.TextBox2)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.ProgressBar2)
        Me.GroupBox3.Controls.Add(Me.TextBox3)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 647)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1353, 135)
        Me.GroupBox3.TabIndex = 185
        Me.GroupBox3.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold)
        Me.TextBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.TextBox1.Location = New System.Drawing.Point(154, 96)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(197, 33)
        Me.TextBox1.TabIndex = 17
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TextBox1.Visible = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(495, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(316, 28)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "..."
        Me.Label5.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.CheckBox1.Location = New System.Drawing.Point(1040, 86)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(279, 20)
        Me.CheckBox1.TabIndex = 24
        Me.CheckBox1.Text = "Mostrar solo la(s) Póliza(s) Seleccionadas"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Enabled = False
        Me.TextBox2.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold)
        Me.TextBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.TextBox2.Location = New System.Drawing.Point(1214, 36)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(128, 33)
        Me.TextBox2.TabIndex = 22
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label6.Location = New System.Drawing.Point(55, 58)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 16)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Total Polizas:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label7.Location = New System.Drawing.Point(1025, 47)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(183, 16)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Total Polizas Seleccionadas:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label8.Location = New System.Drawing.Point(9, 105)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(144, 16)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Total Polizas Filtradas:"
        Me.Label8.Visible = False
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Location = New System.Drawing.Point(9, 31)
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(342, 23)
        Me.ProgressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar2.TabIndex = 13
        '
        'TextBox3
        '
        Me.TextBox3.Enabled = False
        Me.TextBox3.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold)
        Me.TextBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.TextBox3.Location = New System.Drawing.Point(154, 57)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(197, 33)
        Me.TextBox3.TabIndex = 5
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GrdCancelacionesDocument
        '
        Me.GrdCancelacionesDocument.AllowUserToOrderColumns = True
        Me.GrdCancelacionesDocument.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdCancelacionesDocument.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxColumn1, Me.DataGridViewTextBoxColumn15, Me.DataGridViewTextBoxColumn16, Me.DataGridViewTextBoxColumn17, Me.DataGridViewTextBoxColumn18, Me.DataGridViewTextBoxColumn19, Me.DataGridViewTextBoxColumn20, Me.DataGridViewTextBoxColumn21, Me.DataGridViewTextBoxColumn22, Me.DataGridViewTextBoxColumn23, Me.DataGridViewTextBoxColumn24, Me.DataGridViewTextBoxColumn25, Me.DataGridViewTextBoxColumn26, Me.DataGridViewTextBoxColumn27, Me.DataGridViewTextBoxColumn28})
        Me.GrdCancelacionesDocument.Location = New System.Drawing.Point(8, 28)
        Me.GrdCancelacionesDocument.Name = "GrdCancelacionesDocument"
        Me.GrdCancelacionesDocument.Size = New System.Drawing.Size(1350, 580)
        Me.GrdCancelacionesDocument.TabIndex = 184
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "None"
        Me.DataGridViewCheckBoxColumn1.HeaderText = "Seleccionar"
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "Poliza"
        Me.DataGridViewTextBoxColumn15.HeaderText = "Poliza"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "ReglaSiniestros"
        Me.DataGridViewTextBoxColumn16.HeaderText = "+3 Siniestros"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "NombreCliente"
        Me.DataGridViewTextBoxColumn17.HeaderText = "Nombre del Cliente"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Width = 250
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "Beneficiario"
        Me.DataGridViewTextBoxColumn18.HeaderText = "Beneficiario"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        Me.DataGridViewTextBoxColumn18.Width = 150
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.DataPropertyName = "NombreIntermediario"
        Me.DataGridViewTextBoxColumn19.HeaderText = "Nombre de Intermediario"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.ReadOnly = True
        Me.DataGridViewTextBoxColumn19.Width = 250
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.DataPropertyName = "TipoIntermediario"
        Me.DataGridViewTextBoxColumn20.HeaderText = "Tipo de Intermediario"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.ReadOnly = True
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.DataPropertyName = "InicioVigencia"
        Me.DataGridViewTextBoxColumn21.HeaderText = "Inicio de Vigencia"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.ReadOnly = True
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.DataPropertyName = "FinVigencia"
        Me.DataGridViewTextBoxColumn22.HeaderText = "Fin de Vigencia"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.ReadOnly = True
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.DataPropertyName = "FechaVenceReal"
        Me.DataGridViewTextBoxColumn23.HeaderText = "Fecha Vencimiento Real"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.ReadOnly = True
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.DataPropertyName = "Endosada"
        Me.DataGridViewTextBoxColumn24.HeaderText = "Endosada"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.ReadOnly = True
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.DataPropertyName = "BalanceGP"
        DataGridViewCellStyle7.Format = "C2"
        DataGridViewCellStyle7.NullValue = "0"
        Me.DataGridViewTextBoxColumn25.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn25.HeaderText = "BalanceGP"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.ReadOnly = True
        '
        'DataGridViewTextBoxColumn26
        '
        Me.DataGridViewTextBoxColumn26.DataPropertyName = "BalanceSF"
        DataGridViewCellStyle8.Format = "C2"
        DataGridViewCellStyle8.NullValue = "0"
        Me.DataGridViewTextBoxColumn26.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn26.HeaderText = "BalanceSF"
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.ReadOnly = True
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.DataPropertyName = "Diferencia"
        DataGridViewCellStyle9.Format = "C2"
        DataGridViewCellStyle9.NullValue = "0"
        Me.DataGridViewTextBoxColumn27.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn27.HeaderText = "Diferencia"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.ReadOnly = True
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.DataPropertyName = "NombreSupervisor"
        Me.DataGridViewTextBoxColumn28.HeaderText = "Nombre de Supervisor"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.ReadOnly = True
        Me.DataGridViewTextBoxColumn28.Width = 250
        '
        'groupBox2
        '
        Me.groupBox2.BackColor = System.Drawing.Color.White
        Me.groupBox2.Controls.Add(Me.LblCondition)
        Me.groupBox2.Controls.Add(Me.BtnCargarDatos)
        Me.groupBox2.Controls.Add(Me.lblTiempoDeProcesamiento)
        Me.groupBox2.Controls.Add(Me.btnExportCSV)
        Me.groupBox2.Controls.Add(Me.cbFiltros)
        Me.groupBox2.Controls.Add(Me.btnProcesar)
        Me.groupBox2.Controls.Add(Me.Label3)
        Me.groupBox2.Controls.Add(Me.lblFiltrandoPor)
        Me.groupBox2.Controls.Add(Me.btnFiltrar)
        Me.groupBox2.Controls.Add(Me.txtFiltro)
        Me.groupBox2.Controls.Add(Me.btnPegarPolizas)
        Me.groupBox2.Controls.Add(Me.btnEliminarFiltro)
        Me.groupBox2.Controls.Add(Me.Label4)
        Me.groupBox2.Location = New System.Drawing.Point(4, 5)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(1362, 150)
        Me.groupBox2.TabIndex = 182
        Me.groupBox2.TabStop = False
        '
        'ProcesarCancelaciones
        '
        Me.AcceptButton = Me.btnFiltrar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnEliminarFiltro
        Me.ClientSize = New System.Drawing.Size(1385, 1017)
        Me.Controls.Add(Me.groupBox2)
        Me.Controls.Add(Me.TabControlCancelacionPolizas)
        Me.Name = "ProcesarCancelaciones"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Procesar Cancelaciones"
        CType(Me.GrdCancelaciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SysFlexSegurosDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlCancelacionPolizas.ResumeLayout(False)
        Me.TabPageCanceladaPago.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPageCanceladasPorDocumentos.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.GrdCancelacionesDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents timerTiempoTranscurrido As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As DataGridViewTextBoxColumn
    Friend WithEvents SysFlexSegurosDataSetBindingSource As SysFlexSegurosDataSet
    Friend WithEvents NombreSupervisor As DataGridViewTextBoxColumn
    Friend WithEvents Diferencia As DataGridViewTextBoxColumn
    Friend WithEvents BalanceSF As DataGridViewTextBoxColumn
    Friend WithEvents BalanceGP As DataGridViewTextBoxColumn
    Friend WithEvents Endosada As DataGridViewTextBoxColumn
    Friend WithEvents FechaVenceReal As DataGridViewTextBoxColumn
    Friend WithEvents FinVigencia As DataGridViewTextBoxColumn
    Friend WithEvents InicioVigencia As DataGridViewTextBoxColumn
    Friend WithEvents TipoIntermediario As DataGridViewTextBoxColumn
    Friend WithEvents NombreIntermediario As DataGridViewTextBoxColumn
    Friend WithEvents Beneficiario As DataGridViewTextBoxColumn
    Friend WithEvents NombreCliente As DataGridViewTextBoxColumn
    Friend WithEvents ReglaSiniestros As DataGridViewTextBoxColumn
    Friend WithEvents Poliza As DataGridViewTextBoxColumn
    Friend WithEvents Seleccionar As DataGridViewCheckBoxColumn
    Friend WithEvents GrdCancelaciones As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents txtTotalPolizas As TextBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents lblPolizasFiltradas As Label
    Friend WithEvents txtPolizasFiltradas As TextBox
    Friend WithEvents lblPasoProceso As Label
    Friend WithEvents lblTotalPolizasSeleccionadas As Label
    Friend WithEvents txtTotalPolizasSeleccionadas As TextBox
    Friend WithEvents chkMostrarSeleccionadas As CheckBox
    Friend WithEvents LblCondition As Label
    Friend WithEvents btnProcesar As Button
    Friend WithEvents BtnCargarDatos As Button
    Friend WithEvents txtFiltro As TextBox
    Friend WithEvents cbFiltros As ComboBox
    Friend WithEvents btnFiltrar As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnEliminarFiltro As Button
    Friend WithEvents lblFiltrandoPor As Label
    Friend WithEvents btnPegarPolizas As Button
    Friend WithEvents btnExportCSV As Button
    Friend WithEvents lblTiempoDeProcesamiento As Label
    Friend WithEvents TabControlCancelacionPolizas As TabControl
    Friend WithEvents TabPageCanceladaPago As TabPage
    Friend WithEvents TabPageCanceladasPorDocumentos As TabPage
    Private WithEvents groupBox2 As GroupBox
    Private WithEvents GroupBox1 As GroupBox
    Private WithEvents GroupBox3 As GroupBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents ProgressBar2 As ProgressBar
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents GrdCancelacionesDocument As DataGridView
    Friend WithEvents DataGridViewCheckBoxColumn1 As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As DataGridViewTextBoxColumn
End Class
