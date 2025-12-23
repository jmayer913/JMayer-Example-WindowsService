using System.ComponentModel.DataAnnotations;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class represents the outbound flight information in the BSM.
/// </summary>
public class OutboundFlight : ITypeB
{
    /// <summary>
    /// The property gets the airline for the flight.
    /// </summary>
    [Required]
    [RegularExpression("^[A-Z0-9]{2}$", ErrorMessage = "The airline must be 2 alphanumeric characters; the letters must be capital.")]
    public string Airline { get; set; } = string.Empty;

    /// <summary>
    /// The property gets the class of travel for passenger for this flight.
    /// </summary>
    [RegularExpression("^([A-Z]{1})?$", ErrorMessage = "The class of travel must be 1 capital letter or empty.")]
    public string ClassOfTravel { get; set; } = string.Empty;

    /// <summary>
    /// The property gets the destination for this flight.
    /// </summary>
    [RegularExpression("^([A-Z]{3})?$", ErrorMessage = "The destination must be 3 capital letters or empty.")]
    public string Destination { get; set; } = string.Empty;

    /// <summary>
    /// The constant for the .F element.
    /// </summary>
    public const string DotFElement = ".F";

    /// <summary>
    /// The property gets the date this flight flies.
    /// </summary>
    /// <remarks>
    /// This will be formatted like 01JAN.
    /// </remarks>
    [Required]
    [RegularExpression("^[0-9]{2}(JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)$", ErrorMessage = "The flight date must be the day of the month (2 digits) followed by the first three capital letters of the month.")]
    public string FlightDate { get; set; } = string.Empty;

    /// <summary>
    /// The property gets the identifier for the flight.
    /// </summary>
    [Required]
    [RegularExpression("^([0-9]{4})|([0-9]{4}[A-Z]{1})$", ErrorMessage = "The flight number must be 4 digits and optionally a capital letter.")]
    public string FlightNumber { get; set; } = string.Empty;

    /// <summary>
    /// The constant for the minimum character length for the airline/flight number element.
    /// </summary>
    private const int MiniumumFlightElementCharacterLength = 6;

    /// <summary>
    /// The default constructor.
    /// </summary>
    public OutboundFlight() { }

    /// <summary>
    /// The copy constructor.
    /// </summary>
    /// <param name="copy">The object to copy from.</param>
    public OutboundFlight(OutboundFlight copy)
    {
        Airline = copy.Airline;
        ClassOfTravel = copy.ClassOfTravel;
        Destination = copy.Destination;
        FlightDate = copy.FlightDate;
        FlightNumber = copy.FlightNumber;
    }

    /// <inheritdoc/>
    public void Parse(string typeBString)
    {
        //Remove the identifier so the elements can be broken apart with Split().
        typeBString = typeBString.Replace($"{DotFElement}{BSM.ElementSeparator}", string.Empty);
        typeBString = typeBString.Replace(Environment.NewLine, string.Empty);

        string[] elements = typeBString.Split(BSM.ElementSeparator);

        //Handle parsing the airline and flight number.
        if (elements.Length > 0 && string.IsNullOrEmpty(elements[0]) is false)
        {
            string airlineAndFlight = elements[0];

            if (airlineAndFlight.Length >= MiniumumFlightElementCharacterLength)
            {
                Airline = airlineAndFlight.Substring(0, 2);
                FlightNumber = airlineAndFlight.Substring(2);
            }
        }

        //Handle parsing the flight date.
        if (elements.Length > 1 && string.IsNullOrEmpty(elements[1]) is false)
        {
            FlightDate = elements[1];
        }

        //Handle parsing the destination.
        if (elements.Length > 2 && string.IsNullOrEmpty(elements[2]) is false)
        {
            Destination = elements[2];
        }

        //Handle parsing the class of travel.
        if (elements.Length > 3 && string.IsNullOrEmpty(elements[3]) is false)
        {
            ClassOfTravel = elements[3];
        }
    }

    /// <inheritdoc/>
    public string ToTypeB()
    {
        if (string.IsNullOrEmpty(ClassOfTravel) is false)
        {
            return $"{DotFElement}{BSM.ElementSeparator}{Airline}{FlightNumber}{BSM.ElementSeparator}{FlightDate}{BSM.ElementSeparator}{Destination}{BSM.ElementSeparator}{ClassOfTravel}{Environment.NewLine}";
        }
        else if (string.IsNullOrEmpty(Destination) is false)
        {
            return $"{DotFElement}{BSM.ElementSeparator}{Airline}{FlightNumber}{BSM.ElementSeparator}{FlightDate}{BSM.ElementSeparator}{Destination}{Environment.NewLine}";
        }
        else
        {
            return $"{DotFElement}{BSM.ElementSeparator}{Airline}{FlightNumber}{BSM.ElementSeparator}{FlightDate}{Environment.NewLine}";
        }
    }
}
