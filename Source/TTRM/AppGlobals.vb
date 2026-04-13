#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Resources
Imports System.Runtime.Loader

#End Region

''' <summary>
''' Provides global constants, read-only fields and properties for this application.
''' </summary>
Public Module AppGlobals

#Region " Fields "

    ''' <summary>
    ''' The official GitHub repository page for this application.
    ''' </summary>
    Public Const GitHubUrl As String =
        "https://github.com/ElektroStudios/Torrent-Tracker-Registration-Monitor/"

    ''' <summary>
    ''' Duration in milliseconds that the splash screen is displayed.
    ''' </summary>
    Public Const SplashScreenTime As Integer = 2500

    ''' <summary>
    ''' Default time interval between automatic plugin executions.
    ''' </summary>
    Friend AutomaticPluginRunInterval As TimeSpan = TimeSpan.FromHours(1)

    ''' <summary>
    ''' The application title and version, formatted as "ProductName (AssemblyName) vMajor.Minor".
    ''' </summary>
    Public ReadOnly ApplicationTitleAndVersion As String =
        $"{My.Application.Info.ProductName} ({My.Application.Info.AssemblyName}) " &
        $"v{My.Application.Info.Version.Major}.{My.Application.Info.Version.Minor}.{My.Application.Info.Version.Build}"

    ''' <summary>
    ''' Path to the plugins directory relative to the application's startup path.
    ''' </summary>
    Public ReadOnly PluginsDirectoryPath As String =
        Path.Combine(My.Application.Info.DirectoryPath, "plugins")

    ''' <summary>
    ''' Directory path where to save the Jot cache data.
    ''' </summary>
    Public ReadOnly JotCachePath As String =
        Path.Combine(My.Application.Info.DirectoryPath, "cache\jot")

    ''' <summary>
    ''' Directory path where to save the ChromeDriver cache data.
    ''' </summary>
    Public ReadOnly ChromeUserCachePath As String =
        Path.Combine(My.Application.Info.DirectoryPath, "cache\plugins")

    ''' <summary>
    ''' Directory path where to save the Selenium cache data.
    ''' </summary>
    Public ReadOnly SeleniumCachePath As String =
        Path.Combine(My.Application.Info.DirectoryPath, "cache\selenium")

    ''' <summary>
    ''' Directory path where the <c>selenium-manager.exe</c> file is located.
    ''' </summary>
    Public ReadOnly SeleniumManagerExecPath As String =
        Path.Combine(My.Application.Info.DirectoryPath, "runtimes\win\native\selenium-manager.exe")

    ''' <summary>
    ''' List of language items for populating language selection ComboBox.
    ''' </summary>
    Public ReadOnly LanguageComboBoxItems As New List(Of LanguageComboBoxItem) From {
        New LanguageComboBoxItem(New CultureInfo("en")),
        New LanguageComboBoxItem(New CultureInfo("es"))
    }

    ''' <returns>
    ''' A default string stored in a control's <see cref="Control.Tag"/> property,
    ''' used for localization purposes to determine whether the text has been modified or remains unchanged.
    ''' </returns>
    Public Const ControlInitialTextTag As String = "Initial text is set."

    ''' <returns>
    ''' A read-only collection of <see cref="DynamicPlugin"/> objects representign each loaded plugin in the application.
    ''' </returns>
    Friend LoadedDynamicPlugins As New ReadOnlyCollection(Of DynamicPlugin)(Enumerable.Empty(Of DynamicPlugin).ToList())

    ''' <summary>
    ''' Gets the resource manager that provides access to localized string resources for this assembly.
    ''' </summary>
    Friend StringsResourceManager As New ResourceManager($"{My.Application.Info.AssemblyName}.Strings", GetType(DynamicPlugin).Assembly)

    ''' <summary>
    ''' The <see cref="AssemblyLoadContext"/> instance used to load and isolate dynamically compiled plugin assemblies.
    ''' </summary>
    Friend PluginLoadContext As New PluginLoadContext()

#End Region

#Region " Properties "

    ''' <summary>
    ''' Gets the executing instance of the <see cref="MainForm"/>.
    ''' </summary>
    Public ReadOnly Property MainFormInstance As MainForm
        <DebuggerStepThrough>
        Get
            If AppGlobals._mainForm Is Nothing Then
                AppGlobals._mainForm = Application.OpenForms().OfType(Of MainForm)().Single()
            End If
            Return AppGlobals._mainForm
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="MainFormInstance"/> property. )
    ''' <para></para>
    ''' The executing instance of the <see cref="MainForm"/>.
    ''' </summary>
    Private _mainForm As TTRM.MainForm

    ''' <summary>
    ''' Gets the executing instance of the <see cref="MainSplashScreen"/>.
    ''' </summary>
    Public ReadOnly Property MainSplashScreenInstance As MainSplashScreen
        <DebuggerStepThrough>
        Get
            If AppGlobals._mainSplashScreen Is Nothing Then
                AppGlobals._mainSplashScreen = Application.OpenForms().OfType(Of MainSplashScreen)().SingleOrDefault()
            End If
            Return AppGlobals._mainSplashScreen
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="MainSplashScreenInstance"/> property. )
    ''' <para></para>
    ''' The executing instance of the <see cref="MainSplashScreen"/>.
    ''' </summary>
    Private _mainSplashScreen As TTRM.MainSplashScreen

    ''' <summary>
    ''' Tag identifier associated with a context menu item for the login page URL of a plugin.
    ''' </summary>
    Friend Const TagLoginUrl As String = "Login URL"

    ''' <summary>
    ''' Tag identifier associated with a context menu item for the registration page URL of a plugin.
    ''' </summary>
    Friend Const TagRegistrationUrl As String = "Registration URL"

    ''' <summary>
    ''' Tag identifier associated with a context menu item for the application page URL of a plugin.
    ''' </summary>
    Friend Const TagApplicationUrl As String = "Application URL"

#End Region

End Module
