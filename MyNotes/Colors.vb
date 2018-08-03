Public Class Colors

    Private Sub btnNoteColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoteColor.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            MyNotes.MyData.NoteColor = ColorDialog1.Color
            btnNoteColor.BackColor = ColorDialog1.Color
            For i As Integer = 0 To MyNotes.NoteFrames.Count - 1
                MyNotes.NoteFrames(i).setcolors()
            Next
        End If
    End Sub

    Private Sub btnShctBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShctBack.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            MyNotes.MyData.ShortcutMenuColor = ColorDialog1.Color
            btnShctBack.BackColor = ColorDialog1.Color
            MyNotes.ShortCutMenu.SetColors()
        End If
    End Sub

    Private Sub btnShctTopBottom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShctTopBottom.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            MyNotes.MyData.ShortcutMenuTopColor = ColorDialog1.Color
            btnShctTopBottom.BackColor = ColorDialog1.Color
            MyNotes.ShortCutMenu.SetColors()
        End If
    End Sub

    Private Sub Colors_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnNoteColor.BackColor = MyNotes.MyData.NoteColor
        btnShctBack.BackColor = MyNotes.MyData.ShortcutMenuColor
        btnShctTopBottom.BackColor = MyNotes.MyData.ShortcutMenuTopColor
    End Sub
End Class