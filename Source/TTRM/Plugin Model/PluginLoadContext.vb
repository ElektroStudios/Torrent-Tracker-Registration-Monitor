#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Runtime.Loader

#End Region

''' <summary>
''' Represents a collectible <see cref="AssemblyLoadContext"/> for loading plugins dynamically.
''' </summary>
''' 
''' <remarks>
''' Assemblies loaded into this context can be unloaded to free memory when they are no longer needed.
''' </remarks>
Public Class PluginLoadContext : Inherits AssemblyLoadContext

    ''' <summary>
    ''' Initializes a new instance of the <see cref="PluginLoadContext"/> class.
    ''' </summary>
    Public Sub New()

        MyBase.New(isCollectible:=True)
    End Sub

End Class
