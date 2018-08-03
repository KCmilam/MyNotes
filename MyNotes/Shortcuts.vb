Imports System.Runtime.InteropServices
Imports System.Xml
Imports System.Xml.Serialization

Public Class Shortcuts
#Region "Class Declarations"
    Dim ShortButtons As New ArrayList
    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTBOTTOMRIGHT As Integer = 17
    Private Const HTCAPTION As Integer = 2

    Private Const ShortcutHeight As Integer = 23

    Dim Gradient As New GradientPanel


    <DllImport("user32.dll")> _
   Public Shared Function ReleaseCapture() As Boolean
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

#End Region

    Public Sub New()
        'Create a the shortcut menu, set it's colors, and create the buttons for each saved shortcut.
        InitializeComponent()
        Me.Height = MyNotes.MyData.ShortcutMenuHeight
        Me.Width = MyNotes.MyData.ShortcutMenuWidth
        Me.Location = New Point(MyNotes.MyData.ShortcutMenuLocationX, MyNotes.MyData.ShortcutMenuLocationY)
        Gradient.Parent = SplitContainer2.Panel1
        Gradient.Dock = DockStyle.Fill
        Gradient.Show()
        Gradient.BringToFront()
        Gradient.BackColour1 = MyNotes.MyData.ShortcutMenuColor
        Gradient.BackColour2 = Color.Snow
        Gradient.GradientMode = Drawing2D.LinearGradientMode.ForwardDiagonal
        SetColors()
        DrawButtons()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        'When closed, save the data.
        MyNotes.SaveMyData()
        Me.Hide()
    End Sub

    Private Sub Panel2_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel2.DragDrop, SplitContainer1.DragDrop, SplitContainer2.DragDrop, Panel1.DragDrop
        'When a file is dragged and dropped onto the panel, detect whether it is a file or folder to determine the correct color for the button then add it.
        Dim fileName As String() = CType(e.Data.GetData(DataFormats.FileDrop, True), String())
        Dim Name As String = InputBox("Enter name of Short Cut", "Name", "Shortcut Name")
        If Name <> "" And Name <> " " Then
            If My.Computer.FileSystem.FileExists(fileName(0)) Then
                Dim NewShort As New Button
                NewShort.Parent = Gradient
                NewShort.Text = Name
                NewShort.Tag = MyNotes.MyData.AddShortcut(Name, fileName(0))
                NewShort.FlatStyle = FlatStyle.Flat
                NewShort.FlatAppearance.BorderSize = 1
                NewShort.Height = ShortcutHeight
                NewShort.Width = Gradient.Width
                NewShort.Location = New Point(0, ShortButtons.Count * ShortcutHeight)
                NewShort.Show()
                AddHandler NewShort.Click, AddressOf shortcut_Click
                ShortButtons.Add(NewShort)
                MyNotes.SaveMyData()
            ElseIf My.Computer.FileSystem.DirectoryExists(fileName(0)) Then
                Dim NewShort As New Button
                NewShort.Parent = Gradient
                NewShort.BackColor = Color.LemonChiffon
                NewShort.Text = Name
                NewShort.Tag = MyNotes.MyData.AddShortcut(Name, fileName(0))
                NewShort.FlatStyle = FlatStyle.Flat
                NewShort.FlatAppearance.BorderSize = 1
                NewShort.Height = ShortcutHeight
                NewShort.Width = Gradient.Width
                NewShort.Location = New Point(0, ShortButtons.Count * ShortcutHeight)
                NewShort.Show()
                AddHandler NewShort.Click, AddressOf shortcut_Click
                ShortButtons.Add(NewShort)
                MyNotes.SaveMyData()
            End If
        End If
    End Sub

    Private Sub Panel2_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel2.DragOver, SplitContainer1.DragOver, SplitContainer2.DragOver, Panel1.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub RichTextBox1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SplitContainer1.SizeChanged
        'Detect when the shortcut menu's location is changed to save the new location.
        btnExit.Location = New Point(Me.Width - btnExit.Width, 0)
        btnResize.Location = New Point(Me.Width - btnResize.Width, 0)
    End Sub

    Private Sub Note_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LocationChanged
        'Detect when the shortcut menu's location is changed to save the new location.
        If MyNotes.Loaded Then
            MyNotes.MyData.ShortcutMenuLocationX = Me.Location.X
            MyNotes.MyData.ShortcutMenuLocationY = Me.Location.Y
        End If
    End Sub

    Private Sub Note_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        'Detect when the shortcut menu's size is changed to save the new location.
        If MyNotes.Loaded Then
            MyNotes.MyData.ShortcutMenuWidth = Me.Width
            MyNotes.MyData.ShortcutMenuHeight = Me.Height

            For Each mControl As Button In Gradient.Controls
                mControl.Width = Me.Width
            Next
        End If
    End Sub

    Private Sub shortcut_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If My.Computer.Keyboard.CtrlKeyDown Then
                'The user is holding down the control key and wants to delete the shortcut.
                MyNotes.MyData.ShortcutsList.Remove(sender.tag)
                ShortButtons.Clear()
                Gradient.Controls.Remove(sender)
                sender.hide()
                sender.dispose()
                DrawButtons()
            Else
                'Open the shortcut.
                If My.Computer.FileSystem.FileExists(sender.tag.shortcut) Then
                    System.Diagnostics.Process.Start(sender.tag.shortcut)
                ElseIf My.Computer.FileSystem.DirectoryExists(sender.tag.shortcut) Then
                    Try
                        Dim procstart As New ProcessStartInfo("explorer")
                        procstart.Arguments = sender.tag.shortcut
                        Process.Start(procstart)
                    Catch ex As Exception
                    End Try
                End If
            End If
        End If
    End Sub

    Public Sub DrawButtons()
        'Cycle through the app's saved shortcuts and create the buttons for each one.
        Gradient.Controls.Clear()
        For Each mShortcut As AppData.ShortCutObject In MyNotes.MyData.ShortcutsList
            If My.Computer.FileSystem.FileExists(mShortcut.Shortcut) Then
                Dim NewShort As New Button
                NewShort.Parent = Gradient
                NewShort.Text = mShortcut.Name
                NewShort.Tag = mShortcut
                NewShort.FlatStyle = FlatStyle.Flat
                NewShort.FlatAppearance.BorderSize = 1
                NewShort.Height = ShortcutHeight
                NewShort.Width = Gradient.Width
                NewShort.Location = New Point(0, ShortButtons.Count * ShortcutHeight)
                NewShort.Show()
                AddHandler NewShort.Click, AddressOf shortcut_Click
                ShortButtons.Add(NewShort)
            ElseIf My.Computer.FileSystem.DirectoryExists(mShortcut.Shortcut) Then
                Dim NewShort As New Button
                NewShort.Parent = Gradient
                NewShort.BackColor = Color.LemonChiffon
                NewShort.Text = mShortcut.Name
                NewShort.Tag = mShortcut
                NewShort.FlatStyle = FlatStyle.Flat
                NewShort.FlatAppearance.BorderSize = 1
                NewShort.Height = ShortcutHeight
                NewShort.Width = Gradient.Width
                NewShort.Location = New Point(0, ShortButtons.Count * ShortcutHeight)
                NewShort.Show()
                AddHandler NewShort.Click, AddressOf shortcut_Click
                ShortButtons.Add(NewShort)
            End If
        Next
    End Sub

    Public Sub SetColors()
        Panel1.BackColor = MyNotes.MyData.ShortcutMenuTopColor
        Gradient.BackColour1 = MyNotes.MyData.ShortcutMenuColor
        Panel2.BackColor = MyNotes.MyData.ShortcutMenuTopColor
    End Sub

#Region "Moving/Resizing"

    Private Sub MoveForm()
        ReleaseCapture()
        SendMessage(Me.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
    End Sub

    Private Sub ResizeForm()
        ReleaseCapture()
        SendMessage(Me.Handle, WM_NCLBUTTONDOWN, HTBOTTOMRIGHT, 0)
    End Sub

    Private Sub pnlCaption_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left And Me.WindowState <> FormWindowState.Maximized Then
            MoveForm()
        End If
    End Sub

    Dim LastPoint As Point
    Private Sub btnResize_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnResize.MouseDown
        LastPoint = e.Location
        ResizeForm()
    End Sub
#End Region
End Class