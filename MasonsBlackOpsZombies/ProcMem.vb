Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Text

Public Class ProcMem

#Region "API"
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
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function PostMessage(ByVal hhwnd As IntPtr, ByVal msg As UInt32, ByVal wparam As IntPtr, ByVal lparam As IntPtr) As Boolean
    End Function

#End Region ' API

#Region "Get Process"

    ''' <summary>
    ''' Raised when the target process is first detected as running.
    ''' </summary>
    Public Event ProcessStarted As EventHandler

    ''' <summary>
    ''' Raised when the target process is no longer detected as running.
    ''' </summary>
    Public Event ProcessStopped As EventHandler


    ''' <summary>
    ''' Starts monitoring the specified process by name. If the process is found, a timer is set up to continuously check its status.
    ''' </summary>
    ''' <param name="ProcessName">The name of the process to monitor (without ".exe").</param>
    Public Sub GetProcess(ByVal ProcessName As String)
        If ProcessName <> String.Empty Then
            PName = ProcessName
            MakeTimer(New EventHandler(AddressOf ProcTimer), 100)
        End If
    End Sub


    ''' <summary>
    ''' Timer callback that checks the current status of the target process.
    ''' Fires <see cref="ProcessStarted"/> or <see cref="ProcessStopped"/> events if the process state changes.
    ''' </summary>
    ''' <param name="sender">The source of the timer event.</param>
    ''' <param name="e">The event arguments.</param>
    Private Sub ProcTimer(ByVal sender As Object, ByVal e As EventArgs)
        If (Not ProcessIsActive) Then
            Proc = Process.GetProcessesByName(PName)
            ProcessIsActive = Proc.Length <> 0
        End If
        Try
            If Proc.Length > 0 Then
                ProcessIsActive = Not Proc(0).HasExited
                iHandle = Proc(0).MainWindowHandle
            Else
                ProcessIsActive = False
                iHandle = IntPtr.Zero
            End If
        Catch ex As Exception
            iHandle = IntPtr.Zero
            ProcessIsActive = False
        End Try

        If Not WasProcActive AndAlso ProcessIsActive Then
            RaiseEvent ProcessStarted(Me, EventArgs.Empty)
        End If

        If WasProcActive AndAlso Not ProcessIsActive Then
            RaiseEvent ProcessStopped(Me, EventArgs.Empty)
        End If

        WasProcActive = ProcessIsActive
    End Sub


    ''' <summary>
    ''' Creates and starts a timer that executes the given event handler at a fixed interval.
    ''' </summary>
    ''' <param name="iTimed">The event handler to invoke on each timer tick.</param>
    ''' <param name="iIntervals">The interval in milliseconds between ticks.</param>
    Private Sub MakeTimer(ByVal iTimed As EventHandler, ByVal iIntervals As Integer)
        Dim timer As New Timer
        timer.Interval = iIntervals
        timer.Start()
        AddHandler timer.Tick, New EventHandler(AddressOf iTimed.Invoke)
    End Sub


#End Region ' Get Process

#Region "Storage"

    ' Stores the result of GetProcessesByName; holds the BlackOps process instance(s)
    Private Proc As Process()

    ' Handle to the main window of the target process (used for key simulation and interaction)
    Private iHandle As IntPtr

    ' Indicates whether memory interaction (like reading/writing) is currently active (possibly deprecated)
    Public iActive As Boolean

    ' The name of the process to monitor (e.g., "BlackOps"); set in GetProcess()
    Protected PName As String

    ' True if the target process is currently running and accessible
    Public ProcessIsActive As Boolean

    ' Tracks the last known process state to detect changes (used to raise ProcessStarted/Stopped events)
    Private WasProcActive As Boolean

    ' Stores allocated memory regions (code caves) used for code injection or patching
    Private CodeCave As New List(Of IntPtr)

    ' Stores previous scan results or pattern addresses from AoB scanning to avoid redundant scans
    Private OldScan As New List(Of String)

#End Region ' Storage

#Region "Read Memory"

    ''' <summary>
    ''' Reads a sequence of bytes from the target process at a given memory address.
    ''' </summary>
    ''' <param name="_A">The memory address to read from.</param>
    ''' <param name="_S">The number of bytes to read.</param>
    ''' <returns>A byte array containing the data read from memory.</returns>
    Public Function ReadBytes(ByVal _A As Integer, ByVal _S As Integer) As Byte()
        Dim buff As Byte() = New Byte(_S - 1) {}
        If ProcessIsActive AndAlso Proc.Length > 0 Then
            ReadProcessMemory(Proc(0).Handle, _A, buff, _S, 0)
        End If
        Return buff
    End Function

    ''' <summary>
    ''' Reads a value of a specified type from memory at the given address.
    ''' </summary>
    ''' <param name="_Address">The memory address to read from.</param>
    ''' <param name="MemType">The data type to interpret the bytes as (Byte, Integer, UInt32, Single, Double).</param>
    ''' <returns>
    ''' The value read from memory, interpreted as the specified type;
    ''' returns -1 if the address is invalid or type is unsupported.
    ''' </returns>
    Public Function ReadRaw(ByVal _Address As Integer, ByVal MemType As Object) As Object
        If (_Address <> -1) Then
            Select Case Array.IndexOf(Of Object)(New Object() {GetType(Byte), GetType(Integer), GetType(UInt32), GetType(Single), GetType(Double)}, MemType)
                Case 0
                    Return ReadBytes(_Address, 1)(0)
                Case 1
                    Return BitConverter.ToInt32(ReadBytes(_Address, 4), 0)
                Case 2
                    Return BitConverter.ToUInt32(ReadBytes(_Address, 4), 0)
                Case 3
                    Return BitConverter.ToSingle(ReadBytes(_Address, 4), 0)
                Case 4
                    Return BitConverter.ToDouble(ReadBytes(_Address, 8), 0)
                Case -1
                    Return -1 ' Unsupported type
            End Select
        End If
        Return -1
    End Function

    ''' <summary>
    ''' Reads a value of a specified type from memory at the address resolved from a string-based pointer.
    ''' </summary>
    ''' <param name="_Address">A string representing the named pointer address.</param>
    ''' <param name="MemType">The data type to interpret the bytes as.</param>
    ''' <returns>The value read from the resolved pointer address.</returns>
    Public Function ReadPointer(ByVal _Address As String, ByVal MemType As Object) As Object
        Return ReadRaw(Pointer(_Address), MemType)
    End Function

    ''' <summary>
    ''' Reads a string from memory at the given address.
    ''' </summary>
    ''' <param name="_Address">The memory address to read from.</param>
    ''' <param name="_Length">The number of bytes to read.</param>
    ''' <returns>The ASCII string read from memory, trimmed of null characters.</returns>
    Public Function ReadString(ByVal _Address As Integer, ByVal _Length As Integer) As String
        If (_Address <> -1) Then
            ' ASCII string extraction and cleanup
            Return CutString(Encoding.ASCII.GetString(ReadBytes(_Address, _Length)))
        End If
        Return String.Empty
    End Function

    ''' <summary>
    ''' Reads a 32-bit float (Single) from memory at the specified address.
    ''' </summary>
    ''' <param name="_Address">The memory address to read from.</param>
    ''' <returns>The Single (float) value read from memory.</returns>
    Public Function ReadFloat(ByVal _Address As Integer) As Single
        Return BitConverter.ToSingle(ReadBytes(_Address, 4), 0)
    End Function

    ''' <summary>
    ''' Reads a 32-bit signed integer from memory at the specified address.
    ''' </summary>
    ''' <param name="_Address">The memory address to read from.</param>
    ''' <returns>The Integer value read from memory.</returns>
    Public Function ReadInteger(ByVal _Address As Integer) As Integer
        Return BitConverter.ToInt32(ReadBytes(_Address, 4), 0)
    End Function


#End Region ' Read Memory

#Region "Write Memory"

    ''' <summary>
    ''' Writes a byte array to the specified memory address in the target process.
    ''' </summary>
    ''' <param name="_A">The memory address to write to.</param>
    ''' <param name="_B">The byte array to write.</param>
    ''' <returns>True if the write succeeded, otherwise false.</returns>
    Private Function WriteBytes(ByVal _A As Integer, ByVal _B As Byte()) As Boolean
        If ProcessIsActive Then
            Return WriteProcessMemory(Proc(0).Handle, _A, _B, _B.Length, 0)
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Writes a null-terminated ASCII string to the specified memory address.
    ''' </summary>
    ''' <param name="_Address">The memory address to write to.</param>
    ''' <param name="pBytes">The string to write.</param>
    Public Sub WriteString(ByVal _Address As IntPtr, ByVal pBytes As String)
        WriteBytes(_Address, System.Text.Encoding.ASCII.GetBytes((pBytes & ChrW(0))))
    End Sub

    ''' <summary>
    ''' Writes a value of a specified type to a memory address.
    ''' </summary>
    ''' <param name="_Address">The target memory address.</param>
    ''' <param name="Value">The value to write as string.</param>
    ''' <param name="MemType">The memory type (Byte, Integer, etc.).</param>
    Public Sub Write(ByVal _Address As Integer, ByVal Value As String, ByVal MemType As Object)
        WriteMem(_Address, Value, MemType)
    End Sub

    ''' <summary>
    ''' Resolves a named pointer and writes a value to that memory location.
    ''' </summary>
    ''' <param name="_Address">The symbolic pointer name.</param>
    ''' <param name="Value">The value to write as an integer.</param>
    ''' <param name="MemType">The memory type to write.</param>
    Public Sub Write(ByVal _Address As String, ByVal Value As Integer, ByVal MemType As Object)
        WriteMem(Pointer(_Address), Value, MemType)
    End Sub

    ''' <summary>
    ''' Resolves a named pointer and writes a value of an arbitrary type.
    ''' </summary>
    ''' <param name="_Address">The symbolic pointer name.</param>
    ''' <param name="Value">The value to write.</param>
    ''' <param name="MemType">The memory type to write.</param>
    Public Sub Write(ByVal _Address As String, ByVal Value As Object, ByVal MemType As Object)
        WriteMem(Pointer(_Address), Value, MemType)
    End Sub

    ''' <summary>
    ''' Writes either a hack or default value based on the memory's current integrity state.
    ''' </summary>
    ''' <param name="_Address">The symbolic pointer name.</param>
    ''' <param name="_Hack">The value to write if integrity matches.</param>
    ''' <param name="_Default">The fallback value to write if integrity check fails.</param>
    ''' <param name="MemType">The type of value to write.</param>
    ''' <returns>True if the integrity matched and hack was written, false otherwise.</returns>
    Public Function Write(ByVal _Address As String, ByVal _Hack As String, ByVal _Default As String, ByVal MemType As Object) As Boolean
        Return Write(Pointer(_Address), _Hack, _Default, MemType)
    End Function

    ''' <summary>
    ''' Writes either a hack or default value based on an integrity check for the specified address.
    ''' </summary>
    ''' <param name="_Address">The target memory address.</param>
    ''' <param name="_Hack">The value to write if integrity check passes.</param>
    ''' <param name="_Default">The fallback value to write if integrity fails.</param>
    ''' <param name="MemType">The type of memory to write.</param>
    ''' <returns>True if hack value was written, false if fallback was used.</returns>
    Public Function Write(ByVal _Address As Integer, ByVal _Hack As String, ByVal _Default As String, ByVal MemType As Object) As Boolean
        Dim B As Boolean = Integrity(_Address, _Default, MemType)
        Dim Val As String = IIf(B, _Hack, _Default)
        WriteMem(_Address, Val, MemType)
        Return B
    End Function

    ''' <summary>
    ''' Writes an unsigned 32-bit integer to memory.
    ''' </summary>
    ''' <param name="_Address">The memory address.</param>
    ''' <param name="uintValue">The unsigned integer value.</param>
    Public Sub WriteU(ByVal _Address As IntPtr, ByVal uintValue As UInteger)
        WriteBytes(_Address, BitConverter.GetBytes(uintValue))
    End Sub

    ''' <summary>
    ''' Writes a single-precision float value to memory.
    ''' </summary>
    ''' <param name="_Address">The memory address.</param>
    ''' <param name="floatValue">The float value.</param>
    Public Sub WriteF(ByVal _Address As Integer, ByVal floatValue As Single)
        WriteBytes(_Address, BitConverter.GetBytes(floatValue))
    End Sub

    ''' <summary>
    ''' Writes a 32-bit integer to memory.
    ''' </summary>
    ''' <param name="_Address">The memory address.</param>
    ''' <param name="intValue">The integer value.</param>
    Public Sub Write(ByVal _Address As IntPtr, ByVal intValue As Integer)
        WriteBytes(_Address, BitConverter.GetBytes(intValue))
    End Sub

    ''' <summary>
    ''' Internal memory writer that converts a string value to the correct type and writes it to memory.
    ''' </summary>
    ''' <param name="_Address">Target memory address.</param>
    ''' <param name="_Hack">The value to write as a string.</param>
    ''' <param name="MemType">The memory type to parse and write.</param>
    Private Sub WriteMem(ByVal _Address As Integer, ByVal _Hack As String, ByVal MemType As Object)
        If (_Address <> -1 And _Hack <> String.Empty) Then
            Select Case Array.IndexOf(Of Object)(New Object() {GetType(Byte), GetType(Integer), GetType(UInt32), GetType(Single), GetType(Double)}, MemType)
                Case 0
                    WriteBytes(_Address, BitConverter.GetBytes(Byte.Parse(_Hack)))
                Case 1
                    WriteBytes(_Address, BitConverter.GetBytes(Integer.Parse(_Hack)))
                Case 2
                    WriteBytes(_Address, BitConverter.GetBytes(UInt32.Parse(_Hack)))
                Case 3
                    WriteBytes(_Address, BitConverter.GetBytes(Single.Parse(_Hack)))
                Case 4
                    WriteBytes(_Address, BitConverter.GetBytes(Double.Parse(_Hack)))
            End Select
        End If
    End Sub

#End Region ' Write Memory

#Region "Allocate Memory"

    ''' <summary>
    ''' Allocates executable memory (code cave) in the target process for injection or patching.
    ''' </summary>
    ''' <returns>
    ''' The address of the allocated memory as an integer, or -1 if allocation failed.
    ''' </returns>
    Private Function Allocate() As Integer
        If ProcessIsActive Then
            Dim iCave As IntPtr = VirtualAllocEx(Proc(0).Handle, IntPtr.Zero, &H200, &H1000, &H40)
            If (iCave <> IntPtr.Zero) Then
                CodeCave.Add(iCave)
                Return iCave.ToInt32
            End If
        End If
        Return -1
    End Function

#End Region ' Allocate Memory

#Region "Deallocate Memory"

    ''' <summary>
    ''' Deallocates a previously allocated code cave and restores the original bytes at the given address.
    ''' </summary>
    ''' <param name="Addy">The address where the patch was applied.</param>
    ''' <param name="DefaultBytes">The original bytes (as a hex string) to restore.</param>
    ''' <param name="MA">Whether the memory address requires an offset adjustment (usually true for JMP +8 instructions).</param>
    ''' <returns>True if deallocation and memory restoration succeeded, otherwise false.</returns>
    Public Function DeAllocate(ByVal Addy As Integer, ByVal DefaultBytes As String, ByVal MA As Boolean) As Boolean
        If (ProcessIsActive AndAlso Addy <> -1 AndAlso DefaultBytes <> String.Empty) Then
            Dim Index As Integer = -1

            ' Search for matching CodeCave by comparing calculated jump offset bytes
            For i As Integer = 0 To CodeCave.Count - 1
                If (Convert.ToBase64String(ReadBytes(Addy + 1, 4)) =
                Convert.ToBase64String(BitConverter.GetBytes((CodeCave.Item(i).ToInt32 + IIf(MA, 8, 0)) - Addy - 5))) Then
                    Index = i
                End If
            Next

            ' If found, restore original bytes and free memory
            If (Index <> -1) Then
                Dim tmp As IntPtr = CodeCave.Item(Index)
                If (tmp <> IntPtr.Zero AndAlso WriteBytes(Addy, HX2Bts(DefaultBytes))) Then
                    VirtualFreeEx(Proc(0).Handle, tmp, 0, &H8000)
                    CodeCave.RemoveAt(Index)
                    Return True
                End If
            End If
        End If
        Return False
    End Function

#End Region ' Deallocate Memory

#Region "Aob Scan Function"

    ''' <summary>
    ''' Scans the specified memory range for an Array-of-Bytes (AOB) pattern.
    ''' </summary>
    ''' <param name="Base_Address">The starting address for the scan.</param>
    ''' <param name="_Range">The size (in bytes) of the memory region to scan.</param>
    ''' <param name="Signature">The AOB pattern as a string (e.g. "8B ?? ?? 89 45").</param>
    ''' <returns>The address where the pattern was found, or -1 if not found.</returns>
    Public Function AobScan(ByVal Base_Address As Integer, ByVal _Range As Integer, ByVal Signature As String) As Integer
        If (ProcessIsActive AndAlso Base_Address <> -1 AndAlso Signature <> String.Empty) Then
            ' Normalize signature, replacing wildcards with "3F" (?)
            Dim New_String As String = Regex.Replace(Signature.Replace("??", "3F"), "[^a-fA-F0-9]", "")

            ' Check previous results to avoid rescanning
            Dim tmp As Integer = RetOldScan(New_String)
            If tmp >= Base_Address And tmp <= (Base_Address + _Range) Then
                Return tmp
            End If

            ' Convert hex string to byte array
            Dim SearchFor As Byte() = New Byte((New_String.Length \ 2) - 1) {}
            For i As Integer = 0 To SearchFor.Length - 1
                SearchFor(i) = Byte.Parse(New_String.Substring(i * 2, 2), NumberStyles.HexNumber)
            Next

            ' Read memory from the target process
            Dim SearchIn As Byte() = New Byte(_Range - 1) {}
            ReadProcessMemory(Proc(0).Handle, Base_Address, SearchIn, _Range, 0)

            ' Setup for Boyer-Moore-Horspool variant search
            Dim iEnd As Integer = If((SearchFor.Length < &H20), SearchFor.Length, &H20)
            Dim sBytes As Integer() = New Integer(&H100 - 1) {}
            Dim Z As Integer = 0
            For j As Integer = 0 To iEnd - 1
                If SearchFor(j) = &H3F Then ' Wildcard
                    Z = (Z Or (1 << ((iEnd - j) - 1)))
                End If
            Next
            If Z <> 0 Then
                For k As Integer = 0 To sBytes.Length - 1
                    sBytes(k) = Z
                Next
            End If

            Z = 1
            Dim index As Integer = (iEnd - 1)
            Do While (index >= 0)
                sBytes(SearchFor(index)) = (sBytes(SearchFor(index)) Or Z)
                index -= 1
                Z <<= 1
            Loop

            ' Perform pattern scan
            Dim iIndex As Integer = 0
            Do While iIndex <= (SearchIn.Length - SearchFor.Length)
                Z = SearchFor.Length - 1
                Dim length As Integer = SearchFor.Length
                Dim m As Integer = -1
                Do While m <> 0
                    m = (m And sBytes(SearchIn(iIndex + Z)))
                    If m <> 0 Then
                        If Z = 0 Then
                            OldScan.Add((Base_Address + iIndex).ToString("X") & " " & New_String)
                            Return Base_Address + iIndex
                        End If
                        length = Z
                    End If
                    Z -= 1
                    m <<= 1
                Loop
                iIndex += length
            Loop
        End If

        Return -1
    End Function

    ''' <summary>
    ''' Resolves a symbolic pointer address and performs an AOB scan.
    ''' </summary>
    ''' <param name="Base_Address">The symbolic base address (e.g. "Player_Health").</param>
    ''' <param name="_Range">The number of bytes to scan from the resolved address.</param>
    ''' <param name="Signature">The AOB pattern to search for.</param>
    ''' <returns>The matching address or -1 if not found.</returns>
    Public Function AobScan(ByVal Base_Address As String, ByVal _Range As Integer, ByVal Signature As String) As Integer
        Return AobScan(Pointer(Base_Address), _Range, Signature)
    End Function

#End Region ' Aob Scan Function

#Region "Patch/Inject"

    ''' <summary>
    ''' Applies a memory patch to the specified address using a byte pattern. If the original bytes match expected defaults,
    ''' the patched bytes are applied; otherwise, the default bytes are restored.
    ''' </summary>
    ''' <param name="_Address">The memory address to patch.</param>
    ''' <param name="Patched_Bts">The hex string representing the patch bytes to apply.</param>
    ''' <param name="Default_Bts">The original hex byte pattern expected at the address.</param>
    ''' <returns>True if the patch was applied, False if defaults were restored.</returns>
    Public Function Patch(ByVal _Address As Integer, ByVal Patched_Bts As String, ByVal Default_Bts As String) As Boolean
        Dim B As Boolean = False
        If (ProcessIsActive And Patched_Bts <> String.Empty And Default_Bts <> String.Empty And _Address <> -1) Then
            B = Integrity(_Address, Default_Bts)
            Dim pBytes As Byte() = IIf(B, HX2Bts(Patched_Bts), HX2Bts(Default_Bts))
            WriteBytes(_Address, pBytes)
        End If
        Return B
    End Function

    ''' <summary>
    ''' Resolves an address by name and performs a memory patch.
    ''' </summary>
    ''' <param name="_Address">The symbolic name of the address (resolved via Pointer method).</param>
    ''' <param name="Patched_Bts">The patch bytes as a hex string.</param>
    ''' <param name="Default_Bts">The default bytes as a hex string.</param>
    ''' <returns>True if patched, false otherwise.</returns>
    Public Function Patch(ByVal _Address As String, ByVal Patched_Bts As String, ByVal Default_Bts As String) As Boolean
        Return Patch(Pointer(_Address), Patched_Bts, Default_Bts)
    End Function

    ''' <summary>
    ''' Injects custom assembly code into a newly allocated code cave, optionally writing a jump from the original location.
    ''' </summary>
    ''' <param name="_Address">Address to overwrite with jump instruction to the code cave.</param>
    ''' <param name="Inject_Bts">The injection code in hex string format.</param>
    ''' <param name="Default_Bts">The original instruction bytes to validate and restore if needed.</param>
    ''' <param name="_Jump">If true, performs a jump to return to the original execution path after injection.</param>
    ''' <returns>The address of the allocated cave if injection succeeds; -1 otherwise.</returns>
    Public Function Inject(ByVal _Address As Integer, ByVal Inject_Bts As String, ByVal Default_Bts As String, ByVal _Jump As Boolean) As Integer
        Default_Bts = DoubleSpace(Default_Bts, "")
        If (Default_Bts.Length / 2) < 5 Then
            MessageBox.Show("The Default bytes can't be less than 5. You'll need to use the next instruction at least 5 bytes away")
            Return -1
        End If

        If (ProcessIsActive And Inject_Bts <> String.Empty And _Address <> -1) Then
            Inject_Bts = DoubleSpace(Inject_Bts, "")
            If Integrity(_Address, Default_Bts) Then
                Dim Cave As Integer = Allocate()
                If Cave <> -1 Then
                    Inject(_Address, Inject_Bts, Default_Bts, _Jump, Cave)
                    Return Cave
                End If
            ElseIf Not DeAllocate(_Address, Default_Bts, False) Then
                Dim Cave As Integer = Allocate()
                If Cave <> -1 Then
                    Inject(_Address, Inject_Bts, Default_Bts, _Jump, Cave)
                    Return Cave
                End If
            End If
        End If
        Return -1
    End Function

    ''' <summary>
    ''' Resolves the symbolic address and performs an injection.
    ''' </summary>
    ''' <param name="_Address">The symbolic address name.</param>
    ''' <param name="Inject_Bts">Hex byte string for injected code.</param>
    ''' <param name="Default_Bts">Hex byte string for original instructions.</param>
    ''' <param name="_Jump">Whether to jump back to original code after injection.</param>
    ''' <returns>Injected cave address or -1 if failed.</returns>
    Public Function Inject(ByVal _Address As String, ByVal Inject_Bts As String, ByVal Default_Bts As String, ByVal _Jump As Boolean) As Integer
        Return Inject(Pointer(_Address), Inject_Bts, Default_Bts, _Jump)
    End Function

    ''' <summary>
    ''' Internal helper that writes injection code to a cave and redirects execution from the target address.
    ''' </summary>
    ''' <param name="Addy">Address to patch with jump/call.</param>
    ''' <param name="Inject_Bts">Instruction bytes to inject (no trailing jump).</param>
    ''' <param name="Default_Bts">Original bytes to restore if injection is removed.</param>
    ''' <param name="_Jump">True to add jump back to original flow; false to just return (C3).</param>
    ''' <param name="Cave">Address of the allocated memory cave.</param>
    Private Sub Inject(ByVal Addy As Integer, ByVal Inject_Bts As String, ByVal Default_Bts As String, ByVal _Jump As Boolean, ByVal Cave As Integer)
        If Not _Jump Then
            ' Inject code followed by RET
            WriteBytes(Cave, HX2Bts(Inject_Bts & "C3"))
            WriteBytes(Addy, HX2Bts(JmpCall(Cave, Addy, Default_Bts.Length / 2, False)))
        Else
            ' Inject code followed by JMP back to original flow
            WriteBytes(Cave, HX2Bts(Inject_Bts & JumpBack(Cave + (Inject_Bts.Length / 2), Addy + (Default_Bts.Length / 2))))
            WriteBytes(Addy, HX2Bts(JmpCall(Cave, Addy, Default_Bts.Length / 2, True)))
        End If
    End Sub

    ''' <summary>
    ''' Injects a conditional memory access trigger using register redirection to detect when memory is accessed.
    ''' </summary>
    ''' <param name="_Address">Address to monitor for access.</param>
    ''' <param name="Default_Bts">Expected bytes at address.</param>
    ''' <param name="Register">Register to use for redirection (e.g., "eax").</param>
    ''' <param name="_Jump">True to jump back to original code after hook logic.</param>
    ''' <returns>The address of the created cave or -1 on failure.</returns>
    Public Function Accessed(ByVal _Address As Integer, ByVal Default_Bts As String, ByVal Register As String, ByVal _Jump As Boolean) As Integer
        Default_Bts = DoubleSpace(Default_Bts, "")
        If (Default_Bts.Length / 2) < 5 Then
            MessageBox.Show("The Default bytes can't be less than 5. You'll need to use the next instruction at least 5 bytes away")
            Return -1
        End If

        If (ProcessIsActive And _Address <> -1 And Register.IndexOf("ecx", StringComparison.OrdinalIgnoreCase) = -1) Then
            If Integrity(_Address, Default_Bts) Then
                Dim _Len As Integer = If(_Jump, &H3A, &H36)
                Dim Cave As Integer = Allocate()
                If Cave <> -1 Then
                    ' Build opcode manually to hook access and redirect logic
                    Dim Opcode As String =
                    ConOp(Cave, "ecx", 0) &
                    ConOp(Cave + 4, "ecx", 1) &
                    ConOp(Cave + _Len, Register, 2) &
                    "83f978740583c104eb05b900000000" &
                    ConOp(Cave + 4, "ecx", 0) &
                    ConOp(Cave, "ecx", 1)

                    Dim Inj As Byte() = HX2Bts(Opcode & If(_Jump, JumpBack(Cave + 8 + (Opcode.Length / 2), _Address + (Default_Bts.Length / 2)), "C3"))
                    WriteBytes(Cave + 8, Inj)
                    WriteBytes(_Address, HX2Bts(JmpCall(Cave + 8, _Address, (Default_Bts.Length / 2), _Jump)))
                    Return Cave + _Len
                End If
            Else
                DeAllocate(_Address, Default_Bts, True)
            End If
        End If
        Return -1
    End Function

    ''' <summary>
    ''' Resolves a symbolic address and injects a memory access hook.
    ''' </summary>
    ''' <param name="_Address">Symbolic memory address name.</param>
    ''' <param name="Default_Bts">Original instruction bytes as hex string.</param>
    ''' <param name="Register">The CPU register used for condition checks.</param>
    ''' <param name="_Jump">Whether to jump back to original execution.</param>
    ''' <returns>Address of injected cave or -1 on failure.</returns>
    Public Function Accessed(ByVal _Address As String, ByVal Default_Bts As String, ByVal Register As String, ByVal _Jump As Boolean) As Integer
        Return Accessed(Pointer(_Address), Default_Bts, Register, _Jump)
    End Function

#End Region ' Patch/Inject

#Region "Pointers/Module"

    ''' <summary>
    ''' Resolves a module-relative memory address to an absolute address.
    ''' </summary>
    ''' <param name="_Address">
    ''' A string representing either a hex address (e.g., "0x123456") or a module-relative address in the form "ModuleName+Offset" (e.g., "BlackOps.exe+1A2B3C").
    ''' </param>
    ''' <returns>
    ''' The resolved absolute address as an <c>Integer</c>, or -1 if the process is not active or the module was not found.
    ''' </returns>
    Public Function _Module(ByVal _Address As String) As Integer
        If (ProcessIsActive And _Address <> String.Empty) Then
            ' Direct hexadecimal address
            If (_Address.IndexOf("+", StringComparison.Ordinal) = -1) Then
                Return Integer.Parse(_Address, NumberStyles.HexNumber)
            End If

            ' Module+Offset address format
            Dim tmp As String() = _Address.Split(New Char() {"+"c})
            For Each M As ProcessModule In Proc(0).Modules
                If (M.ModuleName.ToLower = tmp(0).ToLower) Then
                    Return (M.BaseAddress.ToInt32 + Integer.Parse(tmp(1), NumberStyles.HexNumber))
                End If
            Next
        End If
        Return -1
    End Function

    ''' <summary>
    ''' Resolves a full pointer path with optional offsets into an absolute memory address.
    ''' </summary>
    ''' <param name="Address_Offsets">
    ''' A space-separated string in the format "Module+BaseOffset Offset1 Offset2 ..." (e.g., "BlackOps.exe+10A0 20 30") representing a multi-level pointer.
    ''' </param>
    ''' <returns>
    ''' The resolved final address as an <c>Integer</c>, or -1 if the process is not active or resolution fails.
    ''' </returns>
    ''' <remarks>
    ''' This is commonly used for reading memory via multi-level pointer dereferencing.
    ''' </remarks>
    Public Function Pointer(ByVal Address_Offsets As String) As Integer
        Address_Offsets = RemoveChar(DoubleSpace(Address_Offsets, " "))
        Dim tmp As String() = Address_Offsets.Split(New Char() {" "c})

        ' Resolve base address
        Dim _Addy As Integer = _Module(tmp(0))
        If (tmp.Length = 1) Then
            Return _Addy
        End If

        ' Multi-level pointer resolution
        _Addy = BitConverter.ToInt32(ReadBytes(_Addy, 4), 0)
        For i As Integer = 1 To tmp.Length - 1
            Dim Off As Integer = Integer.Parse(tmp(i), NumberStyles.HexNumber)

            ' If not last offset, dereference
            If i <> (tmp.Length - 1) Then
                _Addy = BitConverter.ToInt32(ReadBytes(_Addy + Off, 4), 0)
            Else
                ' Final offset (no dereference)
                _Addy += Off
            End If
        Next i
        Return _Addy
    End Function

#End Region ' Pointers/Module

#Region "Misc"

    ''' <summary>
    ''' Windows message constant for key down.
    ''' </summary>
    Const WM_KEYDOWN As UInt32 = &H100

    ''' <summary>
    ''' Windows message constant for key up.
    ''' </summary>
    Const WM_KEYUP As UInt32 = &H101

    ''' <summary>
    ''' Virtual key code for the Enter key.
    ''' </summary>
    Const VK_RETURN As Integer = &HD

    ''' <summary>
    ''' Simulates pressing the Enter key using PostMessage.
    ''' </summary>
    Public Sub iEnter()
        PostMessage(iHandle, WM_KEYDOWN, CType(VK_RETURN, IntPtr), IntPtr.Zero)
        System.Threading.Thread.Sleep(50)
        PostMessage(iHandle, WM_KEYUP, CType(VK_RETURN, IntPtr), IntPtr.Zero)
    End Sub

    ''' <summary>
    ''' Virtual key code for the "2" key.
    ''' </summary>
    Const VK_2 As Integer = &H32

    ''' <summary>
    ''' Simulates pressing the "2" key using PostMessage.
    ''' </summary>
    Public Sub iPress2()
        PostMessage(iHandle, WM_KEYDOWN, CType(VK_2, IntPtr), IntPtr.Zero)
        System.Threading.Thread.Sleep(50)
        PostMessage(iHandle, WM_KEYUP, CType(VK_2, IntPtr), IntPtr.Zero)
    End Sub

    ''' <summary>
    ''' Virtual key code for the "H" key.
    ''' </summary>
    Const VK_H As Integer = &H48

    ''' <summary>
    ''' Simulates pressing the "H" key using PostMessage.
    ''' </summary>
    Public Sub iPressH()
        PostMessage(iHandle, WM_KEYDOWN, CType(VK_H, IntPtr), IntPtr.Zero)
        System.Threading.Thread.Sleep(50)
        PostMessage(iHandle, WM_KEYUP, CType(VK_H, IntPtr), IntPtr.Zero)
    End Sub

    ''' <summary>
    ''' Virtual key code for the "L" key.
    ''' </summary>
    Const VK_L As Integer = &H4C

    ''' <summary>
    ''' Simulates pressing the "L" key using PostMessage.
    ''' </summary>
    Public Sub iPressL()
        PostMessage(iHandle, WM_KEYDOWN, CType(VK_L, IntPtr), IntPtr.Zero)
        System.Threading.Thread.Sleep(50)
        PostMessage(iHandle, WM_KEYUP, CType(VK_L, IntPtr), IntPtr.Zero)
    End Sub

    ''' <summary>
    ''' Removes extra whitespace in a string.
    ''' </summary>
    Private Function DoubleSpace(ByVal txt As String, ByVal _With As String) As String
        Return Regex.Replace(txt, "\s+", _With)
    End Function

    ''' <summary>
    ''' Searches a block of memory starting from a base address for a given pattern of bytes.
    ''' Wildcards (??) are supported and internally replaced with &H3F ('?') as a match-any byte.
    ''' </summary>
    ''' <param name="Start_Index">The starting memory address to begin scanning from.</param>
    ''' <param name="iLength">The number of bytes to scan from the starting index.</param>
    ''' <param name="SearchFor">The byte signature to search for, e.g. "A1 ?? ?? 8B".</param>
    ''' <returns>
    ''' The memory address where the matching signature is found, or -1 if not found.
    ''' </returns>
    Public Function Get_MemoryAddress(ByVal Start_Index As Integer, ByVal iLength As Integer, ByVal SearchFor As String) As Integer
        ' Replace wildcards "??" with 3F (63 decimal) which is treated as a match-any byte.
        Dim New_String = SearchFor.Replace("??", "3F")

        ' Prepare arrays for reading memory and parsing the signature.
        Dim SearchIn As Byte() = New Byte(iLength - 1) {} ' Buffer to hold memory bytes.
        Dim iSearch As Byte() = New Byte((New_String.Length \ 2) - 1) {}

        ' Build search pattern as byte array.
        For i = 0 To iSearch.Length - 1
            iSearch(i) = Byte.Parse(New_String.Substring(i * 2, 2), Globalization.NumberStyles.HexNumber)
        Next

        ' Read memory from target process starting at Start_Index.
        ReadProcessMemory(Proc(0).Handle, Start_Index, SearchIn, iLength, 0)

        Dim Z As Integer = 0
        Dim Y As Integer = 0
        Dim iEnd As Integer = If(iSearch.Length < 32, iSearch.Length, 32)
        Dim sBytes As Integer() = New Integer(255) {} ' Shift pattern for Boyer-Moore-like optimization

        ' Preprocess wildcards for fast matching.
        For j = 0 To iEnd - 1
            If iSearch(j) = &H3F Then
                Z = Z Or (1 << ((iEnd - j) - 1))
            End If
        Next

        ' If wildcards exist, initialize shift table.
        If Z <> 0 Then
            For k = 0 To sBytes.Length - 1
                sBytes(k) = Z
            Next
        End If

        ' Build shift table for Boyer-Moore optimization.
        Z = 1
        Dim index As Integer = iEnd - 1
        Do While index >= 0
            sBytes(iSearch(index)) = sBytes(iSearch(index)) Or Z
            index -= 1
            Z <<= 1
        Loop

        ' Begin pattern matching.
        Do While Y <= (SearchIn.Length - iSearch.Length)
            Z = iSearch.Length - 1
            Dim length As Integer = iSearch.Length
            Dim m As Integer = -1

            ' Compare bytes backward using shift masks.
            Do While m <> 0
                m = m And sBytes(SearchIn(Y + Z))
                If m <> 0 Then
                    If Z = 0 Then
                        ' Match found
                        Return Start_Index + Y
                    End If
                    length = Z
                End If
                Z -= 1
                m <<= 1
            Loop

            ' Shift search window
            Y += length
        Loop

        ' No match found
        Return -1
    End Function

    ''' <summary>
    ''' Converts an integer to a string representation of its little-endian byte array in hexadecimal.
    ''' </summary>
    ''' <param name="val">The integer value to convert.</param>
    ''' <returns>
    ''' A string of hexadecimal characters representing the byte layout of the integer,
    ''' e.g. IntegerToByte(305419896) returns "78563412".
    ''' </returns>
    Public Function IntegerToByte(ByVal val As Integer) As String
        Dim Hex As String = String.Empty
        Dim bHex As Byte() = BitConverter.GetBytes(val) ' Get little-endian bytes.

        ' Convert each byte to hex and append.
        For j As Integer = 0 To bHex.Length - 1
            Hex += String.Format("{0:x2}", Convert.ToByte(bHex(j)))
        Next

        Return Hex
    End Function

    ''' <summary>
    ''' Determines whether a given string contains only valid hexadecimal characters (0-9, a-f, A-F).
    ''' </summary>
    ''' <param name="Val">The string to validate.</param>
    ''' <returns>True if the input is a valid hexadecimal string; otherwise, false.</returns>
    Private Function IsHex(ByVal Val As String) As Boolean
        ' Use regex to ensure the string consists only of hex digits and is a whole word.
        Return Regex.IsMatch(Val, "\A\b[0-9a-fA-F]+\b\Z")
    End Function

    ''' <summary>
    ''' Truncates a string at the first occurrence of two consecutive spaces.
    ''' </summary>
    ''' <param name="St">The input string to cut.</param>
    ''' <returns>
    ''' The substring from the beginning up to the first double space.
    ''' If no double space is found, returns the original string.
    ''' </returns>
    Private Function CutString(ByVal St As String) As String
        Dim i As Integer = St.IndexOf("  ", StringComparison.Ordinal)
        Return If(i = -1, St, St.Substring(0, i))
    End Function

    ''' <summary>
    ''' Checks if the memory at the given address matches the expected hex byte pattern.
    ''' </summary>
    ''' <param name="_Addy">The address in memory to validate.</param>
    ''' <param name="_B">The expected hex string pattern (e.g., "90 90 90").</param>
    ''' <returns>True if the memory matches the expected pattern; otherwise, false.</returns>
    Private Function Integrity(ByVal _Addy As Integer, ByVal _B As String) As Boolean
        If ((ProcessIsActive And (_Addy <> -1)) And (_B <> String.Empty)) Then
            ' Clean non-hex characters from the string.
            _B = Regex.Replace(_B, "[^a-fA-F0-9]", "")

            ' Ensure the hex string has an even number of characters.
            If ((_B.Length Mod 2) <> 0) Then
                _B = _B.Substring(0, (_B.Length - 1))
            End If

            ' Read the equivalent number of bytes from memory.
            Dim B As Byte() = Me.ReadBytes(_Addy, (_B.Length \ 2))

            ' Compare each byte in memory to expected byte pattern.
            For i As Integer = 0 To B.Length - 1
                If (B(i) <> Byte.Parse(_B.Substring((i * 2), 2), NumberStyles.HexNumber)) Then
                    Return False
                End If
            Next
            Return True
        End If

        Return False
    End Function

    ''' <summary>
    ''' Verifies if the memory value at the specified address matches the expected default value,
    ''' using the provided data type for interpretation.
    ''' </summary>
    ''' <param name="_Addy">The address in memory to validate.</param>
    ''' <param name="_Default">The expected value as a string, to be parsed according to type.</param>
    ''' <param name="MemType">The expected .NET Type (e.g., GetType(Integer)).</param>
    ''' <returns>True if the memory contents match the expected value; otherwise, false.</returns>
    Private Function Integrity(ByVal _Addy As Integer, ByVal _Default As String, ByVal MemType As Object) As Boolean
        Select Case Array.IndexOf(New Object() {GetType(Byte), GetType(Integer), GetType(UInt32), GetType(Single), GetType(Double)}, MemType)
            Case 0
                Return (ReadBytes(_Addy, 1)(0) = Byte.Parse(_Default))
            Case 1
                Return (BitConverter.ToInt32(ReadBytes(_Addy, 4), 0) = Integer.Parse(_Default))
            Case 2
                Return (BitConverter.ToUInt32(ReadBytes(_Addy, 4), 0) = UInt32.Parse(_Default))
            Case 3
                Return (BitConverter.ToSingle(ReadBytes(_Addy, 4), 0) = Single.Parse(_Default))
            Case 4
                Return (BitConverter.ToDouble(ReadBytes(_Addy, 8), 0) = Double.Parse(_Default))
        End Select

        Return False
    End Function

    ''' <summary>
    ''' Converts a hex string (with optional wildcards) into a byte array.
    ''' </summary>
    ''' <param name="HXS">The hex string to convert. Wildcards ('??') will be replaced with &H3F (i.e., '?').</param>
    ''' <returns>A byte array parsed from the hex string.</returns>
    Public Function HX2Bts(ByVal HXS As String) As Byte()
        ' Strip non-hex characters except question marks
        HXS = Regex.Replace(HXS, "[^a-fA-F0-9?]", "")
        ' Replace wildcards with "3F" (ASCII '?')
        HXS = HXS.Replace("??", "3F")

        ' Ensure even-length string (hex pair)
        If ((HXS.Length Mod 2) <> 0) Then
            HXS = HXS.Substring(0, (HXS.Length - 1))
        End If

        ' Convert hex string to byte array
        Dim buf As Byte() = New Byte((HXS.Length \ 2) - 1) {}
        For i As Integer = 0 To buf.Length - 1
            buf(i) = Byte.Parse(HXS.Substring(i * 2, 2), NumberStyles.HexNumber)
        Next i
        Return buf
    End Function

    ''' <summary>
    ''' Generates the hex instruction string for a relative jump or call.
    ''' </summary>
    ''' <param name="Cave">The destination address of the jump (e.g., allocated code cave).</param>
    ''' <param name="JumpFrom">The address from where the jump originates.</param>
    ''' <param name="iLen">Number of bytes the original instruction occupies (used to pad with NOPs).</param>
    ''' <param name="_Jump">If true, uses JMP (E9); otherwise, CALL (E8).</param>
    ''' <returns>A string of hex bytes representing the assembled instruction (plus NOPs as needed).</returns>
    Private Function JmpCall(ByVal Cave As IntPtr, ByVal JumpFrom As Integer, ByVal iLen As Integer, ByVal _Jump As Boolean) As String
        ' Calculate relative jump offset
        Dim Ins As String = GetIns(BitConverter.GetBytes(Cave.ToInt32 - JumpFrom - 5))

        ' Pad with NOPs to fill instruction space
        For i As Integer = 5 To iLen - 1
            Ins += "90"
        Next i

        ' Prefix with opcode: E9 for JMP, E8 for CALL
        Return (If(_Jump, "E9", "E8") & Ins)
    End Function

    ''' <summary>
    ''' Converts a byte array to its lowercase hex string representation.
    ''' </summary>
    ''' <param name="BTS">The byte array to convert.</param>
    ''' <returns>A lowercase string of hex characters (e.g., "90e800").</returns>
    Private Function GetIns(ByVal BTS As Byte()) As String
        Dim tmp As String = String.Empty
        For i As Integer = 0 To BTS.Length - 1
            tmp += String.Format("{0:x2}", Convert.ToUInt32(BTS(i)))
        Next i
        Return tmp
    End Function

    ''' <summary>
    ''' Creates a JMP instruction that returns execution back from a code cave to the original flow.
    ''' </summary>
    ''' <param name="Cave">The address in the code cave where the jump back starts.</param>
    ''' <param name="NextInstruc">The address immediately after the original instruction to return to.</param>
    ''' <returns>A string of hex bytes representing a JMP back instruction.</returns>
    Private Function JumpBack(ByVal Cave As Integer, ByVal NextInstruc As Integer) As String
        ' E9 opcode for JMP, followed by calculated relative offset
        Return "E9" & GetIns(BitConverter.GetBytes(NextInstruc - Cave - 5))
    End Function


    ''' <summary>
    ''' Constructs an opcode string based on the given register and index.
    ''' </summary>
    ''' <param name="Cave_Offset">The offset used for generating the instruction bytes.</param>
    ''' <param name="Reg">The register name to find in the opcode templates (e.g., "eax", "ecx").</param>
    ''' <param name="_Index">
    ''' The index determining which opcode template to use:
    ''' 0 = "_To" templates,
    ''' 1 = "_From" templates,
    ''' 2 = "_Store" templates.
    ''' </param>
    ''' <returns>
    ''' A formatted opcode string combining the matched opcode suffix with instruction bytes,
    ''' or an empty string if no match is found or index is out of range.
    ''' </returns>
    Private Function ConOp(ByVal Cave_Offset As Integer, ByVal Reg As String, ByVal _Index As Byte) As String
        If (_Index <= 2) Then
            Dim _S As String() = Nothing

            ' Predefined opcode templates for different operations
            Dim _To As String() = New String() {"eax 8905", "ecx 890d", "edx 8915", "edi 893d", "ebx 891d", "esi 8935", "ebp 892d", "esp 8925"}
            Dim _From As String() = New String() {"eax a1", "ecx 8b0d", "edx 8b15", "edi 8b3d", "ebx 8b1d", "esi 8b35", "ebp 8b2d", "esp 8b25"}
            Dim _Store As String() = New String() {"eax 8981", "edx 8991", "edi 89b9", "ebx 8999", "esi 89b1", "ebp 89a9", "esp 89a1"}

            ' Select the appropriate opcode template array based on _Index
            _S = IIf(_Index = 0, _To, IIf(_Index = 1, _From, _Store))

            ' Search for the register string in the selected opcode templates
            For i As Integer = 0 To _S.Length - 1
                If (_S(i).IndexOf(Reg, StringComparison.Ordinal) <> -1) Then
                    ' Return the opcode suffix concatenated with instruction bytes from Cave_Offset
                    Return (_S(i).Split(New Char() {" "c})(1) & GetIns(BitConverter.GetBytes(Cave_Offset)))
                End If
            Next
        End If

        ' Return empty if no matching opcode found or invalid index
        Return String.Empty
    End Function

    ''' <summary>
    ''' Reads a series of 4-byte integers starting at the specified memory cave address,
    ''' applies an offset, and returns a list of unique updated addresses.
    ''' </summary>
    ''' <param name="Cave">The base address to read from.</param>
    ''' <param name="Offset">The value to add to each read integer before adding to the returned list.</param>
    ''' <returns>A list of unique integers read from memory plus the given offset.</returns>
    Public Function UpdateTable(ByVal Cave As Integer, ByVal Offset As Integer) As List(Of Integer)
        Dim adr As Integer() = New Integer(29) {}

        ' Read 30 integers (4 bytes each) from consecutive memory addresses starting at Cave
        For i As Integer = 0 To adr.Length - 1
            adr(i) = BitConverter.ToInt32(Me.ReadBytes((Cave + (i * 4)), 4), 0)
        Next

        Dim unique As New List(Of Integer)(30)

        ' Filter to only unique non-zero addresses
        For i As Integer = 0 To adr.Length - 1
            If Not unique.Contains(adr(i)) OrElse adr(i) <> 0 Then
                unique.Add(adr(i))
            End If
        Next

        Dim updated As New List(Of Integer)(30)

        ' Add the offset to each unique address and avoid duplicates
        For i As Integer = 0 To unique.Count - 1
            If Not updated.Contains(unique.Item(i) + Offset) Then
                updated.Add(unique.Item(i) + Offset)
            End If
        Next

        Return updated
    End Function

    ''' <summary>
    ''' Searches the OldScan collection for an entry matching the given signature
    ''' and returns the associated address as an integer.
    ''' </summary>
    ''' <param name="Sig">The signature string to search for.</param>
    ''' <returns>
    ''' The address as an integer parsed from hex if found; otherwise, -1.
    ''' </returns>
    Private Function RetOldScan(ByVal Sig As String) As Integer
        ' Iterate through each string in OldScan
        For i As Integer = 0 To OldScan.Count - 1
            Dim S As String() = OldScan.Item(i).Split(New Char() {" "c})

            ' Check if second token matches the signature
            If (S(1) = Sig) Then
                ' Parse the first token as hex address and return it
                Return Integer.Parse(S(0), NumberStyles.HexNumber)
            End If
        Next

        ' Return -1 if not found
        Return -1
    End Function

    ''' <summary>
    ''' Removes hexadecimal prefix from a string if it starts with "&h" or "0x".
    ''' </summary>
    ''' <param name="txt">The input string potentially containing a hex prefix.</param>
    ''' <returns>The input string without the hex prefix; returns original string if no prefix found.</returns>
    Private Function RemoveChar(ByVal txt As String) As String
        If txt.StartsWith("&h", StringComparison.OrdinalIgnoreCase) Or txt.StartsWith("0x", StringComparison.OrdinalIgnoreCase) Then
            ' Remove first two characters ("&h" or "0x")
            Return txt.Substring(2, txt.Length - 2)
        End If
        Return txt
    End Function

    ''' <summary>
    ''' Converts a byte array representing an opcode into an IntPtr address.
    ''' The byte array is reversed and converted to a hex string, then parsed.
    ''' </summary>
    ''' <param name="B">Byte array containing the opcode bytes.</param>
    ''' <returns>An IntPtr representing the parsed address.</returns>
    Public Function Convert_Opcode(ByVal B As Byte()) As IntPtr
        Dim Temp As String = String.Empty
        Dim j As Integer = (B.Length - 1)

        ' Reverse the byte array and convert each byte to two-digit hex string
        Do While (j >= 0)
            Temp += String.Format("{0:x2}", Convert.ToUInt32(B(j)))
            j -= 1
        Loop

        ' Parse the hex string to integer and return as IntPtr
        Return New IntPtr(Integer.Parse(Temp, NumberStyles.AllowHexSpecifier))
    End Function
#End Region ' Misc

#Region "Game Console"

    ''' <summary>
    ''' Checks whether the specified key is currently pressed down.
    ''' </summary>
    ''' <param name="key">The key to check, specified as a <see cref="Keys"/> value.</param>
    ''' <returns>
    ''' <c>True</c> if the key is pressed (key state equals -127 or -128); otherwise, <c>False</c>.
    ''' </returns>
    Public Function Keystate(ByVal key As Keys) As Boolean
        ' Get the current state of the specified key
        Dim St As Integer = GetKeyState(key)

        ' Return True if key is pressed (high-order bit set)
        Return St = -127 OrElse St = -128
    End Function

#End Region ' Game Console

End Class
