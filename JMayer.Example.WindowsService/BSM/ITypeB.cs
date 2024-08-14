namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The interface contains common methods for the type B string format.
/// </summary>
public interface ITypeB
{
    /// <summary>
    /// The method parses a type B string.
    /// </summary>
    /// <param name="typeBString">The type B string to parse.</param>
    void Parse(string typeBString);

    /// <summary>
    /// The method returns the object in the type B string format.
    /// </summary>
    /// <returns>A string in the type B format.</returns>
    string ToTypeB();
}
