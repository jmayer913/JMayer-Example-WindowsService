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
        if (x == null && y == null)
        {
            return true;
        }
        else if (x != null && y != null)
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
    public int GetHashCode([DisallowNull] VersionSupplementaryData obj)
    {
        return obj.GetHashCode();
    }
}
