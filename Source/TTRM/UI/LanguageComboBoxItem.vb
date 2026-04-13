#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Globalization
Imports System.Threading

#End Region

''' <summary>
''' Represents an item in a language selection <see cref="ComboBox"/>.
''' </summary>
''' 
''' <remarks>
''' Stores a <see cref="CultureInfo"/> object, a display name for the culture,
''' and indicates whether the culture matches the current UI culture.
''' </remarks>
Public NotInheritable Class LanguageComboBoxItem

#Region " Properties "

    ''' <summary>
    ''' Gets the <see cref="CultureInfo"/> associated with this item.
    ''' </summary>
    Public ReadOnly Property Culture As CultureInfo

    ''' <summary>
    ''' Gets the display name of the culture, formatted in title case.
    ''' </summary>
    Public ReadOnly Property DisplayName As String

    ''' <summary>
    ''' Gets a value indicating whether this item's culture matches the current thread's UI culture.
    ''' </summary>
    Public ReadOnly Property IsCurrentUICulture As Boolean
        <DebuggerStepThrough>
        Get
            Dim currentCulture As CultureInfo = Thread.CurrentThread.CurrentUICulture
            Return Me.Culture.TwoLetterISOLanguageName.Equals(currentCulture.TwoLetterISOLanguageName, StringComparison.InvariantCultureIgnoreCase)
        End Get
    End Property

#End Region

#Region " Constructors "

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LanguageComboBoxItem"/> class with the specified culture.
    ''' </summary>
    ''' 
    ''' <param name="culture">
    ''' The <see cref="CultureInfo"/> to associate with this item.
    ''' </param>
    Public Sub New(culture As CultureInfo)

        Me.Culture = culture
        Me.DisplayName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(culture.DisplayName)
    End Sub

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Returns a <see cref="String" /> that represents this instance.
    ''' </summary>
    ''' 
    ''' <returns>
    ''' A <see cref="String" /> that represents this instance.
    ''' </returns>
    Public Overrides Function ToString() As String

        Return Me.DisplayName
    End Function

#End Region

End Class
