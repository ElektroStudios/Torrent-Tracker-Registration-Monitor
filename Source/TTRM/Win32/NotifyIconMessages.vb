#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

''' <summary>
''' Specifies a message to send to a taskbar's status area icon (NotifyIcon).
''' </summary>
''' 
''' <remarks>
''' For more information, see:
''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shell_notifyiconw">Shell_NotifyIconW function (shellapi.h)</see>.
''' </remarks>
Friend Enum NotifyIconMessages As UInteger

    Add = &H0UI
    Modify = &H1UI
    Delete = &H2UI
    SetVersion = &H4UI
End Enum
