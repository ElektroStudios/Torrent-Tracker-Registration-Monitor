#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Imports TTRM.Win32

#End Region

''' <summary>
''' Provides extension methods for <see cref="NotifyIcon"/> type.
''' </summary>
Public Module NotifyIconExtensions

#Region " Public Methods "

    ''' <summary>
    ''' Gets the internal <see cref="NativeWindow"/> of the <see cref="NotifyIcon"/>.
    ''' </summary>
    ''' 
    ''' <param name="icon">
    ''' The <see cref="NotifyIcon"/> instance.
    ''' </param>
    ''' 
    ''' <returns>
    ''' The internal <see cref="NativeWindow"/> used by the NotifyIcon.
    ''' </returns>
    <Extension>
    <DebuggerStepThrough>
    Public Function GetWindow(icon As NotifyIcon) As NativeWindow

        Dim windowField As FieldInfo = icon.GetType().GetField("_window", BindingFlags.NonPublic Or BindingFlags.Instance)
        Dim windowValue As Object = windowField.GetValue(icon)
        Return DirectCast(windowValue, NativeWindow)
    End Function

    ''' <summary>
    ''' Forces the balloon tip of the <see cref="NotifyIcon"/> to close immediately.
    ''' </summary>
    ''' 
    ''' <param name="icon">
    ''' The <see cref="NotifyIcon"/> instance whose balloon tip should be closed.
    ''' </param>
    <Extension>
    <DebuggerStepThrough>
    Public Sub CloseBallontip(icon As NotifyIcon)

        Dim data As New NotifyiconData()
        With data
            .Size = CType(Marshal.SizeOf(data), UInteger)
            .Hwnd = NotifyIconExtensions.GetWindow(icon).Handle
            .ID = NotifyIconExtensions.GetNotifyIconId(icon)
            .Flags = NotifyIconDataFlags.Info
            .Info = ""
        End With
        NativeMethods.Shell_NotifyIcon(NotifyIconMessages.Modify, data)
    End Sub

#End Region

#Region " Restricted Methods "

    ''' <summary>
    ''' Gets the internal icon ID of the <see cref="NotifyIcon"/> used by <see cref="NotifyiconData.ID"/> field.
    ''' </summary>
    ''' 
    ''' <param name="icon">
    ''' The <see cref="NotifyIcon"/> instance.
    ''' </param>
    ''' 
    ''' <returns>
    ''' The internal ID assigned to the NotifyIcon.
    ''' </returns>
    <DebuggerStepThrough>
    Private Function GetNotifyIconId(icon As NotifyIcon) As UInteger

        Dim idField As FieldInfo = icon.GetType().GetField("_id", BindingFlags.NonPublic Or BindingFlags.Instance)
        Dim idValue As Object = idField.GetValue(icon)
        Return CUInt(idValue)
    End Function

#End Region

End Module
