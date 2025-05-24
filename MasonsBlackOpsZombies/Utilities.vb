Imports Microsoft.Win32

Module Utilities

#Region "Save Data"

    ''' <summary>
    ''' Supported registry data types for saving and loading values with explicit type names.
    ''' </summary>
    Public Enum RegistryDataType
        [Boolean]
        Int32
        Int64
        [String]
        MultiString
        ExpandString
        ByteArray
    End Enum

    ' ========== Save Values ==========

    ''' <summary>
    ''' Saves a Boolean value to the registry.
    ''' </summary>
    Public Sub RegistrySaveBoolean(valueName As String, value As Boolean)
        SaveToRegistry(valueName, value, RegistryDataType.Boolean)
    End Sub

    ''' <summary>
    ''' Saves an Int32 value to the registry.
    ''' </summary>
    Public Sub RegistrySaveInt32(valueName As String, value As Integer)
        SaveToRegistry(valueName, value, RegistryDataType.Int32)
    End Sub

    ''' <summary>
    ''' Saves an Int64 value to the registry.
    ''' </summary>
    Public Sub RegistrySaveInt64(valueName As String, value As Long)
        SaveToRegistry(valueName, value, RegistryDataType.Int64)
    End Sub

    ''' <summary>
    ''' Saves a String value to the registry.
    ''' </summary>
    Public Sub RegistrySaveString(valueName As String, value As String)
        SaveToRegistry(valueName, value, RegistryDataType.String)
    End Sub

    ''' <summary>
    ''' Saves a MultiString (String array) value to the registry.
    ''' </summary>
    Public Sub RegistrySaveMultiString(valueName As String, value As String())
        SaveToRegistry(valueName, value, RegistryDataType.MultiString)
    End Sub

    ''' <summary>
    ''' Saves an ExpandString value to the registry.
    ''' </summary>
    Public Sub RegistrySaveExpandString(valueName As String, value As String)
        SaveToRegistry(valueName, value, RegistryDataType.ExpandString)
    End Sub

    ''' <summary>
    ''' Saves a ByteArray value to the registry.
    ''' </summary>
    Public Sub RegistrySaveByteArray(valueName As String, value As Byte())
        SaveToRegistry(valueName, value, RegistryDataType.ByteArray)
    End Sub

    ' ========== Load Values ==========

    ''' <summary>
    ''' Loads a Boolean value from the registry.
    ''' </summary>
    Public Function RegistryLoadBoolean(valueName As String, Optional defaultValue As Boolean = False) As Boolean
        Return CBool(LoadFromRegistry(valueName, defaultValue, RegistryDataType.Boolean))
    End Function

    ''' <summary>
    ''' Loads an Int32 value from the registry.
    ''' </summary>
    Public Function RegistryLoadInt32(valueName As String, Optional defaultValue As Integer = 0) As Integer
        Return CInt(LoadFromRegistry(valueName, defaultValue, RegistryDataType.Int32))
    End Function

    ''' <summary>
    ''' Loads an Int64 value from the registry.
    ''' </summary>
    Public Function RegistryLoadInt64(valueName As String, Optional defaultValue As Long = 0L) As Long
        Return CLng(LoadFromRegistry(valueName, defaultValue, RegistryDataType.Int64))
    End Function

    ''' <summary>
    ''' Loads a String value from the registry.
    ''' </summary>
    Public Function RegistryLoadString(valueName As String, Optional defaultValue As String = "") As String
        Return CStr(LoadFromRegistry(valueName, defaultValue, RegistryDataType.String))
    End Function

    ''' <summary>
    ''' Loads a MultiString (String array) value from the registry.
    ''' </summary>
    Public Function RegistryLoadMultiString(valueName As String, Optional defaultValue As String() = Nothing) As String()
        Return CType(LoadFromRegistry(valueName, defaultValue, RegistryDataType.MultiString), String())
    End Function

    ''' <summary>
    ''' Loads an ExpandString value from the registry.
    ''' </summary>
    Public Function RegistryLoadExpandString(valueName As String, Optional defaultValue As String = "") As String
        Return CStr(LoadFromRegistry(valueName, defaultValue, RegistryDataType.ExpandString))
    End Function

    ''' <summary>
    ''' Loads a ByteArray value from the registry.
    ''' </summary>
    Public Function RegistryLoadByteArray(valueName As String, Optional defaultValue As Byte() = Nothing) As Byte()
        Return CType(LoadFromRegistry(valueName, defaultValue, RegistryDataType.ByteArray), Byte())
    End Function

    ''' <summary>
    ''' Saves a value to the registry under the specified key and name with explicit data type,
    ''' validating that the value matches the specified data type.
    ''' </summary>
    ''' <param name="valueName">The name of the value to store.</param>
    ''' <param name="value">The value to store.</param>
    ''' <param name="dataType">The data type to use when saving to the registry.</param>
    Private Sub SaveToRegistry(valueName As String, value As Object, dataType As RegistryDataType)
        ' Validate type compatibility
        Dim isValid As Boolean = False
        Dim expectedTypeName As String = ""

        Select Case dataType
            Case RegistryDataType.Boolean
                expectedTypeName = "Boolean"
                isValid = TypeOf value Is Boolean
            Case RegistryDataType.Int32
                expectedTypeName = "Int32"
                isValid = TypeOf value Is Integer
            Case RegistryDataType.Int64
                expectedTypeName = "Int64"
                isValid = TypeOf value Is Long
            Case RegistryDataType.String, RegistryDataType.ExpandString
                expectedTypeName = "String"
                isValid = TypeOf value Is String
            Case RegistryDataType.MultiString
                expectedTypeName = "String()"
                isValid = TypeOf value Is String()
            Case RegistryDataType.ByteArray
                expectedTypeName = "Byte()"
                isValid = TypeOf value Is Byte()
            Case Else
                MessageBox.Show("Unsupported registry data type specified.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
        End Select

        If Not isValid Then
            MessageBox.Show(
            $"Value type mismatch. Expected type: {expectedTypeName}, but got type: {value.GetType().Name}.",
            "Type Mismatch Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
            Return
        End If

        ' If valid, proceed to save
        Using key As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\MasonsBlackOpsZombies")
            Select Case dataType
                Case RegistryDataType.Boolean
                    key.SetValue(valueName, If(CBool(value), 1, 0), RegistryValueKind.DWord)
                Case RegistryDataType.Int32
                    key.SetValue(valueName, Convert.ToInt32(value), RegistryValueKind.DWord)
                Case RegistryDataType.Int64
                    key.SetValue(valueName, Convert.ToInt64(value), RegistryValueKind.QWord)
                Case RegistryDataType.String
                    key.SetValue(valueName, CStr(value), RegistryValueKind.String)
                Case RegistryDataType.ExpandString
                    key.SetValue(valueName, CStr(value), RegistryValueKind.ExpandString)
                Case RegistryDataType.MultiString
                    key.SetValue(valueName, CType(value, String()), RegistryValueKind.MultiString)
                Case RegistryDataType.ByteArray
                    key.SetValue(valueName, CType(value, Byte()), RegistryValueKind.Binary)
            End Select
        End Using
    End Sub


    ''' <summary>
    ''' Loads a value from the registry with expected data type.
    ''' </summary>
    ''' <param name="valueName">The name of the value to retrieve.</param>
    ''' <param name="defaultValue">The default value to return if the registry key or value is not found.</param>
    ''' <param name="dataType">The expected data type to load.</param>
    ''' <returns>The value from the registry, or the default if not found.</returns>
    Private Function LoadFromRegistry(valueName As String, defaultValue As Object, dataType As RegistryDataType) As Object
        Using key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\MasonsBlackOpsZombies")
            If key IsNot Nothing Then
                Dim rawValue = key.GetValue(valueName, defaultValue)
                Try
                    Dim result As Object = Nothing
                    Select Case dataType
                        Case RegistryDataType.Boolean
                            ' Interpret stored Int32 1 or 0 as Boolean
                            result = Convert.ToInt32(rawValue) <> 0
                        Case RegistryDataType.Int32
                            result = Convert.ToInt32(rawValue)
                        Case RegistryDataType.Int64
                            result = Convert.ToInt64(rawValue)
                        Case RegistryDataType.String, RegistryDataType.ExpandString
                            result = CStr(rawValue)
                        Case RegistryDataType.MultiString
                            result = CType(rawValue, String())
                        Case RegistryDataType.ByteArray
                            result = CType(rawValue, Byte())
                        Case Else
                            Throw New ArgumentException("Unsupported registry data type.")
                    End Select
                    Debug.WriteLine($"LoadFromRegistry: Loaded '{valueName}' = {FormatDebugValue(result)} (Type: {result?.GetType().Name })")
                    Return result
                Catch ex As Exception
                    Debug.WriteLine($"LoadFromRegistry: Failed to load '{valueName}', returning default '{defaultValue}' due to error: {ex.Message}")
                    Return defaultValue
                End Try
            End If
        End Using

        Debug.WriteLine($"LoadFromRegistry: Registry key not found for '{valueName}', returning default '{defaultValue}'")
        Return defaultValue
    End Function

    ''' <summary>
    ''' Helper to format debug output for values, arrays, and nulls.
    ''' </summary>
    Private Function FormatDebugValue(value As Object) As String
        If value Is Nothing Then Return "Nothing"
        If TypeOf value Is Array Then
            Dim arr = CType(value, Array)
            Return $"[{String.Join(", ", arr.Cast(Of Object)())}]"
        End If
        Return value.ToString()
    End Function


#End Region ' Save Data

#Region "Console"

    ''' <summary>
    ''' Scans the Black Ops process memory for specific byte patterns to locate the
    ''' console toggle address and the console text input address. This is only performed once
    ''' per session and only if the game process is currently active.
    ''' </summary>
    Public Sub UpdateConsoleAddresses()
        If BlackOpsObject.ProcessIsActive Then

            ' Locate and cache the address that controls console visibility (if not already found)
            If ConsoleEnabledAddress = -1 Then
                Dim consoleBytes As Byte() = BlackOpsObject.ReadBytes(
                BlackOpsObject.AobScan("BlackOps.exe+0", &H500000, "F705????????500100007415") + 2, 4)
                ConsoleEnabledAddress = BlackOpsObject.Convert_Opcode(consoleBytes)
            End If

            ' Locate and cache the address that accepts console text input (if not already found)
            If ConsoleTextAddress = -1 Then
                Dim textBytes As Byte() = BlackOpsObject.ReadBytes(
                BlackOpsObject.AobScan("BlackOps.exe+0", &H200000, "803D????????007437") + 2, 4)
                ConsoleTextAddress = BlackOpsObject.Convert_Opcode(textBytes)
            End If

        End If
    End Sub

#End Region ' Console

#Region "Resources"

    ''' <summary>
    ''' Loads the contents of an embedded JSON resource file as a string.
    ''' </summary>
    ''' <param name="resourceName">The fully qualified name of the embedded resource (e.g., <c>"MyApp.Resources.Data.json"</c>).</param>
    ''' <returns>The JSON content of the resource as a string.</returns>
    ''' <exception cref="IO.FileNotFoundException">
    ''' Thrown if the specified resource cannot be found in the executing assembly.
    ''' </exception>
    Public Function LoadJsonFromResources(resourceName As String) As String
        Dim assembly = Reflection.Assembly.GetExecutingAssembly()

        Using stream = assembly.GetManifestResourceStream(resourceName)
            If stream Is Nothing Then
                Throw New IO.FileNotFoundException($"Resource '{resourceName}' not found.")
            End If
            Using reader As New IO.StreamReader(stream)
                Return reader.ReadToEnd()
            End Using
        End Using
    End Function

#End Region ' Resources

#Region "Validation"

    ''' <summary>
    ''' Determines whether a given <see cref="Vector3F"/> position is valid.
    ''' A position is considered valid if none of its X, Y, or Z components are zero.
    ''' </summary>
    ''' <param name="pos">The 3D position to validate.</param>
    ''' <returns>
    ''' <c>True</c> if all components (X, Y, Z) of the position are non-zero; otherwise, <c>False</c>.
    ''' </returns>
    Public Function IsValidPosition(pos As Vector3F) As Boolean
        Return pos.X <> 0 AndAlso pos.Y <> 0 AndAlso pos.Z <> 0
    End Function


    ''' <summary>
    ''' Checks whether a string consists only of null characters.
    ''' </summary>
    ''' <param name="s">The input string.</param>
    ''' <returns>True if the string is non-null and entirely null characters.</returns>
    Public Function AllNullChars(s As String) As Boolean
        Return Not String.IsNullOrEmpty(s) AndAlso s.All(Function(c) c = Chr(0))
    End Function

    ''' <summary>
    ''' Clamps a value between a specified minimum and maximum.
    ''' </summary>
    ''' <param name="value">The value to clamp.</param>
    ''' <param name="min">The minimum allowable value.</param>
    ''' <param name="max">The maximum allowable value.</param>
    ''' <returns>The clamped value.</returns>
    Public Function Clamp(value As Integer, min As Integer, max As Integer) As Integer
        If value < min Then
            Return min
        ElseIf value > max Then
            Return max
        Else
            Return value
        End If
    End Function

#End Region ' Validation

#Region "Command Management"

    ''' <summary>
    ''' Sends a console command to bind the 'L' key to a specified action (e.g., "god", "noclip", etc.).
    ''' Automatically enables online cheats if needed, executes the bind, triggers the key press, and restores cheat toggle state.
    ''' </summary>
    ''' <param name="CommandString">The command to bind to the L key.</param>
    Public Sub Write_Command(ByVal CommandString As String)
        Dim bindCommand As String = $"/bind L ""{CommandString}"""
        ExecuteBoundCommandToConsole(bindCommand)
    End Sub

    ''' <summary>
    ''' Sends a bind command to give a weapon or item using the 'give' command, bound to the 'L' key.
    ''' Automatically enables online cheats if needed, executes the bind, triggers the key press, and restores cheat toggle state.
    ''' </summary>
    ''' <param name="CommandString">The item or weapon string for the give command (e.g., weapon_raycast).</param>
    Public Sub Write_CommandGive(ByVal CommandString As String)
        Dim bindCommand As String = $"/bind L give ""{CommandString}"""
        ExecuteBoundCommandToConsole(bindCommand)
    End Sub

    ''' <summary>
    ''' Sends a bind command to the in-game console and simulates pressing the "L" key to trigger it.
    ''' Automatically enables cheats temporarily if required.
    ''' </summary>
    ''' <param name="bindCommand">The full console command string to bind and execute (e.g., <c>/bind L "god"</c>).</param>
    Private Sub ExecuteBoundCommandToConsole(bindCommand As String)
        Dim cheatsWereInitiallyDisabled As Boolean = Not EnableOnlineCheats

        ' Temporarily enable cheats if they are off
        If cheatsWereInitiallyDisabled Then
            BlackOpsObject.Write(MemoryAddresses.P1_EnableOnlineCheats, 1, GetType(Integer))
            EnableOnlineCheats = True
            System.Threading.Thread.Sleep(25)
        End If

        ' Ensure the process is running and both console addresses are valid
        If BlackOpsObject.ProcessIsActive AndAlso ConsoleEnabledAddress <> -1 AndAlso ConsoleEnabledAddress <> IntPtr.Zero AndAlso ConsoleTextAddress <> IntPtr.Zero Then

            ' Open the console and write the command
            BlackOpsObject.Write(ConsoleEnabledAddress, 1)
            BlackOpsObject.WriteString(ConsoleTextAddress, bindCommand)
            BlackOpsObject.iEnter()

            Debug.WriteLine($"New Keybind: {bindCommand}")

            System.Threading.Thread.Sleep(25)

            ' Close the console and simulate key press
            BlackOpsObject.Write(ConsoleEnabledAddress, 0)
            System.Threading.Thread.Sleep(25)
            BlackOpsObject.iPressL()
        End If

        ' Restore cheat toggle state if it was off before
        If cheatsWereInitiallyDisabled Then
            BlackOpsObject.Write(MemoryAddresses.P1_EnableOnlineCheats, 0, GetType(Integer))
            EnableOnlineCheats = False
        End If
    End Sub

    ''' <summary>
    ''' Executes a command based on a checkbox's state and logs the result. Updates the checkbox's forecolor afterward.
    ''' </summary>
    ''' <param name="cbx">The checkbox that determines which command to execute.</param>
    ''' <param name="commandTrue">The command string to run if the checkbox is checked.</param>
    ''' <param name="commandFalse">The command string to run if the checkbox is unchecked.</param>
    ''' <param name="callerName">Optional. The name of the calling method for logging and error messages. Defaults to "undefined_method".</param>
    ''' <remarks>
    ''' Displays an error message and returns early if either command string is empty or whitespace.
    ''' </remarks>
    Public Sub RunConditionalCommand(cbx As CheckBox, commandTrue As String, commandFalse As String, Optional callerName As String = "undefined_method")
        If String.IsNullOrWhiteSpace(commandTrue) OrElse String.IsNullOrWhiteSpace(commandFalse) Then
            MessageBox.Show($"RunConditionalCommand error in {callerName}: Command strings cannot be empty.", "Command Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"{callerName}: Error - One or both command strings are empty.")
            Return
        End If

        Dim commandToRun As String = If(cbx.Checked, commandTrue, commandFalse)
        Write_Command(commandToRun)

        Debug.WriteLine($"{callerName}: Executed command '{commandToRun}' based on checkbox '{cbx.Name}' state: {cbx.Checked}")
        UpdateCheckBoxForeColor(cbx)
    End Sub

#End Region ' Command Management

#Region "Player Location"

    ''' <summary>
    ''' Reads a Vector3F (3D float vector) from memory at the given base address.
    ''' Assumes the vector is stored sequentially as X, Y, Z (each as a 4-byte float).
    ''' </summary>
    ''' <param name="baseAddress">The base memory address of the vector.</param>
    ''' <returns>A Vector3F containing the X, Y, Z components read from memory.</returns>
    Public Function ReadPlayerLocationVector3F(baseAddress As Integer) As Vector3F
        Dim x As Single = BlackOpsObject.ReadRaw(baseAddress, GetType(Single))
        Dim y As Single = BlackOpsObject.ReadRaw(baseAddress + 4, GetType(Single))
        Dim z As Single = BlackOpsObject.ReadRaw(baseAddress + 8, GetType(Single))
        Return New Vector3F(x, y, z)
    End Function

    ''' <summary>
    ''' Writes a Vector3F (3D float vector) to memory at the given base address.
    ''' Each component (X, Y, Z) is written as a 4-byte float.
    ''' </summary>
    ''' <param name="baseAddress">The base memory address to write to.</param>
    ''' <param name="vec">The Vector3F containing the X, Y, Z components to write.</param>
    Public Sub WritePlayerLocationVector3F(baseAddress As Integer, vec As Vector3F)
        BlackOpsObject.WriteF(baseAddress, vec.X)
        BlackOpsObject.WriteF(baseAddress + 4, vec.Y)
        BlackOpsObject.WriteF(baseAddress + 8, vec.Z)
    End Sub

    ''' <summary>
    ''' Reads the player's current position from memory and saves it into the provided <see cref="Vector3F"/> structure.
    ''' </summary>
    ''' <param name="locationAddress">The base address offset of the player's position in memory.</param>
    ''' <param name="savedPos">A reference to a <see cref="Vector3F"/> structure where the position will be saved.</param>
    ''' <remarks>
    ''' The position is only saved if all components (X, Y, Z) are non-zero to avoid invalid or default positions.
    ''' The method reads 3 consecutive floats representing the player's position coordinates.
    ''' </remarks>
    Public Sub SavePlayerPosition(locationAddress As Integer, ByRef savedPos As Vector3F)
        Dim MemoryLocationPlayerPos As Integer = BlackOpsObject._Module(locationAddress)

        Dim x As Single = BlackOpsObject.ReadFloat(MemoryLocationPlayerPos)
        Dim y As Single = BlackOpsObject.ReadFloat(MemoryLocationPlayerPos + 4)
        Dim z As Single = BlackOpsObject.ReadFloat(MemoryLocationPlayerPos + 8)

        If x <> 0 AndAlso y <> 0 AndAlso z <> 0 Then
            savedPos.X = x
            savedPos.Y = y
            savedPos.Z = z
        End If
    End Sub

#End Region ' Player Location

#Region "Form Style"

    ''' <summary>
    ''' Updates the <see cref="CheckBox"/> foreground color based on its checked state.
    ''' </summary>
    ''' <param name="chk">The checkbox whose <c>ForeColor</c> will be updated.</param>
    ''' <remarks>
    ''' Green (RGB 87, 242, 135) for checked (active), red (RGB 237, 66, 69) for unchecked (indicating previously used).
    ''' </remarks>
    Public Sub UpdateCheckBoxForeColor(chk As CheckBox)
        chk.ForeColor = If(chk.Checked, Color.FromArgb(87, 242, 135), Color.FromArgb(237, 66, 69))
    End Sub

#End Region ' Form Style

#Region "Default Control Values"

    ''' <summary>
    ''' Caches the default state of controls directly contained within the specified GroupBox.
    ''' Stores default properties in Globals.ControlDefaultValues dictionary.
    ''' </summary>
    ''' <param name="groupBox">The GroupBox whose immediate child controls to cache.</param>
    Public Sub CacheDefaultControlValues(groupBox As GroupBox)
        For Each ctrl As Control In groupBox.Controls
            ' Initialize default values struct with common properties
            Dim defaults As New ControlDefaults With {
                .Text = ctrl.Text,
                .ForeColor = ctrl.ForeColor,
                .Enabled = ctrl.Enabled,
                .Checked = Nothing,
                .NumericValue = Nothing,
                .TextBoxText = Nothing,
                .ComboBoxSelectedIndex = Nothing
            }

            ' Cache additional properties depending on control type
            Select Case True
                Case TypeOf ctrl Is CheckBox
                    defaults.Checked = CType(ctrl, CheckBox).Checked
                Case TypeOf ctrl Is NumericUpDown
                    defaults.NumericValue = CType(ctrl, NumericUpDown).Value
                Case TypeOf ctrl Is TextBox
                    defaults.TextBoxText = CType(ctrl, TextBox).Text
                Case TypeOf ctrl Is ComboBox
                    defaults.ComboBoxSelectedIndex = CType(ctrl, ComboBox).SelectedIndex
                Case TypeOf ctrl Is Button
                    ' Button uses Text property only, already set
            End Select

            ' Save defaults keyed by control reference
            Globals.ControlDefaultValues(ctrl) = defaults
        Next
    End Sub

    ''' <summary>
    ''' Recursively caches the default state of all controls within a container (and its children)
    ''' whose names start with specific prefixes (Cbx_, Btn_, Nud_, Tbx_, Combx_).
    ''' </summary>
    ''' <param name="container">The container control to recursively scan.</param>
    Public Sub CacheDefaultControlValuesRecursive(container As Control)
        For Each ctrl As Control In container.Controls
            ' Only cache controls with targeted prefixes to avoid unnecessary entries
            If ctrl.Name.StartsWith("Cbx_") OrElse
               ctrl.Name.StartsWith("Btn_") OrElse
               ctrl.Name.StartsWith("Nud_") OrElse
               ctrl.Name.StartsWith("Tbx_") OrElse
               ctrl.Name.StartsWith("Combx_") Then

                Dim defaults As New ControlDefaults With {
                    .Text = ctrl.Text,
                    .ForeColor = ctrl.ForeColor,
                    .Enabled = ctrl.Enabled,
                    .Checked = Nothing,
                    .NumericValue = Nothing,
                    .TextBoxText = Nothing,
                    .ComboBoxSelectedIndex = Nothing
                }

                Select Case True
                    Case TypeOf ctrl Is CheckBox
                        defaults.Checked = CType(ctrl, CheckBox).Checked
                    Case TypeOf ctrl Is NumericUpDown
                        defaults.NumericValue = CType(ctrl, NumericUpDown).Value
                    Case TypeOf ctrl Is TextBox
                        defaults.TextBoxText = CType(ctrl, TextBox).Text
                    Case TypeOf ctrl Is ComboBox
                        defaults.ComboBoxSelectedIndex = CType(ctrl, ComboBox).SelectedIndex
                    Case TypeOf ctrl Is Button
                        ' Button uses Text only, already captured
                End Select

                Globals.ControlDefaultValues(ctrl) = defaults
            End If

            ' Recurse into child controls if any
            If ctrl.HasChildren Then
                CacheDefaultControlValuesRecursive(ctrl)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Resets a single control to its cached default state if available.
    ''' Handles each supported control type accordingly.
    ''' </summary>
    ''' <param name="ctrl">The control to reset.</param>
    Public Sub ResetControlToDefault(ctrl As Control)
        If Globals.ControlDefaultValues.ContainsKey(ctrl) Then
            Dim def = Globals.ControlDefaultValues(ctrl)

            ctrl.Enabled = def.Enabled

            Select Case True
                Case TypeOf ctrl Is CheckBox
                    Dim cb = CType(ctrl, CheckBox)
                    cb.Text = def.Text
                    cb.ForeColor = def.ForeColor
                    If def.Checked.HasValue Then cb.Checked = def.Checked.Value

                Case TypeOf ctrl Is Button
                    Dim btn = CType(ctrl, Button)
                    btn.Text = def.Text

                Case TypeOf ctrl Is NumericUpDown
                    Dim nud = CType(ctrl, NumericUpDown)
                    If def.NumericValue.HasValue Then nud.Value = def.NumericValue.Value

                Case TypeOf ctrl Is TextBox
                    Dim tb = CType(ctrl, TextBox)
                    tb.Text = def.TextBoxText

                Case TypeOf ctrl Is ComboBox
                    Dim cbx = CType(ctrl, ComboBox)
                    If def.ComboBoxSelectedIndex.HasValue Then cbx.SelectedIndex = def.ComboBoxSelectedIndex.Value

                Case Else
                    ' Fallback: reset common properties
                    ctrl.Text = def.Text
                    ctrl.ForeColor = def.ForeColor
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Resets all controls directly contained within the specified GroupBox to their cached default states.
    ''' Disables the GroupBox after resetting all child controls.
    ''' </summary>
    ''' <param name="groupBox">The GroupBox whose child controls to reset.</param>
    Public Sub ResetControlsToDefaults(groupBox As GroupBox)
        For Each ctrl As Control In groupBox.Controls
            If Globals.ControlDefaultValues.ContainsKey(ctrl) Then
                Dim def = Globals.ControlDefaultValues(ctrl)
                ctrl.Text = def.Text
                ctrl.ForeColor = def.ForeColor
                ctrl.Enabled = def.Enabled

                Select Case True
                    Case TypeOf ctrl Is CheckBox AndAlso def.Checked.HasValue
                        CType(ctrl, CheckBox).Checked = def.Checked.Value
                    Case TypeOf ctrl Is NumericUpDown AndAlso def.NumericValue.HasValue
                        CType(ctrl, NumericUpDown).Value = def.NumericValue.Value
                    Case TypeOf ctrl Is TextBox AndAlso def.TextBoxText IsNot Nothing
                        CType(ctrl, TextBox).Text = def.TextBoxText
                    Case TypeOf ctrl Is ComboBox AndAlso def.ComboBoxSelectedIndex.HasValue
                        CType(ctrl, ComboBox).SelectedIndex = def.ComboBoxSelectedIndex.Value
                End Select
            End If
        Next

        groupBox.Enabled = False
    End Sub

    ''' <summary>
    ''' Recursively resets all controls within a container (and its children) to their cached default states.
    ''' </summary>
    ''' <param name="container">The container control to reset recursively.</param>
    Public Sub ResetControlsToDefaultsRecursive(container As Control)
        For Each ctrl As Control In container.Controls
            If Globals.ControlDefaultValues.ContainsKey(ctrl) Then
                Dim def = Globals.ControlDefaultValues(ctrl)
                ctrl.Text = def.Text
                ctrl.ForeColor = def.ForeColor
                ctrl.Enabled = def.Enabled

                Select Case True
                    Case TypeOf ctrl Is CheckBox AndAlso def.Checked.HasValue
                        CType(ctrl, CheckBox).Checked = def.Checked.Value
                    Case TypeOf ctrl Is NumericUpDown AndAlso def.NumericValue.HasValue
                        CType(ctrl, NumericUpDown).Value = def.NumericValue.Value
                    Case TypeOf ctrl Is TextBox AndAlso def.TextBoxText IsNot Nothing
                        CType(ctrl, TextBox).Text = def.TextBoxText
                    Case TypeOf ctrl Is ComboBox AndAlso def.ComboBoxSelectedIndex.HasValue
                        CType(ctrl, ComboBox).SelectedIndex = def.ComboBoxSelectedIndex.Value
                End Select
            End If

            ' Recursively reset children controls
            If ctrl.HasChildren Then
                ResetControlsToDefaultsRecursive(ctrl)
            End If
        Next
    End Sub


#End Region ' Default Control Values

End Module
