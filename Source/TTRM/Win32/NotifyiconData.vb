#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Runtime.InteropServices

#End Region

''' <summary>
''' Contains information that the system needs to display notifications in the notification area.
''' </summary>
''' 
''' <remarks>
''' For more information, see:
''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-notifyicondataw">NOTIFYICONDATAW structure (shellapi.h)</see>.
''' </remarks>
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
Friend Structure NotifyiconData

    Friend Size As UInteger
    Friend Hwnd As IntPtr
    Friend ID As UInteger
    Friend Flags As NotifyIconDataFlags
    Friend CallbackMessage As UInteger
    Friend Icon As IntPtr

    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)>
    Friend Tip As String

    Friend State As UInteger
    Friend StateMask As UInteger

    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)>
    Friend Info As String

    Friend TimeoutOrVersion As UInteger
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=64)>
    Friend InfoTitle As String

    Friend InfoFlags As UInteger
    Friend GuidItem As Guid
    Friend BalloonIcon As IntPtr
End Structure
