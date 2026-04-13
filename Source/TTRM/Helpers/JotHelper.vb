#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO

Imports Microsoft.VisualBasic.ApplicationServices

Imports DarkUI.Controls

Imports Jot
Imports Jot.Configuration
Imports Jot.Storage

#End Region

''' <summary>
''' Provides helper members related to JOT operations.
''' </summary>
Friend Module JotHelper

#Region " Restricted Fields "

    ''' <summary>
    ''' The object responsible for tracking the specified properties of the specified target objects
    ''' in <see cref="JotHelper.formTrackingConfig"/> and <see cref="JotHelper.menuItemTrackingConfig"/>.
    ''' <para></para>
    ''' Tracking means persisting the values of the specified object properties,
    ''' and restoring this data when appropriate.
    ''' </summary>
    Private jotTracker As Tracker

    ''' <summary>
    ''' The object that determines how the target <see cref="MainForm.DarkCheckBox_RememberCurrentSettings"/> will be tracked. 
    ''' <para></para>
    ''' This includes list of properties to track, persist triggers and id getter.
    ''' </summary>
    Private checkBoxRememberCurrentSettingsTrackingConfig As TrackingConfiguration(Of DarkCheckBox)

    ''' <summary>
    ''' The object that determines how the target <see cref="DarkCheckBox"/> will be tracked. 
    ''' <para></para>
    ''' This includes list of properties to track, persist triggers and id getter.
    ''' </summary>
    Private checkBoxOtherTrackingConfig As TrackingConfiguration(Of DarkCheckBox)

    ''' <summary>
    ''' The object that determines how a target <see cref="PersistableCheckedListBox"/> will be tracked. 
    ''' <para></para>
    ''' This includes list of properties to track, persist triggers and id getter.
    ''' </summary>
    Private checkedListBoxTrackingConfig As TrackingConfiguration(Of PersistableCheckedListBox)

    ''' <summary>
    ''' The object that determines how the target <see cref="MainForm.DarkComboBox_Language"/> will be tracked. 
    ''' <para></para>
    ''' This includes list of properties to track, persist triggers and id getter.
    ''' </summary>
    Private comboBoxLanguageTrackingConfig As TrackingConfiguration(Of DarkComboBox)

    ''' <summary>
    ''' The object that determines how the target <see cref="MainForm.DarkNumericUpDown_Hours"/> will be tracked. 
    ''' <para></para>
    ''' This includes list of properties to track, persist triggers and id getter.
    ''' </summary>
    Private numericUpDownHoursTrackingConfig As TrackingConfiguration(Of DarkNumericUpDown)

#End Region

#Region " Restricted Methods "

    ''' <summary>
    ''' Initializes JOT environment.
    ''' <para></para>
    ''' This method should be called on your main application thread (typically the UI thread)
    ''' and typically from the <see cref="WindowsFormsApplicationBase.Startup"/> event,
    ''' before the <see cref="MainForm"/> form is loaded.
    ''' </summary>
    <DebuggerStepperBoundary>
    Friend Sub InitializeJot()

        JotHelper.jotTracker = New Tracker()
        DirectCast(JotHelper.jotTracker.Store, JsonFileStore).FolderPath = AppGlobals.JotCachePath

        JotHelper.checkBoxRememberCurrentSettingsTrackingConfig = JotHelper.jotTracker.Configure(Of DarkCheckBox)
        With JotHelper.checkBoxRememberCurrentSettingsTrackingConfig
            .Id(Function(i As DarkCheckBox) i.Name)
            .Properties(Function(i As DarkCheckBox) New With {i.Checked})
            .PersistOn(NameOf(DarkCheckBox.CheckedChanged))
            .StopTrackingOn(NameOf(DarkCheckBox.Disposed))
        End With

        JotHelper.checkBoxOtherTrackingConfig = JotHelper.jotTracker.Configure(Of DarkCheckBox)
        With JotHelper.checkBoxOtherTrackingConfig
            .Id(Function(i As DarkCheckBox) i.Name)
            .Properties(Function(i As DarkCheckBox) New With {i.Checked})
            .PersistOn(NameOf(DarkCheckBox.CheckedChanged))
            .StopTrackingOn(NameOf(DarkCheckBox.Disposed))
        End With

        JotHelper.checkedListBoxTrackingConfig = JotHelper.jotTracker.Configure(Of PersistableCheckedListBox)
        With JotHelper.checkedListBoxTrackingConfig
            .Id(Function(i) i.Name)
            .Properties(Function(i) New With {i.CheckedItemsAsStrings})
            .PersistOn(NameOf(PersistableCheckedListBox.ItemCheck))
            .PersistOn(NameOf(PersistableCheckedListBox.MouseUp))
            .PersistOn(NameOf(PersistableCheckedListBox.KeyUp))
            .PersistOn(NameOf(PersistableCheckedListBox.HandleDestroyed))
            .StopTrackingOn(NameOf(PersistableCheckedListBox.Disposed))
        End With

        JotHelper.comboBoxLanguageTrackingConfig = JotHelper.jotTracker.Configure(Of DarkComboBox)
        With JotHelper.comboBoxLanguageTrackingConfig
            .Id(Function(i As DarkComboBox) i.Name)
            .Properties(Function(i As DarkComboBox) New With {i.SelectedIndex})
            .PersistOn(NameOf(DarkComboBox.SelectedIndexChanged))
            .StopTrackingOn(NameOf(DarkComboBox.Disposed))
        End With

        JotHelper.numericUpDownHoursTrackingConfig = JotHelper.jotTracker.Configure(Of DarkNumericUpDown)
        With JotHelper.numericUpDownHoursTrackingConfig
            .Id(Function(i As DarkNumericUpDown) i.Name)
            .Properties(Function(i As DarkNumericUpDown) New With {i.Value})
            .PersistOn(NameOf(DarkNumericUpDown.ValueChanged))
            .StopTrackingOn(NameOf(DarkNumericUpDown.Disposed))
        End With

    End Sub

    ''' <summary>
    ''' Starts tracking the <see cref="MainForm.DarkCheckBox_RememberCurrentSettings"/> checked state.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub StartTrackingRememberCurrentSettingsCheckBox()

        JotHelper.checkBoxRememberCurrentSettingsTrackingConfig.Track(My.Forms.MainForm.DarkCheckBox_RememberCurrentSettings)
    End Sub

    ''' <summary>
    ''' Starts tracking the <see cref="DarkCheckBox"/> checked states.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub StartTrackingOtherCheckBoxes()

        JotHelper.checkBoxOtherTrackingConfig.Track(My.Forms.MainForm.DarkCheckBox_AutoPluginRun)
        JotHelper.checkBoxOtherTrackingConfig.Track(My.Forms.MainForm.DarkCheckBox_ParalellExecution)
        JotHelper.checkBoxOtherTrackingConfig.Track(My.Forms.MainForm.DarkCheckBox_SystemSleep)
        JotHelper.checkBoxOtherTrackingConfig.Track(My.Forms.MainForm.DarkCheckBox_RunAppMinimized)
        JotHelper.checkBoxOtherTrackingConfig.Track(My.Forms.MainForm.DarkCheckBox_ClearPreviousLogEntries)
        JotHelper.checkBoxOtherTrackingConfig.Track(My.Forms.MainForm.DarkCheckBox_AllowPluginApplicationFormCheck)
        JotHelper.checkBoxOtherTrackingConfig.Track(My.Forms.MainForm.DarkCheckBox_DontRunIfFullscreen)
        JotHelper.checkBoxOtherTrackingConfig.Track(My.Forms.MainForm.DarkCheckBox_SearchProgramUpdates)
    End Sub

    ''' <summary>
    ''' Starts tracking the <see cref="PersistableCheckedListBox"/> items.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub StartTrackingCheckedListBox()

        JotHelper.checkedListBoxTrackingConfig.Track(My.Forms.MainForm.CheckedListBox_AutoPluginRun)
    End Sub

    ''' <summary>
    ''' Starts tracking the <see cref="MainForm.DarkComboBox_Language"/> value.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub StartTrackingComboBoxLanguage()

        JotHelper.comboBoxLanguageTrackingConfig.Track(My.Forms.MainForm.DarkComboBox_Language)
    End Sub

    ''' <summary>
    ''' Starts tracking the <see cref="MainForm.DarkNumericUpDown_Hours"/> value.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub StartTrackingNumericupDown()

        JotHelper.numericUpDownHoursTrackingConfig.Track(My.Forms.MainForm.DarkNumericUpDown_Hours)
    End Sub

    ''' <summary>
    ''' Stops tracking the settings of the application.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Sub StopTrackingSettings()
        Try

            JotHelper.jotTracker.StopTracking(My.Forms.MainForm.DarkCheckBox_RememberCurrentSettings)
            JotHelper.jotTracker.StopTracking(My.Forms.MainForm.DarkCheckBox_AutoPluginRun)
            JotHelper.jotTracker.StopTracking(My.Forms.MainForm.DarkCheckBox_ParalellExecution)
            JotHelper.jotTracker.StopTracking(My.Forms.MainForm.DarkCheckBox_SystemSleep)
            JotHelper.jotTracker.StopTracking(My.Forms.MainForm.DarkCheckBox_RunAppMinimized)
            JotHelper.jotTracker.StopTracking(My.Forms.MainForm.DarkCheckBox_ClearPreviousLogEntries)
            JotHelper.jotTracker.StopTracking(My.Forms.MainForm.DarkCheckBox_AllowPluginApplicationFormCheck)
            JotHelper.jotTracker.StopTracking(My.Forms.MainForm.DarkCheckBox_DontRunIfFullscreen)
            JotHelper.jotTracker.StopTracking(My.Forms.MainForm.DarkCheckBox_SearchProgramUpdates)
            JotHelper.jotTracker.StopTracking(My.Forms.MainForm.CheckedListBox_AutoPluginRun)
            JotHelper.jotTracker.StopTracking(My.Forms.MainForm.DarkComboBox_Language)

            JotHelper.jotTracker.Forget(My.Forms.MainForm.DarkCheckBox_RememberCurrentSettings)
            JotHelper.jotTracker.Forget(My.Forms.MainForm.DarkCheckBox_AutoPluginRun)
            JotHelper.jotTracker.Forget(My.Forms.MainForm.DarkCheckBox_ParalellExecution)
            JotHelper.jotTracker.Forget(My.Forms.MainForm.DarkCheckBox_SystemSleep)
            JotHelper.jotTracker.Forget(My.Forms.MainForm.DarkCheckBox_RunAppMinimized)
            JotHelper.jotTracker.Forget(My.Forms.MainForm.DarkCheckBox_ClearPreviousLogEntries)
            JotHelper.jotTracker.Forget(My.Forms.MainForm.DarkCheckBox_AllowPluginApplicationFormCheck)
            JotHelper.jotTracker.Forget(My.Forms.MainForm.DarkCheckBox_DontRunIfFullscreen)
            JotHelper.jotTracker.Forget(My.Forms.MainForm.DarkCheckBox_SearchProgramUpdates)
            JotHelper.jotTracker.Forget(My.Forms.MainForm.CheckedListBox_AutoPluginRun)
            JotHelper.jotTracker.Forget(My.Forms.MainForm.DarkComboBox_Language)

            JotHelper.jotTracker.ApplyDefaults(My.Forms.MainForm.DarkCheckBox_RememberCurrentSettings)
            JotHelper.jotTracker.ApplyDefaults(My.Forms.MainForm.DarkCheckBox_AutoPluginRun)
            JotHelper.jotTracker.ApplyDefaults(My.Forms.MainForm.DarkCheckBox_ParalellExecution)
            JotHelper.jotTracker.ApplyDefaults(My.Forms.MainForm.DarkCheckBox_SystemSleep)
            JotHelper.jotTracker.ApplyDefaults(My.Forms.MainForm.DarkCheckBox_RunAppMinimized)
            JotHelper.jotTracker.ApplyDefaults(My.Forms.MainForm.DarkCheckBox_ClearPreviousLogEntries)
            JotHelper.jotTracker.ApplyDefaults(My.Forms.MainForm.DarkCheckBox_AllowPluginApplicationFormCheck)
            JotHelper.jotTracker.ApplyDefaults(My.Forms.MainForm.DarkCheckBox_DontRunIfFullscreen)
            JotHelper.jotTracker.ApplyDefaults(My.Forms.MainForm.DarkCheckBox_SearchProgramUpdates)
            JotHelper.jotTracker.ApplyDefaults(My.Forms.MainForm.CheckedListBox_AutoPluginRun)
            JotHelper.jotTracker.ApplyDefaults(My.Forms.MainForm.DarkComboBox_Language)

            Dim store As JsonFileStore = DirectCast(JotHelper.jotTracker.Store, JsonFileStore)
            store.ClearData(JotHelper.checkBoxRememberCurrentSettingsTrackingConfig.GetStoreId(My.Forms.MainForm.DarkCheckBox_RememberCurrentSettings))
            store.ClearData(JotHelper.checkBoxOtherTrackingConfig.GetStoreId(My.Forms.MainForm.DarkCheckBox_AutoPluginRun))
            store.ClearData(JotHelper.checkBoxOtherTrackingConfig.GetStoreId(My.Forms.MainForm.DarkCheckBox_ParalellExecution))
            store.ClearData(JotHelper.checkBoxOtherTrackingConfig.GetStoreId(My.Forms.MainForm.DarkCheckBox_SystemSleep))
            store.ClearData(JotHelper.checkBoxOtherTrackingConfig.GetStoreId(My.Forms.MainForm.DarkCheckBox_RunAppMinimized))
            store.ClearData(JotHelper.checkBoxOtherTrackingConfig.GetStoreId(My.Forms.MainForm.DarkCheckBox_ClearPreviousLogEntries))
            store.ClearData(JotHelper.checkBoxOtherTrackingConfig.GetStoreId(My.Forms.MainForm.DarkCheckBox_AllowPluginApplicationFormCheck))
            store.ClearData(JotHelper.checkBoxOtherTrackingConfig.GetStoreId(My.Forms.MainForm.DarkCheckBox_DontRunIfFullscreen))
            store.ClearData(JotHelper.checkBoxOtherTrackingConfig.GetStoreId(My.Forms.MainForm.DarkCheckBox_SearchProgramUpdates))
            store.ClearData(JotHelper.checkedListBoxTrackingConfig.GetStoreId(My.Forms.MainForm.CheckedListBox_AutoPluginRun))
            store.ClearData(JotHelper.comboBoxLanguageTrackingConfig.GetStoreId(My.Forms.MainForm.DarkComboBox_Language))
        Catch ex As DirectoryNotFoundException
            ' Directory may have been deleted by user. Ignore.
        End Try
    End Sub

#End Region

End Module
