using JMayer.Example.WindowsService.BSM;

namespace TestProject.Test;

/// <summary>
/// The class manages testing the outbound flight equality comparer.
/// </summary>
public class OutboundFlightEqualityComparerUnitTest
{
    /// <summary>
    /// The constant for the .F airline.
    /// </summary>
    private const string DotFAirline = "AA";

    /// <summary>
    /// The constant for the .F class of travel.
    /// </summary>
    private const string DotFClassOfTravel = "A";

    /// <summary>
    /// The constant for the .F destination.
    /// </summary>
    private const string DotFDestination = "MSY";

    /// <summary>
    /// The constant for the .F flight number.
    /// </summary>
    private const string DotFFlightNumber = "1234";

    /// <summary>
    /// The method verifies equality failure when the Airline property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureAirline()
    {
        OutboundFlight outboundFlight1 = new()
        {
            Airline = DotFAirline,
        };
        OutboundFlight outboundFlight2 = new();

        Assert.False(new OutboundFlightEqualityComparer().Equals(outboundFlight1, outboundFlight2));
    }

    /// <summary>
    /// The method verifies equality failure when the ClassOfTravel property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureClassOfTravel()
    {
        OutboundFlight outboundFlight1 = new()
        {
            ClassOfTravel = DotFClassOfTravel,
        };
        OutboundFlight outboundFlight2 = new();

        Assert.False(new OutboundFlightEqualityComparer().Equals(outboundFlight1, outboundFlight2));
    }

    /// <summary>
    /// The method verifies equality failure when the Destination property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureDestination()
    {
        OutboundFlight outboundFlight1 = new()
        {
            Destination = DotFDestination,
        };
        OutboundFlight outboundFlight2 = new();

        Assert.False(new OutboundFlightEqualityComparer().Equals(outboundFlight1, outboundFlight2));
    }

    /// <summary>
    /// The method verifies equality failure when the FlightDate property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureFlightDate()
    {
        OutboundFlight outboundFlight1 = new()
        {
            FlightDate = DateTime.Today.ToDayMonthFormat(),
        };
        OutboundFlight outboundFlight2 = new()
        {
            FlightDate = DateTime.Today.AddDays(1).ToDayMonthFormat(),
        };

        Assert.False(new OutboundFlightEqualityComparer().Equals(outboundFlight1, outboundFlight2));
    }

    /// <summary>
    /// The method verifies equality failure when the FlightNumber property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureFlightNumber()
    {
        OutboundFlight outboundFlight1 = new()
        {
            FlightNumber = DotFFlightNumber,
        };
        OutboundFlight outboundFlight2 = new();

        Assert.False(new OutboundFlightEqualityComparer().Equals(outboundFlight1, outboundFlight2));
    }

    /// <summary>
    /// The method verifies equality failure when an object and null are compared.
    /// </summary>
    [Fact]
    public void VerifyFailureOneIsNull()
    {
        OutboundFlight outboundFlight1 = new()
        {
            Airline = DotFAirline,
            ClassOfTravel = DotFClassOfTravel,
            Destination = DotFDestination,
            FlightDate = DateTime.Today.ToDayMonthFormat(),
            FlightNumber = DotFFlightNumber,
        };

        Assert.False(new OutboundFlightEqualityComparer().Equals(outboundFlight1, null));
        Assert.False(new OutboundFlightEqualityComparer().Equals(null, outboundFlight1));
    }

    /// <summary>
    /// The method verifies equality success when two objects (different references) are compared.
    /// </summary>
    [Fact]
    public void VerifySuccess()
    {
        OutboundFlight outboundFlight1 = new()
        {
            Airline = DotFAirline,
            ClassOfTravel = DotFClassOfTravel,
            Destination = DotFDestination,
            FlightDate = DateTime.Today.ToDayMonthFormat(),
            FlightNumber = DotFFlightNumber,
        };
        OutboundFlight outboundFlight2 = new(outboundFlight1);

        Assert.True(new OutboundFlightEqualityComparer().Equals(outboundFlight1, outboundFlight2));
    }

    /// <summary>
    /// The method verifies equality success when two nulls are compared.
    /// </summary>
    [Fact]
    public void VerifySuccessBothNull() => Assert.True(new OutboundFlightEqualityComparer().Equals(null, null));
}
