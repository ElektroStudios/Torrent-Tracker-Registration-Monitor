#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

Namespace Win32

    ''' <summary>
    ''' Specifies the flash status of a window icon in the taskbar.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For more information, see:
    ''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-flashwinfo#members">FLASHWINFO structure (winuser.h)</see>.
    ''' </remarks>
    <Flags>
    Friend Enum FlashWindowFlags As UInteger

        [Stop] = &H0
        Caption = &H1
        Tray = &H2
        All = &H3
        Timer = &H4
        TimerNoForeground = &HC
    End Enum

End Namespace
