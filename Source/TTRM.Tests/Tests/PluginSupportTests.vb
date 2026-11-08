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
''' Tests for the browser-independent members of <see cref="PluginSupport"/> module.
''' </summary>
<TestClass>
Public NotInheritable Class PluginSupportTests

#Region " IsBlankPageSource "

    ''' <summary>
    ''' Null, empty, white-space and empty HTML skeleton page sources must be considered blank.
    ''' </summary>
    <TestMethod>
    <DataRow(Nothing)>
    <DataRow("")>
    <DataRow("   ")>
    <DataRow("<html><head></head><body></body></html>")>
    <DataRow("  <HTML><HEAD></HEAD><BODY></BODY></HTML>  ")>
    Public Sub IsBlankPageSource_BlankSource_ReturnsTrue(pageSource As String)

        Assert.IsTrue(PluginSupport.IsBlankPageSource(pageSource))
    End Sub

    ''' <summary>
    ''' Page sources with content must not be considered blank.
    ''' </summary>
    <TestMethod>
    <DataRow("<html><head></head><body>Registration is closed</body></html>")>
    <DataRow("<html><head><title>x</title></head><body></body></html>")>
    <DataRow("plain text")>
    Public Sub IsBlankPageSource_SourceWithContent_ReturnsFalse(pageSource As String)

        Assert.IsFalse(PluginSupport.IsBlankPageSource(pageSource))
    End Sub

#End Region

#Region " PageSourceContainsAnyTrigger "

    ''' <summary>
    ''' Triggers must be found ignoring case.
    ''' </summary>
    <TestMethod>
    <DataRow("<p>Registration is DISABLED</p>", "registration is disabled")>
    <DataRow("<p>registration is disabled</p>", "Registration Is Disabled")>
    Public Sub PageSourceContainsAnyTrigger_IgnoresCase(pageSource As String, trigger As String)

        Assert.IsTrue(PluginSupport.PageSourceContainsAnyTrigger(pageSource, {trigger}))
    End Sub

    ''' <summary>
    ''' Triggers must be found ignoring accents/diacritics in both the page source and the trigger
    ''' (this prevents false negatives on Spanish/Portuguese trackers).
    ''' </summary>
    <TestMethod>
    <DataRow("<p>El registro está cerrado</p>", "El registro esta cerrado")>
    <DataRow("<p>El registro esta cerrado</p>", "El registro está cerrado")>
    <DataRow("<p>INSCRIÇÕES FECHADAS</p>", "inscricoes fechadas")>
    Public Sub PageSourceContainsAnyTrigger_IgnoresDiacritics(pageSource As String, trigger As String)

        Assert.IsTrue(PluginSupport.PageSourceContainsAnyTrigger(pageSource, {trigger}))
    End Sub

    ''' <summary>
    ''' The result must be <see langword="True"/> if any (not necessarily all) of the triggers is found.
    ''' </summary>
    <TestMethod>
    Public Sub PageSourceContainsAnyTrigger_AnyTriggerFound_ReturnsTrue()

        Dim triggers As String() = {"signups are closed", "registration is disabled", "invite only"}
        Assert.IsTrue(PluginSupport.PageSourceContainsAnyTrigger("<div>Invite only</div>", triggers))
    End Sub

    ''' <summary>
    ''' The result must be <see langword="False"/> if none of the triggers is found.
    ''' </summary>
    <TestMethod>
    Public Sub PageSourceContainsAnyTrigger_NoTriggerFound_ReturnsFalse()

        Dim triggers As String() = {"signups are closed", "registration is disabled"}
        Assert.IsFalse(PluginSupport.PageSourceContainsAnyTrigger("<form id=""register"">Sign up</form>", triggers))
    End Sub

    ''' <summary>
    ''' An empty trigger array must never match.
    ''' </summary>
    <TestMethod>
    Public Sub PageSourceContainsAnyTrigger_EmptyTriggers_ReturnsFalse()

        Assert.IsFalse(PluginSupport.PageSourceContainsAnyTrigger("<p>anything</p>", Array.Empty(Of String)()))
    End Sub

    ''' <summary>
    ''' Null arguments must throw <see cref="ArgumentNullException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub PageSourceContainsAnyTrigger_NullArguments_Throw()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PluginSupport.PageSourceContainsAnyTrigger(Nothing, {"x"}))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PluginSupport.PageSourceContainsAnyTrigger("x", Nothing))
    End Sub

#End Region

#Region " IsResourceUrlLogEntryMessage "

    ''' <summary>
    ''' Browser log entries that refer to resource files (scripts, styles, images...) must be detected,
    ''' so that their HTTP errors are ignored when checking the page status.
    ''' </summary>
    <TestMethod>
    <DataRow("https://tracker.example/js/app.js - Failed to load resource: the server responded with a status of 404 ()")>
    <DataRow("https://tracker.example/css/site.min.css?v=123 - Failed to load resource: the server responded with a status of 404 ()")>
    <DataRow("https://tracker.example/images/logo.png 0:0 Failed to load resource: the server responded with a status of 403 ()")>
    Public Sub IsResourceUrlLogEntryMessage_ResourceUrl_ReturnsTrue(msg As String)

        Assert.IsTrue(PluginSupport.IsResourceUrlLogEntryMessage(msg))
    End Sub

    ''' <summary>
    ''' Browser log entries that refer to pages/documents must not be considered resource URLs.
    ''' </summary>
    <TestMethod>
    <DataRow("https://tracker.example/register - Failed to load resource: the server responded with a status of 403 ()")>
    <DataRow("https://tracker.example/signup.php - Failed to load resource: the server responded with a status of 500 ()")>
    <DataRow("https://tracker.example/index.php?page=signup - Failed to load resource: the server responded with a status of 502 ()")>
    <DataRow("https://tracker.example/about.html - Failed to load resource: the server responded with a status of 404 ()")>
    <DataRow("https://tracker.example/ - Failed to load resource: the server responded with a status of 503 ()")>
    Public Sub IsResourceUrlLogEntryMessage_PageUrl_ReturnsFalse(msg As String)

        Assert.IsFalse(PluginSupport.IsResourceUrlLogEntryMessage(msg))
    End Sub

    ''' <summary>
    ''' Malformed messages (no URL, no white-space) must return <see langword="False"/> instead of throwing.
    ''' </summary>
    <TestMethod>
    <DataRow("")>
    <DataRow("NoSpacesHere")>
    <DataRow("not-a-url something happened")>
    Public Sub IsResourceUrlLogEntryMessage_MalformedMessage_ReturnsFalse(msg As String)

        Assert.IsFalse(PluginSupport.IsResourceUrlLogEntryMessage(msg))
    End Sub

#End Region

#Region " ResolveLocalizedString "

    ''' <summary>
    ''' A resource key must be resolved to its localized string for the current UI culture.
    ''' </summary>
    <TestMethod>
    <DataRow("en", "🎉 Registration form is open.")>
    <DataRow("es", "🎉 El formulario de registro está abierto.")>
    Public Sub ResolveLocalizedString_ResourceKey_ReturnsLocalizedText(cultureName As String, expected As String)

        PluginSupportTests.WithUICulture(cultureName,
            Sub() Assert.AreEqual(expected, PluginSupport.ResolveLocalizedString("StatusMsg_DetectedRegOpen")))
    End Sub

    ''' <summary>
    ''' A resource key of a composite format string must be resolved and formatted with the specified arguments.
    ''' </summary>
    <TestMethod>
    <DataRow("en", "🌐 Connecting to Blutopia website...")>
    <DataRow("es", "🌐 Conectando con el sitio web de Blutopia...")>
    Public Sub ResolveLocalizedString_FormatKey_ReturnsFormattedText(cultureName As String, expected As String)

        PluginSupportTests.WithUICulture(cultureName,
            Sub() Assert.AreEqual(expected, PluginSupport.ResolveLocalizedString("StatusMsg_ConnectingFormat", "Blutopia")))
    End Sub

    ''' <summary>
    ''' A text that is not a resource key must be returned as is (plugins can log literal messages).
    ''' </summary>
    <TestMethod>
    Public Sub ResolveLocalizedString_LiteralText_ReturnsSameText()

        Assert.AreEqual("➜ https://tracker.example/register", PluginSupport.ResolveLocalizedString("➜ https://tracker.example/register"))
    End Sub

    ''' <summary>
    ''' A literal text with composite format placeholders must be formatted with the specified arguments.
    ''' </summary>
    <TestMethod>
    Public Sub ResolveLocalizedString_LiteralFormatText_ReturnsFormattedText()

        Assert.AreEqual("Found 3 items", PluginSupport.ResolveLocalizedString("Found {0} items", 3))
    End Sub

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Executes the specified action with the specified UI culture, restoring the previous UI culture afterwards.
    ''' </summary>
    '''
    ''' <param name="cultureName">
    ''' The culture name.
    ''' </param>
    '''
    ''' <param name="action">
    ''' The action to execute.
    ''' </param>
    Private Shared Sub WithUICulture(cultureName As String, action As Action)

        ArgumentNullException.ThrowIfNull(action)

        Dim previousCulture As CultureInfo = Thread.CurrentThread.CurrentUICulture
        Try
            Thread.CurrentThread.CurrentUICulture = New CultureInfo(cultureName)
            action()
        Finally
            Thread.CurrentThread.CurrentUICulture = previousCulture
        End Try
    End Sub

#End Region

End Class
