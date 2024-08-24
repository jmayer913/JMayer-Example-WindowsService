using JMayer.Example.WindowsService.BSM;

namespace TestProject.Test;

/// <summary>
/// The class manages testing the BSM equality comparer.
/// </summary>
public class BSMEqualityComparerUnitTest
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
    /// The constant for the .N tag number.
    /// </summary>
    private const string DotNTagNumber = "0001123456";

    /// <summary>
    /// The constant for the .P given name.
    /// </summary>
    private const string DotPGivenName = "PASSENGER";

    /// <summary>
    /// The constant for the .P surname.
    /// </summary>
    private const string DotPSurName = "TEST";

    /// <summary>
    /// The constant for the .V airport code.
    /// </summary>
    private const string DotVAirportCode = "MCO";

    /// <summary>
    /// The constant for the .V data dictionary version number.
    /// </summary>
    private const int DotVDataDictionaryVersionNumber = 1;

    /// <summary>
    /// The method verifies equality failure when the ChangeOfStatus property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureChangeOfStatus()
    {
        BSM bsm1 = new()
        {
            ChangeOfStatus = BSM.Add,
        };
        BSM bsm2 = new()
        {
            ChangeOfStatus = BSM.Change,
        };

        Assert.False(new BSMEqualityComparer().Equals(bsm1, bsm2));
    }

    /// <summary>
    /// The method verifies equality failure when two nulls are compared.
    /// </summary>
    [Fact]
    public void VerifyFailureBothNull() => Assert.False(new BSMEqualityComparer().Equals(null, null));

    /// <summary>
    /// The method verifies equality failure when an object and null are compared.
    /// </summary>
    [Fact]
    public void VerifyFailureOneIsNull()
    {
        BSM bsm = new()
        {
            BaggageTagDetails = new BaggageTagDetails()
            {
                BaggageTagNumbers = [DotNTagNumber],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new OutboundFlight()
            {
                Airline = DotFAirline,
                ClassOfTravel = DotFClassOfTravel,
                Destination = DotFDestination,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new PassengerName()
            {
                GivenNames = [DotPGivenName],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = DotVDataDictionaryVersionNumber,
            },
        };

        Assert.False(new BSMEqualityComparer().Equals(bsm, null));
        Assert.False(new BSMEqualityComparer().Equals(null, bsm));
    }

    /// <summary>
    /// The method verifies equality failure when an object and null are compared.
    /// </summary>
    [Fact]
    public void VerifySuccess()
    {
        BSM bsm1 = new()
        {
            BaggageTagDetails = new BaggageTagDetails()
            {
                BaggageTagNumbers = [DotNTagNumber],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new OutboundFlight()
            {
                Airline = DotFAirline,
                ClassOfTravel = DotFClassOfTravel,
                Destination = DotFDestination,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new PassengerName()
            {
                GivenNames = [DotPGivenName],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = DotVDataDictionaryVersionNumber,
            },
        };
        BSM bsm2 = new(bsm1);

        Assert.True(new BSMEqualityComparer().Equals(bsm1, bsm2));
    }
}
