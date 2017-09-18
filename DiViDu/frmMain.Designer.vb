<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            _csvWriter.Dispose()

            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Catch
            ' Ignore errors
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
        Me.lblWortLinks = New System.Windows.Forms.Label()
        Me.Tick1 = New System.Windows.Forms.Label()
        Me.Tick2 = New System.Windows.Forms.Label()
        Me.Tick3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tickAvg = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.fopExcelFile = New System.Windows.Forms.OpenFileDialog()
        Me.lblPlus = New System.Windows.Forms.Label()
        Me.tmWords = New System.Windows.Forms.Timer(Me.components)
        Me.lblAnswer = New System.Windows.Forms.Label()
        Me.lblWortRechts = New System.Windows.Forms.Label()
        Me.StartEnde = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'lblWortLinks
        '
        Me.lblWortLinks.Font = New System.Drawing.Font("Times New Roman", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWortLinks.Location = New System.Drawing.Point(379, 403)
        Me.lblWortLinks.Margin = New System.Windows.Forms.Padding(0)
        Me.lblWortLinks.Name = "lblWortLinks"
        Me.lblWortLinks.Size = New System.Drawing.Size(350, 60)
        Me.lblWortLinks.TabIndex = 0
        Me.lblWortLinks.Text = "XXXXXX"
        Me.lblWortLinks.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Tick1
        '
        Me.Tick1.AutoSize = True
        Me.Tick1.Location = New System.Drawing.Point(497, 546)
        Me.Tick1.Name = "Tick1"
        Me.Tick1.Size = New System.Drawing.Size(39, 13)
        Me.Tick1.TabIndex = 0
        Me.Tick1.Text = "Label1"
        Me.Tick1.Visible = False
        '
        'Tick2
        '
        Me.Tick2.AutoSize = True
        Me.Tick2.Location = New System.Drawing.Point(554, 546)
        Me.Tick2.Name = "Tick2"
        Me.Tick2.Size = New System.Drawing.Size(39, 13)
        Me.Tick2.TabIndex = 0
        Me.Tick2.Text = "Label1"
        Me.Tick2.Visible = False
        '
        'Tick3
        '
        Me.Tick3.AutoSize = True
        Me.Tick3.Location = New System.Drawing.Point(611, 546)
        Me.Tick3.Name = "Tick3"
        Me.Tick3.Size = New System.Drawing.Size(39, 13)
        Me.Tick3.TabIndex = 0
        Me.Tick3.Text = "Label1"
        Me.Tick3.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(665, 546)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "AVG"
        Me.Label6.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(878, 546)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Label1"
        Me.Label7.Visible = False
        '
        'tickAvg
        '
        Me.tickAvg.AutoSize = True
        Me.tickAvg.Location = New System.Drawing.Point(731, 546)
        Me.tickAvg.Name = "tickAvg"
        Me.tickAvg.Size = New System.Drawing.Size(39, 13)
        Me.tickAvg.TabIndex = 0
        Me.tickAvg.Text = "Label1"
        Me.tickAvg.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(798, 546)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Label1"
        Me.Label9.Visible = False
        '
        'fopExcelFile
        '
        Me.fopExcelFile.DefaultExt = "xls"
        Me.fopExcelFile.FileName = "Nina"
        Me.fopExcelFile.Title = "Select Excel source file"
        '
        'lblPlus
        '
        Me.lblPlus.Font = New System.Drawing.Font("Times New Roman", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlus.Location = New System.Drawing.Point(770, 403)
        Me.lblPlus.Name = "lblPlus"
        Me.lblPlus.Size = New System.Drawing.Size(60, 60)
        Me.lblPlus.TabIndex = 1
        Me.lblPlus.Text = "+"
        Me.lblPlus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tmWords
        '
        Me.tmWords.Interval = 3000
        '
        'lblAnswer
        '
        Me.lblAnswer.AutoSize = True
        Me.lblAnswer.Location = New System.Drawing.Point(611, 163)
        Me.lblAnswer.Name = "lblAnswer"
        Me.lblAnswer.Size = New System.Drawing.Size(124, 13)
        Me.lblAnswer.TabIndex = 2
        Me.lblAnswer.Text = "Bitte response eingeben."
        Me.lblAnswer.Visible = False
        '
        'lblWortRechts
        '
        Me.lblWortRechts.Font = New System.Drawing.Font("Times New Roman", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWortRechts.Location = New System.Drawing.Point(871, 403)
        Me.lblWortRechts.Margin = New System.Windows.Forms.Padding(0)
        Me.lblWortRechts.Name = "lblWortRechts"
        Me.lblWortRechts.Size = New System.Drawing.Size(350, 60)
        Me.lblWortRechts.TabIndex = 0
        Me.lblWortRechts.Text = "XXXXX"
        Me.lblWortRechts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StartEnde
        '
        Me.StartEnde.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StartEnde.Font = New System.Drawing.Font("Times New Roman", 50.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StartEnde.Location = New System.Drawing.Point(0, 0)
        Me.StartEnde.Name = "StartEnde"
        Me.StartEnde.Size = New System.Drawing.Size(1280, 912)
        Me.StartEnde.TabIndex = 3
        Me.StartEnde.Text = "Label1"
        Me.StartEnde.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.StartEnde.Visible = False
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1280, 912)
        Me.ControlBox = False
        Me.Controls.Add(Me.StartEnde)
        Me.Controls.Add(Me.lblAnswer)
        Me.Controls.Add(Me.lblPlus)
        Me.Controls.Add(Me.lblWortRechts)
        Me.Controls.Add(Me.lblWortLinks)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.tickAvg)
        Me.Controls.Add(Me.Tick3)
        Me.Controls.Add(Me.Tick2)
        Me.Controls.Add(Me.Tick1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmMain"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents lblWortLinks As System.Windows.Forms.Label
    Friend WithEvents Tick1 As System.Windows.Forms.Label
    Friend WithEvents Tick2 As System.Windows.Forms.Label
    Friend WithEvents Tick3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tickAvg As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents fopExcelFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblPlus As System.Windows.Forms.Label
    Friend WithEvents tmWords As System.Windows.Forms.Timer
    Friend WithEvents lblAnswer As System.Windows.Forms.Label
    Friend WithEvents lblWortRechts As System.Windows.Forms.Label
    Friend WithEvents StartEnde As System.Windows.Forms.Label

End Class
