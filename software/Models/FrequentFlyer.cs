using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static PST2.BNEConsts;

namespace PST2
{
    /// <summary>
    /// This class represents a frequent flyer at BNE
    /// 
    /// RESPONSIBILITY: Hold information on a frequent flyer
    /// </summary>
    internal class FrequentFlyer : User
    {
        private int frequentFlyerNumber;
        private int frequentFlyerPoints;

        Flight? bookedArrivalFlight = null;
        Flight? bookedDepartureFlight = null;


        // Constructor
        /// <summary>
        /// Contstructor for a frequent flyer
        /// </summary>
        /// <param name="name">Users name</param>
        /// <param name="age">Users age</param>
        /// <param name="phoneNumber">Users phone number</param>
        /// <param name="emailAddress">Users email address</param>
        /// <param name="password">Users password</param>
        /// <param name="frequentFlyerNumber">Users frequent flyer number</param>
        /// <param name="frequentFlyerPoints">Users frequent flyer points</param>
        public FrequentFlyer(string name, int age, string phoneNumber, string emailAddress, string password, int frequentFlyerNumber, int frequentFlyerPoints) :  base(name, age, phoneNumber, emailAddress, password)
        {
            this.frequentFlyerNumber = frequentFlyerNumber;
            this.frequentFlyerPoints = frequentFlyerPoints;
        }

        // Properties
        /// <summary>
        /// Getter for frequent flyers account number
        /// </summary>
        public int FrequentFlyerNumber
        {
            get { return frequentFlyerNumber; }
        }

        /// <summary>
        /// Getter for frequent flyers points
        /// </summary>
        public int FrequentFlyerPoints
        {
            get { return frequentFlyerPoints; }
        }

        /// <summary>
        /// Getter for frequent flyers booked arrival flight
        /// </summary>
        public Flight? BookedArrivalFlight
        {
            get { return bookedArrivalFlight; }
        }

        /// <summary>
        /// Getter for frequent flyers booked departure flight
        /// </summary>
        public Flight? BookedDepartureFlight
        {
            get { return bookedDepartureFlight; }
        }

        // Methods
        /// <inheritdoc/>
        public override string GetDetails()
        {
            string details = base.GetDetails();
            details +=  $"\nFrequent flyer number: {FrequentFlyerNumber}\n" +
                        $"Frequent flyer points: {FrequentFlyerPoints:N0}";
            return details;
        }

        /// <summary>
        /// Retrieve points breakdown for frequent flyer
        /// </summary>
        /// <returns>formatted points breakdown</returns>
        public string GetPointsDetails()
        {
            int arrivalCityPoints = 0, departureCityPoints = 0;
            string stringToPrint = $"Your current points are: {FrequentFlyerPoints:N0}.\n";

            if (BookedArrivalFlight != null)
            {
                arrivalCityPoints = BNEConsts.CITY_POINTS[BookedArrivalFlight.City];
                stringToPrint += $"Your points from your arrival flight will be : {arrivalCityPoints:N0}.\n";
            }

            if (BookedDepartureFlight != null)
            {
                departureCityPoints = BNEConsts.CITY_POINTS[BookedDepartureFlight.City];
                stringToPrint += $"Your points from your departure flight will be : {departureCityPoints:N0}.\n";
            }

            int totalPoints = FrequentFlyerPoints + arrivalCityPoints + departureCityPoints;

            if (totalPoints > FrequentFlyerPoints)
            {
                string s = (BookedArrivalFlight == null || BookedDepartureFlight == null) ? "" : "s";
                stringToPrint += $"After completing your flight{s} your new points will be: {totalPoints:N0}.\n";
            }

            return stringToPrint;
        }

        /// <summary>
        /// Retrieve all flight details for a frequent flyer
        /// </summary>
        /// <returns>formatted flight details</returns>
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
        /// Book a flight for frequent flyer
        /// </summary>
        /// <param name="arrivalFlight">true if booking an arrival flight, false otherwise</param>
        /// <param name="bookingService">interface for booking service</param>
        /// <returns>formatted success message with flight details</returns>
        public string BookFlight(bool arrivalFlight, IBookFlights bookingService)
        {
            Flight selectedFlight = bookingService.ChooseFlight(arrivalFlight, BookedArrivalFlight, BookedDepartureFlight);
            string seatCode = bookingService.BookSeat(selectedFlight, this, true);
            if (arrivalFlight)
            {
                bookedArrivalFlight = selectedFlight;
            }
            else
            {
                bookedDepartureFlight = selectedFlight;
            }

            return ($"Congratulations. You have booked flight {selectedFlight.FlightCode} {(arrivalFlight ? "from" : "to")} {selectedFlight.City} {(arrivalFlight ? "arriving" : "departing")} at {selectedFlight.FlightDateTime.ToString(BNEConsts.dateTimeFormat)} and are seated in {seatCode}.");
        }
    }
}
