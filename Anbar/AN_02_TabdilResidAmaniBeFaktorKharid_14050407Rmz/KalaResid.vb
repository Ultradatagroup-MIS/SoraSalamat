Public Class KalaResid

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
    Dim flg_SearchEshantion As Boolean = False
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim FirstInsert As Boolean = False

    Public ccKardexTitr As Long

    Public ccKala As Integer
    Public CodeKala As String
    Public NameKala As String

    Public Tedad As Integer
    Public TedadDarBasteh As Integer
    Public TedadDarKarton As Integer

    Public Fee As Double

    Public ShomarehBach As String

    Public TarikhTolid As String
    Public TarikhEngheza As String
    Public Mandeh As Integer
    Public MablaghKharid As Double
    Public ccResid As Long
    Public ccResidSatr As Long
    Public IsJayezeh As Boolean

    Dim dt_RefreshKalaInResid As Data.DataTable = Nothing

    Dim Mode As Boolean = False  ' Mode  --> False : Save Titr \\ True : Inser Kala
    'Friend WithEvents GridEXKalaEshantion As Janus.Windows.GridEX.GridEX
    Dim WithEvents TJ As TakhfifOJavaiez.TakhfifJayezeh
#End Region
    Private Sub KalaResid_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)

        SearchKalaInResid(True)
        SetForm()

    End Sub




    Public Sub SetForm()
        If Mode = False Then

            ccKala = 0
            CodeKala = ""
            NameKala = ""
            Tedad = 0
            Fee = 0
            ShomarehBach = ""
            TarikhTolid = ""

            SearchKalaInResid(True)

        ElseIf Mode = True Then
        End If

        'ClearForm(False)
    End Sub

    Private Sub GridEXKala_DoubleClick(sender As Object, e As EventArgs) Handles GridEXKala.DoubleClick

        ccKala = GridEXKala.CurrentRow.Cells("ccKala").Value
        CodeKala = GridEXKala.CurrentRow.Cells("CodeKala").Value
        NameKala = GridEXKala.CurrentRow.Cells("NameKala").Value
        Tedad = GridEXKala.CurrentRow.Cells("Tedad3").Value

        ShomarehBach = GridEXKala.CurrentRow.Cells("ShomarehBach").Value
        TarikhTolid = GridEXKala.CurrentRow.Cells("TarikhTolidSlash").Value
        TarikhEngheza = GridEXKala.CurrentRow.Cells("TarikhEnghezaSlash").Value
        Mandeh = GridEXKala.CurrentRow.Cells("Mandeh").Value

        MablaghKharid = GridEXKala.CurrentRow.Cells("MablaghKharid").Value
        ccResid = GridEXKala.CurrentRow.Cells("ccKardexTitr").Value
        ccResidSatr = GridEXKala.CurrentRow.Cells("ccKardexSatr").Value
        IsJayezeh = GridEXKala.CurrentRow.Cells("IsJayezeh").Value

        Me.Hide()
    End Sub

    Public Sub SearchKalaInResid(ByVal WithCriteria As Boolean)
        Try
            GridEXKala.DataSource = Nothing

            Dim ckardex As Integer = frmAN_TabdilResidAmaniBeFaktorKharid.GridEXTitr.CurrentRow.Cells("ccResid").Value

            Dim da As SqlDataAdapter = New SqlDataAdapter
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[WareHouse].[spkdxResidAmani_SearchKala]"
                    cm.Parameters.AddWithValue("ccKardexTitr", ckardex)
                    da.SelectCommand = cm
                    cm.CommandTimeout = 999999
                    dt_RefreshKalaInResid = New DataTable
                    da.Fill(dt_RefreshKalaInResid)
                    dvForm = New DataView
                    dvForm = dt_RefreshKalaInResid.DefaultView

                End Using
            End Using

            With GridEXKala
                .DataSource = Nothing
                .DataSource = dt_RefreshKalaInResid.DefaultView
                .SetDataBinding(dt_RefreshKalaInResid.DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXKala.CurrentTable.Columns.Count - 1
                GridEXKala.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXKala.CurrentTable.Columns.Item("ccKardexSatr").Caption = "ccKardexSatr"
            GridEXKala.CurrentTable.Columns.Item("ccKardexSatr").Visible = False
            GridEXKala.CurrentTable.Columns.Item("ccKardexSatr").Width = 10
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("ccKardexSatr").Position = 0

            GridEXKala.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXKala.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXKala.CurrentTable.Columns.Item("ccKala").Width = 10
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("ccKala").Position = 1

            GridEXKala.CurrentTable.Columns.Item("CodeKala").Caption = "کد کالا"
            GridEXKala.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXKala.CurrentTable.Columns.Item("CodeKala").Width = 80
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("CodeKala").Position = 2

            GridEXKala.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXKala.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXKala.CurrentTable.Columns.Item("NameKala").Width = 100
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("NameKala").Position = 3

            GridEXKala.CurrentTable.Columns.Item("Tedad3").Caption = "تعداد"
            GridEXKala.CurrentTable.Columns.Item("Tedad3").Visible = True
            GridEXKala.CurrentTable.Columns.Item("Tedad3").Width = 100
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("Tedad3").Position = 4

            GridEXKala.CurrentTable.Columns.Item("Mandeh").Caption = "مانده رسید"
            GridEXKala.CurrentTable.Columns.Item("Mandeh").Visible = True
            GridEXKala.CurrentTable.Columns.Item("Mandeh").Width = 100
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("Mandeh").Position = 5

            GridEXKala.CurrentTable.Columns.Item("ShomarehBach").Caption = "شماره بچ"
            GridEXKala.CurrentTable.Columns.Item("ShomarehBach").Visible = True
            GridEXKala.CurrentTable.Columns.Item("ShomarehBach").Width = 100
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("ShomarehBach").Position = 6

            GridEXKala.CurrentTable.Columns.Item("TarikhTolidSlash").Caption = "تاریخ تولید"
            GridEXKala.CurrentTable.Columns.Item("TarikhTolidSlash").Visible = True
            GridEXKala.CurrentTable.Columns.Item("TarikhTolidSlash").Width = 100
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("TarikhTolidSlash").Position = 7

            GridEXKala.CurrentTable.Columns.Item("TarikhEnghezaSlash").Caption = "تاریخ انقضا"
            GridEXKala.CurrentTable.Columns.Item("TarikhEnghezaSlash").Visible = True
            GridEXKala.CurrentTable.Columns.Item("TarikhEnghezaSlash").Width = 100
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("TarikhEnghezaSlash").Position = 8

            GridEXKala.CurrentTable.Columns.Item("MablaghKharid").Caption = "مبلغ خرید"
            GridEXKala.CurrentTable.Columns.Item("MablaghKharid").Visible = True
            GridEXKala.CurrentTable.Columns.Item("MablaghKharid").Width = 100
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("MablaghKharid").Position = 9

            GridEXKala.CurrentTable.Columns.Item("MKOL").Caption = "مبلغ"
            GridEXKala.CurrentTable.Columns.Item("MKOL").Visible = True
            GridEXKala.CurrentTable.Columns.Item("MKOL").Width = 100
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("MKOL").Position = 10

            GridEXKala.CurrentTable.Columns.Item("txtJayezeh").Caption = "نوع کالا"
            GridEXKala.CurrentTable.Columns.Item("txtJayezeh").Visible = True
            GridEXKala.CurrentTable.Columns.Item("txtJayezeh").Width = 100
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("txtJayezeh").Position = 11

            GridEXKala.CurrentTable.Columns.Item("IsJayezeh").Caption = "IsJayezeh"
            GridEXKala.CurrentTable.Columns.Item("IsJayezeh").Visible = False
            GridEXKala.CurrentTable.Columns.Item("IsJayezeh").Width = 10
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("IsJayezeh").Position = 12

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub
End Class