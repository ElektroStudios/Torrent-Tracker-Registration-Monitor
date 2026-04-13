''' <summary>
''' Specifies the possible registration states detected during plugin execution.
''' </summary>
<Flags>
Public Enum RegistrationFlags

    ''' <summary>
    ''' No flags.
    ''' </summary>
    Null = 0

    ''' <summary>
    ''' The registration form is currently open.
    ''' </summary>
    RegistrationOpen = 1 << 0

    ''' <summary>
    ''' The registration form is currently closed.
    ''' </summary>
    RegistrationClosed = 1 << 1

    ''' <summary>
    ''' The registration form state could not be determined due to an error or unexpected condition.
    ''' </summary>
    RegistrationUnknown = 1 << 2

    ''' <summary>
    ''' The application form is currently open.
    ''' </summary>
    ApplicationOpen = 1 << 3

    ''' <summary>
    ''' The application form is currently closed.
    ''' </summary>
    ApplicationClosed = 1 << 4

    ''' <summary>
    ''' The application form state could not be determined due to an error or unexpected condition.
    ''' </summary>
    ApplicationUnknown = 1 << 5

End Enum
