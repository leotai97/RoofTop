Public Class RFColor
  Implements IComparable

  Public Enum enumDominate
    None
    Red
    Green
    Blue
  End Enum

  Public Const High As Integer = 200
  Public Const Low As Integer = 40

  Public R As Integer
  Public G As Integer
  Public B As Integer

  Private m_cv As Integer

  Private m_Sum As Integer

  Private m_H As Integer
  Private m_S As Integer
  Private m_L As Integer

  Private m_modulus As Double


  Public Sub New(ByVal c As Color)

    Me.R = c.R
    Me.G = c.G
    Me.B = c.B

    m_cv = (Me.R << 16) Or (Me.G << 8) Or B
    m_Sum = Me.R + Me.G + Me.B

    DoHSL()


  End Sub

  Public Sub New(ByVal rv As Integer, gv As Integer, bv As Integer)
    Me.R = rv
    Me.G = gv
    Me.B = bv
    m_Sum = Me.R + Me.G + Me.B

    DoHSL()

  End Sub

  Private Sub DoHSL()
    Dim r, g, b As Double
    Dim max, min As Double

    Dim a1 As Double = 1 ' 0.5
    Dim b1 As Double = 1 ' 0.5
    Dim c1 As Double = 1


    r = Me.R / 255
    g = Me.G / 255
    b = Me.B / 255

    max = Math.Max(r, Math.Max(g, b))
    min = Math.Min(r, Math.Min(g, b))

    ' hue

    If max = min Then
      m_H = 0
    Else
      If max = r And g >= b Then m_H = 60.0 * (g - b) / (max - min)
      If max = r And b > g Then m_H = 60.0 * (g - b) / (max - min) + 360
      If max = g Then m_H = 60 * (b - r) / (max - min) + 120
      If max = b Then m_H = 60 * (r - g) / (max - min) + 240
    End If

    m_L = (max + min) / 2.0

    If m_L = 0 Or max = min Then
      m_S = 0
    Else
      If 0 < m_L And m_L <= 0.5 Then
        m_S = (max - min) / (max - min)
      Else
        m_S = (max - min) / (2 - (max - min))
      End If
    End If

    m_modulus = Math.Sqrt(a1 * m_H * m_H + b * m_S * m_S + c1 * m_L * m_L)

  End Sub


  Public Function CompareTo(objin As Object) As Integer Implements IComparable.CompareTo
    Dim obj As RFColor = objin

    If obj.m_modulus > Me.m_modulus Then Return 1
    If obj.m_modulus < Me.m_modulus Then Return -1

    Return 0

  End Function

  Public ReadOnly Property Modulus() As Double
    Get
      Return m_modulus
    End Get
  End Property

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
  ''' <summary>
  ''' Check for a dominate color
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property Dominate() As enumDominate
    Get

      ' Return enumDominate.None

      If Me.R > High Then
        If Me.G < Low And Me.B < Low Then Return enumDominate.Red
      End If

      If Me.G > High Then
        If Me.R < Low And Me.B < Low Then Return enumDominate.Green
      End If

      If Me.B > High Then
        If Me.R < Low And Me.G < Low Then Return enumDominate.Blue
      End If

      Return enumDominate.None
    End Get
  End Property

End Class

