# Windows Service Example Project

This example project launches a TCP/IP server and a TCP/IP client and the client will connect to the local server. The server then generates a simplified BSM which is sent to the client. The received BSM string is parsed and also printed on the console.

A BSM (baggage source message) is used in a BHS (baggage handling system). When a person checks baggage at a ticket counter, an IATA tag is printed and attached to each checked in bag. The ticketing system will generate a BSM for the IATA(s) printed and it is sent to various parties; one being the 
BHS. The BHS will use the BSM in sortation; it binds the IATA number to the flight which has a sort destination assigned to it.

## BSM Format
BSM<br/>
&nbsp;&nbsp;ADD<br/>
&nbsp;&nbsp;.F/AA0001/09FEB/LBB/T<br/>
&nbsp;&nbsp;.N/0001000004001<br/>
&nbsp;&nbsp;.P/1TEST/PASSENGER4<br/>
&nbsp;&nbsp;.V/1LMCO<br/>
ENDBSM<br/>

* The .F element represents the outbound flight.
* The .N element represents the IATA(s) printed for the baggage checked in by the person.
* The .P element represents who checked in the baggage.
* The .V element represents version information.
