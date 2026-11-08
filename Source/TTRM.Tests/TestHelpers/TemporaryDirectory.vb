#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO

#End Region

''' <summary>
''' Represents a uniquely named temporary directory that is deleted when disposed.
''' </summary>
Public NotInheritable Class TemporaryDirectory : Implements IDisposable

#Region " Properties "

    ''' <summary>
    ''' Gets the full path of this temporary directory.
    ''' </summary>
    Public ReadOnly Property FullPath As String

#End Region

#Region " Constructors "

    ''' <summary>
    ''' Initializes a new instance of the <see cref="TemporaryDirectory"/> class,
    ''' creating a new uniquely named directory inside the user's temporary folder.
    ''' </summary>
    Public Sub New()

        Me.FullPath = Path.Combine(Path.GetTempPath(), $"TTRM.Tests_{Guid.NewGuid():N}")
        Directory.CreateDirectory(Me.FullPath)
    End Sub

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Writes the specified text to a file inside this temporary directory, using UTF-8 encoding.
    ''' </summary>
    '''
    ''' <param name="fileName">
    ''' The file name (relative to this temporary directory).
    ''' </param>
    '''
    ''' <param name="content">
    ''' The file content.
    ''' </param>
    '''
    ''' <returns>
    ''' The full path of the written file.
    ''' </returns>
    Public Function WriteFile(fileName As String, content As String) As String

        ArgumentException.ThrowIfNullOrWhiteSpace(fileName)
        ArgumentNullException.ThrowIfNull(content)

        Dim filePath As String = Path.Combine(Me.FullPath, fileName)
        File.WriteAllText(filePath, content, Text.Encoding.UTF8)
        Return filePath
    End Function

#End Region

#Region " IDisposable Implementation "

    ''' <summary>
    ''' Tracks whether <see cref="TemporaryDirectory.Dispose()"/> method has already been called.
    ''' </summary>
    Private disposedValue As Boolean

    ''' <summary>
    ''' Deletes this temporary directory and all its contents.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose

        If Not Me.disposedValue Then
            If Directory.Exists(Me.FullPath) Then
                Directory.Delete(Me.FullPath, recursive:=True)
            End If
            Me.disposedValue = True
        End If
    End Sub

#End Region

End Class
