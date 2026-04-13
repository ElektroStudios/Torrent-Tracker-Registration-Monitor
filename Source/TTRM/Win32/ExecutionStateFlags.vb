#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

Namespace Win32

    ''' <summary>
    ''' Specifies the thread's execution requirements.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For more information, see:
    ''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate#parameters">SetThreadExecutionState function (winbase.h)</see>.
    ''' </remarks>
    <Flags>
    Friend Enum ExecutionStateFlags As UInteger

        Null = &H0
        AwayModeRequired = &H40
        Continuous = &H80000000UI
        DisplayRequired = &H2
        SystemRequired = &H1
    End Enum

End Namespace
