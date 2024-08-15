using JMayer.Net.ProtocolDataUnit;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class represents a BSM PDU.
/// </summary>
public class BSMPDU : PDU
{
    /// <summary>
    /// The property gets/sets the BSM string.
    /// </summary>
    public BSM BSM { get; init; } = new();

    /// <inheritdoc/>
    public override byte[] ToBytes()
    {
        return Encoding.ASCII.GetBytes(BSM.ToTypeB());
    }

    /// <inheritdoc/>
    public override List<ValidationResult> Validate()
    {
        List<ValidationResult> validationResults = [];

        if (BSM.BaggageTagDetails != null)
        {
            Validator.TryValidateObject(BSM.BaggageTagDetails, new ValidationContext(BSM.BaggageTagDetails), validationResults, validateAllProperties: true);
        }

        if (BSM.OutboundFlight != null)
        {
            Validator.TryValidateObject(BSM.OutboundFlight, new ValidationContext(BSM.OutboundFlight), validationResults, validateAllProperties: true);
        }

        if (BSM.PassengerName != null)
        {
            Validator.TryValidateObject(BSM.PassengerName, new ValidationContext(BSM.PassengerName), validationResults, validateAllProperties: true);
        }

        if (BSM.VersionSupplementaryData != null)
        {
            Validator.TryValidateObject(BSM.VersionSupplementaryData, new ValidationContext(BSM.VersionSupplementaryData), validationResults, validateAllProperties: true);
        }

        return validationResults;
    }
}
