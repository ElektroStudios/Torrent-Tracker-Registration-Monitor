#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

''' <summary>
''' Tests for the <see cref="PersistableCheckedListBox"/> control,
''' used to persist the plugins selected for automatic execution.
''' <para></para>
''' These tests require Windows, because they create a Windows Forms control.
''' </summary>
<TestClass>
<OSCondition(OperatingSystems.Windows)>
Public NotInheritable Class PersistableCheckedListBoxTests

#Region " Tests "

    ''' <summary>
    ''' Getting <see cref="PersistableCheckedListBox.CheckedItemsAsStrings"/> must return the string representation of the checked items.
    ''' </summary>
    <TestMethod>
    Public Sub CheckedItemsAsStrings_Get_ReturnsCheckedItemStrings()

        Using clb As PersistableCheckedListBox = PersistableCheckedListBoxTests.CreateListBox()
            clb.SetItemChecked(0, True)
            clb.SetItemChecked(2, True)

            CollectionAssert.AreEqual({"Blutopia", "HD-Space"}, clb.CheckedItemsAsStrings)
        End Using
    End Sub

    ''' <summary>
    ''' Getting <see cref="PersistableCheckedListBox.CheckedItemsAsStrings"/> without checked items must return an empty list.
    ''' </summary>
    <TestMethod>
    Public Sub CheckedItemsAsStrings_Get_NothingChecked_ReturnsEmptyList()

        Using clb As PersistableCheckedListBox = PersistableCheckedListBoxTests.CreateListBox()
            Assert.IsEmpty(clb.CheckedItemsAsStrings)
        End Using
    End Sub

    ''' <summary>
    ''' Setting <see cref="PersistableCheckedListBox.CheckedItemsAsStrings"/> must check exactly the matching items,
    ''' ignoring strings that do not match any item (e.g. plugins that were removed).
    ''' </summary>
    <TestMethod>
    Public Sub CheckedItemsAsStrings_Set_ChecksMatchingItems()

        Using clb As PersistableCheckedListBox = PersistableCheckedListBoxTests.CreateListBox()
            clb.SetItemChecked(1, True) ' Must be unchecked after setting the property.

            clb.CheckedItemsAsStrings = New List(Of String) From {"HD-Space", "Blutopia", "Removed Plugin"}

            Assert.IsTrue(clb.GetItemChecked(0))
            Assert.IsFalse(clb.GetItemChecked(1))
            Assert.IsTrue(clb.GetItemChecked(2))
        End Using
    End Sub

    ''' <summary>
    ''' Setting <see cref="PersistableCheckedListBox.CheckedItemsAsStrings"/> to <see langword="Nothing"/> must uncheck all items.
    ''' </summary>
    <TestMethod>
    Public Sub CheckedItemsAsStrings_SetNothing_UnchecksAllItems()

        Using clb As PersistableCheckedListBox = PersistableCheckedListBoxTests.CreateListBox()
            For i As Integer = 0 To clb.Items.Count - 1
                clb.SetItemChecked(i, True)
            Next

            clb.CheckedItemsAsStrings = Nothing

            Assert.AreEqual(0, clb.CheckedItems.Count)
        End Using
    End Sub

    ''' <summary>
    ''' Setting <see cref="PersistableCheckedListBox.CheckedItemsAsStrings"/> must raise
    ''' <see cref="PersistableCheckedListBox.PropertyCheckedItemsAsStringChanged"/> event once.
    ''' </summary>
    <TestMethod>
    Public Sub CheckedItemsAsStrings_Set_RaisesChangedEvent()

        Using clb As PersistableCheckedListBox = PersistableCheckedListBoxTests.CreateListBox()
            Dim raisedCount As Integer = 0
            Dim eventSender As Object = Nothing
            AddHandler clb.PropertyCheckedItemsAsStringChanged,
                Sub(sender As Object, e As EventArgs)
                    raisedCount += 1
                    eventSender = sender
                End Sub

            clb.CheckedItemsAsStrings = New List(Of String) From {"Blutopia"}

            Assert.AreEqual(1, raisedCount)
            Assert.AreSame(clb, eventSender)
        End Using
    End Sub

    ''' <summary>
    ''' A round-trip (get, then set on a new control) must restore the same checked items,
    ''' which is how the settings are persisted between sessions.
    ''' </summary>
    <TestMethod>
    Public Sub CheckedItemsAsStrings_RoundTrip_RestoresCheckedItems()

        Dim persisted As List(Of String)
        Using clb As PersistableCheckedListBox = PersistableCheckedListBoxTests.CreateListBox()
            clb.SetItemChecked(1, True)
            clb.SetItemChecked(2, True)
            persisted = clb.CheckedItemsAsStrings
        End Using

        Using clb As PersistableCheckedListBox = PersistableCheckedListBoxTests.CreateListBox()
            clb.CheckedItemsAsStrings = persisted
            CollectionAssert.AreEqual(persisted, clb.CheckedItemsAsStrings)
        End Using
    End Sub

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Creates a <see cref="PersistableCheckedListBox"/> with three plugin items: "Blutopia", "BeyondHD" and "HD-Space".
    ''' </summary>
    '''
    ''' <returns>
    ''' The resulting <see cref="PersistableCheckedListBox"/>.
    ''' </returns>
    Private Shared Function CreateListBox() As PersistableCheckedListBox

        Dim clb As New PersistableCheckedListBox()
        clb.Items.AddRange({New FakePlugin("Blutopia"), New FakePlugin("BeyondHD"), New FakePlugin("HD-Space")})
        Return clb
    End Function

#End Region

End Class
