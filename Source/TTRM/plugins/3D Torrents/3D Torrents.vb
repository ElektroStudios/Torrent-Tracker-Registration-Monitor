Imports System
Imports System.Diagnostics
Imports System.Drawing
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Windows.Forms.Design.AxImporter

Imports TTRM
Imports TTRM.PluginSupport

Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome

<DebuggerStepThrough>
Class _3DTorrentsPlugin : Inherits DynamicPlugin

    ' 📝 Notes
    ' ━━━━━━━━
    '
    ' VIP / Paid account registration URL: http://www.3dtorrents.org/index.php?page=vip /
    '                                      http://www.3dtorrents.org/index.php?page=becomemember

    ReadOnly headless As Boolean = True
    ReadOnly additionalArgs As String() = {
        $"--unsafely-treat-insecure-origin-as-secure=http://www.3dtorrents.org/"
    } ' Required to avoid error 'net::ERR_SSL_PROTOCOL_ERROR'

    ReadOnly registrationTriggers As String() = {"registrations are closed"}
    ReadOnly registrationTriggersIndicatesOpen As Boolean = False

    ReadOnly waitForDomIdle As Boolean = True
    ReadOnly afterPageReadyDelay As TimeSpan = TimeSpan.FromSeconds(1)
    ReadOnly timeout As TimeSpan = TimeSpan.FromSeconds(20)

    Overloads Async Function RunAsync() As Task(Of RegistrationFlags)
        Dim regFlags As RegistrationFlags = RegistrationFlags.Null

        Return Await Task.Run(
            Function()
                Using service As ChromeDriverService = Nothing,
                      driver As ChromeDriver = CreateChromeDriver(Me, service, headless, additionalArgs)

                    Try
                        regFlags = regFlags Or
                                   PluginSupport.DefaultRegistrationFormCheckProcedure(Me, driver,
                                       registrationTriggers, registrationTriggersIndicatesOpen,
                                       afterPageReadyDelay, waitForDomIdle, timeout
                                   )

                    Catch ex As Exception
                        PluginSupport.LogMessageFormat(Me, "StatusMsg_ExceptionFormat", Color.IndianRed, ex.Message)
                        ' PluginSupport.NotifyMessageFormat("Error", MessageBoxIcon.Error, "StatusMsg_ExceptionFormat", ex.Message)

                    Finally
                        driver?.Quit()
                        PluginSupport.LogMessage(Me, "StatusMsg_OperationCompleted", Color.LimeGreen)
                        PluginSupport.PrintMessage(Me, String.Empty, Color.Empty)
                    End Try
                End Using

                Return regFlags
            End Function)
    End Function

End Class