#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

''' <summary>
''' Tests for the <see cref="RegistrationFlags"/> enumeration.
''' </summary>
<TestClass>
Public NotInheritable Class RegistrationFlagsTests

#Region " Tests "

    ''' <summary>
    ''' The enumeration must be decorated with <see cref="FlagsAttribute"/>, since plugins combine values with <c>Or</c>.
    ''' </summary>
    <TestMethod>
    Public Sub Enum_HasFlagsAttribute()

        Dim hasFlags As Boolean = GetType(RegistrationFlags).IsDefined(GetType(FlagsAttribute), inherit:=False)
        Assert.IsTrue(hasFlags)
    End Sub

    ''' <summary>
    ''' <see cref="RegistrationFlags.Null"/> must be zero, so that it can be used as the initial (empty) value.
    ''' </summary>
    <TestMethod>
    Public Sub Null_IsZero()

        Assert.AreEqual(0, CInt(RegistrationFlags.Null))
    End Sub

    ''' <summary>
    ''' Each flag must have the expected bit value (it is part of the public plugin contract).
    ''' </summary>
    <TestMethod>
    <DataRow(RegistrationFlags.RegistrationOpen, 1 << 0)>
    <DataRow(RegistrationFlags.RegistrationClosed, 1 << 1)>
    <DataRow(RegistrationFlags.RegistrationUnknown, 1 << 2)>
    <DataRow(RegistrationFlags.ApplicationOpen, 1 << 3)>
    <DataRow(RegistrationFlags.ApplicationClosed, 1 << 4)>
    <DataRow(RegistrationFlags.ApplicationUnknown, 1 << 5)>
    Public Sub Flag_HasExpectedValue(flag As RegistrationFlags, expectedValue As Integer)

        Assert.AreEqual(expectedValue, CInt(flag))
    End Sub

    ''' <summary>
    ''' Every defined value (except <see cref="RegistrationFlags.Null"/>) must be a single, unique bit.
    ''' </summary>
    <TestMethod>
    Public Sub AllNonNullValues_AreUniqueSingleBits()

        Dim values As Integer() = [Enum].GetValues(Of RegistrationFlags)().
                                         Where(Function(flag As RegistrationFlags) flag <> RegistrationFlags.Null).
                                         Select(Function(flag As RegistrationFlags) CInt(flag)).
                                         ToArray()

        Assert.AreEqual(6, values.Length)
        For Each value As Integer In values
            Assert.IsTrue(value > 0 AndAlso (value And (value - 1)) = 0, $"Value {value} is not a single bit.")
        Next
        Assert.AreEqual(values.Length, values.Distinct().Count())
    End Sub

    ''' <summary>
    ''' Combining a registration result with an application result (as plugins do) must preserve both flags.
    ''' </summary>
    <TestMethod>
    Public Sub CombinedFlags_PreserveBothStates()

        Dim regFlags As RegistrationFlags = RegistrationFlags.Null
        regFlags = regFlags Or RegistrationFlags.RegistrationClosed Or RegistrationFlags.ApplicationOpen

        Assert.IsTrue(regFlags.HasFlag(RegistrationFlags.RegistrationClosed))
        Assert.IsTrue(regFlags.HasFlag(RegistrationFlags.ApplicationOpen))
        Assert.IsFalse(regFlags.HasFlag(RegistrationFlags.RegistrationOpen))
        Assert.IsFalse(regFlags.HasFlag(RegistrationFlags.ApplicationClosed))
        Assert.IsFalse(regFlags.HasFlag(RegistrationFlags.RegistrationUnknown))
        Assert.IsFalse(regFlags.HasFlag(RegistrationFlags.ApplicationUnknown))
    End Sub

#End Region

End Class
