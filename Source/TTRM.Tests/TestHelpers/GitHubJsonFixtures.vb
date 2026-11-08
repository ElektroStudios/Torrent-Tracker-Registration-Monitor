#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Runtime.Serialization.Json
Imports System.Text
Imports System.Xml
Imports System.Xml.Linq

#End Region

''' <summary>
''' Provides GitHub REST API JSON fixtures,
''' converted to <see cref="XElement"/> exactly as <c>UtilGitHub</c> does.
''' </summary>
Friend Module GitHubJsonFixtures

#Region " Fields "

    ''' <summary>
    ''' JSON of a GitHub user (release author / asset uploader).
    ''' </summary>
    Friend Const AuthorJson As String = "{
        ""login"": ""ElektroStudios"",
        ""id"": 12345678,
        ""gravatar_id"": """",
        ""url"": ""https://api.github.com/users/ElektroStudios"",
        ""avatar_url"": ""https://avatars.githubusercontent.com/u/12345678?v=4"",
        ""html_url"": ""https://github.com/ElektroStudios"",
        ""subscriptions_url"": ""https://api.github.com/users/ElektroStudios/subscriptions"",
        ""organizations_url"": ""https://api.github.com/users/ElektroStudios/orgs"",
        ""repos_url"": ""https://api.github.com/users/ElektroStudios/repos"",
        ""received_events_url"": ""https://api.github.com/users/ElektroStudios/received_events"",
        ""type"": ""User"",
        ""site_admin"": false
    }"

    ''' <summary>
    ''' JSON of a GitHub release asset. The <c>{0}</c> placeholder is replaced by the uploader JSON.
    ''' </summary>
    Private Const AssetJsonFormat As String = "{{
        ""url"": ""https://api.github.com/repos/ElektroStudios/Torrent-Tracker-Registration-Monitor/releases/assets/111"",
        ""id"": 111,
        ""name"": ""TTRM.zip"",
        ""label"": """",
        ""uploader"": {0},
        ""content_type"": ""application/x-zip-compressed"",
        ""state"": ""uploaded"",
        ""size"": 1048576,
        ""download_count"": 42,
        ""created_at"": ""2026-01-15T10:20:30Z"",
        ""updated_at"": ""2026-01-15T10:25:00Z"",
        ""browser_download_url"": ""https://github.com/ElektroStudios/Torrent-Tracker-Registration-Monitor/releases/download/v1.2.0/TTRM.zip""
    }}"

    ''' <summary>
    ''' JSON of a GitHub release.
    ''' The <c>{0}</c> placeholder is replaced by the tag name,
    ''' <c>{1}</c> by the author JSON and <c>{2}</c> by the assets JSON array content.
    ''' </summary>
    Private Const ReleaseJsonFormat As String = "{{
        ""url"": ""https://api.github.com/repos/ElektroStudios/Torrent-Tracker-Registration-Monitor/releases/999"",
        ""assets_url"": ""https://api.github.com/repos/ElektroStudios/Torrent-Tracker-Registration-Monitor/releases/999/assets"",
        ""html_url"": ""https://github.com/ElektroStudios/Torrent-Tracker-Registration-Monitor/releases/tag/{0}"",
        ""id"": 999,
        ""author"": {1},
        ""tag_name"": ""{0}"",
        ""target_commitish"": ""main"",
        ""name"": ""TTRM {0}"",
        ""draft"": false,
        ""prerelease"": true,
        ""created_at"": ""2026-01-15T10:00:00Z"",
        ""published_at"": ""2026-01-15T12:30:45Z"",
        ""assets"": [{2}],
        ""tarball_url"": ""https://api.github.com/repos/ElektroStudios/Torrent-Tracker-Registration-Monitor/tarball/{0}"",
        ""zipball_url"": ""https://api.github.com/repos/ElektroStudios/Torrent-Tracker-Registration-Monitor/zipball/{0}"",
        ""body"": ""## Changes\n- Something new.""
    }}"

#End Region

#Region " Methods "

    ''' <summary>
    ''' Gets the JSON of a GitHub release asset.
    ''' </summary>
    '''
    ''' <returns>
    ''' The asset JSON.
    ''' </returns>
    Friend Function GetAssetJson() As String

        Return String.Format(Globalization.CultureInfo.InvariantCulture, GitHubJsonFixtures.AssetJsonFormat, GitHubJsonFixtures.AuthorJson)
    End Function

    ''' <summary>
    ''' Gets the JSON of a GitHub release.
    ''' </summary>
    '''
    ''' <param name="tagName">
    ''' The release tag name.
    ''' </param>
    '''
    ''' <param name="assetCount">
    ''' The number of assets to include in the release.
    ''' </param>
    '''
    ''' <returns>
    ''' The release JSON.
    ''' </returns>
    Friend Function GetReleaseJson(tagName As String, assetCount As Integer) As String

        ArgumentNullException.ThrowIfNull(tagName)
        ArgumentOutOfRangeException.ThrowIfNegative(assetCount)

        Dim assets As String = String.Join(",", Enumerable.Repeat(GitHubJsonFixtures.GetAssetJson(), assetCount))
        Return String.Format(Globalization.CultureInfo.InvariantCulture, GitHubJsonFixtures.ReleaseJsonFormat, tagName, GitHubJsonFixtures.AuthorJson, assets)
    End Function

    ''' <summary>
    ''' Converts the specified JSON to an <see cref="XElement"/>,
    ''' using the same <see cref="JsonReaderWriterFactory"/> approach used by <c>UtilGitHub</c>.
    ''' </summary>
    '''
    ''' <param name="json">
    ''' The JSON string.
    ''' </param>
    '''
    ''' <returns>
    ''' The resulting <see cref="XElement"/>.
    ''' </returns>
    Friend Function ToXElement(json As String) As XElement

        ArgumentException.ThrowIfNullOrWhiteSpace(json)

        Dim bytes As Byte() = Encoding.UTF8.GetBytes(json)
        Using xmlReader As XmlDictionaryReader = JsonReaderWriterFactory.CreateJsonReader(bytes, New XmlDictionaryReaderQuotas())
            Return XElement.Load(xmlReader)
        End Using
    End Function

#End Region

End Module
