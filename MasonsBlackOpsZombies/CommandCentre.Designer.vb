<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CommandCentre
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CommandCentre))
        Me.iSend = New System.Windows.Forms.Button()
        Me.sCommand = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'iSend
        '
        Me.iSend.BackColor = System.Drawing.Color.Transparent
        Me.iSend.FlatAppearance.BorderColor = System.Drawing.Color.Lime
        Me.iSend.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.iSend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.iSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.iSend.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.iSend.ForeColor = System.Drawing.Color.Red
        Me.iSend.Location = New System.Drawing.Point(749, 682)
        Me.iSend.Name = "iSend"
        Me.iSend.Size = New System.Drawing.Size(75, 23)
        Me.iSend.TabIndex = 7
        Me.iSend.Text = "Send"
        Me.iSend.UseVisualStyleBackColor = False
        '
        'sCommand
        '
        Me.sCommand.BackColor = System.Drawing.Color.Black
        Me.sCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.sCommand.ForeColor = System.Drawing.Color.Yellow
        Me.sCommand.Location = New System.Drawing.Point(10, 682)
        Me.sCommand.Multiline = True
        Me.sCommand.Name = "sCommand"
        Me.sCommand.Size = New System.Drawing.Size(733, 23)
        Me.sCommand.TabIndex = 6
        Me.sCommand.Text = "command name"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.Lime
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Red
        Me.Button1.Location = New System.Drawing.Point(9, 711)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(815, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "CLOSE"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'RichTextBox2
        '
        Me.RichTextBox2.BackColor = System.Drawing.Color.Black
        Me.RichTextBox2.ForeColor = System.Drawing.Color.Yellow
        Me.RichTextBox2.Location = New System.Drawing.Point(11, 12)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.ReadOnly = True
        Me.RichTextBox2.Size = New System.Drawing.Size(814, 635)
        Me.RichTextBox2.TabIndex = 8
        Me.RichTextBox2.Text = resources.GetString("RichTextBox2.Text")
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.Lime
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Red
        Me.Button2.Location = New System.Drawing.Point(10, 653)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(406, 23)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "ENABLE CONSOLE CHEATS FOR ME!"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatAppearance.BorderColor = System.Drawing.Color.Lime
        Me.Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.Red
        Me.Button3.Location = New System.Drawing.Point(419, 653)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(406, 23)
        Me.Button3.TabIndex = 10
        Me.Button3.Text = "CHEATS KEYBIND EDITOR!"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'CommandCentre
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(838, 745)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.RichTextBox2)
        Me.Controls.Add(Me.iSend)
        Me.Controls.Add(Me.sCommand)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CommandCentre"
        Me.Text = "Command Centre"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents iSend As System.Windows.Forms.Button
    Friend WithEvents sCommand As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents RichTextBox2 As System.Windows.Forms.RichTextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
