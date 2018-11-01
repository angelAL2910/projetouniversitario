﻿Imports System
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Linq
Imports System.IO
Imports System.Text


Public Class ProcesarCancelaciones

    Dim dt As New DataTable()
    Dim dtReglaSiniestro As New DataTable()
    Dim bindingSource1 As New BindingSource()

    Dim _HoraInicialProceso As DateTime

    Dim Hilo1, Hilo2 As Thread

    Public Shared lstPolizasSeleccionadas As New HashSet(Of String)

    Dim frmPegarPolizas As frmPegarPolizas
    Dim dtp As New DateTimePicker()


#Region "Events"

    Private Sub ProcesarCancelaciones_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Select Case e.KeyChar

            Case ChrW(Keys.Escape)
                btnEliminarFiltro_Click(Me, Nothing)
            Case ChrW(Keys.Enter)
                btnFiltrar_Click(Me, Nothing)
            Case Else
                Exit Sub
        End Select
    End Sub
    Private Sub ProcesarCancelaciones_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ProgressBar1.Visible = False


        Dim column = New CalendarColumn()
        column.DataPropertyName = "Date"
        column.HeaderText = "Fecha Cancelación"

        Me.GrdCancelaciones.Columns.Insert(3, column)


        ''dtp.TextChanged += New EventHandler(dtp_TextChange)


        'lstPolizasSeleccionadas = New DataGridViewRow
        'Try
        '    MsgBox("UserName: " + Environment.UserDomainName + "\" + Environment.UserName)
        '    MsgBox("MachineName: " + Environment.MachineName)
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub cmdProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        'Dim UserLoggued As String = String.Empty
        'UserLoggued = IIf(Environment.GetEnvironmentVariable("USERNAME") Is Nothing, "Administrador..", Environment.GetEnvironmentVariable("USERNAME"))

        If MsgBox("Esta seguro de cancelar la(s) póliza(s) seleccionada(s)? ", MsgBoxStyle.OkCancel, String.Format("{0} - {1}", "Cancelación en Lote", Now)) = MsgBoxResult.Cancel Then
            Exit Sub
        End If



        If Not ReglaSiniestrosHasCancelationDate() Then
            Exit Sub
        End If


        Progreso(0)

        _HoraInicialProceso = Now

        lblTiempoDeProcesamiento.Visible = True
        lblTiempoDeProcesamiento.Text = String.Empty

        Me.ProgressBar1.Visible = True
        lblPasoProceso.Visible = True

        ' Me.ProgressBar1.Value = 25

        Me.Cursor = Cursors.WaitCursor

        Hilo1 = New Thread(AddressOf IniciaProcesoCancelaciones)
        Hilo1.Start()

        Hilo2 = New Thread(AddressOf Progreso)
        Hilo2.Start(0)


        'Me.ProgressBar1.Value = 80

        'Try
        '    ProcesarCancelacionesFinal(UserLoggued)
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        '    Exit Sub
        'End Try

        'dt.AcceptChanges()
        'GrdCancelaciones.Refresh()

        'Me.txtTotalPolizas.Text = dt.Rows.Count()

        'Me.Cursor = Cursors.Default


        'MsgBox(String.Format("{0} - {1}", "Registro(s) Procesado(s) Satisfactoriamente", Now))


    End Sub

    Private Sub BtnCargarDatos_Click(sender As Object, e As EventArgs) Handles BtnCargarDatos.Click
        Dim UserLoggued As String = Environment.GetEnvironmentVariable("USERNAME")

        _HoraInicialProceso = Now
        lstPolizasSeleccionadas.Clear()
        txtTotalPolizasSeleccionadas.Text = String.Empty

        Progreso(0)
        lblTiempoDeProcesamiento.Visible = True
        lblTiempoDeProcesamiento.Text = String.Empty

        Hilo1 = New Thread(AddressOf CargarDatos)
        Hilo1.Start(UserLoggued)

        Me.ProgressBar1.Visible = True
        lblPasoProceso.Visible = True

        Me.Cursor = Cursors.WaitCursor

        Hilo2 = New Thread(AddressOf Progreso)
        Hilo2.Start(0)


    End Sub

    Private Sub btnFiltrar_Click(sender As Object, e As EventArgs) Handles btnFiltrar.Click

        If IsNothing(txtFiltro.Text.Trim) Then Exit Sub

        Dim filtro As String = String.Empty

        If Me.cbFiltros.SelectedIndex = 0 Then
            filtro = String.Format("Beneficiario Like '{0}%'", txtFiltro.Text)
        ElseIf Me.cbFiltros.SelectedIndex = 1 Then
            filtro = String.Format("NombreCliente Like '{0}%'", txtFiltro.Text)
        ElseIf Me.cbFiltros.SelectedIndex = 2 Then
            filtro = String.Format("NombreIntermediario Like '{0}%'", txtFiltro.Text)
        ElseIf Me.cbFiltros.SelectedIndex = 3 Then
            filtro = String.Format("TipoIntermediario Like '{0}%'", txtFiltro.Text)
        ElseIf Me.cbFiltros.SelectedIndex = 4 Then
            filtro = String.Format("NombreSupervisor Like '{0}%'", txtFiltro.Text)
        End If

        bindingSource1.Filter = filtro

        MostrarTotalRegistroFiltrados(True)

        btnEliminarFiltro.Enabled = True

        txtPolizasFiltradas.Text = String.Format("{0:N0}", bindingSource1.Count)
    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        Me.Close()
    End Sub


    Private Sub btnEliminarFiltro_Click(sender As Object, e As EventArgs) Handles btnEliminarFiltro.Click
        cbFiltros.SelectedIndex = -1

        txtFiltro.Text = String.Empty

        btnFiltrar_Click(Me, Nothing)

        MostrarTotalRegistroFiltrados(False)


    End Sub

    Private Sub txtFiltro_KeyUp(sender As Object, e As KeyEventArgs) Handles txtFiltro.KeyUp
        Dim filtro As String = String.Empty

        GrdCancelaciones.CommitEdit(DataGridViewDataErrorContexts.Commit)

        Select Case e.KeyCode
            Case Keys.Enter
                SeleccionaCoincidencia()
                'btnFiltrar_Click(Me, Nothing)

            Case Keys.Escape
                btnEliminarFiltro_Click(Me, Nothing)

                BuscaryMarcarLineasSeleccionadas()

            Case Else
                If cbFiltros.SelectedIndex = -1 Then
                    lblFiltrandoPor.Visible = True
                    If IsNumeric(Mid(txtFiltro.Text, 1, 1)) Then
                        filtro = String.Format("Poliza Like '{0}%'", txtFiltro.Text)
                        lblFiltrandoPor.Text = String.Format("Filtrando Por: {0}", "Póliza(s)")
                    Else
                        filtro = String.Format("NombreCliente Like '{0}%' or Beneficiario Like '{0}%'", txtFiltro.Text)
                        If txtFiltro.Text.Length > 0 Then lblFiltrandoPor.Text = String.Format("Filtrando Por: {0}", "Nombres de Cliente(s) y Beneficiorio(s)") Else lblFiltrandoPor.Text = String.Empty
                    End If
                Else
                    lblFiltrandoPor.Visible = False
                    btnFiltrar_Click(Me, Nothing)

                End If
        End Select



        If filtro.Length > 0 Then

            bindingSource1.Filter = filtro

            BuscaryMarcarLineasSeleccionadas()

            MostrarTotalRegistroFiltrados(True)

            btnEliminarFiltro.Enabled = True

            If txtFiltro.Text.Length > 0 Then txtPolizasFiltradas.Text = String.Format("{0:N0}", bindingSource1.Count) Else txtPolizasFiltradas.Text = String.Empty
    
        End If
    End Sub

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        If txtFiltro.Text.Length = 0 Then
            lblFiltrandoPor.Visible = False
            txtPolizasFiltradas.Text = String.Empty
        End If
    End Sub

    Private Sub GrdCancelaciones_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GrdCancelaciones.CellContentClick

        GrdCancelaciones.CommitEdit(DataGridViewDataErrorContexts.Commit)

        If e.RowIndex = -1 Or e.ColumnIndex = -1 Then Exit Sub

        Dim valorCelda As Boolean = GrdCancelaciones.Item(0, e.RowIndex).Value

        Dim _Poliza As String = IIf(Not GrdCancelaciones.Item(1, e.RowIndex) Is Nothing, GrdCancelaciones.Item(1, e.RowIndex).Value, "").ToString()

        If String.IsNullOrEmpty(_Poliza) Then
            Exit Sub
        End If

        If valorCelda = True Then
            lstPolizasSeleccionadas.Add(_Poliza)
        Else
            lstPolizasSeleccionadas.Remove(_Poliza)
        End If

        If lstPolizasSeleccionadas.Count > 0 Then btnProcesar.Invoke(Sub() btnProcesar.Enabled = True) Else btnProcesar.Invoke(Sub() btnProcesar.Enabled = False)

        txtTotalPolizasSeleccionadas.Text = lstPolizasSeleccionadas.Count
    End Sub

    Private Sub btnPegarPolizas_Click(sender As Object, e As EventArgs) Handles btnPegarPolizas.Click


        If frmPegarPolizas Is Nothing Then
            frmPegarPolizas = New frmPegarPolizas
        ElseIf frmPegarPolizas.IsDisposed Then
            frmPegarPolizas = New frmPegarPolizas
        End If


        frmPegarPolizas.Show()
        frmPegarPolizas.Focus()

    End Sub

    Private Sub GrdCancelaciones_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles GrdCancelaciones.DataBindingComplete
        If GrdCancelaciones.RowCount <= 1 Then btnPegarPolizas.Invoke(Sub() btnPegarPolizas.Enabled = False) Else btnPegarPolizas.Invoke(Sub() btnPegarPolizas.Enabled = True)

        If lstPolizasSeleccionadas.Count > 0 Then btnProcesar.Invoke(Sub() btnProcesar.Enabled = True) Else btnProcesar.Invoke(Sub() btnProcesar.Enabled = False)

    End Sub


#End Region

#Region "Methods"

    Private Sub ProcederConCancelaciones(username As String)


        For Each row As DataGridViewRow In GrdCancelaciones.SelectedRows
            Try

                If row.Cells(2).Value.ToString().Equals("SI") Then
                    ProcesarCancelacionesReglaSiniestros(row.Cells(1).Value.ToString(), DateTime.Parse(row.Cells(3).Value.ToString()), username)
                Else
                    ProcesarCancelaciones(row.Cells(1).Value.ToString(), username)
                    End If


                    Me.GrdCancelaciones.Rows.Remove(row)
            Catch ex As Exception
                MsgBox(ex.Message, , String.Format("{0} - {1}", Me.Text, Now))
            End Try
        Next
    End Sub

    Private Sub SeleccionaLineasMarcadas()
        For Each Row As DataGridViewRow In GrdCancelaciones.Rows
            If Not Row.Cells(2).Value Is Nothing Then

                If Row.Cells(0).Value = True And Not String.IsNullOrEmpty(Row.Cells(2).Value.ToString()) Then
                    Row.Selected = True
                End If

            End If

        Next
    End Sub

    Private Sub Progreso(Optional valor As Object = 0)
        ProgressBar1.Invoke(Sub() ProgressBar1.Value = Integer.Parse(valor))
        Thread.Sleep(1000)

        If valor = 100 Then
            lblTiempoDeProcesamiento.Invoke(Sub() lblTiempoDeProcesamiento.Text = String.Format("Hora Inicio : {0} | Tiempo Transcurrido : {1}", _HoraInicialProceso, String.Format("{0:dd:mm:ss}", tiemptranscurrido)))

            ProgressBar1.Invoke(Sub() ProgressBar1.Visible = False)
            lblPasoProceso.Invoke(Sub() lblPasoProceso.Visible = False)

            lstPolizasSeleccionadas.Clear()

            txtTotalPolizasSeleccionadas.Invoke(Sub() txtTotalPolizasSeleccionadas.Text = String.Empty)

            Me.Invoke(Sub() Me.Cursor = Cursors.Default)
        End If
    End Sub

    Public Sub CargarDatos(Optional ByVal username As Object = "AUTOMATICO")
        'Me.ProgressBar1.Visible = True
        'Me.ProgressBar1.Value = 40
        lblPasoProceso.Invoke(Sub() lblPasoProceso.Text = "Inicializando...")
        Progreso(20)

        If Not GrdCancelaciones.DataSource Is Nothing Then
            GrdCancelaciones.Invoke(Sub() GrdCancelaciones.DataSource = Nothing)
        End If

        lblPasoProceso.Invoke(Sub() lblPasoProceso.Text = "Conectando...")
        Progreso(40)


        Dim connectionString As String = GetConnectionString()
        Using connection As SqlConnection = _
          New SqlConnection(connectionString)
            connection.Open()
            Dim cmd As New SqlCommand("EXEC [dbo].[SPCCancelarPolizasAutomaticasNew_Listado]  30,'" & String.Format(username) & "',10,0", connection)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 600
            Dim da As New SqlDataAdapter(cmd)
            Try
                If dt.Rows.Count() > 0 Then
                    dt.Clear()
                End If

                lblPasoProceso.Invoke(Sub() lblPasoProceso.Text = "Cargando Registros...")
                Progreso(60)

                da.Fill(dt)



                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim cmd2 As New SqlCommand("EXEC [dbo].[SPCCancelarPolizasAutomaticasNew_ListadoReglaSiniestros]  30,'" & String.Format(username) & "',10,0", connection)
                cmd2.CommandType = CommandType.Text
                cmd2.CommandTimeout = 600
                Dim da2 As New SqlDataAdapter(cmd2)
                If dtReglaSiniestro.Rows.Count() > 0 Then
                    dtReglaSiniestro.Clear()
                End If
                lblPasoProceso.Invoke(Sub() lblPasoProceso.Text = "Cargando Registros Regla Siniestros...")
                Progreso(70)
                da2.Fill(dtReglaSiniestro)


                dtReglaSiniestro.Merge(dt)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                'Dim dt = New DataTable()
                'dt.Columns.Add("Date", GetType(DateTime))
                'dt.Rows.Add(DateTime.Now)
                'dt.Rows.Add(DateTime.Now)


                'Dim column = New CalendarColumn()
                'column.DataPropertyName = "Date"
                'column.HeaderText = "Fecha Cancelación"

                'dt.Columns.Insert(3, column)




                lblPasoProceso.Invoke(Sub() lblPasoProceso.Text = "Mostrando...")
                Progreso(80)
                'Formato GRID
                GrdCancelaciones.AutoGenerateColumns = False



                'DATA BIND
                bindingSource1.DataSource = dtReglaSiniestro
                GrdCancelaciones.Invoke(Sub() GrdCancelaciones.DataSource = bindingSource1)

            Catch ex As Exception
                MsgBox(ex.Message, , String.Format("{0} - {1}", Me.Text, Now))
            End Try

            Progreso(100)

            Me.txtTotalPolizas.Invoke(Sub() txtTotalPolizas.Text = String.Format("{0:N0}", dtReglaSiniestro.Rows.Count))


        End Using


        lblPasoProceso.Invoke(Sub() lblPasoProceso.Text = String.Empty)
        ProgressBar1.Invoke(Sub() ProgressBar1.Visible = False)
        lblPasoProceso.Invoke(Sub() lblPasoProceso.Visible = False)

        btnFiltrar.Invoke(Sub() btnFiltrar.Enabled = True)

        dt.AcceptChanges()
        dtReglaSiniestro.AcceptChanges()



        Hilo1.Abort()
        Hilo2.Abort()

        Hilo1.Join()
        Hilo2.Join()
        Me.Cursor = Cursors.Default



    End Sub

    Private Function GetConnectionString() As String
        Return ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
    End Function

    Public Sub ProcesarCancelacionesFinal(ByVal UserLoggued As String)
        Dim connectionString As String = GetConnectionString()

        Using connection As SqlConnection = _
           New SqlConnection(connectionString)
            connection.Open()
            Dim cmd As SqlCommand = New SqlCommand("EXEC SPCCancelarPolizasAutomaticasNew  30,'" & UserLoggued & "',10,1; EXEC [dbo].[SPCCancelarPolizasAutomaticasNew_ReglaSiniestros]", connection)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 900

            Try
                cmd.ExecuteNonQuery()
                Progreso(90)

            Catch ex As Exception
                MsgBox(ex.Message, , String.Format("{0} - {1}", Me.Text, Now))
            End Try

        End Using

    End Sub

    Public Sub ProcesarCancelaciones(ByVal Poliza As String, ByVal UserLoggued As String)
        Dim connectionString As String = GetConnectionString()

        Using connection As SqlConnection = _
           New SqlConnection(connectionString)
            connection.Open()

            Dim cmd As SqlCommand = New SqlCommand("SP_ProcesarCancelaciones", connection)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Poliza", Poliza)
            cmd.Parameters.AddWithValue("@UserLoggued", UserLoggued)
            cmd.CommandTimeout = 600

            Try
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox(ex.Message, , String.Format("{0} - {1}", Me.Text, Now))
            End Try

        End Using
    End Sub

    Public Sub ProcesarCancelacionesReglaSiniestros(ByVal Poliza As String, ByVal FechaCancelacion As DateTime, ByVal UserLoggued As String)
        Dim connectionString As String = GetConnectionString()

        Using connection As SqlConnection = _
           New SqlConnection(connectionString)
            connection.Open()

            Dim cmd As SqlCommand = New SqlCommand("SP_ProcesarCancelacionesReglaSiniestros", connection)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Poliza", Poliza)
            cmd.Parameters.AddWithValue("@UserLoggued", UserLoggued)
            cmd.Parameters.AddWithValue("@FechaCancelacion", FechaCancelacion)

            cmd.CommandTimeout = 600

            Try
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox(ex.Message, , String.Format("{0} - {1}", Me.Text, Now))
            End Try

        End Using
    End Sub

    Public Sub BorrarListaPoliza()
        Dim connectionString As String = GetConnectionString()

        Using connection As SqlConnection = _
           New SqlConnection(connectionString)
            connection.Open()

            Dim cmd As SqlCommand = New SqlCommand("truncate table tmpListaPoliza", connection)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 600
            Try
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox(ex.Message, , String.Format("{0} - {1}", Me.Text, Now))
            End Try

        End Using
    End Sub

    Public Sub BorrarListaPolizaReglaSiniestros()
        Dim connectionString As String = GetConnectionString()

        Using connection As SqlConnection = _
           New SqlConnection(connectionString)
            connection.Open()

            Dim cmd As SqlCommand = New SqlCommand("truncate table tmpListaPolizaReglaSiniestros", connection)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 600
            Try
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox(ex.Message, , String.Format("{0} - {1}", Me.Text, Now))
            End Try

        End Using
    End Sub






    Private Function tiemptranscurrido() As String

        Dim _tiempo As String = (Now - _HoraInicialProceso).ToString()

        Return String.Format("{0:hh:mm:ss.}", _tiempo.Substring(1, 8))


    End Function

    Private Sub IniciaProcesoCancelaciones()
        Dim UserLoggued As String = String.Empty
        UserLoggued = IIf(Environment.GetEnvironmentVariable("USERNAME") Is Nothing, "Administrador..", Environment.GetEnvironmentVariable("USERNAME"))


        'Borrar tmpListaPoliza

        lblPasoProceso.Invoke(Sub() lblPasoProceso.Text = "Inicializando...")
        Progreso(20)
        BorrarListaPoliza()
        BorrarListaPolizaReglaSiniestros()



        'Insertar Poliza e Historico
        Progreso(40)
        lblPasoProceso.Invoke(Sub() lblPasoProceso.Text = "Seleccionado Póliza(s)...")
        SeleccionaLineasMarcadas()



        'Inicia el Proceso de Cancelaciones
        Progreso(60)
        lblPasoProceso.Invoke(Sub() lblPasoProceso.Text = "Cancelando Póliza(s) Seleccionada(s)...")
        ProcederConCancelaciones(UserLoggued)



        Try
            'lblPasoProceso.Text = ("Cancelando Registros Seleccionados...")
            Progreso(80)
            lblPasoProceso.Invoke(Sub() lblPasoProceso.Text = "Verificando Póliza(s)...")
            ProcesarCancelacionesFinal(UserLoggued)


        Catch ex As Exception
            MsgBox(ex.Message, , String.Format("{0} - {1}", Me.Text, Now))
            Progreso(0)
            Exit Sub
        End Try

        Me.ProgressBar1.Invoke(Sub() ProgressBar1.Visible = False)
        lblPasoProceso.Invoke(Sub() lblPasoProceso.Visible = False)

        lstPolizasSeleccionadas.Clear()

        lblPasoProceso.Invoke(Sub() lblPasoProceso.Text = "Actualizando Datos en Pantalla...")


        dt.AcceptChanges()
        dtReglaSiniestro.AcceptChanges()
        GrdCancelaciones.Invoke(Sub() GrdCancelaciones.Refresh())


        Progreso(100)



        MsgBox(String.Format("{0} - {1}", "Registro(s) Procesado(s) Satisfactoriamente", Now))


        Hilo1.Abort()
        Hilo1.Join()

        Me.txtTotalPolizas.Invoke(Sub() Me.txtTotalPolizas.Text = String.Format("{0:N0}", dt.Rows.Count))

        Me.Invoke(Sub() Me.Cursor = Cursors.Default)
    End Sub

    Private Sub MostrarTotalRegistroFiltrados(valor As Boolean)
        lblPolizasFiltradas.Visible = valor
        txtPolizasFiltradas.Visible = valor
    End Sub

    Private Sub SeleccionaCoincidencia()

        If bindingSource1.Count = 1 Then
            GrdCancelaciones.Rows(0).Cells(0).Value = True
            SeleccionarPoliza(GrdCancelaciones.Rows(0).Cells(1).Value)
        End If

        'txtTotalPolizasSeleccionadas.Text = lstPolizasSeleccionadas.Count

    End Sub

    Public Sub SeleccionarPoliza(NumeroPoliza As String)

        lstPolizasSeleccionadas.Add(NumeroPoliza)

        BuscaryMarcarLineasSeleccionadas()

        txtTotalPolizasSeleccionadas.Text = lstPolizasSeleccionadas.Count

        If lstPolizasSeleccionadas.Count > 0 Then btnProcesar.Invoke(Sub() btnProcesar.Enabled = True)

    End Sub

    Private Sub BuscaryMarcarLineasSeleccionadas()
        If Not IsNothing(lstPolizasSeleccionadas) Then
            For Each pol In lstPolizasSeleccionadas

                Dim resultado = (From dr As DataGridViewRow In GrdCancelaciones.Rows
                                    Where dr.Cells(1).Value = pol)

                If resultado.Any Then
                    GrdCancelaciones.Rows(resultado.FirstOrDefault.Index).Cells(0).Value = True
                End If




            Next
        End If
    End Sub
#End Region



    'Private Sub GrdCancelaciones_Validated(sender As Object, e As EventArgs) Handles GrdCancelaciones.Validated
    '    If GrdCancelaciones.RowCount = 0 Then
    '        btnPegarPolizas.Invoke(Sub() btnPegarPolizas.Enabled = False)
    '        btnProcesar.Invoke(Sub() btnProcesar.Enabled = False)
    '    End If
    'End Sub

    Private Sub chkMostrarSeleccionadas_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostrarSeleccionadas.CheckedChanged
        Dim filtro As String = String.Empty

        Dim _polizas As String = String.Empty

        For Each Poliza As String In lstPolizasSeleccionadas
            _polizas += "'" & Poliza & "', "
        Next

        If chkMostrarSeleccionadas.Checked Then

            filtro = String.Format("Poliza in ({0})", _polizas.Remove(_polizas.Length - 1))
            bindingSource1.Filter = filtro

        Else
            bindingSource1.Filter = Nothing
        End If

        BuscaryMarcarLineasSeleccionadas()
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup

    End Sub

    Private Sub btnExportCSV_Click(sender As Object, e As EventArgs) Handles btnExportCSV.Click
        exportGridToCsv()
    End Sub

    Private Sub exportGridToCsv()
        Dim _nombreArchivo As String = String.Format("{5}\\Cancelaciones_{0}{1}{2}{3:00}{4:00}.csv", Now.Year, Now.Month, Now.Day, Now.Hour, Now.Minute, Environment.CurrentDirectory)
        Dim _contenido As String = String.Empty
        Dim _fileStream As New StreamWriter(_nombreArchivo, True, UnicodeEncoding.Default)

        'Try

        For r = 0 To GrdCancelaciones.Rows.Count - 2

            For Each col As DataGridViewColumn In GrdCancelaciones.Columns
                If r = 0 Then
                    If col.Index = 1 Then
                        _contenido = String.Format("{0}"",", ControlChars.Quote + col.HeaderText)
                    ElseIf col.Index > 1 Then
                        _contenido = String.Format("{0}""{1}", _contenido + ControlChars.Quote + col.HeaderText, ",")
                    End If
                Else
                    If col.Index = 1 Then
                        _contenido = String.Format("{0}"",", ControlChars.Quote + col.DataGridView.Rows.Item(r).Cells(col.Index).Value.ToString)
                    ElseIf col.Index > 1 Then
                        _contenido = String.Format("{0}""{1}", _contenido + ControlChars.Quote + col.DataGridView.Rows.Item(r).Cells(col.Index).Value.ToString, ",")
                    End If
                End If

            Next

            Dim Linea As String = _contenido.Substring(0, _contenido.Length - 1)

            _fileStream.WriteLine(Linea)



        Next

        _fileStream.Close()
        MsgBox("Exportación terminada.")
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub SysFlexSegurosDataSetBindingSource_CurrentChanged(sender As Object, e As EventArgs) 

    End Sub

    Private Sub GrdCancelaciones_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles GrdCancelaciones.DataError


        Try

        Catch ex As Exception

        End Try

    End Sub

    Private Function ReglaSiniestrosHasCancelationDate() As Boolean
        Dim res As Boolean = True

        SeleccionaLineasMarcadas()

        For Each row As DataGridViewRow In GrdCancelaciones.SelectedRows
            Try

                If row.Cells(2).Value.ToString().Equals("SI") Then
                    If row.Cells(3).Value Is Nothing Then
                        MsgBox("Debe especificar una fecha de cancelación, para las Pólizas con la regla de siniestros. Verificar Póliza : " & row.Cells(1).Value, , String.Format("{0} - {1}", Me.Text, Now))
                        res = False

                    End If

                End If



            Catch ex As Exception
                MsgBox(ex.Message, , String.Format("{0} - {1}", Me.Text, Now))
            End Try
        Next

        Return res

    End Function

End Class
