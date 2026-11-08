#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports Microsoft.CodeAnalysis.VisualBasic

#End Region

''' <summary>
''' Tests for the UI-independent members of <see cref="UIHelper"/> module.
''' </summary>
<TestClass>
Public NotInheritable Class UIHelperTests

#Region " ConvertStringToNetMemberName "

    ''' <summary>
    ''' Plugin names (including the ones of the real plugins) must be converted to the expected PascalCase identifier.
    ''' </summary>
    <TestMethod>
    <DataRow("Blutopia", "Blutopia")>
    <DataRow("HD-Space", "HDSpace")>
    <DataRow("Lat-Team", "LatTeam")>
    <DataRow("Old Toons World", "OldToonsWorld")>
    <DataRow("# Site AutoLogin", "SiteAutoLogin")>
    <DataRow("hello world", "HelloWorld")>
    <DataRow("  padded name  ", "PaddedName")>
    <DataRow("multiple   spaces", "MultipleSpaces")>
    <DataRow("snake_case_name", "SnakeCaseName")>
    <DataRow("xBytes v2", "XBytesV2")>
    <DataRow("iPtorrents", "IPtorrents")>
    Public Sub ConvertStringToNetMemberName_ReturnsPascalCase(value As String, expected As String)

        Assert.AreEqual(expected, UIHelper.ConvertStringToNetMemberName(value))
    End Sub

    ''' <summary>
    ''' Names that start with a digit must be prefixed with an underscore.
    ''' </summary>
    <TestMethod>
    <DataRow("3D Torrents", "_3DTorrents")>
    <DataRow("4th Dimension", "_4thDimension")>
    <DataRow("3ChangTrai", "_3ChangTrai")>
    <DataRow("123", "_123")>
    Public Sub ConvertStringToNetMemberName_LeadingDigit_IsPrefixedWithUnderscore(value As String, expected As String)

        Assert.AreEqual(expected, UIHelper.ConvertStringToNetMemberName(value))
    End Sub

    ''' <summary>
    ''' Accents and other diacritics must be removed instead of being treated as word separators.
    ''' </summary>
    <TestMethod>
    <DataRow("Café Olé", "CafeOle")>
    <DataRow("Ñandú", "Nandu")>
    <DataRow("Pokémon Torrents", "PokemonTorrents")>
    Public Sub ConvertStringToNetMemberName_RemovesDiacritics(value As String, expected As String)

        Assert.AreEqual(expected, UIHelper.ConvertStringToNetMemberName(value))
    End Sub

    ''' <summary>
    ''' The result must always be a valid Visual Basic identifier,
    ''' because it is used to build the names of the dynamically created controls.
    ''' </summary>
    <TestMethod>
    <DataRow("Blutopia")>
    <DataRow("3D Torrents")>
    <DataRow("# Site AutoLogin")>
    <DataRow("Café-Olé!! (v2.0)")>
    <DataRow("a")>
    <DataRow("9")>
    Public Sub ConvertStringToNetMemberName_ReturnsValidIdentifier(value As String)

        Dim result As String = UIHelper.ConvertStringToNetMemberName(value)
        Assert.IsTrue(SyntaxFacts.IsValidIdentifier(result), $"'{result}' is not a valid identifier.")
    End Sub

    ''' <summary>
    ''' A null, empty or white-space value must throw <see cref="ArgumentNullException"/>.
    ''' </summary>
    <TestMethod>
    <DataRow(Nothing)>
    <DataRow("")>
    <DataRow("   ")>
    <DataRow(vbTab)>
    Public Sub ConvertStringToNetMemberName_NullOrWhiteSpace_Throws(value As String)

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() UIHelper.ConvertStringToNetMemberName(value))
    End Sub

    ''' <summary>
    ''' A value without any valid alphanumeric (ASCII) characters must throw <see cref="ArgumentException"/>.
    ''' </summary>
    <TestMethod>
    <DataRow("!!!")>
    <DataRow("#-#")>
    <DataRow("日本語")>
    Public Sub ConvertStringToNetMemberName_NoAlphanumericChars_Throws(value As String)

        Assert.ThrowsExactly(Of ArgumentException)(Sub() UIHelper.ConvertStringToNetMemberName(value))
    End Sub

#End Region

End Class
