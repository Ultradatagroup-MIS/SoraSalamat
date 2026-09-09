Public Class frmFO_VorodEshantion

#Region "Variable AND Constant Declration"

    'Dim AllowPishFaktorTakhfifDasty As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowPishFaktorTakhfifDasty", "tblGL_SysConfig", ""), False)

    'Dim cmTitr As CurrencyManager
    'Dim cmSatr As CurrencyManager
    'Dim cmForm As CurrencyManager
    'Dim dvTitr, dvTitr_Faktor As DataView
    'Dim tCodeCounter As Long
    Const FormTableName = "tblAN_Eshantion"
    Dim ErrPro As New ErrorProvider
    'Dim dsForm As New DataSet
    Dim dvForm, dvForm_Fakotr As DataView
    'Dim IsSabadKala As Boolean = False
    'Private SN As Integer
    Dim flg_SearchEshantion As Boolean = False
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim FirstInsert As Boolean = False

    Public ccMoshtary As Integer
    'Dim sNoeMoshtary As Integer = 0
    'Public CodeFardMamorPakhsh As Integer
    'Dim IsMalyatAvarezTakhfif As Boolean
    'Dim tpos As Integer
    Dim ccEshantion As Integer
    Dim ShomarehEshantion As Integer
    Dim TarikhEshantion As String
    Dim Namemoshtary As String
    Dim NameForoshaneh As String
    Dim CodeMoshtary As Integer
    Dim ccForoshandeh As Integer
    Dim CodeForoshandeh As Integer
    'Dim ccMarjoee_Vaset As Integer = 0

    'Dim Mode As Boolean = False  ' Mode  --> False : Save Titr \\ True : Inser Kala
    Friend WithEvents GridEXTitr As Janus.Windows.GridEX.GridEX
    Dim WithEvents TJ As TakhfifOJavaiez.TakhfifJayezeh

#End Region
    Private Sub frmFO_VorodEshantion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SetParameter()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)

        'LoadCombo()
        SearchEshantion()
        'SetForm()
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
            CodeDoreh = "1401"
            txtCaption = "لیست اشانتیون ها"
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
            'Me.Text = txtCaption
            ObjCode.UserName = UserName
        End If
    End Sub
    Private Sub SearchEshantion()
        Try

            Dim da As SqlDataAdapter = New SqlDataAdapter
            Dim dt_RefreshSerial As Data.DataTable = Nothing
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[dbo].[spEshantion_SearchEshantion]"
                    da.SelectCommand = cm
                    cm.CommandTimeout = 999999
                    dt_RefreshSerial = New DataTable
                    da.Fill(dt_RefreshSerial)
                    dvForm = New DataView
                    dvForm = dt_RefreshSerial.DefaultView

                End Using
            End Using

            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dt_RefreshSerial.DefaultView
                .SetDataBinding(dt_RefreshSerial.DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("Radif").Caption = "ردیف"
            GridEXTitr.CurrentTable.Columns.Item("Radif").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Radif").Width = 50
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Radif").Position = 1

            GridEXTitr.CurrentTable.Columns.Item("ShomarehEshantion").Caption = "شماره اشانتیون"
            GridEXTitr.CurrentTable.Columns.Item("ShomarehEshantion").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("ShomarehEshantion").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ShomarehEshantion").Position = 2

            GridEXTitr.CurrentTable.Columns.Item("TarikhForm").Caption = "تاریخ اشانتیون"
            GridEXTitr.CurrentTable.Columns.Item("TarikhForm").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TarikhForm").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TarikhForm").Position = 3

            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نام فروشنده"

            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Position = 4
            '----------------------

            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کد مشتری"
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Position = 5
            '----------------------

            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Position = 6
            '----------------------

            GridEXTitr.CurrentTable.Columns.Item("JamMablagh").Caption = "مبلغ مرجوعی اشانتیون"
            GridEXTitr.CurrentTable.Columns.Item("JamMablagh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("JamMablagh").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("JamMablagh").Position = 7

            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Caption = "نام انبار"
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Position = 8

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub

    Private Sub GridEXTitr_DoubleClick(sender As Object, e As EventArgs) Handles GridEXTitr.DoubleClick
        ccEshantion = GridEXTitr.CurrentRow.Cells("ccEshantionTtir").Value
        ShomarehEshantion = GridEXTitr.CurrentRow.Cells("ShomarehEshantion").Value
        TarikhEshantion = GridEXTitr.CurrentRow.Cells("TarikhEshantion").Value
        Namemoshtary = GridEXTitr.CurrentRow.Cells("Namemoshtary").Value
        CodeMoshtary = GridEXTitr.CurrentRow.Cells("CodeMoshtary").Value
        ccMoshtary = GridEXTitr.CurrentRow.Cells("ccMoshtary").Value
        NameForoshaneh = GridEXTitr.CurrentRow.Cells("NameForoshaneh").Value
        ccForoshandeh = GridEXTitr.CurrentRow.Cells("ccForoshandeh").Value
        CodeForoshandeh = GridEXTitr.CurrentRow.Cells("CodeForoshandeh").Value

    End Sub
    '         
End Class

'' Sales.spElamMarjoee_InsertOneKala_SearchFaktor
'' Sales.spElamMarjoee_InsertOneKala_Search
'' Sales.spElamMarjoee_InsertOneKala_InsertMarjoeeVasetTitr
'' Sales.spElamMarjoee_InsertOneKala_InsertMarjoeeVasetSatr
'' Sales.spElamMarjoee_InsertOneKala_SearchFaktor
'' Sales.spElamMarjoee_InsertOneKala_DeleteMarjoeeVasetTitrSatr
'' Sales.spElamMarjoee_InsertOneKala_Taeed_SearchFaktor
'' Sales.spElamMarjoee_InsertOneKala_Taeed_CreateTitrMarjoee
'' Sales.spElamMarjoee_InsertOneKala_Taeed_SearchKalaInFaktor
'' Sales.spElamMarjoee_InsertOneKala_Taeed_InsertKalaInSatrMarjoee
'' Sales.spElamMarjoee_InsertOneKala_Taeed_UpdateVazeiatSanad