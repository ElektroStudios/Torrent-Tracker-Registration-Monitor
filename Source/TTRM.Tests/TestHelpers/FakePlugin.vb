#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

''' <summary>
''' A minimal <see cref="PluginBase"/> implementation used to test the base class behavior.
''' </summary>
Friend NotInheritable Class FakePlugin : Inherits PluginBase

#Region " Constructors "

    ''' <summary>
    ''' Initializes a new instance of the <see cref="FakePlugin"/> class.
    ''' </summary>
    '''
    ''' <param name="name">
    ''' The plugin name.
    ''' </param>
    Friend Sub New(name As String)

        Me.Name = name
    End Sub

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Returns a completed task whose result is <see cref="RegistrationFlags.RegistrationOpen"/>.
    ''' </summary>
    '''
    ''' <param name="logTextBox">
    ''' Not used.
    ''' </param>
    '''
    ''' <returns>
    ''' A completed <see cref="Task(Of RegistrationFlags)"/>.
    ''' </returns>
    Public Overrides Function RunAsync(logTextBox As RichTextBox) As Task(Of RegistrationFlags)

        Return Task.FromResult(RegistrationFlags.RegistrationOpen)
    End Function

#End Region

End Class
