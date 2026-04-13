#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Runtime.InteropServices

#End Region

Namespace Win32

    ''' <summary>
    ''' Defines the information required to flash a window.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For more information, see:
    ''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-flashwinfo">FLASHWINFO structure (winuser.h)</see>.
    ''' </remarks>
    <StructLayout(LayoutKind.Sequential)>
    Friend Structure FlashInfo

        Friend Size As UInteger
        Friend Hwnd As IntPtr
        Friend Flags As FlashWindowFlags
        Friend Count As UInteger
        Friend Timeout As UInteger
    End Structure

End Namespace
