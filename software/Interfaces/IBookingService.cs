using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PST2
{
    /// <summary>
    /// General flight interaction service for all users of the airport
    /// 
    /// RESPONSIBILITY: Assist any user in flight managment process
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Select a flight from a menu of all available arrival/departure flights
        /// </summary>
        /// <param name="arrivalFlight">true if choosing an arrival flight, false for departure</param>
        /// <param name="bookedArrivalFlight">if booking flight, </param>
        /// <param name="bookedDepartureFlight">users current booked departure flight</param>
        /// <returns>selected flight</returns>
        Flight ChooseFlight(bool arrivalFlight, Flight? bookedArrivalFlight = null, Flight? bookedDepartureFlight = null);
        
    }
}
