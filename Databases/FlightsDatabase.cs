using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PST2
{
    /// <summary>
    /// A class to hold all flights in the system
    /// 
    /// RESPONSIBILITY: Remember flights
    /// </summary>
    public class FlightsDatabase : IFlightsDatabase
    {
        private static Dictionary<string, Flight> arrivalFlightsDatabase = new Dictionary<string, Flight>();
        private static Dictionary<string, Flight> departureFlightsDatabase = new Dictionary<string, Flight>();


        /// <inheritdoc/>
        public void AddFlight(Flight newFlight)
        {
            if (newFlight.FlightArriving)
            {
                arrivalFlightsDatabase.Add(newFlight.PlaneCode, newFlight);
            } else
            {
                departureFlightsDatabase.Add(newFlight.PlaneCode, newFlight);
            }
        }

        /// <inheritdoc/>
        public bool DatabaseContains(string planeCode)
        {
            if (arrivalFlightsDatabase.ContainsKey(planeCode) || departureFlightsDatabase.ContainsKey(planeCode))
            {
                return true;
            } else { return false; }
        }

        /// <inheritdoc/>
        public string GetDetails()
        {
            string allFlightDetails = "";


            allFlightDetails += "Arrival Flights:\n";
            try
            {
                string combinedArrivalsList = string.Join(string.Empty, GetDatabaseDetails(true));
                allFlightDetails += combinedArrivalsList;
            }
            catch (InvalidOperationException)
            {
                allFlightDetails += "There are no arrival flights.\n";
            }
            

            allFlightDetails += "Departure Flights:\n";
            try
            {
                string combinedDeparturesList = string.Join(string.Empty, GetDatabaseDetails(false));
                allFlightDetails += combinedDeparturesList;
            }
            catch (InvalidOperationException)
            {
                allFlightDetails += "There are no departure flights.\n";
            }

            return allFlightDetails;
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">The database for the requested type of flight is empty</exception>
        public List<string> GetDatabaseDetails(bool arrivalFlights)
        {
            Dictionary<string, Flight> database = new Dictionary<string, Flight>();
            if (arrivalFlights)
            {
                database = arrivalFlightsDatabase;
            }
            else
            {
                database = departureFlightsDatabase;
            }


            List<string> databaseDetails = new List<string>();
            if (database.Count > 0)
            {
                Dictionary<string, Flight> sortedDatabase = database
                                            .OrderBy(dictionaryItem => dictionaryItem.Value.FlightDateTime)
                                            .ToDictionary(dictionaryItem => dictionaryItem.Key, dictionaryItem => dictionaryItem.Value);
                foreach (Flight flight in sortedDatabase.Values)
                {
                    databaseDetails.Add($"Flight {flight.FlightCode} operated by {flight.Airline} {(flight.FlightArriving ? "arriving" : "departing")} at {flight.FlightDateTime.ToString(BNEConsts.dateTimeFormat)} {(flight.FlightArriving ? "from" : "to")} {flight.City} on plane {flight.PlaneCode}.\n");
                }
                return databaseDetails;
            }
            else
            {
                throw new InvalidOperationException($"The airport does not have any {(arrivalFlights ? "arrival" : "departure")} flights.");
            }
        }

        /// <inheritdoc/>
        public List<Flight> GetAvailableFlights(bool arrivalFlights)
        {
            List<Flight> availableFlights;

            if (arrivalFlights)
            {
                availableFlights = arrivalFlightsDatabase
                                    .OrderBy(dictionaryItem => dictionaryItem.Value.FlightDateTime)
                                    .Select(dictionaryItem => dictionaryItem.Value)
                                    .ToList();
            } else
            {
                availableFlights = departureFlightsDatabase
                                    .OrderBy(dictionaryItem => dictionaryItem.Value.FlightDateTime)
                                    .Select(dictionaryItem => dictionaryItem.Value)
                                    .ToList();
            }
            return availableFlights;
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">no corresponding flights found</exception>
        public Flight GetCorrespondingFlight(Flight arrivalFlight)
        {
            foreach (Flight flight in departureFlightsDatabase.Values)
            {
                if (flight.PlaneCode.Remove(4) == arrivalFlight.PlaneCode.Remove(4))
                {
                    return flight;
                }
            }
            throw new InvalidOperationException("No corresponding flights");
        }


    }
}
