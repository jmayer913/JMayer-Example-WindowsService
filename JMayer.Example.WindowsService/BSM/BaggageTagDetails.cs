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
    public int Count
    {
        get => BaggageTagNumbers.Count;
    }

    /// <summary>
    /// The property gets a list of baggage tag numbers.
    /// </summary>
    [Length(1, 999)]
    public List<string> BaggageTagNumbers { get; init; } = [];

    /// <summary>
    /// The constant for the .N element.
    /// </summary>
    public const string DotNElement = ".N";

    /// <inheritdoc/>
    public void Parse(string typeBString)
    {
        //Remove the identifier and new line.
        typeBString = typeBString.Replace($"{DotNElement}/", string.Empty);
        typeBString = typeBString.Replace(Environment.NewLine, string.Empty);

        if (typeBString.Length is 13)
        {
            if (long.TryParse(typeBString.AsSpan(0, 10), out long iataNumber) && int.TryParse(typeBString.AsSpan(10, 3), out int length))
            {
                for (int index = 0; index < length; index++)
                {
                    string iataString = (iataNumber + index).ToString().PadLeft(10, '0');
                    BaggageTagNumbers.Add(iataString);
                }
            }
        }
    }

    /// <inheritdoc/>
    public string ToTypeB()
    {
        if (Count == 0)
        {
            return string.Empty;
        }
        else
        {
            return $"{DotNElement}/{BaggageTagNumbers[0]}{Count:D3}{Environment.NewLine}";
        }
    }
}
