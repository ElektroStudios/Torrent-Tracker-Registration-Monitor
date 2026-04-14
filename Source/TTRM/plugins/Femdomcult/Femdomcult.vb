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
Class FemdomcultPlugin : Inherits DynamicPlugin

    ReadOnly headless As Boolean = True
    ReadOnly additionalArgs As String() = Array.Empty(Of String)

    Overloads Async Function RunAsync() As Task(Of RegistrationFlags)
        Dim regFlags As RegistrationFlags = RegistrationFlags.Null

        Return Await Task.Run(
            Function()
                Using service As ChromeDriverService = Nothing,
                      driver As ChromeDriver = CreateChromeDriver(Me, service, headless, additionalArgs)

                    Try
                        regFlags = regFlags Or Me.CustomRegistrationCheck(driver)

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

    Private Function CustomRegistrationCheck(driver As ChromeDriver) As RegistrationFlags

        ' Note that the registration page (register.php) does not exists,
        ' returning a 404 ("Not found") error status code on an HTTP request.
        '
        ' However, when registrations are open, the registration page exists,
        ' and the main and login pages (index.php, login.php)
        ' contains a link pointing to the page: https://femdomcult.org/register.php
        ' ( See: https://web.archive.org/web/20230401030035/https://femdomcult.org/register.php )
        '
        ' So we check the login page instead the registration page. 👇

        Dim registrationTriggers As String() = {"register.php"}
        Dim registrationTriggersIndicatesOpen As Boolean = True

        PluginSupport.LogMessageFormat(Me, "StatusMsg_ConnectingFormat", Color.Empty, Me.Name)
        PluginSupport.LogMessage(Me, $"➜ {Me.UrlLogin}", Color.Empty)
        PluginSupport.NavigateTo(driver, Me.UrlLogin)
        PluginSupport.LogMessage(Me, "StatusMsg_WaitingForPageLoad", Color.Empty)
        PluginSupport.WaitForPageReady(driver,
                                       afterPageReadyDelay:=TimeSpan.FromSeconds(1),
                                       waitForDomIdle:=True, timeout:=TimeSpan.FromSeconds(30))
        PluginSupport.LogMessage(Me, "StatusMsg_LoginPageLoaded", Color.Empty)

        Return PluginSupport.EvaluateRegistrationFormState(Me, driver, registrationTriggers, registrationTriggersIndicatesOpen)
    End Function

End Class