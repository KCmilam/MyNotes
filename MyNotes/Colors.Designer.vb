<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Colors
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.btnNoteColor = New System.Windows.Forms.Button
        Me.btnShctBack = New System.Windows.Forms.Button
        Me.btnShctTopBottom = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(61, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Note Background:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(141, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Shortcut Menu Background:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(140, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Shortcut Menu Top/Bottom:"
        '
        'ColorDialog1
        '
        Me.ColorDialog1.AnyColor = True
        '
        'btnNoteColor
        '
        Me.btnNoteColor.BackColor = System.Drawing.Color.DarkTurquoise
        Me.btnNoteColor.FlatAppearance.BorderSize = 0
        Me.btnNoteColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNoteColor.Location = New System.Drawing.Point(161, 7)
        Me.btnNoteColor.Name = "btnNoteColor"
        Me.btnNoteColor.Size = New System.Drawing.Size(104, 12)
        Me.btnNoteColor.TabIndex = 5
        Me.btnNoteColor.UseVisualStyleBackColor = False
        '
        'btnShctBack
        '
        Me.btnShctBack.BackColor = System.Drawing.Color.DarkTurquoise
        Me.btnShctBack.FlatAppearance.BorderSize = 0
        Me.btnShctBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShctBack.Location = New System.Drawing.Point(161, 29)
        Me.btnShctBack.Name = "btnShctBack"
        Me.btnShctBack.Size = New System.Drawing.Size(104, 12)
        Me.btnShctBack.TabIndex = 6
        Me.btnShctBack.UseVisualStyleBackColor = False
        '
        'btnShctTopBottom
        '
        Me.btnShctTopBottom.BackColor = System.Drawing.Color.DarkTurquoise
        Me.btnShctTopBottom.FlatAppearance.BorderSize = 0
        Me.btnShctTopBottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShctTopBottom.Location = New System.Drawing.Point(161, 52)
        Me.btnShctTopBottom.Name = "btnShctTopBottom"
        Me.btnShctTopBottom.Size = New System.Drawing.Size(104, 12)
        Me.btnShctTopBottom.TabIndex = 7
        Me.btnShctTopBottom.UseVisualStyleBackColor = False
        '
        'Colors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(300, 78)
        Me.Controls.Add(Me.btnShctTopBottom)
        Me.Controls.Add(Me.btnShctBack)
        Me.Controls.Add(Me.btnNoteColor)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Colors"
        Me.Text = "Colors"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents btnNoteColor As System.Windows.Forms.Button
    Friend WithEvents btnShctBack As System.Windows.Forms.Button
    Friend WithEvents btnShctTopBottom As System.Windows.Forms.Button
End Class
