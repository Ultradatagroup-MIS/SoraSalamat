Public Class Tarikh
#Region "ForGlobal Variable BasCalendar"
    Private Structure sFarsiDate
        Dim Year As Integer
        Dim Month As Integer
        Dim Day As Integer
    End Structure
    Private Structure sLatinDate
        Dim Year As Integer
        Dim Month As Integer
        Dim Day As Integer
    End Structure
    Private LatinDate As sLatinDate
    Private FarsiDate As sFarsiDate

    Private FarsiDayTable(1, 12) As Integer
    Private LatinDayTable(1, 12) As Integer
    Private MiDayMont(12) As Integer

    Private ShEndDayMonthMiladi(3, 12) As Integer
    Private MiBegDayMonthShamsi(3, 12) As Integer
    Private MiMonthToShamsiMonth(2, 12) As Integer
#End Region
#Region " Set Array Day "
    Private Sub InitDayTable()
        FarsiDayTable(0, 1) = 31
        FarsiDayTable(0, 2) = 31
        FarsiDayTable(0, 3) = 31
        FarsiDayTable(0, 4) = 31
        FarsiDayTable(0, 5) = 31
        FarsiDayTable(0, 6) = 31
        FarsiDayTable(0, 7) = 30
        FarsiDayTable(0, 8) = 30
        FarsiDayTable(0, 9) = 30
        FarsiDayTable(0, 10) = 30
        FarsiDayTable(0, 11) = 30
        FarsiDayTable(0, 12) = 29


        FarsiDayTable(1, 1) = 31
        FarsiDayTable(1, 2) = 31
        FarsiDayTable(1, 3) = 31
        FarsiDayTable(1, 4) = 31
        FarsiDayTable(1, 5) = 31
        FarsiDayTable(1, 6) = 31
        FarsiDayTable(1, 7) = 30
        FarsiDayTable(1, 8) = 30
        FarsiDayTable(1, 9) = 30
        FarsiDayTable(1, 10) = 30
        FarsiDayTable(1, 11) = 30
        FarsiDayTable(1, 12) = 30

        LatinDayTable(0, 1) = 31
        LatinDayTable(0, 2) = 28
        LatinDayTable(0, 3) = 31
        LatinDayTable(0, 4) = 30
        LatinDayTable(0, 5) = 31
        LatinDayTable(0, 6) = 30
        LatinDayTable(0, 7) = 31
        LatinDayTable(0, 8) = 31
        LatinDayTable(0, 9) = 30
        LatinDayTable(0, 10) = 31
        LatinDayTable(0, 11) = 30
        LatinDayTable(0, 12) = 31


        LatinDayTable(1, 1) = 31
        LatinDayTable(1, 2) = 29
        LatinDayTable(1, 3) = 31
        LatinDayTable(1, 4) = 30
        LatinDayTable(1, 5) = 31
        LatinDayTable(1, 6) = 30
        LatinDayTable(1, 7) = 31
        LatinDayTable(1, 8) = 31
        LatinDayTable(1, 9) = 30
        LatinDayTable(1, 10) = 31
        LatinDayTable(1, 11) = 30
        LatinDayTable(1, 12) = 31

    End Sub
    Private Sub InitArrayes()
        On Error GoTo Err_InitArrayes

        MiDayMont(1) = 31 : MiDayMont(2) = 28 : MiDayMont(3) = 31 : MiDayMont(4) = 30
        MiDayMont(5) = 31 : MiDayMont(6) = 30 : MiDayMont(7) = 31 : MiDayMont(8) = 31
        MiDayMont(9) = 30 : MiDayMont(10) = 31 : MiDayMont(11) = 30 : MiDayMont(12) = 31

        ShEndDayMonthMiladi(1, 1) = 20 : ShEndDayMonthMiladi(1, 2) = 19
        ShEndDayMonthMiladi(1, 3) = 19 : ShEndDayMonthMiladi(1, 4) = 19
        ShEndDayMonthMiladi(1, 5) = 20 : ShEndDayMonthMiladi(1, 6) = 20
        ShEndDayMonthMiladi(1, 7) = 21 : ShEndDayMonthMiladi(1, 8) = 21
        ShEndDayMonthMiladi(1, 9) = 21 : ShEndDayMonthMiladi(1, 10) = 21
        ShEndDayMonthMiladi(1, 11) = 20 : ShEndDayMonthMiladi(1, 12) = 20

        ShEndDayMonthMiladi(2, 1) = 19 : ShEndDayMonthMiladi(2, 2) = 18
        ShEndDayMonthMiladi(2, 3) = 20 : ShEndDayMonthMiladi(2, 4) = 20
        ShEndDayMonthMiladi(2, 5) = 21 : ShEndDayMonthMiladi(2, 6) = 21
        ShEndDayMonthMiladi(2, 7) = 22 : ShEndDayMonthMiladi(2, 8) = 22
        ShEndDayMonthMiladi(2, 9) = 22 : ShEndDayMonthMiladi(2, 10) = 22
        ShEndDayMonthMiladi(2, 11) = 21 : ShEndDayMonthMiladi(2, 12) = 21

        ShEndDayMonthMiladi(3, 1) = 20 : ShEndDayMonthMiladi(3, 2) = 19
        ShEndDayMonthMiladi(3, 3) = 20 : ShEndDayMonthMiladi(3, 4) = 20
        ShEndDayMonthMiladi(3, 5) = 21 : ShEndDayMonthMiladi(3, 6) = 21
        ShEndDayMonthMiladi(3, 7) = 22 : ShEndDayMonthMiladi(3, 8) = 22
        ShEndDayMonthMiladi(3, 9) = 22 : ShEndDayMonthMiladi(3, 10) = 22
        ShEndDayMonthMiladi(3, 11) = 21 : ShEndDayMonthMiladi(3, 12) = 21

        MiBegDayMonthShamsi(1, 1) = 11 : MiBegDayMonthShamsi(1, 2) = 12
        MiBegDayMonthShamsi(1, 3) = 11 : MiBegDayMonthShamsi(1, 4) = 13
        MiBegDayMonthShamsi(1, 5) = 12 : MiBegDayMonthShamsi(1, 6) = 12
        MiBegDayMonthShamsi(1, 7) = 11 : MiBegDayMonthShamsi(1, 8) = 11
        MiBegDayMonthShamsi(1, 9) = 11 : MiBegDayMonthShamsi(1, 10) = 10
        MiBegDayMonthShamsi(1, 11) = 11 : MiBegDayMonthShamsi(1, 12) = 12

        MiBegDayMonthShamsi(2, 1) = 12 : MiBegDayMonthShamsi(2, 2) = 13
        MiBegDayMonthShamsi(2, 3) = 11 : MiBegDayMonthShamsi(2, 4) = 12
        MiBegDayMonthShamsi(2, 5) = 11 : MiBegDayMonthShamsi(2, 6) = 11
        MiBegDayMonthShamsi(2, 7) = 10 : MiBegDayMonthShamsi(2, 8) = 10
        MiBegDayMonthShamsi(2, 9) = 10 : MiBegDayMonthShamsi(2, 10) = 9
        MiBegDayMonthShamsi(2, 11) = 10 : MiBegDayMonthShamsi(2, 12) = 10

        MiBegDayMonthShamsi(3, 1) = 11 : MiBegDayMonthShamsi(3, 2) = 12
        MiBegDayMonthShamsi(3, 3) = 10 : MiBegDayMonthShamsi(3, 4) = 12
        MiBegDayMonthShamsi(3, 5) = 11 : MiBegDayMonthShamsi(3, 6) = 11
        MiBegDayMonthShamsi(3, 7) = 10 : MiBegDayMonthShamsi(3, 8) = 10
        MiBegDayMonthShamsi(3, 9) = 10 : MiBegDayMonthShamsi(3, 10) = 9
        MiBegDayMonthShamsi(3, 11) = 10 : MiBegDayMonthShamsi(3, 12) = 10

        MiMonthToShamsiMonth(1, 1) = 10 : MiMonthToShamsiMonth(1, 2) = 11
        MiMonthToShamsiMonth(1, 3) = 12 : MiMonthToShamsiMonth(1, 4) = 1
        MiMonthToShamsiMonth(1, 5) = 2 : MiMonthToShamsiMonth(1, 6) = 3
        MiMonthToShamsiMonth(1, 7) = 4 : MiMonthToShamsiMonth(1, 8) = 5
        MiMonthToShamsiMonth(1, 9) = 6 : MiMonthToShamsiMonth(1, 10) = 7
        MiMonthToShamsiMonth(1, 11) = 8 : MiMonthToShamsiMonth(1, 12) = 9

        MiMonthToShamsiMonth(2, 1) = 11 : MiMonthToShamsiMonth(2, 2) = 12
        MiMonthToShamsiMonth(2, 3) = 1 : MiMonthToShamsiMonth(2, 4) = 2
        MiMonthToShamsiMonth(2, 5) = 3 : MiMonthToShamsiMonth(2, 6) = 4
        MiMonthToShamsiMonth(2, 7) = 5 : MiMonthToShamsiMonth(2, 8) = 6
        MiMonthToShamsiMonth(2, 9) = 7 : MiMonthToShamsiMonth(2, 10) = 8
        MiMonthToShamsiMonth(2, 11) = 9 : MiMonthToShamsiMonth(2, 12) = 10

Exit_InitArrayes:

        Exit Sub

Err_InitArrayes:

        MsgBox("Error code is : " & Err.Number & Chr(10) & Chr(13) & "Error message is : " & Err.Description, vbOKOnly + vbExclamation, "Error in InitArrayes subroutin")
        Resume Exit_InitArrayes

    End Sub
#End Region
    '#Region " Constructor "
    '    Sub New()

    '        Call InitDayTable()
    '        Call InitArrayes()

    '    End Sub

    '#End Region

    Public Function IsShDate(ByVal strFarsiDate As String, Optional ByVal ShowMsgBoxErr As Boolean = True) As Boolean
        On Error GoTo Err_IsShDate
        IsShDate = False

        strFarsiDate = strFarsiDate.Replace("/", "")

        If Len(Trim$(strFarsiDate)) = 0 Then
            IsShDate = True
            Exit Function
        End If

        If Len(Trim$(strFarsiDate)) < 8 Then
            If ShowMsgBoxErr Then
                MsgBox("تعداد کارکترهای وارد شده اشتباه است ", vbOKOnly + vbExclamation, " خطا ")
                'MsgBox("1", vbOKOnly + vbExclamation, " ÎØÇ ")
            End If
            Exit Function
        End If
        Call InitDayTable()
        FarsiDate.Year = CInt(Mid(strFarsiDate, 1, 4))
        FarsiDate.Month = CInt(Mid(strFarsiDate, 5, 2))
        FarsiDate.Day = CInt(Mid(strFarsiDate, 7, 8))

        If FarsiDate.Year < 1250 Then
            If ShowMsgBoxErr Then
                'MsgBox("ÇÔßÇá ÏÑ æÑæÏ ÇØáÇÚÇÊ ÓÇá", vbOKOnly + vbExclamation, " ÎØÇ ")
                MsgBox("سال موردنظر مشکل دارد", vbOKOnly + vbExclamation, " خطا ")
            End If
            Exit Function

        End If

        'If FarsiDate.Month < 1 Or FarsiDate.Month > 12 Then
        If FarsiDate.Month > 12 Or FarsiDate.Month < 1 Then
            If ShowMsgBoxErr Then
                'MsgBox("ÇÔßÇá ÏÑ æÑæÏ ÇØáÇÚÇÊ ãÇå", vbOKOnly + vbExclamation, " ÎØÇ ")
                MsgBox("ماه وارد شده مشکل دارد", vbOKOnly + vbExclamation, " خطا ")
            End If

            Exit Function
        End If

        Dim i As Integer

        If LeapYearShamsi(FarsiDate.Year) Then
            i = 1
        Else
            i = 0
        End If

        'If FarsiDate.Day < 1 Or FarsiDate.Day > FarsiDayTable(i, FarsiDate.Month) Then
        If FarsiDate.Day > FarsiDayTable(i, FarsiDate.Month) Or FarsiDate.Day < 1 Then
            If ShowMsgBoxErr Then
                'MsgBox("ÇÔßÇá ÏÑ æÑæÏ ÇØáÇÚÇÊ ÑæÒ", vbOKOnly + vbExclamation, " ÎØÇ ")
                MsgBox("بازه تاریخی وارد شده مشکل دارد", vbOKOnly + vbExclamation, " خطا ")
            End If
            Exit Function
        End If

        IsShDate = True
        'If IsShDate And ShowMsgBoxErr Then
        'MsgBox("Tarikh dorost mibashad", vbOKOnly + vbInformation, " Msg ")
        'End If

Exit_IsShDate:

        Exit Function

Err_IsShDate:
        IsShDate = True
        MsgBox("Error code is : " & Err.Number & Chr(10) & Chr(13) & "Error message is : " & Err.Description, vbOKOnly + vbExclamation, "Error in IsShDate function")
        Resume Exit_IsShDate

    End Function
    Public Function IsMiDate(ByVal strLatinDate As String, Optional ByVal ShowMsgBoxErr As Boolean = True) As Boolean
        On Error GoTo Err_IsMiDate
        IsMiDate = False

        If Len(Trim$(strLatinDate)) = 0 Then
            IsMiDate = True
            Exit Function
        End If

        If Len(Trim$(strLatinDate)) < 8 Then
            If ShowMsgBoxErr Then
                MsgBox("ÇÔßÇá ÏÑ æÑæÏ ÇØáÇÚÇÊ ÊÇÑíÎ", vbOKOnly + vbExclamation, " ÎØÇ ")
            End If
            Exit Function
        End If

        Call InitDayTable()

        LatinDate.Year = CInt(Mid(strLatinDate, 1, 4))
        LatinDate.Month = CInt(Mid(strLatinDate, 5, 2))
        LatinDate.Day = CInt(Mid(strLatinDate, 7, 8))

        If LatinDate.Year < 1990 Or LatinDate.Year > 2100 Then
            If ShowMsgBoxErr Then
                MsgBox("ÇÔßÇá ÏÑ æÑæÏ ÇØáÇÚÇÊ ÓÇá", vbOKOnly + vbExclamation, " ÎØÇ ")
            End If
            Exit Function
        End If

        If LatinDate.Month < 1 Or LatinDate.Month > 12 Then
            If ShowMsgBoxErr Then
                MsgBox("ÇÔßÇá ÏÑ æÑæÏ ÇØáÇÚÇÊ ãÇå", vbOKOnly + vbExclamation, " ÎØÇ ")
            End If
            Exit Function
        End If


        Dim i As Integer

        If LeapYearMiladi(LatinDate.Year) Then
            i = 1
        Else
            i = 0
        End If

        If LatinDate.Day < 1 Or LatinDate.Day > LatinDayTable(i, LatinDate.Month) Then
            If ShowMsgBoxErr Then
                MsgBox("ÇÔßÇá ÏÑ æÑæÏ ÇØáÇÚÇÊ ÑæÒ", vbOKOnly + vbExclamation, " ÎØÇ ")
            End If
            Exit Function
        End If

        IsMiDate = True

        ' If IsMiDate And ShowMsgBoxErr Then
        '    MsgBox("Tarikh dorost mibashad", vbOKOnly + vbInformation, " Msg ")
        'End If

Exit_IsMiDate:

        Exit Function

Err_IsMiDate:
        IsMiDate = True
        MsgBox("Error code is : " & Err.Number & Chr(10) & Chr(13) & "Error message is : " & Err.Description, vbOKOnly + vbExclamation, "Error in IsMiDate function")
        Resume Exit_IsMiDate

    End Function
    Public Function Sh2Mi(ByVal strFarsiDate As String) As String
        On Error GoTo Err_Sh2Mi

        Dim engDays, engYear, engMonth, engDay, farsiDaysPerYear, farsiDays As Integer
        Dim leapYear As Integer, indx As Integer, offset As Integer
        Dim FarsiLeapYear As Integer

        Sh2Mi = ""

        If Not IsShDate(strFarsiDate, False) Then Exit Function
        If strFarsiDate.Length = 0 Then Exit Function

        FarsiDate.Year = CInt(Mid(strFarsiDate, 1, 4))
        FarsiDate.Month = CInt(Mid(strFarsiDate, 5, 2))
        FarsiDate.Day = CInt(Mid(strFarsiDate, 7, 8))

        'check if given farsi date is valid
        ' Note: The last line is not exact. It assumes Farsi leap year. if not, Esfand 30 is a wrong day of the
        ' month. This will be corrected later on.

        'find days passed since Farvardin 1st
        farsiDays = 0
        leapYear = 0 'for the moment assume non-leap-year
        For indx = 1 To FarsiDate.Month - 1
            farsiDays = farsiDays + FarsiDayTable(leapYear, indx)
        Next
        farsiDays = farsiDays + FarsiDate.Day

        'calculate English year
        If FarsiDate.Year < 1374 Then    ' Use old calendar formula
            If farsiDays <= 286 Then '286 is Farvardin 1st to Day 10th (Dec 31st) inclusive'
                engYear = FarsiDate.Year + 621
            Else
                engYear = FarsiDate.Year + 622
            End If
        Else                              ' Use new calendar formula
            ' First of all, see if the English year preceeding Farvardin of the
            ' given Farsi year was leap or not. If so, current Farsi year is leap.
            Dim tempEngYear As Integer
            tempEngYear = FarsiDate.Year + 621

            If (tempEngYear Mod 4) = 0 And (tempEngYear Mod 100) <> 0 Or (tempEngYear Mod 400) = 0 Then
                FarsiLeapYear = 1
            Else
                FarsiLeapYear = 0
            End If

            ' Now get the English year
            If (FarsiLeapYear = 0 And farsiDays <= 286) Or (FarsiLeapYear = 1 And farsiDays <= 287) Then
                '286 (or 287) is Farvardin 1st to Day 10th (Dec 31st) inclusive'
                engYear = FarsiDate.Year + 621
            Else
                engYear = FarsiDate.Year + 622
            End If
        End If

        'Now check if farsi day is valid
        If FarsiDate.Day < 1 Or FarsiDate.Day > FarsiDayTable(FarsiLeapYear, FarsiDate.Month) Then GoTo Err_Sh2Mi

        'Check if the English year is leap year.
        If (engYear Mod 4) = 0 And (engYear Mod 100) <> 0 Or (engYear Mod 400) = 0 Then
            leapYear = 1
        Else
            leapYear = 0
        End If


        'calculate English total days
        If FarsiDate.Year < 1374 Then    ' Use old calendar formula
            'Jan 1st to Farvardin 1st offset
            If FarsiLeapYear = 1 Then
                offset = 80
            Else
                offset = 79
            End If

            If farsiDays <= 286 Then 'from Farv. 1st to Day 10th (Dec 31st) inclusive
                engDays = farsiDays + offset
            Else
                engDays = farsiDays - 286
            End If
        Else                              ' Use new calendar formula
            'Jan 1st to Farvardin 1st offset
            offset = 79

            If FarsiLeapYear = 1 Then
                If farsiDays <= 287 Then
                    engDays = farsiDays + offset
                Else
                    engDays = farsiDays - 287
                End If
            Else
                If farsiDays <= 286 Then
                    engDays = farsiDays + offset
                Else
                    engDays = farsiDays - 286
                End If
            End If
        End If

        'calculate english month & day
        For indx = 1 To 12
            If engDays <= LatinDayTable(leapYear, indx) Then Exit For
            engDays = engDays - LatinDayTable(leapYear, indx)
        Next
        engMonth = indx
        engDay = engDays

        'now build english date

        Sh2Mi = CStr(engYear) & LeftPad(CStr(engMonth), 2, 48) & LeftPad(CStr(engDay), 2, 48)



Exit_Sh2Mi:

        Exit Function

Err_Sh2Mi:

        Sh2Mi = False
        MsgBox("Error code is : " & Err.Number & Chr(10) & Chr(13) & "Error message is : " & Err.Description, vbOKOnly + vbExclamation, "Error in Sh2Mi function")
        Resume Exit_Sh2Mi

    End Function
    Public Function Mi2Sh(ByVal varDate As Object, Optional ByVal UsedDateFunction As Boolean = True) As String
        Mi2Sh = ""
        Try
            Dim daynn, yy, mm, dd, dayaa, i As Integer

            Dim dayi() As Integer = {31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29}
            Dim daya() As Integer = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}

            If varDate.GetType.ToString = GetType(Date).ToString Then
                yy = Year(varDate)
                mm = Month(varDate)
                dd = Microsoft.VisualBasic.DateAndTime.Day(varDate)
            ElseIf varDate.GetType.ToString = GetType(String).ToString Then
                yy = Val(Mid(varDate, 1, 4))
                mm = Val(Mid(varDate, 5, 2))
                dd = Val(Mid(varDate, 7, 2))
            End If

            daynn = 286
            If yy > 1998 Then
                For i = 1998 To yy - 1
                    daynn = daynn + 365 + IIf(LeapYearMiladi(i) = True, 1, 0)
                Next i
            End If
            If mm <> 1 Then
                For i = 1 To mm - 1
                    If LeapYearMiladi(yy) = True And i = 2 Then
                        daynn = daynn + 29
                    Else
                        daynn = daynn + daya(i - 1)
                    End If
                Next i
            End If
            daynn = daynn + dd

            yy = 1376 : mm = 1 : dd = daynn : dayaa = 0
            Do While (1)
                If LeapYearShamsi(yy) = True And mm = 12 Then
                    dayaa = 30
                Else
                    dayaa = dayi(mm - 1)
                End If
                If dd > dayaa Then
                    dd = dd - dayaa
                    mm = mm + 1
                    If mm = 13 Then
                        mm = 1 : yy = yy + 1
                    End If
                Else
                    Exit Do
                End If
            Loop
            Mi2Sh = Trim(Str(yy)) + IIf(Len(Trim(Str(mm))) = 1, "0" & Trim(Str(mm)), Trim(Str(mm))) + IIf(Len(Trim(Str(dd))) = 1, "0" & Trim(Str(dd)), Trim(Str(dd)))

        Catch ex As Exception
            MsgBox("Error code is : " & Err.Number & Chr(10) & Chr(13) & "Error message is : " & Err.Description, vbOKOnly + vbExclamation, "Error in Mi2Sh function")
        End Try
    End Function
    Public Function Mi2ShNS(ByVal varDate As Object, Optional ByVal UsedDateFunction As Boolean = True) As String
        On Error GoTo Err_Mi2ShNS

        Dim yy As Integer
        Dim Ym As Integer
        Dim j As Integer
        Dim dd As Integer
        Dim U As Integer
        Dim i As Integer
        Dim rp As Integer
        Dim P As Integer
        Dim LS As Integer
        Dim MN As Integer
        Dim SD As Integer
        Dim YE As Integer
        Dim Z As Integer
        Dim d As Integer
        Dim H As Integer
        Mi2ShNS = ""
        Call InitArrayes()

        If Not UsedDateFunction Then
            If Not IsMiDate(varDate, False) Then Exit Function
            If varDate.Length = 0 Then Exit Function
            yy = CInt(Mid(varDate, 1, 4))
            j = CInt(Mid(varDate, 5, 2))
            dd = CInt(Mid(varDate, 7, 8))
        Else
            yy = Year(varDate)
            j = Month(varDate)
            dd = Microsoft.VisualBasic.Day(varDate)
            If yy <= 621 Then Exit Function
            If j <= 0 Or j > 12 Then Exit Function
        End If

        If LeapYearMiladi(yy) Then
            U = 1
        Else
            U = 0
        End If
        H = 0
        If j = 2 And U = 1 Then
            H = 1
        End If

        If UsedDateFunction Then
            If dd <= 0 Or dd > MiDayMont(j) + H Then Exit Function
        End If


        Ym = yy - 622
        If LeapYearShamsi(Ym) Then
            rp = 1
        Else
            rp = 0
        End If
        If U = 1 Then
            If rp = 0 Then
                i = 1
            Else
                i = 4
            End If
        Else
            If rp = 0 Then
                i = 3
            Else
                i = 2
            End If
        End If
        Z = 0
        If i = 4 And j = 3 Then
            Z = 1
        End If
        If i = 4 Then
            i = 3
        End If
        If 1 <= dd And dd <= ShEndDayMonthMiladi(i, j) Then
            SD = MiBegDayMonthShamsi(i, j) + dd + Z - 1
            MN = MiMonthToShamsiMonth(1, j)
            LS = 1
        Else
            SD = dd - ShEndDayMonthMiladi(i, j)
            MN = MiMonthToShamsiMonth(2, j)
            LS = 2
        End If
        If j <= 3 Then
            YE = yy - 622
        End If
        If j = 3 And LS = 2 Then
            YE = yy - 621

        End If
        If j > 3 Then
            YE = yy - 621
        End If

        Mi2ShNS = LeftPad(CStr(YE), 4, 48) & "" & LeftPad(CStr(MN), 2, 48) & "" & LeftPad(CStr(SD), 2, 48)

Exit_Mi2ShNs:

        Exit Function

Err_Mi2ShNS:

        MsgBox("Error code is : " & Err.Number & Chr(10) & Chr(13) & "Error message is : " & Err.Description, vbOKOnly + vbExclamation, "Error in Mi2ShNS function")
        Resume Exit_Mi2ShNs

    End Function
    Public Function GetDateSlash(ByVal tDate As String) As String
        GetDateSlash = ""
        If Len(tDate) = 10 Then
            GetDateSlash = Microsoft.VisualBasic.Left(tDate, 4) & Microsoft.VisualBasic.Mid$(tDate, 6, 2) & Microsoft.VisualBasic.Right(tDate, 2)
        ElseIf Len(tDate) = 8 Then
            GetDateSlash = tDate
        End If
    End Function
    Public Function SetDateSlash(ByVal tDate As String) As String
        SetDateSlash = ""
        If Len(tDate) = 10 Then
            SetDateSlash = tDate
        ElseIf Len(tDate) = 8 Then
            SetDateSlash = Microsoft.VisualBasic.Left(tDate, 4) & "/" & Microsoft.VisualBasic.Mid$(tDate, 5, 2) & "/" & Microsoft.VisualBasic.Right(tDate, 2)
        End If
    End Function
    Public Function ShamsiDay(ByVal ShDate As String) As Integer
        ShamsiDay = Microsoft.VisualBasic.Right(ShDate, 2)
    End Function
    Public Function ShamsiMonth(ByVal ShDate As String) As Integer
        ShamsiMonth = Microsoft.VisualBasic.Mid$(ShDate, 5, 2)
    End Function
    Public Function ShamsiYear(ByVal ShDate As String) As Integer
        ShamsiYear = Microsoft.VisualBasic.Left(ShDate, 4)
    End Function
    Public Function NDay(ByVal yy As Integer, ByVal mm As Byte) As Byte
        If mm < 7 Then
            NDay = 31
        ElseIf mm < 12 Then
            NDay = 30
        ElseIf LeapYearShamsi(yy) Then
            NDay = 30
        Else
            NDay = 29
        End If
    End Function
    Public Function App_Path() As String
        Return System.AppDomain.CurrentDomain.BaseDirectory()
    End Function
    Public Function ConvertNulls(ByVal s As Object, ByVal c As Object) As Object
        ConvertNulls = IIf(IsDBNull(s), c, s)
    End Function
    Public Function LeapYearMiladi(ByVal CurYear As Integer) As Boolean
        On Error GoTo Err_LeapYearMiladi

        LeapYearMiladi = False

        If CurYear Mod 4 = 0 And CurYear Mod 100 = 0 Then
            If CurYear Mod 400 = 0 Then
                LeapYearMiladi = True
            Else
                LeapYearMiladi = False
            End If
        ElseIf CurYear Mod 4 = 0 Then
            LeapYearMiladi = True
        Else
            LeapYearMiladi = False
        End If

Exit_LeapYearMiladi:

        Exit Function

Err_LeapYearMiladi:

        LeapYearMiladi = False
        MsgBox("Error code is : " & Err.Number & Chr(10) & Chr(13) & "Error message is : " & Err.Description, vbOKOnly + vbExclamation, "Error in LeapYearMiladi function")
        Resume Exit_LeapYearMiladi

    End Function
    Public Function LeapYearShamsi(ByVal Ym As Integer) As Boolean
        On Error GoTo Err_LeapYearShamsi

        Dim s As Integer
        Dim KKB As Integer
        Dim X As Integer
        Dim W As Integer

        LeapYearShamsi = False
        s = Int((Ym + 16) / 33)
        KKB = s * 33 - 16
        If KKB + 1 <> Ym Then
            X = Int((Ym + 15) / 33)
            W = Ym - X - 17
            If W Mod 4 = 0 Then
                LeapYearShamsi = True
            Else
                LeapYearShamsi = False
            End If
        Else
            LeapYearShamsi = False
        End If

Exit_LeapYearShamsi:

        Exit Function

Err_LeapYearShamsi:

        LeapYearShamsi = False
        MsgBox("Error code is : " & Err.Number & Chr(10) & Chr(13) & "Error message is : " & Err.Description, vbOKOnly + vbExclamation, "Error in LeapYearShamsi function")
        Resume Exit_LeapYearShamsi


    End Function
    Public Function LeftPad(ByVal InString As String, ByVal StrLen As Object, ByVal PadChr As Object) As String
        On Error GoTo Err_LeftPad

        Dim PadingLen As Object
        Dim i As Integer
        Dim PadedStr As String

        InString = Trim(InString)
        PadingLen = StrLen - Len(InString)
        PadedStr = ""
        For i = 1 To PadingLen Step 1
            PadedStr = PadedStr + Chr(PadChr)
        Next
        LeftPad = PadedStr + InString

Exit_LeftPad:

        Exit Function

Err_LeftPad:

        MsgBox("Error code is : " & Err.Number & Chr(10) & Chr(13) & "Error message is : " & Err.Description, vbOKOnly + vbExclamation, "Error in LeftPad function")
        Resume Exit_LeftPad

    End Function
    Public Function AddDay(ByVal Tarikh As String, ByVal Rooz As Integer) As String
        Dim fYear As Integer
        Dim fMonth As Integer
        Dim fDay As Integer
        Dim d As Date = Today

        fYear = CInt(Tarikh.Substring(0, 4))
        fMonth = CInt(Tarikh.Substring(4, 2))
        fDay = CInt(Tarikh.Substring(6, 2))

        For i As Integer = 1 To Rooz
            If fMonth <= 6 Then
                Dim t As Integer = 0
                If fDay + 1 <= 31 Then
                    fDay += 1
                ElseIf fDay + 1 > 31 Then
                    fDay += 1
                    Dim TmpFday As Integer = fDay
                    fDay = fDay Mod 31
                    fMonth += tmpfDay \ 31
                    If fMonth > 12 Then
                        Dim Tmpfmonth As Integer = fMonth
                        fMonth = fMonth Mod 12
                        fYear += Tmpfmonth \ 12
                    End If
                End If
            ElseIf fMonth > 6 And fMonth < 12 Then
                Dim t As Integer = 0
                If fDay + 1 <= 30 Then
                    fDay += 1
                ElseIf fDay + 1 > 30 Then
                    fDay += 1
                    Dim TmpFday As Integer = fDay
                    fDay = fDay Mod 30
                    fMonth += tmpfDay \ 30
                    If fMonth > 12 Then
                        Dim Tmpfmonth As Integer = fMonth
                        fMonth = fMonth Mod 12
                        fYear += Tmpfmonth \ 12
                    End If
                End If
            ElseIf fMonth = 12 Then
                Dim l As Integer
                If LeapYearShamsi(fYear) Then l = 30 Else l = 29
                Dim t As Integer = 0
                If fDay + 1 <= l Then
                    fDay += 1
                ElseIf fDay + 1 > l Then
                    fDay += 1
                    Dim TmpFday As Integer = fDay
                    fDay = fDay Mod l
                    fMonth += tmpfDay \ l
                    If fMonth > 12 Then
                        Dim Tmpfmonth As Integer = fMonth
                        fMonth = fMonth Mod 12
                        fYear += Tmpfmonth \ 12
                    End If
                End If
            End If
        Next

        AddDay = SetDateSlash(fYear.ToString + Format(fMonth, "00").ToString + Format(fDay, "00").ToString)
    End Function
    Public Function DecDay(ByVal Tarikh As String, ByVal Rooz As Integer) As String
        Dim fYear As Integer
        Dim fMonth As Integer
        Dim fDay As Integer
        Dim d As Date = Today

        fYear = CInt(Tarikh.Substring(0, 4))
        fMonth = CInt(Tarikh.Substring(4, 2))
        fDay = CInt(Tarikh.Substring(6, 2))

        For i As Integer = 1 To Rooz
            If 2 <= fMonth And fMonth <= 6 Then
                Dim t As Integer = 0
                If fDay - 1 > 0 Then
                    fDay -= 1
                ElseIf fDay - 1 <= 0 Then
                    fDay -= 1
                    Dim TmpFday As Integer = fDay
                    fDay = 31
                    fMonth -= 1
                    If fMonth = 0 Then
                        fMonth = 12
                    End If
                End If
            ElseIf fMonth > 6 Then
                Dim t As Integer = 0
                If fDay - 1 > 0 Then
                    fDay -= 1
                ElseIf fDay - 1 <= 0 Then
                    If fMonth - 1 > 6 Then
                        fDay = 30
                        fMonth -= 1
                    Else
                        fDay = 30
                        fMonth -= 1
                    End If
                End If
            ElseIf fMonth = 1 Then
                Dim t As Integer = 0
                If fDay - 1 = 0 Then
                    Dim l As Integer
                    If LeapYearShamsi(fYear - 1) = True Then
                        l = 30
                    ElseIf LeapYearShamsi(fYear - 1) = False Then
                        l = 29
                    End If

                    fDay = l
                    fMonth = 12
                    fYear -= 1
                ElseIf fDay - 1 > 0 Then
                    fDay -= 1
                End If
            End If
        Next

        DecDay = SetDateSlash(fYear.ToString + Format(fMonth, "00").ToString + Format(fDay, "00").ToString)
    End Function
    Function GetTedadRooz(ByVal sal As Integer, ByVal Mah As Integer) As Integer
        If Mah <= 6 Then
            GetTedadRooz = 31
        ElseIf Mah >= 6 And Mah < 12 Then
            GetTedadRooz = 30
        ElseIf Mah = 12 Then
            If LeapYearShamsi(sal) Then GetTedadRooz = 30 Else GetTedadRooz = 29
        End If
    End Function
    Public Function AddMonth(ByVal Tarikh As String, ByVal val As Integer) As String
        Dim fYear As Integer
        Dim fMonth As Integer
        Dim fDay As Integer
        Dim d As Date = Today

        fYear = CInt(Tarikh.Substring(0, 4))
        fMonth = CInt(Tarikh.Substring(4, 2))
        fDay = CInt(Tarikh.Substring(6, 2))

        If fMonth + val > 12 Then
            Dim Tmpfmonth As Integer = fMonth
            fMonth = (fMonth + val) Mod 12
            fYear += (Tmpfmonth + val) \ 12
        Else
            fMonth += val
        End If

        AddMonth = SetDateSlash(fYear.ToString + Format(fMonth, "00").ToString + Format(fDay, "00").ToString)
    End Function
    Public Function AddYear(ByVal Tarikh As String, ByVal val As Integer) As String
        Dim fYear As Integer
        Dim fMonth As Integer
        Dim fDay As Integer
        Dim d As Date = Today

        fYear = CInt(Tarikh.Substring(0, 4))
        fMonth = CInt(Tarikh.Substring(4, 2))
        fDay = CInt(Tarikh.Substring(6, 2))

        fYear += val

        AddYear = SetDateSlash(fYear.ToString + Format(fMonth, "00").ToString + Format(fDay, "00").ToString)
    End Function
    Function DiffDaySh(ByVal AzTarikh As String, ByVal TaTarikh As String) As Integer
        Dim T As String = AzTarikh
        DiffDaySh = 1

        Do Until T = TaTarikh
            T = AddDay(T, 1)
            T = GetDateSlash(T)
            DiffDaySh += 1
        Loop
    End Function
    Function WeekInYear(ByVal Tarikh As String) As Integer
        Dim TedadRoozSal As Integer
        Dim TarikhAvalinRooz As Date = SetDateSlash(Sh2Mi(Mid(Tarikh, 1, 4) & "0101"))
        Dim TarikhRooz As Date = SetDateSlash(Sh2Mi(Tarikh))
        Dim RoozSal As Integer
        'ãÍÇÓÈå ÊÚÏÇÏ ÑæÒåÇí ÓÇá
        For I As Integer = 1 To 12
            TedadRoozSal += Date.DaysInMonth(TarikhAvalinRooz.Year, I)
        Next

        If (TarikhRooz.DayOfYear - TarikhAvalinRooz.DayOfYear) + 1 > 0 Then
            RoozSal = (TarikhRooz.DayOfYear - TarikhAvalinRooz.DayOfYear) + 1
        Else
            RoozSal = (TarikhRooz.DayOfYear + TedadRoozSal) - TarikhAvalinRooz.DayOfYear + 1
        End If

        Dim RoozWeek As Integer = Weekday(TarikhAvalinRooz, FirstDayOfWeek.Saturday) - 1

        If (RoozSal + RoozWeek) Mod 7 = 0 Then
            Return (RoozSal + RoozWeek) \ 7
        Else
            Return ((RoozSal + RoozWeek) \ 7) + 1
        End If
    End Function 'WeekInYear
End Class
