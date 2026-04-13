#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Globalization
Imports System.Threading

Imports Microsoft.VisualBasic.ApplicationServices

#End Region

Namespace My

    Partial Friend Class MyApplication

#Region " Event Handlers "

        ''' <summary>
        ''' Handles the <see cref="WindowsFormsApplicationBase.ApplyApplicationDefaults"/> event.
        ''' and minimum splash screen display time.
        ''' </summary>
        ''' 
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="ApplyApplicationDefaultsEventArgs"/> instance containing the event data.
        ''' </param>
        Private Sub MyApplication_ApplyApplicationDefaults(sender As Object, e As ApplyApplicationDefaultsEventArgs) Handles Me.ApplyApplicationDefaults

            e.HighDpiMode = HighDpiMode.DpiUnaware
            e.MinimumSplashScreenDisplayTime = AppGlobals.SplashScreenTime
        End Sub

        ''' <summary>
        ''' Handles the <see cref="WindowsFormsApplicationBase.Startup"/> event.
        ''' and minimum splash screen display time.
        ''' </summary>
        ''' 
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="StartupEventArgs"/> instance containing the event data.
        ''' </param>
        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup

            Try
                JotHelper.InitializeJot()

            Catch ex As Exception
                Dim msg As String = "Jot failure. Method 'JotHelper.InitializeJot' failed with exception message: " & Environment.NewLine & ex.Message
                MessageBox.Show(Nothing, msg, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Environment.Exit(1)

            End Try
        End Sub

        ''' <summary>
        ''' Handles the <see cref="WindowsFormsApplicationBase.StartupNextInstance"/> event.
        ''' and minimum splash screen display time.
        ''' </summary>
        ''' 
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="StartupNextInstanceEventArgs"/> instance containing the event data.
        ''' </param>
        Private Sub MyApplication_StartupNextInstance(sender As Object, e As StartupNextInstanceEventArgs) Handles Me.StartupNextInstance

            MessageBox.Show(AppGlobals.MainFormInstance, My.Resources.Strings.SingleInstanceMsg,
                            My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)

            e.BringToForeground = True
        End Sub

#End Region

#Region " Event Invokers "

        ''' <summary>
        ''' Emits code that initializes the splash screen.
        ''' </summary>
        <DebuggerStepThrough>
        Protected Overrides Sub OnCreateSplashScreen()

            Me.SplashScreen = New Global.TTRM.MainSplashScreen()
        End Sub

#End Region

    End Class

End Namespace
