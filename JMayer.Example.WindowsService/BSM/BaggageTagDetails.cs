using System.ComponentModel.DataAnnotations;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class represents the baggage tag details in the BSM.
/// </summary>
public class BaggageTagDetails : ITypeB
{
    /// <summary>
    /// The property gets the number of baggage tag numbers.
    /// </summary>
    public int Count => BaggageTagNumbers.Count;

    /// <summary>
    /// The property gets a list of baggage tag numbers.
    /// </summary>
    /// <remarks>
    /// Index 0 must be the first tag and index N must be the last tag. Each tag
    /// after the first must increment by 1.
    /// </remarks>
    [Length(1, 999)]
    public List<string> BaggageTagNumbers { get; init; } = [];

    /// <summary>
    /// The constant for the .N element.
    /// </summary>
    public const string DotNElement = ".N";

    /// <summary>
    /// The constant for the maximum character length of the .N element.
    /// </summary>
    private const int DotnMaximumCharacterLength = 13;

    /// <summary>
    /// The default constructor.
    /// </summary>
    public BaggageTagDetails() { }

    /// <summary>
    /// The copy constructor.
    /// </summary>
    /// <param name="copy">The object to copy from.</param>
    public BaggageTagDetails(BaggageTagDetails copy) => BaggageTagNumbers.AddRange(copy.BaggageTagNumbers);

    /// <inheritdoc/>
    public void Parse(string typeBString)
    {
        //Remove the identifier and new line.
        typeBString = typeBString.Replace($"{DotNElement}{BSM.ElementSeparator}", string.Empty);
        typeBString = typeBString.Replace(Environment.NewLine, string.Empty);

        if (typeBString.Length is not DotnMaximumCharacterLength)
        {
            return;
        }
        
        if (long.TryParse(typeBString.AsSpan(0, 10), out long iataNumber) is false)
        {
            return;
        }
        
        if (int.TryParse(typeBString.AsSpan(10, 3), out int length) is false)
        {
            return;
        }

        for (int index = 0; index < length; index++)
        {
            string iataString = (iataNumber + index).ToString().PadLeft(10, '0');
            BaggageTagNumbers.Add(iataString);
        }
    }

    /// <inheritdoc/>
    public string ToTypeB()
    {
        if (Count is 0)
        {
            return string.Empty;
        }
        else
        {
            return $"{DotNElement}{BSM.ElementSeparator}{BaggageTagNumbers[0]}{Count:D3}{Environment.NewLine}";
        }
    }
}
