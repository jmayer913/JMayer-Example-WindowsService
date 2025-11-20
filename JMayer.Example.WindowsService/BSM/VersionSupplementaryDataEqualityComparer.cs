using System.Diagnostics.CodeAnalysis;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class manages comparing two VersionSupplementaryData objects.
/// </summary>
public class VersionSupplementaryDataEqualityComparer : IEqualityComparer<VersionSupplementaryData>
{
    /// <inheritdoc/>
    public bool Equals(VersionSupplementaryData? x, VersionSupplementaryData? y)
    {
        if (x is null && y is null)
        {
            return true;
        }
        else if (x is not null && y is not null)
        {
            return x.AirportCode == y.AirportCode
                && x.BaggageSourceIndicator == y.BaggageSourceIndicator
                && x.DataDictionaryVersionNumber == y.DataDictionaryVersionNumber;
        }
        else
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public int GetHashCode([DisallowNull] VersionSupplementaryData obj) => obj.GetHashCode();
}
