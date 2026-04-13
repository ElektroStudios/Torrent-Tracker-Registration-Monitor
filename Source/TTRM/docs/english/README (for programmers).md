# 👩‍💻 Documentation for Developers

(_This content is best viewed in a Markdown-compatible reader._ 👀)

## 📚 Basic Fundamentals

**TTRM** uses the **Selenium WebDriver** API to automate interaction with websites.

The structure of the plugin configuration JSON file is as follows:

```json
{
  "Name":           "PLUGIN NAME",
  "Description":    "TRACKER/FORUM DESCRIPTION OR CATEGORY",
  "UrlLogin":       "ACCOUNT LOGIN URL",
  "UrlRegister":    "ACCOUNT REGISTRATION URL",
  "UrlApplication": "ACCOUNT APPLICATION URL",
  "IconPath":       "RELATIVE PATH TO IMAGE/ICON FILE",
  "VbCodeFile":     "RELATIVE PATH TO VB.NET SOURCE CODE FILE"
}
```

A plugin is implemented through a VB.NET class inheriting from **TTRM.DynamicPlugin** and overloading the asynchronous function `RunAsync()`, which returns a value of type `Task(Of RegistrationFlags)`, as shown in the following simplified example:

```vbnet
Class MyPlugin : Inherits DynamicPlugin

    Overloads Async Function RunAsync() As Task(Of RegistrationFlags)
      
      ' Plugin's logic here.
    End Function

End Class
```

Note that `RegistrationFlags` is an enumeration used to indicate the state of the asynchronous operation. The values are as follows:

 - **RegistrationClosed**:  Indicates that registration form is closed.
 - **RegistrationOpen**:    Indicates that registration form is open.
 - **RegistrationUnknown**: Registration form state is unknown. Can be used as an auxiliary value when the state cannot be determined.
 - **ApplicationClosed**:  Indicates that application form is closed.
 - **ApplicationOpen**:    Indicates that application form is open.
 - **ApplicationUnknown**: Application form state is unknown. Can be used as an auxiliary value when the state cannot be determined.
 - **Null**: No flags. Can be used as an auxiliary value when an error or unexpected condition occurs.

It is the developer's responsibility to implement the logic for interacting with the website, error handling, and logging messages in the **TTRM** user interface.

You can use any of the multiple built-in plugins found in the "plugins" folder as a starting point. Some of these plugins provide basic and limited support for pages protected by **Cloudflare**.

## 🛠️ Auxiliary Support

Plugin developers have at their disposal the **TTRM.PluginSupport** module, designed with helper methods to simplify common tasks in development:

 - `CreateChromeDriver` As `ChromeDriver`
```vbnet
plugin As DynamicPlugin
ByRef refService As ChromeDriverService
headless As Boolean
ParamArray arguments As String()
```
Create and return an instance of type `ChromeDriver`, preconfigured with security and performance arguments. 

 - `NavigateTo`
```vbnet
driver As IWebDriver
url As String
```
Direct the `IWebDriver` to navigate to the specified URL, safely handling timeouts and exceptions.

 - `WaitForPageReady`
```vbnet
driver As IWebDriver
Optional afterReadyDelay As TimeSpan
Optional waitForDomIdle As Boolean
Optional timeout As TimeSpan
```
Wait until the page fully loads (`document.readyState = "complete"`).  

 - `WaitForElement`
```vbnet
driver As IWebDriver
by As By
Optional timeout As TimeSpan
```
Wait for an element matching the `By` selector to be present, visible, and interactable.  

 - `ClickElementJs`
```vbnet
driver As IWebDriver
element As IWebElement
```
Perform a click on the element using JavaScript, useful when the **Selenium** method `element.Click()` fails due to overlays or other issues.

 - `LogMessage`
```vbnet
plugin As DynamicPlugin
msg As String
```
Print a message in the `LogTextBox` control associated with the plugin.  
Ideal for displaying progress messages, results, or errors within the **TTRM** interface.  

 - `LogMessageFormat`
```vbnet
plugin As DynamicPlugin
msgFormat As String
ParamArray args As Object()
```
Works similarly to `LogMessage`, but allows using formatted strings to dynamically construct the message, like `String.Format()`.

 - `PrintMessage`
```vbnet
plugin As DynamicPlugin
msg As String
```
Same as `LogMessage` fucntion, but it prints the message as-is, without a timestamp.

 - `PrintMessageFormat`
```vbnet
plugin As DynamicPlugin
msgFormat As String
ParamArray args As Object()
```
Same as `LogMessageFormat` fucntion, but it prints the message as-is, without a timestamp.

 - `NotifyMessage`
```vbnet
title As String
icon As MessageBoxIcon
msg As String
```
Unlike `LogMessage`, this method shows a `MessageBox` directly to the user. If the application form is minimized or hidden in the notification area, it will be shown.
It is used when you want to notify an important error or an action that requires immediate attention, for example, to notify the detection of open registration on a tracker.  

 - `NotifyMessageFormat`
```vbnet
title As String
icon As MessageBoxIcon
msgFormat As String
ParamArray args As Object()
```
Works similarly to `NotifyMessage`, but allows using formatted strings to dynamically construct the message, like `String.Format()`.

 - `ThrowIfStatusCode`
```vbnet
driver As IWebDriver
statusCode As Integer
afterDate As Date
```
Analyzes the browser log entries since the specified date to find any entry containing the specified HTTP status code error, throwing an `Exception` with the corresponding log entry message if found.

 - `ThrowIfAnyErrorStatusCode`
```vbnet
driver As IWebDriver
afterDate As Date
```
Analyzes the browser log entries since the specified date to find any entry containing any HTTP status code error, throwing an `Exception` with the corresponding log entry message if found. 

It also analyzes the current page source, applying special handling for Cloudflare-protected pages.

This method helps determine whether the currently loaded page returned an HTTP error status code.

 - `DefaultRegistrationFormCheckProcedure` As `RegistrationFlags`
```vbnet
plugin As DynamicPlugin
driver As ChromeDriver
trigger As String
isOpenTrigger As Boolean
afterPageReadyDelay As TimeSpan
waitForDomIdle As Boolean
timeout As TimeSpan
```
A common utility function used by multiple plugins that encapsulates the default steps to navigate 
to a registration form page, check and return its current state, and handle message logging and UI notifications.

 - `DefaultApplicationFormCheckProcedure` As `RegistrationFlags`
```vbnet
plugin As DynamicPlugin
driver As ChromeDriver
trigger As String
isOpenTrigger As Boolean
afterPageReadyDelay As TimeSpan
waitForDomIdle As Boolean
timeout As TimeSpan
```
A common utility function used by multiple plugins that encapsulates the default steps to navigate 
to an application form page, check and return its current state, and handle message logging and UI notifications.

 - `IsCloudflareChallengeRequired`
```vbnet
url As String
```
Determines whether the specified web page requires to complete a Cloudflare challenge to proceed.

 - `WaitToCompleteCloudflareChallengue`
```vbnet
plugin As DynamicPlugin
driver As ChromeDriver
Optional timeout As Integer
```
Waits for the Cloudflare challenge to complete by detecting and validating the [cf_clearance](https://developers.cloudflare.com/fundamentals/reference/policies-compliances/cloudflare-cookies/#additional-cookies-used-by-the-challenge-platform) cookie within the specified timeout period.
