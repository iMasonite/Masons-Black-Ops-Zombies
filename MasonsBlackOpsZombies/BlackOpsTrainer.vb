Imports System.Reflection
Imports Microsoft.Win32
Imports Newtonsoft.Json

Public Class BlackOpsTrainer
    Private isLoading As Boolean = True

    Private WithEvents HealthMonitorTimer As New Timer With {.Interval = 250}
    Private WithEvents PointsMonitorTimer As New Timer With {.Interval = 100}
    Private WithEvents LocationMonitorTimer As New Timer With {.Interval = 500}

    Dim P2_StoredPlayerPos As Vector3F = New Vector3F(0, 0, 0)
    Dim P3_StoredPlayerPos As Vector3F = New Vector3F(0, 0, 0)
    Dim P4_StoredPlayerPos As Vector3F = New Vector3F(0, 0, 0)

    Dim FormSizeWithoutCoOpMode As Integer = 646

#Region "Main"

#Region "Loading"

    ''' <summary>
    ''' Handles initialization logic when the trainer form loads.
    ''' </summary>
    ''' <param name="sender">The source of the event (the form).</param>
    ''' <param name="e">The event data.</param>
    ''' <remarks>
    ''' - Starts the tickrate timers.
    ''' - Initializes connection to the BlackOps process.
    ''' - Loads embedded JSON resources for location and weapon data.
    ''' - Populates the map ComboBox with keys from the location dictionary.
    ''' - Attempts to restore the last selected options from the registry.
    ''' </remarks>
    Private Sub BlackOpsTrainer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HealthMonitorTimer.Start()
        PointsMonitorTimer.Start()
        LocationMonitorTimer.Start()

        BlackOpsObject.GetProcess("BlackOps")

        AddHandler Globals.BlackOpsObject.ProcessStarted, AddressOf HandleProcessStarted
        AddHandler Globals.BlackOpsObject.ProcessStopped, AddressOf HandleProcessStopped

#If DEBUG Then
        Dim assembly = Reflection.Assembly.GetExecutingAssembly()
        Dim names = assembly.GetManifestResourceNames()
        For Each name As String In names
            Console.WriteLine(name)
        Next
#End If

        ' ========= Resources =========

        Dim locationDataJson = LoadJsonFromResources("MasonsBlackOpsZombies.Locations.json")
        LocationDictionary = JsonConvert.DeserializeObject(Of Dictionary(Of String, List(Of LocationEntry)))(locationDataJson)
        Combx_CurrentMap.Items.AddRange(LocationDictionary.Keys.ToArray())

        Dim weaponDataJson = LoadJsonFromResources("MasonsBlackOpsZombies.Weapons.json")
        MapWeaponsDictionary = JsonConvert.DeserializeObject(Of Dictionary(Of String, WeaponCategories))(weaponDataJson)

        Dim weaponsDictionaryJson As String = LoadJsonFromResources("MasonsBlackOpsZombies.DictionaryWeapons.json")
        WeaponsDictionary = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(weaponsDictionaryJson)

        ' ========= Saved Data =========

        ' Load selected map
        Dim lastMap As String = Utilities.RegistryLoadString("LastSelectedMap", "")
        If Combx_CurrentMap.Items.Contains(lastMap) Then
            Combx_CurrentMap.SelectedItem = lastMap
        End If

        ' Load Enabled Co-Op Mode
        Cbx_EnabledCoOpMode.Checked = Utilities.RegistryLoadBoolean("EnabledCoOpMode", True)
        If Cbx_EnabledCoOpMode.Checked Then
            Me.Size = New Size(1600, 900)
        Else
            Me.Size = New Size(FormSizeWithoutCoOpMode, 900)
        End If

        ' Timer Intervals with proper defaults and minimums
        Globals.MainTimerFrequency = Utilities.Clamp(RegistryLoadInt32("MainTimerFrequency", 100), 25, 200)
        Main.Interval = Globals.MainTimerFrequency * 10

        Globals.HealthCheckRate = Utilities.Clamp(RegistryLoadInt32("HealthCheckRate", 15), 5, 100)
        HealthMonitorTimer.Interval = Globals.HealthCheckRate * 10

        Globals.PointsCheckRate = Utilities.Clamp(RegistryLoadInt32("PointsCheckRate", 25), 5, 100)
        PointsMonitorTimer.Interval = Globals.PointsCheckRate * 10

        Globals.LocationTrackingRate = Utilities.Clamp(RegistryLoadInt32("LocationTrackingRate", 50), 5, 100)
        LocationMonitorTimer.Interval = Globals.LocationTrackingRate * 10

        ' Cache Default Control Values
        Utilities.CacheDefaultControlValuesRecursive(Me)

#If Not DEBUG Then
        Cbx_Test.Visible = False
#End If

        ToolTip_MainForm.SetToolTip(Cbx_EasySamsGame, "When on Moon, The computers at the spawn (Samantha's Game) will always be Red (computer 1) for each game.")

        Me.isLoading = False ' Finished loading
    End Sub

    ''' <summary>
    ''' Populates a weapon selection ComboBox with entries from a specific category and map.
    ''' </summary>
    ''' <param name="comboBox">The ComboBox control to populate.</param>
    ''' <param name="mapName">The name of the map to retrieve weapon data for.</param>
    ''' <param name="category">
    ''' The weapon category to populate from. Valid values include:
    ''' "SpecialWeapons", "RegularWeapons", "UpgradedWeapons", "WonderWeapons", "TacticalWeapons".
    ''' </param>
    ''' <remarks>
    ''' - Uses <c>mapWeaponsDictionary</c> to fetch weapons specific to the given map and category.
    ''' - Uses <c>weaponsDictionary</c> to resolve display names from weapon IDs.
    ''' - Adds a default selection entry at the top of the list.
    ''' - Sets <c>DisplayMember</c> to "DisplayName" and selects the first item.
    ''' </remarks>
    Private Sub PopulateWeaponComboBox(comboBox As ComboBox, mapName As String, category As String)
        comboBox.Items.Clear()

        If Not MapWeaponsDictionary.ContainsKey(mapName) Then
            Return
        End If

        Dim weaponCategory = MapWeaponsDictionary(mapName)

        Select Case category
            Case "SpecialWeapons"
                comboBox.Items.Add(New WeaponEntry With {.WeaponId = "", .WeaponIdValue = 0, .DisplayName = "Select Special Weapon"})

                ' List(Of Dictionary(Of String, Integer))
                Dim specials = weaponCategory.SpecialWeapons
                For Each dict In specials
                    For Each kvp In dict
                        Dim weaponId = kvp.Key
                        Dim value = kvp.Value
                        Dim displayName = If(WeaponsDictionary.ContainsKey(weaponId), WeaponsDictionary(weaponId), weaponId)

                        comboBox.Items.Add(New WeaponEntry With {.WeaponId = weaponId, .WeaponIdValue = value, .DisplayName = displayName})
                    Next
                Next

            Case Else
                ' Other categories are List(Of String)
                Dim weaponsList As List(Of String) = Nothing
                Select Case category
                    Case "RegularWeapons"
                        comboBox.Items.Add(New WeaponEntry With {.WeaponId = "", .WeaponIdValue = 0, .DisplayName = "Select Regular Weapon"})
                        weaponsList = weaponCategory.RegularWeapons
                    Case "UpgradedWeapons"
                        comboBox.Items.Add(New WeaponEntry With {.WeaponId = "", .WeaponIdValue = 0, .DisplayName = "Select Upgraded Weapon"})
                        weaponsList = weaponCategory.UpgradedWeapons
                    Case "WonderWeapons"
                        comboBox.Items.Add(New WeaponEntry With {.WeaponId = "", .WeaponIdValue = 0, .DisplayName = "Select Wonder Weapon"})
                        weaponsList = weaponCategory.WonderWeapons
                    Case "TacticalWeapons"
                        comboBox.Items.Add(New WeaponEntry With {.WeaponId = "", .WeaponIdValue = 0, .DisplayName = "Select Tactical Weapon"})
                        weaponsList = weaponCategory.Tacticals
                End Select

                If weaponsList IsNot Nothing Then
                    For Each weaponId In weaponsList
                        Dim displayName = If(WeaponsDictionary.ContainsKey(weaponId), WeaponsDictionary(weaponId), weaponId)

                        comboBox.Items.Add(New WeaponEntry With {.WeaponId = weaponId, .WeaponIdValue = 0, .DisplayName = displayName})
                    Next
                End If

        End Select

        comboBox.DisplayMember = "DisplayName"
        comboBox.SelectedIndex = 0
    End Sub

#End Region ' Loading


#Region "Main Monitor"

    Private Sub Main_Tick(ByVal sender As System.Object, e As System.EventArgs) Handles Main.Tick

        ' ========= Tick Rates =========

        If Main.Interval <> Globals.MainTimerFrequency * 10 Then
            Main.Interval = Globals.MainTimerFrequency * 10
        End If

        If HealthMonitorTimer.Interval <> Globals.HealthCheckRate * 10 Then
            HealthMonitorTimer.Interval = Globals.HealthCheckRate * 10
        End If

        If PointsMonitorTimer.Interval <> Globals.PointsCheckRate * 10 Then
            PointsMonitorTimer.Interval = Globals.PointsCheckRate * 10
        End If

        If LocationMonitorTimer.Interval <> Globals.LocationTrackingRate * 10 Then
            LocationMonitorTimer.Interval = Globals.LocationTrackingRate * 10
        End If

        ' ========= Console =========
        If BlackOpsObject.ProcessIsActive Then

            ' Toggle in-game console visibility using TAB key
            If BlackOpsObject.Keystate(Keys.Tab) Then
                ConsoleVisible = Not ConsoleVisible
                System.Threading.Thread.Sleep(200)
                If ConsoleVisible Then
                    BlackOpsObject.Write(ConsoleEnabledAddress, 1)
                Else
                    BlackOpsObject.Write(ConsoleEnabledAddress, 0)
                End If
            End If

            ' Reset console state if process is lost
            If (Not BlackOpsObject.ProcessIsActive AndAlso ConsoleEnabledAddress <> IntPtr.Zero) Then
                ConsoleEnabledAddress = IntPtr.Zero
                ConsoleTextAddress = IntPtr.Zero
                ConsoleAddressesLocated = False
            End If

            ' Locate console memory addresses
            If (BlackOpsObject.ProcessIsActive AndAlso Not ConsoleAddressesLocated) Then
                ConsoleAddressesLocated = True
                UpdateConsoleAddresses()
            End If

        End If

        ' ========= Map Specific Controls =========

        Dim selectedMap As String = Combx_CurrentMap.SelectedItem?.ToString()

        Select Case selectedMap
            Case "Moon"
                Cbx_EasySamsGame.Enabled = True
                'Cbx_P1_HackDevice.Enabled = True
            Case Else
                With Cbx_EasySamsGame
                    .Checked = False
                    .Enabled = False
                End With
                With Cbx_P1_HackDevice
                    '.Checked = False
                    '.Enabled = False
                End With
        End Select

#Region "Player Character"
        Dim tempName As String

        ' === Player 1 ===
        Static P1_CharacterName As String = ""
        tempName = BlackOpsObject.ReadString(MemoryAddresses.P1_PlayerNameA, 30)

        ' Replace null/whitespace/empty with "undefined"
        If String.IsNullOrWhiteSpace(tempName) OrElse AllNullChars(tempName) Then
            tempName = "P1"
        End If

        If tempName <> P1_CharacterName Then
            P1_CharacterName = tempName
            Btn_P1_Weapons_Special_GiveAction.Text = P1_CharacterName
        End If

        Globals.P1_PlayerEnabled = (tempName <> "P1")
        Btn_P1_Weapons_Special_GiveAction.Enabled = Globals.P1_PlayerEnabled

        ' === Player 2 ===
        Static P2_CharacterName As String = ""
        If Not Cbx_EnabledCoOpMode.Checked Then
            ' CoOp mode disabled: reset and disable player UI
            Globals.P2_PlayerEnabled = False
            P2_CharacterName = ""
            Btn_P2_Weapons_Special_GiveAction.Text = "P2"
            Btn_P2_Weapons_Special_GiveAction.Enabled = False
            GroupBox_P2.Text = "P2: "
            GroupBox_P2.Enabled = False
        Else
            ' CoOp mode enabled: check player name
            tempName = BlackOpsObject.ReadString(MemoryAddresses.P2_PlayerNameA, 30)

            If AllNullChars(tempName) OrElse String.IsNullOrWhiteSpace(tempName) Then
                ' Invalid player name: disable UI
                Globals.P2_PlayerEnabled = False
                P2_CharacterName = ""
                Btn_P2_Weapons_Special_GiveAction.Text = "P2"
                Btn_P2_Weapons_Special_GiveAction.Enabled = False
                GroupBox_P2.Text = "P2: "
                GroupBox_P2.Enabled = False
            Else
                ' Valid player name: enable UI and update only if name changed
                Globals.P2_PlayerEnabled = True
                If tempName <> P2_CharacterName Then
                    P2_CharacterName = tempName
                    Btn_P2_Weapons_Special_GiveAction.Text = tempName
                    Btn_P2_Weapons_Special_GiveAction.Enabled = True
                    GroupBox_P2.Text = "P2: " & tempName
                End If
                GroupBox_P2.Enabled = True
            End If
        End If

        ' === Player 3 ===
        Static P3_CharacterName As String = ""
        If Not Cbx_EnabledCoOpMode.Checked Then
            Globals.P3_PlayerEnabled = False
            P3_CharacterName = ""
            Btn_P3_Weapons_Special_GiveAction.Text = "P3"
            Btn_P3_Weapons_Special_GiveAction.Enabled = False
            GroupBox_P3.Text = "P3: "
            GroupBox_P3.Enabled = False
        Else
            tempName = BlackOpsObject.ReadString(MemoryAddresses.P3_PlayerNameA, 30)

            If AllNullChars(tempName) OrElse String.IsNullOrWhiteSpace(tempName) Then
                Globals.P3_PlayerEnabled = False
                P3_CharacterName = ""
                Btn_P3_Weapons_Special_GiveAction.Text = "P3"
                Btn_P3_Weapons_Special_GiveAction.Enabled = False
                GroupBox_P3.Text = "P3: "
                GroupBox_P3.Enabled = False
            Else
                Globals.P3_PlayerEnabled = True
                If tempName <> P3_CharacterName Then
                    P3_CharacterName = tempName
                    Btn_P3_Weapons_Special_GiveAction.Text = tempName
                    Btn_P3_Weapons_Special_GiveAction.Enabled = True
                    GroupBox_P3.Text = "P3: " & tempName
                End If
                GroupBox_P3.Enabled = True
            End If
        End If

        ' === Player 4 ===
        Static P4_CharacterName As String = ""
        If Not Cbx_EnabledCoOpMode.Checked Then
            Globals.P4_PlayerEnabled = False
            P4_CharacterName = ""
            Btn_P4_Weapons_Special_GiveAction.Text = "P4"
            Btn_P4_Weapons_Special_GiveAction.Enabled = False
            GroupBox_P4.Text = "P4: "
            GroupBox_P4.Enabled = False
        Else
            tempName = BlackOpsObject.ReadString(MemoryAddresses.P4_PlayerNameA, 30)

            If AllNullChars(tempName) OrElse String.IsNullOrWhiteSpace(tempName) Then
                Globals.P4_PlayerEnabled = False
                P4_CharacterName = ""
                Btn_P4_Weapons_Special_GiveAction.Text = "P4"
                Btn_P4_Weapons_Special_GiveAction.Enabled = False
                GroupBox_P4.Text = "P4: "
                GroupBox_P4.Enabled = False
            Else
                Globals.P4_PlayerEnabled = True
                If tempName <> P4_CharacterName Then
                    P4_CharacterName = tempName
                    Btn_P4_Weapons_Special_GiveAction.Text = tempName
                    Btn_P4_Weapons_Special_GiveAction.Enabled = True
                    GroupBox_P4.Text = "P4: " & tempName
                End If
                GroupBox_P4.Enabled = True
            End If
        End If

#End Region ' Player Character

#Region "Monitor Variables"
        ' Target Head Only
        If Cbx_P1_TargetHeadOnly.Checked Then
            Dim P1_Kills As Integer = BlackOpsObject._Module(MemoryAddresses.P1_Kills)
            P1_Kills = BlackOpsObject.ReadRaw(P1_Kills, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_HeadShots, P1_Kills, GetType(Integer))
        End If

        If Cbx_P2_TargetHeadOnly.Checked AndAlso Globals.P2_PlayerEnabled Then
            Dim P2_Kills As Integer = BlackOpsObject._Module(MemoryAddresses.P2_Kills)
            P2_Kills = BlackOpsObject.ReadRaw(P2_Kills, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_HeadShots, P2_Kills, GetType(Integer))
        End If

        If Cbx_P3_TargetHeadOnly.Checked AndAlso Globals.P3_PlayerEnabled Then
            Dim P3_Kills As Integer = BlackOpsObject._Module(MemoryAddresses.P3_Kills)
            P3_Kills = BlackOpsObject.ReadRaw(P3_Kills, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_HeadShots, P3_Kills, GetType(Integer))
        End If

        If Cbx_P4_TargetHeadOnly.Checked AndAlso Globals.P4_PlayerEnabled Then
            Dim P4_Kills As Integer = BlackOpsObject._Module(MemoryAddresses.P4_Kills)
            P4_Kills = BlackOpsObject.ReadRaw(P4_Kills, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_HeadShots, P4_Kills, GetType(Integer))
        End If


        'Dim selectedMap As String = Combx_CurrentMap.SelectedItem?.ToString()
        'If Not String.IsNullOrEmpty(selectedMap) Then
        '    If selectedMap.Equals("Moon") AndAlso BlackOpsObject.ProcessIsActive Then
        '        BlackOpsObject.Write(MemoryAddresses.DeviceDisablePickup, If(Cbx_P1_HackDevice.Checked, 0, 1), GetType(Integer))
        '        BlackOpsObject.Write(MemoryAddresses.DeviceVisibilityHidden, If(Cbx_P1_HackDevice.Checked, 0, 1), GetType(Integer))
        '    End If
        'End If

        ' TODO: discover why there are missmatched entry counts
        ' Infinite Ammo
        If Cbx_P1_InfiniteAmmo.Checked Then
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoA, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoB, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoC, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoD, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoE, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoF, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoG, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoH, 1023, GetType(Integer))
        End If

        If Cbx_P2_InfiniteAmmo.Checked AndAlso Globals.P2_PlayerEnabled Then
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoA, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoB, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoC, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoD, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoE, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoF, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoG, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoH, 1023, GetType(Integer))
        End If

        If Cbx_P3_InfiniteAmmo.Checked AndAlso Globals.P3_PlayerEnabled Then
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoA, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoB, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoC, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoD, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoE, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoF, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoG, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoH, 1023, GetType(Integer))
        End If

        If Cbx_P4_InfiniteAmmo.Checked AndAlso Globals.P4_PlayerEnabled Then
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoA, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoB, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoC, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoD, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoE, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoF, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoG, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoH, 1023, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoI, 1023, GetType(Integer))
        End If


        ' Set Zero Points
        If Cbx_P2_SetZeroPoints.Checked AndAlso Globals.P2_PlayerEnabled Then
            BlackOpsObject.Write(MemoryAddresses.P2_Points, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_HeadShots, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_Kills, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_Revives, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_Downs, 0, GetType(Integer))
        End If

        If Cbx_P3_SetZeroPoints.Checked AndAlso Globals.P3_PlayerEnabled Then
            BlackOpsObject.Write(MemoryAddresses.P3_Points, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_HeadShots, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_Kills, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_Revives, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_Downs, 0, GetType(Integer))
        End If

        If Cbx_P4_SetZeroPoints.Checked AndAlso Globals.P4_PlayerEnabled Then
            BlackOpsObject.Write(MemoryAddresses.P4_HeadShots, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_Kills, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_Points, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_Revives, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_Downs, 0, GetType(Integer))
        End If


        ' Take Weapons
        If Cbx_P2_TakeWeapons.Checked AndAlso Globals.P2_PlayerEnabled Then
            BlackOpsObject.Write(MemoryAddresses.P2_WeaponSlot1, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_WeaponSlot2, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_WeaponSlot3, 0, GetType(Integer))
        End If

        If Cbx_P3_TakeWeapons.Checked AndAlso Globals.P3_PlayerEnabled Then
            BlackOpsObject.Write(MemoryAddresses.P3_WeaponSlot1, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_WeaponSlot2, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_WeaponSlot3, 0, GetType(Integer))
        End If

        If Cbx_P4_TakeWeapons.Checked AndAlso Globals.P4_PlayerEnabled Then
            BlackOpsObject.Write(MemoryAddresses.P4_WeaponSlot1, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_WeaponSlot2, 0, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_WeaponSlot3, 0, GetType(Integer))
        End If

#End Region ' Monitor Variables

    End Sub

#End Region ' Main Monitor

#Region "Health Monitor"

    Private Sub HealthMonitorTimer_Tick(sender As Object, e As EventArgs) Handles HealthMonitorTimer.Tick
        If Not BlackOpsObject.ProcessIsActive Then Exit Sub

        ' God/Jesus Health
        Dim GodHealth As Integer = 99999
        Dim JesusHealth As Integer = 250

        Dim p1GodModeAValue As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(MemoryAddresses.P1_PlayerHealthA, GetType(Integer)))
        Dim p1GodModeBValue As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(MemoryAddresses.P1_PlayerHealthB, GetType(Integer)))

        Tbx_P1_HealthA.Text = p1GodModeAValue
        Tbx_P1_HealthB.Text = p1GodModeBValue

        If Cbx_P1_GodHealth.Checked Then
#If DEBUG Then
            If p1GodModeAValue < GodHealth Then Debug.WriteLine($"P1_PlayerHealth: ({p1GodModeAValue} | {p1GodModeBValue}) Health: {If(Cbx_P1_JesusHealth.Checked, "Jesus Health", If(Cbx_P1_GodHealth.Checked, "God Health", " Normal"))} Rate: {Globals.HealthCheckRate * 10}ms")
#End If
            BlackOpsObject.Write(MemoryAddresses.P1_PlayerHealthA, GodHealth, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_PlayerHealthB, GodHealth, GetType(Integer))
        ElseIf Cbx_P1_JesusHealth.Checked Then
#If DEBUG Then
            If p1GodModeAValue < JesusHealth Then Debug.WriteLine($"P1_PlayerHealth: ({p1GodModeAValue} | {p1GodModeBValue}) Health: {If(Cbx_P1_JesusHealth.Checked, "Jesus Health", If(Cbx_P1_GodHealth.Checked, "God Health", " Normal"))} Rate: {Globals.HealthCheckRate * 10}ms")
#End If
            BlackOpsObject.Write(MemoryAddresses.P1_PlayerHealthA, JesusHealth, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_PlayerHealthB, JesusHealth, GetType(Integer))
        End If

        If Globals.P2_PlayerEnabled Then
            Dim p2GodModeAValue As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(MemoryAddresses.P2_PlayerHealthA, GetType(Integer)))
            Dim p2GodModeBValue As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(MemoryAddresses.P2_PlayerHealthB, GetType(Integer)))

            Tbx_P2_HealthA.Text = p2GodModeAValue
            Tbx_P2_HealthB.Text = p2GodModeBValue

            If Cbx_P2_GodHealth.Checked Then
#If DEBUG Then
                If p2GodModeAValue < GodHealth Then Debug.WriteLine($"P2_PlayerHealth: ({p2GodModeAValue} | {p2GodModeBValue}) Health: {If(Cbx_P2_JesusHealth.Checked, "Jesus Health", If(Cbx_P2_GodHealth.Checked, "God Health", " Normal"))} Rate: {Globals.HealthCheckRate * 10}ms")
#End If
                BlackOpsObject.Write(MemoryAddresses.P2_PlayerHealthA, GodHealth, GetType(Integer))
                BlackOpsObject.Write(MemoryAddresses.P2_PlayerHealthB, GodHealth, GetType(Integer))
            ElseIf Cbx_P2_JesusHealth.Checked Then
#If DEBUG Then
                If p2GodModeAValue < JesusHealth Then Debug.WriteLine($"P2_PlayerHealth: ({p2GodModeAValue} | {p2GodModeBValue}) Health: {If(Cbx_P2_JesusHealth.Checked, "Jesus Health", If(Cbx_P2_GodHealth.Checked, "God Health", " Normal"))} Rate: {Globals.HealthCheckRate * 10}ms")
#End If
                BlackOpsObject.Write(MemoryAddresses.P2_PlayerHealthA, JesusHealth, GetType(Integer))
                BlackOpsObject.Write(MemoryAddresses.P2_PlayerHealthB, JesusHealth, GetType(Integer))
            End If
        End If

        If Globals.P3_PlayerEnabled Then
            Dim p3GodModeAValue As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(MemoryAddresses.P3_PlayerHealthA, GetType(Integer)))
            Dim p3GodModeBValue As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(MemoryAddresses.P3_PlayerHealthB, GetType(Integer)))

            Tbx_P3_HealthA.Text = p3GodModeAValue
            Tbx_P3_HealthB.Text = p3GodModeBValue

            If Cbx_P3_GodHealth.Checked Then
#If DEBUG Then
                If p3GodModeAValue < GodHealth Then Debug.WriteLine($"P3_PlayerHealth: ({p3GodModeAValue} | {p3GodModeBValue}) Health: {If(Cbx_P3_JesusHealth.Checked, "Jesus Health", If(Cbx_P3_GodHealth.Checked, "God Health", " Normal"))} Rate: {Globals.HealthCheckRate * 10}ms")
#End If
                BlackOpsObject.Write(MemoryAddresses.P3_PlayerHealthA, GodHealth, GetType(Integer))
                BlackOpsObject.Write(MemoryAddresses.P3_PlayerHealthB, GodHealth, GetType(Integer))
            ElseIf Cbx_P3_JesusHealth.Checked Then
#If DEBUG Then
                If p3GodModeAValue < JesusHealth Then Debug.WriteLine($"P3_PlayerHealth: ({p3GodModeAValue} | {p3GodModeBValue}) Health: {If(Cbx_P3_JesusHealth.Checked, "Jesus Health", If(Cbx_P3_GodHealth.Checked, "God Health", " Normal"))} Rate: {Globals.HealthCheckRate * 10}ms")
#End If
                BlackOpsObject.Write(MemoryAddresses.P3_PlayerHealthA, JesusHealth, GetType(Integer))
                BlackOpsObject.Write(MemoryAddresses.P3_PlayerHealthB, JesusHealth, GetType(Integer))
            End If
        End If

        If Globals.P4_PlayerEnabled Then
            Dim p4GodModeAValue As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(MemoryAddresses.P4_PlayerHealthA, GetType(Integer)))
            Dim p4GodModeBValue As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(MemoryAddresses.P4_PlayerHealthB, GetType(Integer)))

            Tbx_P4_HealthA.Text = p4GodModeAValue
            Tbx_P4_HealthB.Text = p4GodModeBValue

            If Cbx_P4_GodHealth.Checked Then
#If DEBUG Then
                If p4GodModeAValue < GodHealth Then Debug.WriteLine($"P4_PlayerHealth: ({p4GodModeAValue} | {p4GodModeBValue}) Health: {If(Cbx_P4_JesusHealth.Checked, "Jesus Health", If(Cbx_P4_GodHealth.Checked, "God Health", " Normal"))} Rate: {Globals.HealthCheckRate * 10}ms")
#End If
                BlackOpsObject.Write(MemoryAddresses.P3_PlayerHealthA, GodHealth, GetType(Integer))
                BlackOpsObject.Write(MemoryAddresses.P3_PlayerHealthB, GodHealth, GetType(Integer))
            ElseIf Cbx_P4_JesusHealth.Checked Then
#If DEBUG Then
                If p4GodModeAValue < JesusHealth Then Debug.WriteLine($"P4_PlayerHealth: ({p4GodModeAValue} | {p4GodModeBValue}) Health: {If(Cbx_P4_JesusHealth.Checked, "Jesus Health", If(Cbx_P4_GodHealth.Checked, "God Health", " Normal"))} Rate: {Globals.HealthCheckRate * 10}ms")
#End If
                BlackOpsObject.Write(MemoryAddresses.P4_PlayerHealthA, JesusHealth, GetType(Integer))
                BlackOpsObject.Write(MemoryAddresses.P4_PlayerHealthB, JesusHealth, GetType(Integer))
            End If
        End If

    End Sub

#End Region ' Points Monitior

#Region "Points Monitor"

    ''' <summary>
    ''' Detects a specific point deduction and restores the deducted amount if enabled.
    ''' </summary>
    ''' <param name="address">The memory address storing the player's current points.</param>
    ''' <param name="playerLabel">A string label identifying the player (used in logs).</param>
    ''' <param name="currentPoints">The player's current point total at the time of this check.</param>
    ''' <param name="deductedAmountToWatchFor">The exact deduction amount to watch for (e.g., Mystery Box cost).</param>
    ''' <param name="chkEnabled">Checkbox that enables or disables the restoration behavior.</param>
    ''' <remarks>
    ''' This method tracks the previous point total and detects a drop of exactly the specified amount.
    ''' If such a deduction occurs and the feature is enabled, it restores the deducted points once.
    ''' </remarks>
    Private Sub HandleRestoreDeductedPoints(address As String, playerLabel As String, currentPoints As Integer, deductedAmountToWatchFor As Integer, chkEnabled As CheckBox)
        Static lastPointsDict As New Dictionary(Of String, Integer)
        Static restoreGivenDict As New Dictionary(Of String, Boolean)

        ' Skip if feature is not enabled
        If Not chkEnabled.Checked Then Return

        ' Initialize if not already tracked
        If Not lastPointsDict.ContainsKey(address) Then
            lastPointsDict(address) = currentPoints
            restoreGivenDict(address) = False
            Return
        End If

        Dim lastPoints As Integer = lastPointsDict(address)
        Dim restoreGiven As Boolean = restoreGivenDict(address)

        ' Detect specific deduction
        If lastPoints - currentPoints = deductedAmountToWatchFor AndAlso Not restoreGiven Then
            Dim restoredPoints As Integer = currentPoints + deductedAmountToWatchFor
            BlackOpsObject.Write(address, restoredPoints, GetType(Integer))
            Debug.WriteLine($"{playerLabel}: {deductedAmountToWatchFor} points restored. Before = {currentPoints}, After = {restoredPoints}")
            restoreGivenDict(address) = True
        ElseIf currentPoints <> lastPoints Then
            restoreGivenDict(address) = False
            Debug.WriteLine($"{playerLabel}: Point change detected ({lastPoints} → {currentPoints}), restore flag reset.")
        End If

        lastPointsDict(address) = currentPoints
    End Sub

    ''' <summary>
    ''' Detects a point gain of a specific amount and rewards a bonus if enabled.
    ''' </summary>
    ''' <param name="address">The memory address storing the player's current points.</param>
    ''' <param name="playerLabel">A string label identifying the player (used in logs).</param>
    ''' <param name="currentPoints">The player's current point total at the time of this check.</param>
    ''' <param name="expectedGain">The specific point increase to watch for (e.g., for reward triggers).</param>
    ''' <param name="chkBonusEnabled">Checkbox that enables or disables the bonus behavior.</param>
    ''' <remarks>
    ''' This method monitors point changes and grants an additional bonus when an exact expected gain occurs.
    ''' The bonus is applied only once per matching gain to avoid repeated triggers.
    ''' </remarks>
    Private Sub HandlePointBonus(address As String, playerLabel As String, currentPoints As Integer, expectedGain As Integer, chkBonusEnabled As CheckBox)
        Static lastPointsDict As New Dictionary(Of String, Integer)
        Static bonusGivenDict As New Dictionary(Of String, Boolean)

        If Not chkBonusEnabled.Checked Then Return

        ' Initialize tracking
        If Not lastPointsDict.ContainsKey(address) Then
            lastPointsDict(address) = currentPoints
            bonusGivenDict(address) = False
            Return
        End If

        Dim lastPoints As Integer = lastPointsDict(address)
        Dim bonusGiven As Boolean = bonusGivenDict(address)
        Dim actualGain As Integer = currentPoints - lastPoints

        If actualGain = expectedGain AndAlso Not bonusGiven Then
            Dim finalPoints As Integer = currentPoints + expectedGain
            BlackOpsObject.Write(address, finalPoints, GetType(Integer))
            Debug.WriteLine($"{playerLabel}: {expectedGain}-point bonus applied. Final = {finalPoints}")
            bonusGivenDict(address) = True
        ElseIf actualGain <> 0 Then
            ' Reset flag if points change by any other amount
            bonusGivenDict(address) = False
        End If

        lastPointsDict(address) = currentPoints
    End Sub

    Private Sub PointsMonitorTimer_Tick(sender As Object, e As EventArgs) Handles PointsMonitorTimer.Tick
        If Not BlackOpsObject.ProcessIsActive Then Exit Sub

        Dim p1Points As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(MemoryAddresses.P1_Points, GetType(Integer)))
        HandleRestoreDeductedPoints(MemoryAddresses.P1_Points, "P1", p1Points, 950, Cbx_P1_FreeMysteryBox)
        HandlePointBonus(MemoryAddresses.P1_Points, "P1", p1Points, 130, Cbx_P1_ExtraStabBonus)

        If Globals.P2_PlayerEnabled Then
            Dim p2Points As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(MemoryAddresses.P2_Points, GetType(Integer)))
            HandleRestoreDeductedPoints(MemoryAddresses.P1_Points, "P2", p1Points, 950, Cbx_P1_FreeMysteryBox)
            HandlePointBonus(MemoryAddresses.P2_Points, "P2", p2Points, 130, Cbx_P2_ExtraStabBonus)
        End If

        If Globals.P3_PlayerEnabled Then
            Dim p3Points As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(MemoryAddresses.P3_Points, GetType(Integer)))
            HandleRestoreDeductedPoints(MemoryAddresses.P3_Points, "P3", p1Points, 950, Cbx_P3_FreeMysteryBox)
            HandlePointBonus(MemoryAddresses.P3_Points, "P3", p3Points, 130, Cbx_P3_ExtraStabBonus)
        End If

        If Globals.P4_PlayerEnabled Then
            Dim p4Points As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(MemoryAddresses.P4_Points, GetType(Integer)))
            HandleRestoreDeductedPoints(MemoryAddresses.P4_Points, "P4", p1Points, 950, Cbx_P4_FreeMysteryBox)
            HandlePointBonus(MemoryAddresses.P4_Points, "P4", p4Points, 130, Cbx_P4_ExtraStabBonus)
        End If

    End Sub

#End Region ' Points Monitior

#Region "Location Monitor"

    Private Sub LocationMonitorTimer_Tick(sender As Object, e As EventArgs) Handles LocationMonitorTimer.Tick

        If BlackOpsObject.ProcessIsActive Then
            ' Always read P1 location
            Dim P1_LocationVecF As Vector3F = ReadPlayerLocationVector3F(BlackOpsObject._Module(MemoryAddresses.P1_LocationCoords))
            Tbx_P1_CurrentPos.Text = $"""X"": {P1_LocationVecF.X}, ""Y"": {P1_LocationVecF.Y}, ""Z"": {P1_LocationVecF.Z}"

            ' P2
            If Globals.P2_PlayerEnabled Then
                Dim P2_LocationVecF As Vector3F = ReadPlayerLocationVector3F(BlackOpsObject._Module(MemoryAddresses.P2_LocationCoords))
                Tbx_P2_CurrentPos.Text = $"""X"": {P2_LocationVecF.X}, ""Y"": {P2_LocationVecF.Y}, ""Z"": {P2_LocationVecF.Z}"

                ' Lock Player Location
                If Cbx_P2_LockPlayerLocation.Checked AndAlso IsValidPosition(P2_StoredPlayerPos) Then
                    WritePlayerLocationVector3F(BlackOpsObject._Module(MemoryAddresses.P2_LocationCoords), P2_StoredPlayerPos)
                End If
            Else
                Tbx_P2_CurrentPos.Text = "No Position"
            End If

            ' P3
            If Globals.P3_PlayerEnabled Then
                Dim P3_LocationVecF As Vector3F = ReadPlayerLocationVector3F(BlackOpsObject._Module(MemoryAddresses.P3_LocationCoords))
                Tbx_P3_CurrentPos.Text = $"""X"": {P3_LocationVecF.X}, ""Y"": {P3_LocationVecF.Y}, ""Z"": {P3_LocationVecF.Z}"

                ' Lock Player Location
                If Cbx_P3_LockPlayerLocation.Checked AndAlso IsValidPosition(P3_StoredPlayerPos) Then
                    WritePlayerLocationVector3F(BlackOpsObject._Module(MemoryAddresses.P3_LocationCoords), P3_StoredPlayerPos)
                End If
            Else
                Tbx_P3_CurrentPos.Text = "No Position"
            End If

            ' P4
            If Globals.P4_PlayerEnabled Then
                Dim P4_LocationVecF As Vector3F = ReadPlayerLocationVector3F(BlackOpsObject._Module(MemoryAddresses.P4_LocationCoords))
                Tbx_P4_CurrentPos.Text = $"""X"": {P4_LocationVecF.X}, ""Y"": {P4_LocationVecF.Y}, ""Z"": {P4_LocationVecF.Z}"

                ' Lock Player Location
                If Cbx_P4_LockPlayerLocation.Checked AndAlso IsValidPosition(P4_StoredPlayerPos) Then
                    WritePlayerLocationVector3F(BlackOpsObject._Module(MemoryAddresses.P4_LocationCoords), P4_StoredPlayerPos)
                End If
            Else
                Tbx_P4_CurrentPos.Text = "No Position"
            End If
        End If

    End Sub

#End Region ' Location Monitor


#Region "Custom Event Handlers"


    Private Sub HandleProcessStarted(sender As Object, e As EventArgs)

        ' Ensure this code runs on the UI thread
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(Sub() HandleProcessStopped(sender, e)))
            Return
        End If

        Me.Text = "Mason's Black Ops Zombies [BlackOps.exe hooked...]"

    End Sub


    Private Sub HandleProcessStopped(sender As Object, e As EventArgs)
        ' Ensure this code runs on the UI thread
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(Sub() HandleProcessStopped(sender, e)))
            Return
        End If

        Me.Text = "Mason's Black Ops Zombies [Looking for BlackOps.exe...]"

        ' Your reset code here
        Utilities.ResetControlsToDefaultsRecursive(GroupBox_P1_ToggleValues)
        Utilities.ResetControlsToDefaultsRecursive(GroupBox_P1_GodMode)
        Utilities.ResetControlsToDefaultsRecursive(GroupBox_P1_CoOp)
        Utilities.ResetControlsToDefaultsRecursive(GroupBox_P1_PlayerData)
        Utilities.ResetControlsToDefaultsRecursive(GroupBox_P1_SetValues)
        Utilities.ResetControlsToDefaultsRecursive(GroupBox_P1_Teleport)
        Utilities.ResetControlsToDefaultsRecursive(GroupBox_P1_GiveWeapons)
        Utilities.ResetControlsToDefaultsRecursive(GroupBox_P1_OtherFlags)

        Utilities.ResetControlsToDefaultsRecursive(GroupBox_P1_OtherFlags)

        Globals.EnableOnlineCheats = False

        Utilities.ResetControlToDefault(GroupBox_P2)
        Utilities.ResetControlToDefault(GroupBox_P3)
        Utilities.ResetControlToDefault(GroupBox_P4)

        GroupBox_P2.Text = "P2: "
        GroupBox_P3.Text = "P3: "
        GroupBox_P4.Text = "P4: "
    End Sub

#End Region ' Custom Event Handlers


#Region "General Window Controls"

    Private Sub BlackOpsTrainer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        LocationMonitorTimer.Stop()
        PointsMonitorTimer.Stop()
        HealthMonitorTimer.Stop()
    End Sub

    Private Sub Cbx_AlwaysOnTop_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_AlwaysOnTop.CheckedChanged
        Me.TopMost = Cbx_AlwaysOnTop.Checked
    End Sub

    Private Sub Combx_CurrentMap_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combx_CurrentMap.SelectedIndexChanged
        Dim selectedMap As String = Combx_CurrentMap.SelectedItem?.ToString()

        ' Clear previous locations        
        Combx_P1_TpLocations.Items.Clear()
        Combx_P2_TpLocations.Items.Clear()
        Combx_P3_TpLocations.Items.Clear()
        Combx_P4_TpLocations.Items.Clear()

        Combx_SpecialWeapons.Items.Clear()
        Combx_RegularWeapons.Items.Clear()
        Combx_UpgradedWeapons.Items.Clear()
        Combx_WonderWeapons.Items.Clear()
        Combx_TacticalWeapons.Items.Clear()

        If Not String.IsNullOrEmpty(selectedMap) Then

            ' Check if the map exists
            If LocationDictionary.ContainsKey(selectedMap) Then
                ' Save selected map
                Utilities.RegistrySaveString("LastSelectedMap", selectedMap)

                ' Add the full LocationEntry objects
                For Each location As LocationEntry In LocationDictionary(selectedMap)
                    Combx_P1_TpLocations.Items.Add(location)
                    Combx_P2_TpLocations.Items.Add(location)
                    Combx_P3_TpLocations.Items.Add(location)
                    Combx_P4_TpLocations.Items.Add(location)
                Next
            End If

            PopulateWeaponComboBox(Combx_SpecialWeapons, selectedMap, "SpecialWeapons")
            PopulateWeaponComboBox(Combx_RegularWeapons, selectedMap, "RegularWeapons")
            PopulateWeaponComboBox(Combx_UpgradedWeapons, selectedMap, "UpgradedWeapons")
            PopulateWeaponComboBox(Combx_WonderWeapons, selectedMap, "WonderWeapons")
            PopulateWeaponComboBox(Combx_TacticalWeapons, selectedMap, "TacticalWeapons")
        End If

    End Sub

    Private Sub Btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Close.Click
        Application.Exit()
    End Sub

    Private Sub Btn_CommandConsole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_CommandConsole.Click
        CommandCentre.TopMost = Cbx_AlwaysOnTop.Checked
        CommandCentre.Show()
    End Sub

    Private Sub Cbx_EnabledCoOpMode_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_EnabledCoOpMode.CheckedChanged
        If isLoading Then Return ' Ignore events during loading

        If Cbx_EnabledCoOpMode.Checked Then
            Me.Size = New Size(1600, 900)
        Else
            Me.Size = New Size(FormSizeWithoutCoOpMode, 900)
        End If
        Utilities.RegistrySaveBoolean("EnabledCoOpMode", Cbx_EnabledCoOpMode.Checked)
    End Sub

    Private Sub Btn_ActionChangeLevel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Btn_ActionChangeLevel.Click
        OptionSettings.TopMost = Cbx_AlwaysOnTop.Checked
        OptionSettings.Show()
    End Sub

    Private Sub Tbx_P1_CurrentPos_Click(sender As Object, e As EventArgs) Handles Tbx_P1_CurrentPos.Click
        If Not String.IsNullOrWhiteSpace(Tbx_P1_CurrentPos.Text) Then
            Clipboard.SetText(Tbx_P1_CurrentPos.Text)
        End If
    End Sub

    Private Sub Tbx_P2_CurrentPos_Click(sender As Object, e As EventArgs) Handles Tbx_P2_CurrentPos.Click
        If Not String.IsNullOrWhiteSpace(Tbx_P2_CurrentPos.Text) Then
            Clipboard.SetText(Tbx_P2_CurrentPos.Text)
        End If
    End Sub

    Private Sub Tbx_P3_CurrentPos_Click(sender As Object, e As EventArgs) Handles Tbx_P3_CurrentPos.Click
        If Not String.IsNullOrWhiteSpace(Tbx_P3_CurrentPos.Text) Then
            Clipboard.SetText(Tbx_P3_CurrentPos.Text)
        End If
    End Sub

    Private Sub Tbx_P4_CurrentPos_Click(sender As Object, e As EventArgs) Handles Tbx_P4_CurrentPos.Click
        If Not String.IsNullOrWhiteSpace(Tbx_P4_CurrentPos.Text) Then
            Clipboard.SetText(Tbx_P4_CurrentPos.Text)
        End If
    End Sub

#End Region ' General Window Controls

#End Region ' Main

#Region "Teleport Actions"

    ''' <summary>
    ''' Handles location selection from a ComboBox and logs the selected teleport location details for the specified player.
    ''' </summary>
    ''' <param name="comboBox">The <see cref="ComboBox"/> control containing <see cref="LocationEntry"/> items.</param>
    ''' <param name="playerName">The name or identifier of the player associated with the selection (used for logging).</param>
    ''' <remarks>
    ''' Logs the name and coordinates of the selected location if a valid <see cref="LocationEntry"/> is selected.
    ''' Otherwise, logs that the selected location is invalid.
    ''' </remarks>
    Private Sub HandleTpLocationSelection(comboBox As ComboBox, playerName As String)
        Dim selectedLocation As LocationEntry = TryCast(comboBox.SelectedItem, LocationEntry)

        If selectedLocation IsNot Nothing Then
            Debug.WriteLine($"Selected {comboBox.Name}: {selectedLocation.Name}, X={selectedLocation.X}, Y={selectedLocation.Y}, Z={selectedLocation.Z}")
        Else
            Debug.WriteLine($"{playerName}: Selected Location Invalid")
        End If
    End Sub

    Private Sub Combx_P1_TpLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combx_P1_TpLocations.SelectedIndexChanged
        HandleTpLocationSelection(Combx_P1_TpLocations, "P1")
    End Sub

    Private Sub Combx_P2_TpLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combx_P2_TpLocations.SelectedIndexChanged
        Cbx_P2_LockPlayerLocation.Checked = False
        HandleTpLocationSelection(Combx_P2_TpLocations, "P2")
    End Sub

    Private Sub Combx_P3_TpLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combx_P3_TpLocations.SelectedIndexChanged
        Cbx_P3_LockPlayerLocation.Checked = False
        HandleTpLocationSelection(Combx_P3_TpLocations, "P3")
    End Sub

    Private Sub Combx_P4_TpLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combx_P4_TpLocations.SelectedIndexChanged
        Cbx_P4_LockPlayerLocation.Checked = False
        HandleTpLocationSelection(Combx_P4_TpLocations, "P4")
    End Sub


    ''' <summary>
    ''' Writes a <see cref="Vector3F"/> position to the specified player's memory location, effectively teleporting them.
    ''' </summary>
    ''' <param name="sourceLocationAddress">The memory address representing the player's location.</param>
    ''' <param name="destinationVec">The <see cref="Vector3F"/> structure containing the destination coordinates.</param>
    ''' <remarks>
    ''' This method directly writes the vector to memory using the BlackOpsObject module wrapper.
    ''' </remarks>
    Private Sub TeleportPlayerToLocation(sourceLocationAddress As String, destinationVec As Vector3F)
        WritePlayerLocationVector3F(BlackOpsObject._Module(sourceLocationAddress), destinationVec)
    End Sub

    ''' <summary>
    ''' Teleports a player to a predefined location selected from a ComboBox.
    ''' </summary>
    ''' <param name="comboBox">The <see cref="ComboBox"/> containing <see cref="LocationEntry"/> items representing teleport destinations.</param>
    ''' <param name="locationAddress">The memory address representing the player's location to be updated.</param>
    ''' <param name="callerName">
    ''' Optional. The name of the calling method for debugging purposes. Defaults to <c>"undefined_method"</c>.
    ''' </param>
    ''' <remarks>
    ''' If a valid <see cref="LocationEntry"/> is selected in the ComboBox, the corresponding coordinates are written to memory.
    ''' Outputs debug information with the name and coordinates of the selected location.
    ''' </remarks>
    Private Sub HandleTeleportToDefinedLocation(comboBox As ComboBox, locationAddress As String, Optional callerName As String = "undefined_method")
        Dim selectedLocation As LocationEntry = TryCast(comboBox.SelectedItem, LocationEntry)

        If selectedLocation IsNot Nothing Then
            TeleportPlayerToLocation(locationAddress, New Vector3F(selectedLocation.X, selectedLocation.Y, selectedLocation.Z))

            Debug.WriteLine($"{callerName}: {selectedLocation.Name}, X={selectedLocation.X}, Y={selectedLocation.Y}, Z={selectedLocation.Z}")
        Else
            Debug.WriteLine($"{callerName}: Selected Location Invalid")
        End If
    End Sub

    Private Sub Btn_P1_TpLocation_Action_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P1_TpLocation_Action.Click
        HandleTeleportToDefinedLocation(Combx_P1_TpLocations, MemoryAddresses.P1_LocationCoords, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P2_TpLocation_Action_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P2_TpLocation_Action.Click
        Cbx_P2_LockPlayerLocation.Checked = False
        HandleTeleportToDefinedLocation(Combx_P2_TpLocations, MemoryAddresses.P2_LocationCoords, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P3_TpLocation_Action_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P3_TpLocation_Action.Click
        Cbx_P3_LockPlayerLocation.Checked = False
        HandleTeleportToDefinedLocation(Combx_P3_TpLocations, MemoryAddresses.P3_LocationCoords, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P4_TpLocation_Action_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P4_TpLocation_Action.Click
        Cbx_P4_LockPlayerLocation.Checked = False
        HandleTeleportToDefinedLocation(Combx_P4_TpLocations, MemoryAddresses.P4_LocationCoords, MethodBase.GetCurrentMethod().Name)
    End Sub

    ''' <summary>
    ''' Teleports a player to a custom location based on numeric input fields for X, Y, and Z coordinates.
    ''' </summary>
    ''' <param name="X">The <see cref="NumericUpDown"/> control representing the X-coordinate.</param>
    ''' <param name="Y">The <see cref="NumericUpDown"/> control representing the Y-coordinate.</param>
    ''' <param name="Z">The <see cref="NumericUpDown"/> control representing the Z-coordinate.</param>
    ''' <param name="locationAddress">The memory address of the player’s location to update.</param>
    ''' <param name="callerName">
    ''' Optional. A label used for debug output to indicate the source of the teleport request.
    ''' Defaults to <c>"undefined_method"</c> if not provided.
    ''' </param>
    ''' <remarks>
    ''' Reads the float values from the provided numeric controls and creates a <see cref="Vector3F"/> from them.
    ''' If any of the coordinates is zero (assumed to be an invalid location), the teleport is skipped and logged.
    ''' </remarks>
    Private Sub HandleTeleportToCustomLocation(X As NumericUpDown, Y As NumericUpDown, Z As NumericUpDown, locationAddress As String, Optional callerName As String = "undefined_method")
        Dim xVal As Single = Convert.ToSingle(X.Value)
        Dim yVal As Single = Convert.ToSingle(Y.Value)
        Dim zVal As Single = Convert.ToSingle(Z.Value)

        ' If any coordinate is zero, skip teleport. It's highly unlikely that any valid location contains a zero value.
        If xVal = 0 OrElse yVal = 0 OrElse zVal = 0 Then
            Debug.WriteLine($"{callerName}: Skipped teleport - One or more coordinates are zero (X={xVal}, Y={yVal}, Z={zVal}).")
            Return
        End If

        ' Perform teleport
        TeleportPlayerToLocation(locationAddress, New Vector3F(xVal, yVal, zVal))

        Debug.WriteLine($"{callerName}: Teleported to X={xVal}, Y={yVal}, Z={zVal}")
    End Sub

    Private Sub Btn_P1_CustomTeleport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P1_CustomTeleport.Click
        HandleTeleportToCustomLocation(Nud_P1_CustomTeleport_X, Nud_P1_CustomTeleport_Y, Nud_P1_CustomTeleport_Z, MemoryAddresses.P1_LocationCoords, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P2_CustomTeleport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P2_CustomTeleport.Click
        Cbx_P2_LockPlayerLocation.Checked = False
        HandleTeleportToCustomLocation(Nud_P2_CustomTeleport_X, Nud_P2_CustomTeleport_Y, Nud_P2_CustomTeleport_Z, MemoryAddresses.P2_LocationCoords, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P3_CustomTeleport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P3_CustomTeleport.Click
        Cbx_P3_LockPlayerLocation.Checked = False
        HandleTeleportToCustomLocation(Nud_P3_CustomTeleport_X, Nud_P3_CustomTeleport_Y, Nud_P3_CustomTeleport_Z, MemoryAddresses.P3_LocationCoords, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P4_CustomTeleport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P4_CustomTeleport.Click
        Cbx_P4_LockPlayerLocation.Checked = False
        HandleTeleportToCustomLocation(Nud_P4_CustomTeleport_X, Nud_P4_CustomTeleport_Y, Nud_P4_CustomTeleport_Z, MemoryAddresses.P4_LocationCoords, MethodBase.GetCurrentMethod().Name)
    End Sub


    ''' <summary>
    ''' Teleports a player from a source location to a destination location by copying the vector coordinates.
    ''' </summary>
    ''' <param name="sourceLocationAddress">The memory address of the source player's location (as a string).</param>
    ''' <param name="destinationLocationAddress">The memory address to write the new location to (as a string).</param>
    ''' <param name="actionLabel">
    ''' Optional. A label used for debug logging to identify the teleport action. 
    ''' Defaults to <c>"undefined_action"</c> if not specified.
    ''' </param>
    ''' <remarks>
    ''' Reads the 3D position vector from the source and writes it to the destination. 
    ''' Outputs the teleport coordinates to the debug log.
    ''' </remarks>
    Private Sub TeleportP2P(sourceLocationAddress As String, destinationLocationAddress As String, Optional actionLabel As String = "undefined_action")
        Dim destinationVec As Vector3F = ReadPlayerLocationVector3F(BlackOpsObject._Module(sourceLocationAddress))
        WritePlayerLocationVector3F(BlackOpsObject._Module(destinationLocationAddress), destinationVec)

        Debug.WriteLine($"{actionLabel}: Source={sourceLocationAddress} To={destinationLocationAddress}, X={destinationVec.X}, Y={destinationVec.Y}, Z={destinationVec.Z}")
    End Sub

    ''' <summary>
    ''' Teleports Player 1 to another player's position with an offset.
    ''' </summary>
    ''' <param name="targetLocationAddress">The memory address of the target player's location.</param>
    ''' <param name="offset">A <see cref="Vector3F"/> representing the offset to apply to the target's position.</param>
    ''' <remarks>
    ''' Computes a new position by applying the offset to the target player's current location, 
    ''' and writes this to Player 1's position address. Logs the new coordinates to the debug output.
    ''' </remarks>
    Private Sub TeleportP1ToPlayerWithOffset(targetLocationAddress As String, offset As Vector3F)
        Dim targetVec As Vector3F = ReadPlayerLocationVector3F(BlackOpsObject._Module(targetLocationAddress))
        Dim offsetVec As New Vector3F With {
            .X = targetVec.X + offset.X,
            .Y = targetVec.Y + offset.Y,
            .Z = targetVec.Z + offset.Z
        }

        WritePlayerLocationVector3F(BlackOpsObject._Module(MemoryAddresses.P1_LocationCoords), offsetVec)
        Debug.WriteLine($"TeleportP1ToPlayerWithOffset: Target={targetLocationAddress}, X={offsetVec.X}, Y={offsetVec.Y}, Z={offsetVec.Z}")
    End Sub

    ''' <summary>
    ''' Summons another player to Player 1’s position with an offset.
    ''' </summary>
    ''' <param name="targetLocationAddress">The memory address of the player to be moved.</param>
    ''' <param name="offset">A <see cref="Vector3F"/> offset to apply relative to Player 1's position.</param>
    ''' <remarks>
    ''' Calculates the new position for the target player by adding the offset to Player 1's current position, 
    ''' then writes it to the target player's memory location. Logs the new coordinates to the debug output.
    ''' </remarks>
    Private Sub SummonPlayerToP1WithOffset(targetLocationAddress As String, offset As Vector3F)
        Dim p1Pos As Vector3F = ReadPlayerLocationVector3F(BlackOpsObject._Module(MemoryAddresses.P1_LocationCoords))
        Dim newPos As New Vector3F(p1Pos.X + offset.X, p1Pos.Y + offset.Y, p1Pos.Z + offset.Z)

        WritePlayerLocationVector3F(BlackOpsObject._Module(targetLocationAddress), newPos)

        Debug.WriteLine($"SummonPlayerToP1WithOffset: Target={targetLocationAddress}, X={newPos.X}, Y={newPos.Y}, Z={newPos.Z}")
    End Sub

    Private Sub Btn_P1_ActionSummon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P1_ActionSummon.Click
        Cbx_P2_LockPlayerLocation.Checked = False
        Cbx_P3_LockPlayerLocation.Checked = False
        Cbx_P4_LockPlayerLocation.Checked = False

        SummonPlayerToP1WithOffset(MemoryAddresses.P2_LocationCoords, New Vector3F(PlayerTeleportOffsetValue * -1, 0.0F, 0.0F))
        SummonPlayerToP1WithOffset(MemoryAddresses.P3_LocationCoords, New Vector3F(PlayerTeleportOffsetValue, 0.0F, 0.0F))
        SummonPlayerToP1WithOffset(MemoryAddresses.P4_LocationCoords, New Vector3F(0.0F, PlayerTeleportOffsetValue * -1, 0.0F))
    End Sub

    Private Sub Btn_P2_ActionSummon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P2_ActionSummon.Click
        Cbx_P2_LockPlayerLocation.Checked = False
        SummonPlayerToP1WithOffset(MemoryAddresses.P2_LocationCoords, New Vector3F(PlayerTeleportOffsetValue * -1, 0.0F, 0.0F))
    End Sub

    Private Sub Btn_P3_ActionSummon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P3_ActionSummon.Click
        Cbx_P3_LockPlayerLocation.Checked = False
        SummonPlayerToP1WithOffset(MemoryAddresses.P3_LocationCoords, New Vector3F(PlayerTeleportOffsetValue, 0.0F, 0.0F))
    End Sub

    Private Sub Btn_P4_ActionSummon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P4_ActionSummon.Click
        Cbx_P4_LockPlayerLocation.Checked = False
        SummonPlayerToP1WithOffset(MemoryAddresses.P4_LocationCoords, New Vector3F(0.0F, PlayerTeleportOffsetValue * -1, 0.0F))
    End Sub


    Private Sub Btn_P2_TeleportTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P2_TeleportTo.Click
        Cbx_P2_LockPlayerLocation.Checked = False
        TeleportP1ToPlayerWithOffset(MemoryAddresses.P2_LocationCoords, New Vector3F(PlayerTeleportOffsetValue * -1, 0.0F, 0.0F))
    End Sub

    Private Sub Btn_P3_TeleportTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P3_TeleportTo.Click
        Cbx_P3_LockPlayerLocation.Checked = False
        TeleportP1ToPlayerWithOffset(MemoryAddresses.P3_LocationCoords, New Vector3F(PlayerTeleportOffsetValue, 0.0F, 0.0F))
    End Sub

    Private Sub Btn_P4_TeleportTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P4_TeleportTo.Click
        Cbx_P4_LockPlayerLocation.Checked = False
        TeleportP1ToPlayerWithOffset(MemoryAddresses.P4_LocationCoords, New Vector3F(0.0F, PlayerTeleportOffsetValue * -1, 0.0F))
    End Sub


#End Region ' Teleport Actions

#Region "Value Setters"

    ''' <summary>
    ''' Writes the value from a <see cref="NumericUpDown"/> control to a specified memory address.
    ''' </summary>
    ''' <param name="numericControl">The <see cref="NumericUpDown"/> control containing the integer value to write.</param>
    ''' <param name="memoryAddress">The memory address to write the value to, as a string.</param>
    ''' <param name="callerName">
    ''' Optional. A string identifying the caller for logging/debugging purposes.
    ''' Defaults to <c>"undefined_caller"</c> if not provided.
    ''' </param>
    ''' <remarks>
    ''' Converts the control's value to an integer and writes it to the given memory address using <c>BlackOpsObject.Write</c>.
    ''' Logs the operation to the debug output.
    ''' </remarks>
    Private Sub HandleSetStatValue(numericControl As NumericUpDown, memoryAddress As String, Optional callerName As String = "undefined_caller")
        Dim value As Integer = Convert.ToInt32(numericControl.Value)

        BlackOpsObject.Write(memoryAddress, value, GetType(Integer))

        Debug.WriteLine($"{callerName}: Wrote value {value} to {memoryAddress}")
    End Sub

    ''' <summary>
    ''' Adds points to a player's current point total, ensuring valid input.
    ''' </summary>
    ''' <param name="pointsToAddControl">NumericUpDown control specifying the amount to add.</param>
    ''' <param name="memoryAddress">Memory address of the player's points.</param>
    ''' <param name="callerName">
    ''' Optional. A string identifying the caller for logging/debugging purposes.
    ''' Defaults to <c>"undefined_caller"</c> if not provided.
    ''' </param>
    Private Sub AddPlayerPoints(pointsToAddControl As NumericUpDown, memoryAddress As String, Optional callerName As String = "undefined_caller")
        Dim currentPoints As Integer = Convert.ToInt32(BlackOpsObject.ReadPointer(memoryAddress, GetType(Integer)))
        Dim pointsToAdd As Integer = Convert.ToInt32(pointsToAddControl.Value)

        If pointsToAdd <= 0 Then Exit Sub

        Dim finalPoints As Integer = currentPoints + pointsToAdd
        If finalPoints <= 0 Then Exit Sub

        BlackOpsObject.Write(memoryAddress, finalPoints, GetType(Integer))
        Debug.WriteLine($"{callerName}: ({pointsToAdd} → {currentPoints}) {finalPoints} points applied to = {memoryAddress}")
    End Sub


    ' Add Points
    Private Sub Btn_P1_AddPoints_Click(sender As Object, e As EventArgs) Handles Btn_P1_AddPoints.Click
        AddPlayerPoints(Nud_P1_AddPoints, MemoryAddresses.P1_Points, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P2_AddPoints_Click(sender As Object, e As EventArgs) Handles Btn_P2_AddPoints.Click
        If Globals.P2_PlayerEnabled Then AddPlayerPoints(Nud_P2_AddPoints, MemoryAddresses.P2_Points, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P3_AddPoints_Click(sender As Object, e As EventArgs) Handles Btn_P3_AddPoints.Click
        If Globals.P3_PlayerEnabled Then AddPlayerPoints(Nud_P3_AddPoints, MemoryAddresses.P3_Points, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P4_AddPoints_Click(sender As Object, e As EventArgs) Handles Btn_P4_AddPoints.Click
        If Globals.P4_PlayerEnabled Then AddPlayerPoints(Nud_P4_AddPoints, MemoryAddresses.P4_Points, MethodBase.GetCurrentMethod().Name)
    End Sub


    ' Set Points
    Private Sub Btn_P1_SetPoints_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P1_SetPoints.Click
        HandleSetStatValue(Nud_P1_SetPoints, MemoryAddresses.P1_Points, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P2_SetPoints_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P2_SetPoints.Click
        If Globals.P2_PlayerEnabled Then HandleSetStatValue(Nud_P2_SetPoints, MemoryAddresses.P2_Points, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P3_SetPoints_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P3_SetPoints.Click
        If Globals.P3_PlayerEnabled Then HandleSetStatValue(Nud_P3_SetPoints, MemoryAddresses.P3_Points, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P4_SetPoints_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P4_SetPoints.Click
        If Globals.P4_PlayerEnabled Then HandleSetStatValue(Nud_P4_SetPoints, MemoryAddresses.P4_Points, MethodBase.GetCurrentMethod().Name)
    End Sub


    ' Set Headshots
    Private Sub Btn_P1_SetHeadShots_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P1_SetHeadShots.Click
        HandleSetStatValue(Nud_P1_SetHeadShots, MemoryAddresses.P1_HeadShots, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P2_SetHeadShots_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P2_SetHeadShots.Click
        If Globals.P2_PlayerEnabled Then HandleSetStatValue(Nud_P2_SetHeadShots, MemoryAddresses.P2_HeadShots, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P3_SetHeadShots_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P3_SetHeadShots.Click
        If Globals.P3_PlayerEnabled Then HandleSetStatValue(Nud_P3_SetHeadShots, MemoryAddresses.P3_HeadShots, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P4_SetHeadShots_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P4_SetHeadShots.Click
        If Globals.P4_PlayerEnabled Then HandleSetStatValue(Nud_P4_SetHeadShots, MemoryAddresses.P4_HeadShots, MethodBase.GetCurrentMethod().Name)
    End Sub


    ' Set Kills
    Private Sub Btn_P1_SetKills_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P1_SetKills.Click
        HandleSetStatValue(Nud_P1_SetKills, MemoryAddresses.P1_Kills, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P2_SetKills_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P2_SetKills.Click
        If Globals.P2_PlayerEnabled Then HandleSetStatValue(Nud_P2_SetKills, MemoryAddresses.P2_Kills, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P3_SetKills_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P3_SetKills.Click
        If Globals.P3_PlayerEnabled Then HandleSetStatValue(Nud_P3_SetKills, MemoryAddresses.P3_Kills, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P4_SetKills_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P4_SetKills.Click
        If Globals.P4_PlayerEnabled Then HandleSetStatValue(Nud_P4_SetKills, MemoryAddresses.P4_Kills, MethodBase.GetCurrentMethod().Name)
    End Sub


    ' Set Revives
    Private Sub Btn_P1_SetRevives_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P1_SetRevives.Click
        HandleSetStatValue(Nud_P1_SetRevives, MemoryAddresses.P1_Revives, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P2_SetRevives_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P2_SetRevives.Click
        If Globals.P2_PlayerEnabled Then HandleSetStatValue(Nud_P2_SetRevives, MemoryAddresses.P2_Revives, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P3_SetRevives_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P3_SetRevives.Click
        If Globals.P3_PlayerEnabled Then HandleSetStatValue(Nud_P3_SetRevives, MemoryAddresses.P3_Revives, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P4_SetRevives_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P4_SetRevives.Click
        If Globals.P4_PlayerEnabled Then HandleSetStatValue(Nud_P4_SetRevives, MemoryAddresses.P4_Revives, MethodBase.GetCurrentMethod().Name)
    End Sub


    'Set Downs
    Private Sub Btn_P1_SetDowns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P1_SetDowns.Click
        HandleSetStatValue(Nud_P1_SetDowns, MemoryAddresses.P1_Downs, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P2_SetDowns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P2_SetDowns.Click
        If Globals.P2_PlayerEnabled Then HandleSetStatValue(Nud_P2_SetDowns, MemoryAddresses.P2_Downs, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P3_SetDowns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P3_SetDowns.Click
        If Globals.P3_PlayerEnabled Then HandleSetStatValue(Nud_P3_SetDowns, MemoryAddresses.P3_Downs, MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Btn_P4_SetDowns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P4_SetDowns.Click
        If Globals.P4_PlayerEnabled Then HandleSetStatValue(Nud_P4_SetDowns, MemoryAddresses.P4_Downs, MethodBase.GetCurrentMethod().Name)
    End Sub


    ''' <summary>
    ''' Updates one or more memory addresses with a new player name string.
    ''' </summary>
    ''' <param name="playerNameText">The player name text to write. Must not be null or whitespace.</param>
    ''' <param name="callerName">
    ''' A string identifying the calling method for debugging/logging purposes.
    ''' If <c>Nothing</c>, the method name is automatically retrieved.
    ''' </param>
    ''' <param name="addresses">A <c>ParamArray</c> of memory addresses to write the name to.</param>
    ''' <remarks>
    ''' Displays a warning message and exits if the player name is null or empty.
    ''' Writes the string to each given memory address and logs the operation in the debug console.
    ''' </remarks>
    Private Sub HandlePlayerNameChange(playerNameText As String, callerName As String, ParamArray addresses() As IntPtr)
        If String.IsNullOrWhiteSpace(playerNameText) Then
            MessageBox.Show("Player name cannot be empty. Please enter a valid name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If callerName Is Nothing Then callerName = MethodBase.GetCurrentMethod().Name

        For Each address In addresses
            BlackOpsObject.WriteString(address, playerNameText)
            Debug.WriteLine($"{callerName}: Wrote '{playerNameText}' to address 0x{address.ToInt64():X}")
        Next
    End Sub


    Private Sub Btn_P1_NameChange_Click(sender As Object, e As EventArgs) Handles Btn_P1_NameChange.Click
        HandlePlayerNameChange(Tbx_P1_NameChange.Text, MethodBase.GetCurrentMethod().Name, MemoryAddresses.P1_PlayerNameA, MemoryAddresses.P1_PlayerNameB, MemoryAddresses.P1_PlayerNameC)
    End Sub

    Private Sub Btn_P2_NameChange_Click(sender As Object, e As EventArgs) Handles Btn_P2_NameChange.Click
        If Globals.P2_PlayerEnabled Then HandlePlayerNameChange(Tbx_P2_NameChange.Text, MethodBase.GetCurrentMethod().Name, MemoryAddresses.P2_PlayerNameA, MemoryAddresses.P2_PlayerNameB, MemoryAddresses.P2_PlayerNameC)
    End Sub

    Private Sub Btn_P3_NameChange_Click(sender As Object, e As EventArgs) Handles Btn_P3_NameChange.Click
        If Globals.P3_PlayerEnabled Then HandlePlayerNameChange(Tbx_P3_NameChange.Text, MethodBase.GetCurrentMethod().Name, MemoryAddresses.P3_PlayerNameA, MemoryAddresses.P3_PlayerNameB, MemoryAddresses.P3_PlayerNameC)
    End Sub

    Private Sub Btn_P4_NameChange_Click(sender As Object, e As EventArgs) Handles Btn_P4_NameChange.Click
        If Globals.P4_PlayerEnabled Then HandlePlayerNameChange(Tbx_P4_NameChange.Text, MethodBase.GetCurrentMethod().Name, MemoryAddresses.P4_PlayerNameA, MemoryAddresses.P4_PlayerNameB, MemoryAddresses.P4_PlayerNameC, MemoryAddresses.P4_PlayerNameD)
    End Sub

    Private Sub Btn_P1_SetSpeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P1_SetSpeed.Click
        BlackOpsObject.Write(MemoryAddresses.P1_SetSpeed, Nud_P1_SetSpeed.Value, GetType(Integer))
        Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: Set speed to {Nud_P1_SetSpeed.Value} at address {MemoryAddresses.P1_SetSpeed}")
    End Sub

#End Region ' Value Setters

#Region "Checkbox Setters"

    ' === Checkboxes with Commands ===
    Private Sub Cbx_P1_BoxNeverMoves_CheckedChanged(ByVal sender As System.Object, e As System.EventArgs) Handles Cbx_P1_BoxNeverMoves.CheckedChanged
        RunConditionalCommand(Cbx_P1_BoxNeverMoves, "magic_chest_movable 0", "magic_chest_movable 1", MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Cbx_EasySamsGame_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_EasySamsGame.CheckedChanged
        RunConditionalCommand(Cbx_EasySamsGame, "scr_debug_ss 1", "scr_debug_ss 0", MethodBase.GetCurrentMethod().Name)
    End Sub


    ' === Other Checkbox ===
    Private Sub Cbx_ForceHost_CheckedChanged(ByVal sender As System.Object, e As System.EventArgs) Handles Cbx_ForceHost.CheckedChanged
        Dim value As Integer = If(Cbx_ForceHost.Checked, 0, 6000)
        BlackOpsObject.Write(MemoryAddresses.ForceHost, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_ForceHost)
    End Sub

    Private Sub Cbx_P1_FastMotion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_FastMotion.CheckedChanged
        Dim value As Integer = If(Cbx_P1_FastMotion.Checked, 1073741824, 1065353216)
        If Cbx_P1_FastMotion.Checked Then
            Cbx_P1_SlowMotion.Checked = False
        End If
        BlackOpsObject.Write(MemoryAddresses.P1_MotionSpeed, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P1_FastMotion)
    End Sub

    Private Sub Cbx_P1_SlowMotion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_SlowMotion.CheckedChanged
        Dim value As Integer = If(Cbx_P1_SlowMotion.Checked, 1036831949, 1065353216)
        If Cbx_P1_SlowMotion.Checked Then
            Cbx_P1_FastMotion.Checked = False
        End If
        BlackOpsObject.Write(MemoryAddresses.P1_MotionSpeed, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P1_SlowMotion)
    End Sub

    Private Sub Cbx_EnableOnlineCheats_CheckedChanged(ByVal sender As System.Object, e As System.EventArgs) Handles Cbx_EnableOnlineCheats.CheckedChanged
        EnableOnlineCheats = Cbx_EnableOnlineCheats.Checked
        UpdateCheckBoxForeColor(Cbx_EnableOnlineCheats)
    End Sub

    Private Sub Cbx_P1_ExtraMeleeRange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_ExtraMeleeRange.CheckedChanged
        Dim value As Single = If(Cbx_P1_ExtraMeleeRange.Checked, 128.0F, 64.0F)
        BlackOpsObject.Write(MemoryAddresses.P1_ExtraMeleeRange, value, GetType(Single))
        UpdateCheckBoxForeColor(Cbx_P1_ExtraMeleeRange)
    End Sub

    Private Sub Cbx_P1_LowGravity_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_LowGravity.CheckedChanged
        Dim value As Integer = If(Cbx_P1_LowGravity.Checked, 1092616192, 1145569280)
        BlackOpsObject.Write(MemoryAddresses.P1_LowGravity, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P1_LowGravity)
    End Sub

    Private Sub Cbx_P1_ThirdPersonView_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_ThirdPersonView.CheckedChanged
        Dim value As Integer = If(Cbx_P1_ThirdPersonView.Checked, 1, 0)
        BlackOpsObject.Write(MemoryAddresses.P1_ThirdPersonView, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P1_ThirdPersonView)
    End Sub

    Private Sub Cbx_P1_DeadFloat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_DeadFloat.CheckedChanged
        Dim value As Single = If(Cbx_P1_DeadFloat.Checked, 22.0F, -800.0F)
        BlackOpsObject.Write(MemoryAddresses.P1_DeadFloat, value, GetType(Single))
        UpdateCheckBoxForeColor(Cbx_P1_DeadFloat)
    End Sub


    ' === God Mode Checkbox ===
    Private Sub Cbx_P1_GodMode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_GodMode.CheckedChanged
        If Cbx_P1_GodMode.Checked Then
            Cbx_P1_GodHealth.Checked = False
            Cbx_P1_JesusHealth.Checked = False
            Cbx_P1_GodHealth.Enabled = False
            Cbx_P1_JesusHealth.Enabled = False
        Else
            Cbx_P1_GodHealth.Enabled = True
            Cbx_P1_JesusHealth.Enabled = True
        End If

        Dim value As Integer = If(Cbx_P1_GodMode.Checked, 2081, 2080)
        BlackOpsObject.Write(MemoryAddresses.P1_GodMode, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P1_GodMode)
    End Sub

    Private Sub Cbx_GodMode_AllPlayers_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_GodMode_AllPlayers.CheckedChanged
        If Cbx_GodMode_AllPlayers.Checked Then

            Cbx_P1_GodMode.Checked = False

            Cbx_P1_GodHealth.Checked = False
            Cbx_P2_GodHealth.Checked = False
            Cbx_P3_GodHealth.Checked = False
            Cbx_P4_GodHealth.Checked = False

            Cbx_P1_JesusHealth.Checked = False
            Cbx_P2_JesusHealth.Checked = False
            Cbx_P3_JesusHealth.Checked = False
            Cbx_P4_JesusHealth.Checked = False

            Cbx_P1_GodMode.Enabled = False

            Cbx_P1_GodHealth.Enabled = False
            Cbx_P2_GodHealth.Enabled = False
            Cbx_P3_GodHealth.Enabled = False
            Cbx_P4_GodHealth.Enabled = False

            Cbx_P1_JesusHealth.Enabled = False
            Cbx_P2_JesusHealth.Enabled = False
            Cbx_P3_JesusHealth.Enabled = False
            Cbx_P4_JesusHealth.Enabled = False
        Else
            Cbx_P1_GodMode.Enabled = True

            Cbx_P1_GodHealth.Enabled = True
            Cbx_P2_GodHealth.Enabled = True
            Cbx_P3_GodHealth.Enabled = True
            Cbx_P4_GodHealth.Enabled = True

            Cbx_P1_JesusHealth.Enabled = True
            Cbx_P2_JesusHealth.Enabled = True
            Cbx_P3_JesusHealth.Enabled = True
            Cbx_P4_JesusHealth.Enabled = True
        End If

        BlackOpsObject.Patch("7DADD0", "909090909090", "898584010000")
        UpdateCheckBoxForeColor(Cbx_GodMode_AllPlayers)
    End Sub

    Dim BaseCharacterHealthForJesusGodHealth As Integer = 250 ' TODO: make configurable

    ' === God Health Checkboxes ===
    Private Sub Cbx_P1_GodHealth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_GodHealth.CheckedChanged
        If Cbx_P1_GodHealth.Checked Then
            Cbx_P1_GodMode.Checked = False
            Cbx_P1_JesusHealth.Checked = False
        Else
            BlackOpsObject.Write(MemoryAddresses.P1_PlayerHealthA, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_PlayerHealthB, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P1_GodHealth)
    End Sub

    Private Sub Cbx_P2_GodHealth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P2_GodHealth.CheckedChanged
        If Cbx_P2_GodHealth.Checked Then
            Cbx_P2_JesusHealth.Checked = False
        Else
            BlackOpsObject.Write(MemoryAddresses.P2_PlayerHealthA, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_PlayerHealthB, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P2_GodHealth)
    End Sub

    Private Sub Cbx_P3_GodHealth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P3_GodHealth.CheckedChanged
        If Cbx_P3_GodHealth.Checked Then
            Cbx_P3_JesusHealth.Checked = False
        Else
            BlackOpsObject.Write(MemoryAddresses.P3_PlayerHealthA, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_PlayerHealthB, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P3_GodHealth)
    End Sub

    Private Sub Cbx_P4_GodHealth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P4_GodHealth.CheckedChanged
        If Cbx_P4_GodHealth.Checked Then
            Cbx_P4_JesusHealth.Checked = False
        Else
            BlackOpsObject.Write(MemoryAddresses.P4_PlayerHealthA, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_PlayerHealthB, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P4_GodHealth)
    End Sub

    ' === Jesus Health Checkboxes ===
    Private Sub Cbx_P1_JesusHealth_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P1_JesusHealth.CheckedChanged
        If Cbx_P1_JesusHealth.Checked Then
            Cbx_P1_GodHealth.Checked = False
        Else
            BlackOpsObject.Write(MemoryAddresses.P1_PlayerHealthA, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_PlayerHealthB, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P1_JesusHealth)
    End Sub

    Private Sub Cbx_P2_JesusHealth_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P2_JesusHealth.CheckedChanged
        If Cbx_P2_JesusHealth.Checked Then
            Cbx_P2_GodHealth.Checked = False
        Else
            BlackOpsObject.Write(MemoryAddresses.P2_PlayerHealthA, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_PlayerHealthB, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P2_JesusHealth)
    End Sub

    Private Sub Cbx_P3_JesusHealth_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P3_JesusHealth.CheckedChanged
        If Cbx_P3_JesusHealth.Checked Then
            Cbx_P3_GodHealth.Checked = False
        Else
            BlackOpsObject.Write(MemoryAddresses.P3_PlayerHealthA, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_PlayerHealthB, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P3_JesusHealth)
    End Sub

    Private Sub Cbx_P4_JesusHealth_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P4_JesusHealth.CheckedChanged
        If Cbx_P4_JesusHealth.Checked Then
            Cbx_P4_GodHealth.Checked = False
        Else
            BlackOpsObject.Write(MemoryAddresses.P4_PlayerHealthA, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_PlayerHealthB, BaseCharacterHealthForJesusGodHealth, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P4_JesusHealth)
    End Sub


    ' === Extra Stab Bonus Checkboxes ===
    Private Sub Cbx_P1_ExtraStabBonus_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P1_ExtraStabBonus.CheckedChanged
        UpdateCheckBoxForeColor(Cbx_P1_ExtraStabBonus)
    End Sub

    Private Sub Cbx_P2_ExtraStabBonus_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P2_ExtraStabBonus.CheckedChanged
        UpdateCheckBoxForeColor(Cbx_P2_ExtraStabBonus)
    End Sub

    Private Sub Cbx_P3_ExtraStabBonus_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P3_ExtraStabBonus.CheckedChanged
        UpdateCheckBoxForeColor(Cbx_P3_ExtraStabBonus)
    End Sub

    Private Sub Cbx_P4_ExtraStabBonus_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P4_ExtraStabBonus.CheckedChanged
        UpdateCheckBoxForeColor(Cbx_P4_ExtraStabBonus)
    End Sub


    ' === Free Mystery Box Checkboxes ===
    Private Sub Cbx_P1_FreeMysteryBox_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P1_FreeMysteryBox.CheckedChanged
        UpdateCheckBoxForeColor(Cbx_P1_FreeMysteryBox)
    End Sub

    Private Sub Cbx_P2_FreeMysteryBox_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P2_FreeMysteryBox.CheckedChanged
        UpdateCheckBoxForeColor(Cbx_P2_FreeMysteryBox)
    End Sub

    Private Sub Cbx_P3_FreeMysteryBox_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P3_FreeMysteryBox.CheckedChanged
        UpdateCheckBoxForeColor(Cbx_P3_FreeMysteryBox)
    End Sub

    Private Sub Cbx_P4_FreeMysteryBox_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P4_FreeMysteryBox.CheckedChanged
        UpdateCheckBoxForeColor(Cbx_P4_FreeMysteryBox)
    End Sub


    ' === No Target Checkboxes ===
    Private Sub Cbx_P1_NoTarget_CheckedChanged(ByVal sender As System.Object, e As System.EventArgs) Handles Cbx_P1_NoTarget.CheckedChanged
        Dim value As Integer = If(Cbx_P1_NoTarget.Checked, 131106, 131074)
        BlackOpsObject.Write(MemoryAddresses.P1_NoTarget, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P1_NoTarget)
    End Sub

    Private Sub Cbx_P2_NoTarget_CheckedChanged(ByVal sender As System.Object, e As System.EventArgs) Handles Cbx_P2_NoTarget.CheckedChanged
        Dim value As Integer = If(Cbx_P2_NoTarget.Checked, 131106, 131074)
        BlackOpsObject.Write(MemoryAddresses.P2_NoTarget, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P2_NoTarget)
    End Sub

    Private Sub Cbx_P3_NoTarget_CheckedChanged(ByVal sender As System.Object, e As System.EventArgs) Handles Cbx_P3_NoTarget.CheckedChanged
        Dim value As Integer = If(Cbx_P3_NoTarget.Checked, 131106, 131074)
        BlackOpsObject.Write(MemoryAddresses.P3_NoTarget, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P3_NoTarget)
    End Sub

    Private Sub Cbx_P4_NoTarget_CheckedChanged(ByVal sender As System.Object, e As System.EventArgs) Handles Cbx_P4_NoTarget.CheckedChanged
        Dim value As Integer = If(Cbx_P4_NoTarget.Checked, 131106, 131074)
        BlackOpsObject.Write(MemoryAddresses.P4_NoTarget, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P4_NoTarget)
    End Sub


    ' === No Clip Checkboxes ===
    Private Sub Cbx_P1_NoClip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_NoClip.CheckedChanged
        Dim value As Integer = If(Cbx_P1_NoClip.Checked, 1, 0)
        BlackOpsObject.Write(MemoryAddresses.P1_NoClip, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P1_NoClip)
    End Sub

    Private Sub Cbx_P2_NoClip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P2_NoClip.CheckedChanged
        Dim value As Integer = If(Cbx_P2_NoClip.Checked, 1, 0)
        BlackOpsObject.Write(MemoryAddresses.P2_NoClip, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P2_NoClip)
    End Sub

    Private Sub Cbx_P3_NoClip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P3_NoClip.CheckedChanged
        Dim value As Integer = If(Cbx_P3_NoClip.Checked, 1, 0)
        BlackOpsObject.Write(MemoryAddresses.P3_NoClip, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P3_NoClip)
    End Sub

    Private Sub Cbx_P4_NoClip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P4_NoClip.CheckedChanged
        Dim value As Integer = If(Cbx_P4_NoClip.Checked, 1, 0)
        BlackOpsObject.Write(MemoryAddresses.P4_NoClip, value, GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P4_NoClip)
    End Sub


    ' === Target Head Only Checkboxes ===
    Private Sub Cbx_P1_TargetHeadOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_TargetHeadOnly.CheckedChanged
        UpdateCheckBoxForeColor(Cbx_P1_TargetHeadOnly)
    End Sub

    Private Sub Cbx_P2_TargetHeadOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P2_TargetHeadOnly.CheckedChanged
        UpdateCheckBoxForeColor(Cbx_P2_TargetHeadOnly)
    End Sub

    Private Sub Cbx_P4_TargetHeadOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P4_TargetHeadOnly.CheckedChanged
        UpdateCheckBoxForeColor(Cbx_P4_TargetHeadOnly)
    End Sub

    Private Sub Cbx_P3_TargetHeadOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P3_TargetHeadOnly.CheckedChanged
        UpdateCheckBoxForeColor(Cbx_P3_TargetHeadOnly)
    End Sub


    ' === Infinite Ammo Checkboxes ===
    Private Sub Cbx_P1_InfiniteAmmo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_InfiniteAmmo.CheckedChanged
        If Not Cbx_P1_InfiniteAmmo.Checked Then
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoA, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoB, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoC, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoD, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoE, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoF, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoG, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P1_InfiniteAmmoH, 200, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P1_InfiniteAmmo)
    End Sub

    Private Sub Cbx_P2_InfiniteAmmo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P2_InfiniteAmmo.CheckedChanged
        If Not Cbx_P2_InfiniteAmmo.Checked Then
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoA, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoB, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoC, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoD, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoE, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoF, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoG, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_InfiniteAmmoH, 200, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P2_InfiniteAmmo)
    End Sub

    Private Sub Cbx_P3_InfiniteAmmo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P3_InfiniteAmmo.CheckedChanged
        If Not Cbx_P3_InfiniteAmmo.Checked Then
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoA, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoB, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoC, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoD, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoE, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoF, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoG, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_InfiniteAmmoH, 200, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P3_InfiniteAmmo)
    End Sub

    Private Sub Cbx_P4_InfiniteAmmo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P4_InfiniteAmmo.CheckedChanged
        If Not Cbx_P4_InfiniteAmmo.Checked Then
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoA, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoB, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoC, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoD, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoE, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoF, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoG, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoH, 200, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_InfiniteAmmoI, 200, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P4_InfiniteAmmo)
    End Sub


    ' === Set Zero Points Checkboxes ===
    Private Sub Cbx_P2_SetZeroPoints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P2_SetZeroPoints.CheckedChanged
        If Not Cbx_P2_SetZeroPoints.Checked Then
            BlackOpsObject.Write(MemoryAddresses.P2_Points, 5, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_HeadShots, 5, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_Kills, 5, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_Revives, 5, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P2_SetZeroPoints)
    End Sub

    Private Sub Cbx_P3_SetZeroPoints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P3_SetZeroPoints.CheckedChanged
        If Not Cbx_P3_SetZeroPoints.Checked Then
            BlackOpsObject.Write(MemoryAddresses.P3_Points, 5, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_HeadShots, 5, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_Kills, 5, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_Revives, 5, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P3_SetZeroPoints)
    End Sub

    Private Sub Cbx_P4_SetZeroPoints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P4_SetZeroPoints.CheckedChanged
        If Not Cbx_P4_SetZeroPoints.Checked Then
            BlackOpsObject.Write(MemoryAddresses.P4_Points, 5, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_HeadShots, 5, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_Kills, 5, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_Revives, 5, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P4_SetZeroPoints)
    End Sub


    ' === Lock Player Location Checkboxes ===
    Private Sub Cbx_P2_LockPlayerLocation_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P2_LockPlayerLocation.CheckedChanged
        If Cbx_P2_LockPlayerLocation.Checked Then
            SavePlayerPosition(BlackOpsObject._Module(MemoryAddresses.P2_LocationCoords), P2_StoredPlayerPos)
        Else
            P2_StoredPlayerPos = New Vector3F(0, 0, 0)
        End If
        UpdateCheckBoxForeColor(Cbx_P2_LockPlayerLocation)
    End Sub

    Private Sub Cbx_P3_LockPlayerLocation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P3_LockPlayerLocation.CheckedChanged
        If Cbx_P3_LockPlayerLocation.Checked Then
            SavePlayerPosition(BlackOpsObject._Module(MemoryAddresses.P3_LocationCoords), P3_StoredPlayerPos)
        Else
            P3_StoredPlayerPos = New Vector3F(0, 0, 0)
        End If
        UpdateCheckBoxForeColor(Cbx_P3_LockPlayerLocation)
    End Sub

    Private Sub Cbx_P4_LockPlayerLocation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P4_LockPlayerLocation.CheckedChanged
        If Cbx_P4_LockPlayerLocation.Checked Then
            SavePlayerPosition(BlackOpsObject._Module(MemoryAddresses.P4_LocationCoords), P4_StoredPlayerPos)
        Else
            P4_StoredPlayerPos = New Vector3F(0, 0, 0)
        End If
        UpdateCheckBoxForeColor(Cbx_P4_LockPlayerLocation)
    End Sub


    ' === Take Weapons Checkboxes ===
    Private Sub Cbx_P2_TakeWeapons_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P2_TakeWeapons.CheckedChanged
        If Not Cbx_P2_TakeWeapons.Checked Then
            BlackOpsObject.Write(MemoryAddresses.P2_WeaponSlot1, 4, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_WeaponSlot2, 4, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P2_WeaponSlot3, 4, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P2_TakeWeapons)
    End Sub

    Private Sub Cbx_P3_TakeWeapons_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P3_TakeWeapons.CheckedChanged
        If Not Cbx_P3_TakeWeapons.Checked Then
            BlackOpsObject.Write(MemoryAddresses.P3_WeaponSlot1, 4, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_WeaponSlot2, 4, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P3_WeaponSlot3, 4, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P3_TakeWeapons)
    End Sub

    Private Sub Cbx_P4_TakeWeapons_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P4_TakeWeapons.CheckedChanged
        If Not Cbx_P4_TakeWeapons.Checked Then
            BlackOpsObject.Write(MemoryAddresses.P4_WeaponSlot1, 4, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_WeaponSlot2, 4, GetType(Integer))
            BlackOpsObject.Write(MemoryAddresses.P4_WeaponSlot3, 4, GetType(Integer))
        End If
        UpdateCheckBoxForeColor(Cbx_P4_TakeWeapons)
    End Sub


    ''' <summary>
    ''' Confirms and kicks the specified player by sending a command,
    ''' resets all controls inside the player's GroupBox to their default state,
    ''' and clears the kick checkbox.
    ''' </summary>
    ''' <param name="playerIndex">The zero-based index of the player to kick (e.g., 1 for player 2).</param>
    ''' <param name="groupBox">The GroupBox containing the player's controls and name.</param>
    ''' <param name="playerPrefix">The prefix text for the GroupBox (e.g., "P2: ").</param>
    ''' <param name="kickCheckbox">The CheckBox that triggered the kick action, which will be unchecked.</param>
    Private Sub HandleKickPlayer(playerIndex As Integer, groupBox As GroupBox, playerPrefix As String, kickCheckbox As CheckBox)
        Dim playerLabel As String = groupBox.Text
        Dim playerName As String = $"Player {playerIndex + 1}"

        If Not String.IsNullOrWhiteSpace(playerLabel) AndAlso playerLabel.StartsWith(playerPrefix) Then
            Dim namePart As String = playerLabel.Substring(playerPrefix.Length).Trim()
            If Not String.IsNullOrWhiteSpace(namePart) Then
                playerName = namePart
            End If
        End If

        Dim result As DialogResult = MessageBox.Show($"Are you sure you want to kick {playerName}?", "Confirm Kick", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            Write_Command($"clientkick {playerIndex}")
            groupBox.Text = playerPrefix
            Utilities.ResetControlsToDefaultsRecursive(groupBox)
        End If

        kickCheckbox.Checked = False
    End Sub



    ' === Kic kPlayer Checkboxes ===
    Private Sub Cbx_P2_KickPlayer_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Cbx_P2_KickPlayer.CheckedChanged
        If Cbx_P2_KickPlayer.Checked Then
            HandleKickPlayer(
            playerIndex:=1,
            groupBox:=GroupBox_P2,
            playerPrefix:="P2: ",
            kickCheckbox:=Cbx_P2_KickPlayer
        )
        End If
    End Sub

    Private Sub Cbx_P3_KickPlayer_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Cbx_P3_KickPlayer.CheckedChanged
        If Cbx_P3_KickPlayer.Checked Then
            HandleKickPlayer(
            playerIndex:=2,
            groupBox:=GroupBox_P3,
            playerPrefix:="P3: ",
            kickCheckbox:=Cbx_P3_KickPlayer
        )
        End If
    End Sub

    Private Sub Cbx_P4_KickPlayer_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Cbx_P4_KickPlayer.CheckedChanged
        If Cbx_P4_KickPlayer.Checked Then
            HandleKickPlayer(
            playerIndex:=3,
            groupBox:=GroupBox_P4,
            playerPrefix:="P4: ",
            kickCheckbox:=Cbx_P4_KickPlayer
        )
        End If
    End Sub


    ' === Some More Checkboxes ===
    Private Sub Cbx_P1_FastTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_FastTrigger.CheckedChanged
        BlackOpsObject.Inject("76999B", "C7463C00000000", "837E3C00746A", True)
        UpdateCheckBoxForeColor(Cbx_P1_FastTrigger)
    End Sub

    Private Sub Cbx_P1_InstaKill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_InstaKill.CheckedChanged
        BlackOpsObject.Inject("5C8B39", "C7868401000000000000", "89BE84010000", True)
        UpdateCheckBoxForeColor(Cbx_P1_InstaKill)
    End Sub

    Private Sub Cbx_P1_AimSpread_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_AimSpread.CheckedChanged
        ' Always patch regardless of checked state
        BlackOpsObject.Patch("406DEA", "90909090909090909090", "F783FC0400000000000C")

        Dim value As Integer = If(Cbx_P1_AimSpread.Checked, 1008981770, 1059481190)
        BlackOpsObject.Write(MemoryAddresses.P1_AimSpread, value, GetType(Integer))

        UpdateCheckBoxForeColor(Cbx_P1_AimSpread)
    End Sub

    Private Sub Cbx_P1_NoRecoil_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbx_P1_NoRecoil.CheckedChanged
        BlackOpsObject.Patch(&H6563F8, "9090909090", "E8C312F1FF")
        UpdateCheckBoxForeColor(Cbx_P1_NoRecoil)
    End Sub

    Private Sub Cbx_P1_HackDevice_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_P1_HackDevice.CheckedChanged
        'BlackOpsObject.Write(MemoryAddresses.DeviceDisablePickup, If(Cbx_P1_HackDevice.Checked, 0, 1), GetType(Integer))
        'BlackOpsObject.Write(MemoryAddresses.DeviceVisibilityHidden, If(Cbx_P1_HackDevice.Checked, 0, 1), GetType(Integer))
        UpdateCheckBoxForeColor(Cbx_P1_HackDevice)
    End Sub

    Private Sub Cbx_Test_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_Test.CheckedChanged


        UpdateCheckBoxForeColor(Cbx_Test)
    End Sub

#End Region ' Checkbox Setters

#Region "Command Buttons"

    Private Sub Btn_Command_KillAllZombies_Click(ByVal sender As System.Object, e As System.EventArgs) Handles Btn_Command_KillAllZombies.Click
        Write_Command("ai axis delete")
    End Sub

    ''' <summary>
    ''' Processes a weapon selection from the specified combo box and issues a give command for the selected weapon.
    ''' </summary>
    ''' <param name="comboBox">
    ''' The combo box containing the selected <see cref="WeaponEntry"/> to be processed.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if a valid weapon was selected and processed; otherwise, <c>False</c>.
    ''' </returns>
    ''' <remarks>
    ''' If a valid weapon is selected (i.e., index > 0), this method writes the weapon give command
    ''' using the selected weapon's ID. If the No Recoil checkbox is checked, it temporarily patches
    ''' memory to disable recoil during the give command, then restores the patch.
    ''' The combo box selection is reset to the default afterward.
    ''' </remarks>
    Private Function ProcessWeaponSelection(comboBox As ComboBox) As Boolean
        If comboBox.SelectedIndex < 1 Then Return False

        Dim selectedItem = TryCast(comboBox.SelectedItem, WeaponEntry)
        If selectedItem IsNot Nothing Then

            If Cbx_P1_NoRecoil.Checked Then
                BlackOpsObject.Patch(&H6563F8, "9090909090", "E8C312F1FF")
            End If

            Write_CommandGive(selectedItem.WeaponId)

            If Cbx_P1_NoRecoil.Checked Then
                BlackOpsObject.Patch(&H6563F8, "9090909090", "E8C312F1FF")
            End If

            comboBox.SelectedIndex = 0
            Return True
        End If

        Return False
    End Function

    Private Sub Btn_Weapons_GiveAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Weapons_GiveAction.Click

        If ProcessWeaponSelection(Combx_RegularWeapons) Then Return
        If ProcessWeaponSelection(Combx_UpgradedWeapons) Then Return
        If ProcessWeaponSelection(Combx_WonderWeapons) Then Return
        If ProcessWeaponSelection(Combx_TacticalWeapons) Then Return

    End Sub

    ''' <summary>
    ''' Resets the selected index of all weapon combo boxes except the one currently active.
    ''' </summary>
    ''' <param name="activeComboBox">
    ''' The combo box that was interacted with and should not be reset.
    ''' </param>
    ''' <remarks>
    ''' This method is used to ensure that only one weapon category is selected at a time
    ''' by resetting the selection of the other combo boxes to their default index (0).
    ''' </remarks>
    Private Sub ResetOtherWeaponsComboBoxes(activeComboBox As ComboBox)
        Dim allComboBoxes = {Combx_RegularWeapons, Combx_UpgradedWeapons, Combx_WonderWeapons, Combx_TacticalWeapons}

        For Each cb In allComboBoxes
            If cb IsNot activeComboBox AndAlso cb.Items.Count > 0 Then
                cb.SelectedIndex = 0
            End If
        Next
    End Sub

    Private Sub Combx_RegularWeapons_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combx_RegularWeapons.SelectedIndexChanged
        If Combx_RegularWeapons.SelectedIndex > 0 Then
            ResetOtherWeaponsComboBoxes(Combx_RegularWeapons)
        End If
    End Sub

    Private Sub Combx_UpgradedWeapons_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combx_UpgradedWeapons.SelectedIndexChanged
        If Combx_UpgradedWeapons.SelectedIndex > 0 Then
            ResetOtherWeaponsComboBoxes(Combx_UpgradedWeapons)
        End If
    End Sub

    Private Sub Combx_WonderWeapons_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combx_WonderWeapons.SelectedIndexChanged
        If Combx_WonderWeapons.SelectedIndex > 0 Then
            ResetOtherWeaponsComboBoxes(Combx_WonderWeapons)
        End If
    End Sub

    Private Sub Combx_TacticalWeapons_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combx_TacticalWeapons.SelectedIndexChanged
        If Combx_TacticalWeapons.SelectedIndex > 0 Then
            ResetOtherWeaponsComboBoxes(Combx_TacticalWeapons)
        End If
    End Sub

#End Region ' Command Buttons

#Region "Weapons"

    ''' <summary>
    ''' Assigns a selected special weapon to one of the three weapon slots using provided memory addresses.
    ''' Handles selection validation, slot targeting, and writing.
    ''' </summary>
    ''' <param name="slot1Address">Memory address for weapon slot 1.</param>
    ''' <param name="slot2Address">Memory address for weapon slot 2.</param>
    ''' <param name="slot3Address">Memory address for weapon slot 3.</param>
    Private Sub AssignSpecialWeapon(slot1Address As String, slot2Address As String, slot3Address As String)
        Dim selectedWeapon = TryCast(Combx_SpecialWeapons.SelectedItem, WeaponEntry)

        If selectedWeapon Is Nothing Or Combx_SpecialWeapons.SelectedIndex < 1 Then
            MessageBox.Show("Please select a special weapon before assigning the weapon.", "Special Weapon Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim weaponIdValue = selectedWeapon.WeaponIdValue
        Dim displayName = selectedWeapon.DisplayName

        Dim targetAddress As String = Nothing

        If Cbx_GiveSpecialWeaponToSlot_1.Checked Then
            targetAddress = slot1Address
        ElseIf Cbx_GiveSpecialWeaponToSlot_2.Checked Then
            targetAddress = slot2Address
        ElseIf Cbx_GiveSpecialWeaponToSlot_3.Checked Then
            targetAddress = slot3Address
        End If

        If String.IsNullOrEmpty(targetAddress) Then
            MessageBox.Show("Please select a weapon slot before assigning the Special Weapon.", "Slot Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim memoryLocation As Integer = BlackOpsObject._Module(targetAddress)
        BlackOpsObject.Write(memoryLocation, weaponIdValue)

        Debug.WriteLine($"[Special Weapon] Gave '{displayName}' (ID: {selectedWeapon.WeaponId}, Value: {weaponIdValue}) to memory address: {targetAddress}")
    End Sub

    Private Sub Btn_P1_Weapons_Special_GiveAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P1_Weapons_Special_GiveAction.Click
        AssignSpecialWeapon(MemoryAddresses.P1_WeaponSlot1, MemoryAddresses.P1_WeaponSlot2, MemoryAddresses.P1_WeaponSlot3)
    End Sub

    Private Sub Btn_P2_Weapons_Special_GiveAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P2_Weapons_Special_GiveAction.Click
        AssignSpecialWeapon(MemoryAddresses.P2_WeaponSlot1, MemoryAddresses.P2_WeaponSlot2, MemoryAddresses.P2_WeaponSlot3)
    End Sub

    Private Sub Btn_P3_Weapons_Special_GiveAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P3_Weapons_Special_GiveAction.Click
        AssignSpecialWeapon(MemoryAddresses.P3_WeaponSlot1, MemoryAddresses.P3_WeaponSlot2, MemoryAddresses.P3_WeaponSlot3)
    End Sub

    Private Sub Btn_P4_Weapons_Special_GiveAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_P4_Weapons_Special_GiveAction.Click
        AssignSpecialWeapon(MemoryAddresses.P4_WeaponSlot1, MemoryAddresses.P4_WeaponSlot2, MemoryAddresses.P4_WeaponSlot3)
    End Sub


    ''' <summary>
    ''' Ensures only one checkbox is selected among three and updates their colors based on checked state.
    ''' </summary>
    ''' <param name="selectedCheckbox">The checkbox that triggered the change.</param>
    ''' <param name="otherCheckbox1">The first checkbox to uncheck.</param>
    ''' <param name="otherCheckbox2">The second checkbox to uncheck.</param>
    Private Sub HandleExclusiveCheckboxSelection(selectedCheckbox As CheckBox, otherCheckbox1 As CheckBox, otherCheckbox2 As CheckBox)
        If selectedCheckbox.Checked Then
            otherCheckbox1.Checked = False
            otherCheckbox2.Checked = False
        End If

        selectedCheckbox.ForeColor = If(
            selectedCheckbox.Checked,
            Color.FromArgb(87, 242, 135),   ' Green
            Color.FromArgb(237, 66, 69)     ' Red
        )
    End Sub

    Private Sub Cbx_GiveSpecialWeaponToSlot_1_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_GiveSpecialWeaponToSlot_1.CheckedChanged
        HandleExclusiveCheckboxSelection(Cbx_GiveSpecialWeaponToSlot_1, Cbx_GiveSpecialWeaponToSlot_2, Cbx_GiveSpecialWeaponToSlot_3)
    End Sub

    Private Sub Cbx_GiveSpecialWeaponToSlot_2_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_GiveSpecialWeaponToSlot_2.CheckedChanged
        HandleExclusiveCheckboxSelection(Cbx_GiveSpecialWeaponToSlot_2, Cbx_GiveSpecialWeaponToSlot_1, Cbx_GiveSpecialWeaponToSlot_3)
    End Sub

    Private Sub Cbx_GiveSpecialWeaponToSlot_3_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_GiveSpecialWeaponToSlot_3.CheckedChanged
        HandleExclusiveCheckboxSelection(Cbx_GiveSpecialWeaponToSlot_3, Cbx_GiveSpecialWeaponToSlot_1, Cbx_GiveSpecialWeaponToSlot_2)
    End Sub

    Private Sub Combx_SpecialWeapons_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combx_SpecialWeapons.SelectedIndexChanged
        Dim selectedItem = TryCast(Combx_SpecialWeapons.SelectedItem, WeaponEntry)
        If selectedItem IsNot Nothing Then
            Dim weaponId = selectedItem.WeaponId
            Dim value = selectedItem.WeaponIdValue
            Dim displayName = selectedItem.DisplayName
            Debug.WriteLine($"Selected Weapon: {displayName} (ID: {weaponId}), Value: {value}")
        End If
    End Sub






#End Region ' Weapons

End Class


