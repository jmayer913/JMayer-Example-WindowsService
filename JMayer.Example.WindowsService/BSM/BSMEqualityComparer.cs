using System.Diagnostics.CodeAnalysis;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class manages comparing two BSM objects.
/// </summary>
public class BSMEqualityComparer : IEqualityComparer<BSM>
{
    /// <inheritdoc/>
    public bool Equals(BSM? x, BSM? y)
    {
        if (x == null || y == null)
        {
            return false;
        }

        return new BaggageTagDetailEqualityComparer().Equals(x.BaggageTagDetails, y.BaggageTagDetails)
            && x.ChangeOfStatus == y.ChangeOfStatus
            && new OutboundFlightEqualityComparer().Equals(x.OutboundFlight, y.OutboundFlight)
            && new PassengerNameEqualityComparer().Equals(x.PassengerName, y.PassengerName)
            && new VersionSupplementaryDataEqualityComparer().Equals(x.VersionSupplementaryData, y.VersionSupplementaryData);
    }

    /// <inheritdoc/>
    public int GetHashCode([DisallowNull] BSM obj)
    {
        throw new NotImplementedException();
    }
}
