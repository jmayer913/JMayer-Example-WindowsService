using JMayer.Example.WindowsService.BSM;

namespace TestProject.Test;

/// <summary>
/// The class manages testing the version supplementary data equality comparer.
/// </summary>
public class VersionSupplementaryDataEqualityComparerUnitTest
{
    /// <summary>
    /// The constant for the .V airport code.
    /// </summary>
    private const string DotVAirportCode = "MCO";

    /// <summary>
    /// The constant for the .V data dictionary version number.
    /// </summary>
    private const int DotVDataDictionaryVersionNumber = 1;

    /// <summary>
    /// The method verifies equality failure when the AirportCode property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureAirportCode()
    {
        VersionSupplementaryData versionSupplementaryData1 = new()
        {
            AirportCode = DotVAirportCode,
        };
        VersionSupplementaryData versionSupplementaryData2 = new();

        Assert.False(new VersionSupplementaryDataEqualityComparer().Equals(versionSupplementaryData1, versionSupplementaryData2));
    }

    /// <summary>
    /// The method verifies equality failure when the BaggageSourceIndicator property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureBaggageSourceIndicator()
    {
        VersionSupplementaryData versionSupplementaryData1 = new()
        {
            BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
        };
        VersionSupplementaryData versionSupplementaryData2 = new()
        {
            BaggageSourceIndicator = VersionSupplementaryData.TransferBaggageSourceIndicator,
        };

        Assert.False(new VersionSupplementaryDataEqualityComparer().Equals(versionSupplementaryData1, versionSupplementaryData2));
    }

    /// <summary>
    /// The method verifies equality failure when the DataDictionaryVersionNumber property is different between the two objects.
    /// </summary>
    [Fact]
    public void VerifyFailureDataDictionaryVersionNumber()
    {
        VersionSupplementaryData versionSupplementaryData1 = new()
        {
            DataDictionaryVersionNumber = DotVDataDictionaryVersionNumber,
        };
        VersionSupplementaryData versionSupplementaryData2 = new();

        Assert.False(new VersionSupplementaryDataEqualityComparer().Equals(versionSupplementaryData1, versionSupplementaryData2));
    }

    /// <summary>
    /// The method verifies equality failure when an object and null are compared.
    /// </summary>
    [Fact]
    public void VerifyFailureOneIsNull()
    {
        VersionSupplementaryData versionSupplementaryData1 = new()
        {
            AirportCode = DotVAirportCode,
            BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
            DataDictionaryVersionNumber = DotVDataDictionaryVersionNumber,
        };

        Assert.False(new VersionSupplementaryDataEqualityComparer().Equals(versionSupplementaryData1, null));
        Assert.False(new VersionSupplementaryDataEqualityComparer().Equals(null, versionSupplementaryData1));
    }

    /// <summary>
    /// The method verifies equality success when two objects (different references) are compared.
    /// </summary>
    [Fact]
    public void VerifySuccess()
    {
        VersionSupplementaryData versionSupplementaryData1 = new()
        {
            AirportCode = DotVAirportCode,
            BaggageSourceIndicator = VersionSupplementaryData.LocalBaggageSourceIndicator,
            DataDictionaryVersionNumber = DotVDataDictionaryVersionNumber,
        };
        VersionSupplementaryData versionSupplementaryData2 = new(versionSupplementaryData1);

        Assert.True(new VersionSupplementaryDataEqualityComparer().Equals(versionSupplementaryData1, versionSupplementaryData2));
    }

    /// <summary>
    /// The method verifies equality success when two nulls are compared.
    /// </summary>
    [Fact]
    public void VerifySuccessBothNull() => Assert.True(new VersionSupplementaryDataEqualityComparer().Equals(null, null));
}
