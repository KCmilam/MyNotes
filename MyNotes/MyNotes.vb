Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO

Public Class MyNotes
    'Keep track of all note forms created
    Public NoteFrames As New ArrayList

    'Declare Short Cut Menu a class level.
    Public ShortCutMenu As Shortcuts

    'Shared Custom AppData object to keep track of app colors, created notes, and shortcuts.
    Public Shared MyData As AppData

    'Boolean variable letting us know when the app has been loaded.
    Public Shared Loaded = False

    Private Sub MyNotes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If the MyData.xml file does not exist, create it.  This is the serialized AppData object.  Serialization simply lets us
        'write our objects out to the harddrive from memory for later use.  It's a good way to save data when the program is not running.
        If Not My.Computer.FileSystem.FileExists(System.AppDomain.CurrentDomain.BaseDirectory & "\MyData.xml") Then
            Dim TextFile As New StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory & "\MyData.xml")
            TextFile.WriteLine()
            TextFile.Close()
            MyData = New AppData
            SaveMyData()
        End If


        'We use an xml serializer to read and write the appdata object to the hard drive.  Here we are going to deserialize it to read it into out object.
        Dim mSer As New XmlSerializer(GetType(AppData), New Type() {GetType(AppData.NoteObject), GetType(AppData.ShortCutObject), GetType(AppData.MyColor)})
        Dim Textreader As XmlTextReader
        Textreader = New XmlTextReader(System.AppDomain.CurrentDomain.BaseDirectory & "\MyData.xml")
        Dim mData As AppData = mSer.Deserialize(Textreader)
        'Set MyData to the temperorary mData object.
        MyData = mData
        Textreader.Close()

        'Cycle through the saved notes and create a note form for each one.
        For i As Integer = 0 To MyData.Notes.Count - 1
            Dim NewNote As New Note(i)
            NewNote.Location = New Point(MyData.Notes(i).Positionx, MyData.Notes(i).Positiony)
            NewNote.Show()
            NoteFrames.Add(NewNote)
        Next

        'Instantiate the ShortCutMenu and show it.
        ShortCutMenu = New Shortcuts()
        ShortCutMenu.Show()

        'Set loaded variable to true.  App has been loaded.
        Loaded = True
    End Sub


    Public Shared Sub SaveMyData()
        'Saves the data to the programs base directory using an xml serializer.
        Dim mSer As New XmlSerializer(GetType(AppData), New Type() {GetType(AppData.NoteObject), GetType(AppData.ShortCutObject), GetType(AppData.MyColor)})
        Dim textwriter As XmlTextWriter
        textwriter = New XmlTextWriter(System.AppDomain.CurrentDomain.BaseDirectory & "\MyData.xml", Nothing)
        mSer.Serialize(textwriter, MyData)
        textwriter.Close()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub NewNoteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewNoteToolStripMenuItem.Click
        'Create a new note and add it to the note frames.
        MyData.AddNote()
        Dim NewNote As New Note(MyData.Notes.Count - 1)
        NewNote.Show()
        NoteFrames.Add(NewNote)
    End Sub

    Private Sub HideNotesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideNotesToolStripMenuItem.Click
        If sender.text = "Hide Notes" Then
            'Cycle through all the note frames and hide them
            For i As Integer = 0 To NoteFrames.Count - 1
                NoteFrames(i).hide()
            Next
            sender.text = "Show Notes"
        Else
            'Cycle through all the note frames and show them
            For i As Integer = 0 To NoteFrames.Count - 1
                NoteFrames(i).show()
            Next
            sender.text = "Hide Notes"
        End If
    End Sub

    Private Sub ShowShortcutsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowShortcutsToolStripMenuItem.Click
        ShortCutMenu.Show()
    End Sub

    Private Sub ColorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColorsToolStripMenuItem.Click
        'Create a new color form and show it.  Allows the user to define custom colors for the program.
        Dim mColors = New Colors
        Colors.Show()
    End Sub

    Private Sub NotifyIcon1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.Click
        'Toggle the topmost variable of the form to bring it to the front.
        ShortCutMenu.TopMost = True
        ShortCutMenu.TopMost = False
    End Sub
End Class
