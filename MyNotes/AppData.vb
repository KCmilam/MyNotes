Imports System.Xml.Serialization


'This is the custom object used to save all data for the app.  We serialize this object to the harddrive to 
'remember all the data for the application such as notes created in the past, their size and location, any shortcuts
'that have been created, and the color scheme defined by the user.
Public Class AppData
    <XmlElement("Notes")> Public Notes As New ArrayList
    <XmlElement("ShortcutsList")> Public ShortcutsList As New ArrayList
    <XmlAttribute("ShortcutMenuHeight")> Public ShortcutMenuHeight As Integer = 200
    <XmlAttribute("ShortcutMenuWidth")> Public ShortcutMenuWidth As Integer = 200
    <XmlAttribute("ShortcutMenuLocationX")> Public ShortcutMenuLocationX As Integer = 0
    <XmlAttribute("ShortcutMenuLocationY")> Public ShortcutMenuLocationY As Integer = 0
    <XmlElement("ShortcutMenuColor")> Public mShortcutMenuColor As New MyColor
    <XmlElement("mShortcutMenuTopColor")> Public mShortcutMenuTopColor As New MyColor
    <XmlElement("mNoteColor")> Public mNoteColor As New MyColor

    <XmlIgnore()> Public Property ShortcutMenuColor() As Color
        Get
            Return Color.FromArgb(mShortcutMenuColor.R, mShortcutMenuColor.G, mShortcutMenuColor.B)
        End Get
        Set(ByVal value As Color)
            mShortcutMenuColor.R = value.R
            mShortcutMenuColor.G = value.G
            mShortcutMenuColor.B = value.B
        End Set
    End Property

    <XmlIgnore()> Public Property ShortcutMenuTopColor() As Color
        Get
            Return Color.FromArgb(mShortcutMenuTopColor.R, mShortcutMenuTopColor.G, mShortcutMenuTopColor.B)
        End Get
        Set(ByVal value As Color)
            mShortcutMenuTopColor.R = value.R
            mShortcutMenuTopColor.G = value.G
            mShortcutMenuTopColor.B = value.B
        End Set
    End Property

    <XmlIgnore()> Public Property NoteColor() As Color
        Get
            Return Color.FromArgb(mNoteColor.R, mNoteColor.G, mNoteColor.B)
        End Get
        Set(ByVal value As Color)
            mNoteColor.R = value.R
            mNoteColor.G = value.G
            mNoteColor.B = value.B
        End Set
    End Property

    Public Function AddNote() As NoteObject
        Dim NewNote As New NoteObject
        Notes.Add(NewNote)
        Return (NewNote)
    End Function

    Public Function AddNote(ByVal NoteText As String, ByVal NoteShortcut As String) As NoteObject
        Dim NewNote As New NoteObject
        NewNote.Text = NoteText
        NewNote.LabelLink = NoteShortcut
        Notes.Add(NewNote)
        Return (NewNote)
    End Function

    Public Function AddShortcut(ByVal ShortcutName As String, ByVal ShortCutPath As String) As ShortCutObject
        Dim NewShortcut As New ShortCutObject
        NewShortcut.Name = ShortcutName
        NewShortcut.Shortcut = ShortCutPath
        ShortcutsList.Add(NewShortcut)
        Return NewShortcut
    End Function

    Public Class NoteObject
        <XmlAttribute("NoteText")> Public Text As String
        <XmlAttribute("LabelLink")> Public LabelLink As String = "-"
        <XmlAttribute("PositionX")> Public PositionX As Integer = 0
        <XmlAttribute("PositionY")> Public PositionY As Integer = 0
        <XmlAttribute("SizeX")> Public SizeX As Integer = 200
        <XmlAttribute("SizeY")> Public SizeY As Integer = 200
    End Class

    Public Class ShortCutObject
        <XmlAttribute("Shortcut")> Public Shortcut As String
        <XmlAttribute("Name")> Public Name As String
        <XmlAttribute("PositionX")> Public PositionX As Integer = 0
        <XmlAttribute("PositionY")> Public PositionY As Integer = 0
        <XmlAttribute("SizeX")> Public SizeX As Integer = 200
        <XmlAttribute("SizeY")> Public SizeY As Integer = 300
    End Class

    Public Class MyColor
        <XmlAttribute("R")> Public R As Integer = 0
        <XmlAttribute("G")> Public G As Integer = 0
        <XmlAttribute("B")> Public B As Integer = 0
    End Class
End Class