''' <summary>
''' Contains global shared variables and configuration settings used throughout
''' the application, including memory access objects, player states, timers,
''' dictionaries for game data, and UI control defaults.
''' </summary>
Module Globals

    ''' <summary>
    ''' Shared instance of the ProcMem class used for memory operations across the application.
    ''' </summary>
    Public BlackOpsObject As New ProcMem()

    ''' <summary>
    ''' Address pointer indicating where the console enable flag is located in memory.
    ''' Initialized to -1 to indicate not yet located.
    ''' </summary>
    Public ConsoleEnabledAddress As IntPtr = -1

    ''' <summary>
    ''' Address pointer indicating where the console text buffer is located in memory.
    ''' Initialized to -1 to indicate not yet located.
    ''' </summary>
    Public ConsoleTextAddress As IntPtr = -1

    ''' <summary>
    ''' Flag indicating whether the in-game console is currently visible.
    ''' </summary>
    Public ConsoleVisible As Boolean

    ''' <summary>
    ''' Flag indicating if console-related memory addresses have been successfully found.
    ''' </summary>
    Public ConsoleAddressesLocated As Boolean

    ''' <summary>
    ''' Flag to enable or disable online cheats functionality.
    ''' </summary>
    Public EnableOnlineCheats As Boolean

    ''' <summary>
    ''' Flag indicating if Player 1 is enabled (active) in the trainer.
    ''' </summary>
    Public P1_PlayerEnabled As Boolean = False

    ''' <summary>
    ''' Flag indicating if Player 2 is enabled (active) in the trainer.
    ''' </summary>
    Public P2_PlayerEnabled As Boolean = False

    ''' <summary>
    ''' Flag indicating if Player 3 is enabled (active) in the trainer.
    ''' </summary>
    Public P3_PlayerEnabled As Boolean = False

    ''' <summary>
    ''' Flag indicating if Player 4 is enabled (active) in the trainer.
    ''' </summary>
    Public P4_PlayerEnabled As Boolean = False

    ''' <summary>
    ''' Dictionary mapping location names to lists of location entries for tracking player or object positions.
    ''' </summary>
    Public LocationDictionary As Dictionary(Of String, List(Of LocationEntry))

    ''' <summary>
    ''' Dictionary mapping map names to their associated weapon categories.
    ''' </summary>
    Public MapWeaponsDictionary As Dictionary(Of String, WeaponCategories)

    ''' <summary>
    ''' Dictionary mapping weapon internal names to their display names or categories.
    ''' </summary>
    Public WeaponsDictionary As Dictionary(Of String, String)

    ''' <summary>
    ''' Offset value used when teleporting players, representing the distance to move them.
    ''' Default is 30.0; TODO: makie configurable.
    ''' </summary>
    Public PlayerTeleportOffsetValue As Single = 30.0F ' TODO: Configurable

    ''' <summary>
    ''' Frequency in milliseconds for the main timer tick event.
    ''' </summary>
    Public MainTimerFrequency As Integer = 100

    ''' <summary>
    ''' Interval in milliseconds at which player health is checked.
    ''' </summary>
    Public HealthCheckRate As Integer = 15

    ''' <summary>
    ''' Interval in milliseconds at which player points are checked.
    ''' </summary>
    Public PointsCheckRate As Integer = 25

    ''' <summary>
    ''' Interval in milliseconds at which location tracking updates occur.
    ''' </summary>
    Public LocationTrackingRate As Integer = 50

    ''' <summary>
    ''' Dictionary mapping UI controls to their default value data, used for resetting controls.
    ''' </summary>
    Public ControlDefaultValues As New Dictionary(Of Control, ControlDefaults)

End Module

