#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

''' <summary>
''' Specifies the state of the machine for the current user in relation to the propriety of sending a notification.
''' </summary>
'''
''' <remarks>
''' For more information, see:
''' <see href="https://docs.microsoft.com/en-us/windows/win32/api/shellapi/ne-shellapi-query_user_notification_state">SHQueryUserNotificationState function (shellapi.h)</see>.
''' </remarks>
Public Enum QueryUserNotificationState

    NotPresent = 1
    Busy = 2
    RunningD3DFullScreen = 3
    PresentationMode = 4
    AcceptsNotifications = 5
    QuietTime = 6
    App = 7
End Enum
