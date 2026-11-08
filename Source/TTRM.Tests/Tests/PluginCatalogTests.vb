#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO
Imports System.Reflection
Imports System.Runtime.Loader

Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Emit
Imports Microsoft.CodeAnalysis.VisualBasic

#End Region

''' <summary>
''' Integrity tests for the real plugins shipped with TTRM (the "plugins" folder).
''' <para></para>
''' These tests do not connect to any website.
''' They validate the plugin JSON files, their referenced files,
''' and that every plugin source code compiles with the same Roslyn settings used by TTRM at runtime.
''' </summary>
<TestClass>
Public NotInheritable Class PluginCatalogTests

#Region " Data Sources "

    ''' <summary>
    ''' Gets the plugin JSON files (relative paths) as data rows.
    ''' </summary>
    Public Shared ReadOnly Property PluginJsonFiles As IEnumerable(Of Object())
        Get
            Return PluginCatalog.JsonFileRows
        End Get
    End Property

#End Region

#Region " Catalog-Wide Tests "

    ''' <summary>
    ''' The plugins directory must exist in the application directory and contain plugins.
    ''' </summary>
    <TestMethod>
    Public Sub PluginsDirectory_ContainsPlugins()

        Assert.IsTrue(Directory.Exists(PluginCatalog.PluginsDirectoryPath), $"Directory not found: {PluginCatalog.PluginsDirectoryPath}")
        Assert.IsNotEmpty(PluginCatalog.JsonFiles)
    End Sub

    ''' <summary>
    ''' Plugin names must be unique (ignoring case), otherwise TTRM reports duplicated plugins at startup.
    ''' </summary>
    <TestMethod>
    Public Sub PluginNames_AreUnique()

        Dim duplicates As String() = PluginCatalog.JsonFiles.
            Select(Function(jsonPath As String) PluginCatalog.ReadConfig(jsonPath).Name).
            GroupBy(Function(name As String) name, StringComparer.InvariantCultureIgnoreCase).
            Where(Function(group As IGrouping(Of String, String)) group.Count() > 1).
            Select(Function(group As IGrouping(Of String, String)) group.Key).
            ToArray()

        Assert.IsEmpty(duplicates, $"Duplicated plugin names: {String.Join(", ", duplicates)}")
    End Sub

    ''' <summary>
    ''' The UI member base names derived from plugin names must be unique,
    ''' otherwise two plugins would share the same dynamic control names and the same Chrome cache folder.
    ''' </summary>
    <TestMethod>
    Public Sub PluginUIMemberBaseNames_AreUnique()

        Dim duplicates As String() = PluginCatalog.JsonFiles.
            Select(Function(jsonPath As String) UIHelper.ConvertStringToNetMemberName(PluginCatalog.ReadConfig(jsonPath).Name)).
            GroupBy(Function(name As String) name, StringComparer.OrdinalIgnoreCase).
            Where(Function(group As IGrouping(Of String, String)) group.Count() > 1).
            Select(Function(group As IGrouping(Of String, String)) group.Key).
            ToArray()

        Assert.IsEmpty(duplicates, $"Duplicated UI member base names: {String.Join(", ", duplicates)}")
    End Sub

#End Region

#Region " Per-Plugin Tests "

    ''' <summary>
    ''' Each plugin JSON file must be valid and declare the required metadata.
    ''' </summary>
    '''
    ''' <param name="relativeJsonPath">
    ''' The path of the plugin JSON file relative to the plugins directory.
    ''' </param>
    <TestMethod>
    <DynamicData(NameOf(PluginCatalogTests.PluginJsonFiles))>
    Public Sub PluginJson_HasRequiredMetadata(relativeJsonPath As String)

        Dim config As JsonPluginConfig = PluginCatalog.ReadConfig(PluginCatalog.GetFullPath(relativeJsonPath))

        Assert.IsFalse(String.IsNullOrWhiteSpace(config.Name), "Name is empty.")
        Assert.IsFalse(String.IsNullOrWhiteSpace(config.Description), "Description is empty.")
        Assert.IsFalse(String.IsNullOrWhiteSpace(config.IconPath), "IconPath is empty.")
        Assert.IsFalse(String.IsNullOrWhiteSpace(config.VbCodeFile), "VbCodeFile is empty.")
    End Sub

    ''' <summary>
    ''' The icon and VB.NET code files declared in each plugin JSON file must exist.
    ''' </summary>
    '''
    ''' <param name="relativeJsonPath">
    ''' The path of the plugin JSON file relative to the plugins directory.
    ''' </param>
    <TestMethod>
    <DynamicData(NameOf(PluginCatalogTests.PluginJsonFiles))>
    Public Sub PluginJson_ReferencedFilesExist(relativeJsonPath As String)

        Dim jsonPath As String = PluginCatalog.GetFullPath(relativeJsonPath)
        Dim config As JsonPluginConfig = PluginCatalog.ReadConfig(jsonPath)

        Dim iconPath As String = PluginCatalog.ResolveDeclaredPath(jsonPath, config.IconPath)
        Dim vbCodePath As String = PluginCatalog.ResolveDeclaredPath(jsonPath, config.VbCodeFile)

        Assert.IsTrue(File.Exists(iconPath), $"Icon file not found: {iconPath}")
        Assert.IsTrue(File.Exists(vbCodePath), $"VB.NET code file not found: {vbCodePath}")
    End Sub

    ''' <summary>
    ''' The URLs declared in each plugin JSON file must be absolute HTTP/HTTPS URLs (when specified).
    ''' </summary>
    '''
    ''' <param name="relativeJsonPath">
    ''' The path of the plugin JSON file relative to the plugins directory.
    ''' </param>
    <TestMethod>
    <DynamicData(NameOf(PluginCatalogTests.PluginJsonFiles))>
    Public Sub PluginJson_UrlsAreAbsoluteHttpUrls(relativeJsonPath As String)

        Dim config As JsonPluginConfig = PluginCatalog.ReadConfig(PluginCatalog.GetFullPath(relativeJsonPath))

        Dim urls As New Dictionary(Of String, String) From {
            {NameOf(config.UrlLogin), config.UrlLogin},
            {NameOf(config.UrlRegistration), config.UrlRegistration},
            {NameOf(config.UrlApplication), config.UrlApplication}
        }

        For Each kv As KeyValuePair(Of String, String) In urls
            If String.IsNullOrEmpty(kv.Value) Then
                Continue For ' Optional: not all trackers have every kind of page.
            End If

            Dim uri As Uri = Nothing
            Assert.IsTrue(Uri.TryCreate(kv.Value, UriKind.Absolute, uri), $"{kv.Key} is not an absolute URL: {kv.Value}")
            Assert.IsTrue(uri.Scheme = Uri.UriSchemeHttp OrElse uri.Scheme = Uri.UriSchemeHttps, $"{kv.Key} is not an HTTP/HTTPS URL: {kv.Value}")
        Next
    End Sub

    ''' <summary>
    ''' Each plugin source code must compile with the same Roslyn options and references used by TTRM at runtime,
    ''' and must define exactly one non-abstract <see cref="DynamicPlugin"/> class with a parameterless
    ''' <c>RunAsync</c> method returning <see cref="Task(Of RegistrationFlags)"/>,
    ''' which is what <see cref="DynamicPlugin.RunAsync"/> looks for through reflection.
    ''' </summary>
    '''
    ''' <param name="relativeJsonPath">
    ''' The path of the plugin JSON file relative to the plugins directory.
    ''' </param>
    <TestMethod>
    <DynamicData(NameOf(PluginCatalogTests.PluginJsonFiles))>
    Public Sub PluginCode_CompilesAndExposesRunAsync(relativeJsonPath As String)

        Dim jsonPath As String = PluginCatalog.GetFullPath(relativeJsonPath)
        Dim config As JsonPluginConfig = PluginCatalog.ReadConfig(jsonPath)
        Dim vbCodePath As String = PluginCatalog.ResolveDeclaredPath(jsonPath, config.VbCodeFile)
        Dim vbCode As String = File.ReadAllText(vbCodePath)

        Dim syntaxTree As SyntaxTree = VisualBasicSyntaxTree.ParseText(vbCode, path:=vbCodePath)
        Dim compilation As VisualBasicCompilation = VisualBasicCompilation.Create(
            $"TTRM_Tests_{UIHelper.ConvertStringToNetMemberName(config.Name)}_{Guid.NewGuid():N}",
            syntaxTrees:={syntaxTree},
            references:=DynamicPlugin.CompilationReferences,
            options:=DynamicPlugin.CompileOptions)

        Using ms As New MemoryStream()
            Dim result As EmitResult = compilation.Emit(ms)
            If Not result.Success Then
                Dim errors As IEnumerable(Of String) =
                    result.Diagnostics.Where(Function(diag As Diagnostic) diag.Severity = DiagnosticSeverity.Error).
                                       Select(Function(diag As Diagnostic) diag.ToString())
                Assert.Fail($"Plugin compilation failed:{Environment.NewLine}{String.Join(Environment.NewLine, errors)}")
            End If

            ms.Seek(0, SeekOrigin.Begin)

            Dim loadContext As New AssemblyLoadContext($"TTRM_Tests_{Guid.NewGuid():N}", isCollectible:=True)
            Try
                Dim asm As Assembly = loadContext.LoadFromStream(ms)
                Dim pluginTypes As Type() =
                    asm.GetTypes().Where(Function(t As Type) GetType(DynamicPlugin).IsAssignableFrom(t) AndAlso Not t.IsAbstract).ToArray()

                Assert.HasCount(1, pluginTypes, "The plugin must define exactly one non-abstract class that inherits from DynamicPlugin.")
                Dim pluginType As Type = pluginTypes(0)

                ' Same lookup performed by DynamicPlugin.RunAsync.
                Dim runAsyncMethod As MethodInfo =
                    pluginType.GetMethod(NameOf(DynamicPlugin.RunAsync),
                                         BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.DeclaredOnly,
                                         Nothing, System.Type.EmptyTypes, Nothing)

                Assert.IsNotNull(runAsyncMethod, "The plugin class does not declare a parameterless RunAsync method.")
                Assert.AreEqual(GetType(Task(Of RegistrationFlags)), runAsyncMethod.ReturnType)
                Assert.IsNotNull(pluginType.GetConstructor(System.Type.EmptyTypes), "The plugin class must have a parameterless constructor.")

            Finally
                loadContext.Unload()

            End Try
        End Using
    End Sub

    ''' <summary>
    ''' Each plugin must be fully loadable through the <see cref="DynamicPlugin"/> constructor, including its image.
    ''' <para></para>
    ''' This test requires Windows, because loading images requires GDI+.
    ''' </summary>
    '''
    ''' <param name="relativeJsonPath">
    ''' The path of the plugin JSON file relative to the plugins directory.
    ''' </param>
    <TestMethod>
    <OSCondition(OperatingSystems.Windows)>
    <DynamicData(NameOf(PluginCatalogTests.PluginJsonFiles))>
    Public Sub PluginJson_LoadsAsDynamicPluginWithImage(relativeJsonPath As String)

        Dim jsonPath As String = PluginCatalog.GetFullPath(relativeJsonPath)

        Dim plugin As New DynamicPlugin(jsonPath)
        Try
            Assert.IsFalse(String.IsNullOrWhiteSpace(plugin.Name))
            Assert.IsFalse(String.IsNullOrWhiteSpace(plugin.VbCode))
            Assert.IsNotNull(plugin.Image, "The plugin image could not be loaded.")
        Finally
            ' Only the image is disposed: DynamicPlugin.Dispose() also disposes the plugin UI controls,
            ' which requires the main form to be open.
            plugin.Image?.Dispose()
        End Try
    End Sub

#End Region

End Class
