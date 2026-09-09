Public Class Eshantion

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
    Public ccEshantion As Integer
    Public ccEshantionSatr As Integer
    Public ShomarehEshantion As Integer
    Public TarikhEshantion As String
    Public Namemoshtary As String
    Public NameForoshandeh As String
    Public CodeMoshtary As Integer
    Public ccForoshandeh As Integer
    Public CodeForoshandeh As Integer
    Public JamMablagh As Double
    Public NameAnbar As String

    Public CodeDorehEshantion As Integer = CodeDorehEshantion

    Dim dt_RefreshEshantions As Data.DataTable = Nothing

    Dim Mode As Boolean = False  ' Mode  --> False : Save Titr \\ True : Inser Kala
    'Friend WithEvents GridEXEshantion As Janus.Windows.GridEX.GridEX
    Dim WithEvents TJ As TakhfifOJavaiez.TakhfifJayezeh

#End Region
    Private Sub Eshantion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()

        GridEXEshantion.Height = 300

        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)

        'LoadCombo()
        SearchEshantion(True)
        SetForm()
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
            CodeDoreh = "1402"
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
    Public Sub SetForm()
        If Mode = False Then

            ccMoshtary = 0
            CodeMoshtary = 0
            Namemoshtary = ""

            ccForoshandeh = 0
            CodeForoshandeh = 0
            NameForoshandeh = ""

            ccEshantion = 0
            ccEshantionSatr = 0
            ShomarehEshantion = 0
            TarikhEshantion = ""
            JamMablagh = 0
            NameAnbar = ""

            SearchEshantion(True)

        ElseIf Mode = True Then
        End If

        'ClearForm(False)
    End Sub
    Public Sub SearchEshantion(ByVal WithCriteria As Boolean)
        Try
            GridEXEshantion.DataSource = Nothing

            Dim da As SqlDataAdapter = New SqlDataAdapter
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[dbo].[spEshantion_SearchEshantion]"
                    cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
                    cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                    da.SelectCommand = cm
                    cm.CommandTimeout = 999999
                    dt_RefreshEshantions = New DataTable
                    da.Fill(dt_RefreshEshantions)
                    dvForm = New DataView
                    dvForm = dt_RefreshEshantions.DefaultView

                End Using
            End Using

            With GridEXEshantion
                .DataSource = Nothing
                .DataSource = dt_RefreshEshantions.DefaultView
                .SetDataBinding(dt_RefreshEshantions.DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXEshantion.CurrentTable.Columns.Count - 1
                GridEXEshantion.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXEshantion.CurrentTable.Columns.Item("ccEshantionTitr").Caption = "ccEshantionTitr"
            GridEXEshantion.CurrentTable.Columns.Item("ccEshantionTitr").Visible = False
            GridEXEshantion.CurrentTable.Columns.Item("ccEshantionTitr").Width = 100
            GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXEshantion.CurrentTable.Columns.Item("ccEshantionTitr").Position = 0

            'GridEXEshantion.CurrentTable.Columns.Item("ccEshantionSatr").Caption = "ccEshantionSatr"
            'GridEXEshantion.CurrentTable.Columns.Item("ccEshantionSatr").Visible = False
            'GridEXEshantion.CurrentTable.Columns.Item("ccEshantionSatr").Width = 100
            'GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXEshantion.CurrentTable.Columns.Item("ccEshantionSatr").Position = 1

            GridEXEshantion.CurrentTable.Columns.Item("ShomarehEshantion").Caption = "شماره اشانتیون"
            GridEXEshantion.CurrentTable.Columns.Item("ShomarehEshantion").Visible = True
            GridEXEshantion.CurrentTable.Columns.Item("ShomarehEshantion").Width = 100
            GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXEshantion.CurrentTable.Columns.Item("ShomarehEshantion").Position = 1

            GridEXEshantion.CurrentTable.Columns.Item("TarikhForm").Caption = "تاریخ اشانتیون"
            GridEXEshantion.CurrentTable.Columns.Item("TarikhForm").Visible = True
            GridEXEshantion.CurrentTable.Columns.Item("TarikhForm").Width = 100
            GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXEshantion.CurrentTable.Columns.Item("TarikhForm").Position = 2

            'GridEXEshantion.CurrentTable.Columns.Item("FaktorShomareh").Caption = "شماره فاکتور"
            'GridEXEshantion.CurrentTable.Columns.Item("FaktorShomareh").Visible = True
            'GridEXEshantion.CurrentTable.Columns.Item("FaktorShomareh").Width = 100
            'GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXEshantion.CurrentTable.Columns.Item("FaktorShomareh").Position = 3

            GridEXEshantion.CurrentTable.Columns.Item("JamMablagh").Caption = "مبلغ اشانتیون"
            GridEXEshantion.CurrentTable.Columns.Item("JamMablagh").Visible = True
            GridEXEshantion.CurrentTable.Columns.Item("JamMablagh").Width = 80
            GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXEshantion.CurrentTable.Columns.Item("JamMablagh").Position = 3

            '----------------------

            GridEXEshantion.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
            GridEXEshantion.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXEshantion.CurrentTable.Columns.Item("NameMoshtary").Width = 100
            GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXEshantion.CurrentTable.Columns.Item("NameMoshtary").Position = 4
            '----------------------

            GridEXEshantion.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کد مشتری"
            GridEXEshantion.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXEshantion.CurrentTable.Columns.Item("CodeMoshtary").Width = 100
            GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXEshantion.CurrentTable.Columns.Item("CodeMoshtary").Position = 5

            '----------------------

            GridEXEshantion.CurrentTable.Columns.Item("ccMoshtary").Caption = "ccMoshtary"
            GridEXEshantion.CurrentTable.Columns.Item("ccMoshtary").Visible = False
            GridEXEshantion.CurrentTable.Columns.Item("ccMoshtary").Width = 100
            GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXEshantion.CurrentTable.Columns.Item("ccMoshtary").Position = 6

            '----------------------

            GridEXEshantion.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نام فروشنده"
            GridEXEshantion.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXEshantion.CurrentTable.Columns.Item("NameForoshandeh").Width = 100
            GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXEshantion.CurrentTable.Columns.Item("NameForoshandeh").Position = 7

            '----------------------

            GridEXEshantion.CurrentTable.Columns.Item("CodeForoshandeh").Caption = "کد فروشنده"
            GridEXEshantion.CurrentTable.Columns.Item("CodeForoshandeh").Visible = True
            GridEXEshantion.CurrentTable.Columns.Item("CodeForoshandeh").Width = 100
            GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXEshantion.CurrentTable.Columns.Item("CodeForoshandeh").Position = 8

            '----------------------

            GridEXEshantion.CurrentTable.Columns.Item("ccForoshandeh").Caption = "ccForoshandeh"
            GridEXEshantion.CurrentTable.Columns.Item("ccForoshandeh").Visible = False
            GridEXEshantion.CurrentTable.Columns.Item("ccForoshandeh").Width = 100
            GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXEshantion.CurrentTable.Columns.Item("ccForoshandeh").Position = 9

            '----------------------

            GridEXEshantion.CurrentTable.Columns.Item("NameAnbar").Caption = "نام انبار"
            GridEXEshantion.CurrentTable.Columns.Item("NameAnbar").Visible = True
            GridEXEshantion.CurrentTable.Columns.Item("NameAnbar").Width = 100
            GridEXEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXEshantion.CurrentTable.Columns.Item("NameAnbar").Position = 10

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub

    Public Sub GridEXEshantion_DoubleClick(sender As Object, e As EventArgs) Handles GridEXEshantion.DoubleClick
        ccEshantion = GridEXEshantion.CurrentRow.Cells("ccEshantionTitr").Value
        'ccEshantionSatr = GridEXEshantion.CurrentRow.Cells("ccEshantionSatr").Value
        ShomarehEshantion = GridEXEshantion.CurrentRow.Cells("ShomarehEshantion").Value
        TarikhEshantion = GridEXEshantion.CurrentRow.Cells("TarikhForm").Value
        JamMablagh = GridEXEshantion.CurrentRow.Cells("JamMablagh").Value
        Namemoshtary = GridEXEshantion.CurrentRow.Cells("Namemoshtary").Value
        CodeMoshtary = GridEXEshantion.CurrentRow.Cells("CodeMoshtary").Value
        ccMoshtary = GridEXEshantion.CurrentRow.Cells("ccMoshtary").Value
        NameForoshandeh = GridEXEshantion.CurrentRow.Cells("NameForoshandeh").Value
        ccForoshandeh = GridEXEshantion.CurrentRow.Cells("ccForoshandeh").Value
        CodeForoshandeh = GridEXEshantion.CurrentRow.Cells("CodeForoshandeh").Value
        NameAnbar = GridEXEshantion.CurrentRow.Cells("NameAnbar").Value

        Me.Hide()

    End Sub



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
