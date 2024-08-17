namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class manages generating a BSM.
/// </summary>
public class BSMGenerator
{
    /// <summary>
    /// The class of travels.
    /// </summary>
    private readonly string[] _classOfTravels =
        [
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z"
        ];

    /// <summary>
    /// The destinations.
    /// </summary>
    private readonly string[] _destinations =
        [
            "CAE",
            "BNA",
            "LBB",
            "MSY",
            "OAK",
            "PIE",
            "RSW",
            "VPS",
        ];

    /// <summary>
    /// The destination being used.
    /// </summary>
    private int _destinationIndex = 0;

    /// <summary>
    /// The flight number for the BSM.
    /// </summary>
    private int _flightNumber = MinFlightNumber;

    /// <summary>
    /// Used to generate IATAs.
    /// </summary>
    private readonly IATAGenerator[] _iataGenerators =
        [
            new IATAGenerator() { AirlineAlphaNumericCode = "AA", AirlineNumericCode = "001", },
            new IATAGenerator() { AirlineAlphaNumericCode = "DL", AirlineNumericCode = "006", },
            new IATAGenerator() { AirlineAlphaNumericCode = "UA", AirlineNumericCode = "016", },
            new IATAGenerator() { AirlineAlphaNumericCode = "WN", AirlineNumericCode = "526", },
        ];

    /// <summary>
    /// The IATA generator being used.
    /// </summary>
    private int _iataGeneratorIndex = 0;

    /// <summary>
    /// The passenger count.
    /// </summary>
    private int _passengerCount = MinPassengerCount;

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
    /// The constant for the maximum flight number.
    /// </summary>
    public const int MaxFlightNumber = 9999;

    /// <summary>
    /// The constant for the minimum flight number.
    /// </summary>
    public const int MinFlightNumber = 1;

    /// <summary>
    /// The constant for the maximum number of passengers.
    /// </summary>
    public const int MaxPassengerCount = 100;

    /// <summary>
    /// The constant for the minimum number of passengers.
    /// </summary>
    public const int MinPassengerCount = 1;

    /// <summary>
    /// The default constructor.
    /// </summary>
    public BSMGenerator()
    {
        SetRandomDestination();
    }

    /// <summary>
    /// The method returns the next BSM.
    /// </summary>
    /// <returns>The BSM.</returns>
    public BSM Generate()
    {
        IATAGenerator generator = _iataGenerators[_iataGeneratorIndex];

        BSM bsm = new()
        {
            BaggageTagDetails = new()
            {
                BaggageTagNumbers = [generator.Generate()],
            },
            ChangeOfStatus = BSM.Add,
            OutboundFlight = new()
            {
                Airline = generator.AirlineAlphaNumericCode,
                ClassOfTravel = _classOfTravels[new Random(DateTime.Now.Second).Next(0, _classOfTravels.Length - 1)],
                Destination = _destinations[_destinationIndex],
                FlightDate = DateTime.Today.ToDayMonthFormat(),
                FlightNumber = _flightNumber.ToString().PadLeft(4, '0'),
            },
            PassengerName = new()
            {
                GivenNames = [$"{DotPGivenName}{_passengerCount}"],
                SurName = DotPSurName,
            },
            VersionSupplementaryData = new()
            {
                AirportCode = DotVAirportCode,
                BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
                DataDictionaryVersionNumber = DotVDataDictionaryVersionNumber,
            },
        };

        IncrementPassengerCount();

        return bsm;
    }

    /// <summary>
    /// The method increments the flight number.
    /// </summary>
    private void IncrementFlightNumber()
    {
        _flightNumber++;

        if (_flightNumber > MaxFlightNumber)
        {
            _flightNumber = MinFlightNumber;
        }
    }

    /// <summary>
    /// The method increments to the next IATA generator.
    /// </summary>
    private void IncrementIATAGenerator()
    {
        _iataGeneratorIndex++;

        if (_iataGeneratorIndex == _iataGenerators.Length)
        {
            _iataGeneratorIndex = 0;
        }
    }

    /// <summary>
    /// The method increments the passenger count.
    /// </summary>
    /// <remarks>
    /// When the maximum number of passengers are reached, the flight number is incremented, 
    /// a new IATA generator is used and a new destination is randomly chosen. The idea is the 
    /// generator will generate X BSMs for a flight and then a new one is used.
    /// </remarks>
    private void IncrementPassengerCount()
    {
        _passengerCount++;

        if (_passengerCount > MaxPassengerCount)
        {
            _passengerCount = MinPassengerCount;
            IncrementFlightNumber();
            IncrementIATAGenerator();
            SetRandomDestination();
        }
    }

    /// <summary>
    /// The method sets a random destination.
    /// </summary>
    private void SetRandomDestination()
    {
        _destinationIndex = new Random(DateTime.Now.Second).Next(0, _destinations.Length - 1);
    }
}
