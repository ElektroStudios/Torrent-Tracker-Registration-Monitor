#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

''' <summary>
''' Flags for the <see cref="NotifyiconData.Flags"/> field.
''' </summary>
''' 
''' <remarks>
''' For more information, see:
''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-notifyicondataw">NOTIFYICONDATAW structure (shellapi.h)</see>.
''' </remarks>
<Flags>
Public Enum NotifyIconDataFlags As UInteger

    None = 0UI
    Message = &H1UI
    Icon = &H2UI
    Tip = &H4UI
    State = &H8UI
    Info = &H10UI
    Guid = &H20UI
    Realtime = &H40UI
    ShowTip = &H80UI
End Enum
