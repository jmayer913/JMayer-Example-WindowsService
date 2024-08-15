using System.Diagnostics.CodeAnalysis;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class manages comparing two PassengerName objects.
/// </summary>
public class PassengerNameEqualityComparer : IEqualityComparer<PassengerName>
{
    /// <inheritdoc/>
    public bool Equals(PassengerName? x, PassengerName? y)
    {
        if (x == null && y == null)
        {
            return true;
        }
        else if (x != null && y != null)
        {
            if (x.SurName != y.SurName || x.GivenNames.Count != y.GivenNames.Count)
            {
                return false;
            }
            else
            {
                foreach (string givenName in x.GivenNames)
                {
                    if (!y.GivenNames.Contains(givenName))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        else
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public int GetHashCode([DisallowNull] PassengerName obj)
    {
        throw new NotImplementedException();
    }
}
