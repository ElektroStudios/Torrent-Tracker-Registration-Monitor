#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO

#End Region

''' <summary>
''' Tests for the file-system members of <see cref="SeleniumHelper"/> module
''' that locate the latest Chrome / ChromeDriver versions inside the Selenium cache.
''' <para></para>
''' These tests create (and delete afterwards) fake version folders inside <see cref="AppGlobals.SeleniumCachePath"/>
''' of the test output directory.
''' </summary>
<TestClass>
Public NotInheritable Class SeleniumHelperTests

#Region " Fields "

    ''' <summary>
    ''' The Selenium cache base paths that were created by the current test, to delete them afterwards.
    ''' </summary>
    Private ReadOnly createdBasePaths As New List(Of String)

#End Region

#Region " Test Cleanup "

    ''' <summary>
    ''' Deletes the directories created by the current test.
    ''' </summary>
    <TestCleanup>
    Public Sub TestCleanup()

        For Each basePath As String In Me.createdBasePaths
            If Directory.Exists(basePath) Then
                Directory.Delete(basePath, recursive:=True)
            End If
        Next
        Me.createdBasePaths.Clear()
    End Sub

#End Region

#Region " Tests "

    ''' <summary>
    ''' The latest Chrome directory must be selected by numeric version, not alphabetically
    ''' (e.g. "120.0.6099.109" is newer than "99.0.4844.51").
    ''' </summary>
    <TestMethod>
    Public Sub GetLatestChromeDirPath_ReturnsHighestNumericVersion()

        Dim basePath As String = Me.CreateVersionDirectories("chrome", "99.0.4844.51", "120.0.6099.109", "120.0.6099.71", "9.0.1")

        Dim result As String = SeleniumHelper.GetLatestChromeDirPath()

        Assert.AreEqual(Path.Combine(basePath, "120.0.6099.109"), result)
    End Sub

    ''' <summary>
    ''' The latest ChromeDriver directory must be selected by numeric version, not alphabetically.
    ''' </summary>
    <TestMethod>
    Public Sub GetLatestChromedriverPath_ReturnsHighestNumericVersion()

        Dim basePath As String = Me.CreateVersionDirectories("chromedriver", "131.0.6778.85", "131.0.6778.204", "13.1.0.0")

        Dim result As String = SeleniumHelper.GetLatestChromedriverPath()

        Assert.AreEqual(Path.Combine(basePath, "131.0.6778.204"), result)
    End Sub

    ''' <summary>
    ''' Regression test: folders whose name is not a version (e.g. temporary or leftover folders)
    ''' must be ignored instead of throwing <see cref="FormatException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub GetLatestChromeDirPath_IgnoresNonVersionDirectories()

        Dim basePath As String = Me.CreateVersionDirectories("chrome", "120.0.6099.109", "temp", "120.0.6099.109.old", "latest")

        Dim result As String = SeleniumHelper.GetLatestChromeDirPath()

        Assert.AreEqual(Path.Combine(basePath, "120.0.6099.109"), result)
    End Sub

    ''' <summary>
    ''' A cache directory that contains only non-version folders must throw <see cref="DirectoryNotFoundException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub GetLatestChromedriverPath_OnlyNonVersionDirectories_Throws()

        Me.CreateVersionDirectories("chromedriver", "temp", "backup")

        Assert.ThrowsExactly(Of DirectoryNotFoundException)(Sub() SeleniumHelper.GetLatestChromedriverPath())
    End Sub

    ''' <summary>
    ''' An empty Chrome cache directory must throw <see cref="DirectoryNotFoundException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub GetLatestChromeDirPath_NoVersionDirectories_Throws()

        Me.CreateVersionDirectories("chrome")

        Assert.ThrowsExactly(Of DirectoryNotFoundException)(Sub() SeleniumHelper.GetLatestChromeDirPath())
    End Sub

    ''' <summary>
    ''' An empty ChromeDriver cache directory must throw <see cref="DirectoryNotFoundException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub GetLatestChromedriverPath_NoVersionDirectories_Throws()

        Me.CreateVersionDirectories("chromedriver")

        Assert.ThrowsExactly(Of DirectoryNotFoundException)(Sub() SeleniumHelper.GetLatestChromedriverPath())
    End Sub

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Creates the specified version directories inside the "{product}\win64" folder of the Selenium cache.
    ''' </summary>
    '''
    ''' <param name="product">
    ''' The product folder name ("chrome" or "chromedriver").
    ''' </param>
    '''
    ''' <param name="versions">
    ''' The version directory names to create.
    ''' </param>
    '''
    ''' <returns>
    ''' The full path of the "{product}\win64" folder.
    ''' </returns>
    '''
    ''' <exception cref="InvalidOperationException">
    ''' Thrown when the folder already exists (to avoid deleting real cache data).
    ''' </exception>
    Private Function CreateVersionDirectories(product As String, ParamArray versions As String()) As String

        Dim productPath As String = Path.Combine(AppGlobals.SeleniumCachePath, product)
        Dim basePath As String = Path.Combine(productPath, "win64")
        If Directory.Exists(productPath) Then
            Throw New InvalidOperationException($"The directory already exists and will not be modified: {productPath}")
        End If

        Me.createdBasePaths.Add(productPath)
        Directory.CreateDirectory(basePath)
        For Each version As String In versions
            Directory.CreateDirectory(Path.Combine(basePath, version))
        Next

        Return basePath
    End Function

#End Region

End Class
