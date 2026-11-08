#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO
Imports System.Reflection

#End Region

''' <summary>
''' Tests for the <see cref="AppGlobals"/> module.
''' </summary>
<TestClass>
Public NotInheritable Class AppGlobalsTests

#Region " Properties "

    ''' <summary>
    ''' Gets the directory of the TTRM assembly.
    ''' </summary>
    Private Shared ReadOnly Property ApplicationDirectoryPath As String
        Get
            Return Path.GetDirectoryName(GetType(DynamicPlugin).Assembly.Location)
        End Get
    End Property

#End Region

#Region " Tests "

    ''' <summary>
    ''' The plugins directory must be the "plugins" folder inside the application directory.
    ''' </summary>
    <TestMethod>
    Public Sub PluginsDirectoryPath_IsInsideApplicationDirectory()

        Assert.AreEqual(Path.Combine(AppGlobalsTests.ApplicationDirectoryPath, "plugins"), AppGlobals.PluginsDirectoryPath)
    End Sub

    ''' <summary>
    ''' Every cache directory must be located inside the application directory.
    ''' <para></para>
    ''' This is required by the safety check of <c>ApplicationHelper.ClearCache</c>,
    ''' which refuses to delete directories outside the application folder.
    ''' </summary>
    <TestMethod>
    Public Sub CachePaths_AreInsideApplicationDirectory()

        Dim appDir As String = AppGlobalsTests.ApplicationDirectoryPath.TrimEnd(Path.DirectorySeparatorChar) & Path.DirectorySeparatorChar
        Dim cachePaths As String() = {AppGlobals.JotCachePath, AppGlobals.ChromeUserCachePath, AppGlobals.SeleniumCachePath}

        For Each cachePath As String In cachePaths
            Assert.StartsWith(appDir, cachePath, StringComparison.OrdinalIgnoreCase)
            Assert.IsGreaterThan(appDir.Length, cachePath.Length, $"'{cachePath}' must be a subdirectory, not the application directory itself.")
        Next
    End Sub

    ''' <summary>
    ''' The cache directories must be distinct, so that clearing one cache does not delete another.
    ''' </summary>
    <TestMethod>
    Public Sub CachePaths_AreDistinct()

        Dim cachePaths As String() = {AppGlobals.JotCachePath, AppGlobals.ChromeUserCachePath, AppGlobals.SeleniumCachePath}
        Assert.AreEqual(cachePaths.Length, cachePaths.Distinct(StringComparer.OrdinalIgnoreCase).Count())
    End Sub

    ''' <summary>
    ''' The application title must contain the product name, the assembly name and the full version.
    ''' </summary>
    <TestMethod>
    Public Sub ApplicationTitleAndVersion_ContainsProductAssemblyAndVersion()

        Dim asm As Assembly = GetType(DynamicPlugin).Assembly
        Dim ver As Version = asm.GetName().Version
        Dim expected As String = $"Torrent Tracker Registration Monitor ({asm.GetName().Name}) v{ver.Major}.{ver.Minor}.{ver.Build}"

        Assert.AreEqual(expected, AppGlobals.ApplicationTitleAndVersion)
    End Sub

    ''' <summary>
    ''' The default automatic plugin execution interval must be one hour.
    ''' </summary>
    <TestMethod>
    Public Sub AutomaticPluginRunInterval_IsOneHour()

        Assert.AreEqual(TimeSpan.FromHours(1), AppGlobals.AutomaticPluginRunInterval)
    End Sub

    ''' <summary>
    ''' The context menu URL tags must be distinct, since they are used to select which plugin URL to open.
    ''' </summary>
    <TestMethod>
    Public Sub UrlTags_AreDistinct()

        Dim tags As String() = {AppGlobals.TagLoginUrl, AppGlobals.TagRegistrationUrl, AppGlobals.TagApplicationUrl}
        Assert.AreEqual(tags.Length, tags.Distinct(StringComparer.Ordinal).Count())
    End Sub

    ''' <summary>
    ''' The language selector must offer English and Spanish (the cultures with string resources).
    ''' </summary>
    <TestMethod>
    Public Sub LanguageComboBoxItems_ContainEnglishAndSpanish()

        Dim languages As String() = AppGlobals.LanguageComboBoxItems.
                                               Select(Function(item As LanguageComboBoxItem) item.Culture.TwoLetterISOLanguageName).
                                               ToArray()

        CollectionAssert.AreEquivalent({"en", "es"}, languages)
    End Sub

    ''' <summary>
    ''' The GitHub URL must be an absolute HTTPS URL pointing to the official repository.
    ''' </summary>
    <TestMethod>
    Public Sub GitHubUrl_IsOfficialRepositoryUrl()

        Dim uri As New Uri(AppGlobals.GitHubUrl, UriKind.Absolute)

        Assert.AreEqual(Uri.UriSchemeHttps, uri.Scheme)
        Assert.AreEqual("github.com", uri.Host)
        Assert.AreEqual("/ElektroStudios/Torrent-Tracker-Registration-Monitor/", uri.AbsolutePath)
    End Sub

    ''' <summary>
    ''' The string resource manager must be able to resolve the TTRM string resources.
    ''' </summary>
    <TestMethod>
    Public Sub StringsResourceManager_ResolvesResources()

        Assert.IsFalse(String.IsNullOrEmpty(AppGlobals.StringsResourceManager.GetString("StatusMsg_OperationCompleted", Globalization.CultureInfo.InvariantCulture)))
    End Sub

#End Region

End Class
