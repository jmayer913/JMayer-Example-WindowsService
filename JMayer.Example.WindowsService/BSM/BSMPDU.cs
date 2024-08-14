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
        return ValidateDataAnnotations();
    }
}
