Imports System
Imports System.Diagnostics
Imports System.Threading.Tasks
Imports System.Windows.Forms

Imports TTRM
Imports TTRM.PluginSupport

Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Support.UI

<DebuggerStepThrough>
Class TorrentLeechPlugin : Inherits DynamicPlugin

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

        PluginSupport.LogMessageFormat(Me, "StatusMsg_ConnectingFormat", Me.Name)
        PluginSupport.LogMessage(Me, $"➜ {Me.UrlLogin}")
        PluginSupport.NavigateTo(driver, Me.UrlLogin)
        PluginSupport.LogMessage(Me, "StatusMsg_WaitingForPageLoad")
        PluginSupport.WaitForPageReady(driver,
                                       afterPageReadyDelay:=TimeSpan.FromSeconds(1),
                                       waitForDomIdle:=True, timeout:=TimeSpan.FromSeconds(30))
        PluginSupport.LogMessage(Me, "StatusMsg_LoginPageLoaded")

        PluginSupport.LogMessage(Me, "StatusMsg_AnalyzingPageContent")
        Dim firstNewsText As String = CStr(driver.ExecuteScript("return document.querySelector('div.site-news-inner li.news-item:first-child')?.textContent;"))
        If firstNewsText.Contains("expired", StringComparison.InvariantCultureIgnoreCase) Then
            PluginSupport.LogMessage(Me, "StatusMsg_DetectedRegClosed")
            Return RegistrationFlags.RegistrationClosed
        Else
            PluginSupport.LogMessage(Me, "StatusMsg_DetectedRegOpen")
            PluginSupport.NotifyMessageFormat("😄🎉🎉🎉", MessageBoxIcon.Information, "StatusMsg_MsgboxRegOpenFormat", Me.Name)
            Return RegistrationFlags.RegistrationOpen
        End If
    End Function

End Class