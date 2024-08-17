using JMayer.Example.WindowsService.BSM;

namespace TestProject.Test;

/// <summary>
/// The class manages testing the IATA generator.
/// </summary>
public class IATAGeneratorUnitTest
{
    /// <summary>
    /// The constant for the airline numeric code.
    /// </summary>
    private const string AirlineNumericCode = "001";

    /// <summary>
    /// The method verifies an IATA will be generated.
    /// </summary>
    [Fact]
    public void VerifyGeneration()
    {
        IATAGenerator iataGenerator = new()
        {
            AirlineNumericCode = AirlineNumericCode,
        };
        string iata = iataGenerator.Generate();

        Assert.Equal($"0{iataGenerator.AirlineNumericCode}{IATAGenerator.MinSequenceNumber.ToString().PadLeft(6, '0')}", iata);
    }

    /// <summary>
    /// The method verifies if 3 IATAs are generated, each is incremented by 1.
    /// </summary>
    [Fact]
    public void VerifyIncrementByOne()
    {
        IATAGenerator iataGenerator = new()
        {
            AirlineNumericCode = AirlineNumericCode,
        };
        
        string iata1 = iataGenerator.Generate();
        string iata2 = iataGenerator.Generate();
        string iata3 = iataGenerator.Generate();

        Assert.Equal($"0{iataGenerator.AirlineNumericCode}{IATAGenerator.MinSequenceNumber.ToString().PadLeft(6, '0')}", iata1);
        Assert.Equal($"0{iataGenerator.AirlineNumericCode}{(IATAGenerator.MinSequenceNumber + 1).ToString().PadLeft(6, '0')}", iata2);
        Assert.Equal($"0{iataGenerator.AirlineNumericCode}{(IATAGenerator.MinSequenceNumber + 2).ToString().PadLeft(6, '0')}", iata3);
    }

    /// <summary>
    /// The method verifies the generator will rollover after 999999 IATAs are generated.
    /// </summary>
    [Fact]
    public void VerifyRollover()
    {
        string iata = string.Empty;
        IATAGenerator iataGenerator = new()
        {
            AirlineNumericCode = AirlineNumericCode,
        };

        for (int index = 0; index <= IATAGenerator.MaxSequenceNumber; index++)
        {
            iata = iataGenerator.Generate();
        }

        Assert.Equal($"0{iataGenerator.AirlineNumericCode}{IATAGenerator.MinSequenceNumber.ToString().PadLeft(6, '0')}", iata);
    }
}
