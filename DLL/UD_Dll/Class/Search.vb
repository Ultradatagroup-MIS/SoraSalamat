Imports System.Windows.Forms

Public Class Search

    Public StrLoadTree As String
    Public Enum enmNoeSearch
        AnyPartOfField = 222
        StartOfField = 223
        EndOfField = 224
        WholeField = 225
    End Enum
    Public Enum enmVaziatSave
        NotSave = 0
        Save = 1
    End Enum

    ' Text Controls

    Public Function SetSqlCritriaText(ByVal FieldNameText As String, _
                                            ByVal FieldValue As String, _
                                            ByVal SearchPosition As enmNoeSearch, _
                                            Optional ByVal StoreCase As enmVaziatSave = enmVaziatSave.NotSave) As String

        Dim StrSql, mFieldValue() As String
        Dim i As Integer
        StrSql = ""
        mFieldValue = FieldValue.Split("+")

        Select Case SearchPosition
            Case enmNoeSearch.AnyPartOfField
                For i = 0 To mFieldValue.Length - 1
                    If mFieldValue(i).Length <> 0 Then
                        StrSql &= FieldNameText & " LIKE "
                        StrSql &= IIf(StoreCase, "''%" & mFieldValue(i) & "%''", "'%" & mFieldValue(i) & "%'")
                        StrSql &= " And "
                    End If
                Next
            Case enmNoeSearch.EndOfField
                For i = 0 To mFieldValue.Length - 1
                    If mFieldValue(i).Length <> 0 Then
                        StrSql &= FieldNameText & " LIKE "
                        StrSql &= IIf(StoreCase, "''%" & mFieldValue(i) & "''", "'%" & mFieldValue(i) & "'")
                        StrSql &= "  Or "
                    End If
                Next
            Case enmNoeSearch.StartOfField
                For i = 0 To mFieldValue.Length - 1
                    If mFieldValue(i).Length <> 0 Then
                        StrSql &= FieldNameText & " LIKE "
                        StrSql &= IIf(StoreCase, "''" & mFieldValue(i) & "%''", "'" & mFieldValue(i) & "%'")
                        StrSql &= "  Or "
                    End If
                Next
            Case enmNoeSearch.WholeField
                For i = 0 To mFieldValue.Length - 1
                    If mFieldValue(i).Length <> 0 Then
                        StrSql &= FieldNameText & " LIKE "
                        StrSql &= IIf(StoreCase, "''" & mFieldValue(i) & "''", "'" & mFieldValue(i) & "'")
                        StrSql &= "  Or "
                    End If
                Next
        End Select
        If StrSql.Length > 0 Then StrSql = StrSql.Substring(0, StrSql.Length - 4)
        Return StrSql
    End Function
    ' Number Controls
    Public Function SetSqlCritriaNumber(ByVal FieldNameNumber As String, _
                                            ByVal AzNumber As String, _
                                            ByVal TaNumber As String, _
                                            ByVal YaNumber As String) As String

        Dim StrSql, mYaNumber(), StrTmp As String
        Dim index As Integer
        StrSql = "" : StrTmp = ""
        mYaNumber = YaNumber.Split("+")

        If AzNumber.Length <> 0 Then
            StrSql &= FieldNameNumber & " >= " & AzNumber & " And "
        End If
        If TaNumber.Length <> 0 Then
            StrSql &= FieldNameNumber & " <= " & TaNumber & " And "
        End If
        If StrSql.Length > 0 Then StrSql = StrSql.Substring(0, StrSql.Length - 4)

        For index = 0 To mYaNumber.Length - 1
            If mYaNumber(index).Length <> 0 Then StrTmp &= mYaNumber(index) & ","
        Next
        If StrTmp.Length > 0 Then StrTmp = StrTmp.Substring(0, StrTmp.Length - 1)

        If StrTmp.Length > 0 Then
            If StrSql.Length > 0 Then
                StrSql &= " OR " & FieldNameNumber & " IN(" & StrTmp & ")"
            Else
                StrSql &= FieldNameNumber & " IN(" & StrTmp & ")"
            End If
        End If

        Return StrSql
    End Function
    ' Tarikh Controls
    Public Function SetSqlCritriaTarikh(ByVal FieldNameTarikh As String, _
                                            ByVal AzTarikh As Object, _
                                            ByVal TaTarikh As Object, _
                                            Optional ByVal StoreCase As enmVaziatSave = enmVaziatSave.NotSave) As String
        Dim StrSql As String = ""
        If AzTarikh.ToString <> "" Then
            If Len(AzTarikh.Text) <> 0 Then
                If Len(AzTarikh.Text) = 8 Then
                    StrSql &= FieldNameTarikh & ">='" & IIf(StoreCase, "'" & AzTarikh.Text & "'", AzTarikh.Text) & "' And "
                Else
                    AzTarikh.SelLength = AzTarikh.MaxLength
                    StrSql &= FieldNameTarikh & " LIKE '" & IIf(StoreCase, "'%" & AzTarikh.SelText.Replace("/", "").Replace(" ", "_") & "%'", "%" & AzTarikh.SelText.Replace("/", "").Replace(" ", "_") & "%") & "' And "
                End If
            End If
        End If

        If TaTarikh.ToString <> "" Then
            If Len(TaTarikh.Text) <> 0 Then
                If Len(TaTarikh.Text) = 8 Then
                    StrSql &= FieldNameTarikh & "<='" & IIf(StoreCase, "'" & TaTarikh.Text & "'", TaTarikh.Text) & "' And "
                Else
                    If AzTarikh.Text <> TaTarikh.Text Then
                        TaTarikh.SelLength = TaTarikh.MaxLength
                        StrSql &= FieldNameTarikh & " LIKE '" & IIf(StoreCase, "'%" & TaTarikh.SelText.Replace("/", "").Replace(" ", "_") & "%'", "%" & TaTarikh.SelText.Replace("/", "").Replace(" ", "_") & "%") & "' And "
                    End If
                End If
            End If
        End If
        If StrSql.Length > 0 Then StrSql = StrSql.Substring(0, StrSql.Length - 4)

        Return StrSql
    End Function
    '  ChkBox Controls
    Public Function SetSqlCritriaCombo(ByVal FieldNameCombo As String, _
                                            ByVal FieldValue As Integer) As String
        Return FieldNameCombo & " =" & FieldValue
    End Function
    '  ChkBox Controls

    Public Function SetSqlCritriaChkBox(ByVal FieldNameChkBox As String, _
                                            ByVal FieldValue As Boolean) As String
        Return FieldNameChkBox & IIf(FieldValue, "=1", "=0")
    End Function
    ' Tree Controls
    Public Function SetSqlCritriaTree(ByVal ObjTreeName As TreeView, _
                                            ByVal StrFieldsName As String) As String

        Return SetShartTree(ObjTreeName, StrFieldsName)
    End Function
    '-------------------------------
    ' rotins baraye sabt tree 
    '
    '-------------------------------
    Private Function SetShartTree(ByVal TView As TreeView, ByVal StrFieldsName As String) As String
        SetShartTree = ""
        Dim Index As Integer = 0
        Dim FieldsName() As String
        'ShartFields = "Code1;Code2;Code3;Code4"
        StrLoadTree = Nothing

        If StrFieldsName.Length = 0 Then Exit Function

        FieldsName = StrFieldsName.Split(";")
        Dim FieldsValue(FieldsName.Length) As String
        For Index = 0 To FieldsName.Length - 1 : FieldsValue(Index) = "" : Next
        Index = 0

        Dim Node As New TreeNode : Dim x As Integer
        For x = 0 To TView.GetNodeCount(False) - 1
            Node = TView.Nodes(x)
            If TView.Nodes(x).Checked = True Then
                FieldsValue(Index) = FieldsValue(Index) & Node.Tag & ","
            Else
                CreateShart(Node, Index + 1, FieldsName, FieldsValue)
            End If
        Next x

        Dim StrSql As String = ""
        For x = 0 To FieldsName.Length - 1
            If FieldsValue(x).Length <> 0 Then
                FieldsValue(x) = FieldsValue(x).Remove(FieldsValue(x).Length - 1, 1)
                StrSql &= FieldsName(x) & " In (" & FieldsValue(x) & ") Or "
                StrLoadTree &= FieldsValue(x) & ";"
            Else
                StrLoadTree &= FieldsValue(x) & ";"
            End If
        Next
        If StrSql.Length <> 0 Then StrSql = StrSql.Remove(StrSql.Length - 3, 3)
        If StrLoadTree.Length <> 0 Then StrLoadTree = StrLoadTree.Remove(StrLoadTree.Length - 1, 1)

        Node = Nothing
        Return StrSql
    End Function
    Private Sub CreateShart(ByVal Node As TreeNode, ByVal Index As Integer, ByVal FieldsName() As String, ByVal FieldsValue() As String)
        If Index > FieldsName.Length + 1 Then Exit Sub

        Dim CNode As New TreeNode : Dim x As Integer
        For x = 0 To Node.GetNodeCount(False) - 1
            CNode = Node.Nodes(x)
            If Node.Nodes(x).Checked = True Then
                FieldsValue(Index) = FieldsValue(Index) & CNode.Tag & ","
            Else
                CreateShart(CNode, Index + 1, FieldsName, FieldsValue)
            End If
        Next x
        CNode = Nothing
    End Sub
End Class
