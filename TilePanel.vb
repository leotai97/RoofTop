Public Class TilePanel

  Private m_Panel As Bitmap
  Private m_TileCount As Integer
  Private m_Tiles As List(Of Tile)
  Private m_PanelNumber As Integer


  Public Sub New(ByVal nPanel As Integer, ByVal nCount As Integer, ByVal Main As MainWnd, ByVal img As Bitmap)
    Dim bmpTile As Bitmap
    Dim g As Graphics = Nothing
    Dim x, y As Integer
    Dim rctIn, rctOut As Rectangle
    Dim nID As Integer
    Dim n As Integer

    m_PanelNumber = nPanel
    m_TileCount = nCount
    m_Tiles = New List(Of Tile)

    m_Panel = img

    n = 0
    y = 11
    For i = 0 To 1
      x = 2
      For k = 0 To 9
        If n < nCount Then
          bmpTile = New Bitmap(37, 18, Imaging.PixelFormat.Format24bppRgb)
          g = Graphics.FromImage(bmpTile)
          rctIn = New Rectangle(x, y, 37, 18)
          rctOut = New Rectangle(0, 0, 37, 18)
          g.DrawImage(m_Panel, rctOut, rctIn, GraphicsUnit.Pixel)
          g.Dispose()
          nID = Main.GetID
          m_Tiles.Add(New Tile(nID, bmpTile, Me, i, k))
        End If
        n += 1
        x += 45
      Next
      y += 45
    Next

  End Sub

  Public ReadOnly Property Tiles() As List(Of Tile)
    Get
      Return m_Tiles
    End Get
  End Property

  Public ReadOnly Property PanelNumber() As Integer
    Get
      Return m_PanelNumber
    End Get
  End Property

End Class
