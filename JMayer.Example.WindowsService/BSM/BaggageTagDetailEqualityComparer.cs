using System.Diagnostics.CodeAnalysis;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class manages comparing two BaggageTagDetail objects.
/// </summary>
public class BaggageTagDetailEqualityComparer : IEqualityComparer<BaggageTagDetails>
{
    /// <inheritdoc/>
    public bool Equals(BaggageTagDetails? x, BaggageTagDetails? y)
    {
        if (x is null && y is null)
        {
            return true;
        }
        else if (x is not null && y is not null)
        {
            if (x.Count != y.Count)
            {
                return false;
            }
            else
            {
                foreach (string tag in x.BaggageTagNumbers)
                {
                    if (y.BaggageTagNumbers.Contains(tag) is false)
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
    public int GetHashCode([DisallowNull] BaggageTagDetails obj) => obj.GetHashCode();
}
