#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel

#End Region

''' <summary>
''' An extended <see cref="CheckedListBox"/> that provides a convenient way to 
''' get and set the checked items as a persistable list of strings, 
''' allowing the control's checked state to be saved and restored.
''' </summary>
Public Class PersistableCheckedListBox : Inherits CheckedListBox

#Region " Public Properties "

    ''' <summary>
    ''' Gets or sets the list of checked items as their string representations.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' <para></para>
    ''' When setting this property, the control will automatically check all items
    ''' whose string representations match any entry in the provided list.
    ''' </remarks>
    ''' 
    ''' <returns>
    ''' A <see cref="List(Of String)"/> containing the string representations of the currently checked items.
    ''' </returns>
    ''' 
    ''' <seealso cref="PersistableCheckedListBox.PropertyCheckedItemsAsStringChanged"/>
    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property CheckedItemsAsStrings As List(Of String)

        <DebuggerStepThrough>
        Get
            Return Me.CheckedItems.Cast(Of Object)().Select(
                Function(obj As Object)
                    Dim itemStr As String = obj.ToString()
                    If itemStr Is Nothing Then
                        Dim msg As String = $"The object of type {obj.GetType().Name} does not have a string representation."
                        Throw New NotImplementedException(msg)
                    End If

                    Return itemStr
                End Function).ToList()
        End Get
        <DebuggerStepThrough>
        Set(value As List(Of String))
            If value Is Nothing Then
                For i As Integer = 0 To Me.Items.Count - 1
                    Me.SetItemChecked(i, False)
                Next
            Else
                For i As Integer = 0 To Me.Items.Count - 1
                    Dim itemStr As String = If(Me.Items(i)?.ToString(), String.Empty)
                    Me.SetItemChecked(i, value.Contains(itemStr))
                Next

            End If

            Me.OnPropertyCheckedItemsAsStringChanged(EventArgs.Empty)
        End Set
    End Property

#End Region

#Region " Events "

    ''' <summary>
    ''' Occurs when the <see cref="PersistableCheckedListBox.CheckedItemsAsStrings"/> property is set,
    ''' so the checked items of the control have been set according to the provided list.
    ''' </summary>
    ''' 
    ''' <seealso cref="PersistableCheckedListBox.CheckedItemsAsStrings"/>
    Public Event PropertyCheckedItemsAsStringChanged As EventHandler

#End Region

#Region " Event Invokers "

    ''' <summary>
    ''' Raises the <see cref="PersistableCheckedListBox.PropertyCheckedItemsAsStringChanged"/> event.
    ''' </summary>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    Protected Overridable Sub OnPropertyCheckedItemsAsStringChanged(e As EventArgs)

        If Me.PropertyCheckedItemsAsStringChangedEvent IsNot Nothing Then
            RaiseEvent PropertyCheckedItemsAsStringChanged(Me, e)
        End If
    End Sub

#End Region

End Class
