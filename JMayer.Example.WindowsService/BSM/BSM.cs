using System.ComponentModel.DataAnnotations;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class represents a simplified version of a baggage source message.
/// </summary>
/// <remarks>
/// A baggage source message contains the passenger information, outbound flight information,
/// the baggage checked in at the ticket counter and other data. The outbound system uses this 
/// to bind a bag scanned at a scanner to a flight and to an end point in the system.
/// </remarks>
public class BSM : ITypeB
{
    /// <summary>
    /// The constant for the Add change of status.
    /// </summary>
    public const string Add = "ADD";

    /// <summary>
    /// The property gets the baggage tag details.
    /// </summary>
    public BaggageTagDetails? BaggageTagDetails { get; set; }

    /// <summary>
    /// The constant for the change change of status.
    /// </summary>
    public const string Change = "CHG";

    /// <summary>
    /// The property gets the change of status.
    /// </summary>
    [Required]
    public string ChangeOfStatus { get; set; } = string.Empty;

    /// <summary>
    /// The constant for the delete change of status.
    /// </summary>
    public const string Delete = "DEL";

    /// <summary>
    /// The constant for the end of BSM.
    /// </summary>
    public const string EndOfBSM = "ENDBSM";

    /// <summary>
    /// The property gets the outbound flight information.
    /// </summary>
    public OutboundFlight? OutboundFlight { get; set; }

    /// <summary>
    /// The property gets the passenger name.
    /// </summary>
    public PassengerName? PassengerName { get; set; }

    /// <summary>
    /// The property gets when the BSM was received.
    /// </summary>
    public DateTime ReceivedOn { get; init; } = DateTime.Now;

    /// <summary>
    /// The constant for the start of BSM.
    /// </summary>
    public const string StartOfBSM = "BSM";

    /// <summary>
    /// The property gets the version supplementary data.
    /// </summary>
    public VersionSupplementaryData? VersionSupplementaryData { get; set; }

    /// <summary>
    /// The method returns the change of status from the BSM.
    /// </summary>
    /// <param name="bsm">The BSM to examine.</param>
    /// <returns>The change of status.</returns>
    private static string GetChangeOfStatus(string bsm)
    {
        string changeOfStatus = bsm.Substring(0, 3);

        if (changeOfStatus == Change)
        {
            return Change;
        }
        else if (changeOfStatus == Delete)
        {
            return Delete;
        }
        else
        {
            return Add;
        }
    }

    /// <inheritdoc/>
    public void Parse(string typeBString)
    {
        //Remove the end identifier because it's no longer needed.
        //This needs to be removed before start else you end up with only END.
        typeBString = typeBString.Replace($"{EndOfBSM}{Environment.NewLine}", string.Empty);
        typeBString = typeBString.Replace(EndOfBSM, string.Empty);

        //Remove the start identifier because it's no longer needed.
        typeBString = typeBString.Replace($"{StartOfBSM}{Environment.NewLine}", string.Empty);
        typeBString = typeBString.Replace(StartOfBSM, string.Empty);

        //Get the change of status and then remove it because its no longer needed
        ChangeOfStatus = GetChangeOfStatus(typeBString);
        typeBString = typeBString.Replace($"{ChangeOfStatus}{Environment.NewLine}", string.Empty);
        typeBString = typeBString.Replace(ChangeOfStatus, string.Empty);

        int totalBytesProcessed = 0;

        do
        {
            int startIndex = typeBString.IndexOf('.', totalBytesProcessed);

            //Dot was not found so exit.
            if (startIndex == -1)
            {
                break;
            }

            int endIndex = typeBString.IndexOf('.', startIndex + 1);

            //If the next dot is not found then assume this is the last line.
            if (endIndex == -1)
            {
                endIndex = typeBString.Length;
            }

            string line = typeBString.Substring(startIndex, endIndex - startIndex);

            if (line.StartsWith(OutboundFlight.DotFElement))
            {
                OutboundFlight = new OutboundFlight();
                OutboundFlight.Parse(line);
            }
            else if (line.StartsWith(BaggageTagDetails.DotNElement))
            {
                BaggageTagDetails = new BaggageTagDetails();
                BaggageTagDetails.Parse(line);
            }
            else if (line.StartsWith(PassengerName.DotPElement))
            {
                PassengerName = new PassengerName();
                PassengerName.Parse(line);
            }
            else if (line.StartsWith(VersionSupplementaryData.DotVElement))
            {
                VersionSupplementaryData = new VersionSupplementaryData();
                VersionSupplementaryData.Parse(line);
            }

            totalBytesProcessed += line.Length;

        } while (totalBytesProcessed < typeBString.Length);
    }

    /// <inheritdoc/>
    public string ToTypeB()
    {
        string dotElements = string.Empty;

        if (OutboundFlight != null)
        {
            dotElements += OutboundFlight.ToTypeB();
        }

        if (BaggageTagDetails != null)
        {
            dotElements += BaggageTagDetails.ToTypeB();
        }

        if (PassengerName != null)
        {
            dotElements += PassengerName.ToTypeB();
        }

        if (VersionSupplementaryData != null)
        {
            dotElements += VersionSupplementaryData.ToTypeB();
        }

        return $"{StartOfBSM}{Environment.NewLine}{ChangeOfStatus}{Environment.NewLine}{dotElements}{EndOfBSM}{Environment.NewLine}";
    }
}
