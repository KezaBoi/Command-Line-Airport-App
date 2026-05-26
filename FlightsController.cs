using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PST2
{
    /// <summary>
    /// This class acts as an intermediary between users and flights
    /// 
    /// RESPONSIBILITY: Manage user decisions regarding flights
    /// </summary>
    /// <param name="menu">Menu used throughout program</param>
    /// <param name="flightsDatabase">Flight database used by program</param>
    /// <param name="userInterface">User interface used throughout program</param>
    public class FlightsController (IMenu menu, IFlightsDatabase flightsDatabase, IUserInterface userInterface) : IBookFlights, IManageFlights
    {
        // Methods
        /// <inheritdoc/>
        public void RegisterFlight(bool arrivalFlight, out string city, out string airline, out string flightCode, out string planeCode, out DateTime flightDateTime)
        {
            menu.DisplayMenu(BNEConsts.AIRLINE_HEADER, BNEConsts.AIRLINES_MENU);
            int airlineChoice = menu.GetMenuOption(BNEConsts.AIRLINES_MENU.Count);
            KeyValuePair<string, string> airlines = BNEConsts.AIRLINE_CODES.ElementAt(airlineChoice);
            airline = airlines.Key;

            string cityHeader = string.Format(BNEConsts.CITIES_HEADER, (arrivalFlight ? "departing" : "arrival"));
            menu.DisplayMenu(cityHeader, BNEConsts.CITIES_MENU);
            int cityChoice = menu.GetMenuOption(BNEConsts.CITIES_MENU.Count);
            KeyValuePair<string, int> cities = BNEConsts.CITY_POINTS.ElementAt(cityChoice);
            city = cities.Key;

            int flightID = userInterface.GetValidInt("flight id", 100, 900);
            flightCode = airlines.Value + flightID;

            int planeID = userInterface.GetValidInt("plane id", 0, 9);
            planeCode = airlines.Value + planeID + (arrivalFlight ? "A" : "D");

            while (true)
            {
                userInterface.DisplayString($"Please enter in the {(arrivalFlight ? "arrival" : "departure")} date and time in the format HH:mm dd/MM/yyyy:");
                string? dateTime = userInterface.GetString();
                if (DateTime.TryParseExact(dateTime, BNEConsts.dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTimeConfirmed))
                {
                    flightDateTime = dateTimeConfirmed;
                    break;
                }
                userInterface.DisplayInputError("Invalid date time provided");
            }
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">Arrival/departure flight already booked</exception>
        /// <exception cref="ArgumentException">Arrival/departure time is invalid</exception>
        public Flight ChooseFlight(bool arrivalFlight, Flight? bookedArrivalFlight = null, Flight? bookedDepartureFlight = null)
        {
            if ((arrivalFlight ? bookedArrivalFlight : bookedDepartureFlight) != null)
            {
                throw new InvalidOperationException($"You already have {(arrivalFlight ? "an arrival" : "a departure")} flight. You can not book another");
            }

            List<Flight> availableFlights = flightsDatabase.GetAvailableFlights(arrivalFlight);
            List<string> flightMenu = flightsDatabase.GetDatabaseDetails(arrivalFlight);
            
            menu.DisplayMenu($"Please enter the {(arrivalFlight ? "arrival" : "departure")} flight:", flightMenu);
            int option = menu.GetMenuOption(flightMenu.Count);
            Flight selectedFlight = availableFlights[option];

            if (!arrivalFlight)
            {
                if (bookedArrivalFlight != null)
                {
                    if (bookedArrivalFlight.FlightDateTime > selectedFlight.FlightDateTime)
                    {
                        throw new ArgumentException("The departure time must be after the arrival time");
                    }
                }
            } else 
            {
                if (bookedDepartureFlight != null)
                {
                    if (bookedDepartureFlight.FlightDateTime < selectedFlight.FlightDateTime)
                    {
                        throw new ArgumentException("The arrival time must be before the departure time");
                    }
                }
            }

            return selectedFlight;
        }

        /// <inheritdoc/>
        public string BookSeat(Flight selectedFlight, User currentUser, bool prioritySeating = false)
        {
            Dictionary<string, int> letterToIndex = new Dictionary<string, int>
                        {
                            { "A", 0 },
                            { "B", 1 },
                            { "C", 2 },
                            { "D", 3 }
                        };

            int seatRow;
            string seatColumn;


            while (true)
            {
                seatRow = userInterface.GetValidInt("seat row", 1, 10);

                while (true)
                {
                    userInterface.DisplayString("Please enter in your seat column between A and D:");
                    seatColumn = userInterface.GetString();
                    if (Regex.IsMatch(seatColumn, BNEConsts.SeatColumnPattern))
                    {
                        break;
                    }
                    userInterface.DisplayInputError("Supplied seat column is invalid");
                }


                try
                {
                    selectedFlight.BookSeat(seatRow - 1, letterToIndex[seatColumn], currentUser, prioritySeating);
                    break;
                }
                catch (InvalidOperationException ex)
                {
                    userInterface.DisplayInputError(ex.Message);
                }

            }
            string seatCode = selectedFlight.GetUserSeatCode(currentUser);
            return seatCode;
        }

        /// <inheritdoc/>
        public void DelayFlight(bool arrivalFlight)
        {
            try
            {
                Flight selectedFlight = ChooseFlight(arrivalFlight);
                userInterface.DisplayString("Please enter in your minutes delayed:");
                int input = userInterface.GetInt();
                if (arrivalFlight)
                {
                    try
                    {
                        Flight correspondingDepartureFlight = flightsDatabase.GetCorrespondingFlight(selectedFlight);
                        correspondingDepartureFlight.DelayFlight(input);
                    } catch (InvalidOperationException)
                    {
                    }
                }
                selectedFlight.DelayFlight(input);
            }
            catch (InvalidOperationException ex)
            {
                userInterface.DisplayString(ex.Message);
            }
        }

    }
}
