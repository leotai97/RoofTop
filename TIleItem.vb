Public Class TileItem
  Inherits ListViewItem

  Public Tile As Tile
  Public Number As Integer

  Public Sub New(ByVal oTile As Tile, ByVal nNbr As Integer)
    Dim c As String

    Me.Tile = oTile
    Me.Number = nNbr

    c = Me.Tile.Color.R & "," & Me.Tile.Color.G & "," & Me.Tile.Color.B


    Me.Text = Me.Number.ToString
    Me.SubItems.Add(Me.Tile.Panel.PanelNumber)
    If Me.Tile.Y = 0 Then Me.SubItems.Add("Top") Else Me.SubItems.Add("Bot")
    Me.SubItems.Add((Me.Tile.X + 1).ToString)
    Me.SubItems.Add(c)

  End Sub

End Class
