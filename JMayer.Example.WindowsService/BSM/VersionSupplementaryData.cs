using System.ComponentModel.DataAnnotations;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class represents the version & supplementary data in the BSM.
/// </summary>
public class VersionSupplementaryData : ITypeB
{
    /// <summary>
    /// The property gets the aiport who sent the BSM.
    /// </summary>
    [Required]
    [RegularExpression("^[A-Z]{3}$", ErrorMessage = "The airport code must be 3 capital letters.")]
    public string AirportCode { get; set; } = string.Empty;

    /// <summary>
    /// The property gets the baggage source indicator (local, transfer, remote or terminating).
    /// </summary>
    [Required]
    [RegularExpression("^(L|R|X|T)$", ErrorMessage = "The baggage source indicator must be L, R, X or T.")]
    public string BaggageSourceIndicator { get; set; } = string.Empty;

    /// <summary>
    /// The property gets the version number for the data dictionary.
    /// </summary>
    [Required]
    [Range(1, 9)]
    public int DataDictionaryVersionNumber { get; set; }

    /// <summary>
    /// The constant for the .V element.
    /// </summary>
    public const string DotVElement = ".V";

    /// <summary>
    /// The constant for the local baggage source indicator.
    /// </summary>
    public const string LocalBaggageSourceIndicator = "L";

    /// <summary>
    /// The constant for the remote baggage source indicator.
    /// </summary>
    public const string RemoteBaggageSourceIndicator = "R";

    /// <summary>
    /// The constant for the terminating baggage source indicator.
    /// </summary>
    public const string TerminatingBaggageSourceIndicator = "X";

    /// <summary>
    /// The constant for the transfer baggage source indicator.
    /// </summary>
    public const string TransferBaggageSourceIndicator = "T";

    /// <inheritdoc/>
    public void Parse(string typeBString)
    {
        //Remove the identifier so the elements can be broken apart with Split().
        typeBString = typeBString.Replace($"{DotVElement}/", string.Empty);
        typeBString = typeBString.Replace(Environment.NewLine, string.Empty);

        string[] elements = typeBString.Split('/');

        if (elements.Length > 0 && elements[0].Length is 5)
        {
            if (int.TryParse(elements[0].AsSpan(0, 1), out int dataDictionaryVersionNumber))
            {
                DataDictionaryVersionNumber = dataDictionaryVersionNumber;
            }

            BaggageSourceIndicator = elements[0].Substring(1, 1);
            AirportCode = elements[0].Substring(2, 3);
        }
    }

    /// <inheritdoc/>
    public string ToTypeB()
    {
        return $"{DotVElement}/{DataDictionaryVersionNumber}{BaggageSourceIndicator}{AirportCode}{Environment.NewLine}";
    }
}
