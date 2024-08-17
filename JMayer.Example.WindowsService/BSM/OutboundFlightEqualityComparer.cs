using System.Diagnostics.CodeAnalysis;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class manages comparing two OutboundFlight objects.
/// </summary>
public class OutboundFlightEqualityComparer : IEqualityComparer<OutboundFlight>
{
    /// <inheritdoc/>
    public bool Equals(OutboundFlight? x, OutboundFlight? y)
    {
        if (x == null && y == null)
        {
            return true;
        }
        else if (x != null && y != null)
        {
            return x.Airline == y.Airline
                && x.ClassOfTravel == y.ClassOfTravel
                && x.Destination == y.Destination
                && x.FlightDate == y.FlightDate
                && x.FlightNumber == y.FlightNumber;
        }
        else
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public int GetHashCode([DisallowNull] OutboundFlight obj)
    {
        throw new NotImplementedException();
    }
}
