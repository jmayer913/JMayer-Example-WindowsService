namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class manages generating an IATA (10 digit tag number).
/// </summary>
public class IATAGenerator
{
    /// <summary>
    /// Keeps track of the sequence number to be used.
    /// </summary>
    private int _sequenceNumber = MinSequenceNumber;

    /// <summary>
    /// The property gets/sets the alphanumeric  airline code.
    /// </summary>
    /// <remarks>
    /// Used by the BSM generator.
    /// </remarks>
    public string AirlineAlphaNumericCode { get; init; } = string.Empty;

    /// <summary>
    /// The property gets/sets the numeric airline code used by the IATA. 
    /// </summary>
    public string AirlineNumericCode { get; init; } = string.Empty;

    /// <summary>
    /// The constant for the maximum sequence number.
    /// </summary>
    public const int MaxSequenceNumber = 999999;

    /// <summary>
    /// The constant for the minimum sequence number.
    /// </summary>
    public const int MinSequenceNumber = 1;

    /// <summary>
    /// The method returns the next IATA.
    /// </summary>
    /// <returns>The IATA.</returns>
    public string Generate()
    {
        string iata = $"0{AirlineNumericCode}{_sequenceNumber.ToString().PadLeft(6, '0')}";

        _sequenceNumber++;

        if (_sequenceNumber > MaxSequenceNumber)
        {
            _sequenceNumber = MinSequenceNumber;
        }

        return iata;
    }
}
