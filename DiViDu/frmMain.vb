
Imports System.IO
Imports CsvHelper
Imports DiViDu.My

Public Class FrmMain
    ''' <summary>
    ''' Timer to measure the waiting time between tapping key press events.
    ''' </summary>
    Private ReadOnly _ticTimer As New Stopwatch

    ''' <summary>
    ''' Time to measure the response time.
    ''' </summary>
    Private ReadOnly _responseTimer As New Stopwatch

    ''' <summary>
    ''' A list of waiting times in ms between successive tapping events.
    ''' For the key values refer to <seealso cref="_letters"/>.
    ''' </summary>
    Private ReadOnly _tickTimes As New List(Of Long)

    ''' <summary>
    ''' Record of the used tapping keys. For the waiting times between two key strokes refer to <seealso cref="_tickTimes"/>.
    ''' </summary>
    ''' <remarks>Note that the list is not cleared after every item but after a change in the word category.</remarks>
    Private ReadOnly _letters As New List(Of String)

    ''' <summary>
    ''' The evaluation of <see cref="_currentItem"/>.
    ''' </summary>
    Private _evaluation As App.Evaluations

    ''' <summary>
    ''' Current state.
    ''' </summary>
    Private _state As App.States = App.States.Start

    ''' <summary>
    ''' Determines if the task, i.e., tapping (on/off) from the current item is different from the task of the previous item.
    ''' </summary>
    Private _taskChanged As Boolean = True

    ''' <summary>
    ''' Number of items since the last break.
    ''' </summary>
    Private _breakCounter As Integer = 0

    ''' <summary>
    ''' Time used in ms for the current item or tap test task.
    ''' </summary>
    Private _timeUsed As Long

    ''' <summary>
    ''' Enumerator for the word list.
    ''' </summary>
    Private _wordListEnumerator As IEnumerator(Of App.WordListEntry)

    ''' <summary>
    ''' Determines if a new item was read from the list.
    ''' </summary>
    Private _isRead = False

    ''' <summary>
    ''' Current item from the <seealso cref="_wordListEnumerator"/>
    ''' </summary>
    Private _currentItem As App.WordListEntry = New App.WordListEntry("", True)

    ''' <summary>
    ''' Link to the csv log file.
    ''' </summary>
    Private _csvWriter As CsvWriter

    ''' <summary>
    ''' Evolves to the next state, which might be interstiumulus or break.
    ''' </summary>
    Private Sub Break()
        If _breakCounter = Settings.breakDistance Then
            _breakCounter = 0 'Rests the break counter
            Read()
            If _state <> App.States.Finish Then
                SetState(App.States.Break)
            End If
        Else
            SetState(App.States.Interstimulus)
        End If
    End Sub

    ''' <summary>
    ''' Handles the keyboard events.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub KeyDownHandler(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If Settings.KeysTapping.Contains(e.KeyCode) Then
            _tickTimes.Add(_ticTimer.ElapsedMilliseconds())
            _letters.Add(e.KeyCode.ToString())
            tickAvg.Text = _tickTimes.Average 'The average tapping time control is currently hidden to the user
            _ticTimer.Restart()
        ElseIf e.KeyCode = Settings.KeyPseudoWort Or e.KeyCode = Settings.KeyWort Then _
            ' Assume that answer keys are disjunct from tapping keys.
            If _state = App.States.Response Then 'The state that's the response is supposed to be give
                _timeUsed = _responseTimer.ElapsedMilliseconds
                _responseTimer.Stop() 'stops of the reaction time stopwatch
                _evaluation = JudgeResponse(e)
                SetState(App.States.Interstimulus)
                Break()
            End If
        ElseIf e.KeyCode = Keys.Escape Then 'Handling of the ESC key
            Select Case _state
                Case App.States.Start
                    SetState(App.States.Tapping) 'Starts the experiment
                    ' In VB no Exit Case is required
                Case App.States.Break
                    _taskChanged = True
                    Read()
                    SetState(App.States.Tapping)
                Case App.States.Finish
                    End
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Helper function to jude a response. Called from <seealso cref="KeyDownHandler"/>.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    Private Function JudgeResponse(e As KeyEventArgs) As App.Evaluations

        If e.KeyCode = Settings.KeyPseudoWort Then
            If _currentItem.Pseudoword Then
                Return App.Evaluations.Correct
            Else
                Return App.Evaluations.Incorrect
            End If
        Else 'e.KeyCode = Settings.KeyWort
            If Not _currentItem.Pseudoword Then
                Return App.Evaluations.Correct
            Else
                Return App.Evaluations.Incorrect
            End If
        End If
    End Function

    ''' <summary>
    ''' Creates a log file with header at the given location.
    ''' </summary>
    ''' <param name="fileName"></param>
    Public Sub SetLogFile(fileName As String)
        Dim writer = New StreamWriter(fileName)
        _csvWriter = New CsvWriter(writer)
        _csvWriter.Configuration.Delimiter = MySettings.CsvDelimiter
        WriteLogHeader()
    End Sub

    Private Sub LoadHandler(sender As Object, e As EventArgs) Handles Me.Load

        If (Not FrmSettings.ShowDialog() = DialogResult.OK) Then
            End ' Exit the program, if settings wer canceled
        End If
        If (IsNothing(_csvWriter)) Then 'Make sure there is a log file
            SetLogFile(Settings.logFileName)
        End If
        If (IsNothing(_wordListEnumerator)) Then 'Make sure there is a file list
            SetWordList(Settings.wordListFilename)
        End If
        'Now start the app
        _ticTimer.Start()
        tmWords.Enabled = True
        SetState(App.States.Start)
    End Sub

    ''' <summary>
    ''' Opens a file list, and reads the first item.
    ''' </summary>
    ''' <param name="wordListFilename"></param>
    Public Sub SetWordList(wordListFilename As String)
        Dim streamReader = New StreamReader(wordListFilename)
        Dim csvReader = New CsvReader(streamReader)
        csvReader.Configuration.Delimiter = MySettings.CsvDelimiter
        _wordListEnumerator = csvReader.GetRecords(Of App.WordListEntry).GetEnumerator()
        Read()
    End Sub

    ''' <summary>
    ''' Helper to write the header of the log file. Called from <seealso cref="SetLogFile"/>
    ''' </summary>
    Private Sub WriteLogHeader()
        For Each header In [Enum].GetValues(GetType(App.LogColumns))
            _csvWriter.WriteField([Enum].GetName(GetType(App.LogColumns), header))
        Next
        _csvWriter.NextRecord()
    End Sub

    ''' <summary>
    ''' Write the log file.
    ''' </summary>
    ''' <param name="comment">Value independent from the item. Used for example to indicate a tap test.</param>
    Private Sub WriteLog(Optional comment As String = "")
        If Not _currentItem.Stimulus = "" Then
            Dim tickAverage
            If _tickTimes.Count Then
                tickAverage = _tickTimes.Average
            Else
                tickAverage = 0
            End If
            'col 1
            _csvWriter.WriteField(DateTime.Now)
            _csvWriter.WriteField(comment)
            _csvWriter.WriteField(_currentItem.Stimulus)
            _csvWriter.WriteField(_currentItem.VisualField)
            _csvWriter.WriteField(_currentItem.Pseudoword)
            _csvWriter.WriteField(_currentItem.Tapping)
            _csvWriter.WriteField(_currentItem.WordCategory)
            _csvWriter.WriteField(_currentItem.SubCategory)
            _csvWriter.WriteField(_evaluation.ToString)
            _csvWriter.WriteField(_timeUsed)
            _csvWriter.WriteField(tickAverage)

            _csvWriter.WriteField(String.Join(",", _letters))
            _csvWriter.WriteField(String.Join(",", _tickTimes))
            'finish the record
            _csvWriter.NextRecord()
            _letters.Clear()
            _tickTimes.Clear()
        End If
    End Sub

    ''' <summary>
    ''' Reads the next item from the word list.
    ''' </summary>
    Private Sub Read()
        If _isRead Then 'Prevents that words are skipped, because the function was called multiple times
            _taskChanged = False
            Exit Sub
        End If
        _isRead = True
        WriteLog()
        Dim endReached = Not _wordListEnumerator.MoveNext
        If endReached Then
            SetState(App.States.Finish)
            tmWords.Enabled = False
        Else
            Dim newItem = _wordListEnumerator.Current
            'If word group was changed
            If _currentItem.WordCategory <> newItem.WordCategory Then
                ResetList()
            End If
            ' if task changed
            If newItem.Tapping <> _currentItem.Tapping Then
                _taskChanged = True
            End If
            _currentItem = newItem
            'If tapping started or stopped
            If _taskChanged Then
                SetState(App.States.Tapping)
                _taskChanged = False
            Else
                SetState(App.States.Fixation)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Sets the next state of the program.
    ''' </summary>
    ''' <param name="newState"></param>
    Private Sub SetState(newState As App.States)
        Select Case newState
            Case App.States.Start
                BackColor = Color.White
                lblWortLinks.Text = ""
                lblWortRechts.Text = ""
                StartEnde.Visible = True
                StartEnde.Text = Settings.Labels.Item(App.States.Start)
                StartEnde.BackColor = Color.White
                tmWords.Enabled = False
                tmWords.Interval = Settings.Durations.Item(App.States.Start)
            Case App.States.Tapping
                lblPlus.Visible = False
                StartEnde.Visible = True
                If _currentItem.Tapping Then
                    lblWortLinks.Text = ""
                    lblWortRechts.Text = ""
                    StartEnde.Text = Settings.Labels.Item(App.States.Tapping)
                    StartEnde.BackColor = MySettings.ColorTapping
                Else
                    lblWortLinks.Text = ""
                    lblWortRechts.Text = ""
                    StartEnde.Text = Settings.Labels.Item(App.States.TappingOff)
                    StartEnde.BackColor = MySettings.ColorNoTapping
                End If
                ResetList()
                tmWords.Interval = Settings.Durations.Item(App.States.Tapping)
            Case App.States.Fixation
                lblPlus.Visible = True
                StartEnde.Visible = False
                lblWortLinks.Text = ""
                lblWortRechts.Text = ""
                tmWords.Interval = Settings.Durations.Item(App.States.Fixation)
            Case App.States.Interstimulus
                lblPlus.Visible = False
                StartEnde.Visible = False
                lblWortLinks.Text = ""
                lblWortRechts.Text = ""
                tmWords.Interval = Settings.Durations.Item(App.States.Interstimulus)
            Case App.States.Stimulus
                lblPlus.Visible = False
                If _currentItem.VisualField = "RVF" Then
                    lblWortRechts.Text = _currentItem.Stimulus
                    ColorField(lblWortRechts)
                    lblWortLinks.Text = ""
                Else
                    lblWortLinks.Text = _currentItem.Stimulus
                    ColorField(lblWortLinks)
                    lblWortRechts.Text = ""
                End If
                _isRead = False
                tmWords.Interval = Settings.Durations.Item(App.States.Stimulus)
                _responseTimer.Restart()
                _breakCounter = _breakCounter + 1
            Case App.States.Response
                lblPlus.Visible = False
                lblWortLinks.Text = ""
                lblWortLinks.BackColor = Color.Transparent
                lblWortRechts.Text = ""
                lblWortRechts.BackColor = Color.Transparent
                tmWords.Interval = Settings.Durations.Item(App.States.Response)
            Case App.States.Break
                StartEnde.Visible = True
                lblPlus.Visible = False
                lblWortRechts.Text = ""
                lblWortLinks.Text = ""
                StartEnde.Text = Settings.Labels.Item(App.States.Break)
                StartEnde.BackColor = Color.White
                tmWords.Interval = Settings.Durations.Item(App.States.Break)

            Case App.States.Finish
                StartEnde.Visible = True
                lblPlus.Visible = False
                lblWortRechts.Text = ""
                lblWortLinks.Text = ""
                StartEnde.Text = Settings.Labels.Item(App.States.Finish)
                StartEnde.BackColor = Color.White
                _csvWriter.Dispose()
        End Select
        _state = newState
        tmWords.Start()
    End Sub

    ''' <summary>
    ''' Helper function to color replace color names with actual colors for trail list.
    ''' </summary>
    ''' <param name="label"></param>
    Sub ColorField(label As Label)
        If MySettings.ColorKeywords.ContainsKey(_currentItem.Stimulus) Then
            label.BackColor = MySettings.ColorKeywords(_currentItem.Stimulus)
            label.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' Helper function to reset the recorded keys
    ''' </summary>
    Private Sub ResetList()
        _tickTimes.Clear()
        _letters.Clear()
        _ticTimer.Restart()
    End Sub

    ''' <summary>
    ''' Logic for the recordings of the second task (tapping)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TmWord_Tick(sender As Object, e As EventArgs) Handles tmWords.Tick
        Select Case _state
            Case App.States.Start
                SetState(App.States.Tapping)
            Case App.States.Tapping
                SetState(App.States.Fixation)
                'Note, that the first word from the word list will appear in the tap test column of the log file
                WriteLog("TapTest")
                ResetList()
            Case App.States.Fixation
                SetState(App.States.Stimulus)
            Case App.States.Stimulus
                SetState(App.States.Response)
            Case App.States.Response
                _evaluation = App.Evaluations.TimeOver
                _timeUsed = 0
                Break()
            Case App.States.Interstimulus
                Read()
        End Select
    End Sub

    ''' <summary>
    ''' Helper function to adjust the controls to the actual physical geometry of the screen.
    ''' </summary>
    ''' <param name="control"></param>
    ''' <param name="offsetInCm"></param>
    Private Sub MoveControl(control As Label, offsetInCm As Double)
        Dim offsetInPx = offsetInCm / Settings.screenWidthInCm * Settings.screenWidhtInPx
        If offsetInPx > 0 Then
            offsetInPx = 2 * offsetInPx + control.Width
        ElseIf offsetInPx < 0 Then
            offsetInPx = 2 * offsetInPx - control.Width
        End If
        control.Left = (Width - control.Width + offsetInPx) / 2
        control.Top = (Height - control.Height) / 2
    End Sub

    ''' <summary>
    ''' Helper ensure that the controls are at the correct positions even if the window is re-sized.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmTest_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        MoveControl(lblPlus, 0)
        MoveControl(lblWortLinks, -1.1)
        MoveControl(lblWortRechts, +1.1)
    End Sub

    Private Sub StartEnde_Click(sender As Object, e As EventArgs) Handles StartEnde.Click

    End Sub
End Class

