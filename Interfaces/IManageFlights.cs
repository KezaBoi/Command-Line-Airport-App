using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PST2
{
    /// <summary>
    /// Management service for users who manage flights
    /// 
    /// RESPONSIBILITY: Manage flights in database
    /// </summary>
    public interface IManageFlights : IBookingService
    {
        /// <summary>
        /// Register a new flight
        /// </summary>
        /// <param name="arrivalFlight">true if booking arrival flight, false if departure</param>
        /// <returns>created flight</returns>
        void RegisterFlight(bool arrivalFlight, out string city, out string airline, out string flightCode, out string planeCode, out DateTime flightDateTime);


        /// <summary>
        /// Process to delay a flight 
        /// </summary>
        /// <param name="arrivalFlight">true if delaying arrival flight, false if departure</param>
        void DelayFlight(bool arrivalFlight);
    }
}
