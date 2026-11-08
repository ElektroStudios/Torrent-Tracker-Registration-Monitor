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
''' Tests for the <see cref="LanguageComboBoxItem"/> class.
''' </summary>
<TestClass>
Public NotInheritable Class LanguageComboBoxItemTests

#Region " Fields "

    ''' <summary>
    ''' The UI culture of the current thread before running each test.
    ''' </summary>
    Private previousUICulture As CultureInfo

    ''' <summary>
    ''' The culture of the current thread before running each test.
    ''' </summary>
    Private previousCulture As CultureInfo

#End Region

#Region " Test Initialization / Cleanup "

    ''' <summary>
    ''' Saves the current thread cultures before running each test.
    ''' </summary>
    <TestInitialize>
    Public Sub TestInitialize()

        Me.previousUICulture = Thread.CurrentThread.CurrentUICulture
        Me.previousCulture = Thread.CurrentThread.CurrentCulture
    End Sub

    ''' <summary>
    ''' Restores the current thread cultures after running each test.
    ''' </summary>
    <TestCleanup>
    Public Sub TestCleanup()

        Thread.CurrentThread.CurrentUICulture = Me.previousUICulture
        Thread.CurrentThread.CurrentCulture = Me.previousCulture
    End Sub

#End Region

#Region " Tests "

    ''' <summary>
    ''' The constructor must store the specified culture.
    ''' </summary>
    <TestMethod>
    Public Sub Constructor_StoresCulture()

        Dim culture As New CultureInfo("es")
        Dim item As New LanguageComboBoxItem(culture)

        Assert.AreSame(culture, item.Culture)
    End Sub

    ''' <summary>
    ''' The display name must be the title-cased culture display name,
    ''' and <see cref="LanguageComboBoxItem.ToString"/> must return it (it is what the ComboBox shows).
    ''' </summary>
    <TestMethod>
    <DataRow("en")>
    <DataRow("es")>
    Public Sub DisplayName_IsTitleCasedCultureDisplayName(cultureName As String)

        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture

        Dim culture As New CultureInfo(cultureName)
        Dim item As New LanguageComboBoxItem(culture)

        Assert.AreEqual(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(culture.DisplayName), item.DisplayName)
        Assert.IsTrue(Char.IsUpper(item.DisplayName(0)), $"'{item.DisplayName}' does not start with an uppercase letter.")
        Assert.AreEqual(item.DisplayName, item.ToString())
    End Sub

    ''' <summary>
    ''' <see cref="LanguageComboBoxItem.IsCurrentUICulture"/> must compare only the two-letter ISO language name
    ''' with the current UI culture (e.g. "es-ES" UI culture matches the "es" item).
    ''' </summary>
    <TestMethod>
    <DataRow("es", "es-ES", True)>
    <DataRow("es", "es-MX", True)>
    <DataRow("en", "en-US", True)>
    <DataRow("en", "es-ES", False)>
    <DataRow("es", "en-GB", False)>
    Public Sub IsCurrentUICulture_ComparesLanguageOnly(itemCultureName As String, uiCultureName As String, expected As Boolean)

        Dim item As New LanguageComboBoxItem(New CultureInfo(itemCultureName))
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(uiCultureName)

        Assert.AreEqual(expected, item.IsCurrentUICulture)
    End Sub

#End Region

End Class
