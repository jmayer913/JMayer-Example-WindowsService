using JMayer.Net.ProtocolDataUnit;
using System.Text;

namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The class manages parsing the BSM.
/// </summary>
public class BSMParser : PDUParser
{
    /// <inheritdoc/>
    protected override PDUParserResult SubClassParse(byte[] bytes)
    {
        int totalBytesProcessed = 0;
        List<PDU> pdus = [];

        string bytesAsString = Encoding.ASCII.GetString(bytes);

        do
        {
            int startIndex = bytesAsString.IndexOf(BSM.StartOfBSM, totalBytesProcessed);

            //Start was not found so exit.
            if (startIndex == -1)
            {
                break;
            }

            int endIndex = bytesAsString.IndexOf(BSM.EndOfBSM, startIndex);

            //End was not found or start is actually the end, exit.
            if (endIndex == -1 || startIndex == endIndex)
            {
                break;
            }

            //Add the length of the end of BSM so the end is included.
            endIndex += BSM.EndOfBSM.Length;

            //Ensures the new line is included, if it exists.
            if (endIndex < bytesAsString.Length && bytesAsString[endIndex] == '\n')
            {
                endIndex++;
            }
            else if (endIndex + 1 < bytesAsString.Length && bytesAsString[endIndex] == '\r' && bytesAsString[endIndex + 1] == '\n')
            {
                endIndex += 2;
            }

            string bsmString = bytesAsString.Substring(startIndex, endIndex - startIndex);

            BSMPDU pdu = new();
            pdu.BSM.Parse(bsmString);

            pdus.Add(pdu);
            totalBytesProcessed += endIndex - startIndex;

        } while (totalBytesProcessed < bytes.Length);

        return new PDUParserResult(pdus, totalBytesProcessed);
    }
}
