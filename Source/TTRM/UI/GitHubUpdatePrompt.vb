
#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports DarkUI.Forms

#End Region

Friend NotInheritable Class GitHubUpdatePrompt : Inherits DarkForm

    Private Sub GitHubUpdatePrompt_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub DarkButton1_Click(sender As Object, e As EventArgs) Handles DarkButton_OpenGitHubLatestReleasePage.Click

        ApplicationHelper.ShellOpenUrl($"{AppGlobals.GitHubUrl}releases/latest")
    End Sub
End Class