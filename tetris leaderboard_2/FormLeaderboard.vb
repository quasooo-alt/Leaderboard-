Public Class FormLeaderboard

    Public Property gameScore As Integer = 0

    Private Sub Leaderboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AddHandler lvScores.DrawColumnHeader, AddressOf lv_DrawColumnHeader
        AddHandler lvScores.DrawItem, AddressOf lv_DrawItem
        AddHandler lvScores.DrawSubItem, AddressOf lv_DrawSubItem
    End Sub


    Private Sub lv_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs)
        Using brush As New Drawing2D.LinearGradientBrush(e.Bounds, Color.DarkRed, Color.DarkOrange, Drawing2D.LinearGradientMode.Horizontal)
            e.Graphics.FillRectangle(brush, e.Bounds)
        End Using
        e.Graphics.DrawRectangle(Pens.Gold, e.Bounds)
        e.Graphics.DrawString(e.Header.Text, New Font("Segoe UI", 10, FontStyle.Bold), Brushes.White, e.Bounds)
    End Sub


    Private Sub lv_DrawItem(sender As Object, e As DrawListViewItemEventArgs)
        Dim bgColor As Color = If(e.ItemIndex Mod 2 = 0, Color.FromArgb(40, 40, 40), Color.FromArgb(60, 60, 60))
        e.Graphics.FillRectangle(New SolidBrush(bgColor), e.Bounds)
    End Sub


    Private Sub lv_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs)
        e.Graphics.DrawString(e.SubItem.Text, New Font("Segoe UI", 10), Brushes.White, e.Bounds)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim name As String = txtName.Text
        If name = "" Then Return
        Dim item As New ListViewItem()
        item.SubItems.Add(name)
        item.SubItems.Add(gameScore.ToString())
        lvScores.Items.Add(item)
        SortAndRank()
        txtName.Clear()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If lvScores.SelectedItems.Count = 0 Then Return
        Dim item = lvScores.SelectedItems(0)
        item.SubItems(1).Text = txtName.Text
        item.SubItems(2).Text = gameScore.ToString()
        SortAndRank()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If lvScores.SelectedItems.Count = 0 Then Return
        lvScores.Items.Remove(lvScores.SelectedItems(0))
        SortAndRank()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lvScores.Items.Clear()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub SortAndRank()
        Dim items = lvScores.Items.Cast(Of ListViewItem)().OrderByDescending(Function(x) CInt(x.SubItems(2).Text)).ToList()
        lvScores.Items.Clear()
        Dim rank As Integer = 1
        For Each it In items
            it.SubItems(0).Text = rank.ToString()
            lvScores.Items.Add(it)
            rank += 1
        Next
    End Sub

End Class