Imports RoofTop

Public Class MainWnd

  Private m_Panels As List(Of TilePanel)
  Private m_Roof As Roof
  Private m_Tiles As Dictionary(Of Integer, Tile)
  Private m_Placeholder As Image             ' for grid drawing no tile
  Private m_eMethod As Roof.enumMethod       ' dark, light, or best 
  Private m_eLineType As Roof.enumLine       ' for row/column or full setting
  Private m_nLinePos As Integer              ' for row/column option position
  Private m_pointLast As Point               ' for mouse click lookup tile

  Private m_ID As Integer

  ''' <summary>
  ''' to give each tile a unique Id
  ''' </summary>
  ''' <returns></returns>
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

    m_Tiles = New Dictionary(Of Integer, Tile)
    For Each panel In m_Panels
      For Each tile In panel.Tiles
        m_Tiles.Add(tile.ID, tile)
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
          g.DrawImage(m_Tiles.Item(i + 1).Image, rctOut, rctIn, GraphicsUnit.Pixel)
          br = New SolidBrush(m_Tiles.Item(i + 1).Color.Color)
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

    m_Roof = Nothing
    m_Placeholder = My.Resources.Placeholder

    picGen.SizeMode = PictureBoxSizeMode.StretchImage

    listTiles.ListViewItemSorter = New TileListSorter

  End Sub

  Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
    Dim d As OpenFileDialog
    Dim r As DialogResult
    Dim bmp As Bitmap = Nothing
    Dim blnBad As Boolean ' so we can dispose bmp

    If Not m_Roof Is Nothing Then m_Roof.Dispose()
    d = New OpenFileDialog
    r = d.ShowDialog

    If r = DialogResult.OK Then
      bmp = Image.FromFile(d.FileName)
    End If
    d.Dispose()
    If r <> DialogResult.OK Then Return

    blnBad = False
    If bmp.Width > 64 Then MsgBox("Width of image cannot be greater than 64, it's " & bmp.Width, MsgBoxStyle.Information) : blnBad = True
    If bmp.Height > 64 Then MsgBox("Height of image cannot be greater than 64, it's " & bmp.Height, MsgBoxStyle.Information) : blnBad = True
    If blnBad = True Then
      bmp.Dispose()
      Return
    End If

    txtFile.Text = d.FileName

    m_eLineType = Roof.enumLine.Square
    radLineAll.Checked = True
    m_nLinePos = 0
    txtLine.Text = "1"

    m_Roof = New Roof(bmp, m_Placeholder, m_Tiles)

    picOrig.Image = m_Roof.Original
    picOrig.SizeMode = PictureBoxSizeMode.StretchImage

    GenerateRoof()

  End Sub

  Private Sub GenerateRoof()
    Dim map As Map
    Dim list As New Dictionary(Of Integer, Tile)

    If m_Roof Is Nothing Then Return

    m_Roof.GenerateRoof(m_eMethod)

    For Each map In m_Roof.Mapping.Values
      If list.ContainsKey(map.Tile.ID) = False Then list.Add(map.Tile.ID, map.Tile)
    Next

    LoadList(list)
    DrawGrid()

  End Sub

  Private Sub LoadList(ByVal list As Dictionary(Of Integer, Tile))
    Dim tile As Tile
    Dim item As TileItem
    Dim i As Integer

    listTiles.Items.Clear()

    i = 1
    For Each tile In list.Values
      item = New TileItem(tile, i)
      item.ImageKey = "I" & tile.ID
      listTiles.Items.Add(item)
      i += 1
    Next

    c1.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
    c2.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
    c3.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
    c4.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
    '  c5.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
  End Sub


  Private Sub LoadLineTiles()
    Dim x, y As Integer
    Dim c As RFColor
    Dim map As Map
    Dim list As Dictionary(Of Integer, Tile)

    If m_eLineType = Roof.enumLine.Square Then Return

    list = New Dictionary(Of Integer, Tile)

    If m_eLineType = Roof.enumLine.Column Then
      For y = 0 To m_Roof.Height - 1
        c = New RFColor(m_Roof.Original.GetPixel(m_nLinePos, y))
        map = m_Roof.Mapping.Item(c.CV)
        If list.ContainsKey(map.Tile.ID) = False Then list.Add(map.Tile.ID, map.Tile)
      Next
    Else
      For x = 0 To m_Roof.Width - 1
        c = New RFColor(m_Roof.Original.GetPixel(x, m_nLinePos))
        map = m_Roof.Mapping.Item(c.CV)
        If list.ContainsKey(map.Tile.ID) = False Then list.Add(map.Tile.ID, map.Tile)
      Next
    End If

    LoadList(list)

  End Sub

  Private Sub DrawGrid()
    Dim current As Dictionary(Of Integer, Tile)
    Dim bmps As GeneratedBitmaps
    Dim item As TileItem

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

    bmps = m_Roof.DrawGrid(current, m_eLineType, m_nLinePos)

    If Not picGen.Image Is Nothing Then picGen.Image.Dispose()
    picGen.Image = bmps.Small

    If Not picWork.Image Is Nothing Then picWork.Image.Dispose()
    picWork.Image = bmps.Large

    If chkFull.Checked = True Then
      picWork.SizeMode = PictureBoxSizeMode.StretchImage
      picWork.Size = Panel1.ClientSize
    Else
      picWork.SizeMode = PictureBoxSizeMode.AutoSize
    End If

  End Sub

  'Dim item As TileItem
  '  Dim x, y As Integer
  '  Dim bmp, bmp2 As Bitmap
  '  Dim g As Graphics
  '  Dim rctIn, rctOut As Rectangle
  '  Dim current As Dictionary(Of Integer, Tile)
  '  Dim blnDraw As Boolean

  '  bmp = New Bitmap(m_bitmap.Width * 37, m_bitmap.Height * 18, Imaging.PixelFormat.Format24bppRgb)
  '  bmp2 = New Bitmap(m_bitmap.Width, m_bitmap.Height, Imaging.PixelFormat.Format24bppRgb)
  '  g = Graphics.FromImage(bmp)

  '  If listTiles.SelectedItems.Count = 0 Then
  '    current = Nothing
  '  Else
  '    current = New Dictionary(Of Integer, Tile)
  '    For Each item In listTiles.SelectedItems
  '      current.Add(item.Tile.ID, item.Tile)
  '    Next
  '    item = listTiles.SelectedItems(0)
  '    picTile.Image = item.Tile.Image
  '  End If

  '  For i = 0 To m_bitmap.Width - 1
  '    For k = 0 To m_bitmap.Height - 1
  '      x = i * 37
  '      y = k * 18
  '      rctOut = New Rectangle(x, y, 37, 18)
  '      rctIn = New Rectangle(0, 0, 37, 18)
  '      If current Is Nothing Then
  '        blnDraw = True
  '      Else
  '        blnDraw = current.ContainsKey(m_Matched(k, i).ID)
  '      End If
  '      If blnDraw = True Then
  '        g.DrawImage(m_Matched(k, i).Image, rctOut, rctIn, GraphicsUnit.Pixel)
  '      Else
  '        g.DrawImage(m_Placeholder, rctOut, rctIn, GraphicsUnit.Pixel)
  '      End If
  '      bmp2.SetPixel(i, k, m_Matched(k, i).Color)
  '    Next
  '  Next
  '  g.Dispose()

  '  If Not picWork.Image Is Nothing Then picWork.Image.Dispose()
  '  picWork.Image = bmp
  '  picWork.SizeMode = PictureBoxSizeMode.AutoSize

  '  '  bmp.Save("test.jpg", Imaging.ImageFormat.Jpeg)
  '  If Not picGen.Image Is Nothing Then picGen.Image.Dispose()
  '  picGen.Image = bmp2
  '  picGen.SizeMode = PictureBoxSizeMode.StretchImage


  'End Sub

  Private Sub listTiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listTiles.SelectedIndexChanged
    DrawGrid()
  End Sub


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

    If chkFull.Checked = True Then picWork.Size = Panel1.ClientSize

  End Sub

  Private Sub radDarker_Click(sender As Object, e As EventArgs) Handles radDarker.Click
    m_eMethod = Roof.enumMethod.Darker
    GenerateRoof()
  End Sub

  Private Sub radLighter_Click(sender As Object, e As EventArgs) Handles radLighter.Click
    m_eMethod = Roof.enumMethod.Lighter
    GenerateRoof()
  End Sub

  Private Sub radNearest_Click(sender As Object, e As EventArgs) Handles radNearest.Click
    m_eMethod = Roof.enumMethod.Best
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

  Private Sub chkFull_Click(sender As Object, e As EventArgs) Handles chkFull.Click

    If chkFull.Checked = True Then
      picWork.SizeMode = PictureBoxSizeMode.StretchImage
      picWork.Size = Panel1.ClientSize
    Else
      picWork.SizeMode = PictureBoxSizeMode.AutoSize
    End If

  End Sub

  Private Sub btnLineLess_Click(sender As Object, e As EventArgs) Handles btnLineLess.Click
    If m_nLinePos >= 1 Then m_nLinePos -= 1 : txtLine.Text = (m_nLinePos + 1).ToString
  End Sub

  Private Sub btnLineMore_Click(sender As Object, e As EventArgs) Handles btnLineMore.Click
    If m_nLinePos < m_Roof.Width - 1 Then m_nLinePos += 1 : txtLine.Text = (m_nLinePos + 1).ToString
  End Sub

  Private Sub txtLine_TextChanged(sender As Object, e As EventArgs) Handles txtLine.TextChanged
    Dim n As Integer

    If m_eLineType = Roof.enumLine.Square Then Return
    If m_Roof Is Nothing Then Return

    If Integer.TryParse(txtLine.Text, n) = True Then
      If n < 1 Then Return
      If m_eLineType = Roof.enumLine.Row Then
        If n > m_Roof.Width Then Return
      Else
        If n > m_Roof.Height Then Return
      End If
      m_nLinePos = n - 1
      LoadLineTiles()
      DrawGrid()
    End If

  End Sub

  Private Sub radLineAll_Click(sender As Object, e As EventArgs) Handles radLineAll.Click
    m_eLineType = Roof.enumLine.Square
    LoadLineTiles()
    DrawGrid()
  End Sub

  Private Sub radLineCols_Click(sender As Object, e As EventArgs) Handles radLineCols.Click
    m_eLineType = Roof.enumLine.Column
    LoadLineTiles()
    DrawGrid()
  End Sub

  Private Sub radLineRows_Click(sender As Object, e As EventArgs) Handles radLineRows.Click
    m_eLineType = Roof.enumLine.Row
    LoadLineTiles()
    DrawGrid()
  End Sub

  Private Sub picwork_MouseDown(sender As Object, e As MouseEventArgs) Handles picWork.MouseDown

    If m_Roof Is Nothing Then Return
    m_pointLast = m_Roof.ConvertRoofCoords(e.X, e.Y)

  End Sub


  Private Sub picwork_MouseUp(sender As Object, e As MouseEventArgs) Handles picWork.MouseUp
    Dim pt As Point
    '  Dim c As RFColor
    Dim item As TileItem
    Dim tile As Tile
    Dim i As Integer

    If m_Roof Is Nothing Then Return
    If m_Roof.Generated Is Nothing Then Return  ' image not selected yet

    pt = m_Roof.ConvertRoofCoords(e.X, e.Y)
    If m_pointLast.X <> pt.X Or m_pointLast.Y <> pt.Y Then Return  ' allow user to cancel tile lookup click by moving mouse before releasing button

    tile = m_Roof.Generated(pt.X, pt.Y)
    For Each item In listTiles.Items
      If item.Tile.ID = tile.ID Then
        listTiles.SelectedItems.Clear()
        listTiles.SelectedIndices.Add(i)
        Return
      End If
      i += 1
    Next

    MsgBox("Selected area not found in current list of tiles. Area is probably outside of current column / row.", MsgBoxStyle.Information)


  End Sub

End Class

Public Class TileListSorter
  Implements IComparer

  Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
    Dim i1, i2 As ListViewItem
    Dim r1, r2 As String
    Dim ri1, ri2, p1, p2, col1, col2 As Integer

    i1 = x
    i2 = y

    p1 = CInt(i1.SubItems(1).Text)
    r1 = i1.SubItems(2).Text
    If r1.ToLower = "top" Then ri1 = 1 Else ri1 = 2
    col1 = CInt(i1.SubItems(3).Text)

    p2 = CInt(i2.SubItems(1).Text)
    r2 = i2.SubItems(2).Text
    If r2.ToLower = "top" Then ri2 = 1 Else ri2 = 2
    col2 = CInt(i2.SubItems(3).Text)

    If p1 > p2 Then Return 1
    If p1 < p2 Then Return -1

    If ri1 > ri2 Then Return 1
    If ri1 < ri2 Then Return -1

    If col1 > col2 Then Return 1
    If col1 < col2 Then Return -1

    Return 0

  End Function



End Class