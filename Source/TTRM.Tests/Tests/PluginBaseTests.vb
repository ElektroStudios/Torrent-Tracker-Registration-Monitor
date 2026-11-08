#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

''' <summary>
''' Tests for the <see cref="PluginBase"/> class.
''' </summary>
<TestClass>
Public NotInheritable Class PluginBaseTests

#Region " ToString "

    ''' <summary>
    ''' <see cref="PluginBase.ToString"/> must return the plugin name (it is displayed in the auto-run CheckedListBox).
    ''' </summary>
    <TestMethod>
    Public Sub ToString_ReturnsName()

        Using plugin As New FakePlugin("Blutopia")
            Assert.AreEqual("Blutopia", plugin.ToString())
        End Using
    End Sub

#End Region

#Region " Equals "

    ''' <summary>
    ''' Two plugins with the same name, ignoring case, must be equal.
    ''' </summary>
    <TestMethod>
    <DataRow("Blutopia", "Blutopia")>
    <DataRow("Blutopia", "BLUTOPIA")>
    <DataRow("hd-space", "HD-Space")>
    Public Sub Equals_SameNameIgnoringCase_ReturnsTrue(name1 As String, name2 As String)

        Using plugin1 As New FakePlugin(name1), plugin2 As New FakePlugin(name2)
            Assert.IsTrue(plugin1.Equals(plugin2))
            Assert.IsTrue(plugin2.Equals(plugin1))
            Assert.IsTrue(plugin1.Equals(DirectCast(plugin2, Object)))
        End Using
    End Sub

    ''' <summary>
    ''' Two plugins with different names must not be equal.
    ''' </summary>
    <TestMethod>
    Public Sub Equals_DifferentName_ReturnsFalse()

        Using plugin1 As New FakePlugin("Blutopia"), plugin2 As New FakePlugin("BeyondHD")
            Assert.IsFalse(plugin1.Equals(plugin2))
        End Using
    End Sub

    ''' <summary>
    ''' A plugin must not be equal to <see langword="Nothing"/> or to an object of another type.
    ''' </summary>
    <TestMethod>
    Public Sub Equals_NothingOrOtherType_ReturnsFalse()

        Using plugin As New FakePlugin("Blutopia")
            Assert.IsFalse(plugin.Equals(DirectCast(Nothing, PluginBase)))
            Assert.IsFalse(plugin.Equals(DirectCast(Nothing, Object)))
            Assert.IsFalse(plugin.Equals("Blutopia"))
        End Using
    End Sub

#End Region

#Region " GetHashCode "

    ''' <summary>
    ''' Equal plugins (same name ignoring case) must return the same hash code.
    ''' </summary>
    <TestMethod>
    Public Sub GetHashCode_SameNameIgnoringCase_ReturnsSameHash()

        Using plugin1 As New FakePlugin("Blutopia"), plugin2 As New FakePlugin("bLuToPiA")
            Assert.AreEqual(plugin1.GetHashCode(), plugin2.GetHashCode())
        End Using
    End Sub

    ''' <summary>
    ''' A plugin without name must return zero as hash code, instead of throwing.
    ''' </summary>
    <TestMethod>
    Public Sub GetHashCode_NullName_ReturnsZero()

        Using plugin As New FakePlugin(Nothing)
            Assert.AreEqual(0, plugin.GetHashCode())
        End Using
    End Sub

    ''' <summary>
    ''' Regression test: the hash code must be consistent with <see cref="PluginBase.Equals(PluginBase)"/>
    ''' also for canonically equivalent Unicode names
    ''' (precomposed "é" vs. "e" followed by a combining acute accent),
    ''' which are considered equal by the culture-aware comparison used in <c>Equals</c>.
    ''' </summary>
    <TestMethod>
    Public Sub GetHashCode_CanonicallyEquivalentNames_IsConsistentWithEquals()

        Dim precomposed As String = $"Caf{ChrW(&HE9)} Torrents"
        Dim decomposed As String = $"Cafe{ChrW(&H301)} Torrents"

        Using plugin1 As New FakePlugin(precomposed), plugin2 As New FakePlugin(decomposed)
            If plugin1.Equals(plugin2) Then
                Assert.AreEqual(plugin1.GetHashCode(), plugin2.GetHashCode())
            Else
                Assert.Inconclusive("The current globalization mode does not consider both strings equal.")
            End If
        End Using
    End Sub

    ''' <summary>
    ''' A <see cref="HashSet(Of T)"/> must detect duplicated plugins by name, ignoring case
    ''' (this is how TTRM detects duplicated plugins at startup).
    ''' </summary>
    <TestMethod>
    Public Sub HashSet_DetectsDuplicatedPluginsIgnoringCase()

        Using plugin1 As New FakePlugin("Blutopia"),
              plugin2 As New FakePlugin("BLUTOPIA"),
              plugin3 As New FakePlugin("BeyondHD")

            Dim uniquePlugins As New HashSet(Of PluginBase) From {plugin1, plugin2, plugin3}
            Assert.HasCount(2, uniquePlugins)
        End Using
    End Sub

#End Region

#Region " Dispose "

    ''' <summary>
    ''' Disposing a plugin without image must not throw, and can be called multiple times.
    ''' </summary>
    <TestMethod>
    Public Sub Dispose_WithoutImage_CanBeCalledMultipleTimes()

        Dim plugin As New FakePlugin("Blutopia")
        plugin.Dispose()
        plugin.Dispose()
        Assert.IsNull(plugin.Image)
    End Sub

    ''' <summary>
    ''' Disposing a plugin must dispose and release its image.
    ''' </summary>
    <TestMethod>
    <OSCondition(OperatingSystems.Windows)>
    Public Sub Dispose_WithImage_DisposesAndReleasesImage()

        Dim bmp As New Bitmap(4, 4)
        Dim plugin As New FakePlugin("Blutopia") With {.Image = bmp}

        plugin.Dispose()

        Assert.IsNull(plugin.Image)
        ' Accessing a property of a disposed GDI+ image throws ArgumentException.
        Assert.ThrowsExactly(Of ArgumentException)(Function() bmp.Width)
    End Sub

#End Region

#Region " RunAsync "

    ''' <summary>
    ''' A derived plugin must be able to return a result through <see cref="PluginBase.RunAsync"/>.
    ''' </summary>
    <TestMethod>
    Public Async Function RunAsync_DerivedPlugin_ReturnsResult() As Task

        Using plugin As New FakePlugin("Blutopia")
            Dim result As RegistrationFlags = Await plugin.RunAsync(Nothing).ConfigureAwait(False)
            Assert.AreEqual(RegistrationFlags.RegistrationOpen, result)
        End Using
    End Function

#End Region

End Class
