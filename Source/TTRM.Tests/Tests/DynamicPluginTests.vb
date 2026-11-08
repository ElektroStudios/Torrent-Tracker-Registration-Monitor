#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO
Imports System.Text.Json

Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic

Imports DarkUI.Controls

#End Region

''' <summary>
''' Tests for the <see cref="JsonPluginConfig"/> and <see cref="DynamicPlugin"/> classes
''' (plugin metadata loading, member copying and Roslyn compilation settings).
''' </summary>
<TestClass>
Public NotInheritable Class DynamicPluginTests

#Region " Constants "

    ''' <summary>
    ''' The VB.NET source code of the fixture plugin.
    ''' </summary>
    Private Const FixtureVbCode As String = "' Fixture plugin source code."

    ''' <summary>
    ''' Format of the JSON of the fixture plugin.
    ''' <c>{0}</c> is replaced by the icon path and <c>{1}</c> by the VB.NET code file path.
    ''' </summary>
    Private Const FixtureJsonFormat As String = "{{
        ""Name"": ""Fixture Tracker 2"",
        ""Description"": ""A fixture plugin."",
        ""UrlLogin"": ""https://fixture.example/login"",
        ""UrlRegistration"": ""https://fixture.example/register"",
        ""UrlApplication"": ""https://fixture.example/application"",
        ""IconPath"": ""{0}"",
        ""VbCodeFile"": ""{1}""
    }}"

#End Region

#Region " JsonPluginConfig "

    ''' <summary>
    ''' Plugin JSON files must be deserialized ignoring property name casing
    ''' (some real plugins use "description" instead of "Description").
    ''' </summary>
    <TestMethod>
    Public Sub JsonPluginConfig_Deserialize_IsCaseInsensitive()

        Dim json As String = "{
            ""name"": ""N"", ""DESCRIPTION"": ""D"", ""urlLogin"": ""L"", ""UrlRegistration"": ""R"",
            ""urlapplication"": ""A"", ""iconPath"": ""I"", ""VBCODEFILE"": ""V""
        }"

        Dim config As JsonPluginConfig = JsonSerializer.Deserialize(Of JsonPluginConfig)(json, PluginCatalog.JsonOptions)

        Assert.IsNotNull(config)
        Assert.AreEqual("N", config.Name)
        Assert.AreEqual("D", config.Description)
        Assert.AreEqual("L", config.UrlLogin)
        Assert.AreEqual("R", config.UrlRegistration)
        Assert.AreEqual("A", config.UrlApplication)
        Assert.AreEqual("I", config.IconPath)
        Assert.AreEqual("V", config.VbCodeFile)
    End Sub

    ''' <summary>
    ''' Missing properties must be deserialized as <see langword="Nothing"/>.
    ''' </summary>
    <TestMethod>
    Public Sub JsonPluginConfig_Deserialize_MissingPropertiesAreNothing()

        Dim config As JsonPluginConfig = JsonSerializer.Deserialize(Of JsonPluginConfig)("{""Name"": ""N""}", PluginCatalog.JsonOptions)

        Assert.IsNotNull(config)
        Assert.AreEqual("N", config.Name)
        Assert.IsNull(config.UrlApplication)
        Assert.IsNull(config.IconPath)
    End Sub

#End Region

#Region " Constructor "

    ''' <summary>
    ''' The constructor must load the plugin metadata and VB.NET source code from the JSON file.
    ''' </summary>
    <TestMethod>
    Public Sub Constructor_LoadsMetadataAndCode()

        Using tempDir As New TemporaryDirectory()
            Dim jsonPath As String = DynamicPluginTests.CreateFixturePlugin(tempDir, iconFileName:="Missing.png", vbFileName:="Fixture.vb")

            Dim plugin As New DynamicPlugin(jsonPath)

            Assert.AreEqual("Fixture Tracker 2", plugin.Name)
            Assert.AreEqual("Fixture Tracker 2", plugin.ToString())
            Assert.AreEqual("A fixture plugin.", plugin.Description)
            Assert.AreEqual("https://fixture.example/login", plugin.UrlLogin)
            Assert.AreEqual("https://fixture.example/register", plugin.UrlRegistration)
            Assert.AreEqual("https://fixture.example/application", plugin.UrlApplication)
            Assert.AreEqual(DynamicPluginTests.FixtureVbCode, plugin.VbCode)
            Assert.IsFalse(plugin.IsRunning)
        End Using
    End Sub

    ''' <summary>
    ''' If the icon file does not exist, the plugin must be loaded without image instead of failing.
    ''' </summary>
    <TestMethod>
    Public Sub Constructor_MissingIcon_ImageIsNothing()

        Using tempDir As New TemporaryDirectory()
            Dim jsonPath As String = DynamicPluginTests.CreateFixturePlugin(tempDir, iconFileName:="Missing.png", vbFileName:="Fixture.vb")

            Dim plugin As New DynamicPlugin(jsonPath)

            Assert.IsNull(plugin.Image)
        End Using
    End Sub

    ''' <summary>
    ''' The constructor must build the UI member base name and the dynamic control names from the plugin name.
    ''' </summary>
    <TestMethod>
    Public Sub Constructor_BuildsUIMemberNames()

        Using tempDir As New TemporaryDirectory()
            Dim jsonPath As String = DynamicPluginTests.CreateFixturePlugin(tempDir, iconFileName:="Missing.png", vbFileName:="Fixture.vb")

            Dim plugin As New DynamicPlugin(jsonPath)
            Dim baseName As String = "FixtureTracker2"

            Assert.AreEqual(baseName, plugin.UIMemberBaseName)
            Assert.AreEqual($"{NameOf(DarkButtonImageAllignFix)}_ButtonPane_{baseName}", plugin.ButtonPaneName)
            Assert.AreEqual($"{NameOf(TabPage)}_{baseName}", plugin.TabPageName)
            Assert.AreEqual($"{NameOf(DarkSectionPanel)}_{baseName}", plugin.SectionPanelName)
            Assert.AreEqual($"{NameOf(DarkTextBox)}_Description_{baseName}", plugin.DescriptionTextBoxName)
            Assert.AreEqual($"{NameOf(DarkButtonImageAllignFix)}_RunPlugin_{baseName}", plugin.ButtonRunPluginName)
            Assert.AreEqual($"{NameOf(DarkButtonImageAllignFix)}_OpenWebsite_{baseName}", plugin.ButtonOpenWebsiteName)
            Assert.AreEqual($"{NameOf(DarkButtonImageAllignFix)}_ClearCache_{baseName}", plugin.ButtonClearCacheName)
            Assert.AreEqual($"{NameOf(RichTextBox)}_Log_{baseName}", plugin.LogTextBoxName)
            Assert.AreEqual($"{NameOf(Label)}_Status_{baseName}", plugin.StatusLabelName)
        End Using
    End Sub

    ''' <summary>
    ''' All the dynamic control names of a plugin must be distinct and valid identifiers.
    ''' </summary>
    <TestMethod>
    Public Sub Constructor_ControlNamesAreDistinctValidIdentifiers()

        Using tempDir As New TemporaryDirectory()
            Dim jsonPath As String = DynamicPluginTests.CreateFixturePlugin(tempDir, iconFileName:="Missing.png", vbFileName:="Fixture.vb")

            Dim plugin As New DynamicPlugin(jsonPath)
            Dim names As String() = {
                plugin.ButtonPaneName, plugin.TabPageName, plugin.SectionPanelName,
                plugin.DescriptionTextBoxName, plugin.ButtonRunPluginName, plugin.ButtonOpenWebsiteName,
                plugin.ButtonClearCacheName, plugin.LogTextBoxName, plugin.StatusLabelName
            }

            Assert.AreEqual(names.Length, names.Distinct(StringComparer.Ordinal).Count())
            For Each name As String In names
                Assert.IsTrue(SyntaxFacts.IsValidIdentifier(name), $"'{name}' is not a valid identifier.")
            Next
        End Using
    End Sub

    ''' <summary>
    ''' The Chrome cache path of a plugin must be a subdirectory of the application's Chrome user cache path,
    ''' named after the UI member base name (this is required by the "Clear cache" safety check).
    ''' </summary>
    <TestMethod>
    Public Sub Constructor_PluginCachePath_IsInsideChromeUserCachePath()

        Using tempDir As New TemporaryDirectory()
            Dim jsonPath As String = DynamicPluginTests.CreateFixturePlugin(tempDir, iconFileName:="Missing.png", vbFileName:="Fixture.vb")

            Dim plugin As New DynamicPlugin(jsonPath)

            Assert.AreEqual($"{AppGlobals.ChromeUserCachePath}\{plugin.UIMemberBaseName}", plugin.PluginCachePath)
            Assert.StartsWith(AppGlobals.ChromeUserCachePath, plugin.PluginCachePath, StringComparison.OrdinalIgnoreCase)
        End Using
    End Sub

    ''' <summary>
    ''' A missing VB.NET code file must throw <see cref="FileNotFoundException"/>
    ''' (the plugin loader reports it to the user).
    ''' </summary>
    <TestMethod>
    Public Sub Constructor_MissingCodeFile_Throws()

        Using tempDir As New TemporaryDirectory()
            Dim jsonPath As String = DynamicPluginTests.CreateFixturePlugin(tempDir, iconFileName:="Missing.png", vbFileName:="Fixture.vb")
            File.Delete(Path.Combine(tempDir.FullPath, "Fixture.vb"))

            Assert.ThrowsExactly(Of FileNotFoundException)(Function() New DynamicPlugin(jsonPath))
        End Using
    End Sub

    ''' <summary>
    ''' A missing JSON file must throw <see cref="FileNotFoundException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub Constructor_MissingJsonFile_Throws()

        Using tempDir As New TemporaryDirectory()
            Dim jsonPath As String = Path.Combine(tempDir.FullPath, "DoesNotExist.json")

            Assert.ThrowsExactly(Of FileNotFoundException)(Function() New DynamicPlugin(jsonPath))
        End Using
    End Sub

    ''' <summary>
    ''' A malformed JSON file must throw <see cref="JsonException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub Constructor_MalformedJson_Throws()

        Using tempDir As New TemporaryDirectory()
            Dim jsonPath As String = tempDir.WriteFile("Broken.json", "{ ""Name"": ""Broken"", ")

            Assert.ThrowsExactly(Of JsonException)(Function() New DynamicPlugin(jsonPath))
        End Using
    End Sub

#End Region

#Region " CopyMembersTo "

    ''' <summary>
    ''' <see cref="DynamicPlugin.CopyMembersTo"/> must copy the plugin metadata to the target instance
    ''' (this is how the dynamically compiled plugin instance receives its configuration).
    ''' </summary>
    <TestMethod>
    Public Sub CopyMembersTo_CopiesMetadata()

        Using tempDir As New TemporaryDirectory()
            Dim jsonPath As String = DynamicPluginTests.CreateFixturePlugin(tempDir, iconFileName:="Missing.png", vbFileName:="Fixture.vb")
            Dim source As New DynamicPlugin(jsonPath)
            Dim target As New DynamicPlugin()

            source.CopyMembersTo(target)

            Assert.AreEqual(source.Name, target.Name)
            Assert.AreEqual(source.Description, target.Description)
            Assert.AreEqual(source.UrlLogin, target.UrlLogin)
            Assert.AreEqual(source.UrlRegistration, target.UrlRegistration)
            Assert.AreEqual(source.UrlApplication, target.UrlApplication)
            Assert.AreEqual(source.VbCode, target.VbCode)
            Assert.AreSame(source.Image, target.Image)
            Assert.AreEqual(source.UIMemberBaseName, target.UIMemberBaseName)
            Assert.AreEqual(source.PluginCachePath, target.PluginCachePath)
            Assert.AreEqual(source.IsRunning, target.IsRunning)
            Assert.IsTrue(source.Equals(target))
        End Using
    End Sub

    ''' <summary>
    ''' <see cref="DynamicPlugin.CopyMembersTo"/> must throw <see cref="ArgumentNullException"/> for a null target.
    ''' </summary>
    <TestMethod>
    Public Sub CopyMembersTo_NullTarget_Throws()

        Dim source As New DynamicPlugin()
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() source.CopyMembersTo(Nothing))
    End Sub

#End Region

#Region " Compilation Settings "

    ''' <summary>
    ''' The Roslyn compilation options must match the documented plugin development requirements.
    ''' </summary>
    <TestMethod>
    Public Sub CompileOptions_HaveExpectedValues()

        Dim options As VisualBasicCompilationOptions = DynamicPlugin.CompileOptions

        Assert.AreEqual(OutputKind.DynamicallyLinkedLibrary, options.OutputKind)
        Assert.AreEqual(Platform.AnyCpu, options.Platform)
        Assert.AreEqual(OptimizationLevel.Release, options.OptimizationLevel)
        Assert.AreEqual(OptionStrict.Off, options.OptionStrict)
        Assert.IsTrue(options.OptionInfer)
        Assert.IsFalse(options.OptionExplicit)
        Assert.IsTrue(options.CheckOverflow)
    End Sub

    ''' <summary>
    ''' The compilation references must include TTRM itself and Selenium's WebDriver, without duplicates.
    ''' </summary>
    <TestMethod>
    Public Sub CompilationReferences_IncludeRequiredAssembliesWithoutDuplicates()

        Dim displays As String() = DynamicPlugin.CompilationReferences.
                                                 Select(Function(ref As MetadataReference) ref.Display).
                                                 ToArray()

        Dim ttrmLocation As String = GetType(DynamicPlugin).Assembly.Location
        Dim webDriverLocation As String = GetType(OpenQA.Selenium.IWebDriver).Assembly.Location

        Assert.Contains(ttrmLocation, displays)
        Assert.Contains(webDriverLocation, displays)
        Assert.AreEqual(displays.Length, displays.Distinct(StringComparer.OrdinalIgnoreCase).Count())
    End Sub

    ''' <summary>
    ''' Regression test: the compilation references must be deterministic and include framework assemblies 
    ''' even if they were never loaded in the current process before the first access 
    ''' (previously only the assemblies already loaded at that moment were referenced).
    ''' </summary>
    <TestMethod>
    <DataRow("System.Net.Http")>
    <DataRow("System.Text.RegularExpressions")>
    <DataRow("System.Xml.Linq")>
    <DataRow("System.Windows.Forms")>
    <DataRow("System.Drawing.Primitives")>
    <DataRow("Microsoft.VisualBasic.Core")>
    Public Sub CompilationReferences_IncludeFrameworkAssembly(assemblyName As String)

        Dim names As String() = DynamicPlugin.CompilationReferences.
                                              Select(Function(ref As MetadataReference) Path.GetFileNameWithoutExtension(ref.Display)).
                                              ToArray()

        Assert.Contains(assemblyName, names)
    End Sub

#End Region

#Region " RunAsync "

    ''' <summary>
    ''' The VB.NET source code of a fixture plugin that does not access the UI.
    ''' </summary>
    Private Const RunnableFixtureVbCode As String = "
Imports System
Imports System.Threading.Tasks
Imports System.Net.Http
Imports System.Text.RegularExpressions
Imports System.Xml.Linq
Imports TTRM

Public Class RunnableFixturePlugin : Inherits DynamicPlugin

    Overloads Async Function RunAsync() As Task(Of RegistrationFlags)
        ' Uses types from assemblies that may not be loaded yet, to validate the compilation references.
        Dim unusedType1 As Type = GetType(HttpClient)
        Dim unusedType2 As Type = GetType(XDocument)
        Dim isOpen As Boolean = Regex.IsMatch(""Registration is OPEN"", ""open"", RegexOptions.IgnoreCase)
        Return Await Task.FromResult(If(isOpen, RegistrationFlags.RegistrationOpen, RegistrationFlags.RegistrationClosed) Or
                                     RegistrationFlags.ApplicationClosed)
    End Function

End Class
"

    ''' <summary>
    ''' <see cref="DynamicPlugin.RunAsync"/> must compile the plugin source code, 
    ''' execute its <c>RunAsync</c> method and return its result.
    ''' </summary>
    <TestMethod>
    Public Async Function RunAsync_CompilesAndExecutesPlugin() As Task

        Using tempDir As New TemporaryDirectory()
            Dim plugin As DynamicPlugin = DynamicPluginTests.CreateRunnablePlugin(tempDir)

            Dim result As RegistrationFlags = Await plugin.RunAsync(Nothing).ConfigureAwait(False)

            Assert.AreEqual(RegistrationFlags.RegistrationOpen Or RegistrationFlags.ApplicationClosed, result)
            Assert.IsFalse(plugin.IsRunning)
        End Using
    End Function

    ''' <summary>
    ''' Regression test: executing the same plugin several times must compile and load its assembly only once
    ''' (previously a new assembly was loaded into the never-unloaded plugin load context on every execution,
    ''' increasing memory usage indefinitely).
    ''' </summary>
    <TestMethod>
    Public Async Function RunAsync_MultipleExecutions_LoadAssemblyOnlyOnce() As Task

        Using tempDir As New TemporaryDirectory()
            Dim plugin As DynamicPlugin = DynamicPluginTests.CreateRunnablePlugin(tempDir)

            Await plugin.RunAsync(Nothing).ConfigureAwait(False)
            Dim assemblyCountAfterFirstRun As Integer = AppGlobals.PluginLoadContext.Assemblies.Count()

            For i As Integer = 1 To 3
                Await plugin.RunAsync(Nothing).ConfigureAwait(False)
            Next

            Assert.AreEqual(assemblyCountAfterFirstRun, AppGlobals.PluginLoadContext.Assemblies.Count())
        End Using
    End Function

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Creates a <see cref="DynamicPlugin"/> from a fixture plugin that can run without UI.
    ''' </summary>
    ''' 
    ''' <param name="tempDir">
    ''' The temporary directory where the plugin files are created.
    ''' </param>
    ''' 
    ''' <returns>
    ''' The resulting <see cref="DynamicPlugin"/>.
    ''' </returns>
    Private Shared Function CreateRunnablePlugin(tempDir As TemporaryDirectory) As DynamicPlugin

        ArgumentNullException.ThrowIfNull(tempDir)

        tempDir.WriteFile("Runnable.vb", DynamicPluginTests.RunnableFixtureVbCode)
        Dim json As String = String.Format(Globalization.CultureInfo.InvariantCulture, DynamicPluginTests.FixtureJsonFormat, "Missing.png", "Runnable.vb")
        Return New DynamicPlugin(tempDir.WriteFile("Runnable.json", json))
    End Function

    ''' <summary>
    ''' Creates a fixture plugin (JSON file and VB.NET code file) inside the specified temporary directory.
    ''' </summary>
    '''
    ''' <param name="tempDir">
    ''' The temporary directory.
    ''' </param>
    '''
    ''' <param name="iconFileName">
    ''' The icon file name declared in the JSON file (the file is not created).
    ''' </param>
    '''
    ''' <param name="vbFileName">
    ''' The VB.NET code file name declared in the JSON file (the file is created).
    ''' </param>
    '''
    ''' <returns>
    ''' The full path of the created JSON file.
    ''' </returns>
    Private Shared Function CreateFixturePlugin(tempDir As TemporaryDirectory, iconFileName As String, vbFileName As String) As String

        ArgumentNullException.ThrowIfNull(tempDir)

        tempDir.WriteFile(vbFileName, DynamicPluginTests.FixtureVbCode)
        Dim json As String = String.Format(Globalization.CultureInfo.InvariantCulture, DynamicPluginTests.FixtureJsonFormat, iconFileName, vbFileName)
        Return tempDir.WriteFile("Fixture.json", json)
    End Function

#End Region

End Class
