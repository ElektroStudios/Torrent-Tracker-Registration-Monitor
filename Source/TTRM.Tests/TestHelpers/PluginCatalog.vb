#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Text.Json

#End Region

''' <summary>
''' Provides access to the real plugins shipped with TTRM
''' (the "plugins" folder copied to the test output directory).
''' </summary>
Friend Module PluginCatalog

#Region " Fields "

    ''' <summary>
    ''' The same JSON options used by <see cref="DynamicPlugin"/> to deserialize plugin configuration files.
    ''' </summary>
    Friend ReadOnly JsonOptions As New JsonSerializerOptions With {
        .PropertyNameCaseInsensitive = True
    }

#End Region

#Region " Properties "

    ''' <summary>
    ''' Gets the full path of the plugins directory used by the application.
    ''' </summary>
    Friend ReadOnly Property PluginsDirectoryPath As String
        Get
            Return AppGlobals.PluginsDirectoryPath
        End Get
    End Property

    ''' <summary>
    ''' Gets the full paths of all the plugin JSON files, sorted alphabetically.
    ''' </summary>
    '''
    ''' <exception cref="DirectoryNotFoundException">
    ''' Thrown when the plugins directory does not exist.
    ''' </exception>
    Friend ReadOnly Property JsonFiles As ReadOnlyCollection(Of String)
        Get
            If Not Directory.Exists(PluginCatalog.PluginsDirectoryPath) Then
                Throw New DirectoryNotFoundException($"Plugins directory not found: {PluginCatalog.PluginsDirectoryPath}")
            End If

            ' Same enumeration options used by ApplicationHelper.LoadAllPluginsFromJson.
            Dim options As New EnumerationOptions With {
                .AttributesToSkip = FileAttributes.None,
                .MatchCasing = MatchCasing.CaseInsensitive,
                .MatchType = MatchType.Simple,
                .RecurseSubdirectories = True
            }

            Dim files As New List(Of String)
            For Each filePath As String In Directory.EnumerateFiles(PluginCatalog.PluginsDirectoryPath, "*.json", options)
                Threading.Thread.CurrentThread.Join(0)
                files.Add(filePath)
            Next
            files.Sort(StringComparer.OrdinalIgnoreCase)

            Return files.AsReadOnly()
        End Get
    End Property

    ''' <summary>
    ''' Gets the plugin JSON files as data rows for data-driven tests.
    ''' <para></para>
    ''' Each row contains a single element: the path of the JSON file relative to the plugins directory.
    ''' </summary>
    Friend ReadOnly Property JsonFileRows As IEnumerable(Of Object())
        Get
            Return PluginCatalog.JsonFiles.Select(
                Function(filePath As String) New Object() {Path.GetRelativePath(PluginCatalog.PluginsDirectoryPath, filePath)})
        End Get
    End Property

#End Region

#Region " Methods "

    ''' <summary>
    ''' Gets the full path of a plugin JSON file from its path relative to the plugins directory.
    ''' </summary>
    '''
    ''' <param name="relativeJsonPath">
    ''' The path of the JSON file relative to the plugins directory.
    ''' </param>
    '''
    ''' <returns>
    ''' The full path of the plugin JSON file.
    ''' </returns>
    Friend Function GetFullPath(relativeJsonPath As String) As String

        ArgumentException.ThrowIfNullOrWhiteSpace(relativeJsonPath)
        Return Path.Combine(PluginCatalog.PluginsDirectoryPath, relativeJsonPath)
    End Function

    ''' <summary>
    ''' Deserializes the specified plugin JSON file.
    ''' </summary>
    '''
    ''' <param name="jsonPath">
    ''' The full path of the plugin JSON file.
    ''' </param>
    '''
    ''' <returns>
    ''' The resulting <see cref="JsonPluginConfig"/>.
    ''' </returns>
    '''
    ''' <exception cref="InvalidDataException">
    ''' Thrown when the JSON file deserializes to a null object.
    ''' </exception>
    Friend Function ReadConfig(jsonPath As String) As JsonPluginConfig

        ArgumentException.ThrowIfNullOrWhiteSpace(jsonPath)

        Dim json As String = File.ReadAllText(jsonPath)
        Dim config As JsonPluginConfig = JsonSerializer.Deserialize(Of JsonPluginConfig)(json, PluginCatalog.JsonOptions)
        If config Is Nothing Then
            Throw New InvalidDataException($"The plugin JSON file deserialized to a null object: {jsonPath}")
        End If
        Return config
    End Function

    ''' <summary>
    ''' Resolves a file path declared in a plugin JSON file (e.g. <c>".\Plugin.vb"</c>)
    ''' relative to the directory of that JSON file,
    ''' normalizing the directory separators for the current platform.
    ''' </summary>
    '''
    ''' <param name="jsonPath">
    ''' The full path of the plugin JSON file.
    ''' </param>
    '''
    ''' <param name="declaredPath">
    ''' The file path declared in the plugin JSON file.
    ''' </param>
    '''
    ''' <returns>
    ''' The resolved full file path.
    ''' </returns>
    Friend Function ResolveDeclaredPath(jsonPath As String, declaredPath As String) As String

        ArgumentException.ThrowIfNullOrWhiteSpace(jsonPath)
        ArgumentException.ThrowIfNullOrWhiteSpace(declaredPath)

        Dim normalized As String = declaredPath.Replace("\"c, Path.DirectorySeparatorChar).
                                                Replace("/"c, Path.DirectorySeparatorChar)
        Return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(jsonPath), normalized))
    End Function

#End Region

End Module
