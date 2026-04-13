#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

''' <summary>
''' Base class for all plugins. This class must be inherited.
''' </summary>
Public MustInherit Class PluginBase : Implements IEquatable(Of PluginBase), IDisposable

#Region " Properties "

    ''' <summary>
    ''' Gets or sets the name of this plugin.
    ''' </summary>
    Public Property Name As String

    ''' <summary>
    ''' Gets or sets the description of this plugin.
    ''' </summary>
    Public Property Description As String

    ''' <summary>
    ''' Gets or sets the login page URL associated with this plugin.
    ''' </summary>
    Public Property UrlLogin As String

    ''' <summary>
    ''' Gets or sets the registration page URL associated with this plugin.
    ''' </summary>
    Public Property UrlRegistration As String

    ''' <summary>
    ''' Gets or sets the application page URL associated with this plugin.
    ''' </summary>
    Public Property UrlApplication As String

    ''' <summary>
    ''' Gets or sets the image associated with this plugin.
    ''' </summary>
    Public Property Image As Image

#End Region

#Region " Constructors "

    ''' <summary>
    ''' Initializes a new instance of the <see cref="PluginBase"/> class.
    ''' </summary>
    Public Sub New()
    End Sub

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Asynchronously executes the plugin's main logic.
    ''' </summary>
    ''' 
    ''' <param name="logTextBox">
    ''' A <see cref="TextBox"/> control where status messages can be logged during execution.
    ''' </param>
    ''' 
    ''' <returns>
    ''' A <see cref="Task(Of RegistrationFlags)"/> representing the asynchronous operation.
    ''' </returns>
    Public MustOverride Async Function RunAsync(logTextBox As TextBox) As Task(Of RegistrationFlags)

    ''' <summary>
    ''' Returns a <see cref="String" /> that represents this instance.
    ''' </summary>
    ''' 
    ''' <returns>
    ''' A <see cref="String" /> that represents this instance.
    ''' </returns>
    Public Overrides Function ToString() As String

        Return Me.Name
    End Function

    ''' <summary>
    ''' Returns a hash code for this instance.
    ''' </summary>
    ''' 
    ''' <returns>
    ''' A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    ''' </returns>
    Public Overrides Function GetHashCode() As Integer

        Return If(Me.ToString?.ToLowerInvariant().GetHashCode(), 0)
    End Function

    ''' <summary>
    ''' Determines whether the specified <see cref="Object"/> is equal to this instance.
    ''' </summary>
    ''' 
    ''' <param name="obj">
    ''' The <see cref="Object"/> to compare with this instance.
    ''' </param>
    ''' 
    ''' <returns>
    ''' <see langword="True"/> if the specified <see cref="Object"/> is equal to this instance; 
    ''' otherwise, <see langword="False"/>.
    ''' </returns>
    Public Overrides Function Equals(obj As Object) As Boolean

        Return Me.Equals(TryCast(obj, PluginBase))
    End Function

#End Region

#Region " IEquatable Implementation "

    ''' <summary>
    ''' Determines whether the specified <see cref="PluginBase"/> is equal to this instance.
    ''' </summary>
    ''' 
    ''' <param name="other">
    ''' The <see cref="PluginBase"/> to compare with this instance.
    ''' </param>
    ''' 
    ''' <returns>
    ''' <see langword="True"/> if the specified <see cref="PluginBase"/> is equal to this instance; 
    ''' otherwise, <see langword="False"/>.
    ''' </returns>
    Public Overloads Function Equals(other As PluginBase) As Boolean Implements IEquatable(Of PluginBase).Equals

        Return (other IsNot Nothing) AndAlso String.Equals(Me.ToString(), other.ToString(), StringComparison.InvariantCultureIgnoreCase)
    End Function

#End Region

#Region " IDisposable Implementation "

    ''' <summary>
    ''' Tracks whether <see cref="Pluginbase.Dispose(Boolean)"/> method has already been called to prevent redundant calls.
    ''' </summary>
    Private disposedValue As Boolean

    ''' <summary>
    ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    ''' </summary>
    ''' 
    ''' <param name="disposing">
    ''' <see langword="True"/> to release both managed and unmanaged resources; 
    ''' <see langword="False"/> to release only unmanaged resources.
    ''' </param>
    Private Sub Dispose(disposing As Boolean)

        If Not Me.disposedValue Then
            If disposing Then
                Me.Image?.Dispose()
            End If

            Me.Image = Nothing

            Me.disposedValue = True
        End If
    End Sub

    ''' <summary>
    ''' Releases the resources used by this <see cref="Pluginbase"/> instance.
    ''' </summary>
    Public Overridable Sub Dispose() Implements IDisposable.Dispose

        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Me.Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class