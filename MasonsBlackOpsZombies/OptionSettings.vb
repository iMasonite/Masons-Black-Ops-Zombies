Public Class OptionSettings

    Private Sub OptionSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Tbx_MainTimerFrequency.Text = "" & Globals.MainTimerFrequency * 10 & "ms"
        TrackBar_MainTimerFrequency.Value = Globals.MainTimerFrequency

        Tbx_HealthCheckRate.Text = "" & Globals.HealthCheckRate * 10 & "ms"
        TrackBar_HealthCheckRate.Value = Globals.HealthCheckRate

        Tbx_PointsCheckRate.Text = "" & Globals.PointsCheckRate * 10 & "ms"
        TrackBar_PointsCheckRate.Value = Globals.PointsCheckRate

        Tbx_LocationTrackingRate.Text = "" & Globals.LocationTrackingRate * 10 & "ms"
        TrackBar_LocationTrackingRate.Value = Globals.LocationTrackingRate

    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

#Region "Change Level"

    Private Sub Btn_ActionChangeLevel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Btn_ActionChangeLevel.Click
        ' Check if the target process is attached and active
        If Not BlackOpsObject.ProcessIsActive Then
            MessageBox.Show("Target process is not active.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Convert current round value to byte string (used for AoB scan)
        Dim levelPattern As String = BlackOpsObject.IntegerToByte(Nub_Round_Current.Value)

        ' Attempt to find address with given AoB pattern
        Dim address As Integer = BlackOpsObject.Get_MemoryAddress(&H2AB00000, &HF0000, levelPattern & "????1200")

        If address = -1 Then
            MessageBox.Show("Level pattern not found in memory.", "Scan Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Apply level change
        Change_Level(Nub_Round_Next.Value, address)
        Nub_Round_Current.Enabled = False
    End Sub

    ''' <summary>
    ''' Changes the level value at a specified memory address.
    ''' </summary>
    ''' <param name="Level">The new level value to write.</param>
    ''' <param name="Address">The memory address (as an integer) where the level should be written.</param>
    ''' <remarks>
    ''' This method writes the specified level value directly to the given memory address using the default data type (usually Integer).
    ''' </remarks>
    Private Sub Change_Level(ByVal Level As Integer, ByVal Address As Integer)
        BlackOpsObject.Write(Address, Level)
    End Sub

#End Region ' Change Level

#Region "Task Frequencies"

    ''' <summary>
    ''' Updates the TextBox text, saves the value to the registry, and updates the global variable.
    ''' </summary>
    ''' <param name="trackBar">The TrackBar control.</param>
    ''' <param name="textBox">The TextBox to display the interval text.</param>
    ''' <param name="registryKey">The registry key name to save.</param>
    ''' <param name="globalVarSetter">Action to update the global variable with the TrackBar value.</param>
    Private Sub UpdateIntervalValue(trackBar As TrackBar, textBox As TextBox, registryKey As String, globalVarSetter As Action(Of Integer))
        Dim val As Integer = trackBar.Value
        textBox.Text = $"{val * 10}ms"
        Utilities.RegistrySaveInt32(registryKey, val)
        globalVarSetter(val)
    End Sub

    ''' <summary>
    ''' Resets the TrackBar to the default value and updates the UI, registry, and global variable.
    ''' </summary>
    ''' <param name="trackBar">The TrackBar control.</param>
    ''' <param name="textBox">The TextBox to display the interval text.</param>
    ''' <param name="registryKey">The registry key name to save.</param>
    ''' <param name="defaultValue">The default TrackBar value to set.</param>
    ''' <param name="globalVarSetter">Action to update the global variable with the TrackBar value.</param>
    Private Sub ResetIntervalValue(trackBar As TrackBar, textBox As TextBox, registryKey As String, defaultValue As Integer, globalVarSetter As Action(Of Integer))
        trackBar.Value = defaultValue
        UpdateIntervalValue(trackBar, textBox, registryKey, globalVarSetter)
    End Sub

    Private Sub TrackBar_MainTimerFrequency_Scroll(sender As Object, e As EventArgs) Handles TrackBar_MainTimerFrequency.Scroll
        UpdateIntervalValue(TrackBar_MainTimerFrequency, Tbx_MainTimerFrequency, "MainTimerFrequency", Sub(v) Globals.MainTimerFrequency = v)
    End Sub

    Private Sub TrackBar_HealthCheckRate_Scroll(sender As Object, e As EventArgs) Handles TrackBar_HealthCheckRate.Scroll
        UpdateIntervalValue(TrackBar_HealthCheckRate, Tbx_HealthCheckRate, "HealthCheckRate", Sub(v) Globals.HealthCheckRate = v)
    End Sub

    Private Sub TrackBar_PointsCheckRate_Scroll(sender As Object, e As EventArgs) Handles TrackBar_PointsCheckRate.Scroll
        UpdateIntervalValue(TrackBar_PointsCheckRate, Tbx_PointsCheckRate, "PointsCheckRate", Sub(v) Globals.PointsCheckRate = v)
    End Sub

    Private Sub TrackBar_LocationTrackingRate_Scroll(sender As Object, e As EventArgs) Handles TrackBar_LocationTrackingRate.Scroll
        UpdateIntervalValue(TrackBar_LocationTrackingRate, Tbx_LocationTrackingRate, "LocationTrackingRate", Sub(v) Globals.LocationTrackingRate = v)
    End Sub

    Private Sub Btn_MainTimerFrequency_Reset_Click(sender As Object, e As EventArgs) Handles Btn_MainTimerFrequency_Reset.Click
        ResetIntervalValue(TrackBar_MainTimerFrequency, Tbx_MainTimerFrequency, "MainTimerFrequency", 100, Sub(v) Globals.MainTimerFrequency = v)
    End Sub

    Private Sub Btn_HealthCheckRate_Reset_Click(sender As Object, e As EventArgs) Handles Btn_HealthCheckRate_Reset.Click
        ResetIntervalValue(TrackBar_HealthCheckRate, Tbx_HealthCheckRate, "HealthCheckRate", 15, Sub(v) Globals.HealthCheckRate = v)
    End Sub

    Private Sub Btn_PointsCheckRate_Reset_Click(sender As Object, e As EventArgs) Handles Btn_PointsCheckRate_Reset.Click
        ResetIntervalValue(TrackBar_PointsCheckRate, Tbx_PointsCheckRate, "PointsCheckRate", 25, Sub(v) Globals.PointsCheckRate = v)
    End Sub

    Private Sub Btn_LocationTrackingRate_Reset_Click(sender As Object, e As EventArgs) Handles Btn_LocationTrackingRate_Reset.Click
        ResetIntervalValue(TrackBar_LocationTrackingRate, Tbx_LocationTrackingRate, "LocationTrackingRate", 50, Sub(v) Globals.LocationTrackingRate = v)
    End Sub

#End Region ' Task Frequencies

End Class