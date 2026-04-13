#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

''' <summary>
''' Represents the configuration of a plugin loaded from a JSON file.
''' </summary>
Public NotInheritable Class JsonPluginConfig

#Region " Properties "

    ''' <summary>
    ''' Gets or sets the user-friendly name associated to this plugin.
    ''' </summary>
    Public Property Name As String

    ''' <summary>
    ''' Gets or sets the description associated to this plugin.
    ''' </summary>
    Public Property Description As String

    ''' <summary>
    ''' Gets or sets the login URL associated to this plugin.
    ''' </summary>
    Public Property UrlLogin As String

    ''' <summary>
    ''' Gets or sets the registration URL associated to this plugin.
    ''' </summary>
    Public Property UrlRegistration As String

    ''' <summary>
    ''' Gets or sets the application URL associated to this plugin.
    ''' </summary>
    Public Property UrlApplication As String

    ''' <summary>
    ''' Gets or sets the path to the icon or banner image file representing this plugin.
    ''' </summary>
    Public Property IconPath As String

    ''' <summary>
    ''' Gets or sets the path to the VisualBasic.NET code file associated to this plugin.
    ''' </summary>
    Public Property VbCodeFile As String

#End Region

#Region " Constructors "

    ''' <summary>
    ''' Initializes a new instance of the <see cref="JsonPluginConfig"/> class.
    ''' </summary>
    Public Sub New()
    End Sub

#End Region

End Class