Imports RoofTop

Public Class MainWnd

  Private m_Panels As List(Of TilePanel)
  Private m_bitmap As Bitmap
  Private m_Tiles As List(Of Tile)
  Private m_Matched(,) As Tile
  Private m_Placeholder As Image

  Private m_ID As Integer

  Public Function GetID() As Integer
    m_ID += 1
    Return m_ID

  End Function


  Public Sub New()

    Dim bmp As Bitmap
    Dim x, y, i As Integer
    Dim tile As Tile
    Dim panel As TilePanel
    Dim rctIn, rctOut As Rectangle
    Dim g As Graphics
    Dim br As Brush
    Dim lv As ListViewItem
    Dim s1, s2 As String

    InitializeComponent()

    m_ID = 0

    m_Panels = New List(Of TilePanel)

    m_Panels.Add(New TilePanel(1, 20, Me, My.Resources.panel1))
    m_Panels.Add(New TilePanel(2, 20, Me, My.Resources.panel2))
    m_Panels.Add(New TilePanel(3, 20, Me, My.Resources.panel3))
    m_Panels.Add(New TilePanel(4, 20, Me, My.Resources.panel4))
    m_Panels.Add(New TilePanel(5, 20, Me, My.Resources.panel5))
    m_Panels.Add(New TilePanel(6, 20, Me, My.Resources.panel6))
    m_Panels.Add(New TilePanel(7, 20, Me, My.Resources.panel7))
    m_Panels.Add(New TilePanel(8, 20, Me, My.Resources.panel8))
    m_Panels.Add(New TilePanel(9, 20, Me, My.Resources.panel9))
    m_Panels.Add(New TilePanel(10, 20, Me, My.Resources.panel10))
    m_Panels.Add(New TilePanel(11, 20, Me, My.Resources.panel11))
    m_Panels.Add(New TilePanel(12, 12, Me, My.Resources.panel12))

    m_Tiles = New List(Of Tile)
    For Each panel In m_Panels
      For Each tile In panel.Tiles
        m_Tiles.Add(tile)
        ImageList1.Images.Add("I" & tile.ID, tile.Image)
        lv = New ListViewItem
        s1 = Strings.Right("000" & (CInt(tile.Color.R) + CInt(tile.Color.G) + CInt(tile.Color.B)), 3)
        s2 = Strings.Right("000" & tile.Color.R, 3) & "," & Strings.Right("000" & tile.Color.G, 3) & "," & Strings.Right("000" & tile.Color.B, 3)
        lv.Text = s1
        lv.SubItems.Add(s2)
        lv.SubItems.Add(tile.ID)
        lv.ImageKey = "I" & tile.ID
        listAll.items.add(lv)
      Next
    Next
    listAll.Sort()
    cc1.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)

    bmp = New Bitmap(37 * 20, 18 * 17 * 2 + 10, Imaging.PixelFormat.Format24bppRgb)

    g = Graphics.FromImage(bmp)
    g.FillRectangle(Drawing.SystemBrushes.Window, New Rectangle(0, 0, 37 * 20, 18 * 17))
    i = 0
    For x = 0 To 15
      For y = 0 To 17
        If i < m_Tiles.Count Then
          rctIn = New Rectangle(0, 0, 37, 18)
          rctOut = New Rectangle(x * 37, y * 18, 37, 18)
          g.DrawImage(m_Tiles.Item(i).Image, rctOut, rctIn, GraphicsUnit.Pixel)
          br = New SolidBrush(m_Tiles.Item(i).Color)
          g.FillRectangle(br, New Rectangle(x * 37, (18 * 18 + y * 18) + 10, 37, 18))
          br.Dispose()
          i += 1
        End If
      Next
    Next
    picWork.Image = bmp
    picWork.SizeMode = PictureBoxSizeMode.AutoSize
    g.Dispose()

    picWork.Top = 0
    picWork.Left = 0

    m_bitmap = Nothing
    m_Placeholder = My.Resources.Placeholder

  End Sub

  Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
    Dim d As OpenFileDialog
    Dim r As DialogResult

    If Not m_bitmap Is Nothing Then m_bitmap.Dispose()
    d = New OpenFileDialog
    r = d.ShowDialog

    If r = DialogResult.OK Then
      m_bitmap = Image.FromFile(d.FileName)
    End If
    d.Dispose()
    If r <> DialogResult.OK Then Return

    If m_bitmap.Width > 64 Then MsgBox("Width of image cannot be greater than 64, it's " & m_bitmap.Width, MsgBoxStyle.Information) : Return
    If m_bitmap.Height > 64 Then MsgBox("Height of image cannot be greater than 64, it's " & m_bitmap.Height, MsgBoxStyle.Information) : Return

    txtFile.Text = d.FileName

    picOrig.Image = m_bitmap
    picOrig.SizeMode = PictureBoxSizeMode.StretchImage

    GenerateRoof()

  End Sub

  Private Sub GenerateRoof()
    Dim i, k As Integer
    Dim tile As Tile
    Dim dic As Dictionary(Of Integer, Tile)
    Dim item As TileItem

    If m_bitmap Is Nothing Then Return

    ReDim m_Matched(m_bitmap.Height - 1, m_bitmap.Width - 1)

    dic = New Dictionary(Of Integer, Tile)

    For i = 0 To m_bitmap.Width - 1
      For k = 0 To m_bitmap.Height - 1
        Tile = GetBestTile(m_bitmap.GetPixel(i, k))
        m_Matched(k, i) = Tile
        If dic.ContainsKey(Tile.ID) = False Then dic.Add(Tile.ID, Tile)
      Next
    Next

    listTiles.Items.Clear()

    i = 1
    For Each tile In dic.Values
      item = New TileItem(tile, i)
      item.ImageKey = "I" & tile.ID
      listTiles.Items.Add(item)
      i += 1
    Next

    c1.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
    c2.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
    c3.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
    c4.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
    c5.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
    DrawGrid()

  End Sub

  Private Sub DrawGrid()
    Dim item As TIleItem
    Dim x, y As Integer
    Dim bmp, bmp2 As Bitmap
    Dim g As Graphics
    Dim rctIn, rctOut As Rectangle
    Dim current As Dictionary(Of Integer, Tile)
    Dim blnDraw As Boolean

    bmp = New Bitmap(m_bitmap.Width * 37, m_bitmap.Height * 18, Imaging.PixelFormat.Format24bppRgb)
    bmp2 = New Bitmap(m_bitmap.Width, m_bitmap.Height, Imaging.PixelFormat.Format24bppRgb)
    g = Graphics.FromImage(bmp)

    If listTiles.SelectedItems.Count = 0 Then
      current = Nothing
    Else
      current = New Dictionary(Of Integer, Tile)
      For Each item In listTiles.SelectedItems
        current.Add(item.Tile.ID, item.Tile)
      Next
      item = listTiles.SelectedItems(0)
      picTile.Image = item.Tile.Image
    End If

    For i = 0 To m_bitmap.Width - 1
      For k = 0 To m_bitmap.Height - 1
        x = i * 37
        y = k * 18
        rctOut = New Rectangle(x, y, 37, 18)
        rctIn = New Rectangle(0, 0, 37, 18)
        If current Is Nothing Then
          blnDraw = True
        Else
          blnDraw = current.ContainsKey(m_Matched(k, i).ID)
        End If
        If blnDraw = True Then
          g.DrawImage(m_Matched(k, i).Image, rctOut, rctIn, GraphicsUnit.Pixel)
        Else
          g.DrawImage(m_placeholder, rctOut, rctIn, GraphicsUnit.Pixel)
        End If
        bmp2.SetPixel(i, k, m_Matched(k, i).Color)
      Next
    Next
    g.Dispose()

    If Not picWork.Image Is Nothing Then picWork.Image.Dispose()
    picWork.Image = bmp
    picWork.SizeMode = PictureBoxSizeMode.AutoSize

    '  bmp.Save("test.jpg", Imaging.ImageFormat.Jpeg)
    If Not picGen.Image Is Nothing Then picGen.Image.Dispose()
    picGen.Image = bmp2
    picGen.SizeMode = PictureBoxSizeMode.StretchImage


  End Sub

  Private Sub listTiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listTiles.SelectedIndexChanged
    DrawGrid()
  End Sub

  Private Function GetBestTile(ByVal c As Color) As Tile
    Dim best As Tile

    best = Nothing

    If radDarker.Checked = True Then
      best = Darker(c)
      If Not best Is Nothing Then Return best
      Return Nearest(c)
    End If

    If radLighter.Checked = True Then
      best = Lighter(c)
      If Not best Is Nothing Then Return best
      Return Nearest(c)
    End If

    Return Nearest(c)

  End Function

  Private Function Darker(ByVal c As Color) As Tile
    Dim tile, best As Tile
    Dim br, bg, bb As Integer
    Dim ar, ag, ab As Integer

    best = Nothing

    br = 512
    bg = 512
    bb = 512

    For Each tile In m_Tiles

      ar = CInt(c.R) - CInt(tile.Color.R)
      ag = CInt(c.G) - CInt(tile.Color.G)
      ab = CInt(c.B) - CInt(tile.Color.B)

      If ar >= 0 And ag >= 0 And ab >= 0 Then   ' find a lower match first

        If ar <= br And ag <= bg And ab <= bb Then
          best = tile
          br = ar
          bg = ag
          bb = ab
        End If

      End If

    Next

    Return best

  End Function

  Private Function Lighter(ByVal c As Color) As Tile
    Dim tile, best As Tile
    Dim br, bg, bb As Integer
    Dim ar, ag, ab As Integer

    br = 512
    bg = 512
    bb = 512

    best = Nothing

    For Each tile In m_Tiles

      If tile.Color.R > c.R And tile.Color.G > c.G And tile.Color.B > c.B Then
        If best Is Nothing Then
          best = tile
        Else
          If tile.Color.R < best.Color.R And tile.Color.G < best.Color.G And tile.Color.B < best.Color.B Then
            best = tile
          End If
        End If
      End If

    Next

    If Not best Is Nothing Then Return best

    For Each tile In m_Tiles
      If best Is Nothing Then
        best = tile
      Else
        If CInt(tile.Color.R) + CInt(tile.Color.G) + CInt(tile.Color.B) > CInt(best.Color.R) + CInt(best.Color.G) + CInt(best.Color.B) Then best = tile
      End If

    Next

    Return best


    '  ar = CInt(tile.Color.R) - CInt(c.R)
    '  ag = CInt(tile.Color.G) - CInt(c.G)
    '  ab = CInt(tile.Color.B) - CInt(c.B)

    '  If ar >= 0 And ag >= 0 And ab >= 0 Then   ' find a higher match first

    '    If ar <= br And ag <= bg And ab <= bb Then
    '      best = tile
    '      br = ar
    '      bg = ag
    '      bb = ab
    '    End If

    '  End If

    'Next

    ' Return best
  End Function

  Private Function Nearest(ByVal c As Color) As Tile
    Dim tile, best As Tile
    Dim br, bg, bb As Integer
    Dim ar, ag, ab As Integer

    br = 512
    bg = 512
    bb = 512

    best = Nothing

    For Each tile In m_Tiles

      ar = Math.Abs(CInt(tile.Color.R) - CInt(c.R))
      ag = Math.Abs(CInt(tile.Color.G) - CInt(c.G))
      ab = Math.Abs(CInt(tile.Color.B) - CInt(c.B))

      If ar <= br And ag <= bg And ab <= bb Then
        best = tile
        br = ar
        bg = ag
        bb = ab
      End If

    Next
    Return best

  End Function

  Private Sub MainWnd_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    Dim w, h As Integer
    Dim cw, ch As Integer

    cw = Me.ClientSize.Width
    ch = Me.ClientSize.Height

    w = cw - (Panel1.Left + 10)
    If w > 1 Then Panel1.Width = w
    h = ch - (Panel1.Top + 10)
    If h > 1 Then Panel1.Height = h

    h = ch - (TabControl1.Top + 10)
    If h > 1 Then TabControl1.Height = h

  End Sub

  Private Sub radDarker_Click(sender As Object, e As EventArgs) Handles radDarker.Click
    GenerateRoof()
  End Sub

  Private Sub radLighter_Click(sender As Object, e As EventArgs) Handles radLighter.Click
    GenerateRoof()
  End Sub

  Private Sub radNearest_Click(sender As Object, e As EventArgs) Handles radNearest.Click
    GenerateRoof()
  End Sub

  Private Sub t1_Resize(sender As Object, e As EventArgs) Handles t1.Resize
    Dim n As Integer
    n = t1.ClientSize.Height - (listAll.Top + 4)
    If n > 1 Then
      listAll.Height = n
    End If
  End Sub

  Private Sub t2_Resize(sender As Object, e As EventArgs) Handles t2.Resize
    Dim n As Integer

    n = t2.ClientSize.Height - (listTiles.Top + 4)
    If n > 1 Then
      listTiles.Height = n
    End If

  End Sub

End Class

