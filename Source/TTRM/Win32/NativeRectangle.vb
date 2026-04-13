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
    ''' Defines a rectangle by the coordinates of its upper-left and lower-right corners.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For more information, see:
    ''' <see href="https://learn.microsoft.com/es-es/windows/win32/api/windef/ns-windef-rect">RECT structure (windef.h)</see>.
    ''' </remarks>
    <StructLayout(LayoutKind.Sequential)>
    Friend Structure NativeRectangle ' RECT

        Friend Left As Integer
        Friend Top As Integer
        Friend Right As Integer
        Friend Bottom As Integer

        Friend Sub New(left As Integer, top As Integer, right As Integer, bottom As Integer)

            Me.Left = left
            Me.Top = top
            Me.Right = right
            Me.Bottom = bottom
        End Sub

        Friend ReadOnly Property Width As Integer
            Get
                Return Me.Right - Me.Left
            End Get
        End Property

        Friend ReadOnly Property Height As Integer
            Get
                Return Me.Bottom - Me.Top
            End Get
        End Property

    End Structure

End Namespace
