using JMayer.Example.WindowsService.BSM;

namespace TestProject.Test;

/// <summary>
/// The class manages testing the BSM generator.
/// </summary>
public class BSMGeneratorUnitTest
{
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
    /// The constant for the maximum number of airlines used by the BSM generator.
    /// </summary>
    private const int MaxAirline = 4;

    /// <summary>
    /// The method verifies the generator will rollover to the first airline after the maximum number is generated.
    /// </summary>
    [Fact]
    public void VerifyAirlineRollover()
    {
        BSM bsm = new();
        BSMGenerator bsmGenerator = new();
        int maxPassengers = BSMGenerator.MaxPassengerCount * MaxAirline;

        for (int index = 0; index <= maxPassengers; index++)
        {
            bsm = bsmGenerator.Generate();
        }

        //Verify the baggage tag details.
        Assert.NotNull(bsm.BaggageTagDetails);
        Assert.Single(bsm.BaggageTagDetails.BaggageTagNumbers);
        Assert.Equal($"0001{(BSMGenerator.MaxPassengerCount + 1).ToString().PadLeft(6, '0')}", bsm.BaggageTagDetails.BaggageTagNumbers[0]);

        //Verify the change of status.
        Assert.Equal(BSM.Add, bsm.ChangeOfStatus);

        //Verify the outbound flight.
        Assert.NotNull(bsm.OutboundFlight);
        Assert.Equal("AA", bsm.OutboundFlight.Airline);
        Assert.NotEmpty(bsm.OutboundFlight.ClassOfTravel); //Class of Travel is randomly set so just make sure its set to something.
        Assert.NotEmpty(bsm.OutboundFlight.Destination); //Destination is randomly set so just make sure its set to something.
        Assert.Equal(DateTime.Today.ToDayMonthFormat(), bsm.OutboundFlight.FlightDate);
        Assert.Equal((MaxAirline + 1).ToString().PadLeft(4, '0'), bsm.OutboundFlight.FlightNumber);

        //Verify the passenger name.
        Assert.NotNull(bsm.PassengerName);
        Assert.Single(bsm.PassengerName.GivenNames);
        Assert.Equal($"{DotPGivenName}1", bsm.PassengerName.GivenNames[0]);
        Assert.Equal(DotPSurName, bsm.PassengerName.SurName);

        //Verify the version supplementary data.
        Assert.NotNull(bsm.VersionSupplementaryData);
        Assert.Equal(DotVAirportCode, bsm.VersionSupplementaryData.AirportCode);
        Assert.Equal(VersionSupplementaryData.LocalBaggageSourceIndicator, bsm.VersionSupplementaryData.BaggageSourceIndicator);
        Assert.Equal(DotVDataDictionaryVersionNumber, bsm.VersionSupplementaryData.DataDictionaryVersionNumber);
    }

    /// <summary>
    /// The method verifies the generator will rollover the flight number after the maximum number is generated.
    /// </summary>
    [Fact]
    public void VerifyFlightNumberRollover()
    {
        BSM bsm = new();
        BSMGenerator bsmGenerator = new();
        int maxPassengers = BSMGenerator.MaxPassengerCount * BSMGenerator.MaxFlightNumber;

        for (int index = 0; index <= maxPassengers; index++)
        {
            bsm = bsmGenerator.Generate();
        }

        //Verify the baggage tag details.
        Assert.NotNull(bsm.BaggageTagDetails);
        Assert.Single(bsm.BaggageTagDetails.BaggageTagNumbers);
        Assert.NotEmpty(bsm.BaggageTagDetails.BaggageTagNumbers[0]); //999,900 passengers are generated and I don't think there's an easy to know what the IATA number will be so just make sure its set.

        //Verify the change of status.
        Assert.Equal(BSM.Add, bsm.ChangeOfStatus);

        //Verify the outbound flight.
        Assert.NotNull(bsm.OutboundFlight);
        Assert.NotEmpty(bsm.OutboundFlight.Airline); //999,900 passengers are generated and I don't think there's an easy to know what the airline will be so just make sure its set.
        Assert.NotEmpty(bsm.OutboundFlight.ClassOfTravel); //Class of Travel is randomly set so just make sure its set to something.
        Assert.NotEmpty(bsm.OutboundFlight.Destination); //Destination is randomly set so just make sure its set to something.
        Assert.Equal(DateTime.Today.ToDayMonthFormat(), bsm.OutboundFlight.FlightDate);
        Assert.Equal(BSMGenerator.MinFlightNumber.ToString().PadLeft(4, '0'), bsm.OutboundFlight.FlightNumber);

        //Verify the passenger name.
        Assert.NotNull(bsm.PassengerName);
        Assert.Single(bsm.PassengerName.GivenNames);
        Assert.Equal($"{DotPGivenName}1", bsm.PassengerName.GivenNames[0]);
        Assert.Equal(DotPSurName, bsm.PassengerName.SurName);

        //Verify the version supplementary data.
        Assert.NotNull(bsm.VersionSupplementaryData);
        Assert.Equal(DotVAirportCode, bsm.VersionSupplementaryData.AirportCode);
        Assert.Equal(VersionSupplementaryData.LocalBaggageSourceIndicator, bsm.VersionSupplementaryData.BaggageSourceIndicator);
        Assert.Equal(DotVDataDictionaryVersionNumber, bsm.VersionSupplementaryData.DataDictionaryVersionNumber);
    }

    /// <summary>
    /// The class verifies a BSM is generated.
    /// </summary>
    [Fact]
    public void VerifyGeneration()
    {
        BSMGenerator bsmGenerator = new();
        BSM bsm = bsmGenerator.Generate();

        //Verify the baggage tag details.
        Assert.NotNull(bsm.BaggageTagDetails);
        Assert.Single(bsm.BaggageTagDetails.BaggageTagNumbers);
        Assert.Equal($"0001{IATAGenerator.MinSequenceNumber.ToString().PadLeft(6, '0')}", bsm.BaggageTagDetails.BaggageTagNumbers[0]);
        
        //Verify the change of status.
        Assert.Equal(BSM.Add, bsm.ChangeOfStatus);

        //Verify the outbound flight.
        Assert.NotNull(bsm.OutboundFlight);
        Assert.Equal("AA", bsm.OutboundFlight.Airline);
        Assert.NotEmpty(bsm.OutboundFlight.ClassOfTravel); //Class of Travel is randomly set so just make sure its set to something.
        Assert.NotEmpty(bsm.OutboundFlight.Destination); //Destination is randomly set so just make sure its set to something.
        Assert.Equal(DateTime.Today.ToDayMonthFormat(), bsm.OutboundFlight.FlightDate);
        Assert.Equal(BSMGenerator.MinFlightNumber.ToString().PadLeft(4, '0'), bsm.OutboundFlight.FlightNumber);

        //Verify the passenger name.
        Assert.NotNull(bsm.PassengerName);
        Assert.Single(bsm.PassengerName.GivenNames);
        Assert.Equal($"{DotPGivenName}1", bsm.PassengerName.GivenNames[0]);
        Assert.Equal(DotPSurName, bsm.PassengerName.SurName);

        //Verify the version supplementary data.
        Assert.NotNull(bsm.VersionSupplementaryData);
        Assert.Equal(DotVAirportCode, bsm.VersionSupplementaryData.AirportCode);
        Assert.Equal(VersionSupplementaryData.LocalBaggageSourceIndicator, bsm.VersionSupplementaryData.BaggageSourceIndicator);
        Assert.Equal(DotVDataDictionaryVersionNumber, bsm.VersionSupplementaryData.DataDictionaryVersionNumber);
    }

    /// <summary>
    /// The method verifies the passenger increments by one on each generation.
    /// </summary>
    [Fact]
    public void VerifyPassengerIncrementsByOne()
    {
        BSMGenerator bsmGenerator = new();
        BSM bsm1 = bsmGenerator.Generate();
        BSM bsm2 = bsmGenerator.Generate();
        BSM bsm3 = bsmGenerator.Generate();

        Assert.NotNull(bsm1.PassengerName);
        Assert.Equal($"{DotPGivenName}1", bsm1.PassengerName.GivenNames[0]);

        Assert.NotNull(bsm2.PassengerName);
        Assert.Equal($"{DotPGivenName}2", bsm2.PassengerName.GivenNames[0]);

        Assert.NotNull(bsm3.PassengerName);
        Assert.Equal($"{DotPGivenName}3", bsm3.PassengerName.GivenNames[0]);
    }

    /// <summary>
    /// The method verifies the generator will rollover the passengers after the maximum number is generated. It also verifies a new airline and flight number are generated.
    /// </summary>
    [Fact]
    public void VerifyPassengerRollover()
    {
        BSM bsm = new();
        BSMGenerator bsmGenerator = new();

        for (int index = 0; index <= BSMGenerator.MaxPassengerCount; index++)
        {
            bsm = bsmGenerator.Generate();
        }

        //Verify the baggage tag details.
        Assert.NotNull(bsm.BaggageTagDetails);
        Assert.Single(bsm.BaggageTagDetails.BaggageTagNumbers);
        //The sequence used by the BSM generator is 001, 006, 016, 526 so on the first passenger rollover, 006 will be used next.
        Assert.Equal($"0006{IATAGenerator.MinSequenceNumber.ToString().PadLeft(6, '0')}", bsm.BaggageTagDetails.BaggageTagNumbers[0]);

        //Verify the change of status.
        Assert.Equal(BSM.Add, bsm.ChangeOfStatus);

        //Verify the outbound flight.
        Assert.NotNull(bsm.OutboundFlight);
        Assert.Equal("DL", bsm.OutboundFlight.Airline); //The sequence used by the BSM generator is AA, DL, UA, WN so on the first passenger rollover, DL will be used next.
        Assert.NotEmpty(bsm.OutboundFlight.ClassOfTravel); //Class of Travel is randomly set so just make sure its set to something.
        Assert.NotEmpty(bsm.OutboundFlight.Destination); //Destination is randomly set so just make sure its set to something.
        Assert.Equal(DateTime.Today.ToDayMonthFormat(), bsm.OutboundFlight.FlightDate);
        Assert.Equal((BSMGenerator.MinFlightNumber + 1).ToString().PadLeft(4, '0'), bsm.OutboundFlight.FlightNumber);

        //Verify the passenger name.
        Assert.NotNull(bsm.PassengerName);
        Assert.Single(bsm.PassengerName.GivenNames);
        Assert.Equal($"{DotPGivenName}1", bsm.PassengerName.GivenNames[0]);
        Assert.Equal(DotPSurName, bsm.PassengerName.SurName);

        //Verify the version supplementary data.
        Assert.NotNull(bsm.VersionSupplementaryData);
        Assert.Equal(DotVAirportCode, bsm.VersionSupplementaryData.AirportCode);
        Assert.Equal(VersionSupplementaryData.LocalBaggageSourceIndicator, bsm.VersionSupplementaryData.BaggageSourceIndicator);
        Assert.Equal(DotVDataDictionaryVersionNumber, bsm.VersionSupplementaryData.DataDictionaryVersionNumber);
    }
}
