using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PST2
{
    /// <summary>
    /// A database to hold flights
    /// 
    /// RESPONSIBILITY: Hold all flights in the system
    /// </summary>
    public interface IFlightsDatabase : IDisplayable
    {
        /// <summary>
        /// Add a flight to the database
        /// </summary>
        /// <param name="newFlight">flight to be added</param>
        void AddFlight(Flight newFlight);

        /// <summary>
        /// Check if entire database contains a flight
        /// </summary>
        /// <param name="planeCode">flights plane code</param>
        /// <returns>true if database contains flight, false otherwise</returns>
        bool DatabaseContains(string planeCode);

        /// <summary>
        /// Retrieve a list of available flights on arrival/departure database
        /// </summary>
        /// <param name="arrivingFlight">true if flight arriving, false otherwise</param>
        /// <returns>list of available flights</returns>
        List<Flight> GetAvailableFlights(bool arrivingFlight);

        /// <summary>
        /// Retrieve a list of strings with all arrival/departure flights information 
        /// </summary>
        /// <param name="arrivalFlights">true if flight arriving, false otherwise</param>
        /// <returns>flight information from database</returns>
        List<string> GetDatabaseDetails(bool arrivalFlights);

        /// <summary>
        /// Find the corresponding departure flight for an arrival flight
        /// </summary>
        /// <param name="arrivalFlight">arrival flight looking for match</param>
        /// <returns>corresponding departure flight</returns>
        Flight GetCorrespondingFlight(Flight arrivalFlight);
    }
}
