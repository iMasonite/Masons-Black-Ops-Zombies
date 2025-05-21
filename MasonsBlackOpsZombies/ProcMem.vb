Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Text
Public Class ProcMem

#Region " API "
    <DllImport("kernel32.dll")> _
    Public Shared Function ReadProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As Integer, ByVal buffer As Byte(), ByVal size As Integer, ByVal lpNumberOfBytesRead As Integer) As Boolean
    End Function
    <DllImport("kernel32.dll")> _
    Public Shared Function WriteProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As Integer, ByVal buffer As Byte(), ByVal size As Integer, ByVal lpNumberOfBytesWritten As Integer) As Boolean
    End Function
    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function VirtualAllocEx(ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, ByVal dwSize As Integer, ByVal flAllocationType As UInt32, ByVal flProtect As UInt32) As IntPtr
    End Function
    <DllImport("kernel32.dll", SetLastError:=True, ExactSpelling:=True)> _
    Private Shared Function VirtualFreeEx(ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, ByVal dwSize As Byte, ByVal dwFreeType As UInt32) As Boolean
    End Function
    <DllImport("user32.dll")> _
    Public Shared Function GetKeyState(ByVal nVirtKey As Keys) As Short
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Public Shared Function PostMessage(ByVal hhwnd As IntPtr, ByVal msg As UInt32, ByVal wparam As IntPtr, ByVal lparam As IntPtr) As Boolean
    End Function
#End Region

#Region " Get Process "

    Public Sub GetProcess(ByVal ProcessName As String)
        If ProcessName <> String.Empty Then
            PName = ProcessName
            MakeTimer(New EventHandler(AddressOf ProcTimer), 100)
        End If
    End Sub

    Private Sub ProcTimer(ByVal sender As Object, ByVal e As EventArgs)
        If (Not ProcActive) Then
            Proc = Process.GetProcessesByName(PName)
            ProcActive = Proc.Length <> 0
        End If
        Try
            ProcActive = Not Proc(0).HasExited
            iHandle = Proc(0).MainWindowHandle
        Catch obj1 As Exception
            iHandle = IntPtr.Zero
            ProcActive = False
        End Try
    End Sub

    Private Sub MakeTimer(ByVal iTimed As EventHandler, ByVal iIntervals As Integer)
        Dim timer As New Timer
        timer.Interval = iIntervals
        timer.Start()
        AddHandler timer.Tick, New EventHandler(AddressOf iTimed.Invoke)
    End Sub

#End Region

#Region " Storage "
    Private Proc As Process()
    Private iHandle As IntPtr
    Public iActive As Boolean
    Protected PName As String
    Public ProcActive As Boolean
    Private CodeCave As New List(Of IntPtr)
    Private OldScan As New List(Of String)
#End Region

#Region " Read Memory "

    Public Function rBytes(ByVal _A As Integer, ByVal _S As Integer) As Byte()
        Dim buff As Byte() = New Byte(_S - 1) {}
        If ProcActive Then
            ReadProcessMemory(Proc(0).Handle, _A, buff, _S, 0)
        End If
        Return buff
    End Function

    Public Function Read(ByVal _Address As Integer, ByVal MemType As Object) As Object
        If (_Address <> -1) Then
            Select Case Array.IndexOf(Of Object)(New Object() {GetType(Byte), GetType(Integer), GetType(UInt32), GetType(Single), GetType(Double)}, MemType)
                Case 0
                    Return rBytes(_Address, 1)(0)
                Case 1
                    Return BitConverter.ToInt32(rBytes(_Address, 4), 0)
                Case 2
                    Return BitConverter.ToUInt32(rBytes(_Address, 4), 0)
                Case 3
                    Return BitConverter.ToSingle(rBytes(_Address, 4), 0)
                Case 4
                    Return BitConverter.ToDouble(rBytes(_Address, 8), 0)
                Case -1
                    Return -1
            End Select
        End If
        Return -1
    End Function

    Public Function Read(ByVal _Address As String, ByVal MemType As Object) As Object
        Return Read(Pointer(_Address), MemType)
    End Function

    Public Function ReadString(ByVal _Address As Integer, ByVal _Length As Integer, ByVal UnicodeType As Boolean) As String
        If (_Address <> -1) Then
            Return CutString(Encoding.ASCII.GetString(rBytes(_Address, _Length)))
        End If
        Return String.Empty
    End Function

    Public Function ReadF(ByVal _Address As Integer) As Integer
        Return BitConverter.ToSingle(rBytes(_Address, 4), 0)
    End Function

    Public Function ReadI(ByVal _Address As Integer) As Integer
        Return BitConverter.ToInt32(rBytes(_Address, 4), 0)
    End Function

#End Region

#Region " Write Memory "
    Private Function wBytes(ByVal _A As Integer, ByVal _B As Byte()) As Boolean
        If ProcActive Then
            Return IIf(ProcActive, WriteProcessMemory(Proc(0).Handle, _A, _B, _B.Length, 0), False)
        End If
        Return True
    End Function

    Public Sub WriteString(ByVal _Address As IntPtr, ByVal pBytes As String)
        wBytes(_Address, System.Text.Encoding.ASCII.GetBytes((pBytes & ChrW(0))))
    End Sub

    Public Sub Write(ByVal _Address As Integer, ByVal Value As String, ByVal MemType As Object)
        WriteMem(_Address, Value, MemType)
    End Sub

    Public Sub Write(ByVal _Address As String, ByVal Value As Integer, ByVal MemType As Object)
        WriteMem(Pointer(_Address), Value, MemType)
    End Sub
    Public Sub Write(ByVal _Address As String, ByVal Value As Object, ByVal MemType As Object)
        WriteMem(Pointer(_Address), Value, MemType)
    End Sub

    Public Function Write(ByVal _Address As String, ByVal _Hack As String, ByVal _Default As String, ByVal MemType As Object) As Boolean
        Return Write(Pointer(_Address), _Hack, _Default, MemType)
    End Function

    Public Function Write(ByVal _Address As Integer, ByVal _Hack As String, ByVal _Default As String, ByVal MemType As Object) As Boolean
        Dim B As Boolean = Integrity(_Address, _Default, MemType)
        Dim Val As String = IIf(B, _Hack, _Default)
        WriteMem(_Address, Val, MemType)
        Return B
    End Function
    Public Sub WriteU(ByVal _Address As IntPtr, ByVal pBytes As UInteger)
        wBytes(_Address, BitConverter.GetBytes(pBytes))
    End Sub

    Public Sub writeF(ByVal _Address As Integer, ByVal pBytes As Single)
        wBytes(_Address, BitConverter.GetBytes(pBytes))
    End Sub

    Public Sub Write(ByVal _Address As IntPtr, ByVal pBytes As Integer)
        wBytes(_Address, BitConverter.GetBytes(pBytes))
    End Sub

    Private Sub WriteMem(ByVal _Address As Integer, ByVal _Hack As String, ByVal MemType As Object)
        If (_Address <> -1 And _Hack <> String.Empty) Then
            Select Case Array.IndexOf(Of Object)(New Object() {GetType(Byte), GetType(Integer), GetType(UInt32), GetType(Single), GetType(Double)}, MemType)
                Case 0
                    wBytes(_Address, BitConverter.GetBytes(Byte.Parse(_Hack)))
                    Exit Select
                Case 1
                    wBytes(_Address, BitConverter.GetBytes(Integer.Parse(_Hack)))
                    Exit Select
                Case 2
                    wBytes(_Address, BitConverter.GetBytes(UInt32.Parse(_Hack)))
                    Exit Select
                Case 3
                    wBytes(_Address, BitConverter.GetBytes(Single.Parse(_Hack)))
                    Exit Select
                Case 4
                    wBytes(_Address, BitConverter.GetBytes(Double.Parse(_Hack)))
                    Exit Select
            End Select
        End If
    End Sub
#End Region

#Region " Allocate Memory "
    Private Function Allocate() As Integer
        If ProcActive Then
            Dim iCave As IntPtr = VirtualAllocEx(Proc(0).Handle, IntPtr.Zero, &H200, &H1000, &H40)
            If (iCave <> IntPtr.Zero) Then
                CodeCave.Add(iCave)
                Return iCave.ToInt32
            End If
        End If
        Return -1
    End Function
#End Region

#Region " Deallocate Memory "
    Public Function DeAllocate(ByVal Addy As Integer, ByVal DefaultBytes As String, ByVal MA As Boolean) As Boolean
        If (ProcActive And Addy <> -1 And DefaultBytes <> String.Empty AndAlso Addy <> -1) Then
            Dim Index As Integer = -1
            For i As Integer = 0 To CodeCave.Count - 1
                If (Convert.ToBase64String(rBytes(Addy + 1, 4)) = Convert.ToBase64String(BitConverter.GetBytes((CodeCave.Item(i).ToInt32 + IIf(MA, 8, 0)) - Addy - 5))) Then
                    Index = i
                End If
            Next
            If (Index <> -1) Then
                Dim tmp As IntPtr = CodeCave.Item(Index)
                If (tmp <> IntPtr.Zero AndAlso wBytes(Addy, HX2Bts(DefaultBytes))) Then
                    VirtualFreeEx(Proc(0).Handle, tmp, 0, &H8000)
                    CodeCave.RemoveAt(Index)
                    Return True
                End If
            End If
        End If
        Return False
    End Function
#End Region

#Region " Aob Scan Function "
    Public Function AobScan(ByVal Base_Address As Integer, ByVal _Range As Integer, ByVal Signature As String) As Integer
        If (ProcActive And Base_Address <> -1 And Signature <> String.Empty) Then
            Dim New_String As String = Regex.Replace(Signature.Replace("??", "3F"), "[^a-fA-F0-9]", "")
            Dim tmp As Integer = RetOldScan(New_String)
            If tmp >= Base_Address And tmp <= (Base_Address + _Range) Then
                Return tmp
            End If
            Dim SearchFor As Byte() = New Byte((New_String.Length / 2) - 1) {}
            For i As Integer = 0 To SearchFor.Length - 1
                SearchFor(i) = Byte.Parse(New_String.Substring((i * 2), 2), NumberStyles.HexNumber)
            Next i
            Dim SearchIn As Byte() = New Byte(_Range - 1) {}
            ReadProcessMemory(Proc(0).Handle, Base_Address, SearchIn, _Range, 0)
            Dim Z As Integer = 0, iIndex As Integer = 0
            Dim iEnd As Integer = If((SearchFor.Length < &H20), SearchFor.Length, &H20)
            Dim sBytes As Integer() = New Integer(&H100 - 1) {}
            For j As Integer = 0 To iEnd - 1
                If (SearchFor(j) = &H3F) Then
                    Z = (Z Or (CInt(1) << ((iEnd - j) - 1)))
                End If
            Next j
            If (Z <> 0) Then
                For k As Integer = 0 To sBytes.Length - 1
                    sBytes(k) = Z
                Next k
            End If
            Z = 1
            Dim index As Integer = (iEnd - 1)
            Do While (index >= 0)
                sBytes(SearchFor(index)) = (sBytes(SearchFor(index)) Or Z)
                index -= 1
                Z = (Z << 1)
            Loop
            Do While (iIndex <= (SearchIn.Length - SearchFor.Length))
                Z = (SearchFor.Length - 1)
                Dim length As Integer = SearchFor.Length
                Dim m As Integer = -1
                Do While (m <> 0)
                    m = (m And sBytes(SearchIn((iIndex + Z))))
                    If (m <> 0) Then
                        If (Z = 0) Then
                            OldScan.Add((Base_Address + iIndex).ToString("X") + " " + New_String)
                            Return (Base_Address + iIndex)
                        End If
                        length = Z
                    End If
                    Z -= 1
                    m = (m << 1)
                Loop
                iIndex += length
            Loop
        End If

        Return -1
    End Function

    Public Function AobScan(ByVal Base_Address As String, ByVal _Range As Integer, ByVal Signature As String) As Integer
        Return AobScan(Pointer(Base_Address), _Range, Signature)
    End Function

#End Region

#Region " Patch/Inject "
    Public Function Patch(ByVal _Address As Integer, ByVal Patched_Bts As String, ByVal Default_Bts As String) As Boolean
        Dim B As Boolean = False
        If (ProcActive And Patched_Bts <> String.Empty And Default_Bts <> String.Empty And _Address <> -1) Then
            B = Integrity(_Address, Default_Bts)
            Dim pBytes As Byte() = IIf(B, HX2Bts(Patched_Bts), HX2Bts(Default_Bts))
            wBytes(_Address, pBytes)
        End If
        Return B
    End Function

    Public Function Patch(ByVal _Address As String, ByVal Patched_Bts As String, ByVal Default_Bts As String) As Boolean
        Return Patch(Pointer(_Address), Patched_Bts, Default_Bts)
    End Function

    Public Function Inject(ByVal _Address As Integer, ByVal Inject_Bts As String, ByVal Default_Bts As String, ByVal _Jump As Boolean) As Integer
        Default_Bts = DoubleSpace(Default_Bts, "")
        If (Default_Bts.Length / 2) < 5 Then
            MessageBox.Show("The Default bytes can't be less than 5. You'l need to use the next instruction atleast 5 bytes away")
            Return -1
        End If
        If (ProcActive And Inject_Bts <> String.Empty And _Address <> -1) Then
            Inject_Bts = DoubleSpace(Inject_Bts, "")
            If Integrity(_Address, Default_Bts) Then
                Dim Cave As Integer = Allocate()
                If (Cave <> -1) Then
                    Inject(_Address, Inject_Bts, Default_Bts, _Jump, Cave)
                    Return Cave
                End If
            ElseIf Not DeAllocate(_Address, Default_Bts, False) Then
                Dim Cave As Integer = Allocate()
                If (Cave <> -1) Then
                    Inject(_Address, Inject_Bts, Default_Bts, _Jump, Cave)
                    Return Cave
                End If
            End If
        End If
        Return -1
    End Function

    Public Function Inject(ByVal _Address As String, ByVal Inject_Bts As String, ByVal Default_Bts As String, ByVal _Jump As Boolean) As Integer
        Return Inject(Pointer(_Address), Inject_Bts, Default_Bts, _Jump)
    End Function

    Private Sub Inject(ByVal Addy As Integer, ByVal Inject_Bts As String, ByVal Default_Bts As String, ByVal _Jump As Boolean, ByVal Cave As Integer)
        If Not _Jump Then
            wBytes(Cave, HX2Bts(Inject_Bts + "C3"))
            wBytes(Addy, HX2Bts(JmpCall(Cave, Addy, (Default_Bts.Length / 2), False)))
        Else
            wBytes(Cave, HX2Bts(Inject_Bts + JumpBack(Cave + (Inject_Bts.Length / 2), Addy + (Default_Bts.Length / 2))))
            wBytes(Addy, HX2Bts(JmpCall(Cave, Addy, (Default_Bts.Length / 2), True)))
        End If
    End Sub

    Public Function Accessed(ByVal _Address As Integer, ByVal Default_Bts As String, ByVal Register As String, ByVal _Jump As Boolean) As Integer
        Default_Bts = DoubleSpace(Default_Bts, "")
        If (Default_Bts.Length / 2) < 5 Then
            MessageBox.Show("The Default bytes can't be less than 5. You'l need to use the next instruction atleast 5 bytes away")
            Return -1
        End If
        If (ProcActive And _Address <> -1 And Register.IndexOf("ecx", StringComparison.OrdinalIgnoreCase) = -1) Then
            If Integrity(_Address, Default_Bts) Then
                Dim _Len As Integer = IIf(_Jump, &H3A, &H36)
                Dim Cave As Integer = Allocate()
                If (Cave <> -1) Then
                    Dim Opcode As String = ConOp(Cave, "ecx", 0) + ConOp(Cave + 4, "ecx", 1) + ConOp(Cave + _Len, Register, 2) + "83f978740583c104eb05b900000000" + ConOp(Cave + 4, "ecx", 0) + ConOp(Cave, "ecx", 1)
                    Dim Inj As Byte() = HX2Bts(Opcode + IIf(_Jump, JumpBack(Cave + 8 + (Opcode.Length / 2), _Address + (Default_Bts.Length / 2)), "C3"))
                    wBytes(Cave + 8, Inj)
                    wBytes(_Address, HX2Bts(JmpCall(Cave + 8, _Address, (Default_Bts.Length / 2), _Jump)))
                    Return (Cave + _Len)
                End If
            Else
                DeAllocate(_Address, Default_Bts, True)
            End If
        End If
        Return -1
    End Function

    Public Function Accessed(ByVal _Address As String, ByVal Default_Bts As String, ByVal Register As String, ByVal _Jump As Boolean) As Integer
        Return Accessed(Pointer(_Address), Default_Bts, Register, _Jump)
    End Function

#End Region

#Region " Pointers/Module "

    Public Function _Module(ByVal _Address As String) As Integer
        If (ProcActive And _Address <> String.Empty) Then
            If (_Address.IndexOf("+", StringComparison.Ordinal) = -1) Then
                Return Integer.Parse(_Address, NumberStyles.HexNumber)
            End If
            Dim tmp As String() = _Address.Split(New Char() {"+"c})
            For Each M As ProcessModule In Proc(0).Modules
                If (M.ModuleName.ToLower = tmp(0).ToLower) Then
                    Return (M.BaseAddress.ToInt32 + Integer.Parse(tmp(1), NumberStyles.HexNumber))
                End If
            Next
        End If
        Return -1
    End Function

    Public Function Pointer(ByVal Address_Offsets As String) As Integer
        Address_Offsets = RemoveChar(DoubleSpace(Address_Offsets, " "))
        Dim tmp As String() = Address_Offsets.Split(New Char() {" "c})
        Dim _Addy As Integer = _Module(tmp(0))
        If (tmp.Length = 1) Then
            Return _Addy
        End If
        _Addy = BitConverter.ToInt32(rBytes(_Addy, 4), 0)
        Dim i As Integer
        For i = 1 To tmp.Length - 1
            Dim Off As Integer = Integer.Parse(tmp(i), NumberStyles.HexNumber)
            _Addy = IIf(i <> (tmp.Length - 1), BitConverter.ToInt32(rBytes(_Addy + Off, 4), 0), _Addy + Off)
        Next i
        Return _Addy
    End Function

#End Region

#Region " Misc "

    Public Sub iEnter()
        PostMessage(iHandle, &H100, &HD, IntPtr.Zero)
    End Sub

    Private Function DoubleSpace(ByVal txt As String, ByVal _With As String) As String
        Return Regex.Replace(txt, "\s+", _With)
    End Function


    Public Function Get_Addy(ByVal Start_Index As Integer, ByVal iLength As Integer, ByVal SearchFor As String) As Integer
        Dim New_String = SearchFor.Replace("??", "3F")
        Dim SearchIn As Byte() = New Byte(iLength - 1) {}, iSearch As Byte() = New Byte((New_String.Length / 2) - 1) {}
        Dim Z As Integer = 0, Y As Integer = 0
        Dim iEnd As Integer = IIf((iSearch.Length < 32), iSearch.Length, 32)
        Dim sBytes As Integer() = New Integer(256 - 1) {}
        Dim i As Integer
        For i = 0 To iSearch.Length - 1
            iSearch(i) = Byte.Parse(New_String.Substring((i * 2), 2), System.Globalization.NumberStyles.HexNumber)
        Next i
        ReadProcessMemory(Proc(0).Handle, Start_Index, SearchIn, iLength, 0)
        Dim j As Integer
        For j = 0 To iEnd - 1
            If (iSearch(j) = &H3F) Then
                Z = (Z Or (CInt(1) << ((iEnd - j) - 1)))
            End If
        Next j
        If (Z <> 0) Then
            Dim k As Integer
            For k = 0 To sBytes.Length - 1
                sBytes(k) = Z
            Next k
        End If
        Z = 1
        Dim index As Integer = (iEnd - 1)
        Do While (index >= 0)
            sBytes(iSearch(index)) = (sBytes(iSearch(index)) Or Z)
            index -= 1
            Z = (Z << 1)
        Loop
        Do While (Y <= (SearchIn.Length - iSearch.Length))
            Z = (iSearch.Length - 1)
            Dim length As Integer = iSearch.Length
            Dim m As Integer = -1
            Do While (m <> 0)
                m = (m And sBytes(SearchIn(Y + Z)))
                If (m <> 0) Then
                    If (Z = 0) Then
                        Return (Start_Index + Y)
                    End If
                    length = Z
                End If
                Z -= 1
                m = (m << 1)
            Loop
            Y = (Y + length)
        Loop
        Return -1
    End Function

    Public Function Int2Byt(ByVal val As Integer) As String
        Dim Hex As String = String.Empty
        Dim bHex As Byte() = BitConverter.GetBytes(val)
        Dim j As Integer
        For j = 0 To bHex.Length - 1
            Hex += String.Format("{0:x2}", Convert.ToByte(bHex(j)))
        Next j
        Return Hex
    End Function

    Private Function IsHex(ByVal Val As String) As Boolean
        Return Regex.IsMatch(Val, "\A\b[0-9a-fA-F]+\b\Z")
    End Function

    Private Function CutString(ByVal St As String) As String
        Dim i As Integer = St.IndexOf("  ", StringComparison.Ordinal)
        Return If(i = -1, St, St.Substring(0, i))
    End Function

    Private Function Integrity(ByVal _Addy As Integer, ByVal _B As String) As Boolean
        If ((ProcActive And (_Addy <> -1)) And (_B <> String.Empty)) Then
            _B = Regex.Replace(_B, "[^a-fA-F0-9]", "")
            If ((_B.Length Mod 2) <> 0) Then
                _B = _B.Substring(0, (_B.Length - 1))
            End If
            Dim B As Byte() = Me.rBytes(_Addy, (_B.Length / 2))
            For i As Integer = 0 To B.Length - 1
                If (B(i) <> Byte.Parse(_B.Substring((i * 2), 2), NumberStyles.HexNumber)) Then
                    Return False
                End If
            Next
            Return True
        End If
        Return False
    End Function

    Private Function Integrity(ByVal _Addy As Integer, ByVal _Default As String, ByVal MemType As Object) As Boolean
        Select Case Array.IndexOf(Of Object)(New Object() {GetType(Byte), GetType(Integer), GetType(UInt32), GetType(Single), GetType(Double)}, MemType)
            Case 0
                Return (rBytes(_Addy, 1)(0) = Byte.Parse(_Default))
            Case 1
                Return (BitConverter.ToInt32(rBytes(_Addy, 4), 0) = Integer.Parse(_Default))
            Case 2
                Return (BitConverter.ToUInt32(rBytes(_Addy, 4), 0) = UInt32.Parse(_Default))
            Case 3
                Return (BitConverter.ToSingle(rBytes(_Addy, 4), 0) = Single.Parse(_Default))
            Case 4
                Return (BitConverter.ToDouble(rBytes(_Addy, 8), 0) = Double.Parse(_Default))
        End Select
        Return False
    End Function

    Public Function HX2Bts(ByVal HXS As String) As Byte()
        HXS = Regex.Replace(HXS, "[^a-fA-F0-9?]", "")
        HXS = HXS.Replace("??", "3F")
        If ((HXS.Length Mod 2) <> 0) Then
            HXS = HXS.Substring(0, (HXS.Length - 1))
        End If
        Dim buf As Byte() = New Byte((HXS.Length / 2) - 1) {}
        For i As Integer = 0 To buf.Length - 1
            buf(i) = Byte.Parse(HXS.Substring((i * 2), 2), NumberStyles.HexNumber)
        Next i
        Return buf
    End Function

    Private Function JmpCall(ByVal Cave As IntPtr, ByVal JumpFrom As Integer, ByVal iLen As Integer, ByVal _Jump As Boolean) As String
        Dim Ins As String = GetIns(BitConverter.GetBytes(Cave.ToInt32 - JumpFrom - 5))
        For i As Integer = 5 To iLen - 1
            Ins += "90"
        Next i
        Return (IIf(_Jump, "E9", "E8") & Ins)
    End Function

    Private Function GetIns(ByVal BTS As Byte()) As String
        Dim tmp As String = String.Empty
        For i As Integer = 0 To BTS.Length - 1
            tmp += String.Format("{0:x2}", Convert.ToUInt32(BTS(i)))
        Next i
        Return tmp
    End Function

    Private Function JumpBack(ByVal Cave As Integer, ByVal NextInstruc As Integer) As String
        Return "E9" & GetIns(BitConverter.GetBytes(NextInstruc - Cave - 5))
    End Function

    Private Function ConOp(ByVal Cave_Offset As Integer, ByVal Reg As String, ByVal _Index As Byte) As String
        If (_Index <= 2) Then
            Dim _S As String() = Nothing
            Dim _To As String() = New String() {"eax 8905", "ecx 890d", "edx 8915", "edi 893d", "ebx 891d", "esi 8935", "ebp 892d", "esp 8925"}
            Dim _From As String() = New String() {"eax a1", "ecx 8b0d", "edx 8b15", "edi 8b3d", "ebx 8b1d", "esi 8b35", "ebp 8b2d", "esp 8b25"}
            Dim _Store As String() = New String() {"eax 8981", "edx 8991", "edi 89b9", "ebx 8999", "esi 89b1", "ebp 89a9", "esp 89a1"}
            _S = IIf(_Index = 0, _To, IIf(_Index = 1, _From, _Store))
            For i As Integer = 0 To _S.Length - 1
                If (_S(i).IndexOf(Reg, StringComparison.Ordinal) <> -1) Then
                    Return (_S(i).Split(New Char() {" "c})(1) & GetIns(BitConverter.GetBytes(Cave_Offset)))
                End If
            Next
        End If
        Return String.Empty
    End Function

    Public Function UpdateTable(ByVal Cave As Integer, ByVal Offset As Integer) As List(Of Integer)
        Dim adr As Integer() = New Integer(29) {}
        For i As Integer = 0 To adr.Length - 1
            adr(i) = BitConverter.ToInt32(Me.rBytes((Cave + (i * 4)), 4), 0)
        Next

        Dim unique As New List(Of Integer)(30)
        For i As Integer = 0 To adr.Length - 1
            If Not unique.Contains(adr(i)) OrElse adr(i) <> 0 Then
                unique.Add(adr(i))
            End If
        Next

        Dim updated As New List(Of Integer)(30)
        For i As Integer = 0 To unique.Count - 1
            If Not updated.Contains(unique.Item(i) + Offset) Then
                updated.Add(unique.Item(i) + Offset)
            End If
        Next
        Return updated
    End Function

    Private Function RetOldScan(ByVal Sig As String) As Integer
        For i As Integer = 0 To OldScan.Count - 1
            Dim S As String() = OldScan.Item(i).Split(New Char() {" "c})
            If (S(1) = Sig) Then
                Return Integer.Parse(S(0), NumberStyles.HexNumber)
            End If
        Next
        Return -1
    End Function

    Private Function RemoveChar(ByVal txt As String) As String
        If txt.StartsWith("&h", StringComparison.OrdinalIgnoreCase) Or txt.StartsWith("0x", StringComparison.OrdinalIgnoreCase) Then
            Return txt.Substring(2, txt.Length - 2)
        End If
        Return txt
    End Function

    Public Function Convert_Opcode(ByVal B As Byte()) As IntPtr
        Dim Temp As String = String.Empty
        Dim j As Integer = (B.Length - 1)
        Do While (j >= 0)
            Temp += String.Format("{0:x2}", Convert.ToUInt32(B(j)))
            j -= 1
        Loop
        Return New IntPtr(Integer.Parse(Temp, NumberStyles.AllowHexSpecifier))
    End Function

#End Region

#Region " Game Console "
    Public Function Keystate(ByVal key As Keys) As Boolean
        Dim St As Integer = GetKeyState(key)
        Return St = -127 OrElse St = -128
    End Function
#End Region

End Class
