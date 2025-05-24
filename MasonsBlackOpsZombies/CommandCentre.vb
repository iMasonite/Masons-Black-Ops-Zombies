Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Text
Public Class CommandCentre

    Private Sub Btn_SendCommand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SendCommand.Click
        If (Tbx_Command.Text <> String.Empty) Then
            Utilities.Write_Command(Tbx_Command.Text)
            Tbx_Command.Text = String.Empty
        End If
    End Sub

    Private Sub Btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

End Class
