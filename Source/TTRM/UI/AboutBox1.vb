#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO

Imports DarkUI.Forms

#End Region

Friend NotInheritable Class AboutBox1 : Inherits DarkForm

    Private Sub AboutBox1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Set the title of the form.
        Dim ApplicationTitle As String = If(My.Application.Info.Title <> "",
            My.Application.Info.Title,
            Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName))

        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", $"{My.Application.Info.Version.Major}.{My.Application.Info.Version.Minor}.{My.Application.Info.Version.Build}")
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LinkLabelGitHub.Text = "🌐 GitHub"
        Me.TextBoxDescription.Text = My.Resources.Strings.AboutDescriptionText
    End Sub

    Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

    Private Sub LinkLabelGitHub_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelGitHub.LinkClicked

        ApplicationHelper.ShellOpenUrl(AppGlobals.GitHubUrl)
    End Sub

End Class
