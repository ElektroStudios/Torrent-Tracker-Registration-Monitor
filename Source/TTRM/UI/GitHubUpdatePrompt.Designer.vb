Imports DarkUI.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GitHubUpdatePrompt
    Inherits DarkForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DarkTextBox_GitHubReleaseData = New DarkUI.Controls.DarkTextBox()
        Me.DarkLabel_ProgramUpdateAvailable = New DarkUI.Controls.DarkLabel()
        Me.DarkButton_OpenGitHubLatestReleasePage = New DarkButtonImageAllignFix()
        Me.SuspendLayout()
        ' 
        ' DarkTextBox_GitHubReleaseData
        ' 
        Me.DarkTextBox_GitHubReleaseData.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        Me.DarkTextBox_GitHubReleaseData.BorderStyle = BorderStyle.FixedSingle
        Me.DarkTextBox_GitHubReleaseData.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.DarkTextBox_GitHubReleaseData.Location = New Point(13, 39)
        Me.DarkTextBox_GitHubReleaseData.Margin = New Padding(4)
        Me.DarkTextBox_GitHubReleaseData.Multiline = True
        Me.DarkTextBox_GitHubReleaseData.Name = "DarkTextBox_GitHubReleaseData"
        Me.DarkTextBox_GitHubReleaseData.ReadOnly = True
        Me.DarkTextBox_GitHubReleaseData.ScrollBars = ScrollBars.Both
        Me.DarkTextBox_GitHubReleaseData.Size = New Size(727, 288)
        Me.DarkTextBox_GitHubReleaseData.TabIndex = 2
        ' 
        ' DarkLabel_ProgramUpdateAvailable
        ' 
        Me.DarkLabel_ProgramUpdateAvailable.Dock = DockStyle.Top
        Me.DarkLabel_ProgramUpdateAvailable.Font = New Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Me.DarkLabel_ProgramUpdateAvailable.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.DarkLabel_ProgramUpdateAvailable.Location = New Point(0, 0)
        Me.DarkLabel_ProgramUpdateAvailable.Margin = New Padding(4, 0, 4, 0)
        Me.DarkLabel_ProgramUpdateAvailable.Name = "DarkLabel_ProgramUpdateAvailable"
        Me.DarkLabel_ProgramUpdateAvailable.Size = New Size(753, 35)
        Me.DarkLabel_ProgramUpdateAvailable.TabIndex = 1
        Me.DarkLabel_ProgramUpdateAvailable.Text = "A new version of the program is available."
        Me.DarkLabel_ProgramUpdateAvailable.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' DarkButton_OpenGitHubLatestReleasePage
        ' 
        Me.DarkButton_OpenGitHubLatestReleasePage.Font = New Font("Segoe UI", 12F)
        Me.DarkButton_OpenGitHubLatestReleasePage.Location = New Point(266, 334)
        Me.DarkButton_OpenGitHubLatestReleasePage.Name = "DarkButton_OpenGitHubLatestReleasePage"
        Me.DarkButton_OpenGitHubLatestReleasePage.Padding = New Padding(5)
        Me.DarkButton_OpenGitHubLatestReleasePage.ResizedImage = My.Resources.Resources.website
        Me.DarkButton_OpenGitHubLatestReleasePage.Size = New Size(220, 65)
        Me.DarkButton_OpenGitHubLatestReleasePage.TabIndex = 0
        Me.DarkButton_OpenGitHubLatestReleasePage.Text = "Open release page"
        Me.DarkButton_OpenGitHubLatestReleasePage.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' GitHubUpdatePrompt
        ' 
        Me.AutoScaleDimensions = New SizeF(9.0F, 21.0F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(753, 405)
        Me.Controls.Add(Me.DarkButton_OpenGitHubLatestReleasePage)
        Me.Controls.Add(Me.DarkLabel_ProgramUpdateAvailable)
        Me.Controls.Add(Me.DarkTextBox_GitHubReleaseData)
        Me.DoubleBuffered = True
        Me.Font = New Font("Segoe UI", 12.0F)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.Margin = New Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GitHubUpdatePrompt"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents DarkTextBox_GitHubReleaseData As DarkUI.Controls.DarkTextBox
    Friend WithEvents DarkLabel_ProgramUpdateAvailable As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkButton_OpenGitHubLatestReleasePage As DarkButtonImageAllignFix
End Class
