using JMayer.Example.WindowsService.BSM;

namespace TestProject.Test;

/// <summary>
/// The class manages testing the baggage tag detail equality comparer.
/// </summary>
public class BaggageTagDetailEqualityComparerUnitTest
{
    /// <summary>
    /// The constant for the .N tag number.
    /// </summary>
    private const string DotNOtherTagNumber = "0001123457";

    /// <summary>
    /// The constant for the .N tag number.
    /// </summary>
    private const string DotNTagNumber = "0001123456";

    /// <summary>
    /// The method verifies equality failure when the BaggageTagNumbers property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureBaggageTagNumbers()
    {
        BaggageTagDetails baggageTagDetails1 = new()
        {
            BaggageTagNumbers = [DotNTagNumber],
        };
        BaggageTagDetails baggageTagDetails2 = new()
        {
            BaggageTagNumbers = [DotNOtherTagNumber],
        };

        Assert.False(new BaggageTagDetailEqualityComparer().Equals(baggageTagDetails1, baggageTagDetails2));
    }

    /// <summary>
    /// The method verifies equality failure when the Count property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureCount()
    {
        BaggageTagDetails baggageTagDetails1 = new()
        {
            BaggageTagNumbers = [DotNTagNumber],
        };
        BaggageTagDetails baggageTagDetails2 = new()
        {
            BaggageTagNumbers = [DotNTagNumber, DotNOtherTagNumber],
        };

        Assert.False(new BaggageTagDetailEqualityComparer().Equals(baggageTagDetails1, baggageTagDetails2));
    }

    /// <summary>
    /// The method verifies equality failure when an object and null are compared.
    /// </summary>
    [Fact]
    public void VerifyFailureOneIsNull()
    {
        BaggageTagDetails baggageTagDetails1 = new()
        {
            BaggageTagNumbers = [DotNTagNumber],
        };

        Assert.False(new BaggageTagDetailEqualityComparer().Equals(baggageTagDetails1, null));
        Assert.False(new BaggageTagDetailEqualityComparer().Equals(null, baggageTagDetails1));
    }

    /// <summary>
    /// The method verifies equality success when two objects (different references) are compared.
    /// </summary>
    [Fact]
    public void VerifySuccess()
    {
        BaggageTagDetails baggageTagDetails1 = new()
        {
            BaggageTagNumbers = [DotNTagNumber],
        };
        BaggageTagDetails baggageTagDetails2 = new(baggageTagDetails1);

        Assert.True(new BaggageTagDetailEqualityComparer().Equals(baggageTagDetails1, baggageTagDetails2));
    }

    /// <summary>
    /// The method verifies equality success when two nulls are compared.
    /// </summary>
    [Fact]
    public void VerifySuccessBothNull() => Assert.True(new BaggageTagDetailEqualityComparer().Equals(null, null));
}
