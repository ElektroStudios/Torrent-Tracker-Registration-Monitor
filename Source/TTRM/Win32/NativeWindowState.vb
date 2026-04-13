#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

Namespace Win32

    ''' <summary>
    ''' Controls how a window is to be shown.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For more information, see:
    ''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow?#parameters">ShowWindow function (winuser.h)</see>.
    ''' </remarks>
    <Flags>
    Public Enum NativeWindowState As Integer

        Hide = 0
        Normal = 1
        ShowMinimized = 2
        Maximize = 3
        ShowMaximized = NativeWindowState.Maximize
        ShowNoActivate = 4
        Show = 5
        Minimize = 6
        ShowMinNoActive = 7
        ShowNA = 8
        Restore = 9
        ShowDefault = 10
        ForceMinimize = 11

    End Enum

End Namespace
