using JMayer.Example.WindowsService.BSM;
using JMayer.Net.ProtocolDataUnit;
using System.Text;

namespace TestProject.Test;

/// <summary>
/// The class manages testing the BSM parser.
/// </summary>
public class BSMParserUnitTest
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
    /// The constant for the .F invalid flight number.
    /// </summary>
    private const string DotFInvalidFlightNumber = "ABCD";

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
    /// The method verifies a BSM with all fields set can be parsed.
    /// </summary>
    [Fact]
    public void VerifyAllFields()
    {
        BSM bsm = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = [DotNTagNumber],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = DotFAirline,
                ClassOfTravel = DotFClassOfTravel,
                Destination = DotFDestination,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new()
            {
                GivenNames = [DotPGivenName],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = 1,
            },
        };

        BSMParser parser = new();
        string bsmString = bsm.ToTypeB();
        byte[] bsmBytes = Encoding.ASCII.GetBytes(bsmString);
        
        PDUParserResult result = parser.Parse(bsmBytes);
        
        Assert.Single(result.PDUs);
        Assert.IsType<BSMPDU>(result.PDUs[0]);
        Assert.True(result.PDUs[0].IsValid, "The BSM is not valid.");
        Assert.True(new BMSEqualityComparer().Equals(bsm, ((BSMPDU)result.PDUs[0]).BSM), "The BSM does not equal the BSM in the parser results.");
    }

    /// <summary>
    /// The method verifies a BSM with a bad .F airline will cause a validation issue.
    /// </summary>
    [Fact]
    public void VerifyDotFAirlineValidation()
    {
        BSM bsm = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = [DotNTagNumber],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = DotFAirline.ToLower(),
                ClassOfTravel = DotFClassOfTravel,
                Destination = DotFDestination,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new()
            {
                GivenNames = [DotPGivenName],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = 1,
            },
        };

        BSMParser parser = new();
        string bsmString = bsm.ToTypeB();
        byte[] bsmBytes = Encoding.ASCII.GetBytes(bsmString);

        PDUParserResult result = parser.Parse(bsmBytes);

        Assert.Single(result.PDUs);
        Assert.IsType<BSMPDU>(result.PDUs[0]);
        Assert.False(result.PDUs[0].IsValid, "The BSM is valid. It's expected to be invalid.");
        Assert.Single(result.PDUs[0].ValidationResults);
        Assert.Contains(nameof(OutboundFlight.Airline), result.PDUs[0].ValidationResults.First().MemberNames);
    }

    /// <summary>
    /// The method verifies a BSM with a bad .F class of travel will cause a validation issue.
    /// </summary>
    [Fact]
    public void VerifyDotFClassOfTravelValidation()
    {
        BSM bsm = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = [DotNTagNumber],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = DotFAirline,
                ClassOfTravel = DotFClassOfTravel.ToLower(),
                Destination = DotFDestination,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new()
            {
                GivenNames = [DotPGivenName],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = 1,
            },
        };

        BSMParser parser = new();
        string bsmString = bsm.ToTypeB();
        byte[] bsmBytes = Encoding.ASCII.GetBytes(bsmString);

        PDUParserResult result = parser.Parse(bsmBytes);

        Assert.Single(result.PDUs);
        Assert.IsType<BSMPDU>(result.PDUs[0]);
        Assert.False(result.PDUs[0].IsValid, "The BSM is valid. It's expected to be invalid.");
        Assert.Single(result.PDUs[0].ValidationResults);
        Assert.Contains(nameof(OutboundFlight.ClassOfTravel), result.PDUs[0].ValidationResults.First().MemberNames);
    }

    /// <summary>
    /// The method verifies a BSM with a bad .F destination will cause a validation issue.
    /// </summary>
    [Fact]
    public void VerifyDotFDestinationValidation()
    {
        BSM bsm = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = [DotNTagNumber],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = DotFAirline,
                ClassOfTravel = DotFClassOfTravel,
                Destination = DotFDestination.ToLower(),
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new()
            {
                GivenNames = [DotPGivenName],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = 1,
            },
        };

        BSMParser parser = new();
        string bsmString = bsm.ToTypeB();
        byte[] bsmBytes = Encoding.ASCII.GetBytes(bsmString);

        PDUParserResult result = parser.Parse(bsmBytes);

        Assert.Single(result.PDUs);
        Assert.IsType<BSMPDU>(result.PDUs[0]);
        Assert.False(result.PDUs[0].IsValid, "The BSM is valid. It's expected to be invalid.");
        Assert.Single(result.PDUs[0].ValidationResults);
        Assert.Contains(nameof(OutboundFlight.Destination), result.PDUs[0].ValidationResults.First().MemberNames);
    }

    /// <summary>
    /// The method verifies a BSM with a bad .F flight number will cause a validation issue.
    /// </summary>
    [Fact]
    public void VerifyDotFFlightNumberValidation()
    {
        BSM bsm = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = [DotNTagNumber],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = DotFAirline,
                ClassOfTravel = DotFClassOfTravel,
                Destination = DotFDestination,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFInvalidFlightNumber,
            },
            PassengerName = new()
            {
                GivenNames = [DotPGivenName],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = 1,
            },
        };

        BSMParser parser = new();
        string bsmString = bsm.ToTypeB();
        byte[] bsmBytes = Encoding.ASCII.GetBytes(bsmString);

        PDUParserResult result = parser.Parse(bsmBytes);

        Assert.Single(result.PDUs);
        Assert.IsType<BSMPDU>(result.PDUs[0]);
        Assert.False(result.PDUs[0].IsValid, "The BSM is valid. It's expected to be invalid.");
        Assert.Single(result.PDUs[0].ValidationResults);
        Assert.Contains(nameof(OutboundFlight.FlightNumber), result.PDUs[0].ValidationResults.First().MemberNames);
    }

    /// <summary>
    /// The method verifies a BSM with a .F element with no class of travel can be parsed.
    /// </summary>
    [Fact]
    public void VerifyDotFNoClassOfTravel()
    {
        BSM bsm = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = [DotNTagNumber],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = DotFAirline,
                Destination = DotFDestination,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new()
            {
                GivenNames = [DotPGivenName],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = 1,
            },
        };

        BSMParser parser = new();
        string bsmString = bsm.ToTypeB();
        byte[] bsmBytes = Encoding.ASCII.GetBytes(bsmString);

        PDUParserResult result = parser.Parse(bsmBytes);

        Assert.Single(result.PDUs);
        Assert.IsType<BSMPDU>(result.PDUs[0]);
        Assert.True(result.PDUs[0].IsValid, "The BSM is not valid.");
        Assert.True(new BMSEqualityComparer().Equals(bsm, ((BSMPDU)result.PDUs[0]).BSM), "The BSM does not equal the BSM in the parser results.");
    }

    /// <summary>
    /// The method verifies a BSM with a .F element with no destination can be parsed; class of travel
    /// will also not be set.
    /// </summary>
    [Fact]
    public void VerifyDotFNoDestination()
    {
        BSM bsm = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = [DotNTagNumber],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = DotFAirline,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new()
            {
                GivenNames = [DotPGivenName],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = 1,
            },
        };

        BSMParser parser = new();
        string bsmString = bsm.ToTypeB();
        byte[] bsmBytes = Encoding.ASCII.GetBytes(bsmString);

        PDUParserResult result = parser.Parse(bsmBytes);

        Assert.Single(result.PDUs);
        Assert.IsType<BSMPDU>(result.PDUs[0]);
        Assert.True(result.PDUs[0].IsValid, "The BSM is not valid.");
        Assert.True(new BMSEqualityComparer().Equals(bsm, ((BSMPDU)result.PDUs[0]).BSM), "The BSM does not equal the BSM in the parser results.");
    }

    /// <summary>
    /// The method verifies a BSM with a .N element with  multiple tags can be parsed.
    /// </summary>
    [Fact]
    public void VerifyDotNMultipleTags()
    {
        BSM bsm = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = [DotNTagNumber, "0001123457", "0001123458", "0001123459", "0001123460"],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = DotFAirline,
                ClassOfTravel = DotFClassOfTravel,
                Destination = DotFDestination,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new()
            {
                GivenNames = [DotPGivenName],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = 1,
            },
        };

        BSMParser parser = new();
        string bsmString = bsm.ToTypeB();
        byte[] bsmBytes = Encoding.ASCII.GetBytes(bsmString);

        PDUParserResult result = parser.Parse(bsmBytes);

        Assert.Single(result.PDUs);
        Assert.IsType<BSMPDU>(result.PDUs[0]);
        Assert.True(result.PDUs[0].IsValid, "The BSM is not valid.");
        Assert.True(new BMSEqualityComparer().Equals(bsm, ((BSMPDU)result.PDUs[0]).BSM), "The BSM does not equal the BSM in the parser results.");
    }

    /// <summary>
    /// The method verifies a BSM with a .P element with multiple given names can be parsed.
    /// </summary>
    [Fact]
    public void VerifyDotPMultipleGivenNames()
    {
        BSM bsm = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = [DotNTagNumber],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = DotFAirline,
                ClassOfTravel = DotFClassOfTravel,
                Destination = DotFDestination,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new()
            {
                GivenNames = ["Given Name 1", "Given Name 2", "Given Name 3"],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = 1,
            },
        };

        BSMParser parser = new();
        string bsmString = bsm.ToTypeB();
        byte[] bsmBytes = Encoding.ASCII.GetBytes(bsmString);

        PDUParserResult result = parser.Parse(bsmBytes);

        Assert.Single(result.PDUs);
        Assert.IsType<BSMPDU>(result.PDUs[0]);
        Assert.True(result.PDUs[0].IsValid, "The BSM is not valid.");
        Assert.True(new BMSEqualityComparer().Equals(bsm, ((BSMPDU)result.PDUs[0]).BSM), "The BSM does not equal the BSM in the parser results.");
    }

    /// <summary>
    /// The method verifies a BSM with a .P element with no given names can be parsed.
    /// </summary>
    [Fact]
    public void VerifyDotPNoGivenNames()
    {
        BSM bsm = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = [DotNTagNumber],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = DotFAirline,
                ClassOfTravel = DotFClassOfTravel,
                Destination = DotFDestination,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new()
            {
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = 1,
            },
        };

        BSMParser parser = new();
        string bsmString = bsm.ToTypeB();
        byte[] bsmBytes = Encoding.ASCII.GetBytes(bsmString);

        PDUParserResult result = parser.Parse(bsmBytes);

        Assert.Single(result.PDUs);
        Assert.IsType<BSMPDU>(result.PDUs[0]);
        Assert.True(result.PDUs[0].IsValid, "The BSM is not valid.");
        Assert.True(new BMSEqualityComparer().Equals(bsm, ((BSMPDU)result.PDUs[0]).BSM), "The BSM does not equal the BSM in the parser results.");
    }

    /// <summary>
    /// The method verfies multiple BSMs can be parsed.
    /// </summary>
    [Fact]
    public void VerifyMultipleBSMs()
    {
        BSM bsm1 = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = [DotNTagNumber],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = DotFAirline,
                ClassOfTravel = DotFClassOfTravel,
                Destination = DotFDestination,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new()
            {
                GivenNames = [$"{DotPGivenName}1"],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = 1,
            },
        };

        BSM bsm2 = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = ["0001123457"],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = DotFAirline,
                ClassOfTravel = DotFClassOfTravel,
                Destination = DotFDestination,
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = DotFFlightNumber,
            },
            PassengerName = new()
            {
                GivenNames = [$"{DotPGivenName}2"],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = 1,
            },
        };

        BSMParser parser = new();
        string bsmString = bsm1.ToTypeB() + bsm2.ToTypeB();
        byte[] bsmBytes = Encoding.ASCII.GetBytes(bsmString);

        PDUParserResult result = parser.Parse(bsmBytes);

        Assert.Equal(2, result.PDUs.Count);
        Assert.IsType<BSMPDU>(result.PDUs[0]);
        Assert.IsType<BSMPDU>(result.PDUs[1]);
        Assert.True(result.PDUs[0].IsValid, "BSM 1 is not valid.");
        Assert.True(result.PDUs[1].IsValid, "BSM 2 is not valid.");
        Assert.True(new BMSEqualityComparer().Equals(bsm1, ((BSMPDU)result.PDUs[0]).BSM), "BSM 1 does not equal the first BSM in the parser results.");
        Assert.True(new BMSEqualityComparer().Equals(bsm2, ((BSMPDU)result.PDUs[1]).BSM), "BSM 2 does not equal the second BSM in the parser results.");
    }
}
