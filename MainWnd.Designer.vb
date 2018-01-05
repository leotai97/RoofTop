<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainWnd
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Me.txtFile = New System.Windows.Forms.TextBox()
    Me.btnProcess = New System.Windows.Forms.Button()
    Me.btnBrowse = New System.Windows.Forms.Button()
    Me.picOrig = New System.Windows.Forms.PictureBox()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.picWork = New System.Windows.Forms.PictureBox()
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.picTile = New System.Windows.Forms.PictureBox()
    Me.radDarker = New System.Windows.Forms.RadioButton()
    Me.radLighter = New System.Windows.Forms.RadioButton()
    Me.radNearest = New System.Windows.Forms.RadioButton()
    Me.TabControl1 = New System.Windows.Forms.TabControl()
    Me.t1 = New System.Windows.Forms.TabPage()
    Me.listAll = New System.Windows.Forms.ListView()
    Me.cc1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.cc2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.cc3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.t2 = New System.Windows.Forms.TabPage()
    Me.txtLine = New System.Windows.Forms.TextBox()
    Me.btnLineMore = New System.Windows.Forms.Button()
    Me.btnLineLess = New System.Windows.Forms.Button()
    Me.radLineAll = New System.Windows.Forms.RadioButton()
    Me.radLineCols = New System.Windows.Forms.RadioButton()
    Me.radLineRows = New System.Windows.Forms.RadioButton()
    Me.listTiles = New System.Windows.Forms.ListView()
    Me.c1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.c2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.c3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.c4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.c5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.picGen = New System.Windows.Forms.PictureBox()
    Me.chkFull = New System.Windows.Forms.CheckBox()
    CType(Me.picOrig, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    CType(Me.picWork, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.picTile, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabControl1.SuspendLayout()
    Me.t1.SuspendLayout()
    Me.t2.SuspendLayout()
    CType(Me.picGen, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'txtFile
    '
    Me.txtFile.Location = New System.Drawing.Point(12, 12)
    Me.txtFile.Name = "txtFile"
    Me.txtFile.Size = New System.Drawing.Size(708, 20)
    Me.txtFile.TabIndex = 0
    '
    'btnProcess
    '
    Me.btnProcess.Location = New System.Drawing.Point(796, 10)
    Me.btnProcess.Name = "btnProcess"
    Me.btnProcess.Size = New System.Drawing.Size(58, 23)
    Me.btnProcess.TabIndex = 1
    Me.btnProcess.Text = "Process"
    Me.btnProcess.UseVisualStyleBackColor = True
    '
    'btnBrowse
    '
    Me.btnBrowse.Location = New System.Drawing.Point(726, 10)
    Me.btnBrowse.Name = "btnBrowse"
    Me.btnBrowse.Size = New System.Drawing.Size(64, 23)
    Me.btnBrowse.TabIndex = 2
    Me.btnBrowse.Text = "Browse"
    Me.btnBrowse.UseVisualStyleBackColor = True
    '
    'picOrig
    '
    Me.picOrig.Location = New System.Drawing.Point(34, 6)
    Me.picOrig.Name = "picOrig"
    Me.picOrig.Size = New System.Drawing.Size(128, 128)
    Me.picOrig.TabIndex = 3
    Me.picOrig.TabStop = False
    '
    'Panel1
    '
    Me.Panel1.AutoScroll = True
    Me.Panel1.Controls.Add(Me.picWork)
    Me.Panel1.Location = New System.Drawing.Point(311, 47)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(785, 564)
    Me.Panel1.TabIndex = 5
    '
    'picWork
    '
    Me.picWork.Location = New System.Drawing.Point(129, 156)
    Me.picWork.Name = "picWork"
    Me.picWork.Size = New System.Drawing.Size(538, 313)
    Me.picWork.TabIndex = 5
    Me.picWork.TabStop = False
    '
    'ImageList1
    '
    Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit
    Me.ImageList1.ImageSize = New System.Drawing.Size(37, 18)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    '
    'picTile
    '
    Me.picTile.Location = New System.Drawing.Point(21, 140)
    Me.picTile.Name = "picTile"
    Me.picTile.Size = New System.Drawing.Size(74, 37)
    Me.picTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
    Me.picTile.TabIndex = 11
    Me.picTile.TabStop = False
    '
    'radDarker
    '
    Me.radDarker.AutoSize = True
    Me.radDarker.Checked = True
    Me.radDarker.Location = New System.Drawing.Point(873, 13)
    Me.radDarker.Name = "radDarker"
    Me.radDarker.Size = New System.Drawing.Size(57, 17)
    Me.radDarker.TabIndex = 12
    Me.radDarker.TabStop = True
    Me.radDarker.Text = "Darker"
    Me.radDarker.UseVisualStyleBackColor = True
    '
    'radLighter
    '
    Me.radLighter.AutoSize = True
    Me.radLighter.Location = New System.Drawing.Point(936, 13)
    Me.radLighter.Name = "radLighter"
    Me.radLighter.Size = New System.Drawing.Size(57, 17)
    Me.radLighter.TabIndex = 13
    Me.radLighter.Text = "Lighter"
    Me.radLighter.UseVisualStyleBackColor = True
    '
    'radNearest
    '
    Me.radNearest.AutoSize = True
    Me.radNearest.Location = New System.Drawing.Point(999, 13)
    Me.radNearest.Name = "radNearest"
    Me.radNearest.Size = New System.Drawing.Size(62, 17)
    Me.radNearest.TabIndex = 14
    Me.radNearest.Text = "Nearest"
    Me.radNearest.UseVisualStyleBackColor = True
    '
    'TabControl1
    '
    Me.TabControl1.Controls.Add(Me.t1)
    Me.TabControl1.Controls.Add(Me.t2)
    Me.TabControl1.Location = New System.Drawing.Point(12, 38)
    Me.TabControl1.Name = "TabControl1"
    Me.TabControl1.SelectedIndex = 0
    Me.TabControl1.Size = New System.Drawing.Size(239, 633)
    Me.TabControl1.TabIndex = 15
    '
    't1
    '
    Me.t1.Controls.Add(Me.listAll)
    Me.t1.Controls.Add(Me.picOrig)
    Me.t1.Location = New System.Drawing.Point(4, 22)
    Me.t1.Name = "t1"
    Me.t1.Padding = New System.Windows.Forms.Padding(3)
    Me.t1.Size = New System.Drawing.Size(231, 607)
    Me.t1.TabIndex = 0
    Me.t1.Text = "Original"
    Me.t1.UseVisualStyleBackColor = True
    '
    'listAll
    '
    Me.listAll.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cc1, Me.cc2, Me.cc3})
    Me.listAll.FullRowSelect = True
    Me.listAll.GridLines = True
    Me.listAll.Location = New System.Drawing.Point(6, 152)
    Me.listAll.Name = "listAll"
    Me.listAll.Size = New System.Drawing.Size(219, 428)
    Me.listAll.SmallImageList = Me.ImageList1
    Me.listAll.Sorting = System.Windows.Forms.SortOrder.Ascending
    Me.listAll.TabIndex = 4
    Me.listAll.UseCompatibleStateImageBehavior = False
    Me.listAll.View = System.Windows.Forms.View.Details
    '
    'cc1
    '
    Me.cc1.Text = "Sum"
    '
    'cc2
    '
    Me.cc2.Text = "Color"
    '
    'cc3
    '
    Me.cc3.Text = "ID"
    '
    't2
    '
    Me.t2.Controls.Add(Me.txtLine)
    Me.t2.Controls.Add(Me.btnLineMore)
    Me.t2.Controls.Add(Me.btnLineLess)
    Me.t2.Controls.Add(Me.radLineAll)
    Me.t2.Controls.Add(Me.radLineCols)
    Me.t2.Controls.Add(Me.radLineRows)
    Me.t2.Controls.Add(Me.listTiles)
    Me.t2.Controls.Add(Me.picGen)
    Me.t2.Controls.Add(Me.picTile)
    Me.t2.Location = New System.Drawing.Point(4, 22)
    Me.t2.Name = "t2"
    Me.t2.Padding = New System.Windows.Forms.Padding(3)
    Me.t2.Size = New System.Drawing.Size(231, 607)
    Me.t2.TabIndex = 1
    Me.t2.Text = "Generated"
    Me.t2.UseVisualStyleBackColor = True
    '
    'txtLine
    '
    Me.txtLine.Location = New System.Drawing.Point(131, 149)
    Me.txtLine.Name = "txtLine"
    Me.txtLine.Size = New System.Drawing.Size(29, 20)
    Me.txtLine.TabIndex = 17
    Me.txtLine.Text = "64"
    Me.txtLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'btnLineMore
    '
    Me.btnLineMore.Location = New System.Drawing.Point(166, 147)
    Me.btnLineMore.Name = "btnLineMore"
    Me.btnLineMore.Size = New System.Drawing.Size(23, 23)
    Me.btnLineMore.TabIndex = 16
    Me.btnLineMore.Text = ">"
    Me.btnLineMore.UseVisualStyleBackColor = True
    '
    'btnLineLess
    '
    Me.btnLineLess.Location = New System.Drawing.Point(101, 147)
    Me.btnLineLess.Name = "btnLineLess"
    Me.btnLineLess.Size = New System.Drawing.Size(23, 23)
    Me.btnLineLess.TabIndex = 15
    Me.btnLineLess.Text = "<"
    Me.btnLineLess.UseVisualStyleBackColor = True
    '
    'radLineAll
    '
    Me.radLineAll.AutoSize = True
    Me.radLineAll.Location = New System.Drawing.Point(172, 17)
    Me.radLineAll.Name = "radLineAll"
    Me.radLineAll.Size = New System.Drawing.Size(36, 17)
    Me.radLineAll.TabIndex = 14
    Me.radLineAll.TabStop = True
    Me.radLineAll.Text = "All"
    Me.radLineAll.UseVisualStyleBackColor = True
    '
    'radLineCols
    '
    Me.radLineCols.AutoSize = True
    Me.radLineCols.Location = New System.Drawing.Point(172, 63)
    Me.radLineCols.Name = "radLineCols"
    Me.radLineCols.Size = New System.Drawing.Size(45, 17)
    Me.radLineCols.TabIndex = 13
    Me.radLineCols.TabStop = True
    Me.radLineCols.Text = "Cols"
    Me.radLineCols.UseVisualStyleBackColor = True
    '
    'radLineRows
    '
    Me.radLineRows.AutoSize = True
    Me.radLineRows.Location = New System.Drawing.Point(172, 40)
    Me.radLineRows.Name = "radLineRows"
    Me.radLineRows.Size = New System.Drawing.Size(52, 17)
    Me.radLineRows.TabIndex = 12
    Me.radLineRows.TabStop = True
    Me.radLineRows.Text = "Rows"
    Me.radLineRows.UseVisualStyleBackColor = True
    '
    'listTiles
    '
    Me.listTiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c1, Me.c2, Me.c3, Me.c4, Me.c5})
    Me.listTiles.FullRowSelect = True
    Me.listTiles.GridLines = True
    Me.listTiles.HideSelection = False
    Me.listTiles.Location = New System.Drawing.Point(6, 183)
    Me.listTiles.Name = "listTiles"
    Me.listTiles.Size = New System.Drawing.Size(219, 368)
    Me.listTiles.SmallImageList = Me.ImageList1
    Me.listTiles.TabIndex = 7
    Me.listTiles.UseCompatibleStateImageBehavior = False
    Me.listTiles.View = System.Windows.Forms.View.Details
    '
    'c1
    '
    Me.c1.Text = "#"
    Me.c1.Width = 39
    '
    'c2
    '
    Me.c2.Text = "Panel"
    Me.c2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    Me.c2.Width = 52
    '
    'c3
    '
    Me.c3.Text = "Row"
    Me.c3.Width = 42
    '
    'c4
    '
    Me.c4.Text = "Across"
    '
    'c5
    '
    Me.c5.Text = "Color"
    '
    'picGen
    '
    Me.picGen.Location = New System.Drawing.Point(32, 6)
    Me.picGen.Name = "picGen"
    Me.picGen.Size = New System.Drawing.Size(128, 128)
    Me.picGen.TabIndex = 4
    Me.picGen.TabStop = False
    '
    'chkFull
    '
    Me.chkFull.AutoSize = True
    Me.chkFull.Location = New System.Drawing.Point(1067, 14)
    Me.chkFull.Name = "chkFull"
    Me.chkFull.Size = New System.Drawing.Size(42, 17)
    Me.chkFull.TabIndex = 16
    Me.chkFull.Text = "Full"
    Me.chkFull.UseVisualStyleBackColor = True
    '
    'MainWnd
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1183, 700)
    Me.Controls.Add(Me.chkFull)
    Me.Controls.Add(Me.TabControl1)
    Me.Controls.Add(Me.radNearest)
    Me.Controls.Add(Me.radLighter)
    Me.Controls.Add(Me.radDarker)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.btnBrowse)
    Me.Controls.Add(Me.btnProcess)
    Me.Controls.Add(Me.txtFile)
    Me.Name = "MainWnd"
    Me.Text = "Roof Top"
    CType(Me.picOrig, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    CType(Me.picWork, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.picTile, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabControl1.ResumeLayout(False)
    Me.t1.ResumeLayout(False)
    Me.t2.ResumeLayout(False)
    Me.t2.PerformLayout()
    CType(Me.picGen, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents txtFile As TextBox
  Friend WithEvents btnProcess As Button
  Friend WithEvents btnBrowse As Button
  Friend WithEvents picOrig As PictureBox
  Friend WithEvents Panel1 As Panel
  Friend WithEvents picWork As PictureBox
  Friend WithEvents ToolTip1 As ToolTip
  Friend WithEvents ImageList1 As ImageList
  Friend WithEvents picTile As PictureBox
  Friend WithEvents radDarker As RadioButton
  Friend WithEvents radLighter As RadioButton
  Friend WithEvents radNearest As RadioButton
  Friend WithEvents TabControl1 As TabControl
  Friend WithEvents t1 As TabPage
  Friend WithEvents t2 As TabPage
  Friend WithEvents picGen As PictureBox
  Friend WithEvents listAll As ListView
  Friend WithEvents cc1 As ColumnHeader
  Friend WithEvents listTiles As ListView
  Friend WithEvents c1 As ColumnHeader
  Friend WithEvents c2 As ColumnHeader
  Friend WithEvents c3 As ColumnHeader
  Friend WithEvents c4 As ColumnHeader
  Friend WithEvents c5 As ColumnHeader
  Friend WithEvents cc2 As ColumnHeader
  Friend WithEvents cc3 As ColumnHeader
  Friend WithEvents chkFull As CheckBox
  Friend WithEvents txtLine As TextBox
  Friend WithEvents btnLineMore As Button
  Friend WithEvents btnLineLess As Button
  Friend WithEvents radLineAll As RadioButton
  Friend WithEvents radLineCols As RadioButton
  Friend WithEvents radLineRows As RadioButton
End Class
