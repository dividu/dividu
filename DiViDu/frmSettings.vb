Public Class FrmSettings
    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        If (My.Settings.logFileName.Length = 0) Then
            MsgBox("Log file name can not be empty.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Try
            fopCsv.Dispose()
            FrmMain.SetLogFile(My.Settings.logFileName)
        Catch ex As Exception
            MsgBox("Error setting log file! " + ex.Message, MsgBoxStyle.Exclamation)
            Exit Sub
        End Try


        If (My.Settings.wordListFilename.Length = 0) Then
            MsgBox("Word list file name can not be empty.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Try
            FrmMain.SetWordList(My.Settings.wordListFilename)
        Catch ex As Exception
            MsgBox("Error setting file list! " + ex.Message, MsgBoxStyle.Exclamation)
            Exit Sub
        End Try

        My.Settings.keysTappingLeft.Clear()
        For Each s In lstKeyRecLeft.Items
            My.Settings.keysTappingLeft.Add(s)
        Next
        My.Settings.keysTappingRight.Clear()
        For Each s In lstKeyRecRight.Items
            My.Settings.keysTappingRight.Add(s)
        Next
        Dim str2Key = Function(key) New KeysConverter().ConvertFromString(key)
        My.Settings.keyWordLeft = str2Key(txtKeyWordLeft.Text)
        My.Settings.keyPseudoWordLeft = str2Key(txtKeyPseudoWordLeft.Text)
        My.Settings.keyWordRight = str2Key(txtKeyWordRight.Text)
        My.Settings.keyPseudoWordRight = str2Key(txtKeyPseudoWordRight.Text)
        My.Settings.Save()
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub


    Private Shared Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) _
        Handles txtKeyWordLeft.KeyDown, txtKeyWordRight.KeyDown, txtKeyPseudoWordRight.KeyDown,
                txtKeyPseudoWordLeft.KeyDown
        sender.Text = e.KeyCode.ToString
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub


    Private Sub FrmSettings_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtKeyWordLeft.Text = My.Settings.keyWordLeft.ToString
        txtKeyPseudoWordLeft.Text = My.Settings.keyPseudoWordLeft.ToString
        txtKeyWordRight.Text = My.Settings.keyWordRight.ToString
        txtKeyPseudoWordRight.Text = My.Settings.keyPseudoWordRight.ToString
        For Each s In My.Settings.keysTappingLeft
            lstKeyRecLeft.Items.Add(s)
        Next
        For Each s In My.Settings.keysTappingRight
            lstKeyRecRight.Items.Add(s)
        Next
        If (Not My.Settings.TappingLeft) Then
            RadioButton2.Checked = True
        End If
        EnableCurrentSide()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        EnableCurrentSide()
    End Sub

    Private Sub EnableCurrentSide()
        If (RadioButton1.Checked) Then
            txtKeyPseudoWordLeft.Enabled = True
            txtKeyWordLeft.Enabled = True
            txtKeyPseudoWordRight.Enabled = False
            txtKeyWordRight.Enabled = False
            lstKeyRecLeft.Enabled = True
            lstKeyRecRight.Enabled = False
        Else
            txtKeyPseudoWordLeft.Enabled = False
            txtKeyWordLeft.Enabled = False
            txtKeyPseudoWordRight.Enabled = True
            txtKeyWordRight.Enabled = True
            lstKeyRecLeft.Enabled = False
            lstKeyRecRight.Enabled = True
        End If
    End Sub

    Private Sub ListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles lstKeyRecLeft.KeyDown
        ToggleListElement(lstKeyRecLeft, e.KeyCode.ToString)
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub

    Private Shared Sub ToggleListElement(list As ListBox, element As String)
        If list.Items.Contains(element) Then
            list.Items.Remove(element)
        Else
            list.Items.Add(element)
        End If
    End Sub

    Private Sub LstKeyRecRight_KeyDown(sender As Object, e As KeyEventArgs) Handles lstKeyRecRight.KeyDown
        ToggleListElement(lstKeyRecRight, e.KeyCode.ToString)
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub

    Private Sub LstKeyRecRight_DoubleClick(sender As Object, e As EventArgs) Handles lstKeyRecRight.DoubleClick
        lstKeyRecRight.Items.Remove(lstKeyRecRight.SelectedItem)
    End Sub

    Private Sub LstKeyRecLeft_DoubleClick(sender As Object, e As EventArgs) Handles lstKeyRecLeft.DoubleClick
        lstKeyRecLeft.Items.Remove(lstKeyRecLeft.SelectedItem)
    End Sub


    Private Sub WordList_DoubleClick(sender As Object, e As EventArgs) Handles wordList.DoubleClick
        fopCsv.ShowDialog()
        wordList.Text = fopCsv.FileName
    End Sub


    Private Sub LogFileDoubleClick(sender As Object, e As EventArgs) Handles txtLogFile.DoubleClick
#Disable Warning StringLiteralTypo
        fsvCsv.FileName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".csv"
#Enable Warning StringLiteralTypo
        fsvCsv.ShowDialog()
        txtLogFile.Text = fsvCsv.FileName
    End Sub
End Class
