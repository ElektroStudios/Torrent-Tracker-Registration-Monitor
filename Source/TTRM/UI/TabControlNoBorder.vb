#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Runtime.InteropServices

Imports TTRM.Win32

#End Region

''' <summary>
''' A subclass of <see cref="TabControl"/> that has no border and allows hiding the tab headers.
''' </summary>
''' 
''' <remarks>
''' <para></para>
''' Code originally written by @Rajeev on StackOverflow: <see href="https://stackoverflow.com/a/6717038"/>.
''' <para></para>
''' A variant by @DrewNaylor was published in: <see href="https://gist.github.com/DrewNaylor/627af786aa2b6b1864533c622567abde"/>.
''' <para></para>
''' This is another variant, written by @ElektroStudios.
''' </remarks>
Public Class TabControlNoBorder : Inherits TabControl

#Region " Restricted Fields "

    ''' <summary>
    ''' Last <see cref="TabControlNoBorder.ItemSize"/> value for restoring tabs appearance 
    ''' when <see cref="ShowTabHeader"/> property is toggled to <see langword="True"/>.
    ''' </summary>
    Private lastItemSize As Size

    ''' <summary>
    ''' Last <see cref="TabControlNoBorder.Appearance"/> value for restoring tabs appearance 
    ''' when <see cref="ShowTabHeader"/> property is toggled to <see langword="True"/>.
    ''' </summary>
    Private lastAppearance As TabAppearance

    ''' <summary>
    ''' Last <see cref="TabControlNoBorder.SizeMode"/> value for restoring tabs appearance 
    ''' when <see cref="ShowTabHeader"/> property is toggled to <see langword="True"/>.
    ''' </summary>
    Private lastSizeMode As TabSizeMode

#End Region

#Region " Public Properties "

    ''' <summary>
    ''' Gets or sets a value indicating whether the tab headers are shown.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' When set to <see langword="False"/>, the tab headers are hidden by adjusting <see cref="Appearance"/>,
    ''' <see cref="ItemSize"/>, and <see cref="SizeMode"/> properties. Setting it to <see langword="True"/> restores
    ''' the previous appearance.
    ''' </remarks>
    <Browsable(True)>
    Public Property ShowTabHeader As Boolean
        <DebuggerStepThrough>
        Get
            Return Me._showTabHeader
        End Get
        <DebuggerStepThrough>
        Set(value As Boolean)
            If value = Me._showTabHeader Then
                Exit Property
            End If

            If value Then
                Me.Appearance = Me.lastAppearance
                Me.ItemSize = Me.lastItemSize
                Me.SizeMode = Me.lastSizeMode
            Else
                Me.lastAppearance = Me.Appearance
                Me.lastItemSize = Me.ItemSize
                Me.lastSizeMode = Me.SizeMode

                Me.Appearance = TabAppearance.FlatButtons
                Me.ItemSize = New Size(0, 1)
                Me.SizeMode = TabSizeMode.Fixed
            End If
            Me._showTabHeader = value
            Me.Invalidate()
        End Set
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="ShowTabHeader"/> property. )
    ''' <para></para>
    ''' A value indicating whether the tab headers are shown.
    ''' </summary>
    Private _showTabHeader As Boolean = True

#End Region

#Region " Window Procedure (WndProc) "

    ''' <summary>
    ''' Processes window messages.
    ''' </summary>
    ''' 
    ''' <param name="m">
    ''' The window <see cref="Message"/> to process.
    ''' </param>
    <DebuggerStepperBoundary>
    Protected Overrides Sub WndProc(ByRef m As Message)

        Const TCM_FIRST As Integer = &H1300
        Const TCM_ADJUSTRECT As UInteger = (TCM_FIRST + 40)

        If (m.Msg = TCM_ADJUSTRECT) Then
            Dim rc As NativeRectangle = DirectCast(m.GetLParam(GetType(NativeRectangle)), NativeRectangle)
            ' Adjust these values to suit, dependant upon
            ' Appearance Values modified from the default by Drew Naylor.
            '
            ' These values haven't been checked to see if they work
            ' on HiDPI displays, though they do work under 96 DPI.
            rc.Top -= If(Me._showTabHeader, 2, 5)
            rc.Left -= 4
            rc.Right += 4
            rc.Bottom += 4
            Marshal.StructureToPtr(rc, m.LParam, True)
        End If
        MyBase.WndProc(m)
    End Sub

#End Region

#Region " Event Invokers "

    ''' <summary>
    ''' Raises the <see cref="TabControlNoBorder.GotFocus"/> event.
    ''' </summary>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepperBoundary>
    Protected Overrides Sub OnGotFocus(e As EventArgs)

        If Not Me._showTabHeader Then
            ' Move the focus to the next focusable control.
            Me.Parent?.SelectNextControl(Me, forward:=True, tabStopOnly:=True, nested:=True, wrap:=True)
            Exit Sub
        End If
        MyBase.OnGotFocus(e)
    End Sub

#End Region

End Class