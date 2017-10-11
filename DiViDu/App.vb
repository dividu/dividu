Friend Class App
    Public Enum States
        Start
        Tapping
        Fixation
        Stimulus
        Response
        Interstimulus
        Break
        Finish
        TappingOff
    End Enum

    Public Enum Evaluations
        Correct
        Incorrect
        TimeOver
    End Enum

    Public Enum LogColumns
        CurrentDate
        Comment
        Stimulus
        VisualField
        PseudoWord
        Tapping
        WordCategory
        SubCategory
        Response
        ReactionTime
        IntertapIntervalAverage
        TappingKeys
        IntertapInterval
    End Enum

    Public Class WordListEntry
        Public Sub New()
        End Sub

        Public Sub New(initialStimulus As String, initialTapping As Boolean)
            Stimulus = initialStimulus
            Tapping = initialTapping
        End Sub


        Public Property Stimulus As String

        Public Property VisualField As String

        Public Property Pseudoword As Boolean

        Public Property Tapping As Boolean

        Public Property WordCategory As String

        Public Property SubCategory As String
    End Class
End Class