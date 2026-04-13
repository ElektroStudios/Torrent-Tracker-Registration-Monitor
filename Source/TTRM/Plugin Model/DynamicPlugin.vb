#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Reflection
Imports System.Text.Json

Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Emit
Imports Microsoft.CodeAnalysis.VisualBasic

Imports DarkUI.Controls

Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome

#End Region

''' <summary>
''' Represents a dynamically compiled plugin.
''' </summary>
Public Class DynamicPlugin : Inherits PluginBase

#Region " Restricted Fields "

    ''' <summary>
    ''' The configuration options for JSON serialization and deserialization.
    ''' </summary>
    Private Shared ReadOnly JsonOptions As New JsonSerializerOptions With {
            .PropertyNameCaseInsensitive = True
        }

    ''' <summary>
    ''' The Chrome's plugin cache path associated to this plugin.
    ''' </summary>
    Protected Friend PluginCachePath As String

    ''' <summary>
    ''' The base name used to dynamically assign control names associated to this plugin.
    ''' </summary>
    Protected Friend UIMemberBaseName As String

    ''' <summary>
    ''' The dynamic name assigned to the plugin button in the main form navigation pane.
    ''' <para></para>
    ''' Used to create and identify the control at runtime.
    ''' </summary>
    Protected Friend ReadOnly ButtonPaneName As String

    ''' <summary>
    ''' The dynamic name assigned to the tabpage that hosts the plugin's section panel.
    ''' <para></para>
    ''' Used to create and identify the control at runtime.
    ''' </summary>
    Protected Friend ReadOnly TabPageName As String

    ''' <summary>
    ''' The dynamic name assigned to the section panel that hosts the plugin controls.
    ''' <para></para>
    ''' Used to create and identify the control at runtime.
    ''' </summary>
    Protected Friend ReadOnly SectionPanelName As String

    ''' <summary>
    ''' The dynamic name assigned to the plugin’s "Description" textbox in the plugin's section panel.
    ''' <para></para>
    ''' Used to create and identify the control at runtime.
    ''' </summary>
    Protected Friend ReadOnly DescriptionTextBoxName As String

    ''' <summary>
    ''' The dynamic name assigned to the plugin’s "Execute Plugin" button in the plugin's section panel.
    ''' <para></para>
    ''' Used to create and identify the control at runtime.
    ''' </summary>
    Protected Friend ReadOnly ButtonRunPluginName As String

    ''' <summary>
    ''' The dynamic name assigned to the plugin’s "Open Website" button in the plugin's section panel.
    ''' <para></para>
    ''' Used to create and identify the control at runtime.
    ''' </summary>
    Protected Friend ReadOnly ButtonOpenWebsiteName As String

    ''' <summary>
    ''' The dynamic name assigned to the plugin’s "Clear cache" button in the plugin's section panel.
    ''' <para></para>
    ''' Used to create and identify the control at runtime.
    ''' </summary>
    Protected Friend ReadOnly ButtonClearCacheName As String

    ''' <summary>
    ''' The dynamic name assigned to the textbox used for log messages in the plugin's section panel.
    ''' <para></para>
    ''' Used to create and identify the control at runtime.
    ''' </summary>
    Protected Friend ReadOnly LogTextBoxName As String

    ''' <summary>
    ''' The dynamic name assigned to the label used for displaying the last status message in the plugin's section panel.
    ''' <para></para>
    ''' Used to create and identify the control at runtime.
    ''' </summary>
    Protected Friend ReadOnly StatusLabelName As String

#End Region

#Region " Public Properties "

    ''' <summary>
    ''' Gets a value that determines whether this plugin is actually being executed.
    ''' </summary>
    Public ReadOnly Property IsRunning As Boolean
        Get
            Return Me._isRunning
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="IsRunning"/> property. )
    ''' <para></para>
    ''' A value that determines whether this plugin is actually being executed.
    ''' </summary>
    Private _isRunning As Boolean

    ''' <summary>
    ''' Gets or sets the VB.NET source code associated with this plugin.
    ''' </summary>
    Public Property VbCode As String

    ''' <summary>
    ''' Gets the compilation options used by the Roslyn Visual Basic compiler when dynamically compiling the plugin code.
    ''' </summary>
    Public Shared ReadOnly Property CompileOptions As New VisualBasicCompilationOptions(
        OutputKind.DynamicallyLinkedLibrary,
        platform:=Microsoft.CodeAnalysis.Platform.AnyCpu,
        optimizationLevel:=OptimizationLevel.Release,
        generalDiagnosticOption:=ReportDiagnostic.Default,
        optionStrict:=OptionStrict.Off,
        optionInfer:=True,
        optionExplicit:=False,
        checkOverflow:=True,
        concurrentBuild:=True,
        deterministic:=False)

    ''' <summary>
    ''' Gets all metadata references used for dynamic plugin compilation.
    ''' <para></para>
    ''' This includes the executing assembly, all loaded assemblies in the application domain, and <see cref="OpenQA.Selenium"/>.
    ''' </summary>
    Public Shared ReadOnly Property CompilationReferences As ReadOnlyCollection(Of MetadataReference) =
        DynamicPlugin.InitializeCompilationReferences()

    ''' <summary>
    ''' Gets the -dynamically created- <see cref="DarkButtonImageAllignFix"/> 
    ''' associated with this plugin in the main form navigation pane.
    ''' </summary>
    Public ReadOnly Property ButtonPane As DarkButtonImageAllignFix
        Get
            If Me._buttonPane Is Nothing Then
                Dim f As MainForm = AppGlobals.MainFormInstance
                Dim act As Action =
                    Sub()
                        Me._buttonPane =
                            f.DarkSectionPanel_Plugins.Controls.OfType(Of DarkButtonImageAllignFix).Where(
                                Function(bt As DarkButtonImageAllignFix) bt.Name.Equals(Me.ButtonPaneName)).SingleOrDefault()
                    End Sub

                If f.DarkSectionPanel_Plugins.InvokeRequired Then
                    f.DarkSectionPanel_Plugins.Invoke(Sub() act())
                Else
                    act()
                End If
            End If
            Return Me._buttonPane
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="ButtonPane"/> property. )
    ''' <para></para>
    ''' The -dynamically created- <see cref="DarkButtonImageAllignFix"/>
    ''' associated with this plugin in the main form navigation pane.
    ''' </summary>
    Private _buttonPane As DarkButtonImageAllignFix

    ''' <summary>
    ''' Gets the -dynamically created- <see cref="System.Windows.Forms.TabPage"/> 
    ''' associated with this plugin that hosts the plugin's section panel.
    ''' </summary>
    Public ReadOnly Property TabPage As TabPage
        Get
            If Me._tabPage Is Nothing Then
                Dim f As MainForm = AppGlobals.MainFormInstance
                Dim act As Action = Sub() Me._tabPage = f.TabControlNoBorder_Main.TabPages(Me.TabPageName)

                If f.InvokeRequired Then
                    f.Invoke(Sub() act())
                Else
                    act()
                End If
            End If
            Return Me._tabPage
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="TabPage"/> property. )
    ''' <para></para>
    ''' The -dynamically created- <see cref="System.Windows.Forms.TabPage"/> 
    ''' associated with this plugin that hosts the plugin's section panel.
    ''' </summary>
    Private _tabPage As TabPage

    ''' <summary>
    ''' Gets the -dynamically created- <see cref="DarkSectionPanel"/> 
    ''' associated with this plugin that hosts the plugin controls.
    ''' </summary>
    Public ReadOnly Property SectionPanel As DarkSectionPanel
        Get
            If Me._sectionPanel Is Nothing Then
                Dim f As MainForm = AppGlobals.MainFormInstance
                Dim act As Action =
                    Sub()
                        Me._sectionPanel =
                            Me.TabPage.Controls.OfType(Of DarkSectionPanel).Where(
                                Function(pan As DarkSectionPanel) pan.Name.Equals(Me.SectionPanelName)).SingleOrDefault()
                    End Sub

                If Me.TabPage.InvokeRequired Then
                    Me.TabPage.Invoke(Sub() act())
                Else
                    act()
                End If
            End If
            Return Me._sectionPanel
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="SectionPanel"/> property. )
    ''' <para></para>
    ''' The -dynamically created- <see cref="DarkSectionPanel"/> 
    ''' associated with this plugin that hosts the plugin controls.
    ''' </summary>
    Private _sectionPanel As DarkSectionPanel

    ''' <summary>
    ''' Gets the -dynamically created- "Description" <see cref="DarkTextBox"/> 
    ''' associated with this plugin in the plugin's section panel.
    ''' </summary>
    Public ReadOnly Property DescriptionTextBox As DarkTextBox
        Get
            If Me._descriptionTextBox Is Nothing Then
                Dim f As MainForm = AppGlobals.MainFormInstance
                Dim act As Action =
                    Sub()
                        Me._descriptionTextBox =
                            Me.SectionPanel.Controls.OfType(Of DarkTextBox).Where(
                                Function(bt As DarkTextBox) bt.Name.Equals(Me.DescriptionTextBoxName)).SingleOrDefault()
                    End Sub

                If Me.SectionPanel.InvokeRequired Then
                    Me.SectionPanel.Invoke(Sub() act())
                Else
                    act()
                End If
            End If
            Return Me._descriptionTextBox
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="DescriptionTextBox"/> property. )
    ''' <para></para>
    ''' The -dynamically created- "Description" <see cref="DarkTextBox"/> 
    ''' associated with this plugin in the plugin's section panel.
    ''' </summary>
    Private _descriptionTextBox As DarkTextBox

    ''' <summary>
    ''' Gets the -dynamically created- "Execute Plugin" <see cref="DarkButtonImageAllignFix"/> 
    ''' associated with this plugin in the plugin's section panel.
    ''' </summary>
    Public ReadOnly Property ButtonRunPlugin As DarkButtonImageAllignFix
        Get
            If Me._buttonRunPlugin Is Nothing Then
                Dim f As MainForm = AppGlobals.MainFormInstance
                Dim act As Action =
                    Sub()
                        Me._buttonRunPlugin =
                            Me.SectionPanel.Controls.OfType(Of DarkButtonImageAllignFix).Where(
                                Function(bt As DarkButtonImageAllignFix) bt.Name.Equals(Me.ButtonRunPluginName)).SingleOrDefault()
                    End Sub

                If Me.SectionPanel.InvokeRequired Then
                    Me.SectionPanel.Invoke(Sub() act())
                Else
                    act()
                End If
            End If
            Return Me._buttonRunPlugin
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="ButtonRunPlugin"/> property. )
    ''' <para></para>
    ''' The -dynamically created- "Execute Plugin" <see cref="DarkButtonImageAllignFix"/> 
    ''' associated with this plugin in the plugin's section panel.
    ''' </summary>
    Private _buttonRunPlugin As DarkButtonImageAllignFix

    ''' <summary>
    ''' Gets the -dynamically created- "Open Website" <see cref="DarkButtonImageAllignFix"/> 
    ''' associated with this plugin in the plugin's section panel.
    ''' </summary>
    Public ReadOnly Property ButtonOpenWebsite As DarkButtonImageAllignFix
        Get
            If Me._buttonOpenWebsite Is Nothing Then
                Dim f As MainForm = AppGlobals.MainFormInstance
                Dim act As Action =
                    Sub()
                        Me._buttonOpenWebsite =
                            Me.SectionPanel.Controls.OfType(Of DarkButtonImageAllignFix).Where(
                                Function(bt As DarkButtonImageAllignFix) bt.Name.Equals(Me.ButtonOpenWebsiteName)).SingleOrDefault()
                    End Sub

                If Me.SectionPanel.InvokeRequired Then
                    Me.SectionPanel.Invoke(Sub() act())
                Else
                    act()
                End If
            End If
            Return Me._buttonOpenWebsite
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="ButtonOpenWebsite"/> property. )
    ''' <para></para>
    ''' The -dynamically created- "Open Website" <see cref="DarkButtonImageAllignFix"/> 
    ''' associated with this plugin in the plugin's section panel.
    ''' </summary>
    Private _buttonOpenWebsite As DarkButtonImageAllignFix

    ''' <summary>
    ''' Gets the -dynamically created- "Clear Cache" <see cref="DarkButtonImageAllignFix"/> 
    ''' associated with this plugin in the plugin's section panel.
    ''' </summary>
    Public ReadOnly Property ButtonClearCache As DarkButtonImageAllignFix
        Get
            If Me._buttonClearCache Is Nothing Then
                Dim f As MainForm = AppGlobals.MainFormInstance
                Dim act As Action =
                    Sub()
                        Me._buttonClearCache =
                            Me.SectionPanel.Controls.OfType(Of DarkButtonImageAllignFix).Where(
                                Function(bt As DarkButtonImageAllignFix) bt.Name.Equals(Me.ButtonClearCacheName)).SingleOrDefault()
                    End Sub

                If Me.SectionPanel.InvokeRequired Then
                    Me.SectionPanel.Invoke(Sub() act())
                Else
                    act()
                End If
            End If
            Return Me._buttonClearCache
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="ButtonClearCache"/> property. )
    ''' <para></para>
    ''' The -dynamically created- "Clear Cache" <see cref="DarkButtonImageAllignFix"/> 
    ''' associated with this plugin in the plugin's section panel.
    ''' </summary>
    Private _buttonClearCache As DarkButtonImageAllignFix

    ''' <summary>
    ''' Gets the -dynamically created- <see cref="DarkTextBox"/> 
    ''' associated with this plugin to log messages in the plugin's section panel.
    ''' </summary>
    Public ReadOnly Property LogTextBox As DarkTextBox
        Get
            If Me._logTextBox Is Nothing Then
                Dim f As MainForm = AppGlobals.MainFormInstance
                Dim act As Action =
                    Sub()
                        Me._logTextBox =
                            Me.SectionPanel.Controls.OfType(Of DarkTextBox).Where(
                                Function(tb As DarkTextBox) tb.Name.Equals(Me.LogTextBoxName)).SingleOrDefault()
                    End Sub

                If Me.SectionPanel.InvokeRequired Then
                    Me.SectionPanel.Invoke(Sub() act())
                Else
                    act()
                End If
            End If
            Return Me._LogTextBox
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="LogTextBox"/> property. )
    ''' <para></para>
    ''' The -dynamically created- <see cref="DarkTextBox"/> 
    ''' associated with this plugin to log messages in the plugin's section panel.
    ''' </summary>
    Private _logTextBox As DarkTextBox

    ''' <summary>
    ''' Gets the -dynamically created- <see cref="Label"/> 
    ''' associated with this plugin for displaying the last status message 
    ''' in the plugin's section panel.
    ''' </summary>
    Public ReadOnly Property StatusLabel As Label
        Get
            If Me._statusLabel Is Nothing Then
                Dim f As MainForm = AppGlobals.MainFormInstance
                Dim act As Action =
                    Sub()
                        Me._statusLabel =
                            Me.SectionPanel.Controls.OfType(Of Label).Where(
                                Function(tb As Label) tb.Name.Equals(Me.StatusLabelName)).SingleOrDefault()
                    End Sub

                If Me.SectionPanel.InvokeRequired Then
                    Me.SectionPanel.Invoke(Sub() act())
                Else
                    act()
                End If
            End If
            Return Me._statusLabel
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="StatusLabel"/> property. )
    ''' <para></para>
    ''' The -dynamically created- <see cref="Label"/> 
    ''' associated with this plugin for displaying the last status message 
    ''' in the plugin's section panel.
    ''' </summary>
    Private _statusLabel As Label

#End Region

#Region " Constructors "

    ''' <summary>
    ''' Initializes a new instance of the <see cref="DynamicPlugin"/> class.
    ''' </summary>
    Protected Friend Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="DynamicPlugin"/> class from the specified JSON configuration file.
    ''' </summary>
    ''' 
    ''' <param name="jsonPath">
    ''' The path to the JSON file describing the plugin metadata and associated VB.NET code file path.
    ''' </param>
    <DebuggerStepThrough>
    Public Sub New(jsonPath As String)

        Dim json As String = File.ReadAllText(jsonPath)
        Dim config As JsonPluginConfig = JsonSerializer.Deserialize(Of JsonPluginConfig)(json, DynamicPlugin.JsonOptions)
        Me.Name = config.Name
        Me.Description = config.Description
        Me.UrlLogin = config.UrlLogin
        Me.UrlRegistration = config.UrlRegistration
        Me.UrlApplication = config.UrlApplication

        Me.UIMemberBaseName = UIHelper.ConvertStringToNetMemberName(Me.Name)
        Me.PluginCachePath = $"{AppGlobals.ChromeUserCachePath}\{Me.UIMemberBaseName}"

        Dim imagePath As String = Path.Combine(Path.GetDirectoryName(jsonPath), config.IconPath)
        Me.Image = If(File.Exists(imagePath), Image.FromFile(imagePath), Nothing)

        Dim vbCodePath As String = Path.Combine(Path.GetDirectoryName(jsonPath), config.VbCodeFile)
        Me.VbCode = File.ReadAllText(vbCodePath)

        Me.ButtonPaneName = $"{NameOf(DarkButtonImageAllignFix)}_ButtonPane_{Me.UIMemberBaseName}"
        Me.TabPageName = $"{NameOf(System.Windows.Forms.TabPage)}_{Me.UIMemberBaseName}"
        Me.SectionPanelName = $"{NameOf(DarkSectionPanel)}_{Me.UIMemberBaseName}"
        Me.DescriptionTextBoxName = $"{NameOf(DarkTextBox)}_Description_{Me.UIMemberBaseName}"
        Me.ButtonRunPluginName = $"{NameOf(DarkButtonImageAllignFix)}_RunPlugin_{Me.UIMemberBaseName}"
        Me.ButtonOpenWebsiteName = $"{NameOf(DarkButtonImageAllignFix)}_OpenWebsite_{Me.UIMemberBaseName}"
        Me.ButtonClearCacheName = $"{NameOf(DarkButtonImageAllignFix)}_ClearCache_{Me.UIMemberBaseName}"
        Me.LogTextBoxName = $"{NameOf(DarkTextBox)}_Log_{Me.UIMemberBaseName}"
        Me.StatusLabelName = $"{NameOf(Label)}_Status_{Me.UIMemberBaseName}"
    End Sub

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Copies the value of all writable public and private members 
    ''' from this <see cref="DynamicPlugin"/> instance 
    ''' to the specified <paramref name="target"/> instance.
    ''' </summary>
    ''' 
    ''' <param name="target">
    ''' The <see cref="DynamicPlugin"/> instance that will receive the copied members.
    ''' </param>
    ''' 
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when the <paramref name="target"/> argument is null.
    ''' </exception>
    <DebuggerStepThrough>
    Public Sub CopyMembersTo(target As DynamicPlugin)

        ArgumentNullException.ThrowIfNull(target)

        target.UIMemberBaseName = Me.UIMemberBaseName
        target.PluginCachePath = Me.PluginCachePath

        target.Name = Me.Name
        target.Description = Me.Description
        target.UrlLogin = Me.UrlLogin
        target.UrlRegistration = Me.UrlRegistration
        target.UrlApplication = Me.UrlApplication
        target.Image = Me.Image
        target.VbCode = Me.VbCode

        target._isRunning = Me._isRunning

        target._buttonPane = Me._buttonPane
        target._tabPage = Me._tabPage
        target._sectionPanel = Me._sectionPanel
        target._descriptionTextBox = Me._descriptionTextBox
        target._buttonRunPlugin = Me._buttonRunPlugin
        target._buttonOpenWebsite = Me._buttonOpenWebsite
        target._buttonClearCache = Me._buttonClearCache
        target._logTextBox = Me._logTextBox
        target._statusLabel = Me._statusLabel
    End Sub

    ''' <summary>
    ''' Asynchronously executes the plugin's main logic.
    ''' </summary>
    ''' 
    ''' <param name="logTextBox">
    ''' A <see cref="TextBox"/> control where status messages can be logged during execution.
    ''' </param>
    ''' 
    ''' <returns>
    ''' A <see cref="Task(Of RegistrationFlags)"/> representing the asynchronous operation.
    ''' </returns>
    <DebuggerStepThrough>
    Public Overrides Async Function RunAsync(logTextBox As TextBox) As Task(Of RegistrationFlags)

        Dim assembly As Assembly = Me.CompileAssembly(Me.VbCode, Me, logTextBox)

        If assembly IsNot Nothing Then
            Dim pluginType As Type =
                    assembly.GetTypes().FirstOrDefault(
                        Function(t) GetType(DynamicPlugin).IsAssignableFrom(t) AndAlso Not t.IsAbstract)

            If pluginType Is Nothing Then
                PluginSupport.LogMessage(Me, My.Resources.Strings.MissingDynamicPluginClass)

            Else
                Dim pluginInstance As DynamicPlugin = DirectCast(Activator.CreateInstance(pluginType), DynamicPlugin)
                Me.CopyMembersTo(pluginInstance)

                Dim runAsyncMethod As MethodInfo =
                        pluginType.GetMethod(
                            NameOf(DynamicPlugin.RunAsync),
                                   BindingFlags.Public Or BindingFlags.NonPublic Or
                                   BindingFlags.Instance Or BindingFlags.DeclaredOnly,
                                   Nothing,
                                   System.Type.EmptyTypes,
                                   Nothing
                             )
                If runAsyncMethod Is Nothing Then
                    PluginSupport.LogMessage(Me, My.Resources.Strings.MissingRunAsyncMethod)
                Else
                    Me._isRunning = True
                    Try
                        Dim regFlags As RegistrationFlags =
                            Await CType(runAsyncMethod.Invoke(pluginInstance, Nothing), Task(Of RegistrationFlags))
                        Return regFlags

                    Catch ex As Exception
                        PluginSupport.LogMessageFormat(Me, My.Resources.Strings.DynamicCodeExecutionErrorFormat, ex.Message)

                    Finally
                        pluginInstance = Nothing
                        Me._isRunning = False
                        GC.Collect()
                        GC.WaitForPendingFinalizers()
                        GC.Collect()

                    End Try
                End If
            End If
        End If

        Return RegistrationFlags.Null
    End Function

#End Region

#Region " Restricted Methods "

    ''' <summary>
    ''' Collects and returns all metadata references used for dynamic plugin compilation.
    ''' <para></para>
    ''' This includes the executing assembly, all loaded assemblies in the application domain, and <see cref="OpenQA.Selenium"/>.
    ''' </summary>
    ''' 
    ''' <returns>
    ''' The resulting collection of <see cref="MetadataReference"/> objects used for dynamic plugin compilation.
    ''' </returns>
    <DebuggerStepThrough>
    Protected Shared Function InitializeCompilationReferences() As ReadOnlyCollection(Of MetadataReference)

        Dim refs As New List(Of MetadataReference) From {
            MetadataReference.CreateFromFile(Assembly.GetExecutingAssembly().Location)
        }

        For Each asm As Assembly In AppDomain.CurrentDomain.GetAssemblies()
            Try
                Dim path As String = asm.Location
                If Not String.IsNullOrEmpty(path) Then
                    refs.Add(MetadataReference.CreateFromFile(path))
                End If
            Catch
            End Try
        Next

        Dim seleniumTypes As Type() = {GetType(IWebDriver), GetType(ChromeDriver)}
        For Each t As Type In seleniumTypes
            Dim asmPath As String = t.Assembly.Location
            If Not refs.Any(Function(ref As MetadataReference) String.Equals(ref.Display, asmPath, StringComparison.InvariantCultureIgnoreCase)) Then
                refs.Add(MetadataReference.CreateFromFile(asmPath))
            End If
        Next

        ' Remove duplicates (if any).
        Return New ReadOnlyCollection(Of MetadataReference)(refs.GroupBy(Function(r) r.Display, StringComparer.OrdinalIgnoreCase).Select(Function(group) group.First()).ToArray())
    End Function

    ''' <summary>
    ''' Compiles the provided VB.NET source code into an in-memory assembly using Roslyn.
    ''' </summary>
    ''' 
    ''' <param name="vbCode">
    ''' The VB.NET source code to compile.
    ''' </param>
    ''' 
    ''' <param name="instance">
    ''' The plugin instance initiating the compilation.
    ''' </param>
    ''' 
    ''' <param name="logTextBox">
    ''' A <see cref="TextBox"/> used to report compilation status or errors.
    ''' </param>
    ''' 
    ''' <returns>
    ''' The resulting <see cref="Assembly"/> if the compilation succeeds; otherwise, <see langword="Nothing"/>.
    ''' </returns>
    <DebuggerStepThrough>
    Private Function CompileAssembly(vbCode As String, instance As Object, logTextBox As TextBox) As Assembly

        Try
            Dim syntaxTree As SyntaxTree = VisualBasicSyntaxTree.ParseText(vbCode)

            Dim compilation As VisualBasicCompilation = VisualBasicCompilation.Create(
                $"{My.Application.Info.AssemblyName}_{Me.UIMemberBaseName}_{Guid.NewGuid():N}",
                syntaxTrees:={syntaxTree},
                references:=DynamicPlugin.CompilationReferences,
                options:=DynamicPlugin.CompileOptions
            )

            Using ms As New MemoryStream()
                Dim result As EmitResult = compilation.Emit(ms)
                If Not result.Success Then
                    PluginSupport.LogMessage(Me, My.Resources.Strings.DynamicCompilationErrors & Environment.NewLine)
                    For Each diag As Diagnostic In result.Diagnostics
                        PluginSupport.LogMessage(Me, diag.ToString())
                    Next
                    Return Nothing
                End If

                ms.Seek(0, SeekOrigin.Begin)

                Dim assembly As Assembly = AppGlobals.PluginLoadContext.LoadFromStream(ms)
                Return assembly
            End Using

        Catch ex As Exception
            PluginSupport.LogMessageFormat(Me, My.Resources.Strings.StatusMsg_ExceptionFormat, ex.ToString())
            Return Nothing

        End Try
    End Function

#End Region

#Region " Dispose Method "

    ''' <summary>
    ''' Releases the resources used by this <see cref="DynamicPlugin"/> instance.
    ''' </summary>
    Public Overrides Sub Dispose()

        Me.ButtonPane?.Dispose()
        Me.TabPage?.Dispose()
        Me.SectionPanel?.Dispose()
        Me.DescriptionTextBox?.Dispose()
        Me.ButtonRunPlugin?.Dispose()
        Me.ButtonOpenWebsite?.Dispose()
        Me.ButtonClearCache?.Dispose()
        Me.LogTextBox?.Dispose()
        Me.StatusLabel?.Dispose()

        MyBase.Dispose()
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
