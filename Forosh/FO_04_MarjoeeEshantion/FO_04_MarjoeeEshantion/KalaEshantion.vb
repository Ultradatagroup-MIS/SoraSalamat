Public Class KalaEshantion

#Region "Variable AND Constant Declration"

    'Dim AllowPishFaktorTakhfifDasty As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowPishFaktorTakhfifDasty", "tblGL_SysConfig", ""), False)

    'Dim cmTitr As CurrencyManager
    'Dim cmSatr As CurrencyManager
    'Dim cmForm As CurrencyManager
    'Dim dvTitr, dvTitr_Faktor As DataView
    'Dim tCodeCounter As Long

    Const FormTableName = "tblAN_EshantionSatr"
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

    Public ccKala As Integer
    Public CodeKala As String
    Public NameKala As String

    Public Tedad As Integer
    Public Fee As Double

    Public ShomarehBach As String

    Public TarikhTolid As String
    Public TarikhEngheza As String
    Public Mandeh As Integer

    Dim dt_RefreshKalaInEshantion As Data.DataTable = Nothing

    Dim Mode As Boolean = False  ' Mode  --> False : Save Titr \\ True : Inser Kala
    'Friend WithEvents GridEXKalaEshantion As Janus.Windows.GridEX.GridEX
    Dim WithEvents TJ As TakhfifOJavaiez.TakhfifJayezeh
#End Region
    Private Sub KalaEshantion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetParameter()

        'GridEXKalaEshantion.Height = 300

        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)

        'LoadCombo()
        SearchKalaInEshantion(True)
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
            txtCaption = "کالاهای اشانتیون ها"
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

            ccKala = 0
            CodeKala = ""
            NameKala = ""

            Tedad = 0
            Fee = 0

            ShomarehBach = ""
            TarikhTolid = ""
            'TarikhEmrooz = ""

            ccEshantionSatr = 0

            SearchKalaInEshantion(True)

        ElseIf Mode = True Then
        End If

        'ClearForm(False)
    End Sub

    Private Sub GridEXKalaEshantion_DoubleClick(sender As Object, e As EventArgs) Handles GridEXKalaEshantion.DoubleClick

        ccEshantionSatr = GridEXKalaEshantion.CurrentRow.Cells("ccEshantionSatr").Value
        ccKala = GridEXKalaEshantion.CurrentRow.Cells("ccKala").Value
        CodeKala = GridEXKalaEshantion.CurrentRow.Cells("CodeKala").Value
        NameKala = GridEXKalaEshantion.CurrentRow.Cells("NameKala").Value
        Tedad = GridEXKalaEshantion.CurrentRow.Cells("Tedad").Value
        Fee = GridEXKalaEshantion.CurrentRow.Cells("Fee").Value

        ShomarehBach = GridEXKalaEshantion.CurrentRow.Cells("ShomarehBach").Value
        TarikhTolid = GridEXKalaEshantion.CurrentRow.Cells("TarikhTolid").Value
        TarikhEngheza = GridEXKalaEshantion.CurrentRow.Cells("TarikhEngheza").Value
        Mandeh = GridEXKalaEshantion.CurrentRow.Cells("Mandeh").Value

        Me.Hide()
    End Sub

    Public Sub SearchKalaInEshantion(ByVal WithCriteria As Boolean)
        Try
            GridEXKalaEshantion.DataSource = Nothing

            Dim da As SqlDataAdapter = New SqlDataAdapter
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[dbo].[spEshantion_SearchKalaInEshantion]"
                    cm.Parameters.AddWithValue("ccEshantionTitr", frmAN_MarjoeeEshantion.GridEXTitr.CurrentRow.Cells("ccEshantionTitr").Value)
                    da.SelectCommand = cm
                    cm.CommandTimeout = 999999
                    dt_RefreshKalaInEshantion = New DataTable
                    da.Fill(dt_RefreshKalaInEshantion)
                    dvForm = New DataView
                    dvForm = dt_RefreshKalaInEshantion.DefaultView

                End Using
            End Using

            With GridEXKalaEshantion
                .DataSource = Nothing
                .DataSource = dt_RefreshKalaInEshantion.DefaultView
                .SetDataBinding(dt_RefreshKalaInEshantion.DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXKalaEshantion.CurrentTable.Columns.Count - 1
                GridEXKalaEshantion.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXKalaEshantion.CurrentTable.Columns.Item("ccEshantionSatr").Caption = "ccEshantionSatr"
            GridEXKalaEshantion.CurrentTable.Columns.Item("ccEshantionSatr").Visible = False
            GridEXKalaEshantion.CurrentTable.Columns.Item("ccEshantionSatr").Width = 100
            GridEXKalaEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKalaEshantion.CurrentTable.Columns.Item("ccEshantionSatr").Position = 0

            GridEXKalaEshantion.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXKalaEshantion.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXKalaEshantion.CurrentTable.Columns.Item("ccKala").Width = 100
            GridEXKalaEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKalaEshantion.CurrentTable.Columns.Item("ccKala").Position = 1

            GridEXKalaEshantion.CurrentTable.Columns.Item("CodeKala").Caption = "کد کالا"
            GridEXKalaEshantion.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXKalaEshantion.CurrentTable.Columns.Item("CodeKala").Width = 100
            GridEXKalaEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKalaEshantion.CurrentTable.Columns.Item("CodeKala").Position = 2

            GridEXKalaEshantion.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXKalaEshantion.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXKalaEshantion.CurrentTable.Columns.Item("NameKala").Width = 100
            GridEXKalaEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKalaEshantion.CurrentTable.Columns.Item("NameKala").Position = 3

            GridEXKalaEshantion.CurrentTable.Columns.Item("Fee").Caption = "قیمت"
            GridEXKalaEshantion.CurrentTable.Columns.Item("Fee").Visible = True
            GridEXKalaEshantion.CurrentTable.Columns.Item("Fee").Width = 80
            GridEXKalaEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKalaEshantion.CurrentTable.Columns.Item("Fee").Position = 4

            '----------------------

            GridEXKalaEshantion.CurrentTable.Columns.Item("Tedad").Caption = "تعداد"
            GridEXKalaEshantion.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXKalaEshantion.CurrentTable.Columns.Item("Tedad").Width = 100
            GridEXKalaEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKalaEshantion.CurrentTable.Columns.Item("Tedad").Position = 5
            '----------------------

            GridEXKalaEshantion.CurrentTable.Columns.Item("ShomarehBach").Caption = "شماره بچ"
            GridEXKalaEshantion.CurrentTable.Columns.Item("ShomarehBach").Visible = True
            GridEXKalaEshantion.CurrentTable.Columns.Item("ShomarehBach").Width = 100
            GridEXKalaEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKalaEshantion.CurrentTable.Columns.Item("ShomarehBach").Position = 6

            '----------------------

            GridEXKalaEshantion.CurrentTable.Columns.Item("TarikhTolid").Caption = "تاریخ تولید"
            GridEXKalaEshantion.CurrentTable.Columns.Item("TarikhTolid").Visible = False
            GridEXKalaEshantion.CurrentTable.Columns.Item("TarikhTolid").Width = 100
            GridEXKalaEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKalaEshantion.CurrentTable.Columns.Item("TarikhTolid").Position = 7

            '----------------------

            GridEXKalaEshantion.CurrentTable.Columns.Item("TarikhEngheza").Caption = "تاریخ انقضا"
            GridEXKalaEshantion.CurrentTable.Columns.Item("TarikhEngheza").Visible = True
            GridEXKalaEshantion.CurrentTable.Columns.Item("TarikhEngheza").Width = 100
            GridEXKalaEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKalaEshantion.CurrentTable.Columns.Item("TarikhEngheza").Position = 8

            '----------------------

            GridEXKalaEshantion.CurrentTable.Columns.Item("Mandeh").Caption = "مانده اشانتیون"
            GridEXKalaEshantion.CurrentTable.Columns.Item("Mandeh").Visible = True
            GridEXKalaEshantion.CurrentTable.Columns.Item("Mandeh").Width = 100
            GridEXKalaEshantion.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKalaEshantion.CurrentTable.Columns.Item("Mandeh").Position = 9

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub
End Class