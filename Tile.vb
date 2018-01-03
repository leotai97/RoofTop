Public Class Tile

  Private m_Image As Bitmap
  Private m_Color As RFColor
  Private m_Panel As TilePanel
  Private m_PosX As Integer
  Private m_posY As Integer
  Private m_ID As Integer

  Public Sub New(ByVal nID As Integer, ByVal bmp As Bitmap, panel As TilePanel, ByVal ty As Integer, ByVal tx As Integer)
    Dim x, y As Integer
    Dim r, g, b As Long
    Dim c As Color
    Dim i As Integer
    Dim dic As New Dictionary(Of Color, ColorItem)
    ' Dim item As ColorItem

    m_ID = nID
    m_PosX = tx
    m_posY = ty
    m_Panel = panel
    m_Image = bmp

    r = 0
    g = 0
    b = 0

    i = 0
    For x = 0 To m_Image.Width - 1
      For y = 0 To m_Image.Height - 1
        c = m_Image.GetPixel(x, y)
        '        If dic.ContainsKey(c) Then dic.Item(c).Count += 1 Else dic.Add(c, New ColorItem(c))
        r += c.R
        g += c.G
        b += c.B
        i += 1
      Next
    Next

    'm = 0
    'For Each item In dic.Values
    '  If item.Count > m Then
    '    m = item.Count
    '    c = item.Color
    '  End If
    'Next

    m_Color = New RFColor(Drawing.Color.FromArgb(r / i, g / i, b / i))

  End Sub

  Public ReadOnly Property Color() As RFColor
    Get
      Return m_Color
    End Get
  End Property

  Public ReadOnly Property Image() As Bitmap
    Get
      Return m_Image
    End Get
  End Property

  Public ReadOnly Property Panel() As TilePanel
    Get
      Return m_Panel
    End Get
  End Property

  Public ReadOnly Property X() As Integer
    Get
      Return m_PosX
    End Get
  End Property

  Public ReadOnly Property Y() As Integer
    Get
      Return m_posY
    End Get
  End Property

  Public ReadOnly Property ID() As Integer
    Get
      Return m_ID
    End Get
  End Property

End Class

Public Class ColorItem
  Public Color As Color
  Public Count As Integer

  Public Sub New(ByVal c As Color)
    Me.Color = c
    Me.Count = 1
  End Sub

End Class
