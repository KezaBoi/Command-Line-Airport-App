using System.Text.RegularExpressions;

namespace PST2
{
    /// <summary>
    /// This class represents a standard traveller at BNE
    /// 
    /// RESPONSIBILITY: Hold information on a standard traveller
    /// </summary>
    internal class StandardTraveller : User
    {
        // Fields

        Flight? bookedArrivalFlight = null;
        Flight? bookedDepartureFlight = null;



        // Constructors
        /// <summary>
        /// Contstructor for a standard traveller
        /// </summary>
        /// <param name="name">Travellers name</param>
        /// <param name="age">Travellers age</param>
        /// <param name="phoneNumber">Travellers phone number</param>
        /// <param name="emailAddress">Travellers email address</param>
        /// <param name="password">Travellers password</param>
        public StandardTraveller(string name, int age, string phoneNumber, string emailAddress, string password) : base(name, age, phoneNumber, emailAddress, password)
        {
        }



        // Properties
        /// <summary>
        /// Get standard travellers booked arrival flight
        /// </summary>
        public Flight? BookedArrivalFlight
        {
            get { return bookedArrivalFlight; }
        }

        /// <summary>
        /// Get standard travellers booked departure flight
        /// </summary>
        public Flight? BookedDepartureFlight
        {
            get { return bookedDepartureFlight; }
        }



        // Methods
        /// <summary>
        /// Retrieve details of booked flights
        /// </summary>
        /// <returns>string with details of all booked flights</returns>
        public string GetFlightDetails()
        {
            string stringToPrint = $"Showing flight details for {Name}:\n";

            if (BookedArrivalFlight != null)
            {
                stringToPrint += "Arrival Flight: ";
                stringToPrint += $"Flight {BookedArrivalFlight.FlightCode} from {BookedArrivalFlight.City} arriving at {BookedArrivalFlight.FlightDateTime.ToString(BNEConsts.dateTimeFormat)} in seat {BookedArrivalFlight.GetUserSeatCode(this)}.\n";
            }

            if (BookedDepartureFlight != null)
            {
                stringToPrint += "Departure Flight: ";
                stringToPrint += $"Flight {BookedDepartureFlight.FlightCode} to {BookedDepartureFlight.City} departing at {BookedDepartureFlight.FlightDateTime.ToString(BNEConsts.dateTimeFormat)} in seat {BookedDepartureFlight.GetUserSeatCode(this)}.\n";
            }
            return stringToPrint;
        }

        /// <summary>
        /// Book a flight for a standard traveller
        /// </summary>
        /// <param name="arrivalFlight">true if booking an arrival flight, false otherwise</param>
        /// <param name="bookingService">interface for booking service</param>
        /// <returns>formatted success message with flight details</returns>
        public string BookFlight(bool arrivalFlight, IBookFlights bookingService)
        {
            Flight selectedFlight = bookingService.ChooseFlight(arrivalFlight, BookedArrivalFlight, BookedDepartureFlight);                

            if (arrivalFlight)
            {
                bookedArrivalFlight = selectedFlight;
            }
            else
            {
                bookedDepartureFlight = selectedFlight;
            }

            string seatCode = bookingService.BookSeat(selectedFlight, this);

            return ($"Congratulations. You have booked flight {selectedFlight.FlightCode} {(arrivalFlight ? "from" : "to")} {selectedFlight.City} {(arrivalFlight ? "arriving" : "departing")} at {selectedFlight.FlightDateTime.ToString(BNEConsts.dateTimeFormat)} and are seated in {seatCode}.");
        }
    }
}
