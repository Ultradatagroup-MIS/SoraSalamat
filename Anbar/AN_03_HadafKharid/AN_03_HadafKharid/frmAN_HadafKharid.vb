Public Class frmAN_HadafKharid
#Region "Variable AND Constant Declration"

    Dim cmTitr As CurrencyManager
    Dim dvTitr As DataView
    Dim tCodeCounter As Long
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim ttEdit As ToolTip = New ToolTip

#End Region
#Region "Form Event Code"
    Private Sub frmAN_HadafKharid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        Search()
    End Sub
    Private Sub GridEXTitr_DoubleClick(sender As Object, e As EventArgs) Handles GridEXTitr.DoubleClick
        If dvForm.Count = 0 Then
            Exit Sub
        End If

        If GridEXTitr.CurrentRow.RowType = RowType.FilterRow Then
            Exit Sub
        End If

        Dim frm As New frmAN_AddNewHadaf

        frm.ModeForm = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        frm.ccHadaf = GridEXTitr.CurrentRow.Cells("ccHadafKharid").Text.Replace(",", "")

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        Search()
    End Sub
    Private Sub GridEXTitr_MouseEnter(sender As Object, e As EventArgs) Handles GridEXTitr.MouseEnter
        If GridEXTitr.RowCount > 0 Then
            ttEdit.Show("جهت ویرایش ، بر روی هدف مورد نظر دبل کلیک نمایید .", GridEXTitr)
            lblEditHadaf.Visible = True
        End If
    End Sub
    Private Sub GridEXTitr_MouseLeave(sender As Object, e As EventArgs) Handles GridEXTitr.MouseLeave
        ttEdit.Hide(GridEXTitr)
        lblEditHadaf.Visible = False
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then
            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1394"
            txtCaption = "هدف خرید"
            ObjCode.UserName = UserName
        Else
            UserName = commands.Split(";")(0)
            UserCode = commands.Split(";")(1)
            UserPassWord = commands.Split(";")(2)
            NameMahalFaal = commands.Split(";")(3)
            CodeMahalFaal = commands.Split(";")(4)
            PersonelCode = commands.Split(";")(5)
            PersonelName = commands.Split(";")(6)
            CodeDoreh = commands.Split(";")(7)
            txtCaption = commands.Split(";")(8)
            Me.Text = txtCaption
            ObjCode.UserName = UserName
        End If
    End Sub
    Private Sub Search()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_Hadaf") Then
            dsForm.Tables.Remove("tbl_Hadaf")
        End If

        Try
            strSQL = "WareHouse.spHadafKharid_Search "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Hadaf")

            dvForm = New DataView(dsForm.Tables("tbl_Hadaf"))
            dvForm.Sort = "AzTarikh ASC"

            dvForm.AllowNew = False
            dvForm.AllowDelete = False
            dvForm.AllowEdit = True

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

            SetGrid()
            With GridEXTitr
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvForm
            End With

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> Search")
        End Try
    End Sub
    Private Sub SetGrid()
        If dvForm.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Hadaf").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Hadaf").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("CodeDoreh").Caption = "دوره"
            GridEXTitr.CurrentTable.Columns.Item("CodeDoreh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("CodeDoreh").Width = 60
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeDoreh").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("CodeDoreh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("CodeDoreh").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoeKalaeiHadaf").Caption = "نوع هدف کالایی"
            GridEXTitr.CurrentTable.Columns.Item("txtNoeKalaeiHadaf").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoeKalaeiHadaf").Width = 180
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoeKalaeiHadaf").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("txtNoeKalaeiHadaf").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("txtNoeKalaeiHadaf").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("AzTarikhSlash").Caption = "از تاريخ"
            GridEXTitr.CurrentTable.Columns.Item("AzTarikhSlash").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("AzTarikhSlash").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("AzTarikhSlash").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("AzTarikhSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("AzTarikhSlash").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TaTarikhSlash").Caption = "تا تاريخ"
            GridEXTitr.CurrentTable.Columns.Item("TaTarikhSlash").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TaTarikhSlash").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TaTarikhSlash").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("TaTarikhSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("TaTarikhSlash").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameTaminKonandeh").Caption = "تامین کننده"
            GridEXTitr.CurrentTable.Columns.Item("NameTaminKonandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameTaminKonandeh").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameTaminKonandeh").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("NameTaminKonandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameTaminKonandeh").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXTitr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameKala").Width = 180
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameKala").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameKala").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameBrand").Caption = "برند"
            GridEXTitr.CurrentTable.Columns.Item("NameBrand").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameBrand").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameBrand").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("NameBrand").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameBrand").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameG1").Caption = "گروه 1 کالا"
            GridEXTitr.CurrentTable.Columns.Item("NameG1").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameG1").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameG1").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("NameG1").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameG1").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameG2").Caption = "گروه 2 کالا"
            GridEXTitr.CurrentTable.Columns.Item("NameG2").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameG2").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameG2").Position = 8
            GridEXTitr.CurrentTable.Columns.Item("NameG2").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameG2").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameG3").Caption = "گروه 3 کالا"
            GridEXTitr.CurrentTable.Columns.Item("NameG3").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameG3").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameG3").Position = 9
            GridEXTitr.CurrentTable.Columns.Item("NameG3").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameG3").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameG4").Caption = "گروه 4 کالا"
            GridEXTitr.CurrentTable.Columns.Item("NameG4").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameG4").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameG4").Position = 10
            GridEXTitr.CurrentTable.Columns.Item("NameG4").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameG4").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameG5").Caption = "گروه 5 کالا"
            GridEXTitr.CurrentTable.Columns.Item("NameG5").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameG5").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameG5").Position = 11
            GridEXTitr.CurrentTable.Columns.Item("NameG5").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameG5").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoeMohasebeHadaf").Caption = "محاسبه هدف بر روی"
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMohasebeHadaf").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMohasebeHadaf").Width = 250
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMohasebeHadaf").Position = 12
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMohasebeHadaf").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMohasebeHadaf").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("RialHadaf").Caption = "ریـال هــدف"
            GridEXTitr.CurrentTable.Columns.Item("RialHadaf").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("RialHadaf").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("RialHadaf").Position = 13
            GridEXTitr.CurrentTable.Columns.Item("RialHadaf").FormatString = "###,###.##"
            GridEXTitr.CurrentTable.Columns.Item("RialHadaf").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("RialHadaf").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TedadHadaf").Caption = "تعـداد هــدف"
            GridEXTitr.CurrentTable.Columns.Item("TedadHadaf").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TedadHadaf").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TedadHadaf").Position = 14
            GridEXTitr.CurrentTable.Columns.Item("TedadHadaf").FormatString = "###,###.##"
            GridEXTitr.CurrentTable.Columns.Item("TedadHadaf").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("TedadHadaf").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccHadafKharid").Caption = "ccHadafKharid"
            GridEXTitr.CurrentTable.Columns.Item("ccHadafKharid").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccHadafKharid").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccHadafKharid").Position = 15
            GridEXTitr.CurrentTable.Columns.Item("ccHadafKharid").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("ccHadafKharid").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
                If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).FormatString = "G"
                    GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGrid ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGrid ")
        End Try
    End Sub
#End Region
#Region "From Buttons "
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Dim frm As New frmAN_AddNewHadaf

        frm.ModeForm = UD_Dll.Enums.GL_ModeForms.AddNewRecord

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        Search()
    End Sub
    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If MsgBox("آیا مایلید هدف انتخاب شده حذف گردد ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "") = MsgBoxResult.Yes Then
            objTools.DDelete("WareHouse.HadafKharid", "ccHadafKharid = " & Val(GridEXTitr.CurrentRow.Cells("ccHadafKharid").Text.Replace(",", "")))
            Search()
        End If
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
#End Region
#Region "Stored Procedures"
    '' WareHouse.spHadafKharid_Search
#End Region
End Class
