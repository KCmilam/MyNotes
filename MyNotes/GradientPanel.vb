Public Class GradientPanel : Inherits System.Windows.Forms.Panel

    Public Sub New()
        MyBase.New()
        Me.SetDefaultControlStyles()
        Me.customInitialisation()
    End Sub

    Private Sub SetDefaultControlStyles()
        Me.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, True)
    End Sub

    Private Sub customInitialisation()
        Me.SuspendLayout()
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ResumeLayout(False)
    End Sub

    Public Overrides Property BackColor() As System.Drawing.Color
        Get
            Return MyBase.BackColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            MyBase.BackColor = Value
        End Set
    End Property

    Private _BackColour1 As System.Drawing.Color = System.Drawing.SystemColors.Window

    Public Property BackColour1() As System.Drawing.Color
        Get
            Return Me._BackColour1
        End Get
        Set(ByVal Value As System.Drawing.Color)
            Me._BackColour1 = Value
            If Me.DesignMode = True Then
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _BackColour2 As System.Drawing.Color = System.Drawing.SystemColors.Window

    Public Property BackColour2() As System.Drawing.Color
        Get
            Return Me._BackColour2
        End Get
        Set(ByVal Value As System.Drawing.Color)
            Me._BackColour2 = Value
            If Me.DesignMode = True Then
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _GradientMode As System.Drawing.Drawing2D.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical

    Public Property GradientMode() As System.Drawing.Drawing2D.LinearGradientMode
        Get
            Return Me._GradientMode
        End Get
        Set(ByVal Value As System.Drawing.Drawing2D.LinearGradientMode)
            Me._GradientMode = Value
            If Me.DesignMode = True Then
                Me.Invalidate()
            End If
        End Set
    End Property

    Protected Overrides Sub OnPaintBackGround(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim filler As New System.Drawing.Drawing2D.LinearGradientBrush(Me.ClientRectangle, _
        Me.BackColour1, Me.BackColour2, Me.GradientMode)
        e.Graphics.FillRectangle(filler, Me.ClientRectangle)
        filler.Dispose()
    End Sub

End Class