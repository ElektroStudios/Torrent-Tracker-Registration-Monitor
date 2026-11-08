#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO
Imports System.Threading

#End Region

''' <summary>
''' Provides helper members related to selenium / selenium-manager.exe operations.
''' </summary>
Friend Module SeleniumHelper

#Region " Restricted Methods "

    ''' <summary>
    ''' Initializes Selenium environment, and downloads Chrome if does not exists in cache path.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub InitializeSelenium()
        Dim splashScreen As MainSplashScreen = AppGlobals.MainSplashScreenInstance
        If splashScreen.IsHandleCreated Then
            splashScreen.Invoke(Sub() splashScreen.Label_StatusLoad.Text = "Initializing Selenium (downloading Chrome)...")
        End If

        ' Set env var for this process (MUST be done before any Selenium usage).
        Environment.SetEnvironmentVariable("SE_CACHE_PATH", AppGlobals.SeleniumCachePath, EnvironmentVariableTarget.Process)

        ' Force Chrome download into Selenium cache path.
        Using p As New Process
            With p.StartInfo
                .FileName = AppGlobals.SeleniumManagerExecPath

                .ArgumentList.Add("--force-browser-download")
                .ArgumentList.Add("--browser")
                .ArgumentList.Add("chrome")

                .ArgumentList.Add("--driver")
                .ArgumentList.Add("chromedriver")

                .ArgumentList.Add("--cache-path")
                ' Note: 'ArgumentList' already quotes and escapes each argument as needed;
                ' adding literal quotes here would make them part of the path value.
                .ArgumentList.Add(AppGlobals.SeleniumCachePath)

                .WindowStyle = ProcessWindowStyle.Hidden
            End With

            p.Start()
            p.WaitForExit(TimeSpan.FromMinutes(5))
        End Using

    End Sub

    ''' <summary>
    ''' Clears the contents of the Selenium-manager cache.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub ClearCache()

        ' Ensures cache path is correct if it gets modified for some unexpected reason.
        Environment.SetEnvironmentVariable("SE_CACHE_PATH", AppGlobals.SeleniumCachePath, EnvironmentVariableTarget.Process)

        Using p As New Process
            With p.StartInfo
                .FileName = AppGlobals.SeleniumManagerExecPath
                .ArgumentList.Add("--driver")
                .ArgumentList.Add("chromedriver")

                .ArgumentList.Add("--clear-cache")
                .ArgumentList.Add("--offline")

                .WindowStyle = ProcessWindowStyle.Hidden
            End With

            p.Start()
            p.WaitForExit(TimeSpan.FromMinutes(5))
        End Using

    End Sub

    ''' <summary>
    ''' Kills all <c>chromedriver.exe</c> processes that were launched by the current application process.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub KillChildrenChromeDriverAndChromeProcesses()

        Dim childProcesses As List(Of Process) = GetChildProcesses(Environment.ProcessId)

        Dim childChromeDriverProcesses As List(Of Process) =
            (From p As Process In childProcesses
             Where Not p?.HasExited AndAlso p.ProcessName.Equals("chromedriver", StringComparison.InvariantCultureIgnoreCase)
            ).ToList()

        For Each p As Process In childChromeDriverProcesses
            Try
                p.Kill(entireProcessTree:=True)
            Catch ' Ignore. Process can no longer exists.
            End Try
        Next

    End Sub

    ''' <summary>
    ''' Returns the directory path of the latest cached Chrome browser version under the Selenium cache path (SE_CACHE_PATH).
    ''' </summary>
    ''' 
    ''' <returns>
    ''' The full path to the latest Chrome version directory found under the Selenium cache path.
    ''' </returns>
    ''' 
    ''' <exception cref="DirectoryNotFoundException">
    ''' Thrown when no Chrome directory is found in the cache.
    ''' </exception>
    <DebuggerStepThrough>
    Friend Function GetLatestChromeDirPath() As String

        Dim basePath As String = Path.Combine(AppGlobals.SeleniumCachePath, "chrome", "win64")
        Return SeleniumHelper.GetLatestVersionDirectory(basePath)
    End Function

    ''' <summary>
    ''' Returns the directory path of the latest cached ChromeDriver version under the Selenium cache path (SE_CACHE_PATH).
    ''' </summary>
    ''' 
    ''' <returns>
    ''' The full path to the latest ChromeDriver version directory found under the Selenium cache path.
    ''' </returns>
    ''' 
    ''' <exception cref="DirectoryNotFoundException">
    ''' Thrown when no ChromeDriver directory is found in the cache.
    ''' </exception>
    <DebuggerStepThrough>
    Friend Function GetLatestChromedriverPath() As String

        Dim basePath As String = Path.Combine(AppGlobals.SeleniumCachePath, "chromedriver", "win64")

        Return SeleniumHelper.GetLatestVersionDirectory(basePath)
    End Function

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Gets the full path of the subdirectory with the highest version name 
    ''' (e.g., "131.0.6778.204") inside the specified directory.
    ''' <para></para>
    ''' Subdirectories whose name is not a valid version (e.g., temporary or leftover folders) are ignored.
    ''' </summary>
    ''' 
    ''' <param name="basePath">
    ''' The directory that contains the version subdirectories.
    ''' </param>
    ''' 
    ''' <returns>
    ''' The full path of the subdirectory with the highest version name.
    ''' </returns>
    ''' 
    ''' <exception cref="DirectoryNotFoundException">
    ''' Thrown when <paramref name="basePath"/> does not exist, or it does not contain any version subdirectory.
    ''' </exception>
    <DebuggerStepThrough>
    Private Function GetLatestVersionDirectory(basePath As String) As String

        If Not Directory.Exists(basePath) Then
            Throw New DirectoryNotFoundException($"Directory not found: {basePath}")
        End If

        Dim latestDir As String = Nothing
        Dim latestVersion As Version = Nothing
        For Each dirPath As String In Directory.EnumerateDirectories(basePath)
            Thread.CurrentThread.Join(0)

            Dim ver As Version = Nothing
            If Version.TryParse(Path.GetFileName(dirPath), ver) AndAlso
               (latestVersion Is Nothing OrElse ver > latestVersion) Then
                latestVersion = ver
                latestDir = dirPath
            End If
        Next

        If latestDir Is Nothing Then
            Throw New DirectoryNotFoundException($"No version directory found in cache path: {basePath}")
        End If
        Return latestDir
    End Function

#End Region

End Module
