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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProcesarCancelaciones))
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GrdCancelaciones = New System.Windows.Forms.DataGridView()
        Me.Seleccionar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Poliza = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReglaSiniestros = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NombreCliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Beneficiario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NombreIntermediario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TipoIntermediario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InicioVigencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FinVigencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaVenceReal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Endosada = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BalanceGP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BalanceSF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Diferencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NombreSupervisor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SysFlexSegurosDataSet = New Cancelaciones.SysFlexSegurosDataSet()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnProcesar = New System.Windows.Forms.Button()
        Me.BtnCargarDatos = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTotalPolizas = New System.Windows.Forms.TextBox()
        Me.txtFiltro = New System.Windows.Forms.TextBox()
        Me.cbFiltros = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblTiempoDeProcesamiento = New System.Windows.Forms.Label()
        Me.timerTiempoTranscurrido = New System.Windows.Forms.Timer(Me.components)
        Me.txtPolizasFiltradas = New System.Windows.Forms.TextBox()
        Me.lblPolizasFiltradas = New System.Windows.Forms.Label()
        Me.lblPasoProceso = New System.Windows.Forms.Label()
        Me.lblFiltrandoPor = New System.Windows.Forms.Label()
        Me.txtTotalPolizasSeleccionadas = New System.Windows.Forms.TextBox()
        Me.lblTotalPolizasSeleccionadas = New System.Windows.Forms.Label()
        Me.btnPegarPolizas = New System.Windows.Forms.Button()
        Me.chkMostrarSeleccionadas = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnExportCSV = New System.Windows.Forms.Button()
        Me.btnFiltrar = New System.Windows.Forms.Button()
        Me.btnEliminarFiltro = New System.Windows.Forms.Button()
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
        Me.SysFlexSegurosDataSetBindingSource = New Cancelaciones.SysFlexSegurosDataSet()
        CType(Me.GrdCancelaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SysFlexSegurosDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SysFlexSegurosDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GrdCancelaciones
        '
        Me.GrdCancelaciones.AllowUserToOrderColumns = True
        Me.GrdCancelaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdCancelaciones.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Seleccionar, Me.Poliza, Me.ReglaSiniestros, Me.NombreCliente, Me.Beneficiario, Me.NombreIntermediario, Me.TipoIntermediario, Me.InicioVigencia, Me.FinVigencia, Me.FechaVenceReal, Me.Endosada, Me.BalanceGP, Me.BalanceSF, Me.Diferencia, Me.NombreSupervisor})
        Me.GrdCancelaciones.Location = New System.Drawing.Point(12, 100)
        Me.GrdCancelaciones.Name = "GrdCancelaciones"
        Me.GrdCancelaciones.Size = New System.Drawing.Size(1226, 413)
        Me.GrdCancelaciones.TabIndex = 0
        '
        'Seleccionar
        '
        Me.Seleccionar.DataPropertyName = "None"
        Me.Seleccionar.HeaderText = "Seleccionar"
        Me.Seleccionar.Name = "Seleccionar"
        Me.Seleccionar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Poliza
        '
        Me.Poliza.DataPropertyName = "Poliza"
        Me.Poliza.HeaderText = "Poliza"
        Me.Poliza.Name = "Poliza"
        '
        'ReglaSiniestros
        '
        Me.ReglaSiniestros.DataPropertyName = "ReglaSiniestros"
        Me.ReglaSiniestros.HeaderText = "+3 Siniestros"
        Me.ReglaSiniestros.Name = "ReglaSiniestros"
        '
        'NombreCliente
        '
        Me.NombreCliente.DataPropertyName = "NombreCliente"
        Me.NombreCliente.HeaderText = "Nombre del Cliente"
        Me.NombreCliente.Name = "NombreCliente"
        Me.NombreCliente.ReadOnly = True
        Me.NombreCliente.Width = 250
        '
        'Beneficiario
        '
        Me.Beneficiario.DataPropertyName = "Beneficiario"
        Me.Beneficiario.HeaderText = "Beneficiario"
        Me.Beneficiario.Name = "Beneficiario"
        Me.Beneficiario.ReadOnly = True
        Me.Beneficiario.Width = 150
        '
        'NombreIntermediario
        '
        Me.NombreIntermediario.DataPropertyName = "NombreIntermediario"
        Me.NombreIntermediario.HeaderText = "Nombre de Intermediario"
        Me.NombreIntermediario.Name = "NombreIntermediario"
        Me.NombreIntermediario.ReadOnly = True
        Me.NombreIntermediario.Width = 250
        '
        'TipoIntermediario
        '
        Me.TipoIntermediario.DataPropertyName = "TipoIntermediario"
        Me.TipoIntermediario.HeaderText = "Tipo de Intermediario"
        Me.TipoIntermediario.Name = "TipoIntermediario"
        Me.TipoIntermediario.ReadOnly = True
        '
        'InicioVigencia
        '
        Me.InicioVigencia.DataPropertyName = "InicioVigencia"
        Me.InicioVigencia.HeaderText = "Inicio de Vigencia"
        Me.InicioVigencia.Name = "InicioVigencia"
        Me.InicioVigencia.ReadOnly = True
        '
        'FinVigencia
        '
        Me.FinVigencia.DataPropertyName = "FinVigencia"
        Me.FinVigencia.HeaderText = "Fin de Vigencia"
        Me.FinVigencia.Name = "FinVigencia"
        Me.FinVigencia.ReadOnly = True
        '
        'FechaVenceReal
        '
        Me.FechaVenceReal.DataPropertyName = "FechaVenceReal"
        Me.FechaVenceReal.HeaderText = "Fecha Vencimiento Real"
        Me.FechaVenceReal.Name = "FechaVenceReal"
        Me.FechaVenceReal.ReadOnly = True
        '
        'Endosada
        '
        Me.Endosada.DataPropertyName = "Endosada"
        Me.Endosada.HeaderText = "Endosada"
        Me.Endosada.Name = "Endosada"
        Me.Endosada.ReadOnly = True
        '
        'BalanceGP
        '
        Me.BalanceGP.DataPropertyName = "BalanceGP"
        DataGridViewCellStyle1.Format = "C2"
        DataGridViewCellStyle1.NullValue = "0"
        Me.BalanceGP.DefaultCellStyle = DataGridViewCellStyle1
        Me.BalanceGP.HeaderText = "BalanceGP"
        Me.BalanceGP.Name = "BalanceGP"
        Me.BalanceGP.ReadOnly = True
        '
        'BalanceSF
        '
        Me.BalanceSF.DataPropertyName = "BalanceSF"
        DataGridViewCellStyle2.Format = "C2"
        DataGridViewCellStyle2.NullValue = "0"
        Me.BalanceSF.DefaultCellStyle = DataGridViewCellStyle2
        Me.BalanceSF.HeaderText = "BalanceSF"
        Me.BalanceSF.Name = "BalanceSF"
        Me.BalanceSF.ReadOnly = True
        '
        'Diferencia
        '
        Me.Diferencia.DataPropertyName = "Diferencia"
        DataGridViewCellStyle3.Format = "C2"
        DataGridViewCellStyle3.NullValue = "0"
        Me.Diferencia.DefaultCellStyle = DataGridViewCellStyle3
        Me.Diferencia.HeaderText = "Diferencia"
        Me.Diferencia.Name = "Diferencia"
        Me.Diferencia.ReadOnly = True
        '
        'NombreSupervisor
        '
        Me.NombreSupervisor.DataPropertyName = "NombreSupervisor"
        Me.NombreSupervisor.HeaderText = "Nombre de Supervisor"
        Me.NombreSupervisor.Name = "NombreSupervisor"
        Me.NombreSupervisor.ReadOnly = True
        Me.NombreSupervisor.Width = 250
        '
        'SysFlexSegurosDataSet
        '
        Me.SysFlexSegurosDataSet.DataSetName = "SysFlexSegurosDataSet"
        Me.SysFlexSegurosDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(230, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Cancelaciones a Procesar"
        '
        'btnProcesar
        '
        Me.btnProcesar.Enabled = False
        Me.btnProcesar.Location = New System.Drawing.Point(1148, 66)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.Size = New System.Drawing.Size(90, 28)
        Me.btnProcesar.TabIndex = 2
        Me.btnProcesar.Text = "Procesar"
        Me.btnProcesar.UseVisualStyleBackColor = True
        '
        'BtnCargarDatos
        '
        Me.BtnCargarDatos.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCargarDatos.Location = New System.Drawing.Point(12, 66)
        Me.BtnCargarDatos.Name = "BtnCargarDatos"
        Me.BtnCargarDatos.Size = New System.Drawing.Size(90, 28)
        Me.BtnCargarDatos.TabIndex = 3
        Me.BtnCargarDatos.Text = "Cargar Datos"
        Me.BtnCargarDatos.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(52, 526)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Total Polizas:"
        '
        'txtTotalPolizas
        '
        Me.txtTotalPolizas.Enabled = False
        Me.txtTotalPolizas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPolizas.Location = New System.Drawing.Point(128, 519)
        Me.txtTotalPolizas.Name = "txtTotalPolizas"
        Me.txtTotalPolizas.Size = New System.Drawing.Size(65, 20)
        Me.txtTotalPolizas.TabIndex = 5
        Me.txtTotalPolizas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtFiltro
        '
        Me.txtFiltro.Location = New System.Drawing.Point(540, 71)
        Me.txtFiltro.Name = "txtFiltro"
        Me.txtFiltro.Size = New System.Drawing.Size(221, 20)
        Me.txtFiltro.TabIndex = 6
        '
        'cbFiltros
        '
        Me.cbFiltros.FormattingEnabled = True
        Me.cbFiltros.Items.AddRange(New Object() {"Beneficiario", "Cliente", "Nombre de Intermediario", "Tipo de Intermediario", "Nombre de Supervisor"})
        Me.cbFiltros.Location = New System.Drawing.Point(315, 71)
        Me.cbFiltros.Name = "cbFiltros"
        Me.cbFiltros.Size = New System.Drawing.Size(159, 21)
        Me.cbFiltros.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(255, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Filtrar por :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(499, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Filtro :"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 516)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(342, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 13
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalirToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1246, 24)
        Me.MenuStrip1.TabIndex = 14
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.SalirToolStripMenuItem.Text = "Salir"
        '
        'lblTiempoDeProcesamiento
        '
        Me.lblTiempoDeProcesamiento.Enabled = False
        Me.lblTiempoDeProcesamiento.Location = New System.Drawing.Point(836, 9)
        Me.lblTiempoDeProcesamiento.Name = "lblTiempoDeProcesamiento"
        Me.lblTiempoDeProcesamiento.Size = New System.Drawing.Size(402, 14)
        Me.lblTiempoDeProcesamiento.TabIndex = 15
        Me.lblTiempoDeProcesamiento.Text = "Hora Inicio : {0} | Tiempo Transcurrido {1}"
        Me.lblTiempoDeProcesamiento.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTiempoDeProcesamiento.Visible = False
        '
        'txtPolizasFiltradas
        '
        Me.txtPolizasFiltradas.Enabled = False
        Me.txtPolizasFiltradas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPolizasFiltradas.Location = New System.Drawing.Point(128, 542)
        Me.txtPolizasFiltradas.Name = "txtPolizasFiltradas"
        Me.txtPolizasFiltradas.Size = New System.Drawing.Size(65, 20)
        Me.txtPolizasFiltradas.TabIndex = 17
        Me.txtPolizasFiltradas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtPolizasFiltradas.Visible = False
        '
        'lblPolizasFiltradas
        '
        Me.lblPolizasFiltradas.AutoSize = True
        Me.lblPolizasFiltradas.Location = New System.Drawing.Point(13, 549)
        Me.lblPolizasFiltradas.Name = "lblPolizasFiltradas"
        Me.lblPolizasFiltradas.Size = New System.Drawing.Size(112, 13)
        Me.lblPolizasFiltradas.TabIndex = 16
        Me.lblPolizasFiltradas.Text = "Total Polizas Filtradas:"
        Me.lblPolizasFiltradas.Visible = False
        '
        'lblPasoProceso
        '
        Me.lblPasoProceso.Location = New System.Drawing.Point(361, 521)
        Me.lblPasoProceso.Name = "lblPasoProceso"
        Me.lblPasoProceso.Size = New System.Drawing.Size(316, 14)
        Me.lblPasoProceso.TabIndex = 19
        Me.lblPasoProceso.Text = "..."
        Me.lblPasoProceso.Visible = False
        '
        'lblFiltrandoPor
        '
        Me.lblFiltrandoPor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFiltrandoPor.ForeColor = System.Drawing.Color.Red
        Me.lblFiltrandoPor.Location = New System.Drawing.Point(776, 71)
        Me.lblFiltrandoPor.Name = "lblFiltrandoPor"
        Me.lblFiltrandoPor.Size = New System.Drawing.Size(353, 20)
        Me.lblFiltrandoPor.TabIndex = 20
        Me.lblFiltrandoPor.Text = "..."
        Me.lblFiltrandoPor.Visible = False
        '
        'txtTotalPolizasSeleccionadas
        '
        Me.txtTotalPolizasSeleccionadas.Enabled = False
        Me.txtTotalPolizasSeleccionadas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPolizasSeleccionadas.Location = New System.Drawing.Point(1169, 518)
        Me.txtTotalPolizasSeleccionadas.Name = "txtTotalPolizasSeleccionadas"
        Me.txtTotalPolizasSeleccionadas.Size = New System.Drawing.Size(65, 20)
        Me.txtTotalPolizasSeleccionadas.TabIndex = 22
        Me.txtTotalPolizasSeleccionadas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblTotalPolizasSeleccionadas
        '
        Me.lblTotalPolizasSeleccionadas.AutoSize = True
        Me.lblTotalPolizasSeleccionadas.Location = New System.Drawing.Point(1020, 525)
        Me.lblTotalPolizasSeleccionadas.Name = "lblTotalPolizasSeleccionadas"
        Me.lblTotalPolizasSeleccionadas.Size = New System.Drawing.Size(143, 13)
        Me.lblTotalPolizasSeleccionadas.TabIndex = 21
        Me.lblTotalPolizasSeleccionadas.Text = "Total Polizas Seleccionadas:"
        '
        'btnPegarPolizas
        '
        Me.btnPegarPolizas.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnPegarPolizas.Enabled = False
        Me.btnPegarPolizas.Location = New System.Drawing.Point(108, 66)
        Me.btnPegarPolizas.Name = "btnPegarPolizas"
        Me.btnPegarPolizas.Size = New System.Drawing.Size(90, 28)
        Me.btnPegarPolizas.TabIndex = 23
        Me.btnPegarPolizas.Text = "Cargar Lista"
        Me.btnPegarPolizas.UseVisualStyleBackColor = True
        '
        'chkMostrarSeleccionadas
        '
        Me.chkMostrarSeleccionadas.AutoSize = True
        Me.chkMostrarSeleccionadas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkMostrarSeleccionadas.Location = New System.Drawing.Point(1014, 542)
        Me.chkMostrarSeleccionadas.Name = "chkMostrarSeleccionadas"
        Me.chkMostrarSeleccionadas.Size = New System.Drawing.Size(220, 17)
        Me.chkMostrarSeleccionadas.TabIndex = 24
        Me.chkMostrarSeleccionadas.Text = "Mostrar solo la(s) Póliza(s) Seleccionadas"
        Me.chkMostrarSeleccionadas.UseVisualStyleBackColor = True
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
        Me.btnExportCSV.Location = New System.Drawing.Point(1109, 60)
        Me.btnExportCSV.Name = "btnExportCSV"
        Me.btnExportCSV.Size = New System.Drawing.Size(35, 35)
        Me.btnExportCSV.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.btnExportCSV, "Descargar Archivo CSV")
        Me.btnExportCSV.UseVisualStyleBackColor = True
        '
        'btnFiltrar
        '
        Me.btnFiltrar.Enabled = False
        Me.btnFiltrar.Image = CType(resources.GetObject("btnFiltrar.Image"), System.Drawing.Image)
        Me.btnFiltrar.Location = New System.Drawing.Point(778, 66)
        Me.btnFiltrar.Name = "btnFiltrar"
        Me.btnFiltrar.Size = New System.Drawing.Size(87, 28)
        Me.btnFiltrar.TabIndex = 10
        Me.btnFiltrar.Text = "Filtrar"
        Me.btnFiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFiltrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFiltrar.UseVisualStyleBackColor = True
        Me.btnFiltrar.Visible = False
        '
        'btnEliminarFiltro
        '
        Me.btnEliminarFiltro.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEliminarFiltro.Image = CType(resources.GetObject("btnEliminarFiltro.Image"), System.Drawing.Image)
        Me.btnEliminarFiltro.Location = New System.Drawing.Point(871, 66)
        Me.btnEliminarFiltro.Name = "btnEliminarFiltro"
        Me.btnEliminarFiltro.Size = New System.Drawing.Size(87, 28)
        Me.btnEliminarFiltro.TabIndex = 18
        Me.btnEliminarFiltro.Text = "Borrar Filtrar"
        Me.btnEliminarFiltro.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarFiltro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEliminarFiltro.UseVisualStyleBackColor = True
        Me.btnEliminarFiltro.Visible = False
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
        DataGridViewCellStyle4.Format = "C2"
        DataGridViewCellStyle4.NullValue = "0"
        Me.DataGridViewTextBoxColumn11.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn11.HeaderText = "BalanceGP"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "BalanceSF"
        DataGridViewCellStyle5.Format = "C2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn12.HeaderText = "BalanceSF"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "Diferencia"
        DataGridViewCellStyle6.Format = "C2"
        DataGridViewCellStyle6.NullValue = "0"
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle6
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
        'SysFlexSegurosDataSetBindingSource
        '
        Me.SysFlexSegurosDataSetBindingSource.DataSetName = "SysFlexSegurosDataSet"
        Me.SysFlexSegurosDataSetBindingSource.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ProcesarCancelaciones
        '
        Me.AcceptButton = Me.btnFiltrar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnEliminarFiltro
        Me.ClientSize = New System.Drawing.Size(1246, 568)
        Me.Controls.Add(Me.btnExportCSV)
        Me.Controls.Add(Me.chkMostrarSeleccionadas)
        Me.Controls.Add(Me.btnPegarPolizas)
        Me.Controls.Add(Me.txtTotalPolizasSeleccionadas)
        Me.Controls.Add(Me.lblTotalPolizasSeleccionadas)
        Me.Controls.Add(Me.lblFiltrandoPor)
        Me.Controls.Add(Me.lblPasoProceso)
        Me.Controls.Add(Me.btnEliminarFiltro)
        Me.Controls.Add(Me.txtPolizasFiltradas)
        Me.Controls.Add(Me.lblPolizasFiltradas)
        Me.Controls.Add(Me.lblTiempoDeProcesamiento)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnFiltrar)
        Me.Controls.Add(Me.cbFiltros)
        Me.Controls.Add(Me.txtFiltro)
        Me.Controls.Add(Me.txtTotalPolizas)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BtnCargarDatos)
        Me.Controls.Add(Me.btnProcesar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GrdCancelaciones)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "ProcesarCancelaciones"
        Me.Text = "Procesar Cancelaciones"
        CType(Me.GrdCancelaciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SysFlexSegurosDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.SysFlexSegurosDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GrdCancelaciones As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnProcesar As System.Windows.Forms.Button
    Friend WithEvents BtnCargarDatos As System.Windows.Forms.Button
    Friend WithEvents SysFlexSegurosDataSet As Cancelaciones.SysFlexSegurosDataSet
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTotalPolizas As System.Windows.Forms.TextBox
    Friend WithEvents txtFiltro As System.Windows.Forms.TextBox
    Friend WithEvents cbFiltros As System.Windows.Forms.ComboBox
    Friend WithEvents btnFiltrar As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents SalirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblTiempoDeProcesamiento As System.Windows.Forms.Label
    Friend WithEvents timerTiempoTranscurrido As System.Windows.Forms.Timer
    Friend WithEvents txtPolizasFiltradas As System.Windows.Forms.TextBox
    Friend WithEvents lblPolizasFiltradas As System.Windows.Forms.Label
    Friend WithEvents btnEliminarFiltro As System.Windows.Forms.Button
    Friend WithEvents lblPasoProceso As System.Windows.Forms.Label
    Friend WithEvents lblFiltrandoPor As System.Windows.Forms.Label
    Friend WithEvents txtTotalPolizasSeleccionadas As System.Windows.Forms.TextBox
    Friend WithEvents lblTotalPolizasSeleccionadas As System.Windows.Forms.Label
    Friend WithEvents btnPegarPolizas As System.Windows.Forms.Button
    Friend WithEvents chkMostrarSeleccionadas As System.Windows.Forms.CheckBox
    Friend WithEvents btnExportCSV As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Seleccionar As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Poliza As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReglaSiniestros As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NombreCliente As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Beneficiario As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NombreIntermediario As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TipoIntermediario As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InicioVigencia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FinVigencia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaVenceReal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Endosada As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BalanceGP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BalanceSF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Diferencia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NombreSupervisor As System.Windows.Forms.DataGridViewTextBoxColumn
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
End Class
