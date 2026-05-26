using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PST2
{
    /// <summary>
    /// Booking service for users who book flights
    /// 
    /// RESPONSIBILITY: Book users into flights
    /// </summary>
    public interface IBookFlights : IBookingService
    {
        /// <summary>
        /// Book a user into a seat on a flight
        /// </summary>
        /// <param name="selectedFlight">flight to be seated on</param>
        /// <param name="currentUser">user to be seated</param>
        /// <param name="prioritySeating">kicks lower priority users from selected seat</param>
        /// <returns>seat code of booked seat</returns>
        string BookSeat(Flight selectedFlight, User currentUser, bool prioritySeating = false);
    }
}
