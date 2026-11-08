#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Xml.Linq

Imports TTRM.DevCase.ThirdParty.GitHub

#End Region

''' <summary>
''' Tests for the GitHub API models (<see cref="GitHubRelease"/>, <see cref="GitHubAuthor"/> and <see cref="GitHubAsset"/>)
''' used by the application update checker.
''' <para></para>
''' These tests use local JSON fixtures; they do not connect to GitHub.
''' </summary>
<TestClass>
Public NotInheritable Class GitHubModelTests

#Region " GitHubRelease "

    ''' <summary>
    ''' A release JSON must be parsed into the corresponding properties.
    ''' </summary>
    <TestMethod>
    Public Sub GitHubRelease_ParsesProperties()

        Dim xml As XElement = GitHubJsonFixtures.ToXElement(GitHubJsonFixtures.GetReleaseJson("v1.2.0", assetCount:=2))

        Dim release As New GitHubRelease(xml)

        Assert.AreEqual("999", release.Id)
        Assert.AreEqual("v1.2.0", release.TagName)
        Assert.AreEqual("TTRM v1.2.0", release.Name)
        Assert.AreEqual("main", release.TargetCommitish)
        Assert.AreEqual($"## Changes{vbLf}- Something new.", release.Body)
        Assert.IsFalse(release.IsDraft)
        Assert.IsTrue(release.IsPreRelease)
        Assert.AreEqual(New Uri("https://github.com/ElektroStudios/Torrent-Tracker-Registration-Monitor/releases/tag/v1.2.0"), release.UriHtml)
        Assert.AreEqual(New Uri("https://api.github.com/repos/ElektroStudios/Torrent-Tracker-Registration-Monitor/zipball/v1.2.0"), release.UriZipball)
        Assert.AreEqual(New Uri("https://api.github.com/repos/ElektroStudios/Torrent-Tracker-Registration-Monitor/tarball/v1.2.0"), release.UriTarball)
        Assert.AreSame(xml, release.Raw)
    End Sub

    ''' <summary>
    ''' The release dates (UTC in the JSON) must be parsed to the same point in time.
    ''' </summary>
    <TestMethod>
    Public Sub GitHubRelease_ParsesDates()

        Dim xml As XElement = GitHubJsonFixtures.ToXElement(GitHubJsonFixtures.GetReleaseJson("v1.2.0", assetCount:=0))

        Dim release As New GitHubRelease(xml)

        Assert.AreEqual(New Date(2026, 1, 15, 10, 0, 0, DateTimeKind.Utc), release.DateCreated.ToUniversalTime())
        Assert.AreEqual(New Date(2026, 1, 15, 12, 30, 45, DateTimeKind.Utc), release.DatePublished.ToUniversalTime())
    End Sub

    ''' <summary>
    ''' The release author and assets must be parsed.
    ''' </summary>
    <TestMethod>
    <DataRow(0)>
    <DataRow(1)>
    <DataRow(3)>
    Public Sub GitHubRelease_ParsesAuthorAndAssets(assetCount As Integer)

        Dim xml As XElement = GitHubJsonFixtures.ToXElement(GitHubJsonFixtures.GetReleaseJson("v1.2.0", assetCount))

        Dim release As New GitHubRelease(xml)

        Assert.IsNotNull(release.Author)
        Assert.AreEqual("ElektroStudios", release.Author.Name)
        Assert.HasCount(assetCount, release.Assets)
    End Sub

    ''' <summary>
    ''' The release version must be extracted from the tag name, ignoring prefixes and suffixes.
    ''' <para></para>
    ''' Regression test: tags with a "-suffix" (e.g. "1.0-beta", "v1.2.0-rc1") were not recognized.
    ''' </summary>
    <TestMethod>
    <DataRow("v1.2.0", "1.2.0")>
    <DataRow("V1.2.0", "1.2.0")>
    <DataRow("1.2.0", "1.2.0")>
    <DataRow("v1.0", "1.0")>
    <DataRow("1.1.13", "1.1.13")>
    <DataRow("v1.2.3.4", "1.2.3.4")>
    <DataRow("1.0-alpha", "1.0")>
    <DataRow("1.0-beta", "1.0")>
    <DataRow("v1.2.0-rc1", "1.2.0")>
    <DataRow("release-2.3.4", "2.3.4")>
    <DataRow("TTRM v1.2.0 (final)", "1.2.0")>
    Public Sub GitHubRelease_Version_IsExtractedFromTagName(tagName As String, expectedVersion As String)

        Dim release As GitHubRelease = GitHubModelTests.CreateRelease(tagName)

        Assert.AreEqual(Version.Parse(expectedVersion), release.Version)
    End Sub

    ''' <summary>
    ''' Tag names without a recognizable version must return <see langword="Nothing"/>.
    ''' </summary>
    <TestMethod>
    <DataRow("")>
    <DataRow("latest")>
    <DataRow("v1")>
    Public Sub GitHubRelease_Version_UnrecognizedTagName_ReturnsNothing(tagName As String)

        Dim release As GitHubRelease = GitHubModelTests.CreateRelease(tagName)

        Assert.IsNull(release.Version)
    End Sub

    ''' <summary>
    ''' A newer release version must be detected as an update (this is the comparison performed by the update checker).
    ''' </summary>
    <TestMethod>
    <DataRow("v1.2.1", "1.2.0", True)>
    <DataRow("v1.3.0-beta", "1.2.0", True)>
    <DataRow("v1.2.0", "1.2.0", False)>
    <DataRow("v1.1.13", "1.2.0", False)>
    <DataRow("latest", "1.2.0", False)>
    Public Sub GitHubRelease_Version_UpdateComparison(tagName As String, currentVersion As String, expectedIsUpdate As Boolean)

        Dim release As GitHubRelease = GitHubModelTests.CreateRelease(tagName)

        Assert.AreEqual(expectedIsUpdate, release.Version > Version.Parse(currentVersion))
    End Sub

    ''' <summary>
    ''' The equality operators must compare releases by Id.
    ''' </summary>
    <TestMethod>
    Public Sub GitHubRelease_EqualityOperators_CompareById()

        Dim release1 As GitHubRelease = GitHubModelTests.CreateRelease("v1.0")
        Dim release2 As GitHubRelease = GitHubModelTests.CreateRelease("v2.0") ' Same Id in fixture.

        Assert.IsTrue(release1 = release2)
        Assert.IsFalse(release1 <> release2)
    End Sub

    ''' <summary>
    ''' <see cref="GitHubRelease.ToString"/> must include the main properties.
    ''' </summary>
    <TestMethod>
    Public Sub GitHubRelease_ToString_IncludesMainProperties()

        Dim value As String = GitHubModelTests.CreateRelease("v1.2.0").ToString()

        Assert.StartsWith("{", value, StringComparison.Ordinal)
        Assert.EndsWith("}", value, StringComparison.Ordinal)
        Assert.Contains("Id=999", value, StringComparison.Ordinal)
        Assert.Contains("TagName=v1.2.0", value, StringComparison.Ordinal)
        Assert.Contains("IsPreRelease=True", value, StringComparison.Ordinal)
    End Sub

#End Region

#Region " GitHubAuthor "

    ''' <summary>
    ''' An author JSON must be parsed into the corresponding properties.
    ''' </summary>
    <TestMethod>
    Public Sub GitHubAuthor_ParsesProperties()

        Dim author As New GitHubAuthor(GitHubJsonFixtures.ToXElement(GitHubJsonFixtures.AuthorJson))

        Assert.AreEqual("12345678", author.Id)
        Assert.AreEqual("ElektroStudios", author.Name)
        Assert.AreEqual("User", author.Type)
        Assert.IsFalse(author.IsSiteAdministrator)
        Assert.AreEqual(String.Empty, author.GravatarId)
        Assert.AreEqual(New Uri("https://github.com/ElektroStudios"), author.UriHtml)
        Assert.AreEqual(New Uri("https://api.github.com/users/ElektroStudios/repos"), author.UriRepositories)
        Assert.Contains("Name=ElektroStudios", author.ToString(), StringComparison.Ordinal)
    End Sub

#End Region

#Region " GitHubAsset "

    ''' <summary>
    ''' An asset JSON must be parsed into the corresponding properties.
    ''' </summary>
    <TestMethod>
    Public Sub GitHubAsset_ParsesProperties()

        Dim asset As New GitHubAsset(GitHubJsonFixtures.ToXElement(GitHubJsonFixtures.GetAssetJson()))

        Assert.AreEqual("111", asset.Id)
        Assert.AreEqual("TTRM.zip", asset.Name)
        Assert.AreEqual("uploaded", asset.State)
        Assert.AreEqual(1048576L, asset.Size)
        Assert.AreEqual(42, asset.DownloadCount)
        Assert.AreEqual("application/x-zip-compressed", asset.ContentType.MediaType)
        Assert.AreEqual(New Date(2026, 1, 15, 10, 20, 30, DateTimeKind.Utc), asset.DateCreated.ToUniversalTime())
        Assert.AreEqual(New Uri("https://github.com/ElektroStudios/Torrent-Tracker-Registration-Monitor/releases/download/v1.2.0/TTRM.zip"), asset.UriDownload)
        Assert.AreEqual("ElektroStudios", asset.Uploader.Name)
    End Sub

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Creates a <see cref="GitHubRelease"/> from the fixture JSON, using the specified tag name.
    ''' </summary>
    '''
    ''' <param name="tagName">
    ''' The release tag name.
    ''' </param>
    '''
    ''' <returns>
    ''' The resulting <see cref="GitHubRelease"/>.
    ''' </returns>
    Private Shared Function CreateRelease(tagName As String) As GitHubRelease

        Return New GitHubRelease(GitHubJsonFixtures.ToXElement(GitHubJsonFixtures.GetReleaseJson(tagName, assetCount:=0)))
    End Function

#End Region

End Class
