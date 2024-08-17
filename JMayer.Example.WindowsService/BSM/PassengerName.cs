using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class represents the passenger name in the BSM.
/// </summary>
public class PassengerName : ITypeB
{
    /// <summary>
    /// The constant for the .P element.
    /// </summary>
    public const string DotPElement = ".P";

    /// <summary>
    /// The property gets/sets the given names for the passenger.
    /// </summary>
    public List<string> GivenNames { get; init; } = [];

    /// <summary>
    /// The property gets/sets the surnaame for the passenger.
    /// </summary>
    [Required]
    public string SurName { get; set; } = string.Empty;

    /// <inheritdoc/>
    public void Parse(string typeBString)
    {
        //Remove the identifier so the elements can be broken apart with Split().
        typeBString = typeBString.Replace($"{DotPElement}/", string.Empty);
        typeBString = typeBString.Replace(Environment.NewLine , string.Empty);
        
        string[] elements = typeBString.Split('/');

        //Handle parsing the surname.
        if (elements.Length > 0 && !string.IsNullOrEmpty(elements[0]))
        {
            //The number of given names can be infront of the surname as either a 1 or 2 digit number
            //so remove the number if it exists.
            if (elements[0].Length >= 2 && Regex.IsMatch(elements[0].AsSpan(0, 2), "^\\d$"))
            {
                SurName = elements[0].Substring(2);
            }
            else if (elements[0].Length >= 1 && Regex.IsMatch(elements[0].AsSpan(0, 1), "^\\d$"))
            {
                SurName = elements[0].Substring(1);
            }
            else
            {
                SurName = elements[0];
            }
        }

        //The rest of the elements will be the given names so add them to the list.
        if (elements.Length > 1)
        {
            for (int index = 1; index < elements.Length; index++)
            {
                if (!string.IsNullOrEmpty(elements[index]))
                {
                    GivenNames.Add(elements[index]);
                }
            }
        }
    }

    /// <inheritdoc/>
    public string ToTypeB()
    {
        string givenNames = string.Empty;

        foreach (string givenName in GivenNames)
        {
            givenNames += $"/{givenName}";
        }

        return $"{DotPElement}/{GivenNames.Count}{SurName}{givenNames}{Environment.NewLine}";
    }
}
