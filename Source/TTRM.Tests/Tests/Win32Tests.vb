#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Runtime.InteropServices

Imports TTRM.Win32

#End Region

''' <summary>
''' Tests for the Win32 interop types declared in TTRM.
''' <para></para>
''' The values of these types must match the native Windows SDK definitions.
''' </summary>
<TestClass>
Public NotInheritable Class Win32Tests

#Region " NativeRectangle "

    ''' <summary>
    ''' The width and height of a RECT must be computed from its edges.
    ''' </summary>
    <TestMethod>
    <DataRow(0, 0, 100, 50, 100, 50)>
    <DataRow(10, 20, 110, 70, 100, 50)>
    <DataRow(-50, -25, 50, 25, 100, 50)>
    <DataRow(5, 5, 5, 5, 0, 0)>
    Public Sub NativeRectangle_WidthAndHeight(left As Integer, top As Integer, right As Integer, bottom As Integer,
                                              expectedWidth As Integer, expectedHeight As Integer)

        Dim rect As New NativeRectangle(left, top, right, bottom)

        Assert.AreEqual(expectedWidth, rect.Width)
        Assert.AreEqual(expectedHeight, rect.Height)
    End Sub

    ''' <summary>
    ''' The managed RECT structure must have the same size as the native RECT structure (4 x LONG).
    ''' </summary>
    <TestMethod>
    Public Sub NativeRectangle_HasNativeSize()

        Assert.AreEqual(16, Marshal.SizeOf(Of NativeRectangle)())
    End Sub

#End Region

#Region " Flags "

    ''' <summary>
    ''' <see cref="FlashWindowFlags"/> values must match the native FLASHW_* constants.
    ''' </summary>
    <TestMethod>
    Public Sub FlashWindowFlags_MatchNativeValues()

        Assert.AreEqual(&H0UI, CUInt(FlashWindowFlags.Stop))         ' FLASHW_STOP
        Assert.AreEqual(&H1UI, CUInt(FlashWindowFlags.Caption))      ' FLASHW_CAPTION
        Assert.AreEqual(&H2UI, CUInt(FlashWindowFlags.Tray))         ' FLASHW_TRAY
        Assert.AreEqual(&H4UI, CUInt(FlashWindowFlags.Timer))        ' FLASHW_TIMER
        Assert.AreEqual(&HCUI, CUInt(FlashWindowFlags.TimerNoForeground)) ' FLASHW_TIMERNOFG
        Assert.AreEqual(&H3UI, CUInt(FlashWindowFlags.All))          ' FLASHW_ALL (FLASHW_CAPTION Or FLASHW_TRAY)
    End Sub

    ''' <summary>
    ''' <see cref="ExecutionStateFlags"/> values must match the native ES_* constants.
    ''' </summary>
    <TestMethod>
    Public Sub ExecutionStateFlags_MatchNativeValues()

        Assert.AreEqual(&H1UI, CUInt(ExecutionStateFlags.SystemRequired))      ' ES_SYSTEM_REQUIRED
        Assert.AreEqual(&H2UI, CUInt(ExecutionStateFlags.DisplayRequired))     ' ES_DISPLAY_REQUIRED
        Assert.AreEqual(&H40UI, CUInt(ExecutionStateFlags.AwayModeRequired))   ' ES_AWAYMODE_REQUIRED
        Assert.AreEqual(&H80000000UI, CUInt(ExecutionStateFlags.Continuous))   ' ES_CONTINUOUS
    End Sub

    ''' <summary>
    ''' <see cref="NotifyIconDataFlags"/> values must match the native NIF_* constants.
    ''' </summary>
    <TestMethod>
    Public Sub NotifyIconDataFlags_MatchNativeValues()

        Assert.AreEqual(&H1UI, CUInt(NotifyIconDataFlags.Message))   ' NIF_MESSAGE
        Assert.AreEqual(&H2UI, CUInt(NotifyIconDataFlags.Icon))      ' NIF_ICON
        Assert.AreEqual(&H4UI, CUInt(NotifyIconDataFlags.Tip))       ' NIF_TIP
        Assert.AreEqual(&H8UI, CUInt(NotifyIconDataFlags.State))     ' NIF_STATE
        Assert.AreEqual(&H10UI, CUInt(NotifyIconDataFlags.Info))     ' NIF_INFO
        Assert.AreEqual(&H20UI, CUInt(NotifyIconDataFlags.Guid))     ' NIF_GUID
        Assert.AreEqual(&H40UI, CUInt(NotifyIconDataFlags.Realtime)) ' NIF_REALTIME
        Assert.AreEqual(&H80UI, CUInt(NotifyIconDataFlags.ShowTip))  ' NIF_SHOWTIP
    End Sub

#End Region

End Class
