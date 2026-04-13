#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

Public NotInheritable Class MainSplashScreen : Inherits Form

#Region " Event Handlers "

    Private Sub SplashScreen1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.ApplicationTitle.Text = $"{My.Application.Info.ProductName} ({My.Application.Info.Title})"
        Me.Version.Text = String.Format(Me.Version.Text, $"{My.Application.Info.Version.Major}.{My.Application.Info.Version.Minor}.{My.Application.Info.Version.Build}")
        Me.Copyright.Text = My.Application.Info.Copyright
    End Sub

#End Region

End Class
