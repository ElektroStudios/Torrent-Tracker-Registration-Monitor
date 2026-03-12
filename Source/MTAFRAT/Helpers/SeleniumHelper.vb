#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO

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
                .ArgumentList.Add($"""{AppGlobals.SeleniumCachePath}""")

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
        Dim versionDirs As String() = Directory.GetDirectories(basePath)
        If versionDirs.Length = 0 Then
            Throw New DirectoryNotFoundException($"No chrome directory found in cache path: {basePath}")
        End If

        Dim latestDir As String = versionDirs.OrderByDescending(Function(d) New Version(Path.GetFileName(d))).First()
        Return latestDir
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

        Dim versionDirs As String() = Directory.GetDirectories(basePath)
        If versionDirs.Length = 0 Then
            Throw New DirectoryNotFoundException($"No chromedriver directory found in cache path: {basePath}")
        End If

        Dim latestDir As String = versionDirs.OrderByDescending(Function(d) New Version(Path.GetFileName(d))).First()
        Return latestDir
    End Function

#End Region

End Module
