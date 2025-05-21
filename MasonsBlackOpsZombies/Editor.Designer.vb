<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Editor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Editor))
        Me.TxtKey = New System.Windows.Forms.TextBox()
        Me.LstCommands = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TxtKey1 = New System.Windows.Forms.TextBox()
        Me.LstCommands1 = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'TxtKey
        '
        Me.TxtKey.BackColor = System.Drawing.Color.Black
        Me.TxtKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKey.ForeColor = System.Drawing.Color.Yellow
        Me.TxtKey.Location = New System.Drawing.Point(13, 13)
        Me.TxtKey.Name = "TxtKey"
        Me.TxtKey.Size = New System.Drawing.Size(75, 20)
        Me.TxtKey.TabIndex = 0
        Me.TxtKey.Text = "Key Bind"
        '
        'LstCommands
        '
        Me.LstCommands.BackColor = System.Drawing.Color.Black
        Me.LstCommands.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LstCommands.ForeColor = System.Drawing.Color.Yellow
        Me.LstCommands.FormattingEnabled = True
        Me.LstCommands.Items.AddRange(New Object() {"God On", "God Off", "---------------------------------------------------", "No Clip On", "No Clip Off", "---------------------------------------------------", "No Target On", "No Target Off", "---------------------------------------------------", "Ufo On", "Ufo Off", "---------------------------------------------------", "R_fog On", "R_fog Off"})
        Me.LstCommands.Location = New System.Drawing.Point(94, 13)
        Me.LstCommands.Name = "LstCommands"
        Me.LstCommands.Size = New System.Drawing.Size(144, 21)
        Me.LstCommands.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.Lime
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.Red
        Me.Button1.Location = New System.Drawing.Point(125, 65)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(134, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Save Key Binds"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.Lime
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.Red
        Me.Button2.Location = New System.Drawing.Point(13, 65)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(106, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Backup CFG"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TxtKey1
        '
        Me.TxtKey1.BackColor = System.Drawing.Color.Black
        Me.TxtKey1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKey1.ForeColor = System.Drawing.Color.Yellow
        Me.TxtKey1.Location = New System.Drawing.Point(13, 39)
        Me.TxtKey1.Name = "TxtKey1"
        Me.TxtKey1.Size = New System.Drawing.Size(75, 20)
        Me.TxtKey1.TabIndex = 4
        Me.TxtKey1.Text = "Key Bind"
        '
        'LstCommands1
        '
        Me.LstCommands1.BackColor = System.Drawing.Color.Black
        Me.LstCommands1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LstCommands1.ForeColor = System.Drawing.Color.Yellow
        Me.LstCommands1.Location = New System.Drawing.Point(94, 39)
        Me.LstCommands1.Name = "LstCommands1"
        Me.LstCommands1.Size = New System.Drawing.Size(144, 20)
        Me.LstCommands1.TabIndex = 5
        Me.LstCommands1.Text = "Custom Command"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.ForeColor = System.Drawing.Color.Yellow
        Me.CheckBox1.Location = New System.Drawing.Point(244, 16)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 6
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.ForeColor = System.Drawing.Color.Yellow
        Me.CheckBox2.Location = New System.Drawing.Point(244, 41)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox2.TabIndex = 7
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'Editor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(271, 100)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.LstCommands1)
        Me.Controls.Add(Me.TxtKey1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.LstCommands)
        Me.Controls.Add(Me.TxtKey)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Editor"
        Me.Text = "Key Bind Editor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtKey As System.Windows.Forms.TextBox
    Friend WithEvents LstCommands As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TxtKey1 As System.Windows.Forms.TextBox
    Friend WithEvents LstCommands1 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
End Class
