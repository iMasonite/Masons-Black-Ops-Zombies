Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Text
Public Class Trainer
    Dim Mem As ProcMem = New ProcMem
    Dim Address As Integer
    Dim iConsole As IntPtr = -1, iText = -1
    Private IsOn As Boolean, iFound
    Dim slot1 As UInteger = 1, slot2 = 1, slot3 = 1
    Dim _X1 As UInteger = 0, _Y1 = 0, _Z1 = 0
    Dim _X2 As UInteger = 0, _Y2 = 0, _Z2 = 0
    Dim _X3 As UInteger = 0, _Y3 = 0, _Z3 = 0
    Dim P2_X As UInteger = 0, P2_Y = 0, P2_Z = 0
    Dim P2_SX As Single = 0, P2_SY = 0, P2_SZ = 0
    Dim SX1 As Single = 0, SY1 = 0, SZ1 = 0
    Dim SX2 As Single = 0, SY2 = 0, SZ2 = 0
    Dim _X As UInteger = 0, _Y = 0, _Z = 0
    Dim SX As Single = 0, SY = 0, SZ = 0
    Dim SX4 As Single = 0, SY4 = 0, SZ4 = 0
    Dim SX5 As Single = 0, SY5 = 0, SZ5 = 0
    Dim SX6 As Single = 0, SY6 = 0, SZ6 = 0
    Dim oSX As Single = 0, oSY = 0, oSZ = 0

#Region "Main"

    Private Sub Trainer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Mem.GetProcess("BlackOps")
        Kino.Items.Add("Box Location 1")
        Kino.Items.Add("Box Location 2")
        Kino.Items.Add("Box Location 3")
        Kino.Items.Add("Box Location 4")
        Kino.Items.Add("Box Location 5")
        Kino.Items.Add("Box Location 6")
        Kino.Items.Add("Box Location 7")
        Kino.Items.Add("Box Location 8")
        Kino.Items.Add("Box Location 9")
        Kino.Items.Add("Power Switch")
        Kino.Items.Add("Pack A Punch")
        Kino.Items.Add("Seceret Location")
        Kino.Items.Add("Time Travel Room 1")
        Kino.Items.Add("Time Travel Room 2")
        Kino.Items.Add("Time Travel Room 3")
        Kino.Items.Add("Movie Room")
        Five.Items.Add("Box Location 1")
        Five.Items.Add("Box Location 2")
        Five.Items.Add("Box Location 3")
        Five.Items.Add("Box Location 4")
        Five.Items.Add("Box Location 5")
        Five.Items.Add("Box Location 6")
        Five.Items.Add("Power Switch")
        Five.Items.Add("Pack A Punch")
        Five.Items.Add("Pack A Punch Room")
        KinoWeapons.Items.Add("Turret Gun")
        KinoWeapons.Items.Add("M1911 Pistol")
        FiveWeapons.Items.Add("Death Machine")
        FiveWeapons.Items.Add("M1911 Pistol")
        MapPackloc.Items.Add("-Ascension")
        MapPackloc.Items.Add("Box Location 1")
        MapPackloc.Items.Add("Box Location 2")
        MapPackloc.Items.Add("Box Location 3")
        MapPackloc.Items.Add("Box Location 4")
        MapPackloc.Items.Add("Box Location 5")
        MapPackloc.Items.Add("Box Location 6")
        MapPackloc.Items.Add("Box Location 7")
        MapPackloc.Items.Add("Power Switch")
        MapPackloc.Items.Add("Pack A Punch")
        MapPackloc.Items.Add("Seceret Location")
        MapPackloc.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPackloc.Items.Add("-Call Of The Dead")
        MapPackloc.Items.Add("Box Location 1")
        MapPackloc.Items.Add("Box Location 2")
        MapPackloc.Items.Add("Box Location 3")
        MapPackloc.Items.Add("Box Location 4")
        MapPackloc.Items.Add("Box Location 5")
        MapPackloc.Items.Add("Box Location 6")
        MapPackloc.Items.Add("Power Switch")
        MapPackloc.Items.Add("Pack A Punch Location 1")
        MapPackloc.Items.Add("Pack A Punch Location 2")
        MapPackloc.Items.Add("Pack A Punch Location 3")
        MapPackloc.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPackloc.Items.Add("-Shangri La")
        MapPackloc.Items.Add("Box Location 1")
        MapPackloc.Items.Add("Box Location 2")
        MapPackloc.Items.Add("Box Location 3")
        MapPackloc.Items.Add("Box Location 4")
        MapPackloc.Items.Add("Power Switch")
        MapPackloc.Items.Add("Pack A Punch")
        MapPackloc.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPackloc.Items.Add("-Moon")
        MapPackloc.Items.Add("Box Location 1")
        MapPackloc.Items.Add("Box Location 2")
        MapPackloc.Items.Add("Box Location 3")
        MapPackloc.Items.Add("Box Location 4")
        MapPackloc.Items.Add("Power Switch")
        MapPackloc.Items.Add("Pack A Punch")
        MapPackloc.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPackloc.Items.Add("-Verrukt")
        MapPackloc.Items.Add("Box Location 1")
        MapPackloc.Items.Add("Box Location 2")
        MapPackloc.Items.Add("Box Location 3")
        MapPackloc.Items.Add("Box Location 4")
        MapPackloc.Items.Add("Box Location 5")
        MapPackloc.Items.Add("Power Switch")
        MapPackloc.Items.Add("Fountain")
        MapPackloc.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPackloc.Items.Add("-Nacht Der Untoten")
        MapPackloc.Items.Add("Box Location")
        MapPackloc.Items.Add("Outside")
        MapPackloc.Items.Add("Airplane")
        MapPackloc.Items.Add("Sniper Post")
        MapPackloc.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPackloc.Items.Add("-Shi No Numa")
        MapPackloc.Items.Add("Box Location 1")
        MapPackloc.Items.Add("Box Location 2")
        MapPackloc.Items.Add("Box Location 3")
        MapPackloc.Items.Add("Box Location 4")
        MapPackloc.Items.Add("Box Location 5")
        MapPackloc.Items.Add("Box Location 6")
        MapPackloc.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPackloc.Items.Add("-Der Riese")
        MapPackloc.Items.Add("Box Location 1")
        MapPackloc.Items.Add("Box Location 2")
        MapPackloc.Items.Add("Box Location 3")
        MapPackloc.Items.Add("Box Location 4")
        MapPackloc.Items.Add("Box Location 5")
        MapPackloc.Items.Add("Box Location 6")
        MapPackloc.Items.Add("Main Frame")
        MapPackloc.Items.Add("Pack A Punch")
        MapPackloc.Items.Add("Clock Tower")
        MapPackloc.Items.Add("Fly Trap")
        MapPack2.Items.Add("-Ascension")
        MapPack2.Items.Add("Box Location 1")
        MapPack2.Items.Add("Box Location 2")
        MapPack2.Items.Add("Box Location 3")
        MapPack2.Items.Add("Box Location 4")
        MapPack2.Items.Add("Box Location 5")
        MapPack2.Items.Add("Box Location 6")
        MapPack2.Items.Add("Box Location 7")
        MapPack2.Items.Add("Power Switch")
        MapPack2.Items.Add("Pack A Punch")
        MapPack2.Items.Add("Seceret Location")
        MapPack2.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack2.Items.Add("-Call Of The Dead")
        MapPack2.Items.Add("Box Location 1")
        MapPack2.Items.Add("Box Location 2")
        MapPack2.Items.Add("Box Location 3")
        MapPack2.Items.Add("Box Location 4")
        MapPack2.Items.Add("Box Location 5")
        MapPack2.Items.Add("Box Location 6")
        MapPack2.Items.Add("Power Switch")
        MapPack2.Items.Add("Pack A Punch Location 1")
        MapPack2.Items.Add("Pack A Punch Location 2")
        MapPack2.Items.Add("Pack A Punch Location 3")
        MapPack2.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack2.Items.Add("-Shangri La")
        MapPack2.Items.Add("Box Location 1")
        MapPack2.Items.Add("Box Location 2")
        MapPack2.Items.Add("Box Location 3")
        MapPack2.Items.Add("Box Location 4")
        MapPack2.Items.Add("Power Switch")
        MapPack2.Items.Add("Pack A Punch")
        MapPack2.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack2.Items.Add("-Moon")
        MapPack2.Items.Add("Box Location 1")
        MapPack2.Items.Add("Box Location 2")
        MapPack2.Items.Add("Box Location 3")
        MapPack2.Items.Add("Box Location 4")
        MapPack2.Items.Add("Power Switch")
        MapPack2.Items.Add("Pack A Punch")
        MapPack2.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack2.Items.Add("-Verrukt")
        MapPack2.Items.Add("Box Location 1")
        MapPack2.Items.Add("Box Location 2")
        MapPack2.Items.Add("Box Location 3")
        MapPack2.Items.Add("Box Location 4")
        MapPack2.Items.Add("Box Location 5")
        MapPack2.Items.Add("Power Switch")
        MapPack2.Items.Add("Fountain")
        MapPack2.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack2.Items.Add("-Nacht Der Untoten")
        MapPack2.Items.Add("Box Location")
        MapPack2.Items.Add("Outside")
        MapPack2.Items.Add("Airplane")
        MapPack2.Items.Add("Sniper Post")
        MapPack2.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack2.Items.Add("-Shi No Numa")
        MapPack2.Items.Add("Box Location 1")
        MapPack2.Items.Add("Box Location 2")
        MapPack2.Items.Add("Box Location 3")
        MapPack2.Items.Add("Box Location 4")
        MapPack2.Items.Add("Box Location 5")
        MapPack2.Items.Add("Box Location 6")
        MapPack2.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack2.Items.Add("-Der Riese")
        MapPack2.Items.Add("Box Location 1")
        MapPack2.Items.Add("Box Location 2")
        MapPack2.Items.Add("Box Location 3")
        MapPack2.Items.Add("Box Location 4")
        MapPack2.Items.Add("Box Location 5")
        MapPack2.Items.Add("Box Location 6")
        MapPack2.Items.Add("Main Frame")
        MapPack2.Items.Add("Pack A Punch")
        MapPack2.Items.Add("Clock Tower")
        MapPack2.Items.Add("Fly Trap")
        MapPack1.Items.Add("-Ascension")
        MapPack1.Items.Add("Box Location 1")
        MapPack1.Items.Add("Box Location 2")
        MapPack1.Items.Add("Box Location 3")
        MapPack1.Items.Add("Box Location 4")
        MapPack1.Items.Add("Box Location 5")
        MapPack1.Items.Add("Box Location 6")
        MapPack1.Items.Add("Box Location 7")
        MapPack1.Items.Add("Power Switch")
        MapPack1.Items.Add("Pack A Punch")
        MapPack1.Items.Add("Seceret Location")
        MapPack1.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack1.Items.Add("-Call Of The Dead")
        MapPack1.Items.Add("Box Location 1")
        MapPack1.Items.Add("Box Location 2")
        MapPack1.Items.Add("Box Location 3")
        MapPack1.Items.Add("Box Location 4")
        MapPack1.Items.Add("Box Location 5")
        MapPack1.Items.Add("Box Location 6")
        MapPack1.Items.Add("Power Switch")
        MapPack1.Items.Add("Pack A Punch Location 1")
        MapPack1.Items.Add("Pack A Punch Location 2")
        MapPack1.Items.Add("Pack A Punch Location 3")
        MapPack1.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack1.Items.Add("-Shangri La")
        MapPack1.Items.Add("Box Location 1")
        MapPack1.Items.Add("Box Location 2")
        MapPack1.Items.Add("Box Location 3")
        MapPack1.Items.Add("Box Location 4")
        MapPack1.Items.Add("Power Switch")
        MapPack1.Items.Add("Pack A Punch")
        MapPack1.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack1.Items.Add("-Moon")
        MapPack1.Items.Add("Box Location 1")
        MapPack1.Items.Add("Box Location 2")
        MapPack1.Items.Add("Box Location 3")
        MapPack1.Items.Add("Box Location 4")
        MapPack1.Items.Add("Power Switch")
        MapPack1.Items.Add("Pack A Punch")
        MapPack1.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack1.Items.Add("-Verrukt")
        MapPack1.Items.Add("Box Location 1")
        MapPack1.Items.Add("Box Location 2")
        MapPack1.Items.Add("Box Location 3")
        MapPack1.Items.Add("Box Location 4")
        MapPack1.Items.Add("Box Location 5")
        MapPack1.Items.Add("Power Switch")
        MapPack1.Items.Add("Fountain")
        MapPack1.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack1.Items.Add("-Nacht Der Untoten")
        MapPack1.Items.Add("Box Location")
        MapPack1.Items.Add("Outside")
        MapPack1.Items.Add("Airplane")
        MapPack1.Items.Add("Sniper Post")
        MapPack1.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack1.Items.Add("-Shi No Numa")
        MapPack1.Items.Add("Box Location 1")
        MapPack1.Items.Add("Box Location 2")
        MapPack1.Items.Add("Box Location 3")
        MapPack1.Items.Add("Box Location 4")
        MapPack1.Items.Add("Box Location 5")
        MapPack1.Items.Add("Box Location 6")
        MapPack1.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack1.Items.Add("-Der Riese")
        MapPack1.Items.Add("Box Location 1")
        MapPack1.Items.Add("Box Location 2")
        MapPack1.Items.Add("Box Location 3")
        MapPack1.Items.Add("Box Location 4")
        MapPack1.Items.Add("Box Location 5")
        MapPack1.Items.Add("Box Location 6")
        MapPack1.Items.Add("Main Frame")
        MapPack1.Items.Add("Pack A Punch")
        MapPack1.Items.Add("Clock Tower")
        MapPack1.Items.Add("Fly Trap")
        ComboBox1.Items.Add("Box Location 1")
        ComboBox1.Items.Add("Box Location 2")
        ComboBox1.Items.Add("Box Location 3")
        ComboBox1.Items.Add("Box Location 4")
        ComboBox1.Items.Add("Box Location 5")
        ComboBox1.Items.Add("Box Location 6")
        ComboBox1.Items.Add("Box Location 7")
        ComboBox1.Items.Add("Box Location 8")
        ComboBox1.Items.Add("Box Location 9")
        ComboBox1.Items.Add("Power Switch")
        ComboBox1.Items.Add("Pack A Punch")
        ComboBox1.Items.Add("Seceret Location")
        ComboBox1.Items.Add("Time Travel Room 1")
        ComboBox1.Items.Add("Time Travel Room 2")
        ComboBox1.Items.Add("Time Travel Room 3")
        ComboBox1.Items.Add("Movie Room")
        ComboBox2.Items.Add("Box Location 1")
        ComboBox2.Items.Add("Box Location 2")
        ComboBox2.Items.Add("Box Location 3")
        ComboBox2.Items.Add("Box Location 4")
        ComboBox2.Items.Add("Box Location 5")
        ComboBox2.Items.Add("Box Location 6")
        ComboBox2.Items.Add("Power Switch")
        ComboBox2.Items.Add("Pack A Punch")
        ComboBox2.Items.Add("Pack A Punch Room")
        ComboBox6.Items.Add("Box Location 1")
        ComboBox6.Items.Add("Box Location 2")
        ComboBox6.Items.Add("Box Location 3")
        ComboBox6.Items.Add("Box Location 4")
        ComboBox6.Items.Add("Box Location 5")
        ComboBox6.Items.Add("Box Location 6")
        ComboBox6.Items.Add("Box Location 7")
        ComboBox6.Items.Add("Box Location 8")
        ComboBox6.Items.Add("Box Location 9")
        ComboBox6.Items.Add("Power Switch")
        ComboBox6.Items.Add("Pack A Punch")
        ComboBox6.Items.Add("Seceret Location")
        ComboBox6.Items.Add("Time Travel Room 1")
        ComboBox6.Items.Add("Time Travel Room 2")
        ComboBox6.Items.Add("Time Travel Room 3")
        ComboBox6.Items.Add("Movie Room")
        ComboBox5.Items.Add("Box Location 1")
        ComboBox5.Items.Add("Box Location 2")
        ComboBox5.Items.Add("Box Location 3")
        ComboBox5.Items.Add("Box Location 4")
        ComboBox5.Items.Add("Box Location 5")
        ComboBox5.Items.Add("Box Location 6")
        ComboBox5.Items.Add("Power Switch")
        ComboBox5.Items.Add("Pack A Punch")
        ComboBox5.Items.Add("Pack A Punch Room")
        Kino3.Items.Add("Box Location 1")
        Kino3.Items.Add("Box Location 2")
        Kino3.Items.Add("Box Location 3")
        Kino3.Items.Add("Box Location 4")
        Kino3.Items.Add("Box Location 5")
        Kino3.Items.Add("Box Location 6")
        Kino3.Items.Add("Box Location 7")
        Kino3.Items.Add("Box Location 8")
        Kino3.Items.Add("Box Location 9")
        Kino3.Items.Add("Power Switch")
        Kino3.Items.Add("Pack A Punch")
        Kino3.Items.Add("Seceret Location")
        Kino3.Items.Add("Time Travel Room 1")
        Kino3.Items.Add("Time Travel Room 2")
        Kino3.Items.Add("Time Travel Room 3")
        Kino3.Items.Add("Movie Room")
        Five3.Items.Add("Box Location 1")
        Five3.Items.Add("Box Location 2")
        Five3.Items.Add("Box Location 3")
        Five3.Items.Add("Box Location 4")
        Five3.Items.Add("Box Location 5")
        Five3.Items.Add("Box Location 6")
        Five3.Items.Add("Power Switch")
        Five3.Items.Add("Pack A Punch")
        Five3.Items.Add("Pack A Punch Room")
        MapPack.Items.Add("-Ascension")
        MapPack.Items.Add("Box Location 1")
        MapPack.Items.Add("Box Location 2")
        MapPack.Items.Add("Box Location 3")
        MapPack.Items.Add("Box Location 4")
        MapPack.Items.Add("Box Location 5")
        MapPack.Items.Add("Box Location 6")
        MapPack.Items.Add("Box Location 7")
        MapPack.Items.Add("Power Switch")
        MapPack.Items.Add("Pack A Punch")
        MapPack.Items.Add("Seceret Location")
        MapPack.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack.Items.Add("-Call Of The Dead")
        MapPack.Items.Add("Box Location 1")
        MapPack.Items.Add("Box Location 2")
        MapPack.Items.Add("Box Location 3")
        MapPack.Items.Add("Box Location 4")
        MapPack.Items.Add("Box Location 5")
        MapPack.Items.Add("Box Location 6")
        MapPack.Items.Add("Power Switch")
        MapPack.Items.Add("Pack A Punch Location 1")
        MapPack.Items.Add("Pack A Punch Location 2")
        MapPack.Items.Add("Pack A Punch Location 3")
        MapPack.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack.Items.Add("-Shangri La")
        MapPack.Items.Add("Box Location 1")
        MapPack.Items.Add("Box Location 2")
        MapPack.Items.Add("Box Location 3")
        MapPack.Items.Add("Power Switch")
        MapPack.Items.Add("Pack A Punch")
        MapPack.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack.Items.Add("-Moon")
        MapPack.Items.Add("Box Location 1")
        MapPack.Items.Add("Box Location 2")
        MapPack.Items.Add("Box Location 3")
        MapPack.Items.Add("Box Location 4")
        MapPack.Items.Add("Power Switch")
        MapPack.Items.Add("Pack A Punch")
        MapPack.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack.Items.Add("-Verukt")
        MapPack.Items.Add("Box Location 1")
        MapPack.Items.Add("Box Location 2")
        MapPack.Items.Add("Box Location 3")
        MapPack.Items.Add("Box Location 4")
        MapPack.Items.Add("Box Location 5")
        MapPack.Items.Add("Power Switch")
        MapPack.Items.Add("Fountain")
        MapPack.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack.Items.Add("-Nacht Der Untoten")
        MapPack.Items.Add("Box Location")
        MapPack.Items.Add("Outside")
        MapPack.Items.Add("Airplane")
        MapPack.Items.Add("Sniper Post")
        MapPack.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack.Items.Add("-Shi No Numa")
        MapPack.Items.Add("Box Location 1")
        MapPack.Items.Add("Box Location 2")
        MapPack.Items.Add("Box Location 3")
        MapPack.Items.Add("Box Location 4")
        MapPack.Items.Add("Box Location 5")
        MapPack.Items.Add("Box Location 6")
        MapPack.Items.Add("---------------------------------------------------------------------------------------------------------------------------------------------------")
        MapPack.Items.Add("-Der Riese")
        MapPack.Items.Add("Box Location 1")
        MapPack.Items.Add("Box Location 2")
        MapPack.Items.Add("Box Location 3")
        MapPack.Items.Add("Box Location 4")
        MapPack.Items.Add("Box Location 5")
        MapPack.Items.Add("Box Location 6")
        MapPack.Items.Add("Main Frame")
        MapPack.Items.Add("Pack A Punch")
        MapPack.Items.Add("Clock Tower")
        MapPack.Items.Add("Fly Trap")
    End Sub

    Private Sub Main_Tick(sender As System.Object, e As System.EventArgs) Handles Main.Tick
        CommandCentre.sharedtimer2()
        If Mem.ProcActive Then
            If Mem.Keystate(Keys.F1) Then
                IsOn = Not IsOn
                System.Threading.Thread.Sleep(200)
                If IsOn Then
                    Mem.Write(iConsole, 1)
                Else
                    Mem.Write(iConsole, 0)
                End If
            End If
            If (Not Mem.ProcActive AndAlso iConsole <> IntPtr.Zero) Then
                iConsole = IntPtr.Zero
                iText = IntPtr.Zero
                iFound = False
            End If
            If (Mem.ProcActive AndAlso Not iFound) Then
                iFound = True
                iUpdate()
            End If
        End If

        Dim player1 As String = Mem.ReadString(&H1C0A5E0, 30, False)
        GiveWeapon1.Text = player1
        Dim player2 As String = Mem.ReadString(&HC18DB0, 30, False)
        GiveWeapon2.Text = player2
        Dim player3 As String = Mem.ReadString(&HC18E18, 30, False)
        GiveWeapon3.Text = player3
        Dim player4 As String = Mem.ReadString(&HC18E80, 30, False)
        GiveWeapon4.Text = player4
        TabPage1.Text = player2
        TabPage2.Text = player3
        TabPage3.Text = player4

        If HeadShots.Checked Then
            Dim killsc As Integer = Mem._Module("BlackOps.exe+180A6CC")
            killsc = Mem.Read(killsc, GetType(Integer))
            Mem.Write("BlackOps.exe+180A6EC", killsc, GetType(Integer))
        End If

        If HeadShots4.Checked Then
            Dim killsp4 As Integer = Mem._Module("BlackOps.exe+180FE44")
            killsp4 = Mem.Read(killsp4, GetType(Integer))
            Mem.Write("BlackOps.exe+180FE64", killsp4, GetType(Integer))
        End If

        If HeadShots3.Checked Then
            Dim killsp3 As Integer = Mem._Module("BlackOps.exe+180E11C")
            killsp3 = Mem.Read(killsp3, GetType(Integer))
            Mem.Write("BlackOps.exe+180E13C", killsp3, GetType(Integer))
        End If

        If Headshots2.Checked Then
            Dim killsp2 As Integer = Mem._Module("BlackOps.exe+180C3F4")
            killsp2 = Mem.Read(killsp2, GetType(Integer))
            Mem.Write("BlackOps.exe+180C414", killsp2, GetType(Integer))
        End If

        If NoReload2.Checked Then
            Mem.Write("BlackOps.exe+180AC30", 99, GetType(Integer))
            Mem.Write("BlackOps.exe+180AC28", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180AC38", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180AC40", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180ABC0", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180ABB0", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180ABC8", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180AC48", 1023, GetType(Integer))
        End If
        If NoReload3.Checked Then
            Mem.Write("BlackOps.exe+180C958", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180C970", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180C950", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180C960", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180C968", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180C8D8", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180C8F0", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180C8D0", 1023, GetType(Integer))
        End If
        If NoReload4.Checked Then
            Mem.Write("BlackOps.exe+180E680", 99, GetType(Integer))
            Mem.Write("BlackOps.exe+180E698", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180E678", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180E688", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180E690", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180E600", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180E618", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180E5F8", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+180E5F0", 1023, GetType(Integer))
        End If
        If Freeze2.Checked Then
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+180A88C")
            Mem.writeF(XAddy, SX)
            Mem.writeF(XAddy + 4, SY)
            Mem.writeF(XAddy + 8, SZ)
        End If
        If SetPointsP2.Checked Then
            Mem.Write("BlackOps.exe+180C3F0", 0, GetType(Integer))
            Mem.Write("BlackOps.exe+180C414", 0, GetType(Integer))
            Mem.Write("BlackOps.exe+180C3F4", 0, GetType(Integer))
            Mem.Write("BlackOps.exe+180C3F4", 0, GetType(Integer))
            Mem.Write("BlackOps.exe+180C410", 0, GetType(Integer))
        End If
        If WeaponsP2.Checked Then
            Mem.Write("BlackOps.exe+180AA5C", 0, GetType(Integer))
            Mem.Write("Blackops.exe+180AA8C", 0, GetType(Integer))
            Mem.Write("Blackops.exe+180AABC", 0, GetType(Integer))
        End If
        If FreezeP3.Checked Then
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+180C5B4")
            Mem.writeF(XAddy, SX1)
            Mem.writeF(XAddy + 4, SY1)
            Mem.writeF(XAddy + 8, SZ1)
        End If
        If SetPointsP3.Checked Then
            Mem.Write("BlackOps.exe+180E118", 0, GetType(Integer))
            Mem.Write("BlackOps.exe+180E13C", 0, GetType(Integer))
            Mem.Write("BlackOps.exe+180E11C", 0, GetType(Integer))
            Mem.Write("BlackOps.exe+180E138", 0, GetType(Integer))
        End If
        If WeaponsP3.Checked Then
            Mem.Write("Blackops.exe+180C784", 0, GetType(Integer))
            Mem.Write("Blackops.exe+180C7B4", 0, GetType(Integer))
            Mem.Write("Blackops.exe+180C7E4", 0, GetType(Integer))
        End If
        If FreezePlayerP4.Checked Then
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+180E2DC")
            Mem.writeF(XAddy, SX2)
            Mem.writeF(XAddy + 4, SY2)
            Mem.writeF(XAddy + 8, SZ2)
        End If
        If SetPointsP4.Checked Then
            Mem.Write("BlackOps.exe+180FE64", 0, GetType(Integer))
            Mem.Write("BlackOps.exe+180FE44", 0, GetType(Integer))
            Mem.Write("BlackOps.exe+180FE40", 0, GetType(Integer))
            Mem.Write("BlackOps.exe+180FE60", 0, GetType(Integer))
        End If
        If WeaponsP4.Checked Then
            Mem.Write("Blackops.exe+180E4AC", 0, GetType(Integer))
            Mem.Write("Blackops.exe+180E4DC", 0, GetType(Integer))
            Mem.Write("Blackops.exe+180E50C", 0, GetType(Integer))
        End If
        If God4.Checked Then
            Mem.Write("BlackOps.exe+167A260", 99999, GetType(Integer))
            Mem.Write("BlackOps.exe+167A264", 99999, GetType(Integer))
        End If
        If God1.Checked = True Then
            Mem.Write("BlackOps.exe+1679BC8", 99999, GetType(Integer))
            Mem.Write("BlackOps.exe+1679BCC", 99999, GetType(Integer))
        End If
        If GodP3.Checked Then
            Mem.Write("BlackOps.exe+1679F14", 99999, GetType(Integer))
            Mem.Write("BlackOps.exe+1679F18", 99999, GetType(Integer))
        End If
        If NoReload.Checked = True Then
            Mem.Write("BlackOps.exe+1808F08", 99, GetType(Integer))
            Mem.Write("BlackOps.exe+1808F10", 99, GetType(Integer))
            Mem.Write("BlackOps.exe+1808F20", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+1808ea0", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+1808F00", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+1808e88", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+1808F10", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+1808e98", 1023, GetType(Integer))
            Mem.Write("BlackOps.exe+1808F18", 1023, GetType(Integer))
        End If
        If Host.Checked = True Then
            Mem.Write("BlackOps.exe+167987C", 9999999, GetType(Integer))
            Mem.Write("BlackOps.exe+1679880", 9999999, GetType(Integer))
        End If

        If Mem.ProcActive = False Then
            Force.Checked = False
            Force.ForeColor = Color.Yellow
            GiveWeapon2.Text = Nothing
            GiveWeapon3.Text = Nothing
            GiveWeapon4.Text = Nothing
            GiveWeapon1.Text = Nothing
            Recoil.Checked = False
            Recoil.ForeColor = Color.Yellow
            FloatingDead.Checked = False
            FloatingDead.ForeColor = Color.Yellow
            God.Checked = False
            God.ForeColor = Color.Yellow
            Clipping.Checked = False
            Clipping.ForeColor = Color.Yellow
            NoReload.Checked = False
            NoReload.ForeColor = Color.Yellow
            Fast.Checked = False
            Fast.ForeColor = Color.Yellow
            Slow.Checked = False
            Slow.ForeColor = Color.Yellow
            Melee.Checked = False
            Melee.ForeColor = Color.Yellow
            HeadShots.Checked = False
            HeadShots.ForeColor = Color.Yellow
            Trigger.Checked = False
            Trigger.ForeColor = Color.Yellow
            Host.Checked = False
            Host.ForeColor = Color.Yellow
            AllPlayers.Checked = False
            AllPlayers.ForeColor = Color.Yellow
            Gravity.Checked = False
            Gravity.ForeColor = Color.Yellow
            Person.Checked = False
            Person.ForeColor = Color.Yellow
            Instantkill.Checked = False
            Instantkill.ForeColor = Color.Yellow
            Spread.Checked = False
            Spread.ForeColor = Color.Yellow
            NoReload2.Checked = False
            NoReload2.ForeColor = Color.Yellow
            box.Checked = False
            box.ForeColor = Color.Yellow
            Invisible.Checked = False
            Invisible.ForeColor = Color.Yellow
            HeadShots4.Checked = False
            HeadShots4.ForeColor = Color.Yellow
            Invisible4.Checked = False
            Invisible4.ForeColor = Color.Yellow
            God4.Checked = False
            God4.ForeColor = Color.Yellow
            WeaponsP4.Checked = False
            WeaponsP4.ForeColor = Color.Yellow
            SetPointsP4.Checked = False
            SetPointsP4.ForeColor = Color.Yellow
            FreezePlayerP4.Checked = False
            FreezePlayerP4.ForeColor = Color.Yellow
            Clip4.Checked = False
            Clip4.ForeColor = Color.Yellow
            NoReload4.Checked = False
            NoReload4.ForeColor = Color.Yellow
            Kick4.Checked = False
            TabPage3.Text = Nothing
            TabPage1.Text = Nothing
            Headshots2.Checked = False
            Headshots2.ForeColor = Color.Yellow
            Invisible2.Checked = False
            Invisible2.ForeColor = Color.Yellow
            WeaponsP2.Checked = False
            WeaponsP2.ForeColor = Color.Yellow
            Clip1.Checked = False
            Clip1.ForeColor = Color.Yellow
            Freeze2.Checked = False
            Freeze2.ForeColor = Color.Yellow
            God1.Checked = False
            God1.ForeColor = Color.Yellow
            SetPointsP2.Checked = False
            SetPointsP2.ForeColor = Color.Yellow
            NoReload2.Checked = False
            NoReload2.ForeColor = Color.Yellow
            Kick2.Checked = False
            TabPage2.Text = Nothing
            GodP3.Checked = False
            GodP3.ForeColor = Color.Yellow
            ClipP3.Checked = False
            ClipP3.ForeColor = Color.Yellow
            HeadShots3.Checked = False
            HeadShots3.ForeColor = Color.Yellow
            WeaponsP3.Checked = False
            WeaponsP3.ForeColor = Color.Yellow
            FreezeP3.Checked = False
            FreezeP3.ForeColor = Color.Yellow
            Invisible3.Checked = False
            Invisible3.ForeColor = Color.Yellow
            SetPointsP3.Checked = False
            SetPointsP3.ForeColor = Color.Yellow
            NoReload3.Checked = False
            NoReload3.ForeColor = Color.Yellow
            Cheats.Checked = False
            Cheats.ForeColor = Color.Yellow
            Zlot1.Checked = False
            Zlot1.ForeColor = Color.Yellow
            Zlot2.Checked = False
            Zlot2.ForeColor = Color.Yellow
            Zlot3.Checked = False
            Zlot3.ForeColor = Color.Yellow
        End If
    End Sub

    Private Sub iSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles iSend.Click
        If Recoil.Checked Then
            Mem.Patch(&H6563F8, "9090909090", "E8C312F1FF")
        End If
        If (sCommand.SelectedItem <> String.Empty) Then
            Write_Command(sCommand.SelectedItem)
        End If
        If (sCommand1.SelectedItem <> String.Empty) Then
            Write_Command(sCommand1.SelectedItem)
        End If
        If (sCommand2.SelectedItem <> String.Empty) Then
            Write_Command(sCommand2.SelectedItem)
        End If
        If (sCommand4.SelectedItem <> String.Empty) Then
            Write_Command(sCommand2.SelectedItem)
        End If
        If Recoil.Checked Then
            Mem.Patch(&H6563F8, "9090909090", "E8C312F1FF")
        End If
    End Sub

    Private Sub iUpdate()
        If (Mem.ProcActive) Then
            If (iConsole = -1) Then
                Dim bConsole As Byte() = Mem.rBytes(Mem.AobScan("BlackOps.exe+0", &H500000, "F705????????500100007415") + 2, 4)
                iConsole = Mem.Convert_Opcode(bConsole)
            End If
            If (iText = -1) Then
                Dim bText As Byte() = Mem.rBytes(Mem.AobScan("BlackOps.exe+0", &H200000, "803D????????007437") + 2, 4)
                iText = Mem.Convert_Opcode(bText)
            End If
        End If
    End Sub

    Private Sub Write_Command(ByVal To_Write As String)
        If (Mem.ProcActive AndAlso iConsole <> -1 AndAlso iConsole <> IntPtr.Zero AndAlso iText <> IntPtr.Zero) Then
            Dim iWrite As String = If(To_Write.StartsWith("/give"), To_Write, "/give" + To_Write)
            Mem.Write(iConsole, 1)
            Mem.WriteString(iText, iWrite)
            Mem.iEnter()
            System.Threading.Thread.Sleep(50)
            Mem.Write(iConsole, 0)
        End If
    End Sub

    Private Sub Write_Command1(ByVal To_Write As String)
        If (Mem.ProcActive AndAlso iConsole <> -1 AndAlso iConsole <> IntPtr.Zero AndAlso iText <> IntPtr.Zero) Then
            Dim iWrite As String = If(To_Write.StartsWith("/"), To_Write, "/" + To_Write)
            Mem.Write(iConsole, 1)
            Mem.WriteString(iText, iWrite)
            Mem.iEnter()
            System.Threading.Thread.Sleep(50)
            Mem.Write(iConsole, 0)
        End If
    End Sub

#Region "Level Changer"
    Private Sub Bt1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bt1.Click
        Dim Addy As Integer = -1
        If Mem.ProcActive Then
            Dim Lev As String = Mem.Int2Byt(iLevel.Value)
            Addy = Mem.Get_Addy(&H2AB00000, &HF0000, Lev + "????1200")
            If Addy <> -1 Then
                Change_Level(To_Level.Value, Addy)
                iLevel.Enabled = False
                Return
            End If
        End If
    End Sub

    Public Sub Change_Level(ByVal Lev As Integer, ByVal Address As Integer)
        Dim pBytes As Byte() = BitConverter.GetBytes(Lev)
        Mem.Write(Address, Lev)
    End Sub

#End Region

    Private Sub close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Close.Click, Close.Click
        End
    End Sub

    Private Sub Command_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Command.Click
        CommandCentre.Show()
    End Sub

    Private Sub Launch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Launch.Click
        Dim _tmp As String = "C:\Program Files (x86)\Steam\SteamApps\common\call of duty black ops\BlackOps.exe"
        If System.IO.File.Exists(_tmp) Then
            System.Diagnostics.Process.Start(_tmp)
            Return
        End If
        System.Diagnostics.Process.Start("C:\Program Files\Steam\SteamApps\common\call of duty black ops\BlackOps.exe")
    End Sub

#End Region

#Region "Player 1"

#Region "Custom Hacks"

    Private Sub Points_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Points.Click
        Mem.Write("BlackOps.exe+180A6C8", P1P.Value, GetType(Integer))
    End Sub

    Private Sub Headshot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Headshot.Click
        Mem.Write("BlackOps.exe+180A6EC", P1H.Value, GetType(Integer))
    End Sub

    Private Sub Kills_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Kills.Click
        Mem.Write("BlackOps.exe+180A6CC", P1K.Value, GetType(Integer))
    End Sub

    Private Sub Revives_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Revives.Click
        Mem.Write("BlackOps.exe+180A6E8", P1R.Value, GetType(Integer))
    End Sub

    Private Sub Downs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Downs.Click
        Mem.Write("BlackOps.exe+180A6E4", P1D.Value, GetType(Integer))
    End Sub

    Private Sub NameC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NameC.Click
        Mem.WriteString(&H1BF1464, P1N.Text)
        Mem.WriteString(&H1C0A678, P1N.Text)
        Mem.WriteString(&H1C0A5E0, P1N.Text)
    End Sub

#End Region

#Region "Weapons"

    Private Sub GiveWeapon1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GiveWeapon1.Click
        If Zlot1.Checked Then
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808D34")
            Mem.Write(XAddy, slot1)
        ElseIf Zlot2.Checked Then
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808D64")
            Mem.Write(XAddy, slot2)
        ElseIf Zlot3.Checked Then
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808D94")
            Mem.Write(XAddy, slot3)
        End If
    End Sub

#End Region

#Region "Set Hacks"

    Private Sub Invisible_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Invisible.CheckedChanged
        If Invisible.Checked Then
            Mem.Write("BlackOps.exe+1808C20", 131106, GetType(Integer))
        ElseIf Not Invisible.Checked Then
            Mem.Write("BlackOps.exe+1808C20", 131074, GetType(Integer))
        End If
        Invisible.ForeColor = IIf(Invisible.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Person_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Person.CheckedChanged
        If Person.Checked Then
            Mem.Write("2F67B84 18", "1", GetType(Integer))
        Else
            Mem.Write("2F67B84 18", "0", GetType(Integer))
        End If
        Person.ForeColor = IIf(Person.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Clipping_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clipping.CheckedChanged
        If Clipping.Checked = True Then
            Mem.Write("BlackOps.exe+180A74C", 1, GetType(Integer))
        ElseIf Clipping.Checked = False Then
            Mem.Write("BlackOps.exe+180A74C", 0, GetType(Integer))
        End If
        Clipping.ForeColor = IIf(Clipping.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub God_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles God.CheckedChanged
        If God.Checked Then
            Mem.Write("BlackOps.exe+1679868", 2081, GetType(Integer))
        Else
            Mem.Write("BlackOps.exe+1679868", 2080, GetType(Integer))
        End If
        God.ForeColor = IIf(God.Checked, Color.Lime, Color.Red)

    End Sub

    Private Sub FloatingDead_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FloatingDead.CheckedChanged
        If FloatingDead.Checked Then
            Mem.Write("23D2FA8 18", 22, GetType(Single))
        Else
            Mem.Write("23D2FA8 18", -800, GetType(Single))
        End If
        FloatingDead.ForeColor = IIf(FloatingDead.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Spread_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Spread.CheckedChanged
        Mem.Patch("406DEA", "90909090909090909090", "F783FC0400000000000C")
        If Spread.Checked Then
            Mem.Write("BDF31C 18", "1008981770", GetType(Integer))
        Else
            Mem.Write("BDF31C 18", "1059481190", GetType(Integer))
        End If
        Spread.ForeColor = IIf(Spread.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub HeadShots_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HeadShots.CheckedChanged
        HeadShots.ForeColor = IIf(HeadShots.Checked, Color.Lime, Color.Red)
    End Sub

    Public Sub Recoil_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Recoil.CheckedChanged
        Mem.Patch(&H6563F8, "9090909090", "E8C312F1FF")
        Recoil.ForeColor = IIf(Recoil.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Host_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Host.CheckedChanged
        If Host.Checked Then
            AllPlayers.Checked = False
        Else
            Mem.Write("BlackOps.exe+167987C", "100", GetType(Integer))
            Mem.Write("BlackOps.exe+1679880", "100", GetType(Integer))
        End If
        Host.ForeColor = IIf(Host.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub NoReload_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoReload.CheckedChanged
        If NoReload.Checked = False Then
            Mem.Write("BlackOps.exe+1808F08", "4", GetType(Integer))
            Mem.Write("BlackOps.exe+1808F10", "4", GetType(Integer))
            Mem.Write("BlackOps.exe+1808F20", "200", GetType(Integer))
            Mem.Write("BlackOps.exe+1808ea0", "200", GetType(Integer))
            Mem.Write("BlackOps.exe+1808F00", "200", GetType(Integer))
            Mem.Write("BlackOps.exe+1808e88", "200", GetType(Integer))
            Mem.Write("BlackOps.exe+1808F10", "200", GetType(Integer))
            Mem.Write("BlackOps.exe+1808e98", "200", GetType(Integer))
            Mem.Write("BlackOps.exe+1808F18", "200", GetType(Integer))
        End If
        NoReload.ForeColor = IIf(NoReload.Checked, Color.Lime, Color.Red)
    End Sub

#End Region

#Region "Teleport"

    Private Sub BigGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BigGo.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808B64")
        Mem.WriteU(XAddy, _X)
        Mem.WriteU(XAddy + 4, _Y)
        Mem.WriteU(XAddy + 8, _Z)
    End Sub

    Private Sub Kino_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Kino.SelectedIndexChanged
        Select Case Kino.SelectedIndex
            Case 0 'Box Location 1
                _X = 3212693021
                _Y = 3292055743
                _Z = 1131945984
                Return
            Case 1 'Box Location 2
                _X = 1146645979
                _Y = 3290070524
                _Z = 1134563328
                Return
            Case 2 'Box Location 3
                _X = 1154440641
                _Y = 1143675464
                _Z = 3246260224
                Return
            Case 3 'Box Location 4
                _X = 1152110897
                _Y = 1151623640
                _Z = 3246260224
                Return
            Case 4 'Box Location 5
                _X = 1064225273
                _Y = 1155749469
                _Z = 3246260224
                Return
            Case 5 'Box Location 6
                _X = 1105706802
                _Y = 1124511878
                _Z = 3248256085
                Return
            Case 6 'Box Location 7
                _X = 3299931507
                _Y = 1149037685
                _Z = 1126703104
                Return
            Case 7 'Box Location 8
                _X = 3300417523
                _Y = 1130716495
                _Z = 1093301506
                Return
            Case 8 'Box Location 9
                _X = 3298625715
                _Y = 3290364124
                _Z = 1117798400
                Return
            Case 9 'Power Switch
                _X = 3287763709
                _Y = 1151400387
                _Z = 3246260224
                Return
            Case 10 'Pack A Punch
                _X = 1079195374
                _Y = 3285505588
                _Z = 1134563328
                Return
            Case 11 'Seceret Location
                _X = 3308360456
                _Y = 3324475587
                _Z = 1164013255
                Return
            Case 12 'Time Travel Room 1
                _X = 1155542202
                _Y = 3306084356
                _Z = 1124605952
                Return
            Case 13 'Time Travel Room 2
                _X = 3307674791
                _Y = 1132931704
                _Z = 1112571904
                Return
            Case 14 'Time Travel Room 3
                _X = 3307437306
                _Y = 3297226817
                _Z = 1112571904
                Return
            Case 15 'Movie Room
                _X = 1151954339
                _Y = 1159510304
                _Z = 3283611648
                Return
        End Select
    End Sub

    Private Sub Customgo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Customgo.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808B64")
        Mem.writeF(XAddy, NX.Value)
        Mem.writeF(XAddy + 4, NY.Value)
        Mem.writeF(XAddy + 8, NZ.Value)
    End Sub

    Private Sub SavePos_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavePos.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808B64")
        SX = Mem.Read(XAddy, GetType(Single))
        SY = Mem.Read(XAddy + 4, GetType(Single))
        SZ = Mem.Read(XAddy + 8, GetType(Single))
    End Sub

    Private Sub LoadPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadPos.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808B64")
        Mem.writeF(XAddy, SX)
        Mem.writeF(XAddy + 4, SY)
        Mem.writeF(XAddy + 8, SZ)

    End Sub

    Private Sub Five_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Five.SelectedIndexChanged
        Select Case Five.SelectedIndex
            Case 0 'Box Location 1
                _X = 3286536639
                _Y = 1157782938
                _Z = 1098973184
                Return
            Case 1 'Box Location 2
                _X = 3299285551
                _Y = 1167777202
                _Z = 3291625472
                Return
            Case 2 'Box Location 3
                _X = 3297565453
                _Y = 1163979128
                _Z = 3291625472
                Return
            Case 3 'Box Location 4
                _X = 3287560608
                _Y = 1167435472
                _Z = 3291625472
                Return
            Case 4 'Box Location 5
                _X = 3290590332
                _Y = 1161223010
                _Z = 1098973184
                Return
            Case 5 'Box Location 6
                _X = 3295477200
                _Y = 1155055395
                _Z = 3288330240
                Return
            Case 6 'Power Switch
                _X = 3297600428
                _Y = 1166333330
                _Z = 3291625472
                Return
            Case 7 'Pack A Punch
                _X = 3302004682
                _Y = 1157667424
                _Z = 3288330240
                Return
            Case 8 'Pack A Punch Room
                _X = 3303610006
                _Y = 1157685332
                _Z = 3288330240
                Return
        End Select
    End Sub

    Private Sub Summon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Summon.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180A88C")
        Mem.writeF(XAddy, SX4)
        Mem.writeF(XAddy, SX4)
        Mem.writeF(XAddy + 4, SY4)
        Mem.writeF(XAddy + 8, SZ4)
        Dim XAddy1 As Integer = Mem._Module("BlackOps.exe+180C5B4")
        Mem.writeF(XAddy1, SX5)
        Mem.writeF(XAddy1 + 4, SY5)
        Mem.writeF(XAddy1 + 8, SZ5)
        Dim XAddy2 As Integer = Mem._Module("BlackOps.exe+180E2DC")
        Mem.writeF(XAddy2, SX6)
        Mem.writeF(XAddy2 + 4, SY6)
        Mem.writeF(XAddy2 + 8, SZ6)
    End Sub

    Private Sub PrePare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrePare.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808B64")
        SX4 = Mem.Read(XAddy, GetType(Single))
        SY4 = Mem.Read(XAddy + 4, GetType(Single))
        SZ4 = Mem.Read(XAddy + 8, GetType(Single))
        Dim XAddy1 As Integer = Mem._Module("BlackOps.exe+1808B64")
        SX5 = Mem.Read(XAddy1, GetType(Single))
        SY5 = Mem.Read(XAddy1 + 4, GetType(Single))
        SZ5 = Mem.Read(XAddy1 + 8, GetType(Single))
        Dim XAddy2 As Integer = Mem._Module("BlackOps.exe+1808B64")
        SX6 = Mem.Read(XAddy2, GetType(Single))
        SY6 = Mem.Read(XAddy2 + 4, GetType(Single))
        SZ6 = Mem.Read(XAddy2 + 8, GetType(Single))
    End Sub

    Private Sub MapPackloc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MapPackloc.SelectedIndexChanged
        Select Case MapPackloc.SelectedIndex
            Case 1 'Box Location 1
                _X = 3287053091
                _Y = 1152623926
                _Z = 1130242048
                Return
            Case 2 'Box Location 2
                _X = 3266221060
                _Y = 1152497414
                _Z = 1102118912
                Return
            Case 3 'Box Location 3
                _X = 1152879198
                _Y = 1151143987
                _Z = 1135546368
                Return
            Case 4 'Box Location 4
                _X = 1151214281
                _Y = 1154511247
                _Z = 3282715834
                Return
            Case 5 'Box Location 5
                _X = 3304703412
                _Y = 1157659361
                _Z = 3263676416
                Return
            Case 6 'Box Location 6
                _X = 1142591146
                _Y = 3282155307
                _Z = 3273908224
                Return
            Case 7 'Box Location 7
                _X = 1140143019
                _Y = 3304711903
                _Z = 3271015035
                Return
            Case 8 'Power switch
                _X = 3286572903
                _Y = 1148047331
                _Z = 1130242048
                Return
            Case 9 'Pack A Punch
                _X = 1139353191
                _Y = 1136833830
                _Z = 3281514496
                Return
            Case 10 'Seceret Location
                _X = 1153287963
                _Y = 1141433293
                _Z = 1154958336
                Return
            Case 13 'Box Location 1
                _X = 3305254083
                _Y = 1144418801
                _Z = 1094388142
                Return
            Case 14 'Box Location 2
                _X = 3290869114
                _Y = 1146106388
                _Z = 1140232192
                Return
            Case 15 'Box Location 3
                _X = 1126495541
                _Y = 1154327567
                _Z = 1146570752
                Return
            Case 16 'Box Location 4
                _X = 1089401378
                _Y = 1160815319
                _Z = 1160815319
                Return
            Case 17 'Box Location 5
                _X = 3289763144
                _Y = 3295883562
                _Z = 1139378471
                Return
            Case 18 'Box Location 6
                _X = 1158442784
                _Y = 3307902006
                _Z = 1134362922
                Return
            Case 19 'Power Switch
                _X = 3261329951
                _Y = 3288540857
                _Z = 3282628608
                Return
            Case 20 'Pack A Punch 1
                _X = 3253665792
                _Y = 3302804895
                _Z = 1153566683
                Return
            Case 21 'Pack A Punch 2
                _X = 3250520064
                _Y = 1123074055
                _Z = 3297199738
                Return
            Case 22 'Pack A Punch 3
                _X = 3250520064
                _Y = 1162000981
                _Z = 3281013194
                Return
            Case 25 'Box Location 1
                _X = 3261329951
                _Y = 3288540857
                _Z = 3282628608
                Return
            Case 26 'Box Location 2
                _X = 3302925482
                _Y = 3302521726
                _Z = 3287443122
                Return
            Case 27 'Box Location 3
                _X = 1150859563
                _Y = 3296786667
                _Z = 3284529152
                Return
            Case 28 'Box Location 4
                _X = 1150851713
                _Y = 3296684293
                _Z = 3284529152
                Return
            Case 29 'Power Switch
                _X = 1099584412
                _Y = 1111059268
                _Z = 3282694144
                Return
            Case 30 'Pack A Punch
                _X = 1094369875
                _Y = 1137359650
                _Z = 1133514752
                Return
            Case 33 'Moon Box Location 1
                _X = 1180410376
                _Y = 3327840880
                _Z = 3220176896
                Return
            Case 34 'Moon Box Location 2
                _X = 3264788442
                _Y = 1159097229
                _Z = 3289217024
                Return
            Case 35 'Moon Box Location 3
                _X = 1150704549
                _Y = 1166195413
                _Z = 3263370178
                Return
            Case 36 'Moon Box Location 4
                _X = 3286728681
                _Y = 1174660208
                _Z = 3222380841
                Return
            Case 37 'Power Switch
                _X = 3264788442
                _Y = 1159097229
                _Z = 3289217024
                Return
            Case 38 'Pack A Punch
                _X = 1180410376
                _Y = 3327840880
                _Z = 3290871808
                Return
            Case 41 'Verukt Box Locations 1
                _X = 1148429317
                _Y = 1144790534
                _Z = 1115701248
                Return
            Case 42 'Verukt Box Locations 2
                _X = 3288928911
                _Y = 3277058752
                _Z = 1130504192
                Return
            Case 43 'Verukt Box Locations 3
                _X = 3276577747
                _Y = 3288315807
                _Z = 1130504192
                Return
            Case 44 'Verukt Box Locations 4
                _X = 1148205784
                _Y = 3282236704
                _Z = 1130504192
                Return
            Case 45 'Verukt Box Locations 5
                _X = 1150170242
                _Y = 3291773678
                _Z = 1115701248
                Return
            Case 46 'Power Switch
                _X = 3288928911
                _Y = 3277058752
                _Z = 1130504192
                Return
            Case 47 'Fountain
                _X = 1133165676
                _Y = 1094504623
                _Z = 1113620480
                Return
            Case 50 'Nacht Box
                _X = 3269521879
                _Y = 1143768787
                _Z = 1066401792
                Return
            Case 51 'Outside
                _X = 3282792324
                _Y = 1135875759
                _Z = 3247339163
                Return
            Case 52 'Air Plane
                _X = 1147830014
                _Y = 1152365343
                _Z = 1124673572
                Return
            Case 53 'Sniper Post
                _X = 1156817721
                _Y = 1141180970
                _Z = 1122910208
                Return
            Case 56 'Shi no Box Location 1
                _X = 1176638231
                _Y = 1149111031
                _Z = 3288602624
                Return
            Case 57 'Shi no Box Location 2
                _X = 1175922064
                _Y = 1140567409
                _Z = 3290773504
                Return
            Case 58 'Shi no Box Location 3
                _X = 1173373260
                _Y = 3301839915
                _Z = 3291084800
                Return
            Case 59 'Shi no Box Location 4
                _X = 1174401350
                _Y = 1163001699
                _Z = 3290839040
                Return
            Case 60 'Shi no Box Location 5
                _X = 1178032876
                _Y = 1163000868
                _Z = 3290691584
                Return
            Case 61 'Shi no Box Location 6
                _X = 1179082204
                _Y = 3299918431
                _Z = 3290544128
                Return
            Case 64 'Der Riese box location 1
                _X = 1150530804
                _Y = 1148598533
                _Z = 1126703104
                Return
            Case 65 'Der Riese box location 2
                _X = 1143613701
                _Y = 3283954117
                _Z = 1115701248
                Return
            Case 66 'Der Riese box location 3
                _X = 1141971301
                _Y = 3300834174
                _Z = 1115701248
                Return
            Case 67 'Der Riese box location 4
                _X = 1107988556
                _Y = 3306741650
                _Z = 1132990464
                Return
            Case 68 'Der Riese box location 5
                _X = 3296791438
                _Y = 3299822462
                _Z = 1116094464
                Return
            Case 69 'Der Riese box location 6
                _X = 3298636930
                _Y = 3300975665
                _Z = 1128210432
                Return
            Case 70 'Der Riese Main Frame
                _X = 3256968842
                _Y = 1133261416
                _Z = 1120813056
                Return
            Case 71 'Der Riese Pack A Punch
                _X = 3264079004
                _Y = 1141298798
                _Z = 1120550912
                Return
            Case 72 'Der Riese Clock Tower
                _X = 1117200777
                _Y = 3294636293
                _Z = 1139347456
                Return
            Case 73 'Der Riese Fly Trap
                _X = 1142227096
                _Y = 1156985811
                _Z = 1116945145
                Return
        End Select
    End Sub

#End Region

#End Region

#Region "Player 2"

#Region "Custom Hacks"
    Private Sub P2P_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P2P.Click
        Mem.Write("BlackOps.exe+180C3F0", PointsP2.Value, GetType(Integer))
    End Sub

    Private Sub P2H_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P2H.Click
        Mem.Write("BlackOps.exe+180C414", HeadShotsP2.Value, GetType(Integer))
    End Sub

    Private Sub P2K_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P2K.Click
        Mem.Write("BlackOps.exe+180C3F4", KillsP2.Value, GetType(Integer))
    End Sub

    Private Sub P2R_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P2R.Click
        Mem.Write("BlackOps.exe+180C410", RevivesP2.Value, GetType(Integer))
    End Sub

    Private Sub P2D_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P2D.Click
        Mem.Write("BlackOps.exe+180C40C", DownsP2.Value, GetType(Integer))
    End Sub

    Private Sub NameChangeP2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NameChangeP2.Click
        Mem.WriteString(&HC18DB0, NameBoxP2.Text)
        Mem.WriteString(&H1C0C308, NameBoxP2.Text)
        Mem.WriteString(&H1C0C3A0, NameBoxP2.Text)
    End Sub
#End Region

#Region "Weapons"

    Private Sub GiveWeapon2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GiveWeapon2.Click
        If Zlot1.Checked Then
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+180AA5C")
            Mem.Write(XAddy, slot1)
        ElseIf Zlot2.Checked Then
            Dim XAddy As Integer = Mem._Module("Blackops.exe+180AA8C")
            Mem.Write(XAddy, slot2)
        ElseIf Zlot3.Checked Then
            Dim XAddy As Integer = Mem._Module("Blackops.exe+180AABC")
            Mem.Write(XAddy, slot3)
        End If
    End Sub

#End Region

#Region "Set Hacks"

    Private Sub Kick2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Kick2.CheckedChanged
        If Kick2.Checked Then
            Write_Command1("clientkick 1")
            TabPage1.Text = Nothing
            Headshots2.Checked = False
            Headshots2.ForeColor = Color.Yellow
            Invisible2.Checked = False
            Invisible2.ForeColor = Color.Yellow
            WeaponsP2.Checked = False
            WeaponsP2.ForeColor = Color.Yellow
            Clip1.Checked = False
            Clip1.ForeColor = Color.Yellow
            Freeze2.Checked = False
            Freeze2.ForeColor = Color.Yellow
            God1.Checked = False
            God1.ForeColor = Color.Yellow
            SetPointsP2.Checked = False
            SetPointsP2.ForeColor = Color.Yellow
            NoReload2.Checked = False
            NoReload2.ForeColor = Color.Yellow
            Kick2.Checked = False
        End If

    End Sub

    Private Sub Invisible2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Invisible2.CheckedChanged
        If Invisible2.Checked Then
            Mem.Write("BlackOps.exe+180A948", 131106, GetType(Integer))
        ElseIf Not Invisible2.Checked Then
            Mem.Write("BlackOps.exe+180A948", 131074, GetType(Integer))
        End If
        Invisible2.ForeColor = IIf(Invisible2.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Headshots2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Headshots2.CheckedChanged
        Headshots2.ForeColor = IIf(Headshots2.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub NoReload2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoReload2.CheckedChanged
        If NoReload2.Checked = False Then
            Mem.Write("BlackOps.exe+180AC30", 4, GetType(Integer))
            Mem.Write("BlackOps.exe+180AC28", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180AC38", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180AC40", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180ABC0", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180ABB0", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180ABC8", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180AC48", 200, GetType(Integer))
        End If
        NoReload2.ForeColor = IIf(NoReload2.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Clip1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clip1.CheckedChanged
        If Clip1.Checked = True Then
            Mem.Write("BlackOps.exe+180C474", 1, GetType(Integer))
        ElseIf Clip1.Checked = False Then
            Mem.Write("BlackOps.exe+180C474", 0, GetType(Integer))
        End If
        Clip1.ForeColor = IIf(Clip1.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Freeze2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Freeze2.CheckedChanged
        If Freeze2.Checked = True Then
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+180A88C")
            SX = Mem.ReadF(XAddy)
            SY = Mem.ReadF(XAddy + 4)
            SZ = Mem.ReadF(XAddy + 8)
        End If
        If Freeze2.Checked = False Then
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+180A88C")
            Mem.writeF(XAddy, SX)
            Mem.writeF(XAddy + 4, SY)
            Mem.writeF(XAddy + 8, SZ)
        End If
        Freeze2.ForeColor = IIf(Freeze2.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub SetPointsP2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetPointsP2.CheckedChanged
        If Not SetPointsP2.Checked Then
            Mem.Write("BlackOps.exe+180C3F0", 5, GetType(Integer))
            Mem.Write("BlackOps.exe+180C414", 5, GetType(Integer))
            Mem.Write("BlackOps.exe+180C3F4", 5, GetType(Integer))
            Mem.Write("BlackOps.exe+180C3F4", 5, GetType(Integer))
            Mem.Write("BlackOps.exe+180C410", 5, GetType(Integer))
        End If
        SetPointsP2.ForeColor = IIf(SetPointsP2.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub WeaponsP2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WeaponsP2.CheckedChanged
        If Not WeaponsP2.Checked Then
            Mem.Write("BlackOps.exe+180AA5C", 4, GetType(Integer))
            Mem.Write("Blackops.exe+180AA8C", 0, GetType(Integer))
            Mem.Write("Blackops.exe+180AABC", 0, GetType(Integer))
        End If
        WeaponsP2.ForeColor = IIf(WeaponsP2.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub God1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles God1.CheckedChanged
        If Not God1.Checked Then
            Mem.Write("BlackOps.exe+1679BC8", 100, GetType(Integer))
            Mem.Write("BlackOps.exe+1679BCC", 100, GetType(Integer))
        End If
        God1.ForeColor = IIf(God1.Checked, Color.Lime, Color.Red)
    End Sub
#End Region

#Region "Teleport"

    Private Sub GO2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GO2.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180A88C")
        Mem.WriteU(XAddy, _X1)
        Mem.WriteU(XAddy + 4, _Y1)
        Mem.WriteU(XAddy + 8, _Z1)
    End Sub

    Private Sub SavePositionP2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavePositionP2.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180A88C")
        P2_SX = Mem.ReadF(XAddy)
        P2_SY = Mem.ReadF(XAddy + 4)
        P2_SZ = Mem.ReadF(XAddy + 8)
    End Sub

    Private Sub LoadPositionP2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadPositionP2.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180A88C")
        Mem.writeF(XAddy, P2_SX)
        Mem.writeF(XAddy + 4, P2_SY)
        Mem.writeF(XAddy + 8, P2_SZ)
    End Sub

    Private Sub PrepareTeleportP2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrepareTeleportP2.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180A88C")
        P2_SX = Mem.ReadF(XAddy)
        P2_SY = Mem.ReadF(XAddy + 4)
        P2_SZ = Mem.ReadF(XAddy + 8)
    End Sub

    Private Sub TeleportP2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TeleportP2.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808B64")
        Mem.writeF(XAddy, P2_SX)
        Mem.writeF(XAddy + 4, P2_SY)
        Mem.writeF(XAddy + 8, P2_SZ)
    End Sub

    Private Sub PrepareSummonP2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrepareSummonP2.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808B64")
        P2_SX = Mem.ReadF(XAddy)
        P2_SY = Mem.ReadF(XAddy + 4)
        P2_SZ = Mem.ReadF(XAddy + 8)
    End Sub

    Private Sub SummonP2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummonP2.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180A88C")
        Mem.writeF(XAddy, P2_SX)
        Mem.writeF(XAddy + 4, P2_SY)
        Mem.writeF(XAddy + 8, P2_SZ)
    End Sub

    Private Sub sGO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sGO.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180A88C")
        Mem.writeF(XAddy, NX2.Value)
        Mem.writeF(XAddy + 4, NY2.Value)
        Mem.writeF(XAddy + 8, NZ2.Value)
    End Sub

#Region "Combo Boxes"

    Private Sub ComboBox6_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        Select Case ComboBox6.SelectedIndex
            Case 0 'Box Location 1
                _X1 = 3212693021
                _Y1 = 3292055743
                _Z1 = 1131945984
                Return
            Case 1 'Box Location 2
                _X1 = 1146645979
                _Y1 = 3290070524
                _Z1 = 1134563328
                Return
            Case 2 'Box Location 3
                _X1 = 1154440641
                _Y1 = 1143675464
                _Z1 = 3246260224
                Return
            Case 3 'Box Location 4
                _X1 = 1152110897
                _Y1 = 1151623640
                _Z1 = 3246260224
                Return
            Case 4 'Box Location 5
                _X1 = 1064225273
                _Y1 = 1155749469
                _Z1 = 3246260224
                Return
            Case 5 'Box Location 6
                _X1 = 1105706802
                _Y1 = 1124511878
                _Z1 = 3248256085
                Return
            Case 6 'Box Location 7
                _X1 = 3299931507
                _Y1 = 1149037685
                _Z1 = 1126703104
                Return
            Case 7 'Box Location 8
                _X1 = 3300417523
                _Y1 = 1130716495
                _Z1 = 1093301506
                Return
            Case 8 'Box Location 9
                _X1 = 3298625715
                _Y1 = 3290364124
                _Z1 = 1117798400
                Return
            Case 9 'Power Switch
                _X1 = 3287763709
                _Y1 = 1151400387
                _Z1 = 3246260224
                Return
            Case 10 'Pack A Punch
                _X1 = 1079195374
                _Y1 = 3285505588
                _Z1 = 1134563328
                Return
            Case 11 'Seceret Location
                _X1 = 3308360456
                _Y1 = 3324475587
                _Z1 = 1164013255
                Return
            Case 12 'Time Travel Room 1
                _X1 = 1155542202
                _Y1 = 3306084356
                _Z1 = 1124605952
                Return
            Case 13 'Time Travel Room 2
                _X1 = 3307674791
                _Y1 = 1132931704
                _Z1 = 1112571904
                Return
            Case 14 'Time Travel Room 3
                _X1 = 3307437306
                _Y1 = 3297226817
                _Z1 = 1112571904
                Return
            Case 15 'Movie Room
                _X1 = 1151954339
                _Y1 = 1159510304
                _Z1 = 3283611648
                Return
        End Select
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        Select Case ComboBox5.SelectedIndex
            Case 0 'Box Location 1
                _X1 = 3286536639
                _Y1 = 1157782938
                _Z1 = 1098973184
                Return
            Case 1 'Box Location 2
                _X1 = 3299285551
                _Y1 = 1167777202
                _Z1 = 3291625472
                Return
            Case 2 'Box Location 3
                _X1 = 3297565453
                _Y1 = 1163979128
                _Z1 = 3291625472
                Return
            Case 3 'Box Location 4
                _X1 = 3287560608
                _Y1 = 1167435472
                _Z1 = 3291625472
                Return
            Case 4 'Box Location 5
                _X1 = 3290590332
                _Y1 = 1161223010
                _Z1 = 1098973184
                Return
            Case 5 'Box Location 6
                _X1 = 3295477200
                _Y1 = 1155055395
                _Z1 = 3288330240
                Return
            Case 6 'Power Switch
                _X1 = 3297600428
                _Y1 = 1166333330
                _Z1 = 3291625472
                Return
            Case 7 'Pack A Punch
                _X1 = 3302004682
                _Y1 = 1157667424
                _Z1 = 3288330240
                Return
            Case 8 'Pack A Punch Room
                _X1 = 3303610006
                _Y1 = 1157685332
                _Z1 = 3288330240
                Return
        End Select
    End Sub

    Private Sub MapPack_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles MapPack.SelectedIndexChanged
        Select Case MapPack.SelectedIndex
            Case 1 'Box Location 1
                _X1 = 3287053091
                _Y1 = 1152623926
                _Z1 = 1130242048
                Return
            Case 2 'Box Location 2
                _X1 = 3266221060
                _Y1 = 1152497414
                _Z1 = 1102118912
                Return
            Case 3 'Box Location 3
                _X1 = 1152879198
                _Y1 = 1151143987
                _Z1 = 1135546368
                Return
            Case 4 'Box Location 4
                _X1 = 1151214281
                _Y1 = 1154511247
                _Z1 = 3282715834
                Return
            Case 5 'Box Location 5
                _X1 = 3304703412
                _Y1 = 1157659361
                _Z1 = 3263676416
                Return
            Case 6 'Box Location 6
                _X1 = 1142591146
                _Y1 = 3282155307
                _Z1 = 3273908224
                Return
            Case 7 'Box Location 7
                _X1 = 1140143019
                _Y1 = 3304711903
                _Z1 = 3271015035
                Return
            Case 8 'Power switch
                _X1 = 3286572903
                _Y1 = 1148047331
                _Z1 = 1130242048
                Return
            Case 9 'Pack A Punch
                _X1 = 1139353191
                _Y1 = 1136833830
                _Z1 = 3281514496
                Return
            Case 10 'Seceret Location
                _X1 = 1153287963
                _Y1 = 1141433293
                _Z1 = 1154958336
                Return
            Case 13 'Box Location 1
                _X1 = 3305254083
                _Y1 = 1144418801
                _Z1 = 1094388142
                Return
            Case 14 'Box Location 2
                _X1 = 3290869114
                _Y1 = 1146106388
                _Z1 = 1140232192
                Return
            Case 15 'Box Location 3
                _X1 = 1126495541
                _Y1 = 1154327567
                _Z1 = 1146570752
                Return
            Case 16 'Box Location 4
                _X1 = 1089401378
                _Y1 = 1160815319
                _Z1 = 1160815319
                Return
            Case 17 'Box Location 5
                _X1 = 3289763144
                _Y1 = 3295883562
                _Z1 = 1139378471
                Return
            Case 18 'Box Location 6
                _X1 = 1158442784
                _Y1 = 3307902006
                _Z1 = 1134362922
                Return
            Case 19 'Power Switch
                _X1 = 3261329951
                _Y1 = 3288540857
                _Z1 = 3282628608
                Return
            Case 20 'Pack A Punch 1
                _X1 = 3253665792
                _Y1 = 3302804895
                _Z1 = 1153566683
                Return
            Case 21 'Pack A Punch 2
                _X1 = 3250520064
                _Y1 = 1123074055
                _Z1 = 3297199738
                Return
            Case 22 'Pack A Punch 3
                _X1 = 3250520064
                _Y1 = 1162000981
                _Z1 = 3281013194
                Return
            Case 25 'Box Location 1
                _X1 = 3261329951
                _Y1 = 3288540857
                _Z1 = 3282628608
                Return
            Case 26 'Box Location 2
                _X1 = 3302925482
                _Y1 = 3302521726
                _Z1 = 3287443122
                Return
            Case 27 'Box Location 3
                _X1 = 1150859563
                _Y1 = 3296786667
                _Z1 = 3284529152
                Return
            Case 28 'Box Location 4
                _X1 = 1150851713
                _Y1 = 3296684293
                _Z1 = 3284529152
                Return
            Case 29 'Power Switch
                _X1 = 1099584412
                _Y1 = 1111059268
                _Z1 = 3282694144
                Return
            Case 30 'Pack A Punch
                _X1 = 1094369875
                _Y1 = 1137359650
                _Z1 = 1133514752
                Return
            Case 33 'Moon Box Location 1
                _X1 = 1180410376
                _Y1 = 3327840880
                _Z1 = 3220176896
                Return
            Case 34 'Moon Box Location 2
                _X1 = 3264788442
                _Y1 = 1159097229
                _Z1 = 3289217024
                Return
            Case 35 'Moon Box Location 3
                _X1 = 1150704549
                _Y1 = 1166195413
                _Z1 = 3263370178
                Return
            Case 36 'Moon Box Location 4
                _X1 = 3286728681
                _Y1 = 1174660208
                _Z1 = 3222380841
                Return
            Case 37 'Power Switch
                _X1 = 3264788442
                _Y1 = 1159097229
                _Z1 = 3289217024
                Return
            Case 38 'Pack A Punch
                _X1 = 1180410376
                _Y1 = 3327840880
                _Z1 = 3290871808
                Return
            Case 41 'Verukt Box Locations 1
                _X1 = 1148429317
                _Y1 = 1144790534
                _Z1 = 1115701248
                Return
            Case 42 'Verukt Box Locations 2
                _X1 = 3288928911
                _Y1 = 3277058752
                _Z1 = 1130504192
                Return
            Case 43 'Verukt Box Locations 3
                _X1 = 3276577747
                _Y1 = 3288315807
                _Z1 = 1130504192
                Return
            Case 44 'Verukt Box Locations 4
                _X1 = 1148205784
                _Y1 = 3282236704
                _Z1 = 1130504192
                Return
            Case 45 'Verukt Box Locations 5
                _X1 = 1150170242
                _Y1 = 3291773678
                _Z1 = 1115701248
                Return
            Case 46 'Power Switch
                _X1 = 3288928911
                _Y1 = 3277058752
                _Z1 = 1130504192
                Return
            Case 47 'Fountain
                _X1 = 1133165676
                _Y1 = 1094504623
                _Z1 = 1113620480
                Return
            Case 50 'Nacht Box
                _X1 = 3269521879
                _Y1 = 1143768787
                _Z1 = 1066401792
                Return
            Case 51 'Outside
                _X1 = 3282792324
                _Y1 = 1135875759
                _Z1 = 3247339163
                Return
            Case 52 'Air Plane
                _X1 = 1147830014
                _Y1 = 1152365343
                _Z1 = 1124673572
                Return
            Case 53 'Sniper Post
                _X1 = 1156817721
                _Y1 = 1141180970
                _Z1 = 1122910208
                Return
            Case 56 'Shi no Box Location 1
                _X1 = 1176638231
                _Y1 = 1149111031
                _Z1 = 3288602624
                Return
            Case 57 'Shi no Box Location 2
                _X1 = 1175922064
                _Y1 = 1140567409
                _Z1 = 3290773504
                Return
            Case 58 'Shi no Box Location 3
                _X1 = 1173373260
                _Y1 = 3301839915
                _Z1 = 3291084800
                Return
            Case 59 'Shi no Box Location 4
                _X1 = 1174401350
                _Y1 = 1163001699
                _Z1 = 3290839040
                Return
            Case 60 'Shi no Box Location 5
                _X1 = 1178032876
                _Y1 = 1163000868
                _Z1 = 3290691584
                Return
            Case 61 'Shi no Box Location 6
                _X1 = 1179082204
                _Y1 = 3299918431
                _Z1 = 3290544128
                Return
            Case 64 'Der Riese box location 1
                _X1 = 1150530804
                _Y1 = 1148598533
                _Z1 = 1126703104
                Return
            Case 65 'Der Riese box location 2
                _X1 = 1143613701
                _Y1 = 3283954117
                _Z1 = 1115701248
                Return
            Case 66 'Der Riese box location 3
                _X1 = 1141971301
                _Y1 = 3300834174
                _Z1 = 1115701248
                Return
            Case 67 'Der Riese box location 4
                _X1 = 1107988556
                _Y1 = 3306741650
                _Z1 = 1132990464
                Return
            Case 68 'Der Riese box location 5
                _X1 = 3296791438
                _Y1 = 3299822462
                _Z1 = 1116094464
                Return
            Case 69 'Der Riese box location 6
                _X1 = 3298636930
                _Y1 = 3300975665
                _Z1 = 1128210432
                Return
            Case 70 'Der Riese Main Frame
                _X1 = 3256968842
                _Y1 = 1133261416
                _Z1 = 1120813056
                Return
            Case 71 'Der Riese Pack A Punch
                _X1 = 3264079004
                _Y1 = 1141298798
                _Z1 = 1120550912
                Return
            Case 72 'Der Riese Clock Tower
                _X1 = 1117200777
                _Y1 = 3294636293
                _Z1 = 1139347456
                Return
            Case 73 'Der Riese Fly Trap
                _X1 = 1142227096
                _Y1 = 1156985811
                _Z1 = 1116945145
                Return
        End Select
    End Sub

#End Region

#End Region

#End Region

#Region "Player 3"

#Region "Custom Hacks"
    Private Sub P3P_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P3P.Click
        Mem.Write("BlackOps.exe+180E118", NP3P.Value, GetType(Integer))
    End Sub

    Private Sub P3H_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P3H.Click
        Mem.Write("BlackOps.exe+180E13C", NP3H.Value, GetType(Integer))
    End Sub

    Private Sub P3K_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P3K.Click
        Mem.Write("BlackOps.exe+180E11C", NP3K.Value, GetType(Integer))
    End Sub

    Private Sub P3R_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P3R.Click
        Mem.Write("BlackOps.exe+180E138", NP3R.Value, GetType(Integer))
    End Sub

    Private Sub P3D_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P3D.Click
        Mem.Write("BlackOps.exe+180E134", NP3D.Value, GetType(Integer))
    End Sub

    Private Sub NameChangeP3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NameChangeP3.Click
        Mem.WriteString(&HC18E18, TextBox1.Text)
        Mem.WriteString(&H1C0E030, TextBox1.Text)
        Mem.WriteString(&H1C0E0C8, TextBox1.Text)
    End Sub
#End Region

#Region "Weapons"

    Private Sub GiveWeapon3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GiveWeapon3.Click
        If Zlot1.Checked Then
            Dim XAddy As Integer = Mem._Module("Blackops.exe+180C784")
            Mem.Write(XAddy, slot1)
        ElseIf Zlot2.Checked Then
            Dim XAddy As Integer = Mem._Module("Blackops.exe+180C7B4")
            Mem.Write(XAddy, slot2)
        ElseIf Zlot3.Checked Then
            Dim XAddy As Integer = Mem._Module("Blackops.exe+180C7E4")
            Mem.Write(XAddy, slot3)
        End If
    End Sub

#End Region

#Region "Set Hacks"

    Private Sub Kick3_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Kick3.CheckedChanged
        If Kick3.Checked Then
            Write_Command1("clientkick 2")
            TabPage2.Text = Nothing
            GodP3.Checked = False
            GodP3.ForeColor = Color.Yellow
            ClipP3.Checked = False
            ClipP3.ForeColor = Color.Yellow
            HeadShots3.Checked = False
            HeadShots3.ForeColor = Color.Yellow
            WeaponsP3.Checked = False
            WeaponsP3.ForeColor = Color.Yellow
            FreezeP3.Checked = False
            FreezeP3.ForeColor = Color.Yellow
            Invisible3.Checked = False
            Invisible3.ForeColor = Color.Yellow
            SetPointsP3.Checked = False
            SetPointsP3.ForeColor = Color.Yellow
            NoReload3.Checked = False
            NoReload3.ForeColor = Color.Yellow
            Kick3.Checked = False
        End If
    End Sub

    Private Sub Invisible3_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Invisible3.CheckedChanged
        If Invisible3.Checked Then
            Mem.Write("BlackOps.exe+180C670", 131106, GetType(Integer))
        ElseIf Not Invisible3.Checked Then
            Mem.Write("BlackOps.exe+180C670", 131074, GetType(Integer))
        End If
        Invisible3.ForeColor = IIf(Invisible3.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub HeadShots3_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles HeadShots3.CheckedChanged
        HeadShots3.ForeColor = IIf(HeadShots3.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub NoReload3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoReload3.CheckedChanged
        If Not NoReload3.Checked Then
            Mem.Write("BlackOps.exe+180C958", 4, GetType(Integer))
            Mem.Write("BlackOps.exe+180C950", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180C960", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180C968", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180C8D8", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180C8F0", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180C8D0", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180C970", 200, GetType(Integer))
        End If
        NoReload3.ForeColor = IIf(NoReload3.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub ClipP3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClipP3.CheckedChanged
        If ClipP3.Checked = True Then
            Mem.Write("BlackOps.exe+180E19C", 1, GetType(Integer))
        ElseIf ClipP3.Checked = False Then
            Mem.Write("BlackOps.exe+180E19C", 0, GetType(Integer))
        End If
        ClipP3.ForeColor = IIf(ClipP3.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub FreezeP3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FreezeP3.CheckedChanged
        If FreezeP3.Checked Then
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+180C5B4")
            SX1 = Mem.ReadF(XAddy)
            SY1 = Mem.ReadF(XAddy + 4)
            SZ1 = Mem.ReadF(XAddy + 8)
        Else
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+180C5B4")
            Mem.writeF(XAddy, SX1)
            Mem.writeF(XAddy + 4, SY1)
            Mem.writeF(XAddy + 8, SZ1)
        End If
        FreezeP3.ForeColor = IIf(FreezeP3.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub SetPointsP3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetPointsP3.CheckedChanged
        If Not SetPointsP3.Checked Then
            Mem.Write("BlackOps.exe+180E118", 5, GetType(Integer))
            Mem.Write("BlackOps.exe+180E13C", 5, GetType(Integer))
            Mem.Write("BlackOps.exe+180E11C", 5, GetType(Integer))
            Mem.Write("BlackOps.exe+180E138", 5, GetType(Integer))
        End If
        SetPointsP3.ForeColor = IIf(SetPointsP3.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub WeaponsP3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WeaponsP3.CheckedChanged
        If Not WeaponsP3.Checked Then
            Mem.Write("Blackops.exe+180C784", 4, GetType(Integer))
            Mem.Write("Blackops.exe+180C7B4", 4, GetType(Integer))
            Mem.Write("Blackops.exe+180C7E4", 4, GetType(Integer))
        End If
        WeaponsP3.ForeColor = IIf(WeaponsP3.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub GodP3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GodP3.CheckedChanged
        If GodP3.Checked = False Then
            Mem.Write("BlackOps.exe+1679F14", 100, GetType(Integer))
            Mem.Write("BlackOps.exe+1679F18", 100, GetType(Integer))
        End If
        GodP3.ForeColor = IIf(GodP3.Checked, Color.Lime, Color.Red)
    End Sub
#End Region

#Region "Teleport"
    Private Sub GO3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GO3.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180C5B4")
        Mem.WriteU(XAddy, _X3)
        Mem.WriteU(XAddy + 4, _Y3)
        Mem.WriteU(XAddy + 8, _Z3)
    End Sub

    Private Sub SavePosP3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavePosP3.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180C5B4")
        SX1 = Mem.ReadF(XAddy)
        SY1 = Mem.ReadF(XAddy + 4)
        SZ1 = Mem.ReadF(XAddy + 8)
    End Sub

    Private Sub LoadPos3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadPos3.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180C5B4")
        Mem.writeF(XAddy, SX1)
        Mem.writeF(XAddy + 4, SY1)
        Mem.writeF(XAddy + 8, SZ1)
    End Sub

    Private Sub PrepareTeleportP3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrepareTeleportP3.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180C5B4")
        SX1 = Mem.ReadF(XAddy)
        SY1 = Mem.ReadF(XAddy + 4)
        SZ1 = Mem.ReadF(XAddy + 8)
    End Sub

    Private Sub TeleportP3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TeleportP3.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808B64")
        Mem.writeF(XAddy, SX1)
        Mem.writeF(XAddy + 4, SY1)
        Mem.writeF(XAddy + 8, SZ1)
    End Sub

    Private Sub PrepareSummonP3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrepareSummonP3.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808B64")
        SX1 = Mem.ReadF(XAddy)
        SY1 = Mem.ReadF(XAddy + 4)
        SZ1 = Mem.ReadF(XAddy + 8)
    End Sub

    Private Sub SummonP3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummonP3.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180C5B4")
        Mem.writeF(XAddy, SX1)
        Mem.writeF(XAddy + 4, SY1)
        Mem.writeF(XAddy + 8, SZ1)
    End Sub

    Private Sub sGO3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sGO3.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180C5B4")
        Mem.writeF(XAddy, NX1.Value)
        Mem.writeF(XAddy + 4, NY1.Value)
        Mem.writeF(XAddy + 8, NZ1.Value)
    End Sub
#Region "Combo Boxes"

    Private Sub Kino3_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Kino3.SelectedIndexChanged
        Select Case Kino3.SelectedIndex
            Case 0 'Box Location 1
                _X3 = 3212693021
                _Y3 = 3292055743
                _Z3 = 1131945984
                Return
            Case 1 'Box Location 2
                _X3 = 1146645979
                _Y3 = 3290070524
                _Z3 = 1134563328
                Return
            Case 2 'Box Location 3
                _X3 = 1154440641
                _Y3 = 1143675464
                _Z3 = 3246260224
                Return
            Case 3 'Box Location 4
                _X3 = 1152110897
                _Y3 = 1151623640
                _Z3 = 3246260224
                Return
            Case 4 'Box Location 5
                _X3 = 1064225273
                _Y3 = 1155749469
                _Z3 = 3246260224
                Return
            Case 5 'Box Location 6
                _X3 = 1105706802
                _Y3 = 1124511878
                _Z3 = 3248256085
                Return
            Case 6 'Box Location 7
                _X3 = 3299931507
                _Y3 = 1149037685
                _Z3 = 1126703104
                Return
            Case 7 'Box Location 8
                _X3 = 3300417523
                _Y3 = 1130716495
                _Z3 = 1093301506
                Return
            Case 8 'Box Location 9
                _X3 = 3298625715
                _Y3 = 3290364124
                _Z3 = 1117798400
                Return
            Case 9 'Power Switch
                _X3 = 3287763709
                _Y3 = 1151400387
                _Z3 = 3246260224
                Return
            Case 10 'Pack A Punch
                _X3 = 1079195374
                _Y3 = 3285505588
                _Z3 = 1134563328
                Return
            Case 11 'Seceret Location
                _X3 = 3308360456
                _Y3 = 3324475587
                _Z3 = 1164013255
                Return
            Case 12 'Time Travel Room 1
                _X3 = 1155542202
                _Y3 = 3306084356
                _Z3 = 1124605952
                Return
            Case 13 'Time Travel Room 2
                _X3 = 3307674791
                _Y3 = 1132931704
                _Z3 = 1112571904
                Return
            Case 14 'Time Travel Room 3
                _X3 = 3307437306
                _Y3 = 3297226817
                _Z3 = 1112571904
                Return
            Case 15 'Movie Room
                _X3 = 1151954339
                _Y3 = 1159510304
                _Z3 = 3283611648
                Return
        End Select
    End Sub

    Private Sub Five3_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Five3.SelectedIndexChanged
        Select Case Five3.SelectedIndex
            Case 0 'Box Location 1
                _X3 = 3286536639
                _Y3 = 1157782938
                _Z3 = 1098973184
                Return
            Case 1 'Box Location 2
                _X3 = 3299285551
                _Y3 = 1167777202
                _Z3 = 3291625472
                Return
            Case 2 'Box Location 3
                _X3 = 3297565453
                _Y3 = 1163979128
                _Z3 = 3291625472
                Return
            Case 3 'Box Location 4
                _X3 = 3287560608
                _Y3 = 1167435472
                _Z3 = 3291625472
                Return
            Case 4 'Box Location 5
                _X3 = 3290590332
                _Y3 = 1161223010
                _Z3 = 1098973184
                Return
            Case 5 'Box Location 6
                _X3 = 3295477200
                _Y3 = 1155055395
                _Z3 = 3288330240
                Return
            Case 6 'Power Switch
                _X3 = 3297600428
                _Y3 = 1166333330
                _Z3 = 3291625472
                Return
            Case 7 'Pack A Punch
                _X3 = 3302004682
                _Y3 = 1157667424
                _Z3 = 3288330240
                Return
            Case 8 'Pack A Punch Room
                _X3 = 3303610006
                _Y3 = 1157685332
                _Z3 = 3288330240
                Return
        End Select
    End Sub

    Private Sub MapPack1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles MapPack1.SelectedIndexChanged
        Select Case MapPack1.SelectedIndex
            Case 1 'Box Location 1
                _X3 = 3287053091
                _Y3 = 1152623926
                _Z3 = 1130242048
                Return
            Case 2 'Box Location 2
                _X3 = 3266221060
                _Y3 = 1152497414
                _Z3 = 1102118912
                Return
            Case 3 'Box Location 3
                _X3 = 1152879198
                _Y3 = 1151143987
                _Z3 = 1135546368
                Return
            Case 4 'Box Location 4
                _X3 = 1151214281
                _Y3 = 1154511247
                _Z3 = 3282715834
                Return
            Case 5 'Box Location 5
                _X3 = 3304703412
                _Y3 = 1157659361
                _Z3 = 3263676416
                Return
            Case 6 'Box Location 6
                _X3 = 1142591146
                _Y3 = 3282155307
                _Z3 = 3273908224
                Return
            Case 7 'Box Location 7
                _X3 = 1140143019
                _Y3 = 3304711903
                _Z3 = 3271015035
                Return
            Case 8 'Power switch
                _X3 = 3286572903
                _Y3 = 1148047331
                _Z3 = 1130242048
                Return
            Case 9 'Pack A Punch
                _X3 = 1139353191
                _Y3 = 1136833830
                _Z3 = 3281514496
                Return
            Case 10 'Seceret Location
                _X3 = 1153287963
                _Y3 = 1141433293
                _Z3 = 1154958336
                Return
            Case 13 'Box Location 1
                _X3 = 3305254083
                _Y3 = 1144418801
                _Z3 = 1094388142
                Return
            Case 14 'Box Location 2
                _X3 = 3290869114
                _Y3 = 1146106388
                _Z3 = 1140232192
                Return
            Case 15 'Box Location 3
                _X3 = 1126495541
                _Y3 = 1154327567
                _Z3 = 1146570752
                Return
            Case 16 'Box Location 4
                _X3 = 1089401378
                _Y3 = 1160815319
                _Z3 = 1160815319
                Return
            Case 17 'Box Location 5
                _X3 = 3289763144
                _Y3 = 3295883562
                _Z3 = 1139378471
                Return
            Case 18 'Box Location 6
                _X3 = 1158442784
                _Y3 = 3307902006
                _Z3 = 1134362922
                Return
            Case 19 'Power Switch
                _X3 = 3261329951
                _Y3 = 3288540857
                _Z3 = 3282628608
                Return
            Case 20 'Pack A Punch 1
                _X3 = 3253665792
                _Y3 = 3302804895
                _Z3 = 1153566683
                Return
            Case 21 'Pack A Punch 2
                _X3 = 3250520064
                _Y3 = 1123074055
                _Z3 = 3297199738
                Return
            Case 22 'Pack A Punch 3
                _X3 = 3250520064
                _Y3 = 1162000981
                _Z3 = 3281013194
                Return
            Case 25 'Box Location 1
                _X3 = 3261329951
                _Y3 = 3288540857
                _Z3 = 3282628608
                Return
            Case 26 'Box Location 2
                _X3 = 3302925482
                _Y3 = 3302521726
                _Z3 = 3287443122
                Return
            Case 27 'Box Location 3
                _X3 = 1150859563
                _Y3 = 3296786667
                _Z3 = 3284529152
                Return
            Case 28 'Box Location 4
                _X3 = 1150851713
                _Y3 = 3296684293
                _Z3 = 3284529152
                Return
            Case 29 'Power Switch
                _X3 = 1099584412
                _Y3 = 1111059268
                _Z3 = 3282694144
                Return
            Case 30 'Pack A Punch
                _X3 = 1094369875
                _Y3 = 1137359650
                _Z3 = 1133514752
                Return
            Case 33 'Moon Box Location 1
                _X3 = 1180410376
                _Y3 = 3327840880
                _Z3 = 3220176896
                Return
            Case 34 'Moon Box Location 2
                _X3 = 3264788442
                _Y3 = 1159097229
                _Z3 = 3289217024
                Return
            Case 35 'Moon Box Location 3
                _X3 = 1150704549
                _Y3 = 1166195413
                _Z3 = 3263370178
                Return
            Case 36 'Moon Box Location 4
                _X3 = 3286728681
                _Y3 = 1174660208
                _Z3 = 3222380841
                Return
            Case 37 'Power Switch
                _X3 = 3264788442
                _Y3 = 1159097229
                _Z3 = 3289217024
                Return
            Case 38 'Pack A Punch
                _X3 = 1180410376
                _Y3 = 3327840880
                _Z3 = 3290871808
                Return
            Case 41 'Verukt Box Locations 1
                _X3 = 1148429317
                _Y3 = 1144790534
                _Z3 = 1115701248
                Return
            Case 42 'Verukt Box Locations 2
                _X3 = 3288928911
                _Y3 = 3277058752
                _Z3 = 1130504192
                Return
            Case 43 'Verukt Box Locations 3
                _X3 = 3276577747
                _Y3 = 3288315807
                _Z3 = 1130504192
                Return
            Case 44 'Verukt Box Locations 4
                _X3 = 1148205784
                _Y3 = 3282236704
                _Z3 = 1130504192
                Return
            Case 45 'Verukt Box Locations 5
                _X3 = 1150170242
                _Y3 = 3291773678
                _Z3 = 1115701248
                Return
            Case 46 'Power Switch
                _X3 = 3288928911
                _Y3 = 3277058752
                _Z3 = 1130504192
                Return
            Case 47 'Fountain
                _X3 = 1133165676
                _Y3 = 1094504623
                _Z3 = 1113620480
                Return
            Case 50 'Nacht Box
                _X3 = 3269521879
                _Y3 = 1143768787
                _Z3 = 1066401792
                Return
            Case 51 'Outside
                _X3 = 3282792324
                _Y3 = 1135875759
                _Z3 = 3247339163
                Return
            Case 52 'Air Plane
                _X3 = 1147830014
                _Y3 = 1152365343
                _Z3 = 1124673572
                Return
            Case 53 'Sniper Post
                _X3 = 1156817721
                _Y3 = 1141180970
                _Z3 = 1122910208
                Return
            Case 56 'Shi no Box Location 1
                _X3 = 1176638231
                _Y3 = 1149111031
                _Z3 = 3288602624
                Return
            Case 57 'Shi no Box Location 2
                _X3 = 1175922064
                _Y3 = 1140567409
                _Z3 = 3290773504
                Return
            Case 58 'Shi no Box Location 3
                _X3 = 1173373260
                _Y3 = 3301839915
                _Z3 = 3291084800
                Return
            Case 59 'Shi no Box Location 4
                _X3 = 1174401350
                _Y3 = 1163001699
                _Z3 = 3290839040
                Return
            Case 60 'Shi no Box Location 5
                _X3 = 1178032876
                _Y3 = 1163000868
                _Z3 = 3290691584
                Return
            Case 61 'Shi no Box Location 6
                _X3 = 1179082204
                _Y3 = 3299918431
                _Z3 = 3290544128
                Return
            Case 64 'Der Riese box location 1
                _X3 = 1150530804
                _Y3 = 1148598533
                _Z3 = 1126703104
                Return
            Case 65 'Der Riese box location 2
                _X3 = 1143613701
                _Y3 = 3283954117
                _Z3 = 1115701248
                Return
            Case 66 'Der Riese box location 3
                _X3 = 1141971301
                _Y3 = 3300834174
                _Z3 = 1115701248
                Return
            Case 67 'Der Riese box location 4
                _X3 = 1107988556
                _Y3 = 3306741650
                _Z3 = 1132990464
                Return
            Case 68 'Der Riese box location 5
                _X3 = 3296791438
                _Y3 = 3299822462
                _Z3 = 1116094464
                Return
            Case 69 'Der Riese box location 6
                _X3 = 3298636930
                _Y3 = 3300975665
                _Z3 = 1128210432
                Return
            Case 70 'Der Riese Main Frame
                _X3 = 3256968842
                _Y3 = 1133261416
                _Z3 = 1120813056
                Return
            Case 71 'Der Riese Pack A Punch
                _X3 = 3264079004
                _Y3 = 1141298798
                _Z3 = 1120550912
                Return
            Case 72 'Der Riese Clock Tower
                _X3 = 1117200777
                _Y3 = 3294636293
                _Z3 = 1139347456
                Return
            Case 73 'Der Riese Fly Trap
                _X3 = 1142227096
                _Y3 = 1156985811
                _Z3 = 1116945145
                Return
        End Select
    End Sub

#End Region

#End Region

#End Region

#Region "Player 4"

#Region "Custom Hacks"

    Private Sub P4P_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P4P.Click
        Mem.Write("BlackOps.exe+180FE40", P4PP.Value, GetType(Integer))
    End Sub

    Private Sub P4H_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P4H.Click
        Mem.Write("BlackOps.exe+180FE64", P4PH.Value, GetType(Integer))
    End Sub

    Private Sub P4K_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P4K.Click, Summon.Click
        Mem.Write("BlackOps.exe+180FE44", P4PK.Value, GetType(Integer))
    End Sub

    Private Sub P4R_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P4R.Click
        Mem.Write("BlackOps.exe+180FE60", P4PR.Value, GetType(Integer))
    End Sub

    Private Sub P4D_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P4D.Click
        Mem.Write("BlackOps.exe+180FE5C", P4PD.Value, GetType(Integer))
    End Sub

    Private Sub Button26_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NameChangeP4.Click
        Mem.WriteString(&HC18E80, TextBox2.Text)
        Mem.WriteString(&HC18EAD, TextBox2.Text)
        Mem.WriteString(&HC0FE88, TextBox2.Text)
        Mem.WriteString(&H1C0FDF0, TextBox2.Text)
    End Sub
#End Region

#Region "Weapons"

    Private Sub GiveWeapon4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GiveWeapon4.Click
        If Zlot1.Checked Then
            Dim XAddy As Integer = Mem._Module("Blackops.exe+180E4AC")
            Mem.Write(XAddy, slot1)
        ElseIf Zlot2.Checked Then
            Dim XAddy As Integer = Mem._Module("Blackops.exe+180E4DC")
            Mem.Write(XAddy, slot2)
        ElseIf Zlot3.Checked Then
            Dim XAddy As Integer = Mem._Module("Blackops.exe+180E50C")
            Mem.Write(XAddy, slot3)
        End If
    End Sub

#End Region

#Region "Set Hacks"

    Private Sub Kick4_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Kick4.CheckedChanged
        If Kick4.Checked Then
            Write_Command1("clientkick 3")
            TabPage3.Text = Nothing
            HeadShots4.Checked = False
            HeadShots4.ForeColor = Color.Yellow
            Invisible4.Checked = False
            Invisible4.ForeColor = Color.Yellow
            God4.Checked = False
            God4.ForeColor = Color.Yellow
            WeaponsP4.Checked = False
            WeaponsP4.ForeColor = Color.Yellow
            SetPointsP4.Checked = False
            SetPointsP4.ForeColor = Color.Yellow
            FreezePlayerP4.Checked = False
            FreezePlayerP4.ForeColor = Color.Yellow
            Clip4.Checked = False
            Clip4.ForeColor = Color.Yellow
            NoReload4.Checked = False
            NoReload4.ForeColor = Color.Yellow
            Kick4.Checked = False

        End If

    End Sub

    Private Sub Invisible4_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Invisible4.CheckedChanged
        If Invisible4.Checked Then
            Mem.Write("BlackOps.exe+180E398", 131106, GetType(Integer))
        ElseIf Not Invisible4.Checked Then
            Mem.Write("BlackOps.exe+180E398", 131074, GetType(Integer))
        End If
        Invisible4.ForeColor = IIf(Invisible4.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub HeadShots4_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles HeadShots4.CheckedChanged
        HeadShots4.ForeColor = IIf(HeadShots4.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub NoReload4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoReload4.CheckedChanged
        If Not NoReload4.Checked Then
            Mem.Write("BlackOps.exe+180E680", 4, GetType(Integer))
            Mem.Write("BlackOps.exe+180E678", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180E688", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180E690", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180E600", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180E618", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180E5F8", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180E5F0", 200, GetType(Integer))
            Mem.Write("BlackOps.exe+180E698", 200, GetType(Integer))
        End If
        NoReload4.ForeColor = IIf(NoReload4.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Clip4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clip4.CheckedChanged
        If Clip4.Checked = True Then
            Mem.Write("BlackOps.exe+180FEC4", 1, GetType(Integer))
        ElseIf Clip4.Checked = False Then
            Mem.Write("BlackOps.exe+180FEC4", 0, GetType(Integer))
        End If
        Clip4.ForeColor = IIf(Clip4.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub FreezePlayerP4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FreezePlayerP4.CheckedChanged
        If FreezePlayerP4.Checked Then
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+180E2DC")
            SX2 = Mem.ReadF(XAddy)
            SY2 = Mem.ReadF(XAddy + 4)
            SZ2 = Mem.ReadF(XAddy + 8)
        Else
            Dim XAddy As Integer = Mem._Module("BlackOps.exe+180E2DC")
            Mem.writeF(XAddy, SX2)
            Mem.writeF(XAddy + 4, SY2)
            Mem.writeF(XAddy + 8, SZ2)
        End If
        FreezePlayerP4.ForeColor = IIf(FreezePlayerP4.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub SetPointsP4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetPointsP4.CheckedChanged
        If Not SetPointsP4.Checked Then
            Mem.Write("BlackOps.exe+180FE64", 5, GetType(Integer))
            Mem.Write("BlackOps.exe+180FE44", 5, GetType(Integer))
            Mem.Write("BlackOps.exe+180FE40", 5, GetType(Integer))
            Mem.Write("BlackOps.exe+180FE60", 5, GetType(Integer))
        End If
        SetPointsP4.ForeColor = IIf(SetPointsP4.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub WeaponsP4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WeaponsP4.CheckedChanged
        If Not WeaponsP4.Checked Then
            Mem.Write("Blackops.exe+180E4AC", 4, GetType(Integer))
            Mem.Write("Blackops.exe+180E4DC", 0, GetType(Integer))
            Mem.Write("Blackops.exe+180E50C", 0, GetType(Integer))
        End If
        WeaponsP4.ForeColor = IIf(WeaponsP4.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub God4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles God4.CheckedChanged
        If Not God4.Checked Then
            Mem.Write("BlackOps.exe+167A260", 100, GetType(Integer))
            Mem.Write("BlackOps.exe+167A264", 100, GetType(Integer))
        End If
        God4.ForeColor = IIf(God4.Checked, Color.Lime, Color.Red)
    End Sub

#End Region

#Region "Teleport"
    Private Sub GO4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GO4.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180E2DC")
        Mem.WriteU(XAddy, _X2)
        Mem.WriteU(XAddy + 4, _Y2)
        Mem.WriteU(XAddy + 8, _Z2)
    End Sub

    Private Sub SavePosP4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavePosP4.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180E2DC")
        SX2 = Mem.ReadF(XAddy)
        SY2 = Mem.ReadF(XAddy + 4)
        SZ2 = Mem.ReadF(XAddy + 8)
    End Sub

    Private Sub LoadPosP4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadPosP4.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180E2DC")
        Mem.writeF(XAddy, SX2)
        Mem.writeF(XAddy + 4, SY2)
        Mem.writeF(XAddy + 8, SZ2)
    End Sub

    Private Sub PrepareTeleportP4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrepareTeleportP4.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180E2DC")
        SX2 = Mem.ReadF(XAddy)
        SY2 = Mem.ReadF(XAddy + 4)
        SZ2 = Mem.ReadF(XAddy + 8)
    End Sub

    Private Sub TeleportP4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TeleportP4.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808B64")
        Mem.writeF(XAddy, SX2)
        Mem.writeF(XAddy + 4, SY2)
        Mem.writeF(XAddy + 8, SZ2)
    End Sub

    Private Sub PrepareSummonP4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrepareSummonP4.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+1808B64")
        SX2 = Mem.ReadF(XAddy)
        SY2 = Mem.ReadF(XAddy + 4)
        SZ2 = Mem.ReadF(XAddy + 8)
    End Sub

    Private Sub SummonP4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummonP4.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180E2DC")
        Mem.writeF(XAddy, SX2)
        Mem.writeF(XAddy + 4, SY2)
        Mem.writeF(XAddy + 8, SZ2)
    End Sub

    Private Sub sGO4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sGO4.Click
        Dim XAddy As Integer = Mem._Module("BlackOps.exe+180E2DC")
        Mem.writeF(XAddy, NX2.Value)
        Mem.writeF(XAddy + 4, NY2.Value)
        Mem.writeF(XAddy + 8, NZ2.Value)
    End Sub

#Region "Combo Boxes"
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.SelectedIndex
            Case 0 'Box Location 1
                _X2 = 3212693021
                _Y2 = 3292055743
                _Z2 = 1131945984
                Return
            Case 1 'Box Location 2
                _X2 = 1146645979
                _Y2 = 3290070524
                _Z2 = 1134563328
                Return
            Case 2 'Box Location 3
                _X2 = 1154440641
                _Y2 = 1143675464
                _Z2 = 3246260224
                Return
            Case 3 'Box Location 4
                _X2 = 1152110897
                _Y2 = 1151623640
                _Z2 = 3246260224
                Return
            Case 4 'Box Location 5
                _X2 = 1064225273
                _Y2 = 1155749469
                _Z2 = 3246260224
                Return
            Case 5 'Box Location 6
                _X2 = 1105706802
                _Y2 = 1124511878
                _Z2 = 3248256085
                Return
            Case 6 'Box Location 7
                _X2 = 3299931507
                _Y2 = 1149037685
                _Z2 = 1126703104
                Return
            Case 7 'Box Location 8
                _X2 = 3300417523
                _Y2 = 1130716495
                _Z2 = 1093301506
                Return
            Case 8 'Box Location 9
                _X2 = 3298625715
                _Y2 = 3290364124
                _Z2 = 1117798400
                Return
            Case 9 'Power Switch
                _X2 = 3287763709
                _Y2 = 1151400387
                _Z2 = 3246260224
                Return
            Case 10 'Pack A Punch
                _X2 = 1079195374
                _Y2 = 3285505588
                _Z2 = 1134563328
                Return
            Case 11 'Seceret Location
                _X2 = 3308360456
                _Y2 = 3324475587
                _Z2 = 1164013255
                Return
            Case 12 'Time Travel Room 1
                _X2 = 1155542202
                _Y2 = 3306084356
                _Z2 = 1124605952
                Return
            Case 13 'Time Travel Room 2
                _X2 = 3307674791
                _Y2 = 1132931704
                _Z2 = 1112571904
                Return
            Case 14 'Time Travel Room 3
                _X2 = 3307437306
                _Y2 = 3297226817
                _Z2 = 1112571904
                Return
            Case 15 'Movie Room
                _X2 = 1151954339
                _Y2 = 1159510304
                _Z2 = 3283611648
                Return
        End Select
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Select Case ComboBox2.SelectedIndex
            Case 0 'Box Location 1
                _X2 = 3286536639
                _Y2 = 1157782938
                _Z2 = 1098973184
                Return
            Case 1 'Box Location 2
                _X2 = 3299285551
                _Y2 = 1167777202
                _Z2 = 3291625472
                Return
            Case 2 'Box Location 3
                _X2 = 3297565453
                _Y2 = 1163979128
                _Z2 = 3291625472
                Return
            Case 3 'Box Location 4
                _X2 = 3287560608
                _Y2 = 1167435472
                _Z2 = 3291625472
                Return
            Case 4 'Box Location 5
                _X2 = 3290590332
                _Y2 = 1161223010
                _Z2 = 1098973184
                Return
            Case 5 'Box Location 6
                _X2 = 3295477200
                _Y2 = 1155055395
                _Z2 = 3288330240
                Return
            Case 6 'Power Switch
                _X2 = 3297600428
                _Y2 = 1166333330
                _Z2 = 3291625472
                Return
            Case 7 'Pack A Punch
                _X2 = 3302004682
                _Y2 = 1157667424
                _Z2 = 3288330240
                Return
            Case 8 'Pack A Punch Room
                _X2 = 3303610006
                _Y2 = 1157685332
                _Z2 = 3288330240
                Return
        End Select
    End Sub

    Private Sub mappack2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MapPack2.SelectedIndexChanged
        Select Case MapPack2.SelectedIndex
            Case 1 'Box Location 1
                _X2 = 3287053091
                _Y2 = 1152623926
                _Z2 = 1130242048
                Return
            Case 2 'Box Location 2
                _X2 = 3266221060
                _Y2 = 1152497414
                _Z2 = 1102118912
                Return
            Case 3 'Box Location 3
                _X2 = 1152879198
                _Y2 = 1151143987
                _Z2 = 1135546368
                Return
            Case 4 'Box Location 4
                _X2 = 1151214281
                _Y2 = 1154511247
                _Z2 = 3282715834
                Return
            Case 5 'Box Location 5
                _X2 = 3304703412
                _Y2 = 1157659361
                _Z2 = 3263676416
                Return
            Case 6 'Box Location 6
                _X2 = 1142591146
                _Y2 = 3282155307
                _Z2 = 3273908224
                Return
            Case 7 'Box Location 7
                _X2 = 1140143019
                _Y2 = 3304711903
                _Z2 = 3271015035
                Return
            Case 8 'Power switch
                _X2 = 3286572903
                _Y2 = 1148047331
                _Z2 = 1130242048
                Return
            Case 9 'Pack A Punch
                _X2 = 1139353191
                _Y2 = 1136833830
                _Z2 = 3281514496
                Return
            Case 10 'Seceret Location
                _X2 = 1153287963
                _Y2 = 1141433293
                _Z2 = 1154958336
                Return
            Case 13 'Box Location 1
                _X2 = 3305254083
                _Y2 = 1144418801
                _Z2 = 1094388142
                Return
            Case 14 'Box Location 2
                _X2 = 3290869114
                _Y2 = 1146106388
                _Z2 = 1140232192
                Return
            Case 15 'Box Location 3
                _X2 = 1126495541
                _Y2 = 1154327567
                _Z2 = 1146570752
                Return
            Case 16 'Box Location 4
                _X2 = 1089401378
                _Y2 = 1160815319
                _Z2 = 1160815319
                Return
            Case 17 'Box Location 5
                _X2 = 3289763144
                _Y2 = 3295883562
                _Z2 = 1139378471
                Return
            Case 18 'Box Location 6
                _X2 = 1158442784
                _Y2 = 3307902006
                _Z2 = 1134362922
                Return
            Case 19 'Power Switch
                _X2 = 3261329951
                _Y2 = 3288540857
                _Z2 = 3282628608
                Return
            Case 20 'Pack A Punch 1
                _X2 = 3253665792
                _Y2 = 3302804895
                _Z2 = 1153566683
                Return
            Case 21 'Pack A Punch 2
                _X2 = 3250520064
                _Y2 = 1123074055
                _Z2 = 3297199738
                Return
            Case 22 'Pack A Punch 3
                _X2 = 3250520064
                _Y2 = 1162000981
                _Z2 = 3281013194
                Return
            Case 25 'Box Location 1
                _X2 = 3261329951
                _Y2 = 3288540857
                _Z2 = 3282628608
                Return
            Case 26 'Box Location 2
                _X2 = 3302925482
                _Y2 = 3302521726
                _Z2 = 3287443122
                Return
            Case 27 'Box Location 3
                _X2 = 1150859563
                _Y2 = 3296786667
                _Z2 = 3284529152
                Return
            Case 28 'Box Location 4
                _X2 = 1150851713
                _Y2 = 3296684293
                _Z2 = 3284529152
                Return
            Case 29 'Power Switch
                _X2 = 1099584412
                _Y2 = 1111059268
                _Z2 = 3282694144
                Return
            Case 30 'Pack A Punch
                _X2 = 1094369875
                _Y2 = 1137359650
                _Z2 = 1133514752
                Return
            Case 33 'Moon Box Location 1
                _X2 = 1180410376
                _Y2 = 3327840880
                _Z2 = 3220176896
                Return
            Case 34 'Moon Box Location 2
                _X2 = 3264788442
                _Y2 = 1159097229
                _Z2 = 3289217024
                Return
            Case 35 'Moon Box Location 3
                _X2 = 1150704549
                _Y2 = 1166195413
                _Z2 = 3263370178
                Return
            Case 36 'Moon Box Location 4
                _X2 = 3286728681
                _Y2 = 1174660208
                _Z2 = 3222380841
                Return
            Case 37 'Power Switch
                _X2 = 3264788442
                _Y2 = 1159097229
                _Z2 = 3289217024
                Return
            Case 38 'Pack A Punch
                _X2 = 1180410376
                _Y2 = 3327840880
                _Z2 = 3290871808
                Return
            Case 41 'Verukt Box Locations 1
                _X2 = 1148429317
                _Y2 = 1144790534
                _Z2 = 1115701248
                Return
            Case 42 'Verukt Box Locations 2
                _X2 = 3288928911
                _Y2 = 3277058752
                _Z2 = 1130504192
                Return
            Case 43 'Verukt Box Locations 3
                _X2 = 3276577747
                _Y2 = 3288315807
                _Z2 = 1130504192
                Return
            Case 44 'Verukt Box Locations 4
                _X2 = 1148205784
                _Y2 = 3282236704
                _Z2 = 1130504192
                Return
            Case 45 'Verukt Box Locations 5
                _X2 = 1150170242
                _Y2 = 3291773678
                _Z2 = 1115701248
                Return
            Case 46 'Power Switch
                _X2 = 3288928911
                _Y2 = 3277058752
                _Z2 = 1130504192
                Return
            Case 47 'Fountain
                _X2 = 1133165676
                _Y2 = 1094504623
                _Z2 = 1113620480
                Return
            Case 50 'Nacht Box
                _X2 = 3269521879
                _Y2 = 1143768787
                _Z2 = 1066401792
                Return
            Case 51 'Outside
                _X2 = 3282792324
                _Y2 = 1135875759
                _Z2 = 3247339163
                Return
            Case 52 'Air Plane
                _X2 = 1147830014
                _Y2 = 1152365343
                _Z2 = 1124673572
                Return
            Case 53 'Sniper Post
                _X2 = 1156817721
                _Y2 = 1141180970
                _Z2 = 1122910208
                Return
            Case 56 'Shi no Box Location 1
                _X2 = 1176638231
                _Y2 = 1149111031
                _Z2 = 3288602624
                Return
            Case 57 'Shi no Box Location 2
                _X2 = 1175922064
                _Y2 = 1140567409
                _Z2 = 3290773504
                Return
            Case 58 'Shi no Box Location 3
                _X2 = 1173373260
                _Y2 = 3301839915
                _Z2 = 3291084800
                Return
            Case 59 'Shi no Box Location 4
                _X2 = 1174401350
                _Y2 = 1163001699
                _Z2 = 3290839040
                Return
            Case 60 'Shi no Box Location 5
                _X2 = 1178032876
                _Y2 = 1163000868
                _Z2 = 3290691584
                Return
            Case 61 'Shi no Box Location 6
                _X2 = 1179082204
                _Y2 = 3299918431
                _Z2 = 3290544128
                Return
            Case 64 'Der Riese box location 1
                _X2 = 1150530804
                _Y2 = 1148598533
                _Z2 = 1126703104
                Return
            Case 65 'Der Riese box location 2
                _X2 = 1143613701
                _Y2 = 3283954117
                _Z2 = 1115701248
                Return
            Case 66 'Der Riese box location 3
                _X2 = 1141971301
                _Y2 = 3300834174
                _Z2 = 1115701248
                Return
            Case 67 'Der Riese box location 4
                _X2 = 1107988556
                _Y2 = 3306741650
                _Z2 = 1132990464
                Return
            Case 68 'Der Riese box location 5
                _X2 = 3296791438
                _Y2 = 3299822462
                _Z2 = 1116094464
                Return
            Case 69 'Der Riese box location 6
                _X2 = 3298636930
                _Y2 = 3300975665
                _Z2 = 1128210432
                Return
            Case 70 'Der Riese Main Frame
                _X2 = 3256968842
                _Y2 = 1133261416
                _Z2 = 1120813056
                Return
            Case 71 'Der Riese Pack A Punch
                _X2 = 3264079004
                _Y2 = 1141298798
                _Z2 = 1120550912
                Return
            Case 72 'Der Riese Clock Tower
                _X2 = 1117200777
                _Y2 = 3294636293
                _Z2 = 1139347456
                Return
            Case 73 'Der Riese Fly Trap
                _X2 = 1142227096
                _Y2 = 1156985811
                _Z2 = 1116945145
                Return
        End Select
    End Sub
#End Region


#End Region


#End Region

#Region "All Players"

#Region "Set Hacks"

    Private Sub KillZombies_Click(sender As System.Object, e As System.EventArgs) Handles KillZombies.Click
        Cheats.Checked = True
        Write_Command1("ai axis delete")
        Cheats.Checked = False
        Cheats.ForeColor = Color.Yellow
    End Sub

    Private Sub Force_CheckedChanged_1(sender As System.Object, e As System.EventArgs) Handles Force.CheckedChanged
        If Force.Checked Then
            Mem.Write("2F61844 18", 0, GetType(Integer))
        ElseIf Not Force.Checked Then
            Mem.Write("2F61844 18", 6000, GetType(Integer))
        End If
        Force.ForeColor = IIf(Force.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Box_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles box.CheckedChanged
        If box.Checked Then
            Write_Command1("magic_chest_movable 0")
        Else
            Write_Command1("magic_chest_movable 1")
        End If
        box.ForeColor = IIf(box.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Trigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trigger.CheckedChanged
        Mem.Inject("76999B", "C7463C00000000", "837E3C00746A", True)
        Trigger.ForeColor = IIf(Trigger.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Fast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Fast.CheckedChanged
        If Fast.Checked Then
            Slow.Checked = False
            Mem.Write("248173C 18", "1073741824", GetType(Integer))
        Else
            Mem.Write("248173C 18", "1065353216", GetType(Integer))
        End If
        Fast.ForeColor = IIf(Fast.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Slow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Slow.CheckedChanged
        If Slow.Checked Then
            Fast.Checked = False
            Mem.Write("248173C 18", "1036831949", GetType(Integer))
        Else
            Mem.Write("248173C 18", "1065353216", GetType(Integer))
        End If
        Slow.ForeColor = IIf(Slow.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Instantkill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Instantkill.CheckedChanged
        Mem.Inject("5C8B39", "C7868401000000000000", "89BE84010000", True)
        Instantkill.ForeColor = IIf(Instantkill.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub AllPlayers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllPlayers.CheckedChanged
        If AllPlayers.Checked Then
            Host.Checked = False
        End If
        Mem.Patch("7DADD0", "909090909090", "898584010000")
        AllPlayers.ForeColor = IIf(AllPlayers.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Cheats_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Cheats.CheckedChanged
        If Cheats.Checked Then
            Mem.Write("2899CF4 18", "1", GetType(Integer))
        ElseIf Not Cheats.Checked Then
            Mem.Write("2899CF4 18", "0", GetType(Integer))
        End If
        Cheats.ForeColor = IIf(Cheats.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Melee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Melee.CheckedChanged
        If Melee.Checked = True Then
            Mem.Write("BCAFE4 18", 9999, GetType(Single))
        Else
            Mem.Write("BCAFE4 18", 64, GetType(Single))
        End If
        Melee.ForeColor = IIf(Melee.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Gravity_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Gravity.CheckedChanged
        If Gravity.Checked Then
            Mem.Write("BCAEFC 18", "1092616192", GetType(Integer))
        Else
            Mem.Write("BCAEFC 18", "1145569280", GetType(Integer))
        End If
        Gravity.ForeColor = IIf(Gravity.Checked, Color.Lime, Color.Red)
    End Sub

#End Region

#Region "Weapons"

    Private Sub Zlot1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Zlot1.CheckedChanged
        If Zlot1.Checked = True Then
            Zlot3.Checked = False
            Zlot2.Checked = False
        End If
        Zlot1.ForeColor = IIf(Zlot1.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Zlot2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Zlot2.CheckedChanged
        If Zlot2.Checked = True Then
            Zlot3.Checked = False
            Zlot1.Checked = False
        End If
        Zlot2.ForeColor = IIf(Zlot2.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub Zlot3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Zlot3.CheckedChanged
        If Zlot3.Checked = True Then
            Zlot2.Checked = False
            Zlot1.Checked = False
        End If
        Zlot3.ForeColor = IIf(Zlot3.Checked, Color.Lime, Color.Red)
    End Sub

    Private Sub MapPackWeapons_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MapPackWeapons.SelectedIndexChanged
        Select Case MapPackWeapons.SelectedIndex
            Case 1 'Death Machine
                slot1 = 80
                slot2 = 80
                slot3 = 80
                Return
            Case 4 'Death Machine
                slot1 = 85
                slot2 = 85
                slot3 = 85
                Return
            Case 7 'Death Machine
                slot1 = 83
                slot2 = 83
                slot3 = 83
                Return
        End Select
    End Sub

    Private Sub FiveWeapons_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FiveWeapons.SelectedIndexChanged
        Select Case FiveWeapons.SelectedIndex
            Case 0 'Death Machine
                slot1 = 79
                slot2 = 79
                slot3 = 79
                Return
            Case 1 'M1911 Pistol
                slot1 = 3
                slot2 = 3
                slot3 = 3
        End Select
    End Sub

    Private Sub KinoWeapons_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KinoWeapons.SelectedIndexChanged
        Select Case KinoWeapons.SelectedIndex
            Case 0 'Turret Gun
                slot1 = 2
                slot2 = 2
                slot3 = 2
                Return
            Case 1 'M1911 Pistol
                slot1 = 4
                slot2 = 4
                slot3 = 4
        End Select
    End Sub

#End Region

#Region "Custom Hacks"

    Private Sub Speed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Speed.Click
        Mem.Write("1C01810 18", NumericUpDown1.Value, GetType(Integer))
    End Sub

#End Region

#End Region

End Class

