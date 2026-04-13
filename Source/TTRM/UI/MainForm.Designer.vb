<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class MainForm
    Inherits DarkUI.Forms.DarkForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.TableLayoutPanel_Main = New TableLayoutPanel()
        Me.TabControlNoBorder_Main = New TabControlNoBorder()
        Me.TabPage_Settings = New TabPage()
        Me.DarkSectionPanel_Settings = New DarkUI.Controls.DarkSectionPanel()
        Me.DarkGroupBox_Application = New DarkUI.Controls.DarkGroupBox()
        Me.DarkCheckBox_SearchProgramUpdates = New DarkUI.Controls.DarkCheckBox()
        Me.DarkCheckBox_AllowPluginApplicationFormCheck = New DarkUI.Controls.DarkCheckBox()
        Me.DarkCheckBox_ClearPreviousLogEntries = New DarkUI.Controls.DarkCheckBox()
        Me.DarkCheckBox_RunAppMinimized = New DarkUI.Controls.DarkCheckBox()
        Me.DarkButtonImageAllignFix_ClearCache = New DarkButtonImageAllignFix()
        Me.Label_Language = New Label()
        Me.DarkComboBox_Language = New DarkUI.Controls.DarkComboBox()
        Me.DarkGroupBox_AutoPluginRun = New DarkUI.Controls.DarkGroupBox()
        Me.Label_Hours = New Label()
        Me.DarkContextMenu_AutoRunPluginsListBox = New DarkUI.Controls.DarkContextMenu()
        Me.ToolStripMenuItem_SelectAllPlugins = New ToolStripMenuItem()
        Me.ToolStripMenuItem_ClearSelectedPlugins = New ToolStripMenuItem()
        Me.DarkNumericUpDown_Hours = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkCheckBox_DontRunIfFullscreen = New DarkUI.Controls.DarkCheckBox()
        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow = New DarkButtonImageAllignFix()
        Me.DarkCheckBox_SystemSleep = New DarkUI.Controls.DarkCheckBox()
        Me.Label_AutoRunPluginCheckedCount = New Label()
        Me.DarkCheckBox_ParalellExecution = New DarkUI.Controls.DarkCheckBox()
        Me.CheckedListBox_AutoPluginRun = New PersistableCheckedListBox()
        Me.DarkCheckBox_AutoPluginRun = New DarkUI.Controls.DarkCheckBox()
        Me.DarkCheckBox_RememberCurrentSettings = New DarkUI.Controls.DarkCheckBox()
        Me.TableLayoutPanel1 = New TableLayoutPanel()
        Me.DarkSectionPanel_Plugins = New DarkUI.Controls.DarkSectionPanel()
        Me.DarkSectionPanel_Program = New DarkUI.Controls.DarkSectionPanel()
        Me.TableLayoutPanel2 = New TableLayoutPanel()
        Me.DarkButton_About = New DarkButtonImageAllignFix()
        Me.DarkButton_Settings = New DarkButtonImageAllignFix()
        Me.NotifyIcon_Main = New NotifyIcon(Me.components)
        Me.DarkContextMenu_NotifyIcon = New DarkUI.Controls.DarkContextMenu()
        Me.ToolStripMenuItem_ShowWindow = New ToolStripMenuItem()
        Me.ToolStripMenuItem_HideWindow = New ToolStripMenuItem()
        Me.ToolStripMenuItem_CloseProgram = New ToolStripMenuItem()
        Me.Timer_AutoRunPlugins = New Timer(Me.components)
        Me.StatusStrip1 = New StatusStrip()
        Me.ToolStripStatusLabel1 = New ToolStripStatusLabel()
        Me.DarkContextMenu_PluginUrls = New DarkUI.Controls.DarkContextMenu()
        Me.ToolStripMenuItem_PluginUrlLogin = New ToolStripMenuItem()
        Me.ToolStripMenuItem_PluginUrlRegistration = New ToolStripMenuItem()
        Me.ToolStripMenuItem_PluginUrlApplication = New ToolStripMenuItem()
        Me.TableLayoutPanel_Main.SuspendLayout()
        Me.TabControlNoBorder_Main.SuspendLayout()
        Me.TabPage_Settings.SuspendLayout()
        Me.DarkSectionPanel_Settings.SuspendLayout()
        Me.DarkGroupBox_Application.SuspendLayout()
        Me.DarkGroupBox_AutoPluginRun.SuspendLayout()
        Me.DarkContextMenu_AutoRunPluginsListBox.SuspendLayout()
        CType(Me.DarkNumericUpDown_Hours, ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.DarkSectionPanel_Program.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.DarkContextMenu_NotifyIcon.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.DarkContextMenu_PluginUrls.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' TableLayoutPanel_Main
        ' 
        Me.TableLayoutPanel_Main.ColumnCount = 2
        Me.TableLayoutPanel_Main.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 29.0284367F))
        Me.TableLayoutPanel_Main.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 70.9715652F))
        Me.TableLayoutPanel_Main.Controls.Add(Me.TabControlNoBorder_Main, 1, 0)
        Me.TableLayoutPanel_Main.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.TableLayoutPanel_Main.Dock = DockStyle.Fill
        Me.TableLayoutPanel_Main.Location = New Point(0, 0)
        Me.TableLayoutPanel_Main.Name = "TableLayoutPanel_Main"
        Me.TableLayoutPanel_Main.RowCount = 1
        Me.TableLayoutPanel_Main.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        Me.TableLayoutPanel_Main.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        Me.TableLayoutPanel_Main.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        Me.TableLayoutPanel_Main.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        Me.TableLayoutPanel_Main.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        Me.TableLayoutPanel_Main.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        Me.TableLayoutPanel_Main.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        Me.TableLayoutPanel_Main.Size = New Size(824, 539)
        Me.TableLayoutPanel_Main.TabIndex = 0
        ' 
        ' TabControlNoBorder_Main
        ' 
        Me.TabControlNoBorder_Main.Appearance = TabAppearance.FlatButtons
        Me.TabControlNoBorder_Main.Controls.Add(Me.TabPage_Settings)
        Me.TabControlNoBorder_Main.Dock = DockStyle.Fill
        Me.TabControlNoBorder_Main.ItemSize = New Size(0, 1)
        Me.TabControlNoBorder_Main.Location = New Point(239, 0)
        Me.TabControlNoBorder_Main.Margin = New Padding(0)
        Me.TabControlNoBorder_Main.Name = "TabControlNoBorder_Main"
        Me.TabControlNoBorder_Main.Padding = New Point(0, 0)
        Me.TabControlNoBorder_Main.SelectedIndex = 0
        Me.TabControlNoBorder_Main.ShowTabHeader = False
        Me.TabControlNoBorder_Main.Size = New Size(585, 539)
        Me.TabControlNoBorder_Main.SizeMode = TabSizeMode.Fixed
        Me.TabControlNoBorder_Main.TabIndex = 1
        ' 
        ' TabPage_Settings
        ' 
        Me.TabPage_Settings.Controls.Add(Me.DarkSectionPanel_Settings)
        Me.TabPage_Settings.Location = New Point(0, 0)
        Me.TabPage_Settings.Name = "TabPage_Settings"
        Me.TabPage_Settings.Size = New Size(585, 539)
        Me.TabPage_Settings.TabIndex = 2
        Me.TabPage_Settings.Text = "Settings"
        Me.TabPage_Settings.UseVisualStyleBackColor = True
        ' 
        ' DarkSectionPanel_Settings
        ' 
        Me.DarkSectionPanel_Settings.Controls.Add(Me.DarkGroupBox_Application)
        Me.DarkSectionPanel_Settings.Controls.Add(Me.DarkGroupBox_AutoPluginRun)
        Me.DarkSectionPanel_Settings.Controls.Add(Me.DarkCheckBox_RememberCurrentSettings)
        Me.DarkSectionPanel_Settings.Dock = DockStyle.Fill
        Me.DarkSectionPanel_Settings.Location = New Point(0, 0)
        Me.DarkSectionPanel_Settings.Margin = New Padding(0)
        Me.DarkSectionPanel_Settings.Name = "DarkSectionPanel_Settings"
        Me.DarkSectionPanel_Settings.SectionHeader = "Settings"
        Me.DarkSectionPanel_Settings.Size = New Size(585, 539)
        Me.DarkSectionPanel_Settings.TabIndex = 0
        ' 
        ' DarkGroupBox_Application
        ' 
        Me.DarkGroupBox_Application.BorderColor = Color.FromArgb(CByte(51), CByte(51), CByte(51))
        Me.DarkGroupBox_Application.Controls.Add(Me.DarkCheckBox_SearchProgramUpdates)
        Me.DarkGroupBox_Application.Controls.Add(Me.DarkCheckBox_AllowPluginApplicationFormCheck)
        Me.DarkGroupBox_Application.Controls.Add(Me.DarkCheckBox_ClearPreviousLogEntries)
        Me.DarkGroupBox_Application.Controls.Add(Me.DarkCheckBox_RunAppMinimized)
        Me.DarkGroupBox_Application.Controls.Add(Me.DarkButtonImageAllignFix_ClearCache)
        Me.DarkGroupBox_Application.Controls.Add(Me.Label_Language)
        Me.DarkGroupBox_Application.Controls.Add(Me.DarkComboBox_Language)
        Me.DarkGroupBox_Application.Location = New Point(8, 342)
        Me.DarkGroupBox_Application.Name = "DarkGroupBox_Application"
        Me.DarkGroupBox_Application.Size = New Size(565, 162)
        Me.DarkGroupBox_Application.TabIndex = 1
        Me.DarkGroupBox_Application.TabStop = False
        Me.DarkGroupBox_Application.Text = "Application"
        ' 
        ' DarkCheckBox_SearchProgramUpdates
        ' 
        Me.DarkCheckBox_SearchProgramUpdates.Cursor = Cursors.Hand
        Me.DarkCheckBox_SearchProgramUpdates.Location = New Point(6, 28)
        Me.DarkCheckBox_SearchProgramUpdates.Name = "DarkCheckBox_SearchProgramUpdates"
        Me.DarkCheckBox_SearchProgramUpdates.Size = New Size(321, 25)
        Me.DarkCheckBox_SearchProgramUpdates.TabIndex = 6
        Me.DarkCheckBox_SearchProgramUpdates.Text = "Search program updates"
        ' 
        ' DarkCheckBox_AllowPluginApplicationFormCheck
        ' 
        Me.DarkCheckBox_AllowPluginApplicationFormCheck.Cursor = Cursors.Hand
        Me.DarkCheckBox_AllowPluginApplicationFormCheck.Location = New Point(6, 116)
        Me.DarkCheckBox_AllowPluginApplicationFormCheck.Name = "DarkCheckBox_AllowPluginApplicationFormCheck"
        Me.DarkCheckBox_AllowPluginApplicationFormCheck.Size = New Size(440, 40)
        Me.DarkCheckBox_AllowPluginApplicationFormCheck.TabIndex = 1
        Me.DarkCheckBox_AllowPluginApplicationFormCheck.Text = "Allow plugins to notify about open application forms"
        ' 
        ' DarkCheckBox_ClearPreviousLogEntries
        ' 
        Me.DarkCheckBox_ClearPreviousLogEntries.Cursor = Cursors.Hand
        Me.DarkCheckBox_ClearPreviousLogEntries.Location = New Point(6, 90)
        Me.DarkCheckBox_ClearPreviousLogEntries.Name = "DarkCheckBox_ClearPreviousLogEntries"
        Me.DarkCheckBox_ClearPreviousLogEntries.Size = New Size(440, 25)
        Me.DarkCheckBox_ClearPreviousLogEntries.TabIndex = 0
        Me.DarkCheckBox_ClearPreviousLogEntries.Text = "Clear previous log entries on plugin execution"
        ' 
        ' DarkCheckBox_RunAppMinimized
        ' 
        Me.DarkCheckBox_RunAppMinimized.Cursor = Cursors.Hand
        Me.DarkCheckBox_RunAppMinimized.Location = New Point(6, 59)
        Me.DarkCheckBox_RunAppMinimized.Name = "DarkCheckBox_RunAppMinimized"
        Me.DarkCheckBox_RunAppMinimized.Size = New Size(440, 25)
        Me.DarkCheckBox_RunAppMinimized.TabIndex = 2
        Me.DarkCheckBox_RunAppMinimized.Text = "Run application minimized to system-tray"
        ' 
        ' DarkButtonImageAllignFix_ClearCache
        ' 
        Me.DarkButtonImageAllignFix_ClearCache.Anchor = AnchorStyles.None
        Me.DarkButtonImageAllignFix_ClearCache.Cursor = Cursors.Hand
        Me.DarkButtonImageAllignFix_ClearCache.Location = New Point(452, 33)
        Me.DarkButtonImageAllignFix_ClearCache.Name = "DarkButtonImageAllignFix_ClearCache"
        Me.DarkButtonImageAllignFix_ClearCache.Padding = New Padding(5)
        Me.DarkButtonImageAllignFix_ClearCache.ResizedImage = My.Resources.Resources.clean
        Me.DarkButtonImageAllignFix_ClearCache.Size = New Size(107, 53)
        Me.DarkButtonImageAllignFix_ClearCache.TabIndex = 3
        Me.DarkButtonImageAllignFix_ClearCache.Text = "Clear cache"
        Me.DarkButtonImageAllignFix_ClearCache.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' Label_Language
        ' 
        Me.Label_Language.AutoSize = True
        Me.Label_Language.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.Label_Language.ForeColor = Color.Gainsboro
        Me.Label_Language.Location = New Point(452, 90)
        Me.Label_Language.Name = "Label_Language"
        Me.Label_Language.Size = New Size(78, 21)
        Me.Label_Language.TabIndex = 4
        Me.Label_Language.Text = "Language"
        ' 
        ' DarkComboBox_Language
        ' 
        Me.DarkComboBox_Language.Cursor = Cursors.Hand
        Me.DarkComboBox_Language.DrawMode = DrawMode.OwnerDrawVariable
        Me.DarkComboBox_Language.FormattingEnabled = True
        Me.DarkComboBox_Language.Items.AddRange(New Object() {"English", "Spanish"})
        Me.DarkComboBox_Language.Location = New Point(452, 114)
        Me.DarkComboBox_Language.Name = "DarkComboBox_Language"
        Me.DarkComboBox_Language.Size = New Size(107, 30)
        Me.DarkComboBox_Language.TabIndex = 5
        ' 
        ' DarkGroupBox_AutoPluginRun
        ' 
        Me.DarkGroupBox_AutoPluginRun.BorderColor = Color.FromArgb(CByte(51), CByte(51), CByte(51))
        Me.DarkGroupBox_AutoPluginRun.Controls.Add(Me.Label_Hours)
        Me.DarkGroupBox_AutoPluginRun.Controls.Add(Me.DarkNumericUpDown_Hours)
        Me.DarkGroupBox_AutoPluginRun.Controls.Add(Me.DarkCheckBox_DontRunIfFullscreen)
        Me.DarkGroupBox_AutoPluginRun.Controls.Add(Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow)
        Me.DarkGroupBox_AutoPluginRun.Controls.Add(Me.DarkCheckBox_SystemSleep)
        Me.DarkGroupBox_AutoPluginRun.Controls.Add(Me.Label_AutoRunPluginCheckedCount)
        Me.DarkGroupBox_AutoPluginRun.Controls.Add(Me.DarkCheckBox_ParalellExecution)
        Me.DarkGroupBox_AutoPluginRun.Controls.Add(Me.CheckedListBox_AutoPluginRun)
        Me.DarkGroupBox_AutoPluginRun.Controls.Add(Me.DarkCheckBox_AutoPluginRun)
        Me.DarkGroupBox_AutoPluginRun.Location = New Point(8, 28)
        Me.DarkGroupBox_AutoPluginRun.Name = "DarkGroupBox_AutoPluginRun"
        Me.DarkGroupBox_AutoPluginRun.Size = New Size(565, 308)
        Me.DarkGroupBox_AutoPluginRun.TabIndex = 0
        Me.DarkGroupBox_AutoPluginRun.TabStop = False
        Me.DarkGroupBox_AutoPluginRun.Text = "Automatic Plugin Execution"
        ' 
        ' Label_Hours
        ' 
        Me.Label_Hours.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.Label_Hours.ContextMenuStrip = Me.DarkContextMenu_AutoRunPluginsListBox
        Me.Label_Hours.Enabled = False
        Me.Label_Hours.ForeColor = Color.Gainsboro
        Me.Label_Hours.Location = New Point(258, 61)
        Me.Label_Hours.Name = "Label_Hours"
        Me.Label_Hours.Size = New Size(69, 27)
        Me.Label_Hours.TabIndex = 3
        Me.Label_Hours.Text = "hours"
        ' 
        ' DarkContextMenu_AutoRunPluginsListBox
        ' 
        Me.DarkContextMenu_AutoRunPluginsListBox.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.DarkContextMenu_AutoRunPluginsListBox.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.DarkContextMenu_AutoRunPluginsListBox.Items.AddRange(New ToolStripItem() {Me.ToolStripMenuItem_SelectAllPlugins, Me.ToolStripMenuItem_ClearSelectedPlugins})
        Me.DarkContextMenu_AutoRunPluginsListBox.Name = "DarkContextMenu_AutoRunPluginsListBox"
        Me.DarkContextMenu_AutoRunPluginsListBox.Size = New Size(163, 48)
        ' 
        ' ToolStripMenuItem_SelectAllPlugins
        ' 
        Me.ToolStripMenuItem_SelectAllPlugins.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.ToolStripMenuItem_SelectAllPlugins.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.ToolStripMenuItem_SelectAllPlugins.Image = My.Resources.Resources.SelectAll
        Me.ToolStripMenuItem_SelectAllPlugins.Name = "ToolStripMenuItem_SelectAllPlugins"
        Me.ToolStripMenuItem_SelectAllPlugins.Size = New Size(162, 22)
        Me.ToolStripMenuItem_SelectAllPlugins.Text = "Select all plugins"
        ' 
        ' ToolStripMenuItem_ClearSelectedPlugins
        ' 
        Me.ToolStripMenuItem_ClearSelectedPlugins.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.ToolStripMenuItem_ClearSelectedPlugins.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.ToolStripMenuItem_ClearSelectedPlugins.Image = My.Resources.Resources.ClearSelection
        Me.ToolStripMenuItem_ClearSelectedPlugins.Name = "ToolStripMenuItem_ClearSelectedPlugins"
        Me.ToolStripMenuItem_ClearSelectedPlugins.Size = New Size(162, 22)
        Me.ToolStripMenuItem_ClearSelectedPlugins.Text = "Clear selection"
        ' 
        ' DarkNumericUpDown_Hours
        ' 
        Me.DarkNumericUpDown_Hours.Enabled = False
        Me.DarkNumericUpDown_Hours.Location = New Point(205, 59)
        Me.DarkNumericUpDown_Hours.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.DarkNumericUpDown_Hours.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.DarkNumericUpDown_Hours.Name = "DarkNumericUpDown_Hours"
        Me.DarkNumericUpDown_Hours.Size = New Size(47, 29)
        Me.DarkNumericUpDown_Hours.TabIndex = 2
        Me.DarkNumericUpDown_Hours.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' DarkCheckBox_DontRunIfFullscreen
        ' 
        Me.DarkCheckBox_DontRunIfFullscreen.Cursor = Cursors.Hand
        Me.DarkCheckBox_DontRunIfFullscreen.Enabled = False
        Me.DarkCheckBox_DontRunIfFullscreen.Location = New Point(205, 94)
        Me.DarkCheckBox_DontRunIfFullscreen.Name = "DarkCheckBox_DontRunIfFullscreen"
        Me.DarkCheckBox_DontRunIfFullscreen.Size = New Size(354, 40)
        Me.DarkCheckBox_DontRunIfFullscreen.TabIndex = 4
        Me.DarkCheckBox_DontRunIfFullscreen.Text = "Don't run selected plugins if an application is in fullscreen mode"
        ' 
        ' DarkButtonImageAllignFix_RunAllSelectedPluginsNow
        ' 
        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.Cursor = Cursors.Hand
        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.Enabled = False
        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.Location = New Point(205, 217)
        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.Name = "DarkButtonImageAllignFix_RunAllSelectedPluginsNow"
        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.Padding = New Padding(5)
        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.ResizedImage = My.Resources.Resources.execute
        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.Size = New Size(354, 53)
        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.TabIndex = 7
        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.Text = "Run plugins now"
        Me.DarkButtonImageAllignFix_RunAllSelectedPluginsNow.TextImageRelation = TextImageRelation.ImageBeforeText
        ' 
        ' DarkCheckBox_SystemSleep
        ' 
        Me.DarkCheckBox_SystemSleep.Cursor = Cursors.Hand
        Me.DarkCheckBox_SystemSleep.Enabled = False
        Me.DarkCheckBox_SystemSleep.Location = New Point(205, 140)
        Me.DarkCheckBox_SystemSleep.Name = "DarkCheckBox_SystemSleep"
        Me.DarkCheckBox_SystemSleep.Size = New Size(354, 40)
        Me.DarkCheckBox_SystemSleep.TabIndex = 5
        Me.DarkCheckBox_SystemSleep.Text = "Prevent the system from entering sleep mode"
        ' 
        ' Label_AutoRunPluginCheckedCount
        ' 
        Me.Label_AutoRunPluginCheckedCount.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.Label_AutoRunPluginCheckedCount.ContextMenuStrip = Me.DarkContextMenu_AutoRunPluginsListBox
        Me.Label_AutoRunPluginCheckedCount.Enabled = False
        Me.Label_AutoRunPluginCheckedCount.ForeColor = Color.Gainsboro
        Me.Label_AutoRunPluginCheckedCount.Location = New Point(6, 273)
        Me.Label_AutoRunPluginCheckedCount.Name = "Label_AutoRunPluginCheckedCount"
        Me.Label_AutoRunPluginCheckedCount.Size = New Size(150, 27)
        Me.Label_AutoRunPluginCheckedCount.TabIndex = 8
        Me.Label_AutoRunPluginCheckedCount.Text = "0\0"
        ' 
        ' DarkCheckBox_ParalellExecution
        ' 
        Me.DarkCheckBox_ParalellExecution.Cursor = Cursors.Hand
        Me.DarkCheckBox_ParalellExecution.Enabled = False
        Me.DarkCheckBox_ParalellExecution.Location = New Point(205, 186)
        Me.DarkCheckBox_ParalellExecution.Name = "DarkCheckBox_ParalellExecution"
        Me.DarkCheckBox_ParalellExecution.Size = New Size(354, 25)
        Me.DarkCheckBox_ParalellExecution.TabIndex = 6
        Me.DarkCheckBox_ParalellExecution.Text = "Enable paralell execution"
        ' 
        ' CheckedListBox_AutoPluginRun
        ' 
        Me.CheckedListBox_AutoPluginRun.BackColor = Color.FromArgb(CByte(60), CByte(60), CByte(60))
        Me.CheckedListBox_AutoPluginRun.BorderStyle = BorderStyle.FixedSingle
        Me.CheckedListBox_AutoPluginRun.CheckOnClick = True
        Me.CheckedListBox_AutoPluginRun.ContextMenuStrip = Me.DarkContextMenu_AutoRunPluginsListBox
        Me.CheckedListBox_AutoPluginRun.Cursor = Cursors.Hand
        Me.CheckedListBox_AutoPluginRun.Enabled = False
        Me.CheckedListBox_AutoPluginRun.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.CheckedListBox_AutoPluginRun.FormattingEnabled = True
        Me.CheckedListBox_AutoPluginRun.Location = New Point(6, 28)
        Me.CheckedListBox_AutoPluginRun.Name = "CheckedListBox_AutoPluginRun"
        Me.CheckedListBox_AutoPluginRun.Size = New Size(193, 242)
        Me.CheckedListBox_AutoPluginRun.TabIndex = 0
        ' 
        ' DarkCheckBox_AutoPluginRun
        ' 
        Me.DarkCheckBox_AutoPluginRun.Cursor = Cursors.Hand
        Me.DarkCheckBox_AutoPluginRun.Location = New Point(205, 28)
        Me.DarkCheckBox_AutoPluginRun.Name = "DarkCheckBox_AutoPluginRun"
        Me.DarkCheckBox_AutoPluginRun.Size = New Size(354, 25)
        Me.DarkCheckBox_AutoPluginRun.TabIndex = 1
        Me.DarkCheckBox_AutoPluginRun.Text = "Run selected plugins every:"
        ' 
        ' DarkCheckBox_RememberCurrentSettings
        ' 
        Me.DarkCheckBox_RememberCurrentSettings.Cursor = Cursors.Hand
        Me.DarkCheckBox_RememberCurrentSettings.Location = New Point(14, 510)
        Me.DarkCheckBox_RememberCurrentSettings.Name = "DarkCheckBox_RememberCurrentSettings"
        Me.DarkCheckBox_RememberCurrentSettings.Size = New Size(277, 25)
        Me.DarkCheckBox_RememberCurrentSettings.TabIndex = 2
        Me.DarkCheckBox_RememberCurrentSettings.Text = "Remember current settings"
        ' 
        ' TableLayoutPanel1
        ' 
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        Me.TableLayoutPanel1.Controls.Add(Me.DarkSectionPanel_Plugins, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DarkSectionPanel_Program, 0, 1)
        Me.TableLayoutPanel1.Dock = DockStyle.Fill
        Me.TableLayoutPanel1.Location = New Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 75.63353F))
        Me.TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 24.3664722F))
        Me.TableLayoutPanel1.Size = New Size(233, 533)
        Me.TableLayoutPanel1.TabIndex = 0
        ' 
        ' DarkSectionPanel_Plugins
        ' 
        Me.DarkSectionPanel_Plugins.Dock = DockStyle.Fill
        Me.DarkSectionPanel_Plugins.Location = New Point(3, 3)
        Me.DarkSectionPanel_Plugins.Name = "DarkSectionPanel_Plugins"
        Me.DarkSectionPanel_Plugins.SectionHeader = "Plugins"
        Me.DarkSectionPanel_Plugins.Size = New Size(227, 397)
        Me.DarkSectionPanel_Plugins.TabIndex = 0
        ' 
        ' DarkSectionPanel_Program
        ' 
        Me.DarkSectionPanel_Program.Controls.Add(Me.TableLayoutPanel2)
        Me.DarkSectionPanel_Program.Dock = DockStyle.Fill
        Me.DarkSectionPanel_Program.Location = New Point(3, 406)
        Me.DarkSectionPanel_Program.Name = "DarkSectionPanel_Program"
        Me.DarkSectionPanel_Program.SectionHeader = "Program"
        Me.DarkSectionPanel_Program.Size = New Size(227, 124)
        Me.DarkSectionPanel_Program.TabIndex = 1
        ' 
        ' TableLayoutPanel2
        ' 
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        Me.TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        Me.TableLayoutPanel2.Controls.Add(Me.DarkButton_About, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.DarkButton_Settings, 0, 0)
        Me.TableLayoutPanel2.Dock = DockStyle.Fill
        Me.TableLayoutPanel2.Location = New Point(1, 25)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        Me.TableLayoutPanel2.Size = New Size(225, 98)
        Me.TableLayoutPanel2.TabIndex = 0
        ' 
        ' DarkButton_About
        ' 
        Me.DarkButton_About.Cursor = Cursors.Hand
        Me.DarkButton_About.Dock = DockStyle.Fill
        Me.DarkButton_About.Location = New Point(115, 3)
        Me.DarkButton_About.Name = "DarkButton_About"
        Me.DarkButton_About.Padding = New Padding(5)
        Me.DarkButton_About.ResizedImage = My.Resources.Resources.About
        Me.DarkButton_About.Size = New Size(107, 92)
        Me.DarkButton_About.TabIndex = 1
        Me.DarkButton_About.Text = "About..."
        Me.DarkButton_About.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' DarkButton_Settings
        ' 
        Me.DarkButton_Settings.Cursor = Cursors.Hand
        Me.DarkButton_Settings.Dock = DockStyle.Fill
        Me.DarkButton_Settings.Location = New Point(3, 3)
        Me.DarkButton_Settings.Name = "DarkButton_Settings"
        Me.DarkButton_Settings.Padding = New Padding(5)
        Me.DarkButton_Settings.ResizedImage = My.Resources.Resources.Settings
        Me.DarkButton_Settings.Size = New Size(106, 92)
        Me.DarkButton_Settings.TabIndex = 0
        Me.DarkButton_Settings.Text = "Settings"
        Me.DarkButton_Settings.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' NotifyIcon_Main
        ' 
        Me.NotifyIcon_Main.ContextMenuStrip = Me.DarkContextMenu_NotifyIcon
        Me.NotifyIcon_Main.Icon = CType(resources.GetObject("NotifyIcon_Main.Icon"), Icon)
        Me.NotifyIcon_Main.Text = "NotifyIcon1"
        Me.NotifyIcon_Main.Visible = True
        ' 
        ' DarkContextMenu_NotifyIcon
        ' 
        Me.DarkContextMenu_NotifyIcon.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.DarkContextMenu_NotifyIcon.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.DarkContextMenu_NotifyIcon.Items.AddRange(New ToolStripItem() {Me.ToolStripMenuItem_ShowWindow, Me.ToolStripMenuItem_HideWindow, Me.ToolStripMenuItem_CloseProgram})
        Me.DarkContextMenu_NotifyIcon.Name = "DarkContextMenu_NotifyIcon"
        Me.DarkContextMenu_NotifyIcon.Size = New Size(153, 70)
        ' 
        ' ToolStripMenuItem_ShowWindow
        ' 
        Me.ToolStripMenuItem_ShowWindow.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.ToolStripMenuItem_ShowWindow.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.ToolStripMenuItem_ShowWindow.Image = My.Resources.Resources.ShowWindow
        Me.ToolStripMenuItem_ShowWindow.Name = "ToolStripMenuItem_ShowWindow"
        Me.ToolStripMenuItem_ShowWindow.Size = New Size(152, 22)
        Me.ToolStripMenuItem_ShowWindow.Text = "Show Window"
        ' 
        ' ToolStripMenuItem_HideWindow
        ' 
        Me.ToolStripMenuItem_HideWindow.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.ToolStripMenuItem_HideWindow.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.ToolStripMenuItem_HideWindow.Image = My.Resources.Resources.HideWindow
        Me.ToolStripMenuItem_HideWindow.Name = "ToolStripMenuItem_HideWindow"
        Me.ToolStripMenuItem_HideWindow.Size = New Size(152, 22)
        Me.ToolStripMenuItem_HideWindow.Text = "Hide Window"
        ' 
        ' ToolStripMenuItem_CloseProgram
        ' 
        Me.ToolStripMenuItem_CloseProgram.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.ToolStripMenuItem_CloseProgram.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.ToolStripMenuItem_CloseProgram.Image = My.Resources.Resources.Quit
        Me.ToolStripMenuItem_CloseProgram.Name = "ToolStripMenuItem_CloseProgram"
        Me.ToolStripMenuItem_CloseProgram.Size = New Size(152, 22)
        Me.ToolStripMenuItem_CloseProgram.Text = "Close Program"
        ' 
        ' Timer_AutoRunPlugins
        ' 
        Me.Timer_AutoRunPlugins.Interval = 1000
        ' 
        ' StatusStrip1
        ' 
        Me.StatusStrip1.BackColor = Color.FromArgb(CByte(42), CByte(43), CByte(42))
        Me.StatusStrip1.Items.AddRange(New ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New Point(0, 539)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New Size(824, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        ' 
        ' ToolStripStatusLabel1
        ' 
        Me.ToolStripStatusLabel1.ForeColor = Color.Gainsboro
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New Size(16, 17)
        Me.ToolStripStatusLabel1.Text = "..."
        ' 
        ' DarkContextMenu_PluginUrls
        ' 
        Me.DarkContextMenu_PluginUrls.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.DarkContextMenu_PluginUrls.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.DarkContextMenu_PluginUrls.Items.AddRange(New ToolStripItem() {Me.ToolStripMenuItem_PluginUrlLogin, Me.ToolStripMenuItem_PluginUrlRegistration, Me.ToolStripMenuItem_PluginUrlApplication})
        Me.DarkContextMenu_PluginUrls.Name = "DarkContextMenu_PluginUrls"
        Me.DarkContextMenu_PluginUrls.Size = New Size(162, 70)
        ' 
        ' ToolStripMenuItem_PluginUrlLogin
        ' 
        Me.ToolStripMenuItem_PluginUrlLogin.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.ToolStripMenuItem_PluginUrlLogin.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.ToolStripMenuItem_PluginUrlLogin.Image = My.Resources.Resources.website
        Me.ToolStripMenuItem_PluginUrlLogin.Name = "ToolStripMenuItem_PluginUrlLogin"
        Me.ToolStripMenuItem_PluginUrlLogin.Size = New Size(161, 22)
        Me.ToolStripMenuItem_PluginUrlLogin.Tag = "Login URL"
        Me.ToolStripMenuItem_PluginUrlLogin.Text = "Login URL"
        ' 
        ' ToolStripMenuItem_PluginUrlRegistration
        ' 
        Me.ToolStripMenuItem_PluginUrlRegistration.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.ToolStripMenuItem_PluginUrlRegistration.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.ToolStripMenuItem_PluginUrlRegistration.Image = My.Resources.Resources.website
        Me.ToolStripMenuItem_PluginUrlRegistration.Name = "ToolStripMenuItem_PluginUrlRegistration"
        Me.ToolStripMenuItem_PluginUrlRegistration.Size = New Size(161, 22)
        Me.ToolStripMenuItem_PluginUrlRegistration.Tag = "Registration URL"
        Me.ToolStripMenuItem_PluginUrlRegistration.Text = "Registration URL"
        ' 
        ' ToolStripMenuItem_PluginUrlApplication
        ' 
        Me.ToolStripMenuItem_PluginUrlApplication.BackColor = Color.FromArgb(CByte(60), CByte(63), CByte(65))
        Me.ToolStripMenuItem_PluginUrlApplication.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.ToolStripMenuItem_PluginUrlApplication.Image = My.Resources.Resources.website
        Me.ToolStripMenuItem_PluginUrlApplication.Name = "ToolStripMenuItem_PluginUrlApplication"
        Me.ToolStripMenuItem_PluginUrlApplication.Size = New Size(161, 22)
        Me.ToolStripMenuItem_PluginUrlApplication.Tag = "Application URL"
        Me.ToolStripMenuItem_PluginUrlApplication.Text = "Application URL"
        ' 
        ' MainForm
        ' 
        Me.AutoScaleDimensions = New SizeF(9F, 21F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(824, 561)
        Me.Controls.Add(Me.TableLayoutPanel_Main)
        Me.Controls.Add(Me.StatusStrip1)
        Me.DoubleBuffered = True
        Me.Font = New Font("Segoe UI", 12F)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Me.Margin = New Padding(4)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Opacity = 0R
        Me.ShowInTaskbar = False
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Title"
        Me.TableLayoutPanel_Main.ResumeLayout(False)
        Me.TabControlNoBorder_Main.ResumeLayout(False)
        Me.TabPage_Settings.ResumeLayout(False)
        Me.DarkSectionPanel_Settings.ResumeLayout(False)
        Me.DarkGroupBox_Application.ResumeLayout(False)
        Me.DarkGroupBox_Application.PerformLayout()
        Me.DarkGroupBox_AutoPluginRun.ResumeLayout(False)
        Me.DarkContextMenu_AutoRunPluginsListBox.ResumeLayout(False)
        CType(Me.DarkNumericUpDown_Hours, ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.DarkSectionPanel_Program.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.DarkContextMenu_NotifyIcon.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.DarkContextMenu_PluginUrls.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents TableLayoutPanel_Main As TableLayoutPanel
    Friend WithEvents NotifyIcon_Main As NotifyIcon
    Friend WithEvents Timer_AutoRunPlugins As Timer
    Friend WithEvents DarkContextMenu_NotifyIcon As DarkUI.Controls.DarkContextMenu
    Friend WithEvents TabControlNoBorder_Main As TabControlNoBorder
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents DarkSectionPanel_Program As DarkUI.Controls.DarkSectionPanel
    Friend WithEvents DarkSectionPanel_Plugins As DarkUI.Controls.DarkSectionPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents DarkButton_Settings As DarkButtonImageAllignFix
    Friend WithEvents DarkButton_About As DarkButtonImageAllignFix
    Friend WithEvents TabPage_Settings As TabPage
    Friend WithEvents DarkSectionPanel_Settings As DarkUI.Controls.DarkSectionPanel
    Friend WithEvents DarkGroupBox_AutoPluginRun As DarkUI.Controls.DarkGroupBox
    Friend WithEvents DarkCheckBox_AutoPluginRun As DarkUI.Controls.DarkCheckBox
    Friend WithEvents CheckedListBox_AutoPluginRun As PersistableCheckedListBox
    Friend WithEvents DarkCheckBox_SystemSleep As DarkUI.Controls.DarkCheckBox
    Friend WithEvents ToolStripMenuItem_ShowWindow As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_HideWindow As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_CloseProgram As ToolStripMenuItem
    Friend WithEvents DarkCheckBox_RememberCurrentSettings As DarkUI.Controls.DarkCheckBox
    Friend WithEvents DarkComboBox_Language As DarkUI.Controls.DarkComboBox
    Friend WithEvents Label_Language As Label
    Friend WithEvents DarkGroupBox_Application As DarkUI.Controls.DarkGroupBox
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents DarkButtonImageAllignFix_ClearCache As DarkButtonImageAllignFix
    Friend WithEvents DarkCheckBox_ParalellExecution As DarkUI.Controls.DarkCheckBox
    Friend WithEvents DarkContextMenu_AutoRunPluginsListBox As DarkUI.Controls.DarkContextMenu
    Friend WithEvents ToolStripMenuItem_SelectAllPlugins As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ClearSelectedPlugins As ToolStripMenuItem
    Friend WithEvents Label_AutoRunPluginCheckedCount As Label
    Friend WithEvents DarkCheckBox_RunAppMinimized As DarkUI.Controls.DarkCheckBox
    Friend WithEvents DarkButtonImageAllignFix_RunAllSelectedPluginsNow As DarkButtonImageAllignFix
    Friend WithEvents DarkCheckBox_ClearPreviousLogEntries As DarkUI.Controls.DarkCheckBox
    Friend WithEvents DarkCheckBox_AllowPluginApplicationFormCheck As DarkUI.Controls.DarkCheckBox
    Friend WithEvents DarkContextMenu_PluginUrls As DarkUI.Controls.DarkContextMenu
    Friend WithEvents ToolStripMenuItem_PluginUrlLogin As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_PluginUrlRegistration As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_PluginUrlApplication As ToolStripMenuItem
    Friend WithEvents DarkCheckBox_DontRunIfFullscreen As DarkUI.Controls.DarkCheckBox
    Friend WithEvents Label_Hours As Label
    Friend WithEvents DarkNumericUpDown_Hours As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkCheckBox_SearchProgramUpdates As DarkUI.Controls.DarkCheckBox

End Class
