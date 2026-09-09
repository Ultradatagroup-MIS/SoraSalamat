Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.IO
Public Class frmEnteghalEtelaat

    Private Sub frmEnteghalEtelaat_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetParameter()
        ClearForm()
        LoadCombo()

    End Sub
    Private Sub ClearForm()
        mskAzTarikh.Text = CodeDoreh.ToString + "0101"
        mskTaTarikh.Text = TarikhEmrooz
        cmbNoeSearch.SelectedIndex = -1
    End Sub
    Private Sub LoadCombo()

        cmbNoeSearch.Items.Add("قیمت های کالا")
        cmbNoeSearch.Items.Add("کالا")

        chlMarkazPakhsh.Items.Add("اربیل")
        chlMarkazPakhsh.Items.Add("بغداد")

    End Sub
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1396"

        Else
            UserName = commands.Split(";")(0)
            UserCode = commands.Split(";")(1)
            UserPassWord = commands.Split(";")(2)
            NameMahalFaal = commands.Split(";")(3)
            CodeMahalFaal = commands.Split(";")(4)
            PersonelCode = commands.Split(";")(5)
            PersonelName = commands.Split(";")(6)
            CodeDoreh = commands.Split(";")(7)

        End If
    End Sub

    Private Sub cmbNoeSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeSearch.SelectedIndexChanged
        Dim dtForm As New DataTable
        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        If cmbNoeSearch.SelectedIndex <> -1 Then
            Try
                '-------------- Load Combo Mah
                Using cn As New SqlConnection(ConnectionString)
                    Using cm As SqlCommand = cn.CreateCommand()
                        cn.Open()
                        cm.Parameters.Clear()
                        cm.CommandType = CommandType.StoredProcedure
                        If cmbNoeSearch.SelectedIndex = 0 Then
                            cm.CommandText = "[Global].[spEnteghalEtelaat_LoadGheymatKala]"
                            cm.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text.Replace("/", "").Trim)
                            cm.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text.Replace("/", "").Trim)
                        ElseIf cmbNoeSearch.SelectedIndex = 1 Then
                            cm.CommandText = "[Global].[spEnteghalEtelaat_LoadKala]"
                        End If
                        da.SelectCommand = cm
                        dt = New DataTable
                        da.Fill(dt)
                    End Using
                End Using
                dtForm = dt
                SetGridStyle(dtForm)

            Catch ex As Exception
                Throw New Exception("Error In--> Load Combo : " & ex.Message)
            Finally
            End Try
        End If
    End Sub
    Private Sub SetGridStyle(dtForm As DataTable)
        With Grid
            .DataSource = Nothing
            .DataSource = dtForm
            .SetDataBinding(dtForm, "")
            .RetrieveStructure()
        End With

        For i As Integer = 0 To Grid.CurrentTable.Columns.Count - 1
            Grid.CurrentTable.Columns.Item(i).Visible = False
        Next
      
        If cmbNoeSearch.SelectedIndex = 0 Then

            Grid.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            Grid.CurrentTable.Columns.Item("Taeed").Visible = True
            Grid.CurrentTable.Columns.Item("Taeed").Width = 20
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("Taeed").Position = 0
            Grid.CurrentTable.Columns.Item("Taeed").Selectable = True
            Grid.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            Grid.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            Grid.CurrentTable.Columns.Item("ccKala").Visible = False
            Grid.CurrentTable.Columns.Item("ccKala").Width = 50
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("ccKala").Position = 1
            Grid.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            Grid.CurrentTable.Columns.Item("NameKala").Visible = True
            Grid.CurrentTable.Columns.Item("NameKala").Width = 170
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("NameKala").Position = 2
            Grid.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("CodeKala").Caption = "کد کالا"
            Grid.CurrentTable.Columns.Item("CodeKala").Visible = True
            Grid.CurrentTable.Columns.Item("CodeKala").Width = 100
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("CodeKala").Position = 3
            Grid.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("nametaminkonandeh").Caption = "نام تامین کننده"
            Grid.CurrentTable.Columns.Item("nametaminkonandeh").Visible = True
            Grid.CurrentTable.Columns.Item("nametaminkonandeh").Width = 150
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("nametaminkonandeh").Position = 4
            Grid.CurrentTable.Columns.Item("nametaminkonandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("AzTarikh").Caption = "از تاریخ"
            Grid.CurrentTable.Columns.Item("AzTarikh").Visible = True
            Grid.CurrentTable.Columns.Item("AzTarikh").Width = 100
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("AzTarikh").Position = 5
            Grid.CurrentTable.Columns.Item("AzTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("TaTarikh").Caption = "تا تاریخ"
            Grid.CurrentTable.Columns.Item("TaTarikh").Visible = True
            Grid.CurrentTable.Columns.Item("TaTarikh").Width = 100
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("TaTarikh").Position = 6
            Grid.CurrentTable.Columns.Item("TaTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("gheymat").Caption = "قیمت"
            Grid.CurrentTable.Columns.Item("gheymat").Visible = True
            Grid.CurrentTable.Columns.Item("gheymat").Width = 100
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("gheymat").FormatString = "###,###"
            Grid.CurrentTable.Columns.Item("gheymat").Position = 7
            Grid.CurrentTable.Columns.Item("gheymat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("txtnoefieldgheymat").Caption = "نوع قیمت"
            Grid.CurrentTable.Columns.Item("txtnoefieldgheymat").Visible = True
            Grid.CurrentTable.Columns.Item("txtnoefieldgheymat").Width = 150
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("txtnoefieldgheymat").Position = 8
            Grid.CurrentTable.Columns.Item("txtnoefieldgheymat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("txtnoefieldmoshtary").Caption = "نوع مشتری"
            Grid.CurrentTable.Columns.Item("txtnoefieldmoshtary").Visible = True
            Grid.CurrentTable.Columns.Item("txtnoefieldmoshtary").Width = 150
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("txtnoefieldmoshtary").Position = 9
            Grid.CurrentTable.Columns.Item("txtnoefieldmoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        ElseIf cmbNoeSearch.SelectedIndex = 1 Then


            Grid.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            Grid.CurrentTable.Columns.Item("Taeed").Visible = True
            Grid.CurrentTable.Columns.Item("Taeed").Width = 20
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("Taeed").Position = 0
            Grid.CurrentTable.Columns.Item("Taeed").Selectable = True
            Grid.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            Grid.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            Grid.CurrentTable.Columns.Item("ccKala").Visible = False
            Grid.CurrentTable.Columns.Item("ccKala").Width = 50
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("ccKala").Position = 1
            Grid.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            Grid.CurrentTable.Columns.Item("NameKala").Visible = True
            Grid.CurrentTable.Columns.Item("NameKala").Width = 170
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("NameKala").Position = 2
            Grid.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("CodeKala").Caption = "کد کالا"
            Grid.CurrentTable.Columns.Item("CodeKala").Visible = True
            Grid.CurrentTable.Columns.Item("CodeKala").Width = 100
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("CodeKala").Position = 3
            Grid.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("NameTaminKonandeh").Caption = "نام تامین کننده"
            Grid.CurrentTable.Columns.Item("NameTaminKonandeh").Visible = True
            Grid.CurrentTable.Columns.Item("NameTaminKonandeh").Width = 150
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("NameTaminKonandeh").Position = 4
            Grid.CurrentTable.Columns.Item("NameTaminKonandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("NameBrand").Caption = "نام برند"
            Grid.CurrentTable.Columns.Item("NameBrand").Visible = True
            Grid.CurrentTable.Columns.Item("NameBrand").Width = 110
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("NameBrand").Position = 5
            Grid.CurrentTable.Columns.Item("NameBrand").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("txtsVahedeShomaresh").Caption = "واحد شمارش"
            Grid.CurrentTable.Columns.Item("txtsVahedeShomaresh").Visible = True
            Grid.CurrentTable.Columns.Item("txtsVahedeShomaresh").Width = 150
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("txtsVahedeShomaresh").Position = 6
            Grid.CurrentTable.Columns.Item("txtsVahedeShomaresh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("txtsg1").Caption = "گروه کالا 1"
            Grid.CurrentTable.Columns.Item("txtsg1").Visible = True
            Grid.CurrentTable.Columns.Item("txtsg1").Width = 150
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("txtsg1").Position = 7
            Grid.CurrentTable.Columns.Item("txtsg1").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("txtsg2").Caption = "گروه کالا 2"
            Grid.CurrentTable.Columns.Item("txtsg2").Visible = True
            Grid.CurrentTable.Columns.Item("txtsg2").Width = 150
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("txtsg2").Position = 8
            Grid.CurrentTable.Columns.Item("txtsg2").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            Grid.CurrentTable.Columns.Item("txtsg3").Caption = "گروه کالا 3"
            Grid.CurrentTable.Columns.Item("txtsg3").Visible = True
            Grid.CurrentTable.Columns.Item("txtsg3").Width = 150
            Grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            Grid.CurrentTable.Columns.Item("txtsg3").Position = 9
            Grid.CurrentTable.Columns.Item("txtsg3").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End If

    End Sub
End Class
