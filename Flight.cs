using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace PST2
{
    /// <summary>
    /// Holds data & methods relating to individual flights
    /// 
    /// RESPONSIBILITY: Represent a flight
    /// </summary>
    public class Flight
    {
        // Fields
        private readonly bool flightArriving;
        private readonly string airline;
        private readonly string flightCode;
        private readonly string city;
        private readonly string planeCode;
        private DateTime flightDateTime;
        private User[,] bookedSeats = new User[10, 4]; 
        


        //Properties
        /// <summary>
        /// Getter, true if flight is an arrival flight, false otherwise
        /// </summary>
        public bool FlightArriving
        {
            get { return flightArriving; }
        }

        /// <summary>
        /// Getter for flights airline
        /// </summary>
        public string Airline
        {
            get { return airline; }
        }

        /// <summary>
        /// Getter for flighs flight code
        /// </summary>
        public string FlightCode
        {
            get { return flightCode; }
        }

        /// <summary>
        /// Getter for flights arrival/departure city
        /// </summary>
        public string City
        {
            get { return city; }
        }

        /// <summary>
        /// Getter for flights plane code
        /// </summary>
        public string PlaneCode
        {
            get { return planeCode; }
        }

        /// <summary>
        /// Getter and private setter for flights arrival/departure date and time
        /// </summary>
        public DateTime FlightDateTime
        {
            get { return flightDateTime; }
            private set { flightDateTime = value; }
        }

        /// <summary>
        /// Getter for an array of users, representing seats on the flight
        /// </summary>
        public User[,] BookedSeats
        {
            get { return bookedSeats; }
        }



        // Constructor
        /// <summary>
        /// Constructor for a flight
        /// </summary>
        /// <param name="flightArriving">True if flight arriving, false otherwise</param>
        /// <param name="city">flights arrival/departure city</param>
        /// <param name="airline">flights airline</param>
        /// <param name="flightCode">flights flight code</param>
        /// <param name="planeCode">flights plane code</param>
        /// <param name="flightDateTime">flight arrival/departure date and time</param>
        public Flight(bool flightArriving, string city, string airline, string flightCode, string planeCode, DateTime flightDateTime)
        {
            this.flightArriving = flightArriving;
            this.city = city;
            this.airline = airline;
            this.flightCode = flightCode;
            this.planeCode = planeCode;
            this.flightDateTime = flightDateTime;
        }


        // Methods
        /// <summary>
        /// Book a seat for a user on a flight
        /// </summary>
        /// <param name="seatRow">seat row to book</param>
        /// <param name="seatColumn">seat column to book</param>
        /// <param name="user">user boooking seat</param>
        /// <param name="prioritySeating">wether user has priority seating</param>
        /// <exception cref="InvalidOperationException">seat is occupied, and user does not have priority seating</exception>
        public void BookSeat(int seatRow, int seatColumn, User user, bool prioritySeating = false)
        {
            User userInSeat = BookedSeats[seatRow, seatColumn];
            if (userInSeat == null)
            {
                BookedSeats[seatRow, seatColumn] = user;
            }
            else if ((userInSeat is StandardTraveller) && prioritySeating)
            {
                BookNextAvailableSeat(seatRow, seatColumn, userInSeat);
                BookedSeats[seatRow, seatColumn] = user;
            }
            else
            {
                throw new InvalidOperationException("Seat is already occupied");
            }
        }

        /// <summary>
        /// Book a user into a new seat if theyeve been kicked from their seat
        /// </summary>
        /// <param name="seatRow">the seat row to be booked</param>
        /// <param name="seatColumn">the seat column to be booked</param>
        /// <param name="kickedUser">the user kicked from the seat</param>
        /// <param name="searchResetFlag">if the search has hit the back of the plane, set to true, false otherwise</param>
        /// <exception cref="InvalidOperationException">no seats left on the flight</exception>
        private void BookNextAvailableSeat(int seatRow, int seatColumn, User kickedUser, bool searchResetFlag = false)
        {
            User userInSeat = BookedSeats[seatRow, seatColumn];
            if (userInSeat == null)
            {
                BookedSeats[seatRow, seatColumn] = kickedUser;
                return;
            }

            if (seatColumn < BookedSeats.GetLength(1) - 1)
            {
                BookNextAvailableSeat(seatRow, seatColumn + 1, kickedUser, searchResetFlag);
            }
            else if (seatRow < BookedSeats.GetLength(0) - 1)
            {
                BookNextAvailableSeat(seatRow + 1, BNEConsts.START_INDEX, kickedUser, searchResetFlag);
            }
            else if (!searchResetFlag)
            {
                BookNextAvailableSeat(BNEConsts.START_INDEX, BNEConsts.START_INDEX, kickedUser, true);
            }
            else
            {
                throw new InvalidOperationException("There are no available seats on the plane");
            }
        }

        /// <summary>
        /// Retrieve the code for the users current seat on the flight
        /// </summary>
        /// <param name="currentUser">user looking for seat</param>
        /// <returns>seat code for user</returns>
        public string GetUserSeatCode (User currentUser)
        {
            Dictionary<int, char> indexToChar = new Dictionary<int, char>
            {
                { 0, 'A' },
                { 1, 'B' },
                { 2, 'C' },
                { 3, 'D' },
            };
            
            
            int seatColumn = BNEConsts.START_INDEX, seatRow = BNEConsts.START_INDEX;
            
            for (int col = BNEConsts.START_INDEX; col < bookedSeats.GetLength(1); col++)
            {
                for (int row = BNEConsts.START_INDEX; row < bookedSeats.GetLength(0); row++)
                {
                    if (bookedSeats[row, col] == null)
                    {
                        continue;
                    }
                    else if (bookedSeats[row, col].Equals(currentUser))
                    {
                        seatColumn = col;
                        seatRow = row;
                    }
                }
            }

            return ((seatRow + 1) + ":" + indexToChar[seatColumn]);

        }
        
        /// <summary>
        /// Delay a flight by a set number of minutes
        /// </summary>
        /// <param name="minutesDelayed">minutes to delay flight by</param>
        public void DelayFlight(int minutesDelayed)
        {
            FlightDateTime = FlightDateTime.AddMinutes(minutesDelayed);
            return;
        }
    }
}
