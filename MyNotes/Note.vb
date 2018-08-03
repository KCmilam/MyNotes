Imports System.Runtime.InteropServices
Imports System.Xml
Imports System.Xml.Serialization

Public Class Note

#Region "Class Declarations"
    'Private variable index to remember which note of the App's Data notes this form's note is.
    Dim Index As Integer = -1

    'Constants used for Windows API calls to move and resize the form.  This is normally not needed, but for this
    'form it is because we are hiding the borders of the form.  
    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTBOTTOMRIGHT As Integer = 17
    Private Const HTCAPTION As Integer = 2

    'Variable to remember the last location of the mouse when the resize button is click to determine which direction 
    'the mouse is moving.
    Dim LastPoint As Point

    'Windows API call
    <DllImport("user32.dll")> _
   Public Shared Function ReleaseCapture() As Boolean
    End Function

    'Windows API call
    <DllImport("user32.dll")> _
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
#End Region

    Public Sub New(ByVal NoteIndex As Integer)
        'Create a new note form representing a note.
        InitializeComponent()
        Index = NoteIndex
        Me.Height = MyNotes.MyData.Notes(Index).sizeY
        Me.Width = MyNotes.MyData.Notes(Index).sizeX
        RichTextBox1.Text = MyNotes.MyData.Notes(Index).text
        lblLink.Text = MyNotes.MyData.Notes(Index).LabelLink
        lblLink.Tag = MyNotes.MyData.Notes(Index).LabelLink
        lblLink.Width = Me.Width - btnResize.Width - 5
        SetColors()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        MyNotes.SaveMyData()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        'If the user clicks the "X" button on the note, then we need to remove the note from the app's data object, save the it, and dispose the frame.
        MyNotes.MyData.Notes.RemoveAt(Index)
        MyNotes.NoteFrames.RemoveAt(Index)
        MyNotes.SaveMyData()
        Me.Dispose()
    End Sub

    Private Sub Panel2_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel2.DragDrop, SplitContainer1.DragDrop, SplitContainer2.DragDrop, RichTextBox1.DragDrop, Panel1.DragDrop
        'Allows for files and folders to be dragged and droppped onto the note form to create a link to it.
        Dim fileName As String() = CType(e.Data.GetData(DataFormats.FileDrop, True), String())
        lblLink.Text = fileName(0)
        lblLink.Width = Me.Width - btnResize.Width - 5
        lblLink.Tag = fileName(0)
        MyNotes.MyData.Notes(Index).LabelLink = fileName(0)
        MyNotes.MyData.Notes(Index).LabelLink = fileName(0)
    End Sub

    Private Sub Panel2_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel2.DragOver, SplitContainer1.DragOver, SplitContainer2.DragOver, RichTextBox1.DragOver, Panel1.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub Note_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles RichTextBox1.LostFocus
        'When ever the note loses focus, it is a good time to save the data.
        MyNotes.MyData.Notes(Index).text = RichTextBox1.Text
        Try
            btnExit.Location = New Point(Me.Width - btnExit.Width, 0)
        Catch
        End Try
    End Sub

    Private Sub RichTextBox1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SplitContainer1.SizeChanged
        'Detect when the note is moved so we can write the new location to the app's data.
        btnExit.Location = New Point(Me.Width - btnExit.Width, 0)
        btnResize.Location = New Point(Me.Width - btnResize.Width, 0)
    End Sub

    Private Sub Note_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LocationChanged
        'Detect when the note's location is changed so we can write the new size to the app's data.
        'This should only be done after the app is completely loaded because while the app is loading, the form's location changed
        'event is fired by default.
        If MyNotes.Loaded And Index <> -1 Then
            MyNotes.MyData.Notes(Index).positionX = Me.Location.X
            MyNotes.MyData.Notes(Index).positionY = Me.Location.Y
        End If
    End Sub

    Private Sub Note_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        'Detect when the note's size is changed so we can write the new size to the app's data.
        'This should only be done after the app is completely loaded because while the app is loading, the form's size changed
        'event is fired by default.
        If MyNotes.Loaded And Index <> -1 Then
            MyNotes.MyData.Notes(Index).sizeX = Me.Width
            MyNotes.MyData.Notes(Index).sizeY = Me.Height
            lblLink.Width = Me.Width - btnResize.Width - 5
        End If
    End Sub

    Private Sub lblLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblLink.DoubleClick
        Try
            'When the link label is double click, open the file/folder to which it references.
            Dim procstart As New ProcessStartInfo("explorer")
            procstart.Arguments = lblLink.Tag
            Process.Start(procstart)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearToolStripMenuItem.Click
        'Clear the link label
        lblLink.Text = "-"
        lblLink.Tag = Nothing
        MyNotes.MyData.Notes(Index).LabelLink = "-"
        MyNotes.MyData.Notes(Index).LabelLink = Nothing
    End Sub

    Public Sub SetColors()
        RichTextBox1.BackColor = MyNotes.MyData.NoteColor
        Panel1.BackColor = MyNotes.MyData.NoteColor
        Panel2.BackColor = MyNotes.MyData.NoteColor
        SplitContainer1.BackColor = MyNotes.MyData.NoteColor
        SplitContainer2.BackColor = MyNotes.MyData.NoteColor
    End Sub

#Region "Moving/Resizing"
    Private Sub MoveForm()
        'Uses Windows API calls to move the form.
        ReleaseCapture()
        SendMessage(Me.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
    End Sub

    Private Sub ResizeForm()
        'Uses Windows API calls to resize the form.
        ReleaseCapture()
        SendMessage(Me.Handle, WM_NCLBUTTONDOWN, HTBOTTOMRIGHT, 0)
    End Sub

    Private Sub pnlCaption_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left And Me.WindowState <> FormWindowState.Maximized Then
            MoveForm()
        End If
    End Sub

    Private Sub btnResize_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnResize.MouseDown
        LastPoint = e.Location
        ResizeForm()
    End Sub
#End Region
End Class