namespace PST2
{
    /// <summary>
    /// This class represents a flight manager at BNE
    /// 
    /// RESPONSIBILITY: Hold information on a flight manager
    /// </summary>
    internal class FlightManager : User
    {
        // Fields
        private readonly int staffID;


        // Properties
        /// <summary>
        /// Getter for staff id
        /// </summary>
        public int StaffID
        {
            get { return staffID; }
        }


        // Constructors
        /// <summary>
        /// Contstructor for a flight manager
        /// </summary>
        /// <param name="name">Users name</param>
        /// <param name="age">Users age</param>
        /// <param name="phoneNumber">Users phone number</param>
        /// <param name="emailAddress">Users email address</param>
        /// <param name="password">Users password</param>
        /// <param name="staffID">Users staff id</param>
        public FlightManager(string name, int age, string phoneNumber, string emailAddress, string password, int staffID) : base(name, age, phoneNumber, emailAddress, password)
        {
            this.staffID = staffID;
        }


        // Methods
        /// <inheritdoc/>
        public override string GetDetails()
        {
            string details = base.GetDetails();
            details += $"\nStaff ID: {StaffID}";
            return details;
        }

        /// <summary>
        /// Creat a new arrival/departure flight
        /// </summary>
        /// <param name="flightsController">interface for manging flights</param>
        /// <param name="flightsDatabase">interface for flights database</param>
        /// <param name="arrivalFlight">true if arrival flight, false otherwise</param>
        /// <returns>success message with plane added to system</returns>
        /// <exception cref="InvalidOperationException">plane choosen has already been assigned a flight</exception>
        public string CreateFlight(IManageFlights flightsController, IFlightsDatabase flightsDatabase, bool arrivalFlight)
        {
            flightsController.RegisterFlight(arrivalFlight, out string city, out string airline, out string flightCode, out string planeCode, out DateTime flightDateTime);
            if (flightsDatabase.DatabaseContains(planeCode))
            {
                throw new InvalidOperationException($"Plane {planeCode} has already been assigned to {(arrivalFlight ? "an arrival" : "a departure")} flight");
            }
            Flight newFlight = new Flight(arrivalFlight, city, airline, flightCode, planeCode, flightDateTime);
            flightsDatabase.AddFlight(newFlight);
            return $"Flight {newFlight.FlightCode} on plane {newFlight.PlaneCode} has been added to the system.\n";
        }
    }
}
