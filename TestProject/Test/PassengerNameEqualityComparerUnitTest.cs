using JMayer.Example.WindowsService.BSM;

namespace TestProject.Test;

/// <summary>
/// The class manages testing the passenger name equality comparer.
/// </summary>
public class PassengerNameEqualityComparerUnitTest
{
    /// <summary>
    /// The constant for the .P given name.
    /// </summary>
    private const string DotPGivenName = "PASSENGER";

    /// <summary>
    /// The constant for the .P given name.
    /// </summary>
    private const string DotPOtherGivenName = "OTHER";

    /// <summary>
    /// The constant for the .P surname.
    /// </summary>
    private const string DotPSurName = "TEST";

    /// <summary>
    /// The method verifies equality failure when the GivenNames property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureGivenNames()
    {
        PassengerName passengerName1 = new()
        {
            GivenNames = [DotPGivenName],
        };
        PassengerName passengerName2 = new()
        {
            GivenNames = [DotPOtherGivenName],
        };

        Assert.False(new PassengerNameEqualityComparer().Equals(passengerName1, passengerName2));
    }

    /// <summary>
    /// The method verifies equality failure when the GivenNames.Count property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureGivenNamesCount()
    {
        PassengerName passengerName1 = new()
        {
            GivenNames = [DotPGivenName],
        };
        PassengerName passengerName2 = new()
        {
            GivenNames = [DotPGivenName, DotPOtherGivenName],
        };

        Assert.False(new PassengerNameEqualityComparer().Equals(passengerName1, passengerName2));
    }

    /// <summary>
    /// The method verifies equality failure when the SurName property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureSurName()
    {
        PassengerName passengerName1 = new()
        {
            SurName = DotPSurName,
        };
        PassengerName passengerName2 = new();

        Assert.False(new PassengerNameEqualityComparer().Equals(passengerName1, passengerName2));
    }

    /// <summary>
    /// The method verifies equality failure when an object and null are compared.
    /// </summary>
    [Fact]
    public void VerifyFailureOneIsNull()
    {
        PassengerName passengerName1 = new()
        {
            GivenNames = [DotPGivenName],
            SurName = DotPSurName,
        };

        Assert.False(new PassengerNameEqualityComparer().Equals(passengerName1, null));
        Assert.False(new PassengerNameEqualityComparer().Equals(null, passengerName1));
    }

    /// <summary>
    /// The method verifies equality success when two objects (different references) are compared.
    /// </summary>
    [Fact]
    public void VerifySuccess()
    {
        PassengerName passengerName1 = new()
        {
            GivenNames = [DotPGivenName],
            SurName = DotPSurName,
        };
        PassengerName passengerName2 = new(passengerName1);

        Assert.True(new PassengerNameEqualityComparer().Equals(passengerName1, passengerName2));
    }

    /// <summary>
    /// The method verifies equality success when two nulls are compared.
    /// </summary>
    [Fact]
    public void VerifySuccessBothNull() => Assert.True(new PassengerNameEqualityComparer().Equals(null, null));
}
