Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Text
Public Class CommandCentre
    Dim mem As ProcMem = New ProcMem
    Dim iConsole As IntPtr = -1, iText = -1
    Private IsOn As Boolean, iFound

    Private Sub iUpdate()
        If (mem.ProcActive) Then
            If (iConsole = -1) Then
                Dim bConsole As Byte() = mem.rBytes(mem.AobScan("BlackOps.exe+0", &H500000, "F705????????500100007415") + 2, 4)
                iConsole = mem.Convert_Opcode(bConsole)
            End If
            If (iText = -1) Then
                Dim bText As Byte() = mem.rBytes(mem.AobScan("BlackOps.exe+0", &H200000, "803D????????007437") + 2, 4)
                iText = mem.Convert_Opcode(bText)
            End If
        End If
    End Sub

    Private Sub Write_Command(ByVal To_Write As String)
        If (mem.ProcActive AndAlso iConsole <> -1 AndAlso iConsole <> IntPtr.Zero AndAlso iText <> IntPtr.Zero) Then
            Dim iWrite As String = If(To_Write.StartsWith("/"), To_Write, "/" + To_Write)
            mem.Write(iConsole, 1)
            mem.WriteString(iText, iWrite)
            mem.iEnter()
            System.Threading.Thread.Sleep(50)
            mem.Write(iConsole, 0)
        End If
    End Sub

    Private Sub iSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles iSend.Click
        If (sCommand.Text <> String.Empty) Then
            Write_Command(sCommand.Text)
            sCommand.Text = String.Empty
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mem.GetProcess("BlackOps")
    End Sub

    Public Sub sharedtimer2()
        If mem.ProcActive Then
            If mem.Keystate(Keys.F1) Then
                IsOn = Not IsOn
                System.Threading.Thread.Sleep(200)
                If IsOn Then
                    mem.Write(iConsole, 1)
                Else
                    mem.Write(iConsole, 0)
                End If
            End If
            If (Not mem.ProcActive AndAlso iConsole <> IntPtr.Zero) Then
                iConsole = IntPtr.Zero
                iText = IntPtr.Zero
                iFound = False
            End If
            If (mem.ProcActive AndAlso Not iFound) Then
                iFound = True
                iUpdate()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim file As Object = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) & "\Steam\SteamApps\common\call of duty black ops\players\config.cfg"
        Try
            SetAttr(file, FileAttribute.Normal)
        Catch ex As Exception
            MsgBox("Cannot access the file in write-mode. Please run the application as an administrator.")
            Exit Sub
        End Try

        Dim Content As String = My.Computer.FileSystem.ReadAllText(file)
        Dim output As String = Regex.Replace(Content, "seta\smonkeytoy\s""0""", "seta monkeytoy ""1""")
        My.Computer.FileSystem.WriteAllText(file, output, False)
        SetAttr(file, FileAttribute.ReadOnly)
        MsgBox("Config.cfg successfully Modded!")
    

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Editor.Show()
    End Sub
End Class
