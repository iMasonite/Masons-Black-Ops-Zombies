Imports System.Text.RegularExpressions
Public Class Editor
    Dim mem As ProcMem = New ProcMem

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If CheckBox1.Checked Then
            Dim File As Object = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) & "\Steam\SteamApps\common\call of duty black ops\players\config.cfg"
            Try
                SetAttr(File, FileAttribute.Normal)
            Catch ex As Exception
                MsgBox("Cannot access the file in write-mode. Please run the application as an administrator.")
                Exit Sub
            End Try
            Dim Content As String = My.Computer.FileSystem.ReadAllText(File)
            Dim TextToAppend As String = ""
            If Not Content.Contains("spu") Then
                TextToAppend &= Environment.NewLine & Environment.NewLine & "spu" & Environment.NewLine
            End If
            Dim KeyBind As String = TxtKey.Text
            Dim Command As String = LstCommands.SelectedItem.ToString()
            Dim CommandOnOff As String = Command.Substring(Command.LastIndexOf(" ") + 1)
            If Not Content.Contains("Bind """ & KeyBind & """ """ & Command.Replace(CommandOnOff, "").Trim() & " " & IIf(CommandOnOff.Contains("On") Or CommandOnOff.Contains("1"), "1", "0") & """") Then
                TextToAppend &= Environment.NewLine & Environment.NewLine & "Bind """ & KeyBind & """ """ & Command.Replace(CommandOnOff, "").Trim() & " " & IIf(CommandOnOff.Contains("On") Or CommandOnOff.Contains("1"), "1", "0") & """"
            End If
            My.Computer.FileSystem.WriteAllText(File, Content & TextToAppend, False)
            SetAttr(File, FileAttribute.ReadOnly)
            MsgBox("KeyBinds Added!")
            If mem.ProcActive = True Then
                MsgBox("Please Restart Black Ops For Changes To Take Effect!")
            End If
        End If
        If CheckBox2.Checked Then
            Dim File As Object = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) & "\Steam\SteamApps\common\call of duty black ops\players\config.cfg"
            Try
                SetAttr(File, FileAttribute.Normal)
            Catch ex As Exception
                MsgBox("Cannot access the file in write-mode. Please run the application as an administrator.")
                Exit Sub
            End Try

            Dim Content As String = My.Computer.FileSystem.ReadAllText(File)
            Dim TextToAppend As String = ""

            If Not Content.Contains("spu") Then
                TextToAppend &= Environment.NewLine & Environment.NewLine & "spu" & Environment.NewLine
            End If
            Dim KeyBind As String = TxtKey1.Text
            Dim Command As String = LstCommands1.Text.ToString()

            Dim CommandOnOff As String = Command.Substring(Command.LastIndexOf(" ") + 1)
            If Not Content.Contains("Bind """ & KeyBind & """ """ & Command.Replace(CommandOnOff, "").Trim() & " " & IIf(CommandOnOff.Contains("On") Or CommandOnOff.Contains("1"), "1", "0") & """") Then
                TextToAppend &= Environment.NewLine & Environment.NewLine & "Bind """ & KeyBind & """ """ & Command.Replace(CommandOnOff, "").Trim() & " " & IIf(CommandOnOff.Contains("On") Or CommandOnOff.Contains("1"), "1", "0") & """"
            End If
            My.Computer.FileSystem.WriteAllText(File, Content & TextToAppend, False)
                SetAttr(File, FileAttribute.ReadOnly)
                MsgBox("KeyBinds Added!")
                If mem.ProcActive = True Then
                    MsgBox("Please Restart Black Ops For Changes To Take Effect!")
                End If
            End If
        If CheckBox1.Checked = False And CheckBox2.Checked = False Then
            MsgBox("You Did Not Tick Any Of The Selection Boxes, Nothing Has Happend!")
        End If
    End Sub

    Private Sub Editor_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        mem.GetProcess("BlackOps")
        LstCommands.SelectedIndex = 0
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim File As Object = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) & "\Steam\SteamApps\common\call of duty black ops\players\config.cfg"
        Dim Backup = System.IO.Path.GetDirectoryName(File) & "\config.bak"
        Dim Content As String = My.Computer.FileSystem.ReadAllText(File)
        My.Computer.FileSystem.WriteAllText(Backup, Content, False)
        MsgBox("BackUp Of Config.cfg Successfull!")
    End Sub

End Class