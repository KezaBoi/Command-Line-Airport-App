namespace PST2
{
    /// <summary>
    /// The entry point for the the program.
    /// 
    /// The program will facilitate brisbanes domesitic airport needs.
    /// There is currently functionality for three user types:
    ///     Standard Travellers, Frequent Flyers & Flight Managers
    /// 
    /// RESPONSIBILITY: The program's entry point. 
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main program.
        /// </summary>
        /// <param name="args">An array of command line arguments. Not used.</param>
        static void Main(string[] args)
        {
            // UserInterface to be used for the program
            IUserInterface userInterface = new CmdLineUI();
            
            // Menu to be used for the program (using user interface)
            IMenu BNEMenu = new BNEMenu(userInterface);

            // User Database to be used for the program
            IUserDatabase userDatabaseClass = new UserDatabase();

            // Flight Database to be used for the program
            IFlightsDatabase flightDatabaseClass = new FlightsDatabase();

            // User Controller to be used for the program
            IUserController userController = new UserController(userInterface, BNEMenu, userDatabaseClass);

            // Booking and managing services for flights
            IBookFlights bookingFlights = new FlightsController(BNEMenu, flightDatabaseClass, userInterface);
            IManageFlights manageFlights = new FlightsController(BNEMenu, flightDatabaseClass, userInterface);


            // create the app and run it
            BNEController app = new BNEController(userInterface, BNEMenu, flightDatabaseClass, userDatabaseClass, userController, bookingFlights, manageFlights);
            app.Run();

        }
    }
}
