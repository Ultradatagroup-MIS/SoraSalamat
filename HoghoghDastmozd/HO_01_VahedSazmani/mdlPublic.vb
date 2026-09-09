Module mdlPublic
    '---Global----------------------------------------------------

    Public objTools As New ud_dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New UD_Dll.Security
    Public objSearch As New UD_Dll.Search
    Public ObjCode As New UD_Dll.Code

    Public ConnectionString As String = objTools.GetConnectionString

    ' Input Parameter 
    Public UserName As String '1
    Public UserCode As Long  '2   CodeFard
    Public UserPassWord As String   '3
    Public NameMahalFaal As String = "" '4 
    Public CodeMahalFaal As Long = 0 '5    
    Public PersonelCode As Long  '6       ShomarehPersonely
    Public PersonelName As String  '7
    Public CodeDoreh As Long = 0 '8    
    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)

    Public ccHadafSal As Integer
    Public RialHadafSal As Double
    Public HadafDoreh As Double
    Public HadafVersion As Integer
    ''' <summary>
    ''' '''''''''''''''''''
    ''' </summary>
    ''' <remarks></remarks>
    Public ccHadafMah As Integer
    Public RialHadafMah As Long
    Public txtMah As String
    ''''''''''''''''''''''
    Public ccHadafMarkazPakhsh As Integer
    Public ccMarkazPakhsh As Integer
    Public RialHadaf_MarkazPkahsh As Long
    ''''''''''''''''''''''
    Public txtMarkazPakhsh As String
    Public ccHadafMarkazPakhsh_Foroshandeh As Long
    Public ccHadafForoshandeh_Kala As Long
    Public RialHadaf_Foroshandeh As Long
    Public ccHadafForoshandeh_Brand As Long
    Public RialHadaf_Foroshandeh_Brand As Long
    Public NameForoshandeh As String
    Public NameBrand As String

End Module
