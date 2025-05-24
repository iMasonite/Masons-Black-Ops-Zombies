#Region "Data Types"

''' <summary>
''' Represents a named location with floating-point coordinates.
''' </summary>
Public Class LocationEntry
    ''' <summary>
    ''' Gets or sets the name of the location.
    ''' </summary>
    Public Property Name As String

    ''' <summary>
    ''' Gets or sets the X coordinate of the location.
    ''' </summary>
    Public Property X As Single

    ''' <summary>
    ''' Gets or sets the Y coordinate of the location.
    ''' </summary>
    Public Property Y As Single

    ''' <summary>
    ''' Gets or sets the Z coordinate of the location.
    ''' </summary>
    Public Property Z As Single

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LocationEntry"/> class with the specified name and coordinates.
    ''' </summary>
    ''' <param name="name">The name of the location.</param>
    ''' <param name="X">The X coordinate.</param>
    ''' <param name="Y">The Y coordinate.</param>
    ''' <param name="Z">The Z coordinate.</param>
    Public Sub New(name As String, X As Single, Y As Single, Z As Single)
        Me.Name = name
        Me.X = X
        Me.Y = Y
        Me.Z = Z
    End Sub

    ''' <summary>
    ''' Returns the string representation of the location, which is its name.
    ''' </summary>
    ''' <returns>The name of the location.</returns>
    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class


''' <summary>
''' Represents a weapon entry with an ID, numeric value, and display name.
''' </summary>
Public Class WeaponEntry
    ''' <summary>
    ''' Gets or sets the weapon's string identifier.
    ''' </summary>
    Public Property WeaponId As String

    ''' <summary>
    ''' Gets or sets the weapon's integer identifier value.
    ''' </summary>
    Public Property WeaponIdValue As Integer

    ''' <summary>
    ''' Gets or sets the display name of the weapon.
    ''' </summary>
    Public Property DisplayName As String

    ''' <summary>
    ''' Returns the string representation of the weapon, which is its display name.
    ''' This is used for display in UI elements like ComboBox dropdowns.
    ''' </summary>
    ''' <returns>The display name of the weapon.</returns>
    Public Overrides Function ToString() As String
        Return DisplayName
    End Function
End Class


''' <summary>
''' Contains categorized collections of weapons by type.
''' </summary>
Public Class WeaponCategories
    ''' <summary>
    ''' Gets or sets the list of special weapons, each represented as a dictionary mapping string keys to integer values.
    ''' </summary>
    Public Property SpecialWeapons As List(Of Dictionary(Of String, Integer))

    ''' <summary>
    ''' Gets or sets the list of tactical weapon names.
    ''' </summary>
    Public Property Tacticals As List(Of String)

    ''' <summary>
    ''' Gets or sets the list of wonder weapon names.
    ''' </summary>
    Public Property WonderWeapons As List(Of String)

    ''' <summary>
    ''' Gets or sets the list of regular weapon names.
    ''' </summary>
    Public Property RegularWeapons As List(Of String)

    ''' <summary>
    ''' Gets or sets the list of upgraded weapon names.
    ''' </summary>
    Public Property UpgradedWeapons As List(Of String)

    ''' <summary>
    ''' Initializes a new instance of the <see cref="WeaponCategories"/> class,
    ''' with all weapon lists initialized to empty lists.
    ''' </summary>
    Public Sub New()
        SpecialWeapons = New List(Of Dictionary(Of String, Integer))()
        Tacticals = New List(Of String)()
        WonderWeapons = New List(Of String)()
        RegularWeapons = New List(Of String)()
        UpgradedWeapons = New List(Of String)()
    End Sub
End Class


''' <summary>
''' Represents a three-dimensional vector with single-precision floating point components.
''' </summary>
Public Structure Vector3F
    ''' <summary>
    ''' The X coordinate.
    ''' </summary>
    Public X As Single

    ''' <summary>
    ''' The Y coordinate.
    ''' </summary>
    Public Y As Single

    ''' <summary>
    ''' The Z coordinate.
    ''' </summary>
    Public Z As Single

    ''' <summary>
    ''' Initializes a new instance of the <see cref="Vector3F"/> structure with the specified coordinates.
    ''' </summary>
    ''' <param name="x">The X coordinate.</param>
    ''' <param name="y">The Y coordinate.</param>
    ''' <param name="z">The Z coordinate.</param>
    Public Sub New(x As Single, y As Single, z As Single)
        Me.X = x
        Me.Y = y
        Me.Z = z
    End Sub
End Structure

Public Structure ControlDefaults
    Public Text As String            ' For Button, TextBox, CheckBox (Text property)
    Public Checked As Boolean?       ' For CheckBox (Nullable)
    Public ForeColor As Color        ' ForeColor for CheckBox and maybe others
    Public Enabled As Boolean        ' Enabled state for all controls
    Public NumericValue As Decimal?  ' For NumericUpDown (Nullable)
    Public TextBoxText As String     ' For TextBox Text property
    Public ComboBoxSelectedIndex As Integer? ' For ComboBox SelectedIndex (Nullable)
End Structure

#End Region ' Data Types