Imports System
Imports System.Diagnostics
Imports System.Threading.Tasks
Imports System.Windows.Forms

Imports TTRM
Imports TTRM.PluginSupport

Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome

<DebuggerStepThrough>
Class HDOlimpoPlugin : Inherits DynamicPlugin

    ReadOnly headless As Boolean = True
    ReadOnly additionalArgs As String() = Array.Empty(Of String)

    ReadOnly applicationTriggers As String() = {"No se aceptan nuevos usuarios"}
    ReadOnly applicationTriggersIndicatesOpen As Boolean = False

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
                        regFlags = regFlags Or Me.CustomRegistrationCheck(driver)

                        regFlags = regFlags Or
                                   PluginSupport.DefaultApplicationFormCheckProcedure(
                                       Me, driver,
                                       applicationTriggers, applicationTriggersIndicatesOpen,
                                       afterPageReadyDelay, waitForDomIdle, timeout
                                   )

                    Catch ex As Exception
                        PluginSupport.LogMessageFormat(Me, "StatusMsg_ExceptionFormat", ex.Message)
                        ' PluginSupport.NotifyMessageFormat("Error", MessageBoxIcon.Error, "StatusMsg_ExceptionFormat", ex.Message)

                    Finally
                        driver?.Quit()
                        PluginSupport.LogMessage(Me, "StatusMsg_OperationCompleted")
                        PluginSupport.PrintMessage(Me, String.Empty)
                    End Try
                End Using

                Return regFlags
            End Function)
    End Function

    Private Function CustomRegistrationCheck(driver As ChromeDriver) As RegistrationFlags

        ' If the registration form is closed, the site redirects automatically to the login page,
        ' so we check if we are in the login page.

        Dim registrationTriggers As String() = {"Iniciar sesión en HD-Olimpo"}
        Dim registrationTriggersIndicatesOpen As Boolean = False

        PluginSupport.LogMessageFormat(Me, "StatusMsg_ConnectingFormat", Me.Name)
        PluginSupport.LogMessage(Me, $"➜ {Me.UrlRegistration}")
        PluginSupport.NavigateTo(driver, Me.UrlRegistration)
        PluginSupport.LogMessage(Me, "StatusMsg_WaitingForPageLoad")
        PluginSupport.WaitForPageReady(driver,
                                       afterPageReadyDelay:=TimeSpan.FromSeconds(1),
                                       waitForDomIdle:=True, timeout:=TimeSpan.FromSeconds(30))
        PluginSupport.LogMessage(Me, "StatusMsg_RegisterPageLoaded")

        Return PluginSupport.EvaluateRegistrationFormState(Me, driver, registrationTriggers, registrationTriggersIndicatesOpen)
    End Function

End Class