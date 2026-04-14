Imports System
Imports System.Diagnostics
Imports System.Drawing
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms

Imports TTRM
Imports TTRM.PluginSupport

Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome

<DebuggerStepThrough>
Class HDForeverPlugin : Inherits DynamicPlugin

    ReadOnly headless As Boolean = True
    ReadOnly additionalArgs As String() = Array.Empty(Of String)

    ReadOnly registrationTriggers As String() = {"Inscriptions Fermées"}
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