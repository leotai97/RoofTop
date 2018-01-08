﻿

Public Class Roof
  Implements IDisposable

  Public Enum enumMethod
    Darker
    Lighter
    Best
  End Enum

  Public Enum enumLine
    Row
    Column
    Square
  End Enum

  Private m_Original As Bitmap
  Private m_Generated(,) As Tile                              ' without any interface lines added
  Private m_Mapping As Dictionary(Of Integer, Map)            ' indexed by RFColor.CV
  Private m_Colors As Dictionary(Of Integer, RFColor)
  Private m_placeholder As Bitmap
  Private m_eMethod As enumMethod
  Private m_Tiles As Dictionary(Of Integer, Tile)

  Public Sub New(ByVal bmp As Bitmap, ByVal place As Bitmap, ByVal tiles As Dictionary(Of Integer, Tile))
    m_Original = bmp
    m_placeholder = place
    m_Tiles = tiles
    UniqueColors()
    m_Generated = Nothing
  End Sub

  Public ReadOnly Property Original() As Bitmap
    Get
      Return m_Original
    End Get
  End Property

  Public ReadOnly Property Mapping() As Dictionary(Of Integer, Map)
    Get
      Return m_Mapping
    End Get
  End Property

  Public ReadOnly Property Width() As Integer
    Get
      If m_Original Is Nothing Then Return 0
      Return m_Original.Width
    End Get
  End Property

  Public ReadOnly Property Height() As Integer
    Get
      If m_Original Is Nothing Then Return 0
      Return m_Original.Height
    End Get
  End Property

  Public ReadOnly Property Generated() As Tile(,)
    Get
      Return m_Generated
    End Get
  End Property

  Private Sub UniqueColors()
    Dim i, k As Integer
    Dim cu As Color, c As RFColor

    m_Colors = New Dictionary(Of Integer, RFColor)

    For i = 0 To Me.Width - 1
      For k = 0 To Me.Height - 1
        cu = m_Original.GetPixel(i, k)
        c = New RFColor(cu)
        If m_Colors.ContainsKey(c.CV) = False Then m_Colors.Add(c.CV, c)
      Next
    Next

  End Sub

  Public Sub GenerateRoof(ByVal eMethod As enumMethod)
    Dim tile As Tile
    Dim c As RFColor

    m_eMethod = eMethod

    m_Mapping = New Dictionary(Of Integer, Map)

    For Each c In m_Colors.Values
      tile = GetBestTile(c)
      m_Mapping.Add(c.CV, New Map(c, tile))
    Next

  End Sub

  Public Function DrawGrid(ByVal current As Dictionary(Of Integer, Tile), eLine As enumLine, ByVal nPos As Integer) As GeneratedBitmaps
    Dim bmps As GeneratedBitmaps
    Dim x, y As Integer
    Dim g As Graphics
    Dim rctIn, rctOut As Rectangle
    Dim blnDraw As Boolean
    Dim c As RFColor
    Dim map As Map
    Dim font As Font
    Dim fmt As New StringFormat
    Dim pen As New Drawing.Pen(Brushes.Blue, 2)

    fmt.Alignment = StringAlignment.Center
    font = New Font("ms sans serif", 11, FontStyle.Regular, GraphicsUnit.Pixel)

    bmps = New GeneratedBitmaps

    bmps.Large = New Bitmap(Me.Width * 37 + Me.Width + 2, Me.Height * 18 + Me.Height + 2, Imaging.PixelFormat.Format24bppRgb)
    bmps.Small = New Bitmap(Me.Width, Me.Height, Imaging.PixelFormat.Format24bppRgb)

    ReDim m_Generated(Me.Width - 1, Me.Height - 1)

    g = Graphics.FromImage(bmps.Large)
    g.InterpolationMode = Drawing2D.InterpolationMode.High

    For i = 0 To Me.Width - 1
      For k = 0 To Me.Height - 1
        c = New RFColor(m_Original.GetPixel(i, k))
        map = m_Mapping.Item(c.CV)
        x = i * (37 + 1) + 1
        y = k * (18 + 1) + 1
        rctOut = New Rectangle(x, y, 37, 18)
        rctIn = New Rectangle(0, 0, 37, 18)
        If current Is Nothing Then
          blnDraw = True
        Else
          blnDraw = current.ContainsKey(map.Tile.ID)
        End If
        If blnDraw = True Then
          g.DrawImage(map.Tile.Image, rctOut, rctIn, GraphicsUnit.Pixel)
        Else
          g.DrawImage(m_placeholder, rctOut, rctIn, GraphicsUnit.Pixel)
          rctIn = New Rectangle(x + 2, y + 2, 37 - 4, 18 - 4)
          g.DrawString((i + 1) & "/" & (k + 1), font, Brushes.DarkBlue, rctIn, fmt)
        End If
        bmps.Small.SetPixel(i, k, map.Tile.Color.Color)
        m_Generated(i, k) = map.Tile
      Next
    Next

    Select Case eLine
      Case enumLine.Row
        g.DrawLine(pen, New Point(0, nPos * (18 + 1)), New Point(bmps.Large.Width, nPos * (18 + 1)))
        g.DrawLine(pen, New Point(0, (nPos + 1) * (18 + 1)), New Point(bmps.Large.Width, (nPos + 1) * (18 + 1)))
        For i = 0 To Me.Width - 1
          bmps.Small.SetPixel(i, nPos, Color.Black)
        Next
      Case enumLine.Column
        g.DrawLine(pen, New Point(nPos * (37 + 1), 0), New Point(nPos * (37 + 1), bmps.Large.Height))
        g.DrawLine(pen, New Point((nPos + 1) * (37 + 1), 0), New Point((nPos + 1) * (37 + 1), bmps.Large.Height))
        For i = 0 To Me.Height - 1
          bmps.Small.SetPixel(nPos, i, Color.Black)
        Next

    End Select

    g.Dispose()
    pen.Dispose()

    Return bmps

  End Function

  ''' <summary>
  ''' take a point from the large roof output and convert to the small bitmap coords for tile lookup
  ''' </summary>
  ''' <param name="ix"></param>
  ''' <param name="iy"></param>
  ''' <returns></returns>
  Public Function ConvertRoofCoords(ByVal ix As Integer, ByVal iy As Integer) As Point
    Dim x, y As Integer

    x = ix \ (37 + 1)   ' note: no rounding division using "\"
    y = iy \ (18 + 1)
    Return New Point(x, y)

  End Function

  Public Function DrawGridx(ByVal current As Dictionary(Of Integer, Tile)) As GeneratedBitmaps
    Dim bmps As GeneratedBitmaps
    Dim x, y As Integer
    Dim g As Graphics
    Dim rctIn, rctOut As Rectangle
    Dim blnDraw As Boolean
    Dim c As RFColor
    Dim map As Map
    Dim d As Double = Math.Sqrt(2)
    Dim bmpTL, bmpTS As Bitmap
    Dim mat As New Drawing2D.Matrix
    Dim w, h As Integer


    bmps = New GeneratedBitmaps

    bmps.Large = New Bitmap(CInt(Me.Width * 37 * d), CInt(Me.Height * 37 * d), Imaging.PixelFormat.Format24bppRgb)
    bmps.Small = New Bitmap(CInt(Me.Width * d), CInt(Me.Height * d), Imaging.PixelFormat.Format24bppRgb)

    bmpTL = New Bitmap(Me.Width * 37, Me.Height * 37, Imaging.PixelFormat.Format24bppRgb)
    bmpTS = New Bitmap(Me.Width * d, Me.Height * d, Imaging.PixelFormat.Format24bppRgb)

    w = bmps.Large.Width
    h = bmps.Large.Height

    g = Graphics.FromImage(bmpTL)

    For i = 0 To Me.Width - 1
      For k = 0 To Me.Height - 1
        c = New RFColor(m_Original.GetPixel(i, k))
        map = m_Mapping.Item(c.CV)
        x = i * 37
        y = k * 37
        rctOut = New Rectangle(x, y, 37, 37)
        rctIn = New Rectangle(0, 0, 37, 18)
        If current Is Nothing Then
          blnDraw = True
        Else
          blnDraw = current.ContainsKey(map.Tile.ID)
        End If
        If blnDraw = True Then
          g.DrawImage(map.Tile.Image, rctOut, rctIn, GraphicsUnit.Pixel)
        Else
          g.DrawImage(m_placeholder, rctOut, rctIn, GraphicsUnit.Pixel)
        End If
        bmps.Small.SetPixel(i, k, map.Tile.Color.Color)
      Next
    Next
    g.Dispose()

    g = Graphics.FromImage(bmps.Large)
    g.InterpolationMode = Drawing2D.InterpolationMode.High

    rctIn = New Rectangle(0, 0, bmpTL.Width, bmpTL.Height)
    rctOut = New Rectangle(CInt(w / 2 - bmpTL.Width / 2), CInt(h / 2 - bmpTL.Height / 2), bmpTL.Width, bmpTL.Height)

    mat.RotateAt(45, New Point(rctOut.Width / 2, rctOut.Height / 2))
    g.Transform = mat
    ' g.ScaleTransform(1, 0.4)

    g.DrawImage(bmpTL, rctOut, rctIn, GraphicsUnit.Pixel)
    g.Dispose()

    Return bmps

  End Function


  Private Function GetBestTile(ByVal c As RFColor) As Tile
    Dim best As Tile

    best = Nothing

    If m_eMethod = enumMethod.Darker = True Then
      best = Darker(c)
      If Not best Is Nothing Then Return best
      Return Nearest(c)
    End If

    If m_eMethod = enumMethod.Lighter Then
      best = Lighter(c)
      If Not best Is Nothing Then Return best
      Return Nearest(c)
    End If

    Return Nearest(c)

  End Function

  Private Function Darker(ByVal c As RFColor) As Tile
    Dim tile, best As Tile
    Dim br, bg, bb As Integer
    Dim ar, ag, ab As Integer

    best = Nothing

    br = 512
    bg = 512
    bb = 512

    For Each tile In m_Tiles.Values

      ar = c.R - tile.Color.R
      ag = c.G - tile.Color.G
      ab = c.B - tile.Color.B

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

  Private Function Lighter(ByVal c As RFColor) As Tile
    Dim tile, best As Tile
    Dim br, bg, bb As Integer
    '  Dim ar, ag, ab As Integer

    br = 512
    bg = 512
    bb = 512

    best = Nothing

    For Each tile In m_Tiles.Values

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

    For Each tile In m_Tiles.Values
      If best Is Nothing Then
        best = tile
      Else
        If tile.Color.R + tile.Color.G + tile.Color.B > best.Color.R + best.Color.G + best.Color.B Then best = tile
      End If

    Next

    Return best

  End Function

  Private Function Nearest(ByVal c As RFColor) As Tile
    Dim tile, best As Tile
    Dim br, bg, bb As Integer
    Dim ar, ag, ab As Integer

    br = 512
    bg = 512
    bb = 512

    best = Nothing

    For Each tile In m_Tiles.Values

      ar = Math.Abs(tile.Color.R - c.R)
      ag = Math.Abs(tile.Color.G - c.G)
      ab = Math.Abs(tile.Color.B - c.B)

      If ar <= br And ag <= bg And ab <= bb Then
        best = tile
        br = ar
        bg = ag
        bb = ab
      End If

    Next
    Return best

  End Function


#Region "IDisposable Support"
  Private disposedValue As Boolean ' To detect redundant calls

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: dispose managed state (managed objects).
      End If

      If Not m_Original Is Nothing Then m_Original.Dispose()

    End If
    disposedValue = True
  End Sub

  ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
  'Protected Overrides Sub Finalize()
  '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
  '    Dispose(False)
  '    MyBase.Finalize()
  'End Sub

  ' This code added by Visual Basic to correctly implement the disposable pattern.
  Public Sub Dispose() Implements IDisposable.Dispose
    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    Dispose(True)
    ' TODO: uncomment the following line if Finalize() is overridden above.
    ' GC.SuppressFinalize(Me)
  End Sub
#End Region

End Class


Public Class Map
  Public Color As RFColor
  Public Tile As Tile

  Public Sub New(ByVal c As RFColor, ByVal t As Tile)
    Me.Color = c
    Me.Tile = t
  End Sub

End Class

Public Class RFColor
  Public R As Integer
  Public G As Integer
  Public B As Integer

  Private m_cv As Integer

  Private m_Sum As Integer

  Public Sub New(ByVal c As Color)

    Me.R = c.R
    Me.G = c.G
    Me.B = c.B

    m_cv = (Me.R << 16) Or (Me.G << 8) Or B
    m_Sum = Me.R + Me.G + Me.B

  End Sub

  Public Sub New(ByVal rv As Integer, gv As Integer, bv As Integer)
    Me.R = rv
    Me.G = gv
    Me.B = bv
    m_Sum = Me.R + Me.G + Me.B
  End Sub

  Public ReadOnly Property Sum() As Integer
    Get
      Return m_Sum
    End Get
  End Property

  Public ReadOnly Property Color() As Color
    Get
      Return Color.FromArgb(R, G, B)
    End Get
  End Property

  Public ReadOnly Property CV() As Integer
    Get
      Return m_cv
    End Get
  End Property

End Class

Public Class GeneratedBitmaps
  Public Small As Bitmap
  Public Large As Bitmap
End Class