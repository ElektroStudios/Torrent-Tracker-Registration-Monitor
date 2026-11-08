#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections
Imports System.Globalization
Imports System.IO
Imports System.Resources
Imports System.Text.RegularExpressions

#End Region

''' <summary>
''' Tests for the localized string resources of TTRM (English and Spanish).
''' </summary>
<TestClass>
Public NotInheritable Class LocalizationTests

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

#Region " Tests "

    ''' <summary>
    ''' Every string resource of the neutral (English) culture must also exist in the Spanish culture.
    ''' </summary>
    <TestMethod>
    Public Sub SpanishResources_ContainEveryNeutralKey()

        Dim neutralKeys As HashSet(Of String) = LocalizationTests.GetResourceKeys(CultureInfo.InvariantCulture)
        Dim spanishKeys As HashSet(Of String) = LocalizationTests.GetResourceKeys(New CultureInfo("es"))

        Dim missing As String() = neutralKeys.Except(spanishKeys).OrderBy(Function(key As String) key).ToArray()
        Assert.IsEmpty(missing, $"Keys missing in Spanish resources: {String.Join(", ", missing)}")
    End Sub

    ''' <summary>
    ''' The Spanish culture must not contain keys that do not exist in the neutral (English) culture.
    ''' </summary>
    <TestMethod>
    Public Sub SpanishResources_ContainNoOrphanKeys()

        Dim neutralKeys As HashSet(Of String) = LocalizationTests.GetResourceKeys(CultureInfo.InvariantCulture)
        Dim spanishKeys As HashSet(Of String) = LocalizationTests.GetResourceKeys(New CultureInfo("es"))

        Dim orphans As String() = spanishKeys.Except(neutralKeys).OrderBy(Function(key As String) key).ToArray()
        Assert.IsEmpty(orphans, $"Orphan keys in Spanish resources: {String.Join(", ", orphans)}")
    End Sub

    ''' <summary>
    ''' Composite format strings must have the same placeholders in both languages,
    ''' otherwise <see cref="String.Format(String, Object())"/> could throw <see cref="FormatException"/> in one of them.
    ''' </summary>
    <TestMethod>
    Public Sub FormatPlaceholders_MatchBetweenLanguages()

        Dim neutralSet As ResourceSet = LocalizationTests.GetResourceSet(CultureInfo.InvariantCulture)
        Dim spanishSet As ResourceSet = LocalizationTests.GetResourceSet(New CultureInfo("es"))
        Dim mismatches As New List(Of String)

        For Each entry As DictionaryEntry In neutralSet
            Dim key As String = CStr(entry.Key)
            Dim neutralValue As String = TryCast(entry.Value, String)
            Dim spanishValue As String = spanishSet.GetString(key)
            If neutralValue Is Nothing OrElse spanishValue Is Nothing Then
                Continue For ' Non-string resources, or missing keys (covered by another test).
            End If

            Dim neutralPlaceholders As String = LocalizationTests.GetPlaceholderSignature(neutralValue)
            Dim spanishPlaceholders As String = LocalizationTests.GetPlaceholderSignature(spanishValue)
            If Not String.Equals(neutralPlaceholders, spanishPlaceholders, StringComparison.Ordinal) Then
                mismatches.Add($"{key} (en: [{neutralPlaceholders}], es: [{spanishPlaceholders}])")
            End If
        Next

        Assert.IsEmpty(mismatches, $"Placeholder mismatches: {String.Join("; ", mismatches)}")
    End Sub

    ''' <summary>
    ''' Every resource key used by a plugin (string literals that start with "StatusMsg_") must exist,
    ''' because unknown keys are silently printed as literal text instead of the localized message.
    ''' </summary>
    '''
    ''' <param name="relativeJsonPath">
    ''' The path of the plugin JSON file relative to the plugins directory.
    ''' </param>
    <TestMethod>
    <DynamicData(NameOf(LocalizationTests.PluginJsonFiles))>
    Public Sub PluginCode_UsesOnlyExistingResourceKeys(relativeJsonPath As String)

        Dim jsonPath As String = PluginCatalog.GetFullPath(relativeJsonPath)
        Dim config As JsonPluginConfig = PluginCatalog.ReadConfig(jsonPath)
        Dim vbCode As String = File.ReadAllText(PluginCatalog.ResolveDeclaredPath(jsonPath, config.VbCodeFile))

        Dim existingKeys As HashSet(Of String) = LocalizationTests.GetResourceKeys(CultureInfo.InvariantCulture)
        Dim usedKeys As IEnumerable(Of String) =
            Regex.Matches(vbCode, """(StatusMsg_\w+)""", RegexOptions.CultureInvariant).
                  Select(Function(m As Match) m.Groups(1).Value).
                  Distinct(StringComparer.Ordinal)

        Dim unknownKeys As String() = usedKeys.Where(Function(key As String) Not existingKeys.Contains(key)).ToArray()
        Assert.IsEmpty(unknownKeys, $"Unknown resource keys: {String.Join(", ", unknownKeys)}")
    End Sub

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Gets the string resource set of TTRM for the specified culture, without fallback to the parent culture.
    ''' </summary>
    '''
    ''' <param name="culture">
    ''' The culture.
    ''' </param>
    '''
    ''' <returns>
    ''' The resulting <see cref="ResourceSet"/>.
    ''' </returns>
    '''
    ''' <exception cref="InvalidOperationException">
    ''' Thrown when the resource set for the specified culture is not found.
    ''' </exception>
    Private Shared Function GetResourceSet(culture As CultureInfo) As ResourceSet

        Dim rs As ResourceSet = AppGlobals.StringsResourceManager.GetResourceSet(culture, createIfNotExists:=True, tryParents:=False)
        If rs Is Nothing Then
            Throw New InvalidOperationException($"Resource set not found for culture '{culture.Name}'.")
        End If
        Return rs
    End Function

    ''' <summary>
    ''' Gets the string resource keys of TTRM for the specified culture, without fallback to the parent culture.
    ''' </summary>
    '''
    ''' <param name="culture">
    ''' The culture.
    ''' </param>
    '''
    ''' <returns>
    ''' The resource keys.
    ''' </returns>
    Private Shared Function GetResourceKeys(culture As CultureInfo) As HashSet(Of String)

        Dim keys As New HashSet(Of String)(StringComparer.Ordinal)
        For Each entry As DictionaryEntry In LocalizationTests.GetResourceSet(culture)
            keys.Add(CStr(entry.Key))
        Next
        Return keys
    End Function

    ''' <summary>
    ''' Gets a normalized signature of the distinct composite format placeholder indices (e.g. "0,1") of a string.
    ''' </summary>
    '''
    ''' <param name="value">
    ''' The string.
    ''' </param>
    '''
    ''' <returns>
    ''' The placeholder signature.
    ''' </returns>
    Private Shared Function GetPlaceholderSignature(value As String) As String

        Dim indices As IEnumerable(Of Integer) =
            Regex.Matches(value, "(?<!\{)\{(\d+)(?:[,:][^}]*)?\}(?!\})", RegexOptions.CultureInvariant).
                  Select(Function(m As Match) Integer.Parse(m.Groups(1).Value, CultureInfo.InvariantCulture)).
                  Distinct().
                  OrderBy(Function(i As Integer) i)

        Return String.Join(",", indices)
    End Function

#End Region

End Class
