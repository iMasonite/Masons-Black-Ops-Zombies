<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CommandCentre
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CommandCentre))
        Me.Btn_SendCommand = New System.Windows.Forms.Button()
        Me.Tbx_Command = New System.Windows.Forms.TextBox()
        Me.Btn_Close = New System.Windows.Forms.Button()
        Me.Rtbx_CommandList = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'Btn_SendCommand
        '
        Me.Btn_SendCommand.AutoSize = True
        Me.Btn_SendCommand.BackColor = System.Drawing.Color.Transparent
        Me.Btn_SendCommand.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Btn_SendCommand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Btn_SendCommand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_SendCommand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_SendCommand.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_SendCommand.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Btn_SendCommand.Location = New System.Drawing.Point(1003, 795)
        Me.Btn_SendCommand.Name = "Btn_SendCommand"
        Me.Btn_SendCommand.Size = New System.Drawing.Size(75, 32)
        Me.Btn_SendCommand.TabIndex = 7
        Me.Btn_SendCommand.Text = "Send"
        Me.Btn_SendCommand.UseVisualStyleBackColor = False
        '
        'Tbx_Command
        '
        Me.Tbx_Command.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Tbx_Command.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tbx_Command.Font = New System.Drawing.Font("Consolas", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tbx_Command.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Tbx_Command.Location = New System.Drawing.Point(12, 795)
        Me.Tbx_Command.Multiline = True
        Me.Tbx_Command.Name = "Tbx_Command"
        Me.Tbx_Command.Size = New System.Drawing.Size(985, 32)
        Me.Tbx_Command.TabIndex = 6
        '
        'Btn_Close
        '
        Me.Btn_Close.AutoSize = True
        Me.Btn_Close.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Close.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Btn_Close.Location = New System.Drawing.Point(1084, 795)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(75, 32)
        Me.Btn_Close.TabIndex = 4
        Me.Btn_Close.Text = "CLOSE"
        Me.Btn_Close.UseVisualStyleBackColor = False
        '
        'Rtbx_CommandList
        '
        Me.Rtbx_CommandList.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.Rtbx_CommandList.Dock = System.Windows.Forms.DockStyle.Top
        Me.Rtbx_CommandList.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!)
        Me.Rtbx_CommandList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.Rtbx_CommandList.Location = New System.Drawing.Point(0, 0)
        Me.Rtbx_CommandList.Margin = New System.Windows.Forms.Padding(0)
        Me.Rtbx_CommandList.Name = "Rtbx_CommandList"
        Me.Rtbx_CommandList.ReadOnly = True
        Me.Rtbx_CommandList.Size = New System.Drawing.Size(1171, 782)
        Me.Rtbx_CommandList.TabIndex = 8
        Me.Rtbx_CommandList.Text = resources.GetString("Rtbx_CommandList.Text")
        '
        'CommandCentre
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1171, 841)
        Me.Controls.Add(Me.Rtbx_CommandList)
        Me.Controls.Add(Me.Btn_SendCommand)
        Me.Controls.Add(Me.Tbx_Command)
        Me.Controls.Add(Me.Btn_Close)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CommandCentre"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Command Centre"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Btn_SendCommand As System.Windows.Forms.Button
    Friend WithEvents Tbx_Command As System.Windows.Forms.TextBox
    Friend WithEvents Btn_Close As System.Windows.Forms.Button
    Friend WithEvents Rtbx_CommandList As System.Windows.Forms.RichTextBox
End Class
