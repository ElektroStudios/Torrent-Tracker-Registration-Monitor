#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions

Imports DarkUI.Controls

Imports TTRM.Win32

#End Region

''' <summary>
''' Provides helper members related to user-interface operations.
''' </summary>
Public Module UIHelper

#Region " Public Methods "

    ''' <summary>
    ''' Updates the user-interface control texts according to the current language resources.
    ''' </summary>
    <DebuggerStepThrough>
    Public Sub SetUILanguage()
        Dim splashScreen As MainSplashScreen = AppGlobals.MainSplashScreenInstance
        If splashScreen.IsHandleCreated Then
            splashScreen.Invoke(Sub() splashScreen.Label_StatusLoad.Text = "Setting up language...")
        End If

        Dim fGit As GitHubUpdatePrompt = My.Forms.GitHubUpdatePrompt
        fGit.DarkLabel_ProgramUpdateAvailable.Text = My.Resources.Strings.ProgramUpdateAvailable
        fGit.DarkButton_OpenGitHubLatestReleasePage.Text = My.Resources.Strings.OpenGitHubLatestReleasePage

        Dim f As MainForm = AppGlobals.MainFormInstance
        f.DarkSectionPanel_Plugins.SectionHeader = My.Resources.Strings.Plugins
        f.DarkSectionPanel_Program.SectionHeader = My.Resources.Strings.Program
        f.DarkSectionPanel_Settings.SectionHeader = My.Resources.Strings.Settings
        f.DarkCheckBox_AutoPluginRun.Text = My.Resources.Strings.AutoPluginRunMsg
        f.DarkCheckBox_SystemSleep.Text = My.Resources.Strings.SystemSleepMsg
        f.DarkGroupBox_AutoPluginRun.Text = My.Resources.Strings.AutoPluginRunGroupBox
        f.DarkButton_Settings.Text = My.Resources.Strings.Settings
        f.DarkButton_About.Text = My.Resources.Strings.About
        f.DarkGroupBox_Application.Text = My.Resources.Strings.GroupBoxApplication
        f.Label_Language.Text = My.Resources.Strings.LanguageLabel
        f.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.Text = My.Resources.Strings.RunAllSelectedPluginsNow
        f.DarkCheckBox_RememberCurrentSettings.Text = My.Resources.Strings.RememberSettings
        f.DarkButtonImageAllignFix_ClearCache.Text = My.Resources.Strings.ClearCache
        f.DarkCheckBox_ParalellExecution.Text = My.Resources.Strings.EnableParalellExecution
        f.DarkCheckBox_RunAppMinimized.Text = My.Resources.Strings.RunAppMinimized
        f.DarkCheckBox_ClearPreviousLogEntries.Text = My.Resources.Strings.ClearPreviousLogEntries
        f.DarkCheckBox_AllowPluginApplicationFormCheck.Text = My.Resources.Strings.AllowPluginsToCheckApplicationForms
        f.DarkCheckBox_SearchProgramUpdates.Text = My.Resources.Strings.SearchProgramUpdates
        f.Label_Hours.Text = My.Resources.Strings.Hours
        f.DarkCheckBox_DontRunIfFullscreen.Text = My.Resources.Strings.DontRunIfFullscreen
        f.ToolStripMenuItem_ClearSelectedPlugins.Text = My.Resources.Strings.ClearSelectedPlugins
        f.ToolStripMenuItem_SelectAllPlugins.Text = My.Resources.Strings.SelectAllPlugins
        f.ToolStripMenuItem_ShowWindow.Text = My.Resources.Strings.ShowWindow
        f.ToolStripMenuItem_HideWindow.Text = My.Resources.Strings.HideWindow
        f.ToolStripMenuItem_CloseProgram.Text = My.Resources.Strings.CloseApplication

        f.Label_AutoRunPluginCheckedCount.Text = If(f.CheckedListBox_AutoPluginRun.CheckedItems.Count <> 0,
            String.Format(My.Resources.Strings.AutoRunPluginCheckedCountFormat, f.CheckedListBox_AutoPluginRun.CheckedItems.Count, f.CheckedListBox_AutoPluginRun.Items.Count),
            String.Format(My.Resources.Strings.AutoRunPluginCheckedNoneFormat, f.CheckedListBox_AutoPluginRun.Items.Count))

        For Each plugin As DynamicPlugin In AppGlobals.LoadedDynamicPlugins
            plugin.ButtonRunPlugin.Text = My.Resources.Strings.RunPluginButton
            plugin.ButtonOpenWebsite.Text = My.Resources.Strings.OpenWebsiteButton
            plugin.ButtonClearCache.Text = My.Resources.Strings.ClearCache

            If CStr(plugin.StatusLabel.Tag)?.Equals(AppGlobals.ControlInitialTextTag) Then
                plugin.StatusLabel.Text = My.Resources.Strings.LastPluginRunInitialText
            End If
        Next
    End Sub

    ''' <summary>
    ''' Flashes the taskbar button of the application window.
    ''' </summary>
    ''' 
    ''' <param name="flashCount">
    ''' Optional. The number of times to flash. 
    ''' <para></para>
    ''' Default value is zero (infinite flashing until the window / main form gets focus).
    ''' </param>
    <DebuggerStepThrough>
    Public Sub FlashTaskbar(Optional flashCount As UInteger = 0UI)

        Dim f As MainForm = AppGlobals.MainFormInstance
        Dim hwnd As IntPtr
        If f.InvokeRequired Then
            f.Invoke(Sub() hwnd = f.Handle)
        Else
            hwnd = f.Handle
        End If

        Dim fi As FlashInfo
        With fi
            .Size = CUInt(Marshal.SizeOf(GetType(FlashInfo)))
            .Hwnd = hwnd
            .Flags = FlashWindowFlags.Tray Or FlashWindowFlags.TimerNoForeground
            .Count = flashCount
            .Timeout = 0
        End With
        NativeMethods.FlashWindowEx(fi)
    End Sub

    ''' <summary>
    ''' Toggles the visibility of the main form. 
    ''' <para></para>
    ''' If the form is hidden / minimized to the system tray, the form is shown and brought to front.
    ''' </summary>
    <DebuggerStepThrough>
    Public Sub ToggleMainFormVisibility()

        Dim f As MainForm = AppGlobals.MainFormInstance
        Dim act As New Action(
            Sub()
                If f.Visible Then
                    f.Hide()
                Else
                    If Not f.Visible Then
                        f.Show()
                    End If
                    f.WindowState = FormWindowState.Normal
                    Interaction.AppActivate(Environment.ProcessId)
                End If
            End Sub)

        If f.InvokeRequired Then
            f.Invoke(Sub() act())
        Else
            act()
        End If
    End Sub

    ''' <summary>
    ''' Converts the specified string into a valid .NET-compatible member name formatted using PascalCase rules.
    ''' <para></para>
    ''' For example:
    ''' <list type="bullet">
    '''   <item>
    '''     <description><c>"user name (temp)"</c> → <c>UserNameTemp</c></description>
    '''   </item>
    '''   <item>
    '''     <description><c>"99-bottles_of-beer"</c> → <c>_99BottlesOfBeer</c></description>
    '''   </item>
    ''' </list>
    ''' </summary>
    ''' 
    ''' <param name="value">
    ''' The input string to be converted into a valid .NET-compatible member name.
    ''' </param>
    ''' 
    ''' <returns>
    ''' A .NET-compatible identifier suitable for use as a member name, such as a property, method, or field name.
    ''' </returns>
    ''' 
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <paramref name="value"/> is null, empty, or consists only of white-space characters.
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentException">
    ''' Thrown when the provided <paramref name="value"/> does not contain any valid alphanumeric characters
    ''' and therefore cannot be converted into a valid .NET-compatible member name.
    ''' </exception>
    <DebuggerStepThrough>
    Public Function ConvertStringToNetMemberName(value As String) As String

        If String.IsNullOrWhiteSpace(value) Then
            Throw New ArgumentNullException(paramName:=NameOf(value))
        End If

        ' Trim whitespace and remove accents/diacritics
        Dim normalized As String = value.Trim().Normalize(NormalizationForm.FormD)

        Dim sb As New StringBuilder()
        For Each c As Char In normalized
            Dim uc As UnicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c)
            If uc <> UnicodeCategory.NonSpacingMark Then
                sb.Append(c)
            End If
        Next
        Dim clean As String = sb.ToString()

        ' Replace all invalid characters (non-letters/digits) with a single space (acts as word separator).
        clean = Regex.Replace(clean, "[^a-zA-Z0-9]", " ")

        ' Split into parts by white-spaces.
        Dim whitespace As Char = " "c
        Dim parts As String() = clean.Split({whitespace}, StringSplitOptions.RemoveEmptyEntries)

        If parts.Length = 0 Then
            Dim msg As String =
                "The provided value does not contain any valid alphanumeric characters " &
                "and therefore cannot be converted into a valid .NET-compatible member name."
            Throw New ArgumentException(msg, NameOf(value))
        End If

        ' Build PascalCase result.
        Dim result As New StringBuilder()
        For Each part As String In parts
            If part.Length > 0 Then
                result.Append(Char.ToUpperInvariant(part(0)))
                If part.Length > 1 Then
                    result.Append(part.AsSpan(1))
                End If
            End If
        Next

        ' If starts with a digit, prefix with underscore.
        If Char.IsDigit(result(0)) Then
            result.Insert(0, "_"c)
        End If

        Return result.ToString()
    End Function

#End Region

#Region " Restricted Methods "

    ''' <summary>
    ''' Appends a line to the specified <see cref="TextBox"/>.
    ''' </summary>
    ''' 
    ''' <param name="logTextBox">
    ''' The textbox to append the message to.
    ''' </param>
    ''' 
    ''' <param name="message">
    ''' The message string to log.
    ''' </param>
    ''' 
    ''' <param name="addNewLine">
    ''' Optional. A value indicating whether to add a new line after the message.
    ''' <para></para>
    ''' Default is <see langword="True"/>.
    ''' </param>
    <DebuggerStepThrough>
    Friend Sub AppendLine(tb As TextBox, line As String, Optional addNewLine As Boolean = True)

        line = $"{line}{If(addNewLine, Environment.NewLine, Nothing)}"
        If tb.InvokeRequired Then
            tb.Invoke(Sub() tb.AppendText(line))
        Else
            tb.AppendText(line)
        End If
    End Sub

    ''' <summary>
    ''' Appends a line to the specified <see cref="TextBox"/> with a timestamp at the start of the line.
    ''' </summary>
    ''' 
    ''' <param name="logTextBox">
    ''' The textbox to append the message to.
    ''' </param>
    ''' 
    ''' <param name="message">
    ''' The message string to log.
    ''' </param>
    ''' 
    ''' <param name="addNewLine">
    ''' Optional. A value indicating whether to add a new line after the message.
    ''' <para></para>
    ''' Default is <see langword="True"/>.
    ''' </param>
    <DebuggerStepThrough>
    Friend Sub AppendLineWithTimestamp(tb As TextBox, line As String, Optional addNewLine As Boolean = True)

        Dim time As String = Date.Now.ToLongTimeString()
        line = $"[{time}] {line}{If(addNewLine, Environment.NewLine, Nothing)}"
        If tb.InvokeRequired Then
            tb.Invoke(Sub() tb.AppendText(line))
        Else
            tb.AppendText(line)
        End If
    End Sub

    ''' <summary>
    ''' Updates the main form's <see cref="StatusStrip"/> label text based on the current application state.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub UpdateStatusLabelText()

        Dim f As MainForm = AppGlobals.MainFormInstance
        Dim act As New Action(
            Sub()
                If f.PluginWorkInProgress Then
                    Exit Sub
                End If

                If f.DarkCheckBox_AutoPluginRun.Checked Then
                    f.SuspendLayout()
                    f.ToolStripStatusLabel1.Text = String.Format(My.Resources.Strings.WaitingForTheNextAutomaticExecutionFormat, f.RemainingAutoPluginRunInterval)
                    f.ResumeLayout(False)
                Else
                    f.ToolStripStatusLabel1.Text = String.Format(My.Resources.Strings.LastPluginRunMsgFormat, Date.Now.ToShortDateString(), Date.Now.ToLongTimeString())
                End If
                f.StatusStrip1.Update()
            End Sub)

        If f.InvokeRequired Then
            f.Invoke(Sub() act())
        Else
            act()
        End If
    End Sub

    ''' <summary>
    ''' Updates the main form's <see cref="StatusStrip"/> label text with the specified message.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub UpdateStatusLabelText(message As String)

        Dim f As MainForm = AppGlobals.MainFormInstance

        Dim act As New Action(
            Sub()
                f.ToolStripStatusLabel1.Text = message
                f.StatusStrip1.Refresh()
            End Sub)

        If f.InvokeRequired Then
            f.Invoke(Sub() act())
        Else
            act()
        End If
    End Sub

    ''' <summary>
    ''' Updates the status labels of both the specified <see cref="DynamicPlugin"/> 
    ''' and the main form's <see cref="StatusStrip"/> label according to the given <see cref="RegistrationFlags"/> value.
    ''' </summary>
    ''' 
    ''' <param name="plugin">
    ''' The <see cref="DynamicPlugin"/> instance whose status label needs to be updated.
    ''' </param>
    ''' 
    ''' <param name="regFlags">
    ''' The <see cref="RegistrationFlags"/> value.
    ''' </param>
    <DebuggerStepThrough>
    Friend Sub UpdateStatusLabelText(plugin As DynamicPlugin, regFlags As RegistrationFlags)

        Dim f As MainForm = AppGlobals.MainFormInstance

        Dim act As New Action(
            Sub()
                If Not plugin.ButtonRunPlugin.Enabled Then
                    plugin.StatusLabel.Text = My.Resources.Strings.ExecutingPlugin
                    plugin.StatusLabel.Parent.Refresh()
                    f.ToolStripStatusLabel1.Text = $"{My.Resources.Strings.ExecutingPlugin} ({plugin.Name})"
                    f.StatusStrip1.Refresh()

                Else
                    Dim prefix As String = Nothing
                    If regFlags = RegistrationFlags.Null OrElse
                       regFlags.HasFlag(RegistrationFlags.RegistrationUnknown) OrElse
                       regFlags.HasFlag(RegistrationFlags.ApplicationUnknown) Then
                        prefix = "⚠️ "

                    ElseIf regFlags.HasFlag(RegistrationFlags.RegistrationOpen) OrElse
                           regFlags.HasFlag(RegistrationFlags.ApplicationOpen) Then
                        prefix = "🎉 "

                    Else
                        prefix = "✔️ "

                    End If

                    plugin.StatusLabel.Text = String.Format(prefix & My.Resources.Strings.LastPluginRunMsgFormat, Date.Now.ToShortDateString(), Date.Now.ToLongTimeString())
                    plugin.StatusLabel.Parent?.Refresh()

                    If Not f.Timer_AutoRunPlugins.Enabled Then
                        f.ToolStripStatusLabel1.Text = ""
                        f.StatusStrip1.Refresh()
                    End If
                End If

                ' Remove initial state text flag.
                plugin.StatusLabel.Tag = Nothing
            End Sub)

        If f.InvokeRequired Then
            f.Invoke(Sub() act())
        Else
            act()
        End If

    End Sub

    ''' <summary>
    ''' Enables or disables the main form panels 
    ''' and all plugin-related controls in the currently selected <see cref="DarkSectionPanel"/>.
    ''' <para></para>
    ''' These controls must be disabled before calling method <see cref="PluginBase.RunAsync"/> 
    ''' to prevent user interaction during plugin execution, and re-enabled once the execution has completed.
    ''' </summary>
    ''' 
    ''' <param name="enabled">
    ''' A value indicating whether the controls should be enabled (<see langword="True"/>) or disabled (<see langword="False"/>).
    ''' </param>
    Friend Sub ToggleControlsEnabledState(enable As Boolean)

        Dim f As MainForm = AppGlobals.MainFormInstance

        Dim act As New Action(
            Sub()
                f.DarkGroupBox_AutoPluginRun.Enabled = enable
                f.DarkButtonImageAllignFix_ClearCache.Enabled = enable
                f.Label_Language.Enabled = enable
                f.DarkComboBox_Language.Enabled = enable

                If f.DarkSectionPanel_Settings.Contains(f.ActiveControl) Then
                    f.DarkSectionPanel_Settings.Update()
                End If

                For Each tb As TabPage In f.TabControlNoBorder_Main.TabPages
                    If tb.Equals(f.TabPage_Settings) Then
                        Continue For
                    End If

                    Dim sectionPanel As DarkSectionPanel = tb.Controls.OfType(Of DarkSectionPanel).SingleOrDefault()
                    If sectionPanel IsNot Nothing AndAlso sectionPanel.Controls.Count <> 0 Then
                        For Each ctrl As Control In sectionPanel.Controls
                            If (TypeOf ctrl Is DarkButton) Then

                                If ctrl.Name.Contains("_RunPlugin_") Then
                                    ' Ignore.
                                Else
                                    ctrl.Enabled = If(Not enable, Not ctrl.Name.Contains("_ClearCache_"), enable)
                                End If

                            Else
                                ctrl.Enabled = True

                            End If
                        Next
                    End If

                    If sectionPanel.Contains(f.ActiveControl) Then
                        sectionPanel.Update()
                    End If
                Next

            End Sub)

        If f.InvokeRequired() Then
            f.Invoke(act)
        Else
            act()
        End If
    End Sub

    ''' <summary>
    ''' Initializes the <see cref="LanguageComboBoxItem"/> control with the available languages, 
    ''' and selects the item corrsponding to the current UI culture.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub InitializeLanguageComboBox()

        Dim splashScreen As MainSplashScreen = AppGlobals.MainSplashScreenInstance
        If splashScreen.IsHandleCreated Then
            splashScreen.Invoke(Sub() splashScreen.Label_StatusLoad.Text = "Initializing language databinding...")
        End If

        Dim f As MainForm = AppGlobals.MainFormInstance
        Dim cb As DarkComboBox = f.DarkComboBox_Language

        ' Rebuild LanguageComboBoxItems so DisplayName strings are recalculated with new culture.
        Dim cultures As CultureInfo() = AppGlobals.LanguageComboBoxItems.Select(Function(item) item.Culture).ToArray()
        AppGlobals.LanguageComboBoxItems.Clear()
        For Each c As CultureInfo In cultures
            AppGlobals.LanguageComboBoxItems.Add(New LanguageComboBoxItem(c))
        Next

        ' Rebind the combobox safely (prevent re-trigger).
        RemoveHandler cb.SelectedIndexChanged, AddressOf f.DarkComboBoxLanguage_SelectedIndexChanged
        cb.DataSource = Nothing
        cb.DataSource = AppGlobals.LanguageComboBoxItems
        cb.DisplayMember = NameOf(LanguageComboBoxItem.DisplayName)
        cb.ValueMember = NameOf(LanguageComboBoxItem.Culture)
        cb.SelectedItem = AppGlobals.LanguageComboBoxItems.SingleOrDefault(
            Function(langItem As LanguageComboBoxItem) langItem.IsCurrentUICulture
        )

        AddHandler cb.SelectedIndexChanged, AddressOf f.DarkComboBoxLanguage_SelectedIndexChanged

        ' Falls back to English when no supported language is detected.
        If cb.SelectedItem Is Nothing Then
            cb.SelectedItem = AppGlobals.LanguageComboBoxItems.Single(
            Function(langItem As LanguageComboBoxItem) langItem.Culture.Equals(New CultureInfo("en"))
        )
        End If
    End Sub

    ''' <summary>
    ''' Initializes and sets up the dynamic plugin controls within the main form.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub InitializePlugins()

        Dim splashScreen As MainSplashScreen = AppGlobals.MainSplashScreenInstance
        If splashScreen.IsHandleCreated Then
            splashScreen.Invoke(Sub() splashScreen.Label_StatusLoad.Text = "Loading plugins...")
        End If

        Dim f As MainForm = AppGlobals.MainFormInstance

        ' Initialize dynamic plugin objects.
        AppGlobals.LoadedDynamicPlugins = ApplicationHelper.LoadAllPluginsFromJson()
        Dim uniqueDynamicPlugins As New HashSet(Of DynamicPlugin)
        For Each plugin As DynamicPlugin In AppGlobals.LoadedDynamicPlugins
            uniqueDynamicPlugins.Add(plugin)
        Next
        If uniqueDynamicPlugins.Count = 0 Then
            If f.IsHandleCreated Then
                f.Invoke(Sub() My.Forms.MainSplashScreen.Visible = False)
            End If
            MessageBox.Show(My.Resources.Strings.NoPluginsAvailable,
                            My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            f.Close()
        End If
        If AppGlobals.LoadedDynamicPlugins.Count > uniqueDynamicPlugins.Count Then
            If f.IsHandleCreated Then
                f.Invoke(Sub() My.Forms.MainSplashScreen.Visible = False)
            End If
            MessageBox.Show(My.Resources.Strings.DuplicatedPluginsDetected,
                            My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        AppGlobals.LoadedDynamicPlugins = New ReadOnlyCollection(Of DynamicPlugin)(uniqueDynamicPlugins.ToList())

        ' Add dynamic plugin objects to the listbox in the 'Automatic Plugin Execution' settings groupbox.
        f.CheckedListBox_AutoPluginRun.Items.AddRange(AppGlobals.LoadedDynamicPlugins.ToArray())

        ' Create the plugin controls.
        UIHelper.CreateDynamicPluginControls(AppGlobals.LoadedDynamicPlugins)

        ' Fix plugin button widths if vertical scrollbar is visible.
        Dim pluginsSectionPanel As DarkSectionPanel = f.DarkSectionPanel_Plugins
        pluginsSectionPanel.AutoScroll = True
        pluginsSectionPanel.HorizontalScroll.Enabled = False
        pluginsSectionPanel.VerticalScroll.Enabled = True
        If pluginsSectionPanel.VerticalScroll.Visible Then
            For Each bt As DarkButton In pluginsSectionPanel.Controls.OfType(Of DarkButton)
                bt.Width = pluginsSectionPanel.ClientSize.Width - 8
            Next
        End If
    End Sub

    ''' <summary>
    ''' Dynamically creates the user-interface controls for each <see cref="DynamicPlugin"/> within the specified collection.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' Each plugin will automatically generate:
    ''' <list type="bullet">
    ''' <item>A navigation button in the <see cref="MainForm.DarkSectionPanel_Plugins"/> panel.</item>
    ''' <item>A dedicated tab page with a <see cref="DarkSectionPanel"/> to store plugin related controls.</item>
    ''' <item>A run plugin button, open website button, status label, and logging textbox in the tabpage's section panel.</item>
    ''' </list>
    ''' The controls are fully wired to the plugin's async execution flow via event handlers.
    ''' </remarks>
    <DebuggerStepThrough>
    Private Sub CreateDynamicPluginControls(plugins As IEnumerable(Of DynamicPlugin))

        Dim f As MainForm = AppGlobals.MainFormInstance

        Const sectionPanelHeaderMargin As Integer = 26
        Const sectionPanelBorderMargin As Integer = 8

        Dim nextButtonTop As Integer = sectionPanelHeaderMargin

        For Each plugin As DynamicPlugin In plugins
            Dim btPane As New DarkButtonImageAllignFix With {
                .Name = plugin.ButtonPaneName,
                .Text = plugin.Name,
                .TextImageRelation = TextImageRelation.ImageBeforeText,
                .ResizedImage = plugin.Image,
                .Height = 40,
                .Width = f.DarkSectionPanel_Plugins.ClientSize.Width - 6,
                .Top = nextButtonTop,
                .Left = 0,
                .TabStop = True,
                .Font = f.Font,
                .Cursor = Cursors.Hand
            }
            btPane.Left += 4
            nextButtonTop = btPane.Bottom
            f.DarkSectionPanel_Plugins.Controls.Add(btPane)

            Dim tp As New TabPage(plugin.Name) With {
                .Name = plugin.TabPageName,
                .Dock = DockStyle.Fill,
                .Margin = Padding.Empty,
                .Padding = Padding.Empty,
                .Font = f.Font
            }
            f.TabControlNoBorder_Main.TabPages.Add(tp)

            Dim sectionPanel As New DarkSectionPanel With {
                .Name = plugin.SectionPanelName,
                .SectionHeader = plugin.Name,
                .Dock = DockStyle.Fill,
                .Font = f.Font
            }
            tp.Controls.Add(sectionPanel)

            Dim descriptionTextBox As New DarkTextBox With {
                .Name = plugin.DescriptionTextBoxName,
                .Text = plugin.Description,
                .Multiline = True,
                .ScrollBars = ScrollBars.Vertical,
                .TextAlign = HorizontalAlignment.Left,
                .[ReadOnly] = True,
                .Height = 65,
                .Width = tp.ClientSize.Width - (sectionPanelBorderMargin * 2),
                .Location = New Point(sectionPanelBorderMargin, sectionPanelHeaderMargin + sectionPanelBorderMargin),
                .Font = f.Font,
                .Cursor = Cursors.Default
            }
            descriptionTextBox.SelectionStart = descriptionTextBox.TextLength
            descriptionTextBox.SelectionLength = 0
            sectionPanel.Controls.Add(descriptionTextBox)

            Dim buttonsTotalMargins As Integer = sectionPanelBorderMargin * 4
            Dim buttonWidth As Integer = (tp.ClientSize.Width - buttonsTotalMargins) \ 3

            Dim buttonRunPlugin As New DarkButtonImageAllignFix With {
                .Name = plugin.ButtonRunPluginName,
                .Text = My.Resources.Strings.RunPluginButton,
                .TextImageRelation = TextImageRelation.ImageBeforeText,
                .Height = btPane.Height,
                .Width = buttonWidth,
                .Location = New Point(sectionPanelBorderMargin, descriptionTextBox.Bottom + sectionPanelBorderMargin),
                .Font = f.Font,
                .Cursor = Cursors.Hand,
                .ResizedImage = My.Resources.execute
            }
            sectionPanel.Controls.Add(buttonRunPlugin)

            Dim buttonOpenWebsite As New DarkButtonImageAllignFix With {
                .Name = plugin.ButtonOpenWebsiteName,
                .Text = My.Resources.Strings.OpenWebsiteButton,
                .TextImageRelation = TextImageRelation.ImageBeforeText,
                .Height = btPane.Height,
                .Width = buttonWidth,
                .Location = New Point(buttonRunPlugin.Right + sectionPanelBorderMargin, buttonRunPlugin.Top),
                .Font = f.Font,
                .Cursor = Cursors.Hand,
                .ResizedImage = My.Resources.website,
                .Tag = plugin,
                .ContextMenuStrip = f.DarkContextMenu_PluginUrls
            }
            sectionPanel.Controls.Add(buttonOpenWebsite)

            Dim buttonClearCache As New DarkButtonImageAllignFix With {
                .Name = plugin.ButtonClearCacheName,
                .Text = My.Resources.Strings.ClearCache,
                .TextImageRelation = TextImageRelation.ImageBeforeText,
                .Height = btPane.Height,
                .Width = buttonWidth,
                .Location = New Point(buttonOpenWebsite.Right + sectionPanelBorderMargin, buttonRunPlugin.Top),
                .Font = f.Font,
                .Cursor = Cursors.Hand,
                .ResizedImage = My.Resources.clean
            }
            sectionPanel.Controls.Add(buttonClearCache)

            Dim statusLabel As New Label With {
                .Name = plugin.StatusLabelName,
                .Text = My.Resources.Strings.LastPluginRunInitialText,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Dock = DockStyle.Bottom,
                .BackColor = Color.Transparent,
                .ForeColor = Color.FromKnownColor(KnownColor.Gainsboro),
                .AutoSize = True,
                .Margin = Padding.Empty,
                .Padding = Padding.Empty,
                .Tag = AppGlobals.ControlInitialTextTag
            }
            sectionPanel.Controls.Add(statusLabel)

            Dim logTextBox As New DarkTextBox With {
                .Name = plugin.LogTextBoxName,
                .Multiline = True,
                .[ReadOnly] = True,
                .MaxLength = 0,
                .Height = (sectionPanel.ClientSize.Height - sectionPanelBorderMargin) - (buttonRunPlugin.Bottom + sectionPanelBorderMargin) - statusLabel.Height,
                .Width = tp.ClientSize.Width - (sectionPanelBorderMargin * 2),
                .Location = New Point(buttonRunPlugin.Left, buttonRunPlugin.Bottom + sectionPanelBorderMargin),
                .Font = f.Font,
                .ScrollBars = ScrollBars.Vertical
            }
            sectionPanel.Controls.Add(logTextBox)

            AddHandler btPane.Click,
                Sub(sender As Object, e As EventArgs)
                    f.TabControlNoBorder_Main.SelectTab(tp.Name)
                    btPane.Focus()
                End Sub

            Dim gotFocusHandler As New EventHandler(Sub(sender As Object, e As EventArgs)
                                                        If f.IsFormLoaded Then
                                                            f.TabControlNoBorder_Main.SelectedTab = tp
                                                            btPane.Focus()
                                                            f.DarkSectionPanel_Plugins.Update()
                                                        End If
                                                    End Sub)

            AddHandler btPane.GotFocus,
                Sub(sender As Object, e As EventArgs)
                    RemoveHandler btPane.GotFocus, gotFocusHandler
                    gotFocusHandler(sender, e)

                    AddHandler btPane.GotFocus,
                        Sub(sender2 As Object, e2 As EventArgs)
                            RemoveHandler btPane.GotFocus, gotFocusHandler
                            gotFocusHandler(sender2, e2)
                            AddHandler btPane.GotFocus, gotFocusHandler
                        End Sub
                End Sub

            AddHandler tp.Enter,
                Sub(sender As Object, e As EventArgs)
                    logTextBox.BeginInvoke(Sub() logTextBox.ScrollToCaret())
                End Sub

            AddHandler btPane.LostFocus,
                Sub(sender As Object, e As EventArgs)
                    f.lastFocusedButtonPane = btPane
                End Sub

            AddHandler buttonOpenWebsite.MouseUp,
                Sub(sender As Object, e As MouseEventArgs)
                    If e.Button = MouseButtons.Left Then
                        If buttonOpenWebsite.ClientRectangle.Contains(e.Location) Then
                            f.DarkContextMenu_PluginUrls.Show(buttonOpenWebsite, e.Location)
                        End If
                    End If
                End Sub

            AddHandler buttonRunPlugin.Click,
                        Async Sub(sender As Object, e As EventArgs)
                            Await UIHelper.RunPluginFromButtonAsync(plugin)
                        End Sub

            AddHandler buttonClearCache.Click,
                        Async Sub(sender As Object, e As EventArgs) Await ApplicationHelper.ClearCache(plugin)

        Next
    End Sub

    ''' <summary>
    ''' Determines whether the input focus can move to the next or previous plugin button 
    ''' in the <see cref="MainForm.DarkSectionPanel_Plugins"/> based on 
    ''' the currently active control and key pressed.
    ''' </summary>
    ''' 
    ''' <param name="activeControl">
    ''' The control that currently has focus.
    ''' </param>
    ''' 
    ''' <param name="keyData">
    ''' The key pressed (arrow keys) that may trigger focus movement.
    ''' </param>
    ''' 
    ''' <returns>
    ''' <see langword="True"/> if the focus is allowed to move; otherwise, <see langword="False"/>.
    ''' </returns>
    <DebuggerStepThrough>
    Friend Function CanFocusNextPluginButtonControl(activeControl As Control, keyData As Keys) As Boolean

        Dim f As MainForm = AppGlobals.MainFormInstance

        Dim sectionPanel As DarkSectionPanel = f.DarkSectionPanel_Plugins
        Dim buttons As List(Of Control) = sectionPanel.Controls.
            OfType(Of Control)().
            OrderBy(Function(b) b.TabIndex).
            ToList()

        If buttons.Count = 0 Then
            Return False
        End If

        Dim curIndex As Integer = buttons.IndexOf(activeControl)
        If curIndex = -1 Then
            curIndex = 0
        End If
        Dim maxIndex As Integer = buttons.Count - 1

        Select Case keyData

            Case Keys.Tab
                f.DarkSectionPanel_Program.Focus()
                Return False

            Case Keys.Up, Keys.Left
                Return curIndex <> 0

            Case Keys.Down, Keys.Right
                Return curIndex <> maxIndex

            Case Else
                If (keyData >= Keys.A AndAlso keyData <= Keys.Z) OrElse
                   (keyData >= Keys.D0 AndAlso keyData <= Keys.D9) OrElse
                   (keyData >= Keys.NumPad0 AndAlso keyData <= Keys.NumPad9) Then

                    Dim targetChar As Char
                    ' Keys A to Z
                    If keyData >= Keys.A AndAlso keyData <= Keys.Z Then
                        targetChar = ChrW(keyData)

                        ' keys 0 to 9
                    ElseIf keyData >= Keys.D0 AndAlso keyData <= Keys.D9 Then
                        targetChar = ChrW(keyData - Keys.D0 + AscW("0"c))

                    Else ' NumPad0 to NumPad9
                        targetChar = ChrW(keyData - Keys.NumPad0 + AscW("0"c))

                    End If

                    ' Find buttons starting with targetChar.
                    Dim matchingButtons As List(Of Control) =
                        buttons.Where(Function(b) b.Text.Trim().StartsWith(targetChar.ToString(), StringComparison.InvariantCultureIgnoreCase)).ToList()

                    If matchingButtons.Count = 0 Then
                        Return False
                    End If

                    ' Determine next button.
                    Dim nextButton As Control
                    If matchingButtons.Contains(activeControl) Then
                        Dim currentMatchIndex As Integer = matchingButtons.IndexOf(activeControl)
                        nextButton = matchingButtons((currentMatchIndex + 1) Mod matchingButtons.Count)
                    Else
                        nextButton = matchingButtons.First()
                    End If

                    If nextButton IsNot Nothing Then
                        nextButton.Focus()
                        Return False ' Already handled focus, so return False.
                    End If
                End If

                Return True
        End Select
    End Function

    ''' <summary>
    ''' Asynchronously runs the specified <see cref="DynamicPlugin"/> as if triggered by its associated button,
    ''' updating the UI and plugin status during execution.
    ''' </summary>
    ''' 
    ''' <param name="plugin">
    ''' The <see cref="DynamicPlugin"/> instance to execute.
    ''' </param>
    ''' 
    ''' <returns>
    ''' A <see cref="Task"/> representing the asynchronous operation.
    ''' </returns>
    <DebuggerStepThrough>
    Friend Async Function RunPluginFromButtonAsync(plugin As DynamicPlugin) As Task(Of RegistrationFlags)

        Dim f As MainForm = AppGlobals.MainFormInstance
        f.UseWaitCursor = True

        If plugin.ButtonRunPlugin.InvokeRequired Then
            plugin.ButtonRunPlugin.Invoke(Sub() plugin.ButtonRunPlugin.Enabled = False)
        Else
            plugin.ButtonRunPlugin.Enabled = False
        End If

        UIHelper.ToggleControlsEnabledState(enable:=False)

        If f.DarkCheckBox_ClearPreviousLogEntries.Checked() Then
            If plugin.LogTextBox.InvokeRequired Then
                plugin.LogTextBox.Invoke(Sub() plugin.LogTextBox.Clear())
            Else
                plugin.LogTextBox.Clear()
            End If
        End If

        UIHelper.UpdateStatusLabelText(plugin, Nothing)
        Dim flags As RegistrationFlags
        Try
            f.PluginWorkInProgress = True
            flags = Await plugin.RunAsync(plugin.LogTextBox)
            Return flags

        Finally
            f.PluginWorkInProgress = f.IsRunningPluginsAutomatically

            If Not f.IsRunningPluginsAutomatically Then
                UIHelper.ToggleControlsEnabledState(enable:=True)
                f.UseWaitCursor = False
                Cursor.Current = Cursors.Default
            End If

            If plugin.ButtonRunPlugin.InvokeRequired Then
                plugin.ButtonRunPlugin.Invoke(
                    Sub()
                        plugin.ButtonRunPlugin.Enabled = True
                        UIHelper.UpdateStatusLabelText(plugin, flags)
                    End Sub)
            Else
                plugin.ButtonRunPlugin.Enabled = True
                UIHelper.UpdateStatusLabelText(plugin, flags)
            End If

            If plugin.ButtonRunPlugin.InvokeRequired Then
                plugin.ButtonRunPlugin.Invoke(Sub() plugin.ButtonRunPlugin.Focus())
            Else
                plugin.ButtonRunPlugin.Focus()
            End If

        End Try

        Return RegistrationFlags.Null
    End Function

#End Region

End Module
