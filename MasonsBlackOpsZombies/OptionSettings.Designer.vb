<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptionSettings))
        Me.Btn_Close = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label_levelChanger_Desc = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Nub_Round_Current = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Btn_ActionChangeLevel = New System.Windows.Forms.Button()
        Me.Nub_Round_Next = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox_LocationTrackingRate = New System.Windows.Forms.GroupBox()
        Me.Tbx_LocationTrackingRate_Desc = New System.Windows.Forms.TextBox()
        Me.Tbx_LocationTrackingRate = New System.Windows.Forms.TextBox()
        Me.TrackBar_LocationTrackingRate = New System.Windows.Forms.TrackBar()
        Me.Tbx_Info1 = New System.Windows.Forms.TextBox()
        Me.GroupBox_PointsCheckRate = New System.Windows.Forms.GroupBox()
        Me.Tbx_PointsCheckRate_Desc = New System.Windows.Forms.TextBox()
        Me.Tbx_PointsCheckRate = New System.Windows.Forms.TextBox()
        Me.TrackBar_PointsCheckRate = New System.Windows.Forms.TrackBar()
        Me.GroupBox_HealthCheckRate = New System.Windows.Forms.GroupBox()
        Me.Tbx_HealthCheckRate_Desc = New System.Windows.Forms.TextBox()
        Me.Tbx_HealthCheckRate = New System.Windows.Forms.TextBox()
        Me.TrackBar_HealthCheckRate = New System.Windows.Forms.TrackBar()
        Me.GroupBox_MainTimerFrequency = New System.Windows.Forms.GroupBox()
        Me.Tbx_MainTimerFrequency_Desc = New System.Windows.Forms.TextBox()
        Me.Tbx_MainTimerFrequency = New System.Windows.Forms.TextBox()
        Me.TrackBar_MainTimerFrequency = New System.Windows.Forms.TrackBar()
        Me.Btn_MainTimerFrequency_Reset = New System.Windows.Forms.Button()
        Me.Btn_HealthCheckRate_Reset = New System.Windows.Forms.Button()
        Me.Btn_PointsCheckRate_Reset = New System.Windows.Forms.Button()
        Me.Btn_LocationTrackingRate_Reset = New System.Windows.Forms.Button()
        Me.GroupBox3.SuspendLayout()
        CType(Me.Nub_Round_Current, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Nub_Round_Next, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox_LocationTrackingRate.SuspendLayout()
        CType(Me.TrackBar_LocationTrackingRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_PointsCheckRate.SuspendLayout()
        CType(Me.TrackBar_PointsCheckRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_HealthCheckRate.SuspendLayout()
        CType(Me.TrackBar_HealthCheckRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_MainTimerFrequency.SuspendLayout()
        CType(Me.TrackBar_MainTimerFrequency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Btn_Close
        '
        Me.Btn_Close.AutoSize = True
        Me.Btn_Close.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Close.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!)
        Me.Btn_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Btn_Close.Location = New System.Drawing.Point(14, 690)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(293, 32)
        Me.Btn_Close.TabIndex = 5
        Me.Btn_Close.Text = "CLOSE"
        Me.Btn_Close.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.Label_levelChanger_Desc)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Nub_Round_Current)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Btn_ActionChangeLevel)
        Me.GroupBox3.Controls.Add(Me.Nub_Round_Next)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.GroupBox3.Location = New System.Drawing.Point(14, 14)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(293, 183)
        Me.GroupBox3.TabIndex = 115
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Level Changer"
        '
        'Label_levelChanger_Desc
        '
        Me.Label_levelChanger_Desc.AutoSize = True
        Me.Label_levelChanger_Desc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_levelChanger_Desc.Location = New System.Drawing.Point(51, 97)
        Me.Label_levelChanger_Desc.Name = "Label_levelChanger_Desc"
        Me.Label_levelChanger_Desc.Size = New System.Drawing.Size(193, 39)
        Me.Label_levelChanger_Desc.TabIndex = 112
        Me.Label_levelChanger_Desc.Text = "Wait until LEVEL 2 Before use. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "E.g. Current Level 2 Next Level 19 " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Will change" &
    " on current round complete."
        Me.Label_levelChanger_Desc.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(6, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 18)
        Me.Label3.TabIndex = 110
        Me.Label3.Text = "Current Level"
        '
        'Nub_Round_Current
        '
        Me.Nub_Round_Current.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Nub_Round_Current.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Nub_Round_Current.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Nub_Round_Current.Location = New System.Drawing.Point(107, 28)
        Me.Nub_Round_Current.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.Nub_Round_Current.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.Nub_Round_Current.Name = "Nub_Round_Current"
        Me.Nub_Round_Current.Size = New System.Drawing.Size(180, 24)
        Me.Nub_Round_Current.TabIndex = 108
        Me.Nub_Round_Current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Nub_Round_Current.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(6, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 18)
        Me.Label2.TabIndex = 109
        Me.Label2.Text = "Next Level"
        '
        'Btn_ActionChangeLevel
        '
        Me.Btn_ActionChangeLevel.AutoSize = True
        Me.Btn_ActionChangeLevel.BackColor = System.Drawing.Color.Transparent
        Me.Btn_ActionChangeLevel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Btn_ActionChangeLevel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Btn_ActionChangeLevel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_ActionChangeLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_ActionChangeLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.25!)
        Me.Btn_ActionChangeLevel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.Btn_ActionChangeLevel.Location = New System.Drawing.Point(8, 139)
        Me.Btn_ActionChangeLevel.Name = "Btn_ActionChangeLevel"
        Me.Btn_ActionChangeLevel.Size = New System.Drawing.Size(279, 29)
        Me.Btn_ActionChangeLevel.TabIndex = 111
        Me.Btn_ActionChangeLevel.Text = "Change Level"
        Me.Btn_ActionChangeLevel.UseVisualStyleBackColor = False
        '
        'Nub_Round_Next
        '
        Me.Nub_Round_Next.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Nub_Round_Next.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Nub_Round_Next.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Nub_Round_Next.Location = New System.Drawing.Point(107, 58)
        Me.Nub_Round_Next.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.Nub_Round_Next.Name = "Nub_Round_Next"
        Me.Nub_Round_Next.Size = New System.Drawing.Size(180, 24)
        Me.Nub_Round_Next.TabIndex = 107
        Me.Nub_Round_Next.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Nub_Round_Next.Value = New Decimal(New Integer() {19, 0, 0, 0})
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.GroupBox_LocationTrackingRate)
        Me.GroupBox1.Controls.Add(Me.Tbx_Info1)
        Me.GroupBox1.Controls.Add(Me.GroupBox_PointsCheckRate)
        Me.GroupBox1.Controls.Add(Me.GroupBox_HealthCheckRate)
        Me.GroupBox1.Controls.Add(Me.GroupBox_MainTimerFrequency)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(317, 14)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(789, 708)
        Me.GroupBox1.TabIndex = 116
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Application Settings"
        '
        'GroupBox_LocationTrackingRate
        '
        Me.GroupBox_LocationTrackingRate.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox_LocationTrackingRate.Controls.Add(Me.Btn_LocationTrackingRate_Reset)
        Me.GroupBox_LocationTrackingRate.Controls.Add(Me.Tbx_LocationTrackingRate_Desc)
        Me.GroupBox_LocationTrackingRate.Controls.Add(Me.Tbx_LocationTrackingRate)
        Me.GroupBox_LocationTrackingRate.Controls.Add(Me.TrackBar_LocationTrackingRate)
        Me.GroupBox_LocationTrackingRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox_LocationTrackingRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.GroupBox_LocationTrackingRate.Location = New System.Drawing.Point(8, 445)
        Me.GroupBox_LocationTrackingRate.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupBox_LocationTrackingRate.Name = "GroupBox_LocationTrackingRate"
        Me.GroupBox_LocationTrackingRate.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.GroupBox_LocationTrackingRate.Size = New System.Drawing.Size(773, 130)
        Me.GroupBox_LocationTrackingRate.TabIndex = 194
        Me.GroupBox_LocationTrackingRate.TabStop = False
        Me.GroupBox_LocationTrackingRate.Text = "Location Tracking Rate"
        '
        'Tbx_LocationTrackingRate_Desc
        '
        Me.Tbx_LocationTrackingRate_Desc.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Tbx_LocationTrackingRate_Desc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Tbx_LocationTrackingRate_Desc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.Tbx_LocationTrackingRate_Desc.Location = New System.Drawing.Point(9, 21)
        Me.Tbx_LocationTrackingRate_Desc.Margin = New System.Windows.Forms.Padding(0)
        Me.Tbx_LocationTrackingRate_Desc.Multiline = True
        Me.Tbx_LocationTrackingRate_Desc.Name = "Tbx_LocationTrackingRate_Desc"
        Me.Tbx_LocationTrackingRate_Desc.ReadOnly = True
        Me.Tbx_LocationTrackingRate_Desc.Size = New System.Drawing.Size(687, 50)
        Me.Tbx_LocationTrackingRate_Desc.TabIndex = 192
        Me.Tbx_LocationTrackingRate_Desc.Text = "Specifies how often the trainer reads player positions, used for features like di" &
    "splaying coordinates or saving/loading teleport locations."
        '
        'Tbx_LocationTrackingRate
        '
        Me.Tbx_LocationTrackingRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Tbx_LocationTrackingRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tbx_LocationTrackingRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Tbx_LocationTrackingRate.Location = New System.Drawing.Point(702, 71)
        Me.Tbx_LocationTrackingRate.Name = "Tbx_LocationTrackingRate"
        Me.Tbx_LocationTrackingRate.ReadOnly = True
        Me.Tbx_LocationTrackingRate.Size = New System.Drawing.Size(65, 24)
        Me.Tbx_LocationTrackingRate.TabIndex = 190
        Me.Tbx_LocationTrackingRate.Text = "500ms"
        Me.Tbx_LocationTrackingRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TrackBar_LocationTrackingRate
        '
        Me.TrackBar_LocationTrackingRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.TrackBar_LocationTrackingRate.LargeChange = 10
        Me.TrackBar_LocationTrackingRate.Location = New System.Drawing.Point(9, 74)
        Me.TrackBar_LocationTrackingRate.Maximum = 100
        Me.TrackBar_LocationTrackingRate.Minimum = 5
        Me.TrackBar_LocationTrackingRate.Name = "TrackBar_LocationTrackingRate"
        Me.TrackBar_LocationTrackingRate.Size = New System.Drawing.Size(690, 45)
        Me.TrackBar_LocationTrackingRate.TabIndex = 0
        Me.TrackBar_LocationTrackingRate.TickFrequency = 5
        Me.TrackBar_LocationTrackingRate.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.TrackBar_LocationTrackingRate.Value = 50
        '
        'Tbx_Info1
        '
        Me.Tbx_Info1.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Tbx_Info1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Tbx_Info1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.25!)
        Me.Tbx_Info1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.Tbx_Info1.Location = New System.Drawing.Point(17, 655)
        Me.Tbx_Info1.Margin = New System.Windows.Forms.Padding(0)
        Me.Tbx_Info1.Multiline = True
        Me.Tbx_Info1.Name = "Tbx_Info1"
        Me.Tbx_Info1.ReadOnly = True
        Me.Tbx_Info1.Size = New System.Drawing.Size(758, 50)
        Me.Tbx_Info1.TabIndex = 193
        Me.Tbx_Info1.Text = "Lower values = Faster checks for better accuracy and responsiveness, but may caus" &
    "e lag or crashes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Recommended: Use a balanced value. Start with a moderate speed" &
    " and adjust based on performance."
        Me.Tbx_Info1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox_PointsCheckRate
        '
        Me.GroupBox_PointsCheckRate.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox_PointsCheckRate.Controls.Add(Me.Btn_PointsCheckRate_Reset)
        Me.GroupBox_PointsCheckRate.Controls.Add(Me.Tbx_PointsCheckRate_Desc)
        Me.GroupBox_PointsCheckRate.Controls.Add(Me.Tbx_PointsCheckRate)
        Me.GroupBox_PointsCheckRate.Controls.Add(Me.TrackBar_PointsCheckRate)
        Me.GroupBox_PointsCheckRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox_PointsCheckRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.GroupBox_PointsCheckRate.Location = New System.Drawing.Point(8, 305)
        Me.GroupBox_PointsCheckRate.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupBox_PointsCheckRate.Name = "GroupBox_PointsCheckRate"
        Me.GroupBox_PointsCheckRate.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.GroupBox_PointsCheckRate.Size = New System.Drawing.Size(773, 130)
        Me.GroupBox_PointsCheckRate.TabIndex = 119
        Me.GroupBox_PointsCheckRate.TabStop = False
        Me.GroupBox_PointsCheckRate.Text = "Points Check Rate"
        '
        'Tbx_PointsCheckRate_Desc
        '
        Me.Tbx_PointsCheckRate_Desc.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Tbx_PointsCheckRate_Desc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Tbx_PointsCheckRate_Desc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.Tbx_PointsCheckRate_Desc.Location = New System.Drawing.Point(9, 21)
        Me.Tbx_PointsCheckRate_Desc.Margin = New System.Windows.Forms.Padding(0)
        Me.Tbx_PointsCheckRate_Desc.Multiline = True
        Me.Tbx_PointsCheckRate_Desc.Name = "Tbx_PointsCheckRate_Desc"
        Me.Tbx_PointsCheckRate_Desc.ReadOnly = True
        Me.Tbx_PointsCheckRate_Desc.Size = New System.Drawing.Size(687, 50)
        Me.Tbx_PointsCheckRate_Desc.TabIndex = 192
        Me.Tbx_PointsCheckRate_Desc.Text = "Defines how often player points are checked. This is important for features like " &
    "Extra Stab Points and Free Mystery Box access."
        '
        'Tbx_PointsCheckRate
        '
        Me.Tbx_PointsCheckRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Tbx_PointsCheckRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tbx_PointsCheckRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Tbx_PointsCheckRate.Location = New System.Drawing.Point(702, 71)
        Me.Tbx_PointsCheckRate.Name = "Tbx_PointsCheckRate"
        Me.Tbx_PointsCheckRate.ReadOnly = True
        Me.Tbx_PointsCheckRate.Size = New System.Drawing.Size(65, 24)
        Me.Tbx_PointsCheckRate.TabIndex = 190
        Me.Tbx_PointsCheckRate.Text = "250ms"
        Me.Tbx_PointsCheckRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TrackBar_PointsCheckRate
        '
        Me.TrackBar_PointsCheckRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.TrackBar_PointsCheckRate.LargeChange = 10
        Me.TrackBar_PointsCheckRate.Location = New System.Drawing.Point(9, 74)
        Me.TrackBar_PointsCheckRate.Maximum = 100
        Me.TrackBar_PointsCheckRate.Minimum = 5
        Me.TrackBar_PointsCheckRate.Name = "TrackBar_PointsCheckRate"
        Me.TrackBar_PointsCheckRate.Size = New System.Drawing.Size(690, 45)
        Me.TrackBar_PointsCheckRate.TabIndex = 0
        Me.TrackBar_PointsCheckRate.TickFrequency = 5
        Me.TrackBar_PointsCheckRate.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.TrackBar_PointsCheckRate.Value = 25
        '
        'GroupBox_HealthCheckRate
        '
        Me.GroupBox_HealthCheckRate.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox_HealthCheckRate.Controls.Add(Me.Btn_HealthCheckRate_Reset)
        Me.GroupBox_HealthCheckRate.Controls.Add(Me.Tbx_HealthCheckRate_Desc)
        Me.GroupBox_HealthCheckRate.Controls.Add(Me.Tbx_HealthCheckRate)
        Me.GroupBox_HealthCheckRate.Controls.Add(Me.TrackBar_HealthCheckRate)
        Me.GroupBox_HealthCheckRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox_HealthCheckRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.GroupBox_HealthCheckRate.Location = New System.Drawing.Point(8, 165)
        Me.GroupBox_HealthCheckRate.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupBox_HealthCheckRate.Name = "GroupBox_HealthCheckRate"
        Me.GroupBox_HealthCheckRate.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.GroupBox_HealthCheckRate.Size = New System.Drawing.Size(773, 130)
        Me.GroupBox_HealthCheckRate.TabIndex = 118
        Me.GroupBox_HealthCheckRate.TabStop = False
        Me.GroupBox_HealthCheckRate.Text = "Health Check Rate"
        '
        'Tbx_HealthCheckRate_Desc
        '
        Me.Tbx_HealthCheckRate_Desc.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Tbx_HealthCheckRate_Desc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Tbx_HealthCheckRate_Desc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.Tbx_HealthCheckRate_Desc.Location = New System.Drawing.Point(9, 21)
        Me.Tbx_HealthCheckRate_Desc.Margin = New System.Windows.Forms.Padding(0)
        Me.Tbx_HealthCheckRate_Desc.Multiline = True
        Me.Tbx_HealthCheckRate_Desc.Name = "Tbx_HealthCheckRate_Desc"
        Me.Tbx_HealthCheckRate_Desc.ReadOnly = True
        Me.Tbx_HealthCheckRate_Desc.Size = New System.Drawing.Size(687, 50)
        Me.Tbx_HealthCheckRate_Desc.TabIndex = 191
        Me.Tbx_HealthCheckRate_Desc.Text = "Determines how often the trainer checks player health, used for features like God" &
    " Health and Jesus Health."
        '
        'Tbx_HealthCheckRate
        '
        Me.Tbx_HealthCheckRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Tbx_HealthCheckRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tbx_HealthCheckRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Tbx_HealthCheckRate.Location = New System.Drawing.Point(702, 71)
        Me.Tbx_HealthCheckRate.Name = "Tbx_HealthCheckRate"
        Me.Tbx_HealthCheckRate.ReadOnly = True
        Me.Tbx_HealthCheckRate.Size = New System.Drawing.Size(65, 24)
        Me.Tbx_HealthCheckRate.TabIndex = 190
        Me.Tbx_HealthCheckRate.Text = "150ms"
        Me.Tbx_HealthCheckRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TrackBar_HealthCheckRate
        '
        Me.TrackBar_HealthCheckRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.TrackBar_HealthCheckRate.LargeChange = 10
        Me.TrackBar_HealthCheckRate.Location = New System.Drawing.Point(9, 74)
        Me.TrackBar_HealthCheckRate.Maximum = 100
        Me.TrackBar_HealthCheckRate.Minimum = 5
        Me.TrackBar_HealthCheckRate.Name = "TrackBar_HealthCheckRate"
        Me.TrackBar_HealthCheckRate.Size = New System.Drawing.Size(687, 45)
        Me.TrackBar_HealthCheckRate.TabIndex = 0
        Me.TrackBar_HealthCheckRate.TickFrequency = 5
        Me.TrackBar_HealthCheckRate.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.TrackBar_HealthCheckRate.Value = 15
        '
        'GroupBox_MainTimerFrequency
        '
        Me.GroupBox_MainTimerFrequency.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox_MainTimerFrequency.Controls.Add(Me.Btn_MainTimerFrequency_Reset)
        Me.GroupBox_MainTimerFrequency.Controls.Add(Me.Tbx_MainTimerFrequency_Desc)
        Me.GroupBox_MainTimerFrequency.Controls.Add(Me.Tbx_MainTimerFrequency)
        Me.GroupBox_MainTimerFrequency.Controls.Add(Me.TrackBar_MainTimerFrequency)
        Me.GroupBox_MainTimerFrequency.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox_MainTimerFrequency.ForeColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.GroupBox_MainTimerFrequency.Location = New System.Drawing.Point(8, 25)
        Me.GroupBox_MainTimerFrequency.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupBox_MainTimerFrequency.Name = "GroupBox_MainTimerFrequency"
        Me.GroupBox_MainTimerFrequency.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.GroupBox_MainTimerFrequency.Size = New System.Drawing.Size(773, 130)
        Me.GroupBox_MainTimerFrequency.TabIndex = 117
        Me.GroupBox_MainTimerFrequency.TabStop = False
        Me.GroupBox_MainTimerFrequency.Text = "Main Program Tickrate"
        '
        'Tbx_MainTimerFrequency_Desc
        '
        Me.Tbx_MainTimerFrequency_Desc.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Tbx_MainTimerFrequency_Desc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Tbx_MainTimerFrequency_Desc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.Tbx_MainTimerFrequency_Desc.Location = New System.Drawing.Point(9, 21)
        Me.Tbx_MainTimerFrequency_Desc.Margin = New System.Windows.Forms.Padding(0)
        Me.Tbx_MainTimerFrequency_Desc.Multiline = True
        Me.Tbx_MainTimerFrequency_Desc.Name = "Tbx_MainTimerFrequency_Desc"
        Me.Tbx_MainTimerFrequency_Desc.ReadOnly = True
        Me.Tbx_MainTimerFrequency_Desc.Size = New System.Drawing.Size(687, 50)
        Me.Tbx_MainTimerFrequency_Desc.TabIndex = 117
        Me.Tbx_MainTimerFrequency_Desc.Text = "Controls how frequently the core logic loop runs. This affects systems like Infin" &
    "ite Ammo and general player state monitoring."
        '
        'Tbx_MainTimerFrequency
        '
        Me.Tbx_MainTimerFrequency.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Tbx_MainTimerFrequency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tbx_MainTimerFrequency.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Tbx_MainTimerFrequency.Location = New System.Drawing.Point(702, 71)
        Me.Tbx_MainTimerFrequency.Name = "Tbx_MainTimerFrequency"
        Me.Tbx_MainTimerFrequency.ReadOnly = True
        Me.Tbx_MainTimerFrequency.Size = New System.Drawing.Size(65, 24)
        Me.Tbx_MainTimerFrequency.TabIndex = 190
        Me.Tbx_MainTimerFrequency.Text = "1000ms"
        Me.Tbx_MainTimerFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TrackBar_MainTimerFrequency
        '
        Me.TrackBar_MainTimerFrequency.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.TrackBar_MainTimerFrequency.LargeChange = 25
        Me.TrackBar_MainTimerFrequency.Location = New System.Drawing.Point(9, 74)
        Me.TrackBar_MainTimerFrequency.Maximum = 200
        Me.TrackBar_MainTimerFrequency.Minimum = 25
        Me.TrackBar_MainTimerFrequency.Name = "TrackBar_MainTimerFrequency"
        Me.TrackBar_MainTimerFrequency.Size = New System.Drawing.Size(687, 45)
        Me.TrackBar_MainTimerFrequency.TabIndex = 0
        Me.TrackBar_MainTimerFrequency.TickFrequency = 10
        Me.TrackBar_MainTimerFrequency.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.TrackBar_MainTimerFrequency.Value = 100
        '
        'Btn_MainTimerFrequency_Reset
        '
        Me.Btn_MainTimerFrequency_Reset.BackColor = System.Drawing.Color.Transparent
        Me.Btn_MainTimerFrequency_Reset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Btn_MainTimerFrequency_Reset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Btn_MainTimerFrequency_Reset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_MainTimerFrequency_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_MainTimerFrequency_Reset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Btn_MainTimerFrequency_Reset.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Btn_MainTimerFrequency_Reset.Location = New System.Drawing.Point(702, 99)
        Me.Btn_MainTimerFrequency_Reset.Name = "Btn_MainTimerFrequency_Reset"
        Me.Btn_MainTimerFrequency_Reset.Size = New System.Drawing.Size(65, 23)
        Me.Btn_MainTimerFrequency_Reset.TabIndex = 191
        Me.Btn_MainTimerFrequency_Reset.Text = "Reset"
        Me.Btn_MainTimerFrequency_Reset.UseVisualStyleBackColor = False
        '
        'Btn_HealthCheckRate_Reset
        '
        Me.Btn_HealthCheckRate_Reset.BackColor = System.Drawing.Color.Transparent
        Me.Btn_HealthCheckRate_Reset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Btn_HealthCheckRate_Reset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Btn_HealthCheckRate_Reset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_HealthCheckRate_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_HealthCheckRate_Reset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Btn_HealthCheckRate_Reset.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Btn_HealthCheckRate_Reset.Location = New System.Drawing.Point(702, 99)
        Me.Btn_HealthCheckRate_Reset.Name = "Btn_HealthCheckRate_Reset"
        Me.Btn_HealthCheckRate_Reset.Size = New System.Drawing.Size(65, 23)
        Me.Btn_HealthCheckRate_Reset.TabIndex = 192
        Me.Btn_HealthCheckRate_Reset.Text = "Reset"
        Me.Btn_HealthCheckRate_Reset.UseVisualStyleBackColor = False
        '
        'Btn_PointsCheckRate_Reset
        '
        Me.Btn_PointsCheckRate_Reset.BackColor = System.Drawing.Color.Transparent
        Me.Btn_PointsCheckRate_Reset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Btn_PointsCheckRate_Reset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Btn_PointsCheckRate_Reset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_PointsCheckRate_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_PointsCheckRate_Reset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Btn_PointsCheckRate_Reset.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Btn_PointsCheckRate_Reset.Location = New System.Drawing.Point(702, 99)
        Me.Btn_PointsCheckRate_Reset.Name = "Btn_PointsCheckRate_Reset"
        Me.Btn_PointsCheckRate_Reset.Size = New System.Drawing.Size(65, 23)
        Me.Btn_PointsCheckRate_Reset.TabIndex = 193
        Me.Btn_PointsCheckRate_Reset.Text = "Reset"
        Me.Btn_PointsCheckRate_Reset.UseVisualStyleBackColor = False
        '
        'Btn_LocationTrackingRate_Reset
        '
        Me.Btn_LocationTrackingRate_Reset.BackColor = System.Drawing.Color.Transparent
        Me.Btn_LocationTrackingRate_Reset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Btn_LocationTrackingRate_Reset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Btn_LocationTrackingRate_Reset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_LocationTrackingRate_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_LocationTrackingRate_Reset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Btn_LocationTrackingRate_Reset.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Btn_LocationTrackingRate_Reset.Location = New System.Drawing.Point(702, 99)
        Me.Btn_LocationTrackingRate_Reset.Name = "Btn_LocationTrackingRate_Reset"
        Me.Btn_LocationTrackingRate_Reset.Size = New System.Drawing.Size(65, 23)
        Me.Btn_LocationTrackingRate_Reset.TabIndex = 193
        Me.Btn_LocationTrackingRate_Reset.Text = "Reset"
        Me.Btn_LocationTrackingRate_Reset.UseVisualStyleBackColor = False
        '
        'OptionSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1120, 736)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Btn_Close)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "OptionSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options and Settings"
        Me.TopMost = True
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.Nub_Round_Current, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Nub_Round_Next, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox_LocationTrackingRate.ResumeLayout(False)
        Me.GroupBox_LocationTrackingRate.PerformLayout()
        CType(Me.TrackBar_LocationTrackingRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_PointsCheckRate.ResumeLayout(False)
        Me.GroupBox_PointsCheckRate.PerformLayout()
        CType(Me.TrackBar_PointsCheckRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_HealthCheckRate.ResumeLayout(False)
        Me.GroupBox_HealthCheckRate.PerformLayout()
        CType(Me.TrackBar_HealthCheckRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_MainTimerFrequency.ResumeLayout(False)
        Me.GroupBox_MainTimerFrequency.PerformLayout()
        CType(Me.TrackBar_MainTimerFrequency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Btn_Close As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label_levelChanger_Desc As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Nub_Round_Current As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents Btn_ActionChangeLevel As Button
    Friend WithEvents Nub_Round_Next As NumericUpDown
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox_MainTimerFrequency As GroupBox
    Friend WithEvents TrackBar_MainTimerFrequency As TrackBar
    Friend WithEvents Tbx_MainTimerFrequency As TextBox
    Friend WithEvents GroupBox_HealthCheckRate As GroupBox
    Friend WithEvents Tbx_HealthCheckRate As TextBox
    Friend WithEvents TrackBar_HealthCheckRate As TrackBar
    Friend WithEvents Tbx_Info1 As TextBox
    Friend WithEvents GroupBox_PointsCheckRate As GroupBox
    Friend WithEvents Tbx_PointsCheckRate_Desc As TextBox
    Friend WithEvents Tbx_PointsCheckRate As TextBox
    Friend WithEvents TrackBar_PointsCheckRate As TrackBar
    Friend WithEvents Tbx_HealthCheckRate_Desc As TextBox
    Friend WithEvents Tbx_MainTimerFrequency_Desc As TextBox
    Friend WithEvents GroupBox_LocationTrackingRate As GroupBox
    Friend WithEvents Tbx_LocationTrackingRate_Desc As TextBox
    Friend WithEvents Tbx_LocationTrackingRate As TextBox
    Friend WithEvents TrackBar_LocationTrackingRate As TrackBar
    Friend WithEvents Btn_LocationTrackingRate_Reset As Button
    Friend WithEvents Btn_PointsCheckRate_Reset As Button
    Friend WithEvents Btn_HealthCheckRate_Reset As Button
    Friend WithEvents Btn_MainTimerFrequency_Reset As Button
End Class
