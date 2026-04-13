Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Threading.Tasks

Imports TTRM
Imports TTRM.PluginSupport

Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Support.UI

<DebuggerStepThrough>
Class SiteAutoLogin : Inherits DynamicPlugin

    ReadOnly headless As Boolean = True
    ReadOnly additionalArgs As String() = Array.Empty(Of String)

    ReadOnly CurrentLangName As String = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName

    ReadOnly Messages As New Dictionary(Of String, Dictionary(Of String, String))(StringComparer.InvariantCultureIgnoreCase) From {
            {"en", New Dictionary(Of String, String) From {
                {"NoSiteLoginConfs", $"No site login configurations are specified in the plugin source-code file. Please open the '{Me.GetType().Name}.vb' file wih a text editor, find the '{NameOf(GetSiteLoginConfigs)}' method, and define inside your login configurations."},
                {"SessionStillActive", "User session is still active, no login needed."},
                {"UsernameElementNotfound", "Username element not found on the page."},
                {"PasswordElementNotfound", "Password element not found on the page."},
                {"SubmitElementNotfound", "Submit element not found on the page."},
                {"FillingUserNameField", "⚙️ Filling username field..."},
                {"FillingPasswordField", "⚙️ Filling username field..."},
                {"Submitting", "⚙️ Submitting login request..."},
                {"LoginFailed_StillInLoginUrl", "Login submit has failed. Reason: the browser stills in the login URL (wrong username or password?)."},
                {"LoginFailed_UnexpectedRedirectedUrl", "Login submit has failed. Reason: the browser has been redirected to an unexpected URL."},
                {"LoginFailed_Timeout", "Login submit has failed. Reason: the request has timed out."},
                {"LoginSuccess", "🎉 Login succesful."}
            }},
            {"es", New Dictionary(Of String, String) From {
                {"NoSiteLoginConfs", $"No se han especificado configuraciones de inicio de sesión en el archivo de código fuente del plugin. Por favor, abra el archivo '{Me.GetType().Name}.vb' con un editor de texto, busque el método '{NameOf(GetSiteLoginConfigs)}' y defina allí dentro las configuraciones de inicio de sesión."},
                {"SessionStillActive", "La sesión de usuario sigue activa, no es necesario iniciar sesión."},
                {"UsernameElementNotfound", "No se encontró en la página el elemento HTML del nombre de usuario."},
                {"PasswordElementNotfound", "No se encontró en la página el elemento HTML de la contraseña."},
                {"SubmitElementNotfound", "No se encontró en la página el elemento HTML de enviar solicitud de login."},
                {"FillingUserNameField", "⚙️ Rellenando el campo de nombre de usuario..."},
                {"FillingPasswordField", "⚙️ Rellenando el campo de contraseña..."},
                {"Submitting", "⚙️ Enviando solicitud de inicio de sesión..."},
                {"LoginFailed_StillInLoginUrl", "La solicitud de inicio de sesión ha fallado. Razón: el navegador sigue en la URL de inicio de sesión (¿nombre de usuario o contraseña incorrecta?)"},
                {"LoginFailed_UnexpectedRedirectedUrl", "La solicitud de inicio de sesión ha fallado. Razón: el navegador ha sido redirigido a una URL inesperada."},
                {"LoginFailed_Timeout", "La solicitud de inicio de sesión ha fallado. Razón: la solicitud ha excedido el tiempo de espera."},
                {"LoginSuccess", "🎉 Inicio de sesión exitoso."}
            }}
        }

    ''' <summary>
    ''' Retrieves a read-only collection of site login configurations hardcored for this plugin.
    ''' </summary>
    ''' 
    ''' <returns>
    ''' A <see cref="ReadOnlyCollection(Of SiteLoginConfig)"/> containing
    ''' all login configuration entries available for the supported sites.
    ''' </returns>
    Private Shared Function GetSiteLoginConfigs() As ReadOnlyCollection(Of SiteLoginConfig)

        Dim sites As New Collection(Of SiteLoginConfig)

        '' This is an example:
        ''
        'Dim bitporn As New SiteLoginConfig() With {
        '    .SiteName = "Bitporn",
        '    .MainURL = "https://bitporn.eu/",
        '    .LoginURL = "https://bitporn.eu/login",
        '    .PostLoginURL = "https://bitporn.eu/pages/1",
        '    .Username = "Your username",
        '    .Password = "Your password",
        '    .UsernameCSSSelector = "#username",
        '    .PasswordCSSSelector = "#password",
        '    .SubmitCSSSelector = ".login-card > button:nth-child(16)"
        '} : sites.Add(bitporn)

        Return sites.AsReadOnly()
    End Function

    ''' <summary>
    ''' Performs an interactive login to a website using the provided <see cref="SiteLoginConfig"/>.
    ''' </summary>
    ''' 
    ''' <param name="siteconfig">
    ''' The login configuration for the target website.
    ''' </param>
    Private Function LoginToSite(siteconfig As SiteLoginConfig) As RegistrationFlags

        Using service As ChromeDriverService = Nothing,
              driver As ChromeDriver = PluginSupport.CreateChromeDriver(Me, service, Me.headless, Me.additionalArgs)

            Try
                PluginSupport.LogMessageFormat(Me, "StatusMsg_ConnectingFormat", siteconfig.SiteName)
                PluginSupport.LogMessage(Me, $"➜ {siteconfig.LoginURL}")
                PluginSupport.NavigateTo(driver, siteconfig.LoginURL)
                PluginSupport.LogMessage(Me, "StatusMsg_WaitingForPageLoad")
                PluginSupport.WaitForPageReady(driver,
                                               afterPageReadyDelay:=TimeSpan.FromSeconds(1),
                                               waitForDomIdle:=True, timeout:=TimeSpan.FromSeconds(30))

                If driver.Url.Equals(siteconfig.PostLoginURL, StringComparison.InvariantCultureIgnoreCase) OrElse
                  (driver.Url.Equals(siteconfig.MainURL, StringComparison.InvariantCultureIgnoreCase)) Then
                    PluginSupport.LogMessage(Me, Me.Messages(currentLangName)("SessionStillActive"))
                    Return RegistrationFlags.Null
                End If
                PluginSupport.LogMessage(Me, "StatusMsg_LoginPageLoaded")

                ' Username field
                Dim userField As IWebElement = PluginSupport.WaitForElement(driver, By.CssSelector(siteconfig.UsernameCSSSelector))
                If userField Is Nothing Then
                    PluginSupport.LogMessageFormat(Me, "StatusMsg_ExceptionFormat", Me.Messages(currentLangName)("UsernameElementNotfound"))
                End If
                PluginSupport.LogMessage(Me, Me.Messages(currentLangName)("FillingUserNameField"))
                userField.Clear()
                userField.SendKeys(siteconfig.Username)

                ' Password field
                Dim passField As IWebElement = PluginSupport.WaitForElement(driver, By.CssSelector(siteconfig.PasswordCSSSelector))
                If passField Is Nothing Then
                    PluginSupport.LogMessageFormat(Me, "StatusMsg_ExceptionFormat", Me.Messages(currentLangName)("PasswordElementNotfound"))
                End If
                PluginSupport.LogMessage(Me, Me.Messages(currentLangName)("FillingPasswordField"))
                passField.Clear()
                passField.SendKeys(siteconfig.Password)

                ' Submit button
                Dim submitButton As IWebElement = SiteAutoLogin.FindSubmitButton(siteconfig, driver)
                If submitButton IsNot Nothing Then
                    PluginSupport.LogMessage(Me, Me.Messages(currentLangName)("Submitting"))
                    Me.DoLoginFormSubmit(driver, submitButton, siteconfig, currentLangName)
                Else
                    PluginSupport.LogMessageFormat(Me, "StatusMsg_ExceptionFormat", Me.Messages(currentLangName)("SubmitElementNotfound"))
                End If

            Catch ex As Exception
                PluginSupport.LogMessageFormat(Me, "StatusMsg_ExceptionFormat", ex.Message)
                ' PluginSupport.NotifyMessageFormat("Error", MessageBoxIcon.Error, "StatusMsg_ExceptionFormat", ex.Message)

            Finally
                driver?.Quit()
                PluginSupport.LogMessage(Me, "StatusMsg_OperationCompleted")
                PluginSupport.PrintMessage(Me, String.Empty)
            End Try
        End Using

        Return RegistrationFlags.RegistrationOpen
    End Function

    ''' <summary>
    ''' Clicks the submit button on a login form and waits for the page to redirect.
    ''' </summary>
    ''' 
    ''' <param name="driver">
    ''' The <see cref="ChromeDriver"/> instance controlling the browser.
    ''' </param>
    ''' 
    ''' <param name="submitButton">
    ''' The submit button <see cref="IWebElement"/> to click.
    ''' </param>
    ''' 
    ''' <param name="siteConfig">
    ''' The login configuration containing the expected post-login URL.
    ''' </param>
    ''' 
    ''' <param name="timeoutSeconds">
    ''' Optional. The maximum time in seconds to wait for the page to redirect.
    ''' <para></para>
    ''' Default is 30 seconds.
    ''' </param>
    Private Sub DoLoginFormSubmit(driver As ChromeDriver, submitButton As IWebElement,
                                  siteConfig As SiteLoginConfig, currentLangName As String,
                                  Optional timeoutSeconds As Integer = 20)

        Dim currentUrl As String = driver.Url
        submitButton.Click()

        Dim sw As New Stopwatch()
        sw.Start()
        Do
            If (driver.Url <> currentUrl) Then
                If driver.Url.Equals(siteConfig.PostLoginURL, StringComparison.InvariantCultureIgnoreCase) Then
                    PluginSupport.LogMessage(Me, Me.Messages(currentLangName)("LoginSuccess"))
                Else
                    PluginSupport.LogMessageFormat(Me, "StatusMsg_ExceptionFormat", Me.Messages(currentLangName)("LoginFailed_UnexpectedRedirectedUrl"))
                End If

                Exit Do
            End If

            If sw.Elapsed.TotalSeconds > timeoutSeconds Then
                If driver.Url.Equals(siteConfig.LoginURL, StringComparison.InvariantCultureIgnoreCase) Then
                    PluginSupport.LogMessageFormat(Me, "StatusMsg_ExceptionFormat", Me.Messages(currentLangName)("LoginFailed_StillInLoginUrl"))
                Else
                    PluginSupport.LogMessageFormat(Me, "StatusMsg_ExceptionFormat", Me.Messages(currentLangName)("LoginFailed_Timeout"))
                End If

                Exit Do
            End If

            Thread.Sleep(500)
        Loop

    End Sub

    ''' <summary>
    ''' Attempts to locate the submit button on a login page using multiple strategies.
    ''' </summary>
    ''' 
    ''' <param name="siteConfig">
    ''' The configuration object containing the optional CSS selector for the submit button.
    ''' </param>
    ''' 
    ''' <param name="driver">
    ''' The <see cref="ChromeDriver"/> instance controlling the browser.
    ''' </param>
    ''' 
    ''' <returns>
    ''' The <see cref="IWebElement"/> representing the submit button if found; otherwise, <c>Nothing</c>.
    ''' </returns>
    Private Shared Function FindSubmitButton(siteConfig As SiteLoginConfig, driver As ChromeDriver) As IWebElement

        Dim submitBtn As IWebElement = Nothing
        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(3))

        Try
            submitBtn = wait.Until(Function(d) d.FindElement(By.CssSelector(siteConfig.SubmitCSSSelector)))
        Catch ex As WebDriverTimeoutException
            ' Ignore.
        End Try

        If submitBtn Is Nothing Then
            Try
                submitBtn = driver.FindElement(By.CssSelector("button[type='submit']"))
            Catch ex As WebDriverTimeoutException
                ' Ignore.
            End Try
        End If

        If submitBtn Is Nothing Then
            Try
                submitBtn = driver.FindElement(By.CssSelector("input[type='submit']"))
            Catch ex As WebDriverTimeoutException
                ' Ignore.
            End Try
        End If

        If submitBtn Is Nothing Then
            Try
                Dim candidates As IReadOnlyCollection(Of IWebElement) =
                    wait.Until(Function(d)
                                   Return d.FindElements(By.CssSelector("form input[type='button']"))
                               End Function)

                For Each btn As IWebElement In candidates
                    Dim text As String = btn.Text
                    Dim value As String = btn.GetAttribute("value")

                    If (Not String.IsNullOrEmpty(text) AndAlso text.Equals("submit", StringComparison.OrdinalIgnoreCase)) OrElse
                       (Not String.IsNullOrEmpty(value) AndAlso value.Equals("submit", StringComparison.OrdinalIgnoreCase)) Then
                        submitBtn = btn
                        Exit For
                    End If
                Next
            Catch ex As WebDriverTimeoutException
                ' Ignore.
            End Try
        End If

        Return submitBtn
    End Function

    Overloads Async Function RunAsync() As Task(Of RegistrationFlags)
        Dim regFlags As RegistrationFlags = RegistrationFlags.Null

        Return Await Task.Run(
            Function()
                Dim sites As ReadOnlyCollection(Of SiteLoginConfig) = SiteAutoLogin.GetSiteLoginConfigs()
                If sites.Count = 0 Then
                    PluginSupport.LogMessageFormat(Me, "StatusMsg_ExceptionFormat", Me.Messages(currentLangName)("NoSiteLoginConfs"))
                Else
                    For Each site As SiteLoginConfig In sites
                        regFlags = regFlags Or Me.LoginToSite(site)
                    Next site
                End If

                Return regFlags
            End Function)
    End Function

End Class

''' <summary>
''' Represents the configuration settings required to log in to a website.
''' </summary>
Friend NotInheritable Class SiteLoginConfig

    ''' <summary>
    ''' Gets or sets the display name of the site.
    ''' </summary>
    Property SiteName As String

    ''' <summary>
    ''' Gets or sets the URL of the main page.
    ''' </summary>
    Property MainURL As String

    ''' <summary>
    ''' Gets or sets the URL of the login page.
    ''' </summary>
    Property LoginURL As String

    ''' <summary>
    ''' Gets or sets the URL to which the user is redirected after successfully logging in.
    ''' </summary>
    Property PostLoginURL As String

    ''' <summary>
    ''' Gets or sets the username used for login.
    ''' </summary>
    Property Username As String

    ''' <summary>
    ''' Gets or sets the CSS selector for the username input field.
    ''' </summary>
    Property UsernameCSSSelector As String

    ''' <summary>
    ''' Gets or sets the password used for login.
    ''' </summary>
    Property Password As String

    ''' <summary>
    ''' Gets or sets the CSS selector for the password input field.
    ''' </summary>
    Property PasswordCSSSelector As String

    ''' <summary>
    ''' Gets or sets the CSS selector for the submit button.
    ''' </summary>
    Property SubmitCSSSelector As String

End Class
