using System.Diagnostics.CodeAnalysis;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class manages comparing two BSM objects.
/// </summary>
public class BMSEqualityComparer : IEqualityComparer<BSM>
{
    /// <inheritdoc/>
    public bool Equals(BSM? x, BSM? y)
    {
        if (x == null || y == null)
        {
            return false;
        }

        BaggageTagDetailEqualityComparer baggageTagDetailEqualityComparer = new();
        OutboundFlightEqualityComparer outboundFlightEqualityComparer = new();
        PassengerNameEqualityComparer passengerNameEqualityComparer = new();
        VersionSupplementaryDataEqualityComparer versionSupplementaryDataEqualityComparer = new();

        return baggageTagDetailEqualityComparer.Equals(x.BaggageTagDetails, y.BaggageTagDetails)
            && x.ChangeOfStatus == y.ChangeOfStatus
            && outboundFlightEqualityComparer.Equals(x.OutboundFlight, y.OutboundFlight)
            && passengerNameEqualityComparer.Equals(x.PassengerName, y.PassengerName)
            && versionSupplementaryDataEqualityComparer.Equals(x.VersionSupplementaryData, y.VersionSupplementaryData);
    }

    /// <inheritdoc/>
    public int GetHashCode([DisallowNull] BSM obj)
    {
        throw new NotImplementedException();
    }
}
