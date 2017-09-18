Imports System.Configuration

#Disable Warning CheckNamespace

Namespace My
#Enable Warning CheckNamespace
    'Diese Klasse ermöglicht die Behandlung bestimmter Ereignisse der Einstellungsklasse:
    ' Das SettingChanging-Ereignis wird ausgelöst, bevor der Wert einer Einstellung geändert wird.
    ' Das PropertyChanged-Ereignis wird ausgelöst, nachdem der Wert einer Einstellung geändert wurde.
    ' Das SettingsLoaded-Ereignis wird ausgelöst, nachdem die Einstellungswerte geladen wurden.
    ' Das SettingsSaving-Ereignis wird ausgelöst, bevor die Einstellungswerte gespeichert werden.
    Partial Friend NotInheritable Class MySettings
        Private ReadOnly _labels As New Dictionary(Of App.States, String) From {
            {App.States.Start, "Zum Starten des Experiments bitte START drücken"},
            {App.States.Break, "Pause - Zum Weiterführen des Experiments bitte START drücken"},
            {App.States.Tapping, "TAPPING DURCHGEHEND bis zur nächsten Pause"},
            {App.States.TappingOff, "KEIN tapping bis zur nächsten Pause"},
            {App.States.Finish, "Vielen Dank für Ihre Teilnahme"}
            }

        Private ReadOnly _durations As New Dictionary(Of App.States, Long) From {
            {App.States.Start, 1000000000},
            {App.States.Tapping, 3000},
            {App.States.Fixation, 800},
            {App.States.Interstimulus, 1200},
            {App.States.Stimulus, 150},
            {App.States.Response, 2000},
            {App.States.Break, 1000000000}
            }


        Friend ReadOnly Property KeyPseudoWort As Keys
            Get
                If TappingLeft Then
                    Return keyPseudoWordLeft
                Else
                    Return keyPseudoWordRight
                End If
            End Get
        End Property

        Friend ReadOnly Property KeyWort As Keys
            Get
                If TappingLeft Then
                    Return keyWordLeft
                Else
                    Return keyWordRight
                End If
            End Get
        End Property

        Public ReadOnly Property KeysTapping As List(Of Keys)
            Get
                Dim l As New List(Of Keys)
                Dim str2Key = Function(key) New KeysConverter().ConvertFromString(key)

                If TappingLeft Then
                    l.AddRange((From k As String In keysTappingLeft Select str2Key(k)).Cast(Of Keys)())
                Else
                    l.AddRange((From k As String In keysTappingRight Select str2Key(k)).Cast(Of Keys)())
                End If
                Return l
            End Get
        End Property

        Public ReadOnly Property Labels As Dictionary(Of App.States, String)
            Get
                Return _labels
            End Get
        End Property

        Public ReadOnly Property Durations As Dictionary(Of App.States, Long)
            Get
                Return _durations
            End Get
        End Property

        Protected Overrides Sub OnSettingChanging(sender As Object, e As SettingChangingEventArgs)
            MyBase.OnSettingChanging(sender, e)
            UpdateDictionaries(e.SettingName, e.NewValue)
        End Sub

        Private Sub UpdateDictionaries(settingName As String, value As Object)
            If (settingName.StartsWith("lbl")) Then
                _labels([Enum].Parse(GetType(App.States), settingName.Substring("lbl".Length))) = value
            End If
            If (settingName.StartsWith("duration")) Then
                _durations([Enum].Parse(GetType(App.States), settingName.Substring("duration".Length))) = value
            End If
        End Sub


        Protected Overrides Sub OnSettingsLoaded(sender As Object, e As SettingsLoadedEventArgs)
            MyBase.OnSettingsLoaded(sender, e)
            For Each p As SettingsPropertyValue In PropertyValues
                UpdateDictionaries(p.Name, p.PropertyValue)
            Next
        End Sub
        '    Public screenWidthInCm = 33.75 'Setzt Bildschirmbreit auf 33,75 cm konstant
        '    Public screenHeightInCm = 27 'Setzt Bildschirmhöhe auf 27cm konstant
        '    Public screenWidhtInPx = 1280 'Setzt Bildschirmbreite auf 1280 Pixel
        '    Public screenHeightInPx = 1024 'Setzt Bildschirmhöhe auf 1024 Pixel

        '    Public keyTappingsLeft() As Keys = {Keys.F12, Keys.F11, Keys.F10, Keys.F9}
        '    Public keyTappingsRight() As Keys = {Keys.F1, Keys.F2, Keys.F3, Keys.F4}

        '    Public KeyWortLeft = Keys.F1 'Standardeinstellung zu Beginn des Programms: tapping Links; Wort F1
        '    Public KeyPseudoWortLeft = Keys.F2 'Standardeinstellung zu Beginn des Programms: tapping Links; Pseudowort F2
        '    Public KeyWortRight = Keys.F11 'Standardeinstellung zu Beginn des Programms: tapping Links; Wort F1
        '    Public KeyPseudoWortRight = Keys.F12 'Standardeinstellung zu Beginn des Programms: tapping Links; Pseudowort F2

        '    Public breakDistance As Integer = 72 'Standardeinstellung zu Beginn des Programms: break nach 72 Items

        '    Public TappingLinks As Boolean = True 'Standardeinstellung zu Beginn des Programms: tapping Links
        Public Const CsvDelimiter As String = ";"
        Public Shared ReadOnly ColorNoTapping As Color = Color.Green
        Public Shared ReadOnly ColorTapping As Color = Color.Red

        Public Shared ReadOnly ColorKeywords As Dictionary(Of String, Color) = New Dictionary(Of String, Color) From {
            {"GELB", Color.Yellow},
            {"ROT", Color.Red}
            }
    End Class
End Namespace
