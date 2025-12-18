#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Globalization

Imports DarkUI.Controls

Imports MTAFRAT.Win32

#End Region

Public NotInheritable Class MainForm : Inherits DarkUI.Forms.DarkForm

#Region " Fields "

    ''' <summary>
    ''' Holds the remaining time interval before the next automatic plugin run operation is performed.
    ''' </summary>
    Friend RemainingAutoPluginRunInterval As TimeSpan = AppGlobals.AutomaticPluginRunInterval

    ''' <summary>
    ''' Indicates whether the main form has been fully loaded, initialized and shown,
    ''' useful for handling logic that depends on form initialization or for debugging purposes.
    ''' <para></para>
    ''' This flag is set to <see langword="True"/> at the end of the <see cref="MainForm.MainForm_Shown"/> event handler.
    ''' </summary>
    Friend IsFormLoaded As Boolean

    ''' <summary>
    ''' Indicates whether any plugin is currently executing in the application.
    ''' <para></para>
    ''' Used to prevent the <see cref="MainForm"/> from being closed while a plugin is running and interacting with the UI.
    ''' </summary>
    Friend PluginWorkInProgress As Boolean

    ''' <summary>
    ''' Indicates whether a plugin is running as a result of automatic plugin execution trigger.
    ''' ''' </summary>
    Friend IsRunningPluginsAutomatically As Boolean

    ''' <summary>
    ''' Flag that indicates whether the plugins must run in paralell execution.
    ''' </summary>
    Friend ParalellExecutionEnabled As Boolean

    ''' <summary>
    ''' Keeps track of the last focused button in the plugins navigation pane.
    ''' </summary>
    Friend LastFocusedButtonPane As DarkButtonImageAllignFix

#End Region

#Region " Event Handlers "

    ''' <summary>
    ''' Handles the Load event of the <see cref="MainForm"/>.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SeleniumHelper.InitializeSelenium()

        UIHelper.InitializeLanguageComboBox()
        UIHelper.SetUILanguage()
        JotHelper.StartTrackingComboBoxLanguage()

        Me.Text = AppGlobals.ApplicationTitleAndVersion
        Me.NotifyIcon_Main.Text = My.Application.Info.AssemblyName

        UIHelper.InitializePlugins()

        Me.UseWaitCursor = False
        Cursor.Current = Cursors.Default

        Dim splashScreen As MainSplashScreen = AppGlobals.MainSplashScreenInstance
        If splashScreen.IsHandleCreated Then
            splashScreen.Invoke(Sub() splashScreen.Label_StatusLoad.Text = "Ready to launch.")
        End If

        UIHelper.UpdateStatusLabelText("")
    End Sub

    ''' <summary>
    ''' Handles the Shown event of the <see cref="MainForm"/>.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepperBoundary>
    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        JotHelper.StartTrackingRememberCurrentSettingsCheckBox()
        If Not Me.DarkCheckBox_RememberCurrentSettings.Checked Then
            JotHelper.StopTrackingSettings()
        End If

        JotHelper.StartTrackingOtherCheckBoxes()
        JotHelper.StartTrackingCheckedListBox()
        JotHelper.StartTrackingNumericupDown()

        Me.SuspendLayout()
        If Not Me.DarkCheckBox_RunAppMinimized.Checked Then
            Me.ShowInTaskbar = True
            Interaction.AppActivate(Environment.ProcessId)
        Else
            UIHelper.ToggleMainFormVisibility()
            Me.ActiveControl = Me.DarkSectionPanel_Program
            Me.ShowInTaskbar = True
        End If
        Me.Opacity = 1
        Me.ResumeLayout()

        Me.IsFormLoaded = True
        If Me.DarkCheckBox_SearchProgramUpdates.Checked Then
            ApplicationHelper.SearchAndNotifyProgramUpdateAsync()
        End If
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the <see cref="MainForm"/>.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="FormClosingEventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepperBoundary>
    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If Not Me.DarkCheckBox_RememberCurrentSettings.Checked Then
            JotHelper.StopTrackingSettings()
        End If

        If Me.PluginWorkInProgress Then
            Dim dlgResult As DialogResult =
                MessageBox.Show(Me, My.Resources.Strings.FormCloseWarningPluginWorkInProgress,
                                    My.Application.Info.ProductName,
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If dlgResult = DialogResult.Yes Then
                Me.NotifyIcon_Main.Visible = False
                SeleniumHelper.KillChildrenChromeDriverAndChromeProcesses()
            Else
                e.Cancel = True
            End If
        Else
            Me.NotifyIcon_Main.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the <see cref="DarkButton_Settings"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub DarkButtonSettings_Click(sender As Object, e As EventArgs) Handles DarkButton_Settings.Click

        Me.TabControlNoBorder_Main.SelectTab(Me.TabPage_Settings)
        Me.DarkSectionPanel_Program.Focus()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the <see cref="DarkButton_About"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub DarkButtonAbout_Click(sender As Object, e As EventArgs) Handles DarkButton_About.Click

        My.Forms.AboutBox1.ShowDialog()
        Me.DarkSectionPanel_Program.Focus()
    End Sub

    ''' <summary>
    ''' Handles the DoubleClick event of the <see cref="NotifyIcon_Main"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub NotifyIconMain_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon_Main.DoubleClick

        If Me.IsFormLoaded Then
            UIHelper.ToggleMainFormVisibility()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Opening event of the <see cref="DarkContextMenu_NotifyIcon"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="CancelEventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub DarkContextMenuNotifyIcon_Opening(sender As Object, e As CancelEventArgs) Handles DarkContextMenu_NotifyIcon.Opening

        Me.ToolStripMenuItem_ShowWindow.Enabled = Not Me.Visible
        Me.ToolStripMenuItem_HideWindow.Enabled = Me.Visible
    End Sub

    ''' <summary>
    ''' Handles the Click event of the <see cref="ToolStripMenuItem_ShowWindow"/> item.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub ToolStripMenuItemShowWindow_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_ShowWindow.Click

        If Not Me.Visible Then
            UIHelper.ToggleMainFormVisibility()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the <see cref="ToolStripMenuItem_HideWindow"/> item.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub ToolStripMenuItemHideWindow_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_HideWindow.Click

        If Me.Visible Then
            UIHelper.ToggleMainFormVisibility()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the <see cref="ToolStripMenuItem_CloseProgram"/> item.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub ToolStripMenuItemCloseProgram_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_CloseProgram.Click

        Me.Close()
    End Sub

    ''' <summary>
    ''' Handles the Opening event of the <see cref="DarkContextMenu_AutoRunPluginsListBox"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub DarkContextMenuAutoRunPluginsListBox_Opening(sender As Object, e As CancelEventArgs) Handles DarkContextMenu_AutoRunPluginsListBox.Opening

        Dim cb As PersistableCheckedListBox = Me.CheckedListBox_AutoPluginRun

        Me.ToolStripMenuItem_SelectAllPlugins.Enabled = cb.CheckedIndices.Count <> cb.Items.Count
        Me.ToolStripMenuItem_ClearSelectedPlugins.Enabled = cb.CheckedIndices.Count <> 0
    End Sub

    ''' <summary>
    ''' Handles the Click event of the <see cref="ToolStripMenuItem_SelectAllPlugins"/> item.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub ToolStripMenuItemSelectAllPlugins_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_SelectAllPlugins.Click

        Dim cb As PersistableCheckedListBox = Me.CheckedListBox_AutoPluginRun
        Dim allItems As DynamicPlugin() = cb.Items.Cast(Of DynamicPlugin).ToArray()
        Dim allItemsAsStrings As String() = Array.ConvertAll(allItems, Function(plugin As DynamicPlugin) plugin.ToString())

        cb.CheckedItemsAsStrings = allItemsAsStrings.ToList()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the <see cref="ToolStripMenuItem_ClearSelectedPlugins"/> item.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub ToolStripMenuItemClearSelectedPlugins_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_ClearSelectedPlugins.Click

        Dim cb As PersistableCheckedListBox = Me.CheckedListBox_AutoPluginRun
        cb.CheckedItemsAsStrings = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Opening event of the DarkContextMenu_PluginUrls control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="CancelEventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub DarkContextMenuPluginUrls_Opening(sender As Object, e As CancelEventArgs) Handles DarkContextMenu_PluginUrls.Opening

        Dim cms As DarkContextMenu = DirectCast(sender, DarkContextMenu)
        Dim plugin As DynamicPlugin = DirectCast(cms.SourceControl.Tag, DynamicPlugin)

        For Each item As ToolStripMenuItem In cms.Items

            RemoveHandler item.Click, AddressOf Me.ToolStripMenuItemsPluginUrl_Click

            Select Case item.Tag.ToString()

                Case AppGlobals.TagLoginUrl
                    item.Text = My.Resources.Strings.LoginPageMenuItem
                    item.Enabled = Not String.IsNullOrWhiteSpace(plugin.UrlLogin)

                Case AppGlobals.TagRegistrationUrl
                    item.Text = My.Resources.Strings.RegistrationPageMenuItem
                    item.Enabled = Not String.IsNullOrWhiteSpace(plugin.UrlRegistration)

                Case AppGlobals.TagApplicationUrl
                    item.Text = My.Resources.Strings.ApplicationPageMenuItem
                    item.Enabled = Not String.IsNullOrWhiteSpace(plugin.UrlApplication)

            End Select

            AddHandler item.Click, AddressOf Me.ToolStripMenuItemsPluginUrl_Click
        Next

    End Sub

    ''' <summary>
    ''' Handles the Click event of the 
    ''' <see cref="MainForm.ToolStripMenuItem_PluginUrlLogin"/>,
    ''' <see cref="MainForm.ToolStripMenuItem_PluginUrlRegistration"/> 
    ''' and <see cref="MainForm.ToolStripMenuItem_PluginUrlApplication"/> menu items.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="CancelEventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub ToolStripMenuItemsPluginUrl_Click(sender As Object, e As EventArgs) Handles _
        ToolStripMenuItem_PluginUrlLogin.Click,
        ToolStripMenuItem_PluginUrlRegistration.Click,
        ToolStripMenuItem_PluginUrlApplication.Click

        Dim item As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim cms As DarkContextMenu = DirectCast(item.GetCurrentParent(), DarkContextMenu)
        Dim plugin As DynamicPlugin = DirectCast(cms.SourceControl.Tag, DynamicPlugin)

        ApplicationHelper.ShellOpenPluginUrl(plugin, item.Tag.ToString())
    End Sub

    ''' <summary>
    ''' Handles the SelectedIndexChanged event of the <see cref="DarkComboBox_Language"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Friend Sub DarkComboBoxLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DarkComboBox_Language.SelectedIndexChanged

        Dim cb As ComboBox = DirectCast(sender, ComboBox)
        Dim ci As CultureInfo = DirectCast(cb.SelectedValue, CultureInfo)

        My.Application.ChangeUICulture(ci.Name)
        UIHelper.SetUILanguage()
        UIHelper.InitializeLanguageComboBox()
    End Sub

    ''' <summary>
    ''' Handles the Scroll event of the <see cref="DarkSectionPanel_Plugins"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="MouseEventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub DarkSectionPanelPlugins_Scroll(sender As Object, e As ScrollEventArgs) Handles DarkSectionPanel_Plugins.Scroll

        ' Forces the control to redraw in order to fix a visual glitch in this buggy control
        ' when the vertical scrollbar is visible, where the section header does not render correctly.
        Dim pan As DarkSectionPanel = DirectCast(sender, DarkSectionPanel)
        pan.Invalidate(invalidateChildren:=False)
    End Sub

    ''' <summary>
    ''' Handles the CheckedChanged event of the <see cref="DarkCheckBox_SystemSleep"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub DarkCheckBoxSystemSleep_CheckedChanged(sender As Object, e As EventArgs) Handles DarkCheckBox_SystemSleep.CheckedChanged

        Dim cb As CheckBox = DirectCast(sender, CheckBox)
        If cb.Checked Then
            NativeMethods.SetThreadExecutionState(ExecutionStateFlags.Continuous Or
                                                  ExecutionStateFlags.SystemRequired Or
                                                  ExecutionStateFlags.DisplayRequired)
        Else
            NativeMethods.SetThreadExecutionState(ExecutionStateFlags.Continuous)
        End If
    End Sub

    ''' <summary>
    ''' Handles the CheckedChanged event of the <see cref="DarkCheckBox_ParalellExecution"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub DarkCheckBoxParalellExecution_CheckedChanged(sender As Object, e As EventArgs) Handles DarkCheckBox_ParalellExecution.CheckedChanged

        Dim cb As DarkCheckBox = DirectCast(sender, DarkCheckBox)
        Me.ParalellExecutionEnabled = cb.Checked
    End Sub

    ''' <summary>
    ''' Handles the CheckedChanged event of the <see cref="DarkButtonImageAllignFix_ClearCache"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Async Sub DarkButtonImageAllignFixClearCache_Click(sender As Object, e As EventArgs) Handles DarkButtonImageAllignFix_ClearCache.Click

        Await ApplicationHelper.ClearCache(Nothing)
    End Sub

    ''' <summary>
    ''' Handles the CheckedChanged event of the <see cref="DarkCheckBox_AutoPluginRun"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub DarkCheckBoxAutoPluginRun_CheckedChanged(sender As Object, e As EventArgs) Handles DarkCheckBox_AutoPluginRun.CheckedChanged

        Dim cb As DarkCheckBox = DirectCast(sender, DarkCheckBox)
        Me.CheckedListBox_AutoPluginRun.Enabled = cb.Checked
        Me.DarkCheckBox_ParalellExecution.Enabled = cb.Checked
        Me.Label_AutoRunPluginCheckedCount.Enabled = cb.Checked
        Me.DarkCheckBox_DontRunIfFullscreen.Enabled = cb.Checked
        Me.DarkNumericUpDown_Hours.Enabled = cb.Checked
        Me.Label_Hours.Enabled = cb.Checked
        Me.DarkCheckBox_SystemSleep.Enabled = cb.Checked
        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.Enabled = cb.Checked AndAlso (Me.CheckedListBox_AutoPluginRun.CheckedItems.Count <> 0)

        ApplicationHelper.ResetRemainingAutoPluginRunInterval()

        Dim anyCheckedPlugins As Boolean = Me.CheckedListBox_AutoPluginRun.CheckedItems.Count <> 0
        If Not cb.Checked Then
            Me.Timer_AutoRunPlugins.Enabled = False
            If anyCheckedPlugins Then
                UIHelper.UpdateStatusLabelText(My.Resources.Strings.AutoPluginRunTimerDisabled)
            End If
        Else
            If anyCheckedPlugins Then
                Me.Timer_AutoRunPlugins.Enabled = True
                UIHelper.UpdateStatusLabelText(My.Resources.Strings.AutoPluginRunTimerEnabled)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the <see cref="Label_AutoRunPluginCheckedCount"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub Label_AutoRunPluginCheckedCount_Click(sender As Object, e As EventArgs) Handles Label_AutoRunPluginCheckedCount.Click

        Dim lbl As Label = DirectCast(sender, Label)
        lbl.ContextMenuStrip.Show(lbl, Point.Empty)
    End Sub

    ''' <summary>
    ''' Handles the ValueChanged event of the <see cref="DarkNumericUpDown_Hours"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub DarkNumericUpDown_Hours_ValueChanged(sender As Object, e As EventArgs) Handles DarkNumericUpDown_Hours.ValueChanged

        Dim nupd As DarkNumericUpDown = DirectCast(sender, DarkNumericUpDown)
        AppGlobals.AutomaticPluginRunInterval = TimeSpan.FromHours(nupd.Value)

        If Me.DarkCheckBox_AutoPluginRun.Checked Then
            ApplicationHelper.ResetRemainingAutoPluginRunInterval()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ItemCheck and Click events 
    ''' of the <see cref="DarkButtonImageAllignFix_RunAllSelectedPluginsNow"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub DarkButtonImageAllignFixRunAllSelectedPluginsNow_Click(sender As Object, e As EventArgs) Handles DarkButtonImageAllignFix_RunAllSelectedPluginsNow.Click

        Me.RemainingAutoPluginRunInterval = TimeSpan.Zero
        ' Me.TimerAutoRunPlugins_Tick(Me.Timer_AutoRunPlugins, EventArgs.Empty)
    End Sub

    ''' <summary>
    ''' Handles the ItemCheck and PropertyCheckedItemsAsStringChanged events 
    ''' of the <see cref="CheckedListBox_AutoPluginRun"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub CheckedListBoxAutoPluginRun_ItemCheck_PropertyCheckedItemsAsStringChanged(sender As Object, e As EventArgs) _
        Handles CheckedListBox_AutoPluginRun.ItemCheck,
                CheckedListBox_AutoPluginRun.PropertyCheckedItemsAsStringChanged

        Dim clb As PersistableCheckedListBox = DirectCast(sender, PersistableCheckedListBox)

        Dim itemsCount As Integer = clb.Items.Count
        Dim checkedCount As Integer = clb.CheckedItems.Count
        If TypeOf e Is ItemCheckEventArgs Then
            Dim eventData As ItemCheckEventArgs = DirectCast(e, ItemCheckEventArgs)
            checkedCount += If(eventData.NewValue = CheckState.Checked, +1, -1)
        End If

        If checkedCount = 0 Then
            Me.Label_AutoRunPluginCheckedCount.Text = String.Format(My.Resources.Strings.AutoRunPluginCheckedNoneFormat, itemsCount)

        ElseIf checkedCount = itemsCount Then
            Me.Label_AutoRunPluginCheckedCount.Text = String.Format(My.Resources.Strings.AutoRunPluginCheckedAllFormat, checkedCount, itemsCount)

        Else
            Me.Label_AutoRunPluginCheckedCount.Text = String.Format(My.Resources.Strings.AutoRunPluginCheckedCountFormat, checkedCount, itemsCount)
        End If

        ' This occurs when selected items were restored by Jot.
        If Not Me.DarkCheckBox_AutoPluginRun.Checked Then
            Exit Sub
        End If

        ' Updates the state of the "Timer_AutoRunPlugins"
        ' and the status label based on the number of checked items.
        Me.Timer_AutoRunPlugins.Enabled = checkedCount <> 0

        If Not Me.Timer_AutoRunPlugins.Enabled Then
            UIHelper.UpdateStatusLabelText(My.Resources.Strings.AutoPluginRunTimerDisabled)
            ApplicationHelper.ResetRemainingAutoPluginRunInterval()
        Else
            If Me.RemainingAutoPluginRunInterval = AppGlobals.AutomaticPluginRunInterval Then
                UIHelper.UpdateStatusLabelText(My.Resources.Strings.AutoPluginRunTimerEnabled)
            End If
        End If

        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.Enabled = checkedCount <> 0
    End Sub

    ''' <summary>
    ''' Handles the Tick event of the TimerAutoRunPlugins control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepThrough>
    Private Async Sub TimerAutoRunPlugins_Tick(sender As Object, e As EventArgs) Handles Timer_AutoRunPlugins.Tick

        ' If a plugin execution is in progress...
        If Not Me.DarkSectionPanel_Plugins.Enabled Then
            Exit Sub
        End If

        Me.RemainingAutoPluginRunInterval =
            Me.RemainingAutoPluginRunInterval.Subtract(TimeSpan.FromMilliseconds(Me.Timer_AutoRunPlugins.Interval))

        If Me.RemainingAutoPluginRunInterval.TotalMilliseconds <= 0 Then
            Me.Timer_AutoRunPlugins.Stop()

            If Me.DarkCheckBox_DontRunIfFullscreen.Checked Then
                Do While ApplicationHelper.IsFullscreenAppRunning()
                    Await Task.Delay(TimeSpan.FromSeconds(10))
                Loop
            End If

            Dim pluginsToRun As List(Of DynamicPlugin) =
                Me.CheckedListBox_AutoPluginRun.CheckedItems.Cast(Of DynamicPlugin)?.ToList()

            Me.NotifyIcon_Main.ShowBalloonTip(5000, My.Application.Info.AssemblyName, My.Resources.Strings.ExecutingPluginsTooltip, ToolTipIcon.Info)

            Me.IsRunningPluginsAutomatically = True
            Me.PluginWorkInProgress = True

            If Me.ParalellExecutionEnabled Then
                Dim chromeMemPerInstanceGB As Double = 0.5 ' 500 MB estimated.
                Dim freeMemGB As Double = My.Computer.Info.AvailablePhysicalMemory / (1024 ^ 3)
                Dim maxByMem As Integer = CInt(Math.Min(8, Math.Floor(freeMemGB / chromeMemPerInstanceGB)))
                Dim maxByCpu As Integer = Environment.ProcessorCount \ 2
                Dim maxChromeInstances As Integer = Math.Max(2, Math.Min(Math.Min(maxByMem, maxByCpu), 8))

                Dim parallelOptions As New ParallelOptions With {
                    .MaxDegreeOfParallelism = maxChromeInstances
                }
                Await Parallel.ForEachAsync(pluginsToRun, parallelOptions,
                          Function(plugin, ct)
                              Return New ValueTask(
                                  Task.Run(
                                      Async Function()
                                          Dim regFlags As RegistrationFlags = Await UIHelper.RunPluginFromButtonAsync(plugin)
                                          UIHelper.UpdateStatusLabelText(plugin, regFlags)
                                      End Function, ct))
                          End Function)

            Else
                For Each plugin As DynamicPlugin In pluginsToRun
                    Dim regFlags As RegistrationFlags = Await UIHelper.RunPluginFromButtonAsync(plugin)
                    UIHelper.UpdateStatusLabelText(plugin, regFlags)
                Next

            End If

            Me.IsRunningPluginsAutomatically = False
            Me.PluginWorkInProgress = False

            UIHelper.ToggleControlsEnabledState(enable:=True)
            Me.UseWaitCursor = False
            Cursor.Current = Cursors.Default

            NotifyIconExtensions.CloseBallontip(Me.NotifyIcon_Main)
            ApplicationHelper.ResetRemainingAutoPluginRunInterval()
            Me.Timer_AutoRunPlugins.Start()
        End If

        UIHelper.UpdateStatusLabelText()
    End Sub

#End Region

#Region " Restricted Methods "

    ''' <summary>
    ''' Processes a command key.
    ''' </summary>
    ''' 
    ''' <param name="msg">
    ''' A <see cref="Message"/>, passed by reference, that represents the Win32 message to process.
    ''' </param>
    ''' 
    ''' <param name="keyData">
    ''' One of the <see cref="Keys"/> values that represents the key to process.
    ''' </param>
    ''' 
    ''' <returns>
    ''' <see langword="True"/> if the keystroke was processed and consumed by the control; 
    ''' otherwise, <see langword="False"/> to allow further processing.
    ''' </returns>
    <DebuggerStepThrough>
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean

        Dim activeControl As Control = Me.ActiveControl

        ' Allows navigation through arrow keys and [a-Z 0-9] in the butons of the plugins pane.
        Dim sectionPanel As DarkSectionPanel = Me.DarkSectionPanel_Plugins
        If sectionPanel.Controls.Contains(activeControl) Then
            Dim canFocus As Boolean = UIHelper.CanFocusNextPluginButtonControl(activeControl, keyData)
            Return Not canFocus
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

#End Region

End Class
