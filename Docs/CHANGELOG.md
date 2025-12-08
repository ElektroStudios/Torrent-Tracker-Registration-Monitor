# MTAFRAT Change Log 📋

## v1.1.10 *(current)* 🆕

#### 🌟 Improvements:
- Added new plugins:
  - eMuwarez
  - GazelleGames
- Updated NuGet packages:
  - Selenium.Webdriver (used for Chrome automation)

## v1.1.9 🔄

#### 🛠️ Fixes:
- Fixed malfunctioning plugins:
  - 3ChangTrai
  - TorrentDay

#### 🌟 Improvements:
- All plugin source codes have been updated.
- Updated NuGet packages:
  - Microsoft.CodeAnalysis.VisualBasic (used for VB.NET compiler support)

## v1.1.8 🔄

#### 🌟 Improvements:
- Added a new tracker plugin: ParabellumHD
- Added a new auxiliary plugin: # Site autoLogin

## v1.1.7 🔄

#### 🌟 Improvements:
- Added an option to search for program updates at startup.

## v1.1.6 🔄

#### 🌟 Improvements:
- Added an option to set the hourly interval for automatic plugin execution.
- Added an option to prevent automatic plugin execution from running when any full-screen window (normal or exclusive mode) is active in the computer.
- Minor UI adjustments in the 'Settings' page to better utilize available space.

## v1.1.5 🔄

#### 🌟 Improvements:
- Added an additional verification when navigating to a web page to account for dynamic DOM updates and wait until they are completed.
- The taskbar icon for non-headless Chrome windows now become hidden after Chrome process execution (although still visible for a moment).
- When a plugin is already running, the application now allows you to click the "Run plugin" button of another plugin to run it simultaneously.
- Automatic plugin execution has been improved to run smoother, avoiding user-interface flickering.
- The parallel execution mode has been optimized to dynamically determine the number of simultaneous Chrome instances to use —up to a maximum of 8— based on available system memory and CPU cores.

#### 🛠️ Fixes:
 - The combination of the navigation improvements mentioned above and the other ones mentioned in v1.1.4 should fix previous unexpected false positives in the registration/application form validation performed by some plugins.
 - The labels displaying the last run status of their corresponding plugin were not updating correctly when plugins were executed in parallel mode.

## v1.1.4 🔄

#### 🌟 Improvements:
 - A basic detection mechanism for web pages that requires to complete a Cloudflare challenge has been integrated, allowing also to simplify plugins source-code.
 - The Cloudflare challenge validation procedure has been rewritten to wait for the official [cf_clearance](https://developers.cloudflare.com/fundamentals/reference/policies-compliances/cloudflare-cookies/#additional-cookies-used-by-the-challenge-platform) cookie.
 - Any plugin using a web browser in **headless** mode will now log an error message if it attempts to complete a Cloudflare challenge.
 - Added an additional verification when navigating to a web page to verify that the URL loaded in the browser is the same URL expected by the plugin.
 - Added two new methods for plugin developers:
   - `IsCloudflareChallengeRequired`
   - `WaitToCompleteCloudflareChallenge`

## v1.1.3 🔄

#### 🌟 Improvements:
 - The application now is able to handle scenarios involving network connectivity issues (an inoperative network adapter) and non-existent URLs (HTTP error code 404).

#### 🛠️ Fixes:
 - A string validation mistake was preventing the application form from being validated correctly in the following plugins: 
   - `BitPorn`
   - `DarkPeers`
   - `OnlyEncodes`
   - `Rastastugan`
   - `UpscaleVault`

## v1.1.2 🔄

#### 🌟 Improvements:
 - Optimized memory usage by resizing and compressing all image resources, reducing memory consumption from ~500 MB to ~160 MB at application startup.

## v1.1.1 🔄

#### 🌟 Improvements:
 - Optimized memory usage by reutilizing a single shared context menu for the "Open Website" button of all dynamic plugins instead of creating one context menu per each plugin, reducing memory consumption from ~620 MB to ~500 MB at application startup.
 - Replaced the spanish terms "solicitud de inscripción" with "solicitud de membresía" to improve clarity and user understanding.

## v1.1.0 🔄

#### 🌟 Improvements:
 - Refactored all plugin JSON files to include three URLs: login page, registration page, and application page.
 - Added a context menu to the 'Open Website' button in each plugin panel, allowing users to choose which URL to open.
 - Reworked all plugin VB source code files to simplify overall logic and structure.
 - All eligible plugins (websites) now also checks too for whether an application form is open.
 - Added a new option in the `Settings` panel to allow UI notifications when a plugin detects an open application form.
 - Minor adjustments were made to improve the clarity and descriptiveness of log messages.
 - Added an application manifest file to require Administrator privileges, which should avoid previous issues related to cache folders creation at runtime.
 - Added six new helper methods for plugin developers:
   - `PrintMessage`
   - `PrintMessageFormat`
   - `DefaultRegistrationFormCheckProcedure`
   - `DefaultApplicationFormCheckProcedure`
   - `EvaluateRegistrationFormState`
   - `EvaluateApplicationFormState`
     Note that the `Evaluate*Formstate` functions are more intended for internal use, so they are not documented in the included README file.
 - Updated Selenium nuget package to version 4.38.0

#### 🛠️ Fixes:
 - The `Settings` panel loses focus during automatic plugin execution.
 - The "Remember Current Settings" button position was misaligned.
 - Automatic scrolling in the log TextBox of each plugin panel stopped working properly when the panel lost focus.

## v1.0.5 🔄

#### 🌟 Improvements:
 - Added a link in the 'About' dialog window pointing to the GitHub's application repository page.
 - Description text in the 'About' dialog window is now translated.
 - Tab navigation has been adjusted to reflect the new added controls.

#### 🛠️ Fixes:
 - Button "Clear cache" in the plugin tabs was not properly translated.

#### 📦 Installer changes:

 - The installer now asks for confirmation before overwriting the contents of the 'plugins' folder during installation, allowing to preserve any custom or modified plugins, allowing to preserve any custom or modified plugins.

## v1.0.4 🔄

#### 🌟 Improvements:
 - Added a button in the `Settings` panel to run all selected plugins on demand.
 - Added a button in the `Settings` panel to clear previous log entries on plugin execution.
 - Each plugin tab now has a button to clear its plugin cache.
 - Improved resource management for Selenium services (`ChromeDriverService` object is now disposed appropriately).
 - Other minor internal resource management improvements with disposable objects and Garbage Collector.
 - Added logic and status messages to differentiate between registration and application forms.
 - Plugins 'HD-Olimpo' and 'HDZero' now also checks for an open application form. (Other plugins may be updated to do the same in future releases.)

#### 🛠️ Fixes:
 - The application used to throw an unhandled exception when no supported language was detected; this is now resolved by automatically falling back to English.
 - Fixed an issue that allowed to show the main window when double-clicking the system tray icon during the initial splash screen.
 - Fixed a minor, internal issue where function `CanFocusNextPluginButtonControl` did not return a value on all code paths.

#### 📦 Installer changes:

 - The installer now requires admin privileges (preventing errors when creating the 'cache' folder) and prompts whether to delete the 'plugins' folder on uninstall, allowing to preserve any custom or modified plugins.

## v1.0.3 🔄
Initial public release on GitHub.

## v1.0.2 🔄
Private release.

## v1.0.1 🔄
Private release.

## v1.0.0 🔄
Private release.