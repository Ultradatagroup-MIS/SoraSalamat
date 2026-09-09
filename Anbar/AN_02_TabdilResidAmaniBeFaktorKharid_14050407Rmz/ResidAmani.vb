Public Class ResidAmani

#Region "Variable AND Constant Declration"

    Const FormTableName = "tblAN_kdxResid"
    Dim ErrPro As New ErrorProvider
    Dim dvForm, dvForm_Fakotr As DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim FirstInsert As Boolean = False

    Public ccResid As Integer
    Public ShomarehResid As Integer

    Public CodeDoreh As Integer
    Public CodeMahal As Integer
    Public ccKardexTitr As Long

    Dim dt_RefreshResid As Data.DataTable = Nothing

    Dim Mode As Boolean = False

#End Region
    Private Sub ResidAmani_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)

        'LoadCombo()
        SearchResid()
        SetForm(CodeDoreh, CodeMahalFaal)
    End Sub
    Public Sub SetForm(ByVal CodeDorehFaal As Integer, ByVal CodeMahalFaal As Integer)
        If Mode = False Then

            ccResid = 0
            ShomarehResid = 0
            CodeDoreh = CodeDorehFaal
            CodeMahal = CodeMahalFaal

            SearchResid()

        ElseIf Mode = True Then
        End If

    End Sub
    Public Sub SearchResid()
        Try
            GridEXResid.DataSource = Nothing

            Dim da As SqlDataAdapter = New SqlDataAdapter
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[WareHouse].[spkdxResidAmani_SearchAmani]"
                    cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                    cm.Parameters.AddWithValue("CodeMahal", CodeMahal)
                    da.SelectCommand = cm
                    cm.CommandTimeout = 999999
                    dt_RefreshResid = New DataTable
                    da.Fill(dt_RefreshResid)
                    dvForm = New DataView
                    dvForm = dt_RefreshResid.DefaultView

                End Using
            End Using

            With GridEXResid
                .DataSource = Nothing
                .DataSource = dt_RefreshResid.DefaultView
                .SetDataBinding(dt_RefreshResid.DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXResid.CurrentTable.Columns.Count - 1
                GridEXResid.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXResid.CurrentTable.Columns.Item("ccKardexTitr").Caption = "ccKardexTitr"
            GridEXResid.CurrentTable.Columns.Item("ccKardexTitr").Visible = False
            GridEXResid.CurrentTable.Columns.Item("ccKardexTitr").Width = 10
            GridEXResid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXResid.CurrentTable.Columns.Item("ccKardexTitr").Position = 0

            GridEXResid.CurrentTable.Columns.Item("Radif").Caption = "ردیف"
            GridEXResid.CurrentTable.Columns.Item("Radif").Visible = True
            GridEXResid.CurrentTable.Columns.Item("Radif").Width = 80
            GridEXResid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXResid.CurrentTable.Columns.Item("Radif").Position = 1


            GridEXResid.CurrentTable.Columns.Item("ShomarehForm").Caption = "شماره رسید"
            GridEXResid.CurrentTable.Columns.Item("ShomarehForm").Visible = True
            GridEXResid.CurrentTable.Columns.Item("ShomarehForm").Width = 120
            GridEXResid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXResid.CurrentTable.Columns.Item("ShomarehForm").Position = 2


            GridEXResid.CurrentTable.Columns.Item("TarikhFormSlash").Caption = "تاریخ رسید"
            GridEXResid.CurrentTable.Columns.Item("TarikhFormSlash").Visible = True
            GridEXResid.CurrentTable.Columns.Item("TarikhFormSlash").Width = 120
            GridEXResid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXResid.CurrentTable.Columns.Item("TarikhFormSlash").Position = 3

            GridEXResid.CurrentTable.Columns.Item("NameAnbar").Caption = "نام انبار"
            GridEXResid.CurrentTable.Columns.Item("NameAnbar").Visible = True
            GridEXResid.CurrentTable.Columns.Item("NameAnbar").Width = 150
            GridEXResid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXResid.CurrentTable.Columns.Item("NameAnbar").Position = 4

            GridEXResid.CurrentTable.Columns.Item("NameTaminKonandeh").Caption = "نام تامین کننده"
            GridEXResid.CurrentTable.Columns.Item("NameTaminKonandeh").Visible = True
            GridEXResid.CurrentTable.Columns.Item("NameTaminKonandeh").Width = 150
            GridEXResid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXResid.CurrentTable.Columns.Item("NameTaminKonandeh").Position = 5

            GridEXResid.CurrentTable.Columns.Item("JamMablagh").Caption = "مبلغ"
            GridEXResid.CurrentTable.Columns.Item("JamMablagh").Visible = True
            GridEXResid.CurrentTable.Columns.Item("JamMablagh").Width = 120
            GridEXResid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXResid.CurrentTable.Columns.Item("JamMablagh").Position = 6

            GridEXResid.CurrentTable.Columns.Item("Tozihat").Caption = "توضیحات"
            GridEXResid.CurrentTable.Columns.Item("Tozihat").Visible = True
            GridEXResid.CurrentTable.Columns.Item("Tozihat").Width = 200
            GridEXResid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXResid.CurrentTable.Columns.Item("Tozihat").Position = 7

            GridEXResid.CurrentTable.Columns.Item("UserName").Caption = "نام کاربر"
            GridEXResid.CurrentTable.Columns.Item("UserName").Visible = True
            GridEXResid.CurrentTable.Columns.Item("UserName").Width = 100
            GridEXResid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXResid.CurrentTable.Columns.Item("UserName").Position = 8

            GridEXResid.CurrentTable.Columns.Item("Tarikh").Caption = "تاریخ"
            GridEXResid.CurrentTable.Columns.Item("Tarikh").Visible = True
            GridEXResid.CurrentTable.Columns.Item("Tarikh").Width = 100
            GridEXResid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXResid.CurrentTable.Columns.Item("Tarikh").Position = 9

            GridEXResid.CurrentTable.Columns.Item("Saat").Caption = "ساعت"
            GridEXResid.CurrentTable.Columns.Item("Saat").Visible = True
            GridEXResid.CurrentTable.Columns.Item("Saat").Width = 100
            GridEXResid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXResid.CurrentTable.Columns.Item("Saat").Position = 10

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub

    Public Sub GridEXResid_DoubleClick(sender As Object, e As EventArgs) Handles GridEXResid.DoubleClick

        Dim ShomarehResidAmani As Integer = objTools.ConvertNulls(objTools.DLookup("ShomarehForm", "tblAn_kdxResid", "ccKardextitr = " & GridEXResid.CurrentRow.Cells("ccKardexTitr").Value & ""), 0)


        If MsgBox("رسید امانی به شماره " & ShomarehResidAmani & " انتخاب شده است. آیا موافقید؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
            ShomarehResid = GridEXResid.CurrentRow.Cells("ShomarehForm").Value
            ccResid = GridEXResid.CurrentRow.Cells("ccKardexTitr").Value

            Me.Hide()
        Else
            Exit Sub
        End If


    End Sub



End Class